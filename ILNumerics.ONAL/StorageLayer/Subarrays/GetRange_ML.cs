//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////

using ILNumerics.Core.Arrays;
using ILNumerics.Core.Functions.Builtin;
using ILNumerics.Core.Internal;
using ILNumerics.Core.MemoryLayer;
using ILNumerics.Core.Misc;
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

/* These methods are used from the common (non-accelerated) subarray API. They create a return array / storage. 
 * See the alternative implementation for accelerated subarrays in GetRange_ML_Acc.cs!
 */

namespace ILNumerics.Core.StorageLayer {
    public unsafe abstract partial class BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> : IDisposable, IStorage
        where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

        #region fast subarray

        /// <summary>
        /// Subarray() / GetRange() API, Matlab style. Works Out of-place. Used by public Subarray(), _get indexers on Array{T} &amp; Co. 
        /// </summary>
        /// <param name="d0"></param>
        /// <param name="fromRetT">Flag indicating if this storage belongs to / was called from a return type array.</param>
        /// <returns>Return type array with the subarray as specified.</returns>
        /// <remarks><para>This function is thread safe, by changing the underlying storage / buffer set 'pseudo atomically'. Note, that some 
        /// ML indexing features require the storage to be converted to column major storage order before extracting the subarray.</para></remarks>
        internal virtual StorageT GetRange_ML(DimSpec d0, bool fromRetT) {
            //System.Diagnostics.Debug.Assert(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
            if (d0 is EllipsisSpec) {
                return Create(Handles, Size);

            } else if (d0 is FullDimSpec) {
                //reshape to 1dim vector
                if (m_size.StorageOrder != StorageOrders.ColumnMajor
                    || m_size[0u] != m_size.NumberOfElements
                    || m_size.NumberOfDimensions > Settings.MinNumberOfArrayDimensions) {
                    return Reshape((long)m_size.NumberOfElements, StorageOrders.ColumnMajor); // obsolete: .Subarray(d0);
                }
                return Create(Handles, Size);
            }

            d0.Evaluate(m_size.GetLastDimIdxForMLSubarray(0));

            (var ret, var thisStorage) = checkPrepareGetRangeML(d0, 0, fromRetT);

            uint ndims = Math.Max(1, Settings.MinNumberOfArrayDimensions);
            var bsd = ret.m_size.GetBSD(write: true);
            var stride0 = thisStorage.m_size.GetStride4MLlastDimExpansion(0);
            // the order here is very important! bsd may point to own m_size.m_descriptor (inplace option)!

            // when altering the next line, keep in mind that m_size.GetSeqIndex() does not work with empty arrays!
            bsd[2] = ((d0.Start * stride0 + thisStorage.m_size.BaseOffset));
            bsd[3] = (d0.Length);
            bsd[3 + ndims] = (bsd[3] == 1 ? 0 : (stride0 * d0.Step));
            for (int i = 1; i < ndims; i++) {
                bsd[3 + i] = (1);
                bsd[3 + ndims + i] = 0;
            }
            bsd[0] = (ndims);
            bsd[1] = (d0.Length);
            (ret as LogicalStorage)?.SetNumberTrues(-1);

            return ret;  // no retain! 
        }
        /// <summary>
        /// Subarray() / GetRange() API, Matlab style. Works Out of-place. Used by public Subarray(), _get indexers on Array{T} &amp; Co. 
        /// </summary>
        /// <param name="d0"></param>
        /// <param name="d1"></param>
        /// <param name="fromRetT">Flag indicating if this storage belongs to / was called from a return type array.</param>
        /// <returns>Return type array with the subarray as specified.</returns>
        /// <remarks><para>This function is thread safe, by changing the underlying storage / buffer set 'pseudo atomically'. Note, that some 
        /// ML indexing features require the storage to be converted to column major storage order before extracting the subarray.</para></remarks>
        internal virtual StorageT GetRange_ML(DimSpec d0, DimSpec d1, bool fromRetT) {
            //System.Diagnostics.Debug.Assert(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
            const uint HANDLES_NDIM = 2;

            #region handle ellipsis
            if (d0 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                        return GetRange_ML(d1, fromRetT);
                    case 2:
                        return GetRange_ML(Globals.full, d1, fromRetT);
                    case 3:
                        return GetRange_ML(Globals.full, Globals.full, d1, fromRetT);
                    case 4:
                        return GetRange_ML(Globals.full, Globals.full, Globals.full, d1, fromRetT);
                    case 5:
                        return GetRange_ML(Globals.full, Globals.full, Globals.full, Globals.full, d1, fromRetT);
                    case 6:
                        return GetRange_ML(Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, d1, fromRetT);
                    case 7:
                        return GetRange_ML(Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, d1, fromRetT);
                    default:
                        var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1;
                        return GetRange_ML(tmp, fromRetT, 2);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d1 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                        return GetRange_ML(d0, fromRetT);
                    case 2:
                        return GetRange_ML(d0, Globals.full, fromRetT);
                    case 3:
                        return GetRange_ML(d0, Globals.full, Globals.full, fromRetT);
                    case 4:
                        return GetRange_ML(d0, Globals.full, Globals.full, Globals.full, fromRetT);
                    case 5:
                        return GetRange_ML(d0, Globals.full, Globals.full, Globals.full, Globals.full, fromRetT);
                    case 6:
                        return GetRange_ML(d0, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, fromRetT);
                    case 7:
                        return GetRange_ML(d0, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, fromRetT);
                    default:
                        var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1;
                        return GetRange_ML(tmp, fromRetT, 2);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            #endregion

            // does it fit - or do we have to reshape first? How large is end? 
            d0.Evaluate(m_size[0u] - 1);
            d1.Evaluate(m_size.GetLastDimIdxForMLSubarray(1));

            (var ret, var thisStorage) = checkPrepareGetRangeML(d1, 1, fromRetT);

            var bsd = ret.m_size.GetBSD(write: true);
            // the order here is very important! bsd may point to own m_size.m_descriptor (inplace option)!

            // caution with empty arrays! They don't accept any indices: 
            var size = thisStorage.m_size; 
            bsd[2] = size.NumberOfElements == 0 ? 0 : size.GetSeqIndex(d0.Start, d1.Start);
            long stride0 = size.GetStride(0), stride1 = size.GetStride4MLlastDimExpansion(1);
            bsd[3] = (d0.Length);
            bsd[4] = (d1.Length);
            bsd[3 + HANDLES_NDIM] = (bsd[3] == 1 ? 0 : (stride0 * d0.Step));
            bsd[4 + HANDLES_NDIM] = (bsd[4] == 1 ? 0 : (stride1 * d1.Step));
            bsd[0] = (HANDLES_NDIM);
            bsd[1] = (d0.Length * d1.Length);

            (ret as LogicalStorage)?.SetNumberTrues(-1);
            return ret;
        }
        /// <summary>
        /// Subarray() / GetRange() API, Matlab style. Works Out of-place. Used by public Subarray(), _get indexers on Array{T} &amp; Co. 
        /// </summary>
        /// <param name="d0"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <param name="fromRetT">Flag indicating if this storage belongs to / was called from a return type array.</param>
        /// <returns>Return type array with the subarray as specified.</returns>
        /// <remarks><para>This function is thread safe, by changing the underlying storage / buffer set 'pseudo atomically'. Note, that some 
        /// ML indexing features require the storage to be converted to column major storage order before extracting the subarray.</para></remarks>
        internal virtual StorageT GetRange_ML(DimSpec d0, DimSpec d1, DimSpec d2, bool fromRetT) {
            //System.Diagnostics.Debug.Assert(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
            const uint HANDLES_NDIM = 3;
            #region handle ellipsis
            if (d0 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                        return GetRange_ML(d1, d2, fromRetT);
                    case 3:
                        return GetRange_ML(Globals.full, d1, d2, fromRetT);
                    case 4:
                        return GetRange_ML(Globals.full, Globals.full, d1, d2, fromRetT);
                    case 5:
                        return GetRange_ML(Globals.full, Globals.full, Globals.full, d1, d2, fromRetT);
                    case 6:
                        return GetRange_ML(Globals.full, Globals.full, Globals.full, Globals.full, d1, d2, fromRetT);
                    case 7:
                        return GetRange_ML(Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, d1, d2, fromRetT);
                    default:
                        var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2;
                        return GetRange_ML(tmp, fromRetT, 3);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d1 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                        return GetRange_ML(d0, d2, fromRetT);
                    case 3:
                        return GetRange_ML(d0, Globals.full, d2, fromRetT);
                    case 4:
                        return GetRange_ML(d0, Globals.full, Globals.full, d2, fromRetT);
                    case 5:
                        return GetRange_ML(d0, Globals.full, Globals.full, Globals.full, d2, fromRetT);
                    case 6:
                        return GetRange_ML(d0, Globals.full, Globals.full, Globals.full, Globals.full, d2, fromRetT);
                    case 7:
                        return GetRange_ML(d0, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, d2, fromRetT);
                    default:
                        var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2;
                        return GetRange_ML(tmp, fromRetT, 3);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d2 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                        return GetRange_ML(d0, d1, fromRetT);
                    case 3:
                        return GetRange_ML(d0, d1, Globals.full, fromRetT);
                    case 4:
                        return GetRange_ML(d0, d1, Globals.full, Globals.full, fromRetT);
                    case 5:
                        return GetRange_ML(d0, d1, Globals.full, Globals.full, Globals.full, fromRetT);
                    case 6:
                        return GetRange_ML(d0, d1, Globals.full, Globals.full, Globals.full, Globals.full, fromRetT);
                    case 7:
                        return GetRange_ML(d0, d1, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, fromRetT);
                    default:
                        var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2;
                        return GetRange_ML(tmp, fromRetT, 3);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            #endregion

            // does it fit - or do we have to reshape first? How large is end? 
            d0.Evaluate(m_size[0u] - 1);
            d1.Evaluate(m_size[1u] - 1);
            d2.Evaluate(m_size.GetLastDimIdxForMLSubarray(2));

            (var ret, var thisStorage) = checkPrepareGetRangeML(d2, 2, fromRetT);

            var bsd = ret.m_size.GetBSD(write: true);
            // the order here is very important! bsd may point to own m_size.m_descriptor (inplace option)!

            // caution with empty arrays! They don't accept any indices: 
            var size = thisStorage.m_size;
            bsd[2] = size.NumberOfElements == 0 ? 0 : size.GetSeqIndex(d0.Start, d1.Start, d2.Start);
            long stride0 = size.GetStride(0), stride1 = size.GetStride(1), stride2 = size.GetStride4MLlastDimExpansion(2);
            bsd[3] = (d0.Length);
            bsd[4] = (d1.Length);
            bsd[5] = (d2.Length);
            bsd[3 + HANDLES_NDIM] = (bsd[3] == 1 ? 0 : (stride0 * d0.Step));
            bsd[4 + HANDLES_NDIM] = (bsd[4] == 1 ? 0 : (stride1 * d1.Step));
            bsd[5 + HANDLES_NDIM] = (bsd[5] == 1 ? 0 : (stride2 * d2.Step));
            bsd[0] = (HANDLES_NDIM);
            bsd[1] = (d0.Length * d1.Length * d2.Length);

            (ret as LogicalStorage)?.SetNumberTrues(-1);
            return ret;
        }
        /// <summary>
        /// Subarray() / GetRange() API, Matlab style. Works Out of-place. Used by public Subarray(), _get indexers on Array{T} &amp; Co. 
        /// </summary>
        /// <param name="d0"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <param name="d3"></param>
        /// <param name="fromRetT">Flag indicating if this storage belongs to / was called from a return type array.</param>
        /// <returns>Return type array with the subarray as specified.</returns>
        /// <remarks><para>This function is thread safe, by changing the underlying storage / buffer set 'pseudo atomically'. Note, that some 
        /// ML indexing features require the storage to be converted to column major storage order before extracting the subarray.</para></remarks>
        internal virtual StorageT GetRange_ML(DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3, bool fromRetT) {
            //System.Diagnostics.Debug.Assert(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
            const uint HANDLES_NDIM = 4;

            #region handle ellipsis
            if (d0 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                        return GetRange_ML(d1, d2, d3, fromRetT);
                    case 4:
                        return GetRange_ML(Globals.full, d1, d2, d3, fromRetT);
                    case 5:
                        return GetRange_ML(Globals.full, Globals.full, d1, d2, d3, fromRetT);
                    case 6:
                        return GetRange_ML(Globals.full, Globals.full, Globals.full, d1, d2, d3, fromRetT);
                    case 7:
                        return GetRange_ML(Globals.full, Globals.full, Globals.full, Globals.full, d1, d2, d3, fromRetT);
                    default:
                        var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3;
                        return GetRange_ML(tmp, fromRetT, 4);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d1 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                        return GetRange_ML(d0, d2, d3, fromRetT);
                    case 4:
                        return GetRange_ML(d0, Globals.full, d2, d3, fromRetT);
                    case 5:
                        return GetRange_ML(d0, Globals.full, Globals.full, d2, d3, fromRetT);
                    case 6:
                        return GetRange_ML(d0, Globals.full, Globals.full, Globals.full, d2, d3, fromRetT);
                    case 7:
                        return GetRange_ML(d0, Globals.full, Globals.full, Globals.full, Globals.full, d2, d3, fromRetT);
                    default:
                        var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3;
                        return GetRange_ML(tmp, fromRetT, 4);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d2 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                        return GetRange_ML(d0, d1, d3, fromRetT);
                    case 4:
                        return GetRange_ML(d0, d1, Globals.full, d3, fromRetT);
                    case 5:
                        return GetRange_ML(d0, d1, Globals.full, Globals.full, d3, fromRetT);
                    case 6:
                        return GetRange_ML(d0, d1, Globals.full, Globals.full, Globals.full, d3, fromRetT);
                    case 7:
                        return GetRange_ML(d0, d1, Globals.full, Globals.full, Globals.full, Globals.full, d3, fromRetT);
                    default:
                        var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3;
                        return GetRange_ML(tmp, fromRetT, 4);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d3 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                        return GetRange_ML(d0, d1, d2, fromRetT);
                    case 4:
                        return GetRange_ML(d0, d1, d2, Globals.full, fromRetT);
                    case 5:
                        return GetRange_ML(d0, d1, d2, Globals.full, Globals.full, fromRetT);
                    case 6:
                        return GetRange_ML(d0, d1, d2, Globals.full, Globals.full, Globals.full, fromRetT);
                    case 7:
                        return GetRange_ML(d0, d1, d2, Globals.full, Globals.full, Globals.full, Globals.full, fromRetT);
                    default:
                        var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3;
                        return GetRange_ML(tmp, fromRetT, 4);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            #endregion

            // does it fit - or do we have to reshape first? How large is end? 
            d0.Evaluate(m_size[0u] - 1);
            d1.Evaluate(m_size[1u] - 1);
            d2.Evaluate(m_size[2u] - 1);
            d3.Evaluate(m_size.GetLastDimIdxForMLSubarray(3));

            (var ret, var thisStorage) = checkPrepareGetRangeML(d3, 3, fromRetT);

            var bsd = ret.m_size.GetBSD(write: true);
            // the order here is very important! bsd may point to own m_size.m_descriptor (inplace option)!

            // caution with empty arrays! They don't accept any indices: 
            var size = thisStorage.m_size;
            bsd[2] = size.NumberOfElements == 0 ? 0 : size.GetSeqIndex(d0.Start, d1.Start, d2.Start, d3.Start);
            long stride0 = size.GetStride(0), stride1 = size.GetStride(1), stride2 = size.GetStride(2),
                 stride3 = size.GetStride4MLlastDimExpansion(3);
            bsd[3] = (d0.Length);
            bsd[4] = (d1.Length);
            bsd[5] = (d2.Length);
            bsd[6] = (d3.Length);
            bsd[3 + HANDLES_NDIM] = (bsd[3] == 1 ? 0 : (stride0 * d0.Step));
            bsd[4 + HANDLES_NDIM] = (bsd[4] == 1 ? 0 : (stride1 * d1.Step));
            bsd[5 + HANDLES_NDIM] = (bsd[5] == 1 ? 0 : (stride2 * d2.Step));
            bsd[6 + HANDLES_NDIM] = (bsd[6] == 1 ? 0 : (stride3 * d3.Step));
            bsd[0] = (HANDLES_NDIM);
            bsd[1] = (d0.Length * d1.Length * d2.Length * d3.Length);

            (ret as LogicalStorage)?.SetNumberTrues(-1);
            return ret;
        }
        /// <summary>
        /// Subarray() / GetRange() API, Matlab style. Works Out of-place. Used by public Subarray(), _get indexers on Array{T} &amp; Co. 
        /// </summary>
        /// <param name="d0"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <param name="d3"></param>
        /// <param name="d4"></param>
        /// <param name="fromRetT">Flag indicating if this storage belongs to / was called from a return type array.</param>
        /// <returns>Return type array with the subarray as specified.</returns>
        /// <remarks><para>This function is thread safe, by changing the underlying storage / buffer set 'pseudo atomically'. Note, that some 
        /// ML indexing features require the storage to be converted to column major storage order before extracting the subarray.</para></remarks>
        internal virtual StorageT GetRange_ML(DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3, DimSpec d4, bool fromRetT) {
            //System.Diagnostics.Debug.Assert(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
            const uint HANDLES_NDIM = 5;
            #region handle ellipsis
            if (d0 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        return GetRange_ML(d1, d2, d3, d4, fromRetT);
                    case 5:
                        return GetRange_ML(Globals.full, d1, d2, d3, d4, fromRetT);
                    case 6:
                        return GetRange_ML(Globals.full, Globals.full, d1, d2, d3, d4, fromRetT);
                    case 7:
                        return GetRange_ML(Globals.full, Globals.full, Globals.full, d1, d2, d3, d4, fromRetT);
                    default:
                        var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4;
                        return GetRange_ML(tmp, fromRetT, 5);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d1 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        return GetRange_ML(d0, d2, d3, d4, fromRetT);
                    case 5:
                        return GetRange_ML(d0, Globals.full, d2, d3, d4, fromRetT);
                    case 6:
                        return GetRange_ML(d0, Globals.full, Globals.full, d2, d3, d4, fromRetT);
                    case 7:
                        return GetRange_ML(d0, Globals.full, Globals.full, Globals.full, d2, d3, d4, fromRetT);
                    default:
                        var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4;
                        return GetRange_ML(tmp, fromRetT, 5);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d2 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        return GetRange_ML(d0, d1, d3, d4, fromRetT);
                    case 5:
                        return GetRange_ML(d0, d1, Globals.full, d3, d4, fromRetT);
                    case 6:
                        return GetRange_ML(d0, d1, Globals.full, Globals.full, d3, d4, fromRetT);
                    case 7:
                        return GetRange_ML(d0, d1, Globals.full, Globals.full, Globals.full, d3, d4, fromRetT);
                    default:
                        var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4;
                        return GetRange_ML(tmp, fromRetT, 5);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d3 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        return GetRange_ML(d0, d1, d2, d4, fromRetT);
                    case 5:
                        return GetRange_ML(d0, d1, d2, Globals.full, d4, fromRetT);
                    case 6:
                        return GetRange_ML(d0, d1, d2, Globals.full, Globals.full, d4, fromRetT);
                    case 7:
                        return GetRange_ML(d0, d1, d2, Globals.full, Globals.full, Globals.full, d4, fromRetT);
                    default:
                        var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4;
                        return GetRange_ML(tmp, fromRetT, 5);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d4 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        return GetRange_ML(d0, d1, d2, d3, fromRetT);
                    case 5:
                        return GetRange_ML(d0, d1, d2, d3, Globals.full, fromRetT);
                    case 6:
                        return GetRange_ML(d0, d1, d2, d3, Globals.full, Globals.full, fromRetT);
                    case 7:
                        return GetRange_ML(d0, d1, d2, d3, Globals.full, Globals.full, Globals.full, fromRetT);
                    default:
                        var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4;
                        return GetRange_ML(tmp, fromRetT, 5);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            #endregion

            // does it fit - or do we have to reshape first? How large is end? 
            d0.Evaluate(m_size[0u] - 1);
            d1.Evaluate(m_size[1u] - 1);
            d2.Evaluate(m_size[2u] - 1);
            d3.Evaluate(m_size[3u] - 1);
            d4.Evaluate(m_size.GetLastDimIdxForMLSubarray(4));

            (var ret, var thisStorage) = checkPrepareGetRangeML(d4, 4, fromRetT);

            var bsd = ret.m_size.GetBSD(write: true);
            // the order here is very important! bsd may point to own m_size.m_descriptor (inplace option)!

            // caution with empty arrays! They don't accept any indices: 
            var size = thisStorage.m_size;
            bsd[2] = size.NumberOfElements == 0 ? 0 : size.GetSeqIndex(d0.Start, d1.Start, d2.Start, d3.Start, d4.Start);
            long stride0 = size.GetStride(0), stride1 = size.GetStride(1), stride2 = size.GetStride(2),
                 stride3 = size.GetStride(3), stride4 = size.GetStride4MLlastDimExpansion(4);
            bsd[3] = (d0.Length);
            bsd[4] = (d1.Length);
            bsd[5] = (d2.Length);
            bsd[6] = (d3.Length);
            bsd[7] = (d4.Length);
            bsd[3 + HANDLES_NDIM] = (bsd[3] == 1 ? 0 : (stride0 * d0.Step));
            bsd[4 + HANDLES_NDIM] = (bsd[4] == 1 ? 0 : (stride1 * d1.Step));
            bsd[5 + HANDLES_NDIM] = (bsd[5] == 1 ? 0 : (stride2 * d2.Step));
            bsd[6 + HANDLES_NDIM] = (bsd[6] == 1 ? 0 : (stride3 * d3.Step));
            bsd[7 + HANDLES_NDIM] = (bsd[7] == 1 ? 0 : (stride4 * d4.Step));
            bsd[0] = (HANDLES_NDIM);
            bsd[1] = (d0.Length * d1.Length * d2.Length * d3.Length * d4.Length);

            (ret as LogicalStorage)?.SetNumberTrues(-1);
            return ret;
        }
        /// <summary>
        /// Subarray() / GetRange() API, Matlab style. Works Out of-place. Used by public Subarray(), _get indexers on Array{T} &amp; Co. 
        /// </summary>
        /// <param name="d0"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <param name="d3"></param>
        /// <param name="d4"></param>
        /// <param name="d5"></param>
        /// <param name="fromRetT">Flag indicating if this storage belongs to / was called from a return type array.</param>
        /// <returns>Return type array with the subarray as specified.</returns>
        /// <remarks><para>This function is thread safe, by changing the underlying storage / buffer set 'pseudo atomically'. Note, that some 
        /// ML indexing features require the storage to be converted to column major storage order before extracting the subarray.</para></remarks>
        internal virtual StorageT GetRange_ML(DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3, DimSpec d4, DimSpec d5, bool fromRetT) {
            //System.Diagnostics.Debug.Assert(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
            const uint HANDLES_NDIM = 6;
            #region handle ellipsis
            if (d0 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        return GetRange_ML(d1, d2, d3, d4, d5, fromRetT);
                    case 6:
                        return GetRange_ML(Globals.full, d1, d2, d3, d4, d5, fromRetT);
                    case 7:
                        return GetRange_ML(Globals.full, Globals.full, d1, d2, d3, d4, d5, fromRetT);
                    default:
                        var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5;
                        return GetRange_ML(tmp, fromRetT, 6);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d1 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        return GetRange_ML(d0, d2, d3, d4, d5, fromRetT);
                    case 6:
                        return GetRange_ML(d0, Globals.full, d2, d3, d4, d5, fromRetT);
                    case 7:
                        return GetRange_ML(d0, Globals.full, Globals.full, d2, d3, d4, d5, fromRetT);
                    default:
                        var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5;
                        return GetRange_ML(tmp, fromRetT, 6);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d2 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        return GetRange_ML(d0, d1, d3, d4, d5, fromRetT);
                    case 6:
                        return GetRange_ML(d0, d1, Globals.full, d3, d4, d5, fromRetT);
                    case 7:
                        return GetRange_ML(d0, d1, Globals.full, Globals.full, d3, d4, d5, fromRetT);
                    default:
                        var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5;
                        return GetRange_ML(tmp, fromRetT, 6);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d3 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        return GetRange_ML(d0, d1, d2, d4, d5, fromRetT);
                    case 6:
                        return GetRange_ML(d0, d1, d2, Globals.full, d4, d5, fromRetT);
                    case 7:
                        return GetRange_ML(d0, d1, d2, Globals.full, Globals.full, d4, d5, fromRetT);
                    default:
                        var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5;
                        return GetRange_ML(tmp, fromRetT, 6);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d4 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        return GetRange_ML(d0, d1, d2, d3, d5, fromRetT);
                    case 6:
                        return GetRange_ML(d0, d1, d2, d3, Globals.full, d5, fromRetT);
                    case 7:
                        return GetRange_ML(d0, d1, d2, d3, Globals.full, Globals.full, d5, fromRetT);
                    default:
                        var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5;
                        return GetRange_ML(tmp, fromRetT, 6);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d5 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        return GetRange_ML(d0, d1, d2, d3, d4, fromRetT);
                    case 6:
                        return GetRange_ML(d0, d1, d2, d3, d4, Globals.full, fromRetT);
                    case 7:
                        return GetRange_ML(d0, d1, d2, d3, d4, Globals.full, Globals.full, fromRetT);
                    default:
                        var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5;
                        return GetRange_ML(tmp, fromRetT, 6);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            #endregion

            // does it fit - or do we have to reshape first? How large is end? 
            d0.Evaluate(m_size[0u] - 1);
            d1.Evaluate(m_size[1u] - 1);
            d2.Evaluate(m_size[2u] - 1);
            d3.Evaluate(m_size[3u] - 1);
            d4.Evaluate(m_size[4u] - 1);
            d5.Evaluate(m_size.GetLastDimIdxForMLSubarray(5));

            (var ret, var thisStorage) = checkPrepareGetRangeML(d5, 5, fromRetT);

            var bsd = ret.m_size.GetBSD(write: true);
            // the order here is very important! bsd may point to own m_size.m_descriptor (inplace option)!

            // caution with empty arrays! They don't accept any indices: 
            var size = thisStorage.m_size;
            bsd[2] = size.NumberOfElements == 0 ? 0 : size.GetSeqIndex(d0.Start, d1.Start, d2.Start, d3.Start, d4.Start, d5.Start);
            long stride0 = size.GetStride(0), stride1 = size.GetStride(1), stride2 = size.GetStride(2),
                 stride3 = size.GetStride(3), stride4 = size.GetStride(4), stride5 = size.GetStride4MLlastDimExpansion(5);
            bsd[3] = (d0.Length);
            bsd[4] = (d1.Length);
            bsd[5] = (d2.Length);
            bsd[6] = (d3.Length);
            bsd[7] = (d4.Length);
            bsd[8] = (d5.Length);
            bsd[3 + HANDLES_NDIM] = (bsd[3] == 1 ? 0 : (stride0 * d0.Step));
            bsd[4 + HANDLES_NDIM] = (bsd[4] == 1 ? 0 : (stride1 * d1.Step));
            bsd[5 + HANDLES_NDIM] = (bsd[5] == 1 ? 0 : (stride2 * d2.Step));
            bsd[6 + HANDLES_NDIM] = (bsd[6] == 1 ? 0 : (stride3 * d3.Step));
            bsd[7 + HANDLES_NDIM] = (bsd[7] == 1 ? 0 : (stride4 * d4.Step));
            bsd[8 + HANDLES_NDIM] = (bsd[8] == 1 ? 0 : (stride5 * d5.Step));
            bsd[0] = (HANDLES_NDIM);
            bsd[1] = (d0.Length * d1.Length * d2.Length * d3.Length * d4.Length * d5.Length);

            (ret as LogicalStorage)?.SetNumberTrues(-1);
            return ret;
        }
        /// <summary>
        /// Subarray() / GetRange() API, Matlab style. Works Out of-place. Used by public Subarray(), _get indexers on Array{T} &amp; Co. 
        /// </summary>
        /// <param name="d0"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <param name="d3"></param>
        /// <param name="d4"></param>
        /// <param name="d5"></param>
        /// <param name="d6"></param>
        /// <param name="fromRetT">Flag indicating if this storage belongs to / was called from a return type array.</param>
        /// <returns>Return type array with the subarray as specified.</returns>
        /// <remarks><para>This function is thread safe, by changing the underlying storage / buffer set 'pseudo atomically'. Note, that some 
        /// ML indexing features require the storage to be converted to column major storage order before extracting the subarray.</para></remarks>
        internal virtual StorageT GetRange_ML(DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3, DimSpec d4, DimSpec d5, DimSpec d6, bool fromRetT) {
            //System.Diagnostics.Debug.Assert(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
            const uint HANDLES_NDIM = 7;
            #region handle ellipsis
            if (d0 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        return GetRange_ML(d1, d2, d3, d4, d5, d6, fromRetT);
                    case 7:
                        return GetRange_ML(Globals.full, d1, d2, d3, d4, d5, d6, fromRetT);
                    default:
                        var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6;
                        return GetRange_ML(tmp, fromRetT, 7);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d1 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        return GetRange_ML(d0, d2, d3, d4, d5, d6, fromRetT);
                    case 7:
                        return GetRange_ML(d0, Globals.full, d2, d3, d4, d5, d6, fromRetT);
                    default:
                        var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6;
                        return GetRange_ML(tmp, fromRetT, 7);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d2 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        return GetRange_ML(d0, d1, d3, d4, d5, d6, fromRetT);
                    case 7:
                        return GetRange_ML(d0, d1, Globals.full, d3, d4, d5, d6, fromRetT);
                    default:
                        var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6;
                        return GetRange_ML(tmp, fromRetT, 7);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d3 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        return GetRange_ML(d0, d1, d2, d4, d5, d6, fromRetT);
                    case 7:
                        return GetRange_ML(d0, d1, d2, Globals.full, d4, d5, d6, fromRetT);
                    default:
                        var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6;
                        return GetRange_ML(tmp, fromRetT, 7);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d4 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        return GetRange_ML(d0, d1, d2, d3, d5, d6, fromRetT);
                    case 7:
                        return GetRange_ML(d0, d1, d2, d3, Globals.full, d5, d6, fromRetT);
                    default:
                        var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6;
                        return GetRange_ML(tmp, fromRetT, 7);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d5 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        return GetRange_ML(d0, d1, d2, d3, d4, d6, fromRetT);
                    case 7:
                        return GetRange_ML(d0, d1, d2, d3, d4, Globals.full, d6, fromRetT);
                    default:
                        var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6;
                        return GetRange_ML(tmp, fromRetT, 7);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d6 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        return GetRange_ML(d0, d1, d2, d3, d4, d5, fromRetT);
                    case 7:
                        return GetRange_ML(d0, d1, d2, d3, d4, d5, Globals.full, fromRetT);
                    default:
                        var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6;
                        return GetRange_ML(tmp, fromRetT, 7);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            #endregion

            // does it fit - or do we have to reshape first? How large is end? 
            d0.Evaluate(m_size[0u] - 1);
            d1.Evaluate(m_size[1u] - 1);
            d2.Evaluate(m_size[2u] - 1);
            d3.Evaluate(m_size[3u] - 1);
            d4.Evaluate(m_size[4u] - 1);
            d5.Evaluate(m_size[5u] - 1);
            d6.Evaluate(m_size.GetLastDimIdxForMLSubarray(6));

            (var ret, var thisStorage) = checkPrepareGetRangeML(d6, 6, fromRetT);

            var bsd = ret.m_size.GetBSD(write: true);
            // the order here is very important! bsd may point to own m_size.m_descriptor (inplace option)!

            // caution with empty arrays! They don't accept any indices: 
            var size = thisStorage.m_size;
            bsd[2] = size.NumberOfElements == 0 ? 0 : size.GetSeqIndex(d0.Start, d1.Start, d2.Start, d3.Start, d4.Start, d5.Start, d6.Start);
            long stride0 = size.GetStride(0), stride1 = size.GetStride(1), stride2 = size.GetStride(2),
                 stride3 = size.GetStride(3), stride4 = size.GetStride(4), stride5 = size.GetStride(5),
                 stride6 = size.GetStride4MLlastDimExpansion(6);
            bsd[3] = (d0.Length);
            bsd[4] = (d1.Length);
            bsd[5] = (d2.Length);
            bsd[6] = (d3.Length);
            bsd[7] = (d4.Length);
            bsd[8] = (d5.Length);
            bsd[9] = (d6.Length);
            bsd[3 + HANDLES_NDIM] = (bsd[3] == 1 ? 0 : (stride0 * d0.Step));
            bsd[4 + HANDLES_NDIM] = (bsd[4] == 1 ? 0 : (stride1 * d1.Step));
            bsd[5 + HANDLES_NDIM] = (bsd[5] == 1 ? 0 : (stride2 * d2.Step));
            bsd[6 + HANDLES_NDIM] = (bsd[6] == 1 ? 0 : (stride3 * d3.Step));
            bsd[7 + HANDLES_NDIM] = (bsd[7] == 1 ? 0 : (stride4 * d4.Step));
            bsd[8 + HANDLES_NDIM] = (bsd[8] == 1 ? 0 : (stride5 * d5.Step));
            bsd[9 + HANDLES_NDIM] = (bsd[9] == 1 ? 0 : (stride6 * d6.Step));
            bsd[0] = (HANDLES_NDIM);
            bsd[1] = (d0.Length * d1.Length * d2.Length * d3.Length * d4.Length * d5.Length * d6.Length);

            (ret as LogicalStorage)?.SetNumberTrues(-1);
            return ret;
        }
        /// <summary>
        /// Subarray() / GetRange() API, Matlab style. Works Out of-place. Used by public Subarray(), _get indexers on Array{T} &amp; Co. 
        /// </summary>
        /// <param name="dims">Array of DimSpec objects defining the ranges of the subarray. Ownership of DimSpec elements is taken by method.</param>
        /// <param name="fromRetT">Flag indicating if this storage belongs to / was called from a return type array.</param>
        /// <param name="len">Minimal length of <paramref name="dims"/> parameter.</param>
        /// <returns>Return type array with the subarray as specified.</returns>
        /// <remarks><para>This function is thread safe, by changing the underlying storage / buffer set 'pseudo atomically'. Note, that some 
        /// ML indexing features require the storage to be converted to column major storage order before extracting the subarray.</para></remarks>
        internal unsafe virtual StorageT GetRange_ML(DimSpec[] dims, bool fromRetT, uint? len) {
            uint HANDLES_NDIM = len ?? (uint)dims.Length;
            if (dims == null || HANDLES_NDIM == 0) {
                #region return empty
                //System.Diagnostics.Debug.Assert(Settings.MinNumberOfArrayDimensions == 2);
                StorageT ret;
                if (fromRetT && m_arrayCounter == 1 && GetAsynchReferencesCount() == 1) {
                    ret = this as StorageT;
                } else {
                    ret = Create();
                    ret.m_handles = m_handles;
                    m_handles.Retain();
                }
                ret.m_size.SetAll(0, 1);
                return ret;
                #endregion
            }
            if (m_size.NumberOfDimensions <= 7) {
                switch (HANDLES_NDIM) {
                    case 1:
                        return GetRange_ML(dims[0], fromRetT);
                    case 2:
                        return GetRange_ML(dims[0], dims[1], fromRetT);
                    case 3:
                        return GetRange_ML(dims[0], dims[1], dims[2], fromRetT);
                    case 4:
                        return GetRange_ML(dims[0], dims[1], dims[2], dims[3], fromRetT);
                    case 5:
                        return GetRange_ML(dims[0], dims[1], dims[2], dims[3], dims[4], fromRetT);
                    case 6:
                        return GetRange_ML(dims[0], dims[1], dims[2], dims[3], dims[4], dims[5], fromRetT);
                    case 7:
                        return GetRange_ML(dims[0], dims[1], dims[2], dims[3], dims[4], dims[5], dims[6], fromRetT);
                    default:
                        break;
                }
            }
            // either we have more params or more dimensions in m_size. 

            #region handle ellipsis
            // ML does not support newaxis. This eases the ellipsis substitution here.
            for (int i = 0; i < HANDLES_NDIM; i++) {
                if (dims[i] is EllipsisSpec) {
                    substituteEllipsis(dims, Context.DimSpecArray, ref HANDLES_NDIM, m_size.NumberOfDimensions, i);
                    dims = Context.DimSpecArray;
                    return GetRange_ML(dims, fromRetT, HANDLES_NDIM);
                }
            }
            #endregion
            {
                if (HANDLES_NDIM > Size.MaxNumberOfDimensions) {
                    throw new ArgumentOutOfRangeException($"The number of dimension specifiers provided exceed the maximum number of dimensions {Size.MaxNumberOfDimensions}). Found (after substituting ellipsis specifiers): {HANDLES_NDIM} specifiers.");
                }
                // does it fit - or do we have to reshape first? How large is end? 
                uint i = 0;
                for (; i < HANDLES_NDIM - 1; i++) {
                    dims[i]?.Evaluate(m_size[i] - 1);
                }
                dims[i]?.Evaluate(m_size.GetLastDimIdxForMLSubarray(i));

                (var ret, var thisStorage) = checkPrepareGetRangeML(dims[i], i, fromRetT);

                var bsd = ret.m_size.GetBSD(write: true);
                // the order here is very important! bsd may point to own m_size.m_descriptor (inplace option)!


                // caution with empty arrays! They don't accept any indices: 
                var size = thisStorage.m_size;
                long nrElem = 1;
                long* tmp = Context.TmpBuffer1000;
                for (i = 0; i < HANDLES_NDIM - 1; i++) {
                    tmp[i] = dims[i].Start;
                    tmp[HANDLES_NDIM + i] = size.GetStride((uint)i);
                    nrElem *= dims[i].Length;
                }
                tmp[i] = dims[i].Start;
                tmp[HANDLES_NDIM + i] = size.GetStride4MLlastDimExpansion((uint)i);
                nrElem *= dims[i].Length;

                if (size.NumberOfDimensions == 0) {
                    bsd[2] = 0;
                } else {
                    bsd[2] = size.GetSeqIndex(tmp, HANDLES_NDIM);
                }
                // Must use a second loop, still in here we pot. change our own bsd! So we should have collected all relev. info before.
                for (i = 0; i < HANDLES_NDIM; i++) {
                    if (dims[i].Length == 1) {
                        bsd[3 + i] = 1;
                        bsd[3 + HANDLES_NDIM + i] = 0;
                    } else {
                        bsd[3 + i] = dims[i].Length;
                        bsd[3 + HANDLES_NDIM + i] = tmp[HANDLES_NDIM + i] * dims[i].Step;
                    }
                }
                bsd[0] = HANDLES_NDIM;
                bsd[1] = nrElem;

                (ret as LogicalStorage)?.SetNumberTrues(-1);
                return ret;  // no retain! See: checkPrepareGetRangeML()
            }
        }

        #endregion

        #region BaseArray indexing
        /// <summary>
        /// Subarray() / GetRange() API, Matlab style. Works Out of-place. Used by public Subarray(), _get indexers on Array{T} &amp; Co. 
        /// </summary>
        /// <param name="d0"></param>
        /// <param name="fromRetT">Flag indicating if this storage belongs to / was called from a return type array.</param>
        /// <returns>Return type array with the subarray as specified.</returns>
        /// <remarks><para>This function is thread safe, by changing the underlying storage / buffer set 'pseudo atomically'. Note, that some 
        /// ML indexing features require the storage to be converted to column major storage order before extracting the subarray.</para>
        /// <para>Note, that this overload (single BaseArray index) must be 'virtual' for CellStorage override. All other versions of 
        /// GetRange_ML use iterateSubarrayML which is overridden by CellStorage also.</para></remarks>
        
        internal virtual StorageT GetRange_ML(BaseArray d0, bool fromRetT) {

            const uint HANDLES_NDIM = 1;
            if (d0 is DimSpec) {
                return GetRange_ML(d0 as DimSpec, fromRetT);
            }
            if (d0 is BaseArray<string>) {
                #region single dim string spec may contain ';', addressing multiple dimensions
                var strStorage = (d0 as ConcreteArray<string, Array<string>, InArray<string>, OutArray<string>, Array<string>, Storage<string>>).Storage;
                if (strStorage == null || strStorage.m_size.NumberOfElements != 1) {
                    throw new ArgumentException($"Invalid index specification. Scalar string (array) expected. Found: {strStorage?.m_size.ToString()}");
                }

                var strVal = strStorage.GetValue(0);
                if (strVal.Contains(';')) {

                    // this does not focus on speed anymore! 
                    var dims = strVal.Split(new char[] { ';' }, StringSplitOptions.None);
                    if (dims == null || dims.Length < 1 || dims.Length > 7) {
                        throw new ArgumentException($"Invalid index specification: \"{strVal}\". Unmatching dimension number or too many ';' provided. Maximum number of dimensions supported for string indexing is: 7.");
                    }
                    // the new partial indices are converted to BaseArray<string> and must be released here.
                    var partialDims = dims.Select(d => Storage<string>.Create(d).RetArray).ToArray();
                    var retStor = GetRange_ML(partialDims, fromRetT);
                    return retStor;
                }
                #endregion
            }

            bool allSimpleRanges = true;
            // Note, the BaseArray d0 is NOT released after the iteration. 
            // The iterator should be a struct and no disposal should be required. d0, d1, ... are released from within the array layer.
            var iterator0 = getCheckIterator(d0, 0, (long)m_size.NumberOfElements - 1, ref allSimpleRanges);

            StorageT thisStorage = this as StorageT;

            if (iterator0.GetMaximum() >= m_size[HANDLES_NDIM - 1u]) {
                thisStorage = thisStorage.EnsureStorageOrder(StorageOrders.ColumnMajor, inplace: fromRetT);
            }
            StorageT ret;
            if (allSimpleRanges) {
                #region special case: ML arrays are 2D 

                //if (fromRetT && thisStorage.m_arrayCounter == 1 && thisStorage.GetAsynchReferencesCount() == 1) {
                //    ret = thisStorage;
                //    ret.Retain();
                //} else {
                //    ret = thisStorage.Clone() as StorageT;
                //}
                ret = Create(thisStorage.Handles, thisStorage.Size);

                var bsd = ret.m_size.GetBSD(write: true);
                System.Diagnostics.Debug.Assert(iterator0 != null && iterator0.GetStepSize() >= 0);
                System.Diagnostics.Debug.Assert(Settings.MinNumberOfArrayDimensions == 2);
                var stride = thisStorage.m_size.GetStride4MLlastDimExpansion(0);

                bsd[3] = (iterator0.GetLength());
                bsd[3 + 2] = bsd[3] == 1 ? 0 : (stride * iterator0.GetStepSize().GetValueOrDefault());
                bsd[4] = (1);
                bsd[4 + 2] = 0; //  bsd[3];
                bsd[0] = (2);
                bsd[1] = bsd[3];
                var min = iterator0.GetMinimum().GetValueOrDefault();
                if (min < 0) {
                    min += (iterator0.GetLastDimensionIndex() + 1);
                }
                bsd[2] = (thisStorage.m_size.BaseOffset + stride * min);
                //iterator0.Dispose();
                #endregion
            } else {
                // the index d0 is an index array or a non-simple range (f.e. by strings including ',')
                // In this 1D case Matlab gives the output array the same shape as the index array. 
                Size sd0 = null;
                #region acquire the size of d0, assuming that d0 is still alive, the iterator0 is not disposed either. 
                if (d0 is BaseArray<double>) {
                    sd0 = (d0 as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>).Storage.m_size;
                } else if (d0 is BaseArray<int>) {
                    sd0 = (d0 as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>).Storage.m_size;
                } else if (d0 is BaseArray<uint>) {
                    sd0 = (d0 as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>).Storage.m_size;
                } else if (d0 is BaseArray<long>) {
                    sd0 = (d0 as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>).Storage.m_size;
                } else if (d0 is BaseArray<ulong>) {
                    sd0 = (d0 as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>).Storage.m_size;
                } else if (d0 is BaseArray<float>) {
                    sd0 = (d0 as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>).Storage.m_size;
                } else if (d0 is BaseArray<short>) {
                    sd0 = (d0 as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>).Storage.m_size;
                } else if (d0 is BaseArray<ushort>) {
                    sd0 = (d0 as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>).Storage.m_size;
                } else if (d0 is BaseArray<sbyte>) {
                    sd0 = (d0 as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>).Storage.m_size;
                } else if (d0 is BaseArray<byte>) {
                    sd0 = (d0 as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>).Storage.m_size;
                }
                System.Diagnostics.Debug.Assert(Equals(sd0, null) || sd0.NumberOfElements == iterator0.GetLength());
                #endregion

                #region iterate fast 
                ret = Create();

                ret.m_handles[0] = ret.New((ulong)iterator0.GetLength(), clear: false);
                if (!Equals(sd0, null)) {
                    ret.m_size.SetAll(sd0, 0, StorageOrders.ColumnMajor);
                } else {
                    // all non-numeric index arrays produce a column vector
                    ret.m_size.SetAll(dim0: iterator0.GetLength(), order: StorageOrders.ColumnMajor);
                }

                var cur = 0;
                foreach (var i in iterator0) {
                    ret.SetValue(thisStorage.GetValue(i), cur++);
                }
                // iterator0 was NOT disposed in foreach.
                #endregion
            }
            (ret as LogicalStorage)?.SetNumberTrues(-1);
            return ret;

        }
        /// <summary>
        /// Subarray() / GetRange() API, Matlab style. Works Out of-place. Used by public Subarray(), _get indexers on Array{T} &amp; Co. 
        /// </summary>
        /// <param name="d0"></param>
        /// <param name="d1"></param>
        /// <param name="fromRetT">Flag indicating if this storage belongs to / was called from a return type array.</param>
        /// <returns>Return type array with the subarray as specified.</returns>
        /// <remarks><para>This function is thread safe, by changing the underlying storage / buffer set 'pseudo atomically'. Note, that some 
        /// ML indexing features require the storage to be converted to column major storage order before extracting the subarray.</para></remarks>
        internal StorageT GetRange_ML(BaseArray d0, BaseArray d1, bool fromRetT) {
            //System.Diagnostics.Debug.Assert(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
            const uint HANDLES_NDIM = 2;
            #region handle ellipsis
            if (d0 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                        return GetRange_ML(d1, fromRetT);
                    case 2:
                        return GetRange_ML(Globals.full, d1, fromRetT);
                    case 3:
                        return GetRange_ML(Globals.full, Globals.full, d1, fromRetT);
                    case 4:
                        return GetRange_ML(Globals.full, Globals.full, Globals.full, d1, fromRetT);
                    case 5:
                        return GetRange_ML(Globals.full, Globals.full, Globals.full, Globals.full, d1, fromRetT);
                    case 6:
                        return GetRange_ML(Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, d1, fromRetT);
                    case 7:
                        return GetRange_ML(Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, d1, fromRetT);
                    default:
                        return GetRange_ML(new BaseArray[] { d0, d1 }, fromRetT);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d1 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                        return GetRange_ML(d0, fromRetT);
                    case 2:
                        return GetRange_ML(d0, Globals.full, fromRetT);
                    case 3:
                        return GetRange_ML(d0, Globals.full, Globals.full, fromRetT);
                    case 4:
                        return GetRange_ML(d0, Globals.full, Globals.full, Globals.full, fromRetT);
                    case 5:
                        return GetRange_ML(d0, Globals.full, Globals.full, Globals.full, Globals.full, fromRetT);
                    case 6:
                        return GetRange_ML(d0, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, fromRetT);
                    case 7:
                        return GetRange_ML(d0, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, fromRetT);
                    default:
                        return GetRange_ML(new BaseArray[] { d0, d1 }, fromRetT);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            #endregion

            var iterators = Context.IndexIteratorArray; // thread local context

            bool allSimpleRanges = !(this is CellStorage);
            iterators[0] = getCheckIterator(d0, 0, m_size[0u] - 1, ref allSimpleRanges);
            iterators[1] = getCheckIterator(d1, 1, m_size.GetLastDimIdxForMLSubarray(HANDLES_NDIM - 1), ref allSimpleRanges);

            StorageT thisStorage = this as StorageT;
            if (iterators[HANDLES_NDIM - 1].GetMaximum() >= m_size[HANDLES_NDIM - 1u]) {
                thisStorage = thisStorage.EnsureStorageOrder(StorageOrders.ColumnMajor, inplace: fromRetT);
            }

            if (allSimpleRanges) {                                                                  // TODO: check ref counts on thisStorage / after Assign() 
                                                                                                    // TODO: forward to DimSpec overload? / modify BDS only                             //       check ref counts on returned storage !! 
                return thisStorage.createBSDSubarrayML(iterators, HANDLES_NDIM, fromRetT);
            }
            return thisStorage.iterateSubarrayML_StrgT(iterators, HANDLES_NDIM, fromRetT);

        }
        /// <summary>
        /// Subarray() / GetRange() API, Matlab style. Works Out of-place. Used by public Subarray(), _get indexers on Array{T} &amp; Co. 
        /// </summary>
        /// <param name="d0"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <param name="fromRetT">Flag indicating if this storage belongs to / was called from a return type array.</param>
        /// <returns>Return type array with the subarray as specified.</returns>
        /// <remarks><para>This function is thread safe, by changing the underlying storage / buffer set 'pseudo atomically'. Note, that some 
        /// ML indexing features require the storage to be converted to column major storage order before extracting the subarray.</para></remarks>
        internal StorageT GetRange_ML(BaseArray d0, BaseArray d1, BaseArray d2, bool fromRetT) {
            //System.Diagnostics.Debug.Assert(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
            #region handle ellipsis
            if (d0 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                        return GetRange_ML(d1, d2, fromRetT);
                    case 3:
                        return GetRange_ML(Globals.full, d1, d2, fromRetT);
                    case 4:
                        return GetRange_ML(Globals.full, Globals.full, d1, d2, fromRetT);
                    case 5:
                        return GetRange_ML(Globals.full, Globals.full, Globals.full, d1, d2, fromRetT);
                    case 6:
                        return GetRange_ML(Globals.full, Globals.full, Globals.full, Globals.full, d1, d2, fromRetT);
                    case 7:
                        return GetRange_ML(Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, d1, d2, fromRetT);
                    default:
                        return GetRange_ML(new BaseArray[] { d0, d1, d2 }, fromRetT);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d1 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                        return GetRange_ML(d0, d2, fromRetT);
                    case 3:
                        return GetRange_ML(d0, Globals.full, d2, fromRetT);
                    case 4:
                        return GetRange_ML(d0, Globals.full, Globals.full, d2, fromRetT);
                    case 5:
                        return GetRange_ML(d0, Globals.full, Globals.full, Globals.full, d2, fromRetT);
                    case 6:
                        return GetRange_ML(d0, Globals.full, Globals.full, Globals.full, Globals.full, d2, fromRetT);
                    case 7:
                        return GetRange_ML(d0, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, d2, fromRetT);
                    default:
                        return GetRange_ML(new BaseArray[] { d0, d1, d2 }, fromRetT);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d2 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                        return GetRange_ML(d0, d1, fromRetT);
                    case 3:
                        return GetRange_ML(d0, d1, Globals.full, fromRetT);
                    case 4:
                        return GetRange_ML(d0, d1, Globals.full, Globals.full, fromRetT);
                    case 5:
                        return GetRange_ML(d0, d1, Globals.full, Globals.full, Globals.full, fromRetT);
                    case 6:
                        return GetRange_ML(d0, d1, Globals.full, Globals.full, Globals.full, Globals.full, fromRetT);
                    case 7:
                        return GetRange_ML(d0, d1, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, fromRetT);
                    default:
                        return GetRange_ML(new BaseArray[] { d0, d1, d2 }, fromRetT);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            #endregion
            const uint HANDLES_NDIM = 3;

            var iterators = Context.IndexIteratorArray; // thread local context

            bool allSimpleRanges = !(this is CellStorage);
            iterators[0] = getCheckIterator(d0, 0, m_size[0u] - 1, ref allSimpleRanges);
            iterators[1] = getCheckIterator(d1, 1, m_size[1u] - 1, ref allSimpleRanges);
            iterators[HANDLES_NDIM - 1] = getCheckIterator(d2, HANDLES_NDIM - 1, m_size.GetLastDimIdxForMLSubarray(HANDLES_NDIM - 1), ref allSimpleRanges);

            StorageT thisStorage = this as StorageT;
            if (iterators[HANDLES_NDIM - 1].GetMaximum() >= m_size[HANDLES_NDIM - 1u]) {
                thisStorage = thisStorage.EnsureStorageOrder(StorageOrders.ColumnMajor, inplace: fromRetT);
            }
            if (allSimpleRanges) {
                return thisStorage.createBSDSubarrayML(iterators, HANDLES_NDIM, fromRetT);
            }
            return thisStorage.iterateSubarrayML_StrgT(iterators, HANDLES_NDIM, fromRetT);
        }
        /// <summary>
        /// Subarray() / GetRange() API, Matlab style. Works Out of-place. Used by public Subarray(), _get indexers on Array{T} &amp; Co. 
        /// </summary>
        /// <param name="d0"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <param name="d3"></param>
        /// <param name="fromRetT">Flag indicating if this storage belongs to / was called from a return type array.</param>
        /// <returns>Return type array with the subarray as specified.</returns>
        /// <remarks><para>This function is thread safe, by changing the underlying storage / buffer set 'pseudo atomically'. Note, that some 
        /// ML indexing features require the storage to be converted to column major storage order before extracting the subarray.</para></remarks>
        internal StorageT GetRange_ML(BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3, bool fromRetT) {
            //System.Diagnostics.Debug.Assert(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
            const uint HANDLES_NDIM = 4;

            #region handle ellipsis
            if (d0 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                        return GetRange_ML(d1, d2, d3, fromRetT);
                    case 4:
                        return GetRange_ML(Globals.full, d1, d2, d3, fromRetT);
                    case 5:
                        return GetRange_ML(Globals.full, Globals.full, d1, d2, d3, fromRetT);
                    case 6:
                        return GetRange_ML(Globals.full, Globals.full, Globals.full, d1, d2, d3, fromRetT);
                    case 7:
                        return GetRange_ML(Globals.full, Globals.full, Globals.full, Globals.full, d1, d2, d3, fromRetT);
                    default:
                        return GetRange_ML(new BaseArray[] { d0, d1, d2, d3 }, fromRetT);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d1 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                        return GetRange_ML(d0, d2, d3, fromRetT);
                    case 4:
                        return GetRange_ML(d0, Globals.full, d2, d3, fromRetT);
                    case 5:
                        return GetRange_ML(d0, Globals.full, Globals.full, d2, d3, fromRetT);
                    case 6:
                        return GetRange_ML(d0, Globals.full, Globals.full, Globals.full, d2, d3, fromRetT);
                    case 7:
                        return GetRange_ML(d0, Globals.full, Globals.full, Globals.full, Globals.full, d2, d3, fromRetT);
                    default:
                        return GetRange_ML(new BaseArray[] { d0, d1, d2, d3 }, fromRetT);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d2 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                        return GetRange_ML(d0, d1, d3, fromRetT);
                    case 4:
                        return GetRange_ML(d0, d1, Globals.full, d3, fromRetT);
                    case 5:
                        return GetRange_ML(d0, d1, Globals.full, Globals.full, d3, fromRetT);
                    case 6:
                        return GetRange_ML(d0, d1, Globals.full, Globals.full, Globals.full, d3, fromRetT);
                    case 7:
                        return GetRange_ML(d0, d1, Globals.full, Globals.full, Globals.full, Globals.full, d3, fromRetT);
                    default:
                        return GetRange_ML(new BaseArray[] { d0, d1, d2, d3 }, fromRetT);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d3 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                        return GetRange_ML(d0, d1, d2, fromRetT);
                    case 4:
                        return GetRange_ML(d0, d1, d2, Globals.full, fromRetT);
                    case 5:
                        return GetRange_ML(d0, d1, d2, Globals.full, Globals.full, fromRetT);
                    case 6:
                        return GetRange_ML(d0, d1, d2, Globals.full, Globals.full, Globals.full, fromRetT);
                    case 7:
                        return GetRange_ML(d0, d1, d2, Globals.full, Globals.full, Globals.full, Globals.full, fromRetT);
                    default:
                        return GetRange_ML(new BaseArray[] { d0, d1, d2, d3 }, fromRetT);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            #endregion

            var iterators = Context.IndexIteratorArray; // thread local context
            bool allSimpleRanges = !(this is CellStorage);
            iterators[0] = getCheckIterator(d0, 0, m_size[0u] - 1, ref allSimpleRanges);
            iterators[1] = getCheckIterator(d1, 1, m_size[1u] - 1, ref allSimpleRanges);
            iterators[2] = getCheckIterator(d2, 2, m_size[2u] - 1, ref allSimpleRanges);
            iterators[HANDLES_NDIM - 1] = getCheckIterator(d3, HANDLES_NDIM - 1, m_size.GetLastDimIdxForMLSubarray(HANDLES_NDIM - 1), ref allSimpleRanges);

            StorageT thisStorage = this as StorageT;
            if (iterators[HANDLES_NDIM - 1].GetMaximum() >= m_size[HANDLES_NDIM - 1u]) {
                thisStorage = thisStorage.EnsureStorageOrder(StorageOrders.ColumnMajor, inplace: fromRetT);
            }
            if (allSimpleRanges) {
                return thisStorage.createBSDSubarrayML(iterators, HANDLES_NDIM, fromRetT);
            }
            return thisStorage.iterateSubarrayML_StrgT(iterators, HANDLES_NDIM, fromRetT);
        }
        /// <summary>
        /// Subarray() / GetRange() API, Matlab style. Works Out of-place. Used by public Subarray(), _get indexers on Array{T} &amp; Co. 
        /// </summary>
        /// <param name="d0"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <param name="d3"></param>
        /// <param name="d4"></param>
        /// <param name="fromRetT">Flag indicating if this storage belongs to / was called from a return type array.</param>
        /// <returns>Return type array with the subarray as specified.</returns>
        /// <remarks><para>This function is thread safe, by changing the underlying storage / buffer set 'pseudo atomically'. Note, that some 
        /// ML indexing features require the storage to be converted to column major storage order before extracting the subarray.</para></remarks>
        internal StorageT GetRange_ML(BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3, BaseArray d4, bool fromRetT) {
            //System.Diagnostics.Debug.Assert(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
            const uint HANDLES_NDIM = 5;
            #region handle ellipsis
            if (d0 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        return GetRange_ML(d1, d2, d3, d4, fromRetT);
                    case 5:
                        return GetRange_ML(Globals.full, d1, d2, d3, d4, fromRetT);
                    case 6:
                        return GetRange_ML(Globals.full, Globals.full, d1, d2, d3, d4, fromRetT);
                    case 7:
                        return GetRange_ML(Globals.full, Globals.full, Globals.full, d1, d2, d3, d4, fromRetT);
                    default:
                        return GetRange_ML(new BaseArray[] { d0, d1, d2, d3, d4 }, fromRetT);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d1 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        return GetRange_ML(d0, d2, d3, d4, fromRetT);
                    case 5:
                        return GetRange_ML(d0, Globals.full, d2, d3, d4, fromRetT);
                    case 6:
                        return GetRange_ML(d0, Globals.full, Globals.full, d2, d3, d4, fromRetT);
                    case 7:
                        return GetRange_ML(d0, Globals.full, Globals.full, Globals.full, d2, d3, d4, fromRetT);
                    default:
                        return GetRange_ML(new BaseArray[] { d0, d1, d2, d3, d4 }, fromRetT);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d2 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        return GetRange_ML(d0, d1, d3, d4, fromRetT);
                    case 5:
                        return GetRange_ML(d0, d1, Globals.full, d3, d4, fromRetT);
                    case 6:
                        return GetRange_ML(d0, d1, Globals.full, Globals.full, d3, d4, fromRetT);
                    case 7:
                        return GetRange_ML(d0, d1, Globals.full, Globals.full, Globals.full, d3, d4, fromRetT);
                    default:
                        return GetRange_ML(new BaseArray[] { d0, d1, d2, d3, d4 }, fromRetT);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d3 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        return GetRange_ML(d0, d1, d2, d4, fromRetT);
                    case 5:
                        return GetRange_ML(d0, d1, d2, Globals.full, d4, fromRetT);
                    case 6:
                        return GetRange_ML(d0, d1, d2, Globals.full, Globals.full, d4, fromRetT);
                    case 7:
                        return GetRange_ML(d0, d1, d2, Globals.full, Globals.full, Globals.full, d4, fromRetT);
                    default:
                        return GetRange_ML(new BaseArray[] { d0, d1, d2, d3, d4 }, fromRetT);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d4 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        return GetRange_ML(d0, d1, d2, d3, fromRetT);
                    case 5:
                        return GetRange_ML(d0, d1, d2, d3, Globals.full, fromRetT);
                    case 6:
                        return GetRange_ML(d0, d1, d2, d3, Globals.full, Globals.full, fromRetT);
                    case 7:
                        return GetRange_ML(d0, d1, d2, d3, Globals.full, Globals.full, Globals.full, fromRetT);
                    default:
                        return GetRange_ML(new BaseArray[] { d0, d1, d2, d3, d4 }, fromRetT);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            #endregion

            var iterators = Context.IndexIteratorArray; // thread local context

            bool allSimpleRanges = !(this is CellStorage);
            iterators[0] = getCheckIterator(d0, 0, m_size[0u] - 1, ref allSimpleRanges);
            iterators[1] = getCheckIterator(d1, 1, m_size[1u] - 1, ref allSimpleRanges);
            iterators[2] = getCheckIterator(d2, 2, m_size[2u] - 1, ref allSimpleRanges);
            iterators[3] = getCheckIterator(d3, 3, m_size[3u] - 1, ref allSimpleRanges);
            iterators[HANDLES_NDIM - 1] = getCheckIterator(d4, HANDLES_NDIM - 1, m_size.GetLastDimIdxForMLSubarray(HANDLES_NDIM - 1), ref allSimpleRanges);

            StorageT thisStorage = this as StorageT;
            if (iterators[HANDLES_NDIM - 1].GetMaximum() >= m_size[HANDLES_NDIM - 1u]) {
                thisStorage = thisStorage.EnsureStorageOrder(StorageOrders.ColumnMajor, inplace: fromRetT);
            }
            if (allSimpleRanges) {
                return thisStorage.createBSDSubarrayML(iterators, HANDLES_NDIM, fromRetT);
            }
            return thisStorage.iterateSubarrayML_StrgT(iterators, HANDLES_NDIM, fromRetT);
        }
        /// <summary>
        /// Subarray() / GetRange() API, Matlab style. Works Out of-place. Used by public Subarray(), _get indexers on Array{T} &amp; Co. 
        /// </summary>
        /// <param name="d0"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <param name="d3"></param>
        /// <param name="d4"></param>
        /// <param name="d5"></param>
        /// <param name="fromRetT">Flag indicating if this storage belongs to / was called from a return type array.</param>
        /// <returns>Return type array with the subarray as specified.</returns>
        /// <remarks><para>This function is thread safe, by changing the underlying storage / buffer set 'pseudo atomically'. Note, that some 
        /// ML indexing features require the storage to be converted to column major storage order before extracting the subarray.</para></remarks>
        internal StorageT GetRange_ML(BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3, BaseArray d4, BaseArray d5, bool fromRetT) {
            //System.Diagnostics.Debug.Assert(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
            const uint HANDLES_NDIM = 6;
            #region handle ellipsis
            if (d0 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        return GetRange_ML(d1, d2, d3, d4, d5, fromRetT);
                    case 6:
                        return GetRange_ML(Globals.full, d1, d2, d3, d4, d5, fromRetT);
                    case 7:
                        return GetRange_ML(Globals.full, Globals.full, d1, d2, d3, d4, d5, fromRetT);
                    default:
                        return GetRange_ML(new BaseArray[] { d0, d1, d2, d3, d4, d5 }, fromRetT);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d1 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        return GetRange_ML(d0, d2, d3, d4, d5, fromRetT);
                    case 6:
                        return GetRange_ML(d0, Globals.full, d2, d3, d4, d5, fromRetT);
                    case 7:
                        return GetRange_ML(d0, Globals.full, Globals.full, d2, d3, d4, d5, fromRetT);
                    default:
                        return GetRange_ML(new BaseArray[] { d0, d1, d2, d3, d4, d5 }, fromRetT);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d2 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        return GetRange_ML(d0, d1, d3, d4, d5, fromRetT);
                    case 6:
                        return GetRange_ML(d0, d1, Globals.full, d3, d4, d5, fromRetT);
                    case 7:
                        return GetRange_ML(d0, d1, Globals.full, Globals.full, d3, d4, d5, fromRetT);
                    default:
                        return GetRange_ML(new BaseArray[] { d0, d1, d2, d3, d4, d5 }, fromRetT);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d3 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        return GetRange_ML(d0, d1, d2, d4, d5, fromRetT);
                    case 6:
                        return GetRange_ML(d0, d1, d2, Globals.full, d4, d5, fromRetT);
                    case 7:
                        return GetRange_ML(d0, d1, d2, Globals.full, Globals.full, d4, d5, fromRetT);
                    default:
                        return GetRange_ML(new BaseArray[] { d0, d1, d2, d3, d4, d5 }, fromRetT);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d4 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        return GetRange_ML(d0, d1, d2, d3, d5, fromRetT);
                    case 6:
                        return GetRange_ML(d0, d1, d2, d3, Globals.full, d5, fromRetT);
                    case 7:
                        return GetRange_ML(d0, d1, d2, d3, Globals.full, Globals.full, d5, fromRetT);
                    default:
                        return GetRange_ML(new BaseArray[] { d0, d1, d2, d3, d4, d5 }, fromRetT);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d5 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        return GetRange_ML(d0, d1, d2, d3, d4, fromRetT);
                    case 6:
                        return GetRange_ML(d0, d1, d2, d3, d4, Globals.full, fromRetT);
                    case 7:
                        return GetRange_ML(d0, d1, d2, d3, d4, Globals.full, Globals.full, fromRetT);
                    default:
                        return GetRange_ML(new BaseArray[] { d0, d1, d2, d3, d4, d5 }, fromRetT);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            #endregion

            var iterators = Context.IndexIteratorArray; // thread local context

            bool allSimpleRanges = !(this is CellStorage);
            iterators[0] = getCheckIterator(d0, 0, m_size[0u] - 1, ref allSimpleRanges);
            iterators[1] = getCheckIterator(d1, 1, m_size[1u] - 1, ref allSimpleRanges);
            iterators[2] = getCheckIterator(d2, 2, m_size[2u] - 1, ref allSimpleRanges);
            iterators[3] = getCheckIterator(d3, 3, m_size[3u] - 1, ref allSimpleRanges);
            iterators[4] = getCheckIterator(d4, 4, m_size[4u] - 1, ref allSimpleRanges);
            iterators[HANDLES_NDIM - 1] = getCheckIterator(d5, HANDLES_NDIM - 1, m_size.GetLastDimIdxForMLSubarray(HANDLES_NDIM - 1), ref allSimpleRanges);

            StorageT thisStorage = this as StorageT;
            if (iterators[HANDLES_NDIM - 1].GetMaximum() >= m_size[HANDLES_NDIM - 1u]) {
                thisStorage = thisStorage.EnsureStorageOrder(StorageOrders.ColumnMajor, inplace: fromRetT);
            }
            if (allSimpleRanges) {
                return thisStorage.createBSDSubarrayML(iterators, HANDLES_NDIM, fromRetT);
            }
            return thisStorage.iterateSubarrayML_StrgT(iterators, HANDLES_NDIM, fromRetT);
        }
        /// <summary>
        /// Subarray() / GetRange() API, Matlab style. Works Out of-place. Used by public Subarray(), _get indexers on Array{T} &amp; Co. 
        /// </summary>
        /// <param name="d0"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <param name="d3"></param>
        /// <param name="d4"></param>
        /// <param name="d5"></param>
        /// <param name="d6"></param>
        /// <param name="fromRetT">Flag indicating if this storage belongs to / was called from a return type array.</param>
        /// <returns>Return type array with the subarray as specified.</returns>
        /// <remarks><para>This function is thread safe, by changing the underlying storage / buffer set 'pseudo atomically'. Note, that some 
        /// ML indexing features require the storage to be converted to column major storage order before extracting the subarray.</para></remarks>
        internal StorageT GetRange_ML(BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3, BaseArray d4, BaseArray d5, BaseArray d6, bool fromRetT) {
            //System.Diagnostics.Debug.Assert(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
            const uint HANDLES_NDIM = 7;

            #region handle ellipsis
            if (d0 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        return GetRange_ML(d1, d2, d3, d4, d5, d6, fromRetT);
                    case 7:
                        return GetRange_ML(Globals.full, d1, d2, d3, d4, d5, d6, fromRetT);
                    default:
                        return GetRange_ML(new BaseArray[] { d0, d1, d2, d3, d4, d5, d6 }, fromRetT);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d1 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        return GetRange_ML(d0, d2, d3, d4, d5, d6, fromRetT);
                    case 7:
                        return GetRange_ML(d0, Globals.full, d2, d3, d4, d5, d6, fromRetT);
                    default:
                        return GetRange_ML(new BaseArray[] { d0, d1, d2, d3, d4, d5, d6 }, fromRetT);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d2 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        return GetRange_ML(d0, d1, d3, d4, d5, d6, fromRetT);
                    case 7:
                        return GetRange_ML(d0, d1, Globals.full, d3, d4, d5, d6, fromRetT);
                    default:
                        return GetRange_ML(new BaseArray[] { d0, d1, d2, d3, d4, d5, d6 }, fromRetT);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d3 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        return GetRange_ML(d0, d1, d2, d4, d5, d6, fromRetT);
                    case 7:
                        return GetRange_ML(d0, d1, d2, Globals.full, d4, d5, d6, fromRetT);
                    default:
                        return GetRange_ML(new BaseArray[] { d0, d1, d2, d3, d4, d5, d6 }, fromRetT);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d4 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        return GetRange_ML(d0, d1, d2, d3, d5, d6, fromRetT);
                    case 7:
                        return GetRange_ML(d0, d1, d2, d3, Globals.full, d5, d6, fromRetT);
                    default:
                        return GetRange_ML(new BaseArray[] { d0, d1, d2, d3, d4, d5, d6 }, fromRetT);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d5 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        return GetRange_ML(d0, d1, d2, d3, d4, d6, fromRetT);
                    case 7:
                        return GetRange_ML(d0, d1, d2, d3, d4, Globals.full, d6, fromRetT);
                    default:
                        return GetRange_ML(new BaseArray[] { d0, d1, d2, d3, d4, d5, d6 }, fromRetT);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d6 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        return GetRange_ML(d0, d1, d2, d3, d4, d5, fromRetT);
                    case 7:
                        return GetRange_ML(d0, d1, d2, d3, d4, d5, Globals.full, fromRetT);
                    default:
                        return GetRange_ML(new BaseArray[] { d0, d1, d2, d3, d4, d5, d6 }, fromRetT);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({m_size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            #endregion

            var iterators = Context.IndexIteratorArray; // thread local context

            bool allSimpleRanges = !(this is CellStorage);
            iterators[0] = getCheckIterator(d0, 0, m_size[0u] - 1, ref allSimpleRanges);
            iterators[1] = getCheckIterator(d1, 1, m_size[1u] - 1, ref allSimpleRanges);
            iterators[2] = getCheckIterator(d2, 2, m_size[2u] - 1, ref allSimpleRanges);
            iterators[3] = getCheckIterator(d3, 3, m_size[3u] - 1, ref allSimpleRanges);
            iterators[4] = getCheckIterator(d4, 4, m_size[4u] - 1, ref allSimpleRanges);
            iterators[5] = getCheckIterator(d5, 5, m_size[5u] - 1, ref allSimpleRanges);
            iterators[HANDLES_NDIM - 1] = getCheckIterator(d6, HANDLES_NDIM - 1, m_size.GetLastDimIdxForMLSubarray(HANDLES_NDIM - 1), ref allSimpleRanges);

            StorageT thisStorage = this as StorageT;
            if (iterators[HANDLES_NDIM - 1].GetMaximum() >= m_size[HANDLES_NDIM - 1u]) {
                thisStorage = thisStorage.EnsureStorageOrder(StorageOrders.ColumnMajor, inplace: fromRetT);
            }
            if (allSimpleRanges) {
                return thisStorage.createBSDSubarrayML(iterators, HANDLES_NDIM, fromRetT);
            }
            return thisStorage.iterateSubarrayML_StrgT(iterators, HANDLES_NDIM, fromRetT);
        }
        /// <summary>
        /// Subarray() / GetRange() API, Matlab style. Works Out of-place. Used by public Subarray(), _get indexers on Array{T} &amp; Co. 
        /// </summary>
        /// <param name="dims"></param>
        /// <param name="fromRetT">Flag indicating if this storage belongs to / was called from a return type array.</param>
        /// <returns>Return type array with the subarray as specified.</returns>
        /// <remarks><para>This function is thread safe, by changing the underlying storage / buffer set 'pseudo atomically'. Note, that some 
        /// ML indexing features require the storage to be converted to column major storage order before extracting the subarray.</para></remarks>
        internal StorageT GetRange_ML(BaseArray[] dims, bool fromRetT) {
            if (dims == null || dims.Length == 0) {
                #region return empty
                System.Diagnostics.Debug.Assert(Settings.MinNumberOfArrayDimensions == 2);
                StorageT ret;

                // Currently disabled: keeps things more simple! 
                //if (fromRetT && m_arrayCounter == 1 && GetAsynchReferencesCount() == 1) {
                //    ret = this as StorageT;
                //} else {
                ret = Create();
                ret.m_handles = m_handles;
                m_handles.Retain();
                //}
                ret.m_size.SetAll(0, 1);
                return ret;
                #endregion
            }
            if (m_size.NumberOfDimensions <= 7) {
                switch (dims.Length) {
                    case 1:
                        return GetRange_ML(dims[0], fromRetT);
                    case 2:
                        return GetRange_ML(dims[0], dims[1], fromRetT);
                    case 3:
                        return GetRange_ML(dims[0], dims[1], dims[2], fromRetT);
                    case 4:
                        return GetRange_ML(dims[0], dims[1], dims[2], dims[3], fromRetT);
                    case 5:
                        return GetRange_ML(dims[0], dims[1], dims[2], dims[3], dims[4], fromRetT);
                    case 6:
                        return GetRange_ML(dims[0], dims[1], dims[2], dims[3], dims[4], dims[5], fromRetT);
                    case 7:
                        return GetRange_ML(dims[0], dims[1], dims[2], dims[3], dims[4], dims[5], dims[6], fromRetT);
                    default:
                        break;
                }
            }
            uint HANDLES_NDIM = (uint)dims.Length;
            //System.Diagnostics.Debug.Assert(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);

            #region handle ellipsis

            // ML does not support newaxis. This eases the ellipsis substitution here.

            // TODO: replace with substituteEllipsis()! 
            for (int i = 0; i < HANDLES_NDIM; i++) {
                if (dims[i] is EllipsisSpec) {
                    if (dims.Length == m_size.NumberOfDimensions) {
                        dims[i] = Globals.full;
                        // continue! there might be more ellipsis in higher dims. We need to replace them all. 
                    } else if (dims.Length > m_size.NumberOfDimensions) {
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
                        dims = newDims; // disables multiple disposals in finally block
                        return GetRange_ML(newDims, fromRetT);
                    } else {
                        // dims.Length < m_size.NumberOfDimensions
                        // fills the gap with : full
                        var newDims = new BaseArray[m_size.NumberOfDimensions];
                        int k = 0;
                        for (; k < i; k++) {
                            newDims[k] = dims[k];
                            dims[k] = null;
                        }
                        for (; k < m_size.NumberOfDimensions - (dims.Length - i - 1); k++) {
                            newDims[k] = Globals.full;
                        }
                        for (; k < m_size.NumberOfDimensions; k++) {
                            newDims[k] = dims[++i];
                            dims[i] = null;
                        }
                        dims = newDims; // disables multiple disposals in finally block
                        return GetRange_ML(newDims, fromRetT);
                    }
                }
            }
            #endregion
            {
                var iterators = Context.IndexIteratorArray; // thread local context

                bool allSimpleRanges = !(this is CellStorage);
                int i = 0;
                for (; i < HANDLES_NDIM - 1; i++) {
                    iterators[i] = getCheckIterator(dims[i], (uint)i, m_size[i] - 1, ref allSimpleRanges);
                }
                iterators[i] = getCheckIterator(dims[i], (uint)i, m_size.GetLastDimIdxForMLSubarray((uint)i), ref allSimpleRanges);

                StorageT thisStorage = this as StorageT;
                if (iterators[i].GetMaximum() >= m_size[i]) {
                    thisStorage = thisStorage.EnsureStorageOrder(StorageOrders.ColumnMajor, inplace: fromRetT);
                }
                if (allSimpleRanges) {
                    return thisStorage.createBSDSubarrayML(iterators, HANDLES_NDIM, fromRetT);
                }
                return thisStorage.iterateSubarrayML_StrgT(iterators, HANDLES_NDIM, fromRetT);
            }
        }

        #endregion

        #region HYCALPER LOOPSTART

        /*!HC:TYPELIST:
        <hycalper>
        <type>
            <source locate="here">
                CopyTo_2
            </source>
            <destination>CopyTo_1</destination>
            <destination>CopyTo_4</destination>
            <destination>CopyTo_8</destination>
            <destination>CopyTo_16</destination>
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
        
        internal void CopyTo_2(StorageT srcStorage, StorageT destStorage, IIndexIterator[] iterators, uint ndimsOut) {

            // This handles empty storages! (by handling empty iterators)

            var srcSize = srcStorage.m_size;
            uint ndimsIn = srcSize.NumberOfDimensions;

            long stride0 = (ndimsOut == 1) ? srcSize.GetStride4MLlastDimExpansion(0) : srcSize.GetStride(0);
            var it0 = iterators[0];
            if (it0.GetLength() == 0) return;
            long higdims = 0;
            for (uint i = 1; i < ndimsOut; i++) {
                if (!iterators[i].MoveNext()) {
                    return; // nothing to do. Empty right side.
                }
                // OOR should not happen, since IIterators are always checked for OOR during creation (in this path): 
                // System.Diagnostics.Debug.Assert(val <= iterators[i].GetLastDimensionIndex());
                // throw new IndexOutOfRangeException($"Index '{val}' exceeds the valid range of dimension {i}: 0 <= i < {iterators[i].GetLastDimensionIndex()}.");
                higdims += (iterators[i].Current *
                           ((i == ndimsOut - 1) ? srcSize.GetStride4MLlastDimExpansion(i) : srcSize.GetStride(i)));
            }

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            ushort* pOut = (ushort*)destStorage.m_handles[0].Pointer + destStorage.m_size.BaseOffset;
            ushort* pIn = (ushort*)srcStorage.m_handles[0].Pointer + srcSize.BaseOffset;
            while (true) {

                while (it0.MoveNext()) {
                    pOut[0] = pIn[higdims + it0.Current * stride0];
                    pOut++;
                };
                it0.Reset();

                // increase higher dims
                uint d = 1;
                while (d < ndimsOut) {
                    var itD = iterators[d];
                    var oldIdx = itD.Current;

                    if (itD.MoveNext()) {
                        var val = itD.Current;
                        higdims += (val - oldIdx)
                                * ((d == ndimsOut - 1) ? srcSize.GetStride4MLlastDimExpansion(d) : srcSize.GetStride(d));
                        break;
                    } else {
                        itD.Reset();
                        itD.MoveNext();  // assuming this succeeds
                        higdims += (itD.Current - oldIdx)
                                * ((d == ndimsOut - 1) ? srcSize.GetStride4MLlastDimExpansion(d) : srcSize.GetStride(d));
                        d++;
                    }
                }
                if (d == ndimsOut) return;
            }
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 

       
        
        internal void CopyTo_16(StorageT srcStorage, StorageT destStorage, IIndexIterator[] iterators, uint ndimsOut) {

            // This handles empty storages! (by handling empty iterators)

            var srcSize = srcStorage.m_size;
            uint ndimsIn = srcSize.NumberOfDimensions;

            long stride0 = (ndimsOut == 1) ? srcSize.GetStride4MLlastDimExpansion(0) : srcSize.GetStride(0);
            var it0 = iterators[0];
            if (it0.GetLength() == 0) return;
            long higdims = 0;
            for (uint i = 1; i < ndimsOut; i++) {
                if (!iterators[i].MoveNext()) {
                    return; // nothing to do. Empty right side.
                }
                // OOR should not happen, since IIterators are always checked for OOR during creation (in this path): 
                // System.Diagnostics.Debug.Assert(val <= iterators[i].GetLastDimensionIndex());
                // throw new IndexOutOfRangeException($"Index '{val}' exceeds the valid range of dimension {i}: 0 <= i < {iterators[i].GetLastDimensionIndex()}.");
                higdims += (iterators[i].Current *
                           ((i == ndimsOut - 1) ? srcSize.GetStride4MLlastDimExpansion(i) : srcSize.GetStride(i)));
            }

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            complex* pOut = (complex*)destStorage.m_handles[0].Pointer + destStorage.m_size.BaseOffset;
            complex* pIn = (complex*)srcStorage.m_handles[0].Pointer + srcSize.BaseOffset;
            while (true) {

                while (it0.MoveNext()) {
                    pOut[0] = pIn[higdims + it0.Current * stride0];
                    pOut++;
                };
                it0.Reset();

                // increase higher dims
                uint d = 1;
                while (d < ndimsOut) {
                    var itD = iterators[d];
                    var oldIdx = itD.Current;

                    if (itD.MoveNext()) {
                        var val = itD.Current;
                        higdims += (val - oldIdx)
                                * ((d == ndimsOut - 1) ? srcSize.GetStride4MLlastDimExpansion(d) : srcSize.GetStride(d));
                        break;
                    } else {
                        itD.Reset();
                        itD.MoveNext();  // assuming this succeeds
                        higdims += (itD.Current - oldIdx)
                                * ((d == ndimsOut - 1) ? srcSize.GetStride4MLlastDimExpansion(d) : srcSize.GetStride(d));
                        d++;
                    }
                }
                if (d == ndimsOut) return;
            }
        }

       
        
        internal void CopyTo_8(StorageT srcStorage, StorageT destStorage, IIndexIterator[] iterators, uint ndimsOut) {

            // This handles empty storages! (by handling empty iterators)

            var srcSize = srcStorage.m_size;
            uint ndimsIn = srcSize.NumberOfDimensions;

            long stride0 = (ndimsOut == 1) ? srcSize.GetStride4MLlastDimExpansion(0) : srcSize.GetStride(0);
            var it0 = iterators[0];
            if (it0.GetLength() == 0) return;
            long higdims = 0;
            for (uint i = 1; i < ndimsOut; i++) {
                if (!iterators[i].MoveNext()) {
                    return; // nothing to do. Empty right side.
                }
                // OOR should not happen, since IIterators are always checked for OOR during creation (in this path): 
                // System.Diagnostics.Debug.Assert(val <= iterators[i].GetLastDimensionIndex());
                // throw new IndexOutOfRangeException($"Index '{val}' exceeds the valid range of dimension {i}: 0 <= i < {iterators[i].GetLastDimensionIndex()}.");
                higdims += (iterators[i].Current *
                           ((i == ndimsOut - 1) ? srcSize.GetStride4MLlastDimExpansion(i) : srcSize.GetStride(i)));
            }

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            ulong* pOut = (ulong*)destStorage.m_handles[0].Pointer + destStorage.m_size.BaseOffset;
            ulong* pIn = (ulong*)srcStorage.m_handles[0].Pointer + srcSize.BaseOffset;
            while (true) {

                while (it0.MoveNext()) {
                    pOut[0] = pIn[higdims + it0.Current * stride0];
                    pOut++;
                };
                it0.Reset();

                // increase higher dims
                uint d = 1;
                while (d < ndimsOut) {
                    var itD = iterators[d];
                    var oldIdx = itD.Current;

                    if (itD.MoveNext()) {
                        var val = itD.Current;
                        higdims += (val - oldIdx)
                                * ((d == ndimsOut - 1) ? srcSize.GetStride4MLlastDimExpansion(d) : srcSize.GetStride(d));
                        break;
                    } else {
                        itD.Reset();
                        itD.MoveNext();  // assuming this succeeds
                        higdims += (itD.Current - oldIdx)
                                * ((d == ndimsOut - 1) ? srcSize.GetStride4MLlastDimExpansion(d) : srcSize.GetStride(d));
                        d++;
                    }
                }
                if (d == ndimsOut) return;
            }
        }

       
        
        internal void CopyTo_4(StorageT srcStorage, StorageT destStorage, IIndexIterator[] iterators, uint ndimsOut) {

            // This handles empty storages! (by handling empty iterators)

            var srcSize = srcStorage.m_size;
            uint ndimsIn = srcSize.NumberOfDimensions;

            long stride0 = (ndimsOut == 1) ? srcSize.GetStride4MLlastDimExpansion(0) : srcSize.GetStride(0);
            var it0 = iterators[0];
            if (it0.GetLength() == 0) return;
            long higdims = 0;
            for (uint i = 1; i < ndimsOut; i++) {
                if (!iterators[i].MoveNext()) {
                    return; // nothing to do. Empty right side.
                }
                // OOR should not happen, since IIterators are always checked for OOR during creation (in this path): 
                // System.Diagnostics.Debug.Assert(val <= iterators[i].GetLastDimensionIndex());
                // throw new IndexOutOfRangeException($"Index '{val}' exceeds the valid range of dimension {i}: 0 <= i < {iterators[i].GetLastDimensionIndex()}.");
                higdims += (iterators[i].Current *
                           ((i == ndimsOut - 1) ? srcSize.GetStride4MLlastDimExpansion(i) : srcSize.GetStride(i)));
            }

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            uint* pOut = (uint*)destStorage.m_handles[0].Pointer + destStorage.m_size.BaseOffset;
            uint* pIn = (uint*)srcStorage.m_handles[0].Pointer + srcSize.BaseOffset;
            while (true) {

                while (it0.MoveNext()) {
                    pOut[0] = pIn[higdims + it0.Current * stride0];
                    pOut++;
                };
                it0.Reset();

                // increase higher dims
                uint d = 1;
                while (d < ndimsOut) {
                    var itD = iterators[d];
                    var oldIdx = itD.Current;

                    if (itD.MoveNext()) {
                        var val = itD.Current;
                        higdims += (val - oldIdx)
                                * ((d == ndimsOut - 1) ? srcSize.GetStride4MLlastDimExpansion(d) : srcSize.GetStride(d));
                        break;
                    } else {
                        itD.Reset();
                        itD.MoveNext();  // assuming this succeeds
                        higdims += (itD.Current - oldIdx)
                                * ((d == ndimsOut - 1) ? srcSize.GetStride4MLlastDimExpansion(d) : srcSize.GetStride(d));
                        d++;
                    }
                }
                if (d == ndimsOut) return;
            }
        }

       
        
        internal void CopyTo_1(StorageT srcStorage, StorageT destStorage, IIndexIterator[] iterators, uint ndimsOut) {

            // This handles empty storages! (by handling empty iterators)

            var srcSize = srcStorage.m_size;
            uint ndimsIn = srcSize.NumberOfDimensions;

            long stride0 = (ndimsOut == 1) ? srcSize.GetStride4MLlastDimExpansion(0) : srcSize.GetStride(0);
            var it0 = iterators[0];
            if (it0.GetLength() == 0) return;
            long higdims = 0;
            for (uint i = 1; i < ndimsOut; i++) {
                if (!iterators[i].MoveNext()) {
                    return; // nothing to do. Empty right side.
                }
                // OOR should not happen, since IIterators are always checked for OOR during creation (in this path): 
                // System.Diagnostics.Debug.Assert(val <= iterators[i].GetLastDimensionIndex());
                // throw new IndexOutOfRangeException($"Index '{val}' exceeds the valid range of dimension {i}: 0 <= i < {iterators[i].GetLastDimensionIndex()}.");
                higdims += (iterators[i].Current *
                           ((i == ndimsOut - 1) ? srcSize.GetStride4MLlastDimExpansion(i) : srcSize.GetStride(i)));
            }

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            byte* pOut = (byte*)destStorage.m_handles[0].Pointer + destStorage.m_size.BaseOffset;
            byte* pIn = (byte*)srcStorage.m_handles[0].Pointer + srcSize.BaseOffset;
            while (true) {

                while (it0.MoveNext()) {
                    pOut[0] = pIn[higdims + it0.Current * stride0];
                    pOut++;
                };
                it0.Reset();

                // increase higher dims
                uint d = 1;
                while (d < ndimsOut) {
                    var itD = iterators[d];
                    var oldIdx = itD.Current;

                    if (itD.MoveNext()) {
                        var val = itD.Current;
                        higdims += (val - oldIdx)
                                * ((d == ndimsOut - 1) ? srcSize.GetStride4MLlastDimExpansion(d) : srcSize.GetStride(d));
                        break;
                    } else {
                        itD.Reset();
                        itD.MoveNext();  // assuming this succeeds
                        higdims += (itD.Current - oldIdx)
                                * ((d == ndimsOut - 1) ? srcSize.GetStride4MLlastDimExpansion(d) : srcSize.GetStride(d));
                        d++;
                    }
                }
                if (d == ndimsOut) return;
            }
        }

#endregion HYCALPER AUTO GENERATED CODE

        
        internal void CopyTo_Arbitrary(StorageT srcStorage, StorageT destStorage, IIndexIterator[] iterators, uint ndimsOut, uint elementSize) {

            // This handles empty storages! (by handling empty iterators)

            var srcSize = srcStorage.m_size;
            uint ndimsIn = srcSize.NumberOfDimensions;

            long stride0 = (ndimsOut == 1) ? srcSize.GetStride4MLlastDimExpansion(0) : srcSize.GetStride(0);
            var it0 = iterators[0];
            if (it0.GetLength() == 0) return;
            long higdims = 0;
            for (uint i = 1; i < ndimsOut; i++) {
                if (!iterators[i].MoveNext()) {
                    return; // nothing to do. Empty right side.
                }
                // OOR should not happen, since IIterators are always checked for OOR during creation (in this path): 
                // System.Diagnostics.Debug.Assert(val <= iterators[i].GetLastDimensionIndex());
                // throw new IndexOutOfRangeException($"Index '{val}' exceeds the valid range of dimension {i}: 0 <= i < {iterators[i].GetLastDimensionIndex()}.");
                higdims += (iterators[i].Current *
                           ((i == ndimsOut - 1) ? srcSize.GetStride4MLlastDimExpansion(i) : srcSize.GetStride(i)));
            }

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            byte* pOut = (byte*)destStorage.m_handles[0].Pointer + destStorage.m_size.BaseOffset * elementSize;
            byte* pIn = (byte*)srcStorage.m_handles[0].Pointer + srcSize.BaseOffset * elementSize;
            while (true) {

                while (it0.MoveNext()) {
                    for (int i = 0; i < elementSize; i++) {
                        pOut[i] = pIn[(higdims + it0.Current * stride0) * elementSize + i];
                    }
                    pOut += elementSize;
                };
                it0.Reset();

                // increase higher dims
                uint d = 1;
                while (d < ndimsOut) {
                    var itD = iterators[d];
                    var oldIdx = itD.Current;

                    if (itD.MoveNext()) {
                        var val = itD.Current;
                        higdims += (val - oldIdx)
                                * ((d == ndimsOut - 1) ? srcSize.GetStride4MLlastDimExpansion(d) : srcSize.GetStride(d));
                        break;
                    } else {
                        itD.Reset();
                        itD.MoveNext();  // assuming this succeeds
                        higdims += (itD.Current - oldIdx)
                                * ((d == ndimsOut - 1) ? srcSize.GetStride4MLlastDimExpansion(d) : srcSize.GetStride(d));
                        d++;
                    }
                }
                if (d == ndimsOut) return;
            }
        }


        #region helpers
        internal static void substituteEllipsis(DimSpec[] source, DimSpec[] dest, ref uint hANDLES_NDIM, uint ndims, int i) {
            // copy previous dimspecs over to destination array
            if (!ReferenceEquals(source, dest) && i > 0) {
                Array.Copy(source, dest, i);
            }
            System.Diagnostics.Debug.Assert(i < hANDLES_NDIM);
            if (hANDLES_NDIM == ndims) {
                // replace ellipsis by ':' / full
                dest[i] = Globals.full;
                if (!ReferenceEquals(source, dest)) {
                    Array.Copy(source, i + 1, dest, i + 1, ndims - i - 1);
                }
            } else if (hANDLES_NDIM > ndims) {
                // ellipsis is just removed
                for (int k = i; k < hANDLES_NDIM - 1; k++) {
                    dest[k] = source[k + 1];
                }
                // Array.Copy(source, i + 1, dest, i, hANDLES_NDIM - i - 1); does not work on same source / dest! 
                hANDLES_NDIM--;
            } else if (i < ndims) {
                // fills gap with fulls
                var gap = ndims - hANDLES_NDIM + 1;
                // Array.Copy(source, i + 1, dest, gap + i, hANDLES_NDIM - i - 1); // does not work on same source / dest! 

                for (var k = hANDLES_NDIM - i - 1; k-- > 0;) { // copy all specifiers after the gap first! Start at last entry! this way we won't overwrite any target index and can copy inplace. 
                    dest[gap + i + k] = source[k + i + 1];
                }

                for (int k = i; k < gap + i; k++) {
                    dest[k] = Globals.full;
                }
                hANDLES_NDIM = ndims;
            }
        }
        internal void substituteEllipsis(BaseArray[] source, BaseArray[] dest, ref uint hANDLES_NDIM, uint ndims, int i) {
            // copy previous dimspecs over to destination array
            if (!ReferenceEquals(source, dest) && i > 0) {
                Array.Copy(source, dest, i);
            }
            System.Diagnostics.Debug.Assert(i < hANDLES_NDIM);
            if (hANDLES_NDIM == ndims) {
                // replace ellipsis by ':' / full
                dest[i] = Globals.full;
                if (!ReferenceEquals(source, dest)) {
                    Array.Copy(source, i + 1, dest, i + 1, ndims - i - 1);
                }
            } else if (hANDLES_NDIM > ndims) {
                // ellipsis is just removed
                for (int k = i; k < hANDLES_NDIM - 1; k++) {
                    dest[k] = source[k + 1];
                }
                // Array.Copy(source, i + 1, dest, i, hANDLES_NDIM - i - 1); does not work on same source / dest! 
                hANDLES_NDIM--;
            } else if (i < ndims) {
                // fills gap with fulls
                var gap = ndims - hANDLES_NDIM + 1;
                // Array.Copy(source, i + 1, dest, gap + i, hANDLES_NDIM - i - 1); // does not work on same source / dest! 

                for (var k = hANDLES_NDIM - i - 1; k-- > 0;) { // copy all specifiers after the gap first! Start at last entry! this way we won't overwrite any target index and can copy inplace. 
                    dest[gap + i + k] = source[k + i + 1];
                }

                for (int k = i; k < gap + i; k++) {
                    dest[k] = Globals.full;
                }
                hANDLES_NDIM = ndims;
            }
        }

        /// <summary>
        /// Checks if storage order is suitable for ML last dimension expansion (ColumnMajor required), prepares a storage accordingly. 
        /// </summary>
        /// <param name="d">Dimspec specified for the last dimension.</param>
        /// <param name="index">Index of the last specified dimension.</param>
        /// <param name="fromRetT">True when GetRange was called on a RetT array. False otherwise.</param>
        /// <returns>Tuple: ret is the storage which is to be used in subsequent operations of this subarray() call. 'thisStorage' is 
        /// the storage substituting this storage due to modifications performed to it). Both may refer to this storage.</returns>
        private (StorageT ret, StorageT thisStorage) checkPrepareGetRangeML(DimSpec d, uint index, bool fromRetT) {

            StorageT thisStorage = this as StorageT;

            if (d.End >= m_size[index]) {
                // current End does not fit into this dimension.
                thisStorage = thisStorage.EnsureStorageOrder(StorageOrders.ColumnMajor, inplace: fromRetT);
            }

            // fits inside this dimensions
            System.Diagnostics.Debug.Assert(Settings.MinNumberOfArrayDimensions <= 2);

            StorageT ret;
            // all we need is a storage whose Size can be modified. The buffer set remains untouched for all DimSpec indices.
            if (fromRetT && thisStorage.m_arrayCounter == 1 && thisStorage.GetAsynchReferencesCount() == 1) {
                ret = thisStorage;
            } else {
                ret = Create(thisStorage.Handles, thisStorage.Size);
            }

            return (ret, thisStorage);
        }

        
        internal virtual void CopyTo_T(StorageT srcStorage, StorageT destStorage, IIndexIterator[] iterators, uint ndimsOut) {

            // This handles empty storages! (by handling empty iterators)

            var srcSize = srcStorage.m_size;

            long stride0 = (ndimsOut == 1) ? srcSize.GetStride4MLlastDimExpansion(0) : srcSize.GetStride(0);
            var it0 = iterators[0];
            if (it0.GetLength() == 0) return;
            long higdims = srcStorage.m_size.BaseOffset;
            for (uint i = 1; i < ndimsOut; i++) {
                if (!iterators[i].MoveNext()) {
                    return; // nothing to do. Empty right side.
                }
                // OOR should not happen, since IIterators are always checked for OOR during creation (in this path): 
                // System.Diagnostics.Debug.Assert(val <= iterators[i].GetLastDimensionIndex());
                // throw new IndexOutOfRangeException($"Index '{val}' exceeds the valid range of dimension {i}: 0 <= i < {iterators[i].GetLastDimensionIndex()}.");
                higdims += (iterators[i].Current *
                    ((i == ndimsOut - 1) ? srcSize.GetStride4MLlastDimExpansion(i) : srcSize.GetStride(i)));
            }

            System.Diagnostics.Debug.Assert(!typeof(T).IsValueType);

            T[] pOut = (destStorage.m_handles[0] as ManagedHostHandle<T>).HostArray; // + destStorage.m_size.BaseOffset;
            T[] pIn = (srcStorage.m_handles[0] as ManagedHostHandle<T>).HostArray; // + srcBSD[2].ToUInt64();
            long posOut = destStorage.m_size.BaseOffset;
            while (true) {

                while (it0.MoveNext()) {
                    pOut[posOut] = pIn[higdims + it0.Current * stride0];
                    posOut++;
                };
                it0.Reset();

                // increase higher dims
                uint d = 1;
                while (d < ndimsOut) {
                    var itD = iterators[d];
                    var oldIdx = itD.Current;

                    if (itD.MoveNext()) {
                        higdims += (itD.Current - oldIdx)
                            * ((d == ndimsOut - 1) ? srcSize.GetStride4MLlastDimExpansion(d) : srcSize.GetStride(d));
                        break;
                    } else {
                        itD.Reset();
                        itD.MoveNext();  // assuming this succeeds
                        higdims += (itD.Current - oldIdx)
                            * ((d == ndimsOut - 1) ? srcSize.GetStride4MLlastDimExpansion(d) : srcSize.GetStride(d));
                        d++;
                    }
                }
                if (d == ndimsOut) return;
            }
        }

        private StorageT createBSDSubarrayML(IIndexIterator[] iterators, uint ndims, bool fromRetT) {

            System.Diagnostics.Debug.Assert(Settings.MinNumberOfArrayDimensions <= 2);
            System.Diagnostics.Debug.Assert(ndims >= Settings.MinNumberOfArrayDimensions);

            // no 'inplace' here for simplicity: we would have to temp-store current strides in the BSD to 
            // prevent them from becoming overwritten by dim lengths when in/out ndims differ.
            
            m_handles.Retain();
            StorageT ret = Create(m_handles);

            var bsd = ret.m_size.GetBSD(write: true);
            long baseOffset = m_size.BaseOffset;
            long nelem = 1;
            for (uint i = 0; i < ndims; i++) {
                var it = iterators[i];
                System.Diagnostics.Debug.Assert(it != null && it.GetStepSize() >= 0);
                var stride = (i == ndims - 1) ? m_size.GetStride4MLlastDimExpansion(i) : m_size.GetStride(i);
                bsd[3 + i] = (it.GetLength());
                bsd[3 + ndims + i] = (bsd[3 + i] == 1 ? 0 : (stride * it.GetStepSize().GetValueOrDefault()));

                var min = it.GetMinimum().GetValueOrDefault();
                if (min < 0) {
                    min += (it.GetLastDimensionIndex() + 1);
                }

                baseOffset += stride * min;
                nelem *= it.GetLength();
            }
            bsd[0] = (ndims);
            bsd[1] = (nelem);
            bsd[2] = (baseOffset);
            return ret;

        }
        
        internal RetT iterateSubarrayML(IIndexIterator[] iterators, uint len, bool fromRetT) {
            return iterateSubarrayML_StrgT(iterators, len, fromRetT).RetArray;
        }

        
        internal StorageT iterateSubarrayML_StrgT(IIndexIterator[] iterators, uint len, bool fromRetT) {

            // SetRange(): 'this' storage is either a new storage as clone (ref count: 0) of -, or it is the entry storage to SetRange() in the Remove() branch.

            System.Diagnostics.Debug.Assert(iterators != null);
            System.Diagnostics.Debug.Assert(iterators.Length >= len);
            long outLen = 1;
            uint ndims = Math.Max(len, Settings.MinNumberOfArrayDimensions);
            var ret = Create();  // 'fromRetT' is currently not utilized. RefCount = 0. 

            #region prepare Out BSD

            var outBSD = ret.m_size.GetBSD(true);

            int i = 0;
            for (; i < len; i++) {
                System.Diagnostics.Debug.Assert(iterators[i] != null);
                var dimLen = iterators[i].GetLength();
                outBSD[3 + i] = (dimLen);
                outBSD[3 + ndims + i] = dimLen == 1 ? 0 : outLen;
                outLen *= dimLen;
            }
            for (; i < ndims; i++) {
                // add singletons as neccessary
                outBSD[3 + i] = (1);
                outBSD[3 + ndims + i] = 0;
            }
            outBSD[0] = (ndims);
            outBSD[1] = (outLen);
            outBSD[2] = (0);
            #endregion

            ret.m_handles[0] = ret.New((ulong)outLen, clear: (this is CellStorage));

            if (!(ElementInstance is System.ValueType)) {
                CopyTo_T(this as StorageT, ret as StorageT, iterators, len);
            } else {
                switch (SizeOfT) {
                    case 1:
                        CopyTo_1(this as StorageT, ret as StorageT, iterators, len);
                        break;
                    case 2:
                        CopyTo_2(this as StorageT, ret as StorageT, iterators, len);
                        break;
                    case 4:
                        CopyTo_4(this as StorageT, ret as StorageT, iterators, len);
                        break;
                    case 8:
                        CopyTo_8(this as StorageT, ret as StorageT, iterators, len);
                        break;
                    case 16:
                        CopyTo_16(this as StorageT, ret as StorageT, iterators, len);
                        break;
                    default:
                        CopyTo_Arbitrary(this as StorageT, ret as StorageT, iterators, len, SizeOfT);
                        //throw new InvalidProgramException($"Unsupported element type: {typeof(T).Name}");
                        break;
                }
            }

            (ret as LogicalStorage)?.SetNumberTrues(-1);
            return ret;
        }

        /// <summary>
        /// Converts incoming base array index/indices into iterator. Source array is NOT released.
        /// </summary>
        /// <param name="d0"></param>
        /// <param name="dimIdx"></param>
        /// <param name="lastElementIDx"></param>
        /// <param name="isSimpleRange"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        protected internal IIndexIterator getCheckIterator(BaseArray d0, uint dimIdx, long lastElementIDx, ref bool isSimpleRange) {
            if (object.Equals(d0, null)) {
                throw new ArgumentException($"Invalid index argument for dimension {dimIdx}. Index specification must be a scalar array or a type, implicitly converting to a scalar array. Found: null");
            }
            if (d0 is BaseArray<string>) {
                var d0String = (d0 as ConcreteArray<string, Array<string>, InArray<string>, OutArray<string>, Array<string>, Storage<string>>).Storage;
                if (d0String.m_size.NumberOfElements != 1) {
                    throw new ArgumentException($"Invalid index argument for dimension {dimIdx}. Index specification must be a scalar array or a type, implicitly converting to a scalar array. Found: {d0String.Size.ToString()}.");
                }
                var strIndIt = new StringIndicesIterator(d0String.GetValue(0), lastElementIDx, checkLimits: true);
                isSimpleRange &= strIndIt.GetStepSize() >= 0;
                return strIndIt;  // <- boxing! :(
            } else if (d0 is DimSpec) {
                var dimSpec = d0 as DimSpec;
                dimSpec.Evaluate(lastElementIDx);
                // isSimpleRange is not affected. 
                return d0 as IIndexIterator;
            } else if (d0 is BaseArray<ILExpression>) {
                var endExpr = (d0 as ConcreteArray<ILExpression, Array<ILExpression>, InArray<ILExpression>, OutArray<ILExpression>, Array<ILExpression>, Storage<ILExpression>>).Storage;
                if (endExpr.m_size.NumberOfElements != 1) {
                    throw new ArgumentException($"Invalid index argument for dimension {dimIdx}. Index specification must be a scalar array or of a type, implicitly convertable to a scalar array. Found: {d0.Size.ToString()}. Did you mean to use cell() for concatenating multiple indices?");
                }
                var endInst = endExpr.GetValue(0);
                var endValue = ILExpression.Evaluate(endInst.Expression, lastElementIDx);
                if (endValue < 0 || endValue > lastElementIDx) {
                    throw new IndexOutOfRangeException($"'end' expression evaluates to an index out of range. Expected: 0 <= [end expression] <= {lastElementIDx}. Found: {endValue}.");
                }
                // isSimpleRange is not affected. 
                return new ScalarIndexIterator(endValue);
            } else if (d0 is Cell || d0 is InCell || d0 is OutCell) { // TODO: the C# compiler did not get the 'correct' attempt right: if (d0 is BaseArray<BaseArray>) {  "...is never of the provided type". But this is wrong! Maybe we messed with partial class declarations somewhere? Couldn't solve this and gave up. The current 'solution' is not optimal and should be improved. (not urgent, though)
                List<long> tmpInd = new List<long>();
                ConvertCellIndices2Long(d0 as BaseArray<BaseArray>, dimIdx, lastElementIDx, tmpInd, ref isSimpleRange);
                // d0 should NOT be released here
                d0 = MathInternal.array<long>(values: tmpInd);

            } else if (d0 is BaseArray<bool>) {
                isSimpleRange = false;
                return (d0 as BaseArray<bool>).IndexIterator(lastElementIDx, true, keepAlive: true);
            }
            IIndexIterator ret;
            // d0 will NOT be released by the iterator after the iteration finished.
            if (d0 is BaseArray<double>) {
                ret = (d0 as BaseArray<double>).IndexIterator(lastElementIDx, StorageOrders.ColumnMajor, checkLimits: true, keepAlive: true);
            } else if (d0 is BaseArray<int>) {
                ret = (d0 as BaseArray<int>).IndexIterator(lastElementIDx, StorageOrders.ColumnMajor, checkLimits: true, keepAlive: true);
            } else if (d0 is BaseArray<uint>) {
                ret = (d0 as BaseArray<uint>).IndexIterator(lastElementIDx, StorageOrders.ColumnMajor, checkLimits: true, keepAlive: true);
            } else if (d0 is BaseArray<long>) {
                ret = (d0 as BaseArray<long>).IndexIterator(lastElementIDx, StorageOrders.ColumnMajor, checkLimits: true, keepAlive: true);
            } else if (d0 is BaseArray<ulong>) {
                ret = (d0 as BaseArray<ulong>).IndexIterator(lastElementIDx, StorageOrders.ColumnMajor, checkLimits: true, keepAlive: true);
            } else if (d0 is BaseArray<float>) {
                ret = (d0 as BaseArray<float>).IndexIterator(lastElementIDx, StorageOrders.ColumnMajor, checkLimits: true, keepAlive: true);
            } else if (d0 is BaseArray<short>) {
                ret = (d0 as BaseArray<short>).IndexIterator(lastElementIDx, StorageOrders.ColumnMajor, checkLimits: true, keepAlive: true);
            } else if (d0 is BaseArray<ushort>) {
                ret = (d0 as BaseArray<ushort>).IndexIterator(lastElementIDx, StorageOrders.ColumnMajor, checkLimits: true, keepAlive: true);
            } else if (d0 is BaseArray<sbyte>) {
                ret = (d0 as BaseArray<sbyte>).IndexIterator(lastElementIDx, StorageOrders.ColumnMajor, checkLimits: true, keepAlive: true);
            } else if (d0 is BaseArray<byte>) {
                ret = (d0 as BaseArray<byte>).IndexIterator(lastElementIDx, StorageOrders.ColumnMajor, checkLimits: true, keepAlive: true);
            } else {
                throw new NotSupportedException($"Element type not supported as index array: {d0.GetType().GetGenericArguments()[0].Name}."); // don't release! 
            }
            if (ret.GetLength() > 0 && ret.GetMinimum() < -lastElementIDx - 1 || ret.GetMaximum() > lastElementIDx) {
                throw new IndexOutOfRangeException($"Index into dimension #{dimIdx} of {m_size.ToString()} array is out of range. Allowed: {-lastElementIDx - 1} <= i < {lastElementIDx + 1}. Found: i = {ret.GetMinimum()}.");
            }
            //isSimpleRange &= ret.GetStepSize().HasValue; 
            // we might be able to consider small arrays as "isSimpleRange". But we would have to consider the special case 
            // A[I] and had to track the shape of the index array to use it for the return array. All kinds of sublties 
            // arise: leading singleton dimensions, non-vector shaped index arrays etc. In the end, we simply disable 
            // 'view' indexing here, since it would make all much more complicated. 
            isSimpleRange = false;
            return ret;
        }
        internal IIndexIterator getCheckIteratorLeft(BaseArray d0, uint dimIdx, long lastElementIDx, ref bool expanding, ref long outLen) {
            if (object.Equals(d0, null)) {
                throw new ArgumentException($"Invalid index argument for dimension {dimIdx}. Index specification must be a numeric array or a type, implicitly converting to a numeric array. Found: null.");
            }
            if (d0 is BaseArray<string>) {
                var d0String = (d0 as ConcreteArray<string, Array<string>, InArray<string>, OutArray<string>, Array<string>, Storage<string>>).Storage;
                if (d0String.m_size.NumberOfElements != 1) {
                    throw new ArgumentException($"Invalid index argument for dimension {dimIdx}. Index specification must be a scalar array or a type, implicitly converting to a scalar array. Found: {(object.Equals(d0, null) ? "null" : d0.Size.ToString())}.");
                }
                var strIndIt = new StringIndicesIterator(d0String.GetValue(0), lastElementIDx, checkLimits: false);
                expanding |= (strIndIt.GetMaximum() > lastElementIDx) && (strIndIt.GetLength() > 0);
                outLen *= strIndIt.GetLength();

                return strIndIt;  // <- boxing! :(
            } else if (d0 is DimSpec) {
                var dimSpec = d0 as DimSpec;
                dimSpec.EvaluateLeft(lastElementIDx, ref expanding);
                outLen *= dimSpec.GetLength();
                return d0 as IIndexIterator;

            } else if (d0 is BaseArray<ILExpression>) {
                var endExpr = (d0 as ConcreteArray<ILExpression, Array<ILExpression>, InArray<ILExpression>, OutArray<ILExpression>, Array<ILExpression>, Storage<ILExpression>>).Storage;
                if (endExpr.m_size.NumberOfElements != 1) {
                    throw new ArgumentException($"Invalid index argument for dimension {dimIdx}. Index specification must be a scalar array or a type, implicitly converting to a scalar array. Found: {(object.Equals(d0, null) ? "null" : d0.Size.ToString())}. Did you mean to use cell() for concatenating multiple indices?");
                }
                var endInst = endExpr.GetValue(0);
                var endValue = ILExpression.Evaluate(endInst.Expression, lastElementIDx);
                if (endValue < 0) {
                    throw new IndexOutOfRangeException($"'end' expression evaluates to a negative value: {endValue}. Expected: positive value or 0.");
                }
                expanding |= endValue > lastElementIDx;

                return new ScalarIndexIterator(endValue);
            } else if (d0 is Cell || d0 is InCell || d0 is OutCell) { // TODO: the C# compiler did not get the 'correct' attempt right: if (d0 is BaseArray<BaseArray>) {  "...is never of the provided type". But this is wrong! Maybe we messed with partial class declarations somewhere? Couldn't solve this and gave up. The current 'solution' is not optimal and should be improved. (not urgent, though)
                List<long> tmpInd = new List<long>();
                bool dummy = false;
                ConvertCellIndices2Long(d0 as BaseArray<BaseArray>, dimIdx, lastElementIDx, tmpInd, ref dummy);
                // d0 should NOT be released here
                d0 = MathInternal.array<long>(values: tmpInd);

            }
            IIndexIterator ret;
            // d0 will NOT be disposed after the iteration finished
            if (d0 is BaseArray<double>) {
                ret = (d0 as BaseArray<double>).IndexIterator(lastElementIDx, StorageOrders.ColumnMajor, checkLimits: false, keepAlive: true);
            } else if (d0 is BaseArray<int>) {
                ret = (d0 as BaseArray<int>).IndexIterator(lastElementIDx, StorageOrders.ColumnMajor, checkLimits: false, keepAlive: true);
            } else if (d0 is BaseArray<bool>) {
                // create a regular array from BaseArray d0:
                var L = (d0.GetClonedStorage() as LogicalStorage).RetArray; 
                ret = MathInternal.find(L).IndexIterator(lastElementIDx, StorageOrders.ColumnMajor, checkLimits: false, keepAlive: false); 
            } else if (d0 is BaseArray<uint>) {
                ret = (d0 as BaseArray<uint>).IndexIterator(lastElementIDx, StorageOrders.ColumnMajor, checkLimits: false, keepAlive: true);
            } else if (d0 is BaseArray<long>) {
                ret = (d0 as BaseArray<long>).IndexIterator(lastElementIDx, StorageOrders.ColumnMajor, checkLimits: false, keepAlive: true);
            } else if (d0 is BaseArray<ulong>) {
                ret = (d0 as BaseArray<ulong>).IndexIterator(lastElementIDx, StorageOrders.ColumnMajor, checkLimits: false, keepAlive: true);
            } else if (d0 is BaseArray<float>) {
                ret = (d0 as BaseArray<float>).IndexIterator(lastElementIDx, StorageOrders.ColumnMajor, checkLimits: false, keepAlive: true);
            } else if (d0 is BaseArray<short>) {
                ret = (d0 as BaseArray<short>).IndexIterator(lastElementIDx, StorageOrders.ColumnMajor, checkLimits: false, keepAlive: true);
            } else if (d0 is BaseArray<ushort>) {
                ret = (d0 as BaseArray<ushort>).IndexIterator(lastElementIDx, StorageOrders.ColumnMajor, checkLimits: false, keepAlive: true);
            } else if (d0 is BaseArray<sbyte>) {
                ret = (d0 as BaseArray<sbyte>).IndexIterator(lastElementIDx, StorageOrders.ColumnMajor, checkLimits: false, keepAlive: true);
            } else if (d0 is BaseArray<byte>) {
                ret = (d0 as BaseArray<byte>).IndexIterator(lastElementIDx, StorageOrders.ColumnMajor, checkLimits: false, keepAlive: true);
            } else {
                throw new NotSupportedException($"Element type not supported as index array: {d0.GetType().GetGenericArguments()[0].Name}."); // don't release! 
            }
            if (ret.GetMinimum().HasValue && ret.GetMinimum() < -lastElementIDx) {
                if (lastElementIDx >= 0) { // empty array: lastElementIdx = -1
                    throw new IndexOutOfRangeException($"Index into dimension #{dimIdx} of {m_size.ToString()} array is out of range. Allowed: -{m_size[dimIdx]} <= i < {m_size[dimIdx]}. Found: i = {ret.GetMinimum()}.");
                }
            }
            if (ret.GetMaximum().HasValue) {
                expanding |= ret.GetMaximum() > lastElementIDx;
            }
            outLen *= ret.GetLength();
            return ret;
        }

        private void findLeftBoolExpansionFlags(LogicalStorage ls, uint dimIdx, ref bool expanding, ref long outLen, out long maxIdx) {
            maxIdx = -1;
            if (ls.m_size.NumberOfElements > m_size[dimIdx]) {
                // must check if there are any trues outside of the existing range. Keep the largest index value for later.
                long lastIdx = ls.m_size.NumberOfElements - 1;
                for (long i = 0; i <= lastIdx; i++) {
                    if (ls.GetValueSeq(i)) maxIdx = i;
                }
                expanding |= maxIdx >= m_size[dimIdx];
            }
            outLen *= ls.NumberTrues;
        }

        private void ConvertCellIndices2Long(BaseArray<BaseArray> cell, uint dimIdx, long lastIndexIDx, List<long> list, ref bool isSimpleRange) {
            foreach (var item in cell.Iterator(StorageOrders.ColumnMajor, keepAlive: true)) {
                var it = getCheckIterator(item, dimIdx, lastIndexIDx, ref isSimpleRange);
                list.AddRange(it);
            }
        }
        #endregion

    }
}  

