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
using ILNumerics.Core.Arrays;
using System;
using System.Security;

namespace ILNumerics.Core.StorageLayer {
    public static partial class ExtensionMethods {

        /* Out of-place SetRange API, used by default on all public, mutating functions from non-accelerated code (synchronous, Debug mode etc.). **
         * 
         * Memory management: dimspecs are cleared inside the function here. The array where value is coming from is handled on the layer up, the caller. 
         * SetRange_ML is the Matlab-like implementation for mutable arrays. Value, however, is not guaranteed to be in ML style! 
         * 
         * This variant of SetRange supports removal and expansion of the existing array (in the same way Matlab does).
         * Reshape, detach, remove, expand, ensureStorageOrder ... all create a new storage. Exactly one new storage is to be created by this function and 
         * returned. It will provide atomicity when being Assign()-ed on the array caller layer. Note, that all storage creating sub-functions must work 
         * either in-place or out of-place, depending on whether or not the respective incoming storage is the entry storage (attachd to the outer array) 
         * or not. Otherwise, we would be creating multiple intermediate storages, releasing all of which would be problematic. 
         * 
         * As always for mutating functions: if this storage is referenced /used by other arrays or by pending operations this storage is Detach()ed first. 
         * Note, that storage = storage.Expand_OOP() and Remove() do not detach, since they create and work on a clone anyways! 
         * Within these extension methods, the original storage is not altered! Instead, a clone is made whenever a necessary change would harm integrity 
         * of the storage for concurrent reads (-> always). The clone is then populated and used in subsequent operations. 
         * 
         * Hence, when calling these functions from the array layer, the result returned is likely a new storage which must (/can be atomically) assigned 
         * to the array(s), effectively publishing the new storage. 
         * 
         */

        #region SetRange_ML DimSpec interface 


        /// <summary>
        /// SetRange, ML, Out of-place, DimSpec. Extension method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="LocalT"></typeparam>
        /// <typeparam name="InT"></typeparam>
        /// <typeparam name="OutT"></typeparam>
        /// <typeparam name="RetT"></typeparam>
        /// <typeparam name="StorageT"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="d0"></param>
        /// <returns>New storage, to be Assign()ed to the array layer.</returns>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ArgumentException"></exception>
         
        internal static unsafe StorageT SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT value, DimSpec d0)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            /* Objective: create EXACTLY ONE new storage. Perform all necessary operations on that storage and return it to caller in array layer.
             * It will then atomically be assigned to the array(s) of this storage, therefore replacing it atomically. 
             */
            
                //System.Diagnostics.Debug.Assert(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
                const uint HANDLES_NDIM = 1;
                #region handle ellipsis
                if (d0 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full);
                        case 2:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full);
                        case 3:
                                return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full, Globals.full);
                        case 4:
                                return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full, Globals.full, Globals.full);
                        case 5:
                                return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full);
                        case 6:
                                return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full);
                        case 7:
                                return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full);
                        default:
                            var tmp = BaseStorage< T, LocalT, InT, OutT, RetT, StorageT>.Context.DimSpecArray; tmp[0] = d0; d0 = null;
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, tmp, 1);
                    }
                }
                #endregion

                // does it fit - or do we have to reshape first? How large is end? 
                bool expanding = false;
                d0.EvaluateLeft((long)storage.Size.NumberOfElements - 1, ref expanding);
                if (expanding) {

                    if (value == null || value.Size.NumberOfElements == 0) {
                        throw new NotSupportedException("Combining removal and expansion in a single operation is not supported."); 
                    }
                    // Expand any specified dimensions, checks for special case (last dimension ambiguity)
#if NETFRAMEWORK
                    Span<long> ind = stackalloc long[] { d0.End + 1 };
                    storage = storage.Expand_OOP(ind);
#else
                    storage = storage.Expand_OOP(stackalloc long[] { d0.End + 1 });
#endif
                    // recall not needed! It would only cause trouble for expressions like: A[end + 1, ..,..,.] = ... !
                    //return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0);
                    //
                } else if (value == null || value.Size.NumberOfElements == 0) {
                    if (d0.Length == 0) {
                        return storage as StorageT; 
                    }
                    var iterators = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.IndexIteratorArray;
                    iterators[0] = d0;
                    storage = storage.Remove(iterators, HANDLES_NDIM);
                    return storage as StorageT; 
                }

                //if (d0.End >= storage.Size[0u]) {
                //    // current End does not fit into this dimension.
                //    // but in column major all will be fine 
                //    System.Diagnostics.Debug.Assert(d0.End <= (long)storage.Size.NumberOfElements);
                //    var colMajStor = storage.EnsureStorageOrder(StorageOrders.ColumnMajor);
                //    // clean up intermediate storage, if any
                //    if (storage.ReferenceCount == 0 && !object.ReferenceEquals(colMajStor, storage)) {
                //        storage.Retain(); storage.Release();
                //    }
                //    storage = colMajStor; 
                //}

                //if (storage.ReferenceCount > 1) {
                //    throw new InvalidOperationException("The storage of this array is shared with other arrays currently! It cannot be altered.");
                //} else if (storage.Handles.ReferenceCount > 1) {
                //    if (storage.ReferenceCount == 0) {  // TODO: twisted brainf..k -> this is not threadsafe, obviously. But concurrent readers must be
                //                                        // on non-accelerated code (as per spec.). They can access this storage only, when it was
                //                                        // exposed already. If it is an intermediate storage it was not exposed yet. The entry storage, 
                //                                        // however, can never have a RefCount = 0 here. So this is considered 'safe enough'. 
                //        storage.DetachBuffersetInplace(0);
                //    } else {
                //        storage = storage.DetachBufferset_OOP(targetDeviceID: 0) as StorageT;
                //    }
                //    // TODO: unneeded if "write to full" optimization kicks is
                //} ... all comment above replaced with: 
                checkPrepareSetRangeML(ref storage, d0, 0); 
                // fits inside this dimensions

                long* bsd = stackalloc long[(int)HANDLES_NDIM * 2 + 3];

                bsd[0] = (HANDLES_NDIM);
                bsd[1] = (d0.Length);
                bsd[2] = storage.Size.GetSeqIndex(d0.Start);
                bsd[3] = (d0.Length);
                bsd[3 + HANDLES_NDIM] = storage.Size.GetStride4MLlastDimExpansion(0) * d0.Step;

                if (value.Size.IsBroadcastableTo(bsd)) {
                    if (storage.Size.NumberOfElements == bsd[1] &&
                        storage.Size.NumberOfElements == value.Size.NumberOfElements && 
                        !(storage is CellStorage)) {
                        // 'writes to full' optimization: just use same handle from right side to replace left side completely.
                        // this  check is simple here because we have made sure that inSize broadcasts to outBSD - not the other way around! 
                        // Otherwise we may would run into cases like writing from a row vector to a same length column vector indexing, 
                        // which (may, commonly) triggers broadcasting in binary operators. But here, numel(left) == numel(right) 
                        // means: same shape.
                        // TODO: try to prevent from unneeded Detach() / EnsureStorageOrder() above

                        storage = storage.SetFullOptim(value);
                    } else {
                        // broadcasting assignment, includes the "all elements provided, same shape" case.
                        Core.Functions.Builtin.WriteToOperators.WriteTo_BSD<T>(value.m_handles[0], value.Size, storage.Handles[0], bsd, Storage<T>.SizeOfT);
                    }
                } else if (bsd[1] == value.Size.NumberOfElements) {
                    // traditional ML assignment: no broadcast, disregard right side shape. This may be visible by concurrent readers! 
                    storage.WriteTo_ML_BSDIter(value, bsd);
                } else {
                    throw new ArgumentException($"Cannot assign a right side {value.Size.ToString()} to range {Global.Helper.iterLengths2String(bsd)}. The array on the right side must either be broadcastable or have the same number of elements as the range addressed on the left side.");
                }
                return storage as StorageT;
        }


        /// <summary>
        /// SetRange, ML, Out of-place, DimSpec. Extension method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="LocalT"></typeparam>
        /// <typeparam name="InT"></typeparam>
        /// <typeparam name="OutT"></typeparam>
        /// <typeparam name="RetT"></typeparam>
        /// <typeparam name="StorageT"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="d0"></param>
        /// <param name="d1"></param>
        /// <returns>New storage, to be Assign()ed to the array layer.</returns>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ArgumentException"></exception>

        internal static unsafe StorageT SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT value, DimSpec d0, DimSpec d1)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

                //System.Diagnostics.Debug.Assert(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
                const uint HANDLES_NDIM = 2;
                #region handle ellipsis
                if (d0 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d1);
                            
                        case 2:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, d1);
                            
                        case 3:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full, d1);
                            
                        case 4:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full, Globals.full, d1);
                            
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full, Globals.full, Globals.full, d1);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, d1);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, d1);
                            
                        default:
                            var tmp = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; d1 = d0 = null;
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, tmp, 2);
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d1 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0);
                            
                        case 2:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full);
                            
                        case 3:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, Globals.full);
                            
                        case 4:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, Globals.full, Globals.full);
                            
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, Globals.full, Globals.full, Globals.full);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full);
                            
                        default:
                            var tmp = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; d1 = d0 = null;
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, tmp, 2);
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                #endregion

                // does it fit - or do we have to reshape first? How large is end? 
                bool expanding = false;
                long lastDimLen = storage.Size.GetLastDimIdxForMLSubarray(1);
                d0.EvaluateLeft(storage.Size[0L] - 1, ref expanding);
                d1.EvaluateLeft(lastDimLen, ref expanding);
                if (expanding) {
                    if (value == null || value.Size.NumberOfElements == 0) {
                        throw new NotSupportedException("Combining removal and expansion in a single operation is not supported.");
                    }
                    // Expand any specified dimensions, checks for special case (last dimension ambiguity)
#if NETFRAMEWORK
                    Span<long> ind = stackalloc long[] { d0.End + 1, d1.End + 1 }; 
                    storage = storage.Expand_OOP(ind);
#else
                    storage = storage.Expand_OOP(stackalloc long[] { d0.End + 1, d1.End + 1 });
#endif
                    // recall not needed! It would only cause trouble for expressions like: A[end + 1, ..,..,.] = ... !
                    //return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1);
                    //
                } else if (value == null || value.Size.NumberOfElements == 0) {
                    if (d0.Length * d1.Length == 0) {
                        return storage as StorageT; 
                    }

                    var iterators = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.IndexIteratorArray;
                    iterators[0] = d0;
                    iterators[1] = d1;
                    storage = storage.Remove(iterators, HANDLES_NDIM);
                    return storage as StorageT; 
                }
                checkPrepareSetRangeML(ref storage, d1, 1);
                // fits inside this dimensions

                long* bsd = stackalloc long[(int)HANDLES_NDIM * 2 + 3];

                bsd[0] = (HANDLES_NDIM);
                bsd[1] = (d0.Length * d1.Length);
                bsd[2] = storage.Size.GetSeqIndex(d0.Start, d1.Start);
                bsd[3] = (d0.Length);
                bsd[4] = (d1.Length);
                bsd[3 + HANDLES_NDIM] = storage.Size.GetStride(0) * d0.Step;
                bsd[4 + HANDLES_NDIM] = storage.Size.GetStride4MLlastDimExpansion(1) * d1.Step;

                if (value.Size.IsBroadcastableTo(bsd)) {
                    if (storage.Size.NumberOfElements == bsd[1] && 
                        storage.Size.NumberOfElements == value.Size.NumberOfElements &&
                        !(storage is CellStorage)) {
                        // 'writes to full' optimization: just use same handle from right side to replace left side completely.
                        // this  check is simple here because we have made sure that inSize broadcasts to outBSD - not the other way around! 
                        // Otherwise we may would run into cases like writing from a row vector to a same length column vector indexing, 
                        // which (may, commonly) triggers broadcasting in binary operators. But here, numel(left) == numel(right) 
                        // means: same shape.
                        // TODO: try to prevent from unneeded Detach() / EnsureStorageOrder() above

                        // like Assign, take strides but keep current size
                        storage = storage.SetFullOptim(value);
                    } else {
                        // broadcasting assignment, includes the "all elements provided, same shape" case.
                        Core.Functions.Builtin.WriteToOperators.WriteTo_BSD<T>(value.m_handles[0], value.Size, storage.m_handles[0], bsd, BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT);
                    }
                } else if (bsd[1] == value.Size.NumberOfElements) {
                    // traditional ML assignment: no broadcast, disregard right side shape
                    storage.WriteTo_ML_BSDIter(value, bsd);
                } else {
                    throw new ArgumentException($"The right side argument with size {value.Size.ToString()} is not suitable to be used in the assignment to range {Global.Helper.iterLengths2String(bsd)}. The array on the right side must either be broadcastable or have the same number of elements as the range addressed on the left side.");
                }
                return storage as StorageT;
        }

        /// <summary>
        /// SetRange, ML, Out of-place, DimSpec. Extension method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="LocalT"></typeparam>
        /// <typeparam name="InT"></typeparam>
        /// <typeparam name="OutT"></typeparam>
        /// <typeparam name="RetT"></typeparam>
        /// <typeparam name="StorageT"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="d0"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns>New storage, to be Assign()ed to the array layer.</returns>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ArgumentException"></exception>

        internal static unsafe StorageT SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT value, DimSpec d0, DimSpec d1, DimSpec d2)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

                //System.Diagnostics.Debug.Assert(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
                const uint HANDLES_NDIM = 3;
