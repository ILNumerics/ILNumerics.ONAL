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
using ILNumerics.Core.Arrays;
using ILNumerics.Core.Global;
using ILNumerics.Core.StorageLayer;
using System.Security;

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
    /// <typeparam name="Param1T"></typeparam>
    /// <typeparam name="Param2T"></typeparam>
    /// <typeparam name="Param3T"></typeparam>
    public abstract unsafe class UnaryBaseInPlaceParam3<T, LocalT, InT, OutT, RetT, StorageT, Param1T, Param2T, Param3T>

            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

#region attributes
        static readonly uint m_sizeOfT = Storage<T>.SizeOfT;
        static readonly uint m_sizeOfParam1T = Storage<Param1T>.SizeOfT;
        static readonly uint m_sizeOfParam2T = Storage<Param2T>.SizeOfT;
        static readonly uint m_sizeOfParam3T = Storage<Param3T>.SizeOfT;
#endregion

        /// <summary>
        /// General operator over elements of <paramref name="A"/>, potentially inplace.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="p1">Parameter 1, scalar numeric primitive type.</param>
        /// <param name="p2">Parameter 2, scalar numeric primitive type.</param>
        /// <param name="p3">Parameter 3, scalar numeric primitive type.</param>
        /// <returns>New array with the same size as <paramref name="A"/> and with the result of the unary operation performed on all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The storage order of the array returned depends on the order of <paramref name="A"/>. If 
        /// <paramref name="A"/>.<see cref="Size.IsContinuous"/> is <c>true</c> the array returned will have the same <see cref="Size.StorageOrder"/> 
        /// as <paramref name="A"/>. Otherwise, the elements of the returned array will be reordered in the order defined by 
        /// <see cref="Settings.DefaultStorageOrder"/> (default: <see cref="StorageOrders.ColumnMajor"/>).</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered. However, if <paramref name="A"/>'s storage is suitable and not 
        /// shared by other arrays its memory will be used for the result and the operation is performed 'inplace'.</para></remarks>
        
        public virtual unsafe RetT operate(ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A, Param1T p1, Param2T p2, Param3T p3) {
            return operateInternal(A, p1, p2, p3);
        }
        /// <summary>
        /// General operator over elements of <paramref name="A"/>, potentially inplace.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="p1">Parameter 1, scalar numeric primitive type.</param>
        /// <param name="p2">Parameter 2, scalar numeric primitive type.</param>
        /// <param name="p3">Parameter 3, scalar numeric primitive type.</param>
        /// <returns>New array with the same size as <paramref name="A"/> and with the result of the unary operation performed on all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The storage order of the array returned depends on the order of <paramref name="A"/>. If 
        /// <paramref name="A"/>.<see cref="Size.IsContinuous"/> is <c>true</c> the array returned will have the same <see cref="Size.StorageOrder"/> 
        /// as <paramref name="A"/>. Otherwise, the elements of the returned array will be reordered in the order defined by 
        /// <see cref="Settings.DefaultStorageOrder"/> (default: <see cref="StorageOrders.ColumnMajor"/>).</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered. However, if <paramref name="A"/>'s storage is suitable and not 
        /// shared by other arrays its memory will be used for the result and the operation is performed 'inplace'.</para></remarks>
        
        public virtual unsafe RetT operateInternal(ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A, Param1T p1, Param2T p2, Param3T p3) {
            if (object.Equals(A, null)) {
                return null;
            }
            // don't release A! it might be a RetT!
            using var _1 = ReaderLock.Create(A, out var storage);

            if (storage.S.NumberOfElements == 0) {
                return BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Create(CountableArray.Create(), storage.S).RetArray;
            }
            // we need some computations
            byte* pIn = (byte*)storage.Handles[0].Pointer + storage.S.BaseOffset * m_sizeOfT;

            StorageT ret = null;

            var outLen = storage.S.NumberOfElements;

            // strided. Output is always continous. No inplace option.
            var outStorageOrder = Settings.DefaultStorageOrder;
            ret = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Create(storage.S, outStorageOrder, 0);
            byte* pOut = (byte*)ret.Handles[0].Pointer;
            var bsd = storage.S.GetBSD(write: false);
            var ndims = (uint)bsd[0];

            // strides are ulong* 
            long* ordered_bsd = stackalloc long[(int)ndims * 2 + 3];
            Helper.PrepareBSD(outStorageOrder, bsd, ordered_bsd, m_sizeOfT);

            Strided64(
                pIn, pOut,
                0, outLen,
                ordered_bsd,
                p1, p2, p3);

            return ret.RetArray;
        }

#region abstract interface to be defined by (derived) unary inner functions

        public abstract void Strided64(byte* v1, byte* v2, long start, long len, long* bsd, Param1T p1, Param2T p2, Param3T p3);

#endregion

    }
}


