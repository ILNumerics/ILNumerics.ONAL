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
using static ILNumerics.Globals;
using ILNumerics.Core.Functions.Builtin;

namespace ILNumerics {

    /// <summary>
    /// Static methods for handling ILNumerics arrays in ILNumericsV4's way (as in Matlab(R), julia, octave, etc.) are mostly found as static methods here.
    /// </summary>
    public static partial class ILMath {

        /// <summary>
        /// Get or set the currently active implementation for performing fft functions. Default: <see cref="ILNumerics.Core.Native.MKLFFT"/> (if available at runtime), <see cref="ILNumerics.F2NET.ManagedFFTPACK5"/> otherwise.
        /// </summary>
        /// <remarks>By default, ILNumerics uses the managed fft on all platforms. Only when available (Windows, x64 and with ILNumerics.Core.Native package added) optimized, native MKL implementation is used. In order to enforce using the managed lapack implementation or to provide your own IFFT implementation one can now set this value manually.
        /// <example>ILMath.FFTImplementation = new ILNumerics.F2NET.ManagedFFTPACK5()</example></remarks>
        public static Core.Native.IFFT FFTImplementation { 
            get { return MathInternal.FFTImplementation; } 
            set { 
                if (object.Equals(value, null)) {
                    throw new ArgumentNullException(nameof(value)); 
                }
                MathInternal.FFTImplementation = value; 
            }
        }

        #region HYCALPER LOOPSTART DOUBLE FFT interface 
        /*!HC:TYPELIST:
        <hycalper>
        <type>
        <source locate="here">
            double
        </source>
        <destination>float</destination>
        </type>
        <type>
        <source locate="here">
            complex
        </source>
        <destination>fcomplex </destination>
        </type>
            <type>
                <source locate="after">
                    HCretArrR
                </source>
                <destination>float</destination>
            </type>
            <type>
                <source locate="after">
                    HCretArrC
                </source>
                <destination>fcomplex</destination>
            </type>
            <type>
                <source locate="after">
                    HCinArrR
                </source>
                <destination>float</destination>
            </type>
            <type>
                <source locate="after" endmark=">">
                    HCinArrC
                </source>
                <destination>fcomplex</destination>
            </type>
         </hycalper>
         */

        #region fft(A, dim)
        /// <summary>
        /// Fast fourier transform along a specific dimension.
        /// </summary>
        /// <param name="A">Real input data array.</param>
        /// <param name="dim">[Optional] The working dimension. Default: (null) first non-singleton dimension of <paramref name="A"/> or dimension #0.</param>
        /// <returns>Complex array of the same size than <paramref name="A"/>.</returns>
        /// <remarks>
        /// <para>The output array returned will be complex hermitian. I.e. the real 
        /// part being even and the imaginary part being odd symmetrical.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        public static Array<complex> fft(InArray<double> A, uint? dim = null) {

            using (Scope.Enter()) {
                Array<double> _A = A;
                Array<complex> ret;
                if (_A.IsEmpty) {
                    ret = MathInternal.empty<complex>(_A.Size);
                } else {
                    if (!dim.HasValue) {
                        dim = _A.S.WorkingDimension();
                    }
                    ret = MathInternal.FFTImplementation.FFTForward1D(_A, dim.GetValueOrDefault());
                }
                return ret;
            }
        }
        /// <summary>
        /// Fast fourier transform along a specific dimension.
        /// </summary>
        /// <param name="A">Input array</param>
        /// <param name="dim">[Optional] The working dimension. Default: (null) first non-singleton dimension of <paramref name="A"/> or dimension #0.</param>
        /// <returns>Complex array of the same size than <paramref name="A"/>.</returns>
        /// <remarks>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        public static Array<complex> fft(InArray<complex> A, uint? dim = null) {

