//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////

using ILNumerics.Core.Arrays;
using ILNumerics.Core.Functions.Builtin;
using ILNumerics.Core.MemoryLayer;
using ILNumerics.Core.Native;
using System;
using System.Numerics;
using System.Security;
using ILNumerics.F2NET.Array;
using System.Runtime.CompilerServices;
using ILNumerics.Core.Internal;

namespace ILNumerics.Core.StorageLayer {

    public abstract partial class BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> : IDisposable, IStorage
        where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

        #region WriteTo_ML_ IterIter + _BSDIter implementations

        
        internal void WriteTo_ML_IterIter(StorageT value, IIndexIterator[] iterators, uint ndimsOut) {

            if (m_handles[0] is NativeHostHandle) {
                switch (SizeOfT) {
                    case 1:
                        WriteTo_ML_IterIter_1(value, iterators, ndimsOut);
                        break;
                    case 2:
                        WriteTo_ML_IterIter_2(value, iterators, ndimsOut);
                        break;
                    case 4:
                        WriteTo_ML_IterIter_4(value, iterators, ndimsOut);
                        break;
                    case 8:
                        WriteTo_ML_IterIter_8(value, iterators, ndimsOut);
                        break;
                    case 16:
                        WriteTo_ML_IterIter_16(value, iterators, ndimsOut);
                        break;
                    default:
                        WriteTo_ML_IterIter_Arbitrary(value, iterators, ndimsOut, SizeOfT);
                        break;
                        //throw new NotSupportedException($"The element type {typeof(T)} is not supported in this context. Supported element types: value types (structs) of length 1,2,4,8 or 16 bytes and reference types.");
                }
            } else {
                WriteTo_ML_IterIter_T(value, iterators, ndimsOut); // overridden in CellStorage
            }

        }
        
        internal unsafe void WriteTo_ML_BSDIter(StorageT value, long* outBSD) {

            if (m_handles[0] is NativeHostHandle) {
                switch (SizeOfT) {
                    case 1:
                        WriteTo_ML_BSDIter_1(value, outBSD);
                        break;
                    case 2:
                        WriteTo_ML_BSDIter_2(value, outBSD);
                        break;
                    case 4:
                        WriteTo_ML_BSDIter_4(value, outBSD);
                        break;
                    case 8:
                        WriteTo_ML_BSDIter_8(value, outBSD);
                        break;
                    case 16:
                        WriteTo_ML_BSDIter_16(value, outBSD);
                        break;
                    default:
                        WriteTo_ML_BSDIter_Arbitrary((byte*)m_handles[0].Pointer, value, outBSD, SizeOfT);
                        break;
                        //throw new NotSupportedException($"The element type {typeof(T)} is not supported in this context. Supported element types: value types (structs) of length 1,2,4,8 or 16 bytes and reference types.");
                }
            } else {
                WriteTo_ML_BSDIter_T(value, outBSD);  // this is overridden in CellStorage
            }
        }

        #region HYCALPER LOOPSTART WriteTo_IterIter_'s

        /*!HC:TYPELIST:
        <hycalper>
        <type>
            <source locate="here">
                WriteTo_ML_IterIter_2
            </source>
            <destination>WriteTo_ML_IterIter_1</destination>
            <destination>WriteTo_ML_IterIter_4</destination>
            <destination>WriteTo_ML_IterIter_8</destination>
            <destination>WriteTo_ML_IterIter_16</destination>
        </type>
        <type>
            <source locate="here">
                WriteTo_ML_BSDIter_2
            </source>
            <destination>WriteTo_ML_BSDIter_1</destination>
            <destination>WriteTo_ML_BSDIter_4</destination>
            <destination>WriteTo_ML_BSDIter_8</destination>
            <destination>WriteTo_ML_BSDIter_16</destination>
        </type>
        <type>
            <source locate="here">
                ushort
            </source>
            <destination>byte</destination>
            <destination>uint</destination>
            <destination>ulong</destination>
            <destination>complex</destination>
        </type>
        <type>
            <source locate="here">
                UInt16
            </source>
            <destination>Byte</destination>
            <destination>UInt32</destination>
            <destination>UInt64</destination>
            <destination>Complex</destination>
        </type>
        </hycalper>
        */

        /// <summary>
        /// Copies values from <paramref name="value"/>, regardless of shape(!) to the parts of this storage determined by <paramref name="outIterators"/>. No broadcasting!
        /// </summary>
        /// <param name="value"></param>
        /// <param name="outIterators"></param>
        /// <param name="ndimsOut">Number of iterators in <paramref name="outIterators"/> to consider.</param>
        
