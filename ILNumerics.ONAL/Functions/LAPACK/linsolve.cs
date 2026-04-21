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
        dtrtrs
    </source>
    <destination>ztrtrs</destination>
    <destination>ctrtrs</destination>
    <destination>strtrs</destination>
</type>
<type>
    <source locate="here">
        dgetrf
    </source>
    <destination>zgetrf</destination>
    <destination>cgetrf</destination>
    <destination>sgetrf</destination>
</type>
<type>
    <source locate="here">
        dgetrs
    </source>
    <destination>zgetrs</destination>
    <destination>cgetrs</destination>
    <destination>sgetrs</destination>
</type>
<type>
    <source locate="here">
        dpotrs
    </source>
    <destination>zpotrs</destination>
    <destination>cpotrs</destination>
    <destination>spotrs</destination>
</type>
<type>
    <source locate="here">
        dpotrf
    </source>
    <destination>zpotrf</destination>
    <destination>cpotrf</destination>
    <destination>spotrf</destination>
</type>
<type>
    <source locate="here">
        dgelsy
    </source>
    <destination>zgelsy</destination>
    <destination>cgelsy</destination>
    <destination>sgelsy</destination>
</type>
<type>
    <source locate="after" endmark=" ,">
        HycalpEPS
    </source>
    <destination>eps</destination>
    <destination>epsf</destination>
    <destination>epsf</destination>
</type>
</hycalper>
*/

namespace ILNumerics {
    public static partial class ILMath {

        #region HYCALPER LOOPSTART 

        /// <summary>
        /// Solves a system of linear equations, B = A x.
        /// </summary>
        /// <param name="A">Input matrix <paramref name="A"/>. Size [n, q].</param>
        /// <param name="B">Right hand side <paramref name="B"/>. Size [n, m].</param>
        /// <returns>Solution x solving the equation <c>multiply(A, x) = B</c>. Size [q, m].</returns>
        /// <remarks><para>Depending on the structure and properties of <paramref name="A"/> the equation system is solved with different approaches:
        /// <list type="bullet">
        /// <item>If <paramref name="A"/> is square (q == n) and an <b>upper or lower triangular</b> matrix, the 
        /// system is solved via backward- or forward substitution and the LAPACK function ?trtrs. 
        /// <example><code><![CDATA[Array<double> A = randn<double>(4,4); // construct 4 x 4 matrix 
        /// Array<double> B = vector<double>(1.0,2.0,3.0); 
        /// Array<double> x = linsolve(A,B);]]></code></example></item>
        /// <item><para>if <paramref name="A"/> is square, symmetric /hermitian and positive definite <paramref name="A"/> is 
        /// decomposed into a triangular equation system using cholesky factorization and solved via back-/ forward
        /// substitution.</para></item>
        /// <item>otherwise, if <paramref name="A"/> is only square it will be decomposed into upper and lower triangular matrices using 
        /// LU decomposition and the system than solved with the result.</item>
        /// <item>otherwise, if <paramref name="A"/> is [n, q] with q != n, the system is solved using <see cref="qr(InArray{double})"/> 
        /// decomposition. Note that <paramref name="A"/> can be rank deficient.</item>
        /// </list></para>
        /// <para>The internal storage order of <paramref name="A"/> and/or <paramref name="B"/> may be silently changed when this function returns. 
        /// The reason is that most functionality is performed in native LAPACK routines which require a certain storage layout (mostly 
        /// <see cref="StorageOrders.ColumnMajor"/>).</para>
        /// </remarks>
        /// <seealso cref="linsolve(InArray{double}, InArray{double}, ref MatrixProperties, bool)"/>
        /// <seealso cref="Size.StorageOrder"/>
        /// <seealso cref="lu(InArray{double})"/>
        /// <seealso cref="qr(InArray{double})"/>
        /// <seealso cref="pinv(InArray{double}, double?)"/>
        /// <seealso cref="svd(InArray{double}, OutArray{double}, bool)"/>
        public static Array<double> linsolve(InArray<double> A, InArray<double> B) {
            if (object.Equals(A, null) || object.Equals(B, null)) {
                throw new ArgumentException($"linsolve(): A and B must not be null. Found: A={(isnull(A) ? "(null)" : A.S.ToString())}, B={(isnull(B) ? "(null)" : B.S.ToString())}");
            }
            using (Scope.Enter()) {
                Array<double> _A = A, _B = B;

                MatrixProperties props = MatrixProperties.None;
                if (_A.Size[0] == _A.Size[1]) {
                    props |= MatrixProperties.Square;
                    if (istriup(_A)) {
                        props |= MatrixProperties.UpperTriangular;
                        return linsolve(_A, _B, ref props);
                    }
                    if (istrilow(_A)) {
                        props |= MatrixProperties.LowerTriangular;
                        return linsolve(_A, _B, ref props);
                    }
                    if (ishermitian(_A)) {
                        // give cholesky a try
                        props |= MatrixProperties.Hermitian;
                        props |= MatrixProperties.PositivDefinite;
                        Array<double> ret = linsolve(_A, _B, ref props, throwException: false);
                        if (!object.Equals(ret, null)) {
                            return ret;
                        } else {
                            props ^= MatrixProperties.PositivDefinite;
                        }
                    }
                }
                return linsolve(_A, _B, ref props);
            }
        }

