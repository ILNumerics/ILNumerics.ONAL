using ILNumerics.Core.Arrays;
using ILNumerics.Core.Global;
using ILNumerics.Core.Internal;
using ILNumerics.Core.StorageLayer;
using System;
using System.Security;

namespace ILNumerics.Core.Functions.Builtin {

    internal static partial class numpyInternal {

        /// <summary>
        /// Repeat elements along a flattened array or a specific axis.
        /// </summary>
        /// <typeparam name="T1">Element type of <paramref name="A"/>.</typeparam>
        /// <typeparam name="LocalT">(subtype of <paramref name="A"/>)</typeparam>
        /// <typeparam name="InT">(subtype of <paramref name="A"/>)</typeparam>
        /// <typeparam name="OutT">(subtype of <paramref name="A"/>)</typeparam>
        /// <typeparam name="RetT">(subtype of <paramref name="A"/>)</typeparam>
        /// <typeparam name="StorageT">(subtype of <paramref name="A"/>)</typeparam>
        /// <typeparam name="IndT">Element type of <paramref name="repeats"/>. Must be an integer type.</typeparam>
        /// <param name="A">The array storing the elements to be repeated.</param>
        /// <param name="repeats">Counts for element repetitions.</param>
        /// <param name="axis">[Optional] The working dimension. Default: (null) flatten A and repeat all values along dimension #0.</param>
        /// <remarks>
        /// <para>This function repeats elements of <paramref name="A"/> along a single dimension. By default, where no <paramref name="axis"/> 
        /// is defined <paramref name="A"/> is reshaped to a vector in row-major order and all elements are repeated according to <paramref name="repeats"/>.
        /// Otherwise, if an <paramref name="axis"/> was specified, repetitions are performed along that dimension only. In this case, the array returned
        /// has the same shape as <paramref name="A"/>, except that the working dimension <paramref name="axis"/> is enlarged.</para>
        /// <para>The shape of <paramref name="repeats"/> is ignored. Its values give the counts for each element along the axis <paramref name="axis"/>. 
        /// Values must be numeric, positive integers and are read in row-major order. If <paramref name="repeats"/> has exactly one element 
        /// all elements along the working dimension of <paramref name="A"/> are repeated by the same number. Alternatively, the length of <paramref name="repeats"/> 
        /// must match A.S[axis] to specify individual repetition counts for each element along the working dimension. Thus, if <paramref name="axis"/> is 
        /// null (default) <paramref name="repeats"/> can be a scalar or an array with 'A.S.NumberOfElements == repeats.S.NumberOfElements'.</para>
        /// <para>This function returns a new array and does not alter <paramref name="A"/> or any input parameters.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"> if any of the <paramref name="A"/> or <paramref name="repeats"/> is null</exception>
        /// <exception cref="ArgumentException">
        /// if <paramref name="axis"/> points to a virtual dimension; 
        /// if <paramref name="repeats"/> is not a numeric array, is of a shape which is not broadcastable to the length of the working dimension or contains elements which are not convertible to positive integer values; 
        /// </exception>
        
