using ILNumerics.Core.Arrays;
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.Functions.Builtin {
    internal static partial class MathInternal {

        /// <summary>
        /// Determine, if the array A is null (NULL, not assigned).
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <returns>True, if the array is null, false otherwise</returns>
        /// <remarks>This function is similar to <see cref="Object.Equals(object)"/>. The difference is that memory management is performed on A.</remarks>
        internal static bool isnull(BaseArray A) {
            var ret = ReferenceEquals(A, null);
            return ret; 
        }
    }
}
