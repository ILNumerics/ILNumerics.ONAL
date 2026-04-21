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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
#pragma warning disable CS1591

namespace ILNumerics.F2NET.Formatting {

    /// <summary>
    /// Handling Fortran format expressions. 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// </remarks>
    [DebuggerDisplay("{ToString()}")]
    public class FormatItemList : FormatItem, IEnumerable<FormatItem> {

        public List<FormatItem> List { get; set; }

        public override FormatItemList Parent {
            get {
                return base.Parent;
            }
            internal set {
                foreach (var item in List) {
                    item.Parent = value; 
                }
                base.Parent = value; 
            }
        }
        /// <summary>
        /// Current scaling factor (P editing).
        /// </summary>
        private int ScaleFactor { get; set; } = 0;

        public PlusSignStates PlusSignState { get; set; } = PlusSignStates.S; 
        public FormatItemList(List<FormatItem> items, int? repeatFactor = null, bool reverted = false) : base(repeatFactor ?? 1, reverted) {
            List = items ?? new List<FormatItem>();
            Parent = this; 
        }

        public FormatItemList(string format) {
            List = FormatParser.Parse(format)?.List;
            Parent = this; 
        }

        public override string ToString() {
            return $"{(RepeatFactor != 1 ? RepeatFactor.ToString() : "")}(" + string.Join(",", List.Select(s => s.ToString())) + ")";
        }

        /// <summary>
        /// Gives an iterater over the individual format items in this list. CAUTION!! This will iterate forever! It must be stopped from the outside (see Fortran spec). 
        /// </summary>
        /// <returns>Enumerator over format items in this list.</returns>
        /// <example><![CDATA[
        /// foreach (var item in FormatParser.Parse("(1x,A, /4(2I3), 2E15 .4E2, 'H')")) {
        ///     Console.WriteLine($"FormatItem: {item}");
        ///     if (item.Reverted) break; 
        /// }
        /// Output: 
        /// FormatItem: 1X
        /// FormatItem: 1A
        /// FormatItem: 1/
        /// FormatItem: 1I3
        /// FormatItem: 1I3
        /// FormatItem: 1I3
        /// FormatItem: 1I3
        /// FormatItem: 1I3
        /// FormatItem: 1I3
        /// FormatItem: 1I3
        /// FormatItem: 1I3
        /// FormatItem: 1E15.4E2
        /// FormatItem: 1E15.4E2
        /// FormatItem: 'H'
        /// FormatItem: 1/]]></example>
        public IEnumerator<FormatItem> GetEnumerator() {

            if (List.Count == 1 && (List[0] as DataEditDescriptor)?.Manner == EditDescriptorManner.Asterisc) {
                // special case: format '*'
                while (true) {
                    yield return List[0]; 
                }
            }

            Stack<Tuple<int, FormatItemList,int>> stack = new Stack<Tuple<int, FormatItemList, int>>();
            int i = 0;
            var listRepeat = this.RepeatFactor;
            FormatItemList cur = this;
            bool reverted = false; 

            while (true) {

                for (;  listRepeat > 0; listRepeat--) {
                    for (; i < cur.List.Count; i++) {

                        if (cur.List[i] is FormatItemList) {

                            stack.Push(Tuple.Create(i + 1, cur, listRepeat));  // i + 1 => continue with NEXT item afterwards!
                            cur = cur.List[i] as FormatItemList;
                            listRepeat = cur.RepeatFactor; 
                            i = -1;

                        } else {

                            // items returned are non-list only, any repeat factors are expanded
                            var ret = cur.List[i].Clone();
                            ret.RepeatFactor = 1; ret.Reverted = reverted;
                            for (int d = 0; d < cur.List[i].RepeatFactor; d++) {
                                yield return ret;
                            }
                        }
                    }
                    i = 0; 
                }
                if (stack.Count > 0) {
                    var outer = stack.Pop();
                    cur = outer.Item2;
                    i = outer.Item1;
                    listRepeat = outer.Item3;
                    continue; 
                }
                // whole list done, including repetitions. 
                // where to continue (reverting)? 

                // iterate the last or all indefinitely
                /* ISO/IEC 1539 : 1991 (E), 10.3: 
                 * If format control encounters the rightmost parenthesis of a complete format specification and another effective
                input/output list item is not specified, format control terminates. However, if another effective input/output list
                item is specified, the file is positioned in a manner identical to the way it is positioned when a slash edit
                descriptor is processed (10.6.2). Format control then reverts to the beginning of the format item terminated by the
                last preceding right parenthesis. If there is no such preceding right parenthesis, format control reverts to the first
                left parenthesis of the format specification. If any reversion occurs, the reused portion of the format specification
                must contain at least one data edit descriptor. If format control reverts to a parenthesis that is preceded by a
                repeat specification, the repeat specification is reused. Reversion of format control, of itself, has no effect on the
                scale factor (10.6.5.1), the sign control edit descriptors (10.6.4), or the blank interpretation edit descriptors
                (10.6.6).
                */
                cur = List.Last() is FormatItemList ? List.Last() as FormatItemList : this;
                reverted = true;
                listRepeat = cur.RepeatFactor;
                i = 0; 
                // emergency escape
                if (!cur.ContainsTypeOf<DataEditDescriptor>()) {
                    yield break; 
                    // throw new ArgumentException($"The format '{this.ToString()}' is not suited for reversion: missing DataEditDescriptor (see Fortran90 spec: 10.2.1).");
                }
                yield return new ControlEditDescriptor(EditDescriptorManner.Slash, reverted: true);
                // continue from start... 
            }
        }

        private bool ContainsTypeOf<T>() {
            foreach (var d in List) {
                if (d is FormatItemList) {
                    if (!(d as FormatItemList).ContainsTypeOf<T>()) {
                        return false; 
                    } 
                } else if (d is T) {
                    return true; 
                }
            }
            return false; 
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }

        public string AsEscapedCSString() {
            var ret = $"\"{ToString().Replace("\"", "\\\"")}\"";
            return ret; 
        }

        public override FormatItem Clone() {
            return new FormatItemList(this); 
        }

        public void Scale(int factor) {
            ScaleFactor = factor; 
        }

        public int GetScaling() {
            return ScaleFactor; 
        }

        protected FormatItemList(FormatItemList source) : base(source) {
            var list = List.Select(itm => itm.Clone()).ToList();
            List = list;
        }

    }
}