        private unsafe void WriteTo_ML_IterIter_2(StorageT value, IIndexIterator[] outIterators, uint ndimsOut) {

            // This handles empty storages! (by handling empty iterators)

            // value must have the same number of elements as the range addressed by iterators! 

            var valueIter = value.m_size.Iterator(order: StorageOrders.ColumnMajor).GetEnumerator();
            if (!valueIter.MoveNext())
                return;  // nothing to do

            long stride0 = m_size.GetStride(0);
            var it0 = outIterators[0];
            if (it0.GetLength() == 0) return;
            long higdims = m_size.BaseOffset;
            for (uint i = 1; i < ndimsOut; i++) {
                if (!outIterators[i].MoveNext()) {
                    return; // nothing to do. Empty range.
                }
                // OOR should not happen. Otherwise we would end up in the 'expand' path.
                // System.Diagnostics.Debug.Assert((ulong)val <= (ulong)iterators[i].GetLastDimensionIndex());
                // throw new IndexOutOfRangeException($"Index '{val}' exceeds the valid range of dimension {i}: 0 <= i < {iterators[i].GetLastDimensionIndex()}.");
                higdims += (outIterators[i].Current * m_size.GetStride(i));
            }

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            ushort* pOut = (ushort*)m_handles[0].Pointer;
            ushort* pIn = (ushort*)value.m_handles[0].Pointer;
            while (true) {

                while (it0.MoveNext()) {
                    pOut[higdims + it0.Current * stride0] = pIn[valueIter.Current];
                    valueIter.MoveNext(); // assumes succeess (?!)
                };
                it0.Reset();

                // increase higher dims
                uint d = 1;
                while (d < ndimsOut) {
                    var itD = outIterators[d];
                    var oldIdx = itD.Current;

                    if (itD.MoveNext()) {
                        var val = itD.Current;
                        higdims += (val - oldIdx) * m_size.GetStride(d);
                        break;
                    } else {
                        itD.Reset();
                        itD.MoveNext();  // assuming this succeeds
                        higdims += (itD.Current - oldIdx) * m_size.GetStride(d);
                        d++;
                    }
                }
                if (d == ndimsOut) return;  // careful with dims < 2 (numpy)! we may never step into the while{}!
            }
        }

        /// <summary>
        /// Copies values from <paramref name="value"/>, to the parts of this storage as determined by <paramref name="outBSD"/>. No broadcasting!
        /// </summary>
        /// <param name="value">src values of the copy. The right side of the assignment.</param>
        /// <param name="outBSD">set of BSD describing the parts to be overwritten.</param>
        
