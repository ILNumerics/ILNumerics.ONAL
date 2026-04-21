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
using ILNumerics.Core.StorageLayer; 

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
        Double
    </source>
    <destination>double</destination>
    <destination>float</destination>
    <destination>float</destination>
</type>
<type>
    <source locate="here">
        dgeqrf 
    </source>
    <destination>zgeqrf</destination>
    <destination>cgeqrf</destination>
    <destination>sgeqrf</destination>
</type>
<type>
    <source locate="here">
        dorgqr
    </source>
    <destination>zungqr</destination>
    <destination>cungqr</destination>
    <destination>sorgqr</destination>
</type>
<type>
    <source locate="here">
        dgegqr
    </source>
    <destination>zgegqr</destination>
    <destination>cgegqr</destination>
    <destination>sgegqr</destination>
</type>
<type>
    <source locate="here">
        dgeqp3
    </source>
    <destination>zgeqp3</destination>
    <destination>cgeqp3</destination>
    <destination>sgeqp3</destination>
</type>
</hycalper>
*/

namespace ILNumerics
{
    public partial class ILMath 
    {

        #region HYCALPER LOOPSTART
        /// <summary>
        /// QR decomposition - raw output as returned from LAPACK.
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <returns>Orthonormal / unitary matrix Q and upper triangular matrix R 
        /// packed into a single matrix. This is the output of the lapack function ?geqrf.</returns>
        /// <remarks><para>Input matrix <paramref name="A"/> is not altered. </para>
        /// <para>The matrix returned is the direct output of the lapack 
        /// function [d,s,c,z]geqrf, respectively. It contains the decomposition factors Q and R, 
        /// but they are combined into a single matrix. Overloads exist which return Q and R individually: 
        /// <see cref="qr(InArray{double}, OutArray{double}, bool)"/>.</para></remarks>
        public static Array<double> qr(InArray<double> A) {
            using (Scope.Enter()) {

                Array<double> _A = A;

                if (isnull(_A)) {
                    throw new ArgumentNullException("qr(A): input A must not be null.");
                }
                if (!_A.IsMatrix) {
                    throw new ArgumentException("qr(A): input A must be matrix.");
                }
                long m = _A.Size[0], n = _A.Size[1];
                Array<double> ret = _A.C;

                var minMN = (m < n) ? m : n;
                Array<double> tau = empty<double>(minMN); 
                int info = 0;
                unsafe {
                    Lapack.dgeqrf((int)m, (int)n, (double*)ret.GetHostPointerForWrite(StorageOrders.ColumnMajor), (int)m, (double*)tau.GetHostPointerForWrite(), ref info);
                }
                if (info < 0)
                    throw new ArgumentException($"qr(): unknown error in dgeqrf. Info: {info}.");
                return ret;
            }
        }
        /// <summary>
        /// QR decomposition, returning Q and <paramref name="R"/>, optionally economical sized.
        /// </summary>
        /// <param name="A">Input matrix. Size [m x n].</param>
        /// <param name="R">[Output, optional] Upper triangular matrix <paramref name="R"/>. Size [m x n]. Default: (null) same as <see cref="qr(InArray{double})"/>.</param>
        /// <param name="economySize">[Optional] True: if m &gt;= n the size of Q and <paramref name="R"/> is reduced to [m x m] and [m x n] respectively. Default: false.</param>
        /// <returns>Orthonormal real / unitary complex matrix Q as result of decomposing <paramref name="A"/>. Size [m x m] or [m x min(m,n)] if <paramref name="economySize"/> is true or if A is empty.</returns>
        /// <remarks><para>The function returns Q and R such that the equation 
        /// <code>A = Q ** R</code> holds within the range of roundoff errors. ('**' 
        /// denotes matrix multiplication.)</para></remarks>
        public static Array<double> qr(InArray<double> A, OutArray<double> R, bool economySize = false)
        {
            using (Scope.Enter()) {

                Array<double> _A = A;

                if (isnull(_A)) {
                    throw new ArgumentNullException("qr(A,R...): input A must not be null.");
                }
                if (!_A.IsMatrix) {
                    throw new ArgumentException("qr(A,R...): input A must be matrix.");
                }
                if (Object.Equals(R, null)) {
                    return qr(_A);
                }
                long m = _A.Size[0];
                long n = _A.Size[1];
                if (m < n && economySize) {
                    economySize = false;
                }
                Array<double> ret = (double)0;
                if (m == 0 || n == 0) {
                    if (!object.Equals(R, null)) {
                        lock (R.SynchObj)
                            R.a = empty<double>(m, n);
                    }
                    return empty<double>(_A.Size[0], Math.Min(m, n));
                }
                var minMN = (m < n) ? m : n;

                Array<double> tau = empty<double>(minMN); ;

                if (m >= n) {
                    ret.a = zeros<double>(m, (economySize) ? minMN : m);
                } else {
                    // economySize is always false ... !
                    // a temporary array is needed for extraction of the compact lapack Q (?geqrf)
                    ret.a = zeros<double>(m, n);
                }
                ret[full, r(0, n - 1)] = _A[full, full];
                unsafe {

                    double* QArr = (double*)ret.GetHostPointerForWrite();

                    int info = 0;
                    Lapack.dgeqrf((int)m, (int)n, QArr, (int)m, (double*)tau.GetHostPointerForWrite(), ref info);

                    if (info != 0) {
                        throw new ArgumentException($"qr(): error in lapack dgeqrf. info: {info}");
                    }
                    // extract R, Q
                    lock (R.SynchObj)
                    if (economySize) {
                        R.a = copyUpperTriangle<double>(ret, minMN, n);

                        Lapack.dorgqr((int)m, (int)minMN, (int)minMN, QArr, (int)m, (double*)tau.GetHostPointerForWrite(), ref info);
                    } else {
                        R.a = copyUpperTriangle<double>(ret, m, n);

                        Lapack.dorgqr((int)m, (int)m, (int)minMN, QArr, (int)m, (double*)tau.GetHostPointerForWrite(), ref info);
                        if (m < n) {
                            ret.a = ret[full, r(0, m - 1)];
                        }
                    }
                    if (info != 0) {
                        throw new ArgumentException($"qr(): error in lapack function dorgqr. info: {info}.");
                    }
                }
                return ret;
            }
        }
        /// <summary>
        /// QR decomposition with pivoting, potentially size saving shapes.
        /// </summary>
        /// <param name="A">Input matrix. Size [m x n].</param>
        /// <param name="R">[Output, optional] Upper triangular matrix. Size [m x n] or [min(m,n) x n] depending 
        /// on <paramref name="economySize"/> (see remarks). Default: (null) do not compute <paramref name="R"/>.</param>
        /// <param name="economySize">[Optional] True: return more efficient structures if possible. See remarks for details. Default: false.</param>
        /// <param name="E">[Output, optional] Permutation matrix from pivoting. Size [m x m]. Default: (null) do not return.<paramref name="E"/>.</param>
        /// <returns>Orthonormal / unitary matrix Q as result of decomposing <paramref name="A"/>. Size [m x m] or [m x min(m,n)], depending on <paramref name="economySize"/> (see remarks).</returns>
        /// <remarks>
        /// <para>This function performs the QR decomposition on matrix <paramref name="A"/>. It computes matrices Q, <paramref name="R"/> 
        /// and <paramref name="E"/>, with Q ** R = A ** E, where '**' denotes matrix multiplication.</para>
        /// <para>If <paramref name="R"/> is null on entry, the function returns the compact QR decomposition result from <see cref="qr(InArray{double})"/>.</para>
        /// <para>If <paramref name="economySize"/> is false, the function 
        /// returns Q, <paramref name="R"/> and <paramref name="E"/> such that the equation <code>A * E = Q * R</code> holds within 
        /// roundoff errors.</para>
        /// <para>If <paramref name="economySize"/> is true ... <list type="bullet">
        /// <item> and m &gt;= n the size of Q and <paramref name="R"/> will be [m x min(m,n)] and [min(m,n) x n] respectively. 
        /// For m &lt; n the sizes are not changed and as specified above.</item>
        /// <item>the output parameter <paramref name="E"/> is returned as vector [n] with permutation indices 
        /// rather than as a permutation matrix [n,n]. In this case the equation <code>A[":",E] == Q * R</code> holds, except roundoff errors.</item></list></para>
        /// <para><paramref name="E"/> reflects the pivoting of <paramref name="A"/> done inside the LAPACK function performing the decomposition so that <paramref name="R"/> 
        /// shows increasing diagonal element values. If <paramref name="E"/> is not requested (null) the equation Q ** R = A holds instead.</para></remarks>
        /// <seealso cref="qr(InArray{double}, OutArray{double}, bool)"/>
        /// <seealso cref="svd(InArray{double}, OutArray{double})"/>
        /// <seealso cref="eig(InArray{float}, OutArray{fcomplex}, ref MatrixProperties, bool)"/>
        public static Array<double> qr(
                        InArray<double> A,
                        OutArray<double> R = null,
                        OutArray<int> E = null,
                        bool economySize = false) {
            using (Scope.Enter()) {

                Array<double> _A = A;

                if (isnull(_A)) {
                    throw new ArgumentNullException("qr(A,R,E,...): input A must not be null.");
                }
                if (!_A.IsMatrix) {
                    throw new ArgumentException("qr(A,R,E,...): input A must be matrix.");
                }
                if (Object.Equals(R, null)) {
                    return qr(_A);
                }
                long m = _A.Size[0];
                long n = _A.Size[1];
                if (m < n && economySize) {
                    return qr(_A, R, false);
                }
                if (m == 0 || n == 0) {
                    lock (R.SynchObj)
                    if (economySize) {
                        if (!object.Equals(R, null)) {
                            R.a = zeros<double>(Math.Min(m,n), n);
                        }
                        if (!object.Equals(E, null)) {
                            E.a = zeros<int>(n, dim1: 1);
                        }
                        return empty<double>(m, Math.Min(m, n));
                    } else {
                        if (!object.Equals(R, null)) {
                            R.a = zeros<double>(m, n);
                        }
                        if (!object.Equals(E, null)) {
                            E.a = zeros<int>(n, n);
                        }
                        return empty<double>(m, m);
                    }
                }
                // shortcut w/o pivoting
                if (object.Equals(E, null)) {
                    return qr(_A, R, economySize);
                }

                var minMN = (m < n) ? m : n;
                Array<int> ipvt = zeros<int>(n, 1, StorageOrders.ColumnMajor); 
                Array<double> tau = empty<double>(minMN, StorageOrders.ColumnMajor);

                Array<double> ret;
                if (m >= n && !economySize) {
                    // ret will be [m x m], fill [m x n] with _A

                    ret = zeros<double>(m, m, StorageOrders.ColumnMajor);
                    System.Diagnostics.Debug.Assert(_A.IsMatrix); 
                    ret[full, r(0, n - 1)] = _A;

                } else {
                    // economy flag or economy size. 
                    // ret is [m x min(m,n)], filled with _A
                    // a temporary array is needed for extraction of the compact lapack Q (?geqrf)
                    ret = _A.C;
                }
                //ret[full, r(0, n - 1)] = _A[full, full];

                unsafe {
                    double* QArr = (double*)ret.GetHostPointerForWrite();
                    int info = 0;

                    Lapack.dgeqp3((int)m, (int)n, QArr, (int)m, (int*)ipvt.GetHostPointerForWrite(), (double*)tau.GetHostPointerForWrite(), ref info);

                    if (info != 0) {
                        throw new ArgumentException($"qr(): error inside lapack function dgeqp3. info:{info}.");
                    }

                    // extract R, Q
                    lock (R.SynchObj) {
                        if (economySize) {
                            R.a = copyUpperTriangle<double>(ret, minMN, n);

                            Lapack.dorgqr((int)m, (int)minMN, (int)minMN, QArr, (int)m, (double*)tau.GetHostPointerForWrite(), ref info);

                            // transform E into out typed vector
                            E.a = ipvt - 1;

                        } else {
                            R.a = copyUpperTriangle<double>(ret, m, n);

                            Lapack.dorgqr((int)m, (int)m, (int)minMN, QArr, (int)m, (double*)tau.GetHostPointerForWrite(), ref info);
                            if (m < n) {
                                ret.a = ret[full, r(0, m - 1)];
                            }
                            // transform E into matrix
                            E.a = zeros<int>(n, n);

                            using (Scope.Enter(arrayStyle: ArrayStyles.numpy)) {
                                // numpy advanced indexing helps in picking specific elements from the matrix E. But 
                                // make sure that all indices are vectors: 1-D only! (ipvt is likely [n,1] here!)
                                E[squeeze(ipvt) - 1, counter(0, 1, dim0: n)] = 1;
                            }
                        }
                    }
                    GC.KeepAlive(tau); 
                    if (info != 0) {
                        throw new ArgumentException($"qr(): error in lapack function dorgqr. info: {info}.");
                    }
                    return ret;
                }
            }
        }

        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
        /// <summary>
        /// QR decomposition - raw output as returned from LAPACK.
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <returns>Orthonormal / unitary matrix Q and upper triangular matrix R 
        /// packed into a single matrix. This is the output of the lapack function ?geqrf.</returns>
        /// <remarks><para>Input matrix <paramref name="A"/> is not altered. </para>
        /// <para>The matrix returned is the direct output of the lapack 
        /// function [d,s,c,z]geqrf, respectively. It contains the decomposition factors Q and R, 
        /// but they are combined into a single matrix. Overloads exist which return Q and R individually: 
        /// <see cref="qr(InArray{float}, OutArray{float}, bool)"/>.</para></remarks>
        public static Array<float> qr(InArray<float> A) {
            using (Scope.Enter()) {

                Array<float> _A = A;

                if (isnull(_A)) {
                    throw new ArgumentNullException("qr(A): input A must not be null.");
                }
                if (!_A.IsMatrix) {
                    throw new ArgumentException("qr(A): input A must be matrix.");
                }
                long m = _A.Size[0], n = _A.Size[1];
                Array<float> ret = _A.C;

                var minMN = (m < n) ? m : n;
                Array<float> tau = empty<float>(minMN); 
                int info = 0;
                unsafe {
                    Lapack.sgeqrf((int)m, (int)n, (float*)ret.GetHostPointerForWrite(StorageOrders.ColumnMajor), (int)m, (float*)tau.GetHostPointerForWrite(), ref info);
                }
                if (info < 0)
                    throw new ArgumentException($"qr(): unknown error in sgeqrf. Info: {info}.");
                return ret;
            }
        }
        /// <summary>
        /// QR decomposition, returning Q and <paramref name="R"/>, optionally economical sized.
        /// </summary>
        /// <param name="A">Input matrix. Size [m x n].</param>
        /// <param name="R">[Output, optional] Upper triangular matrix <paramref name="R"/>. Size [m x n]. Default: (null) same as <see cref="qr(InArray{float})"/>.</param>
        /// <param name="economySize">[Optional] True: if m &gt;= n the size of Q and <paramref name="R"/> is reduced to [m x m] and [m x n] respectively. Default: false.</param>
        /// <returns>Orthonormal real / unitary complex matrix Q as result of decomposing <paramref name="A"/>. Size [m x m] or [m x min(m,n)] if <paramref name="economySize"/> is true or if A is empty.</returns>
        /// <remarks><para>The function returns Q and R such that the equation 
        /// <code>A = Q ** R</code> holds within the range of roundoff errors. ('**' 
        /// denotes matrix multiplication.)</para></remarks>
        public static Array<float> qr(InArray<float> A, OutArray<float> R, bool economySize = false)
        {
            using (Scope.Enter()) {

                Array<float> _A = A;

                if (isnull(_A)) {
                    throw new ArgumentNullException("qr(A,R...): input A must not be null.");
                }
                if (!_A.IsMatrix) {
                    throw new ArgumentException("qr(A,R...): input A must be matrix.");
                }
                if (Object.Equals(R, null)) {
                    return qr(_A);
                }
                long m = _A.Size[0];
                long n = _A.Size[1];
                if (m < n && economySize) {
                    economySize = false;
                }
                Array<float> ret = (float)0;
                if (m == 0 || n == 0) {
                    if (!object.Equals(R, null)) {
                        lock (R.SynchObj)
                            R.a = empty<float>(m, n);
                    }
                    return empty<float>(_A.Size[0], Math.Min(m, n));
                }
                var minMN = (m < n) ? m : n;

                Array<float> tau = empty<float>(minMN); ;

                if (m >= n) {
                    ret.a = zeros<float>(m, (economySize) ? minMN : m);
                } else {
                    // economySize is always false ... !
                    // a temporary array is needed for extraction of the compact lapack Q (?geqrf)
                    ret.a = zeros<float>(m, n);
                }
                ret[full, r(0, n - 1)] = _A[full, full];
                unsafe {

                    float* QArr = (float*)ret.GetHostPointerForWrite();

                    int info = 0;
                    Lapack.sgeqrf((int)m, (int)n, QArr, (int)m, (float*)tau.GetHostPointerForWrite(), ref info);

                    if (info != 0) {
                        throw new ArgumentException($"qr(): error in lapack sgeqrf. info: {info}");
                    }
                    // extract R, Q
                    lock (R.SynchObj)
                    if (economySize) {
                        R.a = copyUpperTriangle<float>(ret, minMN, n);

                        Lapack.sorgqr((int)m, (int)minMN, (int)minMN, QArr, (int)m, (float*)tau.GetHostPointerForWrite(), ref info);
                    } else {
                        R.a = copyUpperTriangle<float>(ret, m, n);

                        Lapack.sorgqr((int)m, (int)m, (int)minMN, QArr, (int)m, (float*)tau.GetHostPointerForWrite(), ref info);
                        if (m < n) {
                            ret.a = ret[full, r(0, m - 1)];
                        }
                    }
                    if (info != 0) {
                        throw new ArgumentException($"qr(): error in lapack function sorgqr. info: {info}.");
                    }
                }
                return ret;
            }
        }
        /// <summary>
        /// QR decomposition with pivoting, potentially size saving shapes.
        /// </summary>
        /// <param name="A">Input matrix. Size [m x n].</param>
        /// <param name="R">[Output, optional] Upper triangular matrix. Size [m x n] or [min(m,n) x n] depending 
        /// on <paramref name="economySize"/> (see remarks). Default: (null) do not compute <paramref name="R"/>.</param>
        /// <param name="economySize">[Optional] True: return more efficient structures if possible. See remarks for details. Default: false.</param>
        /// <param name="E">[Output, optional] Permutation matrix from pivoting. Size [m x m]. Default: (null) do not return.<paramref name="E"/>.</param>
        /// <returns>Orthonormal / unitary matrix Q as result of decomposing <paramref name="A"/>. Size [m x m] or [m x min(m,n)], depending on <paramref name="economySize"/> (see remarks).</returns>
        /// <remarks>
        /// <para>This function performs the QR decomposition on matrix <paramref name="A"/>. It computes matrices Q, <paramref name="R"/> 
        /// and <paramref name="E"/>, with Q ** R = A ** E, where '**' denotes matrix multiplication.</para>
        /// <para>If <paramref name="R"/> is null on entry, the function returns the compact QR decomposition result from <see cref="qr(InArray{float})"/>.</para>
        /// <para>If <paramref name="economySize"/> is false, the function 
        /// returns Q, <paramref name="R"/> and <paramref name="E"/> such that the equation <code>A * E = Q * R</code> holds within 
        /// roundoff errors.</para>
        /// <para>If <paramref name="economySize"/> is true ... <list type="bullet">
        /// <item> and m &gt;= n the size of Q and <paramref name="R"/> will be [m x min(m,n)] and [min(m,n) x n] respectively. 
        /// For m &lt; n the sizes are not changed and as specified above.</item>
        /// <item>the output parameter <paramref name="E"/> is returned as vector [n] with permutation indices 
        /// rather than as a permutation matrix [n,n]. In this case the equation <code>A[":",E] == Q * R</code> holds, except roundoff errors.</item></list></para>
        /// <para><paramref name="E"/> reflects the pivoting of <paramref name="A"/> done inside the LAPACK function performing the decomposition so that <paramref name="R"/> 
        /// shows increasing diagonal element values. If <paramref name="E"/> is not requested (null) the equation Q ** R = A holds instead.</para></remarks>
        /// <seealso cref="qr(InArray{float}, OutArray{float}, bool)"/>
        /// <seealso cref="svd(InArray{float}, OutArray{float})"/>
        /// <seealso cref="eig(InArray{float}, OutArray{fcomplex}, ref MatrixProperties, bool)"/>
        public static Array<float> qr(
                        InArray<float> A,
                        OutArray<float> R = null,
                        OutArray<int> E = null,
                        bool economySize = false) {
            using (Scope.Enter()) {

                Array<float> _A = A;

                if (isnull(_A)) {
                    throw new ArgumentNullException("qr(A,R,E,...): input A must not be null.");
                }
                if (!_A.IsMatrix) {
                    throw new ArgumentException("qr(A,R,E,...): input A must be matrix.");
                }
                if (Object.Equals(R, null)) {
                    return qr(_A);
                }
                long m = _A.Size[0];
                long n = _A.Size[1];
                if (m < n && economySize) {
                    return qr(_A, R, false);
                }
                if (m == 0 || n == 0) {
                    lock (R.SynchObj)
                    if (economySize) {
                        if (!object.Equals(R, null)) {
                            R.a = zeros<float>(Math.Min(m,n), n);
                        }
                        if (!object.Equals(E, null)) {
                            E.a = zeros<int>(n, dim1: 1);
                        }
                        return empty<float>(m, Math.Min(m, n));
                    } else {
                        if (!object.Equals(R, null)) {
                            R.a = zeros<float>(m, n);
                        }
                        if (!object.Equals(E, null)) {
                            E.a = zeros<int>(n, n);
                        }
                        return empty<float>(m, m);
                    }
                }
                // shortcut w/o pivoting
                if (object.Equals(E, null)) {
                    return qr(_A, R, economySize);
                }

                var minMN = (m < n) ? m : n;
                Array<int> ipvt = zeros<int>(n, 1, StorageOrders.ColumnMajor); 
                Array<float> tau = empty<float>(minMN, StorageOrders.ColumnMajor);

                Array<float> ret;
                if (m >= n && !economySize) {
                    // ret will be [m x m], fill [m x n] with _A

                    ret = zeros<float>(m, m, StorageOrders.ColumnMajor);
                    System.Diagnostics.Debug.Assert(_A.IsMatrix); 
                    ret[full, r(0, n - 1)] = _A;

                } else {
                    // economy flag or economy size. 
                    // ret is [m x min(m,n)], filled with _A
                    // a temporary array is needed for extraction of the compact lapack Q (?geqrf)
                    ret = _A.C;
                }
                //ret[full, r(0, n - 1)] = _A[full, full];

                unsafe {
                    float* QArr = (float*)ret.GetHostPointerForWrite();
                    int info = 0;

                    Lapack.sgeqp3((int)m, (int)n, QArr, (int)m, (int*)ipvt.GetHostPointerForWrite(), (float*)tau.GetHostPointerForWrite(), ref info);

                    if (info != 0) {
                        throw new ArgumentException($"qr(): error inside lapack function sgeqp3. info:{info}.");
                    }

                    // extract R, Q
                    lock (R.SynchObj) {
                        if (economySize) {
                            R.a = copyUpperTriangle<float>(ret, minMN, n);

                            Lapack.sorgqr((int)m, (int)minMN, (int)minMN, QArr, (int)m, (float*)tau.GetHostPointerForWrite(), ref info);

                            // transform E into out typed vector
                            E.a = ipvt - 1;

                        } else {
                            R.a = copyUpperTriangle<float>(ret, m, n);

                            Lapack.sorgqr((int)m, (int)m, (int)minMN, QArr, (int)m, (float*)tau.GetHostPointerForWrite(), ref info);
                            if (m < n) {
                                ret.a = ret[full, r(0, m - 1)];
                            }
                            // transform E into matrix
                            E.a = zeros<int>(n, n);

                            using (Scope.Enter(arrayStyle: ArrayStyles.numpy)) {
                                // numpy advanced indexing helps in picking specific elements from the matrix E. But 
                                // make sure that all indices are vectors: 1-D only! (ipvt is likely [n,1] here!)
                                E[squeeze(ipvt) - 1, counter(0, 1, dim0: n)] = 1;
                            }
                        }
                    }
                    GC.KeepAlive(tau); 
                    if (info != 0) {
                        throw new ArgumentException($"qr(): error in lapack function sorgqr. info: {info}.");
                    }
                    return ret;
                }
            }
        }

