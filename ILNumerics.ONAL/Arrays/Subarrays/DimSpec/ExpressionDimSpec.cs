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
using ILNumerics.Core.Misc;
using System;
using System.Linq.Expressions;

namespace ILNumerics {

    /// <summary>
    /// This class supports ranges / dimension specifiers defined with 'end' expressions. 
    /// </summary>
    [Serializable]
    public abstract class ExpressionDimSpec : DimSpec {

        public new sealed class Internal : ExpressionDimSpec, ICacheable<Internal> {
            
            private Internal m_previous;
            //private int m_deletionMark;
            public Internal() { }

            //ref int ICacheable<Internal>.DeletionMark => ref m_deletionMark;
            ref Internal ICacheable<Internal>.Previous { get { return ref m_previous;  } }
        }

        #region attributes
        internal ILExpression m_startExpression;
        internal ILExpression m_endExpression; 
        
        [ThreadStatic]
        internal static ExpressionDimSpec s_cache; // internal for testing! 
        #endregion  

        internal ExpressionDimSpec() : base() { }

        /// <summary>
        /// Create uninitialized dimension specifier.
        /// </summary>
        /// <returns>Uninitialized dimension specifier.</returns>
        internal new static ExpressionDimSpec Create() {

            var ret = InMemoryCache<Internal>.Retrieve();
            ret.m_endExpression = null; 
            ret.m_startExpression = null;
            return ret; 
        }
        internal override void Evaluate(long lastElementIdx) {
            if (m_startExpression != null) {
                m_start = ILExpression.Evaluate(m_startExpression.Expression, lastElementIdx); 
            }
            if (m_endExpression != null) {
                m_end = ILExpression.Evaluate(m_endExpression.Expression, lastElementIdx); 
            }
            base.Evaluate(lastElementIdx); 
        }
        internal override void EvaluateLeft(long lastElementIdx, ref bool expand) {
            if (m_startExpression != null) {
                m_start = ILExpression.Evaluate(m_startExpression.Expression, lastElementIdx);
            }
            if (m_endExpression != null) {
                m_end = ILExpression.Evaluate(m_endExpression.Expression, lastElementIdx);
            }
            base.EvaluateLeft(lastElementIdx, ref expand);
        }

        /// <summary>
        /// Disposes this dim spec object. After disposing don't use the object anymore!
        /// </summary>
        public override void Dispose() {
            InMemoryCache<Internal>.Store(this as Internal); 
        }

    }
}
