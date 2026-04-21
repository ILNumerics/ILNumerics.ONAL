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
        /// Computes dimensional indices from squential indices. 
        /// </summary>
        /// <param name="A">The input array providing the size, strides and element type to compute subscript indices for.</param>
        /// <param name="indices">Sequential ('linear') indices into <paramref name="A"/>. n elements of arbitrary shape.</param>
        /// <param name="nrOutDims">[Optional] Number of subscript dimensions. Default: (-1) <paramref name="A"/>.S.NumberOfDimensions.</param>
        /// <returns>A matrix [m, n], where m = <c><paramref name="indices"/>.S.NumberOfElements</c> and n = <c>A.S.NumberOfDimensions</c>. Indices 
        /// for each dimension are stored in columns.</returns>
        /// <remarks><para>This function converts sequential element indices into subscript indices. Subscript indices specify the position of an 
        /// element in <paramref name="A"/> by giving the position of the element in each dimension individually. Hence, n subscript indices are 
        /// required to describe the element position in <paramref name="A"/>. In contrast to that, sequential indices specify the position of an 
        /// element in <paramref name="A"/> by a single sequential index, where all elements are considered to be 'lined-up'. The sequential index 
        /// is than simply the index of the element in the line.</para>
        /// <para>The (virtual) lining-up of the elements in <paramref name="A"/> is performed in column major order. Note, 
        /// that the order considered by <see cref="ind2sub{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, InArray{long}, int)"/> is not related to the actual order
        /// of the elements as they are stored in memory.</para>
        /// <para>If <paramref name="indices"/> is not a vector its elements are read in column major order.</para>
        /// <para>If the optional parameter <paramref name="nrOutDims"/> is positive it specifies the number of output subscript dimensions to compute. 
        /// This number can differ from the actual number of dimensions in <paramref name="A"/>. If <paramref name="nrOutDims"/> is smaller than the 
        /// number of dimensions in <paramref name="A"/> unspecified trailing dimensions of <paramref name="A"/> are virtually merged and the last 
        /// column of subscript indices holds the indices into those merged dimension. Note, that in order to use these indices to address elements 
        /// of <paramref name="A"/> subarray indexing must be performed in <see cref="Settings.ArrayStyle"/> = <see cref="ArrayStyles.ILNumericsV4"/>.</para>
        /// <para>If <paramref name="nrOutDims"/> is larger than <c><paramref name="A"/>.S.NumberOfDimensions</c> then subscripts in columns 
        /// corresponding to virtual dimensions of <paramref name="A"/> will be '0'.</para>
        /// <para>The matrix returned gives the subscripts for each element index in <paramref name="indices"/> in rows. The matrix stores m rows of 
        /// subscripts, corresponding to m elements in <paramref name="indices"/>.</para>
        /// <para><paramref name="indices"/> can contain values addressing non-existing elements in <paramref name="A"/> (indices out-of-range). 
        /// These indices will produce out-of-range subscript indices also. In <see cref="ArrayStyles.ILNumericsV4"/> they may be used to grow the 
        /// size of an array in a left-side index expression. <see cref="ind2sub{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, InArray{long}, int)"/>
        /// does not give an error for out-of-range elements nor for negative values in <paramref name="indices"/>! The special meaning of latter (i.e.: 
        /// counting from the end) is not considered here.</para>
        /// <para><see cref="sub2ind{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, InArray{long})"/>
        /// and <see cref="ind2sub{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, InArray{long}, int)"/>
        /// are equivalent operations, inverting the results of the respective other function. Combining both functions creates a roundtrip and gives 
        /// the original data.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="A"/> has less than 1 dimension.</exception>
        /// <exception cref="ArgumentNullException"> if <paramref name="A"/> or <paramref name="indices"/> are null.</exception>
        internal static Array<long> ind2sub<T, LocalT, InT, OutT, RetT, StorageT>(
            ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A, InArray<long> indices, int nrOutDims = -1)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {

                if (Equals(A, null)) {
                    throw new ArgumentNullException(nameof(A));
                }
                if (object.Equals(indices, null)) {
                    throw new ArgumentNullException(nameof(indices));
                }
                
                using var _1 = ReaderLock.Create(A, out var storage);

                if (storage.S.NumberOfDimensions < 1) {
                    throw new ArgumentException($"A must have at least 1 dimension. Found: {A.S}.");
                }
                if (nrOutDims < 1) {
                    nrOutDims = (int)storage.S.NumberOfDimensions;
                }
                Array<long> _indices = indices; 

                if (_indices.IsEmpty) {
                    return empty<long>(0, nrOutDims);
                }
                Array<long> ret = zeros<long>(_indices.S.NumberOfElements, nrOutDims);
                Array<long> ind = _indices.Reshape(_indices.S.NumberOfElements, StorageOrders.ColumnMajor);
                long i = 0; 
                for (; i < nrOutDims - 1; i++) {
                    ret[full, i] = ind % storage.S[i];
                    ind.a = divide(ind, storage.S[i]); // don't use '/' here! it saturates / rounds via double computations  
                }
                for (; i < nrOutDims; i++) {
                    var mergedDimLen = storage.S.GetLastDimIdxForMLSubarray((uint)i) + 1; 
                    ret[full, i] = ind % mergedDimLen;
                }
                return ret; 
            }
        }

        /// <summary>
        /// Computes dimensional indices from squential indices. 
        /// </summary>
        /// <param name="A">The input array providing the size, strides and element type to compute subscript indices for.</param>
        /// <param name="indices">Sequential ('linear') indices into <paramref name="A"/>. n elements of arbitrary shape.</param>
        /// <param name="nrOutDims">[Optional] Number of subscript dimensions. Default: (-1) <paramref name="A"/>.S.NumberOfDimensions.</param>
        /// <returns>A matrix [m, n], where m = <c><paramref name="indices"/>.S.NumberOfElements</c> and n = <c>A.S.NumberOfDimensions</c>. Indices 
        /// for each dimension are stored in columns.</returns>
        /// <remarks><para>This function converts sequential element indices into subscript indices. Subscript indices specify the position of an 
        /// element in <paramref name="A"/> by giving the position of the element in each dimension individually. Hence, n subscript indices are 
        /// required to describe the element position in <paramref name="A"/>. In contrast to that, sequential indices specify the position of an 
        /// element in <paramref name="A"/> by a single sequential index, where all elements are considered to be 'lined-up'. The sequential index 
        /// is than simply the index of the element in the line.</para>
        /// <para>The (virtual) lining-up of the elements in <paramref name="A"/> is performed in column major order. Note, 
        /// that the order considered by <see cref="ind2sub{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, InArray{long}, int)"/> is not related to the actual order
        /// of the elements as they are stored in memory.</para>
        /// <para>If <paramref name="indices"/> is not a vector its elements are read in column major order.</para>
        /// <para>If the optional parameter <paramref name="nrOutDims"/> is positive it specifies the number of output subscript dimensions to compute. 
        /// This number can differ from the actual number of dimensions in <paramref name="A"/>. If <paramref name="nrOutDims"/> is smaller than the 
        /// number of dimensions in <paramref name="A"/> unspecified trailing dimensions of <paramref name="A"/> are virtually merged and the last 
        /// column of subscript indices holds the indices into those merged dimension. Note, that in order to use these indices to address elements 
        /// of <paramref name="A"/> subarray indexing must be performed in <see cref="Settings.ArrayStyle"/> = <see cref="ArrayStyles.ILNumericsV4"/>.</para>
        /// <para>If <paramref name="nrOutDims"/> is larger than <c><paramref name="A"/>.S.NumberOfDimensions</c> then subscripts in columns 
        /// corresponding to virtual dimensions of <paramref name="A"/> will be '0'.</para>
        /// <para>The matrix returned gives the subscripts for each element index in <paramref name="indices"/> in rows. The matrix stores m rows of 
        /// subscripts, corresponding to m elements in <paramref name="indices"/>.</para>
        /// <para><paramref name="indices"/> can contain values addressing non-existing elements in <paramref name="A"/> (indices out-of-range). 
        /// These indices will produce out-of-range subscript indices also. In <see cref="ArrayStyles.ILNumericsV4"/> they may be used to grow the 
        /// size of an array in a left-side index expression. <see cref="ind2sub{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, InArray{long}, int)"/>
        /// does not give an error for out-of-range elements nor for negative values in <paramref name="indices"/>! The special meaning of latter (i.e.: 
        /// counting from the end) is not considered here.</para>
        /// <para><see cref="sub2ind{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, InArray{long})"/>
        /// and <see cref="ind2sub{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, InArray{long}, int)"/>
        /// are equivalent operations, inverting the results of the respective other function. Combining both functions creates a roundtrip and gives 
        /// the original data.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="A"/> has less than 1 dimension.</exception>
        /// <exception cref="ArgumentNullException"> if <paramref name="A"/> or <paramref name="indices"/> are null.</exception>
        internal static Array<uint> ind2sub<T, LocalT, InT, OutT, RetT, StorageT>(
            ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A, InArray<uint> indices, int nrOutDims = -1)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {

                if (Equals(A, null)) {
                    throw new ArgumentNullException(nameof(A));
                }
                if (object.Equals(indices, null)) {
                    throw new ArgumentNullException(nameof(indices));
                }
                using var _1 = ReaderLock.Create(A, out var storage);

                if (storage.S.NumberOfDimensions < 1) {
                    throw new ArgumentException($"A must have at least 1 dimension. Found: {A.S}.");
                }
                if (nrOutDims < 1) {
                    nrOutDims = (int)storage.S.NumberOfDimensions;
                }
                Array<uint> _indices = indices; 
                if (_indices.IsEmpty) {
                    return empty<uint>(0, nrOutDims);
                }
                Array<uint> ret = zeros<uint>(_indices.S.NumberOfElements, nrOutDims);
                Array<uint> ind = _indices.Reshape(_indices.S.NumberOfElements, StorageOrders.ColumnMajor);
                uint i = 0; 
                for (; i < nrOutDims - 1; i++) {
                    ret[full, i] = ind % (uint)storage.S[i];
                    ind.a = divide(ind, (uint)storage.S[i]); // don't use '/' here! it saturates / rounds via double computations  
                }
                for (; i < nrOutDims; i++) {
                    var mergedDimLen = (uint)storage.S.GetLastDimIdxForMLSubarray(i) + 1; 
                    ret[full, i] = ind % mergedDimLen;
                }

                return ret; 
            }
        }

    }
}
