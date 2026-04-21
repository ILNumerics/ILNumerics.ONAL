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

    #region HYCALPER LOOPSTART 

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
    </hycalper>
    */
    internal static partial class MathInternal {

        /// <summary>
        /// Elementwise 'Equal' comparison operator on two arrays. Compares NaN and Inf like regular values.
        /// </summary>
        /// <param name="A">The first array.</param>
        /// <param name="B">The second array.</param>
        /// <returns>Logical array with the size broadcasted from <paramref name="A"/> and <paramref name="B"/>.</returns>
        /// <remarks><para>General notes on binary broadcasting operators: The operation is performed 
        /// elementwise on corresponding elements of <paramref name="A"/> and <paramref name="B"/>. The sizes of both 
        /// input arrays must be broadcastable and determine the size of the resulting array. Note, that 
        /// broadcasting is performed, regardless of the current setting of <see cref="Settings.ArrayStyle"/>. However, 
        /// the <see cref="Settings.ArrayStyle"/> configuration affects the operation in the following ways: 
        /// <list type="bullet">
        /// <item>If <paramref name="A"/> and <paramref name="B"/> have different number of dimensions, the array with 
        /// fewer dimensions is (virtually) padded with singleton dimensions, until the number of dimensions of both arrays match. In 
        /// <see cref="ArrayStyles.ILNumericsV4"/> the singleton dimensions are added to the end of dimensions. For <see cref="ArrayStyles.numpy"/> 
        /// singletons are padded to the start, such that here, broadcasting aligns the <i>last</i> dimensions.</item>
        /// <item><see cref="ArrayStyles.ILNumericsV4"/>: Result values of integer element types T are clamped to the 
        /// natural range of the interger type. I.e.: there will be no 'wrapping around' when exceeding the upper or lower 
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="double"/> precision 
        /// floating point arithmetics and subsequently casting the result to the desired integer value. For <see cref="ArrayStyles.numpy"/> no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Logical eqnan(BaseArray<double> A, BaseArray<double> B) {
            return InnerLoops.EqualWithEqualNaNs.Double.Instance.operate(
                A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>,
                B as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>);
        }
    }

    namespace InnerLoops.EqualWithEqualNaNs {

        internal unsafe class Double
            : BroadcastingLogicalBase<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> {

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
            /// <param name="numberTrues">[Output] stores the number of true values created in this chunk.</param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            internal unsafe override void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len,
                long* dims_strides, long* numberTrues) {
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
                // [ numberTrues0] - workitem #1 local buffer 
                // [ .... #l]      - .. workitem #l local buffer
                // 
                // all dims & strides are ndims long. pOut is iterated in contigous, COLUMN MAJOR ORDER!
                // 

                #region initialize this chunk
                uint ndims = (uint)dims_strides[0];
                if (ndims == 0) {
                    if (*(double*)pA >= *(double*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                var myNumTrues = 0;

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;
                    double a, b;
                    while (leadLen-- > 0) {
                        a = *(double*)(pA); b = *(double*)(pB); if ((double.IsNaN(a) && double.IsNaN(b)) || a == b) { myNumTrues += *(pOut + 0) = 1; } else { *(pOut + 0) = 0; }
                        pOut += sizeof(byte);
                        pA += *strideA;
                        pB += *strideB;
                    }
                    if (len == 0) {
                        numberTrues[0] = myNumTrues;
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

    #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 

   
    internal static partial class MathInternal {

        /// <summary>
        /// Elementwise 'Equal' comparison operator on two arrays. Compares NaN and Inf like regular values.
        /// </summary>
        /// <param name="A">The first array.</param>
        /// <param name="B">The second array.</param>
        /// <returns>Logical array with the size broadcasted from <paramref name="A"/> and <paramref name="B"/>.</returns>
        /// <remarks><para>General notes on binary broadcasting operators: The operation is performed 
        /// elementwise on corresponding elements of <paramref name="A"/> and <paramref name="B"/>. The sizes of both 
        /// input arrays must be broadcastable and determine the size of the resulting array. Note, that 
        /// broadcasting is performed, regardless of the current setting of <see cref="Settings.ArrayStyle"/>. However, 
        /// the <see cref="Settings.ArrayStyle"/> configuration affects the operation in the following ways: 
        /// <list type="bullet">
        /// <item>If <paramref name="A"/> and <paramref name="B"/> have different number of dimensions, the array with 
        /// fewer dimensions is (virtually) padded with singleton dimensions, until the number of dimensions of both arrays match. In 
        /// <see cref="ArrayStyles.ILNumericsV4"/> the singleton dimensions are added to the end of dimensions. For <see cref="ArrayStyles.numpy"/> 
        /// singletons are padded to the start, such that here, broadcasting aligns the <i>last</i> dimensions.</item>
        /// <item><see cref="ArrayStyles.ILNumericsV4"/>: Result values of integer element types T are clamped to the 
        /// natural range of the interger type. I.e.: there will be no 'wrapping around' when exceeding the upper or lower 
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="fcomplex"/> precision 
        /// floating point arithmetics and subsequently casting the result to the desired integer value. For <see cref="ArrayStyles.numpy"/> no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Logical eqnan(BaseArray<fcomplex> A, BaseArray<fcomplex> B) {
            return InnerLoops.EqualWithEqualNaNs.FComplex.Instance.operate(
                A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>,
                B as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>);
        }
    }

    namespace InnerLoops.EqualWithEqualNaNs {

        internal unsafe class FComplex
            : BroadcastingLogicalBase<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> {

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
            /// <param name="numberTrues">[Output] stores the number of true values created in this chunk.</param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            internal unsafe override void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len,
                long* dims_strides, long* numberTrues) {
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
                // [ numberTrues0] - workitem #1 local buffer 
                // [ .... #l]      - .. workitem #l local buffer
                // 
                // all dims & strides are ndims long. pOut is iterated in contigous, COLUMN MAJOR ORDER!
                // 

                #region initialize this chunk
                uint ndims = (uint)dims_strides[0];
                if (ndims == 0) {
                    if (*(fcomplex*)pA >= *(fcomplex*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                var myNumTrues = 0;

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;
                    fcomplex a, b;
                    while (leadLen-- > 0) {
                        a = *(fcomplex*)(pA); b = *(fcomplex*)(pB); if ((fcomplex.IsNaN(a) && fcomplex.IsNaN(b)) || a == b) { myNumTrues += *(pOut + 0) = 1; } else { *(pOut + 0) = 0; }
                        pOut += sizeof(byte);
                        pA += *strideA;
                        pB += *strideB;
                    }
                    if (len == 0) {
                        numberTrues[0] = myNumTrues;
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
        /// Elementwise 'Equal' comparison operator on two arrays. Compares NaN and Inf like regular values.
        /// </summary>
        /// <param name="A">The first array.</param>
        /// <param name="B">The second array.</param>
        /// <returns>Logical array with the size broadcasted from <paramref name="A"/> and <paramref name="B"/>.</returns>
        /// <remarks><para>General notes on binary broadcasting operators: The operation is performed 
        /// elementwise on corresponding elements of <paramref name="A"/> and <paramref name="B"/>. The sizes of both 
        /// input arrays must be broadcastable and determine the size of the resulting array. Note, that 
        /// broadcasting is performed, regardless of the current setting of <see cref="Settings.ArrayStyle"/>. However, 
        /// the <see cref="Settings.ArrayStyle"/> configuration affects the operation in the following ways: 
        /// <list type="bullet">
        /// <item>If <paramref name="A"/> and <paramref name="B"/> have different number of dimensions, the array with 
        /// fewer dimensions is (virtually) padded with singleton dimensions, until the number of dimensions of both arrays match. In 
        /// <see cref="ArrayStyles.ILNumericsV4"/> the singleton dimensions are added to the end of dimensions. For <see cref="ArrayStyles.numpy"/> 
        /// singletons are padded to the start, such that here, broadcasting aligns the <i>last</i> dimensions.</item>
        /// <item><see cref="ArrayStyles.ILNumericsV4"/>: Result values of integer element types T are clamped to the 
        /// natural range of the interger type. I.e.: there will be no 'wrapping around' when exceeding the upper or lower 
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="complex"/> precision 
        /// floating point arithmetics and subsequently casting the result to the desired integer value. For <see cref="ArrayStyles.numpy"/> no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Logical eqnan(BaseArray<complex> A, BaseArray<complex> B) {
            return InnerLoops.EqualWithEqualNaNs.Complex.Instance.operate(
                A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>,
                B as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>);
        }
    }

    namespace InnerLoops.EqualWithEqualNaNs {

        internal unsafe class Complex
            : BroadcastingLogicalBase<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> {

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
            /// <param name="numberTrues">[Output] stores the number of true values created in this chunk.</param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            internal unsafe override void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len,
                long* dims_strides, long* numberTrues) {
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
                // [ numberTrues0] - workitem #1 local buffer 
                // [ .... #l]      - .. workitem #l local buffer
                // 
                // all dims & strides are ndims long. pOut is iterated in contigous, COLUMN MAJOR ORDER!
                // 

                #region initialize this chunk
                uint ndims = (uint)dims_strides[0];
                if (ndims == 0) {
                    if (*(complex*)pA >= *(complex*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                var myNumTrues = 0;

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;
                    complex a, b;
                    while (leadLen-- > 0) {
                        a = *(complex*)(pA); b = *(complex*)(pB); if ((complex.IsNaN(a) && complex.IsNaN(b)) || a == b) { myNumTrues += *(pOut + 0) = 1; } else { *(pOut + 0) = 0; }
                        pOut += sizeof(byte);
                        pA += *strideA;
                        pB += *strideB;
                    }
                    if (len == 0) {
                        numberTrues[0] = myNumTrues;
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
        /// Elementwise 'Equal' comparison operator on two arrays. Compares NaN and Inf like regular values.
        /// </summary>
        /// <param name="A">The first array.</param>
        /// <param name="B">The second array.</param>
        /// <returns>Logical array with the size broadcasted from <paramref name="A"/> and <paramref name="B"/>.</returns>
        /// <remarks><para>General notes on binary broadcasting operators: The operation is performed 
        /// elementwise on corresponding elements of <paramref name="A"/> and <paramref name="B"/>. The sizes of both 
        /// input arrays must be broadcastable and determine the size of the resulting array. Note, that 
        /// broadcasting is performed, regardless of the current setting of <see cref="Settings.ArrayStyle"/>. However, 
        /// the <see cref="Settings.ArrayStyle"/> configuration affects the operation in the following ways: 
        /// <list type="bullet">
        /// <item>If <paramref name="A"/> and <paramref name="B"/> have different number of dimensions, the array with 
        /// fewer dimensions is (virtually) padded with singleton dimensions, until the number of dimensions of both arrays match. In 
        /// <see cref="ArrayStyles.ILNumericsV4"/> the singleton dimensions are added to the end of dimensions. For <see cref="ArrayStyles.numpy"/> 
        /// singletons are padded to the start, such that here, broadcasting aligns the <i>last</i> dimensions.</item>
        /// <item><see cref="ArrayStyles.ILNumericsV4"/>: Result values of integer element types T are clamped to the 
        /// natural range of the interger type. I.e.: there will be no 'wrapping around' when exceeding the upper or lower 
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="float"/> precision 
        /// floating point arithmetics and subsequently casting the result to the desired integer value. For <see cref="ArrayStyles.numpy"/> no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Logical eqnan(BaseArray<float> A, BaseArray<float> B) {
            return InnerLoops.EqualWithEqualNaNs.Single.Instance.operate(
                A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>,
                B as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>);
        }
    }

    namespace InnerLoops.EqualWithEqualNaNs {

        internal unsafe class Single
            : BroadcastingLogicalBase<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> {

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
            /// <param name="numberTrues">[Output] stores the number of true values created in this chunk.</param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            internal unsafe override void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len,
                long* dims_strides, long* numberTrues) {
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
                // [ numberTrues0] - workitem #1 local buffer 
                // [ .... #l]      - .. workitem #l local buffer
                // 
                // all dims & strides are ndims long. pOut is iterated in contigous, COLUMN MAJOR ORDER!
                // 

                #region initialize this chunk
                uint ndims = (uint)dims_strides[0];
                if (ndims == 0) {
                    if (*(float*)pA >= *(float*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                var myNumTrues = 0;

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;
                    float a, b;
                    while (leadLen-- > 0) {
                        a = *(float*)(pA); b = *(float*)(pB); if ((float.IsNaN(a) && float.IsNaN(b)) || a == b) { myNumTrues += *(pOut + 0) = 1; } else { *(pOut + 0) = 0; }
                        pOut += sizeof(byte);
                        pA += *strideA;
                        pB += *strideB;
                    }
                    if (len == 0) {
                        numberTrues[0] = myNumTrues;
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
