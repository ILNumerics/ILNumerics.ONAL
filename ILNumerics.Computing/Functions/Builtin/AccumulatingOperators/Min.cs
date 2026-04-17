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

    #region HYCALPER LOOPSTART UNARY_OPERATOR_TEMPLATE@Functions\Builtin\AccumulatingOperators\Max.cs 
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
    <source locate="after" endmark=" .()">
        funcname
    </source>
    <destination>min</destination>
    <destination>min</destination>
    <destination>min</destination>
    <destination>min</destination>
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
    <source locate="after" endmark=" .{()">
        innerloopname
    </source>
    <destination>Min</destination>
    <destination>Min</destination>
    <destination>Min</destination>
    <destination>Min</destination>
    <destination>Min</destination>
    <destination>Min</destination>
    <destination>Min</destination>
    <destination>Min</destination>
    <destination>Min</destination>
    <destination>Min</destination>
    <destination>Min</destination>
    <destination>Min</destination>
    </type>
    <type>
    <source locate="after" endmark=" .{()">
        operatorfunc
    </source>
    <destination><![CDATA[<]]></destination>
    <destination><![CDATA[<]]></destination>
    <destination><![CDATA[<]]></destination>
    <destination><![CDATA[<]]></destination>
    <destination><![CDATA[<]]></destination>
    <destination><![CDATA[<]]></destination>
    <destination><![CDATA[<]]></destination>
    <destination><![CDATA[<]]></destination>
    <destination><![CDATA[<]]></destination>
    <destination><![CDATA[<]]></destination>
    <destination><![CDATA[<]]></destination>
    <destination><![CDATA[<]]></destination>
    </type>
    <type>
    <source locate="comment">
        summary
    </source>
    <destination><![CDATA[Computes the minimum of elements of <paramref name="A"/> along the specified dimension.]]></destination>
    <destination><![CDATA[Computes the minimum of elements of <paramref name="A"/> along the specified dimension.]]></destination>
    <destination><![CDATA[Computes the minimum of elements of <paramref name="A"/> along the specified dimension.]]></destination>
    <destination><![CDATA[Computes the minimum of elements of <paramref name="A"/> along the specified dimension.]]></destination>
    <destination><![CDATA[Computes the minimum of elements of <paramref name="A"/> along the specified dimension.]]></destination>
    <destination><![CDATA[Computes the minimum of elements of <paramref name="A"/> along the specified dimension.]]></destination>
    <destination><![CDATA[Computes the minimum of elements of <paramref name="A"/> along the specified dimension.]]></destination>
    <destination><![CDATA[Computes the minimum of elements of <paramref name="A"/> along the specified dimension.]]></destination>
    <destination><![CDATA[Computes the minimum of elements of <paramref name="A"/> along the specified dimension.]]></destination>
    <destination><![CDATA[Computes the minimum of elements of <paramref name="A"/> along the specified dimension.]]></destination>
    <destination><![CDATA[Computes the minimum of elements of <paramref name="A"/> along the specified dimension.]]></destination>
    <destination><![CDATA[Computes the minimum of elements of <paramref name="A"/> along the specified dimension.]]></destination>
    </type>
    <type>
    <source locate="nextline">
        hcisintegertype 
    </source>
    <destination>#if !ISINTEGERTYPE</destination>
    <destination>#if ISINTEGERTYPE</destination>
    <destination>#if ISINTEGERTYPE</destination>
    <destination>#if ISINTEGERTYPE</destination>
    <destination>#if ISINTEGERTYPE</destination>
    <destination>#if ISINTEGERTYPE</destination>
    <destination>#if ISINTEGERTYPE</destination>
    <destination>#if ISINTEGERTYPE</destination>
    <destination>#if ISINTEGERTYPE</destination>
    <destination>#if !ISINTEGERTYPE</destination>
    <destination>#if !ISINTEGERTYPE</destination>
    <destination>#if !ISINTEGERTYPE</destination>
    </type>
    </hycalper>
    */
    #endregion HYCALPER LOOPEND UNARY_OPERATOR_TEMPLATE
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
   
    internal static partial class MathInternal {

        /// <summary>Computes the minimum of elements of <paramref name="A"/> along the specified dimension.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <param name="dim">The index of the dimension to be reduced.</param>
        /// <param name="I">[Optional] Output: 0-based index values of the found elements along the working dimension. Default: (null) don't return the indices.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length min(1,A.S[dim]) and removed if <paramref name="keepdim"/> is false.</returns>
        /// <remarks><para>Empty arrays are handled in the same way as other shapes: the dimension specified by <paramref name="dim"/> is reduced to 1. If A.S[dim] == 0 the 
        /// <paramref name="dim"/>s dimension length in the array returned will also be 0.</para>
        /// <para>Scalar arrays: numpy scalars give a numpy scalar (0 dim) as output.</para>
        /// <para>Special floating point values (for floating point element types): positive and negative infinity are handled in the regular way. NaN ('not a number') values are ignored. If 
        /// all elements in a working dimension are NaN, NaN is returned as result in the corresponding output element.</para>
        /// <para>If the optional output parameter <paramref name="I"/> is not null on entry the function computes and returns the indices in <paramref name="A"/> of the 
        /// values returned. Thus, <paramref name="I"/> has the same shape as the return array. If <paramref name="I"/> is null on entry (default) 
        /// no indices are computated (may or may not decreasing the computational effort of completing this function).</para>
        /// <para>Stability: if <paramref name="I"/> is requested and multiple elements along the working dimension in <paramref name="A"/> have the 
        /// same minimum/maximum value it is undefined which element is 'picked' for the output. Hence, in this case the corresponding value in <paramref name="I"/> 
        /// is undefined. It may point to the first or any other element storing the smallest / largest value returned from the working dimension. Thus, Min() and Max() are <i>unstable</i> 
        /// regarding the index output argument!</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim"/> was false.</exception>
        
        internal unsafe static Array<fcomplex> min(BaseArray<fcomplex> A, OutArray<long> I = null, int dim = -1, bool keepdim = true) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.Min.FComplex.Instance.operate(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, 
                                                                               I, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Min {

            internal class FComplex :

            ReduceSameTypeBaseIndices<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> {

                internal static FComplex Instance = new FComplex();

                // implementation from ReduceSameTypeBase (no indices)
                public unsafe override void Strided64Indices(byte* pSrc, byte* pDest, byte* pIndices, long start, long len, long* bsdIn, long* bsdOut) {

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
                    System.Diagnostics.Debug.Assert(pIndices != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)bsdIn[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims;
                    long* outIdxStrides = bsdOut + 6 + ndims * 3; // 3 + ndims * 2 + 3 + ndims; 
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;

                    long* cur = stackalloc long[Math.Max((int)ndims, 1)]; // assumes stackalloc to clear values (unspecified!)
                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    long f = cur[0] = start;
                    {
                        int i = 0;
                        for (; f > 0 && i < ndims; i++) {
                            cur[i] = f % (dims[i] + 1);
                            f /= (dims[i] + 1);
                            pSrc += cur[i] * inStrides[i];
                            pDest += cur[i] * outStrides[i];
                            pIndices += cur[i] * outIdxStrides[i];
                        }
                        while (i < ndims) { // finish initializing cur
                            cur[i++] = 0;
                        }
                    }
                    System.Diagnostics.Debug.Assert(f == 0);

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long i = 0;
                        // likely to cause a register to be used in the following
                        long curIdx = -1;
#if !ISINTEGERTYPE
                        // find first non-nan
                        while (i < accuDim && (fcomplex.IsNaN(*(fcomplex*)(pSrc + i * accuStride)))) {
                            i++;
                        }
                        if (i < accuDim) {
#endif
                         
                        fcomplex curVal = *(fcomplex*)(pSrc + i * accuStride); curIdx = i++; // first non-NaN element
                            while (i < accuDim) {
                                if (*(fcomplex*)(pSrc + i * accuStride)  < curVal) { curVal = *(fcomplex*)(pSrc + i * accuStride); curIdx = i; }
                                i++;
                            }
                            *(fcomplex*)pDest = curVal;
                            *(long*)pIndices = curIdx;
#if !ISINTEGERTYPE
                        } else {
                            *(fcomplex*)pDest = fcomplex.NaN;
                            *(long*)pIndices = -1;
                        }
#endif
                        if (len-- == 0) {
                            break;
                        }

                        // increase current position
                        int d = 0;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pDest += outStrides[d];
                                pIndices += outIdxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pDest -= outStrides[d] * dims[d];
                                pIndices -= outIdxStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
                // implementation from ReduceSameTypeBase (no indices)
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
                    
                    long* cur = stackalloc long[Math.Max((int)ndims,1)]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    long f = cur[0] = start;
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];
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
                        // likely to cause a register to be used in the following
                       
                        fcomplex tmp; 
                        byte* pRead = pSrc; 
#if !ISINTEGERTYPE
                        // find first non-nan
                        while (leadLen > 0 && (fcomplex.IsNaN(*(fcomplex*)pRead))) {
                            pRead += accuStride; leadLen--; 
                        }
                        if (leadLen > 0) {
#endif
                            // init with first value. This may be NaN!
                           
                            fcomplex curMax = *(fcomplex*)pRead; // first non-NaN element
                            while (leadLen-- > 0) {
                                tmp = (*(fcomplex*)(pRead + 0 * accuStride)); if (tmp  < curMax) curMax = tmp;
                                pRead += accuStride;
                            }
                            *(fcomplex*)pDest = curMax;
#if !ISINTEGERTYPE

                        } else {
                            *(fcomplex*)pDest = fcomplex.NaN;
                        }
#endif
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

        /// <summary>Computes the minimum of elements of <paramref name="A"/> along the specified dimension.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <param name="dim">The index of the dimension to be reduced.</param>
        /// <param name="I">[Optional] Output: 0-based index values of the found elements along the working dimension. Default: (null) don't return the indices.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length min(1,A.S[dim]) and removed if <paramref name="keepdim"/> is false.</returns>
        /// <remarks><para>Empty arrays are handled in the same way as other shapes: the dimension specified by <paramref name="dim"/> is reduced to 1. If A.S[dim] == 0 the 
        /// <paramref name="dim"/>s dimension length in the array returned will also be 0.</para>
        /// <para>Scalar arrays: numpy scalars give a numpy scalar (0 dim) as output.</para>
        /// <para>Special floating point values (for floating point element types): positive and negative infinity are handled in the regular way. NaN ('not a number') values are ignored. If 
        /// all elements in a working dimension are NaN, NaN is returned as result in the corresponding output element.</para>
        /// <para>If the optional output parameter <paramref name="I"/> is not null on entry the function computes and returns the indices in <paramref name="A"/> of the 
        /// values returned. Thus, <paramref name="I"/> has the same shape as the return array. If <paramref name="I"/> is null on entry (default) 
        /// no indices are computated (may or may not decreasing the computational effort of completing this function).</para>
        /// <para>Stability: if <paramref name="I"/> is requested and multiple elements along the working dimension in <paramref name="A"/> have the 
        /// same minimum/maximum value it is undefined which element is 'picked' for the output. Hence, in this case the corresponding value in <paramref name="I"/> 
        /// is undefined. It may point to the first or any other element storing the smallest / largest value returned from the working dimension. Thus, Min() and Max() are <i>unstable</i> 
        /// regarding the index output argument!</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim"/> was false.</exception>
        
        internal unsafe static Array<complex> min(BaseArray<complex> A, OutArray<long> I = null, int dim = -1, bool keepdim = true) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.Min.Complex.Instance.operate(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, 
                                                                               I, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Min {

            internal class Complex :

            ReduceSameTypeBaseIndices<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> {

                internal static Complex Instance = new Complex();

                // implementation from ReduceSameTypeBase (no indices)
                public unsafe override void Strided64Indices(byte* pSrc, byte* pDest, byte* pIndices, long start, long len, long* bsdIn, long* bsdOut) {

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
                    System.Diagnostics.Debug.Assert(pIndices != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)bsdIn[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims;
                    long* outIdxStrides = bsdOut + 6 + ndims * 3; // 3 + ndims * 2 + 3 + ndims; 
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;

                    long* cur = stackalloc long[Math.Max((int)ndims, 1)]; // assumes stackalloc to clear values (unspecified!)
                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    long f = cur[0] = start;
                    {
                        int i = 0;
                        for (; f > 0 && i < ndims; i++) {
                            cur[i] = f % (dims[i] + 1);
                            f /= (dims[i] + 1);
                            pSrc += cur[i] * inStrides[i];
                            pDest += cur[i] * outStrides[i];
                            pIndices += cur[i] * outIdxStrides[i];
                        }
                        while (i < ndims) { // finish initializing cur
                            cur[i++] = 0;
                        }
                    }
                    System.Diagnostics.Debug.Assert(f == 0);

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long i = 0;
                        // likely to cause a register to be used in the following
                        long curIdx = -1;
#if !ISINTEGERTYPE
                        // find first non-nan
                        while (i < accuDim && (complex.IsNaN(*(complex*)(pSrc + i * accuStride)))) {
                            i++;
                        }
                        if (i < accuDim) {
#endif
                         
                        complex curVal = *(complex*)(pSrc + i * accuStride); curIdx = i++; // first non-NaN element
                            while (i < accuDim) {
                                if (*(complex*)(pSrc + i * accuStride)  < curVal) { curVal = *(complex*)(pSrc + i * accuStride); curIdx = i; }
                                i++;
                            }
                            *(complex*)pDest = curVal;
                            *(long*)pIndices = curIdx;
#if !ISINTEGERTYPE
                        } else {
                            *(complex*)pDest = complex.NaN;
                            *(long*)pIndices = -1;
                        }
#endif
                        if (len-- == 0) {
                            break;
                        }

                        // increase current position
                        int d = 0;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pDest += outStrides[d];
                                pIndices += outIdxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pDest -= outStrides[d] * dims[d];
                                pIndices -= outIdxStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
                // implementation from ReduceSameTypeBase (no indices)
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
                    
                    long* cur = stackalloc long[Math.Max((int)ndims,1)]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    long f = cur[0] = start;
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];
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
                        // likely to cause a register to be used in the following
                       
                        complex tmp; 
                        byte* pRead = pSrc; 
#if !ISINTEGERTYPE
                        // find first non-nan
                        while (leadLen > 0 && (complex.IsNaN(*(complex*)pRead))) {
                            pRead += accuStride; leadLen--; 
                        }
                        if (leadLen > 0) {
#endif
                            // init with first value. This may be NaN!
                           
                            complex curMax = *(complex*)pRead; // first non-NaN element
                            while (leadLen-- > 0) {
                                tmp = (*(complex*)(pRead + 0 * accuStride)); if (tmp  < curMax) curMax = tmp;
                                pRead += accuStride;
                            }
                            *(complex*)pDest = curMax;
#if !ISINTEGERTYPE

                        } else {
                            *(complex*)pDest = complex.NaN;
                        }
#endif
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

        /// <summary>Computes the minimum of elements of <paramref name="A"/> along the specified dimension.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <param name="dim">The index of the dimension to be reduced.</param>
        /// <param name="I">[Optional] Output: 0-based index values of the found elements along the working dimension. Default: (null) don't return the indices.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length min(1,A.S[dim]) and removed if <paramref name="keepdim"/> is false.</returns>
        /// <remarks><para>Empty arrays are handled in the same way as other shapes: the dimension specified by <paramref name="dim"/> is reduced to 1. If A.S[dim] == 0 the 
        /// <paramref name="dim"/>s dimension length in the array returned will also be 0.</para>
        /// <para>Scalar arrays: numpy scalars give a numpy scalar (0 dim) as output.</para>
        /// <para>Special floating point values (for floating point element types): positive and negative infinity are handled in the regular way. NaN ('not a number') values are ignored. If 
        /// all elements in a working dimension are NaN, NaN is returned as result in the corresponding output element.</para>
        /// <para>If the optional output parameter <paramref name="I"/> is not null on entry the function computes and returns the indices in <paramref name="A"/> of the 
        /// values returned. Thus, <paramref name="I"/> has the same shape as the return array. If <paramref name="I"/> is null on entry (default) 
        /// no indices are computated (may or may not decreasing the computational effort of completing this function).</para>
        /// <para>Stability: if <paramref name="I"/> is requested and multiple elements along the working dimension in <paramref name="A"/> have the 
        /// same minimum/maximum value it is undefined which element is 'picked' for the output. Hence, in this case the corresponding value in <paramref name="I"/> 
        /// is undefined. It may point to the first or any other element storing the smallest / largest value returned from the working dimension. Thus, Min() and Max() are <i>unstable</i> 
        /// regarding the index output argument!</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim"/> was false.</exception>
        
        internal unsafe static Array<float> min(BaseArray<float> A, OutArray<long> I = null, int dim = -1, bool keepdim = true) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.Min.Single.Instance.operate(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, 
                                                                               I, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Min {

            internal class Single :

            ReduceSameTypeBaseIndices<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> {

                internal static Single Instance = new Single();

                // implementation from ReduceSameTypeBase (no indices)
                public unsafe override void Strided64Indices(byte* pSrc, byte* pDest, byte* pIndices, long start, long len, long* bsdIn, long* bsdOut) {

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
                    System.Diagnostics.Debug.Assert(pIndices != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)bsdIn[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims;
                    long* outIdxStrides = bsdOut + 6 + ndims * 3; // 3 + ndims * 2 + 3 + ndims; 
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;

                    long* cur = stackalloc long[Math.Max((int)ndims, 1)]; // assumes stackalloc to clear values (unspecified!)
                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    long f = cur[0] = start;
                    {
                        int i = 0;
                        for (; f > 0 && i < ndims; i++) {
                            cur[i] = f % (dims[i] + 1);
                            f /= (dims[i] + 1);
                            pSrc += cur[i] * inStrides[i];
                            pDest += cur[i] * outStrides[i];
                            pIndices += cur[i] * outIdxStrides[i];
                        }
                        while (i < ndims) { // finish initializing cur
                            cur[i++] = 0;
                        }
                    }
                    System.Diagnostics.Debug.Assert(f == 0);

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long i = 0;
                        // likely to cause a register to be used in the following
                        long curIdx = -1;
#if !ISINTEGERTYPE
                        // find first non-nan
                        while (i < accuDim && (float.IsNaN(*(float*)(pSrc + i * accuStride)))) {
                            i++;
                        }
                        if (i < accuDim) {
#endif
                         
                        float curVal = *(float*)(pSrc + i * accuStride); curIdx = i++; // first non-NaN element
                            while (i < accuDim) {
                                if (*(float*)(pSrc + i * accuStride)  < curVal) { curVal = *(float*)(pSrc + i * accuStride); curIdx = i; }
                                i++;
                            }
                            *(float*)pDest = curVal;
                            *(long*)pIndices = curIdx;
#if !ISINTEGERTYPE
                        } else {
                            *(float*)pDest = float.NaN;
                            *(long*)pIndices = -1;
                        }
#endif
                        if (len-- == 0) {
                            break;
                        }

                        // increase current position
                        int d = 0;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pDest += outStrides[d];
                                pIndices += outIdxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pDest -= outStrides[d] * dims[d];
                                pIndices -= outIdxStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
                // implementation from ReduceSameTypeBase (no indices)
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
                    
                    long* cur = stackalloc long[Math.Max((int)ndims,1)]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    long f = cur[0] = start;
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];
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
                        // likely to cause a register to be used in the following
                       
                        float tmp; 
                        byte* pRead = pSrc; 
#if !ISINTEGERTYPE
                        // find first non-nan
                        while (leadLen > 0 && (float.IsNaN(*(float*)pRead))) {
                            pRead += accuStride; leadLen--; 
                        }
                        if (leadLen > 0) {
#endif
                            // init with first value. This may be NaN!
                           
                            float curMax = *(float*)pRead; // first non-NaN element
                            while (leadLen-- > 0) {
                                tmp = (*(float*)(pRead + 0 * accuStride)); if (tmp  < curMax) curMax = tmp;
                                pRead += accuStride;
                            }
                            *(float*)pDest = curMax;
#if !ISINTEGERTYPE

                        } else {
                            *(float*)pDest = float.NaN;
                        }
#endif
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

        /// <summary>Computes the minimum of elements of <paramref name="A"/> along the specified dimension.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <param name="dim">The index of the dimension to be reduced.</param>
        /// <param name="I">[Optional] Output: 0-based index values of the found elements along the working dimension. Default: (null) don't return the indices.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length min(1,A.S[dim]) and removed if <paramref name="keepdim"/> is false.</returns>
        /// <remarks><para>Empty arrays are handled in the same way as other shapes: the dimension specified by <paramref name="dim"/> is reduced to 1. If A.S[dim] == 0 the 
        /// <paramref name="dim"/>s dimension length in the array returned will also be 0.</para>
        /// <para>Scalar arrays: numpy scalars give a numpy scalar (0 dim) as output.</para>
        /// <para>Special floating point values (for floating point element types): positive and negative infinity are handled in the regular way. NaN ('not a number') values are ignored. If 
        /// all elements in a working dimension are NaN, NaN is returned as result in the corresponding output element.</para>
        /// <para>If the optional output parameter <paramref name="I"/> is not null on entry the function computes and returns the indices in <paramref name="A"/> of the 
        /// values returned. Thus, <paramref name="I"/> has the same shape as the return array. If <paramref name="I"/> is null on entry (default) 
        /// no indices are computated (may or may not decreasing the computational effort of completing this function).</para>
        /// <para>Stability: if <paramref name="I"/> is requested and multiple elements along the working dimension in <paramref name="A"/> have the 
        /// same minimum/maximum value it is undefined which element is 'picked' for the output. Hence, in this case the corresponding value in <paramref name="I"/> 
        /// is undefined. It may point to the first or any other element storing the smallest / largest value returned from the working dimension. Thus, Min() and Max() are <i>unstable</i> 
        /// regarding the index output argument!</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim"/> was false.</exception>
        
        internal unsafe static Array<long> min(BaseArray<long> A, OutArray<long> I = null, int dim = -1, bool keepdim = true) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.Min.Int64.Instance.operate(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, 
                                                                               I, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Min {

            internal class Int64 :

            ReduceSameTypeBaseIndices<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> {

                internal static Int64 Instance = new Int64();

                // implementation from ReduceSameTypeBase (no indices)
                public unsafe override void Strided64Indices(byte* pSrc, byte* pDest, byte* pIndices, long start, long len, long* bsdIn, long* bsdOut) {

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
                    System.Diagnostics.Debug.Assert(pIndices != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)bsdIn[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims;
                    long* outIdxStrides = bsdOut + 6 + ndims * 3; // 3 + ndims * 2 + 3 + ndims; 
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;

                    long* cur = stackalloc long[Math.Max((int)ndims, 1)]; // assumes stackalloc to clear values (unspecified!)
                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    long f = cur[0] = start;
                    {
                        int i = 0;
                        for (; f > 0 && i < ndims; i++) {
                            cur[i] = f % (dims[i] + 1);
                            f /= (dims[i] + 1);
                            pSrc += cur[i] * inStrides[i];
                            pDest += cur[i] * outStrides[i];
                            pIndices += cur[i] * outIdxStrides[i];
                        }
                        while (i < ndims) { // finish initializing cur
                            cur[i++] = 0;
                        }
                    }
                    System.Diagnostics.Debug.Assert(f == 0);

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long i = 0;
                        // likely to cause a register to be used in the following
                        long curIdx = -1;
#if ISINTEGERTYPE
                        // find first non-nan
                        while (i < accuDim && (long.IsNaN(*(long*)(pSrc + i * accuStride)))) {
                            i++;
                        }
                        if (i < accuDim) {
#endif
                         
                        long curVal = *(long*)(pSrc + i * accuStride); curIdx = i++; // first non-NaN element
                            while (i < accuDim) {
                                if (*(long*)(pSrc + i * accuStride)  < curVal) { curVal = *(long*)(pSrc + i * accuStride); curIdx = i; }
                                i++;
                            }
                            *(long*)pDest = curVal;
                            *(long*)pIndices = curIdx;
#if ISINTEGERTYPE
                        } else {
                            *(long*)pDest = long.NaN;
                            *(long*)pIndices = -1;
                        }
#endif
                        if (len-- == 0) {
                            break;
                        }

                        // increase current position
                        int d = 0;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pDest += outStrides[d];
                                pIndices += outIdxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pDest -= outStrides[d] * dims[d];
                                pIndices -= outIdxStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
                // implementation from ReduceSameTypeBase (no indices)
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
                    
                    long* cur = stackalloc long[Math.Max((int)ndims,1)]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    long f = cur[0] = start;
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];
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
                        // likely to cause a register to be used in the following
                       
                        long tmp; 
                        byte* pRead = pSrc; 
#if ISINTEGERTYPE
                        // find first non-nan
                        while (leadLen > 0 && (long.IsNaN(*(long*)pRead))) {
                            pRead += accuStride; leadLen--; 
                        }
                        if (leadLen > 0) {
#endif
                            // init with first value. This may be NaN!
                           
                            long curMax = *(long*)pRead; // first non-NaN element
                            while (leadLen-- > 0) {
                                tmp = (*(long*)(pRead + 0 * accuStride)); if (tmp  < curMax) curMax = tmp;
                                pRead += accuStride;
                            }
                            *(long*)pDest = curMax;
#if ISINTEGERTYPE

                        } else {
                            *(long*)pDest = long.NaN;
                        }
#endif
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

        /// <summary>Computes the minimum of elements of <paramref name="A"/> along the specified dimension.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <param name="dim">The index of the dimension to be reduced.</param>
        /// <param name="I">[Optional] Output: 0-based index values of the found elements along the working dimension. Default: (null) don't return the indices.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length min(1,A.S[dim]) and removed if <paramref name="keepdim"/> is false.</returns>
        /// <remarks><para>Empty arrays are handled in the same way as other shapes: the dimension specified by <paramref name="dim"/> is reduced to 1. If A.S[dim] == 0 the 
        /// <paramref name="dim"/>s dimension length in the array returned will also be 0.</para>
        /// <para>Scalar arrays: numpy scalars give a numpy scalar (0 dim) as output.</para>
        /// <para>Special floating point values (for floating point element types): positive and negative infinity are handled in the regular way. NaN ('not a number') values are ignored. If 
        /// all elements in a working dimension are NaN, NaN is returned as result in the corresponding output element.</para>
        /// <para>If the optional output parameter <paramref name="I"/> is not null on entry the function computes and returns the indices in <paramref name="A"/> of the 
        /// values returned. Thus, <paramref name="I"/> has the same shape as the return array. If <paramref name="I"/> is null on entry (default) 
        /// no indices are computated (may or may not decreasing the computational effort of completing this function).</para>
        /// <para>Stability: if <paramref name="I"/> is requested and multiple elements along the working dimension in <paramref name="A"/> have the 
        /// same minimum/maximum value it is undefined which element is 'picked' for the output. Hence, in this case the corresponding value in <paramref name="I"/> 
        /// is undefined. It may point to the first or any other element storing the smallest / largest value returned from the working dimension. Thus, Min() and Max() are <i>unstable</i> 
        /// regarding the index output argument!</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim"/> was false.</exception>
        
        internal unsafe static Array<ulong> min(BaseArray<ulong> A, OutArray<long> I = null, int dim = -1, bool keepdim = true) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.Min.UInt64.Instance.operate(A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>, 
                                                                               I, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Min {

            internal class UInt64 :

            ReduceSameTypeBaseIndices<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> {

                internal static UInt64 Instance = new UInt64();

                // implementation from ReduceSameTypeBase (no indices)
                public unsafe override void Strided64Indices(byte* pSrc, byte* pDest, byte* pIndices, long start, long len, long* bsdIn, long* bsdOut) {

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
                    System.Diagnostics.Debug.Assert(pIndices != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)bsdIn[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims;
                    long* outIdxStrides = bsdOut + 6 + ndims * 3; // 3 + ndims * 2 + 3 + ndims; 
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;

                    long* cur = stackalloc long[Math.Max((int)ndims, 1)]; // assumes stackalloc to clear values (unspecified!)
                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    long f = cur[0] = start;
                    {
                        int i = 0;
                        for (; f > 0 && i < ndims; i++) {
                            cur[i] = f % (dims[i] + 1);
                            f /= (dims[i] + 1);
                            pSrc += cur[i] * inStrides[i];
                            pDest += cur[i] * outStrides[i];
                            pIndices += cur[i] * outIdxStrides[i];
                        }
                        while (i < ndims) { // finish initializing cur
                            cur[i++] = 0;
                        }
                    }
                    System.Diagnostics.Debug.Assert(f == 0);

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long i = 0;
                        // likely to cause a register to be used in the following
                        long curIdx = -1;
#if ISINTEGERTYPE
                        // find first non-nan
                        while (i < accuDim && (ulong.IsNaN(*(ulong*)(pSrc + i * accuStride)))) {
                            i++;
                        }
                        if (i < accuDim) {
#endif
                         
                        ulong curVal = *(ulong*)(pSrc + i * accuStride); curIdx = i++; // first non-NaN element
                            while (i < accuDim) {
                                if (*(ulong*)(pSrc + i * accuStride)  < curVal) { curVal = *(ulong*)(pSrc + i * accuStride); curIdx = i; }
                                i++;
                            }
                            *(ulong*)pDest = curVal;
                            *(long*)pIndices = curIdx;
#if ISINTEGERTYPE
                        } else {
                            *(ulong*)pDest = ulong.NaN;
                            *(long*)pIndices = -1;
                        }
#endif
                        if (len-- == 0) {
                            break;
                        }

                        // increase current position
                        int d = 0;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pDest += outStrides[d];
                                pIndices += outIdxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pDest -= outStrides[d] * dims[d];
                                pIndices -= outIdxStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
                // implementation from ReduceSameTypeBase (no indices)
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
                    
                    long* cur = stackalloc long[Math.Max((int)ndims,1)]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    long f = cur[0] = start;
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];
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
                        // likely to cause a register to be used in the following
                       
                        ulong tmp; 
                        byte* pRead = pSrc; 
#if ISINTEGERTYPE
                        // find first non-nan
                        while (leadLen > 0 && (ulong.IsNaN(*(ulong*)pRead))) {
                            pRead += accuStride; leadLen--; 
                        }
                        if (leadLen > 0) {
#endif
                            // init with first value. This may be NaN!
                           
                            ulong curMax = *(ulong*)pRead; // first non-NaN element
                            while (leadLen-- > 0) {
                                tmp = (*(ulong*)(pRead + 0 * accuStride)); if (tmp  < curMax) curMax = tmp;
                                pRead += accuStride;
                            }
                            *(ulong*)pDest = curMax;
#if ISINTEGERTYPE

                        } else {
                            *(ulong*)pDest = ulong.NaN;
                        }
#endif
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

        /// <summary>Computes the minimum of elements of <paramref name="A"/> along the specified dimension.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <param name="dim">The index of the dimension to be reduced.</param>
        /// <param name="I">[Optional] Output: 0-based index values of the found elements along the working dimension. Default: (null) don't return the indices.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length min(1,A.S[dim]) and removed if <paramref name="keepdim"/> is false.</returns>
        /// <remarks><para>Empty arrays are handled in the same way as other shapes: the dimension specified by <paramref name="dim"/> is reduced to 1. If A.S[dim] == 0 the 
        /// <paramref name="dim"/>s dimension length in the array returned will also be 0.</para>
        /// <para>Scalar arrays: numpy scalars give a numpy scalar (0 dim) as output.</para>
        /// <para>Special floating point values (for floating point element types): positive and negative infinity are handled in the regular way. NaN ('not a number') values are ignored. If 
        /// all elements in a working dimension are NaN, NaN is returned as result in the corresponding output element.</para>
        /// <para>If the optional output parameter <paramref name="I"/> is not null on entry the function computes and returns the indices in <paramref name="A"/> of the 
        /// values returned. Thus, <paramref name="I"/> has the same shape as the return array. If <paramref name="I"/> is null on entry (default) 
        /// no indices are computated (may or may not decreasing the computational effort of completing this function).</para>
        /// <para>Stability: if <paramref name="I"/> is requested and multiple elements along the working dimension in <paramref name="A"/> have the 
        /// same minimum/maximum value it is undefined which element is 'picked' for the output. Hence, in this case the corresponding value in <paramref name="I"/> 
        /// is undefined. It may point to the first or any other element storing the smallest / largest value returned from the working dimension. Thus, Min() and Max() are <i>unstable</i> 
        /// regarding the index output argument!</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim"/> was false.</exception>
        
        internal unsafe static Array<int> min(BaseArray<int> A, OutArray<long> I = null, int dim = -1, bool keepdim = true) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.Min.Int32.Instance.operate(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>, 
                                                                               I, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Min {

            internal class Int32 :

            ReduceSameTypeBaseIndices<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> {

                internal static Int32 Instance = new Int32();

                // implementation from ReduceSameTypeBase (no indices)
                public unsafe override void Strided64Indices(byte* pSrc, byte* pDest, byte* pIndices, long start, long len, long* bsdIn, long* bsdOut) {

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
                    System.Diagnostics.Debug.Assert(pIndices != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)bsdIn[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims;
                    long* outIdxStrides = bsdOut + 6 + ndims * 3; // 3 + ndims * 2 + 3 + ndims; 
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;

                    long* cur = stackalloc long[Math.Max((int)ndims, 1)]; // assumes stackalloc to clear values (unspecified!)
                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    long f = cur[0] = start;
                    {
                        int i = 0;
                        for (; f > 0 && i < ndims; i++) {
                            cur[i] = f % (dims[i] + 1);
                            f /= (dims[i] + 1);
                            pSrc += cur[i] * inStrides[i];
                            pDest += cur[i] * outStrides[i];
                            pIndices += cur[i] * outIdxStrides[i];
                        }
                        while (i < ndims) { // finish initializing cur
                            cur[i++] = 0;
                        }
                    }
                    System.Diagnostics.Debug.Assert(f == 0);

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long i = 0;
                        // likely to cause a register to be used in the following
                        long curIdx = -1;
#if ISINTEGERTYPE
                        // find first non-nan
                        while (i < accuDim && (int.IsNaN(*(int*)(pSrc + i * accuStride)))) {
                            i++;
                        }
                        if (i < accuDim) {
#endif
                         
                        int curVal = *(int*)(pSrc + i * accuStride); curIdx = i++; // first non-NaN element
                            while (i < accuDim) {
                                if (*(int*)(pSrc + i * accuStride)  < curVal) { curVal = *(int*)(pSrc + i * accuStride); curIdx = i; }
                                i++;
                            }
                            *(int*)pDest = curVal;
                            *(long*)pIndices = curIdx;
#if ISINTEGERTYPE
                        } else {
                            *(int*)pDest = int.NaN;
                            *(long*)pIndices = -1;
                        }
#endif
                        if (len-- == 0) {
                            break;
                        }

                        // increase current position
                        int d = 0;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pDest += outStrides[d];
                                pIndices += outIdxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pDest -= outStrides[d] * dims[d];
                                pIndices -= outIdxStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
                // implementation from ReduceSameTypeBase (no indices)
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
                    
                    long* cur = stackalloc long[Math.Max((int)ndims,1)]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    long f = cur[0] = start;
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];
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
                        // likely to cause a register to be used in the following
                       
                        int tmp; 
                        byte* pRead = pSrc; 
#if ISINTEGERTYPE
                        // find first non-nan
                        while (leadLen > 0 && (int.IsNaN(*(int*)pRead))) {
                            pRead += accuStride; leadLen--; 
                        }
                        if (leadLen > 0) {
#endif
                            // init with first value. This may be NaN!
                           
                            int curMax = *(int*)pRead; // first non-NaN element
                            while (leadLen-- > 0) {
                                tmp = (*(int*)(pRead + 0 * accuStride)); if (tmp  < curMax) curMax = tmp;
                                pRead += accuStride;
                            }
                            *(int*)pDest = curMax;
#if ISINTEGERTYPE

                        } else {
                            *(int*)pDest = int.NaN;
                        }
#endif
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

        /// <summary>Computes the minimum of elements of <paramref name="A"/> along the specified dimension.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <param name="dim">The index of the dimension to be reduced.</param>
        /// <param name="I">[Optional] Output: 0-based index values of the found elements along the working dimension. Default: (null) don't return the indices.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length min(1,A.S[dim]) and removed if <paramref name="keepdim"/> is false.</returns>
        /// <remarks><para>Empty arrays are handled in the same way as other shapes: the dimension specified by <paramref name="dim"/> is reduced to 1. If A.S[dim] == 0 the 
        /// <paramref name="dim"/>s dimension length in the array returned will also be 0.</para>
        /// <para>Scalar arrays: numpy scalars give a numpy scalar (0 dim) as output.</para>
        /// <para>Special floating point values (for floating point element types): positive and negative infinity are handled in the regular way. NaN ('not a number') values are ignored. If 
        /// all elements in a working dimension are NaN, NaN is returned as result in the corresponding output element.</para>
        /// <para>If the optional output parameter <paramref name="I"/> is not null on entry the function computes and returns the indices in <paramref name="A"/> of the 
        /// values returned. Thus, <paramref name="I"/> has the same shape as the return array. If <paramref name="I"/> is null on entry (default) 
        /// no indices are computated (may or may not decreasing the computational effort of completing this function).</para>
        /// <para>Stability: if <paramref name="I"/> is requested and multiple elements along the working dimension in <paramref name="A"/> have the 
        /// same minimum/maximum value it is undefined which element is 'picked' for the output. Hence, in this case the corresponding value in <paramref name="I"/> 
        /// is undefined. It may point to the first or any other element storing the smallest / largest value returned from the working dimension. Thus, Min() and Max() are <i>unstable</i> 
        /// regarding the index output argument!</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim"/> was false.</exception>
        
        internal unsafe static Array<uint> min(BaseArray<uint> A, OutArray<long> I = null, int dim = -1, bool keepdim = true) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.Min.UInt32.Instance.operate(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>, 
                                                                               I, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Min {

            internal class UInt32 :

            ReduceSameTypeBaseIndices<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> {

                internal static UInt32 Instance = new UInt32();

                // implementation from ReduceSameTypeBase (no indices)
                public unsafe override void Strided64Indices(byte* pSrc, byte* pDest, byte* pIndices, long start, long len, long* bsdIn, long* bsdOut) {

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
                    System.Diagnostics.Debug.Assert(pIndices != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)bsdIn[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims;
                    long* outIdxStrides = bsdOut + 6 + ndims * 3; // 3 + ndims * 2 + 3 + ndims; 
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;

                    long* cur = stackalloc long[Math.Max((int)ndims, 1)]; // assumes stackalloc to clear values (unspecified!)
                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    long f = cur[0] = start;
                    {
                        int i = 0;
                        for (; f > 0 && i < ndims; i++) {
                            cur[i] = f % (dims[i] + 1);
                            f /= (dims[i] + 1);
                            pSrc += cur[i] * inStrides[i];
                            pDest += cur[i] * outStrides[i];
                            pIndices += cur[i] * outIdxStrides[i];
                        }
                        while (i < ndims) { // finish initializing cur
                            cur[i++] = 0;
                        }
                    }
                    System.Diagnostics.Debug.Assert(f == 0);

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long i = 0;
                        // likely to cause a register to be used in the following
                        long curIdx = -1;
#if ISINTEGERTYPE
                        // find first non-nan
                        while (i < accuDim && (uint.IsNaN(*(uint*)(pSrc + i * accuStride)))) {
                            i++;
                        }
                        if (i < accuDim) {
#endif
                         
                        uint curVal = *(uint*)(pSrc + i * accuStride); curIdx = i++; // first non-NaN element
                            while (i < accuDim) {
                                if (*(uint*)(pSrc + i * accuStride)  < curVal) { curVal = *(uint*)(pSrc + i * accuStride); curIdx = i; }
                                i++;
                            }
                            *(uint*)pDest = curVal;
                            *(long*)pIndices = curIdx;
#if ISINTEGERTYPE
                        } else {
                            *(uint*)pDest = uint.NaN;
                            *(long*)pIndices = -1;
                        }
#endif
                        if (len-- == 0) {
                            break;
                        }

                        // increase current position
                        int d = 0;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pDest += outStrides[d];
                                pIndices += outIdxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pDest -= outStrides[d] * dims[d];
                                pIndices -= outIdxStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
                // implementation from ReduceSameTypeBase (no indices)
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
                    
                    long* cur = stackalloc long[Math.Max((int)ndims,1)]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    long f = cur[0] = start;
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];
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
                        // likely to cause a register to be used in the following
                       
                        uint tmp; 
                        byte* pRead = pSrc; 
#if ISINTEGERTYPE
                        // find first non-nan
                        while (leadLen > 0 && (uint.IsNaN(*(uint*)pRead))) {
                            pRead += accuStride; leadLen--; 
                        }
                        if (leadLen > 0) {
#endif
                            // init with first value. This may be NaN!
                           
                            uint curMax = *(uint*)pRead; // first non-NaN element
                            while (leadLen-- > 0) {
                                tmp = (*(uint*)(pRead + 0 * accuStride)); if (tmp  < curMax) curMax = tmp;
                                pRead += accuStride;
                            }
                            *(uint*)pDest = curMax;
#if ISINTEGERTYPE

                        } else {
                            *(uint*)pDest = uint.NaN;
                        }
#endif
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

        /// <summary>Computes the minimum of elements of <paramref name="A"/> along the specified dimension.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <param name="dim">The index of the dimension to be reduced.</param>
        /// <param name="I">[Optional] Output: 0-based index values of the found elements along the working dimension. Default: (null) don't return the indices.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length min(1,A.S[dim]) and removed if <paramref name="keepdim"/> is false.</returns>
        /// <remarks><para>Empty arrays are handled in the same way as other shapes: the dimension specified by <paramref name="dim"/> is reduced to 1. If A.S[dim] == 0 the 
        /// <paramref name="dim"/>s dimension length in the array returned will also be 0.</para>
        /// <para>Scalar arrays: numpy scalars give a numpy scalar (0 dim) as output.</para>
        /// <para>Special floating point values (for floating point element types): positive and negative infinity are handled in the regular way. NaN ('not a number') values are ignored. If 
        /// all elements in a working dimension are NaN, NaN is returned as result in the corresponding output element.</para>
        /// <para>If the optional output parameter <paramref name="I"/> is not null on entry the function computes and returns the indices in <paramref name="A"/> of the 
        /// values returned. Thus, <paramref name="I"/> has the same shape as the return array. If <paramref name="I"/> is null on entry (default) 
        /// no indices are computated (may or may not decreasing the computational effort of completing this function).</para>
        /// <para>Stability: if <paramref name="I"/> is requested and multiple elements along the working dimension in <paramref name="A"/> have the 
        /// same minimum/maximum value it is undefined which element is 'picked' for the output. Hence, in this case the corresponding value in <paramref name="I"/> 
        /// is undefined. It may point to the first or any other element storing the smallest / largest value returned from the working dimension. Thus, Min() and Max() are <i>unstable</i> 
        /// regarding the index output argument!</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim"/> was false.</exception>
        
        internal unsafe static Array<short> min(BaseArray<short> A, OutArray<long> I = null, int dim = -1, bool keepdim = true) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.Min.Int16.Instance.operate(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>, 
                                                                               I, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Min {

            internal class Int16 :

            ReduceSameTypeBaseIndices<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> {

                internal static Int16 Instance = new Int16();

                // implementation from ReduceSameTypeBase (no indices)
                public unsafe override void Strided64Indices(byte* pSrc, byte* pDest, byte* pIndices, long start, long len, long* bsdIn, long* bsdOut) {

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
                    System.Diagnostics.Debug.Assert(pIndices != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)bsdIn[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims;
                    long* outIdxStrides = bsdOut + 6 + ndims * 3; // 3 + ndims * 2 + 3 + ndims; 
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;

                    long* cur = stackalloc long[Math.Max((int)ndims, 1)]; // assumes stackalloc to clear values (unspecified!)
                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    long f = cur[0] = start;
                    {
                        int i = 0;
                        for (; f > 0 && i < ndims; i++) {
                            cur[i] = f % (dims[i] + 1);
                            f /= (dims[i] + 1);
                            pSrc += cur[i] * inStrides[i];
                            pDest += cur[i] * outStrides[i];
                            pIndices += cur[i] * outIdxStrides[i];
                        }
                        while (i < ndims) { // finish initializing cur
                            cur[i++] = 0;
                        }
                    }
                    System.Diagnostics.Debug.Assert(f == 0);

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long i = 0;
                        // likely to cause a register to be used in the following
                        long curIdx = -1;
#if ISINTEGERTYPE
                        // find first non-nan
                        while (i < accuDim && (short.IsNaN(*(short*)(pSrc + i * accuStride)))) {
                            i++;
                        }
                        if (i < accuDim) {
#endif
                         
                        short curVal = *(short*)(pSrc + i * accuStride); curIdx = i++; // first non-NaN element
                            while (i < accuDim) {
                                if (*(short*)(pSrc + i * accuStride)  < curVal) { curVal = *(short*)(pSrc + i * accuStride); curIdx = i; }
                                i++;
                            }
                            *(short*)pDest = curVal;
                            *(long*)pIndices = curIdx;
#if ISINTEGERTYPE
                        } else {
                            *(short*)pDest = short.NaN;
                            *(long*)pIndices = -1;
                        }
#endif
                        if (len-- == 0) {
                            break;
                        }

                        // increase current position
                        int d = 0;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pDest += outStrides[d];
                                pIndices += outIdxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pDest -= outStrides[d] * dims[d];
                                pIndices -= outIdxStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
                // implementation from ReduceSameTypeBase (no indices)
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
                    
                    long* cur = stackalloc long[Math.Max((int)ndims,1)]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    long f = cur[0] = start;
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];
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
                        // likely to cause a register to be used in the following
                       
                        short tmp; 
                        byte* pRead = pSrc; 
#if ISINTEGERTYPE
                        // find first non-nan
                        while (leadLen > 0 && (short.IsNaN(*(short*)pRead))) {
                            pRead += accuStride; leadLen--; 
                        }
                        if (leadLen > 0) {
#endif
                            // init with first value. This may be NaN!
                           
                            short curMax = *(short*)pRead; // first non-NaN element
                            while (leadLen-- > 0) {
                                tmp = (*(short*)(pRead + 0 * accuStride)); if (tmp  < curMax) curMax = tmp;
                                pRead += accuStride;
                            }
                            *(short*)pDest = curMax;
#if ISINTEGERTYPE

                        } else {
                            *(short*)pDest = short.NaN;
                        }
#endif
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

        /// <summary>Computes the minimum of elements of <paramref name="A"/> along the specified dimension.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <param name="dim">The index of the dimension to be reduced.</param>
        /// <param name="I">[Optional] Output: 0-based index values of the found elements along the working dimension. Default: (null) don't return the indices.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length min(1,A.S[dim]) and removed if <paramref name="keepdim"/> is false.</returns>
        /// <remarks><para>Empty arrays are handled in the same way as other shapes: the dimension specified by <paramref name="dim"/> is reduced to 1. If A.S[dim] == 0 the 
        /// <paramref name="dim"/>s dimension length in the array returned will also be 0.</para>
        /// <para>Scalar arrays: numpy scalars give a numpy scalar (0 dim) as output.</para>
        /// <para>Special floating point values (for floating point element types): positive and negative infinity are handled in the regular way. NaN ('not a number') values are ignored. If 
        /// all elements in a working dimension are NaN, NaN is returned as result in the corresponding output element.</para>
        /// <para>If the optional output parameter <paramref name="I"/> is not null on entry the function computes and returns the indices in <paramref name="A"/> of the 
        /// values returned. Thus, <paramref name="I"/> has the same shape as the return array. If <paramref name="I"/> is null on entry (default) 
        /// no indices are computated (may or may not decreasing the computational effort of completing this function).</para>
        /// <para>Stability: if <paramref name="I"/> is requested and multiple elements along the working dimension in <paramref name="A"/> have the 
        /// same minimum/maximum value it is undefined which element is 'picked' for the output. Hence, in this case the corresponding value in <paramref name="I"/> 
        /// is undefined. It may point to the first or any other element storing the smallest / largest value returned from the working dimension. Thus, Min() and Max() are <i>unstable</i> 
        /// regarding the index output argument!</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim"/> was false.</exception>
        
        internal unsafe static Array<ushort> min(BaseArray<ushort> A, OutArray<long> I = null, int dim = -1, bool keepdim = true) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.Min.UInt16.Instance.operate(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>, 
                                                                               I, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Min {

            internal class UInt16 :

            ReduceSameTypeBaseIndices<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> {

                internal static UInt16 Instance = new UInt16();

                // implementation from ReduceSameTypeBase (no indices)
                public unsafe override void Strided64Indices(byte* pSrc, byte* pDest, byte* pIndices, long start, long len, long* bsdIn, long* bsdOut) {

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
                    System.Diagnostics.Debug.Assert(pIndices != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)bsdIn[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims;
                    long* outIdxStrides = bsdOut + 6 + ndims * 3; // 3 + ndims * 2 + 3 + ndims; 
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;

                    long* cur = stackalloc long[Math.Max((int)ndims, 1)]; // assumes stackalloc to clear values (unspecified!)
                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    long f = cur[0] = start;
                    {
                        int i = 0;
                        for (; f > 0 && i < ndims; i++) {
                            cur[i] = f % (dims[i] + 1);
                            f /= (dims[i] + 1);
                            pSrc += cur[i] * inStrides[i];
                            pDest += cur[i] * outStrides[i];
                            pIndices += cur[i] * outIdxStrides[i];
                        }
                        while (i < ndims) { // finish initializing cur
                            cur[i++] = 0;
                        }
                    }
                    System.Diagnostics.Debug.Assert(f == 0);

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long i = 0;
                        // likely to cause a register to be used in the following
                        long curIdx = -1;
#if ISINTEGERTYPE
                        // find first non-nan
                        while (i < accuDim && (ushort.IsNaN(*(ushort*)(pSrc + i * accuStride)))) {
                            i++;
                        }
                        if (i < accuDim) {
#endif
                         
                        ushort curVal = *(ushort*)(pSrc + i * accuStride); curIdx = i++; // first non-NaN element
                            while (i < accuDim) {
                                if (*(ushort*)(pSrc + i * accuStride)  < curVal) { curVal = *(ushort*)(pSrc + i * accuStride); curIdx = i; }
                                i++;
                            }
                            *(ushort*)pDest = curVal;
                            *(long*)pIndices = curIdx;
#if ISINTEGERTYPE
                        } else {
                            *(ushort*)pDest = ushort.NaN;
                            *(long*)pIndices = -1;
                        }
#endif
                        if (len-- == 0) {
                            break;
                        }

                        // increase current position
                        int d = 0;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pDest += outStrides[d];
                                pIndices += outIdxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pDest -= outStrides[d] * dims[d];
                                pIndices -= outIdxStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
                // implementation from ReduceSameTypeBase (no indices)
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
                    
                    long* cur = stackalloc long[Math.Max((int)ndims,1)]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    long f = cur[0] = start;
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];
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
                        // likely to cause a register to be used in the following
                       
                        ushort tmp; 
                        byte* pRead = pSrc; 
#if ISINTEGERTYPE
                        // find first non-nan
                        while (leadLen > 0 && (ushort.IsNaN(*(ushort*)pRead))) {
                            pRead += accuStride; leadLen--; 
                        }
                        if (leadLen > 0) {
#endif
                            // init with first value. This may be NaN!
                           
                            ushort curMax = *(ushort*)pRead; // first non-NaN element
                            while (leadLen-- > 0) {
                                tmp = (*(ushort*)(pRead + 0 * accuStride)); if (tmp  < curMax) curMax = tmp;
                                pRead += accuStride;
                            }
                            *(ushort*)pDest = curMax;
#if ISINTEGERTYPE

                        } else {
                            *(ushort*)pDest = ushort.NaN;
                        }
#endif
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

        /// <summary>Computes the minimum of elements of <paramref name="A"/> along the specified dimension.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <param name="dim">The index of the dimension to be reduced.</param>
        /// <param name="I">[Optional] Output: 0-based index values of the found elements along the working dimension. Default: (null) don't return the indices.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length min(1,A.S[dim]) and removed if <paramref name="keepdim"/> is false.</returns>
        /// <remarks><para>Empty arrays are handled in the same way as other shapes: the dimension specified by <paramref name="dim"/> is reduced to 1. If A.S[dim] == 0 the 
        /// <paramref name="dim"/>s dimension length in the array returned will also be 0.</para>
        /// <para>Scalar arrays: numpy scalars give a numpy scalar (0 dim) as output.</para>
        /// <para>Special floating point values (for floating point element types): positive and negative infinity are handled in the regular way. NaN ('not a number') values are ignored. If 
        /// all elements in a working dimension are NaN, NaN is returned as result in the corresponding output element.</para>
        /// <para>If the optional output parameter <paramref name="I"/> is not null on entry the function computes and returns the indices in <paramref name="A"/> of the 
        /// values returned. Thus, <paramref name="I"/> has the same shape as the return array. If <paramref name="I"/> is null on entry (default) 
        /// no indices are computated (may or may not decreasing the computational effort of completing this function).</para>
        /// <para>Stability: if <paramref name="I"/> is requested and multiple elements along the working dimension in <paramref name="A"/> have the 
        /// same minimum/maximum value it is undefined which element is 'picked' for the output. Hence, in this case the corresponding value in <paramref name="I"/> 
        /// is undefined. It may point to the first or any other element storing the smallest / largest value returned from the working dimension. Thus, Min() and Max() are <i>unstable</i> 
        /// regarding the index output argument!</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim"/> was false.</exception>
        
        internal unsafe static Array<sbyte> min(BaseArray<sbyte> A, OutArray<long> I = null, int dim = -1, bool keepdim = true) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.Min.SByte.Instance.operate(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>, 
                                                                               I, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Min {

            internal class SByte :

            ReduceSameTypeBaseIndices<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> {

                internal static SByte Instance = new SByte();

                // implementation from ReduceSameTypeBase (no indices)
                public unsafe override void Strided64Indices(byte* pSrc, byte* pDest, byte* pIndices, long start, long len, long* bsdIn, long* bsdOut) {

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
                    System.Diagnostics.Debug.Assert(pIndices != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)bsdIn[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims;
                    long* outIdxStrides = bsdOut + 6 + ndims * 3; // 3 + ndims * 2 + 3 + ndims; 
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;

                    long* cur = stackalloc long[Math.Max((int)ndims, 1)]; // assumes stackalloc to clear values (unspecified!)
                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    long f = cur[0] = start;
                    {
                        int i = 0;
                        for (; f > 0 && i < ndims; i++) {
                            cur[i] = f % (dims[i] + 1);
                            f /= (dims[i] + 1);
                            pSrc += cur[i] * inStrides[i];
                            pDest += cur[i] * outStrides[i];
                            pIndices += cur[i] * outIdxStrides[i];
                        }
                        while (i < ndims) { // finish initializing cur
                            cur[i++] = 0;
                        }
                    }
                    System.Diagnostics.Debug.Assert(f == 0);

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long i = 0;
                        // likely to cause a register to be used in the following
                        long curIdx = -1;
#if ISINTEGERTYPE
                        // find first non-nan
                        while (i < accuDim && (sbyte.IsNaN(*(sbyte*)(pSrc + i * accuStride)))) {
                            i++;
                        }
                        if (i < accuDim) {
#endif
                         
                        sbyte curVal = *(sbyte*)(pSrc + i * accuStride); curIdx = i++; // first non-NaN element
                            while (i < accuDim) {
                                if (*(sbyte*)(pSrc + i * accuStride)  < curVal) { curVal = *(sbyte*)(pSrc + i * accuStride); curIdx = i; }
                                i++;
                            }
                            *(sbyte*)pDest = curVal;
                            *(long*)pIndices = curIdx;
#if ISINTEGERTYPE
                        } else {
                            *(sbyte*)pDest = sbyte.NaN;
                            *(long*)pIndices = -1;
                        }
#endif
                        if (len-- == 0) {
                            break;
                        }

                        // increase current position
                        int d = 0;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pDest += outStrides[d];
                                pIndices += outIdxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pDest -= outStrides[d] * dims[d];
                                pIndices -= outIdxStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
                // implementation from ReduceSameTypeBase (no indices)
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
                    
                    long* cur = stackalloc long[Math.Max((int)ndims,1)]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    long f = cur[0] = start;
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];
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
                        // likely to cause a register to be used in the following
                       
                        sbyte tmp; 
                        byte* pRead = pSrc; 
#if ISINTEGERTYPE
                        // find first non-nan
                        while (leadLen > 0 && (sbyte.IsNaN(*(sbyte*)pRead))) {
                            pRead += accuStride; leadLen--; 
                        }
                        if (leadLen > 0) {
#endif
                            // init with first value. This may be NaN!
                           
                            sbyte curMax = *(sbyte*)pRead; // first non-NaN element
                            while (leadLen-- > 0) {
                                tmp = (*(sbyte*)(pRead + 0 * accuStride)); if (tmp  < curMax) curMax = tmp;
                                pRead += accuStride;
                            }
                            *(sbyte*)pDest = curMax;
#if ISINTEGERTYPE

                        } else {
                            *(sbyte*)pDest = sbyte.NaN;
                        }
#endif
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

        /// <summary>Computes the minimum of elements of <paramref name="A"/> along the specified dimension.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <param name="dim">The index of the dimension to be reduced.</param>
        /// <param name="I">[Optional] Output: 0-based index values of the found elements along the working dimension. Default: (null) don't return the indices.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length min(1,A.S[dim]) and removed if <paramref name="keepdim"/> is false.</returns>
        /// <remarks><para>Empty arrays are handled in the same way as other shapes: the dimension specified by <paramref name="dim"/> is reduced to 1. If A.S[dim] == 0 the 
        /// <paramref name="dim"/>s dimension length in the array returned will also be 0.</para>
        /// <para>Scalar arrays: numpy scalars give a numpy scalar (0 dim) as output.</para>
        /// <para>Special floating point values (for floating point element types): positive and negative infinity are handled in the regular way. NaN ('not a number') values are ignored. If 
        /// all elements in a working dimension are NaN, NaN is returned as result in the corresponding output element.</para>
        /// <para>If the optional output parameter <paramref name="I"/> is not null on entry the function computes and returns the indices in <paramref name="A"/> of the 
        /// values returned. Thus, <paramref name="I"/> has the same shape as the return array. If <paramref name="I"/> is null on entry (default) 
        /// no indices are computated (may or may not decreasing the computational effort of completing this function).</para>
        /// <para>Stability: if <paramref name="I"/> is requested and multiple elements along the working dimension in <paramref name="A"/> have the 
        /// same minimum/maximum value it is undefined which element is 'picked' for the output. Hence, in this case the corresponding value in <paramref name="I"/> 
        /// is undefined. It may point to the first or any other element storing the smallest / largest value returned from the working dimension. Thus, Min() and Max() are <i>unstable</i> 
        /// regarding the index output argument!</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim"/> was false.</exception>
        
        internal unsafe static Array<byte> min(BaseArray<byte> A, OutArray<long> I = null, int dim = -1, bool keepdim = true) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.Min.Byte.Instance.operate(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>, 
                                                                               I, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Min {

            internal class Byte :

            ReduceSameTypeBaseIndices<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> {

                internal static Byte Instance = new Byte();

                // implementation from ReduceSameTypeBase (no indices)
                public unsafe override void Strided64Indices(byte* pSrc, byte* pDest, byte* pIndices, long start, long len, long* bsdIn, long* bsdOut) {

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
                    System.Diagnostics.Debug.Assert(pIndices != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)bsdIn[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims;
                    long* outIdxStrides = bsdOut + 6 + ndims * 3; // 3 + ndims * 2 + 3 + ndims; 
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;

                    long* cur = stackalloc long[Math.Max((int)ndims, 1)]; // assumes stackalloc to clear values (unspecified!)
                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    long f = cur[0] = start;
                    {
                        int i = 0;
                        for (; f > 0 && i < ndims; i++) {
                            cur[i] = f % (dims[i] + 1);
                            f /= (dims[i] + 1);
                            pSrc += cur[i] * inStrides[i];
                            pDest += cur[i] * outStrides[i];
                            pIndices += cur[i] * outIdxStrides[i];
                        }
                        while (i < ndims) { // finish initializing cur
                            cur[i++] = 0;
                        }
                    }
                    System.Diagnostics.Debug.Assert(f == 0);

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long i = 0;
                        // likely to cause a register to be used in the following
                        long curIdx = -1;
#if ISINTEGERTYPE
                        // find first non-nan
                        while (i < accuDim && (byte.IsNaN(*(byte*)(pSrc + i * accuStride)))) {
                            i++;
                        }
                        if (i < accuDim) {
#endif
                         
                        byte curVal = *(byte*)(pSrc + i * accuStride); curIdx = i++; // first non-NaN element
                            while (i < accuDim) {
                                if (*(byte*)(pSrc + i * accuStride)  < curVal) { curVal = *(byte*)(pSrc + i * accuStride); curIdx = i; }
                                i++;
                            }
                            *(byte*)pDest = curVal;
                            *(long*)pIndices = curIdx;
#if ISINTEGERTYPE
                        } else {
                            *(byte*)pDest = byte.NaN;
                            *(long*)pIndices = -1;
                        }
#endif
                        if (len-- == 0) {
                            break;
                        }

                        // increase current position
                        int d = 0;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pDest += outStrides[d];
                                pIndices += outIdxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pDest -= outStrides[d] * dims[d];
                                pIndices -= outIdxStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
                // implementation from ReduceSameTypeBase (no indices)
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
                    
                    long* cur = stackalloc long[Math.Max((int)ndims,1)]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    long f = cur[0] = start;
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];
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
                        // likely to cause a register to be used in the following
                       
                        byte tmp; 
                        byte* pRead = pSrc; 
#if ISINTEGERTYPE
                        // find first non-nan
                        while (leadLen > 0 && (byte.IsNaN(*(byte*)pRead))) {
                            pRead += accuStride; leadLen--; 
                        }
                        if (leadLen > 0) {
#endif
                            // init with first value. This may be NaN!
                           
                            byte curMax = *(byte*)pRead; // first non-NaN element
                            while (leadLen-- > 0) {
                                tmp = (*(byte*)(pRead + 0 * accuStride)); if (tmp  < curMax) curMax = tmp;
                                pRead += accuStride;
                            }
                            *(byte*)pDest = curMax;
#if ISINTEGERTYPE

                        } else {
                            *(byte*)pDest = byte.NaN;
                        }
#endif
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

        /// <summary>Computes the minimum of elements of <paramref name="A"/> along the specified dimension.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <param name="dim">The index of the dimension to be reduced.</param>
        /// <param name="I">[Optional] Output: 0-based index values of the found elements along the working dimension. Default: (null) don't return the indices.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length min(1,A.S[dim]) and removed if <paramref name="keepdim"/> is false.</returns>
        /// <remarks><para>Empty arrays are handled in the same way as other shapes: the dimension specified by <paramref name="dim"/> is reduced to 1. If A.S[dim] == 0 the 
        /// <paramref name="dim"/>s dimension length in the array returned will also be 0.</para>
        /// <para>Scalar arrays: numpy scalars give a numpy scalar (0 dim) as output.</para>
        /// <para>Special floating point values (for floating point element types): positive and negative infinity are handled in the regular way. NaN ('not a number') values are ignored. If 
        /// all elements in a working dimension are NaN, NaN is returned as result in the corresponding output element.</para>
        /// <para>If the optional output parameter <paramref name="I"/> is not null on entry the function computes and returns the indices in <paramref name="A"/> of the 
        /// values returned. Thus, <paramref name="I"/> has the same shape as the return array. If <paramref name="I"/> is null on entry (default) 
        /// no indices are computated (may or may not decreasing the computational effort of completing this function).</para>
        /// <para>Stability: if <paramref name="I"/> is requested and multiple elements along the working dimension in <paramref name="A"/> have the 
        /// same minimum/maximum value it is undefined which element is 'picked' for the output. Hence, in this case the corresponding value in <paramref name="I"/> 
        /// is undefined. It may point to the first or any other element storing the smallest / largest value returned from the working dimension. Thus, Min() and Max() are <i>unstable</i> 
        /// regarding the index output argument!</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim"/> was false.</exception>
        
        internal unsafe static Array<double> min(BaseArray<double> A, OutArray<long> I = null, int dim = -1, bool keepdim = true) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.Min.Double.Instance.operate(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, 
                                                                               I, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Min {

            internal class Double :

            ReduceSameTypeBaseIndices<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> {

                internal static Double Instance = new Double();

                // implementation from ReduceSameTypeBase (no indices)
                public unsafe override void Strided64Indices(byte* pSrc, byte* pDest, byte* pIndices, long start, long len, long* bsdIn, long* bsdOut) {

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
                    System.Diagnostics.Debug.Assert(pIndices != (byte*)0);
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)bsdIn[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims;
                    long* outIdxStrides = bsdOut + 6 + ndims * 3; // 3 + ndims * 2 + 3 + ndims; 
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;

                    long* cur = stackalloc long[Math.Max((int)ndims, 1)]; // assumes stackalloc to clear values (unspecified!)
                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    long f = cur[0] = start;
                    {
                        int i = 0;
                        for (; f > 0 && i < ndims; i++) {
                            cur[i] = f % (dims[i] + 1);
                            f /= (dims[i] + 1);
                            pSrc += cur[i] * inStrides[i];
                            pDest += cur[i] * outStrides[i];
                            pIndices += cur[i] * outIdxStrides[i];
                        }
                        while (i < ndims) { // finish initializing cur
                            cur[i++] = 0;
                        }
                    }
                    System.Diagnostics.Debug.Assert(f == 0);

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long i = 0;
                        // likely to cause a register to be used in the following
                        long curIdx = -1;
#if !ISINTEGERTYPE
                        // find first non-nan
                        while (i < accuDim && (double.IsNaN(*(double*)(pSrc + i * accuStride)))) {
                            i++;
                        }
                        if (i < accuDim) {
#endif
                         
                        double curVal = *(double*)(pSrc + i * accuStride); curIdx = i++; // first non-NaN element
                            while (i < accuDim) {
                                if (*(double*)(pSrc + i * accuStride)  < curVal) { curVal = *(double*)(pSrc + i * accuStride); curIdx = i; }
                                i++;
                            }
                            *(double*)pDest = curVal;
                            *(long*)pIndices = curIdx;
#if !ISINTEGERTYPE
                        } else {
                            *(double*)pDest = double.NaN;
                            *(long*)pIndices = -1;
                        }
#endif
                        if (len-- == 0) {
                            break;
                        }

                        // increase current position
                        int d = 0;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pDest += outStrides[d];
                                pIndices += outIdxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pDest -= outStrides[d] * dims[d];
                                pIndices -= outIdxStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
                // implementation from ReduceSameTypeBase (no indices)
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
                    
                    long* cur = stackalloc long[Math.Max((int)ndims,1)]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    long f = cur[0] = start;
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];
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
                        // likely to cause a register to be used in the following
                       
                        double tmp; 
                        byte* pRead = pSrc; 
#if !ISINTEGERTYPE
                        // find first non-nan
                        while (leadLen > 0 && (double.IsNaN(*(double*)pRead))) {
                            pRead += accuStride; leadLen--; 
                        }
                        if (leadLen > 0) {
#endif
                            // init with first value. This may be NaN!
                           
                            double curMax = *(double*)pRead; // first non-NaN element
                            while (leadLen-- > 0) {
                                tmp = (*(double*)(pRead + 0 * accuStride)); if (tmp  < curMax) curMax = tmp;
                                pRead += accuStride;
                            }
                            *(double*)pDest = curMax;
#if !ISINTEGERTYPE

                        } else {
                            *(double*)pDest = double.NaN;
                        }
#endif
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

