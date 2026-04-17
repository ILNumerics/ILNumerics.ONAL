//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////
using ILNumerics.Core.Arrays;
using ILNumerics.Core.Functions.Builtin;
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

        #region HYCALPER LOOPSTART ACCUMULATING_ALL_IND_TEMPLATE@Functions\MiscFunctions\maxall.cs
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
        <destination>double</destination>
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
        <destination>Double</destination>
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
        <destination><![CDATA[#if IsInteger]]></destination>
        </type>
        <type>
        <source locate="after" endmark=" _():">
            operation
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
        <source locate="here" endmark=" _():">
            max
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
        </hycalper>
        */
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        /// <summary>
        /// Computes the (flattened, row-major) sequential index and value of the element with the minimum value in <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the minimum for. This is not altered.</param>
        /// <param name="index">[Optional] index of the minimum value. Default: (null) the index is not returned.</param>
        /// <param name="order">[Optional] iteration order for the flattened sequential indices. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <param name="ignoreNaN">[Optional] determins how NaN values are handled for floating point element types. Ignored for integer elements. Defaul: (true) ignores NaNs.</param>
        /// <returns>Scalar array with the minimum value in A.</returns>
        /// <remarks><para>See <see cref="MathInternal.min(BaseArray{double}, OutArray{long}, int, bool)"/> for 
        /// getting the minimum values along a specific dimension. </para>
        /// <para>The functions 'min','min','minall','minall' give all similar results between the framework APIs (ILNumericsV4/Matlab(R) / numpy)
        /// but show individual behavior regarding the handling of floating point NaN values and iteration order. The value of 'true' 
        /// for <paramref name="ignoreNaN"/> corresponds to the behavior of ILNumerics version 4/ Matlab(R). Only if 
        /// all elements in <paramref name="A"/> are <see cref="float.NaN"/> the value of the result will also be NaN. This is the default.</para>
        /// <para>The value of 'false' for <paramref name="ignoreNaN"/> gives the numpy behavior: NaN values take precedence over non-NaN values. If 
        /// one element in the set of values is NaN the result will also be NaN. This is the default behavior for such functions defined as 
        /// [numpy API] extension methods on the array classes.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{double}, OutArray{long}, int, bool)"/>
        internal static unsafe Array<double> minall(ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> A,
            OutArray<long> index = null, StorageOrders order = StorageOrders.ColumnMajor, bool ignoreNaN = true) {


            /*
             * Algorithm option 1: 
             * 1) find the longest dim: minDimIdx 
             * 2) perform min(I:I) on the longest dim
             * 2.1) modify indices to reflect their sequential indices (row major!) <- this was the showstopper
             * 3) sort min values + indices descending
             * 4) min value + min index is a position [0].
             *
             * Option 2 (recommended): 
             * Implement minall via parallelized operator, handling indices. 
             * */

            // 1) find the longest dim: minDimIdx 
            //uint minDimIdx = 0;
            //long minDimVal = -1;
            //for (uint i = 0; i < ndims; i++) {
            //    if (storage.S[i] > minDimVal) {
            //        minDimVal = storage.S[i];
            //        minDimIdx = i;
            //    }
            //}
            //// 2) perform min(I:I) on the longest dim
            //Array<long> I = 0; 
            //Array<double> minVals = min(A, I: I, dim: (int)minDimIdx);

            //// 3) sort min values + indices descending
            //System.Diagnostics.Debug.Assert(I.S.IsSameShape(minVals.S));
            //// ... not finished ...

            // following is the naive implementation (slow)
            using var _1 = ILNumerics.Core.Global.ReaderLock.Create(A, out var storage, releaseRetT: true);

            if (storage.S.NumberOfElements == 0) {
                // empty A 
                throw new ArgumentException("Unable to compute the minimum of an empty array.");
            }
            if (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor) {
                throw new ArgumentException($"Iteration order must be either {nameof(StorageOrders.ColumnMajor)} or {nameof(StorageOrders.RowMajor)}.");
            }
            var it = storage.S.Iterator(order).GetEnumerator();
            double* pA = (double*)storage.Handles[0].Pointer;
            long curI = -1, minIdx = 0;
            double minVal = 0;
            #if IsInteger
                minVal = storage.GetValue(0);
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (curVal  < minVal) {
                        minVal = curVal;
                        minIdx = curI;
                    }
                }
#else
            if (ignoreNaN) {
                // Floating point: NaN the Matlab way (ignored if possible)
                // init with first non-NaN value
                while (it.MoveNext()) {
                    curI++;
                    if (!double.IsNaN(minVal = pA[it.Current])) {
                        break;
                    }
                }
                if (curI < storage.S.NumberOfElements) {
                    // at least one non-NaN exists. It is stored in minVal now.
                    System.Diagnostics.Debug.Assert(!double.IsNaN(minVal));

                    while (it.MoveNext()) {
                        curI++;
                        var curVal = pA[it.Current];
                        if (curVal  < minVal) { // NaNs compare: false
                            minVal = curVal;
                            minIdx = curI;
                        }
                    }
                } else {
                    // all NaNs
                    minIdx = 0;
                    System.Diagnostics.Debug.Assert(double.IsNaN(minVal));
                }
            } else {
                // Floating point: NaN the numpy way (precedence) 
                minVal = storage.GetValue(0);  // may be NaN!
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (double.IsNaN(curVal)) {
                        minVal = curVal;
                        minIdx = curI;
                        break;
                    }
                    if (curVal  < minVal) {
                        minVal = curVal;
                        minIdx = curI;
                    }
                }
            }
#endif
            if (!Equals(index, null)) {
                lock (index.SynchObj)
                    index.a = minIdx;
            }
            return minVal;
        }
       

        /// <summary>
        /// Computes the (flattened, row-major) sequential index and value of the element with the minimum value in <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the minimum for. This is not altered.</param>
        /// <param name="index">[Optional] index of the minimum value. Default: (null) the index is not returned.</param>
        /// <param name="order">[Optional] iteration order for the flattened sequential indices. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <param name="ignoreNaN">[Optional] determins how NaN values are handled for floating point element types. Ignored for integer elements. Defaul: (true) ignores NaNs.</param>
        /// <returns>Scalar array with the minimum value in A.</returns>
        /// <remarks><para>See <see cref="MathInternal.min(BaseArray{fcomplex}, OutArray{long}, int, bool)"/> for 
        /// getting the minimum values along a specific dimension. </para>
        /// <para>The functions 'min','min','minall','minall' give all similar results between the framework APIs (ILNumericsV4/Matlab(R) / numpy)
        /// but show individual behavior regarding the handling of floating point NaN values and iteration order. The value of 'true' 
        /// for <paramref name="ignoreNaN"/> corresponds to the behavior of ILNumerics version 4/ Matlab(R). Only if 
        /// all elements in <paramref name="A"/> are <see cref="float.NaN"/> the value of the result will also be NaN. This is the default.</para>
        /// <para>The value of 'false' for <paramref name="ignoreNaN"/> gives the numpy behavior: NaN values take precedence over non-NaN values. If 
        /// one element in the set of values is NaN the result will also be NaN. This is the default behavior for such functions defined as 
        /// [numpy API] extension methods on the array classes.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{fcomplex}, OutArray{long}, int, bool)"/>
        internal static unsafe Array<fcomplex> minall(ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> A,
            OutArray<long> index = null, StorageOrders order = StorageOrders.ColumnMajor, bool ignoreNaN = true) {


            /*
             * Algorithm option 1: 
             * 1) find the longest dim: minDimIdx 
             * 2) perform min(I:I) on the longest dim
             * 2.1) modify indices to reflect their sequential indices (row major!) <- this was the showstopper
             * 3) sort min values + indices descending
             * 4) min value + min index is a position [0].
             *
             * Option 2 (recommended): 
             * Implement minall via parallelized operator, handling indices. 
             * */

            // 1) find the longest dim: minDimIdx 
            //uint minDimIdx = 0;
            //long minDimVal = -1;
            //for (uint i = 0; i < ndims; i++) {
            //    if (storage.S[i] > minDimVal) {
            //        minDimVal = storage.S[i];
            //        minDimIdx = i;
            //    }
            //}
            //// 2) perform min(I:I) on the longest dim
            //Array<long> I = 0; 
            //Array<fcomplex> minVals = min(A, I: I, dim: (int)minDimIdx);

            //// 3) sort min values + indices descending
            //System.Diagnostics.Debug.Assert(I.S.IsSameShape(minVals.S));
            //// ... not finished ...

            // following is the naive implementation (slow)
            using var _1 = ILNumerics.Core.Global.ReaderLock.Create(A, out var storage, releaseRetT: true);

            if (storage.S.NumberOfElements == 0) {
                // empty A 
                throw new ArgumentException("Unable to compute the minimum of an empty array.");
            }
            if (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor) {
                throw new ArgumentException($"Iteration order must be either {nameof(StorageOrders.ColumnMajor)} or {nameof(StorageOrders.RowMajor)}.");
            }
            var it = storage.S.Iterator(order).GetEnumerator();
            fcomplex* pA = (fcomplex*)storage.Handles[0].Pointer;
            long curI = -1, minIdx = 0;
            fcomplex minVal = 0;
            #if IsInteger
                minVal = storage.GetValue(0);
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (curVal  < minVal) {
                        minVal = curVal;
                        minIdx = curI;
                    }
                }
