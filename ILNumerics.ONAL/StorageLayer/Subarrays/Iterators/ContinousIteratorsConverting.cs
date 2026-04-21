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
using System.Runtime.CompilerServices;

namespace ILNumerics.Core.StorageLayer {
    internal static partial class Iterators {

        #region HYCALPER LOOPSTART
        /*!HC:TYPELIST:
<hycalper>
<type>
    <source locate="here">
        ContinuousIteratorDouble
    </source>
    <destination>ContinuousIteratorSingle</destination>
    <destination>ContinuousIteratorComplex</destination>
    <destination>ContinuousIteratorfComplex</destination>
    <destination>ContinuousIteratorULong</destination>
    <destination>ContinuousIteratorLong</destination>
    <destination>ContinuousIteratorUInt</destination>
    <destination>ContinuousIteratorInt</destination>
    <destination>ContinuousIteratorUShort</destination>
    <destination>ContinuousIteratorShort</destination>
    <destination>ContinuousIteratorByte</destination>
    <destination>ContinuousIteratorSByte</destination>
    <destination>ContinuousIteratorBool</destination>
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
    <destination>UInt32</destination>
    <destination>int</destination>
    <destination>ushort</destination>
    <destination>short</destination>
    <destination>byte</destination>
    <destination>sbyte</destination>
    <destination>bool</destination>
</type>
</hycalper>
*/
        public unsafe struct ContinuousIteratorDouble<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            double* m_cur;
            double* m_start;
            double* m_end;
            IDisposable m_obj;
            Func<double, Tout> m_converter; 

            public ContinuousIteratorDouble(double* start, double* end, IDisposable obj, Func<double,Tout> converter) {
                m_start = start;
                m_end = end;
                m_cur = (double*)0;
                m_obj = obj;
                m_converter = converter; 
            }