#region handle ellipsis
                if (d0 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d1, d2);
                            
                        case 3:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, d1, d2);
                            
                        case 4:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full, d1, d2);
                            
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full, Globals.full, d1, d2);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full, Globals.full, Globals.full, d1, d2);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, d1, d2);
                            
                        default:
                            var tmp = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; d2 = d1 = d0 = null;
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, tmp, 3);
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d1 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d2);
                            
                        case 3:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, d2);
                            
                        case 4:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, Globals.full, d2);
                            
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, Globals.full, Globals.full, d2);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, Globals.full, Globals.full, Globals.full, d2);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, d2);
                            
                        default:
                            var tmp = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; d2 = d1 = d0 = null;
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, tmp, 3);
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d2 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1);
                            
                        case 3:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, Globals.full);
                            
                        case 4:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, Globals.full, Globals.full);
                            
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, Globals.full, Globals.full, Globals.full);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, Globals.full, Globals.full, Globals.full, Globals.full);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full);
                            
                        default:
                            var tmp = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; d2 = d1 = d0 = null;
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, tmp, 3);
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
#endregion

                // does it fit - or do we have to reshape first? How large is end? 
                bool expanding = false;
                long lastDimLen = storage.Size.GetLastDimIdxForMLSubarray(HANDLES_NDIM - 1);
                d0.EvaluateLeft(storage.Size[0L] - 1, ref expanding);
                d1.EvaluateLeft(storage.Size[1L] - 1, ref expanding);
                d2.EvaluateLeft(lastDimLen, ref expanding);
                if (expanding) {
                    // Expand any specified dimensions, checks for special case (last dimension ambiguity)
                    if (value == null || value.Size.NumberOfElements == 0) {
                        throw new NotSupportedException("Combining removal and expansion in a single operation is not supported.");
                    }
#if NETFRAMEWORK
                    Span<long> ind = stackalloc long[] { d0.End + 1, d1.End + 1, d2.End + 1 };
                    storage = storage.Expand_OOP(ind);
#else
                    storage = storage.Expand_OOP(stackalloc long[] { d0.End + 1, d1.End + 1, d2.End + 1 });
#endif
                    // recall not needed! It would only cause trouble for expressions like: A[end + 1, ..,..,.] = ... !
                    //return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2);
                    //
                } else if (value == null || value.Size.NumberOfElements == 0) {
                    if (d0.Length * d1.Length * d2.Length == 0) {
                        return storage as StorageT;
                    }
                    var iterators = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.IndexIteratorArray;
                    iterators[0] = d0;
                    iterators[1] = d1; 
                    iterators[2] = d2;
                    storage = storage.Remove(iterators, HANDLES_NDIM);
                    return storage as StorageT;

                }

                checkPrepareSetRangeML(ref storage, d2, 2);

                // fits inside this dimensions

                long* bsd = stackalloc long[(int)HANDLES_NDIM * 2 + 3];

                bsd[0] = (HANDLES_NDIM);
                bsd[1] = (d0.Length * d1.Length * d2.Length);
                bsd[2] = storage.Size.GetSeqIndex(d0.Start, d1.Start, d2.Start);
                bsd[3] = (d0.Length);
                bsd[4] = (d1.Length);
                bsd[5] = (d2.Length);
                bsd[3 + HANDLES_NDIM] = storage.Size.GetStride(0) * d0.Step;
                bsd[4 + HANDLES_NDIM] = storage.Size.GetStride(1) * d1.Step;
                bsd[5 + HANDLES_NDIM] = storage.Size.GetStride4MLlastDimExpansion(2) * d2.Step;

                if (value.Size.IsBroadcastableTo(bsd)) {
                    if (storage.Size.NumberOfElements == bsd[1] &&
                        storage.Size.NumberOfElements == value.Size.NumberOfElements &&
                        !(storage is CellStorage)) {
                        // 'writes to full' optimization: just use same handle from right side to replace left side completely.
                        // this  check is simple here because we have made sure that inSize broadcasts to outBSD - not the other way around! 
                        // Otherwise we may would run into cases like writing from a row vector to a same length column vector indexing, 
                        // which (may, commonly) triggers broadcasting in binary operators. But here, numel(left) == numel(right) 
                        // means: same shape.
                        // TODO: try to prevent from unneeded Detach() / EnsureStorageOrder() above

                        storage = storage.SetFullOptim(value);
                    } else {
                        // broadcasting assignment, includes the "all elements provided, same shape" case.
                        Core.Functions.Builtin.WriteToOperators.WriteTo_BSD<T>(value.m_handles[0], value.Size, storage.m_handles[0], bsd, BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT);
                    }
                } else if (bsd[1] == value.Size.NumberOfElements) {
                    // traditional ML assignment: no broadcast, disregard right side shape
                    storage.WriteTo_ML_BSDIter(value, bsd);
                } else {
                    throw new ArgumentException($"The right side argument with size {value.Size.ToString()} is not suitable to be used in the assignment to range {Global.Helper.iterLengths2String(bsd)}. The array on the right side must either be broadcastable or have the same number of elements as the range addressed on the left side.");
                }
                return storage as StorageT;
        }

        /// <summary>
        /// SetRange, ML, Out of-place, DimSpec. Extension method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="LocalT"></typeparam>
        /// <typeparam name="InT"></typeparam>
        /// <typeparam name="OutT"></typeparam>
        /// <typeparam name="RetT"></typeparam>
        /// <typeparam name="StorageT"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="d0"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <param name="d3"></param>
        /// <returns>New storage, to be Assign()ed to the array layer.</returns>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ArgumentException"></exception>

        internal static unsafe StorageT SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT value, DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

                //System.Diagnostics.Debug.Assert(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
                const uint HANDLES_NDIM = 4;
#region handle ellipsis
                if (d0 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d1, d2, d3);
                            
                        case 4:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, d1, d2, d3);
                            
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full, d1, d2, d3);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full, Globals.full, d1, d2, d3);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full, Globals.full, Globals.full, d1, d2, d3);
                            
                        default:
                            var tmp = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, tmp, 4);
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d1 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d2, d3);
                            
                        case 4:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, d2, d3);
                            
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, Globals.full, d2, d3);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, Globals.full, Globals.full, d2, d3);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, Globals.full, Globals.full, Globals.full, d2, d3);
                            
                        default:
                            var tmp = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, tmp, 4);
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d2 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d3);
                            
                        case 4:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, Globals.full, d3);
                            
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, Globals.full, Globals.full, d3);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, Globals.full, Globals.full, Globals.full, d3);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, Globals.full, Globals.full, Globals.full, Globals.full, d3);
                            
                        default:
                            var tmp = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, tmp, 4);
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d3 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2);
                            
                        case 4:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, Globals.full);
                            
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, Globals.full, Globals.full);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, Globals.full, Globals.full, Globals.full);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, Globals.full, Globals.full, Globals.full, Globals.full);
                            
                        default:
                            var tmp = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, tmp, 4);
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
#endregion

                // does it fit - or do we have to reshape first? How large is end? 
                bool expanding = false;
                long lastDimLen = storage.Size.GetLastDimIdxForMLSubarray(HANDLES_NDIM - 1);
                d0.EvaluateLeft(storage.Size[0L] - 1, ref expanding);
                d1.EvaluateLeft(storage.Size[1L] - 1, ref expanding);
                d2.EvaluateLeft(storage.Size[2L] - 1, ref expanding);
                d3.EvaluateLeft(lastDimLen, ref expanding);
                if (expanding) {
                    if (value == null || value.Size.NumberOfElements == 0) {
                        throw new NotSupportedException("Combining removal and expansion in a single operation is not supported.");
                    }
                    // Expand any specified dimensions, checks for special case (last dimension ambiguity)
#if NETFRAMEWORK
                    Span<long> ind = stackalloc long[] { d0.End + 1, d1.End + 1, d2.End + 1, d3.End + 1 };
                    storage = storage.Expand_OOP(ind);
#else
                    storage = storage.Expand_OOP(stackalloc long[] { d0.End + 1, d1.End + 1, d2.End + 1, d3.End + 1 });
#endif
                    // recall not needed! It would only cause trouble for expressions like: A[end + 1, ..,..,.] = ... !
                    //return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3);
                    //
                } else if (value == null || value.Size.NumberOfElements == 0) {
                    if (d0.Length * d1.Length * d2.Length * d3.Length == 0) {
                        return storage as StorageT; 
                    }
                    var iterators = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.IndexIteratorArray;
                    iterators[0] = d0;
                    iterators[1] = d1;
                    iterators[2] = d2;
                    iterators[3] = d3;
                    storage = storage.Remove(iterators, HANDLES_NDIM);
                        return storage as StorageT;
                }

                checkPrepareSetRangeML(ref storage, d3, 3);

                // fits inside this dimensions

                long* bsd = stackalloc long[(int)HANDLES_NDIM * 2 + 3];

                bsd[0] = (HANDLES_NDIM);
                bsd[1] = (d0.Length * d1.Length * d2.Length * d3.Length);
                bsd[2] = storage.Size.GetSeqIndex(d0.Start, d1.Start, d2.Start, d3.Start);
                bsd[3] = (d0.Length);
                bsd[4] = (d1.Length);
                bsd[5] = (d2.Length);
                bsd[6] = (d3.Length);
                bsd[3 + HANDLES_NDIM] = storage.Size.GetStride(0) * d0.Step;
                bsd[4 + HANDLES_NDIM] = storage.Size.GetStride(1) * d1.Step;
                bsd[5 + HANDLES_NDIM] = storage.Size.GetStride(2) * d2.Step;
                bsd[6 + HANDLES_NDIM] = storage.Size.GetStride4MLlastDimExpansion(3) * d3.Step;

                if (value.Size.IsBroadcastableTo(bsd)) {
                    if (storage.Size.NumberOfElements == bsd[1] &&
                        storage.Size.NumberOfElements == value.Size.NumberOfElements &&
                        !(storage is CellStorage)) {
                        // 'writes to full' optimization: just use same handle from right side to replace left side completely.
                        // this  check is simple here because we have made sure that inSize broadcasts to outBSD - not the other way around! 
                        // Otherwise we may would run into cases like writing from a row vector to a same length column vector indexing, 
                        // which (may, commonly) triggers broadcasting in binary operators. But here, numel(left) == numel(right) 
                        // means: same shape.
                        // TODO: try to prevent from unneeded Detach() / EnsureStorageOrder() above

                        storage = storage.SetFullOptim(value);
                    } else {
                        // broadcasting assignment, includes the "all elements provided, same shape" case.
                        Core.Functions.Builtin.WriteToOperators.WriteTo_BSD<T>(value.m_handles[0], value.Size, storage.m_handles[0], bsd, BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT);
                    }
                } else if (bsd[1] == value.Size.NumberOfElements) {
                    // traditional ML assignment: no broadcast, disregard right side shape
                    storage.WriteTo_ML_BSDIter(value, bsd);
                } else {
                    throw new ArgumentException($"The right side argument with size {value.Size.ToString()} is not suitable to be used in the assignment to range {Global.Helper.iterLengths2String(bsd)}. The array on the right side must either be broadcastable or have the same number of elements as the range addressed on the left side.");
                }
                return storage as StorageT;
        }

        /// <summary>
        /// SetRange, ML, Out of-place, DimSpec. Extension method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="LocalT"></typeparam>
        /// <typeparam name="InT"></typeparam>
        /// <typeparam name="OutT"></typeparam>
        /// <typeparam name="RetT"></typeparam>
        /// <typeparam name="StorageT"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="d0"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <param name="d3"></param>
        /// <param name="d4"></param>
        /// <returns>New storage, to be Assign()ed to the array layer.</returns>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ArgumentException"></exception>

        internal static unsafe StorageT SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT value, DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3, DimSpec d4)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

                //System.Diagnostics.Debug.Assert(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
                const uint HANDLES_NDIM = 5;