        /// <summary>
        /// Solves a system of linear equations, B = Ax, taking hints for the best algorithm.
        /// </summary>
        /// <param name="A">Input matrix <paramref name="A"/>. Size [n, q].</param>
        /// <param name="B">Right hand side <paramref name="B"/>. Size [n, m].</param>
        /// <param name="props">[Input, Output] Known / determined matrix properties of <paramref name="A"/>.</param>
        /// <param name="throwException">[Optional] Throws an <see cref="ArgumentException"/> if <paramref name="A"/> was found to 
        /// be singular or a specific property in <paramref name="props"/> could not be confirmed. Default: true.</param>
        /// <returns>Solution x solving the equation <c>multiply(A, x) = B</c>. Size [q, m].</returns>
        /// <remarks><para>If no specific properties of <paramref name="A"/> are given in <paramref name="props"/> and 
        /// depending on the actual structure and properties of <paramref name="A"/> the equation system is solved with different approaches:
        /// <list type="bullet">
        /// <item>If <paramref name="A"/> is square (q == n) and an <b>upper or lower triangular</b> matrix, the 
        /// system is solved via backward- or forward substitution and the LAPACK function ?trtrs. 
        /// <example><code><![CDATA[Array<double> A = randn<double>(4,4); // construct 4 x 4 matrix 
        /// Array<double> B = vector<double>(1.0,2.0,3.0); 
        /// Array<double> x = linsolve(A,B);]]></code></example></item>
        /// <item><para>if <paramref name="A"/> is square, symmetric /hermitian and positive definite <paramref name="A"/> is 
        /// decomposed into a triangular equation system using cholesky factorization and solved via back-/ forward
        /// substitution.</para></item>
        /// <item>otherwise, if <paramref name="A"/> is only square it will be decomposed into upper and lower triangular matrices using 
        /// LU decomposition and the system is than solved with the triangular result.</item>
        /// <item>otherwise, if <paramref name="A"/>' is [n, q] with q != n, the system is solved using <see cref="qr(InArray{double})"/> 
        /// decomposition. Note that <paramref name="A"/> can be rank deficient.</item>
        /// </list></para>
        /// <para>Specifying known properties of <paramref name="A"/> in <paramref name="props"/> may saves some time in 
        /// determining these properties. If, for example, <paramref name="A"/> is known to be positive 
        /// definite, providing <see cref="MatrixProperties.PositivDefinite"/> allows the algorithm to 
        /// perform cholesky factorization with <paramref name="A"/>. However, 
        /// <see cref="linsolve(InArray{double}, InArray{double}, ref MatrixProperties, bool)"/> determines advantageous 
        /// properties automatically if required.</para>
        /// <para>After returning the <paramref name="props"/> structure can be inspected fo the matrix properties of 
        /// <paramref name="A"/> found during the computations. Any bits changed in <paramref name="props"/> by the 
        /// function reflect the path taken within <see cref="linsolve(InArray{double}, InArray{double}, ref MatrixProperties, bool)"/>. 
        /// However, <see cref="linsolve(InArray{double}, InArray{double}, ref MatrixProperties, bool)"/> does not determine <i>all</i> 
        /// properties of <paramref name="A"/> automatically. If, let's say: <paramref name="A"/> is found to be singular, 
        /// the function returns (or throws an exception) without determining further properties of A. In order to query specific 
        /// properties of <paramref name="A"/> other functionality may be more appropriate.</para>
        /// <para>If <paramref name="throwException"/> is false inspecting the <paramref name="props"/> structure on return is 
        /// required to verify that a valid solution has been computed. If <paramref name="A"/> was found to be singular and 
        /// <paramref name="throwException"/> is false then <paramref name="props"/> will have the <see cref="MatrixProperties.Singular"/>
        /// flag set. If the computation was performed by QR decomposition for a non-square matrix <paramref name="A"/> then 
        /// the <see cref="MatrixProperties.RankDeficient"/> flag may be set.</para>
        /// <para>The internal storage order of <paramref name="A"/> and/or <paramref name="B"/> may be silently changed 
        /// by this function. The reason is that most functionality is performed in native LAPACK routines which 
        /// require a certain storage layout (mostly <see cref="StorageOrders.ColumnMajor"/>). Be prepared that 
        /// both arrays may point to different memory regions for element storage afterwards! This does not, however, 
        /// affect the handling of the array with common high level functionality (subarrays, element access etc.).</para>
        /// </remarks>
        /// <seealso cref="linsolve(InArray{double}, InArray{double}, ref MatrixProperties, bool)"/>
        /// <seealso cref="Size.StorageOrder"/>
        /// <seealso cref="lu(InArray{double})"/>
        /// <seealso cref="qr(InArray{double})"/>
        /// <seealso cref="pinv(InArray{double}, double?)"/>
        /// <seealso cref="svd(InArray{double}, OutArray{double}, bool)"/>
        public static Array<double> linsolve(InArray<double> A, InArray<double> B, ref MatrixProperties props, bool throwException = true) {
            
            if (object.Equals(A, null))
                throw new ArgumentException("linsolve(): A must not be null!");
            if (object.Equals(B, null))
                throw new ArgumentException("linsolve(): B must not be null!");
            using (Scope.Enter()) {

                Array<double> _A = A, _B = B;
                if (_A.Size[0] != _B.Size[0])
                    throw new ArgumentException("linsolve(): number of rows of A must match the number of rows of _B!");

                long N = _A.Size[0], M = _B.S[1], Q = _A.S[1]; 
                if (N == 0) {
                    throw new ArgumentException($"linsolve() A and B must have at least one row each. Found: A.Size={_A.S}, B.Size={_B.S}."); 
                }

                if (_A.IsEmpty || _B.IsEmpty) {
                    System.Diagnostics.Debug.Assert(M == 0 || Q == 0); 
                    return empty<double>(M, Q);
                }
                int info = 0;
                Array<double> ret = empty<double>(0);
                if (N == _A.Size[1]) { 
                    props |= MatrixProperties.Square;
                    if ((props & MatrixProperties.LowerTriangular) != 0) {
                        ret.a = linsolveTriLow(_A, _B, ref info);
                        if (info > 0) {
                            props |= MatrixProperties.Singular;
                            if (throwException) {
                                throw new ArgumentException($"linsolve(): matrix A was found to be singular. Inversion from lower triangular form failed."); 
                            }
                        }
                        return ret;
                    }
                    if ((props & MatrixProperties.UpperTriangular) != 0) {
                        ret.a = linsolveTriUp(_A, _B, ref info);
                        if (info > 0) {
                            props |= MatrixProperties.Singular;
                            if (throwException) {
                                throw new ArgumentException($"linsolve(): matrix A was found to be singular. Inversion from upper triangular form failed.");
                            }
                        }
                        return ret;
                    }
                    unsafe {
                        if ((props & MatrixProperties.Hermitian) != 0) {
                            Array<double> cholFact = _A.Storage.copyUpperTriangle(N);
                            Lapack.dpotrf('U', (int)N, (double*)cholFact.GetHostPointerForWrite(), (int)N, ref info);
                            if (info > 0) {
                                props ^= MatrixProperties.Hermitian;
                                if (throwException) {
                                    throw new ArgumentException($"linsolve(): matrix A was specified to be symmetric / hermitian. But this turned out to be not true. Cholesky factorization (dpotrf) failed with info: {info}.");
                                }
                                // proceed with LU below
                            } else {
                                // solve 
                                ret.a = _B.C;

                                Lapack.dpotrs('U', (int)N, (int)_B.Size[1], (double*)cholFact.GetHostPointerForWrite(), (int)N, (double*)ret.GetHostPointerForWrite(StorageOrders.ColumnMajor), (int)N, ref info);
                                return ret;
                            }
                        }

                        // attempt complete (expensive) LU factorization 
                        Array<double> L = _A.C;

                        Array<int> pivIND = empty<int>(N, StorageOrders.ColumnMajor);
                        var pivInd = (int*)pivIND.GetHostPointerForWrite();

                        var lArr = (double*)L.GetHostPointerForWrite(StorageOrders.ColumnMajor);

                        Lapack.dgetrf((int)N, (int)N, lArr, (int)N, pivInd, ref info);

                        if (info > 0)
                            props |= MatrixProperties.Singular;

                        ret.a = _B.C;

                        Lapack.dgetrs('N', (int)N, (int)_B.Size[1], lArr, (int)N, pivInd, (double*)ret.GetHostPointerForWrite(StorageOrders.ColumnMajor), (int)N, ref info);
                        GC.KeepAlive(pivIND); 
                        GC.KeepAlive(L);

                        if (info < 0) {
                            throw new ArgumentException($"linsolve(): error solving in lapack dgetrs. Info: {info}.");
                        }
//#if DEBUG
//                        var retR = (RetArray<double>)ret;
//                        System.Diagnostics.Debug.WriteLine($"({System.Threading.Thread.CurrentThread.ManagedThreadId}) linsolve returns: {retR.Storage.ShortInfo(includeIDs: true)} = {string.Join("\n", retR.Storage.ToString(1000,1000000,StorageOrders.ColumnMajor))} ");
//                        return retR; 
//#else
                        return ret;
//#endif
                    }
                } else {
                    // under- / overdetermined system
                    int rank = 0, minNQ = (int)((N < Q) ? N : Q), maxNQ = (int)((N > Q) ? N : Q);

                    Array<double> tmpA = _A.C;

                    if (N < Q) {
                        ret.a = zeros<double>(Q, M, StorageOrders.ColumnMajor);
                        ret[r(0, N - 1), full] = _B; // this will be detached from _B

                    } else {
                        ret.a = _B;
                    }
                    Array<int> JPVT = zeros<int>(Q, 1, StorageOrders.ColumnMajor);
                    unsafe {
                        Lapack.dgelsy((int)N, (int)Q, (int)_B.Size[1], (double*)tmpA.GetHostPointerForWrite(StorageOrders.ColumnMajor), (int)N, (double*)ret.GetHostPointerForWrite(StorageOrders.ColumnMajor),
                                        maxNQ, (int*)JPVT.GetHostPointerForWrite(), /*!HC:HycalpEPS*/ eps, ref rank, ref info);
                    }
                    if (Q < N) {
                        ret.a = ret[r(0, Q - 1), full];
                    }
                    if (rank < minNQ) {
                        props |= MatrixProperties.RankDeficient;
                    }
                    return ret;
                }
            }
        }

        /// <summary>
        /// Solve system of linear equations A*x = B, with <paramref name="A"/> being a square, upper triangular matrix.
        /// </summary>
        /// <param name="A">Input matrix. Size [n,n]. Upper triangular. No check is made for that!</param>
        /// <param name="B">Right hand side vector /matrix. Size [n,m].</param>
        /// <param name="singularityDetect">[Output] This value gives the row of <paramref name="A"/>, 
        /// where a singularity has been detected (if any). If <paramref name="A"/> is not singular, this will return a negative value.</param>
        /// <returns>Solution x solving <paramref name="A"/> * x = <paramref name="B"/>. Size [n,m].</returns>
        /// <remarks><para>The solution is determined via backward substitution by using a native LAPACK module.</para>
        /// <para>Make sure, <paramref name="A"/> and <paramref name="B"/> are of correct size, since no checks are made for that!</para>
        /// <para>This function is used by <see cref="linsolve(InArray{double}, InArray{double}, ref MatrixProperties, bool)"/>. It can speed 
        /// up the computation for known, matching matrices, since it saves some checks on them which are otherwise automatically 
        /// performed by <see cref="linsolve(InArray{double}, InArray{double}, ref MatrixProperties, bool)"/>.</para>
        /// <para>Any elements of <paramref name="A"/> located below the main diagonal will be ignored.</para>
        /// <para>If <paramref name="A"/> is singular the array returned contains <see cref="double.NaN"/> values.</para></remarks>
        public static Array<double> linsolveTriUp(InArray<double> A, InArray<double> B, ref int singularityDetect) {
            return linsolveTriUpLow(A, B, ref singularityDetect, true);
        }

        /// <summary>
        /// Solve system of linear equations A*x = B, with <paramref name="A"/> being a square, lower triangular matrix.
        /// </summary>
        /// <param name="A">Input matrix. Size [n,n]. Lower triangular. No check is made for that!</param>
        /// <param name="B">Right hand side vector /matrix. Size [n,m].</param>
        /// <param name="singularityDetect">[Output] This value gives the row of <paramref name="A"/>, 
        /// where a singularity has been detected (if any). If <paramref name="A"/> is not singular, this will return a negative value.</param>
        /// <returns>Solution x solving <paramref name="A"/> * x = <paramref name="B"/>. Size [n,m].</returns>
        /// <remarks><para>The solution is determined via forward substitution by using a native LAPACK module.</para>
        /// <para>Make sure, <paramref name="A"/> and <paramref name="B"/> are of correct size, since no checks are made for that!</para>
        /// <para>This function is used by <see cref="linsolve(InArray{double}, InArray{double}, ref MatrixProperties, bool)"/>. It can speed 
        /// up the computation for matrices of known, matching properties since it saves some checks on them which are otherwise automatically 
        /// performed by <see cref="linsolve(InArray{double}, InArray{double}, ref MatrixProperties, bool)"/>.</para>
        /// <para>Any elements of <paramref name="A"/> located above the main diagonal will be ignored.</para>
        /// <para>If <paramref name="A"/> is singular the array returned contains <see cref="double.NaN"/> values.</para></remarks>
        public static Array< double> linsolveTriLow(InArray< double> A, InArray< double> B, ref int singularityDetect) {
            return linsolveTriUpLow(A, B, ref singularityDetect, false); 
        }

