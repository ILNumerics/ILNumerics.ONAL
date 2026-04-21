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
    public abstract partial class Mutable<T1, LocalT, InT, OutT, RetT, StorageT>   // T1 must not read "T" to prevent from conflicts with the .T (transpose) property.
        : ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT>
        where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {

        #region scalar long indexers, read/write
        /// <summary>
        /// Retrieves the value(s) of the element(s) at the position(s) specified by <paramref name="d0"/> or sets them.
        /// </summary>
        /// <param name="d0">Index into the first dimension or sequential index into the array.</param>
        /// <returns>Element value(s) wrapped into a new array.</returns>
        /// <value>Right side array with value(s) to be assigned to the element(s) addressed.</value>
        /// <remarks><para>This functions retrieves/ modifies an individual element or a subarray corresponding to <paramref name="d0"/>.</para> 
        /// <para>If this array has more dimensions than indices specified the size of the subarray returned / of the subarray 
        /// addressed depends on the setting of <see cref="Settings.ArrayStyle"/>. For <see cref="ArrayStyles.ILNumericsV4"/>  
        /// <paramref name="d0"/> is considered a <b>sequential</b> index into its own and into subsequent, merged dimensions.  
        /// The array returned will be a scalar. This mode allows to expand / to remove parts of this array when using the set accessor.</para> 
        /// <para>For <see cref="ArrayStyles.numpy"/> all indices must fit into their dimensions. Any unspecified 
        /// trailing dimensions are substituted with <see cref="Globals.full"/> and the array returned may contain multiple 
        /// elements from those dimensions. <see cref="ArrayStyles.numpy"/> does not allow to change the size of existing arrays.</para>
        /// <para>This indexer is readonly on immutable types. It is optimized for efficient single element access by providing 
        /// a distinct index parameter for each dimension of this array. Providing a number of indices matching the number of 
        /// dimensions in this array is recommended for speed and for compatibility of the index access with both <see cref="ArrayStyles"/>.</para> 
        /// <para>In order to access elements based on their element type <typeparamref name="T1"/> directly (i.e.: without wrapping it into a new 
        /// array) and as a faster alternative for scalar or mostly-scalar algorithms use one of the extension methods  
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, uint)"/> or similar overloads for concrete arrays.</para>
        /// <para>This overload allows indexing into huge arrays and negative indices, addressing elements from the end of their 
        /// respective dimension / merged dimensions.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds 
        /// the <see cref="Size.NumberOfElements"/>.</exception>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html">Subarray tutorial in the ILNumerics online documentation</seealso>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, uint)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, long)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Subarray(DimSpec, DimSpec)"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        public RetT this[long d0] {
            get {

                using var _1 = ReaderLock.Create(this, out var storage, releaseRetT: true);
                return storage.Indexer_Get(d0).RetArray;

            }
            set {
                lock (SynchObj) {

 

                    using var _1 = ReaderLock.Create(value, out var valStorage);
                    m_storage.Assign(m_storage.MutableIndexer_Set(valStorage, d0), true, true);
                }
            }
        }
        /// <summary>
        /// Retrieves the value(s) of the element(s) at the position(s) specified by <paramref name="d0"/> and <paramref name="d1"/> or sets them.
        /// </summary>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension or sequential index into remaining dimensions.</param>
        /// <returns>Element value(s) wrapped into a new array.</returns>
        /// <value>Right side array with value(s) to be assigned to the element(s) addressed.</value>
        /// <remarks><para>This functions retrieves/ modifies an individual element or a subarray corresponding to <paramref name="d0"/> and <paramref name="d1"/>.</para> 
        /// <para>If this array has more dimensions than indices specified the size of the subarray returned / of the subarray 
        /// addressed depends on the setting of <see cref="Settings.ArrayStyle"/>. For <see cref="ArrayStyles.ILNumericsV4"/>  
        /// <paramref name="d1"/> is considered a <b>sequential</b> index into its own and into subsequent, merged dimensions.  
        /// The array returned will be a scalar. This mode allows to expand / to remove parts of this array when using the 
        /// set accessor.</para> 
        /// <para>For <see cref="ArrayStyles.numpy"/> all indices must fit into their dimensions. Any unspecified 
        /// trailing dimensions are substituted with <see cref="Globals.full"/> and the array returned contains multiple 
        /// elements from those dimensions. <see cref="ArrayStyles.numpy"/> does not allow to change the size of existing arrays.</para>
        /// <para>This indexer is readonly on immutable types. It is optimized for efficient single element access by providing 
        /// a distinct index parameter for each dimension of this array. Providing a number of indices matching the number of 
        /// dimensions in this array is recommended for speed and for compatibility of the index access with both <see cref="ArrayStyles"/>.</para> 
        /// <para>In order to access elements based on their element type <typeparamref name="T1"/> directly (i.e.: without wrapping it into a new 
        /// array) and as a faster alternative for scalar or mostly-scalar algorithms use one of the extension methods  
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, uint)"/> or similar overloads for concrete arrays.</para>
        /// <para>This overload allows indexing into huge arrays and negative indices, addressing elements from the end of their 
        /// respective dimension / merged dimensions.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException">if the value of a given index exceeds the valid range.</exception>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html">Subarray tutorial in the ILNumerics online documentation</seealso>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, uint)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, long)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Subarray(DimSpec, DimSpec)"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        public RetT this[long d0, long d1] {
            get {

                using var _1 = ReaderLock.Create(this, out var storage, releaseRetT: true);
                return storage.Indexer_Get(d0, d1).RetArray;

            }
            set {
                lock (SynchObj) {

 

                    using var _1 = ReaderLock.Create(value, out var valStorage); 
                    m_storage.Assign(m_storage.MutableIndexer_Set(valStorage, d0, d1), true, true);
                }
            }
        }
        /// <summary>
        /// Retrieves the value(s) of the element(s) at the position specified by <paramref name="d0"/> ... <paramref name="d2"/> or sets them.
        /// </summary>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value(s) wrapped into a new array.</returns>
        /// <value>Right side array with value(s) to be assigned to the element(s) addressed.</value>
        /// <remarks><para>This functions retrieves/ modifies an individual element or a subarray corresponding to 
        /// <paramref name="d0"/> ... <paramref name="d2"/>.</para> 
        /// <para>If this array has more dimensions than indices specified the size of the subarray returned / of the subarray 
        /// addressed depends on the setting of <see cref="Settings.ArrayStyle"/>. For <see cref="ArrayStyles.ILNumericsV4"/>  
        /// <paramref name="d2"/> is considered a <b>sequential</b> index into its own and into subsequent, merged dimensions.  
        /// The array returned will be a scalar. This mode allows to expand / to remove parts of this array when using the 
        /// set accessor.</para> 
        /// <para>For <see cref="ArrayStyles.numpy"/> all indices must fit into their dimensions. Any unspecified 
        /// trailing dimensions are substituted with <see cref="Globals.full"/> and the array returned contains multiple 
        /// elements from those dimensions. <see cref="ArrayStyles.numpy"/> does not allow to change the size of existing arrays.</para>
        /// <para>This indexer is readonly on immutable types. It is optimized for efficient single element access by providing 
        /// a distinct index parameter for each dimension of this array. Providing a number of indices matching the number of 
        /// dimensions in this array is recommended for speed and for compatibility of the index access with both <see cref="ArrayStyles"/>.</para> 
        /// <para>In order to access elements based on their element type <typeparamref name="T1"/> directly (i.e.: without wrapping it into a new 
        /// array) and as a faster alternative for scalar or mostly-scalar algorithms use one of the extension methods  
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, uint)"/> or similar overloads for concrete arrays.</para>
        /// <para>This overload allows indexing into huge arrays and negative indices, addressing elements from the end of their 
        /// respective dimension / merged dimensions.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException">if the value of a given index exceeds the valid range.</exception>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html">Subarray tutorial in the ILNumerics online documentation</seealso>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, uint)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, long)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Subarray(DimSpec, DimSpec)"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        public RetT this[long d0, long d1, long d2] {
            get {

                using var _1 = ReaderLock.Create(this, out var storage, releaseRetT: true);
                return storage.Indexer_Get(d0, d1, d2).RetArray;

            }
            set {
                lock (SynchObj) {



                    using var _1 = ReaderLock.Create(value, out var valStorage);
                    m_storage.Assign(m_storage.MutableIndexer_Set(valStorage, d0, d1, d2), true, true);
                }
            }
        }
        /// <summary>
        /// Retrieves the value(s) of the element(s) at the position specified by <paramref name="d0"/> ... <paramref name="d3"/> or sets them.
        /// </summary>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value(s) wrapped into a new array.</returns>
        /// <value>Right side array with value(s) to be assigned to the element(s) addressed.</value>
        /// <remarks><para>This functions retrieves/ modifies an individual element or a subarray corresponding to 
        /// <paramref name="d0"/> ... <paramref name="d3"/>.</para> 
        /// <para>If this array has more dimensions than indices specified the size of the subarray returned / of the subarray 
        /// addressed depends on the setting of <see cref="Settings.ArrayStyle"/>. For <see cref="ArrayStyles.ILNumericsV4"/>  
        /// <paramref name="d3"/> is considered a <b>sequential</b> index into its own and into subsequent, merged dimensions.  
        /// The array returned will be a scalar. This mode allows to expand / to remove parts of this array when using the 
        /// set accessor.</para> 
        /// <para>For <see cref="ArrayStyles.numpy"/> all indices must fit into their dimensions. Any unspecified 
        /// trailing dimensions are substituted with <see cref="Globals.full"/> and the array returned contains multiple 
        /// elements from those dimensions. <see cref="ArrayStyles.numpy"/> does not allow to change the size of existing arrays.</para>
        /// <para>This indexer is readonly on immutable types. It is optimized for efficient single element access by providing 
        /// a distinct index parameter for each dimension of this array. Providing a number of indices matching the number of 
        /// dimensions in this array is recommended for speed and for compatibility of the index access with both <see cref="ArrayStyles"/>.</para> 
        /// <para>In order to access elements based on their element type <typeparamref name="T1"/> directly (i.e.: without wrapping it into a new 
        /// array) and as a faster alternative for scalar or mostly-scalar algorithms use one of the extension methods  
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, uint)"/> or similar overloads for concrete arrays.</para>
        /// <para>This overload allows indexing into huge arrays and negative indices, addressing elements from the end of their 
        /// respective dimension / merged dimensions.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException">if the value of a given index exceeds the valid range.</exception>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html">Subarray tutorial in the ILNumerics online documentation</seealso>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, uint)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, long)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Subarray(DimSpec, DimSpec)"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        public RetT this[long d0, long d1, long d2, long d3] {
            get {

                using var _1 = ReaderLock.Create(this, out var storage, releaseRetT: true);
                return storage.Indexer_Get(d0, d1, d2, d3).RetArray;

            }
            set {
                lock (SynchObj) {



                    using var _1 = ReaderLock.Create(value, out var valStorage);
                    m_storage.Assign(m_storage.MutableIndexer_Set(valStorage, d0, d1, d2, d3), true, true);
                }
            }
        }
        /// <summary>
        /// Retrieves the value(s) of the element(s) at the position specified by <paramref name="d0"/> ... <paramref name="d4"/> or sets them.
        /// </summary>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value(s) wrapped into a new array.</returns>
        /// <value>Right side array with value(s) to be assigned to the element(s) addressed.</value>
        /// <remarks><para>This functions retrieves/ modifies an individual element or a subarray corresponding to 
        /// <paramref name="d0"/> ... <paramref name="d4"/>.</para> 
        /// <para>If this array has more dimensions than indices specified the size of the subarray returned / of the subarray 
        /// addressed depends on the setting of <see cref="Settings.ArrayStyle"/>. For <see cref="ArrayStyles.ILNumericsV4"/>  
        /// <paramref name="d4"/> is considered a <b>sequential</b> index into its own and into subsequent, merged dimensions.  
        /// The array returned will be a scalar. This mode allows to expand / to remove parts of this array when using the 
        /// set accessor.</para> 
        /// <para>For <see cref="ArrayStyles.numpy"/> all indices must fit into their dimensions. Any unspecified 
        /// trailing dimensions are substituted with <see cref="Globals.full"/> and the array returned contains multiple 
        /// elements from those dimensions. <see cref="ArrayStyles.numpy"/> does not allow to change the size of existing arrays.</para>
        /// <para>This indexer is readonly on immutable types. It is optimized for efficient single element access by providing 
        /// a distinct index parameter for each dimension of this array. Providing a number of indices matching the number of 
        /// dimensions in this array is recommended for speed and for compatibility of the index access with both <see cref="ArrayStyles"/>.</para> 
        /// <para>In order to access elements based on their element type <typeparamref name="T1"/> directly (i.e.: without wrapping it into a new 
        /// array) and as a faster alternative for scalar or mostly-scalar algorithms use one of the extension methods  
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, uint)"/> or similar overloads for concrete arrays.</para>
        /// <para>This overload allows indexing into huge arrays and negative indices, addressing elements from the end of their 
        /// respective dimension / merged dimensions.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException">if the value of a given index exceeds the valid range.</exception>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html">Subarray tutorial in the ILNumerics online documentation</seealso>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, uint)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, long)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Subarray(DimSpec, DimSpec)"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        public RetT this[long d0, long d1, long d2, long d3, long d4] {
            get {

                using var _1 = ReaderLock.Create(this, out var storage, releaseRetT: true);
                return storage.Indexer_Get(d0, d1, d2, d3, d4).RetArray;

            }
            set {
                lock (SynchObj) {



                    using var _1 = ReaderLock.Create(value, out var valStorage);
                    m_storage.Assign(m_storage.MutableIndexer_Set(valStorage, d0, d1, d2, d3, d4), true, true);
                }
            }
        }
        /// <summary>
        /// Retrieves the value(s) of the element(s) at the position specified by <paramref name="d0"/> ... <paramref name="d5"/> or sets them.
        /// </summary>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension or sequential index into remaining dimensions of the array.</param>
        /// <returns>Element value(s) wrapped into a new array.</returns>
        /// <value>Right side array with value(s) to be assigned to the element(s) addressed.</value>
        /// <remarks><para>This functions retrieves/ modifies an individual element or a subarray corresponding to 
        /// <paramref name="d0"/> ... <paramref name="d5"/>.</para> 
        /// <para>If this array has more dimensions than indices specified the size of the subarray returned / of the subarray 
        /// addressed depends on the setting of <see cref="Settings.ArrayStyle"/>. For <see cref="ArrayStyles.ILNumericsV4"/>  
        /// <paramref name="d5"/> is considered a <b>sequential</b> index into its own and into subsequent, merged dimensions.  
        /// The array returned will be a scalar. This mode allows to expand / to remove parts of this array when using the 
        /// set accessor.</para> 
        /// <para>For <see cref="ArrayStyles.numpy"/> all indices must fit into their dimensions. Any unspecified 
        /// trailing dimensions are substituted with <see cref="Globals.full"/> and the array returned contains multiple 
        /// elements from those dimensions. <see cref="ArrayStyles.numpy"/> does not allow to change the size of existing arrays.</para>
        /// <para>This indexer is readonly on immutable types. It is optimized for efficient single element access by providing 
        /// a distinct index parameter for each dimension of this array. Providing a number of indices matching the number of 
        /// dimensions in this array is recommended for speed and for compatibility of the index access with both <see cref="ArrayStyles"/>.</para> 
        /// <para>In order to access elements based on their element type <typeparamref name="T1"/> directly (i.e.: without wrapping it into a new 
        /// array) and as a faster alternative for scalar or mostly-scalar algorithms use one of the extension methods  
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, uint)"/> or similar overloads for concrete arrays.</para>
        /// <para>This overload allows indexing into huge arrays and negative indices, addressing elements from the end of their 
        /// respective dimension / merged dimensions.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException">if the value of a given index exceeds the valid range.</exception>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html">Subarray tutorial in the ILNumerics online documentation</seealso>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, uint)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, long)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Subarray(DimSpec, DimSpec)"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        public RetT this[long d0, long d1, long d2, long d3, long d4, long d5] {
            get {

                using var _1 = ReaderLock.Create(this, out var storage, releaseRetT: true);
                return storage.Indexer_Get(d0, d1, d2, d3, d4, d5).RetArray;

            }
            set {
                lock (SynchObj) {



                    using var _1 = ReaderLock.Create(value, out var valStorage);
                    m_storage.Assign(m_storage.MutableIndexer_Set(valStorage, d0, d1, d2, d3, d4, d5), true, true);
                }
            }
        }
        /// <summary>
        /// Retrieves the value(s) of the element(s) at the position specified by <paramref name="d0"/> ... <paramref name="d6"/> or sets them.
        /// </summary>
        /// <param name="d0">Index into the first dimension. </param>
        /// <param name="d1">Index into the second dimension. </param>
        /// <param name="d2">Index into the third dimension. </param>
        /// <param name="d3">Index into the fourth dimension. </param>
        /// <param name="d4">Index into the fifths dimension. </param>
        /// <param name="d5">Index into the sixths dimension. </param>
        /// <param name="d6">Index into the sevenths dimension or sequential index into remaining dimensions of the array, if any.</param>
        /// <returns>Element value(s) wrapped into a new array.</returns>
        /// <value>Right side array with value(s) to be assigned to the element(s) addressed.</value>
        /// <remarks><para>This functions retrieves/ modifies an individual element or a subarray corresponding to 
        /// <paramref name="d0"/> ... <paramref name="d6"/>.</para> 
        /// <para>If this array has more dimensions than indices specified the size of the subarray returned / of the subarray 
        /// addressed depends on the setting of <see cref="Settings.ArrayStyle"/>. For <see cref="ArrayStyles.ILNumericsV4"/>  
        /// <paramref name="d6"/> is considered a <b>sequential</b> index into its own and into subsequent, merged dimensions.  
        /// The array returned will be a scalar. This mode allows to expand / to remove parts of this array when using the 
        /// set accessor.</para> 
        /// <para>For <see cref="ArrayStyles.numpy"/> all indices must fit into their dimensions. Any unspecified 
        /// trailing dimensions are substituted with <see cref="Globals.full"/> and the array returned contains multiple 
        /// elements from those dimensions. <see cref="ArrayStyles.numpy"/> does not allow to change the size of existing arrays.</para>
        /// <para>This indexer is readonly on immutable types. It is optimized for efficient single element access by providing 
        /// a distinct index parameter for each dimension of this array. Providing a number of indices matching the number of 
        /// dimensions in this array is recommended for speed and for compatibility of the index access with both <see cref="ArrayStyles"/>.</para> 
        /// <para>In order to access elements based on their element type <typeparamref name="T1"/> directly (i.e.: without wrapping it into a new 
        /// array) and as a faster alternative for scalar or mostly-scalar algorithms use one of the extension methods  
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, uint)"/> or similar overloads for concrete arrays.</para>
        /// <para>This overload allows indexing into huge arrays and negative indices, addressing elements from the end of their 
        /// respective dimension / merged dimensions.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException">if the value of a given index exceeds the valid range.</exception>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html">Subarray tutorial in the ILNumerics online documentation</seealso>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, uint)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, long)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Subarray(DimSpec, DimSpec)"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        public RetT this[long d0, long d1, long d2, long d3, long d4, long d5, long d6] { 
            get {

                using var _1 = ReaderLock.Create(this, out var storage, releaseRetT: true);
                return storage.Indexer_Get(d0, d1, d2, d3, d4, d5, d6).RetArray;

            }
            set {
                lock (SynchObj) {



                    using var _1 = ReaderLock.Create(value, out var valStorage);
                    m_storage.Assign(m_storage.MutableIndexer_Set(valStorage, d0, d1, d2, d3, d4, d5, d6), true, true);
                }
            }
        }
        #endregion

        #region dimspec indexers, read/write
        /// <summary>
        /// Gets a subarray from this array or replaces the values of part of this array.
        /// </summary>
        /// <param name="dims">Array with range specification(s) for dimensions of this array.</param>
        /// <value>The right side array providing the new values.</value>
        /// <returns>Subarray according to the range specification(s) <paramref name="dims"/>...</returns>
        /// <remarks><para>This indexer supports subarray retrieval and subarray assignment 
        /// for mutable ILNumerics array types: <see cref="Array{T}"/>, <see cref="OutArray{T}"/> and 
        /// the corresponding array types for <see cref="Logical"/>, <see cref="OutLogical"/>, <see cref="Cell"/>
        /// and <see cref="OutCell"/>.</para></remarks>
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
            set {
                SetRange(value, dims);
            }
        }

        /// <summary>
        /// Gets a subarray from this array or replaces the values of part of this array.
        /// </summary>
        /// <param name="d0">Range specification for dimension #0.</param>
        /// <value>The right side array providing the new values.</value>
        /// <returns>Subarray according to the range specification(s) <paramref name="d0"/>...</returns>
        /// <remarks><para>This indexer supports subarray retrieval and subarray assignment 
        /// for mutable ILNumerics array types: <see cref="Array{T}"/>, <see cref="OutArray{T}"/> and 
        /// the corresponding array types for <see cref="Logical"/>, <see cref="OutLogical"/>, <see cref="Cell"/>
        /// and <see cref="OutCell"/>.</para></remarks>
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
            set {
                SetRange(value, d0);
            }
        }
        /// <summary>
        /// Gets a subarray from this array or replaces the values of part of this array.
        /// </summary>
        /// <param name="d0">Range specification for dimension #0.</param>
        /// <param name="d1">Range specification for dimension #1.</param>
        /// <value>The right side array providing the new values.</value>
        /// <returns>Subarray according to the range specification(s) <paramref name="d0"/>...</returns>
        /// <remarks><para>This indexer supports subarray retrieval and subarray assignment 
        /// for mutable ILNumerics array types: <see cref="Array{T}"/>, <see cref="OutArray{T}"/> and 
        /// the corresponding array types for <see cref="Logical"/>, <see cref="OutLogical"/>, <see cref="Cell"/>
        /// and <see cref="OutCell"/>.</para></remarks>
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
            set {
                SetRange(value, d0, d1);
            }
        }
        /// <summary>
        /// Gets a subarray from this array or replaces the values of part of this array.
        /// </summary>
        /// <param name="d0">Range specification for dimension #0.</param>
        /// <param name="d1">Range specification for dimension #1.</param>
        /// <param name="d2">Range specification for dimension #2.</param>
        /// <value>The right side array providing the new values.</value>
        /// <returns>Subarray according to the range specification(s) <paramref name="d0"/>...</returns>
        /// <remarks><para>This indexer supports subarray retrieval and subarray assignment 
        /// for mutable ILNumerics array types: <see cref="Array{T}"/>, <see cref="OutArray{T}"/> and 
        /// the corresponding array types for <see cref="Logical"/>, <see cref="OutLogical"/>, <see cref="Cell"/>
        /// and <see cref="OutCell"/>.</para></remarks>
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
            set {
                SetRange(value, d0, d1, d2);
            }
        }
        /// <summary>
        /// Gets a subarray from this array or replaces the values of part of this array.
        /// </summary>
        /// <param name="d0">Range specification for dimension #0.</param>
        /// <param name="d1">Range specification for dimension #1.</param>
        /// <param name="d2">Range specification for dimension #2.</param>
        /// <param name="d3">Range specification for dimension #3.</param>
        /// <value>The right side array providing the new values.</value>
        /// <returns>Subarray according to the range specification(s) <paramref name="d0"/>...</returns>
        /// <remarks><para>This indexer supports subarray retrieval and subarray assignment 
        /// for mutable ILNumerics array types: <see cref="Array{T}"/>, <see cref="OutArray{T}"/> and 
        /// the corresponding array types for <see cref="Logical"/>, <see cref="OutLogical"/>, <see cref="Cell"/>
        /// and <see cref="OutCell"/>.</para></remarks>
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
            set {
                SetRange(value, d0, d1, d2, d3);
            }
        }
        /// <summary>
        /// Gets a subarray from this array or replaces the values of part of this array.
        /// </summary>
        /// <param name="d0">Range specification for dimension #0.</param>
        /// <param name="d1">Range specification for dimension #1.</param>
        /// <param name="d2">Range specification for dimension #2.</param>
        /// <param name="d3">Range specification for dimension #3.</param>
        /// <param name="d4">Range specification for dimension #4.</param>
        /// <value>The right side array providing the new values.</value>
        /// <returns>Subarray according to the range specification(s) <paramref name="d0"/>...</returns>
        /// <remarks><para>This indexer supports subarray retrieval and subarray assignment 
        /// for mutable ILNumerics array types: <see cref="Array{T}"/>, <see cref="OutArray{T}"/> and 
        /// the corresponding array types for <see cref="Logical"/>, <see cref="OutLogical"/>, <see cref="Cell"/>
        /// and <see cref="OutCell"/>.</para></remarks>
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
            set {
                SetRange(value, d0, d1, d2, d3, d4);
            }
        }
        /// <summary>
        /// Gets a subarray from this array or replaces the values of part of this array.
        /// </summary>
        /// <param name="d0">Range specification for dimension #0.</param>
        /// <param name="d1">Range specification for dimension #1.</param>
        /// <param name="d2">Range specification for dimension #2.</param>
        /// <param name="d3">Range specification for dimension #3.</param>
        /// <param name="d4">Range specification for dimension #4.</param>
        /// <param name="d5">Range specification for dimension #5.</param>
        /// <value>The right side array providing the new values.</value>
        /// <returns>Subarray according to the range specification(s) <paramref name="d0"/>...</returns>
        /// <remarks><para>This indexer supports subarray retrieval and subarray assignment 
        /// for mutable ILNumerics array types: <see cref="Array{T}"/>, <see cref="OutArray{T}"/> and 
        /// the corresponding array types for <see cref="Logical"/>, <see cref="OutLogical"/>, <see cref="Cell"/>
        /// and <see cref="OutCell"/>.</para></remarks>
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
            set {
                SetRange(value, d0, d1, d2, d3, d4, d5);
            }
        }
        /// <summary>
        /// Gets a subarray from this array or replaces the values of part of this array.
        /// </summary>
        /// <param name="d0">Range specification for dimension #0.</param>
        /// <param name="d1">Range specification for dimension #1.</param>
        /// <param name="d2">Range specification for dimension #2.</param>
        /// <param name="d3">Range specification for dimension #3.</param>
        /// <param name="d4">Range specification for dimension #4.</param>
        /// <param name="d5">Range specification for dimension #5.</param>
        /// <param name="d6">Range specification for dimension #6.</param>
        /// <value>The right side array providing the new values.</value>
        /// <returns>Subarray according to the range specification(s) <paramref name="d0"/>...</returns>
        /// <remarks><para>This indexer supports subarray retrieval and subarray assignment 
        /// for mutable ILNumerics array types: <see cref="Array{T}"/>, <see cref="OutArray{T}"/> and 
        /// the corresponding array types for <see cref="Logical"/>, <see cref="OutLogical"/>, <see cref="Cell"/>
        /// and <see cref="OutCell"/>.</para></remarks>
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
            set {
                SetRange(value, d0, d1, d2, d3, d4, d5, d6);
            }
        }
        #endregion

        #region BaseArray indexers, read/ write
        /// <summary>
        /// Gets a subarray from this array or replaces the values of parts of this array.
        /// </summary>
        /// <param name="d0">Range specification for dimension #0.</param>
        /// <value>The right side array providing the new values.</value>
        /// <returns>Subarray according to the range specification(s) <paramref name="d0"/>...</returns>
        /// <remarks><para>This indexer supports subarray retrieval and subarray assignment 
        /// for mutable ILNumerics array types: <see cref="Array{T}"/>, <see cref="OutArray{T}"/> and 
        /// the corresponding array types for <see cref="Logical"/>, <see cref="OutLogical"/>, <see cref="Cell"/>
        /// and <see cref="OutCell"/>.</para></remarks>
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
            set {
                SetRange(value, d0);
            }
        }
        /// <summary>
        /// Gets a subarray from this array or replaces the values of part of this array.
        /// </summary>
        /// <param name="d0">Range specification for dimension #0.</param>
        /// <param name="d1">Range specification for dimension #1.</param>
        /// <value>The right side array providing the new values.</value>
        /// <returns>Subarray according to the range specification(s) <paramref name="d0"/>...</returns>
        /// <remarks><para>This indexer supports subarray retrieval and subarray assignment 
        /// for mutable ILNumerics array types: <see cref="Array{T}"/>, <see cref="OutArray{T}"/> and 
        /// the corresponding array types for <see cref="Logical"/>, <see cref="OutLogical"/>, <see cref="Cell"/>
        /// and <see cref="OutCell"/>.</para></remarks>
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
            set {
                SetRange(value, d0, d1);
            }
        }
        /// <summary>
        /// Gets a subarray from this array or replaces the values of part of this array.
        /// </summary>
        /// <param name="d0">Range specification for dimension #0.</param>
        /// <param name="d1">Range specification for dimension #1.</param>
        /// <param name="d2">Range specification for dimension #2.</param>
        /// <value>The right side array providing the new values.</value>
        /// <returns>Subarray according to the range specification(s) <paramref name="d0"/>...</returns>
        /// <remarks><para>This indexer supports subarray retrieval and subarray assignment 
        /// for mutable ILNumerics array types: <see cref="Array{T}"/>, <see cref="OutArray{T}"/> and 
        /// the corresponding array types for <see cref="Logical"/>, <see cref="OutLogical"/>, <see cref="Cell"/>
        /// and <see cref="OutCell"/>.</para></remarks>
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
            set {
                SetRange(value, d0, d1, d2);
            }
        }
        /// <summary>
        /// Gets a subarray from this array or replaces the values of part of this array.
        /// </summary>
        /// <param name="d0">Range specification for dimension #0.</param>
        /// <param name="d1">Range specification for dimension #1.</param>
        /// <param name="d2">Range specification for dimension #2.</param>
        /// <param name="d3">Range specification for dimension #3.</param>
        /// <value>The right side array providing the new values.</value>
        /// <returns>Subarray according to the range specification(s) <paramref name="d0"/>...</returns>
        /// <remarks><para>This indexer supports subarray retrieval and subarray assignment 
        /// for mutable ILNumerics array types: <see cref="Array{T}"/>, <see cref="OutArray{T}"/> and 
        /// the corresponding array types for <see cref="Logical"/>, <see cref="OutLogical"/>, <see cref="Cell"/>
        /// and <see cref="OutCell"/>.</para></remarks>
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
            set {
                SetRange(value, d0, d1, d2, d3);
            }
        }
        /// <summary>
        /// Gets a subarray from this array or replaces the values of part of this array.
        /// </summary>
        /// <param name="d0">Range specification for dimension #0.</param>
        /// <param name="d1">Range specification for dimension #1.</param>
        /// <param name="d2">Range specification for dimension #2.</param>
        /// <param name="d3">Range specification for dimension #3.</param>
        /// <param name="d4">Range specification for dimension #4.</param>
        /// <value>The right side array providing the new values.</value>
        /// <returns>Subarray according to the range specification(s) <paramref name="d0"/>...</returns>
        /// <remarks><para>This indexer supports subarray retrieval and subarray assignment 
        /// for mutable ILNumerics array types: <see cref="Array{T}"/>, <see cref="OutArray{T}"/> and 
        /// the corresponding array types for <see cref="Logical"/>, <see cref="OutLogical"/>, <see cref="Cell"/>
        /// and <see cref="OutCell"/>.</para></remarks>
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
            set {
                SetRange(value, d0, d1, d2, d3, d4);
            }
        }
        /// <summary>
        /// Gets a subarray from this array or replaces the values of part of this array.
        /// </summary>
        /// <param name="d0">Range specification for dimension #0.</param>
        /// <param name="d1">Range specification for dimension #1.</param>
        /// <param name="d2">Range specification for dimension #2.</param>
        /// <param name="d3">Range specification for dimension #3.</param>
        /// <param name="d4">Range specification for dimension #4.</param>
        /// <param name="d5">Range specification for dimension #5.</param>
        /// <value>The right side array providing the new values.</value>
        /// <returns>Subarray according to the range specification(s) <paramref name="d0"/>...</returns>
        /// <remarks><para>This indexer supports subarray retrieval and subarray assignment 
        /// for mutable ILNumerics array types: <see cref="Array{T}"/>, <see cref="OutArray{T}"/> and 
        /// the corresponding array types for <see cref="Logical"/>, <see cref="OutLogical"/>, <see cref="Cell"/>
        /// and <see cref="OutCell"/>.</para></remarks>
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
            set {
                SetRange(value, d0, d1, d2, d3, d4, d5);
            }
        }
        /// <summary>
        /// Gets a subarray from this array or replaces the values of part of this array.
        /// </summary>
        /// <param name="d0">Range specification for dimension #0.</param>
        /// <param name="d1">Range specification for dimension #1.</param>
        /// <param name="d2">Range specification for dimension #2.</param>
        /// <param name="d3">Range specification for dimension #3.</param>
        /// <param name="d4">Range specification for dimension #4.</param>
        /// <param name="d5">Range specification for dimension #5.</param>
        /// <param name="d6">Range specification for dimension #6.</param>
        /// <value>The right side array providing the new values.</value>
        /// <returns>Subarray according to the range specification(s) <paramref name="d0"/>...</returns>
        /// <remarks><para>This indexer supports subarray retrieval and subarray assignment 
        /// for mutable ILNumerics array types: <see cref="Array{T}"/>, <see cref="OutArray{T}"/> and 
        /// the corresponding array types for <see cref="Logical"/>, <see cref="OutLogical"/>, <see cref="Cell"/>
        /// and <see cref="OutCell"/>.</para></remarks>
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
            set {
                SetRange(value, d0, d1, d2, d3, d4, d5, d6);
            }
        }
        /// <summary>
        /// Gets a subarray from this array or replaces the values of part of this array.
        /// </summary>
        /// <param name="dims">Array with range specification(s) for dimensions of this array.</param>
        /// <value>The right side array providing the new values.</value>
        /// <returns>Subarray according to the range specification(s) <paramref name="dims"/>...</returns>
        /// <remarks><para>This indexer supports subarray retrieval and subarray assignment 
        /// for mutable ILNumerics array types: <see cref="Array{T}"/>, <see cref="OutArray{T}"/> and 
        /// the corresponding array types for <see cref="Logical"/>, <see cref="OutLogical"/>, <see cref="Cell"/>
        /// and <see cref="OutCell"/>.</para></remarks>
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
            set {
                SetRange(value, dims); // SetRange() handles all: Expand and Remove, ML / np mode
            }
        }

        #endregion

