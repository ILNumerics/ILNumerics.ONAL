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
//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////
using ILNumerics.Core.Arrays;
using ILNumerics.Core.Global;
using ILNumerics.Core.Internal;
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;

namespace ILNumerics.Core.Functions.Builtin {

    internal static partial class numpyInternal {

        /// <summary>
        /// Replaces elements of <paramref name="A"/> with <paramref name="values"/> at positions given by sequential (i.e.: flatten, row-major) <paramref name="indices"/>.
        /// </summary>
        /// <typeparam name="T1">Element type of <paramref name="A"/>.</typeparam>
        /// <typeparam name="LocalT">(subtype of <paramref name="A"/>)</typeparam>
        /// <typeparam name="InT">(subtype of <paramref name="A"/>)</typeparam>
        /// <typeparam name="OutT">(subtype of <paramref name="A"/>)</typeparam>
        /// <typeparam name="RetT">(subtype of <paramref name="A"/>)</typeparam>
        /// <typeparam name="StorageT">(subtype of <paramref name="A"/>)</typeparam>
        /// <typeparam name="IndT">Element type of indices. Must be numeric.</typeparam>
        /// <param name="A">The array storing the elements to be replaced.</param>
        /// <param name="indices">Index array. The shape is ignored. Values must be numeric and are read in row-major order.</param>
        /// <param name="values">Values array. The shape is ignored. Values are read in row-major order.</param>
        /// <param name="mode">[Optionl] Specifies how to handle index values in <paramref name="indices"/> which are out-of-range. Default: error.</param>
        /// <remarks>
        /// <para>This function has a similar effect as doing <c>A.flat[indices] = values</c> in numpy. However, in ILNumerics the iterator 
        /// returned from <c>A.flat</c> is read-only. Use <see cref="put{T1, LocalT, InT, OutT, RetT, StorageT, IndT}(Mutable{T1, LocalT, InT, OutT, RetT, StorageT}, BaseArray{IndT}, InArray{T1}, PutModes)"/> as 
        /// a replacement.</para>
        /// <para>Note that the values in <paramref name="indices"/> are considered <i>sequential</i> indices, i.e. they correspond to the 
        /// element position in a flattened array, where the flattening is performed in row-major order. For performance reasons and if 
        /// <paramref name="A"/>.<see cref="Size.StorageOrder"/> is not <see cref="StorageOrders.RowMajor"/> A is converted to row-major 
        /// storage layout first and remains in row-major storage after the function returns. Note further, that the conversion happens 
        /// proactively, and is not rolled back in case of errors during the iteration.</para>
        /// <para>Elements of <paramref name="indices"/> can be negative, which corresponds to indexing from the end of flattened <paramref name="A"/>.</para>
        /// <para>Repeated values in <paramref name="indices"/> lead to only the last corresponding value in <paramref name="values"/> to be stored 
        /// at the respective position in <paramref name="A"/>.</para>
        /// <para>If <paramref name="values"/> has more elements than are indices provided in <paramref name="indices"/> superfluent values are ignored. No exception 
        /// is thrown in this case. If <paramref name="values"/> has fewer elements than indices are provided in <paramref name="indices"/> existing values 
        /// are repeated as necessary.</para>
        /// <para>The <paramref name="mode"/> parameter determines what happens with indices laying outside of the bounds of A. The default value
        /// of <see cref="PutModes.Raise"/> throws an <see cref="IndexOutOfRangeException"/> in this case. Two other options exist which bring the
        /// index back into the valid range: <see cref="PutModes.Wrap"/> computes the modulus, <see cref="PutModes.Clip"/> limits the indices to the allowed range. 
        /// Note that negative indices behave as usual (counting from the end of A) for modes <see cref="PutModes.Raise"/> and <see cref="PutModes.Wrap"/> only.</para>
        /// <para><see cref="put{T1, LocalT, InT, OutT, RetT, StorageT, IndT}(Mutable{T1, LocalT, InT, OutT, RetT, StorageT}, BaseArray{IndT}, InArray{T1}, PutModes)"/> requires 
        /// the element type of <paramref name="A"/> to be value types (struct, commonly numeric).</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if <paramref name="mode"/> is <see cref="PutModes.Raise"/> and any element 
        /// in <paramref name="indices"/> is out of the allowed range of [-A.S.NumberOfElements...&gt;= A.<see cref="Size.NumberOfElements"/>].</exception>
        /// <exception cref="ArgumentException">if either of <paramref name="indices"/> or <paramref name="values"/> is null, 
        /// if <paramref name="values"/> is empty but <paramref name="indices"/> is not, 
        /// if the specified <paramref name="mode"/> cannot successfully be applied (for example, because <paramref name="A"/> is empty).</exception>
        
