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
using ILNumerics.Core.DeviceManagement;
using ILNumerics.Core.Global;
using ILNumerics.Core.MemoryLayer;
using ILNumerics.Core.StorageLayer;
using System;
using System.Linq;
using System.Threading;

namespace ILNumerics.Core {

    [Serializable]
    
    public class CountableArray : ICacheable<CountableArray> {

        #region attributes
        private object m_synch = new object(); 

        /// <summary>
        /// The array of device specific memory. Host memory (CLR, unmanaged virt. memory) is specified to be stored at index 0. 
        /// </summary>
        /// <remarks>Order of devices: 0 - process memory (CLR host), 1 - CPU OpenCL device, 2 - integr. GPU (SVM, if available), 3 -... GPU / others</remarks>
        private readonly MemoryHandle[] m_handles;

        /// <summary>
        /// The count of storages currently referencing this <see cref="CountableArray"/>.
        /// </summary>
        int m_referenceCount;  //this is int (not uint) on purpose: Interlocked is defined for int only.

        /// <summary>
        /// The volatile index of the device which was last used to write data associated with this <see cref="CountableArray"/> to. This device currently holds the most up-to date copy of the data.
        /// </summary>
        /// <remarks>This value is subject to change at any time (not threadsafe!).</remarks>
        volatile int m_currentDeviceIdx;

        /// <summary>
        /// The unique identifier for this instance.
        /// </summary>
        public readonly int ID;

        internal int Version {  get; private set; }

        ///// <summary>
        ///// The index of the device a pending operation is expected to be executed fastest / most efficiently.
        ///// </summary>
        ///// <remarks>Note, this optional value is only used within the processing of segments. It marks the 
        ///// device (index) a buffer of this array is going to be allocated on. However, such buffer may or may 
        ///// not already exist on the computed device. Rather, this value is used to inform and to calculate the 
        ///// cost for subsequent segments. In the sequence of segment processing this value is typically set 
        ///// after all BSDs for the inputs of a segment were updated and before the kernel output buffer is 
        ///// allocated (and before the kernel is run).</remarks>
        //int m_computedDeviceIdx;

        private CountableArray m_previous;
        //private int m_deletionMark; 
        private static int s_instanceCounter = 0;
        #endregion

        #region properties

        ///// <summary>
        //////EDIT / commented, because: we must ensure thread safety on delayed buffer creation. Buffers are created 
        //////synchronously and their content is copied asynchronously. For cost calculation all we need is the info
        ////// _if_ a device buffer exists. So later segments can access this info without requiring a further flag as below. 
        ///// The index of the device a pending operation is expected to be executed fastest / most efficiently.
        ///// </summary>
        ///// <remarks>Note, this optional value is only used within the processing of segments. It marks the 
        ///// device (index) a buffer of this array is going to be allocated on. However, such buffer may or may 
        ///// not already exist on the computed device. Rather, this value is used to inform and to calculate the 
        ///// cost for subsequent segments. In the sequence of segment processing this value is typically set 
        ///// after all BSDs for the inputs of a segment were updated and before the kernel output buffer is 
        ///// allocated (and before the kernel is run).</remarks>
        //public int ComputedDeviceIdx {
        //    get {
        //        return m_computedDeviceIdx;
        //    }
        //    
        //    internal set {
        //        if (value < 0 || value >= m_handles.Length) {
        //            throw new ArgumentException($"The 0 based index of devices must lay in the range of available devices. Found: {value}.");
        //        }
        //        if (value != m_computedDeviceIdx) {
        //            m_computedDeviceIdx = value;
        //        }
        //    }
        //}


        /// <summary>
        /// Gets the index of the device where the most up-to-date data of this <see cref="CountableArray"/> are stored or -1 if no data are available.
        /// </summary>
        public int CurrentDeviceIdx {
            get {
                lock (m_synch) {
                    return m_currentDeviceIdx;
                }
            }
            
            internal set {
                lock (m_synch) {
                    if (value < 0 || value >= m_handles.Length) {
                        throw new ArgumentException($"The 0 based index of devices must lay in the range of available devices. Found: {value}.");
                    }
                    if (value != m_currentDeviceIdx) {
                        m_currentDeviceIdx = value;
                    }
                }
            }
        }

        /// <summary>
        /// The current value of the counter of references to this <see cref="CountableArray"/>. This is readonly and <em>not</em> threadsafe.
        /// </summary>
        /// <remarks>In order to in-/decrease the reference counter, use 
        /// the <see cref="CountableArray.Retain()"/>
        /// and <see cref="CountableArray.Release()"/> functions.</remarks>
        public int ReferenceCount {
            get { return Volatile.Read(ref m_referenceCount); }
        }

        /// <summary>
        /// Supports thread safe caching of CountableArray objects.
        /// </summary>
        public ref CountableArray Previous { get { return ref m_previous; } }
        //public ref int DeletionMark => ref m_deletionMark; 