#else
            if (ignoreNaN) {
                // Floating point: NaN the Matlab way (ignored if possible)
                // init with first non-NaN value
                while (it.MoveNext()) {
                    curI++;
                    if (!fcomplex.IsNaN(minVal = pA[it.Current])) {
                        break;
                    }
                }
                if (curI < storage.S.NumberOfElements) {
                    // at least one non-NaN exists. It is stored in minVal now.
                    System.Diagnostics.Debug.Assert(!fcomplex.IsNaN(minVal));

                    while (it.MoveNext()) {
                        curI++;
                        var curVal = pA[it.Current];
                        if (curVal  < minVal) { // NaNs compare: false
                            minVal = curVal;
                            minIdx = curI;
                        }
                    }
                } else {
                    // all NaNs
                    minIdx = 0;
                    System.Diagnostics.Debug.Assert(fcomplex.IsNaN(minVal));
                }
            } else {
                // Floating point: NaN the numpy way (precedence) 
                minVal = storage.GetValue(0);  // may be NaN!
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (fcomplex.IsNaN(curVal)) {
                        minVal = curVal;
                        minIdx = curI;
                        break;
                    }
                    if (curVal  < minVal) {
                        minVal = curVal;
                        minIdx = curI;
                    }
                }
            }
#endif
            if (!Equals(index, null)) {
                lock (index.SynchObj)
                    index.a = minIdx;
            }
            return minVal;
        }
       

        /// <summary>
        /// Computes the (flattened, row-major) sequential index and value of the element with the minimum value in <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the minimum for. This is not altered.</param>
        /// <param name="index">[Optional] index of the minimum value. Default: (null) the index is not returned.</param>
        /// <param name="order">[Optional] iteration order for the flattened sequential indices. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <param name="ignoreNaN">[Optional] determins how NaN values are handled for floating point element types. Ignored for integer elements. Defaul: (true) ignores NaNs.</param>
        /// <returns>Scalar array with the minimum value in A.</returns>
        /// <remarks><para>See <see cref="MathInternal.min(BaseArray{complex}, OutArray{long}, int, bool)"/> for 
        /// getting the minimum values along a specific dimension. </para>
        /// <para>The functions 'min','min','minall','minall' give all similar results between the framework APIs (ILNumericsV4/Matlab(R) / numpy)
        /// but show individual behavior regarding the handling of floating point NaN values and iteration order. The value of 'true' 
        /// for <paramref name="ignoreNaN"/> corresponds to the behavior of ILNumerics version 4/ Matlab(R). Only if 
        /// all elements in <paramref name="A"/> are <see cref="float.NaN"/> the value of the result will also be NaN. This is the default.</para>
        /// <para>The value of 'false' for <paramref name="ignoreNaN"/> gives the numpy behavior: NaN values take precedence over non-NaN values. If 
        /// one element in the set of values is NaN the result will also be NaN. This is the default behavior for such functions defined as 
        /// [numpy API] extension methods on the array classes.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{complex}, OutArray{long}, int, bool)"/>
        internal static unsafe Array<complex> minall(ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> A,
            OutArray<long> index = null, StorageOrders order = StorageOrders.ColumnMajor, bool ignoreNaN = true) {


            /*
             * Algorithm option 1: 
             * 1) find the longest dim: minDimIdx 
             * 2) perform min(I:I) on the longest dim
             * 2.1) modify indices to reflect their sequential indices (row major!) <- this was the showstopper
             * 3) sort min values + indices descending
             * 4) min value + min index is a position [0].
             *
             * Option 2 (recommended): 
             * Implement minall via parallelized operator, handling indices. 
             * */

            // 1) find the longest dim: minDimIdx 
            //uint minDimIdx = 0;
            //long minDimVal = -1;
            //for (uint i = 0; i < ndims; i++) {
            //    if (storage.S[i] > minDimVal) {
            //        minDimVal = storage.S[i];
            //        minDimIdx = i;
            //    }
            //}
            //// 2) perform min(I:I) on the longest dim
            //Array<long> I = 0; 
            //Array<complex> minVals = min(A, I: I, dim: (int)minDimIdx);

            //// 3) sort min values + indices descending
            //System.Diagnostics.Debug.Assert(I.S.IsSameShape(minVals.S));
            //// ... not finished ...

            // following is the naive implementation (slow)
            using var _1 = ILNumerics.Core.Global.ReaderLock.Create(A, out var storage, releaseRetT: true);

            if (storage.S.NumberOfElements == 0) {
                // empty A 
                throw new ArgumentException("Unable to compute the minimum of an empty array.");
            }
            if (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor) {
                throw new ArgumentException($"Iteration order must be either {nameof(StorageOrders.ColumnMajor)} or {nameof(StorageOrders.RowMajor)}.");
            }
            var it = storage.S.Iterator(order).GetEnumerator();
            complex* pA = (complex*)storage.Handles[0].Pointer;
            long curI = -1, minIdx = 0;
            complex minVal = 0;
            #if IsInteger
                minVal = storage.GetValue(0);
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (curVal  < minVal) {
                        minVal = curVal;
                        minIdx = curI;
                    }
                }
#else
            if (ignoreNaN) {
                // Floating point: NaN the Matlab way (ignored if possible)
                // init with first non-NaN value
                while (it.MoveNext()) {
                    curI++;
                    if (!complex.IsNaN(minVal = pA[it.Current])) {
                        break;
                    }
                }
                if (curI < storage.S.NumberOfElements) {
                    // at least one non-NaN exists. It is stored in minVal now.
                    System.Diagnostics.Debug.Assert(!complex.IsNaN(minVal));

                    while (it.MoveNext()) {
                        curI++;
                        var curVal = pA[it.Current];
                        if (curVal  < minVal) { // NaNs compare: false
                            minVal = curVal;
                            minIdx = curI;
                        }
                    }
                } else {
                    // all NaNs
                    minIdx = 0;
                    System.Diagnostics.Debug.Assert(complex.IsNaN(minVal));
                }
            } else {
                // Floating point: NaN the numpy way (precedence) 
                minVal = storage.GetValue(0);  // may be NaN!
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (complex.IsNaN(curVal)) {
                        minVal = curVal;
                        minIdx = curI;
                        break;
                    }
                    if (curVal  < minVal) {
                        minVal = curVal;
                        minIdx = curI;
                    }
                }
            }
