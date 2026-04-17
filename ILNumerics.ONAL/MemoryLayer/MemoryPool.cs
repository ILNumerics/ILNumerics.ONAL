using ILNumerics.Core.DeviceManagement;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using KEYTYPE = System.UIntPtr;  // corresponds to size_t

namespace ILNumerics.Core.MemoryLayer {

    /// <summary>
    /// The memory pool base class is a quantizing memory pool for fast (re-)allocating chunks of memory. 
    /// </summary>
    
    public unsafe abstract class MemoryPool<HandleT> : IMemoryPool where HandleT : MemoryHandle {

        #region attributes

        #region quantize helpers

        // from: https://stackoverflow.com/questions/11376288/fast-computing-of-log2-for-64-bit-integers
        static readonly short[] tab64 = new short[64] {
            63,  0, 58,  1, 59, 47, 53,  2,
            60, 39, 48, 27, 54, 33, 42,  3,
            61, 51, 37, 40, 49, 18, 28, 20,
            55, 30, 34, 11, 43, 14, 22,  4,
            62, 57, 46, 52, 38, 26, 32, 41,
            50, 36, 17, 19, 29, 10, 13, 21,
            56, 45, 25, 31, 35, 16,  9, 12,
            44, 24, 15,  8, 23,  7,  6,  5};

        #endregion

        internal ConcurrentStack<HandleT>[] m_pool;
        //internal Queue<KEYTYPE> m_history; 
        private object m_synch = new object();
        /// <summary>
        /// The device this memory pool belongs to. 
        /// </summary>
        protected readonly Device m_device;
        /// <summary>
        /// Number of bytes currently hold available in this memory pool.
        /// </summary>
        private long m_size;
        /// <summary>
        /// Preferred upper limit on number of bytes the process ought to allocate / use at the same time. This is not a hard limit! 
        /// </summary>
        private long m_maxSize;
        private double m_increaseFactor = .1;
        private uint m_shrinkCount = 0;
        private uint m_oomCount = 0;
        /// <summary>
        /// Number of bytes currently allocated from the OS VM manager. 
        /// </summary>
        private long m_allocatedBytes = 0;
        private AutoResetEvent m_allocSignal = new AutoResetEvent(false);
        /// <summary>
        /// Internal use
        /// </summary>
        protected readonly MemoryTypes m_memoryType;
        /// <summary>
        /// Internal use
        /// </summary>
        protected readonly int m_threadID;

        // absolute number of possible array memory region lengths (bytes)   
        private static readonly int BINCOUNT = IntPtr.Size == 8 ? 472 : 216;
        internal static readonly KEYTYPE[] s_binLengths = new KEYTYPE[BINCOUNT];
        // process wide list of all pools created so far. This is accessed for shrinking and reporting purposes only. 
        /// <summary>
        /// Internal use
        /// </summary>
        protected static readonly List<IMemoryPool> s_processPools = new List<IMemoryPool>();

        #endregion

        #region constructors
        internal MemoryPool(Device device, MemoryTypes memoryType) {

            initBinLengthTable();
            m_pool = new ConcurrentStack<HandleT>[BINCOUNT];
            //m_history = new Queue<KEYTYPE>(); 
            m_size = 0;
#if PERFORMANCE_COUNTERS
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows)) {
                PerformanceCountersWindows.SetMemoryPoolSizeMBDev0(m_size >> 20);
            }
#endif
            m_maxSize = initMaxSize(device);
            //Console.Write(m_maxSize); 
#if PERFORMANCE_COUNTERS
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows)) {
                PerformanceCountersWindows.SetMemoryPoolMaxSizeMBDev0(m_maxSize >> 20);
            }
