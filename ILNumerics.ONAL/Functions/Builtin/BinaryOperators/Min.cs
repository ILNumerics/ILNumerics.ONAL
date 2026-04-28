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
using ILNumerics.Core.MemoryLayer;
using ILNumerics.Core.Native;
using ILNumerics.Core.StorageLayer;
using System;
using System.Security;
using System.Threading;
 
namespace ILNumerics.Core.Functions.Builtin {

    #region HYCALPER LOOPSTART BINARY_OPERATOR_TEMPLATE_FLOAT@Functions\Builtin\BinaryOperators\Max.cs

    /*!HC:TYPELIST:
    <hycalper>
    <type>
        <source locate="here">
            Maximum
        </source>
        <destination>Minimum</destination>
        <destination>Minimum</destination>
        <destination>Minimum</destination>
        <destination>Minimum</destination>
    </type>
    <type>
        <source locate="here">
            max
        </source>
        <destination>min</destination>
        <destination>min</destination>
        <destination>min</destination>
        <destination>min</destination>
    </type>
    <type>
        <source locate="here">
            <![CDATA[a > b]]>
        </source>
        <destination><![CDATA[a < b]]></destination>
        <destination><![CDATA[a < b]]></destination>
        <destination><![CDATA[a < b]]></destination>
        <destination><![CDATA[a < b]]></destination>
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
        <source locate="here">
            double
        </source>
        <destination>double</destination>
        <destination>float</destination>
        <destination>complex</destination>
        <destination>fcomplex</destination>
    </type>
    </hycalper>
    */

    #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 

   
    internal static partial class MathInternal {
        /// <summary>
        /// Gives the minimum of corresponding elements from <paramref name="A"/> and <paramref name="B"/>. Recognizes NaN values.
        /// </summary>
        /// <param name="A">The one array.</param>
        /// <param name="B">The other array.</param>
        /// <param name="omitNaN">[Optional] specifies how to deal with NaN values in 
        /// <paramref name="A"/> or <paramref name="B"/>. Default: (true) prefer non-NaN values if possible.</param>
        /// <returns>Result of operating elements of <paramref name="A"/> and <paramref name="B"/> elementwise.</returns>
        /// <remarks><para>Handling of NaN values: <paramref name="omitNaN"/> is true (default). For floating point element types and if one of the elements to compare is 
        /// <see cref="fcomplex.NaN"/> the other element is returned. If both elements are 
        /// <see cref="fcomplex.NaN"/> the element from <paramref name="A"/> is returned.</para>
        /// <para>If <paramref name="omitNaN"/> is false the result is <see cref="fcomplex.NaN"/> if at least 
        /// one of the elements is NaN. If both elements are NaN than the first element is returned.</para>
        /// </remarks>
        internal static Array<fcomplex> min(BaseArray<fcomplex> A, BaseArray<fcomplex> B, bool omitNaN = true) {
            if (omitNaN) {
                return InnerLoops.MinimumPrefNonNaN.FComplex.Instance.operate(
                    A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>,
                    B as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>);
            } else {
                return InnerLoops.MinimumPrefNaN.FComplex.Instance.operate(
                A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>,
                B as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>);
            }
        }
    }

    namespace InnerLoops.MinimumPrefNonNaN {

