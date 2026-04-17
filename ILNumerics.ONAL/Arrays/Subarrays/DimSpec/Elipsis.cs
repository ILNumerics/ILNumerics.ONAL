using ILNumerics.Core.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics {

    /// <summary>
    /// This class supports the <see cref="Globals.ellipsis"/> keyword in indexing operations. 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("... ellipsis")]
    public sealed class EllipsisSpec : DimSpec {

        internal EllipsisSpec() { }

        /// <summary>
        /// Disposes this dim spec object. After disposing don't use the object anymore!
        /// </summary>
        public override void Dispose() {
            // nothing to do here. Don't cache this! This object is immutable. All occurences
            // use the same, singleton instance. 
        }
    }
}
