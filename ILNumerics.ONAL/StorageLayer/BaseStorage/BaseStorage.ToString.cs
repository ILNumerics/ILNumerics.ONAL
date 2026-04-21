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
using ILNumerics.Core.Arrays;
using ILNumerics.Core.MemoryLayer;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Threading;

namespace ILNumerics.Core.StorageLayer {

    public abstract partial class BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> : IDisposable, IStorage

        where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

        /// <summary>
        /// Create a textual representation of this storage.
        /// </summary>
        /// <returns>String containing general information about the current instance of 
        /// Storage and the formatted elements' values.</returns>
        /// <remarks>If the length of printed elements exceeds a certain value for a row, the printout will be abreviated.</remarks>
        public override string ToString() {
            return string.Join(Environment.NewLine, ToString(Settings.ToStringMaxNumberElementsPerDimension, Settings.ToStringMaxNumberElements, Settings.DefaultStorageOrder, true, true, null));
        }
        
        public IEnumerable<string> ToString(uint maxNumberElementsPerDimension, uint maxNumberElements, StorageOrders style, bool showType = true, bool showSize = true, int? columnWidth = null, bool scaleFP = true, bool waitForCompletion = true) {
            if (ElementInstance is double) {
                if (scaleFP) {
                    return ToStringScaling(maxNumberElementsPerDimension, DoubleGetElement, DoubleScale, maxNumberElements, style, showType, showSize, columnWidth ?? 12);
                } else {
                    return ToStringScaling(maxNumberElementsPerDimension, DoubleGetElement, NoScale, maxNumberElements, style, showType, showSize, columnWidth ?? 12);
                }
            } else if (ElementInstance is float) {
                if (scaleFP) {
                    return ToStringScaling(maxNumberElementsPerDimension, SingleGetElement, SingleScale, maxNumberElements, style, showType, showSize, columnWidth ?? 12);
                } else {
                    return ToStringScaling(maxNumberElementsPerDimension, SingleGetElement, NoScale, maxNumberElements, style, showType, showSize, columnWidth ?? 12);
                }
            } else if (ElementInstance is complex) {
                if (scaleFP) {
                    return ToStringScaling(maxNumberElementsPerDimension, ComplexGetElement, ComplexScale, maxNumberElements, style, showType, showSize, columnWidth ?? 12);
                } else {
                    return ToStringScaling(maxNumberElementsPerDimension, ComplexGetElement, NoScale, maxNumberElements, style, showType, showSize, columnWidth ?? 12);
                }
            } else if (ElementInstance is fcomplex) {
                if (scaleFP) {
                    return ToStringScaling(maxNumberElementsPerDimension, FComplexGetElement, FComplexScale, maxNumberElements, style, showType, showSize, columnWidth ?? 12);
                } else {
                    return ToStringScaling(maxNumberElementsPerDimension, FComplexGetElement, NoScale, maxNumberElements, style, showType, showSize, columnWidth ?? 12);
                }
            } else if (ElementInstance is bool) {
                return ToStringScaling(maxNumberElementsPerDimension, BoolGetElement, s => 1, maxNumberElements, style, showType, showSize, columnWidth ?? 2);
                //} else if (typeof(T) == typeof(string)) {
                //    return ToStringString(maxNumberElementsPerDimension, maxNumberElementsPerDimension, showType, showSize).ToString();
            } else if (typeof(T) == typeof(BaseArray)) {
                return ToStringBaseArray(maxNumberElementsPerDimension, maxNumberElementsPerDimension * 16, showType, showSize);
            } else {
                return ToStringScaling(maxNumberElementsPerDimension, GenericGetElement, s => 1, maxNumberElements, style, showType, showSize, columnWidth ?? 12);
            }
        }

        #region helpers

        #region predefined converter functions - FLOATING POINT TYPES
        double NoScale(StorageT A) {
            return 1.0;
        }
        double DoubleScale(StorageT A) {
            double min, max;
            if ((A as Storage<double>).GetLimits(out min, out max, true)) {
                return Global.Helper.GetScalingForPrint(min, max);
            }
            return 1.0;
        }
        
        unsafe string DoubleGetElement(IntPtr acc, uint len, double scaling, int columnWidth) {
            double element = *((double*)Handles[0].Pointer + Size.GetSeqIndex((uint*)acc, Size.NumberOfDimensions));
            var sElement = Global.Helper.PrettyPrintNumber(scaling, element, 6, columnWidth);
            return sElement; 
        }
        double SingleScale(StorageT A) {
            float min, max;
            if ((A as Storage<float>).GetLimits(out min, out max, true)) {
                return Global.Helper.GetScalingForPrint(min, max);
            }
            return 1.0;
        }
        
        unsafe string SingleGetElement(IntPtr acc, uint len, double scaling, int columnWidth) {
            float element = *((float*)Handles[0].Pointer + Size.GetSeqIndex((uint*)acc, Size.NumberOfDimensions));
            var sElement = Global.Helper.PrettyPrintNumber(scaling, element, 6, columnWidth);
            return sElement; 
        }
        double ComplexScale(StorageT A) {
            complex min, max;
            if ((A as Storage<complex>).GetLimits(out min, out max, true)) {
                return Global.Helper.GetScalingForPrint(Math.Min(min.real, min.imag), Math.Max(max.real, max.imag));
            }
            return 1.0;
        }
        
        unsafe string ComplexGetElement(IntPtr acc, uint len, double scaling, int columnWidth) {
            complex element = *((complex*)Handles[0].Pointer + Size.GetSeqIndex((uint*)acc, Size.NumberOfDimensions));
            var sElement = Global.Helper.PrettyPrintNumber(scaling, element, 5, columnWidth);
            return sElement; 
        }
        double FComplexScale(StorageT A) {
            fcomplex min, max;
            if ((A as Storage<fcomplex>).GetLimits(out min, out max, true)) {
                return Global.Helper.GetScalingForPrint(Math.Min(min.real, min.imag), Math.Max(max.real, max.imag));
            }
            return 1.0;
        }
        
        unsafe string FComplexGetElement(IntPtr acc, uint len, double scaling, int columnWidth) {
            fcomplex element = *((fcomplex*)Handles[0].Pointer + Size.GetSeqIndex((uint*)acc, Size.NumberOfDimensions));
            var sElement = Global.Helper.PrettyPrintNumber(scaling, element, 5, columnWidth);
            return sElement; 
        }

