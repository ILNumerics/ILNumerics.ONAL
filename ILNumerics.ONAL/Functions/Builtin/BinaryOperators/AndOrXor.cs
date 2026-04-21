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

    #region HYCALPER LOOPSTART BINARY_OPERATOR_TEMPLATE@Functions\Builtin\BinaryOperators\EqualTo.cs

    /*!HC:TYPELIST:
    <hycalper>
    <type> 
        <source locate="here">
            <![CDATA[<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>]]>
        </source>
        <destination><![CDATA[<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>]]></destination>
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
        <destination><![CDATA[<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>]]></destination>
        <destination><![CDATA[<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>]]></destination>
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
        <destination><![CDATA[<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>]]></destination>
        <destination><![CDATA[<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>]]></destination>
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
        <destination><![CDATA[<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>]]></destination>
   </type>
    <type>
        <source locate="here"> 
            double
        </source>
        <destination>byte</destination>
        <destination>double</destination>
        <destination>float</destination>
        <destination>complex</destination>
        <destination>fcomplex</destination>
        <destination>sbyte</destination>
        <destination>System.Byte</destination>
        <destination>short</destination>
        <destination>ushort</destination>
        <destination>int</destination>
        <destination>uint</destination>
        <destination>long</destination>
        <destination>ulong</destination>
        <destination>byte</destination>
        <destination>double</destination>
        <destination>float</destination>
        <destination>complex</destination>
        <destination>fcomplex</destination>
        <destination>sbyte</destination>
        <destination>System.Byte</destination>
        <destination>short</destination>
        <destination>ushort</destination>
        <destination>int</destination>
        <destination>uint</destination>
        <destination>long</destination>
        <destination>ulong</destination>
        <destination>byte</destination>
        <destination>double</destination>
        <destination>float</destination>
        <destination>complex</destination>
        <destination>fcomplex</destination>
        <destination>sbyte</destination>
        <destination>System.Byte</destination>
        <destination>short</destination>
        <destination>ushort</destination>
        <destination>int</destination>
        <destination>uint</destination>
        <destination>long</destination>
        <destination>ulong</destination>
   </type>
    <type>
        <source locate="here"> 
            <![CDATA[BaseArray<byte>]]>
        </source>
        <destination><![CDATA[BaseArray<bool>]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
        <destination><![CDATA[BaseArray<bool>]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
        <destination><![CDATA[BaseArray<bool>]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
        <destination><![CDATA[NO MATCH]]></destination>
   </type>
    <type>
        <source locate="here">
            EqualTo
        </source>
        <destination>And</destination>
        <destination>And</destination>
        <destination>And</destination>
        <destination>And</destination>
        <destination>And</destination>
        <destination>And</destination>
        <destination>And</destination>
        <destination>And</destination>
        <destination>And</destination>
        <destination>And</destination>
        <destination>And</destination>
        <destination>And</destination>
        <destination>And</destination>
        <destination>Or</destination>
        <destination>Or</destination>
        <destination>Or</destination>
        <destination>Or</destination>
        <destination>Or</destination>
        <destination>Or</destination>
        <destination>Or</destination>
        <destination>Or</destination>
        <destination>Or</destination>
        <destination>Or</destination>
        <destination>Or</destination>
        <destination>Or</destination>
        <destination>Or</destination>
        <destination>Xor</destination>
        <destination>Xor</destination>
        <destination>Xor</destination>
        <destination>Xor</destination>
        <destination>Xor</destination>
        <destination>Xor</destination>
        <destination>Xor</destination>
        <destination>Xor</destination>
        <destination>Xor</destination>
        <destination>Xor</destination>
        <destination>Xor</destination>
        <destination>Xor</destination>
        <destination>Xor</destination>
   </type>
    <type>
        <source locate="after">
            funcname
        </source>
        <destination>and</destination>
        <destination>and</destination>
        <destination>and</destination>
        <destination>and</destination>
        <destination>and</destination>
        <destination>and</destination>
        <destination>and</destination>
        <destination>and</destination>
        <destination>and</destination>
        <destination>and</destination>
        <destination>and</destination>
        <destination>and</destination>
        <destination>and</destination>
        <destination>or</destination>
        <destination>or</destination>
        <destination>or</destination>
        <destination>or</destination>
        <destination>or</destination>
        <destination>or</destination>
        <destination>or</destination>
        <destination>or</destination>
        <destination>or</destination>
        <destination>or</destination>
        <destination>or</destination>
        <destination>or</destination>
        <destination>or</destination>
        <destination>xor</destination>
        <destination>xor</destination>
        <destination>xor</destination>
        <destination>xor</destination>
        <destination>xor</destination>
        <destination>xor</destination>
        <destination>xor</destination>
        <destination>xor</destination>
        <destination>xor</destination>
        <destination>xor</destination>
        <destination>xor</destination>
        <destination>xor</destination>
        <destination>xor</destination>
   </type>
    <type>
        <source locate="here">
            Double
        </source>
        <destination>Bool</destination>
        <destination>Double</destination>
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
        <destination>Bool</destination>
        <destination>Double</destination>
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
        <destination>Bool</destination>
        <destination>Double</destination>
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
    <type>
        <source locate="after" endmark=" (">
            HCoperator
        </source>
        <destination><![CDATA[!= 0 & 0 != ]]></destination>
        <destination><![CDATA[!= 0 & 0 != ]]></destination>
        <destination><![CDATA[!= 0 & 0 != ]]></destination>
        <destination><![CDATA[!= 0 & 0 != ]]></destination>
        <destination><![CDATA[!= 0 & 0 != ]]></destination>
        <destination><![CDATA[!= 0 & 0 != ]]></destination>
        <destination><![CDATA[!= 0 & 0 != ]]></destination>
        <destination><![CDATA[!= 0 & 0 != ]]></destination>
        <destination><![CDATA[!= 0 & 0 != ]]></destination>
        <destination><![CDATA[!= 0 & 0 != ]]></destination>
        <destination><![CDATA[!= 0 & 0 != ]]></destination>
        <destination><![CDATA[!= 0 & 0 != ]]></destination>
        <destination><![CDATA[!= 0 & 0 != ]]></destination>
        <destination><![CDATA[!= 0 | 0 != ]]></destination>
        <destination><![CDATA[!= 0 | 0 != ]]></destination>
        <destination><![CDATA[!= 0 | 0 != ]]></destination>
        <destination><![CDATA[!= 0 | 0 != ]]></destination>
        <destination><![CDATA[!= 0 | 0 != ]]></destination>
        <destination><![CDATA[!= 0 | 0 != ]]></destination>
        <destination><![CDATA[!= 0 | 0 != ]]></destination>
        <destination><![CDATA[!= 0 | 0 != ]]></destination>
        <destination><![CDATA[!= 0 | 0 != ]]></destination>
        <destination><![CDATA[!= 0 | 0 != ]]></destination>
        <destination><![CDATA[!= 0 | 0 != ]]></destination>
        <destination><![CDATA[!= 0 | 0 != ]]></destination>
        <destination><![CDATA[!= 0 | 0 != ]]></destination>
        <destination><![CDATA[!= 0 ^ 0 != ]]></destination>
        <destination><![CDATA[!= 0 ^ 0 != ]]></destination>
        <destination><![CDATA[!= 0 ^ 0 != ]]></destination>
        <destination><![CDATA[!= 0 ^ 0 != ]]></destination>
        <destination><![CDATA[!= 0 ^ 0 != ]]></destination>
        <destination><![CDATA[!= 0 ^ 0 != ]]></destination>
        <destination><![CDATA[!= 0 ^ 0 != ]]></destination>
        <destination><![CDATA[!= 0 ^ 0 != ]]></destination>
        <destination><![CDATA[!= 0 ^ 0 != ]]></destination>
        <destination><![CDATA[!= 0 ^ 0 != ]]></destination>
        <destination><![CDATA[!= 0 ^ 0 != ]]></destination>
        <destination><![CDATA[!= 0 ^ 0 != ]]></destination>
        <destination><![CDATA[!= 0 ^ 0 != ]]></destination>
   </type>
    </hycalper>
    */

    #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 

   

    internal static partial class MathInternal {

        /// <summary>
        /// Elementwise 'Xor' comparison operator on two arrays.
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
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="ulong"/> precision 
        /// floating point arithmetics and subsequently casting the result to the desired integer value. For <see cref="ArrayStyles.numpy"/> no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Logical xor(BaseArray<ulong> A, BaseArray<ulong> B) {
            return InnerLoops.Xor.UInt64.Instance.operate(
                A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>, 
                B as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>);
        }
    }

    namespace InnerLoops.Xor {

        internal unsafe class UInt64
            : BroadcastingLogicalBase<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> {

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
                    if (*(ulong*)pA  != 0 ^ 0 !=  *(ulong*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(ulong*)pA  != 0 ^ 0 !=  *(ulong*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'Xor' comparison operator on two arrays.
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
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="long"/> precision 
        /// floating point arithmetics and subsequently casting the result to the desired integer value. For <see cref="ArrayStyles.numpy"/> no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Logical xor(BaseArray<long> A, BaseArray<long> B) {
            return InnerLoops.Xor.Int64.Instance.operate(
                A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, 
                B as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>);
        }
    }

    namespace InnerLoops.Xor {

        internal unsafe class Int64
            : BroadcastingLogicalBase<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> {

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
                    if (*(long*)pA  != 0 ^ 0 !=  *(long*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(long*)pA  != 0 ^ 0 !=  *(long*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'Xor' comparison operator on two arrays.
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
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="uint"/> precision 
        /// floating point arithmetics and subsequently casting the result to the desired integer value. For <see cref="ArrayStyles.numpy"/> no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Logical xor(BaseArray<uint> A, BaseArray<uint> B) {
            return InnerLoops.Xor.UInt32.Instance.operate(
                A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>, 
                B as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>);
        }
    }

    namespace InnerLoops.Xor {

        internal unsafe class UInt32
            : BroadcastingLogicalBase<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> {

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
                    if (*(uint*)pA  != 0 ^ 0 !=  *(uint*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(uint*)pA  != 0 ^ 0 !=  *(uint*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'Xor' comparison operator on two arrays.
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
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="int"/> precision 
        /// floating point arithmetics and subsequently casting the result to the desired integer value. For <see cref="ArrayStyles.numpy"/> no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Logical xor(BaseArray<int> A, BaseArray<int> B) {
            return InnerLoops.Xor.Int32.Instance.operate(
                A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>, 
                B as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>);
        }
    }

    namespace InnerLoops.Xor {

        internal unsafe class Int32
            : BroadcastingLogicalBase<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> {

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
                    if (*(int*)pA  != 0 ^ 0 !=  *(int*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(int*)pA  != 0 ^ 0 !=  *(int*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'Xor' comparison operator on two arrays.
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
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="ushort"/> precision 
        /// floating point arithmetics and subsequently casting the result to the desired integer value. For <see cref="ArrayStyles.numpy"/> no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Logical xor(BaseArray<ushort> A, BaseArray<ushort> B) {
            return InnerLoops.Xor.UInt16.Instance.operate(
                A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>, 
                B as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>);
        }
    }

    namespace InnerLoops.Xor {

        internal unsafe class UInt16
            : BroadcastingLogicalBase<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> {

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
                    if (*(ushort*)pA  != 0 ^ 0 !=  *(ushort*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(ushort*)pA  != 0 ^ 0 !=  *(ushort*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'Xor' comparison operator on two arrays.
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
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="short"/> precision 
        /// floating point arithmetics and subsequently casting the result to the desired integer value. For <see cref="ArrayStyles.numpy"/> no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Logical xor(BaseArray<short> A, BaseArray<short> B) {
            return InnerLoops.Xor.Int16.Instance.operate(
                A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>, 
                B as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>);
        }
    }

    namespace InnerLoops.Xor {

        internal unsafe class Int16
            : BroadcastingLogicalBase<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> {

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
                    if (*(short*)pA  != 0 ^ 0 !=  *(short*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(short*)pA  != 0 ^ 0 !=  *(short*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'Xor' comparison operator on two arrays.
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
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="System.Byte"/> precision 
        /// floating point arithmetics and subsequently casting the result to the desired integer value. For <see cref="ArrayStyles.numpy"/> no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Logical xor(BaseArray<System.Byte> A, BaseArray<System.Byte> B) {
            return InnerLoops.Xor.Byte.Instance.operate(
                A as ConcreteArray<System.Byte, Array<System.Byte>, InArray<System.Byte>, OutArray<System.Byte>, Array<System.Byte>, Storage<System.Byte>>, 
                B as ConcreteArray<System.Byte, Array<System.Byte>, InArray<System.Byte>, OutArray<System.Byte>, Array<System.Byte>, Storage<System.Byte>>);
        }
    }

    namespace InnerLoops.Xor {

        internal unsafe class Byte
            : BroadcastingLogicalBase<System.Byte, Array<System.Byte>, InArray<System.Byte>, OutArray<System.Byte>, Array<System.Byte>, Storage<System.Byte>> {

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
                    if (*(System.Byte*)pA  != 0 ^ 0 !=  *(System.Byte*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(System.Byte*)pA  != 0 ^ 0 !=  *(System.Byte*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'Xor' comparison operator on two arrays.
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
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="sbyte"/> precision 
        /// floating point arithmetics and subsequently casting the result to the desired integer value. For <see cref="ArrayStyles.numpy"/> no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Logical xor(BaseArray<sbyte> A, BaseArray<sbyte> B) {
            return InnerLoops.Xor.SByte.Instance.operate(
                A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>, 
                B as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>);
        }
    }

    namespace InnerLoops.Xor {

        internal unsafe class SByte
            : BroadcastingLogicalBase<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> {

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
                    if (*(sbyte*)pA  != 0 ^ 0 !=  *(sbyte*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(sbyte*)pA  != 0 ^ 0 !=  *(sbyte*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'Xor' comparison operator on two arrays.
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
        internal static Logical xor(BaseArray<fcomplex> A, BaseArray<fcomplex> B) {
            return InnerLoops.Xor.FComplex.Instance.operate(
                A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, 
                B as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>);
        }
    }

    namespace InnerLoops.Xor {

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
                    if (*(fcomplex*)pA  != 0 ^ 0 !=  *(fcomplex*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(fcomplex*)pA  != 0 ^ 0 !=  *(fcomplex*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'Xor' comparison operator on two arrays.
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
        internal static Logical xor(BaseArray<complex> A, BaseArray<complex> B) {
            return InnerLoops.Xor.Complex.Instance.operate(
                A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, 
                B as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>);
        }
    }

    namespace InnerLoops.Xor {

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
                    if (*(complex*)pA  != 0 ^ 0 !=  *(complex*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(complex*)pA  != 0 ^ 0 !=  *(complex*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'Xor' comparison operator on two arrays.
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
        internal static Logical xor(BaseArray<float> A, BaseArray<float> B) {
            return InnerLoops.Xor.Single.Instance.operate(
                A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, 
                B as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>);
        }
    }

    namespace InnerLoops.Xor {

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
                    if (*(float*)pA  != 0 ^ 0 !=  *(float*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(float*)pA  != 0 ^ 0 !=  *(float*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'Xor' comparison operator on two arrays.
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
        internal static Logical xor(BaseArray<double> A, BaseArray<double> B) {
            return InnerLoops.Xor.Double.Instance.operate(
                A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, 
                B as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>);
        }
    }

    namespace InnerLoops.Xor {

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
                    if (*(double*)pA  != 0 ^ 0 !=  *(double*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(double*)pA  != 0 ^ 0 !=  *(double*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'Xor' comparison operator on two arrays.
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
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="byte"/> precision 
        /// floating point arithmetics and subsequently casting the result to the desired integer value. For <see cref="ArrayStyles.numpy"/> no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Logical xor(BaseArray<bool> A, BaseArray<bool> B) {
            return InnerLoops.Xor.Bool.Instance.operate(
                A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>, 
                B as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>);
        }
    }

    namespace InnerLoops.Xor {

        internal unsafe class Bool
            : BroadcastingLogicalBase<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage> {

            internal static Bool Instance = new Bool();

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
                    if (*(byte*)pA  != 0 ^ 0 !=  *(byte*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(byte*)pA  != 0 ^ 0 !=  *(byte*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'Or' comparison operator on two arrays.
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
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="ulong"/> precision 
        /// floating point arithmetics and subsequently casting the result to the desired integer value. For <see cref="ArrayStyles.numpy"/> no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Logical or(BaseArray<ulong> A, BaseArray<ulong> B) {
            return InnerLoops.Or.UInt64.Instance.operate(
                A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>, 
                B as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>);
        }
    }

    namespace InnerLoops.Or {

        internal unsafe class UInt64
            : BroadcastingLogicalBase<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> {

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
                    if (*(ulong*)pA  != 0 | 0 !=  *(ulong*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(ulong*)pA  != 0 | 0 !=  *(ulong*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'Or' comparison operator on two arrays.
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
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="long"/> precision 
        /// floating point arithmetics and subsequently casting the result to the desired integer value. For <see cref="ArrayStyles.numpy"/> no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Logical or(BaseArray<long> A, BaseArray<long> B) {
            return InnerLoops.Or.Int64.Instance.operate(
                A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, 
                B as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>);
        }
    }

    namespace InnerLoops.Or {

        internal unsafe class Int64
            : BroadcastingLogicalBase<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> {

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
                    if (*(long*)pA  != 0 | 0 !=  *(long*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(long*)pA  != 0 | 0 !=  *(long*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'Or' comparison operator on two arrays.
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
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="uint"/> precision 
        /// floating point arithmetics and subsequently casting the result to the desired integer value. For <see cref="ArrayStyles.numpy"/> no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Logical or(BaseArray<uint> A, BaseArray<uint> B) {
            return InnerLoops.Or.UInt32.Instance.operate(
                A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>, 
                B as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>);
        }
    }

    namespace InnerLoops.Or {

        internal unsafe class UInt32
            : BroadcastingLogicalBase<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> {

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
                    if (*(uint*)pA  != 0 | 0 !=  *(uint*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(uint*)pA  != 0 | 0 !=  *(uint*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'Or' comparison operator on two arrays.
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
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="int"/> precision 
        /// floating point arithmetics and subsequently casting the result to the desired integer value. For <see cref="ArrayStyles.numpy"/> no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Logical or(BaseArray<int> A, BaseArray<int> B) {
            return InnerLoops.Or.Int32.Instance.operate(
                A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>, 
                B as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>);
        }
    }

    namespace InnerLoops.Or {

        internal unsafe class Int32
            : BroadcastingLogicalBase<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> {

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
                    if (*(int*)pA  != 0 | 0 !=  *(int*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(int*)pA  != 0 | 0 !=  *(int*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'Or' comparison operator on two arrays.
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
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="ushort"/> precision 
        /// floating point arithmetics and subsequently casting the result to the desired integer value. For <see cref="ArrayStyles.numpy"/> no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Logical or(BaseArray<ushort> A, BaseArray<ushort> B) {
            return InnerLoops.Or.UInt16.Instance.operate(
                A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>, 
                B as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>);
        }
    }

    namespace InnerLoops.Or {

        internal unsafe class UInt16
            : BroadcastingLogicalBase<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> {

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
                    if (*(ushort*)pA  != 0 | 0 !=  *(ushort*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(ushort*)pA  != 0 | 0 !=  *(ushort*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'Or' comparison operator on two arrays.
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
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="short"/> precision 
        /// floating point arithmetics and subsequently casting the result to the desired integer value. For <see cref="ArrayStyles.numpy"/> no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Logical or(BaseArray<short> A, BaseArray<short> B) {
            return InnerLoops.Or.Int16.Instance.operate(
                A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>, 
                B as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>);
        }
    }

    namespace InnerLoops.Or {

        internal unsafe class Int16
            : BroadcastingLogicalBase<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> {

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
                    if (*(short*)pA  != 0 | 0 !=  *(short*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(short*)pA  != 0 | 0 !=  *(short*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'Or' comparison operator on two arrays.
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
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="System.Byte"/> precision 
        /// floating point arithmetics and subsequently casting the result to the desired integer value. For <see cref="ArrayStyles.numpy"/> no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Logical or(BaseArray<System.Byte> A, BaseArray<System.Byte> B) {
            return InnerLoops.Or.Byte.Instance.operate(
                A as ConcreteArray<System.Byte, Array<System.Byte>, InArray<System.Byte>, OutArray<System.Byte>, Array<System.Byte>, Storage<System.Byte>>, 
                B as ConcreteArray<System.Byte, Array<System.Byte>, InArray<System.Byte>, OutArray<System.Byte>, Array<System.Byte>, Storage<System.Byte>>);
        }
    }

    namespace InnerLoops.Or {

        internal unsafe class Byte
            : BroadcastingLogicalBase<System.Byte, Array<System.Byte>, InArray<System.Byte>, OutArray<System.Byte>, Array<System.Byte>, Storage<System.Byte>> {

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
                    if (*(System.Byte*)pA  != 0 | 0 !=  *(System.Byte*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(System.Byte*)pA  != 0 | 0 !=  *(System.Byte*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'Or' comparison operator on two arrays.
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
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="sbyte"/> precision 
        /// floating point arithmetics and subsequently casting the result to the desired integer value. For <see cref="ArrayStyles.numpy"/> no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Logical or(BaseArray<sbyte> A, BaseArray<sbyte> B) {
            return InnerLoops.Or.SByte.Instance.operate(
                A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>, 
                B as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>);
        }
    }

    namespace InnerLoops.Or {

        internal unsafe class SByte
            : BroadcastingLogicalBase<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> {

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
                    if (*(sbyte*)pA  != 0 | 0 !=  *(sbyte*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(sbyte*)pA  != 0 | 0 !=  *(sbyte*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'Or' comparison operator on two arrays.
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
        internal static Logical or(BaseArray<fcomplex> A, BaseArray<fcomplex> B) {
            return InnerLoops.Or.FComplex.Instance.operate(
                A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, 
                B as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>);
        }
    }

    namespace InnerLoops.Or {

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
                    if (*(fcomplex*)pA  != 0 | 0 !=  *(fcomplex*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(fcomplex*)pA  != 0 | 0 !=  *(fcomplex*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'Or' comparison operator on two arrays.
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
        internal static Logical or(BaseArray<complex> A, BaseArray<complex> B) {
            return InnerLoops.Or.Complex.Instance.operate(
                A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, 
                B as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>);
        }
    }

    namespace InnerLoops.Or {

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
                    if (*(complex*)pA  != 0 | 0 !=  *(complex*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(complex*)pA  != 0 | 0 !=  *(complex*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'Or' comparison operator on two arrays.
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
        internal static Logical or(BaseArray<float> A, BaseArray<float> B) {
            return InnerLoops.Or.Single.Instance.operate(
                A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, 
                B as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>);
        }
    }

    namespace InnerLoops.Or {

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
                    if (*(float*)pA  != 0 | 0 !=  *(float*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(float*)pA  != 0 | 0 !=  *(float*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'Or' comparison operator on two arrays.
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
        internal static Logical or(BaseArray<double> A, BaseArray<double> B) {
            return InnerLoops.Or.Double.Instance.operate(
                A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, 
                B as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>);
        }
    }

    namespace InnerLoops.Or {

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
                    if (*(double*)pA  != 0 | 0 !=  *(double*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(double*)pA  != 0 | 0 !=  *(double*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'Or' comparison operator on two arrays.
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
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="byte"/> precision 
        /// floating point arithmetics and subsequently casting the result to the desired integer value. For <see cref="ArrayStyles.numpy"/> no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Logical or(BaseArray<bool> A, BaseArray<bool> B) {
            return InnerLoops.Or.Bool.Instance.operate(
                A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>, 
                B as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>);
        }
    }

    namespace InnerLoops.Or {

        internal unsafe class Bool
            : BroadcastingLogicalBase<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage> {

            internal static Bool Instance = new Bool();

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
                    if (*(byte*)pA  != 0 | 0 !=  *(byte*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(byte*)pA  != 0 | 0 !=  *(byte*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'And' comparison operator on two arrays.
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
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="ulong"/> precision 
        /// floating point arithmetics and subsequently casting the result to the desired integer value. For <see cref="ArrayStyles.numpy"/> no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Logical and(BaseArray<ulong> A, BaseArray<ulong> B) {
            return InnerLoops.And.UInt64.Instance.operate(
                A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>, 
                B as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>);
        }
    }

    namespace InnerLoops.And {

        internal unsafe class UInt64
            : BroadcastingLogicalBase<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> {

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
                    if (*(ulong*)pA  != 0 & 0 !=  *(ulong*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(ulong*)pA  != 0 & 0 !=  *(ulong*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'And' comparison operator on two arrays.
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
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="long"/> precision 
        /// floating point arithmetics and subsequently casting the result to the desired integer value. For <see cref="ArrayStyles.numpy"/> no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Logical and(BaseArray<long> A, BaseArray<long> B) {
            return InnerLoops.And.Int64.Instance.operate(
                A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, 
                B as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>);
        }
    }

    namespace InnerLoops.And {

        internal unsafe class Int64
            : BroadcastingLogicalBase<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> {

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
                    if (*(long*)pA  != 0 & 0 !=  *(long*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(long*)pA  != 0 & 0 !=  *(long*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'And' comparison operator on two arrays.
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
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="uint"/> precision 
        /// floating point arithmetics and subsequently casting the result to the desired integer value. For <see cref="ArrayStyles.numpy"/> no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Logical and(BaseArray<uint> A, BaseArray<uint> B) {
            return InnerLoops.And.UInt32.Instance.operate(
                A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>, 
                B as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>);
        }
    }

    namespace InnerLoops.And {

        internal unsafe class UInt32
            : BroadcastingLogicalBase<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> {

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
                    if (*(uint*)pA  != 0 & 0 !=  *(uint*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(uint*)pA  != 0 & 0 !=  *(uint*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'And' comparison operator on two arrays.
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
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="int"/> precision 
        /// floating point arithmetics and subsequently casting the result to the desired integer value. For <see cref="ArrayStyles.numpy"/> no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Logical and(BaseArray<int> A, BaseArray<int> B) {
            return InnerLoops.And.Int32.Instance.operate(
                A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>, 
                B as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>);
        }
    }

    namespace InnerLoops.And {

        internal unsafe class Int32
            : BroadcastingLogicalBase<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> {

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
                    if (*(int*)pA  != 0 & 0 !=  *(int*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(int*)pA  != 0 & 0 !=  *(int*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'And' comparison operator on two arrays.
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
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="ushort"/> precision 
        /// floating point arithmetics and subsequently casting the result to the desired integer value. For <see cref="ArrayStyles.numpy"/> no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Logical and(BaseArray<ushort> A, BaseArray<ushort> B) {
            return InnerLoops.And.UInt16.Instance.operate(
                A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>, 
                B as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>);
        }
    }

    namespace InnerLoops.And {

        internal unsafe class UInt16
            : BroadcastingLogicalBase<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> {

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
                    if (*(ushort*)pA  != 0 & 0 !=  *(ushort*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(ushort*)pA  != 0 & 0 !=  *(ushort*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'And' comparison operator on two arrays.
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
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="short"/> precision 
        /// floating point arithmetics and subsequently casting the result to the desired integer value. For <see cref="ArrayStyles.numpy"/> no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Logical and(BaseArray<short> A, BaseArray<short> B) {
            return InnerLoops.And.Int16.Instance.operate(
                A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>, 
                B as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>);
        }
    }

    namespace InnerLoops.And {

        internal unsafe class Int16
            : BroadcastingLogicalBase<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> {

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
                    if (*(short*)pA  != 0 & 0 !=  *(short*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(short*)pA  != 0 & 0 !=  *(short*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'And' comparison operator on two arrays.
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
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="System.Byte"/> precision 
        /// floating point arithmetics and subsequently casting the result to the desired integer value. For <see cref="ArrayStyles.numpy"/> no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Logical and(BaseArray<System.Byte> A, BaseArray<System.Byte> B) {
            return InnerLoops.And.Byte.Instance.operate(
                A as ConcreteArray<System.Byte, Array<System.Byte>, InArray<System.Byte>, OutArray<System.Byte>, Array<System.Byte>, Storage<System.Byte>>, 
                B as ConcreteArray<System.Byte, Array<System.Byte>, InArray<System.Byte>, OutArray<System.Byte>, Array<System.Byte>, Storage<System.Byte>>);
        }
    }

    namespace InnerLoops.And {

        internal unsafe class Byte
            : BroadcastingLogicalBase<System.Byte, Array<System.Byte>, InArray<System.Byte>, OutArray<System.Byte>, Array<System.Byte>, Storage<System.Byte>> {

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
                    if (*(System.Byte*)pA  != 0 & 0 !=  *(System.Byte*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(System.Byte*)pA  != 0 & 0 !=  *(System.Byte*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'And' comparison operator on two arrays.
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
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="sbyte"/> precision 
        /// floating point arithmetics and subsequently casting the result to the desired integer value. For <see cref="ArrayStyles.numpy"/> no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Logical and(BaseArray<sbyte> A, BaseArray<sbyte> B) {
            return InnerLoops.And.SByte.Instance.operate(
                A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>, 
                B as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>);
        }
    }

    namespace InnerLoops.And {

        internal unsafe class SByte
            : BroadcastingLogicalBase<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> {

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
                    if (*(sbyte*)pA  != 0 & 0 !=  *(sbyte*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(sbyte*)pA  != 0 & 0 !=  *(sbyte*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'And' comparison operator on two arrays.
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
        internal static Logical and(BaseArray<fcomplex> A, BaseArray<fcomplex> B) {
            return InnerLoops.And.FComplex.Instance.operate(
                A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, 
                B as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>);
        }
    }

    namespace InnerLoops.And {

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
                    if (*(fcomplex*)pA  != 0 & 0 !=  *(fcomplex*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(fcomplex*)pA  != 0 & 0 !=  *(fcomplex*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'And' comparison operator on two arrays.
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
        internal static Logical and(BaseArray<complex> A, BaseArray<complex> B) {
            return InnerLoops.And.Complex.Instance.operate(
                A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, 
                B as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>);
        }
    }

    namespace InnerLoops.And {

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
                    if (*(complex*)pA  != 0 & 0 !=  *(complex*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(complex*)pA  != 0 & 0 !=  *(complex*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'And' comparison operator on two arrays.
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
        internal static Logical and(BaseArray<float> A, BaseArray<float> B) {
            return InnerLoops.And.Single.Instance.operate(
                A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, 
                B as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>);
        }
    }

    namespace InnerLoops.And {

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
                    if (*(float*)pA  != 0 & 0 !=  *(float*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(float*)pA  != 0 & 0 !=  *(float*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'And' comparison operator on two arrays.
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
        internal static Logical and(BaseArray<double> A, BaseArray<double> B) {
            return InnerLoops.And.Double.Instance.operate(
                A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, 
                B as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>);
        }
    }

    namespace InnerLoops.And {

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
                    if (*(double*)pA  != 0 & 0 !=  *(double*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(double*)pA  != 0 & 0 !=  *(double*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'And' comparison operator on two arrays.
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
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="byte"/> precision 
        /// floating point arithmetics and subsequently casting the result to the desired integer value. For <see cref="ArrayStyles.numpy"/> no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        internal static Logical and(BaseArray<bool> A, BaseArray<bool> B) {
            return InnerLoops.And.Bool.Instance.operate(
                A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>, 
                B as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>);
        }
    }

    namespace InnerLoops.And {

        internal unsafe class Bool
            : BroadcastingLogicalBase<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage> {

            internal static Bool Instance = new Bool();

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
                    if (*(byte*)pA  != 0 & 0 !=  *(byte*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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

                    while (leadLen-- > 0) {
                        if (*(byte*)pA  != 0 & 0 !=  *(byte*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
