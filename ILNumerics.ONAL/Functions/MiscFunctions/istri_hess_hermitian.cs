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
using System.Text;
using ILNumerics; 

/*!HC:TYPELIST:
<hycalper>
<type>
    <source locate="here">
        double
    </source>
    <destination>float</destination>
    <destination>Int32</destination>
    <destination>Int64</destination>
    <destination>byte</destination>
    <destination>complex</destination>
    <destination>fcomplex</destination>
</type>
<type>
    <source locate="after">
        testReal
    </source>
    <destination></destination>
    <destination></destination>
    <destination></destination>
    <destination></destination>
    <destination>if (A.GetValue(c,c).imag != 0.0) return false;</destination>
    <destination>if (A.GetValue(c,c).imag != 0.0f) return false; </destination>
</type>
<type>
    <source locate="nextline">
        compareComplx
    </source>
    <destination>if (A.GetValue(r,c) != A.GetValue(c,r)) return false; </destination>
    <destination>if (A.GetValue(r,c) != A.GetValue(c,r)) return false; </destination>
    <destination>if (A.GetValue(r,c) != A.GetValue(c,r)) return false; </destination>
    <destination>if (A.GetValue(r,c) != A.GetValue(c,r)) return false; </destination>
    <destination>complex val1 = A.GetValue(r,c); complex val2 = A.GetValue(c,r); if (val1.real != val2.real || val1.imag + val2.imag != 0.0) return false;</destination>
    <destination>fcomplex val1 = A.GetValue(r,c); fcomplex val2 = A.GetValue(c,r); if (val1.real != val2.real || val1.imag + val2.imag != 0.0f) return false;</destination>
</type>
</hycalper>
*/

namespace ILNumerics.Core.Functions.Builtin {
    internal partial class MathInternal {