#region handle ellipsis
                if (d0 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d1, d2, d3, d4);
                            
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, d1, d2, d3, d4);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full, d1, d2, d3, d4);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full, Globals.full, d1, d2, d3, d4);
                            
                        default:
                            var tmp = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, tmp, 5);
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d1 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d2, d3, d4);
                            
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, d2, d3, d4);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, Globals.full, d2, d3, d4);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, Globals.full, Globals.full, d2, d3, d4);
                            
                        default:
                            var tmp = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, tmp, 5);
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d2 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d3, d4);
                            
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, Globals.full, d3, d4);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, Globals.full, Globals.full, d3, d4);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, Globals.full, Globals.full, Globals.full, d3, d4);
                            
                        default:
                            var tmp = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, tmp, 5);
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d3 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d4);
                            
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2,Globals.full, d4);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2,Globals.full, Globals.full, d4);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2,Globals.full, Globals.full, Globals.full, d4);
                            
                        default:
                            var tmp = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, tmp, 5);
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d4 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3);
                            
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, Globals.full);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, Globals.full, Globals.full);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, Globals.full, Globals.full, Globals.full);
                            
                        default:
                            var tmp = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, tmp, 5);
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
#endregion

                // does it fit - or do we have to reshape first? How large is end? 
                bool expanding = false;
                long lastDimLen = storage.Size.GetLastDimIdxForMLSubarray(HANDLES_NDIM - 1);
                d0.EvaluateLeft(storage.Size[0L] - 1, ref expanding);
                d1.EvaluateLeft(storage.Size[1L] - 1, ref expanding);
                d2.EvaluateLeft(storage.Size[2L] - 1, ref expanding);
                d3.EvaluateLeft(storage.Size[3L] - 1, ref expanding);
                d4.EvaluateLeft(lastDimLen, ref expanding);
                if (expanding) {
                    if (value == null || value.Size.NumberOfElements == 0) {
                        throw new NotSupportedException("Combining removal and expansion in a single operation is not supported.");
                    }
                    // Expand any specified dimensions, checks for special case (last dimension ambiguity)
#if NETFRAMEWORK
                    Span<long> ind = stackalloc long[] { d0.End + 1, d1.End + 1, d2.End + 1, d3.End + 1, d4.End + 1 };
                    storage = storage.Expand_OOP(ind);
#else
                    storage = storage.Expand_OOP(stackalloc long[] { d0.End + 1, d1.End + 1, d2.End + 1, d3.End + 1, d4.End + 1 });
#endif
                    // recall not needed! It would only cause trouble for expressions like: A[end + 1, ..,..,.] = ... !
                    //return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, d4);
                    //
                } else if (value == null || value.Size.NumberOfElements == 0) {
                    if (d0.Length * d1.Length * d2.Length * d3.Length * d4.Length == 0) {
                        return storage as StorageT;
                    }
                    var iterators = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.IndexIteratorArray;
                    iterators[0] = d0;
                    iterators[1] = d1;
                    iterators[2] = d2;
                    iterators[3] = d3;
                    iterators[4] = d4;
                    storage = storage.Remove(iterators, HANDLES_NDIM);
                    return storage as StorageT;
                }

                checkPrepareSetRangeML(ref storage, d4, 4);

                // fits inside this dimensions

                long* bsd = stackalloc long[(int)HANDLES_NDIM * 2 + 3];

                bsd[0] = (HANDLES_NDIM);
                bsd[1] = (d0.Length * d1.Length * d2.Length * d3.Length * d4.Length);
                bsd[2] = storage.Size.GetSeqIndex(d0.Start, d1.Start, d2.Start, d3.Start, d4.Start);
                bsd[3] = (d0.Length);
                bsd[4] = (d1.Length);
                bsd[5] = (d2.Length);
                bsd[6] = (d3.Length);
                bsd[7] = (d4.Length);
                bsd[3 + HANDLES_NDIM] = storage.Size.GetStride(0) * d0.Step;
                bsd[4 + HANDLES_NDIM] = storage.Size.GetStride(1) * d1.Step;
                bsd[5 + HANDLES_NDIM] = storage.Size.GetStride(2) * d2.Step;
                bsd[6 + HANDLES_NDIM] = storage.Size.GetStride(3) * d3.Step;
                bsd[7 + HANDLES_NDIM] = storage.Size.GetStride4MLlastDimExpansion(4) * d4.Step;

                if (value.Size.IsBroadcastableTo(bsd)) {
                    if (storage.Size.NumberOfElements == bsd[1] &&
                        storage.Size.NumberOfElements == value.Size.NumberOfElements &&
                        !(storage is CellStorage)) {
                        // 'writes to full' optimization: just use same handle from right side to replace left side completely.
                        // this  check is simple here because we have made sure that inSize broadcasts to outBSD - not the other way around! 
                        // Otherwise we may would run into cases like writing from a row vector to a same length column vector indexing, 
                        // which (may, commonly) triggers broadcasting in binary operators. But here, numel(left) == numel(right) 
                        // means: same shape.
                        // TODO: try to prevent from unneeded Detach() / EnsureStorageOrder() above

                        storage = storage.SetFullOptim(value);
                    } else {
                        // broadcasting assignment, includes the "all elements provided, same shape" case.
                        Core.Functions.Builtin.WriteToOperators.WriteTo_BSD<T>(value.m_handles[0], value.Size, storage.m_handles[0], bsd, BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT);
                    }
                } else if (bsd[1] == value.Size.NumberOfElements) {
                    // traditional ML assignment: no broadcast, disregard right side shape
                    storage.WriteTo_ML_BSDIter(value, bsd);
                } else {
                    throw new ArgumentException($"The right side argument with size {value.Size.ToString()} is not suitable to be used in the assignment to range {Global.Helper.iterLengths2String(bsd)}. The array on the right side must either be broadcastable or have the same number of elements as the range addressed on the left side.");
                }
                return storage as StorageT;
        }

        /// <summary>
        /// SetRange, ML, Out of-place, DimSpec. Extension method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="LocalT"></typeparam>
        /// <typeparam name="InT"></typeparam>
        /// <typeparam name="OutT"></typeparam>
        /// <typeparam name="RetT"></typeparam>
        /// <typeparam name="StorageT"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="d0"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <param name="d3"></param>
        /// <param name="d4"></param>
        /// <param name="d5"></param>
        /// <returns>New storage, to be Assign()ed to the array layer.</returns>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ArgumentException"></exception>

        internal static unsafe StorageT SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT value, DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3, DimSpec d4, DimSpec d5)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

                //System.Diagnostics.Debug.Assert(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
                const uint HANDLES_NDIM = 6;
#region handle ellipsis
                if (d0 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d1, d2, d3, d4, d5);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, d1, d2, d3, d4, d5);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full, d1, d2, d3, d4, d5);
                            
                        default:
                            var tmp = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; d5 = d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, tmp, 6);
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d1 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d2, d3, d4, d5);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, d2, d3, d4, d5);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, Globals.full, d2, d3, d4, d5);
                            
                        default:
                            var tmp = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; d5 = d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, tmp, 6);
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d2 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d3, d4, d5);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, Globals.full, d3, d4, d5);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, Globals.full, Globals.full, d3, d4, d5);
                            
                        default:
                            var tmp = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; d5 = d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, tmp, 6);
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d3 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d4, d5);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, Globals.full, d4, d5);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, Globals.full, Globals.full, d4, d5);
                            
                        default:
                            var tmp = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; d5 = d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, tmp, 6);
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d4 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, d5);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, Globals.full, d5);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, Globals.full, Globals.full, d5);
                            
                        default:
                            var tmp = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; d5 = d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, tmp, 6);
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d5 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, d4);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, d4, Globals.full);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, d4, Globals.full, Globals.full);
                            
                        default:
                            var tmp = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; d5 = d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, tmp, 6);
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
#endregion

                // does it fit - or do we have to reshape first? How large is end? 
                bool expanding = false;
                long lastDimLen = storage.Size.GetLastDimIdxForMLSubarray(HANDLES_NDIM - 1);
                d0.EvaluateLeft(storage.Size[0L] - 1, ref expanding);
                d1.EvaluateLeft(storage.Size[1L] - 1, ref expanding);
                d2.EvaluateLeft(storage.Size[2L] - 1, ref expanding);
                d3.EvaluateLeft(storage.Size[3L] - 1, ref expanding);
                d4.EvaluateLeft(storage.Size[4L] - 1, ref expanding);
                d5.EvaluateLeft(lastDimLen, ref expanding);
                if (expanding) {
                    if (value == null || value.Size.NumberOfElements == 0) {
                        throw new NotSupportedException("Combining removal and expansion in a single operation is not supported.");
                    }
                    // Expand any specified dimensions, checks for special case (last dimension ambiguity)
#if NETFRAMEWORK
                    Span<long> ind = stackalloc long[] { d0.End + 1, d1.End + 1, d2.End + 1, d3.End + 1, d4.End + 1, d5.End + 1 };
                    storage = storage.Expand_OOP(ind);
#else
                    storage = storage.Expand_OOP(stackalloc long[] { d0.End + 1, d1.End + 1, d2.End + 1, d3.End + 1, d4.End + 1, d5.End + 1 });
#endif
                    // recall not needed! It would only cause trouble for expressions like: A[end + 1, ..,..,.] = ... !
                    //return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, d4, d5);
                    //
                } else if (value == null || value.Size.NumberOfElements == 0) {
                    if (d0.Length * d1.Length * d2.Length * d3.Length * d4.Length * d5.Length == 0) {
                        return storage as StorageT;
                    }
                    var iterators = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.IndexIteratorArray;
                    iterators[0] = d0;
                    iterators[1] = d1;
                    iterators[2] = d2;
                    iterators[3] = d3;
                    iterators[4] = d4;
                    iterators[5] = d5;
                    storage = storage.Remove(iterators, HANDLES_NDIM);
                    return storage as StorageT;
                }

                checkPrepareSetRangeML(ref storage, d5, 5);

                // fits inside this dimensions

                long* bsd = stackalloc long[(int)HANDLES_NDIM * 2 + 3];

                bsd[0] = (HANDLES_NDIM);
                bsd[1] = (d0.Length * d1.Length * d2.Length * d3.Length * d4.Length * d5.Length);
                bsd[2] = storage.Size.GetSeqIndex(d0.Start, d1.Start, d2.Start, d3.Start, d4.Start, d5.Start);
                bsd[3] = (d0.Length);
                bsd[4] = (d1.Length);
                bsd[5] = (d2.Length);
                bsd[6] = (d3.Length);
                bsd[7] = (d4.Length);
                bsd[8] = (d5.Length);
                bsd[3 + HANDLES_NDIM] = storage.Size.GetStride(0) * d0.Step;
                bsd[4 + HANDLES_NDIM] = storage.Size.GetStride(1) * d1.Step;
                bsd[5 + HANDLES_NDIM] = storage.Size.GetStride(2) * d2.Step;
                bsd[6 + HANDLES_NDIM] = storage.Size.GetStride(3) * d3.Step;
                bsd[7 + HANDLES_NDIM] = storage.Size.GetStride(4) * d4.Step;
                bsd[8 + HANDLES_NDIM] = storage.Size.GetStride4MLlastDimExpansion(5) * d5.Step;

                if (value.Size.IsBroadcastableTo(bsd)) {
                    if (storage.Size.NumberOfElements == bsd[1] &&
                        storage.Size.NumberOfElements == value.Size.NumberOfElements &&
                        !(storage is CellStorage)) {
                        // 'writes to full' optimization: just use same handle from right side to replace left side completely.
                        // this  check is simple here because we have made sure that inSize broadcasts to outBSD - not the other way around! 
                        // Otherwise we may would run into cases like writing from a row vector to a same length column vector indexing, 
                        // which (may, commonly) triggers broadcasting in binary operators. But here, numel(left) == numel(right) 
                        // means: same shape.
                        // TODO: try to prevent from unneeded Detach() / EnsureStorageOrder() above

                        storage = storage.SetFullOptim(value);
                    } else {
                        // broadcasting assignment, includes the "all elements provided, same shape" case.
                        Core.Functions.Builtin.WriteToOperators.WriteTo_BSD<T>(value.m_handles[0], value.Size, storage.m_handles[0], bsd, BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT);
                    }
                } else if (bsd[1] == value.Size.NumberOfElements) {
                    // traditional ML assignment: no broadcast, disregard right side shape
                    storage.WriteTo_ML_BSDIter(value, bsd);
                } else {
                    throw new ArgumentException($"The right side argument with size {value.Size.ToString()} is not suitable to be used in the assignment to range {Global.Helper.iterLengths2String(bsd)}. The array on the right side must either be broadcastable or have the same number of elements as the range addressed on the left side.");
                }
                return storage as StorageT;
        }

        /// <summary>
        /// SetRange, ML, Out of-place, DimSpec. Extension method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="LocalT"></typeparam>
        /// <typeparam name="InT"></typeparam>
        /// <typeparam name="OutT"></typeparam>
        /// <typeparam name="RetT"></typeparam>
        /// <typeparam name="StorageT"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="d0"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <param name="d3"></param>
        /// <param name="d4"></param>
        /// <param name="d5"></param>
        /// <param name="d6"></param>
        /// <returns>New storage, to be Assign()ed to the array layer.</returns>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ArgumentException"></exception>

        internal static unsafe StorageT SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT value, DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3, DimSpec d4, DimSpec d5, DimSpec d6)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

                //System.Diagnostics.Debug.Assert(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
                const uint HANDLES_NDIM = 7;
