//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////

using ILNumerics.Core.Arrays;
using ILNumerics.Core.Global;
using ILNumerics.Core.MemoryLayer;
using ILNumerics.Core.StorageLayer;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using static ILNumerics.Core.StorageLayer.Iterators;

namespace ILNumerics.Core.Functions.Builtin {

    /// <summary>
    /// Base class for binary operator classes, handling two inputs and the same output type, potentially inplace: +,-,/,*,%
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="LocalT"></typeparam>
    /// <typeparam name="InT"></typeparam>
    /// <typeparam name="OutT"></typeparam>
    /// <typeparam name="RetT"></typeparam>
    /// <typeparam name="StorageT"></typeparam>
    /// <typeparam name="DelegateT">Delegate type for the inner element function. Can be generic.</typeparam>
    internal abstract unsafe partial class BroadcastingBinaryGenericBase<T, LocalT,InT, OutT, RetT,StorageT, 
        DelegateT>
        where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> { 
                
        internal abstract void Strided64(byte* A, byte* B, byte* C, long start, long len, long* dims_strides, DelegateT function);
        internal abstract void Strided64(T[] C, T[] A, T[] B, long offsA, long offsB, long offsC, long start, long len, long* dims_strides, DelegateT function);
        
        internal RetT operate(ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A,
                              ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> B,
                              DelegateT func) {
            if (object.Equals(A, null) || object.Equals(B, null)) {
                throw new ArgumentNullException("A,B", "In a binary operation the input parameters may not be null.");
            }
            // The following was an attempt to optimize continous arrays (both sides) with many singleton dimensions. A 
            // better (and more general) approach would be to implement a specialization for continous arrays, like 'Strided64' : 'Continous64'. 
            // However, as an intermediate solution Helper.BC_prepareOterationDims<T, T> was optimized, turning the arrays into vectors.
            // The following had a similar effect and is not needed anymore ...
            //if (A.Storage.S.IsContinuous && B.Storage.S.StorageOrder == A.Storage.S.StorageOrder && A.Storage.S.IsSameShape(B.Storage.S) && A.Storage.S[0] != A.Storage.S.NumberOfElements) {
            //    var order = A.Storage.S.StorageOrder; 
            //    return operate(A.Storage.Reshape(-1, order), B.Reshape(-1, order), func).Reshape(A.S.ToIntArray(), order); 
            //}

            using var _1 = ReaderLock.Create(A, out var storageA, throwOnNullWithMsg: "In a binary operation op(A,B) the input parameter A may not be null.");
            using var _2 = ReaderLock.Create(B, out var storageB, throwOnNullWithMsg: "In a binary operation op(A,B) the input parameter B may not be null.");

            // Do not release A or B! Both may be RetTs!

            var buffer = Storage<T>.Context.TmpBuffer1000;
            uint ndims;
            // determine out size
            Helper.BC_broadcastOutDims(storageA.Size, storageB.Size, buffer, out ndims);

            #region prepare out array + test for inplace 
            StorageT ret;
            ret = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Create();
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<T>((ulong)buffer[1], false);
            // if the larger of A and B is continuous take its storage order. Otherwise take default setting.
            var larger = storageA.S.NumberOfElements > storageB.S.NumberOfElements ? storageA : storageB;
            if (larger.S.IsContinuous) {
                ret.S.SetDimensionLengths(buffer, larger.S.StorageOrder);
            } else {
                ret.S.SetDimensionLengths(buffer, Settings.DefaultStorageOrder);
            }

            if (buffer[1] == 0) {
                // special case: empty array
                return ret.RetArray;
            }

            #endregion
            // prepare iteration sizes & strides
            Helper.BC_prepareIterationDims<T, T>(storageA.Size, storageB.Size, buffer, ret.S.StorageOrder == StorageOrders.RowMajor);

            if (typeof(T).IsValueType) {

                byte* pOut = (byte*)ret.Handles[0].Pointer + ret.Size.BaseOffset * Storage<T>.SizeOfT;
                byte* pA = (byte*)storageA.Handles[0].Pointer + storageA.Size.BaseOffset * Storage<T>.SizeOfT;
                byte* pB = (byte*)storageB.Handles[0].Pointer + storageB.Size.BaseOffset * Storage<T>.SizeOfT;

                Strided64(pOut, pA, pB, 0, buffer[1], buffer, func);

            } else {
                // T is managed type
                // this runs w/o parallelization, currently. If this is needed, consider using Threading.ThreadPool
                // in order to simplify the marshalling of T[] and the function prototype.
                var pOut = (ret.Handles[0] as ManagedHostHandle<T>).HostArray;
                var pA = (storageA.Handles[0] as ManagedHostHandle<T>).HostArray;
                var pB = (storageB.Handles[0] as ManagedHostHandle<T>).HostArray;

                Strided64(pOut, pA, pB, ret.Size.BaseOffset, storageA.Size.BaseOffset,
                          storageB.Size.BaseOffset, 0, buffer[1], buffer, func);
            }

            return ret.RetArray;
        }
    }
}