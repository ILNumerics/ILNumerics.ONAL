using ILNumerics.Core.Arrays;
using ILNumerics.Core.Global;
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.Functions.Builtin {
    internal static partial class MathInternal {

        /// <summary>
        /// Gives the length of the longest dimension of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <returns>The lengths of the longest dimension as system scalar type.</returns>
        /// <remarks>This function is the same as <see cref="Size.Longest"/>. Memory management is performed on <paramref name="A"/>.
        /// <para>For numpy scalars (0 dimensions) 1 is returned.</para></remarks>
        /// <seealso cref="Size.Longest"/>
        internal static long length(BaseArray A) {
            return A?.Length ?? 0; 
        }
    }
}
