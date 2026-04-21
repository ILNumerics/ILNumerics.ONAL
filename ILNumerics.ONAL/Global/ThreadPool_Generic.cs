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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace ILNumerics.Core.Global {

    /// <summary>
    /// Legacy thread pool. Handles Action[IntPt]. 
    /// </summary>
    public static class ThreadPool {

        private static ThreadPool<IntPtr> s_pool;
        private static object s_lock = new object();

        /// <summary>
        /// Get the ThreadPool associated with the current thread.
        /// </summary>
        internal static ThreadPool<IntPtr> Pool {
            get {
                if (s_pool == null) {
                    // Caution! Race condition: awaiter threads may create a pool with 0 worker threads
                    Interlocked.CompareExchange(ref s_pool, new ThreadPool<IntPtr>(Math.Max(Settings.MaxNumberThreads, 1) - 1), null); 

                }
                return s_pool;
            }
        }

        public static bool QueueUserWorkItem(uint id, Action<IntPtr> action, IntPtr data) {
            return Pool.QueueUserWorkItem(id, action, data);
        }
        public static int Wait4Workers(ref int workerCount) {
            return Pool.Wait4Workers(ref workerCount);
        }
    }


    /// <summary>
    /// Simple, efficient thread pool implementation - THIS CLASS IS NOT THREAD SAFE !! 
    /// </summary>
    [System.Security.SecuritySafeCritical] 
    public sealed partial class ThreadPool<T> where T : struct {

        #region attributes
        //private uint m_maxNumberThreads;
        internal List<Thread> m_threads = new List<Thread>();
        private readonly object m_syncObject = new object();  // used for pulsing worker threads of the thread pool
        private static bool s_exitFlag;
        #endregion

        #region properties

        /// <summary>
        /// Property returns the maximum number of threads used in pool.
        /// </summary>
        public uint MaxNumberThreads {
            get { return (uint)m_threads.Count; }
            set {
                // Note, vers.7: if a worker thread requests a smaller MaxNumberThread value
                // globally, we do not remove / end any threads in the pool! The ThreadPool.MaxNumberThread
                // value is to be understood as the maximum number of parallel items a thread _can_ use from the pool. 
                // Since m_maxNumberThreads is a global static, shared variable we cannot change it here without 
                // the need for serious synchronization. Other threads / work items already enqueued potentially 
                // rely on it... 

                //if (m_maxNumberThreads > value) {
                //    while (m_maxNumberThreads > value) {
                //        Thread<T> thread = m_threads[(int)(--m_maxNumberThreads)];
                //        thread.End();
                //        m_threads.Remove(thread);
                //    }
                //    lock(m_syncObject)
                //        Monitor.PulseAll(m_syncObject); ; // should return fast ??!
                //    //m_barrier.RemoveParticipants((int)(value - m_maxNumberThreads));
                //}
                //if (value < 1) {
                //    throw new ArgumentException($"The maximum number of threads for this thread pool may not be smaller than 1! Found value: {value}."); 
                //}
                lock (m_syncObject) {
                    while (m_threads.Count < value) {
                        m_threads.Add(new Thread((uint)m_threads.Count, m_syncObject));
                    }
                    //m_maxNumberThreads = value;
                }
            }
        }
        #endregion

        /// <summary>
        /// Constructor used to create a new ThreadPool object.
        /// </summary>
        /// <param name="maxThreads">Initialize the pool with the given maximum number of threads.</param>
        public ThreadPool(uint maxThreads) {
            // when created on a (legacy or pipelined) awaiter function thread, maxThreads will be 0.
            // This should not happen, but there are some places throughout ILNumerics.Core which may causes 
            // the pool to be created on such threads, nevertheless. Be prepared to always have at least one 
            // worker thread available, maintaining integrity for the pool.

            MaxNumberThreads = Math.Max(1, maxThreads);  // creates initial threads
        }

        /// <summary>
        /// Enqueue a new work item for parallel processing in a worker thread.
        /// </summary>
        /// <param name="id">The id /index of the target thread.</param>
        /// <param name="action">Delegate with the work definition.</param>
        /// <param name="data">Data to be send to the delegate for processing.</param>
        /// <returns>True if the work chunk has been successfully enqueded and is ready for processing.</returns>
        public bool QueueUserWorkItem(uint id, Action<T> action, T data) {
            //// the first worker thread queueing wakes all other threads
            //if (id == 0) {
            //    s_pool.m_barrier.SignalAndWait(); 
            //}

            //System.Threading.ThreadPool.QueueUserWorkItem((a) => action(data));
            //return true; 

            var thread = m_threads[(int)(id % MaxNumberThreads)];
            //Debug.Assert(thread.m_thread.ThreadState == System.Threading.ThreadState.Running);
            ThreadPoolWorkItem workItem;
            workItem.Action = action;
            workItem.Data = data;
            thread.Queue(workItem);
            return true;
        }
        /// <summary>
        /// Synchronously wait until the variable referenced by <paramref name="workerCount"/> reached 0.
        /// </summary>
        /// <param name="workerCount">Variable to wait on.</param>
        /// <returns>Integer value indicating the time waited on the variable. Used for optimizing future operations in a feedback loop.</returns>
        public int Wait4Workers(ref int workerCount) {
            if (workerCount <= 0) return -1; // called from the 'main' thread. No need for memory barrier! 

            //var waiter = new SpinWait();
            do {
                // It looks like spin-waiting here does not pay off. Likely, the 
                // payload is too small to let multiple workers run truely 
                // in parallel. In this case, the 'lock' would keep spinning 
                // and this thread gets preempted before all workers are done. 
                // Sleep(0) likely cause the thread to become preempted, too. 
                // So using Sleep(0) immediately saves the wasted spinning overhead.  

                //waiter.SpinOnce();

                //for (int i = 0; i < 5; i += delay) {
                //    i -= (delay - 1);
                //} 

                System.Threading.Thread.Sleep(0);
                System.Threading.Thread.MemoryBarrier();
            } while (workerCount > 0 && !s_exitFlag); 
            //LeaveHotSection(); 
            return 1; 
        }
        /// <summary>
        /// Structure describing a single work item used to define a work chunk. 
        /// </summary>
        internal struct ThreadPoolWorkItem {
            /// <summary>
            /// Encapsulates a method that has a single parameter and does not return value.
            /// </summary>
            internal Action<T> Action;
            /// <summary>
            /// ThreadPoolWorkItem data object.
            /// </summary>
            internal T Data; 
            
        }

        internal class Thread {
            private uint m_index; 
            internal System.Threading.Thread m_thread;
            //private ThreadPool m_threadPool; 
            //private object m_syncObject;
            private ConcurrentQueue<ThreadPoolWorkItem> m_items = new ConcurrentQueue<ThreadPoolWorkItem>(); 
            //private bool m_hasWorkload; 
            private bool m_ending;
            public const int HOT_SECTION_KEEP_ALIVE_MS = 50;
            private AutoResetEvent m_resetEvent;

            internal Thread(uint index, object syncobject) {
                m_index = index; 
                m_ending = false;
                //m_syncObject = syncobject;
                m_resetEvent = new AutoResetEvent(false);
                //m_threadPool = threadpool; 
                m_thread = new System.Threading.Thread(this.WorkerFunc);
                m_thread.Name = $"ILNumerics #{index} Worker Thread"; 
                //m_thread.Priority = ThreadPriority.AboveNormal;
                // 
                // BG threads: there once was an issue with IsBackground being 'false'. 
                // It seems to hold an application alive by not shutting down those back-
                // ground threads. IsBackground should be true.
                m_thread.IsBackground = true; 
                //m_thread.Priority = ThreadPriority.Highest;

                m_thread.Start();
                //m_hasWorkload = false; 
            }

            private uint Index {
                get {
                    return m_index; 
                }
            }

            /// <summary>
            /// Signal this thread to finish processing work items and to destroy itself ASAP.
            /// </summary>
            internal void End() {
                //if (m_hasWorkload)
                //    throw new InvalidOperationException("invalid attempt to remove a thread with pending work items");
                m_ending = true;
                m_resetEvent.Set();

            }
            /// <summary>
            /// Queue a new work item for processing by this thread.
            /// </summary>
            /// <param name="workItem"></param>
            public void Queue(ThreadPoolWorkItem workItem) {
                //Debug.Assert(!m_hasWorkload); 
                m_items.Enqueue(workItem);
                //m_hasWorkload = true; // atomic 
                m_resetEvent.Set();
#if OBSOLETE
                Trace.WriteLine($"({System.Threading.Thread.CurrentThread.ManagedThreadId}) enqueued work item to thread: {this.m_thread.ManagedThreadId}."); 
#endif
            }

            //internal bool HasWorkload() {
            //    return m_hasWorkload; 
            //}

            private void WorkerFunc() {
                try {
                    while (true) {

                        m_resetEvent.WaitOne();
                        while (!m_ending && m_items.TryDequeue(out ThreadPoolWorkItem workItem)) {
                            workItem.Action(workItem.Data);
                        }
                        if (m_ending) return;

                    }
                } catch (ThreadAbortException) {
                    // this may happen at system / app domain shutdown. Do not continue normally after TAE!
                    End();
                    ThreadPool<T>.s_exitFlag = true;   // prevents active main thread from waiting for ever
                    //throw;
                } catch (Exception exc) {
                    System.Diagnostics.Trace.WriteLine($"FAILURE: {exc.ToString()}");
                    End();
                    ThreadPool<T>.s_exitFlag = true;   // prevents active main thread from waiting for ever
                    throw;
                }

            }
        }


    }
}
