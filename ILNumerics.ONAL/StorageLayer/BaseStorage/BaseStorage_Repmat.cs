// MIT License
// 
// Copyright (c) 2026 ILNumerics GmbH
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
using ILNumerics.Core.Arrays;
using ILNumerics.Core.DeviceManagement;
using ILNumerics.Core.Functions.Builtin;
using ILNumerics.Core.Global;
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.StorageLayer {

    public abstract partial class BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> : IDisposable, IStorage  
        where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

        /// <summary>
        /// Replicates this array along specified dimensions.
        /// </summary>
        /// <param name="rep">Array specifying the repetition factors along each dimension.</param>
        /// <returns>New storage with elements of this array being repeatedly copied as specified by <paramref name="rep"/>.</returns>
        /// <remarks></remarks>
        /// <exception cref="ArgumentNullException">If <paramref name="rep"/> is null.</exception>
        
        internal unsafe virtual StorageT Repmat(Storage<long> rep) { 

            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {

                if (Equals(rep, null)) {
                    throw new ArgumentNullException(nameof(rep));
                }

                if (rep.S.NumberOfElements > Size.MaxNumberOfDimensions) {
                    throw new ArgumentException($"The maximum number of dimensions is currently limited to {Size.MaxNumberOfDimensions}. Argument rep has too many elements: {rep.S.NumberOfElements}."); 
                }

                // determine output size (numel)
                var outLen = 1L; 
                for (int i = 0; i < rep.S.NumberOfElements; i++) {
                    outLen *= rep.GetValue(i); 
                }

                var ret = Create();
                ret.Handles[0] = DeviceManager.GetDevice(0).New<T>((ulong)(outLen * Size.NumberOfElements));
                var outBSD = ret.S.GetBSD(true); 
                // set output size
                uint ndims = (uint)Math.Max(rep.S.NumberOfElements, Size.NumberOfDimensions);
                //long* dims = stackalloc long[(int)ndims]; 
                var dimspecs = Context.DimSpecArray;
                long stride = 1; 
                for (uint i = 0; i < ndims; i++) {
                    //dims[i] = (i < rep.size) ? rep.GetValue(i) : 1;
                    outBSD[3 + i] = Size[i] * ((i < rep.S.NumberOfElements) ? rep.GetValue(i) : 1L);
                    outBSD[3 + ndims + i] = outBSD[3 + i] == 1 ? 0 : stride;
                    stride *= outBSD[3 + i]; 
                }
                outBSD[0] = ndims;
                outBSD[1] = outLen * Size.NumberOfElements;
                outBSD[2] = 0;
                (ret as LogicalStorage)?.SetNumberTrues((this as LogicalStorage).NumberTrues * outLen); 

                if (outBSD[1] == 0) {
                    return ret; 
                }
                StorageT val = (this as StorageT);
                val.Retain(); // so that we survive the Release below

                for (uint d = 0; d < rep.S.NumberOfElements; d++) {
                    // within my dimensions we need to copy via Get/SetRange_ML

                    if (d + 1 >= val.S.NumberOfDimensions && ElementInstance is ValueType) {
                        // 'd == val.S.NumberOfDimension' -> next dimension higher than val.size. 
                        byte* retP = (byte*)ret.Handles[0].Pointer;  
                        
                        // determine the length of bytes to copy to start with
                        long len = SizeOfT;
                        uint k0 = 0;
                        while (k0 <= d) len *= ret.S[k0++]; 
                        len /= rep.GetValue(d); // last dim is prefilled with 1 x val. safe against div/0. Checked on outLen == 0 above.  

                        // straight memory copies, from now on to end..
                        for (; d < rep.S.NumberOfElements; d++) {
                            long r = rep.GetValue(d);
                            byte* srcP;
                            if (d == 0) {
                                srcP = (byte*)val.Handles[0].Pointer + val.Size.BaseOffset * SizeOfT;
                            } else {
                                srcP = retP;
                            }

                            // long len = (d + 1 == ret.size) ? 
                            for (long k = (d == 0 ? 0 : 1); k < r; k++) {
                                Buffer.MemoryCopy(
                                    source: srcP,
                                    destination: retP + k * len,
                                    destinationSizeInBytes: len, // actually: outLen * m_size.NumberOfElements * SizeOf_T
                                    sourceBytesToCopy: len);
                                //Native.NativeMethods.CopyMemory(
                                //    dest: retP + k * len,
                                //    source: srcP,
                                //    length: (ulong)len); 
                            }
                            len *= r; 
                        }
                        val.Release(); 
                        return ret; 
                    } else {
                        // when copying the current block r times along d, keep in mind that this block may has more dims than d! 
                        // Below we create DimSpec[] definitions to apply to the target (ret) in SetRange_ML. 
                        // The size/shape of the region to address is always equal to val, shifted along d according to current k.

                        #region prepare source storage for next dimension repetitions: includes all lower dimensions as 'full'
                        uint nBlockD = Math.Max(d + 1, val.S.NumberOfDimensions);
                        if (d > 0) {

                            val.Release();
                            for (uint l = 0; l < nBlockD; l++) {
                                if (l < d) {
                                    dimspecs[l] = Globals.full;
                                } else {
                                    dimspecs[l] = Globals.r(0, val.S[l] - 1);
                                }
                            }
                            val = ret.GetRange_ML(dimspecs, false, nBlockD);
                            val.Retain(); // GetRange_ML returns refCount = 0!
                            for(uint i = 0; i < nBlockD; i++) dimspecs[i]?.Dispose();
                        }
                        #endregion

                        long r = rep.GetValue(d);
                        for (long k = d == 0 ? 0 : 1; k < r; k++) {
                            // prepare dimspecs. Must do this for each call to SetRange_ML!
                            // TODO: use Write_To or CopyTo or something instead .... 
                            uint l = 0;
                            // all dims we have been through already can be handled by 'full' dimensions of ret. 
                            for (; l < d; l++) dimspecs[l] = Globals.full;
                            // current dim: 
                            dimspecs[d] = Globals.r(k * Size[d], (k + 1) * Size[d] - 1);
                            System.Diagnostics.Debug.Assert(Size[d] == val.S[d]); 
                            l++;
                            for (; l < val.S.NumberOfDimensions; l++) dimspecs[l] = Globals.r(0, val.S[l] - 1);
                            var newRet = ret.SetRange_ML(val, dimspecs, val.S.NumberOfDimensions);  // <=== here! check how to properly deal with immutability for thread safety ! this was a version 7 new feature ... (but it seems to remain untested :| )
                            if (!object.ReferenceEquals(newRet, ret)) {
                                if (ret.ReferenceCount == 0) {
                                    ret.Retain(); ret.Release();
                                }
                                ret = newRet;
                            }

                            for (uint i = 0; i < l; i++) dimspecs[i]?.Dispose(); 
                        }
                    }
                }
                val.Release(); 
                return ret; 
            }
        }
    }
}
