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
//#define TRACK_NATIVE_ALLOC_BYTES
using System;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;

namespace ILNumerics.Core.MemoryLayer {

    /// <summary>
    /// Handle representing unmanaged host memory in a robust (self-disposing) way. Aligned in certain cases.
    /// </summary>
    /// <remarks>The pointer to native memory wrapped inside this handle will be acquired from the unmanaged heap. For performance reason
    /// the acquiring class / method is responsible for initializing new handles with required data: Length and Pointer. 
    /// <para>In case of <see cref="NativeHostPool"/> <see cref="Pointer"/> will present the lowest address within the allocated memory region 
    /// which fulfills the alignment and length requirements of Intel's OpenCL buffer sharing (CPU / Integrated GPU). See: <see cref="NativeHostPool.MEMORY_ALIGNMENT_BYTES"/> which determines the needed alignment (4K). 
    /// The length of the buffer </para>
    /// <para><b>Note, that alignment for <see cref="NativeHostHandle"/> is only guaranteed for handles pointing to memory regions larger than <see cref="NativeHostPool.MEMORY_ALIGNMENT_BYTES"/></b>.</para>
    /// </remarks>
    [DebuggerDisplay("{ShortDisplay()}")]
    public sealed class NativeHostHandle : MemoryHandle {

        public static readonly IntPtr INVALID_HANDLE = IntPtr.Zero; 
        public static readonly IntPtr EXTERNAL_HANDLE = new IntPtr(-1);

        IntPtr m_alignedPointer;
        internal ulong m_allocatedLength;

#if TRACK_NATIVE_ALLOC_BYTES
        //private NativeHostPool m_trackingPool;
        //internal void RegisterForTracking(NativeHostPool pool) {
        //    m_trackingPool = pool; 
        //}
#endif

#if DEBUG_TRACK_HANDLES
        public static System.Collections.Concurrent.ConcurrentDictionary<int, string> handles = new System.Collections.Concurrent.ConcurrentDictionary<int, string>(); 
#endif 

        /// <summary>
        /// Create a new host memory handle. This will be used by P/Invoke methods having <see cref="NativeHostHandle"/> as return type. 
        /// </summary>
        public NativeHostHandle() : base() {
            // Retain(); // <- don't retain! By spec: create with refcount = 0! 
        }

        /// <summary>
        /// Create a new host memory handle. This will be used by P/Invoke methods having <see cref="NativeHostHandle"/> as return type. 
        /// </summary>
        internal NativeHostHandle(IntPtr ptr) : base() {
            handle = ptr;
            // Retain(); // <- don't retain! By spec: create with refcount = 0!
#if DEBUG
            //System.Diagnostics.Trace.WriteLine($"Entering NativeHostHandle(): {handle}. StackTrace: {Environment.StackTrace}");
#endif

#if DEBUG_TRACK_HANDLES
            handles.TryAdd(GetHashCode(), Environment.StackTrace);
#endif

        }

        public IntPtr GetPointerUnsafe() {
            return m_alignedPointer; 
        }
        /// <summary>
        /// Address of the payload memory region for this handle. This respects alignment requirements and is different to <see cref="Handle"/> which points to the beginning of the memory region originally allocated from the OS. 
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public override IntPtr Pointer {
            get {
#if DEBUG
                if (IsInvalid) {
                    throw new InvalidOperationException("Attempt to access an invalid handle!");
                }
#endif
                return m_alignedPointer;
            }
            internal set {
                if (m_alignedPointer != value) {
                    m_alignedPointer = value;
                }
            }
        }

        /// <summary>
        /// The <see cref="Pointer"/> value with the aligned memory address this handle is pointing to.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public override IntPtr ID {
            get { return Pointer; }
        }

        /// <summary>
        /// The address originally provided by the OS in result of the memory allocation request. This address is not aligned and must be kept unchanged for freeing the handle later. 
        /// </summary>
        public IntPtr Handle {
            get {
                return handle; 
            }
        }