#region handle ellipsis
                if (d0 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d1, d2, d3, d4, d5, d6);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, d1, d2, d3, d4, d5, d6);
                            
                        default:
                            var tmp = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6; d6 = d5 = d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, tmp, 7);
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d1 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d2, d3, d4, d5, d6);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, d2, d3, d4, d5, d6);
                            
                        default:
                            var tmp = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6; d6 = d5 = d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, tmp, 7);
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d2 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d3, d4, d5, d6);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, Globals.full, d3, d4, d5, d6);
                            
                        default:
                            var tmp = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6; d6 = d5 = d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, tmp, 7);
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d3 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d4, d5, d6);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, Globals.full, d4, d5, d6);
                            
                        default:
                            var tmp = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6; d6 = d5 = d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, tmp, 7);
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d4 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, d5, d6);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, Globals.full, d5, d6);
                            
                        default:
                            var tmp = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6; d6 = d5 = d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, tmp, 7);
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d5 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, d4, d6);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, d4, Globals.full, d6);
                            
                        default:
                            var tmp = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6; d6 = d5 = d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, tmp, 7);
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d6 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, d4, d5);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, d4, d5, Globals.full);
                            
                        default:
                            var tmp = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6; d6 = d5 = d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, tmp, 7);
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
#endregion

                // does it fit - or do we have to reshape first? How large is end? 
                bool expanding = false;
                long lastDimLen = storage.Size.GetLastDimIdxForMLSubarray(HANDLES_NDIM - 1);
                d0.EvaluateLeft(storage.Size[0L] - 1, ref expanding);
                d1.EvaluateLeft(storage.Size[1L] - 1, ref expanding);
                d2.EvaluateLeft(storage.Size[2L] - 1, ref expanding);
                d3.EvaluateLeft(storage.Size[3L] - 1, ref expanding);
                d4.EvaluateLeft(storage.Size[4L] - 1, ref expanding);
                d5.EvaluateLeft(storage.Size[5L] - 1, ref expanding);
                d6.EvaluateLeft(lastDimLen, ref expanding);
                if (expanding) {
                    if (value == null || value.Size.NumberOfElements == 0) {
                        throw new NotSupportedException("Combining removal and expansion in a single operation is not supported.");
                    }
                    // Expand any specified dimensions, checks for special case (last dimension ambiguity)
#if NETFRAMEWORK
                    Span<long> ind = stackalloc long[] { d0.End + 1, d1.End + 1, d2.End + 1, d3.End + 1, d4.End + 1, d5.End + 1, d6.End + 1 };
                    storage = storage.Expand_OOP(ind);
#else
                    storage = storage.Expand_OOP(stackalloc long[] { d0.End + 1, d1.End + 1, d2.End + 1, d3.End + 1, d4.End + 1, d5.End + 1, d6.End + 1 });
#endif
                    // recall not needed! It would only cause trouble for expressions like: A[end + 1, ..,..,.] = ... !
                    //return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, d4, d5, d6);
                    //
                } else if (value == null || value.Size.NumberOfElements == 0) {
                    if (d0.Length * d1.Length * d2.Length * d3.Length * d4.Length * d5.Length * d6.Length == 0) {
                        return storage as StorageT;
                    }
                    var iterators = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.IndexIteratorArray;
                    iterators[0] = d0;
                    iterators[1] = d1;
                    iterators[2] = d2;
                    iterators[3] = d3;
                    iterators[4] = d4;
                    iterators[5] = d5;
                    iterators[6] = d6;
                    storage = storage.Remove(iterators, HANDLES_NDIM);
                    return storage as StorageT;
                }

                checkPrepareSetRangeML(ref storage, d6, 6);

                // fits inside this dimensions

                long* bsd = stackalloc long[(int)HANDLES_NDIM * 2 + 3];

                bsd[0] = (HANDLES_NDIM);
                bsd[1] = (d0.Length * d1.Length * d2.Length * d3.Length * d4.Length * d5.Length * d6.Length);
                bsd[2] = storage.Size.GetSeqIndex(d0.Start, d1.Start, d2.Start, d3.Start, d4.Start, d5.Start, d6.Start);
                bsd[3] = (d0.Length);
                bsd[4] = (d1.Length);
                bsd[5] = (d2.Length);
                bsd[6] = (d3.Length);
                bsd[7] = (d4.Length);
                bsd[8] = (d5.Length);
                bsd[9] = (d6.Length);
                bsd[3 + HANDLES_NDIM] = storage.Size.GetStride(0) * d0.Step;
                bsd[4 + HANDLES_NDIM] = storage.Size.GetStride(1) * d1.Step;
                bsd[5 + HANDLES_NDIM] = storage.Size.GetStride(2) * d2.Step;
                bsd[6 + HANDLES_NDIM] = storage.Size.GetStride(3) * d3.Step;
                bsd[7 + HANDLES_NDIM] = storage.Size.GetStride(4) * d4.Step;
                bsd[8 + HANDLES_NDIM] = storage.Size.GetStride(5) * d5.Step;
                bsd[9 + HANDLES_NDIM] = storage.Size.GetStride4MLlastDimExpansion(6) * d6.Step;

                if (value.Size.IsBroadcastableTo(bsd)) {
                    if (storage.Size.NumberOfElements == bsd[1] &&
                        storage.Size.NumberOfElements == value.Size.NumberOfElements &&
                        !(storage is CellStorage)) {
                        // 'writes to full' optimization: just use same handle from right side to replace left side completely.
                        // this  check is simple here because we have made sure that inSize broadcasts to outBSD - not the other way around! 
                        // Otherwise we may would run into cases like writing from a row vector to a same length column vector indexing, 
                        // which (may, commonly) triggers broadcasting in binary operators. But here, numel(left) == numel(right) 
                        // means: same shape.
                        // TODO: try to prevent from unneeded Detach() / EnsureStorageOrder() above
                        storage = storage.SetFullOptim(value);   // performs synchronously

                    } else {
                        // broadcasting assignment, includes the "all elements provided, same shape" case.
                        Core.Functions.Builtin.WriteToOperators.WriteTo_BSD<T>(value.m_handles[0], value.Size, storage.m_handles[0], bsd, BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT);
                    }
                } else if (bsd[1] == value.Size.NumberOfElements) {
                    // traditional ML assignment: no broadcast, disregard right side shape
                    storage.WriteTo_ML_BSDIter(value, bsd);
                } else {
                    throw new ArgumentException($"The right side argument with size {value.Size.ToString()} is not suitable to be used in the assignment to range {Global.Helper.iterLengths2String(bsd)}. The array on the right side must either be broadcastable or have the same number of elements as the range addressed on the left side.");
                }
                return storage as StorageT;
        }

        /// <summary>
        /// SetRange, ML, Out of-place, DimSpec. Extension method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="LocalT"></typeparam>
        /// <typeparam name="InT"></typeparam>
        /// <typeparam name="OutT"></typeparam>
        /// <typeparam name="RetT"></typeparam>
        /// <typeparam name="StorageT"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="dims"></param>
        /// <param name="len"></param>
        /// <returns>New storage, to be Assign()ed to the array layer.</returns>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ArgumentException"></exception>

        internal static unsafe StorageT SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT value, DimSpec[] dims, uint? len = null)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {
                // this is used by Accelerator asynch segments DimSpec[] and long[>8]... 
            if (dims == null || dims.Length == 0) {
                return storage as StorageT; 
            }
            uint HANDLES_NDIM = len ?? (uint)dims.Length;
            if (storage.Size.NumberOfDimensions <= 7) {
                switch (HANDLES_NDIM) {
                    case 1:
                        return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, dims[0]);
                        
                    case 2:
                        return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, dims[0], dims[1]);
                        
                    case 3:
                        return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, dims[0], dims[1], dims[2]);
                        
                    case 4:
                        return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, dims[0], dims[1], dims[2], dims[3]);
                        
                    case 5:
                        return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, dims[0], dims[1], dims[2], dims[3], dims[4]);
                        
                    case 6:
                        return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, dims[0], dims[1], dims[2], dims[3], dims[4], dims[5]);
                        
                    case 7:
                        return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, dims[0], dims[1], dims[2], dims[3], dims[4], dims[5], dims[6]);
                        
                    default:
                        //throw new ArgumentOutOfRangeException($"The maximum number of dimensions in an array is: '{nameof(storage.Size)}.{nameof(storage.Size.MaxNumberOfDimensions)}={storage.Size.MaxNumberOfDimensions}'");
                        break;
                }
            }
                //System.Diagnostics.Debug.Assert(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
#region handle ellipsis
                int i = 0;
                // ML does not support newaxis. This eases the ellipsis substitution here.
                for (; i < HANDLES_NDIM; i++) {
                    if (object.Equals(dims[i],null)) {
                        throw new ArgumentNullException($"Index/range specifier for dimension #{i} is null. Note that dimension specifiers for subarray operations are valid for exactly one operation and cannot be reused! When using an array of dimension specifiers make sure to rebuild the full array before each operation!"); 
                    }
                    if (dims[i] is EllipsisSpec) {
                        BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.substituteEllipsis(dims, BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.DimSpecArray, ref HANDLES_NDIM, storage.Size.NumberOfDimensions, i);
                        dims = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.DimSpecArray;
                        return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, dims, HANDLES_NDIM);
                         
                    }
                }
#endregion

                // does it fit - or do we have to reshape first? How large is end? 
                bool expanding = false;
                long lastDimLen = storage.Size.GetLastDimIdxForMLSubarray(HANDLES_NDIM - 1);
                long nrElements = 1; 
                for (i = 0; i < HANDLES_NDIM - 1; i++) {
                    dims[i]?.EvaluateLeft(storage.Size[i] - 1, ref expanding);
                    nrElements *= dims[i].Length; 
                }
                dims[i]?.EvaluateLeft(lastDimLen, ref expanding);
                nrElements *= dims[i].Length; 
                if (expanding) {
                    if (value == null || value.Size.NumberOfElements == 0) {
                        throw new NotSupportedException("Combining removal and expansion in a single operation is not supported.");
                    }
                    // Expand any specified dimensions, checks for special case (last dimension ambiguity)
                    long[] tmp = new long[HANDLES_NDIM];
                    for (i = 0; i < HANDLES_NDIM; i++) {
                        tmp[i] = dims[i].End + 1; 
                    }
                    storage = storage.Expand_OOP(tmp);
                    // recall not needed! It would only cause trouble for expressions like: A[end + 1, ..,..,.] = ... !
                    //return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, d4);
                    //
                } else if (value == null || value.Size.NumberOfElements == 0) {
                    if (nrElements == 0) {
                        return storage as StorageT; 
                    }
                    var iterators = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.IndexIteratorArray;
                    for (i = 0; i < HANDLES_NDIM; i++) {
                        iterators[i] = dims[i];
                    }
                    storage = storage.Remove(iterators, HANDLES_NDIM);
                    return storage as StorageT; 
                    
                }

                checkPrepareSetRangeML(ref storage, dims[HANDLES_NDIM - 1], HANDLES_NDIM - 1);

                //long* bsd = stackalloc long[(int)HANDLES_NDIM * 2 + 3];

                // fits inside this dimensions
                long* bsd = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.TmpBuffer1000;
                long* starts = bsd + HANDLES_NDIM * 2 + 3; 
                bsd[0] = HANDLES_NDIM;
                bsd[1] = nrElements;
                    
                for (i = 0; i < HANDLES_NDIM - 1; i++) {
                    bsd[3 + i] = dims[i].Length;
                    bsd[3 + HANDLES_NDIM + i] = storage.Size.GetStride((uint)i) * dims[i].Step;
                    starts[i] = dims[i].Start; 
                }
                bsd[3 + i] = dims[i].Length;
                bsd[3 + HANDLES_NDIM + i] = storage.Size.GetStride4MLlastDimExpansion((uint)i) * dims[i].Step;
                starts[i] = dims[i].Start;

                if (storage.Size.NumberOfDimensions == 0) {
                    bsd[2] = 0;
                } else {
                    bsd[2] = storage.Size.GetSeqIndex(starts, HANDLES_NDIM);
                }

                if (value.Size.IsBroadcastableTo(bsd)) {
                    if (storage.Size.NumberOfElements == bsd[1] &&
                        storage.Size.NumberOfElements == value.Size.NumberOfElements &&
                        !(storage is CellStorage)) {
                        // 'writes to full' optimization: just use same handle from right side to replace left side completely.
                        // this  check is simple here because we have made sure that inSize broadcasts to outBSD - not the other way around! 
                        // Otherwise we may would run into cases like writing from a row vector to a same length column vector indexing, 
                        // which (may, commonly) triggers broadcasting in binary operators. But here, numel(left) == numel(right) 
                        // means: same shape.
                        // TODO: try to prevent from unneeded Detach() / EnsureStorageOrder() above

                        storage = storage.SetFullOptim(value);
                    } else {
                        // broadcasting assignment, includes the "all elements provided, same shape" case.
                        Core.Functions.Builtin.WriteToOperators.WriteTo_BSD<T>(value.m_handles[0], value.Size, storage.m_handles[0], bsd, Storage<T>.SizeOfT);
                    }
                } else if (bsd[1] == value.Size.NumberOfElements) {
                    // traditional ML assignment: no broadcast, disregard right side shape
                    storage.WriteTo_ML_BSDIter(value, bsd);
                } else {
                    throw new ArgumentException($"The right side argument with size {value.Size.ToString()} is not suitable to be used in the assignment to range {Global.Helper.iterLengths2String(bsd)}. The array on the right side must either be broadcastable or have the same number of elements as the range addressed on the left side.");
                }
                return storage as StorageT;

        }
        #endregion

        static void checkPrepareSetRangeML<T, LocalT, InT, OutT, RetT, StorageT>(ref BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> stor, DimSpec d, uint index)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            if (d.End >= stor.Size[index] && stor.S.StorageOrder != StorageOrders.ColumnMajor) {
                // current End does not fit into this dimension.
                // but in column major all will be fine 
                // TODO: unneeded if "write to full" optimization kicks in
                System.Diagnostics.Debug.Assert(d.End <= stor.Size.GetLastDimIdxForMLSubarray(index));

                // Memory management: when a change of the entry storage is required, we must be careful to leave "this" storage untouched.
                // The first change creates a new storage / works OOP, subsequent changes work implace on this new instance going forward.
                if (stor.ReferenceCount == 0) {
                    // stor is already a new storage: work inplace 
                    stor = stor.EnsureStorageOrder(StorageOrders.ColumnMajor, forceCopy: false, inplace: true);
                } else {
                    // stor is still the entry storage: create a clone and work on that inplace
                    stor = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Create(stor.Handles, stor.Size)
                                .EnsureStorageOrder(StorageOrders.ColumnMajor, forceCopy: false, inplace: true);
                }
            }

            if (stor.m_handles.ReferenceCount > 1) {
                if (stor.ReferenceCount == 0) {  // TODO: twisted brainf..k -> this is not threadsafe, obviously. But concurrent readers must be
                                                 // on non-accelerated code (as per spec.). They can access this storage only, when it was
                                                 // exposed already. If it is an intermediate storage it was not exposed yet. The entry storage, 
                                                 // however, can never have a RefCount = 0 here. So this is considered 'safe enough'. 
                    stor.DetachBufferSetInplace(0);
                } else {
                    // this is the first actual change in this mutating function. Create a new storage to be used in subsequent mutations:
                    stor = stor.GetDetached(targetDeviceID: 0) as StorageT;
                }
                //var detachedStorage = stor.DetachBufferset_OOP(targetDeviceID: 0) as StorageT;
                //if (stor.ReferenceCount == 0 && !object.ReferenceEquals(detachedStorage, stor)) {
                //    stor.Retain(); stor.Release();
                //}
                //stor = detachedStorage;
            }

        }

        #region SetRange_ML BaseArray interface

        /// <summary>
        /// SetRange, ML, Out of-place, BaseArray indices. Extension method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="LocalT"></typeparam>
        /// <typeparam name="InT"></typeparam>
        /// <typeparam name="OutT"></typeparam>
        /// <typeparam name="RetT"></typeparam>
        /// <typeparam name="StorageT"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="d0"></param>
        /// <returns>New storage, to be Assign()ed to the array layer.</returns>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ArgumentException"></exception>
        
        internal static unsafe StorageT SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT value, BaseArray d0)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            var iterators = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.IndexIteratorArray; // thread local context
                //System.Diagnostics.Debug.Assert(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
                const uint HANDLES_NDIM = 1;
                if (d0 is DimSpec) {
                    return storage.SetRange_ML(value, d0 as DimSpec);
                }
                if (d0 is BaseArray<string>) {
#region single dim string spec may contain ';', addressing multiple dimensions
                    var strStorage = (d0 as ConcreteArray<string, Array<string>, InArray<string>, OutArray<string>, Array<string>, Storage<string>>).Storage;
                    if (strStorage == null || strStorage.Size.NumberOfElements != 1) {
                        throw new ArgumentException($"Invalid index specification. Scalar string (array) expected. Found: {strStorage?.Size.ToString()}");
                    }

                    var strVal = strStorage.GetValue(0);
                    if (strVal.Contains(';')) {
                        // DON'T: '(d0 as RetArray<string>)?.Release();' !! d0 is released by caller 

                        // this does not focus on speed anymore! 
                        var dims = strVal.Split(new char[] { ';' }, StringSplitOptions.None);
                        if (dims == null || dims.Length < 1 || dims.Length > 7) {
                            throw new ArgumentException($"Invalid index specification: \"{strVal}\". Unmatching dimension number or too many ';' provided.");
                        }
                        switch (dims.Length) {
                            case 1:
                                return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, dims[0]);
                                 
                            case 2:
                                return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, dims[0], dims[1]);
                                 
                            case 3:
                                return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, dims[0], dims[1], dims[2]);
                                 
                            case 4:
                                return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, dims[0], dims[1], dims[2], dims[3]);
                                 
                            case 5:
                                return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, dims[0], dims[1], dims[2], dims[3], dims[4]);
                                 
                            case 6:
                                return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, dims[0], dims[1], dims[2], dims[3], dims[4], dims[5]);
                                
                            case 7:
                                return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, dims[0], dims[1], dims[2], dims[3], dims[4], dims[5], dims[6]);
                                
                            default:
                                throw new ArgumentException($"Number of ; separated ranges in string index definition ({dims.Length}) exceed the maximum number of dimensions: {7}.");
                        }
                    }
