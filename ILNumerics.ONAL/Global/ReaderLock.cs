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
using ILNumerics.Core.Arrays;
using ILNumerics.Core.StorageLayer;
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace ILNumerics.Core.Global {

    /// <summary>
    /// A slim lock for thread safe retrieval and retaining of an array's current storage instance. Mutable arrays + InT are retained at lock 
    /// creation time. All array types are released when the lock is disposed (typically at the end of the func.) 
    /// This lock is intended to be used on internal functions to prevent disposal of the current storage throughout processing of the func.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct ReaderLock : IDisposable {

        // The retained storage. Will be released with disposal of this instance.
        IStorage m_storage;  

        internal IStorage Storage { get { return m_storage; } }
        
        private ReaderLock(IStorage storage) {
            m_storage = storage;
        }
        internal static ReaderLock Create<T, LocalT, InT, OutT, RetT, StorageT>(ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A, out StorageT storage, bool releaseRetT = true, string throwOnNullWithMsg = null)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new ()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            var noRetain = false;
            if (object.Equals(A, null) || (!noRetain && A.IsSTA)) {
                // Is this array elegible for a performance shortcut ? On the "main" (= creator) thread we don't need to keep the storage alive. It is hold alive by the array in user code.
                storage = A?.Storage;
                return default; 
            }

            if (A?.TryGetStorageRetained(out storage, !noRetain) ?? false) {
                return new ReaderLock(storage);
            } else if (!string.IsNullOrEmpty(throwOnNullWithMsg) && Equals(A, null)) {
                throw new ArgumentNullException(throwOnNullWithMsg);
            } else { 
                storage = A?.Storage; 
                return default; 
            }
        }

        public void Dispose() {
            m_storage?.Release(); 
        }

        /// <summary>
        /// Releases the current storage and registers a new storage. 
        /// </summary>
        /// <param name="oldStorage">[In/Out] on entry: the old storage expected to be currently registered. On exit: the new storage if it was successfully replaced. Otherwise the oldStorage is not changed.</param>
        /// <param name="newStorage">Incoming, new storage to be registered instead of the current storage.</param>
        internal void Update<T>(ref T oldStorage, T newStorage) {

            // Note, reader locks should not be changed by multiple threads. However, we use CompareExchange anyways - who knows ... :| 
            var currentStorage = Interlocked.CompareExchange(ref m_storage, newStorage as IStorage, oldStorage as IStorage); 
            if (ReferenceEquals(currentStorage, oldStorage)) {
                currentStorage?.Release();
                (newStorage as IStorage)?.Retain();
            }
            oldStorage = newStorage; 
        }
    }
}