#endif
            m_device = device;
            m_shrinkCount = 0;
            m_oomCount = 0;
            m_memoryType = memoryType;
            m_threadID = Thread.CurrentThread.ManagedThreadId;

            lock (s_processPools) {
                s_processPools.Add(this);
            }

        }

        #endregion

        #region properties

        /// <summary>
        /// The managed thread ID of the thread this pool was created by/for. 
        /// </summary>
        public int ThreadID {
            get { return m_threadID; }
        }

        /// <summary>
        /// The memory type managed by this pool [readonly]. 
        /// </summary>
        public MemoryTypes MemoryType {
            get { return m_memoryType; }
        }

        /// <summary>
        /// Number of shrinking operations performed by the pool so far. 
        /// </summary>
        /// <remarks><para>A high number of shrinking operations indicate an suboptimal memory 
        /// configuration for the current problem size / system memory. In such a situation 
        /// (at least some) memory chunks are not optimally reused and the performance 
        /// of the application may be low. </para>
        /// <para>During the run of the algorithm the pool will try to adopt itself to 
        /// the required memory allocation pattern. This means the pool is shrinked and expanded 
        /// as required dynamically. </para>
        /// </remarks>
        /// <seealso cref="MaxSize"/>
        /// <seealso cref="OOMCount"/>
        public uint ShrinkCount {
            // <para>Shrinking is performed when an available memory chunk is too big 
            // to be stored in the pool _additionally_ to other chunks existing in the pool. If during the run of the algorithm the pool keeps shrinking / releasing older objects
            // try to increase the pools <see cref="MaxSize"/> property to give the pool more room for 
            // storing more memory chunks.</para>
            get { return m_shrinkCount; }
        }


        /// <summary>
        /// Number of OOMs catched (and handled) during allocation requests.
        /// </summary>
        /// <remarks><para>A high number of OOM exceptions caught during the run of the algorithms may 
        /// indicate insufficient memory on your system for the particular problem size. Try to limit the 
        /// memory used by other processes and /or threads and to increase the memory available to the thread 
        /// this pool is associated with.</para>
        /// <para>Try to increase the physical RAM available on your system.</para>
        /// <para>OOM exceptions cause the memory pool to decrease its overall storage limit so that 
        /// fewer objects are kept alive by the pool.</para>
        /// </remarks>
        /// <seealso cref="MaxSize"/>
        /// <seealso cref="ShrinkCount"/>
        public uint OOMCount {
            get { return m_oomCount; }
        }

        /// <summary>
        /// A hard limit of a number of bytes the memory pool is able to hold. 
        /// </summary>
        /// <remarks><para>The memory pool limits the amount of memory hold alive to the number of bytes determined 
        /// by <see cref="MaxSize"/>. No memory chunk greater than <see cref="MaxSize"/> can be stored in the pool. </para></remarks>
        public long MaxSize {
            get { return m_maxSize; }
            set {
                if (value != m_maxSize) {
                    //if (m_threadID != Thread.CurrentThread.ManagedThreadId) {
                    //    throw new InvalidOperationException("The memory pool can only be changed by the same thread who created it!");
                    //}
                    m_maxSize = value;
#if PERFORMANCE_COUNTERS
                    if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows)) {
                        PerformanceCountersWindows.SetMemoryPoolMaxSizeMBDev0(m_maxSize >> 20);
                    }
#endif
                    Shrink(m_maxSize);
                }
            }
        }

        /// <summary>
        /// The sum in bytes of all objects currently hold in the pool.
        /// </summary>
        public ulong Size { get { return (ulong)m_size; } }

        /// <summary>
        /// The number of objects currently stored in this pool.
        /// </summary>
        /// <remarks><para>In order to acquire the object count 
        /// the pool iterates its objects which may introduces a significant cost. </para>
        /// <para>This API is not thread safe! Make sure no other threads interfere 
        /// during the execution of this method.</para></remarks>
        public ulong Count {
            get {
                return countObj();
            }
        }
        /// <summary>
        /// The device this pool is associated with. 
        /// </summary>
        public Device Device {
            get { return m_device; }
        }
        #endregion

        #region public API

        internal void RegisterAllocatedBytes(long size) {
            Interlocked.Add(ref m_allocatedBytes, size);
            // Note: this had been used for blocking the main thread from allocating further segments. 
            // But such attempt fails to handle "regular" high memory algorithms, where the memory 
            // pressure is not caused by (pending) segments. Their memory consumption will not 'heal'
            // the pressure. THerefore, we chose a new strategy: 
            // Handling memory pressure is left to OOM states. They will do both: trying to recover from
            // OOM and to decrease the number of concurrent segments. In the hope that this is going to be 
            // (more) robust ... 
            // 
//            lock (m_synch) {
//                var oldState = m_allocatedBytes >= m_maxSize; 
//                m_allocatedBytes += size;
//                var newState = m_allocatedBytes >= m_maxSize;

//                if (!oldState && newState) {
//                    this.Device.InformHighMemoryDemand(true);
//                }
//#if PERFORMANCE_COUNTERS
//                if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows)) {
//                    PerformanceCountersWindows.SetMemoryAllocatedMBytes(m_allocatedBytes >> 20); 
//                }
//#endif
//            }

        }
        internal void UnRegisterAllocatedBytes(long size) {
            Interlocked.Add(ref m_allocatedBytes, -size);
//            lock (m_synch) {
//                var oldState = m_allocatedBytes >= m_maxSize;
//                m_allocatedBytes -= size;
//                var newState = m_allocatedBytes >= m_maxSize;

//                if (oldState && !newState) {
//                    this.Device.InformHighMemoryDemand(false);
//                }

//#if PERFORMANCE_COUNTERS
//                if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows)) {
//                    PerformanceCountersWindows.SetMemoryAllocatedMBytes(m_allocatedBytes >> 20);
//                }
//#endif
//            }
        }
        /// <summary>
        /// The maximum lengths of chunks of memory for efficient processing on multiple threads. 
        /// </summary>
        /// <returns>All allowed memory chunk lengths in bytes.</returns>
        public static IEnumerable<UIntPtr> GetBinLengths() {
            // Actually: returns an enumeration of all lengths for memory chunks in bytes that the memory pool is able to manage (allocate). 
            // <returns>All allowed memory chunk lengths in bytes.</returns>
            return (IEnumerable<UIntPtr>)s_binLengths.Clone();
        }

        /// <summary>
        /// Request a memory chunk of at least length <paramref name="length"/>. 
        /// </summary>
        /// <param name="length">Minimal length requested in bytes.</param>
        /// <param name="init">[Optional] True: clear memory before return (zeros / 0 values); False (default): leave old values in memory set.</param>
        /// <param name="policy">[Optional] Policy for handling new allocations. Regular (default) or segment allocation (limited).</param>
        /// <returns><see cref="MemoryHandle"/> to the memory chunk of (at least) the requested length.</returns>
        /// <remarks><para>The memory will be taken from the pool if a matching chunk is available in the pool. Otherwise, 
        /// a new chunk of memory is allocated from the <see cref="Device"/>'s memory allocator.</para>
        /// <para>'Matching' chunks are those chunks of memory stored in the pool which have the exact 
        /// <paramref name="length"/> requested or a higher length. The 
        /// <see cref="MemoryHandle.Length"/> property informs about the length of the region the returned pointer refers to.</para>
        /// <para>During the retrieval / allocation of the requested memory chunk this function blocks other threads 
        /// from modifying the pools state (lockfree, though).</para>
        /// </remarks> 
        /// <exception cref="OutOfMemoryException"> if no matching memory chunk was found in the pool and no memory of the requested length could be allocated from device memory.</exception>
        public HandleT New(ulong length, bool init = false, PoolSizePolicy policy = PoolSizePolicy.Regular) {
            // update 2021-03-06: delegated to KEYTYPE (long) version, for simplicity (not because 
            // I think that UIntPtr is better suited here!)
            // <para>This function does not <i><see cref="MemoryHandle.Retain(bool)"/></i> the handle returned! Its internal reference count is 0 !</para>
            return New(new UIntPtr(length), init, policy); 
        }

        /// <summary>
        /// Request a memory chunk of length <paramref name="length"/>. 
        /// </summary>
        /// <param name="length">Length requested in bytes.</param>
        /// <param name="init">[Optional] True: returned cleared memory (zeros, 0 values); False (default): do not clear the memory.</param>
        /// <param name="policy">[Optional] Policy for handling new allocations. Regular (default) or segment allocation.</param>
        /// <returns><see cref="IntPtr"/> to the memory chunk.</returns>
        /// <remarks><para>The memory will be taken from the pool if a matching chunk is available in the pool. Otherwise, 
        /// a new chunk of memory is allocated from the <see cref="Device"/>'s memory allocator.</para>
        /// <para>'Matching' chunks are those chunks of memory stored in the pool which have the exact 
        /// <paramref name="length"/> requested or a greater length, within a certain interval. The <see cref="MemoryHandle.Length"/> property
        /// informs about the actual allocated length of the allocated memory chunk the returned pointer refers to.</para>
        /// <para>During the retrieval / allocation of the requested memory chunk this function blocks other threads from modifying the pools state.</para>
        /// <para>The reference count of the handle returned will be set to 0!</para>
        /// </remarks> 
        /// <exception cref="OutOfMemoryException"> if no matching memory chunk was found in the pool and no memory of the requested length could be allocated from device memory.</exception>
        
        public HandleT New(KEYTYPE length, bool init = false, PoolSizePolicy policy = PoolSizePolicy.Regular) {
#if THREAD_LOCAL_MEMORY_POOL
            System.Diagnostics.Debug.Assert(m_threadID == Thread.CurrentThread.ManagedThreadId, "Invalid attempt to access the memory pool: this method must be called from the same thread which was used to create the pool. Cross thread access is not allowed for this memory pool!");
#endif 
            HandleT ret = null;
            var ulen = length.ToUInt64();
            
            var bin = FindBin(ulen);

            var entry = m_pool[bin];
            // quantize the length
            length = s_binLengths[bin];
            ulen = length.ToUInt64();

            if (entry?.TryPop(out ret) ?? false) {
                // retrieval from pool successfull
                Interlocked.Add(ref m_size, -(long)ulen);
#if PERFORMANCE_COUNTERS
                if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows)) {
                    PerformanceCountersWindows.SetMemoryPoolSizeMBDev0(m_size >> 20);
                }
