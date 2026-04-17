using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.MemoryLayer {

    /// <summary>
    /// A handle for managed reference type arrays. This class enables one to handle /store managed arrays with the same API as unmanaged memory. 
    /// </summary>
    /// <remarks>Since most computational arrays in ILNumerics host struct (value type) elements, managed heap memory is rarely used. 
    /// One important example is are cell arrays, which hold reference types as elements. These element arrays are 
    /// allocated on the managed heap by use of <see cref="ManagedHostHandle{BaseArray}"/>.</remarks>
    /// <typeparam name="T">The type of references hold in the internal <see cref="HostArray"/>.</typeparam>
    public sealed class ManagedHostHandle<T> : MemoryHandle {

        internal readonly T[] HostArray;

        GCHandle m_lockGcHandle;

        /// <summary>
        /// Create a new managed memory handle on the host, capable of storing <paramref name="length">_elements_</paramref> of type <typeparamref name="T"/>. 
        /// </summary>
        /// <param name="length">The number of <i>elements</i> (not bytes) of the new managed array.</param>
        /// <param name="clear">[Optional] This parameter is ignored.</param>
        /// <remarks><para>The <paramref name="clear"/> parameter has no effect. The new handle will be acquired by creating 
        /// a new array of <typeparamref name="T"/> from the managed heap. The CLR will ensure that its elements are 
        /// cleared.</para>
        /// <para>The new handle returned will host <paramref name="length"/> elements. However, the <see cref="MemoryHandle.Length"/> property 
        /// of the new handle will reflect the <i>number of bytes</i> consumed by the array to store these elements.</para></remarks>
        public ManagedHostHandle(UIntPtr length, bool clear = false) : base() {
            GC.SuppressFinalize(this);
            HostArray = new T[length.ToUInt64()];
            // if length is > uint.maxvalue we will not get until here...

            // Length in bytes
            Length = new UIntPtr(length.ToUInt32() * (ulong)IntPtr.Size); // Length is used to re-find in bins later! Do consider the workload size only! 
        }
        /// <summary>
        /// Create a managed host handle based on an existing managed <see cref="System.Array"/>. 
        /// </summary>
        /// <param name="source">The preallocated <see cref="System.Array"/>.</param>
        public ManagedHostHandle(T[] source) : base() {
            GC.SuppressFinalize(this);
            HostArray = source;
            // if length is > uint.maxvalue we will not get until here...

            // Length in bytes
            uint length = (uint)(source == null ? 0 : source.Length);
            Length = new UIntPtr(length * (ulong)IntPtr.Size);  // Length is used to re-find in bins later! Do consider the workload size only! 
        }

        /// <summary>
        /// Determine if this handle is a valid handle. Returns false, preventing from finalization.
        /// </summary>
        public override bool IsInvalid {
            get { return false; }
        }

        /// <summary>
        /// Returns an identifier for this managed host handle.
        /// </summary>
        public override IntPtr ID {
            get {
                return new IntPtr(HostArray?.GetHashCode() ?? 0);
            }
        }

        /// <summary>
        /// Release the handle. For managed arrays this is a NOP. 
        /// </summary>
        protected override void ReleaseResource(IntPtr handle) {
            if (m_lockGcHandle.IsAllocated) {
                m_lockGcHandle.Free();
            }
            // nothing else to do. The GC will clean up ...
        }
        internal override void Cache(MemoryPool<MemoryHandle> pool) {
            // the incoming pool will be a native pool. Ignore it and use the ManagedPool corresponding to the T instead! 
            System.Diagnostics.Debug.Assert(pool.MemoryType != MemoryTypes.Managed);
            ManagedHostPool<T>.Pool.Free(this);
        }

        /// <summary>
        /// Acquire a pointer to the first element of the <see cref="HostArray"/>. 
        /// </summary>
        /// <remarks>When requesing the <see cref="Pointer"/> for the first time during the lifetime of the handle
        /// the <see cref="HostArray"/> will be fixed in memory and remain fixed until the handle is disposed.</remarks>
        public override IntPtr Pointer {
            get {
                //if (base.Pointer == IntPtr.Zero && HostArray != null && !m_lockGcHandle.IsAllocated) {
                //    m_lockGcHandle = GCHandle.Alloc(HostArray, GCHandleType.Pinned);
                //    base.Pointer = m_lockGcHandle.AddrOfPinnedObject();
                //    GC.ReRegisterForFinalize(this); 
                //}
                System.Diagnostics.Debug.Assert(base.Pointer == IntPtr.Zero);
                throw new InvalidOperationException("This handle references the managed heap only. Use the HostArray property and pinning in order to acquire a pointer to elements!");
            }
            internal set {
                throw new InvalidOperationException("The 'Pointer' attribute cannot be set on this handle type (ManageHostHandle<T>). Please report this issue to ILNumerics!");
                //base.Pointer = value;
            }
        }

        /// <summary>
        /// Clones &amp; copies elements. 
        /// </summary>
        /// <param name="sourceDevice"></param>
        /// <param name="target">Target handle</param>
        /// <param name="targetDevice"></param>
        /// <param name="start">start</param>
        /// <param name="length">!! Nr of ELEMENTS to copy! This intentionally deviates from the API of the base class!!</param>
        public unsafe override void CopyTo(uint sourceDevice, MemoryHandle target, uint targetDevice, ulong start, IntPtr length) {
            // length is in elements!! 
#if DEBUG
            if (target as ManagedHostHandle<T> == null) {
                System.Diagnostics.Debug.WriteLine($"ManagedHostHandle.CopyTo: expected memory handle (target) type: ManagedHostHandle<T>. Found: {target.GetType().FullName}. Stacktrace: {Environment.StackTrace}.");
            }
#endif            
            var destArr = (target as ManagedHostHandle<T>).HostArray;
            var len = length.ToInt32(); 
            System.Diagnostics.Debug.Assert(destArr.Length >= len);
            if (typeof(T) == typeof(IStorage)) {
                // cell storage
                var arr = (this as ManagedHostHandle<IStorage>).HostArray;
                for (int i = 0; i < len; i++) {
                    var clone = arr[i + (int)start]?.Clone();
                    destArr[i + (int)start] = (T)clone;
                }
            } else {
                Array.Copy(HostArray, (int)start, destArr, 0, len); 
            }
        }
        public override void Clear() {
            Array.Clear(HostArray, 0, HostArray.Length);
        }
        public unsafe override void Set(MemoryHandle src, ulong srcIdx, ulong destIdx, uint size) {
            HostArray[destIdx] = (src as ManagedHostHandle<T>).HostArray[srcIdx];
        }
        public override MemoryHandle Clone() {
            var arr = ILNumerics.Core.Functions.Builtin.MathInternal.New<T>((ulong)HostArray.Length);
            CopyTo(0, arr, 0, 0, new IntPtr(HostArray.Length * IntPtr.Size));
            return arr; 
        }
    }
}
