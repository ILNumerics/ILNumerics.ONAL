using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.Misc {
    /// <summary>
    /// This is used to store chunks left over from intermediate quick sort passes. 
    /// </summary>
    internal struct QSChunkDefinition {

        public long lo;
        public long hi;

        internal static void Set(ref QSChunkDefinition item, long lo, long hi) {
            item.lo = lo;
            item.hi = hi; 
        }

        internal void Get(out long lo, out long hi) {
            lo = this.lo;
            hi = this.hi; 
        }
    }
}
