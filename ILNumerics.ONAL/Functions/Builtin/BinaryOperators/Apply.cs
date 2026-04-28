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
using System.Runtime.CompilerServices;
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
            Double
        </source>
        <destination>Single</destination>
        <destination>Complex</destination>
        <destination>FComplex</destination>
        <destination>SByte</destination>
        <destination>Byte</destination>
        <destination>Int16</destination>
        <destination>UInt16</destination>
        <destination>Int32</destination>
        <destination>UInt32</destination>
        <destination>Int64</destination>
        <destination>UInt64</destination>
   </type>
    </hycalper>
    */
    internal static partial class MathInternal {

        /// <summary>
        /// Binary, elementwise, broadcasting operation: apply(A,B).
        /// </summary>
        /// <param name="A">The one array.</param>
        /// <param name="B">The other array.</param>
        /// <param name="func">The elementary (scalar) function to be used to perform the operation on corresponding elements of <paramref name="A"/> and <paramref name="B"/>.</param>
        /// <returns>Result of operating elements of <paramref name="A"/> and <paramref name="B"/> elementwise.</returns>
        /// <remarks><para>Overloads of <see cref="apply(BaseArray{double}, BaseArray{double}, Func{double, double, double})"/> expect two
        /// arrays of broadcastable size with the same element types. They perform a mapping operation on corresponding elements 
        /// of <paramref name="A"/> and <paramref name="B"/> by evaluating <paramref name="func"/> for each element of the resulting array.</para>
        /// <para>Overloads of <see cref="apply(BaseArray{double}, BaseArray{double}, Func{double, double, double})"/> perform fastest where 
        /// the element type of the input and the output arrays match. Such overloads exist for all numeric value-typed element types.</para>
        /// <para>The function is efficiently parallelized and <paramref name="func"/> is evaluated from multiple threads. Make sure that <paramref name="func"/>
        /// is threadsafe!</para>
        /// <para>For more flexibility a generic overload exist which allows the element types of <paramref name="A"/>, <paramref name="B"/> 
        /// as well as the array returned to be individual different. See: <see cref="apply{Tin1, Tin2, Tout}(BaseArray{Tin1}, BaseArray{Tin2}, Func{Tin1, Tin2, Tout})"/>.</para></remarks>
        /// <seealso cref="apply{Tin1, Tin2, Tout}(BaseArray{Tin1}, BaseArray{Tin2}, Func{Tin1, Tin2, Tout})"/>
        /// <seealso cref="convert{inT, outT}(BaseArray{inT})"/>
        internal static Array<double> apply(BaseArray<double> A, BaseArray<double> B,
            Func<double,double,double> func) {
            return InnerLoops.Apply.Double.Instance.operate(
                A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>,
                B as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, 
                func);
        }
    }
    namespace InnerLoops.Apply {

        internal unsafe class Double
            : BroadcastingBinaryGenericBase<double, Array<double>, InArray<double>, OutArray<double>, Array<double>,
                                    Storage<double>,
                                    Func<double, double, double>> {

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
            /// <param name="func">The scalar function to be used to perform the binary action on individual, 
            /// corresponding elements of <paramref name="pA"/> and <paramref name="pB"/>.</param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            internal unsafe override void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len,
                long* dims_strides, Func<double, double, double> func) {
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
                if (dims_strides[1] == 1) { // updated 5.2: include 1d scalars as special case. Was: ndims == 0 ... 
                    *((double*)pOut + start) = func(*(double*)pA, *(double*)pB  /* ? *(double*)pA : *(double*)pB */ );
                    return;
                }
                uint ndims = (uint)dims_strides[0];
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
                        *((double*)pOut + 0) = func((*(double*)(pA + 0 * *strideA)), (*(double*)(pB + 0 * *strideB))  /* ? (*(double*)(pA + 0 * *strideA)) : (*(double*)(pB + 0 * *strideB)) */ );
                        *((double*)pOut + 1) = func((*(double*)(pA + 1 * *strideA)), (*(double*)(pB + 1 * *strideB))  /* ? (*(double*)(pA + 1 * *strideA)) : (*(double*)(pB + 1 * *strideB)) */ );
                        *((double*)pOut + 2) = func((*(double*)(pA + 2 * *strideA)), (*(double*)(pB + 2 * *strideB))  /* ? (*(double*)(pA + 2 * *strideA)) : (*(double*)(pB + 2 * *strideB)) */ );
                        *((double*)pOut + 3) = func((*(double*)(pA + 3 * *strideA)), (*(double*)(pB + 3 * *strideB))  /* ? (*(double*)(pA + 3 * *strideA)) : (*(double*)(pB + 3 * *strideB)) */ );
                        *((double*)pOut + 4) = func((*(double*)(pA + 4 * *strideA)), (*(double*)(pB + 4 * *strideB))  /* ? (*(double*)(pA + 4 * *strideA)) : (*(double*)(pB + 4 * *strideB)) */ );
                        *((double*)pOut + 5) = func((*(double*)(pA + 5 * *strideA)), (*(double*)(pB + 5 * *strideB))  /* ? (*(double*)(pA + 5 * *strideA)) : (*(double*)(pB + 5 * *strideB)) */ );
                        *((double*)pOut + 6) = func((*(double*)(pA + 6 * *strideA)), (*(double*)(pB + 6 * *strideB))  /* ? (*(double*)(pA + 6 * *strideA)) : (*(double*)(pB + 6 * *strideB)) */ );
                        *((double*)pOut + 7) = func((*(double*)(pA + 7 * *strideA)), (*(double*)(pB + 7 * *strideB))  /* ? (*(double*)(pA + 7 * *strideA)) : (*(double*)(pB + 7 * *strideB)) */ );
                        pOut += 8 * sizeof(double); pA += 8 * *strideA; pB += 8 * *strideB; leadLen -= 8;
                    }
                    while (leadLen-- > 0) {
                        *(double*)pOut = func((*(double*)(pA)), (*(double*)(pB))  /* ? (*(double*)(pA)) : (*(double*)(pB))*/ );
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

            internal override unsafe void Strided64(double[] A, double[] B, double[] C, long offsA, long offsB, long offsC,
                long start, long len, long* dims_strides, Func<double, double, double> func) {
                throw new NotImplementedException();
            }
        }
    }

    #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 

   
    internal static partial class MathInternal {

        /// <summary>
        /// Binary, elementwise, broadcasting operation: apply(A,B).
        /// </summary>
        /// <param name="A">The one array.</param>
        /// <param name="B">The other array.</param>
        /// <param name="func">The elementary (scalar) function to be used to perform the operation on corresponding elements of <paramref name="A"/> and <paramref name="B"/>.</param>
        /// <returns>Result of operating elements of <paramref name="A"/> and <paramref name="B"/> elementwise.</returns>
        /// <remarks><para>Overloads of <see cref="apply(BaseArray{ulong}, BaseArray{ulong}, Func{ulong, ulong, ulong})"/> expect two
        /// arrays of broadcastable size with the same element types. They perform a mapping operation on corresponding elements 
        /// of <paramref name="A"/> and <paramref name="B"/> by evaluating <paramref name="func"/> for each element of the resulting array.</para>
        /// <para>Overloads of <see cref="apply(BaseArray{ulong}, BaseArray{ulong}, Func{ulong, ulong, ulong})"/> perform fastest where 
        /// the element type of the input and the output arrays match. Such overloads exist for all numeric value-typed element types.</para>
        /// <para>The function is efficiently parallelized and <paramref name="func"/> is evaluated from multiple threads. Make sure that <paramref name="func"/>
        /// is threadsafe!</para>
        /// <para>For more flexibility a generic overload exist which allows the element types of <paramref name="A"/>, <paramref name="B"/> 
        /// as well as the array returned to be individual different. See: <see cref="apply{Tin1, Tin2, Tout}(BaseArray{Tin1}, BaseArray{Tin2}, Func{Tin1, Tin2, Tout})"/>.</para></remarks>
        /// <seealso cref="apply{Tin1, Tin2, Tout}(BaseArray{Tin1}, BaseArray{Tin2}, Func{Tin1, Tin2, Tout})"/>
        /// <seealso cref="convert{inT, outT}(BaseArray{inT})"/>
        internal static Array<ulong> apply(BaseArray<ulong> A, BaseArray<ulong> B,
            Func<ulong,ulong,ulong> func) {
            return InnerLoops.Apply.UInt64.Instance.operate(
                A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>,
                B as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>, 
                func);
        }
    }
    namespace InnerLoops.Apply {

        internal unsafe class UInt64
            : BroadcastingBinaryGenericBase<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>,
                                    Storage<ulong>,
                                    Func<ulong, ulong, ulong>> {

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
            /// <param name="func">The scalar function to be used to perform the binary action on individual, 
            /// corresponding elements of <paramref name="pA"/> and <paramref name="pB"/>.</param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            internal unsafe override void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len,
                long* dims_strides, Func<ulong, ulong, ulong> func) {
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
                if (dims_strides[1] == 1) { // updated 5.2: include 1d scalars as special case. Was: ndims == 0 ... 
                    *((ulong*)pOut + start) = func(*(ulong*)pA, *(ulong*)pB  /* ? *(ulong*)pA : *(ulong*)pB */ );
                    return;
                }
                uint ndims = (uint)dims_strides[0];
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

                    while (leadLen > 8) {
                        *((ulong*)pOut + 0) = func((*(ulong*)(pA + 0 * *strideA)), (*(ulong*)(pB + 0 * *strideB))  /* ? (*(ulong*)(pA + 0 * *strideA)) : (*(ulong*)(pB + 0 * *strideB)) */ );
                        *((ulong*)pOut + 1) = func((*(ulong*)(pA + 1 * *strideA)), (*(ulong*)(pB + 1 * *strideB))  /* ? (*(ulong*)(pA + 1 * *strideA)) : (*(ulong*)(pB + 1 * *strideB)) */ );
                        *((ulong*)pOut + 2) = func((*(ulong*)(pA + 2 * *strideA)), (*(ulong*)(pB + 2 * *strideB))  /* ? (*(ulong*)(pA + 2 * *strideA)) : (*(ulong*)(pB + 2 * *strideB)) */ );
                        *((ulong*)pOut + 3) = func((*(ulong*)(pA + 3 * *strideA)), (*(ulong*)(pB + 3 * *strideB))  /* ? (*(ulong*)(pA + 3 * *strideA)) : (*(ulong*)(pB + 3 * *strideB)) */ );
                        *((ulong*)pOut + 4) = func((*(ulong*)(pA + 4 * *strideA)), (*(ulong*)(pB + 4 * *strideB))  /* ? (*(ulong*)(pA + 4 * *strideA)) : (*(ulong*)(pB + 4 * *strideB)) */ );
                        *((ulong*)pOut + 5) = func((*(ulong*)(pA + 5 * *strideA)), (*(ulong*)(pB + 5 * *strideB))  /* ? (*(ulong*)(pA + 5 * *strideA)) : (*(ulong*)(pB + 5 * *strideB)) */ );
                        *((ulong*)pOut + 6) = func((*(ulong*)(pA + 6 * *strideA)), (*(ulong*)(pB + 6 * *strideB))  /* ? (*(ulong*)(pA + 6 * *strideA)) : (*(ulong*)(pB + 6 * *strideB)) */ );
                        *((ulong*)pOut + 7) = func((*(ulong*)(pA + 7 * *strideA)), (*(ulong*)(pB + 7 * *strideB))  /* ? (*(ulong*)(pA + 7 * *strideA)) : (*(ulong*)(pB + 7 * *strideB)) */ );
                        pOut += 8 * sizeof(ulong); pA += 8 * *strideA; pB += 8 * *strideB; leadLen -= 8;
                    }
                    while (leadLen-- > 0) {
                        *(ulong*)pOut = func((*(ulong*)(pA)), (*(ulong*)(pB))  /* ? (*(ulong*)(pA)) : (*(ulong*)(pB))*/ );
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

            internal override unsafe void Strided64(ulong[] A, ulong[] B, ulong[] C, long offsA, long offsB, long offsC,
                long start, long len, long* dims_strides, Func<ulong, ulong, ulong> func) {
                throw new NotImplementedException();
            }
        }
    }


   
    internal static partial class MathInternal {

        /// <summary>
        /// Binary, elementwise, broadcasting operation: apply(A,B).
        /// </summary>
        /// <param name="A">The one array.</param>
        /// <param name="B">The other array.</param>
        /// <param name="func">The elementary (scalar) function to be used to perform the operation on corresponding elements of <paramref name="A"/> and <paramref name="B"/>.</param>
        /// <returns>Result of operating elements of <paramref name="A"/> and <paramref name="B"/> elementwise.</returns>
        /// <remarks><para>Overloads of <see cref="apply(BaseArray{long}, BaseArray{long}, Func{long, long, long})"/> expect two
        /// arrays of broadcastable size with the same element types. They perform a mapping operation on corresponding elements 
        /// of <paramref name="A"/> and <paramref name="B"/> by evaluating <paramref name="func"/> for each element of the resulting array.</para>
        /// <para>Overloads of <see cref="apply(BaseArray{long}, BaseArray{long}, Func{long, long, long})"/> perform fastest where 
        /// the element type of the input and the output arrays match. Such overloads exist for all numeric value-typed element types.</para>
        /// <para>The function is efficiently parallelized and <paramref name="func"/> is evaluated from multiple threads. Make sure that <paramref name="func"/>
        /// is threadsafe!</para>
        /// <para>For more flexibility a generic overload exist which allows the element types of <paramref name="A"/>, <paramref name="B"/> 
        /// as well as the array returned to be individual different. See: <see cref="apply{Tin1, Tin2, Tout}(BaseArray{Tin1}, BaseArray{Tin2}, Func{Tin1, Tin2, Tout})"/>.</para></remarks>
        /// <seealso cref="apply{Tin1, Tin2, Tout}(BaseArray{Tin1}, BaseArray{Tin2}, Func{Tin1, Tin2, Tout})"/>
        /// <seealso cref="convert{inT, outT}(BaseArray{inT})"/>
        internal static Array<long> apply(BaseArray<long> A, BaseArray<long> B,
            Func<long,long,long> func) {
            return InnerLoops.Apply.Int64.Instance.operate(
                A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>,
                B as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, 
                func);
        }
    }
    namespace InnerLoops.Apply {

        internal unsafe class Int64
            : BroadcastingBinaryGenericBase<long, Array<long>, InArray<long>, OutArray<long>, Array<long>,
                                    Storage<long>,
                                    Func<long, long, long>> {

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
            /// <param name="func">The scalar function to be used to perform the binary action on individual, 
            /// corresponding elements of <paramref name="pA"/> and <paramref name="pB"/>.</param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            internal unsafe override void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len,
                long* dims_strides, Func<long, long, long> func) {
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
                if (dims_strides[1] == 1) { // updated 5.2: include 1d scalars as special case. Was: ndims == 0 ... 
                    *((long*)pOut + start) = func(*(long*)pA, *(long*)pB  /* ? *(long*)pA : *(long*)pB */ );
                    return;
                }
                uint ndims = (uint)dims_strides[0];
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

                    while (leadLen > 8) {
                        *((long*)pOut + 0) = func((*(long*)(pA + 0 * *strideA)), (*(long*)(pB + 0 * *strideB))  /* ? (*(long*)(pA + 0 * *strideA)) : (*(long*)(pB + 0 * *strideB)) */ );
                        *((long*)pOut + 1) = func((*(long*)(pA + 1 * *strideA)), (*(long*)(pB + 1 * *strideB))  /* ? (*(long*)(pA + 1 * *strideA)) : (*(long*)(pB + 1 * *strideB)) */ );
                        *((long*)pOut + 2) = func((*(long*)(pA + 2 * *strideA)), (*(long*)(pB + 2 * *strideB))  /* ? (*(long*)(pA + 2 * *strideA)) : (*(long*)(pB + 2 * *strideB)) */ );
                        *((long*)pOut + 3) = func((*(long*)(pA + 3 * *strideA)), (*(long*)(pB + 3 * *strideB))  /* ? (*(long*)(pA + 3 * *strideA)) : (*(long*)(pB + 3 * *strideB)) */ );
                        *((long*)pOut + 4) = func((*(long*)(pA + 4 * *strideA)), (*(long*)(pB + 4 * *strideB))  /* ? (*(long*)(pA + 4 * *strideA)) : (*(long*)(pB + 4 * *strideB)) */ );
                        *((long*)pOut + 5) = func((*(long*)(pA + 5 * *strideA)), (*(long*)(pB + 5 * *strideB))  /* ? (*(long*)(pA + 5 * *strideA)) : (*(long*)(pB + 5 * *strideB)) */ );
                        *((long*)pOut + 6) = func((*(long*)(pA + 6 * *strideA)), (*(long*)(pB + 6 * *strideB))  /* ? (*(long*)(pA + 6 * *strideA)) : (*(long*)(pB + 6 * *strideB)) */ );
                        *((long*)pOut + 7) = func((*(long*)(pA + 7 * *strideA)), (*(long*)(pB + 7 * *strideB))  /* ? (*(long*)(pA + 7 * *strideA)) : (*(long*)(pB + 7 * *strideB)) */ );
                        pOut += 8 * sizeof(long); pA += 8 * *strideA; pB += 8 * *strideB; leadLen -= 8;
                    }
                    while (leadLen-- > 0) {
                        *(long*)pOut = func((*(long*)(pA)), (*(long*)(pB))  /* ? (*(long*)(pA)) : (*(long*)(pB))*/ );
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

            internal override unsafe void Strided64(long[] A, long[] B, long[] C, long offsA, long offsB, long offsC,
                long start, long len, long* dims_strides, Func<long, long, long> func) {
                throw new NotImplementedException();
            }
        }
    }


   
    internal static partial class MathInternal {

        /// <summary>
        /// Binary, elementwise, broadcasting operation: apply(A,B).
        /// </summary>
        /// <param name="A">The one array.</param>
        /// <param name="B">The other array.</param>
        /// <param name="func">The elementary (scalar) function to be used to perform the operation on corresponding elements of <paramref name="A"/> and <paramref name="B"/>.</param>
        /// <returns>Result of operating elements of <paramref name="A"/> and <paramref name="B"/> elementwise.</returns>
        /// <remarks><para>Overloads of <see cref="apply(BaseArray{uint}, BaseArray{uint}, Func{uint, uint, uint})"/> expect two
        /// arrays of broadcastable size with the same element types. They perform a mapping operation on corresponding elements 
        /// of <paramref name="A"/> and <paramref name="B"/> by evaluating <paramref name="func"/> for each element of the resulting array.</para>
        /// <para>Overloads of <see cref="apply(BaseArray{uint}, BaseArray{uint}, Func{uint, uint, uint})"/> perform fastest where 
        /// the element type of the input and the output arrays match. Such overloads exist for all numeric value-typed element types.</para>
        /// <para>The function is efficiently parallelized and <paramref name="func"/> is evaluated from multiple threads. Make sure that <paramref name="func"/>
        /// is threadsafe!</para>
        /// <para>For more flexibility a generic overload exist which allows the element types of <paramref name="A"/>, <paramref name="B"/> 
        /// as well as the array returned to be individual different. See: <see cref="apply{Tin1, Tin2, Tout}(BaseArray{Tin1}, BaseArray{Tin2}, Func{Tin1, Tin2, Tout})"/>.</para></remarks>
        /// <seealso cref="apply{Tin1, Tin2, Tout}(BaseArray{Tin1}, BaseArray{Tin2}, Func{Tin1, Tin2, Tout})"/>
        /// <seealso cref="convert{inT, outT}(BaseArray{inT})"/>
        internal static Array<uint> apply(BaseArray<uint> A, BaseArray<uint> B,
            Func<uint,uint,uint> func) {
            return InnerLoops.Apply.UInt32.Instance.operate(
                A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>,
                B as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>, 
                func);
        }
    }
    namespace InnerLoops.Apply {

        internal unsafe class UInt32
            : BroadcastingBinaryGenericBase<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>,
                                    Storage<uint>,
                                    Func<uint, uint, uint>> {

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
            /// <param name="func">The scalar function to be used to perform the binary action on individual, 
            /// corresponding elements of <paramref name="pA"/> and <paramref name="pB"/>.</param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            internal unsafe override void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len,
                long* dims_strides, Func<uint, uint, uint> func) {
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
                if (dims_strides[1] == 1) { // updated 5.2: include 1d scalars as special case. Was: ndims == 0 ... 
                    *((uint*)pOut + start) = func(*(uint*)pA, *(uint*)pB  /* ? *(uint*)pA : *(uint*)pB */ );
                    return;
                }
                uint ndims = (uint)dims_strides[0];
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

                    while (leadLen > 8) {
                        *((uint*)pOut + 0) = func((*(uint*)(pA + 0 * *strideA)), (*(uint*)(pB + 0 * *strideB))  /* ? (*(uint*)(pA + 0 * *strideA)) : (*(uint*)(pB + 0 * *strideB)) */ );
                        *((uint*)pOut + 1) = func((*(uint*)(pA + 1 * *strideA)), (*(uint*)(pB + 1 * *strideB))  /* ? (*(uint*)(pA + 1 * *strideA)) : (*(uint*)(pB + 1 * *strideB)) */ );
                        *((uint*)pOut + 2) = func((*(uint*)(pA + 2 * *strideA)), (*(uint*)(pB + 2 * *strideB))  /* ? (*(uint*)(pA + 2 * *strideA)) : (*(uint*)(pB + 2 * *strideB)) */ );
                        *((uint*)pOut + 3) = func((*(uint*)(pA + 3 * *strideA)), (*(uint*)(pB + 3 * *strideB))  /* ? (*(uint*)(pA + 3 * *strideA)) : (*(uint*)(pB + 3 * *strideB)) */ );
                        *((uint*)pOut + 4) = func((*(uint*)(pA + 4 * *strideA)), (*(uint*)(pB + 4 * *strideB))  /* ? (*(uint*)(pA + 4 * *strideA)) : (*(uint*)(pB + 4 * *strideB)) */ );
                        *((uint*)pOut + 5) = func((*(uint*)(pA + 5 * *strideA)), (*(uint*)(pB + 5 * *strideB))  /* ? (*(uint*)(pA + 5 * *strideA)) : (*(uint*)(pB + 5 * *strideB)) */ );
                        *((uint*)pOut + 6) = func((*(uint*)(pA + 6 * *strideA)), (*(uint*)(pB + 6 * *strideB))  /* ? (*(uint*)(pA + 6 * *strideA)) : (*(uint*)(pB + 6 * *strideB)) */ );
                        *((uint*)pOut + 7) = func((*(uint*)(pA + 7 * *strideA)), (*(uint*)(pB + 7 * *strideB))  /* ? (*(uint*)(pA + 7 * *strideA)) : (*(uint*)(pB + 7 * *strideB)) */ );
                        pOut += 8 * sizeof(uint); pA += 8 * *strideA; pB += 8 * *strideB; leadLen -= 8;
                    }
                    while (leadLen-- > 0) {
                        *(uint*)pOut = func((*(uint*)(pA)), (*(uint*)(pB))  /* ? (*(uint*)(pA)) : (*(uint*)(pB))*/ );
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

            internal override unsafe void Strided64(uint[] A, uint[] B, uint[] C, long offsA, long offsB, long offsC,
                long start, long len, long* dims_strides, Func<uint, uint, uint> func) {
                throw new NotImplementedException();
            }
        }
    }


   
    internal static partial class MathInternal {

        /// <summary>
        /// Binary, elementwise, broadcasting operation: apply(A,B).
        /// </summary>
        /// <param name="A">The one array.</param>
        /// <param name="B">The other array.</param>
        /// <param name="func">The elementary (scalar) function to be used to perform the operation on corresponding elements of <paramref name="A"/> and <paramref name="B"/>.</param>
        /// <returns>Result of operating elements of <paramref name="A"/> and <paramref name="B"/> elementwise.</returns>
        /// <remarks><para>Overloads of <see cref="apply(BaseArray{int}, BaseArray{int}, Func{int, int, int})"/> expect two
        /// arrays of broadcastable size with the same element types. They perform a mapping operation on corresponding elements 
        /// of <paramref name="A"/> and <paramref name="B"/> by evaluating <paramref name="func"/> for each element of the resulting array.</para>
        /// <para>Overloads of <see cref="apply(BaseArray{int}, BaseArray{int}, Func{int, int, int})"/> perform fastest where 
        /// the element type of the input and the output arrays match. Such overloads exist for all numeric value-typed element types.</para>
        /// <para>The function is efficiently parallelized and <paramref name="func"/> is evaluated from multiple threads. Make sure that <paramref name="func"/>
        /// is threadsafe!</para>
        /// <para>For more flexibility a generic overload exist which allows the element types of <paramref name="A"/>, <paramref name="B"/> 
        /// as well as the array returned to be individual different. See: <see cref="apply{Tin1, Tin2, Tout}(BaseArray{Tin1}, BaseArray{Tin2}, Func{Tin1, Tin2, Tout})"/>.</para></remarks>
        /// <seealso cref="apply{Tin1, Tin2, Tout}(BaseArray{Tin1}, BaseArray{Tin2}, Func{Tin1, Tin2, Tout})"/>
        /// <seealso cref="convert{inT, outT}(BaseArray{inT})"/>
        internal static Array<int> apply(BaseArray<int> A, BaseArray<int> B,
            Func<int,int,int> func) {
            return InnerLoops.Apply.Int32.Instance.operate(
                A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>,
                B as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>, 
                func);
        }
    }
    namespace InnerLoops.Apply {

        internal unsafe class Int32
            : BroadcastingBinaryGenericBase<int, Array<int>, InArray<int>, OutArray<int>, Array<int>,
                                    Storage<int>,
                                    Func<int, int, int>> {

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
            /// <param name="func">The scalar function to be used to perform the binary action on individual, 
            /// corresponding elements of <paramref name="pA"/> and <paramref name="pB"/>.</param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            internal unsafe override void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len,
                long* dims_strides, Func<int, int, int> func) {
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
                if (dims_strides[1] == 1) { // updated 5.2: include 1d scalars as special case. Was: ndims == 0 ... 
                    *((int*)pOut + start) = func(*(int*)pA, *(int*)pB  /* ? *(int*)pA : *(int*)pB */ );
                    return;
                }
                uint ndims = (uint)dims_strides[0];
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

                    while (leadLen > 8) {
                        *((int*)pOut + 0) = func((*(int*)(pA + 0 * *strideA)), (*(int*)(pB + 0 * *strideB))  /* ? (*(int*)(pA + 0 * *strideA)) : (*(int*)(pB + 0 * *strideB)) */ );
                        *((int*)pOut + 1) = func((*(int*)(pA + 1 * *strideA)), (*(int*)(pB + 1 * *strideB))  /* ? (*(int*)(pA + 1 * *strideA)) : (*(int*)(pB + 1 * *strideB)) */ );
                        *((int*)pOut + 2) = func((*(int*)(pA + 2 * *strideA)), (*(int*)(pB + 2 * *strideB))  /* ? (*(int*)(pA + 2 * *strideA)) : (*(int*)(pB + 2 * *strideB)) */ );
                        *((int*)pOut + 3) = func((*(int*)(pA + 3 * *strideA)), (*(int*)(pB + 3 * *strideB))  /* ? (*(int*)(pA + 3 * *strideA)) : (*(int*)(pB + 3 * *strideB)) */ );
                        *((int*)pOut + 4) = func((*(int*)(pA + 4 * *strideA)), (*(int*)(pB + 4 * *strideB))  /* ? (*(int*)(pA + 4 * *strideA)) : (*(int*)(pB + 4 * *strideB)) */ );
                        *((int*)pOut + 5) = func((*(int*)(pA + 5 * *strideA)), (*(int*)(pB + 5 * *strideB))  /* ? (*(int*)(pA + 5 * *strideA)) : (*(int*)(pB + 5 * *strideB)) */ );
                        *((int*)pOut + 6) = func((*(int*)(pA + 6 * *strideA)), (*(int*)(pB + 6 * *strideB))  /* ? (*(int*)(pA + 6 * *strideA)) : (*(int*)(pB + 6 * *strideB)) */ );
                        *((int*)pOut + 7) = func((*(int*)(pA + 7 * *strideA)), (*(int*)(pB + 7 * *strideB))  /* ? (*(int*)(pA + 7 * *strideA)) : (*(int*)(pB + 7 * *strideB)) */ );
                        pOut += 8 * sizeof(int); pA += 8 * *strideA; pB += 8 * *strideB; leadLen -= 8;
                    }
                    while (leadLen-- > 0) {
                        *(int*)pOut = func((*(int*)(pA)), (*(int*)(pB))  /* ? (*(int*)(pA)) : (*(int*)(pB))*/ );
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

            internal override unsafe void Strided64(int[] A, int[] B, int[] C, long offsA, long offsB, long offsC,
                long start, long len, long* dims_strides, Func<int, int, int> func) {
                throw new NotImplementedException();
            }
        }
    }


   
    internal static partial class MathInternal {

        /// <summary>
        /// Binary, elementwise, broadcasting operation: apply(A,B).
        /// </summary>
        /// <param name="A">The one array.</param>
        /// <param name="B">The other array.</param>
        /// <param name="func">The elementary (scalar) function to be used to perform the operation on corresponding elements of <paramref name="A"/> and <paramref name="B"/>.</param>
        /// <returns>Result of operating elements of <paramref name="A"/> and <paramref name="B"/> elementwise.</returns>
        /// <remarks><para>Overloads of <see cref="apply(BaseArray{ushort}, BaseArray{ushort}, Func{ushort, ushort, ushort})"/> expect two
        /// arrays of broadcastable size with the same element types. They perform a mapping operation on corresponding elements 
        /// of <paramref name="A"/> and <paramref name="B"/> by evaluating <paramref name="func"/> for each element of the resulting array.</para>
        /// <para>Overloads of <see cref="apply(BaseArray{ushort}, BaseArray{ushort}, Func{ushort, ushort, ushort})"/> perform fastest where 
        /// the element type of the input and the output arrays match. Such overloads exist for all numeric value-typed element types.</para>
        /// <para>The function is efficiently parallelized and <paramref name="func"/> is evaluated from multiple threads. Make sure that <paramref name="func"/>
        /// is threadsafe!</para>
        /// <para>For more flexibility a generic overload exist which allows the element types of <paramref name="A"/>, <paramref name="B"/> 
        /// as well as the array returned to be individual different. See: <see cref="apply{Tin1, Tin2, Tout}(BaseArray{Tin1}, BaseArray{Tin2}, Func{Tin1, Tin2, Tout})"/>.</para></remarks>
        /// <seealso cref="apply{Tin1, Tin2, Tout}(BaseArray{Tin1}, BaseArray{Tin2}, Func{Tin1, Tin2, Tout})"/>
        /// <seealso cref="convert{inT, outT}(BaseArray{inT})"/>
        internal static Array<ushort> apply(BaseArray<ushort> A, BaseArray<ushort> B,
            Func<ushort,ushort,ushort> func) {
            return InnerLoops.Apply.UInt16.Instance.operate(
                A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>,
                B as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>, 
                func);
        }
    }
    namespace InnerLoops.Apply {

        internal unsafe class UInt16
            : BroadcastingBinaryGenericBase<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>,
                                    Storage<ushort>,
                                    Func<ushort, ushort, ushort>> {

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
            /// <param name="func">The scalar function to be used to perform the binary action on individual, 
            /// corresponding elements of <paramref name="pA"/> and <paramref name="pB"/>.</param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            internal unsafe override void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len,
                long* dims_strides, Func<ushort, ushort, ushort> func) {
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
                if (dims_strides[1] == 1) { // updated 5.2: include 1d scalars as special case. Was: ndims == 0 ... 
                    *((ushort*)pOut + start) = func(*(ushort*)pA, *(ushort*)pB  /* ? *(ushort*)pA : *(ushort*)pB */ );
                    return;
                }
                uint ndims = (uint)dims_strides[0];
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

                    while (leadLen > 8) {
                        *((ushort*)pOut + 0) = func((*(ushort*)(pA + 0 * *strideA)), (*(ushort*)(pB + 0 * *strideB))  /* ? (*(ushort*)(pA + 0 * *strideA)) : (*(ushort*)(pB + 0 * *strideB)) */ );
                        *((ushort*)pOut + 1) = func((*(ushort*)(pA + 1 * *strideA)), (*(ushort*)(pB + 1 * *strideB))  /* ? (*(ushort*)(pA + 1 * *strideA)) : (*(ushort*)(pB + 1 * *strideB)) */ );
                        *((ushort*)pOut + 2) = func((*(ushort*)(pA + 2 * *strideA)), (*(ushort*)(pB + 2 * *strideB))  /* ? (*(ushort*)(pA + 2 * *strideA)) : (*(ushort*)(pB + 2 * *strideB)) */ );
                        *((ushort*)pOut + 3) = func((*(ushort*)(pA + 3 * *strideA)), (*(ushort*)(pB + 3 * *strideB))  /* ? (*(ushort*)(pA + 3 * *strideA)) : (*(ushort*)(pB + 3 * *strideB)) */ );
                        *((ushort*)pOut + 4) = func((*(ushort*)(pA + 4 * *strideA)), (*(ushort*)(pB + 4 * *strideB))  /* ? (*(ushort*)(pA + 4 * *strideA)) : (*(ushort*)(pB + 4 * *strideB)) */ );
                        *((ushort*)pOut + 5) = func((*(ushort*)(pA + 5 * *strideA)), (*(ushort*)(pB + 5 * *strideB))  /* ? (*(ushort*)(pA + 5 * *strideA)) : (*(ushort*)(pB + 5 * *strideB)) */ );
                        *((ushort*)pOut + 6) = func((*(ushort*)(pA + 6 * *strideA)), (*(ushort*)(pB + 6 * *strideB))  /* ? (*(ushort*)(pA + 6 * *strideA)) : (*(ushort*)(pB + 6 * *strideB)) */ );
                        *((ushort*)pOut + 7) = func((*(ushort*)(pA + 7 * *strideA)), (*(ushort*)(pB + 7 * *strideB))  /* ? (*(ushort*)(pA + 7 * *strideA)) : (*(ushort*)(pB + 7 * *strideB)) */ );
                        pOut += 8 * sizeof(ushort); pA += 8 * *strideA; pB += 8 * *strideB; leadLen -= 8;
                    }
                    while (leadLen-- > 0) {
                        *(ushort*)pOut = func((*(ushort*)(pA)), (*(ushort*)(pB))  /* ? (*(ushort*)(pA)) : (*(ushort*)(pB))*/ );
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

            internal override unsafe void Strided64(ushort[] A, ushort[] B, ushort[] C, long offsA, long offsB, long offsC,
                long start, long len, long* dims_strides, Func<ushort, ushort, ushort> func) {
                throw new NotImplementedException();
            }
        }
    }


   
    internal static partial class MathInternal {

        /// <summary>
        /// Binary, elementwise, broadcasting operation: apply(A,B).
        /// </summary>
        /// <param name="A">The one array.</param>
        /// <param name="B">The other array.</param>
        /// <param name="func">The elementary (scalar) function to be used to perform the operation on corresponding elements of <paramref name="A"/> and <paramref name="B"/>.</param>
        /// <returns>Result of operating elements of <paramref name="A"/> and <paramref name="B"/> elementwise.</returns>
        /// <remarks><para>Overloads of <see cref="apply(BaseArray{short}, BaseArray{short}, Func{short, short, short})"/> expect two
        /// arrays of broadcastable size with the same element types. They perform a mapping operation on corresponding elements 
        /// of <paramref name="A"/> and <paramref name="B"/> by evaluating <paramref name="func"/> for each element of the resulting array.</para>
        /// <para>Overloads of <see cref="apply(BaseArray{short}, BaseArray{short}, Func{short, short, short})"/> perform fastest where 
        /// the element type of the input and the output arrays match. Such overloads exist for all numeric value-typed element types.</para>
        /// <para>The function is efficiently parallelized and <paramref name="func"/> is evaluated from multiple threads. Make sure that <paramref name="func"/>
        /// is threadsafe!</para>
        /// <para>For more flexibility a generic overload exist which allows the element types of <paramref name="A"/>, <paramref name="B"/> 
        /// as well as the array returned to be individual different. See: <see cref="apply{Tin1, Tin2, Tout}(BaseArray{Tin1}, BaseArray{Tin2}, Func{Tin1, Tin2, Tout})"/>.</para></remarks>
        /// <seealso cref="apply{Tin1, Tin2, Tout}(BaseArray{Tin1}, BaseArray{Tin2}, Func{Tin1, Tin2, Tout})"/>
        /// <seealso cref="convert{inT, outT}(BaseArray{inT})"/>
        internal static Array<short> apply(BaseArray<short> A, BaseArray<short> B,
            Func<short,short,short> func) {
            return InnerLoops.Apply.Int16.Instance.operate(
                A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>,
                B as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>, 
                func);
        }
    }
    namespace InnerLoops.Apply {

        internal unsafe class Int16
            : BroadcastingBinaryGenericBase<short, Array<short>, InArray<short>, OutArray<short>, Array<short>,
                                    Storage<short>,
                                    Func<short, short, short>> {

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
            /// <param name="func">The scalar function to be used to perform the binary action on individual, 
            /// corresponding elements of <paramref name="pA"/> and <paramref name="pB"/>.</param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            internal unsafe override void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len,
                long* dims_strides, Func<short, short, short> func) {
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
                if (dims_strides[1] == 1) { // updated 5.2: include 1d scalars as special case. Was: ndims == 0 ... 
                    *((short*)pOut + start) = func(*(short*)pA, *(short*)pB  /* ? *(short*)pA : *(short*)pB */ );
                    return;
                }
                uint ndims = (uint)dims_strides[0];
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

                    while (leadLen > 8) {
                        *((short*)pOut + 0) = func((*(short*)(pA + 0 * *strideA)), (*(short*)(pB + 0 * *strideB))  /* ? (*(short*)(pA + 0 * *strideA)) : (*(short*)(pB + 0 * *strideB)) */ );
                        *((short*)pOut + 1) = func((*(short*)(pA + 1 * *strideA)), (*(short*)(pB + 1 * *strideB))  /* ? (*(short*)(pA + 1 * *strideA)) : (*(short*)(pB + 1 * *strideB)) */ );
                        *((short*)pOut + 2) = func((*(short*)(pA + 2 * *strideA)), (*(short*)(pB + 2 * *strideB))  /* ? (*(short*)(pA + 2 * *strideA)) : (*(short*)(pB + 2 * *strideB)) */ );
                        *((short*)pOut + 3) = func((*(short*)(pA + 3 * *strideA)), (*(short*)(pB + 3 * *strideB))  /* ? (*(short*)(pA + 3 * *strideA)) : (*(short*)(pB + 3 * *strideB)) */ );
                        *((short*)pOut + 4) = func((*(short*)(pA + 4 * *strideA)), (*(short*)(pB + 4 * *strideB))  /* ? (*(short*)(pA + 4 * *strideA)) : (*(short*)(pB + 4 * *strideB)) */ );
                        *((short*)pOut + 5) = func((*(short*)(pA + 5 * *strideA)), (*(short*)(pB + 5 * *strideB))  /* ? (*(short*)(pA + 5 * *strideA)) : (*(short*)(pB + 5 * *strideB)) */ );
                        *((short*)pOut + 6) = func((*(short*)(pA + 6 * *strideA)), (*(short*)(pB + 6 * *strideB))  /* ? (*(short*)(pA + 6 * *strideA)) : (*(short*)(pB + 6 * *strideB)) */ );
                        *((short*)pOut + 7) = func((*(short*)(pA + 7 * *strideA)), (*(short*)(pB + 7 * *strideB))  /* ? (*(short*)(pA + 7 * *strideA)) : (*(short*)(pB + 7 * *strideB)) */ );
                        pOut += 8 * sizeof(short); pA += 8 * *strideA; pB += 8 * *strideB; leadLen -= 8;
                    }
                    while (leadLen-- > 0) {
                        *(short*)pOut = func((*(short*)(pA)), (*(short*)(pB))  /* ? (*(short*)(pA)) : (*(short*)(pB))*/ );
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

            internal override unsafe void Strided64(short[] A, short[] B, short[] C, long offsA, long offsB, long offsC,
                long start, long len, long* dims_strides, Func<short, short, short> func) {
                throw new NotImplementedException();
            }
        }
    }


   
    internal static partial class MathInternal {

        /// <summary>
        /// Binary, elementwise, broadcasting operation: apply(A,B).
        /// </summary>
        /// <param name="A">The one array.</param>
        /// <param name="B">The other array.</param>
        /// <param name="func">The elementary (scalar) function to be used to perform the operation on corresponding elements of <paramref name="A"/> and <paramref name="B"/>.</param>
        /// <returns>Result of operating elements of <paramref name="A"/> and <paramref name="B"/> elementwise.</returns>
        /// <remarks><para>Overloads of <see cref="apply(BaseArray{byte}, BaseArray{byte}, Func{byte, byte, byte})"/> expect two
        /// arrays of broadcastable size with the same element types. They perform a mapping operation on corresponding elements 
        /// of <paramref name="A"/> and <paramref name="B"/> by evaluating <paramref name="func"/> for each element of the resulting array.</para>
        /// <para>Overloads of <see cref="apply(BaseArray{byte}, BaseArray{byte}, Func{byte, byte, byte})"/> perform fastest where 
        /// the element type of the input and the output arrays match. Such overloads exist for all numeric value-typed element types.</para>
        /// <para>The function is efficiently parallelized and <paramref name="func"/> is evaluated from multiple threads. Make sure that <paramref name="func"/>
        /// is threadsafe!</para>
        /// <para>For more flexibility a generic overload exist which allows the element types of <paramref name="A"/>, <paramref name="B"/> 
        /// as well as the array returned to be individual different. See: <see cref="apply{Tin1, Tin2, Tout}(BaseArray{Tin1}, BaseArray{Tin2}, Func{Tin1, Tin2, Tout})"/>.</para></remarks>
        /// <seealso cref="apply{Tin1, Tin2, Tout}(BaseArray{Tin1}, BaseArray{Tin2}, Func{Tin1, Tin2, Tout})"/>
        /// <seealso cref="convert{inT, outT}(BaseArray{inT})"/>
        internal static Array<byte> apply(BaseArray<byte> A, BaseArray<byte> B,
            Func<byte,byte,byte> func) {
            return InnerLoops.Apply.Byte.Instance.operate(
                A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>,
                B as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>, 
                func);
        }
    }
    namespace InnerLoops.Apply {

        internal unsafe class Byte
            : BroadcastingBinaryGenericBase<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>,
                                    Storage<byte>,
                                    Func<byte, byte, byte>> {

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
            /// <param name="func">The scalar function to be used to perform the binary action on individual, 
            /// corresponding elements of <paramref name="pA"/> and <paramref name="pB"/>.</param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            internal unsafe override void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len,
                long* dims_strides, Func<byte, byte, byte> func) {
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
                if (dims_strides[1] == 1) { // updated 5.2: include 1d scalars as special case. Was: ndims == 0 ... 
                    *((byte*)pOut + start) = func(*(byte*)pA, *(byte*)pB  /* ? *(byte*)pA : *(byte*)pB */ );
                    return;
                }
                uint ndims = (uint)dims_strides[0];
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

                    while (leadLen > 8) {
                        *((byte*)pOut + 0) = func((*(byte*)(pA + 0 * *strideA)), (*(byte*)(pB + 0 * *strideB))  /* ? (*(byte*)(pA + 0 * *strideA)) : (*(byte*)(pB + 0 * *strideB)) */ );
                        *((byte*)pOut + 1) = func((*(byte*)(pA + 1 * *strideA)), (*(byte*)(pB + 1 * *strideB))  /* ? (*(byte*)(pA + 1 * *strideA)) : (*(byte*)(pB + 1 * *strideB)) */ );
                        *((byte*)pOut + 2) = func((*(byte*)(pA + 2 * *strideA)), (*(byte*)(pB + 2 * *strideB))  /* ? (*(byte*)(pA + 2 * *strideA)) : (*(byte*)(pB + 2 * *strideB)) */ );
                        *((byte*)pOut + 3) = func((*(byte*)(pA + 3 * *strideA)), (*(byte*)(pB + 3 * *strideB))  /* ? (*(byte*)(pA + 3 * *strideA)) : (*(byte*)(pB + 3 * *strideB)) */ );
                        *((byte*)pOut + 4) = func((*(byte*)(pA + 4 * *strideA)), (*(byte*)(pB + 4 * *strideB))  /* ? (*(byte*)(pA + 4 * *strideA)) : (*(byte*)(pB + 4 * *strideB)) */ );
                        *((byte*)pOut + 5) = func((*(byte*)(pA + 5 * *strideA)), (*(byte*)(pB + 5 * *strideB))  /* ? (*(byte*)(pA + 5 * *strideA)) : (*(byte*)(pB + 5 * *strideB)) */ );
                        *((byte*)pOut + 6) = func((*(byte*)(pA + 6 * *strideA)), (*(byte*)(pB + 6 * *strideB))  /* ? (*(byte*)(pA + 6 * *strideA)) : (*(byte*)(pB + 6 * *strideB)) */ );
                        *((byte*)pOut + 7) = func((*(byte*)(pA + 7 * *strideA)), (*(byte*)(pB + 7 * *strideB))  /* ? (*(byte*)(pA + 7 * *strideA)) : (*(byte*)(pB + 7 * *strideB)) */ );
                        pOut += 8 * sizeof(byte); pA += 8 * *strideA; pB += 8 * *strideB; leadLen -= 8;
                    }
                    while (leadLen-- > 0) {
                        *(byte*)pOut = func((*(byte*)(pA)), (*(byte*)(pB))  /* ? (*(byte*)(pA)) : (*(byte*)(pB))*/ );
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

            internal override unsafe void Strided64(byte[] A, byte[] B, byte[] C, long offsA, long offsB, long offsC,
                long start, long len, long* dims_strides, Func<byte, byte, byte> func) {
                throw new NotImplementedException();
            }
        }
    }


   
    internal static partial class MathInternal {

        /// <summary>
        /// Binary, elementwise, broadcasting operation: apply(A,B).
        /// </summary>
        /// <param name="A">The one array.</param>
        /// <param name="B">The other array.</param>
        /// <param name="func">The elementary (scalar) function to be used to perform the operation on corresponding elements of <paramref name="A"/> and <paramref name="B"/>.</param>
        /// <returns>Result of operating elements of <paramref name="A"/> and <paramref name="B"/> elementwise.</returns>
        /// <remarks><para>Overloads of <see cref="apply(BaseArray{sbyte}, BaseArray{sbyte}, Func{sbyte, sbyte, sbyte})"/> expect two
        /// arrays of broadcastable size with the same element types. They perform a mapping operation on corresponding elements 
        /// of <paramref name="A"/> and <paramref name="B"/> by evaluating <paramref name="func"/> for each element of the resulting array.</para>
        /// <para>Overloads of <see cref="apply(BaseArray{sbyte}, BaseArray{sbyte}, Func{sbyte, sbyte, sbyte})"/> perform fastest where 
        /// the element type of the input and the output arrays match. Such overloads exist for all numeric value-typed element types.</para>
        /// <para>The function is efficiently parallelized and <paramref name="func"/> is evaluated from multiple threads. Make sure that <paramref name="func"/>
        /// is threadsafe!</para>
        /// <para>For more flexibility a generic overload exist which allows the element types of <paramref name="A"/>, <paramref name="B"/> 
        /// as well as the array returned to be individual different. See: <see cref="apply{Tin1, Tin2, Tout}(BaseArray{Tin1}, BaseArray{Tin2}, Func{Tin1, Tin2, Tout})"/>.</para></remarks>
        /// <seealso cref="apply{Tin1, Tin2, Tout}(BaseArray{Tin1}, BaseArray{Tin2}, Func{Tin1, Tin2, Tout})"/>
        /// <seealso cref="convert{inT, outT}(BaseArray{inT})"/>
        internal static Array<sbyte> apply(BaseArray<sbyte> A, BaseArray<sbyte> B,
            Func<sbyte,sbyte,sbyte> func) {
            return InnerLoops.Apply.SByte.Instance.operate(
                A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>,
                B as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>, 
                func);
        }
    }
    namespace InnerLoops.Apply {

        internal unsafe class SByte
            : BroadcastingBinaryGenericBase<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>,
                                    Storage<sbyte>,
                                    Func<sbyte, sbyte, sbyte>> {

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
            /// <param name="func">The scalar function to be used to perform the binary action on individual, 
            /// corresponding elements of <paramref name="pA"/> and <paramref name="pB"/>.</param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            internal unsafe override void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len,
                long* dims_strides, Func<sbyte, sbyte, sbyte> func) {
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
                if (dims_strides[1] == 1) { // updated 5.2: include 1d scalars as special case. Was: ndims == 0 ... 
                    *((sbyte*)pOut + start) = func(*(sbyte*)pA, *(sbyte*)pB  /* ? *(sbyte*)pA : *(sbyte*)pB */ );
                    return;
                }
                uint ndims = (uint)dims_strides[0];
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

                    while (leadLen > 8) {
                        *((sbyte*)pOut + 0) = func((*(sbyte*)(pA + 0 * *strideA)), (*(sbyte*)(pB + 0 * *strideB))  /* ? (*(sbyte*)(pA + 0 * *strideA)) : (*(sbyte*)(pB + 0 * *strideB)) */ );
                        *((sbyte*)pOut + 1) = func((*(sbyte*)(pA + 1 * *strideA)), (*(sbyte*)(pB + 1 * *strideB))  /* ? (*(sbyte*)(pA + 1 * *strideA)) : (*(sbyte*)(pB + 1 * *strideB)) */ );
                        *((sbyte*)pOut + 2) = func((*(sbyte*)(pA + 2 * *strideA)), (*(sbyte*)(pB + 2 * *strideB))  /* ? (*(sbyte*)(pA + 2 * *strideA)) : (*(sbyte*)(pB + 2 * *strideB)) */ );
                        *((sbyte*)pOut + 3) = func((*(sbyte*)(pA + 3 * *strideA)), (*(sbyte*)(pB + 3 * *strideB))  /* ? (*(sbyte*)(pA + 3 * *strideA)) : (*(sbyte*)(pB + 3 * *strideB)) */ );
                        *((sbyte*)pOut + 4) = func((*(sbyte*)(pA + 4 * *strideA)), (*(sbyte*)(pB + 4 * *strideB))  /* ? (*(sbyte*)(pA + 4 * *strideA)) : (*(sbyte*)(pB + 4 * *strideB)) */ );
                        *((sbyte*)pOut + 5) = func((*(sbyte*)(pA + 5 * *strideA)), (*(sbyte*)(pB + 5 * *strideB))  /* ? (*(sbyte*)(pA + 5 * *strideA)) : (*(sbyte*)(pB + 5 * *strideB)) */ );
                        *((sbyte*)pOut + 6) = func((*(sbyte*)(pA + 6 * *strideA)), (*(sbyte*)(pB + 6 * *strideB))  /* ? (*(sbyte*)(pA + 6 * *strideA)) : (*(sbyte*)(pB + 6 * *strideB)) */ );
                        *((sbyte*)pOut + 7) = func((*(sbyte*)(pA + 7 * *strideA)), (*(sbyte*)(pB + 7 * *strideB))  /* ? (*(sbyte*)(pA + 7 * *strideA)) : (*(sbyte*)(pB + 7 * *strideB)) */ );
                        pOut += 8 * sizeof(sbyte); pA += 8 * *strideA; pB += 8 * *strideB; leadLen -= 8;
                    }
                    while (leadLen-- > 0) {
                        *(sbyte*)pOut = func((*(sbyte*)(pA)), (*(sbyte*)(pB))  /* ? (*(sbyte*)(pA)) : (*(sbyte*)(pB))*/ );
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

            internal override unsafe void Strided64(sbyte[] A, sbyte[] B, sbyte[] C, long offsA, long offsB, long offsC,
                long start, long len, long* dims_strides, Func<sbyte, sbyte, sbyte> func) {
                throw new NotImplementedException();
            }
        }
    }


   
    internal static partial class MathInternal {

        /// <summary>
        /// Binary, elementwise, broadcasting operation: apply(A,B).
        /// </summary>
        /// <param name="A">The one array.</param>
        /// <param name="B">The other array.</param>
        /// <param name="func">The elementary (scalar) function to be used to perform the operation on corresponding elements of <paramref name="A"/> and <paramref name="B"/>.</param>
        /// <returns>Result of operating elements of <paramref name="A"/> and <paramref name="B"/> elementwise.</returns>
        /// <remarks><para>Overloads of <see cref="apply(BaseArray{fcomplex}, BaseArray{fcomplex}, Func{fcomplex, fcomplex, fcomplex})"/> expect two
        /// arrays of broadcastable size with the same element types. They perform a mapping operation on corresponding elements 
        /// of <paramref name="A"/> and <paramref name="B"/> by evaluating <paramref name="func"/> for each element of the resulting array.</para>
        /// <para>Overloads of <see cref="apply(BaseArray{fcomplex}, BaseArray{fcomplex}, Func{fcomplex, fcomplex, fcomplex})"/> perform fastest where 
        /// the element type of the input and the output arrays match. Such overloads exist for all numeric value-typed element types.</para>
        /// <para>The function is efficiently parallelized and <paramref name="func"/> is evaluated from multiple threads. Make sure that <paramref name="func"/>
        /// is threadsafe!</para>
        /// <para>For more flexibility a generic overload exist which allows the element types of <paramref name="A"/>, <paramref name="B"/> 
        /// as well as the array returned to be individual different. See: <see cref="apply{Tin1, Tin2, Tout}(BaseArray{Tin1}, BaseArray{Tin2}, Func{Tin1, Tin2, Tout})"/>.</para></remarks>
        /// <seealso cref="apply{Tin1, Tin2, Tout}(BaseArray{Tin1}, BaseArray{Tin2}, Func{Tin1, Tin2, Tout})"/>
        /// <seealso cref="convert{inT, outT}(BaseArray{inT})"/>
        internal static Array<fcomplex> apply(BaseArray<fcomplex> A, BaseArray<fcomplex> B,
            Func<fcomplex,fcomplex,fcomplex> func) {
            return InnerLoops.Apply.FComplex.Instance.operate(
                A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>,
                B as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, 
                func);
        }
    }
    namespace InnerLoops.Apply {

        internal unsafe class FComplex
            : BroadcastingBinaryGenericBase<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>,
                                    Storage<fcomplex>,
                                    Func<fcomplex, fcomplex, fcomplex>> {

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
            /// <param name="func">The scalar function to be used to perform the binary action on individual, 
            /// corresponding elements of <paramref name="pA"/> and <paramref name="pB"/>.</param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            internal unsafe override void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len,
                long* dims_strides, Func<fcomplex, fcomplex, fcomplex> func) {
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
                if (dims_strides[1] == 1) { // updated 5.2: include 1d scalars as special case. Was: ndims == 0 ... 
                    *((fcomplex*)pOut + start) = func(*(fcomplex*)pA, *(fcomplex*)pB  /* ? *(fcomplex*)pA : *(fcomplex*)pB */ );
                    return;
                }
                uint ndims = (uint)dims_strides[0];
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
                        *((fcomplex*)pOut + 0) = func((*(fcomplex*)(pA + 0 * *strideA)), (*(fcomplex*)(pB + 0 * *strideB))  /* ? (*(fcomplex*)(pA + 0 * *strideA)) : (*(fcomplex*)(pB + 0 * *strideB)) */ );
                        *((fcomplex*)pOut + 1) = func((*(fcomplex*)(pA + 1 * *strideA)), (*(fcomplex*)(pB + 1 * *strideB))  /* ? (*(fcomplex*)(pA + 1 * *strideA)) : (*(fcomplex*)(pB + 1 * *strideB)) */ );
                        *((fcomplex*)pOut + 2) = func((*(fcomplex*)(pA + 2 * *strideA)), (*(fcomplex*)(pB + 2 * *strideB))  /* ? (*(fcomplex*)(pA + 2 * *strideA)) : (*(fcomplex*)(pB + 2 * *strideB)) */ );
                        *((fcomplex*)pOut + 3) = func((*(fcomplex*)(pA + 3 * *strideA)), (*(fcomplex*)(pB + 3 * *strideB))  /* ? (*(fcomplex*)(pA + 3 * *strideA)) : (*(fcomplex*)(pB + 3 * *strideB)) */ );
                        *((fcomplex*)pOut + 4) = func((*(fcomplex*)(pA + 4 * *strideA)), (*(fcomplex*)(pB + 4 * *strideB))  /* ? (*(fcomplex*)(pA + 4 * *strideA)) : (*(fcomplex*)(pB + 4 * *strideB)) */ );
                        *((fcomplex*)pOut + 5) = func((*(fcomplex*)(pA + 5 * *strideA)), (*(fcomplex*)(pB + 5 * *strideB))  /* ? (*(fcomplex*)(pA + 5 * *strideA)) : (*(fcomplex*)(pB + 5 * *strideB)) */ );
                        *((fcomplex*)pOut + 6) = func((*(fcomplex*)(pA + 6 * *strideA)), (*(fcomplex*)(pB + 6 * *strideB))  /* ? (*(fcomplex*)(pA + 6 * *strideA)) : (*(fcomplex*)(pB + 6 * *strideB)) */ );
                        *((fcomplex*)pOut + 7) = func((*(fcomplex*)(pA + 7 * *strideA)), (*(fcomplex*)(pB + 7 * *strideB))  /* ? (*(fcomplex*)(pA + 7 * *strideA)) : (*(fcomplex*)(pB + 7 * *strideB)) */ );
                        pOut += 8 * sizeof(fcomplex); pA += 8 * *strideA; pB += 8 * *strideB; leadLen -= 8;
                    }
                    while (leadLen-- > 0) {
                        *(fcomplex*)pOut = func((*(fcomplex*)(pA)), (*(fcomplex*)(pB))  /* ? (*(fcomplex*)(pA)) : (*(fcomplex*)(pB))*/ );
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

            internal override unsafe void Strided64(fcomplex[] A, fcomplex[] B, fcomplex[] C, long offsA, long offsB, long offsC,
                long start, long len, long* dims_strides, Func<fcomplex, fcomplex, fcomplex> func) {
                throw new NotImplementedException();
            }
        }
    }


   
    internal static partial class MathInternal {

        /// <summary>
        /// Binary, elementwise, broadcasting operation: apply(A,B).
        /// </summary>
        /// <param name="A">The one array.</param>
        /// <param name="B">The other array.</param>
        /// <param name="func">The elementary (scalar) function to be used to perform the operation on corresponding elements of <paramref name="A"/> and <paramref name="B"/>.</param>
        /// <returns>Result of operating elements of <paramref name="A"/> and <paramref name="B"/> elementwise.</returns>
        /// <remarks><para>Overloads of <see cref="apply(BaseArray{complex}, BaseArray{complex}, Func{complex, complex, complex})"/> expect two
        /// arrays of broadcastable size with the same element types. They perform a mapping operation on corresponding elements 
        /// of <paramref name="A"/> and <paramref name="B"/> by evaluating <paramref name="func"/> for each element of the resulting array.</para>
        /// <para>Overloads of <see cref="apply(BaseArray{complex}, BaseArray{complex}, Func{complex, complex, complex})"/> perform fastest where 
        /// the element type of the input and the output arrays match. Such overloads exist for all numeric value-typed element types.</para>
        /// <para>The function is efficiently parallelized and <paramref name="func"/> is evaluated from multiple threads. Make sure that <paramref name="func"/>
        /// is threadsafe!</para>
        /// <para>For more flexibility a generic overload exist which allows the element types of <paramref name="A"/>, <paramref name="B"/> 
        /// as well as the array returned to be individual different. See: <see cref="apply{Tin1, Tin2, Tout}(BaseArray{Tin1}, BaseArray{Tin2}, Func{Tin1, Tin2, Tout})"/>.</para></remarks>
        /// <seealso cref="apply{Tin1, Tin2, Tout}(BaseArray{Tin1}, BaseArray{Tin2}, Func{Tin1, Tin2, Tout})"/>
        /// <seealso cref="convert{inT, outT}(BaseArray{inT})"/>
        internal static Array<complex> apply(BaseArray<complex> A, BaseArray<complex> B,
            Func<complex,complex,complex> func) {
            return InnerLoops.Apply.Complex.Instance.operate(
                A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>,
                B as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, 
                func);
        }
    }
    namespace InnerLoops.Apply {

        internal unsafe class Complex
            : BroadcastingBinaryGenericBase<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>,
                                    Storage<complex>,
                                    Func<complex, complex, complex>> {

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
            /// <param name="func">The scalar function to be used to perform the binary action on individual, 
            /// corresponding elements of <paramref name="pA"/> and <paramref name="pB"/>.</param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            internal unsafe override void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len,
                long* dims_strides, Func<complex, complex, complex> func) {
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
                if (dims_strides[1] == 1) { // updated 5.2: include 1d scalars as special case. Was: ndims == 0 ... 
                    *((complex*)pOut + start) = func(*(complex*)pA, *(complex*)pB  /* ? *(complex*)pA : *(complex*)pB */ );
                    return;
                }
                uint ndims = (uint)dims_strides[0];
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
                        *((complex*)pOut + 0) = func((*(complex*)(pA + 0 * *strideA)), (*(complex*)(pB + 0 * *strideB))  /* ? (*(complex*)(pA + 0 * *strideA)) : (*(complex*)(pB + 0 * *strideB)) */ );
                        *((complex*)pOut + 1) = func((*(complex*)(pA + 1 * *strideA)), (*(complex*)(pB + 1 * *strideB))  /* ? (*(complex*)(pA + 1 * *strideA)) : (*(complex*)(pB + 1 * *strideB)) */ );
                        *((complex*)pOut + 2) = func((*(complex*)(pA + 2 * *strideA)), (*(complex*)(pB + 2 * *strideB))  /* ? (*(complex*)(pA + 2 * *strideA)) : (*(complex*)(pB + 2 * *strideB)) */ );
                        *((complex*)pOut + 3) = func((*(complex*)(pA + 3 * *strideA)), (*(complex*)(pB + 3 * *strideB))  /* ? (*(complex*)(pA + 3 * *strideA)) : (*(complex*)(pB + 3 * *strideB)) */ );
                        *((complex*)pOut + 4) = func((*(complex*)(pA + 4 * *strideA)), (*(complex*)(pB + 4 * *strideB))  /* ? (*(complex*)(pA + 4 * *strideA)) : (*(complex*)(pB + 4 * *strideB)) */ );
                        *((complex*)pOut + 5) = func((*(complex*)(pA + 5 * *strideA)), (*(complex*)(pB + 5 * *strideB))  /* ? (*(complex*)(pA + 5 * *strideA)) : (*(complex*)(pB + 5 * *strideB)) */ );
                        *((complex*)pOut + 6) = func((*(complex*)(pA + 6 * *strideA)), (*(complex*)(pB + 6 * *strideB))  /* ? (*(complex*)(pA + 6 * *strideA)) : (*(complex*)(pB + 6 * *strideB)) */ );
                        *((complex*)pOut + 7) = func((*(complex*)(pA + 7 * *strideA)), (*(complex*)(pB + 7 * *strideB))  /* ? (*(complex*)(pA + 7 * *strideA)) : (*(complex*)(pB + 7 * *strideB)) */ );
                        pOut += 8 * sizeof(complex); pA += 8 * *strideA; pB += 8 * *strideB; leadLen -= 8;
                    }
                    while (leadLen-- > 0) {
                        *(complex*)pOut = func((*(complex*)(pA)), (*(complex*)(pB))  /* ? (*(complex*)(pA)) : (*(complex*)(pB))*/ );
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

            internal override unsafe void Strided64(complex[] A, complex[] B, complex[] C, long offsA, long offsB, long offsC,
                long start, long len, long* dims_strides, Func<complex, complex, complex> func) {
                throw new NotImplementedException();
            }
        }
    }


   
    internal static partial class MathInternal {

        /// <summary>
        /// Binary, elementwise, broadcasting operation: apply(A,B).
        /// </summary>
        /// <param name="A">The one array.</param>
        /// <param name="B">The other array.</param>
        /// <param name="func">The elementary (scalar) function to be used to perform the operation on corresponding elements of <paramref name="A"/> and <paramref name="B"/>.</param>
        /// <returns>Result of operating elements of <paramref name="A"/> and <paramref name="B"/> elementwise.</returns>
        /// <remarks><para>Overloads of <see cref="apply(BaseArray{float}, BaseArray{float}, Func{float, float, float})"/> expect two
        /// arrays of broadcastable size with the same element types. They perform a mapping operation on corresponding elements 
        /// of <paramref name="A"/> and <paramref name="B"/> by evaluating <paramref name="func"/> for each element of the resulting array.</para>
        /// <para>Overloads of <see cref="apply(BaseArray{float}, BaseArray{float}, Func{float, float, float})"/> perform fastest where 
        /// the element type of the input and the output arrays match. Such overloads exist for all numeric value-typed element types.</para>
        /// <para>The function is efficiently parallelized and <paramref name="func"/> is evaluated from multiple threads. Make sure that <paramref name="func"/>
        /// is threadsafe!</para>
        /// <para>For more flexibility a generic overload exist which allows the element types of <paramref name="A"/>, <paramref name="B"/> 
        /// as well as the array returned to be individual different. See: <see cref="apply{Tin1, Tin2, Tout}(BaseArray{Tin1}, BaseArray{Tin2}, Func{Tin1, Tin2, Tout})"/>.</para></remarks>
        /// <seealso cref="apply{Tin1, Tin2, Tout}(BaseArray{Tin1}, BaseArray{Tin2}, Func{Tin1, Tin2, Tout})"/>
        /// <seealso cref="convert{inT, outT}(BaseArray{inT})"/>
        internal static Array<float> apply(BaseArray<float> A, BaseArray<float> B,
            Func<float,float,float> func) {
            return InnerLoops.Apply.Single.Instance.operate(
                A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>,
                B as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, 
                func);
        }
    }
    namespace InnerLoops.Apply {

        internal unsafe class Single
            : BroadcastingBinaryGenericBase<float, Array<float>, InArray<float>, OutArray<float>, Array<float>,
                                    Storage<float>,
                                    Func<float, float, float>> {

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
            /// <param name="func">The scalar function to be used to perform the binary action on individual, 
            /// corresponding elements of <paramref name="pA"/> and <paramref name="pB"/>.</param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            internal unsafe override void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len,
                long* dims_strides, Func<float, float, float> func) {
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
                if (dims_strides[1] == 1) { // updated 5.2: include 1d scalars as special case. Was: ndims == 0 ... 
                    *((float*)pOut + start) = func(*(float*)pA, *(float*)pB  /* ? *(float*)pA : *(float*)pB */ );
                    return;
                }
                uint ndims = (uint)dims_strides[0];
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
                        *((float*)pOut + 0) = func((*(float*)(pA + 0 * *strideA)), (*(float*)(pB + 0 * *strideB))  /* ? (*(float*)(pA + 0 * *strideA)) : (*(float*)(pB + 0 * *strideB)) */ );
                        *((float*)pOut + 1) = func((*(float*)(pA + 1 * *strideA)), (*(float*)(pB + 1 * *strideB))  /* ? (*(float*)(pA + 1 * *strideA)) : (*(float*)(pB + 1 * *strideB)) */ );
                        *((float*)pOut + 2) = func((*(float*)(pA + 2 * *strideA)), (*(float*)(pB + 2 * *strideB))  /* ? (*(float*)(pA + 2 * *strideA)) : (*(float*)(pB + 2 * *strideB)) */ );
                        *((float*)pOut + 3) = func((*(float*)(pA + 3 * *strideA)), (*(float*)(pB + 3 * *strideB))  /* ? (*(float*)(pA + 3 * *strideA)) : (*(float*)(pB + 3 * *strideB)) */ );
                        *((float*)pOut + 4) = func((*(float*)(pA + 4 * *strideA)), (*(float*)(pB + 4 * *strideB))  /* ? (*(float*)(pA + 4 * *strideA)) : (*(float*)(pB + 4 * *strideB)) */ );
                        *((float*)pOut + 5) = func((*(float*)(pA + 5 * *strideA)), (*(float*)(pB + 5 * *strideB))  /* ? (*(float*)(pA + 5 * *strideA)) : (*(float*)(pB + 5 * *strideB)) */ );
                        *((float*)pOut + 6) = func((*(float*)(pA + 6 * *strideA)), (*(float*)(pB + 6 * *strideB))  /* ? (*(float*)(pA + 6 * *strideA)) : (*(float*)(pB + 6 * *strideB)) */ );
                        *((float*)pOut + 7) = func((*(float*)(pA + 7 * *strideA)), (*(float*)(pB + 7 * *strideB))  /* ? (*(float*)(pA + 7 * *strideA)) : (*(float*)(pB + 7 * *strideB)) */ );
                        pOut += 8 * sizeof(float); pA += 8 * *strideA; pB += 8 * *strideB; leadLen -= 8;
                    }
                    while (leadLen-- > 0) {
                        *(float*)pOut = func((*(float*)(pA)), (*(float*)(pB))  /* ? (*(float*)(pA)) : (*(float*)(pB))*/ );
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

            internal override unsafe void Strided64(float[] A, float[] B, float[] C, long offsA, long offsB, long offsC,
                long start, long len, long* dims_strides, Func<float, float, float> func) {
                throw new NotImplementedException();
            }
        }
    }


#endregion HYCALPER AUTO GENERATED CODE


    #region Generic Apply<T>(A,B)
    internal static partial class MathInternal {

        /// <summary>
        /// Binary, elementwise, broadcasting operation: <![CDATA[Apply<T>]]>.
        /// </summary>
        /// <param name="A">The one array.</param>
        /// <param name="B">The other array.</param>
        /// <param name="func">The elementary (scalar) function to be used to perform the operation on corresponding elements of <paramref name="A"/> and <paramref name="B"/>.</param>
        /// <returns>Result of operating elements of <paramref name="A"/> and <paramref name="B"/> elementwise.</returns>
        internal static Array<T> apply<T>(BaseArray<T> A, BaseArray<T> B,
            Func<T, T, T> func) where T : class {
            return InnerLoops.Apply.Generic<T>.Instance.operate(
                A as ConcreteArray<T, Array<T>, InArray<T>, OutArray<T>, Array<T>, Storage<T>>,
                B as ConcreteArray<T, Array<T>, InArray<T>, OutArray<T>, Array<T>, Storage<T>>,
                func);
        }
    }
    namespace InnerLoops.Apply {

        internal unsafe class Generic<T>
            : BroadcastingBinaryGenericBase<T, Array<T>, InArray<T>, OutArray<T>, Array<T>, Storage<T>,
                Func<T, T, T>>
            where T : class {

            internal static Generic<T> Instance = new Generic<T>();

            /// <summary>
            /// Performs the inner loop for this binary operation.
            /// </summary>
            /// <param name="pOut">Pointer to base address of the output array, including any base offset.</param>
            /// <param name="pA">Pointer to the base address of input array A, including any base offset.</param>
            /// <param name="pB">Pointer to the base address of input array B, including any base offset.</param>
            /// <param name="start">Element index to start operation in this (thread) chunk.</param>
            /// <param name="len">Number of elements to process.</param>
            /// <param name="dims_strides"></param>
            /// <param name="func">The scalar function to be used to perform the binary action on individual, 
            /// corresponding elements of <paramref name="pA"/> and <paramref name="pB"/>.</param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            internal unsafe override void Strided64(byte* pOut, byte* pA, byte* pB, long start, long len,
                long* dims_strides, Func<T, T, T> func) {
                throw new NotImplementedException();
            }

            internal override unsafe void Strided64(T[] C, T[] A, T[] B, long offsA, long offsB, long offsC, long start,
                long len, long* dims_strides, Func<T, T, T> func) {

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
                    C[start + offsC] = func(A[offsA], B[offsB]);
                    return;
                }
                long* dims = dims_strides + 3;
                long* cur = stackalloc long[(int)ndims];
                long* strideOut = dims_strides + 3 + ndims;
                long* strideA = strideOut + ndims;
                long* strideB = strideA + ndims;

                // figure out the dimension index position for start
                cur[0] = start % (dims[0] + 1);
                offsC += cur[0] * strideOut[0];
                offsA += cur[0] * strideA[0];
                offsB += cur[0] * strideB[0];
                long f = start / (dims[0] + 1);
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % (dims[i] + 1);
                    f /= (dims[i] + 1);
                    offsC += cur[i] * strideOut[i];
                    offsA += cur[i] * strideA[i];
                    offsB += cur[i] * strideB[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);
                System.Diagnostics.Debug.Assert(strideOut[0] == 1, "strides for pOut are expected as column major strides!"); // 
                #endregion

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;

                    while (leadLen > 8) {
                        C[offsC + 0] = func(A[offsA + 0 * *strideA], B[offsB + 0 * *strideB]);
                        C[offsC + 1] = func(A[offsA + 1 * *strideA], B[offsB + 1 * *strideB]);
                        C[offsC + 2] = func(A[offsA + 2 * *strideA], B[offsB + 2 * *strideB]);
                        C[offsC + 3] = func(A[offsA + 3 * *strideA], B[offsB + 3 * *strideB]);
                        C[offsC + 4] = func(A[offsA + 4 * *strideA], B[offsB + 4 * *strideB]);
                        C[offsC + 5] = func(A[offsA + 5 * *strideA], B[offsB + 5 * *strideB]);
                        C[offsC + 6] = func(A[offsA + 6 * *strideA], B[offsB + 6 * *strideB]);
                        C[offsC + 7] = func(A[offsA + 7 * *strideA], B[offsB + 7 * *strideB]);
                        offsC += 8; offsA += 8 * *strideA; offsB += 8 * *strideB; leadLen -= 8;
                    }
                    while (leadLen-- > 0) {
                        C[offsC] = func(A[offsA], B[offsB]);
                        offsC += 1;
                        offsA += *strideA;
                        offsB += *strideB;
                    }
                    if (len == 0) {
                        break;
                    }
                    // reset initial offset in lead dimension after first iteration
                    cur[0] = 0;
                    offsA -= strideA[0] * (dims[0] + 1);
                    offsB -= strideB[0] * (dims[0] + 1);

                    // increase higher dims
                    int d = 1;
                    while (d < ndims) {
                        // dims are minus 1
                        if (cur[d] < dims[d]) {
                            offsA += strideA[d];
                            offsB += strideB[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            offsA -= strideA[d] * dims[d];
                            offsB -= strideB[d] * dims[d];
                            d++;
                        }
                    }
                }
            }
        }
    }

    #endregion

    #region Generic Apply<Tin1,Tin2,Tout>(A,B)

    internal static partial class MathInternal {

        /// <summary>
        /// Binary, elementwise, broadcasting <![CDATA[apply<T1,T2,Tout>]]>(A,B). Maps a scalar operation from elements of <paramref name="A"/> and <paramref name="B"/> to a third array. Arbitrary types. Parallelized.
        /// </summary>
        /// <param name="A">The one array.</param>
        /// <param name="B">The other array.</param>
        /// <param name="func">The elementary (scalar) function to be used to perform the operation on corresponding elements of <paramref name="A"/> and <paramref name="B"/>.</param>
        /// <returns>Result of operating elements of <paramref name="A"/> and <paramref name="B"/> elementwise.</returns>
        /// <seealso cref="apply{Tin1, Tin2, Tout}(BaseArray{Tin1}, BaseArray{Tin2}, Func{Tin1, Tin2, long, Tout})"/>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Array<Tout> apply<Tin1, Tin2, Tout>(BaseArray<Tin1> A, BaseArray<Tin2> B,
            Func<Tin1, Tin2, Tout> func) {
            var Aarr = A as ConcreteArray<Tin1, Array<Tin1>, InArray<Tin1>, OutArray<Tin1>, Array<Tin1>, Storage<Tin1>>;
            var Barr = B as ConcreteArray<Tin2, Array<Tin2>, InArray<Tin2>, OutArray<Tin2>, Array<Tin2>, Storage<Tin2>>; 
            if (default(Tout) is ValueType) {
                if (default(Tin1) is ValueType) {
                    if (default(Tin2) is ValueType) {
                        return InnerLoops.Apply.Generic_S_SS<Tin1, Tin2, Tout>.Instance.operate(Aarr, Barr, func);
                    } else {
                        return InnerLoops.Apply.Generic_S_SC<Tin1, Tin2, Tout>.Instance.operate(Aarr, Barr, func);
                    }
                } else {
                    if (default(Tin2) is ValueType) {
                        return InnerLoops.Apply.Generic_S_CS<Tin1, Tin2, Tout>.Instance.operate(Aarr, Barr, func);
                    } else {
                        return InnerLoops.Apply.Generic_S_CC<Tin1, Tin2, Tout>.Instance.operate(Aarr, Barr, func);
                    }
                }
            } else {
                if (default(Tin1) is ValueType) {
                    if (default(Tin2) is ValueType) {
                        return InnerLoops.Apply.Generic_C_SS<Tin1, Tin2, Tout>.Instance.operate(Aarr, Barr, func);
                    } else {
                        return InnerLoops.Apply.Generic_C_SC<Tin1, Tin2, Tout>.Instance.operate(Aarr, Barr, func);
                    }
                } else {
                    if (default(Tin2) is ValueType) {
                        return InnerLoops.Apply.Generic_C_CS<Tin1, Tin2, Tout>.Instance.operate(Aarr, Barr, func);
                    } else {
                        return InnerLoops.Apply.Generic_C_CC<Tin1, Tin2, Tout>.Instance.operate(Aarr, Barr, func);
                    }
                }
            }
        }


        /// <summary>
        /// Binary, elementwise, broadcasting <![CDATA[apply<T1,T2,long,Tout>]]>(A,B). Maps a scalar operation from elements of 
        /// <paramref name="A"/> and <paramref name="B"/> to a third array, provides element index. Arbitrary types. Parallelized.
        /// </summary>
        /// <param name="A">The one array.</param>
        /// <param name="B">The other array.</param>
        /// <param name="func">The elementary (scalar) function to be used to perform the operation on corresponding 
        /// elements of <paramref name="A"/> and <paramref name="B"/>, utilizing respective element values and the index 
        /// used to locate the output array in iteration order.</param>
        /// <returns>New or reused array with result of operating elements of <paramref name="A"/> and <paramref name="B"/> elementwise.</returns>
        /// <remarks><para>The iteration order depends on the order of input arrays: for column major 
        /// input <c>i</c> will run in column major order and in row major order for row major inputs. If inputs have 
        /// other, undefined or different storage orders the result is undefined. It is recommended to use this function on vector input(s) 
        /// or with a well defined storage order for inputs <paramref name="A"/> and/or <paramref name="B"/>.</para>
        /// <para>This function may automatically work inplace on either of both input parameters, if possible.</para>
        /// <para></para>
        /// </remarks>
        /// <example><para>'Merge' elements a of an vector of 0's with a scalar -1, weighting with the location index <c>i</c> by applying the function: <c>c = a + i * b</c>.</para>
        /// <code><![CDATA[Array<int> C = apply(zeros<int>(10,1), vector(-1), (a, b, i) => (int)(a + i * b));
        /// C
        ///<Int32> [10,1] 0...-9 |
        ///    [0]:            0
        ///    [1]:           -1
        ///    [2]:           -2
        ///    [3]:           -3
        ///    [4]:           -4
        ///    [5]:           -5
        ///    [6]:           -6
        ///    [7]:           -7
        ///    [8]:           -8
        ///    [9]:           -9
        /// ]]></code>
        /// <para>Efficiently overwrite/initialize the content of an uninitialized array with upwards counting values, row major storage order.</para>
        /// <code><![CDATA[Array<double> D = apply(empty<double>(4, 3, 2, StorageOrders.RowMajor), zeros(1), (a, b, i) => (double)i);
        /// D
        ///<Double> [4,3,2] 0...23 -
        ///    [0]: (:,:,0)
        ///    [1]:            0           2           4
        ///    [2]:            6           8          10
        ///    [3]:           12          14          16
        ///    [4]:           18          20          22
        ///    [5]: (:,:,1)
        ///    [6]:            1           3           5
        ///    [7]:            7           9          11
        ///    [8]:           13          15          17
        ///    [9]:           19          21          23
        ///]]></code>
        ///<para>The last example produces the same result as the expression: <c>counter(4,3,2, StorageOrders.RowMajor);</c>. The larger flexibility 
        ///in the generator function <paramref name="func"/> comes with a slight computational overhead, though.</para>
        /// </example>
        /// <seealso cref="apply{T}(BaseArray{T}, BaseArray{T}, Func{T, T, T})"/>
        /// <seealso cref="apply{Tin1, Tin2, Tout}(BaseArray{Tin1}, BaseArray{Tin2}, Func{Tin1, Tin2, Tout})"/>
        /// <seealso cref="zeros(InArray{long})"/>
        /// <seealso cref="empty{T}(InArray{long}, StorageOrders)"/>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Array<Tout> apply<Tin1, Tin2, Tout>(BaseArray<Tin1> A, BaseArray<Tin2> B,
            Func<Tin1, Tin2, long, Tout> func) {
            var Aarr = A as ConcreteArray<Tin1, Array<Tin1>, InArray<Tin1>, OutArray<Tin1>, Array<Tin1>, Storage<Tin1>>;
            var Barr = B as ConcreteArray<Tin2, Array<Tin2>, InArray<Tin2>, OutArray<Tin2>, Array<Tin2>, Storage<Tin2>>;
            if (default(Tout) is ValueType) {
                if (default(Tin1) is ValueType) {
                    if (default(Tin2) is ValueType) {
                        return InnerLoops.ApplyIndexed.Generic_S_SS<Tin1, Tin2, Tout>.Instance.operate(Aarr, Barr, func);
                    } else {
                        return InnerLoops.ApplyIndexed.Generic_S_SC<Tin1, Tin2, Tout>.Instance.operate(Aarr, Barr, func);
                    }
                } else {
                    if (default(Tin2) is ValueType) {
                        return InnerLoops.ApplyIndexed.Generic_S_CS<Tin1, Tin2, Tout>.Instance.operate(Aarr, Barr, func);
                    } else {
                        return InnerLoops.ApplyIndexed.Generic_S_CC<Tin1, Tin2, Tout>.Instance.operate(Aarr, Barr, func);
                    }
                }
            } else {
                if (default(Tin1) is ValueType) {
                    if (default(Tin2) is ValueType) {
                        return InnerLoops.ApplyIndexed.Generic_C_SS<Tin1, Tin2, Tout>.Instance.operate(Aarr, Barr, func);
                    } else {
                        return InnerLoops.ApplyIndexed.Generic_C_SC<Tin1, Tin2, Tout>.Instance.operate(Aarr, Barr, func);
                    }
                } else {
                    if (default(Tin2) is ValueType) {
                        return InnerLoops.ApplyIndexed.Generic_C_CS<Tin1, Tin2, Tout>.Instance.operate(Aarr, Barr, func);
                    } else {
                        return InnerLoops.ApplyIndexed.Generic_C_CC<Tin1, Tin2, Tout>.Instance.operate(Aarr, Barr, func);
                    }
                }
            }
        }
    }

    #region HYCALPER LOOPSTART 
    /*!HC:TYPELIST:
<hycalper>
<type>
    <source locate="here">
        BroadcastingBinaryMultiGenericBase_C_CS
    </source>
    <destination>BroadcastingBinaryMultiGenericBase_C_CC</destination>
    <destination>BroadcastingBinaryMultiGenericBase_C_SC</destination>
    <destination>BroadcastingBinaryMultiGenericBase_C_SS</destination>
    <destination>BroadcastingBinaryMultiGenericBase_S_CC</destination>
    <destination>BroadcastingBinaryMultiGenericBase_S_CS</destination>
    <destination>BroadcastingBinaryMultiGenericBase_S_SC</destination>
    <destination>BroadcastingBinaryMultiGenericBase_S_SS</destination>
</type>
<type>
    <source locate="here">
        Generic_C_CS
    </source>
    <destination>Generic_C_CC</destination>
    <destination>Generic_C_SC</destination>
    <destination>Generic_C_SS</destination>
    <destination>Generic_S_CC</destination>
    <destination>Generic_S_CS</destination>
    <destination>Generic_S_SC</destination>
    <destination>Generic_S_SS</destination>
</type>
<type>
    <source locate="nextline">
        HC_TOUT_CLASS
    </source>
    <destination><![CDATA[#if TOUT_IS_CLASS]]></destination>
    <destination><![CDATA[#if TOUT_IS_CLASS]]></destination>
    <destination><![CDATA[#if TOUT_IS_CLASS]]></destination>
    <destination><![CDATA[#if !TOUT_IS_CLASS]]></destination>
    <destination><![CDATA[#if !TOUT_IS_CLASS]]></destination>
    <destination><![CDATA[#if !TOUT_IS_CLASS]]></destination>
    <destination><![CDATA[#if !TOUT_IS_CLASS]]></destination>
</type>
<type>
    <source locate="nextline">
        HC_TIN1_CLASS
    </source>
    <destination><![CDATA[#if TIN1_IS_CLASS]]></destination>
    <destination><![CDATA[#if !TIN1_IS_CLASS]]></destination>
    <destination><![CDATA[#if !TIN1_IS_CLASS]]></destination>
    <destination><![CDATA[#if TIN1_IS_CLASS]]></destination>
    <destination><![CDATA[#if TIN1_IS_CLASS]]></destination>
    <destination><![CDATA[#if !TIN1_IS_CLASS]]></destination>
    <destination><![CDATA[#if !TIN1_IS_CLASS]]></destination>
</type>
//<type>
//    <source locate="after" endmark="/">
//        HC_IN1C
//    </source>
//    <destination><![CDATA[#if TIN1_IS_CLASS]]></destination>
//    <destination><![CDATA[#if !TIN1_IS_CLASS]]></destination>
//    <destination><![CDATA[#if !TIN1_IS_CLASS]]></destination>
//    <destination><![CDATA[#if TIN1_IS_CLASS]]></destination>
//    <destination><![CDATA[#if TIN1_IS_CLASS]]></destination>
//    <destination><![CDATA[#if !TIN1_IS_CLASS]]></destination>
//    <destination><![CDATA[#if !TIN1_IS_CLASS]]></destination>
//</type>
<type>
    <source locate="nextline">
        HC_TIN2_CLASS
    </source>
    <destination><![CDATA[#if TIN2_IS_CLASS]]></destination>
    <destination><![CDATA[#if TIN2_IS_CLASS]]></destination>
    <destination><![CDATA[#if !TIN2_IS_CLASS]]></destination>
    <destination><![CDATA[#if TIN2_IS_CLASS]]></destination>
    <destination><![CDATA[#if !TIN2_IS_CLASS]]></destination>
    <destination><![CDATA[#if TIN2_IS_CLASS]]></destination>
    <destination><![CDATA[#if !TIN2_IS_CLASS]]></destination>
</type>
</hycalper>
*/

    namespace InnerLoops.Apply {

        internal unsafe class Generic_C_CS<Tin1,Tin2,Tout>
            : BroadcastingBinaryMultiGenericBase_C_CS<
                Tin1, Array<Tin1>, InArray<Tin1>, OutArray<Tin1>, Array<Tin1>, Storage<Tin1>,
                Tin2, Array<Tin2>, InArray<Tin2>, OutArray<Tin2>, Array<Tin2>, Storage<Tin2>,
                Tout, Array<Tout>, InArray<Tout>, OutArray<Tout>, Array<Tout>, Storage<Tout>,
                Func<Tin1, Tin2, Tout>>
            {

            internal static Generic_C_CS<Tin1,Tin2,Tout> Instance = new Generic_C_CS<Tin1, Tin2, Tout>();

            /// <summary>
            /// Performs the inner loop for this binary operation.
            /// </summary>
            /// <param name="C">Pointer to base address of the output array, including any base offset.</param>
            /// <param name="A">Pointer to the base address of input array A, including any base offset.</param>
            /// <param name="B">Pointer to the base address of input array B, including any base offset.</param>
            /// <param name="start">Element index to start operation in this (thread) chunk.</param>
            /// <param name="len">Number of elements to process.</param>
            /// <param name="dims_strides"></param>
            /// <param name="func">The scalar function to be used to perform the binary action on individual, 
            /// corresponding elements of <paramref name="A"/> and <paramref name="B"/>.</param>
            /// <param name="offsA">Offset of first element into <paramref name="A"/>.</param>
            /// <param name="offsB">Offset of first element into <paramref name="B"/>.</param>
            /// <param name="offsC">Offset of first element into <paramref name="C"/>.</param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            
            internal override unsafe void Strided64(
            /*!HC:HC_TOUT_CLASS*/
#if TOUT_IS_CLASS
            byte* C,
#else
            Tout[] C,
#endif
            /*!HC:HC_TIN1_CLASS*/
#if TIN1_IS_CLASS
            byte* A,
#else
            Tin1[] A,
#endif
            /*!HC:HC_TIN2_CLASS*/
#if !TIN2_IS_CLASS
            byte* B,
#else
            Tin2[] B,
#endif
            long offsC, long offsA, long offsB,
            long start, long len, long* dims_strides, 
            Func<Tin1, Tin2, Tout> func)

{

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
                            /*!HC:HC_TOUT_CLASS*/
#if TOUT_IS_CLASS
                            Unsafe.Write<Tout>(C + offsC + start * Storage<Tout>.SizeOfT,
#else
                            C[start + offsC] =
#endif
                            /*!HC:HC_TIN1_CLASS*/
#if TIN1_IS_CLASS
                            func(Unsafe.Read<Tin1>((void*)(A + offsA)),
#else
                            func(A[offsA],
#endif
                            /*!HC:HC_TIN2_CLASS*/
#if !TIN2_IS_CLASS
                            Unsafe.Read<Tin2>((void*)(B + offsB)))
#else
                            B[offsB])
#endif
                    /*!HC:HC_TOUT_CLASS*/
#if TOUT_IS_CLASS
                        )
#endif
                    ;
                    return;
                }
                long* dims = dims_strides + 3;
                long* cur = stackalloc long[(int)ndims];
                long* strideOut = dims_strides + 3 + ndims;
                long* strideA = strideOut + ndims;
                long* strideB = strideA + ndims;

                // figure out the dimension index position for start
                cur[0] = start % (dims[0] + 1);
                offsC += cur[0] * strideOut[0];
                offsA += cur[0] * strideA[0];
                offsB += cur[0] * strideB[0];
                long f = start / (dims[0] + 1);
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % (dims[i] + 1);
                    f /= (dims[i] + 1);
                    offsC += cur[i] * strideOut[i];
                    offsA += cur[i] * strideA[i];
                    offsB += cur[i] * strideB[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);
                System.Diagnostics.Debug.Assert(strideOut[0] == Storage<Tout>.SizeOfT, "strides for pOut are expected as column major strides!"); // 
#endregion

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;

                    while (leadLen-- > 0) {
                        /*!HC:HC_TOUT_CLASS*/
#if TOUT_IS_CLASS
                            Unsafe.Write<Tout>(C + offsC,
#else
                        C[offsC] =
#endif
                            /*!HC:HC_TIN1_CLASS*/
#if TIN1_IS_CLASS
                            func(Unsafe.Read<Tin1>((void*)(A + offsA)),
#else
                            func(A[offsA],
#endif
                            /*!HC:HC_TIN2_CLASS*/
#if !TIN2_IS_CLASS
                            Unsafe.Read<Tin2>((void*)(B + offsB)))
#else
                            B[offsB])
#endif
                        /*!HC:HC_TOUT_CLASS*/
#if TOUT_IS_CLASS
                        )
#endif
                        ;
                        offsC += *strideOut;
                        offsA += *strideA;
                        offsB += *strideB;
                    }
                    if (len == 0) {
                        break;
                    }
                    // reset initial offset in lead dimension after first iteration
                    cur[0] = 0;
                    offsA -= strideA[0] * (dims[0] + 1);
                    offsB -= strideB[0] * (dims[0] + 1);

                    // increase higher dims
                    int d = 1;
                    while (d < ndims) {
                        // dims are minus 1
                        if (cur[d] < dims[d]) {
                            offsA += strideA[d];
                            offsB += strideB[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            offsA -= strideA[d] * dims[d];
                            offsB -= strideB[d] * dims[d];
                            d++;
                        }
                    }
                }
            }
        }
    }

    namespace InnerLoops.ApplyIndexed {

        internal unsafe class Generic_C_CS<Tin1, Tin2, Tout>
            : BroadcastingBinaryMultiGenericBase_C_CS<
                Tin1, Array<Tin1>, InArray<Tin1>, OutArray<Tin1>, Array<Tin1>, Storage<Tin1>,
                Tin2, Array<Tin2>, InArray<Tin2>, OutArray<Tin2>, Array<Tin2>, Storage<Tin2>,
                Tout, Array<Tout>, InArray<Tout>, OutArray<Tout>, Array<Tout>, Storage<Tout>,
                Func<Tin1, Tin2, long, Tout>> {

            internal static Generic_C_CS<Tin1, Tin2, Tout> Instance = new Generic_C_CS<Tin1, Tin2, Tout>();

            /// <summary>
            /// Performs the inner loop for this binary operation.
            /// </summary>
            /// <param name="C">Pointer to base address of the output array, including any base offset.</param>
            /// <param name="A">Pointer to the base address of input array A, including any base offset.</param>
            /// <param name="B">Pointer to the base address of input array B, including any base offset.</param>
            /// <param name="start">Element index to start operation in this (thread) chunk.</param>
            /// <param name="len">Number of elements to process.</param>
            /// <param name="dims_strides"></param>
            /// <param name="func">The scalar function to be used to perform the binary action on individual, 
            /// corresponding elements of <paramref name="A"/> and <paramref name="B"/>.</param>
            /// <param name="offsA">Offset of first element into <paramref name="A"/>.</param>
            /// <param name="offsB">Offset of first element into <paramref name="B"/>.</param>
            /// <param name="offsC">Offset of first element into <paramref name="C"/>.</param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            
            internal override unsafe void Strided64(
            /*!HC:HC_TOUT_CLASS*/
#if TOUT_IS_CLASS
            byte* C,
#else
            Tout[] C,
#endif
            /*!HC:HC_TIN1_CLASS*/
#if TIN1_IS_CLASS
            byte* A,
#else
            Tin1[] A,
#endif
            /*!HC:HC_TIN2_CLASS*/
#if !TIN2_IS_CLASS
            byte* B,
#else
            Tin2[] B,
#endif
            long offsC, long offsA, long offsB,
            long start, long len, long* dims_strides,
            Func<Tin1, Tin2, long, Tout> func) {

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
                    /*!HC:HC_TOUT_CLASS*/
#if TOUT_IS_CLASS
                            Unsafe.Write<Tout>(C + offsC + start * Storage<Tout>.SizeOfT,
#else
                    C[start + offsC] =
#endif
                            /*!HC:HC_TIN1_CLASS*/
#if TIN1_IS_CLASS
                            func(Unsafe.Read<Tin1>((void*)(A + offsA)),
#else
                            func(A[offsA],
#endif
                            /*!HC:HC_TIN2_CLASS*/
#if !TIN2_IS_CLASS
                            Unsafe.Read<Tin2>((void*)(B + offsB)), 0)
#else
                            B[offsB], 0)
#endif
                    /*!HC:HC_TOUT_CLASS*/
#if TOUT_IS_CLASS
                        )
#endif
                    ;
                    return;
                }
                long* dims = dims_strides + 3;
                long* cur = stackalloc long[(int)ndims];
                long* strideOut = dims_strides + 3 + ndims;
                long* strideA = strideOut + ndims;
                long* strideB = strideA + ndims;

                // figure out the dimension index position for start
                cur[0] = start % (dims[0] + 1);
                offsC += cur[0] * strideOut[0];
                offsA += cur[0] * strideA[0];
                offsB += cur[0] * strideB[0];
                long f = start / (dims[0] + 1);
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % (dims[i] + 1);
                    f /= (dims[i] + 1);
                    offsC += cur[i] * strideOut[i];
                    offsA += cur[i] * strideA[i];
                    offsB += cur[i] * strideB[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);
                System.Diagnostics.Debug.Assert(strideOut[0] == Storage<Tout>.SizeOfT, "strides for pOut are expected as column major strides!"); // 
                #endregion

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;

                    while (leadLen-- > 0) {
                        /*!HC:HC_TOUT_CLASS*/
#if TOUT_IS_CLASS
                            Unsafe.Write<Tout>(C + offsC,
#else
                        C[offsC] =
#endif
                            /*!HC:HC_TIN1_CLASS*/
#if TIN1_IS_CLASS
                            func(Unsafe.Read<Tin1>((void*)(A + offsA)),
#else
                            func(A[offsA],
#endif
                            /*!HC:HC_TIN2_CLASS*/
#if !TIN2_IS_CLASS
                            Unsafe.Read<Tin2>((void*)(B + offsB)), start++)
#else
                            B[offsB], start++)
#endif
                        /*!HC:HC_TOUT_CLASS*/
#if TOUT_IS_CLASS
                        )
#endif
                        ;
                        offsC += *strideOut;
                        offsA += *strideA;
                        offsB += *strideB;
                    }
                    if (len == 0) {
                        break;
                    }
                    // reset initial offset in lead dimension after first iteration
                    cur[0] = 0;
                    offsA -= strideA[0] * (dims[0] + 1);
                    offsB -= strideB[0] * (dims[0] + 1);

                    // increase higher dims
                    int d = 1;
                    while (d < ndims) {
                        // dims are minus 1
                        if (cur[d] < dims[d]) {
                            offsA += strideA[d];
                            offsB += strideB[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            offsA -= strideA[d] * dims[d];
                            offsB -= strideB[d] * dims[d];
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
   

    namespace InnerLoops.Apply {

        internal unsafe class Generic_S_SS<Tin1,Tin2,Tout>
            : BroadcastingBinaryMultiGenericBase_S_SS<
                Tin1, Array<Tin1>, InArray<Tin1>, OutArray<Tin1>, Array<Tin1>, Storage<Tin1>,
                Tin2, Array<Tin2>, InArray<Tin2>, OutArray<Tin2>, Array<Tin2>, Storage<Tin2>,
                Tout, Array<Tout>, InArray<Tout>, OutArray<Tout>, Array<Tout>, Storage<Tout>,
                Func<Tin1, Tin2, Tout>>
            {

            internal static Generic_S_SS<Tin1,Tin2,Tout> Instance = new Generic_S_SS<Tin1, Tin2, Tout>();

            /// <summary>
            /// Performs the inner loop for this binary operation.
            /// </summary>
            /// <param name="C">Pointer to base address of the output array, including any base offset.</param>
            /// <param name="A">Pointer to the base address of input array A, including any base offset.</param>
            /// <param name="B">Pointer to the base address of input array B, including any base offset.</param>
            /// <param name="start">Element index to start operation in this (thread) chunk.</param>
            /// <param name="len">Number of elements to process.</param>
            /// <param name="dims_strides"></param>
            /// <param name="func">The scalar function to be used to perform the binary action on individual, 
            /// corresponding elements of <paramref name="A"/> and <paramref name="B"/>.</param>
            /// <param name="offsA">Offset of first element into <paramref name="A"/>.</param>
            /// <param name="offsB">Offset of first element into <paramref name="B"/>.</param>
            /// <param name="offsC">Offset of first element into <paramref name="C"/>.</param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            
            internal override unsafe void Strided64(
            #if !TOUT_IS_CLASS
            byte* C,
#else
            Tout[] C,
#endif
            #if !TIN1_IS_CLASS
            byte* A,
#else
            Tin1[] A,
#endif
            #if !TIN2_IS_CLASS
            byte* B,
#else
            Tin2[] B,
#endif
            long offsC, long offsA, long offsB,
            long start, long len, long* dims_strides, 
            Func<Tin1, Tin2, Tout> func)

{

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
                            #if !TOUT_IS_CLASS
                            Unsafe.Write<Tout>(C + offsC + start * Storage<Tout>.SizeOfT,
#else
                            C[start + offsC] =
#endif
                            #if !TIN1_IS_CLASS
                            func(Unsafe.Read<Tin1>((void*)(A + offsA)),
#else
                            func(A[offsA],
#endif
                            #if !TIN2_IS_CLASS
                            Unsafe.Read<Tin2>((void*)(B + offsB)))
#else
                            B[offsB])
#endif
                    #if !TOUT_IS_CLASS
                        )
#endif
                    ;
                    return;
                }
                long* dims = dims_strides + 3;
                long* cur = stackalloc long[(int)ndims];
                long* strideOut = dims_strides + 3 + ndims;
                long* strideA = strideOut + ndims;
                long* strideB = strideA + ndims;

                // figure out the dimension index position for start
                cur[0] = start % (dims[0] + 1);
                offsC += cur[0] * strideOut[0];
                offsA += cur[0] * strideA[0];
                offsB += cur[0] * strideB[0];
                long f = start / (dims[0] + 1);
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % (dims[i] + 1);
                    f /= (dims[i] + 1);
                    offsC += cur[i] * strideOut[i];
                    offsA += cur[i] * strideA[i];
                    offsB += cur[i] * strideB[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);
                System.Diagnostics.Debug.Assert(strideOut[0] == Storage<Tout>.SizeOfT, "strides for pOut are expected as column major strides!"); // 
#endregion

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;

                    while (leadLen-- > 0) {
                        #if !TOUT_IS_CLASS
                            Unsafe.Write<Tout>(C + offsC,
#else
                        C[offsC] =
#endif
                            #if !TIN1_IS_CLASS
                            func(Unsafe.Read<Tin1>((void*)(A + offsA)),
#else
                            func(A[offsA],
#endif
                            #if !TIN2_IS_CLASS
                            Unsafe.Read<Tin2>((void*)(B + offsB)))
#else
                            B[offsB])
#endif
                        #if !TOUT_IS_CLASS
                        )
#endif
                        ;
                        offsC += *strideOut;
                        offsA += *strideA;
                        offsB += *strideB;
                    }
                    if (len == 0) {
                        break;
                    }
                    // reset initial offset in lead dimension after first iteration
                    cur[0] = 0;
                    offsA -= strideA[0] * (dims[0] + 1);
                    offsB -= strideB[0] * (dims[0] + 1);

                    // increase higher dims
                    int d = 1;
                    while (d < ndims) {
                        // dims are minus 1
                        if (cur[d] < dims[d]) {
                            offsA += strideA[d];
                            offsB += strideB[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            offsA -= strideA[d] * dims[d];
                            offsB -= strideB[d] * dims[d];
                            d++;
                        }
                    }
                }
            }
        }
    }

    namespace InnerLoops.ApplyIndexed {

        internal unsafe class Generic_S_SS<Tin1, Tin2, Tout>
            : BroadcastingBinaryMultiGenericBase_S_SS<
                Tin1, Array<Tin1>, InArray<Tin1>, OutArray<Tin1>, Array<Tin1>, Storage<Tin1>,
                Tin2, Array<Tin2>, InArray<Tin2>, OutArray<Tin2>, Array<Tin2>, Storage<Tin2>,
                Tout, Array<Tout>, InArray<Tout>, OutArray<Tout>, Array<Tout>, Storage<Tout>,
                Func<Tin1, Tin2, long, Tout>> {

            internal static Generic_S_SS<Tin1, Tin2, Tout> Instance = new Generic_S_SS<Tin1, Tin2, Tout>();

            /// <summary>
            /// Performs the inner loop for this binary operation.
            /// </summary>
            /// <param name="C">Pointer to base address of the output array, including any base offset.</param>
            /// <param name="A">Pointer to the base address of input array A, including any base offset.</param>
            /// <param name="B">Pointer to the base address of input array B, including any base offset.</param>
            /// <param name="start">Element index to start operation in this (thread) chunk.</param>
            /// <param name="len">Number of elements to process.</param>
            /// <param name="dims_strides"></param>
            /// <param name="func">The scalar function to be used to perform the binary action on individual, 
            /// corresponding elements of <paramref name="A"/> and <paramref name="B"/>.</param>
            /// <param name="offsA">Offset of first element into <paramref name="A"/>.</param>
            /// <param name="offsB">Offset of first element into <paramref name="B"/>.</param>
            /// <param name="offsC">Offset of first element into <paramref name="C"/>.</param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            
            internal override unsafe void Strided64(
            #if !TOUT_IS_CLASS
            byte* C,
#else
            Tout[] C,
#endif
            #if !TIN1_IS_CLASS
            byte* A,
#else
            Tin1[] A,
#endif
            #if !TIN2_IS_CLASS
            byte* B,
#else
            Tin2[] B,
#endif
            long offsC, long offsA, long offsB,
            long start, long len, long* dims_strides,
            Func<Tin1, Tin2, long, Tout> func) {

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
                    #if !TOUT_IS_CLASS
                            Unsafe.Write<Tout>(C + offsC + start * Storage<Tout>.SizeOfT,
#else
                    C[start + offsC] =
#endif
                            #if !TIN1_IS_CLASS
                            func(Unsafe.Read<Tin1>((void*)(A + offsA)),
#else
                            func(A[offsA],
#endif
                            #if !TIN2_IS_CLASS
                            Unsafe.Read<Tin2>((void*)(B + offsB)), 0)
#else
                            B[offsB], 0)
#endif
                    #if !TOUT_IS_CLASS
                        )
#endif
                    ;
                    return;
                }
                long* dims = dims_strides + 3;
                long* cur = stackalloc long[(int)ndims];
                long* strideOut = dims_strides + 3 + ndims;
                long* strideA = strideOut + ndims;
                long* strideB = strideA + ndims;

                // figure out the dimension index position for start
                cur[0] = start % (dims[0] + 1);
                offsC += cur[0] * strideOut[0];
                offsA += cur[0] * strideA[0];
                offsB += cur[0] * strideB[0];
                long f = start / (dims[0] + 1);
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % (dims[i] + 1);
                    f /= (dims[i] + 1);
                    offsC += cur[i] * strideOut[i];
                    offsA += cur[i] * strideA[i];
                    offsB += cur[i] * strideB[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);
                System.Diagnostics.Debug.Assert(strideOut[0] == Storage<Tout>.SizeOfT, "strides for pOut are expected as column major strides!"); // 
                #endregion

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;

                    while (leadLen-- > 0) {
                        #if !TOUT_IS_CLASS
                            Unsafe.Write<Tout>(C + offsC,
#else
                        C[offsC] =
#endif
                            #if !TIN1_IS_CLASS
                            func(Unsafe.Read<Tin1>((void*)(A + offsA)),
#else
                            func(A[offsA],
#endif
                            #if !TIN2_IS_CLASS
                            Unsafe.Read<Tin2>((void*)(B + offsB)), start++)
#else
                            B[offsB], start++)
#endif
                        #if !TOUT_IS_CLASS
                        )
#endif
                        ;
                        offsC += *strideOut;
                        offsA += *strideA;
                        offsB += *strideB;
                    }
                    if (len == 0) {
                        break;
                    }
                    // reset initial offset in lead dimension after first iteration
                    cur[0] = 0;
                    offsA -= strideA[0] * (dims[0] + 1);
                    offsB -= strideB[0] * (dims[0] + 1);

                    // increase higher dims
                    int d = 1;
                    while (d < ndims) {
                        // dims are minus 1
                        if (cur[d] < dims[d]) {
                            offsA += strideA[d];
                            offsB += strideB[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            offsA -= strideA[d] * dims[d];
                            offsB -= strideB[d] * dims[d];
                            d++;
                        }
                    }
                }
            }
        }
    }

   

    namespace InnerLoops.Apply {

        internal unsafe class Generic_S_SC<Tin1,Tin2,Tout>
            : BroadcastingBinaryMultiGenericBase_S_SC<
                Tin1, Array<Tin1>, InArray<Tin1>, OutArray<Tin1>, Array<Tin1>, Storage<Tin1>,
                Tin2, Array<Tin2>, InArray<Tin2>, OutArray<Tin2>, Array<Tin2>, Storage<Tin2>,
                Tout, Array<Tout>, InArray<Tout>, OutArray<Tout>, Array<Tout>, Storage<Tout>,
                Func<Tin1, Tin2, Tout>>
            {

            internal static Generic_S_SC<Tin1,Tin2,Tout> Instance = new Generic_S_SC<Tin1, Tin2, Tout>();

            /// <summary>
            /// Performs the inner loop for this binary operation.
            /// </summary>
            /// <param name="C">Pointer to base address of the output array, including any base offset.</param>
            /// <param name="A">Pointer to the base address of input array A, including any base offset.</param>
            /// <param name="B">Pointer to the base address of input array B, including any base offset.</param>
            /// <param name="start">Element index to start operation in this (thread) chunk.</param>
            /// <param name="len">Number of elements to process.</param>
            /// <param name="dims_strides"></param>
            /// <param name="func">The scalar function to be used to perform the binary action on individual, 
            /// corresponding elements of <paramref name="A"/> and <paramref name="B"/>.</param>
            /// <param name="offsA">Offset of first element into <paramref name="A"/>.</param>
            /// <param name="offsB">Offset of first element into <paramref name="B"/>.</param>
            /// <param name="offsC">Offset of first element into <paramref name="C"/>.</param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            
            internal override unsafe void Strided64(
            #if !TOUT_IS_CLASS
            byte* C,
#else
            Tout[] C,
#endif
            #if !TIN1_IS_CLASS
            byte* A,
#else
            Tin1[] A,
#endif
            #if TIN2_IS_CLASS
            byte* B,
#else
            Tin2[] B,
#endif
            long offsC, long offsA, long offsB,
            long start, long len, long* dims_strides, 
            Func<Tin1, Tin2, Tout> func)

{

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
                            #if !TOUT_IS_CLASS
                            Unsafe.Write<Tout>(C + offsC + start * Storage<Tout>.SizeOfT,
#else
                            C[start + offsC] =
#endif
                            #if !TIN1_IS_CLASS
                            func(Unsafe.Read<Tin1>((void*)(A + offsA)),
#else
                            func(A[offsA],
#endif
                            #if TIN2_IS_CLASS
                            Unsafe.Read<Tin2>((void*)(B + offsB)))
#else
                            B[offsB])
#endif
                    #if !TOUT_IS_CLASS
                        )
#endif
                    ;
                    return;
                }
                long* dims = dims_strides + 3;
                long* cur = stackalloc long[(int)ndims];
                long* strideOut = dims_strides + 3 + ndims;
                long* strideA = strideOut + ndims;
                long* strideB = strideA + ndims;

                // figure out the dimension index position for start
                cur[0] = start % (dims[0] + 1);
                offsC += cur[0] * strideOut[0];
                offsA += cur[0] * strideA[0];
                offsB += cur[0] * strideB[0];
                long f = start / (dims[0] + 1);
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % (dims[i] + 1);
                    f /= (dims[i] + 1);
                    offsC += cur[i] * strideOut[i];
                    offsA += cur[i] * strideA[i];
                    offsB += cur[i] * strideB[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);
                System.Diagnostics.Debug.Assert(strideOut[0] == Storage<Tout>.SizeOfT, "strides for pOut are expected as column major strides!"); // 
#endregion

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;

                    while (leadLen-- > 0) {
                        #if !TOUT_IS_CLASS
                            Unsafe.Write<Tout>(C + offsC,
#else
                        C[offsC] =
#endif
                            #if !TIN1_IS_CLASS
                            func(Unsafe.Read<Tin1>((void*)(A + offsA)),
#else
                            func(A[offsA],
#endif
                            #if TIN2_IS_CLASS
                            Unsafe.Read<Tin2>((void*)(B + offsB)))
#else
                            B[offsB])
#endif
                        #if !TOUT_IS_CLASS
                        )
#endif
                        ;
                        offsC += *strideOut;
                        offsA += *strideA;
                        offsB += *strideB;
                    }
                    if (len == 0) {
                        break;
                    }
                    // reset initial offset in lead dimension after first iteration
                    cur[0] = 0;
                    offsA -= strideA[0] * (dims[0] + 1);
                    offsB -= strideB[0] * (dims[0] + 1);

                    // increase higher dims
                    int d = 1;
                    while (d < ndims) {
                        // dims are minus 1
                        if (cur[d] < dims[d]) {
                            offsA += strideA[d];
                            offsB += strideB[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            offsA -= strideA[d] * dims[d];
                            offsB -= strideB[d] * dims[d];
                            d++;
                        }
                    }
                }
            }
        }
    }

    namespace InnerLoops.ApplyIndexed {

        internal unsafe class Generic_S_SC<Tin1, Tin2, Tout>
            : BroadcastingBinaryMultiGenericBase_S_SC<
                Tin1, Array<Tin1>, InArray<Tin1>, OutArray<Tin1>, Array<Tin1>, Storage<Tin1>,
                Tin2, Array<Tin2>, InArray<Tin2>, OutArray<Tin2>, Array<Tin2>, Storage<Tin2>,
                Tout, Array<Tout>, InArray<Tout>, OutArray<Tout>, Array<Tout>, Storage<Tout>,
                Func<Tin1, Tin2, long, Tout>> {

            internal static Generic_S_SC<Tin1, Tin2, Tout> Instance = new Generic_S_SC<Tin1, Tin2, Tout>();

            /// <summary>
            /// Performs the inner loop for this binary operation.
            /// </summary>
            /// <param name="C">Pointer to base address of the output array, including any base offset.</param>
            /// <param name="A">Pointer to the base address of input array A, including any base offset.</param>
            /// <param name="B">Pointer to the base address of input array B, including any base offset.</param>
            /// <param name="start">Element index to start operation in this (thread) chunk.</param>
            /// <param name="len">Number of elements to process.</param>
            /// <param name="dims_strides"></param>
            /// <param name="func">The scalar function to be used to perform the binary action on individual, 
            /// corresponding elements of <paramref name="A"/> and <paramref name="B"/>.</param>
            /// <param name="offsA">Offset of first element into <paramref name="A"/>.</param>
            /// <param name="offsB">Offset of first element into <paramref name="B"/>.</param>
            /// <param name="offsC">Offset of first element into <paramref name="C"/>.</param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            
            internal override unsafe void Strided64(
            #if !TOUT_IS_CLASS
            byte* C,
#else
            Tout[] C,
#endif
            #if !TIN1_IS_CLASS
            byte* A,
#else
            Tin1[] A,
#endif
            #if TIN2_IS_CLASS
            byte* B,
#else
            Tin2[] B,
#endif
            long offsC, long offsA, long offsB,
            long start, long len, long* dims_strides,
            Func<Tin1, Tin2, long, Tout> func) {

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
                    #if !TOUT_IS_CLASS
                            Unsafe.Write<Tout>(C + offsC + start * Storage<Tout>.SizeOfT,
#else
                    C[start + offsC] =
#endif
                            #if !TIN1_IS_CLASS
                            func(Unsafe.Read<Tin1>((void*)(A + offsA)),
#else
                            func(A[offsA],
#endif
                            #if TIN2_IS_CLASS
                            Unsafe.Read<Tin2>((void*)(B + offsB)), 0)
#else
                            B[offsB], 0)
#endif
                    #if !TOUT_IS_CLASS
                        )
#endif
                    ;
                    return;
                }
                long* dims = dims_strides + 3;
                long* cur = stackalloc long[(int)ndims];
                long* strideOut = dims_strides + 3 + ndims;
                long* strideA = strideOut + ndims;
                long* strideB = strideA + ndims;

                // figure out the dimension index position for start
                cur[0] = start % (dims[0] + 1);
                offsC += cur[0] * strideOut[0];
                offsA += cur[0] * strideA[0];
                offsB += cur[0] * strideB[0];
                long f = start / (dims[0] + 1);
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % (dims[i] + 1);
                    f /= (dims[i] + 1);
                    offsC += cur[i] * strideOut[i];
                    offsA += cur[i] * strideA[i];
                    offsB += cur[i] * strideB[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);
                System.Diagnostics.Debug.Assert(strideOut[0] == Storage<Tout>.SizeOfT, "strides for pOut are expected as column major strides!"); // 
                #endregion

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;

                    while (leadLen-- > 0) {
                        #if !TOUT_IS_CLASS
                            Unsafe.Write<Tout>(C + offsC,
#else
                        C[offsC] =
#endif
                            #if !TIN1_IS_CLASS
                            func(Unsafe.Read<Tin1>((void*)(A + offsA)),
#else
                            func(A[offsA],
#endif
                            #if TIN2_IS_CLASS
                            Unsafe.Read<Tin2>((void*)(B + offsB)), start++)
#else
                            B[offsB], start++)
#endif
                        #if !TOUT_IS_CLASS
                        )
#endif
                        ;
                        offsC += *strideOut;
                        offsA += *strideA;
                        offsB += *strideB;
                    }
                    if (len == 0) {
                        break;
                    }
                    // reset initial offset in lead dimension after first iteration
                    cur[0] = 0;
                    offsA -= strideA[0] * (dims[0] + 1);
                    offsB -= strideB[0] * (dims[0] + 1);

                    // increase higher dims
                    int d = 1;
                    while (d < ndims) {
                        // dims are minus 1
                        if (cur[d] < dims[d]) {
                            offsA += strideA[d];
                            offsB += strideB[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            offsA -= strideA[d] * dims[d];
                            offsB -= strideB[d] * dims[d];
                            d++;
                        }
                    }
                }
            }
        }
    }

   

    namespace InnerLoops.Apply {

        internal unsafe class Generic_S_CS<Tin1,Tin2,Tout>
            : BroadcastingBinaryMultiGenericBase_S_CS<
                Tin1, Array<Tin1>, InArray<Tin1>, OutArray<Tin1>, Array<Tin1>, Storage<Tin1>,
                Tin2, Array<Tin2>, InArray<Tin2>, OutArray<Tin2>, Array<Tin2>, Storage<Tin2>,
                Tout, Array<Tout>, InArray<Tout>, OutArray<Tout>, Array<Tout>, Storage<Tout>,
                Func<Tin1, Tin2, Tout>>
            {

            internal static Generic_S_CS<Tin1,Tin2,Tout> Instance = new Generic_S_CS<Tin1, Tin2, Tout>();

            /// <summary>
            /// Performs the inner loop for this binary operation.
            /// </summary>
            /// <param name="C">Pointer to base address of the output array, including any base offset.</param>
            /// <param name="A">Pointer to the base address of input array A, including any base offset.</param>
            /// <param name="B">Pointer to the base address of input array B, including any base offset.</param>
            /// <param name="start">Element index to start operation in this (thread) chunk.</param>
            /// <param name="len">Number of elements to process.</param>
            /// <param name="dims_strides"></param>
            /// <param name="func">The scalar function to be used to perform the binary action on individual, 
            /// corresponding elements of <paramref name="A"/> and <paramref name="B"/>.</param>
            /// <param name="offsA">Offset of first element into <paramref name="A"/>.</param>
            /// <param name="offsB">Offset of first element into <paramref name="B"/>.</param>
            /// <param name="offsC">Offset of first element into <paramref name="C"/>.</param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            
            internal override unsafe void Strided64(
            #if !TOUT_IS_CLASS
            byte* C,
#else
            Tout[] C,
#endif
            #if TIN1_IS_CLASS
            byte* A,
#else
            Tin1[] A,
#endif
            #if !TIN2_IS_CLASS
            byte* B,
#else
            Tin2[] B,
#endif
            long offsC, long offsA, long offsB,
            long start, long len, long* dims_strides, 
            Func<Tin1, Tin2, Tout> func)

{

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
                            #if !TOUT_IS_CLASS
                            Unsafe.Write<Tout>(C + offsC + start * Storage<Tout>.SizeOfT,
#else
                            C[start + offsC] =
#endif
                            #if TIN1_IS_CLASS
                            func(Unsafe.Read<Tin1>((void*)(A + offsA)),
#else
                            func(A[offsA],
#endif
                            #if !TIN2_IS_CLASS
                            Unsafe.Read<Tin2>((void*)(B + offsB)))
#else
                            B[offsB])
#endif
                    #if !TOUT_IS_CLASS
                        )
#endif
                    ;
                    return;
                }
                long* dims = dims_strides + 3;
                long* cur = stackalloc long[(int)ndims];
                long* strideOut = dims_strides + 3 + ndims;
                long* strideA = strideOut + ndims;
                long* strideB = strideA + ndims;

                // figure out the dimension index position for start
                cur[0] = start % (dims[0] + 1);
                offsC += cur[0] * strideOut[0];
                offsA += cur[0] * strideA[0];
                offsB += cur[0] * strideB[0];
                long f = start / (dims[0] + 1);
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % (dims[i] + 1);
                    f /= (dims[i] + 1);
                    offsC += cur[i] * strideOut[i];
                    offsA += cur[i] * strideA[i];
                    offsB += cur[i] * strideB[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);
                System.Diagnostics.Debug.Assert(strideOut[0] == Storage<Tout>.SizeOfT, "strides for pOut are expected as column major strides!"); // 
#endregion

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;

                    while (leadLen-- > 0) {
                        #if !TOUT_IS_CLASS
                            Unsafe.Write<Tout>(C + offsC,
#else
                        C[offsC] =
#endif
                            #if TIN1_IS_CLASS
                            func(Unsafe.Read<Tin1>((void*)(A + offsA)),
#else
                            func(A[offsA],
#endif
                            #if !TIN2_IS_CLASS
                            Unsafe.Read<Tin2>((void*)(B + offsB)))
#else
                            B[offsB])
#endif
                        #if !TOUT_IS_CLASS
                        )
#endif
                        ;
                        offsC += *strideOut;
                        offsA += *strideA;
                        offsB += *strideB;
                    }
                    if (len == 0) {
                        break;
                    }
                    // reset initial offset in lead dimension after first iteration
                    cur[0] = 0;
                    offsA -= strideA[0] * (dims[0] + 1);
                    offsB -= strideB[0] * (dims[0] + 1);

                    // increase higher dims
                    int d = 1;
                    while (d < ndims) {
                        // dims are minus 1
                        if (cur[d] < dims[d]) {
                            offsA += strideA[d];
                            offsB += strideB[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            offsA -= strideA[d] * dims[d];
                            offsB -= strideB[d] * dims[d];
                            d++;
                        }
                    }
                }
            }
        }
    }

    namespace InnerLoops.ApplyIndexed {

        internal unsafe class Generic_S_CS<Tin1, Tin2, Tout>
            : BroadcastingBinaryMultiGenericBase_S_CS<
                Tin1, Array<Tin1>, InArray<Tin1>, OutArray<Tin1>, Array<Tin1>, Storage<Tin1>,
                Tin2, Array<Tin2>, InArray<Tin2>, OutArray<Tin2>, Array<Tin2>, Storage<Tin2>,
                Tout, Array<Tout>, InArray<Tout>, OutArray<Tout>, Array<Tout>, Storage<Tout>,
                Func<Tin1, Tin2, long, Tout>> {

            internal static Generic_S_CS<Tin1, Tin2, Tout> Instance = new Generic_S_CS<Tin1, Tin2, Tout>();

            /// <summary>
            /// Performs the inner loop for this binary operation.
            /// </summary>
            /// <param name="C">Pointer to base address of the output array, including any base offset.</param>
            /// <param name="A">Pointer to the base address of input array A, including any base offset.</param>
            /// <param name="B">Pointer to the base address of input array B, including any base offset.</param>
            /// <param name="start">Element index to start operation in this (thread) chunk.</param>
            /// <param name="len">Number of elements to process.</param>
            /// <param name="dims_strides"></param>
            /// <param name="func">The scalar function to be used to perform the binary action on individual, 
            /// corresponding elements of <paramref name="A"/> and <paramref name="B"/>.</param>
            /// <param name="offsA">Offset of first element into <paramref name="A"/>.</param>
            /// <param name="offsB">Offset of first element into <paramref name="B"/>.</param>
            /// <param name="offsC">Offset of first element into <paramref name="C"/>.</param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            
            internal override unsafe void Strided64(
            #if !TOUT_IS_CLASS
            byte* C,
#else
            Tout[] C,
#endif
            #if TIN1_IS_CLASS
            byte* A,
#else
            Tin1[] A,
#endif
            #if !TIN2_IS_CLASS
            byte* B,
#else
            Tin2[] B,
#endif
            long offsC, long offsA, long offsB,
            long start, long len, long* dims_strides,
            Func<Tin1, Tin2, long, Tout> func) {

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
                    #if !TOUT_IS_CLASS
                            Unsafe.Write<Tout>(C + offsC + start * Storage<Tout>.SizeOfT,
#else
                    C[start + offsC] =
#endif
                            #if TIN1_IS_CLASS
                            func(Unsafe.Read<Tin1>((void*)(A + offsA)),
#else
                            func(A[offsA],
#endif
                            #if !TIN2_IS_CLASS
                            Unsafe.Read<Tin2>((void*)(B + offsB)), 0)
#else
                            B[offsB], 0)
#endif
                    #if !TOUT_IS_CLASS
                        )
#endif
                    ;
                    return;
                }
                long* dims = dims_strides + 3;
                long* cur = stackalloc long[(int)ndims];
                long* strideOut = dims_strides + 3 + ndims;
                long* strideA = strideOut + ndims;
                long* strideB = strideA + ndims;

                // figure out the dimension index position for start
                cur[0] = start % (dims[0] + 1);
                offsC += cur[0] * strideOut[0];
                offsA += cur[0] * strideA[0];
                offsB += cur[0] * strideB[0];
                long f = start / (dims[0] + 1);
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % (dims[i] + 1);
                    f /= (dims[i] + 1);
                    offsC += cur[i] * strideOut[i];
                    offsA += cur[i] * strideA[i];
                    offsB += cur[i] * strideB[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);
                System.Diagnostics.Debug.Assert(strideOut[0] == Storage<Tout>.SizeOfT, "strides for pOut are expected as column major strides!"); // 
                #endregion

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;

                    while (leadLen-- > 0) {
                        #if !TOUT_IS_CLASS
                            Unsafe.Write<Tout>(C + offsC,
#else
                        C[offsC] =
#endif
                            #if TIN1_IS_CLASS
                            func(Unsafe.Read<Tin1>((void*)(A + offsA)),
#else
                            func(A[offsA],
#endif
                            #if !TIN2_IS_CLASS
                            Unsafe.Read<Tin2>((void*)(B + offsB)), start++)
#else
                            B[offsB], start++)
#endif
                        #if !TOUT_IS_CLASS
                        )
#endif
                        ;
                        offsC += *strideOut;
                        offsA += *strideA;
                        offsB += *strideB;
                    }
                    if (len == 0) {
                        break;
                    }
                    // reset initial offset in lead dimension after first iteration
                    cur[0] = 0;
                    offsA -= strideA[0] * (dims[0] + 1);
                    offsB -= strideB[0] * (dims[0] + 1);

                    // increase higher dims
                    int d = 1;
                    while (d < ndims) {
                        // dims are minus 1
                        if (cur[d] < dims[d]) {
                            offsA += strideA[d];
                            offsB += strideB[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            offsA -= strideA[d] * dims[d];
                            offsB -= strideB[d] * dims[d];
                            d++;
                        }
                    }
                }
            }
        }
    }

   

    namespace InnerLoops.Apply {

        internal unsafe class Generic_S_CC<Tin1,Tin2,Tout>
            : BroadcastingBinaryMultiGenericBase_S_CC<
                Tin1, Array<Tin1>, InArray<Tin1>, OutArray<Tin1>, Array<Tin1>, Storage<Tin1>,
                Tin2, Array<Tin2>, InArray<Tin2>, OutArray<Tin2>, Array<Tin2>, Storage<Tin2>,
                Tout, Array<Tout>, InArray<Tout>, OutArray<Tout>, Array<Tout>, Storage<Tout>,
                Func<Tin1, Tin2, Tout>>
            {

            internal static Generic_S_CC<Tin1,Tin2,Tout> Instance = new Generic_S_CC<Tin1, Tin2, Tout>();

            /// <summary>
            /// Performs the inner loop for this binary operation.
            /// </summary>
            /// <param name="C">Pointer to base address of the output array, including any base offset.</param>
            /// <param name="A">Pointer to the base address of input array A, including any base offset.</param>
            /// <param name="B">Pointer to the base address of input array B, including any base offset.</param>
            /// <param name="start">Element index to start operation in this (thread) chunk.</param>
            /// <param name="len">Number of elements to process.</param>
            /// <param name="dims_strides"></param>
            /// <param name="func">The scalar function to be used to perform the binary action on individual, 
            /// corresponding elements of <paramref name="A"/> and <paramref name="B"/>.</param>
            /// <param name="offsA">Offset of first element into <paramref name="A"/>.</param>
            /// <param name="offsB">Offset of first element into <paramref name="B"/>.</param>
            /// <param name="offsC">Offset of first element into <paramref name="C"/>.</param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            
            internal override unsafe void Strided64(
            #if !TOUT_IS_CLASS
            byte* C,
#else
            Tout[] C,
#endif
            #if TIN1_IS_CLASS
            byte* A,
#else
            Tin1[] A,
#endif
            #if TIN2_IS_CLASS
            byte* B,
#else
            Tin2[] B,
#endif
            long offsC, long offsA, long offsB,
            long start, long len, long* dims_strides, 
            Func<Tin1, Tin2, Tout> func)

{

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
                            #if !TOUT_IS_CLASS
                            Unsafe.Write<Tout>(C + offsC + start * Storage<Tout>.SizeOfT,
#else
                            C[start + offsC] =
#endif
                            #if TIN1_IS_CLASS
                            func(Unsafe.Read<Tin1>((void*)(A + offsA)),
#else
                            func(A[offsA],
#endif
                            #if TIN2_IS_CLASS
                            Unsafe.Read<Tin2>((void*)(B + offsB)))
#else
                            B[offsB])
#endif
                    #if !TOUT_IS_CLASS
                        )
#endif
                    ;
                    return;
                }
                long* dims = dims_strides + 3;
                long* cur = stackalloc long[(int)ndims];
                long* strideOut = dims_strides + 3 + ndims;
                long* strideA = strideOut + ndims;
                long* strideB = strideA + ndims;

                // figure out the dimension index position for start
                cur[0] = start % (dims[0] + 1);
                offsC += cur[0] * strideOut[0];
                offsA += cur[0] * strideA[0];
                offsB += cur[0] * strideB[0];
                long f = start / (dims[0] + 1);
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % (dims[i] + 1);
                    f /= (dims[i] + 1);
                    offsC += cur[i] * strideOut[i];
                    offsA += cur[i] * strideA[i];
                    offsB += cur[i] * strideB[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);
                System.Diagnostics.Debug.Assert(strideOut[0] == Storage<Tout>.SizeOfT, "strides for pOut are expected as column major strides!"); // 
#endregion

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;

                    while (leadLen-- > 0) {
                        #if !TOUT_IS_CLASS
                            Unsafe.Write<Tout>(C + offsC,
#else
                        C[offsC] =
#endif
                            #if TIN1_IS_CLASS
                            func(Unsafe.Read<Tin1>((void*)(A + offsA)),
#else
                            func(A[offsA],
#endif
                            #if TIN2_IS_CLASS
                            Unsafe.Read<Tin2>((void*)(B + offsB)))
#else
                            B[offsB])
#endif
                        #if !TOUT_IS_CLASS
                        )
#endif
                        ;
                        offsC += *strideOut;
                        offsA += *strideA;
                        offsB += *strideB;
                    }
                    if (len == 0) {
                        break;
                    }
                    // reset initial offset in lead dimension after first iteration
                    cur[0] = 0;
                    offsA -= strideA[0] * (dims[0] + 1);
                    offsB -= strideB[0] * (dims[0] + 1);

                    // increase higher dims
                    int d = 1;
                    while (d < ndims) {
                        // dims are minus 1
                        if (cur[d] < dims[d]) {
                            offsA += strideA[d];
                            offsB += strideB[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            offsA -= strideA[d] * dims[d];
                            offsB -= strideB[d] * dims[d];
                            d++;
                        }
                    }
                }
            }
        }
    }

    namespace InnerLoops.ApplyIndexed {

        internal unsafe class Generic_S_CC<Tin1, Tin2, Tout>
            : BroadcastingBinaryMultiGenericBase_S_CC<
                Tin1, Array<Tin1>, InArray<Tin1>, OutArray<Tin1>, Array<Tin1>, Storage<Tin1>,
                Tin2, Array<Tin2>, InArray<Tin2>, OutArray<Tin2>, Array<Tin2>, Storage<Tin2>,
                Tout, Array<Tout>, InArray<Tout>, OutArray<Tout>, Array<Tout>, Storage<Tout>,
                Func<Tin1, Tin2, long, Tout>> {

            internal static Generic_S_CC<Tin1, Tin2, Tout> Instance = new Generic_S_CC<Tin1, Tin2, Tout>();

            /// <summary>
            /// Performs the inner loop for this binary operation.
            /// </summary>
            /// <param name="C">Pointer to base address of the output array, including any base offset.</param>
            /// <param name="A">Pointer to the base address of input array A, including any base offset.</param>
            /// <param name="B">Pointer to the base address of input array B, including any base offset.</param>
            /// <param name="start">Element index to start operation in this (thread) chunk.</param>
            /// <param name="len">Number of elements to process.</param>
            /// <param name="dims_strides"></param>
            /// <param name="func">The scalar function to be used to perform the binary action on individual, 
            /// corresponding elements of <paramref name="A"/> and <paramref name="B"/>.</param>
            /// <param name="offsA">Offset of first element into <paramref name="A"/>.</param>
            /// <param name="offsB">Offset of first element into <paramref name="B"/>.</param>
            /// <param name="offsC">Offset of first element into <paramref name="C"/>.</param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            
            internal override unsafe void Strided64(
            #if !TOUT_IS_CLASS
            byte* C,
#else
            Tout[] C,
#endif
            #if TIN1_IS_CLASS
            byte* A,
#else
            Tin1[] A,
#endif
            #if TIN2_IS_CLASS
            byte* B,
#else
            Tin2[] B,
#endif
            long offsC, long offsA, long offsB,
            long start, long len, long* dims_strides,
            Func<Tin1, Tin2, long, Tout> func) {

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
                    #if !TOUT_IS_CLASS
                            Unsafe.Write<Tout>(C + offsC + start * Storage<Tout>.SizeOfT,
#else
                    C[start + offsC] =
#endif
                            #if TIN1_IS_CLASS
                            func(Unsafe.Read<Tin1>((void*)(A + offsA)),
#else
                            func(A[offsA],
#endif
                            #if TIN2_IS_CLASS
                            Unsafe.Read<Tin2>((void*)(B + offsB)), 0)
#else
                            B[offsB], 0)
#endif
                    #if !TOUT_IS_CLASS
                        )
#endif
                    ;
                    return;
                }
                long* dims = dims_strides + 3;
                long* cur = stackalloc long[(int)ndims];
                long* strideOut = dims_strides + 3 + ndims;
                long* strideA = strideOut + ndims;
                long* strideB = strideA + ndims;

                // figure out the dimension index position for start
                cur[0] = start % (dims[0] + 1);
                offsC += cur[0] * strideOut[0];
                offsA += cur[0] * strideA[0];
                offsB += cur[0] * strideB[0];
                long f = start / (dims[0] + 1);
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % (dims[i] + 1);
                    f /= (dims[i] + 1);
                    offsC += cur[i] * strideOut[i];
                    offsA += cur[i] * strideA[i];
                    offsB += cur[i] * strideB[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);
                System.Diagnostics.Debug.Assert(strideOut[0] == Storage<Tout>.SizeOfT, "strides for pOut are expected as column major strides!"); // 
                #endregion

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;

                    while (leadLen-- > 0) {
                        #if !TOUT_IS_CLASS
                            Unsafe.Write<Tout>(C + offsC,
#else
                        C[offsC] =
#endif
                            #if TIN1_IS_CLASS
                            func(Unsafe.Read<Tin1>((void*)(A + offsA)),
#else
                            func(A[offsA],
#endif
                            #if TIN2_IS_CLASS
                            Unsafe.Read<Tin2>((void*)(B + offsB)), start++)
#else
                            B[offsB], start++)
#endif
                        #if !TOUT_IS_CLASS
                        )
#endif
                        ;
                        offsC += *strideOut;
                        offsA += *strideA;
                        offsB += *strideB;
                    }
                    if (len == 0) {
                        break;
                    }
                    // reset initial offset in lead dimension after first iteration
                    cur[0] = 0;
                    offsA -= strideA[0] * (dims[0] + 1);
                    offsB -= strideB[0] * (dims[0] + 1);

                    // increase higher dims
                    int d = 1;
                    while (d < ndims) {
                        // dims are minus 1
                        if (cur[d] < dims[d]) {
                            offsA += strideA[d];
                            offsB += strideB[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            offsA -= strideA[d] * dims[d];
                            offsB -= strideB[d] * dims[d];
                            d++;
                        }
                    }
                }
            }
        }
    }

   

    namespace InnerLoops.Apply {

        internal unsafe class Generic_C_SS<Tin1,Tin2,Tout>
            : BroadcastingBinaryMultiGenericBase_C_SS<
                Tin1, Array<Tin1>, InArray<Tin1>, OutArray<Tin1>, Array<Tin1>, Storage<Tin1>,
                Tin2, Array<Tin2>, InArray<Tin2>, OutArray<Tin2>, Array<Tin2>, Storage<Tin2>,
                Tout, Array<Tout>, InArray<Tout>, OutArray<Tout>, Array<Tout>, Storage<Tout>,
                Func<Tin1, Tin2, Tout>>
            {

            internal static Generic_C_SS<Tin1,Tin2,Tout> Instance = new Generic_C_SS<Tin1, Tin2, Tout>();

            /// <summary>
            /// Performs the inner loop for this binary operation.
            /// </summary>
            /// <param name="C">Pointer to base address of the output array, including any base offset.</param>
            /// <param name="A">Pointer to the base address of input array A, including any base offset.</param>
            /// <param name="B">Pointer to the base address of input array B, including any base offset.</param>
            /// <param name="start">Element index to start operation in this (thread) chunk.</param>
            /// <param name="len">Number of elements to process.</param>
            /// <param name="dims_strides"></param>
            /// <param name="func">The scalar function to be used to perform the binary action on individual, 
            /// corresponding elements of <paramref name="A"/> and <paramref name="B"/>.</param>
            /// <param name="offsA">Offset of first element into <paramref name="A"/>.</param>
            /// <param name="offsB">Offset of first element into <paramref name="B"/>.</param>
            /// <param name="offsC">Offset of first element into <paramref name="C"/>.</param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            
            internal override unsafe void Strided64(
            #if TOUT_IS_CLASS
            byte* C,
#else
            Tout[] C,
#endif
            #if !TIN1_IS_CLASS
            byte* A,
#else
            Tin1[] A,
#endif
            #if !TIN2_IS_CLASS
            byte* B,
#else
            Tin2[] B,
#endif
            long offsC, long offsA, long offsB,
            long start, long len, long* dims_strides, 
            Func<Tin1, Tin2, Tout> func)

{

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
                            #if TOUT_IS_CLASS
                            Unsafe.Write<Tout>(C + offsC + start * Storage<Tout>.SizeOfT,
#else
                            C[start + offsC] =
#endif
                            #if !TIN1_IS_CLASS
                            func(Unsafe.Read<Tin1>((void*)(A + offsA)),
#else
                            func(A[offsA],
#endif
                            #if !TIN2_IS_CLASS
                            Unsafe.Read<Tin2>((void*)(B + offsB)))
#else
                            B[offsB])
#endif
                    #if TOUT_IS_CLASS
                        )
#endif
                    ;
                    return;
                }
                long* dims = dims_strides + 3;
                long* cur = stackalloc long[(int)ndims];
                long* strideOut = dims_strides + 3 + ndims;
                long* strideA = strideOut + ndims;
                long* strideB = strideA + ndims;

                // figure out the dimension index position for start
                cur[0] = start % (dims[0] + 1);
                offsC += cur[0] * strideOut[0];
                offsA += cur[0] * strideA[0];
                offsB += cur[0] * strideB[0];
                long f = start / (dims[0] + 1);
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % (dims[i] + 1);
                    f /= (dims[i] + 1);
                    offsC += cur[i] * strideOut[i];
                    offsA += cur[i] * strideA[i];
                    offsB += cur[i] * strideB[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);
                System.Diagnostics.Debug.Assert(strideOut[0] == Storage<Tout>.SizeOfT, "strides for pOut are expected as column major strides!"); // 
#endregion

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;

                    while (leadLen-- > 0) {
                        #if TOUT_IS_CLASS
                            Unsafe.Write<Tout>(C + offsC,
#else
                        C[offsC] =
#endif
                            #if !TIN1_IS_CLASS
                            func(Unsafe.Read<Tin1>((void*)(A + offsA)),
#else
                            func(A[offsA],
#endif
                            #if !TIN2_IS_CLASS
                            Unsafe.Read<Tin2>((void*)(B + offsB)))
#else
                            B[offsB])
#endif
                        #if TOUT_IS_CLASS
                        )
#endif
                        ;
                        offsC += *strideOut;
                        offsA += *strideA;
                        offsB += *strideB;
                    }
                    if (len == 0) {
                        break;
                    }
                    // reset initial offset in lead dimension after first iteration
                    cur[0] = 0;
                    offsA -= strideA[0] * (dims[0] + 1);
                    offsB -= strideB[0] * (dims[0] + 1);

                    // increase higher dims
                    int d = 1;
                    while (d < ndims) {
                        // dims are minus 1
                        if (cur[d] < dims[d]) {
                            offsA += strideA[d];
                            offsB += strideB[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            offsA -= strideA[d] * dims[d];
                            offsB -= strideB[d] * dims[d];
                            d++;
                        }
                    }
                }
            }
        }
    }

    namespace InnerLoops.ApplyIndexed {

        internal unsafe class Generic_C_SS<Tin1, Tin2, Tout>
            : BroadcastingBinaryMultiGenericBase_C_SS<
                Tin1, Array<Tin1>, InArray<Tin1>, OutArray<Tin1>, Array<Tin1>, Storage<Tin1>,
                Tin2, Array<Tin2>, InArray<Tin2>, OutArray<Tin2>, Array<Tin2>, Storage<Tin2>,
                Tout, Array<Tout>, InArray<Tout>, OutArray<Tout>, Array<Tout>, Storage<Tout>,
                Func<Tin1, Tin2, long, Tout>> {

            internal static Generic_C_SS<Tin1, Tin2, Tout> Instance = new Generic_C_SS<Tin1, Tin2, Tout>();

            /// <summary>
            /// Performs the inner loop for this binary operation.
            /// </summary>
            /// <param name="C">Pointer to base address of the output array, including any base offset.</param>
            /// <param name="A">Pointer to the base address of input array A, including any base offset.</param>
            /// <param name="B">Pointer to the base address of input array B, including any base offset.</param>
            /// <param name="start">Element index to start operation in this (thread) chunk.</param>
            /// <param name="len">Number of elements to process.</param>
            /// <param name="dims_strides"></param>
            /// <param name="func">The scalar function to be used to perform the binary action on individual, 
            /// corresponding elements of <paramref name="A"/> and <paramref name="B"/>.</param>
            /// <param name="offsA">Offset of first element into <paramref name="A"/>.</param>
            /// <param name="offsB">Offset of first element into <paramref name="B"/>.</param>
            /// <param name="offsC">Offset of first element into <paramref name="C"/>.</param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            
            internal override unsafe void Strided64(
            #if TOUT_IS_CLASS
            byte* C,
#else
            Tout[] C,
#endif
            #if !TIN1_IS_CLASS
            byte* A,
#else
            Tin1[] A,
#endif
            #if !TIN2_IS_CLASS
            byte* B,
#else
            Tin2[] B,
#endif
            long offsC, long offsA, long offsB,
            long start, long len, long* dims_strides,
            Func<Tin1, Tin2, long, Tout> func) {

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
                    #if TOUT_IS_CLASS
                            Unsafe.Write<Tout>(C + offsC + start * Storage<Tout>.SizeOfT,
#else
                    C[start + offsC] =
#endif
                            #if !TIN1_IS_CLASS
                            func(Unsafe.Read<Tin1>((void*)(A + offsA)),
#else
                            func(A[offsA],
#endif
                            #if !TIN2_IS_CLASS
                            Unsafe.Read<Tin2>((void*)(B + offsB)), 0)
#else
                            B[offsB], 0)
#endif
                    #if TOUT_IS_CLASS
                        )
#endif
                    ;
                    return;
                }
                long* dims = dims_strides + 3;
                long* cur = stackalloc long[(int)ndims];
                long* strideOut = dims_strides + 3 + ndims;
                long* strideA = strideOut + ndims;
                long* strideB = strideA + ndims;

                // figure out the dimension index position for start
                cur[0] = start % (dims[0] + 1);
                offsC += cur[0] * strideOut[0];
                offsA += cur[0] * strideA[0];
                offsB += cur[0] * strideB[0];
                long f = start / (dims[0] + 1);
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % (dims[i] + 1);
                    f /= (dims[i] + 1);
                    offsC += cur[i] * strideOut[i];
                    offsA += cur[i] * strideA[i];
                    offsB += cur[i] * strideB[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);
                System.Diagnostics.Debug.Assert(strideOut[0] == Storage<Tout>.SizeOfT, "strides for pOut are expected as column major strides!"); // 
                #endregion

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;

                    while (leadLen-- > 0) {
                        #if TOUT_IS_CLASS
                            Unsafe.Write<Tout>(C + offsC,
#else
                        C[offsC] =
#endif
                            #if !TIN1_IS_CLASS
                            func(Unsafe.Read<Tin1>((void*)(A + offsA)),
#else
                            func(A[offsA],
#endif
                            #if !TIN2_IS_CLASS
                            Unsafe.Read<Tin2>((void*)(B + offsB)), start++)
#else
                            B[offsB], start++)
#endif
                        #if TOUT_IS_CLASS
                        )
#endif
                        ;
                        offsC += *strideOut;
                        offsA += *strideA;
                        offsB += *strideB;
                    }
                    if (len == 0) {
                        break;
                    }
                    // reset initial offset in lead dimension after first iteration
                    cur[0] = 0;
                    offsA -= strideA[0] * (dims[0] + 1);
                    offsB -= strideB[0] * (dims[0] + 1);

                    // increase higher dims
                    int d = 1;
                    while (d < ndims) {
                        // dims are minus 1
                        if (cur[d] < dims[d]) {
                            offsA += strideA[d];
                            offsB += strideB[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            offsA -= strideA[d] * dims[d];
                            offsB -= strideB[d] * dims[d];
                            d++;
                        }
                    }
                }
            }
        }
    }

   

    namespace InnerLoops.Apply {

        internal unsafe class Generic_C_SC<Tin1,Tin2,Tout>
            : BroadcastingBinaryMultiGenericBase_C_SC<
                Tin1, Array<Tin1>, InArray<Tin1>, OutArray<Tin1>, Array<Tin1>, Storage<Tin1>,
                Tin2, Array<Tin2>, InArray<Tin2>, OutArray<Tin2>, Array<Tin2>, Storage<Tin2>,
                Tout, Array<Tout>, InArray<Tout>, OutArray<Tout>, Array<Tout>, Storage<Tout>,
                Func<Tin1, Tin2, Tout>>
            {

            internal static Generic_C_SC<Tin1,Tin2,Tout> Instance = new Generic_C_SC<Tin1, Tin2, Tout>();

            /// <summary>
            /// Performs the inner loop for this binary operation.
            /// </summary>
            /// <param name="C">Pointer to base address of the output array, including any base offset.</param>
            /// <param name="A">Pointer to the base address of input array A, including any base offset.</param>
            /// <param name="B">Pointer to the base address of input array B, including any base offset.</param>
            /// <param name="start">Element index to start operation in this (thread) chunk.</param>
            /// <param name="len">Number of elements to process.</param>
            /// <param name="dims_strides"></param>
            /// <param name="func">The scalar function to be used to perform the binary action on individual, 
            /// corresponding elements of <paramref name="A"/> and <paramref name="B"/>.</param>
            /// <param name="offsA">Offset of first element into <paramref name="A"/>.</param>
            /// <param name="offsB">Offset of first element into <paramref name="B"/>.</param>
            /// <param name="offsC">Offset of first element into <paramref name="C"/>.</param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            
            internal override unsafe void Strided64(
            #if TOUT_IS_CLASS
            byte* C,
#else
            Tout[] C,
#endif
            #if !TIN1_IS_CLASS
            byte* A,
#else
            Tin1[] A,
#endif
            #if TIN2_IS_CLASS
            byte* B,
#else
            Tin2[] B,
#endif
            long offsC, long offsA, long offsB,
            long start, long len, long* dims_strides, 
            Func<Tin1, Tin2, Tout> func)

{

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
                            #if TOUT_IS_CLASS
                            Unsafe.Write<Tout>(C + offsC + start * Storage<Tout>.SizeOfT,
#else
                            C[start + offsC] =
#endif
                            #if !TIN1_IS_CLASS
                            func(Unsafe.Read<Tin1>((void*)(A + offsA)),
#else
                            func(A[offsA],
#endif
                            #if TIN2_IS_CLASS
                            Unsafe.Read<Tin2>((void*)(B + offsB)))
#else
                            B[offsB])
#endif
                    #if TOUT_IS_CLASS
                        )
#endif
                    ;
                    return;
                }
                long* dims = dims_strides + 3;
                long* cur = stackalloc long[(int)ndims];
                long* strideOut = dims_strides + 3 + ndims;
                long* strideA = strideOut + ndims;
                long* strideB = strideA + ndims;

                // figure out the dimension index position for start
                cur[0] = start % (dims[0] + 1);
                offsC += cur[0] * strideOut[0];
                offsA += cur[0] * strideA[0];
                offsB += cur[0] * strideB[0];
                long f = start / (dims[0] + 1);
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % (dims[i] + 1);
                    f /= (dims[i] + 1);
                    offsC += cur[i] * strideOut[i];
                    offsA += cur[i] * strideA[i];
                    offsB += cur[i] * strideB[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);
                System.Diagnostics.Debug.Assert(strideOut[0] == Storage<Tout>.SizeOfT, "strides for pOut are expected as column major strides!"); // 
#endregion

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;

                    while (leadLen-- > 0) {
                        #if TOUT_IS_CLASS
                            Unsafe.Write<Tout>(C + offsC,
#else
                        C[offsC] =
#endif
                            #if !TIN1_IS_CLASS
                            func(Unsafe.Read<Tin1>((void*)(A + offsA)),
#else
                            func(A[offsA],
#endif
                            #if TIN2_IS_CLASS
                            Unsafe.Read<Tin2>((void*)(B + offsB)))
#else
                            B[offsB])
#endif
                        #if TOUT_IS_CLASS
                        )
#endif
                        ;
                        offsC += *strideOut;
                        offsA += *strideA;
                        offsB += *strideB;
                    }
                    if (len == 0) {
                        break;
                    }
                    // reset initial offset in lead dimension after first iteration
                    cur[0] = 0;
                    offsA -= strideA[0] * (dims[0] + 1);
                    offsB -= strideB[0] * (dims[0] + 1);

                    // increase higher dims
                    int d = 1;
                    while (d < ndims) {
                        // dims are minus 1
                        if (cur[d] < dims[d]) {
                            offsA += strideA[d];
                            offsB += strideB[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            offsA -= strideA[d] * dims[d];
                            offsB -= strideB[d] * dims[d];
                            d++;
                        }
                    }
                }
            }
        }
    }

    namespace InnerLoops.ApplyIndexed {

        internal unsafe class Generic_C_SC<Tin1, Tin2, Tout>
            : BroadcastingBinaryMultiGenericBase_C_SC<
                Tin1, Array<Tin1>, InArray<Tin1>, OutArray<Tin1>, Array<Tin1>, Storage<Tin1>,
                Tin2, Array<Tin2>, InArray<Tin2>, OutArray<Tin2>, Array<Tin2>, Storage<Tin2>,
                Tout, Array<Tout>, InArray<Tout>, OutArray<Tout>, Array<Tout>, Storage<Tout>,
                Func<Tin1, Tin2, long, Tout>> {

            internal static Generic_C_SC<Tin1, Tin2, Tout> Instance = new Generic_C_SC<Tin1, Tin2, Tout>();

            /// <summary>
            /// Performs the inner loop for this binary operation.
            /// </summary>
            /// <param name="C">Pointer to base address of the output array, including any base offset.</param>
            /// <param name="A">Pointer to the base address of input array A, including any base offset.</param>
            /// <param name="B">Pointer to the base address of input array B, including any base offset.</param>
            /// <param name="start">Element index to start operation in this (thread) chunk.</param>
            /// <param name="len">Number of elements to process.</param>
            /// <param name="dims_strides"></param>
            /// <param name="func">The scalar function to be used to perform the binary action on individual, 
            /// corresponding elements of <paramref name="A"/> and <paramref name="B"/>.</param>
            /// <param name="offsA">Offset of first element into <paramref name="A"/>.</param>
            /// <param name="offsB">Offset of first element into <paramref name="B"/>.</param>
            /// <param name="offsC">Offset of first element into <paramref name="C"/>.</param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            
            internal override unsafe void Strided64(
            #if TOUT_IS_CLASS
            byte* C,
#else
            Tout[] C,
#endif
            #if !TIN1_IS_CLASS
            byte* A,
#else
            Tin1[] A,
#endif
            #if TIN2_IS_CLASS
            byte* B,
#else
            Tin2[] B,
#endif
            long offsC, long offsA, long offsB,
            long start, long len, long* dims_strides,
            Func<Tin1, Tin2, long, Tout> func) {

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
                    #if TOUT_IS_CLASS
                            Unsafe.Write<Tout>(C + offsC + start * Storage<Tout>.SizeOfT,
#else
                    C[start + offsC] =
#endif
                            #if !TIN1_IS_CLASS
                            func(Unsafe.Read<Tin1>((void*)(A + offsA)),
#else
                            func(A[offsA],
#endif
                            #if TIN2_IS_CLASS
                            Unsafe.Read<Tin2>((void*)(B + offsB)), 0)
#else
                            B[offsB], 0)
#endif
                    #if TOUT_IS_CLASS
                        )
#endif
                    ;
                    return;
                }
                long* dims = dims_strides + 3;
                long* cur = stackalloc long[(int)ndims];
                long* strideOut = dims_strides + 3 + ndims;
                long* strideA = strideOut + ndims;
                long* strideB = strideA + ndims;

                // figure out the dimension index position for start
                cur[0] = start % (dims[0] + 1);
                offsC += cur[0] * strideOut[0];
                offsA += cur[0] * strideA[0];
                offsB += cur[0] * strideB[0];
                long f = start / (dims[0] + 1);
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % (dims[i] + 1);
                    f /= (dims[i] + 1);
                    offsC += cur[i] * strideOut[i];
                    offsA += cur[i] * strideA[i];
                    offsB += cur[i] * strideB[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);
                System.Diagnostics.Debug.Assert(strideOut[0] == Storage<Tout>.SizeOfT, "strides for pOut are expected as column major strides!"); // 
                #endregion

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;

                    while (leadLen-- > 0) {
                        #if TOUT_IS_CLASS
                            Unsafe.Write<Tout>(C + offsC,
#else
                        C[offsC] =
#endif
                            #if !TIN1_IS_CLASS
                            func(Unsafe.Read<Tin1>((void*)(A + offsA)),
#else
                            func(A[offsA],
#endif
                            #if TIN2_IS_CLASS
                            Unsafe.Read<Tin2>((void*)(B + offsB)), start++)
#else
                            B[offsB], start++)
#endif
                        #if TOUT_IS_CLASS
                        )
#endif
                        ;
                        offsC += *strideOut;
                        offsA += *strideA;
                        offsB += *strideB;
                    }
                    if (len == 0) {
                        break;
                    }
                    // reset initial offset in lead dimension after first iteration
                    cur[0] = 0;
                    offsA -= strideA[0] * (dims[0] + 1);
                    offsB -= strideB[0] * (dims[0] + 1);

                    // increase higher dims
                    int d = 1;
                    while (d < ndims) {
                        // dims are minus 1
                        if (cur[d] < dims[d]) {
                            offsA += strideA[d];
                            offsB += strideB[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            offsA -= strideA[d] * dims[d];
                            offsB -= strideB[d] * dims[d];
                            d++;
                        }
                    }
                }
            }
        }
    }

   

    namespace InnerLoops.Apply {

        internal unsafe class Generic_C_CC<Tin1,Tin2,Tout>
            : BroadcastingBinaryMultiGenericBase_C_CC<
                Tin1, Array<Tin1>, InArray<Tin1>, OutArray<Tin1>, Array<Tin1>, Storage<Tin1>,
                Tin2, Array<Tin2>, InArray<Tin2>, OutArray<Tin2>, Array<Tin2>, Storage<Tin2>,
                Tout, Array<Tout>, InArray<Tout>, OutArray<Tout>, Array<Tout>, Storage<Tout>,
                Func<Tin1, Tin2, Tout>>
            {

            internal static Generic_C_CC<Tin1,Tin2,Tout> Instance = new Generic_C_CC<Tin1, Tin2, Tout>();

            /// <summary>
            /// Performs the inner loop for this binary operation.
            /// </summary>
            /// <param name="C">Pointer to base address of the output array, including any base offset.</param>
            /// <param name="A">Pointer to the base address of input array A, including any base offset.</param>
            /// <param name="B">Pointer to the base address of input array B, including any base offset.</param>
            /// <param name="start">Element index to start operation in this (thread) chunk.</param>
            /// <param name="len">Number of elements to process.</param>
            /// <param name="dims_strides"></param>
            /// <param name="func">The scalar function to be used to perform the binary action on individual, 
            /// corresponding elements of <paramref name="A"/> and <paramref name="B"/>.</param>
            /// <param name="offsA">Offset of first element into <paramref name="A"/>.</param>
            /// <param name="offsB">Offset of first element into <paramref name="B"/>.</param>
            /// <param name="offsC">Offset of first element into <paramref name="C"/>.</param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            
            internal override unsafe void Strided64(
            #if TOUT_IS_CLASS
            byte* C,
#else
            Tout[] C,
#endif
            #if TIN1_IS_CLASS
            byte* A,
#else
            Tin1[] A,
#endif
            #if TIN2_IS_CLASS
            byte* B,
#else
            Tin2[] B,
#endif
            long offsC, long offsA, long offsB,
            long start, long len, long* dims_strides, 
            Func<Tin1, Tin2, Tout> func)

{

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
                            #if TOUT_IS_CLASS
                            Unsafe.Write<Tout>(C + offsC + start * Storage<Tout>.SizeOfT,
#else
                            C[start + offsC] =
#endif
                            #if TIN1_IS_CLASS
                            func(Unsafe.Read<Tin1>((void*)(A + offsA)),
#else
                            func(A[offsA],
#endif
                            #if TIN2_IS_CLASS
                            Unsafe.Read<Tin2>((void*)(B + offsB)))
#else
                            B[offsB])
#endif
                    #if TOUT_IS_CLASS
                        )
#endif
                    ;
                    return;
                }
                long* dims = dims_strides + 3;
                long* cur = stackalloc long[(int)ndims];
                long* strideOut = dims_strides + 3 + ndims;
                long* strideA = strideOut + ndims;
                long* strideB = strideA + ndims;

                // figure out the dimension index position for start
                cur[0] = start % (dims[0] + 1);
                offsC += cur[0] * strideOut[0];
                offsA += cur[0] * strideA[0];
                offsB += cur[0] * strideB[0];
                long f = start / (dims[0] + 1);
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % (dims[i] + 1);
                    f /= (dims[i] + 1);
                    offsC += cur[i] * strideOut[i];
                    offsA += cur[i] * strideA[i];
                    offsB += cur[i] * strideB[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);
                System.Diagnostics.Debug.Assert(strideOut[0] == Storage<Tout>.SizeOfT, "strides for pOut are expected as column major strides!"); // 
#endregion

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;

                    while (leadLen-- > 0) {
                        #if TOUT_IS_CLASS
                            Unsafe.Write<Tout>(C + offsC,
#else
                        C[offsC] =
#endif
                            #if TIN1_IS_CLASS
                            func(Unsafe.Read<Tin1>((void*)(A + offsA)),
#else
                            func(A[offsA],
#endif
                            #if TIN2_IS_CLASS
                            Unsafe.Read<Tin2>((void*)(B + offsB)))
#else
                            B[offsB])
#endif
                        #if TOUT_IS_CLASS
                        )
#endif
                        ;
                        offsC += *strideOut;
                        offsA += *strideA;
                        offsB += *strideB;
                    }
                    if (len == 0) {
                        break;
                    }
                    // reset initial offset in lead dimension after first iteration
                    cur[0] = 0;
                    offsA -= strideA[0] * (dims[0] + 1);
                    offsB -= strideB[0] * (dims[0] + 1);

                    // increase higher dims
                    int d = 1;
                    while (d < ndims) {
                        // dims are minus 1
                        if (cur[d] < dims[d]) {
                            offsA += strideA[d];
                            offsB += strideB[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            offsA -= strideA[d] * dims[d];
                            offsB -= strideB[d] * dims[d];
                            d++;
                        }
                    }
                }
            }
        }
    }

    namespace InnerLoops.ApplyIndexed {

        internal unsafe class Generic_C_CC<Tin1, Tin2, Tout>
            : BroadcastingBinaryMultiGenericBase_C_CC<
                Tin1, Array<Tin1>, InArray<Tin1>, OutArray<Tin1>, Array<Tin1>, Storage<Tin1>,
                Tin2, Array<Tin2>, InArray<Tin2>, OutArray<Tin2>, Array<Tin2>, Storage<Tin2>,
                Tout, Array<Tout>, InArray<Tout>, OutArray<Tout>, Array<Tout>, Storage<Tout>,
                Func<Tin1, Tin2, long, Tout>> {

            internal static Generic_C_CC<Tin1, Tin2, Tout> Instance = new Generic_C_CC<Tin1, Tin2, Tout>();

            /// <summary>
            /// Performs the inner loop for this binary operation.
            /// </summary>
            /// <param name="C">Pointer to base address of the output array, including any base offset.</param>
            /// <param name="A">Pointer to the base address of input array A, including any base offset.</param>
            /// <param name="B">Pointer to the base address of input array B, including any base offset.</param>
            /// <param name="start">Element index to start operation in this (thread) chunk.</param>
            /// <param name="len">Number of elements to process.</param>
            /// <param name="dims_strides"></param>
            /// <param name="func">The scalar function to be used to perform the binary action on individual, 
            /// corresponding elements of <paramref name="A"/> and <paramref name="B"/>.</param>
            /// <param name="offsA">Offset of first element into <paramref name="A"/>.</param>
            /// <param name="offsB">Offset of first element into <paramref name="B"/>.</param>
            /// <param name="offsC">Offset of first element into <paramref name="C"/>.</param>
            /// <remarks><para>dims_strides carries the BSD of the output array and the strides of A and B. 
            /// Dimensions may be reordered, strides include the element length scaling. All dims / strides are 
            /// of length dims_strides[0] exactly. outdims are actual (broadcasted) dims - 1! </para></remarks>
            
            internal override unsafe void Strided64(
            #if TOUT_IS_CLASS
            byte* C,
#else
            Tout[] C,
#endif
            #if TIN1_IS_CLASS
            byte* A,
#else
            Tin1[] A,
#endif
            #if TIN2_IS_CLASS
            byte* B,
#else
            Tin2[] B,
#endif
            long offsC, long offsA, long offsB,
            long start, long len, long* dims_strides,
            Func<Tin1, Tin2, long, Tout> func) {

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
                    #if TOUT_IS_CLASS
                            Unsafe.Write<Tout>(C + offsC + start * Storage<Tout>.SizeOfT,
#else
                    C[start + offsC] =
#endif
                            #if TIN1_IS_CLASS
                            func(Unsafe.Read<Tin1>((void*)(A + offsA)),
#else
                            func(A[offsA],
#endif
                            #if TIN2_IS_CLASS
                            Unsafe.Read<Tin2>((void*)(B + offsB)), 0)
#else
                            B[offsB], 0)
#endif
                    #if TOUT_IS_CLASS
                        )
#endif
                    ;
                    return;
                }
                long* dims = dims_strides + 3;
                long* cur = stackalloc long[(int)ndims];
                long* strideOut = dims_strides + 3 + ndims;
                long* strideA = strideOut + ndims;
                long* strideB = strideA + ndims;

                // figure out the dimension index position for start
                cur[0] = start % (dims[0] + 1);
                offsC += cur[0] * strideOut[0];
                offsA += cur[0] * strideA[0];
                offsB += cur[0] * strideB[0];
                long f = start / (dims[0] + 1);
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % (dims[i] + 1);
                    f /= (dims[i] + 1);
                    offsC += cur[i] * strideOut[i];
                    offsA += cur[i] * strideA[i];
                    offsB += cur[i] * strideB[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);
                System.Diagnostics.Debug.Assert(strideOut[0] == Storage<Tout>.SizeOfT, "strides for pOut are expected as column major strides!"); // 
                #endregion

                while (true) {

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                    len -= leadLen;

                    while (leadLen-- > 0) {
                        #if TOUT_IS_CLASS
                            Unsafe.Write<Tout>(C + offsC,
#else
                        C[offsC] =
#endif
                            #if TIN1_IS_CLASS
                            func(Unsafe.Read<Tin1>((void*)(A + offsA)),
#else
                            func(A[offsA],
#endif
                            #if TIN2_IS_CLASS
                            Unsafe.Read<Tin2>((void*)(B + offsB)), start++)
#else
                            B[offsB], start++)
#endif
                        #if TOUT_IS_CLASS
                        )
#endif
                        ;
                        offsC += *strideOut;
                        offsA += *strideA;
                        offsB += *strideB;
                    }
                    if (len == 0) {
                        break;
                    }
                    // reset initial offset in lead dimension after first iteration
                    cur[0] = 0;
                    offsA -= strideA[0] * (dims[0] + 1);
                    offsB -= strideB[0] * (dims[0] + 1);

                    // increase higher dims
                    int d = 1;
                    while (d < ndims) {
                        // dims are minus 1
                        if (cur[d] < dims[d]) {
                            offsA += strideA[d];
                            offsB += strideB[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            offsA -= strideA[d] * dims[d];
                            offsB -= strideB[d] * dims[d];
                            d++;
                        }
                    }
                }
            }
        }
    }


#endregion HYCALPER AUTO GENERATED CODE
#endregion


}
