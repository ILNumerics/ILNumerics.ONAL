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
        <destination><![CDATA[<double,Array<double>,InArray<double>,OutArray<double>,Array<double>,Storage<double>>]]></destination>
        <destination><![CDATA[<double,Array<double>,InArray<double>,OutArray<double>,Array<double>,Storage<double>>]]></destination>
        <destination><![CDATA[<double,Array<double>,InArray<double>,OutArray<double>,Array<double>,Storage<double>>]]></destination>
        <destination><![CDATA[<double,Array<double>,InArray<double>,OutArray<double>,Array<double>,Storage<double>>]]></destination>
        <destination><![CDATA[<double,Array<double>,InArray<double>,OutArray<double>,Array<double>,Storage<double>>]]></destination>
        <destination><![CDATA[<double,Array<double>,InArray<double>,OutArray<double>,Array<double>,Storage<double>>]]></destination>
        <destination><![CDATA[<double,Array<double>,InArray<double>,OutArray<double>,Array<double>,Storage<double>>]]></destination>
        <destination><![CDATA[<double,Array<double>,InArray<double>,OutArray<double>,Array<double>,Storage<double>>]]></destination>
        <destination><![CDATA[<double,Array<double>,InArray<double>,OutArray<double>,Array<double>,Storage<double>>]]></destination>
        <destination><![CDATA[<double,Array<double>,InArray<double>,OutArray<double>,Array<double>,Storage<double>>]]></destination>
        <destination><![CDATA[<double,Array<double>,InArray<double>,OutArray<double>,Array<double>,Storage<double>>]]></destination>
        <destination><![CDATA[<double,Array<double>,InArray<double>,OutArray<double>,Array<double>,Storage<double>>]]></destination>
    </type>
    <type>
        <source locate="after">
            funcname
        </source>
        <destination>le</destination>
        <destination>le</destination>
        <destination>le</destination>
        <destination>le</destination>
        <destination>le</destination>
        <destination>le</destination>
        <destination>le</destination>
        <destination>le</destination>
        <destination>le</destination>
        <destination>le</destination>
        <destination>le</destination>
        <destination>le</destination>
   </type>
    <type>
        <source locate="here">
            EqualTo
        </source>
        <destination>LowerOrEqual</destination>
        <destination>LowerOrEqual</destination>
        <destination>LowerOrEqual</destination>
        <destination>LowerOrEqual</destination>
        <destination>LowerOrEqual</destination>
        <destination>LowerOrEqual</destination>
        <destination>LowerOrEqual</destination>
        <destination>LowerOrEqual</destination>
        <destination>LowerOrEqual</destination>
        <destination>LowerOrEqual</destination>
        <destination>LowerOrEqual</destination>
        <destination>LowerOrEqual</destination>
   </type>
    <type>
        <source locate="here">
            double
        </source>
        <destination>double</destination>
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
        <destination><![CDATA[<=]]></destination>
        <destination><![CDATA[<=]]></destination>
        <destination><![CDATA[<=]]></destination>
        <destination><![CDATA[<=]]></destination>
        <destination><![CDATA[<=]]></destination>
        <destination><![CDATA[<=]]></destination>
        <destination><![CDATA[<=]]></destination>
        <destination><![CDATA[<=]]></destination>
        <destination><![CDATA[<=]]></destination>
        <destination><![CDATA[<=]]></destination>
        <destination><![CDATA[<=]]></destination>
        <destination><![CDATA[<=]]></destination>
   </type>
    </hycalper>
    */

    #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 

   

    internal static partial class MathInternal {

        /// <summary>
        /// Elementwise 'LowerOrEqual' comparison operator on two arrays.
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
        internal static Logical le(BaseArray<ulong> A, BaseArray<ulong> B) {
            return InnerLoops.LowerOrEqual.UInt64.Instance.operate(
                A as ConcreteArray<ulong,Array<ulong>,InArray<ulong>,OutArray<ulong>,Array<ulong>,Storage<ulong>>, 
                B as ConcreteArray<ulong,Array<ulong>,InArray<ulong>,OutArray<ulong>,Array<ulong>,Storage<ulong>>);
        }
    }

    namespace InnerLoops.LowerOrEqual {

        internal unsafe class UInt64
            : BroadcastingLogicalBase<ulong,Array<ulong>,InArray<ulong>,OutArray<ulong>,Array<ulong>,Storage<ulong>> {

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
                    if (*(ulong*)pA  <= *(ulong*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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
                        if (*(ulong*)pA  <= *(ulong*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'LowerOrEqual' comparison operator on two arrays.
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
        internal static Logical le(BaseArray<long> A, BaseArray<long> B) {
            return InnerLoops.LowerOrEqual.Int64.Instance.operate(
                A as ConcreteArray<long,Array<long>,InArray<long>,OutArray<long>,Array<long>,Storage<long>>, 
                B as ConcreteArray<long,Array<long>,InArray<long>,OutArray<long>,Array<long>,Storage<long>>);
        }
    }

    namespace InnerLoops.LowerOrEqual {

        internal unsafe class Int64
            : BroadcastingLogicalBase<long,Array<long>,InArray<long>,OutArray<long>,Array<long>,Storage<long>> {

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
                    if (*(long*)pA  <= *(long*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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
                        if (*(long*)pA  <= *(long*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'LowerOrEqual' comparison operator on two arrays.
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
        internal static Logical le(BaseArray<uint> A, BaseArray<uint> B) {
            return InnerLoops.LowerOrEqual.UInt32.Instance.operate(
                A as ConcreteArray<uint,Array<uint>,InArray<uint>,OutArray<uint>,Array<uint>,Storage<uint>>, 
                B as ConcreteArray<uint,Array<uint>,InArray<uint>,OutArray<uint>,Array<uint>,Storage<uint>>);
        }
    }

    namespace InnerLoops.LowerOrEqual {

        internal unsafe class UInt32
            : BroadcastingLogicalBase<uint,Array<uint>,InArray<uint>,OutArray<uint>,Array<uint>,Storage<uint>> {

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
                    if (*(uint*)pA  <= *(uint*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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
                        if (*(uint*)pA  <= *(uint*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'LowerOrEqual' comparison operator on two arrays.
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
        internal static Logical le(BaseArray<int> A, BaseArray<int> B) {
            return InnerLoops.LowerOrEqual.Int32.Instance.operate(
                A as ConcreteArray<int,Array<int>,InArray<int>,OutArray<int>,Array<int>,Storage<int>>, 
                B as ConcreteArray<int,Array<int>,InArray<int>,OutArray<int>,Array<int>,Storage<int>>);
        }
    }

    namespace InnerLoops.LowerOrEqual {

        internal unsafe class Int32
            : BroadcastingLogicalBase<int,Array<int>,InArray<int>,OutArray<int>,Array<int>,Storage<int>> {

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
                    if (*(int*)pA  <= *(int*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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
                        if (*(int*)pA  <= *(int*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'LowerOrEqual' comparison operator on two arrays.
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
        internal static Logical le(BaseArray<ushort> A, BaseArray<ushort> B) {
            return InnerLoops.LowerOrEqual.UInt16.Instance.operate(
                A as ConcreteArray<ushort,Array<ushort>,InArray<ushort>,OutArray<ushort>,Array<ushort>,Storage<ushort>>, 
                B as ConcreteArray<ushort,Array<ushort>,InArray<ushort>,OutArray<ushort>,Array<ushort>,Storage<ushort>>);
        }
    }

    namespace InnerLoops.LowerOrEqual {

        internal unsafe class UInt16
            : BroadcastingLogicalBase<ushort,Array<ushort>,InArray<ushort>,OutArray<ushort>,Array<ushort>,Storage<ushort>> {

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
                    if (*(ushort*)pA  <= *(ushort*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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
                        if (*(ushort*)pA  <= *(ushort*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'LowerOrEqual' comparison operator on two arrays.
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
        internal static Logical le(BaseArray<short> A, BaseArray<short> B) {
            return InnerLoops.LowerOrEqual.Int16.Instance.operate(
                A as ConcreteArray<short,Array<short>,InArray<short>,OutArray<short>,Array<short>,Storage<short>>, 
                B as ConcreteArray<short,Array<short>,InArray<short>,OutArray<short>,Array<short>,Storage<short>>);
        }
    }

    namespace InnerLoops.LowerOrEqual {

        internal unsafe class Int16
            : BroadcastingLogicalBase<short,Array<short>,InArray<short>,OutArray<short>,Array<short>,Storage<short>> {

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
                    if (*(short*)pA  <= *(short*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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
                        if (*(short*)pA  <= *(short*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'LowerOrEqual' comparison operator on two arrays.
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
        internal static Logical le(BaseArray<byte> A, BaseArray<byte> B) {
            return InnerLoops.LowerOrEqual.Byte.Instance.operate(
                A as ConcreteArray<byte,Array<byte>,InArray<byte>,OutArray<byte>,Array<byte>,Storage<byte>>, 
                B as ConcreteArray<byte,Array<byte>,InArray<byte>,OutArray<byte>,Array<byte>,Storage<byte>>);
        }
    }

    namespace InnerLoops.LowerOrEqual {

        internal unsafe class Byte
            : BroadcastingLogicalBase<byte,Array<byte>,InArray<byte>,OutArray<byte>,Array<byte>,Storage<byte>> {

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
                    if (*(byte*)pA  <= *(byte*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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
                        if (*(byte*)pA  <= *(byte*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'LowerOrEqual' comparison operator on two arrays.
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
        internal static Logical le(BaseArray<sbyte> A, BaseArray<sbyte> B) {
            return InnerLoops.LowerOrEqual.SByte.Instance.operate(
                A as ConcreteArray<sbyte,Array<sbyte>,InArray<sbyte>,OutArray<sbyte>,Array<sbyte>,Storage<sbyte>>, 
                B as ConcreteArray<sbyte,Array<sbyte>,InArray<sbyte>,OutArray<sbyte>,Array<sbyte>,Storage<sbyte>>);
        }
    }

    namespace InnerLoops.LowerOrEqual {

        internal unsafe class SByte
            : BroadcastingLogicalBase<sbyte,Array<sbyte>,InArray<sbyte>,OutArray<sbyte>,Array<sbyte>,Storage<sbyte>> {

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
                    if (*(sbyte*)pA  <= *(sbyte*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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
                        if (*(sbyte*)pA  <= *(sbyte*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'LowerOrEqual' comparison operator on two arrays.
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
        internal static Logical le(BaseArray<fcomplex> A, BaseArray<fcomplex> B) {
            return InnerLoops.LowerOrEqual.FComplex.Instance.operate(
                A as ConcreteArray<fcomplex,Array<fcomplex>,InArray<fcomplex>,OutArray<fcomplex>,Array<fcomplex>,Storage<fcomplex>>, 
                B as ConcreteArray<fcomplex,Array<fcomplex>,InArray<fcomplex>,OutArray<fcomplex>,Array<fcomplex>,Storage<fcomplex>>);
        }
    }

    namespace InnerLoops.LowerOrEqual {

        internal unsafe class FComplex
            : BroadcastingLogicalBase<fcomplex,Array<fcomplex>,InArray<fcomplex>,OutArray<fcomplex>,Array<fcomplex>,Storage<fcomplex>> {

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
                    if (*(fcomplex*)pA  <= *(fcomplex*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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
                        if (*(fcomplex*)pA  <= *(fcomplex*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'LowerOrEqual' comparison operator on two arrays.
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
        internal static Logical le(BaseArray<complex> A, BaseArray<complex> B) {
            return InnerLoops.LowerOrEqual.Complex.Instance.operate(
                A as ConcreteArray<complex,Array<complex>,InArray<complex>,OutArray<complex>,Array<complex>,Storage<complex>>, 
                B as ConcreteArray<complex,Array<complex>,InArray<complex>,OutArray<complex>,Array<complex>,Storage<complex>>);
        }
    }

    namespace InnerLoops.LowerOrEqual {

        internal unsafe class Complex
            : BroadcastingLogicalBase<complex,Array<complex>,InArray<complex>,OutArray<complex>,Array<complex>,Storage<complex>> {

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
                    if (*(complex*)pA  <= *(complex*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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
                        if (*(complex*)pA  <= *(complex*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'LowerOrEqual' comparison operator on two arrays.
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
        internal static Logical le(BaseArray<float> A, BaseArray<float> B) {
            return InnerLoops.LowerOrEqual.Single.Instance.operate(
                A as ConcreteArray<float,Array<float>,InArray<float>,OutArray<float>,Array<float>,Storage<float>>, 
                B as ConcreteArray<float,Array<float>,InArray<float>,OutArray<float>,Array<float>,Storage<float>>);
        }
    }

    namespace InnerLoops.LowerOrEqual {

        internal unsafe class Single
            : BroadcastingLogicalBase<float,Array<float>,InArray<float>,OutArray<float>,Array<float>,Storage<float>> {

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
                    if (*(float*)pA  <= *(float*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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
                        if (*(float*)pA  <= *(float*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
        /// Elementwise 'LowerOrEqual' comparison operator on two arrays.
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
        internal static Logical le(BaseArray<double> A, BaseArray<double> B) {
            return InnerLoops.LowerOrEqual.Double.Instance.operate(
                A as ConcreteArray<double,Array<double>,InArray<double>,OutArray<double>,Array<double>,Storage<double>>, 
                B as ConcreteArray<double,Array<double>,InArray<double>,OutArray<double>,Array<double>,Storage<double>>);
        }
    }

    namespace InnerLoops.LowerOrEqual {

        internal unsafe class Double
            : BroadcastingLogicalBase<double,Array<double>,InArray<double>,OutArray<double>,Array<double>,Storage<double>> {

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
                    if (*(double*)pA  <= *(double*)pB) { numberTrues[0] = *(pOut + start) = (byte)1; } else { numberTrues[0] = *(pOut + start) = (byte)0; }
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
                        if (*(double*)pA  <= *(double*)pB) { myNumTrues += *pOut = 1; } else { *pOut = 0; }
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
