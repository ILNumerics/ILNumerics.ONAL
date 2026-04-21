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
using ILNumerics.Core.DeviceManagement;
using ILNumerics.Core.Functions.Builtin;
using ILNumerics.Core.Global;
using ILNumerics.Core.Internal;
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.StorageLayer {

    public abstract partial class BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>   // T1 must not read "T" to prevent from conflicts with the .T (transpose) property.
        : IDisposable, IStorage
        where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {


        /// <summary>
        /// Prepares the return storage. Commonly, this copies the values if needed or gives a self-reference. This function is here to enable CellStorage to override.
        /// </summary>
        /// <param name="order"></param>
        /// <param name="outStorage"></param>
        /// <returns></returns>

        protected virtual StorageT CreateSelf4Reshape(StorageOrders? order, StorageT outStorage = null) {
            StorageT ret;
            if (!Size.IsContinuous || (Size.StorageOrder != order && Size.NonSingletonDimensions > 1)) {
                // must copy first
                ret = outStorage ?? Create();
                var outHandles = ret.GetHandlesUnsafe();
                outHandles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<T>((ulong)Size.NumberOfElements);
                CopyTo(outHandles[0], null, order);

            } else if (outStorage != null) {

                // own handle in provided storage. Be prepared for pending storages!
                ret = outStorage;
                ret.Handles[0] = GetHandlesUnsafe()[0];  // performs ref counting

            } else {

                // reuse own handle in new storage - synchronous path
                ret = Create(Handles);
                Handles.Retain();

            }
            return ret;
        }

        /// <summary>
        /// Create flattened / reshaped version of this storage.
        /// </summary>
        /// <param name="d0">Length of vector/ number of elements.</param>
        /// <param name="order">Storage order of the output array.</param>
        /// <param name="outStorage">[Optional] pre-allocated output storage or null to create a new storage. Used by asynchronous paths only.</param>
        /// <returns></returns>
        
        internal virtual StorageT Reshape(uint d0, StorageOrders? order, StorageT outStorage = null) {
            if (d0 != Size.NumberOfElements) {
                throw new ArgumentException("In a reshaping operation the number of elements must not change! Check argument 'd0'!");
            }
            if (order != null && (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor)) {
                throw new ArgumentException("Invalid storage order specfied. Valid storage orders are: ColumnMajor and RowMajor."); 
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            StorageT ret = CreateSelf4Reshape(order, outStorage);

            #region configure BSD
            unsafe {
                var bsd = ret.Size.GetBSD(true);
                uint ndims = Math.Max(1, Settings.MinNumberOfArrayDimensions);

                bsd[0] = ndims;
                bsd[1] = d0;
                bsd[2] = ret.Handles == Handles ? Size.BaseOffset : 0;
                if (order == StorageOrders.ColumnMajor) {
                    // column major 
                    bsd[3] = bsd[1];
                    bsd[3 + ndims] = bsd[1] == 1 ? 0 : 1;
                    for (int i = 1; i < ndims; i++) {
                        bsd[3 + i] = d0 == 1 ? 0 : 1;
                        bsd[3 + i + ndims] = d0;
                    }
                } else {
                    // row major
                    bsd[2 + ndims] = bsd[1];
                    bsd[2 + ndims + ndims] = bsd[1] == 1 ? 0 : 1;
                    for (uint i = ndims - 1; i-- > 0;) {
                        bsd[3 + i] = d0 == 1 ? 0 : 1;
                        bsd[3 + i + ndims] = d0;
                    }
                }
            }
            ret.Size.Flags = Size.CONT_FLAG | (uint)order;
            #endregion  
            return ret; 
        }

        /// <summary>
        /// Create reshaped version of this storage.
        /// </summary>
        /// <param name="d0">Number of elements along dim 1.</param>
        /// <param name="d1">Number of elements along dim 2.</param>
        /// <param name="order">Storage order of the output array.</param>
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
        /// vector shaped arrays - for unmatching order settings.</para>
        /// </remarks>
        
        internal virtual StorageT Reshape(uint d0, uint d1, StorageOrders? order) {
            if (order != null && (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor)) {
                throw new ArgumentException("Invalid storage order specfied. Valid storage orders are: ColumnMajor and RowMajor.");
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            StorageT ret = CreateSelf4Reshape(order);

            #region configure BSD
            unsafe {
                var bsd = ret.Size.GetBSD(true);
                long stride;
                uint ndims = Math.Max(2, Settings.MinNumberOfArrayDimensions);

                bsd[0] = (uint)ndims;
                bsd[1] = (d0 * d1);
                bsd[2] = (ret.Handles == Handles ? Size.BaseOffset : 0);
                if (order == StorageOrders.ColumnMajor) {
                    // column major 
                    stride = d0;
                    bsd[3] = (d0);
                    bsd[3 + ndims] = (d0 == 1 ? 0 : 1);
                    bsd[4] = (d1);
                    bsd[4 + ndims] = (d1 == 1 ? 0 : d0);

                    for (uint i = 2; i < ndims; i++) {
                        bsd[3 + i] = (1);
                        bsd[3 + i + ndims] = 0; // bsd[1];
                    }
                    stride *= d1;
                } else {
                    // row major
                    stride = d1;
                    bsd[2 + ndims] = (d1);
                    bsd[2 + ndims + ndims] = (d1 == 1 ? 0 : 1);
                    bsd[1 + ndims] = (d0);
                    bsd[1 + ndims + ndims] = (d0 == 1 ? 0 : d1);
                    for (uint i = ndims - 2; i-- > 0;) {
                        bsd[3 + i] = (1);
                        bsd[3 + i + ndims] = 0; // bsd[1];
                    }
                    stride *= d0;
                }
                if (stride != Size.NumberOfElements) {
                    throw new ArgumentException("In a reshaping operation the number of elements must not change!");
                }
                ret.Size.Flags = Size.CONT_FLAG | (uint)order;
            }
            #endregion
            return ret;
        }

        /// <summary>
        /// Create reshaped version of this storage.
        /// </summary>
        /// <param name="d0">Number of elements along dim 1.</param>
        /// <param name="d1">Number of elements along dim 2.</param>
        /// <param name="d2">Number of elements along dim 3.</param>
        /// <param name="order">Storage order of the output array.</param>
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
        /// vector shaped arrays - for unmatching order settings.</para>
        /// </remarks>
        
        internal virtual StorageT Reshape(uint d0, uint d1, uint d2, StorageOrders? order) {
            if (order != null && (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor)) {
                throw new ArgumentException("Invalid storage order specfied. Valid storage orders are: ColumnMajor and RowMajor.");
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            StorageT ret = CreateSelf4Reshape(order);

            #region configure BSD
            unsafe {
                var bsd = ret.Size.GetBSD(true);
                long stride;
                uint ndims = Math.Max(3, Settings.MinNumberOfArrayDimensions);

                bsd[0] = (uint)ndims;
                bsd[1] = (d0 * d1 * d2);
                bsd[2] = (ret.Handles == Handles ? Size.BaseOffset : 0);
                if (order == StorageOrders.ColumnMajor) {
                    // column major 
                    stride = d0;
                    bsd[3] = (d0);
                    bsd[3 + ndims] = (d0 == 1 ? 0 : 1);
                    bsd[4] = (d1);
                    bsd[4 + ndims] = (d1 == 1 ? 0 : stride);
                    bsd[5] = (d2);
                    stride *= d1; 
                    bsd[5 + ndims] = d2 == 1 ? 0 : stride;

                    for (uint i = 3; i < ndims; i++) {
                        bsd[3 + i] = (1);
                        bsd[3 + i + ndims] = 0; //  bsd[1];
                    }
                    stride *= d2;
                } else {
                    // row major
                    stride = d2;
                    bsd[2 + ndims] = (d2);
                    bsd[2 + ndims + ndims] = (d2 == 1 ? 0 : 1);
                    bsd[1 + ndims] = (d1);
                    bsd[1 + ndims + ndims] = (d1 == 1 ? 0 : stride);
                    bsd[0 + ndims] = (d0);
                    stride *= d1;
                    bsd[0 + ndims + ndims] = (d0 == 1 ? 0 : stride);
                    for (uint i = ndims - 3; i-- > 0;) {
                        bsd[3 + i] = (1);
                        bsd[3 + i + ndims] = 0; // bsd[1];
                    }
                    stride *= d0;
                }
                if (stride != Size.NumberOfElements) {
                    throw new ArgumentException("In a reshaping operation the number of elements must not change!");
                }
                ret.Size.Flags = Size.CONT_FLAG | (uint)order;
            }
            #endregion
            return ret;
        }

        /// <summary>
        /// Create reshaped version of this storage.
        /// </summary>
        /// <param name="d0">Number of elements along dim 1.</param>
        /// <param name="d1">Number of elements along dim 2.</param>
        /// <param name="d2">Number of elements along dim 3.</param>
        /// <param name="d3">Number of elements along dim 4.</param>
        /// <param name="order">Storage order of the output array.</param>
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
        /// vector shaped arrays - for unmatching order settings.</para>
        /// </remarks>
        
        internal virtual StorageT Reshape(uint d0, uint d1, uint d2, uint d3, StorageOrders? order) {
            if (order != null && (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor)) {
                throw new ArgumentException("Invalid storage order. Valid storage orders are: ColumnMajor and RowMajor.");
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            StorageT ret = CreateSelf4Reshape(order);

            #region configure BSD
            unsafe {
                var bsd = ret.Size.GetBSD(true);

                uint ndims = Math.Max(4, Settings.MinNumberOfArrayDimensions);
                long stride;

                bsd[0] = (uint)ndims;
                bsd[1] = (Size.NumberOfElements);
                bsd[2] = (ret.Handles == Handles ? Size.BaseOffset : 0);
                if (order == StorageOrders.ColumnMajor) {
                    // column major 
                    stride = d0;
                    bsd[3] = (d0);
                    bsd[3 + ndims] = d0 == 1 ? 0 : 1;
                    bsd[4] = (d1);
                    bsd[4 + ndims] = (d1 == 1 ? 0 : stride);
                    bsd[5] = (d2);
                    stride *= d1; 
                    bsd[5 + ndims] = (d2 == 1 ? 0 : stride);
                    bsd[6] = (d3);
                    stride *= d2;
                    bsd[6 + ndims] = (d3 == 1 ? 0 : stride);
                    for (uint i = 4; i < ndims; i++) {
                        bsd[3 + i] = (1);
                        bsd[3 + i + ndims] = 0; // bsd[1];
                    }
                    stride *= d3;
                } else {
                    // row major
                    stride = d3;
                    bsd[2 + ndims] = (d3);
                    bsd[2 + ndims + ndims] = (d3 == 1 ? 0 : 1);
                    bsd[1 + ndims] = (d2);
                    bsd[1 + ndims + ndims] = (d2 == 1 ? 0 : stride);
                    bsd[0 + ndims] = (d1);
                    stride *= d2; 
                    bsd[0 + ndims + ndims] = (d1 == 1 ? 0 : stride);
                    bsd[-1 + ndims] = (d0);
                    stride *= d1; 
                    bsd[-1 + ndims + ndims] = (d0 == 1 ? 0 : stride);
                    for (uint i = ndims - 4; i-- > 0;) {
                        bsd[3 + i] = (1);
                        bsd[3 + i + ndims] = 0; // bsd[1];
                    }
                    stride *= d0;
                }
                if (stride != Size.NumberOfElements) {
                    throw new ArgumentException("In a reshaping operation the number of elements must not change!");
                }

                ret.Size.Flags = Size.CONT_FLAG | (uint)order;
            }
            #endregion
            return ret;
        }

        /// <summary>
        /// Create reshaped version of this storage.
        /// </summary>
        /// <param name="d0">Number of elements along dim 1.</param>
        /// <param name="d1">Number of elements along dim 2.</param>
        /// <param name="d2">Number of elements along dim 3.</param>
        /// <param name="d3">Number of elements along dim 4.</param>
        /// <param name="d4">Number of elements along dim 5.</param>
        /// <param name="order">Storage order of the output array.</param>
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
        /// vector shaped arrays - for unmatching order settings.</para>
        /// </remarks>
        
        internal virtual StorageT Reshape(uint d0, uint d1, uint d2, uint d3, uint d4, StorageOrders? order) {
            if (order != null && (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor)) {
                throw new ArgumentException("Invalid storage order. Valid storage orders are: ColumnMajor and RowMajor.");
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            StorageT ret = CreateSelf4Reshape(order);

            #region configure BSD
            unsafe {
                var bsd = ret.Size.GetBSD(true);

                uint ndims = Math.Max(5, Settings.MinNumberOfArrayDimensions);
                long stride;

                bsd[0] = (uint)ndims;
                bsd[1] = (Size.NumberOfElements);
                bsd[2] = (ret.Handles == Handles ? Size.BaseOffset : 0);
                if (order == StorageOrders.ColumnMajor) {
                    // column major 
                    stride = d0;
                    bsd[3] = (d0);
                    bsd[3 + ndims] = (d0 == 1 ? 0 : 1);
                    bsd[4] = (d1);
                    bsd[4 + ndims] = (d1 == 1 ? 0 : stride);
                    bsd[5] = (d2);
                    stride *= d1; 
                    bsd[5 + ndims] = (d2 == 1 ? 0 : stride);
                    bsd[6] = (d3);
                    stride *= d2;
                    bsd[6 + ndims] = (d3 == 1 ? 0 : stride);
                    bsd[7] = (d4);
                    stride *= d3; 
                    bsd[7 + ndims] = (d4 == 1 ? 0 : stride);

                    for (uint i = 5; i < ndims; i++) {
                        bsd[3 + i] = (1);
                        bsd[3 + i + ndims] = 0; // bsd[1];
                    }
                    stride *= d4;
                } else {
                    // row major
                    stride = d4;
                    bsd[2 + ndims] = (d4);
                    bsd[2 + ndims + ndims] = (d4 == 1 ? 0 : 1);
                    bsd[1 + ndims] = (d3);
                    bsd[1 + ndims + ndims] = (d3 == 1 ? 0 : stride);
                    bsd[0 + ndims] = (d2);
                    stride *= d3; 
                    bsd[0 + ndims + ndims] = (d2 == 1 ? 0 : stride);
                    bsd[-1 + ndims] = (d1);
                    stride *= d2; 
                    bsd[-1 + ndims + ndims] = (d1 == 1 ? 0 : stride);
                    bsd[-2 + ndims] = (d0);
                    stride *= d1; 
                    bsd[-2 + ndims + ndims] = (d0 == 1 ? 0 : stride);
                    for (uint i = ndims - 5; i-- > 0;) {
                        bsd[3 + i] = (1);
                        bsd[3 + i + ndims] = 0; // bsd[1];
                    }
                    stride *= d0;
                }
                if (stride != Size.NumberOfElements) {
                    throw new ArgumentException("In a reshaping operation the number of elements must not change!");
                }

                ret.Size.Flags = Size.CONT_FLAG | (uint)order;
            }
            #endregion
            return ret;
        }

        /// <summary>
        /// Create reshaped version of this storage.
        /// </summary>
        /// <param name="d0">Number of elements along dim 1.</param>
        /// <param name="d1">Number of elements along dim 2.</param>
        /// <param name="d2">Number of elements along dim 3.</param>
        /// <param name="d3">Number of elements along dim 4.</param>
        /// <param name="d4">Number of elements along dim 5.</param>
        /// <param name="d5">Number of elements along dim 6.</param>
        /// <param name="order">Storage order of the output array.</param>
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
        /// vector shaped arrays - for unmatching order settings.</para>
        /// </remarks>
        
        internal virtual StorageT Reshape(uint d0, uint d1, uint d2, uint d3, uint d4, uint d5, StorageOrders? order) {
            if (order != null && (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor)) {
                throw new ArgumentException("Invalid storage order. Valid storage orders are: ColumnMajor and RowMajor.");
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            StorageT ret = CreateSelf4Reshape(order);

            #region configure BSD
            unsafe {
                var bsd = ret.Size.GetBSD(true);

                uint ndims = Math.Max(6, Settings.MinNumberOfArrayDimensions);
                long stride;

                bsd[0] = (uint)ndims;
                bsd[1] = (Size.NumberOfElements);
                bsd[2] = (ret.Handles == Handles ? Size.BaseOffset : 0);
                if (order == StorageOrders.ColumnMajor) {
                    // column major 
                    stride = d0;
                    bsd[3] = (d0);
                    bsd[3 + ndims] = (d0 == 1 ? 0 : 1);
                    bsd[4] = (d1);
                    bsd[4 + ndims] = (d1 == 1 ? 0 : stride);
                    bsd[5] = (d2);
                    stride *= d1; 
                    bsd[5 + ndims] = (d2 == 1 ? 0 : stride);
                    bsd[6] = (d3);
                    stride *= d2;
                    bsd[6 + ndims] = (d3 == 1 ? 0 : stride);
                    bsd[7] = (d4);
                    stride *= d3;
                    bsd[7 + ndims] = (d4 == 1 ? 0 : stride);
                    bsd[8] = (d5);
                    stride *= d4;
                    bsd[8 + ndims] = (d5 == 1 ? 0 : stride);

                    for (uint i = 6; i < ndims; i++) {
                        bsd[3 + i] = (1);
                        bsd[3 + i + ndims] = 0; // bsd[1];
                    }
                    stride *= d5;
                } else {
                    // row major
                    stride = d5;
                    bsd[2 + ndims] = (d5);
                    bsd[2 + ndims + ndims] = (d5 == 1 ? 0 : 1);
                    bsd[1 + ndims] = (d4);
                    bsd[1 + ndims + ndims] = (d4 == 1 ? 0 : stride);
                    bsd[0 + ndims] = (d3);
                    stride *= d4; 
                    bsd[0 + ndims + ndims] = (d3 == 1 ? 0 : stride);
                    bsd[-1 + ndims] = (d2);
                    stride *= d3; 
                    bsd[-1 + ndims + ndims] = (d2 == 1 ? 0 : stride);
                    bsd[-2 + ndims] = (d1);
                    stride *= d2; 
                    bsd[-2 + ndims + ndims] = (d1 == 1 ? 0 : stride);
                    bsd[-3 + ndims] = (d0);
                    stride *= d1;
                    bsd[-3 + ndims + ndims] = (d0 == 1 ? 0 : stride);
                    for (uint i = ndims - 6; i-- > 0;) {
                        bsd[3 + i] = (1);
                        bsd[3 + i + ndims] = 0; // bsd[1];
                    }
                    stride *= d0;
                }
                if (stride != Size.NumberOfElements) {
                    throw new ArgumentException("In a reshaping operation the number of elements must not change!");
                }

                ret.Size.Flags = Size.CONT_FLAG | (uint)order;
            }
            #endregion
            return ret;
        }

        /// <summary>
        /// Create reshaped version of this storage.
        /// </summary>
        /// <param name="d0">Number of elements along dim 1.</param>
        /// <param name="d1">Number of elements along dim 2.</param>
        /// <param name="d2">Number of elements along dim 3.</param>
        /// <param name="d3">Number of elements along dim 4.</param>
        /// <param name="d4">Number of elements along dim 5.</param>
        /// <param name="d5">Number of elements along dim 6.</param>
        /// <param name="d6">Number of elements along dim 7.</param>
        /// <param name="order">Storage order of the output array.</param>
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
        /// vector shaped arrays - for unmatching order settings.</para>
        /// </remarks>
        
        internal virtual StorageT Reshape(uint d0, uint d1, uint d2, uint d3, uint d4, uint d5, uint d6, StorageOrders? order) {
            if (order != null && (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor)) {
                throw new ArgumentException("Invalid storage order. Valid storage orders are: ColumnMajor and RowMajor.");
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            StorageT ret = CreateSelf4Reshape(order);

            #region configure BSD
            unsafe {
                var bsd = ret.Size.GetBSD(true);

                uint ndims = Math.Max(7, Settings.MinNumberOfArrayDimensions);
                long stride;

                bsd[0] = (uint)ndims;
                bsd[1] = (Size.NumberOfElements);
                bsd[2] = (ret.Handles == Handles ? Size.BaseOffset : 0);
                if (order == StorageOrders.ColumnMajor) {
                    // column major 
                    stride = d0;
                    bsd[3] = (d0);
                    bsd[3 + ndims] = (d0 == 1 ? 0 : 1);
                    bsd[4] = (d1);
                    bsd[4 + ndims] = (d1 == 1 ? 0 : stride);
                    bsd[5] = (d2);
                    stride *= d1;
                    bsd[5 + ndims] = (d2 == 1 ? 0 : stride);
                    bsd[6] = (d3);
                    stride *= d2; 
                    bsd[6 + ndims] = (d3 == 1 ? 0 : stride);
                    bsd[7] = (d4);
                    stride *= d3; 
                    bsd[7 + ndims] = (d4 == 1 ? 0 : stride);
                    bsd[8] = (d5);
                    stride *= d4; 
                    bsd[8 + ndims] = (d5 == 1 ? 0 : stride);
                    bsd[9] = (d6);
                    stride *= d5; 
                    bsd[9 + ndims] = (d6 == 1 ? 0 : stride);

                    for (uint i = 7; i < ndims; i++) {
                        bsd[3 + i] = (1);
                        bsd[3 + i + ndims] = 0; // bsd[1];
                    }
                    stride *= d6;
                } else {
                    // row major
                    stride = d6;
                    bsd[2 + ndims] = (d6);
                    bsd[2 + ndims + ndims] = (d6 == 1 ? 0 : 1);
                    bsd[1 + ndims] = (d5);
                    bsd[1 + ndims + ndims] = (d5 == 1 ? 0 : stride);
                    bsd[0 + ndims] = (d4);
                    stride *= d5; 
                    bsd[0 + ndims + ndims] = (d4 == 1 ? 0 : stride);
                    bsd[-1 + ndims] = (d3);
                    stride *= d4; 
                    bsd[-1 + ndims + ndims] = (d3 == 1 ? 0 : stride);
                    bsd[-2 + ndims] = (d2);
                    stride *= d3; 
                    bsd[-2 + ndims + ndims] = (d2 == 1 ? 0 : stride);
                    bsd[-3 + ndims] = (d1);
                    stride *= d2; 
                    bsd[-3 + ndims + ndims] = (d1 == 1 ? 0 : stride);
                    bsd[-4 + ndims] = (d0);
                    stride *= d1; 
                    bsd[-4 + ndims + ndims] = (d0 == 1 ? 0 : stride);
                    for (uint i = ndims - 7; i-- > 0;) {
                        bsd[3 + i] = (1);
                        bsd[3 + i + ndims] = 0; // bsd[1];
                    }
                    stride *= d0;
                }
                if (stride != Size.NumberOfElements) {
                    throw new ArgumentException("In a reshaping operation the number of elements must not change!");
                }

                ret.Size.Flags = Size.CONT_FLAG | (uint)order;
            }
            #endregion
            return ret;
        }
        /// <summary>
        /// Create flattened / reshaped version of this storage. Allows one dimension length to be infered.
        /// </summary>
        /// <param name="d0">Length of vector/ number of elements.</param>
        /// <param name="order">Storage order of the output array.</param>
        /// <param name="minDims">[Optional] Minimum number of array dimensions. Default: null (<see cref="Settings.MinNumberOfArrayDimensions"/>).</param>
        /// <param name="outStorage">(not used)</param>
        /// <returns></returns>

        internal virtual StorageT Reshape(long d0, StorageOrders? order, uint? minDims = null, StorageT outStorage = null) {
            if (d0 < 0) {
                d0 = (long)Size.NumberOfElements; 
            } else if (d0 != Size.NumberOfElements) {
                throw new ArgumentException("In a reshaping operation the number of elements must not change! Check argument 'd0'!");
            }
            if (order != null && (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor)) {
                throw new ArgumentException("Invalid storage order specfied. Valid storage orders are: ColumnMajor and RowMajor."); 
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            StorageT ret = CreateSelf4Reshape(order);

            #region configure BSD
            unsafe {
                var bsd = ret.Size.GetBSD(true);
                ulong ndims = Math.Max(1, minDims.HasValue ? minDims.GetValueOrDefault() : Settings.MinNumberOfArrayDimensions);

                bsd[0] = (uint)ndims;
                bsd[1] = (d0);
                bsd[2] = (ret.Handles == Handles ? Size.BaseOffset : 0);
                if (order == StorageOrders.ColumnMajor) {
                    // column major 
                    bsd[3] = bsd[1];
                    bsd[3 + ndims] = (d0 == 1 ? 0 : 1);
                    for (uint i = 1; i < ndims; i++) {
                        bsd[3 + i] = (1);
                        bsd[3 + i + ndims] = 0; // (d0);
                    }
                } else {
                    // row major
                    bsd[2 + ndims] = bsd[1];
                    bsd[2 + ndims + ndims] = (d0 == 1 ? 0 : 1);
                    for (ulong i = ndims - 1; i-- > 0;) {
                        bsd[3 + i] = (1);
                        bsd[3 + i + ndims] = 0; // (d0);
                    }
                }
            }
            ret.Size.Flags = Size.CONT_FLAG | (uint)order;
            #endregion  
            return ret; 
        }
        /// <summary>
        /// Create reshaped version of this storage.
        /// </summary>
        /// <param name="d0">Number of elements along dim 1.</param>
        /// <param name="d1">Number of elements along dim 2.</param>
        /// <param name="order">Storage order of the output array.</param>
        /// <param name="minDims">[Optional] Minimum number of array dimensions. Default: null (<see cref="Settings.MinNumberOfArrayDimensions"/>).</param>
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
        /// vector shaped arrays - for unmatching order settings.</para>
        /// </remarks>
        
        internal unsafe virtual StorageT Reshape(long d0, long d1, StorageOrders? order, uint? minDims = null) {
            if (order != null && (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor)) {
                throw new ArgumentException("Invalid storage order specfied. Valid storage orders are: ColumnMajor and RowMajor.");
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            StorageT ret = CreateSelf4Reshape(order);

            #region configure BSD
            unsafe {
                var bsd = ret.Size.GetBSD(true);
                long stride = 1;
                uint ndims = Math.Max(2, minDims.HasValue ? minDims.GetValueOrDefault() : Settings.MinNumberOfArrayDimensions);

                #region infer missing parameter
                long* negParam = (long*)0;
                if (d0 < 0) {
                    negParam = &d0;
                } else {
                    stride *= d0;
                }
                if (d1 < 0) {
                    if (negParam != (long*)0) throw new ArgumentException("Reshape: exactly one dimension parameter may be specified as -1 to be infered automaticially. More than one such parameter were found.");
                    negParam = &d1;
                } else {
                    stride *= d1;
                }
                if (negParam != (long*)0) *negParam = stride > 0 ? (long)Size.NumberOfElements / stride : 0;
                #endregion

                bsd[0] = (uint)ndims;
                bsd[1] = ((d0 * d1));
                bsd[2] = (ret.Handles == Handles ? Size.BaseOffset : 0);
                if (order == StorageOrders.ColumnMajor) {
                    // column major 
                    stride = d0;
                    bsd[3] = (d0);
                    bsd[3 + ndims] = (d0 == 1 ? 0 : 1);
                    bsd[4] = (d1);
                    bsd[4 + ndims] = (d1 == 1 ? 0 : d0);

                    for (long i = 2; i < ndims; i++) {
                        bsd[3 + i] = (1);
                        bsd[3 + i + ndims] = 0; // bsd[1];
                    }
                    stride *= d1;
                } else {
                    // row major
                    stride = d1;
                    bsd[2 + ndims] = (d1);
                    bsd[2 + ndims + ndims] = (d1 == 1 ? 0 : 1);
                    bsd[1 + ndims] = (d0);
                    bsd[1 + ndims + ndims] = (d0 == 1 ? 0 : d1);
                    for (long i = ndims - 2; i-- > 0;) {
                        bsd[3 + i] = (1);
                        bsd[3 + i + ndims] = 0; // bsd[1];
                    }
                    stride *= d0;
                }
                if (stride != Size.NumberOfElements) {
                    throw new ArgumentException("In a reshaping operation the number of elements must not change!");
                }
                ret.Size.Flags = Size.CONT_FLAG | (uint)order;
            }
            #endregion
            return ret;
        }

        /// <summary>
        /// Create reshaped version of this storage.
        /// </summary>
        /// <param name="d0">Number of elements along dim 1.</param>
        /// <param name="d1">Number of elements along dim 2.</param>
        /// <param name="d2">Number of elements along dim 3.</param>
        /// <param name="order">Storage order of the output array.</param>
        /// <param name="minDims">[Optional] Minimum number of array dimensions. Default: null (<see cref="Settings.MinNumberOfArrayDimensions"/>).</param>
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
        /// vector shaped arrays - for unmatching order settings.</para>
        /// </remarks>
        
        internal unsafe virtual StorageT Reshape(long d0, long d1, long d2, StorageOrders? order, uint? minDims = null) {
            if (order != null && (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor)) {
                throw new ArgumentException("Invalid storage order specfied. Valid storage orders are: ColumnMajor and RowMajor.");
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            StorageT ret = CreateSelf4Reshape(order);

            #region configure BSD
            unsafe {
                var bsd = ret.Size.GetBSD(true);
                long stride = 1;
                uint ndims = Math.Max(3, minDims.HasValue ? minDims.GetValueOrDefault() : Settings.MinNumberOfArrayDimensions);

                #region infer missing parameter
                long* negParam = (long*)0;
                if (d0 < 0) {
                    negParam = &d0;
                } else {
                    stride *= d0;
                }
                if (d1 < 0) {
                    if (negParam != (long*)0) throw new ArgumentException("Reshape: exactly one dimension parameter may be specified as -1 to be infered automaticially. More than one such parameter were found.");
                    negParam = &d1;
                } else {
                    stride *= d1;
                }
                if (d2 < 0) {
                    if (negParam != (long*)0) throw new ArgumentException("Reshape: exactly one dimension parameter may be specified as -1 to be infered automaticially. More than one such parameter were found.");
                    negParam = &d2;
                } else {
                    stride *= d2;
                }
                if (negParam != (long*)0) *negParam = stride > 0 ? (long)Size.NumberOfElements / stride : 0;
                #endregion

                bsd[0] = (uint)ndims;
                bsd[1] = ((d0 * d1 * d2));
                bsd[2] = (ret.Handles == Handles ? Size.BaseOffset : 0);
                if (order == StorageOrders.ColumnMajor) {
                    // column major 
                    stride = d0;
                    bsd[3] = (d0);
                    bsd[3 + ndims] = (d0 == 1 ? 0 : 1);
                    bsd[4] = (d1);
                    bsd[4 + ndims] = (d1 == 1 ? 0 : stride);
                    bsd[5] = (d2);
                    stride *= d1;
                    bsd[5 + ndims] = d2 == 1 ? 0 : stride;

                    for (long i = 3; i < ndims; i++) {
                        bsd[3 + i] = (1);
                        bsd[3 + i + ndims] = 0; // bsd[1];
                    }
                    stride *= d2;
                } else {
                    // row major
                    stride = d2;
                    bsd[2 + ndims] = (d2);
                    bsd[2 + ndims + ndims] = (d2 == 1 ? 0 : 1);
                    bsd[1 + ndims] = (d1);
                    bsd[1 + ndims + ndims] = (d1 == 1 ? 0 : stride);
                    bsd[0 + ndims] = (d0);
                    stride *= d1; 
                    bsd[0 + ndims + ndims] = d0 == 1 ? 0 : stride;
                    for (long i = ndims - 3; i-- > 0;) {
                        bsd[3 + i] = (1);
                        bsd[3 + i + ndims] = 0; // bsd[1];
                    }
                    stride *= d0;
                }
                if (stride != Size.NumberOfElements) {
                    throw new ArgumentException("In a reshaping operation the number of elements must not change!");
                }
                ret.Size.Flags = Size.CONT_FLAG | (uint)order;
            }
            #endregion
            return ret;
        }

        /// <summary>
        /// Create reshaped version of this storage.
        /// </summary>
        /// <param name="d0">Number of elements along dim 1.</param>
        /// <param name="d1">Number of elements along dim 2.</param>
        /// <param name="d2">Number of elements along dim 3.</param>
        /// <param name="d3">Number of elements along dim 4.</param>
        /// <param name="order">Storage order of the output array.</param>
        /// <param name="minDims">[Optional] Minimum number of array dimensions. Default: null (<see cref="Settings.MinNumberOfArrayDimensions"/>).</param>
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
        /// vector shaped arrays - for unmatching order settings.</para>
        /// </remarks>
        
        internal unsafe virtual StorageT Reshape(long d0, long d1, long d2, long d3, StorageOrders? order, uint? minDims = null) {
            if (order != null && (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor)) {
                throw new ArgumentException("Invalid storage order. Valid storage orders are: ColumnMajor and RowMajor.");
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            StorageT ret = CreateSelf4Reshape(order);

            #region configure BSD
            unsafe {
                var bsd = ret.Size.GetBSD(true);
                long stride = 1;
                uint ndims = Math.Max(4, minDims.HasValue ? minDims.GetValueOrDefault() : Settings.MinNumberOfArrayDimensions);

                #region infer missing parameter
                long* negParam = (long*)0;
                if (d0 < 0) {
                    negParam = &d0;
                } else {
                    stride *= d0;
                }
                if (d1 < 0) {
                    if (negParam != (long*)0) throw new ArgumentException("Reshape: exactly one dimension parameter may be specified as -1 to be infered automaticially. More than one such parameter were found.");
                    negParam = &d1;
                } else {
                    stride *= d1;
                }
                if (d2 < 0) {
                    if (negParam != (long*)0) throw new ArgumentException("Reshape: exactly one dimension parameter may be specified as -1 to be infered automaticially. More than one such parameter were found.");
                    negParam = &d2;
                } else {
                    stride *= d2;
                }
                if (d3 < 0) {
                    if (negParam != (long*)0) throw new ArgumentException("Reshape: exactly one dimension parameter may be specified as -1 to be infered automaticially. More than one such parameter were found.");
                    negParam = &d3;
                } else {
                    stride *= d3;
                }
                if (negParam != (long*)0) *negParam = stride > 0 ? (long)Size.NumberOfElements / stride : 0;
                #endregion

                bsd[0] = (uint)ndims;
                bsd[1] = (Size.NumberOfElements);
                bsd[2] = (ret.Handles == Handles ? Size.BaseOffset : 0);
                if (order == StorageOrders.ColumnMajor) {
                    // column major 
                    stride = d0;
                    bsd[3] = (d0);
                    bsd[3 + ndims] = (d0 == 1 ? 0 : 1);
                    bsd[4] = (d1);
                    bsd[4 + ndims] = (d1 == 1 ? 0 : stride);
                    bsd[5] = (d2);
                    stride *= d1;
                    bsd[5 + ndims] = d2 == 1 ? 0 : stride;
                    bsd[6] = (d3);
                    stride *= d2;
                    bsd[6 + ndims] = d3 == 1 ? 0 : stride;
                    for (long i = 4; i < ndims; i++) {
                        bsd[3 + i] = (1);
                        bsd[3 + i + ndims] = 0; //  bsd[1];
                    }
                    stride *= d3;
                } else {
                    // row major
                    stride = d3;
                    bsd[2 + ndims] = (d3);
                    bsd[2 + ndims + ndims] = (d3 == 1 ? 0 : 1);
                    bsd[1 + ndims] = (d2);
                    bsd[1 + ndims + ndims] = (d2 == 1 ? 0 : stride);
                    bsd[0 + ndims] = (d1);
                    stride *= d2; 
                    bsd[0 + ndims + ndims] = d1 == 1 ? 0 : stride;
                    bsd[-1 + ndims] = (d0);
                    stride *= d1; 
                    bsd[-1 + ndims + ndims] = d0 == 1 ? 0 : stride;
                    for (long i = ndims - 4; i-- > 0;) {
                        bsd[3 + i] = (1);
                        bsd[3 + i + ndims] = 0; // bsd[1];
                    }
                    stride *= d0;
                }
                if (stride != Size.NumberOfElements) {
                    throw new ArgumentException("In a reshaping operation the number of elements must not change!");
                }

                ret.Size.Flags = Size.CONT_FLAG | (uint)order;
            }
            #endregion
            return ret;
        }

        /// <summary>
        /// Create reshaped version of this storage.
        /// </summary>
        /// <param name="d0">Number of elements along dim 1.</param>
        /// <param name="d1">Number of elements along dim 2.</param>
        /// <param name="d2">Number of elements along dim 3.</param>
        /// <param name="d3">Number of elements along dim 4.</param>
        /// <param name="d4">Number of elements along dim 5.</param>
        /// <param name="order">Storage order of the output array.</param>
        /// <param name="minDims">[Optional] Minimum number of array dimensions. Default: null (<see cref="Settings.MinNumberOfArrayDimensions"/>).</param>
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
        /// vector shaped arrays - for unmatching order settings.</para>
        /// </remarks>
        
        internal unsafe virtual StorageT Reshape(long d0, long d1, long d2, long d3, long d4, StorageOrders? order, uint? minDims = null) {
            if (order != null && (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor)) {
                throw new ArgumentException("Invalid storage order. Valid storage orders are: ColumnMajor and RowMajor.");
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            StorageT ret = CreateSelf4Reshape(order);

            #region configure BSD
            unsafe {
                var bsd = ret.Size.GetBSD(true);
                long stride = 1;
                uint ndims = Math.Max(5, minDims.HasValue ? minDims.GetValueOrDefault() : Settings.MinNumberOfArrayDimensions);

                #region infer missing parameter
                long* negParam = (long*)0;
                if (d0 < 0) {
                    negParam = &d0;
                } else {
                    stride *= d0;
                }
                if (d1 < 0) {
                    if (negParam != (long*)0) throw new ArgumentException("Reshape: exactly one dimension parameter may be specified as -1 to be infered automaticially. More than one such parameter were found.");
                    negParam = &d1;
                } else {
                    stride *= d1;
                }
                if (d2 < 0) {
                    if (negParam != (long*)0) throw new ArgumentException("Reshape: exactly one dimension parameter may be specified as -1 to be infered automaticially. More than one such parameter were found.");
                    negParam = &d2;
                } else {
                    stride *= d2;
                }
                if (d3 < 0) {
                    if (negParam != (long*)0) throw new ArgumentException("Reshape: exactly one dimension parameter may be specified as -1 to be infered automaticially. More than one such parameter were found.");
                    negParam = &d3;
                } else {
                    stride *= d3;
                }
                if (d4 < 0) {
                    if (negParam != (long*)0) throw new ArgumentException("Reshape: exactly one dimension parameter may be specified as -1 to be infered automaticially. More than one such parameter were found.");
                    negParam = &d4;
                } else {
                    stride *= d4;
                }
                if (negParam != (long*)0) *negParam = stride > 0 ? (long)Size.NumberOfElements / stride : 0;
                #endregion

                bsd[0] = (uint)ndims;
                bsd[1] = (Size.NumberOfElements);
                bsd[2] = (ret.Handles == Handles ? Size.BaseOffset : 0);
                if (order == StorageOrders.ColumnMajor) {
                    // column major 
                    stride = d0;
                    bsd[3] = (d0);
                    bsd[3 + ndims] = (d0 == 1 ? 0 : 1);
                    bsd[4] = (d1);
                    bsd[4 + ndims] = (d1 == 1 ? 0 : stride);
                    bsd[5] = (d2);
                    stride *= d1; 
                    bsd[5 + ndims] = d2 == 1 ? 0 : stride;
                    bsd[6] = (d3);
                    stride *= d2; 
                    bsd[6 + ndims] = d3 == 1 ? 0 : stride;
                    bsd[7] = (d4);
                    stride *= d3; 
                    bsd[7 + ndims] = d4 == 1 ? 0 : stride;

                    for (long i = 5; i < ndims; i++) {
                        bsd[3 + i] = (1);
                        bsd[3 + i + ndims] = 0; // bsd[1];
                    }
                    stride *= d4;
                } else {
                    // row major
                    stride = d4;
                    bsd[2 + ndims] = (d4);
                    bsd[2 + ndims + ndims] = d4 == 1 ? 0 : 1;
                    bsd[1 + ndims] = (d3);
                    bsd[1 + ndims + ndims] = d3 == 1 ? 0 : stride;
                    bsd[0 + ndims] = (d2);
                    stride *= d3; 
                    bsd[0 + ndims + ndims] = d2 == 1 ? 0 : stride;
                    bsd[-1 + ndims] = (d1);
                    stride *= d2;
                    bsd[-1 + ndims + ndims] = d1 == 1 ? 0 : stride;
                    bsd[-2 + ndims] = (d0);
                    stride *= d1; 
                    bsd[-2 + ndims + ndims] = d0 == 1 ? 0 : stride;
                    for (long i = ndims - 5; i-- > 0;) {
                        bsd[3 + i] = (1);
                        bsd[3 + i + ndims] = 0; // bsd[1];
                    }
                    stride *= d0;
                }
                if (stride != Size.NumberOfElements) {
                    throw new ArgumentException("In a reshaping operation the number of elements must not change!");
                }

                ret.Size.Flags = Size.CONT_FLAG | (uint)order;
            }
            #endregion
            return ret;
        }

        /// <summary>
        /// Create reshaped version of this storage.
        /// </summary>
        /// <param name="d0">Number of elements along dim 1.</param>
        /// <param name="d1">Number of elements along dim 2.</param>
        /// <param name="d2">Number of elements along dim 3.</param>
        /// <param name="d3">Number of elements along dim 4.</param>
        /// <param name="d4">Number of elements along dim 5.</param>
        /// <param name="d5">Number of elements along dim 6.</param>
        /// <param name="order">Storage order of the output array.</param>
        /// <param name="minDims">[Optional] Minimum number of array dimensions. Default: null (<see cref="Settings.MinNumberOfArrayDimensions"/>).</param>
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
        /// vector shaped arrays - for unmatching order settings.</para>
        /// </remarks>
        
        internal unsafe virtual StorageT Reshape(long d0, long d1, long d2, long d3, long d4, long d5, StorageOrders? order, uint? minDims = null) {
            if (order != null && (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor)) {
                throw new ArgumentException("Invalid storage order. Valid storage orders are: ColumnMajor and RowMajor.");
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            StorageT ret = CreateSelf4Reshape(order);

            #region configure BSD
            unsafe {
                var bsd = ret.Size.GetBSD(true);
                long stride = 1;
                uint ndims = Math.Max(6, minDims.HasValue ? minDims.GetValueOrDefault() : Settings.MinNumberOfArrayDimensions);

                #region infer missing parameter
                long* negParam = (long*)0;
                if (d0 < 0) {
                    negParam = &d0;
                } else {
                    stride *= d0;
                }
                if (d1 < 0) {
                    if (negParam != (long*)0) throw new ArgumentException("Reshape: exactly one dimension parameter may be specified as -1 to be infered automaticially. More than one such parameter were found.");
                    negParam = &d1;
                } else {
                    stride *= d1;
                }
                if (d2 < 0) {
                    if (negParam != (long*)0) throw new ArgumentException("Reshape: exactly one dimension parameter may be specified as -1 to be infered automaticially. More than one such parameter were found.");
                    negParam = &d2;
                } else {
                    stride *= d2;
                }
                if (d3 < 0) {
                    if (negParam != (long*)0) throw new ArgumentException("Reshape: exactly one dimension parameter may be specified as -1 to be infered automaticially. More than one such parameter were found.");
                    negParam = &d3;
                } else {
                    stride *= d3;
                }
                if (d4 < 0) {
                    if (negParam != (long*)0) throw new ArgumentException("Reshape: exactly one dimension parameter may be specified as -1 to be infered automaticially. More than one such parameter were found.");
                    negParam = &d4;
                } else {
                    stride *= d4;
                }
                if (d5 < 0) {
                    if (negParam != (long*)0) throw new ArgumentException("Reshape: exactly one dimension parameter may be specified as -1 to be infered automaticially. More than one such parameter were found.");
                    negParam = &d5;
                } else {
                    stride *= d5;
                }
                if (negParam != (long*)0) *negParam = stride > 0 ? (long)Size.NumberOfElements / stride : 0;
                #endregion

                bsd[0] = (uint)ndims;
                bsd[1] = (Size.NumberOfElements);
                bsd[2] = (ret.Handles == Handles ? Size.BaseOffset : 0);
                if (order == StorageOrders.ColumnMajor) {
                    // column major 
                    stride = d0;
                    bsd[3] = (d0);
                    bsd[3 + ndims] = d0 == 1 ? 0 : 1;
                    bsd[4] = (d1);
                    bsd[4 + ndims] = d1 == 1 ? 0 : stride;
                    bsd[5] = (d2);
                    stride *= d1; 
                    bsd[5 + ndims] = d2 == 1 ? 0 : stride;
                    bsd[6] = (d3);
                    stride *= d2;
                    bsd[6 + ndims] = d3 == 1 ? 0 : stride;
                    bsd[7] = (d4);
                    stride *= d3; 
                    bsd[7 + ndims] = d4 == 1 ? 0 : stride;
                    bsd[8] = (d5);
                    stride *= d4;
                    bsd[8 + ndims] = d5 == 1 ? 0 : stride;

                    for (long i = 6; i < ndims; i++) {
                        bsd[3 + i] = (1);
                        bsd[3 + i + ndims] = 0; // bsd[1];
                    }
                    stride *= d5;
                } else {
                    // row major
                    stride = d5;
                    bsd[2 + ndims] = (d5);
                    bsd[2 + ndims + ndims] = (d5 == 1 ? 0 : 1);
                    bsd[1 + ndims] = (d4);
                    bsd[1 + ndims + ndims] = (d4 == 1 ? 0 : stride);
                    bsd[0 + ndims] = (d3);
                    stride *= d4; 
                    bsd[0 + ndims + ndims] = d3 == 1 ? 0 : stride;
                    bsd[-1 + ndims] = (d2);
                    stride *= d3; 
                    bsd[-1 + ndims + ndims] = d2 == 1 ? 0 : stride;
                    bsd[-2 + ndims] = (d1);
                    stride *= d2; 
                    bsd[-2 + ndims + ndims] = d1 == 1 ? 0 : stride;
                    bsd[-3 + ndims] = (d0);
                    stride *= d1; 
                    bsd[-3 + ndims + ndims] = d0 == 1 ? 0 : stride;
                    for (long i = ndims - 6; i-- > 0;) {
                        bsd[3 + i] = (1);
                        bsd[3 + i + ndims] = 0; // bsd[1];
                    }
                    stride *= d0;
                }
                if (stride != Size.NumberOfElements) {
                    throw new ArgumentException("In a reshaping operation the number of elements must not change!");
                }

                ret.Size.Flags = Size.CONT_FLAG | (uint)order;
            }
            #endregion
            return ret;
        }

        /// <summary>
        /// Create reshaped version of this storage.
        /// </summary>
        /// <param name="d0">Number of elements along dim 1.</param>
        /// <param name="d1">Number of elements along dim 2.</param>
        /// <param name="d2">Number of elements along dim 3.</param>
        /// <param name="d3">Number of elements along dim 4.</param>
        /// <param name="d4">Number of elements along dim 5.</param>
        /// <param name="d5">Number of elements along dim 6.</param>
        /// <param name="d6">Number of elements along dim 7.</param>
        /// <param name="order">Storage order of the output array.</param>
        /// <param name="minDims">[Optional] Minimum number of array dimensions. Default: null (<see cref="Settings.MinNumberOfArrayDimensions"/>).</param>
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
        /// vector shaped arrays - for unmatching order settings.</para>
        /// </remarks>
        
        internal unsafe virtual StorageT Reshape(long d0, long d1, long d2, long d3, long d4, long d5, long d6, StorageOrders? order, uint? minDims = null) {
            if (order != null && (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor)) {
                throw new ArgumentException("Invalid storage order. Valid storage orders are: ColumnMajor and RowMajor.");
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            StorageT ret = CreateSelf4Reshape(order);

            #region configure BSD
            unsafe {
                var bsd = ret.Size.GetBSD(true);
                long stride = 1;
                uint ndims = Math.Max(7, minDims.HasValue ? minDims.GetValueOrDefault() : Settings.MinNumberOfArrayDimensions);

                #region infer missing parameter
                long* negParam = (long*)0;
                if (d0 < 0) {
                    negParam = &d0;
                } else {
                    stride *= d0;
                }
                if (d1 < 0) {
                    if (negParam != (long*)0) throw new ArgumentException("Reshape: exactly one dimension parameter may be specified as -1 to be infered automaticially. More than one such parameter were found.");
                    negParam = &d1;
                } else {
                    stride *= d1;
                }
                if (d2 < 0) {
                    if (negParam != (long*)0) throw new ArgumentException("Reshape: exactly one dimension parameter may be specified as -1 to be infered automaticially. More than one such parameter were found.");
                    negParam = &d2;
                } else {
                    stride *= d2;
                }
                if (d3 < 0) {
                    if (negParam != (long*)0) throw new ArgumentException("Reshape: exactly one dimension parameter may be specified as -1 to be infered automaticially. More than one such parameter were found.");
                    negParam = &d3;
                } else {
                    stride *= d3;
                }
                if (d4 < 0) {
                    if (negParam != (long*)0) throw new ArgumentException("Reshape: exactly one dimension parameter may be specified as -1 to be infered automaticially. More than one such parameter were found.");
                    negParam = &d4;
                } else {
                    stride *= d4;
                }
                if (d5 < 0) {
                    if (negParam != (long*)0) throw new ArgumentException("Reshape: exactly one dimension parameter may be specified as -1 to be infered automaticially. More than one such parameter were found.");
                    negParam = &d5;
                } else {
                    stride *= d5;
                }
                if (d6 < 0) {
                    if (negParam != (long*)0) throw new ArgumentException("Reshape: exactly one dimension parameter may be specified as -1 to be infered automaticially. More than one such parameter were found.");
                    negParam = &d6;
                } else {
                    stride *= d6;
                }
                if (negParam != (long*)0) *negParam = stride > 0 ? (long)Size.NumberOfElements / stride : 0;
                #endregion

                bsd[0] = (uint)ndims;
                bsd[1] = (Size.NumberOfElements);
                bsd[2] = (ret.Handles == Handles ? Size.BaseOffset : 0);
                if (order == StorageOrders.ColumnMajor) {
                    // column major 
                    stride = d0;
                    bsd[3] = (d0);
                    bsd[3 + ndims] = d0 == 1 ? 0 : 1;
                    bsd[4] = (d1);
                    bsd[4 + ndims] = d1 == 1 ? 0 : stride;
                    bsd[5] = (d2);
                    stride *= d1;
                    bsd[5 + ndims] = d2 == 1 ? 0 : stride;
                    bsd[6] = (d3);
                    stride *= d2;
                    bsd[6 + ndims] = d3 == 1 ? 0 : stride;
                    bsd[7] = (d4);
                    stride *= d3;
                    bsd[7 + ndims] = d4 == 1 ? 0 : stride;
                    bsd[8] = (d5);
                    stride *= d4;
                    bsd[8 + ndims] = d5 == 1 ? 0 : stride;
                    bsd[9] = (d6);
                    stride *= d5;
                    bsd[9 + ndims] = d6 == 1 ? 0 : stride;

                    for (long i = 7; i < ndims; i++) {
                        bsd[3 + i] = (1);
                        bsd[3 + i + ndims] = 0; // bsd[1];
                    }
                    stride *= d6;
                } else {
                    // row major
                    stride = d6;
                    bsd[2 + ndims] = (d6);
                    bsd[2 + ndims + ndims] = d6 == 1 ? 0 : 1;
                    bsd[1 + ndims] = (d5);
                    bsd[1 + ndims + ndims] = d5 == 1 ? 0 : stride;
                    bsd[0 + ndims] = (d4);
                    stride *= d5; 
                    bsd[0 + ndims + ndims] = d4 == 1 ? 0 : stride;
                    bsd[-1 + ndims] = (d3);
                    stride *= d4;
                    bsd[-1 + ndims + ndims] = d3 == 1 ? 0 : stride;
                    bsd[-2 + ndims] = (d2);
                    stride *= d3; 
                    bsd[-2 + ndims + ndims] = d2 == 1 ? 0 : stride;
                    bsd[-3 + ndims] = (d1);
                    stride *= d2;
                    bsd[-3 + ndims + ndims] = d1 == 1 ? 0 : stride;
                    bsd[-4 + ndims] = (d0);
                    stride *= d1; 
                    bsd[-4 + ndims + ndims] = d0 == 1 ? 0 : stride;
                    for (long i = ndims - 7; i-- > 0;) {
                        bsd[3 + i] = (1);
                        bsd[3 + i + ndims] = 0; // bsd[1];
                    }
                    stride *= d0;
                }
                if (stride != Size.NumberOfElements) {
                    throw new ArgumentException("In a reshaping operation the number of elements must not change!");
                }

                ret.Size.Flags = Size.CONT_FLAG | (uint)order;
            }
            #endregion
            return ret;
        }

        /// <summary>
        /// Reshape from lengths array.
        /// </summary>
        /// <param name="dimLengths">dimension lengths</param>
        /// <param name="order">target storage order</param>
        /// <returns>reshaped array</returns>
        internal virtual StorageT Reshape(uint[] dimLengths, StorageOrders? order) {
            if (dimLengths == null || dimLengths.Length == 0) {
                throw new ArgumentException("Invalid parameter: dimLengths must not be null or empty.");
            }
            switch (dimLengths.Length) {
                case 1:
                    return Reshape(dimLengths[0], order);
                case 2:
                    return Reshape(dimLengths[0], dimLengths[1], order);
                case 3:
                    return Reshape(dimLengths[0], dimLengths[1], dimLengths[2], order);
                case 4:
                    return Reshape(dimLengths[0], dimLengths[1], dimLengths[2], dimLengths[3], order);
                case 5:
                    return Reshape(dimLengths[0], dimLengths[1], dimLengths[2], dimLengths[3], dimLengths[4], order);
                case 6:
                    return Reshape(dimLengths[0], dimLengths[1], dimLengths[2], dimLengths[3], dimLengths[4], dimLengths[5], order);
                case 7:
                    return Reshape(dimLengths[0], dimLengths[1], dimLengths[2], dimLengths[3], dimLengths[4], dimLengths[5], dimLengths[6], order);
                default:
                    break;
            }
            if (dimLengths.Length > Size.MaxNumberOfDimensions) {
                throw new InvalidProgramException($"The maximum number of dimensions supported is {Size.MaxNumberOfDimensions}.");
            }
            #region more than 7 dims
            if (order != null && (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor)) {
                throw new ArgumentException("Invalid storage order. Valid storage orders are: ColumnMajor and RowMajor.");
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            StorageT ret = CreateSelf4Reshape(order);

            #region configure BSD
            unsafe {
                var bsd = ret.Size.GetBSD(true);
                System.Diagnostics.Debug.Assert(Settings.MinNumberOfArrayDimensions < dimLengths.Length);
                uint ndims = (uint)dimLengths.Length;
                long stride = 1;

                bsd[0] = (uint)ndims;
                bsd[1] = (Size.NumberOfElements);
                bsd[2] = (ret.Handles == Handles ? Size.BaseOffset : 0);
                if (order == StorageOrders.ColumnMajor) {
                    // column major 
                    for (uint i = 0; i < ndims; i++) {
                        bsd[3 + i] = (dimLengths[i]);
                        bsd[3 + ndims + i] = bsd[3 + i] == 1 ? 0 : stride;
                        stride *= dimLengths[i];
                    }
                } else {
                    // row major
                    for (uint i = ndims; i-- > 0;) {
                        bsd[3 + i] = (dimLengths[i]);
                        bsd[3 + ndims + i] = bsd[3 + i] == 1 ? 0 : stride;
                        stride *= dimLengths[i];
                    }
                }
                if (stride != Size.NumberOfElements) {
                    throw new ArgumentException("In a reshaping operation the number of elements must not change!");
                }

                ret.Size.Flags = Size.CONT_FLAG | (uint)order;
            }
            #endregion
            return ret;

            #endregion
        }

        /// <summary>
        /// Reshape from 'lengths' array.
        /// </summary>
        /// <param name="dimLengths">New dimension lengths.</param>
        /// <param name="order">Target storage order.</param>
        /// <returns>Reshaped array.</returns>
        internal virtual StorageT Reshape(long[] dimLengths, StorageOrders? order) {
            if (dimLengths == null || dimLengths.Length == 0) {
                throw new ArgumentException("Invalid parameter: dimLengths must not be null or empty.");
            }
            if (dimLengths.Length > Size.MaxNumberOfDimensions) {
                throw new ArgumentException($"The maximum number of dimensions supported is {Size.MaxNumberOfDimensions}.");
            }

            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
            if (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor) {
                throw new ArgumentException("Invalid storage order. Valid storage orders are: ColumnMajor and RowMajor.");
            }
            StorageT ret = CreateSelf4Reshape(order);

            #region configure BSD
            unsafe {
                var bsd = ret.Size.GetBSD(true);
                //System.Diagnostics.Debug.Assert(Settings.MinNumberOfArrayDimensions < dimLengths.Length);
                uint ndims = (uint)Math.Max(dimLengths.Length, Settings.MinNumberOfArrayDimensions);
                long stride = 1;

                #region infer missing parameter
                int pos = -1;
                for (int i = 0; i < dimLengths.Length; i++) {
                    if (dimLengths[i] < 0) {
                        if (pos >= 0) {
                            throw new ArgumentException("Reshape: exactly one single dimension length can be infered automatically. More than one parameter was specified with a negative value.");
                        }
                        pos = i;
                    } else {
                        stride *= dimLengths[i];
                    }
                }
                if (pos >= 0) {
                    if (stride == 0) {
                        // at least one dimension was specified 0
                        if (Size.NumberOfElements > 0) {
                            throw new ArgumentException("Reshape: invalid dimension length arguments provided. The number of elements must not change.");
                        }
                        dimLengths[pos] = 0;
                    } else {
                        dimLengths[pos] = (long)Size.NumberOfElements / stride;
                        if (dimLengths[pos] * stride != (long)Size.NumberOfElements) {
                            throw new ArgumentException("Reshape: invalid dimension length arguments provided.");
                        }
                    }

                }
                #endregion

                bsd[0] = (uint)ndims;
                bsd[1] = (Size.NumberOfElements);
                bsd[2] = ((ret.Handles == Handles ? Size.BaseOffset : 0));
                stride = 1;
                if (order == StorageOrders.ColumnMajor) {
                    // column major 
                    uint i = 0;
                    for (; i < dimLengths.Length; i++) {
                        bsd[3 + i] = (dimLengths[i]);
                        bsd[3 + ndims + i] = bsd[3 + i] == 1 ? 0 : stride;
                        stride *= dimLengths[i];
                    }
                    for (; i < ndims; i++) { // default dimensions
                        bsd[3 + i] = (1);
                        bsd[3 + ndims + i] = bsd[1];
                    }
                } else {
                    // row major
                    int i = (int)ndims;
                    for (; i-- > (ndims - dimLengths.Length);) {
                        bsd[3 + i] = (dimLengths[i]);
                        bsd[3 + ndims + i] = bsd[3 + i] == 1 ? 0 : stride;
                        stride *= dimLengths[i];
                    }
                    for (; i-- > 0;) { // default dimensions
                        bsd[3 + i] = (1);
                        bsd[3 + ndims + i] = bsd[1];
                    }
                }
                if (stride != Size.NumberOfElements) {
                    throw new ArgumentException("In a reshaping operation the number of elements must not change!");
                }

                ret.Size.Flags = Size.CONT_FLAG | (uint)order;
            }
            #endregion
            return ret;
        }

        #region dims as 'tuples' 
        internal StorageT Reshape<IndT>(BaseArray<IndT> dimLengths, StorageOrders? order) {

            using var _1 = ReaderLock.Create(dimLengths as ConcreteArray<IndT, Array<IndT>, InArray<IndT>, OutArray<IndT>, Array<IndT>, Storage<IndT>>, out var storage);

            if (Equals(storage,null) ||storage.Size.NumberOfElements < 1) {
                if (Size.NumberOfElements != 1) {
                    throw new ArgumentException($"DimLenghts were provided empty. Reshape to 0-D scalars is only possible for scalars. This array has the shape: {Size.ToString()}");
                }
                var ret = Create(Size.BaseOffset); // overridden in CellStorage -> ok!
                ret.Size.SetScalar(Size.BaseOffset, Math.Max(Settings.MinNumberOfArrayDimensions, 0));
                return ret; 
            } else {
                using (Scope.Enter()) {
                    Array<long> dims = MathInternal.convert<long>(storage.AsRetArray()); // releases dimLength in case of RetArray<IndT>
                    switch (storage.Size.NumberOfElements) {
                        case 1:
                            return Reshape(dims.GetValue(0), order);
                        case 2:
                            return Reshape(dims.GetValue(0), dims.GetValue(1), order);
                        case 3:
                            return Reshape(dims.GetValue(0), dims.GetValue(1), dims.GetValue(2), order);
                        case 4:
                            return Reshape(dims.GetValue(0), dims.GetValue(1), dims.GetValue(2), dims.GetValue(3), order);
                        case 5:
                            return Reshape(dims.GetValue(0), dims.GetValue(1), dims.GetValue(2), dims.GetValue(3), dims.GetValue(4), order);
                        case 6:
                            return Reshape(dims.GetValue(0), dims.GetValue(1), dims.GetValue(2), dims.GetValue(3), dims.GetValue(4), dims.GetValue(5), order);
                        case 7:
                            return Reshape(dims.GetValue(0), dims.GetValue(1), dims.GetValue(2), dims.GetValue(3), dims.GetValue(4), dims.GetValue(5), dims.GetValue(6), order);
                        default:
                            return Reshape(dims.GetArrayForRead(), order); 
                            //throw new ArgumentException($"DimLengths parameter exceeds the maximum number of array dimensions ({Size.MaxNumberOfDimensions}). Found: {storage.Size.NumberOfElements} elements.");
                    }
                }
            }
        }
        #endregion

    }
}
