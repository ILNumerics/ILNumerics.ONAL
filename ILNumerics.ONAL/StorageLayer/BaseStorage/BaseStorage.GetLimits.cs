// MIT License
// 
// Copyright (c) 2026 ILNumerics GmbH
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////
using ILNumerics.Core.Global;
using System;
using System.Security;
using System.Threading;

namespace ILNumerics.Core.StorageLayer {

    /// <summary>
    /// Extension methods on base arrays.
    /// </summary>
    public static partial class ExtensionMethods {

        #region HYCALPER LOOPSTART Floating point types handling NaN & Inf
/*!HC:TYPELIST:
<hycalper>
<type>
    <source locate="here">
        double
    </source>
    <destination>float</destination>
</type>
<type>
    <source locate="here">
        Double
    </source>
    <destination>Single</destination>
</type>
</hycalper>
*/

        /// <summary>
        /// Finds the minimum and maximum values among all elements of <paramref name="storage"/>, optionally ignoring special floating point values.
        /// </summary>
        /// <param name="storage">Input storage.</param>
        /// <param name="min">[Output] The minimum value in <paramref name="storage"/>.</param>
        /// <param name="max">[Output] The maximum value in <paramref name="storage"/>.</param>
        /// <param name="ignoreInfinity">[Optional] When true only non-infinity and non-NaN values will be considered as regular values. Default: false.</param>
        /// <returns>True, if the minimum and maximum values could be computed. False otherwise.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input 
        /// <paramref name="storage"/> in a single run.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings 
        /// of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input <paramref name="storage"/> is not altered.</para>
        /// <para>The function always succeeds and returns <c>true</c> for non-empty arrays of integer 
        /// (non-floating point) element types. Overloads for floating point element types:
        /// the function succeeds on non-empty arrays which have at least one element other than NaN.</para>
        /// <para>The optional parameter <paramref name="ignoreInfinity"/> is useful, for example, to determine the 
        /// minimum and maximum values stored inside this storage in order to find scaling factors and for 
        /// normalization purposes. Here, <see cref="double.NegativeInfinity"/> and <see cref="double.PositiveInfinity"/>
        /// can be ignored since they would be not useful as scaling factors. For the default value ('false')
        /// infinity values are considered regular values and contribute to the result.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <paramref name="storage"/> is empty.</exception>
        
        internal unsafe static bool GetLimits (
            this BaseStorage<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> storage,
            out /*!HC:outArr*/double min, out /*!HC:outArr*/double max, bool ignoreInfinity = false) {

            var numel = storage.S.NumberOfElements;

            #region special input shapes
            if (numel == 0) {
                min = default(double); max = default(double);
                return false;
            } else if (numel == 1) {

                double a0 = storage.GetValue(0);

                if (double.IsNaN(a0)) { min = 0; max = 0; return false; }
                if (ignoreInfinity && double.IsInfinity(a0)) { min = 0; max = 0; return false; }
                min = a0;
                max = a0;
                return true;
            }
            #endregion

            byte* pIn = (byte*)storage.Handles[0].Pointer + storage.S.BaseOffset * (uint)sizeof(double);

            var outLen = storage.S.NumberOfElements;
            uint workItemCount;
            long workItemLength;
            int workerCount = 0;
            long i = 0;

            /*!HC:outArr*/
            double* accum_buffer; 

            if (ignoreInfinity) {

                #region ignore infinity
                if (storage.S.IsContinuous) {

                    #region continous
                    Helper.determineMultithreadingParameters(numel,
                        ref InnerLoops./*!HC:innerloopname*/GetLimitsNoInf.Double.s_threading_overhead_Cont64,
                        out workItemCount, out workItemLength);

                    var tmpbuffer = stackalloc /*!HC:outArr*/double[(int)workItemCount * 2];
                    accum_buffer = tmpbuffer;

                    // continous
                    if (workItemCount-- > 1) {

                        IntPtr* parameters = stackalloc IntPtr[4 + (int)workItemCount * 2];
                        parameters[0] = (IntPtr)pIn;
                        parameters[1] = (IntPtr)tmpbuffer;
                        parameters[2] = (IntPtr)workItemLength;
                        parameters[3] = (IntPtr)(&workerCount);
                        do {

                            System.Threading.Interlocked.Increment(ref workerCount);
                            IntPtr* locParam = parameters + 4 + i * 2;
                            locParam[0] = (IntPtr)parameters;
                            locParam[1] = (IntPtr)i;
                            Core.Global.ThreadPool.QueueUserWorkItem(
                                (uint)i,
                                Core.InnerLoops./*!HC:innerloopname*/GetLimitsNoInf.Double.Continous64OOPAction,
                                (IntPtr)locParam);
                            i++;
                        } while (i < workItemCount); 
                    }
                    workItemLength *= i;
                    Core.InnerLoops./*!HC:innerloopname*/GetLimitsNoInf.Double.Continous64OOP(
                        pIn + workItemLength * (uint)sizeof(double),
                        tmpbuffer + i * 2,
                        outLen - workItemLength);

                    // wait & tune the overhead of threading
                    if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                        Interlocked.Increment(ref InnerLoops./*!HC:innerloopname*/GetLimitsNoInf.Double.s_threading_overhead_Cont64);
                    } else {
                        Interlocked.Decrement(ref InnerLoops./*!HC:innerloopname*/GetLimitsNoInf.Double.s_threading_overhead_Cont64);
                    }
                    #endregion

                } else {

                    #region strided
                    // strided
                    var bsd = storage.S.GetBSD(write: false);
                    var ndims = (uint)bsd[0];
#if TEST_1000ASUINTMAXVALUE
                    if (storage.S.GetElementSpan() >= 1000) {
#else
                    if (Environment.Is64BitProcess && storage.S.GetElementSpan() * (uint)sizeof(double) > uint.MaxValue) {
#endif
                        #region strides are ulong* 
                        long* ordered_bsd = stackalloc long[(int)ndims * 2 + 3];

                        Core.Global.Helper.PrepareBSD(StorageOrders.Other, bsd, ordered_bsd, (uint)sizeof(double));

                        Helper.determineMultithreadingParameters(numel,
                                    ref InnerLoops./*!HC:innerloopname*/GetLimitsNoInf.Double.s_threading_overhead_Strided64,
                                    out workItemCount, out workItemLength);

                        var tmpbuffer = stackalloc /*!HC:outArr*/double[(int)workItemCount * 2];
                        accum_buffer = tmpbuffer;

                        if (workItemCount-- > 1) {

                            IntPtr* parameters = stackalloc IntPtr[5 + (int)workItemCount * 2];
                            parameters[0] = (IntPtr)pIn;
                            parameters[1] = (IntPtr)tmpbuffer;
                            parameters[2] = (IntPtr)ordered_bsd;
                            parameters[3] = (IntPtr)workItemLength;
                            parameters[4] = (IntPtr)(&workerCount);
                            do {

                                System.Threading.Interlocked.Increment(ref workerCount);
                                IntPtr* locParam = parameters + 5 + i * 2;
                                locParam[0] = (IntPtr)parameters;
                                locParam[1] = (IntPtr)i;
                                Core.Global.ThreadPool.QueueUserWorkItem(
                                    (uint)i,
                                    Core.InnerLoops./*!HC:innerloopname*/GetLimitsNoInf.Double.Strided64Action,
                                    (IntPtr)locParam);
                                i++;
                            } while (i < workItemCount); 
                        }
                        workItemLength *= i;
                        Core.InnerLoops./*!HC:innerloopname*/GetLimitsNoInf.Double.Strided64(
                            pIn, tmpbuffer + i * 2,
                            workItemLength, outLen - workItemLength,
                            ordered_bsd);

                        // wait & tune the overhead of threading
                        if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                            Interlocked.Increment(ref InnerLoops./*!HC:innerloopname*/GetLimitsNoInf.Double.s_threading_overhead_Strided64);
                        } else {
                            Interlocked.Decrement(ref InnerLoops./*!HC:innerloopname*/GetLimitsNoInf.Double.s_threading_overhead_Strided64);
                        }
                        #endregion

                    } else {

                        #region strides are uint* 
                        uint* ordered_bsd = stackalloc uint[(int)ndims * 2 + 3];
                        Core.Global.Helper.PrepareBSD(StorageOrders.Other, bsd, ordered_bsd, (uint)sizeof(double));

                        Helper.determineMultithreadingParameters(numel,
                                    ref InnerLoops./*!HC:innerloopname*/GetLimitsNoInf.Double.s_threading_overhead_Strided32,
                                    out workItemCount, out workItemLength);

                        var tmpbuffer = stackalloc /*!HC:outArr*/double[(int)workItemCount * 2];
                        accum_buffer = tmpbuffer;

                        if (workItemCount-- > 1) {

                            IntPtr* parameters = stackalloc IntPtr[5 + (int)workItemCount * 2];
                            parameters[0] = (IntPtr)pIn;
                            parameters[1] = (IntPtr)tmpbuffer;
                            parameters[2] = (IntPtr)ordered_bsd;
                            parameters[3] = (IntPtr)workItemLength;
                            parameters[4] = (IntPtr)(&workerCount);
                            do {

                                System.Threading.Interlocked.Increment(ref workerCount);
                                IntPtr* locParam = parameters + 5 + i * 2;
                                locParam[0] = (IntPtr)parameters;
                                locParam[1] = (IntPtr)i;
                                Core.Global.ThreadPool.QueueUserWorkItem(
                                    (uint)i,
                                    Core.InnerLoops./*!HC:innerloopname*/GetLimitsNoInf.Double.Strided32Action,
                                    (IntPtr)locParam);
                                i++;
                            } while (i < workItemCount); 
                        }
                        workItemLength *= i;
                        Core.InnerLoops./*!HC:innerloopname*/GetLimitsNoInf.Double.Strided32(
                                    pIn, tmpbuffer + i * 2,
                                    (uint)workItemLength, (uint)(outLen - workItemLength),
                                    ordered_bsd);
                        // wait & tune the overhead of threading
                        if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                            Interlocked.Increment(ref InnerLoops./*!HC:innerloopname*/GetLimitsNoInf.Double.s_threading_overhead_Strided32);
                        } else {
                            Interlocked.Decrement(ref InnerLoops./*!HC:innerloopname*/GetLimitsNoInf.Double.s_threading_overhead_Strided32);
                        }
                        #endregion
                    }

                    #endregion strided
                }
                #endregion

            } else {

                #region inifinity is considered as regular value
                if (storage.S.IsContinuous) {

                    #region continous
                    Helper.determineMultithreadingParameters(numel,
                        ref InnerLoops./*!HC:innerloopname*/GetLimits.Double.s_threading_overhead_Cont64,
                        out workItemCount, out workItemLength);

                    var tmpbuffer = stackalloc /*!HC:outArr*/double[(int)workItemCount * 2];
                    accum_buffer = tmpbuffer;

                    // continous
                    if (workItemCount-- > 1) {

                        IntPtr* parameters = stackalloc IntPtr[4 + (int)workItemCount * 2];
                        parameters[0] = (IntPtr)pIn;
                        parameters[1] = (IntPtr)tmpbuffer;
                        parameters[2] = (IntPtr)workItemLength;
                        parameters[3] = (IntPtr)(&workerCount);
                        do {

                            System.Threading.Interlocked.Increment(ref workerCount);
                            IntPtr* locParam = parameters + 4 + i * 2;
                            locParam[0] = (IntPtr)parameters;
                            locParam[1] = (IntPtr)i;
                            Core.Global.ThreadPool.QueueUserWorkItem(
                                (uint)i,
                                Core.InnerLoops./*!HC:innerloopname*/GetLimits.Double.Continous64OOPAction,
                                (IntPtr)locParam);
                            i++;
                        } while (i < workItemCount);
                    }
                    workItemLength *= i;
                    Core.InnerLoops./*!HC:innerloopname*/GetLimits.Double.Continous64OOP(
                        pIn + workItemLength * (uint)sizeof(double),
                        tmpbuffer + i * 2,
                        outLen - workItemLength);

                    // wait & tune the overhead of threading
                    if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                        Interlocked.Increment(ref InnerLoops./*!HC:innerloopname*/GetLimits.Double.s_threading_overhead_Cont64);
                    } else {
                        Interlocked.Decrement(ref InnerLoops./*!HC:innerloopname*/GetLimits.Double.s_threading_overhead_Cont64);
                    }
                    #endregion

                } else {

                    #region strided
                    // strided
                    var bsd = storage.S.GetBSD(write: false);
                    var ndims = (uint)bsd[0];
#if TEST_1000ASUINTMAXVALUE
                    if (storage.S.GetElementSpan() >= 1000) {
#else
                    if (Environment.Is64BitProcess && storage.S.GetElementSpan() * (uint)sizeof(double) > uint.MaxValue) {
#endif
                        #region strides are ulong* 
                        long* ordered_bsd = stackalloc long[(int)ndims * 2 + 3];

                        Core.Global.Helper.PrepareBSD(StorageOrders.Other, bsd, ordered_bsd, (uint)sizeof(double));

                        Helper.determineMultithreadingParameters(numel,
                                    ref InnerLoops./*!HC:innerloopname*/GetLimits.Double.s_threading_overhead_Strided64,
                                    out workItemCount, out workItemLength);

                        var tmpbuffer = stackalloc /*!HC:outArr*/double[(int)workItemCount * 2];
                        accum_buffer = tmpbuffer;

                        if (workItemCount-- > 1) {

                            IntPtr* parameters = stackalloc IntPtr[5 + (int)workItemCount * 2];
                            parameters[0] = (IntPtr)pIn;
                            parameters[1] = (IntPtr)tmpbuffer;
                            parameters[2] = (IntPtr)ordered_bsd;
                            parameters[3] = (IntPtr)workItemLength;
                            parameters[4] = (IntPtr)(&workerCount);
                            do {

                                System.Threading.Interlocked.Increment(ref workerCount);
                                IntPtr* locParam = parameters + 5 + i * 2;
                                locParam[0] = (IntPtr)parameters;
                                locParam[1] = (IntPtr)i;
                                Core.Global.ThreadPool.QueueUserWorkItem(
                                    (uint)i,
                                    Core.InnerLoops./*!HC:innerloopname*/GetLimits.Double.Strided64Action,
                                    (IntPtr)locParam);
                                i++;
                            } while (i < workItemCount);
                        }
                        workItemLength *= i;
                        Core.InnerLoops./*!HC:innerloopname*/GetLimits.Double.Strided64(
                            pIn, tmpbuffer + i * 2,
                            workItemLength, outLen - workItemLength,
                            ordered_bsd);

                        // wait & tune the overhead of threading
                        if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                            Interlocked.Increment(ref InnerLoops./*!HC:innerloopname*/GetLimits.Double.s_threading_overhead_Strided64);
                        } else {
                            Interlocked.Decrement(ref InnerLoops./*!HC:innerloopname*/GetLimits.Double.s_threading_overhead_Strided64);
                        }
                        #endregion

                    } else {

                        #region strides are uint* 
                        uint* ordered_bsd = stackalloc uint[(int)ndims * 2 + 3];
                        Core.Global.Helper.PrepareBSD(StorageOrders.Other, bsd, ordered_bsd, (uint)sizeof(double));

                        Helper.determineMultithreadingParameters(numel,
                                    ref InnerLoops./*!HC:innerloopname*/GetLimits.Double.s_threading_overhead_Strided32,
                                    out workItemCount, out workItemLength);

                        var tmpbuffer = stackalloc /*!HC:outArr*/double[(int)workItemCount * 2];
                        accum_buffer = tmpbuffer;

                        if (workItemCount-- > 1) {

                            IntPtr* parameters = stackalloc IntPtr[5 + (int)workItemCount * 2];
                            parameters[0] = (IntPtr)pIn;
                            parameters[1] = (IntPtr)tmpbuffer;
                            parameters[2] = (IntPtr)ordered_bsd;
                            parameters[3] = (IntPtr)workItemLength;
                            parameters[4] = (IntPtr)(&workerCount);
                            do {

                                System.Threading.Interlocked.Increment(ref workerCount);
                                IntPtr* locParam = parameters + 5 + i * 2;
                                locParam[0] = (IntPtr)parameters;
                                locParam[1] = (IntPtr)i;
                                Core.Global.ThreadPool.QueueUserWorkItem(
                                    (uint)i,
                                    Core.InnerLoops./*!HC:innerloopname*/GetLimits.Double.Strided32Action,
                                    (IntPtr)locParam);
                                i++;
                            } while (i < workItemCount);
                        }
                        workItemLength *= i;
                        Core.InnerLoops./*!HC:innerloopname*/GetLimits.Double.Strided32(
                                    pIn, tmpbuffer + i * 2,
                                    (uint)workItemLength, (uint)(outLen - workItemLength),
                                    ordered_bsd);
                        // wait & tune the overhead of threading
                        if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                            Interlocked.Increment(ref InnerLoops./*!HC:innerloopname*/GetLimits.Double.s_threading_overhead_Strided32);
                        } else {
                            Interlocked.Decrement(ref InnerLoops./*!HC:innerloopname*/GetLimits.Double.s_threading_overhead_Strided32);
                        }
                        #endregion
                    }

                    #endregion strided
                }
                #endregion

            }

            #region NANaccumulateEnd, consider NaN & Inf
            // find non-nan workchunk result. Note: enough to check min values in tmpbuffer! max will be NaN also.
            uint first = 0;
            workItemCount++; // (... had been decreased above for convenience)
            while (first < workItemCount && (double.IsNaN(*(double*)(accum_buffer + first * 2)))) { 
                                      // can not happen ?!:      || (ignoreInfinity && double.IsInfinity(*(double*)(tmpbuffer + first * 2))))) {
                first++;
            }
            if (first == workItemCount) {
                // all NaN or inf
                min = default(double); max = default(double);
                return false;
            } else {
                accum_buffer += first * 2;
                workItemCount -= first;
            }

            // local accumulate
            min = accum_buffer[0];
            max = accum_buffer[1];

            for (i = 1; i < workItemCount; i++) {
                if (
                    // cann not happen?!: (ignoreInfinity == false || !double.IsInfinity(tmpbuffer[i * 2])) && 
                    accum_buffer[i * 2] < min) {
                    min = accum_buffer[i * 2];
                }
                if (
                    // cannot happen!?: (ignoreInfinity == false || !double.IsInfinity(tmpbuffer[i * 2 + 1])) && 
                    accum_buffer[i * 2 + 1] > max) {
                    max = accum_buffer[i * 2 + 1];
                }
            }
            #endregion NANaccumulateEnd, consider NaN & Inf
            return true;
        }
        #endregion HYCALPER LOOPEND 
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 


        /// <summary>
        /// Finds the minimum and maximum values among all elements of <paramref name="storage"/>, optionally ignoring special floating point values.
        /// </summary>
        /// <param name="storage">Input storage.</param>
        /// <param name="min">[Output] The minimum value in <paramref name="storage"/>.</param>
        /// <param name="max">[Output] The maximum value in <paramref name="storage"/>.</param>
        /// <param name="ignoreInfinity">[Optional] When true only non-infinity and non-NaN values will be considered as regular values. Default: false.</param>
        /// <returns>True, if the minimum and maximum values could be computed. False otherwise.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input 
        /// <paramref name="storage"/> in a single run.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings 
        /// of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input <paramref name="storage"/> is not altered.</para>
        /// <para>The function always succeeds and returns <c>true</c> for non-empty arrays of integer 
        /// (non-floating point) element types. Overloads for floating point element types:
        /// the function succeeds on non-empty arrays which have at least one element other than NaN.</para>
        /// <para>The optional parameter <paramref name="ignoreInfinity"/> is useful, for example, to determine the 
        /// minimum and maximum values stored inside this storage in order to find scaling factors and for 
        /// normalization purposes. Here, <see cref="float.NegativeInfinity"/> and <see cref="float.PositiveInfinity"/>
        /// can be ignored since they would be not useful as scaling factors. For the default value ('false')
        /// infinity values are considered regular values and contribute to the result.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <paramref name="storage"/> is empty.</exception>
        
        internal unsafe static bool GetLimits (
            this BaseStorage<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> storage,
            out float min, out float max, bool ignoreInfinity = false) {

            var numel = storage.S.NumberOfElements;

            #region special input shapes
            if (numel == 0) {
                min = default(float); max = default(float);
                return false;
            } else if (numel == 1) {

                float a0 = storage.GetValue(0);

                if (float.IsNaN(a0)) { min = 0; max = 0; return false; }
                if (ignoreInfinity && float.IsInfinity(a0)) { min = 0; max = 0; return false; }
                min = a0;
                max = a0;
                return true;
            }
            #endregion

            byte* pIn = (byte*)storage.Handles[0].Pointer + storage.S.BaseOffset * (uint)sizeof(float);

            var outLen = storage.S.NumberOfElements;
            uint workItemCount;
            long workItemLength;
            int workerCount = 0;
            long i = 0;

           
            float* accum_buffer; 

            if (ignoreInfinity) {

                #region ignore infinity
                if (storage.S.IsContinuous) {

                    #region continous
                    Helper.determineMultithreadingParameters(numel,
                        ref InnerLoops.GetLimitsNoInf.Single.s_threading_overhead_Cont64,
                        out workItemCount, out workItemLength);

                    var tmpbuffer = stackalloc float[(int)workItemCount * 2];
                    accum_buffer = tmpbuffer;

                    // continous
                    if (workItemCount-- > 1) {

                        IntPtr* parameters = stackalloc IntPtr[4 + (int)workItemCount * 2];
                        parameters[0] = (IntPtr)pIn;
                        parameters[1] = (IntPtr)tmpbuffer;
                        parameters[2] = (IntPtr)workItemLength;
                        parameters[3] = (IntPtr)(&workerCount);
                        do {

                            System.Threading.Interlocked.Increment(ref workerCount);
                            IntPtr* locParam = parameters + 4 + i * 2;
                            locParam[0] = (IntPtr)parameters;
                            locParam[1] = (IntPtr)i;
                            Core.Global.ThreadPool.QueueUserWorkItem(
                                (uint)i,
                                Core.InnerLoops.GetLimitsNoInf.Single.Continous64OOPAction,
                                (IntPtr)locParam);
                            i++;
                        } while (i < workItemCount); 
                    }
                    workItemLength *= i;
                    Core.InnerLoops.GetLimitsNoInf.Single.Continous64OOP(
                        pIn + workItemLength * (uint)sizeof(float),
                        tmpbuffer + i * 2,
                        outLen - workItemLength);

                    // wait & tune the overhead of threading
                    if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                        Interlocked.Increment(ref InnerLoops.GetLimitsNoInf.Single.s_threading_overhead_Cont64);
                    } else {
                        Interlocked.Decrement(ref InnerLoops.GetLimitsNoInf.Single.s_threading_overhead_Cont64);
                    }
                    #endregion

                } else {

                    #region strided
                    // strided
                    var bsd = storage.S.GetBSD(write: false);
                    var ndims = (uint)bsd[0];
#if TEST_1000ASUINTMAXVALUE
                    if (storage.S.GetElementSpan() >= 1000) {
#else
                    if (Environment.Is64BitProcess && storage.S.GetElementSpan() * (uint)sizeof(float) > uint.MaxValue) {
#endif
                        #region strides are ulong* 
                        long* ordered_bsd = stackalloc long[(int)ndims * 2 + 3];

                        Core.Global.Helper.PrepareBSD(StorageOrders.Other, bsd, ordered_bsd, (uint)sizeof(float));

                        Helper.determineMultithreadingParameters(numel,
                                    ref InnerLoops.GetLimitsNoInf.Single.s_threading_overhead_Strided64,
                                    out workItemCount, out workItemLength);

                        var tmpbuffer = stackalloc float[(int)workItemCount * 2];
                        accum_buffer = tmpbuffer;

                        if (workItemCount-- > 1) {

                            IntPtr* parameters = stackalloc IntPtr[5 + (int)workItemCount * 2];
                            parameters[0] = (IntPtr)pIn;
                            parameters[1] = (IntPtr)tmpbuffer;
                            parameters[2] = (IntPtr)ordered_bsd;
                            parameters[3] = (IntPtr)workItemLength;
                            parameters[4] = (IntPtr)(&workerCount);
                            do {

                                System.Threading.Interlocked.Increment(ref workerCount);
                                IntPtr* locParam = parameters + 5 + i * 2;
                                locParam[0] = (IntPtr)parameters;
                                locParam[1] = (IntPtr)i;
                                Core.Global.ThreadPool.QueueUserWorkItem(
                                    (uint)i,
                                    Core.InnerLoops.GetLimitsNoInf.Single.Strided64Action,
                                    (IntPtr)locParam);
                                i++;
                            } while (i < workItemCount); 
                        }
                        workItemLength *= i;
                        Core.InnerLoops.GetLimitsNoInf.Single.Strided64(
                            pIn, tmpbuffer + i * 2,
                            workItemLength, outLen - workItemLength,
                            ordered_bsd);

                        // wait & tune the overhead of threading
                        if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                            Interlocked.Increment(ref InnerLoops.GetLimitsNoInf.Single.s_threading_overhead_Strided64);
                        } else {
                            Interlocked.Decrement(ref InnerLoops.GetLimitsNoInf.Single.s_threading_overhead_Strided64);
                        }
                        #endregion

                    } else {

                        #region strides are uint* 
                        uint* ordered_bsd = stackalloc uint[(int)ndims * 2 + 3];
                        Core.Global.Helper.PrepareBSD(StorageOrders.Other, bsd, ordered_bsd, (uint)sizeof(float));

                        Helper.determineMultithreadingParameters(numel,
                                    ref InnerLoops.GetLimitsNoInf.Single.s_threading_overhead_Strided32,
                                    out workItemCount, out workItemLength);

                        var tmpbuffer = stackalloc float[(int)workItemCount * 2];
                        accum_buffer = tmpbuffer;

                        if (workItemCount-- > 1) {

                            IntPtr* parameters = stackalloc IntPtr[5 + (int)workItemCount * 2];
                            parameters[0] = (IntPtr)pIn;
                            parameters[1] = (IntPtr)tmpbuffer;
                            parameters[2] = (IntPtr)ordered_bsd;
                            parameters[3] = (IntPtr)workItemLength;
                            parameters[4] = (IntPtr)(&workerCount);
                            do {

                                System.Threading.Interlocked.Increment(ref workerCount);
                                IntPtr* locParam = parameters + 5 + i * 2;
                                locParam[0] = (IntPtr)parameters;
                                locParam[1] = (IntPtr)i;
                                Core.Global.ThreadPool.QueueUserWorkItem(
                                    (uint)i,
                                    Core.InnerLoops.GetLimitsNoInf.Single.Strided32Action,
                                    (IntPtr)locParam);
                                i++;
                            } while (i < workItemCount); 
                        }
                        workItemLength *= i;
                        Core.InnerLoops.GetLimitsNoInf.Single.Strided32(
                                    pIn, tmpbuffer + i * 2,
                                    (uint)workItemLength, (uint)(outLen - workItemLength),
                                    ordered_bsd);
                        // wait & tune the overhead of threading
                        if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                            Interlocked.Increment(ref InnerLoops.GetLimitsNoInf.Single.s_threading_overhead_Strided32);
                        } else {
                            Interlocked.Decrement(ref InnerLoops.GetLimitsNoInf.Single.s_threading_overhead_Strided32);
                        }
                        #endregion
                    }

                    #endregion strided
                }
                #endregion

            } else {

                #region inifinity is considered as regular value
                if (storage.S.IsContinuous) {

                    #region continous
                    Helper.determineMultithreadingParameters(numel,
                        ref InnerLoops.GetLimits.Single.s_threading_overhead_Cont64,
                        out workItemCount, out workItemLength);

                    var tmpbuffer = stackalloc float[(int)workItemCount * 2];
                    accum_buffer = tmpbuffer;

                    // continous
                    if (workItemCount-- > 1) {

                        IntPtr* parameters = stackalloc IntPtr[4 + (int)workItemCount * 2];
                        parameters[0] = (IntPtr)pIn;
                        parameters[1] = (IntPtr)tmpbuffer;
                        parameters[2] = (IntPtr)workItemLength;
                        parameters[3] = (IntPtr)(&workerCount);
                        do {

                            System.Threading.Interlocked.Increment(ref workerCount);
                            IntPtr* locParam = parameters + 4 + i * 2;
                            locParam[0] = (IntPtr)parameters;
                            locParam[1] = (IntPtr)i;
                            Core.Global.ThreadPool.QueueUserWorkItem(
                                (uint)i,
                                Core.InnerLoops.GetLimits.Single.Continous64OOPAction,
                                (IntPtr)locParam);
                            i++;
                        } while (i < workItemCount);
                    }
                    workItemLength *= i;
                    Core.InnerLoops.GetLimits.Single.Continous64OOP(
                        pIn + workItemLength * (uint)sizeof(float),
                        tmpbuffer + i * 2,
                        outLen - workItemLength);

                    // wait & tune the overhead of threading
                    if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                        Interlocked.Increment(ref InnerLoops.GetLimits.Single.s_threading_overhead_Cont64);
                    } else {
                        Interlocked.Decrement(ref InnerLoops.GetLimits.Single.s_threading_overhead_Cont64);
                    }
                    #endregion

                } else {

                    #region strided
                    // strided
                    var bsd = storage.S.GetBSD(write: false);
                    var ndims = (uint)bsd[0];
#if TEST_1000ASUINTMAXVALUE
                    if (storage.S.GetElementSpan() >= 1000) {
#else
                    if (Environment.Is64BitProcess && storage.S.GetElementSpan() * (uint)sizeof(float) > uint.MaxValue) {
#endif
                        #region strides are ulong* 
                        long* ordered_bsd = stackalloc long[(int)ndims * 2 + 3];

                        Core.Global.Helper.PrepareBSD(StorageOrders.Other, bsd, ordered_bsd, (uint)sizeof(float));

                        Helper.determineMultithreadingParameters(numel,
                                    ref InnerLoops.GetLimits.Single.s_threading_overhead_Strided64,
                                    out workItemCount, out workItemLength);

                        var tmpbuffer = stackalloc float[(int)workItemCount * 2];
                        accum_buffer = tmpbuffer;

                        if (workItemCount-- > 1) {

                            IntPtr* parameters = stackalloc IntPtr[5 + (int)workItemCount * 2];
                            parameters[0] = (IntPtr)pIn;
                            parameters[1] = (IntPtr)tmpbuffer;
                            parameters[2] = (IntPtr)ordered_bsd;
                            parameters[3] = (IntPtr)workItemLength;
                            parameters[4] = (IntPtr)(&workerCount);
                            do {

                                System.Threading.Interlocked.Increment(ref workerCount);
                                IntPtr* locParam = parameters + 5 + i * 2;
                                locParam[0] = (IntPtr)parameters;
                                locParam[1] = (IntPtr)i;
                                Core.Global.ThreadPool.QueueUserWorkItem(
                                    (uint)i,
                                    Core.InnerLoops.GetLimits.Single.Strided64Action,
                                    (IntPtr)locParam);
                                i++;
                            } while (i < workItemCount);
                        }
                        workItemLength *= i;
                        Core.InnerLoops.GetLimits.Single.Strided64(
                            pIn, tmpbuffer + i * 2,
                            workItemLength, outLen - workItemLength,
                            ordered_bsd);

                        // wait & tune the overhead of threading
                        if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                            Interlocked.Increment(ref InnerLoops.GetLimits.Single.s_threading_overhead_Strided64);
                        } else {
                            Interlocked.Decrement(ref InnerLoops.GetLimits.Single.s_threading_overhead_Strided64);
                        }
                        #endregion

                    } else {

                        #region strides are uint* 
                        uint* ordered_bsd = stackalloc uint[(int)ndims * 2 + 3];
                        Core.Global.Helper.PrepareBSD(StorageOrders.Other, bsd, ordered_bsd, (uint)sizeof(float));

                        Helper.determineMultithreadingParameters(numel,
                                    ref InnerLoops.GetLimits.Single.s_threading_overhead_Strided32,
                                    out workItemCount, out workItemLength);

                        var tmpbuffer = stackalloc float[(int)workItemCount * 2];
                        accum_buffer = tmpbuffer;

                        if (workItemCount-- > 1) {

                            IntPtr* parameters = stackalloc IntPtr[5 + (int)workItemCount * 2];
                            parameters[0] = (IntPtr)pIn;
                            parameters[1] = (IntPtr)tmpbuffer;
                            parameters[2] = (IntPtr)ordered_bsd;
                            parameters[3] = (IntPtr)workItemLength;
                            parameters[4] = (IntPtr)(&workerCount);
                            do {

                                System.Threading.Interlocked.Increment(ref workerCount);
                                IntPtr* locParam = parameters + 5 + i * 2;
                                locParam[0] = (IntPtr)parameters;
                                locParam[1] = (IntPtr)i;
                                Core.Global.ThreadPool.QueueUserWorkItem(
                                    (uint)i,
                                    Core.InnerLoops.GetLimits.Single.Strided32Action,
                                    (IntPtr)locParam);
                                i++;
                            } while (i < workItemCount);
                        }
                        workItemLength *= i;
                        Core.InnerLoops.GetLimits.Single.Strided32(
                                    pIn, tmpbuffer + i * 2,
                                    (uint)workItemLength, (uint)(outLen - workItemLength),
                                    ordered_bsd);
                        // wait & tune the overhead of threading
                        if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                            Interlocked.Increment(ref InnerLoops.GetLimits.Single.s_threading_overhead_Strided32);
                        } else {
                            Interlocked.Decrement(ref InnerLoops.GetLimits.Single.s_threading_overhead_Strided32);
                        }
                        #endregion
                    }

                    #endregion strided
                }
                #endregion

            }

            #region NANaccumulateEnd, consider NaN & Inf
            // find non-nan workchunk result. Note: enough to check min values in tmpbuffer! max will be NaN also.
            uint first = 0;
            workItemCount++; // (... had been decreased above for convenience)
            while (first < workItemCount && (float.IsNaN(*(float*)(accum_buffer + first * 2)))) { 
                                      // can not happen ?!:      || (ignoreInfinity && float.IsInfinity(*(float*)(tmpbuffer + first * 2))))) {
                first++;
            }
            if (first == workItemCount) {
                // all NaN or inf
                min = default(float); max = default(float);
                return false;
            } else {
                accum_buffer += first * 2;
                workItemCount -= first;
            }

            // local accumulate
            min = accum_buffer[0];
            max = accum_buffer[1];

            for (i = 1; i < workItemCount; i++) {
                if (
                    // cann not happen?!: (ignoreInfinity == false || !float.IsInfinity(tmpbuffer[i * 2])) && 
                    accum_buffer[i * 2] < min) {
                    min = accum_buffer[i * 2];
                }
                if (
                    // cannot happen!?: (ignoreInfinity == false || !float.IsInfinity(tmpbuffer[i * 2 + 1])) && 
                    accum_buffer[i * 2 + 1] > max) {
                    max = accum_buffer[i * 2 + 1];
                }
            }
            #endregion NANaccumulateEnd, consider NaN & Inf
            return true;
        }

