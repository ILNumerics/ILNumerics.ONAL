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
using ILNumerics.Core.DeviceManagement;
using ILNumerics.Core.Global;
using ILNumerics.Core.Internal;
using ILNumerics.Core.MemoryLayer;
using ILNumerics.Core.Misc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace ILNumerics.Core.StorageLayer
{

    public abstract class BaseStorage {

        protected static int InstancesCount = 0;

        protected int m_id;
        public int ID => m_id;

        /// <summary>
        /// Internal version of this storage. Each (re-)use of the storage corresponds to a new version number.
        /// </summary>
        public int Version { get; protected set; } = 0;

        public bool IsReady { get; } = true; 

    }


    /// <summary>
    /// Generic base class for all ILNumerics arrays. This class is used internally. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="LocalT"></typeparam>
    /// <typeparam name="InT"></typeparam>
    /// <typeparam name="OutT"></typeparam>
    /// <typeparam name="RetT"></typeparam>
    /// <typeparam name="StorageT"></typeparam>
    public abstract partial class BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> : BaseStorage, IDisposable, IStorage, ICacheable<StorageT>
        where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

        #region attributes

        // the size descriptor, basically a BSD (IntPtr[] with base offset, number of, lengths and strides for the dims). 
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Size m_size;  

        // device storages, reference counted.
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal CountableArray m_handles;

        // how many _array_ interface objects are currently out referencing this storage?
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int m_arrayCounter;

        // how many (asynch) READ ops/segments reference this storage?  -> removed again. Asynch segments always store _shallow clones_ of input (read) parameters.
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int m_asynchReferences;

        // in how many scopes this storage has currently been registered?
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected internal Scope.ScopeInfo m_scopeState;

        // Marks and stores the 'main' thread Id on creation or resurrection. Used for performance shortcuts for thread safety. 
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int m_creatorThreadId;

        //// for efficient caching: turns this object into a linked list item
        //[DebuggerBrowsable(DebuggerBrowsableState.Never)]
        //protected StorageT m_previousStorage = null;

        /// <summary>
        /// Number of bytes used to store a single element of type <typeparamref name="T"/> in memory [readonly].
        /// </summary>
        /// <remarks><para><see cref="SizeOfT"/> corresponds to the <i>unmanged</i> size of a single element of this array. 
        /// This size corresponds to <see cref="Marshal.SizeOf{T}(T)"/>. Its value is cached here for performance reasons.</para>
        /// <para>Note that the size of blittable types is the same on the managed as well as on the unmanaged part. For example, 
        /// every element of type <see cref="System.Int32"/> consumes 4 bytes in the unmanaged heap or on the managed stack. </para>
        /// <para>(At least) one exception exists: <see cref="bool"/> elements in .NET are represented by 4 bytes on the managed 
        /// side. ILNumerics, however, stores them more efficiently as single <see cref="byte"/> in memory.</para></remarks>
        public static readonly uint SizeOfT = ComputeSizeOfT();

        internal static readonly T ElementInstance = default(T);

        // the preallocated concrete array types needed for memory management

        /// <summary>
        /// The local array of this storage.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected internal LocalT m_array;  // this cannot be readonly in order to simplify creation: backreference 'this' is only available after the base constructor (in this class) finished. 
        /// <summary>
        /// The input array of this storage.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected internal InT m_inArray;


        /// <summary>
        /// The return array of this storage.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected internal RetT m_retArray;
        /// <summary>
        /// The output array of this storage. 
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected OutT m_outArray;

        #endregion

        #region properties
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal CountableArray Handles {
            get {
                return m_handles; 
            }
            set {
                if (!ReferenceEquals(m_handles, value)) {

                    m_handles?.Release();
                    m_handles = value;

                }
            }
        }
        
        
        /// <summary>
        /// Gives the local array of this storage. This is used when the storage is referenced by a local array type. 
        /// </summary>
        internal LocalT GetLocalArray(bool retain = true) {
            if (retain) Retain();
            Scope.Context.RegisterArray(m_array);
            return m_array;
        }
        /// <summary>
        /// The input array of this storage. This is used when the storage is referenced to by an input array type. 
        /// </summary>
        internal InT GetInArray(bool retain = true) {
            if (retain) Retain();
            Scope.Context.RegisterArray(m_inArray);
            return m_inArray;
        }
        /// <summary>
        /// The return array of this storage. This is used when the storage is referenced to by a return array type. 
        /// </summary>
        /// <remarks>Be cautious when using this property on existing arrays, which have references, either by local array variables or by other means! 
        /// The return type array may violates the rule for RetT to never be shared with anything. Such return type may only be used on new storages 
        /// which have not yet exposed to user code. Commonly, their reference count is 0. The ref count of the RetT returned is incremented (1).</remarks>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal RetT RetArray {
            get {
                Retain();
                return m_retArray;
            }
        }
        /// <summary>
        /// The output array of this storage. This is used when the storage is referenced to by an output array type. 
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal OutT OutArray {
            get {
                // System.Threading.Interlocked.Increment(ref m_arrayCounter); // never on OutArray!
                return m_outArray;
            }
        }

        /// <summary>
        /// Gives the thread specific context object for caching operations. 
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal static ThreadingContext<T, LocalT, InT, OutT, RetT, StorageT> Context {
            get {
                return ThreadingContext<T, LocalT, InT, OutT, RetT, StorageT>.Context;
            }
        }

        protected StorageT m_previous;
        public ref StorageT Previous => ref m_previous;
        
        //protected int m_deletionMark; 
        //public ref int DeletionMark => ref m_deletionMark; 


        //internal StorageT PreviousStorage {
        //    get { return m_previousStorage; }
        //    set { m_previousStorage = value; }
        //}

        /// <summary>
        /// Gets the state of this storage: 0 - outside any scope, 1 - prepared to enter scope, 2 - waiting for leaving a scope &amp; for Release().
        /// </summary>
        /// <remarks>THIS IS NOT USED IN VERSION 5 SCOPING. IT IS LEFT IN HERE BECAUSE WE NEED IT FOR VERSION 6 SCOPING
        /// ONCE WE HAVE COMPILER SUPPORT.
        /// Edit, v.7: time went by so fast ... We don't use it in version 7 either. It is left as TODO. Scope / lifetime of arrays is a static feature and could be optimized by the compiler...! 
        /// Edit2, v7.0: ok, all back to start. We need the scope flag to support multithreading, but also accelerated code and array renaming. Example:
        ///    Assume a local array in accelerated code /legacy fallback code / completed awaiter (fft) a.s.o. The local is registered in the current scope (see, f.e.: fftforward1D).
        ///    Now, the local is renamed, released and marked for not being released again. This would cache the storage and make it accessible for further arrays. 
        ///    However, the storage remains in the current scope! When the scope is left, it checks for the storage being marked for disposal. With a simple 'int' flag
        ///    we had the chance that the storage was revived in the meantime and the scope flag was back enabled (from another thread or from accelerated code).
        ///    This would dispose the storage - which is now unrelated - and would be a bug (aka as race condition)!
        ///    Below change stores the target scope_info_ as a backreference into the storage. In LeaveScope the array is only disposed by the scope (reference identity) 
        ///    stored into m_scopeState. If both do not match the storage is not disposed. Leaving the scope is possible by clearing the flag. Assigning a new storage 
        ///    via Assign simply exchanges both storages from the ScopeInfo in the individual scope, which remains in place untouched. (No need to find 
        ///    and remove the storage in the collection of registered storages! -> O(1)).
        ///    The only drawback: this does not support multiple threads! Though, it is hard to construct a case where this would be an issue: If a single local 
        ///    array is brougth into a scoped region and accessed by multiple threads ... Note, that the InArray case seems to be not affected, since there are no 
        ///    variables (only function arguments) of InArray type. Such argument is disposed by the caller's scope (where the implicit conversion happens), not by ending the local scope. 
        /// </remarks>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal Scope.ScopeInfo ScopeState { get { return m_scopeState; } }