#region single boolean special case
#if OBSOLETE
        NOTE: This overload can be enabled for single-boolean indexing. TODO: check if we need this! 
        Currently, it is forwarded to the common Subarray(d0) anyways. If it is implemented (and does
        proper memory handling !!!) uncomment this and enable this overload back again. 

        /// <summary>
        /// Gets a subarray from this array or replaces the values of part of this array.
        /// </summary>
        /// <param name="A">Boolean mask array with non-zero elements identifying the elements from this array to be retrieved / replaced.</param>
        /// <value>The right side array providing the new values.</value>
        /// <returns>Subarray with elements picked from this array according to non-zero elements in <paramref name="A"/>.</returns>
        /// <remarks><para>This indexer supports subarray retrieval and subarray assignment 
        /// for mutable ILNumerics array types: <see cref="Array{T}"/>, <see cref="OutArray{T}"/> and 
        /// the corresponding array types for <see cref="Logical"/>, <see cref="OutLogical"/>, <see cref="Cell"/>
        /// and <see cref="OutCell"/>.</para>
        /// <para>Elements from this array are addressed by a corresponding non-zero element in the mask
        /// (logical) array <paramref name="A"/>.</para></remarks>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Subarray(BaseArray[])"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, BaseArray, BaseArray, BaseArray)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.SetValue{T1, LocalT, InT, OutT, RetT, StorageT}(Mutable{T1, LocalT, InT, OutT, RetT, StorageT}, T1, long, long)"/>
        /// <exception cref="IndexOutOfRangeException">If an index lays outside of the allowed range. The exception message 
        /// will carry more details regarding the cause of this error.</exception>
        public RetT this[InLogical A] {
            get {
                return Subarray(A);
            }
            set {
                SetRange(value, A);
            }
        }
#endif
#endregion

#if OBSOLETE
        /// <summary>
        /// TODO: check if an optimization for A[A > 0] += 1 is possible via lambdas: A[a => a > 0] += 1; ? 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <remarks>A[A > 0] += 1 resolves to A[A > 0] = A[A > 0] + 1; which evaluates (creates) "A > 0" only once but 
        /// attempts to use the result twice. This crashed on the RetT result and is a common limitation in version 4.
        /// Using a lambda function in a similar manner as for apply() may brings: * removal of temporaries, * faster execution (? -> virtual function call for each element!) 
        /// and * the option to use the compound assinment operators +=, *=, -= etc... 
        /// </remarks>
        private RetT this[Func<T1, bool> predicate] {
            get {
                return null; 
            }
            set {
                throw new NotImplementedException(); 
            }
        }
#endif
    }
}