#endregion HYCALPER AUTO GENERATED CODE

        #region HYCALPER LOOPSTART Complex, FComplex, handling NaN & Inf
        /*!HC:TYPELIST:
        <hycalper>
        <type>
            <source locate="here">
                complex
            </source>
            <destination>fcomplex</destination>
        </type>
        <type>
            <source locate="here">
                Complex
            </source>
            <destination>FComplex</destination>
        </type>
        </hycalper>
        */

        /// <summary>
        /// Finds the minimum and maximum values among the real parts and the imaginary parts of all elements of <paramref name="storage"/>, optionally ignoring special floating point values.
        /// </summary>
        /// <param name="storage">Input storage.</param>
        /// <param name="min">[Output] The minimum value in <paramref name="storage"/>.</param>
        /// <param name="max">[Output] The maximum value in <paramref name="storage"/>.</param>
        /// <param name="ignoreInfinity">[Optional] When true only non-infinity and non-NaN values will be considered as regular values. Default: false.</param>
        /// <returns>True, if the minimum and maximum values could be computed. False otherwise.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input 
        /// <paramref name="storage"/> in a single run.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings 
        /// of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input <paramref name="storage"/> is not altered.</para>
        /// <para>The function always succeeds and returns <c>true</c> for non-empty arrays of integer 
        /// (non-floating point) element types. Overloads for floating point element types:
        /// the function succeeds on non-empty arrays which have at least one element other than NaN.</para>
        /// <para>The optional parameter <paramref name="ignoreInfinity"/> is useful, for example, to determine the 
        /// minimum and maximum values stored inside this storage in order to find scaling factors and for 
        /// normalization purposes. Here, <see cref="double.NegativeInfinity"/> and <see cref="double.PositiveInfinity"/>
        /// can be ignored since they would be not useful as scaling factors. For the default value ('false')
        /// infinity values are considered regular values and contribute to the result.</para>
        /// <para>If the method returns false the values of the real parts of the output parameters <paramref name="min"/> 
        /// and <paramref name="max"/> are <see cref="double.NaN"/> if no limit could have been computed for the real parts. Similarly, 
        /// the imaginary parts are <see cref="double.NaN"/> if no limit could be found among the imaginary parts. If only one valid range could 
        /// be found (either among the real parts or among the imaginary parts) the corresponding part of  <paramref name="min"/> 
        /// and <paramref name="max"/> carry the limits found and the other part will be <see cref="double.NaN"/>.</para> 
        /// </remarks>
        /// <exception cref="ArgumentException">If <paramref name="storage"/> is empty.</exception>
        
        internal unsafe static bool GetLimits(
            this BaseStorage<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> storage,
            out /*!HC:outArr*/complex min, out /*!HC:outArr*/complex max, bool ignoreInfinity = false) {

            var numel = storage.S.NumberOfElements;

            #region special input shapes
            if (numel == 0) {
                min = default(complex); max = default(complex);
                return false;
            } else if (numel == 1) {

                complex a0 = storage.GetValue(0);

                if (complex.IsNaN(a0)) { min = 0; max = 0; return false; }
                if (ignoreInfinity && complex.IsInfinity(a0)) { min = 0; max = 0; return false; }
                min = a0;
                max = a0;
                return true;
            }
            #endregion

            byte* pIn = (byte*)storage.Handles[0].Pointer + storage.S.BaseOffset * (uint)sizeof(complex);

            long outLen = storage.S.NumberOfElements;
            uint workItemCount;
            long workItemLength;
            int workerCount = 0;
            int i = 0;

            /*!HC:outArr*/
            complex* accum_buffer;

            if (ignoreInfinity) {

                #region ignore infinity
                if (storage.S.IsContinuous) {

                    #region continous
                    Helper.determineMultithreadingParameters(numel,
                        ref InnerLoops./*!HC:innerloopname*/GetLimitsNoInf.Complex.s_threading_overhead_Cont64,
                        out workItemCount, out workItemLength);

                    var tmpbuffer = stackalloc /*!HC:outArr*/complex[(int)workItemCount * 2];
                    accum_buffer = tmpbuffer;

                    // continous
                    if (workItemCount-- > 1) {

                        IntPtr* parameters = stackalloc IntPtr[4 + (int)workItemCount * 2];
                        parameters[0] = (IntPtr)pIn;
                        parameters[1] = (IntPtr)tmpbuffer;
                        parameters[2] = (IntPtr)workItemLength;
                        parameters[3] = (IntPtr)(&workerCount);
                        do {

                            System.Threading.Interlocked.Increment(ref workerCount);
                            IntPtr* locParam = parameters + 4 + i * 2;
                            locParam[0] = (IntPtr)parameters;
                            locParam[1] = (IntPtr)i;
                            Core.Global.ThreadPool.QueueUserWorkItem(
                                (uint)i,
                                Core.InnerLoops./*!HC:innerloopname*/GetLimitsNoInf.Complex.Continous64OOPAction,
                                (IntPtr)locParam);
                            i++;
                        } while (i < workItemCount);
                    }
                    workItemLength *= i;
                    Core.InnerLoops./*!HC:innerloopname*/GetLimitsNoInf.Complex.Continous64OOP(
                        pIn + workItemLength * (uint)sizeof(complex),
                        tmpbuffer + i * 2,
                        outLen - workItemLength);

                    // wait & tune the overhead of threading
                    if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                        Interlocked.Increment(ref InnerLoops./*!HC:innerloopname*/GetLimitsNoInf.Complex.s_threading_overhead_Cont64);
                    } else {
                        Interlocked.Decrement(ref InnerLoops./*!HC:innerloopname*/GetLimitsNoInf.Complex.s_threading_overhead_Cont64);
                    }
                    #endregion

                } else {

                    #region strided
                    // strided
                    var bsd = storage.S.GetBSD(write: false);
                    var ndims = (uint)bsd[0];
#if TEST_1000ASUINTMAXVALUE
                    if (storage.S.GetElementSpan() >= 1000) {
#else
                    if (Environment.Is64BitProcess && storage.S.GetElementSpan() * (uint)sizeof(complex) > uint.MaxValue) {
#endif
                        #region strides are ulong* 
                        long* ordered_bsd = stackalloc long[(int)ndims * 2 + 3];

                        Core.Global.Helper.PrepareBSD(StorageOrders.Other, bsd, ordered_bsd, (uint)sizeof(complex));

                        Helper.determineMultithreadingParameters(numel,
                                    ref InnerLoops./*!HC:innerloopname*/GetLimitsNoInf.Complex.s_threading_overhead_Strided64,
                                    out workItemCount, out workItemLength);

                        var tmpbuffer = stackalloc /*!HC:outArr*/complex[(int)workItemCount * 2];
                        accum_buffer = tmpbuffer;

                        if (workItemCount-- > 1) {

                            IntPtr* parameters = stackalloc IntPtr[5 + (int)workItemCount * 2];
                            parameters[0] = (IntPtr)pIn;
                            parameters[1] = (IntPtr)tmpbuffer;
                            parameters[2] = (IntPtr)ordered_bsd;
                            parameters[3] = (IntPtr)workItemLength;
                            parameters[4] = (IntPtr)(&workerCount);
                            do {

                                System.Threading.Interlocked.Increment(ref workerCount);
                                IntPtr* locParam = parameters + 5 + i * 2;
                                locParam[0] = (IntPtr)parameters;
                                locParam[1] = (IntPtr)i;
                                Core.Global.ThreadPool.QueueUserWorkItem(
                                    (uint)i,
                                    Core.InnerLoops./*!HC:innerloopname*/GetLimitsNoInf.Complex.Strided64Action,
                                    (IntPtr)locParam);
                                i++;
                            } while (i < workItemCount);
                        }
                        workItemLength *= i;
                        Core.InnerLoops./*!HC:innerloopname*/GetLimitsNoInf.Complex.Strided64(
                            pIn, tmpbuffer + i * 2,
                            workItemLength, outLen - workItemLength,
                            ordered_bsd);

                        // wait & tune the overhead of threading
                        if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                            Interlocked.Increment(ref InnerLoops./*!HC:innerloopname*/GetLimitsNoInf.Complex.s_threading_overhead_Strided64);
                        } else {
                            Interlocked.Decrement(ref InnerLoops./*!HC:innerloopname*/GetLimitsNoInf.Complex.s_threading_overhead_Strided64);
                        }
                        #endregion

                    } else {

                        #region strides are uint* 
                        uint* ordered_bsd = stackalloc uint[(int)ndims * 2 + 3];
                        Core.Global.Helper.PrepareBSD(StorageOrders.Other, bsd, ordered_bsd, (uint)sizeof(complex));

                        Helper.determineMultithreadingParameters(numel,
                                    ref InnerLoops./*!HC:innerloopname*/GetLimitsNoInf.Complex.s_threading_overhead_Strided32,
                                    out workItemCount, out workItemLength);

                        var tmpbuffer = stackalloc /*!HC:outArr*/complex[(int)workItemCount * 2];
                        accum_buffer = tmpbuffer;

                        if (workItemCount-- > 1) {

                            IntPtr* parameters = stackalloc IntPtr[5 + (int)workItemCount * 2];
                            parameters[0] = (IntPtr)pIn;
                            parameters[1] = (IntPtr)tmpbuffer;
                            parameters[2] = (IntPtr)ordered_bsd;
                            parameters[3] = (IntPtr)workItemLength;
                            parameters[4] = (IntPtr)(&workerCount);
                            do {

                                System.Threading.Interlocked.Increment(ref workerCount);
                                IntPtr* locParam = parameters + 5 + i * 2;
                                locParam[0] = (IntPtr)parameters;
                                locParam[1] = (IntPtr)i;
                                Core.Global.ThreadPool.QueueUserWorkItem(
                                    (uint)i,
                                    Core.InnerLoops./*!HC:innerloopname*/GetLimitsNoInf.Complex.Strided32Action,
                                    (IntPtr)locParam);
                                i++;
                            } while (i < workItemCount);
                        }
                        workItemLength *= i;
                        Core.InnerLoops./*!HC:innerloopname*/GetLimitsNoInf.Complex.Strided32(
                                    pIn, tmpbuffer + i * 2,
                                    (uint)workItemLength, (uint)(outLen - workItemLength),
                                    ordered_bsd);
                        // wait & tune the overhead of threading
                        if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                            Interlocked.Increment(ref InnerLoops./*!HC:innerloopname*/GetLimitsNoInf.Complex.s_threading_overhead_Strided32);
                        } else {
                            Interlocked.Decrement(ref InnerLoops./*!HC:innerloopname*/GetLimitsNoInf.Complex.s_threading_overhead_Strided32);
                        }
                        #endregion
                    }

                    #endregion strided
                }
                #endregion

            } else {

                #region inifinity is considered as regular value
                if (storage.S.IsContinuous) {

                    #region continous
                    Helper.determineMultithreadingParameters(numel,
                        ref InnerLoops./*!HC:innerloopname*/GetLimits.Complex.s_threading_overhead_Cont64,
                        out workItemCount, out workItemLength);

                    var tmpbuffer = stackalloc /*!HC:outArr*/complex[(int)workItemCount * 2];
                    accum_buffer = tmpbuffer;

                    // continous
                    if (workItemCount-- > 1) {

                        IntPtr* parameters = stackalloc IntPtr[4 + (int)workItemCount * 2];
                        parameters[0] = (IntPtr)pIn;
                        parameters[1] = (IntPtr)tmpbuffer;
                        parameters[2] = (IntPtr)workItemLength;
                        parameters[3] = (IntPtr)(&workerCount);
                        do {

                            System.Threading.Interlocked.Increment(ref workerCount);
                            IntPtr* locParam = parameters + 4 + i * 2;
                            locParam[0] = (IntPtr)parameters;
                            locParam[1] = (IntPtr)i;
                            Core.Global.ThreadPool.QueueUserWorkItem(
                                (uint)i,
                                Core.InnerLoops./*!HC:innerloopname*/GetLimits.Complex.Continous64OOPAction,
                                (IntPtr)locParam);
                            i++;
                        } while (i < workItemCount);
                    }
                    workItemLength *= i;
                    Core.InnerLoops./*!HC:innerloopname*/GetLimits.Complex.Continous64OOP(
                        pIn + workItemLength * (uint)sizeof(complex),
                        tmpbuffer + i * 2,
                        outLen - workItemLength);

                    // wait & tune the overhead of threading
                    if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                        Interlocked.Increment(ref InnerLoops./*!HC:innerloopname*/GetLimits.Complex.s_threading_overhead_Cont64);
                    } else {
                        Interlocked.Decrement(ref InnerLoops./*!HC:innerloopname*/GetLimits.Complex.s_threading_overhead_Cont64);
                    }
                    #endregion

                } else {

                    #region strided
                    // strided
                    var bsd = storage.S.GetBSD(write: false);
                    var ndims = (uint)bsd[0];
#if TEST_1000ASUINTMAXVALUE
                    if (storage.S.GetElementSpan() >= 1000) {
#else
                    if (Environment.Is64BitProcess && storage.S.GetElementSpan() * (uint)sizeof(complex) > uint.MaxValue) {
#endif
                        #region strides are ulong* 
                        long* ordered_bsd = stackalloc long[(int)ndims * 2 + 3];

                        Core.Global.Helper.PrepareBSD(StorageOrders.Other, bsd, ordered_bsd, (uint)sizeof(complex));

                        Helper.determineMultithreadingParameters(numel,
                                    ref InnerLoops./*!HC:innerloopname*/GetLimits.Complex.s_threading_overhead_Strided64,
                                    out workItemCount, out workItemLength);

                        var tmpbuffer = stackalloc /*!HC:outArr*/complex[(int)workItemCount * 2];
                        accum_buffer = tmpbuffer;

                        if (workItemCount-- > 1) {

                            IntPtr* parameters = stackalloc IntPtr[5 + (int)workItemCount * 2];
                            parameters[0] = (IntPtr)pIn;
                            parameters[1] = (IntPtr)tmpbuffer;
                            parameters[2] = (IntPtr)ordered_bsd;
                            parameters[3] = (IntPtr)workItemLength;
                            parameters[4] = (IntPtr)(&workerCount);
                            do {

                                System.Threading.Interlocked.Increment(ref workerCount);
                                IntPtr* locParam = parameters + 5 + i * 2;
                                locParam[0] = (IntPtr)parameters;
                                locParam[1] = (IntPtr)i;
                                Core.Global.ThreadPool.QueueUserWorkItem(
                                    (uint)i,
                                    Core.InnerLoops./*!HC:innerloopname*/GetLimits.Complex.Strided64Action,
                                    (IntPtr)locParam);
                                i++;
                            } while (i < workItemCount);
                        }
                        workItemLength *= i;
                        Core.InnerLoops./*!HC:innerloopname*/GetLimits.Complex.Strided64(
                            pIn, tmpbuffer + i * 2,
                            workItemLength, outLen - workItemLength,
                            ordered_bsd);

                        // wait & tune the overhead of threading
                        if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                            Interlocked.Increment(ref InnerLoops./*!HC:innerloopname*/GetLimits.Complex.s_threading_overhead_Strided64);
                        } else {
                            Interlocked.Decrement(ref InnerLoops./*!HC:innerloopname*/GetLimits.Complex.s_threading_overhead_Strided64);
                        }
                        #endregion

                    } else {

                        #region strides are uint* 
                        uint* ordered_bsd = stackalloc uint[(int)ndims * 2 + 3];
                        Core.Global.Helper.PrepareBSD(StorageOrders.Other, bsd, ordered_bsd, (uint)sizeof(complex));

                        Helper.determineMultithreadingParameters(numel,
                                    ref InnerLoops./*!HC:innerloopname*/GetLimits.Complex.s_threading_overhead_Strided32,
                                    out workItemCount, out workItemLength);

                        var tmpbuffer = stackalloc /*!HC:outArr*/complex[(int)workItemCount * 2];
                        accum_buffer = tmpbuffer;

                        if (workItemCount-- > 1) {

                            IntPtr* parameters = stackalloc IntPtr[5 + (int)workItemCount * 2];
                            parameters[0] = (IntPtr)pIn;
                            parameters[1] = (IntPtr)tmpbuffer;
                            parameters[2] = (IntPtr)ordered_bsd;
                            parameters[3] = (IntPtr)workItemLength;
                            parameters[4] = (IntPtr)(&workerCount);
                            do {

                                System.Threading.Interlocked.Increment(ref workerCount);
                                IntPtr* locParam = parameters + 5 + i * 2;
                                locParam[0] = (IntPtr)parameters;
                                locParam[1] = (IntPtr)i;
                                Core.Global.ThreadPool.QueueUserWorkItem(
                                    (uint)i,
                                    Core.InnerLoops./*!HC:innerloopname*/GetLimits.Complex.Strided32Action,
                                    (IntPtr)locParam);
                                i++;
                            } while (i < workItemCount);
                        }
                        workItemLength *= i;
                        Core.InnerLoops./*!HC:innerloopname*/GetLimits.Complex.Strided32(
                                    pIn, tmpbuffer + i * 2,
                                    (uint)workItemLength, (uint)(outLen - workItemLength),
                                    ordered_bsd);
                        // wait & tune the overhead of threading
                        if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                            Interlocked.Increment(ref InnerLoops./*!HC:innerloopname*/GetLimits.Complex.s_threading_overhead_Strided32);
                        } else {
                            Interlocked.Decrement(ref InnerLoops./*!HC:innerloopname*/GetLimits.Complex.s_threading_overhead_Strided32);
                        }
                        #endregion
                    }

                    #endregion strided
                }
                #endregion

            }
            // local accumulate
            min = accum_buffer[0];
            max = accum_buffer[1];
            workItemCount++; // (... had been decreased above for convenience)

            complex v;
            for (i = 1; i < workItemCount; i++) {
                // min value
                v = *(accum_buffer + i * 2);
                if (double.IsNaN(min.real) && (!double.IsNaN(v.real))) {
                    min.real = v.real;
                } else if (min.real < v.real) {
                    min.real = v.real;
                }
                if (double.IsNaN(min.imag) && (!double.IsNaN(v.imag))) {
                    min.imag = v.imag;
                } else if (min.imag < v.imag) {
                    min.imag = v.imag;
                }
                v = *(accum_buffer + i * 2 + 1);
                if (double.IsNaN(max.real) && (!double.IsNaN(v.real))) {
                    max.real = v.real;
                } else if (max.real < v.real) {
                    max.real = v.real;
                }
                if (double.IsNaN(max.imag) && (!double.IsNaN(v.imag))) {
                    max.imag = v.imag;
                } else if (max.imag < v.imag) {
                    max.imag = v.imag;
                }
            }
            return !complex.IsNaN(min) && !complex.IsNaN(max);
        }
        #endregion HYCALPER LOOPEND 
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        /// <summary>
        /// Finds the minimum and maximum values among the real parts and the imaginary parts of all elements of <paramref name="storage"/>, optionally ignoring special floating point values.
        /// </summary>
        /// <param name="storage">Input storage.</param>
        /// <param name="min">[Output] The minimum value in <paramref name="storage"/>.</param>
        /// <param name="max">[Output] The maximum value in <paramref name="storage"/>.</param>
        /// <param name="ignoreInfinity">[Optional] When true only non-infinity and non-NaN values will be considered as regular values. Default: false.</param>
        /// <returns>True, if the minimum and maximum values could be computed. False otherwise.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input 
        /// <paramref name="storage"/> in a single run.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings 
        /// of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input <paramref name="storage"/> is not altered.</para>
        /// <para>The function always succeeds and returns <c>true</c> for non-empty arrays of integer 
        /// (non-floating point) element types. Overloads for floating point element types:
        /// the function succeeds on non-empty arrays which have at least one element other than NaN.</para>
        /// <para>The optional parameter <paramref name="ignoreInfinity"/> is useful, for example, to determine the 
        /// minimum and maximum values stored inside this storage in order to find scaling factors and for 
        /// normalization purposes. Here, <see cref="double.NegativeInfinity"/> and <see cref="double.PositiveInfinity"/>
        /// can be ignored since they would be not useful as scaling factors. For the default value ('false')
        /// infinity values are considered regular values and contribute to the result.</para>
        /// <para>If the method returns false the values of the real parts of the output parameters <paramref name="min"/> 
        /// and <paramref name="max"/> are <see cref="double.NaN"/> if no limit could have been computed for the real parts. Similarly, 
        /// the imaginary parts are <see cref="double.NaN"/> if no limit could be found among the imaginary parts. If only one valid range could 
        /// be found (either among the real parts or among the imaginary parts) the corresponding part of  <paramref name="min"/> 
        /// and <paramref name="max"/> carry the limits found and the other part will be <see cref="double.NaN"/>.</para> 
        /// </remarks>
        /// <exception cref="ArgumentException">If <paramref name="storage"/> is empty.</exception>
        
        internal unsafe static bool GetLimits(
            this BaseStorage<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> storage,
            out fcomplex min, out fcomplex max, bool ignoreInfinity = false) {

            var numel = storage.S.NumberOfElements;

            #region special input shapes
            if (numel == 0) {
                min = default(fcomplex); max = default(fcomplex);
                return false;
            } else if (numel == 1) {

                fcomplex a0 = storage.GetValue(0);

                if (fcomplex.IsNaN(a0)) { min = 0; max = 0; return false; }
                if (ignoreInfinity && fcomplex.IsInfinity(a0)) { min = 0; max = 0; return false; }
                min = a0;
                max = a0;
                return true;
            }
            #endregion

            byte* pIn = (byte*)storage.Handles[0].Pointer + storage.S.BaseOffset * (uint)sizeof(fcomplex);

            long outLen = storage.S.NumberOfElements;
            uint workItemCount;
            long workItemLength;
            int workerCount = 0;
            int i = 0;

           
            fcomplex* accum_buffer;

            if (ignoreInfinity) {

                #region ignore infinity
                if (storage.S.IsContinuous) {

                    #region continous
                    Helper.determineMultithreadingParameters(numel,
                        ref InnerLoops.GetLimitsNoInf.FComplex.s_threading_overhead_Cont64,
                        out workItemCount, out workItemLength);

                    var tmpbuffer = stackalloc fcomplex[(int)workItemCount * 2];
                    accum_buffer = tmpbuffer;

                    // continous
                    if (workItemCount-- > 1) {

                        IntPtr* parameters = stackalloc IntPtr[4 + (int)workItemCount * 2];
                        parameters[0] = (IntPtr)pIn;
                        parameters[1] = (IntPtr)tmpbuffer;
                        parameters[2] = (IntPtr)workItemLength;
                        parameters[3] = (IntPtr)(&workerCount);
                        do {

                            System.Threading.Interlocked.Increment(ref workerCount);
                            IntPtr* locParam = parameters + 4 + i * 2;
                            locParam[0] = (IntPtr)parameters;
                            locParam[1] = (IntPtr)i;
                            Core.Global.ThreadPool.QueueUserWorkItem(
                                (uint)i,
                                Core.InnerLoops.GetLimitsNoInf.FComplex.Continous64OOPAction,
                                (IntPtr)locParam);
                            i++;
                        } while (i < workItemCount);
                    }
                    workItemLength *= i;
                    Core.InnerLoops.GetLimitsNoInf.FComplex.Continous64OOP(
                        pIn + workItemLength * (uint)sizeof(fcomplex),
                        tmpbuffer + i * 2,
                        outLen - workItemLength);

                    // wait & tune the overhead of threading
                    if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                        Interlocked.Increment(ref InnerLoops.GetLimitsNoInf.FComplex.s_threading_overhead_Cont64);
                    } else {
                        Interlocked.Decrement(ref InnerLoops.GetLimitsNoInf.FComplex.s_threading_overhead_Cont64);
                    }
                    #endregion

                } else {

                    #region strided
                    // strided
                    var bsd = storage.S.GetBSD(write: false);
                    var ndims = (uint)bsd[0];
#if TEST_1000ASUINTMAXVALUE
                    if (storage.S.GetElementSpan() >= 1000) {
#else
                    if (Environment.Is64BitProcess && storage.S.GetElementSpan() * (uint)sizeof(fcomplex) > uint.MaxValue) {
#endif
                        #region strides are ulong* 
                        long* ordered_bsd = stackalloc long[(int)ndims * 2 + 3];

                        Core.Global.Helper.PrepareBSD(StorageOrders.Other, bsd, ordered_bsd, (uint)sizeof(fcomplex));

                        Helper.determineMultithreadingParameters(numel,
                                    ref InnerLoops.GetLimitsNoInf.FComplex.s_threading_overhead_Strided64,
                                    out workItemCount, out workItemLength);

                        var tmpbuffer = stackalloc fcomplex[(int)workItemCount * 2];
                        accum_buffer = tmpbuffer;

                        if (workItemCount-- > 1) {

                            IntPtr* parameters = stackalloc IntPtr[5 + (int)workItemCount * 2];
                            parameters[0] = (IntPtr)pIn;
                            parameters[1] = (IntPtr)tmpbuffer;
                            parameters[2] = (IntPtr)ordered_bsd;
                            parameters[3] = (IntPtr)workItemLength;
                            parameters[4] = (IntPtr)(&workerCount);
                            do {

                                System.Threading.Interlocked.Increment(ref workerCount);
                                IntPtr* locParam = parameters + 5 + i * 2;
                                locParam[0] = (IntPtr)parameters;
                                locParam[1] = (IntPtr)i;
                                Core.Global.ThreadPool.QueueUserWorkItem(
                                    (uint)i,
                                    Core.InnerLoops.GetLimitsNoInf.FComplex.Strided64Action,
                                    (IntPtr)locParam);
                                i++;
                            } while (i < workItemCount);
                        }
                        workItemLength *= i;
                        Core.InnerLoops.GetLimitsNoInf.FComplex.Strided64(
                            pIn, tmpbuffer + i * 2,
                            workItemLength, outLen - workItemLength,
                            ordered_bsd);

                        // wait & tune the overhead of threading
                        if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                            Interlocked.Increment(ref InnerLoops.GetLimitsNoInf.FComplex.s_threading_overhead_Strided64);
                        } else {
                            Interlocked.Decrement(ref InnerLoops.GetLimitsNoInf.FComplex.s_threading_overhead_Strided64);
                        }
                        #endregion

                    } else {

                        #region strides are uint* 
                        uint* ordered_bsd = stackalloc uint[(int)ndims * 2 + 3];
                        Core.Global.Helper.PrepareBSD(StorageOrders.Other, bsd, ordered_bsd, (uint)sizeof(fcomplex));

                        Helper.determineMultithreadingParameters(numel,
                                    ref InnerLoops.GetLimitsNoInf.FComplex.s_threading_overhead_Strided32,
                                    out workItemCount, out workItemLength);

                        var tmpbuffer = stackalloc fcomplex[(int)workItemCount * 2];
                        accum_buffer = tmpbuffer;

                        if (workItemCount-- > 1) {

                            IntPtr* parameters = stackalloc IntPtr[5 + (int)workItemCount * 2];
                            parameters[0] = (IntPtr)pIn;
                            parameters[1] = (IntPtr)tmpbuffer;
                            parameters[2] = (IntPtr)ordered_bsd;
                            parameters[3] = (IntPtr)workItemLength;
                            parameters[4] = (IntPtr)(&workerCount);
                            do {

                                System.Threading.Interlocked.Increment(ref workerCount);
                                IntPtr* locParam = parameters + 5 + i * 2;
                                locParam[0] = (IntPtr)parameters;
                                locParam[1] = (IntPtr)i;
                                Core.Global.ThreadPool.QueueUserWorkItem(
                                    (uint)i,
                                    Core.InnerLoops.GetLimitsNoInf.FComplex.Strided32Action,
                                    (IntPtr)locParam);
                                i++;
                            } while (i < workItemCount);
                        }
                        workItemLength *= i;
                        Core.InnerLoops.GetLimitsNoInf.FComplex.Strided32(
                                    pIn, tmpbuffer + i * 2,
                                    (uint)workItemLength, (uint)(outLen - workItemLength),
                                    ordered_bsd);
                        // wait & tune the overhead of threading
                        if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                            Interlocked.Increment(ref InnerLoops.GetLimitsNoInf.FComplex.s_threading_overhead_Strided32);
                        } else {
                            Interlocked.Decrement(ref InnerLoops.GetLimitsNoInf.FComplex.s_threading_overhead_Strided32);
                        }
                        #endregion
                    }

                    #endregion strided
                }
                #endregion

            } else {

                #region inifinity is considered as regular value
                if (storage.S.IsContinuous) {

                    #region continous
                    Helper.determineMultithreadingParameters(numel,
                        ref InnerLoops.GetLimits.FComplex.s_threading_overhead_Cont64,
                        out workItemCount, out workItemLength);

                    var tmpbuffer = stackalloc fcomplex[(int)workItemCount * 2];
                    accum_buffer = tmpbuffer;

                    // continous
                    if (workItemCount-- > 1) {

                        IntPtr* parameters = stackalloc IntPtr[4 + (int)workItemCount * 2];
                        parameters[0] = (IntPtr)pIn;
                        parameters[1] = (IntPtr)tmpbuffer;
                        parameters[2] = (IntPtr)workItemLength;
                        parameters[3] = (IntPtr)(&workerCount);
                        do {

                            System.Threading.Interlocked.Increment(ref workerCount);
                            IntPtr* locParam = parameters + 4 + i * 2;
                            locParam[0] = (IntPtr)parameters;
                            locParam[1] = (IntPtr)i;
                            Core.Global.ThreadPool.QueueUserWorkItem(
                                (uint)i,
                                Core.InnerLoops.GetLimits.FComplex.Continous64OOPAction,
                                (IntPtr)locParam);
                            i++;
                        } while (i < workItemCount);
                    }
                    workItemLength *= i;
                    Core.InnerLoops.GetLimits.FComplex.Continous64OOP(
                        pIn + workItemLength * (uint)sizeof(fcomplex),
                        tmpbuffer + i * 2,
                        outLen - workItemLength);

                    // wait & tune the overhead of threading
                    if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                        Interlocked.Increment(ref InnerLoops.GetLimits.FComplex.s_threading_overhead_Cont64);
                    } else {
                        Interlocked.Decrement(ref InnerLoops.GetLimits.FComplex.s_threading_overhead_Cont64);
                    }
                    #endregion

                } else {

                    #region strided
                    // strided
                    var bsd = storage.S.GetBSD(write: false);
                    var ndims = (uint)bsd[0];
#if TEST_1000ASUINTMAXVALUE
                    if (storage.S.GetElementSpan() >= 1000) {
#else
                    if (Environment.Is64BitProcess && storage.S.GetElementSpan() * (uint)sizeof(fcomplex) > uint.MaxValue) {
#endif
                        #region strides are ulong* 
                        long* ordered_bsd = stackalloc long[(int)ndims * 2 + 3];

                        Core.Global.Helper.PrepareBSD(StorageOrders.Other, bsd, ordered_bsd, (uint)sizeof(fcomplex));

                        Helper.determineMultithreadingParameters(numel,
                                    ref InnerLoops.GetLimits.FComplex.s_threading_overhead_Strided64,
                                    out workItemCount, out workItemLength);

                        var tmpbuffer = stackalloc fcomplex[(int)workItemCount * 2];
                        accum_buffer = tmpbuffer;

                        if (workItemCount-- > 1) {

                            IntPtr* parameters = stackalloc IntPtr[5 + (int)workItemCount * 2];
                            parameters[0] = (IntPtr)pIn;
                            parameters[1] = (IntPtr)tmpbuffer;
                            parameters[2] = (IntPtr)ordered_bsd;
                            parameters[3] = (IntPtr)workItemLength;
                            parameters[4] = (IntPtr)(&workerCount);
                            do {

                                System.Threading.Interlocked.Increment(ref workerCount);
                                IntPtr* locParam = parameters + 5 + i * 2;
                                locParam[0] = (IntPtr)parameters;
                                locParam[1] = (IntPtr)i;
                                Core.Global.ThreadPool.QueueUserWorkItem(
                                    (uint)i,
                                    Core.InnerLoops.GetLimits.FComplex.Strided64Action,
                                    (IntPtr)locParam);
                                i++;
                            } while (i < workItemCount);
                        }
                        workItemLength *= i;
                        Core.InnerLoops.GetLimits.FComplex.Strided64(
                            pIn, tmpbuffer + i * 2,
                            workItemLength, outLen - workItemLength,
                            ordered_bsd);

                        // wait & tune the overhead of threading
                        if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                            Interlocked.Increment(ref InnerLoops.GetLimits.FComplex.s_threading_overhead_Strided64);
                        } else {
                            Interlocked.Decrement(ref InnerLoops.GetLimits.FComplex.s_threading_overhead_Strided64);
                        }
                        #endregion

                    } else {

                        #region strides are uint* 
                        uint* ordered_bsd = stackalloc uint[(int)ndims * 2 + 3];
                        Core.Global.Helper.PrepareBSD(StorageOrders.Other, bsd, ordered_bsd, (uint)sizeof(fcomplex));

                        Helper.determineMultithreadingParameters(numel,
                                    ref InnerLoops.GetLimits.FComplex.s_threading_overhead_Strided32,
                                    out workItemCount, out workItemLength);

                        var tmpbuffer = stackalloc fcomplex[(int)workItemCount * 2];
                        accum_buffer = tmpbuffer;

                        if (workItemCount-- > 1) {

                            IntPtr* parameters = stackalloc IntPtr[5 + (int)workItemCount * 2];
                            parameters[0] = (IntPtr)pIn;
                            parameters[1] = (IntPtr)tmpbuffer;
                            parameters[2] = (IntPtr)ordered_bsd;
                            parameters[3] = (IntPtr)workItemLength;
                            parameters[4] = (IntPtr)(&workerCount);
                            do {

                                System.Threading.Interlocked.Increment(ref workerCount);
                                IntPtr* locParam = parameters + 5 + i * 2;
                                locParam[0] = (IntPtr)parameters;
                                locParam[1] = (IntPtr)i;
                                Core.Global.ThreadPool.QueueUserWorkItem(
                                    (uint)i,
                                    Core.InnerLoops.GetLimits.FComplex.Strided32Action,
                                    (IntPtr)locParam);
                                i++;
                            } while (i < workItemCount);
                        }
                        workItemLength *= i;
                        Core.InnerLoops.GetLimits.FComplex.Strided32(
                                    pIn, tmpbuffer + i * 2,
                                    (uint)workItemLength, (uint)(outLen - workItemLength),
                                    ordered_bsd);
                        // wait & tune the overhead of threading
                        if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                            Interlocked.Increment(ref InnerLoops.GetLimits.FComplex.s_threading_overhead_Strided32);
                        } else {
                            Interlocked.Decrement(ref InnerLoops.GetLimits.FComplex.s_threading_overhead_Strided32);
                        }
                        #endregion
                    }

                    #endregion strided
                }
                #endregion

            }
            // local accumulate
            min = accum_buffer[0];
            max = accum_buffer[1];
            workItemCount++; // (... had been decreased above for convenience)

            fcomplex v;
            for (i = 1; i < workItemCount; i++) {
                // min value
                v = *(accum_buffer + i * 2);
                if (double.IsNaN(min.real) && (!double.IsNaN(v.real))) {
                    min.real = v.real;
                } else if (min.real < v.real) {
                    min.real = v.real;
                }
                if (double.IsNaN(min.imag) && (!double.IsNaN(v.imag))) {
                    min.imag = v.imag;
                } else if (min.imag < v.imag) {
                    min.imag = v.imag;
                }
                v = *(accum_buffer + i * 2 + 1);
                if (double.IsNaN(max.real) && (!double.IsNaN(v.real))) {
                    max.real = v.real;
                } else if (max.real < v.real) {
                    max.real = v.real;
                }
                if (double.IsNaN(max.imag) && (!double.IsNaN(v.imag))) {
                    max.imag = v.imag;
                } else if (max.imag < v.imag) {
                    max.imag = v.imag;
                }
            }
            return !fcomplex.IsNaN(min) && !fcomplex.IsNaN(max);
        }