        private static Array<double> linsolveTriUpLow(InArray<double> A, InArray<double> B, ref int singularityDetect, bool upper) {
            System.Diagnostics.Debug.Assert(B.Size[1] >= 0);
            System.Diagnostics.Debug.Assert(B.Size[0] == A.Size[1]);
            System.Diagnostics.Debug.Assert(A.Size[0] == A.Size[1]);

            using (Scope.Enter()) {

                Array<double> _A = A, _B = B;
                singularityDetect = -1;
                long n = _A.Size[0];
                long m = _B.Size[1];
                int info = 0;
                Array<double> ret = _B.C;

                unsafe {
                    double* retArr = (double*)ret.GetHostPointerForWrite(StorageOrders.ColumnMajor);
                    // solve using Lapack

                    Lapack.dtrtrs(upper ? 'U' : 'L', 'N', 'N', (int)_A.Size[0], (int)_B.Size[1], (double*)_A.GetHostPointerForRead(StorageOrders.ColumnMajor),
                                 (int)_A.Size[0], (double*)ret.GetHostPointerForWrite(), (int)_B.Size[0], ref info);
                    GC.KeepAlive(_A); 
                    if (info < 0)
                        throw new ArgumentException("linsolveTriUp(): error in LAPACK function dtrtrs: " + (-info));
                    if (info > 0) {
                        singularityDetect = info - 1;
                        for (m = 0; m < ret.Size[1]; m++) {
                            ret[r(singularityDetect, end), m] = double.NaN;
                        }
                    } else {
                        singularityDetect = -1;
                    }
                    return ret;
                }
            }
        }

#endregion HYCALPER LOOPEND 
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 

        /// <summary>
        /// Solves a system of linear equations, B = A x.
        /// </summary>
        /// <param name="A">Input matrix <paramref name="A"/>. Size [n, q].</param>
        /// <param name="B">Right hand side <paramref name="B"/>. Size [n, m].</param>
        /// <returns>Solution x solving the equation <c>multiply(A, x) = B</c>. Size [q, m].</returns>
        /// <remarks><para>Depending on the structure and properties of <paramref name="A"/> the equation system is solved with different approaches:
        /// <list type="bullet">
        /// <item>If <paramref name="A"/> is square (q == n) and an <b>upper or lower triangular</b> matrix, the 
        /// system is solved via backward- or forward substitution and the LAPACK function ?trtrs. 
        /// <example><code><![CDATA[Array<float> A = randn<float>(4,4); // construct 4 x 4 matrix 
        /// Array<float> B = vector<float>(1.0,2.0,3.0); 
        /// Array<float> x = linsolve(A,B);]]></code></example></item>
        /// <item><para>if <paramref name="A"/> is square, symmetric /hermitian and positive definite <paramref name="A"/> is 
        /// decomposed into a triangular equation system using cholesky factorization and solved via back-/ forward
        /// substitution.</para></item>
        /// <item>otherwise, if <paramref name="A"/> is only square it will be decomposed into upper and lower triangular matrices using 
        /// LU decomposition and the system than solved with the result.</item>
        /// <item>otherwise, if <paramref name="A"/> is [n, q] with q != n, the system is solved using <see cref="qr(InArray{float})"/> 
        /// decomposition. Note that <paramref name="A"/> can be rank deficient.</item>
        /// </list></para>
        /// <para>The internal storage order of <paramref name="A"/> and/or <paramref name="B"/> may be silently changed when this function returns. 
        /// The reason is that most functionality is performed in native LAPACK routines which require a certain storage layout (mostly 
        /// <see cref="StorageOrders.ColumnMajor"/>).</para>
        /// </remarks>
        /// <seealso cref="linsolve(InArray{float}, InArray{float}, ref MatrixProperties, bool)"/>
        /// <seealso cref="Size.StorageOrder"/>
        /// <seealso cref="lu(InArray{float})"/>
        /// <seealso cref="qr(InArray{float})"/>
        /// <seealso cref="pinv(InArray{float}, float?)"/>
        /// <seealso cref="svd(InArray{float}, OutArray{float}, bool)"/>
        public static Array<float> linsolve(InArray<float> A, InArray<float> B) {
            if (object.Equals(A, null) || object.Equals(B, null)) {
                throw new ArgumentException($"linsolve(): A and B must not be null. Found: A={(isnull(A) ? "(null)" : A.S.ToString())}, B={(isnull(B) ? "(null)" : B.S.ToString())}");
            }
            using (Scope.Enter()) {
                Array<float> _A = A, _B = B;

                MatrixProperties props = MatrixProperties.None;
                if (_A.Size[0] == _A.Size[1]) {
                    props |= MatrixProperties.Square;
                    if (istriup(_A)) {
                        props |= MatrixProperties.UpperTriangular;
                        return linsolve(_A, _B, ref props);
                    }
                    if (istrilow(_A)) {
                        props |= MatrixProperties.LowerTriangular;
                        return linsolve(_A, _B, ref props);
                    }
                    if (ishermitian(_A)) {
                        // give cholesky a try
                        props |= MatrixProperties.Hermitian;
                        props |= MatrixProperties.PositivDefinite;
                        Array<float> ret = linsolve(_A, _B, ref props, throwException: false);
                        if (!object.Equals(ret, null)) {
                            return ret;
                        } else {
                            props ^= MatrixProperties.PositivDefinite;
                        }
                    }
                }
                return linsolve(_A, _B, ref props);
            }
        }

