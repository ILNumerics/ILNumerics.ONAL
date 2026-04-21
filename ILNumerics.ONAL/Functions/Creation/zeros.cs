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
//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using ILNumerics.Core.StorageLayer;
using ILNumerics.Core.DeviceManagement;

namespace ILNumerics.Core.Functions.Builtin {
    internal static partial class MathInternal {

        #region double (numeric)

        /// <summary>
        /// Creates a new array of 0.0-valued <see cref="System.Double"/> elements of given <paramref name="size"/>.
        /// </summary>
        /// <param name="size">Length of dimensions as array.</param>
        /// <returns>New ILNumerics array according to <paramref name="size"/>, initialized with 0.</returns>
        /// <remarks><para>This creates and initializes a new array in undefined element storage order. 
        /// The size of the new array corresponds to <paramref name="size"/>. Each entry in <paramref name="size"/> 
        /// provides the lengths of the corresponding dimension in the new array. Note, that this overload does 
        /// not create a square matrix – even if <paramref name="size"/> contains a single element only.</para>
        /// </remarks>
        /// <seealso cref="zeros{T}(long, long, StorageOrders)"/>
        /// <seealso cref="array{T}(T, long, StorageOrders)"/>
        /// <seealso cref="vector{T}(T, T, T, T, T, T, T, T, T, T)"/>
        [Obsolete("Use zeros<T>(size) instead!")]
        internal unsafe static Array<double> zeros(InArray<long> size) {

            var ret = StorageLayer.Storage<double>.Create();
            ret.S.SetAll(size, order: StorageOrders.ColumnMajor);
            return zerosInternal<double>(ret);
        }

