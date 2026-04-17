using System;
using System.Collections.Generic;
using System.Text;
using ILNumerics;

namespace ILNumerics.Core.Functions.Builtin {
    internal static partial class MathInternal
    {

        /// <summary>
        /// Tests if <paramref name="A"/> is null (NULL or not assigned) or has no elements.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <returns>True if <paramref name="A"/> is null or empty, false otherwise.</returns>
        internal static bool isnullorempty(BaseArray A) {
            if (object.Equals(A, null)) {
                return true;
            }
            return A.Size.NumberOfElements < 1;  // releases if A is RetT
        }

    }
}
