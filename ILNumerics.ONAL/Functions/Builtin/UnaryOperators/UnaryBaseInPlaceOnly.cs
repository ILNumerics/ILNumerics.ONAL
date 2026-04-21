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
using ILNumerics.Core.Functions.Builtin.InnerLoops;
using ILNumerics.Core.Global;
using ILNumerics.Core.StorageLayer;
using System;
using System.Security;
using System.Threading;

namespace ILNumerics.Core.Functions.Builtin {

    /// <summary>
    /// Unary operator template for unary operations, always inplace.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="LocalT"></typeparam>
    /// <typeparam name="InT"></typeparam>
    /// <typeparam name="OutT"></typeparam>
    /// <typeparam name="RetT"></typeparam>
    /// <typeparam name="StorageT"></typeparam>
    internal abstract unsafe class UnaryBaseInPlaceOnly<T, LocalT, InT, OutT, RetT, StorageT> 

            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

        #region attributes
        uint m_sizeOfT = Storage<T>.SizeOfT;
        #endregion

        /// <summary>
        /// General operator over elements of <paramref name="A"/>, potentially inplace.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <returns>New array with the same size as <paramref name="A"/> and with the result of the unary operation performed on all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The storage order of the array returned depends on the order of <paramref name="A"/>. If 
        /// <paramref name="A"/>.<see cref="Size.IsContinuous"/> is <c>true</c> the array returned will have the same <see cref="Size.StorageOrder"/> 
        /// as <paramref name="A"/>. Otherwise, the elements of the returned array will be reordered in the order defined by 
        /// <see cref="Settings.DefaultStorageOrder"/> (default: <see cref="StorageOrders.ColumnMajor"/>).</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered. However, if <paramref name="A"/>'s storage is suitable and not 
        /// shared by other arrays its memory will be used for the result and the operation is performed 'inplace'.</para></remarks>
        
        internal unsafe void operate(Mutable<T, LocalT, InT, OutT, RetT, StorageT> A) {
            if (object.Equals(A, null)) {
                return;
            }
 // wait until pending writes are completed
            lock (A.SynchObj) {
                var storage = A.Storage;
                if (storage.S.NumberOfElements == 0) {
                    return;
                }
                // detach against pending reads
                var newStorage = storage as StorageT;
                
                if (!Helper.canBeUsedForInplaceOp(newStorage)) {
                    newStorage.DetachBufferSetInplace();
                }
                
                // we need some computations. But we don't change size nor striding. Partial element modifications are allowed to be exposed to other threads.
                var outLen = newStorage.S.NumberOfElements;
                byte* pIn = (byte*)newStorage.Handles[0].Pointer + newStorage.S.BaseOffset * m_sizeOfT;
                
                // strided inplace
                var bsd = newStorage.S.GetBSD(write: false);
                var ndims = (uint)bsd[0];

                // strides are ulong* 
                long* ordered_bsd = stackalloc long[(int)ndims * 2 + 3];
                Helper.PrepareBSD(newStorage.S.StorageOrder, bsd, ordered_bsd, m_sizeOfT, -1);
                Strided64Inplace(pIn,
                    0, outLen,
                    ordered_bsd);

                if (!object.ReferenceEquals(newStorage, storage)) {
                    A.Storage.Assign(newStorage, true, false, false);
                }

            }
        }

        #region abstract interface to be defined by (derived) unary inner functions

        public abstract void Strided64Inplace(byte* v1, long start, long len, long* bsd);

        #endregion

    }
}

