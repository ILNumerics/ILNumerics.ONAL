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
using ILNumerics.Core.Global;
using ILNumerics.Core.StorageLayer;
using System;
using System.Security;

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
    internal abstract unsafe class UnaryInplaceAxisBase<T, LocalT, InT, OutT, RetT, StorageT>

            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

        #region named constants
        public readonly uint m_sizeOfT = Storage<T>.SizeOfT;
        #endregion

        /// <summary>
        /// Inplace unary along working axis operator over elements of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">The dimension to operate along. This is expected to be a valid dimension index.</param>
        /// <returns>New array with the same size as <paramref name="A"/>.</returns>
        /// <remarks><para>The storage order of the array returned depends on the order of <paramref name="A"/>.</para>
        /// </remarks>
        
        internal unsafe void operate(ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A, int dim) {
            if (object.Equals(A, null)) {
                return;
            }
            using var _1 = ReaderLock.Create(A, out var storage);

            if (dim < 0) {
                dim = (int)storage.S.WorkingDimension(); // always dim >= 0
            }
            System.Diagnostics.Debug.Assert(dim >= 0);

            // scalar special case / virtual dimensions case
            if (storage.S[dim] <= 1) { // includes scalars & dim >= storage.S.NumberOfDimensions!
                return;
            }

            var ctx = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context;

            long* buffer = ctx.TmpBuffer1000;
            long* iterationBSD = (long*)0;
            long outLen = prepareBSDs(storage, &buffer, ref iterationBSD, (uint)dim);

            if (outLen == 0) {
                return; 
            }
            var ndims = (uint)iterationBSD[0];

            // for chunk distribution: the working dim must always be handled completely by a single thread. THerefore, its elements are not considered for 
            // work distribution later. 
            outLen /= (iterationBSD[3] + 1); 

            Strided64(
                (byte*)storage.Handles[0].Pointer, 
                0, outLen, iterationBSD);

        }

        /// <summary>
        /// Prepares the working bsd for iteration. Working dim in working bsd: FIRST dimension.
        /// </summary>
        /// <param name="inStorage">Input storage.</param>
        /// <param name="buffer">temp buffer with long elements for working BSD.</param>
        /// <param name="bsd">[Out] the working bsd for the in+output array. dims:-1, strides in bytes, working dim at #0.</param>
        /// <param name="dim">Index of the working dimension.</param>
        /// <returns>Number of elements except in working dimension.</returns>
        private long prepareBSDs(StorageT inStorage, long** buffer, ref long* bsd, uint dim) {
            long ret = 1;
            // prepare out BSD 
            var inStorageBSD = inStorage.S.GetBSD(false);
            bsd = *buffer;
            uint ndims = inStorage.S.NumberOfDimensions;
            if (dim >= ndims) {
                throw new ArgumentException($"The index of the reduction dimension must lay inside the range 0 <= S.NumberOfDimensions. Found: {dim}.");
            }
            // ... moved to START in working bsds
            bsd[3] = inStorageBSD[3 + dim] - 1;
            bsd[3 + ndims] = inStorageBSD[3 + ndims + dim] * m_sizeOfT;

            uint workBSDpos = 4;
            if (inStorage.S.StorageOrder == StorageOrders.ColumnMajor) {

                // column major
                for (int i = 0; i < ndims; i++) {

                    if (i != dim) {
                        // bsds for iteration: we copy all 'real' BSDs but sort the axis working dim to the start
                        bsd[workBSDpos] = inStorageBSD[3 + i] - 1;
                        bsd[workBSDpos + ndims] = inStorageBSD[3 + ndims + i] * m_sizeOfT;
                        workBSDpos++;
                    }
                    ret *= inStorageBSD[3 + i];
                }
            } else {

                // row major and other storage orders
                for (int i = 0; i < ndims; i++) {
                    if (dim != ndims - 1 - i) {
                        // bsds for iteration: we copy all 'real' BSDs but sort the axis working dim to the end
                        bsd[workBSDpos] = inStorageBSD[2 + ndims - i] - 1;
                        bsd[workBSDpos + ndims] = inStorageBSD[2 + 2 * ndims - i] * m_sizeOfT;
                        workBSDpos++;
                    }
                    ret *= inStorageBSD[2 + ndims - i];
                }
            }
            bsd[0] = ndims;
            bsd[1] = inStorageBSD[1];
            bsd[2] = inStorageBSD[2] * m_sizeOfT;
            *buffer += 3 + ndims * 2; 
            return ret;
        }
        /// <summary>
        /// Prototyp implementation (sort)
        /// </summary>
        /// <param name="v">length of input dimension[dim]</param>
        /// <returns><paramref name="v"/>.</returns>
        protected virtual long determineWorkDimLength(long v) {
            return v; 
        }

        #region abstract interface to be defined by (derived) unary inner functions
        public abstract void Strided64(byte* pSrc, long start, long len, long* iterationBSD);
        #endregion

    }
}

