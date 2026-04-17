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
        /// Gives the number of elements of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <returns>The number of elements of <paramref name="A"/> as system scalar type.</returns>
        /// <remarks>This function is the same as <see cref="Size.NumberOfElements"/>. Memory management is performed on <paramref name="A"/>.
        /// <para>For numpy scalars (0 dimensions) 1 is returned.</para></remarks>
        /// <seealso cref="Size.Longest"/>
        internal static long numel(BaseArray A) {
            if (object.Equals(A, null)) return 0;

            // below is only that involved because we want to be thread safe and keep BaseArray API... 
            // Otherwise, we would have used:
            // return A.S.NumberOfElements;  ... :|
            var storage = A.GetIStorage();
            storage.Retain();

            var ret = storage.GetSizeInternal().NumberOfElements;
            storage.Release();
            return ret;
        }
    }
}
