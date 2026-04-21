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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILNumerics {

    public unsafe partial class Size {

        #region GetSeqIndex via long indices
        /// <summary>
        /// Retrieves the storage element index into an array stored according to this size descriptor object
        /// based on the provided sequential index <paramref name="d0"/>. Allows negative indices. 
        /// </summary>
        /// <param name="d0">Index into the first dimension / sequential index.</param>
        /// <returns>Storage element index addressing the element in memory.</returns>
        /// <remarks><para>For arrays of column vector shape <paramref name="d0"/> references the index of the row of the 
        /// storage element to return.</para>
        /// <para>More general, for <see cref="NumberOfDimensions">n-dimensional</see> arrays the first index corresponds 
        /// to the position in the first (index 0) dimension.</para>
        /// <para>If the array refered to by this size stores more dimensions than addressable by this function
        /// <paramref name="d0"/> can exceed the limits of the first dimension. In this 
        /// case the storage element index to be returned is computed by subsequently merging trailing dimensions and applying 
        /// the superflous modulos of the value of <paramref name="d0"/> to the next dimension, correspondingly. This process 
        /// is repeated until either the number of dimensions is reached or the superflous indices reach the value of 0.</para>
        /// <para><paramref name="d0"/> may be negative, in which case the resulting index is computed by adding the 
        /// number of elements in the array to the parameters value. Hence, a value of -1 addresses the last element in the array.</para>
        /// <para>This function recognizes arbitrary strides. It expects all sizes (dimension lengths, strides, the base offset 
        /// and the number of elements) to fit into <see cref="long.MaxValue"/>. Consider using one of the overloads with 
        /// <see cref="System.UInt32"/> parameters if you are not dealing with very big arrays and don't require negative indexing.
        /// This may bring better performance in tight loops.</para>
        /// <para>Performance hint: this function (and all corresponding overloads) are optimized for the case
        /// where the number of index parameters provided corresponds to the number of dimensions in the array and all 
        /// provided index parameters are within the range of their corresponding dimension. The functions, however, handle 
        /// arbitrary cases, including addressing, merging and ommitting trailing dimensions. Any of those extended features
        /// may introduce a performance penalty, though.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if the leading dimension specifier <paramref name="d0"/> is equal to
        /// or greater than the <see cref="NumberOfElements"/>.</exception>
        public long GetSeqIndex(long d0) {
            if (d0 < 0) {
                d0 += m_descriptor[1];
            }
            if ((ulong)d0 >= (ulong)m_descriptor[1]) {
                throw new IndexOutOfRangeException($"Index into dimension #0 {{value:{ d0 }}} is outside of the range of this array. 0 <= d0 < {m_descriptor[1]}");
            }
            //if (m_descriptor[0] < 1) {
            //    // numpy scalar
            //    System.Diagnostics.Debug.Assert(m_descriptor[1] == 1);
            //    return m_descriptor[2]; 
            //}
            if (StorageOrder == StorageOrders.ColumnMajor || m_descriptor[0] == 0) {  // check for 0-dim scalars explicitly. They are sometimes not ColumnMajor.
                // this includes numpy 0-dim scalars
                return m_descriptor[2] + d0;
            } else if (d0 >= m_descriptor[3]) {
                // if we end up here, we have 
                // * at least one dimension (scalars are catched above as being col.maj.)
                // * at least two dimension since d0 is larger than dimlen(0) and numel > dimlen(0). 
                System.Diagnostics.Debug.Assert((uint)m_descriptor[0] > 1);
                if (m_descriptor[1] == 0) {
                    // this is an empty array. so indices provided are always out of range here!
                    throw new IndexOutOfRangeException($"Index into empty array is out of the existing range!");
                }

                // forward to more-parameter overload
                return GetSeqIndex(d0 % m_descriptor[3], d0 / m_descriptor[3]);
            }
            return m_descriptor[2] + d0 * m_descriptor[NumberOfDimensions + 3];
        }

        /// <summary>
        /// Retrieves the sequential index into an array stored according to this size descriptor object 
        /// based on the provided indices <paramref name="d0"/> ... <paramref name="d1"/>.  Allows negative indices. 
        /// </summary>
        /// <param name="d0">Index into the first dimension.</param>
        /// <param name="d1">Index into the second dimension.</param>
        /// <returns>Storage element index addressing the element in memory.</returns>
        /// <remarks><para>For arrays of matrix shape <paramref name="d0"/> references the index of the row and 
        /// <paramref name="d1"/> references the index of the column of the sequential index to return.</para>
        /// <para>More general, for <see cref="NumberOfDimensions">n-dimensional</see> arrays the first index corresponds 
        /// to the position in the first (index: 0) dimension and the second index to the position of the element in the 
        /// second dimension (index: 1).</para>
        /// <para>If the array refered to by this size stores more dimensions than addressed by this function
        /// the last index parameter <paramref name="d1"/> can exceed the limits of its corresponding dimension. In this 
        /// case the sequential index to be returned is computed by subsequently merging trailing dimensions and applying 
        /// the superflous modulos of the value of <paramref name="d1"/> to the next dimension, correspondingly. This process 
        /// is repeated until either the number of dimensions is reached or the modified new value of <paramref name="d1"/> 
        /// reaches 0.</para>
        /// <para>Any index parameter may be negative, in which case the resulting index is computed by adding the 
        /// corresponding dimension length to the parameters value. Hence, a value of -1 addresses the last element in the dimension. 
        /// Note, that for the last specified dimension a value of -1 references the last element in the _virtual_ dimension,
        /// merged with any non specified trailing dimensions.</para>
        /// <para>This function recognizes arbitrary strides. It expects all sizes (dimension lengths, strides, the base offset 
        /// and the number of elements) to fit into <see cref="long.MaxValue"/>. Consider using one of the overloads with 
        /// <see cref="System.UInt32"/> parameters if you are not dealing with very big arrays and don't require negative indexing.
        /// This may bring better performance in tight loops.</para>
        /// <para>Performance hint: this function (and all corresponding overloads) are optimized for the case
        /// where the number of index parameters provided corresponds to the number of dimensions in the array and all 
        /// provided index parameters are within the range of their corresponding dimension. The functions, however, handle 
        /// arbitrary cases, including addressing, merging and ommitting trailing dimensions. Any of those extended features
        /// may introduce a performance penalty, though.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if the leading dimension specifier <paramref name="d0"/> is equal to
        /// or greater than the length of the first dimension.</exception>
        /// <exception cref="IndexOutOfRangeException"> if <paramref name="d1"/> is equal or greater than the length of 
        /// the second dimension and the resulting index points to a non-existing element.</exception>
        public long GetSeqIndex(long d0, long d1) {

            /* 
             * * Must allow virtual trailing singleton dimensions. 
             * * Must recognize merged dimensions. 
             * * Must throw on IndexOutOfRange per dimension. 
             * * - all but last dim spec in range
             * * - ret: overall element count
             * * Must recognize arbitrary strides. 
             * * allows negative indices.
             * * Must be efficient! 
             */

            uint nrDims = NumberOfDimensions;
            if (nrDims < 2) {
                if (d1 != 0 && d1 != -1) throw new IndexOutOfRangeException("Index specifiers for (virtual) trailing dimensions must be 0 or -1. Check: d1");
                return GetSeqIndex(d0);
            }
            // d0
            if (d0 < 0) {
                d0 += m_descriptor[3];
            }
            if ((ulong)d0 >= (ulong)m_descriptor[3]) { 
                throw new IndexOutOfRangeException($"Index into dimension #0 {{value:{ d0 }}} is outside the range of this array. 0 <= d0 < { m_descriptor[3]}");
            }
            if ((ulong)d1 >= (ulong)m_descriptor[4]) {
                if (m_descriptor[1] == 0) {
                    // this is an empty array. so indices provided are always out of range here!
                    throw new IndexOutOfRangeException($"Index into empty array is out of the existing range!");
                }
                // last parameter negativ or t.b. merged with trailing dimension(s)
                var lastDimLen = m_descriptor[1] / m_descriptor[3];
                if (d1 < 0) {
                    d1 += lastDimLen;
                }
                if ((ulong)d1 >= (ulong)lastDimLen) {
                    throw new IndexOutOfRangeException($"Index into dimension #1 {{value:{ d1 }}} is outside of the range of this array. 0 <= d1 < { lastDimLen }.");
                }
                if (d1 >= m_descriptor[4]) { // v5.4: was: '... && StorageOrder != StorageOrders.ColumnMajor) {' - assuming old-style striding. But know we store 0 strides for singleton dimensions. Hence, this does not work anymore.

                    // forward to more-parameter overload
                    return GetSeqIndex(d0,
                                       d1 % m_descriptor[4], 
                                       d1 / m_descriptor[4]);
                }
            }
            // now last index is either in dim range or ColumnMajor!
            return (m_descriptor[2]
                    + d0 * m_descriptor[3 + nrDims]
                    + d1 * m_descriptor[4 + nrDims]);
        }

        /// <summary>
        /// Retrieves the sequential index into an array stored according to this size descriptor object based 
        /// on the provided indices <paramref name="d0"/> ... <paramref name="d2"/>. Allows negative indices. 
        /// </summary>
        /// <param name="d0">Index into the first dimension.</param>
        /// <param name="d1">Index into the second dimension.</param>
        /// <param name="d2">Index into the third dimension.</param>
        /// <returns>Sequential index into the array when stored as 1D array of arbitrary storage order.</returns>
        /// <remarks><para>For arrays of matrix shape <paramref name="d0"/> references the index of the row and 
        /// <paramref name="d1"/> references the index of the column of the sequential index to return.</para>
        /// <para>More general, for <see cref="NumberOfDimensions">n-dimensional</see> arrays the first index corresponds 
        /// to the position in the first (index: 0) dimension, the second index to the position of the element in the 
        /// second dimension (index: 1), a.s.o.</para>
        /// <para>If the array addressed by this size has less dimensions than addressed by this function, trailing indices 
        /// (i.e.: such indices which deal with a dimension whose index is higher than existing in the array)
        /// correspond to 'virtual' or singleton dimensions. Those indices must address the 0-th element of the 
        /// virtual dimension. Hence its value must be 0.</para>
        /// <para>Conversely, if the array addressed by this size stores more dimensions than addressed by this function
        /// the last index parameter <paramref name="d2"/> may exceeds the limit of its corresponding dimension. In this 
        /// case the sequential index to be returned is computed by subsequently merging trailing dimensions and applying 
        /// the superflous modulus of the value of <paramref name="d2"/> to the next dimension, correspondingly. This process 
        /// is repeated until either the number of dimensions is reached or the modified new value of <paramref name="d2"/> 
        /// reaches 0.</para>
        /// <para>Any index parameter may be negative, in which case the resulting index is computed by adding the 
        /// corresponding dimension length to the parameters value. Hence, a value of -1 addresses the last element in the dimension. 
        /// Note, that for the last specified dimension a value of -1 references the last element in the _virtual_ dimension,
        /// merged with any non specified trailing dimensions.</para>
        /// <para>This function recognizes arbitrarily strided size objects. It expects all sizes (dimension lengths, strides, the base offset 
        /// and the number of elements) to fit into <see cref="long.MaxValue"/>. Consider using one of the overloads with 
        /// <see cref="System.UInt32"/> parameters if you are not dealing with very big arrays and don't require negative indexing.
        /// This may bring better performance in tight loops.</para>
        /// <para>Performance hint: this function (and all corresponding overloads) are optimized for the case
        /// where the number of index parameters provided corresponds to the number of dimensions in the array and all 
        /// provided index parameters are within the range of their corresponding dimension. The functions, however, handle 
        /// arbitrary cases, including addressing, merging and ommitting trailing dimensions. Any of those extended features
        /// may introduce a performance penalty, though.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if the leading dimension specifier <paramref name="d0"/> is equal 
        /// or greater than the length of the first dimension.</exception>
        /// <exception cref="IndexOutOfRangeException"> if the leading dimension specifier <paramref name="d1"/> is equal 
        /// or greater than the length of the second dimension.</exception>
        /// <exception cref="IndexOutOfRangeException"> if <paramref name="d2"/> is equal or greater than the length of the 
        /// third dimension and the resulting index points to an non-existing element.</exception>
        public long GetSeqIndex(long d0, long d1, long d2) {
            /* 
             * * Must allow virtual trailing singleton dimensions. 
             * * Must recognize merged dimensions. 
             * * Accepts negative indices
             * * Works on arrays with fewer dimensions
             * * Must throw on IndexOutOfRange. 
             * * - all but last dim spec in range
             * * - ret: overall element count
             * * RESTORED: Must recognize arbitrary strides. 
             * * Must be efficient! 
             */
            uint nrDims = NumberOfDimensions;
            if (nrDims < 3) {
                if (d2 != 0 && d2 != -1) throw new IndexOutOfRangeException("Index specifiers for (virtual) trailing dimensions must be 0 or -1. Check: d2");
                return GetSeqIndex(d0, d1);
            }
            // d0
            if (d0 < 0) {
                d0 += m_descriptor[3];
            }
            if ((ulong)d0 >= (ulong)m_descriptor[3]) {
                throw new IndexOutOfRangeException($"Index into dimension #0 {{value:{ d0 }}} is outside the range of this array. 0 <= d0 < { m_descriptor[3]}");
            }
            // d1
            if (d1 < 0) {
                d1 += m_descriptor[4];
            }
            if ((ulong)d1 >= (ulong)m_descriptor[4]) {
                throw new IndexOutOfRangeException($"Index into dimension #1 {{value:{ d1 }}} is outside the range of this array. 0 <= d1 < { m_descriptor[4] }.");
            }
            // d2
            if ((ulong)d2 >= (ulong)m_descriptor[5]) {
                if (m_descriptor[1] == 0) {
                    // this is an empty array. so indices provided are always out of range here!
                    throw new IndexOutOfRangeException($"Index into empty array is out of the existing range!");
                }
                // last parameter negativ or t.b. merged with trailing dimension(s)
                var lastDimLen = m_descriptor[1] / (m_descriptor[3] * m_descriptor[4]);
                if (d2 < 0) {
                    d2 += lastDimLen;
                }
                if ((ulong)d2 >= (ulong)lastDimLen) {
                    throw new IndexOutOfRangeException($"Index into dimension #2 {{value:{ d2 }}} is outside of the range of this array. 0 <= d2 < { lastDimLen }.");
                }
                if (d2 >= m_descriptor[5]) { // v5.4: was: '... && StorageOrder != StorageOrders.ColumnMajor) {' - assuming old-style striding. But know we store 0 strides for singleton dimensions. Hence, this does not work anymore.
                    // forward to more-parameter overload
                    return GetSeqIndex(d0,
                                       d1,
                                       d2 % m_descriptor[5],
                                       d2 / m_descriptor[5]);
                }
            }
            // now last index is either in dim range or ColumnMajor!
            return (m_descriptor[2]
                    + d0 * m_descriptor[3 + nrDims]
                    + d1 * m_descriptor[4 + nrDims]
                    + d2 * m_descriptor[5 + nrDims]);
        }

        /// <summary>
        /// Retrieves the sequential index into an array stored according to this size descriptor object 
        /// based on the provided indices <paramref name="d0"/> ... <paramref name="d3"/>. Allows negative indices.  
        /// </summary>
        /// <param name="d0">Index into the first dimension.</param>
        /// <param name="d1">Index into the second dimension.</param>
        /// <param name="d2">Index into the third dimension.</param>
        /// <param name="d3">Index into the fourth dimension.</param>
        /// <returns>Sequential index into the array when stored as 1D array of arbitrary storage order.</returns>
        /// <remarks><para>For arrays of matrix shape <paramref name="d0"/> references the index of the row and 
        /// <paramref name="d1"/> references the index of the column of the sequential index to return.</para>
        /// <para>More general, for <see cref="NumberOfDimensions">n-dimensional</see> arrays the first index corresponds 
        /// to the position in the first (index: 0) dimension, the second index to the position of the element in the 
        /// second dimension (index: 1) and so forth ...</para>
        /// <para>If the array addressed by this size has less dimensions than addressed by this function, trailing indices 
        /// (i.e.: such indices which deal with a dimension whose index is higher than existing in the array)
        /// correspond to 'virtual' or singleton dimensions. Those indices must address the 0-th element of the 
        /// virtual dimension. Hence its value must be 0.</para>
        /// <para>Conversely, if the array addressed by this size stores more dimensions than addressed by this function
        /// the last index parameter <paramref name="d3"/> may exceeds the limit of its corresponding dimension. In this 
        /// case the sequential index to be returned is computed by subsequently merging trailing dimensions and applying 
        /// the superflous modulus of the value of <paramref name="d3"/> to the next dimension, correspondingly. This process 
        /// is repeated until either the number of dimensions is reached or the modified new value of <paramref name="d3"/> 
        /// reaches 0.</para>
        /// <para>Any index parameter may be negative, in which case the resulting index is computed by adding the 
        /// corresponding dimension length to the parameters value. Hence, a value of -1 addresses the last element in the dimension. 
        /// Note, that for the last specified dimension a value of -1 references the last element in the _virtual_ dimension,
        /// merged with any non specified trailing dimensions.</para>
        /// <para>This function recognizes arbitrarily strided size objects. It expects all sizes (dimension lengths, strides, the base offset 
        /// and the number of elements) to fit into <see cref="long.MaxValue"/>. Consider using one of the overloads with 
        /// <see cref="System.UInt32"/> parameters if you are not dealing with very big arrays and don't require negative indexing.
        /// This may bring better performance in tight loops.</para>
        /// <para>Performance hint: this function (and all corresponding overloads) are optimized for the case
        /// where the number of index parameters provided corresponds to the number of dimensions in the array and all 
        /// provided index parameters are within the range of their corresponding dimension. The functions, however, handle 
        /// arbitrary cases, including addressing, merging and ommitting trailing dimensions. Any of those extended features
        /// may introduce a performance penalty, though.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if any of the leading dimension specifiers <paramref name="d0"/> ... 
        /// <paramref name="d2"/> is equal or greater than the length of its corresponding dimension.</exception>
        /// <exception cref="IndexOutOfRangeException"> if <paramref name="d3"/> is equal or greater than the length 
        /// of the fourth dimension and the resulting index after merging trailing dimensions points to a non-existing element.</exception>
        public long GetSeqIndex(long d0, long d1, long d2, long d3) {
            /* 
             * * Must allow virtual trailing singleton dimensions. 
             * * Must recognize merged dimensions. 
             * * Accepts negative indices
             * * Works on arrays with fewer dimensions
             * * Must throw on IndexOutOfRange. 
             * * - all but last dim spec in range
             * * - ret: overall element count
             * * RESTORED: Must recognize arbitrary strides. 
             * * Must be efficient! 
             */

            uint nrDims = NumberOfDimensions;
            if (nrDims < 4) {
                if (d3 != 0 && d3 != -1) {
                    throw new IndexOutOfRangeException($"Index specifiers for (virtual) trailing dimensions must be 0 or -1. Check parameter: {nameof(d3)}!");
                }
                return GetSeqIndex(d0, d1, d2);
            }
            // d0
            if (d0 < 0) {
                d0 += m_descriptor[3];
            }
            if ((ulong)d0 >= (ulong)m_descriptor[3]) {
                throw new IndexOutOfRangeException($"Index into dimension #0 {{value:{ d0 }}} is outside the range of this array. 0 <= d0 < { m_descriptor[3]}");
            }
            // d1
            if (d1 < 0) {
                d1 += m_descriptor[4];
            }
            if ((ulong)d1 >= (ulong)m_descriptor[4]) {
                throw new IndexOutOfRangeException($"Index into dimension #1 {{value:{ d1 }}} is outside the range of this array. 0 <= d1 < { m_descriptor[4] }.");
            }
            // d2
            if (d2 < 0) {
                d2 += m_descriptor[5];
            }
            if ((ulong)d2 >= (ulong)m_descriptor[5]) {
                throw new IndexOutOfRangeException($"Index into dimension #2 {{value:{ d2 }}} is outside the range of this array. 0 <= d2 < { m_descriptor[5] }.");
            }
            // d3
            if ((ulong)d3 >= (ulong)m_descriptor[6]) {
                if (m_descriptor[1] == 0) {
                    // this is an empty array. so indices provided are always out of range here!
                    throw new IndexOutOfRangeException($"Index into empty array is out of the existing range!");
                }
                // last parameter negativ or t.b. merged with trailing dimension(s)
                var lastDimLen = m_descriptor[1] / (m_descriptor[3] * m_descriptor[4] * m_descriptor[5]);
                if (d3 < 0) {
                    d3 += lastDimLen;
                }
                if ((ulong)d3 >= (ulong)lastDimLen) {
                    throw new IndexOutOfRangeException($"Index into dimension #3 {{value:{ d3 }}} is outside of the range of this array. 0 <= d3 < { lastDimLen }.");
                }
                if (d3 >= m_descriptor[6]) { // v5.4: was: '... && StorageOrder != StorageOrders.ColumnMajor) {' - assuming old-style striding. But know we store 0 strides for singleton dimensions. Hence, this does not work anymore.
                    // forward to more-parameter overload
                    return GetSeqIndex(d0,
                                   d1,
                                   d2,
                                   d3 % m_descriptor[6],
                                   d3 / m_descriptor[6]);
                }
            }
            // now last index is either in dim range or ColumnMajor!
            return (m_descriptor[2]
                    + d0 * m_descriptor[3 + nrDims]
                    + d1 * m_descriptor[4 + nrDims]
                    + d2 * m_descriptor[5 + nrDims]
                    + d3 * m_descriptor[6 + nrDims]);
        }

        /// <summary>
        /// Retrieves the sequential index into an array stored according to this size descriptor object 
        /// based on the provided indices <paramref name="d0"/> ... <paramref name="d4"/>. Allows negative indices.  
        /// </summary>
        /// <param name="d0">Index into the first dimension.</param>
        /// <param name="d1">Index into the second dimension.</param>
        /// <param name="d2">Index into the third dimension.</param>
        /// <param name="d3">Index into the fourth dimension.</param>
        /// <param name="d4">Index into the fives dimension.</param>
        /// <returns>Sequential index into the array when stored as 1D array of arbitrary storage order.</returns>
        /// <remarks><para>For arrays of matrix shape <paramref name="d0"/> references the index of the row and 
        /// <paramref name="d1"/> references the index of the column of the sequential index to return.</para>
        /// <para>More general, for <see cref="NumberOfDimensions">n-dimensional</see> arrays the first index corresponds 
        /// to the position in the first (index: 0) dimension, the second index to the position of the element in the 
        /// second dimension (index: 1) and so forth ...</para>
        /// <para>If the array addressed by this size has less dimensions than addressed by this function, trailing indices 
        /// (i.e.: such indices which deal with a dimension whose index is higher than existing in the array)
        /// correspond to 'virtual' or singleton dimensions. Those indices must address the 0-th element of the 
        /// virtual dimension. Hence its value must be 0.</para>
        /// <para>Conversely, if the array addressed by this size stores more dimensions than addressed by this function
        /// the last index parameter <paramref name="d4"/> may exceeds the limit of its corresponding dimension. In this 
        /// case the sequential index to be returned is computed by subsequently merging trailing dimensions and applying 
        /// the superflous modulus of the value of <paramref name="d4"/> to the next dimension, correspondingly. This process 
        /// is repeated until either the number of dimensions is reached or the modified new value of <paramref name="d4"/> 
        /// reaches 0.</para>
        /// <para>Any index parameter may be negative, in which case the resulting index is computed by adding the 
        /// corresponding dimension length to the parameters value. Hence, a value of -1 addresses the last element in the dimension. 
        /// Note, that for the last specified dimension a value of -1 references the last element in the _virtual_ dimension,
        /// merged with any non specified trailing dimensions.</para>
        /// <para>This function recognizes arbitrarily strided size objects. It expects all sizes (dimension lengths, strides, the base offset 
        /// and the number of elements) to fit into <see cref="long.MaxValue"/>. Consider using one of the overloads with 
        /// <see cref="System.UInt32"/> parameters if you are not dealing with very big arrays and don't require negative indexing.
        /// This may bring better performance in tight loops.</para>
        /// <para>Performance hint: this function (and all corresponding overloads) are optimized for the case
        /// where the number of index parameters provided corresponds to the number of dimensions in the array and all 
        /// provided index parameters are within the range of their corresponding dimension. The functions, however, handle 
        /// arbitrary cases, including addressing, merging and ommitting trailing dimensions. Any of those extended features
        /// may introduce a performance penalty, though.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if any of the leading dimension specifiers <paramref name="d0"/> ... 
        /// <paramref name="d3"/> is equal or greater than the length of its corresponding dimension.</exception>
        /// <exception cref="IndexOutOfRangeException"> if <paramref name="d4"/> is equal or greater than the length 
        /// of the fives dimension and the resulting index after merging trailing dimensions points to a non-existing element.</exception>
        public long GetSeqIndex(long d0, long d1, long d2, long d3, long d4) {
            /* 
             * * Must allow virtual trailing singleton dimensions. 
             * * Must recognize merged dimensions. 
             * * Accepts negative indices
             * * Works on arrays with fewer dimensions
             * * Must throw on IndexOutOfRange. 
             * * - all but last dim spec in range
             * * - ret: overall element count
             * * RESTORED: Must recognize arbitrary strides. 
             * * Must be efficient! 
             */

            uint nrDims = NumberOfDimensions;
            if (nrDims < 5) {
                if (d4 != 0 && d4 != -1) {
                    throw new IndexOutOfRangeException($"Index specifiers for (virtual) trailing dimensions must be 0 or -1. Check parameter: {nameof(d4)}!");
                }
                return GetSeqIndex(d0, d1, d2, d3);
            }
            // d0
            if (d0 < 0) {
                d0 += m_descriptor[3];
            }
            if ((ulong)d0 >= (ulong)m_descriptor[3]) {
                throw new IndexOutOfRangeException($"Index into dimension #0 {{value:{ d0 }}} is outside the range of this array. 0 <= d0 < { m_descriptor[3]}");
            }
            // d1
            if (d1 < 0) {
                d1 += m_descriptor[4];
            }
            if ((ulong)d1 >= (ulong)m_descriptor[4]) {
                throw new IndexOutOfRangeException($"Index into dimension #1 {{value:{ d1 }}} is outside the range of this array. 0 <= d1 < { m_descriptor[4] }.");
            }
            // d2
            if (d2 < 0) {
                d2 += m_descriptor[5];
            }
            if ((ulong)d2 >= (ulong)m_descriptor[5]) {
                throw new IndexOutOfRangeException($"Index into dimension #2 {{value:{ d2 }}} is outside the range of this array. 0 <= d2 < { m_descriptor[5] }.");
            }
            // d3
            if (d3 < 0) {
                d3 += m_descriptor[6];
            }
            if ((ulong)d3 >= (ulong)m_descriptor[6]) {
                throw new IndexOutOfRangeException($"Index into dimension #3 {{value:{ d3 }}} is outside the range of this array. 0 <= d3 < { m_descriptor[6] }.");
            }

            // d4
            if ((ulong)d4 >= (ulong)m_descriptor[7]) {
                if (m_descriptor[1] == 0) {
                    // this is an empty array. so indices provided are always out of range here!
                    throw new IndexOutOfRangeException($"Index into empty array is out of the existing range!");
                }
                // last parameter negativ or t.b. merged with trailing dimension(s)
                var lastDimLen = m_descriptor[1] / (m_descriptor[3] * m_descriptor[4] * m_descriptor[5] * m_descriptor[6]);
                if (d4 < 0) {
                    d4 += lastDimLen;
                }
                if ((ulong)d4 >= (ulong)lastDimLen) {
                    throw new IndexOutOfRangeException($"Index into dimension #4 {{value:{ d4 }}} is outside of the range of this array. 0 <= d4 < { lastDimLen }.");
                }
                if (d4 >= m_descriptor[7]) { // v5.4: was: '... && StorageOrder != StorageOrders.ColumnMajor) {' - assuming old-style striding. But know we store 0 strides for singleton dimensions. Hence, this does not work anymore.
                    // forward to more-parameter overload
                    return GetSeqIndex(d0,
                                       d1,
                                       d2,
                                       d3,
                                       d4 % m_descriptor[7],
                                       d4 / m_descriptor[7]);
                }
            }
            // now last index is either in dim range or ColumnMajor!
            return (m_descriptor[2]
                    + d0 * m_descriptor[3 + nrDims]
                    + d1 * m_descriptor[4 + nrDims]
                    + d2 * m_descriptor[5 + nrDims]
                    + d3 * m_descriptor[6 + nrDims]
                    + d4 * m_descriptor[7 + nrDims]);

        }

        /// <summary>
        /// Retrieves the sequential index into an array stored according to this size descriptor object 
        /// based on the provided indices <paramref name="d0"/> ... <paramref name="d5"/>. Allows negative indices.  
        /// </summary>
        /// <param name="d0">Index into the first dimension.</param>
        /// <param name="d1">Index into the second dimension.</param>
        /// <param name="d2">Index into the third dimension.</param>
        /// <param name="d3">Index into the fourth dimension.</param>
        /// <param name="d4">Index into the fives dimension.</param>
        /// <param name="d5">Index into the sixth dimension.</param>
        /// <returns>Sequential index into the array when stored as 1D array of arbitrary storage order.</returns>
        /// <remarks><para>For arrays of matrix shape <paramref name="d0"/> references the index of the row and 
        /// <paramref name="d1"/> references the index of the column of the sequential index to return.</para>
        /// <para>More general, for <see cref="NumberOfDimensions">n-dimensional</see> arrays the first index corresponds 
        /// to the position in the first (index: 0) dimension, the second index to the position of the element in the 
        /// second dimension (index: 1) and so forth ...</para>
        /// <para>If the array addressed by this size has less dimensions than addressed by this function, trailing indices 
        /// (i.e.: such indices which deal with a dimension whose index is higher than existing in the array)
        /// correspond to 'virtual' or singleton dimensions. Those indices must address the 0-th element of the 
        /// virtual dimension. Hence its value must be 0.</para>
        /// <para>Conversely, if the array addressed by this size stores more dimensions than addressed by this function
        /// the last index parameter <paramref name="d5"/> may exceeds the limit of its corresponding dimension. In this 
        /// case the sequential index returned is computed by subsequently merging trailing dimensions and folding 
        /// the superflous modulus of the value of <paramref name="d5"/> to the next dimension, correspondingly. This process 
        /// is repeated until either the number of dimensions is reached or the modified new value of <paramref name="d5"/> 
        /// reaches 0.</para>
        /// <para>Any index parameter may be negative, in which case the resulting index is computed by adding the 
        /// corresponding dimension length to the parameters value. Hence, a value of -1 addresses the last element in the dimension. 
        /// Note, that for the last specified dimension a value of -1 references the last element in the _virtual_ dimension,
        /// merged with any non specified trailing dimensions.</para>
        /// <para>This function recognizes arbitrarily strided size objects. It expects all sizes (dimension lengths, strides, the base offset 
        /// and the number of elements) to fit into <see cref="long.MaxValue"/>. Consider using one of the overloads with 
        /// <see cref="System.UInt32"/> parameters if you are not dealing with very big arrays and don't require negative indexing.
        /// This may bring better performance in tight loops.</para>
        /// <para>Performance hint: this function (and all corresponding overloads) are optimized for the case
        /// where the number of index parameters provided corresponds to the number of dimensions in the array and all 
        /// provided index parameters are within the range of their corresponding dimension. The functions, however, handle 
        /// arbitrary cases, including addressing, merging and ommitting trailing dimensions. Any of those extended features
        /// may introduce a performance penalty, though.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if any of the leading dimension specifiers <paramref name="d0"/> ... 
        /// <paramref name="d4"/> is equal or greater than the length of its corresponding dimension.</exception>
        /// <exception cref="IndexOutOfRangeException"> if <paramref name="d5"/> is equal or greater than the length 
        /// of the last dimension and the resulting index after merging trailing dimensions points to a non-existing element.</exception>
        public long GetSeqIndex(long d0, long d1, long d2, long d3, long d4, long d5) {
            /* 
             * * Must allow virtual trailing singleton dimensions. 
             * * Must recognize merged dimensions. 
             * * Accepts negative indices
             * * Works on arrays with fewer dimensions
             * * Must throw on IndexOutOfRange. 
             * * - all but last dim spec in range
             * * - ret: overall element count
             * * RESTORED: Must recognize arbitrary strides. 
             * * Must be efficient! 
             */

            uint nrDims = NumberOfDimensions;
            if (nrDims < 6) {
                if (d5 != 0 && d5 != -1) {
                    throw new IndexOutOfRangeException($"Index specifiers for (virtual) trailing dimensions must be 0 or -1. Check parameter: {nameof(d5)}!");
                }
                return GetSeqIndex(d0, d1, d2, d3, d4);
            }
            // d0
            if (d0 < 0) {
                d0 += m_descriptor[3];
            }
            if ((ulong)d0 >= (ulong)m_descriptor[3]) {
                throw new IndexOutOfRangeException($"Index into dimension #0 {{value:{ d0 }}} is outside the range of this array. 0 <= d0 < { m_descriptor[3]}");
            }
            // d1
            if (d1 < 0) {
                d1 += m_descriptor[4];
            }
            if ((ulong)d1 >= (ulong)m_descriptor[4]) {
                throw new IndexOutOfRangeException($"Index into dimension #1 {{value:{ d1 }}} is outside the range of this array. 0 <= d1 < { m_descriptor[4] }.");
            }
            // d2
            if (d2 < 0) {
                d2 += m_descriptor[5];
            }
            if ((ulong)d2 >= (ulong)m_descriptor[5]) {
                throw new IndexOutOfRangeException($"Index into dimension #2 {{value:{ d2 }}} is outside the range of this array. 0 <= d2 < { m_descriptor[5] }.");
            }
            // d3
            if (d3 < 0) {
                d3 += m_descriptor[6];
            }
            if ((ulong)d3 >= (ulong)m_descriptor[6]) {
                throw new IndexOutOfRangeException($"Index into dimension #3 {{value:{ d3 }}} is outside the range of this array. 0 <= d3 < { m_descriptor[6] }.");
            }
            // d4
            if (d4 < 0) {
                d4 += m_descriptor[7];
            }
            if ((ulong)d4 >= (ulong)m_descriptor[7]) {
                throw new IndexOutOfRangeException($"Index into dimension #4 {{value:{ d4 }}} is outside the range of this array. 0 <= d4 < { m_descriptor[7] }.");
            }

            // d5
            if ((ulong)d5 >= (ulong)m_descriptor[8]) {
                if (m_descriptor[1] == 0) {
                    // this is an empty array. so indices provided are always out of range here!
                    throw new IndexOutOfRangeException($"Index into empty array is out of the existing range!");
                }
                // last parameter negativ or t.b. merged with trailing dimension(s)
                var lastDimLen = m_descriptor[1] / (m_descriptor[3] * m_descriptor[4]
                                                        * m_descriptor[5] * m_descriptor[6]
                                                        * m_descriptor[7]);
                if (d5 < 0) {
                    d5 += lastDimLen;
                }
                if ((ulong)d5 >= (ulong)lastDimLen) {
                    throw new IndexOutOfRangeException($"Index into dimension #5 {{value:{ d5 }}} is outside of the range of this array. 0 <= d5 < { lastDimLen }.");
                }
                if (d5 >= m_descriptor[8]) { // v5.4: was: '... && StorageOrder != StorageOrders.ColumnMajor) {' - assuming old-style striding. But know we store 0 strides for singleton dimensions. Hence, this does not work anymore.
                    // forward to more-parameter overload
                    return GetSeqIndex(d0,
                                       d1,
                                       d2,
                                       d3,
                                       d4,
                                       d5 % m_descriptor[8],
                                       d5 / m_descriptor[8]);
                }
            }
            // now last index is either in dim range or ColumnMajor!
            return (m_descriptor[2]
                    + d0 * m_descriptor[3 + nrDims]
                    + d1 * m_descriptor[4 + nrDims]
                    + d2 * m_descriptor[5 + nrDims]
                    + d3 * m_descriptor[6 + nrDims]
                    + d4 * m_descriptor[7 + nrDims]
                    + d5 * m_descriptor[8 + nrDims]);
        }

        /// <summary>
        /// Retrieves the sequential index into an array stored according to this size descriptor object 
        /// based on the provided indices <paramref name="d0"/> ... <paramref name="d6"/>. Allows negative indices.  
        /// </summary>
        /// <param name="d0">Index into the first dimension.</param>
        /// <param name="d1">Index into the second dimension.</param>
        /// <param name="d2">Index into the third dimension.</param>
        /// <param name="d3">Index into the fourth dimension.</param>
        /// <param name="d4">Index into the fives dimension.</param>
        /// <param name="d5">Index into the sixth dimension.</param>
        /// <param name="d6">Index into the seventh dimension.</param>
        /// <returns>Sequential index into the array when stored as 1D array of arbitrary storage order.</returns>
        /// <remarks><para>For arrays of matrix shape <paramref name="d0"/> references the index of the row and 
        /// <paramref name="d1"/> references the index of the column of the sequential index to return.</para>
        /// <para>More general, for <see cref="NumberOfDimensions">n-dimensional</see> arrays the first index corresponds 
        /// to the position in the first (index: 0) dimension, the second index to the position of the element in the 
        /// second dimension (index: 1) and so forth ...</para>
        /// <para>If the array addressed by this size has less dimensions than addressed by this function, trailing indices 
        /// (i.e.: such indices which deal with a dimension whose index is higher than existing in the array)
        /// correspond to 'virtual' or singleton dimensions. Those indices must address the 0-th element of the 
        /// virtual dimension. Hence its value must be 0.</para>
        /// <para>Conversely, if the array addressed by this size stores more dimensions than addressed by this function
        /// the last index parameter may exceeds the limit of its corresponding dimension. In this 
        /// case the sequential index to be returned is computed by subsequently merging trailing dimensions and folding 
        /// the superflous modulus of the value of the last parameter provided into the next dimension, correspondingly. This process 
        /// is repeated until either the number of dimensions is reached or the remaining index value reaches 0.</para>
        /// <para>Any index parameter may be negative, in which case the resulting index is computed by adding the 
        /// corresponding dimension length to the parameters value. Hence, a value of -1 addresses the last element in the dimension. 
        /// Note, that for the last specified dimension a value of -1 references the last element in the _virtual_ dimension,
        /// merged with any non specified trailing dimensions.</para>
        /// <para>This function recognizes arbitrarily strided objects. It expects all sizes (dimension lengths, strides, the base offset 
        /// and the number of elements) to fit into <see cref="long.MaxValue"/>. Consider using one of the overloads with 
        /// <see cref="System.UInt32"/> parameters if you are not dealing with very big arrays and don't require negative indexing.
        /// This may bring better performance in tight loops.</para>
        /// <para>Performance hint: this function (and all corresponding overloads) are optimized for the case
        /// where the number of index parameters provided corresponds to the number of dimensions in the array and all 
        /// provided index parameters are within the range of their corresponding dimension. The functions, however, handle 
        /// arbitrary cases, including addressing, merging and ommitting trailing dimensions. Any of those extended features
        /// may introduce a performance penalty, though.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if any of the dimension specifiers <paramref name="d0"/> ... 
        /// <paramref name="d6"/> is equal or greater than the length of its corresponding dimension.</exception>
        public long GetSeqIndex(long d0, long d1, long d2, long d3, long d4, long d5, long d6) {

            /* 
             * * Must allow virtual trailing singleton dimensions. 
             * * Must recognize merged dimensions. 
             * * Accepts negative indices
             * * Works on arrays with fewer dimensions
             * * Must throw on IndexOutOfRange. 
             * * - all but last dim spec in range
             * * - ret: overall element count
             * * RESTORED: Must recognize arbitrary strides. 
             * * Must be efficient! 
             */

            uint nrDims = NumberOfDimensions;
            if (nrDims < 7) {
                if (d6 != 0 && d6 != -1) {
                    throw new IndexOutOfRangeException($"Index specifiers for (virtual) trailing dimensions must be 0 or -1. Check parameter: {nameof(d6)}!");
                }
                return GetSeqIndex(d0, d1, d2, d3, d4, d5);
            }
            // d0
            if (d0 < 0) {
                d0 += m_descriptor[3];
            }
            if ((ulong)d0 >= (ulong)m_descriptor[3]) {
                throw new IndexOutOfRangeException($"Index into dimension #0 {{value:{ d0 }}} is outside the range of this array. 0 <= d0 < { m_descriptor[3]}");
            }
            // d1
            if (d1 < 0) {
                d1 += m_descriptor[4];
            }
            if ((ulong)d1 >= (ulong)m_descriptor[4]) {
                throw new IndexOutOfRangeException($"Index into dimension #1 {{value:{ d1 }}} is outside the range of this array. 0 <= d1 < { m_descriptor[4] }.");
            }
            // d2
            if (d2 < 0) {
                d2 += m_descriptor[5];
            }
            if ((ulong)d2 >= (ulong)m_descriptor[5]) {
                throw new IndexOutOfRangeException($"Index into dimension #2 {{value:{ d2 }}} is outside the range of this array. 0 <= d2 < { m_descriptor[5] }.");
            }
            // d3
            if (d3 < 0) {
                d3 += m_descriptor[6];
            }
            if ((ulong)d3 >= (ulong)m_descriptor[6]) {
                throw new IndexOutOfRangeException($"Index into dimension #3 {{value:{ d3 }}} is outside the range of this array. 0 <= d3 < { m_descriptor[6] }.");
            }
            // d4
            if (d4 < 0) {
                d4 += m_descriptor[7];
            }
            if ((ulong)d4 >= (ulong)m_descriptor[7]) {
                throw new IndexOutOfRangeException($"Index into dimension #4 {{value:{ d4 }}} is outside the range of this array. 0 <= d4 < { m_descriptor[7] }.");
            }
            // d5
            if (d5 < 0) {
                d5 += m_descriptor[8];
            }
            if ((ulong)d5 >= (ulong)m_descriptor[8]) {
                throw new IndexOutOfRangeException($"Index into dimension #5 {{value:{ d5 }}} is outside the range of this array. 0 <= d5 < { m_descriptor[8] }.");
            }
            // d6
            if (d6 < 0) {
                d6 += m_descriptor[9];
            }
            if ((ulong)d6 >= (ulong)m_descriptor[9]) {
                if (m_descriptor[1] == 0) { // important to prevent from div by 0 below
                    throw new IndexOutOfRangeException($"Indexing into an empty array is not allowed!"); 
                }
                long ret = m_descriptor[2]
                    + d0 * m_descriptor[3 + nrDims]
                    + d1 * m_descriptor[4 + nrDims]
                    + d2 * m_descriptor[5 + nrDims]
                    + d3 * m_descriptor[6 + nrDims]
                    + d4 * m_descriptor[7 + nrDims]
                    + d5 * m_descriptor[8 + nrDims];
                for (int i = 6; i < nrDims && d6 > 0; i++) {
                    long c = d6 % m_descriptor[3 + i];
                    ret += (d6 % m_descriptor[3 + i]) * m_descriptor[3 + nrDims + i];
                    d6 /= m_descriptor[3 + i]; 
                }
                if (d6 > 0) {
                    throw new IndexOutOfRangeException($"Index into dimension #6 {{value:{ d6 }}} is outside the range of this array. 0 <= d6 < { m_descriptor[9] }.");
                }
                return ret; //
            }
            // no further checks due to Size.MaxNumberOfDimensions
            return (m_descriptor[2]
                    + d0 * m_descriptor[3 + nrDims]
                    + d1 * m_descriptor[4 + nrDims]
                    + d2 * m_descriptor[5 + nrDims]
                    + d3 * m_descriptor[6 + nrDims]
                    + d4 * m_descriptor[7 + nrDims]
                    + d5 * m_descriptor[8 + nrDims]
                    + d6 * m_descriptor[9 + nrDims]);
        }

        /// <summary>
        /// Retrieves the element index of an element located at <paramref name="indices"/> stored in an array according to this size descriptor. 
        /// </summary>
        /// <param name="indices">Indices into each dimension of the element to retrieve.</param>
        /// <returns>The element index of the addressed item.</returns>
        /// <remarks><para>The value returned corresponds to the number of elements to add to the beginning of the underlying storage array in order 
        /// to find the requested element. Note that this offset has a unit of 'element'. If the element type is a reference type the array stores 
        /// its element in 1 dimensional managed arrays and the value returned by <see cref="GetSeqIndex(InArray{long})"/> can directly be used for 
        /// indexing into this 1d array.</para> 
        /// <para>For value typed elements which are stored into unmanaged heaps and where access is done via pointers the address of the element 
        /// as addressed by <paramref name="indices"/> is computed by p = p0 + GetSeqIndex(indices) * sizeof(T). Here, p0 is the base address 
        /// (<see cref="IntPtr"/>, void* or byte*) of the arrays memory as acquired from <see cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</see>.</para>
        /// <para><paramref name="indices"/> may contain negative dimension indices in which case the location of the element is considered at the 
        /// absolute value </para></remarks>
        public long GetSeqIndex(InArray<long> indices) {
            using (Scope.Enter(indices)) {
                if (Equals(indices,null) || indices.S.NumberOfElements < 1) {
                    return BaseOffset; 
                } else {
                    switch (indices.S.NumberOfElements) {
                        case 1: return GetSeqIndex(indices.GetValue(0));
                        case 2: return GetSeqIndex(indices.GetValue(0), indices.GetValue(1));
                        case 3: return GetSeqIndex(indices.GetValue(0), indices.GetValue(1), indices.GetValue(2));
                        case 4: return GetSeqIndex(indices.GetValue(0), indices.GetValue(1), indices.GetValue(2), indices.GetValue(3));
                        case 5: return GetSeqIndex(indices.GetValue(0), indices.GetValue(1), indices.GetValue(2), indices.GetValue(3), indices.GetValue(4));
                        case 6: return GetSeqIndex(indices.GetValue(0), indices.GetValue(1), indices.GetValue(2), indices.GetValue(3), indices.GetValue(4), indices.GetValue(5));
                        case 7: return GetSeqIndex(indices.GetValue(0), indices.GetValue(1), indices.GetValue(2), indices.GetValue(3), indices.GetValue(4), indices.GetValue(5), indices.GetValue(6));
                        default:
                            long* ind = (long*)indices.GetHostPointerForRead(StorageOrders.ColumnMajor);
                            return GetSeqIndex(ind, indices.S.NumberOfElements); 
                    }
                }
            }
        }
        #endregion

        #region GetSeqIndex support functions, long
        /// <summary>
        /// Retrieves the sequential index into an array stored according to this size descriptor object 
        /// based on the provided index array <paramref name="d"/>. Allows negative indices.  
        /// </summary>
        /// <param name="d">System.Array with indices into the dimensions of this array.</param>
        /// <returns>Sequential index into the array when stored as 1D array of arbitrary storage order.</returns>
        /// <remarks>
        /// <para>If the array addressed by this size has less dimensions than addressed by <paramref name="d"/>, trailing 
        /// indices (i.e.: such indices dealing with a dimension whose index is higher than existing in the array)
        /// correspond to 'virtual' dimensions. Virtual dimensions are always singleton dimensions, hence indices must 
        /// address the 0-th element of the virtual dimension.</para>
        /// <para>Conversely, if the array addressed by this size stores more dimensions than addressed by <paramref name="d"/>
        /// the value of the last index from <paramref name="d"/> may exceed the length of the corresponding dimension. In this 
        /// case the sequential index to be returned is computed by subsequently merging trailing dimensions and folding 
        /// the superflous modulus of the value of 'd[{last}]' to the next dimension, correspondingly. This process 
        /// is repeated until either the number of dimensions is reached or the modified new value of 'd[{last}]' 
        /// reaches 0.</para>
        /// <para>Any index parameter may be negative, in which case the resulting index is computed by adding the 
        /// corresponding dimension length to the parameters value. Hence, a value of -1 addresses the last element in the dimension. 
        /// Note, that for the last specified dimension a value of -1 references the last element in the _last_ dimension, i.e.: 
        /// unspecified trailing dimensions are (virtually) merged into a single dimension, having a length of the product of the 
        /// lengths of the existing unspecified, trailing dimensions.</para>
        /// <para>This function recognizes arbitrarily strided size objects. It expects all sizes (dimension lengths, strides, the base offset 
        /// and the number of elements) to fit into <see cref="long.MaxValue"/>. Consider using one of the overloads with 
        /// <see cref="System.UInt32"/> parameters if you are not dealing with very big arrays and don't require negative indexing.
        /// This may bring better performance in tight loops.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if any of the indices in <paramref name="d"/> (except the 
        /// last index stored, see above) is equal or greater than the length of its corresponding dimension.</exception>
        /// <exception cref="IndexOutOfRangeException"> the resulting index points to a non-existing element.</exception>
        internal long GetSeqIndex(long[] d) {
            /* 
             * * Must allow virtual trailing singleton dimensions. 
             * * Must recognize merged dimensions. 
             * * Accepts negative indices
             * * Works on arrays with fewer dimensions
             * * Must throw on IndexOutOfRange. 
             * * - all but last dim spec in range
             * * - ret: overall element count
             * * RESTORED: Must recognize arbitrary strides. 
             * * Must be efficient! 
             */

            if (d == null || d.Length == 0) {
                throw new ArgumentException("Indices must be provided as a non-null, non-empty System.Array.");
            }
            if (m_descriptor[1] == 0) {
                // this is an empty array. so indices provided are always out of range here!
                throw new IndexOutOfRangeException($"Index into empty array is out of the existing range!");
            }
            int len = Math.Min(d.Length, (int)NumberOfDimensions) - 1, i = 0; // handle the last dimension separately (merging)
            long ret = m_descriptor[2];
            for (; i < len; i++) {
                if (d[i] < 0) {
                    d[i] = m_descriptor[3 + i] + d[i];
                }
                if (d[i] >= m_descriptor[3 + i] || d[i] < 0) {
                    throw new IndexOutOfRangeException($"The index value {d[i]} for dimension {i} is outside of the dimension length ({m_descriptor[i + 3]}).");
                }
                ret += (m_descriptor[3 + i + NumberOfDimensions] * d[i]);
            }
            var u = d[i];
            if (u < 0) {
                // last provided index counts from the end of the array
                long mergedDimLength = 1; 
                for (int k = i; k < NumberOfDimensions; k++) {
                    mergedDimLength *= m_descriptor[3 + k]; 
                }
                u += mergedDimLength;  // checks are done below on overall result.
            }
            for (; i < NumberOfDimensions && u > 0; i++) {
                var s = m_descriptor[3 + i];
                ret += (m_descriptor[3 + i + NumberOfDimensions] * (u % s));
                u /= s;
            }
            if (u != 0) {
                throw new IndexOutOfRangeException($"The index value {d[d.Length - 1]} for dimension #{d.Length - 1} is outside of the dimension length ({this[d.Length -1]}).");

            }
            for (; i < d.Length; i++) {
                if (d[i] != 0 && d[i] != -1) {
                    throw new IndexOutOfRangeException($"Indices addressing trailing virtual dimensions must be 0. Check index at position {i}!");
                }
            }

            if (ret > GetElementSpan() + m_descriptor[2]) {
                throw new IndexOutOfRangeException("The index of the element addressed is outside of the range of this array.");
            }
            return ret;
        }
        /// <summary>
        /// Computes the memory element offset into an array stored according to this BSD to retrieve the element identified by the _row major_ sequential element index <paramref name="d0"/>.
        /// </summary>
        /// <param name="d0">Sequential element index, in range [-NumberOfElements]...NumberOfElements-1.</param>
        /// <returns>Memory element index to retrieve the element from the underlying 1D storage array.</returns>
        /// <exception cref="IndexOutOfRangeException"> if <paramref name="d0"/> is outside the range of existing elements.</exception>
        public long GetSeqIndex_NP(long d0) {
            long ret = m_descriptor[2];
            if (d0 < 0) d0 += m_descriptor[1];
            if (d0 < 0 || d0 >= m_descriptor[1]) {
                if (m_descriptor[1] > 0) {
                    throw new IndexOutOfRangeException($"The sequential index d0 ({d0}) is out of the range of allowed indices for this array: [-{NumberOfElements}...{NumberOfElements - 1}].");
                } else {
                    throw new IndexOutOfRangeException($"Indexing into an empty array is not possible.");
                }
            }
            uint ndims = (uint)m_descriptor[0];
            long* dims = m_descriptor + 2 + ndims; // initialize with _last_ dim! 
            long* strides = dims + ndims; 
            for (int i = 0; i < ndims && d0 > 0; i++) {
                long c = dims[-i];
                ret += (d0 % c) * strides[-i];
                d0 /= c;
            }
            return ret; 
        }
        /// <summary>
        /// Retrieves the sequential index into an array stored according to this size descriptor object 
        /// based on the provided index array <paramref name="d"/>. Allows negative indices.  
        /// </summary>
        /// <param name="d">System.Array with indices into the dimensions of this array. Negative indices count from the end of a dimension.</param>
        /// <param name="len">Length of <paramref name="d"/>.</param>
        /// <returns>Sequential index into the array when stored as 1D array of arbitrary storage order.</returns>
        /// <remarks>
        /// <para>If the array addressed by this size has less dimensions than addressed by <paramref name="d"/>, trailing 
        /// indices (i.e.: such indices dealing with a dimension whose index is higher than existing in the array)
        /// correspond to 'virtual' or singleton dimensions. Those indices must address the 0-th element of the 
        /// virtual dimension. Hence its value must be 0.</para>
        /// <para>Conversely, if the array addressed by this size stores more dimensions than addressed by <paramref name="d"/>
        /// the value of the last index from <paramref name="d"/> may exceeds the length of the corresponding dimension. In this 
        /// case the sequential index to be returned is computed by subsequently merging trailing dimensions and folding 
        /// the superflous modulus of the value of 'd[{last}]' to the next dimension, correspondingly. This process 
        /// is repeated until either the number of dimensions is reached or the modified new value of 'd[{last}]' 
        /// reaches 0.</para>
        /// <para>This function recognizes arbitrarily strided size objects. It expects all sizes (dimension lengths, strides, the base offset 
        /// and the number of elements) to fit into <see cref="long.MaxValue"/>. Consider using one of the overloads with 
        /// <see cref="System.UInt32"/> parameters if you are not dealing with very big arrays.
        /// This may bring better performance in tight loops.</para>
        /// <para>Any index in <paramref name="d"/> which is negative is translated into the corresponding index found by 
        /// counting from the end (numpy negative index translation). Note that negative indices are allowed for any dimension 
        /// except the last dimension given by <paramref name="d"/> and when <paramref name="len"/> &lt; <see cref="NumberOfDimensions"/>.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if any of the indices in <paramref name="d"/> (except the 
        /// last index stored, see above) is equal or greater than the length of its corresponding dimension.</exception>
        /// <exception cref="IndexOutOfRangeException"> the resulting index points to a non-existing element.</exception>
        internal unsafe long GetSeqIndex(long* d, long len) {
            /* 
             * * Must allow virtual trailing singleton dimensions. 
             * * Must recognize merged dimensions. 
             * * Must throw on IndexOutOfRange. 
             * * - all but last dim spec in range
             * * - ret: overall element count
             * * RESTORED: Must recognize arbitrary strides. 
             * * Must be efficient! 
             */

            if (d == null || len < 0) {
                throw new ArgumentException("Indices pointer must not be NULL and point to a non empty array (len >= 0).");
            } else if (m_descriptor[0] == 0) {
                // numpy scalar
                bool all0(long* indices, long length) {
                    for (;length-->0 ;) {
                        var cur = indices[length]; 
                        if (cur > 0 || cur < -1) { return false; }
                    }
                    return true;
                }
                if (len == 0 || all0(d,len)) { // (len == 1 && (d[0] == 0 || d[0] == -1))) {
                    return m_descriptor[2]; 
                }  else {
                    throw new IndexOutOfRangeException($"Index into 0-dim (scalar) array is out of range! Allowed: set of indices having value 0 or -1.");
                }
            }
            if (m_descriptor[1] == 0) {
                // this is an empty array. so indices provided are always out of range here!
                throw new IndexOutOfRangeException($"Index into empty array is out of the existing range!");
            }
            UInt32 nrDims = NumberOfDimensions, mylen = Math.Min((UInt32)len, nrDims) - 1, i = 0; // handle the last dimension separately (merging)
            long ret = 0;
            for (; i < mylen; i++) {
                if (d[i] < 0) {
                    d[i] = m_descriptor[3 + i] + d[i]; 
                }
                if (d[i] >= m_descriptor[3 + i]) {
                    throw new IndexOutOfRangeException($"The index value {d[i]} for dimension #{i} is outside of the dimension length ({m_descriptor[i + 3]}).");
                }
                ret += m_descriptor[3 + i + nrDims] * d[i];
            }
            var u = d[i];
            if (u < 0) {
                // last provided index counts from the end of the array
                long mergedDimLength = 1;
                for (var k = i; k < nrDims; k++) {
                    mergedDimLength *= m_descriptor[3 + k];
                }
                u += mergedDimLength;  // checks are done below on overall result.
            }

            for (; u > 0 && i < nrDims; i++) {
                var s = m_descriptor[3 + i];
                ret += m_descriptor[3 + i + nrDims] * (u % s);
                u /= s;
            }
            if (u > 0) {
                throw new IndexOutOfRangeException($"Index '{d[mylen]}' at position #{mylen} is out of range for an array [{ILNumerics.Core.Global.Helper.dims2string(m_descriptor + 3, NumberOfDimensions)}]."); 
            }
            for (; i < len; i++) {
                if (d[i] != 0) {
                    throw new IndexOutOfRangeException($"Indices addressing trailing singleton dimensions must be 0. Check index at position {i}!");
                }
            }

            if (ret > GetElementSpan() + m_descriptor[2]) {
                throw new IndexOutOfRangeException("The index of the element addressed is outside of the range of this array.");
            }
            return ret + m_descriptor[2]; // add base offset
        }

        #endregion

    }
}
