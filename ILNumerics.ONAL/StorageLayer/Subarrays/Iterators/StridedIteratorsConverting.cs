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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.StorageLayer {
    internal static partial class Iterators {

        #region HYCALPER LOOPSTART
        /*!HC:TYPELIST:
<hycalper>
<type>
    <source locate="here">
        Double
    </source>
    <destination>Single</destination>
    <destination>Complex</destination>
    <destination>FComplex</destination>
    <destination>ULong</destination>
    <destination>Long</destination>
    <destination>UInt</destination>
    <destination>Int32</destination>
    <destination>UShort</destination>
    <destination>Short</destination>
    <destination>Byte</destination>
    <destination>SByte</destination>
    <destination>Bool</destination>
</type>
<type>
    <source locate="here">
        double
    </source>
    <destination>float</destination>
    <destination>complex</destination>
    <destination>fcomplex</destination>
    <destination>ulong</destination>
    <destination>long</destination>
    <destination>uint</destination>
    <destination>int</destination>
    <destination>ushort</destination>
    <destination>short</destination>
    <destination>byte</destination>
    <destination>sbyte</destination>
    <destination>bool</destination>
</type>
</hycalper>
*/

        public unsafe struct StridedIteratorDouble32<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            double* m_start;
            double* m_curP;
            double* m_end;
            Func<double, Tout> m_converter; 

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorDouble32(double* start, long* bsd, StorageOrders? order, IDisposable obj, Func<double, Tout> converter) {
                m_start = start;
                m_curP = (double*)0;
                m_converter = converter; 
                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = ndims;
                    pCur[1] = (uint)bsd[1];
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start; 
                    } else {
                        // offset from bsd is ignored!
                        for (uint i = 0; i < ndims; i++) {
                            if (order == StorageOrders.RowMajor) {
                                pCur[i + 3] = (uint)bsd[2 + ndims - i];
                                pCur[i + 3 + ndims] = (uint)bsd[2 + ndims * 2 - i];
                            } else {
                                pCur[i + 3] = (uint)bsd[i + 3];
                                pCur[i + 3 + ndims] = (uint)bsd[3 + ndims + i];
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
                        m_end = m_start + m_stride0 * (pCur[3] - 1);
                    }
                    m_higDims = 0;
                }
                m_obj = obj;
            }

            public Tout Current => m_converter(*m_curP);

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() => this;

            public bool MoveNext() {
                if (m_curP == (double*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (uint* pBSD = m_bsd) {
                        if (pBSD[0] < 2) return false; 
                        uint d = 1, ndims = pBSD[0];
                        uint* cur = pBSD + 3 + 2 * ndims;
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (double*)0;
                fixed (uint* pBSD = m_bsd) {
                    uint ndims = pBSD[0];
                    uint* cur = pBSD + 3 + 2 * ndims;
                    for (int i = 0; i < ndims; i++) {
                        cur[i] = 0;
                    }
                    m_end = m_start + m_stride0 * (pBSD[3] - 1);
                }
                m_higDims = 0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }
        public unsafe struct StridedIteratorDouble64<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            double* m_start;
            double* m_curP;
            double* m_end;
            Func<double, Tout> m_converter;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorDouble64(double* start, long* bsd, StorageOrders? order, IDisposable obj, Func<double, Tout> converter) {
                m_start = start;
                m_curP = (double*)0;
                m_converter = converter;
                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd)
                {
                    pCur[0] = (long)bsd[0];
                    pCur[1] = (long)bsd[1];
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
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
                        m_end = m_start + m_stride0 * (pCur[3] - 1);
                    }
                }
                m_higDims = 0;
                m_obj = obj;
            }

            public Tout Current => m_converter(*m_curP);

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() => this;

            public bool MoveNext() {
                if (m_curP == (double*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (long* pBSD = m_bsd)
                    {
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (double*)0;
                fixed (long* pBSD = m_bsd)
                {
                    long ndims = pBSD[0];
                    long* cur = pBSD + 3 + 2 * ndims;
                    for (int i = 0; i < ndims; i++) {
                        cur[i] = 0;
                    }
                    m_end = m_start + m_stride0 * (pBSD[3] - 1);
                }
                m_higDims = 0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        public unsafe struct StridedIteratorBool32<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            bool* m_start;
            bool* m_curP;
            bool* m_end;
            Func<bool, Tout> m_converter; 

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorBool32(bool* start, long* bsd, StorageOrders? order, IDisposable obj, Func<bool, Tout> converter) {
                m_start = start;
                m_curP = (bool*)0;
                m_converter = converter; 
                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = ndims;
                    pCur[1] = (uint)bsd[1];
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start; 
                    } else {
                        // offset from bsd is ignored!
                        for (uint i = 0; i < ndims; i++) {
                            if (order == StorageOrders.RowMajor) {
                                pCur[i + 3] = (uint)bsd[2 + ndims - i];
                                pCur[i + 3 + ndims] = (uint)bsd[2 + ndims * 2 - i];
                            } else {
                                pCur[i + 3] = (uint)bsd[i + 3];
                                pCur[i + 3 + ndims] = (uint)bsd[3 + ndims + i];
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
                        m_end = m_start + m_stride0 * (pCur[3] - 1);
                    }
                    m_higDims = 0;
                }
                m_obj = obj;
            }

            public Tout Current => m_converter(*m_curP);

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() => this;

            public bool MoveNext() {
                if (m_curP == (bool*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (uint* pBSD = m_bsd) {
                        if (pBSD[0] < 2) return false; 
                        uint d = 1, ndims = pBSD[0];
                        uint* cur = pBSD + 3 + 2 * ndims;
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (bool*)0;
                fixed (uint* pBSD = m_bsd) {
                    uint ndims = pBSD[0];
                    uint* cur = pBSD + 3 + 2 * ndims;
                    for (int i = 0; i < ndims; i++) {
                        cur[i] = 0;
                    }
                    m_end = m_start + m_stride0 * (pBSD[3] - 1);
                }
                m_higDims = 0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }
        public unsafe struct StridedIteratorBool64<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            bool* m_start;
            bool* m_curP;
            bool* m_end;
            Func<bool, Tout> m_converter;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorBool64(bool* start, long* bsd, StorageOrders? order, IDisposable obj, Func<bool, Tout> converter) {
                m_start = start;
                m_curP = (bool*)0;
                m_converter = converter;
                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd)
                {
                    pCur[0] = (long)bsd[0];
                    pCur[1] = (long)bsd[1];
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
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
                        m_end = m_start + m_stride0 * (pCur[3] - 1);
                    }
                }
                m_higDims = 0;
                m_obj = obj;
            }

            public Tout Current => m_converter(*m_curP);

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() => this;

            public bool MoveNext() {
                if (m_curP == (bool*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (long* pBSD = m_bsd)
                    {
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (bool*)0;
                fixed (long* pBSD = m_bsd)
                {
                    long ndims = pBSD[0];
                    long* cur = pBSD + 3 + 2 * ndims;
                    for (int i = 0; i < ndims; i++) {
                        cur[i] = 0;
                    }
                    m_end = m_start + m_stride0 * (pBSD[3] - 1);
                }
                m_higDims = 0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }
       

        public unsafe struct StridedIteratorSByte32<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            sbyte* m_start;
            sbyte* m_curP;
            sbyte* m_end;
            Func<sbyte, Tout> m_converter; 

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorSByte32(sbyte* start, long* bsd, StorageOrders? order, IDisposable obj, Func<sbyte, Tout> converter) {
                m_start = start;
                m_curP = (sbyte*)0;
                m_converter = converter; 
                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = ndims;
                    pCur[1] = (uint)bsd[1];
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start; 
                    } else {
                        // offset from bsd is ignored!
                        for (uint i = 0; i < ndims; i++) {
                            if (order == StorageOrders.RowMajor) {
                                pCur[i + 3] = (uint)bsd[2 + ndims - i];
                                pCur[i + 3 + ndims] = (uint)bsd[2 + ndims * 2 - i];
                            } else {
                                pCur[i + 3] = (uint)bsd[i + 3];
                                pCur[i + 3 + ndims] = (uint)bsd[3 + ndims + i];
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
                        m_end = m_start + m_stride0 * (pCur[3] - 1);
                    }
                    m_higDims = 0;
                }
                m_obj = obj;
            }

            public Tout Current => m_converter(*m_curP);

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() => this;

            public bool MoveNext() {
                if (m_curP == (sbyte*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (uint* pBSD = m_bsd) {
                        if (pBSD[0] < 2) return false; 
                        uint d = 1, ndims = pBSD[0];
                        uint* cur = pBSD + 3 + 2 * ndims;
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (sbyte*)0;
                fixed (uint* pBSD = m_bsd) {
                    uint ndims = pBSD[0];
                    uint* cur = pBSD + 3 + 2 * ndims;
                    for (int i = 0; i < ndims; i++) {
                        cur[i] = 0;
                    }
                    m_end = m_start + m_stride0 * (pBSD[3] - 1);
                }
                m_higDims = 0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }
        public unsafe struct StridedIteratorSByte64<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            sbyte* m_start;
            sbyte* m_curP;
            sbyte* m_end;
            Func<sbyte, Tout> m_converter;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorSByte64(sbyte* start, long* bsd, StorageOrders? order, IDisposable obj, Func<sbyte, Tout> converter) {
                m_start = start;
                m_curP = (sbyte*)0;
                m_converter = converter;
                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd)
                {
                    pCur[0] = (long)bsd[0];
                    pCur[1] = (long)bsd[1];
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
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
                        m_end = m_start + m_stride0 * (pCur[3] - 1);
                    }
                }
                m_higDims = 0;
                m_obj = obj;
            }

            public Tout Current => m_converter(*m_curP);

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() => this;

            public bool MoveNext() {
                if (m_curP == (sbyte*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (long* pBSD = m_bsd)
                    {
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (sbyte*)0;
                fixed (long* pBSD = m_bsd)
                {
                    long ndims = pBSD[0];
                    long* cur = pBSD + 3 + 2 * ndims;
                    for (int i = 0; i < ndims; i++) {
                        cur[i] = 0;
                    }
                    m_end = m_start + m_stride0 * (pBSD[3] - 1);
                }
                m_higDims = 0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }
       

        public unsafe struct StridedIteratorByte32<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            byte* m_start;
            byte* m_curP;
            byte* m_end;
            Func<byte, Tout> m_converter; 

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorByte32(byte* start, long* bsd, StorageOrders? order, IDisposable obj, Func<byte, Tout> converter) {
                m_start = start;
                m_curP = (byte*)0;
                m_converter = converter; 
                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = ndims;
                    pCur[1] = (uint)bsd[1];
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start; 
                    } else {
                        // offset from bsd is ignored!
                        for (uint i = 0; i < ndims; i++) {
                            if (order == StorageOrders.RowMajor) {
                                pCur[i + 3] = (uint)bsd[2 + ndims - i];
                                pCur[i + 3 + ndims] = (uint)bsd[2 + ndims * 2 - i];
                            } else {
                                pCur[i + 3] = (uint)bsd[i + 3];
                                pCur[i + 3 + ndims] = (uint)bsd[3 + ndims + i];
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
                        m_end = m_start + m_stride0 * (pCur[3] - 1);
                    }
                    m_higDims = 0;
                }
                m_obj = obj;
            }

            public Tout Current => m_converter(*m_curP);

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() => this;

            public bool MoveNext() {
                if (m_curP == (byte*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (uint* pBSD = m_bsd) {
                        if (pBSD[0] < 2) return false; 
                        uint d = 1, ndims = pBSD[0];
                        uint* cur = pBSD + 3 + 2 * ndims;
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (byte*)0;
                fixed (uint* pBSD = m_bsd) {
                    uint ndims = pBSD[0];
                    uint* cur = pBSD + 3 + 2 * ndims;
                    for (int i = 0; i < ndims; i++) {
                        cur[i] = 0;
                    }
                    m_end = m_start + m_stride0 * (pBSD[3] - 1);
                }
                m_higDims = 0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }
        public unsafe struct StridedIteratorByte64<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            byte* m_start;
            byte* m_curP;
            byte* m_end;
            Func<byte, Tout> m_converter;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorByte64(byte* start, long* bsd, StorageOrders? order, IDisposable obj, Func<byte, Tout> converter) {
                m_start = start;
                m_curP = (byte*)0;
                m_converter = converter;
                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd)
                {
                    pCur[0] = (long)bsd[0];
                    pCur[1] = (long)bsd[1];
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
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
                        m_end = m_start + m_stride0 * (pCur[3] - 1);
                    }
                }
                m_higDims = 0;
                m_obj = obj;
            }

            public Tout Current => m_converter(*m_curP);

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() => this;

            public bool MoveNext() {
                if (m_curP == (byte*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (long* pBSD = m_bsd)
                    {
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (byte*)0;
                fixed (long* pBSD = m_bsd)
                {
                    long ndims = pBSD[0];
                    long* cur = pBSD + 3 + 2 * ndims;
                    for (int i = 0; i < ndims; i++) {
                        cur[i] = 0;
                    }
                    m_end = m_start + m_stride0 * (pBSD[3] - 1);
                }
                m_higDims = 0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }
       

        public unsafe struct StridedIteratorShort32<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            short* m_start;
            short* m_curP;
            short* m_end;
            Func<short, Tout> m_converter; 

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorShort32(short* start, long* bsd, StorageOrders? order, IDisposable obj, Func<short, Tout> converter) {
                m_start = start;
                m_curP = (short*)0;
                m_converter = converter; 
                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = ndims;
                    pCur[1] = (uint)bsd[1];
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start; 
                    } else {
                        // offset from bsd is ignored!
                        for (uint i = 0; i < ndims; i++) {
                            if (order == StorageOrders.RowMajor) {
                                pCur[i + 3] = (uint)bsd[2 + ndims - i];
                                pCur[i + 3 + ndims] = (uint)bsd[2 + ndims * 2 - i];
                            } else {
                                pCur[i + 3] = (uint)bsd[i + 3];
                                pCur[i + 3 + ndims] = (uint)bsd[3 + ndims + i];
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
                        m_end = m_start + m_stride0 * (pCur[3] - 1);
                    }
                    m_higDims = 0;
                }
                m_obj = obj;
            }

            public Tout Current => m_converter(*m_curP);

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() => this;

            public bool MoveNext() {
                if (m_curP == (short*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (uint* pBSD = m_bsd) {
                        if (pBSD[0] < 2) return false; 
                        uint d = 1, ndims = pBSD[0];
                        uint* cur = pBSD + 3 + 2 * ndims;
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (short*)0;
                fixed (uint* pBSD = m_bsd) {
                    uint ndims = pBSD[0];
                    uint* cur = pBSD + 3 + 2 * ndims;
                    for (int i = 0; i < ndims; i++) {
                        cur[i] = 0;
                    }
                    m_end = m_start + m_stride0 * (pBSD[3] - 1);
                }
                m_higDims = 0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }
        public unsafe struct StridedIteratorShort64<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            short* m_start;
            short* m_curP;
            short* m_end;
            Func<short, Tout> m_converter;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorShort64(short* start, long* bsd, StorageOrders? order, IDisposable obj, Func<short, Tout> converter) {
                m_start = start;
                m_curP = (short*)0;
                m_converter = converter;
                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd)
                {
                    pCur[0] = (long)bsd[0];
                    pCur[1] = (long)bsd[1];
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
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
                        m_end = m_start + m_stride0 * (pCur[3] - 1);
                    }
                }
                m_higDims = 0;
                m_obj = obj;
            }

            public Tout Current => m_converter(*m_curP);

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() => this;

            public bool MoveNext() {
                if (m_curP == (short*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (long* pBSD = m_bsd)
                    {
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (short*)0;
                fixed (long* pBSD = m_bsd)
                {
                    long ndims = pBSD[0];
                    long* cur = pBSD + 3 + 2 * ndims;
                    for (int i = 0; i < ndims; i++) {
                        cur[i] = 0;
                    }
                    m_end = m_start + m_stride0 * (pBSD[3] - 1);
                }
                m_higDims = 0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }
       

        public unsafe struct StridedIteratorUShort32<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            ushort* m_start;
            ushort* m_curP;
            ushort* m_end;
            Func<ushort, Tout> m_converter; 

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorUShort32(ushort* start, long* bsd, StorageOrders? order, IDisposable obj, Func<ushort, Tout> converter) {
                m_start = start;
                m_curP = (ushort*)0;
                m_converter = converter; 
                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = ndims;
                    pCur[1] = (uint)bsd[1];
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start; 
                    } else {
                        // offset from bsd is ignored!
                        for (uint i = 0; i < ndims; i++) {
                            if (order == StorageOrders.RowMajor) {
                                pCur[i + 3] = (uint)bsd[2 + ndims - i];
                                pCur[i + 3 + ndims] = (uint)bsd[2 + ndims * 2 - i];
                            } else {
                                pCur[i + 3] = (uint)bsd[i + 3];
                                pCur[i + 3 + ndims] = (uint)bsd[3 + ndims + i];
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
                        m_end = m_start + m_stride0 * (pCur[3] - 1);
                    }
                    m_higDims = 0;
                }
                m_obj = obj;
            }

            public Tout Current => m_converter(*m_curP);

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() => this;

            public bool MoveNext() {
                if (m_curP == (ushort*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (uint* pBSD = m_bsd) {
                        if (pBSD[0] < 2) return false; 
                        uint d = 1, ndims = pBSD[0];
                        uint* cur = pBSD + 3 + 2 * ndims;
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (ushort*)0;
                fixed (uint* pBSD = m_bsd) {
                    uint ndims = pBSD[0];
                    uint* cur = pBSD + 3 + 2 * ndims;
                    for (int i = 0; i < ndims; i++) {
                        cur[i] = 0;
                    }
                    m_end = m_start + m_stride0 * (pBSD[3] - 1);
                }
                m_higDims = 0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }
        public unsafe struct StridedIteratorUShort64<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            ushort* m_start;
            ushort* m_curP;
            ushort* m_end;
            Func<ushort, Tout> m_converter;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorUShort64(ushort* start, long* bsd, StorageOrders? order, IDisposable obj, Func<ushort, Tout> converter) {
                m_start = start;
                m_curP = (ushort*)0;
                m_converter = converter;
                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd)
                {
                    pCur[0] = (long)bsd[0];
                    pCur[1] = (long)bsd[1];
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
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
                        m_end = m_start + m_stride0 * (pCur[3] - 1);
                    }
                }
                m_higDims = 0;
                m_obj = obj;
            }

            public Tout Current => m_converter(*m_curP);

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() => this;

            public bool MoveNext() {
                if (m_curP == (ushort*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (long* pBSD = m_bsd)
                    {
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (ushort*)0;
                fixed (long* pBSD = m_bsd)
                {
                    long ndims = pBSD[0];
                    long* cur = pBSD + 3 + 2 * ndims;
                    for (int i = 0; i < ndims; i++) {
                        cur[i] = 0;
                    }
                    m_end = m_start + m_stride0 * (pBSD[3] - 1);
                }
                m_higDims = 0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }
       

        public unsafe struct StridedIteratorInt3232<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            int* m_start;
            int* m_curP;
            int* m_end;
            Func<int, Tout> m_converter; 

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorInt3232(int* start, long* bsd, StorageOrders? order, IDisposable obj, Func<int, Tout> converter) {
                m_start = start;
                m_curP = (int*)0;
                m_converter = converter; 
                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = ndims;
                    pCur[1] = (uint)bsd[1];
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start; 
                    } else {
                        // offset from bsd is ignored!
                        for (uint i = 0; i < ndims; i++) {
                            if (order == StorageOrders.RowMajor) {
                                pCur[i + 3] = (uint)bsd[2 + ndims - i];
                                pCur[i + 3 + ndims] = (uint)bsd[2 + ndims * 2 - i];
                            } else {
                                pCur[i + 3] = (uint)bsd[i + 3];
                                pCur[i + 3 + ndims] = (uint)bsd[3 + ndims + i];
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
                        m_end = m_start + m_stride0 * (pCur[3] - 1);
                    }
                    m_higDims = 0;
                }
                m_obj = obj;
            }

            public Tout Current => m_converter(*m_curP);

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() => this;

            public bool MoveNext() {
                if (m_curP == (int*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (uint* pBSD = m_bsd) {
                        if (pBSD[0] < 2) return false; 
                        uint d = 1, ndims = pBSD[0];
                        uint* cur = pBSD + 3 + 2 * ndims;
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (int*)0;
                fixed (uint* pBSD = m_bsd) {
                    uint ndims = pBSD[0];
                    uint* cur = pBSD + 3 + 2 * ndims;
                    for (int i = 0; i < ndims; i++) {
                        cur[i] = 0;
                    }
                    m_end = m_start + m_stride0 * (pBSD[3] - 1);
                }
                m_higDims = 0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }
        public unsafe struct StridedIteratorInt3264<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            int* m_start;
            int* m_curP;
            int* m_end;
            Func<int, Tout> m_converter;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorInt3264(int* start, long* bsd, StorageOrders? order, IDisposable obj, Func<int, Tout> converter) {
                m_start = start;
                m_curP = (int*)0;
                m_converter = converter;
                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd)
                {
                    pCur[0] = (long)bsd[0];
                    pCur[1] = (long)bsd[1];
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
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
                        m_end = m_start + m_stride0 * (pCur[3] - 1);
                    }
                }
                m_higDims = 0;
                m_obj = obj;
            }

            public Tout Current => m_converter(*m_curP);

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() => this;

            public bool MoveNext() {
                if (m_curP == (int*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (long* pBSD = m_bsd)
                    {
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (int*)0;
                fixed (long* pBSD = m_bsd)
                {
                    long ndims = pBSD[0];
                    long* cur = pBSD + 3 + 2 * ndims;
                    for (int i = 0; i < ndims; i++) {
                        cur[i] = 0;
                    }
                    m_end = m_start + m_stride0 * (pBSD[3] - 1);
                }
                m_higDims = 0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }
       

        public unsafe struct StridedIteratorUInt32<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            uint* m_start;
            uint* m_curP;
            uint* m_end;
            Func<uint, Tout> m_converter; 

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorUInt32(uint* start, long* bsd, StorageOrders? order, IDisposable obj, Func<uint, Tout> converter) {
                m_start = start;
                m_curP = (uint*)0;
                m_converter = converter; 
                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = ndims;
                    pCur[1] = (uint)bsd[1];
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start; 
                    } else {
                        // offset from bsd is ignored!
                        for (uint i = 0; i < ndims; i++) {
                            if (order == StorageOrders.RowMajor) {
                                pCur[i + 3] = (uint)bsd[2 + ndims - i];
                                pCur[i + 3 + ndims] = (uint)bsd[2 + ndims * 2 - i];
                            } else {
                                pCur[i + 3] = (uint)bsd[i + 3];
                                pCur[i + 3 + ndims] = (uint)bsd[3 + ndims + i];
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
                        m_end = m_start + m_stride0 * (pCur[3] - 1);
                    }
                    m_higDims = 0;
                }
                m_obj = obj;
            }

            public Tout Current => m_converter(*m_curP);

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() => this;

            public bool MoveNext() {
                if (m_curP == (uint*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (uint* pBSD = m_bsd) {
                        if (pBSD[0] < 2) return false; 
                        uint d = 1, ndims = pBSD[0];
                        uint* cur = pBSD + 3 + 2 * ndims;
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (uint*)0;
                fixed (uint* pBSD = m_bsd) {
                    uint ndims = pBSD[0];
                    uint* cur = pBSD + 3 + 2 * ndims;
                    for (int i = 0; i < ndims; i++) {
                        cur[i] = 0;
                    }
                    m_end = m_start + m_stride0 * (pBSD[3] - 1);
                }
                m_higDims = 0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }
        public unsafe struct StridedIteratorUInt64<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            uint* m_start;
            uint* m_curP;
            uint* m_end;
            Func<uint, Tout> m_converter;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorUInt64(uint* start, long* bsd, StorageOrders? order, IDisposable obj, Func<uint, Tout> converter) {
                m_start = start;
                m_curP = (uint*)0;
                m_converter = converter;
                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd)
                {
                    pCur[0] = (long)bsd[0];
                    pCur[1] = (long)bsd[1];
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
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
                        m_end = m_start + m_stride0 * (pCur[3] - 1);
                    }
                }
                m_higDims = 0;
                m_obj = obj;
            }

            public Tout Current => m_converter(*m_curP);

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() => this;

            public bool MoveNext() {
                if (m_curP == (uint*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (long* pBSD = m_bsd)
                    {
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (uint*)0;
                fixed (long* pBSD = m_bsd)
                {
                    long ndims = pBSD[0];
                    long* cur = pBSD + 3 + 2 * ndims;
                    for (int i = 0; i < ndims; i++) {
                        cur[i] = 0;
                    }
                    m_end = m_start + m_stride0 * (pBSD[3] - 1);
                }
                m_higDims = 0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }
       

        public unsafe struct StridedIteratorLong32<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            long* m_start;
            long* m_curP;
            long* m_end;
            Func<long, Tout> m_converter; 

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorLong32(long* start, long* bsd, StorageOrders? order, IDisposable obj, Func<long, Tout> converter) {
                m_start = start;
                m_curP = (long*)0;
                m_converter = converter; 
                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = ndims;
                    pCur[1] = (uint)bsd[1];
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start; 
                    } else {
                        // offset from bsd is ignored!
                        for (uint i = 0; i < ndims; i++) {
                            if (order == StorageOrders.RowMajor) {
                                pCur[i + 3] = (uint)bsd[2 + ndims - i];
                                pCur[i + 3 + ndims] = (uint)bsd[2 + ndims * 2 - i];
                            } else {
                                pCur[i + 3] = (uint)bsd[i + 3];
                                pCur[i + 3 + ndims] = (uint)bsd[3 + ndims + i];
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
                        m_end = m_start + m_stride0 * (pCur[3] - 1);
                    }
                    m_higDims = 0;
                }
                m_obj = obj;
            }

            public Tout Current => m_converter(*m_curP);

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() => this;

            public bool MoveNext() {
                if (m_curP == (long*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (uint* pBSD = m_bsd) {
                        if (pBSD[0] < 2) return false; 
                        uint d = 1, ndims = pBSD[0];
                        uint* cur = pBSD + 3 + 2 * ndims;
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (long*)0;
                fixed (uint* pBSD = m_bsd) {
                    uint ndims = pBSD[0];
                    uint* cur = pBSD + 3 + 2 * ndims;
                    for (int i = 0; i < ndims; i++) {
                        cur[i] = 0;
                    }
                    m_end = m_start + m_stride0 * (pBSD[3] - 1);
                }
                m_higDims = 0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }
        public unsafe struct StridedIteratorLong64<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            long* m_start;
            long* m_curP;
            long* m_end;
            Func<long, Tout> m_converter;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorLong64(long* start, long* bsd, StorageOrders? order, IDisposable obj, Func<long, Tout> converter) {
                m_start = start;
                m_curP = (long*)0;
                m_converter = converter;
                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd)
                {
                    pCur[0] = (long)bsd[0];
                    pCur[1] = (long)bsd[1];
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
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
                        m_end = m_start + m_stride0 * (pCur[3] - 1);
                    }
                }
                m_higDims = 0;
                m_obj = obj;
            }

            public Tout Current => m_converter(*m_curP);

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() => this;

            public bool MoveNext() {
                if (m_curP == (long*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (long* pBSD = m_bsd)
                    {
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (long*)0;
                fixed (long* pBSD = m_bsd)
                {
                    long ndims = pBSD[0];
                    long* cur = pBSD + 3 + 2 * ndims;
                    for (int i = 0; i < ndims; i++) {
                        cur[i] = 0;
                    }
                    m_end = m_start + m_stride0 * (pBSD[3] - 1);
                }
                m_higDims = 0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }
       

        public unsafe struct StridedIteratorULong32<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            ulong* m_start;
            ulong* m_curP;
            ulong* m_end;
            Func<ulong, Tout> m_converter; 

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorULong32(ulong* start, long* bsd, StorageOrders? order, IDisposable obj, Func<ulong, Tout> converter) {
                m_start = start;
                m_curP = (ulong*)0;
                m_converter = converter; 
                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = ndims;
                    pCur[1] = (uint)bsd[1];
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start; 
                    } else {
                        // offset from bsd is ignored!
                        for (uint i = 0; i < ndims; i++) {
                            if (order == StorageOrders.RowMajor) {
                                pCur[i + 3] = (uint)bsd[2 + ndims - i];
                                pCur[i + 3 + ndims] = (uint)bsd[2 + ndims * 2 - i];
                            } else {
                                pCur[i + 3] = (uint)bsd[i + 3];
                                pCur[i + 3 + ndims] = (uint)bsd[3 + ndims + i];
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
                        m_end = m_start + m_stride0 * (pCur[3] - 1);
                    }
                    m_higDims = 0;
                }
                m_obj = obj;
            }

            public Tout Current => m_converter(*m_curP);

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() => this;

            public bool MoveNext() {
                if (m_curP == (ulong*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (uint* pBSD = m_bsd) {
                        if (pBSD[0] < 2) return false; 
                        uint d = 1, ndims = pBSD[0];
                        uint* cur = pBSD + 3 + 2 * ndims;
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (ulong*)0;
                fixed (uint* pBSD = m_bsd) {
                    uint ndims = pBSD[0];
                    uint* cur = pBSD + 3 + 2 * ndims;
                    for (int i = 0; i < ndims; i++) {
                        cur[i] = 0;
                    }
                    m_end = m_start + m_stride0 * (pBSD[3] - 1);
                }
                m_higDims = 0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }
        public unsafe struct StridedIteratorULong64<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            ulong* m_start;
            ulong* m_curP;
            ulong* m_end;
            Func<ulong, Tout> m_converter;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorULong64(ulong* start, long* bsd, StorageOrders? order, IDisposable obj, Func<ulong, Tout> converter) {
                m_start = start;
                m_curP = (ulong*)0;
                m_converter = converter;
                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd)
                {
                    pCur[0] = (long)bsd[0];
                    pCur[1] = (long)bsd[1];
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
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
                        m_end = m_start + m_stride0 * (pCur[3] - 1);
                    }
                }
                m_higDims = 0;
                m_obj = obj;
            }

            public Tout Current => m_converter(*m_curP);

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() => this;

            public bool MoveNext() {
                if (m_curP == (ulong*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (long* pBSD = m_bsd)
                    {
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (ulong*)0;
                fixed (long* pBSD = m_bsd)
                {
                    long ndims = pBSD[0];
                    long* cur = pBSD + 3 + 2 * ndims;
                    for (int i = 0; i < ndims; i++) {
                        cur[i] = 0;
                    }
                    m_end = m_start + m_stride0 * (pBSD[3] - 1);
                }
                m_higDims = 0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }
       

        public unsafe struct StridedIteratorFComplex32<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            fcomplex* m_start;
            fcomplex* m_curP;
            fcomplex* m_end;
            Func<fcomplex, Tout> m_converter; 

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorFComplex32(fcomplex* start, long* bsd, StorageOrders? order, IDisposable obj, Func<fcomplex, Tout> converter) {
                m_start = start;
                m_curP = (fcomplex*)0;
                m_converter = converter; 
                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = ndims;
                    pCur[1] = (uint)bsd[1];
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start; 
                    } else {
                        // offset from bsd is ignored!
                        for (uint i = 0; i < ndims; i++) {
                            if (order == StorageOrders.RowMajor) {
                                pCur[i + 3] = (uint)bsd[2 + ndims - i];
                                pCur[i + 3 + ndims] = (uint)bsd[2 + ndims * 2 - i];
                            } else {
                                pCur[i + 3] = (uint)bsd[i + 3];
                                pCur[i + 3 + ndims] = (uint)bsd[3 + ndims + i];
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
                        m_end = m_start + m_stride0 * (pCur[3] - 1);
                    }
                    m_higDims = 0;
                }
                m_obj = obj;
            }

            public Tout Current => m_converter(*m_curP);

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() => this;

            public bool MoveNext() {
                if (m_curP == (fcomplex*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (uint* pBSD = m_bsd) {
                        if (pBSD[0] < 2) return false; 
                        uint d = 1, ndims = pBSD[0];
                        uint* cur = pBSD + 3 + 2 * ndims;
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (fcomplex*)0;
                fixed (uint* pBSD = m_bsd) {
                    uint ndims = pBSD[0];
                    uint* cur = pBSD + 3 + 2 * ndims;
                    for (int i = 0; i < ndims; i++) {
                        cur[i] = 0;
                    }
                    m_end = m_start + m_stride0 * (pBSD[3] - 1);
                }
                m_higDims = 0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }
        public unsafe struct StridedIteratorFComplex64<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            fcomplex* m_start;
            fcomplex* m_curP;
            fcomplex* m_end;
            Func<fcomplex, Tout> m_converter;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorFComplex64(fcomplex* start, long* bsd, StorageOrders? order, IDisposable obj, Func<fcomplex, Tout> converter) {
                m_start = start;
                m_curP = (fcomplex*)0;
                m_converter = converter;
                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd)
                {
                    pCur[0] = (long)bsd[0];
                    pCur[1] = (long)bsd[1];
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
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
                        m_end = m_start + m_stride0 * (pCur[3] - 1);
                    }
                }
                m_higDims = 0;
                m_obj = obj;
            }

            public Tout Current => m_converter(*m_curP);

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() => this;

            public bool MoveNext() {
                if (m_curP == (fcomplex*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (long* pBSD = m_bsd)
                    {
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (fcomplex*)0;
                fixed (long* pBSD = m_bsd)
                {
                    long ndims = pBSD[0];
                    long* cur = pBSD + 3 + 2 * ndims;
                    for (int i = 0; i < ndims; i++) {
                        cur[i] = 0;
                    }
                    m_end = m_start + m_stride0 * (pBSD[3] - 1);
                }
                m_higDims = 0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }
       

        public unsafe struct StridedIteratorComplex32<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            complex* m_start;
            complex* m_curP;
            complex* m_end;
            Func<complex, Tout> m_converter; 

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorComplex32(complex* start, long* bsd, StorageOrders? order, IDisposable obj, Func<complex, Tout> converter) {
                m_start = start;
                m_curP = (complex*)0;
                m_converter = converter; 
                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = ndims;
                    pCur[1] = (uint)bsd[1];
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start; 
                    } else {
                        // offset from bsd is ignored!
                        for (uint i = 0; i < ndims; i++) {
                            if (order == StorageOrders.RowMajor) {
                                pCur[i + 3] = (uint)bsd[2 + ndims - i];
                                pCur[i + 3 + ndims] = (uint)bsd[2 + ndims * 2 - i];
                            } else {
                                pCur[i + 3] = (uint)bsd[i + 3];
                                pCur[i + 3 + ndims] = (uint)bsd[3 + ndims + i];
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
                        m_end = m_start + m_stride0 * (pCur[3] - 1);
                    }
                    m_higDims = 0;
                }
                m_obj = obj;
            }

            public Tout Current => m_converter(*m_curP);

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() => this;

            public bool MoveNext() {
                if (m_curP == (complex*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (uint* pBSD = m_bsd) {
                        if (pBSD[0] < 2) return false; 
                        uint d = 1, ndims = pBSD[0];
                        uint* cur = pBSD + 3 + 2 * ndims;
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (complex*)0;
                fixed (uint* pBSD = m_bsd) {
                    uint ndims = pBSD[0];
                    uint* cur = pBSD + 3 + 2 * ndims;
                    for (int i = 0; i < ndims; i++) {
                        cur[i] = 0;
                    }
                    m_end = m_start + m_stride0 * (pBSD[3] - 1);
                }
                m_higDims = 0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }
        public unsafe struct StridedIteratorComplex64<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            complex* m_start;
            complex* m_curP;
            complex* m_end;
            Func<complex, Tout> m_converter;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorComplex64(complex* start, long* bsd, StorageOrders? order, IDisposable obj, Func<complex, Tout> converter) {
                m_start = start;
                m_curP = (complex*)0;
                m_converter = converter;
                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd)
                {
                    pCur[0] = (long)bsd[0];
                    pCur[1] = (long)bsd[1];
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
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
                        m_end = m_start + m_stride0 * (pCur[3] - 1);
                    }
                }
                m_higDims = 0;
                m_obj = obj;
            }

            public Tout Current => m_converter(*m_curP);

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() => this;

            public bool MoveNext() {
                if (m_curP == (complex*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (long* pBSD = m_bsd)
                    {
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (complex*)0;
                fixed (long* pBSD = m_bsd)
                {
                    long ndims = pBSD[0];
                    long* cur = pBSD + 3 + 2 * ndims;
                    for (int i = 0; i < ndims; i++) {
                        cur[i] = 0;
                    }
                    m_end = m_start + m_stride0 * (pBSD[3] - 1);
                }
                m_higDims = 0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }
       

        public unsafe struct StridedIteratorSingle32<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            float* m_start;
            float* m_curP;
            float* m_end;
            Func<float, Tout> m_converter; 

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorSingle32(float* start, long* bsd, StorageOrders? order, IDisposable obj, Func<float, Tout> converter) {
                m_start = start;
                m_curP = (float*)0;
                m_converter = converter; 
                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = ndims;
                    pCur[1] = (uint)bsd[1];
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start; 
                    } else {
                        // offset from bsd is ignored!
                        for (uint i = 0; i < ndims; i++) {
                            if (order == StorageOrders.RowMajor) {
                                pCur[i + 3] = (uint)bsd[2 + ndims - i];
                                pCur[i + 3 + ndims] = (uint)bsd[2 + ndims * 2 - i];
                            } else {
                                pCur[i + 3] = (uint)bsd[i + 3];
                                pCur[i + 3 + ndims] = (uint)bsd[3 + ndims + i];
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
                        m_end = m_start + m_stride0 * (pCur[3] - 1);
                    }
                    m_higDims = 0;
                }
                m_obj = obj;
            }

            public Tout Current => m_converter(*m_curP);

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() => this;

            public bool MoveNext() {
                if (m_curP == (float*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (uint* pBSD = m_bsd) {
                        if (pBSD[0] < 2) return false; 
                        uint d = 1, ndims = pBSD[0];
                        uint* cur = pBSD + 3 + 2 * ndims;
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (float*)0;
                fixed (uint* pBSD = m_bsd) {
                    uint ndims = pBSD[0];
                    uint* cur = pBSD + 3 + 2 * ndims;
                    for (int i = 0; i < ndims; i++) {
                        cur[i] = 0;
                    }
                    m_end = m_start + m_stride0 * (pBSD[3] - 1);
                }
                m_higDims = 0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }
        public unsafe struct StridedIteratorSingle64<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            float* m_start;
            float* m_curP;
            float* m_end;
            Func<float, Tout> m_converter;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorSingle64(float* start, long* bsd, StorageOrders? order, IDisposable obj, Func<float, Tout> converter) {
                m_start = start;
                m_curP = (float*)0;
                m_converter = converter;
                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd)
                {
                    pCur[0] = (long)bsd[0];
                    pCur[1] = (long)bsd[1];
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
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
                        m_end = m_start + m_stride0 * (pCur[3] - 1);
                    }
                }
                m_higDims = 0;
                m_obj = obj;
            }

            public Tout Current => m_converter(*m_curP);

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() => this;

            public bool MoveNext() {
                if (m_curP == (float*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (long* pBSD = m_bsd)
                    {
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (float*)0;
                fixed (long* pBSD = m_bsd)
                {
                    long ndims = pBSD[0];
                    long* cur = pBSD + 3 + 2 * ndims;
                    for (int i = 0; i < ndims; i++) {
                        cur[i] = 0;
                    }
                    m_end = m_start + m_stride0 * (pBSD[3] - 1);
                }
                m_higDims = 0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }

#endregion HYCALPER AUTO GENERATED CODE

    }
}
