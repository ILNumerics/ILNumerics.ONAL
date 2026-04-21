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
using ILNumerics.Core.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics {

    /// <summary>
    /// This class supports the <see cref="Globals.full"/> specifier in indexing expressions.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay(": full")]
    public abstract class FullDimSpec : DimSpec {

        public new sealed class Internal : FullDimSpec, ICacheable<Internal> {

            private Internal m_previous; 
            //private int m_deletionMark;
            public Internal() { }
            //ref int ICacheable<Internal>.DeletionMark => ref m_deletionMark;
            ref Internal ICacheable<Internal>.Previous { get { return ref m_previous; }  }
        }

        /// <summary>
        /// Creates a new full dimension specifier.
        /// </summary>
        protected FullDimSpec() {
            m_isSingleIndex = false;
            m_isSlice = false; 
        }

        /// <summary>
        /// Create uninitialized dimension specifier.
        /// </summary>
        /// <returns>Uninitialized dimension specifier.</returns>
        internal new static FullDimSpec Create() {
            return InMemoryCache<Internal>.Retrieve();
        }

        internal override void Evaluate(long lstElementIdx) {
            m_start = 0; // Math.Max(lstElementIdx,0);
            m_step = 1;
            m_end = lstElementIdx;
            m_cur = -1;
            m_lastElementIDX = lstElementIdx; 
        }

        internal override void EvaluateLeft(long lastElementIdx, ref bool expand) {
            Evaluate(lastElementIdx);
        }

        /// <summary>
        /// Disposes this dim spec object. After disposing don't use the object anymore!
        /// </summary>
        public override void Dispose() {
            InMemoryCache<Internal>.Store(this as Internal); 
        }

    }
}
