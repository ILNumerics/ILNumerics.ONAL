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
using ILNumerics.Core.MemoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace ILNumerics.Core.DeviceManagement {
    
    public abstract class Device {

        #region attributes

        
        protected Func<Device, MemoryPool<MemoryHandle>> m_poolFactoryFunc;
        protected readonly List<IMemoryPool> m_memoryPools = new List<IMemoryPool>();
        protected double[] m_t;  // transfer costs from this to any device [tks]. TODO!! 
        protected double m_la;  // costs: accelerated loads [tks]
        protected double m_sa;  // costs: accelerated stores [tks]
        protected uint m_maxComputeUnits = 0;
        private object m_synch = new object(); 
        protected static long s_devicesHighMemoryFlag = 0;  // bit array, != 0 -> indicates high memory state for all devices. This is not threadsafe and always stale!
        //private static AutoResetEvent s_awaiterCompletedSignal = new AutoResetEvent(false);  // attempted to limit live awaiters via WaitHandle - replaced by AwaiterBase.s_limitCounter
        //Commented. See notes in MemoryPool.RegisterAllocatedBytes() ! 
        //public static ManualResetEvent LeaveCriticalMemoryState = new ManualResetEvent(true); 
        #endregion

        #region constructors
        
        public Device(string name, Func<Device, MemoryPool<MemoryHandle>> memoryPoolFactoryFunc) {
            m_poolFactoryFunc = memoryPoolFactoryFunc;
            this.Name = name;
        }

        #endregion

        #region properties
        /// <summary>
        /// State flag indicating that at least one device is low on memory / under memory pressure / exceeds the limit of planned maximum memory consumption. Not threadsafe!
        /// </summary>
        public static bool IsHighMemoryState => s_devicesHighMemoryFlag != 0;

        public double[] T {
            get {
                return m_t;
            }
        }

        /// <summary>
        /// Load access costs (tks).
        /// </summary>
        public double LA { get { return m_la; } }

        /// <summary>
        /// Store access costs (tks).
        /// </summary>
        public double SA { get { return m_sa; } }


        /// <summary>
        /// Access to the (native) memory pool for this device. 
        /// </summary>
        public abstract MemoryPool<MemoryHandle> MemoryPool {  get; }

        /// <summary>
        /// A name used to identify this device. 
        /// </summary>
        public virtual string Name { get; internal set; }

        /// <summary>
        /// The 0-based index of this device in the ordered set of devices currently avaiable on this system.
        /// </summary>
        public int Index { get; internal set; }

        /// <summary>
        /// Gets the type(s) of the device. A bitfield with bits set according to values from <see cref="ILNumerics.DeviceTypes"/>.
        /// </summary>
        /// <seealso cref="DeviceTypes"/>
        /// <seealso cref="Device.Name"/>
        public abstract DeviceTypes DeviceType {
            get;
        }

        /// <summary>
        /// Flag indicating whether or not this device is part of a shared memory context, 
        /// accessing the computers main memory. 
        /// </summary>
        public virtual bool IsSVM {
            get {
                return false; 
            }
            internal set { }
        }

        /// <summary>
        /// The number of compute units. On host: number of cores. CL: number of compute units (or numbmer of concurrently running SIMD threads). 
        /// </summary>
        public uint MaxComputeUnits {
            get {
                if (m_maxComputeUnits == 0) {
                    m_maxComputeUnits = (uint)Environment.ProcessorCount;
                }
                return m_maxComputeUnits;
            }
        }
        /// <summary>
        /// Maximum (optimal) number of operations performed on this device at once (in parallel). CPU: all threads * SIMD width! 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal abstract long GetMaxOPS<T>() where T : struct;
#endregion

#region public API
        public virtual void Initialize() {
            m_t = new double[DeviceManager.GetDeviceCount()];
        }

        /// <summary>
        /// Ensures that a memory handle for the array exists on this device. Its content may not be ready yet, though. 
        /// </summary>
        /// <param name="handles"></param>
        /// <returns></returns>
        internal abstract MemoryHandle EnsureBufferAsync(CountableArray handles);

        /// <summary>
        /// Informs about the current system health, memory related. Derived classes implement device specific measures. This base implementation always returns false. 
        /// </summary>
        /// <returns>true if there is high memory pressure. Allocations should be limited.</returns>
        [Obsolete("Performance related API. Will be removed in a future release.")]
        public virtual bool IsLowMemory() {
            return false;
        }

        /// <summary>
        /// Default maximum size of a memory pool: 200MB.
        /// </summary>
        /// <returns>Size in bytes. (200MB)</returns>
        internal virtual long GetMaxPoolSizeHint() {
            if (Environment.Is64BitProcess) {
#if NETCOREAPP3_0_OR_GREATER
                return GC.GetGCMemoryInfo().TotalAvailableMemoryBytes / 2;  // long.MaxValue;
#else
                return 300L << 20; // long.MaxValue;  // TODO: check & adjust! 
#endif
            } else {
                return 200L << 20;
            }
        }

        /// <summary>
        /// Read a single value of type <typeparamref name="T"/> from the memory position <paramref name="handle"/> on this device.
        /// </summary>
        /// <typeparam name="T">The type of the value to read. Must derive from <see cref="System.ValueType"/>.</typeparam>
        /// <param name="index">The index of the first element of type <typeparamref name="T"/> to retrieve relative to the 
        /// start of the memory region referenced by <paramref name="handle"/>.</param>
        /// <param name="handle">Memory handle to the memory containing the value to be retrieved.</param>
        /// <returns>The value read from the memory address.</returns>
        public abstract T GetValue<T>(MemoryHandle handle, ulong index) where T : struct;
        /// <summary>
        /// Store the <paramref name="value"/> at the memory with element position <paramref name="index"/> on this device.
        /// </summary>
        /// <typeparam name="T">The element type of <paramref name="value"/> (derived from <see cref="System.ValueType"/>).</typeparam>
        /// <param name="value">The new value to be written to the memory.</param>
        /// <param name="index">The element index of the element to be written.</param>
        /// <param name="handle">Memory handle to the memory containing the element whose value is going to be replaced. This handle must belong to this device.</param>
        public unsafe abstract void SetValue<T>(T value, MemoryHandle handle, ulong index) where T : struct;

        /// <summary>
        /// Copy a range of memory from the host to this device. 
        /// </summary>
        /// <param name="hostHandle">Memory handle to host device.</param>
        /// <param name="deviceHandle">Memory handle to device memory.</param>
        /// <param name="offset">Offset of the first byte to read.</param>
        /// <param name="length">Number of bytes to copy.</param>
        public abstract void CopyFromHost(MemoryHandle hostHandle, MemoryHandle deviceHandle, IntPtr offset, IntPtr length);
        /// <summary>
        /// Copies a block of bytes from this device to the host memory. Blocking operation. Memory barrier for related handles.
        /// </summary>
        /// <param name="deviceHandle"></param>
        /// <param name="hostHandle"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        public abstract void CopyToHost(MemoryHandle deviceHandle, MemoryHandle hostHandle, IntPtr offset, IntPtr length);
        /// <summary>
        /// Copy memory onto the same device. 
        /// </summary>
        /// <param name="sourceHandle">Handle to memory on this device, the region to copy.</param>
        /// <param name="destHandle">Handle to the destination region, must be on this device.</param>
        /// <param name="offset">Start offset in bytes.</param>
        /// <param name="length">Number of bytes to copy. </param>
        public abstract void Copy(MemoryHandle sourceHandle, MemoryHandle destHandle, IntPtr offset, IntPtr length);

        /// <summary>
        /// Allocate new memory for the given number of elements.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="elementCount">Number of elements to store.</param>
        /// <param name="clear">[Optional] true: make sure the memory is cleared on return. Default: false.</param>
        /// <param name="policy">[Optional] the allocation policy to use for the current call.</param>
        /// <param name="numElemsTyped">[Optional] if set, gives the number of typed elements, considering the element byte length - for untyped allocations.</param>
        /// <returns><see cref="MemoryLayer.NativeHostHandle"/> for structs, <see cref="ManagedHostHandle{T}"/> for reference types <typeparamref name="T"/>.</returns>
        /// <remarks><para>The handle returned has a reference count of 0.</para>
        /// <para>For compatibility with shared memory devices use: <see cref="New{T}(ulong, CountableArray, PoolSizePolicy, ulong)"/></para></remarks>
        public abstract MemoryHandle New<T>(ulong elementCount, bool clear = false, PoolSizePolicy policy = PoolSizePolicy.Regular, ulong numElemsTyped = ulong.MaxValue);

        /// <summary>
        /// Allocate memory on this OpenCL device and store it into <paramref name="A"/>. Suitable for shared memory devices.  
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="numberOfElements">Numbmer of elements to allocate.</param>
        /// <param name="A">Target for the new memory handle / buffer.</param>
        /// <param name="policy">[Optional] the allocation policy to use for the current call.</param>
        /// <param name="numElemsTyped">[Optional] if set, gives the number of typed elements, considering the element byte length - for untyped allocations.</param>
        
		public void New<T>(ulong numberOfElements, CountableArray A, PoolSizePolicy policy = PoolSizePolicy.Regular, ulong numElemsTyped = ulong.MaxValue) {

            if (IsSVM) {
                // allocate on the host, use EnsureBuffer then
                System.Diagnostics.Debug.Assert(A.IsOnDevice(0) == false || A[0].IsInvalid);
                A[0] = DeviceManager.GetDevice(0).New<T>(numberOfElements, clear: false, policy: policy, numElemsTyped);
                // EnsureBuffer(A); -> ONAL: removed. We only have the Host device.
            } else {
                A[(uint)Index] = this.New<T>(numberOfElements, clear: false, policy: policy, numElemsTyped);
            }
            System.Diagnostics.Debug.Assert(A.CurrentDeviceIdx == Index, "New handles should be 'current' on the device.");
        }


        ///// <summary>
        ///// Returns an allocated memory handle for later reuse.
        ///// </summary>
        ///// <typeparam name="T">Element type. Determines the pool category (managed / unmanaged).</typeparam>
        ///// <param name="handle">The handle to be pooled.</param>
        //public abstract void Free<T>(MemoryHandle handle);

        internal void ShrinkAllPoolsBut<HandleT>(MemoryPool<HandleT> memoryPool) where HandleT : MemoryHandle {
            lock (m_memoryPools) {
                foreach (var pool in m_memoryPools) {
                    if (pool != memoryPool) {
                        // Make sure not to shrink the pool of the calling thread. This would cause a dead lock! 
                        pool.Shrink(0);
                    }
                }
            }
        }
#endregion

#region public API 
        public override string ToString() {
            return Name;
        }
        [Obsolete("Performance related API. Will be removed in a future release.")]
        public abstract void EnsureBuffer(CountableArray array);

        public virtual void ReleaseHandle(MemoryHandle handle) {
            handle.Release();   // caution: do not call handle.ReleaseHandle()! This would release the internal CriticalHandle.handle. :| 
        }

#endregion

    }
}