        public unsafe class FComplex
            : BroadcastingBinaryBase<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> {

            internal static FComplex Instance = new FComplex();

            /// <summary>
            /// Performs the inner loop for this binary operation.
            /// </summary>
            /// <param name="pOut">Pointer to base address of the output array, including any base offset.</param>
            /// <param name="pA">Pointer to the base address of input array A, including any base offset.</param>
            /// <param name="pB">Pointer to the base address of input array B, including any base offset.</param>
            /// <param name="start">Element index to start operation in this (thread) chunk.</param>
            /// <param name="len">Number of elements to process.</param>
            /// <param name="dims_strides"></param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            protected internal unsafe override void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len,
                long* dims_strides) {
                // . Scaled, reordered and ready 4 broadcasting (singletons -> 0-strides)
                // 
                // [ ndims ] - nOutDims
                // [ nelem ] - out nr.elements
                // [ offst ] - base offset, may be non-null for inplace operation
                // [ dim0 ] - length of out dim 0, if exists
                // [ ...1 ] - length of out dim 1, if exists
                // [ dimn-1]- length of out dim n-1, if exists!
                // [ strideA0 ] - stride A dim 0, if exists
                // [ ....  A1 ] - stride A dim 1, if exists
                // [ strideAN-1 ] - stride A dim n-1, if exists
                // [ strideB0 ] - stride B dim 0, if exists
                // [ ....  B1 ] - stride B dim 1, if exists
                // [ strideBN-1 ] - stride B dim n-1, if exists
                // 
                // all dims & strides are ndims long. pOut is iterated in contigous, COLUMN MAJOR ORDER!
                // 

                #region initialize this chunk
                uint ndims = (uint)dims_strides[0];
                fcomplex a, b; 
                if (ndims == 0) {
                    a = *(fcomplex*)pA; b = *(fcomplex*)pB; *((fcomplex*)pOut + start) = (fcomplex.IsNaN(a)) ? (fcomplex.IsNaN(b) ? a : b) : (fcomplex.IsNaN(b) ? a : (a < b ? a : b));
                    return;
                }
                long* dims = dims_strides + 3;
                long* cur = stackalloc long[(int)ndims];
                long* strideOut = dims_strides + 3 + ndims;
                long* strideA = strideOut + ndims;
                long* strideB = strideA + ndims;

                // figure out the dimension index position for start
                cur[0] = start % (dims[0] + 1);
                pOut += cur[0] * strideOut[0];
                pA += cur[0] * strideA[0];
                pB += cur[0] * strideB[0];
                long f = start / (dims[0] + 1);
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % (dims[i] + 1);
                    f /= (dims[i] + 1);
                    pOut += cur[i] * strideOut[i];
                    pA += cur[i] * strideA[i];
                    pB += cur[i] * strideB[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);
                System.Diagnostics.Debug.Assert(strideOut[0] == Storage<fcomplex>.SizeOfT, "strides for pOut are expected as column major strides!"); // 
                #endregion

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;

                    while (leadLen-- > 0) {
                        a = *(fcomplex*)pA; b = *(fcomplex*)pB; *((fcomplex*)pOut) = (fcomplex.IsNaN(a)) ? (fcomplex.IsNaN(b) ? a : b) : (fcomplex.IsNaN(b) ? a : (a < b ? a : b));
                        pOut += sizeof(fcomplex);
                        pA += *strideA;
                        pB += *strideB;
                    }
                    if (len == 0) {
                        break;
                    }
                    // reset initial offset in lead dimension after first iteration
                    cur[0] = 0;
                    pA -= strideA[0] * (dims[0] + 1);
                    pB -= strideB[0] * (dims[0] + 1);

                    // increase higher dims
                    int d = 1;
                    while (d < ndims) {
                        // dims are minus 1
                        if (cur[d] < dims[d]) {
                            pA += strideA[d];
                            pB += strideB[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            pA -= strideA[d] * dims[d];
                            pB -= strideB[d] * dims[d];
                            d++;
                        }
                    }
                }
            }
        }
    }
    namespace InnerLoops.MinimumPrefNaN {

        public unsafe class FComplex
            : BroadcastingBinaryBase<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> {

            internal static FComplex Instance = new FComplex();

            /// <summary>
            /// Performs the inner loop for this binary operation.
            /// </summary>
            /// <param name="pOut">Pointer to base address of the output array, including any base offset.</param>
            /// <param name="pA">Pointer to the base address of input array A, including any base offset.</param>
            /// <param name="pB">Pointer to the base address of input array B, including any base offset.</param>
            /// <param name="start">Element index to start operation in this (thread) chunk.</param>
            /// <param name="len">Number of elements to process.</param>
            /// <param name="dims_strides"></param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            protected internal unsafe override void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len,
                long* dims_strides) {
                // . Scaled, reordered and ready 4 broadcasting (singletons -> 0-strides)
                // 
                // [ ndims ] - nOutDims
                // [ nelem ] - out nr.elements
                // [ offst ] - base offset, may be non-null for inplace operation
                // [ dim0 ] - length of out dim 0, if exists
                // [ ...1 ] - length of out dim 1, if exists
                // [ dimn-1]- length of out dim n-1, if exists!
                // [ strideA0 ] - stride A dim 0, if exists
                // [ ....  A1 ] - stride A dim 1, if exists
                // [ strideAN-1 ] - stride A dim n-1, if exists
                // [ strideB0 ] - stride B dim 0, if exists
                // [ ....  B1 ] - stride B dim 1, if exists
                // [ strideBN-1 ] - stride B dim n-1, if exists
                // 
                // all dims & strides are ndims long. pOut is iterated in contigous, COLUMN MAJOR ORDER!
                // 

                #region initialize this chunk
                uint ndims = (uint)dims_strides[0];
                fcomplex a, b;
                if (ndims == 0) {
                    a = *(fcomplex*)pA; b = *(fcomplex*)pB; *((fcomplex*)pOut + start) = fcomplex.IsNaN(a) ? a : (fcomplex.IsNaN(b) ? b : (a < b ? a : b));
                    return;
                }
                long* dims = dims_strides + 3;
                long* cur = stackalloc long[(int)ndims];
                long* strideOut = dims_strides + 3 + ndims;
                long* strideA = strideOut + ndims;
                long* strideB = strideA + ndims;

                // figure out the dimension index position for start
                cur[0] = start % (dims[0] + 1);
                pOut += cur[0] * strideOut[0];
                pA += cur[0] * strideA[0];
                pB += cur[0] * strideB[0];
                long f = start / (dims[0] + 1);
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % (dims[i] + 1);
                    f /= (dims[i] + 1);
                    pOut += cur[i] * strideOut[i];
                    pA += cur[i] * strideA[i];
                    pB += cur[i] * strideB[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);
                System.Diagnostics.Debug.Assert(strideOut[0] == Storage<fcomplex>.SizeOfT, "strides for pOut are expected as column major strides!"); // 
                #endregion

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;

                    while (leadLen > 8) {
                        a = *(fcomplex*)(pA + 0 * *strideA); b = *(fcomplex*)(pB + 0 * *strideB); *((fcomplex*)pOut + 0) = fcomplex.IsNaN(a) ? a : (fcomplex.IsNaN(b) ? b : (a < b ? a : b));
                        a = *(fcomplex*)(pA + 1 * *strideA); b = *(fcomplex*)(pB + 1 * *strideB); *((fcomplex*)pOut + 1) = fcomplex.IsNaN(a) ? a : (fcomplex.IsNaN(b) ? b : (a < b ? a : b));
                        a = *(fcomplex*)(pA + 2 * *strideA); b = *(fcomplex*)(pB + 2 * *strideB); *((fcomplex*)pOut + 2) = fcomplex.IsNaN(a) ? a : (fcomplex.IsNaN(b) ? b : (a < b ? a : b));
                        a = *(fcomplex*)(pA + 3 * *strideA); b = *(fcomplex*)(pB + 3 * *strideB); *((fcomplex*)pOut + 3) = fcomplex.IsNaN(a) ? a : (fcomplex.IsNaN(b) ? b : (a < b ? a : b));
                        a = *(fcomplex*)(pA + 4 * *strideA); b = *(fcomplex*)(pB + 4 * *strideB); *((fcomplex*)pOut + 4) = fcomplex.IsNaN(a) ? a : (fcomplex.IsNaN(b) ? b : (a < b ? a : b));
                        a = *(fcomplex*)(pA + 5 * *strideA); b = *(fcomplex*)(pB + 5 * *strideB); *((fcomplex*)pOut + 5) = fcomplex.IsNaN(a) ? a : (fcomplex.IsNaN(b) ? b : (a < b ? a : b));
                        a = *(fcomplex*)(pA + 6 * *strideA); b = *(fcomplex*)(pB + 6 * *strideB); *((fcomplex*)pOut + 6) = fcomplex.IsNaN(a) ? a : (fcomplex.IsNaN(b) ? b : (a < b ? a : b));
                        a = *(fcomplex*)(pA + 7 * *strideA); b = *(fcomplex*)(pB + 7 * *strideB); *((fcomplex*)pOut + 7) = fcomplex.IsNaN(a) ? a : (fcomplex.IsNaN(b) ? b : (a < b ? a : b));
                        pOut += 8 * sizeof(fcomplex); pA += 8 * *strideA; pB += 8 * *strideB; leadLen -= 8;
                    }
                    while (leadLen-- > 0) {
                        a = *(fcomplex*)pA; b = *(fcomplex*)pB; *((fcomplex*)pOut) = fcomplex.IsNaN(a) ? a : (fcomplex.IsNaN(b) ? b : (a < b ? a : b));
                        pOut += sizeof(fcomplex);
                        pA += *strideA;
                        pB += *strideB;
                    }
                    if (len == 0) {
                        break;
                    }
                    // reset initial offset in lead dimension after first iteration
                    cur[0] = 0;
                    pA -= strideA[0] * (dims[0] + 1);
                    pB -= strideB[0] * (dims[0] + 1);

                    // increase higher dims
                    int d = 1;
                    while (d < ndims) {
                        // dims are minus 1
                        if (cur[d] < dims[d]) {
                            pA += strideA[d];
                            pB += strideB[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            pA -= strideA[d] * dims[d];
                            pB -= strideB[d] * dims[d];
                            d++;
                        }
                    }
                }
            }
        }
    }


   
    internal static partial class MathInternal {
        /// <summary>
        /// Gives the minimum of corresponding elements from <paramref name="A"/> and <paramref name="B"/>. Recognizes NaN values.
        /// </summary>
        /// <param name="A">The one array.</param>
        /// <param name="B">The other array.</param>
        /// <param name="omitNaN">[Optional] specifies how to deal with NaN values in 
        /// <paramref name="A"/> or <paramref name="B"/>. Default: (true) prefer non-NaN values if possible.</param>
        /// <returns>Result of operating elements of <paramref name="A"/> and <paramref name="B"/> elementwise.</returns>
        /// <remarks><para>Handling of NaN values: <paramref name="omitNaN"/> is true (default). For floating point element types and if one of the elements to compare is 
        /// <see cref="complex.NaN"/> the other element is returned. If both elements are 
        /// <see cref="complex.NaN"/> the element from <paramref name="A"/> is returned.</para>
        /// <para>If <paramref name="omitNaN"/> is false the result is <see cref="complex.NaN"/> if at least 
        /// one of the elements is NaN. If both elements are NaN than the first element is returned.</para>
        /// </remarks>
        internal static Array<complex> min(BaseArray<complex> A, BaseArray<complex> B, bool omitNaN = true) {
            if (omitNaN) {
                return InnerLoops.MinimumPrefNonNaN.Complex.Instance.operate(
                    A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>,
                    B as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>);
            } else {
                return InnerLoops.MinimumPrefNaN.Complex.Instance.operate(
                A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>,
                B as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>);
            }
        }
    }

    namespace InnerLoops.MinimumPrefNonNaN {

        public unsafe class Complex
            : BroadcastingBinaryBase<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> {

            internal static Complex Instance = new Complex();

            /// <summary>
            /// Performs the inner loop for this binary operation.
            /// </summary>
            /// <param name="pOut">Pointer to base address of the output array, including any base offset.</param>
            /// <param name="pA">Pointer to the base address of input array A, including any base offset.</param>
            /// <param name="pB">Pointer to the base address of input array B, including any base offset.</param>
            /// <param name="start">Element index to start operation in this (thread) chunk.</param>
            /// <param name="len">Number of elements to process.</param>
            /// <param name="dims_strides"></param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            protected internal unsafe override void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len,
                long* dims_strides) {
                // . Scaled, reordered and ready 4 broadcasting (singletons -> 0-strides)
                // 
                // [ ndims ] - nOutDims
                // [ nelem ] - out nr.elements
                // [ offst ] - base offset, may be non-null for inplace operation
                // [ dim0 ] - length of out dim 0, if exists
                // [ ...1 ] - length of out dim 1, if exists
                // [ dimn-1]- length of out dim n-1, if exists!
                // [ strideA0 ] - stride A dim 0, if exists
                // [ ....  A1 ] - stride A dim 1, if exists
                // [ strideAN-1 ] - stride A dim n-1, if exists
                // [ strideB0 ] - stride B dim 0, if exists
                // [ ....  B1 ] - stride B dim 1, if exists
                // [ strideBN-1 ] - stride B dim n-1, if exists
                // 
                // all dims & strides are ndims long. pOut is iterated in contigous, COLUMN MAJOR ORDER!
                // 

                #region initialize this chunk
                uint ndims = (uint)dims_strides[0];
                complex a, b; 
                if (ndims == 0) {
                    a = *(complex*)pA; b = *(complex*)pB; *((complex*)pOut + start) = (complex.IsNaN(a)) ? (complex.IsNaN(b) ? a : b) : (complex.IsNaN(b) ? a : (a < b ? a : b));
                    return;
                }
                long* dims = dims_strides + 3;
                long* cur = stackalloc long[(int)ndims];
                long* strideOut = dims_strides + 3 + ndims;
                long* strideA = strideOut + ndims;
                long* strideB = strideA + ndims;

                // figure out the dimension index position for start
                cur[0] = start % (dims[0] + 1);
                pOut += cur[0] * strideOut[0];
                pA += cur[0] * strideA[0];
                pB += cur[0] * strideB[0];
                long f = start / (dims[0] + 1);
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % (dims[i] + 1);
                    f /= (dims[i] + 1);
                    pOut += cur[i] * strideOut[i];
                    pA += cur[i] * strideA[i];
                    pB += cur[i] * strideB[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);
                System.Diagnostics.Debug.Assert(strideOut[0] == Storage<complex>.SizeOfT, "strides for pOut are expected as column major strides!"); // 
                #endregion

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;

                    while (leadLen-- > 0) {
                        a = *(complex*)pA; b = *(complex*)pB; *((complex*)pOut) = (complex.IsNaN(a)) ? (complex.IsNaN(b) ? a : b) : (complex.IsNaN(b) ? a : (a < b ? a : b));
                        pOut += sizeof(complex);
                        pA += *strideA;
                        pB += *strideB;
                    }
                    if (len == 0) {
                        break;
                    }
                    // reset initial offset in lead dimension after first iteration
                    cur[0] = 0;
                    pA -= strideA[0] * (dims[0] + 1);
                    pB -= strideB[0] * (dims[0] + 1);

                    // increase higher dims
                    int d = 1;
                    while (d < ndims) {
                        // dims are minus 1
                        if (cur[d] < dims[d]) {
                            pA += strideA[d];
                            pB += strideB[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            pA -= strideA[d] * dims[d];
                            pB -= strideB[d] * dims[d];
                            d++;
                        }
                    }
                }
            }
        }
    }
    namespace InnerLoops.MinimumPrefNaN {

        public unsafe class Complex
            : BroadcastingBinaryBase<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> {

            internal static Complex Instance = new Complex();

            /// <summary>
            /// Performs the inner loop for this binary operation.
            /// </summary>
            /// <param name="pOut">Pointer to base address of the output array, including any base offset.</param>
            /// <param name="pA">Pointer to the base address of input array A, including any base offset.</param>
            /// <param name="pB">Pointer to the base address of input array B, including any base offset.</param>
            /// <param name="start">Element index to start operation in this (thread) chunk.</param>
            /// <param name="len">Number of elements to process.</param>
            /// <param name="dims_strides"></param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            protected internal unsafe override void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len,
                long* dims_strides) {
                // . Scaled, reordered and ready 4 broadcasting (singletons -> 0-strides)
                // 
                // [ ndims ] - nOutDims
                // [ nelem ] - out nr.elements
                // [ offst ] - base offset, may be non-null for inplace operation
                // [ dim0 ] - length of out dim 0, if exists
                // [ ...1 ] - length of out dim 1, if exists
                // [ dimn-1]- length of out dim n-1, if exists!
                // [ strideA0 ] - stride A dim 0, if exists
                // [ ....  A1 ] - stride A dim 1, if exists
                // [ strideAN-1 ] - stride A dim n-1, if exists
                // [ strideB0 ] - stride B dim 0, if exists
                // [ ....  B1 ] - stride B dim 1, if exists
                // [ strideBN-1 ] - stride B dim n-1, if exists
                // 
                // all dims & strides are ndims long. pOut is iterated in contigous, COLUMN MAJOR ORDER!
                // 

                #region initialize this chunk
                uint ndims = (uint)dims_strides[0];
                complex a, b;
                if (ndims == 0) {
                    a = *(complex*)pA; b = *(complex*)pB; *((complex*)pOut + start) = complex.IsNaN(a) ? a : (complex.IsNaN(b) ? b : (a < b ? a : b));
                    return;
                }
                long* dims = dims_strides + 3;
                long* cur = stackalloc long[(int)ndims];
                long* strideOut = dims_strides + 3 + ndims;
                long* strideA = strideOut + ndims;
                long* strideB = strideA + ndims;

                // figure out the dimension index position for start
                cur[0] = start % (dims[0] + 1);
                pOut += cur[0] * strideOut[0];
                pA += cur[0] * strideA[0];
                pB += cur[0] * strideB[0];
                long f = start / (dims[0] + 1);
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % (dims[i] + 1);
                    f /= (dims[i] + 1);
                    pOut += cur[i] * strideOut[i];
                    pA += cur[i] * strideA[i];
                    pB += cur[i] * strideB[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);
                System.Diagnostics.Debug.Assert(strideOut[0] == Storage<complex>.SizeOfT, "strides for pOut are expected as column major strides!"); // 
                #endregion

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;

                    while (leadLen > 8) {
                        a = *(complex*)(pA + 0 * *strideA); b = *(complex*)(pB + 0 * *strideB); *((complex*)pOut + 0) = complex.IsNaN(a) ? a : (complex.IsNaN(b) ? b : (a < b ? a : b));
                        a = *(complex*)(pA + 1 * *strideA); b = *(complex*)(pB + 1 * *strideB); *((complex*)pOut + 1) = complex.IsNaN(a) ? a : (complex.IsNaN(b) ? b : (a < b ? a : b));
                        a = *(complex*)(pA + 2 * *strideA); b = *(complex*)(pB + 2 * *strideB); *((complex*)pOut + 2) = complex.IsNaN(a) ? a : (complex.IsNaN(b) ? b : (a < b ? a : b));
                        a = *(complex*)(pA + 3 * *strideA); b = *(complex*)(pB + 3 * *strideB); *((complex*)pOut + 3) = complex.IsNaN(a) ? a : (complex.IsNaN(b) ? b : (a < b ? a : b));
                        a = *(complex*)(pA + 4 * *strideA); b = *(complex*)(pB + 4 * *strideB); *((complex*)pOut + 4) = complex.IsNaN(a) ? a : (complex.IsNaN(b) ? b : (a < b ? a : b));
                        a = *(complex*)(pA + 5 * *strideA); b = *(complex*)(pB + 5 * *strideB); *((complex*)pOut + 5) = complex.IsNaN(a) ? a : (complex.IsNaN(b) ? b : (a < b ? a : b));
                        a = *(complex*)(pA + 6 * *strideA); b = *(complex*)(pB + 6 * *strideB); *((complex*)pOut + 6) = complex.IsNaN(a) ? a : (complex.IsNaN(b) ? b : (a < b ? a : b));
                        a = *(complex*)(pA + 7 * *strideA); b = *(complex*)(pB + 7 * *strideB); *((complex*)pOut + 7) = complex.IsNaN(a) ? a : (complex.IsNaN(b) ? b : (a < b ? a : b));
                        pOut += 8 * sizeof(complex); pA += 8 * *strideA; pB += 8 * *strideB; leadLen -= 8;
                    }
                    while (leadLen-- > 0) {
                        a = *(complex*)pA; b = *(complex*)pB; *((complex*)pOut) = complex.IsNaN(a) ? a : (complex.IsNaN(b) ? b : (a < b ? a : b));
                        pOut += sizeof(complex);
                        pA += *strideA;
                        pB += *strideB;
                    }
                    if (len == 0) {
                        break;
                    }
                    // reset initial offset in lead dimension after first iteration
                    cur[0] = 0;
                    pA -= strideA[0] * (dims[0] + 1);
                    pB -= strideB[0] * (dims[0] + 1);

                    // increase higher dims
                    int d = 1;
                    while (d < ndims) {
                        // dims are minus 1
                        if (cur[d] < dims[d]) {
                            pA += strideA[d];
                            pB += strideB[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            pA -= strideA[d] * dims[d];
                            pB -= strideB[d] * dims[d];
                            d++;
                        }
                    }
                }
            }
        }
    }


   
    internal static partial class MathInternal {
        /// <summary>
        /// Gives the minimum of corresponding elements from <paramref name="A"/> and <paramref name="B"/>. Recognizes NaN values.
        /// </summary>
        /// <param name="A">The one array.</param>
        /// <param name="B">The other array.</param>
        /// <param name="omitNaN">[Optional] specifies how to deal with NaN values in 
        /// <paramref name="A"/> or <paramref name="B"/>. Default: (true) prefer non-NaN values if possible.</param>
        /// <returns>Result of operating elements of <paramref name="A"/> and <paramref name="B"/> elementwise.</returns>
        /// <remarks><para>Handling of NaN values: <paramref name="omitNaN"/> is true (default). For floating point element types and if one of the elements to compare is 
        /// <see cref="float.NaN"/> the other element is returned. If both elements are 
        /// <see cref="float.NaN"/> the element from <paramref name="A"/> is returned.</para>
        /// <para>If <paramref name="omitNaN"/> is false the result is <see cref="float.NaN"/> if at least 
        /// one of the elements is NaN. If both elements are NaN than the first element is returned.</para>
        /// </remarks>
        internal static Array<float> min(BaseArray<float> A, BaseArray<float> B, bool omitNaN = true) {
            if (omitNaN) {
                return InnerLoops.MinimumPrefNonNaN.Single.Instance.operate(
                    A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>,
                    B as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>);
            } else {
                return InnerLoops.MinimumPrefNaN.Single.Instance.operate(
                A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>,
                B as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>);
            }
        }
    }

    namespace InnerLoops.MinimumPrefNonNaN {

        public unsafe class Single
            : BroadcastingBinaryBase<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> {

            internal static Single Instance = new Single();

            /// <summary>
            /// Performs the inner loop for this binary operation.
            /// </summary>
            /// <param name="pOut">Pointer to base address of the output array, including any base offset.</param>
            /// <param name="pA">Pointer to the base address of input array A, including any base offset.</param>
            /// <param name="pB">Pointer to the base address of input array B, including any base offset.</param>
            /// <param name="start">Element index to start operation in this (thread) chunk.</param>
            /// <param name="len">Number of elements to process.</param>
            /// <param name="dims_strides"></param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            protected internal unsafe override void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len,
                long* dims_strides) {
                // . Scaled, reordered and ready 4 broadcasting (singletons -> 0-strides)
                // 
                // [ ndims ] - nOutDims
                // [ nelem ] - out nr.elements
                // [ offst ] - base offset, may be non-null for inplace operation
                // [ dim0 ] - length of out dim 0, if exists
                // [ ...1 ] - length of out dim 1, if exists
                // [ dimn-1]- length of out dim n-1, if exists!
                // [ strideA0 ] - stride A dim 0, if exists
                // [ ....  A1 ] - stride A dim 1, if exists
                // [ strideAN-1 ] - stride A dim n-1, if exists
                // [ strideB0 ] - stride B dim 0, if exists
                // [ ....  B1 ] - stride B dim 1, if exists
                // [ strideBN-1 ] - stride B dim n-1, if exists
                // 
                // all dims & strides are ndims long. pOut is iterated in contigous, COLUMN MAJOR ORDER!
                // 

                #region initialize this chunk
                uint ndims = (uint)dims_strides[0];
                float a, b; 
                if (ndims == 0) {
                    a = *(float*)pA; b = *(float*)pB; *((float*)pOut + start) = (float.IsNaN(a)) ? (float.IsNaN(b) ? a : b) : (float.IsNaN(b) ? a : (a < b ? a : b));
                    return;
                }
                long* dims = dims_strides + 3;
                long* cur = stackalloc long[(int)ndims];
                long* strideOut = dims_strides + 3 + ndims;
                long* strideA = strideOut + ndims;
                long* strideB = strideA + ndims;

                // figure out the dimension index position for start
                cur[0] = start % (dims[0] + 1);
                pOut += cur[0] * strideOut[0];
                pA += cur[0] * strideA[0];
                pB += cur[0] * strideB[0];
                long f = start / (dims[0] + 1);
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % (dims[i] + 1);
                    f /= (dims[i] + 1);
                    pOut += cur[i] * strideOut[i];
                    pA += cur[i] * strideA[i];
                    pB += cur[i] * strideB[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);
                System.Diagnostics.Debug.Assert(strideOut[0] == Storage<float>.SizeOfT, "strides for pOut are expected as column major strides!"); // 
                #endregion

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;

                    while (leadLen-- > 0) {
                        a = *(float*)pA; b = *(float*)pB; *((float*)pOut) = (float.IsNaN(a)) ? (float.IsNaN(b) ? a : b) : (float.IsNaN(b) ? a : (a < b ? a : b));
                        pOut += sizeof(float);
                        pA += *strideA;
                        pB += *strideB;
                    }
                    if (len == 0) {
                        break;
                    }
                    // reset initial offset in lead dimension after first iteration
                    cur[0] = 0;
                    pA -= strideA[0] * (dims[0] + 1);
                    pB -= strideB[0] * (dims[0] + 1);

                    // increase higher dims
                    int d = 1;
                    while (d < ndims) {
                        // dims are minus 1
                        if (cur[d] < dims[d]) {
                            pA += strideA[d];
                            pB += strideB[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            pA -= strideA[d] * dims[d];
                            pB -= strideB[d] * dims[d];
                            d++;
                        }
                    }
                }
            }
        }
    }
    namespace InnerLoops.MinimumPrefNaN {

        public unsafe class Single
            : BroadcastingBinaryBase<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> {

            internal static Single Instance = new Single();

            /// <summary>
            /// Performs the inner loop for this binary operation.
            /// </summary>
            /// <param name="pOut">Pointer to base address of the output array, including any base offset.</param>
            /// <param name="pA">Pointer to the base address of input array A, including any base offset.</param>
            /// <param name="pB">Pointer to the base address of input array B, including any base offset.</param>
            /// <param name="start">Element index to start operation in this (thread) chunk.</param>
            /// <param name="len">Number of elements to process.</param>
            /// <param name="dims_strides"></param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            protected internal unsafe override void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len,
                long* dims_strides) {
                // . Scaled, reordered and ready 4 broadcasting (singletons -> 0-strides)
                // 
                // [ ndims ] - nOutDims
                // [ nelem ] - out nr.elements
                // [ offst ] - base offset, may be non-null for inplace operation
                // [ dim0 ] - length of out dim 0, if exists
                // [ ...1 ] - length of out dim 1, if exists
                // [ dimn-1]- length of out dim n-1, if exists!
                // [ strideA0 ] - stride A dim 0, if exists
                // [ ....  A1 ] - stride A dim 1, if exists
                // [ strideAN-1 ] - stride A dim n-1, if exists
                // [ strideB0 ] - stride B dim 0, if exists
                // [ ....  B1 ] - stride B dim 1, if exists
                // [ strideBN-1 ] - stride B dim n-1, if exists
                // 
                // all dims & strides are ndims long. pOut is iterated in contigous, COLUMN MAJOR ORDER!
                // 

                #region initialize this chunk
                uint ndims = (uint)dims_strides[0];
                float a, b;
                if (ndims == 0) {
                    a = *(float*)pA; b = *(float*)pB; *((float*)pOut + start) = float.IsNaN(a) ? a : (float.IsNaN(b) ? b : (a < b ? a : b));
                    return;
                }
                long* dims = dims_strides + 3;
                long* cur = stackalloc long[(int)ndims];
                long* strideOut = dims_strides + 3 + ndims;
                long* strideA = strideOut + ndims;
                long* strideB = strideA + ndims;

                // figure out the dimension index position for start
                cur[0] = start % (dims[0] + 1);
                pOut += cur[0] * strideOut[0];
                pA += cur[0] * strideA[0];
                pB += cur[0] * strideB[0];
                long f = start / (dims[0] + 1);
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % (dims[i] + 1);
                    f /= (dims[i] + 1);
                    pOut += cur[i] * strideOut[i];
                    pA += cur[i] * strideA[i];
                    pB += cur[i] * strideB[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);
                System.Diagnostics.Debug.Assert(strideOut[0] == Storage<float>.SizeOfT, "strides for pOut are expected as column major strides!"); // 
                #endregion

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;

                    while (leadLen > 8) {
                        a = *(float*)(pA + 0 * *strideA); b = *(float*)(pB + 0 * *strideB); *((float*)pOut + 0) = float.IsNaN(a) ? a : (float.IsNaN(b) ? b : (a < b ? a : b));
                        a = *(float*)(pA + 1 * *strideA); b = *(float*)(pB + 1 * *strideB); *((float*)pOut + 1) = float.IsNaN(a) ? a : (float.IsNaN(b) ? b : (a < b ? a : b));
                        a = *(float*)(pA + 2 * *strideA); b = *(float*)(pB + 2 * *strideB); *((float*)pOut + 2) = float.IsNaN(a) ? a : (float.IsNaN(b) ? b : (a < b ? a : b));
                        a = *(float*)(pA + 3 * *strideA); b = *(float*)(pB + 3 * *strideB); *((float*)pOut + 3) = float.IsNaN(a) ? a : (float.IsNaN(b) ? b : (a < b ? a : b));
                        a = *(float*)(pA + 4 * *strideA); b = *(float*)(pB + 4 * *strideB); *((float*)pOut + 4) = float.IsNaN(a) ? a : (float.IsNaN(b) ? b : (a < b ? a : b));
                        a = *(float*)(pA + 5 * *strideA); b = *(float*)(pB + 5 * *strideB); *((float*)pOut + 5) = float.IsNaN(a) ? a : (float.IsNaN(b) ? b : (a < b ? a : b));
                        a = *(float*)(pA + 6 * *strideA); b = *(float*)(pB + 6 * *strideB); *((float*)pOut + 6) = float.IsNaN(a) ? a : (float.IsNaN(b) ? b : (a < b ? a : b));
                        a = *(float*)(pA + 7 * *strideA); b = *(float*)(pB + 7 * *strideB); *((float*)pOut + 7) = float.IsNaN(a) ? a : (float.IsNaN(b) ? b : (a < b ? a : b));
                        pOut += 8 * sizeof(float); pA += 8 * *strideA; pB += 8 * *strideB; leadLen -= 8;
                    }
                    while (leadLen-- > 0) {
                        a = *(float*)pA; b = *(float*)pB; *((float*)pOut) = float.IsNaN(a) ? a : (float.IsNaN(b) ? b : (a < b ? a : b));
                        pOut += sizeof(float);
                        pA += *strideA;
                        pB += *strideB;
                    }
                    if (len == 0) {
                        break;
                    }
                    // reset initial offset in lead dimension after first iteration
                    cur[0] = 0;
                    pA -= strideA[0] * (dims[0] + 1);
                    pB -= strideB[0] * (dims[0] + 1);

                    // increase higher dims
                    int d = 1;
                    while (d < ndims) {
                        // dims are minus 1
                        if (cur[d] < dims[d]) {
                            pA += strideA[d];
                            pB += strideB[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            pA -= strideA[d] * dims[d];
                            pB -= strideB[d] * dims[d];
                            d++;
                        }
                    }
                }
            }
        }
    }


   
    internal static partial class MathInternal {
        /// <summary>
        /// Gives the minimum of corresponding elements from <paramref name="A"/> and <paramref name="B"/>. Recognizes NaN values.
        /// </summary>
        /// <param name="A">The one array.</param>
        /// <param name="B">The other array.</param>
        /// <param name="omitNaN">[Optional] specifies how to deal with NaN values in 
        /// <paramref name="A"/> or <paramref name="B"/>. Default: (true) prefer non-NaN values if possible.</param>
        /// <returns>Result of operating elements of <paramref name="A"/> and <paramref name="B"/> elementwise.</returns>
        /// <remarks><para>Handling of NaN values: <paramref name="omitNaN"/> is true (default). For floating point element types and if one of the elements to compare is 
        /// <see cref="double.NaN"/> the other element is returned. If both elements are 
        /// <see cref="double.NaN"/> the element from <paramref name="A"/> is returned.</para>
        /// <para>If <paramref name="omitNaN"/> is false the result is <see cref="double.NaN"/> if at least 
        /// one of the elements is NaN. If both elements are NaN than the first element is returned.</para>
        /// </remarks>
        internal static Array<double> min(BaseArray<double> A, BaseArray<double> B, bool omitNaN = true) {
            if (omitNaN) {
                return InnerLoops.MinimumPrefNonNaN.Double.Instance.operate(
                    A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>,
                    B as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>);
            } else {
                return InnerLoops.MinimumPrefNaN.Double.Instance.operate(
                A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>,
                B as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>);
            }
        }
    }

    namespace InnerLoops.MinimumPrefNonNaN {

        public unsafe class Double
            : BroadcastingBinaryBase<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> {

            internal static Double Instance = new Double();

            /// <summary>
            /// Performs the inner loop for this binary operation.
            /// </summary>
            /// <param name="pOut">Pointer to base address of the output array, including any base offset.</param>
            /// <param name="pA">Pointer to the base address of input array A, including any base offset.</param>
            /// <param name="pB">Pointer to the base address of input array B, including any base offset.</param>
            /// <param name="start">Element index to start operation in this (thread) chunk.</param>
            /// <param name="len">Number of elements to process.</param>
            /// <param name="dims_strides"></param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            protected internal unsafe override void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len,
                long* dims_strides) {
                // . Scaled, reordered and ready 4 broadcasting (singletons -> 0-strides)
                // 
                // [ ndims ] - nOutDims
                // [ nelem ] - out nr.elements
                // [ offst ] - base offset, may be non-null for inplace operation
                // [ dim0 ] - length of out dim 0, if exists
                // [ ...1 ] - length of out dim 1, if exists
                // [ dimn-1]- length of out dim n-1, if exists!
                // [ strideA0 ] - stride A dim 0, if exists
                // [ ....  A1 ] - stride A dim 1, if exists
                // [ strideAN-1 ] - stride A dim n-1, if exists
                // [ strideB0 ] - stride B dim 0, if exists
                // [ ....  B1 ] - stride B dim 1, if exists
                // [ strideBN-1 ] - stride B dim n-1, if exists
                // 
                // all dims & strides are ndims long. pOut is iterated in contigous, COLUMN MAJOR ORDER!
                // 

                #region initialize this chunk
                uint ndims = (uint)dims_strides[0];
                double a, b; 
                if (ndims == 0) {
                    a = *(double*)pA; b = *(double*)pB; *((double*)pOut + start) = (double.IsNaN(a)) ? (double.IsNaN(b) ? a : b) : (double.IsNaN(b) ? a : (a < b ? a : b));
                    return;
                }
                long* dims = dims_strides + 3;
                long* cur = stackalloc long[(int)ndims];
                long* strideOut = dims_strides + 3 + ndims;
                long* strideA = strideOut + ndims;
                long* strideB = strideA + ndims;

                // figure out the dimension index position for start
                cur[0] = start % (dims[0] + 1);
                pOut += cur[0] * strideOut[0];
                pA += cur[0] * strideA[0];
                pB += cur[0] * strideB[0];
                long f = start / (dims[0] + 1);
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % (dims[i] + 1);
                    f /= (dims[i] + 1);
                    pOut += cur[i] * strideOut[i];
                    pA += cur[i] * strideA[i];
                    pB += cur[i] * strideB[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);
                System.Diagnostics.Debug.Assert(strideOut[0] == Storage<double>.SizeOfT, "strides for pOut are expected as column major strides!"); // 
                #endregion

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;

                    while (leadLen-- > 0) {
                        a = *(double*)pA; b = *(double*)pB; *((double*)pOut) = (double.IsNaN(a)) ? (double.IsNaN(b) ? a : b) : (double.IsNaN(b) ? a : (a < b ? a : b));
                        pOut += sizeof(double);
                        pA += *strideA;
                        pB += *strideB;
                    }
                    if (len == 0) {
                        break;
                    }
                    // reset initial offset in lead dimension after first iteration
                    cur[0] = 0;
                    pA -= strideA[0] * (dims[0] + 1);
                    pB -= strideB[0] * (dims[0] + 1);

                    // increase higher dims
                    int d = 1;
                    while (d < ndims) {
                        // dims are minus 1
                        if (cur[d] < dims[d]) {
                            pA += strideA[d];
                            pB += strideB[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            pA -= strideA[d] * dims[d];
                            pB -= strideB[d] * dims[d];
                            d++;
                        }
                    }
                }
            }
        }
    }
    namespace InnerLoops.MinimumPrefNaN {

        public unsafe class Double
            : BroadcastingBinaryBase<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> {

            internal static Double Instance = new Double();

            /// <summary>
            /// Performs the inner loop for this binary operation.
            /// </summary>
            /// <param name="pOut">Pointer to base address of the output array, including any base offset.</param>
            /// <param name="pA">Pointer to the base address of input array A, including any base offset.</param>
            /// <param name="pB">Pointer to the base address of input array B, including any base offset.</param>
            /// <param name="start">Element index to start operation in this (thread) chunk.</param>
            /// <param name="len">Number of elements to process.</param>
            /// <param name="dims_strides"></param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            protected internal unsafe override void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len,
                long* dims_strides) {
                // . Scaled, reordered and ready 4 broadcasting (singletons -> 0-strides)
                // 
                // [ ndims ] - nOutDims
                // [ nelem ] - out nr.elements
                // [ offst ] - base offset, may be non-null for inplace operation
                // [ dim0 ] - length of out dim 0, if exists
                // [ ...1 ] - length of out dim 1, if exists
                // [ dimn-1]- length of out dim n-1, if exists!
                // [ strideA0 ] - stride A dim 0, if exists
                // [ ....  A1 ] - stride A dim 1, if exists
                // [ strideAN-1 ] - stride A dim n-1, if exists
                // [ strideB0 ] - stride B dim 0, if exists
                // [ ....  B1 ] - stride B dim 1, if exists
                // [ strideBN-1 ] - stride B dim n-1, if exists
                // 
                // all dims & strides are ndims long. pOut is iterated in contigous, COLUMN MAJOR ORDER!
                // 

                #region initialize this chunk
                uint ndims = (uint)dims_strides[0];
                double a, b;
                if (ndims == 0) {
                    a = *(double*)pA; b = *(double*)pB; *((double*)pOut + start) = double.IsNaN(a) ? a : (double.IsNaN(b) ? b : (a < b ? a : b));
                    return;
                }
                long* dims = dims_strides + 3;
                long* cur = stackalloc long[(int)ndims];
                long* strideOut = dims_strides + 3 + ndims;
                long* strideA = strideOut + ndims;
                long* strideB = strideA + ndims;

                // figure out the dimension index position for start
                cur[0] = start % (dims[0] + 1);
                pOut += cur[0] * strideOut[0];
                pA += cur[0] * strideA[0];
                pB += cur[0] * strideB[0];
                long f = start / (dims[0] + 1);
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % (dims[i] + 1);
                    f /= (dims[i] + 1);
                    pOut += cur[i] * strideOut[i];
                    pA += cur[i] * strideA[i];
                    pB += cur[i] * strideB[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);
                System.Diagnostics.Debug.Assert(strideOut[0] == Storage<double>.SizeOfT, "strides for pOut are expected as column major strides!"); // 
                #endregion

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;

                    while (leadLen > 8) {
                        a = *(double*)(pA + 0 * *strideA); b = *(double*)(pB + 0 * *strideB); *((double*)pOut + 0) = double.IsNaN(a) ? a : (double.IsNaN(b) ? b : (a < b ? a : b));
                        a = *(double*)(pA + 1 * *strideA); b = *(double*)(pB + 1 * *strideB); *((double*)pOut + 1) = double.IsNaN(a) ? a : (double.IsNaN(b) ? b : (a < b ? a : b));
                        a = *(double*)(pA + 2 * *strideA); b = *(double*)(pB + 2 * *strideB); *((double*)pOut + 2) = double.IsNaN(a) ? a : (double.IsNaN(b) ? b : (a < b ? a : b));
                        a = *(double*)(pA + 3 * *strideA); b = *(double*)(pB + 3 * *strideB); *((double*)pOut + 3) = double.IsNaN(a) ? a : (double.IsNaN(b) ? b : (a < b ? a : b));
                        a = *(double*)(pA + 4 * *strideA); b = *(double*)(pB + 4 * *strideB); *((double*)pOut + 4) = double.IsNaN(a) ? a : (double.IsNaN(b) ? b : (a < b ? a : b));
                        a = *(double*)(pA + 5 * *strideA); b = *(double*)(pB + 5 * *strideB); *((double*)pOut + 5) = double.IsNaN(a) ? a : (double.IsNaN(b) ? b : (a < b ? a : b));
                        a = *(double*)(pA + 6 * *strideA); b = *(double*)(pB + 6 * *strideB); *((double*)pOut + 6) = double.IsNaN(a) ? a : (double.IsNaN(b) ? b : (a < b ? a : b));
                        a = *(double*)(pA + 7 * *strideA); b = *(double*)(pB + 7 * *strideB); *((double*)pOut + 7) = double.IsNaN(a) ? a : (double.IsNaN(b) ? b : (a < b ? a : b));
                        pOut += 8 * sizeof(double); pA += 8 * *strideA; pB += 8 * *strideB; leadLen -= 8;
                    }
                    while (leadLen-- > 0) {
                        a = *(double*)pA; b = *(double*)pB; *((double*)pOut) = double.IsNaN(a) ? a : (double.IsNaN(b) ? b : (a < b ? a : b));
                        pOut += sizeof(double);
                        pA += *strideA;
                        pB += *strideB;
                    }
                    if (len == 0) {
                        break;
                    }
                    // reset initial offset in lead dimension after first iteration
                    cur[0] = 0;
                    pA -= strideA[0] * (dims[0] + 1);
                    pB -= strideB[0] * (dims[0] + 1);

                    // increase higher dims
                    int d = 1;
                    while (d < ndims) {
                        // dims are minus 1
                        if (cur[d] < dims[d]) {
                            pA += strideA[d];
                            pB += strideB[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            pA -= strideA[d] * dims[d];
                            pB -= strideB[d] * dims[d];
                            d++;
                        }
                    }
                }
            }
        }
    }