        /// <summary>
        /// Solves a system of linear equations, B = Ax, taking hints for the best algorithm.
        /// </summary>
        /// <param name="A">Input matrix <paramref name="A"/>. Size [n, q].</param>
        /// <param name="B">Right hand side <paramref name="B"/>. Size [n, m].</param>
        /// <param name="props">[Input, Output] Known / determined matrix properties of <paramref name="A"/>.</param>
        /// <param name="throwException">[Optional] Throws an <see cref="ArgumentException"/> if <paramref name="A"/> was found to 
        /// be singular or a specific property in <paramref name="props"/> could not be confirmed. Default: true.</param>
        /// <returns>Solution x solving the equation <c>multiply(A, x) = B</c>. Size [q, m].</returns>
        /// <remarks><para>If no specific properties of <paramref name="A"/> are given in <paramref name="props"/> and 
        /// depending on the actual structure and properties of <paramref name="A"/> the equation system is solved with different approaches:
        /// <list type="bullet">
        /// <item>If <paramref name="A"/> is square (q == n) and an <b>upper or lower triangular</b> matrix, the 
        /// system is solved via backward- or forward substitution and the LAPACK function ?trtrs. 
        /// <example><code><![CDATA[Array<float> A = randn<float>(4,4); // construct 4 x 4 matrix 
        /// Array<float> B = vector<float>(1.0,2.0,3.0); 
        /// Array<float> x = linsolve(A,B);]]></code></example></item>
        /// <item><para>if <paramref name="A"/> is square, symmetric /hermitian and positive definite <paramref name="A"/> is 
        /// decomposed into a triangular equation system using cholesky factorization and solved via back-/ forward
        /// substitution.</para></item>
        /// <item>otherwise, if <paramref name="A"/> is only square it will be decomposed into upper and lower triangular matrices using 
        /// LU decomposition and the system is than solved with the triangular result.</item>
        /// <item>otherwise, if <paramref name="A"/>' is [n, q] with q != n, the system is solved using <see cref="qr(InArray{float})"/> 
        /// decomposition. Note that <paramref name="A"/> can be rank deficient.</item>
        /// </list></para>
        /// <para>Specifying known properties of <paramref name="A"/> in <paramref name="props"/> may saves some time in 
        /// determining these properties. If, for example, <paramref name="A"/> is known to be positive 
        /// definite, providing <see cref="MatrixProperties.PositivDefinite"/> allows the algorithm to 
        /// perform cholesky factorization with <paramref name="A"/>. However, 
        /// <see cref="linsolve(InArray{float}, InArray{float}, ref MatrixProperties, bool)"/> determines advantageous 
        /// properties automatically if required.</para>
        /// <para>After returning the <paramref name="props"/> structure can be inspected fo the matrix properties of 
        /// <paramref name="A"/> found during the computations. Any bits changed in <paramref name="props"/> by the 
        /// function reflect the path taken within <see cref="linsolve(InArray{float}, InArray{float}, ref MatrixProperties, bool)"/>. 
        /// However, <see cref="linsolve(InArray{float}, InArray{float}, ref MatrixProperties, bool)"/> does not determine <i>all</i> 
        /// properties of <paramref name="A"/> automatically. If, let's say: <paramref name="A"/> is found to be singular, 
        /// the function returns (or throws an exception) without determining further properties of A. In order to query specific 
        /// properties of <paramref name="A"/> other functionality may be more appropriate.</para>
        /// <para>If <paramref name="throwException"/> is false inspecting the <paramref name="props"/> structure on return is 
        /// required to verify that a valid solution has been computed. If <paramref name="A"/> was found to be singular and 
        /// <paramref name="throwException"/> is false then <paramref name="props"/> will have the <see cref="MatrixProperties.Singular"/>
        /// flag set. If the computation was performed by QR decomposition for a non-square matrix <paramref name="A"/> then 
        /// the <see cref="MatrixProperties.RankDeficient"/> flag may be set.</para>
        /// <para>The internal storage order of <paramref name="A"/> and/or <paramref name="B"/> may be silently changed 
        /// by this function. The reason is that most functionality is performed in native LAPACK routines which 
        /// require a certain storage layout (mostly <see cref="StorageOrders.ColumnMajor"/>). Be prepared that 
        /// both arrays may point to different memory regions for element storage afterwards! This does not, however, 
        /// affect the handling of the array with common high level functionality (subarrays, element access etc.).</para>
        /// </remarks>
        /// <seealso cref="linsolve(InArray{float}, InArray{float}, ref MatrixProperties, bool)"/>
        /// <seealso cref="Size.StorageOrder"/>
        /// <seealso cref="lu(InArray{float})"/>
        /// <seealso cref="qr(InArray{float})"/>
        /// <seealso cref="pinv(InArray{float}, float?)"/>
        /// <seealso cref="svd(InArray{float}, OutArray{float}, bool)"/>
        public static Array<float> linsolve(InArray<float> A, InArray<float> B, ref MatrixProperties props, bool throwException = true) {
            
            if (object.Equals(A, null))
                throw new ArgumentException("linsolve(): A must not be null!");
            if (object.Equals(B, null))
                throw new ArgumentException("linsolve(): B must not be null!");
            using (Scope.Enter()) {

                Array<float> _A = A, _B = B;
                if (_A.Size[0] != _B.Size[0])
                    throw new ArgumentException("linsolve(): number of rows of A must match the number of rows of _B!");

                long N = _A.Size[0], M = _B.S[1], Q = _A.S[1]; 
                if (N == 0) {
                    throw new ArgumentException($"linsolve() A and B must have at least one row each. Found: A.Size={_A.S}, B.Size={_B.S}."); 
                }

                if (_A.IsEmpty || _B.IsEmpty) {
                    System.Diagnostics.Debug.Assert(M == 0 || Q == 0); 
                    return empty<float>(M, Q);
                }
                int info = 0;
                Array<float> ret = empty<float>(0);
                if (N == _A.Size[1]) { 
                    props |= MatrixProperties.Square;
                    if ((props & MatrixProperties.LowerTriangular) != 0) {
                        ret.a = linsolveTriLow(_A, _B, ref info);
                        if (info > 0) {
                            props |= MatrixProperties.Singular;
                            if (throwException) {
                                throw new ArgumentException($"linsolve(): matrix A was found to be singular. Inversion from lower triangular form failed."); 
                            }
                        }
                        return ret;
                    }
                    if ((props & MatrixProperties.UpperTriangular) != 0) {
                        ret.a = linsolveTriUp(_A, _B, ref info);
                        if (info > 0) {
                            props |= MatrixProperties.Singular;
                            if (throwException) {
                                throw new ArgumentException($"linsolve(): matrix A was found to be singular. Inversion from upper triangular form failed.");
                            }
                        }
                        return ret;
                    }
                    unsafe {
                        if ((props & MatrixProperties.Hermitian) != 0) {
                            Array<float> cholFact = _A.Storage.copyUpperTriangle(N);
                            Lapack.spotrf('U', (int)N, (float*)cholFact.GetHostPointerForWrite(), (int)N, ref info);
                            if (info > 0) {
                                props ^= MatrixProperties.Hermitian;
                                if (throwException) {
                                    throw new ArgumentException($"linsolve(): matrix A was specified to be symmetric / hermitian. But this turned out to be not true. Cholesky factorization (spotrf) failed with info: {info}.");
                                }
                                // proceed with LU below
                            } else {
                                // solve 
                                ret.a = _B.C;

                                Lapack.spotrs('U', (int)N, (int)_B.Size[1], (float*)cholFact.GetHostPointerForWrite(), (int)N, (float*)ret.GetHostPointerForWrite(StorageOrders.ColumnMajor), (int)N, ref info);
                                return ret;
                            }
                        }

                        // attempt complete (expensive) LU factorization 
                        Array<float> L = _A.C;

                        Array<int> pivIND = empty<int>(N, StorageOrders.ColumnMajor);
                        var pivInd = (int*)pivIND.GetHostPointerForWrite();

                        var lArr = (float*)L.GetHostPointerForWrite(StorageOrders.ColumnMajor);

                        Lapack.sgetrf((int)N, (int)N, lArr, (int)N, pivInd, ref info);

                        if (info > 0)
                            props |= MatrixProperties.Singular;

                        ret.a = _B.C;

                        Lapack.sgetrs('N', (int)N, (int)_B.Size[1], lArr, (int)N, pivInd, (float*)ret.GetHostPointerForWrite(StorageOrders.ColumnMajor), (int)N, ref info);
                        GC.KeepAlive(pivIND); 
                        GC.KeepAlive(L);

                        if (info < 0) {
                            throw new ArgumentException($"linsolve(): error solving in lapack sgetrs. Info: {info}.");
                        }
//#if DEBUG
//                        var retR = (RetArray<float>)ret;
//                        System.Diagnostics.Debug.WriteLine($"({System.Threading.Thread.CurrentThread.ManagedThreadId}) linsolve returns: {retR.Storage.ShortInfo(includeIDs: true)} = {string.Join("\n", retR.Storage.ToString(1000,1000000,StorageOrders.ColumnMajor))} ");
//                        return retR; 
//#else
                        return ret;
//#endif
                    }
                } else {
                    // under- / overdetermined system
                    int rank = 0, minNQ = (int)((N < Q) ? N : Q), maxNQ = (int)((N > Q) ? N : Q);

                    Array<float> tmpA = _A.C;

                    if (N < Q) {
                        ret.a = zeros<float>(Q, M, StorageOrders.ColumnMajor);
                        ret[r(0, N - 1), full] = _B; // this will be detached from _B

                    } else {
                        ret.a = _B;
                    }
                    Array<int> JPVT = zeros<int>(Q, 1, StorageOrders.ColumnMajor);
                    unsafe {
                        Lapack.sgelsy((int)N, (int)Q, (int)_B.Size[1], (float*)tmpA.GetHostPointerForWrite(StorageOrders.ColumnMajor), (int)N, (float*)ret.GetHostPointerForWrite(StorageOrders.ColumnMajor),
                                        maxNQ, (int*)JPVT.GetHostPointerForWrite(),  epsf, ref rank, ref info);
                    }
                    if (Q < N) {
                        ret.a = ret[r(0, Q - 1), full];
                    }
                    if (rank < minNQ) {
                        props |= MatrixProperties.RankDeficient;
                    }
                    return ret;
                }
            }
        }

        /// <summary>
        /// Solve system of linear equations A*x = B, with <paramref name="A"/> being a square, upper triangular matrix.
        /// </summary>
        /// <param name="A">Input matrix. Size [n,n]. Upper triangular. No check is made for that!</param>
        /// <param name="B">Right hand side vector /matrix. Size [n,m].</param>
        /// <param name="singularityDetect">[Output] This value gives the row of <paramref name="A"/>, 
        /// where a singularity has been detected (if any). If <paramref name="A"/> is not singular, this will return a negative value.</param>
        /// <returns>Solution x solving <paramref name="A"/> * x = <paramref name="B"/>. Size [n,m].</returns>
        /// <remarks><para>The solution is determined via backward substitution by using a native LAPACK module.</para>
        /// <para>Make sure, <paramref name="A"/> and <paramref name="B"/> are of correct size, since no checks are made for that!</para>
        /// <para>This function is used by <see cref="linsolve(InArray{float}, InArray{float}, ref MatrixProperties, bool)"/>. It can speed 
        /// up the computation for known, matching matrices, since it saves some checks on them which are otherwise automatically 
        /// performed by <see cref="linsolve(InArray{float}, InArray{float}, ref MatrixProperties, bool)"/>.</para>
        /// <para>Any elements of <paramref name="A"/> located below the main diagonal will be ignored.</para>
        /// <para>If <paramref name="A"/> is singular the array returned contains <see cref="float.NaN"/> values.</para></remarks>
        public static Array<float> linsolveTriUp(InArray<float> A, InArray<float> B, ref int singularityDetect) {
            return linsolveTriUpLow(A, B, ref singularityDetect, true);
        }

        /// <summary>
        /// Solve system of linear equations A*x = B, with <paramref name="A"/> being a square, lower triangular matrix.
        /// </summary>
        /// <param name="A">Input matrix. Size [n,n]. Lower triangular. No check is made for that!</param>
        /// <param name="B">Right hand side vector /matrix. Size [n,m].</param>
        /// <param name="singularityDetect">[Output] This value gives the row of <paramref name="A"/>, 
        /// where a singularity has been detected (if any). If <paramref name="A"/> is not singular, this will return a negative value.</param>
        /// <returns>Solution x solving <paramref name="A"/> * x = <paramref name="B"/>. Size [n,m].</returns>
        /// <remarks><para>The solution is determined via forward substitution by using a native LAPACK module.</para>
        /// <para>Make sure, <paramref name="A"/> and <paramref name="B"/> are of correct size, since no checks are made for that!</para>
        /// <para>This function is used by <see cref="linsolve(InArray{float}, InArray{float}, ref MatrixProperties, bool)"/>. It can speed 
        /// up the computation for matrices of known, matching properties since it saves some checks on them which are otherwise automatically 
        /// performed by <see cref="linsolve(InArray{float}, InArray{float}, ref MatrixProperties, bool)"/>.</para>
        /// <para>Any elements of <paramref name="A"/> located above the main diagonal will be ignored.</para>
        /// <para>If <paramref name="A"/> is singular the array returned contains <see cref="float.NaN"/> values.</para></remarks>
        public static Array< float> linsolveTriLow(InArray< float> A, InArray< float> B, ref int singularityDetect) {
            return linsolveTriUpLow(A, B, ref singularityDetect, false); 
        }

        private static Array<float> linsolveTriUpLow(InArray<float> A, InArray<float> B, ref int singularityDetect, bool upper) {
            System.Diagnostics.Debug.Assert(B.Size[1] >= 0);
            System.Diagnostics.Debug.Assert(B.Size[0] == A.Size[1]);
            System.Diagnostics.Debug.Assert(A.Size[0] == A.Size[1]);

            using (Scope.Enter()) {

                Array<float> _A = A, _B = B;
                singularityDetect = -1;
                long n = _A.Size[0];
                long m = _B.Size[1];
                int info = 0;
                Array<float> ret = _B.C;

                unsafe {
                    float* retArr = (float*)ret.GetHostPointerForWrite(StorageOrders.ColumnMajor);
                    // solve using Lapack

                    Lapack.strtrs(upper ? 'U' : 'L', 'N', 'N', (int)_A.Size[0], (int)_B.Size[1], (float*)_A.GetHostPointerForRead(StorageOrders.ColumnMajor),
                                 (int)_A.Size[0], (float*)ret.GetHostPointerForWrite(), (int)_B.Size[0], ref info);
                    GC.KeepAlive(_A); 
                    if (info < 0)
                        throw new ArgumentException("linsolveTriUp(): error in LAPACK function strtrs: " + (-info));
                    if (info > 0) {
                        singularityDetect = info - 1;
                        for (m = 0; m < ret.Size[1]; m++) {
                            ret[r(singularityDetect, end), m] = float.NaN;
                        }
                    } else {
                        singularityDetect = -1;
                    }
                    return ret;
                }
            }
        }


