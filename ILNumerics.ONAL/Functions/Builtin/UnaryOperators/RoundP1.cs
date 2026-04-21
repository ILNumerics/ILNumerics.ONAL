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
using ILNumerics.Core.Arrays;
using ILNumerics.Core.Global;
using ILNumerics.Core.StorageLayer;
using System;
using System.Security;
using System.Threading;

/*!HC:TYPELIST:
<hycalper>
    <type>
    <source locate="here">
        double
    </source>
        <destination>float</destination>
        <destination>complex</destination>
        <destination>fcomplex</destination>
    </type>
    <type>
    <source locate="here">
        Double
    </source>
        <destination>Single</destination>
        <destination>Complex</destination>
        <destination>FComplex</destination>
    </type>
    <type>
        <source locate="after">
            funcname
        </source>
        <destination>round</destination>
        <destination>round</destination>
        <destination>round</destination>
    </type>
    <type>
        <source locate="after" endmark=" ()">
            operatorFunc
        </source>
        <destination>(float)Math.Round</destination>
        <destination>complex.Round</destination>
        <destination>fcomplex.Round</destination>
    </type>
</hycalper>
*/

namespace ILNumerics.Core.Functions.Builtin {

    #region HYCALPER LOOPSTART UNARY_OPERATOR_TEMPLATE
    internal static partial class MathInternal {