            public Tout Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return m_converter(*m_cur);
                }
            }

            object IEnumerator.Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return Current;
                }
            }
            public void Dispose() {

                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() {
                return this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext() {
                if (m_cur == (double*)0) {
                    m_cur = m_start;
                    return m_end >= m_cur;
                } else if (m_end > m_cur) {
                    m_cur++;
                } else {
                    return false;
                }
                return true;
            }

            public void Reset() {
                m_cur = (double*)0;
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return this;
            }
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       
        public unsafe struct ContinuousIteratorBool<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            bool* m_cur;
            bool* m_start;
            bool* m_end;
            IDisposable m_obj;
            Func<bool, Tout> m_converter; 

            public ContinuousIteratorBool(bool* start, bool* end, IDisposable obj, Func<bool,Tout> converter) {
                m_start = start;
                m_end = end;
                m_cur = (bool*)0;
                m_obj = obj;
                m_converter = converter; 
            }

            public Tout Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return m_converter(*m_cur);
                }
            }

            object IEnumerator.Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return Current;
                }
            }
            public void Dispose() {

                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() {
                return this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext() {
                if (m_cur == (bool*)0) {
                    m_cur = m_start;
                    return m_end >= m_cur;
                } else if (m_end > m_cur) {
                    m_cur++;
                } else {
                    return false;
                }
                return true;
            }

            public void Reset() {
                m_cur = (bool*)0;
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return this;
            }
        }
       
        public unsafe struct ContinuousIteratorSByte<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            sbyte* m_cur;
            sbyte* m_start;
            sbyte* m_end;
            IDisposable m_obj;
            Func<sbyte, Tout> m_converter; 

            public ContinuousIteratorSByte(sbyte* start, sbyte* end, IDisposable obj, Func<sbyte,Tout> converter) {
                m_start = start;
                m_end = end;
                m_cur = (sbyte*)0;
                m_obj = obj;
                m_converter = converter; 
            }

            public Tout Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return m_converter(*m_cur);
                }
            }

            object IEnumerator.Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return Current;
                }
            }
            public void Dispose() {

                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() {
                return this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext() {
                if (m_cur == (sbyte*)0) {
                    m_cur = m_start;
                    return m_end >= m_cur;
                } else if (m_end > m_cur) {
                    m_cur++;
                } else {
                    return false;
                }
                return true;
            }

            public void Reset() {
                m_cur = (sbyte*)0;
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return this;
            }
        }
       
        public unsafe struct ContinuousIteratorByte<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            byte* m_cur;
            byte* m_start;
            byte* m_end;
            IDisposable m_obj;
            Func<byte, Tout> m_converter; 

            public ContinuousIteratorByte(byte* start, byte* end, IDisposable obj, Func<byte,Tout> converter) {
                m_start = start;
                m_end = end;
                m_cur = (byte*)0;
                m_obj = obj;
                m_converter = converter; 
            }

            public Tout Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return m_converter(*m_cur);
                }
            }

            object IEnumerator.Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return Current;
                }
            }
            public void Dispose() {

                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() {
                return this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext() {
                if (m_cur == (byte*)0) {
                    m_cur = m_start;
                    return m_end >= m_cur;
                } else if (m_end > m_cur) {
                    m_cur++;
                } else {
                    return false;
                }
                return true;
            }

            public void Reset() {
                m_cur = (byte*)0;
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return this;
            }
        }
       
        public unsafe struct ContinuousIteratorShort<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            short* m_cur;
            short* m_start;
            short* m_end;
            IDisposable m_obj;
            Func<short, Tout> m_converter; 

            public ContinuousIteratorShort(short* start, short* end, IDisposable obj, Func<short,Tout> converter) {
                m_start = start;
                m_end = end;
                m_cur = (short*)0;
                m_obj = obj;
                m_converter = converter; 
            }

            public Tout Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return m_converter(*m_cur);
                }
            }

            object IEnumerator.Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return Current;
                }
            }
            public void Dispose() {

                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() {
                return this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext() {
                if (m_cur == (short*)0) {
                    m_cur = m_start;
                    return m_end >= m_cur;
                } else if (m_end > m_cur) {
                    m_cur++;
                } else {
                    return false;
                }
                return true;
            }

            public void Reset() {
                m_cur = (short*)0;
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return this;
            }
        }
       
        public unsafe struct ContinuousIteratorUShort<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            ushort* m_cur;
            ushort* m_start;
            ushort* m_end;
            IDisposable m_obj;
            Func<ushort, Tout> m_converter; 

            public ContinuousIteratorUShort(ushort* start, ushort* end, IDisposable obj, Func<ushort,Tout> converter) {
                m_start = start;
                m_end = end;
                m_cur = (ushort*)0;
                m_obj = obj;
                m_converter = converter; 
            }

            public Tout Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return m_converter(*m_cur);
                }
            }

            object IEnumerator.Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return Current;
                }
            }
            public void Dispose() {

                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() {
                return this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext() {
                if (m_cur == (ushort*)0) {
                    m_cur = m_start;
                    return m_end >= m_cur;
                } else if (m_end > m_cur) {
                    m_cur++;
                } else {
                    return false;
                }
                return true;
            }

            public void Reset() {
                m_cur = (ushort*)0;
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return this;
            }
        }
       
        public unsafe struct ContinuousIteratorInt<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            int* m_cur;
            int* m_start;
            int* m_end;
            IDisposable m_obj;
            Func<int, Tout> m_converter; 

            public ContinuousIteratorInt(int* start, int* end, IDisposable obj, Func<int,Tout> converter) {
                m_start = start;
                m_end = end;
                m_cur = (int*)0;
                m_obj = obj;
                m_converter = converter; 
            }

            public Tout Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return m_converter(*m_cur);
                }
            }

            object IEnumerator.Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return Current;
                }
            }
            public void Dispose() {

                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() {
                return this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext() {
                if (m_cur == (int*)0) {
                    m_cur = m_start;
                    return m_end >= m_cur;
                } else if (m_end > m_cur) {
                    m_cur++;
                } else {
                    return false;
                }
                return true;
            }

            public void Reset() {
                m_cur = (int*)0;
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return this;
            }
        }
       
        public unsafe struct ContinuousIteratorUInt<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            UInt32* m_cur;
            UInt32* m_start;
            UInt32* m_end;
            IDisposable m_obj;
            Func<UInt32, Tout> m_converter; 

            public ContinuousIteratorUInt(UInt32* start, UInt32* end, IDisposable obj, Func<UInt32,Tout> converter) {
                m_start = start;
                m_end = end;
                m_cur = (UInt32*)0;
                m_obj = obj;
                m_converter = converter; 
            }

            public Tout Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return m_converter(*m_cur);
                }
            }

            object IEnumerator.Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return Current;
                }
            }
            public void Dispose() {

                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() {
                return this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext() {
                if (m_cur == (UInt32*)0) {
                    m_cur = m_start;
                    return m_end >= m_cur;
                } else if (m_end > m_cur) {
                    m_cur++;
                } else {
                    return false;
                }
                return true;
            }

            public void Reset() {
                m_cur = (UInt32*)0;
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return this;
            }
        }
       
        public unsafe struct ContinuousIteratorLong<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            long* m_cur;
            long* m_start;
            long* m_end;
            IDisposable m_obj;
            Func<long, Tout> m_converter; 

            public ContinuousIteratorLong(long* start, long* end, IDisposable obj, Func<long,Tout> converter) {
                m_start = start;
                m_end = end;
                m_cur = (long*)0;
                m_obj = obj;
                m_converter = converter; 
            }

            public Tout Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return m_converter(*m_cur);
                }
            }

            object IEnumerator.Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return Current;
                }
            }
            public void Dispose() {

                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() {
                return this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext() {
                if (m_cur == (long*)0) {
                    m_cur = m_start;
                    return m_end >= m_cur;
                } else if (m_end > m_cur) {
                    m_cur++;
                } else {
                    return false;
                }
                return true;
            }

            public void Reset() {
                m_cur = (long*)0;
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return this;
            }
        }
       
        public unsafe struct ContinuousIteratorULong<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            ulong* m_cur;
            ulong* m_start;
            ulong* m_end;
            IDisposable m_obj;
            Func<ulong, Tout> m_converter; 

            public ContinuousIteratorULong(ulong* start, ulong* end, IDisposable obj, Func<ulong,Tout> converter) {
                m_start = start;
                m_end = end;
                m_cur = (ulong*)0;
                m_obj = obj;
                m_converter = converter; 
            }

            public Tout Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return m_converter(*m_cur);
                }
            }

            object IEnumerator.Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return Current;
                }
            }
            public void Dispose() {

                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() {
                return this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext() {
                if (m_cur == (ulong*)0) {
                    m_cur = m_start;
                    return m_end >= m_cur;
                } else if (m_end > m_cur) {
                    m_cur++;
                } else {
                    return false;
                }
                return true;
            }

            public void Reset() {
                m_cur = (ulong*)0;
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return this;
            }
        }
       
        public unsafe struct ContinuousIteratorfComplex<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            fcomplex* m_cur;
            fcomplex* m_start;
            fcomplex* m_end;
            IDisposable m_obj;
            Func<fcomplex, Tout> m_converter; 

            public ContinuousIteratorfComplex(fcomplex* start, fcomplex* end, IDisposable obj, Func<fcomplex,Tout> converter) {
                m_start = start;
                m_end = end;
                m_cur = (fcomplex*)0;
                m_obj = obj;
                m_converter = converter; 
            }

            public Tout Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return m_converter(*m_cur);
                }
            }

            object IEnumerator.Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return Current;
                }
            }
            public void Dispose() {

                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() {
                return this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext() {
                if (m_cur == (fcomplex*)0) {
                    m_cur = m_start;
                    return m_end >= m_cur;
                } else if (m_end > m_cur) {
                    m_cur++;
                } else {
                    return false;
                }
                return true;
            }

            public void Reset() {
                m_cur = (fcomplex*)0;
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return this;
            }
        }
       
        public unsafe struct ContinuousIteratorComplex<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            complex* m_cur;
            complex* m_start;
            complex* m_end;
            IDisposable m_obj;
            Func<complex, Tout> m_converter; 

            public ContinuousIteratorComplex(complex* start, complex* end, IDisposable obj, Func<complex,Tout> converter) {
                m_start = start;
                m_end = end;
                m_cur = (complex*)0;
                m_obj = obj;
                m_converter = converter; 
            }

            public Tout Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return m_converter(*m_cur);
                }
            }

            object IEnumerator.Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return Current;
                }
            }
            public void Dispose() {

                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() {
                return this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext() {
                if (m_cur == (complex*)0) {
                    m_cur = m_start;
                    return m_end >= m_cur;
                } else if (m_end > m_cur) {
                    m_cur++;
                } else {
                    return false;
                }
                return true;
            }

            public void Reset() {
                m_cur = (complex*)0;
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return this;
            }
        }
       
        public unsafe struct ContinuousIteratorSingle<Tout> : IEnumerable<Tout>, IEnumerator<Tout> where Tout : struct {

            float* m_cur;
            float* m_start;
            float* m_end;
            IDisposable m_obj;
            Func<float, Tout> m_converter; 

            public ContinuousIteratorSingle(float* start, float* end, IDisposable obj, Func<float,Tout> converter) {
                m_start = start;
                m_end = end;
                m_cur = (float*)0;
                m_obj = obj;
                m_converter = converter; 
            }

            public Tout Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return m_converter(*m_cur);
                }
            }

            object IEnumerator.Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return Current;
                }
            }
            public void Dispose() {

                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;  // prevent multiple disposes!
                }
            }

            public IEnumerator<Tout> GetEnumerator() {
                return this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext() {
                if (m_cur == (float*)0) {
                    m_cur = m_start;
                    return m_end >= m_cur;
                } else if (m_end > m_cur) {
                    m_cur++;
                } else {
                    return false;
                }
                return true;
            }

            public void Reset() {
                m_cur = (float*)0;
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return this;
            }
        }

