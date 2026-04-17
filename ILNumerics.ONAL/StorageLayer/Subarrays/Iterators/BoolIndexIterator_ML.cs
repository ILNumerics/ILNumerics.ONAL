using ILNumerics.Core.Arrays;
using ILNumerics.Core.Global;
using ILNumerics.Core.MemoryLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.StorageLayer {
    internal static partial class Iterators {

        #region Multidim Iterator for numpy advanced indexing 

        /// <summary>
        /// Specialized iterator for ML logical indexing. Does not work with numpy scalars or empty arrays.
        /// </summary>
        /// <remarks>
        /// <para>This iterator takes an n-dim logical index array and iterates over the <i>sequential indices</i> of 
        /// its true elements in <i>column major order</i>. No support for numeric index arrays. No support for shared index arrays in the same index space set.</para>
        /// <para>This class is derived from MultidimIterators_np (implementation wise). It is NOT a struct 
        /// since it will always be used boxed into an IIndexIterator interface (ML style subarray expressions involving Logical arrays -> see: GetRange_ML / SetRange_ML). 
        /// The iterator is capable of iterating over the bool storage without a consolidating index array for the element
        /// positions of the true elements. It performs best for small bool indides and when the bool index does not need 
        /// to be iterated over multiple times.</para>
        /// <para>No checks are made for the length of dimensions / indices being within an allowed range etc. All checks must be made 
        /// by the user of the class.</para>
        /// <para>Implementation notes: Similarly to MultidimIterator_np this logical iterator DOES NOT accept numpy scalars (0 dim)
        /// bool arrays. This should naturally happen rarely: such scalars are created 
        /// in numpy ArrayStyle only. However, we are working around this as follows: numpy scalars are presented as 1D arrays by 
        /// providing strides and dims pointer which point to the '1' describing "Number of Elements" in the original BSD[1] element. 
        /// The m_nrDims attribute of this iterator is set to 1 than and the original strides and dims pointer from the arrays 
        /// BSD can be reused in the iterator. Hence, the iterator really ever sees arrays with 1D or more.</para></remarks>
        internal unsafe class BoolIndexIterator_ML : IIndexIterator {

            /// <summary>
            /// Byte pointer to index array storage (if any).
            /// </summary>
            internal byte* m_indexArrayBase;
            /// <summary>
            /// number of dimensions of this indexing array and the broadcasted result _for this input dimension_, number of entries in m_curPos and m_outDims.
            /// </summary>
            internal uint m_nrDims;
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

            internal long m_current;

            internal IDisposable m_array4Dispose; 

            internal long m_intCurIdx;
            /// <summary>
            /// the last index in the source dimension. Used for translating neg. index values during iteration. 
            /// </summary>
            private long m_maxIdx;
            private long m_nrTrues; 

            private NativeHostHandle m_handle;

            /// <summary>
            /// Initialize existing multi dim iterator struct for another array
            /// </summary>
            /// <param name="baseP">base pointer address as byte* pointer, including any base offset.</param>
            /// <param name="nrDims">Lengths of strides, outDims and curPos. Greater or equal to 1.</param>
            /// <param name="outDims">broadcasted size of target iteration shape, in bytes(!). This may change after initialization but before the iteration starts.</param>
            /// <param name="strides">Strides of the arrays dimensions.</param>
            /// <param name="curPos">temp position buffer of length according to <paramref name="nrDims"/>.Subject to change before iteration starts.</param>
            /// <param name="lastDimIdx">Index of the last element in the dimension this iterator will be applied to. </param>
            /// <param name="array4dispose"></param>
            /// <param name="knownMaxValue"></param>
            /// <param name="nrTrues"></param>

            internal BoolIndexIterator_ML(
                byte* baseP,
                uint nrDims,
                long* strides,
                long* outDims,
                long* curPos,
                long lastDimIdx, // for neg. index values during iteration
                IVolatile array4dispose,
                long knownMaxValue,
                long nrTrues) { // long step = 1) {

                m_indexArrayBase = baseP;
                m_nrDims = nrDims > 0 ? nrDims : 1; 
                m_strides = strides;
                m_outDims = outDims;
                m_curPos = curPos;
                if (m_curPos == (long*)0) {
                    m_handle = (NativeHostHandle)DeviceManagement.DeviceManager.GetDevice(0).New<long>((ulong)m_nrDims, false);
                    m_curPos = (long*)m_handle.Pointer; 
                }
                if (nrDims < 1) {
                    throw new NotSupportedException($"{nameof(BoolIndexIterator_ML)} requires 1 or more dimensions."); 
                }
                m_curPos[0] = -1; 
                for (int i = 1; i < nrDims; i++) {
                    m_curPos[i] = 0;
                }

                m_intCurIdx = -strides[0];
                m_current = -1;
                m_array4Dispose = array4dispose;
                m_maxIdx = knownMaxValue;
                m_nrTrues = nrTrues; 
            }
            /// <summary>
            /// Gets the current value this iterator points to. This must be called only, when MoveNext() had been succeeded.
            /// </summary>
            public long Current {
                get {
                    return m_current;
                }
            }

            object IEnumerator.Current => m_current;

            /// <summary>
            /// Moves the iterators cursor to the next position. Column major order.  
            /// </summary>
            /// <returns>True if the iterator was successfully incremented. Otherwise false is returned.</returns>
            /// <remarks>The iterator remains in consistent state, regardless of the result. If false is returned <see cref="Current"/> remains on the last element of the collection.</remarks>
            public bool MoveNext() {
                // this is NOT prepared to work with np-scalars (0-dim)! Those should be (are) sorted out before iteration.
                System.Diagnostics.Debug.Assert(m_nrDims > 0);
                int d = 0; 
                while (d < m_nrDims) {
                    if (++m_curPos[d] < m_outDims[d]) {
                        m_intCurIdx += m_strides[d];
                        m_current++; 
                        if (m_indexArrayBase[m_intCurIdx] != 0) {
                            return true;
                        } else {
                            d = 0; 
                        }
                    } else {
                        m_intCurIdx -= m_strides[d] * (m_outDims[d] - 1); 
                        m_curPos[d++] = 0; 
                    }
                }
                return false;
            }

            /// <summary>
            /// Resets this iterator and all other iterators sharing the same curPos buffer to initial state (before first position).
            /// </summary>
            public void Reset() {
                m_curPos[0] = -1;
                for (int i = 1; i < m_nrDims; i++) {
                    m_curPos[i] = 0;
                }
                m_current = -1;
                m_intCurIdx = -m_strides[0]; 
            }

            public long GetLength() {
                return m_nrTrues; 
            }

            public long? GetMaximum() {
                return m_maxIdx; // sic: this must not depend on m_nrTrues, m_maxIDX was (optionally explicitly) provided for this interface member.
            }

            public long? GetMinimum() {
                return null;
            }

            public long GetLastDimensionIndex() {
                return -1;
            }

            public long? GetStepSize() {
                return null; 
            }

            public IEnumerator<long> GetEnumerator() {
                return this; 
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return this; 
            }
            
            public void Dispose() {
                m_array4Dispose?.Dispose();
                m_handle?.Cache(DeviceManagement.DeviceManager.GetDevice(0).MemoryPool); 
            }
        }

        #endregion Multidim Iterators for numpy advanced indexing 
    }
}
