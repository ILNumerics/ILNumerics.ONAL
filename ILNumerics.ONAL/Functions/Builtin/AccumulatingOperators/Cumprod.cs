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
using ILNumerics;
using ILNumerics.Core.Arrays;
using ILNumerics.Core.Functions.Builtin.InnerLoops;
using ILNumerics.Core.Global;
using ILNumerics.Core.StorageLayer;
using System;
using System.Security;
using System.Threading;
namespace ILNumerics.Core.Functions.Builtin {

    #region HYCALPER LOOPSTART UNARY_OPERATOR_TEMPLATE@Functions\Builtin\AccumulatingOperators\Cumsum.cs
    /*!HC:TYPELIST:
    <hycalper>
    <type>
    <source locate="here">
        double
    </source>
    <destination>double</destination>
    <destination>byte</destination>
    <destination>sbyte</destination>
    <destination>ushort</destination>
    <destination>short</destination>
    <destination>uint</destination>
    <destination>int</destination>
    <destination>ulong</destination>
    <destination>long</destination>
    <destination>float</destination>
    <destination>complex</destination>
    <destination>fcomplex</destination>
    </type>
    <type>
    <source locate="here">
        Double
    </source>
    <destination>Double</destination>
    <destination>Byte</destination>
    <destination>SByte</destination>
    <destination>UInt16</destination>
    <destination>Int16</destination>
    <destination>UInt32</destination>
    <destination>Int32</destination>
    <destination>UInt64</destination>
    <destination>Int64</destination>
    <destination>Single</destination>
    <destination>Complex</destination>
    <destination>FComplex</destination>
    </type>
    <type>
    <source locate="after" endmark=" *(">
        operatorfunc
    </source>
    <destination>*= </destination>
    <destination>*= </destination>
    <destination>*= </destination>
    <destination>*= </destination>
    <destination>*= </destination>
    <destination>*= </destination>
    <destination>*= </destination>
    <destination>*= </destination>
    <destination>*= </destination>
    <destination>*= </destination>
    <destination>*= </destination>
    <destination>*= </destination>
    </type>
    <type>
    <source locate="after" endmark=";">
        initAccumValue
    </source>
    <destination>1</destination>
    <destination>1</destination>
    <destination>1</destination>
    <destination>1</destination>
    <destination>1</destination>
    <destination>1</destination>
    <destination>1</destination>
    <destination>1</destination>
    <destination>1</destination>
    <destination>1</destination>
    <destination>1</destination>
    <destination>1</destination>
    </type>
    <type>
    <source locate="here">
        Cumsum
    </source>
    <destination>Cumprod</destination>
    <destination>Cumprod</destination>
    <destination>Cumprod</destination>
    <destination>Cumprod</destination>
    <destination>Cumprod</destination>
    <destination>Cumprod</destination>
    <destination>Cumprod</destination>
    <destination>Cumprod</destination>
    <destination>Cumprod</destination>
    <destination>Cumprod</destination>
    <destination>Cumprod</destination>
    <destination>Cumprod</destination>
    </type>
    <type>
    <source locate="here">
        cumsum
    </source>
    <destination>cumprod</destination>
    <destination>cumprod</destination>
    <destination>cumprod</destination>
    <destination>cumprod</destination>
    <destination>cumprod</destination>
    <destination>cumprod</destination>
    <destination>cumprod</destination>
    <destination>cumprod</destination>
    <destination>cumprod</destination>
    <destination>cumprod</destination>
    <destination>cumprod</destination>
    <destination>cumprod</destination>
    </type>
    <type>
    <source locate="comment">
        summary
    </source>
    <destination><![CDATA[Computes the product of elements of <paramref name="A"/> along the specified dimension.]]></destination>
    <destination><![CDATA[Computes the product of elements of <paramref name="A"/> along the specified dimension.]]></destination>
    <destination><![CDATA[Computes the product of elements of <paramref name="A"/> along the specified dimension.]]></destination>
    <destination><![CDATA[Computes the product of elements of <paramref name="A"/> along the specified dimension.]]></destination>
    <destination><![CDATA[Computes the product of elements of <paramref name="A"/> along the specified dimension.]]></destination>
    <destination><![CDATA[Computes the product of elements of <paramref name="A"/> along the specified dimension.]]></destination>
    <destination><![CDATA[Computes the product of elements of <paramref name="A"/> along the specified dimension.]]></destination>
    <destination><![CDATA[Computes the product of elements of <paramref name="A"/> along the specified dimension.]]></destination>
    <destination><![CDATA[Computes the product of elements of <paramref name="A"/> along the specified dimension.]]></destination>
    <destination><![CDATA[Computes the product of elements of <paramref name="A"/> along the specified dimension.]]></destination>
    <destination><![CDATA[Computes the product of elements of <paramref name="A"/> along the specified dimension.]]></destination>
    <destination><![CDATA[Computes the product of elements of <paramref name="A"/> along the specified dimension.]]></destination>
    </type>
    </hycalper>
    */