#endregion
                }


                // does it fit - or do we have to reshape first? How large is end? 
                bool expanding = false;

                long lastDimIdx = (long)storage.Size.NumberOfElements - 1;
                long outLen = 1;
                iterators[0] = storage.getCheckIteratorLeft(d0, 0, lastDimIdx, ref expanding, ref outLen);
                if (expanding) {
                    if (value == null || value.Size.NumberOfElements == 0) {
                        throw new NotSupportedException("Combining removal and expansion in a single operation is not supported.");
                    }
                    // Expand any specified dimensions, checks for special case (last dimension ambiguity)
                    long expDim0 = (iterators[0].GetMaximum() ?? lastDimIdx) + 1;
                    storage = storage.Expand_OOP(new Span<long>(&expDim0, 1));
                    // recall not needed! It would only cause trouble for expressions like: A[end + 1, ..,..,.] = ... !
                    //return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0);
                    //
                } else if (value == null || value.Size.NumberOfElements == 0) {
                    if (outLen == 0) {
                        return storage as StorageT;
                    }
                    storage = storage.Remove(iterators, HANDLES_NDIM);
                    return storage as StorageT;
                }
                // Note: empty indices d? are not handled separately. We run to end to get all error handling without blowing the code here. 
                // The exception: if we won't actually change this array, we don't need the checks for mutability (Detach() etc.). Thus
                // we check against outLen > 0 below.

                //if (storage.ReferenceCount > 1 && outLen > 0 && storage.IsReady) {
                //    throw new InvalidOperationException("The storage of this array is shared with other arrays currently! It cannot be altered.");
                //}

                // handle Matlab's implicit reshape 'feature'
                if ((iterators[HANDLES_NDIM - 1].GetMaximum() ?? (storage.Size[HANDLES_NDIM - 1] - 1)) >= storage.Size[HANDLES_NDIM - 1]) {
                    // current End does not fit into this dimension.
                    // but in column major all will be fine 
                    System.Diagnostics.Debug.Assert((iterators[HANDLES_NDIM - 1].GetMaximum() ?? (storage.Size[HANDLES_NDIM - 1] - 1)) <= lastDimIdx);
                    storage = storage.EnsureStorageOrder(StorageOrders.ColumnMajor);
                }
                // ensure is writable
                if (storage.m_handles.ReferenceCount > 1 && outLen > 0) {
                    storage = storage.GetDetached(targetDeviceID: 0) as StorageT;  // CAUTION!! GetDetached may have changed the base offset (-> consolidation)!!!
                                                                                   // Below code copes with changed base offset because iterators do not consider it and 
                                                                                   // we use the (adopted) storage.Size only.
                }

                if (value.Size.IsBroadcastableTo(iterators, HANDLES_NDIM)) {
                    // broadcasting assignment, includes the "all elements provided, same shape" case.
                    Core.Functions.Builtin.WriteToBSDIterOperators.WriteTo_BSD_Iter<T>(
                                            value.m_handles[0], value.Size,
                                            storage.m_handles[0], storage.Size, iterators, HANDLES_NDIM, 
                                            BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT, 
                                            storage.Size.GetStride4MLlastDimExpansion(HANDLES_NDIM - 1));
                } else if (outLen == value.Size.NumberOfElements) {
                    // traditional ML assignment
                    storage.WriteTo_ML_IterIter(value, iterators, HANDLES_NDIM);
                } else {
                    throw new ArgumentException($"The right side argument with size {value.Size.ToString()} is not suitable to be used in the (broadcasting) assignment to range {Global.Helper.iterLengths2String(iterators, HANDLES_NDIM)}.");
                }
                return storage as StorageT;
        }

        /// <summary>
        /// SetRange, ML, Out of-place, BaseArray indices. Extension method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="LocalT"></typeparam>
        /// <typeparam name="InT"></typeparam>
        /// <typeparam name="OutT"></typeparam>
        /// <typeparam name="RetT"></typeparam>
        /// <typeparam name="StorageT"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="d0"></param>
        /// <param name="d1"></param>
        /// <returns>New storage, to be Assign()ed to the array layer.</returns>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ArgumentException"></exception>

        internal static unsafe StorageT SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT value, BaseArray d0, BaseArray d1)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            var iterators = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.IndexIteratorArray; // thread local context
                //System.Diagnostics.Debug.Assert(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
                const uint HANDLES_NDIM = 2;

#region handle ellipsis
                if (d0 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d1);
                            
                        case 2:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, d1);
                            
                        case 3:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full, d1);
                            
                        case 4:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full, Globals.full, d1);
                            
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full, Globals.full, Globals.full, d1);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, d1);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, d1);
                            
                        default:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, new BaseArray[] { d0, d1 }); 
                             
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d1 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0);
                            
                        case 2:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full);
                            
                        case 3:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, Globals.full);
                            
                        case 4:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, Globals.full, Globals.full);
                            
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, Globals.full, Globals.full, Globals.full);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full);
                            
                        default:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, new BaseArray[] { d0, d1 }); 
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
#endregion

                // does it fit - or do we have to reshape first? How large is end? 
                bool expanding = false;

                long lastDimIdx = storage.Size.GetLastDimIdxForMLSubarray(1);
                long outLen = 1; 
                iterators[0] = storage.getCheckIteratorLeft(d0, 0, storage.Size[0L] - 1, ref expanding, ref outLen);
                iterators[1] = storage.getCheckIteratorLeft(d1, 1, lastDimIdx, ref expanding, ref outLen);
                if (expanding) {
                    if (value == null || value.Size.NumberOfElements == 0) {
                        throw new NotSupportedException("Combining removal and expansion in a single operation is not supported.");
                    }
                    // Expand any specified dimensions, checks for special case (last dimension ambiguity)
                    long expDim0 = (iterators[0].GetMaximum() ?? (storage.Size[0L] - 1)) + 1;
                    long expDim1 = (iterators[1].GetMaximum() ?? lastDimIdx) + 1;
#if NETFRAMEWORK
                    Span<long> ind = stackalloc long[] { expDim0, expDim1 };
                    storage = storage.Expand_OOP(ind);
#else
                    storage = storage.Expand_OOP(stackalloc long[] { expDim0, expDim1 });
#endif
                    // recall not needed! It would only cause trouble for expressions like: A[end + 1, ..,..,.] = ... !
                    //return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1);
                    //
                } else if (value == null || value.Size.NumberOfElements == 0) {
                    if (outLen == 0) {
                        return storage as StorageT;
                    }
                    storage = storage.Remove(iterators, HANDLES_NDIM);
                    return storage as StorageT; 
                }

                // Note: empty indices d? are not handled separately. We run to end to get all error handling without blowing the code here. 
                // The exception: if we won't actually change this array, we don't need the checks for mutability (Detach() etc.). Thus
                // we check against outLen > 0 below.

                //if (storage.ReferenceCount > 1 && outLen > 0) {
                //    throw new InvalidOperationException("The storage of this array is shared with other arrays currently! It cannot be altered.");
                //}

                // handle Matlab's implicit reshape 'feature'
                if ((iterators[HANDLES_NDIM - 1].GetMaximum() ?? (storage.Size[HANDLES_NDIM - 1] - 1)) >= storage.Size[HANDLES_NDIM - 1]) {
                    // current End does not fit into this dimension.
                    // but in column major all will be fine 
                    System.Diagnostics.Debug.Assert((iterators[HANDLES_NDIM - 1].GetMaximum() ?? (storage.Size[HANDLES_NDIM - 1] - 1)) <= lastDimIdx);
                    storage = storage.EnsureStorageOrder(StorageOrders.ColumnMajor);
                }
                // ensure is writable
                if (storage.m_handles.ReferenceCount > 1 && outLen > 0) {
                    storage = storage.GetDetached(targetDeviceID: 0) as StorageT;
                }

                if (value.Size.IsBroadcastableTo(iterators, HANDLES_NDIM)) {
                    // broadcasting assignment, includes the "all elements provided, same shape" case.
                    Core.Functions.Builtin.WriteToBSDIterOperators.WriteTo_BSD_Iter<T>(
                                            value.m_handles[0], value.Size,
                                            storage.m_handles[0], storage.Size, iterators, HANDLES_NDIM, 
                                            BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT, 
                                            storage.Size.GetStride4MLlastDimExpansion(HANDLES_NDIM - 1));
                } else if (outLen == value.Size.NumberOfElements) {
                    // traditional ML assignment
                    storage.WriteTo_ML_IterIter(value, iterators, HANDLES_NDIM); 
                } else {
                    throw new ArgumentException($"The right side argument with size {value.Size.ToString()} is not suitable to be used in the (broadcasting) assignment to range {Global.Helper.iterLengths2String(iterators, HANDLES_NDIM)}."); 
                }
                return storage as StorageT;
        }

        /// <summary>
        /// SetRange, ML, Out of-place, BaseArray indices. Extension method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="LocalT"></typeparam>
        /// <typeparam name="InT"></typeparam>
        /// <typeparam name="OutT"></typeparam>
        /// <typeparam name="RetT"></typeparam>
        /// <typeparam name="StorageT"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="d0"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns>New storage, to be Assign()ed to the array layer.</returns>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ArgumentException"></exception>

        internal static unsafe StorageT SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT value, BaseArray d0, BaseArray d1, BaseArray d2)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            var iterators = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.IndexIteratorArray; // thread local context
                //System.Diagnostics.Debug.Assert(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
                const uint HANDLES_NDIM = 3;

