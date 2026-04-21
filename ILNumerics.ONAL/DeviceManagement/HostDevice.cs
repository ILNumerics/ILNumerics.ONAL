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
//#undef USING_OPENCL
using ILNumerics.Core.MemoryLayer;
using ILNumerics.Core.StorageLayer;
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace ILNumerics.Core.DeviceManagement {

    /// <summary>
    /// This class represents the host processor as ILNumerics device. It is used for all .NET based code.
    /// </summary>
    
    public sealed class 
        HostDevice : Device {

        #region attributes
        IntPtr m_lowMemoryNotificationHandle;
        readonly static Thread m_mainThread = Thread.CurrentThread;  // save the 'main' thread

#endregion

#region properties
        /// <summary>
        /// The memory pool instance for the host. Unique for each local thread.
        /// </summary>
        //[ThreadStatic]
        private static MemoryPool<MemoryHandle> m_memoryPool;

        /// <summary>
        /// Thread local access to the (native) memory pool for this device. 
        /// </summary>
        public override MemoryPool<MemoryHandle> MemoryPool {
            
            get {
                if (m_memoryPool == null) {

                    m_memoryPool = m_poolFactoryFunc(this);

                    lock (m_memoryPools) {
                        m_memoryPools.Add(m_memoryPool);
                    }

                }
                return m_memoryPool;
            }
        }

        /// <summary>
        /// Gives the type (kind) of this device. This returns <see cref="DeviceTypes.Host"/>.
        /// </summary>
        public override DeviceTypes DeviceType => DeviceTypes.Host;

        public static bool IsMainThread => m_mainThread == Thread.CurrentThread;

        #endregion

        #region constructors

        public HostDevice(): base("Host", factory) {
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                m_lowMemoryNotificationHandle = Native.NativeMethods.CreateMemoryResourceNotification(Native.NativeMethods.MemoryResourceNotificationType.LowMemoryResourceNotification);
            } else {
                m_lowMemoryNotificationHandle = IntPtr.Zero; // is not referenced below
            }
            Index = 0;

        }

#endregion
        
        static NativeHostPool factory(Device dev) {
            if (IsMainThread) {
                return new NativeHostPool(dev) { MaxSize = 0 };
            } else {
                // TODO: replace with a dummy pool, not pooling at all! 
                return new NativeHostPool(dev) { MaxSize = 0 };
            }
        }

        /// <summary>
        /// Copy memory from <paramref name="source"/> to <paramref name="dest"/>, both from hosts virtual memory. 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <param name="length"></param>
        /// <param name="offset"></param>
        public unsafe override void Copy(MemoryHandle source, MemoryHandle dest, IntPtr offset, IntPtr length) {
            // copy from host ... to host
            System.Diagnostics.Debug.Assert(source is NativeHostHandle);
            System.Diagnostics.Debug.Assert(dest is NativeHostHandle);
            Copy((IntPtr)((byte*)source.Pointer + offset.ToInt64()), dest.Pointer, length); 
        }

        /// <summary>
        /// Copy memory from <paramref name="source"/> to <paramref name="dest"/>, both from hosts virtual memory. 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <param name="length"></param>
        public unsafe void Copy(IntPtr source, IntPtr dest, IntPtr length) {
            //NativeMethods.CopyMemory(dest, source, length);
            System.Buffer.MemoryCopy((void*)source, (void*)dest, (long)length, (long)length);
        }
        
        public unsafe override void CopyFromHost(MemoryHandle hostHandle, MemoryHandle deviceHandle, IntPtr offset, IntPtr length) {
            //NativeMethods.CopyMemory((byte*)deviceHandle.Pointer, (byte*)hostHandle.Pointer + (long)offset, (ulong)length);
            throw new NotImplementedException();
            //System.Buffer.MemoryCopy(
            //    source: (byte*)hostHandle.Pointer + (long)offset, 
            //    destination: (void*)deviceHandle.Pointer, 
            //    destinationSizeInBytes: (long)length, 
            //    sourceBytesToCopy: (long)length);
        }

        /// <summary>
        /// Copies device memory to the host. 
        /// </summary>
        /// <param name="deviceHandle"></param>
        /// <param name="hostHandle"></param>
        /// <param name="offset">Offset into the device handle.</param>
        /// <param name="length">Number of bytes to copy.</param>
        public unsafe override void CopyToHost(MemoryHandle deviceHandle, MemoryHandle hostHandle, IntPtr offset, IntPtr length) {
            //NativeMethods.CopyMemory((byte*)hostHandle.Pointer, (byte*)deviceHandle.Pointer + (long)offset, (ulong)length);
            throw new NotImplementedException(); 
            //System.Buffer.MemoryCopy(
            //    source: (byte*)deviceHandle.Pointer + (long)offset, 
            //    destination: (byte*)hostHandle.Pointer, 
            //    sourceBytesToCopy: (long)length, 
            //    destinationSizeInBytes: (long)length);
        }

        /// <summary>
        /// Read a single value of type <typeparamref name="T"/> from the element position <paramref name="index"/> on this device.
        /// </summary>
        /// <typeparam name="T">The type of the value to read. Must derive from <see cref="System.ValueType"/>.</typeparam>
        /// <param name="index">The index of the first element of type <typeparamref name="T"/> to retrieve relative to the 
        /// start of the memory region referenced by <paramref name="handle"/>.</param>
        /// <param name="handle">Memory handle to the memory containing the value to be retrieved.</param>
        /// <returns>The value of the element addressed by <paramref name="index"/>.</returns>
        public unsafe override T GetValue<T>(MemoryHandle handle, ulong index) {
            // TODO: T is not constrained to struct (anymore, but was when this function was first written)
            T ret = default(T);
            var gch = GCHandle.Alloc(ret, GCHandleType.Pinned); 
            try {
                uint elemLength = (uint)Marshal.SizeOf(ret); 
                var src = (ulong)handle.Pointer.ToInt64() + index * elemLength; 
                //NativeMethods.CopyMemory(gch.AddrOfPinnedObject(), new IntPtr((long)src), new IntPtr(elemLength));
                System.Buffer.MemoryCopy(
                  source: (byte*)handle.Pointer + index * elemLength,
                  destination: (void*)gch.AddrOfPinnedObject(),
                  sourceBytesToCopy: elemLength,
                  destinationSizeInBytes: elemLength);
                return ret;
            } finally {
                gch.Free(); 
            }
        }

        /// <summary>
        /// Store the <paramref name="value"/> at the memory with element position <paramref name="index"/> on this device.
        /// </summary>
        /// <typeparam name="T">The element type of <paramref name="value"/> (derived from <see cref="System.ValueType"/>).</typeparam>
        /// <param name="value">The new value to be written to the memory.</param>
        /// <param name="index">The element index of the element to be written.</param>
        /// <param name="handle">Memory handle to the memory containing the element whose value is going to be replaced. This handle must belong to this device.</param>
        public unsafe override void SetValue<T>(T value, MemoryHandle handle, ulong index) {
            // Note: T is constrained to struct
            var gch = GCHandle.Alloc(value, GCHandleType.Pinned);
            try {
                var elemLength = (uint)Marshal.SizeOf(value);
                ulong addr = (ulong)handle.Pointer + index * elemLength; 
                //NativeMethods.CopyMemory(new IntPtr((long)addr), gch.AddrOfPinnedObject(), new IntPtr(elemLength));
                System.Buffer.MemoryCopy(
                    source: (void*)gch.AddrOfPinnedObject(),
                    destination: (byte*)handle.Pointer + index * elemLength,
                    sourceBytesToCopy: elemLength,
                    destinationSizeInBytes: elemLength); 
            } finally {
                gch.Free();
            }
        }

        /// <summary>
        /// Acquire a memory handle / array from this pool or create a new one. This class allows <typeparamref name="T"/> to be a reference type.
        /// </summary>
        /// <typeparam name="T">Type of elements which will be stored in the memory.</typeparam>
        /// <param name="elementCount">Number of elements. This determins the size of the memory required.</param>
        /// <param name="clear">[Optional] True: clear the element values. False: do not clear the elements (default).</param>
        /// <param name="policy">[Optional] Allocation policy: regular (default) or segmented mode (limited).</param>
        /// <param name="numElemsTyped">[Optional] if set, gives the number of typed elements, considering the element byte length - for untyped allocations.</param>
        /// <returns>A <see cref="NativeHostHandle"/> referencing a region of main (virtual) memory sufficient to store <paramref name="elementCount"/> elements of type <typeparamref name="T"/>.</returns>
        /// <remarks><para>The <see cref="HostDevice"/> allocates arrays for <see cref="System.ValueType"/> elements on 
        /// native process memory instead on the managed heap. Reference type elements, however, are allocated on the managed heap. 
        /// Hence, the handle returned will either be a <see cref="NativeHostHandle"/> or a <see cref="ManagedHostHandle{T}"/>. Note, both 
        /// categories of memory are managed in their own pools. In the case of <see cref="System.ValueType"/> elements and native storage 
        /// the new memory will - if possible - be taken from the <see cref="NativeHostPool"/> pool associated with the current thread and the host device. 
        /// For managed arrays the global, static, thread local pool <see cref="ManagedHostPool{T}.Pool"/> is considered.</para></remarks>
        /// <seealso cref="NativeHostPool"/>
        /// <seealso cref="ManagedHostPool{T}"/>
        public override MemoryHandle New<T>(ulong elementCount, bool clear = false, PoolSizePolicy policy = PoolSizePolicy.Regular, ulong numElemsTyped = ulong.MaxValue) {
            T inst = default(T);
            MemoryHandle ret;

            if (inst is System.ValueType) {
                // Allocate from native pool
                // We want the same behavior as sizeof(), since we are considering structs only and sizeof(bool) shall be 1.
                // But T is considered a runtime type here, so we need to use Marshal.Sizeof() instead. This would give 4 for bool, though...
                if (inst is bool) {
                    ret = this.MemoryPool.New(new UIntPtr(elementCount), clear, policy);
                }else if (inst is char) {
                    ret = this.MemoryPool.New(new UIntPtr(elementCount * 2), clear, policy);
                } else {
                    uint size = (uint)Marshal.SizeOf<T>();  // Note: .SizeOf(inst) would call typeof(inst) internally, anyway  
                    ret = this.MemoryPool.New(new UIntPtr(size * elementCount), clear, policy);
                }
            } else {
                // allocate from managed pool
                if (typeof(T) == typeof(IStorage) || typeof(T) == typeof(BaseArray)) {
                    // cell storage
                    ret = ManagedHostPool<IStorage>.Pool.New(new UIntPtr(elementCount * (uint)IntPtr.Size), clear, policy);
                } else {
                    ret = ManagedHostPool<T>.Pool.New(new UIntPtr(elementCount * (uint)IntPtr.Size), clear, policy);
                }
            }
            return ret; 
        }

        [Obsolete("Performance related API. Will be removed in a future release.")]
        public override bool IsLowMemory() {

            if (!System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) { 
                // TODO: implement for other platforms. Potentially, we can go with https://docs.microsoft.com/en-us/dotnet/api/system.gc.registerforfullgcnotification?view=netstandard-2.1 ?
                return false;  
            }
            int ret;
            int err = Native.NativeMethods.QueryMemoryResourceNotification(m_lowMemoryNotificationHandle, out ret);
            return err == 0 || ret != 0;
        }

        /// <summary>
        /// Make sure up to-date data are available on the host memory (as NativeHostHandle).
        /// </summary>
        /// <param name="array">The array of handles whose data existence needs to be ensured for the host memory.</param>
        [Obsolete("Performance related API. Will be removed in a future release.")]
        public override void EnsureBuffer(CountableArray array) {
            
            // makes sure that Handles[0] is up to-date. 
            if (array.CurrentDeviceIdx < 0 || array.IsOnDevice(0)) {
                return; 
            }

            var srcHandle = array[(uint)array.CurrentDeviceIdx];
            // design convention: any svm device buffer would also keep a native host buffer around.
            var hostHandle = MemoryPool.New(srcHandle.Length, init: false);
#pragma warning disable CS0618
            array.SetOrReplace(0, hostHandle);  // retains host handle
#pragma warning restore
            DeviceManager.GetDevice((uint)array.CurrentDeviceIdx).CopyToHost(
                srcHandle, hostHandle, IntPtr.Zero, (IntPtr)(long)hostHandle.Length);
        }

#region private helpers 
        internal override MemoryHandle EnsureBufferAsync(CountableArray handles) {
            throw new NotImplementedException();
        }

        internal override long GetMaxOPS<T>() {
            throw new NotImplementedException();
        }
#endregion

    }
}
