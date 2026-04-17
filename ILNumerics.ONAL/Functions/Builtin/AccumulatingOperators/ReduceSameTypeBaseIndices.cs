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
    internal unsafe abstract class ReduceSameTypeBaseIndices<T, LocalT, InT, OutT, RetT, StorageT> : ReduceSameTypeBase<T, LocalT, InT, OutT, RetT, StorageT>

            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> 

        {

        /// <summary>
        /// Reduction operator over elements of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="I">Output: indices of found values.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <param name="dim">The dimension to operate along. This is expected to be a valid dimension index.</param>
        /// <returns>New array with the same size as <paramref name="A"/> except the dimension <paramref name="dim"/> which is reduced to min(1,A.S[dim]).</returns>
        /// <remarks><para>The storage order of the array returned depends on the order of <paramref name="A"/>.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe RetT operate(ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A, OutArray<long> I, int dim, bool keepdim = true) {

            if (object.Equals(A, null)) {
                return null;
            }
            if (object.Equals(I, null)) {
                // no indices required
                return base.operate(A, dim, keepdim); 
            }
            // indices aware version
            using var _1 = ReaderLock.Create(A, out var storage, releaseRetT: true);

            if (dim < 0) {
                dim = (int)storage.Size.WorkingDimension(); // always dim >= 0
            }
            System.Diagnostics.Debug.Assert(dim >= 0); 
            // scalar special case / virtual dimensions case
            if (dim >= storage.Size.NumberOfDimensions || (storage.Size[dim] <= 1 && keepdim)) { // includes scalars!
                if (!object.Equals(I,null)) {
                    I.a = MathInternal.array<long>(0, storage.Size); 
                }
                return (storage.Clone() as StorageT).m_retArray;
            }

            StorageT ret = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Create();

            var ctx = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context;
            long* buffer = ctx.TmpBuffer1000;
            long* inBSD = (long*)0, outBSD = (long*)0, outIdxBSD = (long*)0;
            long outLen = prepareBSDs(storage, ret, &buffer, ref inBSD, ref outBSD, ref outIdxBSD, (uint)dim, keepdim);
            ret.Handles[0] = ret.New((ulong)outLen, false);

            if (I.Storage.Size.NumberOfElements < outLen /* v7: removed: || I.ReferenceCount > 1 */ || I.Storage.Handles.ReferenceCount > 1) {
                var iStor = Storage<long>.Create();
                //iStor.m_handles.Retain();
                iStor.Handles[0] = iStor.New((ulong)outLen, false); // DeviceManagement.DeviceManager.GetDevice(0).New<long>((ulong)outLen, false);
                iStor.S.SetAll(ret.S); // no base offset in ret, so this is fine!
                I.Storage.Assign(iStor, toOutT: true, fromRetT: true);
                iStor = I.Storage;  
            } else {
                I.Storage.Size.SetAll(ret.S); 
            }

            if (outLen == 0) {
                return ret.RetArray;
            }
            var ndims = (uint)inBSD[0];

            Strided64Indices(
                (byte*)storage.Handles[0].Pointer, (byte*)ret.Handles[0].Pointer, (byte*)I.Storage.Handles[0].Pointer,
                0, outLen, inBSD, outBSD);

            return ret.RetArray;
        }

        /// <summary>
        /// Prepares the BSDs of the output array and the working bsds for iteration. See Strided64() for the format specification.
        /// </summary>
        /// <param name="inStorage">Input storage.</param>
        /// <param name="outStorage">Output storage.</param>
        /// <param name="keepdim">Whether the new, reduced, singleton dimension is to be kept from the output. False: remove.</param>
        /// <param name="buffer">temp buffer with long elements for both temp. working BSDs.</param>
        /// <param name="inBSD">[Out] the working bsd for the input array.</param>
        /// <param name="outBSD">[Out] the working bsd for the output array.</param>
        /// <param name="outIdxBSD">[Out] the working bsd for the index output array (strides for 'long' elements).</param>
        /// <param name="dim">Index of the accumulating dimension.</param>
        /// <returns>Number of elements required for the output array.</returns>
        private long prepareBSDs(StorageT inStorage, StorageT outStorage, long** buffer, ref long* inBSD, 
                                ref long* outBSD, ref long* outIdxBSD, uint dim, bool keepdim) {
            long ret = 1;
            // prepare out BSD
            var outStorageBSD = outStorage.S.GetBSD(true);
            var inStorageBSD = inStorage.Size.GetBSD(false);
            inBSD = *buffer;
            uint ndims = inStorage.Size.NumberOfDimensions; 
            if (dim >= ndims) {
                throw new ArgumentException($"The index of the reduction dimension must lay inside the range 0 <= S.NumberOfDimensions. Found: {dim}."); 
            }
            outBSD = inBSD + 2 * ndims + 3;
            outIdxBSD = outBSD + 2 * ndims + 3; 
            *buffer += 6 * ndims + 9; 
            // TODO: outIdxBSD is completely defined here. But we only use the strides later. One might merge 
            // all working BSD into a single structure and remove redundant data for performance (of scalar-like data).  
            if (inStorage.Size.StorageOrder == StorageOrders.ColumnMajor) {
                uint workBSDpos = 3;
                for (int i = 0; i < ndims; i++) {
                    if (i == dim) {
                        outStorageBSD[3 + i] = Math.Min(1, inStorageBSD[3 + i]);
                    } else {
                        outStorageBSD[3 + i] = inStorageBSD[3 + i];
                    }
                    outStorageBSD[3 + ndims + i] = outStorageBSD[3 + i] == 1 ? 0 : ret; // supports broadcasting! 
                    ret *= outStorageBSD[3 + i];
                    // bsds for iteration: we copy all 'real' BSDs but sort the axis working dim to the end
                    if (i == dim) {
                        inBSD[2 + ndims] = inStorageBSD[3 + i] - 1;
                        inBSD[2 + 2 * ndims] = inStorageBSD[3 + ndims + i] * m_sizeOfT;
                        outBSD[2 + ndims] = outStorageBSD[3 + i] - 1;
                        outBSD[2 + 2 * ndims] = outStorageBSD[3 + ndims + i] * m_sizeOfT;
                        outIdxBSD[2 + ndims] = outStorageBSD[3 + i] - 1;
                        outIdxBSD[2 + 2 * ndims] = outStorageBSD[3 + ndims + i] * sizeof(long);
                    } else {
                        inBSD[workBSDpos] = inStorageBSD[3 + i] - 1;
                        inBSD[workBSDpos + ndims] = inStorageBSD[3 + ndims + i] * m_sizeOfT;
                        outBSD[workBSDpos] = outStorageBSD[3 + i] - 1;
                        outBSD[workBSDpos + ndims] = outStorageBSD[3 + ndims + i] * m_sizeOfT;
                        outIdxBSD[workBSDpos] = outStorageBSD[3 + i] - 1;
                        outIdxBSD[workBSDpos + ndims] = outStorageBSD[3 + ndims + i] * sizeof(long);
                        workBSDpos++;
                    }
                }
            } else {
                // row major and other storage orders
                uint workBSDpos = 3;
                for (int i = 0; i < ndims; i++) {
                    if (dim == ndims - 1 - i) {
                        outStorageBSD[2 + ndims - i] = Math.Min(1, inStorageBSD[2 + ndims - i]);
                    } else {
                        outStorageBSD[2 + ndims - i] = inStorageBSD[2 + ndims - i];
                    }
                    outStorageBSD[2 + 2 * ndims - i] = outStorageBSD[2 + ndims - i] == 1 ? 0 : ret;   // supports broadcasting! 
                    ret *= outStorageBSD[2 + ndims - i];
                    // bsds for iteration: we copy all 'real' BSDs but sort the axis working dim to the end
                    if (dim == ndims - 1 - i) {
                        inBSD[2 + ndims] = inStorageBSD[2 + ndims - i] - 1;
                        inBSD[2 + 2 * ndims] = inStorageBSD[2 + 2 * ndims - i] * m_sizeOfT;
                        outBSD[2 + ndims] = outStorageBSD[2 + ndims - i] - 1;
                        outBSD[2 + 2 * ndims] = outStorageBSD[2 + 2 * ndims - i] * m_sizeOfT;
                        outIdxBSD[2 + ndims] = outStorageBSD[2 + ndims - i] - 1;
                        outIdxBSD[2 + 2 * ndims] = outStorageBSD[2 + 2 * ndims - i] * sizeof(long);
                    } else {
                        inBSD[workBSDpos] = inStorageBSD[2 + ndims - i] - 1;
                        inBSD[workBSDpos + ndims] = inStorageBSD[2 + 2 * ndims - i] * m_sizeOfT;
                        outBSD[workBSDpos] = outStorageBSD[2 + ndims - i] - 1;
                        outBSD[workBSDpos + ndims] = outStorageBSD[2 + 2 * ndims - i] * m_sizeOfT;
                        outIdxBSD[workBSDpos] = outStorageBSD[2 + ndims - i] - 1;
                        outIdxBSD[workBSDpos + ndims] = outStorageBSD[2 + 2 * ndims - i] * sizeof(long);
                        workBSDpos++;
                    }
                }
            }
            outStorageBSD[0] = ndims;
            outStorageBSD[1] = ret;
            outStorageBSD[2] = 0;
            outBSD[0] = ndims;
            outBSD[1] = ret;
            outBSD[2] = 0;
            outIdxBSD[0] = ndims;
            outIdxBSD[1] = ret;
            outIdxBSD[2] = 0;
            inBSD[0] = ndims;
            inBSD[1] = inStorageBSD[1];
            inBSD[2] = inStorageBSD[2] * m_sizeOfT;
            if (!keepdim) {
                outStorage.S.RemoveDimension(dim); 
            }
#if DEBUG
            Size.CheckSizeBroadcastableStrides(outStorage.S);
#endif

            return ret; 
        }

        #region abstract interface to be defined by (derived) unary inner functions
        public abstract void Strided64Indices(byte* pSrc, byte* pDest, byte* pIndices, long start, long len, long* bsdIn, long* bsdOut);
        #endregion

    }

}