        /// <summary>
        /// Creates a square matrix of 0.0-valued <see cref="System.Double"/> elements, size [dim0, dim0].
        /// </summary>
        /// <param name="rows_columns">Length of dimension #0 and dimension #1.</param>
        /// <returns>New ILNumerics array initialized with default(T).</returns>
        /// <remarks>This creates a square matrix with the same number of rows and columns. The elements 
        /// are ordered in undefined order.
        /// <para>This function always creates a matrix, hence the array returned will always have 
        /// two dimensions. Use <see cref="vector{T}(T, T, T, T)"/> or <see cref="array{T}(T, long, StorageOrders)"/> 
        /// for creating arrays with less than two dimension if your setting of <see cref="Settings.ArrayStyle"/> allows it.</para></remarks>
        /// <seealso cref="zeros{T}(long, long, StorageOrders)"/>
        /// <seealso cref="array{T}(T, long, StorageOrders)"/>
        /// <seealso cref="vector{T}(T, T, T, T, T, T, T, T, T, T)"/>
        internal unsafe static Array<double> zeros(long rows_columns)  {

            var ret = StorageLayer.Storage<double>.Create();
            ret.S.SetAll(dim0: rows_columns, dim1: rows_columns, order: StorageOrders.ColumnMajor);
            return zerosInternal<double>(ret);
        }
        /// <summary>
        /// Creates a matrix of 0.0-valued <see cref="System.Double"/> elements. Storage order: undefined. 
        /// </summary>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <returns>New ILNumerics array initialized with default(T).</returns>
        /// <seealso cref="zeros{T}(long, long, StorageOrders)"/>
        internal unsafe static Array<double> zeros(long dim0, long dim1) {

            var ret = StorageLayer.Storage<double>.Create();
            ret.S.SetAll(dim0, dim1, order: StorageOrders.ColumnMajor);
            return zerosInternal<double>(ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array of 0.0-valued <see cref="System.Double"/> elements. Storage order: undefined. 
        /// </summary>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <returns>New ILNumerics array initialized with default(T).</returns>
        /// <seealso cref="zeros{T}(long, long, StorageOrders)"/>
        internal unsafe static Array<double> zeros(long dim0, long dim1, long dim2)  {

            var ret = StorageLayer.Storage<double>.Create();
            ret.S.SetAll(dim0, dim1, dim2, order: StorageOrders.ColumnMajor);
            return zerosInternal<double>(ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array of 0.0-valued <see cref="System.Double"/> elements. Storage order: undefined. 
        /// </summary>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="dim3">Length of dimension #3.</param>
        /// <returns>New ILNumerics array initialized with default(T).</returns>
        /// <seealso cref="zeros{T}(long, long, StorageOrders)"/>
        internal unsafe static Array<double> zeros(long dim0, long dim1, long dim2, long dim3)  {

            var ret = StorageLayer.Storage<double>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, order: StorageOrders.ColumnMajor);
            return zerosInternal<double>(ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array of 0.0-valued <see cref="System.Double"/> elements. Storage order: undefined. 
        /// </summary>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="dim3">Length of dimension #3.</param>
        /// <param name="dim4">Length of dimension #4.</param>
        /// <returns>New ILNumerics array initialized with default(T).</returns>
        /// <seealso cref="zeros{T}(long, long, StorageOrders)"/>
        internal unsafe static Array<double> zeros(long dim0, long dim1, long dim2, long dim3, long dim4)  {

            var ret = StorageLayer.Storage<double>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, dim4, order: StorageOrders.ColumnMajor);
            return zerosInternal<double>(ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array of 0.0-valued <see cref="System.Double"/> elements. Storage order: undefined. 
        /// </summary>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="dim3">Length of dimension #3.</param>
        /// <param name="dim4">Length of dimension #4.</param>
        /// <param name="dim5">Length of dimension #5.</param>
        /// <returns>New ILNumerics array initialized with default(T).</returns>
        /// <seealso cref="zeros{T}(long, long, StorageOrders)"/>
        internal unsafe static Array<double> zeros(long dim0, long dim1, long dim2, long dim3, long dim4, long dim5)  {

            var ret = StorageLayer.Storage<double>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, dim4, dim5, order: StorageOrders.ColumnMajor);
            return zerosInternal<double>(ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array of 0.0-valued <see cref="System.Double"/> elements. Storage order: undefined. 
        /// </summary>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="dim3">Length of dimension #3.</param>
        /// <param name="dim4">Length of dimension #4.</param>
        /// <param name="dim5">Length of dimension #5.</param>
        /// <param name="dim6">Length of dimension #6.</param>
        /// <returns>New ILNumerics array initialized with default(T).</returns>
        /// <seealso cref="zeros{T}(long, long, StorageOrders)"/>
        internal unsafe static Array<double> zeros(long dim0, long dim1, long dim2, long dim3, long dim4, long dim5, long dim6)  {

            var ret = StorageLayer.Storage<double>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, dim4, dim5, dim6, order: StorageOrders.ColumnMajor);
            return zerosInternal<double>(ret);
        }
        #endregion

        #region generic (numeric)
        /// <summary>
        /// Creates a square matrix of 0-valued elements.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="rows_columns">Length of dimension #0 and dimension #1 (the number of rows and columns).</param>
        /// <param name="order">[Optional] The storage order for the new array. Default: undefined.</param>
        /// <returns>New ILNumerics array initialized with default(T).</returns>
        /// <remarks>This creates a square matrix with the same number of rows and columns. 
        /// <para>This function always creates a matrix, hence the array returned will always have 
        /// two dimensions. Use <see cref="vector{T}(T, T, T, T)"/> or <see cref="array{T}(T, long, StorageOrders)"/> 
        /// for creating arrays with less than two dimension if your setting of <see cref="Settings.ArrayStyle"/> allows it.</para>
        /// <para>When the optional argument <paramref name="order"/> is omitted ILNumerics is free to decide for the storage order. Often, an advantageous 
        /// (in terms of processing performance) storage order is selected according to the current algorithmic context, to the current setting of <see cref="Settings.ArrayStyle"/> 
        /// and other factors.</para>
        /// </remarks>
        /// <seealso cref="zeros{T}(long, long, StorageOrders)"/>
        /// <seealso cref="array{T}(T, long, StorageOrders)"/>
        /// <seealso cref="vector{T}(T, T, T, T, T, T, T, T, T, T)"/>
        /// <seealso cref="ones{T}(long, long, StorageOrders)"/>
        /// <seealso cref="empty{T}(long, long, StorageOrders)"/>
        /// <seealso href="https://ilnumerics.net/ArrayCreation3.html"/>
        internal unsafe static Array<T> zeros<T>(long rows_columns, StorageOrders order = StorageOrders.ColumnMajor)  {

            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0: rows_columns, dim1: rows_columns, order: order);
            return zerosInternal<T>(ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array of 0 elements.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="order">[Optional] The storage order for the new array. Default: undefined.</param>
        /// <returns>New ILNumerics array initialized with default(T).</returns>
        /// <remarks><para>When the optional argument <paramref name="order"/> is omitted ILNumerics is free to decide for the storage order. Often, an advantageous 
        /// (in terms of processing performance) storage order is selected according to the current algorithmic context, to the current setting of <see cref="Settings.ArrayStyle"/> 
        /// and other factors.</para></remarks>
        /// <seealso href="https://ilnumerics.net/ArrayCreation3.html"/>
        internal unsafe static Array<T> zeros<T>(long dim0, long dim1, StorageOrders order = StorageOrders.ColumnMajor)  {

            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, order: order);
            return zerosInternal<T>(ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array of 0 elements.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="order">[Optional] The storage order for the new array. Default: undefined.</param>
        /// <returns>New ILNumerics array initialized with default(T).</returns>
        /// <remarks>
        /// <para>When the optional argument <paramref name="order"/> is omitted ILNumerics is free to decide for the storage order. Often, an advantageous 
        /// (in terms of processing performance) storage order is selected according to the current algorithmic context, to the current setting of <see cref="Settings.ArrayStyle"/> 
        /// and other factors.</para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/ArrayCreation3.html"/>
        internal unsafe static Array<T> zeros<T>(long dim0, long dim1, long dim2, StorageOrders order = StorageOrders.ColumnMajor)  {

            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, dim2, order: order);
            return zerosInternal<T>(ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array of 0 elements.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="dim3">Length of dimension #3.</param>
        /// <param name="order">[Optional] The storage order for the new array. Default: undefined.</param>
        /// <returns>New ILNumerics array initialized with default(T).</returns>
        /// <remarks>
        /// <para>When the optional argument <paramref name="order"/> is omitted ILNumerics is free to decide for the storage order. Often, an advantageous 
        /// (in terms of processing performance) storage order is selected according to the current algorithmic context, to the current setting of <see cref="Settings.ArrayStyle"/> 
        /// and other factors.</para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/ArrayCreation3.html"/>
        internal unsafe static Array<T> zeros<T>(long dim0, long dim1, long dim2, long dim3, StorageOrders order = StorageOrders.ColumnMajor)  {

            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, order: order);
            return zerosInternal<T>(ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array of 0 elements.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="dim3">Length of dimension #3.</param>
        /// <param name="dim4">Length of dimension #4.</param>
        /// <param name="order">[Optional] The storage order for the new array. Default: undefined.</param>
        /// <returns>New ILNumerics array initialized with default(T).</returns>
        /// <remarks>
        /// <para>When the optional argument <paramref name="order"/> is omitted ILNumerics is free to decide for the storage order. Often, an advantageous 
        /// (in terms of processing performance) storage order is selected according to the current algorithmic context, to the current setting of <see cref="Settings.ArrayStyle"/> 
        /// and other factors.</para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/ArrayCreation3.html"/>
        internal unsafe static Array<T> zeros<T>(long dim0, long dim1, long dim2, long dim3, long dim4, StorageOrders order = StorageOrders.ColumnMajor)  {

            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, dim4, order: order);
            return zerosInternal<T>(ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array of 0 elements.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="dim3">Length of dimension #3.</param>
        /// <param name="dim4">Length of dimension #4.</param>
        /// <param name="dim5">Length of dimension #5.</param>
        /// <param name="order">[Optional] The storage order for the new array. Default: undefined.</param>
        /// <returns>New ILNumerics array initialized with default(T).</returns>
        /// <remarks>
        /// <para>When the optional argument <paramref name="order"/> is omitted ILNumerics is free to decide for the storage order. Often, an advantageous 
        /// (in terms of processing performance) storage order is selected according to the current algorithmic context, to the current setting of <see cref="Settings.ArrayStyle"/> 
        /// and other factors.</para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/ArrayCreation3.html"/>
        internal unsafe static Array<T> zeros<T>(long dim0, long dim1, long dim2, long dim3, long dim4, long dim5, StorageOrders order = StorageOrders.ColumnMajor)  {

            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, dim4, dim5, order: order);
            return zerosInternal<T>(ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array of 0 elements.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="dim3">Length of dimension #3.</param>
        /// <param name="dim4">Length of dimension #4.</param>
        /// <param name="dim5">Length of dimension #5.</param>
        /// <param name="dim6">Length of dimension #6.</param>
        /// <param name="order">[Optional] The storage order for the new array. Default: undefined.</param>
        /// <returns>New ILNumerics array initialized with default(T).</returns>
        /// <seealso href="https://ilnumerics.net/ArrayCreation3.html"/>
        internal unsafe static Array<T> zeros<T>(long dim0, long dim1, long dim2, long dim3, long dim4, long dim5, long dim6, StorageOrders order = StorageOrders.ColumnMajor)  {

            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, dim4, dim5, dim6, order: order);
            return zerosInternal<T>(ret);
        }
        /// <summary>
        /// Create a new array of zeros '0' with the same size as <paramref name="size"/>. 
        /// </summary>
        /// <typeparam name="T">Element type for the new array.</typeparam>
        /// <param name="size">Number and lengths of the dimensions for the new array.</param>
        /// <param name="order">[Optional] Storage order for the new array. Default: undefined.</param>
        /// <returns>New array of the specified size and storage order, initialized with '0'.</returns>
        /// <remarks><para>When the optional argument <paramref name="order"/> is omitted ILNumerics is free to decide for the storage order. Often, an advantageous 
        /// (in terms of processing performance) storage order is selected according to the current algorithmic context, to the current setting of <see cref="Settings.ArrayStyle"/> 
        /// and other factors.</para></remarks>
        /// <seealso href="https://ilnumerics.net/ArrayCreation3.html"/>
        internal unsafe static Array<T> zeros<T>(Size size, StorageOrders order = StorageOrders.ColumnMajor) {
            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetDimensionLengths(size.GetBSD(false), order: order);
            return zerosInternal<T>(ret);
        }

        /// <summary>
        /// Create a new array of zeros '0' with dimensions number and lengths as determined by <paramref name="size"/>. 
        /// </summary>
        /// <typeparam name="T">Element type for the new array.</typeparam>
        /// <param name="size">Number and lengths of the dimensions for the new array.</param>
        /// <param name="order">[Optional] Storage order for the new array. Default: undefined.</param>
        /// <returns>New array of the specified size and storage order, initialized with '0'.</returns>
        /// <remarks><para>When the optional argument <paramref name="order"/> is omitted ILNumerics is free to decide for the storage order. Often, an advantageous 
        /// (in terms of processing performance) storage order is selected according to the current algorithmic context, to the current setting of <see cref="Settings.ArrayStyle"/> 
        /// and other factors.</para></remarks>
        /// <seealso href="https://ilnumerics.net/ArrayCreation3.html"/>
        internal unsafe static Array<T> zeros<T>(InArray<long> size, StorageOrders order = StorageOrders.ColumnMajor) {
            var ret = Storage<T>.Create();
            ret.S.SetAll(size, order); // this frees size already!
            ret.Handles[0] = DeviceManager.GetDevice(0).New<T>((ulong)ret.S.NumberOfElements, true);
            return ret.RetArray; 
        }

        /// <summary>
        /// Creates a new array of zeros '0' with a size determined by <paramref name="size"/>. 
        /// </summary>
        /// <typeparam name="T">Element type for the new array. This must be a value type.</typeparam>
        /// <param name="size">Variable length <see cref="System.Array"/> or comma separated list with lengths of the dimensions of the new array.</param>
        /// <returns>New array of the specified size and undefined storage order, initialized with '0'.</returns>
        /// <remarks>
        /// <para><paramref name="size"/> cannot be <c>null</c>. Its lengths determines the number of dimensions of the new array. The elements  
        /// determine the lengths of corresponding dimensions and cannot contain negative values.</para>
        /// <para>Arrays returned from this overload are in undefined storage order. Use <see cref="zeros{T}(InArray{long}, StorageOrders)"/> for 
        /// determining the storage order explicitly.</para>
        /// <para>Since the variable length parameter list and the 'params' keyword (C#) implicitly allocates new storage under the control of the 
        /// GC consider using one of the <see cref="vector{T}(T)"/> overloads and <see cref="zeros{T}(InArray{long}, StorageOrders)"/> instead to provide the dimension lengths. 
        /// This is recommended when high-performance is important.</para>
        /// </remarks>
        /// <seealso cref="zeros{T}(InArray{long}, StorageOrders)"/>
        /// <seealso cref="array{T}(T, long, StorageOrders)"/>
        /// <seealso cref="vector{T}(T, T, T, T, T, T, T, T, T, T)"/>
        /// <seealso cref="ones{T}(long, long, StorageOrders)"/>
        /// <seealso cref="empty{T}(long, long, StorageOrders)"/>
        /// <seealso cref="Size.MaxNumberOfDimensions"/>
        /// <seealso href="https://ilnumerics.net/ArrayCreation3.html"/>
        /// <exception cref="ArgumentException">if <typeparamref name="T"/> is not supported. Use one of the predefined, scalar value types (<see cref="System.Double"/>, float, int, ...), 
        /// <see cref="ILNumerics.complex"/> or <see cref="ILNumerics.fcomplex"/>.</exception>
        /// <exception cref="ArgumentException">if <paramref name="size"/> is null, has more elements than <see cref="Size.MaxNumberOfDimensions"/> or contains negative values.</exception>
        internal static Array<T> zeros<T>(params long[] size) {
            return zeros<T>(vector(size));
        }

        #endregion

        
        private unsafe static Array<T> zerosInternal<T>(Storage<T> ret)  {
            System.Diagnostics.Debug.Assert(ret != null && ret.S.IsContinuous);
            var outLen = ret.S.NumberOfElements;
            //ret.m_handles = CountableArray.Create();
            System.Diagnostics.Debug.Assert(ret.Handles != null); 
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<T>((ulong)outLen, true);

            return ret.RetArray;
        }


    }
}
