using ILNumerics.Core.Arrays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ILNumerics.Core.StorageLayer {

    public abstract partial class BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> : IDisposable, IStorage  // T1 must not read "T" to prevent from conflicts with the .T (transpose) property.
        where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

        /// <summary>
        /// Ensures that elements are layed out in a specific, continous storage order. 
        /// </summary>
        /// <param name="order">The storage order to be enforced. Must be one of: <see cref="StorageOrders.ColumnMajor"/> or <see cref="StorageOrders.RowMajor"/>.</param>
        /// <param name="forceCopy">[Optional] Copy the elements to new memory, no matter if the order is correct already (used by Detach()). Default: false.</param>
        /// <param name="inplace">[Optional] flag instructing the function to work on this storage directly. False: when required a new storage is created and Assign()-ed
        /// to the array(s) of this storage. True: when required the storage order is enforced / configured on this storage and without creating a new storage (not thread 
        /// safe, applicable for local storages, which are not exposed (yet) to other threads. Default: true (work inplace).</param>
        /// <remarks><para>Inplace option: by default, the function ensures the required storage order on this storage (inplace). This works, without renaming the array(s) of 
        /// this storage, hence, is not thread safe. This method is recommended for enforcing a certain storage order on local (mutable) arrays, and for immutable arrays, 
        /// like RetT and InT type arrays. Latter shall not be renamed (Assign()-ed) due to their specified immutability. (Note the contradiction with changing theirs size,
        /// though!) </para>
        /// <para>The not-inplace option (inplace = false) is for thread safety on any array which is potentially exposed to the public. It creates a new storage with the correct storage 
        /// order and assignes the new storage to the arrays of this storage. Note, that afterwards, the new storage instance must be used for processing subsequent steps 
        /// going forward. It can be stored from the return value if this function or fetched from the array layer.</para></remarks>
        
        internal unsafe StorageT EnsureStorageOrder(StorageOrders order, bool forceCopy = false, bool inplace = true) {
            // No need to check for array referenceCount (?!). We do not "change" the values of this array. All elements remain 
            // at their positions and keep their values! BUT it may affect algorithms relying on the storage order (strides)!!
            // Further, it may also affect users relying on the instance of m_handles!

            // Edit, v7.0: new, threadsafe approach (out of-place): ensure the storage order of a clone. 

            if (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor) {
                throw new ArgumentException("Invalid storage order specified. Allowed values: 'ColumnMajor' and 'RowMajor'.");
            }

            // First "Size" waits for asynch ops !
            if (!forceCopy && (Size.StorageOrder == order || (m_size.IsContinuous && m_size.NonSingletonDimensions < 2))) {
                // && m_handles.ReferenceCount <= 1) {  <- ho: removed (again?) to support A.T[Globals.r(0, 1, I)] on row-major 2D array A, without copy. No idea why reference check was required at the first place? 
                // Nothing to do: 
                return this as StorageT;
            }
            // else -> copy
            if (!inplace) {
                // Copy to new memory, enable caller to atomically mutate (on the array layer)! This storage is not changed.
                StorageT ret = Create();  // ref count = 0
                var newHandle = New((ulong)m_size.NumberOfElements);
                CopyTo(newHandle, null, order);
                ret.Handles[0] = newHandle;

                // configure BSD of the new storage
                var outBsd = ret.Size.GetBSD(true);
                var myBsd = Size.GetBSD(false);
                var ndims = Size.NumberOfDimensions;
                outBsd[0] = ndims;
                outBsd[1] = myBsd[1];
                outBsd[2] = 0;

                long stride = 1;
                if (order == StorageOrders.ColumnMajor) {
                    for (int i = 0; i < ndims; i++) {
                        outBsd[3 + i] = myBsd[3 + i];
                        outBsd[3 + ndims + i] = myBsd[3 + i] == 1 ? 0 : stride;
                        stride *= outBsd[3 + i];
                    }
                } else {
                    // row major 
                    for (int i = 0; i < ndims; i++) {
                        outBsd[2 + ndims - i] = myBsd[2 + ndims - i];
                        outBsd[2 + 2 * ndims - i] = myBsd[2 + ndims - i] == 1 ? 0 : stride;
                        stride *= myBsd[2 + ndims - i];
                    }
                }
                System.Diagnostics.Debug.Assert(stride == myBsd[1]);

                ret = Assign(ret, toOutT: true, fromRetT: true, renameInT: true);  // atomically assign new value / rename arrays. 
                // Note, new storage is never pending. This is why we do not have to adjust ref counts here...

                return ret;

            } else {
                // inplace 

                // must copy to new memory. Operating inplace is possible for RetT (no other refs can be around) and when thread safety is not required.
                var newHandle = New((ulong)m_size.NumberOfElements);
                CopyTo(newHandle, null, order);
                m_handles.Release();
                m_handles = CountableArray.Create();
                m_handles[0] = newHandle;

                // configure BSD of this storage
                var bsd = m_size.GetBSD(true);
                //if (copy) {
                bsd[2] = 0;
                //}
                uint ndims = (uint)bsd[0];
                long stride = 1;
                if (order == StorageOrders.ColumnMajor) {
                    for (int i = 0; i < ndims; i++) {
                        bsd[3 + ndims + i] = bsd[3 + i] == 1 ? 0 : stride;
                        stride *= bsd[3 + i];
                    }
                } else {
                    // row major 
                    for (int i = 0; i < ndims; i++) {
                        bsd[2 + 2 * ndims - i] = bsd[2 + ndims - i] == 1 ? 0 : stride;
                        stride *= bsd[2 + ndims - i];
                    }
                }
                return this as StorageT; 
            }
        }

        //internal IStorage GetIStorage(bool release) {
        //    var ret = Create(m_handles, m_size);
        //    if (release) Release();  
        //    return ret; 
        //}
    }
}
