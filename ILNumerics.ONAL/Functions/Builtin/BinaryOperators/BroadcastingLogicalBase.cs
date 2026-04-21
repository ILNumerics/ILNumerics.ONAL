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

    internal abstract unsafe partial class BroadcastingLogicalBase<T, LocalT,InT, OutT, RetT,StorageT>

        where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {


        internal abstract void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len,
                long* dims_strides, long* numberTrues);

        
        internal Logical operate(ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A,
                              ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> B) // where T : struct <- not working with operators!
{
            using var _1 = ReaderLock.Create(A, out var storageA, throwOnNullWithMsg: "In a binary operation op(A,B) the input parameter A may not be null.");
            using var _2 = ReaderLock.Create(B, out var storageB, throwOnNullWithMsg: "In a binary operation op(A,B) the input parameter B may not be null.");

            var buffer = Storage<T>.Context.TmpBuffer1000;
            // buffer layout for iteration data: [OUT BSD][STRIDES A][STRIDES B][NUM Trues * workitem][parameter][locParam...* workitem]
            uint ndims;
            // determine out size
            Helper.BC_broadcastOutDims(storageA.Size, storageB.Size, buffer, out ndims);

            #region prepare out array, no test for inplace (because in type != out type) 
            LogicalStorage ret = LogicalStorage.Create();
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<byte>((ulong)buffer[1], false);
            // if the larger of A and B is continuous take its storage order. Otherwise take default setting.
            ret.S.SetDimensionLengths(buffer, Settings.DefaultStorageOrder);
            ret.NumberTrues = 0;
            if (buffer[1] == 0) {
                // special case: empty array
                return ret.RetArray;
            }
            #endregion
            // prepare iteration sizes & strides
            Helper.BC_prepareIterationDims<T, byte>(storageA.Size, storageB.Size, buffer, ret.S.StorageOrder == StorageOrders.RowMajor);

            byte* pOut = (byte*)ret.Handles[0].Pointer + ret.Size.BaseOffset;
            byte* pA = (byte*)storageA.Handles[0].Pointer + storageA.Size.BaseOffset * Storage<T>.SizeOfT;
            byte* pB = (byte*)storageB.Handles[0].Pointer + storageB.Size.BaseOffset * Storage<T>.SizeOfT;

            Strided64(pOut, pA, pB, 0, buffer[1], buffer, buffer + buffer[0] * 4 + 3);

            // accumulate number trues from work chunks
            ret.NumberTrues = *(buffer + buffer[0] * 4 + 3);

            return ret.RetArray;
        }
    }
}