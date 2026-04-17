namespace ILNumerics.Core.Functions.Builtin {
    internal static partial class MathInternal
    {
        /// <summary>
        /// Number of dimensions of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <returns>If <paramref name="A"/> is null: 0. Otherwise returns the number of dimensions of <paramref name="A"/>.</returns>
        /// <remarks>This is an alias for <see cref="Size.NumberOfDimensions"/>.</remarks>
        /// <seealso cref="Size.NumberOfDimensions"/>
        internal static uint ndims(BaseArray A) {
            if (object.Equals(A, null)) return 0;

            // below is only that involved because we want to be thread safe and keep BaseArray API... 
            // Otherwise, we would have used:
            // return A.S.NumberOfDimensions;  ... :|
            
            var storage = A.GetIStorage(); 
            if (storage != null) {
                storage.Retain(); 
            }
            var ret = storage.GetSizeInternal().NumberOfDimensions; 
            storage.Release();
            return ret; 
        }
    }
}
