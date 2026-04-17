//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////


using System;
using System.Collections.Generic;
using System.Text;
using ILNumerics;
using static ILNumerics.Globals;
using static ILNumerics.ILMath;

namespace ILNumerics  {

    public static partial class ILMath {

        #region HYCALPER LOOPSTART 
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
    <source locate="after">
        HycalpEPS
    </source>
    <destination>eps</destination>
    <destination>epsf</destination>
    <destination>epsf</destination>
</type>
<type>
    <source locate="here">
        dgesdd
    </source>
    <destination>zgesdd</destination>
    <destination>cgesdd</destination>
    <destination>sgesdd</destination>
</type>
<type>
    <source locate="nextline">
        multiplyCast
    </source>
    <destination>Ret = multiply(multiply(V[full, r(0,count-1)], tocomplex(S)), U);</destination>
    <destination>Ret = multiply(multiply(V[full, r(0,count-1)], tofcomplex(S)), U);</destination>
    <destination>Ret = multiply(multiply(V[full, r(0,count-1)], S), U);</destination>
</type>
<type>
    <source locate="nextline">
        Uconj
    </source>
    <destination>U = conj(U[full, r(0,count-1)].T);</destination>
    <destination>U = conj(U[full, r(0,count - 1)].T);</destination>
    <destination>U = U[full, r(0,count - 1)].T;</destination>
</type>
<type>
    <source locate="nextline">
        speedyexit
    </source>
    <destination><![CDATA[return conj(pinv(conj(A.T), tolerance)).T;]]></destination>
    <destination><![CDATA[return conj(pinv(conj(A.T), tolerance)).T;]]></destination>
    <destination><![CDATA[return pinv(A.T, tolerance).T;]]></destination>
</type>
</hycalper>
*/


