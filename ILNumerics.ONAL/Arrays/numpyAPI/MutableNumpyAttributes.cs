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
using ILNumerics.Core.MemoryLayer;
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;

namespace ILNumerics.Core.Arrays {

    /// <summary>
    /// Base class for all mutable array types. Internal use.
    /// </summary>
    public abstract partial class Mutable<T1, LocalT, InT, OutT, RetT, StorageT>    // T1 must not read "T" to prevent from conflicts with the .T (transpose) property.
        : ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT>
        where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {

        #region HYCALPER LOOPSTART NUMPY_ATTRIBUTES

        /// <summary>
        /// [numpy API] The dimension lengths as ILNumerics vector of length corresponding to <see cref="Size.NumberOfDimensions"/>.
        /// </summary>
        /// <remarks><para>This property allows to query the dimension lengths as an ILNumerics vector. The vector returned will have 
        /// at least (<see cref="Settings.MinNumberOfArrayDimensions"/>) one dimension with the dimension lengths of this array as elements. Numpy scalars return an empty vector.</para>
        /// <para>For mutable arrays the shape can also be changed inplace by assigning to this property. Changing the shape is only allowed 
        /// for mutable arrays which are in a suitable state: the storage must be continously layed out, the number of elements must not change.</para>
        /// <para>Immutable arrays do not expose a setter property / function for the shape attribute.</para>
        /// </remarks>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Reshape(long, long, StorageOrders?)"/>
        /// <exception cref="InvalidOperationException"> if this array is currently shared with multiple instance. For example, if it is in use 
        /// by an instance of <see cref="InArray{T}"/> as function parameter and the function has not returned yet. Applying the ILNumerics function 
        /// rules prevent from such situations commonly.</exception>
        /// <exception cref="ArgumentException"> if trying to assign a vector having too many elements, having negative elements, 
        /// if the new dimensions specified would result in a different number of elements than <see cref="Size.NumberOfElements"/>, or if the storage 
        /// of this array cannot be reshaped without copying elements (<see cref="Size.IsContinuous"/> is false).</exception>
        public Array<long> shape {
            get {
                using var _1 = ReaderLock.Create(this, out var storage); 
                return storage.shape_get().RetArray;
            }

/*!HC:HC_IMMUTABLE*/
#if !IS_IMMUTABLE
            set {
                using var _1 = ReaderLock.Create(value, out var storage, throwOnNullWithMsg: "'null' cannot be assigned to the A.shape property of an array A."); 
                lock (SynchObj)
                    m_storage.shape_set(storage);
            }
#endif
        }

        /// <summary>
        /// [numpy API] The strides of elements within the dimensions. Unit: element size.
        /// </summary>
        /// <remarks>Elements within a dimension of ILNumerics arrays are separated in memory by a certain, fixed address offset. This offset is 
        /// the same for all elements within the same dimension. The array returned gives this offset for each dimension of this array.
        /// <para>Note that the unit of the distance is the <i>element size</i>, i.e. the number of bytes consumed by a single element. In difference 
        /// to that, the numpy attribute <see href="https://docs.scipy.org/doc/numpy/reference/generated/numpy.ndarray.strides.html#numpy.ndarray.strides"/> 
        /// gives this distance in <i>bytes</i>.</para>
        /// <para>This attribute is readonly.</para></remarks>
        /// <seealso cref="itemsize"/>
        /// <seealso cref="shape"/>
        public Array<long> strides {
            get {
                using var _1 = ReaderLock.Create(this, out var storage);
                return storage.strides_get().RetArray;
            }
        }

        /// <summary>
        /// [numpy API] The number of dimensions stored for this array.
        /// </summary>
        /// <remarks><para>This readonly attribute is an alias for <see cref="Size.NumberOfDimensions"/>.</para></remarks>
        /// <seealso cref="Size.NumberOfDimensions"/>
        public uint ndim {
            get {
                var ret = m_storage.Size.NumberOfDimensions;
                /*!HC:HC_MUTABLE*/
#if IS_MUTABLE
                (this as RetT)?.Release();
#endif
                return ret; 
            }
        }

        /*!HC:HC_IMMUTABLE*/
#if !IS_IMMUTABLE
        /// <summary>
        /// [numpy API][Expert API!] Gives a reference to the memory handle used to store elements of this array on the host. 
        /// </summary>
        /// <remarks><para>The memory handle returned gives access to the memory used to store the arrays data. In order 
        /// to read and/or write from / to this memory one must take many considerations into account, including but not 
        /// limited to: <see cref="shape"/>,<see cref="strides"/>,<see cref="itemsize"/>, handle type, element type,  <see cref="Size.BaseOffset"/>, <see cref="BaseArray.ReferenceCount"/>,
        /// <see cref="CountableArray.ReferenceCount"/>. Failing to do so may result in memory access violations and/or in corrupted 
        /// objects.</para>
        /// <para>Handling with memory handles directly is inherently unsafe! Neither the ILNumerics API nor the .NET CLR will 
        /// prevent you from doing wrong! Therefore, this API is for expert users only. You have been warned and you are on your own!</para>
        /// <para>The handle returned will be a <see cref="NativeHostHandle"/> for <see cref="ValueType"/> elements and a <see cref="ManagedHostHandle{T1}"/>
        /// for reference type <typeparamref name="T1"/>. Latter, in general live on the managed heap while former address unmanaged heap process memory. Good luck!</para>
        /// </remarks>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="InvalidOperationException"> when no handle is attached to the hast memory for this array.</exception>
        public MemoryHandle data {
            get {
                var ret = m_storage.Handles[0]; 
                if (Equals(ret,null)) {
                    throw new InvalidOperationException("There is currently no memory allocated for this array on the host."); 
                }
                return ret;
            }
        }
#endif 

        /// <summary>
        /// [numpy API] Gives the number of elements in this array. This attribute is an alias for <see cref="Size.NumberOfElements"/>. [readonly]
        /// </summary>
        public long size_ {
            get {
                var ret = m_storage.Size.NumberOfElements;
                /*!HC:HC_MUTABLE*/
#if IS_MUTABLE
                (this as RetT)?.Release();
#endif
                return ret;
            }
        }

        /// <summary>
        /// [numpy API] The total number of bytes required to store all elements of this array. [readonly] 
        /// </summary>
        /// <remarks>The number returned counts the bytes for the elements of the array only. It does not take into 
        /// account the memory used for non-element fields and attributes. Also, <see cref="nbytes"/> may not be 
        /// a measure for the overall size of the memory region actually _used_ to store the elements. This region
        /// might be larger if elements are non-contiguously laid-out in memory.</remarks>
        /// <seealso cref="strides"/>
        /// <seealso cref="Size.GetElementSpan()"/>
        public long nbytes {
            get {
                var ret = m_storage.Size.NumberOfElements * itemsize;
                /*!HC:HC_MUTABLE*/
#if IS_MUTABLE
                (this as RetT)?.Release();
#endif
                return ret; 
            }
        }

        /// <summary>
        /// [numpy API] The number of bytes required to store a single element <typeparamref name="T1"/> in memory. [readonly]
        /// </summary>
        /// <remarks>The number of bytes for a single element <typeparamref name="T1"/> mostly correspond to the sizeof(T1) operator in C# for numeric types. 
        /// One exception are the elements of <see cref="Logical"/> arrays which consume only 1 byte, while sizeof(bool) typically equals: 4. 
        /// <see cref="itemsize"/> can be seen as a more general variant of 'sizeof(T1)', taking these subtleties into account.</remarks>
        public uint itemsize {
            get {
                var ret = Storage<T1>.SizeOfT;
                /*!HC:HC_MUTABLE*/
#if IS_MUTABLE
                (this as RetT)?.Release();
#endif
                return ret;
            }
        }

        #region flat
        /// <summary>
        /// Readonly iterator over the elements of this array in row-major order. 
        /// </summary>
        /// <returns>Iterator object.</returns>
        /// <remarks><para>The <see cref="IEnumerable{T}"/> returned allows to iterate over the elements of an array A as if 'A.flatten' 
        /// was called first. Iteration goes along the rows (last dimension indices varying fastest). </para>
        /// <para>Note that in difference to numpys 'flatiter' objects, the source array cannot be changed via the iterator. 
        /// For assigning to elements consider utilizing a size iterator <see cref="ILNumerics.ExtensionMethods.Iterator(Size, StorageOrders?)"/> 
        /// to acquire an iterator over the element offsets in memory and use <see cref="ILNumerics.ExtensionMethods.SetValue{T1, LocalT, InT, OutT, RetT, StorageT}(Mutable{T1, LocalT, InT, OutT, RetT, StorageT}, T1, long)">Array{T1}.SetValue(T1, long)</see> or <see cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders?)</see> 
        /// to write to the memory.</para>
        /// <para><see cref="flat"/> is defined for reference type elements and for all numeric value type element arrays.</para>
        /// </remarks> 
        /// <seealso href="https://ilnumerics.net/ArrayImExport.html"/>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders?)</seealso>     
        /// <seealso cref="ILNumerics.ExtensionMethods.Iterator(Size, StorageOrders?)"/>
        public IEnumerable<T1> flat {
            get {
                return ILNumerics.ExtensionMethods.Iterator(this, StorageOrders.RowMajor);
            }
        }
        #endregion


        #endregion HYCALPER LOOPEND NUMPY_ATTRIBUTES

    }
}
