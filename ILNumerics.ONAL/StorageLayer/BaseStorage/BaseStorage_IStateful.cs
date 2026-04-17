using ILNumerics.Core.Arrays;
using System;
using System.Threading;

namespace ILNumerics.Core.StorageLayer {

    /// <summary>
    /// Generic base class for all ILNumerics arrays. This class is used internally. 
    /// </summary>
    public abstract partial class BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> : IDisposable, IStorage
        where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

        public Size GetSizeUnsafe() {
            return m_size;
        }
        public CountableArray GetHandlesUnsafe() {
            return m_handles;
        }

        public bool IsSTA => m_creatorThreadId == Thread.CurrentThread.ManagedThreadId;

        public int GetElementTypeLength() {
            return (int)Storage<T>.SizeOfT; 
        }

        public int GetReferenceCount() {
            return m_arrayCounter; 
        }

        public void Finish() {

        }
    }
}