        /// <summary>
        /// Moore-Penrose pseudo inverse of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <param name="tolerance">[Optional] Tolerance. Default: (null) - use default tolerance (see remarks).</param>
        /// <returns>Pseudo inverse of input matrix <paramref name="A"/></returns>
        /// <remarks>The function returns the pseudo inverse (Moore-Penrose pseudoinverse)
        /// of input matrix <paramref name="A"/>. The return value will be of the same size as <paramref name="A"/>.T. 
        /// It will satisfy the following conditions: 
        /// <list type="bullet">
        /// <item><paramref name="A"/> * pinv(<paramref name="A"/>) * <paramref name="A"/>  = <paramref name="A"/> </item>
        /// <item>pinv(<paramref name="A"/>) * <paramref name="A"/> * pinv(<paramref name="A"/>) = pinv(<paramref name="A"/>)</item>
        /// <item>pinv(<paramref name="A"/>) * <paramref name="A"/> is hermitian.</item>
        /// <item><paramref name="A"/> * pinv(<paramref name="A"/>) is hermitian.</item>
        /// </list>
        /// <see cref="pinv(InArray{double}, double?)"/> relies on <see cref="svd(InArray{complex})"/>, utilizing native Lapack functions internally. 
        /// Singular values less than <paramref name="tolerance"/> will be set to zero. As default tolerance the following equation is used: \\
        /// <c>tolerance = length(<paramref name="A"/>) * norm(<paramref name="A"/>) * eps</c>, where 
        /// <list type="bullet">
        /// <item>length(<paramref name="A"/>) - the longest dimension of <paramref name="A"/>.</item>
        /// <item>norm(<paramref name="A"/>) being the largest singular value of <paramref name="A"/>.</item>
        /// <item>eps - the smallest number greater than one.</item>
        /// </list>
        /// </remarks>
        /// <seealso cref="pinv(InArray{double}, double?)"/>
        /// <seealso cref="svd(InArray{double})"/>
        /// <seealso cref="eps"/>
        /// <seealso cref="norm(InArray{double}, Double)"/>
        public static Array<double> pinv(InArray<double> A, double? tolerance = null) {
            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {

                Array<double> _A = A; 

                if (!_A.IsMatrix) {
                    throw new ArgumentException($"pinv() requires a matrix as input argument. Found: {_A.S.ToString()}"); 
                }
                // in order to use the cheap packed version of svd, the matrix must be m x n with m > n! 
                if (_A.Size[0] < _A.Size[1]) {
                    /*!HC:speedyexit*/
                    return pinv(_A.T, tolerance).T;
                }

                if (_A.IsScalar) {
                    return (double)1.0 / _A;
                }
                if (_A.IsEmpty) {
                    return empty<double>(_A.T.S); 
                }
                Array<double> U = (double)0;
                Array<double> V = (double)0;
                Array<Double> S = svd(_A, U, V, true, false);

                var m = _A.Size[0];
                var n = _A.Size[1];

                Array<Double> s;
                switch (m) {
                    case 0:
                        s = zeros<Double>(1);
                        break;
                    case 1:
                        s = S[0];
                        break;
                    default:
                        s = diag(S);
                        break;
                }
                double tol;

                if (!tolerance.HasValue) {
                    tol = (double)(_A.Size.Longest * max(s).GetValue(0) * /*!HC:HycalpEPS*/ eps);
                } else {
                    tol = tolerance.GetValueOrDefault(); 
                }
                // sum vector elements: s is dense vector returned from svd
                var count = (s > (Double)tol).NumberTrues;

                Array<double> Ret = empty<double>(0, dim1: 0);
                if (count == 0)
                    S.a = zeros<Double>(n, m);
                else {
                    Array<Double> OneVec = array<Double>((Double)1.0, count, 1);

                    S.a = diag(divide(OneVec, s[r(0,count - 1), 0]));
                    /*!HC:Uconj*/
                    U.a = U[full,r(0,count-1)].T;
                    /*!HC:multiplyCast*/
                    Ret.a = multiply(multiply(V[full,r(0,count - 1)], S), U);
                }
                return Ret;
            }
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       


        /// <summary>
        /// Moore-Penrose pseudo inverse of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <param name="tolerance">[Optional] Tolerance. Default: (null) - use default tolerance (see remarks).</param>
        /// <returns>Pseudo inverse of input matrix <paramref name="A"/></returns>
        /// <remarks>The function returns the pseudo inverse (Moore-Penrose pseudoinverse)
        /// of input matrix <paramref name="A"/>. The return value will be of the same size as <paramref name="A"/>.T. 
        /// It will satisfy the following conditions: 
        /// <list type="bullet">
        /// <item><paramref name="A"/> * pinv(<paramref name="A"/>) * <paramref name="A"/>  = <paramref name="A"/> </item>
        /// <item>pinv(<paramref name="A"/>) * <paramref name="A"/> * pinv(<paramref name="A"/>) = pinv(<paramref name="A"/>)</item>
        /// <item>pinv(<paramref name="A"/>) * <paramref name="A"/> is hermitian.</item>
        /// <item><paramref name="A"/> * pinv(<paramref name="A"/>) is hermitian.</item>
        /// </list>
        /// <see cref="pinv(InArray{float}, float?)"/> relies on <see cref="svd(InArray{complex})"/>, utilizing native Lapack functions internally. 
        /// Singular values less than <paramref name="tolerance"/> will be set to zero. As default tolerance the following equation is used: \\
        /// <c>tolerance = length(<paramref name="A"/>) * norm(<paramref name="A"/>) * eps</c>, where 
        /// <list type="bullet">
        /// <item>length(<paramref name="A"/>) - the longest dimension of <paramref name="A"/>.</item>
        /// <item>norm(<paramref name="A"/>) being the largest singular value of <paramref name="A"/>.</item>
        /// <item>eps - the smallest number greater than one.</item>
        /// </list>
        /// </remarks>
        /// <seealso cref="pinv(InArray{float}, float?)"/>
        /// <seealso cref="svd(InArray{float})"/>
        /// <seealso cref="eps"/>
        /// <seealso cref="norm(InArray{float}, float)"/>
        public static Array<float> pinv(InArray<float> A, float? tolerance = null) {
            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {

                Array<float> _A = A; 

                if (!_A.IsMatrix) {
                    throw new ArgumentException($"pinv() requires a matrix as input argument. Found: {_A.S.ToString()}"); 
                }
                // in order to use the cheap packed version of svd, the matrix must be m x n with m > n! 
                if (_A.Size[0] < _A.Size[1]) {
                    return pinv(A.T, tolerance).T;
                }

                if (_A.IsScalar) {
                    return (float)1.0 / _A;
                }
                if (_A.IsEmpty) {
                    return empty<float>(_A.T.S); 
                }
                Array<float> U = (float)0;
                Array<float> V = (float)0;
                Array<float> S = svd(_A, U, V, true, false);

                var m = _A.Size[0];
                var n = _A.Size[1];

                Array<float> s;
                switch (m) {
                    case 0:
                        s = zeros<float>(1);
                        break;
                    case 1:
                        s = S[0];
                        break;
                    default:
                        s = diag(S);
                        break;
                }
                float tol;

                if (!tolerance.HasValue) {
                    tol = (float)(_A.Size.Longest * max(s).GetValue(0) *  epsf);
                } else {
                    tol = tolerance.GetValueOrDefault(); 
                }
                // sum vector elements: s is dense vector returned from svd
                var count = (s > (float)tol).NumberTrues;

                Array<float> Ret = empty<float>(0, dim1: 0);
                if (count == 0)
                    S.a = zeros<float>(n, m);
                else {
                    Array<float> OneVec = array<float>((float)1.0, count, 1);

                    S.a = diag(divide(OneVec, s[r(0,count - 1), 0]));
                    U = U[full, r(0,count - 1)].T;
                    Ret = multiply(multiply(V[full, r(0,count-1)], S), U);
                }
                return Ret;
            }
        }
       


        /// <summary>
        /// Moore-Penrose pseudo inverse of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <param name="tolerance">[Optional] Tolerance. Default: (null) - use default tolerance (see remarks).</param>
        /// <returns>Pseudo inverse of input matrix <paramref name="A"/></returns>
        /// <remarks>The function returns the pseudo inverse (Moore-Penrose pseudoinverse)
        /// of input matrix <paramref name="A"/>. The return value will be of the same size as <paramref name="A"/>.T. 
        /// It will satisfy the following conditions: 
        /// <list type="bullet">
        /// <item><paramref name="A"/> * pinv(<paramref name="A"/>) * <paramref name="A"/>  = <paramref name="A"/> </item>
        /// <item>pinv(<paramref name="A"/>) * <paramref name="A"/> * pinv(<paramref name="A"/>) = pinv(<paramref name="A"/>)</item>
        /// <item>pinv(<paramref name="A"/>) * <paramref name="A"/> is hermitian.</item>
        /// <item><paramref name="A"/> * pinv(<paramref name="A"/>) is hermitian.</item>
        /// </list>
        /// <see cref="pinv(InArray{fcomplex}, fcomplex?)"/> relies on <see cref="svd(InArray{complex})"/>, utilizing native Lapack functions internally. 
        /// Singular values less than <paramref name="tolerance"/> will be set to zero. As default tolerance the following equation is used: \\
        /// <c>tolerance = length(<paramref name="A"/>) * norm(<paramref name="A"/>) * eps</c>, where 
        /// <list type="bullet">
        /// <item>length(<paramref name="A"/>) - the longest dimension of <paramref name="A"/>.</item>
        /// <item>norm(<paramref name="A"/>) being the largest singular value of <paramref name="A"/>.</item>
        /// <item>eps - the smallest number greater than one.</item>
        /// </list>
        /// </remarks>
        /// <seealso cref="pinv(InArray{fcomplex}, fcomplex?)"/>
        /// <seealso cref="svd(InArray{fcomplex})"/>
        /// <seealso cref="eps"/>
        /// <seealso cref="norm(InArray{fcomplex}, float)"/>
        public static Array<fcomplex> pinv(InArray<fcomplex> A, fcomplex? tolerance = null) {
            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {

                Array<fcomplex> _A = A; 

                if (!_A.IsMatrix) {
                    throw new ArgumentException($"pinv() requires a matrix as input argument. Found: {_A.S.ToString()}"); 
                }
                // in order to use the cheap packed version of svd, the matrix must be m x n with m > n! 
                if (_A.Size[0] < _A.Size[1]) {
                    return conj(pinv(conj(A.T), tolerance)).T;
                }

                if (_A.IsScalar) {
                    return (fcomplex)1.0 / _A;
                }
                if (_A.IsEmpty) {
                    return empty<fcomplex>(_A.T.S); 
                }
                Array<fcomplex> U = (fcomplex)0;
                Array<fcomplex> V = (fcomplex)0;
                Array<float> S = svd(_A, U, V, true, false);

                var m = _A.Size[0];
                var n = _A.Size[1];

                Array<float> s;
                switch (m) {
                    case 0:
                        s = zeros<float>(1);
                        break;
                    case 1:
                        s = S[0];
                        break;
                    default:
                        s = diag(S);
                        break;
                }
                fcomplex tol;

                if (!tolerance.HasValue) {
                    tol = (fcomplex)(_A.Size.Longest * max(s).GetValue(0) *  epsf);
                } else {
                    tol = tolerance.GetValueOrDefault(); 
                }
                // sum vector elements: s is dense vector returned from svd
                var count = (s > (float)tol).NumberTrues;

                Array<fcomplex> Ret = empty<fcomplex>(0, dim1: 0);
                if (count == 0)
                    S.a = zeros<float>(n, m);
                else {
                    Array<float> OneVec = array<float>((float)1.0, count, 1);

                    S.a = diag(divide(OneVec, s[r(0,count - 1), 0]));
                    U = conj(U[full, r(0,count - 1)].T);
                    Ret = multiply(multiply(V[full, r(0,count-1)], tofcomplex(S)), U);
                }
                return Ret;
            }
        }
       


        /// <summary>
        /// Moore-Penrose pseudo inverse of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <param name="tolerance">[Optional] Tolerance. Default: (null) - use default tolerance (see remarks).</param>
        /// <returns>Pseudo inverse of input matrix <paramref name="A"/></returns>
        /// <remarks>The function returns the pseudo inverse (Moore-Penrose pseudoinverse)
        /// of input matrix <paramref name="A"/>. The return value will be of the same size as <paramref name="A"/>.T. 
        /// It will satisfy the following conditions: 
        /// <list type="bullet">
        /// <item><paramref name="A"/> * pinv(<paramref name="A"/>) * <paramref name="A"/>  = <paramref name="A"/> </item>
        /// <item>pinv(<paramref name="A"/>) * <paramref name="A"/> * pinv(<paramref name="A"/>) = pinv(<paramref name="A"/>)</item>
        /// <item>pinv(<paramref name="A"/>) * <paramref name="A"/> is hermitian.</item>
        /// <item><paramref name="A"/> * pinv(<paramref name="A"/>) is hermitian.</item>
        /// </list>
        /// <see cref="pinv(InArray{complex}, complex?)"/> relies on <see cref="svd(InArray{complex})"/>, utilizing native Lapack functions internally. 
        /// Singular values less than <paramref name="tolerance"/> will be set to zero. As default tolerance the following equation is used: \\
        /// <c>tolerance = length(<paramref name="A"/>) * norm(<paramref name="A"/>) * eps</c>, where 
        /// <list type="bullet">
        /// <item>length(<paramref name="A"/>) - the longest dimension of <paramref name="A"/>.</item>
        /// <item>norm(<paramref name="A"/>) being the largest singular value of <paramref name="A"/>.</item>
        /// <item>eps - the smallest number greater than one.</item>
        /// </list>
        /// </remarks>
        /// <seealso cref="pinv(InArray{complex}, complex?)"/>
        /// <seealso cref="svd(InArray{complex})"/>
        /// <seealso cref="eps"/>
        /// <seealso cref="norm(InArray{complex}, double)"/>
        public static Array<complex> pinv(InArray<complex> A, complex? tolerance = null) {
            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {

                Array<complex> _A = A; 

                if (!_A.IsMatrix) {
                    throw new ArgumentException($"pinv() requires a matrix as input argument. Found: {_A.S.ToString()}"); 
                }
                // in order to use the cheap packed version of svd, the matrix must be m x n with m > n! 
                if (_A.Size[0] < _A.Size[1]) {
                    return conj(pinv(conj(A.T), tolerance)).T;
                }

                if (_A.IsScalar) {
                    return (complex)1.0 / _A;
                }
                if (_A.IsEmpty) {
                    return empty<complex>(_A.T.S); 
                }
                Array<complex> U = (complex)0;
                Array<complex> V = (complex)0;
                Array<double> S = svd(_A, U, V, true, false);

                var m = _A.Size[0];
                var n = _A.Size[1];

                Array<double> s;
                switch (m) {
                    case 0:
                        s = zeros<double>(1);
                        break;
                    case 1:
                        s = S[0];
                        break;
                    default:
                        s = diag(S);
                        break;
                }
                complex tol;

                if (!tolerance.HasValue) {
                    tol = (complex)(_A.Size.Longest * max(s).GetValue(0) *  eps);
                } else {
                    tol = tolerance.GetValueOrDefault(); 
                }
                // sum vector elements: s is dense vector returned from svd
                var count = (s > (double)tol).NumberTrues;

                Array<complex> Ret = empty<complex>(0, dim1: 0);
                if (count == 0)
                    S.a = zeros<double>(n, m);
                else {
                    Array<double> OneVec = array<double>((double)1.0, count, 1);

                    S.a = diag(divide(OneVec, s[r(0,count - 1), 0]));
                    U = conj(U[full, r(0,count-1)].T);
                    Ret = multiply(multiply(V[full, r(0,count-1)], tocomplex(S)), U);
                }
                return Ret;
            }
        }

#endregion HYCALPER AUTO GENERATED CODE

    }
}