#endif
            if (!Equals(index, null)) {
                lock (index.SynchObj)
                    index.a = minIdx;
            }
            return minVal;
        }
       

        /// <summary>
        /// Computes the (flattened, row-major) sequential index and value of the element with the minimum value in <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the minimum for. This is not altered.</param>
        /// <param name="index">[Optional] index of the minimum value. Default: (null) the index is not returned.</param>
        /// <param name="order">[Optional] iteration order for the flattened sequential indices. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <param name="ignoreNaN">[Optional] determins how NaN values are handled for floating point element types. Ignored for integer elements. Defaul: (true) ignores NaNs.</param>
        /// <returns>Scalar array with the minimum value in A.</returns>
        /// <remarks><para>See <see cref="MathInternal.min(BaseArray{float}, OutArray{long}, int, bool)"/> for 
        /// getting the minimum values along a specific dimension. </para>
        /// <para>The functions 'min','min','minall','minall' give all similar results between the framework APIs (ILNumericsV4/Matlab(R) / numpy)
        /// but show individual behavior regarding the handling of floating point NaN values and iteration order. The value of 'true' 
        /// for <paramref name="ignoreNaN"/> corresponds to the behavior of ILNumerics version 4/ Matlab(R). Only if 
        /// all elements in <paramref name="A"/> are <see cref="float.NaN"/> the value of the result will also be NaN. This is the default.</para>
        /// <para>The value of 'false' for <paramref name="ignoreNaN"/> gives the numpy behavior: NaN values take precedence over non-NaN values. If 
        /// one element in the set of values is NaN the result will also be NaN. This is the default behavior for such functions defined as 
        /// [numpy API] extension methods on the array classes.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{float}, OutArray{long}, int, bool)"/>
        internal static unsafe Array<float> minall(ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> A,
            OutArray<long> index = null, StorageOrders order = StorageOrders.ColumnMajor, bool ignoreNaN = true) {


            /*
             * Algorithm option 1: 
             * 1) find the longest dim: minDimIdx 
             * 2) perform min(I:I) on the longest dim
             * 2.1) modify indices to reflect their sequential indices (row major!) <- this was the showstopper
             * 3) sort min values + indices descending
             * 4) min value + min index is a position [0].
             *
             * Option 2 (recommended): 
             * Implement minall via parallelized operator, handling indices. 
             * */

            // 1) find the longest dim: minDimIdx 
            //uint minDimIdx = 0;
            //long minDimVal = -1;
            //for (uint i = 0; i < ndims; i++) {
            //    if (storage.S[i] > minDimVal) {
            //        minDimVal = storage.S[i];
            //        minDimIdx = i;
            //    }
            //}
            //// 2) perform min(I:I) on the longest dim
            //Array<long> I = 0; 
            //Array<float> minVals = min(A, I: I, dim: (int)minDimIdx);

            //// 3) sort min values + indices descending
            //System.Diagnostics.Debug.Assert(I.S.IsSameShape(minVals.S));
            //// ... not finished ...

            // following is the naive implementation (slow)
            using var _1 = ILNumerics.Core.Global.ReaderLock.Create(A, out var storage, releaseRetT: true);

            if (storage.S.NumberOfElements == 0) {
                // empty A 
                throw new ArgumentException("Unable to compute the minimum of an empty array.");
            }
            if (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor) {
                throw new ArgumentException($"Iteration order must be either {nameof(StorageOrders.ColumnMajor)} or {nameof(StorageOrders.RowMajor)}.");
            }
            var it = storage.S.Iterator(order).GetEnumerator();
            float* pA = (float*)storage.Handles[0].Pointer;
            long curI = -1, minIdx = 0;
            float minVal = 0;
            #if IsInteger
                minVal = storage.GetValue(0);
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (curVal  < minVal) {
                        minVal = curVal;
                        minIdx = curI;
                    }
                }
#else
            if (ignoreNaN) {
                // Floating point: NaN the Matlab way (ignored if possible)
                // init with first non-NaN value
                while (it.MoveNext()) {
                    curI++;
                    if (!float.IsNaN(minVal = pA[it.Current])) {
                        break;
                    }
                }
                if (curI < storage.S.NumberOfElements) {
                    // at least one non-NaN exists. It is stored in minVal now.
                    System.Diagnostics.Debug.Assert(!float.IsNaN(minVal));

                    while (it.MoveNext()) {
                        curI++;
                        var curVal = pA[it.Current];
                        if (curVal  < minVal) { // NaNs compare: false
                            minVal = curVal;
                            minIdx = curI;
                        }
                    }
                } else {
                    // all NaNs
                    minIdx = 0;
                    System.Diagnostics.Debug.Assert(float.IsNaN(minVal));
                }
            } else {
                // Floating point: NaN the numpy way (precedence) 
                minVal = storage.GetValue(0);  // may be NaN!
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (float.IsNaN(curVal)) {
                        minVal = curVal;
                        minIdx = curI;
                        break;
                    }
                    if (curVal  < minVal) {
                        minVal = curVal;
                        minIdx = curI;
                    }
                }
            }
#endif
            if (!Equals(index, null)) {
                lock (index.SynchObj)
                    index.a = minIdx;
            }
            return minVal;
        }
       

        /// <summary>
        /// Computes the (flattened, row-major) sequential index and value of the element with the minimum value in <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the minimum for. This is not altered.</param>
        /// <param name="index">[Optional] index of the minimum value. Default: (null) the index is not returned.</param>
        /// <param name="order">[Optional] iteration order for the flattened sequential indices. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <param name="ignoreNaN">[Optional] determins how NaN values are handled for floating point element types. Ignored for integer elements. Defaul: (true) ignores NaNs.</param>
        /// <returns>Scalar array with the minimum value in A.</returns>
        /// <remarks><para>See <see cref="MathInternal.min(BaseArray{long}, OutArray{long}, int, bool)"/> for 
        /// getting the minimum values along a specific dimension. </para>
        /// <para>The functions 'min','min','minall','minall' give all similar results between the framework APIs (ILNumericsV4/Matlab(R) / numpy)
        /// but show individual behavior regarding the handling of floating point NaN values and iteration order. The value of 'true' 
        /// for <paramref name="ignoreNaN"/> corresponds to the behavior of ILNumerics version 4/ Matlab(R). Only if 
        /// all elements in <paramref name="A"/> are <see cref="float.NaN"/> the value of the result will also be NaN. This is the default.</para>
        /// <para>The value of 'false' for <paramref name="ignoreNaN"/> gives the numpy behavior: NaN values take precedence over non-NaN values. If 
        /// one element in the set of values is NaN the result will also be NaN. This is the default behavior for such functions defined as 
        /// [numpy API] extension methods on the array classes.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{long}, OutArray{long}, int, bool)"/>
        internal static unsafe Array<long> minall(ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> A,
            OutArray<long> index = null, StorageOrders order = StorageOrders.ColumnMajor, bool ignoreNaN = true) {


            /*
             * Algorithm option 1: 
             * 1) find the longest dim: minDimIdx 
             * 2) perform min(I:I) on the longest dim
             * 2.1) modify indices to reflect their sequential indices (row major!) <- this was the showstopper
             * 3) sort min values + indices descending
             * 4) min value + min index is a position [0].
             *
             * Option 2 (recommended): 
             * Implement minall via parallelized operator, handling indices. 
             * */

            // 1) find the longest dim: minDimIdx 
            //uint minDimIdx = 0;
            //long minDimVal = -1;
            //for (uint i = 0; i < ndims; i++) {
            //    if (storage.S[i] > minDimVal) {
            //        minDimVal = storage.S[i];
            //        minDimIdx = i;
            //    }
            //}
            //// 2) perform min(I:I) on the longest dim
            //Array<long> I = 0; 
            //Array<long> minVals = min(A, I: I, dim: (int)minDimIdx);

            //// 3) sort min values + indices descending
            //System.Diagnostics.Debug.Assert(I.S.IsSameShape(minVals.S));
            //// ... not finished ...

            // following is the naive implementation (slow)
            using var _1 = ILNumerics.Core.Global.ReaderLock.Create(A, out var storage, releaseRetT: true);

            if (storage.S.NumberOfElements == 0) {
                // empty A 
                throw new ArgumentException("Unable to compute the minimum of an empty array.");
            }
            if (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor) {
                throw new ArgumentException($"Iteration order must be either {nameof(StorageOrders.ColumnMajor)} or {nameof(StorageOrders.RowMajor)}.");
            }
            var it = storage.S.Iterator(order).GetEnumerator();
            long* pA = (long*)storage.Handles[0].Pointer;
            long curI = -1, minIdx = 0;
            long minVal = 0;
            #if !IsInteger
                minVal = storage.GetValue(0);
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (curVal  < minVal) {
                        minVal = curVal;
                        minIdx = curI;
                    }
                }
