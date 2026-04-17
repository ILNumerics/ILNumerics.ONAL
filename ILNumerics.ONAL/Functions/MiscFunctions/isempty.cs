using System;
using System.Collections.Generic;
using System.Text;
using ILNumerics;

namespace ILNumerics.Core.Functions.Builtin {
    internal static partial class MathInternal
    {
        /// <summary>
        /// Tests if <paramref name="A"/> has no elements.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <returns>True if <paramref name="A"/> has no elements, false otherwise.</returns>
        /// <remarks>
        /// <para>This function is an alias for <see cref="BaseArray.IsEmpty"/>.</para>
        /// </remarks>
        /// <seealso cref="ILNumerics.BaseArray.IsEmpty"/>
        /// <seealso cref="Size.NumberOfElements"/>
        /// <seealso cref="isnullorempty(BaseArray)"/>
        internal static bool isempty(BaseArray A) {
            return A.IsEmpty;
        }

    }
}