        private unsafe void WriteTo_ML_BSDIter_2(StorageT value, long* outBSD) {

            // This handles empty storages! (by handling empty iterators)

            // value must have the same number of elements as the range addressed by iterators! 

            var valueIter = value.m_size.Iterator(order: StorageOrders.ColumnMajor).GetEnumerator();
            if (!valueIter.MoveNext())
                return;  // nothing to do

            uint ndims = (uint)outBSD[0]; // number of dimensions addressed by the user
            if (outBSD[1] == 0) return;

            long higdims = outBSD[2]; // this includes the initial base offset already!

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            ushort* pOut = (ushort*)m_handles[0].Pointer;
            ushort* pIn = (ushort*)value.m_handles[0].Pointer;
            long stride0 = (long)outBSD[3 + ndims];
            long dimLen0 = (long)outBSD[3];
            long* cur = stackalloc long[(int)ndims];
            for (int i = 0; i < ndims; i++) {
                cur[i] = 0;
            }

            while (true) {
                long cur0 = 0;
                while (cur0 < dimLen0) {
                    pOut[higdims + cur0 * stride0] = pIn[valueIter.Current];
                    valueIter.MoveNext();
                    cur0++;
                }

                // increase higher dims
                uint d = 1;
                while (d < ndims) {
                    if (cur[d] < (long)outBSD[3 + d] - 1) {
                        higdims += (long)outBSD[3 + ndims + d];
                        cur[d]++;
                        break;
                    } else {
                        higdims -= (long)outBSD[3 + ndims + d] * cur[d];
                        cur[d] = 0;
                        d++;
                    }
                }
                if (d == ndims) return;
            }
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 

       

        /// <summary>
        /// Copies values from <paramref name="value"/>, regardless of shape(!) to the parts of this storage determined by <paramref name="outIterators"/>. No broadcasting!
        /// </summary>
        /// <param name="value"></param>
        /// <param name="outIterators"></param>
        /// <param name="ndimsOut">Number of iterators in <paramref name="outIterators"/> to consider.</param>
        
        private unsafe void WriteTo_ML_IterIter_16(StorageT value, IIndexIterator[] outIterators, uint ndimsOut) {

            // This handles empty storages! (by handling empty iterators)

            // value must have the same number of elements as the range addressed by iterators! 

            var valueIter = value.m_size.Iterator(order: StorageOrders.ColumnMajor).GetEnumerator();
            if (!valueIter.MoveNext())
                return;  // nothing to do

            long stride0 = m_size.GetStride(0);
            var it0 = outIterators[0];
            if (it0.GetLength() == 0) return;
            long higdims = m_size.BaseOffset;
            for (uint i = 1; i < ndimsOut; i++) {
                if (!outIterators[i].MoveNext()) {
                    return; // nothing to do. Empty range.
                }
                // OOR should not happen. Otherwise we would end up in the 'expand' path.
                // System.Diagnostics.Debug.Assert((ulong)val <= (ulong)iterators[i].GetLastDimensionIndex());
                // throw new IndexOutOfRangeException($"Index '{val}' exceeds the valid range of dimension {i}: 0 <= i < {iterators[i].GetLastDimensionIndex()}.");
                higdims += (outIterators[i].Current * m_size.GetStride(i));
            }

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            complex* pOut = (complex*)m_handles[0].Pointer;
            complex* pIn = (complex*)value.m_handles[0].Pointer;
            while (true) {

                while (it0.MoveNext()) {
                    pOut[higdims + it0.Current * stride0] = pIn[valueIter.Current];
                    valueIter.MoveNext(); // assumes succeess (?!)
                };
                it0.Reset();

                // increase higher dims
                uint d = 1;
                while (d < ndimsOut) {
                    var itD = outIterators[d];
                    var oldIdx = itD.Current;

                    if (itD.MoveNext()) {
                        var val = itD.Current;
                        higdims += (val - oldIdx) * m_size.GetStride(d);
                        break;
                    } else {
                        itD.Reset();
                        itD.MoveNext();  // assuming this succeeds
                        higdims += (itD.Current - oldIdx) * m_size.GetStride(d);
                        d++;
                    }
                }
                if (d == ndimsOut) return;  // careful with dims < 2 (numpy)! we may never step into the while{}!
            }
        }

        /// <summary>
        /// Copies values from <paramref name="value"/>, to the parts of this storage as determined by <paramref name="outBSD"/>. No broadcasting!
        /// </summary>
        /// <param name="value">src values of the copy. The right side of the assignment.</param>
        /// <param name="outBSD">set of BSD describing the parts to be overwritten.</param>
        
        private unsafe void WriteTo_ML_BSDIter_16(StorageT value, long* outBSD) {

            // This handles empty storages! (by handling empty iterators)

            // value must have the same number of elements as the range addressed by iterators! 

            var valueIter = value.m_size.Iterator(order: StorageOrders.ColumnMajor).GetEnumerator();
            if (!valueIter.MoveNext())
                return;  // nothing to do

            uint ndims = (uint)outBSD[0]; // number of dimensions addressed by the user
            if (outBSD[1] == 0) return;

            long higdims = outBSD[2]; // this includes the initial base offset already!

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            complex* pOut = (complex*)m_handles[0].Pointer;
            complex* pIn = (complex*)value.m_handles[0].Pointer;
            long stride0 = (long)outBSD[3 + ndims];
            long dimLen0 = (long)outBSD[3];
            long* cur = stackalloc long[(int)ndims];
            for (int i = 0; i < ndims; i++) {
                cur[i] = 0;
            }

            while (true) {
                long cur0 = 0;
                while (cur0 < dimLen0) {
                    pOut[higdims + cur0 * stride0] = pIn[valueIter.Current];
                    valueIter.MoveNext();
                    cur0++;
                }

                // increase higher dims
                uint d = 1;
                while (d < ndims) {
                    if (cur[d] < (long)outBSD[3 + d] - 1) {
                        higdims += (long)outBSD[3 + ndims + d];
                        cur[d]++;
                        break;
                    } else {
                        higdims -= (long)outBSD[3 + ndims + d] * cur[d];
                        cur[d] = 0;
                        d++;
                    }
                }
                if (d == ndims) return;
            }
        }

       

        /// <summary>
        /// Copies values from <paramref name="value"/>, regardless of shape(!) to the parts of this storage determined by <paramref name="outIterators"/>. No broadcasting!
        /// </summary>
        /// <param name="value"></param>
        /// <param name="outIterators"></param>
        /// <param name="ndimsOut">Number of iterators in <paramref name="outIterators"/> to consider.</param>
        
        private unsafe void WriteTo_ML_IterIter_8(StorageT value, IIndexIterator[] outIterators, uint ndimsOut) {

            // This handles empty storages! (by handling empty iterators)

            // value must have the same number of elements as the range addressed by iterators! 

            var valueIter = value.m_size.Iterator(order: StorageOrders.ColumnMajor).GetEnumerator();
            if (!valueIter.MoveNext())
                return;  // nothing to do

            long stride0 = m_size.GetStride(0);
            var it0 = outIterators[0];
            if (it0.GetLength() == 0) return;
            long higdims = m_size.BaseOffset;
            for (uint i = 1; i < ndimsOut; i++) {
                if (!outIterators[i].MoveNext()) {
                    return; // nothing to do. Empty range.
                }
                // OOR should not happen. Otherwise we would end up in the 'expand' path.
                // System.Diagnostics.Debug.Assert((ulong)val <= (ulong)iterators[i].GetLastDimensionIndex());
                // throw new IndexOutOfRangeException($"Index '{val}' exceeds the valid range of dimension {i}: 0 <= i < {iterators[i].GetLastDimensionIndex()}.");
                higdims += (outIterators[i].Current * m_size.GetStride(i));
            }

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            ulong* pOut = (ulong*)m_handles[0].Pointer;
            ulong* pIn = (ulong*)value.m_handles[0].Pointer;
            while (true) {

                while (it0.MoveNext()) {
                    pOut[higdims + it0.Current * stride0] = pIn[valueIter.Current];
                    valueIter.MoveNext(); // assumes succeess (?!)
                };
                it0.Reset();

                // increase higher dims
                uint d = 1;
                while (d < ndimsOut) {
                    var itD = outIterators[d];
                    var oldIdx = itD.Current;

                    if (itD.MoveNext()) {
                        var val = itD.Current;
                        higdims += (val - oldIdx) * m_size.GetStride(d);
                        break;
                    } else {
                        itD.Reset();
                        itD.MoveNext();  // assuming this succeeds
                        higdims += (itD.Current - oldIdx) * m_size.GetStride(d);
                        d++;
                    }
                }
                if (d == ndimsOut) return;  // careful with dims < 2 (numpy)! we may never step into the while{}!
            }
        }

        /// <summary>
        /// Copies values from <paramref name="value"/>, to the parts of this storage as determined by <paramref name="outBSD"/>. No broadcasting!
        /// </summary>
        /// <param name="value">src values of the copy. The right side of the assignment.</param>
        /// <param name="outBSD">set of BSD describing the parts to be overwritten.</param>
        
        private unsafe void WriteTo_ML_BSDIter_8(StorageT value, long* outBSD) {

            // This handles empty storages! (by handling empty iterators)

            // value must have the same number of elements as the range addressed by iterators! 

            var valueIter = value.m_size.Iterator(order: StorageOrders.ColumnMajor).GetEnumerator();
            if (!valueIter.MoveNext())
                return;  // nothing to do

            uint ndims = (uint)outBSD[0]; // number of dimensions addressed by the user
            if (outBSD[1] == 0) return;

            long higdims = outBSD[2]; // this includes the initial base offset already!

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            ulong* pOut = (ulong*)m_handles[0].Pointer;
            ulong* pIn = (ulong*)value.m_handles[0].Pointer;
            long stride0 = (long)outBSD[3 + ndims];
            long dimLen0 = (long)outBSD[3];
            long* cur = stackalloc long[(int)ndims];
            for (int i = 0; i < ndims; i++) {
                cur[i] = 0;
            }

            while (true) {
                long cur0 = 0;
                while (cur0 < dimLen0) {
                    pOut[higdims + cur0 * stride0] = pIn[valueIter.Current];
                    valueIter.MoveNext();
                    cur0++;
                }

                // increase higher dims
                uint d = 1;
                while (d < ndims) {
                    if (cur[d] < (long)outBSD[3 + d] - 1) {
                        higdims += (long)outBSD[3 + ndims + d];
                        cur[d]++;
                        break;
                    } else {
                        higdims -= (long)outBSD[3 + ndims + d] * cur[d];
                        cur[d] = 0;
                        d++;
                    }
                }
                if (d == ndims) return;
            }
        }

       

        /// <summary>
        /// Copies values from <paramref name="value"/>, regardless of shape(!) to the parts of this storage determined by <paramref name="outIterators"/>. No broadcasting!
        /// </summary>
        /// <param name="value"></param>
        /// <param name="outIterators"></param>
        /// <param name="ndimsOut">Number of iterators in <paramref name="outIterators"/> to consider.</param>
        
        private unsafe void WriteTo_ML_IterIter_4(StorageT value, IIndexIterator[] outIterators, uint ndimsOut) {

            // This handles empty storages! (by handling empty iterators)

            // value must have the same number of elements as the range addressed by iterators! 

            var valueIter = value.m_size.Iterator(order: StorageOrders.ColumnMajor).GetEnumerator();
            if (!valueIter.MoveNext())
                return;  // nothing to do

            long stride0 = m_size.GetStride(0);
            var it0 = outIterators[0];
            if (it0.GetLength() == 0) return;
            long higdims = m_size.BaseOffset;
            for (uint i = 1; i < ndimsOut; i++) {
                if (!outIterators[i].MoveNext()) {
                    return; // nothing to do. Empty range.
                }
                // OOR should not happen. Otherwise we would end up in the 'expand' path.
                // System.Diagnostics.Debug.Assert((ulong)val <= (ulong)iterators[i].GetLastDimensionIndex());
                // throw new IndexOutOfRangeException($"Index '{val}' exceeds the valid range of dimension {i}: 0 <= i < {iterators[i].GetLastDimensionIndex()}.");
                higdims += (outIterators[i].Current * m_size.GetStride(i));
            }

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            uint* pOut = (uint*)m_handles[0].Pointer;
            uint* pIn = (uint*)value.m_handles[0].Pointer;
            while (true) {

                while (it0.MoveNext()) {
                    pOut[higdims + it0.Current * stride0] = pIn[valueIter.Current];
                    valueIter.MoveNext(); // assumes succeess (?!)
                };
                it0.Reset();

                // increase higher dims
                uint d = 1;
                while (d < ndimsOut) {
                    var itD = outIterators[d];
                    var oldIdx = itD.Current;

                    if (itD.MoveNext()) {
                        var val = itD.Current;
                        higdims += (val - oldIdx) * m_size.GetStride(d);
                        break;
                    } else {
                        itD.Reset();
                        itD.MoveNext();  // assuming this succeeds
                        higdims += (itD.Current - oldIdx) * m_size.GetStride(d);
                        d++;
                    }
                }
                if (d == ndimsOut) return;  // careful with dims < 2 (numpy)! we may never step into the while{}!
            }
        }

        /// <summary>
        /// Copies values from <paramref name="value"/>, to the parts of this storage as determined by <paramref name="outBSD"/>. No broadcasting!
        /// </summary>
        /// <param name="value">src values of the copy. The right side of the assignment.</param>
        /// <param name="outBSD">set of BSD describing the parts to be overwritten.</param>
        
        private unsafe void WriteTo_ML_BSDIter_4(StorageT value, long* outBSD) {

            // This handles empty storages! (by handling empty iterators)

            // value must have the same number of elements as the range addressed by iterators! 

            var valueIter = value.m_size.Iterator(order: StorageOrders.ColumnMajor).GetEnumerator();
            if (!valueIter.MoveNext())
                return;  // nothing to do

            uint ndims = (uint)outBSD[0]; // number of dimensions addressed by the user
            if (outBSD[1] == 0) return;

            long higdims = outBSD[2]; // this includes the initial base offset already!

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            uint* pOut = (uint*)m_handles[0].Pointer;
            uint* pIn = (uint*)value.m_handles[0].Pointer;
            long stride0 = (long)outBSD[3 + ndims];
            long dimLen0 = (long)outBSD[3];
            long* cur = stackalloc long[(int)ndims];
            for (int i = 0; i < ndims; i++) {
                cur[i] = 0;
            }

            while (true) {
                long cur0 = 0;
                while (cur0 < dimLen0) {
                    pOut[higdims + cur0 * stride0] = pIn[valueIter.Current];
                    valueIter.MoveNext();
                    cur0++;
                }

                // increase higher dims
                uint d = 1;
                while (d < ndims) {
                    if (cur[d] < (long)outBSD[3 + d] - 1) {
                        higdims += (long)outBSD[3 + ndims + d];
                        cur[d]++;
                        break;
                    } else {
                        higdims -= (long)outBSD[3 + ndims + d] * cur[d];
                        cur[d] = 0;
                        d++;
                    }
                }
                if (d == ndims) return;
            }
        }

       

        /// <summary>
        /// Copies values from <paramref name="value"/>, regardless of shape(!) to the parts of this storage determined by <paramref name="outIterators"/>. No broadcasting!
        /// </summary>
        /// <param name="value"></param>
        /// <param name="outIterators"></param>
        /// <param name="ndimsOut">Number of iterators in <paramref name="outIterators"/> to consider.</param>
        
        private unsafe void WriteTo_ML_IterIter_1(StorageT value, IIndexIterator[] outIterators, uint ndimsOut) {

            // This handles empty storages! (by handling empty iterators)

            // value must have the same number of elements as the range addressed by iterators! 

            var valueIter = value.m_size.Iterator(order: StorageOrders.ColumnMajor).GetEnumerator();
            if (!valueIter.MoveNext())
                return;  // nothing to do

            long stride0 = m_size.GetStride(0);
            var it0 = outIterators[0];
            if (it0.GetLength() == 0) return;
            long higdims = m_size.BaseOffset;
            for (uint i = 1; i < ndimsOut; i++) {
                if (!outIterators[i].MoveNext()) {
                    return; // nothing to do. Empty range.
                }
                // OOR should not happen. Otherwise we would end up in the 'expand' path.
                // System.Diagnostics.Debug.Assert((ulong)val <= (ulong)iterators[i].GetLastDimensionIndex());
                // throw new IndexOutOfRangeException($"Index '{val}' exceeds the valid range of dimension {i}: 0 <= i < {iterators[i].GetLastDimensionIndex()}.");
                higdims += (outIterators[i].Current * m_size.GetStride(i));
            }

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            byte* pOut = (byte*)m_handles[0].Pointer;
            byte* pIn = (byte*)value.m_handles[0].Pointer;
            while (true) {

                while (it0.MoveNext()) {
                    pOut[higdims + it0.Current * stride0] = pIn[valueIter.Current];
                    valueIter.MoveNext(); // assumes succeess (?!)
                };
                it0.Reset();

                // increase higher dims
                uint d = 1;
                while (d < ndimsOut) {
                    var itD = outIterators[d];
                    var oldIdx = itD.Current;

                    if (itD.MoveNext()) {
                        var val = itD.Current;
                        higdims += (val - oldIdx) * m_size.GetStride(d);
                        break;
                    } else {
                        itD.Reset();
                        itD.MoveNext();  // assuming this succeeds
                        higdims += (itD.Current - oldIdx) * m_size.GetStride(d);
                        d++;
                    }
                }
                if (d == ndimsOut) return;  // careful with dims < 2 (numpy)! we may never step into the while{}!
            }
        }

        /// <summary>
        /// Copies values from <paramref name="value"/>, to the parts of this storage as determined by <paramref name="outBSD"/>. No broadcasting!
        /// </summary>
        /// <param name="value">src values of the copy. The right side of the assignment.</param>
        /// <param name="outBSD">set of BSD describing the parts to be overwritten.</param>
        
        private unsafe void WriteTo_ML_BSDIter_1(StorageT value, long* outBSD) {

            // This handles empty storages! (by handling empty iterators)

            // value must have the same number of elements as the range addressed by iterators! 

            var valueIter = value.m_size.Iterator(order: StorageOrders.ColumnMajor).GetEnumerator();
            if (!valueIter.MoveNext())
                return;  // nothing to do

            uint ndims = (uint)outBSD[0]; // number of dimensions addressed by the user
            if (outBSD[1] == 0) return;

            long higdims = outBSD[2]; // this includes the initial base offset already!

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            byte* pOut = (byte*)m_handles[0].Pointer;
            byte* pIn = (byte*)value.m_handles[0].Pointer;
            long stride0 = (long)outBSD[3 + ndims];
            long dimLen0 = (long)outBSD[3];
            long* cur = stackalloc long[(int)ndims];
            for (int i = 0; i < ndims; i++) {
                cur[i] = 0;
            }

            while (true) {
                long cur0 = 0;
                while (cur0 < dimLen0) {
                    pOut[higdims + cur0 * stride0] = pIn[valueIter.Current];
                    valueIter.MoveNext();
                    cur0++;
                }

                // increase higher dims
                uint d = 1;
                while (d < ndims) {
                    if (cur[d] < (long)outBSD[3 + d] - 1) {
                        higdims += (long)outBSD[3 + ndims + d];
                        cur[d]++;
                        break;
                    } else {
                        higdims -= (long)outBSD[3 + ndims + d] * cur[d];
                        cur[d] = 0;
                        d++;
                    }
                }
                if (d == ndims) return;
            }
        }

#endregion HYCALPER AUTO GENERATED CODE

        /// <summary>
        /// Copies values from <paramref name="value"/>, regardless of shape(!) to the parts of this storage determined by <paramref name="outIterators"/>. No broadcasting!
        /// </summary>
        /// <param name="value"></param>
        /// <param name="outIterators"></param>
        /// <param name="ndimsOut">Number of iterators in <paramref name="outIterators"/> to consider.</param>
        /// <param name="elementSize">single element size in bytes</param>
        
        private unsafe void WriteTo_ML_IterIter_Arbitrary(StorageT value, IIndexIterator[] outIterators, uint ndimsOut, uint elementSize) {

            // This handles empty storages! (by handling empty iterators)

            // value must have the same number of elements as the range addressed by iterators! 

            var valueIter = value.m_size.Iterator(order: StorageOrders.ColumnMajor).GetEnumerator();
            if (!valueIter.MoveNext())
                return;  // nothing to do

            long stride0 = m_size.GetStride(0);
            var it0 = outIterators[0];
            if (it0.GetLength() == 0) return;
            long higdims = m_size.BaseOffset;
            for (uint i = 1; i < ndimsOut; i++) {
                if (!outIterators[i].MoveNext()) {
                    return; // nothing to do. Empty range.
                }
                // OOR should not happen. Otherwise we would end up in the 'expand' path.
                // System.Diagnostics.Debug.Assert((ulong)val <= (ulong)iterators[i].GetLastDimensionIndex());
                // throw new IndexOutOfRangeException($"Index '{val}' exceeds the valid range of dimension {i}: 0 <= i < {iterators[i].GetLastDimensionIndex()}.");
                higdims += (outIterators[i].Current * m_size.GetStride(i));
            }

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            byte* pOut = (byte*)m_handles[0].Pointer;
            byte* pIn = (byte*)value.m_handles[0].Pointer;
            while (true) {

                while (it0.MoveNext()) {
                    for (int i = 0; i < elementSize; i++) {
                        pOut[(higdims + it0.Current * stride0) * elementSize + i] = pIn[valueIter.Current * elementSize + i];
                    }
                    valueIter.MoveNext(); // assumes succeess (?!)
                };
                it0.Reset();

                // increase higher dims
                uint d = 1;
                while (d < ndimsOut) {
                    var itD = outIterators[d];
                    var oldIdx = itD.Current;

                    if (itD.MoveNext()) {
                        var val = itD.Current;
                        higdims += (val - oldIdx) * m_size.GetStride(d);
                        break;
                    } else {
                        itD.Reset();
                        itD.MoveNext();  // assuming this succeeds
                        higdims += (itD.Current - oldIdx) * m_size.GetStride(d);
                        d++;
                    }
                }
                if (d == ndimsOut) return;  // careful with dims < 2 (numpy)! we may never step into the while{}!
            }
        }

        /// <summary>
        /// Copies values from <paramref name="value"/>, to the parts of this storage as determined by <paramref name="outBSD"/>. No broadcasting!
        /// </summary>
        /// <param name="pOut"></param>
        /// <param name="value">src values of the copy. The right side of the assignment.</param>
        /// <param name="outBSD">set of BSD describing the parts to be overwritten.</param>
        /// <param name="elementSize">Single element size in bytes.</param>

        private unsafe static void WriteTo_ML_BSDIter_Arbitrary(byte* pOut, StorageT value, long* outBSD, uint elementSize) {

            // This handles empty storages! (by handling empty iterators)

            // value must have the same number of elements as the range addressed by iterators! 

            var valueIter = value.m_size.Iterator(order: StorageOrders.ColumnMajor).GetEnumerator();
            if (!valueIter.MoveNext())
                return;  // nothing to do

            uint ndims = (uint)outBSD[0]; // number of dimensions addressed by the user
            if (outBSD[1] == 0) return;

            long higdims = outBSD[2]; // this includes the initial base offset already!

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            byte* pIn = (byte*)value.m_handles[0].Pointer;
            long stride0 = (long)outBSD[3 + ndims];
            long dimLen0 = (long)outBSD[3];
            long* cur = stackalloc long[(int)ndims];
            for (int i = 0; i < ndims; i++) {
                cur[i] = 0;
            }

            while (true) {
                long cur0 = 0;
                while (cur0 < dimLen0) {
                    for (int i = 0; i < elementSize; i++) {
                        pOut[(higdims + cur0 * stride0) * elementSize + i] = pIn[valueIter.Current * elementSize + i];
                    }
                    valueIter.MoveNext();
                    cur0++;
                }

                // increase higher dims
                uint d = 1;
                while (d < ndims) {
                    if (cur[d] < (long)outBSD[3 + d] - 1) {
                        higdims += (long)outBSD[3 + ndims + d];
                        cur[d]++;
                        break;
                    } else {
                        higdims -= (long)outBSD[3 + ndims + d] * cur[d];
                        cur[d] = 0;
                        d++;
                    }
                }
                if (d == ndims) return;
            }
        }


        
        protected virtual unsafe void WriteTo_ML_IterIter_T(StorageT value, IIndexIterator[] iterators, uint ndimsOut) {

            // This handles empty storages! (by handling empty iterators)

            var valueIter = value.GetEnumerator(storageOrder: StorageOrders.ColumnMajor, dispose: false);
            if (!valueIter.MoveNext())
                return;  // nothing to do

            long stride0 = m_size.GetStride(0);
            var it0 = iterators[0];
            if (it0.GetLength() == 0) return;
            long higdims = m_size.BaseOffset;
            for (uint i = 1; i < ndimsOut; i++) {
                if (!iterators[i].MoveNext()) {
                    return; // nothing to do. Empty right side.
                }
                // OOR should not happen. Otherwise we would end up in the 'expand' path.
                // System.Diagnostics.Debug.Assert((ulong)val <= (ulong)iterators[i].GetLastDimensionIndex());
                // throw new IndexOutOfRangeException($"Index '{val}' exceeds the valid range of dimension {i}: 0 <= i < {iterators[i].GetLastDimensionIndex()}.");
                higdims += (iterators[i].Current * m_size.GetStride(i));
            }

            System.Diagnostics.Debug.Assert(!typeof(T).IsValueType); // && SizeOfT == IntPtr.m_size);
            System.Diagnostics.Debug.Assert(m_handles[0] is ManagedHostHandle<T>);


            T[] pOut = (m_handles[0] as ManagedHostHandle<T>).HostArray;

            while (true) {

                while (it0.MoveNext()) {
                    pOut[higdims + it0.Current * stride0] = valueIter.Current;
                    valueIter.MoveNext(); // assumes succeess (?!)
                };
                it0.Reset();

                // increase higher dims
                uint d = 1;
                while (d < ndimsOut) {
                    var itD = iterators[d];
                    var oldIdx = itD.Current;

                    if (itD.MoveNext()) {
                        var val = itD.Current;
                        higdims += (val - oldIdx) * m_size.GetStride(d);
                        break;
                    } else {
                        itD.Reset();
                        itD.MoveNext();  // assuming this succeeds
                        higdims += (itD.Current - oldIdx) * m_size.GetStride(d);
                        if (++d == ndimsOut) return;
                    }
                }
            }
        }

        /// <summary>
        /// Copies values from <paramref name="value"/>, regardless of shape(!) to the parts of this storage as determined by <paramref name="outBSD"/>. No broadcasting!
        /// </summary>
        /// <param name="value">src values of the copy. The right side of the assignment.</param>
        /// <param name="outBSD">set of BSD describing the parts to be overwritten.</param>
        
        protected unsafe virtual void WriteTo_ML_BSDIter_T(StorageT value, long* outBSD) {

            // This handles empty storages! 

            // value must have the same number of elements as the range addressed by iterators! 

            var valueIter = value.m_size.Iterator(order: StorageOrders.ColumnMajor).GetEnumerator();
            if (!valueIter.MoveNext())
                return;  // nothing to do

            uint ndims = (uint)outBSD[0]; // number of dimensions addressed by the user
            if (outBSD[1] == 0) return;

            long higdims = outBSD[2]; // this includes the initial base offset already!

            System.Diagnostics.Debug.Assert(!typeof(T).IsValueType);

            T[] pOut = (m_handles[0] as ManagedHostHandle<T>).HostArray;
            T[] pIn = (value.m_handles[0] as ManagedHostHandle<T>).HostArray;
            long stride0 = outBSD[3 + ndims];
            long dimLen0 = outBSD[3] * stride0;
            long* cur = stackalloc long[(int)ndims];
            for (int i = 0; i < ndims; i++) {
                cur[i] = 0;
            }

            while (true) {
                long cur0 = 0;
                while (cur0 < dimLen0) {
                    pOut[higdims + cur0 * stride0] = pIn[valueIter.Current];
                    valueIter.MoveNext();
                    cur0++;
                }

                // increase higher dims
                uint d = 1;
                while (d < ndims) {
                    if (cur[d] < outBSD[3 + d] - 1) {
                        higdims += outBSD[3 + ndims + d];
                        cur[d]++;
                        break;
                    } else {
                        higdims -= outBSD[3 + ndims + d] * cur[d];
                        cur[d] = 0;
                        d++;
                    }
                }
                if (d == ndims) return;
            }
        }


        #endregion

    }
}