        /// <summary>
        /// Solves a system of linear equations, B = A x.
        /// </summary>
        /// <param name="A">Input matrix <paramref name="A"/>. Size [n, q].</param>
        /// <param name="B">Right hand side <paramref name="B"/>. Size [n, m].</param>
        /// <returns>Solution x solving the equation <c>multiply(A, x) = B</c>. Size [q, m].</returns>
        /// <remarks><para>Depending on the structure and properties of <paramref name="A"/> the equation system is solved with different approaches:
        /// <list type="bullet">
        /// <item>If <paramref name="A"/> is square (q == n) and an <b>upper or lower triangular</b> matrix, the 
        /// system is solved via backward- or forward substitution and the LAPACK function ?trtrs. 
        /// <example><code><![CDATA[Array<fcomplex> A = randn<fcomplex>(4,4); // construct 4 x 4 matrix 
        /// Array<fcomplex> B = vector<fcomplex>(1.0,2.0,3.0); 
        /// Array<fcomplex> x = linsolve(A,B);]]></code></example></item>
        /// <item><para>if <paramref name="A"/> is square, symmetric /hermitian and positive definite <paramref name="A"/> is 
        /// decomposed into a triangular equation system using cholesky factorization and solved via back-/ forward
        /// substitution.</para></item>
        /// <item>otherwise, if <paramref name="A"/> is only square it will be decomposed into upper and lower triangular matrices using 
        /// LU decomposition and the system than solved with the result.</item>
        /// <item>otherwise, if <paramref name="A"/> is [n, q] with q != n, the system is solved using <see cref="qr(InArray{fcomplex})"/> 
        /// decomposition. Note that <paramref name="A"/> can be rank deficient.</item>
        /// </list></para>
        /// <para>The internal storage order of <paramref name="A"/> and/or <paramref name="B"/> may be silently changed when this function returns. 
        /// The reason is that most functionality is performed in native LAPACK routines which require a certain storage layout (mostly 
        /// <see cref="StorageOrders.ColumnMajor"/>).</para>
        /// </remarks>
        /// <seealso cref="linsolve(InArray{fcomplex}, InArray{fcomplex}, ref MatrixProperties, bool)"/>
        /// <seealso cref="Size.StorageOrder"/>
        /// <seealso cref="lu(InArray{fcomplex})"/>
        /// <seealso cref="qr(InArray{fcomplex})"/>
        /// <seealso cref="pinv(InArray{fcomplex}, fcomplex?)"/>
        /// <seealso cref="svd(InArray{fcomplex}, OutArray{fcomplex}, bool)"/>
        public static Array<fcomplex> linsolve(InArray<fcomplex> A, InArray<fcomplex> B) {
            if (object.Equals(A, null) || object.Equals(B, null)) {
                throw new ArgumentException($"linsolve(): A and B must not be null. Found: A={(isnull(A) ? "(null)" : A.S.ToString())}, B={(isnull(B) ? "(null)" : B.S.ToString())}");
            }
            using (Scope.Enter()) {
                Array<fcomplex> _A = A, _B = B;

                MatrixProperties props = MatrixProperties.None;
                if (_A.Size[0] == _A.Size[1]) {
                    props |= MatrixProperties.Square;
                    if (istriup(_A)) {
                        props |= MatrixProperties.UpperTriangular;
                        return linsolve(_A, _B, ref props);
                    }
                    if (istrilow(_A)) {
                        props |= MatrixProperties.LowerTriangular;
                        return linsolve(_A, _B, ref props);
                    }
                    if (ishermitian(_A)) {
                        // give cholesky a try
                        props |= MatrixProperties.Hermitian;
                        props |= MatrixProperties.PositivDefinite;
                        Array<fcomplex> ret = linsolve(_A, _B, ref props, throwException: false);
                        if (!object.Equals(ret, null)) {
                            return ret;
                        } else {
                            props ^= MatrixProperties.PositivDefinite;
                        }
                    }
                }
                return linsolve(_A, _B, ref props);
            }
        }

        /// <summary>
        /// Solves a system of linear equations, B = Ax, taking hints for the best algorithm.
        /// </summary>
        /// <param name="A">Input matrix <paramref name="A"/>. Size [n, q].</param>
        /// <param name="B">Right hand side <paramref name="B"/>. Size [n, m].</param>
        /// <param name="props">[Input, Output] Known / determined matrix properties of <paramref name="A"/>.</param>
        /// <param name="throwException">[Optional] Throws an <see cref="ArgumentException"/> if <paramref name="A"/> was found to 
        /// be singular or a specific property in <paramref name="props"/> could not be confirmed. Default: true.</param>
        /// <returns>Solution x solving the equation <c>multiply(A, x) = B</c>. Size [q, m].</returns>
        /// <remarks><para>If no specific properties of <paramref name="A"/> are given in <paramref name="props"/> and 
        /// depending on the actual structure and properties of <paramref name="A"/> the equation system is solved with different approaches:
        /// <list type="bullet">
        /// <item>If <paramref name="A"/> is square (q == n) and an <b>upper or lower triangular</b> matrix, the 
        /// system is solved via backward- or forward substitution and the LAPACK function ?trtrs. 
        /// <example><code><![CDATA[Array<fcomplex> A = randn<fcomplex>(4,4); // construct 4 x 4 matrix 
        /// Array<fcomplex> B = vector<fcomplex>(1.0,2.0,3.0); 
        /// Array<fcomplex> x = linsolve(A,B);]]></code></example></item>
        /// <item><para>if <paramref name="A"/> is square, symmetric /hermitian and positive definite <paramref name="A"/> is 
        /// decomposed into a triangular equation system using cholesky factorization and solved via back-/ forward
        /// substitution.</para></item>
        /// <item>otherwise, if <paramref name="A"/> is only square it will be decomposed into upper and lower triangular matrices using 
        /// LU decomposition and the system is than solved with the triangular result.</item>
        /// <item>otherwise, if <paramref name="A"/>' is [n, q] with q != n, the system is solved using <see cref="qr(InArray{fcomplex})"/> 
        /// decomposition. Note that <paramref name="A"/> can be rank deficient.</item>
        /// </list></para>
        /// <para>Specifying known properties of <paramref name="A"/> in <paramref name="props"/> may saves some time in 
        /// determining these properties. If, for example, <paramref name="A"/> is known to be positive 
        /// definite, providing <see cref="MatrixProperties.PositivDefinite"/> allows the algorithm to 
        /// perform cholesky factorization with <paramref name="A"/>. However, 
        /// <see cref="linsolve(InArray{fcomplex}, InArray{fcomplex}, ref MatrixProperties, bool)"/> determines advantageous 
        /// properties automatically if required.</para>
        /// <para>After returning the <paramref name="props"/> structure can be inspected fo the matrix properties of 
        /// <paramref name="A"/> found during the computations. Any bits changed in <paramref name="props"/> by the 
        /// function reflect the path taken within <see cref="linsolve(InArray{fcomplex}, InArray{fcomplex}, ref MatrixProperties, bool)"/>. 
        /// However, <see cref="linsolve(InArray{fcomplex}, InArray{fcomplex}, ref MatrixProperties, bool)"/> does not determine <i>all</i> 
        /// properties of <paramref name="A"/> automatically. If, let's say: <paramref name="A"/> is found to be singular, 
        /// the function returns (or throws an exception) without determining further properties of A. In order to query specific 
        /// properties of <paramref name="A"/> other functionality may be more appropriate.</para>
        /// <para>If <paramref name="throwException"/> is false inspecting the <paramref name="props"/> structure on return is 
        /// required to verify that a valid solution has been computed. If <paramref name="A"/> was found to be singular and 
        /// <paramref name="throwException"/> is false then <paramref name="props"/> will have the <see cref="MatrixProperties.Singular"/>
        /// flag set. If the computation was performed by QR decomposition for a non-square matrix <paramref name="A"/> then 
        /// the <see cref="MatrixProperties.RankDeficient"/> flag may be set.</para>
        /// <para>The internal storage order of <paramref name="A"/> and/or <paramref name="B"/> may be silently changed 
        /// by this function. The reason is that most functionality is performed in native LAPACK routines which 
        /// require a certain storage layout (mostly <see cref="StorageOrders.ColumnMajor"/>). Be prepared that 
        /// both arrays may point to different memory regions for element storage afterwards! This does not, however, 
        /// affect the handling of the array with common high level functionality (subarrays, element access etc.).</para>
        /// </remarks>
        /// <seealso cref="linsolve(InArray{fcomplex}, InArray{fcomplex}, ref MatrixProperties, bool)"/>
        /// <seealso cref="Size.StorageOrder"/>
        /// <seealso cref="lu(InArray{fcomplex})"/>
        /// <seealso cref="qr(InArray{fcomplex})"/>
        /// <seealso cref="pinv(InArray{fcomplex}, fcomplex?)"/>
        /// <seealso cref="svd(InArray{fcomplex}, OutArray{fcomplex}, bool)"/>
        public static Array<fcomplex> linsolve(InArray<fcomplex> A, InArray<fcomplex> B, ref MatrixProperties props, bool throwException = true) {
            
            if (object.Equals(A, null))
                throw new ArgumentException("linsolve(): A must not be null!");
            if (object.Equals(B, null))
                throw new ArgumentException("linsolve(): B must not be null!");
            using (Scope.Enter()) {

                Array<fcomplex> _A = A, _B = B;
                if (_A.Size[0] != _B.Size[0])
                    throw new ArgumentException("linsolve(): number of rows of A must match the number of rows of _B!");

                long N = _A.Size[0], M = _B.S[1], Q = _A.S[1]; 
                if (N == 0) {
                    throw new ArgumentException($"linsolve() A and B must have at least one row each. Found: A.Size={_A.S}, B.Size={_B.S}."); 
                }

                if (_A.IsEmpty || _B.IsEmpty) {
                    System.Diagnostics.Debug.Assert(M == 0 || Q == 0); 
                    return empty<fcomplex>(M, Q);
                }
                int info = 0;
                Array<fcomplex> ret = empty<fcomplex>(0);
                if (N == _A.Size[1]) { 
                    props |= MatrixProperties.Square;
                    if ((props & MatrixProperties.LowerTriangular) != 0) {
                        ret.a = linsolveTriLow(_A, _B, ref info);
                        if (info > 0) {
                            props |= MatrixProperties.Singular;
                            if (throwException) {
                                throw new ArgumentException($"linsolve(): matrix A was found to be singular. Inversion from lower triangular form failed."); 
                            }
                        }
                        return ret;
                    }
                    if ((props & MatrixProperties.UpperTriangular) != 0) {
                        ret.a = linsolveTriUp(_A, _B, ref info);
                        if (info > 0) {
                            props |= MatrixProperties.Singular;
                            if (throwException) {
                                throw new ArgumentException($"linsolve(): matrix A was found to be singular. Inversion from upper triangular form failed.");
                            }
                        }
                        return ret;
                    }
                    unsafe {
                        if ((props & MatrixProperties.Hermitian) != 0) {
                            Array<fcomplex> cholFact = _A.Storage.copyUpperTriangle(N);
                            Lapack.cpotrf('U', (int)N, (fcomplex*)cholFact.GetHostPointerForWrite(), (int)N, ref info);
                            if (info > 0) {
                                props ^= MatrixProperties.Hermitian;
                                if (throwException) {
                                    throw new ArgumentException($"linsolve(): matrix A was specified to be symmetric / hermitian. But this turned out to be not true. Cholesky factorization (cpotrf) failed with info: {info}.");
                                }
                                // proceed with LU below
                            } else {
                                // solve 
                                ret.a = _B.C;

                                Lapack.cpotrs('U', (int)N, (int)_B.Size[1], (fcomplex*)cholFact.GetHostPointerForWrite(), (int)N, (fcomplex*)ret.GetHostPointerForWrite(StorageOrders.ColumnMajor), (int)N, ref info);
                                return ret;
                            }
                        }

                        // attempt complete (expensive) LU factorization 
                        Array<fcomplex> L = _A.C;

                        Array<int> pivIND = empty<int>(N, StorageOrders.ColumnMajor);
                        var pivInd = (int*)pivIND.GetHostPointerForWrite();

                        var lArr = (fcomplex*)L.GetHostPointerForWrite(StorageOrders.ColumnMajor);

                        Lapack.cgetrf((int)N, (int)N, lArr, (int)N, pivInd, ref info);

                        if (info > 0)
                            props |= MatrixProperties.Singular;

                        ret.a = _B.C;

                        Lapack.cgetrs('N', (int)N, (int)_B.Size[1], lArr, (int)N, pivInd, (fcomplex*)ret.GetHostPointerForWrite(StorageOrders.ColumnMajor), (int)N, ref info);
                        GC.KeepAlive(pivIND); 
                        GC.KeepAlive(L);

                        if (info < 0) {
                            throw new ArgumentException($"linsolve(): error solving in lapack cgetrs. Info: {info}.");
                        }
//#if DEBUG
//                        var retR = (RetArray<fcomplex>)ret;
//                        System.Diagnostics.Debug.WriteLine($"({System.Threading.Thread.CurrentThread.ManagedThreadId}) linsolve returns: {retR.Storage.ShortInfo(includeIDs: true)} = {string.Join("\n", retR.Storage.ToString(1000,1000000,StorageOrders.ColumnMajor))} ");
//                        return retR; 
//#else
                        return ret;
//#endif
                    }
                } else {
                    // under- / overdetermined system
                    int rank = 0, minNQ = (int)((N < Q) ? N : Q), maxNQ = (int)((N > Q) ? N : Q);

                    Array<fcomplex> tmpA = _A.C;

                    if (N < Q) {
                        ret.a = zeros<fcomplex>(Q, M, StorageOrders.ColumnMajor);
                        ret[r(0, N - 1), full] = _B; // this will be detached from _B

                    } else {
                        ret.a = _B;
                    }
                    Array<int> JPVT = zeros<int>(Q, 1, StorageOrders.ColumnMajor);
                    unsafe {
                        Lapack.cgelsy((int)N, (int)Q, (int)_B.Size[1], (fcomplex*)tmpA.GetHostPointerForWrite(StorageOrders.ColumnMajor), (int)N, (fcomplex*)ret.GetHostPointerForWrite(StorageOrders.ColumnMajor),
                                        maxNQ, (int*)JPVT.GetHostPointerForWrite(),  epsf, ref rank, ref info);
                    }
                    if (Q < N) {
                        ret.a = ret[r(0, Q - 1), full];
                    }
                    if (rank < minNQ) {
                        props |= MatrixProperties.RankDeficient;
                    }
                    return ret;
                }
            }
        }