#endif
            } else {

                HandleT AllocateRegular() {

                    // regular (non-segment) allocation: 

                    int curGCCount = GC.CollectionCount(2);
                    try {
                        // outLength is set to the (next bigger) length bin

                        ret = AllocateInternal(length);
                        
                    } catch (OutOfMemoryException) {

                        m_oomCount++;  // sloppy but enough here(?)

                        // Recover
                        // =======
                        // this OOM exception was thrown by the Marshal class: no guarantee that the GC was triggered!
                        if (GC.CollectionCount(2) == curGCCount) // don't even expect this to be threadsafe...!
                            GC.Collect();

                        // The GC tried to free some garbage. It called 
                        // the finalizers on Storage which may reclaimes CountableArray and releases their IntPtr
                        // back to the OS. 
                        // In order for this memory to show up and be allocatable again we must wait until the 
                        // finalizers are done and try again. 

                        // if we are running in the debugger we cannot allow this to happen in the ImmediatWindow
                        // since there will be no finalizer threads running and waiting for them would 
                        // leave us in the endless dark of eternity. 
                        // System.Diagnostics.Debugger.NotifyOfCrossThreadDependency();
                        if (!tryDetectIfRunningInDebugEvaluation()) {
                            GC.WaitForPendingFinalizers();
                        } else {
                            // poor mans waiting
                            Thread.Sleep(1000);
                        }

                        // retry
                        try {

                            ret = AllocateInternal(length);

                        } catch (OutOfMemoryException) {

                            // if nothing helps: clear this pool 
#if DEBUG
                            System.Diagnostics.Trace.WriteLine($"MemoryPool: Shrinking memory pool(s)...");
#endif
                            Shrink_threadLocal(0);

                            GC.Collect();
                            if (!tryDetectIfRunningInDebugEvaluation()) {
#if DEBUG
                                System.Diagnostics.Trace.WriteLine($"MemoryPool: waiting for finalizers cleaning up memory handles ...");
#endif
                                GC.WaitForPendingFinalizers();
                            } else {
                                // poor mans waiting
                                Thread.Sleep(500);
                            }

                            // if we run into OOMs the pools size is decreased subsequently
                            // TODO: define, check for and inform about when a lower limit for m_maxSize was reached! 
                            m_maxSize = (long)(m_maxSize * (1 - m_increaseFactor));
#if PERFORMANCE_COUNTERS
                            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows)) {
                                PerformanceCountersWindows.SetMemoryPoolMaxSizeMBDev0(m_maxSize >> 20);
                            }
#endif

                            //// retry  EDIT: disabled, while using a global pool in 7.0 
                            //try {
                            //    ret = AllocateInternal(length);

                            //} catch (OutOfMemoryException) {
                            //    // if nothing helps: clear _all_ pools 
                            //    DeviceManager.GetDevice(0).ShrinkAllPoolsBut(this);
                            //}

                            // last safty net: after all pools and all contexts have been cleared, 
                            // it will be only the finalizer freeing the associated objects. 
                            try {
                                ret = AllocateInternal(length);
                            } catch (OutOfMemoryException) {
                                var then = DateTime.Now.AddSeconds(2);
                                while (DateTime.Now < then) {
                                    GC.Collect();
                                    if (!tryDetectIfRunningInDebugEvaluation()) {
                                        GC.WaitForPendingFinalizers();
                                    } else {
                                        // poor mans waiting
                                        Thread.Sleep(100);
                                    }
                                    ret = AllocateInternal(length);
                                    if (ret != null) {
                                        return ret;
                                    }
                                }
                                throw;
                            }
                        }
                    }
                    return ret;
                }

                if (policy == PoolSizePolicy.Segment) {

                    // pseudo: 
                    // 1) if found in pool : end :   
                    // 2) else if within allocation limits: end : 
                    // 3) else if segments active : wait for completion signal -> repeat 1 : 
                    // 4) else => regular New 

                    while (ret == null) { // m_allocatedBytes > m_maxSize - (long)ulen && Device.GetLiveSegments() > 0) {

                        if (entry?.TryPop(out ret) ?? false) {
                            // retrieval from pool successfull
                            Interlocked.Add(ref m_size, -(long)ulen);
#if PERFORMANCE_COUNTERS
                            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows)) {
                                PerformanceCountersWindows.SetMemoryPoolSizeMBDev0(m_size >> 20);
                            }
#endif
                        } else {

                            // When an allocation exceeds the planned memory size (as defined by MaxSize) 
                            // we allow the additional allocation but prevent from starting further (asynchronous) tasks. 
                            // The corresponding check is done in AllocateInternal / RegisterAllocatedBytes. 
                            // EDIT: removed certain checks here ... if (AwaiterBase.LiveAwaitersCountInfo > (HostDevice.IsMainThread ? 0 : 1)) ... 
                            ret = AllocateRegular();

                        } 
                    }

                } else {

                    // out-of-segment memory allocation
                    ret = AllocateRegular();

                }
                if (ret == null) {
                    // further OOMs can be catched by the user
                    try {
                        ret = AllocateInternal(length);
                    } catch (OutOfMemoryException exc) {
                        string msg = $"Out of memory requesting length: {length}" + Environment.NewLine;
                        msg += $@"Running in immediate evaluation mode (VS): {tryDetectIfRunningInDebugEvaluation()}. Current scope: {Scope.Context.CurScope}. Current scope array count: {Scope.Context.CurArray}. 
Device: {Device.ToString()}. {this.ToString()}";
                        throw new OutOfMemoryException(msg, exc);
                    }
                }
            }
            // clear / init bytes. TODO: there might be faster ways to do this...? shall we do it in parallel?
            if (init) { // && ret is NativeHostHandle && ret != null) { // && ret.Pointer != IntPtr.Zero) {
                ret?.Clear();
            }
