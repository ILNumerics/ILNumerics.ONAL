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
#pragma warning disable CS1591

using System.Diagnostics;

namespace ILNumerics.F2NET.Formatting {
    
    public abstract class FormatItem {

        public int RepeatFactor { get; set; }
        protected FormatItemList m_parent; 

        /// <summary>
        ///  The format item list hosting this format item.
        /// </summary>
        public virtual FormatItemList Parent {
            get {
                return m_parent;
            }
            internal set {
                m_parent = value; 
            }
        }

        /// <summary>
        /// Flag determining if this format item is the result of reverting the list of format items after reaching the end of the original list. 
        /// </summary>
        public bool Reverted { get; set; }

        protected FormatItem(int repeatFactor = 1, bool reverted = false) {
            RepeatFactor = repeatFactor;
            Reverted = reverted; 
        }

        protected FormatItem(FormatItem source) {
            this.RepeatFactor = source.RepeatFactor;
            this.Reverted = source.Reverted;
            this.Parent = source.Parent; 
        }

        public abstract FormatItem Clone();

    }
}