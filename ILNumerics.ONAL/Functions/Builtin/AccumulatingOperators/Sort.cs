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
        /// Sort elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="descending">[Optional] Determins the sorting direction. Default: (false) ascending.</param>
        /// <param name="dim">The index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim"/> = -1 depends on the value of <see cref="Settings.ArrayStyle"/>. 
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts 
        /// with the first dimension (index #0).</para></remarks>
        
        internal unsafe static /*!HC:RetCls*/Array</*!HC:outArr*/double> /*!HC:funcname*/sort(BaseArray<double> A, int dim = -1, bool descending = false) {
            if (object.Equals(A, null)) {
                return null;
            }
            Array<double> ret = (A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>)?.C;
            ret.Detach();
            if (descending) {
                InnerLoops./*!HC:innerloopname*/SortDesc.Double.Instance.operate(ret, dim);
            } else {
                InnerLoops./*!HC:innerloopname*/SortAsc.Double.Instance.operate(ret, dim);
            }
            //(A as RetArray<double>)?.Release(); // A was released in ().C above! 
            return ret;
        }

        /// <summary>
        /// Sort elements of <paramref name="A" /> along the specified dimension. Computes indices also.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="Indices">[Output, optional] On return contains the indices required to sort the array.</param>
        /// <param name="descending">[Optional] Determins the sorting direction. Default: (false) ascending.</param>
        /// <param name="dim">The index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A" />.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim" /> = -1 depends on the value of <see cref="P:ILNumerics.Settings.ArrayStyle" />.
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts
        /// with the first dimension (index #0).</para><para>If <paramref name="Indices" /> is of the same shape as (non-empty, non-scalar) <paramref name="A" /> on entry, its content is sorted with
        /// the values of A. Otherwise and if <paramref name="Indices" /> is not null and not of the same shape as <paramref name="A" />
        /// the function will resize <paramref name="Indices" /> and fill it with zero based indices along the working dimension and sort
        /// these together with <paramref name="A" />.</para><para>It is recommended to initialize <paramref name="Indices" /> with 0 or <see cref="M:ILNumerics.Core.Functions.Builtin.MathInternal.empty``1(ILNumerics.InArray{System.Int64},ILNumerics.StorageOrders)" /> to indicate
        /// that the indices are required. If <paramref name="A" /> is scalar and <paramref name="Indices" /> is not null, any predefined value of <paramref name="Indices" />
        /// is ignored and 0 is returned in <paramref name="Indices" />. That means that initializing <paramref name="Indices" /> with 1
        /// or any other scalar literal (for ease of use) does work also, as long as <paramref name="A" /> is not scalar.</para></remarks>
        
        internal unsafe static /*!HC:RetCls*/Array</*!HC:outArr*/double> /*!HC:funcname*/sort(BaseArray<double> A, OutArray<long> Indices, int dim = -1, bool descending = false) {
            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {
                if (object.Equals(A, null)) {
                    return null;
                }
                if (Equals(Indices, null)) {
                    return sort(A, dim, descending);
                }
                lock (Indices.SynchObj) {
                    Storage<long> indStorage = Indices.Storage;
                    if (indStorage.S.NumberOfElements == 1) {
                        indStorage.Assign(indStorage.SetValue(0, d0: 0));
                    }
                    Array<double> ret = (A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>)?.C;
                    ret.Detach();

                    if (descending) {
                        InnerLoops./*!HC:innerIDXloopname*/SortIndDesc.Double.Instance.operate(ret, indStorage, dim);
                    } else {
                        InnerLoops./*!HC:innerIDXloopname*/SortIndAsc.Double.Instance.operate(ret, indStorage, dim);
                    }
                    // has indices' storage been recreated inside the quick sort operator? 
                    // Disabled: we cannot exchange the storage of the incoming OutT here, since this would not 
                    // affect the storage registered as OutArray storage in an _asynchronous AIP_ call, wrapping
                    // this sort function. Instead, Indices storage must not change and we must use Assign()
                    // inside the inner operator to modify the (same) storage directly. 
                    //if (!Equals(indStorage, Indices.Storage)) {
                    //    Indices.Storage.Assign(indStorage, toOutT: true);  // performs locking on Indices array
                    //}
                    return ret;
                }
            }
        }
    }
    namespace InnerLoops {

        namespace /*!HC:innerloopname*/SortAsc {

            internal class Double :

            UnaryInplaceAxisBase<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> {

                internal static Double Instance = new Double();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortAscST((double*)pSrc, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace /*!HC:innerloopname*/SortDesc {

            internal class Double :

            UnaryInplaceAxisBase<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> {

                internal static Double Instance = new Double();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortDescST((double*)pSrc, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace /*!HC:innerloopname*/SortIndAsc {

            internal class Double :  

            UnaryInplaceAxisIndicesBase<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> {

                internal static Double Instance = new Double();
                public unsafe override void Strided64(byte* pSrc, byte* pIndices, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;
                    long* idxStrides = inStrides + ndims; 

                    long accuInStride = inStrides[0] / m_sizeOfT;
                    System.Diagnostics.Debug.Assert(accuInStride == idxStrides[0] / sizeof(long), "This quick sort implementation requires the strides of indices to match the strides of the values array, at least along the working dimension!"); 

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pIndices += cur[i] * idxStrides[i]; 
                    }
                    while(i < ndims) {
                        cur[i++] = 0; 
                    }
                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortAscIDXST((double*)pSrc, (long*)pIndices, 0, dims[0] * accuInStride, accuInStride);
                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pIndices += idxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pIndices -= idxStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace /*!HC:innerloopname*/SortIndDesc {

            internal class Double :

            UnaryInplaceAxisIndicesBase<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> {

                internal static Double Instance = new Double();
                public unsafe override void Strided64(byte* pSrc, byte* pIndices, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;
                    long* idxStrides = inStrides + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;
                    System.Diagnostics.Debug.Assert(accuInStride == idxStrides[0] / sizeof(long), "This quick sort implementation requires the strides of indices to match the strides of the values array, at least along the working dimension!");

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pIndices += cur[i] * idxStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortDescIDXST((double*)pSrc, (long*)pIndices, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pIndices += idxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pIndices -= idxStrides[d] * dims[d];
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
        /// Sort elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="descending">[Optional] Determins the sorting direction. Default: (false) ascending.</param>
        /// <param name="dim">The index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim"/> = -1 depends on the value of <see cref="Settings.ArrayStyle"/>. 
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts 
        /// with the first dimension (index #0).</para></remarks>
        
        internal unsafe static Array<fcomplex> sort(BaseArray<fcomplex> A, int dim = -1, bool descending = false) {
            if (object.Equals(A, null)) {
                return null;
            }
            Array<fcomplex> ret = (A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>)?.C;
            ret.Detach();
            if (descending) {
                InnerLoops.SortDesc.FComplex.Instance.operate(ret, dim);
            } else {
                InnerLoops.SortAsc.FComplex.Instance.operate(ret, dim);
            }
            //(A as RetArray<fcomplex>)?.Release(); // A was released in ().C above! 
            return ret;
        }

        /// <summary>
        /// Sort elements of <paramref name="A" /> along the specified dimension. Computes indices also.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="Indices">[Output, optional] On return contains the indices required to sort the array.</param>
        /// <param name="descending">[Optional] Determins the sorting direction. Default: (false) ascending.</param>
        /// <param name="dim">The index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A" />.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim" /> = -1 depends on the value of <see cref="P:ILNumerics.Settings.ArrayStyle" />.
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts
        /// with the first dimension (index #0).</para><para>If <paramref name="Indices" /> is of the same shape as (non-empty, non-scalar) <paramref name="A" /> on entry, its content is sorted with
        /// the values of A. Otherwise and if <paramref name="Indices" /> is not null and not of the same shape as <paramref name="A" />
        /// the function will resize <paramref name="Indices" /> and fill it with zero based indices along the working dimension and sort
        /// these together with <paramref name="A" />.</para><para>It is recommended to initialize <paramref name="Indices" /> with 0 or <see cref="M:ILNumerics.Core.Functions.Builtin.MathInternal.empty``1(ILNumerics.InArray{System.Int64},ILNumerics.StorageOrders)" /> to indicate
        /// that the indices are required. If <paramref name="A" /> is scalar and <paramref name="Indices" /> is not null, any predefined value of <paramref name="Indices" />
        /// is ignored and 0 is returned in <paramref name="Indices" />. That means that initializing <paramref name="Indices" /> with 1
        /// or any other scalar literal (for ease of use) does work also, as long as <paramref name="A" /> is not scalar.</para></remarks>
        
        internal unsafe static Array<fcomplex> sort(BaseArray<fcomplex> A, OutArray<long> Indices, int dim = -1, bool descending = false) {
            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {
                if (object.Equals(A, null)) {
                    return null;
                }
                if (Equals(Indices, null)) {
                    return sort(A, dim, descending);
                }
                lock (Indices.SynchObj) {
                    Storage<long> indStorage = Indices.Storage;
                    if (indStorage.S.NumberOfElements == 1) {
                        indStorage.Assign(indStorage.SetValue(0, d0: 0));
                    }
                    Array<fcomplex> ret = (A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>)?.C;
                    ret.Detach();

                    if (descending) {
                        InnerLoops.SortIndDesc.FComplex.Instance.operate(ret, indStorage, dim);
                    } else {
                        InnerLoops.SortIndAsc.FComplex.Instance.operate(ret, indStorage, dim);
                    }
                    // has indices' storage been recreated inside the quick sort operator? 
                    // Disabled: we cannot exchange the storage of the incoming OutT here, since this would not 
                    // affect the storage registered as OutArray storage in an _asynchronous AIP_ call, wrapping
                    // this sort function. Instead, Indices storage must not change and we must use Assign()
                    // inside the inner operator to modify the (same) storage directly. 
                    //if (!Equals(indStorage, Indices.Storage)) {
                    //    Indices.Storage.Assign(indStorage, toOutT: true);  // performs locking on Indices array
                    //}
                    return ret;
                }
            }
        }
    }
    namespace InnerLoops {

        namespace SortAsc {

            internal class FComplex :

            UnaryInplaceAxisBase<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> {

                internal static FComplex Instance = new FComplex();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortAscST((fcomplex*)pSrc, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace SortDesc {

            internal class FComplex :

            UnaryInplaceAxisBase<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> {

                internal static FComplex Instance = new FComplex();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortDescST((fcomplex*)pSrc, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace SortIndAsc {

            internal class FComplex :  

            UnaryInplaceAxisIndicesBase<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> {

                internal static FComplex Instance = new FComplex();
                public unsafe override void Strided64(byte* pSrc, byte* pIndices, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;
                    long* idxStrides = inStrides + ndims; 

                    long accuInStride = inStrides[0] / m_sizeOfT;
                    System.Diagnostics.Debug.Assert(accuInStride == idxStrides[0] / sizeof(long), "This quick sort implementation requires the strides of indices to match the strides of the values array, at least along the working dimension!"); 

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pIndices += cur[i] * idxStrides[i]; 
                    }
                    while(i < ndims) {
                        cur[i++] = 0; 
                    }
                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortAscIDXST((fcomplex*)pSrc, (long*)pIndices, 0, dims[0] * accuInStride, accuInStride);
                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pIndices += idxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pIndices -= idxStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace SortIndDesc {

            internal class FComplex :

            UnaryInplaceAxisIndicesBase<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> {

                internal static FComplex Instance = new FComplex();
                public unsafe override void Strided64(byte* pSrc, byte* pIndices, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;
                    long* idxStrides = inStrides + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;
                    System.Diagnostics.Debug.Assert(accuInStride == idxStrides[0] / sizeof(long), "This quick sort implementation requires the strides of indices to match the strides of the values array, at least along the working dimension!");

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pIndices += cur[i] * idxStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortDescIDXST((fcomplex*)pSrc, (long*)pIndices, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pIndices += idxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pIndices -= idxStrides[d] * dims[d];
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
        /// Sort elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="descending">[Optional] Determins the sorting direction. Default: (false) ascending.</param>
        /// <param name="dim">The index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim"/> = -1 depends on the value of <see cref="Settings.ArrayStyle"/>. 
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts 
        /// with the first dimension (index #0).</para></remarks>
        
        internal unsafe static Array<complex> sort(BaseArray<complex> A, int dim = -1, bool descending = false) {
            if (object.Equals(A, null)) {
                return null;
            }
            Array<complex> ret = (A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>)?.C;
            ret.Detach();
            if (descending) {
                InnerLoops.SortDesc.Complex.Instance.operate(ret, dim);
            } else {
                InnerLoops.SortAsc.Complex.Instance.operate(ret, dim);
            }
            //(A as RetArray<complex>)?.Release(); // A was released in ().C above! 
            return ret;
        }

        /// <summary>
        /// Sort elements of <paramref name="A" /> along the specified dimension. Computes indices also.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="Indices">[Output, optional] On return contains the indices required to sort the array.</param>
        /// <param name="descending">[Optional] Determins the sorting direction. Default: (false) ascending.</param>
        /// <param name="dim">The index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A" />.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim" /> = -1 depends on the value of <see cref="P:ILNumerics.Settings.ArrayStyle" />.
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts
        /// with the first dimension (index #0).</para><para>If <paramref name="Indices" /> is of the same shape as (non-empty, non-scalar) <paramref name="A" /> on entry, its content is sorted with
        /// the values of A. Otherwise and if <paramref name="Indices" /> is not null and not of the same shape as <paramref name="A" />
        /// the function will resize <paramref name="Indices" /> and fill it with zero based indices along the working dimension and sort
        /// these together with <paramref name="A" />.</para><para>It is recommended to initialize <paramref name="Indices" /> with 0 or <see cref="M:ILNumerics.Core.Functions.Builtin.MathInternal.empty``1(ILNumerics.InArray{System.Int64},ILNumerics.StorageOrders)" /> to indicate
        /// that the indices are required. If <paramref name="A" /> is scalar and <paramref name="Indices" /> is not null, any predefined value of <paramref name="Indices" />
        /// is ignored and 0 is returned in <paramref name="Indices" />. That means that initializing <paramref name="Indices" /> with 1
        /// or any other scalar literal (for ease of use) does work also, as long as <paramref name="A" /> is not scalar.</para></remarks>
        
        internal unsafe static Array<complex> sort(BaseArray<complex> A, OutArray<long> Indices, int dim = -1, bool descending = false) {
            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {
                if (object.Equals(A, null)) {
                    return null;
                }
                if (Equals(Indices, null)) {
                    return sort(A, dim, descending);
                }
                lock (Indices.SynchObj) {
                    Storage<long> indStorage = Indices.Storage;
                    if (indStorage.S.NumberOfElements == 1) {
                        indStorage.Assign(indStorage.SetValue(0, d0: 0));
                    }
                    Array<complex> ret = (A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>)?.C;
                    ret.Detach();

                    if (descending) {
                        InnerLoops.SortIndDesc.Complex.Instance.operate(ret, indStorage, dim);
                    } else {
                        InnerLoops.SortIndAsc.Complex.Instance.operate(ret, indStorage, dim);
                    }
                    // has indices' storage been recreated inside the quick sort operator? 
                    // Disabled: we cannot exchange the storage of the incoming OutT here, since this would not 
                    // affect the storage registered as OutArray storage in an _asynchronous AIP_ call, wrapping
                    // this sort function. Instead, Indices storage must not change and we must use Assign()
                    // inside the inner operator to modify the (same) storage directly. 
                    //if (!Equals(indStorage, Indices.Storage)) {
                    //    Indices.Storage.Assign(indStorage, toOutT: true);  // performs locking on Indices array
                    //}
                    return ret;
                }
            }
        }
    }
    namespace InnerLoops {

        namespace SortAsc {

            internal class Complex :

            UnaryInplaceAxisBase<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> {

                internal static Complex Instance = new Complex();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortAscST((complex*)pSrc, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace SortDesc {

            internal class Complex :

            UnaryInplaceAxisBase<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> {

                internal static Complex Instance = new Complex();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortDescST((complex*)pSrc, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace SortIndAsc {

            internal class Complex :  

            UnaryInplaceAxisIndicesBase<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> {

                internal static Complex Instance = new Complex();
                public unsafe override void Strided64(byte* pSrc, byte* pIndices, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;
                    long* idxStrides = inStrides + ndims; 

                    long accuInStride = inStrides[0] / m_sizeOfT;
                    System.Diagnostics.Debug.Assert(accuInStride == idxStrides[0] / sizeof(long), "This quick sort implementation requires the strides of indices to match the strides of the values array, at least along the working dimension!"); 

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pIndices += cur[i] * idxStrides[i]; 
                    }
                    while(i < ndims) {
                        cur[i++] = 0; 
                    }
                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortAscIDXST((complex*)pSrc, (long*)pIndices, 0, dims[0] * accuInStride, accuInStride);
                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pIndices += idxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pIndices -= idxStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace SortIndDesc {

            internal class Complex :

            UnaryInplaceAxisIndicesBase<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> {

                internal static Complex Instance = new Complex();
                public unsafe override void Strided64(byte* pSrc, byte* pIndices, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;
                    long* idxStrides = inStrides + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;
                    System.Diagnostics.Debug.Assert(accuInStride == idxStrides[0] / sizeof(long), "This quick sort implementation requires the strides of indices to match the strides of the values array, at least along the working dimension!");

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pIndices += cur[i] * idxStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortDescIDXST((complex*)pSrc, (long*)pIndices, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pIndices += idxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pIndices -= idxStrides[d] * dims[d];
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
        /// Sort elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="descending">[Optional] Determins the sorting direction. Default: (false) ascending.</param>
        /// <param name="dim">The index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim"/> = -1 depends on the value of <see cref="Settings.ArrayStyle"/>. 
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts 
        /// with the first dimension (index #0).</para></remarks>
        
        internal unsafe static Array<float> sort(BaseArray<float> A, int dim = -1, bool descending = false) {
            if (object.Equals(A, null)) {
                return null;
            }
            Array<float> ret = (A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>)?.C;
            ret.Detach();
            if (descending) {
                InnerLoops.SortDesc.Single.Instance.operate(ret, dim);
            } else {
                InnerLoops.SortAsc.Single.Instance.operate(ret, dim);
            }
            //(A as RetArray<float>)?.Release(); // A was released in ().C above! 
            return ret;
        }

        /// <summary>
        /// Sort elements of <paramref name="A" /> along the specified dimension. Computes indices also.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="Indices">[Output, optional] On return contains the indices required to sort the array.</param>
        /// <param name="descending">[Optional] Determins the sorting direction. Default: (false) ascending.</param>
        /// <param name="dim">The index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A" />.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim" /> = -1 depends on the value of <see cref="P:ILNumerics.Settings.ArrayStyle" />.
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts
        /// with the first dimension (index #0).</para><para>If <paramref name="Indices" /> is of the same shape as (non-empty, non-scalar) <paramref name="A" /> on entry, its content is sorted with
        /// the values of A. Otherwise and if <paramref name="Indices" /> is not null and not of the same shape as <paramref name="A" />
        /// the function will resize <paramref name="Indices" /> and fill it with zero based indices along the working dimension and sort
        /// these together with <paramref name="A" />.</para><para>It is recommended to initialize <paramref name="Indices" /> with 0 or <see cref="M:ILNumerics.Core.Functions.Builtin.MathInternal.empty``1(ILNumerics.InArray{System.Int64},ILNumerics.StorageOrders)" /> to indicate
        /// that the indices are required. If <paramref name="A" /> is scalar and <paramref name="Indices" /> is not null, any predefined value of <paramref name="Indices" />
        /// is ignored and 0 is returned in <paramref name="Indices" />. That means that initializing <paramref name="Indices" /> with 1
        /// or any other scalar literal (for ease of use) does work also, as long as <paramref name="A" /> is not scalar.</para></remarks>
        
        internal unsafe static Array<float> sort(BaseArray<float> A, OutArray<long> Indices, int dim = -1, bool descending = false) {
            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {
                if (object.Equals(A, null)) {
                    return null;
                }
                if (Equals(Indices, null)) {
                    return sort(A, dim, descending);
                }
                lock (Indices.SynchObj) {
                    Storage<long> indStorage = Indices.Storage;
                    if (indStorage.S.NumberOfElements == 1) {
                        indStorage.Assign(indStorage.SetValue(0, d0: 0));
                    }
                    Array<float> ret = (A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>)?.C;
                    ret.Detach();

                    if (descending) {
                        InnerLoops.SortIndDesc.Single.Instance.operate(ret, indStorage, dim);
                    } else {
                        InnerLoops.SortIndAsc.Single.Instance.operate(ret, indStorage, dim);
                    }
                    // has indices' storage been recreated inside the quick sort operator? 
                    // Disabled: we cannot exchange the storage of the incoming OutT here, since this would not 
                    // affect the storage registered as OutArray storage in an _asynchronous AIP_ call, wrapping
                    // this sort function. Instead, Indices storage must not change and we must use Assign()
                    // inside the inner operator to modify the (same) storage directly. 
                    //if (!Equals(indStorage, Indices.Storage)) {
                    //    Indices.Storage.Assign(indStorage, toOutT: true);  // performs locking on Indices array
                    //}
                    return ret;
                }
            }
        }
    }
    namespace InnerLoops {

        namespace SortAsc {

            internal class Single :

            UnaryInplaceAxisBase<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> {

                internal static Single Instance = new Single();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortAscST((float*)pSrc, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace SortDesc {

            internal class Single :

            UnaryInplaceAxisBase<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> {

                internal static Single Instance = new Single();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortDescST((float*)pSrc, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace SortIndAsc {

            internal class Single :  

            UnaryInplaceAxisIndicesBase<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> {

                internal static Single Instance = new Single();
                public unsafe override void Strided64(byte* pSrc, byte* pIndices, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;
                    long* idxStrides = inStrides + ndims; 

                    long accuInStride = inStrides[0] / m_sizeOfT;
                    System.Diagnostics.Debug.Assert(accuInStride == idxStrides[0] / sizeof(long), "This quick sort implementation requires the strides of indices to match the strides of the values array, at least along the working dimension!"); 

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pIndices += cur[i] * idxStrides[i]; 
                    }
                    while(i < ndims) {
                        cur[i++] = 0; 
                    }
                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortAscIDXST((float*)pSrc, (long*)pIndices, 0, dims[0] * accuInStride, accuInStride);
                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pIndices += idxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pIndices -= idxStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace SortIndDesc {

            internal class Single :

            UnaryInplaceAxisIndicesBase<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> {

                internal static Single Instance = new Single();
                public unsafe override void Strided64(byte* pSrc, byte* pIndices, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;
                    long* idxStrides = inStrides + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;
                    System.Diagnostics.Debug.Assert(accuInStride == idxStrides[0] / sizeof(long), "This quick sort implementation requires the strides of indices to match the strides of the values array, at least along the working dimension!");

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pIndices += cur[i] * idxStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortDescIDXST((float*)pSrc, (long*)pIndices, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pIndices += idxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pIndices -= idxStrides[d] * dims[d];
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
        /// Sort elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="descending">[Optional] Determins the sorting direction. Default: (false) ascending.</param>
        /// <param name="dim">The index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim"/> = -1 depends on the value of <see cref="Settings.ArrayStyle"/>. 
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts 
        /// with the first dimension (index #0).</para></remarks>
        
        internal unsafe static Array<long> sort(BaseArray<long> A, int dim = -1, bool descending = false) {
            if (object.Equals(A, null)) {
                return null;
            }
            Array<long> ret = (A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>)?.C;
            ret.Detach();
            if (descending) {
                InnerLoops.SortDesc.Int64.Instance.operate(ret, dim);
            } else {
                InnerLoops.SortAsc.Int64.Instance.operate(ret, dim);
            }
            //(A as RetArray<long>)?.Release(); // A was released in ().C above! 
            return ret;
        }

        /// <summary>
        /// Sort elements of <paramref name="A" /> along the specified dimension. Computes indices also.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="Indices">[Output, optional] On return contains the indices required to sort the array.</param>
        /// <param name="descending">[Optional] Determins the sorting direction. Default: (false) ascending.</param>
        /// <param name="dim">The index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A" />.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim" /> = -1 depends on the value of <see cref="P:ILNumerics.Settings.ArrayStyle" />.
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts
        /// with the first dimension (index #0).</para><para>If <paramref name="Indices" /> is of the same shape as (non-empty, non-scalar) <paramref name="A" /> on entry, its content is sorted with
        /// the values of A. Otherwise and if <paramref name="Indices" /> is not null and not of the same shape as <paramref name="A" />
        /// the function will resize <paramref name="Indices" /> and fill it with zero based indices along the working dimension and sort
        /// these together with <paramref name="A" />.</para><para>It is recommended to initialize <paramref name="Indices" /> with 0 or <see cref="M:ILNumerics.Core.Functions.Builtin.MathInternal.empty``1(ILNumerics.InArray{System.Int64},ILNumerics.StorageOrders)" /> to indicate
        /// that the indices are required. If <paramref name="A" /> is scalar and <paramref name="Indices" /> is not null, any predefined value of <paramref name="Indices" />
        /// is ignored and 0 is returned in <paramref name="Indices" />. That means that initializing <paramref name="Indices" /> with 1
        /// or any other scalar literal (for ease of use) does work also, as long as <paramref name="A" /> is not scalar.</para></remarks>
        
        internal unsafe static Array<long> sort(BaseArray<long> A, OutArray<long> Indices, int dim = -1, bool descending = false) {
            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {
                if (object.Equals(A, null)) {
                    return null;
                }
                if (Equals(Indices, null)) {
                    return sort(A, dim, descending);
                }
                lock (Indices.SynchObj) {
                    Storage<long> indStorage = Indices.Storage;
                    if (indStorage.S.NumberOfElements == 1) {
                        indStorage.Assign(indStorage.SetValue(0, d0: 0));
                    }
                    Array<long> ret = (A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>)?.C;
                    ret.Detach();

                    if (descending) {
                        InnerLoops.SortIndDesc.Int64.Instance.operate(ret, indStorage, dim);
                    } else {
                        InnerLoops.SortIndAsc.Int64.Instance.operate(ret, indStorage, dim);
                    }
                    // has indices' storage been recreated inside the quick sort operator? 
                    // Disabled: we cannot exchange the storage of the incoming OutT here, since this would not 
                    // affect the storage registered as OutArray storage in an _asynchronous AIP_ call, wrapping
                    // this sort function. Instead, Indices storage must not change and we must use Assign()
                    // inside the inner operator to modify the (same) storage directly. 
                    //if (!Equals(indStorage, Indices.Storage)) {
                    //    Indices.Storage.Assign(indStorage, toOutT: true);  // performs locking on Indices array
                    //}
                    return ret;
                }
            }
        }
    }
    namespace InnerLoops {

        namespace SortAsc {

            internal class Int64 :

            UnaryInplaceAxisBase<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> {

                internal static Int64 Instance = new Int64();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortAscST((long*)pSrc, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace SortDesc {

            internal class Int64 :

            UnaryInplaceAxisBase<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> {

                internal static Int64 Instance = new Int64();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortDescST((long*)pSrc, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace SortIndAsc {

            internal class Int64 :  

            UnaryInplaceAxisIndicesBase<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> {

                internal static Int64 Instance = new Int64();
                public unsafe override void Strided64(byte* pSrc, byte* pIndices, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;
                    long* idxStrides = inStrides + ndims; 

                    long accuInStride = inStrides[0] / m_sizeOfT;
                    System.Diagnostics.Debug.Assert(accuInStride == idxStrides[0] / sizeof(long), "This quick sort implementation requires the strides of indices to match the strides of the values array, at least along the working dimension!"); 

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pIndices += cur[i] * idxStrides[i]; 
                    }
                    while(i < ndims) {
                        cur[i++] = 0; 
                    }
                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortAscIDXST((long*)pSrc, (long*)pIndices, 0, dims[0] * accuInStride, accuInStride);
                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pIndices += idxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pIndices -= idxStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace SortIndDesc {

            internal class Int64 :

            UnaryInplaceAxisIndicesBase<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> {

                internal static Int64 Instance = new Int64();
                public unsafe override void Strided64(byte* pSrc, byte* pIndices, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;
                    long* idxStrides = inStrides + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;
                    System.Diagnostics.Debug.Assert(accuInStride == idxStrides[0] / sizeof(long), "This quick sort implementation requires the strides of indices to match the strides of the values array, at least along the working dimension!");

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pIndices += cur[i] * idxStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortDescIDXST((long*)pSrc, (long*)pIndices, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pIndices += idxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pIndices -= idxStrides[d] * dims[d];
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
        /// Sort elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="descending">[Optional] Determins the sorting direction. Default: (false) ascending.</param>
        /// <param name="dim">The index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim"/> = -1 depends on the value of <see cref="Settings.ArrayStyle"/>. 
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts 
        /// with the first dimension (index #0).</para></remarks>
        
        internal unsafe static Array<ulong> sort(BaseArray<ulong> A, int dim = -1, bool descending = false) {
            if (object.Equals(A, null)) {
                return null;
            }
            Array<ulong> ret = (A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>)?.C;
            ret.Detach();
            if (descending) {
                InnerLoops.SortDesc.UInt64.Instance.operate(ret, dim);
            } else {
                InnerLoops.SortAsc.UInt64.Instance.operate(ret, dim);
            }
            //(A as RetArray<ulong>)?.Release(); // A was released in ().C above! 
            return ret;
        }

        /// <summary>
        /// Sort elements of <paramref name="A" /> along the specified dimension. Computes indices also.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="Indices">[Output, optional] On return contains the indices required to sort the array.</param>
        /// <param name="descending">[Optional] Determins the sorting direction. Default: (false) ascending.</param>
        /// <param name="dim">The index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A" />.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim" /> = -1 depends on the value of <see cref="P:ILNumerics.Settings.ArrayStyle" />.
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts
        /// with the first dimension (index #0).</para><para>If <paramref name="Indices" /> is of the same shape as (non-empty, non-scalar) <paramref name="A" /> on entry, its content is sorted with
        /// the values of A. Otherwise and if <paramref name="Indices" /> is not null and not of the same shape as <paramref name="A" />
        /// the function will resize <paramref name="Indices" /> and fill it with zero based indices along the working dimension and sort
        /// these together with <paramref name="A" />.</para><para>It is recommended to initialize <paramref name="Indices" /> with 0 or <see cref="M:ILNumerics.Core.Functions.Builtin.MathInternal.empty``1(ILNumerics.InArray{System.Int64},ILNumerics.StorageOrders)" /> to indicate
        /// that the indices are required. If <paramref name="A" /> is scalar and <paramref name="Indices" /> is not null, any predefined value of <paramref name="Indices" />
        /// is ignored and 0 is returned in <paramref name="Indices" />. That means that initializing <paramref name="Indices" /> with 1
        /// or any other scalar literal (for ease of use) does work also, as long as <paramref name="A" /> is not scalar.</para></remarks>
        
        internal unsafe static Array<ulong> sort(BaseArray<ulong> A, OutArray<long> Indices, int dim = -1, bool descending = false) {
            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {
                if (object.Equals(A, null)) {
                    return null;
                }
                if (Equals(Indices, null)) {
                    return sort(A, dim, descending);
                }
                lock (Indices.SynchObj) {
                    Storage<long> indStorage = Indices.Storage;
                    if (indStorage.S.NumberOfElements == 1) {
                        indStorage.Assign(indStorage.SetValue(0, d0: 0));
                    }
                    Array<ulong> ret = (A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>)?.C;
                    ret.Detach();

                    if (descending) {
                        InnerLoops.SortIndDesc.UInt64.Instance.operate(ret, indStorage, dim);
                    } else {
                        InnerLoops.SortIndAsc.UInt64.Instance.operate(ret, indStorage, dim);
                    }
                    // has indices' storage been recreated inside the quick sort operator? 
                    // Disabled: we cannot exchange the storage of the incoming OutT here, since this would not 
                    // affect the storage registered as OutArray storage in an _asynchronous AIP_ call, wrapping
                    // this sort function. Instead, Indices storage must not change and we must use Assign()
                    // inside the inner operator to modify the (same) storage directly. 
                    //if (!Equals(indStorage, Indices.Storage)) {
                    //    Indices.Storage.Assign(indStorage, toOutT: true);  // performs locking on Indices array
                    //}
                    return ret;
                }
            }
        }
    }
    namespace InnerLoops {

        namespace SortAsc {

            internal class UInt64 :

            UnaryInplaceAxisBase<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> {

                internal static UInt64 Instance = new UInt64();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortAscST((ulong*)pSrc, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace SortDesc {

            internal class UInt64 :

            UnaryInplaceAxisBase<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> {

                internal static UInt64 Instance = new UInt64();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortDescST((ulong*)pSrc, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace SortIndAsc {

            internal class UInt64 :  

            UnaryInplaceAxisIndicesBase<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> {

                internal static UInt64 Instance = new UInt64();
                public unsafe override void Strided64(byte* pSrc, byte* pIndices, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;
                    long* idxStrides = inStrides + ndims; 

                    long accuInStride = inStrides[0] / m_sizeOfT;
                    System.Diagnostics.Debug.Assert(accuInStride == idxStrides[0] / sizeof(long), "This quick sort implementation requires the strides of indices to match the strides of the values array, at least along the working dimension!"); 

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pIndices += cur[i] * idxStrides[i]; 
                    }
                    while(i < ndims) {
                        cur[i++] = 0; 
                    }
                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortAscIDXST((ulong*)pSrc, (long*)pIndices, 0, dims[0] * accuInStride, accuInStride);
                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pIndices += idxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pIndices -= idxStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace SortIndDesc {

            internal class UInt64 :

            UnaryInplaceAxisIndicesBase<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> {

                internal static UInt64 Instance = new UInt64();
                public unsafe override void Strided64(byte* pSrc, byte* pIndices, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;
                    long* idxStrides = inStrides + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;
                    System.Diagnostics.Debug.Assert(accuInStride == idxStrides[0] / sizeof(long), "This quick sort implementation requires the strides of indices to match the strides of the values array, at least along the working dimension!");

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pIndices += cur[i] * idxStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortDescIDXST((ulong*)pSrc, (long*)pIndices, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pIndices += idxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pIndices -= idxStrides[d] * dims[d];
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
        /// Sort elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="descending">[Optional] Determins the sorting direction. Default: (false) ascending.</param>
        /// <param name="dim">The index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim"/> = -1 depends on the value of <see cref="Settings.ArrayStyle"/>. 
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts 
        /// with the first dimension (index #0).</para></remarks>
        
        internal unsafe static Array<int> sort(BaseArray<int> A, int dim = -1, bool descending = false) {
            if (object.Equals(A, null)) {
                return null;
            }
            Array<int> ret = (A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>)?.C;
            ret.Detach();
            if (descending) {
                InnerLoops.SortDesc.Int32.Instance.operate(ret, dim);
            } else {
                InnerLoops.SortAsc.Int32.Instance.operate(ret, dim);
            }
            //(A as RetArray<int>)?.Release(); // A was released in ().C above! 
            return ret;
        }

        /// <summary>
        /// Sort elements of <paramref name="A" /> along the specified dimension. Computes indices also.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="Indices">[Output, optional] On return contains the indices required to sort the array.</param>
        /// <param name="descending">[Optional] Determins the sorting direction. Default: (false) ascending.</param>
        /// <param name="dim">The index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A" />.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim" /> = -1 depends on the value of <see cref="P:ILNumerics.Settings.ArrayStyle" />.
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts
        /// with the first dimension (index #0).</para><para>If <paramref name="Indices" /> is of the same shape as (non-empty, non-scalar) <paramref name="A" /> on entry, its content is sorted with
        /// the values of A. Otherwise and if <paramref name="Indices" /> is not null and not of the same shape as <paramref name="A" />
        /// the function will resize <paramref name="Indices" /> and fill it with zero based indices along the working dimension and sort
        /// these together with <paramref name="A" />.</para><para>It is recommended to initialize <paramref name="Indices" /> with 0 or <see cref="M:ILNumerics.Core.Functions.Builtin.MathInternal.empty``1(ILNumerics.InArray{System.Int64},ILNumerics.StorageOrders)" /> to indicate
        /// that the indices are required. If <paramref name="A" /> is scalar and <paramref name="Indices" /> is not null, any predefined value of <paramref name="Indices" />
        /// is ignored and 0 is returned in <paramref name="Indices" />. That means that initializing <paramref name="Indices" /> with 1
        /// or any other scalar literal (for ease of use) does work also, as long as <paramref name="A" /> is not scalar.</para></remarks>
        
        internal unsafe static Array<int> sort(BaseArray<int> A, OutArray<long> Indices, int dim = -1, bool descending = false) {
            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {
                if (object.Equals(A, null)) {
                    return null;
                }
                if (Equals(Indices, null)) {
                    return sort(A, dim, descending);
                }
                lock (Indices.SynchObj) {
                    Storage<long> indStorage = Indices.Storage;
                    if (indStorage.S.NumberOfElements == 1) {
                        indStorage.Assign(indStorage.SetValue(0, d0: 0));
                    }
                    Array<int> ret = (A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>)?.C;
                    ret.Detach();

                    if (descending) {
                        InnerLoops.SortIndDesc.Int32.Instance.operate(ret, indStorage, dim);
                    } else {
                        InnerLoops.SortIndAsc.Int32.Instance.operate(ret, indStorage, dim);
                    }
                    // has indices' storage been recreated inside the quick sort operator? 
                    // Disabled: we cannot exchange the storage of the incoming OutT here, since this would not 
                    // affect the storage registered as OutArray storage in an _asynchronous AIP_ call, wrapping
                    // this sort function. Instead, Indices storage must not change and we must use Assign()
                    // inside the inner operator to modify the (same) storage directly. 
                    //if (!Equals(indStorage, Indices.Storage)) {
                    //    Indices.Storage.Assign(indStorage, toOutT: true);  // performs locking on Indices array
                    //}
                    return ret;
                }
            }
        }
    }
    namespace InnerLoops {

        namespace SortAsc {

            internal class Int32 :

            UnaryInplaceAxisBase<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> {

                internal static Int32 Instance = new Int32();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortAscST((int*)pSrc, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace SortDesc {

            internal class Int32 :

            UnaryInplaceAxisBase<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> {

                internal static Int32 Instance = new Int32();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortDescST((int*)pSrc, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace SortIndAsc {

            internal class Int32 :  

            UnaryInplaceAxisIndicesBase<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> {

                internal static Int32 Instance = new Int32();
                public unsafe override void Strided64(byte* pSrc, byte* pIndices, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;
                    long* idxStrides = inStrides + ndims; 

                    long accuInStride = inStrides[0] / m_sizeOfT;
                    System.Diagnostics.Debug.Assert(accuInStride == idxStrides[0] / sizeof(long), "This quick sort implementation requires the strides of indices to match the strides of the values array, at least along the working dimension!"); 

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pIndices += cur[i] * idxStrides[i]; 
                    }
                    while(i < ndims) {
                        cur[i++] = 0; 
                    }
                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortAscIDXST((int*)pSrc, (long*)pIndices, 0, dims[0] * accuInStride, accuInStride);
                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pIndices += idxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pIndices -= idxStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace SortIndDesc {

            internal class Int32 :

            UnaryInplaceAxisIndicesBase<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> {

                internal static Int32 Instance = new Int32();
                public unsafe override void Strided64(byte* pSrc, byte* pIndices, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;
                    long* idxStrides = inStrides + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;
                    System.Diagnostics.Debug.Assert(accuInStride == idxStrides[0] / sizeof(long), "This quick sort implementation requires the strides of indices to match the strides of the values array, at least along the working dimension!");

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pIndices += cur[i] * idxStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortDescIDXST((int*)pSrc, (long*)pIndices, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pIndices += idxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pIndices -= idxStrides[d] * dims[d];
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
        /// Sort elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="descending">[Optional] Determins the sorting direction. Default: (false) ascending.</param>
        /// <param name="dim">The index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim"/> = -1 depends on the value of <see cref="Settings.ArrayStyle"/>. 
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts 
        /// with the first dimension (index #0).</para></remarks>
        
        internal unsafe static Array<uint> sort(BaseArray<uint> A, int dim = -1, bool descending = false) {
            if (object.Equals(A, null)) {
                return null;
            }
            Array<uint> ret = (A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>)?.C;
            ret.Detach();
            if (descending) {
                InnerLoops.SortDesc.UInt32.Instance.operate(ret, dim);
            } else {
                InnerLoops.SortAsc.UInt32.Instance.operate(ret, dim);
            }
            //(A as RetArray<uint>)?.Release(); // A was released in ().C above! 
            return ret;
        }

        /// <summary>
        /// Sort elements of <paramref name="A" /> along the specified dimension. Computes indices also.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="Indices">[Output, optional] On return contains the indices required to sort the array.</param>
        /// <param name="descending">[Optional] Determins the sorting direction. Default: (false) ascending.</param>
        /// <param name="dim">The index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A" />.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim" /> = -1 depends on the value of <see cref="P:ILNumerics.Settings.ArrayStyle" />.
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts
        /// with the first dimension (index #0).</para><para>If <paramref name="Indices" /> is of the same shape as (non-empty, non-scalar) <paramref name="A" /> on entry, its content is sorted with
        /// the values of A. Otherwise and if <paramref name="Indices" /> is not null and not of the same shape as <paramref name="A" />
        /// the function will resize <paramref name="Indices" /> and fill it with zero based indices along the working dimension and sort
        /// these together with <paramref name="A" />.</para><para>It is recommended to initialize <paramref name="Indices" /> with 0 or <see cref="M:ILNumerics.Core.Functions.Builtin.MathInternal.empty``1(ILNumerics.InArray{System.Int64},ILNumerics.StorageOrders)" /> to indicate
        /// that the indices are required. If <paramref name="A" /> is scalar and <paramref name="Indices" /> is not null, any predefined value of <paramref name="Indices" />
        /// is ignored and 0 is returned in <paramref name="Indices" />. That means that initializing <paramref name="Indices" /> with 1
        /// or any other scalar literal (for ease of use) does work also, as long as <paramref name="A" /> is not scalar.</para></remarks>
        
        internal unsafe static Array<uint> sort(BaseArray<uint> A, OutArray<long> Indices, int dim = -1, bool descending = false) {
            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {
                if (object.Equals(A, null)) {
                    return null;
                }
                if (Equals(Indices, null)) {
                    return sort(A, dim, descending);
                }
                lock (Indices.SynchObj) {
                    Storage<long> indStorage = Indices.Storage;
                    if (indStorage.S.NumberOfElements == 1) {
                        indStorage.Assign(indStorage.SetValue(0, d0: 0));
                    }
                    Array<uint> ret = (A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>)?.C;
                    ret.Detach();

                    if (descending) {
                        InnerLoops.SortIndDesc.UInt32.Instance.operate(ret, indStorage, dim);
                    } else {
                        InnerLoops.SortIndAsc.UInt32.Instance.operate(ret, indStorage, dim);
                    }
                    // has indices' storage been recreated inside the quick sort operator? 
                    // Disabled: we cannot exchange the storage of the incoming OutT here, since this would not 
                    // affect the storage registered as OutArray storage in an _asynchronous AIP_ call, wrapping
                    // this sort function. Instead, Indices storage must not change and we must use Assign()
                    // inside the inner operator to modify the (same) storage directly. 
                    //if (!Equals(indStorage, Indices.Storage)) {
                    //    Indices.Storage.Assign(indStorage, toOutT: true);  // performs locking on Indices array
                    //}
                    return ret;
                }
            }
        }
    }
    namespace InnerLoops {

        namespace SortAsc {

            internal class UInt32 :

            UnaryInplaceAxisBase<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> {

                internal static UInt32 Instance = new UInt32();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortAscST((uint*)pSrc, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace SortDesc {

            internal class UInt32 :

            UnaryInplaceAxisBase<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> {

                internal static UInt32 Instance = new UInt32();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortDescST((uint*)pSrc, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace SortIndAsc {

            internal class UInt32 :  

            UnaryInplaceAxisIndicesBase<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> {

                internal static UInt32 Instance = new UInt32();
                public unsafe override void Strided64(byte* pSrc, byte* pIndices, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;
                    long* idxStrides = inStrides + ndims; 

                    long accuInStride = inStrides[0] / m_sizeOfT;
                    System.Diagnostics.Debug.Assert(accuInStride == idxStrides[0] / sizeof(long), "This quick sort implementation requires the strides of indices to match the strides of the values array, at least along the working dimension!"); 

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pIndices += cur[i] * idxStrides[i]; 
                    }
                    while(i < ndims) {
                        cur[i++] = 0; 
                    }
                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortAscIDXST((uint*)pSrc, (long*)pIndices, 0, dims[0] * accuInStride, accuInStride);
                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pIndices += idxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pIndices -= idxStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace SortIndDesc {

            internal class UInt32 :

            UnaryInplaceAxisIndicesBase<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> {

                internal static UInt32 Instance = new UInt32();
                public unsafe override void Strided64(byte* pSrc, byte* pIndices, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;
                    long* idxStrides = inStrides + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;
                    System.Diagnostics.Debug.Assert(accuInStride == idxStrides[0] / sizeof(long), "This quick sort implementation requires the strides of indices to match the strides of the values array, at least along the working dimension!");

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pIndices += cur[i] * idxStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortDescIDXST((uint*)pSrc, (long*)pIndices, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pIndices += idxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pIndices -= idxStrides[d] * dims[d];
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
        /// Sort elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="descending">[Optional] Determins the sorting direction. Default: (false) ascending.</param>
        /// <param name="dim">The index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim"/> = -1 depends on the value of <see cref="Settings.ArrayStyle"/>. 
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts 
        /// with the first dimension (index #0).</para></remarks>
        
        internal unsafe static Array<short> sort(BaseArray<short> A, int dim = -1, bool descending = false) {
            if (object.Equals(A, null)) {
                return null;
            }
            Array<short> ret = (A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>)?.C;
            ret.Detach();
            if (descending) {
                InnerLoops.SortDesc.Int16.Instance.operate(ret, dim);
            } else {
                InnerLoops.SortAsc.Int16.Instance.operate(ret, dim);
            }
            //(A as RetArray<short>)?.Release(); // A was released in ().C above! 
            return ret;
        }

        /// <summary>
        /// Sort elements of <paramref name="A" /> along the specified dimension. Computes indices also.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="Indices">[Output, optional] On return contains the indices required to sort the array.</param>
        /// <param name="descending">[Optional] Determins the sorting direction. Default: (false) ascending.</param>
        /// <param name="dim">The index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A" />.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim" /> = -1 depends on the value of <see cref="P:ILNumerics.Settings.ArrayStyle" />.
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts
        /// with the first dimension (index #0).</para><para>If <paramref name="Indices" /> is of the same shape as (non-empty, non-scalar) <paramref name="A" /> on entry, its content is sorted with
        /// the values of A. Otherwise and if <paramref name="Indices" /> is not null and not of the same shape as <paramref name="A" />
        /// the function will resize <paramref name="Indices" /> and fill it with zero based indices along the working dimension and sort
        /// these together with <paramref name="A" />.</para><para>It is recommended to initialize <paramref name="Indices" /> with 0 or <see cref="M:ILNumerics.Core.Functions.Builtin.MathInternal.empty``1(ILNumerics.InArray{System.Int64},ILNumerics.StorageOrders)" /> to indicate
        /// that the indices are required. If <paramref name="A" /> is scalar and <paramref name="Indices" /> is not null, any predefined value of <paramref name="Indices" />
        /// is ignored and 0 is returned in <paramref name="Indices" />. That means that initializing <paramref name="Indices" /> with 1
        /// or any other scalar literal (for ease of use) does work also, as long as <paramref name="A" /> is not scalar.</para></remarks>
        
        internal unsafe static Array<short> sort(BaseArray<short> A, OutArray<long> Indices, int dim = -1, bool descending = false) {
            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {
                if (object.Equals(A, null)) {
                    return null;
                }
                if (Equals(Indices, null)) {
                    return sort(A, dim, descending);
                }
                lock (Indices.SynchObj) {
                    Storage<long> indStorage = Indices.Storage;
                    if (indStorage.S.NumberOfElements == 1) {
                        indStorage.Assign(indStorage.SetValue(0, d0: 0));
                    }
                    Array<short> ret = (A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>)?.C;
                    ret.Detach();

                    if (descending) {
                        InnerLoops.SortIndDesc.Int16.Instance.operate(ret, indStorage, dim);
                    } else {
                        InnerLoops.SortIndAsc.Int16.Instance.operate(ret, indStorage, dim);
                    }
                    // has indices' storage been recreated inside the quick sort operator? 
                    // Disabled: we cannot exchange the storage of the incoming OutT here, since this would not 
                    // affect the storage registered as OutArray storage in an _asynchronous AIP_ call, wrapping
                    // this sort function. Instead, Indices storage must not change and we must use Assign()
                    // inside the inner operator to modify the (same) storage directly. 
                    //if (!Equals(indStorage, Indices.Storage)) {
                    //    Indices.Storage.Assign(indStorage, toOutT: true);  // performs locking on Indices array
                    //}
                    return ret;
                }
            }
        }
    }
    namespace InnerLoops {

        namespace SortAsc {

            internal class Int16 :

            UnaryInplaceAxisBase<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> {

                internal static Int16 Instance = new Int16();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortAscST((short*)pSrc, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace SortDesc {

            internal class Int16 :

            UnaryInplaceAxisBase<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> {

                internal static Int16 Instance = new Int16();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortDescST((short*)pSrc, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace SortIndAsc {

            internal class Int16 :  

            UnaryInplaceAxisIndicesBase<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> {

                internal static Int16 Instance = new Int16();
                public unsafe override void Strided64(byte* pSrc, byte* pIndices, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;
                    long* idxStrides = inStrides + ndims; 

                    long accuInStride = inStrides[0] / m_sizeOfT;
                    System.Diagnostics.Debug.Assert(accuInStride == idxStrides[0] / sizeof(long), "This quick sort implementation requires the strides of indices to match the strides of the values array, at least along the working dimension!"); 

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pIndices += cur[i] * idxStrides[i]; 
                    }
                    while(i < ndims) {
                        cur[i++] = 0; 
                    }
                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortAscIDXST((short*)pSrc, (long*)pIndices, 0, dims[0] * accuInStride, accuInStride);
                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pIndices += idxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pIndices -= idxStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace SortIndDesc {

            internal class Int16 :

            UnaryInplaceAxisIndicesBase<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> {

                internal static Int16 Instance = new Int16();
                public unsafe override void Strided64(byte* pSrc, byte* pIndices, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;
                    long* idxStrides = inStrides + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;
                    System.Diagnostics.Debug.Assert(accuInStride == idxStrides[0] / sizeof(long), "This quick sort implementation requires the strides of indices to match the strides of the values array, at least along the working dimension!");

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pIndices += cur[i] * idxStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortDescIDXST((short*)pSrc, (long*)pIndices, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pIndices += idxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pIndices -= idxStrides[d] * dims[d];
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
        /// Sort elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="descending">[Optional] Determins the sorting direction. Default: (false) ascending.</param>
        /// <param name="dim">The index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim"/> = -1 depends on the value of <see cref="Settings.ArrayStyle"/>. 
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts 
        /// with the first dimension (index #0).</para></remarks>
        
        internal unsafe static Array<ushort> sort(BaseArray<ushort> A, int dim = -1, bool descending = false) {
            if (object.Equals(A, null)) {
                return null;
            }
            Array<ushort> ret = (A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>)?.C;
            ret.Detach();
            if (descending) {
                InnerLoops.SortDesc.UInt16.Instance.operate(ret, dim);
            } else {
                InnerLoops.SortAsc.UInt16.Instance.operate(ret, dim);
            }
            //(A as RetArray<ushort>)?.Release(); // A was released in ().C above! 
            return ret;
        }

        /// <summary>
        /// Sort elements of <paramref name="A" /> along the specified dimension. Computes indices also.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="Indices">[Output, optional] On return contains the indices required to sort the array.</param>
        /// <param name="descending">[Optional] Determins the sorting direction. Default: (false) ascending.</param>
        /// <param name="dim">The index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A" />.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim" /> = -1 depends on the value of <see cref="P:ILNumerics.Settings.ArrayStyle" />.
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts
        /// with the first dimension (index #0).</para><para>If <paramref name="Indices" /> is of the same shape as (non-empty, non-scalar) <paramref name="A" /> on entry, its content is sorted with
        /// the values of A. Otherwise and if <paramref name="Indices" /> is not null and not of the same shape as <paramref name="A" />
        /// the function will resize <paramref name="Indices" /> and fill it with zero based indices along the working dimension and sort
        /// these together with <paramref name="A" />.</para><para>It is recommended to initialize <paramref name="Indices" /> with 0 or <see cref="M:ILNumerics.Core.Functions.Builtin.MathInternal.empty``1(ILNumerics.InArray{System.Int64},ILNumerics.StorageOrders)" /> to indicate
        /// that the indices are required. If <paramref name="A" /> is scalar and <paramref name="Indices" /> is not null, any predefined value of <paramref name="Indices" />
        /// is ignored and 0 is returned in <paramref name="Indices" />. That means that initializing <paramref name="Indices" /> with 1
        /// or any other scalar literal (for ease of use) does work also, as long as <paramref name="A" /> is not scalar.</para></remarks>
        
        internal unsafe static Array<ushort> sort(BaseArray<ushort> A, OutArray<long> Indices, int dim = -1, bool descending = false) {
            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {
                if (object.Equals(A, null)) {
                    return null;
                }
                if (Equals(Indices, null)) {
                    return sort(A, dim, descending);
                }
                lock (Indices.SynchObj) {
                    Storage<long> indStorage = Indices.Storage;
                    if (indStorage.S.NumberOfElements == 1) {
                        indStorage.Assign(indStorage.SetValue(0, d0: 0));
                    }
                    Array<ushort> ret = (A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>)?.C;
                    ret.Detach();

                    if (descending) {
                        InnerLoops.SortIndDesc.UInt16.Instance.operate(ret, indStorage, dim);
                    } else {
                        InnerLoops.SortIndAsc.UInt16.Instance.operate(ret, indStorage, dim);
                    }
                    // has indices' storage been recreated inside the quick sort operator? 
                    // Disabled: we cannot exchange the storage of the incoming OutT here, since this would not 
                    // affect the storage registered as OutArray storage in an _asynchronous AIP_ call, wrapping
                    // this sort function. Instead, Indices storage must not change and we must use Assign()
                    // inside the inner operator to modify the (same) storage directly. 
                    //if (!Equals(indStorage, Indices.Storage)) {
                    //    Indices.Storage.Assign(indStorage, toOutT: true);  // performs locking on Indices array
                    //}
                    return ret;
                }
            }
        }
    }
    namespace InnerLoops {

        namespace SortAsc {

            internal class UInt16 :

            UnaryInplaceAxisBase<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> {

                internal static UInt16 Instance = new UInt16();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortAscST((ushort*)pSrc, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace SortDesc {

            internal class UInt16 :

            UnaryInplaceAxisBase<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> {

                internal static UInt16 Instance = new UInt16();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortDescST((ushort*)pSrc, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace SortIndAsc {

            internal class UInt16 :  

            UnaryInplaceAxisIndicesBase<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> {

                internal static UInt16 Instance = new UInt16();
                public unsafe override void Strided64(byte* pSrc, byte* pIndices, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;
                    long* idxStrides = inStrides + ndims; 

                    long accuInStride = inStrides[0] / m_sizeOfT;
                    System.Diagnostics.Debug.Assert(accuInStride == idxStrides[0] / sizeof(long), "This quick sort implementation requires the strides of indices to match the strides of the values array, at least along the working dimension!"); 

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pIndices += cur[i] * idxStrides[i]; 
                    }
                    while(i < ndims) {
                        cur[i++] = 0; 
                    }
                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortAscIDXST((ushort*)pSrc, (long*)pIndices, 0, dims[0] * accuInStride, accuInStride);
                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pIndices += idxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pIndices -= idxStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace SortIndDesc {

            internal class UInt16 :

            UnaryInplaceAxisIndicesBase<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> {

                internal static UInt16 Instance = new UInt16();
                public unsafe override void Strided64(byte* pSrc, byte* pIndices, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;
                    long* idxStrides = inStrides + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;
                    System.Diagnostics.Debug.Assert(accuInStride == idxStrides[0] / sizeof(long), "This quick sort implementation requires the strides of indices to match the strides of the values array, at least along the working dimension!");

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pIndices += cur[i] * idxStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortDescIDXST((ushort*)pSrc, (long*)pIndices, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pIndices += idxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pIndices -= idxStrides[d] * dims[d];
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
        /// Sort elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="descending">[Optional] Determins the sorting direction. Default: (false) ascending.</param>
        /// <param name="dim">The index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim"/> = -1 depends on the value of <see cref="Settings.ArrayStyle"/>. 
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts 
        /// with the first dimension (index #0).</para></remarks>
        
        internal unsafe static Array<sbyte> sort(BaseArray<sbyte> A, int dim = -1, bool descending = false) {
            if (object.Equals(A, null)) {
                return null;
            }
            Array<sbyte> ret = (A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>)?.C;
            ret.Detach();
            if (descending) {
                InnerLoops.SortDesc.SByte.Instance.operate(ret, dim);
            } else {
                InnerLoops.SortAsc.SByte.Instance.operate(ret, dim);
            }
            //(A as RetArray<sbyte>)?.Release(); // A was released in ().C above! 
            return ret;
        }

        /// <summary>
        /// Sort elements of <paramref name="A" /> along the specified dimension. Computes indices also.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="Indices">[Output, optional] On return contains the indices required to sort the array.</param>
        /// <param name="descending">[Optional] Determins the sorting direction. Default: (false) ascending.</param>
        /// <param name="dim">The index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A" />.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim" /> = -1 depends on the value of <see cref="P:ILNumerics.Settings.ArrayStyle" />.
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts
        /// with the first dimension (index #0).</para><para>If <paramref name="Indices" /> is of the same shape as (non-empty, non-scalar) <paramref name="A" /> on entry, its content is sorted with
        /// the values of A. Otherwise and if <paramref name="Indices" /> is not null and not of the same shape as <paramref name="A" />
        /// the function will resize <paramref name="Indices" /> and fill it with zero based indices along the working dimension and sort
        /// these together with <paramref name="A" />.</para><para>It is recommended to initialize <paramref name="Indices" /> with 0 or <see cref="M:ILNumerics.Core.Functions.Builtin.MathInternal.empty``1(ILNumerics.InArray{System.Int64},ILNumerics.StorageOrders)" /> to indicate
        /// that the indices are required. If <paramref name="A" /> is scalar and <paramref name="Indices" /> is not null, any predefined value of <paramref name="Indices" />
        /// is ignored and 0 is returned in <paramref name="Indices" />. That means that initializing <paramref name="Indices" /> with 1
        /// or any other scalar literal (for ease of use) does work also, as long as <paramref name="A" /> is not scalar.</para></remarks>
        
        internal unsafe static Array<sbyte> sort(BaseArray<sbyte> A, OutArray<long> Indices, int dim = -1, bool descending = false) {
            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {
                if (object.Equals(A, null)) {
                    return null;
                }
                if (Equals(Indices, null)) {
                    return sort(A, dim, descending);
                }
                lock (Indices.SynchObj) {
                    Storage<long> indStorage = Indices.Storage;
                    if (indStorage.S.NumberOfElements == 1) {
                        indStorage.Assign(indStorage.SetValue(0, d0: 0));
                    }
                    Array<sbyte> ret = (A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>)?.C;
                    ret.Detach();

                    if (descending) {
                        InnerLoops.SortIndDesc.SByte.Instance.operate(ret, indStorage, dim);
                    } else {
                        InnerLoops.SortIndAsc.SByte.Instance.operate(ret, indStorage, dim);
                    }
                    // has indices' storage been recreated inside the quick sort operator? 
                    // Disabled: we cannot exchange the storage of the incoming OutT here, since this would not 
                    // affect the storage registered as OutArray storage in an _asynchronous AIP_ call, wrapping
                    // this sort function. Instead, Indices storage must not change and we must use Assign()
                    // inside the inner operator to modify the (same) storage directly. 
                    //if (!Equals(indStorage, Indices.Storage)) {
                    //    Indices.Storage.Assign(indStorage, toOutT: true);  // performs locking on Indices array
                    //}
                    return ret;
                }
            }
        }
    }
    namespace InnerLoops {

        namespace SortAsc {

            internal class SByte :

            UnaryInplaceAxisBase<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> {

                internal static SByte Instance = new SByte();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortAscST((sbyte*)pSrc, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace SortDesc {

            internal class SByte :

            UnaryInplaceAxisBase<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> {

                internal static SByte Instance = new SByte();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortDescST((sbyte*)pSrc, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace SortIndAsc {

            internal class SByte :  

            UnaryInplaceAxisIndicesBase<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> {

                internal static SByte Instance = new SByte();
                public unsafe override void Strided64(byte* pSrc, byte* pIndices, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;
                    long* idxStrides = inStrides + ndims; 

                    long accuInStride = inStrides[0] / m_sizeOfT;
                    System.Diagnostics.Debug.Assert(accuInStride == idxStrides[0] / sizeof(long), "This quick sort implementation requires the strides of indices to match the strides of the values array, at least along the working dimension!"); 

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pIndices += cur[i] * idxStrides[i]; 
                    }
                    while(i < ndims) {
                        cur[i++] = 0; 
                    }
                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortAscIDXST((sbyte*)pSrc, (long*)pIndices, 0, dims[0] * accuInStride, accuInStride);
                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pIndices += idxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pIndices -= idxStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace SortIndDesc {

            internal class SByte :

            UnaryInplaceAxisIndicesBase<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> {

                internal static SByte Instance = new SByte();
                public unsafe override void Strided64(byte* pSrc, byte* pIndices, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;
                    long* idxStrides = inStrides + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;
                    System.Diagnostics.Debug.Assert(accuInStride == idxStrides[0] / sizeof(long), "This quick sort implementation requires the strides of indices to match the strides of the values array, at least along the working dimension!");

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pIndices += cur[i] * idxStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortDescIDXST((sbyte*)pSrc, (long*)pIndices, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pIndices += idxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pIndices -= idxStrides[d] * dims[d];
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
        /// Sort elements of <paramref name="A"/> along the specified dimension.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="descending">[Optional] Determins the sorting direction. Default: (false) ascending.</param>
        /// <param name="dim">The index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim"/> = -1 depends on the value of <see cref="Settings.ArrayStyle"/>. 
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts 
        /// with the first dimension (index #0).</para></remarks>
        
        internal unsafe static Array<byte> sort(BaseArray<byte> A, int dim = -1, bool descending = false) {
            if (object.Equals(A, null)) {
                return null;
            }
            Array<byte> ret = (A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>)?.C;
            ret.Detach();
            if (descending) {
                InnerLoops.SortDesc.Byte.Instance.operate(ret, dim);
            } else {
                InnerLoops.SortAsc.Byte.Instance.operate(ret, dim);
            }
            //(A as RetArray<byte>)?.Release(); // A was released in ().C above! 
            return ret;
        }

        /// <summary>
        /// Sort elements of <paramref name="A" /> along the specified dimension. Computes indices also.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="Indices">[Output, optional] On return contains the indices required to sort the array.</param>
        /// <param name="descending">[Optional] Determins the sorting direction. Default: (false) ascending.</param>
        /// <param name="dim">The index of the working dimension or -1 for determining the working dimension automatically (default).</param>
        /// <returns>New array with the same shape as <paramref name="A" />.</returns>
        /// <remarks><para>The working dimension for <paramref name="dim" /> = -1 depends on the value of <see cref="P:ILNumerics.Settings.ArrayStyle" />.
        /// For numpy style it starts with the last dimension to search for a non-singleton dimension. For ILNumericsV4 the search starts
        /// with the first dimension (index #0).</para><para>If <paramref name="Indices" /> is of the same shape as (non-empty, non-scalar) <paramref name="A" /> on entry, its content is sorted with
        /// the values of A. Otherwise and if <paramref name="Indices" /> is not null and not of the same shape as <paramref name="A" />
        /// the function will resize <paramref name="Indices" /> and fill it with zero based indices along the working dimension and sort
        /// these together with <paramref name="A" />.</para><para>It is recommended to initialize <paramref name="Indices" /> with 0 or <see cref="M:ILNumerics.Core.Functions.Builtin.MathInternal.empty``1(ILNumerics.InArray{System.Int64},ILNumerics.StorageOrders)" /> to indicate
        /// that the indices are required. If <paramref name="A" /> is scalar and <paramref name="Indices" /> is not null, any predefined value of <paramref name="Indices" />
        /// is ignored and 0 is returned in <paramref name="Indices" />. That means that initializing <paramref name="Indices" /> with 1
        /// or any other scalar literal (for ease of use) does work also, as long as <paramref name="A" /> is not scalar.</para></remarks>
        
        internal unsafe static Array<byte> sort(BaseArray<byte> A, OutArray<long> Indices, int dim = -1, bool descending = false) {
            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {
                if (object.Equals(A, null)) {
                    return null;
                }
                if (Equals(Indices, null)) {
                    return sort(A, dim, descending);
                }
                lock (Indices.SynchObj) {
                    Storage<long> indStorage = Indices.Storage;
                    if (indStorage.S.NumberOfElements == 1) {
                        indStorage.Assign(indStorage.SetValue(0, d0: 0));
                    }
                    Array<byte> ret = (A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>)?.C;
                    ret.Detach();

                    if (descending) {
                        InnerLoops.SortIndDesc.Byte.Instance.operate(ret, indStorage, dim);
                    } else {
                        InnerLoops.SortIndAsc.Byte.Instance.operate(ret, indStorage, dim);
                    }
                    // has indices' storage been recreated inside the quick sort operator? 
                    // Disabled: we cannot exchange the storage of the incoming OutT here, since this would not 
                    // affect the storage registered as OutArray storage in an _asynchronous AIP_ call, wrapping
                    // this sort function. Instead, Indices storage must not change and we must use Assign()
                    // inside the inner operator to modify the (same) storage directly. 
                    //if (!Equals(indStorage, Indices.Storage)) {
                    //    Indices.Storage.Assign(indStorage, toOutT: true);  // performs locking on Indices array
                    //}
                    return ret;
                }
            }
        }
    }
    namespace InnerLoops {

        namespace SortAsc {

            internal class Byte :

            UnaryInplaceAxisBase<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> {

                internal static Byte Instance = new Byte();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortAscST((byte*)pSrc, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace SortDesc {

            internal class Byte :

            UnaryInplaceAxisBase<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> {

                internal static Byte Instance = new Byte();
                public unsafe override void Strided64(byte* pSrc, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortDescST((byte*)pSrc, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace SortIndAsc {

            internal class Byte :  

            UnaryInplaceAxisIndicesBase<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> {

                internal static Byte Instance = new Byte();
                public unsafe override void Strided64(byte* pSrc, byte* pIndices, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;
                    long* idxStrides = inStrides + ndims; 

                    long accuInStride = inStrides[0] / m_sizeOfT;
                    System.Diagnostics.Debug.Assert(accuInStride == idxStrides[0] / sizeof(long), "This quick sort implementation requires the strides of indices to match the strides of the values array, at least along the working dimension!"); 

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pIndices += cur[i] * idxStrides[i]; 
                    }
                    while(i < ndims) {
                        cur[i++] = 0; 
                    }
                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortAscIDXST((byte*)pSrc, (long*)pIndices, 0, dims[0] * accuInStride, accuInStride);
                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pIndices += idxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pIndices -= idxStrides[d] * dims[d];
                                cur[d] = 0;
                                d++;
                            }
                        }
                    }
                }
            }
        }
        namespace SortIndDesc {

            internal class Byte :

            UnaryInplaceAxisIndicesBase<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> {

                internal static Byte Instance = new Byte();
                public unsafe override void Strided64(byte* pSrc, byte* pIndices, long start, long len, long* iterationBSD) {

                    /*
                     * IterBSD: dims: -1
                     *          dims[axis] is reordered to be the FIRST dimension
                     *          strides is byte stride: element length is factored in
                     *          baseIndex is byte size: element length is factored in
                     *          
                     * pSrc is the first element of the whole array (not the memoryhandle start! Not the start of this working dim iteration. Includes the base offset)
                     * start, len: corresponds to number of elements to work through in _higher dimensions_. Working dims lenght (iterationBSD[3]) is excluded here!
                     */
                    System.Diagnostics.Debug.Assert(pSrc != (byte*)0); 
                    System.Diagnostics.Debug.Assert(start >= 0);
                    System.Diagnostics.Debug.Assert(len >= 0);

                    uint ndims = (uint)iterationBSD[0];
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");


                    long* dims = iterationBSD + 3;
                    long* inStrides = iterationBSD + 3 + ndims;
                    long* idxStrides = inStrides + ndims;

                    long accuInStride = inStrides[0] / m_sizeOfT;
                    System.Diagnostics.Debug.Assert(accuInStride == idxStrides[0] / sizeof(long), "This quick sort implementation requires the strides of indices to match the strides of the values array, at least along the working dimension!");

                    long* cur = stackalloc long[(int)ndims]; // assumes stackalloc to clear values (unspecified!)

                    // figure out the dimension index position for start
                    // chunks (start) are split along higher dimensions only. 1st dimension is the accumulation dim and always completely done by a single thread.
                    long f = cur[0] = start;
                    pSrc += iterationBSD[2];
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        cur[i] = f % (dims[i] + 1);
                        f /= (dims[i] + 1);
                        pSrc += cur[i] * inStrides[i];
                        pIndices += cur[i] * idxStrides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0;
                    }

                    System.Diagnostics.Debug.Assert(f == 0);
                    System.Diagnostics.Debug.Assert(dims[0] > 0); //scalar working dims are a NoP and handled outside this function!
                    // Note, dims[] is actual length minus 1!

                    while (true) {

                        ILNumerics.Core.Misc.QuickSort.QuickSortDescIDXST((byte*)pSrc, (long*)pIndices, 0, dims[0] * accuInStride, accuInStride); 

                        // iteration always goes along the accumulation length, out-element by out-element..

                        if (--len == 0) {
                            break;
                        }

                        // increase current position
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                pSrc += inStrides[d];
                                pIndices += idxStrides[d];
                                cur[d]++;
                                break;
                            } else {
                                pSrc -= inStrides[d] * dims[d];
                                pIndices -= idxStrides[d] * dims[d];
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