        /// <summary>
        /// QR decomposition - raw output as returned from LAPACK.
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <returns>Orthonormal / unitary matrix Q and upper triangular matrix R 
        /// packed into a single matrix. This is the output of the lapack function ?geqrf.</returns>
        /// <remarks><para>Input matrix <paramref name="A"/> is not altered. </para>
        /// <para>The matrix returned is the direct output of the lapack 
        /// function [d,s,c,z]geqrf, respectively. It contains the decomposition factors Q and R, 
        /// but they are combined into a single matrix. Overloads exist which return Q and R individually: 
        /// <see cref="qr(InArray{fcomplex}, OutArray{fcomplex}, bool)"/>.</para></remarks>
        public static Array<fcomplex> qr(InArray<fcomplex> A) {
            using (Scope.Enter()) {

                Array<fcomplex> _A = A;

                if (isnull(_A)) {
                    throw new ArgumentNullException("qr(A): input A must not be null.");
                }
                if (!_A.IsMatrix) {
                    throw new ArgumentException("qr(A): input A must be matrix.");
                }
                long m = _A.Size[0], n = _A.Size[1];
                Array<fcomplex> ret = _A.C;

                var minMN = (m < n) ? m : n;
                Array<fcomplex> tau = empty<fcomplex>(minMN); 
                int info = 0;
                unsafe {
                    Lapack.cgeqrf((int)m, (int)n, (fcomplex*)ret.GetHostPointerForWrite(StorageOrders.ColumnMajor), (int)m, (fcomplex*)tau.GetHostPointerForWrite(), ref info);
                }
                if (info < 0)
                    throw new ArgumentException($"qr(): unknown error in cgeqrf. Info: {info}.");
                return ret;
            }
        }
        /// <summary>
        /// QR decomposition, returning Q and <paramref name="R"/>, optionally economical sized.
        /// </summary>
        /// <param name="A">Input matrix. Size [m x n].</param>
        /// <param name="R">[Output, optional] Upper triangular matrix <paramref name="R"/>. Size [m x n]. Default: (null) same as <see cref="qr(InArray{fcomplex})"/>.</param>
        /// <param name="economySize">[Optional] True: if m &gt;= n the size of Q and <paramref name="R"/> is reduced to [m x m] and [m x n] respectively. Default: false.</param>
        /// <returns>Orthonormal real / unitary complex matrix Q as result of decomposing <paramref name="A"/>. Size [m x m] or [m x min(m,n)] if <paramref name="economySize"/> is true or if A is empty.</returns>
        /// <remarks><para>The function returns Q and R such that the equation 
        /// <code>A = Q ** R</code> holds within the range of roundoff errors. ('**' 
        /// denotes matrix multiplication.)</para></remarks>
        public static Array<fcomplex> qr(InArray<fcomplex> A, OutArray<fcomplex> R, bool economySize = false)
        {
            using (Scope.Enter()) {

                Array<fcomplex> _A = A;

                if (isnull(_A)) {
                    throw new ArgumentNullException("qr(A,R...): input A must not be null.");
                }
                if (!_A.IsMatrix) {
                    throw new ArgumentException("qr(A,R...): input A must be matrix.");
                }
                if (Object.Equals(R, null)) {
                    return qr(_A);
                }
                long m = _A.Size[0];
                long n = _A.Size[1];
                if (m < n && economySize) {
                    economySize = false;
                }
                Array<fcomplex> ret = (fcomplex)0;
                if (m == 0 || n == 0) {
                    if (!object.Equals(R, null)) {
                        lock (R.SynchObj)
                            R.a = empty<fcomplex>(m, n);
                    }
                    return empty<fcomplex>(_A.Size[0], Math.Min(m, n));
                }
                var minMN = (m < n) ? m : n;

                Array<fcomplex> tau = empty<fcomplex>(minMN); ;

                if (m >= n) {
                    ret.a = zeros<fcomplex>(m, (economySize) ? minMN : m);
                } else {
                    // economySize is always false ... !
                    // a temporary array is needed for extraction of the compact lapack Q (?geqrf)
                    ret.a = zeros<fcomplex>(m, n);
                }
                ret[full, r(0, n - 1)] = _A[full, full];
                unsafe {

                    fcomplex* QArr = (fcomplex*)ret.GetHostPointerForWrite();

                    int info = 0;
                    Lapack.cgeqrf((int)m, (int)n, QArr, (int)m, (fcomplex*)tau.GetHostPointerForWrite(), ref info);

                    if (info != 0) {
                        throw new ArgumentException($"qr(): error in lapack cgeqrf. info: {info}");
                    }
                    // extract R, Q
                    lock (R.SynchObj)
                    if (economySize) {
                        R.a = copyUpperTriangle<fcomplex>(ret, minMN, n);

                        Lapack.cungqr((int)m, (int)minMN, (int)minMN, QArr, (int)m, (fcomplex*)tau.GetHostPointerForWrite(), ref info);
                    } else {
                        R.a = copyUpperTriangle<fcomplex>(ret, m, n);

                        Lapack.cungqr((int)m, (int)m, (int)minMN, QArr, (int)m, (fcomplex*)tau.GetHostPointerForWrite(), ref info);
                        if (m < n) {
                            ret.a = ret[full, r(0, m - 1)];
                        }
                    }
                    if (info != 0) {
                        throw new ArgumentException($"qr(): error in lapack function cungqr. info: {info}.");
                    }
                }
                return ret;
            }
        }
        /// <summary>
        /// QR decomposition with pivoting, potentially size saving shapes.
        /// </summary>
        /// <param name="A">Input matrix. Size [m x n].</param>
        /// <param name="R">[Output, optional] Upper triangular matrix. Size [m x n] or [min(m,n) x n] depending 
        /// on <paramref name="economySize"/> (see remarks). Default: (null) do not compute <paramref name="R"/>.</param>
        /// <param name="economySize">[Optional] True: return more efficient structures if possible. See remarks for details. Default: false.</param>
        /// <param name="E">[Output, optional] Permutation matrix from pivoting. Size [m x m]. Default: (null) do not return.<paramref name="E"/>.</param>
        /// <returns>Orthonormal / unitary matrix Q as result of decomposing <paramref name="A"/>. Size [m x m] or [m x min(m,n)], depending on <paramref name="economySize"/> (see remarks).</returns>
        /// <remarks>
        /// <para>This function performs the QR decomposition on matrix <paramref name="A"/>. It computes matrices Q, <paramref name="R"/> 
        /// and <paramref name="E"/>, with Q ** R = A ** E, where '**' denotes matrix multiplication.</para>
        /// <para>If <paramref name="R"/> is null on entry, the function returns the compact QR decomposition result from <see cref="qr(InArray{fcomplex})"/>.</para>
        /// <para>If <paramref name="economySize"/> is false, the function 
        /// returns Q, <paramref name="R"/> and <paramref name="E"/> such that the equation <code>A * E = Q * R</code> holds within 
        /// roundoff errors.</para>
        /// <para>If <paramref name="economySize"/> is true ... <list type="bullet">
        /// <item> and m &gt;= n the size of Q and <paramref name="R"/> will be [m x min(m,n)] and [min(m,n) x n] respectively. 
        /// For m &lt; n the sizes are not changed and as specified above.</item>
        /// <item>the output parameter <paramref name="E"/> is returned as vector [n] with permutation indices 
        /// rather than as a permutation matrix [n,n]. In this case the equation <code>A[":",E] == Q * R</code> holds, except roundoff errors.</item></list></para>
        /// <para><paramref name="E"/> reflects the pivoting of <paramref name="A"/> done inside the LAPACK function performing the decomposition so that <paramref name="R"/> 
        /// shows increasing diagonal element values. If <paramref name="E"/> is not requested (null) the equation Q ** R = A holds instead.</para></remarks>
        /// <seealso cref="qr(InArray{fcomplex}, OutArray{fcomplex}, bool)"/>
        /// <seealso cref="svd(InArray{fcomplex}, OutArray{fcomplex})"/>
        /// <seealso cref="eig(InArray{float}, OutArray{fcomplex}, ref MatrixProperties, bool)"/>
        public static Array<fcomplex> qr(
                        InArray<fcomplex> A,
                        OutArray<fcomplex> R = null,
                        OutArray<int> E = null,
                        bool economySize = false) {
            using (Scope.Enter()) {

                Array<fcomplex> _A = A;

                if (isnull(_A)) {
                    throw new ArgumentNullException("qr(A,R,E,...): input A must not be null.");
                }
                if (!_A.IsMatrix) {
                    throw new ArgumentException("qr(A,R,E,...): input A must be matrix.");
                }
                if (Object.Equals(R, null)) {
                    return qr(_A);
                }
                long m = _A.Size[0];
                long n = _A.Size[1];
                if (m < n && economySize) {
                    return qr(_A, R, false);
                }
                if (m == 0 || n == 0) {
                    lock (R.SynchObj)
                    if (economySize) {
                        if (!object.Equals(R, null)) {
                            R.a = zeros<fcomplex>(Math.Min(m,n), n);
                        }
                        if (!object.Equals(E, null)) {
                            E.a = zeros<int>(n, dim1: 1);
                        }
                        return empty<fcomplex>(m, Math.Min(m, n));
                    } else {
                        if (!object.Equals(R, null)) {
                            R.a = zeros<fcomplex>(m, n);
                        }
                        if (!object.Equals(E, null)) {
                            E.a = zeros<int>(n, n);
                        }
                        return empty<fcomplex>(m, m);
                    }
                }
                // shortcut w/o pivoting
                if (object.Equals(E, null)) {
                    return qr(_A, R, economySize);
                }

                var minMN = (m < n) ? m : n;
                Array<int> ipvt = zeros<int>(n, 1, StorageOrders.ColumnMajor); 
                Array<fcomplex> tau = empty<fcomplex>(minMN, StorageOrders.ColumnMajor);

                Array<fcomplex> ret;
                if (m >= n && !economySize) {
                    // ret will be [m x m], fill [m x n] with _A

                    ret = zeros<fcomplex>(m, m, StorageOrders.ColumnMajor);
                    System.Diagnostics.Debug.Assert(_A.IsMatrix); 
                    ret[full, r(0, n - 1)] = _A;

                } else {
                    // economy flag or economy size. 
                    // ret is [m x min(m,n)], filled with _A
                    // a temporary array is needed for extraction of the compact lapack Q (?geqrf)
                    ret = _A.C;
                }
                //ret[full, r(0, n - 1)] = _A[full, full];

                unsafe {
                    fcomplex* QArr = (fcomplex*)ret.GetHostPointerForWrite();
                    int info = 0;

                    Lapack.cgeqp3((int)m, (int)n, QArr, (int)m, (int*)ipvt.GetHostPointerForWrite(), (fcomplex*)tau.GetHostPointerForWrite(), ref info);

                    if (info != 0) {
                        throw new ArgumentException($"qr(): error inside lapack function cgeqp3. info:{info}.");
                    }

                    // extract R, Q
                    lock (R.SynchObj) {
                        if (economySize) {
                            R.a = copyUpperTriangle<fcomplex>(ret, minMN, n);

                            Lapack.cungqr((int)m, (int)minMN, (int)minMN, QArr, (int)m, (fcomplex*)tau.GetHostPointerForWrite(), ref info);

                            // transform E into out typed vector
                            E.a = ipvt - 1;

                        } else {
                            R.a = copyUpperTriangle<fcomplex>(ret, m, n);

                            Lapack.cungqr((int)m, (int)m, (int)minMN, QArr, (int)m, (fcomplex*)tau.GetHostPointerForWrite(), ref info);
                            if (m < n) {
                                ret.a = ret[full, r(0, m - 1)];
                            }
                            // transform E into matrix
                            E.a = zeros<int>(n, n);

                            using (Scope.Enter(arrayStyle: ArrayStyles.numpy)) {
                                // numpy advanced indexing helps in picking specific elements from the matrix E. But 
                                // make sure that all indices are vectors: 1-D only! (ipvt is likely [n,1] here!)
                                E[squeeze(ipvt) - 1, counter(0, 1, dim0: n)] = 1;
                            }
                        }
                    }
                    GC.KeepAlive(tau); 
                    if (info != 0) {
                        throw new ArgumentException($"qr(): error in lapack function cungqr. info: {info}.");
                    }
                    return ret;
                }
            }
        }

