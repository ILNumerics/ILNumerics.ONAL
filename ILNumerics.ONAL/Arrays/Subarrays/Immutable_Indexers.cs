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
using ILNumerics.Core.Global;
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.Arrays {

    /// <summary>
    /// Base class for all mutable array types. Internal use.
    /// </summary>
    /// <typeparam name="T1">The element type.</typeparam>
    /// <typeparam name="LocalT">The local array type of this array type set. </typeparam>
    /// <typeparam name="InT">The input array type of this array type set.</typeparam>
    /// <typeparam name="OutT">The output array type of this array type set.</typeparam>
    /// <typeparam name="RetT">The return array type of this array type set.</typeparam>
    /// <typeparam name="StorageT">The storage type of this array type set.</typeparam>
    public abstract partial class Immutable<T1, LocalT, InT, OutT, RetT, StorageT>   // T1 must not read "T" to prevent from conflicts with the .T (transpose) property.
        : ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT>
        where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {

        internal Immutable(StorageT storage) : base(storage) { }

        #region scalar long indexers, read
        /// <summary>
        /// Retrieves the value(s) of the element(s) at the position(s) specified by <paramref name="d0"/>. Deep indexing for cells.
        /// </summary>
        /// <param name="d0">Index into the first dimension or sequential index into the array.</param>
        /// <returns>Element value wrapped into a new scalar array.</returns>
        /// <remarks><para>This functions retrieves the value of the element at <b>sequential</b> index <paramref name="d0"/>. Note that 
        /// the index provided in <paramref name="d0"/> does not correspond to the storage position of the element to retrieve. This 
        /// would only be true for special cases of storage layout and dimension numbers. For arbitrary storage 
        /// layouts both are likely to be different.</para> 
        /// <para>This indexer is readonly on immutable types. The get accessor returns a scalar array. In order to 
        /// receive the value of the element addressed as system type directly (i.e.: without wrapping it into a new 
        /// array) and as a faster alternative for scalar or mostly-scalar algorithms use 
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, uint)"/> and/or its specific 
        /// overloads for the individiual element types.</para>
        /// </remarks>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Subarray(DimSpec)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the <see cref="Size.NumberOfElements"/>.</exception>
        public RetT this[long d0] {
            get {

                using var _1 = ReaderLock.Create(this, out var storage, releaseRetT: true);
                return storage.Indexer_Get(d0).RetArray;

            }
        }
        /// <summary>
        /// Retrieves the value(s) of the element(s) at the position(s) specified by <paramref name="d0"/> and <paramref name="d1"/>. Deep indexing for cells.
        /// </summary>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value wrapped into a new scalar array.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first two dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions.</para> 
        /// <para>This indexer is readonly on immutable types. The get accessor returns a scalar array. In order to 
        /// receive the value of the element addressed as system type directly (i.e.: without wrapping it into a new 
        /// array) and as a faster alternative for scalar or mostly-scalar algorithms use 
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, uint, uint)"/>.</para>
        /// </remarks>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, uint, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, long, long)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/>.</exception>
        public RetT this[long d0, long d1] {
            get {

                using var _1 = ReaderLock.Create(this, out var storage, releaseRetT: true);
                return storage.Indexer_Get(d0, d1).RetArray;

            }
        }
        /// <summary>
        /// Retrieves the value(s) of the element(s) at the position(s) specified by <paramref name="d0"/> ... <paramref name="d2"/>. Deep indexing for cells.
        /// </summary>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first three dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions.</para> 
        /// </remarks>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the number of elements in the 
        /// first dimension, or the value of <paramref name="d1"/> exceeds the number of elements in the 2nd dimension, 
        /// or the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/>.</exception>
        public RetT this[long d0, long d1, long d2] {
            get {

                using var _1 = ReaderLock.Create(this, out var storage, releaseRetT: true);
                return storage.Indexer_Get(d0, d1, d2).RetArray;

            }
        }
        /// <summary>
        /// Retrieves the value(s) of the element(s) at the position(s) specified by <paramref name="d0"/> ... <paramref name="d3"/>. Deep indexing for cells.
        /// </summary>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first four dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions.</para> 
        /// </remarks>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/>.</exception>
        public RetT this[long d0, long d1, long d2, long d3] {
            get {

                using var _1 = ReaderLock.Create(this, out var storage, releaseRetT: true);
                return storage.Indexer_Get(d0, d1, d2, d3).RetArray;
            
            }
        }
        /// <summary>
        /// Retrieves the value(s) of the element(s) at the position(s) specified by <paramref name="d0"/> ... <paramref name="d4"/>. Deep indexing for cells.
        /// </summary>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first five dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions.</para> 
        /// </remarks>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/>.</exception>
        public RetT this[long d0, long d1, long d2, long d3, long d4] {
            get {

                using var _1 = ReaderLock.Create(this, out var storage, releaseRetT: true);
                return storage.Indexer_Get(d0, d1, d2, d3, d4).RetArray;

            }
        }
        /// <summary>
        /// Retrieves the value(s) of the element(s) at the position(s) specified by <paramref name="d0"/> ... <paramref name="d5"/>. Deep indexing for cells.
        /// </summary>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the first six dimensions. </para>
        /// <para>If the value provided by the last index parameter exceeds the number of elements in its corresponding dimension
        /// the superflous index value is translated into subsequent dimensions.</para> 
        /// </remarks>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/>.</exception>
        public RetT this[long d0, long d1, long d2, long d3, long d4, long d5] {
            get {

                using var _1 = ReaderLock.Create(this, out var storage, releaseRetT: true);
                return storage.Indexer_Get(d0, d1, d2, d3, d4, d5).RetArray;

            }
        }
        /// <summary>
        /// Retrieves the value(s) of the element(s) at the position(s) specified by <paramref name="d0"/> ... <paramref name="d6"/>. Deep indexing for cells.
        /// </summary>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension. </param>
        /// <param name="d6">Index into the sevenths dimension or sequential index into remaining dimensions of the array, if any.</param>
        /// <returns>Element value.</returns>
        /// <remarks><para>This functions retrieves the value of the element as given by the indices into the seven dimensions. </para>
        /// </remarks>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, uint)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if at least one of the values of <paramref name="d0"/>, 
        /// <paramref name="d1"/>, <paramref name="d2"/>, <paramref name="d3"/>, <paramref name="d4"/>, <paramref name="d5"/> exceeds the number of elements in its corresponding dimension, 
        /// or if the computed index according to all index parameters <paramref name="d0"/>, <paramref name="d1"/>,... exceeds the 
        /// <see cref="Size.NumberOfElements"/>.</exception>
        public RetT this[long d0, long d1, long d2, long d3, long d4, long d5, long d6] {
            get {

                using var _1 = ReaderLock.Create(this, out var storage, releaseRetT: true);
                return storage.Indexer_Get(d0, d1, d2, d3, d4, d5, d6).RetArray; 

            }
        }
        #endregion

        #region dimspec indexers, read
        /// <summary>
        /// Gets a subarray from this array.
        /// </summary>
        /// <param name="d0">Range specification(s).</param>
        /// <returns>Subarray according to the range specification(s) in <paramref name="d0"/>.</returns>
        /// <remarks><para>This indexer supports subarray retrieval from immutable arrays (RetArray, Logical etc.). This is 
        /// part of the 'fast' subarray API: the array returned will reference the same memory as this array in a 'lazy, copy on-write' 
        /// scheme.</para></remarks>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Subarray(BaseArray[])"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, BaseArray, BaseArray, BaseArray)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.SetValue{T1, LocalT, InT, OutT, RetT, StorageT}(Mutable{T1, LocalT, InT, OutT, RetT, StorageT}, T1, long, long)"/>
        /// <exception cref="IndexOutOfRangeException">If an index lays outside of the allowed range. The exception message 
        /// will carry more details regarding the cause of this error.</exception>
        public RetT this[DimSpec d0] {
            get {
                return Subarray(d0);
            }
        }
        /// <summary>
        /// Gets a subarray from this array.
        /// </summary>
        /// <param name="d0">Range specification(s) for dimension #0.</param>
        /// <param name="d1">Range specification(s) for dimension #1.</param>
        /// <returns>Subarray according to the range specification(s) in <paramref name="d0"/>, ...</returns>
        /// <remarks><para>This indexer supports subarray retrieval from immutable arrays (RetArray, Logical etc.). This is 
        /// part of the 'fast' subarray API: the array returned will reference the same memory as this array in a 'lazy, copy on-write' 
        /// scheme.</para></remarks>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Subarray(BaseArray[])"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, BaseArray, BaseArray, BaseArray)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.SetValue{T1, LocalT, InT, OutT, RetT, StorageT}(Mutable{T1, LocalT, InT, OutT, RetT, StorageT}, T1, long, long)"/>
        /// <exception cref="IndexOutOfRangeException">If an index lays outside of the allowed range. The exception message 
        /// will carry more details regarding the cause of this error.</exception>
        public RetT this[DimSpec d0, DimSpec d1] {
            get {
                return Subarray(d0, d1);
            }
        }
        /// <summary>
        /// Gets a subarray from this array.
        /// </summary>
        /// <param name="d0">Range specification(s) for dimension #0.</param>
        /// <param name="d1">Range specification(s) for dimension #1.</param>
        /// <param name="d2">Range specification(s) for dimension #2.</param>
        /// <returns>Subarray according to the range specification(s) in <paramref name="d0"/>, ...</returns>
        /// <remarks><para>This indexer supports subarray retrieval from immutable arrays (RetArray, Logical etc.). This is 
        /// part of the 'fast' subarray API: the array returned will reference the same memory as this array in a 'lazy, copy on-write' 
        /// scheme.</para></remarks>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Subarray(BaseArray[])"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, BaseArray, BaseArray, BaseArray)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.SetValue{T1, LocalT, InT, OutT, RetT, StorageT}(Mutable{T1, LocalT, InT, OutT, RetT, StorageT}, T1, long, long)"/>
        /// <exception cref="IndexOutOfRangeException">If an index lays outside of the allowed range. The exception message 
        /// will carry more details regarding the cause of this error.</exception>
        public RetT this[DimSpec d0, DimSpec d1, DimSpec d2] {
            get {
                return Subarray(d0, d1, d2);
            }
        }
        /// <summary>
        /// Gets a subarray from this array.
        /// </summary>
        /// <param name="d0">Range specification(s) for dimension #0.</param>
        /// <param name="d1">Range specification(s) for dimension #1.</param>
        /// <param name="d2">Range specification(s) for dimension #2.</param>
        /// <param name="d3">Range specification(s) for dimension #3.</param>
        /// <returns>Subarray according to the range specification(s) in <paramref name="d0"/>, ...</returns>
        /// <remarks><para>This indexer supports subarray retrieval from immutable arrays (RetArray, Logical etc.). This is 
        /// part of the 'fast' subarray API: the array returned will reference the same memory as this array in a 'lazy, copy on-write' 
        /// scheme.</para></remarks>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Subarray(BaseArray[])"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, BaseArray, BaseArray, BaseArray)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.SetValue{T1, LocalT, InT, OutT, RetT, StorageT}(Mutable{T1, LocalT, InT, OutT, RetT, StorageT}, T1, long, long)"/>
        /// <exception cref="IndexOutOfRangeException">If an index lays outside of the allowed range. The exception message 
        /// will carry more details regarding the cause of this error.</exception>
        public RetT this[DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3] {
            get {
                return Subarray(d0, d1, d2, d3);
            }
        }
        /// <summary>
        /// Gets a subarray from this array.
        /// </summary>
        /// <param name="d0">Range specification(s) for dimension #0.</param>
        /// <param name="d1">Range specification(s) for dimension #1.</param>
        /// <param name="d2">Range specification(s) for dimension #2.</param>
        /// <param name="d3">Range specification(s) for dimension #3.</param>
        /// <param name="d4">Range specification(s) for dimension #4.</param>
        /// <returns>Subarray according to the range specification(s) in <paramref name="d0"/>, ...</returns>
        /// <remarks><para>This indexer supports subarray retrieval from immutable arrays (RetArray, Logical etc.). This is 
        /// part of the 'fast' subarray API: the array returned will reference the same memory as this array in a 'lazy, copy on-write' 
        /// scheme.</para></remarks>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Subarray(BaseArray[])"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, BaseArray, BaseArray, BaseArray)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.SetValue{T1, LocalT, InT, OutT, RetT, StorageT}(Mutable{T1, LocalT, InT, OutT, RetT, StorageT}, T1, long, long)"/>
        /// <exception cref="IndexOutOfRangeException">If an index lays outside of the allowed range. The exception message 
        /// will carry more details regarding the cause of this error.</exception>
        public RetT this[DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3, DimSpec d4] {
            get {
                return Subarray(d0, d1, d2, d3, d4);
            }
        }
        /// <summary>
        /// Gets a subarray from this array.
        /// </summary>
        /// <param name="d0">Range specification(s) for dimension #0.</param>
        /// <param name="d1">Range specification(s) for dimension #1.</param>
        /// <param name="d2">Range specification(s) for dimension #2.</param>
        /// <param name="d3">Range specification(s) for dimension #3.</param>
        /// <param name="d4">Range specification(s) for dimension #4.</param>
        /// <param name="d5">Range specification(s) for dimension #5.</param>
        /// <returns>Subarray according to the range specification(s) in <paramref name="d0"/>, ...</returns>
        /// <remarks><para>This indexer supports subarray retrieval from immutable arrays (RetArray, Logical etc.). This is 
        /// part of the 'fast' subarray API: the array returned will reference the same memory as this array in a 'lazy, copy on-write' 
        /// scheme.</para></remarks>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Subarray(BaseArray[])"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, BaseArray, BaseArray, BaseArray)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.SetValue{T1, LocalT, InT, OutT, RetT, StorageT}(Mutable{T1, LocalT, InT, OutT, RetT, StorageT}, T1, long, long)"/>
        /// <exception cref="IndexOutOfRangeException">If an index lays outside of the allowed range. The exception message 
        /// will carry more details regarding the cause of this error.</exception>
        public RetT this[DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3, DimSpec d4, DimSpec d5] {
            get {
                return Subarray(d0, d1, d2, d3, d4, d5);
            }
        }
        /// <summary>
        /// Gets a subarray from this array.
        /// </summary>
        /// <param name="d0">Range specification(s) for dimension #0.</param>
        /// <param name="d1">Range specification(s) for dimension #1.</param>
        /// <param name="d2">Range specification(s) for dimension #2.</param>
        /// <param name="d3">Range specification(s) for dimension #3.</param>
        /// <param name="d4">Range specification(s) for dimension #4.</param>
        /// <param name="d5">Range specification(s) for dimension #5.</param>
        /// <param name="d6">Range specification(s) for dimension #6.</param>
        /// <returns>Subarray according to the range specification(s) in <paramref name="d0"/>, ...</returns>
        /// <remarks><para>This indexer supports subarray retrieval from immutable arrays (RetArray, Logical etc.). This is 
        /// part of the 'fast' subarray API: the array returned will reference the same memory as this array in a 'lazy, copy on-write' 
        /// scheme.</para></remarks>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Subarray(BaseArray[])"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, BaseArray, BaseArray, BaseArray)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.SetValue{T1, LocalT, InT, OutT, RetT, StorageT}(Mutable{T1, LocalT, InT, OutT, RetT, StorageT}, T1, long, long)"/>
        /// <exception cref="IndexOutOfRangeException">If an index lays outside of the allowed range. The exception message 
        /// will carry more details regarding the cause of this error.</exception>
        public RetT this[DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3, DimSpec d4, DimSpec d5, DimSpec d6] {
            get {
                return Subarray(d0, d1, d2, d3, d4, d5, d6);
            }
        }

        /// <summary>
        /// Gets a subarray from this array.
        /// </summary>
        /// <param name="dims">Array with range specification(s) for dimensions of this array.</param>
        /// <returns>Subarray according to the range specification(s) in <paramref name="dims"/>.</returns>
        /// <remarks><para>This indexer supports subarray retrieval from immutable arrays (RetArray, Logical etc.).</para></remarks>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Subarray(BaseArray[])"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, BaseArray, BaseArray, BaseArray)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.SetValue{T1, LocalT, InT, OutT, RetT, StorageT}(Mutable{T1, LocalT, InT, OutT, RetT, StorageT}, T1, long, long)"/>
        /// <exception cref="IndexOutOfRangeException">If an index lays outside of the allowed range. The exception message 
        /// will carry more details regarding the cause of this error.</exception>
        public RetT this[params DimSpec[] dims] {
            get {
                return Subarray(dims);
            }
        }
        #endregion

        #region BaseArray indexers, read
        /// <summary>
        /// Gets a subarray from this array.
        /// </summary>
        /// <param name="d0">Range specification(s) for dimension #0.</param>
        /// <returns>Subarray according to the range specification(s) in <paramref name="d0"/>.</returns>
        /// <remarks><para>This indexer supports subarray retrieval from immutable arrays (RetArray, Logical etc.). This is 
        /// part of the 'flexible' subarray API: the array returned will reference new memory with copies of the elements from this array.</para></remarks>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Subarray(BaseArray[])"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, BaseArray, BaseArray, BaseArray)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.SetValue{T1, LocalT, InT, OutT, RetT, StorageT}(Mutable{T1, LocalT, InT, OutT, RetT, StorageT}, T1, long, long)"/>
        /// <exception cref="IndexOutOfRangeException">If an index lays outside of the allowed range. The exception message 
        /// will carry more details regarding the cause of this error.</exception>
        public RetT this[BaseArray d0] {
            get {
                return Subarray(d0);
            }
        }
        /// <summary>
        /// Gets a subarray from this array.
        /// </summary>
        /// <param name="d0">Range specification(s) for dimension #0.</param>
        /// <param name="d1">Range specification(s) for dimension #1.</param>
        /// <returns>Subarray according to the range specification(s) in <paramref name="d0"/>,... .</returns>
        /// <remarks><para>This indexer supports subarray retrieval from immutable arrays (RetArray, Logical etc.). This is 
        /// part of the 'flexible' subarray API: the array returned will reference new memory with copies of the elements from this array.</para></remarks>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Subarray(BaseArray[])"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, BaseArray, BaseArray, BaseArray)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.SetValue{T1, LocalT, InT, OutT, RetT, StorageT}(Mutable{T1, LocalT, InT, OutT, RetT, StorageT}, T1, long, long)"/>
        /// <exception cref="IndexOutOfRangeException">If an index lays outside of the allowed range. The exception message 
        /// will carry more details regarding the cause of this error.</exception>
        public RetT this[BaseArray d0, BaseArray d1] {
            get {
                return Subarray(d0, d1);
            }
        }
        /// <summary>
        /// Gets a subarray from this array.
        /// </summary>
        /// <param name="d0">Range specification(s) for dimension #0.</param>
        /// <param name="d1">Range specification(s) for dimension #1.</param>
        /// <param name="d2">Range specification(s) for dimension #2.</param>
        /// <returns>Subarray according to the range specification(s) in <paramref name="d0"/>,... .</returns>
        /// <remarks><para>This indexer supports subarray retrieval from immutable arrays (RetArray, Logical etc.). This is 
        /// part of the 'flexible' subarray API: the array returned will reference new memory with copies of the elements from this array.</para></remarks>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Subarray(BaseArray[])"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, BaseArray, BaseArray, BaseArray)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.SetValue{T1, LocalT, InT, OutT, RetT, StorageT}(Mutable{T1, LocalT, InT, OutT, RetT, StorageT}, T1, long, long)"/>
        /// <exception cref="IndexOutOfRangeException">If an index lays outside of the allowed range. The exception message 
        /// will carry more details regarding the cause of this error.</exception>
        public RetT this[BaseArray d0, BaseArray d1, BaseArray d2] {
            get {
                return Subarray(d0, d1, d2);
            }
        }
        /// <summary>
        /// Gets a subarray from this array.
        /// </summary>
        /// <param name="d0">Range specification(s) for dimension #0.</param>
        /// <param name="d1">Range specification(s) for dimension #1.</param>
        /// <param name="d2">Range specification(s) for dimension #2.</param>
        /// <param name="d3">Range specification(s) for dimension #3.</param>
        /// <returns>Subarray according to the range specification(s) in <paramref name="d0"/>,... .</returns>
        /// <remarks><para>This indexer supports subarray retrieval from immutable arrays (RetArray, Logical etc.). This is 
        /// part of the 'flexible' subarray API: the array returned will reference new memory with copies of the elements from this array.</para></remarks>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Subarray(BaseArray[])"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, BaseArray, BaseArray, BaseArray)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.SetValue{T1, LocalT, InT, OutT, RetT, StorageT}(Mutable{T1, LocalT, InT, OutT, RetT, StorageT}, T1, long, long)"/>
        /// <exception cref="IndexOutOfRangeException">If an index lays outside of the allowed range. The exception message 
        /// will carry more details regarding the cause of this error.</exception>
        public RetT this[BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3] {
            get {
                return Subarray(d0, d1, d2, d3);
            }
        }
        /// <summary>
        /// Gets a subarray from this array.
        /// </summary>
        /// <param name="d0">Range specification(s) for dimension #0.</param>
        /// <param name="d1">Range specification(s) for dimension #1.</param>
        /// <param name="d2">Range specification(s) for dimension #2.</param>
        /// <param name="d3">Range specification(s) for dimension #3.</param>
        /// <param name="d4">Range specification(s) for dimension #4.</param>
        /// <returns>Subarray according to the range specification(s) in <paramref name="d0"/>,... .</returns>
        /// <remarks><para>This indexer supports subarray retrieval from immutable arrays (RetArray, Logical etc.). This is 
        /// part of the 'flexible' subarray API: the array returned will reference new memory with copies of the elements from this array.</para></remarks>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Subarray(BaseArray[])"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, BaseArray, BaseArray, BaseArray)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.SetValue{T1, LocalT, InT, OutT, RetT, StorageT}(Mutable{T1, LocalT, InT, OutT, RetT, StorageT}, T1, long, long)"/>
        /// <exception cref="IndexOutOfRangeException">If an index lays outside of the allowed range. The exception message 
        /// will carry more details regarding the cause of this error.</exception>
        public RetT this[BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3, BaseArray d4] {
            get {
                return Subarray(d0, d1, d2, d3, d4);
            }
        }
        /// <summary>
        /// Gets a subarray from this array.
        /// </summary>
        /// <param name="d0">Range specification(s) for dimension #0.</param>
        /// <param name="d1">Range specification(s) for dimension #1.</param>
        /// <param name="d2">Range specification(s) for dimension #2.</param>
        /// <param name="d3">Range specification(s) for dimension #3.</param>
        /// <param name="d4">Range specification(s) for dimension #4.</param>
        /// <param name="d5">Range specification(s) for dimension #5.</param>
        /// <returns>Subarray according to the range specification(s) in <paramref name="d0"/>,... .</returns>
        /// <remarks><para>This indexer supports subarray retrieval from immutable arrays (RetArray, Logical etc.). This is 
        /// part of the 'flexible' subarray API: the array returned will reference new memory with copies of the elements from this array.</para></remarks>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Subarray(BaseArray[])"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, BaseArray, BaseArray, BaseArray)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.SetValue{T1, LocalT, InT, OutT, RetT, StorageT}(Mutable{T1, LocalT, InT, OutT, RetT, StorageT}, T1, long, long)"/>
        /// <exception cref="IndexOutOfRangeException">If an index lays outside of the allowed range. The exception message 
        /// will carry more details regarding the cause of this error.</exception>
        public RetT this[BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3, BaseArray d4, BaseArray d5] {
            get {
                return Subarray(d0, d1, d2, d3, d4, d5);
            }
        }
        /// <summary>
        /// Gets a subarray from this array.
        /// </summary>
        /// <param name="d0">Range specification(s) for dimension #0.</param>
        /// <param name="d1">Range specification(s) for dimension #1.</param>
        /// <param name="d2">Range specification(s) for dimension #2.</param>
        /// <param name="d3">Range specification(s) for dimension #3.</param>
        /// <param name="d4">Range specification(s) for dimension #4.</param>
        /// <param name="d5">Range specification(s) for dimension #5.</param>
        /// <param name="d6">Range specification(s) for dimension #6.</param>
        /// <returns>Subarray according to the range specification(s) in <paramref name="d0"/>,... .</returns>
        /// <remarks><para>This indexer supports subarray retrieval from immutable arrays (RetArray, Logical etc.). This is 
        /// part of the 'flexible' subarray API: the array returned will reference new memory with copies of the elements from this array.</para></remarks>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Subarray(BaseArray[])"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, BaseArray, BaseArray, BaseArray)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.SetValue{T1, LocalT, InT, OutT, RetT, StorageT}(Mutable{T1, LocalT, InT, OutT, RetT, StorageT}, T1, long, long)"/>
        /// <exception cref="IndexOutOfRangeException">If an index lays outside of the allowed range. The exception message 
        /// will carry more details regarding the cause of this error.</exception>
        public RetT this[BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3, BaseArray d4, BaseArray d5, BaseArray d6] {
            get {
                return Subarray(d0, d1, d2, d3, d4, d5, d6);
            }
        }

        /// <summary>
        /// Gets a subarray from this array, ranges are based on an array of index arrays.
        /// </summary>
        /// <param name="dims">Range specification(s).</param>
        /// <returns>Subarray according to the range specification(s) in <paramref name="dims"/>.</returns>
        /// <remarks><para>This indexer supports subarray retrieval from immutable arrays (RetArray, Logical etc.). This is 
        /// part of the 'flexible' subarray API: the array returned will reference new memory with copies of the elements from this array.</para></remarks>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Subarray(BaseArray[])"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, BaseArray, BaseArray, BaseArray)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.SetValue{T1, LocalT, InT, OutT, RetT, StorageT}(Mutable{T1, LocalT, InT, OutT, RetT, StorageT}, T1, long, long)"/>
        /// <exception cref="IndexOutOfRangeException">If an index lays outside of the allowed range. The exception message 
        /// will carry more details regarding the cause of this error.</exception>
        public RetT this[params BaseArray[] dims] {
            get {
                return Subarray(dims);
            }
        }

        #endregion


    }
}