        /// <summary>
        /// Gets the memory handle of the device referenced by <paramref name="i"/> or sets it. 
        /// </summary>
        /// <param name="i">Device ID</param>
        /// <returns>MemoryHandle of the specified device.</returns>
        public MemoryHandle this[uint i] {
            
            get {
                lock (m_synch) {

                    if (m_currentDeviceIdx != i && m_handles[i] == null) { // || !object.ReferenceEquals((m_handles[m_currentDeviceIdx] as ILNumerics.Core.Segments.OpenCL.Buffer)?.SVMHandle, m_handles[i]))) {
                        var device = DeviceManagement.DeviceManager.GetDevice(i);
                        device.EnsureBuffer(this);
                    }
                    //Finish(); 
                    return m_handles[i];  // this handle might be pending the completion of element copy
                }
            }
            
            internal set {
                lock (m_synch) {
                    // an externally provided handle is considered a 'new' value. Any existing handles 
                    // are released first and the new handle will be the only value for this array. 
                    // TODO: can be improved by keeping SVM buffers if i points to such or to a native host handle. 

                    // SetOrReplace(i, value); <- no, this would not clear other buffer slots! 
                    if (m_handles[i] != value) {
                        if (m_currentDeviceIdx >= 0) {
                            ReleaseHandles();
                        }
                        if (value != null) {
                            value.Retain();
                            m_handles[i] = value;
                            m_currentDeviceIdx = (int)i;
                        }
                    }
                }
            }
        }

        #endregion

        #region constructors

        /// <summary>
        /// Provide a countable array for a new storage. Reference count will be 1 (no need to Retain()!). 
        /// </summary>
        /// <returns><see cref="CountableArray"/> for further use.</returns>
        internal static CountableArray Create() {
            var ret = InMemoryCache<CountableArray>.Retrieve();
            ret.m_currentDeviceIdx = -1;
            System.Diagnostics.Debug.Assert(ret.ReferenceCount == 0);
            ret.m_referenceCount = 1; 
            return ret;

        }
        internal void Cache() {
            Version++; 
            InMemoryCache<CountableArray>.Store(this); 
        }

        /// <summary>
        /// Creates a new handles array. Internal use. Use CountableArray.Create() and Cache() for de-/allocation!
        /// </summary>
        
        public CountableArray() {
            var devcount = DeviceManagement.DeviceManager.GetDeviceCount();
            m_handles = new MemoryHandle[devcount];
            // m_referenceCount = 1; //! saves the first Retain() call  -> will be done by Create()
            m_currentDeviceIdx = -1;
            ID = Interlocked.Increment(ref s_instanceCounter); 
        }
        #endregion

#region public interface

        /// <summary>
        /// Registers the caller as user of this <see cref="CountableArray"/>. Increase the reference counter. 
        /// </summary>
        /// <returns>The value of the internal reference counter after registering the caller.</returns>
        /// <seealso cref="Release()"/>
        internal void Retain() {
            System.Diagnostics.Debug.Assert(m_previous == null, "Retaining a cached object."); 
            System.Threading.Interlocked.Increment(ref m_referenceCount);
        }

        /// <summary>
        /// Decrease the reference counter. Release resources and cache this object when reference counter reaches 0.
        /// </summary>
        public int Release() {
            System.Diagnostics.Debug.Assert(m_previous == null, "Releasing a cached object.");
            int c = System.Threading.Interlocked.Decrement(ref m_referenceCount);
            if (c == 0) {
                lock (m_synch) {

                    // Finish();  // if there are life buffers in processing then they are kept alive from the segments!
                    ReleaseHandles();
                    // cache this countable array in the cache

                    m_currentDeviceIdx = -1; // indicates no handles are up to-date.
                    Cache();
                }
            }
            return c;
        }

        /// <summary>
        /// Copies the most up-to-date memory from this <see cref="CountableArray"/> to the <paramref name="targetDeviceIdx"/> of a new <see cref="CountableArray"/>. 
        /// </summary>
        /// <param name="targetDeviceIdx">The 0-based index of the device which is going to hold the (only) reference to the copied memory.</param>
        /// <param name="size">The size descriptor. Used to read the required region to copy.</param>
        /// <param name="elementSizeBytes">The number of bytes used to store a single element of type <typeparamref name="T"/>.</param>
        /// <remarks><para>The memory is copied from the 'current' handle, i.e.: from the device the data of this <see cref="CountableArray"/>
        /// were last written to.</para></remarks>
        /// <returns>A new <see cref="CountableArray"/> holding the most current data in its <paramref name="targetDeviceIdx"/> slot.</returns>
        