#endregion HYCALPER AUTO GENERATED CODE

        #region HYCALPER LOOPSTART Integer types 
        /*!HC:TYPELIST:
        <hycalper>
        <type>
            <source locate="here">
                sbyte
            </source>
            <destination>int</destination>
            <destination>byte</destination>
            <destination>short</destination>
            <destination>ushort</destination>
            <destination>uint</destination>
            <destination>long</destination>
            <destination>ulong</destination>
        </type>
        <type>
            <source locate="here">
                Sbyte
            </source>
            <destination>Int32</destination>
            <destination>Byte</destination>
            <destination>Int16</destination>
            <destination>UInt16</destination>
            <destination>UInt32</destination>
            <destination>Int64</destination>
            <destination>UInt64</destination>
        </type>
        </hycalper>
        */

        /// <summary>
        /// Finds the minimum and maximum values among all elements of <paramref name="storage"/>.
        /// </summary>
        /// <param name="storage">Input storage.</param>
        /// <param name="min">[Output] The minimum value in <paramref name="storage"/>.</param>
        /// <param name="max">[Output] The maximum value in <paramref name="storage"/>.</param>
        /// <returns>True, if the minimum and maximum values could be computed. False otherwise.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input 
        /// <paramref name="storage"/> in a single run.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings 
        /// of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input <paramref name="storage"/> is not altered.</para>
        /// <para>The function always succeeds and returns <c>true</c> for non-empty arrays of integer 
        /// (non-floating point) element types. Overloads for floating point element types:
        /// the function succeeds on non-empty arrays which have at least one element other than NaN.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <paramref name="storage"/> is empty.</exception>
        
        internal unsafe static bool GetLimits(
            this BaseStorage<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> storage,
            out sbyte min, out sbyte max) {

            var numel = storage.S.NumberOfElements;

            #region special input shapes
            if (numel == 0) {
                min = default(sbyte); max = default(sbyte);
                return false;
            } else if (numel == 1) {

                sbyte a0 = storage.GetValue(0);



                min = a0;
                max = a0;
                return true;
            }
            #endregion

            byte* pIn = (byte*)storage.Handles[0].Pointer + storage.S.BaseOffset * (uint)sizeof(sbyte);

            long outLen = storage.S.NumberOfElements;
            uint workItemCount;
            long workItemLength;
            int workerCount = 0;
            int i = 0;

            /*!HC:outArr*/
            sbyte* accum_buffer;

            if (storage.S.IsContinuous) {

                #region continous
                Helper.determineMultithreadingParameters(numel,
                    ref InnerLoops./*!HC:innerloopname*/GetLimits.Sbyte.s_threading_overhead_Cont64,
                    out workItemCount, out workItemLength);

                var tmpbuffer = stackalloc /*!HC:outArr*/sbyte[(int)workItemCount * 2];
                accum_buffer = tmpbuffer;

                // continous
                if (workItemCount-- > 1) {

                    IntPtr* parameters = stackalloc IntPtr[4 + (int)workItemCount * 2];
                    parameters[0] = (IntPtr)pIn;
                    parameters[1] = (IntPtr)tmpbuffer;
                    parameters[2] = (IntPtr)workItemLength;
                    parameters[3] = (IntPtr)(&workerCount);
                    do {

                        System.Threading.Interlocked.Increment(ref workerCount);
                        IntPtr* locParam = parameters + 4 + i * 2;
                        locParam[0] = (IntPtr)parameters;
                        locParam[1] = (IntPtr)i;
                        Core.Global.ThreadPool.QueueUserWorkItem(
                            (uint)i,
                            Core.InnerLoops./*!HC:innerloopname*/GetLimits.Sbyte.Continous64OOPAction,
                            (IntPtr)locParam);
                        i++;
                    } while (i < workItemCount);
                }
                workItemLength *= i;
                Core.InnerLoops./*!HC:innerloopname*/GetLimits.Sbyte.Continous64OOP(
                    pIn + workItemLength * (uint)sizeof(sbyte),
                    tmpbuffer + i * 2,
                    outLen - workItemLength);

                // wait & tune the overhead of threading
                if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                    Interlocked.Increment(ref InnerLoops./*!HC:innerloopname*/GetLimits.Sbyte.s_threading_overhead_Cont64);
                } else {
                    Interlocked.Decrement(ref InnerLoops./*!HC:innerloopname*/GetLimits.Sbyte.s_threading_overhead_Cont64);
                }
                #endregion

            } else {

                #region strided
                // strided
                var bsd = storage.S.GetBSD(write: false);
                var ndims = (uint)bsd[0];
#if TEST_1000ASUINTMAXVALUE
                if (storage.S.GetElementSpan() >= 1000) {
#else
                    if (Environment.Is64BitProcess && storage.S.GetElementSpan() * (uint)sizeof(sbyte) > uint.MaxValue) {
#endif
                    #region strides are ulong* 
                    long* ordered_bsd = stackalloc long[(int)ndims * 2 + 3];

                    Core.Global.Helper.PrepareBSD(StorageOrders.Other, bsd, ordered_bsd, (uint)sizeof(sbyte));

                    Helper.determineMultithreadingParameters(numel,
                                ref InnerLoops./*!HC:innerloopname*/GetLimits.Sbyte.s_threading_overhead_Strided64,
                                out workItemCount, out workItemLength);

                    var tmpbuffer = stackalloc /*!HC:outArr*/sbyte[(int)workItemCount * 2];
                    accum_buffer = tmpbuffer;

                    if (workItemCount-- > 1) {

                        IntPtr* parameters = stackalloc IntPtr[5 + (int)workItemCount * 2];
                        parameters[0] = (IntPtr)pIn;
                        parameters[1] = (IntPtr)tmpbuffer;
                        parameters[2] = (IntPtr)ordered_bsd;
                        parameters[3] = (IntPtr)workItemLength;
                        parameters[4] = (IntPtr)(&workerCount);
                        do {

                            System.Threading.Interlocked.Increment(ref workerCount);
                            IntPtr* locParam = parameters + 5 + i * 2;
                            locParam[0] = (IntPtr)parameters;
                            locParam[1] = (IntPtr)i;
                            Core.Global.ThreadPool.QueueUserWorkItem(
                                (uint)i,
                                Core.InnerLoops./*!HC:innerloopname*/GetLimits.Sbyte.Strided64Action,
                                (IntPtr)locParam);
                            i++;
                        } while (i < workItemCount);
                    }
                    workItemLength *= i;
                    Core.InnerLoops./*!HC:innerloopname*/GetLimits.Sbyte.Strided64(
                        pIn, tmpbuffer + i * 2,
                        workItemLength, outLen - workItemLength,
                        ordered_bsd);

                    // wait & tune the overhead of threading
                    if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                        Interlocked.Increment(ref InnerLoops./*!HC:innerloopname*/GetLimits.Sbyte.s_threading_overhead_Strided64);
                    } else {
                        Interlocked.Decrement(ref InnerLoops./*!HC:innerloopname*/GetLimits.Sbyte.s_threading_overhead_Strided64);
                    }
                    #endregion

                } else {

                    #region strides are uint* 
                    uint* ordered_bsd = stackalloc uint[(int)ndims * 2 + 3];
                    Core.Global.Helper.PrepareBSD(StorageOrders.Other, bsd, ordered_bsd, (uint)sizeof(sbyte));

                    Helper.determineMultithreadingParameters(numel,
                                ref InnerLoops./*!HC:innerloopname*/GetLimits.Sbyte.s_threading_overhead_Strided32,
                                out workItemCount, out workItemLength);

                    var tmpbuffer = stackalloc /*!HC:outArr*/sbyte[(int)workItemCount * 2];
                    accum_buffer = tmpbuffer;

                    if (workItemCount-- > 1) {

                        IntPtr* parameters = stackalloc IntPtr[5 + (int)workItemCount * 2];
                        parameters[0] = (IntPtr)pIn;
                        parameters[1] = (IntPtr)tmpbuffer;
                        parameters[2] = (IntPtr)ordered_bsd;
                        parameters[3] = (IntPtr)workItemLength;
                        parameters[4] = (IntPtr)(&workerCount);
                        do {

                            System.Threading.Interlocked.Increment(ref workerCount);
                            IntPtr* locParam = parameters + 5 + i * 2;
                            locParam[0] = (IntPtr)parameters;
                            locParam[1] = (IntPtr)i;
                            Core.Global.ThreadPool.QueueUserWorkItem(
                                (uint)i,
                                Core.InnerLoops./*!HC:innerloopname*/GetLimits.Sbyte.Strided32Action,
                                (IntPtr)locParam);
                            i++;
                        } while (i < workItemCount);
                    }
                    workItemLength *= i;
                    Core.InnerLoops./*!HC:innerloopname*/GetLimits.Sbyte.Strided32(
                                pIn, tmpbuffer + i * 2,
                                (uint)workItemLength, (uint)(outLen - workItemLength),
                                ordered_bsd);
                    // wait & tune the overhead of threading
                    if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                        Interlocked.Increment(ref InnerLoops./*!HC:innerloopname*/GetLimits.Sbyte.s_threading_overhead_Strided32);
                    } else {
                        Interlocked.Decrement(ref InnerLoops./*!HC:innerloopname*/GetLimits.Sbyte.s_threading_overhead_Strided32);
                    }
                    #endregion
                }

                #endregion strided
            }

            #region local accumulate
            min = accum_buffer[0];
            max = accum_buffer[1];
            workItemCount++; // (... had been decreased above for convenience)

            for (i = 1; i < workItemCount; i++) {
                if (accum_buffer[i * 2] < min) {
                    min = accum_buffer[i * 2];
                }
                if (accum_buffer[i * 2 + 1] > max) {
                    max = accum_buffer[i * 2 + 1];
                }
            }
            #endregion
            return true;
        }

        #endregion HYCALPER LOOPEND Integer types
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        /// <summary>
        /// Finds the minimum and maximum values among all elements of <paramref name="storage"/>.
        /// </summary>
        /// <param name="storage">Input storage.</param>
        /// <param name="min">[Output] The minimum value in <paramref name="storage"/>.</param>
        /// <param name="max">[Output] The maximum value in <paramref name="storage"/>.</param>
        /// <returns>True, if the minimum and maximum values could be computed. False otherwise.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input 
        /// <paramref name="storage"/> in a single run.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings 
        /// of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input <paramref name="storage"/> is not altered.</para>
        /// <para>The function always succeeds and returns <c>true</c> for non-empty arrays of integer 
        /// (non-floating point) element types. Overloads for floating point element types:
        /// the function succeeds on non-empty arrays which have at least one element other than NaN.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <paramref name="storage"/> is empty.</exception>
        
        internal unsafe static bool GetLimits(
            this BaseStorage<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> storage,
            out ulong min, out ulong max) {

            var numel = storage.S.NumberOfElements;

            #region special input shapes
            if (numel == 0) {
                min = default(ulong); max = default(ulong);
                return false;
            } else if (numel == 1) {

                ulong a0 = storage.GetValue(0);



                min = a0;
                max = a0;
                return true;
            }
            #endregion

            byte* pIn = (byte*)storage.Handles[0].Pointer + storage.S.BaseOffset * (uint)sizeof(ulong);

            long outLen = storage.S.NumberOfElements;
            uint workItemCount;
            long workItemLength;
            int workerCount = 0;
            int i = 0;

           
            ulong* accum_buffer;

            if (storage.S.IsContinuous) {

                #region continous
                Helper.determineMultithreadingParameters(numel,
                    ref InnerLoops.GetLimits.UInt64.s_threading_overhead_Cont64,
                    out workItemCount, out workItemLength);

                var tmpbuffer = stackalloc ulong[(int)workItemCount * 2];
                accum_buffer = tmpbuffer;

                // continous
                if (workItemCount-- > 1) {

                    IntPtr* parameters = stackalloc IntPtr[4 + (int)workItemCount * 2];
                    parameters[0] = (IntPtr)pIn;
                    parameters[1] = (IntPtr)tmpbuffer;
                    parameters[2] = (IntPtr)workItemLength;
                    parameters[3] = (IntPtr)(&workerCount);
                    do {

                        System.Threading.Interlocked.Increment(ref workerCount);
                        IntPtr* locParam = parameters + 4 + i * 2;
                        locParam[0] = (IntPtr)parameters;
                        locParam[1] = (IntPtr)i;
                        Core.Global.ThreadPool.QueueUserWorkItem(
                            (uint)i,
                            Core.InnerLoops.GetLimits.UInt64.Continous64OOPAction,
                            (IntPtr)locParam);
                        i++;
                    } while (i < workItemCount);
                }
                workItemLength *= i;
                Core.InnerLoops.GetLimits.UInt64.Continous64OOP(
                    pIn + workItemLength * (uint)sizeof(ulong),
                    tmpbuffer + i * 2,
                    outLen - workItemLength);

                // wait & tune the overhead of threading
                if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                    Interlocked.Increment(ref InnerLoops.GetLimits.UInt64.s_threading_overhead_Cont64);
                } else {
                    Interlocked.Decrement(ref InnerLoops.GetLimits.UInt64.s_threading_overhead_Cont64);
                }
                #endregion

            } else {

                #region strided
                // strided
                var bsd = storage.S.GetBSD(write: false);
                var ndims = (uint)bsd[0];
#if TEST_1000ASUINTMAXVALUE
                if (storage.S.GetElementSpan() >= 1000) {
#else
                    if (Environment.Is64BitProcess && storage.S.GetElementSpan() * (uint)sizeof(ulong) > uint.MaxValue) {
#endif
                    #region strides are ulong* 
                    long* ordered_bsd = stackalloc long[(int)ndims * 2 + 3];

                    Core.Global.Helper.PrepareBSD(StorageOrders.Other, bsd, ordered_bsd, (uint)sizeof(ulong));

                    Helper.determineMultithreadingParameters(numel,
                                ref InnerLoops.GetLimits.UInt64.s_threading_overhead_Strided64,
                                out workItemCount, out workItemLength);

                    var tmpbuffer = stackalloc ulong[(int)workItemCount * 2];
                    accum_buffer = tmpbuffer;

                    if (workItemCount-- > 1) {

                        IntPtr* parameters = stackalloc IntPtr[5 + (int)workItemCount * 2];
                        parameters[0] = (IntPtr)pIn;
                        parameters[1] = (IntPtr)tmpbuffer;
                        parameters[2] = (IntPtr)ordered_bsd;
                        parameters[3] = (IntPtr)workItemLength;
                        parameters[4] = (IntPtr)(&workerCount);
                        do {

                            System.Threading.Interlocked.Increment(ref workerCount);
                            IntPtr* locParam = parameters + 5 + i * 2;
                            locParam[0] = (IntPtr)parameters;
                            locParam[1] = (IntPtr)i;
                            Core.Global.ThreadPool.QueueUserWorkItem(
                                (uint)i,
                                Core.InnerLoops.GetLimits.UInt64.Strided64Action,
                                (IntPtr)locParam);
                            i++;
                        } while (i < workItemCount);
                    }
                    workItemLength *= i;
                    Core.InnerLoops.GetLimits.UInt64.Strided64(
                        pIn, tmpbuffer + i * 2,
                        workItemLength, outLen - workItemLength,
                        ordered_bsd);

                    // wait & tune the overhead of threading
                    if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                        Interlocked.Increment(ref InnerLoops.GetLimits.UInt64.s_threading_overhead_Strided64);
                    } else {
                        Interlocked.Decrement(ref InnerLoops.GetLimits.UInt64.s_threading_overhead_Strided64);
                    }
                    #endregion

                } else {

                    #region strides are uint* 
                    uint* ordered_bsd = stackalloc uint[(int)ndims * 2 + 3];
                    Core.Global.Helper.PrepareBSD(StorageOrders.Other, bsd, ordered_bsd, (uint)sizeof(ulong));

                    Helper.determineMultithreadingParameters(numel,
                                ref InnerLoops.GetLimits.UInt64.s_threading_overhead_Strided32,
                                out workItemCount, out workItemLength);

                    var tmpbuffer = stackalloc ulong[(int)workItemCount * 2];
                    accum_buffer = tmpbuffer;

                    if (workItemCount-- > 1) {

                        IntPtr* parameters = stackalloc IntPtr[5 + (int)workItemCount * 2];
                        parameters[0] = (IntPtr)pIn;
                        parameters[1] = (IntPtr)tmpbuffer;
                        parameters[2] = (IntPtr)ordered_bsd;
                        parameters[3] = (IntPtr)workItemLength;
                        parameters[4] = (IntPtr)(&workerCount);
                        do {

                            System.Threading.Interlocked.Increment(ref workerCount);
                            IntPtr* locParam = parameters + 5 + i * 2;
                            locParam[0] = (IntPtr)parameters;
                            locParam[1] = (IntPtr)i;
                            Core.Global.ThreadPool.QueueUserWorkItem(
                                (uint)i,
                                Core.InnerLoops.GetLimits.UInt64.Strided32Action,
                                (IntPtr)locParam);
                            i++;
                        } while (i < workItemCount);
                    }
                    workItemLength *= i;
                    Core.InnerLoops.GetLimits.UInt64.Strided32(
                                pIn, tmpbuffer + i * 2,
                                (uint)workItemLength, (uint)(outLen - workItemLength),
                                ordered_bsd);
                    // wait & tune the overhead of threading
                    if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                        Interlocked.Increment(ref InnerLoops.GetLimits.UInt64.s_threading_overhead_Strided32);
                    } else {
                        Interlocked.Decrement(ref InnerLoops.GetLimits.UInt64.s_threading_overhead_Strided32);
                    }
                    #endregion
                }

                #endregion strided
            }

            #region local accumulate
            min = accum_buffer[0];
            max = accum_buffer[1];
            workItemCount++; // (... had been decreased above for convenience)

            for (i = 1; i < workItemCount; i++) {
                if (accum_buffer[i * 2] < min) {
                    min = accum_buffer[i * 2];
                }
                if (accum_buffer[i * 2 + 1] > max) {
                    max = accum_buffer[i * 2 + 1];
                }
            }
            #endregion
            return true;
        }

       

        /// <summary>
        /// Finds the minimum and maximum values among all elements of <paramref name="storage"/>.
        /// </summary>
        /// <param name="storage">Input storage.</param>
        /// <param name="min">[Output] The minimum value in <paramref name="storage"/>.</param>
        /// <param name="max">[Output] The maximum value in <paramref name="storage"/>.</param>
        /// <returns>True, if the minimum and maximum values could be computed. False otherwise.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input 
        /// <paramref name="storage"/> in a single run.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings 
        /// of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input <paramref name="storage"/> is not altered.</para>
        /// <para>The function always succeeds and returns <c>true</c> for non-empty arrays of integer 
        /// (non-floating point) element types. Overloads for floating point element types:
        /// the function succeeds on non-empty arrays which have at least one element other than NaN.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <paramref name="storage"/> is empty.</exception>
        
        internal unsafe static bool GetLimits(
            this BaseStorage<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> storage,
            out long min, out long max) {

            var numel = storage.S.NumberOfElements;

            #region special input shapes
            if (numel == 0) {
                min = default(long); max = default(long);
                return false;
            } else if (numel == 1) {

                long a0 = storage.GetValue(0);



                min = a0;
                max = a0;
                return true;
            }
            #endregion

            byte* pIn = (byte*)storage.Handles[0].Pointer + storage.S.BaseOffset * (uint)sizeof(long);

            long outLen = storage.S.NumberOfElements;
            uint workItemCount;
            long workItemLength;
            int workerCount = 0;
            int i = 0;

           
            long* accum_buffer;

            if (storage.S.IsContinuous) {

                #region continous
                Helper.determineMultithreadingParameters(numel,
                    ref InnerLoops.GetLimits.Int64.s_threading_overhead_Cont64,
                    out workItemCount, out workItemLength);

                var tmpbuffer = stackalloc long[(int)workItemCount * 2];
                accum_buffer = tmpbuffer;

                // continous
                if (workItemCount-- > 1) {

                    IntPtr* parameters = stackalloc IntPtr[4 + (int)workItemCount * 2];
                    parameters[0] = (IntPtr)pIn;
                    parameters[1] = (IntPtr)tmpbuffer;
                    parameters[2] = (IntPtr)workItemLength;
                    parameters[3] = (IntPtr)(&workerCount);
                    do {

                        System.Threading.Interlocked.Increment(ref workerCount);
                        IntPtr* locParam = parameters + 4 + i * 2;
                        locParam[0] = (IntPtr)parameters;
                        locParam[1] = (IntPtr)i;
                        Core.Global.ThreadPool.QueueUserWorkItem(
                            (uint)i,
                            Core.InnerLoops.GetLimits.Int64.Continous64OOPAction,
                            (IntPtr)locParam);
                        i++;
                    } while (i < workItemCount);
                }
                workItemLength *= i;
                Core.InnerLoops.GetLimits.Int64.Continous64OOP(
                    pIn + workItemLength * (uint)sizeof(long),
                    tmpbuffer + i * 2,
                    outLen - workItemLength);

                // wait & tune the overhead of threading
                if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                    Interlocked.Increment(ref InnerLoops.GetLimits.Int64.s_threading_overhead_Cont64);
                } else {
                    Interlocked.Decrement(ref InnerLoops.GetLimits.Int64.s_threading_overhead_Cont64);
                }
                #endregion

            } else {

                #region strided
                // strided
                var bsd = storage.S.GetBSD(write: false);
                var ndims = (uint)bsd[0];
#if TEST_1000ASUINTMAXVALUE
                if (storage.S.GetElementSpan() >= 1000) {
#else
                    if (Environment.Is64BitProcess && storage.S.GetElementSpan() * (uint)sizeof(long) > uint.MaxValue) {
#endif
                    #region strides are ulong* 
                    long* ordered_bsd = stackalloc long[(int)ndims * 2 + 3];

                    Core.Global.Helper.PrepareBSD(StorageOrders.Other, bsd, ordered_bsd, (uint)sizeof(long));

                    Helper.determineMultithreadingParameters(numel,
                                ref InnerLoops.GetLimits.Int64.s_threading_overhead_Strided64,
                                out workItemCount, out workItemLength);

                    var tmpbuffer = stackalloc long[(int)workItemCount * 2];
                    accum_buffer = tmpbuffer;

                    if (workItemCount-- > 1) {

                        IntPtr* parameters = stackalloc IntPtr[5 + (int)workItemCount * 2];
                        parameters[0] = (IntPtr)pIn;
                        parameters[1] = (IntPtr)tmpbuffer;
                        parameters[2] = (IntPtr)ordered_bsd;
                        parameters[3] = (IntPtr)workItemLength;
                        parameters[4] = (IntPtr)(&workerCount);
                        do {

                            System.Threading.Interlocked.Increment(ref workerCount);
                            IntPtr* locParam = parameters + 5 + i * 2;
                            locParam[0] = (IntPtr)parameters;
                            locParam[1] = (IntPtr)i;
                            Core.Global.ThreadPool.QueueUserWorkItem(
                                (uint)i,
                                Core.InnerLoops.GetLimits.Int64.Strided64Action,
                                (IntPtr)locParam);
                            i++;
                        } while (i < workItemCount);
                    }
                    workItemLength *= i;
                    Core.InnerLoops.GetLimits.Int64.Strided64(
                        pIn, tmpbuffer + i * 2,
                        workItemLength, outLen - workItemLength,
                        ordered_bsd);

                    // wait & tune the overhead of threading
                    if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                        Interlocked.Increment(ref InnerLoops.GetLimits.Int64.s_threading_overhead_Strided64);
                    } else {
                        Interlocked.Decrement(ref InnerLoops.GetLimits.Int64.s_threading_overhead_Strided64);
                    }
                    #endregion

                } else {

                    #region strides are uint* 
                    uint* ordered_bsd = stackalloc uint[(int)ndims * 2 + 3];
                    Core.Global.Helper.PrepareBSD(StorageOrders.Other, bsd, ordered_bsd, (uint)sizeof(long));

                    Helper.determineMultithreadingParameters(numel,
                                ref InnerLoops.GetLimits.Int64.s_threading_overhead_Strided32,
                                out workItemCount, out workItemLength);

                    var tmpbuffer = stackalloc long[(int)workItemCount * 2];
                    accum_buffer = tmpbuffer;

                    if (workItemCount-- > 1) {

                        IntPtr* parameters = stackalloc IntPtr[5 + (int)workItemCount * 2];
                        parameters[0] = (IntPtr)pIn;
                        parameters[1] = (IntPtr)tmpbuffer;
                        parameters[2] = (IntPtr)ordered_bsd;
                        parameters[3] = (IntPtr)workItemLength;
                        parameters[4] = (IntPtr)(&workerCount);
                        do {

                            System.Threading.Interlocked.Increment(ref workerCount);
                            IntPtr* locParam = parameters + 5 + i * 2;
                            locParam[0] = (IntPtr)parameters;
                            locParam[1] = (IntPtr)i;
                            Core.Global.ThreadPool.QueueUserWorkItem(
                                (uint)i,
                                Core.InnerLoops.GetLimits.Int64.Strided32Action,
                                (IntPtr)locParam);
                            i++;
                        } while (i < workItemCount);
                    }
                    workItemLength *= i;
                    Core.InnerLoops.GetLimits.Int64.Strided32(
                                pIn, tmpbuffer + i * 2,
                                (uint)workItemLength, (uint)(outLen - workItemLength),
                                ordered_bsd);
                    // wait & tune the overhead of threading
                    if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                        Interlocked.Increment(ref InnerLoops.GetLimits.Int64.s_threading_overhead_Strided32);
                    } else {
                        Interlocked.Decrement(ref InnerLoops.GetLimits.Int64.s_threading_overhead_Strided32);
                    }
                    #endregion
                }

                #endregion strided
            }

            #region local accumulate
            min = accum_buffer[0];
            max = accum_buffer[1];
            workItemCount++; // (... had been decreased above for convenience)

            for (i = 1; i < workItemCount; i++) {
                if (accum_buffer[i * 2] < min) {
                    min = accum_buffer[i * 2];
                }
                if (accum_buffer[i * 2 + 1] > max) {
                    max = accum_buffer[i * 2 + 1];
                }
            }
            #endregion
            return true;
        }

       

        /// <summary>
        /// Finds the minimum and maximum values among all elements of <paramref name="storage"/>.
        /// </summary>
        /// <param name="storage">Input storage.</param>
        /// <param name="min">[Output] The minimum value in <paramref name="storage"/>.</param>
        /// <param name="max">[Output] The maximum value in <paramref name="storage"/>.</param>
        /// <returns>True, if the minimum and maximum values could be computed. False otherwise.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input 
        /// <paramref name="storage"/> in a single run.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings 
        /// of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input <paramref name="storage"/> is not altered.</para>
        /// <para>The function always succeeds and returns <c>true</c> for non-empty arrays of integer 
        /// (non-floating point) element types. Overloads for floating point element types:
        /// the function succeeds on non-empty arrays which have at least one element other than NaN.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <paramref name="storage"/> is empty.</exception>
        
        internal unsafe static bool GetLimits(
            this BaseStorage<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> storage,
            out uint min, out uint max) {

            var numel = storage.S.NumberOfElements;

            #region special input shapes
            if (numel == 0) {
                min = default(uint); max = default(uint);
                return false;
            } else if (numel == 1) {

                uint a0 = storage.GetValue(0);



                min = a0;
                max = a0;
                return true;
            }
            #endregion

            byte* pIn = (byte*)storage.Handles[0].Pointer + storage.S.BaseOffset * (uint)sizeof(uint);

            long outLen = storage.S.NumberOfElements;
            uint workItemCount;
            long workItemLength;
            int workerCount = 0;
            int i = 0;

           
            uint* accum_buffer;

            if (storage.S.IsContinuous) {

                #region continous
                Helper.determineMultithreadingParameters(numel,
                    ref InnerLoops.GetLimits.UInt32.s_threading_overhead_Cont64,
                    out workItemCount, out workItemLength);

                var tmpbuffer = stackalloc uint[(int)workItemCount * 2];
                accum_buffer = tmpbuffer;

                // continous
                if (workItemCount-- > 1) {

                    IntPtr* parameters = stackalloc IntPtr[4 + (int)workItemCount * 2];
                    parameters[0] = (IntPtr)pIn;
                    parameters[1] = (IntPtr)tmpbuffer;
                    parameters[2] = (IntPtr)workItemLength;
                    parameters[3] = (IntPtr)(&workerCount);
                    do {

                        System.Threading.Interlocked.Increment(ref workerCount);
                        IntPtr* locParam = parameters + 4 + i * 2;
                        locParam[0] = (IntPtr)parameters;
                        locParam[1] = (IntPtr)i;
                        Core.Global.ThreadPool.QueueUserWorkItem(
                            (uint)i,
                            Core.InnerLoops.GetLimits.UInt32.Continous64OOPAction,
                            (IntPtr)locParam);
                        i++;
                    } while (i < workItemCount);
                }
                workItemLength *= i;
                Core.InnerLoops.GetLimits.UInt32.Continous64OOP(
                    pIn + workItemLength * (uint)sizeof(uint),
                    tmpbuffer + i * 2,
                    outLen - workItemLength);

                // wait & tune the overhead of threading
                if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                    Interlocked.Increment(ref InnerLoops.GetLimits.UInt32.s_threading_overhead_Cont64);
                } else {
                    Interlocked.Decrement(ref InnerLoops.GetLimits.UInt32.s_threading_overhead_Cont64);
                }
                #endregion

            } else {

                #region strided
                // strided
                var bsd = storage.S.GetBSD(write: false);
                var ndims = (uint)bsd[0];
#if TEST_1000ASUINTMAXVALUE
                if (storage.S.GetElementSpan() >= 1000) {
#else
                    if (Environment.Is64BitProcess && storage.S.GetElementSpan() * (uint)sizeof(uint) > uint.MaxValue) {
#endif
                    #region strides are ulong* 
                    long* ordered_bsd = stackalloc long[(int)ndims * 2 + 3];

                    Core.Global.Helper.PrepareBSD(StorageOrders.Other, bsd, ordered_bsd, (uint)sizeof(uint));

                    Helper.determineMultithreadingParameters(numel,
                                ref InnerLoops.GetLimits.UInt32.s_threading_overhead_Strided64,
                                out workItemCount, out workItemLength);

                    var tmpbuffer = stackalloc uint[(int)workItemCount * 2];
                    accum_buffer = tmpbuffer;

                    if (workItemCount-- > 1) {

                        IntPtr* parameters = stackalloc IntPtr[5 + (int)workItemCount * 2];
                        parameters[0] = (IntPtr)pIn;
                        parameters[1] = (IntPtr)tmpbuffer;
                        parameters[2] = (IntPtr)ordered_bsd;
                        parameters[3] = (IntPtr)workItemLength;
                        parameters[4] = (IntPtr)(&workerCount);
                        do {

                            System.Threading.Interlocked.Increment(ref workerCount);
                            IntPtr* locParam = parameters + 5 + i * 2;
                            locParam[0] = (IntPtr)parameters;
                            locParam[1] = (IntPtr)i;
                            Core.Global.ThreadPool.QueueUserWorkItem(
                                (uint)i,
                                Core.InnerLoops.GetLimits.UInt32.Strided64Action,
                                (IntPtr)locParam);
                            i++;
                        } while (i < workItemCount);
                    }
                    workItemLength *= i;
                    Core.InnerLoops.GetLimits.UInt32.Strided64(
                        pIn, tmpbuffer + i * 2,
                        workItemLength, outLen - workItemLength,
                        ordered_bsd);

                    // wait & tune the overhead of threading
                    if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                        Interlocked.Increment(ref InnerLoops.GetLimits.UInt32.s_threading_overhead_Strided64);
                    } else {
                        Interlocked.Decrement(ref InnerLoops.GetLimits.UInt32.s_threading_overhead_Strided64);
                    }
                    #endregion

                } else {

                    #region strides are uint* 
                    uint* ordered_bsd = stackalloc uint[(int)ndims * 2 + 3];
                    Core.Global.Helper.PrepareBSD(StorageOrders.Other, bsd, ordered_bsd, (uint)sizeof(uint));

                    Helper.determineMultithreadingParameters(numel,
                                ref InnerLoops.GetLimits.UInt32.s_threading_overhead_Strided32,
                                out workItemCount, out workItemLength);

                    var tmpbuffer = stackalloc uint[(int)workItemCount * 2];
                    accum_buffer = tmpbuffer;

                    if (workItemCount-- > 1) {

                        IntPtr* parameters = stackalloc IntPtr[5 + (int)workItemCount * 2];
                        parameters[0] = (IntPtr)pIn;
                        parameters[1] = (IntPtr)tmpbuffer;
                        parameters[2] = (IntPtr)ordered_bsd;
                        parameters[3] = (IntPtr)workItemLength;
                        parameters[4] = (IntPtr)(&workerCount);
                        do {

                            System.Threading.Interlocked.Increment(ref workerCount);
                            IntPtr* locParam = parameters + 5 + i * 2;
                            locParam[0] = (IntPtr)parameters;
                            locParam[1] = (IntPtr)i;
                            Core.Global.ThreadPool.QueueUserWorkItem(
                                (uint)i,
                                Core.InnerLoops.GetLimits.UInt32.Strided32Action,
                                (IntPtr)locParam);
                            i++;
                        } while (i < workItemCount);
                    }
                    workItemLength *= i;
                    Core.InnerLoops.GetLimits.UInt32.Strided32(
                                pIn, tmpbuffer + i * 2,
                                (uint)workItemLength, (uint)(outLen - workItemLength),
                                ordered_bsd);
                    // wait & tune the overhead of threading
                    if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                        Interlocked.Increment(ref InnerLoops.GetLimits.UInt32.s_threading_overhead_Strided32);
                    } else {
                        Interlocked.Decrement(ref InnerLoops.GetLimits.UInt32.s_threading_overhead_Strided32);
                    }
                    #endregion
                }

                #endregion strided
            }

            #region local accumulate
            min = accum_buffer[0];
            max = accum_buffer[1];
            workItemCount++; // (... had been decreased above for convenience)

            for (i = 1; i < workItemCount; i++) {
                if (accum_buffer[i * 2] < min) {
                    min = accum_buffer[i * 2];
                }
                if (accum_buffer[i * 2 + 1] > max) {
                    max = accum_buffer[i * 2 + 1];
                }
            }
            #endregion
            return true;
        }

       

        /// <summary>
        /// Finds the minimum and maximum values among all elements of <paramref name="storage"/>.
        /// </summary>
        /// <param name="storage">Input storage.</param>
        /// <param name="min">[Output] The minimum value in <paramref name="storage"/>.</param>
        /// <param name="max">[Output] The maximum value in <paramref name="storage"/>.</param>
        /// <returns>True, if the minimum and maximum values could be computed. False otherwise.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input 
        /// <paramref name="storage"/> in a single run.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings 
        /// of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input <paramref name="storage"/> is not altered.</para>
        /// <para>The function always succeeds and returns <c>true</c> for non-empty arrays of integer 
        /// (non-floating point) element types. Overloads for floating point element types:
        /// the function succeeds on non-empty arrays which have at least one element other than NaN.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <paramref name="storage"/> is empty.</exception>
        
        internal unsafe static bool GetLimits(
            this BaseStorage<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> storage,
            out ushort min, out ushort max) {

            var numel = storage.S.NumberOfElements;

            #region special input shapes
            if (numel == 0) {
                min = default(ushort); max = default(ushort);
                return false;
            } else if (numel == 1) {

                ushort a0 = storage.GetValue(0);



                min = a0;
                max = a0;
                return true;
            }
            #endregion

            byte* pIn = (byte*)storage.Handles[0].Pointer + storage.S.BaseOffset * (uint)sizeof(ushort);

            long outLen = storage.S.NumberOfElements;
            uint workItemCount;
            long workItemLength;
            int workerCount = 0;
            int i = 0;

           
            ushort* accum_buffer;

            if (storage.S.IsContinuous) {

                #region continous
                Helper.determineMultithreadingParameters(numel,
                    ref InnerLoops.GetLimits.UInt16.s_threading_overhead_Cont64,
                    out workItemCount, out workItemLength);

                var tmpbuffer = stackalloc ushort[(int)workItemCount * 2];
                accum_buffer = tmpbuffer;

                // continous
                if (workItemCount-- > 1) {

                    IntPtr* parameters = stackalloc IntPtr[4 + (int)workItemCount * 2];
                    parameters[0] = (IntPtr)pIn;
                    parameters[1] = (IntPtr)tmpbuffer;
                    parameters[2] = (IntPtr)workItemLength;
                    parameters[3] = (IntPtr)(&workerCount);
                    do {

                        System.Threading.Interlocked.Increment(ref workerCount);
                        IntPtr* locParam = parameters + 4 + i * 2;
                        locParam[0] = (IntPtr)parameters;
                        locParam[1] = (IntPtr)i;
                        Core.Global.ThreadPool.QueueUserWorkItem(
                            (uint)i,
                            Core.InnerLoops.GetLimits.UInt16.Continous64OOPAction,
                            (IntPtr)locParam);
                        i++;
                    } while (i < workItemCount);
                }
                workItemLength *= i;
                Core.InnerLoops.GetLimits.UInt16.Continous64OOP(
                    pIn + workItemLength * (uint)sizeof(ushort),
                    tmpbuffer + i * 2,
                    outLen - workItemLength);

                // wait & tune the overhead of threading
                if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                    Interlocked.Increment(ref InnerLoops.GetLimits.UInt16.s_threading_overhead_Cont64);
                } else {
                    Interlocked.Decrement(ref InnerLoops.GetLimits.UInt16.s_threading_overhead_Cont64);
                }
                #endregion

            } else {

                #region strided
                // strided
                var bsd = storage.S.GetBSD(write: false);
                var ndims = (uint)bsd[0];
#if TEST_1000ASUINTMAXVALUE
                if (storage.S.GetElementSpan() >= 1000) {
#else
                    if (Environment.Is64BitProcess && storage.S.GetElementSpan() * (uint)sizeof(ushort) > uint.MaxValue) {
#endif
                    #region strides are ulong* 
                    long* ordered_bsd = stackalloc long[(int)ndims * 2 + 3];

                    Core.Global.Helper.PrepareBSD(StorageOrders.Other, bsd, ordered_bsd, (uint)sizeof(ushort));

                    Helper.determineMultithreadingParameters(numel,
                                ref InnerLoops.GetLimits.UInt16.s_threading_overhead_Strided64,
                                out workItemCount, out workItemLength);

                    var tmpbuffer = stackalloc ushort[(int)workItemCount * 2];
                    accum_buffer = tmpbuffer;

                    if (workItemCount-- > 1) {

                        IntPtr* parameters = stackalloc IntPtr[5 + (int)workItemCount * 2];
                        parameters[0] = (IntPtr)pIn;
                        parameters[1] = (IntPtr)tmpbuffer;
                        parameters[2] = (IntPtr)ordered_bsd;
                        parameters[3] = (IntPtr)workItemLength;
                        parameters[4] = (IntPtr)(&workerCount);
                        do {

                            System.Threading.Interlocked.Increment(ref workerCount);
                            IntPtr* locParam = parameters + 5 + i * 2;
                            locParam[0] = (IntPtr)parameters;
                            locParam[1] = (IntPtr)i;
                            Core.Global.ThreadPool.QueueUserWorkItem(
                                (uint)i,
                                Core.InnerLoops.GetLimits.UInt16.Strided64Action,
                                (IntPtr)locParam);
                            i++;
                        } while (i < workItemCount);
                    }
                    workItemLength *= i;
                    Core.InnerLoops.GetLimits.UInt16.Strided64(
                        pIn, tmpbuffer + i * 2,
                        workItemLength, outLen - workItemLength,
                        ordered_bsd);

                    // wait & tune the overhead of threading
                    if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                        Interlocked.Increment(ref InnerLoops.GetLimits.UInt16.s_threading_overhead_Strided64);
                    } else {
                        Interlocked.Decrement(ref InnerLoops.GetLimits.UInt16.s_threading_overhead_Strided64);
                    }
                    #endregion

                } else {

                    #region strides are uint* 
                    uint* ordered_bsd = stackalloc uint[(int)ndims * 2 + 3];
                    Core.Global.Helper.PrepareBSD(StorageOrders.Other, bsd, ordered_bsd, (uint)sizeof(ushort));

                    Helper.determineMultithreadingParameters(numel,
                                ref InnerLoops.GetLimits.UInt16.s_threading_overhead_Strided32,
                                out workItemCount, out workItemLength);

                    var tmpbuffer = stackalloc ushort[(int)workItemCount * 2];
                    accum_buffer = tmpbuffer;

                    if (workItemCount-- > 1) {

                        IntPtr* parameters = stackalloc IntPtr[5 + (int)workItemCount * 2];
                        parameters[0] = (IntPtr)pIn;
                        parameters[1] = (IntPtr)tmpbuffer;
                        parameters[2] = (IntPtr)ordered_bsd;
                        parameters[3] = (IntPtr)workItemLength;
                        parameters[4] = (IntPtr)(&workerCount);
                        do {

                            System.Threading.Interlocked.Increment(ref workerCount);
                            IntPtr* locParam = parameters + 5 + i * 2;
                            locParam[0] = (IntPtr)parameters;
                            locParam[1] = (IntPtr)i;
                            Core.Global.ThreadPool.QueueUserWorkItem(
                                (uint)i,
                                Core.InnerLoops.GetLimits.UInt16.Strided32Action,
                                (IntPtr)locParam);
                            i++;
                        } while (i < workItemCount);
                    }
                    workItemLength *= i;
                    Core.InnerLoops.GetLimits.UInt16.Strided32(
                                pIn, tmpbuffer + i * 2,
                                (uint)workItemLength, (uint)(outLen - workItemLength),
                                ordered_bsd);
                    // wait & tune the overhead of threading
                    if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                        Interlocked.Increment(ref InnerLoops.GetLimits.UInt16.s_threading_overhead_Strided32);
                    } else {
                        Interlocked.Decrement(ref InnerLoops.GetLimits.UInt16.s_threading_overhead_Strided32);
                    }
                    #endregion
                }

                #endregion strided
            }

            #region local accumulate
            min = accum_buffer[0];
            max = accum_buffer[1];
            workItemCount++; // (... had been decreased above for convenience)

            for (i = 1; i < workItemCount; i++) {
                if (accum_buffer[i * 2] < min) {
                    min = accum_buffer[i * 2];
                }
                if (accum_buffer[i * 2 + 1] > max) {
                    max = accum_buffer[i * 2 + 1];
                }
            }
            #endregion
            return true;
        }

       

        /// <summary>
        /// Finds the minimum and maximum values among all elements of <paramref name="storage"/>.
        /// </summary>
        /// <param name="storage">Input storage.</param>
        /// <param name="min">[Output] The minimum value in <paramref name="storage"/>.</param>
        /// <param name="max">[Output] The maximum value in <paramref name="storage"/>.</param>
        /// <returns>True, if the minimum and maximum values could be computed. False otherwise.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input 
        /// <paramref name="storage"/> in a single run.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings 
        /// of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input <paramref name="storage"/> is not altered.</para>
        /// <para>The function always succeeds and returns <c>true</c> for non-empty arrays of integer 
        /// (non-floating point) element types. Overloads for floating point element types:
        /// the function succeeds on non-empty arrays which have at least one element other than NaN.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <paramref name="storage"/> is empty.</exception>
        
        internal unsafe static bool GetLimits(
            this BaseStorage<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> storage,
            out short min, out short max) {

            var numel = storage.S.NumberOfElements;

            #region special input shapes
            if (numel == 0) {
                min = default(short); max = default(short);
                return false;
            } else if (numel == 1) {

                short a0 = storage.GetValue(0);



                min = a0;
                max = a0;
                return true;
            }
            #endregion

            byte* pIn = (byte*)storage.Handles[0].Pointer + storage.S.BaseOffset * (uint)sizeof(short);

            long outLen = storage.S.NumberOfElements;
            uint workItemCount;
            long workItemLength;
            int workerCount = 0;
            int i = 0;

           
            short* accum_buffer;

            if (storage.S.IsContinuous) {

                #region continous
                Helper.determineMultithreadingParameters(numel,
                    ref InnerLoops.GetLimits.Int16.s_threading_overhead_Cont64,
                    out workItemCount, out workItemLength);

                var tmpbuffer = stackalloc short[(int)workItemCount * 2];
                accum_buffer = tmpbuffer;

                // continous
                if (workItemCount-- > 1) {

                    IntPtr* parameters = stackalloc IntPtr[4 + (int)workItemCount * 2];
                    parameters[0] = (IntPtr)pIn;
                    parameters[1] = (IntPtr)tmpbuffer;
                    parameters[2] = (IntPtr)workItemLength;
                    parameters[3] = (IntPtr)(&workerCount);
                    do {

                        System.Threading.Interlocked.Increment(ref workerCount);
                        IntPtr* locParam = parameters + 4 + i * 2;
                        locParam[0] = (IntPtr)parameters;
                        locParam[1] = (IntPtr)i;
                        Core.Global.ThreadPool.QueueUserWorkItem(
                            (uint)i,
                            Core.InnerLoops.GetLimits.Int16.Continous64OOPAction,
                            (IntPtr)locParam);
                        i++;
                    } while (i < workItemCount);
                }
                workItemLength *= i;
                Core.InnerLoops.GetLimits.Int16.Continous64OOP(
                    pIn + workItemLength * (uint)sizeof(short),
                    tmpbuffer + i * 2,
                    outLen - workItemLength);

                // wait & tune the overhead of threading
                if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                    Interlocked.Increment(ref InnerLoops.GetLimits.Int16.s_threading_overhead_Cont64);
                } else {
                    Interlocked.Decrement(ref InnerLoops.GetLimits.Int16.s_threading_overhead_Cont64);
                }
                #endregion

            } else {

                #region strided
                // strided
                var bsd = storage.S.GetBSD(write: false);
                var ndims = (uint)bsd[0];
#if TEST_1000ASUINTMAXVALUE
                if (storage.S.GetElementSpan() >= 1000) {
#else
                    if (Environment.Is64BitProcess && storage.S.GetElementSpan() * (uint)sizeof(short) > uint.MaxValue) {
#endif
                    #region strides are ulong* 
                    long* ordered_bsd = stackalloc long[(int)ndims * 2 + 3];

                    Core.Global.Helper.PrepareBSD(StorageOrders.Other, bsd, ordered_bsd, (uint)sizeof(short));

                    Helper.determineMultithreadingParameters(numel,
                                ref InnerLoops.GetLimits.Int16.s_threading_overhead_Strided64,
                                out workItemCount, out workItemLength);

                    var tmpbuffer = stackalloc short[(int)workItemCount * 2];
                    accum_buffer = tmpbuffer;

                    if (workItemCount-- > 1) {

                        IntPtr* parameters = stackalloc IntPtr[5 + (int)workItemCount * 2];
                        parameters[0] = (IntPtr)pIn;
                        parameters[1] = (IntPtr)tmpbuffer;
                        parameters[2] = (IntPtr)ordered_bsd;
                        parameters[3] = (IntPtr)workItemLength;
                        parameters[4] = (IntPtr)(&workerCount);
                        do {

                            System.Threading.Interlocked.Increment(ref workerCount);
                            IntPtr* locParam = parameters + 5 + i * 2;
                            locParam[0] = (IntPtr)parameters;
                            locParam[1] = (IntPtr)i;
                            Core.Global.ThreadPool.QueueUserWorkItem(
                                (uint)i,
                                Core.InnerLoops.GetLimits.Int16.Strided64Action,
                                (IntPtr)locParam);
                            i++;
                        } while (i < workItemCount);
                    }
                    workItemLength *= i;
                    Core.InnerLoops.GetLimits.Int16.Strided64(
                        pIn, tmpbuffer + i * 2,
                        workItemLength, outLen - workItemLength,
                        ordered_bsd);

                    // wait & tune the overhead of threading
                    if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                        Interlocked.Increment(ref InnerLoops.GetLimits.Int16.s_threading_overhead_Strided64);
                    } else {
                        Interlocked.Decrement(ref InnerLoops.GetLimits.Int16.s_threading_overhead_Strided64);
                    }
                    #endregion

                } else {

                    #region strides are uint* 
                    uint* ordered_bsd = stackalloc uint[(int)ndims * 2 + 3];
                    Core.Global.Helper.PrepareBSD(StorageOrders.Other, bsd, ordered_bsd, (uint)sizeof(short));

                    Helper.determineMultithreadingParameters(numel,
                                ref InnerLoops.GetLimits.Int16.s_threading_overhead_Strided32,
                                out workItemCount, out workItemLength);

                    var tmpbuffer = stackalloc short[(int)workItemCount * 2];
                    accum_buffer = tmpbuffer;

                    if (workItemCount-- > 1) {

                        IntPtr* parameters = stackalloc IntPtr[5 + (int)workItemCount * 2];
                        parameters[0] = (IntPtr)pIn;
                        parameters[1] = (IntPtr)tmpbuffer;
                        parameters[2] = (IntPtr)ordered_bsd;
                        parameters[3] = (IntPtr)workItemLength;
                        parameters[4] = (IntPtr)(&workerCount);
                        do {

                            System.Threading.Interlocked.Increment(ref workerCount);
                            IntPtr* locParam = parameters + 5 + i * 2;
                            locParam[0] = (IntPtr)parameters;
                            locParam[1] = (IntPtr)i;
                            Core.Global.ThreadPool.QueueUserWorkItem(
                                (uint)i,
                                Core.InnerLoops.GetLimits.Int16.Strided32Action,
                                (IntPtr)locParam);
                            i++;
                        } while (i < workItemCount);
                    }
                    workItemLength *= i;
                    Core.InnerLoops.GetLimits.Int16.Strided32(
                                pIn, tmpbuffer + i * 2,
                                (uint)workItemLength, (uint)(outLen - workItemLength),
                                ordered_bsd);
                    // wait & tune the overhead of threading
                    if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                        Interlocked.Increment(ref InnerLoops.GetLimits.Int16.s_threading_overhead_Strided32);
                    } else {
                        Interlocked.Decrement(ref InnerLoops.GetLimits.Int16.s_threading_overhead_Strided32);
                    }
                    #endregion
                }

                #endregion strided
            }

            #region local accumulate
            min = accum_buffer[0];
            max = accum_buffer[1];
            workItemCount++; // (... had been decreased above for convenience)

            for (i = 1; i < workItemCount; i++) {
                if (accum_buffer[i * 2] < min) {
                    min = accum_buffer[i * 2];
                }
                if (accum_buffer[i * 2 + 1] > max) {
                    max = accum_buffer[i * 2 + 1];
                }
            }
            #endregion
            return true;
        }

       

        /// <summary>
        /// Finds the minimum and maximum values among all elements of <paramref name="storage"/>.
        /// </summary>
        /// <param name="storage">Input storage.</param>
        /// <param name="min">[Output] The minimum value in <paramref name="storage"/>.</param>
        /// <param name="max">[Output] The maximum value in <paramref name="storage"/>.</param>
        /// <returns>True, if the minimum and maximum values could be computed. False otherwise.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input 
        /// <paramref name="storage"/> in a single run.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings 
        /// of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input <paramref name="storage"/> is not altered.</para>
        /// <para>The function always succeeds and returns <c>true</c> for non-empty arrays of integer 
        /// (non-floating point) element types. Overloads for floating point element types:
        /// the function succeeds on non-empty arrays which have at least one element other than NaN.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <paramref name="storage"/> is empty.</exception>
        
        internal unsafe static bool GetLimits(
            this BaseStorage<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> storage,
            out byte min, out byte max) {

            var numel = storage.S.NumberOfElements;

            #region special input shapes
            if (numel == 0) {
                min = default(byte); max = default(byte);
                return false;
            } else if (numel == 1) {

                byte a0 = storage.GetValue(0);



                min = a0;
                max = a0;
                return true;
            }
            #endregion

            byte* pIn = (byte*)storage.Handles[0].Pointer + storage.S.BaseOffset * (uint)sizeof(byte);

            long outLen = storage.S.NumberOfElements;
            uint workItemCount;
            long workItemLength;
            int workerCount = 0;
            int i = 0;

           
            byte* accum_buffer;

            if (storage.S.IsContinuous) {

                #region continous
                Helper.determineMultithreadingParameters(numel,
                    ref InnerLoops.GetLimits.Byte.s_threading_overhead_Cont64,
                    out workItemCount, out workItemLength);

                var tmpbuffer = stackalloc byte[(int)workItemCount * 2];
                accum_buffer = tmpbuffer;

                // continous
                if (workItemCount-- > 1) {

                    IntPtr* parameters = stackalloc IntPtr[4 + (int)workItemCount * 2];
                    parameters[0] = (IntPtr)pIn;
                    parameters[1] = (IntPtr)tmpbuffer;
                    parameters[2] = (IntPtr)workItemLength;
                    parameters[3] = (IntPtr)(&workerCount);
                    do {

                        System.Threading.Interlocked.Increment(ref workerCount);
                        IntPtr* locParam = parameters + 4 + i * 2;
                        locParam[0] = (IntPtr)parameters;
                        locParam[1] = (IntPtr)i;
                        Core.Global.ThreadPool.QueueUserWorkItem(
                            (uint)i,
                            Core.InnerLoops.GetLimits.Byte.Continous64OOPAction,
                            (IntPtr)locParam);
                        i++;
                    } while (i < workItemCount);
                }
                workItemLength *= i;
                Core.InnerLoops.GetLimits.Byte.Continous64OOP(
                    pIn + workItemLength * (uint)sizeof(byte),
                    tmpbuffer + i * 2,
                    outLen - workItemLength);

                // wait & tune the overhead of threading
                if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                    Interlocked.Increment(ref InnerLoops.GetLimits.Byte.s_threading_overhead_Cont64);
                } else {
                    Interlocked.Decrement(ref InnerLoops.GetLimits.Byte.s_threading_overhead_Cont64);
                }
                #endregion

            } else {

                #region strided
                // strided
                var bsd = storage.S.GetBSD(write: false);
                var ndims = (uint)bsd[0];
#if TEST_1000ASUINTMAXVALUE
                if (storage.S.GetElementSpan() >= 1000) {
#else
                    if (Environment.Is64BitProcess && storage.S.GetElementSpan() * (uint)sizeof(byte) > uint.MaxValue) {
#endif
                    #region strides are ulong* 
                    long* ordered_bsd = stackalloc long[(int)ndims * 2 + 3];

                    Core.Global.Helper.PrepareBSD(StorageOrders.Other, bsd, ordered_bsd, (uint)sizeof(byte));

                    Helper.determineMultithreadingParameters(numel,
                                ref InnerLoops.GetLimits.Byte.s_threading_overhead_Strided64,
                                out workItemCount, out workItemLength);

                    var tmpbuffer = stackalloc byte[(int)workItemCount * 2];
                    accum_buffer = tmpbuffer;

                    if (workItemCount-- > 1) {

                        IntPtr* parameters = stackalloc IntPtr[5 + (int)workItemCount * 2];
                        parameters[0] = (IntPtr)pIn;
                        parameters[1] = (IntPtr)tmpbuffer;
                        parameters[2] = (IntPtr)ordered_bsd;
                        parameters[3] = (IntPtr)workItemLength;
                        parameters[4] = (IntPtr)(&workerCount);
                        do {

                            System.Threading.Interlocked.Increment(ref workerCount);
                            IntPtr* locParam = parameters + 5 + i * 2;
                            locParam[0] = (IntPtr)parameters;
                            locParam[1] = (IntPtr)i;
                            Core.Global.ThreadPool.QueueUserWorkItem(
                                (uint)i,
                                Core.InnerLoops.GetLimits.Byte.Strided64Action,
                                (IntPtr)locParam);
                            i++;
                        } while (i < workItemCount);
                    }
                    workItemLength *= i;
                    Core.InnerLoops.GetLimits.Byte.Strided64(
                        pIn, tmpbuffer + i * 2,
                        workItemLength, outLen - workItemLength,
                        ordered_bsd);

                    // wait & tune the overhead of threading
                    if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                        Interlocked.Increment(ref InnerLoops.GetLimits.Byte.s_threading_overhead_Strided64);
                    } else {
                        Interlocked.Decrement(ref InnerLoops.GetLimits.Byte.s_threading_overhead_Strided64);
                    }
                    #endregion

                } else {

                    #region strides are uint* 
                    uint* ordered_bsd = stackalloc uint[(int)ndims * 2 + 3];
                    Core.Global.Helper.PrepareBSD(StorageOrders.Other, bsd, ordered_bsd, (uint)sizeof(byte));

                    Helper.determineMultithreadingParameters(numel,
                                ref InnerLoops.GetLimits.Byte.s_threading_overhead_Strided32,
                                out workItemCount, out workItemLength);

                    var tmpbuffer = stackalloc byte[(int)workItemCount * 2];
                    accum_buffer = tmpbuffer;

                    if (workItemCount-- > 1) {

                        IntPtr* parameters = stackalloc IntPtr[5 + (int)workItemCount * 2];
                        parameters[0] = (IntPtr)pIn;
                        parameters[1] = (IntPtr)tmpbuffer;
                        parameters[2] = (IntPtr)ordered_bsd;
                        parameters[3] = (IntPtr)workItemLength;
                        parameters[4] = (IntPtr)(&workerCount);
                        do {

                            System.Threading.Interlocked.Increment(ref workerCount);
                            IntPtr* locParam = parameters + 5 + i * 2;
                            locParam[0] = (IntPtr)parameters;
                            locParam[1] = (IntPtr)i;
                            Core.Global.ThreadPool.QueueUserWorkItem(
                                (uint)i,
                                Core.InnerLoops.GetLimits.Byte.Strided32Action,
                                (IntPtr)locParam);
                            i++;
                        } while (i < workItemCount);
                    }
                    workItemLength *= i;
                    Core.InnerLoops.GetLimits.Byte.Strided32(
                                pIn, tmpbuffer + i * 2,
                                (uint)workItemLength, (uint)(outLen - workItemLength),
                                ordered_bsd);
                    // wait & tune the overhead of threading
                    if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                        Interlocked.Increment(ref InnerLoops.GetLimits.Byte.s_threading_overhead_Strided32);
                    } else {
                        Interlocked.Decrement(ref InnerLoops.GetLimits.Byte.s_threading_overhead_Strided32);
                    }
                    #endregion
                }

                #endregion strided
            }

            #region local accumulate
            min = accum_buffer[0];
            max = accum_buffer[1];
            workItemCount++; // (... had been decreased above for convenience)

            for (i = 1; i < workItemCount; i++) {
                if (accum_buffer[i * 2] < min) {
                    min = accum_buffer[i * 2];
                }
                if (accum_buffer[i * 2 + 1] > max) {
                    max = accum_buffer[i * 2 + 1];
                }
            }
            #endregion
            return true;
        }

       

        /// <summary>
        /// Finds the minimum and maximum values among all elements of <paramref name="storage"/>.
        /// </summary>
        /// <param name="storage">Input storage.</param>
        /// <param name="min">[Output] The minimum value in <paramref name="storage"/>.</param>
        /// <param name="max">[Output] The maximum value in <paramref name="storage"/>.</param>
        /// <returns>True, if the minimum and maximum values could be computed. False otherwise.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input 
        /// <paramref name="storage"/> in a single run.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings 
        /// of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input <paramref name="storage"/> is not altered.</para>
        /// <para>The function always succeeds and returns <c>true</c> for non-empty arrays of integer 
        /// (non-floating point) element types. Overloads for floating point element types:
        /// the function succeeds on non-empty arrays which have at least one element other than NaN.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If <paramref name="storage"/> is empty.</exception>
        
        internal unsafe static bool GetLimits(
            this BaseStorage<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> storage,
            out int min, out int max) {

            var numel = storage.S.NumberOfElements;

            #region special input shapes
            if (numel == 0) {
                min = default(int); max = default(int);
                return false;
            } else if (numel == 1) {

                int a0 = storage.GetValue(0);



                min = a0;
                max = a0;
                return true;
            }
            #endregion

            byte* pIn = (byte*)storage.Handles[0].Pointer + storage.S.BaseOffset * (uint)sizeof(int);

            long outLen = storage.S.NumberOfElements;
            uint workItemCount;
            long workItemLength;
            int workerCount = 0;
            int i = 0;

           
            int* accum_buffer;

            if (storage.S.IsContinuous) {

                #region continous
                Helper.determineMultithreadingParameters(numel,
                    ref InnerLoops.GetLimits.Int32.s_threading_overhead_Cont64,
                    out workItemCount, out workItemLength);

                var tmpbuffer = stackalloc int[(int)workItemCount * 2];
                accum_buffer = tmpbuffer;

                // continous
                if (workItemCount-- > 1) {

                    IntPtr* parameters = stackalloc IntPtr[4 + (int)workItemCount * 2];
                    parameters[0] = (IntPtr)pIn;
                    parameters[1] = (IntPtr)tmpbuffer;
                    parameters[2] = (IntPtr)workItemLength;
                    parameters[3] = (IntPtr)(&workerCount);
                    do {

                        System.Threading.Interlocked.Increment(ref workerCount);
                        IntPtr* locParam = parameters + 4 + i * 2;
                        locParam[0] = (IntPtr)parameters;
                        locParam[1] = (IntPtr)i;
                        Core.Global.ThreadPool.QueueUserWorkItem(
                            (uint)i,
                            Core.InnerLoops.GetLimits.Int32.Continous64OOPAction,
                            (IntPtr)locParam);
                        i++;
                    } while (i < workItemCount);
                }
                workItemLength *= i;
                Core.InnerLoops.GetLimits.Int32.Continous64OOP(
                    pIn + workItemLength * (uint)sizeof(int),
                    tmpbuffer + i * 2,
                    outLen - workItemLength);

                // wait & tune the overhead of threading
                if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                    Interlocked.Increment(ref InnerLoops.GetLimits.Int32.s_threading_overhead_Cont64);
                } else {
                    Interlocked.Decrement(ref InnerLoops.GetLimits.Int32.s_threading_overhead_Cont64);
                }
                #endregion

            } else {

                #region strided
                // strided
                var bsd = storage.S.GetBSD(write: false);
                var ndims = (uint)bsd[0];
#if TEST_1000ASUINTMAXVALUE
                if (storage.S.GetElementSpan() >= 1000) {
#else
                    if (Environment.Is64BitProcess && storage.S.GetElementSpan() * (uint)sizeof(int) > uint.MaxValue) {
#endif
                    #region strides are ulong* 
                    long* ordered_bsd = stackalloc long[(int)ndims * 2 + 3];

                    Core.Global.Helper.PrepareBSD(StorageOrders.Other, bsd, ordered_bsd, (uint)sizeof(int));

                    Helper.determineMultithreadingParameters(numel,
                                ref InnerLoops.GetLimits.Int32.s_threading_overhead_Strided64,
                                out workItemCount, out workItemLength);

                    var tmpbuffer = stackalloc int[(int)workItemCount * 2];
                    accum_buffer = tmpbuffer;

                    if (workItemCount-- > 1) {

                        IntPtr* parameters = stackalloc IntPtr[5 + (int)workItemCount * 2];
                        parameters[0] = (IntPtr)pIn;
                        parameters[1] = (IntPtr)tmpbuffer;
                        parameters[2] = (IntPtr)ordered_bsd;
                        parameters[3] = (IntPtr)workItemLength;
                        parameters[4] = (IntPtr)(&workerCount);
                        do {

                            System.Threading.Interlocked.Increment(ref workerCount);
                            IntPtr* locParam = parameters + 5 + i * 2;
                            locParam[0] = (IntPtr)parameters;
                            locParam[1] = (IntPtr)i;
                            Core.Global.ThreadPool.QueueUserWorkItem(
                                (uint)i,
                                Core.InnerLoops.GetLimits.Int32.Strided64Action,
                                (IntPtr)locParam);
                            i++;
                        } while (i < workItemCount);
                    }
                    workItemLength *= i;
                    Core.InnerLoops.GetLimits.Int32.Strided64(
                        pIn, tmpbuffer + i * 2,
                        workItemLength, outLen - workItemLength,
                        ordered_bsd);

                    // wait & tune the overhead of threading
                    if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                        Interlocked.Increment(ref InnerLoops.GetLimits.Int32.s_threading_overhead_Strided64);
                    } else {
                        Interlocked.Decrement(ref InnerLoops.GetLimits.Int32.s_threading_overhead_Strided64);
                    }
                    #endregion

                } else {

                    #region strides are uint* 
                    uint* ordered_bsd = stackalloc uint[(int)ndims * 2 + 3];
                    Core.Global.Helper.PrepareBSD(StorageOrders.Other, bsd, ordered_bsd, (uint)sizeof(int));

                    Helper.determineMultithreadingParameters(numel,
                                ref InnerLoops.GetLimits.Int32.s_threading_overhead_Strided32,
                                out workItemCount, out workItemLength);

                    var tmpbuffer = stackalloc int[(int)workItemCount * 2];
                    accum_buffer = tmpbuffer;

                    if (workItemCount-- > 1) {

                        IntPtr* parameters = stackalloc IntPtr[5 + (int)workItemCount * 2];
                        parameters[0] = (IntPtr)pIn;
                        parameters[1] = (IntPtr)tmpbuffer;
                        parameters[2] = (IntPtr)ordered_bsd;
                        parameters[3] = (IntPtr)workItemLength;
                        parameters[4] = (IntPtr)(&workerCount);
                        do {

                            System.Threading.Interlocked.Increment(ref workerCount);
                            IntPtr* locParam = parameters + 5 + i * 2;
                            locParam[0] = (IntPtr)parameters;
                            locParam[1] = (IntPtr)i;
                            Core.Global.ThreadPool.QueueUserWorkItem(
                                (uint)i,
                                Core.InnerLoops.GetLimits.Int32.Strided32Action,
                                (IntPtr)locParam);
                            i++;
                        } while (i < workItemCount);
                    }
                    workItemLength *= i;
                    Core.InnerLoops.GetLimits.Int32.Strided32(
                                pIn, tmpbuffer + i * 2,
                                (uint)workItemLength, (uint)(outLen - workItemLength),
                                ordered_bsd);
                    // wait & tune the overhead of threading
                    if (Global.ThreadPool.Wait4Workers(ref workerCount) > 0) {
                        Interlocked.Increment(ref InnerLoops.GetLimits.Int32.s_threading_overhead_Strided32);
                    } else {
                        Interlocked.Decrement(ref InnerLoops.GetLimits.Int32.s_threading_overhead_Strided32);
                    }
                    #endregion
                }

                #endregion strided
            }

            #region local accumulate
            min = accum_buffer[0];
            max = accum_buffer[1];
            workItemCount++; // (... had been decreased above for convenience)

            for (i = 1; i < workItemCount; i++) {
                if (accum_buffer[i * 2] < min) {
                    min = accum_buffer[i * 2];
                }
                if (accum_buffer[i * 2 + 1] > max) {
                    max = accum_buffer[i * 2 + 1];
                }
            }
            #endregion
            return true;
        }


