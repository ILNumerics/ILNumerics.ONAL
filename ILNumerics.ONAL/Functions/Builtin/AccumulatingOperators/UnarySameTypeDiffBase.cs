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
    internal abstract unsafe class UnarySameTypeDiffBase<T, LocalT, InT, OutT, RetT, StorageT>

            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

        #region named constants
        public readonly uint m_sizeOfT = Storage<T>.SizeOfT;
        #endregion

        /// <summary>
        /// Reduction operator over elements of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="N">Order of differences to compute along dimension <paramref name="dim"/>.</param>
        /// <param name="dim">The dimension to operate along. This is expected to be a valid dimension index in range 0...A.S.NumberoOfDimensions - 1.</param>
        /// <returns>New array with the same size as <paramref name="A"/> except dimension <paramref name="dim"/> which is reduced to max(0,A.S[dim]-N).</returns>
        /// <remarks><para>The storage order of the array returned depends on the order of <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para>On <paramref name="N"/> = 0 the array returned equals A.</para></remarks>
        
        internal unsafe RetT operate(ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A, uint N, int dim) {
            if (object.Equals(A, null)) {
                return null;
            }
            if (dim < 0) {
                dim = (int)A.Storage.S.WorkingDimension(); // always dim >= 0
            }
            if (dim >= A.Storage.S.NumberOfDimensions) {
                throw new ArgumentException($"Parameter dim must be in range [0...{A.Storage.S.NumberOfDimensions - 1}] (a valid dimension index).");
            }
            if (N < 0) {

                throw new ArgumentException($"N must be positive or 0.");

            } else if (N == 0) {

                var retA = A.Storage.Clone() as StorageT; 
                (A as RetT)?.Release();
                return retA.RetArray;

            } else if (N > 1) {
               // this performs N copy+operates. TODO: make a shortcut for N >= A.S[dim]!
                return operate(operate(A,1,dim), N - 1, dim); 
            }
            // N is 1 from here on...

            System.Diagnostics.Debug.Assert(dim >= 0);
            System.Diagnostics.Debug.Assert(N == 1);
            var storage = A.Storage;
            StorageT ret = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Create();

            var ctx = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context;
            long* buffer = ctx.TmpBuffer1000;
            long* inBSD = (long*)0, outBSD = (long*)0;
            long outLen = prepareBSDs(storage, ret, &buffer, ref inBSD, ref outBSD, (uint)dim);
            ret.Handles = CountableArray.Create();
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<T>((ulong)outLen, false);

            if (outLen == 0) {
                (A as RetT)?.Release();
                return ret.RetArray;
            }
            var ndims = (uint)inBSD[0];

            // for chunk distribution: the working dim must always be handled completely by a single thread. THerefore, its elements are not considered for 
            // work distribution later. 
            outLen /= (outBSD[3] + 1); 

            Strided64(
                (byte*)storage.Handles[0].Pointer, (byte*)ret.Handles[0].Pointer,
                0, outLen,
                inBSD, outBSD);

            (A as RetT)?.Release();
            return ret.RetArray;
        }

        /// <summary>
        /// Prepares the BSDs of the output array and the working bsds for iteration. Working dim outStorageBSD: same as input. in working bsds: FIRST dimension.
        /// </summary>
        /// <param name="inStorage">Input storage.</param>
        /// <param name="outStorage">Output storage.</param>
        /// <param name="buffer">temp buffer with long elements for both temp. working BSDs.</param>
        /// <param name="inBSD">[Out] the working bsd for the input array.</param>
        /// <param name="outBSD">[Out] the working bsd for the output array.</param>
        /// <param name="dim">Index of the accumulating dimension.</param>
        /// <returns>Number of elements required for the output array.</returns>
        private long prepareBSDs(StorageT inStorage, StorageT outStorage, long** buffer, ref long* inBSD, ref long* outBSD, uint dim) {
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
            outStorageBSD[3 + dim] = determineWorkDimLength(inStorageBSD[3 + dim]);
            // ... moved to START in working bsds
            inBSD[3] = inStorageBSD[3 + dim] - 1;
            inBSD[3 + ndims] = inStorageBSD[3 + ndims + dim] * m_sizeOfT;

            uint workBSDpos = 4;
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
            outBSD[3] = outStorageBSD[3 + dim] - 1;
            outBSD[3 + ndims] = outStorageBSD[3 + ndims + dim] * m_sizeOfT;
            outStorageBSD[0] = ndims;
            outStorageBSD[1] = ret;
            outStorageBSD[2] = 0;
            outBSD[0] = ndims;
            outBSD[1] = ret;
            outBSD[2] = 0;
            inBSD[0] = ndims;
            inBSD[1] = inStorageBSD[1];
            inBSD[2] = inStorageBSD[2] * m_sizeOfT;
            return ret;
        }
        /// <summary>
        /// Working length of diff() result.
        /// </summary>
        /// <param name="v">length of input dimension[dim]</param>
        /// <returns><paramref name="v"/>Math.Min(v-1, 0)</returns>
        protected virtual long determineWorkDimLength(long v) {
            return Math.Min(v-1, 0); 
        }

        #region abstract interface to be defined by (derived) unary inner functions

        public abstract void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut);

        #endregion

    }
}

