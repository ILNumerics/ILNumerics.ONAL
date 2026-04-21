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
using System.Collections.Generic;
using System.Text;
using ILNumerics;
using static ILNumerics.Globals;
using System.Security;
using ILNumerics.Core.StorageLayer;

namespace ILNumerics.Core.Functions.Builtin {
    internal static partial class MathInternal {

        #region generic (numeric)
        /// <summary>
        /// Create empty array with 0 elements of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Element type of the array returned.</typeparam>
        /// <returns>Empty array with smallest possible number of dimensions and 0 elements.</returns>
        /// <remarks><para>If the current array style is <see cref="ArrayStyles.ILNumericsV4"/> (default)
        /// the array returned will have the size [0,1]. Otherwise it has size [0], i.e: an empty vector!</para></remarks>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <seealso cref="Settings.MinNumberOfArrayDimensions"/>
        /// <seealso cref="empty{T}(InArray{long}, StorageOrders)"/>
        /// <seealso cref="empty{T}(long, StorageOrders)"/>
        internal static Array<T> empty<T>() {
            return empty<T>(0);
        }

        /// <summary>
        /// Create 1D array of arbitrary size without clearing the elements.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="order">[Optional] Storage order. Default: (null) means <see cref = "F:ILNumerics.StorageOrders.ColumnMajor" />.</param>
        /// <returns>Array as specified.</returns>
        internal unsafe static Array<T> empty<T>(long dim0, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0: dim0, order: order);
            return emptyInternal<T>(ret);
        }

