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
using System;

namespace ILNumerics.Core.StorageLayer {

    /// <summary>
    /// Interface, supporting array hosting containers, like Cell. This type is used internally. 
    /// </summary>
    public unsafe interface IStorage {
        /// <summary>
        /// Gives a short info message describing the main properties of this storage. 
        /// </summary>
        /// <returns>Info message.</returns>
        string ShortInfo();
        /// <summary>
        /// Gives a short info message describing the main properties of this storage. Allows to configure the content of the info message.  
        /// </summary>
        /// <param name="ignoreLocks">Create the message and include all info requested, despite the array/storage being locked for asynchronous access (as far es supported).</param>
        /// <param name="includeCounters">Include current state of the 4 reference counters. Default: true.</param>
        /// <param name="includeDevices">Include storage state information for all devices configured, as far as supported.</param>
        /// <param name="includeIDs">Include identifiers of all objects within the storage hierarchy. Default: true.</param>
        /// <param name="includeSize">Include size information. Default: true.</param>
        /// <param name="includeStorageOrder">Include storage order information. Default: true.</param>
        /// <param name="includeType">Include type information. Default: true.</param>
        /// <param name="includeValues">Include summary element value information. Default: true.</param>
        /// <returns>Info message.</returns>
        string ShortInfo(bool includeType = true, bool includeSize = true, bool includeValues = true, bool includeStorageOrder = true, bool includeDevices = true, bool includeIDs = true, bool includeCounters = true, bool ignoreLocks = false);
        /// <summary>
        /// Gives object information. 
        /// </summary>
        /// <returns>Object information.</returns>
        string ToString();

        /// <summary>
        /// Gets a BaseArray instance (RetArray) corresponding to a clone of this storage.
        /// </summary>
        /// <returns>A clone </returns>
        /// <remarks>This API is more 'low level' than BaseArray.GetClonedArray() which is able to cope with pending arrays. GetBaseArrayClone() is used in the context of cells and does not handle pending storages (safely).</remarks>
        BaseArray GetBaseArrayClone();
        /// <summary>
        /// Gets a BaseArray instance (RetArray) corresponding to this storage.
        /// </summary>
        /// <returns></returns>
        BaseArray GetBaseArray();
        /// <summary>
        /// Retrieves the cell _content_ of the cell addressed by <paramref name="indices"/> without cloning. 
        /// </summary>
        /// <param name="indices">Vector of the full index path, from root cell to the target element.</param>
        /// <param name="lenIndices">Number of indices stored in <paramref name="indices"/>.</param>
        /// <param name="start">First index in <paramref name="indices"/> to be considered.</param>
        /// <returns>The storage object found at <paramref name="indices"/>, potentially wrapped into a scalar <see cref="Storage{T}"/>.</returns>
        IStorage GetCellContentDirect(long* indices, uint lenIndices, uint start);
        /// <summary>
        /// Sets the value of the element / cell element (==array) addressed by <paramref name="indices"/> without cloning. 
        /// </summary>
        /// <param name="value">New value for the element. A clone will be stored for all non-volatile array types (non-RetT).</param>
        /// <param name="indices">Vector of the full index path, from root cell to the target element.</param>
        /// <param name="start">First index in <paramref name="indices"/> to be considered.</param>
        /// <param name="allowExpand">Flag indicating which expansion mode to apply (numpy=false, ILNumericsV4=true).</param>
        /// <returns>The IStorage having the new value set at position determined by <paramref name="indices"/>.</returns>
        /// <remarks>The storage returned will be a new storage for non-cell arrays (atomic mutation). It will be the same storage for cell arrays (deep indexing without clone).</remarks>
        IStorage SetCellContentDirect(BaseArray value, Span<long> indices, uint start, bool allowExpand);

        /// <summary>
        /// Increases the reference count for the current object to prevent it from being released.
        /// </summary>
        /// <remarks>Call this method when you need to ensure that the object remains valid and is not
        /// disposed or released while in use. You are responsible for later releasing the reference when it is no
        /// longer needed, typically by calling a corresponding release method. Failing to balance retain and release
        /// calls may result in resource leaks.</remarks>
        void Retain();
        /// <summary>
        /// Releases resources or locks held by the current instance, allowing them to be reused or disposed.
        /// </summary>
        /// <remarks>Call this method when the instance is no longer needed or when you want to free
        /// associated resources. After calling <c>Release</c>, the instance may not be usable until reacquired or
        /// reinitialized, depending on the implementation.</remarks>
        void Release();

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
        IStorage Clone();

        /// <summary>
        /// Gets the unique identifier for the entity.
        /// </summary>
        int ID { get; }

        /// <summary>
        /// Retrieves the (set of) handle(s) associated with the current instance.
        /// </summary>
        /// <returns>A CountableArray containing the handles. The array may be empty if no handles are available.</returns>
        CountableArray GetHandlesInternal();

        /// <summary>
        /// Explicitly set the internal countable array (for all device's memory handles) - ignoring any pending state may existing - and update reference counters. 
        /// </summary>
        /// <param name="handles">The new handles for this IStorage.</param>
        /// <remarks>This method allows to modify the internal Handles array even for pending storages. Reference counts are updated accordingly.</remarks>
        void SetHandlesInternal(CountableArray handles);

        /// <summary>
        /// Gets the size descriptor of this storage. 
        /// </summary>
        /// <returns>A reference to the Size descriptor of this storage instance. </returns>
        Size GetSizeInternal();

        /// <summary>
        /// Whether or not this storage references a cell object. Returns true for storages of Cell, InCell, OutCell and RetCell. False otherwise. 
        /// </summary>
        bool IsCell { get; }

        /// <summary>
        /// Number identifying the modification version of this storage.
        /// </summary>
        int Version { get; }

        /// <summary>
        /// Returns the length, in bytes, of the element type represented by this instance.
        /// </summary>
        /// <returns>The number of bytes that make up the element type.</returns>
        int GetElementTypeLength();

        /// <summary>
        /// Gets the current number of active references to the object. 
        /// </summary>
        /// <returns>The number of active references. Returns 0 if there are no references.</returns>
        int GetReferenceCount();

        /// <summary>
        /// Create and return a detached version of this storage. Out of-place version. This storage is not changed.
        /// </summary>
        /// <returns>Detached storage as new instance or this storage instance. The buffer set's (Handles) reference count will have a value of 1.</returns>
        /// <remarks>Note, that detaching may affect (resets) the base offset in the BSD of the storage returned!</remarks>
        IStorage GetDetached(uint targetDeviceID);

        /// <summary>
        /// Detaches this storage's handles from other storages it might be shared with. Inplace version. 
        /// </summary>
        /// <param name="deviceIdx">The device index of the target device. Currently: 0 (the host device).</param>
        /// <returns>True if the storage was actually detached. False, if no detaching was required, hence no copy was performed.</returns>
        bool DetachBufferSetInplace(uint deviceIdx);

        /// <summary>
        /// Enters the specified scope, updating the provided scope information.
        /// </summary>
        /// <param name="scope">The scope information to store the entrance.</param>
        void EnterScope(Scope.ScopeInfo scope);
        /// <summary>
        /// Indicates leaving the provided scope. Performs memory management tasks. 
        /// </summary>
        /// <param name="scope">The scope information storing the corresponding entrance.</param>
        void LeaveScope(Scope.ScopeInfo scope);

    }
}