#if DEBUG_DISABLED
            if (ret is NativeHostHandle)
                System.IO.File.AppendAllText($"D:\\dumps\\localfree.txt", $"Reused: {(ret as NativeHostHandle).Handle}\r\n");
#endif
            ret.m_referenceCount = 0;  // ugly hack, in case that a handle made its way into the pool or even was modified (again) afterwards. 
#if DEBUG_TRACK_DATA
            Trace.WriteLine($"({System.Threading.Thread.CurrentThread.ManagedThreadId}) memory handle reclaimed: {ret.ID.ToInt64()}");
#endif

            return ret;
        }
        protected string GetStackTraceShort() {
            var st = new StackTrace();
            return string.Join(" < ", st.GetFrames().Select(sf => sf.GetMethod().Name));
        }

        /// <summary>
        /// Attempt to determine if the function was called from the Immediate Window of from some 
        /// other non-standard debug places. Helps to prevent from death lock when waiting on non-running finalizer thread. 
        /// </summary>
        /// <returns>true if this thread is probably the only debugger thread running. False otherwise.</returns>
        /// <remarks>If this returns false this does not mean that the finalizer is really running / runnable! It is barely 
        /// more than a hint and should be taken as such.</remarks>
        private bool tryDetectIfRunningInDebugEvaluation() {
            if (!System.Diagnostics.Debugger.IsAttached) {
                return false;
            }
            int runningThreads = 0;
            foreach (ProcessThread t in Process.GetCurrentProcess().Threads) {

                if (t.ThreadState == System.Diagnostics.ThreadState.Running)
                    runningThreads++;
            }
            return runningThreads < 2;
        }

        /// <summary>
        /// Returns a chunk of memory back to the pool.
        /// </summary>
        /// <param name="handle"><typeparamref name="HandleT"/> refering to the memory chunk.</param>
        /// <remarks><para>The pool will try to hold on to the memory pointed to by <paramref name="handle"/>. If 
        /// the handles memory length is too large and will not 'fit' into the pool (see: <see cref="MaxSize"/>) it will be 
        /// released back to the <see cref="Device"/> memory, disposing the handle.</para>
        /// <para>If the handles size is smaller than <see cref="MaxSize"/> but other objects currently stored 
        /// in the pool prevent to store <paramref name="handle"/> without exceeding <see cref="MaxSize"/> at least some of those  
        /// handles will be released until there is enough free space in the pool to fit the new handle into the pool. 
        /// This process (shrinking) may also involves the modification of the <see cref="MaxSize"/> property of the pool.</para></remarks>

        public virtual void Free(HandleT handle) {

#if DEBUG
#if THREAD_LOCAL_MEMORY_POOL
            Debug.Assert(m_threadID == Thread.CurrentThread.ManagedThreadId, "Invalid attempt to access the memory pool: this method must be called from the same thread which was used to create the pool. Cross thread access is not allowed for this memory pool!");
#endif
            if (this is NativeHostPool) {
                Debug.Assert(handle is NativeHostHandle, $"Invalid type of handle returned to NativeHostPool: {handle.GetType().Name}.");
            } else if (this.GetType().Name.StartsWith("ManagedHostPool")) {
                Debug.Assert(handle.GetType().Name.StartsWith("ManagedHostHandle"));
            }
#if DEBUG_DISABLED
            if (handle is NativeHostHandle)
            System.IO.File.AppendAllText($"D:\\dumps\\localfree.txt", $"Cached: {handle.Pointer} by {Environment.StackTrace}\r\n");
#endif

#endif
            if (handle == null || handle.Length == KEYTYPE.Zero) return;
            var ulength = handle.Length.ToUInt64();

            // any chance at all to store this into the pool? 
            if ((long)ulength > m_maxSize) {

                handle?.Dispose();
                return;

            }

            if ((long)ulength + m_size > m_maxSize) {

                // make room by increasing the pool + cleaning older objects
                m_maxSize = (long)(m_maxSize * (1 + m_increaseFactor));
#if PERFORMANCE_COUNTERS
                if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows)) {
                    PerformanceCountersWindows.SetMemoryPoolMaxSizeMBDev0(m_maxSize >> 20);
                }
