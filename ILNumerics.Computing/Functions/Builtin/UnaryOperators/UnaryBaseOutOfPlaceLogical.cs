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
    /// Unary operator template for unary operations, no inplace. Output type is different from the input type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="LocalT"></typeparam>
    /// <typeparam name="InT"></typeparam>
    /// <typeparam name="OutT"></typeparam>
    /// <typeparam name="RetT"></typeparam>
    /// <typeparam name="StorageT"></typeparam>
    internal abstract unsafe class UnaryBaseOutOfPlaceLogical<T, LocalT, InT, OutT, RetT, StorageT> 

            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> { 

        #region attributes
        uint m_sizeOfT = Storage<T>.SizeOfT;
        #endregion

        /// <summary>
        /// General operator over elements of <paramref name="A"/>, out of-place.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="forcedOutOrder"></param>
        /// <returns>New array with the same size as <paramref name="A"/> and with the result of the unary operation performed on all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The storage order of the array returned depends on the order of <paramref name="A"/>. If 
        /// <paramref name="A"/>.<see cref="Size.IsContinuous"/> is <c>true</c> the array returned will have the same <see cref="Size.StorageOrder"/> 
        /// as <paramref name="A"/>. Otherwise, the elements of the returned array will be reordered in the order defined by 
        /// <see cref="Settings.DefaultStorageOrder"/> (default: <see cref="StorageOrders.ColumnMajor"/>).</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered. However, if <paramref name="A"/>'s storage is suitable and not 
        /// shared by other arrays its memory will be used for the result and the operation is performed 'inplace'.</para></remarks>

        internal unsafe Logical operate(ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A, StorageOrders? forcedOutOrder = null) {
            if (object.Equals(A, null)) {
                return null;
            }
            // don't release A! it might be a RetT!
            using var _1 = ReaderLock.Create(A, out var storage);

            if (storage.S.NumberOfElements == 0) {
                // no inplace! 
                var tmp = LogicalStorage.Create(storage.S, Settings.DefaultStorageOrder, baseOffset: 0);
                tmp.S.SetAll(storage.S);
                return tmp.RetArray;
            }
            // we need some computations
            byte* pIn = (byte*)storage.Handles[0].Pointer + storage.S.BaseOffset * m_sizeOfT;

            LogicalStorage ret = null;

            var outLen = storage.S.NumberOfElements;

            long* buffer = Storage<T>.Context.TmpBuffer1000;

                #region strided
                // strided. Output is always continous. No inplace option.
                var outStorageOrder = Settings.DefaultStorageOrder;
                ret = LogicalStorage.Create(storage.S, outStorageOrder, 0);
                byte* pOut = (byte*)ret.Handles[0].Pointer;
                var bsd = storage.S.GetBSD(write: false);
                var ndims = (uint)bsd[0];

                // strides are ulong* 
                long* ordered_bsd = buffer; 
                buffer += ndims * 2 + 3; 

                Helper.PrepareBSD(outStorageOrder, bsd, ordered_bsd, m_sizeOfT);

                Strided64(
                    pIn, pOut,
                    0, outLen,
                    ordered_bsd,
                    buffer);

                #endregion

            //InnerLoops.Sumall.Int64.Instance.Continous((byte*)buffer, (byte*)buffer, workItemCount + 1); 
            ret.SetNumberTrues(buffer[0]);

            return ret.RetArray;
        }

        #region abstract interface to be defined by (derived) unary inner function classes
        public abstract void Strided64(byte* v1, byte* v2, long start, long len, long* bsd, long* nrTrues);

        #endregion

    }
}

