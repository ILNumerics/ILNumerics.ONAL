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
