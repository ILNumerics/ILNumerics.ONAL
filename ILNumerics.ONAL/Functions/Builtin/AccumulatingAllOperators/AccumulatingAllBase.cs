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
    /// Unary operator template for unary operations, no inplace. Output type may be different from the input type! (Anyall, Allall, Sumall)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="LocalT"></typeparam>
    /// <typeparam name="InT"></typeparam>
    /// <typeparam name="OutT"></typeparam>
    /// <typeparam name="RetT"></typeparam>
    /// <typeparam name="StorageT"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="LocalT2"></typeparam>
    /// <typeparam name="InT2"></typeparam>
    /// <typeparam name="OutT2"></typeparam>
    /// <typeparam name="RetT2"></typeparam>
    /// <typeparam name="StorageT2"></typeparam>
    internal abstract unsafe class AccumulatingAllBase<T, LocalT, InT, OutT, RetT, StorageT,
                                                      T2, LocalT2, InT2, OutT2, RetT2, StorageT2>

            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where StorageT2 : BaseStorage<T2, LocalT2, InT2, OutT2, RetT2, StorageT2>, new()
            where LocalT2 : Mutable<T2, LocalT2, InT2, OutT2, RetT2, StorageT2>
            where InT2 : Immutable<T2, LocalT2, InT2, OutT2, RetT2, StorageT2>
            where OutT2 : Mutable<T2, LocalT2, InT2, OutT2, RetT2, StorageT2>
            where RetT2 : Mutable<T2, LocalT2, InT2, OutT2, RetT2, StorageT2> {

        #region attributes
        #endregion

        /// <summary>
        /// General operator over elements of <paramref name="A"/>, not inplace. Potentially other output type.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <returns>New scalar array.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The storage order of the array returned depends on the order of <paramref name="A"/>. If 
        /// <paramref name="A"/>.<see cref="Size.IsContinuous"/> is <c>true</c> the array returned will have the same <see cref="Size.StorageOrder"/> 
        /// as <paramref name="A"/>. Otherwise, the elements of the returned array will be reordered in the order defined by 
        /// <see cref="Settings.DefaultStorageOrder"/> (default: <see cref="StorageOrders.ColumnMajor"/>).</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered. However, if <paramref name="A"/>'s storage is suitable and not 
        /// shared by other arrays its memory will be used for the result and the operation is performed 'inplace'.</para></remarks>
        
        internal unsafe RetT2 operate(ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A) {
            if (object.Equals(A, null)) {
                return null;
            }
            using var _1 = ReaderLock.Create(A, out var storage);

            var numel = storage.S.NumberOfElements;

            var ret = BaseStorage<T2, LocalT2, InT2, OutT2, RetT2, StorageT2>.Create();
            ret.S.SetScalar(0,Settings.MinNumberOfArrayDimensions);
            ret.Handles[0] = MathInternal.New<T2>(1);

            if (numel < 1) {
                // empty 
                if (Settings.MinNumberOfArrayDimensions > 0) {
                    ret.S.SetAll(1);
                } else {
                    ret.S.SetScalar(0, 0);
                }
                ret.SetValueSeq(default(T2), 0);
                (ret as LogicalStorage)?.SetNumberTrues(-1); 
                return ret.RetArray; 
            }
            Strided(storage, ret);
            (ret as LogicalStorage)?.SetNumberTrues(-1);
            return ret.RetArray;
        }

        #region abstract interface to be defined by (derived) unary inner function classes

        /// <summary>
        /// Performs strided accumulation over a threaded chunk. 
        /// </summary>
        /// <param name="v1">Input storage</param>
        /// <param name="v2">Output storage, scalar.</param>
        internal abstract void Strided(StorageT v1, StorageT2 v2);



        #endregion

    }
}

