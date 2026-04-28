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

namespace ILNumerics.Core.Functions.Builtin {
    internal static partial class MathInternal {

        #region double

        /// <summary>
        /// Creates a square matrix of 1.0-valued <see cref="System.Double"/> elements, size [rows_columns x rows_columns].
        /// </summary>
        /// <param name="rows_columns">Length of dimension #0 and dimension #1.</param>
        /// <returns>New ILNumerics array, all elements initialized with value: 1.0.</returns>
        /// <remarks>This creates a <b>square matrix</b> with the same number of rows and columns. The elements 
        /// are storage in undefined order.
        /// <para>This function always creates a matrix, hence the array returned will always have 
        /// two dimensions. Use <see cref="vector{T}(T, T, T, T)"/> or <see cref="array{T}(T, long, StorageOrders)"/> 
        /// for creating arrays with less than two dimension if your setting of <see cref="Settings.ArrayStyle"/> allows it (requires: ArrayStyle.numpy).</para></remarks>
        /// <seealso cref="ones{T}(long, long, StorageOrders)"/>
        /// <seealso cref="zeros{T}(long, long, StorageOrders)"/>
        /// <seealso cref="array{T}(T, long, StorageOrders)"/>
        /// <seealso cref="vector{T}(T, T, T, T, T, T, T, T, T, T)"/>
        /// <seealso href="https://ilnumerics.net/ArrayCreation3.html"/>
        internal unsafe static Array<double> ones(long rows_columns)  {

            var ret = StorageLayer.Storage<double>.Create();
            ret.S.SetAll(dim0: rows_columns, dim1: rows_columns, order: StorageOrders.ColumnMajor);
            return onesInternal<double>(ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array of 1.0 <see cref="System.Double"/> elements.
        /// </summary>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <returns>New ILNumerics array initialized with 1.0.</returns>
        /// <seealso cref="ones{T}(long, long, StorageOrders)"/>
        /// <seealso cref="zeros{T}(long, long, StorageOrders)"/>
        /// <seealso cref="array{T}(T, long, StorageOrders)"/>
        /// <seealso cref="vector{T}(T, T, T, T, T, T, T, T, T, T)"/>
        /// <seealso href="https://ilnumerics.net/ArrayCreation3.html"/>
        internal unsafe static Array<double> ones(long dim0, long dim1)  {

            var ret = StorageLayer.Storage<double>.Create();
            ret.S.SetAll(dim0, dim1, order: StorageOrders.ColumnMajor);
            return onesInternal<double>(ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array of 1.0 <see cref="System.Double"/> elements.
        /// </summary>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <returns>New ILNumerics array initialized with 1.0.</returns>
        /// <seealso cref="ones{T}(long, long, StorageOrders)"/>
        /// <seealso cref="zeros{T}(long, long, StorageOrders)"/>
        /// <seealso cref="array{T}(T, long, StorageOrders)"/>
        /// <seealso cref="vector{T}(T, T, T, T, T, T, T, T, T, T)"/>
        /// <seealso href="https://ilnumerics.net/ArrayCreation3.html"/>
        internal unsafe static Array<double> ones(long dim0, long dim1, long dim2)  {

            var ret = StorageLayer.Storage<double>.Create();
            ret.S.SetAll(dim0, dim1, dim2, order: StorageOrders.ColumnMajor);
            return onesInternal<double>(ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array of 1.0 <see cref="System.Double"/> elements.
        /// </summary>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="dim3">Length of dimension #3.</param>
        /// <returns>New ILNumerics array initialized with 1.0.</returns>
        /// <seealso cref="ones{T}(long, long, StorageOrders)"/>
        /// <seealso cref="zeros{T}(long, long, StorageOrders)"/>
        /// <seealso cref="array{T}(T, long, StorageOrders)"/>
        /// <seealso cref="vector{T}(T, T, T, T, T, T, T, T, T, T)"/>
        /// <seealso href="https://ilnumerics.net/ArrayCreation3.html"/>
        internal unsafe static Array<double> ones(long dim0, long dim1, long dim2, long dim3)  {

            var ret = StorageLayer.Storage<double>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, order: StorageOrders.ColumnMajor);
            return onesInternal<double>(ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array of 1.0 <see cref="System.Double"/> elements.
        /// </summary>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="dim3">Length of dimension #3.</param>
        /// <param name="dim4">Length of dimension #4.</param>
        /// <returns>New ILNumerics array initialized with 1.0.</returns>
        /// <seealso cref="ones{T}(long, long, StorageOrders)"/>
        /// <seealso cref="zeros{T}(long, long, StorageOrders)"/>
        /// <seealso cref="array{T}(T, long, StorageOrders)"/>
        /// <seealso cref="vector{T}(T, T, T, T, T, T, T, T, T, T)"/>
        /// <seealso href="https://ilnumerics.net/ArrayCreation3.html"/>
        internal unsafe static Array<double> ones(long dim0, long dim1, long dim2, long dim3, long dim4)  {

            var ret = StorageLayer.Storage<double>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, dim4, order: StorageOrders.ColumnMajor);
            return onesInternal<double>(ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array of 1.0 <see cref="System.Double"/> elements.
        /// </summary>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="dim3">Length of dimension #3.</param>
        /// <param name="dim4">Length of dimension #4.</param>
        /// <param name="dim5">Length of dimension #5.</param>
        /// <returns>New ILNumerics array initialized with 1.0.</returns>
        /// <seealso cref="ones{T}(long, long, StorageOrders)"/>
        /// <seealso cref="zeros{T}(long, long, StorageOrders)"/>
        /// <seealso cref="array{T}(T, long, StorageOrders)"/>
        /// <seealso cref="vector{T}(T, T, T, T, T, T, T, T, T, T)"/>
        /// <seealso href="https://ilnumerics.net/ArrayCreation3.html"/>
        internal unsafe static Array<double> ones(long dim0, long dim1, long dim2, long dim3, long dim4, long dim5)  {

            var ret = StorageLayer.Storage<double>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, dim4, dim5, order: StorageOrders.ColumnMajor);
            return onesInternal<double>(ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array of 1.0 <see cref="System.Double"/> elements.
        /// </summary>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="dim3">Length of dimension #3.</param>
        /// <param name="dim4">Length of dimension #4.</param>
        /// <param name="dim5">Length of dimension #5.</param>
        /// <param name="dim6">Length of dimension #6.</param>
        /// <returns>New ILNumerics array initialized with 1.0.</returns>
        /// <seealso cref="ones{T}(long, long, StorageOrders)"/>
        /// <seealso cref="zeros{T}(long, long, StorageOrders)"/>
        /// <seealso cref="array{T}(T, long, StorageOrders)"/>
        /// <seealso cref="vector{T}(T, T, T, T, T, T, T, T, T, T)"/>
        /// <seealso href="https://ilnumerics.net/ArrayCreation3.html"/>
        internal unsafe static Array<double> ones(long dim0, long dim1, long dim2, long dim3, long dim4, long dim5, long dim6)  {

            var ret = StorageLayer.Storage<double>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, dim4, dim5, dim6, order: StorageOrders.ColumnMajor);
            return onesInternal<double>(ret);
        }
        #endregion

        #region generic (numeric)

        /// <summary>
        /// Creates a square matrix of 1-valued elements.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="rows_columns">Length of dimension #0 and dimension #1 (the number of rows and columns).</param>
        /// <param name="order">[Optional] The storage order for the new array. Default: undefined.</param>
        /// <returns>New ILNumerics array initialized with 1s.</returns>
        /// <remarks>This creates a square matrix with the same number of rows and columns. The elements 
        /// are ordered in undefined order.
        /// <para>This function always creates a matrix, hence the array returned will always have 
        /// two dimensions. Use <see cref="vector{T}(T, T, T, T)"/> or <see cref="array{T}(T, long, StorageOrders)"/> 
        /// for creating arrays with less than two dimension if your setting of <see cref="Settings.ArrayStyle"/> allows it.</para>
        /// <para>Supported element types are all predefined, scalar numeric value types.</para>
        /// <para>If the optional argument <paramref name="order"/> is omitted ILNumerics is free to decide for the storage order. Often, an advantageous 
        /// (in terms of processing performance) storage order is selected according to the current algorithmic context, to the current setting of <see cref="Settings.ArrayStyle"/> 
        /// and other factors.</para>
        /// </remarks>
        /// <seealso cref="zeros{T}(long, long, StorageOrders)"/>
        /// <seealso cref="array{T}(T, long, StorageOrders)"/>
        /// <seealso cref="vector{T}(T, T, T, T, T, T, T, T, T, T)"/>
        /// <seealso cref="ones{T}(long, long, StorageOrders)"/>
        /// <seealso cref="empty{T}(long, long, StorageOrders)"/>
        /// <seealso href="https://ilnumerics.net/ArrayCreation3.html"/>
        /// <exception cref="ArgumentException">if <typeparamref name="T"/> is not supported. Use one of the predefined, scalar value types (<see cref="System.Double"/>, float, int, ...), 
        /// <see cref="ILNumerics.complex"/> or <see cref="ILNumerics.fcomplex"/>.</exception>
        internal unsafe static Array<T> ones<T>(long rows_columns, StorageOrders order = StorageOrders.ColumnMajor) where T : struct, IConvertible {

            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0: rows_columns, dim1: rows_columns, order: order);
            return onesInternal<T>(ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array of ones for numeric element types <typeparamref name="T"/>.
        /// </summary>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="order">[Optional] The storage order for the new array. Default: undefined.</param>
        /// <returns>New ILNumerics array initialized with ones.</returns>
        /// <remarks><para>Supported element types are all predefined, scalar numeric value types.</para>
        /// <para>If the optional argument <paramref name="order"/> is omitted ILNumerics is free to decide for the storage order. Often, an advantageous 
        /// (in terms of processing performance) storage order is selected according to the current algorithmic context, to the current setting of <see cref="Settings.ArrayStyle"/> 
        /// and other factors.</para>
        /// </remarks>
        /// <seealso cref="zeros{T}(long, long, StorageOrders)"/>
        /// <seealso cref="array{T}(T, long, StorageOrders)"/>
        /// <seealso cref="vector{T}(T, T, T, T, T, T, T, T, T, T)"/>
        /// <seealso cref="ones{T}(long, long, StorageOrders)"/>
        /// <seealso cref="empty{T}(long, long, StorageOrders)"/>
        /// <seealso href="https://ilnumerics.net/ArrayCreation3.html"/>
        /// <exception cref="ArgumentException">if <typeparamref name="T"/> is not supported. Use one of the predefined, scalar value types (<see cref="System.Double"/>, float, int, ...), 
        /// <see cref="ILNumerics.complex"/> or <see cref="ILNumerics.fcomplex"/>.</exception>
        internal unsafe static Array<T> ones<T>(long dim0, long dim1, StorageOrders order = StorageOrders.ColumnMajor) where T : struct, IConvertible {

            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, order: order);
            return onesInternal<T>(ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array of ones for numeric element types <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="order">[Optional] The storage order for the new array. Default: undefined.</param>
        /// <returns>New ILNumerics array initialized with ones.</returns>
        /// <remarks><para>Supported element types are all predefined, scalar numeric value types.</para>
        /// <para>If the optional argument <paramref name="order"/> is omitted ILNumerics is free to decide for the storage order. Often, an advantageous 
        /// (in terms of processing performance) storage order is selected according to the current algorithmic context, to the current setting of <see cref="Settings.ArrayStyle"/> 
        /// and other factors.</para>
        /// </remarks>
        /// <seealso cref="zeros{T}(long, long, StorageOrders)"/>
        /// <seealso cref="array{T}(T, long, StorageOrders)"/>
        /// <seealso cref="vector{T}(T, T, T, T, T, T, T, T, T, T)"/>
        /// <seealso cref="ones{T}(long, long, StorageOrders)"/>
        /// <seealso cref="empty{T}(long, long, StorageOrders)"/>
        /// <seealso href="https://ilnumerics.net/ArrayCreation3.html"/>
        /// <exception cref="ArgumentException">if <typeparamref name="T"/> is not supported. Use one of the predefined, scalar value types (<see cref="System.Double"/>, float, int, ...), 
        /// <see cref="ILNumerics.complex"/> or <see cref="ILNumerics.fcomplex"/>.</exception>
        internal unsafe static Array<T> ones<T>(long dim0, long dim1, long dim2, StorageOrders order = StorageOrders.ColumnMajor) where T : struct, IConvertible {

            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, dim2, order: order);
            return onesInternal<T>(ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array of ones for numeric element types <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="dim3">Length of dimension #3.</param>
        /// <param name="order">[Optional] The storage order for the new array. Default: undefined.</param>
        /// <returns>New ILNumerics array initialized with ones.</returns>
        /// <remarks><para>Supported element types are all predefined, scalar numeric value types.</para>
        /// <para>If the optional argument <paramref name="order"/> is omitted ILNumerics is free to decide for the storage order. Often, an advantageous 
        /// (in terms of processing performance) storage order is selected according to the current algorithmic context, to the current setting of <see cref="Settings.ArrayStyle"/> 
        /// and other factors.</para>
        /// </remarks>
        /// <seealso cref="zeros{T}(long, long, StorageOrders)"/>
        /// <seealso cref="array{T}(T, long, StorageOrders)"/>
        /// <seealso cref="vector{T}(T, T, T, T, T, T, T, T, T, T)"/>
        /// <seealso cref="ones{T}(long, long, StorageOrders)"/>
        /// <seealso cref="empty{T}(long, long, StorageOrders)"/>
        /// <seealso href="https://ilnumerics.net/ArrayCreation3.html"/>
        /// <exception cref="ArgumentException">if <typeparamref name="T"/> is not supported. Use one of the predefined, scalar value types (<see cref="System.Double"/>, float, int, ...), 
        /// <see cref="ILNumerics.complex"/> or <see cref="ILNumerics.fcomplex"/>.</exception>
        internal unsafe static Array<T> ones<T>(long dim0, long dim1, long dim2, long dim3, StorageOrders order = StorageOrders.ColumnMajor) where T : struct, IConvertible {

            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, order: order);
            return onesInternal<T>(ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array of ones for numeric element types <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="dim3">Length of dimension #3.</param>
        /// <param name="dim4">Length of dimension #4.</param>
        /// <param name="order">[Optional] The storage order for the new array. Default: undefined.</param>
        /// <returns>New ILNumerics array initialized with ones.</returns>
        /// <remarks><para>Supported element types are all predefined, scalar numeric value types.</para>
        /// <para>If the optional argument <paramref name="order"/> is omitted ILNumerics is free to decide for the storage order. Often, an advantageous 
        /// (in terms of processing performance) storage order is selected according to the current algorithmic context, to the current setting of <see cref="Settings.ArrayStyle"/> 
        /// and other factors.</para>
        /// </remarks>
        /// <seealso cref="zeros{T}(long, long, StorageOrders)"/>
        /// <seealso cref="array{T}(T, long, StorageOrders)"/>
        /// <seealso cref="vector{T}(T, T, T, T, T, T, T, T, T, T)"/>
        /// <seealso cref="ones{T}(long, long, StorageOrders)"/>
        /// <seealso cref="empty{T}(long, long, StorageOrders)"/>
        /// <seealso href="https://ilnumerics.net/ArrayCreation3.html"/>
        /// <exception cref="ArgumentException">if <typeparamref name="T"/> is not supported. Use one of the predefined, scalar value types (<see cref="System.Double"/>, float, int, ...), 
        /// <see cref="ILNumerics.complex"/> or <see cref="ILNumerics.fcomplex"/>.</exception>
        internal unsafe static Array<T> ones<T>(long dim0, long dim1, long dim2, long dim3, long dim4, StorageOrders order = StorageOrders.ColumnMajor) where T : struct, IConvertible {

            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, dim4, order: order);
            return onesInternal<T>(ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array of ones for numeric element types <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="dim3">Length of dimension #3.</param>
        /// <param name="dim4">Length of dimension #4.</param>
        /// <param name="dim5">Length of dimension #5.</param>
        /// <param name="order">[Optional] The storage order for the new array. Default: undefined.</param>
        /// <returns>New ILNumerics array initialized with ones.</returns>
        /// <remarks><para>Supported element types are all predefined, scalar numeric value types.</para>
        /// <para>If the optional argument <paramref name="order"/> is omitted ILNumerics is free to decide for the storage order. Often, an advantageous 
        /// (in terms of processing performance) storage order is selected according to the current algorithmic context, to the current setting of <see cref="Settings.ArrayStyle"/> 
        /// and other factors.</para>
        /// </remarks>
        /// <seealso cref="zeros{T}(long, long, StorageOrders)"/>
        /// <seealso cref="array{T}(T, long, StorageOrders)"/>
        /// <seealso cref="vector{T}(T, T, T, T, T, T, T, T, T, T)"/>
        /// <seealso cref="ones{T}(long, long, StorageOrders)"/>
        /// <seealso cref="empty{T}(long, long, StorageOrders)"/>
        /// <seealso href="https://ilnumerics.net/ArrayCreation3.html"/>
        /// <exception cref="ArgumentException">if <typeparamref name="T"/> is not supported. Use one of the predefined, scalar value types (<see cref="System.Double"/>, float, int, ...), 
        /// <see cref="ILNumerics.complex"/> or <see cref="ILNumerics.fcomplex"/>.</exception>
        internal unsafe static Array<T> ones<T>(long dim0, long dim1, long dim2, long dim3, long dim4, long dim5, StorageOrders order = StorageOrders.ColumnMajor) where T : struct, IConvertible {

            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, dim4, dim5, order: order);
            return onesInternal<T>(ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array of ones for numeric element types <typeparamref name="T"/>.
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
        /// <returns>New ILNumerics array initialized with ones.</returns>
        /// <remarks><para>Supported element types are all predefined, scalar numeric value types.</para>
        /// <para>If the optional argument <paramref name="order"/> is omitted ILNumerics is free to decide for the storage order. Often, an advantageous 
        /// (in terms of processing performance) storage order is selected according to the current algorithmic context, to the current setting of <see cref="Settings.ArrayStyle"/> 
        /// and other factors.</para>
        /// </remarks>
        /// <seealso cref="zeros{T}(long, long, StorageOrders)"/>
        /// <seealso cref="array{T}(T, long, StorageOrders)"/>
        /// <seealso cref="vector{T}(T, T, T, T, T, T, T, T, T, T)"/>
        /// <seealso cref="ones{T}(long, long, StorageOrders)"/>
        /// <seealso cref="empty{T}(long, long, StorageOrders)"/>
        /// <seealso href="https://ilnumerics.net/ArrayCreation3.html"/>
        /// <exception cref="ArgumentException">if <typeparamref name="T"/> is not supported. Use one of the predefined, scalar value types (<see cref="System.Double"/>, float, int, ...), 
        /// <see cref="ILNumerics.complex"/> or <see cref="ILNumerics.fcomplex"/>.</exception>
        internal unsafe static Array<T> ones<T>(long dim0, long dim1, long dim2, long dim3, long dim4, long dim5, long dim6, StorageOrders order = StorageOrders.ColumnMajor) where T : struct, IConvertible {

            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, dim4, dim5, dim6, order: order);
            return onesInternal<T>(ret);
        }

        /// <summary>
        /// Creates a new array of ones '1' with the same size as <paramref name="size"/>. 
        /// </summary>
        /// <typeparam name="T">Element type for the new array.</typeparam>
        /// <param name="size">Number and lengths of the dimensions for the new array.</param>
        /// <param name="order">[Optional] Storage order for the new array. Default: undefined.</param>
        /// <returns>New array of the specified size and storage order, initialized with '1'.</returns>
        /// <remarks><para>Supported element types are all predefined, scalar numeric value types.</para>
        /// <para>If the optional argument <paramref name="order"/> is omitted ILNumerics is free to decide for the storage order. Often, an advantageous 
        /// (in terms of processing performance) storage order is selected according to the current algorithmic context, to the current setting of <see cref="Settings.ArrayStyle"/> 
        /// and other factors.</para>
        /// </remarks>
        /// <seealso cref="zeros{T}(long, long, StorageOrders)"/>
        /// <seealso cref="array{T}(T, long, StorageOrders)"/>
        /// <seealso cref="vector{T}(T, T, T, T, T, T, T, T, T, T)"/>
        /// <seealso cref="ones{T}(long, long, StorageOrders)"/>
        /// <seealso cref="empty{T}(long, long, StorageOrders)"/>
        /// <seealso href="https://ilnumerics.net/ArrayCreation3.html"/>
        /// <exception cref="ArgumentException">if <typeparamref name="T"/> is not supported. Use one of the predefined, scalar value types (<see cref="System.Double"/>, float, int, ...), 
        /// <see cref="ILNumerics.complex"/> or <see cref="ILNumerics.fcomplex"/>.</exception>
        internal unsafe static Array<T> ones<T>(Size size, StorageOrders order = StorageOrders.ColumnMajor) where T : struct {
            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetDimensionLengths(size.GetBSD(false), order: order);
            return onesInternal<T>(ret);
        }

        /// <summary>
        /// Creates a new array of ones '1' with a size determined by <paramref name="size"/>. 
        /// </summary>
        /// <typeparam name="T">Element type for the new array.</typeparam>
        /// <param name="size">Vector with lengths of the dimensions of the new array.</param>
        /// <param name="order">[Optional] Storage order for the new array. Default: undefined.</param>
        /// <returns>New array of the specified size and storage order, initialized with '1'.</returns>
        /// <remarks><para>Supported element types are all predefined, scalar numeric value types.</para>
        /// <para>If the optional argument <paramref name="order"/> is omitted ILNumerics is free to decide for the storage order. Often, an advantageous 
        /// (in terms of processing performance) storage order is selected according to the current algorithmic context, to the current setting of <see cref="Settings.ArrayStyle"/> 
        /// and other factors.</para>
        /// </remarks>
        /// <seealso cref="zeros{T}(long, long, StorageOrders)"/>
        /// <seealso cref="array{T}(T, long, StorageOrders)"/>
        /// <seealso cref="vector{T}(T, T, T, T, T, T, T, T, T, T)"/>
        /// <seealso cref="ones{T}(long, long, StorageOrders)"/>
        /// <seealso cref="empty{T}(long, long, StorageOrders)"/>
        /// <seealso href="https://ilnumerics.net/ArrayCreation3.html"/>
        /// <exception cref="ArgumentException">if <typeparamref name="T"/> is not supported. Use one of the predefined, scalar value types (<see cref="System.Double"/>, float, int, ...), 
        /// <see cref="ILNumerics.complex"/> or <see cref="ILNumerics.fcomplex"/>.</exception>
        internal unsafe static Array<T> ones<T>(InArray<long> size, StorageOrders order = StorageOrders.ColumnMajor) where T : struct {
            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(size, order: order);
            return onesInternal<T>(ret);
        }
        /// <summary> 
        /// Creates a new array of ones '1' with a size determined by <paramref name="size"/>. 
        /// </summary>
        /// <typeparam name="T">Element type for the new array. This must be a value type.</typeparam>
        /// <param name="size">Variable length <see cref="System.Array"/> or comma separated list with lengths of the dimensions of the new array.</param>
        /// <returns>New array of the specified size and undefined storage order, initialized with '1'.</returns>
        /// <remarks><para>Supported element types are all predefined, scalar numeric value types.</para>
        /// <para><paramref name="size"/> must be not null. Its lengths determines the number of dimensions of the new array. Negative element values are not allowed.</para>
        /// <para>Arrays returned from this overload are in undefined storage order. Use <see cref="ones{T}(InArray{long}, StorageOrders)"/> for 
        /// determining the storage order explicitly.</para>
        /// <para>Since the variable length parameter list and the 'params' keyword (C#) implicitly allocates new storage under the control of the 
        /// GC consider using one of the <see cref="vector{T}(T)"/> overloads and <see cref="ones{T}(InArray{long}, StorageOrders)"/> instead to provide the dimension lengths. 
        /// This is recommended when high-performance is important.</para>
        /// </remarks>
        /// <seealso cref="zeros{T}(long, long, StorageOrders)"/>
        /// <seealso cref="array{T}(T, long, StorageOrders)"/>
        /// <seealso cref="vector{T}(T, T, T, T, T, T, T, T, T, T)"/>
        /// <seealso cref="ones{T}(long, long, StorageOrders)"/>
        /// <seealso cref="empty{T}(long, long, StorageOrders)"/>
        /// <seealso cref="Size.MaxNumberOfDimensions"/>
        /// <seealso href="https://ilnumerics.net/ArrayCreation3.html"/>
        /// <exception cref="ArgumentException">if <typeparamref name="T"/> is not supported. Use one of the predefined, scalar value types (<see cref="System.Double"/>, float, int, ...), 
        /// <see cref="ILNumerics.complex"/> or <see cref="ILNumerics.fcomplex"/>.</exception>
        /// <exception cref="ArgumentException">if <paramref name="size"/> is null, has more elements than <see cref="Size.MaxNumberOfDimensions"/> or contains negative values.</exception>
        internal static Array<T> ones<T>(params long[] size) where T : struct {
            return ones<T>(vector(size)); 
        }

        #endregion

        
        private unsafe static Array<T> onesInternal<T>(Storage<T> ret) where T : struct {
            System.Diagnostics.Debug.Assert(ret != null && ret.S.IsContinuous);
            var outLen = ret.S.NumberOfElements;
            System.Diagnostics.Debug.Assert(ret.Handles != null); 
            //ret.m_handles = CountableArray.Create();
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<T>((ulong)outLen);

            uint elemLength = StorageLayer.Storage<T>.SizeOfT;

            if (false) {
                #region HYCALPER LOOPSTART 
                /*!HC:TYPELIST:
                <hycalper>
                    <type>
                        <source locate="here">
                            double
                        </source>
                        <destination>float</destination>
                        <destination>complex</destination>
                        <destination>fcomplex</destination>
                        <destination>sbyte</destination>
                        <destination>byte</destination>
                        <destination>short</destination>
                        <destination>ushort</destination>
                        <destination>int</destination>
                        <destination>uint</destination>
                        <destination>long</destination>
                        <destination>ulong</destination>
                    </type>
                </hycalper>
                */

            } else if (Storage<T>.ElementInstance is double) {
                double* p = (double*)ret.Handles[0].Pointer + ret.S.BaseOffset;

                for (long i = outLen; i-- > 0; ) {
                    *p++ = 1;
                }
                #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
               

            } else if (Storage<T>.ElementInstance is ulong) {
                ulong* p = (ulong*)ret.Handles[0].Pointer + ret.S.BaseOffset;

                for (long i = outLen; i-- > 0; ) {
                    *p++ = 1;
                }
               

            } else if (Storage<T>.ElementInstance is long) {
                long* p = (long*)ret.Handles[0].Pointer + ret.S.BaseOffset;

                for (long i = outLen; i-- > 0; ) {
                    *p++ = 1;
                }
               

            } else if (Storage<T>.ElementInstance is uint) {
                uint* p = (uint*)ret.Handles[0].Pointer + ret.S.BaseOffset;

                for (long i = outLen; i-- > 0; ) {
                    *p++ = 1;
                }
               

            } else if (Storage<T>.ElementInstance is int) {
                int* p = (int*)ret.Handles[0].Pointer + ret.S.BaseOffset;

                for (long i = outLen; i-- > 0; ) {
                    *p++ = 1;
                }
               

            } else if (Storage<T>.ElementInstance is ushort) {
                ushort* p = (ushort*)ret.Handles[0].Pointer + ret.S.BaseOffset;

                for (long i = outLen; i-- > 0; ) {
                    *p++ = 1;
                }
               

            } else if (Storage<T>.ElementInstance is short) {
                short* p = (short*)ret.Handles[0].Pointer + ret.S.BaseOffset;

                for (long i = outLen; i-- > 0; ) {
                    *p++ = 1;
                }
               

            } else if (Storage<T>.ElementInstance is byte) {
                byte* p = (byte*)ret.Handles[0].Pointer + ret.S.BaseOffset;

                for (long i = outLen; i-- > 0; ) {
                    *p++ = 1;
                }
               

            } else if (Storage<T>.ElementInstance is sbyte) {
                sbyte* p = (sbyte*)ret.Handles[0].Pointer + ret.S.BaseOffset;

                for (long i = outLen; i-- > 0; ) {
                    *p++ = 1;
                }
               

            } else if (Storage<T>.ElementInstance is fcomplex) {
                fcomplex* p = (fcomplex*)ret.Handles[0].Pointer + ret.S.BaseOffset;

                for (long i = outLen; i-- > 0; ) {
                    *p++ = 1;
                }
               

            } else if (Storage<T>.ElementInstance is complex) {
                complex* p = (complex*)ret.Handles[0].Pointer + ret.S.BaseOffset;

                for (long i = outLen; i-- > 0; ) {
                    *p++ = 1;
                }
               

            } else if (Storage<T>.ElementInstance is float) {
                float* p = (float*)ret.Handles[0].Pointer + ret.S.BaseOffset;

                for (long i = outLen; i-- > 0; ) {
                    *p++ = 1;
                }

#endregion HYCALPER AUTO GENERATED CODE
            } else {
                throw new ArgumentException($"The element type '{typeof(T).Name}' is not supported for ones()."); 
            }
            return ret.RetArray;
        }


    }
}