        /// <summary>
        /// Solve system of linear equations A*x = B, with <paramref name="A"/> being a square, upper triangular matrix.
        /// </summary>
        /// <param name="A">Input matrix. Size [n,n]. Upper triangular. No check is made for that!</param>
        /// <param name="B">Right hand side vector /matrix. Size [n,m].</param>
        /// <param name="singularityDetect">[Output] This value gives the row of <paramref name="A"/>, 
        /// where a singularity has been detected (if any). If <paramref name="A"/> is not singular, this will return a negative value.</param>
        /// <returns>Solution x solving <paramref name="A"/> * x = <paramref name="B"/>. Size [n,m].</returns>
        /// <remarks><para>The solution is determined via backward substitution by using a native LAPACK module.</para>
        /// <para>Make sure, <paramref name="A"/> and <paramref name="B"/> are of correct size, since no checks are made for that!</para>
        /// <para>This function is used by <see cref="linsolve(InArray{fcomplex}, InArray{fcomplex}, ref MatrixProperties, bool)"/>. It can speed 
        /// up the computation for known, matching matrices, since it saves some checks on them which are otherwise automatically 
        /// performed by <see cref="linsolve(InArray{fcomplex}, InArray{fcomplex}, ref MatrixProperties, bool)"/>.</para>
        /// <para>Any elements of <paramref name="A"/> located below the main diagonal will be ignored.</para>
        /// <para>If <paramref name="A"/> is singular the array returned contains <see cref="fcomplex.NaN"/> values.</para></remarks>
        public static Array<fcomplex> linsolveTriUp(InArray<fcomplex> A, InArray<fcomplex> B, ref int singularityDetect) {
            return linsolveTriUpLow(A, B, ref singularityDetect, true);
        }

        /// <summary>
        /// Solve system of linear equations A*x = B, with <paramref name="A"/> being a square, lower triangular matrix.
        /// </summary>
        /// <param name="A">Input matrix. Size [n,n]. Lower triangular. No check is made for that!</param>
        /// <param name="B">Right hand side vector /matrix. Size [n,m].</param>
        /// <param name="singularityDetect">[Output] This value gives the row of <paramref name="A"/>, 
        /// where a singularity has been detected (if any). If <paramref name="A"/> is not singular, this will return a negative value.</param>
        /// <returns>Solution x solving <paramref name="A"/> * x = <paramref name="B"/>. Size [n,m].</returns>
        /// <remarks><para>The solution is determined via forward substitution by using a native LAPACK module.</para>
        /// <para>Make sure, <paramref name="A"/> and <paramref name="B"/> are of correct size, since no checks are made for that!</para>
        /// <para>This function is used by <see cref="linsolve(InArray{fcomplex}, InArray{fcomplex}, ref MatrixProperties, bool)"/>. It can speed 
        /// up the computation for matrices of known, matching properties since it saves some checks on them which are otherwise automatically 
        /// performed by <see cref="linsolve(InArray{fcomplex}, InArray{fcomplex}, ref MatrixProperties, bool)"/>.</para>
        /// <para>Any elements of <paramref name="A"/> located above the main diagonal will be ignored.</para>
        /// <para>If <paramref name="A"/> is singular the array returned contains <see cref="fcomplex.NaN"/> values.</para></remarks>
        public static Array< fcomplex> linsolveTriLow(InArray< fcomplex> A, InArray< fcomplex> B, ref int singularityDetect) {
            return linsolveTriUpLow(A, B, ref singularityDetect, false); 
        }

        private static Array<fcomplex> linsolveTriUpLow(InArray<fcomplex> A, InArray<fcomplex> B, ref int singularityDetect, bool upper) {
            System.Diagnostics.Debug.Assert(B.Size[1] >= 0);
            System.Diagnostics.Debug.Assert(B.Size[0] == A.Size[1]);
            System.Diagnostics.Debug.Assert(A.Size[0] == A.Size[1]);

            using (Scope.Enter()) {

                Array<fcomplex> _A = A, _B = B;
                singularityDetect = -1;
                long n = _A.Size[0];
                long m = _B.Size[1];
                int info = 0;
                Array<fcomplex> ret = _B.C;

                unsafe {
                    fcomplex* retArr = (fcomplex*)ret.GetHostPointerForWrite(StorageOrders.ColumnMajor);
                    // solve using Lapack

                    Lapack.ctrtrs(upper ? 'U' : 'L', 'N', 'N', (int)_A.Size[0], (int)_B.Size[1], (fcomplex*)_A.GetHostPointerForRead(StorageOrders.ColumnMajor),
                                 (int)_A.Size[0], (fcomplex*)ret.GetHostPointerForWrite(), (int)_B.Size[0], ref info);
                    GC.KeepAlive(_A); 
                    if (info < 0)
                        throw new ArgumentException("linsolveTriUp(): error in LAPACK function ctrtrs: " + (-info));
                    if (info > 0) {
                        singularityDetect = info - 1;
                        for (m = 0; m < ret.Size[1]; m++) {
                            ret[r(singularityDetect, end), m] = fcomplex.NaN;
                        }
                    } else {
                        singularityDetect = -1;
                    }
                    return ret;
                }
            }
        }