        internal static unsafe RetT repeat<T1, LocalT, InT, OutT, RetT, StorageT, IndT>(ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> A,
                                         BaseArray<IndT> repeats, uint? axis = null)
            where IndT : struct, IConvertible
            where T1 : struct
            where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {

            if (Equals(repeats, null)) {
                throw new ArgumentNullException(nameof(repeats));
            }
            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            if (!axis.HasValue) {

                // default case: flatten the array
                using var _1 = ReaderLock.Create(A, out var storageA); 
                var minDims = Settings.MinNumberOfArrayDimensions;
                return repeat(storageA.Reshape(storageA.S.NumberOfElements, StorageOrders.RowMajor, minDims, null).RetArray, repeats, (uint)Math.Max((int)minDims - 1, 0));

            }

            using var _2 = ReaderLock.Create(repeats as ConcreteArray<IndT, Array<IndT>, InArray<IndT>, OutArray<IndT>, Array<IndT>, Storage<IndT>>, out var repStorage);
            using var _3 = ReaderLock.Create(A, out var AStorage);

            var axisI = axis.GetValueOrDefault();
            if (axis.GetValueOrDefault() >= AStorage.S.NumberOfDimensions) {
                throw new ArgumentException($"'axis' argument must address a non-virtual dimension (axis < A.S.NumberOfDimensions). Found: {axisI}. A.S.NumberOfDimensions = {AStorage.S.NumberOfDimensions}.");
            }
            if (repStorage.S.NumberOfElements != 1 && repStorage.S.NumberOfElements != AStorage.S[axisI]) {
                throw new ArgumentException($"'repeats' has an invalid size. Expected: 1 or {AStorage.S[axisI]}.");
            }

            var repIt = repStorage.AsRetArray().IndexIterator(-1, StorageOrders.RowMajor).GetEnumerator();
            // determine required output size 
            long repetCount = 0;
            while (repIt.MoveNext()) {
                if (repIt.Current < 0) {
                    throw new ArgumentException($"'repeats' parameter cannot have negative repetition counts.");
                }
                repetCount += repIt.Current;
            }
            if (repStorage.S.NumberOfElements == 1) {
                repetCount *= AStorage.S[axisI];
            }
            // try to create out storage first. If this fails we don't need to reorder A. 
            var ret = Storage<T1>.Create();
            var outNElem = repetCount > 0 ? AStorage.S.NumberOfElements / AStorage.S[axisI] * repetCount : 0;
            ret.Handles[0] = MathInternal.New<T1>((ulong)outNElem);

            if (AStorage.S.StorageOrder != StorageOrders.RowMajor && AStorage.S.NonSingletonDimensions != 1) {
                // to not conflict with thread safety / memory management we continue on a clone of A, which is not 
                // exposed to the public yet. 
                AStorage = (AStorage.Clone() as StorageT).EnsureStorageOrder(StorageOrders.RowMajor, inplace: true);  // refcount: 1
            }
            // configure out storage
            var bsd = ret.S.GetBSD(true);
            var ndims = AStorage.S.NumberOfDimensions;
            long strides = 1;
            long innerBytes = Storage<T1>.SizeOfT;
            long outerCount = 1;
            long wDim = AStorage.S[axisI];
            for (uint i = ndims; i-- > 0;) {
                bsd[3 + i] = (i == axisI ? repetCount : AStorage.S[i]);
                bsd[3 + ndims + i] = bsd[3 + i] == 1 ? 0 : strides;
                strides *= bsd[3 + i];
                if (i < axis) {
                    outerCount *= bsd[3 + i];
                } else if (i > axis) {
                    innerBytes *= bsd[3 + i];
                }
            }
            System.Diagnostics.Debug.Assert(strides == outNElem);
            bsd[0] = ndims;
            bsd[1] = strides;
            bsd[2] = 0;

            // loop over out dims, elemen by element. At each position copy all inner elements, replicate according to repeats
            byte* outP = (byte*)ret.Handles[0].Pointer;
            byte* inP = (byte*)AStorage.Handles[0].Pointer;
            var byteLength = new IntPtr(innerBytes);
            if (repStorage.S.NumberOfElements == 1) {
                // broadcasting repeats
                repIt.Reset();
                repIt.MoveNext();
                long scalarRep = repIt.Current;
                for (int outerDims = 0; outerDims < outerCount; outerDims++) {
                    for (int i = 0; i < wDim; i++) {
                        for (int r = 0; r < scalarRep; r++) {

                            Buffer.MemoryCopy(
                              source: (void*)inP,
                              destination: (void*)outP,
                              sourceBytesToCopy: (long)byteLength,
                              destinationSizeInBytes: (long)byteLength);
                            outP += innerBytes;
                        }
                        inP += innerBytes;
                    }
                }

            } else {
                // repeats is an array
                for (int outerDims = 0; outerDims < outerCount; outerDims++) {
                    repIt.Reset();
                    repIt.MoveNext();
                    for (int i = 0; i < wDim; i++) {
                        for (int r = 0; r < repIt.Current; r++) {

                            Buffer.MemoryCopy(
                              source: (void*)inP,
                              destination: (void*)outP,
                              sourceBytesToCopy: (long)byteLength,
                              destinationSizeInBytes: (long)byteLength);
                            outP += innerBytes;
                        }
#if DEBUG
                        System.Diagnostics.Debug.Assert(repIt.MoveNext() || i > 0, "Invalid length of repeats parameter!");
#else
                        repIt.MoveNext(); 
#endif
                        inP += innerBytes;
                    }
                }
            }
            // we may have to release / reassign Astorage, if we had to reorder A
            if (!ReferenceEquals(AStorage, _3.Storage)) {
                A.Storage.Assign(AStorage, true); 
            }
            return ret.RetArray as RetT;
        }
    }
}
