
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
