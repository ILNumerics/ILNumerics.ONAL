//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////
using ILNumerics.Core.Arrays;
using ILNumerics.Core.Functions.Builtin;
using ILNumerics.Core.Global;
using ILNumerics.Core.Native;
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using static ILNumerics.Core.Functions.Builtin.MathInternal; 

namespace ILNumerics.Core.Functions.Builtin {

    internal static partial class MathInternal {

        #region HYCALPER LOOPSTART ACCUMULATING_ALL_IND_TEMPLATE
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
                <type>
                <source locate="nextline">
                    IsInteger 
                </source>
                <destination><![CDATA[#if !IsInteger]]></destination>
                <destination><![CDATA[#if !IsInteger]]></destination>
                <destination><![CDATA[#if !IsInteger]]></destination>
                <destination><![CDATA[#if !IsInteger]]></destination>
                <destination><![CDATA[#if !IsInteger]]></destination>
                <destination><![CDATA[#if !IsInteger]]></destination>
                <destination><![CDATA[#if !IsInteger]]></destination>
                <destination><![CDATA[#if !IsInteger]]></destination>
                <destination><![CDATA[#if IsInteger]]></destination>
                <destination><![CDATA[#if IsInteger]]></destination>
                <destination><![CDATA[#if IsInteger]]></destination>
                </type>
                </hycalper>
                */

        /// <summary>
        /// Computes the (flattened, row-major) sequential index and value of the element with the maximum value in <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the maximum for. This is not altered.</param>
        /// <param name="index">[Optional] index of the maximum value. Default: (null) the index is not returned.</param>
        /// <param name="order">[Optional] iteration order for the flattened sequential indices. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <param name="ignoreNaN">[Optional] determins how NaN values are handled for floating point element types. Ignored for integer elements. Defaul: (true) ignores NaNs.</param>
        /// <returns>Scalar array with the maximum value in A.</returns>
        /// <remarks><para>See <see cref="MathInternal.max(BaseArray{double}, OutArray{long}, int, bool)"/> for 
        /// getting the maximum values along a specific dimension. </para>
        /// <para>The functions 'max','min','maxall','minall' give all similar results between the framework APIs (ILNumericsV4/Matlab(R) / numpy)
        /// but show individual behavior regarding the handling of floating point NaN values and iteration order. The value of 'true' 
        /// for <paramref name="ignoreNaN"/> corresponds to the behavior of ILNumerics version 4/ Matlab(R). Only if 
        /// all elements in <paramref name="A"/> are <see cref="float.NaN"/> the value of the result will also be NaN. This is the default.</para>
        /// <para>The value of 'false' for <paramref name="ignoreNaN"/> gives the numpy behavior: NaN values take precedence over non-NaN values. If 
        /// one element in the set of values is NaN the result will also be NaN. This is the default behavior for such functions defined as 
        /// [numpy API] extension methods on the array classes.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{double}, OutArray{long}, int, bool)"/>
        internal static unsafe Array<double> maxall(ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> A,
            OutArray<long> index = null, StorageOrders order = StorageOrders.ColumnMajor, bool ignoreNaN = true) {


            /*
             * Algorithm option 1: 
             * 1) find the longest dim: maxDimIdx 
             * 2) perform max(I:I) on the longest dim
             * 2.1) modify indices to reflect their sequential indices (row major!) <- this was the showstopper
             * 3) sort max values + indices descending
             * 4) max value + max index is a position [0].
             *
             * Option 2 (recommended): 
             * Implement maxall via parallelized operator, handling indices. 
             * */

            // 1) find the longest dim: maxDimIdx 
            //uint maxDimIdx = 0;
            //long maxDimVal = -1;
            //for (uint i = 0; i < ndims; i++) {
            //    if (storage.S[i] > maxDimVal) {
            //        maxDimVal = storage.S[i];
            //        maxDimIdx = i;
            //    }
            //}
            //// 2) perform max(I:I) on the longest dim
            //Array<long> I = 0; 
            //Array<double> maxVals = max(A, I: I, dim: (int)maxDimIdx);

            //// 3) sort max values + indices descending
            //System.Diagnostics.Debug.Assert(I.S.IsSameShape(maxVals.S));
            //// ... not finished ...

            // following is the naive implementation (slow)
            using var _1 = ILNumerics.Core.Global.ReaderLock.Create(A, out var storage, releaseRetT: true);

            if (storage.S.NumberOfElements == 0) {
                // empty A 
                throw new ArgumentException("Unable to compute the maximum of an empty array.");
            }
            if (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor) {
                throw new ArgumentException($"Iteration order must be either {nameof(StorageOrders.ColumnMajor)} or {nameof(StorageOrders.RowMajor)}.");
            }
            var it = storage.S.Iterator(order).GetEnumerator();
            double* pA = (double*)storage.Handles[0].Pointer;
            long curI = -1, maxIdx = 0;
            double maxVal = 0;
            /*!HC:IsInteger*/
#if IsInteger
                // no NaN handling
                maxVal = storage.GetValue(0);
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (curVal /*!HC:operation*/ > maxVal) {
                        maxVal = curVal;
                        maxIdx = curI;
                    }
                }
#else
            if (ignoreNaN) {
                // Floating point: NaN the Matlab way (ignored if possible)
                // init with first non-NaN value
                while (it.MoveNext()) {
                    curI++;
                    if (!double.IsNaN(maxVal = pA[it.Current])) {
                        break;
                    }
                }
                if (curI < storage.S.NumberOfElements) {
                    // at least one non-NaN exists. It is stored in maxVal now.
                    System.Diagnostics.Debug.Assert(!double.IsNaN(maxVal));

                    while (it.MoveNext()) {
                        curI++;
                        var curVal = pA[it.Current];
                        if (curVal /*!HC:operation*/ > maxVal) { // NaNs compare: false
                            maxVal = curVal;
                            maxIdx = curI;
                        }
                    }
                } else {
                    // all NaNs
                    maxIdx = 0;
                    System.Diagnostics.Debug.Assert(double.IsNaN(maxVal));
                }
            } else {
                // Floating point: NaN the numpy way (precedence) 
                maxVal = storage.GetValue(0);  // may be NaN!
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (double.IsNaN(curVal)) {
                        maxVal = curVal;
                        maxIdx = curI;
                        break;
                    }
                    if (curVal /*!HC:operation*/ > maxVal) {
                        maxVal = curVal;
                        maxIdx = curI;
                    }
                }
            }
#endif
            if (!Equals(index, null)) {
                lock (index.SynchObj)
                    index.a = maxIdx;
            }
            return maxVal;
        }
#endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        /// <summary>
        /// Computes the (flattened, row-major) sequential index and value of the element with the maximum value in <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the maximum for. This is not altered.</param>
        /// <param name="index">[Optional] index of the maximum value. Default: (null) the index is not returned.</param>
        /// <param name="order">[Optional] iteration order for the flattened sequential indices. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <param name="ignoreNaN">[Optional] determins how NaN values are handled for floating point element types. Ignored for integer elements. Defaul: (true) ignores NaNs.</param>
        /// <returns>Scalar array with the maximum value in A.</returns>
        /// <remarks><para>See <see cref="MathInternal.max(BaseArray{fcomplex}, OutArray{long}, int, bool)"/> for 
        /// getting the maximum values along a specific dimension. </para>
        /// <para>The functions 'max','min','maxall','minall' give all similar results between the framework APIs (ILNumericsV4/Matlab(R) / numpy)
        /// but show individual behavior regarding the handling of floating point NaN values and iteration order. The value of 'true' 
        /// for <paramref name="ignoreNaN"/> corresponds to the behavior of ILNumerics version 4/ Matlab(R). Only if 
        /// all elements in <paramref name="A"/> are <see cref="float.NaN"/> the value of the result will also be NaN. This is the default.</para>
        /// <para>The value of 'false' for <paramref name="ignoreNaN"/> gives the numpy behavior: NaN values take precedence over non-NaN values. If 
        /// one element in the set of values is NaN the result will also be NaN. This is the default behavior for such functions defined as 
        /// [numpy API] extension methods on the array classes.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{fcomplex}, OutArray{long}, int, bool)"/>
        internal static unsafe Array<fcomplex> maxall(ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> A,
            OutArray<long> index = null, StorageOrders order = StorageOrders.ColumnMajor, bool ignoreNaN = true) {


            /*
             * Algorithm option 1: 
             * 1) find the longest dim: maxDimIdx 
             * 2) perform max(I:I) on the longest dim
             * 2.1) modify indices to reflect their sequential indices (row major!) <- this was the showstopper
             * 3) sort max values + indices descending
             * 4) max value + max index is a position [0].
             *
             * Option 2 (recommended): 
             * Implement maxall via parallelized operator, handling indices. 
             * */

            // 1) find the longest dim: maxDimIdx 
            //uint maxDimIdx = 0;
            //long maxDimVal = -1;
            //for (uint i = 0; i < ndims; i++) {
            //    if (storage.S[i] > maxDimVal) {
            //        maxDimVal = storage.S[i];
            //        maxDimIdx = i;
            //    }
            //}
            //// 2) perform max(I:I) on the longest dim
            //Array<long> I = 0; 
            //Array<fcomplex> maxVals = max(A, I: I, dim: (int)maxDimIdx);

            //// 3) sort max values + indices descending
            //System.Diagnostics.Debug.Assert(I.S.IsSameShape(maxVals.S));
            //// ... not finished ...

            // following is the naive implementation (slow)
            using var _1 = ILNumerics.Core.Global.ReaderLock.Create(A, out var storage, releaseRetT: true);

            if (storage.S.NumberOfElements == 0) {
                // empty A 
                throw new ArgumentException("Unable to compute the maximum of an empty array.");
            }
            if (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor) {
                throw new ArgumentException($"Iteration order must be either {nameof(StorageOrders.ColumnMajor)} or {nameof(StorageOrders.RowMajor)}.");
            }
            var it = storage.S.Iterator(order).GetEnumerator();
            fcomplex* pA = (fcomplex*)storage.Handles[0].Pointer;
            long curI = -1, maxIdx = 0;
            fcomplex maxVal = 0;
            #if IsInteger
                maxVal = storage.GetValue(0);
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (curVal  > maxVal) {
                        maxVal = curVal;
                        maxIdx = curI;
                    }
                }
#else
            if (ignoreNaN) {
                // Floating point: NaN the Matlab way (ignored if possible)
                // init with first non-NaN value
                while (it.MoveNext()) {
                    curI++;
                    if (!fcomplex.IsNaN(maxVal = pA[it.Current])) {
                        break;
                    }
                }
                if (curI < storage.S.NumberOfElements) {
                    // at least one non-NaN exists. It is stored in maxVal now.
                    System.Diagnostics.Debug.Assert(!fcomplex.IsNaN(maxVal));

                    while (it.MoveNext()) {
                        curI++;
                        var curVal = pA[it.Current];
                        if (curVal  > maxVal) { // NaNs compare: false
                            maxVal = curVal;
                            maxIdx = curI;
                        }
                    }
                } else {
                    // all NaNs
                    maxIdx = 0;
                    System.Diagnostics.Debug.Assert(fcomplex.IsNaN(maxVal));
                }
            } else {
                // Floating point: NaN the numpy way (precedence) 
                maxVal = storage.GetValue(0);  // may be NaN!
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (fcomplex.IsNaN(curVal)) {
                        maxVal = curVal;
                        maxIdx = curI;
                        break;
                    }
                    if (curVal  > maxVal) {
                        maxVal = curVal;
                        maxIdx = curI;
                    }
                }
            }
#endif
            if (!Equals(index, null)) {
                lock (index.SynchObj)
                    index.a = maxIdx;
            }
            return maxVal;
        }
       

        /// <summary>
        /// Computes the (flattened, row-major) sequential index and value of the element with the maximum value in <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the maximum for. This is not altered.</param>
        /// <param name="index">[Optional] index of the maximum value. Default: (null) the index is not returned.</param>
        /// <param name="order">[Optional] iteration order for the flattened sequential indices. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <param name="ignoreNaN">[Optional] determins how NaN values are handled for floating point element types. Ignored for integer elements. Defaul: (true) ignores NaNs.</param>
        /// <returns>Scalar array with the maximum value in A.</returns>
        /// <remarks><para>See <see cref="MathInternal.max(BaseArray{complex}, OutArray{long}, int, bool)"/> for 
        /// getting the maximum values along a specific dimension. </para>
        /// <para>The functions 'max','min','maxall','minall' give all similar results between the framework APIs (ILNumericsV4/Matlab(R) / numpy)
        /// but show individual behavior regarding the handling of floating point NaN values and iteration order. The value of 'true' 
        /// for <paramref name="ignoreNaN"/> corresponds to the behavior of ILNumerics version 4/ Matlab(R). Only if 
        /// all elements in <paramref name="A"/> are <see cref="float.NaN"/> the value of the result will also be NaN. This is the default.</para>
        /// <para>The value of 'false' for <paramref name="ignoreNaN"/> gives the numpy behavior: NaN values take precedence over non-NaN values. If 
        /// one element in the set of values is NaN the result will also be NaN. This is the default behavior for such functions defined as 
        /// [numpy API] extension methods on the array classes.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{complex}, OutArray{long}, int, bool)"/>
        internal static unsafe Array<complex> maxall(ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> A,
            OutArray<long> index = null, StorageOrders order = StorageOrders.ColumnMajor, bool ignoreNaN = true) {


            /*
             * Algorithm option 1: 
             * 1) find the longest dim: maxDimIdx 
             * 2) perform max(I:I) on the longest dim
             * 2.1) modify indices to reflect their sequential indices (row major!) <- this was the showstopper
             * 3) sort max values + indices descending
             * 4) max value + max index is a position [0].
             *
             * Option 2 (recommended): 
             * Implement maxall via parallelized operator, handling indices. 
             * */

            // 1) find the longest dim: maxDimIdx 
            //uint maxDimIdx = 0;
            //long maxDimVal = -1;
            //for (uint i = 0; i < ndims; i++) {
            //    if (storage.S[i] > maxDimVal) {
            //        maxDimVal = storage.S[i];
            //        maxDimIdx = i;
            //    }
            //}
            //// 2) perform max(I:I) on the longest dim
            //Array<long> I = 0; 
            //Array<complex> maxVals = max(A, I: I, dim: (int)maxDimIdx);

            //// 3) sort max values + indices descending
            //System.Diagnostics.Debug.Assert(I.S.IsSameShape(maxVals.S));
            //// ... not finished ...

            // following is the naive implementation (slow)
            using var _1 = ILNumerics.Core.Global.ReaderLock.Create(A, out var storage, releaseRetT: true);

            if (storage.S.NumberOfElements == 0) {
                // empty A 
                throw new ArgumentException("Unable to compute the maximum of an empty array.");
            }
            if (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor) {
                throw new ArgumentException($"Iteration order must be either {nameof(StorageOrders.ColumnMajor)} or {nameof(StorageOrders.RowMajor)}.");
            }
            var it = storage.S.Iterator(order).GetEnumerator();
            complex* pA = (complex*)storage.Handles[0].Pointer;
            long curI = -1, maxIdx = 0;
            complex maxVal = 0;
            #if IsInteger
                maxVal = storage.GetValue(0);
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (curVal  > maxVal) {
                        maxVal = curVal;
                        maxIdx = curI;
                    }
                }
#else
            if (ignoreNaN) {
                // Floating point: NaN the Matlab way (ignored if possible)
                // init with first non-NaN value
                while (it.MoveNext()) {
                    curI++;
                    if (!complex.IsNaN(maxVal = pA[it.Current])) {
                        break;
                    }
                }
                if (curI < storage.S.NumberOfElements) {
                    // at least one non-NaN exists. It is stored in maxVal now.
                    System.Diagnostics.Debug.Assert(!complex.IsNaN(maxVal));

                    while (it.MoveNext()) {
                        curI++;
                        var curVal = pA[it.Current];
                        if (curVal  > maxVal) { // NaNs compare: false
                            maxVal = curVal;
                            maxIdx = curI;
                        }
                    }
                } else {
                    // all NaNs
                    maxIdx = 0;
                    System.Diagnostics.Debug.Assert(complex.IsNaN(maxVal));
                }
            } else {
                // Floating point: NaN the numpy way (precedence) 
                maxVal = storage.GetValue(0);  // may be NaN!
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (complex.IsNaN(curVal)) {
                        maxVal = curVal;
                        maxIdx = curI;
                        break;
                    }
                    if (curVal  > maxVal) {
                        maxVal = curVal;
                        maxIdx = curI;
                    }
                }
            }
#endif
            if (!Equals(index, null)) {
                lock (index.SynchObj)
                    index.a = maxIdx;
            }
            return maxVal;
        }
       

        /// <summary>
        /// Computes the (flattened, row-major) sequential index and value of the element with the maximum value in <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the maximum for. This is not altered.</param>
        /// <param name="index">[Optional] index of the maximum value. Default: (null) the index is not returned.</param>
        /// <param name="order">[Optional] iteration order for the flattened sequential indices. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <param name="ignoreNaN">[Optional] determins how NaN values are handled for floating point element types. Ignored for integer elements. Defaul: (true) ignores NaNs.</param>
        /// <returns>Scalar array with the maximum value in A.</returns>
        /// <remarks><para>See <see cref="MathInternal.max(BaseArray{float}, OutArray{long}, int, bool)"/> for 
        /// getting the maximum values along a specific dimension. </para>
        /// <para>The functions 'max','min','maxall','minall' give all similar results between the framework APIs (ILNumericsV4/Matlab(R) / numpy)
        /// but show individual behavior regarding the handling of floating point NaN values and iteration order. The value of 'true' 
        /// for <paramref name="ignoreNaN"/> corresponds to the behavior of ILNumerics version 4/ Matlab(R). Only if 
        /// all elements in <paramref name="A"/> are <see cref="float.NaN"/> the value of the result will also be NaN. This is the default.</para>
        /// <para>The value of 'false' for <paramref name="ignoreNaN"/> gives the numpy behavior: NaN values take precedence over non-NaN values. If 
        /// one element in the set of values is NaN the result will also be NaN. This is the default behavior for such functions defined as 
        /// [numpy API] extension methods on the array classes.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{float}, OutArray{long}, int, bool)"/>
        internal static unsafe Array<float> maxall(ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> A,
            OutArray<long> index = null, StorageOrders order = StorageOrders.ColumnMajor, bool ignoreNaN = true) {


            /*
             * Algorithm option 1: 
             * 1) find the longest dim: maxDimIdx 
             * 2) perform max(I:I) on the longest dim
             * 2.1) modify indices to reflect their sequential indices (row major!) <- this was the showstopper
             * 3) sort max values + indices descending
             * 4) max value + max index is a position [0].
             *
             * Option 2 (recommended): 
             * Implement maxall via parallelized operator, handling indices. 
             * */

            // 1) find the longest dim: maxDimIdx 
            //uint maxDimIdx = 0;
            //long maxDimVal = -1;
            //for (uint i = 0; i < ndims; i++) {
            //    if (storage.S[i] > maxDimVal) {
            //        maxDimVal = storage.S[i];
            //        maxDimIdx = i;
            //    }
            //}
            //// 2) perform max(I:I) on the longest dim
            //Array<long> I = 0; 
            //Array<float> maxVals = max(A, I: I, dim: (int)maxDimIdx);

            //// 3) sort max values + indices descending
            //System.Diagnostics.Debug.Assert(I.S.IsSameShape(maxVals.S));
            //// ... not finished ...

            // following is the naive implementation (slow)
            using var _1 = ILNumerics.Core.Global.ReaderLock.Create(A, out var storage, releaseRetT: true);

            if (storage.S.NumberOfElements == 0) {
                // empty A 
                throw new ArgumentException("Unable to compute the maximum of an empty array.");
            }
            if (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor) {
                throw new ArgumentException($"Iteration order must be either {nameof(StorageOrders.ColumnMajor)} or {nameof(StorageOrders.RowMajor)}.");
            }
            var it = storage.S.Iterator(order).GetEnumerator();
            float* pA = (float*)storage.Handles[0].Pointer;
            long curI = -1, maxIdx = 0;
            float maxVal = 0;
            #if IsInteger
                maxVal = storage.GetValue(0);
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (curVal  > maxVal) {
                        maxVal = curVal;
                        maxIdx = curI;
                    }
                }
#else
            if (ignoreNaN) {
                // Floating point: NaN the Matlab way (ignored if possible)
                // init with first non-NaN value
                while (it.MoveNext()) {
                    curI++;
                    if (!float.IsNaN(maxVal = pA[it.Current])) {
                        break;
                    }
                }
                if (curI < storage.S.NumberOfElements) {
                    // at least one non-NaN exists. It is stored in maxVal now.
                    System.Diagnostics.Debug.Assert(!float.IsNaN(maxVal));

                    while (it.MoveNext()) {
                        curI++;
                        var curVal = pA[it.Current];
                        if (curVal  > maxVal) { // NaNs compare: false
                            maxVal = curVal;
                            maxIdx = curI;
                        }
                    }
                } else {
                    // all NaNs
                    maxIdx = 0;
                    System.Diagnostics.Debug.Assert(float.IsNaN(maxVal));
                }
            } else {
                // Floating point: NaN the numpy way (precedence) 
                maxVal = storage.GetValue(0);  // may be NaN!
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (float.IsNaN(curVal)) {
                        maxVal = curVal;
                        maxIdx = curI;
                        break;
                    }
                    if (curVal  > maxVal) {
                        maxVal = curVal;
                        maxIdx = curI;
                    }
                }
            }
#endif
            if (!Equals(index, null)) {
                lock (index.SynchObj)
                    index.a = maxIdx;
            }
            return maxVal;
        }
       

        /// <summary>
        /// Computes the (flattened, row-major) sequential index and value of the element with the maximum value in <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the maximum for. This is not altered.</param>
        /// <param name="index">[Optional] index of the maximum value. Default: (null) the index is not returned.</param>
        /// <param name="order">[Optional] iteration order for the flattened sequential indices. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <param name="ignoreNaN">[Optional] determins how NaN values are handled for floating point element types. Ignored for integer elements. Defaul: (true) ignores NaNs.</param>
        /// <returns>Scalar array with the maximum value in A.</returns>
        /// <remarks><para>See <see cref="MathInternal.max(BaseArray{long}, OutArray{long}, int, bool)"/> for 
        /// getting the maximum values along a specific dimension. </para>
        /// <para>The functions 'max','min','maxall','minall' give all similar results between the framework APIs (ILNumericsV4/Matlab(R) / numpy)
        /// but show individual behavior regarding the handling of floating point NaN values and iteration order. The value of 'true' 
        /// for <paramref name="ignoreNaN"/> corresponds to the behavior of ILNumerics version 4/ Matlab(R). Only if 
        /// all elements in <paramref name="A"/> are <see cref="float.NaN"/> the value of the result will also be NaN. This is the default.</para>
        /// <para>The value of 'false' for <paramref name="ignoreNaN"/> gives the numpy behavior: NaN values take precedence over non-NaN values. If 
        /// one element in the set of values is NaN the result will also be NaN. This is the default behavior for such functions defined as 
        /// [numpy API] extension methods on the array classes.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{long}, OutArray{long}, int, bool)"/>
        internal static unsafe Array<long> maxall(ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> A,
            OutArray<long> index = null, StorageOrders order = StorageOrders.ColumnMajor, bool ignoreNaN = true) {


            /*
             * Algorithm option 1: 
             * 1) find the longest dim: maxDimIdx 
             * 2) perform max(I:I) on the longest dim
             * 2.1) modify indices to reflect their sequential indices (row major!) <- this was the showstopper
             * 3) sort max values + indices descending
             * 4) max value + max index is a position [0].
             *
             * Option 2 (recommended): 
             * Implement maxall via parallelized operator, handling indices. 
             * */

            // 1) find the longest dim: maxDimIdx 
            //uint maxDimIdx = 0;
            //long maxDimVal = -1;
            //for (uint i = 0; i < ndims; i++) {
            //    if (storage.S[i] > maxDimVal) {
            //        maxDimVal = storage.S[i];
            //        maxDimIdx = i;
            //    }
            //}
            //// 2) perform max(I:I) on the longest dim
            //Array<long> I = 0; 
            //Array<long> maxVals = max(A, I: I, dim: (int)maxDimIdx);

            //// 3) sort max values + indices descending
            //System.Diagnostics.Debug.Assert(I.S.IsSameShape(maxVals.S));
            //// ... not finished ...

            // following is the naive implementation (slow)
            using var _1 = ILNumerics.Core.Global.ReaderLock.Create(A, out var storage, releaseRetT: true);

            if (storage.S.NumberOfElements == 0) {
                // empty A 
                throw new ArgumentException("Unable to compute the maximum of an empty array.");
            }
            if (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor) {
                throw new ArgumentException($"Iteration order must be either {nameof(StorageOrders.ColumnMajor)} or {nameof(StorageOrders.RowMajor)}.");
            }
            var it = storage.S.Iterator(order).GetEnumerator();
            long* pA = (long*)storage.Handles[0].Pointer;
            long curI = -1, maxIdx = 0;
            long maxVal = 0;
            #if !IsInteger
                maxVal = storage.GetValue(0);
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (curVal  > maxVal) {
                        maxVal = curVal;
                        maxIdx = curI;
                    }
                }
#else
            if (ignoreNaN) {
                // Floating point: NaN the Matlab way (ignored if possible)
                // init with first non-NaN value
                while (it.MoveNext()) {
                    curI++;
                    if (!long.IsNaN(maxVal = pA[it.Current])) {
                        break;
                    }
                }
                if (curI < storage.S.NumberOfElements) {
                    // at least one non-NaN exists. It is stored in maxVal now.
                    System.Diagnostics.Debug.Assert(!long.IsNaN(maxVal));

                    while (it.MoveNext()) {
                        curI++;
                        var curVal = pA[it.Current];
                        if (curVal  > maxVal) { // NaNs compare: false
                            maxVal = curVal;
                            maxIdx = curI;
                        }
                    }
                } else {
                    // all NaNs
                    maxIdx = 0;
                    System.Diagnostics.Debug.Assert(long.IsNaN(maxVal));
                }
            } else {
                // Floating point: NaN the numpy way (precedence) 
                maxVal = storage.GetValue(0);  // may be NaN!
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (long.IsNaN(curVal)) {
                        maxVal = curVal;
                        maxIdx = curI;
                        break;
                    }
                    if (curVal  > maxVal) {
                        maxVal = curVal;
                        maxIdx = curI;
                    }
                }
            }
#endif
            if (!Equals(index, null)) {
                lock (index.SynchObj)
                    index.a = maxIdx;
            }
            return maxVal;
        }
       

        /// <summary>
        /// Computes the (flattened, row-major) sequential index and value of the element with the maximum value in <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the maximum for. This is not altered.</param>
        /// <param name="index">[Optional] index of the maximum value. Default: (null) the index is not returned.</param>
        /// <param name="order">[Optional] iteration order for the flattened sequential indices. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <param name="ignoreNaN">[Optional] determins how NaN values are handled for floating point element types. Ignored for integer elements. Defaul: (true) ignores NaNs.</param>
        /// <returns>Scalar array with the maximum value in A.</returns>
        /// <remarks><para>See <see cref="MathInternal.max(BaseArray{ulong}, OutArray{long}, int, bool)"/> for 
        /// getting the maximum values along a specific dimension. </para>
        /// <para>The functions 'max','min','maxall','minall' give all similar results between the framework APIs (ILNumericsV4/Matlab(R) / numpy)
        /// but show individual behavior regarding the handling of floating point NaN values and iteration order. The value of 'true' 
        /// for <paramref name="ignoreNaN"/> corresponds to the behavior of ILNumerics version 4/ Matlab(R). Only if 
        /// all elements in <paramref name="A"/> are <see cref="float.NaN"/> the value of the result will also be NaN. This is the default.</para>
        /// <para>The value of 'false' for <paramref name="ignoreNaN"/> gives the numpy behavior: NaN values take precedence over non-NaN values. If 
        /// one element in the set of values is NaN the result will also be NaN. This is the default behavior for such functions defined as 
        /// [numpy API] extension methods on the array classes.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{ulong}, OutArray{long}, int, bool)"/>
        internal static unsafe Array<ulong> maxall(ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> A,
            OutArray<long> index = null, StorageOrders order = StorageOrders.ColumnMajor, bool ignoreNaN = true) {


            /*
             * Algorithm option 1: 
             * 1) find the longest dim: maxDimIdx 
             * 2) perform max(I:I) on the longest dim
             * 2.1) modify indices to reflect their sequential indices (row major!) <- this was the showstopper
             * 3) sort max values + indices descending
             * 4) max value + max index is a position [0].
             *
             * Option 2 (recommended): 
             * Implement maxall via parallelized operator, handling indices. 
             * */

            // 1) find the longest dim: maxDimIdx 
            //uint maxDimIdx = 0;
            //long maxDimVal = -1;
            //for (uint i = 0; i < ndims; i++) {
            //    if (storage.S[i] > maxDimVal) {
            //        maxDimVal = storage.S[i];
            //        maxDimIdx = i;
            //    }
            //}
            //// 2) perform max(I:I) on the longest dim
            //Array<long> I = 0; 
            //Array<ulong> maxVals = max(A, I: I, dim: (int)maxDimIdx);

            //// 3) sort max values + indices descending
            //System.Diagnostics.Debug.Assert(I.S.IsSameShape(maxVals.S));
            //// ... not finished ...

            // following is the naive implementation (slow)
            using var _1 = ILNumerics.Core.Global.ReaderLock.Create(A, out var storage, releaseRetT: true);

            if (storage.S.NumberOfElements == 0) {
                // empty A 
                throw new ArgumentException("Unable to compute the maximum of an empty array.");
            }
            if (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor) {
                throw new ArgumentException($"Iteration order must be either {nameof(StorageOrders.ColumnMajor)} or {nameof(StorageOrders.RowMajor)}.");
            }
            var it = storage.S.Iterator(order).GetEnumerator();
            ulong* pA = (ulong*)storage.Handles[0].Pointer;
            long curI = -1, maxIdx = 0;
            ulong maxVal = 0;
            #if !IsInteger
                maxVal = storage.GetValue(0);
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (curVal  > maxVal) {
                        maxVal = curVal;
                        maxIdx = curI;
                    }
                }
#else
            if (ignoreNaN) {
                // Floating point: NaN the Matlab way (ignored if possible)
                // init with first non-NaN value
                while (it.MoveNext()) {
                    curI++;
                    if (!ulong.IsNaN(maxVal = pA[it.Current])) {
                        break;
                    }
                }
                if (curI < storage.S.NumberOfElements) {
                    // at least one non-NaN exists. It is stored in maxVal now.
                    System.Diagnostics.Debug.Assert(!ulong.IsNaN(maxVal));

                    while (it.MoveNext()) {
                        curI++;
                        var curVal = pA[it.Current];
                        if (curVal  > maxVal) { // NaNs compare: false
                            maxVal = curVal;
                            maxIdx = curI;
                        }
                    }
                } else {
                    // all NaNs
                    maxIdx = 0;
                    System.Diagnostics.Debug.Assert(ulong.IsNaN(maxVal));
                }
            } else {
                // Floating point: NaN the numpy way (precedence) 
                maxVal = storage.GetValue(0);  // may be NaN!
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (ulong.IsNaN(curVal)) {
                        maxVal = curVal;
                        maxIdx = curI;
                        break;
                    }
                    if (curVal  > maxVal) {
                        maxVal = curVal;
                        maxIdx = curI;
                    }
                }
            }
#endif
            if (!Equals(index, null)) {
                lock (index.SynchObj)
                    index.a = maxIdx;
            }
            return maxVal;
        }
       

        /// <summary>
        /// Computes the (flattened, row-major) sequential index and value of the element with the maximum value in <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the maximum for. This is not altered.</param>
        /// <param name="index">[Optional] index of the maximum value. Default: (null) the index is not returned.</param>
        /// <param name="order">[Optional] iteration order for the flattened sequential indices. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <param name="ignoreNaN">[Optional] determins how NaN values are handled for floating point element types. Ignored for integer elements. Defaul: (true) ignores NaNs.</param>
        /// <returns>Scalar array with the maximum value in A.</returns>
        /// <remarks><para>See <see cref="MathInternal.max(BaseArray{int}, OutArray{long}, int, bool)"/> for 
        /// getting the maximum values along a specific dimension. </para>
        /// <para>The functions 'max','min','maxall','minall' give all similar results between the framework APIs (ILNumericsV4/Matlab(R) / numpy)
        /// but show individual behavior regarding the handling of floating point NaN values and iteration order. The value of 'true' 
        /// for <paramref name="ignoreNaN"/> corresponds to the behavior of ILNumerics version 4/ Matlab(R). Only if 
        /// all elements in <paramref name="A"/> are <see cref="float.NaN"/> the value of the result will also be NaN. This is the default.</para>
        /// <para>The value of 'false' for <paramref name="ignoreNaN"/> gives the numpy behavior: NaN values take precedence over non-NaN values. If 
        /// one element in the set of values is NaN the result will also be NaN. This is the default behavior for such functions defined as 
        /// [numpy API] extension methods on the array classes.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{int}, OutArray{long}, int, bool)"/>
        internal static unsafe Array<int> maxall(ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> A,
            OutArray<long> index = null, StorageOrders order = StorageOrders.ColumnMajor, bool ignoreNaN = true) {


            /*
             * Algorithm option 1: 
             * 1) find the longest dim: maxDimIdx 
             * 2) perform max(I:I) on the longest dim
             * 2.1) modify indices to reflect their sequential indices (row major!) <- this was the showstopper
             * 3) sort max values + indices descending
             * 4) max value + max index is a position [0].
             *
             * Option 2 (recommended): 
             * Implement maxall via parallelized operator, handling indices. 
             * */

            // 1) find the longest dim: maxDimIdx 
            //uint maxDimIdx = 0;
            //long maxDimVal = -1;
            //for (uint i = 0; i < ndims; i++) {
            //    if (storage.S[i] > maxDimVal) {
            //        maxDimVal = storage.S[i];
            //        maxDimIdx = i;
            //    }
            //}
            //// 2) perform max(I:I) on the longest dim
            //Array<long> I = 0; 
            //Array<int> maxVals = max(A, I: I, dim: (int)maxDimIdx);

            //// 3) sort max values + indices descending
            //System.Diagnostics.Debug.Assert(I.S.IsSameShape(maxVals.S));
            //// ... not finished ...

            // following is the naive implementation (slow)
            using var _1 = ILNumerics.Core.Global.ReaderLock.Create(A, out var storage, releaseRetT: true);

            if (storage.S.NumberOfElements == 0) {
                // empty A 
                throw new ArgumentException("Unable to compute the maximum of an empty array.");
            }
            if (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor) {
                throw new ArgumentException($"Iteration order must be either {nameof(StorageOrders.ColumnMajor)} or {nameof(StorageOrders.RowMajor)}.");
            }
            var it = storage.S.Iterator(order).GetEnumerator();
            int* pA = (int*)storage.Handles[0].Pointer;
            long curI = -1, maxIdx = 0;
            int maxVal = 0;
            #if !IsInteger
                maxVal = storage.GetValue(0);
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (curVal  > maxVal) {
                        maxVal = curVal;
                        maxIdx = curI;
                    }
                }
#else
            if (ignoreNaN) {
                // Floating point: NaN the Matlab way (ignored if possible)
                // init with first non-NaN value
                while (it.MoveNext()) {
                    curI++;
                    if (!int.IsNaN(maxVal = pA[it.Current])) {
                        break;
                    }
                }
                if (curI < storage.S.NumberOfElements) {
                    // at least one non-NaN exists. It is stored in maxVal now.
                    System.Diagnostics.Debug.Assert(!int.IsNaN(maxVal));

                    while (it.MoveNext()) {
                        curI++;
                        var curVal = pA[it.Current];
                        if (curVal  > maxVal) { // NaNs compare: false
                            maxVal = curVal;
                            maxIdx = curI;
                        }
                    }
                } else {
                    // all NaNs
                    maxIdx = 0;
                    System.Diagnostics.Debug.Assert(int.IsNaN(maxVal));
                }
            } else {
                // Floating point: NaN the numpy way (precedence) 
                maxVal = storage.GetValue(0);  // may be NaN!
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (int.IsNaN(curVal)) {
                        maxVal = curVal;
                        maxIdx = curI;
                        break;
                    }
                    if (curVal  > maxVal) {
                        maxVal = curVal;
                        maxIdx = curI;
                    }
                }
            }
#endif
            if (!Equals(index, null)) {
                lock (index.SynchObj)
                    index.a = maxIdx;
            }
            return maxVal;
        }
       

        /// <summary>
        /// Computes the (flattened, row-major) sequential index and value of the element with the maximum value in <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the maximum for. This is not altered.</param>
        /// <param name="index">[Optional] index of the maximum value. Default: (null) the index is not returned.</param>
        /// <param name="order">[Optional] iteration order for the flattened sequential indices. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <param name="ignoreNaN">[Optional] determins how NaN values are handled for floating point element types. Ignored for integer elements. Defaul: (true) ignores NaNs.</param>
        /// <returns>Scalar array with the maximum value in A.</returns>
        /// <remarks><para>See <see cref="MathInternal.max(BaseArray{uint}, OutArray{long}, int, bool)"/> for 
        /// getting the maximum values along a specific dimension. </para>
        /// <para>The functions 'max','min','maxall','minall' give all similar results between the framework APIs (ILNumericsV4/Matlab(R) / numpy)
        /// but show individual behavior regarding the handling of floating point NaN values and iteration order. The value of 'true' 
        /// for <paramref name="ignoreNaN"/> corresponds to the behavior of ILNumerics version 4/ Matlab(R). Only if 
        /// all elements in <paramref name="A"/> are <see cref="float.NaN"/> the value of the result will also be NaN. This is the default.</para>
        /// <para>The value of 'false' for <paramref name="ignoreNaN"/> gives the numpy behavior: NaN values take precedence over non-NaN values. If 
        /// one element in the set of values is NaN the result will also be NaN. This is the default behavior for such functions defined as 
        /// [numpy API] extension methods on the array classes.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{uint}, OutArray{long}, int, bool)"/>
        internal static unsafe Array<uint> maxall(ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> A,
            OutArray<long> index = null, StorageOrders order = StorageOrders.ColumnMajor, bool ignoreNaN = true) {


            /*
             * Algorithm option 1: 
             * 1) find the longest dim: maxDimIdx 
             * 2) perform max(I:I) on the longest dim
             * 2.1) modify indices to reflect their sequential indices (row major!) <- this was the showstopper
             * 3) sort max values + indices descending
             * 4) max value + max index is a position [0].
             *
             * Option 2 (recommended): 
             * Implement maxall via parallelized operator, handling indices. 
             * */

            // 1) find the longest dim: maxDimIdx 
            //uint maxDimIdx = 0;
            //long maxDimVal = -1;
            //for (uint i = 0; i < ndims; i++) {
            //    if (storage.S[i] > maxDimVal) {
            //        maxDimVal = storage.S[i];
            //        maxDimIdx = i;
            //    }
            //}
            //// 2) perform max(I:I) on the longest dim
            //Array<long> I = 0; 
            //Array<uint> maxVals = max(A, I: I, dim: (int)maxDimIdx);

            //// 3) sort max values + indices descending
            //System.Diagnostics.Debug.Assert(I.S.IsSameShape(maxVals.S));
            //// ... not finished ...

            // following is the naive implementation (slow)
            using var _1 = ILNumerics.Core.Global.ReaderLock.Create(A, out var storage, releaseRetT: true);

            if (storage.S.NumberOfElements == 0) {
                // empty A 
                throw new ArgumentException("Unable to compute the maximum of an empty array.");
            }
            if (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor) {
                throw new ArgumentException($"Iteration order must be either {nameof(StorageOrders.ColumnMajor)} or {nameof(StorageOrders.RowMajor)}.");
            }
            var it = storage.S.Iterator(order).GetEnumerator();
            uint* pA = (uint*)storage.Handles[0].Pointer;
            long curI = -1, maxIdx = 0;
            uint maxVal = 0;
            #if !IsInteger
                maxVal = storage.GetValue(0);
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (curVal  > maxVal) {
                        maxVal = curVal;
                        maxIdx = curI;
                    }
                }
#else
            if (ignoreNaN) {
                // Floating point: NaN the Matlab way (ignored if possible)
                // init with first non-NaN value
                while (it.MoveNext()) {
                    curI++;
                    if (!uint.IsNaN(maxVal = pA[it.Current])) {
                        break;
                    }
                }
                if (curI < storage.S.NumberOfElements) {
                    // at least one non-NaN exists. It is stored in maxVal now.
                    System.Diagnostics.Debug.Assert(!uint.IsNaN(maxVal));

                    while (it.MoveNext()) {
                        curI++;
                        var curVal = pA[it.Current];
                        if (curVal  > maxVal) { // NaNs compare: false
                            maxVal = curVal;
                            maxIdx = curI;
                        }
                    }
                } else {
                    // all NaNs
                    maxIdx = 0;
                    System.Diagnostics.Debug.Assert(uint.IsNaN(maxVal));
                }
            } else {
                // Floating point: NaN the numpy way (precedence) 
                maxVal = storage.GetValue(0);  // may be NaN!
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (uint.IsNaN(curVal)) {
                        maxVal = curVal;
                        maxIdx = curI;
                        break;
                    }
                    if (curVal  > maxVal) {
                        maxVal = curVal;
                        maxIdx = curI;
                    }
                }
            }
#endif
            if (!Equals(index, null)) {
                lock (index.SynchObj)
                    index.a = maxIdx;
            }
            return maxVal;
        }
       

        /// <summary>
        /// Computes the (flattened, row-major) sequential index and value of the element with the maximum value in <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the maximum for. This is not altered.</param>
        /// <param name="index">[Optional] index of the maximum value. Default: (null) the index is not returned.</param>
        /// <param name="order">[Optional] iteration order for the flattened sequential indices. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <param name="ignoreNaN">[Optional] determins how NaN values are handled for floating point element types. Ignored for integer elements. Defaul: (true) ignores NaNs.</param>
        /// <returns>Scalar array with the maximum value in A.</returns>
        /// <remarks><para>See <see cref="MathInternal.max(BaseArray{short}, OutArray{long}, int, bool)"/> for 
        /// getting the maximum values along a specific dimension. </para>
        /// <para>The functions 'max','min','maxall','minall' give all similar results between the framework APIs (ILNumericsV4/Matlab(R) / numpy)
        /// but show individual behavior regarding the handling of floating point NaN values and iteration order. The value of 'true' 
        /// for <paramref name="ignoreNaN"/> corresponds to the behavior of ILNumerics version 4/ Matlab(R). Only if 
        /// all elements in <paramref name="A"/> are <see cref="float.NaN"/> the value of the result will also be NaN. This is the default.</para>
        /// <para>The value of 'false' for <paramref name="ignoreNaN"/> gives the numpy behavior: NaN values take precedence over non-NaN values. If 
        /// one element in the set of values is NaN the result will also be NaN. This is the default behavior for such functions defined as 
        /// [numpy API] extension methods on the array classes.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{short}, OutArray{long}, int, bool)"/>
        internal static unsafe Array<short> maxall(ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> A,
            OutArray<long> index = null, StorageOrders order = StorageOrders.ColumnMajor, bool ignoreNaN = true) {


            /*
             * Algorithm option 1: 
             * 1) find the longest dim: maxDimIdx 
             * 2) perform max(I:I) on the longest dim
             * 2.1) modify indices to reflect their sequential indices (row major!) <- this was the showstopper
             * 3) sort max values + indices descending
             * 4) max value + max index is a position [0].
             *
             * Option 2 (recommended): 
             * Implement maxall via parallelized operator, handling indices. 
             * */

            // 1) find the longest dim: maxDimIdx 
            //uint maxDimIdx = 0;
            //long maxDimVal = -1;
            //for (uint i = 0; i < ndims; i++) {
            //    if (storage.S[i] > maxDimVal) {
            //        maxDimVal = storage.S[i];
            //        maxDimIdx = i;
            //    }
            //}
            //// 2) perform max(I:I) on the longest dim
            //Array<long> I = 0; 
            //Array<short> maxVals = max(A, I: I, dim: (int)maxDimIdx);

            //// 3) sort max values + indices descending
            //System.Diagnostics.Debug.Assert(I.S.IsSameShape(maxVals.S));
            //// ... not finished ...

            // following is the naive implementation (slow)
            using var _1 = ILNumerics.Core.Global.ReaderLock.Create(A, out var storage, releaseRetT: true);

            if (storage.S.NumberOfElements == 0) {
                // empty A 
                throw new ArgumentException("Unable to compute the maximum of an empty array.");
            }
            if (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor) {
                throw new ArgumentException($"Iteration order must be either {nameof(StorageOrders.ColumnMajor)} or {nameof(StorageOrders.RowMajor)}.");
            }
            var it = storage.S.Iterator(order).GetEnumerator();
            short* pA = (short*)storage.Handles[0].Pointer;
            long curI = -1, maxIdx = 0;
            short maxVal = 0;
            #if !IsInteger
                maxVal = storage.GetValue(0);
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (curVal  > maxVal) {
                        maxVal = curVal;
                        maxIdx = curI;
                    }
                }
#else
            if (ignoreNaN) {
                // Floating point: NaN the Matlab way (ignored if possible)
                // init with first non-NaN value
                while (it.MoveNext()) {
                    curI++;
                    if (!short.IsNaN(maxVal = pA[it.Current])) {
                        break;
                    }
                }
                if (curI < storage.S.NumberOfElements) {
                    // at least one non-NaN exists. It is stored in maxVal now.
                    System.Diagnostics.Debug.Assert(!short.IsNaN(maxVal));

                    while (it.MoveNext()) {
                        curI++;
                        var curVal = pA[it.Current];
                        if (curVal  > maxVal) { // NaNs compare: false
                            maxVal = curVal;
                            maxIdx = curI;
                        }
                    }
                } else {
                    // all NaNs
                    maxIdx = 0;
                    System.Diagnostics.Debug.Assert(short.IsNaN(maxVal));
                }
            } else {
                // Floating point: NaN the numpy way (precedence) 
                maxVal = storage.GetValue(0);  // may be NaN!
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (short.IsNaN(curVal)) {
                        maxVal = curVal;
                        maxIdx = curI;
                        break;
                    }
                    if (curVal  > maxVal) {
                        maxVal = curVal;
                        maxIdx = curI;
                    }
                }
            }
#endif
            if (!Equals(index, null)) {
                lock (index.SynchObj)
                    index.a = maxIdx;
            }
            return maxVal;
        }
       

        /// <summary>
        /// Computes the (flattened, row-major) sequential index and value of the element with the maximum value in <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the maximum for. This is not altered.</param>
        /// <param name="index">[Optional] index of the maximum value. Default: (null) the index is not returned.</param>
        /// <param name="order">[Optional] iteration order for the flattened sequential indices. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <param name="ignoreNaN">[Optional] determins how NaN values are handled for floating point element types. Ignored for integer elements. Defaul: (true) ignores NaNs.</param>
        /// <returns>Scalar array with the maximum value in A.</returns>
        /// <remarks><para>See <see cref="MathInternal.max(BaseArray{ushort}, OutArray{long}, int, bool)"/> for 
        /// getting the maximum values along a specific dimension. </para>
        /// <para>The functions 'max','min','maxall','minall' give all similar results between the framework APIs (ILNumericsV4/Matlab(R) / numpy)
        /// but show individual behavior regarding the handling of floating point NaN values and iteration order. The value of 'true' 
        /// for <paramref name="ignoreNaN"/> corresponds to the behavior of ILNumerics version 4/ Matlab(R). Only if 
        /// all elements in <paramref name="A"/> are <see cref="float.NaN"/> the value of the result will also be NaN. This is the default.</para>
        /// <para>The value of 'false' for <paramref name="ignoreNaN"/> gives the numpy behavior: NaN values take precedence over non-NaN values. If 
        /// one element in the set of values is NaN the result will also be NaN. This is the default behavior for such functions defined as 
        /// [numpy API] extension methods on the array classes.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{ushort}, OutArray{long}, int, bool)"/>
        internal static unsafe Array<ushort> maxall(ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> A,
            OutArray<long> index = null, StorageOrders order = StorageOrders.ColumnMajor, bool ignoreNaN = true) {


            /*
             * Algorithm option 1: 
             * 1) find the longest dim: maxDimIdx 
             * 2) perform max(I:I) on the longest dim
             * 2.1) modify indices to reflect their sequential indices (row major!) <- this was the showstopper
             * 3) sort max values + indices descending
             * 4) max value + max index is a position [0].
             *
             * Option 2 (recommended): 
             * Implement maxall via parallelized operator, handling indices. 
             * */

            // 1) find the longest dim: maxDimIdx 
            //uint maxDimIdx = 0;
            //long maxDimVal = -1;
            //for (uint i = 0; i < ndims; i++) {
            //    if (storage.S[i] > maxDimVal) {
            //        maxDimVal = storage.S[i];
            //        maxDimIdx = i;
            //    }
            //}
            //// 2) perform max(I:I) on the longest dim
            //Array<long> I = 0; 
            //Array<ushort> maxVals = max(A, I: I, dim: (int)maxDimIdx);

            //// 3) sort max values + indices descending
            //System.Diagnostics.Debug.Assert(I.S.IsSameShape(maxVals.S));
            //// ... not finished ...

            // following is the naive implementation (slow)
            using var _1 = ILNumerics.Core.Global.ReaderLock.Create(A, out var storage, releaseRetT: true);

            if (storage.S.NumberOfElements == 0) {
                // empty A 
                throw new ArgumentException("Unable to compute the maximum of an empty array.");
            }
            if (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor) {
                throw new ArgumentException($"Iteration order must be either {nameof(StorageOrders.ColumnMajor)} or {nameof(StorageOrders.RowMajor)}.");
            }
            var it = storage.S.Iterator(order).GetEnumerator();
            ushort* pA = (ushort*)storage.Handles[0].Pointer;
            long curI = -1, maxIdx = 0;
            ushort maxVal = 0;
            #if !IsInteger
                maxVal = storage.GetValue(0);
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (curVal  > maxVal) {
                        maxVal = curVal;
                        maxIdx = curI;
                    }
                }
#else
            if (ignoreNaN) {
                // Floating point: NaN the Matlab way (ignored if possible)
                // init with first non-NaN value
                while (it.MoveNext()) {
                    curI++;
                    if (!ushort.IsNaN(maxVal = pA[it.Current])) {
                        break;
                    }
                }
                if (curI < storage.S.NumberOfElements) {
                    // at least one non-NaN exists. It is stored in maxVal now.
                    System.Diagnostics.Debug.Assert(!ushort.IsNaN(maxVal));

                    while (it.MoveNext()) {
                        curI++;
                        var curVal = pA[it.Current];
                        if (curVal  > maxVal) { // NaNs compare: false
                            maxVal = curVal;
                            maxIdx = curI;
                        }
                    }
                } else {
                    // all NaNs
                    maxIdx = 0;
                    System.Diagnostics.Debug.Assert(ushort.IsNaN(maxVal));
                }
            } else {
                // Floating point: NaN the numpy way (precedence) 
                maxVal = storage.GetValue(0);  // may be NaN!
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (ushort.IsNaN(curVal)) {
                        maxVal = curVal;
                        maxIdx = curI;
                        break;
                    }
                    if (curVal  > maxVal) {
                        maxVal = curVal;
                        maxIdx = curI;
                    }
                }
            }
#endif
            if (!Equals(index, null)) {
                lock (index.SynchObj)
                    index.a = maxIdx;
            }
            return maxVal;
        }
       

        /// <summary>
        /// Computes the (flattened, row-major) sequential index and value of the element with the maximum value in <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the maximum for. This is not altered.</param>
        /// <param name="index">[Optional] index of the maximum value. Default: (null) the index is not returned.</param>
        /// <param name="order">[Optional] iteration order for the flattened sequential indices. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <param name="ignoreNaN">[Optional] determins how NaN values are handled for floating point element types. Ignored for integer elements. Defaul: (true) ignores NaNs.</param>
        /// <returns>Scalar array with the maximum value in A.</returns>
        /// <remarks><para>See <see cref="MathInternal.max(BaseArray{sbyte}, OutArray{long}, int, bool)"/> for 
        /// getting the maximum values along a specific dimension. </para>
        /// <para>The functions 'max','min','maxall','minall' give all similar results between the framework APIs (ILNumericsV4/Matlab(R) / numpy)
        /// but show individual behavior regarding the handling of floating point NaN values and iteration order. The value of 'true' 
        /// for <paramref name="ignoreNaN"/> corresponds to the behavior of ILNumerics version 4/ Matlab(R). Only if 
        /// all elements in <paramref name="A"/> are <see cref="float.NaN"/> the value of the result will also be NaN. This is the default.</para>
        /// <para>The value of 'false' for <paramref name="ignoreNaN"/> gives the numpy behavior: NaN values take precedence over non-NaN values. If 
        /// one element in the set of values is NaN the result will also be NaN. This is the default behavior for such functions defined as 
        /// [numpy API] extension methods on the array classes.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{sbyte}, OutArray{long}, int, bool)"/>
        internal static unsafe Array<sbyte> maxall(ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> A,
            OutArray<long> index = null, StorageOrders order = StorageOrders.ColumnMajor, bool ignoreNaN = true) {


            /*
             * Algorithm option 1: 
             * 1) find the longest dim: maxDimIdx 
             * 2) perform max(I:I) on the longest dim
             * 2.1) modify indices to reflect their sequential indices (row major!) <- this was the showstopper
             * 3) sort max values + indices descending
             * 4) max value + max index is a position [0].
             *
             * Option 2 (recommended): 
             * Implement maxall via parallelized operator, handling indices. 
             * */

            // 1) find the longest dim: maxDimIdx 
            //uint maxDimIdx = 0;
            //long maxDimVal = -1;
            //for (uint i = 0; i < ndims; i++) {
            //    if (storage.S[i] > maxDimVal) {
            //        maxDimVal = storage.S[i];
            //        maxDimIdx = i;
            //    }
            //}
            //// 2) perform max(I:I) on the longest dim
            //Array<long> I = 0; 
            //Array<sbyte> maxVals = max(A, I: I, dim: (int)maxDimIdx);

            //// 3) sort max values + indices descending
            //System.Diagnostics.Debug.Assert(I.S.IsSameShape(maxVals.S));
            //// ... not finished ...

            // following is the naive implementation (slow)
            using var _1 = ILNumerics.Core.Global.ReaderLock.Create(A, out var storage, releaseRetT: true);

            if (storage.S.NumberOfElements == 0) {
                // empty A 
                throw new ArgumentException("Unable to compute the maximum of an empty array.");
            }
            if (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor) {
                throw new ArgumentException($"Iteration order must be either {nameof(StorageOrders.ColumnMajor)} or {nameof(StorageOrders.RowMajor)}.");
            }
            var it = storage.S.Iterator(order).GetEnumerator();
            sbyte* pA = (sbyte*)storage.Handles[0].Pointer;
            long curI = -1, maxIdx = 0;
            sbyte maxVal = 0;
            #if !IsInteger
                maxVal = storage.GetValue(0);
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (curVal  > maxVal) {
                        maxVal = curVal;
                        maxIdx = curI;
                    }
                }
#else
            if (ignoreNaN) {
                // Floating point: NaN the Matlab way (ignored if possible)
                // init with first non-NaN value
                while (it.MoveNext()) {
                    curI++;
                    if (!sbyte.IsNaN(maxVal = pA[it.Current])) {
                        break;
                    }
                }
                if (curI < storage.S.NumberOfElements) {
                    // at least one non-NaN exists. It is stored in maxVal now.
                    System.Diagnostics.Debug.Assert(!sbyte.IsNaN(maxVal));

                    while (it.MoveNext()) {
                        curI++;
                        var curVal = pA[it.Current];
                        if (curVal  > maxVal) { // NaNs compare: false
                            maxVal = curVal;
                            maxIdx = curI;
                        }
                    }
                } else {
                    // all NaNs
                    maxIdx = 0;
                    System.Diagnostics.Debug.Assert(sbyte.IsNaN(maxVal));
                }
            } else {
                // Floating point: NaN the numpy way (precedence) 
                maxVal = storage.GetValue(0);  // may be NaN!
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (sbyte.IsNaN(curVal)) {
                        maxVal = curVal;
                        maxIdx = curI;
                        break;
                    }
                    if (curVal  > maxVal) {
                        maxVal = curVal;
                        maxIdx = curI;
                    }
                }
            }
#endif
            if (!Equals(index, null)) {
                lock (index.SynchObj)
                    index.a = maxIdx;
            }
            return maxVal;
        }
       

