//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////

using ILNumerics.Core.Arrays;
using ILNumerics.Core.MemoryLayer;
using ILNumerics.Core.Native;
using System;
using System.Security;

namespace ILNumerics.Core.StorageLayer {

    public abstract partial class BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> : IDisposable, IStorage
        where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

        #region CopyTo()

        /// <summary>
        /// Copy the data of this array to another memory region, specify element storage order for writing. 
        /// </summary>
        /// <param name="dest">Pointer to a memory region, large enough to store all elements of this array in the storage layout given by <paramref name="layout"/>.</param>
        /// <param name="outSize">[Output] On return the size descriptor holds the dimension lengths and strides according to the size of this array and the specified <paramref name="layout"/>.</param>
        /// <param name="layout">[Optional] The storage order used to write the elements to <paramref name="dest"/>. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <remarks><para><paramref name="outSize"/> can be <c>null</c> on entry in which case it will be ignored.</para>
        /// <para>If <paramref name="layout"/> is <c>null</c> or <see cref="StorageOrders.Other"/> the storage layout of the array 
        /// returned will be automatically determined based on the current storage layout: copying from continous storages will keep 
        /// the source storage layout (column- or row major layout). Copying from non-continous storages creates a storage according to 
        /// <see cref="Settings.DefaultStorageOrder"/>. </para>
        /// <para>If <paramref name="layout"/> is one of <see cref="StorageOrders.ColumnMajor"/> or <see cref="StorageOrders.RowMajor"/> the 
        /// elements are stored into <paramref name="dest"/> with this layout.</para>
        /// <para>Make sure that the memory region addressed by <paramref name="dest"/> is large enough, even if the current storage layout 
        /// corresponds to non-continously stored elements! Note further, that 
        /// any potentially existing holes in the element storage layout will not be cleared!</para>
        /// </remarks>
        
        internal virtual void CopyTo(MemoryHandle dest, Size outSize, StorageOrders? layout = null) {

            if (dest is NativeHostHandle) {
                Core.Functions.Builtin.CopyToOperators.CopyTo(Handles[0].Pointer, Size, dest.Pointer, outSize, layout, SizeOfT);
            } else {
                System.Diagnostics.Debug.Assert(Handles[0] is ManagedHostHandle<T>);  // otherwise CellStorage / LogicalStorage overrides would be used.
                Core.Functions.Builtin.CopyToOperators.CopyTo<T>((Handles[0] as ManagedHostHandle<T>).HostArray, Size, 
                                                                 (dest as ManagedHostHandle<T>).HostArray, outSize, layout);
            }

        }


        #endregion
    }
}
