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
using ILNumerics;
using static ILNumerics.Globals;
using static ILNumerics.Core.Functions.Builtin.MathInternal;
using ILNumerics.Core.Functions.Builtin;


/*!HC:TYPELIST:
<hycalper>
<type>
    <source locate="here">
        double
    </source>
    <destination>complex</destination>
    <destination>fcomplex</destination>
    <destination>float</destination>
</type>
<type>
    <source locate="here">
        dgetrf
    </source>
    <destination>zgetrf</destination>
    <destination>cgetrf</destination>
    <destination>sgetrf</destination>
</type>
</hycalper>
*/

namespace ILNumerics {

    public partial class ILMath {

        #region HYCALPER LOOPSTART 
        /// <summary>
        /// Determinant of square matrix.
        /// </summary>
        /// <param name="A">Input matrix (square).</param>
        /// <returns>Determinant of <paramref name="A"/>.</returns>
        /// <remarks>
        /// <para>The determinant is computed by decomposing <paramref name="A"/> into upper and lower triangular part using LU decomposition. </para>
        /// <para>The determinant of a scalar matrix is the scalar itself. If <paramref name="A"/> is empty an empty array will be returned.</para>
        /// </remarks>
        /// <example>Creating a nonsingular 4x4 (double) matrix and it's determinant
        /// <code><![CDATA[Array<double> A = ILMath.counter<double>(1.0, 1.0, 4, 4);
        ///A[1, 0] = 0.0;  // ensure that A 
        ///A[2, 3] = 0.0;  // has full rank
        ///A
        ///<Double> [4,4] 1...16 order:|
        ///         1          5          9         13
        ///         0          6         10         14
        ///         3          7         11          0
        ///         4          8         12         16
        /// det(A)
        /// //D
        /// <Double> [1,1] -360 order:|
        /// ]]></code></example>
        ///<exception cref="ArgumentException"> if <paramref name="A"/> is not a square matrix.</exception>
        public static Array<double> det(InArray<double> A) {
            using (Scope.Enter()) {
                Array<double> _A = A; 
                if (isnull(_A) || !_A.IsMatrix) {
                    throw new ArgumentException("det(_A): _A must be a matrix.");
                }
                if (_A.IsScalar) {
                    return _A.C;
                }

                if (_A.IsEmpty) {
                    return empty<double>(0); 
                }
                long m = _A.Size[0];
                if (m != _A.Size[1]) {
                    throw new ArgumentException("det: matrix _A must be square");
                }
                if (m == 2) {
                    return _A.GetValue(0, 0) * _A.GetValue(1, 1) - _A.GetValue(0, 1) * _A.GetValue(1, 0); 
                }
                
                Array<double> L = _A.C;
                L.Storage.EnsureStorageOrder(StorageOrders.ColumnMajor);

                Array<int> P = empty<int>(m, StorageOrders.ColumnMajor);
                //Array<int> permP = empty<int>(m, StorageOrders.ColumnMajor); 

                int info = 0;
                unsafe {
                    Lapack.dgetrf((int)m, (int)m, (double*)L.GetHostPointerForWrite(), (int)m, (int*)P.GetHostPointerForWrite(), ref info);

                    if (info < 0) {
                        throw new ArgumentException($"det(_A): error in lapack function dgetrf. Info: {info}.");
                    }
                    // number of row changes due to pivoting. All we need is the info odd/even for the sign of the result. 
                    // In a former attempt we computed the actual target row indices of _A. But it seems like this is not needed. 
                    // perm2indicesForward((int*)P.GetHostPointerForWrite(), (int*)permP.GetHostPointerForWrite(), (uint)m, (uint)m); 
                }
                long s = (P != counter<int>(1,1,m)).NumberTrues % 2;
                return prod(diag(L)) * (double)(s == 0 ? 1 : -1); 
            }
        }
        #endregion HYCALPER LOOPEND 
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
        /// <summary>
        /// Determinant of square matrix.
        /// </summary>
        /// <param name="A">Input matrix (square).</param>
        /// <returns>Determinant of <paramref name="A"/>.</returns>
        /// <remarks>
        /// <para>The determinant is computed by decomposing <paramref name="A"/> into upper and lower triangular part using LU decomposition. </para>
        /// <para>The determinant of a scalar matrix is the scalar itself. If <paramref name="A"/> is empty an empty array will be returned.</para>
        /// </remarks>
        /// <example>Creating a nonsingular 4x4 (float) matrix and it's determinant
        /// <code><![CDATA[Array<float> A = ILMath.counter<float>(1.0, 1.0, 4, 4);
        ///A[1, 0] = 0.0;  // ensure that A 
        ///A[2, 3] = 0.0;  // has full rank
        ///A
        ///<Double> [4,4] 1...16 order:|
        ///         1          5          9         13
        ///         0          6         10         14
        ///         3          7         11          0
        ///         4          8         12         16
        /// det(A)
        /// //D
        /// <Double> [1,1] -360 order:|
        /// ]]></code></example>
        ///<exception cref="ArgumentException"> if <paramref name="A"/> is not a square matrix.</exception>
        public static Array<float> det(InArray<float> A) {
            using (Scope.Enter()) {
                Array<float> _A = A; 
                if (isnull(_A) || !_A.IsMatrix) {
                    throw new ArgumentException("det(_A): _A must be a matrix.");
                }
                if (_A.IsScalar) {
                    return _A.C;
                }

                if (_A.IsEmpty) {
                    return empty<float>(0); 
                }
                long m = _A.Size[0];
                if (m != _A.Size[1]) {
                    throw new ArgumentException("det: matrix _A must be square");
                }
                if (m == 2) {
                    return _A.GetValue(0, 0) * _A.GetValue(1, 1) - _A.GetValue(0, 1) * _A.GetValue(1, 0); 
                }
                
                Array<float> L = _A.C;
                L.Storage.EnsureStorageOrder(StorageOrders.ColumnMajor);

                Array<int> P = empty<int>(m, StorageOrders.ColumnMajor);
                //Array<int> permP = empty<int>(m, StorageOrders.ColumnMajor); 

                int info = 0;
                unsafe {
                    Lapack.sgetrf((int)m, (int)m, (float*)L.GetHostPointerForWrite(), (int)m, (int*)P.GetHostPointerForWrite(), ref info);

                    if (info < 0) {
                        throw new ArgumentException($"det(_A): error in lapack function sgetrf. Info: {info}.");
                    }
                    // number of row changes due to pivoting. All we need is the info odd/even for the sign of the result. 
                    // In a former attempt we computed the actual target row indices of _A. But it seems like this is not needed. 
                    // perm2indicesForward((int*)P.GetHostPointerForWrite(), (int*)permP.GetHostPointerForWrite(), (uint)m, (uint)m); 
                }
                long s = (P != counter<int>(1,1,m)).NumberTrues % 2;
                return prod(diag(L)) * (float)(s == 0 ? 1 : -1); 
            }
        }
        /// <summary>
        /// Determinant of square matrix.
        /// </summary>
        /// <param name="A">Input matrix (square).</param>
        /// <returns>Determinant of <paramref name="A"/>.</returns>
        /// <remarks>
        /// <para>The determinant is computed by decomposing <paramref name="A"/> into upper and lower triangular part using LU decomposition. </para>
        /// <para>The determinant of a scalar matrix is the scalar itself. If <paramref name="A"/> is empty an empty array will be returned.</para>
        /// </remarks>
        /// <example>Creating a nonsingular 4x4 (fcomplex) matrix and it's determinant
        /// <code><![CDATA[Array<fcomplex> A = ILMath.counter<fcomplex>(1.0, 1.0, 4, 4);
        ///A[1, 0] = 0.0;  // ensure that A 
        ///A[2, 3] = 0.0;  // has full rank
        ///A
        ///<Double> [4,4] 1...16 order:|
        ///         1          5          9         13
        ///         0          6         10         14
        ///         3          7         11          0
        ///         4          8         12         16
        /// det(A)
        /// //D
        /// <Double> [1,1] -360 order:|
        /// ]]></code></example>
        ///<exception cref="ArgumentException"> if <paramref name="A"/> is not a square matrix.</exception>
        public static Array<fcomplex> det(InArray<fcomplex> A) {
            using (Scope.Enter()) {
                Array<fcomplex> _A = A; 
                if (isnull(_A) || !_A.IsMatrix) {
                    throw new ArgumentException("det(_A): _A must be a matrix.");
                }
                if (_A.IsScalar) {
                    return _A.C;
                }

                if (_A.IsEmpty) {
                    return empty<fcomplex>(0); 
                }
                long m = _A.Size[0];
                if (m != _A.Size[1]) {
                    throw new ArgumentException("det: matrix _A must be square");
                }
                if (m == 2) {
                    return _A.GetValue(0, 0) * _A.GetValue(1, 1) - _A.GetValue(0, 1) * _A.GetValue(1, 0); 
                }
                
                Array<fcomplex> L = _A.C;
                L.Storage.EnsureStorageOrder(StorageOrders.ColumnMajor);

                Array<int> P = empty<int>(m, StorageOrders.ColumnMajor);
                //Array<int> permP = empty<int>(m, StorageOrders.ColumnMajor); 

                int info = 0;
                unsafe {
                    Lapack.cgetrf((int)m, (int)m, (fcomplex*)L.GetHostPointerForWrite(), (int)m, (int*)P.GetHostPointerForWrite(), ref info);

                    if (info < 0) {
                        throw new ArgumentException($"det(_A): error in lapack function cgetrf. Info: {info}.");
                    }
                    // number of row changes due to pivoting. All we need is the info odd/even for the sign of the result. 
                    // In a former attempt we computed the actual target row indices of _A. But it seems like this is not needed. 
                    // perm2indicesForward((int*)P.GetHostPointerForWrite(), (int*)permP.GetHostPointerForWrite(), (uint)m, (uint)m); 
                }
                long s = (P != counter<int>(1,1,m)).NumberTrues % 2;
                return prod(diag(L)) * (fcomplex)(s == 0 ? 1 : -1); 
            }
        }
        /// <summary>
        /// Determinant of square matrix.
        /// </summary>
        /// <param name="A">Input matrix (square).</param>
        /// <returns>Determinant of <paramref name="A"/>.</returns>
        /// <remarks>
        /// <para>The determinant is computed by decomposing <paramref name="A"/> into upper and lower triangular part using LU decomposition. </para>
        /// <para>The determinant of a scalar matrix is the scalar itself. If <paramref name="A"/> is empty an empty array will be returned.</para>
        /// </remarks>
        /// <example>Creating a nonsingular 4x4 (complex) matrix and it's determinant
        /// <code><![CDATA[Array<complex> A = ILMath.counter<complex>(1.0, 1.0, 4, 4);
        ///A[1, 0] = 0.0;  // ensure that A 
        ///A[2, 3] = 0.0;  // has full rank
        ///A
        ///<Double> [4,4] 1...16 order:|
        ///         1          5          9         13
        ///         0          6         10         14
        ///         3          7         11          0
        ///         4          8         12         16
        /// det(A)
        /// //D
        /// <Double> [1,1] -360 order:|
        /// ]]></code></example>
        ///<exception cref="ArgumentException"> if <paramref name="A"/> is not a square matrix.</exception>
        public static Array<complex> det(InArray<complex> A) {
            using (Scope.Enter()) {
                Array<complex> _A = A; 
                if (isnull(_A) || !_A.IsMatrix) {
                    throw new ArgumentException("det(_A): _A must be a matrix.");
                }
                if (_A.IsScalar) {
                    return _A.C;
                }

                if (_A.IsEmpty) {
                    return empty<complex>(0); 
                }
                long m = _A.Size[0];
                if (m != _A.Size[1]) {
                    throw new ArgumentException("det: matrix _A must be square");
                }
                if (m == 2) {
                    return _A.GetValue(0, 0) * _A.GetValue(1, 1) - _A.GetValue(0, 1) * _A.GetValue(1, 0); 
                }
                
                Array<complex> L = _A.C;
                L.Storage.EnsureStorageOrder(StorageOrders.ColumnMajor);

                Array<int> P = empty<int>(m, StorageOrders.ColumnMajor);
                //Array<int> permP = empty<int>(m, StorageOrders.ColumnMajor); 

                int info = 0;
                unsafe {
                    Lapack.zgetrf((int)m, (int)m, (complex*)L.GetHostPointerForWrite(), (int)m, (int*)P.GetHostPointerForWrite(), ref info);

                    if (info < 0) {
                        throw new ArgumentException($"det(_A): error in lapack function zgetrf. Info: {info}.");
                    }
                    // number of row changes due to pivoting. All we need is the info odd/even for the sign of the result. 
                    // In a former attempt we computed the actual target row indices of _A. But it seems like this is not needed. 
                    // perm2indicesForward((int*)P.GetHostPointerForWrite(), (int*)permP.GetHostPointerForWrite(), (uint)m, (uint)m); 
                }
                long s = (P != counter<int>(1,1,m)).NumberTrues % 2;
                return prod(diag(L)) * (complex)(s == 0 ? 1 : -1); 
            }
        }

#endregion HYCALPER AUTO GENERATED CODE
   }
}
