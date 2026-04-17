using System;
using System.Diagnostics;
using ILNumerics.Core.StorageLayer;
using ILNumerics.Core.Arrays;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ILNumerics.Core.Internal;

#pragma warning disable CS1591 // No XML comments is fine.
#pragma warning disable CS1712 // No XML comments is fine.

namespace ILNumerics.Core.Global {

    public class ArrayDebuggerProxyBase { }

    /// <summary>
    /// This class is for internal use only. Do not instantiate from this class!
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ArrayDebuggerProxy2<T, LocalT, InT, OutT, RetT, StorageT> : ArrayDebuggerProxyBase
        where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private StorageT m_storage;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Size Dimensions {
            get {
                if (m_storage?.IsReady ?? false) {
                    return m_storage.Size;
                } else {
                    return null;
                }
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public RowVisualizer[] Rows {
            get {
                if (m_storage == null) {
                    return new[] { new RowVisualizer() { Value = "null" } };
                }
                if (m_storage.IsDisposed) {
                    return new[] { new RowVisualizer() { Value = "(disposed)" } };
                }
                // in case we run multithreaded in the IDE: VS immediate window and data tips allow a single thread to run only! 
                using (Settings.Ensure(() => Settings.MaxNumberThreads, 1u)) {
                    var ret = m_storage.ToString(
                        Settings.ToStringMaxNumberElementsPerDimension,
                        Settings.ToStringMaxNumberElements,
                        Settings.DefaultStorageOrder,
                        showSize: false,
                        showType: false).Select(l => new RowVisualizer() { Value = l });
                    //Debugger.Launch();
                    //Debugger.Break();
                    //System.IO.File.AppendAllText(@"D:\inst\log.txt", "\r\nRows: " + new StackTrace().ToString());
                    return ret.ToArray();
                }

            }
        }

        public ArrayDebuggerProxy2(ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> array)
            : base() { 

            //Debugger.Break(); 
            // make DEEP copy to minimize debugger IDE effects! 
            // array's reference counter is not changed. RetT is not freed!
            // handles are copied and their ref counter is not changed.
            if (!array.Storage.IsReady) {
                m_storage = null;
                return; 
            }
#if DEBUG 
            var oldArrCounter = array.Storage.ReferenceCount;
            var oldHandCounter = array.Storage.m_handles.ReferenceCount;
#endif 
            m_storage = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> 
                .Create(array.Storage.m_handles, array.Storage.GetSizeUnsafe());
            m_storage.DetachBufferSetInplace();
            
#if DEBUG
            Debug.Assert(oldArrCounter == array.Storage.ReferenceCount);
            Debug.Assert(oldHandCounter == array.Storage.m_handles.ReferenceCount);
#endif 
        }

        public void Dispose() {
            m_storage?.Release(); 
        }
    }

    [DebuggerDisplay("{Value,nq}")]
    public class RowVisualizer {
        public string Value;

        internal static IEnumerable<RowVisualizer> Convert(string[] tmp) {
            RowVisualizer[] ret = new RowVisualizer[tmp.Length];
            for (int i = 0; i < tmp.Length; i++) {
                ret[i] = new RowVisualizer() { Value = tmp[i] }; 
            }
            return ret; 
        }
    }

}

#pragma warning restore CS1591 // Internal using only. No XML Comments.
#pragma warning restore CS1712 // Internal using only. No XML Comments.