#else
            if (ignoreNaN) {
                // Floating point: NaN the Matlab way (ignored if possible)
                // init with first non-NaN value
                while (it.MoveNext()) {
                    curI++;
                    if (!long.IsNaN(minVal = pA[it.Current])) {
                        break;
                    }
                }
                if (curI < storage.S.NumberOfElements) {
                    // at least one non-NaN exists. It is stored in minVal now.
                    System.Diagnostics.Debug.Assert(!long.IsNaN(minVal));

                    while (it.MoveNext()) {
                        curI++;
                        var curVal = pA[it.Current];
                        if (curVal  < minVal) { // NaNs compare: false
                            minVal = curVal;
                            minIdx = curI;
                        }
                    }
                } else {
                    // all NaNs
                    minIdx = 0;
                    System.Diagnostics.Debug.Assert(long.IsNaN(minVal));
                }
            } else {
                // Floating point: NaN the numpy way (precedence) 
                minVal = storage.GetValue(0);  // may be NaN!
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (long.IsNaN(curVal)) {
                        minVal = curVal;
                        minIdx = curI;
                        break;
                    }
                    if (curVal  < minVal) {
                        minVal = curVal;
                        minIdx = curI;
                    }
                }
            }
#endif
            if (!Equals(index, null)) {
                lock (index.SynchObj)
                    index.a = minIdx;
            }
            return minVal;
        }
       

        /// <summary>
        /// Computes the (flattened, row-major) sequential index and value of the element with the minimum value in <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the minimum for. This is not altered.</param>
        /// <param name="index">[Optional] index of the minimum value. Default: (null) the index is not returned.</param>
        /// <param name="order">[Optional] iteration order for the flattened sequential indices. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <param name="ignoreNaN">[Optional] determins how NaN values are handled for floating point element types. Ignored for integer elements. Defaul: (true) ignores NaNs.</param>
        /// <returns>Scalar array with the minimum value in A.</returns>
        /// <remarks><para>See <see cref="MathInternal.min(BaseArray{ulong}, OutArray{long}, int, bool)"/> for 
        /// getting the minimum values along a specific dimension. </para>
        /// <para>The functions 'min','min','minall','minall' give all similar results between the framework APIs (ILNumericsV4/Matlab(R) / numpy)
        /// but show individual behavior regarding the handling of floating point NaN values and iteration order. The value of 'true' 
        /// for <paramref name="ignoreNaN"/> corresponds to the behavior of ILNumerics version 4/ Matlab(R). Only if 
        /// all elements in <paramref name="A"/> are <see cref="float.NaN"/> the value of the result will also be NaN. This is the default.</para>
        /// <para>The value of 'false' for <paramref name="ignoreNaN"/> gives the numpy behavior: NaN values take precedence over non-NaN values. If 
        /// one element in the set of values is NaN the result will also be NaN. This is the default behavior for such functions defined as 
        /// [numpy API] extension methods on the array classes.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{ulong}, OutArray{long}, int, bool)"/>
        internal static unsafe Array<ulong> minall(ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> A,
            OutArray<long> index = null, StorageOrders order = StorageOrders.ColumnMajor, bool ignoreNaN = true) {


            /*
             * Algorithm option 1: 
             * 1) find the longest dim: minDimIdx 
             * 2) perform min(I:I) on the longest dim
             * 2.1) modify indices to reflect their sequential indices (row major!) <- this was the showstopper
             * 3) sort min values + indices descending
             * 4) min value + min index is a position [0].
             *
             * Option 2 (recommended): 
             * Implement minall via parallelized operator, handling indices. 
             * */

            // 1) find the longest dim: minDimIdx 
            //uint minDimIdx = 0;
            //long minDimVal = -1;
            //for (uint i = 0; i < ndims; i++) {
            //    if (storage.S[i] > minDimVal) {
            //        minDimVal = storage.S[i];
            //        minDimIdx = i;
            //    }
            //}
            //// 2) perform min(I:I) on the longest dim
            //Array<long> I = 0; 
            //Array<ulong> minVals = min(A, I: I, dim: (int)minDimIdx);

            //// 3) sort min values + indices descending
            //System.Diagnostics.Debug.Assert(I.S.IsSameShape(minVals.S));
            //// ... not finished ...

            // following is the naive implementation (slow)
            using var _1 = ILNumerics.Core.Global.ReaderLock.Create(A, out var storage, releaseRetT: true);

            if (storage.S.NumberOfElements == 0) {
                // empty A 
                throw new ArgumentException("Unable to compute the minimum of an empty array.");
            }
            if (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor) {
                throw new ArgumentException($"Iteration order must be either {nameof(StorageOrders.ColumnMajor)} or {nameof(StorageOrders.RowMajor)}.");
            }
            var it = storage.S.Iterator(order).GetEnumerator();
            ulong* pA = (ulong*)storage.Handles[0].Pointer;
            long curI = -1, minIdx = 0;
            ulong minVal = 0;
            #if !IsInteger
                minVal = storage.GetValue(0);
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (curVal  < minVal) {
                        minVal = curVal;
                        minIdx = curI;
                    }
                }
#else
            if (ignoreNaN) {
                // Floating point: NaN the Matlab way (ignored if possible)
                // init with first non-NaN value
                while (it.MoveNext()) {
                    curI++;
                    if (!ulong.IsNaN(minVal = pA[it.Current])) {
                        break;
                    }
                }
                if (curI < storage.S.NumberOfElements) {
                    // at least one non-NaN exists. It is stored in minVal now.
                    System.Diagnostics.Debug.Assert(!ulong.IsNaN(minVal));

                    while (it.MoveNext()) {
                        curI++;
                        var curVal = pA[it.Current];
                        if (curVal  < minVal) { // NaNs compare: false
                            minVal = curVal;
                            minIdx = curI;
                        }
                    }
                } else {
                    // all NaNs
                    minIdx = 0;
                    System.Diagnostics.Debug.Assert(ulong.IsNaN(minVal));
                }
            } else {
                // Floating point: NaN the numpy way (precedence) 
                minVal = storage.GetValue(0);  // may be NaN!
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (ulong.IsNaN(curVal)) {
                        minVal = curVal;
                        minIdx = curI;
                        break;
                    }
                    if (curVal  < minVal) {
                        minVal = curVal;
                        minIdx = curI;
                    }
                }
            }