            using (Scope.Enter()) {
                Array<complex> _A = A;
                Array<complex> ret;
                if (_A.IsEmpty) {
                    ret = MathInternal.empty<complex>(_A.Size);
                } else {
                    if (!dim.HasValue) {
                        dim = _A.S.WorkingDimension();
                    }
                    ret = MathInternal.FFTImplementation.FFTForward1D(_A, dim.GetValueOrDefault());
                }

                return ret;
            }
        }
        /// <summary>
        /// Inverse fast fourier transform along a specific dimension.
        /// </summary>
        /// <param name="A">Input array</param>
        /// <param name="dim">[Optional] The working dimension. Default: (null) first non-singleton dimension of <paramref name="A"/> or dimension #0.</param>
        /// <returns>Complex array of the same size than <paramref name="A"/>.</returns>
        /// <remarks>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        public static Array<complex> ifft(InArray<complex> A, uint? dim = null) {

            using (Scope.Enter()) {
                Array<complex> _A = A;
                Array<complex> ret;
                if (_A.IsEmpty) {
                    ret = MathInternal.empty<complex>(_A.Size);
                } else {
                    if (!dim.HasValue) {
                        dim = _A.S.WorkingDimension();
                    }
                    ret = MathInternal.FFTImplementation.FFTBackward1D(_A, dim.GetValueOrDefault());
                }
                return ret;
            }
        }
        /// <summary>
        /// Inverse fast fourier transform from complex hermitian input to real data.
        /// </summary>
        /// <param name="A">Complex hermitian input array (frequency domain).</param>
        /// <param name="dim">[Optional] The working dimension. Default: (null) first non-singleton dimension of <paramref name="A"/> or dimension #0.</param>
        /// <returns>Real array of the same size than <paramref name="A"/>.</returns>
        /// <remarks>
        /// <para>Since a transform of complex hermitian input data results in an 
        /// output having the imaginary part equal to zero, only the real part is 
        /// returned for convenience reasons.</para>
        /// <para>No check is made for A being hermitian! If A is not hermitian (by means 
        /// of round-off errors), the result will be incorrect!</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        public static Array<double> ifftsym(InArray<complex> A, uint? dim = null) {
            using (Scope.Enter()) {
                Array<complex> _A = A;
                Array<double> ret;
                if (_A.IsEmpty) {
                    ret = MathInternal.empty<double>(_A.Size);
                } else {
                    if (!dim.HasValue) {
                        dim = _A.S.WorkingDimension();
                    }
                    ret = MathInternal.FFTImplementation.FFTBackwSym1D(_A, dim.GetValueOrDefault());
                }
                return ret;
            }
        }
        #endregion

        #region fft2(A)
        /// <summary>
        /// Fast 2D discrete fourier transform.
        /// </summary>
        /// <param name="A">Real input array.</param>
        /// <returns>Complex array with transformation result.</returns>
        /// <remarks>
        /// <para>The 2D transformation is computed for the first 2 dimensions, regardless 
        /// of those dimensions being singleton or non-singleton dimensions. If A is an n-d array, 
        /// the transformation is repeated for trailing dimensions of A respectively. </para>
        /// <para>The output array returned will be complex hermitian.</para>
        /// <para>The two dimensional transformation is equivalent to repeatedly transforming 
        /// the columns and after that transforming the rows of A. However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        public static Array<complex > fft2(InArray<double > A) {

            using (Scope.Enter()) {
                Array<complex> ret;
                if (A.IsEmpty) {
                    ret = MathInternal.empty<complex>(A.Size);
                } else {
                    ret = MathInternal.FFTImplementation.FFTForward(A, 2);
                }
                return ret;
            }
        }
        /// <summary>
        /// Fast 2D discrete fourier transform.
        /// </summary>
        /// <param name="A">Complex source array.</param>
        /// <returns>Complex array with transformation result.</returns>
        /// <remarks>
        /// <para>The transformation is computed for the first 2 dimensions, regardless 
        /// of those dimensions being singleton or non-singleton dimensions. If A is an n-d array, 
        /// the transformation is repeated for trailing dimensions of A respectively. </para>
        /// <para>The two dimensional transformation is equivalent to repeatedly transforming 
        /// the columns and after that transforming the rows of A. However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        public static Array<complex> fft2(InArray<complex> A) {
            using (Scope.Enter()) {
                Array<complex> _A = A, ret;
                if (_A.IsEmpty) {
                    ret = MathInternal.empty<complex>(_A.Size);
                } else {
                    ret = MathInternal.FFTImplementation.FFTForward(_A, 2);
                }
                return ret;
            }
        }
        /// <summary>
        /// Inverse fast 2D discrete fourier transform.
        /// </summary>
        /// <param name="A">Complex source array.</param>
        /// <returns>Complex array with the inverse transformation of <paramref name="A"/>.</returns>
        /// <remarks>
        /// <para>The transformation is computed for the first 2 dimensions, regardless 
        /// of those dimensions being singleton or non-singleton. If A is an n-d array, 
        /// the transformation is repeated for trailing dimensions of A respectively. </para>
        /// <para>The two dimensional transformation is equivalent to repeatedly transforming 
        /// the columns and after that transforming the rows of A. However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        public static Array<complex> ifft2(InArray<complex> A) {
            using (Scope.Enter()) {
                Array<complex> _A = A, ret;
                if (_A.IsEmpty) {
                    ret = MathInternal.empty<complex>(_A.Size);
                } else {
                    ret = MathInternal.FFTImplementation.FFTBackward(_A, 2);
                }
                return ret; 
            }
        }
        /// <summary>
        /// Inverse, 2D, discrete fast fourier transform (hermitian input).
        /// </summary>
        /// <param name="A">Complex hermitian input array (frequency domain)</param>
        /// <returns>Real array with the inverse transformation of complex, hermitian <paramref name="A"/>.</returns>
        /// <remarks>
        /// <para>Since a transform of complex hermitian input data results in the 
        /// output having the imaginary part equal to zero, only the real part is 
        /// returned for efficiency reasons.</para>
        /// <para>One way to create a hermitian array is to (forward) transform a real array.</para>
        /// <para>No check is made for A being hermitian! If A is not hermitian (within 
        /// round-off errors), the result will be incorrect!</para>
        /// <para>The transformation is computed for the first 2 dimensions, regardless 
        /// of those dimensions being singleton or non-singleton. If A is an n-d array, 
        /// the transformation is repeated for trailing dimensions of A respectively. </para>
        /// <para>The two dimensional transformation is equivalent to repeatedly transforming 
        /// the columns and after that transforming the rows of A. However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        public static Array<double> ifft2sym(InArray<complex> A) {
            using (Scope.Enter()) {
                Array<complex> _A = A;
                Array<double> ret;
                if (_A.IsEmpty) {
                    ret = MathInternal.empty<double>(_A.Size);
                } else {
                    ret = MathInternal.FFTImplementation.FFTBackwSym(_A, 2);
                }
                return ret;
            }
        }
        #endregion

        #region fft2(A,m,n)
        /// <summary>
        /// Fast fourier transform (2D)
        /// </summary>
        /// <param name="A">Input array</param>
        /// <param name="m">Transformation column length</param>
        /// <param name="n">Transformation row length</param>
        /// <returns>Transformation result, complex hermitian</returns>
        /// <remarks>
        /// <para>The transformation is computed for the first 2 dimensions, regardless 
        /// of those dimensions being singleton or non-singleton. If A is an n-d array, 
        /// the transformation is repeated for trailing dimensions of A respectively. </para>
        /// <para>The data to be transformed (based on the A array) are resized according to 
        /// the length parameter m and n. If m or n is larger then the length of the corresponding 
        /// dimension of A, zeros will be padded, otherwise the dimensions are truncated respectively. </para>
        /// <para>The two dimensional transformation is equivalent to repeatedly transforming 
        /// the columns and after that transforming the rows of A. However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">is thrown if n or m is negative. In versions greater 
        /// than 4.12 the API changed to <see cref="uint"/> length paramters, making this exception obsolete.</exception>
        public static Array</*!HC:HCretArrC*/ complex> fft2(InArray</*!HC:HCinArrR*/ double> A, uint m, uint n) {

            using (Scope.Enter()) {
                Array</*!HC:HCinArrR*/ double> _A = A;
                Array </*!HC:HCretArrC*/ complex > ret;
                if (_A.IsEmpty) {
                    ret = MathInternal.empty</*!HC:HCretArrC*/ complex>(_A.Size);
                } else {
                    Array<long> Asize = _A.shape;
                    Asize[0] = m; Asize[1] = n;
                    Array</*!HC:HCinArrR*/ double> resizedA = resize4Transform </*!HC:HCinArrR*/ double>(_A, Asize);
                    ret = MathInternal.FFTImplementation.FFTForward(resizedA, 2);
                }
                return ret; 
            }
        }
        /// <summary>
        /// Fast fourier transform (2D)
        /// </summary>
        /// <param name="A">input array</param>
        /// <param name="m">Transformation column length</param>
        /// <param name="n">Transformation row length</param>
        /// <returns>Transformation result</returns>
        /// <remarks>
        /// <para>The transformation is computed for the first 2 dimensions, regardless 
        /// of those dimensions being singleton or non-singleton. If A is an n-d array, 
        /// the transformation is repeated for trailing dimensions of A respectively. </para>
        /// <para>The data to be transformed (based on the A array) are resized according to 
        /// the length parameter m and n. If m or n is larger then the length of the corresponding 
        /// dimension of A, zeros will be padded, otherwise the dimensions are truncated respectively. </para>
        /// <para>The two dimensional transformation is equivalent to repeatedly transforming 
        /// the columns and after that transforming the rows of A. However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">is thrown if n or m is negative. In versions greater 
        /// than 4.12 the API changed to <see cref="uint"/> length paramters, making this exception obsolete.</exception>
        public static Array</*!HC:HCretArrC*/ complex> fft2(InArray</*!HC:HCinArrC*/ complex> A, uint m, uint n) {

            using (Scope.Enter()) {
                Array</*!HC:HCinArrC*/ complex> _A = A;
                Array</*!HC:HCretArrC*/ complex> ret;
                if (_A.IsEmpty) {
                    ret = MathInternal.empty</*!HC:HCretArrC*/ complex>(_A.Size);
                } else {

                    Array<long> Asize = _A.shape;
                    Asize[0] = m; Asize[1] = n;
                    Array</*!HC:HCinArrC*/ complex> resizedA = resize4Transform</*!HC:HCinArrC*/ complex>(_A, Asize);
                    ret = MathInternal.FFTImplementation.FFTForward(resizedA, 2);
                }
                return ret;
            }
        }
        /// <summary>
        /// Inverse fast fourier transform (2D)
        /// </summary>
        /// <param name="A">Input array</param>
        /// <param name="m">Transformation column length</param>
        /// <param name="n">Transformation row length</param>
        /// <returns>Transformation result</returns>
        /// <remarks>
        /// <para>The transformation is computed for the first 2 dimensions, regardless 
        /// of those dimensions being singleton or non-singleton. If A is an n-d array, 
        /// the transformation is repeated for trailing dimensions of A respectively. </para>
        /// <para>The data to be transformed (based on the array A) are resized according to 
        /// the length parameter m and n. If m or n is larger then the length of the corresponding 
        /// dimension of A, zeros will be padded, otherwise the dimensions are truncated respectively. </para>
        /// <para>The two dimensional transformation is equivalent to repeatedly transforming 
        /// the columns and after that transforming the rows of A. However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying fft implementation.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">is thrown if n or m is negative. In versions greater 
        /// than 4.12 the API changed to <see cref="uint"/> length paramters, making this exception obsolete.</exception>
        public static Array</*!HC:HCretArrC*/ complex> ifft2(InArray</*!HC:HCinArrC*/ complex> A, uint m, uint n) {
            using (Scope.Enter()) {
                Array</*!HC:HCinArrC*/ complex> _A = A;
                Array</*!HC:HCretArrC*/ complex> ret;
                if (_A.IsEmpty) {
                    ret = MathInternal.empty</*!HC:HCretArrC*/ complex>(_A.Size);
                } else {
                    Array<long> Asize = _A.shape;
                    Asize[0] = m; Asize[1] = n;
                    var resizedA = resize4Transform</*!HC:HCinArrC*/ complex>(_A, Asize);
                    ret = MathInternal.FFTImplementation.FFTBackward(resizedA, 2);
                }
                return ret;

            }
        }
        /// <summary>
        /// Inverse fast fourier transform (2D)
        /// </summary>
        /// <param name="A">Complex hermitian input array, symmetric in first 2 dimensions</param>
        /// <param name="m">Transformation column length</param>
        /// <param name="n">Transformation row length</param>
        /// <returns>Transformation result</returns>
        /// <remarks>
        /// <para>The transformation is computed for the first 2 dimensions, regardless 
        /// of those dimensions being singleton or non-singleton. If A is an n-d array, 
        /// the transformation is repeated for trailing dimensions of A respectively. The 
        /// lengths of those trailing dimensions are not altered.</para>
        /// <para>Since a transform of complex hermitian input data results in the 
        /// output having the imaginary part equals zero, only the real part is 
        /// returned for convenience reasons.</para>
        /// <para>No check is made for A being hermitian! If A is not hermitian (by means 
        /// of round-off errors), the result will be wrong!</para>
        /// <para>The data to be transformed (based on the array A) are resized according to 
        /// the length parameter m and n. If m or n is larger then the length of the corresponding 
        /// dimension of A, zeros will be padded, otherwise the dimensions are truncated respectively. </para>
        /// <para>The two dimensional transformation is equivalent to repeatedly transforming 
        /// the columns and after that transforming the rows of A. However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">is thrown if n or m is negative. In versions greater 
        /// than 4.12 the API changed to <see cref="uint"/> length paramters, making this exception obsolete.</exception>
        public static Array</*!HC:HCretArrR*/ double> ifft2sym(InArray</*!HC:HCinArrC*/ complex> A, uint m, uint n) {
            using (Scope.Enter()) {
                Array</*!HC:HCinArrC*/ complex> _A = A;
                Array</*!HC:HCretArrR*/ double> ret;
                if (_A.IsEmpty) {
                    ret = MathInternal.empty</*!HC:HCretArrR*/ double>(_A.Size);
                } else {
                    Array<long> Asize = _A.shape;
                    Asize[0] = m; Asize[1] = n;
                    Array</*!HC:HCinArrC*/ complex> resizedA = resize4Transform</*!HC:HCinArrC*/ complex>(_A, Asize);
                    ret = MathInternal.FFTImplementation.FFTBackwSym(resizedA, 2);
                }
                return ret;

            }
        }
        #endregion

        #region fftn(A)
        /// <summary>
        /// Fast fourier transform (n-D)
        /// </summary>
        /// <param name="A">Input array, n-D</param>
        /// <returns>Transformation result, complex hermitian</returns>
        /// <remarks>
        /// <para>The n-dimensional transformation is computed for the n-dimensional array A. 
        /// This is equivalent to repeatedly (inplace) 
        /// computing one dimensional transformations along all dimensions of A.
        /// However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        public static Array</*!HC:HCretArrC*/ complex> fftn(InArray</*!HC:HCinArrR*/ double> A) {
            using (Scope.Enter()) {
                Array</*!HC:HCinArrR*/ double> _A = A;
                Array</*!HC:HCretArrC*/ complex> ret; 
                if (_A.IsEmpty) {
                    ret = MathInternal.empty</*!HC:HCretArrC*/ complex>(_A.Size);
                }else {
                    ret = MathInternal.FFTImplementation.FFTForward(_A, _A.Size.NumberOfDimensions);
                }
                return ret;
            }
        }
        /// <summary>
        /// Fast fourier transform (n-D)
        /// </summary>
        /// <param name="A">Input array, n-D</param>
        /// <returns>Transformation result</returns>
        /// <remarks>
        /// <para>The n-dimensional transformation is computed for the n-dimensional array A. 
        /// This is equivalent to repeatedly (inplace) 
        /// computing one dimensional transformations along all dimensions of A.
        /// However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        public static Array</*!HC:HCretArrC*/ complex> fftn(InArray</*!HC:HCinArrC*/ complex> A) {
            using (Scope.Enter()) {
                Array</*!HC:HCinArrC*/ complex> _A = A;
                Array</*!HC:HCretArrC*/ complex> ret;
                if (_A.IsEmpty) {
                    ret = MathInternal.empty</*!HC:HCretArrC*/ complex>(_A.Size);
                } else {
                    ret = MathInternal.FFTImplementation.FFTForward(_A, _A.Size.NumberOfDimensions);
                }
                return ret; 
            }
        }
        /// <summary>
        /// Inverse fast fourier transform (n-D)
        /// </summary>
        /// <param name="A">Input array, n-D (frequency domain)</param>
        /// <returns>Transformation result</returns>
        /// <remarks>
        /// <para>The n-dimensional inverse transformation is computed for the n-dimensional array A. 
        /// This is equivalent to repeatedly (inplace) 
        /// computing one dimensional transformations along all dimensions of A.
        /// However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        public static Array</*!HC:HCretArrC*/ complex> ifftn(InArray</*!HC:HCinArrC*/ complex> A) {
            using (Scope.Enter()) {
                Array</*!HC:HCinArrC*/ complex> _A = A;
                Array</*!HC:HCretArrC*/ complex> ret;
                if (_A.IsEmpty) {
                    ret = MathInternal.empty</*!HC:HCretArrC*/ complex>(_A.Size);
                } else {
                    ret = MathInternal.FFTImplementation.FFTBackward(_A, _A.Size.NumberOfDimensions);
                }
                return ret; 
            }
        }
        /// <summary>
        /// Inverse fast fourier transform (n-D)
        /// </summary>
        /// <param name="A">Input array, n-D, complex hermitian (frequency domain)</param>
        /// <returns>Transformation result</returns>
        /// <remarks>
        /// <para>The n-dimensional inverse transformation is computed for the n-dimensional array A. 
        /// This is equivalent to repeatedly (inplace) 
        /// computing one dimensional transformations along all dimensions of A.
        /// However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>Since a transform of complex hermitian input data results in the 
        /// output having the imaginary part equals zero, only the real part is 
        /// returned for convenience reasons.</para>
        /// <para>No check is made for A being hermitian! If A is not hermitian (by means 
        /// of round-off errors), the result will be wrong!</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        public static Array</*!HC:HCretArrR*/ double> ifftnsym(InArray</*!HC:HCinArrC*/ complex> A) {
            using (Scope.Enter()) {
                Array</*!HC:HCinArrC*/ complex> _A = A;
                Array</*!HC:HCretArrR*/ double> ret; 
                if (_A.IsEmpty) {
                    ret = MathInternal.empty</*!HC:HCretArrR*/ double>(_A.Size);
                } else {
                    ret = MathInternal.FFTImplementation.FFTBackwSym(_A, _A.Size.NumberOfDimensions);
                }
                return ret;
            }
        }
        #endregion

        #region fftn(A, InArray<int> dims)
        /// <summary>
        /// Fast fourier transform (n-D, specific size)
        /// </summary>
        /// <param name="A">Input array, n-D</param>
        /// <param name="dims">Transformation lengths, specifies the length of the dimensions 
        /// for the transformation array. The length of dims must be &gt; or equal to the number of 
        /// dimensions of A. For elements in dim being smaller than corresponding dimension 
        /// length in A, the dimensions will be truncated, otherwise zeros will be padded.</param>
        /// <returns>Transformation result of size specified by 'dims' parameter, complex hermitian</returns>
        /// <remarks>
        /// <para>The n-dimensional transformation is computed for the n-dimensional array A. 
        /// Before the transform, the input is resized according to the 'dims' parameter. 
        /// Dimensions larger than corresponding entries in 'dim' are truncated, dimensions
        /// smaller than corresponding entries in 'dim' are zero padded.</para>
        /// <para>The n-dimensional transformation is equivalent to repeatedly (inplace) 
        /// computing one dimensional transformations along all dimensions of A.
        /// However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">is thrown if the 
        /// dim parameter is null, its length is less then the number of dimensions of A 
        /// or any element of dims is non-negative</exception>
        public static Array</*!HC:HCretArrC*/ complex> fftn(InArray</*!HC:HCinArrR*/ double> A, InArray<long> dims) {
            using (Scope.Enter()) {
                Array</*!HC:HCinArrR*/ double> _A = A;
                Array</*!HC:HCretArrC*/ complex> ret; 
                Array<long> _dims = dims;
                if (_A.IsEmpty) {
                    ret = MathInternal.empty</*!HC:HCretArrC*/ complex>(_A.Size);
                } else {
                    Array</*!HC:HCinArrR*/ double> resizedA = resize4Transform</*!HC:HCinArrR*/ double>(_A, _dims);
                    ret = MathInternal.FFTImplementation.FFTForward(resizedA, (uint)_dims.S.NumberOfElements);
                }
                return ret; 
            }
        }
        /// <summary>
        /// Fast fourier transform (n-D, specific size)
        /// </summary>
        /// <param name="A">Tnput array, n-D</param>
        /// <param name="dims">Transformation lengths, specifies the length of the dimensions 
        /// for the transformation array. The length of dims must be &gt; or equal to  the number of 
        /// dimensions of A. For elements in dim being smaller than corresponding dimension 
        /// length in A, the dimensions will be truncated, otherwise zeros will be padded.</param>
        /// <returns>Transformation result of size specified by 'dims' parameter</returns>
        /// <remarks>
        /// <para>The n-dimensional transformation is computed for the n-dimensional array A. 
        /// Before the transform, the input is resized according to the 'dims' parameter. 
        /// Dimensions larger than corresponding entries in 'dim' are truncated, dimensions
        /// smaller than corresponding entries in 'dim' are zero padded.</para>
        /// <para>The n-dimensional transformation is equivalent to repeatedly (inplace) 
        /// computing one dimensional transformations along all dimensions of A.
        /// However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">is thrown if the 
        /// dim parameter is null, its length is smaller then the number of dimensions of A 
        /// or any element of dims is non-negative</exception>
        public static Array</*!HC:HCretArrC*/ complex> fftn(InArray</*!HC:HCinArrC*/ complex> A, InArray<long> dims) {
            using (Scope.Enter()) {
                Array</*!HC:HCinArrC*/ complex> _A = A;
                Array</*!HC:HCretArrC*/ complex> ret; 
                Array<long> _dims = dims;
                if (_A.IsEmpty) {
                    ret = MathInternal.empty</*!HC:HCretArrC*/ complex>(_A.Size);
                } else {
                    Array</*!HC:HCinArrC*/ complex> resizedA = resize4Transform</*!HC:HCinArrC*/ complex>(_A, _dims);
                    ret = MathInternal.FFTImplementation.FFTForward(resizedA, (uint)_dims.S.NumberOfElements);
                }
                return ret; 
            }
        }
        /// <summary>
        /// Inverse fast fourier transform (n-D, specific size)
        /// </summary>
        /// <param name="A">Input array, n-D</param>
        /// <param name="dims">Transformation lengths, specifies the length of the dimensions 
        /// for the transformation array. The length of dims must be &gt; or equal to  the number of 
        /// dimensions of A. For elements in dim being smaller than corresponding dimension 
        /// length in A, the dimensions will be truncated, otherwise zeros will be padded.</param>
        /// <returns>Transformation result of size specified by 'dims' parameter</returns>
        /// <remarks>
        /// <para>The n-dimensional transformation is computed for the n-dimensional array A. 
        /// Before the transform, the input is resized according to the 'dims' parameter. 
        /// Dimensions larger than corresponding entries in 'dim' are truncated, dimensions
        /// smaller than corresponding entries in 'dim' are zero padded.</para>
        /// <para>The n-dimensional transformation is equivalent to repeatedly (inplace) 
        /// computing one dimensional transformations along all dimensions of A.
        /// However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">is thrown if the 
        /// dim parameter is null, its length is less then the number of dimensions of A 
        /// or any element of dims is non-negative</exception>
        public static Array</*!HC:HCretArrC*/ complex> ifftn(InArray</*!HC:HCinArrC*/ complex> A, InArray<long> dims) {
            using (Scope.Enter()) {
                Array</*!HC:HCinArrC*/ complex> _A = A;
                Array</*!HC:HCretArrC*/ complex> ret; 
                Array<long> _dims = dims;
                if (_A.IsEmpty) {
                    ret = MathInternal.empty</*!HC:HCretArrC*/ complex>(_A.Size);
                } else {
                    Array</*!HC:HCinArrC*/ complex> resizedA = resize4Transform</*!HC:HCinArrC*/ complex>(_A, _dims);
                    return MathInternal.FFTImplementation.FFTBackward(resizedA, (uint)_dims.S.NumberOfElements);
                }
                return ret; 
            }
        }
        /// <summary>
        /// Inverse fast fourier transform (n-D, complex hermitian, specific size)
        /// </summary>
        /// <param name="A">Complex hermitian input array, n-D</param>
        /// <param name="dims">Transformation lengths, specifies the length of the dimensions 
        /// for the transformation array. The length of dims must be &gt; or equal to  the number of 
        /// dimensions of A. For elements in dim being smaller than corresponding dimension 
        /// length in A, the dimensions will be truncated, otherwise zeros will be padded.</param>
        /// <returns>Transformation result, real array of the size specified by the 'dims' parameter</returns>
        /// <remarks>
        /// <para>The n-dimensional transformation is computed for the n-dimensional array A. 
        /// Before the transform, the input is resized according to the 'dims' parameter. 
        /// Dimensions larger than corresponding entries in 'dim' are truncated, dimensions
        /// smaller than corresponding entries in 'dim' are zero padded.</para>
        /// <para>The n-dimensional transformation is equivalent to repeatedly (inplace) 
        /// computing one dimensional transformations along all dimensions of A.
        /// However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>Since a transform of complex hermitian input data results in the 
        /// output having the imaginary part equals zero, only the real part is 
        /// returned for convenience reasons.</para>
        /// <para>No check is made for A being hermitian! If A is not hermitian (by means 
        /// of round-off errors), the result will be wrong!</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">is thrown if the 
        /// dim parameter is null, its length is less then the number of dimensions of A 
        /// or any element of dims is non-negative</exception>
        public static Array</*!HC:HCretArrR*/ double> ifftnsym(InArray</*!HC:HCinArrC*/ complex> A, InArray<long> dims) {
            using (Scope.Enter()) {
                Array</*!HC:HCinArrC*/ complex> _A = A;
                Array</*!HC:HCretArrR*/ double> ret; 
                Array<long> _dims = dims;
                if (_A.IsEmpty) {
                    ret = empty</*!HC:HCretArrR*/ double>(_A.Size);
                } else {
                    Array</*!HC:HCinArrC*/ complex> resizedA = resize4Transform</*!HC:HCinArrC*/ complex>(_A, _dims);
                    ret = FFTImplementation.FFTBackwSym(resizedA, (uint)_dims.S.NumberOfElements);
                }
                return ret;
            }
        }
        #endregion

        #region OBSOLETE fftn(A, params dims) 
        /// <summary>
        /// Fast fourier transform (n-D, specific size)
        /// </summary>
        /// <param name="A">Input array, n-D</param>
        /// <param name="dims">Transformation lengths, specifies the length of the dimensions 
        /// for the transformation array. The length of dims must be &gt; or equal to the number of 
        /// dimensions of A. For elements in dim being smaller than corresponding dimension 
        /// length in A, the dimensions will be truncated, otherwise zeros will be padded.</param>
        /// <returns>Transformation result of size specified by 'dims' parameter, complex hermitian</returns>
        /// <remarks>
        /// <para>The n-dimensional transformation is computed for the n-dimensional array A. 
        /// Before the transform, the input is resized according to the 'dims' parameter. 
        /// Dimensions larger than corresponding entries in 'dim' are truncated, dimensions
        /// smaller than corresponding entries in 'dim' are zero padded.</para>
        /// <para>The n-dimensional transformation is equivalent to repeatedly (inplace) 
        /// computing one dimensional transformations along all dimensions of A.
        /// However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">is thrown if the 
        /// dim parameter is null, its length is less then the number of dimensions of A 
        /// or any element of dims is non-negative</exception>
        [Obsolete("Use fftn(InArray<T> A, InArray<uint> dims) instead! The function ILMath.vector<T>() can be used to replace 'params' arguments.")]
        public static Array</*!HC:HCretArrC*/ complex> fftn(InArray</*!HC:HCinArrR*/ double> A, params uint[] dims) {
            using (Scope.Enter(A)) {
                if (A.IsEmpty) return MathInternal.empty</*!HC:HCretArrC*/ complex>(A.Size);
                Array</*!HC:HCinArrR*/ double> resizedA = resize4Transform(A, toint64(vector(dims)));
                return MathInternal.FFTImplementation.FFTForward(resizedA, (uint)dims.Length);
            }
        }
        /// <summary>
        /// Fast fourier transform (n-D, specific size)
        /// </summary>
        /// <param name="A">Tnput array, n-D</param>
        /// <param name="dims">Transformation lengths, specifies the length of the dimensions 
        /// for the transformation array. The length of dims must be &gt; or equal to  the number of 
        /// dimensions of A. For elements in dim being smaller than corresponding dimension 
        /// length in A, the dimensions will be truncated, otherwise zeros will be padded.</param>
        /// <returns>Transformation result of size specified by 'dims' parameter</returns>
        /// <remarks>
        /// <para>The n-dimensional transformation is computed for the n-dimensional array A. 
        /// Before the transform, the input is resized according to the 'dims' parameter. 
        /// Dimensions larger than corresponding entries in 'dim' are truncated, dimensions
        /// smaller than corresponding entries in 'dim' are zero padded.</para>
        /// <para>The n-dimensional transformation is equivalent to repeatedly (inplace) 
        /// computing one dimensional transformations along all dimensions of A.
        /// However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">is thrown if the 
        /// dim parameter is null, its length is smaller then the number of dimensions of A 
        /// or any element of dims is non-negative</exception>
        [Obsolete("Use fftn(InArray<T> A, InArray<uint> dims) instead! The function ILMath.vector<T>() can be used to replace 'params' arguments.")]
        public static Array</*!HC:HCretArrC*/ complex> fftn(InArray</*!HC:HCinArrC*/ complex> A, params uint[] dims) {
            using (Scope.Enter(A)) {
                if (A.IsEmpty) return MathInternal.empty</*!HC:HCretArrC*/ complex>(A.Size);
                Array</*!HC:HCinArrC*/ complex> resizedA = resize4Transform(A, toint64(vector(dims)));
                return MathInternal.FFTImplementation.FFTForward(resizedA, (uint)dims.Length);
            }
        }
        /// <summary>
        /// Inverse fast fourier transform (n-D, specific size)
        /// </summary>
        /// <param name="A">Input array, n-D</param>
        /// <param name="dims">Transformation lengths, specifies the length of the dimensions 
        /// for the transformation array. The length of dims must be &gt; or equal to  the number of 
        /// dimensions of A. For elements in dim being smaller than corresponding dimension 
        /// length in A, the dimensions will be truncated, otherwise zeros will be padded.</param>
        /// <returns>Transformation result of size specified by 'dims' parameter</returns>
        /// <remarks>
        /// <para>The n-dimensional transformation is computed for the n-dimensional array A. 
        /// Before the transform, the input is resized according to the 'dims' parameter. 
        /// Dimensions larger than corresponding entries in 'dim' are truncated, dimensions
        /// smaller than corresponding entries in 'dim' are zero padded.</para>
        /// <para>The n-dimensional transformation is equivalent to repeatedly (inplace) 
        /// computing one dimensional transformations along all dimensions of A.
        /// However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">is thrown if the 
        /// dim parameter is null, its length is less then the number of dimensions of A 
        /// or any element of dims is non-negative</exception>
        [Obsolete("Use ifftn(InArray<T> A, InArray<uint> dims) instead! The function ILMath.vector<T>() can be used to replace 'params' arguments.")]
        public static Array</*!HC:HCretArrC*/ complex> ifftn(InArray</*!HC:HCinArrC*/ complex> A, params uint[] dims) {
            using (Scope.Enter(A)) {
                if (A.IsEmpty) return MathInternal.empty</*!HC:HCretArrC*/ complex>(A.Size);
                Array</*!HC:HCinArrC*/ complex> resizedA = resize4Transform(A, toint64(vector(dims)));
                return MathInternal.FFTImplementation.FFTBackward(resizedA, (uint)dims.Length);
            }
        }
        /// <summary>
        /// Inverse fast fourier transform (n-D, complex hermitian, specific size)
        /// </summary>
        /// <param name="A">Complex hermitian input array, n-D</param>
        /// <param name="dims">Transformation lengths, specifies the length of the dimensions 
        /// for the transformation array. The length of dims must be &gt; or equal to  the number of 
        /// dimensions of A. For elements in dim being smaller than corresponding dimension 
        /// length in A, the dimensions will be truncated, otherwise zeros will be padded.</param>
        /// <returns>Transformation result, real array of the size specified by the 'dims' parameter</returns>
        /// <remarks>
        /// <para>The n-dimensional transformation is computed for the n-dimensional array A. 
        /// Before the transform, the input is resized according to the 'dims' parameter. 
        /// Dimensions larger than corresponding entries in 'dim' are truncated, dimensions
        /// smaller than corresponding entries in 'dim' are zero padded.</para>
        /// <para>The n-dimensional transformation is equivalent to repeatedly (inplace) 
        /// computing one dimensional transformations along all dimensions of A.
        /// However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>Since a transform of complex hermitian input data results in the 
        /// output having the imaginary part equals zero, only the real part is 
        /// returned for convenience reasons.</para>
        /// <para>No check is made for A being hermitian! If A is not hermitian (by means 
        /// of round-off errors), the result will be wrong!</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">is thrown if the 
        /// dim parameter is null, its length is less then the number of dimensions of A 
        /// or any element of dims is non-negative</exception>
        [Obsolete("Use ifftnsym(InArray<T> A, InArray<uint> dims) instead! The function ILMath.vector<T>() can be used to replace 'params' arguments.")]
        public static Array</*!HC:HCretArrR*/ double> ifftnsym(InArray</*!HC:HCinArrC*/ complex> A, params uint[] dims) {
            using (Scope.Enter(A)) {
                if (A.IsEmpty) return empty</*!HC:HCretArrR*/ double>(A.Size);
                Array</*!HC:HCinArrC*/ complex> resizedA = resize4Transform(A, toint64(vector(dims)));
                return FFTImplementation.FFTBackwSym(resizedA, (uint)dims.Length);
            }
        }