        /// <summary>
        /// Solves a system of linear equations, B = A x.
        /// </summary>
        /// <param name="A">Input matrix <paramref name="A"/>. Size [n, q].</param>
        /// <param name="B">Right hand side <paramref name="B"/>. Size [n, m].</param>
        /// <returns>Solution x solving the equation <c>multiply(A, x) = B</c>. Size [q, m].</returns>
        /// <remarks><para>Depending on the structure and properties of <paramref name="A"/> the equation system is solved with different approaches:
        /// <list type="bullet">
        /// <item>If <paramref name="A"/> is square (q == n) and an <b>upper or lower triangular</b> matrix, the 
        /// system is solved via backward- or forward substitution and the LAPACK function ?trtrs. 
        /// <example><code><![CDATA[Array<complex> A = randn<complex>(4,4); // construct 4 x 4 matrix 
        /// Array<complex> B = vector<complex>(1.0,2.0,3.0); 
        /// Array<complex> x = linsolve(A,B);]]></code></example></item>
        /// <item><para>if <paramref name="A"/> is square, symmetric /hermitian and positive definite <paramref name="A"/> is 
        /// decomposed into a triangular equation system using cholesky factorization and solved via back-/ forward
        /// substitution.</para></item>
        /// <item>otherwise, if <paramref name="A"/> is only square it will be decomposed into upper and lower triangular matrices using 
        /// LU decomposition and the system than solved with the result.</item>
        /// <item>otherwise, if <paramref name="A"/> is [n, q] with q != n, the system is solved using <see cref="qr(InArray{complex})"/> 
        /// decomposition. Note that <paramref name="A"/> can be rank deficient.</item>
        /// </list></para>
        /// <para>The internal storage order of <paramref name="A"/> and/or <paramref name="B"/> may be silently changed when this function returns. 
        /// The reason is that most functionality is performed in native LAPACK routines which require a certain storage layout (mostly 
        /// <see cref="StorageOrders.ColumnMajor"/>).</para>
        /// </remarks>
        /// <seealso cref="linsolve(InArray{complex}, InArray{complex}, ref MatrixProperties, bool)"/>
        /// <seealso cref="Size.StorageOrder"/>
        /// <seealso cref="lu(InArray{complex})"/>
        /// <seealso cref="qr(InArray{complex})"/>
        /// <seealso cref="pinv(InArray{complex}, complex?)"/>
        /// <seealso cref="svd(InArray{complex}, OutArray{complex}, bool)"/>
        public static Array<complex> linsolve(InArray<complex> A, InArray<complex> B) {
            if (object.Equals(A, null) || object.Equals(B, null)) {
                throw new ArgumentException($"linsolve(): A and B must not be null. Found: A={(isnull(A) ? "(null)" : A.S.ToString())}, B={(isnull(B) ? "(null)" : B.S.ToString())}");
            }
            using (Scope.Enter()) {
                Array<complex> _A = A, _B = B;

                MatrixProperties props = MatrixProperties.None;
                if (_A.Size[0] == _A.Size[1]) {
                    props |= MatrixProperties.Square;
                    if (istriup(_A)) {
                        props |= MatrixProperties.UpperTriangular;
                        return linsolve(_A, _B, ref props);
                    }
                    if (istrilow(_A)) {
                        props |= MatrixProperties.LowerTriangular;
                        return linsolve(_A, _B, ref props);
                    }
                    if (ishermitian(_A)) {
                        // give cholesky a try
                        props |= MatrixProperties.Hermitian;
                        props |= MatrixProperties.PositivDefinite;
                        Array<complex> ret = linsolve(_A, _B, ref props, throwException: false);
                        if (!object.Equals(ret, null)) {
                            return ret;
                        } else {
                            props ^= MatrixProperties.PositivDefinite;
                        }
                    }
                }
                return linsolve(_A, _B, ref props);
            }
        }

        /// <summary>
        /// Solves a system of linear equations, B = Ax, taking hints for the best algorithm.
        /// </summary>
        /// <param name="A">Input matrix <paramref name="A"/>. Size [n, q].</param>
        /// <param name="B">Right hand side <paramref name="B"/>. Size [n, m].</param>
        /// <param name="props">[Input, Output] Known / determined matrix properties of <paramref name="A"/>.</param>
        /// <param name="throwException">[Optional] Throws an <see cref="ArgumentException"/> if <paramref name="A"/> was found to 
        /// be singular or a specific property in <paramref name="props"/> could not be confirmed. Default: true.</param>
        /// <returns>Solution x solving the equation <c>multiply(A, x) = B</c>. Size [q, m].</returns>
        /// <remarks><para>If no specific properties of <paramref name="A"/> are given in <paramref name="props"/> and 
        /// depending on the actual structure and properties of <paramref name="A"/> the equation system is solved with different approaches:
        /// <list type="bullet">
        /// <item>If <paramref name="A"/> is square (q == n) and an <b>upper or lower triangular</b> matrix, the 
        /// system is solved via backward- or forward substitution and the LAPACK function ?trtrs. 
        /// <example><code><![CDATA[Array<complex> A = randn<complex>(4,4); // construct 4 x 4 matrix 
        /// Array<complex> B = vector<complex>(1.0,2.0,3.0); 
        /// Array<complex> x = linsolve(A,B);]]></code></example></item>
        /// <item><para>if <paramref name="A"/> is square, symmetric /hermitian and positive definite <paramref name="A"/> is 
        /// decomposed into a triangular equation system using cholesky factorization and solved via back-/ forward
        /// substitution.</para></item>
        /// <item>otherwise, if <paramref name="A"/> is only square it will be decomposed into upper and lower triangular matrices using 
        /// LU decomposition and the system is than solved with the triangular result.</item>
        /// <item>otherwise, if <paramref name="A"/>' is [n, q] with q != n, the system is solved using <see cref="qr(InArray{complex})"/> 
        /// decomposition. Note that <paramref name="A"/> can be rank deficient.</item>
        /// </list></para>
        /// <para>Specifying known properties of <paramref name="A"/> in <paramref name="props"/> may saves some time in 
        /// determining these properties. If, for example, <paramref name="A"/> is known to be positive 
        /// definite, providing <see cref="MatrixProperties.PositivDefinite"/> allows the algorithm to 
        /// perform cholesky factorization with <paramref name="A"/>. However, 
        /// <see cref="linsolve(InArray{complex}, InArray{complex}, ref MatrixProperties, bool)"/> determines advantageous 
        /// properties automatically if required.</para>
        /// <para>After returning the <paramref name="props"/> structure can be inspected fo the matrix properties of 
        /// <paramref name="A"/> found during the computations. Any bits changed in <paramref name="props"/> by the 
        /// function reflect the path taken within <see cref="linsolve(InArray{complex}, InArray{complex}, ref MatrixProperties, bool)"/>. 
        /// However, <see cref="linsolve(InArray{complex}, InArray{complex}, ref MatrixProperties, bool)"/> does not determine <i>all</i> 
        /// properties of <paramref name="A"/> automatically. If, let's say: <paramref name="A"/> is found to be singular, 
        /// the function returns (or throws an exception) without determining further properties of A. In order to query specific 
        /// properties of <paramref name="A"/> other functionality may be more appropriate.</para>
        /// <para>If <paramref name="throwException"/> is false inspecting the <paramref name="props"/> structure on return is 
        /// required to verify that a valid solution has been computed. If <paramref name="A"/> was found to be singular and 
        /// <paramref name="throwException"/> is false then <paramref name="props"/> will have the <see cref="MatrixProperties.Singular"/>
        /// flag set. If the computation was performed by QR decomposition for a non-square matrix <paramref name="A"/> then 
        /// the <see cref="MatrixProperties.RankDeficient"/> flag may be set.</para>
        /// <para>The internal storage order of <paramref name="A"/> and/or <paramref name="B"/> may be silently changed 
        /// by this function. The reason is that most functionality is performed in native LAPACK routines which 
        /// require a certain storage layout (mostly <see cref="StorageOrders.ColumnMajor"/>). Be prepared that 
        /// both arrays may point to different memory regions for element storage afterwards! This does not, however, 
        /// affect the handling of the array with common high level functionality (subarrays, element access etc.).</para>
        /// </remarks>
        /// <seealso cref="linsolve(InArray{complex}, InArray{complex}, ref MatrixProperties, bool)"/>
        /// <seealso cref="Size.StorageOrder"/>
        /// <seealso cref="lu(InArray{complex})"/>
        /// <seealso cref="qr(InArray{complex})"/>
        /// <seealso cref="pinv(InArray{complex}, complex?)"/>
        /// <seealso cref="svd(InArray{complex}, OutArray{complex}, bool)"/>
        public static Array<complex> linsolve(InArray<complex> A, InArray<complex> B, ref MatrixProperties props, bool throwException = true) {
            
            if (object.Equals(A, null))
                throw new ArgumentException("linsolve(): A must not be null!");
            if (object.Equals(B, null))
                throw new ArgumentException("linsolve(): B must not be null!");
            using (Scope.Enter()) {

                Array<complex> _A = A, _B = B;
                if (_A.Size[0] != _B.Size[0])
                    throw new ArgumentException("linsolve(): number of rows of A must match the number of rows of _B!");

                long N = _A.Size[0], M = _B.S[1], Q = _A.S[1]; 
                if (N == 0) {
                    throw new ArgumentException($"linsolve() A and B must have at least one row each. Found: A.Size={_A.S}, B.Size={_B.S}."); 
                }

                if (_A.IsEmpty || _B.IsEmpty) {
                    System.Diagnostics.Debug.Assert(M == 0 || Q == 0); 
                    return empty<complex>(M, Q);
                }
                int info = 0;
                Array<complex> ret = empty<complex>(0);
                if (N == _A.Size[1]) { 
                    props |= MatrixProperties.Square;
                    if ((props & MatrixProperties.LowerTriangular) != 0) {
                        ret.a = linsolveTriLow(_A, _B, ref info);
                        if (info > 0) {
                            props |= MatrixProperties.Singular;
                            if (throwException) {
                                throw new ArgumentException($"linsolve(): matrix A was found to be singular. Inversion from lower triangular form failed."); 
                            }
                        }
                        return ret;
                    }
                    if ((props & MatrixProperties.UpperTriangular) != 0) {
                        ret.a = linsolveTriUp(_A, _B, ref info);
                        if (info > 0) {
                            props |= MatrixProperties.Singular;
                            if (throwException) {
                                throw new ArgumentException($"linsolve(): matrix A was found to be singular. Inversion from upper triangular form failed.");
                            }
                        }
                        return ret;
                    }
                    unsafe {
                        if ((props & MatrixProperties.Hermitian) != 0) {
                            Array<complex> cholFact = _A.Storage.copyUpperTriangle(N);
                            Lapack.zpotrf('U', (int)N, (complex*)cholFact.GetHostPointerForWrite(), (int)N, ref info);
                            if (info > 0) {
                                props ^= MatrixProperties.Hermitian;
                                if (throwException) {
                                    throw new ArgumentException($"linsolve(): matrix A was specified to be symmetric / hermitian. But this turned out to be not true. Cholesky factorization (zpotrf) failed with info: {info}.");
                                }
                                // proceed with LU below
                            } else {
                                // solve 
                                ret.a = _B.C;

                                Lapack.zpotrs('U', (int)N, (int)_B.Size[1], (complex*)cholFact.GetHostPointerForWrite(), (int)N, (complex*)ret.GetHostPointerForWrite(StorageOrders.ColumnMajor), (int)N, ref info);
                                return ret;
                            }
                        }

                        // attempt complete (expensive) LU factorization 
                        Array<complex> L = _A.C;

                        Array<int> pivIND = empty<int>(N, StorageOrders.ColumnMajor);
                        var pivInd = (int*)pivIND.GetHostPointerForWrite();

                        var lArr = (complex*)L.GetHostPointerForWrite(StorageOrders.ColumnMajor);

                        Lapack.zgetrf((int)N, (int)N, lArr, (int)N, pivInd, ref info);

                        if (info > 0)
                            props |= MatrixProperties.Singular;

                        ret.a = _B.C;

                        Lapack.zgetrs('N', (int)N, (int)_B.Size[1], lArr, (int)N, pivInd, (complex*)ret.GetHostPointerForWrite(StorageOrders.ColumnMajor), (int)N, ref info);
                        GC.KeepAlive(pivIND); 
                        GC.KeepAlive(L);

                        if (info < 0) {
                            throw new ArgumentException($"linsolve(): error solving in lapack zgetrs. Info: {info}.");
                        }
//#if DEBUG
//                        var retR = (RetArray<complex>)ret;
//                        System.Diagnostics.Debug.WriteLine($"({System.Threading.Thread.CurrentThread.ManagedThreadId}) linsolve returns: {retR.Storage.ShortInfo(includeIDs: true)} = {string.Join("\n", retR.Storage.ToString(1000,1000000,StorageOrders.ColumnMajor))} ");
//                        return retR; 
//#else
                        return ret;
//#endif
                    }
                } else {
                    // under- / overdetermined system
                    int rank = 0, minNQ = (int)((N < Q) ? N : Q), maxNQ = (int)((N > Q) ? N : Q);

                    Array<complex> tmpA = _A.C;

                    if (N < Q) {
                        ret.a = zeros<complex>(Q, M, StorageOrders.ColumnMajor);
                        ret[r(0, N - 1), full] = _B; // this will be detached from _B

                    } else {
                        ret.a = _B;
                    }
                    Array<int> JPVT = zeros<int>(Q, 1, StorageOrders.ColumnMajor);
                    unsafe {
                        Lapack.zgelsy((int)N, (int)Q, (int)_B.Size[1], (complex*)tmpA.GetHostPointerForWrite(StorageOrders.ColumnMajor), (int)N, (complex*)ret.GetHostPointerForWrite(StorageOrders.ColumnMajor),
                                        maxNQ, (int*)JPVT.GetHostPointerForWrite(),  eps, ref rank, ref info);
                    }
                    if (Q < N) {
                        ret.a = ret[r(0, Q - 1), full];
                    }
                    if (rank < minNQ) {
                        props |= MatrixProperties.RankDeficient;
                    }
                    return ret;
                }
            }
        }