        /// <summary>
        /// Computes the (flattened, row-major) sequential index and value of the element with the maximum value in <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the maximum for. This is not altered.</param>
        /// <param name="index">[Optional] index of the maximum value. Default: (null) the index is not returned.</param>
        /// <param name="order">[Optional] iteration order for the flattened sequential indices. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <param name="ignoreNaN">[Optional] determins how NaN values are handled for floating point element types. Ignored for integer elements. Defaul: (true) ignores NaNs.</param>
        /// <returns>Scalar array with the maximum value in A.</returns>
        /// <remarks><para>See <see cref="MathInternal.max(BaseArray{byte}, OutArray{long}, int, bool)"/> for 
        /// getting the maximum values along a specific dimension. </para>
        /// <para>The functions 'max','min','maxall','minall' give all similar results between the framework APIs (ILNumericsV4/Matlab(R) / numpy)
        /// but show individual behavior regarding the handling of floating point NaN values and iteration order. The value of 'true' 
        /// for <paramref name="ignoreNaN"/> corresponds to the behavior of ILNumerics version 4/ Matlab(R). Only if 
        /// all elements in <paramref name="A"/> are <see cref="float.NaN"/> the value of the result will also be NaN. This is the default.</para>
        /// <para>The value of 'false' for <paramref name="ignoreNaN"/> gives the numpy behavior: NaN values take precedence over non-NaN values. If 
        /// one element in the set of values is NaN the result will also be NaN. This is the default behavior for such functions defined as 
        /// [numpy API] extension methods on the array classes.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.max(BaseArray{byte}, OutArray{long}, int, bool)"/>
        internal static unsafe Array<byte> maxall(ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> A,
            OutArray<long> index = null, StorageOrders order = StorageOrders.ColumnMajor, bool ignoreNaN = true) {


            /*
             * Algorithm option 1: 
             * 1) find the longest dim: maxDimIdx 
             * 2) perform max(I:I) on the longest dim
             * 2.1) modify indices to reflect their sequential indices (row major!) <- this was the showstopper
             * 3) sort max values + indices descending
             * 4) max value + max index is a position [0].
             *
             * Option 2 (recommended): 
             * Implement maxall via parallelized operator, handling indices. 
             * */

            // 1) find the longest dim: maxDimIdx 
            //uint maxDimIdx = 0;
            //long maxDimVal = -1;
            //for (uint i = 0; i < ndims; i++) {
            //    if (storage.S[i] > maxDimVal) {
            //        maxDimVal = storage.S[i];
            //        maxDimIdx = i;
            //    }
            //}
            //// 2) perform max(I:I) on the longest dim
            //Array<long> I = 0; 
            //Array<byte> maxVals = max(A, I: I, dim: (int)maxDimIdx);

            //// 3) sort max values + indices descending
            //System.Diagnostics.Debug.Assert(I.S.IsSameShape(maxVals.S));
            //// ... not finished ...

            // following is the naive implementation (slow)
            using var _1 = ILNumerics.Core.Global.ReaderLock.Create(A, out var storage, releaseRetT: true);

            if (storage.S.NumberOfElements == 0) {
                // empty A 
                throw new ArgumentException("Unable to compute the maximum of an empty array.");
            }
            if (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor) {
                throw new ArgumentException($"Iteration order must be either {nameof(StorageOrders.ColumnMajor)} or {nameof(StorageOrders.RowMajor)}.");
            }
            var it = storage.S.Iterator(order).GetEnumerator();
            byte* pA = (byte*)storage.Handles[0].Pointer;
            long curI = -1, maxIdx = 0;
            byte maxVal = 0;
            #if !IsInteger
                maxVal = storage.GetValue(0);
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (curVal  > maxVal) {
                        maxVal = curVal;
                        maxIdx = curI;
                    }
                }
#else
            if (ignoreNaN) {
                // Floating point: NaN the Matlab way (ignored if possible)
                // init with first non-NaN value
                while (it.MoveNext()) {
                    curI++;
                    if (!byte.IsNaN(maxVal = pA[it.Current])) {
                        break;
                    }
                }
                if (curI < storage.S.NumberOfElements) {
                    // at least one non-NaN exists. It is stored in maxVal now.
                    System.Diagnostics.Debug.Assert(!byte.IsNaN(maxVal));

                    while (it.MoveNext()) {
                        curI++;
                        var curVal = pA[it.Current];
                        if (curVal  > maxVal) { // NaNs compare: false
                            maxVal = curVal;
                            maxIdx = curI;
                        }
                    }
                } else {
                    // all NaNs
                    maxIdx = 0;
                    System.Diagnostics.Debug.Assert(byte.IsNaN(maxVal));
                }
            } else {
                // Floating point: NaN the numpy way (precedence) 
                maxVal = storage.GetValue(0);  // may be NaN!
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (byte.IsNaN(curVal)) {
                        maxVal = curVal;
                        maxIdx = curI;
                        break;
                    }
                    if (curVal  > maxVal) {
                        maxVal = curVal;
                        maxIdx = curI;
                    }
                }
            }
#endif
            if (!Equals(index, null)) {
                lock (index.SynchObj)
                    index.a = maxIdx;
            }
            return maxVal;
        }

#endregion HYCALPER AUTO GENERATED CODE

    }
}