#endregion

#endregion HYCALPER LOOPEND DOUBLE FFT interface
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        #region fft(A, dim)
        /// <summary>
        /// Fast fourier transform along a specific dimension.
        /// </summary>
        /// <param name="A">Real input data array.</param>
        /// <param name="dim">[Optional] The working dimension. Default: (null) first non-singleton dimension of <paramref name="A"/> or dimension #0.</param>
        /// <returns>Complex array of the same size than <paramref name="A"/>.</returns>
        /// <remarks>
        /// <para>The output array returned will be fcomplex  hermitian. I.e. the real 
        /// part being even and the imaginary part being odd symmetrical.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        public static Array<fcomplex > fft(InArray<float> A, uint? dim = null) {

            using (Scope.Enter()) {
                Array<float> _A = A;
                Array<fcomplex > ret;
                if (_A.IsEmpty) {
                    ret = MathInternal.empty<fcomplex >(_A.Size);
                } else {
                    if (!dim.HasValue) {
                        dim = _A.S.WorkingDimension();
                    }
                    ret = MathInternal.FFTImplementation.FFTForward1D(_A, dim.GetValueOrDefault());
                }
                return ret;
            }
        }
        /// <summary>
        /// Fast fourier transform along a specific dimension.
        /// </summary>
        /// <param name="A">Input array</param>
        /// <param name="dim">[Optional] The working dimension. Default: (null) first non-singleton dimension of <paramref name="A"/> or dimension #0.</param>
        /// <returns>Complex array of the same size than <paramref name="A"/>.</returns>
        /// <remarks>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        public static Array<fcomplex > fft(InArray<fcomplex > A, uint? dim = null) {

            using (Scope.Enter()) {
                Array<fcomplex > _A = A;
                Array<fcomplex > ret;
                if (_A.IsEmpty) {
                    ret = MathInternal.empty<fcomplex >(_A.Size);
                } else {
                    if (!dim.HasValue) {
                        dim = _A.S.WorkingDimension();
                    }
                    ret = MathInternal.FFTImplementation.FFTForward1D(_A, dim.GetValueOrDefault());
                }

                return ret;
            }
        }
        /// <summary>
        /// Inverse fast fourier transform along a specific dimension.
        /// </summary>
        /// <param name="A">Input array</param>
        /// <param name="dim">[Optional] The working dimension. Default: (null) first non-singleton dimension of <paramref name="A"/> or dimension #0.</param>
        /// <returns>Complex array of the same size than <paramref name="A"/>.</returns>
        /// <remarks>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        public static Array<fcomplex > ifft(InArray<fcomplex > A, uint? dim = null) {

            using (Scope.Enter()) {
                Array<fcomplex > _A = A;
                Array<fcomplex > ret;
                if (_A.IsEmpty) {
                    ret = MathInternal.empty<fcomplex >(_A.Size);
                } else {
                    if (!dim.HasValue) {
                        dim = _A.S.WorkingDimension();
                    }
                    ret = MathInternal.FFTImplementation.FFTBackward1D(_A, dim.GetValueOrDefault());
                }
                return ret;
            }
        }
        /// <summary>
        /// Inverse fast fourier transform from fcomplex  hermitian input to real data.
        /// </summary>
        /// <param name="A">Complex hermitian input array (frequency domain).</param>
        /// <param name="dim">[Optional] The working dimension. Default: (null) first non-singleton dimension of <paramref name="A"/> or dimension #0.</param>
        /// <returns>Real array of the same size than <paramref name="A"/>.</returns>
        /// <remarks>
        /// <para>Since a transform of fcomplex  hermitian input data results in an 
        /// output having the imaginary part equal to zero, only the real part is 
        /// returned for convenience reasons.</para>
        /// <para>No check is made for A being hermitian! If A is not hermitian (by means 
        /// of round-off errors), the result will be incorrect!</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        public static Array<float> ifftsym(InArray<fcomplex > A, uint? dim = null) {
            using (Scope.Enter()) {
                Array<fcomplex > _A = A;
                Array<float> ret;
                if (_A.IsEmpty) {
                    ret = MathInternal.empty<float>(_A.Size);
                } else {
                    if (!dim.HasValue) {
                        dim = _A.S.WorkingDimension();
                    }
                    ret = MathInternal.FFTImplementation.FFTBackwSym1D(_A, dim.GetValueOrDefault());
                }
                return ret;
            }
        }
        #endregion

        #region fft2(A)
        /// <summary>
        /// Fast 2D discrete fourier transform.
        /// </summary>
        /// <param name="A">Real input array.</param>
        /// <returns>Complex array with transformation result.</returns>
        /// <remarks>
        /// <para>The 2D transformation is computed for the first 2 dimensions, regardless 
        /// of those dimensions being singleton or non-singleton dimensions. If A is an n-d array, 
        /// the transformation is repeated for trailing dimensions of A respectively. </para>
        /// <para>The output array returned will be fcomplex  hermitian.</para>
        /// <para>The two dimensional transformation is equivalent to repeatedly transforming 
        /// the columns and after that transforming the rows of A. However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        public static Array<fcomplex  > fft2(InArray<float > A) {

            using (Scope.Enter()) {
                Array<fcomplex > ret;
                if (A.IsEmpty) {
                    ret = MathInternal.empty<fcomplex >(A.Size);
                } else {
                    ret = MathInternal.FFTImplementation.FFTForward(A, 2);
                }
                return ret;
            }
        }
        /// <summary>
        /// Fast 2D discrete fourier transform.
        /// </summary>
        /// <param name="A">Complex source array.</param>
        /// <returns>Complex array with transformation result.</returns>
        /// <remarks>
        /// <para>The transformation is computed for the first 2 dimensions, regardless 
        /// of those dimensions being singleton or non-singleton dimensions. If A is an n-d array, 
        /// the transformation is repeated for trailing dimensions of A respectively. </para>
        /// <para>The two dimensional transformation is equivalent to repeatedly transforming 
        /// the columns and after that transforming the rows of A. However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        public static Array<fcomplex > fft2(InArray<fcomplex > A) {
            using (Scope.Enter()) {
                Array<fcomplex > _A = A, ret;
                if (_A.IsEmpty) {
                    ret = MathInternal.empty<fcomplex >(_A.Size);
                } else {
                    ret = MathInternal.FFTImplementation.FFTForward(_A, 2);
                }
                return ret;
            }
        }
        /// <summary>
        /// Inverse fast 2D discrete fourier transform.
        /// </summary>
        /// <param name="A">Complex source array.</param>
        /// <returns>Complex array with the inverse transformation of <paramref name="A"/>.</returns>
        /// <remarks>
        /// <para>The transformation is computed for the first 2 dimensions, regardless 
        /// of those dimensions being singleton or non-singleton. If A is an n-d array, 
        /// the transformation is repeated for trailing dimensions of A respectively. </para>
        /// <para>The two dimensional transformation is equivalent to repeatedly transforming 
        /// the columns and after that transforming the rows of A. However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        public static Array<fcomplex > ifft2(InArray<fcomplex > A) {
            using (Scope.Enter()) {
                Array<fcomplex > _A = A, ret;
                if (_A.IsEmpty) {
                    ret = MathInternal.empty<fcomplex >(_A.Size);
                } else {
                    ret = MathInternal.FFTImplementation.FFTBackward(_A, 2);
                }
                return ret; 
            }
        }
        /// <summary>
        /// Inverse, 2D, discrete fast fourier transform (hermitian input).
        /// </summary>
        /// <param name="A">Complex hermitian input array (frequency domain)</param>
        /// <returns>Real array with the inverse transformation of fcomplex , hermitian <paramref name="A"/>.</returns>
        /// <remarks>
        /// <para>Since a transform of fcomplex  hermitian input data results in the 
        /// output having the imaginary part equal to zero, only the real part is 
        /// returned for efficiency reasons.</para>
        /// <para>One way to create a hermitian array is to (forward) transform a real array.</para>
        /// <para>No check is made for A being hermitian! If A is not hermitian (within 
        /// round-off errors), the result will be incorrect!</para>
        /// <para>The transformation is computed for the first 2 dimensions, regardless 
        /// of those dimensions being singleton or non-singleton. If A is an n-d array, 
        /// the transformation is repeated for trailing dimensions of A respectively. </para>
        /// <para>The two dimensional transformation is equivalent to repeatedly transforming 
        /// the columns and after that transforming the rows of A. However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        public static Array<float> ifft2sym(InArray<fcomplex > A) {
            using (Scope.Enter()) {
                Array<fcomplex > _A = A;
                Array<float> ret;
                if (_A.IsEmpty) {
                    ret = MathInternal.empty<float>(_A.Size);
                } else {
                    ret = MathInternal.FFTImplementation.FFTBackwSym(_A, 2);
                }
                return ret;
            }
        }
        #endregion

        #region fft2(A,m,n)
        /// <summary>
        /// Fast fourier transform (2D)
        /// </summary>
        /// <param name="A">Input array</param>
        /// <param name="m">Transformation column length</param>
        /// <param name="n">Transformation row length</param>
        /// <returns>Transformation result, fcomplex  hermitian</returns>
        /// <remarks>
        /// <para>The transformation is computed for the first 2 dimensions, regardless 
        /// of those dimensions being singleton or non-singleton. If A is an n-d array, 
        /// the transformation is repeated for trailing dimensions of A respectively. </para>
        /// <para>The data to be transformed (based on the A array) are resized according to 
        /// the length parameter m and n. If m or n is larger then the length of the corresponding 
        /// dimension of A, zeros will be padded, otherwise the dimensions are truncated respectively. </para>
        /// <para>The two dimensional transformation is equivalent to repeatedly transforming 
        /// the columns and after that transforming the rows of A. However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">is thrown if n or m is negative. In versions greater 
        /// than 4.12 the API changed to <see cref="uint"/> length paramters, making this exception obsolete.</exception>
        public static Array< fcomplex > fft2(InArray< float> A, uint m, uint n) {

            using (Scope.Enter()) {
                Array< float> _A = A;
                Array < fcomplex  > ret;
                if (_A.IsEmpty) {
                    ret = MathInternal.empty< fcomplex >(_A.Size);
                } else {
                    Array<long> Asize = _A.shape;
                    Asize[0] = m; Asize[1] = n;
                    Array< float> resizedA = resize4Transform < float>(_A, Asize);
                    ret = MathInternal.FFTImplementation.FFTForward(resizedA, 2);
                }
                return ret; 
            }
        }
        /// <summary>
        /// Fast fourier transform (2D)
        /// </summary>
        /// <param name="A">input array</param>
        /// <param name="m">Transformation column length</param>
        /// <param name="n">Transformation row length</param>
        /// <returns>Transformation result</returns>
        /// <remarks>
        /// <para>The transformation is computed for the first 2 dimensions, regardless 
        /// of those dimensions being singleton or non-singleton. If A is an n-d array, 
        /// the transformation is repeated for trailing dimensions of A respectively. </para>
        /// <para>The data to be transformed (based on the A array) are resized according to 
        /// the length parameter m and n. If m or n is larger then the length of the corresponding 
        /// dimension of A, zeros will be padded, otherwise the dimensions are truncated respectively. </para>
        /// <para>The two dimensional transformation is equivalent to repeatedly transforming 
        /// the columns and after that transforming the rows of A. However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">is thrown if n or m is negative. In versions greater 
        /// than 4.12 the API changed to <see cref="uint"/> length paramters, making this exception obsolete.</exception>
        public static Array< fcomplex > fft2(InArray< fcomplex> A, uint m, uint n) {

            using (Scope.Enter()) {
                Array< fcomplex> _A = A;
                Array< fcomplex > ret;
                if (_A.IsEmpty) {
                    ret = MathInternal.empty< fcomplex >(_A.Size);
                } else {

                    Array<long> Asize = _A.shape;
                    Asize[0] = m; Asize[1] = n;
                    Array< fcomplex> resizedA = resize4Transform< fcomplex>(_A, Asize);
                    ret = MathInternal.FFTImplementation.FFTForward(resizedA, 2);
                }
                return ret;
            }
        }
        /// <summary>
        /// Inverse fast fourier transform (2D)
        /// </summary>
        /// <param name="A">Input array</param>
        /// <param name="m">Transformation column length</param>
        /// <param name="n">Transformation row length</param>
        /// <returns>Transformation result</returns>
        /// <remarks>
        /// <para>The transformation is computed for the first 2 dimensions, regardless 
        /// of those dimensions being singleton or non-singleton. If A is an n-d array, 
        /// the transformation is repeated for trailing dimensions of A respectively. </para>
        /// <para>The data to be transformed (based on the array A) are resized according to 
        /// the length parameter m and n. If m or n is larger then the length of the corresponding 
        /// dimension of A, zeros will be padded, otherwise the dimensions are truncated respectively. </para>
        /// <para>The two dimensional transformation is equivalent to repeatedly transforming 
        /// the columns and after that transforming the rows of A. However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying fft implementation.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">is thrown if n or m is negative. In versions greater 
        /// than 4.12 the API changed to <see cref="uint"/> length paramters, making this exception obsolete.</exception>
        public static Array< fcomplex > ifft2(InArray< fcomplex> A, uint m, uint n) {
            using (Scope.Enter()) {
                Array< fcomplex> _A = A;
                Array< fcomplex > ret;
                if (_A.IsEmpty) {
                    ret = MathInternal.empty< fcomplex >(_A.Size);
                } else {
                    Array<long> Asize = _A.shape;
                    Asize[0] = m; Asize[1] = n;
                    var resizedA = resize4Transform< fcomplex>(_A, Asize);
                    ret = MathInternal.FFTImplementation.FFTBackward(resizedA, 2);
                }
                return ret;

            }
        }
        /// <summary>
        /// Inverse fast fourier transform (2D)
        /// </summary>
        /// <param name="A">Complex hermitian input array, symmetric in first 2 dimensions</param>
        /// <param name="m">Transformation column length</param>
        /// <param name="n">Transformation row length</param>
        /// <returns>Transformation result</returns>
        /// <remarks>
        /// <para>The transformation is computed for the first 2 dimensions, regardless 
        /// of those dimensions being singleton or non-singleton. If A is an n-d array, 
        /// the transformation is repeated for trailing dimensions of A respectively. The 
        /// lengths of those trailing dimensions are not altered.</para>
        /// <para>Since a transform of fcomplex  hermitian input data results in the 
        /// output having the imaginary part equals zero, only the real part is 
        /// returned for convenience reasons.</para>
        /// <para>No check is made for A being hermitian! If A is not hermitian (by means 
        /// of round-off errors), the result will be wrong!</para>
        /// <para>The data to be transformed (based on the array A) are resized according to 
        /// the length parameter m and n. If m or n is larger then the length of the corresponding 
        /// dimension of A, zeros will be padded, otherwise the dimensions are truncated respectively. </para>
        /// <para>The two dimensional transformation is equivalent to repeatedly transforming 
        /// the columns and after that transforming the rows of A. However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">is thrown if n or m is negative. In versions greater 
        /// than 4.12 the API changed to <see cref="uint"/> length paramters, making this exception obsolete.</exception>
        public static Array< float> ifft2sym(InArray< fcomplex> A, uint m, uint n) {
            using (Scope.Enter()) {
                Array< fcomplex> _A = A;
                Array< float> ret;
                if (_A.IsEmpty) {
                    ret = MathInternal.empty< float>(_A.Size);
                } else {
                    Array<long> Asize = _A.shape;
                    Asize[0] = m; Asize[1] = n;
                    Array< fcomplex> resizedA = resize4Transform< fcomplex>(_A, Asize);
                    ret = MathInternal.FFTImplementation.FFTBackwSym(resizedA, 2);
                }
                return ret;

            }
        }
        #endregion

        #region fftn(A)
        /// <summary>
        /// Fast fourier transform (n-D)
        /// </summary>
        /// <param name="A">Input array, n-D</param>
        /// <returns>Transformation result, fcomplex  hermitian</returns>
        /// <remarks>
        /// <para>The n-dimensional transformation is computed for the n-dimensional array A. 
        /// This is equivalent to repeatedly (inplace) 
        /// computing one dimensional transformations along all dimensions of A.
        /// However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        public static Array< fcomplex > fftn(InArray< float> A) {
            using (Scope.Enter()) {
                Array< float> _A = A;
                Array< fcomplex > ret; 
                if (_A.IsEmpty) {
                    ret = MathInternal.empty< fcomplex >(_A.Size);
                }else {
                    ret = MathInternal.FFTImplementation.FFTForward(_A, _A.Size.NumberOfDimensions);
                }
                return ret;
            }
        }
        /// <summary>
        /// Fast fourier transform (n-D)
        /// </summary>
        /// <param name="A">Input array, n-D</param>
        /// <returns>Transformation result</returns>
        /// <remarks>
        /// <para>The n-dimensional transformation is computed for the n-dimensional array A. 
        /// This is equivalent to repeatedly (inplace) 
        /// computing one dimensional transformations along all dimensions of A.
        /// However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        public static Array< fcomplex > fftn(InArray< fcomplex> A) {
            using (Scope.Enter()) {
                Array< fcomplex> _A = A;
                Array< fcomplex > ret;
                if (_A.IsEmpty) {
                    ret = MathInternal.empty< fcomplex >(_A.Size);
                } else {
                    ret = MathInternal.FFTImplementation.FFTForward(_A, _A.Size.NumberOfDimensions);
                }
                return ret; 
            }
        }
        /// <summary>
        /// Inverse fast fourier transform (n-D)
        /// </summary>
        /// <param name="A">Input array, n-D (frequency domain)</param>
        /// <returns>Transformation result</returns>
        /// <remarks>
        /// <para>The n-dimensional inverse transformation is computed for the n-dimensional array A. 
        /// This is equivalent to repeatedly (inplace) 
        /// computing one dimensional transformations along all dimensions of A.
        /// However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        public static Array< fcomplex > ifftn(InArray< fcomplex> A) {
            using (Scope.Enter()) {
                Array< fcomplex> _A = A;
                Array< fcomplex > ret;
                if (_A.IsEmpty) {
                    ret = MathInternal.empty< fcomplex >(_A.Size);
                } else {
                    ret = MathInternal.FFTImplementation.FFTBackward(_A, _A.Size.NumberOfDimensions);
                }
                return ret; 
            }
        }
        /// <summary>
        /// Inverse fast fourier transform (n-D)
        /// </summary>
        /// <param name="A">Input array, n-D, fcomplex  hermitian (frequency domain)</param>
        /// <returns>Transformation result</returns>
        /// <remarks>
        /// <para>The n-dimensional inverse transformation is computed for the n-dimensional array A. 
        /// This is equivalent to repeatedly (inplace) 
        /// computing one dimensional transformations along all dimensions of A.
        /// However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>Since a transform of fcomplex  hermitian input data results in the 
        /// output having the imaginary part equals zero, only the real part is 
        /// returned for convenience reasons.</para>
        /// <para>No check is made for A being hermitian! If A is not hermitian (by means 
        /// of round-off errors), the result will be wrong!</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        public static Array< float> ifftnsym(InArray< fcomplex> A) {
            using (Scope.Enter()) {
                Array< fcomplex> _A = A;
                Array< float> ret; 
                if (_A.IsEmpty) {
                    ret = MathInternal.empty< float>(_A.Size);
                } else {
                    ret = MathInternal.FFTImplementation.FFTBackwSym(_A, _A.Size.NumberOfDimensions);
                }
                return ret;
            }
        }
        #endregion

        #region fftn(A, InArray<int> dims)
        /// <summary>
        /// Fast fourier transform (n-D, specific size)
        /// </summary>
        /// <param name="A">Input array, n-D</param>
        /// <param name="dims">Transformation lengths, specifies the length of the dimensions 
        /// for the transformation array. The length of dims must be &gt; or equal to the number of 
        /// dimensions of A. For elements in dim being smaller than corresponding dimension 
        /// length in A, the dimensions will be truncated, otherwise zeros will be padded.</param>
        /// <returns>Transformation result of size specified by 'dims' parameter, fcomplex  hermitian</returns>
        /// <remarks>
        /// <para>The n-dimensional transformation is computed for the n-dimensional array A. 
        /// Before the transform, the input is resized according to the 'dims' parameter. 
        /// Dimensions larger than corresponding entries in 'dim' are truncated, dimensions
        /// smaller than corresponding entries in 'dim' are zero padded.</para>
        /// <para>The n-dimensional transformation is equivalent to repeatedly (inplace) 
        /// computing one dimensional transformations along all dimensions of A.
        /// However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">is thrown if the 
        /// dim parameter is null, its length is less then the number of dimensions of A 
        /// or any element of dims is non-negative</exception>
        public static Array< fcomplex > fftn(InArray< float> A, InArray<long> dims) {
            using (Scope.Enter()) {
                Array< float> _A = A;
                Array< fcomplex > ret; 
                Array<long> _dims = dims;
                if (_A.IsEmpty) {
                    ret = MathInternal.empty< fcomplex >(_A.Size);
                } else {
                    Array< float> resizedA = resize4Transform< float>(_A, _dims);
                    ret = MathInternal.FFTImplementation.FFTForward(resizedA, (uint)_dims.S.NumberOfElements);
                }
                return ret; 
            }
        }
        /// <summary>
        /// Fast fourier transform (n-D, specific size)
        /// </summary>
        /// <param name="A">Tnput array, n-D</param>
        /// <param name="dims">Transformation lengths, specifies the length of the dimensions 
        /// for the transformation array. The length of dims must be &gt; or equal to  the number of 
        /// dimensions of A. For elements in dim being smaller than corresponding dimension 
        /// length in A, the dimensions will be truncated, otherwise zeros will be padded.</param>
        /// <returns>Transformation result of size specified by 'dims' parameter</returns>
        /// <remarks>
        /// <para>The n-dimensional transformation is computed for the n-dimensional array A. 
        /// Before the transform, the input is resized according to the 'dims' parameter. 
        /// Dimensions larger than corresponding entries in 'dim' are truncated, dimensions
        /// smaller than corresponding entries in 'dim' are zero padded.</para>
        /// <para>The n-dimensional transformation is equivalent to repeatedly (inplace) 
        /// computing one dimensional transformations along all dimensions of A.
        /// However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">is thrown if the 
        /// dim parameter is null, its length is smaller then the number of dimensions of A 
        /// or any element of dims is non-negative</exception>
        public static Array< fcomplex > fftn(InArray< fcomplex> A, InArray<long> dims) {
            using (Scope.Enter()) {
                Array< fcomplex> _A = A;
                Array< fcomplex > ret; 
                Array<long> _dims = dims;
                if (_A.IsEmpty) {
                    ret = MathInternal.empty< fcomplex >(_A.Size);
                } else {
                    Array< fcomplex> resizedA = resize4Transform< fcomplex>(_A, _dims);
                    ret = MathInternal.FFTImplementation.FFTForward(resizedA, (uint)_dims.S.NumberOfElements);
                }
                return ret; 
            }
        }
        /// <summary>
        /// Inverse fast fourier transform (n-D, specific size)
        /// </summary>
        /// <param name="A">Input array, n-D</param>
        /// <param name="dims">Transformation lengths, specifies the length of the dimensions 
        /// for the transformation array. The length of dims must be &gt; or equal to  the number of 
        /// dimensions of A. For elements in dim being smaller than corresponding dimension 
        /// length in A, the dimensions will be truncated, otherwise zeros will be padded.</param>
        /// <returns>Transformation result of size specified by 'dims' parameter</returns>
        /// <remarks>
        /// <para>The n-dimensional transformation is computed for the n-dimensional array A. 
        /// Before the transform, the input is resized according to the 'dims' parameter. 
        /// Dimensions larger than corresponding entries in 'dim' are truncated, dimensions
        /// smaller than corresponding entries in 'dim' are zero padded.</para>
        /// <para>The n-dimensional transformation is equivalent to repeatedly (inplace) 
        /// computing one dimensional transformations along all dimensions of A.
        /// However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">is thrown if the 
        /// dim parameter is null, its length is less then the number of dimensions of A 
        /// or any element of dims is non-negative</exception>
        public static Array< fcomplex > ifftn(InArray< fcomplex> A, InArray<long> dims) {
            using (Scope.Enter()) {
                Array< fcomplex> _A = A;
                Array< fcomplex > ret; 
                Array<long> _dims = dims;
                if (_A.IsEmpty) {
                    ret = MathInternal.empty< fcomplex >(_A.Size);
                } else {
                    Array< fcomplex> resizedA = resize4Transform< fcomplex>(_A, _dims);
                    return MathInternal.FFTImplementation.FFTBackward(resizedA, (uint)_dims.S.NumberOfElements);
                }
                return ret; 
            }
        }
        /// <summary>
        /// Inverse fast fourier transform (n-D, fcomplex  hermitian, specific size)
        /// </summary>
        /// <param name="A">Complex hermitian input array, n-D</param>
        /// <param name="dims">Transformation lengths, specifies the length of the dimensions 
        /// for the transformation array. The length of dims must be &gt; or equal to  the number of 
        /// dimensions of A. For elements in dim being smaller than corresponding dimension 
        /// length in A, the dimensions will be truncated, otherwise zeros will be padded.</param>
        /// <returns>Transformation result, real array of the size specified by the 'dims' parameter</returns>
        /// <remarks>
        /// <para>The n-dimensional transformation is computed for the n-dimensional array A. 
        /// Before the transform, the input is resized according to the 'dims' parameter. 
        /// Dimensions larger than corresponding entries in 'dim' are truncated, dimensions
        /// smaller than corresponding entries in 'dim' are zero padded.</para>
        /// <para>The n-dimensional transformation is equivalent to repeatedly (inplace) 
        /// computing one dimensional transformations along all dimensions of A.
        /// However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>Since a transform of fcomplex  hermitian input data results in the 
        /// output having the imaginary part equals zero, only the real part is 
        /// returned for convenience reasons.</para>
        /// <para>No check is made for A being hermitian! If A is not hermitian (by means 
        /// of round-off errors), the result will be wrong!</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">is thrown if the 
        /// dim parameter is null, its length is less then the number of dimensions of A 
        /// or any element of dims is non-negative</exception>
        public static Array< float> ifftnsym(InArray< fcomplex> A, InArray<long> dims) {
            using (Scope.Enter()) {
                Array< fcomplex> _A = A;
                Array< float> ret; 
                Array<long> _dims = dims;
                if (_A.IsEmpty) {
                    ret = empty< float>(_A.Size);
                } else {
                    Array< fcomplex> resizedA = resize4Transform< fcomplex>(_A, _dims);
                    ret = FFTImplementation.FFTBackwSym(resizedA, (uint)_dims.S.NumberOfElements);
                }
                return ret;
            }
        }
        #endregion

        #region OBSOLETE fftn(A, params dims) 
        /// <summary>
        /// Fast fourier transform (n-D, specific size)
        /// </summary>
        /// <param name="A">Input array, n-D</param>
        /// <param name="dims">Transformation lengths, specifies the length of the dimensions 
        /// for the transformation array. The length of dims must be &gt; or equal to the number of 
        /// dimensions of A. For elements in dim being smaller than corresponding dimension 
        /// length in A, the dimensions will be truncated, otherwise zeros will be padded.</param>
        /// <returns>Transformation result of size specified by 'dims' parameter, fcomplex  hermitian</returns>
        /// <remarks>
        /// <para>The n-dimensional transformation is computed for the n-dimensional array A. 
        /// Before the transform, the input is resized according to the 'dims' parameter. 
        /// Dimensions larger than corresponding entries in 'dim' are truncated, dimensions
        /// smaller than corresponding entries in 'dim' are zero padded.</para>
        /// <para>The n-dimensional transformation is equivalent to repeatedly (inplace) 
        /// computing one dimensional transformations along all dimensions of A.
        /// However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">is thrown if the 
        /// dim parameter is null, its length is less then the number of dimensions of A 
        /// or any element of dims is non-negative</exception>
        [Obsolete("Use fftn(InArray<T> A, InArray<uint> dims) instead! The function ILMath.vector<T>() can be used to replace 'params' arguments.")]
        public static Array< fcomplex > fftn(InArray< float> A, params uint[] dims) {
            using (Scope.Enter(A)) {
                if (A.IsEmpty) return MathInternal.empty< fcomplex >(A.Size);
                Array< float> resizedA = resize4Transform(A, toint64(vector(dims)));
                return MathInternal.FFTImplementation.FFTForward(resizedA, (uint)dims.Length);
            }
        }
        /// <summary>
        /// Fast fourier transform (n-D, specific size)
        /// </summary>
        /// <param name="A">Tnput array, n-D</param>
        /// <param name="dims">Transformation lengths, specifies the length of the dimensions 
        /// for the transformation array. The length of dims must be &gt; or equal to  the number of 
        /// dimensions of A. For elements in dim being smaller than corresponding dimension 
        /// length in A, the dimensions will be truncated, otherwise zeros will be padded.</param>
        /// <returns>Transformation result of size specified by 'dims' parameter</returns>
        /// <remarks>
        /// <para>The n-dimensional transformation is computed for the n-dimensional array A. 
        /// Before the transform, the input is resized according to the 'dims' parameter. 
        /// Dimensions larger than corresponding entries in 'dim' are truncated, dimensions
        /// smaller than corresponding entries in 'dim' are zero padded.</para>
        /// <para>The n-dimensional transformation is equivalent to repeatedly (inplace) 
        /// computing one dimensional transformations along all dimensions of A.
        /// However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">is thrown if the 
        /// dim parameter is null, its length is smaller then the number of dimensions of A 
        /// or any element of dims is non-negative</exception>
        [Obsolete("Use fftn(InArray<T> A, InArray<uint> dims) instead! The function ILMath.vector<T>() can be used to replace 'params' arguments.")]
        public static Array< fcomplex > fftn(InArray< fcomplex> A, params uint[] dims) {
            using (Scope.Enter(A)) {
                if (A.IsEmpty) return MathInternal.empty< fcomplex >(A.Size);
                Array< fcomplex> resizedA = resize4Transform(A, toint64(vector(dims)));
                return MathInternal.FFTImplementation.FFTForward(resizedA, (uint)dims.Length);
            }
        }
        /// <summary>
        /// Inverse fast fourier transform (n-D, specific size)
        /// </summary>
        /// <param name="A">Input array, n-D</param>
        /// <param name="dims">Transformation lengths, specifies the length of the dimensions 
        /// for the transformation array. The length of dims must be &gt; or equal to  the number of 
        /// dimensions of A. For elements in dim being smaller than corresponding dimension 
        /// length in A, the dimensions will be truncated, otherwise zeros will be padded.</param>
        /// <returns>Transformation result of size specified by 'dims' parameter</returns>
        /// <remarks>
        /// <para>The n-dimensional transformation is computed for the n-dimensional array A. 
        /// Before the transform, the input is resized according to the 'dims' parameter. 
        /// Dimensions larger than corresponding entries in 'dim' are truncated, dimensions
        /// smaller than corresponding entries in 'dim' are zero padded.</para>
        /// <para>The n-dimensional transformation is equivalent to repeatedly (inplace) 
        /// computing one dimensional transformations along all dimensions of A.
        /// However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">is thrown if the 
        /// dim parameter is null, its length is less then the number of dimensions of A 
        /// or any element of dims is non-negative</exception>
        [Obsolete("Use ifftn(InArray<T> A, InArray<uint> dims) instead! The function ILMath.vector<T>() can be used to replace 'params' arguments.")]
        public static Array< fcomplex > ifftn(InArray< fcomplex> A, params uint[] dims) {
            using (Scope.Enter(A)) {
                if (A.IsEmpty) return MathInternal.empty< fcomplex >(A.Size);
                Array< fcomplex> resizedA = resize4Transform(A, toint64(vector(dims)));
                return MathInternal.FFTImplementation.FFTBackward(resizedA, (uint)dims.Length);
            }
        }
        /// <summary>
        /// Inverse fast fourier transform (n-D, fcomplex  hermitian, specific size)
        /// </summary>
        /// <param name="A">Complex hermitian input array, n-D</param>
        /// <param name="dims">Transformation lengths, specifies the length of the dimensions 
        /// for the transformation array. The length of dims must be &gt; or equal to  the number of 
        /// dimensions of A. For elements in dim being smaller than corresponding dimension 
        /// length in A, the dimensions will be truncated, otherwise zeros will be padded.</param>
        /// <returns>Transformation result, real array of the size specified by the 'dims' parameter</returns>
        /// <remarks>
        /// <para>The n-dimensional transformation is computed for the n-dimensional array A. 
        /// Before the transform, the input is resized according to the 'dims' parameter. 
        /// Dimensions larger than corresponding entries in 'dim' are truncated, dimensions
        /// smaller than corresponding entries in 'dim' are zero padded.</para>
        /// <para>The n-dimensional transformation is equivalent to repeatedly (inplace) 
        /// computing one dimensional transformations along all dimensions of A.
        /// However, using this 
        /// function may be of magnitudes faster than using 1D transformations. This 
        /// depends on the algorithm and API provided by the underlying native library.</para>
        /// <para>Since a transform of fcomplex  hermitian input data results in the 
        /// output having the imaginary part equals zero, only the real part is 
        /// returned for convenience reasons.</para>
        /// <para>No check is made for A being hermitian! If A is not hermitian (by means 
        /// of round-off errors), the result will be wrong!</para>
        /// <para>The forward fourier transform and the inverse fourier transform of 
        /// a given data array A are mathematically equivalent. It's only a 
        /// scaling factor which is needed to make sure, A equals ifft(fft(A)). That 
        /// scaling is introduced in the inverse transform.</para>
        /// <para>The transformation is computed by use of the fully managed translation of FFTPACK Fortran codes (version 5, adopted for multiple precision). 
        /// Alternatively and on Windows/x64, one may utilize a native, optimized version library by referencing the ILNumerics.Core.Native nuget package. 
        /// The actual implementation used is automatically selected at ILNumerics startup and accessed via the 
        /// static member ILMath.FFT. See the online documentation for more 
        /// details in how to tune/configure and select dedicated native libraries.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">is thrown if the 
        /// dim parameter is null, its length is less then the number of dimensions of A 
        /// or any element of dims is non-negative</exception>
        [Obsolete("Use ifftnsym(InArray<T> A, InArray<uint> dims) instead! The function ILMath.vector<T>() can be used to replace 'params' arguments.")]
        public static Array< float> ifftnsym(InArray< fcomplex> A, params uint[] dims) {
            using (Scope.Enter(A)) {
                if (A.IsEmpty) return empty< float>(A.Size);
                Array< fcomplex> resizedA = resize4Transform(A, toint64(vector(dims)));
                return FFTImplementation.FFTBackwSym(resizedA, (uint)dims.Length);
            }
        }