#region handle ellipsis
                if (d0 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d1, d2);
                            
                        case 3:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, d1, d2);
                            
                        case 4:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full, d1, d2);
                            
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full, Globals.full, d1, d2);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full, Globals.full, Globals.full, d1, d2);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, d1, d2);
                            
                        default:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, new BaseArray[] { d0, d1, d2 }); 
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d1 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d2);
                            
                        case 3:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, d2);
                            
                        case 4:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, Globals.full, d2);
                            
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, Globals.full, Globals.full, d2);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, Globals.full, Globals.full, Globals.full, d2);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, d2);
                            
                        default:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, new BaseArray[] { d0, d1, d2 });
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d2 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1);
                            
                        case 3:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, Globals.full);
                            
                        case 4:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, Globals.full, Globals.full);
                            
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, Globals.full, Globals.full, Globals.full);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, Globals.full, Globals.full, Globals.full, Globals.full);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full);
                            
                        default:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, new BaseArray[] { d0, d1, d2 }); 
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
#endregion

                // does it fit - or do we have to reshape first? How large is end? 
                bool expanding = false;

                long lastDimIdx = storage.Size.GetLastDimIdxForMLSubarray(HANDLES_NDIM - 1);
                long outLen = 1;
                iterators[0] = storage.getCheckIteratorLeft(d0, 0, storage.Size[0L] - 1, ref expanding, ref outLen);
                iterators[1] = storage.getCheckIteratorLeft(d1, 1, storage.Size[1L] - 1, ref expanding, ref outLen);
                iterators[2] = storage.getCheckIteratorLeft(d2, 2, lastDimIdx, ref expanding, ref outLen);
                if (expanding) {
                    if (value == null || value.Size.NumberOfElements == 0) {
                        throw new NotSupportedException("Combining removal and expansion in a single operation is not supported.");
                    }
                    // Expand any specified dimensions, checks for special case (last dimension ambiguity)
                    long expDim0 = (iterators[0].GetMaximum() ?? (storage.Size[0L] - 1)) + 1;
                    long expDim1 = (iterators[1].GetMaximum() ?? (storage.Size[1L] - 1)) + 1;
                    long expDim2 = (iterators[2].GetMaximum() ?? lastDimIdx) + 1;
#if NETFRAMEWORK
                    Span<long> ind = stackalloc long[] { expDim0, expDim1, expDim2 };
                    storage = storage.Expand_OOP(ind);
#else
                    storage = storage.Expand_OOP(stackalloc long[] { expDim0, expDim1, expDim2 });
#endif
                    // recall not needed! It would only cause trouble for expressions like: A[end + 1, ..,..,.] = ... !
                    //return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2);
                    //
                } else if (value == null || value.Size.NumberOfElements == 0) {
                    if (outLen == 0) {
                        return storage as StorageT;
                    }
                    storage = storage.Remove(iterators, HANDLES_NDIM);
                    return storage as StorageT;
                }

                // Note: empty indices d? are not handled separately. We run to end to get all error handling without blowing the code here. 
                // The exception: if we won't actually change this array, we don't need the checks for mutability (Detach() etc.). Thus
                // we check against outLen > 0 below.

                //if (storage.ReferenceCount > 1 && outLen > 0) {
                //    throw new InvalidOperationException("The storage of this array is shared with other arrays currently! It cannot be altered.");
                //}

                // handle Matlab's implicit reshape 'feature'
                if ((iterators[HANDLES_NDIM - 1].GetMaximum() ?? (storage.Size[HANDLES_NDIM - 1] - 1)) >= storage.Size[HANDLES_NDIM - 1]) {
                    // current End does not fit into this dimension.
                    // but in column major all will be fine 
                    System.Diagnostics.Debug.Assert((iterators[HANDLES_NDIM - 1].GetMaximum() ?? (storage.Size[HANDLES_NDIM - 1] - 1)) <= lastDimIdx);
                    storage = storage.EnsureStorageOrder(StorageOrders.ColumnMajor);
                }
                // ensure is writable
                if (storage.m_handles.ReferenceCount > 1 && outLen > 0) {
                    storage = storage.GetDetached(targetDeviceID: 0) as StorageT;
                }

                if (value.Size.IsBroadcastableTo(iterators, HANDLES_NDIM)) {
                    // broadcasting assignment, includes the "all elements provided, same shape" case.
                    Core.Functions.Builtin.WriteToBSDIterOperators.WriteTo_BSD_Iter<T>(
                                            value.m_handles[0], value.Size,
                                            storage.m_handles[0], storage.Size, iterators, HANDLES_NDIM, 
                                            BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT, 
                                            storage.Size.GetStride4MLlastDimExpansion(HANDLES_NDIM - 1));
                } else if (outLen == value.Size.NumberOfElements) {
                    // traditional ML assignment
                    storage.WriteTo_ML_IterIter(value, iterators, HANDLES_NDIM);
                } else {
                    throw new ArgumentException($"The right side argument with size {value.Size.ToString()} is not suitable to be used in the (broadcasting) assignment to range {Global.Helper.iterLengths2String(iterators, HANDLES_NDIM)}.");
                }
                return storage as StorageT;
        }

        /// <summary>
        /// SetRange, ML, Out of-place, BaseArray indices. Extension method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="LocalT"></typeparam>
        /// <typeparam name="InT"></typeparam>
        /// <typeparam name="OutT"></typeparam>
        /// <typeparam name="RetT"></typeparam>
        /// <typeparam name="StorageT"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="d0"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <param name="d3"></param>
        /// <returns>New storage, to be Assign()ed to the array layer.</returns>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ArgumentException"></exception>

        internal static unsafe StorageT SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT value, BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            var iterators = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.IndexIteratorArray; // thread local context
                //System.Diagnostics.Debug.Assert(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
                const uint HANDLES_NDIM = 4;
#region handle ellipsis
                if (d0 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d1, d2, d3);
                            
                        case 4:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, d1, d2, d3);
                            
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full, d1, d2, d3);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full, Globals.full, d1, d2, d3);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full, Globals.full, Globals.full, d1, d2, d3);
                            
                        default:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, new BaseArray[] { d0, d1, d2, d3 }); 
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d1 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d2, d3);
                            
                        case 4:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, d2, d3);
                            
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, Globals.full, d2, d3);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, Globals.full, Globals.full, d2, d3);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, Globals.full, Globals.full, Globals.full, d2, d3);
                            
                        default:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, new BaseArray[] { d0, d1, d2, d3 }); 
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d2 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d3);
                            
                        case 4:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, Globals.full, d3);
                            
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, Globals.full, Globals.full, d3);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, Globals.full, Globals.full, Globals.full, d3);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, Globals.full, Globals.full, Globals.full, Globals.full, d3);
                            
                        default:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, new BaseArray[] { d0, d1, d2, d3 }); 
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d3 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2);
                            
                        case 4:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, Globals.full);
                            
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, Globals.full, Globals.full);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, Globals.full, Globals.full, Globals.full);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, Globals.full, Globals.full, Globals.full, Globals.full);
                            
                        default:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, new BaseArray[] { d0, d1, d2, d3 }); 
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
#endregion

                // does it fit - or do we have to reshape first? How large is end? 
                bool expanding = false;
                long lastDimIdx = storage.Size.GetLastDimIdxForMLSubarray(HANDLES_NDIM - 1);
                long outLen = 1;
                iterators[0] = storage.getCheckIteratorLeft(d0, 0, storage.Size[0L] - 1, ref expanding, ref outLen);
                iterators[1] = storage.getCheckIteratorLeft(d1, 1, storage.Size[1L] - 1, ref expanding, ref outLen);
                iterators[2] = storage.getCheckIteratorLeft(d2, 2, storage.Size[2L] - 1, ref expanding, ref outLen);
                iterators[3] = storage.getCheckIteratorLeft(d3, 3, lastDimIdx, ref expanding, ref outLen);
                if (expanding) {
                    if (value == null || value.Size.NumberOfElements == 0) {
                        throw new NotSupportedException("Combining removal and expansion in a single operation is not supported.");
                    }
                    // Expand any specified dimensions, checks for special case (last dimension ambiguity)
                    long expDim0 = (iterators[0].GetMaximum() ?? (storage.Size[0L] - 1)) + 1;
                    long expDim1 = (iterators[1].GetMaximum() ?? (storage.Size[1L] - 1)) + 1;
                    long expDim2 = (iterators[2].GetMaximum() ?? (storage.Size[2L] - 1)) + 1;
                    long expDim3 = (iterators[3].GetMaximum() ?? lastDimIdx) + 1;
#if NETFRAMEWORK
                    Span<long> ind = stackalloc long[] { expDim0, expDim1, expDim2, expDim3 };
                    storage = storage.Expand_OOP(ind);
#else
                    storage = storage.Expand_OOP(stackalloc long[] { expDim0, expDim1, expDim2, expDim3 });
#endif
                    // recall not needed! It would only cause trouble for expressions like: A[end + 1, ..,..,.] = ... !
                    //return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3);
                    //
                } else if (value == null || value.Size.NumberOfElements == 0) {
                    if (outLen == 0) {
                        return storage as StorageT;
                    }
                    storage = storage.Remove(iterators, HANDLES_NDIM);
                    return storage as StorageT;
                }
                // Note: empty indices d? are not handled separately. We run to end to get all error handling without blowing the code here. 
                // The exception: if we won't actually change this array, we don't need the checks for mutability (Detach() etc.). Thus
                // we check against outLen > 0 below.

                //if (storage.ReferenceCount > 1 && outLen > 0) {
                //    throw new InvalidOperationException("The storage of this array is shared with other arrays currently! It cannot be altered.");
                //}

                // handle Matlab's implicit reshape 'feature'
                if ((iterators[HANDLES_NDIM - 1].GetMaximum() ?? (storage.Size[HANDLES_NDIM - 1] - 1)) >= storage.Size[HANDLES_NDIM - 1]) {
                    // current End does not fit into this dimension.
                    // but in column major all will be fine 
                    System.Diagnostics.Debug.Assert((iterators[HANDLES_NDIM - 1].GetMaximum() ?? (storage.Size[HANDLES_NDIM - 1] - 1)) <= lastDimIdx);
                    storage = storage.EnsureStorageOrder(StorageOrders.ColumnMajor);
                }
                // ensure is writable
                if (storage.m_handles.ReferenceCount > 1 && outLen > 0) {
                    storage = storage.GetDetached(targetDeviceID: 0) as StorageT;
                }

                if (value.Size.IsBroadcastableTo(iterators, HANDLES_NDIM)) {
                    // broadcasting assignment, includes the "all elements provided, same shape" case.
                    Core.Functions.Builtin.WriteToBSDIterOperators.WriteTo_BSD_Iter<T>(
                                            value.m_handles[0], value.Size,
                                            storage.m_handles[0], storage.Size, iterators, HANDLES_NDIM,
                                            BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT, 
                                            storage.Size.GetStride4MLlastDimExpansion(HANDLES_NDIM - 1));
                } else if (outLen == value.Size.NumberOfElements) {
                    // traditional ML assignment
                    storage.WriteTo_ML_IterIter(value, iterators, HANDLES_NDIM);
                } else {
                    throw new ArgumentException($"The right side argument with size {value.Size.ToString()} is not suitable to be used in the (broadcasting) assignment to range {Global.Helper.iterLengths2String(iterators, HANDLES_NDIM)}.");
                }
                return storage as StorageT;
        }

        /// <summary>
        /// SetRange, ML, Out of-place, BaseArray indices. Extension method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="LocalT"></typeparam>
        /// <typeparam name="InT"></typeparam>
        /// <typeparam name="OutT"></typeparam>
        /// <typeparam name="RetT"></typeparam>
        /// <typeparam name="StorageT"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="d0"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <param name="d3"></param>
        /// <param name="d4"></param>
        /// <returns>New storage, to be Assign()ed to the array layer.</returns>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ArgumentException"></exception>

        internal static unsafe StorageT SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT value, BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3, BaseArray d4)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            var iterators = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.IndexIteratorArray; // thread local context
                //System.Diagnostics.Debug.Assert(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
                const uint HANDLES_NDIM = 5;
