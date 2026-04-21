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
        <destination>double</destination>
        <destination>float</destination>
        <destination>complex</destination>
        <destination>fcomplex</destination>
    </type>
    <type>
    <source locate="here">
        Double
    </source>
        <destination>Double</destination>
        <destination>Single</destination>
        <destination>Complex</destination>
        <destination>FComplex</destination>
    </type>
    <type>
        <source locate="after">
            funcname
        </source>
        <destination>isinf</destination>
        <destination>isinf</destination>
        <destination>isinf</destination>
        <destination>isinf</destination>
    </type>
    <type>
        <source locate="after" endmark=" ()">
            operatorfunc
        </source>
        <destination>double.IsInfinity</destination>
        <destination>float.IsInfinity</destination>
        <destination>complex.IsInfinity</destination>
        <destination>fcomplex.IsInfinity</destination>
    </type>
    <type>
        <source locate="after" endmark=" ();">
            postOp
        </source>
        <destination>? (byte)1:(byte)0</destination>
        <destination>? (byte)1:(byte)0</destination>
        <destination>? (byte)1:(byte)0</destination>
        <destination>? (byte)1:(byte)0</destination>
    </type>
    <type>
        <source locate="after" endmark=" :,*.()">
            innerloopname
        </source>
        <destination>IsInfinity</destination>
        <destination>IsInfinity</destination>
        <destination>IsInfinity</destination>
        <destination>IsInfinity</destination>
    </type>
    <type>
        <source locate="comment">
            summary
        </source>
        <destination>Checks for infinite elements.</destination>
        <destination>Checks for infinite elements.</destination>
        <destination>Checks for infinite elements.</destination>
        <destination>Checks for infinite elements.</destination>
    </type>
    <type>
        <source locate="comment">
            returns
        </source>
        <destination><![CDATA[Logical array of the same size as <paramref name="A"/> with 'true' element where elements in <paramref name="A"/> equal +/-infinity.]]></destination>
        <destination><![CDATA[Logical array of the same size as <paramref name="A"/> with 'true' element where elements in <paramref name="A"/> equal +/-infinity.]]></destination>
        <destination><![CDATA[Logical array of the same size as <paramref name="A"/> with 'true' element where elements in <paramref name="A"/> equal +/-infinity.]]></destination>
        <destination><![CDATA[Logical array of the same size as <paramref name="A"/> with 'true' element where elements in <paramref name="A"/> equal +/-infinity.]]></destination>
    </type>
</hycalper>
*/

namespace ILNumerics.Core.Functions.Builtin {

    #region HYCALPER LOOPSTART UNARY_OPERATOR_TEMPLATE_LOGICAL@Functions\Builtin\UnaryOperators\IsFinite.cs

    #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
    internal static partial class MathInternal {

        /// <summary>Checks for infinite elements.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Logical array of the same size as <paramref name="A"/> with 'true' element where elements in <paramref name="A"/> equal +/-infinity.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The storage order of the array returned depends on the order of <paramref name="A"/>. If 
        /// <paramref name="A"/>.<see cref="Size.IsContinuous"/> is <c>true</c> the array returned will have the same <see cref="Size.StorageOrder"/> 
        /// as <paramref name="A"/>. Otherwise, the elements of the returned array will be reordered in the order defined by 
        /// <see cref="Settings.DefaultStorageOrder"/> (default: <see cref="StorageOrders.ColumnMajor"/>).</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para><b>Handling NaN elements</b> The functions <see cref="isfinite(BaseArray{fcomplex})"/>, <see cref="isinf(BaseArray{fcomplex})"/>, 
        /// <see cref="isposinf(BaseArray{fcomplex})"/> and <see cref="isneginf(BaseArray{fcomplex})"/> return false for NaN (not a number) 
        /// special floating point value elements.</para>
        /// </remarks>
        