#endif
            if (!Equals(index, null)) {
                lock (index.SynchObj)
                    index.a = minIdx;
            }
            return minVal;
        }
       

        /// <summary>
        /// Computes the (flattened, row-major) sequential index and value of the element with the minimum value in <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the minimum for. This is not altered.</param>
        /// <param name="index">[Optional] index of the minimum value. Default: (null) the index is not returned.</param>
        /// <param name="order">[Optional] iteration order for the flattened sequential indices. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <param name="ignoreNaN">[Optional] determins how NaN values are handled for floating point element types. Ignored for integer elements. Defaul: (true) ignores NaNs.</param>
        /// <returns>Scalar array with the minimum value in A.</returns>
        /// <remarks><para>See <see cref="MathInternal.min(BaseArray{int}, OutArray{long}, int, bool)"/> for 
        /// getting the minimum values along a specific dimension. </para>
        /// <para>The functions 'min','min','minall','minall' give all similar results between the framework APIs (ILNumericsV4/Matlab(R) / numpy)
        /// but show individual behavior regarding the handling of floating point NaN values and iteration order. The value of 'true' 
        /// for <paramref name="ignoreNaN"/> corresponds to the behavior of ILNumerics version 4/ Matlab(R). Only if 
        /// all elements in <paramref name="A"/> are <see cref="float.NaN"/> the value of the result will also be NaN. This is the default.</para>
        /// <para>The value of 'false' for <paramref name="ignoreNaN"/> gives the numpy behavior: NaN values take precedence over non-NaN values. If 
        /// one element in the set of values is NaN the result will also be NaN. This is the default behavior for such functions defined as 
        /// [numpy API] extension methods on the array classes.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{int}, OutArray{long}, int, bool)"/>
        internal static unsafe Array<int> minall(ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> A,
            OutArray<long> index = null, StorageOrders order = StorageOrders.ColumnMajor, bool ignoreNaN = true) {


            /*
             * Algorithm option 1: 
             * 1) find the longest dim: minDimIdx 
             * 2) perform min(I:I) on the longest dim
             * 2.1) modify indices to reflect their sequential indices (row major!) <- this was the showstopper
             * 3) sort min values + indices descending
             * 4) min value + min index is a position [0].
             *
             * Option 2 (recommended): 
             * Implement minall via parallelized operator, handling indices. 
             * */

            // 1) find the longest dim: minDimIdx 
            //uint minDimIdx = 0;
            //long minDimVal = -1;
            //for (uint i = 0; i < ndims; i++) {
            //    if (storage.S[i] > minDimVal) {
            //        minDimVal = storage.S[i];
            //        minDimIdx = i;
            //    }
            //}
            //// 2) perform min(I:I) on the longest dim
            //Array<long> I = 0; 
            //Array<int> minVals = min(A, I: I, dim: (int)minDimIdx);

            //// 3) sort min values + indices descending
            //System.Diagnostics.Debug.Assert(I.S.IsSameShape(minVals.S));
            //// ... not finished ...

            // following is the naive implementation (slow)
            using var _1 = ILNumerics.Core.Global.ReaderLock.Create(A, out var storage, releaseRetT: true);

            if (storage.S.NumberOfElements == 0) {
                // empty A 
                throw new ArgumentException("Unable to compute the minimum of an empty array.");
            }
            if (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor) {
                throw new ArgumentException($"Iteration order must be either {nameof(StorageOrders.ColumnMajor)} or {nameof(StorageOrders.RowMajor)}.");
            }
            var it = storage.S.Iterator(order).GetEnumerator();
            int* pA = (int*)storage.Handles[0].Pointer;
            long curI = -1, minIdx = 0;
            int minVal = 0;
            #if !IsInteger
                minVal = storage.GetValue(0);
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (curVal  < minVal) {
                        minVal = curVal;
                        minIdx = curI;
                    }
                }
#else
            if (ignoreNaN) {
                // Floating point: NaN the Matlab way (ignored if possible)
                // init with first non-NaN value
                while (it.MoveNext()) {
                    curI++;
                    if (!int.IsNaN(minVal = pA[it.Current])) {
                        break;
                    }
                }
                if (curI < storage.S.NumberOfElements) {
                    // at least one non-NaN exists. It is stored in minVal now.
                    System.Diagnostics.Debug.Assert(!int.IsNaN(minVal));

                    while (it.MoveNext()) {
                        curI++;
                        var curVal = pA[it.Current];
                        if (curVal  < minVal) { // NaNs compare: false
                            minVal = curVal;
                            minIdx = curI;
                        }
                    }
                } else {
                    // all NaNs
                    minIdx = 0;
                    System.Diagnostics.Debug.Assert(int.IsNaN(minVal));
                }
            } else {
                // Floating point: NaN the numpy way (precedence) 
                minVal = storage.GetValue(0);  // may be NaN!
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (int.IsNaN(curVal)) {
                        minVal = curVal;
                        minIdx = curI;
                        break;
                    }
                    if (curVal  < minVal) {
                        minVal = curVal;
                        minIdx = curI;
                    }
                }
            }
#endif
            if (!Equals(index, null)) {
                lock (index.SynchObj)
                    index.a = minIdx;
            }
            return minVal;
        }
       

        /// <summary>
        /// Computes the (flattened, row-major) sequential index and value of the element with the minimum value in <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the minimum for. This is not altered.</param>
        /// <param name="index">[Optional] index of the minimum value. Default: (null) the index is not returned.</param>
        /// <param name="order">[Optional] iteration order for the flattened sequential indices. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <param name="ignoreNaN">[Optional] determins how NaN values are handled for floating point element types. Ignored for integer elements. Defaul: (true) ignores NaNs.</param>
        /// <returns>Scalar array with the minimum value in A.</returns>
        /// <remarks><para>See <see cref="MathInternal.min(BaseArray{uint}, OutArray{long}, int, bool)"/> for 
        /// getting the minimum values along a specific dimension. </para>
        /// <para>The functions 'min','min','minall','minall' give all similar results between the framework APIs (ILNumericsV4/Matlab(R) / numpy)
        /// but show individual behavior regarding the handling of floating point NaN values and iteration order. The value of 'true' 
        /// for <paramref name="ignoreNaN"/> corresponds to the behavior of ILNumerics version 4/ Matlab(R). Only if 
        /// all elements in <paramref name="A"/> are <see cref="float.NaN"/> the value of the result will also be NaN. This is the default.</para>
        /// <para>The value of 'false' for <paramref name="ignoreNaN"/> gives the numpy behavior: NaN values take precedence over non-NaN values. If 
        /// one element in the set of values is NaN the result will also be NaN. This is the default behavior for such functions defined as 
        /// [numpy API] extension methods on the array classes.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{uint}, OutArray{long}, int, bool)"/>
        internal static unsafe Array<uint> minall(ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> A,
            OutArray<long> index = null, StorageOrders order = StorageOrders.ColumnMajor, bool ignoreNaN = true) {


            /*
             * Algorithm option 1: 
             * 1) find the longest dim: minDimIdx 
             * 2) perform min(I:I) on the longest dim
             * 2.1) modify indices to reflect their sequential indices (row major!) <- this was the showstopper
             * 3) sort min values + indices descending
             * 4) min value + min index is a position [0].
             *
             * Option 2 (recommended): 
             * Implement minall via parallelized operator, handling indices. 
             * */

            // 1) find the longest dim: minDimIdx 
            //uint minDimIdx = 0;
            //long minDimVal = -1;
            //for (uint i = 0; i < ndims; i++) {
            //    if (storage.S[i] > minDimVal) {
            //        minDimVal = storage.S[i];
            //        minDimIdx = i;
            //    }
            //}
            //// 2) perform min(I:I) on the longest dim
            //Array<long> I = 0; 
            //Array<uint> minVals = min(A, I: I, dim: (int)minDimIdx);

            //// 3) sort min values + indices descending
            //System.Diagnostics.Debug.Assert(I.S.IsSameShape(minVals.S));
            //// ... not finished ...

            // following is the naive implementation (slow)
            using var _1 = ILNumerics.Core.Global.ReaderLock.Create(A, out var storage, releaseRetT: true);

            if (storage.S.NumberOfElements == 0) {
                // empty A 
                throw new ArgumentException("Unable to compute the minimum of an empty array.");
            }
            if (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor) {
                throw new ArgumentException($"Iteration order must be either {nameof(StorageOrders.ColumnMajor)} or {nameof(StorageOrders.RowMajor)}.");
            }
            var it = storage.S.Iterator(order).GetEnumerator();
            uint* pA = (uint*)storage.Handles[0].Pointer;
            long curI = -1, minIdx = 0;
            uint minVal = 0;
            #if !IsInteger
                minVal = storage.GetValue(0);
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (curVal  < minVal) {
                        minVal = curVal;
                        minIdx = curI;
                    }
                }
