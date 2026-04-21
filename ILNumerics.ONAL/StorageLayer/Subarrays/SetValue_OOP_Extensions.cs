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

    public static partial class ExtensionMethods {

        #region SetValue Out of-place, potentially expanding.
        
        internal static StorageT SetValue<T, LocalT, InT, OutT, RetT, StorageT>(
            this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT source, long d0, bool allowExpand = true)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            //if (storage.ReferenceCount > 1) {
            //    throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
            //}

            if (source.Size.NumberOfElements != 1) {
                throw new ArgumentException($"Addressing a single element for write access requires a scalar array as source value. Found: {source.Size.NumberOfElements} elements.");
            }
            long seqIdx;
            try {
                seqIdx = storage.Size.GetSeqIndex(d0);
            } catch (IndexOutOfRangeException) {
                if (!allowExpand) throw;
                storage = storage.Expand_OOP(new long[] { d0 + 1 });
                seqIdx = storage.Size.GetSeqIndex(d0);
            }
            // SetValueSeq already detaches, when required ... 
            //if (Handles.ReferenceCount > 1) {
            //    DetachBufferset();
            //}
            return storage.SetValueSeq_OOP(source.GetValue(0), seqIdx * BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT);
            //Handles[0].Set(source.Handles[0], (ulong)source.Size.BaseOffset, (ulong)seqIdx, BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT);
        }
        
        internal static StorageT SetValue<T, LocalT, InT, OutT, RetT, StorageT>(
            this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT source, long d0, long d1, bool allowExpand = true)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            //if (storage.ReferenceCount > 1) {
            //    throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
            //}
            if (source.Size.NumberOfElements != 1) {
                throw new ArgumentException($"Addressing a single element for write access requires a scalar array as source. Found: {source.Size.NumberOfElements} elements.");
            }

            long seqIdx;
            try {
                seqIdx = storage.Size.GetSeqIndex(d0, d1);
            } catch (IndexOutOfRangeException) {
                if (!allowExpand) throw;
                storage = storage.Expand_OOP(new long[] { d0 + 1, d1 + 1 });  // cannot use stackalloc here -> catch() block :( 
                seqIdx = storage.Size.GetSeqIndex(d0, d1);
            }
            // SetValueSeq already detaches, when required ... 
            //if (Handles.ReferenceCount > 1) {
            //    DetachBufferset();
            //}
            return storage.SetValueSeq_OOP(source.GetValue(0), seqIdx * BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT);
            //Handles[0].Set(source.Handles[0], (ulong)source.Size.BaseOffset, (ulong)seqIdx, BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT);
        }
        
        internal static StorageT SetValue<T, LocalT, InT, OutT, RetT, StorageT>(
            this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT source, long d0, long d1, long d2, bool allowExpand = true)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            if (source.Size.NumberOfElements != 1) {
                throw new ArgumentException($"Addressing a single element for write access requires a scalar array as source. Found: {source.Size.NumberOfElements} elements.");
            }
            //if (storage.ReferenceCount > 1) {
            //    throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
            //}

            long seqIdx;
            try {
                seqIdx = storage.Size.GetSeqIndex(d0, d1, d2);
            } catch (IndexOutOfRangeException) {
                if (!allowExpand) throw;
                storage = storage.Expand_OOP(new long[] { d0 + 1, d1 + 1, d2 + 1 });
                seqIdx = storage.Size.GetSeqIndex(d0, d1, d2);
            }
            // SetValueSeq already detaches, when required ... 
            //if (Handles.ReferenceCount > 1) {
            //    DetachBufferset();
            //}
            return storage.SetValueSeq_OOP(source.GetValue(0), seqIdx * BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT);
            //Handles[0].Set(source.Handles[0], (ulong)source.Size.BaseOffset, (ulong)seqIdx, BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT);
        }
        
        internal static StorageT SetValue<T, LocalT, InT, OutT, RetT, StorageT>(
            this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT source, long d0, long d1, long d2, long d3, bool allowExpand = true)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            if (source.Size.NumberOfElements != 1) {
                throw new ArgumentException($"Addressing a single element for write access requires a scalar array as source. Found: {source.Size.NumberOfElements} elements.");
            }
            //if (storage.ReferenceCount > 1) {
            //    throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
            //}

            long seqIdx;
            try {
                seqIdx = storage.Size.GetSeqIndex(d0, d1, d2, d3);
            } catch (IndexOutOfRangeException) {
                if (!allowExpand) throw;
                storage = storage.Expand_OOP(new long[] { d0 + 1, d1 + 1, d2 + 1, d3 + 1 });
                seqIdx = storage.Size.GetSeqIndex(d0, d1, d2, d3);
            }
            // SetValueSeq already detaches, when required ... 
            //if (Handles.ReferenceCount > 1) {
            //    DetachBufferset();
            //}
            return storage.SetValueSeq_OOP(source.GetValue(0), seqIdx * BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT);
            //Handles[0].Set(source.Handles[0], (ulong)source.Size.BaseOffset, (ulong)seqIdx, BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT);
        }
        
        internal static StorageT SetValue<T, LocalT, InT, OutT, RetT, StorageT>(
            this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT source, long d0, long d1, long d2, long d3, long d4, bool allowExpand = true)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {
            if (source.Size.NumberOfElements != 1) {
                throw new ArgumentException($"Addressing a single element for write access requires a scalar array as source. Found: {source.Size.NumberOfElements} elements.");
            }
            //if (storage.ReferenceCount > 1) {
            //    throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
            //}

            long seqIdx;
            try {
                seqIdx = storage.Size.GetSeqIndex(d0, d1, d2, d3, d4);
            } catch (IndexOutOfRangeException) {
                if (!allowExpand) throw;
                storage = storage.Expand_OOP(new long[] { d0 + 1, d1 + 1, d2 + 1, d3 + 1, d4 + 1 });
                seqIdx = storage.Size.GetSeqIndex(d0, d1, d2, d3, d4);
            }
            // SetValueSeq already detaches, when required ... 
            //if (Handles.ReferenceCount > 1) {
            //    DetachBufferset();
            //}
            return storage.SetValueSeq_OOP(source.GetValue(0), seqIdx * BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT);
            //Handles[0].Set(source.Handles[0], (ulong)source.Size.BaseOffset, (ulong)seqIdx, BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT);
        }
        
        internal static StorageT SetValue<T, LocalT, InT, OutT, RetT, StorageT>(
            this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT source, long d0, long d1, long d2, long d3, long d4, long d5, bool allowExpand = true)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {
            if (source.Size.NumberOfElements != 1) {
                throw new ArgumentException($"Addressing a single element for write access requires a scalar array as source. Found: {source.Size.NumberOfElements} elements.");
            }
            //if (storage.ReferenceCount > 1) {
            //    throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
            //}

            long seqIdx;
            try {
                seqIdx = storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5);
            } catch (IndexOutOfRangeException) {
                if (!allowExpand) throw;
                storage = storage.Expand_OOP(new long[] { d0 + 1, d1 + 1, d2 + 1, d3 + 1, d4 + 1, d5 + 1 });
                seqIdx = storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5);
            }
            // SetValueSeq already detaches, when required ... 
            //if (Handles.ReferenceCount > 1) {
            //    DetachBufferset();
            //}
            return storage.SetValueSeq_OOP(source.GetValue(0), seqIdx * BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT);
            //Handles[0].Set(source.Handles[0], (ulong)source.Size.BaseOffset, (ulong)seqIdx, BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT);
        }
        
        internal static StorageT SetValue<T, LocalT, InT, OutT, RetT, StorageT>(
            this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT source, long d0, long d1, long d2, long d3, long d4, long d5, long d6, bool allowExpand = true)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {
            if (source.Size.NumberOfElements != 1) {
                throw new ArgumentException($"Addressing a single element for write access requires a scalar array as source. Found: {source.Size.NumberOfElements} elements.");
            }
            //if (storage.ReferenceCount > 1) {
            //    throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
            //}

            long seqIdx;
            try {
                seqIdx = storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6);
            } catch (IndexOutOfRangeException) {
                if (!allowExpand) throw;
                storage = storage.Expand_OOP(new long[] { d0 + 1, d1 + 1, d2 + 1, d3 + 1, d4 + 1, d5 + 1, d6 + 1 });
                seqIdx = storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6);
            }
            // SetValueSeq already detaches, when required ... 
            //if (Handles.ReferenceCount > 1) {
            //    DetachBufferset();
            //}
            return storage.SetValueSeq_OOP(source.GetValue(0), seqIdx * BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT);
            //Handles[0].Set(source.Handles[0], (ulong)source.Size.BaseOffset, (ulong)seqIdx, BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT);
        }
        
        internal static StorageT SetValue<T, LocalT, InT, OutT, RetT, StorageT>(
            this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT source, long[] dims, bool allowExpand = true)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            if (source.Size.NumberOfElements != 1) {
                throw new ArgumentException($"Addressing a single element for write access requires a scalar array as source. Found: {source.Size.NumberOfElements} elements.");
            }
            //if (storage.ReferenceCount > 1) {
            //    throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
            //}

            long seqIdx;
            try {
                seqIdx = storage.Size.GetSeqIndex(dims);
            } catch (IndexOutOfRangeException) {
                if (!allowExpand) throw;
                using (Scope.Enter()) {

                    storage = storage.Expand_OOP((MathInternal.vector(dims) + 1).GetArrayForRead());
                    seqIdx = storage.Size.GetSeqIndex(dims);
                }
            }
            // SetValueSeq already detaches, when required ... 
            //if (Handles.ReferenceCount > 1) {
            //    DetachBufferset();
            //}
            return storage.SetValueSeq_OOP(source.GetValue(0), seqIdx * BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT);
            //Handles[0].Set(source.Handles[0], (ulong)source.Size.BaseOffset, (ulong)seqIdx, BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT);
        }
        /// <summary>
        /// Helper method delegating array (long*) indices to individual SetValue(d0,d1,..) dimensional indices. Used by Cells deep indexing. 
        /// </summary>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="indices"></param>
        /// <param name="lenIndices"></param>
        /// <param name="allowExpand"></param>
        internal unsafe static StorageT SetValue<T, LocalT, InT, OutT, RetT, StorageT>(
            this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, T value, long* indices, uint lenIndices, bool allowExpand = true)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            switch (lenIndices) {
                case 0:
                    if (storage.Size.NumberOfDimensions != 0) {
                        throw new IndexOutOfRangeException($"Indexing with empty indices is only allowed on 0-dimensional scalar arrays. Found: {storage.Size.ToString()}.");
                    }
                    return storage.SetValueSeq_OOP(value, storage.Size.BaseOffset * BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT);

                case 1:
                    return storage.SetValue(value, indices[0], allowExpand);

                case 2:
                    return storage.SetValue(value, indices[0], indices[1], allowExpand);

                case 3:
                    return storage.SetValue(value, indices[0], indices[1], indices[2], allowExpand);

                case 4:
                    return storage.SetValue(value, indices[0], indices[1], indices[2], indices[3], allowExpand);

                case 5:
                    return storage.SetValue(value, indices[0], indices[1], indices[2], indices[3], indices[4], allowExpand);

                case 6:
                    return storage.SetValue(value, indices[0], indices[1], indices[2], indices[3], indices[4], indices[5], allowExpand);

                case 7:
                    return storage.SetValue(value, indices[0], indices[1], indices[2], indices[3], indices[4], indices[5], indices[6], allowExpand);

                default:

                    //if (storage.ReferenceCount > 1) {
                    //    if (!(storage is CellStorage)) { // ReferenceEquals(value, this.m_retArray) || ReferenceCount != 2 ) {
                    //        throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
                    //    }
                    //}

                    long seqIdx;
                    try {
                        seqIdx = storage.Size.GetSeqIndex(indices, lenIndices) * BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT;
                    } catch (IndexOutOfRangeException) {
                        if (!allowExpand) {
                            throw;
                        }
                        storage = storage.Expand_OOP((((IntPtr)indices).AsArray<long>(lenIndices) + 1).GetArrayForRead());
                        seqIdx = storage.Size.GetSeqIndex(indices, lenIndices) * BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT;
                    }

                    return storage.SetValueSeq_OOP(value, seqIdx);
            }
        }
        
        internal unsafe static StorageT SetValue<T, LocalT, InT, OutT, RetT, StorageT>(
            this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, T value, long d0, bool allowExpand = true)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            //if (storage.ReferenceCount > 1) {
            //    if (!(storage is CellStorage)) { // ReferenceEquals(value, this.m_retArray) || ReferenceCount != 2 ) {
            //        throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
            //    }
            //}

            long seqIdx;
            try {
                seqIdx = storage.Size.GetSeqIndex(d0) * BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT;
            } catch (IndexOutOfRangeException) {
                if (!allowExpand) {
                    throw;
                }
                storage = storage.Expand_OOP(new long[] { d0 + 1 });
                seqIdx = storage.Size.GetSeqIndex(d0) * BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT;
            }
            return storage.SetValueSeq_OOP(value, seqIdx);
        }
        
        internal unsafe static StorageT SetValue<T, LocalT, InT, OutT, RetT, StorageT>(
            this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, T value, long d0, long d1, bool allowExpand = true)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            //if (storage.ReferenceCount > 1) {
            //    if (!(storage is CellStorage)) { // ReferenceEquals(value, this.m_retArray) || ReferenceCount != 2 ) {
            //        throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
            //    }
            //}

            long seqIdx;
            try {
                seqIdx = storage.Size.GetSeqIndex(d0, d1) * BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT;
            } catch (IndexOutOfRangeException) {
                if (!allowExpand) {
                    throw;
                }
                storage = storage.Expand_OOP(new long[] { d0 + 1, d1 + 1 });
                seqIdx = storage.Size.GetSeqIndex(d0, d1) * BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT;
            }

            return storage.SetValueSeq_OOP(value, seqIdx);

        }
        
        internal unsafe static StorageT SetValue<T, LocalT, InT, OutT, RetT, StorageT>(
            this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, T value, long d0, long d1, long d2, bool allowExpand = true)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            //if (storage.ReferenceCount > 1) {
            //    if (!(storage is CellStorage)) { // ReferenceEquals(value, this.m_retArray) || ReferenceCount != 2 ) {
            //        throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
            //    }
            //}

            long seqIdx;
            try {
                seqIdx = storage.Size.GetSeqIndex(d0, d1, d2) * BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT;
            } catch (IndexOutOfRangeException) {
                if (!allowExpand) {
                    throw;
                }
                storage = storage.Expand_OOP(new long[] { d0 + 1, d1 + 1, d2 + 1 });
                seqIdx = storage.Size.GetSeqIndex(d0, d1, d2) * BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT;
            }

            return storage.SetValueSeq_OOP(value, seqIdx);

        }
        
        internal unsafe static StorageT SetValue<T, LocalT, InT, OutT, RetT, StorageT>(
            this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, T value, long d0, long d1, long d2, long d3, bool allowExpand = true)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            //if (storage.ReferenceCount > 1) {
            //    if (!(storage is CellStorage)) { // ReferenceEquals(value, this.m_retArray) || ReferenceCount != 2 ) {
            //        throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
            //    }
            //}

            long seqIdx;
            try {
                seqIdx = storage.Size.GetSeqIndex(d0, d1, d2, d3) * BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT;
            } catch (IndexOutOfRangeException) {
                if (!allowExpand) {
                    throw;
                }
                storage = storage.Expand_OOP(new long[] { d0 + 1, d1 + 1, d2 + 1, d3 + 1 });
                seqIdx = storage.Size.GetSeqIndex(d0, d1, d2, d3) * BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT;
            }

            return storage.SetValueSeq_OOP(value, seqIdx);

        }
        
        internal unsafe static StorageT SetValue<T, LocalT, InT, OutT, RetT, StorageT>(
            this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, T value, long d0, long d1, long d2, long d3, long d4, bool allowExpand = true)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            //if (storage.ReferenceCount > 1) {
            //    if (!(storage is CellStorage)) { // ReferenceEquals(value, this.m_retArray) || ReferenceCount != 2 ) {
            //        throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
            //    }
            //}

            long seqIdx;
            try {
                seqIdx = storage.Size.GetSeqIndex(d0, d1, d2, d3, d4) * BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT;
            } catch (IndexOutOfRangeException) {
                if (!allowExpand) {
                    throw;
                }
                storage = storage.Expand_OOP(new long[] { d0 + 1, d1 + 1, d2 + 1, d3 + 1, d4 + 1 });
                seqIdx = storage.Size.GetSeqIndex(d0, d1, d2, d3, d4) * BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT;
            }

            return storage.SetValueSeq_OOP(value, seqIdx);

        }
        
        internal unsafe static StorageT SetValue<T, LocalT, InT, OutT, RetT, StorageT>(
            this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, T value, long d0, long d1, long d2, long d3, long d4, long d5, bool allowExpand = true)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            //if (storage.ReferenceCount > 1) {
            //    if (!(storage is CellStorage)) { // ReferenceEquals(value, this.m_retArray) || ReferenceCount != 2 ) {
            //        throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
            //    }
            //}

            long seqIdx;
            try {
                seqIdx = storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5) * BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT;
            } catch (IndexOutOfRangeException) {
                if (!allowExpand) {
                    throw;
                }
                storage = storage.Expand_OOP(new long[] { d0 + 1, d1 + 1, d2 + 1, d3 + 1, d4 + 1, d5 + 1 });
                seqIdx = storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5) * BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT;
            }

            return storage.SetValueSeq_OOP(value, seqIdx);

        }
        
        internal unsafe static StorageT SetValue<T, LocalT, InT, OutT, RetT, StorageT>(
            this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, T value, long d0, long d1, long d2, long d3, long d4, long d5, long d6, bool allowExpand = true)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            //if (storage.ReferenceCount > 1) {
            //    if (!(storage is CellStorage)) { // ReferenceEquals(value, this.m_retArray) || ReferenceCount != 2 ) {
            //        throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
            //    }
            //}

            long seqIdx;
            try {
                seqIdx = storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6) * BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT;
            } catch (IndexOutOfRangeException) {
                if (!allowExpand) {
                    throw;
                }
                storage = storage.Expand_OOP(new long[] { d0 + 1, d1 + 1, d2 + 1, d3 + 1, d4 + 1, d5 + 1, d6 + 1 });
                seqIdx = storage.Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6) * BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT;
            }

            return storage.SetValueSeq_OOP(value, seqIdx);

        }
        
        internal unsafe static StorageT SetValue<T, LocalT, InT, OutT, RetT, StorageT>(
            this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, T value, long[] dims, bool allowExpand = true)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            //if (storage.ReferenceCount > 1) {
            //    if (!(storage is CellStorage)) { // ReferenceEquals(value, this.m_retArray) || ReferenceCount != 2 ) {
            //        throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
            //    }
            //}

            long seqIdx;
            try {
                seqIdx = storage.Size.GetSeqIndex(dims) * BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT;
            } catch (IndexOutOfRangeException) {
                if (!allowExpand) {
                    throw;
                }
                using (Scope.Enter()) {

                    storage = storage.Expand_OOP((MathInternal.vector(dims) + 1).GetArrayForRead());
                    seqIdx = storage.Size.GetSeqIndex(dims) * BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT;
                }
            }

            return storage.SetValueSeq_OOP(value, seqIdx);

        }
        
        internal unsafe static StorageT SetValue<T, LocalT, InT, OutT, RetT, StorageT>(
            this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, T value, Span<long> dims, bool allowExpand = true)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            //if (storage.ReferenceCount > 1) {
            //    if (!(storage is CellStorage)) { // ReferenceEquals(value, this.m_retArray) || ReferenceCount != 2 ) {
            //        throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
            //    }
            //}

            long seqIdx;
            var pInd = Unsafe.AsPointer(ref dims[0]);   // we don't need fixed() because we know the indices are hosted + kept alive by caller ... :| !!!

            try {
                seqIdx = storage.Size.GetSeqIndex((long*)pInd, dims.Length) * BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT;
            } catch (IndexOutOfRangeException) {
                if (!allowExpand) {
                    throw;
                }
                using (Scope.Enter()) {

                    storage = storage.Expand_OOP((MathInternal.vector<long>(dims) + 1).GetArrayForRead());
                    seqIdx = storage.Size.GetSeqIndex((long*)pInd, dims.Length) * BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT;
                }
            }

            return storage.SetValueSeq_OOP(value, seqIdx);

        }
        
        internal unsafe static StorageT SetValue<T, LocalT, InT, OutT, RetT, StorageT>(
            this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, T value, InArray<long> dims, bool allowExpand = true)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            using (Scope.Enter(dims)) {

                //if (storage.ReferenceCount > 1) {
                //    if (!(storage is CellStorage)) { // ReferenceEquals(value, this.m_retArray) || ReferenceCount != 2 ) {
                //        throw new InvalidOperationException($"This storage is currently in use by more than one array and can not get altered.");
                //    }
                //}

                long seqIdx;
                try {
                    seqIdx = storage.Size.GetSeqIndex(dims) * BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT;
                } catch (IndexOutOfRangeException) {
                    if (!allowExpand) {
                        throw;
                    }

                    storage = storage.Expand_OOP((dims + 1).GetArrayForRead());
                    seqIdx = storage.Size.GetSeqIndex(dims) * BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT;

                }

                return storage.SetValueSeq_OOP(value, seqIdx);
            }
        }

        #endregion

        #region SetValue(CellStorage,...) overloads
        
        internal static CellStorage SetValue(this CellStorage storage, CellStorage source, long d0, bool allowExpand = true) {
            // incoming 'source' cell storage may is the 'value' from C[2] = C.

            // Here we decide what is the new "value" to be stored. Either the incoming cell itself 
            // or the value of its (only) element. Latter is detected by a flag set in implicit casts to RetCell. 

            // Ref counting + release: 
            // 'source' is released in caller, SetCellContentDirect stores incoming RetT directly. It stores a clone otherwise. So we give the RetT of a clone here!

            if (source.FromImplicitCast && source.Size.NumberOfElements == 1) {

                var cellVal = (source.Handles[0] as ManagedHostHandle<IStorage>)?.HostArray[source.Size.BaseOffset].GetBaseArrayClone();
                return (storage as IStorage).SetCellContentDirect(cellVal, _params<long>.Get(d0), 0, allowExpand: allowExpand) as CellStorage;

            } else {

                return (storage as IStorage).SetCellContentDirect((source as IStorage).GetBaseArrayClone(), _params<long>.Get(d0), 0, allowExpand: allowExpand) as CellStorage;
            }
        }

        
        internal static CellStorage SetValue(this CellStorage storage, CellStorage source, long d0, long d1, bool allowExpand = true) {

            // See remarks above: static CellStorage SetValue(this CellStorage storage, CellStorage source, long d0, bool allowExpand = true)
            if (source.FromImplicitCast && source.Size.NumberOfElements == 1) {

                var cellVal = (source.Handles[0] as ManagedHostHandle<IStorage>)?.HostArray[source.Size.BaseOffset].GetBaseArrayClone();
                return (storage as IStorage).SetCellContentDirect(cellVal, _params<long>.Get(d0, d1), 0, allowExpand: allowExpand) as CellStorage;

            } else {

                return (storage as IStorage).SetCellContentDirect((source as IStorage).GetBaseArrayClone(), _params<long>.Get(d0, d1), 0, allowExpand: allowExpand) as CellStorage;
            }
        }
        
        internal static CellStorage SetValue(this CellStorage storage, CellStorage source, long d0, long d1, long d2, bool allowExpand = true) {
            // incoming 'source' cell storage may is the 'value' from C[2] = C.

            // Here we decide what is the new "value" to be stored. Either the incoming cell itself 
            // or the value of its (only) element. Latter is detected by a flag set in implicit casts to RetCell. 

            // Ref counting + release: 
            // 'source' is released in caller, setting the value will clone again -> replace(). No need to clone here!

            if (source.FromImplicitCast && source.Size.NumberOfElements == 1) {

                var cellVal = (source.Handles[0] as ManagedHostHandle<IStorage>)?.HostArray[source.Size.BaseOffset].GetBaseArrayClone();
                return (storage as IStorage).SetCellContentDirect(cellVal, _params<long>.Get(d0, d1, d2), 0, allowExpand: allowExpand) as CellStorage;

            } else {
             
                return (storage as IStorage).SetCellContentDirect((source as IStorage).GetBaseArrayClone(), _params<long>.Get(d0, d1, d2), 0, allowExpand: allowExpand) as CellStorage;
            }
        }
        
        internal static CellStorage SetValue(this CellStorage storage, CellStorage source, long d0, long d1, long d2, long d3, bool allowExpand = true) {
            // incoming 'source' cell storage may is the 'value' from C[2] = C.

            // Here we decide what is the new "value" to be stored. Either the incoming cell itself 
            // or the value of its (only) element. Latter is detected by a flag set in implicit casts to RetCell. 

            // Ref counting + release: 
            // 'source' is released in caller, setting the value will clone again -> replace(). No need to clone here!

            if (source.FromImplicitCast && source.Size.NumberOfElements == 1) {

                var cellVal = (source.Handles[0] as ManagedHostHandle<IStorage>)?.HostArray[source.Size.BaseOffset].GetBaseArrayClone();
                return (storage as IStorage).SetCellContentDirect(cellVal, _params<long>.Get(d0, d1, d2, d3), 0, allowExpand: allowExpand) as CellStorage;

            } else {

                return (storage as IStorage).SetCellContentDirect((source as IStorage).GetBaseArrayClone(), _params<long>.Get(d0, d1, d2, d3), 0, allowExpand: allowExpand) as CellStorage;
            }
        }
        
        internal static CellStorage SetValue(this CellStorage storage, CellStorage source, long d0, long d1, long d2, long d3, long d4, bool allowExpand = true) {
            // incoming 'source' cell storage may is the 'value' from C[2] = C.

            // Here we decide what is the new "value" to be stored. Either the incoming cell itself 
            // or the value of its (only) element. Latter is detected by a flag set in implicit casts to RetCell. 

            // Ref counting + release: 
            // 'source' is released in caller, setting the value will clone again -> replace(). No need to clone here!

            if (source.FromImplicitCast && source.Size.NumberOfElements == 1) {

                var cellVal = (source.Handles[0] as ManagedHostHandle<IStorage>)?.HostArray[source.Size.BaseOffset].GetBaseArrayClone();
                return (storage as IStorage).SetCellContentDirect(cellVal, _params<long>.Get(d0, d1, d2, d3, d4), 0, allowExpand: allowExpand) as CellStorage;

            } else {

                return (storage as IStorage).SetCellContentDirect((source as IStorage).GetBaseArrayClone(), _params<long>.Get(d0, d1, d2, d3, d4), 0, allowExpand: allowExpand) as CellStorage;
            }
        }
        
        internal static CellStorage SetValue(this CellStorage storage, CellStorage source, long d0, long d1, long d2, long d3, long d4, long d5, bool allowExpand = true) {

            // incoming 'source' cell storage may is the 'value' from C[2] = C.

            // Here we decide what is the new "value" to be stored. Either the incoming cell itself 
            // or the value of its (only) element. Latter is detected by a flag set in implicit casts to RetCell. 

            // Ref counting + release: 
            // 'source' is released in caller, setting the value will clone again -> replace(). No need to clone here!

            if (source.FromImplicitCast && source.Size.NumberOfElements == 1) {

                var cellVal = (source.Handles[0] as ManagedHostHandle<IStorage>)?.HostArray[source.Size.BaseOffset].GetBaseArrayClone();
                return (storage as IStorage).SetCellContentDirect(cellVal, _params<long>.Get(d0, d1, d2, d3, d4, d5), 0, allowExpand: allowExpand) as CellStorage;

            } else {

                return (storage as IStorage).SetCellContentDirect((source as IStorage).GetBaseArrayClone(), _params<long>.Get(d0, d1, d2, d3, d4, d5), 0, allowExpand: allowExpand) as CellStorage;
            }
        }
        
        internal static CellStorage SetValue(this CellStorage storage, CellStorage source, long d0, long d1, long d2, long d3, long d4, long d5, long d6, bool allowExpand = true) {
            // incoming 'source' cell storage may is the 'value' from C[2] = C.

            // Here we decide what is the new "value" to be stored. Either the incoming cell itself 
            // or the value of its (only) element. Latter is detected by a flag set in implicit casts to RetCell. 

            // Ref counting + release: 
            // 'source' is released in caller, setting the value will clone again -> replace(). No need to clone here!

            if (source.FromImplicitCast && source.Size.NumberOfElements == 1) {

                var cellVal = (source.Handles[0] as ManagedHostHandle<IStorage>)?.HostArray[source.Size.BaseOffset].GetBaseArrayClone();
                return (storage as IStorage).SetCellContentDirect(cellVal, _params<long>.Get(d0, d1, d2, d3, d4, d5, d6), 0, allowExpand: allowExpand) as CellStorage;

            } else {

                return (storage as IStorage).SetCellContentDirect((source as IStorage).GetBaseArrayClone(), _params<long>.Get(d0, d1, d2, d3, d4, d5, d6), 0, allowExpand: allowExpand) as CellStorage;
            }
        }
        #endregion

    }
}
