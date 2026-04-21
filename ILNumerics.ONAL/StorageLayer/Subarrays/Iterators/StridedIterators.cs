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
</type>
</hycalper>
*/

        public unsafe struct StridedIteratorDouble32 : IEnumerable<double>, IEnumerator<double> {

            double* m_start;
            double* m_curP;
            double* m_end;

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorDouble32(double* start, long* bsd, StorageOrders? order, IDisposable obj) {
                m_start = start;
                m_curP = (double*)0;
                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = (uint)bsd[0];
                    pCur[1] = (uint)bsd[1];
                    m_higDims = 0;
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
                    } else {
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
                }
                m_obj = obj;
            }

            public double Current {
                get {
                    return *m_curP;
                }
            }
            object IEnumerator.Current {
                get {
                    return *m_curP;
                }
            }

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<double> GetEnumerator() {
                return this;
            }

            public bool MoveNext() {
                if (m_curP == (double*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (uint* pBSD = m_bsd)
                    {
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
                fixed (uint* pBSD = m_bsd)
                {
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
        public unsafe struct StridedIteratorDouble64 : IEnumerable<double>, IEnumerator<double> {

            double* m_start;
            double* m_curP;
            double* m_end;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorDouble64(double* start, long* bsd, StorageOrders? order, IDisposable obj) {
                m_start = start;
                m_curP = (double*)0;
                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd) {
                    pCur[0] = (long)bsd[0];
                    pCur[1] = (long)bsd[1];
                    m_higDims = 0;
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
                m_obj = obj;
            }

            public double Current {
                get {
                    return *m_curP;
                }
            }
            object IEnumerator.Current {
                get {
                    return *m_curP;
                }
            }

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<double> GetEnumerator() {
                return this;
            }

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
       

        public unsafe struct StridedIteratorSByte32 : IEnumerable<sbyte>, IEnumerator<sbyte> {

            sbyte* m_start;
            sbyte* m_curP;
            sbyte* m_end;

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorSByte32(sbyte* start, long* bsd, StorageOrders? order, IDisposable obj) {
                m_start = start;
                m_curP = (sbyte*)0;
                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = (uint)bsd[0];
                    pCur[1] = (uint)bsd[1];
                    m_higDims = 0;
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
                    } else {
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
                }
                m_obj = obj;
            }

            public sbyte Current {
                get {
                    return *m_curP;
                }
            }
            object IEnumerator.Current {
                get {
                    return *m_curP;
                }
            }

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<sbyte> GetEnumerator() {
                return this;
            }

            public bool MoveNext() {
                if (m_curP == (sbyte*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (uint* pBSD = m_bsd)
                    {
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
                fixed (uint* pBSD = m_bsd)
                {
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
        public unsafe struct StridedIteratorSByte64 : IEnumerable<sbyte>, IEnumerator<sbyte> {

            sbyte* m_start;
            sbyte* m_curP;
            sbyte* m_end;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorSByte64(sbyte* start, long* bsd, StorageOrders? order, IDisposable obj) {
                m_start = start;
                m_curP = (sbyte*)0;
                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd) {
                    pCur[0] = (long)bsd[0];
                    pCur[1] = (long)bsd[1];
                    m_higDims = 0;
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
                m_obj = obj;
            }

            public sbyte Current {
                get {
                    return *m_curP;
                }
            }
            object IEnumerator.Current {
                get {
                    return *m_curP;
                }
            }

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<sbyte> GetEnumerator() {
                return this;
            }

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
       

        public unsafe struct StridedIteratorByte32 : IEnumerable<byte>, IEnumerator<byte> {

            byte* m_start;
            byte* m_curP;
            byte* m_end;

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorByte32(byte* start, long* bsd, StorageOrders? order, IDisposable obj) {
                m_start = start;
                m_curP = (byte*)0;
                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = (uint)bsd[0];
                    pCur[1] = (uint)bsd[1];
                    m_higDims = 0;
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
                    } else {
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
                }
                m_obj = obj;
            }

            public byte Current {
                get {
                    return *m_curP;
                }
            }
            object IEnumerator.Current {
                get {
                    return *m_curP;
                }
            }

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<byte> GetEnumerator() {
                return this;
            }

            public bool MoveNext() {
                if (m_curP == (byte*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (uint* pBSD = m_bsd)
                    {
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
                fixed (uint* pBSD = m_bsd)
                {
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
        public unsafe struct StridedIteratorByte64 : IEnumerable<byte>, IEnumerator<byte> {

            byte* m_start;
            byte* m_curP;
            byte* m_end;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorByte64(byte* start, long* bsd, StorageOrders? order, IDisposable obj) {
                m_start = start;
                m_curP = (byte*)0;
                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd) {
                    pCur[0] = (long)bsd[0];
                    pCur[1] = (long)bsd[1];
                    m_higDims = 0;
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
                m_obj = obj;
            }

            public byte Current {
                get {
                    return *m_curP;
                }
            }
            object IEnumerator.Current {
                get {
                    return *m_curP;
                }
            }

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<byte> GetEnumerator() {
                return this;
            }

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
       

        public unsafe struct StridedIteratorShort32 : IEnumerable<short>, IEnumerator<short> {

            short* m_start;
            short* m_curP;
            short* m_end;

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorShort32(short* start, long* bsd, StorageOrders? order, IDisposable obj) {
                m_start = start;
                m_curP = (short*)0;
                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = (uint)bsd[0];
                    pCur[1] = (uint)bsd[1];
                    m_higDims = 0;
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
                    } else {
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
                }
                m_obj = obj;
            }

            public short Current {
                get {
                    return *m_curP;
                }
            }
            object IEnumerator.Current {
                get {
                    return *m_curP;
                }
            }

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<short> GetEnumerator() {
                return this;
            }

            public bool MoveNext() {
                if (m_curP == (short*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (uint* pBSD = m_bsd)
                    {
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
                fixed (uint* pBSD = m_bsd)
                {
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
        public unsafe struct StridedIteratorShort64 : IEnumerable<short>, IEnumerator<short> {

            short* m_start;
            short* m_curP;
            short* m_end;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorShort64(short* start, long* bsd, StorageOrders? order, IDisposable obj) {
                m_start = start;
                m_curP = (short*)0;
                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd) {
                    pCur[0] = (long)bsd[0];
                    pCur[1] = (long)bsd[1];
                    m_higDims = 0;
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
                m_obj = obj;
            }

            public short Current {
                get {
                    return *m_curP;
                }
            }
            object IEnumerator.Current {
                get {
                    return *m_curP;
                }
            }

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<short> GetEnumerator() {
                return this;
            }

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
       

        public unsafe struct StridedIteratorUShort32 : IEnumerable<ushort>, IEnumerator<ushort> {

            ushort* m_start;
            ushort* m_curP;
            ushort* m_end;

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorUShort32(ushort* start, long* bsd, StorageOrders? order, IDisposable obj) {
                m_start = start;
                m_curP = (ushort*)0;
                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = (uint)bsd[0];
                    pCur[1] = (uint)bsd[1];
                    m_higDims = 0;
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
                    } else {
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
                }
                m_obj = obj;
            }

            public ushort Current {
                get {
                    return *m_curP;
                }
            }
            object IEnumerator.Current {
                get {
                    return *m_curP;
                }
            }

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<ushort> GetEnumerator() {
                return this;
            }

            public bool MoveNext() {
                if (m_curP == (ushort*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (uint* pBSD = m_bsd)
                    {
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
                fixed (uint* pBSD = m_bsd)
                {
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
        public unsafe struct StridedIteratorUShort64 : IEnumerable<ushort>, IEnumerator<ushort> {

            ushort* m_start;
            ushort* m_curP;
            ushort* m_end;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorUShort64(ushort* start, long* bsd, StorageOrders? order, IDisposable obj) {
                m_start = start;
                m_curP = (ushort*)0;
                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd) {
                    pCur[0] = (long)bsd[0];
                    pCur[1] = (long)bsd[1];
                    m_higDims = 0;
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
                m_obj = obj;
            }

            public ushort Current {
                get {
                    return *m_curP;
                }
            }
            object IEnumerator.Current {
                get {
                    return *m_curP;
                }
            }

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<ushort> GetEnumerator() {
                return this;
            }

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
       

        public unsafe struct StridedIteratorInt3232 : IEnumerable<int>, IEnumerator<int> {

            int* m_start;
            int* m_curP;
            int* m_end;

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorInt3232(int* start, long* bsd, StorageOrders? order, IDisposable obj) {
                m_start = start;
                m_curP = (int*)0;
                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = (uint)bsd[0];
                    pCur[1] = (uint)bsd[1];
                    m_higDims = 0;
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
                    } else {
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
                }
                m_obj = obj;
            }

            public int Current {
                get {
                    return *m_curP;
                }
            }
            object IEnumerator.Current {
                get {
                    return *m_curP;
                }
            }

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<int> GetEnumerator() {
                return this;
            }

            public bool MoveNext() {
                if (m_curP == (int*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (uint* pBSD = m_bsd)
                    {
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
                fixed (uint* pBSD = m_bsd)
                {
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
        public unsafe struct StridedIteratorInt3264 : IEnumerable<int>, IEnumerator<int> {

            int* m_start;
            int* m_curP;
            int* m_end;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorInt3264(int* start, long* bsd, StorageOrders? order, IDisposable obj) {
                m_start = start;
                m_curP = (int*)0;
                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd) {
                    pCur[0] = (long)bsd[0];
                    pCur[1] = (long)bsd[1];
                    m_higDims = 0;
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
                m_obj = obj;
            }

            public int Current {
                get {
                    return *m_curP;
                }
            }
            object IEnumerator.Current {
                get {
                    return *m_curP;
                }
            }

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<int> GetEnumerator() {
                return this;
            }

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
       

        public unsafe struct StridedIteratorUInt32 : IEnumerable<uint>, IEnumerator<uint> {

            uint* m_start;
            uint* m_curP;
            uint* m_end;

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorUInt32(uint* start, long* bsd, StorageOrders? order, IDisposable obj) {
                m_start = start;
                m_curP = (uint*)0;
                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = (uint)bsd[0];
                    pCur[1] = (uint)bsd[1];
                    m_higDims = 0;
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
                    } else {
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
                }
                m_obj = obj;
            }

            public uint Current {
                get {
                    return *m_curP;
                }
            }
            object IEnumerator.Current {
                get {
                    return *m_curP;
                }
            }

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<uint> GetEnumerator() {
                return this;
            }

            public bool MoveNext() {
                if (m_curP == (uint*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (uint* pBSD = m_bsd)
                    {
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
                fixed (uint* pBSD = m_bsd)
                {
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
        public unsafe struct StridedIteratorUInt64 : IEnumerable<uint>, IEnumerator<uint> {

            uint* m_start;
            uint* m_curP;
            uint* m_end;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorUInt64(uint* start, long* bsd, StorageOrders? order, IDisposable obj) {
                m_start = start;
                m_curP = (uint*)0;
                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd) {
                    pCur[0] = (long)bsd[0];
                    pCur[1] = (long)bsd[1];
                    m_higDims = 0;
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
                m_obj = obj;
            }

            public uint Current {
                get {
                    return *m_curP;
                }
            }
            object IEnumerator.Current {
                get {
                    return *m_curP;
                }
            }

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<uint> GetEnumerator() {
                return this;
            }

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
       

        public unsafe struct StridedIteratorLong32 : IEnumerable<long>, IEnumerator<long> {

            long* m_start;
            long* m_curP;
            long* m_end;

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorLong32(long* start, long* bsd, StorageOrders? order, IDisposable obj) {
                m_start = start;
                m_curP = (long*)0;
                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = (uint)bsd[0];
                    pCur[1] = (uint)bsd[1];
                    m_higDims = 0;
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
                    } else {
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
                }
                m_obj = obj;
            }

            public long Current {
                get {
                    return *m_curP;
                }
            }
            object IEnumerator.Current {
                get {
                    return *m_curP;
                }
            }

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<long> GetEnumerator() {
                return this;
            }

            public bool MoveNext() {
                if (m_curP == (long*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (uint* pBSD = m_bsd)
                    {
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
                fixed (uint* pBSD = m_bsd)
                {
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
        public unsafe struct StridedIteratorLong64 : IEnumerable<long>, IEnumerator<long> {

            long* m_start;
            long* m_curP;
            long* m_end;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorLong64(long* start, long* bsd, StorageOrders? order, IDisposable obj) {
                m_start = start;
                m_curP = (long*)0;
                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd) {
                    pCur[0] = (long)bsd[0];
                    pCur[1] = (long)bsd[1];
                    m_higDims = 0;
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
                m_obj = obj;
            }

            public long Current {
                get {
                    return *m_curP;
                }
            }
            object IEnumerator.Current {
                get {
                    return *m_curP;
                }
            }

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<long> GetEnumerator() {
                return this;
            }

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
       

        public unsafe struct StridedIteratorULong32 : IEnumerable<ulong>, IEnumerator<ulong> {

            ulong* m_start;
            ulong* m_curP;
            ulong* m_end;

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorULong32(ulong* start, long* bsd, StorageOrders? order, IDisposable obj) {
                m_start = start;
                m_curP = (ulong*)0;
                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = (uint)bsd[0];
                    pCur[1] = (uint)bsd[1];
                    m_higDims = 0;
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
                    } else {
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
                }
                m_obj = obj;
            }

            public ulong Current {
                get {
                    return *m_curP;
                }
            }
            object IEnumerator.Current {
                get {
                    return *m_curP;
                }
            }

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<ulong> GetEnumerator() {
                return this;
            }

            public bool MoveNext() {
                if (m_curP == (ulong*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (uint* pBSD = m_bsd)
                    {
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
                fixed (uint* pBSD = m_bsd)
                {
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
        public unsafe struct StridedIteratorULong64 : IEnumerable<ulong>, IEnumerator<ulong> {

            ulong* m_start;
            ulong* m_curP;
            ulong* m_end;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorULong64(ulong* start, long* bsd, StorageOrders? order, IDisposable obj) {
                m_start = start;
                m_curP = (ulong*)0;
                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd) {
                    pCur[0] = (long)bsd[0];
                    pCur[1] = (long)bsd[1];
                    m_higDims = 0;
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
                m_obj = obj;
            }

            public ulong Current {
                get {
                    return *m_curP;
                }
            }
            object IEnumerator.Current {
                get {
                    return *m_curP;
                }
            }

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<ulong> GetEnumerator() {
                return this;
            }

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
       

        public unsafe struct StridedIteratorFComplex32 : IEnumerable<fcomplex>, IEnumerator<fcomplex> {

            fcomplex* m_start;
            fcomplex* m_curP;
            fcomplex* m_end;

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorFComplex32(fcomplex* start, long* bsd, StorageOrders? order, IDisposable obj) {
                m_start = start;
                m_curP = (fcomplex*)0;
                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = (uint)bsd[0];
                    pCur[1] = (uint)bsd[1];
                    m_higDims = 0;
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
                    } else {
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
                }
                m_obj = obj;
            }

            public fcomplex Current {
                get {
                    return *m_curP;
                }
            }
            object IEnumerator.Current {
                get {
                    return *m_curP;
                }
            }

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<fcomplex> GetEnumerator() {
                return this;
            }

            public bool MoveNext() {
                if (m_curP == (fcomplex*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (uint* pBSD = m_bsd)
                    {
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
                fixed (uint* pBSD = m_bsd)
                {
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
        public unsafe struct StridedIteratorFComplex64 : IEnumerable<fcomplex>, IEnumerator<fcomplex> {

            fcomplex* m_start;
            fcomplex* m_curP;
            fcomplex* m_end;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorFComplex64(fcomplex* start, long* bsd, StorageOrders? order, IDisposable obj) {
                m_start = start;
                m_curP = (fcomplex*)0;
                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd) {
                    pCur[0] = (long)bsd[0];
                    pCur[1] = (long)bsd[1];
                    m_higDims = 0;
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
                m_obj = obj;
            }

            public fcomplex Current {
                get {
                    return *m_curP;
                }
            }
            object IEnumerator.Current {
                get {
                    return *m_curP;
                }
            }

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<fcomplex> GetEnumerator() {
                return this;
            }

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
       

        public unsafe struct StridedIteratorComplex32 : IEnumerable<complex>, IEnumerator<complex> {

            complex* m_start;
            complex* m_curP;
            complex* m_end;

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorComplex32(complex* start, long* bsd, StorageOrders? order, IDisposable obj) {
                m_start = start;
                m_curP = (complex*)0;
                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = (uint)bsd[0];
                    pCur[1] = (uint)bsd[1];
                    m_higDims = 0;
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
                    } else {
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
                }
                m_obj = obj;
            }

            public complex Current {
                get {
                    return *m_curP;
                }
            }
            object IEnumerator.Current {
                get {
                    return *m_curP;
                }
            }

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<complex> GetEnumerator() {
                return this;
            }

            public bool MoveNext() {
                if (m_curP == (complex*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (uint* pBSD = m_bsd)
                    {
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
                fixed (uint* pBSD = m_bsd)
                {
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
        public unsafe struct StridedIteratorComplex64 : IEnumerable<complex>, IEnumerator<complex> {

            complex* m_start;
            complex* m_curP;
            complex* m_end;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorComplex64(complex* start, long* bsd, StorageOrders? order, IDisposable obj) {
                m_start = start;
                m_curP = (complex*)0;
                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd) {
                    pCur[0] = (long)bsd[0];
                    pCur[1] = (long)bsd[1];
                    m_higDims = 0;
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
                m_obj = obj;
            }

            public complex Current {
                get {
                    return *m_curP;
                }
            }
            object IEnumerator.Current {
                get {
                    return *m_curP;
                }
            }

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<complex> GetEnumerator() {
                return this;
            }

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
       

        public unsafe struct StridedIteratorSingle32 : IEnumerable<float>, IEnumerator<float> {

            float* m_start;
            float* m_curP;
            float* m_end;

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorSingle32(float* start, long* bsd, StorageOrders? order, IDisposable obj) {
                m_start = start;
                m_curP = (float*)0;
                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = (uint)bsd[0];
                    pCur[1] = (uint)bsd[1];
                    m_higDims = 0;
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
                    } else {
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
                }
                m_obj = obj;
            }

            public float Current {
                get {
                    return *m_curP;
                }
            }
            object IEnumerator.Current {
                get {
                    return *m_curP;
                }
            }

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<float> GetEnumerator() {
                return this;
            }

            public bool MoveNext() {
                if (m_curP == (float*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
                } else {
                    // increase higher dims
                    fixed (uint* pBSD = m_bsd)
                    {
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
                fixed (uint* pBSD = m_bsd)
                {
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
        public unsafe struct StridedIteratorSingle64 : IEnumerable<float>, IEnumerator<float> {

            float* m_start;
            float* m_curP;
            float* m_end;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorSingle64(float* start, long* bsd, StorageOrders? order, IDisposable obj) {
                m_start = start;
                m_curP = (float*)0;
                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd) {
                    pCur[0] = (long)bsd[0];
                    pCur[1] = (long)bsd[1];
                    m_higDims = 0;
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
                m_obj = obj;
            }

            public float Current {
                get {
                    return *m_curP;
                }
            }
            object IEnumerator.Current {
                get {
                    return *m_curP;
                }
            }

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<float> GetEnumerator() {
                return this;
            }

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

        #region strided 32 / 64 - bool

        public unsafe struct StridedIteratorBool32 : IEnumerable<bool>, IEnumerator<bool> {

            byte* m_start;
            byte* m_curP;
            byte* m_end;

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorBool32(byte* start, long* bsd, StorageOrders? order, IDisposable obj) {
                m_start = start;
                m_curP = (byte*)0;
                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = (uint)bsd[0];
                    pCur[1] = (uint)bsd[1];
                    m_higDims = 0;
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
                    } else {
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
                }
                m_obj = obj;
            }

            public bool Current {
                get {
                    return *m_curP != 0;
                }
            }
            object IEnumerator.Current {
                get {
                    return *m_curP != 0;
                }
            }

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<bool> GetEnumerator() {
                return this;
            }

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
        public unsafe struct StridedIteratorBool64 : IEnumerable<bool>, IEnumerator<bool> {

            byte* m_start;
            byte* m_curP;
            byte* m_end;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;

            public StridedIteratorBool64(byte* start, long* bsd, StorageOrders? order, IDisposable obj) {
                m_start = start;
                m_curP = (byte*)0;
                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd) {
                    pCur[0] = (long)bsd[0];
                    pCur[1] = (long)bsd[1];
                    m_higDims = 0;
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
                m_obj = obj;
            }

            public bool Current {
                get {
                    return *m_curP != 0;
                }
            }
            object IEnumerator.Current {
                get {
                    return *m_curP != 0;
                }
            }

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<bool> GetEnumerator() {
                return this;
            }

            public bool MoveNext() {
                if (m_curP == (byte*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (byte*)0;
                fixed (long* pBSD = m_bsd) {
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


        #endregion
        
        #region strided iterators, generic, on T[] values 

        public unsafe struct StridedIterator32<T> : IEnumerable<T>, IEnumerator<T> {

            uint m_start;
            long m_cur;
            uint m_end;

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;
            T[] m_values; 

            public StridedIterator32(T[] values, uint start, long* bsd, StorageOrders? order, IDisposable obj) {
                m_values = values; 
                m_start = start;
                m_cur = -1;
                m_higDims = 0;
                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = (uint)bsd[0];
                    pCur[1] = (uint)bsd[1];
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
                    } else {
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
                }
                m_obj = obj;
            }

            public T Current {
                get {
                    if (m_cur < 0) throw new InvalidOperationException("This iterator does not currently point to a valid valid. It might be empty or MoveNext() was not called.");
                    return m_values[m_cur];
                }
            }
            object IEnumerator.Current {
                get {
                    if (m_cur < 0) throw new InvalidOperationException("This iterator does not currently point to a valid value. It might be empty or MoveNext() was not called.");
                    return m_values[m_cur];
                }
            }

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<T> GetEnumerator() => this; 

            public bool MoveNext() {
                if (m_cur < 0) {
                    m_cur = m_start;
                    return m_stride0 != 0;
                } else if (m_cur < m_end) {
                    m_cur += m_stride0;
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
                        m_cur = m_start + m_higDims;
                        m_end = (uint)m_cur + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_cur = unchecked((uint)-1);
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
        public unsafe struct StridedIterator64<T> : IEnumerable<T>, IEnumerator<T> {

            long m_start;
            long m_cur;
            long m_end;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;
            T[] m_values; 

            public StridedIterator64(T[] values, long start, long* bsd, StorageOrders? order, IDisposable obj) {
                m_values = values; 
                m_start = start;
                m_cur = -1;
                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd) {
                    pCur[0] = (long)bsd[0];
                    pCur[1] = (long)bsd[1];
                    m_higDims = 0;
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
                m_obj = obj;
            }

            public T Current {
                get {
                    return m_values[m_cur];
                }
            }
            object IEnumerator.Current {
                get {
                    return m_values[m_cur];
                }
            }

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<T> GetEnumerator() {
                return this;
            }

            public bool MoveNext() {
                if (m_cur < 0) {
                    m_cur = m_start;
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
                        m_cur = m_start + m_higDims;
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
                    m_end = m_start + m_stride0 * (pBSD[3] - 1);
                }
                m_higDims = 0;
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return this;
            }
        }

        #endregion

        #region cell iterator - long, strided only
        public unsafe struct StridedCellIterator64 : IEnumerable<BaseArray>, IEnumerator<BaseArray> {

            long m_start;
            long m_cur;
            long m_end;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;
            IStorage[] m_values; 

            public StridedCellIterator64(IStorage[] values, long start, long* bsd, StorageOrders? order, IDisposable obj) {
                m_values = values; 
                m_start = start;
                m_cur = -1;
                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd) {
                    pCur[0] = (long)bsd[0];
                    pCur[1] = (long)bsd[1];
                    m_higDims = 0;
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
                m_obj = obj;
            }

            public BaseArray Current {
                get {
                    return m_values[m_cur]?.GetBaseArrayClone();
                }
            }
            object IEnumerator.Current {
                get {
                    return m_values[m_cur]?.GetBaseArrayClone();
                }
            }

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<BaseArray> GetEnumerator() {
                return this;
            }

            public bool MoveNext() {
                if (m_cur < 0) {
                    m_cur = m_start;
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
                        m_cur = m_start + m_higDims;
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
                    m_end = m_start + m_stride0 * (pBSD[3] - 1);
                }
                m_higDims = 0;
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return this;
            }
        }

        #endregion

        #region strided iterators over long, + IIndexIterator

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
</type>
</hycalper>
*/

        internal unsafe struct StridedIteratorDoubleLong32 : IIndexIterator {

            double* m_start;
            double* m_curP;
            double* m_end;

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;
            uint m_min;
            uint m_max;
            uint? m_stepsize;
            uint m_lastDimIdx;

            internal StridedIteratorDoubleLong32(double* start, long* bsd, StorageOrders? order, IDisposable obj, uint lastDimensionIdex, uint minimum, uint maximum, uint? stepsize) {
                m_start = start;
                m_curP = (double*)0;
                m_max = maximum;
                m_min = minimum;
                m_stepsize = stepsize;
                m_lastDimIdx = lastDimensionIdex; 

                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = (uint)bsd[0];
                    pCur[1] = (uint)bsd[1];
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
                    } else {
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
                }
                m_higDims = 0;
                m_obj = obj;
            }

            public long Current {
                get {
                    var ret = (long)*m_curP;
                    if (ret < 0) {
                        ret += m_lastDimIdx + 1; 
                    }
                    return ret; 
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<long> GetEnumerator() => this;

            public long GetLastDimensionIndex() => m_lastDimIdx;

            public long GetLength() {
                fixed (uint* bsd = m_bsd)
                    return (long)bsd[1]; 
            }

            public long? GetMaximum() => GetLength() > 0 ? (long?)m_max : null; 

            public long? GetMinimum() => GetLength() > 0 ? (long?)m_min : null;

            public long? GetStepSize() => m_stepsize; 

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
        internal unsafe struct StridedIteratorDoubleLong64 : IIndexIterator {

            double* m_start;
            double* m_curP;
            double* m_end;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;
            long m_min;
            long m_max;
            long? m_stepsize;
            long m_lastDimIdx;

            internal StridedIteratorDoubleLong64(double* start, long* bsd, StorageOrders? order, IDisposable obj, long lastDimensionIdex, long minimum, long maximum, long? stepsize) {
                m_start = start;
                m_curP = (double*)0;
                m_max = maximum;
                m_min = minimum;
                m_stepsize = stepsize;
                m_lastDimIdx = lastDimensionIdex;

                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd) {
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

            public long Current {
                get {
                    var ret = (long)*m_curP;
                    if (ret < 0) {
                        ret += m_lastDimIdx + 1;
                    }
                    return ret;
                }
            }

            object IEnumerator.Current => Current;

            long IEnumerator<long>.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<long> GetEnumerator() => this;

            public long GetLastDimensionIndex() => m_lastDimIdx;

            public long GetLength() {
                fixed (long* bsd = m_bsd)
                    return bsd[1];
            }

            public long? GetMaximum() => GetLength() > 0 ? (long?)m_max : null;

            public long? GetMinimum() => GetLength() > 0 ? (long?)m_min : null;

            public long? GetStepSize() => m_stepsize;

            public bool MoveNext() {
                if (m_curP == (double*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (double*)0;
                fixed (long* pBSD = m_bsd) {
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

            IEnumerator<long> IEnumerable<long>.GetEnumerator() => this;
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        internal unsafe struct StridedIteratorSByteLong32 : IIndexIterator {

            sbyte* m_start;
            sbyte* m_curP;
            sbyte* m_end;

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;
            uint m_min;
            uint m_max;
            uint? m_stepsize;
            uint m_lastDimIdx;

            internal StridedIteratorSByteLong32(sbyte* start, long* bsd, StorageOrders? order, IDisposable obj, uint lastDimensionIdex, uint minimum, uint maximum, uint? stepsize) {
                m_start = start;
                m_curP = (sbyte*)0;
                m_max = maximum;
                m_min = minimum;
                m_stepsize = stepsize;
                m_lastDimIdx = lastDimensionIdex; 

                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = (uint)bsd[0];
                    pCur[1] = (uint)bsd[1];
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
                    } else {
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
                }
                m_higDims = 0;
                m_obj = obj;
            }

            public long Current {
                get {
                    var ret = (long)*m_curP;
                    if (ret < 0) {
                        ret += m_lastDimIdx + 1; 
                    }
                    return ret; 
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<long> GetEnumerator() => this;

            public long GetLastDimensionIndex() => m_lastDimIdx;

            public long GetLength() {
                fixed (uint* bsd = m_bsd)
                    return (long)bsd[1]; 
            }

            public long? GetMaximum() => GetLength() > 0 ? (long?)m_max : null; 

            public long? GetMinimum() => GetLength() > 0 ? (long?)m_min : null;

            public long? GetStepSize() => m_stepsize; 

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
        internal unsafe struct StridedIteratorSByteLong64 : IIndexIterator {

            sbyte* m_start;
            sbyte* m_curP;
            sbyte* m_end;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;
            long m_min;
            long m_max;
            long? m_stepsize;
            long m_lastDimIdx;

            internal StridedIteratorSByteLong64(sbyte* start, long* bsd, StorageOrders? order, IDisposable obj, long lastDimensionIdex, long minimum, long maximum, long? stepsize) {
                m_start = start;
                m_curP = (sbyte*)0;
                m_max = maximum;
                m_min = minimum;
                m_stepsize = stepsize;
                m_lastDimIdx = lastDimensionIdex;

                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd) {
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

            public long Current {
                get {
                    var ret = (long)*m_curP;
                    if (ret < 0) {
                        ret += m_lastDimIdx + 1;
                    }
                    return ret;
                }
            }

            object IEnumerator.Current => Current;

            long IEnumerator<long>.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<long> GetEnumerator() => this;

            public long GetLastDimensionIndex() => m_lastDimIdx;

            public long GetLength() {
                fixed (long* bsd = m_bsd)
                    return bsd[1];
            }

            public long? GetMaximum() => GetLength() > 0 ? (long?)m_max : null;

            public long? GetMinimum() => GetLength() > 0 ? (long?)m_min : null;

            public long? GetStepSize() => m_stepsize;

            public bool MoveNext() {
                if (m_curP == (sbyte*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (sbyte*)0;
                fixed (long* pBSD = m_bsd) {
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

            IEnumerator<long> IEnumerable<long>.GetEnumerator() => this;
        }
       

        internal unsafe struct StridedIteratorByteLong32 : IIndexIterator {

            byte* m_start;
            byte* m_curP;
            byte* m_end;

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;
            uint m_min;
            uint m_max;
            uint? m_stepsize;
            uint m_lastDimIdx;

            internal StridedIteratorByteLong32(byte* start, long* bsd, StorageOrders? order, IDisposable obj, uint lastDimensionIdex, uint minimum, uint maximum, uint? stepsize) {
                m_start = start;
                m_curP = (byte*)0;
                m_max = maximum;
                m_min = minimum;
                m_stepsize = stepsize;
                m_lastDimIdx = lastDimensionIdex; 

                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = (uint)bsd[0];
                    pCur[1] = (uint)bsd[1];
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
                    } else {
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
                }
                m_higDims = 0;
                m_obj = obj;
            }

            public long Current {
                get {
                    var ret = (long)*m_curP;
                    if (ret < 0) {
                        ret += m_lastDimIdx + 1; 
                    }
                    return ret; 
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<long> GetEnumerator() => this;

            public long GetLastDimensionIndex() => m_lastDimIdx;

            public long GetLength() {
                fixed (uint* bsd = m_bsd)
                    return (long)bsd[1]; 
            }

            public long? GetMaximum() => GetLength() > 0 ? (long?)m_max : null; 

            public long? GetMinimum() => GetLength() > 0 ? (long?)m_min : null;

            public long? GetStepSize() => m_stepsize; 

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
        internal unsafe struct StridedIteratorByteLong64 : IIndexIterator {

            byte* m_start;
            byte* m_curP;
            byte* m_end;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;
            long m_min;
            long m_max;
            long? m_stepsize;
            long m_lastDimIdx;

            internal StridedIteratorByteLong64(byte* start, long* bsd, StorageOrders? order, IDisposable obj, long lastDimensionIdex, long minimum, long maximum, long? stepsize) {
                m_start = start;
                m_curP = (byte*)0;
                m_max = maximum;
                m_min = minimum;
                m_stepsize = stepsize;
                m_lastDimIdx = lastDimensionIdex;

                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd) {
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

            public long Current {
                get {
                    var ret = (long)*m_curP;
                    if (ret < 0) {
                        ret += m_lastDimIdx + 1;
                    }
                    return ret;
                }
            }

            object IEnumerator.Current => Current;

            long IEnumerator<long>.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<long> GetEnumerator() => this;

            public long GetLastDimensionIndex() => m_lastDimIdx;

            public long GetLength() {
                fixed (long* bsd = m_bsd)
                    return bsd[1];
            }

            public long? GetMaximum() => GetLength() > 0 ? (long?)m_max : null;

            public long? GetMinimum() => GetLength() > 0 ? (long?)m_min : null;

            public long? GetStepSize() => m_stepsize;

            public bool MoveNext() {
                if (m_curP == (byte*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (byte*)0;
                fixed (long* pBSD = m_bsd) {
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

            IEnumerator<long> IEnumerable<long>.GetEnumerator() => this;
        }
       

        internal unsafe struct StridedIteratorShortLong32 : IIndexIterator {

            short* m_start;
            short* m_curP;
            short* m_end;

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;
            uint m_min;
            uint m_max;
            uint? m_stepsize;
            uint m_lastDimIdx;

            internal StridedIteratorShortLong32(short* start, long* bsd, StorageOrders? order, IDisposable obj, uint lastDimensionIdex, uint minimum, uint maximum, uint? stepsize) {
                m_start = start;
                m_curP = (short*)0;
                m_max = maximum;
                m_min = minimum;
                m_stepsize = stepsize;
                m_lastDimIdx = lastDimensionIdex; 

                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = (uint)bsd[0];
                    pCur[1] = (uint)bsd[1];
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
                    } else {
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
                }
                m_higDims = 0;
                m_obj = obj;
            }

            public long Current {
                get {
                    var ret = (long)*m_curP;
                    if (ret < 0) {
                        ret += m_lastDimIdx + 1; 
                    }
                    return ret; 
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<long> GetEnumerator() => this;

            public long GetLastDimensionIndex() => m_lastDimIdx;

            public long GetLength() {
                fixed (uint* bsd = m_bsd)
                    return (long)bsd[1]; 
            }

            public long? GetMaximum() => GetLength() > 0 ? (long?)m_max : null; 

            public long? GetMinimum() => GetLength() > 0 ? (long?)m_min : null;

            public long? GetStepSize() => m_stepsize; 

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
        internal unsafe struct StridedIteratorShortLong64 : IIndexIterator {

            short* m_start;
            short* m_curP;
            short* m_end;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;
            long m_min;
            long m_max;
            long? m_stepsize;
            long m_lastDimIdx;

            internal StridedIteratorShortLong64(short* start, long* bsd, StorageOrders? order, IDisposable obj, long lastDimensionIdex, long minimum, long maximum, long? stepsize) {
                m_start = start;
                m_curP = (short*)0;
                m_max = maximum;
                m_min = minimum;
                m_stepsize = stepsize;
                m_lastDimIdx = lastDimensionIdex;

                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd) {
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

            public long Current {
                get {
                    var ret = (long)*m_curP;
                    if (ret < 0) {
                        ret += m_lastDimIdx + 1;
                    }
                    return ret;
                }
            }

            object IEnumerator.Current => Current;

            long IEnumerator<long>.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<long> GetEnumerator() => this;

            public long GetLastDimensionIndex() => m_lastDimIdx;

            public long GetLength() {
                fixed (long* bsd = m_bsd)
                    return bsd[1];
            }

            public long? GetMaximum() => GetLength() > 0 ? (long?)m_max : null;

            public long? GetMinimum() => GetLength() > 0 ? (long?)m_min : null;

            public long? GetStepSize() => m_stepsize;

            public bool MoveNext() {
                if (m_curP == (short*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (short*)0;
                fixed (long* pBSD = m_bsd) {
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

            IEnumerator<long> IEnumerable<long>.GetEnumerator() => this;
        }
       

        internal unsafe struct StridedIteratorUShortLong32 : IIndexIterator {

            ushort* m_start;
            ushort* m_curP;
            ushort* m_end;

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;
            uint m_min;
            uint m_max;
            uint? m_stepsize;
            uint m_lastDimIdx;

            internal StridedIteratorUShortLong32(ushort* start, long* bsd, StorageOrders? order, IDisposable obj, uint lastDimensionIdex, uint minimum, uint maximum, uint? stepsize) {
                m_start = start;
                m_curP = (ushort*)0;
                m_max = maximum;
                m_min = minimum;
                m_stepsize = stepsize;
                m_lastDimIdx = lastDimensionIdex; 

                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = (uint)bsd[0];
                    pCur[1] = (uint)bsd[1];
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
                    } else {
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
                }
                m_higDims = 0;
                m_obj = obj;
            }

            public long Current {
                get {
                    var ret = (long)*m_curP;
                    if (ret < 0) {
                        ret += m_lastDimIdx + 1; 
                    }
                    return ret; 
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<long> GetEnumerator() => this;

            public long GetLastDimensionIndex() => m_lastDimIdx;

            public long GetLength() {
                fixed (uint* bsd = m_bsd)
                    return (long)bsd[1]; 
            }

            public long? GetMaximum() => GetLength() > 0 ? (long?)m_max : null; 

            public long? GetMinimum() => GetLength() > 0 ? (long?)m_min : null;

            public long? GetStepSize() => m_stepsize; 

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
        internal unsafe struct StridedIteratorUShortLong64 : IIndexIterator {

            ushort* m_start;
            ushort* m_curP;
            ushort* m_end;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;
            long m_min;
            long m_max;
            long? m_stepsize;
            long m_lastDimIdx;

            internal StridedIteratorUShortLong64(ushort* start, long* bsd, StorageOrders? order, IDisposable obj, long lastDimensionIdex, long minimum, long maximum, long? stepsize) {
                m_start = start;
                m_curP = (ushort*)0;
                m_max = maximum;
                m_min = minimum;
                m_stepsize = stepsize;
                m_lastDimIdx = lastDimensionIdex;

                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd) {
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

            public long Current {
                get {
                    var ret = (long)*m_curP;
                    if (ret < 0) {
                        ret += m_lastDimIdx + 1;
                    }
                    return ret;
                }
            }

            object IEnumerator.Current => Current;

            long IEnumerator<long>.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<long> GetEnumerator() => this;

            public long GetLastDimensionIndex() => m_lastDimIdx;

            public long GetLength() {
                fixed (long* bsd = m_bsd)
                    return bsd[1];
            }

            public long? GetMaximum() => GetLength() > 0 ? (long?)m_max : null;

            public long? GetMinimum() => GetLength() > 0 ? (long?)m_min : null;

            public long? GetStepSize() => m_stepsize;

            public bool MoveNext() {
                if (m_curP == (ushort*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (ushort*)0;
                fixed (long* pBSD = m_bsd) {
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

            IEnumerator<long> IEnumerable<long>.GetEnumerator() => this;
        }
       

        internal unsafe struct StridedIteratorInt32Long32 : IIndexIterator {

            int* m_start;
            int* m_curP;
            int* m_end;

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;
            uint m_min;
            uint m_max;
            uint? m_stepsize;
            uint m_lastDimIdx;

            internal StridedIteratorInt32Long32(int* start, long* bsd, StorageOrders? order, IDisposable obj, uint lastDimensionIdex, uint minimum, uint maximum, uint? stepsize) {
                m_start = start;
                m_curP = (int*)0;
                m_max = maximum;
                m_min = minimum;
                m_stepsize = stepsize;
                m_lastDimIdx = lastDimensionIdex; 

                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = (uint)bsd[0];
                    pCur[1] = (uint)bsd[1];
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
                    } else {
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
                }
                m_higDims = 0;
                m_obj = obj;
            }

            public long Current {
                get {
                    var ret = (long)*m_curP;
                    if (ret < 0) {
                        ret += m_lastDimIdx + 1; 
                    }
                    return ret; 
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<long> GetEnumerator() => this;

            public long GetLastDimensionIndex() => m_lastDimIdx;

            public long GetLength() {
                fixed (uint* bsd = m_bsd)
                    return (long)bsd[1]; 
            }

            public long? GetMaximum() => GetLength() > 0 ? (long?)m_max : null; 

            public long? GetMinimum() => GetLength() > 0 ? (long?)m_min : null;

            public long? GetStepSize() => m_stepsize; 

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
        internal unsafe struct StridedIteratorInt32Long64 : IIndexIterator {

            int* m_start;
            int* m_curP;
            int* m_end;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;
            long m_min;
            long m_max;
            long? m_stepsize;
            long m_lastDimIdx;

            internal StridedIteratorInt32Long64(int* start, long* bsd, StorageOrders? order, IDisposable obj, long lastDimensionIdex, long minimum, long maximum, long? stepsize) {
                m_start = start;
                m_curP = (int*)0;
                m_max = maximum;
                m_min = minimum;
                m_stepsize = stepsize;
                m_lastDimIdx = lastDimensionIdex;

                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd) {
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

            public long Current {
                get {
                    var ret = (long)*m_curP;
                    if (ret < 0) {
                        ret += m_lastDimIdx + 1;
                    }
                    return ret;
                }
            }

            object IEnumerator.Current => Current;

            long IEnumerator<long>.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<long> GetEnumerator() => this;

            public long GetLastDimensionIndex() => m_lastDimIdx;

            public long GetLength() {
                fixed (long* bsd = m_bsd)
                    return bsd[1];
            }

            public long? GetMaximum() => GetLength() > 0 ? (long?)m_max : null;

            public long? GetMinimum() => GetLength() > 0 ? (long?)m_min : null;

            public long? GetStepSize() => m_stepsize;

            public bool MoveNext() {
                if (m_curP == (int*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (int*)0;
                fixed (long* pBSD = m_bsd) {
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

            IEnumerator<long> IEnumerable<long>.GetEnumerator() => this;
        }
       

        internal unsafe struct StridedIteratorUIntLong32 : IIndexIterator {

            uint* m_start;
            uint* m_curP;
            uint* m_end;

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;
            uint m_min;
            uint m_max;
            uint? m_stepsize;
            uint m_lastDimIdx;

            internal StridedIteratorUIntLong32(uint* start, long* bsd, StorageOrders? order, IDisposable obj, uint lastDimensionIdex, uint minimum, uint maximum, uint? stepsize) {
                m_start = start;
                m_curP = (uint*)0;
                m_max = maximum;
                m_min = minimum;
                m_stepsize = stepsize;
                m_lastDimIdx = lastDimensionIdex; 

                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = (uint)bsd[0];
                    pCur[1] = (uint)bsd[1];
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
                    } else {
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
                }
                m_higDims = 0;
                m_obj = obj;
            }

            public long Current {
                get {
                    var ret = (long)*m_curP;
                    if (ret < 0) {
                        ret += m_lastDimIdx + 1; 
                    }
                    return ret; 
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<long> GetEnumerator() => this;

            public long GetLastDimensionIndex() => m_lastDimIdx;

            public long GetLength() {
                fixed (uint* bsd = m_bsd)
                    return (long)bsd[1]; 
            }

            public long? GetMaximum() => GetLength() > 0 ? (long?)m_max : null; 

            public long? GetMinimum() => GetLength() > 0 ? (long?)m_min : null;

            public long? GetStepSize() => m_stepsize; 

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
        internal unsafe struct StridedIteratorUIntLong64 : IIndexIterator {

            uint* m_start;
            uint* m_curP;
            uint* m_end;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;
            long m_min;
            long m_max;
            long? m_stepsize;
            long m_lastDimIdx;

            internal StridedIteratorUIntLong64(uint* start, long* bsd, StorageOrders? order, IDisposable obj, long lastDimensionIdex, long minimum, long maximum, long? stepsize) {
                m_start = start;
                m_curP = (uint*)0;
                m_max = maximum;
                m_min = minimum;
                m_stepsize = stepsize;
                m_lastDimIdx = lastDimensionIdex;

                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd) {
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

            public long Current {
                get {
                    var ret = (long)*m_curP;
                    if (ret < 0) {
                        ret += m_lastDimIdx + 1;
                    }
                    return ret;
                }
            }

            object IEnumerator.Current => Current;

            long IEnumerator<long>.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<long> GetEnumerator() => this;

            public long GetLastDimensionIndex() => m_lastDimIdx;

            public long GetLength() {
                fixed (long* bsd = m_bsd)
                    return bsd[1];
            }

            public long? GetMaximum() => GetLength() > 0 ? (long?)m_max : null;

            public long? GetMinimum() => GetLength() > 0 ? (long?)m_min : null;

            public long? GetStepSize() => m_stepsize;

            public bool MoveNext() {
                if (m_curP == (uint*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (uint*)0;
                fixed (long* pBSD = m_bsd) {
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

            IEnumerator<long> IEnumerable<long>.GetEnumerator() => this;
        }
       

        internal unsafe struct StridedIteratorLongLong32 : IIndexIterator {

            long* m_start;
            long* m_curP;
            long* m_end;

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;
            uint m_min;
            uint m_max;
            uint? m_stepsize;
            uint m_lastDimIdx;

            internal StridedIteratorLongLong32(long* start, long* bsd, StorageOrders? order, IDisposable obj, uint lastDimensionIdex, uint minimum, uint maximum, uint? stepsize) {
                m_start = start;
                m_curP = (long*)0;
                m_max = maximum;
                m_min = minimum;
                m_stepsize = stepsize;
                m_lastDimIdx = lastDimensionIdex; 

                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = (uint)bsd[0];
                    pCur[1] = (uint)bsd[1];
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
                    } else {
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
                }
                m_higDims = 0;
                m_obj = obj;
            }

            public long Current {
                get {
                    var ret = (long)*m_curP;
                    if (ret < 0) {
                        ret += m_lastDimIdx + 1; 
                    }
                    return ret; 
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<long> GetEnumerator() => this;

            public long GetLastDimensionIndex() => m_lastDimIdx;

            public long GetLength() {
                fixed (uint* bsd = m_bsd)
                    return (long)bsd[1]; 
            }

            public long? GetMaximum() => GetLength() > 0 ? (long?)m_max : null; 

            public long? GetMinimum() => GetLength() > 0 ? (long?)m_min : null;

            public long? GetStepSize() => m_stepsize; 

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
        internal unsafe struct StridedIteratorLongLong64 : IIndexIterator {

            long* m_start;
            long* m_curP;
            long* m_end;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;
            long m_min;
            long m_max;
            long? m_stepsize;
            long m_lastDimIdx;

            internal StridedIteratorLongLong64(long* start, long* bsd, StorageOrders? order, IDisposable obj, long lastDimensionIdex, long minimum, long maximum, long? stepsize) {
                m_start = start;
                m_curP = (long*)0;
                m_max = maximum;
                m_min = minimum;
                m_stepsize = stepsize;
                m_lastDimIdx = lastDimensionIdex;

                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd) {
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

            public long Current {
                get {
                    var ret = (long)*m_curP;
                    if (ret < 0) {
                        ret += m_lastDimIdx + 1;
                    }
                    return ret;
                }
            }

            object IEnumerator.Current => Current;

            long IEnumerator<long>.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<long> GetEnumerator() => this;

            public long GetLastDimensionIndex() => m_lastDimIdx;

            public long GetLength() {
                fixed (long* bsd = m_bsd)
                    return bsd[1];
            }

            public long? GetMaximum() => GetLength() > 0 ? (long?)m_max : null;

            public long? GetMinimum() => GetLength() > 0 ? (long?)m_min : null;

            public long? GetStepSize() => m_stepsize;

            public bool MoveNext() {
                if (m_curP == (long*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (long*)0;
                fixed (long* pBSD = m_bsd) {
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

            IEnumerator<long> IEnumerable<long>.GetEnumerator() => this;
        }
       

        internal unsafe struct StridedIteratorULongLong32 : IIndexIterator {

            ulong* m_start;
            ulong* m_curP;
            ulong* m_end;

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;
            uint m_min;
            uint m_max;
            uint? m_stepsize;
            uint m_lastDimIdx;

            internal StridedIteratorULongLong32(ulong* start, long* bsd, StorageOrders? order, IDisposable obj, uint lastDimensionIdex, uint minimum, uint maximum, uint? stepsize) {
                m_start = start;
                m_curP = (ulong*)0;
                m_max = maximum;
                m_min = minimum;
                m_stepsize = stepsize;
                m_lastDimIdx = lastDimensionIdex; 

                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = (uint)bsd[0];
                    pCur[1] = (uint)bsd[1];
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
                    } else {
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
                }
                m_higDims = 0;
                m_obj = obj;
            }

            public long Current {
                get {
                    var ret = (long)*m_curP;
                    if (ret < 0) {
                        ret += m_lastDimIdx + 1; 
                    }
                    return ret; 
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<long> GetEnumerator() => this;

            public long GetLastDimensionIndex() => m_lastDimIdx;

            public long GetLength() {
                fixed (uint* bsd = m_bsd)
                    return (long)bsd[1]; 
            }

            public long? GetMaximum() => GetLength() > 0 ? (long?)m_max : null; 

            public long? GetMinimum() => GetLength() > 0 ? (long?)m_min : null;

            public long? GetStepSize() => m_stepsize; 

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
        internal unsafe struct StridedIteratorULongLong64 : IIndexIterator {

            ulong* m_start;
            ulong* m_curP;
            ulong* m_end;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;
            long m_min;
            long m_max;
            long? m_stepsize;
            long m_lastDimIdx;

            internal StridedIteratorULongLong64(ulong* start, long* bsd, StorageOrders? order, IDisposable obj, long lastDimensionIdex, long minimum, long maximum, long? stepsize) {
                m_start = start;
                m_curP = (ulong*)0;
                m_max = maximum;
                m_min = minimum;
                m_stepsize = stepsize;
                m_lastDimIdx = lastDimensionIdex;

                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd) {
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

            public long Current {
                get {
                    var ret = (long)*m_curP;
                    if (ret < 0) {
                        ret += m_lastDimIdx + 1;
                    }
                    return ret;
                }
            }

            object IEnumerator.Current => Current;

            long IEnumerator<long>.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<long> GetEnumerator() => this;

            public long GetLastDimensionIndex() => m_lastDimIdx;

            public long GetLength() {
                fixed (long* bsd = m_bsd)
                    return bsd[1];
            }

            public long? GetMaximum() => GetLength() > 0 ? (long?)m_max : null;

            public long? GetMinimum() => GetLength() > 0 ? (long?)m_min : null;

            public long? GetStepSize() => m_stepsize;

            public bool MoveNext() {
                if (m_curP == (ulong*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (ulong*)0;
                fixed (long* pBSD = m_bsd) {
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

            IEnumerator<long> IEnumerable<long>.GetEnumerator() => this;
        }
       

        internal unsafe struct StridedIteratorFComplexLong32 : IIndexIterator {

            fcomplex* m_start;
            fcomplex* m_curP;
            fcomplex* m_end;

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;
            uint m_min;
            uint m_max;
            uint? m_stepsize;
            uint m_lastDimIdx;

            internal StridedIteratorFComplexLong32(fcomplex* start, long* bsd, StorageOrders? order, IDisposable obj, uint lastDimensionIdex, uint minimum, uint maximum, uint? stepsize) {
                m_start = start;
                m_curP = (fcomplex*)0;
                m_max = maximum;
                m_min = minimum;
                m_stepsize = stepsize;
                m_lastDimIdx = lastDimensionIdex; 

                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = (uint)bsd[0];
                    pCur[1] = (uint)bsd[1];
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
                    } else {
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
                }
                m_higDims = 0;
                m_obj = obj;
            }

            public long Current {
                get {
                    var ret = (long)*m_curP;
                    if (ret < 0) {
                        ret += m_lastDimIdx + 1; 
                    }
                    return ret; 
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<long> GetEnumerator() => this;

            public long GetLastDimensionIndex() => m_lastDimIdx;

            public long GetLength() {
                fixed (uint* bsd = m_bsd)
                    return (long)bsd[1]; 
            }

            public long? GetMaximum() => GetLength() > 0 ? (long?)m_max : null; 

            public long? GetMinimum() => GetLength() > 0 ? (long?)m_min : null;

            public long? GetStepSize() => m_stepsize; 

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
        internal unsafe struct StridedIteratorFComplexLong64 : IIndexIterator {

            fcomplex* m_start;
            fcomplex* m_curP;
            fcomplex* m_end;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;
            long m_min;
            long m_max;
            long? m_stepsize;
            long m_lastDimIdx;

            internal StridedIteratorFComplexLong64(fcomplex* start, long* bsd, StorageOrders? order, IDisposable obj, long lastDimensionIdex, long minimum, long maximum, long? stepsize) {
                m_start = start;
                m_curP = (fcomplex*)0;
                m_max = maximum;
                m_min = minimum;
                m_stepsize = stepsize;
                m_lastDimIdx = lastDimensionIdex;

                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd) {
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

            public long Current {
                get {
                    var ret = (long)*m_curP;
                    if (ret < 0) {
                        ret += m_lastDimIdx + 1;
                    }
                    return ret;
                }
            }

            object IEnumerator.Current => Current;

            long IEnumerator<long>.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<long> GetEnumerator() => this;

            public long GetLastDimensionIndex() => m_lastDimIdx;

            public long GetLength() {
                fixed (long* bsd = m_bsd)
                    return bsd[1];
            }

            public long? GetMaximum() => GetLength() > 0 ? (long?)m_max : null;

            public long? GetMinimum() => GetLength() > 0 ? (long?)m_min : null;

            public long? GetStepSize() => m_stepsize;

            public bool MoveNext() {
                if (m_curP == (fcomplex*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (fcomplex*)0;
                fixed (long* pBSD = m_bsd) {
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

            IEnumerator<long> IEnumerable<long>.GetEnumerator() => this;
        }
       

        internal unsafe struct StridedIteratorComplexLong32 : IIndexIterator {

            complex* m_start;
            complex* m_curP;
            complex* m_end;

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;
            uint m_min;
            uint m_max;
            uint? m_stepsize;
            uint m_lastDimIdx;

            internal StridedIteratorComplexLong32(complex* start, long* bsd, StorageOrders? order, IDisposable obj, uint lastDimensionIdex, uint minimum, uint maximum, uint? stepsize) {
                m_start = start;
                m_curP = (complex*)0;
                m_max = maximum;
                m_min = minimum;
                m_stepsize = stepsize;
                m_lastDimIdx = lastDimensionIdex; 

                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = (uint)bsd[0];
                    pCur[1] = (uint)bsd[1];
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
                    } else {
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
                }
                m_higDims = 0;
                m_obj = obj;
            }

            public long Current {
                get {
                    var ret = (long)*m_curP;
                    if (ret < 0) {
                        ret += m_lastDimIdx + 1; 
                    }
                    return ret; 
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<long> GetEnumerator() => this;

            public long GetLastDimensionIndex() => m_lastDimIdx;

            public long GetLength() {
                fixed (uint* bsd = m_bsd)
                    return (long)bsd[1]; 
            }

            public long? GetMaximum() => GetLength() > 0 ? (long?)m_max : null; 

            public long? GetMinimum() => GetLength() > 0 ? (long?)m_min : null;

            public long? GetStepSize() => m_stepsize; 

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
        internal unsafe struct StridedIteratorComplexLong64 : IIndexIterator {

            complex* m_start;
            complex* m_curP;
            complex* m_end;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;
            long m_min;
            long m_max;
            long? m_stepsize;
            long m_lastDimIdx;

            internal StridedIteratorComplexLong64(complex* start, long* bsd, StorageOrders? order, IDisposable obj, long lastDimensionIdex, long minimum, long maximum, long? stepsize) {
                m_start = start;
                m_curP = (complex*)0;
                m_max = maximum;
                m_min = minimum;
                m_stepsize = stepsize;
                m_lastDimIdx = lastDimensionIdex;

                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd) {
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

            public long Current {
                get {
                    var ret = (long)*m_curP;
                    if (ret < 0) {
                        ret += m_lastDimIdx + 1;
                    }
                    return ret;
                }
            }

            object IEnumerator.Current => Current;

            long IEnumerator<long>.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<long> GetEnumerator() => this;

            public long GetLastDimensionIndex() => m_lastDimIdx;

            public long GetLength() {
                fixed (long* bsd = m_bsd)
                    return bsd[1];
            }

            public long? GetMaximum() => GetLength() > 0 ? (long?)m_max : null;

            public long? GetMinimum() => GetLength() > 0 ? (long?)m_min : null;

            public long? GetStepSize() => m_stepsize;

            public bool MoveNext() {
                if (m_curP == (complex*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (complex*)0;
                fixed (long* pBSD = m_bsd) {
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

            IEnumerator<long> IEnumerable<long>.GetEnumerator() => this;
        }
       

        internal unsafe struct StridedIteratorSingleLong32 : IIndexIterator {

            float* m_start;
            float* m_curP;
            float* m_end;

            fixed uint m_bsd[17 + 7];
            uint m_higDims;
            uint m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;
            uint m_min;
            uint m_max;
            uint? m_stepsize;
            uint m_lastDimIdx;

            internal StridedIteratorSingleLong32(float* start, long* bsd, StorageOrders? order, IDisposable obj, uint lastDimensionIdex, uint minimum, uint maximum, uint? stepsize) {
                m_start = start;
                m_curP = (float*)0;
                m_max = maximum;
                m_min = minimum;
                m_stepsize = stepsize;
                m_lastDimIdx = lastDimensionIdex; 

                var ndims = (uint)bsd[0];

                fixed (uint* pCur = m_bsd) {
                    pCur[0] = (uint)bsd[0];
                    pCur[1] = (uint)bsd[1];
                    // offset from bsd is ignored!
                    if (ndims == 0) {
                        // np.scalar
                        m_stride0 = 1;
                        m_end = m_start;
                    } else {
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
                }
                m_higDims = 0;
                m_obj = obj;
            }

            public long Current {
                get {
                    var ret = (long)*m_curP;
                    if (ret < 0) {
                        ret += m_lastDimIdx + 1; 
                    }
                    return ret; 
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<long> GetEnumerator() => this;

            public long GetLastDimensionIndex() => m_lastDimIdx;

            public long GetLength() {
                fixed (uint* bsd = m_bsd)
                    return (long)bsd[1]; 
            }

            public long? GetMaximum() => GetLength() > 0 ? (long?)m_max : null; 

            public long? GetMinimum() => GetLength() > 0 ? (long?)m_min : null;

            public long? GetStepSize() => m_stepsize; 

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
        internal unsafe struct StridedIteratorSingleLong64 : IIndexIterator {

            float* m_start;
            float* m_curP;
            float* m_end;

            fixed long m_bsd[17 + 7];
            long m_higDims;
            long m_stride0;  // use '0' as mark for 'empty' array!
            IDisposable m_obj;
            long m_min;
            long m_max;
            long? m_stepsize;
            long m_lastDimIdx;

            internal StridedIteratorSingleLong64(float* start, long* bsd, StorageOrders? order, IDisposable obj, long lastDimensionIdex, long minimum, long maximum, long? stepsize) {
                m_start = start;
                m_curP = (float*)0;
                m_max = maximum;
                m_min = minimum;
                m_stepsize = stepsize;
                m_lastDimIdx = lastDimensionIdex;

                var ndims = (uint)bsd[0];

                fixed (long* pCur = m_bsd) {
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

            public long Current {
                get {
                    var ret = (long)*m_curP;
                    if (ret < 0) {
                        ret += m_lastDimIdx + 1;
                    }
                    return ret;
                }
            }

            object IEnumerator.Current => Current;

            long IEnumerator<long>.Current => Current;

            public void Dispose() {
                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<long> GetEnumerator() => this;

            public long GetLastDimensionIndex() => m_lastDimIdx;

            public long GetLength() {
                fixed (long* bsd = m_bsd)
                    return bsd[1];
            }

            public long? GetMaximum() => GetLength() > 0 ? (long?)m_max : null;

            public long? GetMinimum() => GetLength() > 0 ? (long?)m_min : null;

            public long? GetStepSize() => m_stepsize;

            public bool MoveNext() {
                if (m_curP == (float*)0) {
                    m_curP = m_start;
                    return m_stride0 != 0;
                } else if (m_curP < m_end) {
                    m_curP += m_stride0;
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
                        m_curP = m_start + m_higDims;
                        m_end = m_curP + m_stride0 * (pBSD[3] - 1);
                    }
                }
                return true;
            }

            public void Reset() {
                m_curP = (float*)0;
                fixed (long* pBSD = m_bsd) {
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

            IEnumerator<long> IEnumerable<long>.GetEnumerator() => this;
        }

#endregion HYCALPER AUTO GENERATED CODE

        #endregion strided iterators over long, + IIndexIterator

    }
}
