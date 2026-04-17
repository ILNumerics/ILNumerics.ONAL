using ILNumerics.Core.DeviceManagement;
using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace ILNumerics.Core.MemoryLayer {

    /// <summary>
    /// <para>Base class for memory resources. All types handling memory in various ways derive 
    /// from <see cref="MemoryHandle"/>. This class either wraps a simple pointer to unmanaged 
    /// memory or a reference to a managed array of various element types. Instances are subject 
    /// of garbage collection (as a fallback only), perform reference counting and are happy to 
    /// be used in a multithreaded consumer / provider scenario.</para>
    /// <para>Typically, <see cref="MemoryHandle"/> is allocated, stored and maintained by a 
    /// corresponding <see cref="MemoryPool{HandleT}"/>.</para>
    /// </summary>
    public abstract class MemoryHandle : CriticalHandle {

        UIntPtr m_length;
        /// <summary>
        /// Gets the number of objects currently referencing this handle. Readonly. 
        /// </summary>

        public int m_referenceCount = 0;  // this is a field so that we can use it in Interlocked (ref) operations. TODO: ... but it should not be 'public', though ... :| !
        Device m_device;

        /// <summary>
        /// The memory pool of the device this handle belongs to. Used for freeing the handle.
        /// </summary>
        public Device Device {
            
            get {
                return m_device;
            }
            protected internal set {
                m_device = value;
            }
        }

        /// <summary>
        /// An identifier for this memory object.
        /// </summary>
        public virtual IntPtr ID {
            get { return base.handle; }
        }


        /// <summary>
        /// The memory address of host (unmanaged heap) memory represented by this memory handle. 
        /// </summary>
        /// <remarks><para>The pointer returned is only valid on host devices and only as long as the current thread 
        /// does not enqueue any new asynchronous operations (involving this same memory handle). Typically, 
        /// you acquire the pointer, immediately perform some (low level) operations on its memory, and - as the program 
        /// proceeds with new array operations - forget about its value.</para></remarks>
        public virtual IntPtr Pointer {
            get {
#if DEBUG
                if (IsInvalid) {
                    throw new InvalidOperationException("Attempt to access an invalid handle!"); 
                }
#endif
                // is the memory ready? Are there other (asynch) operations writing to it? 
                // We consider it safe to only wait for the state to _become_  zero here. 
                // Because, the state is only changed when attempting to enqueue new asynch 
                // operations writing to the memory. And the lifetime of the pointer returned 
                // is limited accordingly.
                return handle;
            }
            internal set {
#if DEBUG
                if (IsInvalid) {
                    throw new InvalidOperationException("Attempt to access an invalid handle!");
                }
#endif

                if (handle != value) {
                    handle = value;
                }
            }
        }
        /// <summary>
        /// Number of bytes referenced by this handle.
        /// </summary>
        public UIntPtr Length {
            get { return m_length; }
            internal set {
                if (m_length != value) {
                    m_length = value;
                }
            }
        }

        /// <summary>
        /// Determine if this handle is a valid handle. Neglects the reference counter.
        /// </summary>
        public override bool IsInvalid {
            get { return handle == NativeHostHandle.INVALID_HANDLE; }
        }


        //public static ConcurrentDictionary<IntPtr, MemoryHandle> KeptAliveHandles { get; } = new ConcurrentDictionary<IntPtr, MemoryHandle>(); 

        /// <summary>
        /// Note: this constructor is called from the CLR in P/Invoke calls. Make sure to fill all required attributes afterwards! 
        /// </summary>
        protected MemoryHandle() : base(NativeHostHandle.INVALID_HANDLE) {
            m_referenceCount = 0;
#if DEBUG_TRACK_HANDLES
            Interlocked.Increment(ref m_objectsCount); 
#endif
        }

        /// <summary>
        /// Register a reference to this handle. This method is threadsafe. 
        /// </summary>
        /// <param name="acquireWriteLockSynchronously">[Optional] Synchronous operations may acquire a write lock based on the number of references currently existing for this buffer. Default: false.</param>
        /// <remarks><para>Note that the write lock acquired by <paramref name="acquireWriteLockSynchronously"/>is only valid on this 
        /// (main) thread and only in synchronous operations on buffer / handles. It is solely based on the number of references to this handle and will not prevent 
        /// asynchronous operations (segments, copies) to access the handle for writing! Such operations rely on other synchronization methods (events). The method will 
        /// fail if <paramref name="acquireWriteLockSynchronously"/> is true but the number of references pointing to this handle is not suited for writing. Expected 
        /// number of references: 2 (one for hosting array, one for caller operation)</para> </remarks>
        /// <exception cref="InvalidOperationException">if a write lock was requested but could not be acquired.</exception>
        public int Retain(bool acquireWriteLockSynchronously = false) {
            int newCount = System.Threading.Interlocked.Increment(ref m_referenceCount);
            if (acquireWriteLockSynchronously) {
                if (newCount != 2) {
                    Interlocked.Decrement(ref m_referenceCount);
                    throw new InvalidOperationException("Unable to acquire a write lock for this memory handle. There are too many / too few references for this object. Make sure that the handle has exactly one reference registered to it before attempting to lock the handle for writing!");
                }
            }
            var orig = handle;
            if (orig == IntPtr.Zero && (this is NativeHostHandle)) {
                throw new InvalidOperationException($"Invalid attempt to retain disposed handle, from: {Environment.StackTrace}.");
            }
            return newCount;
        }

        /// <summary>
        /// Releases one reference to this handle. May causes the handle to be returned to the pool if this is the last reference pointing to the handle. This method is threadsafe. 
        /// </summary>
        /// <remarks>This method may be called by the base class' finalizer! </remarks>
        public virtual void Release() {
            var newRefCount = System.Threading.Interlocked.Decrement(ref m_referenceCount);
            if (newRefCount == 0 && handle != NativeHostHandle.EXTERNAL_HANDLE) { // <-- hack to prevent EXTERNAL native handles to end up in the pool

                System.Diagnostics.Debug.Assert(m_referenceCount == 0);
                //System.Diagnostics.Debug.Assert(IsInvalid);  // ho: why should the handle be invalid here? It remains valid when cached in the pool! 

                this.Cache(Device.MemoryPool);  // only external handles may not set Device properly! 

            }
        }

        /// <summary>
        /// Attempt to release this handle, neglecting any ref counting. This method really gives up the unmanaged resources and is called during finalization of CriticalHandle (among others). 
        /// </summary>
        /// <param name="manual">False when called in finalization.</param>
        /// <remarks><para><see cref="Dispose(bool)"/> is implemented in a way that the <b>first</b> thread executing this 
        /// method is releasing the underlying resource and marks this handle as invalid. Note, that <see cref="ReleaseResource(IntPtr)"/>
        /// is called exactly once (by this first thread only). Care must be taken if the resource is managed by reference 
        /// counting and multiple references exist.</para>
        /// <para>This method is threadsafe.</para>
        /// </remarks>
        protected override void Dispose(bool manual) {

            // we do not use the _closed mechanism of the base class! 

            // CAUTION: This does not check for State being null nor for reference count = 0! Make 
            // sure that the memory handle is actually not referenced anymore before disposing it!

            var orig = handle;
            if (orig == NativeHostHandle.INVALID_HANDLE) {
                return;
            }
            orig = Interlocked.CompareExchange(ref handle, NativeHostHandle.INVALID_HANDLE, orig);

            if (orig == NativeHostHandle.INVALID_HANDLE) {
                // invalid, another thread has disposed this handle in the meantime
                return;
            }
            // we are on the disposing thread. But still, CL buffers might be Release()-d concurrently!!
            // Also, do not release external handles!!
            if (orig != NativeHostHandle.EXTERNAL_HANDLE) {
                ReleaseResource(orig);
            }

            m_length = UIntPtr.Zero;

            if (manual) {
                GC.SuppressFinalize(this);
            }
        }
        /// <summary>
        /// Cache this handle 
        /// </summary>
        /// <param name="pool"></param>
        internal virtual void Cache(MemoryPool<MemoryHandle> pool) {
            System.Diagnostics.Debug.Assert(pool != null);
            pool?.Free(this);
        }

        /// <summary>
        /// Derived, concrete classes implement this method in order to perform actual release of the managed resource hosted on by this handle. 
        /// </summary>
        /// <param name="handle">The handle to give back to the OS / device. This may or may not be the same as the internal pointer stored by this handle!</param>
        protected abstract void ReleaseResource(IntPtr handle);

        protected override bool ReleaseHandle() {
            throw new InvalidOperationException($"This class does not support this method. Use {nameof(ReleaseResource)}(IntPtr) instead!");
        }

        /// <summary>
        /// Copy memory.
        /// </summary>
        /// <param name="sourceDevice"></param>
        /// <param name="target"></param>
        /// <param name="targetDevice"></param>
        /// <param name="start">offset to the src memory address in bytes.</param>
        /// <param name="length">number of bytes to copy. Note: derived types may expect 'nr of elements' here!</param>
        public unsafe virtual void CopyTo(uint sourceDevice, MemoryHandle target, uint targetDevice, ulong start, IntPtr length) {
            //Native.NativeMethods.CopyMemory(target.Pointer, (IntPtr)((byte*)Pointer + start), length);
            System.Buffer.MemoryCopy(
              source: (byte*)Pointer + start,
              destination: (void*)target.Pointer,
              sourceBytesToCopy: (long)length,
              destinationSizeInBytes: (long)length);
        }
        //public unsafe virtual void Clear() {
        //    long tbcleared8 = (long)Length.ToUInt64() / 8;
        //    long* pl = (long*)Pointer;
        //    long i = 0;
        //    while (i < tbcleared8 - 4) { // carefull with silent underruns! 
        //        pl[0] = 0;
        //        pl[1] = 0;
        //        pl[2] = 0;
        //        pl[3] = 0;
        //        pl += 4; i += 4;
        //    }
        //    while (i++ < tbcleared8) {
        //        *(pl++) = 0;
        //    }
        //    // lengths are multiple of 8! (TODO: really? I mean: REALLY??? -> check! )
        //}

        /// <summary>
        /// Clears the memory of this handle, set all bytes to 0.
        /// </summary>
        public abstract void Clear();

        public unsafe virtual void Set(MemoryHandle src, ulong srcIdx, ulong destIdx, uint size) {
            // no checks performed for performance reasons! 
            System.Diagnostics.Debug.Assert(srcIdx < src.Length.ToUInt64() && destIdx < Length.ToUInt64());
            System.Diagnostics.Debug.Assert(srcIdx >= 0 && destIdx >= 0);
            switch (size) {
                case 1:
                    *((byte*)Pointer + destIdx) = *((byte*)src.Pointer + srcIdx);
                    break;
                case 2:
                    *((short*)Pointer + destIdx) = *((short*)src.Pointer + srcIdx);
                    break;
                case 4:
                    *((int*)Pointer + destIdx) = *((int*)src.Pointer + srcIdx);
                    break;
                case 8:
                    *((long*)Pointer + destIdx) = *((long*)src.Pointer + srcIdx);
                    break;
                case 16:
                    *((complex*)Pointer + destIdx) = *((complex*)src.Pointer + srcIdx);
                    break;
                default:
                    for (int i = 0; i < size; i++) {
                        *((byte*)Pointer + destIdx + i) = *((byte*)src.Pointer + srcIdx + i);
                    }
                    break;
            }
        }

        /// <summary>
        /// Returns a new memory handle of the same type with a shallow copy of all elements of this handle. 
        /// </summary>
        /// <returns>MemoryHandle of the same concrete type.</returns>
        public abstract MemoryHandle Clone();

    }
}
