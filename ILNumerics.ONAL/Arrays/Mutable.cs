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
using ILNumerics.Core.Global;
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.Arrays {

    /// <summary>
    /// Base class for all mutable array types. Internal use.
    /// </summary>
    /// <typeparam name="T1">The element type.</typeparam>
    /// <typeparam name="LocalT">The local array type of this array type set. </typeparam>
    /// <typeparam name="InT">The input array type of this array type set.</typeparam>
    /// <typeparam name="OutT">The output array type of this array type set.</typeparam>
    /// <typeparam name="RetT">The return array type of this array type set.</typeparam>
    /// <typeparam name="StorageT">The storage type of this array type set.</typeparam>
    public abstract partial class Mutable<T1, LocalT, InT, OutT, RetT, StorageT>   // T1 must not read "T" to prevent from conflicts with the .T (transpose) property.
        : ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT>
        where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {

        object m_synchObj; 
        /// <summary>
        /// Common synch object for securing mutating access. Shared between LocalT and OutT. 
        /// </summary>
        internal Object SynchObj => m_synchObj;

        internal Mutable(StorageT storage, object synch) : base(storage) {
            m_synchObj = synch;
        }

        #region element access / IO
        /// <summary>
        /// [Expert API - UNSAFE!] Acquire a writable pointer to the memory location storing the value of the first element of this array. 
        /// </summary>
        /// <returns>Pointer to the first element of this array.</returns>
        /// <remarks>This function returns a pointer to the first element stored in this array. Any 
        /// base offset configured for the array is taken into account. The pointer points to the 
        /// memory used by this array directly - not to a copy of this memory!
        /// <para>For empty arrays (<see cref="BaseArray.IsEmpty"/> is <c>true</c>) the value of the pointer returned is undefined.</para>
        /// <para>This pointer is valid immediately, after the function returns. It remains valid until the next use of this array or 
        /// until the end of the current scope block. Do not 
        /// attempt to use this pointer after the array has been used, released, ran out of scope, was reassigned or is modified!</para>
        /// <para>Keep in mind that any <i>use</i> of an array - would it be for reading, for writing, when using the array as parameter in 
        /// array function calls, etc. - potentially and transparently alters the internal array structures. Hence, using the array 
        /// in any of such ways may invalidates the pointer acquired. Using such invalidated pointer results in undefined behavior, including 
        /// writing to invalid memory and/or crashing the application. </para>
        /// <para>In a multithreading setup and when the array hosting the memory to write to is shared with other threads manual 
        /// synchronization (f.e.: locking) is required. It is recommended to use a clone of the arrays before providing it to another thread. </para>
        /// <para>The order of elements in this array is determined by the size descriptor <see cref="Size"/>. 
        /// Use the strides, dimension lengths and the size of the <typeparamref name="T1"/> elements 
        /// <see cref="ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}.SizeOfT"/>
        /// in order to compute the byte offset to individual elements relative to this pointer.</para>
        /// <para>The memory region addressed by the pointer returned exists on the <b>unmanaged</b> heap for numeric element types. 
        /// Hence, it does not need to be pinned and will not be moved by the GC. However, this memory 
        /// is subject of deterministic disposal, pooling and frequent reuse by other arrays. Do not use 
        /// the pointer returned after this array left the current function scope, was modified, reassigned or released!</para>
        /// <para>Since ILNumerics' memory management does transparently perform 'lazy copies on write' 
        /// calling this function may cause the underlying memory to be copied - transparently as well. Therefore, 
        /// do not call this function unless you really want to change the memory addressed. For reading 
        /// purposes it is cheaper to acquire a pointer via: 
        /// <see cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>.
        /// Latter option is available not only for <see cref="Array{T}"/> (as <see cref="GetHostPointerForWrite(StorageOrders?)"/>), 
        /// but on any non-volatile array, including input and output arrays.</para>
        /// <para><see cref="GetHostPointerForWrite(StorageOrders?)"/> works for element types <typeparamref name="T1"/> of <see cref="ValueType"/> (structs in C#) 
        /// only. The behavior for reference types ('class' elements) is undefined.</para>
        /// </remarks>
        /// <example><code><![CDATA[
        /// using (Scope.Enter()) {    
        ///        // create a local array: 
        ///        Array<double> A = new double[,] {
        ///            { 1, 2, 3 },
        ///            { 4, 5, 6 }
        ///        };
        ///        // or use any other way of creating the array: 
        ///        // Array<double> A = ILMath.zeros<double>(100,200); 
        ///        // or from another array: 
        ///        // Array<double> A = otherArray.C;  
        ///
        ///        // get a pointer to the first element
        ///        unsafe
        ///        {
        ///            double* pA = (double*)A.GetHostPointerForWrite();
        ///            // the pointer can be used for reading AND writing
        ///            pA[0] = -99;
        ///    
        ///            // A
        ///            // [2 x 3] <double>
        ///            // -99   2   3
        ///            //   4   5   6
        ///    
        ///            // Watch the storage order 'A.S.StorageOrder'! To find specific elements: 
        ///            double el12 = pA[A.S.GetSeqIndex(1, 2)];
        ///            // el12: 6
        ///            Assert.IsTrue(el12 == 6);
        ///            Assert.IsTrue(A.GetValue(0) == -99);
        ///        }
        ///        // work with A commonly...
        /// }
        /// // don't use the pointer outside this scope block!
        /// ]]></code></example>
        /// <exception cref="InvalidOperationException"> if called on a return type array or if the elements are not of a <see cref="ValueType"/>.</exception>
        /// <seealso cref="ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}.GetArrayForRead"/>
        /// <seealso cref="ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead"/>
        /// <seealso cref="Size.BaseOffset"/>
        
        public IntPtr GetHostPointerForWrite(StorageOrders? order = null) {
            using var _1 = ReaderLock.Create(this, out StorageT storage, releaseRetT: false); // this func is defined on mutable arrays only
            //if (storage.ReferenceCount > 1) {
            //    throw new InvalidOperationException("The memory for this array is currently shared by more than a single local array (views). It cannot get modified. Please create a local array first and acquire a write pointer from the new array."); 
            //}
            // Instead of detaching the multiple layers of this storage individually (creating multiple (pot. asynch) clones) we test
            // for the need for detaching once and create a detached clone. Make sure to wait for pending storage, if required. (This can 
            // be improved later on, by making it cope with pending storages. But we'd need a more general "asynchronization" strategy first).

            if (storage.ReferenceCount > 2 || storage.GetAsynchReferencesCount() > (storage.IsReady ? 1 : 2) || storage.Handles.ReferenceCount > 1
                || order != null && order != storage.S.StorageOrder) {

                if (order != null && order != storage.S.StorageOrder) {
                    var newStorage = storage.EnsureStorageOrder(order.GetValueOrDefault(), forceCopy: true, inplace: false);
                    // EnsureStorageOrder performs Assign() - no need to do it here again.
                    if (!ReferenceEquals(newStorage, storage)) {
                        _1.Update(ref storage, newStorage);
                    }

                } else {

                    var detached = storage.GetDetached();
                    var newStorage = storage.Assign(detached as StorageT, true, true, true);
                    if (!ReferenceEquals(newStorage, storage)) {
                        _1.Update(ref storage, newStorage);
                    }

                }
            }
            //#if DEBUG
            //            if (storage.ReferenceCount > 2) Debugger.Break();
            //            if (!(storage.Handles.ReferenceCount == 1 && storage.GetAsynchReferencesCount() == 1 && storage.ReferenceCount == 2)) {
            //                var msg = $"({System.Threading.Thread.CurrentThread.ManagedThreadId}) GetHostPointerForWrite({storage.ShortInfo(includeIDs: true, includeCounters: true)}): storage.Handles.ReferenceCount: {storage.Handles.ReferenceCount}, storage.GetAsynchReferencesCount(): {storage.GetAsynchReferencesCount()}, storage.ReferenceCount: {storage.ReferenceCount}";
            //                System.Diagnostics.Debug.WriteLine(msg);
            //            }
            //#endif
            return GetHostPointerForRead();
        }

        /// <summary>
        /// Returns a span providing access to this array's internal storage for reading and writing. 
        /// </summary>
        /// <param name="order">[Optional] The order of elements returned. If order is null (default) the current value of <see cref="Settings.DefaultStorageOrder"/> is assumed, which depends on <see cref="Settings.ArrayStyle"/>.</param>
        /// <returns>A span with the exact length needed to hold all elements of this array, referencing the elements of this array, and having the same lifetime as this array.</returns>
        /// <remarks><para>Depending on the current internal storage layout this array is reordered internally as required
        /// before returning the span representing the elements in the requested storage order. </para>
        /// <para>Note, that for returning an array as span it must be ensured that the arrays elements are layed-out continously in memory. 
        /// Thus, the requested storage order is enforced in advance. This may or may not involve an internal copy of the elements of this array and a (transparent to the user) modification in its storage layout.</para>
        /// <para>When called on a mutable array type the span returned can be used to alter elements of this array via the Span&lt;T&gt;.</para>
        /// <para>The Span returned is only valid as long as this array is alive and before a subsequent modification is performed to this array.</para>
        /// </remarks>
        
        public unsafe Span<T1> AsSpan(StorageOrders? order = null) {
            if (order != null && (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor)) {
                throw new ArgumentException($"Invalid value for 'order' argument. To give access to the elements of this array via a Span<T> the storage order must be continous (RowMajor or ColumnMajor). Found: '{order}'.");
            }
            if (S.NumberOfElements > int.MaxValue) {
                throw new ArgumentOutOfRangeException($"The number of elements of this array ({S.NumberOfElements}) exceeds the maximum number of elements a Span<T> can contain ({int.MaxValue}).");
            }
            var p = GetHostPointerForWrite(order ?? Settings.DefaultStorageOrder);  // performs reader lock and EnsureStorageOrder 
            var ret = new Span<T1>((void*)p, (int)S.NumberOfElements);
            return ret;
        }

        /// <summary>
        /// Returns a span providing access to this array's internal storage for reading. 
        /// </summary>
        /// <param name="order">[Optional] The order of elements returned. If order is null (default) the current value of <see cref="Settings.DefaultStorageOrder"/> is assumed, which depends on <see cref="Settings.ArrayStyle"/>.</param>
        /// <returns>A span with the exact length needed to hold all elements of this array, referencing the elements of this array, and having the same lifetime as this array.</returns>
        /// <remarks><para>Depending on the current internal storage layout this array is reordered internally as required
        /// before returning the span representing the elements in the requested storage order. </para>
        /// <para>Note, that for returning an array as span it must be ensured that the arrays elements are layed-out continously in memory. 
        /// Thus, the requested storage order is enforced in advance. This may or may not involve an internal copy of the elements of this array and a (transparent to the user) modification in its storage layout.</para>
        /// <para>The Span returned is only valid as long as this array is alive and before a subsequent modification is performed to this array.</para>
        /// <para>Using a read-only span allows to read from the underlying memory of this array - even while this memory may be shared with other arrays. Thus, 
        /// saving from 'detaching' the array and from a memory copy makes using read-only span preferrable over <see cref="AsSpan(StorageOrders?)"/> when no modifications to element values are required.</para>
        /// </remarks>
        
        public ReadOnlySpan<T1> AsReadOnlySpan(StorageOrders? order = null) {
            return AsReadOnlySpanInternal(order);
        }

        #endregion

    }
}