#endregion

#region constructors

        static BaseStorage() { }

        internal BaseStorage() {
            m_handles = CountableArray.Create();
            m_size = new Size();
            m_id = (int)Interlocked.Increment(ref InstancesCount); 
        } 
#endregion

#region BaseArray implementation

        public Size Size {
            get {
                return m_size;
            }
        }

        public bool IsDisposed {
            get {
                return Handles == null;
            }
        }

        public bool IsCell {
            get {
                return ElementInstance is IStorage; 
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Size S {
            get {
                return Size;
            }
        }

        /// <summary>
        /// Gets the number of arrays published for this storage. 
        /// </summary>
        public int ReferenceCount { get { return m_arrayCounter; } }

        /// <summary>
        /// Gives detailed info about the current configuration of this storage. 
        /// </summary>
        /// <returns>String with description of this storage.</returns>
        public string Info() {
            return ShortInfo(true, true, true, true, true, true, true); 
        }

        /// <summary>
        /// Gives a short description about the type, shape and a few element values of this storage.
        /// </summary>
        /// <returns>String with short description of this storage.</returns>
        public virtual string ShortInfo() {
            return ShortInfo(true, true, true, true, true);
        }
        /// <summary>
        /// Gives a short description. Allows to disable individual info.
        /// </summary>
        /// <returns>String with short description of this storage.</returns>
        
        public string ShortInfo(bool includeType = true, bool includeSize = true, bool includeValues = true, bool includeStorageOrder = true, bool includeDevices = true, bool includeIDs = false, bool includeCounters = false, bool ignoreLocks = false) {
            StringBuilder ret = new StringBuilder();
            string getIDs() {
                var handles = m_handles;
                if (handles == null) {
                    return $"S:{ID},{Version} BS:-- Buf:--";
                } else { //if (handles.CurrentDeviceIdx < 0) {   // disabled for no segment-support in OSS version
                    return $"S:{ID},{Version} BS:{m_handles.ID},{m_handles.Version} Buf:--";
                    //} else {
                    //    return $"S:{ID},{Version} BS:{m_handles.ID},{m_handles.Version} Buf{m_handles.CurrentDeviceIdx}:{m_handles.GetID(m_handles.CurrentDeviceIdx)}";
                }
            }
            if (includeType) {
                Type type = GetType();
                if (ReferenceCount == 0 && GetAsynchReferencesCount() == 0) {
                    if (includeIDs) {
                        return $"(disposed) " + getIDs();
                    }
                    return "(disposed)";
                } else {
                    var generix = type.GetGenericArguments();
                    if (generix != null && generix.Length > 0) {
                        ret.Append("<" + generix[0].Name + "> ");
                        //} else if (this is LogicalStorage) {  // :O -> what the heck!??! bad code smell ...! TODO: make virtual function and move to derived types
                        //    ret.Append("Logical "); 
                    } else {
                        ret.Append(type.Name.Replace("Storage", "") + " "); // handles LogicalStorage + CellStorage :) ... yeah, sure... this is ... well... working :| 
                    }
                }
            }
            if (includeIDs) {
                ret.Append(getIDs() + " ");
            }
            if (includeCounters) {
                ret.Append($"RefCnts: Arr:{m_arrayCounter},asynch:{m_asynchReferences}/BS:{(m_handles?.ReferenceCount.ToString() ?? "-")}/Buf:{(m_handles != null && m_handles.CurrentDeviceIdx >= 0 ? m_handles.GetHandlesArray()[m_handles.CurrentDeviceIdx].m_referenceCount.ToString() : "-")}");
            }
            var handles = GetHandlesUnsafe();
            if (includeSize) {
                ret.Append(Size.ToString());
            }
            if (includeValues) {
                switch (Size.NumberOfElements) {
                    case 0:
                        break;
                    case 1:
                        ret.Append(" " + GetValue(0).ToString());
                        break;
                    case 2:
                        ret.Append($" {GetValue(0)}, {GetValue(-1)}");
                        break;
                    default:
                        ret.Append($" {GetValue(0)}...{GetValue(-1)}");
                        break;
                }
            }
            if (includeStorageOrder) {
                ret.Append(" ");
                if (Size.StorageOrder == StorageOrders.ColumnMajor) {
                    ret.Append("|");
                }
                if (Size.StorageOrder == StorageOrders.RowMajor) {
                    ret.Append("-");
                }
                if (Size.StorageOrder == StorageOrders.Other) {
                    ret.Append("?");
                }
            }
            if (includeDevices) {
                ret.Append(" Dev:" + handles.ShortInfo());
            }

            return ret.ToString();
        }
        
        private string ShortInfoHelper(MemoryHandle h, int i) {
            return object.Equals(h, null) ? "" : $"{i}"; 
        }

        public void ToStream(Stream outStream, string format, ArrayStreamSerializationFlags method) {
            throw new NotImplementedException();
        }
        public int GetAsynchReferencesCount() {
            return m_asynchReferences; 
        }
        public void Retain() {
            System.Diagnostics.Debug.Assert(m_previous == null, "Retaining a cached object.");
            var count = System.Threading.Interlocked.Increment(ref m_arrayCounter);
            if (count == 1) {
                // Either this is the first time we use an array of this fresh storage or we came back from an
                // orphanded storage which lost all _array_ references already. In latter case, the storage must
                // not be disposed yet and hold alive by asynch references! This happens in the context of LRun 
                // fallback functions, when the original storage has been renamed or released from the main
                // thread in the meantime. 
#if DEBUG
                var asynchCount =
#endif
                System.Threading.Interlocked.Increment(ref m_asynchReferences);

#if DEBUG
                // not threadsafe: 
                System.Diagnostics.Debug.Assert(asynchCount >= 1 && m_handles != null /* <- workaround for IsDisposed which may block forever if a pending write exists */, "Coming back from an array-ref free storage is allowed, but only, if there have been asynch refs holding the storage alive!");
#endif 
            }
        }

        /// <summary>
        /// Attempts to retain this storage. 
        /// </summary>
        /// <returns>True if this storage was retained successfully. False otherwise.</returns>
        /// <remarks><para>This storage will only be retained, if the reference counter does not indicate a disposed storage. </para></remarks>
        public bool TryRetain() {
            var curCount = m_arrayCounter;
            if (curCount > 0 &&
                Interlocked.CompareExchange(ref m_arrayCounter, curCount + 1, curCount) == curCount) {
                // Note: this is not thread safe! the storage might has completed a roundtrip to the cache and another Retain() in the meantime. 
                // To rule this out, we must check that the storage is still attached to the same array in the caller! 
                return true; 
            }
            return false; 
        }

        public void Release() {
            // disabled 
        }

        /// <summary>
        /// Internal use. Indicates to the storage that a scope block is left. 
        /// </summary>
        public void LeaveScope(Scope.ScopeInfo scope) {
            // scopes are no counters anymore, but state flags. Leaving a scope now means releasing the array! 
            if (ReferenceEquals(m_scopeState, scope)) {
                m_scopeState = null;
                Release();
            }
        }

        /// <summary>
        /// 'Traditional', synchronous assignment. Overwrite size and handles of this storage. 
        /// </summary>
        /// <param name="value">The new storage to provide new configuration.</param>
        
        public virtual void Assign(StorageT value) {

            if (object.ReferenceEquals(value, this)) {
                return;
            }

            if (object.ReferenceEquals(value, null)) {
                // TODO: unspecified! What does it mean to assign null to an array / storage? 
                // For now, we set it to empty [0]
                m_handles.Release();
                m_handles = CountableArray.Create(); // retains
                m_handles[0] = DeviceManager.GetDevice(0).New<T>(0);
                Size.SetAll(0);
                return;

            } else {
                (this as IStorage).SetHandlesInternal(value.Handles);
                Size.SetAll(value.Size);
            }
        }

        /// <summary>
        /// Assigns a new value, replacing all of this storage (size, handles &amp; all buffers). Deals with pending arrays.
        /// </summary>
        /// <param name="value">The new storage to replace this storage.</param>
        /// <param name="toOutT">flag indicating if value is assigned to an output array. </param>
        /// <param name="renameInT">[Optional] true: when switching storages the input type arrays are also flipped. False: only flip mutable arrays (default).</param>
        /// <param name="fromRetT">[Optional] flag indicating if value is referenced by a return type array (only). Default: false.</param>
        public virtual StorageT Assign(StorageT value, bool toOutT, bool fromRetT = false, bool renameInT = false) {

            if (object.ReferenceEquals(value, this) && (this.IsReady || this.ReferenceCount < 2)) {
                return this as StorageT;
            }
            System.Diagnostics.Debug.Assert(!object.ReferenceEquals(m_outArray, null));

            // We need a new storage, if ... 
            // * value is null or no RetT  (TODO: could use ref.count, more general ?)
            // 
            // We need to rename left side, if ... 
            // * this.RefCount > 1

            StorageT rsStorage;
            if (fromRetT && !object.Equals(value, null) && value.ReferenceCount <= 1 && (value.GetAsynchReferencesCount() <= 1)) { // ref count may be 0 when trying to assign a fresh storage (for array renaming)
                                                                                                                                   // TODO: <= (value.IsReady ? 1 : 2))) {  // Value might be the target of an asynch write (only!). If no other reads are using it we can simply take it over, too. 
                rsStorage = value;
                rsStorage.Retain();

            } else {

                if (ReferenceCount == 1 && m_asynchReferences == 1) {
                    // TODO: IsReady is not required, no? We can take over a pending storage as new one, I guess ? Check...!
                    // we can reuse this storage
                    rsStorage = this as StorageT;

                } else {

                    rsStorage = Create();
                    rsStorage.Retain();

                }

                // can copy it directly. If value is RetT, no asynch part is required! 

                if (Equals(value, null)) {
                    // TODO: unspecified! What does it mean to assign null to an array? 
                    // here we set it to empty [0]
                    rsStorage.m_handles[0] = DeviceManager.GetDevice(0).New<T>(0);
                    rsStorage.Size.SetAll(0);

                } else {

                    (rsStorage as IStorage).SetHandlesInternal(value.m_handles);
                    rsStorage.Size.SetAll(value.Size);

                }
            }
            System.Diagnostics.Debug.Assert(!object.ReferenceEquals(m_outArray, null));

            //renaming section - flip all array types (in contrast to ILNumerics.Core!)
            if (!object.ReferenceEquals(m_array.m_storage, rsStorage)) {

                var A = m_array; 

                void flipStorages(StorageT a, StorageT b, bool renameInArray) {

                    /* a and b have this, bidirectional structure.
                     * LocalT l <--> StorageT A
                     * OutT   o <-+
                     * InT    i <-+
                     * RetT   r <-+
                     * We must exchange the storage from A with B. This requires to update &amp; re-wire all variable <-> storage connections. 
                     * A.local, A.outarr, A.in..., A.ret..., l.storage, and o.storage, i.storage, r.storage
                     * The challenge is to order these stores in a way which results in threadsafety for concurrent readers (writes are safe through locking). 
                     * 
                     */
                    
                    var localB = b.m_array;
                    var outB = b.m_outArray;
                    var inB = b.m_inArray;
                    var retB = b.m_retArray; 
                    
                    // flip storage attributes
                    b.m_array = a.m_array; 
                    b.m_outArray = a.m_outArray;
                    b.m_inArray = a.m_inArray;
                    b.m_retArray = a.m_retArray;

                    a.m_array = localB;
                    a.m_outArray = outB;
                    a.m_inArray = inB; 
                    a.m_retArray = retB;

                    // flip array (vars on stack), not threadsafe, unfortunately ... 
                    a.m_array.m_storage = a;
                    a.m_outArray.m_storage = a;
                    a.m_inArray.m_storage = a;
                    a.m_retArray.m_storage = a; 

                    b.m_array.m_storage = b; 
                    b.m_outArray.m_storage = b;
                    b.m_inArray.m_storage = b;
                    b.m_retArray.m_storage = b;

                    if (renameInArray) {
                        a.m_inArray = inB;
                        a.m_inArray.m_storage = a;
                        b.m_inArray.m_storage = b;
                    }
                    Release();
                }

                lock (A.SynchObj) {

                    flipStorages(this as StorageT, rsStorage, renameInT); 
                    
                }

            }
            System.Diagnostics.Debug.Assert(!object.ReferenceEquals(m_array, null));
            System.Diagnostics.Debug.Assert(!object.ReferenceEquals(rsStorage.m_array, null));
            System.Diagnostics.Debug.Assert(!object.ReferenceEquals(m_outArray, null));
            System.Diagnostics.Debug.Assert(!object.ReferenceEquals(rsStorage.m_outArray, null));
            return rsStorage; 
        }

        /// <summary>
        /// Copy upper triangular matrix from this storage into a new array.
        /// </summary>
        /// <param name="n">Length of first dimension of destination array.</param>
        /// <returns>Column major array of size [n x n] with the upper triangular part of this array.</returns>
        /// <remarks><para>This function works with value element types (struct) only.</para></remarks>
        /// <exception cref="InvalidOperationException"> if <typeparamref name="T"/> is not a value type.</exception>
        internal Array<T> copyUpperTriangle(long n) {

            using (Scope.Enter()) {
                Array<T> ret = Functions.Builtin.MathInternal.zeros<T>(n, n, StorageOrders.ColumnMajor);

                for (long c = 0; c < n; c++) {
                    for (long r = 0; r <= c; r++) {
                        long pI = Size.GetSeqIndex(r, c);
                        long pO = ret.S.GetSeqIndex(r, c);
                        ret.SetValue(GetValueSeq(pI), pO);
                    }
                }
                return ret;
            }
        }

        /// <summary>
        /// Indicates the need for producing a copy of the buffer set before performing writes on this storage's elements. 
        /// </summary>
        /// <returns>True: should detach buffer set before writing. False: it is safe to write without detaching the buffer set from other storages first.</returns>
        
        internal bool MustDetachBufferSet() {
            return m_handles.ReferenceCount > 1;
        }
        
#endregion

        #region IStorage implementation
        /// <summary>
        /// Gives a shallow clone of this array: new array object, same memory. 
        /// </summary>
        /// <returns>Clone of this array as <typeparamref name="RetT"/>.</returns>
        /// <remarks>For Storage{T} this creates a shallow clone. For cells the object returned must be cloned manually.</remarks>
        /// <see cref="BaseArray.IsOfType{ElementType}"/>
        BaseArray IStorage.GetBaseArrayClone() {
            return Create(Handles, Size).RetArray; 
            //return RetArray; 
        }
        /// <summary>
        /// Gets a BaseArray instance (RetArray) corresponding to this storage.
        /// </summary>
        /// <returns>RetArray of this storage.</returns>
        BaseArray IStorage.GetBaseArray() {
            return RetArray;
        }
        protected unsafe long* AsPointer(Span<long> span, int offset = 0) {
            return (long*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref span[offset]);
        }

        /// <summary>
        /// Retrieves the cell _content_ of the cell addressed by <paramref name="indices"/> without cloning. 
        /// </summary>
        /// <param name="indices">Vector of the full index path, from root cell to the target element.</param>
        /// <param name="lenIndices">Number of indices stored in <paramref name="indices"/>.</param>
        /// <param name="start">First index in <paramref name="indices"/> to be considered.</param>
        /// <returns>The storage object found at <paramref name="indices"/>, potentially wrapped into a scalar <see cref="Storage{T}"/>.</returns>
        unsafe IStorage IStorage.GetCellContentDirect(long* indices, uint lenIndices, uint start) {
            // System.Diagnostics.Debug.Assert(start != 0, "GetCellContentDirectly() should always be called via a cell element. No direct entry to Array<T>!");  <- no! root cell may be 0-D numpy scalar!
            if (start > lenIndices || start < 0) {
                throw new ArgumentException($"The start index {start} for deep cell position {ILNumerics.Core.Global.Helper.dims2string(indices, lenIndices)} is out of range.");
            } else if (start == lenIndices) {
                return this; 
            } else {
                // pointing at a value in this array: wrapping it into self typed Storage<T>
                return Create(GetValueSeq(Size.GetSeqIndex(indices + start, lenIndices - start))); 
            }
        }
        /// <summary>
        /// Sets a new value at the element as defined by <paramref name="indices"/>.
        /// </summary>
        /// <param name="value">The new value to be set directly. No clone will be made!</param>
        /// <param name="indices">Vector of the full index path, from root cell to the target element.</param>
        /// <param name="start">First index in <paramref name="indices"/> to be considered.</param>
        /// <param name="allowExpand">Flag indicating which expansion mode to apply (numpy=false, ILNumericsV4=true).</param>
        
        unsafe IStorage IStorage.SetCellContentDirect(BaseArray value, Span<long> indices, uint start, bool allowExpand) {

            if (start >= indices.Length || start < 0) {  // should not happen ?!?
                throw new ArgumentException($"The start index {start} for deep cell position {ILNumerics.Core.Global.Helper.dims2string(AsPointer(indices), (uint)indices.Length)} is out of range.");
            } else {
                // pointing at a value in this array: wrapping it into self typed Storage<T>
                if (Equals(value,null)) {
                    throw new ArgumentException($"Cannot assign value of type '{value?.GetElementType().Name}' to the element at {ILNumerics.Core.Global.Helper.dims2string(AsPointer(indices), (uint)indices.Length)}. Expected was: {typeof(T).Name}."); 
                } else {
                    // Caution! Do not dispose value when it is RetT!
                    var size = value.GetIStorage().GetSizeInternal();
                    if (size.NumberOfElements != 1) {
                        throw new ArgumentException($"Replacing an element of <{typeof(T).Name}> via deep indexing requires a new value as _scalar_ array of element type <{typeof(T).Name}>. Found: {size.ToString()}.");
                    }
                }

                return this.SetValue((T)value.GetItem(0), AsPointer(indices, (int)start), (uint)(indices.Length - start), allowExpand); 
            }
        }


        /// <summary>
        /// Creates a shallow copy of this storage. Supports pending storages. 
        /// </summary>
        /// <returns>Shallow, lazy clone of this <see cref="IStorage"/>, having the size of and sharing the memory handles with this storage.</returns>
        /// <remarks><para>The storage returned has a reference count of 1 and an asynch reference count of 1  
        /// (2 when this is a pending storage). The reference
        /// count for the (shared) buffer set is increased. The clone owns its own size structure, now 
        /// exposing the same size information as this storage, including dimensions and strides.</para>
        /// <para>If Clone() was called on a pending storage the cloned storage returned will be pending as well. 
        /// However, the State objects representing the pending operations may or may not be the same instance.</para>
        /// <para>When the current storage comprises elements of a managed type (non-numerical arrays, as, for example: cell or 
        /// Array{string}) the clone returned likely comprises of copies of the source elements. This is an unspecified 
        /// implementation detail.</para></remarks>
        public virtual IStorage Clone() {
            var ret = Create(Handles, Size);
            // ret.Retain();  // consistent with pending case below
            ret.m_arrayCounter = 1;  // let's safe some atomic ops ... 
            ret.m_asynchReferences = 1; 
            //ret.m_scopeState = m_scopeState;  // cloning - at this stage - does not imply that the clone shares the lifetime
                                                // with the source! Scope state is managed when the lifetime of the clone is
                                                // determined. Commented + disabled, due to multiple releases in CellTests.cs
            System.Diagnostics.Debug.Assert(ret.ReferenceCount == 1);
            System.Diagnostics.Debug.Assert(ret.GetAsynchReferencesCount() == 1);  // from Retain -> 0 ... -> 1
            return ret;
        }
        
        public void EnterScope(Scope.ScopeInfo scope) {
            m_scopeState = scope; 
        }
        
        public void CloneTo(StorageT dest) {
            (dest as IStorage)?.SetHandlesInternal(m_handles);
            dest?.GetSizeUnsafe().SetAll(GetSizeUnsafe()); 
        }
        
        CountableArray IStorage.GetHandlesInternal() {
            return m_handles;
        }
        void IStorage.SetHandlesInternal(CountableArray handles) {
            handles?.Retain();   // order is important! 1st: retain, 2nd release. handles might be instance equal to m_handles!
            m_handles?.Release(); 
            m_handles = handles;
        }
        Size IStorage.GetSizeInternal() {
            return m_size;
        }
#endregion

        /// <summary>
        /// Create a new memory handle of <paramref name="elementLength"/> elements of the natural element type of this storage. 
        /// </summary>
        /// <param name="elementLength">Number of elements for the new handle.</param>
        /// <param name="clear">[optional] clear the new memory. Default: false.</param>
        /// <returns>The new handle according to the storage's internal element type.</returns>
        /// <remarks><para>This method is overriden for derived, specialized storages, where the actual element type used in memory differs from <typeparamref name="T"/>.</para></remarks>
        /// <seealso cref="LogicalStorage.New(ulong, bool)"/>
        protected internal virtual MemoryHandle New(ulong elementLength, bool clear = false) {
            return DeviceManager.GetDevice(0).New<T>(elementLength, clear);
        }

#region  factory functions

        // Creates a new storage based on a size array and order. Base offset will be 0. Element memory is allocated but remains uninitialized. 
        
        internal static StorageT Create(BaseArray<long> size, StorageOrders order) {
            using var _1 = ReaderLock.Create(size as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, out var storage); 
            var ret = Create();
            ret.S.SetAll(storage.AsRetArray(), order); // this frees size already!
            DeviceManager.GetDevice(0).New<T>((ulong)ret.S.NumberOfElements, ret.Handles, policy: PoolSizePolicy.Regular);
#if DEBUG
            Size.CheckSizeBroadcastableStrides(ret.S); 
#endif
            return ret; 
        }
        /// <summary>
        /// Extract quick scalar from this storage instance.
        /// </summary>
        /// <param name="v">BaseOffset for the new scalar.</param>
        /// <param name="fromRetArray">True: mutate this instance into a scalar, false: return a new storage.</param>
        /// <param name="outStorage"></param>
        /// <returns>Storage referencing a single element of this storage.</returns>

        internal virtual StorageT Create(long v, bool fromRetArray = false, StorageT outStorage = null) {
            StorageT ret;
            if (outStorage != null) {
                ret = outStorage;
                (ret as IStorage).SetHandlesInternal(m_handles); 
            } else {

#if !ALLOCATE_NEW_HANDLE_FOR_SCALAR
                Handles.Retain(); 
                ret = Create(Handles);  // caution! here, we would (re)use the whole memory! may causes delays on frequent self assignments! V[i] = V[i] + 1; 
#else
                // so, instead, we create a new handles array, allocate memory for the single element only and copy it over immediately. May not the fastest, but hey
                // who is going to do this with soo many scalars, anyway?  

                // THIS IS ~ THE SAME SPEED AS IN VERSION 4. BUT W/O THE COPY ABOVE SCALAR OPERATIONS ARE SO MUCH FASTER NOW! 
                // PREFER THE METHOD ABOVE! (DO NOT ALLOCATE NEW MEMORY FOR A SCALAR)
                ret = Create();
                ret.m_handles[0] = DeviceManager.GetDevice(0).New<T>((ulong)1);
                ret.SetValueSeq(GetValueSeq(v), 0); 
                v = 0;
#endif

            }
            ret.m_size.SetScalar(v, Settings.MinNumberOfArrayDimensions); 
#if DEBUG
            Size.CheckSizeBroadcastableStrides(ret.S); 
#endif
            return ret;
        }

        /// <summary>
        /// Creates an <i>uninitialized</i> storage which remains in an <span color="red">INCONSISTENT STATE</span> until configured! Only Size &amp; m_handles are created.
        /// </summary>
        /// <param name="handles">[Optional] existing handles to be shared with the new storage. Reference counter is NOT changed!</param>
        /// <returns>INCONSISTENT storage - needs configuration of host memory handle and size! Reference count is 0.</returns>
        /// <remarks><para>If <paramref name="handles"/> is provided, the storage returned holds these handles without changing its reference count. If no <paramref name="handles"/> 
        /// are provided the handles array of the returned storage will have a reference count of 1!</para></remarks>
        internal static StorageT Create(CountableArray handles = null) {
            var ret = InMemoryCache<StorageT>.Retrieve();
            ret.m_creatorThreadId = Thread.CurrentThread.ManagedThreadId;
            // TODO: unfortunately, Create() is used throughout the whole Core lib to create BaseStorages, _INCLUDING LOGICALSTORAGE_  !!! 
            // We must clear the .NumberTrues cache on such storages! The following is an ugly hack to make this sure. 
            // It should be replaced with a better / more clean class design around LogicalStorage!! 
            (ret as LogicalStorage)?.SetNumberTrues(-1); 
            ret.Handles = handles ?? CountableArray.Create();
            ret.m_asynchReferences = 0;
            ret.m_scopeState = null;
            System.Diagnostics.Debug.Assert(ret.Handles != null);
            System.Diagnostics.Debug.Assert(ret.Handles.ReferenceCount >= 1); 
            System.Diagnostics.Debug.Assert(ret.ReferenceCount == 0, $"Invalid reference count in StorageT.Create():" + ret.ReferenceCount);
            System.Diagnostics.Debug.Assert(!object.Equals(ret.m_outArray, null));
            System.Diagnostics.Debug.Assert(object.ReferenceEquals(ret.m_array.m_storage, ret)); 
            return ret; 
        }

        /// <summary>
        /// Provide a scalar storage with the value of <paramref name="scalar"/>.
        /// </summary>
        /// <param name="scalar">Value of the element of the new storage.</param>
        /// <returns>Storage of size 1x1, storing a single element with the value <paramref name="scalar"/>.</returns>
        /// <remarks><para>This will get the storage from the storage pool if possible. A new storage 
        /// is returned only if no matching  storage was available in the pool.</para>
        /// <para>The storage is created as column major storage. The number and lengths of the created 
        /// dimensions depend on the value of the configuration setting <see cref="Settings.MinNumberOfArrayDimensions"/>. 
        /// By default, this value is 2 and all arrays have at least two dimensions. In this case a #
        /// matrix of size [1, 1] is created. If the configured value for <see cref="Settings.MinNumberOfArrayDimensions"/>
        /// is smaller than 2 a vector with the length [1] or a scalar array (ndims==0, nelem==1; numpy mode) is created.</para></remarks>
        public unsafe static StorageT Create(T scalar) {
            return Create(scalar, ndims: 0); 
        }

        /// <summary>
        /// Provide a storage for the provided scalar value.
        /// </summary>
        /// <param name="scalar">Value of the element of the new storage.</param>
        /// <param name="ndims">Number of (singleton) dimensions for the new array returned.</param>
        /// <returns>Storage of size as specified by <paramref name="ndims"/>, storing a single element 
        /// with the value <paramref name="scalar"/>. RefCount = 0.</returns>
        /// <remarks><para>The storage is created as column major storage. The number and lengths of the created 
        /// dimensions depend on the value of the configuration setting <see cref="Settings.MinNumberOfArrayDimensions"/>. 
        /// By default, this value is 2 and all arrays have at least two dimensions. In this case a #
        /// matrix of size [1, 1] is created. If the configured value for <see cref="Settings.MinNumberOfArrayDimensions"/>
        /// is smaller than 2 a vector with the length [1] or a numpy scalar is created.</para></remarks>
        public unsafe static StorageT Create(T scalar, uint ndims) {
            if (ndims > Size.MaxNumberOfDimensions) {
                throw new ArgumentException($"Parameter 'ndims' exceeds the maximum number of dimensions supported ({Size.MaxNumberOfDimensions}). See: 'Size.MaxDimensions'"); 
            }
            ndims = Math.Max(ndims, Settings.MinNumberOfArrayDimensions); 
            var ret = Create();
            // make scalar of ndims dimensions
            var bsd = ret.S.GetBSD(write: true);
            bsd[0] = ndims; 
            bsd[1] = 1; 
            bsd[2] = 0;
            for (int i = 0; i < ndims; i++) {
                bsd[3 + i] = 1; 
                bsd[3 + i + ndims] = 0;
            }

            var handle = DeviceManager.GetDevice(0).New<T>(1);
            ret.Handles[0] = handle;

            if (handle is NativeHostHandle) {
                // note: this is more general and 'simple' than going through if .. else cascades and 
                // eventually doing: *((double*)handle.Pointer) = (double)(object)scalar;
                // however, we should evaluate if it is really faster!? 
                //System.TypedReference tr = __makeref(scalar);
                //switch (SizeOfT) {
                //    case 1:
                //        *((byte*)handle.Pointer) = *(byte*)(*(IntPtr*)(&tr));
                //        break;
                //    case 2:
                //        *((short*)handle.Pointer) = *(short*)(*(IntPtr*)(&tr));
                //        break;
                //    case 4:
                //        *((int*)handle.Pointer) = *(int*)(*(IntPtr*)(&tr));
                //        break;
                //    case 8:
                //        *((long*)handle.Pointer) = *(long*)(*(IntPtr*)(&tr));
                //        break;
                //    case 16:
                //        *((complex*)handle.Pointer) = *(complex*)(*(IntPtr*)(&tr));
                //        break;
                //    default:
                //        var srcP = (byte*)(*(IntPtr*)(&tr));
                //        var destP = ((byte*)handle.Pointer);
                //        for (int i = 0; i < SizeOfT; i++) {
                //            destP[i] = srcP[i]; 
                //        }
                //        break; 
                //        //throw new InvalidOperationException("This datatype is currently not supported.");
                //}
                Unsafe.Copy((void*)handle.Pointer, ref scalar); 

            } else if (handle is ManagedHostHandle<T>) {
                (handle as ManagedHostHandle<T>).HostArray[0] = scalar;
            } else if (handle is ManagedHostHandle<IStorage>) {
                (handle as ManagedHostHandle<IStorage>).HostArray[0] = null; // make sure that no old refs are disposed again...
                ret.SetValueSeq(scalar, 0); 
            } else {
                throw new NotSupportedException($"The handle type '{handle.GetType().Name}' is not supported in this context."); 
            }

#if DEBUG
            Size.CheckSizeBroadcastableStrides(ret.S); 
#endif
            return ret;
        }

        /// <summary>
        /// Provides a storage based on an existing <see cref="CountableArray"/> and a given size. 
        /// </summary>
        /// <param name="countablearray">The <see cref="CountableArray"/> to be used in the storage returned.</param>
        /// <param name="size">The source size to be copied. This object will not be altered. </param>
        /// <returns>A storage with the provided <paramref name="countablearray"/> and a size as given by <paramref name="size"/>. Reference count returned is 0!</returns>
        /// <remarks><para>The <see cref="CountableArray"/> <paramref name="countablearray"/> will be retained. The <paramref name="size"/>
        /// will be copied to the size structure of the returned storage.</para></remarks>
        internal static StorageT Create(CountableArray countablearray, Size size) {
            var ret = Create();
            countablearray.Retain();
            ret.Handles = countablearray;
            ret.Size.SetAll(size);
#if DEBUG
            if (ret.IsReady) {
                Size.CheckSizeBroadcastableStrides(ret.S);
            }
#endif
            return ret;
        }
        /// <summary>
        /// Creates a new storage based on source, references same handles, size and state (in any state).
        /// </summary>
        /// <param name="source">Source storage. </param>
        /// <param name="ignoreState">true: assumes this storage to be in state 'ready'. false (default): recognize and transfers the state from source.</param>
        /// <returns>New storage as clone of source, array ReferenceCount is 1. Asynch reference count is 1 (ready) or 2 (pending).</returns>
        internal static StorageT Create(StorageT source, bool ignoreState = false) {
            if (object.ReferenceEquals(source, null)) {
                return null;
            }
            var ret = Create();
            ret.Retain(); 

            (ret as IStorage).SetHandlesInternal(source.Handles); 
            ret.Size.SetAll(source.GetSizeUnsafe());

#if DEBUG
            if (ret.IsReady || ignoreState) {
                Size.CheckSizeBroadcastableStrides(ret.S);
            }
#endif
            return ret;
        }
        /// <summary>
        /// (Synchronously) provide a storage according to the given (and in ready state) <paramref name="size"/> and <paramref name="storageOrder"/>.
        /// </summary>
        /// <param name="size">The incoming size descriptor. This object will not be altered. It provides the dimension number and lengths only.</param>
        /// <param name="baseOffset">[Optional] Element base offset for the new storage. Default: 0.</param>
        /// <param name="storageOrder">The target storage order for this returned storage. Continous storage orders take preceedance over the actual storage order of <paramref name="size"/>.</param>
        /// <returns>Initialized storage with uninitialized memory according to <paramref name="size"/> and <paramref name="storageOrder"/> to be filled with values in own algorithms. RefCount is 0.</returns>
        /// <remarks><para>If <paramref name="storageOrder"/> is <see cref="StorageOrders.Other"/> the returned storage 
        /// has the same strides as <paramref name="size"/>. Otherwise <paramref name="storageOrder"/> is either <see cref="StorageOrders.ColumnMajor"/>
        /// or <see cref="StorageOrders.RowMajor"/> and the new storage will be in that order, too.</para></remarks>
        
        internal static StorageT Create(Size size, StorageOrders storageOrder = StorageOrders.ColumnMajor, long baseOffset = 0) {
            
            // Edit2 v7: the check was removed again, after adjusting the buffer allocation below.
            // Edit v7: following check was re-introduced, because below allocation scheme does not work with non-continuos storage orders! 
            // following check was removed on purpose: new storages can have any storage order (ex: unary operators) 
            //if (storageOrder != StorageOrders.ColumnMajor && storageOrder != StorageOrders.RowMajor) {
            //    throw new ArgumentException("New storages must be in row major or column major storage order.");
            //}
            var ret = Create();
            ret.Handles = CountableArray.Create();
            if (storageOrder == StorageOrders.ColumnMajor || storageOrder == StorageOrders.RowMajor) { 
                ret.Handles[0] = DeviceManager.GetDevice(0).New<T>((ulong)size.NumberOfElements);
                ret.Size.SetAll(size, baseOffset, storageOrder);
            } else {
                Debug.Assert(storageOrder == StorageOrders.Other, "expected 'Other' storageOrder. Found: " + storageOrder);
                // the required memory size is: length of memory spanned by size + required base offset
                var span = size.GetElementSpan() + 1; 
                ret.Handles[0] = DeviceManager.GetDevice(0).New<T>((ulong)(span + baseOffset));
                ret.Size.SetAll(size, baseOffset: baseOffset);
            }
#if DEBUG
            Size.CheckSizeBroadcastableStrides(ret.S); 
#endif
            return ret;
        }

#endregion

#region IEnumerable<T> Member

        /// <summary>
        /// Enumerator returning elements as T.
        /// </summary>
        /// <param name="storageOrder">The order of iteration. Allowed values: <see cref="StorageOrders.ColumnMajor"/> or <see cref="StorageOrders.RowMajor"/>.</param>
        /// <param name="dispose">If this parameter is true the storage will release itself after the enumerator has ended.</param>
        /// <remarks>
        /// <para>This method is thread safe.</para>
        /// </remarks>
        public IEnumerator<T> GetEnumerator(StorageOrders? storageOrder, bool dispose) {
            if (storageOrder != StorageOrders.ColumnMajor && storageOrder != StorageOrders.RowMajor) {
                throw new ArgumentException("Enumeration is possible in column major or row major storage order. The 'storage order' argument was: " + storageOrder);
            }
            try {
                if (!dispose) {
                    Retain();   // Reader lock for all mutables & InT arrays.
                }
                foreach(var i in Size.Iterator(storageOrder)) {
                    yield return GetValueSeq(i);
                }
            } finally {
                // all release: mutables release its reader lock. RetT releases itself.
                Release(); 
            }
        }

#endregion

#region overriding object.ToString(), Equals()

        /// <summary>
        /// Generate a hash code based on the current element values.
        /// </summary>
        /// <returns>The hash code.</returns>
        /// <remarks>The hash code is generated by taking the values currently stored in the array into account.
        /// Therefore, the function must iterate over all elements in the array - which makes it a somehow expensive 
        /// operation. Take this into account when considering to use large arrays in collections like dictionaries 
        /// or hashtables, which make great use of hash codes.</remarks>
        public override int GetHashCode() {
            if (IsDisposed) return base.GetHashCode();
            int ret = Size.GetHashCode();
            ret = unchecked(ret * 17 + Handles.GetHashCode());
            return ret;
        }

        #endregion

        #region Cleanup

        /// <summary>
        /// Makes sure that this storage uses a dedicated buffer set - not being shared with other storages. This is required but not sufficient for enabling writes to this storage!
        /// </summary>
        /// <param name="targetDeviceID">[Optional] The device the current data of this storages device specific memory shall be copied onto. (ONAL: not used)</param>
        /// <remarks>This function ensures that...
        /// <list type="bullet">
        /// <item>this storage uses memory which is not shared by other storages. If memory is currently shared, the 
        /// memorys data is copied to a new memory region which is then current to this storage.</item>
        /// </list>
        /// <para>Note that this function does not check whether this storage is shared by multiple arrays 
        /// (InArray, local array, output arrays). It does neither check if this storage is being used 
        /// by (other) asynch operations! Both checks must be performed also, in order to prepare a storage for writing! </para>
        /// <para>While during common use and when accessing 
        /// a local array this local array will be the only array referencing the storage there are situations 
        /// when other arrays may reference a storage. Examples include the handling of input array types, the use 
        /// of this storage in asynchronous operations and 
        /// when local arrays (<see cref="Array{T}"/>) are defined as attributes of classes. Therefore, it is recommended
        /// to ensure that no other arrays currently exist which share this storage: <see cref="ReferenceCount"/> == 1 &amp;&amp; <see cref="GetAsynchReferencesCount()"/> == 1. 
        /// </para>
        /// </remarks>
        public bool DetachBufferSetInplace(uint targetDeviceID = 0) {
            // Note, we cannot perform below check here! In a perfect threadsafe world the array counter could be 0 and the
            // asynch counter could be 2. But we do not live in such a world! At least asynch counter can be arbitrary now! 
            // However, newly enqueued segments should not matter, because they will wait for 'this' detach()ing thread.
            //if (m_arrayCounter > 1 || GetAsynchReferencesCount() > 1) {
            //    throw new InvalidOperationException("Invalid attempt to detach a shared storage. This indicates a bug in ILNumerics! Please report back this issue to: support@ilnumerics.net!"); 
            //}

            if (m_handles.ReferenceCount == 1)
                return false;
            var newCountableArray = m_handles.Clone<T>(m_size, SizeOfT, targetDeviceID);
            m_size.BaseOffset = 0;
            m_handles.Release();
            m_handles = newCountableArray;

            // TODO: try to replace with the next line. Issue: this changes the BSD /strides of this array. 
            // Therefore, many functions must change their order of operations, ex.: SetRange()
            // Advantage: smaller memory footprint, continous storage. 
            // EnsureStorageOrder(m_size.IsContinuous ? m_size.StorageOrder : Settings.DefaultStorageOrder, forceCopy: true);
            System.Diagnostics.Debug.Assert(m_handles != null && m_handles.ReferenceCount == 1);
            //System.Diagnostics.Debug.Assert(ReferenceCount == 1);
            return true; 
        }

        /// <summary>
        /// Creates a new, detached (writable) storage. Out of-place! -> Does not change this storage! 
        /// </summary>
        /// <param name="targetDeviceID">[Optional] the device ID of the target device data must be copied to.</param>
        /// <returns>The detached storage or this storage.</returns>
        /// <remarks><para>This version of GetDetached() is used by default since version 7.0. It works 'Out Of-Place' and
        /// allows to mutate / exchange this storage atomically, when required. The storage returned points to a new storage or 
        /// to this storage. Use reference comparison to detect, if a clone was made (a new storage reference) 
        /// or not (same reference). </para>
        /// <para>Note, this function evolved from and replaces DetachBufferset() in earlier releases. It does no longer mutate 
        /// this storage directly. Instead, when required a new storage is created (copied) and returned instead. Callers 
        /// must decide if Assign() is required or not. This allows to use GetDetached() in a chain of mutations (f.e.: SetRangeML())
        /// </para></remarks>
        public IStorage GetDetached(uint targetDeviceID = 0) {
            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            // This IStorage function must execute even in a pending state ! 
            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            // It is commonly called within AllInputDevicesSelected() -> allocate I/O output storage. Thus,
            // the Size and the CountableArray are set. 

            //if (!Settings.ThreadIgnoreSegmentLocks && !IsReady) {
            //    // pending storage outside of LRun or asynch worker. We need an asynch implementation: 
            //    // TODO: this is currently not used. It returns a storage with the ref count of 1 or 2, as 
            //    // commonly returned by Clone()! This needs to be fixed and made consistent with the non-pending
            //    // case below, which returns a ref count of 0. 
            //    var cloned = Clone(); 
            //    var detached = Create();

            //    var simpAw = SimpleAwaiter.Create<T,LocalT,InT, OutT, RetT, StorageT>(cloned as StorageT, detached,
            //                                valuesFunc: (inStor, outStor, context) => {
            //                                    inStor.DetachBufferSetInplace(context.DeviceID ?? 0); 
            //                                },
            //                                name: "GetDetached");
            //    return detached;     

            //} else {
            
                // disabled: there is more to deciding, whether we have to detach or not! (f.e.: asynch refs...?). Leave this to the caller and do what this method is supposed to do!
                //if (m_handles.ReferenceCount == 1)  
                //    return this as StorageT;
                var newCountableArray = m_handles.Clone<T>(m_size, SizeOfT, targetDeviceID);
                var ret = Create(newCountableArray);
                ret.S.SetAll(m_size, baseOffset: 0);

                System.Diagnostics.Debug.Assert(ret.m_handles != null && ret.m_handles.ReferenceCount == 1);
                // Assign(ret, true, true); 
                return ret;
            //}
        }

        /// <summary>
        /// Expert interface: disposes this array storage. This must be the last action performed on this object! Afterwards, this object can not be used anymore! 
        /// </summary>
        /// <remarks><para>During regular use ILNumerics arrays are cleaned up automatically and deterministically. Users should not call 
        /// <see cref="Dispose()"/> explicitly! However, if Dispose was called on an array A, A cannot be used anymore thereafter! Note, 
        /// that it is even illegal to use _any_ reference to this storage afterwards! Even reassigning to a local array variable can cause 
        /// an exception after calling Dispose() on the local array variable. Or, even worse: (re-)using an array object after calling Dispose() 
        /// may causes unexpected side effects on subsequent array operations, because the disposed object may be cached and reused in the meantime! </para>
        /// <para>Don't call Dispose() manually! This interface is provided for compatibility with legacy versions only.</para>
        /// </remarks>
        
        public void Dispose() {
            System.Diagnostics.Debug.Assert(m_arrayCounter == 0, "The reference count for storage " + ID + " was found to equal " + m_arrayCounter + " in Dispose().");
            Version++;  // prevents any scope blocks from inadvertendly freeing this storage again

            // the host array is only released once here. It may be used
            // by other storages, still. 
            m_handles?.Release();
            m_handles = null;
            m_creatorThreadId = -1;
            // 'register' this storage object in global memory cache for quick pooling/reusing
            InMemoryCache<StorageT>.Store(this as StorageT);


        }

        #endregion

        #region helper functions

        /// <summary>
        /// Configures <paramref name="size">input size</paramref> with the size according to the dimensions of A. 
        /// </summary>        
        internal unsafe static void GetSizeFromSystemArray(Array A, Size size) {
            var bsd = size.GetBSD(write: true);
            if (A == null) {
                A = new byte[0];
            }
            uint rank = (uint)A.Rank;
            if (rank < Settings.MinNumberOfArrayDimensions) {
                rank = Settings.MinNumberOfArrayDimensions;
            }
            bsd[0] = rank;

            long stride = A.LongLength;
            bsd[1] = stride;
            bsd[2] = 0;
            int i = 0;
            if (A.Rank == 1) {
                if (Settings.DefaultStorageOrder != StorageOrders.RowMajor) {
                    // make column major
                    bsd[i + 3] = A.GetLongLength(0);
                    bsd[3 + rank] = bsd[i + 3] == 1 ? 0 : 1;
                    for (i = 1; i < rank; i++) {
                        bsd[i + 3] = (1);
                        bsd[3 + rank + i] = 0;
                    }
                } else { 
                    //Settings.DefaultStorageOrder == StorageOrders.RowMajor
                    bsd[2 + rank - i] = A.GetLongLength(0);
                    bsd[2 + 2 * rank - i] = bsd[2 + rank - i] == 1 ? 0 : 1;
                    for (i = 1; i < rank; i++) {
                        bsd[2 + rank - i] = 1;
                        bsd[2 + 2 * rank - i] = 0;
                    }
                }
            } else {
                for (; i < rank && i < A.Rank; i++) {
                    long dimLen = A.GetLongLength(i);
                    bsd[i + 3] = dimLen;
                    if (dimLen > 0) {
                        stride = stride / dimLen;
                    }
                    bsd[3 + rank + i] = dimLen == 1 ? 0 : stride;
                }
                System.Diagnostics.Debug.Assert(stride == 1 || stride == 0);
                for (; i < rank; i++) {
                    bsd[i + 3] = 1;
                    bsd[3 + rank + i] = 0;
                }
            }
        }

        
        internal static unsafe void MarshalCopy(Array A, MemoryHandle handle, Size outSize) {
            System.Diagnostics.Debug.Assert(A != null);

            GetSizeFromSystemArray(A, outSize);

            var aType = A.GetType().GetElementType();
            if (aType == typeof(T)) {
                if (ElementInstance is bool) {
                    // bool is 4 bytes on CLR, 1 byte in unmanaged world
                    byte* hP = (byte*)handle.Pointer;
                    foreach (bool item in A) {
                        *(hP++) = (item ? (byte)1 : (byte)0);
                    }
                } else {
                    // we can simply do a block copy. the handle will be 
                    // on the managed heap for ref types and for structs all other 
                    // element types (except bool) match sizes.
                    if (!aType.IsValueType) {
                        System.Diagnostics.Debug.Assert(A.Rank == 1);
                        if (handle is ManagedHostHandle<IStorage>) {

                            System.Diagnostics.Debug.Assert(typeof(T) == typeof(BaseArray));

                            var arr = (handle as ManagedHostHandle<IStorage>).HostArray;
                            //System.Array.Copy(A, arr, (int)outSize.NumberOfElements); // "int" length is a restriction of the Array.Copy method.
                            for (int i = 0; i < (int)outSize.NumberOfElements; i++) {
                                // T is BaseArray
                                var ba = A.GetValue(i) as BaseArray;
                                if (!Equals(ba, null)) {
                                    var jstor = ba.GetClonedStorage(forceRelease: true);
                                    arr[i] = jstor;
                                }
                            }
                        } else {
                            System.Array.Copy(A, (handle as ManagedHostHandle<T>).HostArray, (int)outSize.NumberOfElements); // "int" length is a restriction of the Array.Copy method.
                        }
                    } else {
                        // structs live on the unmanaged heap
                        var gch = GCHandle.Alloc(A, GCHandleType.Pinned);
                        try {
                            //NativeMethods.CopyMemory(handle.Pointer, gch.AddrOfPinnedObject(), (IntPtr)(SizeOfT * outSize.NumberOfElements));
                            Buffer.MemoryCopy(
                              source: (void*)gch.AddrOfPinnedObject(),
                              destination: (void*)handle.Pointer,
                              sourceBytesToCopy: SizeOfT * outSize.NumberOfElements,
                              destinationSizeInBytes: SizeOfT * outSize.NumberOfElements);
                        } finally {
                            if (gch.IsAllocated) gch.Free();
                        }
                    }
                }
            } else if (aType.GetInterface("IConvertible") != null) {

                #region element type conversions
                if (ElementInstance is double) {
                    double* arrP = (double*)handle.Pointer;
                    foreach (var item in A) {
                        *(arrP++) = Convert.ToDouble(item);
                    }
                } else if (ElementInstance is float) {
                    float* arrP = (float*)handle.Pointer;
                    foreach (var item in A) {
                        *(arrP++) = Convert.ToSingle(item);
                    }
                } else if (ElementInstance is int) {
                    int* arrP = (int*)handle.Pointer;
                    foreach (var item in A) {
                        *(arrP++) = Convert.ToInt32(item);
                    }
                } else if (ElementInstance is uint) {
                    uint* arrP = (uint*)handle.Pointer;
                    foreach (var item in A) {
                        *(arrP++) = Convert.ToUInt32(item);
                    }
                } else if (ElementInstance is bool) {
                    byte* arrP = (byte*)handle.Pointer;
                    foreach (var item in A) {
                        *(arrP++) = Convert.ToBoolean(item) ? (byte)1 : (byte)0;
                    }
                } else if (ElementInstance is sbyte) {
                    sbyte* arrP = (sbyte*)handle.Pointer;
                    foreach (var item in A) {
                        *(arrP++) = Convert.ToSByte(item);
                    }
                } else if (ElementInstance is byte) {
                    byte* arrP = (byte*)handle.Pointer;
                    foreach (var item in A) {
                        *(arrP++) = Convert.ToByte(item);
                    }
                } else if (ElementInstance is long) {
                    long* arrP = (long*)handle.Pointer;
                    foreach (var item in A) {
                        *(arrP++) = Convert.ToInt64(item);
                    }
                } else if (ElementInstance is ulong) {
                    ulong* arrP = (ulong*)handle.Pointer;
                    foreach (var item in A) {
                        *(arrP++) = Convert.ToUInt64(item);
                    }
                } else if (ElementInstance is char) {
                    char* arrP = (char*)handle.Pointer;
                    foreach (var item in A) {
                        *(arrP++) = Convert.ToChar(item);
                    }
                } else if (ElementInstance is decimal) {
                    decimal* arrP = (decimal*)handle.Pointer;
                    foreach (var item in A) {
                        *(arrP++) = Convert.ToDecimal(item);
                    }
                    #endregion
                } else {
                    throw new InvalidCastException(String.Format("Cannot convert System.Array of type {0} to Array of {1}. When using array literals, try to declare the element type explicitly!", aType.FullName, typeof(T).FullName));
                }
            } else {
                #region try to convert scalar Array<T> and the like. This may be the result of array initializer literals: ... = { {A, 3}, {C + 1, 0} }; 
                int releasedItemIndex = -1;
                uint posArr = 0;
                foreach (var item in A) {
                    var ba = item as BaseArray;
                    if (object.Equals(ba, null)) {
                        throw new InvalidCastException("Error converting System.Array to Array: unsupported element type. Only primitive numeric elements and scalar numeric Array can be converted from elements of the source System.Array.");
                    }
                    // check size. Be careful not to release incoming RetT!
                    var clonedStorage = ba.GetClonedStorage(false);
                    var baSize = clonedStorage.GetSizeInternal();
                    if (baSize.NumberOfElements != 1) {
                        throw new InvalidOperationException("ILNumerics arrays - when provided as elements of array initializer literals - must be scalar! The array found at position " + posArr + " is of size " + baSize.ToString());
                    }
                    clonedStorage.Release();

                    #region convert by type

                    object baItem = ba.GetItem(0);
                    releasedItemIndex++;
                    if (ElementInstance is double) {
                        ((double*)handle.Pointer)[posArr++] = Convert.ToDouble(baItem);
                    } else if (ElementInstance is float) {
                        ((float*)handle.Pointer)[posArr++] = Convert.ToSingle(baItem);
                    } else if (ElementInstance is int) {
                        ((int*)handle.Pointer)[posArr++] = Convert.ToInt32(baItem);
                    } else if (ElementInstance is uint) {
                        ((uint*)handle.Pointer)[posArr++] = Convert.ToUInt32(baItem);
                    } else if (ElementInstance is byte) {
                        ((byte*)handle.Pointer)[posArr++] = Convert.ToByte(baItem);
                    } else if (ElementInstance is sbyte) {
                        ((sbyte*)handle.Pointer)[posArr++] = Convert.ToSByte(baItem);
                    } else if (ElementInstance is short) {
                        ((short*)handle.Pointer)[posArr++] = Convert.ToInt16(baItem);
                    } else if (ElementInstance is ushort) {
                        ((ushort*)handle.Pointer)[posArr++] = Convert.ToUInt16(baItem);
                    } else if (ElementInstance is long) {
                        ((long*)handle.Pointer)[posArr++] = Convert.ToInt64(baItem);
                    } else if (ElementInstance is ulong) {
                        ((ulong*)handle.Pointer)[posArr++] = Convert.ToUInt64(baItem);
                    } else {
                        throw new InvalidCastException("Error converting System.Array to Array: unsupported element type. Only scalar Arrays of .NET scalar numeric primitive value types are supported: double, float, int, uint, long, ulong, short, ushort, sbyte, byte.");
                    }
                    #endregion
                }

                #endregion
            }
        }
        internal static uint ComputeSizeOfT() {
            if (typeof(T).IsValueType) {
                if (typeof(T) == typeof(bool)) {
                    return 1;
                } else if (typeof(T) == typeof(char)) {
                    return 2;
                } else {
                    return (uint)Marshal.SizeOf<T>();
                }
            } else {
                return 1; // (uint)IntPtr.Size; // yeah...it is not exactly true. But this is used for determining stride lengths, mainly. So 1 fits into well for T[] elements.
            }
        }

#endregion

    }
}
