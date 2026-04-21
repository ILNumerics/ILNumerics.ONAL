// MIT License
// 
// Copyright (c) 2026 ILNumerics GmbH
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
using System;
using ILNumerics;
using ILNumerics.Core.Arrays;
using ILNumerics.Core.Global;
using ILNumerics.Core.StorageLayer;
using static ILNumerics.Globals;

namespace ILNumerics.Core.Functions.Builtin {

    internal partial class MathInternal {

        /// <summary>
        /// Computes sequential (linear) indices from dimensional indices.
        /// </summary>
        /// <param name="A">The input array providing the size, strides and element type to compute the sequential indices for.</param>
        /// <param name="indices">Dimensional index tuples into <paramref name="A"/> as m elements of n rows. Matrix [m, n].</param>
        /// <returns>Vector [m] with sequential element indices into <paramref name="A"/> according to the subscript indices (rows) from <paramref name="indices"/>.</returns>
        /// <remarks><para>This function converts subscript indices into sequential element indices. Subscript indices specify the position of an 
        /// element in <paramref name="A"/> by giving the position of the element in each dimension individually. Hence, n subscript indices are 
        /// required to describe a single element position in <paramref name="A"/>. In contrast to that, sequential indices specify the position of an 
        /// element in <paramref name="A"/> by a single sequential index, where all elements are considered to be 'lined-up'. The sequential index 
        /// is than simply the index of the element in the line.</para>
        /// <para>The (virtual) lining-up of the elements in <paramref name="A"/> is performed in <i>column major order</i>. Note, 
        /// that the order considered by <see cref="ind2sub{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, InArray{long}, int)"/> 
        /// and by <see cref="sub2ind{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, InArray{long})"/> 
        /// is not related to the actual order of the elements as they are stored in memory.</para>
        /// <para>If <paramref name="indices"/> has fewer columns as dimensions exist in <paramref name="A"/> indices into unspecified dimensions 
        /// are considered 0. The last column of <paramref name="indices"/> may contains values as indices into the <i>merged trailing</i> 
        /// dimensions of <paramref name="A"/>.</para>
        /// <para>The vector returned gives the sequential index for each element specified as a row in <paramref name="indices"/>. A sequential index  
        /// corresponds to the 0-based index of the element after lining-up all elements of <paramref name="A"/>, starting at element 0 and walking 
        /// along the array in column major order.</para>
        /// <para><paramref name="indices"/> can contain values addressing non-existing elements in <paramref name="A"/> (indices out-of-range). 
        /// These indices will produce invalid sequential indices and do not produce an error. Also, no error is generated for negative values 
        /// in <paramref name="indices"/>! The special meaning of latter (i.e.: counting from the end) is not considered here.</para>
        /// <para><see cref="sub2ind{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, InArray{long})"/>
        /// and <see cref="ind2sub{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, InArray{long}, int)"/>
        /// are equivalent operations, inverting the results of the respective other function. Combining both functions creates a roundtrip and gives 
        /// the original data.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="A"/> has less than 1 dimension.</exception>
        /// <exception cref="ArgumentNullException"> if <paramref name="A"/> or <paramref name="indices"/> are null.</exception>
        internal static Array<long> sub2ind<T, LocalT, InT, OutT, RetT, StorageT>(
            ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A, InArray<long> indices)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {

                using var _1 = ReaderLock.Create(A, out var storage, throwOnNullWithMsg: "sub2ind(A, indices): parameter 'A' cannot be null."); 

                if (isnull(indices)) {
                    throw new ArgumentNullException(nameof(indices));
                }
                Array<long> _indices = indices; 

                if (storage.S.NumberOfDimensions < 1) {
                    throw new ArgumentException($"A must have at least 1 dimension. Found: {A.S}.");
                }
                if (_indices.IsEmpty) {
                    return empty<long>(dim0: 0);
                }
                if (!_indices.IsMatrix) {
                    throw new ArgumentException($"_indices argument must be a matrix.");
                }
                Array<long> ret = _indices[full,0];
                long l = A.S[0]; 
                for (long i = 1; i < _indices.S[1]; i++) {
                    ret = ret + l * _indices[full,i];
                    l *= A.S[i]; 
                }

                return ret; 
            }
        }