        internal static unsafe void put<T1, LocalT, InT, OutT, RetT, StorageT, IndT>(Mutable<T1, LocalT, InT, OutT, RetT, StorageT> A,
                                         BaseArray<IndT> indices, InArray<T1> values, PutModes mode = PutModes.Raise)
            where IndT : struct, IConvertible
            where T1: struct
            where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {

            using (Scope.Enter(values)) {

                if (Equals(indices, null)) {
                    throw new ArgumentNullException(nameof(indices));
                }
                if (Equals(values, null)) {
                    throw new ArgumentNullException(nameof(values));
                }
                using var _i = ReaderLock.Create(indices as ConcreteArray<IndT, Array<IndT>, InArray<IndT>, OutArray<IndT>, Array<IndT>, Storage<IndT>>, out var indStorage);
                var valStorage = values.Storage;

                lock (A.SynchObj) {
                    
                    var newStorage = A.Storage;  // no reader lock on mutable arrays! 

                    // early exit
                    if (indStorage.Size.NumberOfElements == 0) {
                        return;
                    } else if (newStorage.S.NumberOfElements == 0) {
                        throw new IndexOutOfRangeException("Unable to index into an empty array.");
                    }
                    //if (A.ReferenceCount > 1) {
                    //    throw new InvalidOperationException("This array is currently part of a multi-array set and cannot change. Please follow the ILNumerics function rules! This commonly prevents from this error.");
                    //}

                    var mustDetach = newStorage.Handles.ReferenceCount > 1;
                    if (newStorage.Size.StorageOrder != StorageOrders.RowMajor || mustDetach) {
                        newStorage = newStorage.EnsureStorageOrder(StorageOrders.RowMajor, mustDetach, inplace: false);
                    }

                    // index modes: do no error checking which requires a full iteration over the indices array (performance)
                    var lastIdx = mode == PutModes.Raise ? newStorage.S.NumberOfElements - 1 : -1;
                    var indIt = indStorage.AsRetArray().IndexIterator(lastDimIdx: lastIdx, order: StorageOrders.RowMajor).GetEnumerator();

                    switch (Storage<T1>.SizeOfT) {
                        case 1:
                            put_1((byte*)newStorage.Handles[0].Pointer + newStorage.S.BaseOffset, newStorage.S.NumberOfElements, indIt, (byte*)valStorage.Handles[0].Pointer, valStorage.Size, mode);
                            break;
                        case 2:
                            put_2((ushort*)((byte*)newStorage.Handles[0].Pointer + newStorage.S.BaseOffset), newStorage.S.NumberOfElements, indIt, (ushort*)valStorage.Handles[0].Pointer, valStorage.Size, mode);
                            break;
                        case 4:
                            put_4((uint*)((byte*)newStorage.Handles[0].Pointer + newStorage.S.BaseOffset), newStorage.S.NumberOfElements, indIt, (uint*)valStorage.Handles[0].Pointer, valStorage.Size, mode);
                            break;
                        case 8:
                            put_8((ulong*)((byte*)newStorage.Handles[0].Pointer + newStorage.S.BaseOffset), newStorage.S.NumberOfElements, indIt, (ulong*)valStorage.Handles[0].Pointer, valStorage.Size, mode);
                            break;
                        case 16:
                            put_16((complex*)((byte*)newStorage.Handles[0].Pointer + newStorage.S.BaseOffset), newStorage.S.NumberOfElements, indIt, (complex*)valStorage.Handles[0].Pointer, valStorage.Size, mode);
                            break;
                        default:
                            throw new NotSupportedException($"Supported element types are of length 1,2,4,8 or 16 bytes.");
                    }
                    A.Storage.Assign(newStorage, true, false, false);
                }
            }
        }


        #region HYCALPER LOOPSTART 
        /*!HC:TYPELIST:
<hycalper>
<type>
    <source locate="here">
        byte
    </source>
    <destination>ushort</destination>
    <destination>uint</destination>
    <destination>ulong</destination>
    <destination>complex</destination>
</type>
<type>
    <source locate="here">
        put_1
    </source>
    <destination>put_2</destination>
    <destination>put_4</destination>
    <destination>put_8</destination>
    <destination>put_16</destination>
</type>
</hycalper>
*/

