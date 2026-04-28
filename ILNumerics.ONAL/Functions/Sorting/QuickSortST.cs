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
using System;
using System.Collections.Generic;
using System.Text;
using ILNumerics; 
using ILNumerics.Core.Misc;
using ILNumerics.Core.StorageLayer;

/*!HC:TYPELIST:
<hycalper>
<type>
<source locate="after">
inArr1
</source>
<destination>sbyte</destination>
<destination>byte</destination>
<destination>char</destination>
<destination>complex</destination>
<destination>fcomplex</destination>
<destination>float</destination>
<destination>Int16</destination>
<destination>Int32</destination>
<destination>Int64</destination>
<destination>UInt16</destination>
<destination>UInt32</destination>
<destination>UInt64</destination>
</type>
<type>
<source locate="after">
inArr2
</source>
<destination>long</destination>
<destination>long</destination>
<destination>long</destination>
<destination>long</destination>
<destination>long</destination>
<destination>long</destination>
<destination>long</destination>
<destination>long</destination>
<destination>long</destination>
<destination>long</destination>
<destination>long</destination>
<destination>long</destination>
</type>
<type>
<source locate="nextline">
checkAscNAN
</source>
<destination></destination>
<destination></destination>
<destination></destination>
<destination><![CDATA[if (checkNaNAsc(lo, ref hi, inc, vec)) return;]]></destination>
<destination><![CDATA[if (checkNaNAsc(lo, ref hi, inc, vec)) return;]]></destination>
<destination><![CDATA[if (checkNaNAsc(lo, ref hi, inc, vec)) return;]]></destination>
<destination></destination>
<destination></destination>
<destination></destination>
<destination></destination>
<destination></destination>
<destination></destination>
</type>
<type>
<source locate="nextline">
checkDescNAN
</source>
<destination></destination>
<destination></destination>
<destination></destination>
<destination><![CDATA[if (checkNaNDesc(lo, ref hi, inc, vec)) return;]]></destination>
<destination><![CDATA[if (checkNaNDesc(lo, ref hi, inc, vec)) return;]]></destination>
<destination><![CDATA[if (checkNaNDesc(lo, ref hi, inc, vec)) return;]]></destination>
<destination></destination>
<destination></destination>
<destination></destination>
<destination></destination>
<destination></destination>
<destination></destination>
</type>
<type>
<source locate="nextline">
checkIdxAscNAN 
</source>
<destination></destination>
<destination></destination>
<destination></destination>
<destination><![CDATA[if (checkNaNIDXAsc(lo, ref hi, inc, vecIdx, vec)) return;]]></destination>
<destination><![CDATA[if (checkNaNIDXAsc(lo, ref hi, inc, vecIdx, vec)) return;]]></destination>
<destination><![CDATA[if (checkNaNIDXAsc(lo, ref hi, inc, vecIdx, vec)) return;]]></destination>
<destination></destination>
<destination></destination>
<destination></destination>
<destination></destination>
<destination></destination>
<destination></destination>
</type>
<type>
<source locate="nextline">
checkIdxDescNAN 
</source>
<destination></destination>
<destination></destination>
<destination></destination>
<destination><![CDATA[if (checkNaNIDXDesc(lo, ref hi, inc, vecIdx, vec)) return;]]></destination>
<destination><![CDATA[if (checkNaNIDXDesc(lo, ref hi, inc, vecIdx, vec)) return;]]></destination>
<destination><![CDATA[if (checkNaNIDXDesc(lo, ref hi, inc, vecIdx, vec)) return;]]></destination>
<destination></destination>
<destination></destination>
<destination></destination>
<destination></destination>
<destination></destination>
<destination></destination>
</type>
</hycalper>
*/

namespace ILNumerics.Core.Misc {

    /// <summary>
    /// the class provides a number of one dimensional quicksort implementations for several datatypes/ properties
    /// </summary>
    public static partial class QuickSort {

        #region HYCALPER LOOPSTART single threaded quick sorts

