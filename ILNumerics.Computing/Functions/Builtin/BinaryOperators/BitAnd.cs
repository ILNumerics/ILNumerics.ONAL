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

    #region HYCALPER LOOPSTART BINARY_OPERATOR_TEMPLATE@Functions\Builtin\BinaryOperators\Add.cs

    /*!HC:TYPELIST:
    <hycalper>
    <type>
        <source locate="here">
            Add
        </source>
        <destination>BitAnd</destination>
        <destination>BitAnd</destination>
        <destination>BitAnd</destination>
        <destination>BitAnd</destination>
        <destination>BitAnd</destination>
        <destination>BitAnd</destination>
        <destination>BitAnd</destination>
        <destination>BitAnd</destination>
   </type>
    <type>
        <source locate="after" endmark="(">
            funcname
        </source>
        <destination>bitand</destination>
        <destination>bitand</destination>
        <destination>bitand</destination>
        <destination>bitand</destination>
        <destination>bitand</destination>
        <destination>bitand</destination>
        <destination>bitand</destination>
        <destination>bitand</destination>
   </type>
    <type>
        <source locate="here">
            double
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
            Double
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
        <source locate="after" endmark=" (">
            operator
        </source>
        <destination><![CDATA[&]]></destination>
        <destination><![CDATA[&]]></destination>
        <destination><![CDATA[&]]></destination>
        <destination><![CDATA[&]]></destination>
        <destination><![CDATA[&]]></destination>
        <destination><![CDATA[&]]></destination>
        <destination><![CDATA[&]]></destination>
        <destination><![CDATA[&]]></destination>
   </type>
    <type>
        <source locate="after" endmark=" (">
            outCast
        </source>
        <destination><![CDATA[(sbyte)]]></destination>
        <destination><![CDATA[(byte)]]></destination>
        <destination><![CDATA[(short)]]></destination>
        <destination><![CDATA[(ushort)]]></destination>
        <destination><![CDATA[]]></destination>
        <destination><![CDATA[]]></destination>
        <destination><![CDATA[]]></destination>
        <destination><![CDATA[]]></destination>
   </type>
    </hycalper>
    */

    #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 

   

    internal static partial class MathInternal {

        /// <summary>
        /// Binary, elementwise, broadcasting operation: BitAnd.
        /// </summary>
        /// <param name="A">The one array.</param>
        /// <param name="B">The other array.</param>
        /// <returns>Result of operating elements of <paramref name="A"/> and <paramref name="B"/> elementwise.</returns>
        [ArrayOperation("ILNumerics.Accelerator.Core.BinaryArrayOperatorProxy")]
        internal static Array<ulong>  bitand(BaseArray<ulong> A, BaseArray<ulong> B) {
            return InnerLoops.BitAnd.UInt64.Instance.operate(
                A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>,
                B as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>);
        }
    }

    namespace InnerLoops.BitAnd {

        public unsafe class UInt64 
            : BroadcastingBinaryBase<ulong,Array<ulong>,InArray<ulong>,OutArray<ulong>,Array<ulong>,Storage<ulong>> {

            public static UInt64 Instance = new UInt64(); 

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
                    *((ulong*)pOut + start) =   (*(ulong*)pA  & *(ulong*)pB  /* ? *(ulong*)pA : *(ulong*)pB */ );
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
                        *(ulong*)pOut =   ((*(ulong*)(pA))  & (*(ulong*)(pB))  /* ? (*(ulong*)(pA)) : (*(ulong*)(pB))*/ ); 
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
        /// Binary, elementwise, broadcasting operation: BitAnd.
        /// </summary>
        /// <param name="A">The one array.</param>
        /// <param name="B">The other array.</param>
        /// <returns>Result of operating elements of <paramref name="A"/> and <paramref name="B"/> elementwise.</returns>
        [ArrayOperation("ILNumerics.Accelerator.Core.BinaryArrayOperatorProxy")]
        internal static Array<long>  bitand(BaseArray<long> A, BaseArray<long> B) {
            return InnerLoops.BitAnd.Int64.Instance.operate(
                A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>,
                B as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>);
        }
    }

    namespace InnerLoops.BitAnd {

        public unsafe class Int64 
            : BroadcastingBinaryBase<long,Array<long>,InArray<long>,OutArray<long>,Array<long>,Storage<long>> {

            public static Int64 Instance = new Int64(); 

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
                    *((long*)pOut + start) =   (*(long*)pA  & *(long*)pB  /* ? *(long*)pA : *(long*)pB */ );
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
                        *(long*)pOut =   ((*(long*)(pA))  & (*(long*)(pB))  /* ? (*(long*)(pA)) : (*(long*)(pB))*/ ); 
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
        /// Binary, elementwise, broadcasting operation: BitAnd.
        /// </summary>
        /// <param name="A">The one array.</param>
        /// <param name="B">The other array.</param>
        /// <returns>Result of operating elements of <paramref name="A"/> and <paramref name="B"/> elementwise.</returns>
        [ArrayOperation("ILNumerics.Accelerator.Core.BinaryArrayOperatorProxy")]
        internal static Array<uint>  bitand(BaseArray<uint> A, BaseArray<uint> B) {
            return InnerLoops.BitAnd.UInt32.Instance.operate(
                A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>,
                B as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>);
        }
    }

    namespace InnerLoops.BitAnd {

        public unsafe class UInt32 
            : BroadcastingBinaryBase<uint,Array<uint>,InArray<uint>,OutArray<uint>,Array<uint>,Storage<uint>> {

            public static UInt32 Instance = new UInt32(); 

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
                    *((uint*)pOut + start) =   (*(uint*)pA  & *(uint*)pB  /* ? *(uint*)pA : *(uint*)pB */ );
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
                        *(uint*)pOut =   ((*(uint*)(pA))  & (*(uint*)(pB))  /* ? (*(uint*)(pA)) : (*(uint*)(pB))*/ ); 
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
        /// Binary, elementwise, broadcasting operation: BitAnd.
        /// </summary>
        /// <param name="A">The one array.</param>
        /// <param name="B">The other array.</param>
        /// <returns>Result of operating elements of <paramref name="A"/> and <paramref name="B"/> elementwise.</returns>
        [ArrayOperation("ILNumerics.Accelerator.Core.BinaryArrayOperatorProxy")]
        internal static Array<int>  bitand(BaseArray<int> A, BaseArray<int> B) {
            return InnerLoops.BitAnd.Int32.Instance.operate(
                A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>,
                B as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>);
        }
    }

    namespace InnerLoops.BitAnd {

        public unsafe class Int32 
            : BroadcastingBinaryBase<int,Array<int>,InArray<int>,OutArray<int>,Array<int>,Storage<int>> {

            public static Int32 Instance = new Int32(); 

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
                    *((int*)pOut + start) =   (*(int*)pA  & *(int*)pB  /* ? *(int*)pA : *(int*)pB */ );
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
                        *(int*)pOut =   ((*(int*)(pA))  & (*(int*)(pB))  /* ? (*(int*)(pA)) : (*(int*)(pB))*/ ); 
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
        /// Binary, elementwise, broadcasting operation: BitAnd.
        /// </summary>
        /// <param name="A">The one array.</param>
        /// <param name="B">The other array.</param>
        /// <returns>Result of operating elements of <paramref name="A"/> and <paramref name="B"/> elementwise.</returns>
        [ArrayOperation("ILNumerics.Accelerator.Core.BinaryArrayOperatorProxy")]
        internal static Array<ushort>  bitand(BaseArray<ushort> A, BaseArray<ushort> B) {
            return InnerLoops.BitAnd.UInt16.Instance.operate(
                A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>,
                B as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>);
        }
    }

    namespace InnerLoops.BitAnd {

        public unsafe class UInt16 
            : BroadcastingBinaryBase<ushort,Array<ushort>,InArray<ushort>,OutArray<ushort>,Array<ushort>,Storage<ushort>> {

            public static UInt16 Instance = new UInt16(); 

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
                    *((ushort*)pOut + start) =  (ushort) (*(ushort*)pA  & *(ushort*)pB  /* ? *(ushort*)pA : *(ushort*)pB */ );
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
                        *(ushort*)pOut =  (ushort) ((*(ushort*)(pA))  & (*(ushort*)(pB))  /* ? (*(ushort*)(pA)) : (*(ushort*)(pB))*/ ); 
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
        /// Binary, elementwise, broadcasting operation: BitAnd.
        /// </summary>
        /// <param name="A">The one array.</param>
        /// <param name="B">The other array.</param>
        /// <returns>Result of operating elements of <paramref name="A"/> and <paramref name="B"/> elementwise.</returns>
        [ArrayOperation("ILNumerics.Accelerator.Core.BinaryArrayOperatorProxy")]
        internal static Array<short>  bitand(BaseArray<short> A, BaseArray<short> B) {
            return InnerLoops.BitAnd.Int16.Instance.operate(
                A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>,
                B as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>);
        }
    }

    namespace InnerLoops.BitAnd {

        public unsafe class Int16 
            : BroadcastingBinaryBase<short,Array<short>,InArray<short>,OutArray<short>,Array<short>,Storage<short>> {

            public static Int16 Instance = new Int16(); 

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
                    *((short*)pOut + start) =  (short) (*(short*)pA  & *(short*)pB  /* ? *(short*)pA : *(short*)pB */ );
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
                        *(short*)pOut =  (short) ((*(short*)(pA))  & (*(short*)(pB))  /* ? (*(short*)(pA)) : (*(short*)(pB))*/ ); 
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
        /// Binary, elementwise, broadcasting operation: BitAnd.
        /// </summary>
        /// <param name="A">The one array.</param>
        /// <param name="B">The other array.</param>
        /// <returns>Result of operating elements of <paramref name="A"/> and <paramref name="B"/> elementwise.</returns>
        [ArrayOperation("ILNumerics.Accelerator.Core.BinaryArrayOperatorProxy")]
        internal static Array<byte>  bitand(BaseArray<byte> A, BaseArray<byte> B) {
            return InnerLoops.BitAnd.Byte.Instance.operate(
                A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>,
                B as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>);
        }
    }

    namespace InnerLoops.BitAnd {

        public unsafe class Byte 
            : BroadcastingBinaryBase<byte,Array<byte>,InArray<byte>,OutArray<byte>,Array<byte>,Storage<byte>> {

            public static Byte Instance = new Byte(); 

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
                    *((byte*)pOut + start) =  (byte) (*(byte*)pA  & *(byte*)pB  /* ? *(byte*)pA : *(byte*)pB */ );
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
                        *(byte*)pOut =  (byte) ((*(byte*)(pA))  & (*(byte*)(pB))  /* ? (*(byte*)(pA)) : (*(byte*)(pB))*/ ); 
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
        /// Binary, elementwise, broadcasting operation: BitAnd.
        /// </summary>
        /// <param name="A">The one array.</param>
        /// <param name="B">The other array.</param>
        /// <returns>Result of operating elements of <paramref name="A"/> and <paramref name="B"/> elementwise.</returns>
        [ArrayOperation("ILNumerics.Accelerator.Core.BinaryArrayOperatorProxy")]
        internal static Array<sbyte>  bitand(BaseArray<sbyte> A, BaseArray<sbyte> B) {
            return InnerLoops.BitAnd.SByte.Instance.operate(
                A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>,
                B as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>);
        }
    }

    namespace InnerLoops.BitAnd {

        public unsafe class SByte 
            : BroadcastingBinaryBase<sbyte,Array<sbyte>,InArray<sbyte>,OutArray<sbyte>,Array<sbyte>,Storage<sbyte>> {

            public static SByte Instance = new SByte(); 

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
                    *((sbyte*)pOut + start) =  (sbyte) (*(sbyte*)pA  & *(sbyte*)pB  /* ? *(sbyte*)pA : *(sbyte*)pB */ );
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
                        *(sbyte*)pOut =  (sbyte) ((*(sbyte*)(pA))  & (*(sbyte*)(pB))  /* ? (*(sbyte*)(pA)) : (*(sbyte*)(pB))*/ ); 
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