        /// <summary>
        /// Solve system of linear equations A*x = B, with <paramref name="A"/> being a square, upper triangular matrix.
        /// </summary>
        /// <param name="A">Input matrix. Size [n,n]. Upper triangular. No check is made for that!</param>
        /// <param name="B">Right hand side vector /matrix. Size [n,m].</param>
        /// <param name="singularityDetect">[Output] This value gives the row of <paramref name="A"/>, 
        /// where a singularity has been detected (if any). If <paramref name="A"/> is not singular, this will return a negative value.</param>
        /// <returns>Solution x solving <paramref name="A"/> * x = <paramref name="B"/>. Size [n,m].</returns>
        /// <remarks><para>The solution is determined via backward substitution by using a native LAPACK module.</para>
        /// <para>Make sure, <paramref name="A"/> and <paramref name="B"/> are of correct size, since no checks are made for that!</para>
        /// <para>This function is used by <see cref="linsolve(InArray{complex}, InArray{complex}, ref MatrixProperties, bool)"/>. It can speed 
        /// up the computation for known, matching matrices, since it saves some checks on them which are otherwise automatically 
        /// performed by <see cref="linsolve(InArray{complex}, InArray{complex}, ref MatrixProperties, bool)"/>.</para>
        /// <para>Any elements of <paramref name="A"/> located below the main diagonal will be ignored.</para>
        /// <para>If <paramref name="A"/> is singular the array returned contains <see cref="complex.NaN"/> values.</para></remarks>
        public static Array<complex> linsolveTriUp(InArray<complex> A, InArray<complex> B, ref int singularityDetect) {
            return linsolveTriUpLow(A, B, ref singularityDetect, true);
        }

        /// <summary>
        /// Solve system of linear equations A*x = B, with <paramref name="A"/> being a square, lower triangular matrix.
        /// </summary>
        /// <param name="A">Input matrix. Size [n,n]. Lower triangular. No check is made for that!</param>
        /// <param name="B">Right hand side vector /matrix. Size [n,m].</param>
        /// <param name="singularityDetect">[Output] This value gives the row of <paramref name="A"/>, 
        /// where a singularity has been detected (if any). If <paramref name="A"/> is not singular, this will return a negative value.</param>
        /// <returns>Solution x solving <paramref name="A"/> * x = <paramref name="B"/>. Size [n,m].</returns>
        /// <remarks><para>The solution is determined via forward substitution by using a native LAPACK module.</para>
        /// <para>Make sure, <paramref name="A"/> and <paramref name="B"/> are of correct size, since no checks are made for that!</para>
        /// <para>This function is used by <see cref="linsolve(InArray{complex}, InArray{complex}, ref MatrixProperties, bool)"/>. It can speed 
        /// up the computation for matrices of known, matching properties since it saves some checks on them which are otherwise automatically 
        /// performed by <see cref="linsolve(InArray{complex}, InArray{complex}, ref MatrixProperties, bool)"/>.</para>
        /// <para>Any elements of <paramref name="A"/> located above the main diagonal will be ignored.</para>
        /// <para>If <paramref name="A"/> is singular the array returned contains <see cref="complex.NaN"/> values.</para></remarks>
        public static Array< complex> linsolveTriLow(InArray< complex> A, InArray< complex> B, ref int singularityDetect) {
            return linsolveTriUpLow(A, B, ref singularityDetect, false); 
        }

        private static Array<complex> linsolveTriUpLow(InArray<complex> A, InArray<complex> B, ref int singularityDetect, bool upper) {
            System.Diagnostics.Debug.Assert(B.Size[1] >= 0);
            System.Diagnostics.Debug.Assert(B.Size[0] == A.Size[1]);
            System.Diagnostics.Debug.Assert(A.Size[0] == A.Size[1]);

            using (Scope.Enter()) {

                Array<complex> _A = A, _B = B;
                singularityDetect = -1;
                long n = _A.Size[0];
                long m = _B.Size[1];
                int info = 0;
                Array<complex> ret = _B.C;

                unsafe {
                    complex* retArr = (complex*)ret.GetHostPointerForWrite(StorageOrders.ColumnMajor);
                    // solve using Lapack

                    Lapack.ztrtrs(upper ? 'U' : 'L', 'N', 'N', (int)_A.Size[0], (int)_B.Size[1], (complex*)_A.GetHostPointerForRead(StorageOrders.ColumnMajor),
                                 (int)_A.Size[0], (complex*)ret.GetHostPointerForWrite(), (int)_B.Size[0], ref info);
                    GC.KeepAlive(_A); 
                    if (info < 0)
                        throw new ArgumentException("linsolveTriUp(): error in LAPACK function ztrtrs: " + (-info));
                    if (info > 0) {
                        singularityDetect = info - 1;
                        for (m = 0; m < ret.Size[1]; m++) {
                            ret[r(singularityDetect, end), m] = complex.NaN;
                        }
                    } else {
                        singularityDetect = -1;
                    }
                    return ret;
                }
            }
        }


#endregion HYCALPER AUTO GENERATED CODE

    }
}
