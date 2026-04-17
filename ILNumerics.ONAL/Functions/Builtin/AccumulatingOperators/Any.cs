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
    <destination>bool</destination>
    </type>
    <type>
    <source locate="after" endmark=" *()">
        innerElemType
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
    <destination>byte</destination>
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
    <destination>Bool</destination>
    </type>
    <type>
    <source locate="comment">
        summary
    </source>
    <destination><![CDATA[Determines whether any element of <paramref name="A"/> along the given dimension <paramref name="dim"/>]]> is not zero.</destination>
    <destination><![CDATA[Determines whether any element of <paramref name="A"/> along the given dimension <paramref name="dim"/>]]> is not zero.</destination>
    <destination><![CDATA[Determines whether any element of <paramref name="A"/> along the given dimension <paramref name="dim"/>]]> is not zero.</destination>
    <destination><![CDATA[Determines whether any element of <paramref name="A"/> along the given dimension <paramref name="dim"/>]]> is not zero.</destination>
    <destination><![CDATA[Determines whether any element of <paramref name="A"/> along the given dimension <paramref name="dim"/>]]> is not zero.</destination>
    <destination><![CDATA[Determines whether any element of <paramref name="A"/> along the given dimension <paramref name="dim"/>]]> is not zero.</destination>
    <destination><![CDATA[Determines whether any element of <paramref name="A"/> along the given dimension <paramref name="dim"/>]]> is not zero.</destination>
    <destination><![CDATA[Determines whether any element of <paramref name="A"/> along the given dimension <paramref name="dim"/>]]> is not zero.</destination>
    <destination><![CDATA[Determines whether any element of <paramref name="A"/> along the given dimension <paramref name="dim"/>]]> is not zero.</destination>
    <destination><![CDATA[Determines whether any element of <paramref name="A"/> along the given dimension <paramref name="dim"/>]]> is not zero.</destination>
    <destination><![CDATA[Determines whether any element of <paramref name="A"/> along the given dimension <paramref name="dim"/>]]> is not zero.</destination>
    <destination><![CDATA[Determines whether any element of <paramref name="A"/> along the given dimension <paramref name="dim"/>]]> is true.</destination>
    </type>
    <type>
    <source locate="here">
        <![CDATA[<bool,Array<bool>,InArray<bool>,OutArray<bool>,Array<bool>,Storage<bool>]]>
    </source>
    <destination></destination>
    <destination></destination>
    <destination></destination>
    <destination></destination>
    <destination></destination>
    <destination></destination>
    <destination></destination>
    <destination></destination>
    <destination></destination>
    <destination></destination>
    <destination></destination>
    <destination><![CDATA[<bool,Logical,InLogical,OutLogical,Logical,LogicalStorage]]></destination>
    </type>
    </hycalper>
    */
    #region HYCALPER LOOPSTART UNARY_OPERATOR_TEMPLATE
    internal static partial class MathInternal {

        /// <summary>Determines whether any element of <paramref name="A"/> along the given dimension <paramref name="dim"/> is not zero.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] The index of the dimension to be reduced. Default: (-1) the first non-singleton dimension found in <paramref name="A"/> or 0.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length 1.</returns>
        /// <remarks><para>For floating point elements: special floating point values (<see cref="float.NaN"/>, <see cref="float.PositiveInfinity"/>, etc.; being 'not zero') 
        /// evaluate to 'true'.</para>
        /// <para>Empty arrays are allowed. In difference to <see cref="sum(BaseArray{float}, int, bool)"/> or 
        /// <see cref="prod(BaseArray{float}, int, bool)"/> dimension <paramref name="dim"/> may be of length 0, which leads to a 
        /// singleton dimension in the result and the non-existing elements evaluate to 'false'.</para>
        /// <para>The default value for <paramref name="dim"/> is -1. It causes the function to determine the working dimension (numpy: 'axis') automatically. 
        /// This search is based on <see cref="Size.WorkingDimension()"/> and starts looking at the first dimension for <see cref="Settings.ArrayStyle"/> <see cref="ArrayStyles.ILNumericsV4"/>. 
        /// For <see cref="Settings.ArrayStyle"/> <see cref="ArrayStyles.numpy"/>, however, the search starts with the last dimension and works its way up to the first dimension. 
        /// If the array does not contain any non-singleton dimension 0 is returned in both cases.</para>
        /// </remarks>
        /// <see cref="Size.WorkingDimension"/>
        /// <see cref="Settings.ArrayStyle"/>
        
        internal unsafe static Logical /*!HC:funcname*/any(BaseArray<double> A, int dim = -1, bool keepdim = true) {
            return InnerLoops.Any.Double.Instance.operate(
                A as ConcreteArray<double,Array<double>,InArray<double>,OutArray<double>,Array<double>,Storage<double>>, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Any {

            internal class Double :

            ReduceOtherTypeBase<double,Array<double>,InArray<double>,OutArray<double>,Array<double>,Storage<double>,
                        bool, Logical, InLogical, OutLogical, Logical, LogicalStorage> {

                internal static Double Instance = new Double();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut, long* nrTrues) {

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
                    if (ndims == 0) {
                        pDest += bsdOut[2];
                        // Note: this is used by any() and all(). Both test on non-zero (but in a different way below). 
                        // Therefore, we don't need to modify the 'HC:operatorfunc' != 0 in the next line. It works this 
                        // (same) way for both functions. 
                        var scalarVal = (*(/*!HC:innerElemType*/double*)(pSrc + bsdIn[2]) != 0) ? (byte)1 : (byte)0;
                        *nrTrues = *pDest = scalarVal;
                        return; 
                    }
                    System.Diagnostics.Debug.Assert(ndims > 0, $"Invalid BSD for input array detected: ndims = {ndims}.");

                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims;
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;

                    long* cur = stackalloc long[Math.Max((int)ndims,1)]; 

                    nrTrues[0] = 0; 
                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    //if (ndims == 0) { // WRONG?? TODO! test: reducing numpy 1D vector to np scalar
                    //    *pDest = *pSrc != 0 ? (byte)1 : (byte)0;
                    //    *nrTrues = (long)pDest[0];
                    //    return; // TODO: check!
                    //}

                    long f = cur[0] = start;
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

                        byte* pRead = pSrc;

                        while (leadLen > 0) {
                            if ((*(/*!HC:innerElemType*/double*)pRead) /*!HC:operatorfunc*/ != 0) break;
                            pRead += accuStride; leadLen--; 
                        }
                        *pDest = leadLen /*!HC:operatorfunc*/ > 0 ? (byte)1 : (byte)0;
                        *nrTrues += (long)pDest[0];

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

    #endregion HYCALPER LOOPEND 
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
    internal static partial class MathInternal {

        /// <summary>Determines whether any element of <paramref name="A"/> along the given dimension <paramref name="dim"/> is true.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] The index of the dimension to be reduced. Default: (-1) the first non-singleton dimension found in <paramref name="A"/> or 0.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length 1.</returns>
        /// <remarks><para>For floating point elements: special floating point values (<see cref="float.NaN"/>, <see cref="float.PositiveInfinity"/>, etc.; being 'not zero') 
        /// evaluate to 'true'.</para>
        /// <para>Empty arrays are allowed. In difference to <see cref="sum(BaseArray{float}, int, bool)"/> or 
        /// <see cref="prod(BaseArray{float}, int, bool)"/> dimension <paramref name="dim"/> may be of length 0, which leads to a 
        /// singleton dimension in the result and the non-existing elements evaluate to 'false'.</para>
        /// <para>The default value for <paramref name="dim"/> is -1. It causes the function to determine the working dimension (numpy: 'axis') automatically. 
        /// This search is based on <see cref="Size.WorkingDimension()"/> and starts looking at the first dimension for <see cref="Settings.ArrayStyle"/> <see cref="ArrayStyles.ILNumericsV4"/>. 
        /// For <see cref="Settings.ArrayStyle"/> <see cref="ArrayStyles.numpy"/>, however, the search starts with the last dimension and works its way up to the first dimension. 
        /// If the array does not contain any non-singleton dimension 0 is returned in both cases.</para>
        /// </remarks>
        /// <see cref="Size.WorkingDimension"/>
        /// <see cref="Settings.ArrayStyle"/>
        
        internal unsafe static Logical any(BaseArray<bool> A, int dim = -1, bool keepdim = true) {
            return InnerLoops.Any.Bool.Instance.operate(
                A as ConcreteArray<bool,Logical,InLogical,OutLogical,Logical,LogicalStorage>, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Any {

            internal class Bool :

            ReduceOtherTypeBase<bool,Logical,InLogical,OutLogical,Logical,LogicalStorage,
                        bool, Logical, InLogical, OutLogical, Logical, LogicalStorage> {

                internal static Bool Instance = new Bool();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut, long* nrTrues) {

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
                    if (ndims == 0) {
                        pDest += bsdOut[2];
                        // Note: this is used by any() and all(). Both test on non-zero (but in a different way below). 
                        // Therefore, we don't need to modify the 'HC:operatorfunc' != 0 in the next line. It works this 
                        // (same) way for both functions. 
                        var scalarVal = (*(byte*)(pSrc + bsdIn[2]) != 0) ? (byte)1 : (byte)0;
                        *nrTrues = *pDest = scalarVal;
                        return; 
                    }
                    System.Diagnostics.Debug.Assert(ndims > 0, $"Invalid BSD for input array detected: ndims = {ndims}.");

                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims;
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;

                    long* cur = stackalloc long[Math.Max((int)ndims,1)]; 

                    nrTrues[0] = 0; 
                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    //if (ndims == 0) { // WRONG?? TODO! test: reducing numpy 1D vector to np scalar
                    //    *pDest = *pSrc != 0 ? (byte)1 : (byte)0;
                    //    *nrTrues = (long)pDest[0];
                    //    return; // TODO: check!
                    //}

                    long f = cur[0] = start;
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

                        byte* pRead = pSrc;

                        while (leadLen > 0) {
                            if ((*(byte*)pRead)  != 0) break;
                            pRead += accuStride; leadLen--; 
                        }
                        *pDest = leadLen  > 0 ? (byte)1 : (byte)0;
                        *nrTrues += (long)pDest[0];

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

        /// <summary>Determines whether any element of <paramref name="A"/> along the given dimension <paramref name="dim"/> is not zero.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] The index of the dimension to be reduced. Default: (-1) the first non-singleton dimension found in <paramref name="A"/> or 0.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length 1.</returns>
        /// <remarks><para>For floating point elements: special floating point values (<see cref="float.NaN"/>, <see cref="float.PositiveInfinity"/>, etc.; being 'not zero') 
        /// evaluate to 'true'.</para>
        /// <para>Empty arrays are allowed. In difference to <see cref="sum(BaseArray{float}, int, bool)"/> or 
        /// <see cref="prod(BaseArray{float}, int, bool)"/> dimension <paramref name="dim"/> may be of length 0, which leads to a 
        /// singleton dimension in the result and the non-existing elements evaluate to 'false'.</para>
        /// <para>The default value for <paramref name="dim"/> is -1. It causes the function to determine the working dimension (numpy: 'axis') automatically. 
        /// This search is based on <see cref="Size.WorkingDimension()"/> and starts looking at the first dimension for <see cref="Settings.ArrayStyle"/> <see cref="ArrayStyles.ILNumericsV4"/>. 
        /// For <see cref="Settings.ArrayStyle"/> <see cref="ArrayStyles.numpy"/>, however, the search starts with the last dimension and works its way up to the first dimension. 
        /// If the array does not contain any non-singleton dimension 0 is returned in both cases.</para>
        /// </remarks>
        /// <see cref="Size.WorkingDimension"/>
        /// <see cref="Settings.ArrayStyle"/>
        
        internal unsafe static Logical any(BaseArray<fcomplex> A, int dim = -1, bool keepdim = true) {
            return InnerLoops.Any.FComplex.Instance.operate(
                A as ConcreteArray<fcomplex,Array<fcomplex>,InArray<fcomplex>,OutArray<fcomplex>,Array<fcomplex>,Storage<fcomplex>>, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Any {

            internal class FComplex :

            ReduceOtherTypeBase<fcomplex,Array<fcomplex>,InArray<fcomplex>,OutArray<fcomplex>,Array<fcomplex>,Storage<fcomplex>,
                        bool, Logical, InLogical, OutLogical, Logical, LogicalStorage> {

                internal static FComplex Instance = new FComplex();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut, long* nrTrues) {

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
                    if (ndims == 0) {
                        pDest += bsdOut[2];
                        // Note: this is used by any() and all(). Both test on non-zero (but in a different way below). 
                        // Therefore, we don't need to modify the 'HC:operatorfunc' != 0 in the next line. It works this 
                        // (same) way for both functions. 
                        var scalarVal = (*(fcomplex*)(pSrc + bsdIn[2]) != 0) ? (byte)1 : (byte)0;
                        *nrTrues = *pDest = scalarVal;
                        return; 
                    }
                    System.Diagnostics.Debug.Assert(ndims > 0, $"Invalid BSD for input array detected: ndims = {ndims}.");

                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims;
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;

                    long* cur = stackalloc long[Math.Max((int)ndims,1)]; 

                    nrTrues[0] = 0; 
                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    //if (ndims == 0) { // WRONG?? TODO! test: reducing numpy 1D vector to np scalar
                    //    *pDest = *pSrc != 0 ? (byte)1 : (byte)0;
                    //    *nrTrues = (long)pDest[0];
                    //    return; // TODO: check!
                    //}

                    long f = cur[0] = start;
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

                        byte* pRead = pSrc;

                        while (leadLen > 0) {
                            if ((*(fcomplex*)pRead)  != 0) break;
                            pRead += accuStride; leadLen--; 
                        }
                        *pDest = leadLen  > 0 ? (byte)1 : (byte)0;
                        *nrTrues += (long)pDest[0];

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

        /// <summary>Determines whether any element of <paramref name="A"/> along the given dimension <paramref name="dim"/> is not zero.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] The index of the dimension to be reduced. Default: (-1) the first non-singleton dimension found in <paramref name="A"/> or 0.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length 1.</returns>
        /// <remarks><para>For floating point elements: special floating point values (<see cref="float.NaN"/>, <see cref="float.PositiveInfinity"/>, etc.; being 'not zero') 
        /// evaluate to 'true'.</para>
        /// <para>Empty arrays are allowed. In difference to <see cref="sum(BaseArray{float}, int, bool)"/> or 
        /// <see cref="prod(BaseArray{float}, int, bool)"/> dimension <paramref name="dim"/> may be of length 0, which leads to a 
        /// singleton dimension in the result and the non-existing elements evaluate to 'false'.</para>
        /// <para>The default value for <paramref name="dim"/> is -1. It causes the function to determine the working dimension (numpy: 'axis') automatically. 
        /// This search is based on <see cref="Size.WorkingDimension()"/> and starts looking at the first dimension for <see cref="Settings.ArrayStyle"/> <see cref="ArrayStyles.ILNumericsV4"/>. 
        /// For <see cref="Settings.ArrayStyle"/> <see cref="ArrayStyles.numpy"/>, however, the search starts with the last dimension and works its way up to the first dimension. 
        /// If the array does not contain any non-singleton dimension 0 is returned in both cases.</para>
        /// </remarks>
        /// <see cref="Size.WorkingDimension"/>
        /// <see cref="Settings.ArrayStyle"/>
        
        internal unsafe static Logical any(BaseArray<complex> A, int dim = -1, bool keepdim = true) {
            return InnerLoops.Any.Complex.Instance.operate(
                A as ConcreteArray<complex,Array<complex>,InArray<complex>,OutArray<complex>,Array<complex>,Storage<complex>>, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Any {

            internal class Complex :

            ReduceOtherTypeBase<complex,Array<complex>,InArray<complex>,OutArray<complex>,Array<complex>,Storage<complex>,
                        bool, Logical, InLogical, OutLogical, Logical, LogicalStorage> {

                internal static Complex Instance = new Complex();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut, long* nrTrues) {

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
                    if (ndims == 0) {
                        pDest += bsdOut[2];
                        // Note: this is used by any() and all(). Both test on non-zero (but in a different way below). 
                        // Therefore, we don't need to modify the 'HC:operatorfunc' != 0 in the next line. It works this 
                        // (same) way for both functions. 
                        var scalarVal = (*(complex*)(pSrc + bsdIn[2]) != 0) ? (byte)1 : (byte)0;
                        *nrTrues = *pDest = scalarVal;
                        return; 
                    }
                    System.Diagnostics.Debug.Assert(ndims > 0, $"Invalid BSD for input array detected: ndims = {ndims}.");

                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims;
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;

                    long* cur = stackalloc long[Math.Max((int)ndims,1)]; 

                    nrTrues[0] = 0; 
                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    //if (ndims == 0) { // WRONG?? TODO! test: reducing numpy 1D vector to np scalar
                    //    *pDest = *pSrc != 0 ? (byte)1 : (byte)0;
                    //    *nrTrues = (long)pDest[0];
                    //    return; // TODO: check!
                    //}

                    long f = cur[0] = start;
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

                        byte* pRead = pSrc;

                        while (leadLen > 0) {
                            if ((*(complex*)pRead)  != 0) break;
                            pRead += accuStride; leadLen--; 
                        }
                        *pDest = leadLen  > 0 ? (byte)1 : (byte)0;
                        *nrTrues += (long)pDest[0];

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

        /// <summary>Determines whether any element of <paramref name="A"/> along the given dimension <paramref name="dim"/> is not zero.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] The index of the dimension to be reduced. Default: (-1) the first non-singleton dimension found in <paramref name="A"/> or 0.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length 1.</returns>
        /// <remarks><para>For floating point elements: special floating point values (<see cref="float.NaN"/>, <see cref="float.PositiveInfinity"/>, etc.; being 'not zero') 
        /// evaluate to 'true'.</para>
        /// <para>Empty arrays are allowed. In difference to <see cref="sum(BaseArray{float}, int, bool)"/> or 
        /// <see cref="prod(BaseArray{float}, int, bool)"/> dimension <paramref name="dim"/> may be of length 0, which leads to a 
        /// singleton dimension in the result and the non-existing elements evaluate to 'false'.</para>
        /// <para>The default value for <paramref name="dim"/> is -1. It causes the function to determine the working dimension (numpy: 'axis') automatically. 
        /// This search is based on <see cref="Size.WorkingDimension()"/> and starts looking at the first dimension for <see cref="Settings.ArrayStyle"/> <see cref="ArrayStyles.ILNumericsV4"/>. 
        /// For <see cref="Settings.ArrayStyle"/> <see cref="ArrayStyles.numpy"/>, however, the search starts with the last dimension and works its way up to the first dimension. 
        /// If the array does not contain any non-singleton dimension 0 is returned in both cases.</para>
        /// </remarks>
        /// <see cref="Size.WorkingDimension"/>
        /// <see cref="Settings.ArrayStyle"/>
        
        internal unsafe static Logical any(BaseArray<float> A, int dim = -1, bool keepdim = true) {
            return InnerLoops.Any.Single.Instance.operate(
                A as ConcreteArray<float,Array<float>,InArray<float>,OutArray<float>,Array<float>,Storage<float>>, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Any {

            internal class Single :

            ReduceOtherTypeBase<float,Array<float>,InArray<float>,OutArray<float>,Array<float>,Storage<float>,
                        bool, Logical, InLogical, OutLogical, Logical, LogicalStorage> {

                internal static Single Instance = new Single();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut, long* nrTrues) {

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
                    if (ndims == 0) {
                        pDest += bsdOut[2];
                        // Note: this is used by any() and all(). Both test on non-zero (but in a different way below). 
                        // Therefore, we don't need to modify the 'HC:operatorfunc' != 0 in the next line. It works this 
                        // (same) way for both functions. 
                        var scalarVal = (*(float*)(pSrc + bsdIn[2]) != 0) ? (byte)1 : (byte)0;
                        *nrTrues = *pDest = scalarVal;
                        return; 
                    }
                    System.Diagnostics.Debug.Assert(ndims > 0, $"Invalid BSD for input array detected: ndims = {ndims}.");

                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims;
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;

                    long* cur = stackalloc long[Math.Max((int)ndims,1)]; 

                    nrTrues[0] = 0; 
                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    //if (ndims == 0) { // WRONG?? TODO! test: reducing numpy 1D vector to np scalar
                    //    *pDest = *pSrc != 0 ? (byte)1 : (byte)0;
                    //    *nrTrues = (long)pDest[0];
                    //    return; // TODO: check!
                    //}

                    long f = cur[0] = start;
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

                        byte* pRead = pSrc;

                        while (leadLen > 0) {
                            if ((*(float*)pRead)  != 0) break;
                            pRead += accuStride; leadLen--; 
                        }
                        *pDest = leadLen  > 0 ? (byte)1 : (byte)0;
                        *nrTrues += (long)pDest[0];

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

        /// <summary>Determines whether any element of <paramref name="A"/> along the given dimension <paramref name="dim"/> is not zero.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] The index of the dimension to be reduced. Default: (-1) the first non-singleton dimension found in <paramref name="A"/> or 0.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length 1.</returns>
        /// <remarks><para>For floating point elements: special floating point values (<see cref="float.NaN"/>, <see cref="float.PositiveInfinity"/>, etc.; being 'not zero') 
        /// evaluate to 'true'.</para>
        /// <para>Empty arrays are allowed. In difference to <see cref="sum(BaseArray{float}, int, bool)"/> or 
        /// <see cref="prod(BaseArray{float}, int, bool)"/> dimension <paramref name="dim"/> may be of length 0, which leads to a 
        /// singleton dimension in the result and the non-existing elements evaluate to 'false'.</para>
        /// <para>The default value for <paramref name="dim"/> is -1. It causes the function to determine the working dimension (numpy: 'axis') automatically. 
        /// This search is based on <see cref="Size.WorkingDimension()"/> and starts looking at the first dimension for <see cref="Settings.ArrayStyle"/> <see cref="ArrayStyles.ILNumericsV4"/>. 
        /// For <see cref="Settings.ArrayStyle"/> <see cref="ArrayStyles.numpy"/>, however, the search starts with the last dimension and works its way up to the first dimension. 
        /// If the array does not contain any non-singleton dimension 0 is returned in both cases.</para>
        /// </remarks>
        /// <see cref="Size.WorkingDimension"/>
        /// <see cref="Settings.ArrayStyle"/>
        
        internal unsafe static Logical any(BaseArray<long> A, int dim = -1, bool keepdim = true) {
            return InnerLoops.Any.Int64.Instance.operate(
                A as ConcreteArray<long,Array<long>,InArray<long>,OutArray<long>,Array<long>,Storage<long>>, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Any {

            internal class Int64 :

            ReduceOtherTypeBase<long,Array<long>,InArray<long>,OutArray<long>,Array<long>,Storage<long>,
                        bool, Logical, InLogical, OutLogical, Logical, LogicalStorage> {

                internal static Int64 Instance = new Int64();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut, long* nrTrues) {

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
                    if (ndims == 0) {
                        pDest += bsdOut[2];
                        // Note: this is used by any() and all(). Both test on non-zero (but in a different way below). 
                        // Therefore, we don't need to modify the 'HC:operatorfunc' != 0 in the next line. It works this 
                        // (same) way for both functions. 
                        var scalarVal = (*(long*)(pSrc + bsdIn[2]) != 0) ? (byte)1 : (byte)0;
                        *nrTrues = *pDest = scalarVal;
                        return; 
                    }
                    System.Diagnostics.Debug.Assert(ndims > 0, $"Invalid BSD for input array detected: ndims = {ndims}.");

                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims;
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;

                    long* cur = stackalloc long[Math.Max((int)ndims,1)]; 

                    nrTrues[0] = 0; 
                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    //if (ndims == 0) { // WRONG?? TODO! test: reducing numpy 1D vector to np scalar
                    //    *pDest = *pSrc != 0 ? (byte)1 : (byte)0;
                    //    *nrTrues = (long)pDest[0];
                    //    return; // TODO: check!
                    //}

                    long f = cur[0] = start;
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

                        byte* pRead = pSrc;

                        while (leadLen > 0) {
                            if ((*(long*)pRead)  != 0) break;
                            pRead += accuStride; leadLen--; 
                        }
                        *pDest = leadLen  > 0 ? (byte)1 : (byte)0;
                        *nrTrues += (long)pDest[0];

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

        /// <summary>Determines whether any element of <paramref name="A"/> along the given dimension <paramref name="dim"/> is not zero.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] The index of the dimension to be reduced. Default: (-1) the first non-singleton dimension found in <paramref name="A"/> or 0.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length 1.</returns>
        /// <remarks><para>For floating point elements: special floating point values (<see cref="float.NaN"/>, <see cref="float.PositiveInfinity"/>, etc.; being 'not zero') 
        /// evaluate to 'true'.</para>
        /// <para>Empty arrays are allowed. In difference to <see cref="sum(BaseArray{float}, int, bool)"/> or 
        /// <see cref="prod(BaseArray{float}, int, bool)"/> dimension <paramref name="dim"/> may be of length 0, which leads to a 
        /// singleton dimension in the result and the non-existing elements evaluate to 'false'.</para>
        /// <para>The default value for <paramref name="dim"/> is -1. It causes the function to determine the working dimension (numpy: 'axis') automatically. 
        /// This search is based on <see cref="Size.WorkingDimension()"/> and starts looking at the first dimension for <see cref="Settings.ArrayStyle"/> <see cref="ArrayStyles.ILNumericsV4"/>. 
        /// For <see cref="Settings.ArrayStyle"/> <see cref="ArrayStyles.numpy"/>, however, the search starts with the last dimension and works its way up to the first dimension. 
        /// If the array does not contain any non-singleton dimension 0 is returned in both cases.</para>
        /// </remarks>
        /// <see cref="Size.WorkingDimension"/>
        /// <see cref="Settings.ArrayStyle"/>
        
        internal unsafe static Logical any(BaseArray<ulong> A, int dim = -1, bool keepdim = true) {
            return InnerLoops.Any.UInt64.Instance.operate(
                A as ConcreteArray<ulong,Array<ulong>,InArray<ulong>,OutArray<ulong>,Array<ulong>,Storage<ulong>>, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Any {

            internal class UInt64 :

            ReduceOtherTypeBase<ulong,Array<ulong>,InArray<ulong>,OutArray<ulong>,Array<ulong>,Storage<ulong>,
                        bool, Logical, InLogical, OutLogical, Logical, LogicalStorage> {

                internal static UInt64 Instance = new UInt64();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut, long* nrTrues) {

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
                    if (ndims == 0) {
                        pDest += bsdOut[2];
                        // Note: this is used by any() and all(). Both test on non-zero (but in a different way below). 
                        // Therefore, we don't need to modify the 'HC:operatorfunc' != 0 in the next line. It works this 
                        // (same) way for both functions. 
                        var scalarVal = (*(ulong*)(pSrc + bsdIn[2]) != 0) ? (byte)1 : (byte)0;
                        *nrTrues = *pDest = scalarVal;
                        return; 
                    }
                    System.Diagnostics.Debug.Assert(ndims > 0, $"Invalid BSD for input array detected: ndims = {ndims}.");

                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims;
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;

                    long* cur = stackalloc long[Math.Max((int)ndims,1)]; 

                    nrTrues[0] = 0; 
                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    //if (ndims == 0) { // WRONG?? TODO! test: reducing numpy 1D vector to np scalar
                    //    *pDest = *pSrc != 0 ? (byte)1 : (byte)0;
                    //    *nrTrues = (long)pDest[0];
                    //    return; // TODO: check!
                    //}

                    long f = cur[0] = start;
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

                        byte* pRead = pSrc;

                        while (leadLen > 0) {
                            if ((*(ulong*)pRead)  != 0) break;
                            pRead += accuStride; leadLen--; 
                        }
                        *pDest = leadLen  > 0 ? (byte)1 : (byte)0;
                        *nrTrues += (long)pDest[0];

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

        /// <summary>Determines whether any element of <paramref name="A"/> along the given dimension <paramref name="dim"/> is not zero.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] The index of the dimension to be reduced. Default: (-1) the first non-singleton dimension found in <paramref name="A"/> or 0.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length 1.</returns>
        /// <remarks><para>For floating point elements: special floating point values (<see cref="float.NaN"/>, <see cref="float.PositiveInfinity"/>, etc.; being 'not zero') 
        /// evaluate to 'true'.</para>
        /// <para>Empty arrays are allowed. In difference to <see cref="sum(BaseArray{float}, int, bool)"/> or 
        /// <see cref="prod(BaseArray{float}, int, bool)"/> dimension <paramref name="dim"/> may be of length 0, which leads to a 
        /// singleton dimension in the result and the non-existing elements evaluate to 'false'.</para>
        /// <para>The default value for <paramref name="dim"/> is -1. It causes the function to determine the working dimension (numpy: 'axis') automatically. 
        /// This search is based on <see cref="Size.WorkingDimension()"/> and starts looking at the first dimension for <see cref="Settings.ArrayStyle"/> <see cref="ArrayStyles.ILNumericsV4"/>. 
        /// For <see cref="Settings.ArrayStyle"/> <see cref="ArrayStyles.numpy"/>, however, the search starts with the last dimension and works its way up to the first dimension. 
        /// If the array does not contain any non-singleton dimension 0 is returned in both cases.</para>
        /// </remarks>
        /// <see cref="Size.WorkingDimension"/>
        /// <see cref="Settings.ArrayStyle"/>
        
        internal unsafe static Logical any(BaseArray<int> A, int dim = -1, bool keepdim = true) {
            return InnerLoops.Any.Int32.Instance.operate(
                A as ConcreteArray<int,Array<int>,InArray<int>,OutArray<int>,Array<int>,Storage<int>>, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Any {

            internal class Int32 :

            ReduceOtherTypeBase<int,Array<int>,InArray<int>,OutArray<int>,Array<int>,Storage<int>,
                        bool, Logical, InLogical, OutLogical, Logical, LogicalStorage> {

                internal static Int32 Instance = new Int32();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut, long* nrTrues) {

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
                    if (ndims == 0) {
                        pDest += bsdOut[2];
                        // Note: this is used by any() and all(). Both test on non-zero (but in a different way below). 
                        // Therefore, we don't need to modify the 'HC:operatorfunc' != 0 in the next line. It works this 
                        // (same) way for both functions. 
                        var scalarVal = (*(int*)(pSrc + bsdIn[2]) != 0) ? (byte)1 : (byte)0;
                        *nrTrues = *pDest = scalarVal;
                        return; 
                    }
                    System.Diagnostics.Debug.Assert(ndims > 0, $"Invalid BSD for input array detected: ndims = {ndims}.");

                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims;
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;

                    long* cur = stackalloc long[Math.Max((int)ndims,1)]; 

                    nrTrues[0] = 0; 
                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    //if (ndims == 0) { // WRONG?? TODO! test: reducing numpy 1D vector to np scalar
                    //    *pDest = *pSrc != 0 ? (byte)1 : (byte)0;
                    //    *nrTrues = (long)pDest[0];
                    //    return; // TODO: check!
                    //}

                    long f = cur[0] = start;
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

                        byte* pRead = pSrc;

                        while (leadLen > 0) {
                            if ((*(int*)pRead)  != 0) break;
                            pRead += accuStride; leadLen--; 
                        }
                        *pDest = leadLen  > 0 ? (byte)1 : (byte)0;
                        *nrTrues += (long)pDest[0];

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

        /// <summary>Determines whether any element of <paramref name="A"/> along the given dimension <paramref name="dim"/> is not zero.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] The index of the dimension to be reduced. Default: (-1) the first non-singleton dimension found in <paramref name="A"/> or 0.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length 1.</returns>
        /// <remarks><para>For floating point elements: special floating point values (<see cref="float.NaN"/>, <see cref="float.PositiveInfinity"/>, etc.; being 'not zero') 
        /// evaluate to 'true'.</para>
        /// <para>Empty arrays are allowed. In difference to <see cref="sum(BaseArray{float}, int, bool)"/> or 
        /// <see cref="prod(BaseArray{float}, int, bool)"/> dimension <paramref name="dim"/> may be of length 0, which leads to a 
        /// singleton dimension in the result and the non-existing elements evaluate to 'false'.</para>
        /// <para>The default value for <paramref name="dim"/> is -1. It causes the function to determine the working dimension (numpy: 'axis') automatically. 
        /// This search is based on <see cref="Size.WorkingDimension()"/> and starts looking at the first dimension for <see cref="Settings.ArrayStyle"/> <see cref="ArrayStyles.ILNumericsV4"/>. 
        /// For <see cref="Settings.ArrayStyle"/> <see cref="ArrayStyles.numpy"/>, however, the search starts with the last dimension and works its way up to the first dimension. 
        /// If the array does not contain any non-singleton dimension 0 is returned in both cases.</para>
        /// </remarks>
        /// <see cref="Size.WorkingDimension"/>
        /// <see cref="Settings.ArrayStyle"/>
        
        internal unsafe static Logical any(BaseArray<uint> A, int dim = -1, bool keepdim = true) {
            return InnerLoops.Any.UInt32.Instance.operate(
                A as ConcreteArray<uint,Array<uint>,InArray<uint>,OutArray<uint>,Array<uint>,Storage<uint>>, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Any {

            internal class UInt32 :

            ReduceOtherTypeBase<uint,Array<uint>,InArray<uint>,OutArray<uint>,Array<uint>,Storage<uint>,
                        bool, Logical, InLogical, OutLogical, Logical, LogicalStorage> {

                internal static UInt32 Instance = new UInt32();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut, long* nrTrues) {

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
                    if (ndims == 0) {
                        pDest += bsdOut[2];
                        // Note: this is used by any() and all(). Both test on non-zero (but in a different way below). 
                        // Therefore, we don't need to modify the 'HC:operatorfunc' != 0 in the next line. It works this 
                        // (same) way for both functions. 
                        var scalarVal = (*(uint*)(pSrc + bsdIn[2]) != 0) ? (byte)1 : (byte)0;
                        *nrTrues = *pDest = scalarVal;
                        return; 
                    }
                    System.Diagnostics.Debug.Assert(ndims > 0, $"Invalid BSD for input array detected: ndims = {ndims}.");

                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims;
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;

                    long* cur = stackalloc long[Math.Max((int)ndims,1)]; 

                    nrTrues[0] = 0; 
                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    //if (ndims == 0) { // WRONG?? TODO! test: reducing numpy 1D vector to np scalar
                    //    *pDest = *pSrc != 0 ? (byte)1 : (byte)0;
                    //    *nrTrues = (long)pDest[0];
                    //    return; // TODO: check!
                    //}

                    long f = cur[0] = start;
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

                        byte* pRead = pSrc;

                        while (leadLen > 0) {
                            if ((*(uint*)pRead)  != 0) break;
                            pRead += accuStride; leadLen--; 
                        }
                        *pDest = leadLen  > 0 ? (byte)1 : (byte)0;
                        *nrTrues += (long)pDest[0];

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

        /// <summary>Determines whether any element of <paramref name="A"/> along the given dimension <paramref name="dim"/> is not zero.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] The index of the dimension to be reduced. Default: (-1) the first non-singleton dimension found in <paramref name="A"/> or 0.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length 1.</returns>
        /// <remarks><para>For floating point elements: special floating point values (<see cref="float.NaN"/>, <see cref="float.PositiveInfinity"/>, etc.; being 'not zero') 
        /// evaluate to 'true'.</para>
        /// <para>Empty arrays are allowed. In difference to <see cref="sum(BaseArray{float}, int, bool)"/> or 
        /// <see cref="prod(BaseArray{float}, int, bool)"/> dimension <paramref name="dim"/> may be of length 0, which leads to a 
        /// singleton dimension in the result and the non-existing elements evaluate to 'false'.</para>
        /// <para>The default value for <paramref name="dim"/> is -1. It causes the function to determine the working dimension (numpy: 'axis') automatically. 
        /// This search is based on <see cref="Size.WorkingDimension()"/> and starts looking at the first dimension for <see cref="Settings.ArrayStyle"/> <see cref="ArrayStyles.ILNumericsV4"/>. 
        /// For <see cref="Settings.ArrayStyle"/> <see cref="ArrayStyles.numpy"/>, however, the search starts with the last dimension and works its way up to the first dimension. 
        /// If the array does not contain any non-singleton dimension 0 is returned in both cases.</para>
        /// </remarks>
        /// <see cref="Size.WorkingDimension"/>
        /// <see cref="Settings.ArrayStyle"/>
        
        internal unsafe static Logical any(BaseArray<short> A, int dim = -1, bool keepdim = true) {
            return InnerLoops.Any.Int16.Instance.operate(
                A as ConcreteArray<short,Array<short>,InArray<short>,OutArray<short>,Array<short>,Storage<short>>, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Any {

            internal class Int16 :

            ReduceOtherTypeBase<short,Array<short>,InArray<short>,OutArray<short>,Array<short>,Storage<short>,
                        bool, Logical, InLogical, OutLogical, Logical, LogicalStorage> {

                internal static Int16 Instance = new Int16();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut, long* nrTrues) {

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
                    if (ndims == 0) {
                        pDest += bsdOut[2];
                        // Note: this is used by any() and all(). Both test on non-zero (but in a different way below). 
                        // Therefore, we don't need to modify the 'HC:operatorfunc' != 0 in the next line. It works this 
                        // (same) way for both functions. 
                        var scalarVal = (*(short*)(pSrc + bsdIn[2]) != 0) ? (byte)1 : (byte)0;
                        *nrTrues = *pDest = scalarVal;
                        return; 
                    }
                    System.Diagnostics.Debug.Assert(ndims > 0, $"Invalid BSD for input array detected: ndims = {ndims}.");

                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims;
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;

                    long* cur = stackalloc long[Math.Max((int)ndims,1)]; 

                    nrTrues[0] = 0; 
                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    //if (ndims == 0) { // WRONG?? TODO! test: reducing numpy 1D vector to np scalar
                    //    *pDest = *pSrc != 0 ? (byte)1 : (byte)0;
                    //    *nrTrues = (long)pDest[0];
                    //    return; // TODO: check!
                    //}

                    long f = cur[0] = start;
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

                        byte* pRead = pSrc;

                        while (leadLen > 0) {
                            if ((*(short*)pRead)  != 0) break;
                            pRead += accuStride; leadLen--; 
                        }
                        *pDest = leadLen  > 0 ? (byte)1 : (byte)0;
                        *nrTrues += (long)pDest[0];

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

        /// <summary>Determines whether any element of <paramref name="A"/> along the given dimension <paramref name="dim"/> is not zero.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] The index of the dimension to be reduced. Default: (-1) the first non-singleton dimension found in <paramref name="A"/> or 0.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length 1.</returns>
        /// <remarks><para>For floating point elements: special floating point values (<see cref="float.NaN"/>, <see cref="float.PositiveInfinity"/>, etc.; being 'not zero') 
        /// evaluate to 'true'.</para>
        /// <para>Empty arrays are allowed. In difference to <see cref="sum(BaseArray{float}, int, bool)"/> or 
        /// <see cref="prod(BaseArray{float}, int, bool)"/> dimension <paramref name="dim"/> may be of length 0, which leads to a 
        /// singleton dimension in the result and the non-existing elements evaluate to 'false'.</para>
        /// <para>The default value for <paramref name="dim"/> is -1. It causes the function to determine the working dimension (numpy: 'axis') automatically. 
        /// This search is based on <see cref="Size.WorkingDimension()"/> and starts looking at the first dimension for <see cref="Settings.ArrayStyle"/> <see cref="ArrayStyles.ILNumericsV4"/>. 
        /// For <see cref="Settings.ArrayStyle"/> <see cref="ArrayStyles.numpy"/>, however, the search starts with the last dimension and works its way up to the first dimension. 
        /// If the array does not contain any non-singleton dimension 0 is returned in both cases.</para>
        /// </remarks>
        /// <see cref="Size.WorkingDimension"/>
        /// <see cref="Settings.ArrayStyle"/>
        
        internal unsafe static Logical any(BaseArray<ushort> A, int dim = -1, bool keepdim = true) {
            return InnerLoops.Any.UInt16.Instance.operate(
                A as ConcreteArray<ushort,Array<ushort>,InArray<ushort>,OutArray<ushort>,Array<ushort>,Storage<ushort>>, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Any {

            internal class UInt16 :

            ReduceOtherTypeBase<ushort,Array<ushort>,InArray<ushort>,OutArray<ushort>,Array<ushort>,Storage<ushort>,
                        bool, Logical, InLogical, OutLogical, Logical, LogicalStorage> {

                internal static UInt16 Instance = new UInt16();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut, long* nrTrues) {

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
                    if (ndims == 0) {
                        pDest += bsdOut[2];
                        // Note: this is used by any() and all(). Both test on non-zero (but in a different way below). 
                        // Therefore, we don't need to modify the 'HC:operatorfunc' != 0 in the next line. It works this 
                        // (same) way for both functions. 
                        var scalarVal = (*(ushort*)(pSrc + bsdIn[2]) != 0) ? (byte)1 : (byte)0;
                        *nrTrues = *pDest = scalarVal;
                        return; 
                    }
                    System.Diagnostics.Debug.Assert(ndims > 0, $"Invalid BSD for input array detected: ndims = {ndims}.");

                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims;
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;

                    long* cur = stackalloc long[Math.Max((int)ndims,1)]; 

                    nrTrues[0] = 0; 
                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    //if (ndims == 0) { // WRONG?? TODO! test: reducing numpy 1D vector to np scalar
                    //    *pDest = *pSrc != 0 ? (byte)1 : (byte)0;
                    //    *nrTrues = (long)pDest[0];
                    //    return; // TODO: check!
                    //}

                    long f = cur[0] = start;
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

                        byte* pRead = pSrc;

                        while (leadLen > 0) {
                            if ((*(ushort*)pRead)  != 0) break;
                            pRead += accuStride; leadLen--; 
                        }
                        *pDest = leadLen  > 0 ? (byte)1 : (byte)0;
                        *nrTrues += (long)pDest[0];

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

        /// <summary>Determines whether any element of <paramref name="A"/> along the given dimension <paramref name="dim"/> is not zero.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] The index of the dimension to be reduced. Default: (-1) the first non-singleton dimension found in <paramref name="A"/> or 0.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length 1.</returns>
        /// <remarks><para>For floating point elements: special floating point values (<see cref="float.NaN"/>, <see cref="float.PositiveInfinity"/>, etc.; being 'not zero') 
        /// evaluate to 'true'.</para>
        /// <para>Empty arrays are allowed. In difference to <see cref="sum(BaseArray{float}, int, bool)"/> or 
        /// <see cref="prod(BaseArray{float}, int, bool)"/> dimension <paramref name="dim"/> may be of length 0, which leads to a 
        /// singleton dimension in the result and the non-existing elements evaluate to 'false'.</para>
        /// <para>The default value for <paramref name="dim"/> is -1. It causes the function to determine the working dimension (numpy: 'axis') automatically. 
        /// This search is based on <see cref="Size.WorkingDimension()"/> and starts looking at the first dimension for <see cref="Settings.ArrayStyle"/> <see cref="ArrayStyles.ILNumericsV4"/>. 
        /// For <see cref="Settings.ArrayStyle"/> <see cref="ArrayStyles.numpy"/>, however, the search starts with the last dimension and works its way up to the first dimension. 
        /// If the array does not contain any non-singleton dimension 0 is returned in both cases.</para>
        /// </remarks>
        /// <see cref="Size.WorkingDimension"/>
        /// <see cref="Settings.ArrayStyle"/>
        
        internal unsafe static Logical any(BaseArray<sbyte> A, int dim = -1, bool keepdim = true) {
            return InnerLoops.Any.SByte.Instance.operate(
                A as ConcreteArray<sbyte,Array<sbyte>,InArray<sbyte>,OutArray<sbyte>,Array<sbyte>,Storage<sbyte>>, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Any {

            internal class SByte :

            ReduceOtherTypeBase<sbyte,Array<sbyte>,InArray<sbyte>,OutArray<sbyte>,Array<sbyte>,Storage<sbyte>,
                        bool, Logical, InLogical, OutLogical, Logical, LogicalStorage> {

                internal static SByte Instance = new SByte();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut, long* nrTrues) {

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
                    if (ndims == 0) {
                        pDest += bsdOut[2];
                        // Note: this is used by any() and all(). Both test on non-zero (but in a different way below). 
                        // Therefore, we don't need to modify the 'HC:operatorfunc' != 0 in the next line. It works this 
                        // (same) way for both functions. 
                        var scalarVal = (*(sbyte*)(pSrc + bsdIn[2]) != 0) ? (byte)1 : (byte)0;
                        *nrTrues = *pDest = scalarVal;
                        return; 
                    }
                    System.Diagnostics.Debug.Assert(ndims > 0, $"Invalid BSD for input array detected: ndims = {ndims}.");

                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims;
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;

                    long* cur = stackalloc long[Math.Max((int)ndims,1)]; 

                    nrTrues[0] = 0; 
                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    //if (ndims == 0) { // WRONG?? TODO! test: reducing numpy 1D vector to np scalar
                    //    *pDest = *pSrc != 0 ? (byte)1 : (byte)0;
                    //    *nrTrues = (long)pDest[0];
                    //    return; // TODO: check!
                    //}

                    long f = cur[0] = start;
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

                        byte* pRead = pSrc;

                        while (leadLen > 0) {
                            if ((*(sbyte*)pRead)  != 0) break;
                            pRead += accuStride; leadLen--; 
                        }
                        *pDest = leadLen  > 0 ? (byte)1 : (byte)0;
                        *nrTrues += (long)pDest[0];

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

        /// <summary>Determines whether any element of <paramref name="A"/> along the given dimension <paramref name="dim"/> is not zero.</summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] The index of the dimension to be reduced. Default: (-1) the first non-singleton dimension found in <paramref name="A"/> or 0.</param>
        /// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
        /// <returns>New array with the same shape as <paramref name="A"/> except that the 
        /// dimension specified by <paramref name="dim"/> is reduced to length 1.</returns>
        /// <remarks><para>For floating point elements: special floating point values (<see cref="float.NaN"/>, <see cref="float.PositiveInfinity"/>, etc.; being 'not zero') 
        /// evaluate to 'true'.</para>
        /// <para>Empty arrays are allowed. In difference to <see cref="sum(BaseArray{float}, int, bool)"/> or 
        /// <see cref="prod(BaseArray{float}, int, bool)"/> dimension <paramref name="dim"/> may be of length 0, which leads to a 
        /// singleton dimension in the result and the non-existing elements evaluate to 'false'.</para>
        /// <para>The default value for <paramref name="dim"/> is -1. It causes the function to determine the working dimension (numpy: 'axis') automatically. 
        /// This search is based on <see cref="Size.WorkingDimension()"/> and starts looking at the first dimension for <see cref="Settings.ArrayStyle"/> <see cref="ArrayStyles.ILNumericsV4"/>. 
        /// For <see cref="Settings.ArrayStyle"/> <see cref="ArrayStyles.numpy"/>, however, the search starts with the last dimension and works its way up to the first dimension. 
        /// If the array does not contain any non-singleton dimension 0 is returned in both cases.</para>
        /// </remarks>
        /// <see cref="Size.WorkingDimension"/>
        /// <see cref="Settings.ArrayStyle"/>
        
        internal unsafe static Logical any(BaseArray<byte> A, int dim = -1, bool keepdim = true) {
            return InnerLoops.Any.Byte.Instance.operate(
                A as ConcreteArray<byte,Array<byte>,InArray<byte>,OutArray<byte>,Array<byte>,Storage<byte>>, dim, keepdim);
        }
    }
    namespace InnerLoops {

        namespace Any {

            internal class Byte :

            ReduceOtherTypeBase<byte,Array<byte>,InArray<byte>,OutArray<byte>,Array<byte>,Storage<byte>,
                        bool, Logical, InLogical, OutLogical, Logical, LogicalStorage> {

                internal static Byte Instance = new Byte();
                public unsafe override void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsdIn, long* bsdOut, long* nrTrues) {

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
                    if (ndims == 0) {
                        pDest += bsdOut[2];
                        // Note: this is used by any() and all(). Both test on non-zero (but in a different way below). 
                        // Therefore, we don't need to modify the 'HC:operatorfunc' != 0 in the next line. It works this 
                        // (same) way for both functions. 
                        var scalarVal = (*(byte*)(pSrc + bsdIn[2]) != 0) ? (byte)1 : (byte)0;
                        *nrTrues = *pDest = scalarVal;
                        return; 
                    }
                    System.Diagnostics.Debug.Assert(ndims > 0, $"Invalid BSD for input array detected: ndims = {ndims}.");

                    long* dims = bsdIn + 3;
                    long* inStrides = bsdIn + 3 + ndims;
                    long* outStrides = bsdOut + 3 + ndims;
                    System.Diagnostics.Debug.Assert(dims[0] >= bsdOut[3], "BSDs have invalid format.");
                    System.Diagnostics.Debug.Assert(dims[ndims - 1] >= bsdOut[3 + ndims - 1], "BSDs have invalid format.");

                    long accuDim = dims[ndims - 1] + 1;
                    long accuStride = inStrides[ndims - 1];
                    System.Diagnostics.Debug.Assert(bsdOut[3 + ndims - 1] == 0); // <-- singleton dim - 1

                    ndims--;

                    long* cur = stackalloc long[Math.Max((int)ndims,1)]; 

                    nrTrues[0] = 0; 
                    // figure out the dimension index position for start
                    pSrc += bsdIn[2];
                    pDest += bsdOut[2];

                    //if (ndims == 0) { // WRONG?? TODO! test: reducing numpy 1D vector to np scalar
                    //    *pDest = *pSrc != 0 ? (byte)1 : (byte)0;
                    //    *nrTrues = (long)pDest[0];
                    //    return; // TODO: check!
                    //}

                    long f = cur[0] = start;
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

                        byte* pRead = pSrc;

                        while (leadLen > 0) {
                            if ((*(byte*)pRead)  != 0) break;
                            pRead += accuStride; leadLen--; 
                        }
                        *pDest = leadLen  > 0 ? (byte)1 : (byte)0;
                        *nrTrues += (long)pDest[0];

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