#region handle ellipsis
                if (d0 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d1, d2, d3, d4);
                            
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, d1, d2, d3, d4);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full, d1, d2, d3, d4);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full, Globals.full, d1, d2, d3, d4);
                            
                        default:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, new BaseArray[] { d0, d1, d2, d3, d4 });
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d1 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d2, d3, d4);
                            
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, d2, d3, d4);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, Globals.full, d2, d3, d4);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, Globals.full, Globals.full, d2, d3, d4);
                            
                        default:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, new BaseArray[] { d0, d1, d2, d3, d4 });
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d2 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d3, d4);
                            
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, Globals.full, d3, d4);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, Globals.full, Globals.full, d3, d4);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, Globals.full, Globals.full, Globals.full, d3, d4);
                            
                        default:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, new BaseArray[] { d0, d1, d2, d3, d4 });
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d3 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d4);
                            
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, Globals.full, d4);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, Globals.full, Globals.full, d4);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, Globals.full, Globals.full, Globals.full, d4);
                            
                        default:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, new BaseArray[] { d0, d1, d2, d3, d4 });
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d4 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3);
                            
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, Globals.full);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, Globals.full, Globals.full);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, Globals.full, Globals.full, Globals.full);
                            
                        default:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, new BaseArray[] { d0, d1, d2, d3, d4 });
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
#endregion

                // does it fit - or do we have to reshape first? How large is end? 
                bool expanding = false;
                long lastDimIdx = storage.Size.GetLastDimIdxForMLSubarray(HANDLES_NDIM - 1);
                long outLen = 1;
                iterators[0] = storage.getCheckIteratorLeft(d0, 0, storage.Size[0L] - 1, ref expanding, ref outLen);
                iterators[1] = storage.getCheckIteratorLeft(d1, 1, storage.Size[1L] - 1, ref expanding, ref outLen);
                iterators[2] = storage.getCheckIteratorLeft(d2, 2, storage.Size[2L] - 1, ref expanding, ref outLen);
                iterators[3] = storage.getCheckIteratorLeft(d3, 3, storage.Size[3L] - 1, ref expanding, ref outLen);
                iterators[4] = storage.getCheckIteratorLeft(d4, 4, lastDimIdx, ref expanding, ref outLen);
                if (expanding) {
                    if (value == null || value.Size.NumberOfElements == 0) {
                        throw new NotSupportedException("Combining removal and expansion in a single operation is not supported.");
                    }
                    // Expand any specified dimensions, checks for special case (last dimension ambiguity)
                    long expDim0 = (iterators[0].GetMaximum() ?? (storage.Size[0L] - 1)) + 1;
                    long expDim1 = (iterators[1].GetMaximum() ?? (storage.Size[1L] - 1)) + 1;
                    long expDim2 = (iterators[2].GetMaximum() ?? (storage.Size[2L] - 1)) + 1;
                    long expDim3 = (iterators[3].GetMaximum() ?? (storage.Size[3L] - 1)) + 1;
                    long expDim4 = (iterators[4].GetMaximum() ?? lastDimIdx) + 1;
#if NETFRAMEWORK
                    Span<long> ind = stackalloc long[] { expDim0, expDim1, expDim2, expDim3, expDim4 };
                    storage = storage.Expand_OOP(ind);
#else
                    storage = storage.Expand_OOP(stackalloc long[] { expDim0, expDim1, expDim2, expDim3 });
#endif
                    // recall not needed! It would only cause trouble for expressions like: A[end + 1, ..,..,.] = ... !
                    //return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, d4);
                    //
                } else if (value == null || value.Size.NumberOfElements == 0) {
                    if (outLen == 0) {
                        return storage as StorageT;
                    }
                    storage = storage.Remove(iterators, HANDLES_NDIM);
                    return storage as StorageT;
                }

                // Note: empty indices d? are not handled separately. We run to end to get all error handling without blowing the code here. 
                // The exception: if we won't actually change this array, we don't need the checks for mutability (Detach() etc.). Thus
                // we check against outLen > 0 below.

                //if (storage.ReferenceCount > 1 && outLen > 0) {
                //    throw new InvalidOperationException("The storage of this array is shared with other arrays currently! It cannot be altered.");
                //}

                // handle Matlab's implicit reshape 'feature'
                if ((iterators[HANDLES_NDIM - 1].GetMaximum() ?? (storage.Size[HANDLES_NDIM - 1] - 1)) >= storage.Size[HANDLES_NDIM - 1]) {
                    // current End does not fit into this dimension.
                    // but in column major all will be fine 
                    System.Diagnostics.Debug.Assert((iterators[HANDLES_NDIM - 1].GetMaximum() ?? (storage.Size[HANDLES_NDIM - 1] - 1)) <= lastDimIdx);
                    storage = storage.EnsureStorageOrder(StorageOrders.ColumnMajor);
                }
                // ensure is writable
                if (storage.m_handles.ReferenceCount > 1 && outLen > 0) {
                    storage = storage.GetDetached(targetDeviceID: 0) as StorageT;
                }

                if (value.Size.IsBroadcastableTo(iterators, HANDLES_NDIM)) {
                    // broadcasting assignment, includes the "all elements provided, same shape" case.
                    Core.Functions.Builtin.WriteToBSDIterOperators.WriteTo_BSD_Iter<T>(
                                            value.m_handles[0], value.Size,
                                            storage.m_handles[0], storage.Size, iterators, HANDLES_NDIM,
                                            BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT, 
                                            storage.Size.GetStride4MLlastDimExpansion(HANDLES_NDIM - 1));
                } else if (outLen == value.Size.NumberOfElements) {
                    // traditional ML assignment
                    storage.WriteTo_ML_IterIter(value, iterators, HANDLES_NDIM);
                } else {
                    throw new ArgumentException($"The right side argument with size {value.Size.ToString()} is not suitable to be used in the (broadcasting) assignment to range {Global.Helper.iterLengths2String(iterators, HANDLES_NDIM)}.");
                }
                return storage as StorageT;
        }

        /// <summary>
        /// SetRange, ML, Out of-place, BaseArray indices. Extension method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="LocalT"></typeparam>
        /// <typeparam name="InT"></typeparam>
        /// <typeparam name="OutT"></typeparam>
        /// <typeparam name="RetT"></typeparam>
        /// <typeparam name="StorageT"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="d0"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <param name="d3"></param>
        /// <param name="d4"></param>
        /// <param name="d5"></param>
        /// <returns>New storage, to be Assign()ed to the array layer.</returns>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ArgumentException"></exception>

        internal static unsafe StorageT SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT value, BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3, BaseArray d4, BaseArray d5)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            var iterators = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.IndexIteratorArray; // thread local context
                //System.Diagnostics.Debug.Assert(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
                const uint HANDLES_NDIM = 6;
#region handle ellipsis
                if (d0 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d1, d2, d3, d4, d5);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, d1, d2, d3, d4, d5);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, Globals.full, d1, d2, d3, d4, d5);
                            
                        default:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, new BaseArray[] { d0, d1, d2, d3, d4, d5 });
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d1 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d2, d3, d4, d5);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, d2, d3, d4, d5);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, Globals.full, d2, d3, d4, d5);
                            
                        default:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, new BaseArray[] { d0, d1, d2, d3, d4, d5 });
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d2 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d3, d4, d5);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, Globals.full, d3, d4, d5);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, Globals.full, Globals.full, d3, d4, d5);
                            
                        default:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, new BaseArray[] { d0, d1, d2, d3, d4, d5 });
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d3 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d4, d5);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, Globals.full, d4, d5);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, Globals.full, Globals.full, d4, d5);
                            
                        default:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, new BaseArray[] { d0, d1, d2, d3, d4, d5 });
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d4 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, d5);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, Globals.full, d5);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, Globals.full, Globals.full, d5);
                            
                        default:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, new BaseArray[] { d0, d1, d2, d3, d4, d5 });
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d5 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, d4);
                            
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, d4, Globals.full);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, d4, Globals.full, Globals.full);
                            
                        default:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, new BaseArray[] { d0, d1, d2, d3, d4, d5 });
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
#endregion

                // does it fit - or do we have to reshape first? How large is end? 
                bool expanding = false;
                long lastDimIdx = storage.Size.GetLastDimIdxForMLSubarray(HANDLES_NDIM - 1);
                long outLen = 1;
                iterators[0] = storage.getCheckIteratorLeft(d0, 0, storage.Size[0L] - 1, ref expanding, ref outLen);
                iterators[1] = storage.getCheckIteratorLeft(d1, 1, storage.Size[1L] - 1, ref expanding, ref outLen);
                iterators[2] = storage.getCheckIteratorLeft(d2, 2, storage.Size[2L] - 1, ref expanding, ref outLen);
                iterators[3] = storage.getCheckIteratorLeft(d3, 3, storage.Size[3L] - 1, ref expanding, ref outLen);
                iterators[4] = storage.getCheckIteratorLeft(d4, 4, storage.Size[4L] - 1, ref expanding, ref outLen);
                iterators[5] = storage.getCheckIteratorLeft(d5, 5, lastDimIdx, ref expanding, ref outLen);
                if (expanding) {
                    if (value == null || value.Size.NumberOfElements == 0) {
                        throw new NotSupportedException("Combining removal and expansion in a single operation is not supported.");
                    }
                    // Expand any specified dimensions, checks for special case (last dimension ambiguity)
                    long expDim0 = (iterators[0].GetMaximum() ?? (storage.Size[0L] - 1)) + 1;
                    long expDim1 = (iterators[1].GetMaximum() ?? (storage.Size[1L] - 1)) + 1;
                    long expDim2 = (iterators[2].GetMaximum() ?? (storage.Size[2L] - 1)) + 1;
                    long expDim3 = (iterators[3].GetMaximum() ?? (storage.Size[3L] - 1)) + 1;
                    long expDim4 = (iterators[4].GetMaximum() ?? (storage.Size[4L] - 1)) + 1;
                    long expDim5 = (iterators[5].GetMaximum() ?? lastDimIdx) + 1;
#if NETFRAMEWORK
                    Span<long> ind = stackalloc long[] { expDim0, expDim1, expDim2, expDim3, expDim4, expDim5 };
                    storage = storage.Expand_OOP(ind);
#else
                    storage = storage.Expand_OOP(stackalloc long[] { expDim0, expDim1, expDim2, expDim3, expDim4, expDim5 });
#endif
                    // recall not needed! It would only cause trouble for expressions like: A[end + 1, ..,..,.] = ... !
                    //return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, d4, d5);
                    //
                } else if (value == null || value.Size.NumberOfElements == 0) {
                    if (outLen == 0) {
                        return storage as StorageT;
                    }
                    storage = storage.Remove(iterators, HANDLES_NDIM);
                    return storage as StorageT;
                }

                // Note: empty indices d? are not handled separately. We run to end to get all error handling without blowing the code here. 
                // The exception: if we won't actually change this array, we don't need the checks for mutability (Detach() etc.). Thus
                // we check against outLen > 0 below.

                //if (storage.ReferenceCount > 1 && outLen > 0) {
                //    throw new InvalidOperationException("The storage of this array is shared with other arrays currently! It cannot be altered.");
                //}

                // handle Matlab's implicit reshape 'feature'
                if ((iterators[HANDLES_NDIM - 1].GetMaximum() ?? (storage.Size[HANDLES_NDIM - 1] - 1)) >= storage.Size[HANDLES_NDIM - 1]) {
                    // current End does not fit into this dimension.
                    // but in column major all will be fine 
                    System.Diagnostics.Debug.Assert((iterators[HANDLES_NDIM - 1].GetMaximum() ?? (storage.Size[HANDLES_NDIM - 1] - 1)) <= lastDimIdx);
                    storage = storage.EnsureStorageOrder(StorageOrders.ColumnMajor);
                }
                // ensure is writable
                if (storage.m_handles.ReferenceCount > 1 && outLen > 0) {
                    storage = storage.GetDetached(targetDeviceID: 0) as StorageT;
                }

                if (value.Size.IsBroadcastableTo(iterators, HANDLES_NDIM)) {
                    // broadcasting assignment, includes the "all elements provided, same shape" case.
                    Core.Functions.Builtin.WriteToBSDIterOperators.WriteTo_BSD_Iter<T>(
                                            value.m_handles[0], value.Size,
                                            storage.m_handles[0], storage.Size, iterators, HANDLES_NDIM,
                                            BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT, 
                                            storage.Size.GetStride4MLlastDimExpansion(HANDLES_NDIM - 1));
                } else if (outLen == value.Size.NumberOfElements) {
                    // traditional ML assignment
                    storage.WriteTo_ML_IterIter(value, iterators, HANDLES_NDIM);
                } else {
                    throw new ArgumentException($"The right side argument with size {value.Size.ToString()} is not suitable to be used in the (broadcasting) assignment to range {Global.Helper.iterLengths2String(iterators, HANDLES_NDIM)}.");
                }
                return storage as StorageT;
        }

        /// <summary>
        /// SetRange, ML, Out of-place, BaseArray indices. Extension method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="LocalT"></typeparam>
        /// <typeparam name="InT"></typeparam>
        /// <typeparam name="OutT"></typeparam>
        /// <typeparam name="RetT"></typeparam>
        /// <typeparam name="StorageT"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="d0"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <param name="d3"></param>
        /// <param name="d4"></param>
        /// <param name="d5"></param>
        /// <param name="d6"></param>
        /// <returns>New storage, to be Assign()ed to the array layer.</returns>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ArgumentException"></exception>

        internal static unsafe StorageT SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT value, BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3, BaseArray d4, BaseArray d5, BaseArray d6)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            var iterators = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.IndexIteratorArray; // thread local context
                //System.Diagnostics.Debug.Assert(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
                const uint HANDLES_NDIM = 7;
#region handle ellipsis
                if (d0 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d1, d2, d3, d4, d5, d6);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, Globals.full, d1, d2, d3, d4, d5, d6);
                            
                        default:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, new BaseArray[] { d0, d1, d2, d3, d4, d5, d6 });
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d1 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d2, d3, d4, d5, d6);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, Globals.full, d2, d3, d4, d5, d6);
                            
                        default:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, new BaseArray[] { d0, d1, d2, d3, d4, d5, d6 });
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d2 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d3, d4, d5, d6);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, Globals.full, d3, d4, d5, d6);
                            
                        default:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, new BaseArray[] { d0, d1, d2, d3, d4, d5, d6 });
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d3 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d4, d5, d6);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, Globals.full, d4, d5, d6);
                            
                        default:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, new BaseArray[] { d0, d1, d2, d3, d4, d5, d6 });
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d4 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, d5, d6);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, Globals.full, d5, d6);
                            
                        default:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, new BaseArray[] { d0, d1, d2, d3, d4, d5, d6 });
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d5 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, d4, d6);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, d4, Globals.full, d6);
                            
                        default:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, new BaseArray[] { d0, d1, d2, d3, d4, d5, d6 });
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d6 is EllipsisSpec) {
                    switch (storage.Size.NumberOfDimensions) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, d4, d5);
                            
                        case 7:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, d4, d5, Globals.full);
                            
                        default:
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, new BaseArray[] { d0, d1, d2, d3, d4, d5, d6 });
                            
                            //throw new InvalidOperationException($"The number of dimensions of this array ({storage.Size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({storage.Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
