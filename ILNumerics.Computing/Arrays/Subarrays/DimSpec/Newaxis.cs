using ILNumerics.Core.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics {
    /// <summary>
    /// This class supports the <see cref="Globals.newaxis"/> keywords in numpy index expressions.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("newaxis")]
    public sealed class NewaxisSpec : DimSpec {

        internal NewaxisSpec() { }

        internal override void Evaluate(long dimLastIdx) {
            if (Settings.ArrayStyle != ArrayStyles.numpy) {
                throw new ArgumentException($"The 'newaxis' dimension specifier is only supported in numpy mode. Current 'Settings.ArrayStyle' = {Settings.ArrayStyle}.");
            }
        }
        internal override void EvaluateLeft(long lastElementIdx, ref bool expand) {
            if (Settings.ArrayStyle != ArrayStyles.numpy) {
                throw new ArgumentException($"The 'newaxis' dimension specifier is only supported in numpy mode. Current 'Settings.ArrayStyle' = {Settings.ArrayStyle}."); 
            }
        }

        public override void Dispose() {
            // nothing to do here. Don't cache this! Immutable object, reuses singleton instance.
        }

    }
}