    #endregion HYCALPER LOOPEND UNARY_OPERATOR_TEMPLATE
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
   
    internal static partial class MathInternal {

        /// <summary>Computes the product of elements of <paramref name="A"/> along the specified dimension.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] The dimension index of the working dimension. Default: (-1) work along the first non-singleton dimension or along dimension #0.</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para></para></remarks>
        internal unsafe static Array<fcomplex> cumprod(BaseArray<fcomplex> A, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.Cumprod.FComplex.Instance.operate(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, dim);
        }
    }
    namespace InnerLoops {

        namespace Cumprod {

            internal class FComplex :

            UnarySameTypeSameSizeBase<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> {

                internal static FComplex Instance = new FComplex();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut) {

                    /*
                     * bsdIn:   dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     * bsdOut:  dims: -1
                     *          dims[axis] is same size than input! reordered to the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * inDims correspond to outDims. Iteration is done along both: in and out arrays. 
                     * 
                     * start, len: corresponds to number of elements to work throug in _higher dimensions_. Working dims lenght (bsd[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(bsdIn != (byte*)0);
                    System.Diagnostics.Debug.Assert(bsdOut != (byte*)0);
                    System.Diagnostics.Debug.Assert(bsdIn[0] == bsdOut[0]);
                    System.Diagnostics.Debug.Assert(bsdIn[1] >= bsdOut[1]);
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(pDest != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)bsdIn[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims; 
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3] , "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1] , "BSDs have invalid format.");

                    //long accuDim = dims[ndims - 1] + 1;
                    long accuInStride = inStrides[0];
                    long accuOutStride = outStrides[0];

                    long* cur = stackalloc long[(int)ndims]; // don't assume stackalloc to clear values! (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pDest += cur[i] * outStrides[i];
                    }
                    while (i < ndims) { // finish initializing cur
                        cur[i++] = 0; 
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length - 1!

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long leadLen = dims[0] + 1;
                        fcomplex tmp =  1; // likely to cause a register to be used in the following
                        byte* pRead = pSrc;
                        byte* pWrite = pDest; 

                        while (leadLen-- > 0) {
                            *(fcomplex*)(pWrite) = (tmp  *=  *(fcomplex*)(pRead));
                            pRead += accuInStride; pWrite += accuOutStride; 
                        }

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pDest += outStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pDest -= outStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Computes the product of elements of <paramref name="A"/> along the specified dimension.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] The dimension index of the working dimension. Default: (-1) work along the first non-singleton dimension or along dimension #0.</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para></para></remarks>
        internal unsafe static Array<complex> cumprod(BaseArray<complex> A, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.Cumprod.Complex.Instance.operate(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, dim);
        }
    }
    namespace InnerLoops {

        namespace Cumprod {

            internal class Complex :

            UnarySameTypeSameSizeBase<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> {

                internal static Complex Instance = new Complex();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut) {

                    /*
                     * bsdIn:   dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     * bsdOut:  dims: -1
                     *          dims[axis] is same size than input! reordered to the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * inDims correspond to outDims. Iteration is done along both: in and out arrays. 
                     * 
                     * start, len: corresponds to number of elements to work throug in _higher dimensions_. Working dims lenght (bsd[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(bsdIn != (byte*)0);
                    System.Diagnostics.Debug.Assert(bsdOut != (byte*)0);
                    System.Diagnostics.Debug.Assert(bsdIn[0] == bsdOut[0]);
                    System.Diagnostics.Debug.Assert(bsdIn[1] >= bsdOut[1]);
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(pDest != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)bsdIn[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims; 
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3] , "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1] , "BSDs have invalid format.");

                    //long accuDim = dims[ndims - 1] + 1;
                    long accuInStride = inStrides[0];
                    long accuOutStride = outStrides[0];

                    long* cur = stackalloc long[(int)ndims]; // don't assume stackalloc to clear values! (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pDest += cur[i] * outStrides[i];
                    }
                    while (i < ndims) { // finish initializing cur
                        cur[i++] = 0; 
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length - 1!

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long leadLen = dims[0] + 1;
                        complex tmp =  1; // likely to cause a register to be used in the following
                        byte* pRead = pSrc;
                        byte* pWrite = pDest; 

                        while (leadLen-- > 0) {
                            *(complex*)(pWrite) = (tmp  *=  *(complex*)(pRead));
                            pRead += accuInStride; pWrite += accuOutStride; 
                        }

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pDest += outStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pDest -= outStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Computes the product of elements of <paramref name="A"/> along the specified dimension.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] The dimension index of the working dimension. Default: (-1) work along the first non-singleton dimension or along dimension #0.</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para></para></remarks>
        internal unsafe static Array<float> cumprod(BaseArray<float> A, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.Cumprod.Single.Instance.operate(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, dim);
        }
    }
    namespace InnerLoops {

        namespace Cumprod {

            internal class Single :

            UnarySameTypeSameSizeBase<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> {

                internal static Single Instance = new Single();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut) {

                    /*
                     * bsdIn:   dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     * bsdOut:  dims: -1
                     *          dims[axis] is same size than input! reordered to the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * inDims correspond to outDims. Iteration is done along both: in and out arrays. 
                     * 
                     * start, len: corresponds to number of elements to work throug in _higher dimensions_. Working dims lenght (bsd[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(bsdIn != (byte*)0);
                    System.Diagnostics.Debug.Assert(bsdOut != (byte*)0);
                    System.Diagnostics.Debug.Assert(bsdIn[0] == bsdOut[0]);
                    System.Diagnostics.Debug.Assert(bsdIn[1] >= bsdOut[1]);
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(pDest != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)bsdIn[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims; 
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3] , "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1] , "BSDs have invalid format.");

                    //long accuDim = dims[ndims - 1] + 1;
                    long accuInStride = inStrides[0];
                    long accuOutStride = outStrides[0];

                    long* cur = stackalloc long[(int)ndims]; // don't assume stackalloc to clear values! (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pDest += cur[i] * outStrides[i];
                    }
                    while (i < ndims) { // finish initializing cur
                        cur[i++] = 0; 
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length - 1!

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long leadLen = dims[0] + 1;
                        float tmp =  1; // likely to cause a register to be used in the following
                        byte* pRead = pSrc;
                        byte* pWrite = pDest; 

                        while (leadLen-- > 0) {
                            *(float*)(pWrite) = (tmp  *=  *(float*)(pRead));
                            pRead += accuInStride; pWrite += accuOutStride; 
                        }

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pDest += outStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pDest -= outStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Computes the product of elements of <paramref name="A"/> along the specified dimension.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] The dimension index of the working dimension. Default: (-1) work along the first non-singleton dimension or along dimension #0.</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para></para></remarks>
        internal unsafe static Array<long> cumprod(BaseArray<long> A, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.Cumprod.Int64.Instance.operate(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, dim);
        }
    }
    namespace InnerLoops {

        namespace Cumprod {

            internal class Int64 :

            UnarySameTypeSameSizeBase<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> {

                internal static Int64 Instance = new Int64();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut) {

                    /*
                     * bsdIn:   dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     * bsdOut:  dims: -1
                     *          dims[axis] is same size than input! reordered to the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * inDims correspond to outDims. Iteration is done along both: in and out arrays. 
                     * 
                     * start, len: corresponds to number of elements to work throug in _higher dimensions_. Working dims lenght (bsd[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(bsdIn != (byte*)0);
                    System.Diagnostics.Debug.Assert(bsdOut != (byte*)0);
                    System.Diagnostics.Debug.Assert(bsdIn[0] == bsdOut[0]);
                    System.Diagnostics.Debug.Assert(bsdIn[1] >= bsdOut[1]);
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(pDest != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)bsdIn[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims; 
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3] , "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1] , "BSDs have invalid format.");

                    //long accuDim = dims[ndims - 1] + 1;
                    long accuInStride = inStrides[0];
                    long accuOutStride = outStrides[0];

                    long* cur = stackalloc long[(int)ndims]; // don't assume stackalloc to clear values! (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pDest += cur[i] * outStrides[i];
                    }
                    while (i < ndims) { // finish initializing cur
                        cur[i++] = 0; 
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length - 1!

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long leadLen = dims[0] + 1;
                        long tmp =  1; // likely to cause a register to be used in the following
                        byte* pRead = pSrc;
                        byte* pWrite = pDest; 

                        while (leadLen-- > 0) {
                            *(long*)(pWrite) = (tmp  *=  *(long*)(pRead));
                            pRead += accuInStride; pWrite += accuOutStride; 
                        }

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pDest += outStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pDest -= outStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Computes the product of elements of <paramref name="A"/> along the specified dimension.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] The dimension index of the working dimension. Default: (-1) work along the first non-singleton dimension or along dimension #0.</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para></para></remarks>
        internal unsafe static Array<ulong> cumprod(BaseArray<ulong> A, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.Cumprod.UInt64.Instance.operate(A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>, dim);
        }
    }
    namespace InnerLoops {

        namespace Cumprod {

            internal class UInt64 :

            UnarySameTypeSameSizeBase<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> {

                internal static UInt64 Instance = new UInt64();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut) {

                    /*
                     * bsdIn:   dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     * bsdOut:  dims: -1
                     *          dims[axis] is same size than input! reordered to the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * inDims correspond to outDims. Iteration is done along both: in and out arrays. 
                     * 
                     * start, len: corresponds to number of elements to work throug in _higher dimensions_. Working dims lenght (bsd[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(bsdIn != (byte*)0);
                    System.Diagnostics.Debug.Assert(bsdOut != (byte*)0);
                    System.Diagnostics.Debug.Assert(bsdIn[0] == bsdOut[0]);
                    System.Diagnostics.Debug.Assert(bsdIn[1] >= bsdOut[1]);
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(pDest != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)bsdIn[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims; 
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3] , "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1] , "BSDs have invalid format.");

                    //long accuDim = dims[ndims - 1] + 1;
                    long accuInStride = inStrides[0];
                    long accuOutStride = outStrides[0];

                    long* cur = stackalloc long[(int)ndims]; // don't assume stackalloc to clear values! (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pDest += cur[i] * outStrides[i];
                    }
                    while (i < ndims) { // finish initializing cur
                        cur[i++] = 0; 
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length - 1!

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long leadLen = dims[0] + 1;
                        ulong tmp =  1; // likely to cause a register to be used in the following
                        byte* pRead = pSrc;
                        byte* pWrite = pDest; 

                        while (leadLen-- > 0) {
                            *(ulong*)(pWrite) = (tmp  *=  *(ulong*)(pRead));
                            pRead += accuInStride; pWrite += accuOutStride; 
                        }

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pDest += outStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pDest -= outStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Computes the product of elements of <paramref name="A"/> along the specified dimension.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] The dimension index of the working dimension. Default: (-1) work along the first non-singleton dimension or along dimension #0.</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para></para></remarks>
        internal unsafe static Array<int> cumprod(BaseArray<int> A, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.Cumprod.Int32.Instance.operate(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>, dim);
        }
    }
    namespace InnerLoops {

        namespace Cumprod {

            internal class Int32 :

            UnarySameTypeSameSizeBase<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> {

                internal static Int32 Instance = new Int32();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut) {

                    /*
                     * bsdIn:   dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     * bsdOut:  dims: -1
                     *          dims[axis] is same size than input! reordered to the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * inDims correspond to outDims. Iteration is done along both: in and out arrays. 
                     * 
                     * start, len: corresponds to number of elements to work throug in _higher dimensions_. Working dims lenght (bsd[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(bsdIn != (byte*)0);
                    System.Diagnostics.Debug.Assert(bsdOut != (byte*)0);
                    System.Diagnostics.Debug.Assert(bsdIn[0] == bsdOut[0]);
                    System.Diagnostics.Debug.Assert(bsdIn[1] >= bsdOut[1]);
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(pDest != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)bsdIn[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims; 
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3] , "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1] , "BSDs have invalid format.");

                    //long accuDim = dims[ndims - 1] + 1;
                    long accuInStride = inStrides[0];
                    long accuOutStride = outStrides[0];

                    long* cur = stackalloc long[(int)ndims]; // don't assume stackalloc to clear values! (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pDest += cur[i] * outStrides[i];
                    }
                    while (i < ndims) { // finish initializing cur
                        cur[i++] = 0; 
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length - 1!

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long leadLen = dims[0] + 1;
                        int tmp =  1; // likely to cause a register to be used in the following
                        byte* pRead = pSrc;
                        byte* pWrite = pDest; 

                        while (leadLen-- > 0) {
                            *(int*)(pWrite) = (tmp  *=  *(int*)(pRead));
                            pRead += accuInStride; pWrite += accuOutStride; 
                        }

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pDest += outStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pDest -= outStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Computes the product of elements of <paramref name="A"/> along the specified dimension.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] The dimension index of the working dimension. Default: (-1) work along the first non-singleton dimension or along dimension #0.</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para></para></remarks>
        internal unsafe static Array<uint> cumprod(BaseArray<uint> A, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.Cumprod.UInt32.Instance.operate(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>, dim);
        }
    }
    namespace InnerLoops {

        namespace Cumprod {

            internal class UInt32 :

            UnarySameTypeSameSizeBase<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> {

                internal static UInt32 Instance = new UInt32();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut) {

                    /*
                     * bsdIn:   dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     * bsdOut:  dims: -1
                     *          dims[axis] is same size than input! reordered to the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * inDims correspond to outDims. Iteration is done along both: in and out arrays. 
                     * 
                     * start, len: corresponds to number of elements to work throug in _higher dimensions_. Working dims lenght (bsd[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(bsdIn != (byte*)0);
                    System.Diagnostics.Debug.Assert(bsdOut != (byte*)0);
                    System.Diagnostics.Debug.Assert(bsdIn[0] == bsdOut[0]);
                    System.Diagnostics.Debug.Assert(bsdIn[1] >= bsdOut[1]);
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(pDest != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)bsdIn[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims; 
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3] , "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1] , "BSDs have invalid format.");

                    //long accuDim = dims[ndims - 1] + 1;
                    long accuInStride = inStrides[0];
                    long accuOutStride = outStrides[0];

                    long* cur = stackalloc long[(int)ndims]; // don't assume stackalloc to clear values! (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pDest += cur[i] * outStrides[i];
                    }
                    while (i < ndims) { // finish initializing cur
                        cur[i++] = 0; 
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length - 1!

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long leadLen = dims[0] + 1;
                        uint tmp =  1; // likely to cause a register to be used in the following
                        byte* pRead = pSrc;
                        byte* pWrite = pDest; 

                        while (leadLen-- > 0) {
                            *(uint*)(pWrite) = (tmp  *=  *(uint*)(pRead));
                            pRead += accuInStride; pWrite += accuOutStride; 
                        }

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pDest += outStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pDest -= outStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Computes the product of elements of <paramref name="A"/> along the specified dimension.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] The dimension index of the working dimension. Default: (-1) work along the first non-singleton dimension or along dimension #0.</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para></para></remarks>
        internal unsafe static Array<short> cumprod(BaseArray<short> A, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.Cumprod.Int16.Instance.operate(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>, dim);
        }
    }
    namespace InnerLoops {

        namespace Cumprod {

            internal class Int16 :

            UnarySameTypeSameSizeBase<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> {

                internal static Int16 Instance = new Int16();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut) {

                    /*
                     * bsdIn:   dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     * bsdOut:  dims: -1
                     *          dims[axis] is same size than input! reordered to the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * inDims correspond to outDims. Iteration is done along both: in and out arrays. 
                     * 
                     * start, len: corresponds to number of elements to work throug in _higher dimensions_. Working dims lenght (bsd[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(bsdIn != (byte*)0);
                    System.Diagnostics.Debug.Assert(bsdOut != (byte*)0);
                    System.Diagnostics.Debug.Assert(bsdIn[0] == bsdOut[0]);
                    System.Diagnostics.Debug.Assert(bsdIn[1] >= bsdOut[1]);
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(pDest != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)bsdIn[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims; 
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3] , "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1] , "BSDs have invalid format.");

                    //long accuDim = dims[ndims - 1] + 1;
                    long accuInStride = inStrides[0];
                    long accuOutStride = outStrides[0];

                    long* cur = stackalloc long[(int)ndims]; // don't assume stackalloc to clear values! (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pDest += cur[i] * outStrides[i];
                    }
                    while (i < ndims) { // finish initializing cur
                        cur[i++] = 0; 
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length - 1!

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long leadLen = dims[0] + 1;
                        short tmp =  1; // likely to cause a register to be used in the following
                        byte* pRead = pSrc;
                        byte* pWrite = pDest; 

                        while (leadLen-- > 0) {
                            *(short*)(pWrite) = (tmp  *=  *(short*)(pRead));
                            pRead += accuInStride; pWrite += accuOutStride; 
                        }

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pDest += outStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pDest -= outStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Computes the product of elements of <paramref name="A"/> along the specified dimension.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] The dimension index of the working dimension. Default: (-1) work along the first non-singleton dimension or along dimension #0.</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para></para></remarks>
        internal unsafe static Array<ushort> cumprod(BaseArray<ushort> A, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.Cumprod.UInt16.Instance.operate(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>, dim);
        }
    }
    namespace InnerLoops {

        namespace Cumprod {

            internal class UInt16 :

            UnarySameTypeSameSizeBase<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> {

                internal static UInt16 Instance = new UInt16();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut) {

                    /*
                     * bsdIn:   dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     * bsdOut:  dims: -1
                     *          dims[axis] is same size than input! reordered to the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * inDims correspond to outDims. Iteration is done along both: in and out arrays. 
                     * 
                     * start, len: corresponds to number of elements to work throug in _higher dimensions_. Working dims lenght (bsd[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(bsdIn != (byte*)0);
                    System.Diagnostics.Debug.Assert(bsdOut != (byte*)0);
                    System.Diagnostics.Debug.Assert(bsdIn[0] == bsdOut[0]);
                    System.Diagnostics.Debug.Assert(bsdIn[1] >= bsdOut[1]);
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(pDest != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)bsdIn[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims; 
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3] , "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1] , "BSDs have invalid format.");

                    //long accuDim = dims[ndims - 1] + 1;
                    long accuInStride = inStrides[0];
                    long accuOutStride = outStrides[0];

                    long* cur = stackalloc long[(int)ndims]; // don't assume stackalloc to clear values! (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pDest += cur[i] * outStrides[i];
                    }
                    while (i < ndims) { // finish initializing cur
                        cur[i++] = 0; 
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length - 1!

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long leadLen = dims[0] + 1;
                        ushort tmp =  1; // likely to cause a register to be used in the following
                        byte* pRead = pSrc;
                        byte* pWrite = pDest; 

                        while (leadLen-- > 0) {
                            *(ushort*)(pWrite) = (tmp  *=  *(ushort*)(pRead));
                            pRead += accuInStride; pWrite += accuOutStride; 
                        }

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pDest += outStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pDest -= outStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Computes the product of elements of <paramref name="A"/> along the specified dimension.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] The dimension index of the working dimension. Default: (-1) work along the first non-singleton dimension or along dimension #0.</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para></para></remarks>
        internal unsafe static Array<sbyte> cumprod(BaseArray<sbyte> A, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.Cumprod.SByte.Instance.operate(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>, dim);
        }
    }
    namespace InnerLoops {

        namespace Cumprod {

            internal class SByte :

            UnarySameTypeSameSizeBase<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> {

                internal static SByte Instance = new SByte();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut) {

                    /*
                     * bsdIn:   dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     * bsdOut:  dims: -1
                     *          dims[axis] is same size than input! reordered to the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * inDims correspond to outDims. Iteration is done along both: in and out arrays. 
                     * 
                     * start, len: corresponds to number of elements to work throug in _higher dimensions_. Working dims lenght (bsd[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(bsdIn != (byte*)0);
                    System.Diagnostics.Debug.Assert(bsdOut != (byte*)0);
                    System.Diagnostics.Debug.Assert(bsdIn[0] == bsdOut[0]);
                    System.Diagnostics.Debug.Assert(bsdIn[1] >= bsdOut[1]);
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(pDest != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)bsdIn[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims; 
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3] , "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1] , "BSDs have invalid format.");

                    //long accuDim = dims[ndims - 1] + 1;
                    long accuInStride = inStrides[0];
                    long accuOutStride = outStrides[0];

                    long* cur = stackalloc long[(int)ndims]; // don't assume stackalloc to clear values! (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pDest += cur[i] * outStrides[i];
                    }
                    while (i < ndims) { // finish initializing cur
                        cur[i++] = 0; 
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length - 1!

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long leadLen = dims[0] + 1;
                        sbyte tmp =  1; // likely to cause a register to be used in the following
                        byte* pRead = pSrc;
                        byte* pWrite = pDest; 

                        while (leadLen-- > 0) {
                            *(sbyte*)(pWrite) = (tmp  *=  *(sbyte*)(pRead));
                            pRead += accuInStride; pWrite += accuOutStride; 
                        }

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pDest += outStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pDest -= outStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Computes the product of elements of <paramref name="A"/> along the specified dimension.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] The dimension index of the working dimension. Default: (-1) work along the first non-singleton dimension or along dimension #0.</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para></para></remarks>
        internal unsafe static Array<byte> cumprod(BaseArray<byte> A, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.Cumprod.Byte.Instance.operate(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>, dim);
        }
    }
    namespace InnerLoops {

        namespace Cumprod {

            internal class Byte :

            UnarySameTypeSameSizeBase<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> {

                internal static Byte Instance = new Byte();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut) {

                    /*
                     * bsdIn:   dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     * bsdOut:  dims: -1
                     *          dims[axis] is same size than input! reordered to the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * inDims correspond to outDims. Iteration is done along both: in and out arrays. 
                     * 
                     * start, len: corresponds to number of elements to work throug in _higher dimensions_. Working dims lenght (bsd[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(bsdIn != (byte*)0);
                    System.Diagnostics.Debug.Assert(bsdOut != (byte*)0);
                    System.Diagnostics.Debug.Assert(bsdIn[0] == bsdOut[0]);
                    System.Diagnostics.Debug.Assert(bsdIn[1] >= bsdOut[1]);
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(pDest != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)bsdIn[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims; 
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3] , "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1] , "BSDs have invalid format.");

                    //long accuDim = dims[ndims - 1] + 1;
                    long accuInStride = inStrides[0];
                    long accuOutStride = outStrides[0];

                    long* cur = stackalloc long[(int)ndims]; // don't assume stackalloc to clear values! (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pDest += cur[i] * outStrides[i];
                    }
                    while (i < ndims) { // finish initializing cur
                        cur[i++] = 0; 
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length - 1!

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long leadLen = dims[0] + 1;
                        byte tmp =  1; // likely to cause a register to be used in the following
                        byte* pRead = pSrc;
                        byte* pWrite = pDest; 

                        while (leadLen-- > 0) {
                            *(byte*)(pWrite) = (tmp  *=  *(byte*)(pRead));
                            pRead += accuInStride; pWrite += accuOutStride; 
                        }

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pDest += outStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pDest -= outStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Computes the product of elements of <paramref name="A"/> along the specified dimension.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] The dimension index of the working dimension. Default: (-1) work along the first non-singleton dimension or along dimension #0.</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para></para></remarks>
        internal unsafe static Array<double> cumprod(BaseArray<double> A, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.Cumprod.Double.Instance.operate(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, dim);
        }
    }
    namespace InnerLoops {

        namespace Cumprod {

            internal class Double :

            UnarySameTypeSameSizeBase<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> {

                internal static Double Instance = new Double();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut) {

                    /*
                     * bsdIn:   dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     * bsdOut:  dims: -1
                     *          dims[axis] is same size than input! reordered to the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * inDims correspond to outDims. Iteration is done along both: in and out arrays. 
                     * 
                     * start, len: corresponds to number of elements to work throug in _higher dimensions_. Working dims lenght (bsd[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(bsdIn != (byte*)0);
                    System.Diagnostics.Debug.Assert(bsdOut != (byte*)0);
                    System.Diagnostics.Debug.Assert(bsdIn[0] == bsdOut[0]);
                    System.Diagnostics.Debug.Assert(bsdIn[1] >= bsdOut[1]);
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(pDest != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)bsdIn[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims; 
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3] , "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1] , "BSDs have invalid format.");

                    //long accuDim = dims[ndims - 1] + 1;
                    long accuInStride = inStrides[0];
                    long accuOutStride = outStrides[0];

                    long* cur = stackalloc long[(int)ndims]; // don't assume stackalloc to clear values! (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pDest += cur[i] * outStrides[i];
                    }
                    while (i < ndims) { // finish initializing cur
                        cur[i++] = 0; 
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length - 1!

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long leadLen = dims[0] + 1;
                        double tmp =  1; // likely to cause a register to be used in the following
                        byte* pRead = pSrc;
                        byte* pWrite = pDest; 

                        while (leadLen-- > 0) {
                            *(double*)(pWrite) = (tmp  *=  *(double*)(pRead));
                            pRead += accuInStride; pWrite += accuOutStride; 
                        }

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pDest += outStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pDest -= outStrides[d] * dims[d];
                                cur[d] = 0;
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

