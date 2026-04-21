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
using static ILNumerics.Core.Functions.Builtin.MathInternal; 

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
    <source locate="after">
        lapack_*potrf
    </source>
    <destination>Lapack.zpotrf</destination>
    <destination>Lapack.cpotrf</destination>
    <destination>Lapack.spotrf</destination>
</type>
</hycalper>
*/

namespace ILNumerics {

    public partial class ILMath {

        #region HYCALPER LOOPSTART 
        /// <summary>Cholesky factorization of a symmetric, positive definite matrix.</summary>
        /// <param name="A">Input matrix. <paramref name="A"/> must be a symmetric/hermitian matrix. </param>
        /// <param name="throwException">Throws an <see cref="ArgumentException"/> if <paramref name="A"/> is found to be not positive definite.</param>
        /// <returns>Cholesky factorization of <paramref name="A"/>.</returns>
        /// <remarks>
        /// <para>No check is performed on <paramref name="A"/>'s symmetry for performance reasons.</para>
        /// <para>If <paramref name="throwException"/> is true and 
        /// <paramref name="A"/> is found to be not positive definite an <see cref="ArgumentException"/> 
        /// is thrown and the operation is canceled.</para>
        /// <para>If <paramref name="throwException"/> is false, check the
        /// return value's dimension to determine the success of the 
        /// operation (unless you are sure, <paramref name="A"/> was positive definite). 
        /// If <paramref name="A"/> was found not to be positive definite the matrix returned 
        /// will be of dimension [k x k] and the result of the cholesky 
        /// factorization of A[0:k-1;0:k-1]. Here k is the first leading 
        /// minor of <paramref name="A"/> at which <paramref name="A"/> was found to be not positive definite.</para>
        /// <para>The factorization is carried out by use of the LAPACK functions 
        /// DPOTRF, ZPOTRF, SPOTRF or CPOTRF respectively.</para></remarks>
        public static Array<double> chol(InArray<double> A, bool throwException = true) {
            using (Scope.Enter()) {
                Array<double> _A = A;
                if (!_A.IsMatrix)
                    throw new ArgumentException("chol() requires an input matrix. A is not a matrix.");
                long n = _A.Size[0];
                int info = 0;
                if (_A.Size[1] != n)
                    throw new ArgumentException("chol: input matrix must be square!");
                Array<double> ret = _A.Storage.copyUpperTriangle(n);
                unsafe {
                    /*!HC:lapack_*potrf*/
                    Lapack.dpotrf('U', (int)n, (double*)ret.GetHostPointerForRead(), (int)n, ref info);
                    if (info < 0) {
                        throw new ArgumentException("chol: illegal parameter error:" + info);
                    } else if (info > 0) {
                        // not pos.definite 
                        if (!throwException) {
                            if (info > 1) {
                                int newDim = info - 1;
                                ret = _A.Storage.copyUpperTriangle(newDim);
                                /*!HC:lapack_*potrf*/
                                Lapack.dpotrf('U', (int)newDim, (double*)ret.GetHostPointerForRead(), (int)newDim, ref info);
                                if (info != 0)
                                    throw new ArgumentException("chol: the original matrix was not positiv definite. An attempt to decompose the submatrix of order " + (newDim + 1).ToString() + " failed for unknown reasons. Giving up.");
                                return ret;
                            } else {
                                return empty<double>(0);
                            }
                        } else {
                            throw new ArgumentException("chol: the input matrix was not positive definite!");
                        }
                    } else {
                        return ret; 
                    }
                }
            }
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
        /// <summary>Cholesky factorization of a symmetric, positive definite matrix.</summary>
        /// <param name="A">Input matrix. <paramref name="A"/> must be a symmetric/hermitian matrix. </param>
        /// <param name="throwException">Throws an <see cref="ArgumentException"/> if <paramref name="A"/> is found to be not positive definite.</param>
        /// <returns>Cholesky factorization of <paramref name="A"/>.</returns>
        /// <remarks>
        /// <para>No check is performed on <paramref name="A"/>'s symmetry for performance reasons.</para>
        /// <para>If <paramref name="throwException"/> is true and 
        /// <paramref name="A"/> is found to be not positive definite an <see cref="ArgumentException"/> 
        /// is thrown and the operation is canceled.</para>
        /// <para>If <paramref name="throwException"/> is false, check the
        /// return value's dimension to determine the success of the 
        /// operation (unless you are sure, <paramref name="A"/> was positive definite). 
        /// If <paramref name="A"/> was found not to be positive definite the matrix returned 
        /// will be of dimension [k x k] and the result of the cholesky 
        /// factorization of A[0:k-1;0:k-1]. Here k is the first leading 
        /// minor of <paramref name="A"/> at which <paramref name="A"/> was found to be not positive definite.</para>
        /// <para>The factorization is carried out by use of the LAPACK functions 
        /// DPOTRF, ZPOTRF, SPOTRF or CPOTRF respectively.</para></remarks>
        public static Array<float> chol(InArray<float> A, bool throwException = true) {
            using (Scope.Enter()) {
                Array<float> _A = A;
                if (!_A.IsMatrix)
                    throw new ArgumentException("chol() requires an input matrix. A is not a matrix.");
                long n = _A.Size[0];
                int info = 0;
                if (_A.Size[1] != n)
                    throw new ArgumentException("chol: input matrix must be square!");
                Array<float> ret = _A.Storage.copyUpperTriangle(n);
                unsafe {
                   
                    Lapack.spotrf('U', (int)n, (float*)ret.GetHostPointerForRead(), (int)n, ref info);
                    if (info < 0) {
                        throw new ArgumentException("chol: illegal parameter error:" + info);
                    } else if (info > 0) {
                        // not pos.definite 
                        if (!throwException) {
                            if (info > 1) {
                                int newDim = info - 1;
                                ret = _A.Storage.copyUpperTriangle(newDim);
                               
                                Lapack.spotrf('U', (int)newDim, (float*)ret.GetHostPointerForRead(), (int)newDim, ref info);
                                if (info != 0)
                                    throw new ArgumentException("chol: the original matrix was not positiv definite. An attempt to decompose the submatrix of order " + (newDim + 1).ToString() + " failed for unknown reasons. Giving up.");
                                return ret;
                            } else {
                                return empty<float>(0);
                            }
                        } else {
                            throw new ArgumentException("chol: the input matrix was not positive definite!");
                        }
                    } else {
                        return ret; 
                    }
                }
            }
        }
        /// <summary>Cholesky factorization of a symmetric, positive definite matrix.</summary>
        /// <param name="A">Input matrix. <paramref name="A"/> must be a symmetric/hermitian matrix. </param>
        /// <param name="throwException">Throws an <see cref="ArgumentException"/> if <paramref name="A"/> is found to be not positive definite.</param>
        /// <returns>Cholesky factorization of <paramref name="A"/>.</returns>
        /// <remarks>
        /// <para>No check is performed on <paramref name="A"/>'s symmetry for performance reasons.</para>
        /// <para>If <paramref name="throwException"/> is true and 
        /// <paramref name="A"/> is found to be not positive definite an <see cref="ArgumentException"/> 
        /// is thrown and the operation is canceled.</para>
        /// <para>If <paramref name="throwException"/> is false, check the
        /// return value's dimension to determine the success of the 
        /// operation (unless you are sure, <paramref name="A"/> was positive definite). 
        /// If <paramref name="A"/> was found not to be positive definite the matrix returned 
        /// will be of dimension [k x k] and the result of the cholesky 
        /// factorization of A[0:k-1;0:k-1]. Here k is the first leading 
        /// minor of <paramref name="A"/> at which <paramref name="A"/> was found to be not positive definite.</para>
        /// <para>The factorization is carried out by use of the LAPACK functions 
        /// DPOTRF, ZPOTRF, SPOTRF or CPOTRF respectively.</para></remarks>
        public static Array<fcomplex> chol(InArray<fcomplex> A, bool throwException = true) {
            using (Scope.Enter()) {
                Array<fcomplex> _A = A;
                if (!_A.IsMatrix)
                    throw new ArgumentException("chol() requires an input matrix. A is not a matrix.");
                long n = _A.Size[0];
                int info = 0;
                if (_A.Size[1] != n)
                    throw new ArgumentException("chol: input matrix must be square!");
                Array<fcomplex> ret = _A.Storage.copyUpperTriangle(n);
                unsafe {
                   
                    Lapack.cpotrf('U', (int)n, (fcomplex*)ret.GetHostPointerForRead(), (int)n, ref info);
                    if (info < 0) {
                        throw new ArgumentException("chol: illegal parameter error:" + info);
                    } else if (info > 0) {
                        // not pos.definite 
                        if (!throwException) {
                            if (info > 1) {
                                int newDim = info - 1;
                                ret = _A.Storage.copyUpperTriangle(newDim);
                               
                                Lapack.cpotrf('U', (int)newDim, (fcomplex*)ret.GetHostPointerForRead(), (int)newDim, ref info);
                                if (info != 0)
                                    throw new ArgumentException("chol: the original matrix was not positiv definite. An attempt to decompose the submatrix of order " + (newDim + 1).ToString() + " failed for unknown reasons. Giving up.");
                                return ret;
                            } else {
                                return empty<fcomplex>(0);
                            }
                        } else {
                            throw new ArgumentException("chol: the input matrix was not positive definite!");
                        }
                    } else {
                        return ret; 
                    }
                }
            }
        }
        /// <summary>Cholesky factorization of a symmetric, positive definite matrix.</summary>
        /// <param name="A">Input matrix. <paramref name="A"/> must be a symmetric/hermitian matrix. </param>
        /// <param name="throwException">Throws an <see cref="ArgumentException"/> if <paramref name="A"/> is found to be not positive definite.</param>
        /// <returns>Cholesky factorization of <paramref name="A"/>.</returns>
        /// <remarks>
        /// <para>No check is performed on <paramref name="A"/>'s symmetry for performance reasons.</para>
        /// <para>If <paramref name="throwException"/> is true and 
        /// <paramref name="A"/> is found to be not positive definite an <see cref="ArgumentException"/> 
        /// is thrown and the operation is canceled.</para>
        /// <para>If <paramref name="throwException"/> is false, check the
        /// return value's dimension to determine the success of the 
        /// operation (unless you are sure, <paramref name="A"/> was positive definite). 
        /// If <paramref name="A"/> was found not to be positive definite the matrix returned 
        /// will be of dimension [k x k] and the result of the cholesky 
        /// factorization of A[0:k-1;0:k-1]. Here k is the first leading 
        /// minor of <paramref name="A"/> at which <paramref name="A"/> was found to be not positive definite.</para>
        /// <para>The factorization is carried out by use of the LAPACK functions 
        /// DPOTRF, ZPOTRF, SPOTRF or CPOTRF respectively.</para></remarks>
        public static Array<complex> chol(InArray<complex> A, bool throwException = true) {
            using (Scope.Enter()) {
                Array<complex> _A = A;
                if (!_A.IsMatrix)
                    throw new ArgumentException("chol() requires an input matrix. A is not a matrix.");
                long n = _A.Size[0];
                int info = 0;
                if (_A.Size[1] != n)
                    throw new ArgumentException("chol: input matrix must be square!");
                Array<complex> ret = _A.Storage.copyUpperTriangle(n);
                unsafe {
                   
                    Lapack.zpotrf('U', (int)n, (complex*)ret.GetHostPointerForRead(), (int)n, ref info);
                    if (info < 0) {
                        throw new ArgumentException("chol: illegal parameter error:" + info);
                    } else if (info > 0) {
                        // not pos.definite 
                        if (!throwException) {
                            if (info > 1) {
                                int newDim = info - 1;
                                ret = _A.Storage.copyUpperTriangle(newDim);
                               
                                Lapack.zpotrf('U', (int)newDim, (complex*)ret.GetHostPointerForRead(), (int)newDim, ref info);
                                if (info != 0)
                                    throw new ArgumentException("chol: the original matrix was not positiv definite. An attempt to decompose the submatrix of order " + (newDim + 1).ToString() + " failed for unknown reasons. Giving up.");
                                return ret;
                            } else {
                                return empty<complex>(0);
                            }
                        } else {
                            throw new ArgumentException("chol: the input matrix was not positive definite!");
                        }
                    } else {
                        return ret; 
                    }
                }
            }
        }

#endregion HYCALPER AUTO GENERATED CODE

    }
}