#endregion


#endregion HYCALPER AUTO GENERATED CODE

#region private helper
        /// <summary>
        /// This expects the first 2 elements of <paramref name="size"/> to store m and n parameters for 2d ffts to be transformed over slices of A.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="A"></param>
        /// <param name="size"></param>
        /// <returns>Resized version of A.</returns>
        /// <remarks></remarks>
        private static Array<T> resize4Transform<T>(InArray<T> A, InArray<long> size) {
            using (Scope.Enter(A, size)) {
                if (isnull(size) || size.S.NumberOfElements < A.Size.NumberOfDimensions)
                    throw new ArgumentException("length of output dimensions must be &gt; or equal to number of dimensions of input array!");

                // check if we really have to do anything
                uint minDimsLen = (uint)Math.Min(size.S.NumberOfElements, A.Size.NumberOfDimensions);
                uint i = 0;
                for (; i < minDimsLen; i++) {
                    if (A.S[i] != size.GetValue(i)) {
                        break;
                    }
                }
                if (i == minDimsLen && size.S.NumberOfElements == A.S.NumberOfDimensions) {
                    return A;
                }
                if (prodall(size) == 0) {
                    return empty<T>(size, StorageOrders.ColumnMajor); 
                }
                // ok, we must create a new array 
                Array<T> ret = zeros<T>(size, StorageOrders.ColumnMajor); 

                if (ret.S.NumberOfElements > 0) {
                    DimSpec[] Lindices = new DimSpec[minDimsLen]; // <-- ??? :| -> GC!
                    DimSpec[] Rindices = new DimSpec[minDimsLen]; // <-- ??? :| -> GC!
                    for (i = 0; i < minDimsLen; i++) {
                        //if (size[i] == 0) return MathInternal.empty<T>(newDimensions);  <-- checked above
                        Lindices[i] = r(0, Math.Min(A.Size[i] - 1, size.GetValue(i) - 1));
                        Rindices[i] = r(0, Math.Min(A.Size[i] - 1, size.GetValue(i) - 1));
                    }
                    ret[Lindices] = A[Rindices];
                }
                return ret;
            }
        }

#endregion

    }
}
