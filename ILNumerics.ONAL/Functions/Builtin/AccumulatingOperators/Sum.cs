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
        /// Compute the sum of elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <param name="dim">[Optional] The index of the dimension to be reduced. Default: (-1) the first non-singleton dimension found in <paramref name="A"/> or 0.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length min(1,A.S[dim]) and removed if <paramref name="keepdim"/> is false.</returns>
        /// <remarks><para></para></remarks>
        /// <exception cref="ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim"/> was false.</exception>
        
        internal unsafe static /*!HC:RetCls*/Array</*!HC:outArr*/double> /*!HC:funcname*/sum(BaseArray<double> A, int dim = -1, bool keepdim = true) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops./*!HC:innerloopname*/Sum.Double.Instance.operate(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace /*!HC:innerloopname*/Sum {

            internal class Double :

            ReduceSameTypeBase<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> {

                internal static Double Instance = new Double();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut) {

                    /*
                     * bsdIn:   dims: -1
                     *          dims[axis] is reordered to be the LAST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     * bsdOut:  dims: -1
                     *          dims[axis] is not removed! reordered to the LAST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * inDims correspond to outDims. Iteration is done along outDims only. 
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

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;  
                    
                    long* cur = stackalloc long[Math.Max((int)ndims, 1)]; 

                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    long f = cur[0] = start;
                    int i = 0;
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

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long leadLen = accuDim;
                        /*!HC:outArr*/double tmp = /*!HC:initialValue*/ 0; // likely to cause a register to be used in the following
                        byte* pRead = pSrc; 

                        while (leadLen-- > 0) {
                            tmp = (/*!HC:outArr*/double)(tmp /*!HC:operatorfunc*/ + (*(double*)pRead)/*!HC:postOperator*/ /**/);
                            pRead += accuStride;
                        }
                        *(/*!HC:outArr*/double*)pDest = tmp; 
                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 0;
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
        /// Compute the sum of elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <param name="dim">[Optional] The index of the dimension to be reduced. Default: (-1) the first non-singleton dimension found in <paramref name="A"/> or 0.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length min(1,A.S[dim]) and removed if <paramref name="keepdim"/> is false.</returns>
        /// <remarks><para></para></remarks>
        /// <exception cref="ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim"/> was false.</exception>
        
        internal unsafe static Array<fcomplex> sum(BaseArray<fcomplex> A, int dim = -1, bool keepdim = true) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.Sum.FComplex.Instance.operate(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Sum {

            internal class FComplex :

            ReduceSameTypeBase<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> {

                internal static FComplex Instance = new FComplex();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut) {

                    /*
                     * bsdIn:   dims: -1
                     *          dims[axis] is reordered to be the LAST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     * bsdOut:  dims: -1
                     *          dims[axis] is not removed! reordered to the LAST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * inDims correspond to outDims. Iteration is done along outDims only. 
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

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;  
                    
                    long* cur = stackalloc long[Math.Max((int)ndims, 1)]; 

                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    long f = cur[0] = start;
                    int i = 0;
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

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long leadLen = accuDim;
                       fcomplex tmp =  0; // likely to cause a register to be used in the following
                        byte* pRead = pSrc; 

                        while (leadLen-- > 0) {
                            tmp = (fcomplex)(tmp  + (*(fcomplex*)pRead) /**/);
                            pRead += accuStride;
                        }
                        *(fcomplex*)pDest = tmp; 
                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 0;
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
        /// Compute the sum of elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <param name="dim">[Optional] The index of the dimension to be reduced. Default: (-1) the first non-singleton dimension found in <paramref name="A"/> or 0.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length min(1,A.S[dim]) and removed if <paramref name="keepdim"/> is false.</returns>
        /// <remarks><para></para></remarks>
        /// <exception cref="ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim"/> was false.</exception>
        
        internal unsafe static Array<complex> sum(BaseArray<complex> A, int dim = -1, bool keepdim = true) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.Sum.Complex.Instance.operate(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Sum {

            internal class Complex :

            ReduceSameTypeBase<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> {

                internal static Complex Instance = new Complex();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut) {

                    /*
                     * bsdIn:   dims: -1
                     *          dims[axis] is reordered to be the LAST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     * bsdOut:  dims: -1
                     *          dims[axis] is not removed! reordered to the LAST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * inDims correspond to outDims. Iteration is done along outDims only. 
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

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;  
                    
                    long* cur = stackalloc long[Math.Max((int)ndims, 1)]; 

                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    long f = cur[0] = start;
                    int i = 0;
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

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long leadLen = accuDim;
                       complex tmp =  0; // likely to cause a register to be used in the following
                        byte* pRead = pSrc; 

                        while (leadLen-- > 0) {
                            tmp = (complex)(tmp  + (*(complex*)pRead) /**/);
                            pRead += accuStride;
                        }
                        *(complex*)pDest = tmp; 
                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 0;
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
        /// Compute the sum of elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <param name="dim">[Optional] The index of the dimension to be reduced. Default: (-1) the first non-singleton dimension found in <paramref name="A"/> or 0.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length min(1,A.S[dim]) and removed if <paramref name="keepdim"/> is false.</returns>
        /// <remarks><para></para></remarks>
        /// <exception cref="ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim"/> was false.</exception>
        
        internal unsafe static Array<float> sum(BaseArray<float> A, int dim = -1, bool keepdim = true) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.Sum.Single.Instance.operate(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Sum {

            internal class Single :

            ReduceSameTypeBase<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> {

                internal static Single Instance = new Single();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut) {

                    /*
                     * bsdIn:   dims: -1
                     *          dims[axis] is reordered to be the LAST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     * bsdOut:  dims: -1
                     *          dims[axis] is not removed! reordered to the LAST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * inDims correspond to outDims. Iteration is done along outDims only. 
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

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;  
                    
                    long* cur = stackalloc long[Math.Max((int)ndims, 1)]; 

                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    long f = cur[0] = start;
                    int i = 0;
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

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long leadLen = accuDim;
                       float tmp =  0; // likely to cause a register to be used in the following
                        byte* pRead = pSrc; 

                        while (leadLen-- > 0) {
                            tmp = (float)(tmp  + (*(float*)pRead) /**/);
                            pRead += accuStride;
                        }
                        *(float*)pDest = tmp; 
                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 0;
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
        /// Compute the sum of elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <param name="dim">[Optional] The index of the dimension to be reduced. Default: (-1) the first non-singleton dimension found in <paramref name="A"/> or 0.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length min(1,A.S[dim]) and removed if <paramref name="keepdim"/> is false.</returns>
        /// <remarks><para></para></remarks>
        /// <exception cref="ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim"/> was false.</exception>
        
        internal unsafe static Array<long> sum(BaseArray<long> A, int dim = -1, bool keepdim = true) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.Sum.Int64.Instance.operate(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Sum {

            internal class Int64 :

            ReduceSameTypeBase<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> {

                internal static Int64 Instance = new Int64();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut) {

                    /*
                     * bsdIn:   dims: -1
                     *          dims[axis] is reordered to be the LAST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     * bsdOut:  dims: -1
                     *          dims[axis] is not removed! reordered to the LAST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * inDims correspond to outDims. Iteration is done along outDims only. 
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

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;  
                    
                    long* cur = stackalloc long[Math.Max((int)ndims, 1)]; 

                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    long f = cur[0] = start;
                    int i = 0;
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

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long leadLen = accuDim;
                       long tmp =  0; // likely to cause a register to be used in the following
                        byte* pRead = pSrc; 

                        while (leadLen-- > 0) {
                            tmp = (long)(tmp  + (*(long*)pRead) /**/);
                            pRead += accuStride;
                        }
                        *(long*)pDest = tmp; 
                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 0;
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
        /// Compute the sum of elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <param name="dim">[Optional] The index of the dimension to be reduced. Default: (-1) the first non-singleton dimension found in <paramref name="A"/> or 0.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length min(1,A.S[dim]) and removed if <paramref name="keepdim"/> is false.</returns>
        /// <remarks><para></para></remarks>
        /// <exception cref="ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim"/> was false.</exception>
        
        internal unsafe static Array<ulong> sum(BaseArray<ulong> A, int dim = -1, bool keepdim = true) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.Sum.UInt64.Instance.operate(A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Sum {

            internal class UInt64 :

            ReduceSameTypeBase<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> {

                internal static UInt64 Instance = new UInt64();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut) {

                    /*
                     * bsdIn:   dims: -1
                     *          dims[axis] is reordered to be the LAST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     * bsdOut:  dims: -1
                     *          dims[axis] is not removed! reordered to the LAST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * inDims correspond to outDims. Iteration is done along outDims only. 
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

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;  
                    
                    long* cur = stackalloc long[Math.Max((int)ndims, 1)]; 

                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    long f = cur[0] = start;
                    int i = 0;
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

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long leadLen = accuDim;
                       ulong tmp =  0; // likely to cause a register to be used in the following
                        byte* pRead = pSrc; 

                        while (leadLen-- > 0) {
                            tmp = (ulong)(tmp  + (*(ulong*)pRead) /**/);
                            pRead += accuStride;
                        }
                        *(ulong*)pDest = tmp; 
                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 0;
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
        /// Compute the sum of elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <param name="dim">[Optional] The index of the dimension to be reduced. Default: (-1) the first non-singleton dimension found in <paramref name="A"/> or 0.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length min(1,A.S[dim]) and removed if <paramref name="keepdim"/> is false.</returns>
        /// <remarks><para></para></remarks>
        /// <exception cref="ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim"/> was false.</exception>
        
        internal unsafe static Array<int> sum(BaseArray<int> A, int dim = -1, bool keepdim = true) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.Sum.Int32.Instance.operate(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Sum {

            internal class Int32 :

            ReduceSameTypeBase<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> {

                internal static Int32 Instance = new Int32();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut) {

                    /*
                     * bsdIn:   dims: -1
                     *          dims[axis] is reordered to be the LAST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     * bsdOut:  dims: -1
                     *          dims[axis] is not removed! reordered to the LAST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * inDims correspond to outDims. Iteration is done along outDims only. 
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

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;  
                    
                    long* cur = stackalloc long[Math.Max((int)ndims, 1)]; 

                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    long f = cur[0] = start;
                    int i = 0;
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

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long leadLen = accuDim;
                       int tmp =  0; // likely to cause a register to be used in the following
                        byte* pRead = pSrc; 

                        while (leadLen-- > 0) {
                            tmp = (int)(tmp  + (*(int*)pRead) /**/);
                            pRead += accuStride;
                        }
                        *(int*)pDest = tmp; 
                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 0;
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
        /// Compute the sum of elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <param name="dim">[Optional] The index of the dimension to be reduced. Default: (-1) the first non-singleton dimension found in <paramref name="A"/> or 0.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length min(1,A.S[dim]) and removed if <paramref name="keepdim"/> is false.</returns>
        /// <remarks><para></para></remarks>
        /// <exception cref="ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim"/> was false.</exception>
        
        internal unsafe static Array<uint> sum(BaseArray<uint> A, int dim = -1, bool keepdim = true) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.Sum.UInt32.Instance.operate(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Sum {

            internal class UInt32 :

            ReduceSameTypeBase<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> {

                internal static UInt32 Instance = new UInt32();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut) {

                    /*
                     * bsdIn:   dims: -1
                     *          dims[axis] is reordered to be the LAST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     * bsdOut:  dims: -1
                     *          dims[axis] is not removed! reordered to the LAST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * inDims correspond to outDims. Iteration is done along outDims only. 
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

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;  
                    
                    long* cur = stackalloc long[Math.Max((int)ndims, 1)]; 

                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    long f = cur[0] = start;
                    int i = 0;
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

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long leadLen = accuDim;
                       uint tmp =  0; // likely to cause a register to be used in the following
                        byte* pRead = pSrc; 

                        while (leadLen-- > 0) {
                            tmp = (uint)(tmp  + (*(uint*)pRead) /**/);
                            pRead += accuStride;
                        }
                        *(uint*)pDest = tmp; 
                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 0;
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
        /// Compute the sum of elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <param name="dim">[Optional] The index of the dimension to be reduced. Default: (-1) the first non-singleton dimension found in <paramref name="A"/> or 0.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length min(1,A.S[dim]) and removed if <paramref name="keepdim"/> is false.</returns>
        /// <remarks><para></para></remarks>
        /// <exception cref="ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim"/> was false.</exception>
        
        internal unsafe static Array<short> sum(BaseArray<short> A, int dim = -1, bool keepdim = true) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.Sum.Int16.Instance.operate(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Sum {

            internal class Int16 :

            ReduceSameTypeBase<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> {

                internal static Int16 Instance = new Int16();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut) {

                    /*
                     * bsdIn:   dims: -1
                     *          dims[axis] is reordered to be the LAST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     * bsdOut:  dims: -1
                     *          dims[axis] is not removed! reordered to the LAST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * inDims correspond to outDims. Iteration is done along outDims only. 
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

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;  
                    
                    long* cur = stackalloc long[Math.Max((int)ndims, 1)]; 

                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    long f = cur[0] = start;
                    int i = 0;
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

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long leadLen = accuDim;
                       short tmp =  0; // likely to cause a register to be used in the following
                        byte* pRead = pSrc; 

                        while (leadLen-- > 0) {
                            tmp = (short)(tmp  + (*(short*)pRead) /**/);
                            pRead += accuStride;
                        }
                        *(short*)pDest = tmp; 
                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 0;
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
        /// Compute the sum of elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <param name="dim">[Optional] The index of the dimension to be reduced. Default: (-1) the first non-singleton dimension found in <paramref name="A"/> or 0.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length min(1,A.S[dim]) and removed if <paramref name="keepdim"/> is false.</returns>
        /// <remarks><para></para></remarks>
        /// <exception cref="ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim"/> was false.</exception>
        
        internal unsafe static Array<ushort> sum(BaseArray<ushort> A, int dim = -1, bool keepdim = true) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.Sum.UInt16.Instance.operate(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Sum {

            internal class UInt16 :

            ReduceSameTypeBase<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> {

                internal static UInt16 Instance = new UInt16();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut) {

                    /*
                     * bsdIn:   dims: -1
                     *          dims[axis] is reordered to be the LAST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     * bsdOut:  dims: -1
                     *          dims[axis] is not removed! reordered to the LAST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * inDims correspond to outDims. Iteration is done along outDims only. 
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

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;  
                    
                    long* cur = stackalloc long[Math.Max((int)ndims, 1)]; 

                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    long f = cur[0] = start;
                    int i = 0;
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

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long leadLen = accuDim;
                       ushort tmp =  0; // likely to cause a register to be used in the following
                        byte* pRead = pSrc; 

                        while (leadLen-- > 0) {
                            tmp = (ushort)(tmp  + (*(ushort*)pRead) /**/);
                            pRead += accuStride;
                        }
                        *(ushort*)pDest = tmp; 
                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 0;
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
        /// Compute the sum of elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <param name="dim">[Optional] The index of the dimension to be reduced. Default: (-1) the first non-singleton dimension found in <paramref name="A"/> or 0.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length min(1,A.S[dim]) and removed if <paramref name="keepdim"/> is false.</returns>
        /// <remarks><para></para></remarks>
        /// <exception cref="ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim"/> was false.</exception>
        
        internal unsafe static Array<sbyte> sum(BaseArray<sbyte> A, int dim = -1, bool keepdim = true) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.Sum.SByte.Instance.operate(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Sum {

            internal class SByte :

            ReduceSameTypeBase<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> {

                internal static SByte Instance = new SByte();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut) {

                    /*
                     * bsdIn:   dims: -1
                     *          dims[axis] is reordered to be the LAST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     * bsdOut:  dims: -1
                     *          dims[axis] is not removed! reordered to the LAST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * inDims correspond to outDims. Iteration is done along outDims only. 
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

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;  
                    
                    long* cur = stackalloc long[Math.Max((int)ndims, 1)]; 

                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    long f = cur[0] = start;
                    int i = 0;
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

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long leadLen = accuDim;
                       sbyte tmp =  0; // likely to cause a register to be used in the following
                        byte* pRead = pSrc; 

                        while (leadLen-- > 0) {
                            tmp = (sbyte)(tmp  + (*(sbyte*)pRead) /**/);
                            pRead += accuStride;
                        }
                        *(sbyte*)pDest = tmp; 
                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 0;
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
        /// Compute the sum of elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <param name="dim">[Optional] The index of the dimension to be reduced. Default: (-1) the first non-singleton dimension found in <paramref name="A"/> or 0.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length min(1,A.S[dim]) and removed if <paramref name="keepdim"/> is false.</returns>
        /// <remarks><para></para></remarks>
        /// <exception cref="ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim"/> was false.</exception>
        
        internal unsafe static Array<byte> sum(BaseArray<byte> A, int dim = -1, bool keepdim = true) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.Sum.Byte.Instance.operate(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Sum {

            internal class Byte :

            ReduceSameTypeBase<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> {

                internal static Byte Instance = new Byte();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut) {

                    /*
                     * bsdIn:   dims: -1
                     *          dims[axis] is reordered to be the LAST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     * bsdOut:  dims: -1
                     *          dims[axis] is not removed! reordered to the LAST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * inDims correspond to outDims. Iteration is done along outDims only. 
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

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;  
                    
                    long* cur = stackalloc long[Math.Max((int)ndims, 1)]; 

                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    long f = cur[0] = start;
                    int i = 0;
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

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long leadLen = accuDim;
                       byte tmp =  0; // likely to cause a register to be used in the following
                        byte* pRead = pSrc; 

                        while (leadLen-- > 0) {
                            tmp = (byte)(tmp  + (*(byte*)pRead) /**/);
                            pRead += accuStride;
                        }
                        *(byte*)pDest = tmp; 
                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 0;
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

