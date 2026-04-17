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
using System.Diagnostics;
using System.Security;
using System.Threading;

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
    public abstract unsafe partial class BroadcastingBinaryBase<T, LocalT,InT, OutT, RetT,StorageT>
        where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> { 

        // TODO: add declaration for continous worker functions. 
        /// <summary>
        /// abstract declaration for strided worker functions
        /// </summary>
        /// <param name="pOut"></param>
        /// <param name="pA"></param>
        /// <param name="pB"></param>
        /// <param name="start"></param>
        /// <param name="len"></param>
        /// <param name="dims_strides"></param>
        protected internal abstract void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len,
                long* dims_strides);

        
        public virtual RetT operate(ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A, 
                              ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> B) // where T : struct <- not working with operators!
        {
            return operateInternal(A, B); 
        }

        
        public RetT operateInternal(ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A,
                              ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> B) // where T : struct <- not working with operators!
        { 
            using var _1 = ReaderLock.Create(A, out var storageA, throwOnNullWithMsg: "In a binary operation op(A,B) the input parameter A may not be null.");
            using var _2 = ReaderLock.Create(B, out var storageB, throwOnNullWithMsg: "In a binary operation op(A,B) the input parameter B may not be null.");

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

                byte* pOut = (byte*)ret.Handles[0].Pointer + ret.Size.BaseOffset * Storage<T>.SizeOfT;
                byte* pA = (byte*)storageA.Handles[0].Pointer + storageA.Size.BaseOffset * Storage<T>.SizeOfT;
                byte* pB = (byte*)storageB.Handles[0].Pointer + storageB.Size.BaseOffset * Storage<T>.SizeOfT;

                Strided64(pOut, pA, pB, 0, buffer[1], buffer);

                return ret.RetArray;
        }
    }

}