        /// <summary>
        /// Performs copy operation for numpy.put.
        /// </summary>
        /// <param name="pA">First element of A pointer. Base offset considered.</param>
        /// <param name="lenA">Number of elements of A.</param>
        /// <param name="indices">Indices iterator.</param>
        /// <param name="pV">Base pointer for values array. No base offset considered.</param>
        /// <param name="mode">How to handle out-of-range indices. See: <see cref="PutModes"/>.</param>
        /// <param name="sizeV">Size descriptor for values array.</param>
        private unsafe static void put_1(byte* pA, long lenA, IEnumerator<long> indices, byte* pV, Size sizeV, PutModes mode) {

            var vIt = sizeV.Iterator(StorageOrders.RowMajor).GetEnumerator(); 
            while (indices.MoveNext()) {
                if (!vIt.MoveNext()) {
                    vIt.Reset(); 
                    if (!vIt.MoveNext()) {
                        throw new ArgumentException("Values argument must not be empty.");
                    }
                }
                long i = indices.Current; 
                // negative indices are handled in the iterator! 
                //if (i < 0) {
                //    i += lenA; 
                //}
                if ((ulong)i >= (ulong)lenA) {
                    switch (mode) {
                        case PutModes.Raise:
                            throw new IndexOutOfRangeException($"Index {i} is out of the allowed range:  [-{lenA}<= i <{lenA}].");

                        case PutModes.Wrap:  // no negative handling in iterators (lastDimIdx was set to -1)
                            i %= lenA;
                            if (i < 0) i += lenA;
                            System.Diagnostics.Debug.Assert(i >= 0 && i < lenA); 
                            break;
                        case PutModes.Clip: // no negative handling in iterators (lastDimIdx was set to -1)
                            if (i < 0) i = 0;
                            else if (i >= lenA) i = lenA - 1; 
                            break; 
                    }
                }
                pA[i] = pV[vIt.Current]; 

            }
        }

#endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        /// <summary>
        /// Performs copy operation for numpy.put.
        /// </summary>
        /// <param name="pA">First element of A pointer. Base offset considered.</param>
        /// <param name="lenA">Number of elements of A.</param>
        /// <param name="indices">Indices iterator.</param>
        /// <param name="pV">Base pointer for values array. No base offset considered.</param>
        /// <param name="mode">How to handle out-of-range indices. See: <see cref="PutModes"/>.</param>
        /// <param name="sizeV">Size descriptor for values array.</param>
        private unsafe static void put_16(complex* pA, long lenA, IEnumerator<long> indices, complex* pV, Size sizeV, PutModes mode) {

            var vIt = sizeV.Iterator(StorageOrders.RowMajor).GetEnumerator(); 
            while (indices.MoveNext()) {
                if (!vIt.MoveNext()) {
                    vIt.Reset(); 
                    if (!vIt.MoveNext()) {
                        throw new ArgumentException("Values argument must not be empty.");
                    }
                }
                long i = indices.Current; 
                // negative indices are handled in the iterator! 
                //if (i < 0) {
                //    i += lenA; 
                //}
                if ((ulong)i >= (ulong)lenA) {
                    switch (mode) {
                        case PutModes.Raise:
                            throw new IndexOutOfRangeException($"Index {i} is out of the allowed range:  [-{lenA}<= i <{lenA}].");

                        case PutModes.Wrap:  // no negative handling in iterators (lastDimIdx was set to -1)
                            i %= lenA;
                            if (i < 0) i += lenA;
                            System.Diagnostics.Debug.Assert(i >= 0 && i < lenA); 
                            break;
                        case PutModes.Clip: // no negative handling in iterators (lastDimIdx was set to -1)
                            if (i < 0) i = 0;
                            else if (i >= lenA) i = lenA - 1; 
                            break; 
                    }
                }
                pA[i] = pV[vIt.Current]; 

            }
        }

       

        /// <summary>
        /// Performs copy operation for numpy.put.
        /// </summary>
        /// <param name="pA">First element of A pointer. Base offset considered.</param>
        /// <param name="lenA">Number of elements of A.</param>
        /// <param name="indices">Indices iterator.</param>
        /// <param name="pV">Base pointer for values array. No base offset considered.</param>
        /// <param name="mode">How to handle out-of-range indices. See: <see cref="PutModes"/>.</param>
        /// <param name="sizeV">Size descriptor for values array.</param>
        private unsafe static void put_8(ulong* pA, long lenA, IEnumerator<long> indices, ulong* pV, Size sizeV, PutModes mode) {

            var vIt = sizeV.Iterator(StorageOrders.RowMajor).GetEnumerator(); 
            while (indices.MoveNext()) {
                if (!vIt.MoveNext()) {
                    vIt.Reset(); 
                    if (!vIt.MoveNext()) {
                        throw new ArgumentException("Values argument must not be empty.");
                    }
                }
                long i = indices.Current; 
                // negative indices are handled in the iterator! 
                //if (i < 0) {
                //    i += lenA; 
                //}
                if ((ulong)i >= (ulong)lenA) {
                    switch (mode) {
                        case PutModes.Raise:
                            throw new IndexOutOfRangeException($"Index {i} is out of the allowed range:  [-{lenA}<= i <{lenA}].");

                        case PutModes.Wrap:  // no negative handling in iterators (lastDimIdx was set to -1)
                            i %= lenA;
                            if (i < 0) i += lenA;
                            System.Diagnostics.Debug.Assert(i >= 0 && i < lenA); 
                            break;
                        case PutModes.Clip: // no negative handling in iterators (lastDimIdx was set to -1)
                            if (i < 0) i = 0;
                            else if (i >= lenA) i = lenA - 1; 
                            break; 
                    }
                }
                pA[i] = pV[vIt.Current]; 

            }
        }

       