#else
            if (ignoreNaN) {
                // Floating point: NaN the Matlab way (ignored if possible)
                // init with first non-NaN value
                while (it.MoveNext()) {
                    curI++;
                    if (!uint.IsNaN(minVal = pA[it.Current])) {
                        break;
                    }
                }
                if (curI < storage.S.NumberOfElements) {
                    // at least one non-NaN exists. It is stored in minVal now.
                    System.Diagnostics.Debug.Assert(!uint.IsNaN(minVal));

                    while (it.MoveNext()) {
                        curI++;
                        var curVal = pA[it.Current];
                        if (curVal  < minVal) { // NaNs compare: false
                            minVal = curVal;
                            minIdx = curI;
                        }
                    }
                } else {
                    // all NaNs
                    minIdx = 0;
                    System.Diagnostics.Debug.Assert(uint.IsNaN(minVal));
                }
            } else {
                // Floating point: NaN the numpy way (precedence) 
                minVal = storage.GetValue(0);  // may be NaN!
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (uint.IsNaN(curVal)) {
                        minVal = curVal;
                        minIdx = curI;
                        break;
                    }
                    if (curVal  < minVal) {
                        minVal = curVal;
                        minIdx = curI;
                    }
                }
            }
#endif
            if (!Equals(index, null)) {
                lock (index.SynchObj)
                    index.a = minIdx;
            }
            return minVal;
        }
       

        /// <summary>
        /// Computes the (flattened, row-major) sequential index and value of the element with the minimum value in <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the minimum for. This is not altered.</param>
        /// <param name="index">[Optional] index of the minimum value. Default: (null) the index is not returned.</param>
        /// <param name="order">[Optional] iteration order for the flattened sequential indices. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <param name="ignoreNaN">[Optional] determins how NaN values are handled for floating point element types. Ignored for integer elements. Defaul: (true) ignores NaNs.</param>
        /// <returns>Scalar array with the minimum value in A.</returns>
        /// <remarks><para>See <see cref="MathInternal.min(BaseArray{short}, OutArray{long}, int, bool)"/> for 
        /// getting the minimum values along a specific dimension. </para>
        /// <para>The functions 'min','min','minall','minall' give all similar results between the framework APIs (ILNumericsV4/Matlab(R) / numpy)
        /// but show individual behavior regarding the handling of floating point NaN values and iteration order. The value of 'true' 
        /// for <paramref name="ignoreNaN"/> corresponds to the behavior of ILNumerics version 4/ Matlab(R). Only if 
        /// all elements in <paramref name="A"/> are <see cref="float.NaN"/> the value of the result will also be NaN. This is the default.</para>
        /// <para>The value of 'false' for <paramref name="ignoreNaN"/> gives the numpy behavior: NaN values take precedence over non-NaN values. If 
        /// one element in the set of values is NaN the result will also be NaN. This is the default behavior for such functions defined as 
        /// [numpy API] extension methods on the array classes.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{short}, OutArray{long}, int, bool)"/>
        internal static unsafe Array<short> minall(ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> A,
            OutArray<long> index = null, StorageOrders order = StorageOrders.ColumnMajor, bool ignoreNaN = true) {


            /*
             * Algorithm option 1: 
             * 1) find the longest dim: minDimIdx 
             * 2) perform min(I:I) on the longest dim
             * 2.1) modify indices to reflect their sequential indices (row major!) <- this was the showstopper
             * 3) sort min values + indices descending
             * 4) min value + min index is a position [0].
             *
             * Option 2 (recommended): 
             * Implement minall via parallelized operator, handling indices. 
             * */

            // 1) find the longest dim: minDimIdx 
            //uint minDimIdx = 0;
            //long minDimVal = -1;
            //for (uint i = 0; i < ndims; i++) {
            //    if (storage.S[i] > minDimVal) {
            //        minDimVal = storage.S[i];
            //        minDimIdx = i;
            //    }
            //}
            //// 2) perform min(I:I) on the longest dim
            //Array<long> I = 0; 
            //Array<short> minVals = min(A, I: I, dim: (int)minDimIdx);

            //// 3) sort min values + indices descending
            //System.Diagnostics.Debug.Assert(I.S.IsSameShape(minVals.S));
            //// ... not finished ...

            // following is the naive implementation (slow)
            using var _1 = ILNumerics.Core.Global.ReaderLock.Create(A, out var storage, releaseRetT: true);

            if (storage.S.NumberOfElements == 0) {
                // empty A 
                throw new ArgumentException("Unable to compute the minimum of an empty array.");
            }
            if (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor) {
                throw new ArgumentException($"Iteration order must be either {nameof(StorageOrders.ColumnMajor)} or {nameof(StorageOrders.RowMajor)}.");
            }
            var it = storage.S.Iterator(order).GetEnumerator();
            short* pA = (short*)storage.Handles[0].Pointer;
            long curI = -1, minIdx = 0;
            short minVal = 0;
            #if !IsInteger
                minVal = storage.GetValue(0);
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (curVal  < minVal) {
                        minVal = curVal;
                        minIdx = curI;
                    }
                }
#else
            if (ignoreNaN) {
                // Floating point: NaN the Matlab way (ignored if possible)
                // init with first non-NaN value
                while (it.MoveNext()) {
                    curI++;
                    if (!short.IsNaN(minVal = pA[it.Current])) {
                        break;
                    }
                }
                if (curI < storage.S.NumberOfElements) {
                    // at least one non-NaN exists. It is stored in minVal now.
                    System.Diagnostics.Debug.Assert(!short.IsNaN(minVal));

                    while (it.MoveNext()) {
                        curI++;
                        var curVal = pA[it.Current];
                        if (curVal  < minVal) { // NaNs compare: false
                            minVal = curVal;
                            minIdx = curI;
                        }
                    }
                } else {
                    // all NaNs
                    minIdx = 0;
                    System.Diagnostics.Debug.Assert(short.IsNaN(minVal));
                }
            } else {
                // Floating point: NaN the numpy way (precedence) 
                minVal = storage.GetValue(0);  // may be NaN!
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (short.IsNaN(curVal)) {
                        minVal = curVal;
                        minIdx = curI;
                        break;
                    }
                    if (curVal  < minVal) {
                        minVal = curVal;
                        minIdx = curI;
                    }
                }
            }
