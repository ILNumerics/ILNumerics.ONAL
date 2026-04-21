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
using static ILNumerics.Globals;

namespace ILNumerics.Core.Functions.Builtin {

    internal partial class MathInternal {

        /// <summary>
        /// Creates a new matrix with element type <see cref="double"/> having diagonal values of 1.0.  
        /// </summary>
        /// <param name="rows">Number of rows to create.</param>
        /// <param name="columns">Number of columns to create.</param>
        /// <returns>Unity matrix (diagonal matrix) of type double, column major storage.</returns>
        internal static Array<double> eye(int rows, int columns) {
            return eye<double>(1.0, rows, columns, StorageOrders.ColumnMajor); 
        }
        /// <summary>
        /// Create unity matrix, arbitrary numeric type.
        /// </summary>
        /// <param name="rows">Number of rows.</param>
        /// <param name="columns">Number of columns.</param>
        /// <returns>Unity matrix (diagonal matrix) of element type T.</returns>
        /// <typeparam name="T">Element type, must derive from <see cref="System.ValueType"/> and <see cref="IConvertible"/>.</typeparam>
        /// <exception cref="ArgumentException">If the type specified is not supported. Supported types are: double, float, complex, fcomplex, int, long, short, byte</exception>
        internal static Array<T> eye<T>(int rows, int columns) where T : struct, IConvertible {
            return eye<T>((T)ones<T>(1), rows, columns, StorageOrders.ColumnMajor);
        }
        /// <summary>
        /// Creates a diagonal matrix, arbitrary value type, diagonal value and storage order.
        /// </summary>
        /// <param name="diagVal">The value to be assigned to all diagonal elements of the returned matrix.</param>
        /// <param name="rows">Number of rows to create.</param>
        /// <param name="columns">Number of columns to create.</param>
        /// <param name="order">[Optional] The storage order for the matrix returned. Default: ColumnMajor.</param>
        /// <returns>Matrix of element type <typeparamref name="T"/> having the element <paramref name="diagVal"/> on the main diagonal.</returns>
        /// <typeparam name="T">Element type, must be a value type.</typeparam>
        internal static Array<T> eye<T>(T diagVal, int rows, int columns, StorageOrders order = StorageOrders.ColumnMajor)
            where T : struct {
            using (Scope.Enter()) {
                Array<T> ret = zeros<T>(rows, columns, order);
                int diagLen = Math.Min(rows, columns);
                for (int i = 0; i < diagLen; i++)
                    ret.SetValue(diagVal, i, i);
                return ret;
            }

        }
        /// <summary>
        /// Creates a new matrix with element type <see cref="double"/> having diagonal values of 1.0.  
        /// </summary>
        /// <param name="rows">Number of rows to create.</param>
        /// <param name="columns">Number of columns to create.</param>
        /// <returns>Unity matrix (diagonal matrix) of type double, column major storage.</returns>
        internal static Array<double> eye(long rows, long columns) {
            return eye<double>(1.0, rows, columns, StorageOrders.ColumnMajor); 
        }
        /// <summary>
        /// Create unity matrix, arbitrary numeric type.
        /// </summary>
        /// <param name="rows">Number of rows.</param>
        /// <param name="columns">Number of columns.</param>
        /// <returns>Unity matrix (diagonal matrix) of element type T.</returns>
        /// <typeparam name="T">Element type, must derive from <see cref="System.ValueType"/> and <see cref="IConvertible"/>.</typeparam>
        /// <exception cref="ArgumentException">If the type specified is not supported. Supported types are: double, float, complex, fcomplex, int, long, short, byte</exception>
        internal static Array<T> eye<T>(long rows, long columns) where T : struct, IConvertible {
            return eye<T>((T)ones<T>(1), rows, columns, StorageOrders.ColumnMajor);
        }
        /// <summary>
        /// Creates a diagonal matrix, arbitrary value type, diagonal value and storage order.
        /// </summary>
        /// <param name="diagVal">The value to be assigned to all diagonal elements of the returned matrix.</param>
        /// <param name="rows">Number of rows to create.</param>
        /// <param name="columns">Number of columns to create.</param>
        /// <param name="order">[Optional] The storage order for the matrix returned. Default: ColumnMajor.</param>
        /// <returns>Matrix of element type <typeparamref name="T"/> having the element <paramref name="diagVal"/> on the main diagonal.</returns>
        /// <typeparam name="T">Element type, must be a value type.</typeparam>
        internal static Array<T> eye<T>(T diagVal, long rows, long columns, StorageOrders order = StorageOrders.ColumnMajor)
            where T : struct {
            using (Scope.Enter()) {
                Array<T> ret = zeros<T>(rows, columns, order);
                long diagLen = Math.Min(rows, columns);
                for (long i = 0; i < diagLen; i++)
                    ret.SetValue(diagVal, i, i);
                return ret;
            }

        }
    }
}