        internal unsafe static Logical  isinf(BaseArray<fcomplex> A) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.IsInfinity.FComplex.Instance.operate(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>);
        }
    }
    namespace InnerLoops {

        namespace  IsInfinity {

            internal class FComplex :
            UnaryBaseOutOfPlaceLogical<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>
            {

                internal static FComplex Instance = new FComplex();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsd, long* nrTrues) {

                    uint ndims = (uint)bsd[0];
                    if (ndims == 0) {
                        // special case: np scalar
                        System.Diagnostics.Debug.Assert(start == 0);
                        System.Diagnostics.Debug.Assert(len == 1);
                        if ((*(pDest) =  fcomplex.IsInfinity(*(fcomplex*)pSrc) ? (byte)1:(byte)0) != 0) {
                            nrTrues[0] = 1;
                        } else {
                            nrTrues[0] = 0; 
                        }
                        return; 
                    }
                    long* dims = bsd + 3;
                    long* strides = dims + ndims;
                    long stride0 = strides[0];
                    long* cur = stackalloc long[(int)ndims];

                    // figure out the dimension index position for start
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");

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


                    byte* pOut = (byte*)pDest + start;
                    long trueCount = 0; 

                    while (true) {

                        byte* pIn = pSrc + higdims + cur[0] * stride0;

                        // iteration length limited to either the dimension lengths or the end of the requested chunk 
                        long leadLen = Math.Min(len, dims[0] - cur[0]);
                        len -= leadLen;

                        while (leadLen-- > 0) {
                            if ((*pOut =  fcomplex.IsInfinity(*(fcomplex*)pIn) ? (byte)1:(byte)0) != 0) trueCount++;
                            pOut++;  pIn += stride0;
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
                    nrTrues[0] = trueCount; 
                }
            }
        }
    }

    internal static partial class MathInternal {

        /// <summary>Checks for infinite elements.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Logical array of the same size as <paramref name="A"/> with 'true' element where elements in <paramref name="A"/> equal +/-infinity.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The storage order of the array returned depends on the order of <paramref name="A"/>. If 
        /// <paramref name="A"/>.<see cref="Size.IsContinuous"/> is <c>true</c> the array returned will have the same <see cref="Size.StorageOrder"/> 
        /// as <paramref name="A"/>. Otherwise, the elements of the returned array will be reordered in the order defined by 
        /// <see cref="Settings.DefaultStorageOrder"/> (default: <see cref="StorageOrders.ColumnMajor"/>).</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para><b>Handling NaN elements</b> The functions <see cref="isfinite(BaseArray{complex})"/>, <see cref="isinf(BaseArray{complex})"/>, 
        /// <see cref="isposinf(BaseArray{complex})"/> and <see cref="isneginf(BaseArray{complex})"/> return false for NaN (not a number) 
        /// special floating point value elements.</para>
        /// </remarks>
        
        internal unsafe static Logical  isinf(BaseArray<complex> A) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.IsInfinity.Complex.Instance.operate(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>);
        }
    }
    namespace InnerLoops {

        namespace  IsInfinity {

            internal class Complex :
            UnaryBaseOutOfPlaceLogical<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>
            {

                internal static Complex Instance = new Complex();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsd, long* nrTrues) {

                    uint ndims = (uint)bsd[0];
                    if (ndims == 0) {
                        // special case: np scalar
                        System.Diagnostics.Debug.Assert(start == 0);
                        System.Diagnostics.Debug.Assert(len == 1);
                        if ((*(pDest) =  complex.IsInfinity(*(complex*)pSrc) ? (byte)1:(byte)0) != 0) {
                            nrTrues[0] = 1;
                        } else {
                            nrTrues[0] = 0; 
                        }
                        return; 
                    }
                    long* dims = bsd + 3;
                    long* strides = dims + ndims;
                    long stride0 = strides[0];
                    long* cur = stackalloc long[(int)ndims];

                    // figure out the dimension index position for start
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");

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


                    byte* pOut = (byte*)pDest + start;
                    long trueCount = 0; 

                    while (true) {

                        byte* pIn = pSrc + higdims + cur[0] * stride0;

                        // iteration length limited to either the dimension lengths or the end of the requested chunk 
                        long leadLen = Math.Min(len, dims[0] - cur[0]);
                        len -= leadLen;

                        while (leadLen-- > 0) {
                            if ((*pOut =  complex.IsInfinity(*(complex*)pIn) ? (byte)1:(byte)0) != 0) trueCount++;
                            pOut++;  pIn += stride0;
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
                    nrTrues[0] = trueCount; 
                }
            }
        }
    }

    internal static partial class MathInternal {

        /// <summary>Checks for infinite elements.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Logical array of the same size as <paramref name="A"/> with 'true' element where elements in <paramref name="A"/> equal +/-infinity.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The storage order of the array returned depends on the order of <paramref name="A"/>. If 
        /// <paramref name="A"/>.<see cref="Size.IsContinuous"/> is <c>true</c> the array returned will have the same <see cref="Size.StorageOrder"/> 
        /// as <paramref name="A"/>. Otherwise, the elements of the returned array will be reordered in the order defined by 
        /// <see cref="Settings.DefaultStorageOrder"/> (default: <see cref="StorageOrders.ColumnMajor"/>).</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para><b>Handling NaN elements</b> The functions <see cref="isfinite(BaseArray{float})"/>, <see cref="isinf(BaseArray{float})"/>, 
        /// <see cref="isposinf(BaseArray{float})"/> and <see cref="isneginf(BaseArray{float})"/> return false for NaN (not a number) 
        /// special floating point value elements.</para>
        /// </remarks>
        
        internal unsafe static Logical  isinf(BaseArray<float> A) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.IsInfinity.Single.Instance.operate(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>);
        }
    }
    namespace InnerLoops {

        namespace  IsInfinity {

            internal class Single :
            UnaryBaseOutOfPlaceLogical<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>
            {

                internal static Single Instance = new Single();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsd, long* nrTrues) {

                    uint ndims = (uint)bsd[0];
                    if (ndims == 0) {
                        // special case: np scalar
                        System.Diagnostics.Debug.Assert(start == 0);
                        System.Diagnostics.Debug.Assert(len == 1);
                        if ((*(pDest) =  float.IsInfinity(*(float*)pSrc) ? (byte)1:(byte)0) != 0) {
                            nrTrues[0] = 1;
                        } else {
                            nrTrues[0] = 0; 
                        }
                        return; 
                    }
                    long* dims = bsd + 3;
                    long* strides = dims + ndims;
                    long stride0 = strides[0];
                    long* cur = stackalloc long[(int)ndims];

                    // figure out the dimension index position for start
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");

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


                    byte* pOut = (byte*)pDest + start;
                    long trueCount = 0; 

                    while (true) {

                        byte* pIn = pSrc + higdims + cur[0] * stride0;

                        // iteration length limited to either the dimension lengths or the end of the requested chunk 
                        long leadLen = Math.Min(len, dims[0] - cur[0]);
                        len -= leadLen;

                        while (leadLen-- > 0) {
                            if ((*pOut =  float.IsInfinity(*(float*)pIn) ? (byte)1:(byte)0) != 0) trueCount++;
                            pOut++;  pIn += stride0;
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
                    nrTrues[0] = trueCount; 
                }
            }
        }
    }

    internal static partial class MathInternal {

        /// <summary>Checks for infinite elements.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Logical array of the same size as <paramref name="A"/> with 'true' element where elements in <paramref name="A"/> equal +/-infinity.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The storage order of the array returned depends on the order of <paramref name="A"/>. If 
        /// <paramref name="A"/>.<see cref="Size.IsContinuous"/> is <c>true</c> the array returned will have the same <see cref="Size.StorageOrder"/> 
        /// as <paramref name="A"/>. Otherwise, the elements of the returned array will be reordered in the order defined by 
        /// <see cref="Settings.DefaultStorageOrder"/> (default: <see cref="StorageOrders.ColumnMajor"/>).</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para><b>Handling NaN elements</b> The functions <see cref="isfinite(BaseArray{double})"/>, <see cref="isinf(BaseArray{double})"/>, 
        /// <see cref="isposinf(BaseArray{double})"/> and <see cref="isneginf(BaseArray{double})"/> return false for NaN (not a number) 
        /// special floating point value elements.</para>
        /// </remarks>
        
        internal unsafe static Logical  isinf(BaseArray<double> A) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.IsInfinity.Double.Instance.operate(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>);
        }
    }
    namespace InnerLoops {

        namespace  IsInfinity {

            internal class Double :
            UnaryBaseOutOfPlaceLogical<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>
            {

                internal static Double Instance = new Double();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsd, long* nrTrues) {

                    uint ndims = (uint)bsd[0];
                    if (ndims == 0) {
                        // special case: np scalar
                        System.Diagnostics.Debug.Assert(start == 0);
                        System.Diagnostics.Debug.Assert(len == 1);
                        if ((*(pDest) =  double.IsInfinity(*(double*)pSrc) ? (byte)1:(byte)0) != 0) {
                            nrTrues[0] = 1;
                        } else {
                            nrTrues[0] = 0; 
                        }
                        return; 
                    }
                    long* dims = bsd + 3;
                    long* strides = dims + ndims;
                    long stride0 = strides[0];
                    long* cur = stackalloc long[(int)ndims];

                    // figure out the dimension index position for start
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");

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


                    byte* pOut = (byte*)pDest + start;
                    long trueCount = 0; 

                    while (true) {

                        byte* pIn = pSrc + higdims + cur[0] * stride0;

                        // iteration length limited to either the dimension lengths or the end of the requested chunk 
                        long leadLen = Math.Min(len, dims[0] - cur[0]);
                        len -= leadLen;

                        while (leadLen-- > 0) {
                            if ((*pOut =  double.IsInfinity(*(double*)pIn) ? (byte)1:(byte)0) != 0) trueCount++;
                            pOut++;  pIn += stride0;
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
                    nrTrues[0] = trueCount; 
                }
            }
        }
    }


#endregion HYCALPER AUTO GENERATED CODE

}