#endif

                Shrink_threadLocal(m_maxSize - (long)ulength);
            }

            // put 
            int bin = FindBin(ulength);

            while (m_pool[bin] == null) {
                Interlocked.CompareExchange<ConcurrentStack<HandleT>>(ref m_pool[bin], new ConcurrentStack<HandleT>(), null);
            }
            m_pool[bin].Push(handle);
#if DEBUG_TRACK_DATA
            Trace.WriteLine($"({System.Threading.Thread.CurrentThread.ManagedThreadId}) memory handle cached: {handle.ID.ToInt64()}");
#endif

            //System.Diagnostics.Debug.WriteLineIf(this is NativeHostPool, $"NativeHostPool:stored {handle.Length.ToUInt64()} bytes from {(handle as NativeHostHandle).Handle}.");
            Interlocked.Add(ref m_size, (long)ulength);
#if PERFORMANCE_COUNTERS
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows)) {
                PerformanceCountersWindows.SetMemoryPoolSizeMBDev0(m_size >> 20);
            }
#endif

        }

        /// <summary>
        /// Shrinks the pool down to <paramref name="shrinkedSize"/> by releasing old objects.
        /// </summary>
        /// <param name="shrinkedSize">The target size in bytes of the pool. The pool will be shrinked, until its 
        /// size drops below this value.</param>
        public void Shrink(long shrinkedSize) {
            Shrink_threadLocal(shrinkedSize);
        }

        /// <summary>
        /// map lengths to bins
        /// </summary>
        /// <param name="l">length of requested memory region</param>
        /// <returns>the index of the bin to look for the requested memory region</returns>
        public static int FindBin(ulong l) {
            if (l < 128) return (l == 0) ? 0 : (int)(l - 1) >> 3;
            int g = log2(--l);  // implicit floor due to int conversion; floor(log2(l))
            uint j = (uint)(l >> (g - 3)) & 7;
            return (int)((g - 5) * 8 + j);
        }

        /// <summary>
        /// Produces statistical information about this memory pool instance for informal messages.
        /// </summary>
        /// <returns>Informal string.</returns>
        public override string ToString() {
            int used = 0;
            long bytes = 0;
            for (int i = 0; i < m_pool.Length; i++) {
                if (m_pool[i] != null && m_pool[i].Count != 0) {
                    used++;
                    var bin = m_pool[i];
                    bytes += bin.Sum(b => (long)b.Length.ToUInt64());
                }
            }
            return $"MemoryPool: {used} of {m_pool.Length} bins store {bytes} bytes. {OOMCount} OOMs handled. Pool shrinked {m_shrinkCount} times.";

        }