        internal CountableArray Clone<T>(Size size, uint elementSizeBytes, uint targetDeviceIdx = 0) {
            System.Diagnostics.Debug.Assert(targetDeviceIdx < m_handles.Length, "Parameter 'targetDevice' must be in the range of available devices.");
            lock (m_synch) {

                var ret = Create();

                if (m_currentDeviceIdx < 0) {
                    // no data 
                    return ret;
                }
                var elemCount = (size.GetElementSpan() + 1);
                if (m_handles[targetDeviceIdx] != null) {
                    // stay on the same device
                    // This case may handles ManagedHostHandles<T> also! Be prepared! For such types (mostly Cell and Array<string>) sizeOfT must be 1, which is 
                    // ensured by always acquiring it from Storage<T>.SizeOfT. This way we can save the condition here and handle the copy in a general way. 
                    var dev = DeviceManagement.DeviceManager.GetDevice(targetDeviceIdx);
                    dev.New<T>((ulong)elemCount, ret);
                    var destHandle = ret[targetDeviceIdx];
                    m_handles[targetDeviceIdx].CopyTo(targetDeviceIdx, destHandle, targetDeviceIdx, (ulong)(size.BaseOffset * elementSizeBytes), new IntPtr(elemCount * elementSizeBytes));
                    // old: dev.Copy(Handles[targetDeviceIdx], destHandle, new IntPtr(size.BaseOffset * sizeOfT), new IntPtr(lenBytes));
                    //ret[targetDeviceIdx] = destHandle;
                } else {
                    // if the data are not on the target device yet, we always have to involve the host. This case handles Native (struct) data only! No managed types here. 
                    var lenBytes = elemCount * elementSizeBytes;
                    var hostHandle = DeviceManager.GetDevice(0).MemoryPool.New((ulong)lenBytes);
                    DeviceManager.GetDevice((uint)CurrentDeviceIdx)
                        .CopyToHost(m_handles[CurrentDeviceIdx], hostHandle,
                                    new IntPtr(size.BaseOffset * elementSizeBytes), new IntPtr(lenBytes));
                    ret[0] = hostHandle;
                    DeviceManager.GetDevice(targetDeviceIdx).EnsureBuffer(ret);
                }
                return ret;
            }
        }

        /// <summary>
        /// Stores an existing handle into an existing slot of this <see cref="CountableArray"/>. Considers reference counting. Does not affect other slots. Not threadsafe.
        /// </summary>
        /// <param name="index">Slot (/device) index.</param>
        /// <param name="handle">The handle to be stored.</param>
        /// <remarks>This function is not threadsafe! If the value null was provided for <paramref name="handle"/> the slot <paramref name="index"/> is cleared.</remarks>
        
        [Obsolete("Use SetOrReplace(uint, CountableArray) instead, to facilitate SVM buffers!")]
        internal void SetOrReplace(int index, MemoryHandle handle) {
            lock (m_synch) {

                if (m_handles[index] != handle) {
                    m_handles[index]?.Release();
                    handle?.Retain();
                    m_handles[index] = handle;
                    if (handle != null) {
                        m_currentDeviceIdx = index;
                    }
                }
            }
        }

        /// <summary>
        /// Returns the plain array used internally to hold the handles. Used for EnsureBuffer(), debugging and as visualization helper only! 
        /// </summary>
        /// <returns>Reference to the internal handles array. Do use with care!</returns>
        /// <remarks>Do not use this method unless you know what you are doing! Use the indexer of <see cref="CountableArray"/> instead!</remarks>
        internal MemoryHandle[] GetHandlesArray() {
            return m_handles;
        }


        /// <summary>
        /// Gets a hash code of the handles _and its content_ currently stored in this object.
        /// </summary>
        /// <returns>Hash code recognizing this object and its content.</returns>
        public override int GetHashCode() {
            lock (m_synch) {
                int ret = base.GetHashCode();
                for (int i = 0; i < m_handles.Length; i++) {
                    var handle = m_handles[i];
                    if (handle != null) {
                        ret = unchecked(ret * 17 + handle.GetHashCode());
                    }
                }
                return ret;
            }
        }

#endregion

#region private helpers
        /// <summary>
        /// Releases any handles currently stored in this countable array.
        /// </summary>
        private void ReleaseHandles() {

            for (int i = 0; i < m_handles.Length; i++) {
                var handle = m_handles[i]; 
                if (handle is ManagedHostHandle<IStorage> mhh) {
                    var arr = mhh.HostArray;
                    for (int h = 0; h < arr.Length; h++) {
                        arr[h]?.Release();
                        arr[h] = null;
                    }
                }
                if (handle != null) {
                    m_handles[i] = null;
                    handle.Release();

                }
            }
        }
        
        /// <summary>
        /// Checks whether this countable array maintains a current copy of its data on the specified device. 
        /// </summary>
        /// <param name="v">Index of the device to test.</param>
        /// <returns>true if this data is on the specified device. False otherwise.</returns>
        
        public bool IsOnDevice(int v) {
            return m_handles[v] != null; 
        }

        
        internal IntPtr GetID(int target) {
            lock (m_synch) {
                if (m_handles == null || target < 0 || target >= m_handles.Length) {
                    return new IntPtr(-1);
                }
                return m_handles[target]?.ID ?? new IntPtr(-1);
            }
        }

        
        internal string ShortInfo() {
            lock (m_synch) {
                return string.Join(",", m_handles.Select ((h, i) => object.Equals(h, null) ? "" : i.ToString())); 
            }
        }

        #endregion
    }
}
