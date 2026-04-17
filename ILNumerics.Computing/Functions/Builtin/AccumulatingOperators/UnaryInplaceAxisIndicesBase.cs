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
    internal abstract unsafe class UnaryInplaceAxisIndicesBase<T, LocalT, InT, OutT, RetT, StorageT>

            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

        #region named constants
        public readonly uint m_sizeOfT = Storage<T>.SizeOfT;
        #endregion

        /// <summary>
        /// Inplace unary along working axis operator over elements of <paramref name="A"/>, maintaining indices.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="indices">[Output] Storage of indices when returning.</param>
        /// <param name="dim">The dimension to operate along. This is expected to be a valid dimension index or 0.</param>
        /// <returns>New array with the same size as <paramref name="A"/>.</returns>
        /// <remarks><para>The storage order of the array returned depends on the order of <paramref name="A"/>.</para>
        /// <para>No new array is created unless required. Elements of <paramref name="A"/> and <paramref name="indices"/> are altered inplace, if possible.</para></remarks>
        
        internal unsafe void operate(Mutable<T, LocalT, InT, OutT, RetT, StorageT> A, 
                                     Storage<long> indices, int dim) {
            
            // Note on thread safety: this base func is ONLY used by sort(), currently. A is kept alive in caller. 
            // Further, the caller has not yet exposed A to the public. Hence, we need no ReaderLock here. 
            // Caution! When this function is used on publicly exposed types A we must add a proper Readerlock here! 
            
            if (object.Equals(A, null)) {
                return;
            }
            using var _1 = ReaderLock.Create(A, out var storage, releaseRetT: true);

            if (dim < 0) {
                dim = (int)storage.S.WorkingDimension(); // always dim >= 0
            }
            // since the iteration strides of the working dim are shared (here) between A and indices
            // we must require A to have continous storage order. 
            if (!storage.S.IsContinuous) {
                var indStorOrder = indices.S.IsContinuous ? indices.S.StorageOrder : Settings.DefaultStorageOrder;
                _1.Update(ref storage, storage.EnsureStorageOrder(indStorOrder, inplace: false)); 
            }
            if (Equals(indices,null) || !indices.S.IsSameShape(storage.S)) {
                indices.Assign(createIndices4Sorting(storage, (uint)dim, storage.S.StorageOrder).Storage);
            } 
            if (indices.S.StorageOrder != storage.S.StorageOrder) {
                indices.EnsureStorageOrder(storage.S.StorageOrder, inplace: true);
            }
            System.Diagnostics.Debug.Assert(!Equals(indices, null)); 
            System.Diagnostics.Debug.Assert(storage.S.IsContinuous); 
            System.Diagnostics.Debug.Assert(indices.S.IsSameSize(storage.S)); //mixes array styles: indices may has singletons padded!
            System.Diagnostics.Debug.Assert(indices.S.IsContinuous);
            System.Diagnostics.Debug.Assert(indices.S.StorageOrder == storage.S.StorageOrder);
            System.Diagnostics.Debug.Assert(dim >= 0);

            // scalar special case / virtual dimensions case
            if (storage.S[dim] <= 1) { // includes scalars & dim >= storage.S.NumberOfDimensions!
                return;
            }

            var ctx = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context;

            long* buffer = ctx.TmpBuffer1000;
            long* iterationBSD = (long*)0;
            long outLen = prepareBSDs(storage, indices, &buffer, ref iterationBSD, (uint)dim);

            if (outLen == 0) {
                return; 
            }
            var ndims = (uint)iterationBSD[0];

            // for chunk distribution: the working dim must always be handled completely by a single thread. THerefore, its elements are not considered for 
            // work distribution later. 
            outLen /= (iterationBSD[3] + 1); 

            Strided64(
                (byte*)storage.Handles[0].Pointer,
                (byte*)indices.Handles[0].Pointer + indices.S.BaseOffset * sizeof(long),
                0, outLen, iterationBSD);
        }

        private static Array<long> createIndices4Sorting(StorageT A, uint dim, StorageOrders order) {
            using (Scope.Enter()) {

                // special case: numpy scalar
                if (A.S.NumberOfDimensions == 0) {
                    return 0; 
                }

                Array<long> outer = A.RetArray.shape;
                outer[dim] = 1;
                outer.a = MathInternal.array<long>(0, outer, order: order);

                Array<long> inner = MathInternal.ones<long>(A.S.NumberOfDimensions, 1, order: order);
                inner[dim] = A.S[dim];
                inner.a = MathInternal.counter<long>(0, 1, inner, order: order); 

                Array<long> ret = inner + outer;
                ret.Storage.EnsureStorageOrder(order);

                return ret; 
            }
        }

        /// <summary>
        /// Prepares the working bsd for iteration. Working dim in working bsd: FIRST dimension.
        /// </summary>
        /// <param name="inStorage">Input storage.</param>
        /// <param name="indices">Storage for the indices, same shape as <paramref name="inStorage"/>, may has other strides though!</param>
        /// <param name="buffer">temp buffer with long elements for working BSD.</param>
        /// <param name="bsd">[Out] the working bsd for the in+output array. dims:-1, strides in bytes, working dim at #0.</param>
        /// <param name="dim">Index of the working dimension.</param>
        /// <returns>Number of elements except in working dimension.</returns>
        private long prepareBSDs(StorageT inStorage, Storage<long> indices, long** buffer, ref long* bsd, uint dim) {
            long ret = 1;
            // prepare out BSD 
            var inStorageBSD = inStorage.S.GetBSD(false);
            var indicesBSD = indices.S.GetBSD(false); 
            bsd = *buffer;
            uint ndims = inStorage.S.NumberOfDimensions;
            if (dim >= ndims) {
                throw new ArgumentException($"The index of the reduction dimension must lay inside the range 0 <= ... <= S.NumberOfDimensions. Found: {dim}.");
            }
            // ... moved to START in working bsds
            bsd[3] = inStorageBSD[3 + dim] - 1;
            bsd[3 + ndims] = inStorageBSD[3 + ndims + dim] * m_sizeOfT;
            bsd[3 + ndims + ndims] = indicesBSD[3 + ndims + dim] * sizeof(long); 

            uint workBSDpos = 4;
            if (inStorage.S.StorageOrder == StorageOrders.ColumnMajor) {

                // column major
                for (int i = 0; i < ndims; i++) {

                    if (i != dim) {
                        // bsds for iteration: we copy all 'real' BSDs but sort the axis working dim to the start
                        bsd[workBSDpos] = inStorageBSD[3 + i] - 1;
                        bsd[workBSDpos + ndims] = inStorageBSD[3 + ndims + i] * m_sizeOfT;
                        bsd[workBSDpos + ndims + ndims] = indicesBSD[3 + ndims + i] * sizeof(long);
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
                        bsd[workBSDpos + ndims + ndims] = indicesBSD[2 + 2 * ndims - i] * sizeof(long);
                        workBSDpos++;
                    }
                    ret *= inStorageBSD[2 + ndims - i];
                }
            }
            bsd[0] = ndims;
            bsd[1] = inStorageBSD[1];
            bsd[2] = inStorageBSD[2] * m_sizeOfT;
            *buffer += 3 + ndims * 3; //iterBSD + indices strides
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

        public abstract void Strided64(byte* pSrc, byte* pInd, long start, long len, long* iterationBSD);

        #endregion

    }
}