#endregion

#region private helper
        private static void initBinLengthTable() {
            int i = 0;
            for (; i < 15; i++) {
                s_binLengths[i] = new KEYTYPE((ulong)(i + 1) * 8);
            }
            var maxNrBits = IntPtr.Size * 8;  // make sure UIntPtr does not overflow on 32 bit!

            System.Diagnostics.Debug.Assert(s_binLengths != null && s_binLengths.Length == BINCOUNT);

            for (int b = 7; b < maxNrBits; b++) {
                for (int s = 0; s < 8; s++) {
                    s_binLengths[i++] = new KEYTYPE((ulong)((ulong)(8 + s) << (b - 3)));
                }
            }
            s_binLengths[i] = new UIntPtr(IntPtr.Size == 8 ? unchecked((ulong)-1) : unchecked((uint)-1));
        }

        static int log2(ulong value) {
            value |= value >> 1;
            value |= value >> 2;
            value |= value >> 4;
            value |= value >> 8;
            value |= value >> 16;
            value |= value >> 32;
            return tab64[(((value - (value >> 1)) * 0x07EDD5E59A4E28C2)) >> 58];
        }
        /// <summary>
        /// perform the shrinking - to be called from single thread only!
        /// </summary>
        /// <param name="shrinkedSize">ignored</param>
        private void Shrink_threadLocal(long shrinkedSize) {

            // TODO: consider fast path: if shrinkedSize == 0 (!OOM condition) -> simply clear all !
#if PERFORMANCE_COUNTERS
            try {
#endif


                m_shrinkCount++;
                for (int i = 0; i < BINCOUNT / 2;) {   // BINCOUNT is either 472 or 216
                    if (!ClearStack(m_pool[i]) ||
                        !ClearStack(m_pool[BINCOUNT - ++i])) {
                        return;
                    }
                }

                bool ClearStack(ConcurrentStack<HandleT> stack) {
                    while (stack?.Count > 0) {

                        if (stack.TryPop(out HandleT dummy)) {

                            Interlocked.Add(ref m_size, -(long)dummy.Length);
                            dummy?.Dispose();  // this does correctly track the allocated bytes in the memory pool. 
                            if (m_size <= shrinkedSize)
                                return false;
                        }
                    }
                    return true;
                }
#if PERFORMANCE_COUNTERS
            } finally {
                if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows)) {
                    PerformanceCountersWindows.SetMemoryPoolSizeMBDev0(m_size >> 20);
                }
            }