        /// <summary>
        /// Create 1D array of arbitrary size without clearing the elements.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="order">[Optional] Storage order. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>Array as specified.</returns>
        internal unsafe static Array<T> empty<T>(long dim0, long dim1, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, order: order);
            return emptyInternal<T>(ret);
        }
        /// <summary>
        /// Create 1D array of arbitrary size without clearing the elements.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="order">[Optional] Storage order. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>Array as specified.</returns>
        internal unsafe static Array<T> empty<T>(long dim0, long dim1, long dim2, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, dim2, order: order);
            return emptyInternal<T>(ret);
        }
        /// <summary>
        /// Create 1D array of arbitrary size without clearing the elements.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="dim3">Length of dimension #3.</param>
        /// <param name="order">[Optional] Storage order. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>Array as specified.</returns>
        internal unsafe static Array<T> empty<T>(long dim0, long dim1, long dim2, long dim3, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, order: order);
            return emptyInternal<T>(ret);
        }
        /// <summary>
        /// Create 1D array of arbitrary size without clearing the elements.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="dim3">Length of dimension #3.</param>
        /// <param name="dim4">Length of dimension #4.</param>
        /// <param name="order">[Optional] Storage order. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>Array as specified.</returns>
        internal unsafe static Array<T> empty<T>(long dim0, long dim1, long dim2, long dim3, long dim4, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, dim4, order: order);
            return emptyInternal<T>(ret);
        }
        /// <summary>
        /// Create 1D array of arbitrary size without clearing the elements.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="dim3">Length of dimension #3.</param>
        /// <param name="dim4">Length of dimension #4.</param>
        /// <param name="dim5">Length of dimension #5.</param>
        /// <param name="order">[Optional] Storage order. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>Array as specified.</returns>
        internal unsafe static Array<T> empty<T>(long dim0, long dim1, long dim2, long dim3, long dim4, long dim5, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, dim4, dim5, order: order);
            return emptyInternal<T>(ret);
        }
        /// <summary>
        /// Create 1D array of arbitrary size without clearing the elements.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="dim3">Length of dimension #3.</param>
        /// <param name="dim4">Length of dimension #4.</param>
        /// <param name="dim5">Length of dimension #5.</param>
        /// <param name="dim6">Length of dimension #6.</param>
        /// <param name="order">[Optional] Storage order. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>Array as specified.</returns> 
        internal unsafe static Array<T> empty<T>(long dim0, long dim1, long dim2, long dim3, long dim4, long dim5, long dim6, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, dim4, dim5, dim6, order: order);
            return emptyInternal<T>(ret);
        }
        /// <summary>
        /// Create 1D array of arbitrary size without clearing the elements.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="size">Length of dimensions.</param>
        /// <param name="order">[Optional] Storage order. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>Array as specified.</returns>
        internal unsafe static Array<T> empty<T>(InArray<long> size, StorageOrders order = StorageOrders.ColumnMajor) {
            //using (Scope.Enter(size)) {
            //}
            return StorageLayer.Storage<T>.Create(size, order).RetArray;  // Create() handles (frees) size!
        }

        /// <summary>
        /// Create an empty array of the same size (shape) as another array without clearing the elements.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="size">Size descriptor of the other array A, as acquired by A.S.</param>
        /// <param name="order">[Optional] Storage order. Default: <see cref="StorageOrders.ColumnMajor"/>. This setting overrides the storage order of <paramref name="size"/>.</param>
        /// <returns>Array as specified.</returns>
        /// <remarks>The values of the elements of the array returned are undefined. If <paramref name="order"/> is 
        /// any of <see cref="StorageOrders.ColumnMajor"/> or <see cref="StorageOrders.RowMajor"/> the empty array 
        /// will have the same storage order as <paramref name="order"/>. Otherwise, the storage order corresponds to the 
        /// current value of <see cref="Settings.DefaultStorageOrder"/>.</remarks>
        internal unsafe static Array<T> empty<T>(Size size, StorageOrders order = StorageOrders.ColumnMajor) {
            
            if (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor) {
                order = Settings.DefaultStorageOrder; 
            }
            var ret = StorageLayer.Storage<T>.Create(size, order);
            return emptyInternal<T>(ret);
        }

        #endregion

        #region Obsolete methods (double)
        /// <summary>
        /// Create empty array with 0 elements of type <see cref="System.Double"/>.
        /// </summary>
        /// <returns>Empty array with smallest possible number of dimensions and 0 elements.</returns>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <seealso cref="Settings.MinNumberOfArrayDimensions"/>
        /// <seealso cref="empty{T}(InArray{long}, StorageOrders)"/>
        /// <seealso cref="empty{T}(long, StorageOrders)"/>
        [Obsolete("Use empty<T>() instead, specifying the element type T ('<double>') explicitly!")]
        internal static Array<double> empty() { return empty<double>(0); }

        /// <summary>
        /// Create array with a size determined by <paramref name="dims"/> and uninitialized elements of type <see cref="System.Double"/>.
        /// </summary>
        /// <returns>New array, having the number and lengths of dimensions as specified by <paramref name="dims"/> and and all uninitialized elements.</returns>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <seealso cref="Settings.MinNumberOfArrayDimensions"/>
        /// <seealso cref="empty{T}(InArray{long}, StorageOrders)"/>
        /// <seealso cref="empty{T}(long, StorageOrders)"/>
        [Obsolete("Use empty<T>() instead, specifying the element type T ('<double>') explicitly!")]
        internal static Array<double> empty(params int[] dims) {
            if (dims == null || dims.Length == 0) {
                return empty<double>(0);
            } else {
                switch (dims.Length) {
                    case 1:
                        return empty<double>(dims[0]);
                    case 2:
                        return empty<double>(dims[0], dims[1]);
                    case 3:
                        return empty<double>(dims[0], dims[1], dims[2]);
                    case 4:
                        return empty<double>(dims[0], dims[1], dims[2], dims[3]);
                    case 5:
                        return empty<double>(dims[0], dims[1], dims[2], dims[3], dims[4]);
                    case 6:
                        return empty<double>(dims[0], dims[1], dims[2], dims[3], dims[4], dims[5]);
                    case 7:
                        return empty<double>(dims[0], dims[1], dims[2], dims[3], dims[4], dims[5], dims[6]);
                    default:
                        return empty<double>(toint64(vector(dims)));  
                        //throw new ArgumentException($"Too many dimensions: the maximum number of dimensions supported by this overload is {Size.MaxNumberOfDimensions}. Use empty<T>(InArray<long>, StorageOrder) instead!");
                }
            }
        }
        /// <summary>
        /// Creates a new array of uninitialized values with a size determined by <paramref name="size"/>. 
        /// </summary>
        /// <typeparam name="T">Element type for the new array.</typeparam>
        /// <param name="size">Variable length <see cref="System.Array"/> or comma separated list with lengths of the dimensions of the new array.</param>
        /// <returns>New array of the specified size and <see cref="StorageOrders.ColumnMajor"/> storage order, uninitialized values.</returns>
        /// <remarks>
        /// <para><paramref name="size"/> cannot be <c>null</c>. Its lengths determines the number of dimensions of the new array. The elements  
        /// determine the lengths of corresponding dimensions and cannot contain negative values.</para>
        /// <para>This overload creates uninitialized arrays in <see cref="StorageOrders.ColumnMajor"/> only. Use <see cref="empty{T}(InArray{long}, StorageOrders)"/> for 
        /// determining the storage order explicitly.</para>
        /// <para>Since the variable length parameter list and the 'params' keyword (C#) implicitly allocates new storage under the control of the 
        /// GC consider using one of the <see cref="vector{T}(T)"/> overloads and <see cref="empty{T}(InArray{long}, StorageOrders)"/> instead to provide the dimension lengths. 
        /// This is recommended when high-performance is important.</para>
        /// </remarks>
        /// <seealso cref="zeros{T}(long, long, StorageOrders)"/>
        /// <seealso cref="array{T}(T, long, StorageOrders)"/>
        /// <seealso cref="vector{T}(T, T, T, T, T, T, T, T, T, T)"/>
        /// <seealso cref="ones{T}(long, long, StorageOrders)"/>
        /// <seealso cref="empty{T}(InArray{long}, StorageOrders)"/>
        /// <seealso cref="Size.MaxNumberOfDimensions"/>
        /// <seealso href="https://ilnumerics.net/ArrayCreation3.html"/>
        /// <exception cref="ArgumentException">if <typeparamref name="T"/> is not supported. Use one of the predefined, scalar value types (<see cref="System.Double"/>, float, int, ...), 
        /// <see cref="ILNumerics.complex"/> or <see cref="ILNumerics.fcomplex"/>.</exception>
        /// <exception cref="ArgumentException">if <paramref name="size"/> is null, has more elements than <see cref="Size.MaxNumberOfDimensions"/> or contains negative values.</exception>
        internal static Array<T> empty<T>(params long[] size) {
            return empty<T>(vector(size));
        }

        #endregion

        
        private unsafe static Array<T> emptyInternal<T>(Storage<T> ret) {
            System.Diagnostics.Debug.Assert(ret != null && ret.S.IsContinuous);
            var outLen = ret.S.NumberOfElements;
            System.Diagnostics.Debug.Assert(ret.Handles != null); 
            //ret.m_handles = CountableArray.Create();
            ret.Handles[0] = ret.New((ulong)outLen, false);

            if (ret is Storage<bool>) {
                // this may happens on T == bool
                throw new NotSupportedException($"The type {typeof(T).Name} cannot be used with empty<T>(). One alternative is: 'empty() > 0'."); 
            }

            return ret.RetArray;
        }



    }
}