#endregion HYCALPER AUTO GENERATED CODE

        #region HYCALPER LOOPSTART  [Internal] Continous iterators, returning IIndexIterator
        /*!HC:TYPELIST:
<hycalper>
<type>
    <source locate="here">
        ContinuousIteratorDoubleLong
    </source>
    <destination>ContinuousIteratorSingleLong</destination>
    <destination>ContinuousIteratorComplexLong</destination>
    <destination>ContinuousIteratorFComplexLong</destination>
    <destination>ContinuousIteratorULongLong</destination>
    <destination>ContinuousIteratorLongLong</destination>
    <destination>ContinuousIteratorUIntLong</destination>
    <destination>ContinuousIteratorInt32Long</destination>
    <destination>ContinuousIteratorUShortLong</destination>
    <destination>ContinuousIteratorShortLong</destination>
    <destination>ContinuousIteratorByteLong</destination>
    <destination>ContinuousIteratorSByteLong</destination>
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
    <destination>UInt32</destination>
    <destination>int</destination>
    <destination>ushort</destination>
    <destination>short</destination>
    <destination>byte</destination>
    <destination>sbyte</destination>
</type>
</hycalper>
*/
        internal unsafe struct ContinuousIteratorDoubleLong : IIndexIterator {

            double* m_cur;
            double* m_start;
            double* m_end;
            IDisposable m_obj;
            long m_lastDimIdx;
            long m_min;
            long m_max;
            long? m_stepsize; 

            internal ContinuousIteratorDoubleLong(double* start, double* end, IDisposable obj, long lastDimensionIndex, long minimum, long maximum, long? stepsize) {
                m_start = start;
                m_end = end;
                m_cur = (double*)0;
                m_obj = obj;
                m_lastDimIdx = lastDimensionIndex;
                m_min = minimum;
                m_max = maximum;
                m_stepsize = stepsize; 
            }

            public long Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    var ret = (long)*m_cur;
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

            public long GetLength() => m_end - m_start + 1;

            public long? GetMaximum() => GetLength() > 0 ? (Nullable<long>)m_max : null;

            public long? GetMinimum() => GetLength() > 0 ? (Nullable<long>)m_min : null;

            public long? GetStepSize() => m_stepsize;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext() {
                if (m_cur == (double*)0) {
                    m_cur = m_start;
                    return m_end >= m_cur;
                } else if (m_end > m_cur) {
                    m_cur++;
                } else {
                    return false;
                }
                return true;
            }

            public void Reset() {
                m_cur = (double*)0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }


        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       
        internal unsafe struct ContinuousIteratorSByteLong : IIndexIterator {

            sbyte* m_cur;
            sbyte* m_start;
            sbyte* m_end;
            IDisposable m_obj;
            long m_lastDimIdx;
            long m_min;
            long m_max;
            long? m_stepsize; 

            internal ContinuousIteratorSByteLong(sbyte* start, sbyte* end, IDisposable obj, long lastDimensionIndex, long minimum, long maximum, long? stepsize) {
                m_start = start;
                m_end = end;
                m_cur = (sbyte*)0;
                m_obj = obj;
                m_lastDimIdx = lastDimensionIndex;
                m_min = minimum;
                m_max = maximum;
                m_stepsize = stepsize; 
            }

            public long Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    var ret = (long)*m_cur;
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

            public long GetLength() => m_end - m_start + 1;

            public long? GetMaximum() => GetLength() > 0 ? (Nullable<long>)m_max : null;

            public long? GetMinimum() => GetLength() > 0 ? (Nullable<long>)m_min : null;

            public long? GetStepSize() => m_stepsize;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext() {
                if (m_cur == (sbyte*)0) {
                    m_cur = m_start;
                    return m_end >= m_cur;
                } else if (m_end > m_cur) {
                    m_cur++;
                } else {
                    return false;
                }
                return true;
            }

            public void Reset() {
                m_cur = (sbyte*)0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }


       
        internal unsafe struct ContinuousIteratorByteLong : IIndexIterator {

            byte* m_cur;
            byte* m_start;
            byte* m_end;
            IDisposable m_obj;
            long m_lastDimIdx;
            long m_min;
            long m_max;
            long? m_stepsize; 

            internal ContinuousIteratorByteLong(byte* start, byte* end, IDisposable obj, long lastDimensionIndex, long minimum, long maximum, long? stepsize) {
                m_start = start;
                m_end = end;
                m_cur = (byte*)0;
                m_obj = obj;
                m_lastDimIdx = lastDimensionIndex;
                m_min = minimum;
                m_max = maximum;
                m_stepsize = stepsize; 
            }

            public long Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    var ret = (long)*m_cur;
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

            public long GetLength() => m_end - m_start + 1;

            public long? GetMaximum() => GetLength() > 0 ? (Nullable<long>)m_max : null;

            public long? GetMinimum() => GetLength() > 0 ? (Nullable<long>)m_min : null;

            public long? GetStepSize() => m_stepsize;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext() {
                if (m_cur == (byte*)0) {
                    m_cur = m_start;
                    return m_end >= m_cur;
                } else if (m_end > m_cur) {
                    m_cur++;
                } else {
                    return false;
                }
                return true;
            }

            public void Reset() {
                m_cur = (byte*)0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }


       
        internal unsafe struct ContinuousIteratorShortLong : IIndexIterator {

            short* m_cur;
            short* m_start;
            short* m_end;
            IDisposable m_obj;
            long m_lastDimIdx;
            long m_min;
            long m_max;
            long? m_stepsize; 

            internal ContinuousIteratorShortLong(short* start, short* end, IDisposable obj, long lastDimensionIndex, long minimum, long maximum, long? stepsize) {
                m_start = start;
                m_end = end;
                m_cur = (short*)0;
                m_obj = obj;
                m_lastDimIdx = lastDimensionIndex;
                m_min = minimum;
                m_max = maximum;
                m_stepsize = stepsize; 
            }

            public long Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    var ret = (long)*m_cur;
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

            public long GetLength() => m_end - m_start + 1;

            public long? GetMaximum() => GetLength() > 0 ? (Nullable<long>)m_max : null;

            public long? GetMinimum() => GetLength() > 0 ? (Nullable<long>)m_min : null;

            public long? GetStepSize() => m_stepsize;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext() {
                if (m_cur == (short*)0) {
                    m_cur = m_start;
                    return m_end >= m_cur;
                } else if (m_end > m_cur) {
                    m_cur++;
                } else {
                    return false;
                }
                return true;
            }

            public void Reset() {
                m_cur = (short*)0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }


       
        internal unsafe struct ContinuousIteratorUShortLong : IIndexIterator {

            ushort* m_cur;
            ushort* m_start;
            ushort* m_end;
            IDisposable m_obj;
            long m_lastDimIdx;
            long m_min;
            long m_max;
            long? m_stepsize; 

            internal ContinuousIteratorUShortLong(ushort* start, ushort* end, IDisposable obj, long lastDimensionIndex, long minimum, long maximum, long? stepsize) {
                m_start = start;
                m_end = end;
                m_cur = (ushort*)0;
                m_obj = obj;
                m_lastDimIdx = lastDimensionIndex;
                m_min = minimum;
                m_max = maximum;
                m_stepsize = stepsize; 
            }

            public long Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    var ret = (long)*m_cur;
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

            public long GetLength() => m_end - m_start + 1;

            public long? GetMaximum() => GetLength() > 0 ? (Nullable<long>)m_max : null;

            public long? GetMinimum() => GetLength() > 0 ? (Nullable<long>)m_min : null;

            public long? GetStepSize() => m_stepsize;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext() {
                if (m_cur == (ushort*)0) {
                    m_cur = m_start;
                    return m_end >= m_cur;
                } else if (m_end > m_cur) {
                    m_cur++;
                } else {
                    return false;
                }
                return true;
            }

            public void Reset() {
                m_cur = (ushort*)0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }


       
        internal unsafe struct ContinuousIteratorInt32Long : IIndexIterator {

            int* m_cur;
            int* m_start;
            int* m_end;
            IDisposable m_obj;
            long m_lastDimIdx;
            long m_min;
            long m_max;
            long? m_stepsize; 

            internal ContinuousIteratorInt32Long(int* start, int* end, IDisposable obj, long lastDimensionIndex, long minimum, long maximum, long? stepsize) {
                m_start = start;
                m_end = end;
                m_cur = (int*)0;
                m_obj = obj;
                m_lastDimIdx = lastDimensionIndex;
                m_min = minimum;
                m_max = maximum;
                m_stepsize = stepsize; 
            }

            public long Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    var ret = (long)*m_cur;
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

            public long GetLength() => m_end - m_start + 1;

            public long? GetMaximum() => GetLength() > 0 ? (Nullable<long>)m_max : null;

            public long? GetMinimum() => GetLength() > 0 ? (Nullable<long>)m_min : null;

            public long? GetStepSize() => m_stepsize;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext() {
                if (m_cur == (int*)0) {
                    m_cur = m_start;
                    return m_end >= m_cur;
                } else if (m_end > m_cur) {
                    m_cur++;
                } else {
                    return false;
                }
                return true;
            }

            public void Reset() {
                m_cur = (int*)0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }


       
        internal unsafe struct ContinuousIteratorUIntLong : IIndexIterator {

            UInt32* m_cur;
            UInt32* m_start;
            UInt32* m_end;
            IDisposable m_obj;
            long m_lastDimIdx;
            long m_min;
            long m_max;
            long? m_stepsize; 

            internal ContinuousIteratorUIntLong(UInt32* start, UInt32* end, IDisposable obj, long lastDimensionIndex, long minimum, long maximum, long? stepsize) {
                m_start = start;
                m_end = end;
                m_cur = (UInt32*)0;
                m_obj = obj;
                m_lastDimIdx = lastDimensionIndex;
                m_min = minimum;
                m_max = maximum;
                m_stepsize = stepsize; 
            }

            public long Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    var ret = (long)*m_cur;
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

            public long GetLength() => m_end - m_start + 1;

            public long? GetMaximum() => GetLength() > 0 ? (Nullable<long>)m_max : null;

            public long? GetMinimum() => GetLength() > 0 ? (Nullable<long>)m_min : null;

            public long? GetStepSize() => m_stepsize;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext() {
                if (m_cur == (UInt32*)0) {
                    m_cur = m_start;
                    return m_end >= m_cur;
                } else if (m_end > m_cur) {
                    m_cur++;
                } else {
                    return false;
                }
                return true;
            }

            public void Reset() {
                m_cur = (UInt32*)0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }


       
        internal unsafe struct ContinuousIteratorLongLong : IIndexIterator {

            long* m_cur;
            long* m_start;
            long* m_end;
            IDisposable m_obj;
            long m_lastDimIdx;
            long m_min;
            long m_max;
            long? m_stepsize; 

            internal ContinuousIteratorLongLong(long* start, long* end, IDisposable obj, long lastDimensionIndex, long minimum, long maximum, long? stepsize) {
                m_start = start;
                m_end = end;
                m_cur = (long*)0;
                m_obj = obj;
                m_lastDimIdx = lastDimensionIndex;
                m_min = minimum;
                m_max = maximum;
                m_stepsize = stepsize; 
            }

            public long Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    var ret = (long)*m_cur;
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

            public long GetLength() => m_end - m_start + 1;

            public long? GetMaximum() => GetLength() > 0 ? (Nullable<long>)m_max : null;

            public long? GetMinimum() => GetLength() > 0 ? (Nullable<long>)m_min : null;

            public long? GetStepSize() => m_stepsize;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext() {
                if (m_cur == (long*)0) {
                    m_cur = m_start;
                    return m_end >= m_cur;
                } else if (m_end > m_cur) {
                    m_cur++;
                } else {
                    return false;
                }
                return true;
            }

            public void Reset() {
                m_cur = (long*)0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }


       
        internal unsafe struct ContinuousIteratorULongLong : IIndexIterator {

            ulong* m_cur;
            ulong* m_start;
            ulong* m_end;
            IDisposable m_obj;
            long m_lastDimIdx;
            long m_min;
            long m_max;
            long? m_stepsize; 

            internal ContinuousIteratorULongLong(ulong* start, ulong* end, IDisposable obj, long lastDimensionIndex, long minimum, long maximum, long? stepsize) {
                m_start = start;
                m_end = end;
                m_cur = (ulong*)0;
                m_obj = obj;
                m_lastDimIdx = lastDimensionIndex;
                m_min = minimum;
                m_max = maximum;
                m_stepsize = stepsize; 
            }

            public long Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    var ret = (long)*m_cur;
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

            public long GetLength() => m_end - m_start + 1;

            public long? GetMaximum() => GetLength() > 0 ? (Nullable<long>)m_max : null;

            public long? GetMinimum() => GetLength() > 0 ? (Nullable<long>)m_min : null;

            public long? GetStepSize() => m_stepsize;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext() {
                if (m_cur == (ulong*)0) {
                    m_cur = m_start;
                    return m_end >= m_cur;
                } else if (m_end > m_cur) {
                    m_cur++;
                } else {
                    return false;
                }
                return true;
            }

            public void Reset() {
                m_cur = (ulong*)0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }


       
        internal unsafe struct ContinuousIteratorFComplexLong : IIndexIterator {

            fcomplex* m_cur;
            fcomplex* m_start;
            fcomplex* m_end;
            IDisposable m_obj;
            long m_lastDimIdx;
            long m_min;
            long m_max;
            long? m_stepsize; 

            internal ContinuousIteratorFComplexLong(fcomplex* start, fcomplex* end, IDisposable obj, long lastDimensionIndex, long minimum, long maximum, long? stepsize) {
                m_start = start;
                m_end = end;
                m_cur = (fcomplex*)0;
                m_obj = obj;
                m_lastDimIdx = lastDimensionIndex;
                m_min = minimum;
                m_max = maximum;
                m_stepsize = stepsize; 
            }

            public long Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    var ret = (long)*m_cur;
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

            public long GetLength() => m_end - m_start + 1;

            public long? GetMaximum() => GetLength() > 0 ? (Nullable<long>)m_max : null;

            public long? GetMinimum() => GetLength() > 0 ? (Nullable<long>)m_min : null;

            public long? GetStepSize() => m_stepsize;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext() {
                if (m_cur == (fcomplex*)0) {
                    m_cur = m_start;
                    return m_end >= m_cur;
                } else if (m_end > m_cur) {
                    m_cur++;
                } else {
                    return false;
                }
                return true;
            }

            public void Reset() {
                m_cur = (fcomplex*)0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }


       
        internal unsafe struct ContinuousIteratorComplexLong : IIndexIterator {

            complex* m_cur;
            complex* m_start;
            complex* m_end;
            IDisposable m_obj;
            long m_lastDimIdx;
            long m_min;
            long m_max;
            long? m_stepsize; 

            internal ContinuousIteratorComplexLong(complex* start, complex* end, IDisposable obj, long lastDimensionIndex, long minimum, long maximum, long? stepsize) {
                m_start = start;
                m_end = end;
                m_cur = (complex*)0;
                m_obj = obj;
                m_lastDimIdx = lastDimensionIndex;
                m_min = minimum;
                m_max = maximum;
                m_stepsize = stepsize; 
            }

            public long Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    var ret = (long)*m_cur;
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

            public long GetLength() => m_end - m_start + 1;

            public long? GetMaximum() => GetLength() > 0 ? (Nullable<long>)m_max : null;

            public long? GetMinimum() => GetLength() > 0 ? (Nullable<long>)m_min : null;

            public long? GetStepSize() => m_stepsize;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext() {
                if (m_cur == (complex*)0) {
                    m_cur = m_start;
                    return m_end >= m_cur;
                } else if (m_end > m_cur) {
                    m_cur++;
                } else {
                    return false;
                }
                return true;
            }

            public void Reset() {
                m_cur = (complex*)0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }


       
        internal unsafe struct ContinuousIteratorSingleLong : IIndexIterator {

            float* m_cur;
            float* m_start;
            float* m_end;
            IDisposable m_obj;
            long m_lastDimIdx;
            long m_min;
            long m_max;
            long? m_stepsize; 

            internal ContinuousIteratorSingleLong(float* start, float* end, IDisposable obj, long lastDimensionIndex, long minimum, long maximum, long? stepsize) {
                m_start = start;
                m_end = end;
                m_cur = (float*)0;
                m_obj = obj;
                m_lastDimIdx = lastDimensionIndex;
                m_min = minimum;
                m_max = maximum;
                m_stepsize = stepsize; 
            }

            public long Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    var ret = (long)*m_cur;
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

            public long GetLength() => m_end - m_start + 1;

            public long? GetMaximum() => GetLength() > 0 ? (Nullable<long>)m_max : null;

            public long? GetMinimum() => GetLength() > 0 ? (Nullable<long>)m_min : null;

            public long? GetStepSize() => m_stepsize;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext() {
                if (m_cur == (float*)0) {
                    m_cur = m_start;
                    return m_end >= m_cur;
                } else if (m_end > m_cur) {
                    m_cur++;
                } else {
                    return false;
                }
                return true;
            }

            public void Reset() {
                m_cur = (float*)0;
            }

            IEnumerator IEnumerable.GetEnumerator() => this;
        }



#endregion HYCALPER AUTO GENERATED CODE

    }
}