#endif

        }

        private ulong countObj() {
            ulong ret = 0;
            foreach (var item in m_pool) {
                if (item != null) {
                    ret = ret + (ulong)item.Count;
                }
            }
            return ret;
        }

        private long initMaxSize(Device device) {
            return device.GetMaxPoolSizeHint();
        }
        //private void Put(HandleT handle, KEYTYPE length) {
        //    ulong ulength = length.ToUInt64();
        //    int bin = FindBin(ulength);

        //    if (m_pool[bin] == null) {
        //        m_pool[bin] = new Stack<HandleT>();
        //    }
        //    m_pool[bin].Push(handle);
        //    //m_history.Enqueue(length); 
        //}

#endregion

#region abstract interface

        /// <summary>
        /// Allocates a new block of memory / resource according to <paramref name="length"/>. 
        /// </summary>
        /// <param name="length">Length of the new memory region in bytes.</param>
        /// <returns><see cref="IntPtr"/> handle to the newly allocated region.</returns>
        /// <remarks><para>The handle returned should be a valid handle. In case of failure throw <see cref="OutOfMemoryException"/> or another, more adequate exception.</para></remarks>
        
        internal abstract HandleT AllocateInternal(UIntPtr length);

#region obsolete 

        // this is now obsolete: we are using critical handles and release the handle itself. 

        ///// <summary>
        ///// Free the memory region pointed to by <paramref name="handle"/>. 
        ///// </summary>
        ///// <param name="handle">Handle referencing the memory region to be freed.</param>
        ///// <remarks>Error handling: users implementing this method must decide whether to throw an appropriate exception on failures 
        ///// freeing the resource (which is not catched by memory pool, hence may lead to application shutdown) or if it would be better 
        ///// to ignore the failure and allow the app to continue.</remarks>
        //
        //internal abstract void ReleaseInternal(IntPtr handle);
#endregion

#endregion
    }

}
