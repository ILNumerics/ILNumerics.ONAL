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
using ILNumerics.Core.Functions.Builtin;
using ILNumerics.Core.Global;
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.Arrays {

    public abstract partial class ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT>   // T1 must not read "T" to prevent from conflicts with the .T (transpose) property.
        : BaseArray<T1>, IEnumerable<T1>
        where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {

        #region uint overloads - can be re-enabled once extensive profiling has proven that this really gives some speedup!
                // changes performed to long overload in the meantime: Reshape returns storage instead of RetT. 

        ///// <summary>
        ///// Create reshaped, 1 dimensional version of this array. Flattens the array to a vector.
        ///// </summary>
        ///// <param name="d0">Length of the vector produced. This must be equal to <see cref="Size.NumberOfElements"/> or a negative number.</param>
        ///// <param name="order">[Optional] Storage order for the new array. Default: null (<see cref="Settings.DefaultStorageOrder"/>).</param>
        ///// <returns>A new array with the same values as this array, lined up in a 1-dimensional vector.</returns>
        ///// <remarks><para>The (redundant) parameter <paramref name="d0"/> equals the number of elements along the first 
        ///// dimension for the returned array. An overload exists which allows to infer the correct value automatically: 
        ///// <see cref="Reshape(long, StorageOrders?)"/>.</para>
        ///// <para>The setting of <see cref="Settings.MinNumberOfArrayDimensions"/> is taken into account. 
        ///// Therefore - by default - the array returned will have 2 dimensions, since the default value for 
        ///// <see cref="Settings.MinNumberOfArrayDimensions"/> is 2.</para>
        ///// <para>The storage order for both: the order for reading 
        ///// the elements from this array and storing the element into the reshaped array is 
        ///// determined as follows: </para>
        ///// <list type="bullets">
        ///// <item>If <paramref name="order"/> is specified (<see cref="StorageOrders.ColumnMajor"/>
        ///// or <see cref="StorageOrders.RowMajor"/>) its value is used for the output. A copy is 
        ///// made only if really needed.</item>
        ///// <item>Otherwise, the value of <see cref="Settings.DefaultStorageOrder"/> determins the 
        ///// target storage order.</item>
        ///// </list>
        ///// <para>A copy of the elements is required for non-continous arrays and - except for 
        ///// vector shaped arrays - for unmatching order settings. Otherwise the returned 
        ///// array references the same memory as this array with a modified size descriptor.</para>
        ///// </remarks>
        ///// <see cref="Reshape(uint, uint, StorageOrders?)"/>
        ///// <see cref="Reshape(long, long, StorageOrders?)"/>
        ///// <see cref="Reshape(uint[], StorageOrders?)"/>
        ///// <see cref="Reshape(long[], StorageOrders?)"/>
        //public virtual RetT Reshape(uint d0, StorageOrders? order = null) {
        //    return m_storage.Reshape(d0, order); 
        //}

        ///// <summary>
        ///// Create reshaped version of this storage.
        ///// </summary>
        ///// <param name="d0">Number of elements along dim 0.</param>
        ///// <param name="d1">Number of elements along dim 1.</param>
        ///// <param name="order">[Optional] Storage order for the output array. Default: 
        ///// null (<see cref="Settings.DefaultStorageOrder"/>).</param>
        ///// <returns>Reshaped array.</returns>
        ///// <remarks><para>All dimensions of the target shape must be specified explicitly. In order to 
        ///// infer one of the dimensions automatically, use <see cref="Reshape(long,long,StorageOrders?)"/> instead.</para>
        ///// <para>The storage order for both: the order for reading 
        ///// the elements from this array and storing the element into the reshaped array is 
        ///// determined as follows: </para>
        ///// <list type="bullets">
        ///// <item>If <paramref name="order"/> is specified (<see cref="StorageOrders.ColumnMajor"/>
        ///// or <see cref="StorageOrders.RowMajor"/>) its value is used for the output. A copy is 
        ///// made only if really needed.</item>
        ///// <item>Otherwise, the value of <see cref="Settings.DefaultStorageOrder"/> determins the 
        ///// target storage order.</item>
        ///// </list>
        ///// <para>A copy of the elements is required for non-continous arrays and - except for 
        ///// vector shaped arrays - for unmatching order settings. Otherwise the returned 
        ///// array references the same memory as this array and uses another size descriptor only.</para>
        ///// </remarks>
        ///// <see cref="Reshape(uint, StorageOrders?)"/>
        ///// <see cref="Reshape(uint[], StorageOrders?)"/>
        ///// <see cref="Reshape(long, StorageOrders?)"/>
        ///// <see cref="Reshape(long[], StorageOrders?)"/>
        //public virtual RetT Reshape(uint d0, uint d1, StorageOrders? order = null) {
        //    return m_storage.Reshape(d0, d1, order);
        //}
        ///// <summary>
        ///// Create reshaped version of this storage.
        ///// </summary>
        ///// <param name="d0">Number of elements along dim 0.</param>
        ///// <param name="d1">Number of elements along dim 1.</param>
        ///// <param name="d2">Number of elements along dim 2.</param>
        ///// <param name="order">[Optional] Storage order for the output array. Default: 
        ///// null (Settings.DefaultStorageOrder).</param>
        ///// <returns>Reshaped array.</returns>
        ///// <remarks><para>All dimensions of the target shape must be specified explicitly. In order to 
        ///// infer one of the dimensions automatically, use <see cref="Reshape(long,long,long,StorageOrders?)"/> instead.</para>
        ///// <para>The storage order for both: the order for reading 
        ///// the elements from this array and storing the element into the reshaped array is 
        ///// determined as follows: </para>
        ///// <list type="bullets">
        ///// <item>If <paramref name="order"/> is specified (<see cref="StorageOrders.ColumnMajor"/>
        ///// or <see cref="StorageOrders.RowMajor"/>) its value is used for the output. A copy is 
        ///// made only if really needed.</item>
        ///// <item>Otherwise, the value of <see cref="Settings.DefaultStorageOrder"/> determins the 
        ///// target storage order.</item>
        ///// </list>
        ///// <para>A copy of the elements is required for non-continous arrays and - except for 
        ///// vector shaped arrays - for unmatching order settings. Otherwise the returned 
        ///// array references the same memory as this array and uses another size descriptor only.</para>
        ///// </remarks>
        ///// <see cref="Reshape(uint, StorageOrders?)"/>
        ///// <see cref="Reshape(uint[], StorageOrders?)"/>
        ///// <see cref="Reshape(long, StorageOrders?)"/>
        ///// <see cref="Reshape(long[], StorageOrders?)"/>
        //public virtual RetT Reshape(uint d0, uint d1, uint d2, StorageOrders? order = null) {
        //    return m_storage.Reshape(d0, d1, d2, order);
        //}
        ///// <summary>
        ///// Create reshaped version of this storage.
        ///// </summary>
        ///// <param name="d0">Number of elements along dim 0.</param>
        ///// <param name="d1">Number of elements along dim 1.</param>
        ///// <param name="d2">Number of elements along dim 2.</param>
        ///// <param name="d3">Number of elements along dim 3.</param>
        ///// <param name="order">[Optional] Storage order for the output array. Default: 
        ///// null (Settings.DefaultStorageOrder).</param>
        ///// <returns>Reshaped array.</returns>
        ///// <remarks><para>All dimensions of the target shape must be specified explicitly. In order to 
        ///// infer one of the dimensions automatically, use <see cref="Reshape(long,long,long,long,StorageOrders?)"/> instead.</para>
        ///// <para>The storage order for both: the order for reading 
        ///// the elements from this array and storing the element into the reshaped array is 
        ///// determined as follows: </para>
        ///// <list type="bullets">
        ///// <item>If <paramref name="order"/> is specified (<see cref="StorageOrders.ColumnMajor"/>
        ///// or <see cref="StorageOrders.RowMajor"/>) its value is used for the output. A copy is 
        ///// made only if really needed.</item>
        ///// <item>Otherwise, the value of <see cref="Settings.DefaultStorageOrder"/> determins the 
        ///// target storage order.</item>
        ///// </list>
        ///// <para>A copy of the elements is required for non-continous arrays and - except for 
        ///// vector shaped arrays - for unmatching order settings. Otherwise the returned 
        ///// array references the same memory as this array and uses another size descriptor only.</para>
        ///// </remarks>
        ///// <see cref="Reshape(uint, StorageOrders?)"/>
        ///// <see cref="Reshape(uint[], StorageOrders?)"/>
        ///// <see cref="Reshape(long, StorageOrders?)"/>
        ///// <see cref="Reshape(long[], StorageOrders?)"/>
        //public virtual RetT Reshape(uint d0, uint d1, uint d2, uint d3, StorageOrders? order = null) {
        //    return m_storage.Reshape(d0, d1, d2, d3, order);
        //}
        ///// <summary>
        ///// Create reshaped version of this storage.
        ///// </summary>
        ///// <param name="d0">Number of elements along dim 0.</param>
        ///// <param name="d1">Number of elements along dim 1.</param>
        ///// <param name="d2">Number of elements along dim 2.</param>
        ///// <param name="d3">Number of elements along dim 3.</param>
        ///// <param name="d4">Number of elements along dim 4.</param>
        ///// <param name="order">[Optional] Storage order for the output array. Default: 
        ///// null (Settings.DefaultStorageOrder).</param>
        ///// <returns>Reshaped array.</returns>
        ///// <remarks><para>All dimensions of the target shape must be specified explicitly. In order to 
        ///// infer one of the dimensions automatically, use <see cref="Reshape(long,long,long,long,long,StorageOrders?)"/> instead.</para>
        ///// <para>The storage order for both: the order for reading 
        ///// the elements from this array and storing the element into the reshaped array is 
        ///// determined as follows: </para>
        ///// <list type="bullets">
        ///// <item>If <paramref name="order"/> is specified (<see cref="StorageOrders.ColumnMajor"/>
        ///// or <see cref="StorageOrders.RowMajor"/>) its value is used for the output. A copy is 
        ///// made only if really needed.</item>
        ///// <item>Otherwise, the value of <see cref="Settings.DefaultStorageOrder"/> determins the 
        ///// target storage order.</item>
        ///// </list>
        ///// <para>A copy of the elements is required for non-continous arrays and - except for 
        ///// vector shaped arrays - for unmatching order settings. Otherwise the returned 
        ///// array references the same memory as this array and uses another size descriptor only.</para>
        ///// </remarks>
        ///// <see cref="Reshape(uint, StorageOrders?)"/>
        ///// <see cref="Reshape(uint[], StorageOrders?)"/>
        ///// <see cref="Reshape(long, StorageOrders?)"/>
        ///// <see cref="Reshape(long[], StorageOrders?)"/>
        //public virtual RetT Reshape(uint d0, uint d1, uint d2, uint d3, uint d4, StorageOrders? order = null) {
        //    return m_storage.Reshape(d0, d1, d2, d3, d4, order);
        //}
        ///// <summary>
        ///// Create reshaped version of this storage.
        ///// </summary>
        ///// <param name="d0">Number of elements along dim 0.</param>
        ///// <param name="d1">Number of elements along dim 1.</param>
        ///// <param name="d2">Number of elements along dim 2.</param>
        ///// <param name="d3">Number of elements along dim 3.</param>
        ///// <param name="d4">Number of elements along dim 4.</param>
        ///// <param name="d5">Number of elements along dim 5.</param>
        ///// <param name="order">[Optional] Storage order for the output array. Default: 
        ///// null (Settings.DefaultStorageOrder).</param>
        ///// <returns>Reshaped array.</returns>
        ///// <remarks><para>All dimensions of the target shape must be specified explicitly. In order to 
        ///// infer one of the dimensions automatically, use <see cref="Reshape(long,long,long,long,long,long,StorageOrders?)"/> instead.</para>
        ///// <para>The storage order for both: the order for reading 
        ///// the elements from this array and storing the element into the reshaped array is 
        ///// determined as follows: </para>
        ///// <list type="bullets">
        ///// <item>If <paramref name="order"/> is specified (<see cref="StorageOrders.ColumnMajor"/>
        ///// or <see cref="StorageOrders.RowMajor"/>) its value is used for the output. A copy is 
        ///// made only if really needed.</item>
        ///// <item>Otherwise, the value of <see cref="Settings.DefaultStorageOrder"/> determins the 
        ///// target storage order.</item>
        ///// </list>
        ///// <para>A copy of the elements is required for non-continous arrays and - except for 
        ///// vector shaped arrays - for unmatching order settings. Otherwise the returned 
        ///// array references the same memory as this array and uses another size descriptor only.</para>
        ///// </remarks>
        ///// <see cref="Reshape(uint, StorageOrders?)"/>
        ///// <see cref="Reshape(uint[], StorageOrders?)"/>
        ///// <see cref="Reshape(long, StorageOrders?)"/>
        ///// <see cref="Reshape(long[], StorageOrders?)"/>
        //public virtual RetT Reshape(uint d0, uint d1, uint d2, uint d3, uint d4, uint d5, StorageOrders? order = null) {
        //    return m_storage.Reshape(d0, d1, d2, d3, d4, d5, order);
        //}
        ///// <summary>
        ///// Create reshaped version of this storage.
        ///// </summary>
        ///// <param name="d0">Number of elements along dim 0.</param>
        ///// <param name="d1">Number of elements along dim 1.</param>
        ///// <param name="d2">Number of elements along dim 2.</param>
        ///// <param name="d3">Number of elements along dim 3.</param>
        ///// <param name="d4">Number of elements along dim 4.</param>
        ///// <param name="d5">Number of elements along dim 5.</param>
        ///// <param name="d6">Number of elements along dim 6.</param>
        ///// <param name="order">[Optional] Storage order for the output array. Default: 
        ///// null (Settings.DefaultStorageOrder).</param>
        ///// <returns>Reshaped array.</returns>
        ///// <remarks><para>All dimensions of the target shape must be specified explicitly. In order to 
        ///// infer one of the dimensions automatically, use <see cref="Reshape(long,long,long,long,long,long,long,StorageOrders?)"/> instead.</para>
        ///// <para>The storage order for both: the order for reading 
        ///// the elements from this array and storing the element into the reshaped array is 
        ///// determined as follows: </para>
        ///// <list type="bullets">
        ///// <item>If <paramref name="order"/> is specified (<see cref="StorageOrders.ColumnMajor"/>
        ///// or <see cref="StorageOrders.RowMajor"/>) its value is used for reading and writing. A copy is 
        ///// made only if really needed.</item>
        ///// <item>Otherwise, the value of <see cref="Settings.DefaultStorageOrder"/> determins the 
        ///// target storage order.</item>
        ///// </list>
        ///// <para>A copy of the elements is required for non-continous arrays and - except for 
        ///// vector shaped arrays - for unmatching order settings. Otherwise the returned 
        ///// array references the same memory as this array and uses another size descriptor only.</para>
        ///// </remarks>
        ///// <see cref="Reshape(uint, StorageOrders?)"/>
        ///// <see cref="Reshape(uint[], StorageOrders?)"/>
        ///// <see cref="Reshape(long, StorageOrders?)"/>
        ///// <see cref="Reshape(long[], StorageOrders?)"/>
        //public virtual RetT Reshape(uint d0, uint d1, uint d2, uint d3, uint d4, uint d5, uint d6, StorageOrders? order = null) {
        //    return m_storage.Reshape(d0, d1, d2, d3, d4, d5, d6, order);
        //}

        /// <summary>
        /// Creates reshaped version, accepts dimension lengths as array.
        /// </summary>
        /// <param name="dimLengths">Lengths array.</param>
        /// <param name="order">Storage order for reading and writing</param>
        /// <returns>Reshaped array.</returns>
        /// <remarks><para>All dimensions of the target shape must be specified explicitly. In order to 
        /// infer one of the dimensions automatically, use <see cref="Reshape(long[],StorageOrders?)"/> instead.</para>
        /// <para>This function is tested for up to 7 dimensions only!</para>
        /// <para>This function is provided for such situations where the size / shape 
        /// of the target array is computed automatically. Here, it is convenient to 
        /// provide the dimension lengths as a single array parameter.</para>
        /// <para>The same limitations exist as for the overloads with individual dimension parameters.</para>
        /// </remarks>
        /// <see cref="Reshape(long,long,long, StorageOrders?)"/>
        /// <see cref="Reshape(long[])"/>
        /// <see cref="Reshape{IndT}(BaseArray{IndT}, StorageOrders?)"/>
        /// <see cref="Reshape(long[], StorageOrders?)"/>
        /// <exception cref="ArgumentException"> if <paramref name="dimLengths"/> is null or longer than the maximum number of dimensions supported: <see cref="Size.MaxNumberOfDimensions"/>.</exception>
        public RetT Reshape(uint[] dimLengths, StorageOrders? order = null) {
            using var _1 = ReaderLock.Create(this, out var storage);
            return storage.Reshape(dimLengths, order).RetArray;
        }

        #endregion uint overloads

        #region long overloads

        /// <summary>
        /// Create reshaped, 1 dimensional version of this array. Flattens the array to a vector.
        /// </summary>
        /// <param name="d0">Length of the vector produced. This must be equal to <see cref="Size.NumberOfElements"/> or a negative number.</param>
        /// <param name="order">[Optional] Storage order for the new array. Default: null (<see cref="Settings.DefaultStorageOrder"/>).</param>
        /// <returns>A new array with the same values as this array, lined up in a 1-dimensional vector.</returns>
        /// <remarks><para>The (redundant) parameter <paramref name="d0"/> indicates the number of elements along the first 
        /// dimension for the returned array. If <paramref name="d0"/> is positive, its value must equal the number of
        /// elements in this array. If <paramref name="d0"/> is negative, the correct number of elements is substituted automatically.</para>
        /// <para>The setting of <see cref="Settings.MinNumberOfArrayDimensions"/> is taken into account. 
        /// Therefore - by default - the array returned will have 2 dimensions, since the default value for 
        /// <see cref="Settings.MinNumberOfArrayDimensions"/> is 2.</para>
        /// <para>The storage order for both: the order for reading 
        /// the elements from this array and storing the element into the reshaped array is 
        /// determined as follows: </para>
        /// <list type="bullets">
        /// <item>If <paramref name="order"/> is specified (<see cref="StorageOrders.ColumnMajor"/>
        /// or <see cref="StorageOrders.RowMajor"/>) its value is used for the output. A copy is 
        /// made only if really needed.</item>
        /// <item>Otherwise, the value of <see cref="Settings.DefaultStorageOrder"/> determins the 
        /// target storage order.</item>
        /// </list>
        /// <para>A copy of the elements is required for non-continous arrays and - except for 
        /// vector shaped arrays - for unmatching order settings. Otherwise the returned 
        /// array references the same memory as this array with a modified size descriptor.</para>
        /// </remarks>
        /// <see cref="Reshape(long, long, StorageOrders?)"/>
        /// <see cref="Reshape{IndT}(BaseArray{IndT}, StorageOrders?)"/>
        /// <see cref="Reshape(long[], StorageOrders?)"/>
        public RetT Reshape(long d0, StorageOrders? order = null) {
            using var _1 = ReaderLock.Create(this, out var storage); 
            return storage.Reshape(d0, order).RetArray;
        }

        /// <summary>
        /// Create reshaped version of this storage.
        /// </summary>
        /// <param name="d0">Number of elements along dim 0.</param>
        /// <param name="d1">Number of elements along dim 1.</param>
        /// <param name="order">[Optional] Storage order for the output array. Default: 
        /// null (Settings.DefaultStorageOrder).</param>
        /// <returns>Reshaped array.</returns>
        /// <remarks><para>The storage order for both: the order for reading 
        /// the elements from this array and storing the element into the reshaped array is 
        /// determined as follows: </para>
        /// <list type="bullets">
        /// <item>If <paramref name="order"/> is specified (<see cref="StorageOrders.ColumnMajor"/>
        /// or <see cref="StorageOrders.RowMajor"/>) its value is used for the output. A copy is 
        /// made only if really needed.</item>
        /// <item>Otherwise, the value of <see cref="Settings.DefaultStorageOrder"/> determins the 
        /// target storage order.</item>
        /// </list>
        /// <para>A copy of the elements is required for non-continous arrays and - except for 
        /// vector shaped arrays - for unmatching order settings. Otherwise the returned 
        /// array references the same memory as this array and uses another size descriptor only.</para>
        /// </remarks>
        /// <see cref="Reshape(long, long, StorageOrders?)"/>
        /// <see cref="Reshape{IndT}(BaseArray{IndT}, StorageOrders?)"/>
        /// <see cref="Reshape(long[], StorageOrders?)"/>
        public RetT Reshape(long d0, long d1, StorageOrders? order = null) {
            using var _1 = ReaderLock.Create(this, out var storage);
            return storage.Reshape(d0, d1, order).RetArray;
        }
        /// <summary>
        /// Create reshaped version of this storage.
        /// </summary>
        /// <param name="d0">Number of elements along dim 0.</param>
        /// <param name="d1">Number of elements along dim 1.</param>
        /// <param name="d2">Number of elements along dim 2.</param>
        /// <param name="order">[Optional] Storage order for the output array. Default: 
        /// null (Settings.DefaultStorageOrder).</param>
        /// <returns>Reshaped array.</returns>
        /// <remarks><para>The storage order for both: the order for reading 
        /// the elements from this array and storing the element into the reshaped array is 
        /// determined as follows: </para>
        /// <list type="bullets">
        /// <item>If <paramref name="order"/> is specified (<see cref="StorageOrders.ColumnMajor"/>
        /// or <see cref="StorageOrders.RowMajor"/>) its value is used for the output. A copy is 
        /// made only if really needed.</item>
        /// <item>Otherwise, the value of <see cref="Settings.DefaultStorageOrder"/> determins the 
        /// target storage order.</item>
        /// </list>
        /// <para>A copy of the elements is required for non-continous arrays and - except for 
        /// vector shaped arrays - for unmatching order settings. Otherwise the returned 
        /// array references the same memory as this array and uses another size descriptor only.</para>
        /// </remarks>
        /// <see cref="Reshape{IndT}(BaseArray{IndT}, StorageOrders?)"/>
        /// <see cref="Reshape(long, StorageOrders?)"/>
        /// <see cref="Reshape(long[], StorageOrders?)"/>
        public RetT Reshape(long d0, long d1, long d2, StorageOrders? order = null) {
            using var _1 = ReaderLock.Create(this, out var storage);
            return storage.Reshape(d0, d1, d2, order).RetArray;
        }
        /// <summary>
        /// Create reshaped version of this storage.
        /// </summary>
        /// <param name="d0">Number of elements along dim 0.</param>
        /// <param name="d1">Number of elements along dim 1.</param>
        /// <param name="d2">Number of elements along dim 2.</param>
        /// <param name="d3">Number of elements along dim 3.</param>
        /// <param name="order">[Optional] Storage order for the output array. Default: 
        /// null (Settings.DefaultStorageOrder).</param>
        /// <returns>Reshaped array.</returns>
        /// <remarks><para>The storage order for both: the order for reading 
        /// the elements from this array and storing the element into the reshaped array is 
        /// determined as follows: </para>
        /// <list type="bullets">
        /// <item>If <paramref name="order"/> is specified (<see cref="StorageOrders.ColumnMajor"/>
        /// or <see cref="StorageOrders.RowMajor"/>) its value is used for the output. A copy is 
        /// made only if really needed.</item>
        /// <item>Otherwise, the value of <see cref="Settings.DefaultStorageOrder"/> determins the 
        /// target storage order.</item>
        /// </list>
        /// <para>A copy of the elements is required for non-continous arrays and - except for 
        /// vector shaped arrays - for unmatching order settings. Otherwise the returned 
        /// array references the same memory as this array and uses another size descriptor only.</para>
        /// </remarks>
        /// <see cref="Reshape{IndT}(BaseArray{IndT}, StorageOrders?)"/>
        /// <see cref="Reshape(long, StorageOrders?)"/>
        /// <see cref="Reshape(long[], StorageOrders?)"/>
        public RetT Reshape(long d0, long d1, long d2, long d3, StorageOrders? order = null) {
            using var _1 = ReaderLock.Create(this, out var storage);
            return storage.Reshape(d0, d1, d2, d3, order).RetArray;
        }
        /// <summary>
        /// Create reshaped version of this storage.
        /// </summary>
        /// <param name="d0">Number of elements along dim 0.</param>
        /// <param name="d1">Number of elements along dim 1.</param>
        /// <param name="d2">Number of elements along dim 2.</param>
        /// <param name="d3">Number of elements along dim 3.</param>
        /// <param name="d4">Number of elements along dim 4.</param>
        /// <param name="order">[Optional] Storage order for the output array. Default: 
        /// null (Settings.DefaultStorageOrder).</param>
        /// <returns>Reshaped array.</returns>
        /// <remarks><para>The storage order for both: the order for reading 
        /// the elements from this array and storing the element into the reshaped array is 
        /// determined as follows: </para>
        /// <list type="bullets">
        /// <item>If <paramref name="order"/> is specified (<see cref="StorageOrders.ColumnMajor"/>
        /// or <see cref="StorageOrders.RowMajor"/>) its value is used for the output. A copy is 
        /// made only if really needed.</item>
        /// <item>Otherwise, the value of <see cref="Settings.DefaultStorageOrder"/> determins the 
        /// target storage order.</item>
        /// </list>
        /// <para>A copy of the elements is required for non-continous arrays and - except for 
        /// vector shaped arrays - for unmatching order settings. Otherwise the returned 
        /// array references the same memory as this array and uses another size descriptor only.</para>
        /// </remarks>
        /// <see cref="Reshape(long, StorageOrders?)"/>
        /// <see cref="Reshape{IndT}(BaseArray{IndT}, StorageOrders?)"/>
        /// <see cref="Reshape(long[], StorageOrders?)"/>
        public RetT Reshape(long d0, long d1, long d2, long d3, long d4, StorageOrders? order = null) {
            using var _1 = ReaderLock.Create(this, out var storage);
            return storage.Reshape(d0, d1, d2, d3, d4, order).RetArray;
        }
        /// <summary>
        /// Create reshaped version of this storage.
        /// </summary>
        /// <param name="d0">Number of elements along dim 0.</param>
        /// <param name="d1">Number of elements along dim 1.</param>
        /// <param name="d2">Number of elements along dim 2.</param>
        /// <param name="d3">Number of elements along dim 3.</param>
        /// <param name="d4">Number of elements along dim 4.</param>
        /// <param name="d5">Number of elements along dim 5.</param>
        /// <param name="order">[Optional] Storage order for the output array. Default: 
        /// null (Settings.DefaultStorageOrder).</param>
        /// <returns>Reshaped array.</returns>
        /// <remarks><para>The storage order for both: the order for reading 
        /// the elements from this array and storing the element into the reshaped array is 
        /// determined as follows: </para>
        /// <list type="bullets">
        /// <item>If <paramref name="order"/> is specified (<see cref="StorageOrders.ColumnMajor"/>
        /// or <see cref="StorageOrders.RowMajor"/>) its value is used for the output. A copy is 
        /// made only if really needed.</item>
        /// <item>Otherwise, the value of <see cref="Settings.DefaultStorageOrder"/> determins the 
        /// target storage order.</item>
        /// </list>
        /// <para>A copy of the elements is required for non-continous arrays and - except for 
        /// vector shaped arrays - for unmatching order settings. Otherwise the returned 
        /// array references the same memory as this array and uses another size descriptor only.</para>
        /// </remarks>
        /// <see cref="Reshape{IndT}(BaseArray{IndT}, StorageOrders?)"/>
        /// <see cref="Reshape(long, StorageOrders?)"/>
        /// <see cref="Reshape(long[], StorageOrders?)"/>
        public RetT Reshape(long d0, long d1, long d2, long d3, long d4, long d5, StorageOrders? order = null) {
            using var _1 = ReaderLock.Create(this, out var storage);
            return storage.Reshape(d0, d1, d2, d3, d4, d5, order).RetArray;
        }
        /// <summary>
        /// Create reshaped version of this storage.
        /// </summary>
        /// <param name="d0">Number of elements along dim 0.</param>
        /// <param name="d1">Number of elements along dim 1.</param>
        /// <param name="d2">Number of elements along dim 2.</param>
        /// <param name="d3">Number of elements along dim 3.</param>
        /// <param name="d4">Number of elements along dim 4.</param>
        /// <param name="d5">Number of elements along dim 5.</param>
        /// <param name="d6">Number of elements along dim 6.</param>
        /// <param name="order">[Optional] Storage order for the output array. Default: 
        /// null (Settings.DefaultStorageOrder).</param>
        /// <returns>Reshaped array.</returns>
        /// <remarks><para>The storage order for both: the order for reading 
        /// the elements from this array and storing the element into the reshaped array is 
        /// determined as follows: </para>
        /// <list type="bullets">
        /// <item>If <paramref name="order"/> is specified (<see cref="StorageOrders.ColumnMajor"/>
        /// or <see cref="StorageOrders.RowMajor"/>) its value is used for reading and writing. A copy is 
        /// made only if really needed.</item>
        /// <item>Otherwise, the value of <see cref="Settings.DefaultStorageOrder"/> determins the 
        /// target storage order.</item>
        /// </list>
        /// <para>A copy of the elements is required for non-continous arrays and - except for 
        /// vector shaped arrays - for unmatching order settings. Otherwise the returned 
        /// array references the same memory as this array and uses another size descriptor only.</para>
        /// </remarks>
        /// <see cref="Reshape{IndT}(BaseArray{IndT}, StorageOrders?)"/>
        /// <see cref="Reshape(long, StorageOrders?)"/>
        /// <see cref="Reshape(long[], StorageOrders?)"/>
        public RetT Reshape(long d0, long d1, long d2, long d3, long d4, long d5, long d6, StorageOrders? order = null) {
            using var _1 = ReaderLock.Create(this, out var storage);
            return storage.Reshape(d0, d1, d2, d3, d4, d5, d6, order).RetArray;
        }

        /// <summary>
        /// Creates reshaped version, accepts dimension lengths as array.
        /// </summary>
        /// <param name="dimLengths">Lengths array.</param>
        /// <param name="order">[Optional] Storage order for reading and writing. Default: <see cref="Settings.DefaultStorageOrder"/>, 
        /// depends on <see cref="Settings.ArrayStyle"/>.</param>
        /// <returns>Reshaped array.</returns>
        /// <remarks>
        /// <para>This function is provided for such situations where the size / shape 
        /// of the target array is computed automatically. Here, it is convenient to 
        /// provide the dimension lengths as a single array parameter.</para>
        /// <para>The same limitations exist as for the overloads with individual dimension parameters.</para>
        /// </remarks>
        /// <see cref="Reshape{IndT}(BaseArray{IndT}, StorageOrders?)"/>
        /// <see cref="Reshape(long, StorageOrders?)"/>
        /// <see cref="Reshape(long[], StorageOrders?)"/>
        /// <exception cref="ArgumentException"> if <paramref name="dimLengths"/> is null or longer than the maximum number of dimensions supported: <see cref="Size.MaxNumberOfDimensions"/>.</exception>
        [Obsolete("Use Reshape(InArray<long>,StorageOrders) instead! Lists of number literals (new dimension lengths) can be wrapped into size(1,2,3,...).")]
        public RetT Reshape(long[] dimLengths, StorageOrders? order = null) {
            using var _1 = ReaderLock.Create(this, out var storage);
            return storage.Reshape(dimLengths, order).RetArray;
        }

        /// <summary>
        /// Creates reshaped version of this array, accepts variable length arguments array.
        /// </summary>
        /// <param name="size">Size for the new array, can be provided as individual literals.</param>
        /// <returns>Reshaped array.</returns>
        /// <remarks><para>This function is provided for such situations where the size / shape 
        /// of the target array is computed automatically. Here, it is convenient to 
        /// provide the dimension lengths as a single array parameter.</para>
        /// <para>Also, a variable number of number literals are accepted ('params' argument list, C#).</para>
        /// <para>The same limitations exist as for the overloads with individual dimension parameters.</para>
        /// </remarks>
        /// <seealso cref="Reshape{IndT}(BaseArray{IndT}, StorageOrders?)"/>
        /// <seealso cref="Reshape(long, StorageOrders?)"/>
        /// <seealso cref="Reshape(long[], StorageOrders?)"/>
        /// <exception cref="ArgumentException"> if <paramref name="size"/> is null or longer than the maximum number of dimensions supported: <see cref="Size.MaxNumberOfDimensions"/>.</exception>
        public RetT Reshape(params long[] size) {
            using var _1 = ReaderLock.Create(this, out var storage);
            return storage.Reshape(size, null).RetArray;
        }

        #endregion

        /// <summary>
        /// Creates reshaped version, accepts dimension lengths as array.
        /// </summary>
        /// <param name="dimLengths">Numeric array with target dimension lengths. null or an empty array creates a (0-D) scalar if possible.</param>
        /// <param name="order">Storage order for reading and writing</param>
        /// <returns>Reshaped array.</returns>
        /// <remarks><para>This function is provided for situations where the size / shape 
        /// of the target array is computed for unknown number of dimensions. Here, it is convenient to 
        /// provide the dimension lengths as a single array parameter.</para>
        /// <para>Empty <paramref name="dimLengths"/> leads to the creation of a 0-D scalar array, respecting the current settings 
        /// of <see cref="Settings.MinNumberOfArrayDimensions"/>, though.</para>
        /// <para>Elements of <paramref name="dimLengths"/> are interpreted as the target dimension lengths for the array returned.</para>
        /// <para>All but one element in <paramref name="dimLengths"/> must be positive. If one negative element is found
        /// the function will infer the required dimension length for the corresponding dimension from the other elements provided.</para>
        /// </remarks>
        /// <seealso cref="Reshape(long, StorageOrders?)"/>
        /// <seealso cref="Reshape(long[])"/>
        /// <seealso cref="Reshape(long[], StorageOrders?)"/>
        /// <exception cref="ArgumentException"> if <paramref name="dimLengths"/> is longer than the maximum number of allowed dimensions: <see cref="Size.MaxNumberOfDimensions"/>.</exception>
        public RetT Reshape<IndT>(BaseArray<IndT> dimLengths, StorageOrders? order = null) {
            using var _1 = ReaderLock.Create(this, out var storage);
            return storage.Reshape(dimLengths, order).RetArray;
        }

    }
}
