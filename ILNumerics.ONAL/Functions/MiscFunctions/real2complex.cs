using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.Functions.Builtin {
    internal static partial class MathInternal {

        /// <summary>
        /// Creates a complex array from a real array. This alias for ccomplex(A,0) is now deprecated.
        /// </summary>
        /// <param name="A">Real array A.</param>
        [Obsolete("Replace real2complex(A) with: ccomplex(A,0)!")]
        internal static Array<complex> real2complex(InArray<double> A) {
            return ccomplex(A, 0);
        }
        /// <summary>
        /// Creates a complex array from a real array. This alias for ccomplex(A,0) is now deprecated.
        /// </summary>
        /// <param name="A">Real array A.</param>
        [Obsolete("Replace real2complex(A) with: ccomplex(A,0)!")]
        internal static Array<fcomplex> real2complex(InArray<float> A) {
            return ccomplex(A, 0);
        }

    }
}