#endregion

                // does it fit - or do we have to reshape first? How large is end? 
                bool expanding = false;
                long lastDimIdx = storage.Size.GetLastDimIdxForMLSubarray(HANDLES_NDIM - 1);
                long outLen = 1;
                iterators[0] = storage.getCheckIteratorLeft(d0, 0, storage.Size[0L] - 1, ref expanding, ref outLen);
                iterators[1] = storage.getCheckIteratorLeft(d1, 1, storage.Size[1L] - 1, ref expanding, ref outLen);
                iterators[2] = storage.getCheckIteratorLeft(d2, 2, storage.Size[2L] - 1, ref expanding, ref outLen);
                iterators[3] = storage.getCheckIteratorLeft(d3, 3, storage.Size[3L] - 1, ref expanding, ref outLen);
                iterators[4] = storage.getCheckIteratorLeft(d4, 4, storage.Size[4L] - 1, ref expanding, ref outLen);
                iterators[5] = storage.getCheckIteratorLeft(d5, 5, storage.Size[5L] - 1, ref expanding, ref outLen);
                iterators[6] = storage.getCheckIteratorLeft(d6, 6, lastDimIdx, ref expanding, ref outLen);
                if (expanding) {
                    if (value == null || value.Size.NumberOfElements == 0) {
                        throw new NotSupportedException("Combining removal and expansion in a single operation is not supported.");
                    }
                    // Expand any specified dimensions, checks for special case (last dimension ambiguity)
                    long expDim0 = (iterators[0].GetMaximum() ?? (storage.Size[0L] - 1)) + 1;
                    long expDim1 = (iterators[1].GetMaximum() ?? (storage.Size[1L] - 1)) + 1;
                    long expDim2 = (iterators[2].GetMaximum() ?? (storage.Size[2L] - 1)) + 1;
                    long expDim3 = (iterators[3].GetMaximum() ?? (storage.Size[3L] - 1)) + 1;
                    long expDim4 = (iterators[4].GetMaximum() ?? (storage.Size[4L] - 1)) + 1;
                    long expDim5 = (iterators[5].GetMaximum() ?? (storage.Size[5L] - 1)) + 1;
                    long expDim6 = (iterators[6].GetMaximum() ?? lastDimIdx) + 1;
#if NETFRAMEWORK
                    Span<long> ind = stackalloc long[] { expDim0, expDim1, expDim2, expDim3, expDim4, expDim5, expDim6 };
                    storage = storage.Expand_OOP(ind);
#else
                    storage = storage.Expand_OOP(stackalloc long[] { expDim0, expDim1, expDim2, expDim3, expDim4, expDim5, expDim6 });
#endif
                    // recall not needed! It would only cause trouble for expressions like: A[end + 1, ..,..,.] = ... !
                    //return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, d4, d5, d6);
                    //
                } else if (value == null || value.Size.NumberOfElements == 0) {
                    if (outLen == 0) {
                        return storage as StorageT;
                    }
                    storage = storage.Remove(iterators, HANDLES_NDIM);
                    return storage as StorageT;
                }

                // Note: empty indices d? are not handled separately. We run to end to get all error handling without blowing the code here. 
                // The exception: if we won't actually change this array, we don't need the checks for mutability (Detach() etc.). Thus
                // we check against outLen > 0 below.

                //if (storage.ReferenceCount > 1 && outLen > 0) {
                //    throw new InvalidOperationException("The storage of this array is shared with other arrays currently! It cannot be altered.");
                //}

                // handle Matlab's implicit reshape 'feature'
                if ((iterators[HANDLES_NDIM - 1].GetMaximum() ?? (storage.Size[HANDLES_NDIM - 1] - 1)) >= storage.Size[HANDLES_NDIM - 1]) {
                    // current End does not fit into this dimension.
                    // but in column major all will be fine 
                    System.Diagnostics.Debug.Assert((iterators[HANDLES_NDIM - 1].GetMaximum() ?? (storage.Size[HANDLES_NDIM - 1] - 1)) <= lastDimIdx);
                    storage = storage.EnsureStorageOrder(StorageOrders.ColumnMajor);
                }
                // ensure is writable
                if (storage.m_handles.ReferenceCount > 1 && outLen > 0) {
                    storage = storage.GetDetached(targetDeviceID: 0) as StorageT;
                }

                if (value.Size.IsBroadcastableTo(iterators, HANDLES_NDIM)) {
                    // broadcasting assignment, includes the "all elements provided, same shape" case.
                    Core.Functions.Builtin.WriteToBSDIterOperators.WriteTo_BSD_Iter<T>(
                                            value.m_handles[0], value.Size,
                                            storage.m_handles[0], storage.Size, iterators, HANDLES_NDIM,
                                            BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT, 
                                            storage.Size.GetStride4MLlastDimExpansion(HANDLES_NDIM - 1));
                } else if (outLen == value.Size.NumberOfElements) {
                    // traditional ML assignment
                    storage.WriteTo_ML_IterIter(value, iterators, HANDLES_NDIM);
                } else {
                    throw new ArgumentException($"The right side argument with size {value.Size.ToString()} is not suitable to be used in the (broadcasting) assignment to range {Global.Helper.iterLengths2String(iterators, HANDLES_NDIM)}.");
                }
                return storage as StorageT;
        }

        /// <summary>
        /// SetRange, ML, Out of-place, BaseArray indices. Extension method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="LocalT"></typeparam>
        /// <typeparam name="InT"></typeparam>
        /// <typeparam name="OutT"></typeparam>
        /// <typeparam name="RetT"></typeparam>
        /// <typeparam name="StorageT"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="dims"></param>
        /// <returns>New storage, to be Assign()ed by the array layer.</returns>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ArgumentException"></exception>
        internal static unsafe StorageT SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT value, BaseArray[] dims)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            if (dims == null || dims.Length == 0) {
                return storage as StorageT;
            }
            uint HANDLES_NDIM = (uint)dims.Length;
            if (storage.Size.NumberOfDimensions <= 7) {
                switch (dims.Length) {
                    case 1:
                        return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, dims[0]);
                        
                    case 2:
                        return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, dims[0], dims[1]);
                        
                    case 3:
                        return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, dims[0], dims[1], dims[2]);
                        
                    case 4:
                        return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, dims[0], dims[1], dims[2], dims[3]);
                        
                    case 5:
                        return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, dims[0], dims[1], dims[2], dims[3], dims[4]);
                        
                    case 6:
                        return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, dims[0], dims[1], dims[2], dims[3], dims[4], dims[5]);
                        
                    case 7:
                        return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, dims[0], dims[1], dims[2], dims[3], dims[4], dims[5], dims[6]);
                        
                    default:
                        break; 
                        // throw new ArgumentOutOfRangeException($"The maximum number of dimensions in an array is: '{nameof(storage.Size)}.{nameof(storage.Size.MaxNumberOfDimensions)}={storage.Size.MaxNumberOfDimensions}'");
                }
            }
            var iterators = BaseStorage<T,LocalT,InT,OutT,RetT,StorageT>.Context.IndexIteratorArray; // thread local context
                //System.Diagnostics.Debug.Assert(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);

#region handle ellipsis
                int i = 0;
                // ML does not support newaxis. This eases the ellipsis substitution here.
                for (; i < HANDLES_NDIM; i++) {
                    if (dims[i] is EllipsisSpec) {
                        if (dims.Length == storage.Size.NumberOfDimensions) {
                            dims[i] = Globals.full;
                            // continue! there might be more ellipsis in higher dims. We need to replace them all. 
                        } else if (dims.Length > storage.Size.NumberOfDimensions) {
                            // ellipsis is erased, source array cleared
                            var newDims = new BaseArray[dims.Length - 1];
                            for (int k = 0; k < i; k++) {
                                newDims[k] = dims[k];
                                dims[k] = null;
                            }
                            dims[i] = null;
                            for (int k = i + 1; k < dims.Length; k++) {
                                newDims[k - 1] = dims[k];
                                dims[k] = null;
                            }
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, newDims);
                             
                        } else {
                            // dims.Length < storage.Size.NumberOfDimensions
                            // fills the gap with : full
                            var newDims = new BaseArray[storage.Size.NumberOfDimensions];
                            int k = 0;
                            for (; k < i; k++) {
                                newDims[k] = dims[k];
                                dims[k] = null;
                            }
                            for (; k < storage.Size.NumberOfDimensions - (dims.Length - i - 1); k++) {
                                newDims[k] = Globals.full;
                            }
                            for (; k < storage.Size.NumberOfDimensions; k++) {
                                newDims[k] = dims[++i];
                                dims[i] = null;
                            }
                            return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, newDims);
                             
                        }
                    }
                }
#endregion

                // does it fit - or do we have to reshape first? How large is end? 
                bool expanding = false;
                long lastDimIdx = storage.Size.GetLastDimIdxForMLSubarray(HANDLES_NDIM - 1);
                long outLen = 1;
                for (i = 0; i < HANDLES_NDIM - 1; i++) {
                    iterators[i] = storage.getCheckIteratorLeft(dims[i], (uint)i, storage.Size[i] - 1, ref expanding, ref outLen);
                }
                iterators[i] = storage.getCheckIteratorLeft(dims[i], (uint)i, lastDimIdx, ref expanding, ref outLen);

                if (expanding) {
                    if (value == null || value.Size.NumberOfElements == 0) {
                        throw new NotSupportedException("Combining removal and expansion in a single operation is not supported.");
                    }
                    // Expand any specified dimensions, checks for special case (last dimension ambiguity)
                    long[] tmp = new long[HANDLES_NDIM];
                    for (i = 0; i < HANDLES_NDIM - 1; i++) {
                        tmp[i] = (iterators[i].GetMaximum() ?? (storage.Size[i] - 1)) + 1; 
                    }
                    tmp[i] = (iterators[i].GetMaximum() ?? lastDimIdx) + 1;
                    storage = storage.Expand_OOP(tmp);
                    // recall not needed! It would only cause trouble for expressions like: A[end + 1, ..,..,.] = ... !
                    //return storage.SetRange_ML<T, LocalT, InT, OutT, RetT, StorageT>(value, d0, d1, d2, d3, d4);
                    //
                } else if (value == null || value.Size.NumberOfElements == 0) {
                    if (outLen == 0) {
                        return storage as StorageT;
                    }
                    storage = storage.Remove(iterators, HANDLES_NDIM);
                    return storage as StorageT;
                }

                // Note: empty indices d? are not handled separately. We run to end to get all error handling without blowing the code here. 
                // The exception: if we won't actually change this array, we don't need the checks for mutability (Detach() etc.). Thus
                // we check against outLen > 0 below.

                //if (storage.ReferenceCount > 1 && outLen > 0) {
                //    throw new InvalidOperationException("The storage of this array is shared with other arrays currently! It cannot be altered.");
                //}

                // handle Matlab's implicit reshape 'feature'
                if ((iterators[HANDLES_NDIM - 1].GetMaximum() ?? (storage.Size[HANDLES_NDIM - 1] - 1)) >= storage.Size[HANDLES_NDIM - 1]) {
                    // current End does not fit into this dimension.
                    // but in column major all will be fine 
                    System.Diagnostics.Debug.Assert((iterators[HANDLES_NDIM - 1].GetMaximum() ?? (storage.Size[HANDLES_NDIM - 1] - 1)) <= lastDimIdx);
                    storage = storage.EnsureStorageOrder(StorageOrders.ColumnMajor);
                }
                // ensure is writable
                if (storage.m_handles.ReferenceCount > 1 && outLen > 0) {
                    storage = storage.GetDetached(targetDeviceID: 0) as StorageT;
                }

                if (value.Size.IsBroadcastableTo(iterators, HANDLES_NDIM)) {
                    // broadcasting assignment, includes the "all elements provided, same shape" case.
                    Core.Functions.Builtin.WriteToBSDIterOperators.WriteTo_BSD_Iter<T>(
                                            value.m_handles[0], value.Size,
                                            storage.m_handles[0], storage.Size, iterators, HANDLES_NDIM,
                                            BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.SizeOfT, 
                                            storage.Size.GetStride4MLlastDimExpansion(HANDLES_NDIM - 1));
                } else if (outLen == value.Size.NumberOfElements) {
                    // traditional ML assignment
                    storage.WriteTo_ML_IterIter(value, iterators, HANDLES_NDIM);
                } else {
                    throw new ArgumentException($"The right side argument with size {value.Size.ToString()} is not suitable to be used in the (broadcasting) assignment to range {Global.Helper.iterLengths2String(iterators, HANDLES_NDIM)}.");
                }
                return storage as StorageT;
        }
        #endregion

    }
}