#endif
            if (!Equals(index, null)) {
                lock (index.SynchObj)
                    index.a = minIdx;
            }
            return minVal;
        }
       

        /// <summary>
        /// Computes the (flattened, row-major) sequential index and value of the element with the minimum value in <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the minimum for. This is not altered.</param>
        /// <param name="index">[Optional] index of the minimum value. Default: (null) the index is not returned.</param>
        /// <param name="order">[Optional] iteration order for the flattened sequential indices. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <param name="ignoreNaN">[Optional] determins how NaN values are handled for floating point element types. Ignored for integer elements. Defaul: (true) ignores NaNs.</param>
        /// <returns>Scalar array with the minimum value in A.</returns>
        /// <remarks><para>See <see cref="MathInternal.min(BaseArray{ushort}, OutArray{long}, int, bool)"/> for 
        /// getting the minimum values along a specific dimension. </para>
        /// <para>The functions 'min','min','minall','minall' give all similar results between the framework APIs (ILNumericsV4/Matlab(R) / numpy)
        /// but show individual behavior regarding the handling of floating point NaN values and iteration order. The value of 'true' 
        /// for <paramref name="ignoreNaN"/> corresponds to the behavior of ILNumerics version 4/ Matlab(R). Only if 
        /// all elements in <paramref name="A"/> are <see cref="float.NaN"/> the value of the result will also be NaN. This is the default.</para>
        /// <para>The value of 'false' for <paramref name="ignoreNaN"/> gives the numpy behavior: NaN values take precedence over non-NaN values. If 
        /// one element in the set of values is NaN the result will also be NaN. This is the default behavior for such functions defined as 
        /// [numpy API] extension methods on the array classes.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{ushort}, OutArray{long}, int, bool)"/>
        internal static unsafe Array<ushort> minall(ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> A,
            OutArray<long> index = null, StorageOrders order = StorageOrders.ColumnMajor, bool ignoreNaN = true) {


            /*
             * Algorithm option 1: 
             * 1) find the longest dim: minDimIdx 
             * 2) perform min(I:I) on the longest dim
             * 2.1) modify indices to reflect their sequential indices (row major!) <- this was the showstopper
             * 3) sort min values + indices descending
             * 4) min value + min index is a position [0].
             *
             * Option 2 (recommended): 
             * Implement minall via parallelized operator, handling indices. 
             * */

            // 1) find the longest dim: minDimIdx 
            //uint minDimIdx = 0;
            //long minDimVal = -1;
            //for (uint i = 0; i < ndims; i++) {
            //    if (storage.S[i] > minDimVal) {
            //        minDimVal = storage.S[i];
            //        minDimIdx = i;
            //    }
            //}
            //// 2) perform min(I:I) on the longest dim
            //Array<long> I = 0; 
            //Array<ushort> minVals = min(A, I: I, dim: (int)minDimIdx);

            //// 3) sort min values + indices descending
            //System.Diagnostics.Debug.Assert(I.S.IsSameShape(minVals.S));
            //// ... not finished ...

            // following is the naive implementation (slow)
            using var _1 = ILNumerics.Core.Global.ReaderLock.Create(A, out var storage, releaseRetT: true);

            if (storage.S.NumberOfElements == 0) {
                // empty A 
                throw new ArgumentException("Unable to compute the minimum of an empty array.");
            }
            if (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor) {
                throw new ArgumentException($"Iteration order must be either {nameof(StorageOrders.ColumnMajor)} or {nameof(StorageOrders.RowMajor)}.");
            }
            var it = storage.S.Iterator(order).GetEnumerator();
            ushort* pA = (ushort*)storage.Handles[0].Pointer;
            long curI = -1, minIdx = 0;
            ushort minVal = 0;
            #if !IsInteger
                minVal = storage.GetValue(0);
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (curVal  < minVal) {
                        minVal = curVal;
                        minIdx = curI;
                    }
                }
#else
            if (ignoreNaN) {
                // Floating point: NaN the Matlab way (ignored if possible)
                // init with first non-NaN value
                while (it.MoveNext()) {
                    curI++;
                    if (!ushort.IsNaN(minVal = pA[it.Current])) {
                        break;
                    }
                }
                if (curI < storage.S.NumberOfElements) {
                    // at least one non-NaN exists. It is stored in minVal now.
                    System.Diagnostics.Debug.Assert(!ushort.IsNaN(minVal));

                    while (it.MoveNext()) {
                        curI++;
                        var curVal = pA[it.Current];
                        if (curVal  < minVal) { // NaNs compare: false
                            minVal = curVal;
                            minIdx = curI;
                        }
                    }
                } else {
                    // all NaNs
                    minIdx = 0;
                    System.Diagnostics.Debug.Assert(ushort.IsNaN(minVal));
                }
            } else {
                // Floating point: NaN the numpy way (precedence) 
                minVal = storage.GetValue(0);  // may be NaN!
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (ushort.IsNaN(curVal)) {
                        minVal = curVal;
                        minIdx = curI;
                        break;
                    }
                    if (curVal  < minVal) {
                        minVal = curVal;
                        minIdx = curI;
                    }
                }
            }
#endif
            if (!Equals(index, null)) {
                lock (index.SynchObj)
                    index.a = minIdx;
            }
            return minVal;
        }
       

        /// <summary>
        /// Computes the (flattened, row-major) sequential index and value of the element with the minimum value in <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the minimum for. This is not altered.</param>
        /// <param name="index">[Optional] index of the minimum value. Default: (null) the index is not returned.</param>
        /// <param name="order">[Optional] iteration order for the flattened sequential indices. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <param name="ignoreNaN">[Optional] determins how NaN values are handled for floating point element types. Ignored for integer elements. Defaul: (true) ignores NaNs.</param>
        /// <returns>Scalar array with the minimum value in A.</returns>
        /// <remarks><para>See <see cref="MathInternal.min(BaseArray{sbyte}, OutArray{long}, int, bool)"/> for 
        /// getting the minimum values along a specific dimension. </para>
        /// <para>The functions 'min','min','minall','minall' give all similar results between the framework APIs (ILNumericsV4/Matlab(R) / numpy)
        /// but show individual behavior regarding the handling of floating point NaN values and iteration order. The value of 'true' 
        /// for <paramref name="ignoreNaN"/> corresponds to the behavior of ILNumerics version 4/ Matlab(R). Only if 
        /// all elements in <paramref name="A"/> are <see cref="float.NaN"/> the value of the result will also be NaN. This is the default.</para>
        /// <para>The value of 'false' for <paramref name="ignoreNaN"/> gives the numpy behavior: NaN values take precedence over non-NaN values. If 
        /// one element in the set of values is NaN the result will also be NaN. This is the default behavior for such functions defined as 
        /// [numpy API] extension methods on the array classes.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{sbyte}, OutArray{long}, int, bool)"/>
        internal static unsafe Array<sbyte> minall(ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> A,
            OutArray<long> index = null, StorageOrders order = StorageOrders.ColumnMajor, bool ignoreNaN = true) {


            /*
             * Algorithm option 1: 
             * 1) find the longest dim: minDimIdx 
             * 2) perform min(I:I) on the longest dim
             * 2.1) modify indices to reflect their sequential indices (row major!) <- this was the showstopper
             * 3) sort min values + indices descending
             * 4) min value + min index is a position [0].
             *
             * Option 2 (recommended): 
             * Implement minall via parallelized operator, handling indices. 
             * */

            // 1) find the longest dim: minDimIdx 
            //uint minDimIdx = 0;
            //long minDimVal = -1;
            //for (uint i = 0; i < ndims; i++) {
            //    if (storage.S[i] > minDimVal) {
            //        minDimVal = storage.S[i];
            //        minDimIdx = i;
            //    }
            //}
            //// 2) perform min(I:I) on the longest dim
            //Array<long> I = 0; 
            //Array<sbyte> minVals = min(A, I: I, dim: (int)minDimIdx);

            //// 3) sort min values + indices descending
            //System.Diagnostics.Debug.Assert(I.S.IsSameShape(minVals.S));
            //// ... not finished ...

            // following is the naive implementation (slow)
            using var _1 = ILNumerics.Core.Global.ReaderLock.Create(A, out var storage, releaseRetT: true);

            if (storage.S.NumberOfElements == 0) {
                // empty A 
                throw new ArgumentException("Unable to compute the minimum of an empty array.");
            }
            if (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor) {
                throw new ArgumentException($"Iteration order must be either {nameof(StorageOrders.ColumnMajor)} or {nameof(StorageOrders.RowMajor)}.");
            }
            var it = storage.S.Iterator(order).GetEnumerator();
            sbyte* pA = (sbyte*)storage.Handles[0].Pointer;
            long curI = -1, minIdx = 0;
            sbyte minVal = 0;
            #if !IsInteger
                minVal = storage.GetValue(0);
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (curVal  < minVal) {
                        minVal = curVal;
                        minIdx = curI;
                    }
                }