        /// <summary>
        /// One dimensional quick sort, inline, ascending, arbitrary element spacing
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public static unsafe void QuickSortAscST(/*!HC:inArr1*/ double* vec, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0; 

            /*!HC:inArr1*/
            double a1, a2, a3;
            /*!HC:inArr1*/
            double temp, pivotItem;
            /*!HC:checkAscNAN*/
            if (checkNaNAsc(lo, ref hi, inc, vec)) return;

            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp <= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                        }
                        *(vec + i + inc) = pivotItem;
                    }
                    #endregion

                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {

                    // do the quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 > a2) {
                        if (a1 > a3) {
                            if (a3 > a2) {
                                *(vec + hi) = a1;
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                            } else {
                                *(vec + hi) = a1;
                                *(vec + lo) = a3;
                                *(vec + lo + inc) = a2;
                            }
                        } else {
                            *(vec + lo) = a2;
                            *(vec + lo + inc) = a1;
                        }
                    } else {
                        if (a2 > a3) {
                            *(vec + hi) = a2;
                            if (a3 > a1) {
                                *(vec + lo + inc) = a3;
                            } else {
                                *(vec + lo + inc) = a1;
                                *(vec + lo) = a3;
                            }
                        } else {
                            *(vec + lo + inc) = a2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) < pivotItem);
                        do j -= inc; while (*(vec + j) > pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vec,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, descending, arbitrary element spacing.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortDescST(/*!HC:inArr1*/ double* vec, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

            /*!HC:inArr1*/
            double a1, a2, a3;
            /*!HC:inArr1*/
            double temp, pivotItem;
            /*!HC:checkDescNAN*/
            if (checkNaNDesc(lo, ref hi, inc, vec)) return;

            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp >= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                        }
                        *(vec + i + inc) = pivotItem;
                    }
                    #endregion
                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 < a2) {
                        if (a2 < a3) {
                            *(vec + hi) = a1;
                            *(vec + lo) = a3;
                            *(vec + lo + inc) = a2;
                        } else {
                            if (a1 < a3) {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vec + hi) = a1;
                            } else {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a1;
                                //*(vec + hi) = a3; 
                            }
                        }
                    } else {
                        if (a2 < a3) {
                            if (a1 < a3) {
                                *(vec + lo) = a3;
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a1;
                            } else {
                                //*(vec + lo) = a1; 
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a3;
                            }
                        } else {
                            //*(vec + lo) = a1; 
                            //*(vec + hi) = a3; 
                            *(vec + lo + inc) = a2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) > pivotItem);
                        do j -= inc; while (*(vec + j) < pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4D(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, ascending, arbitrary element spacing, indices aware.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="vecIdx">indices vector, content will in-place be sorted along with <paramref name="vec"/></param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MminimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortAscIDXST(/*!HC:inArr1*/ double* vec, /*!HC:inArr2*/ long* vecIdx, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

            /*!HC:inArr1*/
            double a1, a2, a3;
            /*!HC:inArr2*/
            long ai1, ai2, ai3;
            /*!HC:inArr1*/
            double temp, pivotItem;
            /*!HC:checkIdxAscNAN*/
            if (checkNaNIDXAsc(lo, ref hi, inc, vecIdx, vec)) return;
            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        ai1 = *(vecIdx + j);
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp <= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                            *(vecIdx + i + inc) = *(vecIdx + i);
                        }
                        *(vec + i + inc) = pivotItem;
                        *(vecIdx + i + inc) = ai1;
                    }
                    #endregion
                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    ai1 = *(vecIdx + lo);
                    ai2 = *(vecIdx + pivotIndex);
                    ai3 = *(vecIdx + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    *(vecIdx + pivotIndex) = *(vecIdx + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 > a2) {
                        if (a1 > a3) {
                            if (a3 > a2) {
                                *(vec + hi) = a1;
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vecIdx + hi) = ai1;
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                            } else {
                                *(vec + hi) = a1;
                                *(vec + lo) = a3;
                                *(vec + lo + inc) = a2;
                                *(vecIdx + hi) = ai1;
                                *(vecIdx + lo) = ai3;
                                *(vecIdx + lo + inc) = ai2;
                            }
                        } else {
                            *(vec + lo) = a2;
                            *(vec + lo + inc) = a1;
                            *(vecIdx + lo) = ai2;
                            *(vecIdx + lo + inc) = ai1;
                        }
                    } else {
                        if (a2 > a3) {
                            *(vec + hi) = a2;
                            *(vecIdx + hi) = ai2;
                            if (a3 > a1) {
                                *(vec + lo + inc) = a3;
                                *(vecIdx + lo + inc) = ai3;
                            } else {
                                *(vec + lo + inc) = a1;
                                *(vec + lo) = a3;
                                *(vecIdx + lo + inc) = ai1;
                                *(vecIdx + lo) = ai3;
                            }
                        } else {
                            *(vec + lo + inc) = a2;
                            *(vecIdx + lo + inc) = ai2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    ai1 = *(vecIdx + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) < pivotItem);
                        do j -= inc; while (*(vec + j) > pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        ai2 = *(vecIdx + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                        *(vecIdx + i) = *(vecIdx + j);
                        *(vecIdx + j) = ai2;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
                    *(vecIdx + lo + inc) = *(vecIdx + j);
                    *(vecIdx + j) = ai1;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, descending, arbitrary element spacing, indices aware.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="vecIdx">indices vector, content will in-place be sorted along with <paramref name="vec"/></param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortDescIDXST(/*!HC:inArr1*/ double* vec, /*!HC:inArr2*/ long* vecIdx, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

            /*!HC:inArr1*/
            double a1, a2, a3;
            /*!HC:inArr2*/
            long ai1, ai2, ai3;
            /*!HC:inArr1*/
            double temp, pivotItem;
            /*!HC:checkIdxDescNAN*/
            if (checkNaNIDXDesc(lo, ref hi, inc, vecIdx, vec)) return;
            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        ai1 = *(vecIdx + j);
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp >= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                            *(vecIdx + i + inc) = *(vecIdx + i);
                        }
                        *(vec + i + inc) = pivotItem;
                        *(vecIdx + i + inc) = ai1;
                    }
                    #endregion

                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    ai1 = *(vecIdx + lo);
                    ai2 = *(vecIdx + pivotIndex);
                    ai3 = *(vecIdx + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    *(vecIdx + pivotIndex) = *(vecIdx + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 < a2) {
                        if (a2 < a3) {
                            *(vec + hi) = a1;
                            *(vec + lo) = a3;
                            *(vec + lo + inc) = a2;
                            *(vecIdx + hi) = ai1;
                            *(vecIdx + lo) = ai3;
                            *(vecIdx + lo + inc) = ai2;
                        } else {
                            if (a1 < a3) {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vec + hi) = a1;
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                                *(vecIdx + hi) = ai1;
                            } else {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a1;
                                //*(vec + hi) = a3; 
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai1;
                                //*(vecIdx + hi) = ai3; 
                            }
                        }
                    } else {
                        if (a2 < a3) {
                            if (a1 < a3) {
                                *(vec + lo) = a3;
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a1;
                                *(vecIdx + lo) = ai3;
                                *(vecIdx + hi) = ai2;
                                *(vecIdx + lo + inc) = ai1;
                            } else {
                                //*(vec + lo) = a1; 
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a3;
                                //*(vecIdx + lo) = ai1; 
                                *(vecIdx + hi) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                            }
                        } else {
                            //*(vec + lo) = a1; 
                            //*(vec + hi) = a3; 
                            *(vec + lo + inc) = a2;
                            //*(vecIdx + lo) = ai1; 
                            //*(vecIdx + hi) = ai3; 
                            *(vecIdx + lo + inc) = ai2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    ai1 = *(vecIdx + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) > pivotItem);
                        do j -= inc; while (*(vec + j) < pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        ai2 = *(vecIdx + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                        *(vecIdx + i) = *(vecIdx + j);
                        *(vecIdx + j) = ai2;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
                    *(vecIdx + lo + inc) = *(vecIdx + j);
                    *(vecIdx + j) = ai1;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }

#endregion HYCALPER LOOPEND single threaded quick sorts
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 

        /// <summary>
        /// One dimensional quick sort, inline, ascending, arbitrary element spacing
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public static unsafe void QuickSortAscST( UInt64* vec, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0; 

           
            UInt64 a1, a2, a3;
           
            UInt64 temp, pivotItem;
            

            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp <= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                        }
                        *(vec + i + inc) = pivotItem;
                    }
                    #endregion

                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {

                    // do the quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 > a2) {
                        if (a1 > a3) {
                            if (a3 > a2) {
                                *(vec + hi) = a1;
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                            } else {
                                *(vec + hi) = a1;
                                *(vec + lo) = a3;
                                *(vec + lo + inc) = a2;
                            }
                        } else {
                            *(vec + lo) = a2;
                            *(vec + lo + inc) = a1;
                        }
                    } else {
                        if (a2 > a3) {
                            *(vec + hi) = a2;
                            if (a3 > a1) {
                                *(vec + lo + inc) = a3;
                            } else {
                                *(vec + lo + inc) = a1;
                                *(vec + lo) = a3;
                            }
                        } else {
                            *(vec + lo + inc) = a2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) < pivotItem);
                        do j -= inc; while (*(vec + j) > pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vec,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, descending, arbitrary element spacing.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortDescST( UInt64* vec, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            UInt64 a1, a2, a3;
           
            UInt64 temp, pivotItem;
            

            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp >= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                        }
                        *(vec + i + inc) = pivotItem;
                    }
                    #endregion
                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 < a2) {
                        if (a2 < a3) {
                            *(vec + hi) = a1;
                            *(vec + lo) = a3;
                            *(vec + lo + inc) = a2;
                        } else {
                            if (a1 < a3) {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vec + hi) = a1;
                            } else {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a1;
                                //*(vec + hi) = a3; 
                            }
                        }
                    } else {
                        if (a2 < a3) {
                            if (a1 < a3) {
                                *(vec + lo) = a3;
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a1;
                            } else {
                                //*(vec + lo) = a1; 
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a3;
                            }
                        } else {
                            //*(vec + lo) = a1; 
                            //*(vec + hi) = a3; 
                            *(vec + lo + inc) = a2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) > pivotItem);
                        do j -= inc; while (*(vec + j) < pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4D(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, ascending, arbitrary element spacing, indices aware.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="vecIdx">indices vector, content will in-place be sorted along with <paramref name="vec"/></param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MminimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortAscIDXST( UInt64* vec,  long* vecIdx, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            UInt64 a1, a2, a3;
           
            long ai1, ai2, ai3;
           
            UInt64 temp, pivotItem;
            
            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        ai1 = *(vecIdx + j);
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp <= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                            *(vecIdx + i + inc) = *(vecIdx + i);
                        }
                        *(vec + i + inc) = pivotItem;
                        *(vecIdx + i + inc) = ai1;
                    }
                    #endregion
                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    ai1 = *(vecIdx + lo);
                    ai2 = *(vecIdx + pivotIndex);
                    ai3 = *(vecIdx + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    *(vecIdx + pivotIndex) = *(vecIdx + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 > a2) {
                        if (a1 > a3) {
                            if (a3 > a2) {
                                *(vec + hi) = a1;
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vecIdx + hi) = ai1;
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                            } else {
                                *(vec + hi) = a1;
                                *(vec + lo) = a3;
                                *(vec + lo + inc) = a2;
                                *(vecIdx + hi) = ai1;
                                *(vecIdx + lo) = ai3;
                                *(vecIdx + lo + inc) = ai2;
                            }
                        } else {
                            *(vec + lo) = a2;
                            *(vec + lo + inc) = a1;
                            *(vecIdx + lo) = ai2;
                            *(vecIdx + lo + inc) = ai1;
                        }
                    } else {
                        if (a2 > a3) {
                            *(vec + hi) = a2;
                            *(vecIdx + hi) = ai2;
                            if (a3 > a1) {
                                *(vec + lo + inc) = a3;
                                *(vecIdx + lo + inc) = ai3;
                            } else {
                                *(vec + lo + inc) = a1;
                                *(vec + lo) = a3;
                                *(vecIdx + lo + inc) = ai1;
                                *(vecIdx + lo) = ai3;
                            }
                        } else {
                            *(vec + lo + inc) = a2;
                            *(vecIdx + lo + inc) = ai2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    ai1 = *(vecIdx + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) < pivotItem);
                        do j -= inc; while (*(vec + j) > pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        ai2 = *(vecIdx + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                        *(vecIdx + i) = *(vecIdx + j);
                        *(vecIdx + j) = ai2;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
                    *(vecIdx + lo + inc) = *(vecIdx + j);
                    *(vecIdx + j) = ai1;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, descending, arbitrary element spacing, indices aware.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="vecIdx">indices vector, content will in-place be sorted along with <paramref name="vec"/></param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortDescIDXST( UInt64* vec,  long* vecIdx, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            UInt64 a1, a2, a3;
           
            long ai1, ai2, ai3;
           
            UInt64 temp, pivotItem;
            
            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        ai1 = *(vecIdx + j);
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp >= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                            *(vecIdx + i + inc) = *(vecIdx + i);
                        }
                        *(vec + i + inc) = pivotItem;
                        *(vecIdx + i + inc) = ai1;
                    }
                    #endregion

                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    ai1 = *(vecIdx + lo);
                    ai2 = *(vecIdx + pivotIndex);
                    ai3 = *(vecIdx + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    *(vecIdx + pivotIndex) = *(vecIdx + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 < a2) {
                        if (a2 < a3) {
                            *(vec + hi) = a1;
                            *(vec + lo) = a3;
                            *(vec + lo + inc) = a2;
                            *(vecIdx + hi) = ai1;
                            *(vecIdx + lo) = ai3;
                            *(vecIdx + lo + inc) = ai2;
                        } else {
                            if (a1 < a3) {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vec + hi) = a1;
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                                *(vecIdx + hi) = ai1;
                            } else {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a1;
                                //*(vec + hi) = a3; 
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai1;
                                //*(vecIdx + hi) = ai3; 
                            }
                        }
                    } else {
                        if (a2 < a3) {
                            if (a1 < a3) {
                                *(vec + lo) = a3;
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a1;
                                *(vecIdx + lo) = ai3;
                                *(vecIdx + hi) = ai2;
                                *(vecIdx + lo + inc) = ai1;
                            } else {
                                //*(vec + lo) = a1; 
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a3;
                                //*(vecIdx + lo) = ai1; 
                                *(vecIdx + hi) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                            }
                        } else {
                            //*(vec + lo) = a1; 
                            //*(vec + hi) = a3; 
                            *(vec + lo + inc) = a2;
                            //*(vecIdx + lo) = ai1; 
                            //*(vecIdx + hi) = ai3; 
                            *(vecIdx + lo + inc) = ai2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    ai1 = *(vecIdx + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) > pivotItem);
                        do j -= inc; while (*(vec + j) < pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        ai2 = *(vecIdx + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                        *(vecIdx + i) = *(vecIdx + j);
                        *(vecIdx + j) = ai2;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
                    *(vecIdx + lo + inc) = *(vecIdx + j);
                    *(vecIdx + j) = ai1;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }


        /// <summary>
        /// One dimensional quick sort, inline, ascending, arbitrary element spacing
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public static unsafe void QuickSortAscST( UInt32* vec, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0; 

           
            UInt32 a1, a2, a3;
           
            UInt32 temp, pivotItem;
            

            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp <= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                        }
                        *(vec + i + inc) = pivotItem;
                    }
                    #endregion

                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {

                    // do the quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 > a2) {
                        if (a1 > a3) {
                            if (a3 > a2) {
                                *(vec + hi) = a1;
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                            } else {
                                *(vec + hi) = a1;
                                *(vec + lo) = a3;
                                *(vec + lo + inc) = a2;
                            }
                        } else {
                            *(vec + lo) = a2;
                            *(vec + lo + inc) = a1;
                        }
                    } else {
                        if (a2 > a3) {
                            *(vec + hi) = a2;
                            if (a3 > a1) {
                                *(vec + lo + inc) = a3;
                            } else {
                                *(vec + lo + inc) = a1;
                                *(vec + lo) = a3;
                            }
                        } else {
                            *(vec + lo + inc) = a2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) < pivotItem);
                        do j -= inc; while (*(vec + j) > pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vec,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, descending, arbitrary element spacing.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortDescST( UInt32* vec, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            UInt32 a1, a2, a3;
           
            UInt32 temp, pivotItem;
            

            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp >= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                        }
                        *(vec + i + inc) = pivotItem;
                    }
                    #endregion
                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 < a2) {
                        if (a2 < a3) {
                            *(vec + hi) = a1;
                            *(vec + lo) = a3;
                            *(vec + lo + inc) = a2;
                        } else {
                            if (a1 < a3) {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vec + hi) = a1;
                            } else {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a1;
                                //*(vec + hi) = a3; 
                            }
                        }
                    } else {
                        if (a2 < a3) {
                            if (a1 < a3) {
                                *(vec + lo) = a3;
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a1;
                            } else {
                                //*(vec + lo) = a1; 
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a3;
                            }
                        } else {
                            //*(vec + lo) = a1; 
                            //*(vec + hi) = a3; 
                            *(vec + lo + inc) = a2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) > pivotItem);
                        do j -= inc; while (*(vec + j) < pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4D(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, ascending, arbitrary element spacing, indices aware.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="vecIdx">indices vector, content will in-place be sorted along with <paramref name="vec"/></param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MminimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortAscIDXST( UInt32* vec,  long* vecIdx, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            UInt32 a1, a2, a3;
           
            long ai1, ai2, ai3;
           
            UInt32 temp, pivotItem;
            
            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        ai1 = *(vecIdx + j);
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp <= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                            *(vecIdx + i + inc) = *(vecIdx + i);
                        }
                        *(vec + i + inc) = pivotItem;
                        *(vecIdx + i + inc) = ai1;
                    }
                    #endregion
                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    ai1 = *(vecIdx + lo);
                    ai2 = *(vecIdx + pivotIndex);
                    ai3 = *(vecIdx + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    *(vecIdx + pivotIndex) = *(vecIdx + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 > a2) {
                        if (a1 > a3) {
                            if (a3 > a2) {
                                *(vec + hi) = a1;
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vecIdx + hi) = ai1;
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                            } else {
                                *(vec + hi) = a1;
                                *(vec + lo) = a3;
                                *(vec + lo + inc) = a2;
                                *(vecIdx + hi) = ai1;
                                *(vecIdx + lo) = ai3;
                                *(vecIdx + lo + inc) = ai2;
                            }
                        } else {
                            *(vec + lo) = a2;
                            *(vec + lo + inc) = a1;
                            *(vecIdx + lo) = ai2;
                            *(vecIdx + lo + inc) = ai1;
                        }
                    } else {
                        if (a2 > a3) {
                            *(vec + hi) = a2;
                            *(vecIdx + hi) = ai2;
                            if (a3 > a1) {
                                *(vec + lo + inc) = a3;
                                *(vecIdx + lo + inc) = ai3;
                            } else {
                                *(vec + lo + inc) = a1;
                                *(vec + lo) = a3;
                                *(vecIdx + lo + inc) = ai1;
                                *(vecIdx + lo) = ai3;
                            }
                        } else {
                            *(vec + lo + inc) = a2;
                            *(vecIdx + lo + inc) = ai2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    ai1 = *(vecIdx + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) < pivotItem);
                        do j -= inc; while (*(vec + j) > pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        ai2 = *(vecIdx + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                        *(vecIdx + i) = *(vecIdx + j);
                        *(vecIdx + j) = ai2;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
                    *(vecIdx + lo + inc) = *(vecIdx + j);
                    *(vecIdx + j) = ai1;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, descending, arbitrary element spacing, indices aware.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="vecIdx">indices vector, content will in-place be sorted along with <paramref name="vec"/></param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortDescIDXST( UInt32* vec,  long* vecIdx, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            UInt32 a1, a2, a3;
           
            long ai1, ai2, ai3;
           
            UInt32 temp, pivotItem;
            
            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        ai1 = *(vecIdx + j);
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp >= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                            *(vecIdx + i + inc) = *(vecIdx + i);
                        }
                        *(vec + i + inc) = pivotItem;
                        *(vecIdx + i + inc) = ai1;
                    }
                    #endregion

                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    ai1 = *(vecIdx + lo);
                    ai2 = *(vecIdx + pivotIndex);
                    ai3 = *(vecIdx + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    *(vecIdx + pivotIndex) = *(vecIdx + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 < a2) {
                        if (a2 < a3) {
                            *(vec + hi) = a1;
                            *(vec + lo) = a3;
                            *(vec + lo + inc) = a2;
                            *(vecIdx + hi) = ai1;
                            *(vecIdx + lo) = ai3;
                            *(vecIdx + lo + inc) = ai2;
                        } else {
                            if (a1 < a3) {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vec + hi) = a1;
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                                *(vecIdx + hi) = ai1;
                            } else {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a1;
                                //*(vec + hi) = a3; 
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai1;
                                //*(vecIdx + hi) = ai3; 
                            }
                        }
                    } else {
                        if (a2 < a3) {
                            if (a1 < a3) {
                                *(vec + lo) = a3;
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a1;
                                *(vecIdx + lo) = ai3;
                                *(vecIdx + hi) = ai2;
                                *(vecIdx + lo + inc) = ai1;
                            } else {
                                //*(vec + lo) = a1; 
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a3;
                                //*(vecIdx + lo) = ai1; 
                                *(vecIdx + hi) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                            }
                        } else {
                            //*(vec + lo) = a1; 
                            //*(vec + hi) = a3; 
                            *(vec + lo + inc) = a2;
                            //*(vecIdx + lo) = ai1; 
                            //*(vecIdx + hi) = ai3; 
                            *(vecIdx + lo + inc) = ai2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    ai1 = *(vecIdx + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) > pivotItem);
                        do j -= inc; while (*(vec + j) < pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        ai2 = *(vecIdx + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                        *(vecIdx + i) = *(vecIdx + j);
                        *(vecIdx + j) = ai2;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
                    *(vecIdx + lo + inc) = *(vecIdx + j);
                    *(vecIdx + j) = ai1;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }


        /// <summary>
        /// One dimensional quick sort, inline, ascending, arbitrary element spacing
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public static unsafe void QuickSortAscST( UInt16* vec, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0; 

           
            UInt16 a1, a2, a3;
           
            UInt16 temp, pivotItem;
            

            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp <= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                        }
                        *(vec + i + inc) = pivotItem;
                    }
                    #endregion

                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {

                    // do the quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 > a2) {
                        if (a1 > a3) {
                            if (a3 > a2) {
                                *(vec + hi) = a1;
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                            } else {
                                *(vec + hi) = a1;
                                *(vec + lo) = a3;
                                *(vec + lo + inc) = a2;
                            }
                        } else {
                            *(vec + lo) = a2;
                            *(vec + lo + inc) = a1;
                        }
                    } else {
                        if (a2 > a3) {
                            *(vec + hi) = a2;
                            if (a3 > a1) {
                                *(vec + lo + inc) = a3;
                            } else {
                                *(vec + lo + inc) = a1;
                                *(vec + lo) = a3;
                            }
                        } else {
                            *(vec + lo + inc) = a2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) < pivotItem);
                        do j -= inc; while (*(vec + j) > pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vec,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, descending, arbitrary element spacing.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortDescST( UInt16* vec, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            UInt16 a1, a2, a3;
           
            UInt16 temp, pivotItem;
            

            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp >= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                        }
                        *(vec + i + inc) = pivotItem;
                    }
                    #endregion
                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 < a2) {
                        if (a2 < a3) {
                            *(vec + hi) = a1;
                            *(vec + lo) = a3;
                            *(vec + lo + inc) = a2;
                        } else {
                            if (a1 < a3) {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vec + hi) = a1;
                            } else {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a1;
                                //*(vec + hi) = a3; 
                            }
                        }
                    } else {
                        if (a2 < a3) {
                            if (a1 < a3) {
                                *(vec + lo) = a3;
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a1;
                            } else {
                                //*(vec + lo) = a1; 
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a3;
                            }
                        } else {
                            //*(vec + lo) = a1; 
                            //*(vec + hi) = a3; 
                            *(vec + lo + inc) = a2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) > pivotItem);
                        do j -= inc; while (*(vec + j) < pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4D(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, ascending, arbitrary element spacing, indices aware.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="vecIdx">indices vector, content will in-place be sorted along with <paramref name="vec"/></param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MminimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortAscIDXST( UInt16* vec,  long* vecIdx, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            UInt16 a1, a2, a3;
           
            long ai1, ai2, ai3;
           
            UInt16 temp, pivotItem;
            
            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        ai1 = *(vecIdx + j);
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp <= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                            *(vecIdx + i + inc) = *(vecIdx + i);
                        }
                        *(vec + i + inc) = pivotItem;
                        *(vecIdx + i + inc) = ai1;
                    }
                    #endregion
                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    ai1 = *(vecIdx + lo);
                    ai2 = *(vecIdx + pivotIndex);
                    ai3 = *(vecIdx + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    *(vecIdx + pivotIndex) = *(vecIdx + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 > a2) {
                        if (a1 > a3) {
                            if (a3 > a2) {
                                *(vec + hi) = a1;
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vecIdx + hi) = ai1;
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                            } else {
                                *(vec + hi) = a1;
                                *(vec + lo) = a3;
                                *(vec + lo + inc) = a2;
                                *(vecIdx + hi) = ai1;
                                *(vecIdx + lo) = ai3;
                                *(vecIdx + lo + inc) = ai2;
                            }
                        } else {
                            *(vec + lo) = a2;
                            *(vec + lo + inc) = a1;
                            *(vecIdx + lo) = ai2;
                            *(vecIdx + lo + inc) = ai1;
                        }
                    } else {
                        if (a2 > a3) {
                            *(vec + hi) = a2;
                            *(vecIdx + hi) = ai2;
                            if (a3 > a1) {
                                *(vec + lo + inc) = a3;
                                *(vecIdx + lo + inc) = ai3;
                            } else {
                                *(vec + lo + inc) = a1;
                                *(vec + lo) = a3;
                                *(vecIdx + lo + inc) = ai1;
                                *(vecIdx + lo) = ai3;
                            }
                        } else {
                            *(vec + lo + inc) = a2;
                            *(vecIdx + lo + inc) = ai2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    ai1 = *(vecIdx + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) < pivotItem);
                        do j -= inc; while (*(vec + j) > pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        ai2 = *(vecIdx + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                        *(vecIdx + i) = *(vecIdx + j);
                        *(vecIdx + j) = ai2;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
                    *(vecIdx + lo + inc) = *(vecIdx + j);
                    *(vecIdx + j) = ai1;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, descending, arbitrary element spacing, indices aware.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="vecIdx">indices vector, content will in-place be sorted along with <paramref name="vec"/></param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortDescIDXST( UInt16* vec,  long* vecIdx, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            UInt16 a1, a2, a3;
           
            long ai1, ai2, ai3;
           
            UInt16 temp, pivotItem;
            
            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        ai1 = *(vecIdx + j);
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp >= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                            *(vecIdx + i + inc) = *(vecIdx + i);
                        }
                        *(vec + i + inc) = pivotItem;
                        *(vecIdx + i + inc) = ai1;
                    }
                    #endregion

                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    ai1 = *(vecIdx + lo);
                    ai2 = *(vecIdx + pivotIndex);
                    ai3 = *(vecIdx + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    *(vecIdx + pivotIndex) = *(vecIdx + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 < a2) {
                        if (a2 < a3) {
                            *(vec + hi) = a1;
                            *(vec + lo) = a3;
                            *(vec + lo + inc) = a2;
                            *(vecIdx + hi) = ai1;
                            *(vecIdx + lo) = ai3;
                            *(vecIdx + lo + inc) = ai2;
                        } else {
                            if (a1 < a3) {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vec + hi) = a1;
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                                *(vecIdx + hi) = ai1;
                            } else {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a1;
                                //*(vec + hi) = a3; 
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai1;
                                //*(vecIdx + hi) = ai3; 
                            }
                        }
                    } else {
                        if (a2 < a3) {
                            if (a1 < a3) {
                                *(vec + lo) = a3;
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a1;
                                *(vecIdx + lo) = ai3;
                                *(vecIdx + hi) = ai2;
                                *(vecIdx + lo + inc) = ai1;
                            } else {
                                //*(vec + lo) = a1; 
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a3;
                                //*(vecIdx + lo) = ai1; 
                                *(vecIdx + hi) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                            }
                        } else {
                            //*(vec + lo) = a1; 
                            //*(vec + hi) = a3; 
                            *(vec + lo + inc) = a2;
                            //*(vecIdx + lo) = ai1; 
                            //*(vecIdx + hi) = ai3; 
                            *(vecIdx + lo + inc) = ai2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    ai1 = *(vecIdx + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) > pivotItem);
                        do j -= inc; while (*(vec + j) < pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        ai2 = *(vecIdx + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                        *(vecIdx + i) = *(vecIdx + j);
                        *(vecIdx + j) = ai2;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
                    *(vecIdx + lo + inc) = *(vecIdx + j);
                    *(vecIdx + j) = ai1;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }


        /// <summary>
        /// One dimensional quick sort, inline, ascending, arbitrary element spacing
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public static unsafe void QuickSortAscST( Int64* vec, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0; 

           
            Int64 a1, a2, a3;
           
            Int64 temp, pivotItem;
            

            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp <= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                        }
                        *(vec + i + inc) = pivotItem;
                    }
                    #endregion

                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {

                    // do the quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 > a2) {
                        if (a1 > a3) {
                            if (a3 > a2) {
                                *(vec + hi) = a1;
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                            } else {
                                *(vec + hi) = a1;
                                *(vec + lo) = a3;
                                *(vec + lo + inc) = a2;
                            }
                        } else {
                            *(vec + lo) = a2;
                            *(vec + lo + inc) = a1;
                        }
                    } else {
                        if (a2 > a3) {
                            *(vec + hi) = a2;
                            if (a3 > a1) {
                                *(vec + lo + inc) = a3;
                            } else {
                                *(vec + lo + inc) = a1;
                                *(vec + lo) = a3;
                            }
                        } else {
                            *(vec + lo + inc) = a2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) < pivotItem);
                        do j -= inc; while (*(vec + j) > pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vec,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, descending, arbitrary element spacing.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortDescST( Int64* vec, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            Int64 a1, a2, a3;
           
            Int64 temp, pivotItem;
            

            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp >= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                        }
                        *(vec + i + inc) = pivotItem;
                    }
                    #endregion
                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 < a2) {
                        if (a2 < a3) {
                            *(vec + hi) = a1;
                            *(vec + lo) = a3;
                            *(vec + lo + inc) = a2;
                        } else {
                            if (a1 < a3) {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vec + hi) = a1;
                            } else {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a1;
                                //*(vec + hi) = a3; 
                            }
                        }
                    } else {
                        if (a2 < a3) {
                            if (a1 < a3) {
                                *(vec + lo) = a3;
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a1;
                            } else {
                                //*(vec + lo) = a1; 
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a3;
                            }
                        } else {
                            //*(vec + lo) = a1; 
                            //*(vec + hi) = a3; 
                            *(vec + lo + inc) = a2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) > pivotItem);
                        do j -= inc; while (*(vec + j) < pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4D(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, ascending, arbitrary element spacing, indices aware.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="vecIdx">indices vector, content will in-place be sorted along with <paramref name="vec"/></param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MminimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortAscIDXST( Int64* vec,  long* vecIdx, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            Int64 a1, a2, a3;
           
            long ai1, ai2, ai3;
           
            Int64 temp, pivotItem;
            
            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        ai1 = *(vecIdx + j);
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp <= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                            *(vecIdx + i + inc) = *(vecIdx + i);
                        }
                        *(vec + i + inc) = pivotItem;
                        *(vecIdx + i + inc) = ai1;
                    }
                    #endregion
                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    ai1 = *(vecIdx + lo);
                    ai2 = *(vecIdx + pivotIndex);
                    ai3 = *(vecIdx + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    *(vecIdx + pivotIndex) = *(vecIdx + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 > a2) {
                        if (a1 > a3) {
                            if (a3 > a2) {
                                *(vec + hi) = a1;
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vecIdx + hi) = ai1;
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                            } else {
                                *(vec + hi) = a1;
                                *(vec + lo) = a3;
                                *(vec + lo + inc) = a2;
                                *(vecIdx + hi) = ai1;
                                *(vecIdx + lo) = ai3;
                                *(vecIdx + lo + inc) = ai2;
                            }
                        } else {
                            *(vec + lo) = a2;
                            *(vec + lo + inc) = a1;
                            *(vecIdx + lo) = ai2;
                            *(vecIdx + lo + inc) = ai1;
                        }
                    } else {
                        if (a2 > a3) {
                            *(vec + hi) = a2;
                            *(vecIdx + hi) = ai2;
                            if (a3 > a1) {
                                *(vec + lo + inc) = a3;
                                *(vecIdx + lo + inc) = ai3;
                            } else {
                                *(vec + lo + inc) = a1;
                                *(vec + lo) = a3;
                                *(vecIdx + lo + inc) = ai1;
                                *(vecIdx + lo) = ai3;
                            }
                        } else {
                            *(vec + lo + inc) = a2;
                            *(vecIdx + lo + inc) = ai2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    ai1 = *(vecIdx + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) < pivotItem);
                        do j -= inc; while (*(vec + j) > pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        ai2 = *(vecIdx + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                        *(vecIdx + i) = *(vecIdx + j);
                        *(vecIdx + j) = ai2;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
                    *(vecIdx + lo + inc) = *(vecIdx + j);
                    *(vecIdx + j) = ai1;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, descending, arbitrary element spacing, indices aware.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="vecIdx">indices vector, content will in-place be sorted along with <paramref name="vec"/></param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortDescIDXST( Int64* vec,  long* vecIdx, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            Int64 a1, a2, a3;
           
            long ai1, ai2, ai3;
           
            Int64 temp, pivotItem;
            
            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        ai1 = *(vecIdx + j);
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp >= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                            *(vecIdx + i + inc) = *(vecIdx + i);
                        }
                        *(vec + i + inc) = pivotItem;
                        *(vecIdx + i + inc) = ai1;
                    }
                    #endregion

                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    ai1 = *(vecIdx + lo);
                    ai2 = *(vecIdx + pivotIndex);
                    ai3 = *(vecIdx + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    *(vecIdx + pivotIndex) = *(vecIdx + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 < a2) {
                        if (a2 < a3) {
                            *(vec + hi) = a1;
                            *(vec + lo) = a3;
                            *(vec + lo + inc) = a2;
                            *(vecIdx + hi) = ai1;
                            *(vecIdx + lo) = ai3;
                            *(vecIdx + lo + inc) = ai2;
                        } else {
                            if (a1 < a3) {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vec + hi) = a1;
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                                *(vecIdx + hi) = ai1;
                            } else {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a1;
                                //*(vec + hi) = a3; 
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai1;
                                //*(vecIdx + hi) = ai3; 
                            }
                        }
                    } else {
                        if (a2 < a3) {
                            if (a1 < a3) {
                                *(vec + lo) = a3;
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a1;
                                *(vecIdx + lo) = ai3;
                                *(vecIdx + hi) = ai2;
                                *(vecIdx + lo + inc) = ai1;
                            } else {
                                //*(vec + lo) = a1; 
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a3;
                                //*(vecIdx + lo) = ai1; 
                                *(vecIdx + hi) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                            }
                        } else {
                            //*(vec + lo) = a1; 
                            //*(vec + hi) = a3; 
                            *(vec + lo + inc) = a2;
                            //*(vecIdx + lo) = ai1; 
                            //*(vecIdx + hi) = ai3; 
                            *(vecIdx + lo + inc) = ai2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    ai1 = *(vecIdx + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) > pivotItem);
                        do j -= inc; while (*(vec + j) < pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        ai2 = *(vecIdx + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                        *(vecIdx + i) = *(vecIdx + j);
                        *(vecIdx + j) = ai2;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
                    *(vecIdx + lo + inc) = *(vecIdx + j);
                    *(vecIdx + j) = ai1;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }


        /// <summary>
        /// One dimensional quick sort, inline, ascending, arbitrary element spacing
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public static unsafe void QuickSortAscST( Int32* vec, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0; 

           
            Int32 a1, a2, a3;
           
            Int32 temp, pivotItem;
            

            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp <= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                        }
                        *(vec + i + inc) = pivotItem;
                    }
                    #endregion

                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {

                    // do the quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 > a2) {
                        if (a1 > a3) {
                            if (a3 > a2) {
                                *(vec + hi) = a1;
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                            } else {
                                *(vec + hi) = a1;
                                *(vec + lo) = a3;
                                *(vec + lo + inc) = a2;
                            }
                        } else {
                            *(vec + lo) = a2;
                            *(vec + lo + inc) = a1;
                        }
                    } else {
                        if (a2 > a3) {
                            *(vec + hi) = a2;
                            if (a3 > a1) {
                                *(vec + lo + inc) = a3;
                            } else {
                                *(vec + lo + inc) = a1;
                                *(vec + lo) = a3;
                            }
                        } else {
                            *(vec + lo + inc) = a2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) < pivotItem);
                        do j -= inc; while (*(vec + j) > pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vec,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, descending, arbitrary element spacing.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortDescST( Int32* vec, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            Int32 a1, a2, a3;
           
            Int32 temp, pivotItem;
            

            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp >= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                        }
                        *(vec + i + inc) = pivotItem;
                    }
                    #endregion
                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 < a2) {
                        if (a2 < a3) {
                            *(vec + hi) = a1;
                            *(vec + lo) = a3;
                            *(vec + lo + inc) = a2;
                        } else {
                            if (a1 < a3) {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vec + hi) = a1;
                            } else {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a1;
                                //*(vec + hi) = a3; 
                            }
                        }
                    } else {
                        if (a2 < a3) {
                            if (a1 < a3) {
                                *(vec + lo) = a3;
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a1;
                            } else {
                                //*(vec + lo) = a1; 
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a3;
                            }
                        } else {
                            //*(vec + lo) = a1; 
                            //*(vec + hi) = a3; 
                            *(vec + lo + inc) = a2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) > pivotItem);
                        do j -= inc; while (*(vec + j) < pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4D(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, ascending, arbitrary element spacing, indices aware.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="vecIdx">indices vector, content will in-place be sorted along with <paramref name="vec"/></param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MminimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortAscIDXST( Int32* vec,  long* vecIdx, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            Int32 a1, a2, a3;
           
            long ai1, ai2, ai3;
           
            Int32 temp, pivotItem;
            
            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        ai1 = *(vecIdx + j);
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp <= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                            *(vecIdx + i + inc) = *(vecIdx + i);
                        }
                        *(vec + i + inc) = pivotItem;
                        *(vecIdx + i + inc) = ai1;
                    }
                    #endregion
                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    ai1 = *(vecIdx + lo);
                    ai2 = *(vecIdx + pivotIndex);
                    ai3 = *(vecIdx + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    *(vecIdx + pivotIndex) = *(vecIdx + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 > a2) {
                        if (a1 > a3) {
                            if (a3 > a2) {
                                *(vec + hi) = a1;
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vecIdx + hi) = ai1;
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                            } else {
                                *(vec + hi) = a1;
                                *(vec + lo) = a3;
                                *(vec + lo + inc) = a2;
                                *(vecIdx + hi) = ai1;
                                *(vecIdx + lo) = ai3;
                                *(vecIdx + lo + inc) = ai2;
                            }
                        } else {
                            *(vec + lo) = a2;
                            *(vec + lo + inc) = a1;
                            *(vecIdx + lo) = ai2;
                            *(vecIdx + lo + inc) = ai1;
                        }
                    } else {
                        if (a2 > a3) {
                            *(vec + hi) = a2;
                            *(vecIdx + hi) = ai2;
                            if (a3 > a1) {
                                *(vec + lo + inc) = a3;
                                *(vecIdx + lo + inc) = ai3;
                            } else {
                                *(vec + lo + inc) = a1;
                                *(vec + lo) = a3;
                                *(vecIdx + lo + inc) = ai1;
                                *(vecIdx + lo) = ai3;
                            }
                        } else {
                            *(vec + lo + inc) = a2;
                            *(vecIdx + lo + inc) = ai2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    ai1 = *(vecIdx + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) < pivotItem);
                        do j -= inc; while (*(vec + j) > pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        ai2 = *(vecIdx + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                        *(vecIdx + i) = *(vecIdx + j);
                        *(vecIdx + j) = ai2;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
                    *(vecIdx + lo + inc) = *(vecIdx + j);
                    *(vecIdx + j) = ai1;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, descending, arbitrary element spacing, indices aware.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="vecIdx">indices vector, content will in-place be sorted along with <paramref name="vec"/></param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortDescIDXST( Int32* vec,  long* vecIdx, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            Int32 a1, a2, a3;
           
            long ai1, ai2, ai3;
           
            Int32 temp, pivotItem;
            
            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        ai1 = *(vecIdx + j);
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp >= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                            *(vecIdx + i + inc) = *(vecIdx + i);
                        }
                        *(vec + i + inc) = pivotItem;
                        *(vecIdx + i + inc) = ai1;
                    }
                    #endregion

                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    ai1 = *(vecIdx + lo);
                    ai2 = *(vecIdx + pivotIndex);
                    ai3 = *(vecIdx + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    *(vecIdx + pivotIndex) = *(vecIdx + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 < a2) {
                        if (a2 < a3) {
                            *(vec + hi) = a1;
                            *(vec + lo) = a3;
                            *(vec + lo + inc) = a2;
                            *(vecIdx + hi) = ai1;
                            *(vecIdx + lo) = ai3;
                            *(vecIdx + lo + inc) = ai2;
                        } else {
                            if (a1 < a3) {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vec + hi) = a1;
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                                *(vecIdx + hi) = ai1;
                            } else {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a1;
                                //*(vec + hi) = a3; 
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai1;
                                //*(vecIdx + hi) = ai3; 
                            }
                        }
                    } else {
                        if (a2 < a3) {
                            if (a1 < a3) {
                                *(vec + lo) = a3;
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a1;
                                *(vecIdx + lo) = ai3;
                                *(vecIdx + hi) = ai2;
                                *(vecIdx + lo + inc) = ai1;
                            } else {
                                //*(vec + lo) = a1; 
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a3;
                                //*(vecIdx + lo) = ai1; 
                                *(vecIdx + hi) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                            }
                        } else {
                            //*(vec + lo) = a1; 
                            //*(vec + hi) = a3; 
                            *(vec + lo + inc) = a2;
                            //*(vecIdx + lo) = ai1; 
                            //*(vecIdx + hi) = ai3; 
                            *(vecIdx + lo + inc) = ai2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    ai1 = *(vecIdx + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) > pivotItem);
                        do j -= inc; while (*(vec + j) < pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        ai2 = *(vecIdx + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                        *(vecIdx + i) = *(vecIdx + j);
                        *(vecIdx + j) = ai2;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
                    *(vecIdx + lo + inc) = *(vecIdx + j);
                    *(vecIdx + j) = ai1;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }


        /// <summary>
        /// One dimensional quick sort, inline, ascending, arbitrary element spacing
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public static unsafe void QuickSortAscST( Int16* vec, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0; 

           
            Int16 a1, a2, a3;
           
            Int16 temp, pivotItem;
            

            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp <= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                        }
                        *(vec + i + inc) = pivotItem;
                    }
                    #endregion

                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {

                    // do the quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 > a2) {
                        if (a1 > a3) {
                            if (a3 > a2) {
                                *(vec + hi) = a1;
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                            } else {
                                *(vec + hi) = a1;
                                *(vec + lo) = a3;
                                *(vec + lo + inc) = a2;
                            }
                        } else {
                            *(vec + lo) = a2;
                            *(vec + lo + inc) = a1;
                        }
                    } else {
                        if (a2 > a3) {
                            *(vec + hi) = a2;
                            if (a3 > a1) {
                                *(vec + lo + inc) = a3;
                            } else {
                                *(vec + lo + inc) = a1;
                                *(vec + lo) = a3;
                            }
                        } else {
                            *(vec + lo + inc) = a2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) < pivotItem);
                        do j -= inc; while (*(vec + j) > pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vec,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, descending, arbitrary element spacing.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortDescST( Int16* vec, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            Int16 a1, a2, a3;
           
            Int16 temp, pivotItem;
            

            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp >= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                        }
                        *(vec + i + inc) = pivotItem;
                    }
                    #endregion
                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 < a2) {
                        if (a2 < a3) {
                            *(vec + hi) = a1;
                            *(vec + lo) = a3;
                            *(vec + lo + inc) = a2;
                        } else {
                            if (a1 < a3) {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vec + hi) = a1;
                            } else {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a1;
                                //*(vec + hi) = a3; 
                            }
                        }
                    } else {
                        if (a2 < a3) {
                            if (a1 < a3) {
                                *(vec + lo) = a3;
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a1;
                            } else {
                                //*(vec + lo) = a1; 
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a3;
                            }
                        } else {
                            //*(vec + lo) = a1; 
                            //*(vec + hi) = a3; 
                            *(vec + lo + inc) = a2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) > pivotItem);
                        do j -= inc; while (*(vec + j) < pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4D(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, ascending, arbitrary element spacing, indices aware.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="vecIdx">indices vector, content will in-place be sorted along with <paramref name="vec"/></param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MminimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortAscIDXST( Int16* vec,  long* vecIdx, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            Int16 a1, a2, a3;
           
            long ai1, ai2, ai3;
           
            Int16 temp, pivotItem;
            
            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        ai1 = *(vecIdx + j);
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp <= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                            *(vecIdx + i + inc) = *(vecIdx + i);
                        }
                        *(vec + i + inc) = pivotItem;
                        *(vecIdx + i + inc) = ai1;
                    }
                    #endregion
                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    ai1 = *(vecIdx + lo);
                    ai2 = *(vecIdx + pivotIndex);
                    ai3 = *(vecIdx + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    *(vecIdx + pivotIndex) = *(vecIdx + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 > a2) {
                        if (a1 > a3) {
                            if (a3 > a2) {
                                *(vec + hi) = a1;
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vecIdx + hi) = ai1;
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                            } else {
                                *(vec + hi) = a1;
                                *(vec + lo) = a3;
                                *(vec + lo + inc) = a2;
                                *(vecIdx + hi) = ai1;
                                *(vecIdx + lo) = ai3;
                                *(vecIdx + lo + inc) = ai2;
                            }
                        } else {
                            *(vec + lo) = a2;
                            *(vec + lo + inc) = a1;
                            *(vecIdx + lo) = ai2;
                            *(vecIdx + lo + inc) = ai1;
                        }
                    } else {
                        if (a2 > a3) {
                            *(vec + hi) = a2;
                            *(vecIdx + hi) = ai2;
                            if (a3 > a1) {
                                *(vec + lo + inc) = a3;
                                *(vecIdx + lo + inc) = ai3;
                            } else {
                                *(vec + lo + inc) = a1;
                                *(vec + lo) = a3;
                                *(vecIdx + lo + inc) = ai1;
                                *(vecIdx + lo) = ai3;
                            }
                        } else {
                            *(vec + lo + inc) = a2;
                            *(vecIdx + lo + inc) = ai2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    ai1 = *(vecIdx + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) < pivotItem);
                        do j -= inc; while (*(vec + j) > pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        ai2 = *(vecIdx + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                        *(vecIdx + i) = *(vecIdx + j);
                        *(vecIdx + j) = ai2;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
                    *(vecIdx + lo + inc) = *(vecIdx + j);
                    *(vecIdx + j) = ai1;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, descending, arbitrary element spacing, indices aware.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="vecIdx">indices vector, content will in-place be sorted along with <paramref name="vec"/></param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortDescIDXST( Int16* vec,  long* vecIdx, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            Int16 a1, a2, a3;
           
            long ai1, ai2, ai3;
           
            Int16 temp, pivotItem;
            
            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        ai1 = *(vecIdx + j);
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp >= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                            *(vecIdx + i + inc) = *(vecIdx + i);
                        }
                        *(vec + i + inc) = pivotItem;
                        *(vecIdx + i + inc) = ai1;
                    }
                    #endregion

                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    ai1 = *(vecIdx + lo);
                    ai2 = *(vecIdx + pivotIndex);
                    ai3 = *(vecIdx + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    *(vecIdx + pivotIndex) = *(vecIdx + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 < a2) {
                        if (a2 < a3) {
                            *(vec + hi) = a1;
                            *(vec + lo) = a3;
                            *(vec + lo + inc) = a2;
                            *(vecIdx + hi) = ai1;
                            *(vecIdx + lo) = ai3;
                            *(vecIdx + lo + inc) = ai2;
                        } else {
                            if (a1 < a3) {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vec + hi) = a1;
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                                *(vecIdx + hi) = ai1;
                            } else {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a1;
                                //*(vec + hi) = a3; 
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai1;
                                //*(vecIdx + hi) = ai3; 
                            }
                        }
                    } else {
                        if (a2 < a3) {
                            if (a1 < a3) {
                                *(vec + lo) = a3;
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a1;
                                *(vecIdx + lo) = ai3;
                                *(vecIdx + hi) = ai2;
                                *(vecIdx + lo + inc) = ai1;
                            } else {
                                //*(vec + lo) = a1; 
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a3;
                                //*(vecIdx + lo) = ai1; 
                                *(vecIdx + hi) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                            }
                        } else {
                            //*(vec + lo) = a1; 
                            //*(vec + hi) = a3; 
                            *(vec + lo + inc) = a2;
                            //*(vecIdx + lo) = ai1; 
                            //*(vecIdx + hi) = ai3; 
                            *(vecIdx + lo + inc) = ai2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    ai1 = *(vecIdx + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) > pivotItem);
                        do j -= inc; while (*(vec + j) < pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        ai2 = *(vecIdx + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                        *(vecIdx + i) = *(vecIdx + j);
                        *(vecIdx + j) = ai2;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
                    *(vecIdx + lo + inc) = *(vecIdx + j);
                    *(vecIdx + j) = ai1;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }


        /// <summary>
        /// One dimensional quick sort, inline, ascending, arbitrary element spacing
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public static unsafe void QuickSortAscST( float* vec, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0; 

           
            float a1, a2, a3;
           
            float temp, pivotItem;
            if (checkNaNAsc(lo, ref hi, inc, vec)) return;

            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp <= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                        }
                        *(vec + i + inc) = pivotItem;
                    }
                    #endregion

                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {

                    // do the quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 > a2) {
                        if (a1 > a3) {
                            if (a3 > a2) {
                                *(vec + hi) = a1;
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                            } else {
                                *(vec + hi) = a1;
                                *(vec + lo) = a3;
                                *(vec + lo + inc) = a2;
                            }
                        } else {
                            *(vec + lo) = a2;
                            *(vec + lo + inc) = a1;
                        }
                    } else {
                        if (a2 > a3) {
                            *(vec + hi) = a2;
                            if (a3 > a1) {
                                *(vec + lo + inc) = a3;
                            } else {
                                *(vec + lo + inc) = a1;
                                *(vec + lo) = a3;
                            }
                        } else {
                            *(vec + lo + inc) = a2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) < pivotItem);
                        do j -= inc; while (*(vec + j) > pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vec,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, descending, arbitrary element spacing.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortDescST( float* vec, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            float a1, a2, a3;
           
            float temp, pivotItem;
            if (checkNaNDesc(lo, ref hi, inc, vec)) return;

            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp >= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                        }
                        *(vec + i + inc) = pivotItem;
                    }
                    #endregion
                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 < a2) {
                        if (a2 < a3) {
                            *(vec + hi) = a1;
                            *(vec + lo) = a3;
                            *(vec + lo + inc) = a2;
                        } else {
                            if (a1 < a3) {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vec + hi) = a1;
                            } else {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a1;
                                //*(vec + hi) = a3; 
                            }
                        }
                    } else {
                        if (a2 < a3) {
                            if (a1 < a3) {
                                *(vec + lo) = a3;
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a1;
                            } else {
                                //*(vec + lo) = a1; 
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a3;
                            }
                        } else {
                            //*(vec + lo) = a1; 
                            //*(vec + hi) = a3; 
                            *(vec + lo + inc) = a2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) > pivotItem);
                        do j -= inc; while (*(vec + j) < pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4D(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, ascending, arbitrary element spacing, indices aware.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="vecIdx">indices vector, content will in-place be sorted along with <paramref name="vec"/></param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MminimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortAscIDXST( float* vec,  long* vecIdx, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            float a1, a2, a3;
           
            long ai1, ai2, ai3;
           
            float temp, pivotItem;
            if (checkNaNIDXAsc(lo, ref hi, inc, vecIdx, vec)) return;
            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        ai1 = *(vecIdx + j);
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp <= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                            *(vecIdx + i + inc) = *(vecIdx + i);
                        }
                        *(vec + i + inc) = pivotItem;
                        *(vecIdx + i + inc) = ai1;
                    }
                    #endregion
                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    ai1 = *(vecIdx + lo);
                    ai2 = *(vecIdx + pivotIndex);
                    ai3 = *(vecIdx + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    *(vecIdx + pivotIndex) = *(vecIdx + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 > a2) {
                        if (a1 > a3) {
                            if (a3 > a2) {
                                *(vec + hi) = a1;
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vecIdx + hi) = ai1;
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                            } else {
                                *(vec + hi) = a1;
                                *(vec + lo) = a3;
                                *(vec + lo + inc) = a2;
                                *(vecIdx + hi) = ai1;
                                *(vecIdx + lo) = ai3;
                                *(vecIdx + lo + inc) = ai2;
                            }
                        } else {
                            *(vec + lo) = a2;
                            *(vec + lo + inc) = a1;
                            *(vecIdx + lo) = ai2;
                            *(vecIdx + lo + inc) = ai1;
                        }
                    } else {
                        if (a2 > a3) {
                            *(vec + hi) = a2;
                            *(vecIdx + hi) = ai2;
                            if (a3 > a1) {
                                *(vec + lo + inc) = a3;
                                *(vecIdx + lo + inc) = ai3;
                            } else {
                                *(vec + lo + inc) = a1;
                                *(vec + lo) = a3;
                                *(vecIdx + lo + inc) = ai1;
                                *(vecIdx + lo) = ai3;
                            }
                        } else {
                            *(vec + lo + inc) = a2;
                            *(vecIdx + lo + inc) = ai2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    ai1 = *(vecIdx + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) < pivotItem);
                        do j -= inc; while (*(vec + j) > pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        ai2 = *(vecIdx + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                        *(vecIdx + i) = *(vecIdx + j);
                        *(vecIdx + j) = ai2;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
                    *(vecIdx + lo + inc) = *(vecIdx + j);
                    *(vecIdx + j) = ai1;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, descending, arbitrary element spacing, indices aware.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="vecIdx">indices vector, content will in-place be sorted along with <paramref name="vec"/></param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortDescIDXST( float* vec,  long* vecIdx, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            float a1, a2, a3;
           
            long ai1, ai2, ai3;
           
            float temp, pivotItem;
            if (checkNaNIDXDesc(lo, ref hi, inc, vecIdx, vec)) return;
            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        ai1 = *(vecIdx + j);
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp >= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                            *(vecIdx + i + inc) = *(vecIdx + i);
                        }
                        *(vec + i + inc) = pivotItem;
                        *(vecIdx + i + inc) = ai1;
                    }
                    #endregion

                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    ai1 = *(vecIdx + lo);
                    ai2 = *(vecIdx + pivotIndex);
                    ai3 = *(vecIdx + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    *(vecIdx + pivotIndex) = *(vecIdx + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 < a2) {
                        if (a2 < a3) {
                            *(vec + hi) = a1;
                            *(vec + lo) = a3;
                            *(vec + lo + inc) = a2;
                            *(vecIdx + hi) = ai1;
                            *(vecIdx + lo) = ai3;
                            *(vecIdx + lo + inc) = ai2;
                        } else {
                            if (a1 < a3) {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vec + hi) = a1;
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                                *(vecIdx + hi) = ai1;
                            } else {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a1;
                                //*(vec + hi) = a3; 
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai1;
                                //*(vecIdx + hi) = ai3; 
                            }
                        }
                    } else {
                        if (a2 < a3) {
                            if (a1 < a3) {
                                *(vec + lo) = a3;
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a1;
                                *(vecIdx + lo) = ai3;
                                *(vecIdx + hi) = ai2;
                                *(vecIdx + lo + inc) = ai1;
                            } else {
                                //*(vec + lo) = a1; 
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a3;
                                //*(vecIdx + lo) = ai1; 
                                *(vecIdx + hi) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                            }
                        } else {
                            //*(vec + lo) = a1; 
                            //*(vec + hi) = a3; 
                            *(vec + lo + inc) = a2;
                            //*(vecIdx + lo) = ai1; 
                            //*(vecIdx + hi) = ai3; 
                            *(vecIdx + lo + inc) = ai2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    ai1 = *(vecIdx + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) > pivotItem);
                        do j -= inc; while (*(vec + j) < pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        ai2 = *(vecIdx + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                        *(vecIdx + i) = *(vecIdx + j);
                        *(vecIdx + j) = ai2;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
                    *(vecIdx + lo + inc) = *(vecIdx + j);
                    *(vecIdx + j) = ai1;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }


        /// <summary>
        /// One dimensional quick sort, inline, ascending, arbitrary element spacing
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public static unsafe void QuickSortAscST( fcomplex* vec, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0; 

           
            fcomplex a1, a2, a3;
           
            fcomplex temp, pivotItem;
            if (checkNaNAsc(lo, ref hi, inc, vec)) return;

            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp <= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                        }
                        *(vec + i + inc) = pivotItem;
                    }
                    #endregion

                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {

                    // do the quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 > a2) {
                        if (a1 > a3) {
                            if (a3 > a2) {
                                *(vec + hi) = a1;
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                            } else {
                                *(vec + hi) = a1;
                                *(vec + lo) = a3;
                                *(vec + lo + inc) = a2;
                            }
                        } else {
                            *(vec + lo) = a2;
                            *(vec + lo + inc) = a1;
                        }
                    } else {
                        if (a2 > a3) {
                            *(vec + hi) = a2;
                            if (a3 > a1) {
                                *(vec + lo + inc) = a3;
                            } else {
                                *(vec + lo + inc) = a1;
                                *(vec + lo) = a3;
                            }
                        } else {
                            *(vec + lo + inc) = a2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) < pivotItem);
                        do j -= inc; while (*(vec + j) > pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vec,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, descending, arbitrary element spacing.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortDescST( fcomplex* vec, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            fcomplex a1, a2, a3;
           
            fcomplex temp, pivotItem;
            if (checkNaNDesc(lo, ref hi, inc, vec)) return;

            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp >= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                        }
                        *(vec + i + inc) = pivotItem;
                    }
                    #endregion
                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 < a2) {
                        if (a2 < a3) {
                            *(vec + hi) = a1;
                            *(vec + lo) = a3;
                            *(vec + lo + inc) = a2;
                        } else {
                            if (a1 < a3) {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vec + hi) = a1;
                            } else {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a1;
                                //*(vec + hi) = a3; 
                            }
                        }
                    } else {
                        if (a2 < a3) {
                            if (a1 < a3) {
                                *(vec + lo) = a3;
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a1;
                            } else {
                                //*(vec + lo) = a1; 
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a3;
                            }
                        } else {
                            //*(vec + lo) = a1; 
                            //*(vec + hi) = a3; 
                            *(vec + lo + inc) = a2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) > pivotItem);
                        do j -= inc; while (*(vec + j) < pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4D(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, ascending, arbitrary element spacing, indices aware.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="vecIdx">indices vector, content will in-place be sorted along with <paramref name="vec"/></param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MminimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortAscIDXST( fcomplex* vec,  long* vecIdx, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            fcomplex a1, a2, a3;
           
            long ai1, ai2, ai3;
           
            fcomplex temp, pivotItem;
            if (checkNaNIDXAsc(lo, ref hi, inc, vecIdx, vec)) return;
            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        ai1 = *(vecIdx + j);
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp <= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                            *(vecIdx + i + inc) = *(vecIdx + i);
                        }
                        *(vec + i + inc) = pivotItem;
                        *(vecIdx + i + inc) = ai1;
                    }
                    #endregion
                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    ai1 = *(vecIdx + lo);
                    ai2 = *(vecIdx + pivotIndex);
                    ai3 = *(vecIdx + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    *(vecIdx + pivotIndex) = *(vecIdx + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 > a2) {
                        if (a1 > a3) {
                            if (a3 > a2) {
                                *(vec + hi) = a1;
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vecIdx + hi) = ai1;
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                            } else {
                                *(vec + hi) = a1;
                                *(vec + lo) = a3;
                                *(vec + lo + inc) = a2;
                                *(vecIdx + hi) = ai1;
                                *(vecIdx + lo) = ai3;
                                *(vecIdx + lo + inc) = ai2;
                            }
                        } else {
                            *(vec + lo) = a2;
                            *(vec + lo + inc) = a1;
                            *(vecIdx + lo) = ai2;
                            *(vecIdx + lo + inc) = ai1;
                        }
                    } else {
                        if (a2 > a3) {
                            *(vec + hi) = a2;
                            *(vecIdx + hi) = ai2;
                            if (a3 > a1) {
                                *(vec + lo + inc) = a3;
                                *(vecIdx + lo + inc) = ai3;
                            } else {
                                *(vec + lo + inc) = a1;
                                *(vec + lo) = a3;
                                *(vecIdx + lo + inc) = ai1;
                                *(vecIdx + lo) = ai3;
                            }
                        } else {
                            *(vec + lo + inc) = a2;
                            *(vecIdx + lo + inc) = ai2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    ai1 = *(vecIdx + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) < pivotItem);
                        do j -= inc; while (*(vec + j) > pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        ai2 = *(vecIdx + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                        *(vecIdx + i) = *(vecIdx + j);
                        *(vecIdx + j) = ai2;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
                    *(vecIdx + lo + inc) = *(vecIdx + j);
                    *(vecIdx + j) = ai1;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, descending, arbitrary element spacing, indices aware.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="vecIdx">indices vector, content will in-place be sorted along with <paramref name="vec"/></param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortDescIDXST( fcomplex* vec,  long* vecIdx, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            fcomplex a1, a2, a3;
           
            long ai1, ai2, ai3;
           
            fcomplex temp, pivotItem;
            if (checkNaNIDXDesc(lo, ref hi, inc, vecIdx, vec)) return;
            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        ai1 = *(vecIdx + j);
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp >= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                            *(vecIdx + i + inc) = *(vecIdx + i);
                        }
                        *(vec + i + inc) = pivotItem;
                        *(vecIdx + i + inc) = ai1;
                    }
                    #endregion

                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    ai1 = *(vecIdx + lo);
                    ai2 = *(vecIdx + pivotIndex);
                    ai3 = *(vecIdx + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    *(vecIdx + pivotIndex) = *(vecIdx + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 < a2) {
                        if (a2 < a3) {
                            *(vec + hi) = a1;
                            *(vec + lo) = a3;
                            *(vec + lo + inc) = a2;
                            *(vecIdx + hi) = ai1;
                            *(vecIdx + lo) = ai3;
                            *(vecIdx + lo + inc) = ai2;
                        } else {
                            if (a1 < a3) {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vec + hi) = a1;
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                                *(vecIdx + hi) = ai1;
                            } else {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a1;
                                //*(vec + hi) = a3; 
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai1;
                                //*(vecIdx + hi) = ai3; 
                            }
                        }
                    } else {
                        if (a2 < a3) {
                            if (a1 < a3) {
                                *(vec + lo) = a3;
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a1;
                                *(vecIdx + lo) = ai3;
                                *(vecIdx + hi) = ai2;
                                *(vecIdx + lo + inc) = ai1;
                            } else {
                                //*(vec + lo) = a1; 
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a3;
                                //*(vecIdx + lo) = ai1; 
                                *(vecIdx + hi) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                            }
                        } else {
                            //*(vec + lo) = a1; 
                            //*(vec + hi) = a3; 
                            *(vec + lo + inc) = a2;
                            //*(vecIdx + lo) = ai1; 
                            //*(vecIdx + hi) = ai3; 
                            *(vecIdx + lo + inc) = ai2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    ai1 = *(vecIdx + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) > pivotItem);
                        do j -= inc; while (*(vec + j) < pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        ai2 = *(vecIdx + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                        *(vecIdx + i) = *(vecIdx + j);
                        *(vecIdx + j) = ai2;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
                    *(vecIdx + lo + inc) = *(vecIdx + j);
                    *(vecIdx + j) = ai1;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }


        /// <summary>
        /// One dimensional quick sort, inline, ascending, arbitrary element spacing
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public static unsafe void QuickSortAscST( complex* vec, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0; 

           
            complex a1, a2, a3;
           
            complex temp, pivotItem;
            if (checkNaNAsc(lo, ref hi, inc, vec)) return;

            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp <= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                        }
                        *(vec + i + inc) = pivotItem;
                    }
                    #endregion

                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {

                    // do the quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 > a2) {
                        if (a1 > a3) {
                            if (a3 > a2) {
                                *(vec + hi) = a1;
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                            } else {
                                *(vec + hi) = a1;
                                *(vec + lo) = a3;
                                *(vec + lo + inc) = a2;
                            }
                        } else {
                            *(vec + lo) = a2;
                            *(vec + lo + inc) = a1;
                        }
                    } else {
                        if (a2 > a3) {
                            *(vec + hi) = a2;
                            if (a3 > a1) {
                                *(vec + lo + inc) = a3;
                            } else {
                                *(vec + lo + inc) = a1;
                                *(vec + lo) = a3;
                            }
                        } else {
                            *(vec + lo + inc) = a2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) < pivotItem);
                        do j -= inc; while (*(vec + j) > pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vec,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, descending, arbitrary element spacing.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortDescST( complex* vec, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            complex a1, a2, a3;
           
            complex temp, pivotItem;
            if (checkNaNDesc(lo, ref hi, inc, vec)) return;

            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp >= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                        }
                        *(vec + i + inc) = pivotItem;
                    }
                    #endregion
                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 < a2) {
                        if (a2 < a3) {
                            *(vec + hi) = a1;
                            *(vec + lo) = a3;
                            *(vec + lo + inc) = a2;
                        } else {
                            if (a1 < a3) {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vec + hi) = a1;
                            } else {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a1;
                                //*(vec + hi) = a3; 
                            }
                        }
                    } else {
                        if (a2 < a3) {
                            if (a1 < a3) {
                                *(vec + lo) = a3;
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a1;
                            } else {
                                //*(vec + lo) = a1; 
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a3;
                            }
                        } else {
                            //*(vec + lo) = a1; 
                            //*(vec + hi) = a3; 
                            *(vec + lo + inc) = a2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) > pivotItem);
                        do j -= inc; while (*(vec + j) < pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4D(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, ascending, arbitrary element spacing, indices aware.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="vecIdx">indices vector, content will in-place be sorted along with <paramref name="vec"/></param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MminimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortAscIDXST( complex* vec,  long* vecIdx, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            complex a1, a2, a3;
           
            long ai1, ai2, ai3;
           
            complex temp, pivotItem;
            if (checkNaNIDXAsc(lo, ref hi, inc, vecIdx, vec)) return;
            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        ai1 = *(vecIdx + j);
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp <= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                            *(vecIdx + i + inc) = *(vecIdx + i);
                        }
                        *(vec + i + inc) = pivotItem;
                        *(vecIdx + i + inc) = ai1;
                    }
                    #endregion
                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    ai1 = *(vecIdx + lo);
                    ai2 = *(vecIdx + pivotIndex);
                    ai3 = *(vecIdx + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    *(vecIdx + pivotIndex) = *(vecIdx + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 > a2) {
                        if (a1 > a3) {
                            if (a3 > a2) {
                                *(vec + hi) = a1;
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vecIdx + hi) = ai1;
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                            } else {
                                *(vec + hi) = a1;
                                *(vec + lo) = a3;
                                *(vec + lo + inc) = a2;
                                *(vecIdx + hi) = ai1;
                                *(vecIdx + lo) = ai3;
                                *(vecIdx + lo + inc) = ai2;
                            }
                        } else {
                            *(vec + lo) = a2;
                            *(vec + lo + inc) = a1;
                            *(vecIdx + lo) = ai2;
                            *(vecIdx + lo + inc) = ai1;
                        }
                    } else {
                        if (a2 > a3) {
                            *(vec + hi) = a2;
                            *(vecIdx + hi) = ai2;
                            if (a3 > a1) {
                                *(vec + lo + inc) = a3;
                                *(vecIdx + lo + inc) = ai3;
                            } else {
                                *(vec + lo + inc) = a1;
                                *(vec + lo) = a3;
                                *(vecIdx + lo + inc) = ai1;
                                *(vecIdx + lo) = ai3;
                            }
                        } else {
                            *(vec + lo + inc) = a2;
                            *(vecIdx + lo + inc) = ai2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    ai1 = *(vecIdx + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) < pivotItem);
                        do j -= inc; while (*(vec + j) > pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        ai2 = *(vecIdx + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                        *(vecIdx + i) = *(vecIdx + j);
                        *(vecIdx + j) = ai2;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
                    *(vecIdx + lo + inc) = *(vecIdx + j);
                    *(vecIdx + j) = ai1;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, descending, arbitrary element spacing, indices aware.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="vecIdx">indices vector, content will in-place be sorted along with <paramref name="vec"/></param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortDescIDXST( complex* vec,  long* vecIdx, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            complex a1, a2, a3;
           
            long ai1, ai2, ai3;
           
            complex temp, pivotItem;
            if (checkNaNIDXDesc(lo, ref hi, inc, vecIdx, vec)) return;
            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        ai1 = *(vecIdx + j);
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp >= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                            *(vecIdx + i + inc) = *(vecIdx + i);
                        }
                        *(vec + i + inc) = pivotItem;
                        *(vecIdx + i + inc) = ai1;
                    }
                    #endregion

                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    ai1 = *(vecIdx + lo);
                    ai2 = *(vecIdx + pivotIndex);
                    ai3 = *(vecIdx + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    *(vecIdx + pivotIndex) = *(vecIdx + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 < a2) {
                        if (a2 < a3) {
                            *(vec + hi) = a1;
                            *(vec + lo) = a3;
                            *(vec + lo + inc) = a2;
                            *(vecIdx + hi) = ai1;
                            *(vecIdx + lo) = ai3;
                            *(vecIdx + lo + inc) = ai2;
                        } else {
                            if (a1 < a3) {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vec + hi) = a1;
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                                *(vecIdx + hi) = ai1;
                            } else {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a1;
                                //*(vec + hi) = a3; 
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai1;
                                //*(vecIdx + hi) = ai3; 
                            }
                        }
                    } else {
                        if (a2 < a3) {
                            if (a1 < a3) {
                                *(vec + lo) = a3;
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a1;
                                *(vecIdx + lo) = ai3;
                                *(vecIdx + hi) = ai2;
                                *(vecIdx + lo + inc) = ai1;
                            } else {
                                //*(vec + lo) = a1; 
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a3;
                                //*(vecIdx + lo) = ai1; 
                                *(vecIdx + hi) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                            }
                        } else {
                            //*(vec + lo) = a1; 
                            //*(vec + hi) = a3; 
                            *(vec + lo + inc) = a2;
                            //*(vecIdx + lo) = ai1; 
                            //*(vecIdx + hi) = ai3; 
                            *(vecIdx + lo + inc) = ai2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    ai1 = *(vecIdx + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) > pivotItem);
                        do j -= inc; while (*(vec + j) < pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        ai2 = *(vecIdx + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                        *(vecIdx + i) = *(vecIdx + j);
                        *(vecIdx + j) = ai2;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
                    *(vecIdx + lo + inc) = *(vecIdx + j);
                    *(vecIdx + j) = ai1;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }


        /// <summary>
        /// One dimensional quick sort, inline, ascending, arbitrary element spacing
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public static unsafe void QuickSortAscST( char* vec, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0; 

           
            char a1, a2, a3;
           
            char temp, pivotItem;
            

            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp <= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                        }
                        *(vec + i + inc) = pivotItem;
                    }
                    #endregion

                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {

                    // do the quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 > a2) {
                        if (a1 > a3) {
                            if (a3 > a2) {
                                *(vec + hi) = a1;
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                            } else {
                                *(vec + hi) = a1;
                                *(vec + lo) = a3;
                                *(vec + lo + inc) = a2;
                            }
                        } else {
                            *(vec + lo) = a2;
                            *(vec + lo + inc) = a1;
                        }
                    } else {
                        if (a2 > a3) {
                            *(vec + hi) = a2;
                            if (a3 > a1) {
                                *(vec + lo + inc) = a3;
                            } else {
                                *(vec + lo + inc) = a1;
                                *(vec + lo) = a3;
                            }
                        } else {
                            *(vec + lo + inc) = a2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) < pivotItem);
                        do j -= inc; while (*(vec + j) > pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vec,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, descending, arbitrary element spacing.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortDescST( char* vec, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            char a1, a2, a3;
           
            char temp, pivotItem;
            

            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp >= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                        }
                        *(vec + i + inc) = pivotItem;
                    }
                    #endregion
                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 < a2) {
                        if (a2 < a3) {
                            *(vec + hi) = a1;
                            *(vec + lo) = a3;
                            *(vec + lo + inc) = a2;
                        } else {
                            if (a1 < a3) {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vec + hi) = a1;
                            } else {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a1;
                                //*(vec + hi) = a3; 
                            }
                        }
                    } else {
                        if (a2 < a3) {
                            if (a1 < a3) {
                                *(vec + lo) = a3;
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a1;
                            } else {
                                //*(vec + lo) = a1; 
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a3;
                            }
                        } else {
                            //*(vec + lo) = a1; 
                            //*(vec + hi) = a3; 
                            *(vec + lo + inc) = a2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) > pivotItem);
                        do j -= inc; while (*(vec + j) < pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4D(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, ascending, arbitrary element spacing, indices aware.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="vecIdx">indices vector, content will in-place be sorted along with <paramref name="vec"/></param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MminimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortAscIDXST( char* vec,  long* vecIdx, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            char a1, a2, a3;
           
            long ai1, ai2, ai3;
           
            char temp, pivotItem;
            
            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        ai1 = *(vecIdx + j);
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp <= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                            *(vecIdx + i + inc) = *(vecIdx + i);
                        }
                        *(vec + i + inc) = pivotItem;
                        *(vecIdx + i + inc) = ai1;
                    }
                    #endregion
                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    ai1 = *(vecIdx + lo);
                    ai2 = *(vecIdx + pivotIndex);
                    ai3 = *(vecIdx + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    *(vecIdx + pivotIndex) = *(vecIdx + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 > a2) {
                        if (a1 > a3) {
                            if (a3 > a2) {
                                *(vec + hi) = a1;
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vecIdx + hi) = ai1;
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                            } else {
                                *(vec + hi) = a1;
                                *(vec + lo) = a3;
                                *(vec + lo + inc) = a2;
                                *(vecIdx + hi) = ai1;
                                *(vecIdx + lo) = ai3;
                                *(vecIdx + lo + inc) = ai2;
                            }
                        } else {
                            *(vec + lo) = a2;
                            *(vec + lo + inc) = a1;
                            *(vecIdx + lo) = ai2;
                            *(vecIdx + lo + inc) = ai1;
                        }
                    } else {
                        if (a2 > a3) {
                            *(vec + hi) = a2;
                            *(vecIdx + hi) = ai2;
                            if (a3 > a1) {
                                *(vec + lo + inc) = a3;
                                *(vecIdx + lo + inc) = ai3;
                            } else {
                                *(vec + lo + inc) = a1;
                                *(vec + lo) = a3;
                                *(vecIdx + lo + inc) = ai1;
                                *(vecIdx + lo) = ai3;
                            }
                        } else {
                            *(vec + lo + inc) = a2;
                            *(vecIdx + lo + inc) = ai2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    ai1 = *(vecIdx + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) < pivotItem);
                        do j -= inc; while (*(vec + j) > pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        ai2 = *(vecIdx + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                        *(vecIdx + i) = *(vecIdx + j);
                        *(vecIdx + j) = ai2;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
                    *(vecIdx + lo + inc) = *(vecIdx + j);
                    *(vecIdx + j) = ai1;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, descending, arbitrary element spacing, indices aware.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="vecIdx">indices vector, content will in-place be sorted along with <paramref name="vec"/></param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortDescIDXST( char* vec,  long* vecIdx, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            char a1, a2, a3;
           
            long ai1, ai2, ai3;
           
            char temp, pivotItem;
            
            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        ai1 = *(vecIdx + j);
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp >= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                            *(vecIdx + i + inc) = *(vecIdx + i);
                        }
                        *(vec + i + inc) = pivotItem;
                        *(vecIdx + i + inc) = ai1;
                    }
                    #endregion

                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    ai1 = *(vecIdx + lo);
                    ai2 = *(vecIdx + pivotIndex);
                    ai3 = *(vecIdx + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    *(vecIdx + pivotIndex) = *(vecIdx + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 < a2) {
                        if (a2 < a3) {
                            *(vec + hi) = a1;
                            *(vec + lo) = a3;
                            *(vec + lo + inc) = a2;
                            *(vecIdx + hi) = ai1;
                            *(vecIdx + lo) = ai3;
                            *(vecIdx + lo + inc) = ai2;
                        } else {
                            if (a1 < a3) {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vec + hi) = a1;
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                                *(vecIdx + hi) = ai1;
                            } else {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a1;
                                //*(vec + hi) = a3; 
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai1;
                                //*(vecIdx + hi) = ai3; 
                            }
                        }
                    } else {
                        if (a2 < a3) {
                            if (a1 < a3) {
                                *(vec + lo) = a3;
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a1;
                                *(vecIdx + lo) = ai3;
                                *(vecIdx + hi) = ai2;
                                *(vecIdx + lo + inc) = ai1;
                            } else {
                                //*(vec + lo) = a1; 
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a3;
                                //*(vecIdx + lo) = ai1; 
                                *(vecIdx + hi) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                            }
                        } else {
                            //*(vec + lo) = a1; 
                            //*(vec + hi) = a3; 
                            *(vec + lo + inc) = a2;
                            //*(vecIdx + lo) = ai1; 
                            //*(vecIdx + hi) = ai3; 
                            *(vecIdx + lo + inc) = ai2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    ai1 = *(vecIdx + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) > pivotItem);
                        do j -= inc; while (*(vec + j) < pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        ai2 = *(vecIdx + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                        *(vecIdx + i) = *(vecIdx + j);
                        *(vecIdx + j) = ai2;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
                    *(vecIdx + lo + inc) = *(vecIdx + j);
                    *(vecIdx + j) = ai1;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }


        /// <summary>
        /// One dimensional quick sort, inline, ascending, arbitrary element spacing
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public static unsafe void QuickSortAscST( byte* vec, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0; 

           
            byte a1, a2, a3;
           
            byte temp, pivotItem;
            

            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp <= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                        }
                        *(vec + i + inc) = pivotItem;
                    }
                    #endregion

                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {

                    // do the quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 > a2) {
                        if (a1 > a3) {
                            if (a3 > a2) {
                                *(vec + hi) = a1;
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                            } else {
                                *(vec + hi) = a1;
                                *(vec + lo) = a3;
                                *(vec + lo + inc) = a2;
                            }
                        } else {
                            *(vec + lo) = a2;
                            *(vec + lo + inc) = a1;
                        }
                    } else {
                        if (a2 > a3) {
                            *(vec + hi) = a2;
                            if (a3 > a1) {
                                *(vec + lo + inc) = a3;
                            } else {
                                *(vec + lo + inc) = a1;
                                *(vec + lo) = a3;
                            }
                        } else {
                            *(vec + lo + inc) = a2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) < pivotItem);
                        do j -= inc; while (*(vec + j) > pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vec,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, descending, arbitrary element spacing.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortDescST( byte* vec, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            byte a1, a2, a3;
           
            byte temp, pivotItem;
            

            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp >= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                        }
                        *(vec + i + inc) = pivotItem;
                    }
                    #endregion
                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 < a2) {
                        if (a2 < a3) {
                            *(vec + hi) = a1;
                            *(vec + lo) = a3;
                            *(vec + lo + inc) = a2;
                        } else {
                            if (a1 < a3) {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vec + hi) = a1;
                            } else {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a1;
                                //*(vec + hi) = a3; 
                            }
                        }
                    } else {
                        if (a2 < a3) {
                            if (a1 < a3) {
                                *(vec + lo) = a3;
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a1;
                            } else {
                                //*(vec + lo) = a1; 
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a3;
                            }
                        } else {
                            //*(vec + lo) = a1; 
                            //*(vec + hi) = a3; 
                            *(vec + lo + inc) = a2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) > pivotItem);
                        do j -= inc; while (*(vec + j) < pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4D(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, ascending, arbitrary element spacing, indices aware.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="vecIdx">indices vector, content will in-place be sorted along with <paramref name="vec"/></param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MminimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortAscIDXST( byte* vec,  long* vecIdx, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            byte a1, a2, a3;
           
            long ai1, ai2, ai3;
           
            byte temp, pivotItem;
            
            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        ai1 = *(vecIdx + j);
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp <= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                            *(vecIdx + i + inc) = *(vecIdx + i);
                        }
                        *(vec + i + inc) = pivotItem;
                        *(vecIdx + i + inc) = ai1;
                    }
                    #endregion
                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    ai1 = *(vecIdx + lo);
                    ai2 = *(vecIdx + pivotIndex);
                    ai3 = *(vecIdx + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    *(vecIdx + pivotIndex) = *(vecIdx + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 > a2) {
                        if (a1 > a3) {
                            if (a3 > a2) {
                                *(vec + hi) = a1;
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vecIdx + hi) = ai1;
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                            } else {
                                *(vec + hi) = a1;
                                *(vec + lo) = a3;
                                *(vec + lo + inc) = a2;
                                *(vecIdx + hi) = ai1;
                                *(vecIdx + lo) = ai3;
                                *(vecIdx + lo + inc) = ai2;
                            }
                        } else {
                            *(vec + lo) = a2;
                            *(vec + lo + inc) = a1;
                            *(vecIdx + lo) = ai2;
                            *(vecIdx + lo + inc) = ai1;
                        }
                    } else {
                        if (a2 > a3) {
                            *(vec + hi) = a2;
                            *(vecIdx + hi) = ai2;
                            if (a3 > a1) {
                                *(vec + lo + inc) = a3;
                                *(vecIdx + lo + inc) = ai3;
                            } else {
                                *(vec + lo + inc) = a1;
                                *(vec + lo) = a3;
                                *(vecIdx + lo + inc) = ai1;
                                *(vecIdx + lo) = ai3;
                            }
                        } else {
                            *(vec + lo + inc) = a2;
                            *(vecIdx + lo + inc) = ai2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    ai1 = *(vecIdx + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) < pivotItem);
                        do j -= inc; while (*(vec + j) > pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        ai2 = *(vecIdx + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                        *(vecIdx + i) = *(vecIdx + j);
                        *(vecIdx + j) = ai2;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
                    *(vecIdx + lo + inc) = *(vecIdx + j);
                    *(vecIdx + j) = ai1;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, descending, arbitrary element spacing, indices aware.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="vecIdx">indices vector, content will in-place be sorted along with <paramref name="vec"/></param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortDescIDXST( byte* vec,  long* vecIdx, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            byte a1, a2, a3;
           
            long ai1, ai2, ai3;
           
            byte temp, pivotItem;
            
            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        ai1 = *(vecIdx + j);
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp >= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                            *(vecIdx + i + inc) = *(vecIdx + i);
                        }
                        *(vec + i + inc) = pivotItem;
                        *(vecIdx + i + inc) = ai1;
                    }
                    #endregion

                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    ai1 = *(vecIdx + lo);
                    ai2 = *(vecIdx + pivotIndex);
                    ai3 = *(vecIdx + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    *(vecIdx + pivotIndex) = *(vecIdx + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 < a2) {
                        if (a2 < a3) {
                            *(vec + hi) = a1;
                            *(vec + lo) = a3;
                            *(vec + lo + inc) = a2;
                            *(vecIdx + hi) = ai1;
                            *(vecIdx + lo) = ai3;
                            *(vecIdx + lo + inc) = ai2;
                        } else {
                            if (a1 < a3) {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vec + hi) = a1;
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                                *(vecIdx + hi) = ai1;
                            } else {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a1;
                                //*(vec + hi) = a3; 
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai1;
                                //*(vecIdx + hi) = ai3; 
                            }
                        }
                    } else {
                        if (a2 < a3) {
                            if (a1 < a3) {
                                *(vec + lo) = a3;
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a1;
                                *(vecIdx + lo) = ai3;
                                *(vecIdx + hi) = ai2;
                                *(vecIdx + lo + inc) = ai1;
                            } else {
                                //*(vec + lo) = a1; 
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a3;
                                //*(vecIdx + lo) = ai1; 
                                *(vecIdx + hi) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                            }
                        } else {
                            //*(vec + lo) = a1; 
                            //*(vec + hi) = a3; 
                            *(vec + lo + inc) = a2;
                            //*(vecIdx + lo) = ai1; 
                            //*(vecIdx + hi) = ai3; 
                            *(vecIdx + lo + inc) = ai2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    ai1 = *(vecIdx + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) > pivotItem);
                        do j -= inc; while (*(vec + j) < pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        ai2 = *(vecIdx + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                        *(vecIdx + i) = *(vecIdx + j);
                        *(vecIdx + j) = ai2;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
                    *(vecIdx + lo + inc) = *(vecIdx + j);
                    *(vecIdx + j) = ai1;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }


        /// <summary>
        /// One dimensional quick sort, inline, ascending, arbitrary element spacing
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public static unsafe void QuickSortAscST( sbyte* vec, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0; 

           
            sbyte a1, a2, a3;
           
            sbyte temp, pivotItem;
            

            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp <= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                        }
                        *(vec + i + inc) = pivotItem;
                    }
                    #endregion

                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {

                    // do the quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 > a2) {
                        if (a1 > a3) {
                            if (a3 > a2) {
                                *(vec + hi) = a1;
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                            } else {
                                *(vec + hi) = a1;
                                *(vec + lo) = a3;
                                *(vec + lo + inc) = a2;
                            }
                        } else {
                            *(vec + lo) = a2;
                            *(vec + lo + inc) = a1;
                        }
                    } else {
                        if (a2 > a3) {
                            *(vec + hi) = a2;
                            if (a3 > a1) {
                                *(vec + lo + inc) = a3;
                            } else {
                                *(vec + lo + inc) = a1;
                                *(vec + lo) = a3;
                            }
                        } else {
                            *(vec + lo + inc) = a2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) < pivotItem);
                        do j -= inc; while (*(vec + j) > pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vec,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, descending, arbitrary element spacing.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortDescST( sbyte* vec, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            sbyte a1, a2, a3;
           
            sbyte temp, pivotItem;
            

            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp >= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                        }
                        *(vec + i + inc) = pivotItem;
                    }
                    #endregion
                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 < a2) {
                        if (a2 < a3) {
                            *(vec + hi) = a1;
                            *(vec + lo) = a3;
                            *(vec + lo + inc) = a2;
                        } else {
                            if (a1 < a3) {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vec + hi) = a1;
                            } else {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a1;
                                //*(vec + hi) = a3; 
                            }
                        }
                    } else {
                        if (a2 < a3) {
                            if (a1 < a3) {
                                *(vec + lo) = a3;
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a1;
                            } else {
                                //*(vec + lo) = a1; 
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a3;
                            }
                        } else {
                            //*(vec + lo) = a1; 
                            //*(vec + hi) = a3; 
                            *(vec + lo + inc) = a2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) > pivotItem);
                        do j -= inc; while (*(vec + j) < pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4D(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, ascending, arbitrary element spacing, indices aware.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="vecIdx">indices vector, content will in-place be sorted along with <paramref name="vec"/></param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MminimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortAscIDXST( sbyte* vec,  long* vecIdx, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            sbyte a1, a2, a3;
           
            long ai1, ai2, ai3;
           
            sbyte temp, pivotItem;
            
            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        ai1 = *(vecIdx + j);
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp <= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                            *(vecIdx + i + inc) = *(vecIdx + i);
                        }
                        *(vec + i + inc) = pivotItem;
                        *(vecIdx + i + inc) = ai1;
                    }
                    #endregion
                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    ai1 = *(vecIdx + lo);
                    ai2 = *(vecIdx + pivotIndex);
                    ai3 = *(vecIdx + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    *(vecIdx + pivotIndex) = *(vecIdx + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 > a2) {
                        if (a1 > a3) {
                            if (a3 > a2) {
                                *(vec + hi) = a1;
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vecIdx + hi) = ai1;
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                            } else {
                                *(vec + hi) = a1;
                                *(vec + lo) = a3;
                                *(vec + lo + inc) = a2;
                                *(vecIdx + hi) = ai1;
                                *(vecIdx + lo) = ai3;
                                *(vecIdx + lo + inc) = ai2;
                            }
                        } else {
                            *(vec + lo) = a2;
                            *(vec + lo + inc) = a1;
                            *(vecIdx + lo) = ai2;
                            *(vecIdx + lo + inc) = ai1;
                        }
                    } else {
                        if (a2 > a3) {
                            *(vec + hi) = a2;
                            *(vecIdx + hi) = ai2;
                            if (a3 > a1) {
                                *(vec + lo + inc) = a3;
                                *(vecIdx + lo + inc) = ai3;
                            } else {
                                *(vec + lo + inc) = a1;
                                *(vec + lo) = a3;
                                *(vecIdx + lo + inc) = ai1;
                                *(vecIdx + lo) = ai3;
                            }
                        } else {
                            *(vec + lo + inc) = a2;
                            *(vecIdx + lo + inc) = ai2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    ai1 = *(vecIdx + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) < pivotItem);
                        do j -= inc; while (*(vec + j) > pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        ai2 = *(vecIdx + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                        *(vecIdx + i) = *(vecIdx + j);
                        *(vecIdx + j) = ai2;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
                    *(vecIdx + lo + inc) = *(vecIdx + j);
                    *(vecIdx + j) = ai1;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }
        /// <summary>
        /// One dimensional quick sort, inline, descending, arbitrary element spacing, indices aware.
        /// </summary>
        /// <param name="vec">data array, 1dim, replaced on output with sorted values</param>
        /// <param name="lo">lowest index of sorting range</param>
        /// <param name="hi">highest index of sorting range</param>
        /// <param name="vecIdx">indices vector, content will in-place be sorted along with <paramref name="vec"/></param>
        /// <param name="inc">spacing between elements (dimension specifier)</param>
        /// <remarks><para>Quick sort algorithm is used for ranges of minimum lengths specified by 
        /// <see cref="P:ILNumerics.Settings.MinimumQuicksortLength"/> and above. 
        /// Below that length, a simple insertion sort is used instead.</para>
        /// <para>The algorithm implements a non-recursive quicksort variant. Therefore a small amount of 
        /// memory is needed to perform the sorting. That 'stack' memory is preserved from the ILMemoryPool. 
        /// The size is determined by the value of 
        /// <see cref="P:ILNumerics.Settings.MaxSafeQuicksortRecursionDepth"/>.</para>
        /// <para><b>Handling of NaN values (for double,float,complex or fcomplex arrays element datatypes only):</b> 
        /// if the input array contains any NaN values, those elements will be sorted to the end of the array on output.</para></remarks>
        public unsafe static void QuickSortDescIDXST( sbyte* vec,  long* vecIdx, long lo, long hi, long inc) {
            System.Diagnostics.Debug.Assert(vec != null);
            System.Diagnostics.Debug.Assert(lo <= hi);
            System.Diagnostics.Debug.Assert(lo >= 0);

            long pivotIndex, i, j;
            if (hi - lo < inc) return;
            var ctx = QuickSortThreadingContext.Local;
            ctx.QuickSortChunksCount = 0;

           
            sbyte a1, a2, a3;
           
            long ai1, ai2, ai3;
           
            sbyte temp, pivotItem;
            
            for (; ; ) {
                if (hi - lo < (Settings.MinimumQuicksortLength * inc)) {
                    #region small array: insertion sort
                    for (j = lo + inc; j <= hi; j += inc) {
                        pivotItem = (*(vec + j));
                        ai1 = *(vecIdx + j);
                        for (i = j - inc; i >= lo; i -= inc) {
                            temp = *(vec + i);
                            if (temp >= pivotItem)
                                break;
                            *(vec + i + inc) = temp;
                            *(vecIdx + i + inc) = *(vecIdx + i);
                        }
                        *(vec + i + inc) = pivotItem;
                        *(vecIdx + i + inc) = ai1;
                    }
                    #endregion

                    // load next chunk from stack 
                    if (!ctx.PopQuickSortChunk(out lo, out hi)) {
                        break;
                    }
                } else {
                    // quicksort
                    pivotIndex = lo + (int)((hi - lo) / inc / 2) * inc;
                    a1 = *(vec + lo);
                    a2 = *(vec + pivotIndex);
                    a3 = *(vec + hi);
                    ai1 = *(vecIdx + lo);
                    ai2 = *(vecIdx + pivotIndex);
                    ai3 = *(vecIdx + hi);
                    *(vec + pivotIndex) = *(vec + lo + inc);
                    *(vecIdx + pivotIndex) = *(vecIdx + lo + inc);
                    #region bring lo, lo+inc, hi in order
                    // by explicitely stepping through all possible paths,
                    // we save some assignments and comparisons
                    if (a1 < a2) {
                        if (a2 < a3) {
                            *(vec + hi) = a1;
                            *(vec + lo) = a3;
                            *(vec + lo + inc) = a2;
                            *(vecIdx + hi) = ai1;
                            *(vecIdx + lo) = ai3;
                            *(vecIdx + lo + inc) = ai2;
                        } else {
                            if (a1 < a3) {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a3;
                                *(vec + hi) = a1;
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                                *(vecIdx + hi) = ai1;
                            } else {
                                *(vec + lo) = a2;
                                *(vec + lo + inc) = a1;
                                //*(vec + hi) = a3; 
                                *(vecIdx + lo) = ai2;
                                *(vecIdx + lo + inc) = ai1;
                                //*(vecIdx + hi) = ai3; 
                            }
                        }
                    } else {
                        if (a2 < a3) {
                            if (a1 < a3) {
                                *(vec + lo) = a3;
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a1;
                                *(vecIdx + lo) = ai3;
                                *(vecIdx + hi) = ai2;
                                *(vecIdx + lo + inc) = ai1;
                            } else {
                                //*(vec + lo) = a1; 
                                *(vec + hi) = a2;
                                *(vec + lo + inc) = a3;
                                //*(vecIdx + lo) = ai1; 
                                *(vecIdx + hi) = ai2;
                                *(vecIdx + lo + inc) = ai3;
                            }
                        } else {
                            //*(vec + lo) = a1; 
                            //*(vec + hi) = a3; 
                            *(vec + lo + inc) = a2;
                            //*(vecIdx + lo) = ai1; 
                            //*(vecIdx + hi) = ai3; 
                            *(vecIdx + lo + inc) = ai2;
                        }
                    }
                    #endregion
                    i = lo + inc; j = hi;
                    pivotItem = *(vec + lo + inc);
                    ai1 = *(vecIdx + lo + inc);
                    for (; ; ) {
                        do i += inc; while (*(vec + i) > pivotItem);
                        do j -= inc; while (*(vec + j) < pivotItem);
                        if (j < i) break;
                        temp = *(vec + i);
                        ai2 = *(vecIdx + i);
                        *(vec + i) = *(vec + j);
                        *(vec + j) = temp;
                        *(vecIdx + i) = *(vecIdx + j);
                        *(vecIdx + j) = ai2;
                    }
                    *(vec + lo + inc) = *(vec + j);
                    *(vec + j) = pivotItem;
                    *(vecIdx + lo + inc) = *(vecIdx + j);
                    *(vecIdx + j) = ai1;
#if DEBUG
                    //System.Diagnostics.Debug.Assert(Check4A(vecP,lo,hi,j,inc)); 
#endif 
                    if (hi - i + inc >= j - lo) {
                        ctx.PushQuickSortChunk(lo: i, hi: hi);
                        hi = j - inc;
                    } else {
                        ctx.PushQuickSortChunk(lo: lo, hi: j - inc);
                        lo = i;
                    }
                }
            }
        }


#endregion HYCALPER AUTO GENERATED CODE


        #region HYCALPER LOOPSTART checkNANs 
        /*!HC:TYPELIST:
        <hycalper>
            <type>
                <source locate="after" endmark=" .*(">
                    inArr1
                </source>
                <destination>complex</destination>
                <destination>fcomplex</destination>
                <destination>float</destination>
            </type>
         </hycalper>
         */

        /// <summary>
        /// sort nan elements to the end, test if the array is already sorted
        /// </summary>
        /// <returns>true, if already sorted</returns>
        unsafe private static bool checkNaNIDXAsc(long lo, ref long hi, long inc, long* vecIdx, /*!HC:inArr1*/double* vec) {
            long tempIdx;
            bool isSorted = true;
            long iLo = lo, iHi = lo;
            if (hi == lo) {
                // only 1 element
                if (/*!HC:inArr1*/double.IsNaN(vec[lo])) {
                    hi = lo - inc;
                }
                return true;
            }
            /*!HC:inArr1*/
            double oldVal = vec[iLo];
            while (iLo <= hi) {
                /*!HC:inArr1*/
                double loVal = *(vec + iLo);
                if (/*!HC:inArr1*/double.IsNaN(loVal)) {
                    iHi = Math.Max(iHi, iLo + inc);
                    // walk up, finding next non-NaN value
                    while (iHi <= hi && /*!HC:inArr1*/double.IsNaN(*(vec + iHi))) {
                        iHi += inc;
                    }
                    if (iHi > hi) {
                        // iLo is NaN
                        hi = iLo - inc;
                        break;
                    }
                    // swap 
                    *(vec + iLo) = *(vec + iHi);
                    *(vec + iHi) = /*!HC:inArr1*/double.NaN;
                    tempIdx = *(vecIdx + iLo);
                    *(vecIdx + iLo) = *(vecIdx + iHi);
                    *(vecIdx + iHi) = tempIdx;
                }
                if (isSorted && vec[iLo] < oldVal) {
                    isSorted = false;
                }
                oldVal = vec[iLo];
                iLo += inc;
            }
            return isSorted;
        }

        /// <summary>
        /// sort nan elements to the end, test if the array is already sorted
        /// </summary>
        /// <returns>true, if already sorted</returns>
        unsafe private static bool checkNaNIDXDesc(long lo, ref long hi, long inc, long* vecIdx, /*!HC:inArr1*/double* vec) {
            long tempIdx;
            bool isSorted = true;
            long iLo = lo, iHi = lo;
            if (hi == lo) {
                // only 1 element
                if (/*!HC:inArr1*/double.IsNaN(vec[lo])) {
                    hi = lo - inc;
                }
                return true;
            }
            /*!HC:inArr1*/
            double oldVal = vec[iLo];
            while (iLo <= hi) {
                /*!HC:inArr1*/
                double loVal = *(vec + iLo);
                if (/*!HC:inArr1*/double.IsNaN(loVal)) {
                    iHi = Math.Max(iHi, iLo + inc);
                    // walk up, finding next non-NaN value
                    while (iHi <= hi && /*!HC:inArr1*/double.IsNaN(*(vec + iHi))) {
                        iHi += inc;
                    }
                    if (iHi > hi) {
                        // iLo is NaN
                        hi = iLo - inc;
                        break;
                    }
                    // swap 
                    *(vec + iLo) = *(vec + iHi);
                    *(vec + iHi) = /*!HC:inArr1*/double.NaN;
                    tempIdx = *(vecIdx + iLo);
                    *(vecIdx + iLo) = *(vecIdx + iHi);
                    *(vecIdx + iHi) = tempIdx;
                }
                if (isSorted && vec[iLo] > oldVal) {
                    isSorted = false;
                }
                oldVal = vec[iLo];
                iLo += inc;
            }
            return isSorted;
        }

        /// <summary>
        /// sort nan elements to the end, test if the array is already sorted
        /// </summary>
        /// <returns>true, if already sorted</returns>
        unsafe internal static bool checkNaNAsc(long lo, ref long hi, long inc, /*!HC:inArr1*/double* vec) {
            bool isSorted = true;
            long iLo = lo, iHi = lo;
            if (hi == lo) {
                // only 1 element
                if (/*!HC:inArr1*/double.IsNaN(vec[lo])) {
                    hi = lo - inc;
                }
                return true;
            }
            /*!HC:inArr1*/
            double oldVal = vec[iLo];
            while (iLo <= hi) {
                /*!HC:inArr1*/
                double loVal = *(vec + iLo);
                if (/*!HC:inArr1*/double.IsNaN(loVal)) {
                    iHi = Math.Max(iHi, iLo + inc);
                    // walk up, finding next non-NaN value
                    while (iHi <= hi && /*!HC:inArr1*/double.IsNaN(*(vec + iHi))) {
                        iHi += inc;
                    }
                    if (iHi > hi) {
                        // iLo is NaN
                        hi = iLo - inc;
                        break;
                    }
                    // swap 
                    *(vec + iLo) = *(vec + iHi);
                    *(vec + iHi) = /*!HC:inArr1*/double.NaN;
                }
                if (isSorted && vec[iLo] < oldVal) {
                    isSorted = false;
                }
                oldVal = vec[iLo];
                iLo += inc;
            }
            return isSorted;
        }

        /// <summary>
        /// sort nan elements to the end, test if the array is already sorted
        /// </summary>
        /// <returns>true, if already sorted</returns>
        unsafe private static bool checkNaNDesc(long lo, ref long hi, long inc, /*!HC:inArr1*/double* vec) {
            bool isSorted = true;
            long iLo = lo, iHi = lo;
            if (hi == lo) {
                // only 1 element
                if (/*!HC:inArr1*/double.IsNaN(vec[lo])) {
                    hi = lo - inc;
                }
                return true;
            }
            /*!HC:inArr1*/
            double oldVal = vec[iLo];
            while (iLo <= hi) {
                /*!HC:inArr1*/
                double loVal = *(vec + iLo);
                if (/*!HC:inArr1*/double.IsNaN(loVal)) {
                    iHi = Math.Max(iHi, iLo + inc);
                    // walk up, finding next non-NaN value
                    while (iHi <= hi && /*!HC:inArr1*/double.IsNaN(*(vec + iHi))) {
                        iHi += inc;
                    }
                    if (iHi > hi) {
                        // iLo is NaN
                        hi = iLo - inc;
                        break;
                    }
                    // swap 
                    *(vec + iLo) = *(vec + iHi);
                    *(vec + iHi) = /*!HC:inArr1*/double.NaN;
                }
                if (isSorted && vec[iLo] > oldVal) {
                    isSorted = false;
                }
                oldVal = vec[iLo];
                iLo += inc;
            }
            return isSorted;
        }

        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        /// <summary>
        /// sort nan elements to the end, test if the array is already sorted
        /// </summary>
        /// <returns>true, if already sorted</returns>
        unsafe private static bool checkNaNIDXAsc(long lo, ref long hi, long inc, long* vecIdx, float* vec) {
            long tempIdx;
            bool isSorted = true;
            long iLo = lo, iHi = lo;
            if (hi == lo) {
                // only 1 element
                if (float.IsNaN(vec[lo])) {
                    hi = lo - inc;
                }
                return true;
            }
           
            float oldVal = vec[iLo];
            while (iLo <= hi) {
               
                float loVal = *(vec + iLo);
                if (float.IsNaN(loVal)) {
                    iHi = Math.Max(iHi, iLo + inc);
                    // walk up, finding next non-NaN value
                    while (iHi <= hi && float.IsNaN(*(vec + iHi))) {
                        iHi += inc;
                    }
                    if (iHi > hi) {
                        // iLo is NaN
                        hi = iLo - inc;
                        break;
                    }
                    // swap 
                    *(vec + iLo) = *(vec + iHi);
                    *(vec + iHi) = float.NaN;
                    tempIdx = *(vecIdx + iLo);
                    *(vecIdx + iLo) = *(vecIdx + iHi);
                    *(vecIdx + iHi) = tempIdx;
                }
                if (isSorted && vec[iLo] < oldVal) {
                    isSorted = false;
                }
                oldVal = vec[iLo];
                iLo += inc;
            }
            return isSorted;
        }

        /// <summary>
        /// sort nan elements to the end, test if the array is already sorted
        /// </summary>
        /// <returns>true, if already sorted</returns>
        unsafe private static bool checkNaNIDXDesc(long lo, ref long hi, long inc, long* vecIdx, float* vec) {
            long tempIdx;
            bool isSorted = true;
            long iLo = lo, iHi = lo;
            if (hi == lo) {
                // only 1 element
                if (float.IsNaN(vec[lo])) {
                    hi = lo - inc;
                }
                return true;
            }
           
            float oldVal = vec[iLo];
            while (iLo <= hi) {
               
                float loVal = *(vec + iLo);
                if (float.IsNaN(loVal)) {
                    iHi = Math.Max(iHi, iLo + inc);
                    // walk up, finding next non-NaN value
                    while (iHi <= hi && float.IsNaN(*(vec + iHi))) {
                        iHi += inc;
                    }
                    if (iHi > hi) {
                        // iLo is NaN
                        hi = iLo - inc;
                        break;
                    }
                    // swap 
                    *(vec + iLo) = *(vec + iHi);
                    *(vec + iHi) = float.NaN;
                    tempIdx = *(vecIdx + iLo);
                    *(vecIdx + iLo) = *(vecIdx + iHi);
                    *(vecIdx + iHi) = tempIdx;
                }
                if (isSorted && vec[iLo] > oldVal) {
                    isSorted = false;
                }
                oldVal = vec[iLo];
                iLo += inc;
            }
            return isSorted;
        }

        /// <summary>
        /// sort nan elements to the end, test if the array is already sorted
        /// </summary>
        /// <returns>true, if already sorted</returns>
        unsafe internal static bool checkNaNAsc(long lo, ref long hi, long inc, float* vec) {
            bool isSorted = true;
            long iLo = lo, iHi = lo;
            if (hi == lo) {
                // only 1 element
                if (float.IsNaN(vec[lo])) {
                    hi = lo - inc;
                }
                return true;
            }
           
            float oldVal = vec[iLo];
            while (iLo <= hi) {
               
                float loVal = *(vec + iLo);
                if (float.IsNaN(loVal)) {
                    iHi = Math.Max(iHi, iLo + inc);
                    // walk up, finding next non-NaN value
                    while (iHi <= hi && float.IsNaN(*(vec + iHi))) {
                        iHi += inc;
                    }
                    if (iHi > hi) {
                        // iLo is NaN
                        hi = iLo - inc;
                        break;
                    }
                    // swap 
                    *(vec + iLo) = *(vec + iHi);
                    *(vec + iHi) = float.NaN;
                }
                if (isSorted && vec[iLo] < oldVal) {
                    isSorted = false;
                }
                oldVal = vec[iLo];
                iLo += inc;
            }
            return isSorted;
        }

        /// <summary>
        /// sort nan elements to the end, test if the array is already sorted
        /// </summary>
        /// <returns>true, if already sorted</returns>
        unsafe private static bool checkNaNDesc(long lo, ref long hi, long inc, float* vec) {
            bool isSorted = true;
            long iLo = lo, iHi = lo;
            if (hi == lo) {
                // only 1 element
                if (float.IsNaN(vec[lo])) {
                    hi = lo - inc;
                }
                return true;
            }
           
            float oldVal = vec[iLo];
            while (iLo <= hi) {
               
                float loVal = *(vec + iLo);
                if (float.IsNaN(loVal)) {
                    iHi = Math.Max(iHi, iLo + inc);
                    // walk up, finding next non-NaN value
                    while (iHi <= hi && float.IsNaN(*(vec + iHi))) {
                        iHi += inc;
                    }
                    if (iHi > hi) {
                        // iLo is NaN
                        hi = iLo - inc;
                        break;
                    }
                    // swap 
                    *(vec + iLo) = *(vec + iHi);
                    *(vec + iHi) = float.NaN;
                }
                if (isSorted && vec[iLo] > oldVal) {
                    isSorted = false;
                }
                oldVal = vec[iLo];
                iLo += inc;
            }
            return isSorted;
        }

       

        /// <summary>
        /// sort nan elements to the end, test if the array is already sorted
        /// </summary>
        /// <returns>true, if already sorted</returns>
        unsafe private static bool checkNaNIDXAsc(long lo, ref long hi, long inc, long* vecIdx, fcomplex* vec) {
            long tempIdx;
            bool isSorted = true;
            long iLo = lo, iHi = lo;
            if (hi == lo) {
                // only 1 element
                if (fcomplex.IsNaN(vec[lo])) {
                    hi = lo - inc;
                }
                return true;
            }
           
            fcomplex oldVal = vec[iLo];
            while (iLo <= hi) {
               
                fcomplex loVal = *(vec + iLo);
                if (fcomplex.IsNaN(loVal)) {
                    iHi = Math.Max(iHi, iLo + inc);
                    // walk up, finding next non-NaN value
                    while (iHi <= hi && fcomplex.IsNaN(*(vec + iHi))) {
                        iHi += inc;
                    }
                    if (iHi > hi) {
                        // iLo is NaN
                        hi = iLo - inc;
                        break;
                    }
                    // swap 
                    *(vec + iLo) = *(vec + iHi);
                    *(vec + iHi) = fcomplex.NaN;
                    tempIdx = *(vecIdx + iLo);
                    *(vecIdx + iLo) = *(vecIdx + iHi);
                    *(vecIdx + iHi) = tempIdx;
                }
                if (isSorted && vec[iLo] < oldVal) {
                    isSorted = false;
                }
                oldVal = vec[iLo];
                iLo += inc;
            }
            return isSorted;
        }

        /// <summary>
        /// sort nan elements to the end, test if the array is already sorted
        /// </summary>
        /// <returns>true, if already sorted</returns>
        unsafe private static bool checkNaNIDXDesc(long lo, ref long hi, long inc, long* vecIdx, fcomplex* vec) {
            long tempIdx;
            bool isSorted = true;
            long iLo = lo, iHi = lo;
            if (hi == lo) {
                // only 1 element
                if (fcomplex.IsNaN(vec[lo])) {
                    hi = lo - inc;
                }
                return true;
            }
           
            fcomplex oldVal = vec[iLo];
            while (iLo <= hi) {
               
                fcomplex loVal = *(vec + iLo);
                if (fcomplex.IsNaN(loVal)) {
                    iHi = Math.Max(iHi, iLo + inc);
                    // walk up, finding next non-NaN value
                    while (iHi <= hi && fcomplex.IsNaN(*(vec + iHi))) {
                        iHi += inc;
                    }
                    if (iHi > hi) {
                        // iLo is NaN
                        hi = iLo - inc;
                        break;
                    }
                    // swap 
                    *(vec + iLo) = *(vec + iHi);
                    *(vec + iHi) = fcomplex.NaN;
                    tempIdx = *(vecIdx + iLo);
                    *(vecIdx + iLo) = *(vecIdx + iHi);
                    *(vecIdx + iHi) = tempIdx;
                }
                if (isSorted && vec[iLo] > oldVal) {
                    isSorted = false;
                }
                oldVal = vec[iLo];
                iLo += inc;
            }
            return isSorted;
        }

        /// <summary>
        /// sort nan elements to the end, test if the array is already sorted
        /// </summary>
        /// <returns>true, if already sorted</returns>
        unsafe internal static bool checkNaNAsc(long lo, ref long hi, long inc, fcomplex* vec) {
            bool isSorted = true;
            long iLo = lo, iHi = lo;
            if (hi == lo) {
                // only 1 element
                if (fcomplex.IsNaN(vec[lo])) {
                    hi = lo - inc;
                }
                return true;
            }
           
            fcomplex oldVal = vec[iLo];
            while (iLo <= hi) {
               
                fcomplex loVal = *(vec + iLo);
                if (fcomplex.IsNaN(loVal)) {
                    iHi = Math.Max(iHi, iLo + inc);
                    // walk up, finding next non-NaN value
                    while (iHi <= hi && fcomplex.IsNaN(*(vec + iHi))) {
                        iHi += inc;
                    }
                    if (iHi > hi) {
                        // iLo is NaN
                        hi = iLo - inc;
                        break;
                    }
                    // swap 
                    *(vec + iLo) = *(vec + iHi);
                    *(vec + iHi) = fcomplex.NaN;
                }
                if (isSorted && vec[iLo] < oldVal) {
                    isSorted = false;
                }
                oldVal = vec[iLo];
                iLo += inc;
            }
            return isSorted;
        }

        /// <summary>
        /// sort nan elements to the end, test if the array is already sorted
        /// </summary>
        /// <returns>true, if already sorted</returns>
        unsafe private static bool checkNaNDesc(long lo, ref long hi, long inc, fcomplex* vec) {
            bool isSorted = true;
            long iLo = lo, iHi = lo;
            if (hi == lo) {
                // only 1 element
                if (fcomplex.IsNaN(vec[lo])) {
                    hi = lo - inc;
                }
                return true;
            }
           
            fcomplex oldVal = vec[iLo];
            while (iLo <= hi) {
               
                fcomplex loVal = *(vec + iLo);
                if (fcomplex.IsNaN(loVal)) {
                    iHi = Math.Max(iHi, iLo + inc);
                    // walk up, finding next non-NaN value
                    while (iHi <= hi && fcomplex.IsNaN(*(vec + iHi))) {
                        iHi += inc;
                    }
                    if (iHi > hi) {
                        // iLo is NaN
                        hi = iLo - inc;
                        break;
                    }
                    // swap 
                    *(vec + iLo) = *(vec + iHi);
                    *(vec + iHi) = fcomplex.NaN;
                }
                if (isSorted && vec[iLo] > oldVal) {
                    isSorted = false;
                }
                oldVal = vec[iLo];
                iLo += inc;
            }
            return isSorted;
        }

       

        /// <summary>
        /// sort nan elements to the end, test if the array is already sorted
        /// </summary>
        /// <returns>true, if already sorted</returns>
        unsafe private static bool checkNaNIDXAsc(long lo, ref long hi, long inc, long* vecIdx, complex* vec) {
            long tempIdx;
            bool isSorted = true;
            long iLo = lo, iHi = lo;
            if (hi == lo) {
                // only 1 element
                if (complex.IsNaN(vec[lo])) {
                    hi = lo - inc;
                }
                return true;
            }
           
            complex oldVal = vec[iLo];
            while (iLo <= hi) {
               
                complex loVal = *(vec + iLo);
                if (complex.IsNaN(loVal)) {
                    iHi = Math.Max(iHi, iLo + inc);
                    // walk up, finding next non-NaN value
                    while (iHi <= hi && complex.IsNaN(*(vec + iHi))) {
                        iHi += inc;
                    }
                    if (iHi > hi) {
                        // iLo is NaN
                        hi = iLo - inc;
                        break;
                    }
                    // swap 
                    *(vec + iLo) = *(vec + iHi);
                    *(vec + iHi) = complex.NaN;
                    tempIdx = *(vecIdx + iLo);
                    *(vecIdx + iLo) = *(vecIdx + iHi);
                    *(vecIdx + iHi) = tempIdx;
                }
                if (isSorted && vec[iLo] < oldVal) {
                    isSorted = false;
                }
                oldVal = vec[iLo];
                iLo += inc;
            }
            return isSorted;
        }

        /// <summary>
        /// sort nan elements to the end, test if the array is already sorted
        /// </summary>
        /// <returns>true, if already sorted</returns>
        unsafe private static bool checkNaNIDXDesc(long lo, ref long hi, long inc, long* vecIdx, complex* vec) {
            long tempIdx;
            bool isSorted = true;
            long iLo = lo, iHi = lo;
            if (hi == lo) {
                // only 1 element
                if (complex.IsNaN(vec[lo])) {
                    hi = lo - inc;
                }
                return true;
            }
           
            complex oldVal = vec[iLo];
            while (iLo <= hi) {
               
                complex loVal = *(vec + iLo);
                if (complex.IsNaN(loVal)) {
                    iHi = Math.Max(iHi, iLo + inc);
                    // walk up, finding next non-NaN value
                    while (iHi <= hi && complex.IsNaN(*(vec + iHi))) {
                        iHi += inc;
                    }
                    if (iHi > hi) {
                        // iLo is NaN
                        hi = iLo - inc;
                        break;
                    }
                    // swap 
                    *(vec + iLo) = *(vec + iHi);
                    *(vec + iHi) = complex.NaN;
                    tempIdx = *(vecIdx + iLo);
                    *(vecIdx + iLo) = *(vecIdx + iHi);
                    *(vecIdx + iHi) = tempIdx;
                }
                if (isSorted && vec[iLo] > oldVal) {
                    isSorted = false;
                }
                oldVal = vec[iLo];
                iLo += inc;
            }
            return isSorted;
        }

        /// <summary>
        /// sort nan elements to the end, test if the array is already sorted
        /// </summary>
        /// <returns>true, if already sorted</returns>
        unsafe internal static bool checkNaNAsc(long lo, ref long hi, long inc, complex* vec) {
            bool isSorted = true;
            long iLo = lo, iHi = lo;
            if (hi == lo) {
                // only 1 element
                if (complex.IsNaN(vec[lo])) {
                    hi = lo - inc;
                }
                return true;
            }
           
            complex oldVal = vec[iLo];
            while (iLo <= hi) {
               
                complex loVal = *(vec + iLo);
                if (complex.IsNaN(loVal)) {
                    iHi = Math.Max(iHi, iLo + inc);
                    // walk up, finding next non-NaN value
                    while (iHi <= hi && complex.IsNaN(*(vec + iHi))) {
                        iHi += inc;
                    }
                    if (iHi > hi) {
                        // iLo is NaN
                        hi = iLo - inc;
                        break;
                    }
                    // swap 
                    *(vec + iLo) = *(vec + iHi);
                    *(vec + iHi) = complex.NaN;
                }
                if (isSorted && vec[iLo] < oldVal) {
                    isSorted = false;
                }
                oldVal = vec[iLo];
                iLo += inc;
            }
            return isSorted;
        }

        /// <summary>
        /// sort nan elements to the end, test if the array is already sorted
        /// </summary>
        /// <returns>true, if already sorted</returns>
        unsafe private static bool checkNaNDesc(long lo, ref long hi, long inc, complex* vec) {
            bool isSorted = true;
            long iLo = lo, iHi = lo;
            if (hi == lo) {
                // only 1 element
                if (complex.IsNaN(vec[lo])) {
                    hi = lo - inc;
                }
                return true;
            }
           
            complex oldVal = vec[iLo];
            while (iLo <= hi) {
               
                complex loVal = *(vec + iLo);
                if (complex.IsNaN(loVal)) {
                    iHi = Math.Max(iHi, iLo + inc);
                    // walk up, finding next non-NaN value
                    while (iHi <= hi && complex.IsNaN(*(vec + iHi))) {
                        iHi += inc;
                    }
                    if (iHi > hi) {
                        // iLo is NaN
                        hi = iLo - inc;
                        break;
                    }
                    // swap 
                    *(vec + iLo) = *(vec + iHi);
                    *(vec + iHi) = complex.NaN;
                }
                if (isSorted && vec[iLo] > oldVal) {
                    isSorted = false;
                }
                oldVal = vec[iLo];
                iLo += inc;
            }
            return isSorted;
        }


#endregion HYCALPER AUTO GENERATED CODE

        #region Debug assertion checks
        private static bool Check4A<T>(T[] vec, int lo, int hi, int pivIndex, int inc) {
            double piv = Convert.ToDouble(vec[pivIndex].ToString()); 
            for (int i = lo; i <= pivIndex; i += inc) {
                if (Convert.ToDouble(vec[i].ToString()) > piv) {
                    return false; 
                }
            }
            for (int i = hi; i >= pivIndex; i -= inc) {
                if (Convert.ToDouble(vec[i].ToString()) < piv) {
                    return false; 
                }
            }
            return true; 
        }
        private static bool Check4D<T>(T[] vec, int lo, int hi, int pivIndex, int inc) {
            double piv = Convert.ToDouble(vec[pivIndex].ToString()); 
            for (int i = lo; i <= pivIndex; i += inc) {
                if (Convert.ToDouble(vec[i].ToString()) < piv) {
                    return false; 
                }
            }
            for (int i = hi; i >= pivIndex; i -= inc) {
                if (Convert.ToDouble(vec[i].ToString()) > piv) {
                    return false; 
                }
            }
            return true; 
        }
        private static bool Check3(double[] vec, int hiBound, int loBound, int loSwap, int hiSwap, double pivotItem) {
            for (int i = loBound; i < loSwap - 1; i++) {
                if (!(vec[i] <= pivotItem)) {
                    return false; 
                }
            }
            for (int i = hiSwap + 1; i < hiBound; i++) {
                if (!(vec[i] > pivotItem)) {
                    return false; 
                }
            }
            if (loSwap < hiSwap) {
                if (!(vec[loSwap] <= pivotItem && pivotItem < vec[hiSwap])) {
                    return false; 
                }
            } else {
                if (!(vec[hiSwap] <= pivotItem && (loBound <= loSwap) && (loSwap <= hiSwap + 1) && (hiSwap + 1 <= hiBound + 1))) {
                    return false; 
                }
            }
            return true; 
        }
        private static bool Check1(double[] vec, int loBound, int loSwap, int hiSwap, double pivotItem) {
            for (int i = loBound + 1; i < loSwap - 1; i++) {
                if (!(vec[i] <= pivotItem && loSwap <= hiSwap + 1)) {
                    return false; 
                }
            }
            return true; 
        }
        private static bool Check2(double[] vec, int hiBound, int loSwap, int hiSwap, double pivotItem) {
            for (int i = hiSwap + 1; i < hiBound; i++) {
                if (!(vec[i] > pivotItem && loSwap <= hiSwap + 1)) {
                    return false; 
                }
            }
            return true;
        }
#endregion 

    }
}