#endregion HYCALPER AUTO GENERATED CODE
    }
}

namespace ILNumerics.Core.InnerLoops.GetLimits {  // double, float, no special inf handling

    #region HYCALPER LOOPSTART
    /*!HC:TYPELIST:
    <hycalper>
    <type>
        <source locate="here">
            double
        </source>
        <destination>float</destination>
    </type>
    <type>
        <source locate="here">
            Double
        </source>
        <destination>Single</destination>
    </type>
    </hycalper>
    */

    internal static class Double {

        internal static int s_threading_overhead_Cont64 = 10;  
        internal static int s_threading_overhead_Strided32 = 10;  
        internal static int s_threading_overhead_Strided64 = 10;

        internal static unsafe Action<IntPtr> Continous64OOPAction = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = p[2].ToInt64();
            var i = locP[1].ToInt64();
            Continous64OOP((byte*)p[0] + len * (uint)sizeof(double) * i,
                            (/*!HC:outArr*/double*)p[1] + i * 2,
                            (long)len);
            Interlocked.Decrement(ref *(int*)p[3]);
        };
        internal static unsafe Action<IntPtr> Strided32Action = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = (uint)p[3].ToInt32();
            var i = (uint)locP[1].ToInt32();
            Strided32((byte*)p[0], (/*!HC:outArr*/double*)p[1] + i * 2, len * i, len, (uint*)p[2]);
            Interlocked.Decrement(ref *(int*)p[4]);
        };
        internal static unsafe Action<IntPtr> Strided64Action = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = (long)p[3].ToInt64();
            var i = (long)locP[1].ToInt64();
            Strided64((byte*)p[0], (/*!HC:outArr*/double*)p[1] + i * 2, len * i, len, (long*)p[2]);
            Interlocked.Decrement(ref *(int*)p[4]);
        };

        internal static unsafe void Continous64OOP(byte* pIn, /*!HC:outArr*/double* pOut, long len) {

            System.Diagnostics.Debug.Assert(pIn != (byte*)0);
            System.Diagnostics.Debug.Assert(pOut != (/*!HC:outArr*/double*)0);

            // floatingpoint types: find the first non-nan value
            while (len > 0 && (double.IsNaN(*(double*)pIn))) {
                pIn += sizeof(double);
                len--;
            }
            if (len == 0) {
                // all nans
                pOut[0] = double.NaN;
                pOut[1] = double.NaN;
                return;
            }

            double v, min = *(double*)pIn, max = min;

            while (len >= 8) {
                v = *(double*)(pIn + 0 * sizeof(double)); if (v < min) min = v; if (v > max) max = v;
                v = *(double*)(pIn + 1 * sizeof(double)); if (v < min) min = v; if (v > max) max = v;
                v = *(double*)(pIn + 2 * sizeof(double)); if (v < min) min = v; if (v > max) max = v;
                v = *(double*)(pIn + 3 * sizeof(double)); if (v < min) min = v; if (v > max) max = v;
                v = *(double*)(pIn + 4 * sizeof(double)); if (v < min) min = v; if (v > max) max = v;
                v = *(double*)(pIn + 5 * sizeof(double)); if (v < min) min = v; if (v > max) max = v;
                v = *(double*)(pIn + 6 * sizeof(double)); if (v < min) min = v; if (v > max) max = v;
                v = *(double*)(pIn + 7 * sizeof(double)); if (v < min) min = v; if (v > max) max = v;
                pIn += 8 * sizeof(double); len -= 8;
            }
            while (len-- > 0) {
                v = *(double*)pIn; if (v < min) min = v; if (v > max) max = v;
                pIn += sizeof(double);
            }
            pOut[0] = min;
            pOut[1] = max;
        }

        /// <summary>
        /// Inner ACCUMALL loop for strided, general (non-huge) array.
        /// </summary>
        /// <param name="pSrc">pointer to first element to consider here.</param>
        /// <param name="pOut">Pointer to single element to store result.</param>
        /// <param name="start">First sequential element.</param>
        /// <param name="len">Number of sequential elements to process.</param>
        /// <param name="ordered_bytestrided_bsd">(Re-)ordered BSD of the source array. Strides indicate <i>byte spacings</i>.</param>
        internal static unsafe void Strided32(byte* pSrc, /*!HC:outArr*/double* pOut, uint start, uint len, uint* ordered_bytestrided_bsd) {

            int ndims = (int)ordered_bytestrided_bsd[0];
            uint* dims = ordered_bytestrided_bsd + 3;
            uint* strides = dims + ndims;
            uint stride0 = strides[0];
            uint* cur = stackalloc uint[ndims];

            // figure out the dimension index position for start
            System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

            cur[0] = start % dims[0];
            uint f = start / dims[0];
            uint higdims = 0;
            int i = 1;
            for (; f > 0 && i < ndims; i++) {
                cur[i] = f % dims[i];
                f /= dims[i];
                higdims += cur[i] * strides[i];
            }
            while (i < ndims) {
                cur[i++] = 0;
            }

            System.Diagnostics.Debug.Assert(f == 0);

            double v;
            double min = double./*!HC:initMinValue*/NaN;
            double max = double./*!HC:initMaxValue*/NaN;

            while (true) {

                byte* pIn = pSrc + (higdims + cur[0] * stride0);

                // iteration length limited to either the dimension lengths or the end of the requested chunk 
                uint leadLen = Math.Min(len, dims[0] - cur[0]);
                len -= leadLen;

                #region HYCALPER !HC:NANfindStrided
                if (double.IsNaN(min)) {
                    while (leadLen > 0 && double.IsNaN(*(double*)pIn)) {
                        leadLen--; pIn += stride0;
                    }
                    if (leadLen > 0) {
                        // found non-nan value
                        min = *(double*)pIn; max = min;
                    } else if (len == 0) {
                        // all nan: return nans
                        pOut[0] = double.NaN;
                        pOut[1] = double.NaN;
                        return;
                    }
                }
                #endregion HYCALPER 

                while (leadLen > 8) {
                    v = *(double*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(double*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(double*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(double*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(double*)(pIn + 4 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(double*)(pIn + 5 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(double*)(pIn + 6 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(double*)(pIn + 7 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 8 * stride0; leadLen -= 8;
                }
                while (leadLen > 4) {
                    v = *(double*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(double*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(double*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(double*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 4 * stride0; leadLen -= 4;
                }
                while (leadLen-- > 0) {
                    v = *(double*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += stride0;
                }
                if (len == 0) {
                    break;
                }
                // reset initial offset in lead dimension after first iteration
                cur[0] = 0;

                // increase higher dims
                int d = 1;
                while (d < ndims) {
                    if (cur[d] < dims[d] - 1) {
                        higdims += strides[d];
                        cur[d]++;
                        break;
                    } else {
                        cur[d] = 0;
                        higdims -= strides[d] * (dims[d] - 1);
                        d++;
                    }
                }
            }
            pOut[0] = min;
            pOut[1] = max;
        }

        internal static unsafe void Strided64(byte* pSrc, /*!HC:outArr*/double* pOut, long start, long len, long* bsd) {

            uint ndims = (uint)bsd[0];
            long* dims = bsd + 3;
            long* strides = dims + ndims;
            long stride0 = strides[0];
            long* cur = stackalloc long[(int)ndims];

            // figure out the dimension index position for start
            System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

            cur[0] = start % dims[0];
            long f = start / dims[0];
            long higdims = 0;
            int i = 1;
            for (; f > 0 && i < ndims; i++) {
                cur[i] = f % dims[i];
                f /= dims[i];
                higdims += cur[i] * strides[i];
            }
            while (i < ndims) {
                cur[i++] = 0;
            }

            System.Diagnostics.Debug.Assert(f == 0);

            double v;
            double min = double./*!HC:initMinValue*/NaN;
            double max = double./*!HC:initMaxValue*/NaN;

            while (true) {

                byte* pIn = pSrc + higdims + cur[0] * stride0;

                // iteration length limited to either the dimension lengths or the end of the requested chunk 
                long leadLen = Math.Min(len, dims[0] - cur[0]);
                len -= leadLen;

                #region HYCALPER !HC:NANfindStrided
                if (double.IsNaN(min)) {
                    while (leadLen > 0 && double.IsNaN(*(double*)pIn)) {
                        leadLen--; pIn += stride0;
                    }
                    if (leadLen > 0) {
                        // found non-nan value
                        min = *(double*)pIn; max = min;
                    } else if (len == 0) {
                        // all nan: return nans
                        pOut[0] = double.NaN;
                        pOut[1] = double.NaN;
                        return;
                    }
                }
                #endregion HYCALPER 

                while (leadLen > 8) {
                    v = *(double*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(double*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(double*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(double*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(double*)(pIn + 4 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(double*)(pIn + 5 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(double*)(pIn + 6 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(double*)(pIn + 7 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 8 * stride0; leadLen -= 8;
                }
                while (leadLen > 4) {
                    v = *(double*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(double*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(double*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(double*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 4 * stride0; leadLen -= 4;
                }
                while (leadLen-- > 0) {
                    v = *(double*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += stride0;
                }
                if (len == 0) {
                    break;
                }
                // reset initial offset in lead dimension after first iteration
                cur[0] = 0;

                // increase higher dims
                int d = 1;
                while (d < ndims) {
                    if (cur[d] < dims[d] - 1) {
                        higdims += strides[d];
                        cur[d]++;
                        break;
                    } else {
                        cur[d] = 0;
                        higdims -= strides[d] * (dims[d] - 1);
                        d++;
                    }
                }
            }
            pOut[0] = min;
            pOut[1] = max;
        }
    }
    #endregion HYCALPER LOOPEND 
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
   

    internal static class Single {

        internal static int s_threading_overhead_Cont64 = 10;  
        internal static int s_threading_overhead_Strided32 = 10;  
        internal static int s_threading_overhead_Strided64 = 10;

        internal static unsafe Action<IntPtr> Continous64OOPAction = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = p[2].ToInt64();
            var i = locP[1].ToInt64();
            Continous64OOP((byte*)p[0] + len * (uint)sizeof(float) * i,
                            (float*)p[1] + i * 2,
                            (long)len);
            Interlocked.Decrement(ref *(int*)p[3]);
        };
        internal static unsafe Action<IntPtr> Strided32Action = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = (uint)p[3].ToInt32();
            var i = (uint)locP[1].ToInt32();
            Strided32((byte*)p[0], (float*)p[1] + i * 2, len * i, len, (uint*)p[2]);
            Interlocked.Decrement(ref *(int*)p[4]);
        };
        internal static unsafe Action<IntPtr> Strided64Action = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = (long)p[3].ToInt64();
            var i = (long)locP[1].ToInt64();
            Strided64((byte*)p[0], (float*)p[1] + i * 2, len * i, len, (long*)p[2]);
            Interlocked.Decrement(ref *(int*)p[4]);
        };

        internal static unsafe void Continous64OOP(byte* pIn, float* pOut, long len) {

            System.Diagnostics.Debug.Assert(pIn != (byte*)0);
            System.Diagnostics.Debug.Assert(pOut != (float*)0);

            // floatingpoint types: find the first non-nan value
            while (len > 0 && (float.IsNaN(*(float*)pIn))) {
                pIn += sizeof(float);
                len--;
            }
            if (len == 0) {
                // all nans
                pOut[0] = float.NaN;
                pOut[1] = float.NaN;
                return;
            }

            float v, min = *(float*)pIn, max = min;

            while (len >= 8) {
                v = *(float*)(pIn + 0 * sizeof(float)); if (v < min) min = v; if (v > max) max = v;
                v = *(float*)(pIn + 1 * sizeof(float)); if (v < min) min = v; if (v > max) max = v;
                v = *(float*)(pIn + 2 * sizeof(float)); if (v < min) min = v; if (v > max) max = v;
                v = *(float*)(pIn + 3 * sizeof(float)); if (v < min) min = v; if (v > max) max = v;
                v = *(float*)(pIn + 4 * sizeof(float)); if (v < min) min = v; if (v > max) max = v;
                v = *(float*)(pIn + 5 * sizeof(float)); if (v < min) min = v; if (v > max) max = v;
                v = *(float*)(pIn + 6 * sizeof(float)); if (v < min) min = v; if (v > max) max = v;
                v = *(float*)(pIn + 7 * sizeof(float)); if (v < min) min = v; if (v > max) max = v;
                pIn += 8 * sizeof(float); len -= 8;
            }
            while (len-- > 0) {
                v = *(float*)pIn; if (v < min) min = v; if (v > max) max = v;
                pIn += sizeof(float);
            }
            pOut[0] = min;
            pOut[1] = max;
        }

        /// <summary>
        /// Inner ACCUMALL loop for strided, general (non-huge) array.
        /// </summary>
        /// <param name="pSrc">pointer to first element to consider here.</param>
        /// <param name="pOut">Pointer to single element to store result.</param>
        /// <param name="start">First sequential element.</param>
        /// <param name="len">Number of sequential elements to process.</param>
        /// <param name="ordered_bytestrided_bsd">(Re-)ordered BSD of the source array. Strides indicate <i>byte spacings</i>.</param>
        internal static unsafe void Strided32(byte* pSrc, float* pOut, uint start, uint len, uint* ordered_bytestrided_bsd) {

            int ndims = (int)ordered_bytestrided_bsd[0];
            uint* dims = ordered_bytestrided_bsd + 3;
            uint* strides = dims + ndims;
            uint stride0 = strides[0];
            uint* cur = stackalloc uint[ndims];

            // figure out the dimension index position for start
            System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

            cur[0] = start % dims[0];
            uint f = start / dims[0];
            uint higdims = 0;
            int i = 1;
            for (; f > 0 && i < ndims; i++) {
                cur[i] = f % dims[i];
                f /= dims[i];
                higdims += cur[i] * strides[i];
            }
            while (i < ndims) {
                cur[i++] = 0;
            }

            System.Diagnostics.Debug.Assert(f == 0);

            float v;
            float min = float.NaN;
            float max = float.NaN;

            while (true) {

                byte* pIn = pSrc + (higdims + cur[0] * stride0);

                // iteration length limited to either the dimension lengths or the end of the requested chunk 
                uint leadLen = Math.Min(len, dims[0] - cur[0]);
                len -= leadLen;

                #region HYCALPER !HC:NANfindStrided
                if (float.IsNaN(min)) {
                    while (leadLen > 0 && float.IsNaN(*(float*)pIn)) {
                        leadLen--; pIn += stride0;
                    }
                    if (leadLen > 0) {
                        // found non-nan value
                        min = *(float*)pIn; max = min;
                    } else if (len == 0) {
                        // all nan: return nans
                        pOut[0] = float.NaN;
                        pOut[1] = float.NaN;
                        return;
                    }
                }
                #endregion HYCALPER 

                while (leadLen > 8) {
                    v = *(float*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(float*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(float*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(float*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(float*)(pIn + 4 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(float*)(pIn + 5 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(float*)(pIn + 6 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(float*)(pIn + 7 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 8 * stride0; leadLen -= 8;
                }
                while (leadLen > 4) {
                    v = *(float*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(float*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(float*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(float*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 4 * stride0; leadLen -= 4;
                }
                while (leadLen-- > 0) {
                    v = *(float*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += stride0;
                }
                if (len == 0) {
                    break;
                }
                // reset initial offset in lead dimension after first iteration
                cur[0] = 0;

                // increase higher dims
                int d = 1;
                while (d < ndims) {
                    if (cur[d] < dims[d] - 1) {
                        higdims += strides[d];
                        cur[d]++;
                        break;
                    } else {
                        cur[d] = 0;
                        higdims -= strides[d] * (dims[d] - 1);
                        d++;
                    }
                }
            }
            pOut[0] = min;
            pOut[1] = max;
        }

        internal static unsafe void Strided64(byte* pSrc, float* pOut, long start, long len, long* bsd) {

            uint ndims = (uint)bsd[0];
            long* dims = bsd + 3;
            long* strides = dims + ndims;
            long stride0 = strides[0];
            long* cur = stackalloc long[(int)ndims];

            // figure out the dimension index position for start
            System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

            cur[0] = start % dims[0];
            long f = start / dims[0];
            long higdims = 0;
            int i = 1;
            for (; f > 0 && i < ndims; i++) {
                cur[i] = f % dims[i];
                f /= dims[i];
                higdims += cur[i] * strides[i];
            }
            while (i < ndims) {
                cur[i++] = 0;
            }

            System.Diagnostics.Debug.Assert(f == 0);

            float v;
            float min = float.NaN;
            float max = float.NaN;

            while (true) {

                byte* pIn = pSrc + higdims + cur[0] * stride0;

                // iteration length limited to either the dimension lengths or the end of the requested chunk 
                long leadLen = Math.Min(len, dims[0] - cur[0]);
                len -= leadLen;

                #region HYCALPER !HC:NANfindStrided
                if (float.IsNaN(min)) {
                    while (leadLen > 0 && float.IsNaN(*(float*)pIn)) {
                        leadLen--; pIn += stride0;
                    }
                    if (leadLen > 0) {
                        // found non-nan value
                        min = *(float*)pIn; max = min;
                    } else if (len == 0) {
                        // all nan: return nans
                        pOut[0] = float.NaN;
                        pOut[1] = float.NaN;
                        return;
                    }
                }
                #endregion HYCALPER 

                while (leadLen > 8) {
                    v = *(float*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(float*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(float*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(float*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(float*)(pIn + 4 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(float*)(pIn + 5 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(float*)(pIn + 6 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(float*)(pIn + 7 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 8 * stride0; leadLen -= 8;
                }
                while (leadLen > 4) {
                    v = *(float*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(float*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(float*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(float*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 4 * stride0; leadLen -= 4;
                }
                while (leadLen-- > 0) {
                    v = *(float*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += stride0;
                }
                if (len == 0) {
                    break;
                }
                // reset initial offset in lead dimension after first iteration
                cur[0] = 0;

                // increase higher dims
                int d = 1;
                while (d < ndims) {
                    if (cur[d] < dims[d] - 1) {
                        higdims += strides[d];
                        cur[d]++;
                        break;
                    } else {
                        cur[d] = 0;
                        higdims -= strides[d] * (dims[d] - 1);
                        d++;
                    }
                }
            }
            pOut[0] = min;
            pOut[1] = max;
        }
    }

#endregion HYCALPER AUTO GENERATED CODE

}

namespace ILNumerics.Core.InnerLoops.GetLimitsNoInf { // double, float, ignore Infinity
    
    #region HYCALPER LOOPSTART
/*!HC:TYPELIST:
<hycalper>
<type>
    <source locate="here">
        double
    </source>
    <destination>float</destination>
</type>
<type>
    <source locate="here">
        Double
    </source>
    <destination>Single</destination>
</type>
</hycalper>
*/
    internal static class Double {

        internal static int s_threading_overhead_Cont64 = 10;  // empirical value
        internal static int s_threading_overhead_Strided32 = 10;  // empirical value
        internal static int s_threading_overhead_Strided64 = 10;  // empirical value

        internal static unsafe Action<IntPtr> Continous64OOPAction = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = p[2].ToInt64();
            var i = locP[1].ToInt64();
            Continous64OOP((byte*)p[0] + len * (uint)sizeof(double) * i,
                            (/*!HC:outArr*/double*)p[1] + i * 2,
                            (long)len);
            Interlocked.Decrement(ref *(int*)p[3]);
        };
        internal static unsafe Action<IntPtr> Strided32Action = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = (uint)p[3].ToInt32();
            var i = (uint)locP[1].ToInt32();
            Strided32((byte*)p[0], (/*!HC:outArr*/double*)p[1] + i * 2, len * i, len, (uint*)p[2]);
            Interlocked.Decrement(ref *(int*)p[4]);
        };
        internal static unsafe Action<IntPtr> Strided64Action = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = (long)p[3].ToInt64();
            var i = (long)locP[1].ToInt64();
            Strided64((byte*)p[0], (/*!HC:outArr*/double*)p[1] + i * 2, len * i, len, (long*)p[2]);
            Interlocked.Decrement(ref *(int*)p[4]);
        };

        internal static unsafe void Continous64OOP(byte* pIn, /*!HC:outArr*/double* pOut, long len) {
            
            System.Diagnostics.Debug.Assert(pIn != (byte*)0);
            System.Diagnostics.Debug.Assert(pOut != (/*!HC:outArr*/double*)0);

            #region find non-nan values
            // floatingpoint types: find the first non-nan value
            while (len > 0 && (double.IsNaN(*(double*)pIn) 
                            || double.IsInfinity(*(double*)pIn))) {
                pIn += sizeof(double);
                len--;
            }
            if (len == 0) {
                // all nans
                pOut[0] = double.NaN;
                pOut[1] = double.NaN;
                return;
            }
            #endregion 

            double v, min = *(double*)pIn, max = min;
            while (len >= 8) {
                v = *(double*)(pIn + 0 * sizeof(double)); if (!double.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                v = *(double*)(pIn + 1 * sizeof(double)); if (!double.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                v = *(double*)(pIn + 2 * sizeof(double)); if (!double.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                v = *(double*)(pIn + 3 * sizeof(double)); if (!double.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                v = *(double*)(pIn + 4 * sizeof(double)); if (!double.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                v = *(double*)(pIn + 5 * sizeof(double)); if (!double.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                v = *(double*)(pIn + 6 * sizeof(double)); if (!double.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                v = *(double*)(pIn + 7 * sizeof(double)); if (!double.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                pIn += 8 * sizeof(double); len -= 8;
            }
            while (len-- > 0) {
                v = *(double*)pIn; if (!double.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                pIn += sizeof(double);
            }
            pOut[0] = min;
            pOut[1] = max;
        }

        /// <summary>
        /// Inner ACCUMALL loop for strided, general (non-huge) array.
        /// </summary>
        /// <param name="pSrc">pointer to first element to consider here.</param>
        /// <param name="pOut">Pointer to single element to store result.</param>
        /// <param name="start">First sequential element.</param>
        /// <param name="len">Number of sequential elements to process.</param>
        /// <param name="ordered_bytestrided_bsd">(Re-)ordered BSD of the source array. Strides indicate <i>byte spacings</i>.</param>
        internal static unsafe void Strided32(byte* pSrc, /*!HC:outArr*/double* pOut, uint start, uint len, uint* ordered_bytestrided_bsd) {

            int ndims = (int)ordered_bytestrided_bsd[0];
            uint* dims = ordered_bytestrided_bsd + 3;
            uint* strides = dims + ndims;
            uint stride0 = strides[0];
            uint* cur = stackalloc uint[ndims];

            // figure out the dimension index position for start
            System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

            cur[0] = start % dims[0];
            uint f = start / dims[0];
            uint higdims = 0;
            int i = 1;
            for (; f > 0 && i < ndims; i++) {
                cur[i] = f % dims[i];
                f /= dims[i];
                higdims += cur[i] * strides[i];
            }
            while (i < ndims) {
                cur[i++] = 0;
            }

            System.Diagnostics.Debug.Assert(f == 0);

            double v;
            double min = double./*!HC:initMinValue*/NaN;
            double max = double./*!HC:initMaxValue*/NaN;

            while (true) {

                byte* pIn = pSrc + (higdims + cur[0] * stride0);

                // iteration length limited to either the dimension lengths or the end of the requested chunk 
                uint leadLen = Math.Min(len, dims[0] - cur[0]);
                len -= leadLen;

                #region HYCALPER !HC:NANNoInfFindStrided
                if (double.IsNaN(min)) {
                    while (leadLen > 0 && (double.IsNaN(*(double*)pIn) 
                                        || double.IsInfinity(*(double*)pIn))) {
                        leadLen--; pIn += stride0;
                    }
                    if (leadLen > 0) {
                        // found non-nan value
                        min = *(double*)pIn; max = min;
                    } else if (len == 0) {
                        // all nan / inf: return nans
                        pOut[0] = double.NaN;
                        pOut[1] = double.NaN;
                        return;
                    }
                }
                #endregion HYCALPER 

                while (leadLen > 8) {
                    v = *(double*)(pIn + 0 * stride0); if (!double.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(double*)(pIn + 1 * stride0); if (!double.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(double*)(pIn + 2 * stride0); if (!double.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(double*)(pIn + 3 * stride0); if (!double.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(double*)(pIn + 4 * stride0); if (!double.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(double*)(pIn + 5 * stride0); if (!double.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(double*)(pIn + 6 * stride0); if (!double.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(double*)(pIn + 7 * stride0); if (!double.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    pIn += 8 * stride0; leadLen -= 8;
                }
                while (leadLen > 4) {
                    v = *(double*)(pIn + 0 * stride0); if (!double.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(double*)(pIn + 1 * stride0); if (!double.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(double*)(pIn + 2 * stride0); if (!double.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(double*)(pIn + 3 * stride0); if (!double.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    pIn += 4 * stride0; leadLen -= 4;
                }
                while (leadLen-- > 0) {
                    v = *(double*)(pIn + 0 * stride0); if (!double.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    pIn += stride0;
                }
                if (len == 0) {
                    break;
                }
                // reset initial offset in lead dimension after first iteration
                cur[0] = 0;

                // increase higher dims
                int d = 1;
                while (d < ndims) {
                    if (cur[d] < dims[d] - 1) {
                        higdims += strides[d];
                        cur[d]++;
                        break;
                    } else {
                        cur[d] = 0;
                        higdims -= strides[d] * (dims[d] - 1);
                        d++;
                    }
                }
            }
            pOut[0] = min;
            pOut[1] = max;
        }

        internal static unsafe void Strided64(byte* pSrc, /*!HC:outArr*/double* pOut, long start, long len, long* bsd) {

            uint ndims = (uint)bsd[0];
            long* dims = bsd + 3;
            long* strides = dims + ndims;
            long stride0 = strides[0];
            long* cur = stackalloc long[(int)ndims];

            // figure out the dimension index position for start
            System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

            cur[0] = start % dims[0];
            long f = start / dims[0];
            long higdims = 0;
            int i = 1;
            for (; f > 0 && i < ndims; i++) {
                cur[i] = f % dims[i];
                f /= dims[i];
                higdims += cur[i] * strides[i];
            }
            while (i < ndims) {
                cur[i++] = 0;
            }

            System.Diagnostics.Debug.Assert(f == 0);

            double v;
            double min = double./*!HC:initMinValue*/NaN;
            double max = double./*!HC:initMaxValue*/NaN;

            while (true) {

                byte* pIn = pSrc + higdims + cur[0] * stride0;

                // iteration length limited to either the dimension lengths or the end of the requested chunk 
                long leadLen = Math.Min(len, dims[0] - cur[0]);
                len -= leadLen;

                #region HYCALPER !HC:NANNoInffindStrided
                if (double.IsNaN(min)) {
                    while (leadLen > 0 && (double.IsNaN(*(double*)pIn)
                        || double.IsInfinity(*(double*)pIn))) {
                        leadLen--; pIn += stride0;
                    }
                    if (leadLen > 0) {
                        // found non-nan/no-inf value
                        min = *(double*)pIn; max = min;
                    } else if (len == 0) {
                        // all nan: return nans
                        pOut[0] = double.NaN;
                        pOut[1] = double.NaN;
                        return;
                    }
                }
                #endregion HYCALPER 

                while (leadLen > 8) {
                    v = *(double*)(pIn + 0 * stride0); if (!double.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(double*)(pIn + 1 * stride0); if (!double.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(double*)(pIn + 2 * stride0); if (!double.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(double*)(pIn + 3 * stride0); if (!double.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(double*)(pIn + 4 * stride0); if (!double.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(double*)(pIn + 5 * stride0); if (!double.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(double*)(pIn + 6 * stride0); if (!double.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(double*)(pIn + 7 * stride0); if (!double.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    pIn += 8 * stride0; leadLen -= 8;
                }
                while (leadLen > 4) {
                    v = *(double*)(pIn + 0 * stride0); if (!double.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(double*)(pIn + 1 * stride0); if (!double.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(double*)(pIn + 2 * stride0); if (!double.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(double*)(pIn + 3 * stride0); if (!double.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    pIn += 4 * stride0; leadLen -= 4;
                }
                while (leadLen-- > 0) {
                    v = *(double*)(pIn + 0 * stride0); if (!double.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    pIn += stride0;
                }
                if (len == 0) {
                    break;
                }
                // reset initial offset in lead dimension after first iteration
                cur[0] = 0;

                // increase higher dims
                int d = 1;
                while (d < ndims) {
                    if (cur[d] < dims[d] - 1) {
                        higdims += strides[d];
                        cur[d]++;
                        break;
                    } else {
                        cur[d] = 0;
                        higdims -= strides[d] * (dims[d] - 1);
                        d++;
                    }
                }
            }
            pOut[0] = min;
            pOut[1] = max;
        }
    }
        #endregion HYCALPER LOOPEND 
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 

    internal static class Single {

        internal static int s_threading_overhead_Cont64 = 10;  // empirical value
        internal static int s_threading_overhead_Strided32 = 10;  // empirical value
        internal static int s_threading_overhead_Strided64 = 10;  // empirical value

        internal static unsafe Action<IntPtr> Continous64OOPAction = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = p[2].ToInt64();
            var i = locP[1].ToInt64();
            Continous64OOP((byte*)p[0] + len * (uint)sizeof(float) * i,
                            (float*)p[1] + i * 2,
                            (long)len);
            Interlocked.Decrement(ref *(int*)p[3]);
        };
        internal static unsafe Action<IntPtr> Strided32Action = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = (uint)p[3].ToInt32();
            var i = (uint)locP[1].ToInt32();
            Strided32((byte*)p[0], (float*)p[1] + i * 2, len * i, len, (uint*)p[2]);
            Interlocked.Decrement(ref *(int*)p[4]);
        };
        internal static unsafe Action<IntPtr> Strided64Action = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = (long)p[3].ToInt64();
            var i = (long)locP[1].ToInt64();
            Strided64((byte*)p[0], (float*)p[1] + i * 2, len * i, len, (long*)p[2]);
            Interlocked.Decrement(ref *(int*)p[4]);
        };

        internal static unsafe void Continous64OOP(byte* pIn, float* pOut, long len) {
            
            System.Diagnostics.Debug.Assert(pIn != (byte*)0);
            System.Diagnostics.Debug.Assert(pOut != (float*)0);

            #region find non-nan values
            // floatingpoint types: find the first non-nan value
            while (len > 0 && (float.IsNaN(*(float*)pIn) 
                            || float.IsInfinity(*(float*)pIn))) {
                pIn += sizeof(float);
                len--;
            }
            if (len == 0) {
                // all nans
                pOut[0] = float.NaN;
                pOut[1] = float.NaN;
                return;
            }
            #endregion 

            float v, min = *(float*)pIn, max = min;
            while (len >= 8) {
                v = *(float*)(pIn + 0 * sizeof(float)); if (!float.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                v = *(float*)(pIn + 1 * sizeof(float)); if (!float.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                v = *(float*)(pIn + 2 * sizeof(float)); if (!float.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                v = *(float*)(pIn + 3 * sizeof(float)); if (!float.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                v = *(float*)(pIn + 4 * sizeof(float)); if (!float.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                v = *(float*)(pIn + 5 * sizeof(float)); if (!float.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                v = *(float*)(pIn + 6 * sizeof(float)); if (!float.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                v = *(float*)(pIn + 7 * sizeof(float)); if (!float.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                pIn += 8 * sizeof(float); len -= 8;
            }
            while (len-- > 0) {
                v = *(float*)pIn; if (!float.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                pIn += sizeof(float);
            }
            pOut[0] = min;
            pOut[1] = max;
        }

        /// <summary>
        /// Inner ACCUMALL loop for strided, general (non-huge) array.
        /// </summary>
        /// <param name="pSrc">pointer to first element to consider here.</param>
        /// <param name="pOut">Pointer to single element to store result.</param>
        /// <param name="start">First sequential element.</param>
        /// <param name="len">Number of sequential elements to process.</param>
        /// <param name="ordered_bytestrided_bsd">(Re-)ordered BSD of the source array. Strides indicate <i>byte spacings</i>.</param>
        internal static unsafe void Strided32(byte* pSrc, float* pOut, uint start, uint len, uint* ordered_bytestrided_bsd) {

            int ndims = (int)ordered_bytestrided_bsd[0];
            uint* dims = ordered_bytestrided_bsd + 3;
            uint* strides = dims + ndims;
            uint stride0 = strides[0];
            uint* cur = stackalloc uint[ndims];

            // figure out the dimension index position for start
            System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

            cur[0] = start % dims[0];
            uint f = start / dims[0];
            uint higdims = 0;
            int i = 1;
            for (; f > 0 && i < ndims; i++) {
                cur[i] = f % dims[i];
                f /= dims[i];
                higdims += cur[i] * strides[i];
            }
            while (i < ndims) {
                cur[i++] = 0;
            }

            System.Diagnostics.Debug.Assert(f == 0);

            float v;
            float min = float.NaN;
            float max = float.NaN;

            while (true) {

                byte* pIn = pSrc + (higdims + cur[0] * stride0);

                // iteration length limited to either the dimension lengths or the end of the requested chunk 
                uint leadLen = Math.Min(len, dims[0] - cur[0]);
                len -= leadLen;

                #region HYCALPER !HC:NANNoInfFindStrided
                if (float.IsNaN(min)) {
                    while (leadLen > 0 && (float.IsNaN(*(float*)pIn) 
                                        || float.IsInfinity(*(float*)pIn))) {
                        leadLen--; pIn += stride0;
                    }
                    if (leadLen > 0) {
                        // found non-nan value
                        min = *(float*)pIn; max = min;
                    } else if (len == 0) {
                        // all nan / inf: return nans
                        pOut[0] = float.NaN;
                        pOut[1] = float.NaN;
                        return;
                    }
                }
                #endregion HYCALPER 

                while (leadLen > 8) {
                    v = *(float*)(pIn + 0 * stride0); if (!float.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(float*)(pIn + 1 * stride0); if (!float.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(float*)(pIn + 2 * stride0); if (!float.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(float*)(pIn + 3 * stride0); if (!float.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(float*)(pIn + 4 * stride0); if (!float.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(float*)(pIn + 5 * stride0); if (!float.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(float*)(pIn + 6 * stride0); if (!float.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(float*)(pIn + 7 * stride0); if (!float.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    pIn += 8 * stride0; leadLen -= 8;
                }
                while (leadLen > 4) {
                    v = *(float*)(pIn + 0 * stride0); if (!float.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(float*)(pIn + 1 * stride0); if (!float.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(float*)(pIn + 2 * stride0); if (!float.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(float*)(pIn + 3 * stride0); if (!float.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    pIn += 4 * stride0; leadLen -= 4;
                }
                while (leadLen-- > 0) {
                    v = *(float*)(pIn + 0 * stride0); if (!float.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    pIn += stride0;
                }
                if (len == 0) {
                    break;
                }
                // reset initial offset in lead dimension after first iteration
                cur[0] = 0;

                // increase higher dims
                int d = 1;
                while (d < ndims) {
                    if (cur[d] < dims[d] - 1) {
                        higdims += strides[d];
                        cur[d]++;
                        break;
                    } else {
                        cur[d] = 0;
                        higdims -= strides[d] * (dims[d] - 1);
                        d++;
                    }
                }
            }
            pOut[0] = min;
            pOut[1] = max;
        }

        internal static unsafe void Strided64(byte* pSrc, float* pOut, long start, long len, long* bsd) {

            uint ndims = (uint)bsd[0];
            long* dims = bsd + 3;
            long* strides = dims + ndims;
            long stride0 = strides[0];
            long* cur = stackalloc long[(int)ndims];

            // figure out the dimension index position for start
            System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

            cur[0] = start % dims[0];
            long f = start / dims[0];
            long higdims = 0;
            int i = 1;
            for (; f > 0 && i < ndims; i++) {
                cur[i] = f % dims[i];
                f /= dims[i];
                higdims += cur[i] * strides[i];
            }
            while (i < ndims) {
                cur[i++] = 0;
            }

            System.Diagnostics.Debug.Assert(f == 0);

            float v;
            float min = float.NaN;
            float max = float.NaN;

            while (true) {

                byte* pIn = pSrc + higdims + cur[0] * stride0;

                // iteration length limited to either the dimension lengths or the end of the requested chunk 
                long leadLen = Math.Min(len, dims[0] - cur[0]);
                len -= leadLen;

                #region HYCALPER !HC:NANNoInffindStrided
                if (float.IsNaN(min)) {
                    while (leadLen > 0 && (float.IsNaN(*(float*)pIn)
                        || float.IsInfinity(*(float*)pIn))) {
                        leadLen--; pIn += stride0;
                    }
                    if (leadLen > 0) {
                        // found non-nan/no-inf value
                        min = *(float*)pIn; max = min;
                    } else if (len == 0) {
                        // all nan: return nans
                        pOut[0] = float.NaN;
                        pOut[1] = float.NaN;
                        return;
                    }
                }
                #endregion HYCALPER 

                while (leadLen > 8) {
                    v = *(float*)(pIn + 0 * stride0); if (!float.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(float*)(pIn + 1 * stride0); if (!float.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(float*)(pIn + 2 * stride0); if (!float.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(float*)(pIn + 3 * stride0); if (!float.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(float*)(pIn + 4 * stride0); if (!float.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(float*)(pIn + 5 * stride0); if (!float.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(float*)(pIn + 6 * stride0); if (!float.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(float*)(pIn + 7 * stride0); if (!float.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    pIn += 8 * stride0; leadLen -= 8;
                }
                while (leadLen > 4) {
                    v = *(float*)(pIn + 0 * stride0); if (!float.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(float*)(pIn + 1 * stride0); if (!float.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(float*)(pIn + 2 * stride0); if (!float.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    v = *(float*)(pIn + 3 * stride0); if (!float.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    pIn += 4 * stride0; leadLen -= 4;
                }
                while (leadLen-- > 0) {
                    v = *(float*)(pIn + 0 * stride0); if (!float.IsInfinity(v)) { if (v < min) min = v; if (v > max) max = v; }
                    pIn += stride0;
                }
                if (len == 0) {
                    break;
                }
                // reset initial offset in lead dimension after first iteration
                cur[0] = 0;

                // increase higher dims
                int d = 1;
                while (d < ndims) {
                    if (cur[d] < dims[d] - 1) {
                        higdims += strides[d];
                        cur[d]++;
                        break;
                    } else {
                        cur[d] = 0;
                        higdims -= strides[d] * (dims[d] - 1);
                        d++;
                    }
                }
            }
            pOut[0] = min;
            pOut[1] = max;
        }
    }

#endregion HYCALPER AUTO GENERATED CODE

}

namespace ILNumerics.Core.InnerLoops.GetLimits {  // complex, fcomplex, no special inf handling

    #region HYCALPER LOOPSTART
    /*!HC:TYPELIST:
    <hycalper>
    <type>
        <source locate="here">
            complex
        </source>
        <destination>fcomplex</destination>
    </type>
    <type>
        <source locate="here">
            Complex
        </source>
        <destination>FComplex</destination>
    </type>
    <type>
        <source locate="here">
            double
        </source>
        <destination>float</destination>
    </type>
    </hycalper>
    */

    internal static class Complex {

        internal static int s_threading_overhead_Cont64 = 10; 
        internal static int s_threading_overhead_Strided32 = 10; 
        internal static int s_threading_overhead_Strided64 = 10;

        internal static unsafe Action<IntPtr> Continous64OOPAction = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = p[2].ToInt64();
            var i = locP[1].ToInt64();
            Continous64OOP((byte*)p[0] + len * (uint)sizeof(complex) * i,
                            (/*!HC:outArr*/complex*)p[1] + i * 2,
                            (long)len);
            Interlocked.Decrement(ref *(int*)p[3]);
        };
        internal static unsafe Action<IntPtr> Strided32Action = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = (uint)p[3].ToInt32();
            var i = (uint)locP[1].ToInt32();
            Strided32((byte*)p[0], (/*!HC:outArr*/complex*)p[1] + i * 2, len * i, len, (uint*)p[2]);
            Interlocked.Decrement(ref *(int*)p[4]);
        };
        internal static unsafe Action<IntPtr> Strided64Action = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = (long)p[3].ToInt64();
            var i = (long)locP[1].ToInt64();
            Strided64((byte*)p[0], (/*!HC:outArr*/complex*)p[1] + i * 2, len * i, len, (long*)p[2]);
            Interlocked.Decrement(ref *(int*)p[4]);
        };

        internal static unsafe void Continous64OOP(byte* pIn, /*!HC:outArr*/complex* pOut, long len) {

            System.Diagnostics.Debug.Assert(pIn != (byte*)0);
            System.Diagnostics.Debug.Assert(pOut != (/*!HC:outArr*/complex*)0);

            // floatingpoint types: find the first non-nan value

            complex v, min = complex.NaN, max = complex.NaN;

            #region find non-NaN values
            while (len > 0 && complex.IsNaN(min)) {
                v = *(complex*)pIn;
                if (double.IsNaN(min.real)) {
                    if (!double.IsNaN(v.real)) {
                        // initialize
                        min.real = max.real = v.real;
                    }
                } else {
                    // handle real part regularly
                    if (v.real < min.real) min.real = v.real;
                    if (v.real > max.real) max.real = v.real;
                }
                if (double.IsNaN(min.imag)) {
                    if (!double.IsNaN(v.imag)) {
                        // initialize
                        min.imag = max.imag = v.imag;
                    }
                } else {
                    if (v.imag < min.imag) min.imag = v.imag;
                    if (v.imag > max.imag) max.imag = v.imag;
                }
                pIn += sizeof(complex);
                len--;
            }
            if (double.IsNaN(min.real) && double.IsNaN(min.imag)) {
                // all nans
                pOut[0] = complex.NaN;
                pOut[1] = complex.NaN;
                return;
            }
            #endregion  

            while (len >= 8) {
                v = *(complex*)(pIn + 0 * sizeof(complex)); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                v = *(complex*)(pIn + 1 * sizeof(complex)); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                v = *(complex*)(pIn + 2 * sizeof(complex)); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                v = *(complex*)(pIn + 3 * sizeof(complex)); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                v = *(complex*)(pIn + 4 * sizeof(complex)); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                v = *(complex*)(pIn + 5 * sizeof(complex)); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                v = *(complex*)(pIn + 6 * sizeof(complex)); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                v = *(complex*)(pIn + 7 * sizeof(complex)); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                pIn += 8 * sizeof(complex); len -= 8;
            }
            while (len-- > 0) {
                v = *(complex*)pIn; if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                pIn += sizeof(complex);
            }
            pOut[0] = min;
            pOut[1] = max;
        }

        /// <summary>
        /// Inner ACCUMALL loop for strided, general (non-huge) array.
        /// </summary>
        /// <param name="pSrc">pointer to first element to consider here.</param>
        /// <param name="pOut">Pointer to single element to store result.</param>
        /// <param name="start">First sequential element.</param>
        /// <param name="len">Number of sequential elements to process.</param>
        /// <param name="ordered_bytestrided_bsd">(Re-)ordered BSD of the source array. Strides indicate <i>byte spacings</i>.</param>
        internal static unsafe void Strided32(byte* pSrc, /*!HC:outArr*/complex* pOut, uint start, uint len, uint* ordered_bytestrided_bsd) {

            int ndims = (int)ordered_bytestrided_bsd[0];
            uint* dims = ordered_bytestrided_bsd + 3;
            uint* strides = dims + ndims;
            uint stride0 = strides[0];
            uint* cur = stackalloc uint[ndims];

            // figure out the dimension index position for start
            System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

            cur[0] = start % dims[0];
            uint f = start / dims[0];
            uint higdims = 0;
            int i = 1;
            for (; f > 0 && i < ndims; i++) {
                cur[i] = f % dims[i];
                f /= dims[i];
                higdims += cur[i] * strides[i];
            }
            while (i < ndims) {
                cur[i++] = 0;
            }

            System.Diagnostics.Debug.Assert(f == 0);

            complex v;
            complex min = complex./*!HC:initMinValue*/NaN;
            complex max = complex./*!HC:initMaxValue*/NaN;

            while (true) {

                byte* pIn = pSrc + (higdims + cur[0] * stride0);

                // iteration length limited to either the dimension lengths or the end of the requested chunk 
                uint leadLen = Math.Min(len, dims[0] - cur[0]);
                len -= leadLen;

                #region find non-NaN values
                if (complex.IsNaN(min)) {
                    while (leadLen > 0 && complex.IsNaN(min)) {
                        v = *(complex*)pIn;
                        if (double.IsNaN(min.real)) {
                            if (!double.IsNaN(v.real)) {
                                // initialize
                                min.real = max.real = v.real;
                            }
                        } else {
                            // handle real part regularly
                            if (v.real < min.real) min.real = v.real;
                            if (v.real > max.real) max.real = v.real;
                        }
                        if (double.IsNaN(min.imag)) {
                            if (!double.IsNaN(v.imag)) {
                                // initialize
                                min.imag = max.imag = v.imag;
                            }
                        } else {
                            if (v.imag < min.imag) min.imag = v.imag;
                            if (v.imag > max.imag) max.imag = v.imag;
                        }
                        pIn += stride0;
                        leadLen--;
                    }
                }
                #endregion  

                while (leadLen > 8) {
                    v = *(complex*)(pIn + 0 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(complex*)(pIn + 1 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(complex*)(pIn + 2 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(complex*)(pIn + 3 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(complex*)(pIn + 4 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(complex*)(pIn + 5 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(complex*)(pIn + 6 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(complex*)(pIn + 7 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    pIn += 8 * stride0; leadLen -= 8;
                }
                while (leadLen > 4) {
                    v = *(complex*)(pIn + 0 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(complex*)(pIn + 1 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(complex*)(pIn + 2 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(complex*)(pIn + 3 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    pIn += 4 * stride0; leadLen -= 4;
                }
                while (leadLen-- > 0) {
                    v = *(complex*)(pIn + 0 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    pIn += stride0;
                }
                if (len == 0) {
                    break;
                }
                // reset initial offset in lead dimension after first iteration
                cur[0] = 0;

                // increase higher dims
                int d = 1;
                while (d < ndims) {
                    if (cur[d] < dims[d] - 1) {
                        higdims += strides[d];
                        cur[d]++;
                        break;
                    } else {
                        cur[d] = 0;
                        higdims -= strides[d] * (dims[d] - 1);
                        d++;
                    }
                }
            }
            pOut[0] = min;
            pOut[1] = max;
        }

        internal static unsafe void Strided64(byte* pSrc, /*!HC:outArr*/complex* pOut, long start, long len, long* bsd) {

            uint ndims = (uint)bsd[0];
            long* dims = bsd + 3;
            long* strides = dims + ndims;
            long stride0 = strides[0];
            long* cur = stackalloc long[(int)ndims];

            // figure out the dimension index position for start
            System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

            cur[0] = start % dims[0];
            long f = start / dims[0];
            long higdims = 0;
            int i = 1;
            for (; f > 0 && i < ndims; i++) {
                cur[i] = f % dims[i];
                f /= dims[i];
                higdims += cur[i] * strides[i];
            }
            while (i < ndims) {
                cur[i++] = 0;
            }

            System.Diagnostics.Debug.Assert(f == 0);

            complex v;
            complex min = complex./*!HC:initMinValue*/NaN;
            complex max = complex./*!HC:initMaxValue*/NaN;

            while (true) {

                byte* pIn = pSrc + higdims + cur[0] * stride0;

                // iteration length limited to either the dimension lengths or the end of the requested chunk 
                long leadLen = Math.Min(len, dims[0] - cur[0]);
                len -= leadLen;

                #region find non-NaN values
                if (complex.IsNaN(min)) {
                    while (leadLen > 0 && complex.IsNaN(min)) {
                        v = *(complex*)pIn;
                        if (double.IsNaN(min.real)) {
                            if (!double.IsNaN(v.real)) {
                                // initialize
                                min.real = max.real = v.real;
                            }
                        } else {
                            // handle real part regularly
                            if (v.real < min.real) min.real = v.real;
                            if (v.real > max.real) max.real = v.real;
                        }
                        if (double.IsNaN(min.imag)) {
                            if (!double.IsNaN(v.imag)) {
                                // initialize
                                min.imag = max.imag = v.imag;
                            }
                        } else {
                            if (v.imag < min.imag) min.imag = v.imag;
                            if (v.imag > max.imag) max.imag = v.imag;
                        }
                        pIn += stride0;
                        leadLen--;
                    }
                }
                #endregion  

                while (leadLen > 8) {
                    v = *(complex*)(pIn + 0 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(complex*)(pIn + 1 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(complex*)(pIn + 2 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(complex*)(pIn + 3 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(complex*)(pIn + 4 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(complex*)(pIn + 5 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(complex*)(pIn + 6 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(complex*)(pIn + 7 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    pIn += 8 * stride0; leadLen -= 8;
                }
                while (leadLen > 4) {
                    v = *(complex*)(pIn + 0 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(complex*)(pIn + 1 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(complex*)(pIn + 2 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(complex*)(pIn + 3 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    pIn += 4 * stride0; leadLen -= 4;
                }
                while (leadLen-- > 0) {
                    v = *(complex*)(pIn + 0 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    pIn += stride0;
                }
                if (len == 0) {
                    break;
                }
                // reset initial offset in lead dimension after first iteration
                cur[0] = 0;

                // increase higher dims
                int d = 1;
                while (d < ndims) {
                    if (cur[d] < dims[d] - 1) {
                        higdims += strides[d];
                        cur[d]++;
                        break;
                    } else {
                        cur[d] = 0;
                        higdims -= strides[d] * (dims[d] - 1);
                        d++;
                    }
                }
            }
            pOut[0] = min;
            pOut[1] = max;
        }
    }
    #endregion HYCALPER LOOPEND 
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
   

    internal static class FComplex {

        internal static int s_threading_overhead_Cont64 = 10; 
        internal static int s_threading_overhead_Strided32 = 10; 
        internal static int s_threading_overhead_Strided64 = 10;

        internal static unsafe Action<IntPtr> Continous64OOPAction = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = p[2].ToInt64();
            var i = locP[1].ToInt64();
            Continous64OOP((byte*)p[0] + len * (uint)sizeof(fcomplex) * i,
                            (fcomplex*)p[1] + i * 2,
                            (long)len);
            Interlocked.Decrement(ref *(int*)p[3]);
        };
        internal static unsafe Action<IntPtr> Strided32Action = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = (uint)p[3].ToInt32();
            var i = (uint)locP[1].ToInt32();
            Strided32((byte*)p[0], (fcomplex*)p[1] + i * 2, len * i, len, (uint*)p[2]);
            Interlocked.Decrement(ref *(int*)p[4]);
        };
        internal static unsafe Action<IntPtr> Strided64Action = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = (long)p[3].ToInt64();
            var i = (long)locP[1].ToInt64();
            Strided64((byte*)p[0], (fcomplex*)p[1] + i * 2, len * i, len, (long*)p[2]);
            Interlocked.Decrement(ref *(int*)p[4]);
        };

        internal static unsafe void Continous64OOP(byte* pIn, fcomplex* pOut, long len) {

            System.Diagnostics.Debug.Assert(pIn != (byte*)0);
            System.Diagnostics.Debug.Assert(pOut != (fcomplex*)0);

            // floatingpoint types: find the first non-nan value

            fcomplex v, min = fcomplex.NaN, max = fcomplex.NaN;

            #region find non-NaN values
            while (len > 0 && fcomplex.IsNaN(min)) {
                v = *(fcomplex*)pIn;
                if (float.IsNaN(min.real)) {
                    if (!float.IsNaN(v.real)) {
                        // initialize
                        min.real = max.real = v.real;
                    }
                } else {
                    // handle real part regularly
                    if (v.real < min.real) min.real = v.real;
                    if (v.real > max.real) max.real = v.real;
                }
                if (float.IsNaN(min.imag)) {
                    if (!float.IsNaN(v.imag)) {
                        // initialize
                        min.imag = max.imag = v.imag;
                    }
                } else {
                    if (v.imag < min.imag) min.imag = v.imag;
                    if (v.imag > max.imag) max.imag = v.imag;
                }
                pIn += sizeof(fcomplex);
                len--;
            }
            if (float.IsNaN(min.real) && float.IsNaN(min.imag)) {
                // all nans
                pOut[0] = fcomplex.NaN;
                pOut[1] = fcomplex.NaN;
                return;
            }
            #endregion  

            while (len >= 8) {
                v = *(fcomplex*)(pIn + 0 * sizeof(fcomplex)); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                v = *(fcomplex*)(pIn + 1 * sizeof(fcomplex)); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                v = *(fcomplex*)(pIn + 2 * sizeof(fcomplex)); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                v = *(fcomplex*)(pIn + 3 * sizeof(fcomplex)); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                v = *(fcomplex*)(pIn + 4 * sizeof(fcomplex)); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                v = *(fcomplex*)(pIn + 5 * sizeof(fcomplex)); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                v = *(fcomplex*)(pIn + 6 * sizeof(fcomplex)); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                v = *(fcomplex*)(pIn + 7 * sizeof(fcomplex)); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                pIn += 8 * sizeof(fcomplex); len -= 8;
            }
            while (len-- > 0) {
                v = *(fcomplex*)pIn; if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                pIn += sizeof(fcomplex);
            }
            pOut[0] = min;
            pOut[1] = max;
        }

        /// <summary>
        /// Inner ACCUMALL loop for strided, general (non-huge) array.
        /// </summary>
        /// <param name="pSrc">pointer to first element to consider here.</param>
        /// <param name="pOut">Pointer to single element to store result.</param>
        /// <param name="start">First sequential element.</param>
        /// <param name="len">Number of sequential elements to process.</param>
        /// <param name="ordered_bytestrided_bsd">(Re-)ordered BSD of the source array. Strides indicate <i>byte spacings</i>.</param>
        internal static unsafe void Strided32(byte* pSrc, fcomplex* pOut, uint start, uint len, uint* ordered_bytestrided_bsd) {

            int ndims = (int)ordered_bytestrided_bsd[0];
            uint* dims = ordered_bytestrided_bsd + 3;
            uint* strides = dims + ndims;
            uint stride0 = strides[0];
            uint* cur = stackalloc uint[ndims];

            // figure out the dimension index position for start
            System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

            cur[0] = start % dims[0];
            uint f = start / dims[0];
            uint higdims = 0;
            int i = 1;
            for (; f > 0 && i < ndims; i++) {
                cur[i] = f % dims[i];
                f /= dims[i];
                higdims += cur[i] * strides[i];
            }
            while (i < ndims) {
                cur[i++] = 0;
            }

            System.Diagnostics.Debug.Assert(f == 0);

            fcomplex v;
            fcomplex min = fcomplex.NaN;
            fcomplex max = fcomplex.NaN;

            while (true) {

                byte* pIn = pSrc + (higdims + cur[0] * stride0);

                // iteration length limited to either the dimension lengths or the end of the requested chunk 
                uint leadLen = Math.Min(len, dims[0] - cur[0]);
                len -= leadLen;

                #region find non-NaN values
                if (fcomplex.IsNaN(min)) {
                    while (leadLen > 0 && fcomplex.IsNaN(min)) {
                        v = *(fcomplex*)pIn;
                        if (float.IsNaN(min.real)) {
                            if (!float.IsNaN(v.real)) {
                                // initialize
                                min.real = max.real = v.real;
                            }
                        } else {
                            // handle real part regularly
                            if (v.real < min.real) min.real = v.real;
                            if (v.real > max.real) max.real = v.real;
                        }
                        if (float.IsNaN(min.imag)) {
                            if (!float.IsNaN(v.imag)) {
                                // initialize
                                min.imag = max.imag = v.imag;
                            }
                        } else {
                            if (v.imag < min.imag) min.imag = v.imag;
                            if (v.imag > max.imag) max.imag = v.imag;
                        }
                        pIn += stride0;
                        leadLen--;
                    }
                }
                #endregion  

                while (leadLen > 8) {
                    v = *(fcomplex*)(pIn + 0 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(fcomplex*)(pIn + 1 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(fcomplex*)(pIn + 2 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(fcomplex*)(pIn + 3 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(fcomplex*)(pIn + 4 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(fcomplex*)(pIn + 5 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(fcomplex*)(pIn + 6 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(fcomplex*)(pIn + 7 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    pIn += 8 * stride0; leadLen -= 8;
                }
                while (leadLen > 4) {
                    v = *(fcomplex*)(pIn + 0 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(fcomplex*)(pIn + 1 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(fcomplex*)(pIn + 2 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(fcomplex*)(pIn + 3 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    pIn += 4 * stride0; leadLen -= 4;
                }
                while (leadLen-- > 0) {
                    v = *(fcomplex*)(pIn + 0 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    pIn += stride0;
                }
                if (len == 0) {
                    break;
                }
                // reset initial offset in lead dimension after first iteration
                cur[0] = 0;

                // increase higher dims
                int d = 1;
                while (d < ndims) {
                    if (cur[d] < dims[d] - 1) {
                        higdims += strides[d];
                        cur[d]++;
                        break;
                    } else {
                        cur[d] = 0;
                        higdims -= strides[d] * (dims[d] - 1);
                        d++;
                    }
                }
            }
            pOut[0] = min;
            pOut[1] = max;
        }

        internal static unsafe void Strided64(byte* pSrc, fcomplex* pOut, long start, long len, long* bsd) {

            uint ndims = (uint)bsd[0];
            long* dims = bsd + 3;
            long* strides = dims + ndims;
            long stride0 = strides[0];
            long* cur = stackalloc long[(int)ndims];

            // figure out the dimension index position for start
            System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

            cur[0] = start % dims[0];
            long f = start / dims[0];
            long higdims = 0;
            int i = 1;
            for (; f > 0 && i < ndims; i++) {
                cur[i] = f % dims[i];
                f /= dims[i];
                higdims += cur[i] * strides[i];
            }
            while (i < ndims) {
                cur[i++] = 0;
            }

            System.Diagnostics.Debug.Assert(f == 0);

            fcomplex v;
            fcomplex min = fcomplex.NaN;
            fcomplex max = fcomplex.NaN;

            while (true) {

                byte* pIn = pSrc + higdims + cur[0] * stride0;

                // iteration length limited to either the dimension lengths or the end of the requested chunk 
                long leadLen = Math.Min(len, dims[0] - cur[0]);
                len -= leadLen;

                #region find non-NaN values
                if (fcomplex.IsNaN(min)) {
                    while (leadLen > 0 && fcomplex.IsNaN(min)) {
                        v = *(fcomplex*)pIn;
                        if (float.IsNaN(min.real)) {
                            if (!float.IsNaN(v.real)) {
                                // initialize
                                min.real = max.real = v.real;
                            }
                        } else {
                            // handle real part regularly
                            if (v.real < min.real) min.real = v.real;
                            if (v.real > max.real) max.real = v.real;
                        }
                        if (float.IsNaN(min.imag)) {
                            if (!float.IsNaN(v.imag)) {
                                // initialize
                                min.imag = max.imag = v.imag;
                            }
                        } else {
                            if (v.imag < min.imag) min.imag = v.imag;
                            if (v.imag > max.imag) max.imag = v.imag;
                        }
                        pIn += stride0;
                        leadLen--;
                    }
                }
                #endregion  

                while (leadLen > 8) {
                    v = *(fcomplex*)(pIn + 0 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(fcomplex*)(pIn + 1 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(fcomplex*)(pIn + 2 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(fcomplex*)(pIn + 3 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(fcomplex*)(pIn + 4 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(fcomplex*)(pIn + 5 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(fcomplex*)(pIn + 6 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(fcomplex*)(pIn + 7 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    pIn += 8 * stride0; leadLen -= 8;
                }
                while (leadLen > 4) {
                    v = *(fcomplex*)(pIn + 0 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(fcomplex*)(pIn + 1 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(fcomplex*)(pIn + 2 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    v = *(fcomplex*)(pIn + 3 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    pIn += 4 * stride0; leadLen -= 4;
                }
                while (leadLen-- > 0) {
                    v = *(fcomplex*)(pIn + 0 * stride0); if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag;
                    pIn += stride0;
                }
                if (len == 0) {
                    break;
                }
                // reset initial offset in lead dimension after first iteration
                cur[0] = 0;

                // increase higher dims
                int d = 1;
                while (d < ndims) {
                    if (cur[d] < dims[d] - 1) {
                        higdims += strides[d];
                        cur[d]++;
                        break;
                    } else {
                        cur[d] = 0;
                        higdims -= strides[d] * (dims[d] - 1);
                        d++;
                    }
                }
            }
            pOut[0] = min;
            pOut[1] = max;
        }
    }

#endregion HYCALPER AUTO GENERATED CODE

}
namespace ILNumerics.Core.InnerLoops.GetLimitsNoInf {  // complex, fcomplex, ignore Infinity

    #region HYCALPER LOOPSTART
    /*!HC:TYPELIST:
    <hycalper>
    <type>
        <source locate="here">
            complex
        </source>
        <destination>fcomplex</destination>
    </type>
    <type>
        <source locate="here">
            Complex
        </source>
        <destination>FComplex</destination>
    </type>
    <type>
        <source locate="here">
            double
        </source>
        <destination>float</destination>
    </type>
    </hycalper>
    */

    internal static class Complex {

        internal static int s_threading_overhead_Cont64 = 10;
        internal static int s_threading_overhead_Strided32 = 10;
        internal static int s_threading_overhead_Strided64 = 10;

        internal static unsafe Action<IntPtr> Continous64OOPAction = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = p[2].ToInt64();
            var i = locP[1].ToInt64();
            Continous64OOP((byte*)p[0] + len * (uint)sizeof(complex) * i,
                            (/*!HC:outArr*/complex*)p[1] + i * 2,
                            (long)len);
            Interlocked.Decrement(ref *(int*)p[3]);
        };
        internal static unsafe Action<IntPtr> Strided32Action = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = (uint)p[3].ToInt32();
            var i = (uint)locP[1].ToInt32();
            Strided32((byte*)p[0], (/*!HC:outArr*/complex*)p[1] + i * 2, len * i, len, (uint*)p[2]);
            Interlocked.Decrement(ref *(int*)p[4]);
        };
        internal static unsafe Action<IntPtr> Strided64Action = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = (long)p[3].ToInt64();
            var i = (long)locP[1].ToInt64();
            Strided64((byte*)p[0], (/*!HC:outArr*/complex*)p[1] + i * 2, len * i, len, (long*)p[2]);
            Interlocked.Decrement(ref *(int*)p[4]);
        };

        internal static unsafe void Continous64OOP(byte* pIn, /*!HC:outArr*/complex* pOut, long len) {

            System.Diagnostics.Debug.Assert(pIn != (byte*)0);
            System.Diagnostics.Debug.Assert(pOut != (/*!HC:outArr*/complex*)0);

            // floatingpoint types: find the first non-nan value

            complex v, min = complex.NaN, max = complex.NaN;

            #region find non-NaN values
            while (len > 0 && complex.IsNaN(min)) {
                v = *(complex*)pIn;
                if (double.IsNaN(min.real)) {
                    if (!double.IsNaN(v.real) && !double.IsInfinity(v.real)) {
                        // initialize
                        min.real = max.real = v.real;
                    }
                } else {
                    // handle real part regularly
                    if (!double.IsInfinity(v.real)) {
                        if (v.real < min.real) min.real = v.real; 
                        if (v.real > max.real) max.real = v.real;
                    }
                }
                if (double.IsNaN(min.imag)) {
                    if (!double.IsNaN(v.imag) && !double.IsInfinity(v.imag)) {
                        // initialize
                        min.imag = max.imag = v.imag;
                    }
                } else {
                    if (!double.IsInfinity(v.imag)) {
                        if (v.imag < min.imag) min.imag = v.imag;
                        if (v.imag > max.imag) max.imag = v.imag;
                    }
                }
                pIn += sizeof(complex);
                len--;
            }
            if (double.IsNaN(min.real) && double.IsNaN(min.imag)) {
                // all nans or infs
                pOut[0] = complex.NaN;
                pOut[1] = complex.NaN;
                return;
            }
            #endregion  

            while (len >= 8) {
                v = *(complex*)(pIn + 0 * sizeof(complex)); if (!double.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!double.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                v = *(complex*)(pIn + 1 * sizeof(complex)); if (!double.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!double.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                v = *(complex*)(pIn + 2 * sizeof(complex)); if (!double.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!double.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                v = *(complex*)(pIn + 3 * sizeof(complex)); if (!double.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!double.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                v = *(complex*)(pIn + 4 * sizeof(complex)); if (!double.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!double.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                v = *(complex*)(pIn + 5 * sizeof(complex)); if (!double.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!double.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                v = *(complex*)(pIn + 6 * sizeof(complex)); if (!double.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!double.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                v = *(complex*)(pIn + 7 * sizeof(complex)); if (!double.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; }
                if (!double.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                pIn += 8 * sizeof(complex); len -= 8;
            }
            while (len-- > 0) {
                v = *(complex*)pIn;
                if (!double.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; }
                if (!double.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                pIn += sizeof(complex);
            }
            pOut[0] = min;
            pOut[1] = max;
        }

        /// <summary>
        /// Inner ACCUMALL loop for strided, general (non-huge) array.
        /// </summary>
        /// <param name="pSrc">pointer to first element to consider here.</param>
        /// <param name="pOut">Pointer to single element to store result.</param>
        /// <param name="start">First sequential element.</param>
        /// <param name="len">Number of sequential elements to process.</param>
        /// <param name="ordered_bytestrided_bsd">(Re-)ordered BSD of the source array. Strides indicate <i>byte spacings</i>.</param>
        internal static unsafe void Strided32(byte* pSrc, /*!HC:outArr*/complex* pOut, uint start, uint len, uint* ordered_bytestrided_bsd) {

            int ndims = (int)ordered_bytestrided_bsd[0];
            uint* dims = ordered_bytestrided_bsd + 3;
            uint* strides = dims + ndims;
            uint stride0 = strides[0];
            uint* cur = stackalloc uint[ndims];

            // figure out the dimension index position for start
            System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

            cur[0] = start % dims[0];
            uint f = start / dims[0];
            uint higdims = 0;
            int i = 1;
            for (; f > 0 && i < ndims; i++) {
                cur[i] = f % dims[i];
                f /= dims[i];
                higdims += cur[i] * strides[i];
            }
            while (i < ndims) {
                cur[i++] = 0;
            }

            System.Diagnostics.Debug.Assert(f == 0);

            complex v;
            complex min = complex./*!HC:initMinValue*/NaN;
            complex max = complex./*!HC:initMaxValue*/NaN;

            while (true) {

                byte* pIn = pSrc + (higdims + cur[0] * stride0);

                // iteration length limited to either the dimension lengths or the end of the requested chunk 
                uint leadLen = Math.Min(len, dims[0] - cur[0]);
                len -= leadLen;

                #region find non-NaN, non-infinity values
                if (complex.IsNaN(min)) {
                    while (leadLen > 0 && complex.IsNaN(min)) {
                        v = *(complex*)pIn;
                        if (double.IsNaN(min.real)) {
                            if (!double.IsNaN(v.real) && !double.IsInfinity(v.real)) {
                                // initialize
                                min.real = max.real = v.real;
                            }
                        } else {
                            // handle real part regularly
                            if (!double.IsInfinity(v.real)) {
                                if (v.real < min.real) min.real = v.real;
                                if (v.real > max.real) max.real = v.real;
                            }
                        }
                        if (double.IsNaN(min.imag)) {
                            if (!double.IsNaN(v.imag) && !double.IsInfinity(v.imag)) {
                                // initialize
                                min.imag = max.imag = v.imag;
                            }
                        } else {
                            if (!double.IsInfinity(v.imag)) {
                                if (v.imag < min.imag) min.imag = v.imag;
                                if (v.imag > max.imag) max.imag = v.imag;
                            }
                        }
                        pIn += stride0;
                        leadLen--;
                    }
                }
                #endregion  

                while (leadLen > 8) {
                    v = *(complex*)(pIn + 0 * stride0); if (!double.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!double.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(complex*)(pIn + 1 * stride0); if (!double.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!double.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(complex*)(pIn + 2 * stride0); if (!double.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!double.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(complex*)(pIn + 3 * stride0); if (!double.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!double.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(complex*)(pIn + 4 * stride0); if (!double.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!double.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(complex*)(pIn + 5 * stride0); if (!double.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!double.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(complex*)(pIn + 6 * stride0); if (!double.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!double.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(complex*)(pIn + 7 * stride0); if (!double.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!double.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    pIn += 8 * stride0; leadLen -= 8;
                }
                while (leadLen > 4) {
                    v = *(complex*)(pIn + 0 * stride0); if (!double.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!double.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(complex*)(pIn + 1 * stride0); if (!double.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!double.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(complex*)(pIn + 2 * stride0); if (!double.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!double.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(complex*)(pIn + 3 * stride0); if (!double.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!double.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    pIn += 4 * stride0; leadLen -= 4;
                }
                while (leadLen-- > 0) {
                    v = *(complex*)(pIn + 0 * stride0); if (!double.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!double.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    pIn += stride0;
                }
                if (len == 0) {
                    break;
                }
                // reset initial offset in lead dimension after first iteration
                cur[0] = 0;

                // increase higher dims
                int d = 1;
                while (d < ndims) {
                    if (cur[d] < dims[d] - 1) {
                        higdims += strides[d];
                        cur[d]++;
                        break;
                    } else {
                        cur[d] = 0;
                        higdims -= strides[d] * (dims[d] - 1);
                        d++;
                    }
                }
            }
            pOut[0] = min;
            pOut[1] = max;
        }

        internal static unsafe void Strided64(byte* pSrc, /*!HC:outArr*/complex* pOut, long start, long len, long* bsd) {

            uint ndims = (uint)bsd[0];
            long* dims = bsd + 3;
            long* strides = dims + ndims;
            long stride0 = strides[0];
            long* cur = stackalloc long[(int)ndims];

            // figure out the dimension index position for start
            System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

            cur[0] = start % dims[0];
            long f = start / dims[0];
            long higdims = 0;
            int i = 1;
            for (; f > 0 && i < ndims; i++) {
                cur[i] = f % dims[i];
                f /= dims[i];
                higdims += cur[i] * strides[i];
            }
            while (i < ndims) {
                cur[i++] = 0;
            }

            System.Diagnostics.Debug.Assert(f == 0);

            complex v;
            complex min = complex./*!HC:initMinValue*/NaN;
            complex max = complex./*!HC:initMaxValue*/NaN;

            while (true) {

                byte* pIn = pSrc + higdims + cur[0] * stride0;

                // iteration length limited to either the dimension lengths or the end of the requested chunk 
                long leadLen = Math.Min(len, dims[0] - cur[0]);
                len -= leadLen;

                #region find non-NaN, non-infinity values
                if (complex.IsNaN(min)) {
                    while (leadLen > 0 && complex.IsNaN(min)) {
                        v = *(complex*)pIn;
                        if (double.IsNaN(min.real)) {
                            if (!double.IsNaN(v.real) && !double.IsInfinity(v.real)) {
                                // initialize
                                min.real = max.real = v.real;
                            }
                        } else {
                            // handle real part regularly
                            if (!double.IsInfinity(v.real)) {
                                if (v.real < min.real) min.real = v.real;
                                if (v.real > max.real) max.real = v.real;
                            }
                        }
                        if (double.IsNaN(min.imag)) {
                            if (!double.IsNaN(v.imag) && !double.IsInfinity(v.imag)) {
                                // initialize
                                min.imag = max.imag = v.imag;
                            }
                        } else {
                            if (!double.IsInfinity(v.imag)) {
                                if (v.imag < min.imag) min.imag = v.imag;
                                if (v.imag > max.imag) max.imag = v.imag;
                            }
                        }
                        pIn += stride0;
                        leadLen--;
                    }
                }
                #endregion  

                while (leadLen > 8) {
                    v = *(complex*)(pIn + 0 * stride0); if (!double.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!double.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(complex*)(pIn + 1 * stride0); if (!double.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!double.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(complex*)(pIn + 2 * stride0); if (!double.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!double.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(complex*)(pIn + 3 * stride0); if (!double.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!double.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(complex*)(pIn + 4 * stride0); if (!double.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!double.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(complex*)(pIn + 5 * stride0); if (!double.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!double.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(complex*)(pIn + 6 * stride0); if (!double.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!double.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(complex*)(pIn + 7 * stride0); if (!double.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!double.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    pIn += 8 * stride0; leadLen -= 8;
                }
                while (leadLen > 4) {
                    v = *(complex*)(pIn + 0 * stride0); if (!double.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!double.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(complex*)(pIn + 1 * stride0); if (!double.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!double.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(complex*)(pIn + 2 * stride0); if (!double.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!double.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(complex*)(pIn + 3 * stride0); if (!double.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!double.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    pIn += 4 * stride0; leadLen -= 4;
                }
                while (leadLen-- > 0) {
                    v = *(complex*)(pIn + 0 * stride0); if (!double.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!double.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    pIn += stride0;
                }
                if (len == 0) {
                    break;
                }
                // reset initial offset in lead dimension after first iteration
                cur[0] = 0;

                // increase higher dims
                int d = 1;
                while (d < ndims) {
                    if (cur[d] < dims[d] - 1) {
                        higdims += strides[d];
                        cur[d]++;
                        break;
                    } else {
                        cur[d] = 0;
                        higdims -= strides[d] * (dims[d] - 1);
                        d++;
                    }
                }
            }
            pOut[0] = min;
            pOut[1] = max;
        }
    }
    #endregion HYCALPER LOOPEND 
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
   

    internal static class FComplex {

        internal static int s_threading_overhead_Cont64 = 10;
        internal static int s_threading_overhead_Strided32 = 10;
        internal static int s_threading_overhead_Strided64 = 10;

        internal static unsafe Action<IntPtr> Continous64OOPAction = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = p[2].ToInt64();
            var i = locP[1].ToInt64();
            Continous64OOP((byte*)p[0] + len * (uint)sizeof(fcomplex) * i,
                            (fcomplex*)p[1] + i * 2,
                            (long)len);
            Interlocked.Decrement(ref *(int*)p[3]);
        };
        internal static unsafe Action<IntPtr> Strided32Action = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = (uint)p[3].ToInt32();
            var i = (uint)locP[1].ToInt32();
            Strided32((byte*)p[0], (fcomplex*)p[1] + i * 2, len * i, len, (uint*)p[2]);
            Interlocked.Decrement(ref *(int*)p[4]);
        };
        internal static unsafe Action<IntPtr> Strided64Action = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = (long)p[3].ToInt64();
            var i = (long)locP[1].ToInt64();
            Strided64((byte*)p[0], (fcomplex*)p[1] + i * 2, len * i, len, (long*)p[2]);
            Interlocked.Decrement(ref *(int*)p[4]);
        };

        internal static unsafe void Continous64OOP(byte* pIn, fcomplex* pOut, long len) {

            System.Diagnostics.Debug.Assert(pIn != (byte*)0);
            System.Diagnostics.Debug.Assert(pOut != (fcomplex*)0);

            // floatingpoint types: find the first non-nan value

            fcomplex v, min = fcomplex.NaN, max = fcomplex.NaN;

            #region find non-NaN values
            while (len > 0 && fcomplex.IsNaN(min)) {
                v = *(fcomplex*)pIn;
                if (float.IsNaN(min.real)) {
                    if (!float.IsNaN(v.real) && !float.IsInfinity(v.real)) {
                        // initialize
                        min.real = max.real = v.real;
                    }
                } else {
                    // handle real part regularly
                    if (!float.IsInfinity(v.real)) {
                        if (v.real < min.real) min.real = v.real; 
                        if (v.real > max.real) max.real = v.real;
                    }
                }
                if (float.IsNaN(min.imag)) {
                    if (!float.IsNaN(v.imag) && !float.IsInfinity(v.imag)) {
                        // initialize
                        min.imag = max.imag = v.imag;
                    }
                } else {
                    if (!float.IsInfinity(v.imag)) {
                        if (v.imag < min.imag) min.imag = v.imag;
                        if (v.imag > max.imag) max.imag = v.imag;
                    }
                }
                pIn += sizeof(fcomplex);
                len--;
            }
            if (float.IsNaN(min.real) && float.IsNaN(min.imag)) {
                // all nans or infs
                pOut[0] = fcomplex.NaN;
                pOut[1] = fcomplex.NaN;
                return;
            }
            #endregion  

            while (len >= 8) {
                v = *(fcomplex*)(pIn + 0 * sizeof(fcomplex)); if (!float.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!float.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                v = *(fcomplex*)(pIn + 1 * sizeof(fcomplex)); if (!float.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!float.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                v = *(fcomplex*)(pIn + 2 * sizeof(fcomplex)); if (!float.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!float.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                v = *(fcomplex*)(pIn + 3 * sizeof(fcomplex)); if (!float.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!float.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                v = *(fcomplex*)(pIn + 4 * sizeof(fcomplex)); if (!float.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!float.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                v = *(fcomplex*)(pIn + 5 * sizeof(fcomplex)); if (!float.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!float.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                v = *(fcomplex*)(pIn + 6 * sizeof(fcomplex)); if (!float.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!float.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                v = *(fcomplex*)(pIn + 7 * sizeof(fcomplex)); if (!float.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; }
                if (!float.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                pIn += 8 * sizeof(fcomplex); len -= 8;
            }
            while (len-- > 0) {
                v = *(fcomplex*)pIn;
                if (!float.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; }
                if (!float.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                pIn += sizeof(fcomplex);
            }
            pOut[0] = min;
            pOut[1] = max;
        }

        /// <summary>
        /// Inner ACCUMALL loop for strided, general (non-huge) array.
        /// </summary>
        /// <param name="pSrc">pointer to first element to consider here.</param>
        /// <param name="pOut">Pointer to single element to store result.</param>
        /// <param name="start">First sequential element.</param>
        /// <param name="len">Number of sequential elements to process.</param>
        /// <param name="ordered_bytestrided_bsd">(Re-)ordered BSD of the source array. Strides indicate <i>byte spacings</i>.</param>
        internal static unsafe void Strided32(byte* pSrc, fcomplex* pOut, uint start, uint len, uint* ordered_bytestrided_bsd) {

            int ndims = (int)ordered_bytestrided_bsd[0];
            uint* dims = ordered_bytestrided_bsd + 3;
            uint* strides = dims + ndims;
            uint stride0 = strides[0];
            uint* cur = stackalloc uint[ndims];

            // figure out the dimension index position for start
            System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

            cur[0] = start % dims[0];
            uint f = start / dims[0];
            uint higdims = 0;
            int i = 1;
            for (; f > 0 && i < ndims; i++) {
                cur[i] = f % dims[i];
                f /= dims[i];
                higdims += cur[i] * strides[i];
            }
            while (i < ndims) {
                cur[i++] = 0;
            }

            System.Diagnostics.Debug.Assert(f == 0);

            fcomplex v;
            fcomplex min = fcomplex.NaN;
            fcomplex max = fcomplex.NaN;

            while (true) {

                byte* pIn = pSrc + (higdims + cur[0] * stride0);

                // iteration length limited to either the dimension lengths or the end of the requested chunk 
                uint leadLen = Math.Min(len, dims[0] - cur[0]);
                len -= leadLen;

                #region find non-NaN, non-infinity values
                if (fcomplex.IsNaN(min)) {
                    while (leadLen > 0 && fcomplex.IsNaN(min)) {
                        v = *(fcomplex*)pIn;
                        if (float.IsNaN(min.real)) {
                            if (!float.IsNaN(v.real) && !float.IsInfinity(v.real)) {
                                // initialize
                                min.real = max.real = v.real;
                            }
                        } else {
                            // handle real part regularly
                            if (!float.IsInfinity(v.real)) {
                                if (v.real < min.real) min.real = v.real;
                                if (v.real > max.real) max.real = v.real;
                            }
                        }
                        if (float.IsNaN(min.imag)) {
                            if (!float.IsNaN(v.imag) && !float.IsInfinity(v.imag)) {
                                // initialize
                                min.imag = max.imag = v.imag;
                            }
                        } else {
                            if (!float.IsInfinity(v.imag)) {
                                if (v.imag < min.imag) min.imag = v.imag;
                                if (v.imag > max.imag) max.imag = v.imag;
                            }
                        }
                        pIn += stride0;
                        leadLen--;
                    }
                }
                #endregion  

                while (leadLen > 8) {
                    v = *(fcomplex*)(pIn + 0 * stride0); if (!float.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!float.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(fcomplex*)(pIn + 1 * stride0); if (!float.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!float.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(fcomplex*)(pIn + 2 * stride0); if (!float.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!float.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(fcomplex*)(pIn + 3 * stride0); if (!float.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!float.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(fcomplex*)(pIn + 4 * stride0); if (!float.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!float.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(fcomplex*)(pIn + 5 * stride0); if (!float.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!float.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(fcomplex*)(pIn + 6 * stride0); if (!float.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!float.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(fcomplex*)(pIn + 7 * stride0); if (!float.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!float.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    pIn += 8 * stride0; leadLen -= 8;
                }
                while (leadLen > 4) {
                    v = *(fcomplex*)(pIn + 0 * stride0); if (!float.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!float.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(fcomplex*)(pIn + 1 * stride0); if (!float.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!float.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(fcomplex*)(pIn + 2 * stride0); if (!float.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!float.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(fcomplex*)(pIn + 3 * stride0); if (!float.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!float.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    pIn += 4 * stride0; leadLen -= 4;
                }
                while (leadLen-- > 0) {
                    v = *(fcomplex*)(pIn + 0 * stride0); if (!float.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!float.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    pIn += stride0;
                }
                if (len == 0) {
                    break;
                }
                // reset initial offset in lead dimension after first iteration
                cur[0] = 0;

                // increase higher dims
                int d = 1;
                while (d < ndims) {
                    if (cur[d] < dims[d] - 1) {
                        higdims += strides[d];
                        cur[d]++;
                        break;
                    } else {
                        cur[d] = 0;
                        higdims -= strides[d] * (dims[d] - 1);
                        d++;
                    }
                }
            }
            pOut[0] = min;
            pOut[1] = max;
        }

        internal static unsafe void Strided64(byte* pSrc, fcomplex* pOut, long start, long len, long* bsd) {

            uint ndims = (uint)bsd[0];
            long* dims = bsd + 3;
            long* strides = dims + ndims;
            long stride0 = strides[0];
            long* cur = stackalloc long[(int)ndims];

            // figure out the dimension index position for start
            System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

            cur[0] = start % dims[0];
            long f = start / dims[0];
            long higdims = 0;
            int i = 1;
            for (; f > 0 && i < ndims; i++) {
                cur[i] = f % dims[i];
                f /= dims[i];
                higdims += cur[i] * strides[i];
            }
            while (i < ndims) {
                cur[i++] = 0;
            }

            System.Diagnostics.Debug.Assert(f == 0);

            fcomplex v;
            fcomplex min = fcomplex.NaN;
            fcomplex max = fcomplex.NaN;

            while (true) {

                byte* pIn = pSrc + higdims + cur[0] * stride0;

                // iteration length limited to either the dimension lengths or the end of the requested chunk 
                long leadLen = Math.Min(len, dims[0] - cur[0]);
                len -= leadLen;

                #region find non-NaN, non-infinity values
                if (fcomplex.IsNaN(min)) {
                    while (leadLen > 0 && fcomplex.IsNaN(min)) {
                        v = *(fcomplex*)pIn;
                        if (float.IsNaN(min.real)) {
                            if (!float.IsNaN(v.real) && !float.IsInfinity(v.real)) {
                                // initialize
                                min.real = max.real = v.real;
                            }
                        } else {
                            // handle real part regularly
                            if (!float.IsInfinity(v.real)) {
                                if (v.real < min.real) min.real = v.real;
                                if (v.real > max.real) max.real = v.real;
                            }
                        }
                        if (float.IsNaN(min.imag)) {
                            if (!float.IsNaN(v.imag) && !float.IsInfinity(v.imag)) {
                                // initialize
                                min.imag = max.imag = v.imag;
                            }
                        } else {
                            if (!float.IsInfinity(v.imag)) {
                                if (v.imag < min.imag) min.imag = v.imag;
                                if (v.imag > max.imag) max.imag = v.imag;
                            }
                        }
                        pIn += stride0;
                        leadLen--;
                    }
                }
                #endregion  

                while (leadLen > 8) {
                    v = *(fcomplex*)(pIn + 0 * stride0); if (!float.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!float.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(fcomplex*)(pIn + 1 * stride0); if (!float.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!float.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(fcomplex*)(pIn + 2 * stride0); if (!float.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!float.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(fcomplex*)(pIn + 3 * stride0); if (!float.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!float.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(fcomplex*)(pIn + 4 * stride0); if (!float.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!float.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(fcomplex*)(pIn + 5 * stride0); if (!float.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!float.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(fcomplex*)(pIn + 6 * stride0); if (!float.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!float.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(fcomplex*)(pIn + 7 * stride0); if (!float.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!float.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    pIn += 8 * stride0; leadLen -= 8;
                }
                while (leadLen > 4) {
                    v = *(fcomplex*)(pIn + 0 * stride0); if (!float.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!float.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(fcomplex*)(pIn + 1 * stride0); if (!float.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!float.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(fcomplex*)(pIn + 2 * stride0); if (!float.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!float.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    v = *(fcomplex*)(pIn + 3 * stride0); if (!float.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!float.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    pIn += 4 * stride0; leadLen -= 4;
                }
                while (leadLen-- > 0) {
                    v = *(fcomplex*)(pIn + 0 * stride0); if (!float.IsInfinity(v.real)) { if (v.real < min.real) min.real = v.real; if (v.real > max.real) max.real = v.real; } if (!float.IsInfinity(v.imag)) { if (v.imag < min.imag) min.imag = v.imag; if (v.imag > max.imag) max.imag = v.imag; }
                    pIn += stride0;
                }
                if (len == 0) {
                    break;
                }
                // reset initial offset in lead dimension after first iteration
                cur[0] = 0;

                // increase higher dims
                int d = 1;
                while (d < ndims) {
                    if (cur[d] < dims[d] - 1) {
                        higdims += strides[d];
                        cur[d]++;
                        break;
                    } else {
                        cur[d] = 0;
                        higdims -= strides[d] * (dims[d] - 1);
                        d++;
                    }
                }
            }
            pOut[0] = min;
            pOut[1] = max;
        }
    }

#endregion HYCALPER AUTO GENERATED CODE

}

namespace ILNumerics.Core.InnerLoops.GetLimits {

    #region HYCALPER LOOPSTART
/*!HC:TYPELIST:
<hycalper>
<type>
    <source locate="here">
        sbyte
    </source>
    <destination>int</destination>
    <destination>byte</destination>
    <destination>short</destination>
    <destination>ushort</destination>
    <destination>uint</destination>
    <destination>long</destination>
    <destination>ulong</destination>
</type>
<type>
    <source locate="here">
        Sbyte
    </source>
    <destination>Int32</destination>
    <destination>Byte</destination>
    <destination>Int16</destination>
    <destination>UInt16</destination>
    <destination>UInt32</destination>
    <destination>Int64</destination>
    <destination>UInt64</destination>
</type>
</hycalper>
*/

    internal static class Sbyte {

        internal static int s_threading_overhead_Cont64 = 10;  
        internal static int s_threading_overhead_Strided32 = 10;  
        internal static int s_threading_overhead_Strided64 = 10; 

        internal static unsafe Action<IntPtr> Continous64OOPAction = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = p[2].ToInt64();
            var i = locP[1].ToInt64();
            Continous64OOP((byte*)p[0] + len * (uint)sizeof(sbyte) * i,
                            (/*!HC:outArr*/sbyte*)p[1] + i * 2,
                            (long)len);
            Interlocked.Decrement(ref *(int*)p[3]);
        };
        internal static unsafe Action<IntPtr> Strided32Action = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = (uint)p[3].ToInt32();
            var i = (uint)locP[1].ToInt32();
            Strided32((byte*)p[0], (/*!HC:outArr*/sbyte*)p[1] + i * 2, len * i, len, (uint*)p[2]);
            Interlocked.Decrement(ref *(int*)p[4]);
        };
        internal static unsafe Action<IntPtr> Strided64Action = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = (long)p[3].ToInt64();
            var i = (long)locP[1].ToInt64();
            Strided64((byte*)p[0], (/*!HC:outArr*/sbyte*)p[1] + i * 2, len * i, len, (long*)p[2]);
            Interlocked.Decrement(ref *(int*)p[4]);
        };
        internal static unsafe void Continous64OOP(byte* pIn, sbyte* pOut, long len) {

            System.Diagnostics.Debug.Assert(pIn != (byte*)0);
            System.Diagnostics.Debug.Assert(pOut != (sbyte*)0);

            sbyte v, min = *(sbyte*)pIn, max = min;
            while (len >= 8) {
                v = *(sbyte*)(pIn + 0 * sizeof(sbyte)); if (v < min) min = v; if (v > max) max = v;
                v = *(sbyte*)(pIn + 1 * sizeof(sbyte)); if (v < min) min = v; if (v > max) max = v;
                v = *(sbyte*)(pIn + 2 * sizeof(sbyte)); if (v < min) min = v; if (v > max) max = v;
                v = *(sbyte*)(pIn + 3 * sizeof(sbyte)); if (v < min) min = v; if (v > max) max = v;
                v = *(sbyte*)(pIn + 4 * sizeof(sbyte)); if (v < min) min = v; if (v > max) max = v;
                v = *(sbyte*)(pIn + 5 * sizeof(sbyte)); if (v < min) min = v; if (v > max) max = v;
                v = *(sbyte*)(pIn + 6 * sizeof(sbyte)); if (v < min) min = v; if (v > max) max = v;
                v = *(sbyte*)(pIn + 7 * sizeof(sbyte)); if (v < min) min = v; if (v > max) max = v;
                pIn += 8 * sizeof(sbyte); len -= 8;
            }
            while (len-- > 0) {
                v = *(sbyte*)pIn; if (v < min) min = v; if (v > max) max = v;
                pIn += sizeof(sbyte);
            }
            pOut[0] = min;
            pOut[1] = max;
        }

        /// <summary>
        /// Inner ACCUMALL loop for strided, general (non-huge) array.
        /// </summary>
        /// <param name="pSrc">pointer to first element to consider here.</param>
        /// <param name="pOut">Pointer to single element to store result.</param>
        /// <param name="start">First sequential element.</param>
        /// <param name="len">Number of sequential elements to process.</param>
        /// <param name="ordered_bytestrided_bsd">(Re-)ordered BSD of the source array. Strides indicate <i>byte spacings</i>.</param>
        internal static unsafe void Strided32(byte* pSrc, sbyte* pOut, uint start, uint len, uint* ordered_bytestrided_bsd) {

            int ndims = (int)ordered_bytestrided_bsd[0];
            uint* dims = ordered_bytestrided_bsd + 3;
            uint* strides = dims + ndims;
            uint stride0 = strides[0];
            uint* cur = stackalloc uint[ndims];

            // figure out the dimension index position for start
            System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

            cur[0] = start % dims[0];
            uint f = start / dims[0];
            uint higdims = 0;
            int i = 1; 
            for (; f > 0 && i < ndims; i++) {
                cur[i] = f % dims[i];
                f /= dims[i];
                higdims += cur[i] * strides[i];
            }
            while (i < ndims) {
                cur[i++] = 0;
            }

            System.Diagnostics.Debug.Assert(f == 0);

            sbyte v;
            sbyte min = sbyte.MaxValue;
            sbyte max = sbyte.MinValue;

            while (true) {

                byte* pIn = pSrc + (higdims + cur[0] * stride0);

                // iteration length limited to either the dimension lengths or the end of the requested chunk 
                uint leadLen = Math.Min(len, dims[0] - cur[0]);
                len -= leadLen;

                while (leadLen > 8) {
                    v = *(sbyte*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(sbyte*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(sbyte*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(sbyte*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(sbyte*)(pIn + 4 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(sbyte*)(pIn + 5 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(sbyte*)(pIn + 6 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(sbyte*)(pIn + 7 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 8 * stride0; leadLen -= 8;
                }
                while (leadLen > 4) {
                    v = *(sbyte*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(sbyte*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(sbyte*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(sbyte*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 4 * stride0; leadLen -= 4;
                }
                while (leadLen-- > 0) {
                    v = *(sbyte*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += stride0;
                }
                if (len == 0) {
                    break;
                }
                // reset initial offset in lead dimension after first iteration
                cur[0] = 0;

                // increase higher dims
                int d = 1;
                while (d < ndims) {
                    if (cur[d] < dims[d] - 1) {
                        higdims += strides[d];
                        cur[d]++;
                        break;
                    } else {
                        cur[d] = 0;
                        higdims -= strides[d] * (dims[d] - 1);
                        d++;
                    }
                }
            }
            pOut[0] = min;
            pOut[1] = max;
        }

        internal static unsafe void Strided64(byte* pSrc, sbyte* pOut, long start, long len, long* bsd) {

            uint ndims = (uint)bsd[0];
            long* dims = bsd + 3;
            long* strides = dims + ndims;
            long stride0 = strides[0];
            long* cur = stackalloc long[(int)ndims];

            // figure out the dimension index position for start
            System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

            cur[0] = start % dims[0];
            long f = start / dims[0];
            long higdims = 0;
            int i = 1;
            for (; f > 0 && i < ndims; i++) {
                cur[i] = f % dims[i];
                f /= dims[i];
                higdims += cur[i] * strides[i];
            }
            while (i < ndims) {
                cur[i++] = 0;
            }

            System.Diagnostics.Debug.Assert(f == 0);

            sbyte v;
            sbyte min = sbyte.MaxValue;
            sbyte max = sbyte.MinValue;

            while (true) {

                byte* pIn = pSrc + higdims + cur[0] * stride0;

                // iteration length limited to either the dimension lengths or the end of the requested chunk 
                long leadLen = Math.Min(len, dims[0] - cur[0]);
                len -= leadLen;

                while (leadLen > 8) {
                    v = *(sbyte*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(sbyte*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(sbyte*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(sbyte*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(sbyte*)(pIn + 4 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(sbyte*)(pIn + 5 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(sbyte*)(pIn + 6 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(sbyte*)(pIn + 7 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 8 * stride0; leadLen -= 8;
                }
                while (leadLen > 4) {
                    v = *(sbyte*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(sbyte*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(sbyte*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(sbyte*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 4 * stride0; leadLen -= 4;
                }
                while (leadLen-- > 0) {
                    v = *(sbyte*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += stride0;
                }
                if (len == 0) {
                    break;
                }
                // reset initial offset in lead dimension after first iteration
                cur[0] = 0;

                // increase higher dims
                int d = 1;
                while (d < ndims) {
                    if (cur[d] < dims[d] - 1) {
                        higdims += strides[d];
                        cur[d]++;
                        break;
                    } else {
                        cur[d] = 0;
                        higdims -= strides[d] * (dims[d] - 1);
                        d++;
                    }
                }
            }
            pOut[0] = min;
            pOut[1] = max;
        }
    }
    #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 


    internal static class UInt64 {

        internal static int s_threading_overhead_Cont64 = 10;  
        internal static int s_threading_overhead_Strided32 = 10;  
        internal static int s_threading_overhead_Strided64 = 10; 

        internal static unsafe Action<IntPtr> Continous64OOPAction = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = p[2].ToInt64();
            var i = locP[1].ToInt64();
            Continous64OOP((byte*)p[0] + len * (uint)sizeof(ulong) * i,
                            (ulong*)p[1] + i * 2,
                            (long)len);
            Interlocked.Decrement(ref *(int*)p[3]);
        };
        internal static unsafe Action<IntPtr> Strided32Action = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = (uint)p[3].ToInt32();
            var i = (uint)locP[1].ToInt32();
            Strided32((byte*)p[0], (ulong*)p[1] + i * 2, len * i, len, (uint*)p[2]);
            Interlocked.Decrement(ref *(int*)p[4]);
        };
        internal static unsafe Action<IntPtr> Strided64Action = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = (long)p[3].ToInt64();
            var i = (long)locP[1].ToInt64();
            Strided64((byte*)p[0], (ulong*)p[1] + i * 2, len * i, len, (long*)p[2]);
            Interlocked.Decrement(ref *(int*)p[4]);
        };
        internal static unsafe void Continous64OOP(byte* pIn, ulong* pOut, long len) {

            System.Diagnostics.Debug.Assert(pIn != (byte*)0);
            System.Diagnostics.Debug.Assert(pOut != (ulong*)0);

            ulong v, min = *(ulong*)pIn, max = min;
            while (len >= 8) {
                v = *(ulong*)(pIn + 0 * sizeof(ulong)); if (v < min) min = v; if (v > max) max = v;
                v = *(ulong*)(pIn + 1 * sizeof(ulong)); if (v < min) min = v; if (v > max) max = v;
                v = *(ulong*)(pIn + 2 * sizeof(ulong)); if (v < min) min = v; if (v > max) max = v;
                v = *(ulong*)(pIn + 3 * sizeof(ulong)); if (v < min) min = v; if (v > max) max = v;
                v = *(ulong*)(pIn + 4 * sizeof(ulong)); if (v < min) min = v; if (v > max) max = v;
                v = *(ulong*)(pIn + 5 * sizeof(ulong)); if (v < min) min = v; if (v > max) max = v;
                v = *(ulong*)(pIn + 6 * sizeof(ulong)); if (v < min) min = v; if (v > max) max = v;
                v = *(ulong*)(pIn + 7 * sizeof(ulong)); if (v < min) min = v; if (v > max) max = v;
                pIn += 8 * sizeof(ulong); len -= 8;
            }
            while (len-- > 0) {
                v = *(ulong*)pIn; if (v < min) min = v; if (v > max) max = v;
                pIn += sizeof(ulong);
            }
            pOut[0] = min;
            pOut[1] = max;
        }

        /// <summary>
        /// Inner ACCUMALL loop for strided, general (non-huge) array.
        /// </summary>
        /// <param name="pSrc">pointer to first element to consider here.</param>
        /// <param name="pOut">Pointer to single element to store result.</param>
        /// <param name="start">First sequential element.</param>
        /// <param name="len">Number of sequential elements to process.</param>
        /// <param name="ordered_bytestrided_bsd">(Re-)ordered BSD of the source array. Strides indicate <i>byte spacings</i>.</param>
        internal static unsafe void Strided32(byte* pSrc, ulong* pOut, uint start, uint len, uint* ordered_bytestrided_bsd) {

            int ndims = (int)ordered_bytestrided_bsd[0];
            uint* dims = ordered_bytestrided_bsd + 3;
            uint* strides = dims + ndims;
            uint stride0 = strides[0];
            uint* cur = stackalloc uint[ndims];

            // figure out the dimension index position for start
            System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

            cur[0] = start % dims[0];
            uint f = start / dims[0];
            uint higdims = 0;
            int i = 1; 
            for (; f > 0 && i < ndims; i++) {
                cur[i] = f % dims[i];
                f /= dims[i];
                higdims += cur[i] * strides[i];
            }
            while (i < ndims) {
                cur[i++] = 0;
            }

            System.Diagnostics.Debug.Assert(f == 0);

            ulong v;
            ulong min = ulong.MaxValue;
            ulong max = ulong.MinValue;

            while (true) {

                byte* pIn = pSrc + (higdims + cur[0] * stride0);

                // iteration length limited to either the dimension lengths or the end of the requested chunk 
                uint leadLen = Math.Min(len, dims[0] - cur[0]);
                len -= leadLen;

                while (leadLen > 8) {
                    v = *(ulong*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ulong*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ulong*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ulong*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ulong*)(pIn + 4 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ulong*)(pIn + 5 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ulong*)(pIn + 6 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ulong*)(pIn + 7 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 8 * stride0; leadLen -= 8;
                }
                while (leadLen > 4) {
                    v = *(ulong*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ulong*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ulong*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ulong*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 4 * stride0; leadLen -= 4;
                }
                while (leadLen-- > 0) {
                    v = *(ulong*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += stride0;
                }
                if (len == 0) {
                    break;
                }
                // reset initial offset in lead dimension after first iteration
                cur[0] = 0;

                // increase higher dims
                int d = 1;
                while (d < ndims) {
                    if (cur[d] < dims[d] - 1) {
                        higdims += strides[d];
                        cur[d]++;
                        break;
                    } else {
                        cur[d] = 0;
                        higdims -= strides[d] * (dims[d] - 1);
                        d++;
                    }
                }
            }
            pOut[0] = min;
            pOut[1] = max;
        }

        internal static unsafe void Strided64(byte* pSrc, ulong* pOut, long start, long len, long* bsd) {

            uint ndims = (uint)bsd[0];
            long* dims = bsd + 3;
            long* strides = dims + ndims;
            long stride0 = strides[0];
            long* cur = stackalloc long[(int)ndims];

            // figure out the dimension index position for start
            System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

            cur[0] = start % dims[0];
            long f = start / dims[0];
            long higdims = 0;
            int i = 1;
            for (; f > 0 && i < ndims; i++) {
                cur[i] = f % dims[i];
                f /= dims[i];
                higdims += cur[i] * strides[i];
            }
            while (i < ndims) {
                cur[i++] = 0;
            }

            System.Diagnostics.Debug.Assert(f == 0);

            ulong v;
            ulong min = ulong.MaxValue;
            ulong max = ulong.MinValue;

            while (true) {

                byte* pIn = pSrc + higdims + cur[0] * stride0;

                // iteration length limited to either the dimension lengths or the end of the requested chunk 
                long leadLen = Math.Min(len, dims[0] - cur[0]);
                len -= leadLen;

                while (leadLen > 8) {
                    v = *(ulong*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ulong*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ulong*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ulong*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ulong*)(pIn + 4 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ulong*)(pIn + 5 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ulong*)(pIn + 6 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ulong*)(pIn + 7 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 8 * stride0; leadLen -= 8;
                }
                while (leadLen > 4) {
                    v = *(ulong*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ulong*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ulong*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ulong*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 4 * stride0; leadLen -= 4;
                }
                while (leadLen-- > 0) {
                    v = *(ulong*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += stride0;
                }
                if (len == 0) {
                    break;
                }
                // reset initial offset in lead dimension after first iteration
                cur[0] = 0;

                // increase higher dims
                int d = 1;
                while (d < ndims) {
                    if (cur[d] < dims[d] - 1) {
                        higdims += strides[d];
                        cur[d]++;
                        break;
                    } else {
                        cur[d] = 0;
                        higdims -= strides[d] * (dims[d] - 1);
                        d++;
                    }
                }
            }
            pOut[0] = min;
            pOut[1] = max;
        }
    }


    internal static class Int64 {

        internal static int s_threading_overhead_Cont64 = 10;  
        internal static int s_threading_overhead_Strided32 = 10;  
        internal static int s_threading_overhead_Strided64 = 10; 

        internal static unsafe Action<IntPtr> Continous64OOPAction = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = p[2].ToInt64();
            var i = locP[1].ToInt64();
            Continous64OOP((byte*)p[0] + len * (uint)sizeof(long) * i,
                            (long*)p[1] + i * 2,
                            (long)len);
            Interlocked.Decrement(ref *(int*)p[3]);
        };
        internal static unsafe Action<IntPtr> Strided32Action = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = (uint)p[3].ToInt32();
            var i = (uint)locP[1].ToInt32();
            Strided32((byte*)p[0], (long*)p[1] + i * 2, len * i, len, (uint*)p[2]);
            Interlocked.Decrement(ref *(int*)p[4]);
        };
        internal static unsafe Action<IntPtr> Strided64Action = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = (long)p[3].ToInt64();
            var i = (long)locP[1].ToInt64();
            Strided64((byte*)p[0], (long*)p[1] + i * 2, len * i, len, (long*)p[2]);
            Interlocked.Decrement(ref *(int*)p[4]);
        };
        internal static unsafe void Continous64OOP(byte* pIn, long* pOut, long len) {

            System.Diagnostics.Debug.Assert(pIn != (byte*)0);
            System.Diagnostics.Debug.Assert(pOut != (long*)0);

            long v, min = *(long*)pIn, max = min;
            while (len >= 8) {
                v = *(long*)(pIn + 0 * sizeof(long)); if (v < min) min = v; if (v > max) max = v;
                v = *(long*)(pIn + 1 * sizeof(long)); if (v < min) min = v; if (v > max) max = v;
                v = *(long*)(pIn + 2 * sizeof(long)); if (v < min) min = v; if (v > max) max = v;
                v = *(long*)(pIn + 3 * sizeof(long)); if (v < min) min = v; if (v > max) max = v;
                v = *(long*)(pIn + 4 * sizeof(long)); if (v < min) min = v; if (v > max) max = v;
                v = *(long*)(pIn + 5 * sizeof(long)); if (v < min) min = v; if (v > max) max = v;
                v = *(long*)(pIn + 6 * sizeof(long)); if (v < min) min = v; if (v > max) max = v;
                v = *(long*)(pIn + 7 * sizeof(long)); if (v < min) min = v; if (v > max) max = v;
                pIn += 8 * sizeof(long); len -= 8;
            }
            while (len-- > 0) {
                v = *(long*)pIn; if (v < min) min = v; if (v > max) max = v;
                pIn += sizeof(long);
            }
            pOut[0] = min;
            pOut[1] = max;
        }

        /// <summary>
        /// Inner ACCUMALL loop for strided, general (non-huge) array.
        /// </summary>
        /// <param name="pSrc">pointer to first element to consider here.</param>
        /// <param name="pOut">Pointer to single element to store result.</param>
        /// <param name="start">First sequential element.</param>
        /// <param name="len">Number of sequential elements to process.</param>
        /// <param name="ordered_bytestrided_bsd">(Re-)ordered BSD of the source array. Strides indicate <i>byte spacings</i>.</param>
        internal static unsafe void Strided32(byte* pSrc, long* pOut, uint start, uint len, uint* ordered_bytestrided_bsd) {

            int ndims = (int)ordered_bytestrided_bsd[0];
            uint* dims = ordered_bytestrided_bsd + 3;
            uint* strides = dims + ndims;
            uint stride0 = strides[0];
            uint* cur = stackalloc uint[ndims];

            // figure out the dimension index position for start
            System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

            cur[0] = start % dims[0];
            uint f = start / dims[0];
            uint higdims = 0;
            int i = 1; 
            for (; f > 0 && i < ndims; i++) {
                cur[i] = f % dims[i];
                f /= dims[i];
                higdims += cur[i] * strides[i];
            }
            while (i < ndims) {
                cur[i++] = 0;
            }

            System.Diagnostics.Debug.Assert(f == 0);

            long v;
            long min = long.MaxValue;
            long max = long.MinValue;

            while (true) {

                byte* pIn = pSrc + (higdims + cur[0] * stride0);

                // iteration length limited to either the dimension lengths or the end of the requested chunk 
                uint leadLen = Math.Min(len, dims[0] - cur[0]);
                len -= leadLen;

                while (leadLen > 8) {
                    v = *(long*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(long*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(long*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(long*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(long*)(pIn + 4 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(long*)(pIn + 5 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(long*)(pIn + 6 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(long*)(pIn + 7 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 8 * stride0; leadLen -= 8;
                }
                while (leadLen > 4) {
                    v = *(long*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(long*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(long*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(long*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 4 * stride0; leadLen -= 4;
                }
                while (leadLen-- > 0) {
                    v = *(long*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += stride0;
                }
                if (len == 0) {
                    break;
                }
                // reset initial offset in lead dimension after first iteration
                cur[0] = 0;

                // increase higher dims
                int d = 1;
                while (d < ndims) {
                    if (cur[d] < dims[d] - 1) {
                        higdims += strides[d];
                        cur[d]++;
                        break;
                    } else {
                        cur[d] = 0;
                        higdims -= strides[d] * (dims[d] - 1);
                        d++;
                    }
                }
            }
            pOut[0] = min;
            pOut[1] = max;
        }

        internal static unsafe void Strided64(byte* pSrc, long* pOut, long start, long len, long* bsd) {

            uint ndims = (uint)bsd[0];
            long* dims = bsd + 3;
            long* strides = dims + ndims;
            long stride0 = strides[0];
            long* cur = stackalloc long[(int)ndims];

            // figure out the dimension index position for start
            System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

            cur[0] = start % dims[0];
            long f = start / dims[0];
            long higdims = 0;
            int i = 1;
            for (; f > 0 && i < ndims; i++) {
                cur[i] = f % dims[i];
                f /= dims[i];
                higdims += cur[i] * strides[i];
            }
            while (i < ndims) {
                cur[i++] = 0;
            }

            System.Diagnostics.Debug.Assert(f == 0);

            long v;
            long min = long.MaxValue;
            long max = long.MinValue;

            while (true) {

                byte* pIn = pSrc + higdims + cur[0] * stride0;

                // iteration length limited to either the dimension lengths or the end of the requested chunk 
                long leadLen = Math.Min(len, dims[0] - cur[0]);
                len -= leadLen;

                while (leadLen > 8) {
                    v = *(long*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(long*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(long*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(long*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(long*)(pIn + 4 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(long*)(pIn + 5 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(long*)(pIn + 6 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(long*)(pIn + 7 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 8 * stride0; leadLen -= 8;
                }
                while (leadLen > 4) {
                    v = *(long*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(long*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(long*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(long*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 4 * stride0; leadLen -= 4;
                }
                while (leadLen-- > 0) {
                    v = *(long*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += stride0;
                }
                if (len == 0) {
                    break;
                }
                // reset initial offset in lead dimension after first iteration
                cur[0] = 0;

                // increase higher dims
                int d = 1;
                while (d < ndims) {
                    if (cur[d] < dims[d] - 1) {
                        higdims += strides[d];
                        cur[d]++;
                        break;
                    } else {
                        cur[d] = 0;
                        higdims -= strides[d] * (dims[d] - 1);
                        d++;
                    }
                }
            }
            pOut[0] = min;
            pOut[1] = max;
        }
    }


    internal static class UInt32 {

        internal static int s_threading_overhead_Cont64 = 10;  
        internal static int s_threading_overhead_Strided32 = 10;  
        internal static int s_threading_overhead_Strided64 = 10; 

        internal static unsafe Action<IntPtr> Continous64OOPAction = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = p[2].ToInt64();
            var i = locP[1].ToInt64();
            Continous64OOP((byte*)p[0] + len * (uint)sizeof(uint) * i,
                            (uint*)p[1] + i * 2,
                            (long)len);
            Interlocked.Decrement(ref *(int*)p[3]);
        };
        internal static unsafe Action<IntPtr> Strided32Action = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = (uint)p[3].ToInt32();
            var i = (uint)locP[1].ToInt32();
            Strided32((byte*)p[0], (uint*)p[1] + i * 2, len * i, len, (uint*)p[2]);
            Interlocked.Decrement(ref *(int*)p[4]);
        };
        internal static unsafe Action<IntPtr> Strided64Action = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = (long)p[3].ToInt64();
            var i = (long)locP[1].ToInt64();
            Strided64((byte*)p[0], (uint*)p[1] + i * 2, len * i, len, (long*)p[2]);
            Interlocked.Decrement(ref *(int*)p[4]);
        };
        internal static unsafe void Continous64OOP(byte* pIn, uint* pOut, long len) {

            System.Diagnostics.Debug.Assert(pIn != (byte*)0);
            System.Diagnostics.Debug.Assert(pOut != (uint*)0);

            uint v, min = *(uint*)pIn, max = min;
            while (len >= 8) {
                v = *(uint*)(pIn + 0 * sizeof(uint)); if (v < min) min = v; if (v > max) max = v;
                v = *(uint*)(pIn + 1 * sizeof(uint)); if (v < min) min = v; if (v > max) max = v;
                v = *(uint*)(pIn + 2 * sizeof(uint)); if (v < min) min = v; if (v > max) max = v;
                v = *(uint*)(pIn + 3 * sizeof(uint)); if (v < min) min = v; if (v > max) max = v;
                v = *(uint*)(pIn + 4 * sizeof(uint)); if (v < min) min = v; if (v > max) max = v;
                v = *(uint*)(pIn + 5 * sizeof(uint)); if (v < min) min = v; if (v > max) max = v;
                v = *(uint*)(pIn + 6 * sizeof(uint)); if (v < min) min = v; if (v > max) max = v;
                v = *(uint*)(pIn + 7 * sizeof(uint)); if (v < min) min = v; if (v > max) max = v;
                pIn += 8 * sizeof(uint); len -= 8;
            }
            while (len-- > 0) {
                v = *(uint*)pIn; if (v < min) min = v; if (v > max) max = v;
                pIn += sizeof(uint);
            }
            pOut[0] = min;
            pOut[1] = max;
        }

        /// <summary>
        /// Inner ACCUMALL loop for strided, general (non-huge) array.
        /// </summary>
        /// <param name="pSrc">pointer to first element to consider here.</param>
        /// <param name="pOut">Pointer to single element to store result.</param>
        /// <param name="start">First sequential element.</param>
        /// <param name="len">Number of sequential elements to process.</param>
        /// <param name="ordered_bytestrided_bsd">(Re-)ordered BSD of the source array. Strides indicate <i>byte spacings</i>.</param>
        internal static unsafe void Strided32(byte* pSrc, uint* pOut, uint start, uint len, uint* ordered_bytestrided_bsd) {

            int ndims = (int)ordered_bytestrided_bsd[0];
            uint* dims = ordered_bytestrided_bsd + 3;
            uint* strides = dims + ndims;
            uint stride0 = strides[0];
            uint* cur = stackalloc uint[ndims];

            // figure out the dimension index position for start
            System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

            cur[0] = start % dims[0];
            uint f = start / dims[0];
            uint higdims = 0;
            int i = 1; 
            for (; f > 0 && i < ndims; i++) {
                cur[i] = f % dims[i];
                f /= dims[i];
                higdims += cur[i] * strides[i];
            }
            while (i < ndims) {
                cur[i++] = 0;
            }

            System.Diagnostics.Debug.Assert(f == 0);

            uint v;
            uint min = uint.MaxValue;
            uint max = uint.MinValue;

            while (true) {

                byte* pIn = pSrc + (higdims + cur[0] * stride0);

                // iteration length limited to either the dimension lengths or the end of the requested chunk 
                uint leadLen = Math.Min(len, dims[0] - cur[0]);
                len -= leadLen;

                while (leadLen > 8) {
                    v = *(uint*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(uint*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(uint*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(uint*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(uint*)(pIn + 4 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(uint*)(pIn + 5 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(uint*)(pIn + 6 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(uint*)(pIn + 7 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 8 * stride0; leadLen -= 8;
                }
                while (leadLen > 4) {
                    v = *(uint*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(uint*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(uint*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(uint*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 4 * stride0; leadLen -= 4;
                }
                while (leadLen-- > 0) {
                    v = *(uint*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += stride0;
                }
                if (len == 0) {
                    break;
                }
                // reset initial offset in lead dimension after first iteration
                cur[0] = 0;

                // increase higher dims
                int d = 1;
                while (d < ndims) {
                    if (cur[d] < dims[d] - 1) {
                        higdims += strides[d];
                        cur[d]++;
                        break;
                    } else {
                        cur[d] = 0;
                        higdims -= strides[d] * (dims[d] - 1);
                        d++;
                    }
                }
            }
            pOut[0] = min;
            pOut[1] = max;
        }

        internal static unsafe void Strided64(byte* pSrc, uint* pOut, long start, long len, long* bsd) {

            uint ndims = (uint)bsd[0];
            long* dims = bsd + 3;
            long* strides = dims + ndims;
            long stride0 = strides[0];
            long* cur = stackalloc long[(int)ndims];

            // figure out the dimension index position for start
            System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

            cur[0] = start % dims[0];
            long f = start / dims[0];
            long higdims = 0;
            int i = 1;
            for (; f > 0 && i < ndims; i++) {
                cur[i] = f % dims[i];
                f /= dims[i];
                higdims += cur[i] * strides[i];
            }
            while (i < ndims) {
                cur[i++] = 0;
            }

            System.Diagnostics.Debug.Assert(f == 0);

            uint v;
            uint min = uint.MaxValue;
            uint max = uint.MinValue;

            while (true) {

                byte* pIn = pSrc + higdims + cur[0] * stride0;

                // iteration length limited to either the dimension lengths or the end of the requested chunk 
                long leadLen = Math.Min(len, dims[0] - cur[0]);
                len -= leadLen;

                while (leadLen > 8) {
                    v = *(uint*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(uint*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(uint*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(uint*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(uint*)(pIn + 4 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(uint*)(pIn + 5 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(uint*)(pIn + 6 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(uint*)(pIn + 7 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 8 * stride0; leadLen -= 8;
                }
                while (leadLen > 4) {
                    v = *(uint*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(uint*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(uint*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(uint*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 4 * stride0; leadLen -= 4;
                }
                while (leadLen-- > 0) {
                    v = *(uint*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += stride0;
                }
                if (len == 0) {
                    break;
                }
                // reset initial offset in lead dimension after first iteration
                cur[0] = 0;

                // increase higher dims
                int d = 1;
                while (d < ndims) {
                    if (cur[d] < dims[d] - 1) {
                        higdims += strides[d];
                        cur[d]++;
                        break;
                    } else {
                        cur[d] = 0;
                        higdims -= strides[d] * (dims[d] - 1);
                        d++;
                    }
                }
            }
            pOut[0] = min;
            pOut[1] = max;
        }
    }


    internal static class UInt16 {

        internal static int s_threading_overhead_Cont64 = 10;  
        internal static int s_threading_overhead_Strided32 = 10;  
        internal static int s_threading_overhead_Strided64 = 10; 

        internal static unsafe Action<IntPtr> Continous64OOPAction = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = p[2].ToInt64();
            var i = locP[1].ToInt64();
            Continous64OOP((byte*)p[0] + len * (uint)sizeof(ushort) * i,
                            (ushort*)p[1] + i * 2,
                            (long)len);
            Interlocked.Decrement(ref *(int*)p[3]);
        };
        internal static unsafe Action<IntPtr> Strided32Action = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = (uint)p[3].ToInt32();
            var i = (uint)locP[1].ToInt32();
            Strided32((byte*)p[0], (ushort*)p[1] + i * 2, len * i, len, (uint*)p[2]);
            Interlocked.Decrement(ref *(int*)p[4]);
        };
        internal static unsafe Action<IntPtr> Strided64Action = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = (long)p[3].ToInt64();
            var i = (long)locP[1].ToInt64();
            Strided64((byte*)p[0], (ushort*)p[1] + i * 2, len * i, len, (long*)p[2]);
            Interlocked.Decrement(ref *(int*)p[4]);
        };
        internal static unsafe void Continous64OOP(byte* pIn, ushort* pOut, long len) {

            System.Diagnostics.Debug.Assert(pIn != (byte*)0);
            System.Diagnostics.Debug.Assert(pOut != (ushort*)0);

            ushort v, min = *(ushort*)pIn, max = min;
            while (len >= 8) {
                v = *(ushort*)(pIn + 0 * sizeof(ushort)); if (v < min) min = v; if (v > max) max = v;
                v = *(ushort*)(pIn + 1 * sizeof(ushort)); if (v < min) min = v; if (v > max) max = v;
                v = *(ushort*)(pIn + 2 * sizeof(ushort)); if (v < min) min = v; if (v > max) max = v;
                v = *(ushort*)(pIn + 3 * sizeof(ushort)); if (v < min) min = v; if (v > max) max = v;
                v = *(ushort*)(pIn + 4 * sizeof(ushort)); if (v < min) min = v; if (v > max) max = v;
                v = *(ushort*)(pIn + 5 * sizeof(ushort)); if (v < min) min = v; if (v > max) max = v;
                v = *(ushort*)(pIn + 6 * sizeof(ushort)); if (v < min) min = v; if (v > max) max = v;
                v = *(ushort*)(pIn + 7 * sizeof(ushort)); if (v < min) min = v; if (v > max) max = v;
                pIn += 8 * sizeof(ushort); len -= 8;
            }
            while (len-- > 0) {
                v = *(ushort*)pIn; if (v < min) min = v; if (v > max) max = v;
                pIn += sizeof(ushort);
            }
            pOut[0] = min;
            pOut[1] = max;
        }

        /// <summary>
        /// Inner ACCUMALL loop for strided, general (non-huge) array.
        /// </summary>
        /// <param name="pSrc">pointer to first element to consider here.</param>
        /// <param name="pOut">Pointer to single element to store result.</param>
        /// <param name="start">First sequential element.</param>
        /// <param name="len">Number of sequential elements to process.</param>
        /// <param name="ordered_bytestrided_bsd">(Re-)ordered BSD of the source array. Strides indicate <i>byte spacings</i>.</param>
        internal static unsafe void Strided32(byte* pSrc, ushort* pOut, uint start, uint len, uint* ordered_bytestrided_bsd) {

            int ndims = (int)ordered_bytestrided_bsd[0];
            uint* dims = ordered_bytestrided_bsd + 3;
            uint* strides = dims + ndims;
            uint stride0 = strides[0];
            uint* cur = stackalloc uint[ndims];

            // figure out the dimension index position for start
            System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

            cur[0] = start % dims[0];
            uint f = start / dims[0];
            uint higdims = 0;
            int i = 1; 
            for (; f > 0 && i < ndims; i++) {
                cur[i] = f % dims[i];
                f /= dims[i];
                higdims += cur[i] * strides[i];
            }
            while (i < ndims) {
                cur[i++] = 0;
            }

            System.Diagnostics.Debug.Assert(f == 0);

            ushort v;
            ushort min = ushort.MaxValue;
            ushort max = ushort.MinValue;

            while (true) {

                byte* pIn = pSrc + (higdims + cur[0] * stride0);

                // iteration length limited to either the dimension lengths or the end of the requested chunk 
                uint leadLen = Math.Min(len, dims[0] - cur[0]);
                len -= leadLen;

                while (leadLen > 8) {
                    v = *(ushort*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ushort*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ushort*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ushort*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ushort*)(pIn + 4 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ushort*)(pIn + 5 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ushort*)(pIn + 6 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ushort*)(pIn + 7 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 8 * stride0; leadLen -= 8;
                }
                while (leadLen > 4) {
                    v = *(ushort*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ushort*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ushort*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ushort*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 4 * stride0; leadLen -= 4;
                }
                while (leadLen-- > 0) {
                    v = *(ushort*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += stride0;
                }
                if (len == 0) {
                    break;
                }
                // reset initial offset in lead dimension after first iteration
                cur[0] = 0;

                // increase higher dims
                int d = 1;
                while (d < ndims) {
                    if (cur[d] < dims[d] - 1) {
                        higdims += strides[d];
                        cur[d]++;
                        break;
                    } else {
                        cur[d] = 0;
                        higdims -= strides[d] * (dims[d] - 1);
                        d++;
                    }
                }
            }
            pOut[0] = min;
            pOut[1] = max;
        }

        internal static unsafe void Strided64(byte* pSrc, ushort* pOut, long start, long len, long* bsd) {

            uint ndims = (uint)bsd[0];
            long* dims = bsd + 3;
            long* strides = dims + ndims;
            long stride0 = strides[0];
            long* cur = stackalloc long[(int)ndims];

            // figure out the dimension index position for start
            System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

            cur[0] = start % dims[0];
            long f = start / dims[0];
            long higdims = 0;
            int i = 1;
            for (; f > 0 && i < ndims; i++) {
                cur[i] = f % dims[i];
                f /= dims[i];
                higdims += cur[i] * strides[i];
            }
            while (i < ndims) {
                cur[i++] = 0;
            }

            System.Diagnostics.Debug.Assert(f == 0);

            ushort v;
            ushort min = ushort.MaxValue;
            ushort max = ushort.MinValue;

            while (true) {

                byte* pIn = pSrc + higdims + cur[0] * stride0;

                // iteration length limited to either the dimension lengths or the end of the requested chunk 
                long leadLen = Math.Min(len, dims[0] - cur[0]);
                len -= leadLen;

                while (leadLen > 8) {
                    v = *(ushort*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ushort*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ushort*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ushort*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ushort*)(pIn + 4 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ushort*)(pIn + 5 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ushort*)(pIn + 6 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ushort*)(pIn + 7 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 8 * stride0; leadLen -= 8;
                }
                while (leadLen > 4) {
                    v = *(ushort*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ushort*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ushort*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(ushort*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 4 * stride0; leadLen -= 4;
                }
                while (leadLen-- > 0) {
                    v = *(ushort*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += stride0;
                }
                if (len == 0) {
                    break;
                }
                // reset initial offset in lead dimension after first iteration
                cur[0] = 0;

                // increase higher dims
                int d = 1;
                while (d < ndims) {
                    if (cur[d] < dims[d] - 1) {
                        higdims += strides[d];
                        cur[d]++;
                        break;
                    } else {
                        cur[d] = 0;
                        higdims -= strides[d] * (dims[d] - 1);
                        d++;
                    }
                }
            }
            pOut[0] = min;
            pOut[1] = max;
        }
    }


    internal static class Int16 {

        internal static int s_threading_overhead_Cont64 = 10;  
        internal static int s_threading_overhead_Strided32 = 10;  
        internal static int s_threading_overhead_Strided64 = 10; 

        internal static unsafe Action<IntPtr> Continous64OOPAction = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = p[2].ToInt64();
            var i = locP[1].ToInt64();
            Continous64OOP((byte*)p[0] + len * (uint)sizeof(short) * i,
                            (short*)p[1] + i * 2,
                            (long)len);
            Interlocked.Decrement(ref *(int*)p[3]);
        };
        internal static unsafe Action<IntPtr> Strided32Action = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = (uint)p[3].ToInt32();
            var i = (uint)locP[1].ToInt32();
            Strided32((byte*)p[0], (short*)p[1] + i * 2, len * i, len, (uint*)p[2]);
            Interlocked.Decrement(ref *(int*)p[4]);
        };
        internal static unsafe Action<IntPtr> Strided64Action = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = (long)p[3].ToInt64();
            var i = (long)locP[1].ToInt64();
            Strided64((byte*)p[0], (short*)p[1] + i * 2, len * i, len, (long*)p[2]);
            Interlocked.Decrement(ref *(int*)p[4]);
        };
        internal static unsafe void Continous64OOP(byte* pIn, short* pOut, long len) {

            System.Diagnostics.Debug.Assert(pIn != (byte*)0);
            System.Diagnostics.Debug.Assert(pOut != (short*)0);

            short v, min = *(short*)pIn, max = min;
            while (len >= 8) {
                v = *(short*)(pIn + 0 * sizeof(short)); if (v < min) min = v; if (v > max) max = v;
                v = *(short*)(pIn + 1 * sizeof(short)); if (v < min) min = v; if (v > max) max = v;
                v = *(short*)(pIn + 2 * sizeof(short)); if (v < min) min = v; if (v > max) max = v;
                v = *(short*)(pIn + 3 * sizeof(short)); if (v < min) min = v; if (v > max) max = v;
                v = *(short*)(pIn + 4 * sizeof(short)); if (v < min) min = v; if (v > max) max = v;
                v = *(short*)(pIn + 5 * sizeof(short)); if (v < min) min = v; if (v > max) max = v;
                v = *(short*)(pIn + 6 * sizeof(short)); if (v < min) min = v; if (v > max) max = v;
                v = *(short*)(pIn + 7 * sizeof(short)); if (v < min) min = v; if (v > max) max = v;
                pIn += 8 * sizeof(short); len -= 8;
            }
            while (len-- > 0) {
                v = *(short*)pIn; if (v < min) min = v; if (v > max) max = v;
                pIn += sizeof(short);
            }
            pOut[0] = min;
            pOut[1] = max;
        }

        /// <summary>
        /// Inner ACCUMALL loop for strided, general (non-huge) array.
        /// </summary>
        /// <param name="pSrc">pointer to first element to consider here.</param>
        /// <param name="pOut">Pointer to single element to store result.</param>
        /// <param name="start">First sequential element.</param>
        /// <param name="len">Number of sequential elements to process.</param>
        /// <param name="ordered_bytestrided_bsd">(Re-)ordered BSD of the source array. Strides indicate <i>byte spacings</i>.</param>
        internal static unsafe void Strided32(byte* pSrc, short* pOut, uint start, uint len, uint* ordered_bytestrided_bsd) {

            int ndims = (int)ordered_bytestrided_bsd[0];
            uint* dims = ordered_bytestrided_bsd + 3;
            uint* strides = dims + ndims;
            uint stride0 = strides[0];
            uint* cur = stackalloc uint[ndims];

            // figure out the dimension index position for start
            System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

            cur[0] = start % dims[0];
            uint f = start / dims[0];
            uint higdims = 0;
            int i = 1; 
            for (; f > 0 && i < ndims; i++) {
                cur[i] = f % dims[i];
                f /= dims[i];
                higdims += cur[i] * strides[i];
            }
            while (i < ndims) {
                cur[i++] = 0;
            }

            System.Diagnostics.Debug.Assert(f == 0);

            short v;
            short min = short.MaxValue;
            short max = short.MinValue;

            while (true) {

                byte* pIn = pSrc + (higdims + cur[0] * stride0);

                // iteration length limited to either the dimension lengths or the end of the requested chunk 
                uint leadLen = Math.Min(len, dims[0] - cur[0]);
                len -= leadLen;

                while (leadLen > 8) {
                    v = *(short*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(short*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(short*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(short*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(short*)(pIn + 4 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(short*)(pIn + 5 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(short*)(pIn + 6 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(short*)(pIn + 7 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 8 * stride0; leadLen -= 8;
                }
                while (leadLen > 4) {
                    v = *(short*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(short*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(short*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(short*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 4 * stride0; leadLen -= 4;
                }
                while (leadLen-- > 0) {
                    v = *(short*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += stride0;
                }
                if (len == 0) {
                    break;
                }
                // reset initial offset in lead dimension after first iteration
                cur[0] = 0;

                // increase higher dims
                int d = 1;
                while (d < ndims) {
                    if (cur[d] < dims[d] - 1) {
                        higdims += strides[d];
                        cur[d]++;
                        break;
                    } else {
                        cur[d] = 0;
                        higdims -= strides[d] * (dims[d] - 1);
                        d++;
                    }
                }
            }
            pOut[0] = min;
            pOut[1] = max;
        }

        internal static unsafe void Strided64(byte* pSrc, short* pOut, long start, long len, long* bsd) {

            uint ndims = (uint)bsd[0];
            long* dims = bsd + 3;
            long* strides = dims + ndims;
            long stride0 = strides[0];
            long* cur = stackalloc long[(int)ndims];

            // figure out the dimension index position for start
            System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

            cur[0] = start % dims[0];
            long f = start / dims[0];
            long higdims = 0;
            int i = 1;
            for (; f > 0 && i < ndims; i++) {
                cur[i] = f % dims[i];
                f /= dims[i];
                higdims += cur[i] * strides[i];
            }
            while (i < ndims) {
                cur[i++] = 0;
            }

            System.Diagnostics.Debug.Assert(f == 0);

            short v;
            short min = short.MaxValue;
            short max = short.MinValue;

            while (true) {

                byte* pIn = pSrc + higdims + cur[0] * stride0;

                // iteration length limited to either the dimension lengths or the end of the requested chunk 
                long leadLen = Math.Min(len, dims[0] - cur[0]);
                len -= leadLen;

                while (leadLen > 8) {
                    v = *(short*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(short*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(short*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(short*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(short*)(pIn + 4 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(short*)(pIn + 5 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(short*)(pIn + 6 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(short*)(pIn + 7 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 8 * stride0; leadLen -= 8;
                }
                while (leadLen > 4) {
                    v = *(short*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(short*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(short*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(short*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 4 * stride0; leadLen -= 4;
                }
                while (leadLen-- > 0) {
                    v = *(short*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += stride0;
                }
                if (len == 0) {
                    break;
                }
                // reset initial offset in lead dimension after first iteration
                cur[0] = 0;

                // increase higher dims
                int d = 1;
                while (d < ndims) {
                    if (cur[d] < dims[d] - 1) {
                        higdims += strides[d];
                        cur[d]++;
                        break;
                    } else {
                        cur[d] = 0;
                        higdims -= strides[d] * (dims[d] - 1);
                        d++;
                    }
                }
            }
            pOut[0] = min;
            pOut[1] = max;
        }
    }


    internal static class Byte {

        internal static int s_threading_overhead_Cont64 = 10;  
        internal static int s_threading_overhead_Strided32 = 10;  
        internal static int s_threading_overhead_Strided64 = 10; 

        internal static unsafe Action<IntPtr> Continous64OOPAction = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = p[2].ToInt64();
            var i = locP[1].ToInt64();
            Continous64OOP((byte*)p[0] + len * (uint)sizeof(byte) * i,
                            (byte*)p[1] + i * 2,
                            (long)len);
            Interlocked.Decrement(ref *(int*)p[3]);
        };
        internal static unsafe Action<IntPtr> Strided32Action = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = (uint)p[3].ToInt32();
            var i = (uint)locP[1].ToInt32();
            Strided32((byte*)p[0], (byte*)p[1] + i * 2, len * i, len, (uint*)p[2]);
            Interlocked.Decrement(ref *(int*)p[4]);
        };
        internal static unsafe Action<IntPtr> Strided64Action = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = (long)p[3].ToInt64();
            var i = (long)locP[1].ToInt64();
            Strided64((byte*)p[0], (byte*)p[1] + i * 2, len * i, len, (long*)p[2]);
            Interlocked.Decrement(ref *(int*)p[4]);
        };
        internal static unsafe void Continous64OOP(byte* pIn, byte* pOut, long len) {

            System.Diagnostics.Debug.Assert(pIn != (byte*)0);
            System.Diagnostics.Debug.Assert(pOut != (byte*)0);

            byte v, min = *(byte*)pIn, max = min;
            while (len >= 8) {
                v = *(byte*)(pIn + 0 * sizeof(byte)); if (v < min) min = v; if (v > max) max = v;
                v = *(byte*)(pIn + 1 * sizeof(byte)); if (v < min) min = v; if (v > max) max = v;
                v = *(byte*)(pIn + 2 * sizeof(byte)); if (v < min) min = v; if (v > max) max = v;
                v = *(byte*)(pIn + 3 * sizeof(byte)); if (v < min) min = v; if (v > max) max = v;
                v = *(byte*)(pIn + 4 * sizeof(byte)); if (v < min) min = v; if (v > max) max = v;
                v = *(byte*)(pIn + 5 * sizeof(byte)); if (v < min) min = v; if (v > max) max = v;
                v = *(byte*)(pIn + 6 * sizeof(byte)); if (v < min) min = v; if (v > max) max = v;
                v = *(byte*)(pIn + 7 * sizeof(byte)); if (v < min) min = v; if (v > max) max = v;
                pIn += 8 * sizeof(byte); len -= 8;
            }
            while (len-- > 0) {
                v = *(byte*)pIn; if (v < min) min = v; if (v > max) max = v;
                pIn += sizeof(byte);
            }
            pOut[0] = min;
            pOut[1] = max;
        }

        /// <summary>
        /// Inner ACCUMALL loop for strided, general (non-huge) array.
        /// </summary>
        /// <param name="pSrc">pointer to first element to consider here.</param>
        /// <param name="pOut">Pointer to single element to store result.</param>
        /// <param name="start">First sequential element.</param>
        /// <param name="len">Number of sequential elements to process.</param>
        /// <param name="ordered_bytestrided_bsd">(Re-)ordered BSD of the source array. Strides indicate <i>byte spacings</i>.</param>
        internal static unsafe void Strided32(byte* pSrc, byte* pOut, uint start, uint len, uint* ordered_bytestrided_bsd) {

            int ndims = (int)ordered_bytestrided_bsd[0];
            uint* dims = ordered_bytestrided_bsd + 3;
            uint* strides = dims + ndims;
            uint stride0 = strides[0];
            uint* cur = stackalloc uint[ndims];

            // figure out the dimension index position for start
            System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

            cur[0] = start % dims[0];
            uint f = start / dims[0];
            uint higdims = 0;
            int i = 1; 
            for (; f > 0 && i < ndims; i++) {
                cur[i] = f % dims[i];
                f /= dims[i];
                higdims += cur[i] * strides[i];
            }
            while (i < ndims) {
                cur[i++] = 0;
            }

            System.Diagnostics.Debug.Assert(f == 0);

            byte v;
            byte min = byte.MaxValue;
            byte max = byte.MinValue;

            while (true) {

                byte* pIn = pSrc + (higdims + cur[0] * stride0);

                // iteration length limited to either the dimension lengths or the end of the requested chunk 
                uint leadLen = Math.Min(len, dims[0] - cur[0]);
                len -= leadLen;

                while (leadLen > 8) {
                    v = *(byte*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(byte*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(byte*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(byte*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(byte*)(pIn + 4 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(byte*)(pIn + 5 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(byte*)(pIn + 6 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(byte*)(pIn + 7 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 8 * stride0; leadLen -= 8;
                }
                while (leadLen > 4) {
                    v = *(byte*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(byte*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(byte*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(byte*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 4 * stride0; leadLen -= 4;
                }
                while (leadLen-- > 0) {
                    v = *(byte*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += stride0;
                }
                if (len == 0) {
                    break;
                }
                // reset initial offset in lead dimension after first iteration
                cur[0] = 0;

                // increase higher dims
                int d = 1;
                while (d < ndims) {
                    if (cur[d] < dims[d] - 1) {
                        higdims += strides[d];
                        cur[d]++;
                        break;
                    } else {
                        cur[d] = 0;
                        higdims -= strides[d] * (dims[d] - 1);
                        d++;
                    }
                }
            }
            pOut[0] = min;
            pOut[1] = max;
        }

        internal static unsafe void Strided64(byte* pSrc, byte* pOut, long start, long len, long* bsd) {

            uint ndims = (uint)bsd[0];
            long* dims = bsd + 3;
            long* strides = dims + ndims;
            long stride0 = strides[0];
            long* cur = stackalloc long[(int)ndims];

            // figure out the dimension index position for start
            System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

            cur[0] = start % dims[0];
            long f = start / dims[0];
            long higdims = 0;
            int i = 1;
            for (; f > 0 && i < ndims; i++) {
                cur[i] = f % dims[i];
                f /= dims[i];
                higdims += cur[i] * strides[i];
            }
            while (i < ndims) {
                cur[i++] = 0;
            }

            System.Diagnostics.Debug.Assert(f == 0);

            byte v;
            byte min = byte.MaxValue;
            byte max = byte.MinValue;

            while (true) {

                byte* pIn = pSrc + higdims + cur[0] * stride0;

                // iteration length limited to either the dimension lengths or the end of the requested chunk 
                long leadLen = Math.Min(len, dims[0] - cur[0]);
                len -= leadLen;

                while (leadLen > 8) {
                    v = *(byte*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(byte*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(byte*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(byte*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(byte*)(pIn + 4 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(byte*)(pIn + 5 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(byte*)(pIn + 6 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(byte*)(pIn + 7 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 8 * stride0; leadLen -= 8;
                }
                while (leadLen > 4) {
                    v = *(byte*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(byte*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(byte*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(byte*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 4 * stride0; leadLen -= 4;
                }
                while (leadLen-- > 0) {
                    v = *(byte*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += stride0;
                }
                if (len == 0) {
                    break;
                }
                // reset initial offset in lead dimension after first iteration
                cur[0] = 0;

                // increase higher dims
                int d = 1;
                while (d < ndims) {
                    if (cur[d] < dims[d] - 1) {
                        higdims += strides[d];
                        cur[d]++;
                        break;
                    } else {
                        cur[d] = 0;
                        higdims -= strides[d] * (dims[d] - 1);
                        d++;
                    }
                }
            }
            pOut[0] = min;
            pOut[1] = max;
        }
    }


    internal static class Int32 {

        internal static int s_threading_overhead_Cont64 = 10;  
        internal static int s_threading_overhead_Strided32 = 10;  
        internal static int s_threading_overhead_Strided64 = 10; 

        internal static unsafe Action<IntPtr> Continous64OOPAction = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = p[2].ToInt64();
            var i = locP[1].ToInt64();
            Continous64OOP((byte*)p[0] + len * (uint)sizeof(int) * i,
                            (int*)p[1] + i * 2,
                            (long)len);
            Interlocked.Decrement(ref *(int*)p[3]);
        };
        internal static unsafe Action<IntPtr> Strided32Action = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = (uint)p[3].ToInt32();
            var i = (uint)locP[1].ToInt32();
            Strided32((byte*)p[0], (int*)p[1] + i * 2, len * i, len, (uint*)p[2]);
            Interlocked.Decrement(ref *(int*)p[4]);
        };
        internal static unsafe Action<IntPtr> Strided64Action = data => {
            IntPtr* locP = (IntPtr*)(IntPtr)data;
            IntPtr* p = (IntPtr*)locP[0];
            var len = (long)p[3].ToInt64();
            var i = (long)locP[1].ToInt64();
            Strided64((byte*)p[0], (int*)p[1] + i * 2, len * i, len, (long*)p[2]);
            Interlocked.Decrement(ref *(int*)p[4]);
        };
        internal static unsafe void Continous64OOP(byte* pIn, int* pOut, long len) {

            System.Diagnostics.Debug.Assert(pIn != (byte*)0);
            System.Diagnostics.Debug.Assert(pOut != (int*)0);

            int v, min = *(int*)pIn, max = min;
            while (len >= 8) {
                v = *(int*)(pIn + 0 * sizeof(int)); if (v < min) min = v; if (v > max) max = v;
                v = *(int*)(pIn + 1 * sizeof(int)); if (v < min) min = v; if (v > max) max = v;
                v = *(int*)(pIn + 2 * sizeof(int)); if (v < min) min = v; if (v > max) max = v;
                v = *(int*)(pIn + 3 * sizeof(int)); if (v < min) min = v; if (v > max) max = v;
                v = *(int*)(pIn + 4 * sizeof(int)); if (v < min) min = v; if (v > max) max = v;
                v = *(int*)(pIn + 5 * sizeof(int)); if (v < min) min = v; if (v > max) max = v;
                v = *(int*)(pIn + 6 * sizeof(int)); if (v < min) min = v; if (v > max) max = v;
                v = *(int*)(pIn + 7 * sizeof(int)); if (v < min) min = v; if (v > max) max = v;
                pIn += 8 * sizeof(int); len -= 8;
            }
            while (len-- > 0) {
                v = *(int*)pIn; if (v < min) min = v; if (v > max) max = v;
                pIn += sizeof(int);
            }
            pOut[0] = min;
            pOut[1] = max;
        }

        /// <summary>
        /// Inner ACCUMALL loop for strided, general (non-huge) array.
        /// </summary>
        /// <param name="pSrc">pointer to first element to consider here.</param>
        /// <param name="pOut">Pointer to single element to store result.</param>
        /// <param name="start">First sequential element.</param>
        /// <param name="len">Number of sequential elements to process.</param>
        /// <param name="ordered_bytestrided_bsd">(Re-)ordered BSD of the source array. Strides indicate <i>byte spacings</i>.</param>
        internal static unsafe void Strided32(byte* pSrc, int* pOut, uint start, uint len, uint* ordered_bytestrided_bsd) {

            int ndims = (int)ordered_bytestrided_bsd[0];
            uint* dims = ordered_bytestrided_bsd + 3;
            uint* strides = dims + ndims;
            uint stride0 = strides[0];
            uint* cur = stackalloc uint[ndims];

            // figure out the dimension index position for start
            System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

            cur[0] = start % dims[0];
            uint f = start / dims[0];
            uint higdims = 0;
            int i = 1; 
            for (; f > 0 && i < ndims; i++) {
                cur[i] = f % dims[i];
                f /= dims[i];
                higdims += cur[i] * strides[i];
            }
            while (i < ndims) {
                cur[i++] = 0;
            }

            System.Diagnostics.Debug.Assert(f == 0);

            int v;
            int min = int.MaxValue;
            int max = int.MinValue;

            while (true) {

                byte* pIn = pSrc + (higdims + cur[0] * stride0);

                // iteration length limited to either the dimension lengths or the end of the requested chunk 
                uint leadLen = Math.Min(len, dims[0] - cur[0]);
                len -= leadLen;

                while (leadLen > 8) {
                    v = *(int*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(int*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(int*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(int*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(int*)(pIn + 4 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(int*)(pIn + 5 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(int*)(pIn + 6 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(int*)(pIn + 7 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 8 * stride0; leadLen -= 8;
                }
                while (leadLen > 4) {
                    v = *(int*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(int*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(int*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(int*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 4 * stride0; leadLen -= 4;
                }
                while (leadLen-- > 0) {
                    v = *(int*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += stride0;
                }
                if (len == 0) {
                    break;
                }
                // reset initial offset in lead dimension after first iteration
                cur[0] = 0;

                // increase higher dims
                int d = 1;
                while (d < ndims) {
                    if (cur[d] < dims[d] - 1) {
                        higdims += strides[d];
                        cur[d]++;
                        break;
                    } else {
                        cur[d] = 0;
                        higdims -= strides[d] * (dims[d] - 1);
                        d++;
                    }
                }
            }
            pOut[0] = min;
            pOut[1] = max;
        }

        internal static unsafe void Strided64(byte* pSrc, int* pOut, long start, long len, long* bsd) {

            uint ndims = (uint)bsd[0];
            long* dims = bsd + 3;
            long* strides = dims + ndims;
            long stride0 = strides[0];
            long* cur = stackalloc long[(int)ndims];

            // figure out the dimension index position for start
            System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

            cur[0] = start % dims[0];
            long f = start / dims[0];
            long higdims = 0;
            int i = 1;
            for (; f > 0 && i < ndims; i++) {
                cur[i] = f % dims[i];
                f /= dims[i];
                higdims += cur[i] * strides[i];
            }
            while (i < ndims) {
                cur[i++] = 0;
            }

            System.Diagnostics.Debug.Assert(f == 0);

            int v;
            int min = int.MaxValue;
            int max = int.MinValue;

            while (true) {

                byte* pIn = pSrc + higdims + cur[0] * stride0;

                // iteration length limited to either the dimension lengths or the end of the requested chunk 
                long leadLen = Math.Min(len, dims[0] - cur[0]);
                len -= leadLen;

                while (leadLen > 8) {
                    v = *(int*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(int*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(int*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(int*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(int*)(pIn + 4 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(int*)(pIn + 5 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(int*)(pIn + 6 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(int*)(pIn + 7 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 8 * stride0; leadLen -= 8;
                }
                while (leadLen > 4) {
                    v = *(int*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(int*)(pIn + 1 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(int*)(pIn + 2 * stride0); if (v < min) min = v; if (v > max) max = v;
                    v = *(int*)(pIn + 3 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += 4 * stride0; leadLen -= 4;
                }
                while (leadLen-- > 0) {
                    v = *(int*)(pIn + 0 * stride0); if (v < min) min = v; if (v > max) max = v;
                    pIn += stride0;
                }
                if (len == 0) {
                    break;
                }
                // reset initial offset in lead dimension after first iteration
                cur[0] = 0;

                // increase higher dims
                int d = 1;
                while (d < ndims) {
                    if (cur[d] < dims[d] - 1) {
                        higdims += strides[d];
                        cur[d]++;
                        break;
                    } else {
                        cur[d] = 0;
                        higdims -= strides[d] * (dims[d] - 1);
                        d++;
                    }
                }
            }
            pOut[0] = min;
            pOut[1] = max;
        }
    }

#endregion HYCALPER AUTO GENERATED CODE

}
