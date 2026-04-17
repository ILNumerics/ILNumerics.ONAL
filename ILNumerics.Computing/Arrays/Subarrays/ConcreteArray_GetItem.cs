using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ILNumerics.Core.Arrays {
    public abstract partial class ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT>   // T1 must not read "T" to prevent from conflicts with the .T (transpose) property.
        : BaseArray<T1>, IEnumerable<T1>
        where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {

        /// <summary>
        /// Retrieve the element addressed by the <i>sequential</i> index <paramref name="i"/>.
        /// </summary>
        /// <param name="i">Sequential index (1 dim), assuming <see cref="StorageOrders.ColumnMajor"/> storage order.</param>
        /// <returns>Boxed copy of the element at position <paramref name="i"/> if the elements stored in this 
        /// array are structs. Otherwise a reference copy of the element at <paramref name="i"/> is returned.</returns>
        /// <remarks>This function provides a way to access element values of this untyped base array for 
        /// convenience reasons. It is needed in very rare cases only. Don't expect great performance 
        /// from using untyped functions like this! The recommended performant way is to use the strongly typed classes 
        /// (<see cref="Array{T}"/>, <see cref="Logical"/>, <see cref="Cell"/>) and the corresponding typed 
        /// API functions only.
        /// <para>The element is found by 'sequential' index <paramref name="i"/>. This corresponds to the 
        /// position of the element in a flattened version of this array, where the flattening operation
        /// was performed in <see cref="StorageOrders.ColumnMajor"/> storage order.</para>
        /// </remarks>
        /// <seealso cref="Array{T}"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{bool}, long)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, long, long, long)"/>
        /// <seealso cref="Size.GetSeqIndex(InArray{long})"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="StorageOrders"/>
        public override object GetItem(long i) {
            return m_storage.GetValue(i);
        }
        /// <summary>
        /// Create a shallow clone of the storage of this array. (Array) reference count: 1, asynch reference count: 1 or 2 (if pending). 
        /// </summary>
        /// <param name="forceRelease">[Optional] Release on true if this is RetT. Default: false. </param>
        /// <returns>Shallow clone of this storage, reference count: 1 (ready) or 2 (pending).</returns>
        internal override IStorage GetClonedStorage(bool forceRelease = false) {
            var ret = m_storage.Clone();
            return ret; 
        }
        internal override BaseArray GetClonedArray() {
            return (m_storage.Clone() as StorageT).m_retArray;
        }
        internal bool TryGetStorageRetained(out StorageT storage, bool retain = true) {
            while (true) {
                storage = m_storage;
                if (storage == null || (storage.m_handles == null && storage.ReferenceCount == 0)) {  // allows pending storages
                    return false;  
                }
                if (retain == false || storage.TryRetain()) {
                    // this is only thread safe, as long as the array this was called on is still valid and not yet disposed. 
                    // We can prevent from the case that the storage was renamed and cached in the meantime: Double check that 
                    // the storage is attached to this array, still. (But this information, of course, is stale immediately, too!)
                    if (ReferenceEquals(storage, m_storage)) { 
                        return true;
                    } else if (retain) {
                        storage.Release(); 
                    }
                }
            }
        }

    }
}
