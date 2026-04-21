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

    #region HYCALPER LOOPSTART UNARY_OPERATOR_TEMPLATE
    /*!HC:TYPELIST:
    <hycalper>
    <type>
    <source locate="here">
        <![CDATA[<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>]]>
    </source>
    <destination><![CDATA[<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>]]></destination>
    <destination><![CDATA[<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>]]></destination>
    <destination><![CDATA[<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>]]></destination>
    <destination><![CDATA[<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>]]></destination>
    <destination><![CDATA[<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>]]></destination>
    <destination><![CDATA[<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>]]></destination>
    <destination><![CDATA[<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>]]></destination>
    <destination><![CDATA[<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>]]></destination>
    <destination><![CDATA[<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>]]></destination>
    <destination><![CDATA[<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>]]></destination>
    <destination><![CDATA[<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>]]></destination>
    <destination><![CDATA[<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>]]></destination>
    </type>
    <type>
    <source locate="here">
        <![CDATA[BaseArray<double>]]>
    </source>
    <destination><![CDATA[BaseArray<double>]]></destination>
    <destination><![CDATA[BaseArray<double>]]></destination>
    <destination><![CDATA[BaseArray<double>]]></destination>
    <destination><![CDATA[BaseArray<double>]]></destination>
    <destination><![CDATA[BaseArray<double>]]></destination>
    <destination><![CDATA[BaseArray<double>]]></destination>
    <destination><![CDATA[BaseArray<double>]]></destination>
    <destination><![CDATA[BaseArray<double>]]></destination>
    <destination><![CDATA[BaseArray<double>]]></destination>
    <destination><![CDATA[BaseArray<double>]]></destination>
    <destination><![CDATA[BaseArray<double>]]></destination>
    <destination><![CDATA[BaseArray<bool>]]></destination>
    </type>
    <type>
    <source locate="here">
        <![CDATA[ILNumerics.Array<double>]]>
    </source>
    <destination><![CDATA[ILNumerics.Array<double>]]></destination>
    <destination><![CDATA[ILNumerics.Array<double>]]></destination>
    <destination><![CDATA[ILNumerics.Array<double>]]></destination>
    <destination><![CDATA[ILNumerics.Array<double>]]></destination>
    <destination><![CDATA[ILNumerics.Array<double>]]></destination>
    <destination><![CDATA[ILNumerics.Array<double>]]></destination>
    <destination><![CDATA[ILNumerics.Array<double>]]></destination>
    <destination><![CDATA[ILNumerics.Array<double>]]></destination>
    <destination><![CDATA[ILNumerics.Array<double>]]></destination>
    <destination><![CDATA[ILNumerics.Array<double>]]></destination>
    <destination><![CDATA[ILNumerics.Array<double>]]></destination>
    <destination><![CDATA[Logical]]></destination>
    </type>
    <type>
    <source locate="here">
        double
    </source>
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
    <destination>byte</destination>
    </type>
    <type>
    <source locate="here">
        Double
    </source>
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
    <destination>Boolean</destination>
    </type>
    </hycalper>
    */
    internal static partial class MathInternal {

        /// <summary>
        /// Flips elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] Index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim"/> = -1 depends on the value of <see cref="Settings.ArrayStyle"/>. 
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts 
        /// with the first dimension (index #0).</para>
        /// <para>This method is implemented for all common numeric element types, including <see cref="ILNumerics.complex"/> and logical arrays.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="fliplr(BaseArray{double})"/>
        /// <seealso cref="flipud(BaseArray{double})"/>
        internal static ILNumerics.Array<double> flip(BaseArray<double> A, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            var storageA = (A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>)?.Storage;
            return InnerLoops.FlipOOplace.Double.Instance.operate(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, dim); 
        }
        /// <summary>
        /// Flips the order of the rows of matrix <paramref name="A"/>, i.e.: works along dimension #0.
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <returns>Array of the same size and type as <paramref name="A"/> having the rows flipped.</returns>
        /// <remarks>
        /// <para>This function is an alias for <see cref="flip(BaseArray{double}, int)"/> with a <c>dim</c> argument of 0.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="flip(BaseArray{double}, int)"/>
        /// <seealso cref="fliplr(BaseArray{double})"/>
        internal static ILNumerics.Array<double> flipud(BaseArray<double> A) {
            if (Equals(A, null)) {
                throw new ArgumentException($"Input A must be a matrix.");
            }
            return flip(A, 0);
        }
        /// <summary>
        /// Flips the order of the columns of matrix <paramref name="A"/>, i.e.: works along dimension #1.
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <returns>Array of the same size and type as <paramref name="A"/> having the columns flipped.</returns>
        /// <remarks>
        /// <para>This function is an alias for <see cref="flip(BaseArray{double}, int)"/> with a <c>dim</c> argument of 0.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="flip(BaseArray{double}, int)"/>
        /// <seealso cref="flipud(BaseArray{double})"/>
        internal static ILNumerics.Array<double> fliplr(BaseArray<double> A) {
            if (Equals(A, null)) {
                throw new ArgumentException($"Input A must be a matrix.");
            }
            return flip(A, 1);
        }
    }
    namespace InnerLoops {

        namespace FlipInplace {

            internal class Double :

            UnaryInplaceAxisBase<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> {

                internal static Double Instance = new Double();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;


                    long* cur = stackalloc long[(int)ndims];

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) { // stackalloc does not clear values
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    long accuInStride = inStrides[0];
                    while (true) {

                        //ILNumerics.Core.Misc.QuickSort.QuickSortAscST((double*)pSrc, 0, dims[0] * accuInStride, accuInStride);
                        byte* s = pSrc, e = pSrc + accuInStride * dims[0];  // dims[0] is -1!
                        double tmp = 0;
                        while (s < e) {
                            tmp = *(double*)e;
                            *(double*)e = *(double*)s;
                            *(double*)s = tmp;
                            e -= accuInStride;
                            s += accuInStride;
                        }

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
    }
    namespace InnerLoops {

        namespace FlipOOplace {

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
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    //long accuDim = dims[ndims - 1] + 1;
                    long accuInStride = inStrides[0];
                    long accuOutStride = outStrides[0];

                    long* cur = stackalloc long[(int)ndims]; // don't assume stackalloc to clear values (unspecified!)

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

                        // iteration always goes along the full accumulation length, out-element by out-element..
                        long leadLen = dims[0] + 1;

                        byte* pRead = pSrc + dims[0] * accuInStride;
                        byte* pWrite = pDest;

                        while (leadLen-- > 0) {
                            *(double*)(pWrite) = *(double*)(pRead);
                            pRead -= accuInStride; pWrite += accuOutStride;
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
    #endregion HYCALPER LOOPEND UNARY_OPERATOR_TEMPLATE
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
   
    internal static partial class MathInternal {

        /// <summary>
        /// Flips elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] Index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim"/> = -1 depends on the value of <see cref="Settings.ArrayStyle"/>. 
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts 
        /// with the first dimension (index #0).</para>
        /// <para>This method is implemented for all common numeric element types, including <see cref="ILNumerics.complex"/> and logical arrays.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="fliplr(BaseArray{byte})"/>
        /// <seealso cref="flipud(BaseArray{byte})"/>
        internal static Logical flip(BaseArray<bool> A, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            var storageA = (A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage;
            return InnerLoops.FlipOOplace.Boolean.Instance.operate(A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>, dim); 
        }
        /// <summary>
        /// Flips the order of the rows of matrix <paramref name="A"/>, i.e.: works along dimension #0.
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <returns>Array of the same size and type as <paramref name="A"/> having the rows flipped.</returns>
        /// <remarks>
        /// <para>This function is an alias for <see cref="flip(BaseArray{byte}, int)"/> with a <c>dim</c> argument of 0.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="flip(BaseArray{byte}, int)"/>
        /// <seealso cref="fliplr(BaseArray{byte})"/>
        internal static Logical flipud(BaseArray<bool> A) {
            if (Equals(A, null)) {
                throw new ArgumentException($"Input A must be a matrix.");
            }
            return flip(A, 0);
        }
        /// <summary>
        /// Flips the order of the columns of matrix <paramref name="A"/>, i.e.: works along dimension #1.
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <returns>Array of the same size and type as <paramref name="A"/> having the columns flipped.</returns>
        /// <remarks>
        /// <para>This function is an alias for <see cref="flip(BaseArray{byte}, int)"/> with a <c>dim</c> argument of 0.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="flip(BaseArray{byte}, int)"/>
        /// <seealso cref="flipud(BaseArray{byte})"/>
        internal static Logical fliplr(BaseArray<bool> A) {
            if (Equals(A, null)) {
                throw new ArgumentException($"Input A must be a matrix.");
            }
            return flip(A, 1);
        }
    }
    namespace InnerLoops {

        namespace FlipInplace {

            internal class Boolean :

            UnaryInplaceAxisBase<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage> {

                internal static Boolean Instance = new Boolean();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;


                    long* cur = stackalloc long[(int)ndims];

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) { // stackalloc does not clear values
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    long accuInStride = inStrides[0];
                    while (true) {

                        //ILNumerics.Core.Misc.QuickSort.QuickSortAscST((byte*)pSrc, 0, dims[0] * accuInStride, accuInStride);
                        byte* s = pSrc, e = pSrc + accuInStride * dims[0];  // dims[0] is -1!
                        byte tmp = 0;
                        while (s < e) {
                            tmp = *(byte*)e;
                            *(byte*)e = *(byte*)s;
                            *(byte*)s = tmp;
                            e -= accuInStride;
                            s += accuInStride;
                        }

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
    }
    namespace InnerLoops {

        namespace FlipOOplace {

            internal class Boolean :

            UnarySameTypeSameSizeBase<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage> {

                internal static Boolean Instance = new Boolean();
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
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    //long accuDim = dims[ndims - 1] + 1;
                    long accuInStride = inStrides[0];
                    long accuOutStride = outStrides[0];

                    long* cur = stackalloc long[(int)ndims]; // don't assume stackalloc to clear values (unspecified!)

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

                        // iteration always goes along the full accumulation length, out-element by out-element..
                        long leadLen = dims[0] + 1;

                        byte* pRead = pSrc + dims[0] * accuInStride;
                        byte* pWrite = pDest;

                        while (leadLen-- > 0) {
                            *(byte*)(pWrite) = *(byte*)(pRead);
                            pRead -= accuInStride; pWrite += accuOutStride;
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

        /// <summary>
        /// Flips elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] Index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim"/> = -1 depends on the value of <see cref="Settings.ArrayStyle"/>. 
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts 
        /// with the first dimension (index #0).</para>
        /// <para>This method is implemented for all common numeric element types, including <see cref="ILNumerics.complex"/> and logical arrays.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="fliplr(BaseArray{fcomplex})"/>
        /// <seealso cref="flipud(BaseArray{fcomplex})"/>
        internal static ILNumerics.Array<fcomplex> flip(BaseArray<fcomplex> A, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            var storageA = (A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>)?.Storage;
            return InnerLoops.FlipOOplace.FComplex.Instance.operate(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, dim); 
        }
        /// <summary>
        /// Flips the order of the rows of matrix <paramref name="A"/>, i.e.: works along dimension #0.
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <returns>Array of the same size and type as <paramref name="A"/> having the rows flipped.</returns>
        /// <remarks>
        /// <para>This function is an alias for <see cref="flip(BaseArray{fcomplex}, int)"/> with a <c>dim</c> argument of 0.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="flip(BaseArray{fcomplex}, int)"/>
        /// <seealso cref="fliplr(BaseArray{fcomplex})"/>
        internal static ILNumerics.Array<fcomplex> flipud(BaseArray<fcomplex> A) {
            if (Equals(A, null)) {
                throw new ArgumentException($"Input A must be a matrix.");
            }
            return flip(A, 0);
        }
        /// <summary>
        /// Flips the order of the columns of matrix <paramref name="A"/>, i.e.: works along dimension #1.
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <returns>Array of the same size and type as <paramref name="A"/> having the columns flipped.</returns>
        /// <remarks>
        /// <para>This function is an alias for <see cref="flip(BaseArray{fcomplex}, int)"/> with a <c>dim</c> argument of 0.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="flip(BaseArray{fcomplex}, int)"/>
        /// <seealso cref="flipud(BaseArray{fcomplex})"/>
        internal static ILNumerics.Array<fcomplex> fliplr(BaseArray<fcomplex> A) {
            if (Equals(A, null)) {
                throw new ArgumentException($"Input A must be a matrix.");
            }
            return flip(A, 1);
        }
    }
    namespace InnerLoops {

        namespace FlipInplace {

            internal class FComplex :

            UnaryInplaceAxisBase<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> {

                internal static FComplex Instance = new FComplex();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;


                    long* cur = stackalloc long[(int)ndims];

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) { // stackalloc does not clear values
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    long accuInStride = inStrides[0];
                    while (true) {

                        //ILNumerics.Core.Misc.QuickSort.QuickSortAscST((fcomplex*)pSrc, 0, dims[0] * accuInStride, accuInStride);
                        byte* s = pSrc, e = pSrc + accuInStride * dims[0];  // dims[0] is -1!
                        fcomplex tmp = 0;
                        while (s < e) {
                            tmp = *(fcomplex*)e;
                            *(fcomplex*)e = *(fcomplex*)s;
                            *(fcomplex*)s = tmp;
                            e -= accuInStride;
                            s += accuInStride;
                        }

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
    }
    namespace InnerLoops {

        namespace FlipOOplace {

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
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    //long accuDim = dims[ndims - 1] + 1;
                    long accuInStride = inStrides[0];
                    long accuOutStride = outStrides[0];

                    long* cur = stackalloc long[(int)ndims]; // don't assume stackalloc to clear values (unspecified!)

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

                        // iteration always goes along the full accumulation length, out-element by out-element..
                        long leadLen = dims[0] + 1;

                        byte* pRead = pSrc + dims[0] * accuInStride;
                        byte* pWrite = pDest;

                        while (leadLen-- > 0) {
                            *(fcomplex*)(pWrite) = *(fcomplex*)(pRead);
                            pRead -= accuInStride; pWrite += accuOutStride;
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

        /// <summary>
        /// Flips elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] Index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim"/> = -1 depends on the value of <see cref="Settings.ArrayStyle"/>. 
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts 
        /// with the first dimension (index #0).</para>
        /// <para>This method is implemented for all common numeric element types, including <see cref="ILNumerics.complex"/> and logical arrays.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="fliplr(BaseArray{complex})"/>
        /// <seealso cref="flipud(BaseArray{complex})"/>
        internal static ILNumerics.Array<complex> flip(BaseArray<complex> A, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            var storageA = (A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>)?.Storage;
            return InnerLoops.FlipOOplace.Complex.Instance.operate(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, dim); 
        }
        /// <summary>
        /// Flips the order of the rows of matrix <paramref name="A"/>, i.e.: works along dimension #0.
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <returns>Array of the same size and type as <paramref name="A"/> having the rows flipped.</returns>
        /// <remarks>
        /// <para>This function is an alias for <see cref="flip(BaseArray{complex}, int)"/> with a <c>dim</c> argument of 0.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="flip(BaseArray{complex}, int)"/>
        /// <seealso cref="fliplr(BaseArray{complex})"/>
        internal static ILNumerics.Array<complex> flipud(BaseArray<complex> A) {
            if (Equals(A, null)) {
                throw new ArgumentException($"Input A must be a matrix.");
            }
            return flip(A, 0);
        }
        /// <summary>
        /// Flips the order of the columns of matrix <paramref name="A"/>, i.e.: works along dimension #1.
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <returns>Array of the same size and type as <paramref name="A"/> having the columns flipped.</returns>
        /// <remarks>
        /// <para>This function is an alias for <see cref="flip(BaseArray{complex}, int)"/> with a <c>dim</c> argument of 0.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="flip(BaseArray{complex}, int)"/>
        /// <seealso cref="flipud(BaseArray{complex})"/>
        internal static ILNumerics.Array<complex> fliplr(BaseArray<complex> A) {
            if (Equals(A, null)) {
                throw new ArgumentException($"Input A must be a matrix.");
            }
            return flip(A, 1);
        }
    }
    namespace InnerLoops {

        namespace FlipInplace {

            internal class Complex :

            UnaryInplaceAxisBase<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> {

                internal static Complex Instance = new Complex();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;


                    long* cur = stackalloc long[(int)ndims];

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) { // stackalloc does not clear values
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    long accuInStride = inStrides[0];
                    while (true) {

                        //ILNumerics.Core.Misc.QuickSort.QuickSortAscST((complex*)pSrc, 0, dims[0] * accuInStride, accuInStride);
                        byte* s = pSrc, e = pSrc + accuInStride * dims[0];  // dims[0] is -1!
                        complex tmp = 0;
                        while (s < e) {
                            tmp = *(complex*)e;
                            *(complex*)e = *(complex*)s;
                            *(complex*)s = tmp;
                            e -= accuInStride;
                            s += accuInStride;
                        }

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
    }
    namespace InnerLoops {

        namespace FlipOOplace {

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
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    //long accuDim = dims[ndims - 1] + 1;
                    long accuInStride = inStrides[0];
                    long accuOutStride = outStrides[0];

                    long* cur = stackalloc long[(int)ndims]; // don't assume stackalloc to clear values (unspecified!)

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

                        // iteration always goes along the full accumulation length, out-element by out-element..
                        long leadLen = dims[0] + 1;

                        byte* pRead = pSrc + dims[0] * accuInStride;
                        byte* pWrite = pDest;

                        while (leadLen-- > 0) {
                            *(complex*)(pWrite) = *(complex*)(pRead);
                            pRead -= accuInStride; pWrite += accuOutStride;
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

        /// <summary>
        /// Flips elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] Index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim"/> = -1 depends on the value of <see cref="Settings.ArrayStyle"/>. 
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts 
        /// with the first dimension (index #0).</para>
        /// <para>This method is implemented for all common numeric element types, including <see cref="ILNumerics.complex"/> and logical arrays.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="fliplr(BaseArray{float})"/>
        /// <seealso cref="flipud(BaseArray{float})"/>
        internal static ILNumerics.Array<float> flip(BaseArray<float> A, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            var storageA = (A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>)?.Storage;
            return InnerLoops.FlipOOplace.Single.Instance.operate(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, dim); 
        }
        /// <summary>
        /// Flips the order of the rows of matrix <paramref name="A"/>, i.e.: works along dimension #0.
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <returns>Array of the same size and type as <paramref name="A"/> having the rows flipped.</returns>
        /// <remarks>
        /// <para>This function is an alias for <see cref="flip(BaseArray{float}, int)"/> with a <c>dim</c> argument of 0.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="flip(BaseArray{float}, int)"/>
        /// <seealso cref="fliplr(BaseArray{float})"/>
        internal static ILNumerics.Array<float> flipud(BaseArray<float> A) {
            if (Equals(A, null)) {
                throw new ArgumentException($"Input A must be a matrix.");
            }
            return flip(A, 0);
        }
        /// <summary>
        /// Flips the order of the columns of matrix <paramref name="A"/>, i.e.: works along dimension #1.
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <returns>Array of the same size and type as <paramref name="A"/> having the columns flipped.</returns>
        /// <remarks>
        /// <para>This function is an alias for <see cref="flip(BaseArray{float}, int)"/> with a <c>dim</c> argument of 0.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="flip(BaseArray{float}, int)"/>
        /// <seealso cref="flipud(BaseArray{float})"/>
        internal static ILNumerics.Array<float> fliplr(BaseArray<float> A) {
            if (Equals(A, null)) {
                throw new ArgumentException($"Input A must be a matrix.");
            }
            return flip(A, 1);
        }
    }
    namespace InnerLoops {

        namespace FlipInplace {

            internal class Single :

            UnaryInplaceAxisBase<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> {

                internal static Single Instance = new Single();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;


                    long* cur = stackalloc long[(int)ndims];

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) { // stackalloc does not clear values
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    long accuInStride = inStrides[0];
                    while (true) {

                        //ILNumerics.Core.Misc.QuickSort.QuickSortAscST((float*)pSrc, 0, dims[0] * accuInStride, accuInStride);
                        byte* s = pSrc, e = pSrc + accuInStride * dims[0];  // dims[0] is -1!
                        float tmp = 0;
                        while (s < e) {
                            tmp = *(float*)e;
                            *(float*)e = *(float*)s;
                            *(float*)s = tmp;
                            e -= accuInStride;
                            s += accuInStride;
                        }

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
    }
    namespace InnerLoops {

        namespace FlipOOplace {

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
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    //long accuDim = dims[ndims - 1] + 1;
                    long accuInStride = inStrides[0];
                    long accuOutStride = outStrides[0];

                    long* cur = stackalloc long[(int)ndims]; // don't assume stackalloc to clear values (unspecified!)

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

                        // iteration always goes along the full accumulation length, out-element by out-element..
                        long leadLen = dims[0] + 1;

                        byte* pRead = pSrc + dims[0] * accuInStride;
                        byte* pWrite = pDest;

                        while (leadLen-- > 0) {
                            *(float*)(pWrite) = *(float*)(pRead);
                            pRead -= accuInStride; pWrite += accuOutStride;
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

        /// <summary>
        /// Flips elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] Index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim"/> = -1 depends on the value of <see cref="Settings.ArrayStyle"/>. 
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts 
        /// with the first dimension (index #0).</para>
        /// <para>This method is implemented for all common numeric element types, including <see cref="ILNumerics.complex"/> and logical arrays.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="fliplr(BaseArray{long})"/>
        /// <seealso cref="flipud(BaseArray{long})"/>
        internal static ILNumerics.Array<long> flip(BaseArray<long> A, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            var storageA = (A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>)?.Storage;
            return InnerLoops.FlipOOplace.Int64.Instance.operate(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, dim); 
        }
        /// <summary>
        /// Flips the order of the rows of matrix <paramref name="A"/>, i.e.: works along dimension #0.
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <returns>Array of the same size and type as <paramref name="A"/> having the rows flipped.</returns>
        /// <remarks>
        /// <para>This function is an alias for <see cref="flip(BaseArray{long}, int)"/> with a <c>dim</c> argument of 0.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="flip(BaseArray{long}, int)"/>
        /// <seealso cref="fliplr(BaseArray{long})"/>
        internal static ILNumerics.Array<long> flipud(BaseArray<long> A) {
            if (Equals(A, null)) {
                throw new ArgumentException($"Input A must be a matrix.");
            }
            return flip(A, 0);
        }
        /// <summary>
        /// Flips the order of the columns of matrix <paramref name="A"/>, i.e.: works along dimension #1.
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <returns>Array of the same size and type as <paramref name="A"/> having the columns flipped.</returns>
        /// <remarks>
        /// <para>This function is an alias for <see cref="flip(BaseArray{long}, int)"/> with a <c>dim</c> argument of 0.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="flip(BaseArray{long}, int)"/>
        /// <seealso cref="flipud(BaseArray{long})"/>
        internal static ILNumerics.Array<long> fliplr(BaseArray<long> A) {
            if (Equals(A, null)) {
                throw new ArgumentException($"Input A must be a matrix.");
            }
            return flip(A, 1);
        }
    }
    namespace InnerLoops {

        namespace FlipInplace {

            internal class Int64 :

            UnaryInplaceAxisBase<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> {

                internal static Int64 Instance = new Int64();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;


                    long* cur = stackalloc long[(int)ndims];

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) { // stackalloc does not clear values
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    long accuInStride = inStrides[0];
                    while (true) {

                        //ILNumerics.Core.Misc.QuickSort.QuickSortAscST((long*)pSrc, 0, dims[0] * accuInStride, accuInStride);
                        byte* s = pSrc, e = pSrc + accuInStride * dims[0];  // dims[0] is -1!
                        long tmp = 0;
                        while (s < e) {
                            tmp = *(long*)e;
                            *(long*)e = *(long*)s;
                            *(long*)s = tmp;
                            e -= accuInStride;
                            s += accuInStride;
                        }

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
    }
    namespace InnerLoops {

        namespace FlipOOplace {

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
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    //long accuDim = dims[ndims - 1] + 1;
                    long accuInStride = inStrides[0];
                    long accuOutStride = outStrides[0];

                    long* cur = stackalloc long[(int)ndims]; // don't assume stackalloc to clear values (unspecified!)

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

                        // iteration always goes along the full accumulation length, out-element by out-element..
                        long leadLen = dims[0] + 1;

                        byte* pRead = pSrc + dims[0] * accuInStride;
                        byte* pWrite = pDest;

                        while (leadLen-- > 0) {
                            *(long*)(pWrite) = *(long*)(pRead);
                            pRead -= accuInStride; pWrite += accuOutStride;
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

        /// <summary>
        /// Flips elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] Index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim"/> = -1 depends on the value of <see cref="Settings.ArrayStyle"/>. 
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts 
        /// with the first dimension (index #0).</para>
        /// <para>This method is implemented for all common numeric element types, including <see cref="ILNumerics.complex"/> and logical arrays.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="fliplr(BaseArray{ulong})"/>
        /// <seealso cref="flipud(BaseArray{ulong})"/>
        internal static ILNumerics.Array<ulong> flip(BaseArray<ulong> A, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            var storageA = (A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>)?.Storage;
            return InnerLoops.FlipOOplace.UInt64.Instance.operate(A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>, dim); 
        }
        /// <summary>
        /// Flips the order of the rows of matrix <paramref name="A"/>, i.e.: works along dimension #0.
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <returns>Array of the same size and type as <paramref name="A"/> having the rows flipped.</returns>
        /// <remarks>
        /// <para>This function is an alias for <see cref="flip(BaseArray{ulong}, int)"/> with a <c>dim</c> argument of 0.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="flip(BaseArray{ulong}, int)"/>
        /// <seealso cref="fliplr(BaseArray{ulong})"/>
        internal static ILNumerics.Array<ulong> flipud(BaseArray<ulong> A) {
            if (Equals(A, null)) {
                throw new ArgumentException($"Input A must be a matrix.");
            }
            return flip(A, 0);
        }
        /// <summary>
        /// Flips the order of the columns of matrix <paramref name="A"/>, i.e.: works along dimension #1.
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <returns>Array of the same size and type as <paramref name="A"/> having the columns flipped.</returns>
        /// <remarks>
        /// <para>This function is an alias for <see cref="flip(BaseArray{ulong}, int)"/> with a <c>dim</c> argument of 0.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="flip(BaseArray{ulong}, int)"/>
        /// <seealso cref="flipud(BaseArray{ulong})"/>
        internal static ILNumerics.Array<ulong> fliplr(BaseArray<ulong> A) {
            if (Equals(A, null)) {
                throw new ArgumentException($"Input A must be a matrix.");
            }
            return flip(A, 1);
        }
    }
    namespace InnerLoops {

        namespace FlipInplace {

            internal class UInt64 :

            UnaryInplaceAxisBase<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> {

                internal static UInt64 Instance = new UInt64();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;


                    long* cur = stackalloc long[(int)ndims];

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) { // stackalloc does not clear values
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    long accuInStride = inStrides[0];
                    while (true) {

                        //ILNumerics.Core.Misc.QuickSort.QuickSortAscST((ulong*)pSrc, 0, dims[0] * accuInStride, accuInStride);
                        byte* s = pSrc, e = pSrc + accuInStride * dims[0];  // dims[0] is -1!
                        ulong tmp = 0;
                        while (s < e) {
                            tmp = *(ulong*)e;
                            *(ulong*)e = *(ulong*)s;
                            *(ulong*)s = tmp;
                            e -= accuInStride;
                            s += accuInStride;
                        }

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
    }
    namespace InnerLoops {

        namespace FlipOOplace {

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
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    //long accuDim = dims[ndims - 1] + 1;
                    long accuInStride = inStrides[0];
                    long accuOutStride = outStrides[0];

                    long* cur = stackalloc long[(int)ndims]; // don't assume stackalloc to clear values (unspecified!)

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

                        // iteration always goes along the full accumulation length, out-element by out-element..
                        long leadLen = dims[0] + 1;

                        byte* pRead = pSrc + dims[0] * accuInStride;
                        byte* pWrite = pDest;

                        while (leadLen-- > 0) {
                            *(ulong*)(pWrite) = *(ulong*)(pRead);
                            pRead -= accuInStride; pWrite += accuOutStride;
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

        /// <summary>
        /// Flips elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] Index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim"/> = -1 depends on the value of <see cref="Settings.ArrayStyle"/>. 
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts 
        /// with the first dimension (index #0).</para>
        /// <para>This method is implemented for all common numeric element types, including <see cref="ILNumerics.complex"/> and logical arrays.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="fliplr(BaseArray{int})"/>
        /// <seealso cref="flipud(BaseArray{int})"/>
        internal static ILNumerics.Array<int> flip(BaseArray<int> A, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            var storageA = (A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>)?.Storage;
            return InnerLoops.FlipOOplace.Int32.Instance.operate(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>, dim); 
        }
        /// <summary>
        /// Flips the order of the rows of matrix <paramref name="A"/>, i.e.: works along dimension #0.
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <returns>Array of the same size and type as <paramref name="A"/> having the rows flipped.</returns>
        /// <remarks>
        /// <para>This function is an alias for <see cref="flip(BaseArray{int}, int)"/> with a <c>dim</c> argument of 0.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="flip(BaseArray{int}, int)"/>
        /// <seealso cref="fliplr(BaseArray{int})"/>
        internal static ILNumerics.Array<int> flipud(BaseArray<int> A) {
            if (Equals(A, null)) {
                throw new ArgumentException($"Input A must be a matrix.");
            }
            return flip(A, 0);
        }
        /// <summary>
        /// Flips the order of the columns of matrix <paramref name="A"/>, i.e.: works along dimension #1.
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <returns>Array of the same size and type as <paramref name="A"/> having the columns flipped.</returns>
        /// <remarks>
        /// <para>This function is an alias for <see cref="flip(BaseArray{int}, int)"/> with a <c>dim</c> argument of 0.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="flip(BaseArray{int}, int)"/>
        /// <seealso cref="flipud(BaseArray{int})"/>
        internal static ILNumerics.Array<int> fliplr(BaseArray<int> A) {
            if (Equals(A, null)) {
                throw new ArgumentException($"Input A must be a matrix.");
            }
            return flip(A, 1);
        }
    }
    namespace InnerLoops {

        namespace FlipInplace {

            internal class Int32 :

            UnaryInplaceAxisBase<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> {

                internal static Int32 Instance = new Int32();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;


                    long* cur = stackalloc long[(int)ndims];

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) { // stackalloc does not clear values
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    long accuInStride = inStrides[0];
                    while (true) {

                        //ILNumerics.Core.Misc.QuickSort.QuickSortAscST((int*)pSrc, 0, dims[0] * accuInStride, accuInStride);
                        byte* s = pSrc, e = pSrc + accuInStride * dims[0];  // dims[0] is -1!
                        int tmp = 0;
                        while (s < e) {
                            tmp = *(int*)e;
                            *(int*)e = *(int*)s;
                            *(int*)s = tmp;
                            e -= accuInStride;
                            s += accuInStride;
                        }

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
    }
    namespace InnerLoops {

        namespace FlipOOplace {

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
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    //long accuDim = dims[ndims - 1] + 1;
                    long accuInStride = inStrides[0];
                    long accuOutStride = outStrides[0];

                    long* cur = stackalloc long[(int)ndims]; // don't assume stackalloc to clear values (unspecified!)

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

                        // iteration always goes along the full accumulation length, out-element by out-element..
                        long leadLen = dims[0] + 1;

                        byte* pRead = pSrc + dims[0] * accuInStride;
                        byte* pWrite = pDest;

                        while (leadLen-- > 0) {
                            *(int*)(pWrite) = *(int*)(pRead);
                            pRead -= accuInStride; pWrite += accuOutStride;
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

        /// <summary>
        /// Flips elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] Index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim"/> = -1 depends on the value of <see cref="Settings.ArrayStyle"/>. 
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts 
        /// with the first dimension (index #0).</para>
        /// <para>This method is implemented for all common numeric element types, including <see cref="ILNumerics.complex"/> and logical arrays.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="fliplr(BaseArray{uint})"/>
        /// <seealso cref="flipud(BaseArray{uint})"/>
        internal static ILNumerics.Array<uint> flip(BaseArray<uint> A, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            var storageA = (A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>)?.Storage;
            return InnerLoops.FlipOOplace.UInt32.Instance.operate(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>, dim); 
        }
        /// <summary>
        /// Flips the order of the rows of matrix <paramref name="A"/>, i.e.: works along dimension #0.
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <returns>Array of the same size and type as <paramref name="A"/> having the rows flipped.</returns>
        /// <remarks>
        /// <para>This function is an alias for <see cref="flip(BaseArray{uint}, int)"/> with a <c>dim</c> argument of 0.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="flip(BaseArray{uint}, int)"/>
        /// <seealso cref="fliplr(BaseArray{uint})"/>
        internal static ILNumerics.Array<uint> flipud(BaseArray<uint> A) {
            if (Equals(A, null)) {
                throw new ArgumentException($"Input A must be a matrix.");
            }
            return flip(A, 0);
        }
        /// <summary>
        /// Flips the order of the columns of matrix <paramref name="A"/>, i.e.: works along dimension #1.
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <returns>Array of the same size and type as <paramref name="A"/> having the columns flipped.</returns>
        /// <remarks>
        /// <para>This function is an alias for <see cref="flip(BaseArray{uint}, int)"/> with a <c>dim</c> argument of 0.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="flip(BaseArray{uint}, int)"/>
        /// <seealso cref="flipud(BaseArray{uint})"/>
        internal static ILNumerics.Array<uint> fliplr(BaseArray<uint> A) {
            if (Equals(A, null)) {
                throw new ArgumentException($"Input A must be a matrix.");
            }
            return flip(A, 1);
        }
    }
    namespace InnerLoops {

        namespace FlipInplace {

            internal class UInt32 :

            UnaryInplaceAxisBase<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> {

                internal static UInt32 Instance = new UInt32();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;


                    long* cur = stackalloc long[(int)ndims];

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) { // stackalloc does not clear values
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    long accuInStride = inStrides[0];
                    while (true) {

                        //ILNumerics.Core.Misc.QuickSort.QuickSortAscST((uint*)pSrc, 0, dims[0] * accuInStride, accuInStride);
                        byte* s = pSrc, e = pSrc + accuInStride * dims[0];  // dims[0] is -1!
                        uint tmp = 0;
                        while (s < e) {
                            tmp = *(uint*)e;
                            *(uint*)e = *(uint*)s;
                            *(uint*)s = tmp;
                            e -= accuInStride;
                            s += accuInStride;
                        }

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
    }
    namespace InnerLoops {

        namespace FlipOOplace {

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
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    //long accuDim = dims[ndims - 1] + 1;
                    long accuInStride = inStrides[0];
                    long accuOutStride = outStrides[0];

                    long* cur = stackalloc long[(int)ndims]; // don't assume stackalloc to clear values (unspecified!)

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

                        // iteration always goes along the full accumulation length, out-element by out-element..
                        long leadLen = dims[0] + 1;

                        byte* pRead = pSrc + dims[0] * accuInStride;
                        byte* pWrite = pDest;

                        while (leadLen-- > 0) {
                            *(uint*)(pWrite) = *(uint*)(pRead);
                            pRead -= accuInStride; pWrite += accuOutStride;
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

        /// <summary>
        /// Flips elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] Index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim"/> = -1 depends on the value of <see cref="Settings.ArrayStyle"/>. 
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts 
        /// with the first dimension (index #0).</para>
        /// <para>This method is implemented for all common numeric element types, including <see cref="ILNumerics.complex"/> and logical arrays.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="fliplr(BaseArray{short})"/>
        /// <seealso cref="flipud(BaseArray{short})"/>
        internal static ILNumerics.Array<short> flip(BaseArray<short> A, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            var storageA = (A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>)?.Storage;
            return InnerLoops.FlipOOplace.Int16.Instance.operate(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>, dim); 
        }
        /// <summary>
        /// Flips the order of the rows of matrix <paramref name="A"/>, i.e.: works along dimension #0.
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <returns>Array of the same size and type as <paramref name="A"/> having the rows flipped.</returns>
        /// <remarks>
        /// <para>This function is an alias for <see cref="flip(BaseArray{short}, int)"/> with a <c>dim</c> argument of 0.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="flip(BaseArray{short}, int)"/>
        /// <seealso cref="fliplr(BaseArray{short})"/>
        internal static ILNumerics.Array<short> flipud(BaseArray<short> A) {
            if (Equals(A, null)) {
                throw new ArgumentException($"Input A must be a matrix.");
            }
            return flip(A, 0);
        }
        /// <summary>
        /// Flips the order of the columns of matrix <paramref name="A"/>, i.e.: works along dimension #1.
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <returns>Array of the same size and type as <paramref name="A"/> having the columns flipped.</returns>
        /// <remarks>
        /// <para>This function is an alias for <see cref="flip(BaseArray{short}, int)"/> with a <c>dim</c> argument of 0.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="flip(BaseArray{short}, int)"/>
        /// <seealso cref="flipud(BaseArray{short})"/>
        internal static ILNumerics.Array<short> fliplr(BaseArray<short> A) {
            if (Equals(A, null)) {
                throw new ArgumentException($"Input A must be a matrix.");
            }
            return flip(A, 1);
        }
    }
    namespace InnerLoops {

        namespace FlipInplace {

            internal class Int16 :

            UnaryInplaceAxisBase<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> {

                internal static Int16 Instance = new Int16();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;


                    long* cur = stackalloc long[(int)ndims];

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) { // stackalloc does not clear values
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    long accuInStride = inStrides[0];
                    while (true) {

                        //ILNumerics.Core.Misc.QuickSort.QuickSortAscST((short*)pSrc, 0, dims[0] * accuInStride, accuInStride);
                        byte* s = pSrc, e = pSrc + accuInStride * dims[0];  // dims[0] is -1!
                        short tmp = 0;
                        while (s < e) {
                            tmp = *(short*)e;
                            *(short*)e = *(short*)s;
                            *(short*)s = tmp;
                            e -= accuInStride;
                            s += accuInStride;
                        }

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
    }
    namespace InnerLoops {

        namespace FlipOOplace {

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
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    //long accuDim = dims[ndims - 1] + 1;
                    long accuInStride = inStrides[0];
                    long accuOutStride = outStrides[0];

                    long* cur = stackalloc long[(int)ndims]; // don't assume stackalloc to clear values (unspecified!)

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

                        // iteration always goes along the full accumulation length, out-element by out-element..
                        long leadLen = dims[0] + 1;

                        byte* pRead = pSrc + dims[0] * accuInStride;
                        byte* pWrite = pDest;

                        while (leadLen-- > 0) {
                            *(short*)(pWrite) = *(short*)(pRead);
                            pRead -= accuInStride; pWrite += accuOutStride;
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

        /// <summary>
        /// Flips elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] Index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim"/> = -1 depends on the value of <see cref="Settings.ArrayStyle"/>. 
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts 
        /// with the first dimension (index #0).</para>
        /// <para>This method is implemented for all common numeric element types, including <see cref="ILNumerics.complex"/> and logical arrays.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="fliplr(BaseArray{ushort})"/>
        /// <seealso cref="flipud(BaseArray{ushort})"/>
        internal static ILNumerics.Array<ushort> flip(BaseArray<ushort> A, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            var storageA = (A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>)?.Storage;
            return InnerLoops.FlipOOplace.UInt16.Instance.operate(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>, dim); 
        }
        /// <summary>
        /// Flips the order of the rows of matrix <paramref name="A"/>, i.e.: works along dimension #0.
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <returns>Array of the same size and type as <paramref name="A"/> having the rows flipped.</returns>
        /// <remarks>
        /// <para>This function is an alias for <see cref="flip(BaseArray{ushort}, int)"/> with a <c>dim</c> argument of 0.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="flip(BaseArray{ushort}, int)"/>
        /// <seealso cref="fliplr(BaseArray{ushort})"/>
        internal static ILNumerics.Array<ushort> flipud(BaseArray<ushort> A) {
            if (Equals(A, null)) {
                throw new ArgumentException($"Input A must be a matrix.");
            }
            return flip(A, 0);
        }
        /// <summary>
        /// Flips the order of the columns of matrix <paramref name="A"/>, i.e.: works along dimension #1.
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <returns>Array of the same size and type as <paramref name="A"/> having the columns flipped.</returns>
        /// <remarks>
        /// <para>This function is an alias for <see cref="flip(BaseArray{ushort}, int)"/> with a <c>dim</c> argument of 0.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="flip(BaseArray{ushort}, int)"/>
        /// <seealso cref="flipud(BaseArray{ushort})"/>
        internal static ILNumerics.Array<ushort> fliplr(BaseArray<ushort> A) {
            if (Equals(A, null)) {
                throw new ArgumentException($"Input A must be a matrix.");
            }
            return flip(A, 1);
        }
    }
    namespace InnerLoops {

        namespace FlipInplace {

            internal class UInt16 :

            UnaryInplaceAxisBase<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> {

                internal static UInt16 Instance = new UInt16();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;


                    long* cur = stackalloc long[(int)ndims];

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) { // stackalloc does not clear values
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    long accuInStride = inStrides[0];
                    while (true) {

                        //ILNumerics.Core.Misc.QuickSort.QuickSortAscST((ushort*)pSrc, 0, dims[0] * accuInStride, accuInStride);
                        byte* s = pSrc, e = pSrc + accuInStride * dims[0];  // dims[0] is -1!
                        ushort tmp = 0;
                        while (s < e) {
                            tmp = *(ushort*)e;
                            *(ushort*)e = *(ushort*)s;
                            *(ushort*)s = tmp;
                            e -= accuInStride;
                            s += accuInStride;
                        }

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
    }
    namespace InnerLoops {

        namespace FlipOOplace {

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
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    //long accuDim = dims[ndims - 1] + 1;
                    long accuInStride = inStrides[0];
                    long accuOutStride = outStrides[0];

                    long* cur = stackalloc long[(int)ndims]; // don't assume stackalloc to clear values (unspecified!)

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

                        // iteration always goes along the full accumulation length, out-element by out-element..
                        long leadLen = dims[0] + 1;

                        byte* pRead = pSrc + dims[0] * accuInStride;
                        byte* pWrite = pDest;

                        while (leadLen-- > 0) {
                            *(ushort*)(pWrite) = *(ushort*)(pRead);
                            pRead -= accuInStride; pWrite += accuOutStride;
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

        /// <summary>
        /// Flips elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] Index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim"/> = -1 depends on the value of <see cref="Settings.ArrayStyle"/>. 
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts 
        /// with the first dimension (index #0).</para>
        /// <para>This method is implemented for all common numeric element types, including <see cref="ILNumerics.complex"/> and logical arrays.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="fliplr(BaseArray{sbyte})"/>
        /// <seealso cref="flipud(BaseArray{sbyte})"/>
        internal static ILNumerics.Array<sbyte> flip(BaseArray<sbyte> A, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            var storageA = (A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>)?.Storage;
            return InnerLoops.FlipOOplace.SByte.Instance.operate(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>, dim); 
        }
        /// <summary>
        /// Flips the order of the rows of matrix <paramref name="A"/>, i.e.: works along dimension #0.
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <returns>Array of the same size and type as <paramref name="A"/> having the rows flipped.</returns>
        /// <remarks>
        /// <para>This function is an alias for <see cref="flip(BaseArray{sbyte}, int)"/> with a <c>dim</c> argument of 0.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="flip(BaseArray{sbyte}, int)"/>
        /// <seealso cref="fliplr(BaseArray{sbyte})"/>
        internal static ILNumerics.Array<sbyte> flipud(BaseArray<sbyte> A) {
            if (Equals(A, null)) {
                throw new ArgumentException($"Input A must be a matrix.");
            }
            return flip(A, 0);
        }
        /// <summary>
        /// Flips the order of the columns of matrix <paramref name="A"/>, i.e.: works along dimension #1.
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <returns>Array of the same size and type as <paramref name="A"/> having the columns flipped.</returns>
        /// <remarks>
        /// <para>This function is an alias for <see cref="flip(BaseArray{sbyte}, int)"/> with a <c>dim</c> argument of 0.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="flip(BaseArray{sbyte}, int)"/>
        /// <seealso cref="flipud(BaseArray{sbyte})"/>
        internal static ILNumerics.Array<sbyte> fliplr(BaseArray<sbyte> A) {
            if (Equals(A, null)) {
                throw new ArgumentException($"Input A must be a matrix.");
            }
            return flip(A, 1);
        }
    }
    namespace InnerLoops {

        namespace FlipInplace {

            internal class SByte :

            UnaryInplaceAxisBase<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> {

                internal static SByte Instance = new SByte();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;


                    long* cur = stackalloc long[(int)ndims];

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) { // stackalloc does not clear values
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    long accuInStride = inStrides[0];
                    while (true) {

                        //ILNumerics.Core.Misc.QuickSort.QuickSortAscST((sbyte*)pSrc, 0, dims[0] * accuInStride, accuInStride);
                        byte* s = pSrc, e = pSrc + accuInStride * dims[0];  // dims[0] is -1!
                        sbyte tmp = 0;
                        while (s < e) {
                            tmp = *(sbyte*)e;
                            *(sbyte*)e = *(sbyte*)s;
                            *(sbyte*)s = tmp;
                            e -= accuInStride;
                            s += accuInStride;
                        }

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
    }
    namespace InnerLoops {

        namespace FlipOOplace {

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
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    //long accuDim = dims[ndims - 1] + 1;
                    long accuInStride = inStrides[0];
                    long accuOutStride = outStrides[0];

                    long* cur = stackalloc long[(int)ndims]; // don't assume stackalloc to clear values (unspecified!)

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

                        // iteration always goes along the full accumulation length, out-element by out-element..
                        long leadLen = dims[0] + 1;

                        byte* pRead = pSrc + dims[0] * accuInStride;
                        byte* pWrite = pDest;

                        while (leadLen-- > 0) {
                            *(sbyte*)(pWrite) = *(sbyte*)(pRead);
                            pRead -= accuInStride; pWrite += accuOutStride;
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

        /// <summary>
        /// Flips elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] Index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim"/> = -1 depends on the value of <see cref="Settings.ArrayStyle"/>. 
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts 
        /// with the first dimension (index #0).</para>
        /// <para>This method is implemented for all common numeric element types, including <see cref="ILNumerics.complex"/> and logical arrays.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="fliplr(BaseArray{byte})"/>
        /// <seealso cref="flipud(BaseArray{byte})"/>
        internal static ILNumerics.Array<byte> flip(BaseArray<byte> A, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            var storageA = (A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>)?.Storage;
            return InnerLoops.FlipOOplace.Byte.Instance.operate(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>, dim); 
        }
        /// <summary>
        /// Flips the order of the rows of matrix <paramref name="A"/>, i.e.: works along dimension #0.
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <returns>Array of the same size and type as <paramref name="A"/> having the rows flipped.</returns>
        /// <remarks>
        /// <para>This function is an alias for <see cref="flip(BaseArray{byte}, int)"/> with a <c>dim</c> argument of 0.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="flip(BaseArray{byte}, int)"/>
        /// <seealso cref="fliplr(BaseArray{byte})"/>
        internal static ILNumerics.Array<byte> flipud(BaseArray<byte> A) {
            if (Equals(A, null)) {
                throw new ArgumentException($"Input A must be a matrix.");
            }
            return flip(A, 0);
        }
        /// <summary>
        /// Flips the order of the columns of matrix <paramref name="A"/>, i.e.: works along dimension #1.
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <returns>Array of the same size and type as <paramref name="A"/> having the columns flipped.</returns>
        /// <remarks>
        /// <para>This function is an alias for <see cref="flip(BaseArray{byte}, int)"/> with a <c>dim</c> argument of 0.</para>
        /// <para>This function may works inplace and may returns the same instance of <paramref name="A"/>.</para>
        /// </remarks>
        /// <seealso cref="flip(BaseArray{byte}, int)"/>
        /// <seealso cref="flipud(BaseArray{byte})"/>
        internal static ILNumerics.Array<byte> fliplr(BaseArray<byte> A) {
            if (Equals(A, null)) {
                throw new ArgumentException($"Input A must be a matrix.");
            }
            return flip(A, 1);
        }
    }
    namespace InnerLoops {

        namespace FlipInplace {

            internal class Byte :

            UnaryInplaceAxisBase<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> {

                internal static Byte Instance = new Byte();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;


                    long* cur = stackalloc long[(int)ndims];

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) { // stackalloc does not clear values
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    long accuInStride = inStrides[0];
                    while (true) {

                        //ILNumerics.Core.Misc.QuickSort.QuickSortAscST((byte*)pSrc, 0, dims[0] * accuInStride, accuInStride);
                        byte* s = pSrc, e = pSrc + accuInStride * dims[0];  // dims[0] is -1!
                        byte tmp = 0;
                        while (s < e) {
                            tmp = *(byte*)e;
                            *(byte*)e = *(byte*)s;
                            *(byte*)s = tmp;
                            e -= accuInStride;
                            s += accuInStride;
                        }

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
    }
    namespace InnerLoops {

        namespace FlipOOplace {

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
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    //long accuDim = dims[ndims - 1] + 1;
                    long accuInStride = inStrides[0];
                    long accuOutStride = outStrides[0];

                    long* cur = stackalloc long[(int)ndims]; // don't assume stackalloc to clear values (unspecified!)

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

                        // iteration always goes along the full accumulation length, out-element by out-element..
                        long leadLen = dims[0] + 1;

                        byte* pRead = pSrc + dims[0] * accuInStride;
                        byte* pWrite = pDest;

                        while (leadLen-- > 0) {
                            *(byte*)(pWrite) = *(byte*)(pRead);
                            pRead -= accuInStride; pWrite += accuOutStride;
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