        /// <summary>
        /// Releases one reference to this handle. May causes the handle to be returned to the pool if this is the last reference 
        /// pointing to the handle. This method is threadsafe. 
        /// </summary>
        /// <remarks>This method may be called by a base class finalizer! </remarks>
        public override void Release() {
            var newRefCount = System.Threading.Interlocked.Decrement(ref m_referenceCount);
            if (newRefCount == 0 && handle != NativeHostHandle.EXTERNAL_HANDLE) { // <-- hack to prevent EXTERNAL native handles to end up in the pool

                System.Diagnostics.Debug.Assert(m_referenceCount == 0);
#if DEBUG_TRACK_DATA
                Trace.WriteLine($"({System.Threading.Thread.CurrentThread.ManagedThreadId}) caching memory handle (native): {ID.ToInt64()}");
#endif

                this.Cache(Device.MemoryPool);  // only external handles may not set Device properly! 

            }
            if (newRefCount < 0) {
                Debugger.Break();
            }
        }

        public string ShortDisplay() {
            return $"Length: {Length} Ref:{m_referenceCount} (ready - " + m_alignedPointer.ToString() + ")";
        }
        /// <summary>
        /// Release the handles resource, neglecting reference counting. This releases the memory back to the OS.
        /// </summary>
        protected unsafe override void ReleaseResource(IntPtr handle) {
            // On .NET Framework this will run in a prepared CER when called from finalizer

#if TRACK_NATIVE_ALLOC_BYTES
            //m_trackingPool?.InformHandleReleased(this);
            System.Diagnostics.Trace.WriteLine($"NativeHostHandle releasing/length: {handle}/{Length}.");
#endif
#if DEBUG_DISABLED
            System.Diagnostics.Trace.WriteLineIf(Length == new UIntPtr(360448), 
                $"NativeHostHandle releasing[dec/hex]/length: [{handle.ToString()}/{handle.ToString("X8")}]/{Length}.");
            System.IO.File.AppendAllText($"D:\\dumps\\localfree.txt", $"Release: {handle}\r\n");
#endif 
            if (handle != IntPtr.Zero) {
#if DEBUG_TRACK_HANDLES
                System.Diagnostics.Trace.WriteLine($"Entering ReleaseResource for {this.GetType().Name}: {handle}.");
#endif
#if DEBUG_TRACK_DATA
                Trace.WriteLine($"({System.Threading.Thread.CurrentThread.ManagedThreadId}) deallocating memory handle (native): {m_alignedPointer.ToInt64()} (handle: {handle.ToInt64()})");
#endif

                //Native.NativeMethods.LocalFree(handle);
                // edit v6: use Marshal always.
                System.Diagnostics.Debug.Assert(m_allocatedLength > 0);
                System.Runtime.InteropServices.Marshal.FreeHGlobal(handle);
                handle = IntPtr.Zero; 
                Device.MemoryPool.UnRegisterAllocatedBytes((long)m_allocatedLength); 

#if DEBUG_TRACK_HANDLES
                System.Threading.Interlocked.Decrement(ref m_objectsCount);
                handles.Remove(GetHashCode(), out string _); 
#endif

            }
        }

        public override MemoryHandle Clone() {
            var arr = DeviceManagement.DeviceManager.GetDevice(0).MemoryPool.New(Length);
            CopyTo(0, arr, 0, 0, (IntPtr)(long)Length);
            return arr; 
        }

        public unsafe override void Clear() {
            if (Pointer == IntPtr.Zero) {
                return; 
            }

            long tbcleared8 = (long)Length.ToUInt64() / 8;
            long* pl = (long*)Pointer;
            long i = 0;
            while (i < tbcleared8 - 4) { // carefull with silent underruns! 
                pl[0] = 0;
                pl[1] = 0;
                pl[2] = 0;
                pl[3] = 0;
                pl += 4; i += 4;
            }
            while (i++ < tbcleared8) {
                *(pl++) = 0;
            }
        }

    }
}
