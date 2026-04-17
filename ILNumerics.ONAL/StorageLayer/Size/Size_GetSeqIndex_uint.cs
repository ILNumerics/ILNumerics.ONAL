using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILNumerics {

    public unsafe partial class Size {

        #region GetSeqIndex via uint indices
        /// <summary>
        /// Retrieves the storage element index into an array stored according to this size descriptor object
        /// based on the provided sequential index <paramref name="d0"/>.  
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
        /// <para>This function recognizes arbitrary strides. It expects all sizes (dimension lengths, strides, the base offset 
        /// and the number of elements) to fit into <see cref="uint.MaxValue"/>. Use one of the overloads with 
        /// <see cref="System.Int64"/> parameters if you are dealing with very big arrays or negative indices.</para>
        /// <para>Performance hint: this function (and all corresponding overloads) are optimized for the case
        /// where the number of index parameters provided corresponds to the number of dimensions in the array and all 
        /// provided index parameters are within the range of their corresponding dimension. The functions, however, handle 
        /// arbitrary cases, including addressing, merging and ommitting trailing dimensions. Any of those extended features
        /// may introduce a performance penalty, though.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if the leading dimension specifier <paramref name="d0"/> is equal to
        /// or greater than the <see cref="NumberOfElements"/>.</exception>
        public uint GetSeqIndex(uint d0) {

            if (d0 >= (uint)m_descriptor[1]) {
                throw new IndexOutOfRangeException($"Index into dimension #0 {{value:{ d0 }}} is outside of the range of this array. 0 <= d0 < {(ulong)m_descriptor[1]}");
            }
            if (StorageOrder == StorageOrders.ColumnMajor || m_descriptor[0] == 0) { // <- check for 0Dim explicitly! numpy 0-dim scalars are sometimes not ColumnMajor! 
                // this includes numpy 0-dim scalars
                return (uint)m_descriptor[2] + d0;
            } else if (d0 >= (long)m_descriptor[3]) {
                // if we end up here, we have 
                // * at least one dimension (scalars are catched above as being col.maj.)
                // * at least two dimension since d0 is larger than dimlen(0) and numel > dimlen(0). 
                System.Diagnostics.Debug.Assert((uint)m_descriptor[0] > 1);
                if (m_descriptor[1] == 0) {
                    // this is an empty array. so indices provided are always out of range here!
                    throw new IndexOutOfRangeException($"Index into empty array is out of the existing range!");
                }

                // forward to more-parameter overload
                return GetSeqIndex(d0 % (uint)m_descriptor[3], d0 / (uint)m_descriptor[3]);
            }
            return (uint)m_descriptor[2] + d0 * (uint)m_descriptor[NumberOfDimensions + 3];
        }

        /// <summary>
        /// Retrieves the sequential index into an array stored according to this size descriptor object 
        /// based on the provided indices <paramref name="d0"/> and <paramref name="d1"/>.   
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
        /// <para>This function recognizes arbitrary strides. It expects all sizes (dimension lengths, strides, the base offset 
        /// and the number of elements) to fit into <see cref="uint.MaxValue"/>. Use one of the overloads with 
        /// <see cref="System.Int64"/> parameters if you are dealing with very big arrays or negative indices.</para>
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
        public uint GetSeqIndex(uint d0, uint d1) {

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
                if (d1 != 0) throw new IndexOutOfRangeException("Indices for (virtual) trailing dimensions must be 0. Check: d1");
                return GetSeqIndex(d0);
            }
            // d0
            if (d0 >= (uint)m_descriptor[3]) {
                throw new IndexOutOfRangeException($"Index into dimension #0 {{value:{ d0 }}} is outside the range of this array. 0 <= d0 < { (long)m_descriptor[3]}");
            }
            if (d1 >= (uint)m_descriptor[4]) {
                if (m_descriptor[1] == 0) {
                    // this is an empty array. so indices provided are always out of range here!
                    throw new IndexOutOfRangeException($"Index into empty array is out of the existing range!");
                }
                var lastDimLen = (uint)m_descriptor[1] / ((uint)m_descriptor[3]);
                if (d1 >= lastDimLen) {
                    throw new IndexOutOfRangeException($"Index into dimension #1 {{value:{ d1 }}} is outside of the range of this array. 0 <= d1 < { lastDimLen }.");
                }
                // last parameter negativ or t.b. merged with trailing dimension(s)
                // forward to more-parameter overload
                return GetSeqIndex(d0,
                                    d1 % (uint)m_descriptor[4],
                                    d1 / (uint)m_descriptor[4]);
            }
            // last index is in dim range 
            return ((uint)m_descriptor[2]
                    + d0 * (uint)m_descriptor[3 + nrDims]
                    + d1 * (uint)m_descriptor[4 + nrDims]);
        }

        /// <summary>
        /// Retrieves the sequential index into an array stored according to this size descriptor object based 
        /// on the provided indices <paramref name="d0"/> ... <paramref name="d2"/>.  
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
        /// <para>This function recognizes arbitrarily strided size objects. It expects all sizes (dimension lengths, strides, the base offset 
        /// and the number of elements) to fit into <see cref="uint.MaxValue"/>. Use one of the overloads with 
        /// <see cref="System.Int64"/> parameters if you are dealing with very big arrays or negative indices.</para>
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
        public uint GetSeqIndex(uint d0, uint d1, uint d2) {
            /* 
             * * Must allow virtual trailing singleton dimensions. 
             * * Must recognize merged dimensions. 
             * * Accepts neg. indices
             * * Works on arrays with fewer dimensions
             * * Must throw on IndexOutOfRange. 
             * * - all but last dim spec in range
             * * - ret: overall element count
             * * RESTORED: Must recognize arbitrary strides. 
             * * Must be efficient! 
             */
            uint nrDims = NumberOfDimensions;
            if (nrDims < 3) {
                if (d2 != 0) throw new IndexOutOfRangeException("Indices for (virtual) trailing dimensions must be 0. Check: d2");
                return GetSeqIndex(d0, d1);
            }
            // d0
            if (d0 >= (uint)m_descriptor[3]) {
                throw new IndexOutOfRangeException($"Index into dimension #0 {{value:{ d0 }}} is outside the range of this array. 0 <= d0 < { (long)m_descriptor[3]}");
            }
            // d1
            if (d1 >= (uint)m_descriptor[4]) {
                throw new IndexOutOfRangeException($"Index into dimension #1 {{value:{ d1 }}} is outside the range of this array. 0 <= d1 < { (long)m_descriptor[4] }.");
            }
            // d4
            if (d2 >= (uint)m_descriptor[5]) {
                if (m_descriptor[1] == 0) {
                    // this is an empty array. so indices provided are always out of range here!
                    throw new IndexOutOfRangeException($"Index into empty array is out of the existing range!");
                }
                var lastDimLen = (uint)m_descriptor[1] / ((uint)m_descriptor[3] * (uint)m_descriptor[4]);
                if (d2 >= lastDimLen) {
                    throw new IndexOutOfRangeException($"Index into dimension #2 {{value:{ d2 }}} is outside of the range of this array. 0 <= d2 < { lastDimLen }.");
                }
                // last parameter negativ or t.b. merged with trailing dimension(s)
                // forward to more-parameter overload
                return GetSeqIndex(d0,
                                    d1,
                                    d2 % (uint)m_descriptor[5],
                                    d2 / (uint)m_descriptor[5]);
            }
            // now last index is either in dim range or ColumnMajor!
            return (uint)m_descriptor[2]
                    + d0 * (uint)m_descriptor[3 + nrDims]
                    + d1 * (uint)m_descriptor[4 + nrDims]
                    + d2 * (uint)m_descriptor[5 + nrDims];
        }

        /// <summary>
        /// Retrieves the sequential index into an array stored according to this size descriptor object 
        /// based on the provided indices <paramref name="d0"/> ... <paramref name="d3"/>.   
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
        /// <para>This function recognizes arbitrarily strided size objects. It expects all sizes (dimension lengths, strides, the base offset 
        /// and the number of elements) to fit into <see cref="uint.MaxValue"/>. Use one of the overloads with 
        /// <see cref="System.Int64"/> parameters if you are dealing with very big arrays or negative indices.</para>
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
        public uint GetSeqIndex(uint d0, uint d1, uint d2, uint d3) {
            /* 
             * * Must allow virtual trailing singleton dimensions. 
             * * Must recognize merged dimensions. 
             * * Accepts neg. indices
             * * Works on arrays with fewer dimensions
             * * Must throw on IndexOutOfRange. 
             * * - all but last dim spec in range
             * * - ret: overall element count
             * * RESTORED: Must recognize arbitrary strides. 
             * * Must be efficient! 
             */

            uint nrDims = NumberOfDimensions;
            if (nrDims < 4) {
                if (d3 != 0) {
                    throw new IndexOutOfRangeException($"Indices addressing trailing singleton dimensions must be 0. Check parameter: {nameof(d3)}!");
                }
                return GetSeqIndex(d0, d1, d2);
            }
            // d0
            if (d0 >= (uint)m_descriptor[3]) {
                throw new IndexOutOfRangeException($"Index into dimension #0 {{value:{ d0 }}} is outside the range of this array. 0 <= d0 < { (long)m_descriptor[3]}");
            }
            // d1
            if (d1 >= (uint)m_descriptor[4]) {
                throw new IndexOutOfRangeException($"Index into dimension #1 {{value:{ d1 }}} is outside the range of this array. 0 <= d1 < { (long)m_descriptor[4] }.");
            }
            // d2
            if (d2 >= (uint)m_descriptor[5]) {
                throw new IndexOutOfRangeException($"Index into dimension #2 {{value:{ d2 }}} is outside the range of this array. 0 <= d2 < { (long)m_descriptor[5] }.");
            }
            // d3
            if (d3 >= (ulong)m_descriptor[6]) {
                if (m_descriptor[1] == 0) {
                    // this is an empty array. so indices provided are always out of range here!
                    throw new IndexOutOfRangeException($"Index into empty array is out of the existing range!");
                }
                var lastDimLen = (uint)m_descriptor[1] / ((uint)m_descriptor[3] * (uint)m_descriptor[4] * (uint)m_descriptor[5]);
                if (d3 >= lastDimLen) {
                    throw new IndexOutOfRangeException($"Index into dimension #3 {{value:{ d3 }}} is outside of the range of this array. 0 <= d3 < { lastDimLen }.");
                }
                // last parameter negativ or t.b. merged with trailing dimension(s)
                // forward to more-parameter overload
                return GetSeqIndex(d0,
                                d1,
                                d2,
                                d3 % (uint)m_descriptor[6],
                                d3 / (uint)m_descriptor[6]);
            }
            // now last index is either in dim range or ColumnMajor!
            return (uint)m_descriptor[2]
                    + d0 * (uint)m_descriptor[3 + nrDims]
                    + d1 * (uint)m_descriptor[4 + nrDims]
                    + d2 * (uint)m_descriptor[5 + nrDims]
                    + d3 * (uint)m_descriptor[6 + nrDims];
        }

        /// <summary>
        /// Retrieves the sequential index into an array stored according to this size descriptor object 
        /// based on the provided indices <paramref name="d0"/> ... <paramref name="d4"/>.   
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
        /// <para>This function recognizes arbitrarily strided size objects. It expects all sizes (dimension lengths, strides, the base offset 
        /// and the number of elements) to fit into <see cref="uint.MaxValue"/>. Use one of the overloads with 
        /// <see cref="System.Int64"/> parameters if you are dealing with very big arrays or negative indices.</para>
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
        public uint GetSeqIndex(uint d0, uint d1, uint d2, uint d3, uint d4) {
            /* 
             * * Must allow virtual trailing singleton dimensions. 
             * * Must recognize merged dimensions. 
             * * Accepts neg. indices
             * * Works on arrays with fewer dimensions
             * * Must throw on IndexOutOfRange. 
             * * - all but last dim spec in range
             * * - ret: overall element count
             * * RESTORED: Must recognize arbitrary strides. 
             * * Must be efficient! 
             */

            uint nrDims = NumberOfDimensions;
            if (nrDims < 5) {
                if (d4 != 0) {
                    throw new IndexOutOfRangeException($"Indices addressing trailing singleton dimensions must be 0. Check parameter: {nameof(d4)}!");
                }
                return GetSeqIndex(d0, d1, d2, d3);
            }
            // d0
            if (d0 >= (uint)m_descriptor[3]) {
                throw new IndexOutOfRangeException($"Index into dimension #0 {{value:{ d0 }}} is outside the range of this array. 0 <= d0 < { (long)m_descriptor[3]}");
            }
            // d1
            if (d1 >= (uint)m_descriptor[4]) {
                throw new IndexOutOfRangeException($"Index into dimension #1 {{value:{ d1 }}} is outside the range of this array. 0 <= d1 < { (long)m_descriptor[4] }.");
            }
            // d2
            if (d2 >= (uint)m_descriptor[5]) {
                throw new IndexOutOfRangeException($"Index into dimension #2 {{value:{ d2 }}} is outside the range of this array. 0 <= d2 < { (long)m_descriptor[5] }.");
            }
            // d3
            if (d3 >= (uint)m_descriptor[6]) {
                throw new IndexOutOfRangeException($"Index into dimension #3 {{value:{ d3 }}} is outside the range of this array. 0 <= d3 < { (long)m_descriptor[6] }.");
            }

            // d4
            if (d4 >= (uint)m_descriptor[7]) {
                if (m_descriptor[1] == 0) {
                    // this is an empty array. so indices provided are always out of range here!
                    throw new IndexOutOfRangeException($"Index into empty array is out of the existing range!");
                }
                var lastDimLen = (uint)m_descriptor[1] / ((uint)m_descriptor[3] * (uint)m_descriptor[4] * (uint)m_descriptor[5] * (uint)m_descriptor[6]);
                if (d4 >= lastDimLen) {
                    throw new IndexOutOfRangeException($"Index into dimension #4 {{value:{ d4 }}} is outside of the range of this array. 0 <= d4 < { lastDimLen }.");
                }
                // last parameter negativ or t.b. merged with trailing dimension(s)
                // forward to more-parameter overload
                return GetSeqIndex(d0,
                                    d1,
                                    d2,
                                    d3,
                                    d4 % (uint)m_descriptor[7],
                                    d4 / (uint)m_descriptor[7]);
            }
            // now last index is either in dim range or ColumnMajor!
            return (uint)m_descriptor[2]
                    + d0 * (uint)m_descriptor[3 + nrDims]
                    + d1 * (uint)m_descriptor[4 + nrDims]
                    + d2 * (uint)m_descriptor[5 + nrDims]
                    + d3 * (uint)m_descriptor[6 + nrDims]
                    + d4 * (uint)m_descriptor[7 + nrDims];

        }

        /// <summary>
        /// Retrieves the sequential index into an array stored according to this size descriptor object 
        /// based on the provided indices <paramref name="d0"/> ... <paramref name="d5"/>.   
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
        /// <para>This function recognizes arbitrarily strided size objects. It expects all sizes (dimension lengths, strides, the base offset 
        /// and the number of elements) to fit into <see cref="uint.MaxValue"/>. Use one of the overloads with 
        /// <see cref="System.Int64"/> parameters if you are dealing with very big arrays or negative indices.</para>
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
        public uint GetSeqIndex(uint d0, uint d1, uint d2, uint d3, uint d4, uint d5) {
            /* 
             * * Must allow virtual trailing singleton dimensions. 
             * * Must recognize merged dimensions. 
             * * Accepts neg. indices
             * * Works on arrays with fewer dimensions
             * * Must throw on IndexOutOfRange. 
             * * - all but last dim spec in range
             * * - ret: overall element count
             * * RESTORED: Must recognize arbitrary strides. 
             * * Must be efficient! 
             */

            uint nrDims = NumberOfDimensions;
            if (nrDims < 6) {
                if (d5 != 0) {
                    throw new IndexOutOfRangeException($"Indices addressing trailing singleton dimensions must be 0. Check parameter: {nameof(d5)}!");
                }
                return GetSeqIndex(d0, d1, d2, d3, d4);
            }
            // d0
            if (d0 >= (uint)m_descriptor[3]) {
                throw new IndexOutOfRangeException($"Index into dimension #0 {{value:{ d0 }}} is outside the range of this array. 0 <= d0 < { (long)m_descriptor[3]}");
            }
            // d1
            if (d1 >= (uint)m_descriptor[4]) {
                throw new IndexOutOfRangeException($"Index into dimension #1 {{value:{ d1 }}} is outside the range of this array. 0 <= d1 < { (long)m_descriptor[4] }.");
            }
            // d2
            if (d2 >= (uint)m_descriptor[5]) {
                throw new IndexOutOfRangeException($"Index into dimension #2 {{value:{ d2 }}} is outside the range of this array. 0 <= d2 < { (long)m_descriptor[5] }.");
            }
            // d3
            if (d3 >= (uint)m_descriptor[6]) {
                throw new IndexOutOfRangeException($"Index into dimension #3 {{value:{ d3 }}} is outside the range of this array. 0 <= d3 < { (long)m_descriptor[6] }.");
            }
            // d4
            if (d4 >= (uint)m_descriptor[7]) {
                throw new IndexOutOfRangeException($"Index into dimension #4 {{value:{ d4 }}} is outside the range of this array. 0 <= d4 < { (long)m_descriptor[7] }.");
            }

            // d5
            if (d5 >= (uint)m_descriptor[8]) {
                if (m_descriptor[1] == 0) {
                    // this is an empty array. so indices provided are always out of range here!
                    throw new IndexOutOfRangeException($"Index into empty array is out of the existing range!");
                }
                var lastDimLen = (uint)m_descriptor[1] / ((uint)m_descriptor[3] * (uint)m_descriptor[4]
                                                        * (uint)m_descriptor[5] * (uint)m_descriptor[6]
                                                        * (uint)m_descriptor[7]);
                if (d5 >= lastDimLen) {
                    throw new IndexOutOfRangeException($"Index into dimension #5 {{value:{ d5 }}} is outside of the range of this array. 0 <= d5 < { lastDimLen }.");
                }
                // last parameter negativ or t.b. merged with trailing dimension(s)
                // forward to more-parameter overload
                return GetSeqIndex(d0,
                                    d1,
                                    d2,
                                    d3,
                                    d4,
                                    d5 % (uint)m_descriptor[8],
                                    d5 / (uint)m_descriptor[8]);
            }
            // now last index is either in dim range or ColumnMajor!
            return (uint)m_descriptor[2]
                    + d0 * (uint)m_descriptor[3 + nrDims]
                    + d1 * (uint)m_descriptor[4 + nrDims]
                    + d2 * (uint)m_descriptor[5 + nrDims]
                    + d3 * (uint)m_descriptor[6 + nrDims]
                    + d4 * (uint)m_descriptor[7 + nrDims]
                    + d5 * (uint)m_descriptor[8 + nrDims];
        }

        /// <summary>
        /// Retrieves the sequential index into an array stored according to this size descriptor object 
        /// based on the provided indices <paramref name="d0"/> ... <paramref name="d6"/>.   
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
        /// <para>This function recognizes arbitrarily strided objects. It expects all sizes (dimension lengths, strides, the base offset 
        /// and the number of elements) to fit into <see cref="uint.MaxValue"/>. Use one of the overloads with 
        /// <see cref="System.Int64"/> parameters if you are dealing with very big arrays or negative indices.</para>
        /// <para>Performance hint: this function (and all corresponding overloads) are optimized for the case
        /// where the number of index parameters provided corresponds to the number of dimensions in the array and all 
        /// provided index parameters are within the range of their corresponding dimension. The functions, however, handle 
        /// arbitrary cases, including addressing, merging and ommitting trailing dimensions. Any of those extended features
        /// may introduce a performance penalty, though.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if any of the dimension specifiers <paramref name="d0"/> ... 
        /// <paramref name="d6"/> is equal or greater than the length of its corresponding dimension.</exception>
        public uint GetSeqIndex(uint d0, uint d1, uint d2, uint d3, uint d4, uint d5, uint d6) {

            /* 
             * * Must allow virtual trailing singleton dimensions. 
             * * Must recognize merged dimensions. 
             * * Accepts neg. indices
             * * Works on arrays with fewer dimensions
             * * Must throw on IndexOutOfRange. 
             * * - all but last dim spec in range
             * * - ret: overall element count
             * * RESTORED: Must recognize arbitrary strides. 
             * * Must be efficient! 
             */

            uint nrDims = NumberOfDimensions;
            if (nrDims < 7) {
                if (d6 != 0) {
                    throw new IndexOutOfRangeException($"Indices addressing trailing singleton dimensions must be 0. Check parameter: {nameof(d6)}!");
                }
                return GetSeqIndex(d0, d1, d2, d3, d4, d5);
            }
            // d0
            if (d0 >= (uint)m_descriptor[3]) {
                throw new IndexOutOfRangeException($"Index into dimension #0 {{value:{ d0 }}} is outside the range of this array. 0 <= d0 < { (long)m_descriptor[3]}");
            }
            // d1
            if (d1 >= (uint)m_descriptor[4]) {
                throw new IndexOutOfRangeException($"Index into dimension #1 {{value:{ d1 }}} is outside the range of this array. 0 <= d1 < { (long)m_descriptor[4] }.");
            }
            // d2
            if (d2 >= (uint)m_descriptor[5]) {
                throw new IndexOutOfRangeException($"Index into dimension #2 {{value:{ d2 }}} is outside the range of this array. 0 <= d2 < { (long)m_descriptor[5] }.");
            }
            // d3
            if (d3 >= (uint)m_descriptor[6]) {
                throw new IndexOutOfRangeException($"Index into dimension #3 {{value:{ d3 }}} is outside the range of this array. 0 <= d3 < { (long)m_descriptor[6] }.");
            }
            // d4
            if (d4 >= (uint)m_descriptor[7]) {
                throw new IndexOutOfRangeException($"Index into dimension #4 {{value:{ d4 }}} is outside the range of this array. 0 <= d4 < { (long)m_descriptor[7] }.");
            }
            // d5
            if (d5 >= (uint)m_descriptor[8]) {
                throw new IndexOutOfRangeException($"Index into dimension #5 {{value:{ d5 }}} is outside the range of this array. 0 <= d5 < { (long)m_descriptor[8] }.");
            }
            // d6
            if (d6 >= (uint)m_descriptor[9]) {

                if (m_descriptor[1] == 0) {
                    // this is an empty array. so indices provided are always out of range here!
                    throw new IndexOutOfRangeException($"Index into empty array is out of the existing range!");
                }
                var lastDimLen = (uint)m_descriptor[1] / ((uint)m_descriptor[3] * (uint)m_descriptor[4]
                                                        * (uint)m_descriptor[5] * (uint)m_descriptor[6]
                                                        * (uint)m_descriptor[7] * (uint)m_descriptor[8]);
                if (d6 >= lastDimLen) {
                    throw new IndexOutOfRangeException($"Index into dimension #6 {{value:{ d6 }}} is outside of the range of this array. 0 <= d6 < { lastDimLen }.");
                }
                // last parameter negativ or t.b. merged with trailing dimension(s)
                // forward to more-parameter overload
                uint* buf = stackalloc uint[8];
                buf[0] = d0; buf[1] = d1; buf[2] = d2; 
                buf[3] = d3; buf[4] = d4; buf[5] = d5;
                buf[6] = d6 % (uint)m_descriptor[9]; 
                buf[7] = d6 / (uint)m_descriptor[9];
                return GetSeqIndex(buf, 8u);

            }
            // no further checks due to Size.MaxNumberOfDimensions
            return (uint)m_descriptor[2]
                    + d0 * (uint)m_descriptor[3 + nrDims]
                    + d1 * (uint)m_descriptor[4 + nrDims]
                    + d2 * (uint)m_descriptor[5 + nrDims]
                    + d3 * (uint)m_descriptor[6 + nrDims]
                    + d4 * (uint)m_descriptor[7 + nrDims]
                    + d5 * (uint)m_descriptor[8 + nrDims]
                    + d6 * (uint)m_descriptor[9 + nrDims];
        }
        #endregion

        #region GetSeqIndex support functions, uint
        /// <summary>
        /// Retrieves the sequential index into an array stored according to this size descriptor object 
        /// based on the provided index array <paramref name="d"/>. 
        /// </summary>
        /// <param name="d">System.Array with indices into the dimensions of this array.</param>
        /// <returns>Sequential index into the array when stored as 1D array of arbitrary storage order.</returns>
        /// <remarks>
        /// <para>If the array addressed by this size has less dimensions than addressed by <paramref name="d"/>, trailing 
        /// indices (i.e.: such indices dealing with a dimension whose index is higher than existing in the array)
        /// correspond to 'virtual' or singleton dimensions. Those indices must address the 0-th element of the 
        /// virtual dimension. Hence its value must be 0.</para>
        /// <para>Conversely, if the array addressed by this size stores more dimensions than addressed by <paramref name="d"/>
        /// the value of the last index from <paramref name="d"/> may exceed the length of the corresponding dimension. In this 
        /// case the sequential index to be returned is computed by subsequently merging trailing dimensions and folding 
        /// the superflous modulus of the value of 'd[{last}]' to the next dimension, correspondingly. This process 
        /// is repeated until either the number of dimensions is reached or the modified new value of 'd[{last}]' 
        /// reaches 0.</para>
        /// <para>This function recognizes arbitrarily strided size objects. It expects all sizes (dimension lengths, strides, the base offset 
        /// and the number of elements) to fit into <see cref="uint.MaxValue"/>. Use one of the overloads with 
        /// <see cref="System.Int64"/> parameters if you are dealing with very big arrays or negative indices.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if any of the indices in <paramref name="d"/> (except the 
        /// last index stored, see above) is equal or greater than the length of its corresponding dimension.</exception>
        /// <exception cref="IndexOutOfRangeException"> the resulting index points to a non-existing element.</exception>
        internal uint GetSeqIndex(uint[] d) {
            /* 
             * * Must allow virtual trailing singleton dimensions. 
             * * Must recognize merged dimensions. 
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
            if (m_descriptor[0] == 0 && d.Length == 1 && d[0] == 0) {
                return (uint)m_descriptor[2]; // special case: numpy scalar addressed by [0]
            }
            UInt32 nrDims = NumberOfDimensions, len = Math.Min((UInt32)d.Length, nrDims) - 1, i = 0; // handle the last dimension separately (merging)
            uint ret = 0;
            for (; i < len; i++) {
                if (d[i] >= (uint)m_descriptor[3 + i]) {
                    throw new IndexOutOfRangeException($"The index value {d[i]} for dimension #{i} is outside of the dimension length ({(uint)m_descriptor[i + 3]}).");
                }
                ret += (uint)m_descriptor[3 + i + nrDims] * d[i];
            }
            var u = d[i];
            for (; i < nrDims; i++) {
                var s = (uint)m_descriptor[3 + i];
                ret += (uint)m_descriptor[3 + i + nrDims] * (u % s);
                u /= s;
            }
            for (; i < d.Length; i++) {
                if (d[i] != 0) {
                    throw new IndexOutOfRangeException($"Indices addressing trailing singleton dimensions must be 0. Check index at position {i}!");
                }
            }
            if (ret > GetElementSpan() || u > 0) {
                throw new IndexOutOfRangeException("The index of the element addressed is outside of the range of this array.");
            }
            return ret + (uint)m_descriptor[2];
        }
        /// <summary>
        /// Retrieves the sequential index into an array stored according to this size descriptor object 
        /// based on the provided index array <paramref name="d"/>. 
        /// </summary>
        /// <param name="d">System.Array with indices into the dimensions of this array.</param>
        /// <param name="len">Length of <paramref name="d"/>.</param>
        /// <returns>Sequential index into the array when stored as 1D array of arbitrary storage order.</returns>
        /// <remarks>
        /// <para>If the array addressed by this size has less dimensions than addressed by <paramref name="d"/>, trailing 
        /// indices (i.e.: such indices dealing with a dimension whose index is higher than existing in the array)
        /// correspond to 'virtual' or singleton dimensions. Those indices must address the 0-th element of the 
        /// virtual dimension. Hence its value must be 0.</para>
        /// <para>Conversely, if the array addressed by this size stores more dimensions than addressed by <paramref name="d"/>
        /// the value of the last index from <paramref name="d"/> may exceed the length of the corresponding dimension. In this 
        /// case the sequential index to be returned is computed by subsequently merging trailing dimensions and folding 
        /// the superflous modulus of the value of 'd[{last}]' to the next dimension, correspondingly. This process 
        /// is repeated until either the number of dimensions is reached or the modified new value of 'd[{last}]' 
        /// reaches 0.</para>
        /// <para>This function recognizes arbitrarily strided size objects. It expects all sizes (dimension lengths, strides, the base offset 
        /// and the number of elements) to fit into <see cref="uint.MaxValue"/>. Use one of the overloads with 
        /// <see cref="System.Int64"/> parameters if you are dealing with very big arrays or negative indices.</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if any of the indices in <paramref name="d"/> (except the 
        /// last index stored, see above) is equal or greater than the length of its corresponding dimension.</exception>
        /// <exception cref="IndexOutOfRangeException"> the resulting index points to a non-existing element.</exception>
        public unsafe uint GetSeqIndex(uint* d, uint len) {
            /* 
             * * Must allow virtual trailing singleton dimensions. 
             * * Must recognize merged dimensions. 
             * * Must throw on IndexOutOfRange. 
             * * Must allow to index into numpy scalar with: [0]
             * * - all but last dim spec in range
             * * - ret: overall element count
             * * RESTORED: Must recognize arbitrary strides. 
             * * Must be efficient! 
             */

            if (d == null || len == 0) {
                throw new ArgumentException("Indices pointer must not be NULL and point to a non empty array (len > 0).");
            }
            if (m_descriptor[1] == 0) {
                // this is an empty array. so indices provided are always out of range here!
                throw new IndexOutOfRangeException($"Index into empty array is out of the existing range!");
            }
            if (m_descriptor[0] == 0 && len == 1 && d[0] == 0) {
                return (uint)m_descriptor[2]; // special case: numpy scalar addressed by [0]
            }
            UInt32 nrDims = NumberOfDimensions, mylen = Math.Min((UInt32)len, nrDims) - 1, i = 0; // handle the last dimension separately (merging)
            uint ret = 0;
            for (; i < mylen; i++) {
                if (d[i] >= (uint)m_descriptor[3 + i]) {
                    throw new IndexOutOfRangeException($"The index value {d[i]} for dimension #{i} is outside of the dimension length ({(uint)m_descriptor[i + 3]}).");
                }
                ret += (uint)m_descriptor[3 + i + nrDims] * d[i];
            }
            var u = d[i];
            for (; i < nrDims; i++) {
                var s = (uint)m_descriptor[3 + i];
                ret += (uint)m_descriptor[3 + i + nrDims] * (u % s);
                u /= s;
            }
            for (; i < len; i++) {
                if (d[i] != 0) {
                    throw new IndexOutOfRangeException($"Indices addressing trailing singleton dimensions must be 0. Check index at position {i}!");
                }
            }

            if (ret > GetElementSpan() || u > 0) {
                throw new IndexOutOfRangeException("The index of the element addressed is outside of the range of this array.");
            }
            return ret + (uint)m_descriptor[2]; // add base offset
        }
        #endregion

    }
}