        /// <summary>
        /// QR decomposition - raw output as returned from LAPACK.
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <returns>Orthonormal / unitary matrix Q and upper triangular matrix R 
        /// packed into a single matrix. This is the output of the lapack function ?geqrf.</returns>
        /// <remarks><para>Input matrix <paramref name="A"/> is not altered. </para>
        /// <para>The matrix returned is the direct output of the lapack 
        /// function [d,s,c,z]geqrf, respectively. It contains the decomposition factors Q and R, 
        /// but they are combined into a single matrix. Overloads exist which return Q and R individually: 
        /// <see cref="qr(InArray{complex}, OutArray{complex}, bool)"/>.</para></remarks>
        public static Array<complex> qr(InArray<complex> A) {
            using (Scope.Enter()) {

                Array<complex> _A = A;

                if (isnull(_A)) {
                    throw new ArgumentNullException("qr(A): input A must not be null.");
                }
                if (!_A.IsMatrix) {
                    throw new ArgumentException("qr(A): input A must be matrix.");
                }
                long m = _A.Size[0], n = _A.Size[1];
                Array<complex> ret = _A.C;

                var minMN = (m < n) ? m : n;
                Array<complex> tau = empty<complex>(minMN); 
                int info = 0;
                unsafe {
                    Lapack.zgeqrf((int)m, (int)n, (complex*)ret.GetHostPointerForWrite(StorageOrders.ColumnMajor), (int)m, (complex*)tau.GetHostPointerForWrite(), ref info);
                }
                if (info < 0)
                    throw new ArgumentException($"qr(): unknown error in zgeqrf. Info: {info}.");
                return ret;
            }
        }
        /// <summary>
        /// QR decomposition, returning Q and <paramref name="R"/>, optionally economical sized.
        /// </summary>
        /// <param name="A">Input matrix. Size [m x n].</param>
        /// <param name="R">[Output, optional] Upper triangular matrix <paramref name="R"/>. Size [m x n]. Default: (null) same as <see cref="qr(InArray{complex})"/>.</param>
        /// <param name="economySize">[Optional] True: if m &gt;= n the size of Q and <paramref name="R"/> is reduced to [m x m] and [m x n] respectively. Default: false.</param>
        /// <returns>Orthonormal real / unitary complex matrix Q as result of decomposing <paramref name="A"/>. Size [m x m] or [m x min(m,n)] if <paramref name="economySize"/> is true or if A is empty.</returns>
        /// <remarks><para>The function returns Q and R such that the equation 
        /// <code>A = Q ** R</code> holds within the range of roundoff errors. ('**' 
        /// denotes matrix multiplication.)</para></remarks>
        public static Array<complex> qr(InArray<complex> A, OutArray<complex> R, bool economySize = false)
        {
            using (Scope.Enter()) {

                Array<complex> _A = A;

                if (isnull(_A)) {
                    throw new ArgumentNullException("qr(A,R...): input A must not be null.");
                }
                if (!_A.IsMatrix) {
                    throw new ArgumentException("qr(A,R...): input A must be matrix.");
                }
                if (Object.Equals(R, null)) {
                    return qr(_A);
                }
                long m = _A.Size[0];
                long n = _A.Size[1];
                if (m < n && economySize) {
                    economySize = false;
                }
                Array<complex> ret = (complex)0;
                if (m == 0 || n == 0) {
                    if (!object.Equals(R, null)) {
                        lock (R.SynchObj)
                            R.a = empty<complex>(m, n);
                    }
                    return empty<complex>(_A.Size[0], Math.Min(m, n));
                }
                var minMN = (m < n) ? m : n;

                Array<complex> tau = empty<complex>(minMN); ;

                if (m >= n) {
                    ret.a = zeros<complex>(m, (economySize) ? minMN : m);
                } else {
                    // economySize is always false ... !
                    // a temporary array is needed for extraction of the compact lapack Q (?geqrf)
                    ret.a = zeros<complex>(m, n);
                }
                ret[full, r(0, n - 1)] = _A[full, full];
                unsafe {

                    complex* QArr = (complex*)ret.GetHostPointerForWrite();

                    int info = 0;
                    Lapack.zgeqrf((int)m, (int)n, QArr, (int)m, (complex*)tau.GetHostPointerForWrite(), ref info);

                    if (info != 0) {
                        throw new ArgumentException($"qr(): error in lapack zgeqrf. info: {info}");
                    }
                    // extract R, Q
                    lock (R.SynchObj)
                    if (economySize) {
                        R.a = copyUpperTriangle<complex>(ret, minMN, n);

                        Lapack.zungqr((int)m, (int)minMN, (int)minMN, QArr, (int)m, (complex*)tau.GetHostPointerForWrite(), ref info);
                    } else {
                        R.a = copyUpperTriangle<complex>(ret, m, n);

                        Lapack.zungqr((int)m, (int)m, (int)minMN, QArr, (int)m, (complex*)tau.GetHostPointerForWrite(), ref info);
                        if (m < n) {
                            ret.a = ret[full, r(0, m - 1)];
                        }
                    }
                    if (info != 0) {
                        throw new ArgumentException($"qr(): error in lapack function zungqr. info: {info}.");
                    }
                }
                return ret;
            }
        }
        /// <summary>
        /// QR decomposition with pivoting, potentially size saving shapes.
        /// </summary>
        /// <param name="A">Input matrix. Size [m x n].</param>
        /// <param name="R">[Output, optional] Upper triangular matrix. Size [m x n] or [min(m,n) x n] depending 
        /// on <paramref name="economySize"/> (see remarks). Default: (null) do not compute <paramref name="R"/>.</param>
        /// <param name="economySize">[Optional] True: return more efficient structures if possible. See remarks for details. Default: false.</param>
        /// <param name="E">[Output, optional] Permutation matrix from pivoting. Size [m x m]. Default: (null) do not return.<paramref name="E"/>.</param>
        /// <returns>Orthonormal / unitary matrix Q as result of decomposing <paramref name="A"/>. Size [m x m] or [m x min(m,n)], depending on <paramref name="economySize"/> (see remarks).</returns>
        /// <remarks>
        /// <para>This function performs the QR decomposition on matrix <paramref name="A"/>. It computes matrices Q, <paramref name="R"/> 
        /// and <paramref name="E"/>, with Q ** R = A ** E, where '**' denotes matrix multiplication.</para>
        /// <para>If <paramref name="R"/> is null on entry, the function returns the compact QR decomposition result from <see cref="qr(InArray{complex})"/>.</para>
        /// <para>If <paramref name="economySize"/> is false, the function 
        /// returns Q, <paramref name="R"/> and <paramref name="E"/> such that the equation <code>A * E = Q * R</code> holds within 
        /// roundoff errors.</para>
        /// <para>If <paramref name="economySize"/> is true ... <list type="bullet">
        /// <item> and m &gt;= n the size of Q and <paramref name="R"/> will be [m x min(m,n)] and [min(m,n) x n] respectively. 
        /// For m &lt; n the sizes are not changed and as specified above.</item>
        /// <item>the output parameter <paramref name="E"/> is returned as vector [n] with permutation indices 
        /// rather than as a permutation matrix [n,n]. In this case the equation <code>A[":",E] == Q * R</code> holds, except roundoff errors.</item></list></para>
        /// <para><paramref name="E"/> reflects the pivoting of <paramref name="A"/> done inside the LAPACK function performing the decomposition so that <paramref name="R"/> 
        /// shows increasing diagonal element values. If <paramref name="E"/> is not requested (null) the equation Q ** R = A holds instead.</para></remarks>
        /// <seealso cref="qr(InArray{complex}, OutArray{complex}, bool)"/>
        /// <seealso cref="svd(InArray{complex}, OutArray{complex})"/>
        /// <seealso cref="eig(InArray{float}, OutArray{fcomplex}, ref MatrixProperties, bool)"/>
        public static Array<complex> qr(
                        InArray<complex> A,
                        OutArray<complex> R = null,
                        OutArray<int> E = null,
                        bool economySize = false) {
            using (Scope.Enter()) {

                Array<complex> _A = A;

                if (isnull(_A)) {
                    throw new ArgumentNullException("qr(A,R,E,...): input A must not be null.");
                }
                if (!_A.IsMatrix) {
                    throw new ArgumentException("qr(A,R,E,...): input A must be matrix.");
                }
                if (Object.Equals(R, null)) {
                    return qr(_A);
                }
                long m = _A.Size[0];
                long n = _A.Size[1];
                if (m < n && economySize) {
                    return qr(_A, R, false);
                }
                if (m == 0 || n == 0) {
                    lock (R.SynchObj)
                    if (economySize) {
                        if (!object.Equals(R, null)) {
                            R.a = zeros<complex>(Math.Min(m,n), n);
                        }
                        if (!object.Equals(E, null)) {
                            E.a = zeros<int>(n, dim1: 1);
                        }
                        return empty<complex>(m, Math.Min(m, n));
                    } else {
                        if (!object.Equals(R, null)) {
                            R.a = zeros<complex>(m, n);
                        }
                        if (!object.Equals(E, null)) {
                            E.a = zeros<int>(n, n);
                        }
                        return empty<complex>(m, m);
                    }
                }
                // shortcut w/o pivoting
                if (object.Equals(E, null)) {
                    return qr(_A, R, economySize);
                }

                var minMN = (m < n) ? m : n;
                Array<int> ipvt = zeros<int>(n, 1, StorageOrders.ColumnMajor); 
                Array<complex> tau = empty<complex>(minMN, StorageOrders.ColumnMajor);

                Array<complex> ret;
                if (m >= n && !economySize) {
                    // ret will be [m x m], fill [m x n] with _A

                    ret = zeros<complex>(m, m, StorageOrders.ColumnMajor);
                    System.Diagnostics.Debug.Assert(_A.IsMatrix); 
                    ret[full, r(0, n - 1)] = _A;

                } else {
                    // economy flag or economy size. 
                    // ret is [m x min(m,n)], filled with _A
                    // a temporary array is needed for extraction of the compact lapack Q (?geqrf)
                    ret = _A.C;
                }
                //ret[full, r(0, n - 1)] = _A[full, full];

                unsafe {
                    complex* QArr = (complex*)ret.GetHostPointerForWrite();
                    int info = 0;

                    Lapack.zgeqp3((int)m, (int)n, QArr, (int)m, (int*)ipvt.GetHostPointerForWrite(), (complex*)tau.GetHostPointerForWrite(), ref info);

                    if (info != 0) {
                        throw new ArgumentException($"qr(): error inside lapack function zgeqp3. info:{info}.");
                    }

                    // extract R, Q
                    lock (R.SynchObj) {
                        if (economySize) {
                            R.a = copyUpperTriangle<complex>(ret, minMN, n);

                            Lapack.zungqr((int)m, (int)minMN, (int)minMN, QArr, (int)m, (complex*)tau.GetHostPointerForWrite(), ref info);

                            // transform E into out typed vector
                            E.a = ipvt - 1;

                        } else {
                            R.a = copyUpperTriangle<complex>(ret, m, n);

                            Lapack.zungqr((int)m, (int)m, (int)minMN, QArr, (int)m, (complex*)tau.GetHostPointerForWrite(), ref info);
                            if (m < n) {
                                ret.a = ret[full, r(0, m - 1)];
                            }
                            // transform E into matrix
                            E.a = zeros<int>(n, n);

                            using (Scope.Enter(arrayStyle: ArrayStyles.numpy)) {
                                // numpy advanced indexing helps in picking specific elements from the matrix E. But 
                                // make sure that all indices are vectors: 1-D only! (ipvt is likely [n,1] here!)
                                E[squeeze(ipvt) - 1, counter(0, 1, dim0: n)] = 1;
                            }
                        }
                    }
                    GC.KeepAlive(tau); 
                    if (info != 0) {
                        throw new ArgumentException($"qr(): error in lapack function zungqr. info: {info}.");
                    }
                    return ret;
                }
            }
        }


#endregion HYCALPER AUTO GENERATED CODE

    }
}