//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////
using ILNumerics.Core.Arrays;
using ILNumerics.Core.DeviceManagement;
using ILNumerics.Core.Functions.Builtin.InnerLoops;
using ILNumerics.Core.Global;
using ILNumerics.Core.Internal;
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;

namespace ILNumerics.Core.Functions.Builtin {

    /// <summary>
    /// Unary operator template for unary operations, potentially inplace. Output type is the same as the input type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="LocalT"></typeparam>
    /// <typeparam name="InT"></typeparam>
    /// <typeparam name="OutT"></typeparam>
    /// <typeparam name="RetT"></typeparam>
    /// <typeparam name="StorageT"></typeparam>
    
    public abstract unsafe class UnaryBaseInPlace<T, LocalT, InT, OutT, RetT, StorageT>

            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

#region attributes
        protected uint m_sizeOfT = Storage<T>.SizeOfT;
#endregion

        /// <summary>
        /// General operator over elements of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <returns>New array with the same size as <paramref name="A"/> and with the result of the unary operation performed on all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The storage order of the array returned depends on the current setting of <see cref="Settings.ArrayStyle"/> and <see cref="Settings.DefaultStorageOrder"/> 
        /// (default: <see cref="StorageOrders.ColumnMajor"/>).</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        public virtual RetT operate(ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A) {
            return operateInternal(A);
        }
        
        protected unsafe RetT operateInternal(ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            // don't release A! it might be a RetT!
            using var _1 = ReaderLock.Create(A, out var storage);

            if (storage.S.NumberOfElements == 0) {
                return BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Create(storage).RetArray;
            }
            // we need some computations
            var outStorageOrder = storage.S.IsContinuous ? storage.S.StorageOrder : Settings.DefaultStorageOrder;
            StorageT ret = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Create(storage.S, outStorageOrder);

            Strided64(storage, ret);
            return ret.RetArray;
        }

#region abstract interface to be defined by (derived) unary inner functions

        public abstract void Strided64(StorageT A, StorageT Out);

#endregion

    }
}