        /// <summary>Round elements to a specified number of fractional digits.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="decimals">[Optional] number of fractional digits to round to. Default: (0) round to integers.</param>
        /// <returns>Array of same size as A with elements of A rounded towards the nearest even integer value.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The storage order of the array returned depends on the order of <paramref name="A"/>. If 
        /// <paramref name="A"/>.<see cref="Size.IsContinuous"/> is <c>true</c> the array returned will have the same <see cref="Size.StorageOrder"/> 
        /// as <paramref name="A"/>. Otherwise, the elements of the returned array will be reordered in the order defined by 
        /// <see cref="Settings.DefaultStorageOrder"/> (default: <see cref="StorageOrders.ColumnMajor"/>).</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered. However, if <paramref name="A"/>'s storage is suitable and not 
        /// shared by other arrays its memory will be used for the result and the operation is performed 'inplace'.</para></remarks>
        internal unsafe static Array<double> round(BaseArray<double> A, int decimals = 0) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.RoundP1.Double.Instance.operate(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, decimals);
        }
    }
    namespace InnerLoops {

        namespace RoundP1 {

            internal class Double :

            UnaryBaseOOPlaceParameter1<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>, int>
            {

                internal static Double Instance = new Double();

                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsd, int decimals) {

                    uint ndims = (uint)bsd[0];
                    if (ndims == 0) {
                        System.Diagnostics.Debug.Assert(start == 0); 
                        *(double*)(pDest) = /*!HC:operatorFunc*/ Math.Round(*(double*)pSrc, decimals) /**/;
                        return; 
                    }

                    long* dims = bsd + 3;
                    long* strides = dims + ndims;
                    long stride0 = strides[0];
                    long* cur = stackalloc long[(int)ndims];

                    // figure out the dimension index position for start
                    cur[0] = start % dims[0];
                    long f = start / dims[0];
                    long higdims = 0;
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % dims[i];
                        f /= dims[i];
                        higdims += cur[i] * strides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);


                    double* pOut = (double*)pDest + start;

                    while (true) {

                        byte* pIn = pSrc + higdims + cur[0] * stride0;

                        // iteration length limited to either the dimension lengths or the end of the requested chunk 
                        long leadLen = Math.Min(len, dims[0] - cur[0]);
                        len -= leadLen;

                        while (leadLen-- > 0) {
                            *pOut++ = /*!HC:operatorFunc*/ Math.Round(*(double*)pIn, decimals) /**/;
                            pIn += stride0;
                        }
                        if (len == 0) {
                            break;
                        }
                        // reset initial offset in lead dimension after first iteration
                        cur[0] = 0;  // TODO: how can we improve this? Subseqent stores are redundant!

                        // increase higher dims
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d] - 1) {
                                higdims += strides[d];
                                cur[d]++;
                                break;
                            } else {
                                cur[d] = 0;
                                higdims -= strides[d] * (dims[d] - 1);
                                d++;
                            }
                        }
                    }
                }
            }
        }
    }
    #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
    internal static partial class MathInternal {

        /// <summary>Round elements to a specified number of fractional digits.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="decimals">[Optional] number of fractional digits to round to. Default: (0) round to integers.</param>
        /// <returns>Array of same size as A with elements of A rounded towards the nearest even integer value.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The storage order of the array returned depends on the order of <paramref name="A"/>. If 
        /// <paramref name="A"/>.<see cref="Size.IsContinuous"/> is <c>true</c> the array returned will have the same <see cref="Size.StorageOrder"/> 
        /// as <paramref name="A"/>. Otherwise, the elements of the returned array will be reordered in the order defined by 
        /// <see cref="Settings.DefaultStorageOrder"/> (default: <see cref="StorageOrders.ColumnMajor"/>).</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered. However, if <paramref name="A"/>'s storage is suitable and not 
        /// shared by other arrays its memory will be used for the result and the operation is performed 'inplace'.</para></remarks>
        internal unsafe static Array<fcomplex> round(BaseArray<fcomplex> A, int decimals = 0) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.RoundP1.FComplex.Instance.operate(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, decimals);
        }
    }
    namespace InnerLoops {

        namespace RoundP1 {

            internal class FComplex :

            UnaryBaseOOPlaceParameter1<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>, int>
            {

                internal static FComplex Instance = new FComplex();

                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsd, int decimals) {

                    uint ndims = (uint)bsd[0];
                    if (ndims == 0) {
                        System.Diagnostics.Debug.Assert(start == 0); 
                        *(fcomplex*)(pDest) =  fcomplex.Round(*(fcomplex*)pSrc, decimals) /**/;
                        return; 
                    }

                    long* dims = bsd + 3;
                    long* strides = dims + ndims;
                    long stride0 = strides[0];
                    long* cur = stackalloc long[(int)ndims];

                    // figure out the dimension index position for start
                    cur[0] = start % dims[0];
                    long f = start / dims[0];
                    long higdims = 0;
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % dims[i];
                        f /= dims[i];
                        higdims += cur[i] * strides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);


                    fcomplex* pOut = (fcomplex*)pDest + start;

                    while (true) {

                        byte* pIn = pSrc + higdims + cur[0] * stride0;

                        // iteration length limited to either the dimension lengths or the end of the requested chunk 
                        long leadLen = Math.Min(len, dims[0] - cur[0]);
                        len -= leadLen;

                        while (leadLen-- > 0) {
                            *pOut++ =  fcomplex.Round(*(fcomplex*)pIn, decimals) /**/;
                            pIn += stride0;
                        }
                        if (len == 0) {
                            break;
                        }
                        // reset initial offset in lead dimension after first iteration
                        cur[0] = 0;  // TODO: how can we improve this? Subseqent stores are redundant!

                        // increase higher dims
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d] - 1) {
                                higdims += strides[d];
                                cur[d]++;
                                break;
                            } else {
                                cur[d] = 0;
                                higdims -= strides[d] * (dims[d] - 1);
                                d++;
                            }
                        }
                    }
                }
            }
        }
    }
    internal static partial class MathInternal {

        /// <summary>Round elements to a specified number of fractional digits.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="decimals">[Optional] number of fractional digits to round to. Default: (0) round to integers.</param>
        /// <returns>Array of same size as A with elements of A rounded towards the nearest even integer value.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The storage order of the array returned depends on the order of <paramref name="A"/>. If 
        /// <paramref name="A"/>.<see cref="Size.IsContinuous"/> is <c>true</c> the array returned will have the same <see cref="Size.StorageOrder"/> 
        /// as <paramref name="A"/>. Otherwise, the elements of the returned array will be reordered in the order defined by 
        /// <see cref="Settings.DefaultStorageOrder"/> (default: <see cref="StorageOrders.ColumnMajor"/>).</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered. However, if <paramref name="A"/>'s storage is suitable and not 
        /// shared by other arrays its memory will be used for the result and the operation is performed 'inplace'.</para></remarks>
        internal unsafe static Array<complex> round(BaseArray<complex> A, int decimals = 0) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.RoundP1.Complex.Instance.operate(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, decimals);
        }
    }
    namespace InnerLoops {

        namespace RoundP1 {

            internal class Complex :

            UnaryBaseOOPlaceParameter1<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>, int>
            {

                internal static Complex Instance = new Complex();

                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsd, int decimals) {

                    uint ndims = (uint)bsd[0];
                    if (ndims == 0) {
                        System.Diagnostics.Debug.Assert(start == 0); 
                        *(complex*)(pDest) =  complex.Round(*(complex*)pSrc, decimals) /**/;
                        return; 
                    }

                    long* dims = bsd + 3;
                    long* strides = dims + ndims;
                    long stride0 = strides[0];
                    long* cur = stackalloc long[(int)ndims];

                    // figure out the dimension index position for start
                    cur[0] = start % dims[0];
                    long f = start / dims[0];
                    long higdims = 0;
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % dims[i];
                        f /= dims[i];
                        higdims += cur[i] * strides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);


                    complex* pOut = (complex*)pDest + start;

                    while (true) {

                        byte* pIn = pSrc + higdims + cur[0] * stride0;

                        // iteration length limited to either the dimension lengths or the end of the requested chunk 
                        long leadLen = Math.Min(len, dims[0] - cur[0]);
                        len -= leadLen;

                        while (leadLen-- > 0) {
                            *pOut++ =  complex.Round(*(complex*)pIn, decimals) /**/;
                            pIn += stride0;
                        }
                        if (len == 0) {
                            break;
                        }
                        // reset initial offset in lead dimension after first iteration
                        cur[0] = 0;  // TODO: how can we improve this? Subseqent stores are redundant!

                        // increase higher dims
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d] - 1) {
                                higdims += strides[d];
                                cur[d]++;
                                break;
                            } else {
                                cur[d] = 0;
                                higdims -= strides[d] * (dims[d] - 1);
                                d++;
                            }
                        }
                    }
                }
            }
        }
    }
    internal static partial class MathInternal {

        /// <summary>Round elements to a specified number of fractional digits.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="decimals">[Optional] number of fractional digits to round to. Default: (0) round to integers.</param>
        /// <returns>Array of same size as A with elements of A rounded towards the nearest even integer value.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The storage order of the array returned depends on the order of <paramref name="A"/>. If 
        /// <paramref name="A"/>.<see cref="Size.IsContinuous"/> is <c>true</c> the array returned will have the same <see cref="Size.StorageOrder"/> 
        /// as <paramref name="A"/>. Otherwise, the elements of the returned array will be reordered in the order defined by 
        /// <see cref="Settings.DefaultStorageOrder"/> (default: <see cref="StorageOrders.ColumnMajor"/>).</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered. However, if <paramref name="A"/>'s storage is suitable and not 
        /// shared by other arrays its memory will be used for the result and the operation is performed 'inplace'.</para></remarks>
        internal unsafe static Array<float> round(BaseArray<float> A, int decimals = 0) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.RoundP1.Single.Instance.operate(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, decimals);
        }
    }
    namespace InnerLoops {

        namespace RoundP1 {

            internal class Single :

            UnaryBaseOOPlaceParameter1<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>, int>
            {

                internal static Single Instance = new Single();

                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsd, int decimals) {

                    uint ndims = (uint)bsd[0];
                    if (ndims == 0) {
                        System.Diagnostics.Debug.Assert(start == 0); 
                        *(float*)(pDest) =  (float)Math.Round(*(float*)pSrc, decimals) /**/;
                        return; 
                    }

                    long* dims = bsd + 3;
                    long* strides = dims + ndims;
                    long stride0 = strides[0];
                    long* cur = stackalloc long[(int)ndims];

                    // figure out the dimension index position for start
                    cur[0] = start % dims[0];
                    long f = start / dims[0];
                    long higdims = 0;
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % dims[i];
                        f /= dims[i];
                        higdims += cur[i] * strides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);


                    float* pOut = (float*)pDest + start;

                    while (true) {

                        byte* pIn = pSrc + higdims + cur[0] * stride0;

                        // iteration length limited to either the dimension lengths or the end of the requested chunk 
                        long leadLen = Math.Min(len, dims[0] - cur[0]);
                        len -= leadLen;

                        while (leadLen-- > 0) {
                            *pOut++ =  (float)Math.Round(*(float*)pIn, decimals) /**/;
                            pIn += stride0;
                        }
                        if (len == 0) {
                            break;
                        }
                        // reset initial offset in lead dimension after first iteration
                        cur[0] = 0;  // TODO: how can we improve this? Subseqent stores are redundant!

                        // increase higher dims
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d] - 1) {
                                higdims += strides[d];
                                cur[d]++;
                                break;
                            } else {
                                cur[d] = 0;
                                higdims -= strides[d] * (dims[d] - 1);
                                d++;
                            }
                        }
                    }
                }
            }
        }
    }

#endregion HYCALPER AUTO GENERATED CODE



}