        /// <summary>
        /// Performs copy operation for numpy.put.
        /// </summary>
        /// <param name="pA">First element of A pointer. Base offset considered.</param>
        /// <param name="lenA">Number of elements of A.</param>
        /// <param name="indices">Indices iterator.</param>
        /// <param name="pV">Base pointer for values array. No base offset considered.</param>
        /// <param name="mode">How to handle out-of-range indices. See: <see cref="PutModes"/>.</param>
        /// <param name="sizeV">Size descriptor for values array.</param>
        private unsafe static void put_4(uint* pA, long lenA, IEnumerator<long> indices, uint* pV, Size sizeV, PutModes mode) {

            var vIt = sizeV.Iterator(StorageOrders.RowMajor).GetEnumerator(); 
            while (indices.MoveNext()) {
                if (!vIt.MoveNext()) {
                    vIt.Reset(); 
                    if (!vIt.MoveNext()) {
                        throw new ArgumentException("Values argument must not be empty.");
                    }
                }
                long i = indices.Current; 
                // negative indices are handled in the iterator! 
                //if (i < 0) {
                //    i += lenA; 
                //}
                if ((ulong)i >= (ulong)lenA) {
                    switch (mode) {
                        case PutModes.Raise:
                            throw new IndexOutOfRangeException($"Index {i} is out of the allowed range:  [-{lenA}<= i <{lenA}].");

                        case PutModes.Wrap:  // no negative handling in iterators (lastDimIdx was set to -1)
                            i %= lenA;
                            if (i < 0) i += lenA;
                            System.Diagnostics.Debug.Assert(i >= 0 && i < lenA); 
                            break;
                        case PutModes.Clip: // no negative handling in iterators (lastDimIdx was set to -1)
                            if (i < 0) i = 0;
                            else if (i >= lenA) i = lenA - 1; 
                            break; 
                    }
                }
                pA[i] = pV[vIt.Current]; 

            }
        }

       

        /// <summary>
        /// Performs copy operation for numpy.put.
        /// </summary>
        /// <param name="pA">First element of A pointer. Base offset considered.</param>
        /// <param name="lenA">Number of elements of A.</param>
        /// <param name="indices">Indices iterator.</param>
        /// <param name="pV">Base pointer for values array. No base offset considered.</param>
        /// <param name="mode">How to handle out-of-range indices. See: <see cref="PutModes"/>.</param>
        /// <param name="sizeV">Size descriptor for values array.</param>
        private unsafe static void put_2(ushort* pA, long lenA, IEnumerator<long> indices, ushort* pV, Size sizeV, PutModes mode) {

            var vIt = sizeV.Iterator(StorageOrders.RowMajor).GetEnumerator(); 
            while (indices.MoveNext()) {
                if (!vIt.MoveNext()) {
                    vIt.Reset(); 
                    if (!vIt.MoveNext()) {
                        throw new ArgumentException("Values argument must not be empty.");
                    }
                }
                long i = indices.Current; 
                // negative indices are handled in the iterator! 
                //if (i < 0) {
                //    i += lenA; 
                //}
                if ((ulong)i >= (ulong)lenA) {
                    switch (mode) {
                        case PutModes.Raise:
                            throw new IndexOutOfRangeException($"Index {i} is out of the allowed range:  [-{lenA}<= i <{lenA}].");

                        case PutModes.Wrap:  // no negative handling in iterators (lastDimIdx was set to -1)
                            i %= lenA;
                            if (i < 0) i += lenA;
                            System.Diagnostics.Debug.Assert(i >= 0 && i < lenA); 
                            break;
                        case PutModes.Clip: // no negative handling in iterators (lastDimIdx was set to -1)
                            if (i < 0) i = 0;
                            else if (i >= lenA) i = lenA - 1; 
                            break; 
                    }
                }
                pA[i] = pV[vIt.Current]; 

            }
        }


#endregion HYCALPER AUTO GENERATED CODE
    }
}