#endregion HYCALPER AUTO GENERATED CODE

    #region HYCALPER LOOPSTART BINARY_OPERATOR_TEMPLATE_INTEGER@Functions\Builtin\BinaryOperators\Max.cs
    /*!HC:TYPELIST:
    <hycalper>
    <type>
        <source locate="here">
            ulong
        </source>
        <destination>sbyte</destination>
        <destination>byte</destination>
        <destination>short</destination>
        <destination>ushort</destination>
        <destination>int</destination>
        <destination>uint</destination>
        <destination>long</destination>
        <destination>ulong</destination>
    </type>
    <type>
        <source locate="here">
            UInt64
        </source>
        <destination>SByte</destination>
        <destination>Byte</destination>
        <destination>Int16</destination>
        <destination>UInt16</destination>
        <destination>Int32</destination>
        <destination>UInt32</destination>
        <destination>Int64</destination>
        <destination>UInt64</destination>
    </type>
    <type>
        <source locate="here">
            Maximum
        </source>
        <destination>Minimum</destination>
        <destination>Minimum</destination>
        <destination>Minimum</destination>
        <destination>Minimum</destination>
        <destination>Minimum</destination>
        <destination>Minimum</destination>
        <destination>Minimum</destination>
        <destination>Minimum</destination>
   </type>
    <type>
        <source locate="here">
            max
        </source>
        <destination>min</destination>
        <destination>min</destination>
        <destination>min</destination>
        <destination>min</destination>
        <destination>min</destination>
        <destination>min</destination>
        <destination>min</destination>
        <destination>min</destination>
   </type>
    <type>
        <source locate="after" endmark=" (*">
                hcoperator
        </source>
        <destination><![CDATA[<]]></destination>
        <destination><![CDATA[<]]></destination>
        <destination><![CDATA[<]]></destination>
        <destination><![CDATA[<]]></destination>
        <destination><![CDATA[<]]></destination>
        <destination><![CDATA[<]]></destination>
        <destination><![CDATA[<]]></destination>
        <destination><![CDATA[<]]></destination>
    </type>
</hycalper>
*/
    #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 

   
    internal static partial class MathInternal { 

        /// <summary>
        /// Binary, elementwise, broadcasting operation: Minimum.
        /// </summary>
        /// <param name="A">The one array.</param>
        /// <param name="B">The other array.</param>
        /// <returns>Result of operating elements of <paramref name="A"/> and <paramref name="B"/> elementwise.</returns>
        internal static Array<ulong> min(BaseArray<ulong> A, BaseArray<ulong> B) {
            return InnerLoops.Minimum.UInt64.Instance.operate(
                A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>,
                B as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>);
        }
    }

    namespace InnerLoops.Minimum {

        public unsafe class UInt64
            : BroadcastingBinaryBase<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> {

            internal static UInt64 Instance = new UInt64();

            /// <summary>
            /// Performs the inner loop for this binary operation.
            /// </summary>
            /// <param name="pOut">Pointer to base address of the output array, including any base offset.</param>
            /// <param name="pA">Pointer to the base address of input array A, including any base offset.</param>
            /// <param name="pB">Pointer to the base address of input array B, including any base offset.</param>
            /// <param name="start">Element index to start operation in this (thread) chunk.</param>
            /// <param name="len">Number of elements to process.</param>
            /// <param name="dims_strides"></param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            protected internal unsafe override void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len,
                long* dims_strides) {
                // . Scaled, reordered and ready 4 broadcasting (singletons -> 0-strides)
                // 
                // [ ndims ] - nOutDims
                // [ nelem ] - out nr.elements
                // [ offst ] - base offset, may be non-null for inplace operation
                // [ dim0 ] - length of out dim 0, if exists
                // [ ...1 ] - length of out dim 1, if exists
                // [ dimn-1]- length of out dim n-1, if exists!
                // [ strideA0 ] - stride A dim 0, if exists
                // [ ....  A1 ] - stride A dim 1, if exists
                // [ strideAN-1 ] - stride A dim n-1, if exists
                // [ strideB0 ] - stride B dim 0, if exists
                // [ ....  B1 ] - stride B dim 1, if exists
                // [ strideBN-1 ] - stride B dim n-1, if exists
                // 
                // all dims & strides are ndims long. pOut is iterated in contigous, COLUMN MAJOR ORDER!
                // 

                #region initialize this chunk
                uint ndims = (uint)dims_strides[0];
                if (ndims == 0) {
                    *((ulong*)pOut + start) =  /*dummy*/ (*(ulong*)pA  < *(ulong*)pB ? *(ulong*)pA : *(ulong*)pB);
                    return;
                }
                long* dims = dims_strides + 3;
                long* cur = stackalloc long[(int)ndims];
                long* strideOut = dims_strides + 3 + ndims;
                long* strideA = strideOut + ndims;
                long* strideB = strideA + ndims;

                // figure out the dimension index position for start
                cur[0] = start % (dims[0] + 1);
                pOut += cur[0] * strideOut[0];
                pA += cur[0] * strideA[0];
                pB += cur[0] * strideB[0];
                long f = start / (dims[0] + 1);
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % (dims[i] + 1);
                    f /= (dims[i] + 1);
                    pOut += cur[i] * strideOut[i];
                    pA += cur[i] * strideA[i];
                    pB += cur[i] * strideB[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);
                System.Diagnostics.Debug.Assert(strideOut[0] == Storage<ulong>.SizeOfT, "strides for pOut are expected as column major strides!"); // 
                #endregion

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;

                    while (leadLen-- > 0) {
                        *(ulong*)pOut =  /*dummy*/ ((*(ulong*)(pA))  < (*(ulong*)(pB)) ? (*(ulong*)(pA)) : (*(ulong*)(pB)));
                        pOut += sizeof(ulong);
                        pA += *strideA;
                        pB += *strideB;
                    }
                    if (len == 0) {
                        break;
                    }
                    // reset initial offset in lead dimension after first iteration
                    cur[0] = 0;
                    pA -= strideA[0] * (dims[0] + 1);
                    pB -= strideB[0] * (dims[0] + 1);

                    // increase higher dims
                    int d = 1;
                    while (d < ndims) {
                        // dims are minus 1
                        if (cur[d] < dims[d]) {
                            pA += strideA[d];
                            pB += strideB[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            pA -= strideA[d] * dims[d];
                            pB -= strideB[d] * dims[d];
                            d++;
                        }
                    }
                }
            }
        }
    }

   
    internal static partial class MathInternal { 

        /// <summary>
        /// Binary, elementwise, broadcasting operation: Minimum.
        /// </summary>
        /// <param name="A">The one array.</param>
        /// <param name="B">The other array.</param>
        /// <returns>Result of operating elements of <paramref name="A"/> and <paramref name="B"/> elementwise.</returns>
        internal static Array<long> min(BaseArray<long> A, BaseArray<long> B) {
            return InnerLoops.Minimum.Int64.Instance.operate(
                A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>,
                B as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>);
        }
    }

    namespace InnerLoops.Minimum {

        public unsafe class Int64
            : BroadcastingBinaryBase<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> {

            internal static Int64 Instance = new Int64();

            /// <summary>
            /// Performs the inner loop for this binary operation.
            /// </summary>
            /// <param name="pOut">Pointer to base address of the output array, including any base offset.</param>
            /// <param name="pA">Pointer to the base address of input array A, including any base offset.</param>
            /// <param name="pB">Pointer to the base address of input array B, including any base offset.</param>
            /// <param name="start">Element index to start operation in this (thread) chunk.</param>
            /// <param name="len">Number of elements to process.</param>
            /// <param name="dims_strides"></param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            protected internal unsafe override void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len,
                long* dims_strides) {
                // . Scaled, reordered and ready 4 broadcasting (singletons -> 0-strides)
                // 
                // [ ndims ] - nOutDims
                // [ nelem ] - out nr.elements
                // [ offst ] - base offset, may be non-null for inplace operation
                // [ dim0 ] - length of out dim 0, if exists
                // [ ...1 ] - length of out dim 1, if exists
                // [ dimn-1]- length of out dim n-1, if exists!
                // [ strideA0 ] - stride A dim 0, if exists
                // [ ....  A1 ] - stride A dim 1, if exists
                // [ strideAN-1 ] - stride A dim n-1, if exists
                // [ strideB0 ] - stride B dim 0, if exists
                // [ ....  B1 ] - stride B dim 1, if exists
                // [ strideBN-1 ] - stride B dim n-1, if exists
                // 
                // all dims & strides are ndims long. pOut is iterated in contigous, COLUMN MAJOR ORDER!
                // 

                #region initialize this chunk
                uint ndims = (uint)dims_strides[0];
                if (ndims == 0) {
                    *((long*)pOut + start) =  /*dummy*/ (*(long*)pA  < *(long*)pB ? *(long*)pA : *(long*)pB);
                    return;
                }
                long* dims = dims_strides + 3;
                long* cur = stackalloc long[(int)ndims];
                long* strideOut = dims_strides + 3 + ndims;
                long* strideA = strideOut + ndims;
                long* strideB = strideA + ndims;

                // figure out the dimension index position for start
                cur[0] = start % (dims[0] + 1);
                pOut += cur[0] * strideOut[0];
                pA += cur[0] * strideA[0];
                pB += cur[0] * strideB[0];
                long f = start / (dims[0] + 1);
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % (dims[i] + 1);
                    f /= (dims[i] + 1);
                    pOut += cur[i] * strideOut[i];
                    pA += cur[i] * strideA[i];
                    pB += cur[i] * strideB[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);
                System.Diagnostics.Debug.Assert(strideOut[0] == Storage<long>.SizeOfT, "strides for pOut are expected as column major strides!"); // 
                #endregion

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;

                    while (leadLen-- > 0) {
                        *(long*)pOut =  /*dummy*/ ((*(long*)(pA))  < (*(long*)(pB)) ? (*(long*)(pA)) : (*(long*)(pB)));
                        pOut += sizeof(long);
                        pA += *strideA;
                        pB += *strideB;
                    }
                    if (len == 0) {
                        break;
                    }
                    // reset initial offset in lead dimension after first iteration
                    cur[0] = 0;
                    pA -= strideA[0] * (dims[0] + 1);
                    pB -= strideB[0] * (dims[0] + 1);

                    // increase higher dims
                    int d = 1;
                    while (d < ndims) {
                        // dims are minus 1
                        if (cur[d] < dims[d]) {
                            pA += strideA[d];
                            pB += strideB[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            pA -= strideA[d] * dims[d];
                            pB -= strideB[d] * dims[d];
                            d++;
                        }
                    }
                }
            }
        }
    }

   
    internal static partial class MathInternal { 

        /// <summary>
        /// Binary, elementwise, broadcasting operation: Minimum.
        /// </summary>
        /// <param name="A">The one array.</param>
        /// <param name="B">The other array.</param>
        /// <returns>Result of operating elements of <paramref name="A"/> and <paramref name="B"/> elementwise.</returns>
        internal static Array<uint> min(BaseArray<uint> A, BaseArray<uint> B) {
            return InnerLoops.Minimum.UInt32.Instance.operate(
                A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>,
                B as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>);
        }
    }

    namespace InnerLoops.Minimum {

        public unsafe class UInt32
            : BroadcastingBinaryBase<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> {

            internal static UInt32 Instance = new UInt32();

            /// <summary>
            /// Performs the inner loop for this binary operation.
            /// </summary>
            /// <param name="pOut">Pointer to base address of the output array, including any base offset.</param>
            /// <param name="pA">Pointer to the base address of input array A, including any base offset.</param>
            /// <param name="pB">Pointer to the base address of input array B, including any base offset.</param>
            /// <param name="start">Element index to start operation in this (thread) chunk.</param>
            /// <param name="len">Number of elements to process.</param>
            /// <param name="dims_strides"></param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            protected internal unsafe override void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len,
                long* dims_strides) {
                // . Scaled, reordered and ready 4 broadcasting (singletons -> 0-strides)
                // 
                // [ ndims ] - nOutDims
                // [ nelem ] - out nr.elements
                // [ offst ] - base offset, may be non-null for inplace operation
                // [ dim0 ] - length of out dim 0, if exists
                // [ ...1 ] - length of out dim 1, if exists
                // [ dimn-1]- length of out dim n-1, if exists!
                // [ strideA0 ] - stride A dim 0, if exists
                // [ ....  A1 ] - stride A dim 1, if exists
                // [ strideAN-1 ] - stride A dim n-1, if exists
                // [ strideB0 ] - stride B dim 0, if exists
                // [ ....  B1 ] - stride B dim 1, if exists
                // [ strideBN-1 ] - stride B dim n-1, if exists
                // 
                // all dims & strides are ndims long. pOut is iterated in contigous, COLUMN MAJOR ORDER!
                // 

                #region initialize this chunk
                uint ndims = (uint)dims_strides[0];
                if (ndims == 0) {
                    *((uint*)pOut + start) =  /*dummy*/ (*(uint*)pA  < *(uint*)pB ? *(uint*)pA : *(uint*)pB);
                    return;
                }
                long* dims = dims_strides + 3;
                long* cur = stackalloc long[(int)ndims];
                long* strideOut = dims_strides + 3 + ndims;
                long* strideA = strideOut + ndims;
                long* strideB = strideA + ndims;

                // figure out the dimension index position for start
                cur[0] = start % (dims[0] + 1);
                pOut += cur[0] * strideOut[0];
                pA += cur[0] * strideA[0];
                pB += cur[0] * strideB[0];
                long f = start / (dims[0] + 1);
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % (dims[i] + 1);
                    f /= (dims[i] + 1);
                    pOut += cur[i] * strideOut[i];
                    pA += cur[i] * strideA[i];
                    pB += cur[i] * strideB[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);
                System.Diagnostics.Debug.Assert(strideOut[0] == Storage<uint>.SizeOfT, "strides for pOut are expected as column major strides!"); // 
                #endregion

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;

                    while (leadLen-- > 0) {
                        *(uint*)pOut =  /*dummy*/ ((*(uint*)(pA))  < (*(uint*)(pB)) ? (*(uint*)(pA)) : (*(uint*)(pB)));
                        pOut += sizeof(uint);
                        pA += *strideA;
                        pB += *strideB;
                    }
                    if (len == 0) {
                        break;
                    }
                    // reset initial offset in lead dimension after first iteration
                    cur[0] = 0;
                    pA -= strideA[0] * (dims[0] + 1);
                    pB -= strideB[0] * (dims[0] + 1);

                    // increase higher dims
                    int d = 1;
                    while (d < ndims) {
                        // dims are minus 1
                        if (cur[d] < dims[d]) {
                            pA += strideA[d];
                            pB += strideB[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            pA -= strideA[d] * dims[d];
                            pB -= strideB[d] * dims[d];
                            d++;
                        }
                    }
                }
            }
        }
    }

   
    internal static partial class MathInternal { 

        /// <summary>
        /// Binary, elementwise, broadcasting operation: Minimum.
        /// </summary>
        /// <param name="A">The one array.</param>
        /// <param name="B">The other array.</param>
        /// <returns>Result of operating elements of <paramref name="A"/> and <paramref name="B"/> elementwise.</returns>
        internal static Array<int> min(BaseArray<int> A, BaseArray<int> B) {
            return InnerLoops.Minimum.Int32.Instance.operate(
                A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>,
                B as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>);
        }
    }

    namespace InnerLoops.Minimum {

        public unsafe class Int32
            : BroadcastingBinaryBase<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> {

            internal static Int32 Instance = new Int32();

            /// <summary>
            /// Performs the inner loop for this binary operation.
            /// </summary>
            /// <param name="pOut">Pointer to base address of the output array, including any base offset.</param>
            /// <param name="pA">Pointer to the base address of input array A, including any base offset.</param>
            /// <param name="pB">Pointer to the base address of input array B, including any base offset.</param>
            /// <param name="start">Element index to start operation in this (thread) chunk.</param>
            /// <param name="len">Number of elements to process.</param>
            /// <param name="dims_strides"></param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            protected internal unsafe override void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len,
                long* dims_strides) {
                // . Scaled, reordered and ready 4 broadcasting (singletons -> 0-strides)
                // 
                // [ ndims ] - nOutDims
                // [ nelem ] - out nr.elements
                // [ offst ] - base offset, may be non-null for inplace operation
                // [ dim0 ] - length of out dim 0, if exists
                // [ ...1 ] - length of out dim 1, if exists
                // [ dimn-1]- length of out dim n-1, if exists!
                // [ strideA0 ] - stride A dim 0, if exists
                // [ ....  A1 ] - stride A dim 1, if exists
                // [ strideAN-1 ] - stride A dim n-1, if exists
                // [ strideB0 ] - stride B dim 0, if exists
                // [ ....  B1 ] - stride B dim 1, if exists
                // [ strideBN-1 ] - stride B dim n-1, if exists
                // 
                // all dims & strides are ndims long. pOut is iterated in contigous, COLUMN MAJOR ORDER!
                // 

                #region initialize this chunk
                uint ndims = (uint)dims_strides[0];
                if (ndims == 0) {
                    *((int*)pOut + start) =  /*dummy*/ (*(int*)pA  < *(int*)pB ? *(int*)pA : *(int*)pB);
                    return;
                }
                long* dims = dims_strides + 3;
                long* cur = stackalloc long[(int)ndims];
                long* strideOut = dims_strides + 3 + ndims;
                long* strideA = strideOut + ndims;
                long* strideB = strideA + ndims;

                // figure out the dimension index position for start
                cur[0] = start % (dims[0] + 1);
                pOut += cur[0] * strideOut[0];
                pA += cur[0] * strideA[0];
                pB += cur[0] * strideB[0];
                long f = start / (dims[0] + 1);
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % (dims[i] + 1);
                    f /= (dims[i] + 1);
                    pOut += cur[i] * strideOut[i];
                    pA += cur[i] * strideA[i];
                    pB += cur[i] * strideB[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);
                System.Diagnostics.Debug.Assert(strideOut[0] == Storage<int>.SizeOfT, "strides for pOut are expected as column major strides!"); // 
                #endregion

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;

                    while (leadLen-- > 0) {
                        *(int*)pOut =  /*dummy*/ ((*(int*)(pA))  < (*(int*)(pB)) ? (*(int*)(pA)) : (*(int*)(pB)));
                        pOut += sizeof(int);
                        pA += *strideA;
                        pB += *strideB;
                    }
                    if (len == 0) {
                        break;
                    }
                    // reset initial offset in lead dimension after first iteration
                    cur[0] = 0;
                    pA -= strideA[0] * (dims[0] + 1);
                    pB -= strideB[0] * (dims[0] + 1);

                    // increase higher dims
                    int d = 1;
                    while (d < ndims) {
                        // dims are minus 1
                        if (cur[d] < dims[d]) {
                            pA += strideA[d];
                            pB += strideB[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            pA -= strideA[d] * dims[d];
                            pB -= strideB[d] * dims[d];
                            d++;
                        }
                    }
                }
            }
        }
    }

   
    internal static partial class MathInternal { 

        /// <summary>
        /// Binary, elementwise, broadcasting operation: Minimum.
        /// </summary>
        /// <param name="A">The one array.</param>
        /// <param name="B">The other array.</param>
        /// <returns>Result of operating elements of <paramref name="A"/> and <paramref name="B"/> elementwise.</returns>
        internal static Array<ushort> min(BaseArray<ushort> A, BaseArray<ushort> B) {
            return InnerLoops.Minimum.UInt16.Instance.operate(
                A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>,
                B as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>);
        }
    }

    namespace InnerLoops.Minimum {

        public unsafe class UInt16
            : BroadcastingBinaryBase<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> {

            internal static UInt16 Instance = new UInt16();

            /// <summary>
            /// Performs the inner loop for this binary operation.
            /// </summary>
            /// <param name="pOut">Pointer to base address of the output array, including any base offset.</param>
            /// <param name="pA">Pointer to the base address of input array A, including any base offset.</param>
            /// <param name="pB">Pointer to the base address of input array B, including any base offset.</param>
            /// <param name="start">Element index to start operation in this (thread) chunk.</param>
            /// <param name="len">Number of elements to process.</param>
            /// <param name="dims_strides"></param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            protected internal unsafe override void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len,
                long* dims_strides) {
                // . Scaled, reordered and ready 4 broadcasting (singletons -> 0-strides)
                // 
                // [ ndims ] - nOutDims
                // [ nelem ] - out nr.elements
                // [ offst ] - base offset, may be non-null for inplace operation
                // [ dim0 ] - length of out dim 0, if exists
                // [ ...1 ] - length of out dim 1, if exists
                // [ dimn-1]- length of out dim n-1, if exists!
                // [ strideA0 ] - stride A dim 0, if exists
                // [ ....  A1 ] - stride A dim 1, if exists
                // [ strideAN-1 ] - stride A dim n-1, if exists
                // [ strideB0 ] - stride B dim 0, if exists
                // [ ....  B1 ] - stride B dim 1, if exists
                // [ strideBN-1 ] - stride B dim n-1, if exists
                // 
                // all dims & strides are ndims long. pOut is iterated in contigous, COLUMN MAJOR ORDER!
                // 

                #region initialize this chunk
                uint ndims = (uint)dims_strides[0];
                if (ndims == 0) {
                    *((ushort*)pOut + start) =  /*dummy*/ (*(ushort*)pA  < *(ushort*)pB ? *(ushort*)pA : *(ushort*)pB);
                    return;
                }
                long* dims = dims_strides + 3;
                long* cur = stackalloc long[(int)ndims];
                long* strideOut = dims_strides + 3 + ndims;
                long* strideA = strideOut + ndims;
                long* strideB = strideA + ndims;

                // figure out the dimension index position for start
                cur[0] = start % (dims[0] + 1);
                pOut += cur[0] * strideOut[0];
                pA += cur[0] * strideA[0];
                pB += cur[0] * strideB[0];
                long f = start / (dims[0] + 1);
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % (dims[i] + 1);
                    f /= (dims[i] + 1);
                    pOut += cur[i] * strideOut[i];
                    pA += cur[i] * strideA[i];
                    pB += cur[i] * strideB[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);
                System.Diagnostics.Debug.Assert(strideOut[0] == Storage<ushort>.SizeOfT, "strides for pOut are expected as column major strides!"); // 
                #endregion

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;

                    while (leadLen-- > 0) {
                        *(ushort*)pOut =  /*dummy*/ ((*(ushort*)(pA))  < (*(ushort*)(pB)) ? (*(ushort*)(pA)) : (*(ushort*)(pB)));
                        pOut += sizeof(ushort);
                        pA += *strideA;
                        pB += *strideB;
                    }
                    if (len == 0) {
                        break;
                    }
                    // reset initial offset in lead dimension after first iteration
                    cur[0] = 0;
                    pA -= strideA[0] * (dims[0] + 1);
                    pB -= strideB[0] * (dims[0] + 1);

                    // increase higher dims
                    int d = 1;
                    while (d < ndims) {
                        // dims are minus 1
                        if (cur[d] < dims[d]) {
                            pA += strideA[d];
                            pB += strideB[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            pA -= strideA[d] * dims[d];
                            pB -= strideB[d] * dims[d];
                            d++;
                        }
                    }
                }
            }
        }
    }

   
    internal static partial class MathInternal { 

        /// <summary>
        /// Binary, elementwise, broadcasting operation: Minimum.
        /// </summary>
        /// <param name="A">The one array.</param>
        /// <param name="B">The other array.</param>
        /// <returns>Result of operating elements of <paramref name="A"/> and <paramref name="B"/> elementwise.</returns>
        internal static Array<short> min(BaseArray<short> A, BaseArray<short> B) {
            return InnerLoops.Minimum.Int16.Instance.operate(
                A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>,
                B as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>);
        }
    }

    namespace InnerLoops.Minimum {

        public unsafe class Int16
            : BroadcastingBinaryBase<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> {

            internal static Int16 Instance = new Int16();

            /// <summary>
            /// Performs the inner loop for this binary operation.
            /// </summary>
            /// <param name="pOut">Pointer to base address of the output array, including any base offset.</param>
            /// <param name="pA">Pointer to the base address of input array A, including any base offset.</param>
            /// <param name="pB">Pointer to the base address of input array B, including any base offset.</param>
            /// <param name="start">Element index to start operation in this (thread) chunk.</param>
            /// <param name="len">Number of elements to process.</param>
            /// <param name="dims_strides"></param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            protected internal unsafe override void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len,
                long* dims_strides) {
                // . Scaled, reordered and ready 4 broadcasting (singletons -> 0-strides)
                // 
                // [ ndims ] - nOutDims
                // [ nelem ] - out nr.elements
                // [ offst ] - base offset, may be non-null for inplace operation
                // [ dim0 ] - length of out dim 0, if exists
                // [ ...1 ] - length of out dim 1, if exists
                // [ dimn-1]- length of out dim n-1, if exists!
                // [ strideA0 ] - stride A dim 0, if exists
                // [ ....  A1 ] - stride A dim 1, if exists
                // [ strideAN-1 ] - stride A dim n-1, if exists
                // [ strideB0 ] - stride B dim 0, if exists
                // [ ....  B1 ] - stride B dim 1, if exists
                // [ strideBN-1 ] - stride B dim n-1, if exists
                // 
                // all dims & strides are ndims long. pOut is iterated in contigous, COLUMN MAJOR ORDER!
                // 

                #region initialize this chunk
                uint ndims = (uint)dims_strides[0];
                if (ndims == 0) {
                    *((short*)pOut + start) =  /*dummy*/ (*(short*)pA  < *(short*)pB ? *(short*)pA : *(short*)pB);
                    return;
                }
                long* dims = dims_strides + 3;
                long* cur = stackalloc long[(int)ndims];
                long* strideOut = dims_strides + 3 + ndims;
                long* strideA = strideOut + ndims;
                long* strideB = strideA + ndims;

                // figure out the dimension index position for start
                cur[0] = start % (dims[0] + 1);
                pOut += cur[0] * strideOut[0];
                pA += cur[0] * strideA[0];
                pB += cur[0] * strideB[0];
                long f = start / (dims[0] + 1);
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % (dims[i] + 1);
                    f /= (dims[i] + 1);
                    pOut += cur[i] * strideOut[i];
                    pA += cur[i] * strideA[i];
                    pB += cur[i] * strideB[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);
                System.Diagnostics.Debug.Assert(strideOut[0] == Storage<short>.SizeOfT, "strides for pOut are expected as column major strides!"); // 
                #endregion

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;

                    while (leadLen-- > 0) {
                        *(short*)pOut =  /*dummy*/ ((*(short*)(pA))  < (*(short*)(pB)) ? (*(short*)(pA)) : (*(short*)(pB)));
                        pOut += sizeof(short);
                        pA += *strideA;
                        pB += *strideB;
                    }
                    if (len == 0) {
                        break;
                    }
                    // reset initial offset in lead dimension after first iteration
                    cur[0] = 0;
                    pA -= strideA[0] * (dims[0] + 1);
                    pB -= strideB[0] * (dims[0] + 1);

                    // increase higher dims
                    int d = 1;
                    while (d < ndims) {
                        // dims are minus 1
                        if (cur[d] < dims[d]) {
                            pA += strideA[d];
                            pB += strideB[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            pA -= strideA[d] * dims[d];
                            pB -= strideB[d] * dims[d];
                            d++;
                        }
                    }
                }
            }
        }
    }

   
    internal static partial class MathInternal { 

        /// <summary>
        /// Binary, elementwise, broadcasting operation: Minimum.
        /// </summary>
        /// <param name="A">The one array.</param>
        /// <param name="B">The other array.</param>
        /// <returns>Result of operating elements of <paramref name="A"/> and <paramref name="B"/> elementwise.</returns>
        internal static Array<byte> min(BaseArray<byte> A, BaseArray<byte> B) {
            return InnerLoops.Minimum.Byte.Instance.operate(
                A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>,
                B as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>);
        }
    }

    namespace InnerLoops.Minimum {

        public unsafe class Byte
            : BroadcastingBinaryBase<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> {

            internal static Byte Instance = new Byte();

            /// <summary>
            /// Performs the inner loop for this binary operation.
            /// </summary>
            /// <param name="pOut">Pointer to base address of the output array, including any base offset.</param>
            /// <param name="pA">Pointer to the base address of input array A, including any base offset.</param>
            /// <param name="pB">Pointer to the base address of input array B, including any base offset.</param>
            /// <param name="start">Element index to start operation in this (thread) chunk.</param>
            /// <param name="len">Number of elements to process.</param>
            /// <param name="dims_strides"></param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            protected internal unsafe override void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len,
                long* dims_strides) {
                // . Scaled, reordered and ready 4 broadcasting (singletons -> 0-strides)
                // 
                // [ ndims ] - nOutDims
                // [ nelem ] - out nr.elements
                // [ offst ] - base offset, may be non-null for inplace operation
                // [ dim0 ] - length of out dim 0, if exists
                // [ ...1 ] - length of out dim 1, if exists
                // [ dimn-1]- length of out dim n-1, if exists!
                // [ strideA0 ] - stride A dim 0, if exists
                // [ ....  A1 ] - stride A dim 1, if exists
                // [ strideAN-1 ] - stride A dim n-1, if exists
                // [ strideB0 ] - stride B dim 0, if exists
                // [ ....  B1 ] - stride B dim 1, if exists
                // [ strideBN-1 ] - stride B dim n-1, if exists
                // 
                // all dims & strides are ndims long. pOut is iterated in contigous, COLUMN MAJOR ORDER!
                // 

                #region initialize this chunk
                uint ndims = (uint)dims_strides[0];
                if (ndims == 0) {
                    *((byte*)pOut + start) =  /*dummy*/ (*(byte*)pA  < *(byte*)pB ? *(byte*)pA : *(byte*)pB);
                    return;
                }
                long* dims = dims_strides + 3;
                long* cur = stackalloc long[(int)ndims];
                long* strideOut = dims_strides + 3 + ndims;
                long* strideA = strideOut + ndims;
                long* strideB = strideA + ndims;

                // figure out the dimension index position for start
                cur[0] = start % (dims[0] + 1);
                pOut += cur[0] * strideOut[0];
                pA += cur[0] * strideA[0];
                pB += cur[0] * strideB[0];
                long f = start / (dims[0] + 1);
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % (dims[i] + 1);
                    f /= (dims[i] + 1);
                    pOut += cur[i] * strideOut[i];
                    pA += cur[i] * strideA[i];
                    pB += cur[i] * strideB[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);
                System.Diagnostics.Debug.Assert(strideOut[0] == Storage<byte>.SizeOfT, "strides for pOut are expected as column major strides!"); // 
                #endregion

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;

                    while (leadLen-- > 0) {
                        *(byte*)pOut =  /*dummy*/ ((*(byte*)(pA))  < (*(byte*)(pB)) ? (*(byte*)(pA)) : (*(byte*)(pB)));
                        pOut += sizeof(byte);
                        pA += *strideA;
                        pB += *strideB;
                    }
                    if (len == 0) {
                        break;
                    }
                    // reset initial offset in lead dimension after first iteration
                    cur[0] = 0;
                    pA -= strideA[0] * (dims[0] + 1);
                    pB -= strideB[0] * (dims[0] + 1);

                    // increase higher dims
                    int d = 1;
                    while (d < ndims) {
                        // dims are minus 1
                        if (cur[d] < dims[d]) {
                            pA += strideA[d];
                            pB += strideB[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            pA -= strideA[d] * dims[d];
                            pB -= strideB[d] * dims[d];
                            d++;
                        }
                    }
                }
            }
        }
    }

   
    internal static partial class MathInternal { 

        /// <summary>
        /// Binary, elementwise, broadcasting operation: Minimum.
        /// </summary>
        /// <param name="A">The one array.</param>
        /// <param name="B">The other array.</param>
        /// <returns>Result of operating elements of <paramref name="A"/> and <paramref name="B"/> elementwise.</returns>
        internal static Array<sbyte> min(BaseArray<sbyte> A, BaseArray<sbyte> B) {
            return InnerLoops.Minimum.SByte.Instance.operate(
                A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>,
                B as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>);
        }
    }

    namespace InnerLoops.Minimum {

        public unsafe class SByte
            : BroadcastingBinaryBase<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> {

            internal static SByte Instance = new SByte();

            /// <summary>
            /// Performs the inner loop for this binary operation.
            /// </summary>
            /// <param name="pOut">Pointer to base address of the output array, including any base offset.</param>
            /// <param name="pA">Pointer to the base address of input array A, including any base offset.</param>
            /// <param name="pB">Pointer to the base address of input array B, including any base offset.</param>
            /// <param name="start">Element index to start operation in this (thread) chunk.</param>
            /// <param name="len">Number of elements to process.</param>
            /// <param name="dims_strides"></param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            protected internal unsafe override void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len,
                long* dims_strides) {
                // . Scaled, reordered and ready 4 broadcasting (singletons -> 0-strides)
                // 
                // [ ndims ] - nOutDims
                // [ nelem ] - out nr.elements
                // [ offst ] - base offset, may be non-null for inplace operation
                // [ dim0 ] - length of out dim 0, if exists
                // [ ...1 ] - length of out dim 1, if exists
                // [ dimn-1]- length of out dim n-1, if exists!
                // [ strideA0 ] - stride A dim 0, if exists
                // [ ....  A1 ] - stride A dim 1, if exists
                // [ strideAN-1 ] - stride A dim n-1, if exists
                // [ strideB0 ] - stride B dim 0, if exists
                // [ ....  B1 ] - stride B dim 1, if exists
                // [ strideBN-1 ] - stride B dim n-1, if exists
                // 
                // all dims & strides are ndims long. pOut is iterated in contigous, COLUMN MAJOR ORDER!
                // 

                #region initialize this chunk
                uint ndims = (uint)dims_strides[0];
                if (ndims == 0) {
                    *((sbyte*)pOut + start) =  /*dummy*/ (*(sbyte*)pA  < *(sbyte*)pB ? *(sbyte*)pA : *(sbyte*)pB);
                    return;
                }
                long* dims = dims_strides + 3;
                long* cur = stackalloc long[(int)ndims];
                long* strideOut = dims_strides + 3 + ndims;
                long* strideA = strideOut + ndims;
                long* strideB = strideA + ndims;

                // figure out the dimension index position for start
                cur[0] = start % (dims[0] + 1);
                pOut += cur[0] * strideOut[0];
                pA += cur[0] * strideA[0];
                pB += cur[0] * strideB[0];
                long f = start / (dims[0] + 1);
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % (dims[i] + 1);
                    f /= (dims[i] + 1);
                    pOut += cur[i] * strideOut[i];
                    pA += cur[i] * strideA[i];
                    pB += cur[i] * strideB[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);
                System.Diagnostics.Debug.Assert(strideOut[0] == Storage<sbyte>.SizeOfT, "strides for pOut are expected as column major strides!"); // 
                #endregion

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;

                    while (leadLen-- > 0) {
                        *(sbyte*)pOut =  /*dummy*/ ((*(sbyte*)(pA))  < (*(sbyte*)(pB)) ? (*(sbyte*)(pA)) : (*(sbyte*)(pB)));
                        pOut += sizeof(sbyte);
                        pA += *strideA;
                        pB += *strideB;
                    }
                    if (len == 0) {
                        break;
                    }
                    // reset initial offset in lead dimension after first iteration
                    cur[0] = 0;
                    pA -= strideA[0] * (dims[0] + 1);
                    pB -= strideB[0] * (dims[0] + 1);

                    // increase higher dims
                    int d = 1;
                    while (d < ndims) {
                        // dims are minus 1
                        if (cur[d] < dims[d]) {
                            pA += strideA[d];
                            pB += strideB[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            pA -= strideA[d] * dims[d];
                            pB -= strideB[d] * dims[d];
                            d++;
                        }
                    }
                }
            }
        }
    }

#endregion HYCALPER AUTO GENERATED CODE

}