        /// <summary>
        /// Computes sequential (linear) indices from dimensional indices.
        /// </summary>
        /// <param name="A">The input array providing the size, strides and element type to compute the sequential indices for.</param>
        /// <param name="indices">Dimensional index tuples into <paramref name="A"/> as m elements of n rows. Matrix [m, n].</param>
        /// <returns>Vector [m] with sequential element indices into <paramref name="A"/> according to the subscript indices (rows) from <paramref name="indices"/>.</returns>
        /// <remarks><para>This function converts subscript indices into sequential element indices. Subscript indices specify the position of an 
        /// element in <paramref name="A"/> by giving the position of the element in each dimension individually. Hence, n subscript indices are 
        /// required to describe a single element position in <paramref name="A"/>. In contrast to that, sequential indices specify the position of an 
        /// element in <paramref name="A"/> by a single sequential index, where all elements are considered to be 'lined-up'. The sequential index 
        /// is than simply the index of the element in the line.</para>
        /// <para>The (virtual) lining-up of the elements in <paramref name="A"/> is performed in <i>column major order</i>. Note, 
        /// that the order considered by <see cref="ind2sub{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, InArray{long}, int)"/> 
        /// and by <see cref="sub2ind{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, InArray{long})"/> 
        /// is not related to the actual order of the elements as they are stored in memory.</para>
        /// <para>If <paramref name="indices"/> has fewer columns as dimensions exist in <paramref name="A"/> indices into unspecified dimensions 
        /// are considered 0. The last column of <paramref name="indices"/> may contains values as indices into the <i>merged trailing</i> 
        /// dimensions of <paramref name="A"/>.</para>
        /// <para>The vector returned gives the sequential index for each element specified as a row in <paramref name="indices"/>. A sequential index  
        /// corresponds to the 0-based index of the element after lining-up all elements of <paramref name="A"/>, starting at element 0 and walking 
        /// along the array in column major order.</para>
        /// <para><paramref name="indices"/> can contain values addressing non-existing elements in <paramref name="A"/> (indices out-of-range). 
        /// These indices will produce invalid sequential indices and do not produce an error. Also, no error is generated for negative values 
        /// in <paramref name="indices"/>! The special meaning of latter (i.e.: counting from the end) is not considered here.</para>
        /// <para><see cref="sub2ind{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, InArray{long})"/>
        /// and <see cref="ind2sub{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, InArray{long}, int)"/>
        /// are equivalent operations, inverting the results of the respective other function. Combining both functions creates a roundtrip and gives 
        /// the original data.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="A"/> has less than 1 dimension.</exception>
        /// <exception cref="ArgumentNullException"> if <paramref name="A"/> or <paramref name="indices"/> are null.</exception>
        internal static Array<uint> sub2ind<T, LocalT, InT, OutT, RetT, StorageT>(
            ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A, InArray<uint> indices)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {

                using var _1 = ReaderLock.Create(A, out var storage, throwOnNullWithMsg: "sub2ind(A, indices): parameter 'A' cannot be null.");

                if (isnull(indices)) {
                    throw new ArgumentNullException(nameof(indices));
                }
                Array<uint> _indices = indices;

                if (storage.S.NumberOfDimensions < 1) {
                    throw new ArgumentException($"A must have at least 1 dimension. Found: {A.S}.");
                }
                if (_indices.IsEmpty) {
                    return empty<uint>(dim0: 0);
                }
                if (!_indices.IsMatrix) {
                    throw new ArgumentException($"_indices argument must be a matrix.");
                }
                Array<uint> ret = _indices[full, 0];
                uint l = (uint)A.S[0];
                for (uint i = 1; i < _indices.S[1]; i++) {
                    ret = ret + l * _indices[full, i];
                    l *= (uint)A.S[i];
                }

                return ret;
            }
        }

    }
}
