using ILNumerics.Core.Arrays;
using ILNumerics.Core.StorageLayer;
using System;

namespace ILNumerics.Core.Internal {
    public static class SegmentTypeExtensions {

        /// <summary>
        /// Returns a clone of this storage as RetT return type array. To be used on synchronous execution regions only. 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="LocalT"></typeparam>
        /// <typeparam name="InT"></typeparam>
        /// <typeparam name="OutT"></typeparam>
        /// <typeparam name="RetT"></typeparam>
        /// <typeparam name="StorageT"></typeparam>
        /// <param name="storage">This storage.</param>
        /// <returns>Clone as RetT.</returns>
        public static RetT AsRetArray<T1, LocalT, InT, OutT, RetT, StorageT>(
            this BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT> storage)

        where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {

            var ret = (storage.Clone() as StorageT);
            return ret.m_retArray;
        }
    }
}

namespace ILNumerics.F2NET.Array {
    public static partial class Intrinsics {

        #region generic (numeric)

        /// <summary>
        /// Wraps a 1D array (vector) around allocated memory.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="ptr">The pointer to allocated memory to be wrapped. Must be large enough to hold the specified number of elements.</param>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="order">[Optional] Storage order. Default: (null) means <see cref = "F:ILNumerics.StorageOrders.ColumnMajor" />.</param>
        /// <returns>Array as specified.</returns>
        public unsafe static Array<T> AsArray<T>(this IntPtr ptr, long dim0, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = Core.StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0: dim0, order: order);
            return createHandle<T>(ptr, ret);
        }
        /// <summary>
        /// Create 1D array of arbitrary size without clearing the elements.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="ptr">The pointer to allocated memory to be wrapped. Must be large enough to hold the specified number of elements.</param>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="order">[Optional] Storage order. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>Array as specified.</returns>
        public unsafe static Array<T> AsArray<T>(this IntPtr ptr, long dim0, long dim1, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, order: order);
            return createHandle<T>(ptr, ret);
        }
        /// <summary>
        /// Create 1D array of arbitrary size without clearing the elements.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="ptr">The pointer to allocated memory to be wrapped. Must be large enough to hold the specified number of elements.</param>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="order">[Optional] Storage order. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>Array as specified.</returns>
        public unsafe static Array<T> AsArray<T>(this IntPtr ptr, long dim0, long dim1, long dim2, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, dim2, order: order);
            return createHandle<T>(ptr, ret);
        }
        /// <summary>
        /// Create 1D array of arbitrary size without clearing the elements.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="ptr">The pointer to allocated memory to be wrapped. Must be large enough to hold the specified number of elements.</param>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="dim3">Length of dimension #3.</param>
        /// <param name="order">[Optional] Storage order. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>Array as specified.</returns>
        public unsafe static Array<T> AsArray<T>(this IntPtr ptr, long dim0, long dim1, long dim2, long dim3, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, order: order);
            return createHandle<T>(ptr, ret);
        }
        /// <summary>
        /// Create 1D array of arbitrary size without clearing the elements.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="ptr">The pointer to allocated memory to be wrapped. Must be large enough to hold the specified number of elements.</param>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="dim3">Length of dimension #3.</param>
        /// <param name="dim4">Length of dimension #4.</param>
        /// <param name="order">[Optional] Storage order. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>Array as specified.</returns>
        public unsafe static Array<T> AsArray<T>(this IntPtr ptr, long dim0, long dim1, long dim2, long dim3, long dim4, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, dim4, order: order);
            return createHandle<T>(ptr, ret);
        }
        /// <summary>
        /// Create 1D array of arbitrary size without clearing the elements.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="ptr">The pointer to allocated memory to be wrapped. Must be large enough to hold the specified number of elements.</param>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="dim3">Length of dimension #3.</param>
        /// <param name="dim4">Length of dimension #4.</param>
        /// <param name="dim5">Length of dimension #5.</param>
        /// <param name="order">[Optional] Storage order. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>Array as specified.</returns>
        public unsafe static Array<T> AsArray<T>(this IntPtr ptr, long dim0, long dim1, long dim2, long dim3, long dim4, long dim5, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, dim4, dim5, order: order);
            return createHandle<T>(ptr, ret);
        }
        /// <summary>
        /// Create 1D array of arbitrary size without clearing the elements.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="ptr">The pointer to allocated memory to be wrapped. Must be large enough to hold the specified number of elements.</param>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="dim3">Length of dimension #3.</param>
        /// <param name="dim4">Length of dimension #4.</param>
        /// <param name="dim5">Length of dimension #5.</param>
        /// <param name="dim6">Length of dimension #6.</param>
        /// <param name="order">[Optional] Storage order. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>Array as specified.</returns> 
        public unsafe static Array<T> AsArray<T>(this IntPtr ptr, long dim0, long dim1, long dim2, long dim3, long dim4, long dim5, long dim6, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, dim4, dim5, dim6, order: order);
            return createHandle<T>(ptr, ret);
        }

        #endregion

        
        private unsafe static Array<T> createHandle<T>(IntPtr ptr, Storage<T> ret) {
            System.Diagnostics.Debug.Assert(ret != null && ret.S.IsContinuous);
            var outLen = ret.S.NumberOfElements;
            System.Diagnostics.Debug.Assert(ret.Handles != null);
            //ret.m_handles = CountableArray.Create();
            
            ret.Handles[0] = new ILNumerics.Core.MemoryLayer.NativeHostHandle(Core.MemoryLayer.NativeHostHandle.EXTERNAL_HANDLE) { 
                Pointer = ptr, Length = new UIntPtr((ulong)(outLen * Storage<T>.SizeOfT)) 
            }; 

            if (ret is Storage<bool>) {
                // this may happens on T == bool
                throw new NotSupportedException($"The type {typeof(T).Name} cannot be used with {nameof(AsArray)}<T>()."); 
            }

            return ret.RetArray;
        }

    }
}
