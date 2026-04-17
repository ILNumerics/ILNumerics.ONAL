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