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
//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////
using ILNumerics.Core.Arrays;
using ILNumerics.Core.Functions.Builtin.InnerLoops;
using ILNumerics.Core.Global;
using ILNumerics.Core.Internal;
using ILNumerics.Core.StorageLayer;
using System;
using System.Security;
using System.Threading;

namespace ILNumerics.Core.Functions.Builtin {

    /// <summary>
    /// Accumulating operator template for reduction operations. No index retrieval. Potentially varying output types.
    /// </summary>
    /// <typeparam name="T">Element type.</typeparam>
    /// <typeparam name="LocalT"></typeparam>
    /// <typeparam name="InT"></typeparam>
    /// <typeparam name="OutT"></typeparam>
    /// <typeparam name="RetT"></typeparam>
    /// <typeparam name="StorageT"></typeparam>
    internal abstract unsafe class ReduceSameTypeBase<T, LocalT, InT, OutT, RetT, StorageT> 

            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

        #region named constants
        public const int THREAD_OVERHEAD = 0;
        public readonly uint m_sizeOfT = Storage<T>.SizeOfT;
        #endregion

        /// <summary>
        /// Reduction operator over elements of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <param name="dim">The dimension to operate along. This is expected to be a valid dimension index.</param>
        /// <returns>New array with the same size as <paramref name="A"/> except dimension <paramref name="dim"/> which is reduced to min(1,A.S[dim]).</returns>
        /// <remarks><para>The storage order of the array returned depends on the order of <paramref name="A"/>.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe RetT operate(ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A, int dim, bool keepdim = true) {
            
            if (object.Equals(A, null)) {
                return null;
            }
            
            using var _1 = ReaderLock.Create(A, out var storage);
            
            if (dim < 0) {
                dim = (int)A.Storage.S.WorkingDimension(); // always dim >= 0
            }
            System.Diagnostics.Debug.Assert(dim >= 0); 

            // scalar special case / virtual dimensions case
            if (dim >= storage.S.NumberOfDimensions || (storage.S[dim] <= 1 && keepdim)) { // includes scalars!
                return (storage.Clone() as StorageT).m_retArray;
            }

            StorageT ret = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Create();

            var ctx = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context;
            long* buffer = ctx.TmpBuffer1000;
            long* inBSD = (long*)0, outBSD = (long*)0;
            long outLen = prepareBSDs(storage, ret, &buffer, ref inBSD, ref outBSD, (uint)dim, keepdim);
            ret.Handles = CountableArray.Create(); 
            ret.Handles[0] = ret.New((ulong)outLen, false); 

            if (outLen == 0) {
                return ret.RetArray;
            }
            var ndims = (uint)inBSD[0];

            Strided64(
                (byte*)storage.Handles[0].Pointer, (byte*)ret.Handles[0].Pointer,
                0, outLen,
                inBSD, outBSD);

            return ret.RetArray;
        }

        /// <summary>
        /// Prepares the BSDs of the output array and the working bsds for iteration. Working dim: outStorageBSD: min(1,A.S[dim]), in working BSD moved to END.
        /// </summary>
        /// <param name="inStorage">Input storage.</param>
        /// <param name="outStorage">Output storage.</param>
        /// <param name="keepdim">Whether the new, reduced, singleton dimension is to be kept from the output. False: remove.</param>
        /// <param name="buffer">temp buffer with long elements for both temp. working BSDs.</param>
        /// <param name="inBSD">[Out] the working bsd for the input array.</param>
        /// <param name="outBSD">[Out] the working bsd for the output array.</param>
        /// <param name="dim">Index of the accumulating dimension.</param>
        /// <returns>Number of elements required for the output array.</returns>
        private long prepareBSDs(StorageT inStorage, StorageT outStorage, long** buffer, ref long* inBSD, ref long* outBSD, uint dim, bool keepdim) {
            long ret = 1;
            // prepare out BSD
            var outStorageBSD = outStorage.S.GetBSD(true);
            var inStorageBSD = inStorage.S.GetBSD(false);
            inBSD = *buffer;
            uint ndims = inStorage.S.NumberOfDimensions; 
            if (dim >= ndims) {
                throw new ArgumentException($"The index of the reduction dimension must lay inside the range 0 <= S.NumberOfDimensions. Found: {dim}."); 
            }
            outBSD = inBSD + 2 * ndims + 3;
            *buffer += 4 * ndims + 6;
            // working dim length
            outStorageBSD[3 + dim] = Math.Min(1, inStorageBSD[3 + dim]);
            // ... moved to END in working bsds
            inBSD[2 + ndims] = inStorageBSD[3 + dim] - 1;
            inBSD[2 + 2 * ndims] = inStorageBSD[3 + ndims + dim] * m_sizeOfT;
            outBSD[2 + ndims] = outStorageBSD[3 + dim] - 1;
            outBSD[2 + 2 * ndims] = outStorageBSD[3 + ndims + dim] * m_sizeOfT;

            uint workBSDpos = 3;
            if (inStorage.S.StorageOrder == StorageOrders.ColumnMajor) {

                // column major
                for (int i = 0; i < ndims; i++) {

                    if (i != dim) {
                        outStorageBSD[3 + i] = inStorageBSD[3 + i];
                        // bsds for iteration: we copy all 'real' BSDs but sort the axis working dim to the end
                        inBSD[workBSDpos] = inStorageBSD[3 + i] - 1;
                        inBSD[workBSDpos + ndims] = inStorageBSD[3 + ndims + i] * m_sizeOfT;
                        outBSD[workBSDpos] = outStorageBSD[3 + i] - 1;
                        outBSD[workBSDpos + ndims] = ret * m_sizeOfT;
                        workBSDpos++;
                    }
                    outStorageBSD[3 + ndims + i] = outStorageBSD[3 + i] == 1 ? 0 : ret;
                    ret *= outStorageBSD[3 + i];
                }
            } else {

                // row major and other storage orders
                for (int i = 0; i < ndims; i++) {

                    if (dim != ndims - 1 - i) {
                        outStorageBSD[2 + ndims - i] = inStorageBSD[2 + ndims - i];
                        // bsds for iteration: we copy all 'real' BSDs but sort the axis working dim to the end
                        inBSD[workBSDpos] = inStorageBSD[2 + ndims - i] - 1;
                        inBSD[workBSDpos + ndims] = inStorageBSD[2 + 2 * ndims - i] * m_sizeOfT;
                        outBSD[workBSDpos] = outStorageBSD[2 + ndims - i] - 1;
                        outBSD[workBSDpos + ndims] = ret * m_sizeOfT;
                        workBSDpos++;
                    }
                    outStorageBSD[2 + 2 * ndims - i] = outStorageBSD[2 + ndims - i] == 1 ? 0 : ret;
                    ret *= outStorageBSD[2 + ndims - i];
                }
            }
            outStorageBSD[0] = ndims;
            outStorageBSD[1] = ret;
            outStorageBSD[2] = 0;
            outBSD[0] = ndims;
            outBSD[1] = ret;
            outBSD[2] = 0;
            inBSD[0] = ndims;
            inBSD[1] = inStorageBSD[1];
            inBSD[2] = inStorageBSD[2] * m_sizeOfT;
            if (!keepdim) {
                outStorage.S.RemoveDimension(dim); 
            }
            return ret; 
        }

        #region abstract interface to be defined by (derived) unary inner functions

        public abstract void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut);

        #endregion

    }
}

