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

        /// <summary>
        /// Sets a single value, generic version. Works in-place (may modify this storage).
        /// </summary>
        /// <param name="value">Generic value.</param>
        /// <param name="byteIdx">BYTE index of the handle's storage to store <paramref name="value"/> into. This must include any base offset and striding. See notes for ref types!</param>
        /// <remarks><para><paramref name="byteIdx"/> is scaled by the element size as acquired from <see cref="SizeOfT"/>. For value types this gived (mostly) the 
        /// result of <see cref="System.Runtime.InteropServices.Marshal.SizeOf{T}(T)"/>. For reference types, however, <see cref="SizeOfT"/> gives 1. Hence, the scaling 
        /// expected here is the byte offset for value types and the element offset for reference types! !</para></remarks>
        
        internal unsafe virtual void SetValueSeq(T value, long byteIdx) {
            // ==================================================================================
            // THIS METHOD EXPOSES AN UGLGY DESIGN FLAW!
            // BaseArray<...> and BaseStorage<,,,> miss another type parameter: the storage type, HandleT.
            // All objects where the storage type is different from the exposed element type T
            // become clumsy and require those aweful 'if type1 ... else if type2' cascades.
            // Logical and Cell would profit a lot! 
            //
            // Update ho,18-06-06: does not look toooo bad. It may help to override Cell-/LogicalStorage where useful. 
            // =================================================================================)

            //System.Diagnostics.Debug.Assert(ReferenceCount <= 1 || !IsReady);  // 0 or 1: allow within generator funcs (vector).
            System.Diagnostics.Debug.Assert(byteIdx <= (Size.GetElementSpan() + Size.BaseOffset) * SizeOfT);

            if (Handles.ReferenceCount > 1) {
                var baseOffs = Size.BaseOffset;
                var detached = DetachBufferSetInplace(0);
                if (detached) {
                    System.Diagnostics.Debug.Assert(Size.BaseOffset == 0, "After detaching the base offset was expected to be '0'. Found: " + Size.BaseOffset + "."); 
                    byteIdx -= baseOffs * SizeOfT;
                }
            }

            if (Handles[0] is NativeHostHandle) {
                //if (dummy is bool) {  // -> overloaded in -> LogicalStorage
                //    *((byte*)Handles[0].Pointer + byteIdx) = (bool)(object)value ? (byte)1 : (byte)0;
                //} else {
                System.Runtime.CompilerServices.Unsafe.Copy<T>((byte*)Handles[0].Pointer + byteIdx, ref value);
                //T dummy = default(T);
                //System.TypedReference tr = __makeref(dummy);
                //*(IntPtr*)(&tr) = (IntPtr)((byte*)Handles[0].Pointer + byteIdx);
                //__refvalue(tr, T) = value;
                //}
            } else if (Handles[0] is ManagedHostHandle<IStorage>) {
                // Note this is implemented here also (and not _only_ in CellStorage) beause we 
                // want to be able to handle vector<BaseArray> also. It does not give all features 
                // of Cell but is convenient as temporary array container nevertheless, for example 
                // in cell(size(...),vector<BaseArray>(A,A+2,...)) initialization functions.
                var arr = (Handles[0] as ManagedHostHandle<IStorage>).HostArray;
                arr[byteIdx / SizeOfT]?.Release();
                var istor = (value as BaseArray)?.GetClonedStorage();
                arr[byteIdx / SizeOfT] = istor;
            } else {
                (Handles[0] as ManagedHostHandle<T>).HostArray[byteIdx / SizeOfT] = value;
            }
        }

        /// <summary>
        /// Sets a single value, generic version. Out-of-place version (does not modify this storage).
        /// </summary>
        /// <param name="value">Generic value.</param>
        /// <param name="byteIdx">BYTE index of the handle's storage to store <paramref name="value"/> into. This must include any base offset and striding. See notes for ref types!</param>
        /// <returns>This storage or a new, detached storage with the value at address <paramref name="byteIdx"/> set to the new value.</returns>
        /// <remarks><para><paramref name="byteIdx"/> is scaled by the element size as acquired from <see cref="SizeOfT"/>. For value types this gives (mostly) the 
        /// result of <see cref="System.Runtime.InteropServices.Marshal.SizeOf{T}(T)"/>. For reference types, however, <see cref="SizeOfT"/> gives 1. Hence, the scaling 
        /// expected here is the byte offset for value types and the element offset for reference types!</para></remarks>
        
        internal unsafe virtual StorageT SetValueSeq_OOP(T value, long byteIdx) {
            
            // Disabled: disregard array reference count. It may be 2 temporarily. And if other threads read from this storage it is allowed and pot. consequences are disregarded, too. 
            // System.Diagnostics.Debug.Assert(ReferenceCount <= 1 || !IsReady);  // 0 or 1: allow within generator funcs (vector).
            System.Diagnostics.Debug.Assert(byteIdx <= (Size.GetElementSpan() + Size.BaseOffset) * SizeOfT);

            if (Handles.ReferenceCount > 1) {
                var baseOffs = Size.BaseOffset;
                var newStorage = GetDetached(0) as StorageT;
                if (!object.ReferenceEquals(this, newStorage)) {
                    // adjust the target address for new value. BaseOffset was reset' by DetachBufferset().
                    byteIdx -= baseOffs * SizeOfT;
                    newStorage.SetValueSeq(value, byteIdx);
                    return newStorage;
                }
            }
            SetValueSeq(value, byteIdx);
            return this as StorageT; 
        }


        // moved to extension method, SetValue_Gen_OOP
        //#region generic write access
        //
        //internal virtual void SetValue(StorageT source, long d0, bool allowExpand = true) {

        //    if (ReferenceCount > 1) {
        //        throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
        //    }

        //    if (source.Size.NumberOfElements != 1) {
        //        throw new ArgumentException($"Addressing a single element for write access requires a scalar array as source value. Found: {source.Size.NumberOfElements} elements.");
        //    }
        //    long seqIdx;
        //    try {
        //        seqIdx = this.Size.GetSeqIndex(d0);
        //    } catch (IndexOutOfRangeException) {
        //        if (!allowExpand) throw; 
        //        Expand(new long[] { d0 + 1 });
        //        seqIdx = this.Size.GetSeqIndex(d0);
        //    }
        //    // SetValueSeq already detaches, when required ... 
        //    //if (Handles.ReferenceCount > 1) {
        //    //    DetachBufferset();
        //    //}
        //    SetValueSeq(source.GetValue(0), seqIdx * SizeOfT);
        //    //Handles[0].Set(source.Handles[0], (ulong)source.Size.BaseOffset, (ulong)seqIdx, SizeOfT);
        //}
        //
        //internal virtual void SetValue(StorageT source, long d0, long d1, bool allowExpand = true) {
        //    if (source.Size.NumberOfElements != 1) {
        //        throw new ArgumentException($"Addressing a single element for write access requires a scalar array as source. Found: {source.Size.NumberOfElements} elements.");
        //    }
        //    if (ReferenceCount > 1) {
        //        throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
        //    }

        //    long seqIdx;
        //    try {
        //        seqIdx = this.Size.GetSeqIndex(d0, d1);
        //    } catch (IndexOutOfRangeException) {
        //        if (!allowExpand) throw;
        //        Expand(new long[] { d0 + 1, d1 + 1 });  // cannot use stackalloc here -> catch() block :( 
        //        seqIdx = this.Size.GetSeqIndex(d0, d1);
        //    }
        //    // SetValueSeq already detaches, when required ... 
        //    //if (Handles.ReferenceCount > 1) {
        //    //    DetachBufferset();
        //    //}
        //    SetValueSeq(source.GetValue(0), seqIdx * SizeOfT);
        //    //Handles[0].Set(source.Handles[0], (ulong)source.Size.BaseOffset, (ulong)seqIdx, SizeOfT);
        //} 
        //
        //internal virtual void SetValue(StorageT source, long d0, long d1, long d2, bool allowExpand = true) {
        //    if (source.Size.NumberOfElements != 1) {
        //        throw new ArgumentException($"Addressing a single element for write access requires a scalar array as source. Found: {source.Size.NumberOfElements} elements.");
        //    }
        //    if (ReferenceCount > 1) {
        //        throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
        //    }

        //    long seqIdx;
        //    try {
        //        seqIdx = this.Size.GetSeqIndex(d0, d1, d2);
        //    } catch (IndexOutOfRangeException) {
        //        if (!allowExpand) throw;
        //        Expand(new long[] { d0 + 1, d1 + 1, d2 + 1 });
        //        seqIdx = this.Size.GetSeqIndex(d0, d1, d2);
        //    }
        //    // SetValueSeq already detaches, when required ... 
        //    //if (Handles.ReferenceCount > 1) {
        //    //    DetachBufferset();
        //    //}
        //    SetValueSeq(source.GetValue(0), seqIdx * SizeOfT);
        //    //Handles[0].Set(source.Handles[0], (ulong)source.Size.BaseOffset, (ulong)seqIdx, SizeOfT);
        //}
        //
        //internal virtual void SetValue(StorageT source, long d0, long d1, long d2, long d3, bool allowExpand = true) {
        //    if (source.Size.NumberOfElements != 1) {
        //        throw new ArgumentException($"Addressing a single element for write access requires a scalar array as source. Found: {source.Size.NumberOfElements} elements.");
        //    }
        //    if (ReferenceCount > 1) {
        //        throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
        //    }

        //    long seqIdx;
        //    try {
        //        seqIdx = this.Size.GetSeqIndex(d0, d1, d2, d3);
        //    } catch (IndexOutOfRangeException) {
        //        if (!allowExpand) throw;
        //        Expand(new long[] { d0 + 1, d1 + 1, d2 + 1, d3 + 1 });
        //        seqIdx = this.Size.GetSeqIndex(d0, d1, d2, d3);
        //    }
        //    // SetValueSeq already detaches, when required ... 
        //    //if (Handles.ReferenceCount > 1) {
        //    //    DetachBufferset();
        //    //}
        //    SetValueSeq(source.GetValue(0), seqIdx * SizeOfT);
        //    //Handles[0].Set(source.Handles[0], (ulong)source.Size.BaseOffset, (ulong)seqIdx, SizeOfT);
        //}
        //
        //internal virtual void SetValue(StorageT source, long d0, long d1, long d2, long d3, long d4, bool allowExpand = true) {
        //    if (source.Size.NumberOfElements != 1) {
        //        throw new ArgumentException($"Addressing a single element for write access requires a scalar array as source. Found: {source.Size.NumberOfElements} elements.");
        //    }
        //    if (ReferenceCount > 1) {
        //        throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
        //    }

        //    long seqIdx;
        //    try {
        //        seqIdx = this.Size.GetSeqIndex(d0, d1, d2, d3, d4);
        //    } catch (IndexOutOfRangeException) {
        //        if (!allowExpand) throw;
        //        Expand(new long[] { d0 + 1, d1 + 1, d2 + 1, d3 + 1, d4 + 1 });
        //        seqIdx = this.Size.GetSeqIndex(d0, d1, d2, d3, d4);
        //    }
        //    // SetValueSeq already detaches, when required ... 
        //    //if (Handles.ReferenceCount > 1) {
        //    //    DetachBufferset();
        //    //}
        //    SetValueSeq(source.GetValue(0), seqIdx * SizeOfT);
        //    //Handles[0].Set(source.Handles[0], (ulong)source.Size.BaseOffset, (ulong)seqIdx, SizeOfT);
        //}
        //
        //internal virtual void SetValue(StorageT source, long d0, long d1, long d2, long d3, long d4, long d5, bool allowExpand = true) {
        //    if (source.Size.NumberOfElements != 1) {
        //        throw new ArgumentException($"Addressing a single element for write access requires a scalar array as source. Found: {source.Size.NumberOfElements} elements.");
        //    }
        //    if (ReferenceCount > 1) {
        //        throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
        //    }

        //    long seqIdx;
        //    try {
        //        seqIdx = this.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5);
        //    } catch (IndexOutOfRangeException) {
        //        if (!allowExpand) throw;
        //        Expand(new long[] { d0 + 1, d1 + 1, d2 + 1, d3 + 1, d4 + 1, d5 + 1 });
        //        seqIdx = this.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5);
        //    }
        //    // SetValueSeq already detaches, when required ... 
        //    //if (Handles.ReferenceCount > 1) {
        //    //    DetachBufferset();
        //    //}
        //    SetValueSeq(source.GetValue(0), seqIdx * SizeOfT);
        //    //Handles[0].Set(source.Handles[0], (ulong)source.Size.BaseOffset, (ulong)seqIdx, SizeOfT);
        //}
        //
        //internal virtual void SetValue(StorageT source, long d0, long d1, long d2, long d3, long d4, long d5, long d6, bool allowExpand = true) {
        //    if (source.Size.NumberOfElements != 1) {
        //        throw new ArgumentException($"Addressing a single element for write access requires a scalar array as source. Found: {source.Size.NumberOfElements} elements.");
        //    }
        //    if (ReferenceCount > 1) {
        //        throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
        //    }

        //    long seqIdx;
        //    try {
        //        seqIdx = this.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6);
        //    } catch (IndexOutOfRangeException) {
        //        if (!allowExpand) throw;
        //        Expand(new long[] { d0 + 1, d1 + 1, d2 + 1, d3 + 1, d4 + 1, d5 + 1, d6 + 1 });
        //        seqIdx = this.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6);
        //    }
        //    // SetValueSeq already detaches, when required ... 
        //    //if (Handles.ReferenceCount > 1) {
        //    //    DetachBufferset();
        //    //}
        //    SetValueSeq(source.GetValue(0), seqIdx * SizeOfT);
        //    //Handles[0].Set(source.Handles[0], (ulong)source.Size.BaseOffset, (ulong)seqIdx, SizeOfT);
        //}
        //
        //internal virtual void SetValue(StorageT source, long[] dims, bool allowExpand = true) {
        //    if (source.Size.NumberOfElements != 1) {
        //        throw new ArgumentException($"Addressing a single element for write access requires a scalar array as source. Found: {source.Size.NumberOfElements} elements.");
        //    }
        //    if (ReferenceCount > 1) {
        //        throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
        //    }

        //    long seqIdx;
        //    try {
        //        seqIdx = this.Size.GetSeqIndex(dims);
        //    } catch (IndexOutOfRangeException) {
        //        if (!allowExpand) throw;
        //        using (Scope.Enter()) {

        //            Expand((MathInternal.vector(dims) + 1).GetArrayForRead());
        //            seqIdx = this.Size.GetSeqIndex(dims);
        //        }
        //    }
        //    // SetValueSeq already detaches, when required ... 
        //    //if (Handles.ReferenceCount > 1) {
        //    //    DetachBufferset();
        //    //}
        //    SetValueSeq(source.GetValue(0), seqIdx * SizeOfT);
        //    //Handles[0].Set(source.Handles[0], (ulong)source.Size.BaseOffset, (ulong)seqIdx, SizeOfT);
        //}


        ///// <summary>
        ///// Helper method delegating array (long*) indices to individual SetValue(d0,d1,..) dimensional indices. Used by Cells deep indexing. 
        ///// </summary>
        ///// <param name="value"></param>
        ///// <param name="indices"></param>
        ///// <param name="lenIndices"></param>
        ///// <param name="allowExpand"></param>
        //internal unsafe void SetValue(T value, long* indices, uint lenIndices, bool allowExpand) {
        //    switch(lenIndices) {
        //        case 0:
        //            if (Size.NumberOfDimensions != 0) {
        //                throw new IndexOutOfRangeException($"Indexing with empty indices is only allowed on 0-dimensional scalar arrays. Found: {Size.ToString()}.");
        //            }
        //            SetValueSeq(value, Size.BaseOffset * SizeOfT); 
        //            break;
        //        case 1:
        //            SetValue(value, indices[0], allowExpand);
        //            break;
        //        case 2:
        //            SetValue(value, indices[0], indices[1], allowExpand);
        //            break;
        //        case 3:
        //            SetValue(value, indices[0], indices[1], indices[2], allowExpand);
        //            break;
        //        case 4:
        //            SetValue(value, indices[0], indices[1], indices[2], indices[3], allowExpand);
        //            break;
        //        case 5:
        //            SetValue(value, indices[0], indices[1], indices[2], indices[3], indices[4], allowExpand);
        //            break;
        //        case 6:
        //            SetValue(value, indices[0], indices[1], indices[2], indices[3], indices[4], indices[5], allowExpand);
        //            break;
        //        case 7:
        //            SetValue(value, indices[0], indices[1], indices[2], indices[3], indices[4], indices[5], indices[6], allowExpand);
        //            break;
        //        default:

        //            if (ReferenceCount > 1) {
        //                if (!(this is CellStorage)) { // ReferenceEquals(value, this.m_retArray) || ReferenceCount != 2 ) {
        //                    throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
        //                }
        //            }

        //            long seqIdx;
        //            try {
        //                seqIdx = this.Size.GetSeqIndex(indices, lenIndices) * SizeOfT;
        //            } catch (IndexOutOfRangeException) {
        //                if (!allowExpand) {
        //                    throw;
        //                }
        //                Expand((((IntPtr)indices).AsArray<long>(lenIndices) + 1).GetArrayForRead());
        //                seqIdx = this.Size.GetSeqIndex(indices, lenIndices) * SizeOfT;
        //            }

        //            SetValueSeq(value, seqIdx);
        //            break; 
        //    }
        //}
        //internal void SetValue(T value, long d0, bool allowExpand = true) {

        //    if (ReferenceCount > 1) {
        //        if (!(this is CellStorage)) { // ReferenceEquals(value, this.m_retArray) || ReferenceCount != 2 ) {
        //            throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
        //        }
        //    }

        //    long seqIdx;
        //    try {
        //        seqIdx = this.Size.GetSeqIndex(d0) * SizeOfT;
        //    } catch (IndexOutOfRangeException) {
        //        if (!allowExpand) {
        //            throw;
        //        }
        //        Expand(new long[] { d0 + 1 });
        //        seqIdx = this.Size.GetSeqIndex(d0) * SizeOfT;
        //    }
        //    SetValueSeq(value, seqIdx);
        //}
        //internal void SetValue(T value, long d0, long d1, bool allowExpand = true) {

        //    if (ReferenceCount > 1) {
        //        if (!(this is CellStorage)) { // ReferenceEquals(value, this.m_retArray) || ReferenceCount != 2 ) {
        //            throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
        //        }
        //    }

        //    long seqIdx;
        //    try {
        //        seqIdx = this.Size.GetSeqIndex(d0, d1) * SizeOfT;
        //    } catch (IndexOutOfRangeException) {
        //        if (!allowExpand) {
        //            throw;
        //        }
        //        Expand(new long[] { d0 + 1, d1 + 1 });
        //        seqIdx = this.Size.GetSeqIndex(d0, d1) * SizeOfT;
        //    }

        //    SetValueSeq(value, seqIdx);

        //}
        //internal void SetValue(T value, long d0, long d1, long d2, bool allowExpand = true) {

        //    if (ReferenceCount > 1) {
        //        if (!(this is CellStorage)) { // ReferenceEquals(value, this.m_retArray) || ReferenceCount != 2 ) {
        //            throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
        //        }
        //    }

        //    long seqIdx;
        //    try {
        //        seqIdx = this.Size.GetSeqIndex(d0, d1, d2) * SizeOfT;
        //    } catch (IndexOutOfRangeException) {
        //        if (!allowExpand) {
        //            throw;
        //        }
        //        Expand(new long[] { d0 + 1, d1 + 1, d2 + 1 });
        //        seqIdx = this.Size.GetSeqIndex(d0, d1, d2) * SizeOfT;
        //    }

        //    SetValueSeq(value, seqIdx);

        //}
        //internal void SetValue(T value, long d0, long d1, long d2, long d3, bool allowExpand = true) {

        //    if (ReferenceCount > 1) {
        //        if (!(this is CellStorage)) { // ReferenceEquals(value, this.m_retArray) || ReferenceCount != 2 ) {
        //            throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
        //        }
        //    }

        //    long seqIdx;
        //    try {
        //        seqIdx = this.Size.GetSeqIndex(d0, d1, d2, d3) * SizeOfT;
        //    } catch (IndexOutOfRangeException) {
        //        if (!allowExpand) {
        //            throw;
        //        }
        //        Expand(new long[] { d0 + 1, d1 + 1, d2 + 1, d3 + 1 });
        //        seqIdx = this.Size.GetSeqIndex(d0, d1, d2, d3) * SizeOfT;
        //    }

        //    SetValueSeq(value, seqIdx);

        //}
        //internal void SetValue(T value, long d0, long d1, long d2, long d3, long d4, bool allowExpand = true) {

        //    if (ReferenceCount > 1) {
        //        if (!(this is CellStorage)) { // ReferenceEquals(value, this.m_retArray) || ReferenceCount != 2 ) {
        //            throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
        //        }
        //    }

        //    long seqIdx;
        //    try {
        //        seqIdx = this.Size.GetSeqIndex(d0, d1, d2, d3, d4) * SizeOfT;
        //    } catch (IndexOutOfRangeException) {
        //        if (!allowExpand) {
        //            throw;
        //        }
        //        Expand(new long[] { d0 + 1, d1 + 1, d2 + 1, d3 + 1, d4 + 1 });
        //        seqIdx = this.Size.GetSeqIndex(d0, d1, d2, d3, d4) * SizeOfT;
        //    }

        //    SetValueSeq(value, seqIdx);

        //}
        //internal void SetValue(T value, long d0, long d1, long d2, long d3, long d4, long d5, bool allowExpand = true) {

        //    if (ReferenceCount > 1) {
        //        if (!(this is CellStorage)) { // ReferenceEquals(value, this.m_retArray) || ReferenceCount != 2 ) {
        //            throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
        //        }
        //    }

        //    long seqIdx;
        //    try {
        //        seqIdx = this.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5) * SizeOfT;
        //    } catch (IndexOutOfRangeException) {
        //        if (!allowExpand) {
        //            throw;
        //        }
        //        Expand(new long[] { d0 + 1, d1 + 1, d2 + 1, d3 + 1, d4 + 1, d5 + 1 });
        //        seqIdx = this.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5) * SizeOfT;
        //    }

        //    SetValueSeq(value, seqIdx);

        //}
        //internal void SetValue(T value, long d0, long d1, long d2, long d3, long d4, long d5, long d6, bool allowExpand = true) {

        //    if (ReferenceCount > 1) {
        //        if (!(this is CellStorage)) { // ReferenceEquals(value, this.m_retArray) || ReferenceCount != 2 ) {
        //            throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
        //        }
        //    }

        //    long seqIdx;
        //    try {
        //        seqIdx = this.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6) * SizeOfT;
        //    } catch (IndexOutOfRangeException) {
        //        if (!allowExpand) {
        //            throw; 
        //        }
        //        Expand(new long[] { d0 + 1, d1 + 1, d2 + 1, d3 + 1, d4 + 1, d5 + 1, d6 + 1 });
        //        seqIdx = this.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6) * SizeOfT;
        //    }

        //    SetValueSeq(value, seqIdx);

        //}
        //internal void SetValue(T value, long[] dims, bool allowExpand = true) {

        //    if (ReferenceCount > 1) {
        //        if (!(this is CellStorage)) { // ReferenceEquals(value, this.m_retArray) || ReferenceCount != 2 ) {
        //            throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
        //        }
        //    }

        //    long seqIdx;
        //    try {
        //        seqIdx = this.Size.GetSeqIndex(dims) * SizeOfT;
        //    } catch (IndexOutOfRangeException) {
        //        if (!allowExpand) {
        //            throw;
        //        }
        //        using (Scope.Enter()) {

        //            Expand((MathInternal.vector(dims) + 1).GetArrayForRead());
        //            seqIdx = this.Size.GetSeqIndex(dims) * SizeOfT;
        //        }
        //    }

        //    SetValueSeq(value, seqIdx);

        //}
        //internal unsafe void SetValue(T value, Span<long> dims, bool allowExpand = true) {

        //    if (ReferenceCount > 1) {
        //        if (!(this is CellStorage)) { // ReferenceEquals(value, this.m_retArray) || ReferenceCount != 2 ) {
        //            throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
        //        }
        //    }

        //    long seqIdx;
        //    var pInd = Unsafe.AsPointer(ref dims[0]);   // we don't need fixed() because we know the indices are hosted + kept alive by caller ... :| !!!

        //    try {
        //        seqIdx = this.Size.GetSeqIndex((long*)pInd, dims.Length) * SizeOfT;
        //    } catch (IndexOutOfRangeException) {
        //        if (!allowExpand) {
        //            throw;
        //        }
        //        using (Scope.Enter()) {

        //            Expand((MathInternal.vector(dims) + 1).GetArrayForRead());
        //            seqIdx = this.Size.GetSeqIndex((long*)pInd, dims.Length) * SizeOfT;
        //        }
        //    }

        //    SetValueSeq(value, seqIdx);

        //}
        //internal void SetValue(T value, InArray<long> dims, bool allowExpand = true) {
        //    using (Scope.Enter(dims)) {

        //        if (ReferenceCount > 1) {
        //            if (!(this is CellStorage)) { // ReferenceEquals(value, this.m_retArray) || ReferenceCount != 2 ) {
        //                throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
        //            }
        //        }

        //        long seqIdx;
        //        try {
        //            seqIdx = this.Size.GetSeqIndex(dims) * SizeOfT;
        //        } catch (IndexOutOfRangeException) {
        //            if (!allowExpand) {
        //                throw;
        //            }

        //            Expand((dims + 1).GetArrayForRead());
        //            seqIdx = this.Size.GetSeqIndex(dims) * SizeOfT;

        //        }

        //        SetValueSeq(value, seqIdx);
        //    }
        //}
        //#endregion


    }
}
