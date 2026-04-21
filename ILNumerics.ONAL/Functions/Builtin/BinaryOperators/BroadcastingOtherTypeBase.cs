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
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="LocalT2"></typeparam>
    /// <typeparam name="InT2"></typeparam>
    /// <typeparam name="OutT2"></typeparam>
    /// <typeparam name="RetT2"></typeparam>
    /// <typeparam name="StorageT2"></typeparam>
    public abstract unsafe partial class BroadcastingOtherTypeBase<T, LocalT, InT, OutT, RetT, StorageT, T2, LocalT2, InT2, OutT2, RetT2, StorageT2>

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

        
        public RetT2 operate(ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A,
                              ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> B) // where T : struct <- not working with operators!
{

            // Do not release A or B! Both may be RetTs!
            using var _1 = ReaderLock.Create(A, out var storageA, throwOnNullWithMsg: "In a binary operation op(A,B) the input parameter A may not be null.");
            using var _2 = ReaderLock.Create(B, out var storageB, throwOnNullWithMsg: "In a binary operation op(A,B) the input parameter B may not be null.");

            var buffer = Storage<T>.Context.TmpBuffer1000;
            uint ndims;
            // determine out size
            Helper.BC_broadcastOutDims(storageA.Size, storageB.Size, buffer, out ndims);

            #region prepare out array, no test for inplace (because in type != out type)
            StorageT2 ret = BaseStorage<T2, LocalT2, InT2, OutT2, RetT2, StorageT2>.Create();
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<T2>((ulong)buffer[1], false);
            // if the larger of A and B is continuous take its storage order. Otherwise take default setting.
            var larger = storageA.S.NumberOfElements > storageB.S.NumberOfElements ? storageA : storageB;
            if (larger.S.IsContinuous) {
                ret.S.SetDimensionLengths(buffer, larger.S.StorageOrder);
            } else {
                ret.S.SetDimensionLengths(buffer, Settings.DefaultStorageOrder);
            }

                (ret as LogicalStorage)?.SetNumberTrues(-1);  // no special support here! Consider using a specialized base function returning logicals and counting number trues already, if returning logicals fast is needed.
            if (buffer[1] == 0) {
                // special case: empty array
                return ret.RetArray;
            }
            #endregion
            // prepare iteration sizes & strides
            Helper.BC_prepareIterationDims<T, T2>(storageA.Size, storageB.Size, buffer, ret.S.StorageOrder == StorageOrders.RowMajor);

            byte* pOut = (byte*)ret.Handles[0].Pointer + ret.Size.BaseOffset * Storage<T2>.SizeOfT;
            byte* pA = (byte*)storageA.Handles[0].Pointer + storageA.Size.BaseOffset * Storage<T>.SizeOfT;
            byte* pB = (byte*)storageB.Handles[0].Pointer + storageB.Size.BaseOffset * Storage<T>.SizeOfT;

            Strided64(pOut, pA, pB, 0, buffer[1], buffer);

            return ret.RetArray;
        }
        protected internal abstract void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len, long* dims_strides);

    }
}