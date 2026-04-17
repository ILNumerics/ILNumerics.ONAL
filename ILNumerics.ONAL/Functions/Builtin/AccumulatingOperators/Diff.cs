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
    </type>
    </hycalper>
    */
    internal static partial class MathInternal {

        /// <summary>
        /// Computes the differences between successive elements of <paramref name="A"/> along the specified dimension <paramref name="dim"/> or the n-th derivative.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="N">[Optional] Order of difference. Default: 1.</param>
        /// <param name="dim">[Optional] The dimension index of the working dimension. Default (-1): first non-singleton dimension of <paramref name="A"/> or 0, if no such dimension exists.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced by 1 element.</returns>
        /// <remarks><para>If the length of the <paramref name="dim"/>s dimension in <paramref name="A"/> is lower than 2, an empty 
        /// array is returned, having the size of <paramref name="A"/> but the <paramref name="dim"/>s dimension reduced to max(A.S[dim]-1,0).</para></remarks>
        
        internal unsafe static /*!HC:RetCls*/Array</*!HC:outArr*/double> diff(BaseArray<double> A, uint N = 1, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            // for thread safety: make a clone of A if it is not RetT: 
            return InnerLoops.Diff.Double.Instance.operate(A.ToArray<double>(), N, dim);
        }
    }
    namespace InnerLoops {

        namespace Diff {

            internal class Double :

            UnarySameTypeDiffBase<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> {

                internal static Double Instance = new Double();

                protected override long determineWorkDimLength(long v) {
                    return Math.Max(v - 1, 0); 
                }
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

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

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
                        long leadLen = dims[0];
                        System.Diagnostics.Debug.Assert(leadLen > 0);  // leadLen == outLen && outLen > 0 && outS = inS + 1
                        byte* pRead = pSrc;
                        byte* pWrite = pDest;

                        while (leadLen-- > 0) {
                            *(double*)(pWrite) = (double)(*(double*)(pRead + 1 * accuInStride) - * (double*)(pRead));
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
    #endregion HYCALPER LOOPEND UNARY_OPERATOR_TEMPLATE
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
   
    internal static partial class MathInternal {

        /// <summary>
        /// Computes the differences between successive elements of <paramref name="A"/> along the specified dimension <paramref name="dim"/> or the n-th derivative.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="N">[Optional] Order of difference. Default: 1.</param>
        /// <param name="dim">[Optional] The dimension index of the working dimension. Default (-1): first non-singleton dimension of <paramref name="A"/> or 0, if no such dimension exists.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced by 1 element.</returns>
        /// <remarks><para>If the length of the <paramref name="dim"/>s dimension in <paramref name="A"/> is lower than 2, an empty 
        /// array is returned, having the size of <paramref name="A"/> but the <paramref name="dim"/>s dimension reduced to max(A.S[dim]-1,0).</para></remarks>
        
        internal unsafe static Array<fcomplex> diff(BaseArray<fcomplex> A, uint N = 1, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            // for thread safety: make a clone of A if it is not RetT: 
            return InnerLoops.Diff.FComplex.Instance.operate(A.ToArray<fcomplex>(), N, dim);
        }
    }
    namespace InnerLoops {

        namespace Diff {

            internal class FComplex :

            UnarySameTypeDiffBase<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> {

                internal static FComplex Instance = new FComplex();

                protected override long determineWorkDimLength(long v) {
                    return Math.Max(v - 1, 0); 
                }
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

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

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
                        long leadLen = dims[0];
                        System.Diagnostics.Debug.Assert(leadLen > 0);  // leadLen == outLen && outLen > 0 && outS = inS + 1
                        byte* pRead = pSrc;
                        byte* pWrite = pDest;

                        while (leadLen-- > 0) {
                            *(fcomplex*)(pWrite) = (fcomplex)(*(fcomplex*)(pRead + 1 * accuInStride) - * (fcomplex*)(pRead));
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

        /// <summary>
        /// Computes the differences between successive elements of <paramref name="A"/> along the specified dimension <paramref name="dim"/> or the n-th derivative.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="N">[Optional] Order of difference. Default: 1.</param>
        /// <param name="dim">[Optional] The dimension index of the working dimension. Default (-1): first non-singleton dimension of <paramref name="A"/> or 0, if no such dimension exists.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced by 1 element.</returns>
        /// <remarks><para>If the length of the <paramref name="dim"/>s dimension in <paramref name="A"/> is lower than 2, an empty 
        /// array is returned, having the size of <paramref name="A"/> but the <paramref name="dim"/>s dimension reduced to max(A.S[dim]-1,0).</para></remarks>
        
        internal unsafe static Array<complex> diff(BaseArray<complex> A, uint N = 1, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            // for thread safety: make a clone of A if it is not RetT: 
            return InnerLoops.Diff.Complex.Instance.operate(A.ToArray<complex>(), N, dim);
        }
    }
    namespace InnerLoops {

        namespace Diff {

            internal class Complex :

            UnarySameTypeDiffBase<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> {

                internal static Complex Instance = new Complex();

                protected override long determineWorkDimLength(long v) {
                    return Math.Max(v - 1, 0); 
                }
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

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

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
                        long leadLen = dims[0];
                        System.Diagnostics.Debug.Assert(leadLen > 0);  // leadLen == outLen && outLen > 0 && outS = inS + 1
                        byte* pRead = pSrc;
                        byte* pWrite = pDest;

                        while (leadLen-- > 0) {
                            *(complex*)(pWrite) = (complex)(*(complex*)(pRead + 1 * accuInStride) - * (complex*)(pRead));
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

        /// <summary>
        /// Computes the differences between successive elements of <paramref name="A"/> along the specified dimension <paramref name="dim"/> or the n-th derivative.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="N">[Optional] Order of difference. Default: 1.</param>
        /// <param name="dim">[Optional] The dimension index of the working dimension. Default (-1): first non-singleton dimension of <paramref name="A"/> or 0, if no such dimension exists.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced by 1 element.</returns>
        /// <remarks><para>If the length of the <paramref name="dim"/>s dimension in <paramref name="A"/> is lower than 2, an empty 
        /// array is returned, having the size of <paramref name="A"/> but the <paramref name="dim"/>s dimension reduced to max(A.S[dim]-1,0).</para></remarks>
        
        internal unsafe static Array<float> diff(BaseArray<float> A, uint N = 1, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            // for thread safety: make a clone of A if it is not RetT: 
            return InnerLoops.Diff.Single.Instance.operate(A.ToArray<float>(), N, dim);
        }
    }
    namespace InnerLoops {

        namespace Diff {

            internal class Single :

            UnarySameTypeDiffBase<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> {

                internal static Single Instance = new Single();

                protected override long determineWorkDimLength(long v) {
                    return Math.Max(v - 1, 0); 
                }
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

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

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
                        long leadLen = dims[0];
                        System.Diagnostics.Debug.Assert(leadLen > 0);  // leadLen == outLen && outLen > 0 && outS = inS + 1
                        byte* pRead = pSrc;
                        byte* pWrite = pDest;

                        while (leadLen-- > 0) {
                            *(float*)(pWrite) = (float)(*(float*)(pRead + 1 * accuInStride) - * (float*)(pRead));
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

        /// <summary>
        /// Computes the differences between successive elements of <paramref name="A"/> along the specified dimension <paramref name="dim"/> or the n-th derivative.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="N">[Optional] Order of difference. Default: 1.</param>
        /// <param name="dim">[Optional] The dimension index of the working dimension. Default (-1): first non-singleton dimension of <paramref name="A"/> or 0, if no such dimension exists.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced by 1 element.</returns>
        /// <remarks><para>If the length of the <paramref name="dim"/>s dimension in <paramref name="A"/> is lower than 2, an empty 
        /// array is returned, having the size of <paramref name="A"/> but the <paramref name="dim"/>s dimension reduced to max(A.S[dim]-1,0).</para></remarks>
        
        internal unsafe static Array<long> diff(BaseArray<long> A, uint N = 1, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            // for thread safety: make a clone of A if it is not RetT: 
            return InnerLoops.Diff.Int64.Instance.operate(A.ToArray<long>(), N, dim);
        }
    }
    namespace InnerLoops {

        namespace Diff {

            internal class Int64 :

            UnarySameTypeDiffBase<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> {

                internal static Int64 Instance = new Int64();

                protected override long determineWorkDimLength(long v) {
                    return Math.Max(v - 1, 0); 
                }
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

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

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
                        long leadLen = dims[0];
                        System.Diagnostics.Debug.Assert(leadLen > 0);  // leadLen == outLen && outLen > 0 && outS = inS + 1
                        byte* pRead = pSrc;
                        byte* pWrite = pDest;

                        while (leadLen-- > 0) {
                            *(long*)(pWrite) = (long)(*(long*)(pRead + 1 * accuInStride) - * (long*)(pRead));
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

        /// <summary>
        /// Computes the differences between successive elements of <paramref name="A"/> along the specified dimension <paramref name="dim"/> or the n-th derivative.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="N">[Optional] Order of difference. Default: 1.</param>
        /// <param name="dim">[Optional] The dimension index of the working dimension. Default (-1): first non-singleton dimension of <paramref name="A"/> or 0, if no such dimension exists.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced by 1 element.</returns>
        /// <remarks><para>If the length of the <paramref name="dim"/>s dimension in <paramref name="A"/> is lower than 2, an empty 
        /// array is returned, having the size of <paramref name="A"/> but the <paramref name="dim"/>s dimension reduced to max(A.S[dim]-1,0).</para></remarks>
        
        internal unsafe static Array<ulong> diff(BaseArray<ulong> A, uint N = 1, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            // for thread safety: make a clone of A if it is not RetT: 
            return InnerLoops.Diff.UInt64.Instance.operate(A.ToArray<ulong>(), N, dim);
        }
    }
    namespace InnerLoops {

        namespace Diff {

            internal class UInt64 :

            UnarySameTypeDiffBase<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> {

                internal static UInt64 Instance = new UInt64();

                protected override long determineWorkDimLength(long v) {
                    return Math.Max(v - 1, 0); 
                }
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

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

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
                        long leadLen = dims[0];
                        System.Diagnostics.Debug.Assert(leadLen > 0);  // leadLen == outLen && outLen > 0 && outS = inS + 1
                        byte* pRead = pSrc;
                        byte* pWrite = pDest;

                        while (leadLen-- > 0) {
                            *(ulong*)(pWrite) = (ulong)(*(ulong*)(pRead + 1 * accuInStride) - * (ulong*)(pRead));
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

        /// <summary>
        /// Computes the differences between successive elements of <paramref name="A"/> along the specified dimension <paramref name="dim"/> or the n-th derivative.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="N">[Optional] Order of difference. Default: 1.</param>
        /// <param name="dim">[Optional] The dimension index of the working dimension. Default (-1): first non-singleton dimension of <paramref name="A"/> or 0, if no such dimension exists.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced by 1 element.</returns>
        /// <remarks><para>If the length of the <paramref name="dim"/>s dimension in <paramref name="A"/> is lower than 2, an empty 
        /// array is returned, having the size of <paramref name="A"/> but the <paramref name="dim"/>s dimension reduced to max(A.S[dim]-1,0).</para></remarks>
        
        internal unsafe static Array<int> diff(BaseArray<int> A, uint N = 1, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            // for thread safety: make a clone of A if it is not RetT: 
            return InnerLoops.Diff.Int32.Instance.operate(A.ToArray<int>(), N, dim);
        }
    }
    namespace InnerLoops {

        namespace Diff {

            internal class Int32 :

            UnarySameTypeDiffBase<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> {

                internal static Int32 Instance = new Int32();

                protected override long determineWorkDimLength(long v) {
                    return Math.Max(v - 1, 0); 
                }
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

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

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
                        long leadLen = dims[0];
                        System.Diagnostics.Debug.Assert(leadLen > 0);  // leadLen == outLen && outLen > 0 && outS = inS + 1
                        byte* pRead = pSrc;
                        byte* pWrite = pDest;

                        while (leadLen-- > 0) {
                            *(int*)(pWrite) = (int)(*(int*)(pRead + 1 * accuInStride) - * (int*)(pRead));
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

        /// <summary>
        /// Computes the differences between successive elements of <paramref name="A"/> along the specified dimension <paramref name="dim"/> or the n-th derivative.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="N">[Optional] Order of difference. Default: 1.</param>
        /// <param name="dim">[Optional] The dimension index of the working dimension. Default (-1): first non-singleton dimension of <paramref name="A"/> or 0, if no such dimension exists.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced by 1 element.</returns>
        /// <remarks><para>If the length of the <paramref name="dim"/>s dimension in <paramref name="A"/> is lower than 2, an empty 
        /// array is returned, having the size of <paramref name="A"/> but the <paramref name="dim"/>s dimension reduced to max(A.S[dim]-1,0).</para></remarks>
        
        internal unsafe static Array<uint> diff(BaseArray<uint> A, uint N = 1, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            // for thread safety: make a clone of A if it is not RetT: 
            return InnerLoops.Diff.UInt32.Instance.operate(A.ToArray<uint>(), N, dim);
        }
    }
    namespace InnerLoops {

        namespace Diff {

            internal class UInt32 :

            UnarySameTypeDiffBase<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> {

                internal static UInt32 Instance = new UInt32();

                protected override long determineWorkDimLength(long v) {
                    return Math.Max(v - 1, 0); 
                }
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

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

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
                        long leadLen = dims[0];
                        System.Diagnostics.Debug.Assert(leadLen > 0);  // leadLen == outLen && outLen > 0 && outS = inS + 1
                        byte* pRead = pSrc;
                        byte* pWrite = pDest;

                        while (leadLen-- > 0) {
                            *(uint*)(pWrite) = (uint)(*(uint*)(pRead + 1 * accuInStride) - * (uint*)(pRead));
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

        /// <summary>
        /// Computes the differences between successive elements of <paramref name="A"/> along the specified dimension <paramref name="dim"/> or the n-th derivative.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="N">[Optional] Order of difference. Default: 1.</param>
        /// <param name="dim">[Optional] The dimension index of the working dimension. Default (-1): first non-singleton dimension of <paramref name="A"/> or 0, if no such dimension exists.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced by 1 element.</returns>
        /// <remarks><para>If the length of the <paramref name="dim"/>s dimension in <paramref name="A"/> is lower than 2, an empty 
        /// array is returned, having the size of <paramref name="A"/> but the <paramref name="dim"/>s dimension reduced to max(A.S[dim]-1,0).</para></remarks>
        
        internal unsafe static Array<short> diff(BaseArray<short> A, uint N = 1, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            // for thread safety: make a clone of A if it is not RetT: 
            return InnerLoops.Diff.Int16.Instance.operate(A.ToArray<short>(), N, dim);
        }
    }
    namespace InnerLoops {

        namespace Diff {

            internal class Int16 :

            UnarySameTypeDiffBase<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> {

                internal static Int16 Instance = new Int16();

                protected override long determineWorkDimLength(long v) {
                    return Math.Max(v - 1, 0); 
                }
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

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

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
                        long leadLen = dims[0];
                        System.Diagnostics.Debug.Assert(leadLen > 0);  // leadLen == outLen && outLen > 0 && outS = inS + 1
                        byte* pRead = pSrc;
                        byte* pWrite = pDest;

                        while (leadLen-- > 0) {
                            *(short*)(pWrite) = (short)(*(short*)(pRead + 1 * accuInStride) - * (short*)(pRead));
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

        /// <summary>
        /// Computes the differences between successive elements of <paramref name="A"/> along the specified dimension <paramref name="dim"/> or the n-th derivative.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="N">[Optional] Order of difference. Default: 1.</param>
        /// <param name="dim">[Optional] The dimension index of the working dimension. Default (-1): first non-singleton dimension of <paramref name="A"/> or 0, if no such dimension exists.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced by 1 element.</returns>
        /// <remarks><para>If the length of the <paramref name="dim"/>s dimension in <paramref name="A"/> is lower than 2, an empty 
        /// array is returned, having the size of <paramref name="A"/> but the <paramref name="dim"/>s dimension reduced to max(A.S[dim]-1,0).</para></remarks>
        
        internal unsafe static Array<ushort> diff(BaseArray<ushort> A, uint N = 1, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            // for thread safety: make a clone of A if it is not RetT: 
            return InnerLoops.Diff.UInt16.Instance.operate(A.ToArray<ushort>(), N, dim);
        }
    }
    namespace InnerLoops {

        namespace Diff {

            internal class UInt16 :

            UnarySameTypeDiffBase<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> {

                internal static UInt16 Instance = new UInt16();

                protected override long determineWorkDimLength(long v) {
                    return Math.Max(v - 1, 0); 
                }
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

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

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
                        long leadLen = dims[0];
                        System.Diagnostics.Debug.Assert(leadLen > 0);  // leadLen == outLen && outLen > 0 && outS = inS + 1
                        byte* pRead = pSrc;
                        byte* pWrite = pDest;

                        while (leadLen-- > 0) {
                            *(ushort*)(pWrite) = (ushort)(*(ushort*)(pRead + 1 * accuInStride) - * (ushort*)(pRead));
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

        /// <summary>
        /// Computes the differences between successive elements of <paramref name="A"/> along the specified dimension <paramref name="dim"/> or the n-th derivative.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="N">[Optional] Order of difference. Default: 1.</param>
        /// <param name="dim">[Optional] The dimension index of the working dimension. Default (-1): first non-singleton dimension of <paramref name="A"/> or 0, if no such dimension exists.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced by 1 element.</returns>
        /// <remarks><para>If the length of the <paramref name="dim"/>s dimension in <paramref name="A"/> is lower than 2, an empty 
        /// array is returned, having the size of <paramref name="A"/> but the <paramref name="dim"/>s dimension reduced to max(A.S[dim]-1,0).</para></remarks>
        
        internal unsafe static Array<sbyte> diff(BaseArray<sbyte> A, uint N = 1, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            // for thread safety: make a clone of A if it is not RetT: 
            return InnerLoops.Diff.SByte.Instance.operate(A.ToArray<sbyte>(), N, dim);
        }
    }
    namespace InnerLoops {

        namespace Diff {

            internal class SByte :

            UnarySameTypeDiffBase<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> {

                internal static SByte Instance = new SByte();

                protected override long determineWorkDimLength(long v) {
                    return Math.Max(v - 1, 0); 
                }
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

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

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
                        long leadLen = dims[0];
                        System.Diagnostics.Debug.Assert(leadLen > 0);  // leadLen == outLen && outLen > 0 && outS = inS + 1
                        byte* pRead = pSrc;
                        byte* pWrite = pDest;

                        while (leadLen-- > 0) {
                            *(sbyte*)(pWrite) = (sbyte)(*(sbyte*)(pRead + 1 * accuInStride) - * (sbyte*)(pRead));
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

        /// <summary>
        /// Computes the differences between successive elements of <paramref name="A"/> along the specified dimension <paramref name="dim"/> or the n-th derivative.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="N">[Optional] Order of difference. Default: 1.</param>
        /// <param name="dim">[Optional] The dimension index of the working dimension. Default (-1): first non-singleton dimension of <paramref name="A"/> or 0, if no such dimension exists.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced by 1 element.</returns>
        /// <remarks><para>If the length of the <paramref name="dim"/>s dimension in <paramref name="A"/> is lower than 2, an empty 
        /// array is returned, having the size of <paramref name="A"/> but the <paramref name="dim"/>s dimension reduced to max(A.S[dim]-1,0).</para></remarks>
        
        internal unsafe static Array<byte> diff(BaseArray<byte> A, uint N = 1, int dim = -1) {
            if (object.Equals(A, null)) {
                return null;
            }
            // for thread safety: make a clone of A if it is not RetT: 
            return InnerLoops.Diff.Byte.Instance.operate(A.ToArray<byte>(), N, dim);
        }
    }
    namespace InnerLoops {

        namespace Diff {

            internal class Byte :

            UnarySameTypeDiffBase<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> {

                internal static Byte Instance = new Byte();

                protected override long determineWorkDimLength(long v) {
                    return Math.Max(v - 1, 0); 
                }
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

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

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
                        long leadLen = dims[0];
                        System.Diagnostics.Debug.Assert(leadLen > 0);  // leadLen == outLen && outLen > 0 && outS = inS + 1
                        byte* pRead = pSrc;
                        byte* pWrite = pDest;

                        while (leadLen-- > 0) {
                            *(byte*)(pWrite) = (byte)(*(byte*)(pRead + 1 * accuInStride) - * (byte*)(pRead));
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

