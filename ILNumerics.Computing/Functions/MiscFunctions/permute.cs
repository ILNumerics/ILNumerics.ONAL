using ILNumerics.Core.Arrays;
using ILNumerics.Core.Global;
using ILNumerics.Core.StorageLayer;
using System;

namespace ILNumerics.Core.Functions.Builtin {
    internal static partial class MathInternal {

        #region permute

        /// <summary>
        /// Reorder dimensions of n-dimensional array <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input array</param>
        /// <param name="dim">New dimension order. The order of the zero-based indices of the original dimensions specify the target arrays shape.</param>
        /// <returns>Array with reordered dimensions.</returns>
        /// <remarks><paramref name="A"/> will not change. A new array is created, having 
        /// the dimensions rearranged in the order specified by <paramref name="dim"/>.
        /// <para>The length of <paramref name="dim"/> must match the number of dimensions in A: <c>A.S.NumberOfDimensions == dim.Length</c>.</para>
        /// <para>In version 5 <see cref="permute{T}(BaseArray{T}, InArray{long})"/> became more efficient since no 
        /// elements must be copied for a permutation of the dimensions. The resulting array may not expose any common 
        /// storage layout, though. Thus, subsequent operations handling the result may have to perform a copy nevertheless.</para>
        /// </remarks>
        /// <exception cref="ArgumentException"> If the number of elements in <paramref name="A"/> and the number of elements for the 
        /// new dimensions specified by <paramref name="dim"/> do not match or if any index of a dimension from <paramref name="A"/> is 
        /// missing in list of elements in <paramref name="dim"/>.</exception>
        internal unsafe static Array<T> permute<T>(BaseArray<T> A, InArray<uint> dim) {
            using (Scope.Enter()) {
                if (isnull(A)) {
                    throw new ArgumentNullException("A cannot be null.");
                }
                using var _1 = ReaderLock.Create(A as ConcreteArray<T, Array<T>, InArray<T>, OutArray<T>, Array<T>, Storage<T>>, out var storage);
                Array<uint> _dim = dim; 
                if (isnullorempty(_dim) || _dim.S.NumberOfElements != storage.S.NumberOfDimensions) {
                    throw new ArgumentException("'_dim' must specify the order of all dimensions in A uniquely. Its length must correspond to the number of dimensions in A.");
                }
                // check if all dimension indices are in _dim, exactly once
                Array<uint> dimSorted = sort(_dim);
                if ((bool)(dimSorted.GetValue(0) != 0) || (bool)(anyall(diff(dimSorted) != 1))) {
                    throw new ArgumentException("'_dim' must specify the order of all dimensions in A uniquely. Its length must correspond to the number of dimensions in A.");
                }
                var n = storage.S.NumberOfDimensions;
                var ret = Storage<T>.Create(storage.Handles, storage.Size);
                var retBSD = ret.S.GetBSD(true);
                var aBSD = storage.S.GetBSD(false);
                var ndims = storage.S.NumberOfDimensions;
                for (int i = 0; i < ndims; i++) {
                    var d = _dim.GetValue(i);
                    retBSD[3 + i] = aBSD[3 + d];
                    retBSD[3 + ndims + i] = aBSD[3 + ndims + d];
                }
                return ret.RetArray;
            }
        }
        /// <summary>
        /// Reorder dimensions of n-dimensional array <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input array</param>
        /// <param name="dim">New dimension order. The order of the zero-based indices of the original dimensions specify the target arrays shape.</param>
        /// <returns>Array with reordered dimensions.</returns>
        /// <remarks><paramref name="A"/> will not change. A new array is created, having 
        /// the dimensions rearranged in the order specified by <paramref name="dim"/>.
        /// <para>The length of <paramref name="dim"/> must match the number of dimensions in A: <c>A.S.NumberOfDimensions == dim.Length</c>.</para>
        /// <para>From version 5 <see cref="permute{T}(BaseArray{T}, InArray{long})"/> is much more efficient since no elements must 
        /// be copied for a permutation of the dimensions.</para>
        /// </remarks>
        /// <exception cref="ArgumentException"> If the number of elements in <paramref name="A"/> and the number of elements for the 
        /// new dimensions specified by <paramref name="dim"/> do not match or if any index of a dimension from <paramref name="A"/> is 
        /// missing in list of elements in <paramref name="dim"/>.</exception>
        internal unsafe static Array<T> permute<T>(BaseArray<T> A, InArray<long> dim) {
            using (Scope.Enter(dim)) {
                if (isnull(A)) {
                    throw new ArgumentException("A cannot be null.");
                }
                using var _1 = ReaderLock.Create(A as ConcreteArray<T, Array<T>, InArray<T>, OutArray<T>, Array<T>, Storage<T>>, out var storage);
                Array<long> _dim = dim;
                if (isnullorempty(_dim) || _dim.S.NumberOfElements != storage.S.NumberOfDimensions) {
                    throw new ArgumentException("'_dim' must specify the order of all dimensions in A uniquely. Its length must correspond to the number of dimensions in A.");
                }
                // check if all dimension indices are in _dim, exactly once
                Array<long> dimSorted = sort(_dim);
                if ((bool)(dimSorted.GetValue(0) != 0) || (bool)(anyall(diff(dimSorted) != 1))) {
                    throw new ArgumentException("'_dim' must specify the order of all dimensions in A uniquely. Its length must correspond to the number of dimensions in A.");
                }
                uint n = storage.S.NumberOfDimensions;
                var ret = Storage<T>.Create(storage.Handles, storage.Size);
                var retBSD = ret.S.GetBSD(true);
                var aBSD = storage.S.GetBSD(false);
                var ndims = storage.S.NumberOfDimensions;
                for (int i = 0; i < ndims; i++) {
                    var d = _dim.GetValue(i);
                    retBSD[3 + i] = aBSD[3 + d];
                    retBSD[3 + ndims + i] = aBSD[3 + ndims + d];
                }
                return ret.RetArray;
            }
        }

        #endregion

    }
}