        #region HYCALPER LOOPSTART 
        /// <summary>
        /// Determines if matrix <paramref name="A"/> is a lower triangular matrix.
        /// </summary>
        /// <param name="A">Matrix of numeric element type.</param>
        /// <returns>true if <paramref name="A"/> is a lower triangular matrix, false otherwise.</returns>
        /// <exception cref="ArgumentException">if <paramref name="A"/> was null or not a matrix.</exception>
        internal static bool istrilow(InArray<double> A) {
            if (object.Equals(A, null))
                throw new ArgumentException("istrilow: A must not be null!");
            using (Scope.Enter()) {
                Array<double> A_ = A; 
                if (A_.IsScalar) { return true; }
                if (A_.IsEmpty) { return false; }
                if (!A_.IsMatrix)
                    throw new ArgumentException("istrilow: A_ must be a matrix or a scalar array!");
                long n = A_.Size[1];
                for (long c = 1; c < n; c++) {
                    for (long r = 0; r < c; r++) {
                        if (A_.GetValue(r, c) != (double)0) return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Determines if matrix <paramref name="A"/> is an upper triangular matrix.
        /// </summary>
        /// <param name="A">Matrix or scalar array <paramref name="A"/>, numeric element type.</param>
        /// <returns>true if <paramref name="A"/> is an upper triangular matrix, false otherwise.</returns>
        /// <exception cref="ArgumentException">if <paramref name="A"/> was null or not a matrix.</exception>
        internal static bool istriup(InArray<double> A) {
            if (object.Equals(A, null))
                throw new ArgumentException("istriup: A must not be null!");
            using (Scope.Enter()) {
                Array<double> A_ = A;
                if (A_.IsScalar) { return true; }
                if (A_.IsEmpty) { return false; }
                if (!A_.IsMatrix)
                    throw new ArgumentException("istriup: A_ must be a matrix or a scalar array!");
                long m = A_.Size[0];
                long n = A_.Size[1];
                for (long c = 0; c < n; c++) {
                    for (long r = c + 1; r < m; r++) {
                        if (A_.GetValue(r, c) != (double)0) return false;
                    }
                }
                return true;
            }
        }
        /// <summary>
        /// Determines if matrix <paramref name="A"/> is a lower Hessenberg matrix. 
        /// </summary>
        /// <param name="A">Matrix or scalar of numeric element type.</param>
        /// <returns>true if <paramref name="A"/> is a lower Hessenberg matrix, false otherwise.</returns>
        /// <exception cref="ArgumentException">if <paramref name="A"/> was null.</exception>
        internal static bool ishesslow(InArray<double> A) {
            if (object.Equals(A, null))
                throw new ArgumentException("ishesslow: A must not be null!");
            using (Scope.Enter()) {
                Array<double> A_ = A;
                if (A_.IsScalar) { return true; }
                if (A_.IsEmpty) { return false; }
                if (!A_.IsMatrix) return false;
                long m = A_.Size[0];
                long n = A_.Size[1];
                if (m != n) return false;
                for (long c = 2; c < n; c++) {
                    for (long r = 0; r < c - 1; r++) {
                        if (A_.GetValue(r, c) != (double)0) return false;
                    }
                }
                return true;
            }
        }
        /// <summary>
        /// Determines if matrix <paramref name="A"/> is an upper Hessenberg matrix.
        /// </summary>
        /// <param name="A">Matrix or scalar array, numeric element type.</param>
        /// <returns>true if <paramref name="A"/> is an upper Hessenberg matrix, false otherwise.</returns>
        /// <exception cref="ArgumentException">if <paramref name="A"/> was null.</exception>
        internal static bool ishessup(InArray<double> A) {
            if (object.Equals(A, null))
                throw new ArgumentException("ishessup: A must not be null!");
            using (Scope.Enter()) {
                Array<double> A_ = A;
                if (A_.IsScalar) { return true; }
                if (A_.IsEmpty) { return false; }
                if (!A_.IsMatrix) return false;
                long n = A_.Size[1];
                if (n != A_.Size[0]) return false;
                for (long c = 0; c < n - 2; c++) {
                    for (long r = c + 2; r < n; r++) {
                        if (A_.GetValue(r, c) != (double)0) return false;
                    }
                }
                return true;
            }
        }
        /// <summary>
        /// Determines if matrix <paramref name="A"/> is a hermitian matrix.
        /// </summary>
        /// <param name="A">Matrix or scalar array, numeric element type.</param>
        /// <returns>true if A is a hermitian matrix, false otherwise.</returns>
        /// <exception cref="ArgumentException">if A was null.</exception>
        internal static bool ishermitian(InArray<double> A) {
            if (object.Equals(A,null))
                throw new ArgumentException("ishessup: A must not be null!"); 
            using (Scope.Enter()) {
                Array<double> A_ = A;
                if (A_.IsScalar) { return true; }
                if (A_.IsEmpty) { return false; }
                if (!A_.IsMatrix) return false;
                long n = A_.Size[1];
                if (n != A_.Size[0]) return false;
                for (long c = 0; c < n; c++) {
                    /*!HC:testReal*/
                    //
                    for (long r = c + 1; r < n; r++) {
                        /*!HC:compareComplx*/
                        if (A_.GetValue(r, c) != A_.GetValue(c, r)) return false;
                    }
                }
                return true;
            }
        }
        #endregion HYCALPER LOOPEND 
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
        /// <summary>
        /// Determines if matrix <paramref name="A"/> is a lower triangular matrix.
        /// </summary>
        /// <param name="A">Matrix of numeric element type.</param>
        /// <returns>true if <paramref name="A"/> is a lower triangular matrix, false otherwise.</returns>
        /// <exception cref="ArgumentException">if <paramref name="A"/> was null or not a matrix.</exception>
        internal static bool istrilow(InArray<fcomplex> A) {
            if (object.Equals(A, null))
                throw new ArgumentException("istrilow: A must not be null!");
            using (Scope.Enter()) {
                Array<fcomplex> A_ = A; 
                if (A_.IsScalar) { return true; }
                if (A_.IsEmpty) { return false; }
                if (!A_.IsMatrix)
                    throw new ArgumentException("istrilow: A_ must be a matrix or a scalar array!");
                long n = A_.Size[1];
                for (long c = 1; c < n; c++) {
                    for (long r = 0; r < c; r++) {
                        if (A_.GetValue(r, c) != (fcomplex)0) return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Determines if matrix <paramref name="A"/> is an upper triangular matrix.
        /// </summary>
        /// <param name="A">Matrix or scalar array <paramref name="A"/>, numeric element type.</param>
        /// <returns>true if <paramref name="A"/> is an upper triangular matrix, false otherwise.</returns>
        /// <exception cref="ArgumentException">if <paramref name="A"/> was null or not a matrix.</exception>
        internal static bool istriup(InArray<fcomplex> A) {
            if (object.Equals(A, null))
                throw new ArgumentException("istriup: A must not be null!");
            using (Scope.Enter()) {
                Array<fcomplex> A_ = A;
                if (A_.IsScalar) { return true; }
                if (A_.IsEmpty) { return false; }
                if (!A_.IsMatrix)
                    throw new ArgumentException("istriup: A_ must be a matrix or a scalar array!");
                long m = A_.Size[0];
                long n = A_.Size[1];
                for (long c = 0; c < n; c++) {
                    for (long r = c + 1; r < m; r++) {
                        if (A_.GetValue(r, c) != (fcomplex)0) return false;
                    }
                }
                return true;
            }
        }
        /// <summary>
        /// Determines if matrix <paramref name="A"/> is a lower Hessenberg matrix. 
        /// </summary>
        /// <param name="A">Matrix or scalar of numeric element type.</param>
        /// <returns>true if <paramref name="A"/> is a lower Hessenberg matrix, false otherwise.</returns>
        /// <exception cref="ArgumentException">if <paramref name="A"/> was null.</exception>
        internal static bool ishesslow(InArray<fcomplex> A) {
            if (object.Equals(A, null))
                throw new ArgumentException("ishesslow: A must not be null!");
            using (Scope.Enter()) {
                Array<fcomplex> A_ = A;
                if (A_.IsScalar) { return true; }
                if (A_.IsEmpty) { return false; }
                if (!A_.IsMatrix) return false;
                long m = A_.Size[0];
                long n = A_.Size[1];
                if (m != n) return false;
                for (long c = 2; c < n; c++) {
                    for (long r = 0; r < c - 1; r++) {
                        if (A_.GetValue(r, c) != (fcomplex)0) return false;
                    }
                }
                return true;
            }
        }
        /// <summary>
        /// Determines if matrix <paramref name="A"/> is an upper Hessenberg matrix.
        /// </summary>
        /// <param name="A">Matrix or scalar array, numeric element type.</param>
        /// <returns>true if <paramref name="A"/> is an upper Hessenberg matrix, false otherwise.</returns>
        /// <exception cref="ArgumentException">if <paramref name="A"/> was null.</exception>
        internal static bool ishessup(InArray<fcomplex> A) {
            if (object.Equals(A, null))
                throw new ArgumentException("ishessup: A must not be null!");
            using (Scope.Enter()) {
                Array<fcomplex> A_ = A;
                if (A_.IsScalar) { return true; }
                if (A_.IsEmpty) { return false; }
                if (!A_.IsMatrix) return false;
                long n = A_.Size[1];
                if (n != A_.Size[0]) return false;
                for (long c = 0; c < n - 2; c++) {
                    for (long r = c + 2; r < n; r++) {
                        if (A_.GetValue(r, c) != (fcomplex)0) return false;
                    }
                }
                return true;
            }
        }
        /// <summary>
        /// Determines if matrix <paramref name="A"/> is a hermitian matrix.
        /// </summary>
        /// <param name="A">Matrix or scalar array, numeric element type.</param>
        /// <returns>true if A is a hermitian matrix, false otherwise.</returns>
        /// <exception cref="ArgumentException">if A was null.</exception>
        internal static bool ishermitian(InArray<fcomplex> A) {
            if (object.Equals(A,null))
                throw new ArgumentException("ishessup: A must not be null!"); 
            using (Scope.Enter()) {
                Array<fcomplex> A_ = A;
                if (A_.IsScalar) { return true; }
                if (A_.IsEmpty) { return false; }
                if (!A_.IsMatrix) return false;
                long n = A_.Size[1];
                if (n != A_.Size[0]) return false;
                for (long c = 0; c < n; c++) {
                   
                    if (A.GetValue(c,c).imag != 0.0f) return false; 
                    for (long r = c + 1; r < n; r++) {
                        fcomplex val1 = A.GetValue(r,c); fcomplex val2 = A.GetValue(c,r); if (val1.real != val2.real || val1.imag + val2.imag != 0.0f) return false;
                    }
                }
                return true;
            }
        }
        /// <summary>
        /// Determines if matrix <paramref name="A"/> is a lower triangular matrix.
        /// </summary>
        /// <param name="A">Matrix of numeric element type.</param>
        /// <returns>true if <paramref name="A"/> is a lower triangular matrix, false otherwise.</returns>
        /// <exception cref="ArgumentException">if <paramref name="A"/> was null or not a matrix.</exception>
        internal static bool istrilow(InArray<complex> A) {
            if (object.Equals(A, null))
                throw new ArgumentException("istrilow: A must not be null!");
            using (Scope.Enter()) {
                Array<complex> A_ = A; 
                if (A_.IsScalar) { return true; }
                if (A_.IsEmpty) { return false; }
                if (!A_.IsMatrix)
                    throw new ArgumentException("istrilow: A_ must be a matrix or a scalar array!");
                long n = A_.Size[1];
                for (long c = 1; c < n; c++) {
                    for (long r = 0; r < c; r++) {
                        if (A_.GetValue(r, c) != (complex)0) return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Determines if matrix <paramref name="A"/> is an upper triangular matrix.
        /// </summary>
        /// <param name="A">Matrix or scalar array <paramref name="A"/>, numeric element type.</param>
        /// <returns>true if <paramref name="A"/> is an upper triangular matrix, false otherwise.</returns>
        /// <exception cref="ArgumentException">if <paramref name="A"/> was null or not a matrix.</exception>
        internal static bool istriup(InArray<complex> A) {
            if (object.Equals(A, null))
                throw new ArgumentException("istriup: A must not be null!");
            using (Scope.Enter()) {
                Array<complex> A_ = A;
                if (A_.IsScalar) { return true; }
                if (A_.IsEmpty) { return false; }
                if (!A_.IsMatrix)
                    throw new ArgumentException("istriup: A_ must be a matrix or a scalar array!");
                long m = A_.Size[0];
                long n = A_.Size[1];
                for (long c = 0; c < n; c++) {
                    for (long r = c + 1; r < m; r++) {
                        if (A_.GetValue(r, c) != (complex)0) return false;
                    }
                }
                return true;
            }
        }
        /// <summary>
        /// Determines if matrix <paramref name="A"/> is a lower Hessenberg matrix. 
        /// </summary>
        /// <param name="A">Matrix or scalar of numeric element type.</param>
        /// <returns>true if <paramref name="A"/> is a lower Hessenberg matrix, false otherwise.</returns>
        /// <exception cref="ArgumentException">if <paramref name="A"/> was null.</exception>
        internal static bool ishesslow(InArray<complex> A) {
            if (object.Equals(A, null))
                throw new ArgumentException("ishesslow: A must not be null!");
            using (Scope.Enter()) {
                Array<complex> A_ = A;
                if (A_.IsScalar) { return true; }
                if (A_.IsEmpty) { return false; }
                if (!A_.IsMatrix) return false;
                long m = A_.Size[0];
                long n = A_.Size[1];
                if (m != n) return false;
                for (long c = 2; c < n; c++) {
                    for (long r = 0; r < c - 1; r++) {
                        if (A_.GetValue(r, c) != (complex)0) return false;
                    }
                }
                return true;
            }
        }
        /// <summary>
        /// Determines if matrix <paramref name="A"/> is an upper Hessenberg matrix.
        /// </summary>
        /// <param name="A">Matrix or scalar array, numeric element type.</param>
        /// <returns>true if <paramref name="A"/> is an upper Hessenberg matrix, false otherwise.</returns>
        /// <exception cref="ArgumentException">if <paramref name="A"/> was null.</exception>
        internal static bool ishessup(InArray<complex> A) {
            if (object.Equals(A, null))
                throw new ArgumentException("ishessup: A must not be null!");
            using (Scope.Enter()) {
                Array<complex> A_ = A;
                if (A_.IsScalar) { return true; }
                if (A_.IsEmpty) { return false; }
                if (!A_.IsMatrix) return false;
                long n = A_.Size[1];
                if (n != A_.Size[0]) return false;
                for (long c = 0; c < n - 2; c++) {
                    for (long r = c + 2; r < n; r++) {
                        if (A_.GetValue(r, c) != (complex)0) return false;
                    }
                }
                return true;
            }
        }
        /// <summary>
        /// Determines if matrix <paramref name="A"/> is a hermitian matrix.
        /// </summary>
        /// <param name="A">Matrix or scalar array, numeric element type.</param>
        /// <returns>true if A is a hermitian matrix, false otherwise.</returns>
        /// <exception cref="ArgumentException">if A was null.</exception>
        internal static bool ishermitian(InArray<complex> A) {
            if (object.Equals(A,null))
                throw new ArgumentException("ishessup: A must not be null!"); 
            using (Scope.Enter()) {
                Array<complex> A_ = A;
                if (A_.IsScalar) { return true; }
                if (A_.IsEmpty) { return false; }
                if (!A_.IsMatrix) return false;
                long n = A_.Size[1];
                if (n != A_.Size[0]) return false;
                for (long c = 0; c < n; c++) {
                   
                    if (A.GetValue(c,c).imag != 0.0) return false;
                    for (long r = c + 1; r < n; r++) {
                        complex val1 = A.GetValue(r,c); complex val2 = A.GetValue(c,r); if (val1.real != val2.real || val1.imag + val2.imag != 0.0) return false;
                    }
                }
                return true;
            }
        }
        /// <summary>
        /// Determines if matrix <paramref name="A"/> is a lower triangular matrix.
        /// </summary>
        /// <param name="A">Matrix of numeric element type.</param>
        /// <returns>true if <paramref name="A"/> is a lower triangular matrix, false otherwise.</returns>
        /// <exception cref="ArgumentException">if <paramref name="A"/> was null or not a matrix.</exception>
        internal static bool istrilow(InArray<byte> A) {
            if (object.Equals(A, null))
                throw new ArgumentException("istrilow: A must not be null!");
            using (Scope.Enter()) {
                Array<byte> A_ = A; 
                if (A_.IsScalar) { return true; }
                if (A_.IsEmpty) { return false; }
                if (!A_.IsMatrix)
                    throw new ArgumentException("istrilow: A_ must be a matrix or a scalar array!");
                long n = A_.Size[1];
                for (long c = 1; c < n; c++) {
                    for (long r = 0; r < c; r++) {
                        if (A_.GetValue(r, c) != (byte)0) return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Determines if matrix <paramref name="A"/> is an upper triangular matrix.
        /// </summary>
        /// <param name="A">Matrix or scalar array <paramref name="A"/>, numeric element type.</param>
        /// <returns>true if <paramref name="A"/> is an upper triangular matrix, false otherwise.</returns>
        /// <exception cref="ArgumentException">if <paramref name="A"/> was null or not a matrix.</exception>
        internal static bool istriup(InArray<byte> A) {
            if (object.Equals(A, null))
                throw new ArgumentException("istriup: A must not be null!");
            using (Scope.Enter()) {
                Array<byte> A_ = A;
                if (A_.IsScalar) { return true; }
                if (A_.IsEmpty) { return false; }
                if (!A_.IsMatrix)
                    throw new ArgumentException("istriup: A_ must be a matrix or a scalar array!");
                long m = A_.Size[0];
                long n = A_.Size[1];
                for (long c = 0; c < n; c++) {
                    for (long r = c + 1; r < m; r++) {
                        if (A_.GetValue(r, c) != (byte)0) return false;
                    }
                }
                return true;
            }
        }
        /// <summary>
        /// Determines if matrix <paramref name="A"/> is a lower Hessenberg matrix. 
        /// </summary>
        /// <param name="A">Matrix or scalar of numeric element type.</param>
        /// <returns>true if <paramref name="A"/> is a lower Hessenberg matrix, false otherwise.</returns>
        /// <exception cref="ArgumentException">if <paramref name="A"/> was null.</exception>
        internal static bool ishesslow(InArray<byte> A) {
            if (object.Equals(A, null))
                throw new ArgumentException("ishesslow: A must not be null!");
            using (Scope.Enter()) {
                Array<byte> A_ = A;
                if (A_.IsScalar) { return true; }
                if (A_.IsEmpty) { return false; }
                if (!A_.IsMatrix) return false;
                long m = A_.Size[0];
                long n = A_.Size[1];
                if (m != n) return false;
                for (long c = 2; c < n; c++) {
                    for (long r = 0; r < c - 1; r++) {
                        if (A_.GetValue(r, c) != (byte)0) return false;
                    }
                }
                return true;
            }
        }
        /// <summary>
        /// Determines if matrix <paramref name="A"/> is an upper Hessenberg matrix.
        /// </summary>
        /// <param name="A">Matrix or scalar array, numeric element type.</param>
        /// <returns>true if <paramref name="A"/> is an upper Hessenberg matrix, false otherwise.</returns>
        /// <exception cref="ArgumentException">if <paramref name="A"/> was null.</exception>
        internal static bool ishessup(InArray<byte> A) {
            if (object.Equals(A, null))
                throw new ArgumentException("ishessup: A must not be null!");
            using (Scope.Enter()) {
                Array<byte> A_ = A;
                if (A_.IsScalar) { return true; }
                if (A_.IsEmpty) { return false; }
                if (!A_.IsMatrix) return false;
                long n = A_.Size[1];
                if (n != A_.Size[0]) return false;
                for (long c = 0; c < n - 2; c++) {
                    for (long r = c + 2; r < n; r++) {
                        if (A_.GetValue(r, c) != (byte)0) return false;
                    }
                }
                return true;
            }
        }
        /// <summary>
        /// Determines if matrix <paramref name="A"/> is a hermitian matrix.
        /// </summary>
        /// <param name="A">Matrix or scalar array, numeric element type.</param>
        /// <returns>true if A is a hermitian matrix, false otherwise.</returns>
        /// <exception cref="ArgumentException">if A was null.</exception>
        internal static bool ishermitian(InArray<byte> A) {
            if (object.Equals(A,null))
                throw new ArgumentException("ishessup: A must not be null!"); 
            using (Scope.Enter()) {
                Array<byte> A_ = A;
                if (A_.IsScalar) { return true; }
                if (A_.IsEmpty) { return false; }
                if (!A_.IsMatrix) return false;
                long n = A_.Size[1];
                if (n != A_.Size[0]) return false;
                for (long c = 0; c < n; c++) {
                   
                    
                    for (long r = c + 1; r < n; r++) {
                        if (A.GetValue(r,c) != A.GetValue(c,r)) return false; 
                    }
                }
                return true;
            }
        }
        /// <summary>
        /// Determines if matrix <paramref name="A"/> is a lower triangular matrix.
        /// </summary>
        /// <param name="A">Matrix of numeric element type.</param>
        /// <returns>true if <paramref name="A"/> is a lower triangular matrix, false otherwise.</returns>
        /// <exception cref="ArgumentException">if <paramref name="A"/> was null or not a matrix.</exception>
        internal static bool istrilow(InArray<Int64> A) {
            if (object.Equals(A, null))
                throw new ArgumentException("istrilow: A must not be null!");
            using (Scope.Enter()) {
                Array<Int64> A_ = A; 
                if (A_.IsScalar) { return true; }
                if (A_.IsEmpty) { return false; }
                if (!A_.IsMatrix)
                    throw new ArgumentException("istrilow: A_ must be a matrix or a scalar array!");
                long n = A_.Size[1];
                for (long c = 1; c < n; c++) {
                    for (long r = 0; r < c; r++) {
                        if (A_.GetValue(r, c) != (Int64)0) return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Determines if matrix <paramref name="A"/> is an upper triangular matrix.
        /// </summary>
        /// <param name="A">Matrix or scalar array <paramref name="A"/>, numeric element type.</param>
        /// <returns>true if <paramref name="A"/> is an upper triangular matrix, false otherwise.</returns>
        /// <exception cref="ArgumentException">if <paramref name="A"/> was null or not a matrix.</exception>
        internal static bool istriup(InArray<Int64> A) {
            if (object.Equals(A, null))
                throw new ArgumentException("istriup: A must not be null!");
            using (Scope.Enter()) {
                Array<Int64> A_ = A;
                if (A_.IsScalar) { return true; }
                if (A_.IsEmpty) { return false; }
                if (!A_.IsMatrix)
                    throw new ArgumentException("istriup: A_ must be a matrix or a scalar array!");
                long m = A_.Size[0];
                long n = A_.Size[1];
                for (long c = 0; c < n; c++) {
                    for (long r = c + 1; r < m; r++) {
                        if (A_.GetValue(r, c) != (Int64)0) return false;
                    }
                }
                return true;
            }
        }
        /// <summary>
        /// Determines if matrix <paramref name="A"/> is a lower Hessenberg matrix. 
        /// </summary>
        /// <param name="A">Matrix or scalar of numeric element type.</param>
        /// <returns>true if <paramref name="A"/> is a lower Hessenberg matrix, false otherwise.</returns>
        /// <exception cref="ArgumentException">if <paramref name="A"/> was null.</exception>
        internal static bool ishesslow(InArray<Int64> A) {
            if (object.Equals(A, null))
                throw new ArgumentException("ishesslow: A must not be null!");
            using (Scope.Enter()) {
                Array<Int64> A_ = A;
                if (A_.IsScalar) { return true; }
                if (A_.IsEmpty) { return false; }
                if (!A_.IsMatrix) return false;
                long m = A_.Size[0];
                long n = A_.Size[1];
                if (m != n) return false;
                for (long c = 2; c < n; c++) {
                    for (long r = 0; r < c - 1; r++) {
                        if (A_.GetValue(r, c) != (Int64)0) return false;
                    }
                }
                return true;
            }
        }
        /// <summary>
        /// Determines if matrix <paramref name="A"/> is an upper Hessenberg matrix.
        /// </summary>
        /// <param name="A">Matrix or scalar array, numeric element type.</param>
        /// <returns>true if <paramref name="A"/> is an upper Hessenberg matrix, false otherwise.</returns>
        /// <exception cref="ArgumentException">if <paramref name="A"/> was null.</exception>
        internal static bool ishessup(InArray<Int64> A) {
            if (object.Equals(A, null))
                throw new ArgumentException("ishessup: A must not be null!");
            using (Scope.Enter()) {
                Array<Int64> A_ = A;
                if (A_.IsScalar) { return true; }
                if (A_.IsEmpty) { return false; }
                if (!A_.IsMatrix) return false;
                long n = A_.Size[1];
                if (n != A_.Size[0]) return false;
                for (long c = 0; c < n - 2; c++) {
                    for (long r = c + 2; r < n; r++) {
                        if (A_.GetValue(r, c) != (Int64)0) return false;
                    }
                }
                return true;
            }
        }
        /// <summary>
        /// Determines if matrix <paramref name="A"/> is a hermitian matrix.
        /// </summary>
        /// <param name="A">Matrix or scalar array, numeric element type.</param>
        /// <returns>true if A is a hermitian matrix, false otherwise.</returns>
        /// <exception cref="ArgumentException">if A was null.</exception>
        internal static bool ishermitian(InArray<Int64> A) {
            if (object.Equals(A,null))
                throw new ArgumentException("ishessup: A must not be null!"); 
            using (Scope.Enter()) {
                Array<Int64> A_ = A;
                if (A_.IsScalar) { return true; }
                if (A_.IsEmpty) { return false; }
                if (!A_.IsMatrix) return false;
                long n = A_.Size[1];
                if (n != A_.Size[0]) return false;
                for (long c = 0; c < n; c++) {
                   
                    
                    for (long r = c + 1; r < n; r++) {
                        if (A.GetValue(r,c) != A.GetValue(c,r)) return false; 
                    }
                }
                return true;
            }
        }
        /// <summary>
        /// Determines if matrix <paramref name="A"/> is a lower triangular matrix.
        /// </summary>
        /// <param name="A">Matrix of numeric element type.</param>
        /// <returns>true if <paramref name="A"/> is a lower triangular matrix, false otherwise.</returns>
        /// <exception cref="ArgumentException">if <paramref name="A"/> was null or not a matrix.</exception>
        internal static bool istrilow(InArray<Int32> A) {
            if (object.Equals(A, null))
                throw new ArgumentException("istrilow: A must not be null!");
            using (Scope.Enter()) {
                Array<Int32> A_ = A; 
                if (A_.IsScalar) { return true; }
                if (A_.IsEmpty) { return false; }
                if (!A_.IsMatrix)
                    throw new ArgumentException("istrilow: A_ must be a matrix or a scalar array!");
                long n = A_.Size[1];
                for (long c = 1; c < n; c++) {
                    for (long r = 0; r < c; r++) {
                        if (A_.GetValue(r, c) != (Int32)0) return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Determines if matrix <paramref name="A"/> is an upper triangular matrix.
        /// </summary>
        /// <param name="A">Matrix or scalar array <paramref name="A"/>, numeric element type.</param>
        /// <returns>true if <paramref name="A"/> is an upper triangular matrix, false otherwise.</returns>
        /// <exception cref="ArgumentException">if <paramref name="A"/> was null or not a matrix.</exception>
        internal static bool istriup(InArray<Int32> A) {
            if (object.Equals(A, null))
                throw new ArgumentException("istriup: A must not be null!");
            using (Scope.Enter()) {
                Array<Int32> A_ = A;
                if (A_.IsScalar) { return true; }
                if (A_.IsEmpty) { return false; }
                if (!A_.IsMatrix)
                    throw new ArgumentException("istriup: A_ must be a matrix or a scalar array!");
                long m = A_.Size[0];
                long n = A_.Size[1];
                for (long c = 0; c < n; c++) {
                    for (long r = c + 1; r < m; r++) {
                        if (A_.GetValue(r, c) != (Int32)0) return false;
                    }
                }
                return true;
            }
        }
        /// <summary>
        /// Determines if matrix <paramref name="A"/> is a lower Hessenberg matrix. 
        /// </summary>
        /// <param name="A">Matrix or scalar of numeric element type.</param>
        /// <returns>true if <paramref name="A"/> is a lower Hessenberg matrix, false otherwise.</returns>
        /// <exception cref="ArgumentException">if <paramref name="A"/> was null.</exception>
        internal static bool ishesslow(InArray<Int32> A) {
            if (object.Equals(A, null))
                throw new ArgumentException("ishesslow: A must not be null!");
            using (Scope.Enter()) {
                Array<Int32> A_ = A;
                if (A_.IsScalar) { return true; }
                if (A_.IsEmpty) { return false; }
                if (!A_.IsMatrix) return false;
                long m = A_.Size[0];
                long n = A_.Size[1];
                if (m != n) return false;
                for (long c = 2; c < n; c++) {
                    for (long r = 0; r < c - 1; r++) {
                        if (A_.GetValue(r, c) != (Int32)0) return false;
                    }
                }
                return true;
            }
        }
        /// <summary>
        /// Determines if matrix <paramref name="A"/> is an upper Hessenberg matrix.
        /// </summary>
        /// <param name="A">Matrix or scalar array, numeric element type.</param>
        /// <returns>true if <paramref name="A"/> is an upper Hessenberg matrix, false otherwise.</returns>
        /// <exception cref="ArgumentException">if <paramref name="A"/> was null.</exception>
        internal static bool ishessup(InArray<Int32> A) {
            if (object.Equals(A, null))
                throw new ArgumentException("ishessup: A must not be null!");
            using (Scope.Enter()) {
                Array<Int32> A_ = A;
                if (A_.IsScalar) { return true; }
                if (A_.IsEmpty) { return false; }
                if (!A_.IsMatrix) return false;
                long n = A_.Size[1];
                if (n != A_.Size[0]) return false;
                for (long c = 0; c < n - 2; c++) {
                    for (long r = c + 2; r < n; r++) {
                        if (A_.GetValue(r, c) != (Int32)0) return false;
                    }
                }
                return true;
            }
        }
        /// <summary>
        /// Determines if matrix <paramref name="A"/> is a hermitian matrix.
        /// </summary>
        /// <param name="A">Matrix or scalar array, numeric element type.</param>
        /// <returns>true if A is a hermitian matrix, false otherwise.</returns>
        /// <exception cref="ArgumentException">if A was null.</exception>
        internal static bool ishermitian(InArray<Int32> A) {
            if (object.Equals(A,null))
                throw new ArgumentException("ishessup: A must not be null!"); 
            using (Scope.Enter()) {
                Array<Int32> A_ = A;
                if (A_.IsScalar) { return true; }
                if (A_.IsEmpty) { return false; }
                if (!A_.IsMatrix) return false;
                long n = A_.Size[1];
                if (n != A_.Size[0]) return false;
                for (long c = 0; c < n; c++) {
                   
                    
                    for (long r = c + 1; r < n; r++) {
                        if (A.GetValue(r,c) != A.GetValue(c,r)) return false; 
                    }
                }
                return true;
            }
        }
        /// <summary>
        /// Determines if matrix <paramref name="A"/> is a lower triangular matrix.
        /// </summary>
        /// <param name="A">Matrix of numeric element type.</param>
        /// <returns>true if <paramref name="A"/> is a lower triangular matrix, false otherwise.</returns>
        /// <exception cref="ArgumentException">if <paramref name="A"/> was null or not a matrix.</exception>
        internal static bool istrilow(InArray<float> A) {
            if (object.Equals(A, null))
                throw new ArgumentException("istrilow: A must not be null!");
            using (Scope.Enter()) {
                Array<float> A_ = A; 
                if (A_.IsScalar) { return true; }
                if (A_.IsEmpty) { return false; }
                if (!A_.IsMatrix)
                    throw new ArgumentException("istrilow: A_ must be a matrix or a scalar array!");
                long n = A_.Size[1];
                for (long c = 1; c < n; c++) {
                    for (long r = 0; r < c; r++) {
                        if (A_.GetValue(r, c) != (float)0) return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Determines if matrix <paramref name="A"/> is an upper triangular matrix.
        /// </summary>
        /// <param name="A">Matrix or scalar array <paramref name="A"/>, numeric element type.</param>
        /// <returns>true if <paramref name="A"/> is an upper triangular matrix, false otherwise.</returns>
        /// <exception cref="ArgumentException">if <paramref name="A"/> was null or not a matrix.</exception>
        internal static bool istriup(InArray<float> A) {
            if (object.Equals(A, null))
                throw new ArgumentException("istriup: A must not be null!");
            using (Scope.Enter()) {
                Array<float> A_ = A;
                if (A_.IsScalar) { return true; }
                if (A_.IsEmpty) { return false; }
                if (!A_.IsMatrix)
                    throw new ArgumentException("istriup: A_ must be a matrix or a scalar array!");
                long m = A_.Size[0];
                long n = A_.Size[1];
                for (long c = 0; c < n; c++) {
                    for (long r = c + 1; r < m; r++) {
                        if (A_.GetValue(r, c) != (float)0) return false;
                    }
                }
                return true;
            }
        }
        /// <summary>
        /// Determines if matrix <paramref name="A"/> is a lower Hessenberg matrix. 
        /// </summary>
        /// <param name="A">Matrix or scalar of numeric element type.</param>
        /// <returns>true if <paramref name="A"/> is a lower Hessenberg matrix, false otherwise.</returns>
        /// <exception cref="ArgumentException">if <paramref name="A"/> was null.</exception>
        internal static bool ishesslow(InArray<float> A) {
            if (object.Equals(A, null))
                throw new ArgumentException("ishesslow: A must not be null!");
            using (Scope.Enter()) {
                Array<float> A_ = A;
                if (A_.IsScalar) { return true; }
                if (A_.IsEmpty) { return false; }
                if (!A_.IsMatrix) return false;
                long m = A_.Size[0];
                long n = A_.Size[1];
                if (m != n) return false;
                for (long c = 2; c < n; c++) {
                    for (long r = 0; r < c - 1; r++) {
                        if (A_.GetValue(r, c) != (float)0) return false;
                    }
                }
                return true;
            }
        }
        /// <summary>
        /// Determines if matrix <paramref name="A"/> is an upper Hessenberg matrix.
        /// </summary>
        /// <param name="A">Matrix or scalar array, numeric element type.</param>
        /// <returns>true if <paramref name="A"/> is an upper Hessenberg matrix, false otherwise.</returns>
        /// <exception cref="ArgumentException">if <paramref name="A"/> was null.</exception>
        internal static bool ishessup(InArray<float> A) {
            if (object.Equals(A, null))
                throw new ArgumentException("ishessup: A must not be null!");
            using (Scope.Enter()) {
                Array<float> A_ = A;
                if (A_.IsScalar) { return true; }
                if (A_.IsEmpty) { return false; }
                if (!A_.IsMatrix) return false;
                long n = A_.Size[1];
                if (n != A_.Size[0]) return false;
                for (long c = 0; c < n - 2; c++) {
                    for (long r = c + 2; r < n; r++) {
                        if (A_.GetValue(r, c) != (float)0) return false;
                    }
                }
                return true;
            }
        }
        /// <summary>
        /// Determines if matrix <paramref name="A"/> is a hermitian matrix.
        /// </summary>
        /// <param name="A">Matrix or scalar array, numeric element type.</param>
        /// <returns>true if A is a hermitian matrix, false otherwise.</returns>
        /// <exception cref="ArgumentException">if A was null.</exception>
        internal static bool ishermitian(InArray<float> A) {
            if (object.Equals(A,null))
                throw new ArgumentException("ishessup: A must not be null!"); 
            using (Scope.Enter()) {
                Array<float> A_ = A;
                if (A_.IsScalar) { return true; }
                if (A_.IsEmpty) { return false; }
                if (!A_.IsMatrix) return false;
                long n = A_.Size[1];
                if (n != A_.Size[0]) return false;
                for (long c = 0; c < n; c++) {
                   
                    
                    for (long r = c + 1; r < n; r++) {
                        if (A.GetValue(r,c) != A.GetValue(c,r)) return false; 
                    }
                }
                return true;
            }
        }

#endregion HYCALPER AUTO GENERATED CODE

    }
}
