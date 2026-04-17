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
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="LocalT2"></typeparam>
    /// <typeparam name="InT2"></typeparam>
    /// <typeparam name="OutT2"></typeparam>
    /// <typeparam name="RetT2"></typeparam>
    /// <typeparam name="StorageT2"></typeparam>
    
    internal abstract unsafe class UnaryBaseOutOfPlace<T, LocalT, InT, OutT, RetT, StorageT,
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
        uint m_sizeOfT = Storage<T>.SizeOfT;
        uint m_sizeOfT2 = Storage<T2>.SizeOfT;
        #endregion

        /// <summary>
        /// General operator over elements of <paramref name="A"/>, potentially inplace.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <returns>New array with the same size as <paramref name="A"/> and with the result of the unary operation performed on all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The storage order of the array returned depends on <see cref="Settings.DefaultStorageOrder"/> (default: <see cref="StorageOrders.ColumnMajor"/>).</para>
        /// </remarks>
        
        internal unsafe RetT2 operate(ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A) { // ho: removed, because not used?: , StorageOrders? forcedOutOrder = null
            if (object.Equals(A, null)) {
                return null;
            }
            
            // block A's storage from disposing (on other threads), release RetT A eventually. 
            using var _1 = ReaderLock.Create(A, out var storage); 
            
            // don't release A! it might be a RetT!
            if (storage.S.NumberOfElements == 0) {
                // no inplace! Don't make a clone of A either, since there are no elements to copy and
                // we would just produce another reference to its buffer set. 
                var handles = CountableArray.Create();
                handles[0] = MathInternal.New<T2>(0, 0); 
                return BaseStorage<T2, LocalT2, InT2, OutT2, RetT2, StorageT2>.Create(handles, storage.S).RetArray; 
            }
            // we need some computations
            var outStorageOrder = storage.S.IsContinuous ? storage.S.StorageOrder : Settings.DefaultStorageOrder;
            StorageT2 ret = BaseStorage<T2, LocalT2, InT2, OutT2, RetT2, StorageT2>.Create(storage.S, outStorageOrder);

            Strided64(storage, ret);

            return ret.RetArray;
        }

        #region abstract interface to be defined by (derived) unary inner function classes
        public abstract void Strided64(StorageT A, StorageT2 Out);

        #endregion

    }
}

