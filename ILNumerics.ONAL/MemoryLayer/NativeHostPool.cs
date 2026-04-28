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
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ILNumerics.Core.MemoryLayer
{
    /// <summary>
    /// A memory pool responsible for creating and caching memory chunks on (unmanaged) host memory. 
    /// </summary>
    
    public class NativeHostPool : MemoryPool<MemoryHandle>
    {
        // constants for memory alignment and size requirements for sharing device buffers on CPU + Intel integrated GPU. 
        // See: https://software.intel.com/en-us/iocl-opg-sharing-resources-efficiently 
        const int MEMORY_ALIGNMENT_BYTES = 1 << 12; // 4096; 
        const int MEMORY_SIZE_MULTIPLE_OF = 64 - 1;
        static readonly ulong MAX_ADJUSTABLE_SIZE = (UIntPtr.Size == 4 ? uint.MaxValue : ulong.MaxValue) - (MEMORY_ALIGNMENT_BYTES + MEMORY_SIZE_MULTIPLE_OF); 

#if TRACK_NATIVE_ALLOC_BYTES
        private long m_allocBytes = 0; 
#endif
        internal long AllocBytes {
            get {
#if TRACK_NATIVE_ALLOC_BYTES
                return m_allocBytes;
#else
                return 0; 
#endif
            }
        }

        /// <summary>
        /// Create a new memory pool for the host device (CLR). 
        /// </summary>
        /// <param name="device">The host device.</param>
        public NativeHostPool(Device device) : base(device, MemoryTypes.Process) {
            //System.Diagnostics.Debug.Assert(device.Name.ToLower().Contains("host")); 
        }

        
        internal override MemoryHandle AllocateInternal(UIntPtr length)
        {
            unsafe
            {

#if TRACK_NATIVE_ALLOC_BYTES
                if (m_allocBytes > 5L * 1024 * 1024 * 1024) {
                    // artificial 5GB limit on the unmanaged heap
                    throw new OutOfMemoryException(); 
                } 
#endif
#pragma warning disable CS0618
                // on 64 bit we need to keep track of the systems health.
                // It looks like the CLR uses CreateMemoryResourceNotification & Co. 
                // to perform this monitoring. In this attempt we do the same.
                // Note, that on 32 bit we get an NULL handle early enough, so not 
                // to drain the system memory on paging. This is due to the limited (~2GB)
                // virt. address space on 32 bit. But without this precaution
                // LocalAlloc() would allow us for way too much memory and start paging badly...
                if (Device.IsLowMemory()) {  // ho: was: Environment.Is64BitProcess && ... but shouldn't we be 
                                             // proactively careful on x86 also? 
                    throw new OutOfMemoryException(); 
                }
#pragma warning restore
                var allocLength = length;
                var adjustLength = length.ToUInt64() < MAX_ADJUSTABLE_SIZE; 

                if (adjustLength) {
                    allocLength += MEMORY_ALIGNMENT_BYTES + MEMORY_SIZE_MULTIPLE_OF; 
                }
                NativeHostHandle handle = null;
                IntPtr ptr = IntPtr.Zero; 
                try {
                    //var handle = Native.NativeMethods.LocalAlloc(0, allocLength);
                    ptr = Marshal.AllocHGlobal((IntPtr)(void*)allocLength);
                } finally {
                    if (ptr != IntPtr.Zero) {
                        handle = new NativeHostHandle(ptr);
                        Device.MemoryPool.RegisterAllocatedBytes((long)allocLength.ToUInt64()); 
                    }
                }
                if (handle == null || handle.Handle == IntPtr.Zero) 
                    throw new OutOfMemoryException($"Failed to allocate {allocLength} (aligned) bytes from native host memory."); 
                handle.Length = length;
                handle.m_allocatedLength = allocLength.ToUInt64(); 
                handle.Device = this.Device;
                
                System.Diagnostics.Debug.Assert(handle.m_referenceCount == 0); 

                if (adjustLength) {
                    long p = handle.Handle.ToInt64();
                    handle.Pointer = new IntPtr(p + MEMORY_ALIGNMENT_BYTES - (p & MEMORY_ALIGNMENT_BYTES - 1)); 
                } else {
                    handle.Pointer = handle.Handle; 
                }

#if TRACK_NATIVE_ALLOC_BYTES
                System.Diagnostics.Trace.WriteLine($"Allocated: {handle.Handle.ToInt64()}, length: {handle.Length} wasted: {handle.Pointer.ToInt64() - handle.Handle.ToInt64()}. From: {GetStackTraceShort()}");  
#endif

#if DEBUG_DISABLED
                *(byte*)handle.Pointer = 0xff;
                *((byte*)handle.Pointer + length.ToUInt64() - 1) = 0xff;
#endif

#if DEBUG_DISABLED
                if (length.ToUInt64() > 100) {
                    System.IO.File.AppendAllText($"D:\\dumps\\localfree.txt", $"Allocate: {handle.Pointer} - Length: {length} - Stack: {Environment.StackTrace}\r\n");
                }
#endif
#if TRACK_NATIVE_ALLOC_BYTES
                Interlocked.Add(ref m_allocBytes, (long)length.ToUInt64());
                handle.RegisterForTracking(this); 
#endif
#if DEBUG_TRACK_DATA
                Trace.WriteLine($"({System.Threading.Thread.CurrentThread.ManagedThreadId}) allocated memory handle (native): {handle.ID.ToInt64()}");
#endif

                return handle;
            }
        }


#if TRACK_NATIVE_ALLOC_BYTES
        internal void InformHandleReleased(NativeHostHandle handle) {
            Interlocked.Add(ref m_allocBytes, -(long)handle.Length.ToUInt64());
        }
#endif

    }
}
