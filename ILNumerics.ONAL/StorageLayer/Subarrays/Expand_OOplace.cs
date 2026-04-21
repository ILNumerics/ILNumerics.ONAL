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
using System;
using System.Security;

namespace ILNumerics.Core.StorageLayer {
    public abstract partial class BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> : IDisposable, IStorage
        where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {


        #region Expand out of-place interface, used by regular (non-accelerated) Array<T> & Co. 

        /// <summary>
        /// Creates a new storage similar to this storage with the given dimensions expanded to the specified lengths. 
        /// </summary>
        /// <param name="lengths">Vector with dimension lengths. Size: 0...[Size.MaxNumberOfDimensions].</param>
        /// <returns>A new storage with the expanded size.</returns>
        /// 
        
        internal unsafe StorageT Expand_OOP(Span<long> lengths) {

            var storage = this as StorageT; 

            //if (storage.ReferenceCount > 1 && storage.IsReady) {
            //    throw new InvalidOperationException("This array can not be expanded currently because other arrays share the same storage. Check input-parameters of the current scope and make sure that the original array in the callers scope has not been used as input parameter AND output parameter at the same time. Consider providing a clone of the original array A when trying to mutate the array and when assigning to itself: A[1,full] = A.C");
            //}
           
            if (lengths == null || lengths.Length == 0)
                return storage;

            if (lengths.Length > Size.MaxNumberOfDimensions) {
                throw new ArgumentException($"Number of parameters exceeds maximum number of dimensions ({Size.MaxNumberOfDimensions})."); 
            }
            if (lengths.Length < storage.Size.NumberOfDimensions && lengths[lengths.Length-1] > storage.Size[(uint)lengths.Length - 1]) {
                // Fewer dimension as existing in this array were specified and the last specified dimension is an expanding one. 
                // Ambiguities arise only when higher (unspecified) dimensions of this array do actually store more elements. 
                // Otherwise, the length of the last dimension cannot reach out into such an higher (merged-into-last-specified) 
                // dimension and it would be clear that the last dimension was meant to expand this array.
                for (uint t = (uint)lengths.Length; t < storage.Size.NumberOfDimensions; t++) {
                    if (storage.Size[t] > 1) {
                        throw new ArgumentException($"Unable to expand array {storage.Size.ToString()} along ambigous dimension #{lengths.Length - 1}! Consider specifying all dimensions when expansion is intended or to prevent from expansion by pre-creating a properly sized array.");
                    }
                }
            }
            int i = 0;
            long nelem = 1;
            int ndims = Math.Max(lengths.Length, (int)storage.Size.NumberOfDimensions); 
            for (; i < lengths.Length; i++) {
                lengths[i] = Math.Max(lengths[i], storage.Size[i]); 
                nelem *= lengths[i];
            }
            for (; i < storage.Size.NumberOfDimensions; i++) {
                nelem *= storage.Size[i];
            }

            var newCA = CountableArray.Create();
            var ret = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Create(newCA);
            newCA[0] = storage.New((ulong)nelem, true);

            #region prepare new, expanded myBSD
            long* outBSD = stackalloc long[ndims * 2 + 3];
            outBSD[0] = ndims;
            outBSD[1] = nelem;
            outBSD[2] = 0;
            for (i = 0; i < lengths.Length; i++) {
                outBSD[3 + i] = lengths[i];
            }
            for (; i < ndims; i++) {
                outBSD[i + 3] = (storage.Size[i]);
            }
            long stride = 1;
            if (Settings.DefaultStorageOrder == StorageOrders.ColumnMajor) {
                for (i = 0; i < ndims; i++) {
                    outBSD[3 + ndims + i] = (outBSD[3 + i] == 1 ? 0 : stride);
                    stride *= outBSD[3 + i];
                }
            } else {
                System.Diagnostics.Debug.Assert(Settings.DefaultStorageOrder == StorageOrders.RowMajor);
                for (i = 0; i < ndims; i++) {
                    outBSD[2 + 2 * ndims - i] = (outBSD[2 + ndims - i] == 1 ? 0 : stride);
                    stride *= outBSD[2 + ndims - i];
                }
            }
            System.Diagnostics.Debug.Assert(stride == nelem);
            #endregion

            #region prepare BSD for WriteTo
            long* copyBSD = stackalloc long[(int)ndims * 2 + 3];
            var myBSD = storage.Size.GetBSD(false);

            copyBSD[0] = myBSD[0];
            copyBSD[1] = myBSD[1];
            copyBSD[2] = 0;
            uint myDims = (uint)myBSD[0];
            for (i = 0; i < myDims; i++) {
                copyBSD[3 + i] = myBSD[3 + i];
                copyBSD[3 + myDims + i] = outBSD[3 + ndims + i];
            }
            #endregion
            Core.Functions.Builtin.WriteToOperators.WriteTo_BSD<T>(storage.m_handles[0], storage.Size, newCA[0], copyBSD, BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT, inExpand: true);

            ret.Size.SetAll(outBSD);
            
            // storage.Assign(ret, toOutT: true, fromRetT: true);
            return ret; 
        }

        #endregion
    }
}
