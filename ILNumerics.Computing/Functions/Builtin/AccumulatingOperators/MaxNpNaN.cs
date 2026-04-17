//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////

using ILNumerics;
using ILNumerics.Core.Arrays;
using ILNumerics.Core.Functions.Builtin;
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
    internal static partial class numpyInternal {

        /// <summary>
        /// Computes the maximum of elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
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
        /// values returned. Thus, <paramref name="I"/> has the same shape as the return array. If the storage of <paramref name="I"/> on entry is sufficient 
        /// (i.e.: large enough and not shared with other arrays) it will be directly used for storing the indices and I is 
        /// reshaped as required. Otherwise, a new array is created and returned. In order to safe the index computation 
        /// null can be provided as <paramref name="I"/> which is the default.</para>
        /// <para>If <paramref name="I"/> is requested and multiple elements along the working dimension in <paramref name="A"/> have the same maximum value it is undefined which 
        /// element is 'picked' for the output. Hence, in this case the corresponding value in <paramref name="I"/> is undefined and may point to the first or any other occurrence of the 
        /// value returned from the working dimension.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim"/> was false.</exception>
        internal unsafe static /*!HC:RetCls*/Array</*!HC:outArr*/double> /*!HC:funcname*/max(BaseArray<double> A, OutArray<long> I = null, int dim = -1, bool keepdim = true) {
            if (object.Equals(A, null)) {
                return null;
            }
            return Core.Functions.Builtin.InnerLoops./*!HC:innerloopname*/Max_npNaN.Double.Instance.operate(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>,
                                                                               I, dim, keepdim);
        }
    }
    namespace Core.Functions.Builtin.InnerLoops {

        namespace /*!HC:innerloopname*/Max_npNaN {

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
                    for (int i = 1; i < ndims; i++) { // cur[0] is sure set below
                        cur[i] = 0; 
                    }
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
                        long curIdx = 0;
                        /*!HC:outArr*/
                        double curVal = *(/*!HC:outArr*/double*)(pSrc + i * accuStride), tmp; // first non-NaN element
                        while (i < accuDim) {
                            tmp = *(/*!HC:outArr*/double*)(pSrc + i * accuStride);
                            if (/*!HC:outArr*/double.IsNaN(tmp)) { curVal = tmp; curIdx = i; break; }
                            if (tmp/*!HC:operatorfunc*/ > curVal) { curVal = tmp; curIdx = i; }
                            i++;
                        }
                        *(/*!HC:outArr*/double*)pDest = curVal;
                        *(long*)pIndices = curIdx;

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
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;

                    long* cur = stackalloc long[Math.Max((int)ndims, 1)]; // assumes stackalloc to clear values (unspecified!)

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
                    while (i < ndims) {
                        cur[i++] = 0; 
                    }
                    System.Diagnostics.Debug.Assert(f == 0);

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long leadLen = accuDim;
                        // likely to cause a register to be used in the following
                        /*!HC:outArr*/
                        double tmp;
                        byte* pRead = pSrc;
                        // init with first value. This may be NaN!
                        /*!HC:outArr*/
                        double curVal = *(/*!HC:outArr*/double*)pRead; 
                        while (leadLen-- > 0) {
                            tmp = (*(/*!HC:outArr*/double*)(pRead + 0 * accuStride)); if (/*!HC:outArr*/double.IsNaN(tmp)) { curVal = tmp; break; } if (tmp /*!HC:operatorfunc*/ > curVal) curVal = tmp;
                            pRead += accuStride;
                        }
                        *(/*!HC:outArr*/double*)pDest = curVal;

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
   
    internal static partial class numpyInternal {

        /// <summary>
        /// Computes the maximum of elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
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
        /// values returned. Thus, <paramref name="I"/> has the same shape as the return array. If the storage of <paramref name="I"/> on entry is sufficient 
        /// (i.e.: large enough and not shared with other arrays) it will be directly used for storing the indices and I is 
        /// reshaped as required. Otherwise, a new array is created and returned. In order to safe the index computation 
        /// null can be provided as <paramref name="I"/> which is the default.</para>
        /// <para>If <paramref name="I"/> is requested and multiple elements along the working dimension in <paramref name="A"/> have the same maximum value it is undefined which 
        /// element is 'picked' for the output. Hence, in this case the corresponding value in <paramref name="I"/> is undefined and may point to the first or any other occurrence of the 
        /// value returned from the working dimension.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim"/> was false.</exception>
        internal unsafe static Array<fcomplex> max(BaseArray<fcomplex> A, OutArray<long> I = null, int dim = -1, bool keepdim = true) {
            if (object.Equals(A, null)) {
                return null;
            }
            return Core.Functions.Builtin.InnerLoops.Max_npNaN.FComplex.Instance.operate(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>,
                                                                               I, dim, keepdim);
        }
    }
    namespace Core.Functions.Builtin.InnerLoops {

        namespace Max_npNaN {

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
                    for (int i = 1; i < ndims; i++) { // cur[0] is sure set below
                        cur[i] = 0; 
                    }
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
                        long curIdx = 0;
                       
                        fcomplex curVal = *(fcomplex*)(pSrc + i * accuStride), tmp; // first non-NaN element
                        while (i < accuDim) {
                            tmp = *(fcomplex*)(pSrc + i * accuStride);
                            if (fcomplex.IsNaN(tmp)) { curVal = tmp; curIdx = i; break; }
                            if (tmp > curVal) { curVal = tmp; curIdx = i; }
                            i++;
                        }
                        *(fcomplex*)pDest = curVal;
                        *(long*)pIndices = curIdx;

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
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;

                    long* cur = stackalloc long[Math.Max((int)ndims, 1)]; // assumes stackalloc to clear values (unspecified!)

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
                    while (i < ndims) {
                        cur[i++] = 0; 
                    }
                    System.Diagnostics.Debug.Assert(f == 0);

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long leadLen = accuDim;
                        // likely to cause a register to be used in the following
                       
                        fcomplex tmp;
                        byte* pRead = pSrc;
                        // init with first value. This may be NaN!
                       
                        fcomplex curVal = *(fcomplex*)pRead; 
                        while (leadLen-- > 0) {
                            tmp = (*(fcomplex*)(pRead + 0 * accuStride)); if (fcomplex.IsNaN(tmp)) { curVal = tmp; break; } if (tmp  > curVal) curVal = tmp;
                            pRead += accuStride;
                        }
                        *(fcomplex*)pDest = curVal;

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
   
    internal static partial class numpyInternal {

        /// <summary>
        /// Computes the maximum of elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
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
        /// values returned. Thus, <paramref name="I"/> has the same shape as the return array. If the storage of <paramref name="I"/> on entry is sufficient 
        /// (i.e.: large enough and not shared with other arrays) it will be directly used for storing the indices and I is 
        /// reshaped as required. Otherwise, a new array is created and returned. In order to safe the index computation 
        /// null can be provided as <paramref name="I"/> which is the default.</para>
        /// <para>If <paramref name="I"/> is requested and multiple elements along the working dimension in <paramref name="A"/> have the same maximum value it is undefined which 
        /// element is 'picked' for the output. Hence, in this case the corresponding value in <paramref name="I"/> is undefined and may point to the first or any other occurrence of the 
        /// value returned from the working dimension.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim"/> was false.</exception>
        internal unsafe static Array<complex> max(BaseArray<complex> A, OutArray<long> I = null, int dim = -1, bool keepdim = true) {
            if (object.Equals(A, null)) {
                return null;
            }
            return Core.Functions.Builtin.InnerLoops.Max_npNaN.Complex.Instance.operate(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>,
                                                                               I, dim, keepdim);
        }
    }
    namespace Core.Functions.Builtin.InnerLoops {

        namespace Max_npNaN {

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
                    for (int i = 1; i < ndims; i++) { // cur[0] is sure set below
                        cur[i] = 0; 
                    }
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
                        long curIdx = 0;
                       
                        complex curVal = *(complex*)(pSrc + i * accuStride), tmp; // first non-NaN element
                        while (i < accuDim) {
                            tmp = *(complex*)(pSrc + i * accuStride);
                            if (complex.IsNaN(tmp)) { curVal = tmp; curIdx = i; break; }
                            if (tmp > curVal) { curVal = tmp; curIdx = i; }
                            i++;
                        }
                        *(complex*)pDest = curVal;
                        *(long*)pIndices = curIdx;

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
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;

                    long* cur = stackalloc long[Math.Max((int)ndims, 1)]; // assumes stackalloc to clear values (unspecified!)

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
                    while (i < ndims) {
                        cur[i++] = 0; 
                    }
                    System.Diagnostics.Debug.Assert(f == 0);

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long leadLen = accuDim;
                        // likely to cause a register to be used in the following
                       
                        complex tmp;
                        byte* pRead = pSrc;
                        // init with first value. This may be NaN!
                       
                        complex curVal = *(complex*)pRead; 
                        while (leadLen-- > 0) {
                            tmp = (*(complex*)(pRead + 0 * accuStride)); if (complex.IsNaN(tmp)) { curVal = tmp; break; } if (tmp  > curVal) curVal = tmp;
                            pRead += accuStride;
                        }
                        *(complex*)pDest = curVal;

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
   
    internal static partial class numpyInternal {

        /// <summary>
        /// Computes the maximum of elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
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
        /// values returned. Thus, <paramref name="I"/> has the same shape as the return array. If the storage of <paramref name="I"/> on entry is sufficient 
        /// (i.e.: large enough and not shared with other arrays) it will be directly used for storing the indices and I is 
        /// reshaped as required. Otherwise, a new array is created and returned. In order to safe the index computation 
        /// null can be provided as <paramref name="I"/> which is the default.</para>
        /// <para>If <paramref name="I"/> is requested and multiple elements along the working dimension in <paramref name="A"/> have the same maximum value it is undefined which 
        /// element is 'picked' for the output. Hence, in this case the corresponding value in <paramref name="I"/> is undefined and may point to the first or any other occurrence of the 
        /// value returned from the working dimension.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim"/> was false.</exception>
        internal unsafe static Array<float> max(BaseArray<float> A, OutArray<long> I = null, int dim = -1, bool keepdim = true) {
            if (object.Equals(A, null)) {
                return null;
            }
            return Core.Functions.Builtin.InnerLoops.Max_npNaN.Single.Instance.operate(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>,
                                                                               I, dim, keepdim);
        }
    }
    namespace Core.Functions.Builtin.InnerLoops {

        namespace Max_npNaN {

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
                    for (int i = 1; i < ndims; i++) { // cur[0] is sure set below
                        cur[i] = 0; 
                    }
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
                        long curIdx = 0;
                       
                        float curVal = *(float*)(pSrc + i * accuStride), tmp; // first non-NaN element
                        while (i < accuDim) {
                            tmp = *(float*)(pSrc + i * accuStride);
                            if (float.IsNaN(tmp)) { curVal = tmp; curIdx = i; break; }
                            if (tmp > curVal) { curVal = tmp; curIdx = i; }
                            i++;
                        }
                        *(float*)pDest = curVal;
                        *(long*)pIndices = curIdx;

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
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;

                    long* cur = stackalloc long[Math.Max((int)ndims, 1)]; // assumes stackalloc to clear values (unspecified!)

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
                    while (i < ndims) {
                        cur[i++] = 0; 
                    }
                    System.Diagnostics.Debug.Assert(f == 0);

                    while (true) {

                        // iteration always goes along the accumulation length, out-element by out-element..
                        long leadLen = accuDim;
                        // likely to cause a register to be used in the following
                       
                        float tmp;
                        byte* pRead = pSrc;
                        // init with first value. This may be NaN!
                       
                        float curVal = *(float*)pRead; 
                        while (leadLen-- > 0) {
                            tmp = (*(float*)(pRead + 0 * accuStride)); if (float.IsNaN(tmp)) { curVal = tmp; break; } if (tmp  > curVal) curVal = tmp;
                            pRead += accuStride;
                        }
                        *(float*)pDest = curVal;

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