        unsafe string GenericGetElement(IntPtr acc, uint len, double scaling, int columnWidth) {
            T element = GetValueSeq(Size.GetSeqIndex((uint*)acc, Size.NumberOfDimensions));
            var sElement = element?.ToString().PadLeft(columnWidth);
            return sElement; 
        }

        
        unsafe string BoolGetElement(IntPtr acc, uint len, double scaling, int columnWidth) {
            byte element = *((byte*)Handles[0].Pointer + Size.GetSeqIndex((uint*)acc, Size.NumberOfDimensions));
            var sElement = (element != 0) ? " â–®" : " â–¯";
            return sElement; 
        }
        #endregion

        /// <summary>
        /// Checks and adjusts the limits used for ToString()
        /// </summary>
        /// <param name="maxNumberElementsPerDim">[In/Out] Upper limit of elements displayed per dimension.</param>
        /// <param name="maxNumberElements">[In/Out] upper, hard limit of all elements displayed. </param>
        /// <param name="order">The storage order for the output. Row- /Column order.</param>
        /// <returns>The number of dimensions to be displayed. Any or both ref parameters may also be adjusted.</returns>
        /// <remarks><para>If the number of dimensions is too high and the actual number of elements per dimension would 
        /// lead to too many elements being shown the hard limit <paramref name="maxNumberElements"/> takes preceedence. 
        /// The number of dimension to be displayed is successively decreased until the resulting overall number of elements 
        /// drops below <paramref name="maxNumberElements"/>. For <paramref name="order"/> equal to <see cref="StorageOrders.ColumnMajor"/>
        /// the last dimensions are skipped first. For <paramref name="order"/> equal to <see cref="StorageOrders.RowMajor"/>
        /// first dimensions are skipped first. I.e: the 'important' dimensions are preferred for display.</para>
        /// </remarks>
        private uint checkAdjustLimits(ref uint maxNumberElementsPerDim, ref uint maxNumberElements, StorageOrders order) {
            if (maxNumberElementsPerDim == 0) {
                maxNumberElementsPerDim = uint.MaxValue; 
            }
            if (maxNumberElements == 0) {
                maxNumberElements = uint.MaxValue; 
            }
            if (maxNumberElementsPerDim < 2) {
                maxNumberElementsPerDim = 2; //start and end at least shown
            }
            int ndims = (int)Size.NumberOfDimensions;
            long nrElements;
            if (order == StorageOrders.ColumnMajor) {
                do {
                    nrElements = 1;
                    for (int i = 0; i < Math.Min(ndims, Size.NumberOfDimensions); i++) {
                        nrElements *= (uint)Math.Min(maxNumberElementsPerDim, Size[i]);
                    }
                    if (nrElements <= maxNumberElements) {
                        break;
                    }
                    ndims--;
                }
                while (ndims > 0);

            } else if (order == StorageOrders.RowMajor) {
                do {
                    nrElements = 1;
                    for (var i = Math.Min(ndims, Size.NumberOfDimensions); i-- > 0;) {
                        nrElements *= (uint)Math.Min(maxNumberElementsPerDim, Size[i]);
                    }
                    if (nrElements <= maxNumberElements) {
                        break;
                    }
                    ndims--;
                }
                while (ndims > 0);
            } else {
                throw new ArgumentException($"Unknown storage order or storage order not supported: {order}.");
            }
            System.Diagnostics.Debug.Assert(ndims >= 0);
            if (ndims < 2) {
                maxNumberElementsPerDim = (uint)Math.Sqrt(maxNumberElements);
                ndims = 2; 
            }
            return (uint)ndims;
        }
        #endregion

        
        public unsafe IEnumerable<string> ToStringScaling(
            uint maxNumberElementsPerDim,
            Func<IntPtr, uint, double, int, string> retrievalFunc, // long* acc, uint len, double scaling, int columnWidth -> string
            Func<StorageT, double> scalingFunc,
            uint maxNumberElementsAllDims = 10000,
            StorageOrders style = StorageOrders.ColumnMajor,
            bool showType = true,
            bool showSize = true,
            int columnWidth = 10) {
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 1u)) {

                var ret = new List<string>(); 
                StringBuilder s = new StringBuilder(500);
                
                void Append(string txt) {
                    s.Append(txt);
                }
                void AppendLine(string txt = "") {
                    
                    ret.Add(s.ToString());
                    s.Clear(); 
                }
                IEnumerable<string> Finish() {
                    if (s.Length > 0) {
                        ret.Add(s.ToString()); 
                    }
                    return ret; 
                }


                if (showType) {
                    Append($"<{typeof(T).Name}>");
                }
                if (showSize) {
                    if (s.Length > 0) Append(" ");
                    Append($"{Size.ToString()}");
                }
                if (Size.NumberOfElements == 0) {
                    if (s.Length > 0) Append(" ");
                    Append("[empty]");
                    return Finish();
                }

                // scalar (numpy)?
                if (Size.NumberOfDimensions == 0) {
                    Append($" {GetValue(0)}");
                    return Finish();
                }

                var ndims = checkAdjustLimits(ref maxNumberElementsPerDim, ref maxNumberElementsAllDims, style);
                var nEnd = maxNumberElementsPerDim / 2;  // favor start for uneven maxNumberElementsPerDim!
                var nStart = maxNumberElementsPerDim - nEnd;

                uint* acc = stackalloc uint[Math.Max((int)Size.NumberOfDimensions, 2)];
                for (int i = Math.Max((int)Size.NumberOfDimensions, 2); i-- > 0;) acc[i] = 0;

                var bufferSetReady = m_handles.CurrentDeviceIdx >= 0; 
                Double scaling = bufferSetReady ? scalingFunc(this as StorageT) : 1.0;

                if (scaling != 1) {
                    Append($" {scaling:e0} * ");
                }

                if (style == StorageOrders.RowMajor) {
                    uint dID1 = Math.Max((uint)Size.NumberOfDimensions, 2) - 2, 
                        dID2 = Math.Max((uint)Size.NumberOfDimensions, 2) - 1; 
                    while (acc[ndims - 1] < Size[ndims - 1]) {

                        // show only two LAST dimensions at the same time ... 
                        // print header

                        if (Size.NumberOfDimensions > 2) {
                            if (s.Length > 0) AppendLine();
                            Append("(");
                            for (uint i = 0; i < Size.NumberOfDimensions - 2; i++)
                                Append(String.Format("{0},", acc[i]));
                            Append(":,:)");
                        }
                        // show this for 2 TRAILING dimensions
                        for (uint d0 = 0; d0 < Size[dID1]; d0++) {
                            if (d0 == nStart && d0 < Size[dID1] - nEnd) {
                                d0 = (uint)(Size[dID1] - nEnd - 1); // d0++ in for loop header!
                                AppendLine();
                                Append("...".PadLeft(columnWidth));
                                continue;
                            }
                            if (s.Length > 0) AppendLine();
                            acc[dID1] = d0;

                            for (uint d1 = 0; d1 < Size[dID2]; d1++) {
                                if (d1 == nStart && d1 < Size[dID2] - nEnd) {
                                    d1 = (uint)(Size[dID2] - nEnd - 1); // d1++ in for loop header! 
                                    Append("...".PadLeft(columnWidth));
                                    continue;
                                }

                                acc[dID2] = d1;
                                if (bufferSetReady) {
                                    var sElement = retrievalFunc((IntPtr)acc, Size.NumberOfDimensions, scaling, columnWidth);
                                    Append(sElement);
                                } else {
                                    Append("?".PadLeft(columnWidth)); 
                                }
                            }
                        }
                        // increase higher dimension
                        uint d = 2;
                        while (d <= ndims - 1) {
                            var d_ = Size.NumberOfDimensions - d - 1; 
                            acc[d_]++;
                            if (acc[d_] == nStart && acc[d_] < Size[d_] - nEnd) {
                                AppendLine(Environment.NewLine);
                                Append("(");
                                for (uint i = 0; i < Size.NumberOfDimensions - 2; i++)
                                    Append(String.Format("{0},", acc[i]));
                                Append(":,:)");
                                AppendLine();
                                Append("...");
                                acc[d_] = (uint)(Size[d_] - nEnd);
                                break;
                            }
                            if (acc[d_] < Size[d_]) break;
                            acc[d_] = 0;
                            d++;
                        }
                        if (d >= ndims) break;
                    }
                } else { 
                    // ColumnMajor and other styles:  
                    while (acc[ndims - 1] < Size[ndims - 1]) {

                        // show only two first dimensions at the same time ... 
                        // print header
                        if (Size.NumberOfDimensions > 2) {
                            if (s.Length > 0) AppendLine();
                            Append("(:,:");
                            for (uint i = 2; i < Size.NumberOfDimensions; i++)
                                Append(String.Format(",{0}", acc[i]));
                            Append(")");
                        }
                        // show this for 2 leading dimensions
                        for (uint d0 = 0; d0 < Size[0]; d0++) {
                            if (d0 == nStart && d0 < Size[0] - nEnd) {
                                d0 = (uint)(Size[0] - nEnd - 1); // d0++ in for loop header!
                                AppendLine();
                                Append("...".PadLeft(columnWidth));
                                continue;
                            }
                            if (s.Length > 0) AppendLine();
                            acc[0] = d0;

                            for (uint d1 = 0; d1 < Size[1]; d1++) {
                                if (d1 == nStart && d1 < Size[1] - nEnd) {
                                    d1 = (uint)(Size[1] - nEnd - 1); // d1++ in for loop header! 
                                    Append("...".PadLeft(columnWidth));
                                    continue;
                                }

                                acc[1] = d1;
                                var sElement = retrievalFunc((IntPtr)acc, Size.NumberOfDimensions, scaling, columnWidth);
                                Append(sElement);
                            }
                        }
                        // increase higher dimension
                        uint d = 2;
                        while (d <= ndims - 1) {
                            acc[d]++;
                            if (acc[d] == nStart && acc[d] < Size[d] - nEnd) {
                                AppendLine();
                                Append("(:,:");
                                for (uint i = 2; i < Size.NumberOfDimensions; i++)
                                    Append(String.Format(",{0}", acc[i]));
                                Append(")");

                                AppendLine();
                                Append("...");
                                acc[d] = (uint)(Size[d] - nEnd);
                                break;
                            }
                            if (acc[d] < Size[d]) break;
                            acc[d] = 0;
                            d++;
                        }
                        if (d >= ndims) break;
                    }

                }
                if (ndims < Size.NumberOfDimensions) {
                    long nrSkipped = 1;
                    int i = 0;
                    for (; i < Size.NumberOfDimensions - ndims; i++) {
                        nrSkipped *= Size[style == StorageOrders.ColumnMajor ? Size.NumberOfDimensions - 1 - i : i];
                    }
                    for (; i < Size.NumberOfDimensions; i++) {
                        // from these dimensions we have shown the first page only
                        nrSkipped *= (Size[style == StorageOrders.ColumnMajor ? Size.NumberOfDimensions - 1 - i : i] - 1);
                    }
                    AppendLine(); 
                    Append($"({nrSkipped} more elements)");
                }
                return Finish(); 
            }
        }

        #region HYCALPER LOOPSTART ToString Integer types
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

        
        private unsafe StringBuilder ToStringSbyte(uint maxRows, uint maxCharsPerRow, bool includeType = true, bool includeSize = true) {

            StringBuilder s = new StringBuilder(500);
            if (includeType) {
                s.Append($"<{typeof(T).Name}>");
            }
            if (includeSize) {
                if (s.Length > 0) s.Append(" ");
                s.Append($"{Size.ToString()}");
            }
            if (Size.NumberOfElements == 0) {
                if (s.Length > 0) s.Append(" ");
                s.Append("[empty]");
                return s;
            }
            System.Diagnostics.Debug.Assert(Size.NumberOfDimensions > 0); 

            uint* acc = stackalloc uint[Math.Max((int)Size.NumberOfDimensions, 2)];
            for (int i = Math.Max((int)Size.NumberOfDimensions, 2); i-- > 0;) acc[i] = 0;

            String sElement;
            uint rowsCount = 0;
            int elemLength = Math.Max(sbyte.MaxValue.ToString().Length, sbyte.MinValue.ToString().Length) + 1; 

            var pHost = (sbyte*)Handles[0].Pointer;

            // scalar (numpy)?
            if (Size.NumberOfElements == 1 && Size.NumberOfDimensions == 0) {
                s.Append($" {pHost[Size.BaseOffset]}");
                return s;
            }

            sbyte element;
            bool firstPage = true;
            while (acc[Size.NumberOfDimensions - 1] < Size[Size.NumberOfDimensions - 1]) {

                // show only two first dimensions at the same time ... 
                // print header
                if (!firstPage && s.Length > 0) s.Append(Environment.NewLine);
                if (Size.NumberOfDimensions > 2) {
                    s.Append("(:,:");
                    for (uint i = 2; i < Size.NumberOfDimensions; i++)
                        s.Append(String.Format(",{0}", acc[i]));
                    s.Append(")");
                }
                // show this for 2 leading dimensions
                for (uint d0 = 0; d0 < Size[0]; d0++) {
                    if (++rowsCount > maxRows) {
                        s.Append(Environment.NewLine + "( ... more rows exist!)");
                        return s;
                    }
                    if (s.Length > 0) s.Append(Environment.NewLine);
                    acc[0] = d0;
                    uint curLineLength = 0;
                    for (uint d1 = 0; d1 < Size[1]; d1++) {
                        acc[1] = d1;
                        element = pHost[Size.GetSeqIndex(acc, Size.NumberOfDimensions)];
                        sElement = element.ToString();
                        sElement = sElement.PadLeft(elemLength);
                        curLineLength += (uint)sElement.Length;
                        if (curLineLength > maxCharsPerRow - " ...".Length) {
                            s.Append(" ...");
                            break;
                        } else {
                            s.Append(sElement);
                        }
                    }
                }
                // increase higher dimension
                firstPage = false;
                uint d = 2;
                while (d <= Size.NumberOfDimensions - 1) {
                    acc[d]++;
                    if (acc[d] < Size[d]) break;
                    acc[d] = 0;
                    d++;
                }
                if (d >= Size.NumberOfDimensions) break;
            }
            return s;
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        
        private unsafe StringBuilder ToStringUInt64(uint maxRows, uint maxCharsPerRow, bool includeType = true, bool includeSize = true) {

            StringBuilder s = new StringBuilder(500);
            if (includeType) {
                s.Append($"<{typeof(T).Name}>");
            }
            if (includeSize) {
                if (s.Length > 0) s.Append(" ");
                s.Append($"{Size.ToString()}");
            }
            if (Size.NumberOfElements == 0) {
                if (s.Length > 0) s.Append(" ");
                s.Append("[empty]");
                return s;
            }
            System.Diagnostics.Debug.Assert(Size.NumberOfDimensions > 0); 

            uint* acc = stackalloc uint[Math.Max((int)Size.NumberOfDimensions, 2)];
            for (int i = Math.Max((int)Size.NumberOfDimensions, 2); i-- > 0;) acc[i] = 0;

            String sElement;
            uint rowsCount = 0;
            int elemLength = Math.Max(ulong.MaxValue.ToString().Length, ulong.MinValue.ToString().Length) + 1; 

            var pHost = (ulong*)Handles[0].Pointer;

            // scalar (numpy)?
            if (Size.NumberOfElements == 1 && Size.NumberOfDimensions == 0) {
                s.Append($" {pHost[Size.BaseOffset]}");
                return s;
            }

            ulong element;
            bool firstPage = true;
            while (acc[Size.NumberOfDimensions - 1] < Size[Size.NumberOfDimensions - 1]) {

                // show only two first dimensions at the same time ... 
                // print header
                if (!firstPage && s.Length > 0) s.Append(Environment.NewLine);
                if (Size.NumberOfDimensions > 2) {
                    s.Append("(:,:");
                    for (uint i = 2; i < Size.NumberOfDimensions; i++)
                        s.Append(String.Format(",{0}", acc[i]));
                    s.Append(")");
                }
                // show this for 2 leading dimensions
                for (uint d0 = 0; d0 < Size[0]; d0++) {
                    if (++rowsCount > maxRows) {
                        s.Append(Environment.NewLine + "( ... more rows exist!)");
                        return s;
                    }
                    if (s.Length > 0) s.Append(Environment.NewLine);
                    acc[0] = d0;
                    uint curLineLength = 0;
                    for (uint d1 = 0; d1 < Size[1]; d1++) {
                        acc[1] = d1;
                        element = pHost[Size.GetSeqIndex(acc, Size.NumberOfDimensions)];
                        sElement = element.ToString();
                        sElement = sElement.PadLeft(elemLength);
                        curLineLength += (uint)sElement.Length;
                        if (curLineLength > maxCharsPerRow - " ...".Length) {
                            s.Append(" ...");
                            break;
                        } else {
                            s.Append(sElement);
                        }
                    }
                }
                // increase higher dimension
                firstPage = false;
                uint d = 2;
                while (d <= Size.NumberOfDimensions - 1) {
                    acc[d]++;
                    if (acc[d] < Size[d]) break;
                    acc[d] = 0;
                    d++;
                }
                if (d >= Size.NumberOfDimensions) break;
            }
            return s;
        }
       

        
        private unsafe StringBuilder ToStringInt64(uint maxRows, uint maxCharsPerRow, bool includeType = true, bool includeSize = true) {

            StringBuilder s = new StringBuilder(500);
            if (includeType) {
                s.Append($"<{typeof(T).Name}>");
            }
            if (includeSize) {
                if (s.Length > 0) s.Append(" ");
                s.Append($"{Size.ToString()}");
            }
            if (Size.NumberOfElements == 0) {
                if (s.Length > 0) s.Append(" ");
                s.Append("[empty]");
                return s;
            }
            System.Diagnostics.Debug.Assert(Size.NumberOfDimensions > 0); 

            uint* acc = stackalloc uint[Math.Max((int)Size.NumberOfDimensions, 2)];
            for (int i = Math.Max((int)Size.NumberOfDimensions, 2); i-- > 0;) acc[i] = 0;

            String sElement;
            uint rowsCount = 0;
            int elemLength = Math.Max(long.MaxValue.ToString().Length, long.MinValue.ToString().Length) + 1; 

            var pHost = (long*)Handles[0].Pointer;

            // scalar (numpy)?
            if (Size.NumberOfElements == 1 && Size.NumberOfDimensions == 0) {
                s.Append($" {pHost[Size.BaseOffset]}");
                return s;
            }

            long element;
            bool firstPage = true;
            while (acc[Size.NumberOfDimensions - 1] < Size[Size.NumberOfDimensions - 1]) {

                // show only two first dimensions at the same time ... 
                // print header
                if (!firstPage && s.Length > 0) s.Append(Environment.NewLine);
                if (Size.NumberOfDimensions > 2) {
                    s.Append("(:,:");
                    for (uint i = 2; i < Size.NumberOfDimensions; i++)
                        s.Append(String.Format(",{0}", acc[i]));
                    s.Append(")");
                }
                // show this for 2 leading dimensions
                for (uint d0 = 0; d0 < Size[0]; d0++) {
                    if (++rowsCount > maxRows) {
                        s.Append(Environment.NewLine + "( ... more rows exist!)");
                        return s;
                    }
                    if (s.Length > 0) s.Append(Environment.NewLine);
                    acc[0] = d0;
                    uint curLineLength = 0;
                    for (uint d1 = 0; d1 < Size[1]; d1++) {
                        acc[1] = d1;
                        element = pHost[Size.GetSeqIndex(acc, Size.NumberOfDimensions)];
                        sElement = element.ToString();
                        sElement = sElement.PadLeft(elemLength);
                        curLineLength += (uint)sElement.Length;
                        if (curLineLength > maxCharsPerRow - " ...".Length) {
                            s.Append(" ...");
                            break;
                        } else {
                            s.Append(sElement);
                        }
                    }
                }
                // increase higher dimension
                firstPage = false;
                uint d = 2;
                while (d <= Size.NumberOfDimensions - 1) {
                    acc[d]++;
                    if (acc[d] < Size[d]) break;
                    acc[d] = 0;
                    d++;
                }
                if (d >= Size.NumberOfDimensions) break;
            }
            return s;
        }
       

        
        private unsafe StringBuilder ToStringUInt32(uint maxRows, uint maxCharsPerRow, bool includeType = true, bool includeSize = true) {

            StringBuilder s = new StringBuilder(500);
            if (includeType) {
                s.Append($"<{typeof(T).Name}>");
            }
            if (includeSize) {
                if (s.Length > 0) s.Append(" ");
                s.Append($"{Size.ToString()}");
            }
            if (Size.NumberOfElements == 0) {
                if (s.Length > 0) s.Append(" ");
                s.Append("[empty]");
                return s;
            }
            System.Diagnostics.Debug.Assert(Size.NumberOfDimensions > 0); 

            uint* acc = stackalloc uint[Math.Max((int)Size.NumberOfDimensions, 2)];
            for (int i = Math.Max((int)Size.NumberOfDimensions, 2); i-- > 0;) acc[i] = 0;

            String sElement;
            uint rowsCount = 0;
            int elemLength = Math.Max(uint.MaxValue.ToString().Length, uint.MinValue.ToString().Length) + 1; 

            var pHost = (uint*)Handles[0].Pointer;

            // scalar (numpy)?
            if (Size.NumberOfElements == 1 && Size.NumberOfDimensions == 0) {
                s.Append($" {pHost[Size.BaseOffset]}");
                return s;
            }

            uint element;
            bool firstPage = true;
            while (acc[Size.NumberOfDimensions - 1] < Size[Size.NumberOfDimensions - 1]) {

                // show only two first dimensions at the same time ... 
                // print header
                if (!firstPage && s.Length > 0) s.Append(Environment.NewLine);
                if (Size.NumberOfDimensions > 2) {
                    s.Append("(:,:");
                    for (uint i = 2; i < Size.NumberOfDimensions; i++)
                        s.Append(String.Format(",{0}", acc[i]));
                    s.Append(")");
                }
                // show this for 2 leading dimensions
                for (uint d0 = 0; d0 < Size[0]; d0++) {
                    if (++rowsCount > maxRows) {
                        s.Append(Environment.NewLine + "( ... more rows exist!)");
                        return s;
                    }
                    if (s.Length > 0) s.Append(Environment.NewLine);
                    acc[0] = d0;
                    uint curLineLength = 0;
                    for (uint d1 = 0; d1 < Size[1]; d1++) {
                        acc[1] = d1;
                        element = pHost[Size.GetSeqIndex(acc, Size.NumberOfDimensions)];
                        sElement = element.ToString();
                        sElement = sElement.PadLeft(elemLength);
                        curLineLength += (uint)sElement.Length;
                        if (curLineLength > maxCharsPerRow - " ...".Length) {
                            s.Append(" ...");
                            break;
                        } else {
                            s.Append(sElement);
                        }
                    }
                }
                // increase higher dimension
                firstPage = false;
                uint d = 2;
                while (d <= Size.NumberOfDimensions - 1) {
                    acc[d]++;
                    if (acc[d] < Size[d]) break;
                    acc[d] = 0;
                    d++;
                }
                if (d >= Size.NumberOfDimensions) break;
            }
            return s;
        }
       

        
        private unsafe StringBuilder ToStringUInt16(uint maxRows, uint maxCharsPerRow, bool includeType = true, bool includeSize = true) {

            StringBuilder s = new StringBuilder(500);
            if (includeType) {
                s.Append($"<{typeof(T).Name}>");
            }
            if (includeSize) {
                if (s.Length > 0) s.Append(" ");
                s.Append($"{Size.ToString()}");
            }
            if (Size.NumberOfElements == 0) {
                if (s.Length > 0) s.Append(" ");
                s.Append("[empty]");
                return s;
            }
            System.Diagnostics.Debug.Assert(Size.NumberOfDimensions > 0); 

            uint* acc = stackalloc uint[Math.Max((int)Size.NumberOfDimensions, 2)];
            for (int i = Math.Max((int)Size.NumberOfDimensions, 2); i-- > 0;) acc[i] = 0;

            String sElement;
            uint rowsCount = 0;
            int elemLength = Math.Max(ushort.MaxValue.ToString().Length, ushort.MinValue.ToString().Length) + 1; 

            var pHost = (ushort*)Handles[0].Pointer;

            // scalar (numpy)?
            if (Size.NumberOfElements == 1 && Size.NumberOfDimensions == 0) {
                s.Append($" {pHost[Size.BaseOffset]}");
                return s;
            }

            ushort element;
            bool firstPage = true;
            while (acc[Size.NumberOfDimensions - 1] < Size[Size.NumberOfDimensions - 1]) {

                // show only two first dimensions at the same time ... 
                // print header
                if (!firstPage && s.Length > 0) s.Append(Environment.NewLine);
                if (Size.NumberOfDimensions > 2) {
                    s.Append("(:,:");
                    for (uint i = 2; i < Size.NumberOfDimensions; i++)
                        s.Append(String.Format(",{0}", acc[i]));
                    s.Append(")");
                }
                // show this for 2 leading dimensions
                for (uint d0 = 0; d0 < Size[0]; d0++) {
                    if (++rowsCount > maxRows) {
                        s.Append(Environment.NewLine + "( ... more rows exist!)");
                        return s;
                    }
                    if (s.Length > 0) s.Append(Environment.NewLine);
                    acc[0] = d0;
                    uint curLineLength = 0;
                    for (uint d1 = 0; d1 < Size[1]; d1++) {
                        acc[1] = d1;
                        element = pHost[Size.GetSeqIndex(acc, Size.NumberOfDimensions)];
                        sElement = element.ToString();
                        sElement = sElement.PadLeft(elemLength);
                        curLineLength += (uint)sElement.Length;
                        if (curLineLength > maxCharsPerRow - " ...".Length) {
                            s.Append(" ...");
                            break;
                        } else {
                            s.Append(sElement);
                        }
                    }
                }
                // increase higher dimension
                firstPage = false;
                uint d = 2;
                while (d <= Size.NumberOfDimensions - 1) {
                    acc[d]++;
                    if (acc[d] < Size[d]) break;
                    acc[d] = 0;
                    d++;
                }
                if (d >= Size.NumberOfDimensions) break;
            }
            return s;
        }
       

        
        private unsafe StringBuilder ToStringInt16(uint maxRows, uint maxCharsPerRow, bool includeType = true, bool includeSize = true) {

            StringBuilder s = new StringBuilder(500);
            if (includeType) {
                s.Append($"<{typeof(T).Name}>");
            }
            if (includeSize) {
                if (s.Length > 0) s.Append(" ");
                s.Append($"{Size.ToString()}");
            }
            if (Size.NumberOfElements == 0) {
                if (s.Length > 0) s.Append(" ");
                s.Append("[empty]");
                return s;
            }
            System.Diagnostics.Debug.Assert(Size.NumberOfDimensions > 0); 

            uint* acc = stackalloc uint[Math.Max((int)Size.NumberOfDimensions, 2)];
            for (int i = Math.Max((int)Size.NumberOfDimensions, 2); i-- > 0;) acc[i] = 0;

            String sElement;
            uint rowsCount = 0;
            int elemLength = Math.Max(short.MaxValue.ToString().Length, short.MinValue.ToString().Length) + 1; 

            var pHost = (short*)Handles[0].Pointer;

            // scalar (numpy)?
            if (Size.NumberOfElements == 1 && Size.NumberOfDimensions == 0) {
                s.Append($" {pHost[Size.BaseOffset]}");
                return s;
            }

            short element;
            bool firstPage = true;
            while (acc[Size.NumberOfDimensions - 1] < Size[Size.NumberOfDimensions - 1]) {

                // show only two first dimensions at the same time ... 
                // print header
                if (!firstPage && s.Length > 0) s.Append(Environment.NewLine);
                if (Size.NumberOfDimensions > 2) {
                    s.Append("(:,:");
                    for (uint i = 2; i < Size.NumberOfDimensions; i++)
                        s.Append(String.Format(",{0}", acc[i]));
                    s.Append(")");
                }
                // show this for 2 leading dimensions
                for (uint d0 = 0; d0 < Size[0]; d0++) {
                    if (++rowsCount > maxRows) {
                        s.Append(Environment.NewLine + "( ... more rows exist!)");
                        return s;
                    }
                    if (s.Length > 0) s.Append(Environment.NewLine);
                    acc[0] = d0;
                    uint curLineLength = 0;
                    for (uint d1 = 0; d1 < Size[1]; d1++) {
                        acc[1] = d1;
                        element = pHost[Size.GetSeqIndex(acc, Size.NumberOfDimensions)];
                        sElement = element.ToString();
                        sElement = sElement.PadLeft(elemLength);
                        curLineLength += (uint)sElement.Length;
                        if (curLineLength > maxCharsPerRow - " ...".Length) {
                            s.Append(" ...");
                            break;
                        } else {
                            s.Append(sElement);
                        }
                    }
                }
                // increase higher dimension
                firstPage = false;
                uint d = 2;
                while (d <= Size.NumberOfDimensions - 1) {
                    acc[d]++;
                    if (acc[d] < Size[d]) break;
                    acc[d] = 0;
                    d++;
                }
                if (d >= Size.NumberOfDimensions) break;
            }
            return s;
        }
       

        
        private unsafe StringBuilder ToStringByte(uint maxRows, uint maxCharsPerRow, bool includeType = true, bool includeSize = true) {

            StringBuilder s = new StringBuilder(500);
            if (includeType) {
                s.Append($"<{typeof(T).Name}>");
            }
            if (includeSize) {
                if (s.Length > 0) s.Append(" ");
                s.Append($"{Size.ToString()}");
            }
            if (Size.NumberOfElements == 0) {
                if (s.Length > 0) s.Append(" ");
                s.Append("[empty]");
                return s;
            }
            System.Diagnostics.Debug.Assert(Size.NumberOfDimensions > 0); 

            uint* acc = stackalloc uint[Math.Max((int)Size.NumberOfDimensions, 2)];
            for (int i = Math.Max((int)Size.NumberOfDimensions, 2); i-- > 0;) acc[i] = 0;

            String sElement;
            uint rowsCount = 0;
            int elemLength = Math.Max(byte.MaxValue.ToString().Length, byte.MinValue.ToString().Length) + 1; 

            var pHost = (byte*)Handles[0].Pointer;

            // scalar (numpy)?
            if (Size.NumberOfElements == 1 && Size.NumberOfDimensions == 0) {
                s.Append($" {pHost[Size.BaseOffset]}");
                return s;
            }

            byte element;
            bool firstPage = true;
            while (acc[Size.NumberOfDimensions - 1] < Size[Size.NumberOfDimensions - 1]) {

                // show only two first dimensions at the same time ... 
                // print header
                if (!firstPage && s.Length > 0) s.Append(Environment.NewLine);
                if (Size.NumberOfDimensions > 2) {
                    s.Append("(:,:");
                    for (uint i = 2; i < Size.NumberOfDimensions; i++)
                        s.Append(String.Format(",{0}", acc[i]));
                    s.Append(")");
                }
                // show this for 2 leading dimensions
                for (uint d0 = 0; d0 < Size[0]; d0++) {
                    if (++rowsCount > maxRows) {
                        s.Append(Environment.NewLine + "( ... more rows exist!)");
                        return s;
                    }
                    if (s.Length > 0) s.Append(Environment.NewLine);
                    acc[0] = d0;
                    uint curLineLength = 0;
                    for (uint d1 = 0; d1 < Size[1]; d1++) {
                        acc[1] = d1;
                        element = pHost[Size.GetSeqIndex(acc, Size.NumberOfDimensions)];
                        sElement = element.ToString();
                        sElement = sElement.PadLeft(elemLength);
                        curLineLength += (uint)sElement.Length;
                        if (curLineLength > maxCharsPerRow - " ...".Length) {
                            s.Append(" ...");
                            break;
                        } else {
                            s.Append(sElement);
                        }
                    }
                }
                // increase higher dimension
                firstPage = false;
                uint d = 2;
                while (d <= Size.NumberOfDimensions - 1) {
                    acc[d]++;
                    if (acc[d] < Size[d]) break;
                    acc[d] = 0;
                    d++;
                }
                if (d >= Size.NumberOfDimensions) break;
            }
            return s;
        }
       

        
        private unsafe StringBuilder ToStringInt32(uint maxRows, uint maxCharsPerRow, bool includeType = true, bool includeSize = true) {

            StringBuilder s = new StringBuilder(500);
            if (includeType) {
                s.Append($"<{typeof(T).Name}>");
            }
            if (includeSize) {
                if (s.Length > 0) s.Append(" ");
                s.Append($"{Size.ToString()}");
            }
            if (Size.NumberOfElements == 0) {
                if (s.Length > 0) s.Append(" ");
                s.Append("[empty]");
                return s;
            }
            System.Diagnostics.Debug.Assert(Size.NumberOfDimensions > 0); 

            uint* acc = stackalloc uint[Math.Max((int)Size.NumberOfDimensions, 2)];
            for (int i = Math.Max((int)Size.NumberOfDimensions, 2); i-- > 0;) acc[i] = 0;

            String sElement;
            uint rowsCount = 0;
            int elemLength = Math.Max(int.MaxValue.ToString().Length, int.MinValue.ToString().Length) + 1; 

            var pHost = (int*)Handles[0].Pointer;

            // scalar (numpy)?
            if (Size.NumberOfElements == 1 && Size.NumberOfDimensions == 0) {
                s.Append($" {pHost[Size.BaseOffset]}");
                return s;
            }

            int element;
            bool firstPage = true;
            while (acc[Size.NumberOfDimensions - 1] < Size[Size.NumberOfDimensions - 1]) {

                // show only two first dimensions at the same time ... 
                // print header
                if (!firstPage && s.Length > 0) s.Append(Environment.NewLine);
                if (Size.NumberOfDimensions > 2) {
                    s.Append("(:,:");
                    for (uint i = 2; i < Size.NumberOfDimensions; i++)
                        s.Append(String.Format(",{0}", acc[i]));
                    s.Append(")");
                }
                // show this for 2 leading dimensions
                for (uint d0 = 0; d0 < Size[0]; d0++) {
                    if (++rowsCount > maxRows) {
                        s.Append(Environment.NewLine + "( ... more rows exist!)");
                        return s;
                    }
                    if (s.Length > 0) s.Append(Environment.NewLine);
                    acc[0] = d0;
                    uint curLineLength = 0;
                    for (uint d1 = 0; d1 < Size[1]; d1++) {
                        acc[1] = d1;
                        element = pHost[Size.GetSeqIndex(acc, Size.NumberOfDimensions)];
                        sElement = element.ToString();
                        sElement = sElement.PadLeft(elemLength);
                        curLineLength += (uint)sElement.Length;
                        if (curLineLength > maxCharsPerRow - " ...".Length) {
                            s.Append(" ...");
                            break;
                        } else {
                            s.Append(sElement);
                        }
                    }
                }
                // increase higher dimension
                firstPage = false;
                uint d = 2;
                while (d <= Size.NumberOfDimensions - 1) {
                    acc[d]++;
                    if (acc[d] < Size[d]) break;
                    acc[d] = 0;
                    d++;
                }
                if (d >= Size.NumberOfDimensions) break;
            }
            return s;
        }

#endregion HYCALPER AUTO GENERATED CODE

        
        private unsafe StringBuilder ToStringString(uint maxRows, uint maxCharsPerRow, bool includeType = true, bool includeSize = true) {

            StringBuilder s = new StringBuilder(500);
            if (includeType) {
                s.Append($"<{typeof(T).Name}>");
            }
            if (includeSize) {
                if (s.Length > 0) s.Append(" ");
                s.Append($"{Size.ToString()}");
            }
            if (Size.NumberOfElements == 0) {
                if (s.Length > 0) s.Append(" ");
                s.Append("[empty]");
                return s;
            }

            uint* acc = stackalloc uint[Math.Max((int)Size.NumberOfDimensions, 2)];
            for (int i = Math.Max((int)Size.NumberOfDimensions, 2); i-- > 0;) acc[i] = 0;

            String sElement = null;
            uint rowsCount = 0;

            var hostArr = (Handles[0] as ManagedHostHandle<string>).HostArray;
            // scalar (numpy)?
            if (Size.NumberOfDimensions == 0) {
                var element = hostArr[Size.BaseOffset];
                if (element == null) {
                    sElement = "[null]";
                } else if (element.Length > 25) {
                    sElement = element.Substring(0, 24) + "â€¦";
                } else {
                    sElement = element;
                }
                s.Append($" {sElement}");
                return s;
            }

            bool firstPage = true;
            while (acc[Size.NumberOfDimensions - 1] < Size[Size.NumberOfDimensions - 1]) {

                // show only two first dimensions at the same time ... 
                // print header
                if (!firstPage && s.Length > 0) s.Append(Environment.NewLine);
                uint actualWidth = Math.Min(maxCharsPerRow, (uint)Size[1] * 2) + 4; // '+ 4' -> ugly heuristic!
                if (Size.NumberOfDimensions > 2) {
                    s.Append("(:,:");
                    for (uint i = 2; i < Size.NumberOfDimensions; i++)
                        s.Append(String.Format(",{0}", acc[i]));
                    s.Append(")");
                    s.Append(new string(' ', (int)(Math.Max(0, actualWidth - (5 + (Size.NumberOfDimensions - 2) * 2)))));
                } else {
                    // insert empty line to force sufficient data tips width in Visual Studio editors.
                    s.Append(new string(' ', (int)actualWidth));
                }
                // show this for 2 leading dimensions
                for (uint d0 = 0; d0 < Size[0]; d0++) {
                    if (++rowsCount > maxRows) {
                        s.Append(Environment.NewLine + "(â€¦ more rows exist!)");
                        return s;
                    }
                    if (s.Length > 0) s.Append(Environment.NewLine);
                    acc[0] = d0;
                    uint curLineLength = 0;
                    for (uint d1 = 0; d1 < Size[1]; d1++) {
                        acc[1] = d1;
                        var element = hostArr[Size.GetSeqIndex(acc, Size.NumberOfDimensions)];
                        if (element == null) {
                            sElement = "[null]".PadLeft(15); 
                        } else if (element.Length > 13) {
                            sElement = "  " + element.Substring(0, 12) + "â€¦"; 
                        } else {
                            sElement = element.PadLeft(15); 
                        }
                        curLineLength += (uint)sElement.Length;
                        if (curLineLength > maxCharsPerRow - " â€¦".Length) {
                            s.Append(" â€¦");
                            break;
                        } else {
                            s.Append(sElement);
                        }
                    }
                }
                // increase higher dimension
                firstPage = false;
                uint d = 2;
                while (d <= Size.NumberOfDimensions - 1) {
                    acc[d]++;
                    if (acc[d] < Size[d]) break;
                    acc[d] = 0;
                    d++;
                }
                if (d >= Size.NumberOfDimensions) break;
            }
            return s;
        }
        
        private unsafe IEnumerable<string> ToStringBaseArray(uint maxRows, uint maxCharsPerRow, bool includeType = true, bool includeSize = true) {

            var ret = new List<string>(100); 
            StringBuilder s = new StringBuilder(500);
            if (includeType) {
                s.Append($"<{typeof(T).Name}>");
            }
            if (includeSize) {
                if (s.Length > 0) s.Append(" ");
                s.Append($"{Size.ToString()}");
            }
            if (Size.NumberOfElements == 0) {
                if (s.Length > 0) s.Append(" ");
                s.Append("[empty]");
                ret.Add(s.ToString()); 
                return ret;
            }
            ret.Add(s.ToString()); s.Clear(); 

            uint* acc = stackalloc uint[Math.Max((int)Size.NumberOfDimensions, 2)];
            for (int i = Math.Max((int)Size.NumberOfDimensions, 2); i-- > 0;) acc[i] = 0;

            string sElement = null;
            uint rowsCount = 0;

            var hostArr = (Handles[0] as ManagedHostHandle<IStorage>).HostArray;
            // scalar (numpy)?
            if (Size.NumberOfDimensions == 0) {
                var element = hostArr[Size.BaseOffset].GetBaseArrayClone();
                if (element == null) {
                    sElement = "{null}";
                } else {
                    sElement = element.ShortInfo(true, true, true, false, includeDevice: false);
                }
                s.Append($" {sElement}");
                ret.Add(s.ToString()); s.Clear();
                return ret; 
            }

            bool firstPage = true;
            while (acc[Size.NumberOfDimensions - 1] < Size[Size.NumberOfDimensions - 1]) {

                // show only two first dimensions at the same time ... 
                // print header
                if (!firstPage && s.Length > 0) {
                    ret.Add(s.ToString()); s.Clear();
                }
                uint actualWidth = Math.Min(maxCharsPerRow, (uint)Size[1] * 2) + 4; // '+ 4' -> ugly heuristic!
                if (Size.NumberOfDimensions > 2) {
                    s.Append("(:,:");
                    for (uint i = 2; i < Size.NumberOfDimensions; i++)
                        s.Append(String.Format(",{0}", acc[i]));
                    s.Append(")");
                    s.Append(new string(' ', (int)(Math.Max(0, (int)actualWidth - (5 + (Size.NumberOfDimensions - 2) * 2)))));
                //} else {
                //    // insert empty line to force sufficient data tips width in Visual Studio editors.
                //    s.Append(new string(' ', (int)actualWidth));
                }
                // show this for 2 leading dimensions
                for (uint d0 = 0; d0 < Size[0]; d0++) {
                    if (++rowsCount > maxRows) {
                        ret.Add(s.ToString()); s.Clear();
                        ret.Add("(â€¦ more rows exist!)");
                        return ret;
                    }
                    if (s.Length > 0) {
                        ret.Add(s.ToString()); s.Clear();
                    }
                    acc[0] = d0;
                    uint curLineLength = 0;
                    for (uint d1 = 0; d1 < Size[1]; d1++) {
                        acc[1] = d1;
                        var element = hostArr[Size.GetSeqIndex(acc, Size.NumberOfDimensions)]?.GetBaseArrayClone()?.ShortInfo(true, true, true, false, includeDevice: false);
                        if (element == null) {
                            sElement = "{null}".PadLeft(25); 
                        } else {
                            sElement = $"{{{element}}}".PadLeft(25); 
                        }
                        curLineLength += (uint)sElement.Length;
                        if (curLineLength > maxCharsPerRow - " â€¦".Length) {
                            s.Append(" â€¦");
                            break;
                        } else {
                            s.Append(sElement);
                        }
                    }
                }
                if (s.Length > 0) {
                    ret.Add(s.ToString()); s.Clear();
                }

                // increase higher dimension
                firstPage = false;
                uint d = 2;
                while (d <= Size.NumberOfDimensions - 1) {
                    acc[d]++;
                    if (acc[d] < Size[d]) break;
                    acc[d] = 0;
                    d++;
                }
                if (d >= Size.NumberOfDimensions) break;
            }
            return ret;
        }
        
    }
}
