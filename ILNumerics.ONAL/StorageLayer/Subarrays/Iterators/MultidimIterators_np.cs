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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.StorageLayer {
    internal static partial class Iterators {

        #region Multidim Iterator for numpy advanced indexing 

        /// <summary>
        /// Specialized, unified iterator struct for numpy advanced indexing.
        /// </summary>
        /// <remarks><para>This struct is stored as item in an unmanaged array. The iterator is capable of iterating all supported index types without the need for virtual calls.</para></remarks>
        internal unsafe struct MultidimIterator {

            /// <summary>
            /// Index of the original(!) dimension this iterator is addressing. (may be reordered)
            /// </summary>
            internal uint m_index;
            /// <summary>
            /// Gives the number of index arrays forming the set of advanced indices.  
            /// </summary>
            /// <remarks>Advanced indices are grouped and handled synchronously in MoveNext / Reset(). This 
            /// value determines the number of grouped members / adjacent iterators to be incremented at the same time.
            /// </remarks>
            internal uint* m_setCount;
            /// <summary>
            /// byte pointer to index array storage, if any
            /// </summary>
            internal byte* m_indexArrayBase;
            /// <summary>
            /// element type of indices. None for slices. 
            /// </summary>
            internal NumericType m_elementType;
            /// <summary>
            /// number of dimensions of this indexing array and the broadcasted result _for this input dimension_, number of entries in m_curPos and m_outDims.
            /// </summary>
            internal uint* m_nrDims;
            /// <summary>
            /// Offset in elements unit for the initial first element regarding the base offset (if exists, otherwise - for slices and scalars - regarding 0).
            /// </summary>
            internal long m_offset;
            /// <summary>
            /// Strides of all m_nrDims index array dimensions, in bytes(!). This is individual for each index array in a set. (while curPos &amp; outDims are shared.) For slices this holds the bsd and other supporting data for iterating the actual bool array (instead of the 'virtual' index arrays derived from it). Element size unit.
            /// </summary>
            internal long* m_strides;
            /// <summary>
            /// Result shape this index array must be broadcasted to. These values may change after initialization - but before iteration starts. 
            /// </summary>
            internal long* m_outDims;
            /// <summary>
            /// Current position in the iteration. Shared between all index arrays in a set. 
            /// </summary>
            internal long* m_curPos;
            /// <summary>
            /// The stride between elements of this dimension in the input array being indexed, in elements unit(!).
            /// </summary>
            /// <remarks>This value is cached here for convenient access during iteration and so that it is re-sorted with the advanced indices if required.</remarks>
            internal long m_inStride;

            /// <summary>
            /// the last index in the source dimension. Used for translating neg. index values during iteration. 
            /// </summary>
            internal long m_lastDimIdx;

            /// <summary>
            /// Initialize existing multi dim iterator struct for another array
            /// </summary>
            /// <param name="it">target iterator struct</param>
            /// <param name="baseP">base pointer address as byte* pointer, including any base offset.</param>
            /// <param name="elementType">numeric element type code</param>
            /// <param name="nrDims">Lengths of strides, outDims and curPos. Greater or equal to 1. Subject to change before iteration starts!</param>
            /// <param name="outDims">broadcasted size of target iteration shape, in bytes(!). This may change after initialization but before the iteration starts.</param>
            /// <param name="strides">Strides of the arrays dimensions.</param>
            /// <param name="curPos">temp position buffer of length according to <paramref name="nrDims"/>.Subject to change before iteration starts.</param>
            /// <param name="lastDimIdx">Index of the last element in the dimension this iterator will be applied to. </param>
            /// <param name="setCount">Pointer to the variable counting the number of advanced indices. Subject to change before iteration starts. </param>
            /// <param name="index">Index of (input) dimension. Used for error reporting only. Bool array: index of the subspace dimension spaned by the index bool array.</param>
            /// <param name="outStride">Spacing between elements of the output array in the dimension addressed by <paramref name="index"/>, in elements(!).</param>
            /// <param name="offset">Offset of the first element relative to the baseP (if exists) or to 0 (slice, scalar), in bytes.</param>
            static internal void Set(
                ref MultidimIterator it,
                uint index,
                byte* baseP,
                NumericType elementType,
                uint* nrDims,
                long offset,
                long* strides,
                long* outDims,
                long* curPos,
                long lastDimIdx, // for neg. index values during iteration
                uint* setCount,
                long outStride
                ) { // long step = 1) {

                it.m_index = index;
                it.m_setCount = setCount;
                it.m_indexArrayBase = baseP;
                it.m_elementType = elementType;

                System.Diagnostics.Debug.Assert(nrDims == null || nrDims[0] >= 0); 
                it.m_nrDims = nrDims;
                it.m_offset = offset;
                it.m_strides = strides;
                it.m_outDims = outDims;
                it.m_curPos = curPos;
                if (nrDims != null) {
                    it.m_curPos[0] = -1; // Reset(), before first MoveNext() was called
                    for (int i = 1; i < nrDims[0]; i++) {
                        curPos[i] = 0;
                    }
                }

                it.m_inStride = outStride;
                it.m_lastDimIdx = lastDimIdx;
                //it.m_step = 1;
            }
            /// <summary>
            /// Gets the current value this iterator points to. This must be called only, when MoveNext() had been succeeded.
            /// </summary>
            public long Current {
                get {

                    #region Boolean iterator - finds the next True element first

                    if (m_elementType == NumericType.Boolean) {
                        if (m_curPos[0] != m_strides[0]) {
                            // MoveNext has been _successfully_ called. There must be another True element! 
                            // layout of m_strides*: [version][nrDims][highdims][curpos][dims - 1][strides]
                            // highdims are * strides + offset; 
                            // dims & strides are reversed.
                            // dims is minus 1! 
                            // curPos is elements (here: == bytes)! 
                            m_strides[0] = m_curPos[0]; // updates the version cache. 
                            var ndims = m_strides[1]; // this bool dimensions only. Additional higher dims of the output index space is handled in MoveNext() 
                            var dims = m_strides + 3 + Size.MaxNumberOfDimensions;
                            do {
                                while (m_strides[3] < dims[0]) {
                                    m_strides[3]++;
                                    if (m_indexArrayBase[m_strides[2] + m_strides[3] * dims[Size.MaxNumberOfDimensions]] != 0) {
                                        if (m_strides[3 + m_index] > m_lastDimIdx) {
                                            throw new IndexOutOfRangeException($"Boolean index array contains True values at #({Helper.dims2string(m_strides + 3,(uint)ndims, true)}) which maps outside the element range of this arrays dimensions."); 
                                        }
                                        return m_strides[3 + m_index];  // note that m_curpos is reversed here! Hence, m_index does not correspond to the dimension index here! (double reversal required)
                                    }
                                }
                                // increment highdims 
                                int d = 1;
                                m_strides[3] = -1;
                                while (d < ndims) {
                                    if (m_strides[3 + d] < dims[d]) {
                                        m_strides[3 + d]++;
                                        m_strides[2] += dims[Size.MaxNumberOfDimensions + d];
                                        break;
                                    } else {
                                        m_strides[2] -= dims[d] * dims[Size.MaxNumberOfDimensions + d];
                                        m_strides[3 + d] = 0;
                                    }
                                    d++;
                                }
#if DEBUG
                                // this can regularly happen when m_nrDims > 1 ! 
                                //if (d == ndims) {
                                //    // we assumed that Current was only called after MoveNext() had been successful.
                                //    throw new InvalidOperationException("Current iteration pointer is not on a valid 'True' element and there are no more 'True' elements in the array. This indicates a bug. Please report it to ILNumerics!");
                                //}
#endif
                            } while (true); // assuming this always finds another True! (Otherwise, uncomment the check on d==ndims above)
                        }
                        // else: uptodate
                        if (m_strides[3 + m_index] > m_lastDimIdx) {
                            throw new IndexOutOfRangeException($"Boolean index array contains True values at #({Helper.dims2string(m_strides + 3, (uint)m_strides[1], true)}) which maps outside the element range of this arrays dimensions.");
                        }
                        return m_strides[3 + m_index];
                    }

                    #endregion

                    #region regular numeric index iterator 
                    System.Diagnostics.Debug.Assert(m_nrDims != null); 
                    long ret = m_offset;
                    for (int i = 0; i < m_nrDims[0]; i++) {
                        ret += m_curPos[i] * m_strides[i];
                    }

                    if (m_indexArrayBase != null) {
                        switch (m_elementType) {
                            case NumericType.Int32:
                                ret = (long)(*(int*)(m_indexArrayBase + ret));
                                break;
                            case NumericType.UInt32:
                                ret = (long)(*(uint*)(m_indexArrayBase + ret));
                                break;
                            case NumericType.Int64:
                                ret = (long)(*(long*)(m_indexArrayBase + ret));
                                break;
                            case NumericType.UInt64:
                                ret = (long)(*(ulong*)(m_indexArrayBase + ret));
                                break;
                            case NumericType.Double:
                                ret = (long)(*(double*)(m_indexArrayBase + ret));
                                break;
                            case NumericType.Single:
                                ret = (long)(*(float*)(m_indexArrayBase + ret));
                                break;
                            case NumericType.Byte:
                                ret = (long)(*(m_indexArrayBase + ret));
                                break;
                            case NumericType.SByte:
                                ret = (long)(*(sbyte*)(m_indexArrayBase + ret));
                                break;
                            case NumericType.Int16:
                                ret = (long)(*(short*)(m_indexArrayBase + ret));
                                break;
                            case NumericType.UInt16:
                                ret = (long)(*(ushort*)(m_indexArrayBase + ret));
                                break;
                            default:
                                throw new InvalidProgramException($"Invalid element type: {(NumericType)m_elementType}. Only integer types and non-complex floating point types are supported as indices.");
                        }
                    }
                    if (ret < 0) {
                        ret += m_lastDimIdx + 1;
                    }
                    if ((ulong)ret > (ulong)m_lastDimIdx) {
                        throw new IndexOutOfRangeException($"Index value provided for dimension #{m_index} was out of range. Expected: -{m_lastDimIdx + 1} <= d < {m_lastDimIdx + 1}. Found: {ret} at position: [{Global.Helper.dims2string(m_curPos, (uint)m_nrDims[0], true)}].");
                    }
                    return ret;
                    #endregion
                }
            }

            /// <summary>
            /// Moves the iterators cursor to the next position. Column major order.  
            /// </summary>
            /// <returns>True if the iterator was successfully incremented. Otherwise false is returned.</returns>
            /// <remarks>The iterator remains in consistent state, regardless of the result. If false is returned <see cref="Current"/> remains on the last element of the collection.</remarks>
            public bool MoveNext() {
                // this is NOT prepared to work with np-scalars (0-dim)! Those should be (are) sorted out before iteration.
                System.Diagnostics.Debug.Assert(m_nrDims != null && m_nrDims[0] > 0);

                if (m_curPos[0] < m_outDims[0] - 1) {

                    m_curPos[0]++;
                    return true;

                } else if (m_nrDims[0] > 1) {

                    uint d = 1;
                    while (d < m_nrDims[0]) {
                        if (m_curPos[d] < m_outDims[d] - 1) {
                            m_curPos[d]++;
                            break;
                        }
                        // m_curPos[d] = 0; <= don't! It leaves the iterator in an undefined state! 
                        d++;
                    }
                    if (d < m_nrDims[0]) {
                        // succeeded! clear all curPos up to dim #d
                        while (d-- > 0) {
                            m_curPos[d] = 0;
                        }
                        return true;
                    }
                }
                return false;
            }

            /// <summary>
            /// Resets this iterator and all other iterators sharing the same curPos buffer to initial state (before first position).
            /// </summary>
            public void Reset() {
                System.Diagnostics.Debug.Assert(m_nrDims != null && m_nrDims[0] > 0);
                if (m_elementType == NumericType.Boolean) {
                    var ndims = m_strides[1];
                    for (int i = -1; i <= ndims; i++) { // -1: erase highdims field
                        m_strides[2 + i] = 0; 
                    }
                    m_strides[0] = -1; // erase up-to date version flag
                    m_strides[3] = -1; // set internal position _before_ first element
                }
                m_curPos[0] = -1;
                for (int i = 1; i < m_nrDims[0]; i++) {
                    m_curPos[i] = 0;
                }
            }

        }

        #endregion Multidim Iterators for numpy advanced indexing 
    }
}
