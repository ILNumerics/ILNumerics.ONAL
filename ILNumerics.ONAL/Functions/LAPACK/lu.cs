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
using ILNumerics.Core.MemoryLayer;
using static ILNumerics.Globals;
using static ILNumerics.Core.Functions.Builtin.MathInternal;
using ILNumerics.Core.Functions.Builtin;
using System.Security;

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
        /// LU matrix decomposition. Decompose general matrix <paramref name="A"/> into strictly upper part and lower part. 
        /// </summary>
        /// <param name="A">Input matrix. Size [m x n].</param>
        /// <returns>Triangular matrices L and U composed into a single matrix as returned from LAPACK function ?getrf. Size [m x n].</returns>
        /// <remarks><para>The matrix returned is composed out of the lower triangular matrix L with unit diagonal and the strictly upper triangular matrix U.</para>
        /// <code>
        /// :'''''''|
        /// |1 \    |
        /// | 1 \ R |
        /// |  1 \  |
        /// | L 1 \ |
        /// |    1 \|
        /// '''''''''
        /// </code>
        /// <para>This overload is mostly needed for further operations via Lapack libraries. If you need the 
        /// L and U matrices directly, consider using the overload
        /// <see cref="lu(InArray{double}, OutArray{double}, OutArray{double})"/> instead.</para>
        /// <para>lu uses the Lapack function ?getrf, which is provided by a native LAPACK implementation (mostly Intel's MKL).</para>
        /// </remarks>
        /// <seealso cref="lu(InArray{double}, OutArray{double}, OutArray{double})"/>
        /// <exception cref="ArgumentException"> if input A is not a matrix.</exception>
        public static Array<double> lu(InArray<double> A) {
            return lu(A, null, null);
        }
        /// <summary>
        /// Decompose matrix <paramref name="A"/> into uper and lower triangular matrices. Also generates pivoting permutation matrix. 
        /// </summary>
        /// <param name="A">Input matrix. Size [m x n].</param>
        /// <param name="U">[Output] Upper triangular matrix. Size [min(m,n) x n]. This can be null.</param>
        /// <param name="P">[Output, optional] Permutation matrix <paramref name="P"/>. Size [m x m]. Default: (null) do not compute the permutation matrix. L is ordered propery before the function returns.</param>
        /// <returns>Lower triangular matrix L. Size [m x min(m,n)].</returns>
        /// <remarks><para><paramref name="A"/> is decomposed into L and <paramref name="U"/> so that the equation 
        /// <c>ILMath.multiply(L,U) == ILMath.multiply(P,A)</c> holds within the range of round off errors.</para>
        /// <para>The matrix L returned is a lower triangular matrix with unit diagonal. Depending on <paramref name="P"/> the rows 
        /// of L may be reordered due to pivoting during the triangulation. If <paramref name="P"/> is not null on entry (i.e.: 
        /// the permutation matrix <paramref name="P"/> is computed and returned from <see cref="lu(InArray{double}, OutArray{double}, OutArray{double})"/>) L 
        /// is truely lower triangular. In this case the equation 
        /// <c>ILMath.multiply(L,U) == ILMath.multiply(P,A)</c> holds. Note, that the result of matrix multiplying L with <paramref name="U"/> 
        /// yields a version of <paramref name="A"/> having its rows reordered. <paramref name="P"/> permutes the rows of <paramref name="A"/> accordingly. 
        /// Note further, that <paramref name="P"/> is suitable to be multiplied 
        /// to <paramref name="A"/>, while some other systems return <paramref name="P"/><sup>-1</sup> for reordering the result L**<paramref name="U"/> instead.</para>
        /// <para>If <paramref name="P"/> is null on entry (i.e.: the permutation matrix <paramref name="P"/> is not requested and thus not returned) any
        /// permutation performed in the triangulation is reversed before returning L so that the matrix L returned is not guranteed to be lower triangular. Instead, 
        /// the rows of L are reordered by P<sup>-1</sup>**L. In this case the simplified equation <c>ILMath.multiply(L,U) == ILMath.multiply(A)</c> holds (default). </para>
        /// <para><paramref name="U"/> is a strictly upper triangular matrix. Any permutation is performed on L and not on <paramref name="U"/>. Thus, the order of rows of 
        /// <paramref name="U"/> does not depend on <paramref name="P"/>.</para>
        /// <example><![CDATA[ 
        /// <code>
        /// // Construct a matrix A: 
        /// Array<double> A = new double[,]{
        ///    {1, 2, 3},
        ///    {4, 4, 4},
        ///    {5, 6, 7}
        /// };
        /// // A
        /// // <Double> [3x3] order: - 
        /// //(:,:) 
        /// // 1    2    3 
        /// // 4    4    4 
        /// // 5    6    7 
        /// //
        /// // define arrays serving as output arrays for U and P
        /// Array<double> U = 1, P = 1;
        /// Array<double> L = ILMath.lu(A, U, P); 
        /// 
        /// // L 
        /// // <Double> [3,3] 1...1 order:|
        /// //            1          0          0
        /// //     0,800000          1          0
        /// //     0,200000  -1,000000          1
        /// // 
        /// // U 
        /// //<Double> [3,3] 5...1,11022302462516E-15 order:|
        /// //            5          6          7
        /// //            0  -0,800000  -1,600000
        /// //            0          0   0,000000
        /// //
        /// //P
        /// //<Double> [3,3] 0...0 order:|
        /// //            0          0          1
        /// //            0          1          0
        /// //            1          0          0
        /// </code>]]>
        /// L and U are triangular matrices. P is returned as well. The result of L**U gives:
        /// <code>
        /// multiply(L,U)
        ///            5          6          7
        ///            4          4          4
        ///            1          2          3
        /// </code>
        /// This result matches a reordered version of A, which can be computed by help of P:
        /// multiply(P,A)
        ///            5          6          7
        ///            4          4          4
        ///            1          2          3
        /// </example>
        /// <example>
        /// // Computing the LU factorization of A without returning the permutation matrix P: 
        /// <![CDATA[<code>
        /// L = lu(A,U); 
        /// 
        /// // L 
        /// // <Double> [3,3] 1...1 order:|
        /// //   0,200000  -1,000000          1
        /// //   0,800000          1          0
        /// //          1          0          0
        /// // 
        /// // U 
        /// //<Double> [3,3] 5...1,11022302462516E-15 order:|
        /// //            5          6          7
        /// //            0  -0,800000  -1,600000
        /// //            0          0   0,000000
        /// //
        /// </code>]]>
        /// This time, while U did not change, L is not triangular but a reordered version of a lower triangular matrix. However, 
        /// multiplying L ** U gives A directly: 
        /// <![CDATA[<code>
        ///  
        /// multiply(L,U)
        ///            1          2          3
        ///            4          4          4
        ///            5          6          7
        /// </code>]]>
        /// </example>
        /// <para>lu uses the Lapack function ?getrf.</para>
        /// <para>The input matrix <paramref name="A"/> is not altered.</para>
        /// <para>Either of <paramref name="U"/> and/or <paramref name="P"/> can be null on entry. If <paramref name="U"/> 
        /// is null the upper triangular matrix will be computed but not returned. Instead, the compressed form of L 
        /// and <paramref name="U"/> is returned as created by the LAPACK function ?getrf:  
        /// </para>
        /// <para><code>
        /// :'''''''|
        /// |1 \    |
        /// | 1 \ R |
        /// |  1 \  |
        /// | L 1 \ |
        /// |    1 \|
        /// '''''''''
        /// </code></para>
        /// <para>If <paramref name="P"/> is null on entry and <paramref name="U"/> is not null the permutation matrix is not computed but rows of L are reordered accordingly in order to fullfill the equation 'A = L**U'.</para>
        /// </remarks>
        /// <seealso cref="chol(InArray{double}, bool)"/>
        /// <seealso cref="ILNumerics.Core.Native.ILapack"/>
        /// <exception cref="ArgumentException"> if input <paramref name="A"/> is not a matrix.</exception>
        
        public static Array<double> lu(InArray<double> A, OutArray<double> U
                                                            , OutArray<double> P = null) {
            using (Scope.Enter()) {

                Array<double> _A = A; 

                if (!_A.IsMatrix)
                    throw new ArgumentException("lu() is defined for matrices only.");
                long m = _A.Size[0], n = _A.Size[1], minMN = (m < n) ? m : n;

                int info = 0;
                Array<double> L = _A.C;

                MemoryHandle pivH = null, outPermH = null;
                try {
                    unsafe {
                        pivH = New<int>(Math.Max(1, minMN));
                        int* pivInd = (int*)pivH.Pointer;

                        Lapack.dgetrf((int)m, (int)n, (double*)L.GetHostPointerForWrite(StorageOrders.ColumnMajor), (int)m, pivInd, ref info);
                        if (info < 0) {
                            throw new ArgumentException($"lu(): invalid parameter. Error code returned from LAPACK.dgetrf: {info}.");
                        } else {
                            // completed successfuly 
                            if (!Object.Equals(U, null)) {
                                lock (U.SynchObj)
                                    U.a = copyUpperTriangle<double>(L, minMN, n);
                                outPermH = New<int>(m);
                                var outPerm = (int*)outPermH.Pointer;
                                if (!Object.Equals(P, null)) {
                                    perm2indicesForward(pivInd, outPerm, (uint)minMN, (uint)m);
                                    L.a = copyLowerTriangle<double>(L, m, minMN, (double)1);
                                    lock (P.SynchObj) {
                                        P.a = zeros<double>(m, m); // diag(ones<double>(m)); // ! sic: P is [m x m] 

                                        // construct permutation matrix P 
                                        for (uint r = 0; r < m; r++) {
                                            P[r, outPerm[r]] = (double)1.0;
                                        }
                                    }
                                } else {
                                    perm2indicesBackward(pivInd, outPerm, (uint)minMN, (uint)m);
                                    L.a = copyLowerTrianglePerm<double>(L, (uint)m, (uint)minMN, (double)1, outPerm);
                                }
                            }
                        }
                        return L;
                    }
                } finally {
                    if (pivH != null) free<int>(pivH, 0);
                    if (outPermH != null) free<int>(outPermH, 0);
                }
            }
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
        /// <summary>
        /// LU matrix decomposition. Decompose general matrix <paramref name="A"/> into strictly upper part and lower part. 
        /// </summary>
        /// <param name="A">Input matrix. Size [m x n].</param>
        /// <returns>Triangular matrices L and U composed into a single matrix as returned from LAPACK function ?getrf. Size [m x n].</returns>
        /// <remarks><para>The matrix returned is composed out of the lower triangular matrix L with unit diagonal and the strictly upper triangular matrix U.</para>
        /// <code>
        /// :'''''''|
        /// |1 \    |
        /// | 1 \ R |
        /// |  1 \  |
        /// | L 1 \ |
        /// |    1 \|
        /// '''''''''
        /// </code>
        /// <para>This overload is mostly needed for further operations via Lapack libraries. If you need the 
        /// L and U matrices directly, consider using the overload
        /// <see cref="lu(InArray{float}, OutArray{float}, OutArray{float})"/> instead.</para>
        /// <para>lu uses the Lapack function ?getrf, which is provided by a native LAPACK implementation (mostly Intel's MKL).</para>
        /// </remarks>
        /// <seealso cref="lu(InArray{float}, OutArray{float}, OutArray{float})"/>
        /// <exception cref="ArgumentException"> if input A is not a matrix.</exception>
        public static Array<float> lu(InArray<float> A) {
            return lu(A, null, null);
        }
        /// <summary>
        /// Decompose matrix <paramref name="A"/> into uper and lower triangular matrices. Also generates pivoting permutation matrix. 
        /// </summary>
        /// <param name="A">Input matrix. Size [m x n].</param>
        /// <param name="U">[Output] Upper triangular matrix. Size [min(m,n) x n]. This can be null.</param>
        /// <param name="P">[Output, optional] Permutation matrix <paramref name="P"/>. Size [m x m]. Default: (null) do not compute the permutation matrix. L is ordered propery before the function returns.</param>
        /// <returns>Lower triangular matrix L. Size [m x min(m,n)].</returns>
        /// <remarks><para><paramref name="A"/> is decomposed into L and <paramref name="U"/> so that the equation 
        /// <c>ILMath.multiply(L,U) == ILMath.multiply(P,A)</c> holds within the range of round off errors.</para>
        /// <para>The matrix L returned is a lower triangular matrix with unit diagonal. Depending on <paramref name="P"/> the rows 
        /// of L may be reordered due to pivoting during the triangulation. If <paramref name="P"/> is not null on entry (i.e.: 
        /// the permutation matrix <paramref name="P"/> is computed and returned from <see cref="lu(InArray{float}, OutArray{float}, OutArray{float})"/>) L 
        /// is truely lower triangular. In this case the equation 
        /// <c>ILMath.multiply(L,U) == ILMath.multiply(P,A)</c> holds. Note, that the result of matrix multiplying L with <paramref name="U"/> 
        /// yields a version of <paramref name="A"/> having its rows reordered. <paramref name="P"/> permutes the rows of <paramref name="A"/> accordingly. 
        /// Note further, that <paramref name="P"/> is suitable to be multiplied 
        /// to <paramref name="A"/>, while some other systems return <paramref name="P"/><sup>-1</sup> for reordering the result L**<paramref name="U"/> instead.</para>
        /// <para>If <paramref name="P"/> is null on entry (i.e.: the permutation matrix <paramref name="P"/> is not requested and thus not returned) any
        /// permutation performed in the triangulation is reversed before returning L so that the matrix L returned is not guranteed to be lower triangular. Instead, 
        /// the rows of L are reordered by P<sup>-1</sup>**L. In this case the simplified equation <c>ILMath.multiply(L,U) == ILMath.multiply(A)</c> holds (default). </para>
        /// <para><paramref name="U"/> is a strictly upper triangular matrix. Any permutation is performed on L and not on <paramref name="U"/>. Thus, the order of rows of 
        /// <paramref name="U"/> does not depend on <paramref name="P"/>.</para>
        /// <example><![CDATA[ 
        /// <code>
        /// // Construct a matrix A: 
        /// Array<float> A = new float[,]{
        ///    {1, 2, 3},
        ///    {4, 4, 4},
        ///    {5, 6, 7}
        /// };
        /// // A
        /// // <Double> [3x3] order: - 
        /// //(:,:) 
        /// // 1    2    3 
        /// // 4    4    4 
        /// // 5    6    7 
        /// //
        /// // define arrays serving as output arrays for U and P
        /// Array<float> U = 1, P = 1;
        /// Array<float> L = ILMath.lu(A, U, P); 
        /// 
        /// // L 
        /// // <Double> [3,3] 1...1 order:|
        /// //            1          0          0
        /// //     0,800000          1          0
        /// //     0,200000  -1,000000          1
        /// // 
        /// // U 
        /// //<Double> [3,3] 5...1,11022302462516E-15 order:|
        /// //            5          6          7
        /// //            0  -0,800000  -1,600000
        /// //            0          0   0,000000
        /// //
        /// //P
        /// //<Double> [3,3] 0...0 order:|
        /// //            0          0          1
        /// //            0          1          0
        /// //            1          0          0
        /// </code>]]>
        /// L and U are triangular matrices. P is returned as well. The result of L**U gives:
        /// <code>
        /// multiply(L,U)
        ///            5          6          7
        ///            4          4          4
        ///            1          2          3
        /// </code>
        /// This result matches a reordered version of A, which can be computed by help of P:
        /// multiply(P,A)
        ///            5          6          7
        ///            4          4          4
        ///            1          2          3
        /// </example>
        /// <example>
        /// // Computing the LU factorization of A without returning the permutation matrix P: 
        /// <![CDATA[<code>
        /// L = lu(A,U); 
        /// 
        /// // L 
        /// // <Double> [3,3] 1...1 order:|
        /// //   0,200000  -1,000000          1
        /// //   0,800000          1          0
        /// //          1          0          0
        /// // 
        /// // U 
        /// //<Double> [3,3] 5...1,11022302462516E-15 order:|
        /// //            5          6          7
        /// //            0  -0,800000  -1,600000
        /// //            0          0   0,000000
        /// //
        /// </code>]]>
        /// This time, while U did not change, L is not triangular but a reordered version of a lower triangular matrix. However, 
        /// multiplying L ** U gives A directly: 
        /// <![CDATA[<code>
        ///  
        /// multiply(L,U)
        ///            1          2          3
        ///            4          4          4
        ///            5          6          7
        /// </code>]]>
        /// </example>
        /// <para>lu uses the Lapack function ?getrf.</para>
        /// <para>The input matrix <paramref name="A"/> is not altered.</para>
        /// <para>Either of <paramref name="U"/> and/or <paramref name="P"/> can be null on entry. If <paramref name="U"/> 
        /// is null the upper triangular matrix will be computed but not returned. Instead, the compressed form of L 
        /// and <paramref name="U"/> is returned as created by the LAPACK function ?getrf:  
        /// </para>
        /// <para><code>
        /// :'''''''|
        /// |1 \    |
        /// | 1 \ R |
        /// |  1 \  |
        /// | L 1 \ |
        /// |    1 \|
        /// '''''''''
        /// </code></para>
        /// <para>If <paramref name="P"/> is null on entry and <paramref name="U"/> is not null the permutation matrix is not computed but rows of L are reordered accordingly in order to fullfill the equation 'A = L**U'.</para>
        /// </remarks>
        /// <seealso cref="chol(InArray{float}, bool)"/>
        /// <seealso cref="ILNumerics.Core.Native.ILapack"/>
        /// <exception cref="ArgumentException"> if input <paramref name="A"/> is not a matrix.</exception>
        
        public static Array<float> lu(InArray<float> A, OutArray<float> U
                                                            , OutArray<float> P = null) {
            using (Scope.Enter()) {

                Array<float> _A = A; 

                if (!_A.IsMatrix)
                    throw new ArgumentException("lu() is defined for matrices only.");
                long m = _A.Size[0], n = _A.Size[1], minMN = (m < n) ? m : n;

                int info = 0;
                Array<float> L = _A.C;

                MemoryHandle pivH = null, outPermH = null;
                try {
                    unsafe {
                        pivH = New<int>(Math.Max(1, minMN));
                        int* pivInd = (int*)pivH.Pointer;

                        Lapack.sgetrf((int)m, (int)n, (float*)L.GetHostPointerForWrite(StorageOrders.ColumnMajor), (int)m, pivInd, ref info);
                        if (info < 0) {
                            throw new ArgumentException($"lu(): invalid parameter. Error code returned from LAPACK.sgetrf: {info}.");
                        } else {
                            // completed successfuly 
                            if (!Object.Equals(U, null)) {
                                lock (U.SynchObj)
                                    U.a = copyUpperTriangle<float>(L, minMN, n);
                                outPermH = New<int>(m);
                                var outPerm = (int*)outPermH.Pointer;
                                if (!Object.Equals(P, null)) {
                                    perm2indicesForward(pivInd, outPerm, (uint)minMN, (uint)m);
                                    L.a = copyLowerTriangle<float>(L, m, minMN, (float)1);
                                    lock (P.SynchObj) {
                                        P.a = zeros<float>(m, m); // diag(ones<float>(m)); // ! sic: P is [m x m] 

                                        // construct permutation matrix P 
                                        for (uint r = 0; r < m; r++) {
                                            P[r, outPerm[r]] = (float)1.0;
                                        }
                                    }
                                } else {
                                    perm2indicesBackward(pivInd, outPerm, (uint)minMN, (uint)m);
                                    L.a = copyLowerTrianglePerm<float>(L, (uint)m, (uint)minMN, (float)1, outPerm);
                                }
                            }
                        }
                        return L;
                    }
                } finally {
                    if (pivH != null) free<int>(pivH, 0);
                    if (outPermH != null) free<int>(outPermH, 0);
                }
            }
        }
        /// <summary>
        /// LU matrix decomposition. Decompose general matrix <paramref name="A"/> into strictly upper part and lower part. 
        /// </summary>
        /// <param name="A">Input matrix. Size [m x n].</param>
        /// <returns>Triangular matrices L and U composed into a single matrix as returned from LAPACK function ?getrf. Size [m x n].</returns>
        /// <remarks><para>The matrix returned is composed out of the lower triangular matrix L with unit diagonal and the strictly upper triangular matrix U.</para>
        /// <code>
        /// :'''''''|
        /// |1 \    |
        /// | 1 \ R |
        /// |  1 \  |
        /// | L 1 \ |
        /// |    1 \|
        /// '''''''''
        /// </code>
        /// <para>This overload is mostly needed for further operations via Lapack libraries. If you need the 
        /// L and U matrices directly, consider using the overload
        /// <see cref="lu(InArray{fcomplex}, OutArray{fcomplex}, OutArray{fcomplex})"/> instead.</para>
        /// <para>lu uses the Lapack function ?getrf, which is provided by a native LAPACK implementation (mostly Intel's MKL).</para>
        /// </remarks>
        /// <seealso cref="lu(InArray{fcomplex}, OutArray{fcomplex}, OutArray{fcomplex})"/>
        /// <exception cref="ArgumentException"> if input A is not a matrix.</exception>
        public static Array<fcomplex> lu(InArray<fcomplex> A) {
            return lu(A, null, null);
        }
        /// <summary>
        /// Decompose matrix <paramref name="A"/> into uper and lower triangular matrices. Also generates pivoting permutation matrix. 
        /// </summary>
        /// <param name="A">Input matrix. Size [m x n].</param>
        /// <param name="U">[Output] Upper triangular matrix. Size [min(m,n) x n]. This can be null.</param>
        /// <param name="P">[Output, optional] Permutation matrix <paramref name="P"/>. Size [m x m]. Default: (null) do not compute the permutation matrix. L is ordered propery before the function returns.</param>
        /// <returns>Lower triangular matrix L. Size [m x min(m,n)].</returns>
        /// <remarks><para><paramref name="A"/> is decomposed into L and <paramref name="U"/> so that the equation 
        /// <c>ILMath.multiply(L,U) == ILMath.multiply(P,A)</c> holds within the range of round off errors.</para>
        /// <para>The matrix L returned is a lower triangular matrix with unit diagonal. Depending on <paramref name="P"/> the rows 
        /// of L may be reordered due to pivoting during the triangulation. If <paramref name="P"/> is not null on entry (i.e.: 
        /// the permutation matrix <paramref name="P"/> is computed and returned from <see cref="lu(InArray{fcomplex}, OutArray{fcomplex}, OutArray{fcomplex})"/>) L 
        /// is truely lower triangular. In this case the equation 
        /// <c>ILMath.multiply(L,U) == ILMath.multiply(P,A)</c> holds. Note, that the result of matrix multiplying L with <paramref name="U"/> 
        /// yields a version of <paramref name="A"/> having its rows reordered. <paramref name="P"/> permutes the rows of <paramref name="A"/> accordingly. 
        /// Note further, that <paramref name="P"/> is suitable to be multiplied 
        /// to <paramref name="A"/>, while some other systems return <paramref name="P"/><sup>-1</sup> for reordering the result L**<paramref name="U"/> instead.</para>
        /// <para>If <paramref name="P"/> is null on entry (i.e.: the permutation matrix <paramref name="P"/> is not requested and thus not returned) any
        /// permutation performed in the triangulation is reversed before returning L so that the matrix L returned is not guranteed to be lower triangular. Instead, 
        /// the rows of L are reordered by P<sup>-1</sup>**L. In this case the simplified equation <c>ILMath.multiply(L,U) == ILMath.multiply(A)</c> holds (default). </para>
        /// <para><paramref name="U"/> is a strictly upper triangular matrix. Any permutation is performed on L and not on <paramref name="U"/>. Thus, the order of rows of 
        /// <paramref name="U"/> does not depend on <paramref name="P"/>.</para>
        /// <example><![CDATA[ 
        /// <code>
        /// // Construct a matrix A: 
        /// Array<fcomplex> A = new fcomplex[,]{
        ///    {1, 2, 3},
        ///    {4, 4, 4},
        ///    {5, 6, 7}
        /// };
        /// // A
        /// // <Double> [3x3] order: - 
        /// //(:,:) 
        /// // 1    2    3 
        /// // 4    4    4 
        /// // 5    6    7 
        /// //
        /// // define arrays serving as output arrays for U and P
        /// Array<fcomplex> U = 1, P = 1;
        /// Array<fcomplex> L = ILMath.lu(A, U, P); 
        /// 
        /// // L 
        /// // <Double> [3,3] 1...1 order:|
        /// //            1          0          0
        /// //     0,800000          1          0
        /// //     0,200000  -1,000000          1
        /// // 
        /// // U 
        /// //<Double> [3,3] 5...1,11022302462516E-15 order:|
        /// //            5          6          7
        /// //            0  -0,800000  -1,600000
        /// //            0          0   0,000000
        /// //
        /// //P
        /// //<Double> [3,3] 0...0 order:|
        /// //            0          0          1
        /// //            0          1          0
        /// //            1          0          0
        /// </code>]]>
        /// L and U are triangular matrices. P is returned as well. The result of L**U gives:
        /// <code>
        /// multiply(L,U)
        ///            5          6          7
        ///            4          4          4
        ///            1          2          3
        /// </code>
        /// This result matches a reordered version of A, which can be computed by help of P:
        /// multiply(P,A)
        ///            5          6          7
        ///            4          4          4
        ///            1          2          3
        /// </example>
        /// <example>
        /// // Computing the LU factorization of A without returning the permutation matrix P: 
        /// <![CDATA[<code>
        /// L = lu(A,U); 
        /// 
        /// // L 
        /// // <Double> [3,3] 1...1 order:|
        /// //   0,200000  -1,000000          1
        /// //   0,800000          1          0
        /// //          1          0          0
        /// // 
        /// // U 
        /// //<Double> [3,3] 5...1,11022302462516E-15 order:|
        /// //            5          6          7
        /// //            0  -0,800000  -1,600000
        /// //            0          0   0,000000
        /// //
        /// </code>]]>
        /// This time, while U did not change, L is not triangular but a reordered version of a lower triangular matrix. However, 
        /// multiplying L ** U gives A directly: 
        /// <![CDATA[<code>
        ///  
        /// multiply(L,U)
        ///            1          2          3
        ///            4          4          4
        ///            5          6          7
        /// </code>]]>
        /// </example>
        /// <para>lu uses the Lapack function ?getrf.</para>
        /// <para>The input matrix <paramref name="A"/> is not altered.</para>
        /// <para>Either of <paramref name="U"/> and/or <paramref name="P"/> can be null on entry. If <paramref name="U"/> 
        /// is null the upper triangular matrix will be computed but not returned. Instead, the compressed form of L 
        /// and <paramref name="U"/> is returned as created by the LAPACK function ?getrf:  
        /// </para>
        /// <para><code>
        /// :'''''''|
        /// |1 \    |
        /// | 1 \ R |
        /// |  1 \  |
        /// | L 1 \ |
        /// |    1 \|
        /// '''''''''
        /// </code></para>
        /// <para>If <paramref name="P"/> is null on entry and <paramref name="U"/> is not null the permutation matrix is not computed but rows of L are reordered accordingly in order to fullfill the equation 'A = L**U'.</para>
        /// </remarks>
        /// <seealso cref="chol(InArray{fcomplex}, bool)"/>
        /// <seealso cref="ILNumerics.Core.Native.ILapack"/>
        /// <exception cref="ArgumentException"> if input <paramref name="A"/> is not a matrix.</exception>
        
        public static Array<fcomplex> lu(InArray<fcomplex> A, OutArray<fcomplex> U
                                                            , OutArray<fcomplex> P = null) {
            using (Scope.Enter()) {

                Array<fcomplex> _A = A; 

                if (!_A.IsMatrix)
                    throw new ArgumentException("lu() is defined for matrices only.");
                long m = _A.Size[0], n = _A.Size[1], minMN = (m < n) ? m : n;

                int info = 0;
                Array<fcomplex> L = _A.C;

                MemoryHandle pivH = null, outPermH = null;
                try {
                    unsafe {
                        pivH = New<int>(Math.Max(1, minMN));
                        int* pivInd = (int*)pivH.Pointer;

                        Lapack.cgetrf((int)m, (int)n, (fcomplex*)L.GetHostPointerForWrite(StorageOrders.ColumnMajor), (int)m, pivInd, ref info);
                        if (info < 0) {
                            throw new ArgumentException($"lu(): invalid parameter. Error code returned from LAPACK.cgetrf: {info}.");
                        } else {
                            // completed successfuly 
                            if (!Object.Equals(U, null)) {
                                lock (U.SynchObj)
                                    U.a = copyUpperTriangle<fcomplex>(L, minMN, n);
                                outPermH = New<int>(m);
                                var outPerm = (int*)outPermH.Pointer;
                                if (!Object.Equals(P, null)) {
                                    perm2indicesForward(pivInd, outPerm, (uint)minMN, (uint)m);
                                    L.a = copyLowerTriangle<fcomplex>(L, m, minMN, (fcomplex)1);
                                    lock (P.SynchObj) {
                                        P.a = zeros<fcomplex>(m, m); // diag(ones<fcomplex>(m)); // ! sic: P is [m x m] 

                                        // construct permutation matrix P 
                                        for (uint r = 0; r < m; r++) {
                                            P[r, outPerm[r]] = (fcomplex)1.0;
                                        }
                                    }
                                } else {
                                    perm2indicesBackward(pivInd, outPerm, (uint)minMN, (uint)m);
                                    L.a = copyLowerTrianglePerm<fcomplex>(L, (uint)m, (uint)minMN, (fcomplex)1, outPerm);
                                }
                            }
                        }
                        return L;
                    }
                } finally {
                    if (pivH != null) free<int>(pivH, 0);
                    if (outPermH != null) free<int>(outPermH, 0);
                }
            }
        }
        /// <summary>
        /// LU matrix decomposition. Decompose general matrix <paramref name="A"/> into strictly upper part and lower part. 
        /// </summary>
        /// <param name="A">Input matrix. Size [m x n].</param>
        /// <returns>Triangular matrices L and U composed into a single matrix as returned from LAPACK function ?getrf. Size [m x n].</returns>
        /// <remarks><para>The matrix returned is composed out of the lower triangular matrix L with unit diagonal and the strictly upper triangular matrix U.</para>
        /// <code>
        /// :'''''''|
        /// |1 \    |
        /// | 1 \ R |
        /// |  1 \  |
        /// | L 1 \ |
        /// |    1 \|
        /// '''''''''
        /// </code>
        /// <para>This overload is mostly needed for further operations via Lapack libraries. If you need the 
        /// L and U matrices directly, consider using the overload
        /// <see cref="lu(InArray{complex}, OutArray{complex}, OutArray{complex})"/> instead.</para>
        /// <para>lu uses the Lapack function ?getrf, which is provided by a native LAPACK implementation (mostly Intel's MKL).</para>
        /// </remarks>
        /// <seealso cref="lu(InArray{complex}, OutArray{complex}, OutArray{complex})"/>
        /// <exception cref="ArgumentException"> if input A is not a matrix.</exception>
        public static Array<complex> lu(InArray<complex> A) {
            return lu(A, null, null);
        }
        /// <summary>
        /// Decompose matrix <paramref name="A"/> into uper and lower triangular matrices. Also generates pivoting permutation matrix. 
        /// </summary>
        /// <param name="A">Input matrix. Size [m x n].</param>
        /// <param name="U">[Output] Upper triangular matrix. Size [min(m,n) x n]. This can be null.</param>
        /// <param name="P">[Output, optional] Permutation matrix <paramref name="P"/>. Size [m x m]. Default: (null) do not compute the permutation matrix. L is ordered propery before the function returns.</param>
        /// <returns>Lower triangular matrix L. Size [m x min(m,n)].</returns>
        /// <remarks><para><paramref name="A"/> is decomposed into L and <paramref name="U"/> so that the equation 
        /// <c>ILMath.multiply(L,U) == ILMath.multiply(P,A)</c> holds within the range of round off errors.</para>
        /// <para>The matrix L returned is a lower triangular matrix with unit diagonal. Depending on <paramref name="P"/> the rows 
        /// of L may be reordered due to pivoting during the triangulation. If <paramref name="P"/> is not null on entry (i.e.: 
        /// the permutation matrix <paramref name="P"/> is computed and returned from <see cref="lu(InArray{complex}, OutArray{complex}, OutArray{complex})"/>) L 
        /// is truely lower triangular. In this case the equation 
        /// <c>ILMath.multiply(L,U) == ILMath.multiply(P,A)</c> holds. Note, that the result of matrix multiplying L with <paramref name="U"/> 
        /// yields a version of <paramref name="A"/> having its rows reordered. <paramref name="P"/> permutes the rows of <paramref name="A"/> accordingly. 
        /// Note further, that <paramref name="P"/> is suitable to be multiplied 
        /// to <paramref name="A"/>, while some other systems return <paramref name="P"/><sup>-1</sup> for reordering the result L**<paramref name="U"/> instead.</para>
        /// <para>If <paramref name="P"/> is null on entry (i.e.: the permutation matrix <paramref name="P"/> is not requested and thus not returned) any
        /// permutation performed in the triangulation is reversed before returning L so that the matrix L returned is not guranteed to be lower triangular. Instead, 
        /// the rows of L are reordered by P<sup>-1</sup>**L. In this case the simplified equation <c>ILMath.multiply(L,U) == ILMath.multiply(A)</c> holds (default). </para>
        /// <para><paramref name="U"/> is a strictly upper triangular matrix. Any permutation is performed on L and not on <paramref name="U"/>. Thus, the order of rows of 
        /// <paramref name="U"/> does not depend on <paramref name="P"/>.</para>
        /// <example><![CDATA[ 
        /// <code>
        /// // Construct a matrix A: 
        /// Array<complex> A = new complex[,]{
        ///    {1, 2, 3},
        ///    {4, 4, 4},
        ///    {5, 6, 7}
        /// };
        /// // A
        /// // <Double> [3x3] order: - 
        /// //(:,:) 
        /// // 1    2    3 
        /// // 4    4    4 
        /// // 5    6    7 
        /// //
        /// // define arrays serving as output arrays for U and P
        /// Array<complex> U = 1, P = 1;
        /// Array<complex> L = ILMath.lu(A, U, P); 
        /// 
        /// // L 
        /// // <Double> [3,3] 1...1 order:|
        /// //            1          0          0
        /// //     0,800000          1          0
        /// //     0,200000  -1,000000          1
        /// // 
        /// // U 
        /// //<Double> [3,3] 5...1,11022302462516E-15 order:|
        /// //            5          6          7
        /// //            0  -0,800000  -1,600000
        /// //            0          0   0,000000
        /// //
        /// //P
        /// //<Double> [3,3] 0...0 order:|
        /// //            0          0          1
        /// //            0          1          0
        /// //            1          0          0
        /// </code>]]>
        /// L and U are triangular matrices. P is returned as well. The result of L**U gives:
        /// <code>
        /// multiply(L,U)
        ///            5          6          7
        ///            4          4          4
        ///            1          2          3
        /// </code>
        /// This result matches a reordered version of A, which can be computed by help of P:
        /// multiply(P,A)
        ///            5          6          7
        ///            4          4          4
        ///            1          2          3
        /// </example>
        /// <example>
        /// // Computing the LU factorization of A without returning the permutation matrix P: 
        /// <![CDATA[<code>
        /// L = lu(A,U); 
        /// 
        /// // L 
        /// // <Double> [3,3] 1...1 order:|
        /// //   0,200000  -1,000000          1
        /// //   0,800000          1          0
        /// //          1          0          0
        /// // 
        /// // U 
        /// //<Double> [3,3] 5...1,11022302462516E-15 order:|
        /// //            5          6          7
        /// //            0  -0,800000  -1,600000
        /// //            0          0   0,000000
        /// //
        /// </code>]]>
        /// This time, while U did not change, L is not triangular but a reordered version of a lower triangular matrix. However, 
        /// multiplying L ** U gives A directly: 
        /// <![CDATA[<code>
        ///  
        /// multiply(L,U)
        ///            1          2          3
        ///            4          4          4
        ///            5          6          7
        /// </code>]]>
        /// </example>
        /// <para>lu uses the Lapack function ?getrf.</para>
        /// <para>The input matrix <paramref name="A"/> is not altered.</para>
        /// <para>Either of <paramref name="U"/> and/or <paramref name="P"/> can be null on entry. If <paramref name="U"/> 
        /// is null the upper triangular matrix will be computed but not returned. Instead, the compressed form of L 
        /// and <paramref name="U"/> is returned as created by the LAPACK function ?getrf:  
        /// </para>
        /// <para><code>
        /// :'''''''|
        /// |1 \    |
        /// | 1 \ R |
        /// |  1 \  |
        /// | L 1 \ |
        /// |    1 \|
        /// '''''''''
        /// </code></para>
        /// <para>If <paramref name="P"/> is null on entry and <paramref name="U"/> is not null the permutation matrix is not computed but rows of L are reordered accordingly in order to fullfill the equation 'A = L**U'.</para>
        /// </remarks>
        /// <seealso cref="chol(InArray{complex}, bool)"/>
        /// <seealso cref="ILNumerics.Core.Native.ILapack"/>
        /// <exception cref="ArgumentException"> if input <paramref name="A"/> is not a matrix.</exception>
        
        public static Array<complex> lu(InArray<complex> A, OutArray<complex> U
                                                            , OutArray<complex> P = null) {
            using (Scope.Enter()) {

                Array<complex> _A = A; 

                if (!_A.IsMatrix)
                    throw new ArgumentException("lu() is defined for matrices only.");
                long m = _A.Size[0], n = _A.Size[1], minMN = (m < n) ? m : n;

                int info = 0;
                Array<complex> L = _A.C;

                MemoryHandle pivH = null, outPermH = null;
                try {
                    unsafe {
                        pivH = New<int>(Math.Max(1, minMN));
                        int* pivInd = (int*)pivH.Pointer;

                        Lapack.zgetrf((int)m, (int)n, (complex*)L.GetHostPointerForWrite(StorageOrders.ColumnMajor), (int)m, pivInd, ref info);
                        if (info < 0) {
                            throw new ArgumentException($"lu(): invalid parameter. Error code returned from LAPACK.zgetrf: {info}.");
                        } else {
                            // completed successfuly 
                            if (!Object.Equals(U, null)) {
                                lock (U.SynchObj)
                                    U.a = copyUpperTriangle<complex>(L, minMN, n);
                                outPermH = New<int>(m);
                                var outPerm = (int*)outPermH.Pointer;
                                if (!Object.Equals(P, null)) {
                                    perm2indicesForward(pivInd, outPerm, (uint)minMN, (uint)m);
                                    L.a = copyLowerTriangle<complex>(L, m, minMN, (complex)1);
                                    lock (P.SynchObj) {
                                        P.a = zeros<complex>(m, m); // diag(ones<complex>(m)); // ! sic: P is [m x m] 

                                        // construct permutation matrix P 
                                        for (uint r = 0; r < m; r++) {
                                            P[r, outPerm[r]] = (complex)1.0;
                                        }
                                    }
                                } else {
                                    perm2indicesBackward(pivInd, outPerm, (uint)minMN, (uint)m);
                                    L.a = copyLowerTrianglePerm<complex>(L, (uint)m, (uint)minMN, (complex)1, outPerm);
                                }
                            }
                        }
                        return L;
                    }
                } finally {
                    if (pivH != null) free<int>(pivH, 0);
                    if (outPermH != null) free<int>(outPermH, 0);
                }
            }
        }

#endregion HYCALPER AUTO GENERATED CODE

    }
}
