using ILNumerics.Core.StorageLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics {
    public static partial class ExtensionMethods {

        /// <summary>
        /// Efficient iterator over the array for use in foreach loops. 
        /// </summary>
        /// <param name="S">The array instance.</param>
        /// <param name="order">[Optional] Determines the order the elements are walked along. Default: <c>null</c> (<see cref="Settings.DefaultStorageOrder"/>)</param>
        /// <returns>Slim, self destructing iterator instance, iterating over all elements of this array.</returns>
        public unsafe static IEnumerable<long> Iterator(
            this Size S,
            StorageOrders? order = null) {

            if (object.Equals(S, null)) {
                throw new ArgumentNullException("Cannot iterate over 'null'.");
            }
            if (order == null) {
                order = Settings.DefaultStorageOrder;
            }
             
            if ((S.StorageOrder == order || S.NonSingletonDimensions < 2) && S.IsContinuous) {
                return new Iterators.ContinuousSizeIterator(S);
            } else {
                return new Iterators.StridedSizeIterator(S.GetBSD(write: false), order);
            }
        }

    }

    namespace Core.StorageLayer {

        internal static partial class Iterators {

            public unsafe struct StridedSizeIterator : IEnumerable<long>, IEnumerator<long> {

                long m_cur;
                long m_end;

                fixed long m_bsd[3 + 3 * Size.MaxNumberOfDimensions];
                long m_higDims;
                long m_stride0;  // use '0' as mark for 'empty' array!

                public StridedSizeIterator(long* bsd, StorageOrders? order) {
                    if (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor) {
                        throw new ArgumentException($"'order' must be ColumnMajor or RowMajor.");
                    }

                    m_cur = -1;
                    var ndims = (uint)bsd[0];

                    fixed (long* pCur = m_bsd) {
                        pCur[0] = (long)bsd[0];
                        pCur[1] = (long)bsd[1];
                        pCur[2] = (long)bsd[2];
                        m_higDims = pCur[2];

                        if (ndims == 0) {
                            // np.scalar
                            m_stride0 = 1;
                            m_end = m_higDims;
                        } else {
                            for (long i = 0; i < ndims; i++) {
                                if (order == StorageOrders.RowMajor) {
                                    pCur[i + 3] = (long)bsd[2 + ndims - i];
                                    pCur[i + 3 + ndims] = (long)bsd[2 + ndims * 2 - i];
                                } else {
                                    pCur[i + 3] = (long)bsd[i + 3];
                                    pCur[i + 3 + ndims] = (long)bsd[3 + ndims + i];
                                }
                                pCur[i + 3 + 2 * ndims] = 0;
                            }
                            m_stride0 = pCur[3 + ndims];
                            if (m_stride0 == 0) {
                                m_stride0 = 1;
                            }
                            if (pCur[1] == 0) {
                                m_stride0 = 0;  // mark empty
                            }
                            m_end = m_higDims + m_stride0 * (pCur[3] - 1);
                        }
                    }
                }

                public long Current {
                    get {
                        return m_cur;
                    }
                }
                object IEnumerator.Current {
                    get {
                        return m_cur;
                    }
                }

                public void Dispose() {
                }

                public IEnumerator<long> GetEnumerator() {
                    return this;
                }

                public bool MoveNext() {
                    if (m_cur < 0) {
                        m_cur = m_higDims;
                        return m_stride0 != 0;
                    } else if (m_cur < m_end) {
                        m_cur += m_stride0;
                    } else {
                        // increase higher dims
                        fixed (long* pBSD = m_bsd) {
                            if (pBSD[0] < 2) return false;
                            long d = 1, ndims = pBSD[0];
                            long* cur = pBSD + 3 + 2 * ndims;
                            while (true) {
                                if (cur[d] < pBSD[3 + d] - 1) {
                                    cur[d]++;
                                    m_higDims += pBSD[3 + d + ndims];
                                    break;
                                }
                                if (d + 1 == ndims) {
                                    return false;
                                }
                                cur[d] = 0;
                                m_higDims -= (pBSD[3 + d + ndims] * (pBSD[3 + d] - 1));
                                d++;
                            }
                            m_cur = m_higDims;
                            m_end = m_cur + m_stride0 * (pBSD[3] - 1);
                        }
                    }
                    return true;
                }

                public void Reset() {
                    m_cur = -1;
                    fixed (long* pBSD = m_bsd) {
                        long ndims = pBSD[0];
                        long* cur = pBSD + 3 + 2 * ndims;
                        for (int i = 0; i < ndims; i++) {
                            cur[i] = 0;
                        }
                        m_higDims = pBSD[2];
                        m_end = m_higDims + m_stride0 * (pBSD[3] - 1);
                    }
                }

                IEnumerator IEnumerable.GetEnumerator() {
                    return this;
                }
            }

            public unsafe struct BroadcastingSizeRowMajorIterator {

                long* m_strides;
                long* m_bcSize;
                long* m_curPos;
                uint m_nrDims; 
                long m_highdims; 

                /// <summary>
                /// Create a new broadcasting iterator for the given shapes / sizes. 
                /// </summary>
                /// <param name="size">Size object of the array to iterate.</param>
                /// <param name="bcSize">[In] required broadcasting size.</param>
                /// <param name="nrOutDims">Number of dimensions given in <paramref name="bcSize"/>. This can be different from <see cref="Size.NumberOfDimensions"></see>.</param>
                /// <param name="buffer">pointer to address space of min length <paramref name="nrOutDims"/> * 3.</param>
                public BroadcastingSizeRowMajorIterator(Size size, long* bcSize, uint nrOutDims, long** buffer) {

                    size.CheckIsBroadcastableTo_np(bcSize, nrOutDims);

                    m_nrDims = Math.Max(1, nrOutDims); // min 1 dimension!
                    m_strides = *buffer; *buffer += m_nrDims;
                    m_bcSize = *buffer; *buffer += m_nrDims; 
                    m_curPos = *buffer; *buffer += m_nrDims;

                    m_strides[0] = size.GetStride(size.NumberOfDimensions - 1); // m_nrDims - 1);
                    m_bcSize[0] = (nrOutDims == 0 ? 0 : bcSize[m_nrDims - 1]) - 1; 
                    m_curPos[0] = -1;
                    for (uint i = 1; i < m_nrDims; i++) {
                        m_curPos[i] = 0;
                        m_strides[i] = size.GetStride(size.NumberOfDimensions - 1 - i); // this was: m_nrDims - 1 - i); but we must align the incoming shape with the new shape AT THE LAST DIMENSION (numpy broadcasting!)
                        m_bcSize[i] = bcSize[m_nrDims - 1 - i] - 1;
                    }
                    m_highdims = size.BaseOffset; 
                }

                public long Current {
                    get { return m_curPos[0] * m_strides[0] + m_highdims; }
                }

                public void Reset() {
                    m_curPos[0] = -1;
                    for (uint i = 1; i < m_nrDims; i++) {
                        // don't loose the initial base offset from highdims!
                        m_highdims -= m_strides[i] * m_curPos[i];
                        m_curPos[i] = 0;
                    }
                }

                public bool MoveNext() {
                    if (m_curPos[0] < m_bcSize[0]) {
                        m_curPos[0]++;
                        return true; 
                    } else {
                        uint d = 1;
                        while (d < m_nrDims) {
                            if (m_curPos[d] < m_bcSize[d]) {
                                m_curPos[d]++;
                                m_highdims += m_strides[d];
                                // only now we can modify current state! 
                                while (d-- > 1) {
                                    m_curPos[d] = 0; 
                                    m_highdims -= m_strides[d] * m_bcSize[d]; 
                                }
                                m_curPos[0] = 0;
                                return true; 
                            }
                            d++; 
                        }
                        return false; 
                    }
                }

                internal long GetNext() {
#if DEBUG 
                    System.Diagnostics.Debug.Assert(MoveNext());
#else
                    // optimistic assumption. Make sure not to call this at the end of the iteration!
                    MoveNext(); 
#endif
                    return m_curPos[0] * m_strides[0] + m_highdims; 
                }
            }

        }

    }
}
