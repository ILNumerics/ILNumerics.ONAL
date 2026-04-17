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
        public unsafe struct ContinuousIteratorDouble : IEnumerable<double>, IEnumerator<double> {

            double* m_cur;
            double* m_start;
            double* m_end;
            IDisposable m_obj;

            public ContinuousIteratorDouble(double* start, double* end, IDisposable obj) {
                m_start = start;
                m_end = end;
                m_cur = (double*)0;
                m_obj = obj;
            }

            public double Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return *m_cur;
                }
            }

            object IEnumerator.Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return *m_cur;
                }
            }
            public void Dispose() {

                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null; 
                }
            }

            public IEnumerator<double> GetEnumerator() {
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
       
        public unsafe struct ContinuousIteratorSByte : IEnumerable<sbyte>, IEnumerator<sbyte> {

            sbyte* m_cur;
            sbyte* m_start;
            sbyte* m_end;
            IDisposable m_obj;

            public ContinuousIteratorSByte(sbyte* start, sbyte* end, IDisposable obj) {
                m_start = start;
                m_end = end;
                m_cur = (sbyte*)0;
                m_obj = obj;
            }

            public sbyte Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return *m_cur;
                }
            }

            object IEnumerator.Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return *m_cur;
                }
            }
            public void Dispose() {

                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null; 
                }
            }

            public IEnumerator<sbyte> GetEnumerator() {
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


       
        public unsafe struct ContinuousIteratorByte : IEnumerable<byte>, IEnumerator<byte> {

            byte* m_cur;
            byte* m_start;
            byte* m_end;
            IDisposable m_obj;

            public ContinuousIteratorByte(byte* start, byte* end, IDisposable obj) {
                m_start = start;
                m_end = end;
                m_cur = (byte*)0;
                m_obj = obj;
            }

            public byte Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return *m_cur;
                }
            }

            object IEnumerator.Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return *m_cur;
                }
            }
            public void Dispose() {

                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null; 
                }
            }

            public IEnumerator<byte> GetEnumerator() {
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


       
        public unsafe struct ContinuousIteratorShort : IEnumerable<short>, IEnumerator<short> {

            short* m_cur;
            short* m_start;
            short* m_end;
            IDisposable m_obj;

            public ContinuousIteratorShort(short* start, short* end, IDisposable obj) {
                m_start = start;
                m_end = end;
                m_cur = (short*)0;
                m_obj = obj;
            }

            public short Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return *m_cur;
                }
            }

            object IEnumerator.Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return *m_cur;
                }
            }
            public void Dispose() {

                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null; 
                }
            }

            public IEnumerator<short> GetEnumerator() {
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


       
        public unsafe struct ContinuousIteratorUShort : IEnumerable<ushort>, IEnumerator<ushort> {

            ushort* m_cur;
            ushort* m_start;
            ushort* m_end;
            IDisposable m_obj;

            public ContinuousIteratorUShort(ushort* start, ushort* end, IDisposable obj) {
                m_start = start;
                m_end = end;
                m_cur = (ushort*)0;
                m_obj = obj;
            }

            public ushort Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return *m_cur;
                }
            }

            object IEnumerator.Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return *m_cur;
                }
            }
            public void Dispose() {

                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null; 
                }
            }

            public IEnumerator<ushort> GetEnumerator() {
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


       
        public unsafe struct ContinuousIteratorInt : IEnumerable<int>, IEnumerator<int> {

            int* m_cur;
            int* m_start;
            int* m_end;
            IDisposable m_obj;

            public ContinuousIteratorInt(int* start, int* end, IDisposable obj) {
                m_start = start;
                m_end = end;
                m_cur = (int*)0;
                m_obj = obj;
            }

            public int Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return *m_cur;
                }
            }

            object IEnumerator.Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return *m_cur;
                }
            }
            public void Dispose() {

                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null; 
                }
            }

            public IEnumerator<int> GetEnumerator() {
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


       
        public unsafe struct ContinuousIteratorUInt : IEnumerable<UInt32>, IEnumerator<UInt32> {

            UInt32* m_cur;
            UInt32* m_start;
            UInt32* m_end;
            IDisposable m_obj;

            public ContinuousIteratorUInt(UInt32* start, UInt32* end, IDisposable obj) {
                m_start = start;
                m_end = end;
                m_cur = (UInt32*)0;
                m_obj = obj;
            }

            public UInt32 Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return *m_cur;
                }
            }

            object IEnumerator.Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return *m_cur;
                }
            }
            public void Dispose() {

                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null; 
                }
            }

            public IEnumerator<UInt32> GetEnumerator() {
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


       
        public unsafe struct ContinuousIteratorLong : IEnumerable<long>, IEnumerator<long> {

            long* m_cur;
            long* m_start;
            long* m_end;
            IDisposable m_obj;

            public ContinuousIteratorLong(long* start, long* end, IDisposable obj) {
                m_start = start;
                m_end = end;
                m_cur = (long*)0;
                m_obj = obj;
            }

            public long Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return *m_cur;
                }
            }

            object IEnumerator.Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return *m_cur;
                }
            }
            public void Dispose() {

                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null; 
                }
            }

            public IEnumerator<long> GetEnumerator() {
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


       
        public unsafe struct ContinuousIteratorULong : IEnumerable<ulong>, IEnumerator<ulong> {

            ulong* m_cur;
            ulong* m_start;
            ulong* m_end;
            IDisposable m_obj;

            public ContinuousIteratorULong(ulong* start, ulong* end, IDisposable obj) {
                m_start = start;
                m_end = end;
                m_cur = (ulong*)0;
                m_obj = obj;
            }

            public ulong Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return *m_cur;
                }
            }

            object IEnumerator.Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return *m_cur;
                }
            }
            public void Dispose() {

                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null; 
                }
            }

            public IEnumerator<ulong> GetEnumerator() {
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


       
        public unsafe struct ContinuousIteratorfComplex : IEnumerable<fcomplex>, IEnumerator<fcomplex> {

            fcomplex* m_cur;
            fcomplex* m_start;
            fcomplex* m_end;
            IDisposable m_obj;

            public ContinuousIteratorfComplex(fcomplex* start, fcomplex* end, IDisposable obj) {
                m_start = start;
                m_end = end;
                m_cur = (fcomplex*)0;
                m_obj = obj;
            }

            public fcomplex Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return *m_cur;
                }
            }

            object IEnumerator.Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return *m_cur;
                }
            }
            public void Dispose() {

                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null; 
                }
            }

            public IEnumerator<fcomplex> GetEnumerator() {
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


       
        public unsafe struct ContinuousIteratorComplex : IEnumerable<complex>, IEnumerator<complex> {

            complex* m_cur;
            complex* m_start;
            complex* m_end;
            IDisposable m_obj;

            public ContinuousIteratorComplex(complex* start, complex* end, IDisposable obj) {
                m_start = start;
                m_end = end;
                m_cur = (complex*)0;
                m_obj = obj;
            }

            public complex Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return *m_cur;
                }
            }

            object IEnumerator.Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return *m_cur;
                }
            }
            public void Dispose() {

                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null; 
                }
            }

            public IEnumerator<complex> GetEnumerator() {
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


       
        public unsafe struct ContinuousIteratorSingle : IEnumerable<float>, IEnumerator<float> {

            float* m_cur;
            float* m_start;
            float* m_end;
            IDisposable m_obj;

            public ContinuousIteratorSingle(float* start, float* end, IDisposable obj) {
                m_start = start;
                m_end = end;
                m_cur = (float*)0;
                m_obj = obj;
            }

            public float Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return *m_cur;
                }
            }

            object IEnumerator.Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return *m_cur;
                }
            }
            public void Dispose() {

                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null; 
                }
            }

            public IEnumerator<float> GetEnumerator() {
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

        public unsafe struct ContinuousIteratorBool : IEnumerable<bool>, IEnumerator<bool> {

            byte* m_cur;
            byte* m_start;
            byte* m_end;
            IDisposable m_obj;

            public ContinuousIteratorBool(byte* start, byte* end, IDisposable obj) {
                m_start = start;
                m_end = end;
                m_cur = (byte*)0;
                m_obj = obj;
            }

            public bool Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return *m_cur != 0;
                }
            }

            object IEnumerator.Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return *m_cur != 0;
                }
            }
            public void Dispose() {

                if (m_obj != null) {
                    m_obj.Dispose();
                    m_obj = null;
                }
            }

            public IEnumerator<bool> GetEnumerator() {
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

        public unsafe struct ContinuousIterator<T> : IEnumerable<T>, IEnumerator<T> {

            long m_cur;
            long m_start;
            long m_end;
            IDisposable m_obj;
            T[] m_values; 

            public ContinuousIterator(T[] values, long start, long end, IDisposable obj) {
                m_start = start;
                m_end = end;
                m_cur = -1; 
                m_obj = obj;
                m_values = values; 
            }

            public T Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return m_values[m_cur];
                }
            }

            object IEnumerator.Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext() {
                if (m_cur < 0) {
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
                m_cur = -1;
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return this;
            }
        }
        public unsafe struct ContinuousSizeIterator : IEnumerable<long>, IEnumerator<long> {

            long m_cur;
            long m_end;
            long m_start;

            public ContinuousSizeIterator(Size S) {
                m_start = S.BaseOffset; 
                m_end = (long)S.NumberOfElements - 1 + m_start;
                m_cur = -1;
            }

            public long Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return m_cur;
                }
            }

            object IEnumerator.Current {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get {
                    return m_cur;
                }
            }

            public void Dispose() {

            }

            public IEnumerator<long> GetEnumerator() {
                return this;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext() {
                if (m_cur < 0) {
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
                m_cur = -1;
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return this;
            }
        }

    }
}
