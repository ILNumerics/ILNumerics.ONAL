using ILNumerics.Core.Arrays;
using System;

namespace ILNumerics.Core.StorageLayer {

    [System.Diagnostics.DebuggerTypeProxy(typeof(BaseStorage<,,,,,>.BaseStorageVisualizer))]
    public abstract partial class BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> : IDisposable, IStorage
        where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {
        internal class BaseStorageVisualizer {
            BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> instance; 

            BaseStorageVisualizer(BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> instance) {
                this.instance = instance;
            }

            public long ID => instance.ID;

            public string Size => instance.m_size.ToString();

            public int ReferenceCount => instance.ReferenceCount; 
            public bool IsReady => instance.IsReady;
            public CountableArray Handles => instance.m_handles;

        }
    }
}