#else
            if (ignoreNaN) {
                // Floating point: NaN the Matlab way (ignored if possible)
                // init with first non-NaN value
                while (it.MoveNext()) {
                    curI++;
                    if (!sbyte.IsNaN(minVal = pA[it.Current])) {
                        break;
                    }
                }
                if (curI < storage.S.NumberOfElements) {
                    // at least one non-NaN exists. It is stored in minVal now.
                    System.Diagnostics.Debug.Assert(!sbyte.IsNaN(minVal));

                    while (it.MoveNext()) {
                        curI++;
                        var curVal = pA[it.Current];
                        if (curVal  < minVal) { // NaNs compare: false
                            minVal = curVal;
                            minIdx = curI;
                        }
                    }
                } else {
                    // all NaNs
                    minIdx = 0;
                    System.Diagnostics.Debug.Assert(sbyte.IsNaN(minVal));
                }
            } else {
                // Floating point: NaN the numpy way (precedence) 
                minVal = storage.GetValue(0);  // may be NaN!
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (sbyte.IsNaN(curVal)) {
                        minVal = curVal;
                        minIdx = curI;
                        break;
                    }
                    if (curVal  < minVal) {
                        minVal = curVal;
                        minIdx = curI;
                    }
                }
            }
#endif
            if (!Equals(index, null)) {
                lock (index.SynchObj)
                    index.a = minIdx;
            }
            return minVal;
        }
       

        /// <summary>
        /// Computes the (flattened, row-major) sequential index and value of the element with the minimum value in <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Array to compute the minimum for. This is not altered.</param>
        /// <param name="index">[Optional] index of the minimum value. Default: (null) the index is not returned.</param>
        /// <param name="order">[Optional] iteration order for the flattened sequential indices. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <param name="ignoreNaN">[Optional] determins how NaN values are handled for floating point element types. Ignored for integer elements. Defaul: (true) ignores NaNs.</param>
        /// <returns>Scalar array with the minimum value in A.</returns>
        /// <remarks><para>See <see cref="MathInternal.min(BaseArray{byte}, OutArray{long}, int, bool)"/> for 
        /// getting the minimum values along a specific dimension. </para>
        /// <para>The functions 'min','min','minall','minall' give all similar results between the framework APIs (ILNumericsV4/Matlab(R) / numpy)
        /// but show individual behavior regarding the handling of floating point NaN values and iteration order. The value of 'true' 
        /// for <paramref name="ignoreNaN"/> corresponds to the behavior of ILNumerics version 4/ Matlab(R). Only if 
        /// all elements in <paramref name="A"/> are <see cref="float.NaN"/> the value of the result will also be NaN. This is the default.</para>
        /// <para>The value of 'false' for <paramref name="ignoreNaN"/> gives the numpy behavior: NaN values take precedence over non-NaN values. If 
        /// one element in the set of values is NaN the result will also be NaN. This is the default behavior for such functions defined as 
        /// [numpy API] extension methods on the array classes.</para>
        /// </remarks>
        /// <seealso cref="MathInternal.min(BaseArray{byte}, OutArray{long}, int, bool)"/>
        internal static unsafe Array<byte> minall(ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> A,
            OutArray<long> index = null, StorageOrders order = StorageOrders.ColumnMajor, bool ignoreNaN = true) {


            /*
             * Algorithm option 1: 
             * 1) find the longest dim: minDimIdx 
             * 2) perform min(I:I) on the longest dim
             * 2.1) modify indices to reflect their sequential indices (row major!) <- this was the showstopper
             * 3) sort min values + indices descending
             * 4) min value + min index is a position [0].
             *
             * Option 2 (recommended): 
             * Implement minall via parallelized operator, handling indices. 
             * */

            // 1) find the longest dim: minDimIdx 
            //uint minDimIdx = 0;
            //long minDimVal = -1;
            //for (uint i = 0; i < ndims; i++) {
            //    if (storage.S[i] > minDimVal) {
            //        minDimVal = storage.S[i];
            //        minDimIdx = i;
            //    }
            //}
            //// 2) perform min(I:I) on the longest dim
            //Array<long> I = 0; 
            //Array<byte> minVals = min(A, I: I, dim: (int)minDimIdx);

            //// 3) sort min values + indices descending
            //System.Diagnostics.Debug.Assert(I.S.IsSameShape(minVals.S));
            //// ... not finished ...

            // following is the naive implementation (slow)
            using var _1 = ILNumerics.Core.Global.ReaderLock.Create(A, out var storage, releaseRetT: true);

            if (storage.S.NumberOfElements == 0) {
                // empty A 
                throw new ArgumentException("Unable to compute the minimum of an empty array.");
            }
            if (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor) {
                throw new ArgumentException($"Iteration order must be either {nameof(StorageOrders.ColumnMajor)} or {nameof(StorageOrders.RowMajor)}.");
            }
            var it = storage.S.Iterator(order).GetEnumerator();
            byte* pA = (byte*)storage.Handles[0].Pointer;
            long curI = -1, minIdx = 0;
            byte minVal = 0;
            #if !IsInteger
                minVal = storage.GetValue(0);
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (curVal  < minVal) {
                        minVal = curVal;
                        minIdx = curI;
                    }
                }
#else
            if (ignoreNaN) {
                // Floating point: NaN the Matlab way (ignored if possible)
                // init with first non-NaN value
                while (it.MoveNext()) {
                    curI++;
                    if (!byte.IsNaN(minVal = pA[it.Current])) {
                        break;
                    }
                }
                if (curI < storage.S.NumberOfElements) {
                    // at least one non-NaN exists. It is stored in minVal now.
                    System.Diagnostics.Debug.Assert(!byte.IsNaN(minVal));

                    while (it.MoveNext()) {
                        curI++;
                        var curVal = pA[it.Current];
                        if (curVal  < minVal) { // NaNs compare: false
                            minVal = curVal;
                            minIdx = curI;
                        }
                    }
                } else {
                    // all NaNs
                    minIdx = 0;
                    System.Diagnostics.Debug.Assert(byte.IsNaN(minVal));
                }
            } else {
                // Floating point: NaN the numpy way (precedence) 
                minVal = storage.GetValue(0);  // may be NaN!
                while (it.MoveNext()) {
                    curI++;
                    var curVal = pA[it.Current];
                    if (byte.IsNaN(curVal)) {
                        minVal = curVal;
                        minIdx = curI;
                        break;
                    }
                    if (curVal  < minVal) {
                        minVal = curVal;
                        minIdx = curI;
                    }
                }
            }
#endif
            if (!Equals(index, null)) {
                lock (index.SynchObj)
                    index.a = minIdx;
            }
            return minVal;
        }

#endregion HYCALPER AUTO GENERATED CODE

    }
}
