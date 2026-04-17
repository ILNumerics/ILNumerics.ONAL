//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////

using ILNumerics.Core.Arrays;
using ILNumerics.Core.Internal;
using ILNumerics.Core.MemoryLayer;
using ILNumerics.Core.Misc;
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
using static ILNumerics.Core.StorageLayer.Iterators;

namespace ILNumerics.Core.StorageLayer {
    public unsafe abstract partial class BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> : IDisposable, IStorage
        where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

        /**
         * These methods are used from the array layer, indexing interface. The return RetT (instead of StorageT from the ***_ACC segment interface). 
         * Memory management and the prevention from multiple disposes of incoming dimensional indices is done by only wrapping the 
         * mutating parts into a try { } finally {} block and releasing the dims only when entering this block. (Recursive) ellipsis handling 
         * is excluded from this block. Before an error is generated / thrown from outside the try-finally block ReleaseDims() is explicitly called. 
         * 
         * See also: memory management by an additional parameter 'releaseDims' in GetRange_np_Acc. 
         */

        #region fast subarray
        internal virtual StorageT GetRange_np(DimSpec[] dims, uint? len = null) {
            if (dims == null || (len ?? (uint)dims.Length) == 0) {

                #region return all
                return GetRange_np(Globals.ellipsis);
                #endregion

            }
            var HANDLES_NDIM = len ?? (uint)dims.Length;
            if (m_size.NumberOfDimensions <= 7) {
                switch (HANDLES_NDIM) {
                    case 1:
                        return GetRange_np(dims[0]);
                    case 2:
                        return GetRange_np(dims[0], dims[1]);
                    case 3:
                        return GetRange_np(dims[0], dims[1], dims[2]);
                    case 4:
                        return GetRange_np(dims[0], dims[1], dims[2], dims[3]);
                    case 5:
                        return GetRange_np(dims[0], dims[1], dims[2], dims[3], dims[4]);
                    case 6:
                        return GetRange_np(dims[0], dims[1], dims[2], dims[3], dims[4], dims[5]);
                    case 7:
                        return GetRange_np(dims[0], dims[1], dims[2], dims[3], dims[4], dims[5], dims[6]);
                    default:
                        break;
                }
            }

            #region handle ellipsis

            // np does not support newaxis! We count and substract the number of provided newaxis specifiers from the given indexing arguments.
            var nrNewaxis = countNewAxes(dims, HANDLES_NDIM);
            for (int i = 0; i < HANDLES_NDIM; i++) {
                if (dims[i] is EllipsisSpec) {                                       // TODO: CHECK THIS: simply size+nrNewaxis??
                    substituteEllipsis(dims, Context.DimSpecArray, ref HANDLES_NDIM, m_size.NumberOfDimensions + (uint)nrNewaxis, i);
                    dims = Context.DimSpecArray;
                    return GetRange_np(dims, HANDLES_NDIM);
                }
            }
            #endregion

            m_handles.Retain();
            StorageT ret = Create(m_handles);

            var bsd = ret.m_size.GetBSD(write: true);
            bsd[2] = m_size.BaseOffset;

            uint inDim = 0, outDim = 0;
            long outLen = 1;
            for (int i = 0; i < HANDLES_NDIM; i++) {
                checkEvaluate_np(dims[i], ref inDim, ref outDim, bsd, ref outLen);
            }
            finalizeBSD_np(bsd, inDim, outDim, outLen);

            (ret as LogicalStorage)?.SetNumberTrues(-1);
            return ret;
        }
        internal virtual StorageT GetRange_np(DimSpec d0) {

                if (d0 is EllipsisSpec || d0 is FullDimSpec) {
                    return Create(Handles, Size);  // no retain here! Storages returned are expected to have refcount 0 ! 
                }

                m_handles.Retain();
                StorageT ret = Create(m_handles);

                var bsd = ret.m_size.GetBSD(write: true);
                bsd[2] = (m_size.BaseOffset);

                uint inDim = 0, outDim = 0;
                long outLen = 1;
                checkEvaluate_np(d0, ref inDim, ref outDim, bsd, ref outLen);
                finalizeBSD_np(bsd, inDim, outDim, outLen);

                (ret as LogicalStorage)?.SetNumberTrues(-1);

                return ret;
        }
        internal virtual StorageT GetRange_np(DimSpec d0, DimSpec d1) {

            #region handle ellipsis
            if (d0 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions + countNewAxes(1, d1)) {
                    case 0:
                    case 1:
                        return GetRange_np(d1);
                    case 2:
                        return GetRange_np(Globals.full, d1);
                    case 3:
                        return GetRange_np(Globals.full, Globals.full, d1);
                    case 4:
                        return GetRange_np(Globals.full, Globals.full, Globals.full, d1);
                    case 5:
                        return GetRange_np(Globals.full, Globals.full, Globals.full, Globals.full, d1);
                    case 6:
                        return GetRange_np(Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, d1);
                    case 7:
                        return GetRange_np(Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, d1);
                    default:
                        var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; d0 = d1 = null;
                        return GetRange_np(tmp, 2);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d1 is EllipsisSpec) {
                switch (m_size.NumberOfDimensions + countNewAxes(1, d0)) {
                    case 0:
                    case 1:
                        return GetRange_np(d0);
                    case 2:
                        return GetRange_np(d0, Globals.full);
                    case 3:
                        return GetRange_np(d0, Globals.full, Globals.full);
                    case 4:
                        return GetRange_np(d0, Globals.full, Globals.full, Globals.full);
                    case 5:
                        return GetRange_np(d0, Globals.full, Globals.full, Globals.full, Globals.full);
                    case 6:
                        return GetRange_np(d0, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full);
                    case 7:
                        return GetRange_np(d0, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full);
                    default:
                        var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; d0 = d1 = null;
                        return GetRange_np(tmp, 2);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            #endregion

            m_handles.Retain();
            StorageT ret = Create(m_handles);

            var bsd = ret.m_size.GetBSD(write: true);
            bsd[2] = (m_size.BaseOffset);

            uint inDim = 0, outDim = 0;
            long outLen = 1;
            checkEvaluate_np(d0, ref inDim, ref outDim, bsd, ref outLen);
            checkEvaluate_np(d1, ref inDim, ref outDim, bsd, ref outLen);
            finalizeBSD_np(bsd, inDim, outDim, outLen);

            (ret as LogicalStorage)?.SetNumberTrues(-1);
            return ret;

        }
        internal virtual StorageT GetRange_np(DimSpec d0, DimSpec d1, DimSpec d2) {
                System.Diagnostics.Debug.Assert(Settings.ArrayStyle == ArrayStyles.numpy);

                #region handle ellipsis
                if (d0 is EllipsisSpec) {
                    switch (m_size.NumberOfDimensions + countNewAxes(2, d1, d2)) {
                        case 0:
                        case 1:
                        case 2:
                            return GetRange_np(d1, d2);
                        case 3:
                            return GetRange_np(Globals.full, d1, d2);
                        case 4:
                            return GetRange_np(Globals.full, Globals.full, d1, d2);
                        case 5:
                            return GetRange_np(Globals.full, Globals.full, Globals.full, d1, d2);
                        case 6:
                            return GetRange_np(Globals.full, Globals.full, Globals.full, Globals.full, d1, d2);
                        case 7:
                            return GetRange_np(Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, d1, d2);
                        default:
                            var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2;  
                            return GetRange_np(tmp, 3);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d1 is EllipsisSpec) {
                    switch (m_size.NumberOfDimensions + countNewAxes(2, d0, d2)) {
                        case 0:
                        case 1:
                        case 2:
                            return GetRange_np(d0, d2);
                        case 3:
                            return GetRange_np(d0, Globals.full, d2);
                        case 4:
                            return GetRange_np(d0, Globals.full, Globals.full, d2);
                        case 5:
                            return GetRange_np(d0, Globals.full, Globals.full, Globals.full, d2);
                        case 6:
                            return GetRange_np(d0, Globals.full, Globals.full, Globals.full, Globals.full, d2);
                        case 7:
                            return GetRange_np(d0, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, d2);
                        default:
                            var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2;  
                            return GetRange_np(tmp, 3);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d2 is EllipsisSpec) {
                    switch (m_size.NumberOfDimensions + countNewAxes(2, d0, d1)) {
                        case 0:
                        case 1:
                        case 2:
                            return GetRange_np(d0, d1);
                        case 3:
                            return GetRange_np(d0, d1, Globals.full);
                        case 4:
                            return GetRange_np(d0, d1, Globals.full, Globals.full);
                        case 5:
                            return GetRange_np(d0, d1, Globals.full, Globals.full, Globals.full);
                        case 6:
                            return GetRange_np(d0, d1, Globals.full, Globals.full, Globals.full, Globals.full);
                        case 7:
                            return GetRange_np(d0, d1, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full);
                        default:
                            var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2;  
                            return GetRange_np(tmp, 3);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                #endregion

                m_handles.Retain();
                StorageT ret = Create(m_handles);

                var bsd = ret.m_size.GetBSD(write: true);
                bsd[2] = (m_size.BaseOffset);

                uint inDim = 0, outDim = 0;
                long outLen = 1;
                checkEvaluate_np(d0, ref inDim, ref outDim, bsd, ref outLen);
                checkEvaluate_np(d1, ref inDim, ref outDim, bsd, ref outLen);
                checkEvaluate_np(d2, ref inDim, ref outDim, bsd, ref outLen);
                finalizeBSD_np(bsd, inDim, outDim, outLen);

                (ret as LogicalStorage)?.SetNumberTrues(-1);
                return ret;
        }
        internal virtual StorageT GetRange_np(DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3) {

                System.Diagnostics.Debug.Assert(Settings.ArrayStyle == ArrayStyles.numpy);

                #region handle ellipsis
                if (d0 is EllipsisSpec) {
                    switch (m_size.NumberOfDimensions + countNewAxes(3, d1, d2, d3)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                            return GetRange_np(d1, d2, d3);
                        case 4:
                            return GetRange_np(Globals.full, d1, d2, d3);
                        case 5:
                            return GetRange_np(Globals.full, Globals.full, d1, d2, d3);
                        case 6:
                            return GetRange_np(Globals.full, Globals.full, Globals.full, d1, d2, d3);
                        case 7:
                            return GetRange_np(Globals.full, Globals.full, Globals.full, Globals.full, d1, d2, d3);
                        default:
                            var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3;     
                            return GetRange_np(tmp, 4);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d1 is EllipsisSpec) {
                    switch (m_size.NumberOfDimensions + countNewAxes(3, d0, d2, d3)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                            return GetRange_np(d0, d2, d3);
                        case 4:
                            return GetRange_np(d0, Globals.full, d2, d3);
                        case 5:
                            return GetRange_np(d0, Globals.full, Globals.full, d2, d3);
                        case 6:
                            return GetRange_np(d0, Globals.full, Globals.full, Globals.full, d2, d3);
                        case 7:
                            return GetRange_np(d0, Globals.full, Globals.full, Globals.full, Globals.full, d2, d3);
                        default:
                            var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; 
                            return GetRange_np(tmp, 4);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d2 is EllipsisSpec) {
                    switch (m_size.NumberOfDimensions + countNewAxes(3, d0, d1, d3)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                            return GetRange_np(d0, d1, d3);
                        case 4:
                            return GetRange_np(d0, d1, Globals.full, d3);
                        case 5:
                            return GetRange_np(d0, d1, Globals.full, Globals.full, d3);
                        case 6:
                            return GetRange_np(d0, d1, Globals.full, Globals.full, Globals.full, d3);
                        case 7:
                            return GetRange_np(d0, d1, Globals.full, Globals.full, Globals.full, Globals.full, d3);
                        default:
                            var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3;     
                            return GetRange_np(tmp, 4);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d3 is EllipsisSpec) {
                    switch (m_size.NumberOfDimensions + countNewAxes(3, d0, d1, d2)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                            return GetRange_np(d0, d1, d2);
                        case 4:
                            return GetRange_np(d0, d1, d2, Globals.full);
                        case 5:
                            return GetRange_np(d0, d1, d2, Globals.full, Globals.full);
                        case 6:
                            return GetRange_np(d0, d1, d2, Globals.full, Globals.full, Globals.full);
                        case 7:
                            return GetRange_np(d0, d1, d2, Globals.full, Globals.full, Globals.full, Globals.full);
                        default:
                            var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3;     
                            return GetRange_np(tmp, 4);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                #endregion

                m_handles.Retain();
                StorageT ret = Create(m_handles);

                var bsd = ret.m_size.GetBSD(write: true);
                bsd[2] = (m_size.BaseOffset);

                uint inDim = 0, outDim = 0;
                long outLen = 1;
                checkEvaluate_np(d0, ref inDim, ref outDim, bsd, ref outLen);
                checkEvaluate_np(d1, ref inDim, ref outDim, bsd, ref outLen);
                checkEvaluate_np(d2, ref inDim, ref outDim, bsd, ref outLen);
                checkEvaluate_np(d3, ref inDim, ref outDim, bsd, ref outLen);
                finalizeBSD_np(bsd, inDim, outDim, outLen);

                (ret as LogicalStorage)?.SetNumberTrues(-1);
                return ret;
        }
        internal virtual StorageT GetRange_np(DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3, DimSpec d4) {
                System.Diagnostics.Debug.Assert(Settings.ArrayStyle == ArrayStyles.numpy);

                #region handle ellipsis
                if (d0 is EllipsisSpec) {
                    switch (m_size.NumberOfDimensions + countNewAxes(4, d1, d2, d3, d4)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            return GetRange_np(d1, d2, d3, d4);
                        case 5:
                            return GetRange_np(Globals.full, d1, d2, d3, d4);
                        case 6:
                            return GetRange_np(Globals.full, Globals.full, d1, d2, d3, d4);
                        case 7:
                            return GetRange_np(Globals.full, Globals.full, Globals.full, d1, d2, d3, d4);
                        default:
                            var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4;
                            return GetRange_np(tmp, 5);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d1 is EllipsisSpec) {
                    switch (m_size.NumberOfDimensions + countNewAxes(4, d0, d2, d3, d4)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            return GetRange_np(d0, d2, d3, d4);
                        case 5:
                            return GetRange_np(d0, Globals.full, d2, d3, d4);
                        case 6:
                            return GetRange_np(d0, Globals.full, Globals.full, d2, d3, d4);
                        case 7:
                            return GetRange_np(d0, Globals.full, Globals.full, Globals.full, d2, d3, d4);
                        default:
                            var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; 
                            return GetRange_np(tmp, 5);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d2 is EllipsisSpec) {
                    switch (m_size.NumberOfDimensions + countNewAxes(4, d0, d1, d3, d4)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            return GetRange_np(d0, d1, d3, d4);
                        case 5:
                            return GetRange_np(d0, d1, Globals.full, d3, d4);
                        case 6:
                            return GetRange_np(d0, d1, Globals.full, Globals.full, d3, d4);
                        case 7:
                            return GetRange_np(d0, d1, Globals.full, Globals.full, Globals.full, d3, d4);
                        default:
                            var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4;
                            return GetRange_np(tmp, 5);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d3 is EllipsisSpec) {
                    switch (m_size.NumberOfDimensions + countNewAxes(4, d0, d1, d2, d4)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            return GetRange_np(d0, d1, d2, d4);
                        case 5:
                            return GetRange_np(d0, d1, d2, Globals.full, d4);
                        case 6:
                            return GetRange_np(d0, d1, d2, Globals.full, Globals.full, d4);
                        case 7:
                            return GetRange_np(d0, d1, d2, Globals.full, Globals.full, Globals.full, d4);
                        default:
                            var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4;
                            return GetRange_np(tmp, 5);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d4 is EllipsisSpec) {
                    switch (m_size.NumberOfDimensions + countNewAxes(4, d0, d1, d2, d3)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            return GetRange_np(d0, d1, d2, d3);
                        case 5:
                            return GetRange_np(d0, d1, d2, d3, Globals.full);
                        case 6:
                            return GetRange_np(d0, d1, d2, d3, Globals.full, Globals.full);
                        case 7:
                            return GetRange_np(d0, d1, d2, d3, Globals.full, Globals.full, Globals.full);
                        default:
                            var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4;
                            return GetRange_np(tmp, 5);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                #endregion

                m_handles.Retain();
                StorageT ret = Create(m_handles);

                var bsd = ret.m_size.GetBSD(write: true);
                bsd[2] = (m_size.BaseOffset);

                uint inDim = 0, outDim = 0;
                long outLen = 1;
                checkEvaluate_np(d0, ref inDim, ref outDim, bsd, ref outLen);
                checkEvaluate_np(d1, ref inDim, ref outDim, bsd, ref outLen);
                checkEvaluate_np(d2, ref inDim, ref outDim, bsd, ref outLen);
                checkEvaluate_np(d3, ref inDim, ref outDim, bsd, ref outLen);
                checkEvaluate_np(d4, ref inDim, ref outDim, bsd, ref outLen);
                finalizeBSD_np(bsd, inDim, outDim, outLen);

                (ret as LogicalStorage)?.SetNumberTrues(-1);
                return ret;
        }
        internal virtual StorageT GetRange_np(DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3, DimSpec d4, DimSpec d5) {
                System.Diagnostics.Debug.Assert(Settings.ArrayStyle == ArrayStyles.numpy);

                #region handle ellipsis
                if (d0 is EllipsisSpec) {
                    switch (m_size.NumberOfDimensions + countNewAxes(5, d1, d2, d3, d4, d5)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            return GetRange_np(d1, d2, d3, d4, d5);
                        case 6:
                            return GetRange_np(Globals.full, d1, d2, d3, d4, d5);
                        case 7:
                            return GetRange_np(Globals.full, Globals.full, d1, d2, d3, d4, d5);
                        default:
                            var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; 
                            return GetRange_np(tmp, 6);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d1 is EllipsisSpec) {
                    switch (m_size.NumberOfDimensions + countNewAxes(5, d0, d2, d3, d4, d5)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            return GetRange_np(d0, d2, d3, d4, d5);
                        case 6:
                            return GetRange_np(d0, Globals.full, d2, d3, d4, d5);
                        case 7:
                            return GetRange_np(d0, Globals.full, Globals.full, d2, d3, d4, d5);
                        default:
                            var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; 
                            return GetRange_np(tmp, 6);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d2 is EllipsisSpec) {
                    switch (m_size.NumberOfDimensions + countNewAxes(5, d0, d1, d3, d4, d5)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            return GetRange_np(d0, d1, d3, d4, d5);
                        case 6:
                            return GetRange_np(d0, d1, Globals.full, d3, d4, d5);
                        case 7:
                            return GetRange_np(d0, d1, Globals.full, Globals.full, d3, d4, d5);
                        default:
                            var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; 
                            return GetRange_np(tmp, 6);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d3 is EllipsisSpec) {
                    switch (m_size.NumberOfDimensions + countNewAxes(5, d0, d1, d2, d4, d5)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            return GetRange_np(d0, d1, d2, d4, d5);
                        case 6:
                            return GetRange_np(d0, d1, d2, Globals.full, d4, d5);
                        case 7:
                            return GetRange_np(d0, d1, d2, Globals.full, Globals.full, d4, d5);
                        default:
                            var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; 
                            return GetRange_np(tmp, 6);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d4 is EllipsisSpec) {
                    switch (m_size.NumberOfDimensions + countNewAxes(5, d0, d1, d2, d3, d5)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            return GetRange_np(d0, d1, d2, d3, d5);
                        case 6:
                            return GetRange_np(d0, d1, d2, d3, Globals.full, d5);
                        case 7:
                            return GetRange_np(d0, d1, d2, d3, Globals.full, Globals.full, d5);
                        default:
                            var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; 
                            return GetRange_np(tmp, 6);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d5 is EllipsisSpec) {
                    switch (m_size.NumberOfDimensions + countNewAxes(5, d0, d1, d2, d3, d4)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            return GetRange_np(d0, d1, d2, d3, d4);
                        case 6:
                            return GetRange_np(d0, d1, d2, d3, d4, Globals.full);
                        case 7:
                            return GetRange_np(d0, d1, d2, d3, d4, Globals.full, Globals.full);
                        default:
                            var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; 
                            return GetRange_np(tmp, 6);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                #endregion

                m_handles.Retain();
                StorageT ret = Create(m_handles);

                var bsd = ret.m_size.GetBSD(write: true);
                bsd[2] = (m_size.BaseOffset);

                uint inDim = 0, outDim = 0;
                long outLen = 1;
                checkEvaluate_np(d0, ref inDim, ref outDim, bsd, ref outLen);
                checkEvaluate_np(d1, ref inDim, ref outDim, bsd, ref outLen);
                checkEvaluate_np(d2, ref inDim, ref outDim, bsd, ref outLen);
                checkEvaluate_np(d3, ref inDim, ref outDim, bsd, ref outLen);
                checkEvaluate_np(d4, ref inDim, ref outDim, bsd, ref outLen);
                checkEvaluate_np(d5, ref inDim, ref outDim, bsd, ref outLen);
                finalizeBSD_np(bsd, inDim, outDim, outLen);

                (ret as LogicalStorage)?.SetNumberTrues(-1);
                return ret;
        }
        internal virtual StorageT GetRange_np(DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3, DimSpec d4, DimSpec d5, DimSpec d6) {
                System.Diagnostics.Debug.Assert(Settings.ArrayStyle == ArrayStyles.numpy);

                #region handle ellipsis
                if (d0 is EllipsisSpec) {
                    switch (m_size.NumberOfDimensions + countNewAxes(6, d1, d2, d3, d4, d5, d6)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            return GetRange_np(d1, d2, d3, d4, d5, d6);
                        case 7:
                            return GetRange_np(Globals.full, d1, d2, d3, d4, d5, d6);
                        default:
                            var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6; 
                            return GetRange_np(tmp, 7);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d1 is EllipsisSpec) {
                    switch (m_size.NumberOfDimensions + countNewAxes(6, d0, d2, d3, d4, d5, d6)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            return GetRange_np(d0, d2, d3, d4, d5, d6);
                        case 7:
                            return GetRange_np(d0, Globals.full, d2, d3, d4, d5, d6);
                        default:
                            var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6; 
                            return GetRange_np(tmp, 7);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d2 is EllipsisSpec) {
                    switch (m_size.NumberOfDimensions + countNewAxes(6, d0, d1, d3, d4, d5, d6)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            return GetRange_np(d0, d1, d3, d4, d5, d6);
                        case 7:
                            return GetRange_np(d0, d1, Globals.full, d3, d4, d5, d6);
                        default:
                            var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6; 
                            return GetRange_np(tmp, 7);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d3 is EllipsisSpec) {
                    switch (m_size.NumberOfDimensions + countNewAxes(6, d0, d1, d2, d4, d5, d6)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            return GetRange_np(d0, d1, d2, d4, d5, d6);
                        case 7:
                            return GetRange_np(d0, d1, d2, Globals.full, d4, d5, d6);
                        default:
                            var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6; 
                            return GetRange_np(tmp, 7);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d4 is EllipsisSpec) {
                    switch (m_size.NumberOfDimensions + countNewAxes(6, d0, d1, d2, d3, d5, d6)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            return GetRange_np(d0, d1, d2, d3, d5, d6);
                        case 7:
                            return GetRange_np(d0, d1, d2, d3, Globals.full, d5, d6);
                        default:
                            var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6; 
                            return GetRange_np(tmp, 7);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d5 is EllipsisSpec) {
                    switch (m_size.NumberOfDimensions + countNewAxes(6, d0, d1, d2, d3, d4, d6)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            return GetRange_np(d0, d1, d2, d3, d4, d6);
                        case 7:
                            return GetRange_np(d0, d1, d2, d3, d4, Globals.full, d6);
                        default:
                            var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6; 
                            return GetRange_np(tmp, 7);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d6 is EllipsisSpec) {
                    switch (m_size.NumberOfDimensions + countNewAxes(6, d0, d1, d2, d3, d4, d5)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            return GetRange_np(d0, d1, d2, d3, d4, d5);
                        case 7:
                            return GetRange_np(d0, d1, d2, d3, d4, d5, Globals.full);
                        default:
                            var tmp = Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6; 
                            return GetRange_np(tmp, 7);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                #endregion

                m_handles.Retain();
                StorageT ret = Create(m_handles);

                var bsd = ret.m_size.GetBSD(write: true);
                bsd[2] = (m_size.BaseOffset);

                uint inDim = 0, outDim = 0;
                long outLen = 1;
                checkEvaluate_np(d0, ref inDim, ref outDim, bsd, ref outLen);
                checkEvaluate_np(d1, ref inDim, ref outDim, bsd, ref outLen);
                checkEvaluate_np(d2, ref inDim, ref outDim, bsd, ref outLen);
                checkEvaluate_np(d3, ref inDim, ref outDim, bsd, ref outLen);
                checkEvaluate_np(d4, ref inDim, ref outDim, bsd, ref outLen);
                checkEvaluate_np(d5, ref inDim, ref outDim, bsd, ref outLen);
                checkEvaluate_np(d6, ref inDim, ref outDim, bsd, ref outLen);
                finalizeBSD_np(bsd, inDim, outDim, outLen);

                (ret as LogicalStorage)?.SetNumberTrues(-1);
                return ret;

        }
        #endregion

        #region BaseArray indexing

        internal StorageT GetRange_np(BaseArray d0) {

            if (d0 is EllipsisSpec) {
                //(d0 as EllipsisSpec)?.Dispose();  // ellipsis is an immutable singleton
                return Create(Handles, Size); 
            }

            System.Diagnostics.Debug.Assert(!(d0 is EllipsisSpec)); // DimSpec overload would have been called, no?

            var iterators = Context.MultidimIterators;
            long* idxOutDimsBuffer = Context.TmpBuffer1000;

            // these buffers are shared between all index array members of the set: curpos (during iterations) and idxOutDims (broadcasted output size)
            long* idxCurPosBuffer = idxOutDimsBuffer + Size.MaxNumberOfDimensions;
            // this buffer is used as temp. buffer for the iterators
            long* tmpBuffer = idxCurPosBuffer + Size.MaxNumberOfDimensions; // max 3...7 * long per dim 

            int firstIDXArrayPos = -1;
            uint nrIdxOutDims = 0;
            uint inputDimIdx = 0;
            uint nrIters = 0;
            uint setCount = 0;  // will be shared with all IDX iterators

            collectArrayIndex(d0, ref nrIters, ref inputDimIdx,         
                ref firstIDXArrayPos, &tmpBuffer,
                &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                iterators);
            return iterateSubarray_np((int)nrIters, iterators, firstIDXArrayPos,
                                        nrIdxOutDims, idxOutDimsBuffer, &tmpBuffer);

        }
        internal StorageT GetRange_np(BaseArray d0, BaseArray d1) {

            var iterators = Context.MultidimIterators;
            long* idxOutDimsBuffer = Context.TmpBuffer1000;

                #region handle ellipsis
                if (d0 is EllipsisSpec) {
                    if ((d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (m_size.NumberOfDimensions + countNewAxes(1, d1)) {
                        case 0:
                        case 1:
                            return GetRange_np(d1);
                        case 2:
                            return GetRange_np(Globals.full, d1);
                        case 3:
                            return GetRange_np(Globals.full, Globals.full, d1);
                        case 4:
                            return GetRange_np(Globals.full, Globals.full, Globals.full, d1);
                        case 5:
                            return GetRange_np(Globals.full, Globals.full, Globals.full, Globals.full, d1);
                        case 6:
                            return GetRange_np(Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, d1);
                        case 7:
                            return GetRange_np(Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, d1);
                        default:
                            var tmp = Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; 
                            return GetRange_np(tmp, 2);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d1 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (m_size.NumberOfDimensions + countNewAxes(1, d0)) {
                        case 0:
                        case 1:
                            return GetRange_np(d0);
                        case 2:
                            return GetRange_np(d0, Globals.full);
                        case 3:
                            return GetRange_np(d0, Globals.full, Globals.full);
                        case 4:
                            return GetRange_np(d0, Globals.full, Globals.full, Globals.full);
                        case 5:
                            return GetRange_np(d0, Globals.full, Globals.full, Globals.full, Globals.full);
                        case 6:
                            return GetRange_np(d0, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full);
                        case 7:
                            return GetRange_np(d0, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full);
                        default:
                            var tmp = Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; 
                            return GetRange_np(tmp, 2);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                #endregion

                // these buffers are shared between all index array members of the set: curpos (during iterations) and idxOutDims (broadcasted output size)
                long* idxCurPosBuffer = idxOutDimsBuffer + Size.MaxNumberOfDimensions;
                // this buffer is used as temp. buffer for the iterators
                long* tmpBuffer = idxCurPosBuffer + Size.MaxNumberOfDimensions; // max 3...7 * long per dim 

                int firstIDXArrayPos = -1;
                uint nrIdxOutDims = 0;
                uint inputDimIdx = 0;
                uint nrIters = 0;
                uint setCount = 0;  // will be shared with all IDX iterators

                collectArrayIndex(d0, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                collectArrayIndex(d1, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                return iterateSubarray_np((int)nrIters, iterators, firstIDXArrayPos,
                                          nrIdxOutDims, idxOutDimsBuffer, &tmpBuffer);
        }
        internal StorageT GetRange_np(BaseArray d0, BaseArray d1, BaseArray d2) {

            var iterators = Context.MultidimIterators;
            long* idxOutDimsBuffer = Context.TmpBuffer1000;

                #region handle ellipsis
                if (d0 is EllipsisSpec) {
                    if ((d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (m_size.NumberOfDimensions + countNewAxes(2, d1, d2)) {
                        case 0:
                        case 1:
                        case 2:
                            return GetRange_np(d1, d2);
                        case 3:
                            return GetRange_np(Globals.full, d1, d2);
                        case 4:
                            return GetRange_np(Globals.full, Globals.full, d1, d2);
                        case 5:
                            return GetRange_np(Globals.full, Globals.full, Globals.full, d1, d2);
                        case 6:
                            return GetRange_np(Globals.full, Globals.full, Globals.full, Globals.full, d1, d2);
                        case 7:
                            return GetRange_np(Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, d1, d2);
                        default:
                            var tmp = Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2;
                            return GetRange_np(tmp, 3);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d1 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (m_size.NumberOfDimensions + countNewAxes(2, d0, d2)) {
                        case 0:
                        case 1:
                        case 2:
                            return GetRange_np(d0, d2);
                        case 3:
                            return GetRange_np(d0, Globals.full, d2);
                        case 4:
                            return GetRange_np(d0, Globals.full, Globals.full, d2);
                        case 5:
                            return GetRange_np(d0, Globals.full, Globals.full, Globals.full, d2);
                        case 6:
                            return GetRange_np(d0, Globals.full, Globals.full, Globals.full, Globals.full, d2);
                        case 7:
                            return GetRange_np(d0, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, d2);
                        default:
                            var tmp = Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2;
                            return GetRange_np(tmp, 3);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d2 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (m_size.NumberOfDimensions + countNewAxes(2, d0, d1)) {
                        case 0:
                        case 1:
                        case 2:
                            return GetRange_np(d0, d1);
                        case 3:
                            return GetRange_np(d0, d1, Globals.full);
                        case 4:
                            return GetRange_np(d0, d1, Globals.full, Globals.full);
                        case 5:
                            return GetRange_np(d0, d1, Globals.full, Globals.full, Globals.full);
                        case 6:
                            return GetRange_np(d0, d1, Globals.full, Globals.full, Globals.full, Globals.full);
                        case 7:
                            return GetRange_np(d0, d1, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full);
                        default:
                            var tmp = Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2;
                            return GetRange_np(tmp, 3);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                #endregion

                // these buffers are shared between all index array members of the set: curpos (during iterations) and idxOutDims (broadcasted output size)
                long* idxCurPosBuffer = idxOutDimsBuffer + Size.MaxNumberOfDimensions;
                // this buffer is used as temp. buffer for the iterators
                long* tmpBuffer = idxCurPosBuffer + Size.MaxNumberOfDimensions; // max 3...7 * long per dim 

                int firstIDXArrayPos = -1;
                uint nrIdxOutDims = 0;
                uint inputDimIdx = 0;
                uint nrIters = 0;
                uint setCount = 0;  // will be shared with all IDX iterators

                collectArrayIndex(d0, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                collectArrayIndex(d1, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                collectArrayIndex(d2, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                return iterateSubarray_np((int)nrIters, iterators, firstIDXArrayPos,
                                          nrIdxOutDims, idxOutDimsBuffer, &tmpBuffer);
        }
        internal StorageT GetRange_np(BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3) {

            var iterators = Context.MultidimIterators;
            long* idxOutDimsBuffer = Context.TmpBuffer1000;

                #region handle ellipsis
                if (d0 is EllipsisSpec) {
                    if ((d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (m_size.NumberOfDimensions + countNewAxes(3, d1, d2, d3)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                            return GetRange_np(d1, d2, d3);
                        case 4:
                            return GetRange_np(Globals.full, d1, d2, d3);
                        case 5:
                            return GetRange_np(Globals.full, Globals.full, d1, d2, d3);
                        case 6:
                            return GetRange_np(Globals.full, Globals.full, Globals.full, d1, d2, d3);
                        case 7:
                            return GetRange_np(Globals.full, Globals.full, Globals.full, Globals.full, d1, d2, d3);
                        default:
                            var tmp = Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3;
                            return GetRange_np(tmp, 4);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d1 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (m_size.NumberOfDimensions + countNewAxes(3, d0, d2, d3)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                            return GetRange_np(d0, d2, d3);
                        case 4:
                            return GetRange_np(d0, Globals.full, d2, d3);
                        case 5:
                            return GetRange_np(d0, Globals.full, Globals.full, d2, d3);
                        case 6:
                            return GetRange_np(d0, Globals.full, Globals.full, Globals.full, d2, d3);
                        case 7:
                            return GetRange_np(d0, Globals.full, Globals.full, Globals.full, Globals.full, d2, d3);
                        default:
                            var tmp = Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3;
                            return GetRange_np(tmp, 4);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d2 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (m_size.NumberOfDimensions + countNewAxes(3, d0, d1, d3)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                            return GetRange_np(d0, d1, d3);
                        case 4:
                            return GetRange_np(d0, d1, Globals.full, d3);
                        case 5:
                            return GetRange_np(d0, d1, Globals.full, Globals.full, d3);
                        case 6:
                            return GetRange_np(d0, d1, Globals.full, Globals.full, Globals.full, d3);
                        case 7:
                            return GetRange_np(d0, d1, Globals.full, Globals.full, Globals.full, Globals.full, d3);
                        default:
                            var tmp = Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; 
                            return GetRange_np(tmp, 4);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d3 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (m_size.NumberOfDimensions + countNewAxes(3, d0, d1, d2)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                            return GetRange_np(d0, d1, d2);
                        case 4:
                            return GetRange_np(d0, d1, d2, Globals.full);
                        case 5:
                            return GetRange_np(d0, d1, d2, Globals.full, Globals.full);
                        case 6:
                            return GetRange_np(d0, d1, d2, Globals.full, Globals.full, Globals.full);
                        case 7:
                            return GetRange_np(d0, d1, d2, Globals.full, Globals.full, Globals.full, Globals.full);
                        default:
                            var tmp = Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; 
                            return GetRange_np(tmp, 4);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                #endregion

                // these buffers are shared between all index array members of the set: curpos (during iterations) and idxOutDims (broadcasted output size)
                long* idxCurPosBuffer = idxOutDimsBuffer + Size.MaxNumberOfDimensions;
                // this buffer is used as temp. buffer for the iterators
                long* tmpBuffer = idxCurPosBuffer + Size.MaxNumberOfDimensions; // max 3...7 * long per dim 

                int firstIDXArrayPos = -1;
                uint nrIdxOutDims = 0;
                uint inputDimIdx = 0;
                uint nrIters = 0;
                uint setCount = 0;  // will be shared with all IDX iterators

                collectArrayIndex(d0, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                collectArrayIndex(d1, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                collectArrayIndex(d2, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                collectArrayIndex(d3, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                return iterateSubarray_np((int)nrIters, iterators, firstIDXArrayPos,
                                          nrIdxOutDims, idxOutDimsBuffer, &tmpBuffer);
        }
        internal StorageT GetRange_np(BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3, BaseArray d4) {

            var iterators = Context.MultidimIterators;
            long* idxOutDimsBuffer = Context.TmpBuffer1000;


            #region handle ellipsis
            if (d0 is EllipsisSpec) {
                if ((d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                    (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                    (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                    (d4 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1) {
                    throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                }
                switch (m_size.NumberOfDimensions + countNewAxes(4, d1, d2, d3, d4)) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        return GetRange_np(d1, d2, d3, d4);
                    case 5:
                        return GetRange_np(Globals.full, d1, d2, d3, d4);
                    case 6:
                        return GetRange_np(Globals.full, Globals.full, d1, d2, d3, d4);
                    case 7:
                        return GetRange_np(Globals.full, Globals.full, Globals.full, d1, d2, d3, d4);
                    default:
                        var tmp = Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; 
                        return GetRange_np(tmp, 5);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d1 is EllipsisSpec) {
                if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                    (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                    (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                    (d4 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1) {
                    throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                }
                switch (m_size.NumberOfDimensions + countNewAxes(4, d0, d2, d3, d4)) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        return GetRange_np(d0, d2, d3, d4);
                    case 5:
                        return GetRange_np(d0, Globals.full, d2, d3, d4);
                    case 6:
                        return GetRange_np(d0, Globals.full, Globals.full, d2, d3, d4);
                    case 7:
                        return GetRange_np(d0, Globals.full, Globals.full, Globals.full, d2, d3, d4);
                    default:
                        var tmp = Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4;
                        return GetRange_np(tmp, 5);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d2 is EllipsisSpec) {
                if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                    (d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                    (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                    (d4 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1) {
                    throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                }
                switch (m_size.NumberOfDimensions + countNewAxes(4, d0, d1, d3, d4)) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        return GetRange_np(d0, d1, d3, d4);
                    case 5:
                        return GetRange_np(d0, d1, Globals.full, d3, d4);
                    case 6:
                        return GetRange_np(d0, d1, Globals.full, Globals.full, d3, d4);
                    case 7:
                        return GetRange_np(d0, d1, Globals.full, Globals.full, Globals.full, d3, d4);
                    default:
                        var tmp = Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; 
                        return GetRange_np(tmp, 5);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d3 is EllipsisSpec) {
                if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                    (d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                    (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                    (d4 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1) {
                    throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                }
                switch (m_size.NumberOfDimensions + countNewAxes(4, d0, d1, d2, d4)) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        return GetRange_np(d0, d1, d2, d4);
                    case 5:
                        return GetRange_np(d0, d1, d2, Globals.full, d4);
                    case 6:
                        return GetRange_np(d0, d1, d2, Globals.full, Globals.full, d4);
                    case 7:
                        return GetRange_np(d0, d1, d2, Globals.full, Globals.full, Globals.full, d4);
                    default:
                        var tmp = Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4;
                        return GetRange_np(tmp, 5);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            if (d4 is EllipsisSpec) {
                if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                    (d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                    (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                    (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1) {
                    throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                }
                switch (m_size.NumberOfDimensions + countNewAxes(4, d0, d1, d2, d3)) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        return GetRange_np(d0, d1, d2, d3);
                    case 5:
                        return GetRange_np(d0, d1, d2, d3, Globals.full);
                    case 6:
                        return GetRange_np(d0, d1, d2, d3, Globals.full, Globals.full);
                    case 7:
                        return GetRange_np(d0, d1, d2, d3, Globals.full, Globals.full, Globals.full);
                    default:
                        var tmp = Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; 
                        return GetRange_np(tmp, 5);
                        //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                }
            }
            #endregion

                // these buffers are shared between all index array members of the set: curpos (during iterations) and idxOutDims (broadcasted output size)
                long* idxCurPosBuffer = idxOutDimsBuffer + Size.MaxNumberOfDimensions;
                // this buffer is used as temp. buffer for the iterators
                long* tmpBuffer = idxCurPosBuffer + Size.MaxNumberOfDimensions; // max 3...7 * long per dim 

                int firstIDXArrayPos = -1;
                uint nrIdxOutDims = 0;
                uint inputDimIdx = 0;
                uint nrIters = 0;
                uint setCount = 0;  // will be shared with all IDX iterators

                collectArrayIndex(d0, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                collectArrayIndex(d1, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                collectArrayIndex(d2, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                collectArrayIndex(d3, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                collectArrayIndex(d4, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                return iterateSubarray_np((int)nrIters, iterators, firstIDXArrayPos,
                                          nrIdxOutDims, idxOutDimsBuffer, &tmpBuffer);
        }
        internal StorageT GetRange_np(BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3, BaseArray d4, BaseArray d5) {

            var iterators = Context.MultidimIterators;
            long* idxOutDimsBuffer = Context.TmpBuffer1000;


                #region handle ellipsis
                if (d0 is EllipsisSpec) {
                    if ((d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d4 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d5 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (m_size.NumberOfDimensions + countNewAxes(5, d1, d2, d3, d4, d5)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            return GetRange_np(d1, d2, d3, d4, d5);
                        case 6:
                            return GetRange_np(Globals.full, d1, d2, d3, d4, d5);
                        case 7:
                            return GetRange_np(Globals.full, Globals.full, d1, d2, d3, d4, d5);
                        default:
                            var tmp = Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; 
                            return GetRange_np(tmp, 6);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d1 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d4 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d5 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (m_size.NumberOfDimensions + countNewAxes(5, d0, d2, d3, d4, d5)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            return GetRange_np(d0, d2, d3, d4, d5);
                        case 6:
                            return GetRange_np(d0, Globals.full, d2, d3, d4, d5);
                        case 7:
                            return GetRange_np(d0, Globals.full, Globals.full, d2, d3, d4, d5);
                        default:
                            var tmp = Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; 
                            return GetRange_np(tmp, 6);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d2 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d4 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d5 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (m_size.NumberOfDimensions + countNewAxes(5, d0, d1, d3, d4, d5)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            return GetRange_np(d0, d1, d3, d4, d5);
                        case 6:
                            return GetRange_np(d0, d1, Globals.full, d3, d4, d5);
                        case 7:
                            return GetRange_np(d0, d1, Globals.full, Globals.full, d3, d4, d5);
                        default:
                            var tmp = Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; 
                            return GetRange_np(tmp, 6);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d3 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d4 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d5 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (m_size.NumberOfDimensions + countNewAxes(5, d0, d1, d2, d4, d5)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            return GetRange_np(d0, d1, d2, d4, d5);
                        case 6:
                            return GetRange_np(d0, d1, d2, Globals.full, d4, d5);
                        case 7:
                            return GetRange_np(d0, d1, d2, Globals.full, Globals.full, d4, d5);
                        default:
                            var tmp = Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5;
                            return GetRange_np(tmp, 6);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d4 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d5 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (m_size.NumberOfDimensions + countNewAxes(5, d0, d1, d2, d3, d5)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            return GetRange_np(d0, d1, d2, d3, d5);
                        case 6:
                            return GetRange_np(d0, d1, d2, d3, Globals.full, d5);
                        case 7:
                            return GetRange_np(d0, d1, d2, d3, Globals.full, Globals.full, d5);
                        default:
                            var tmp = Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5;
                            return GetRange_np(tmp, 6);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d5 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d4 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (m_size.NumberOfDimensions + countNewAxes(5, d0, d1, d2, d3, d4)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            return GetRange_np(d0, d1, d2, d3, d4);
                        case 6:
                            return GetRange_np(d0, d1, d2, d3, d4, Globals.full);
                        case 7:
                            return GetRange_np(d0, d1, d2, d3, d4, Globals.full, Globals.full);
                        default:
                            var tmp = Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5;
                            return GetRange_np(tmp, 6);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                #endregion

                // these buffers are shared between all index array members of the set: curpos (during iterations) and idxOutDims (broadcasted output size)
                long* idxCurPosBuffer = idxOutDimsBuffer + Size.MaxNumberOfDimensions;
                // this buffer is used as temp. buffer for the iterators
                long* tmpBuffer = idxCurPosBuffer + Size.MaxNumberOfDimensions; // max 3...7 * long per dim 

                int firstIDXArrayPos = -1;
                uint nrIdxOutDims = 0;
                uint inputDimIdx = 0;
                uint nrIters = 0;
                uint setCount = 0;  // will be shared with all IDX iterators

                collectArrayIndex(d0, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                collectArrayIndex(d1, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                collectArrayIndex(d2, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                collectArrayIndex(d3, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                collectArrayIndex(d4, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                collectArrayIndex(d5, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                return iterateSubarray_np((int)nrIters, iterators, firstIDXArrayPos,
                                          nrIdxOutDims, idxOutDimsBuffer, &tmpBuffer);
        }
        internal StorageT GetRange_np(BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3, BaseArray d4, BaseArray d5, BaseArray d6) {

            var iterators = Context.MultidimIterators;
            long* idxOutDimsBuffer = Context.TmpBuffer1000;


                #region handle ellipsis
                if (d0 is EllipsisSpec) {
                    if ((d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d4 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d5 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d6 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (m_size.NumberOfDimensions + countNewAxes(6, d1, d2, d3, d4, d5, d6)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            return GetRange_np(d1, d2, d3, d4, d5, d6);
                        case 7:
                            return GetRange_np(Globals.full, d1, d2, d3, d4, d5, d6);
                        default:
                            var tmp = Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6; 
                            return GetRange_np(tmp, 7);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d1 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d4 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d5 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d6 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (m_size.NumberOfDimensions + countNewAxes(6, d0, d2, d3, d4, d5, d6)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            return GetRange_np(d0, d2, d3, d4, d5, d6);
                        case 7:
                            return GetRange_np(d0, Globals.full, d2, d3, d4, d5, d6);
                        default:
                            var tmp = Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6; 
                            return GetRange_np(tmp, 7);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d2 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d4 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d5 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d6 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (m_size.NumberOfDimensions + countNewAxes(6, d0, d1, d3, d4, d5, d6)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            return GetRange_np(d0, d1, d3, d4, d5, d6);
                        case 7:
                            return GetRange_np(d0, d1, Globals.full, d3, d4, d5, d6);
                        default:
                            var tmp = Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6; 
                            return GetRange_np(tmp, 7);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d3 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d4 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d5 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d6 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (m_size.NumberOfDimensions + countNewAxes(6, d0, d1, d2, d4, d5, d6)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            return GetRange_np(d0, d1, d2, d4, d5, d6);
                        case 7:
                            return GetRange_np(d0, d1, d2, Globals.full, d4, d5, d6);
                        default:
                            var tmp = Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6;
                            return GetRange_np(tmp, 7);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d4 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d5 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d6 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (m_size.NumberOfDimensions + countNewAxes(6, d0, d1, d2, d3, d5, d6)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            return GetRange_np(d0, d1, d2, d3, d5, d6);
                        case 7:
                            return GetRange_np(d0, d1, d2, d3, Globals.full, d5, d6);
                        default:
                            var tmp = Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6; 
                            return GetRange_np(tmp, 7);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d5 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d4 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d6 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (m_size.NumberOfDimensions + countNewAxes(6, d0, d1, d2, d3, d4, d6)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            return GetRange_np(d0, d1, d2, d3, d4, d6);
                        case 7:
                            return GetRange_np(d0, d1, d2, d3, d4, Globals.full, d6);
                        default:
                            var tmp = Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6; 
                            return GetRange_np(tmp, 7);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                if (d6 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d4 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1 ||
                        (d5 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (m_size.NumberOfDimensions + countNewAxes(6, d0, d1, d2, d3, d4, d5)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            return GetRange_np(d0, d1, d2, d3, d4, d5);
                        case 7:
                            return GetRange_np(d0, d1, d2, d3, d4, d5, Globals.full);
                        default:
                            var tmp = Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6; 
                            return GetRange_np(tmp, 7);
                            //throw new InvalidOperationException($"The number of dimensions of this array ({m_size.NumberOfDimensions}) exceeds the maximum number of dimensions configured for ILNumerics ({Size.MaxNumberOfDimensions}). Please contact ILNumerics support!");
                    }
                }
                #endregion

                // these buffers are shared between all index array members of the set: curpos (during iterations) and idxOutDims (broadcasted output size)
                long* idxCurPosBuffer = idxOutDimsBuffer + Size.MaxNumberOfDimensions;
                // this buffer is used as temp. buffer for the iterators
                long* tmpBuffer = idxCurPosBuffer + Size.MaxNumberOfDimensions; // max 3...7 * long per dim 

                int firstIDXArrayPos = -1;
                uint nrIdxOutDims = 0;
                uint inputDimIdx = 0;
                uint nrIters = 0;
                uint setCount = 0;  // will be shared with all IDX iterators

                collectArrayIndex(d0, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                collectArrayIndex(d1, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                collectArrayIndex(d2, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                collectArrayIndex(d3, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                collectArrayIndex(d4, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                collectArrayIndex(d5, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                collectArrayIndex(d6, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                return iterateSubarray_np((int)nrIters, iterators, firstIDXArrayPos,
                                          nrIdxOutDims, idxOutDimsBuffer, &tmpBuffer);
        }
        internal StorageT GetRange_np(BaseArray[] dims, uint? len = null) {

            if (dims == null || (len ?? (uint)dims.Length) == 0) {

                #region return all
                return GetRange_np(Globals.ellipsis);
                #endregion

            }

            var HANDLES_NDIM = len ?? (uint)dims.Length;

            if (m_size.NumberOfDimensions <= 7) {
                switch (HANDLES_NDIM) {
                    case 1:
                        return GetRange_np(dims[0]);
                    case 2:
                        return GetRange_np(dims[0], dims[1]);
                    case 3:
                        return GetRange_np(dims[0], dims[1], dims[2]);
                    case 4:
                        return GetRange_np(dims[0], dims[1], dims[2], dims[3]);
                    case 5:
                        return GetRange_np(dims[0], dims[1], dims[2], dims[3], dims[4]);
                    case 6:
                        return GetRange_np(dims[0], dims[1], dims[2], dims[3], dims[4], dims[5]);
                    case 7:
                        return GetRange_np(dims[0], dims[1], dims[2], dims[3], dims[4], dims[5], dims[6]);
                    default:
                        break;
                        //throw new NotSupportedException($"Invalid indices array. Too many dimensions: {dims.Length} (max: {Size.MaxNumberOfDimensions})");
                }
            }


            #region handle ellipsis
            

            var nrNewaxis = countNewAxes(dims, HANDLES_NDIM);
            for (int i = 0; i < HANDLES_NDIM; i++) {
                if (dims[i] is EllipsisSpec) {
                    for (int ii = 0; ii < HANDLES_NDIM; ii++) {
                        if ((dims[ii] as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.m_size.NumberOfDimensions > 1) {
                            throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is not supported.");
                        }
                    }
                    substituteEllipsis(dims, Context.BaseArrayArray, ref HANDLES_NDIM, m_size.NumberOfDimensions + (uint)nrNewaxis, i);
                    dims = Context.BaseArrayArray;
                    return GetRange_np(dims, HANDLES_NDIM);
                }
            }
            #endregion

            var iterators = Context.MultidimIterators;
            long* idxOutDimsBuffer = Context.TmpBuffer1000;

                // these buffers are shared between all index array members of the set: curpos (during iterations) and idxOutDims (broadcasted output size)
                long* idxCurPosBuffer = idxOutDimsBuffer + Size.MaxNumberOfDimensions;
                // this buffer is used as temp. buffer for the iterators
                long* tmpBuffer = idxCurPosBuffer + Size.MaxNumberOfDimensions; // max 3...7 * long per dim 

                int firstIDXArrayPos = -1;
                uint nrIdxOutDims = 0;
                uint inputDimIdx = 0;
                uint nrIters = 0;
                uint setCount = 0;  // will be shared with all IDX iterators

                for (int i = 0; i < HANDLES_NDIM; i++) {
                    collectArrayIndex(dims[i], ref nrIters, ref inputDimIdx,
                        ref firstIDXArrayPos, &tmpBuffer,
                        &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                        iterators);
                }
                return iterateSubarray_np((int)nrIters, iterators, firstIDXArrayPos,
                                          nrIdxOutDims, idxOutDimsBuffer, &tmpBuffer);
        }

        #endregion

        #region private helpers 
        internal uint countNewAxes(uint d, BaseArray d0, BaseArray d1 = null, BaseArray d2 = null,
                                          BaseArray d3 = null, BaseArray d4 = null, BaseArray d5 = null, BaseArray d6 = null) {
            System.Diagnostics.Debug.Assert(d > 0);

            uint ret = (d0 is NewaxisSpec) ? 1u : 0u;
            if (d > 1) {
                if (d1 is NewaxisSpec) { ret++; }
                if (d > 2) {
                    if (d2 is NewaxisSpec) { ret++; }
                    if (d > 3) {
                        if (d3 is NewaxisSpec) { ret++; }
                        if (d > 4) {
                            if (d4 is NewaxisSpec) { ret++; }
                            if (d > 5) {
                                if (d5 is NewaxisSpec) { ret++; }
                                if (d > 6) {
                                    if (d6 is NewaxisSpec) { ret++; }
                                }
                            }
                        }
                    }
                }
            }
            return ret;
        }
        internal int countNewAxes<DimT>(DimT[] dims, uint len) where DimT : BaseArray {
            int ret = 0;
            for (int i = 0; i < len; i++) {
                if (dims[i] is NewaxisSpec) {
                    ret++;
                }
            }
            return ret;
        }

        private const uint MAXDIMSPLUS3 = Size.MaxNumberOfDimensions + 3;
        internal void checkEvaluate_np(DimSpec d, ref uint inDim, ref uint outDim, long* outbsd, ref long outLen) {
            if (d == null || d is NewaxisSpec) {
                outbsd[3 + outDim] = (1);
                outbsd[MAXDIMSPLUS3 + outDim] = (0);
                outDim++;
                //d?.Dispose(); // Newaxis is an immutable singleton
                return;
            }

            d.Evaluate(m_size[inDim] - 1);

            outbsd[2] = outbsd[2] + (d.Start * m_size.GetStride((uint)inDim));
            if (!d.IsSingleIndex && inDim < m_size.NumberOfDimensions) {
                outbsd[3 + outDim] = (d.Length);
                outbsd[MAXDIMSPLUS3 + outDim] = (d.Length != 1 ? (d.Step * m_size.GetStride((uint)inDim)) : 0);
                outDim++;
                outLen *= d.Length;
            }
            inDim++;
        }
        // this brings the output bsd into the correct format. determins outLen + strides from temporary 
        internal void finalizeBSD_np(long* bsd, ulong inDim, ulong outDim, long outLen) {

            int nOutDims = (int)outDim + (int)Math.Max(m_size.NumberOfDimensions, inDim) - (int)inDim;  // caution! uint underflow on scalars!
            if (nOutDims < 0) // special case: numpy scalar array
                nOutDims = 0;
            if (nOutDims > Size.MaxNumberOfDimensions) {
                throw new InvalidOperationException($"Index expression exceeds maximum number of dimensions allowed. Size.MaxNumberOfDimensions: {Size.MaxNumberOfDimensions}");
            }
            var myBSD = m_size.GetBSD(false);

            for (; inDim < m_size.NumberOfDimensions; inDim++, outDim++) {
                bsd[3 + outDim] = myBSD[3 + inDim];
                outLen *= bsd[3 + outDim];
                bsd[MAXDIMSPLUS3 + outDim] = bsd[3 + outDim] != 1 ? myBSD[3 + m_size.NumberOfDimensions + inDim] : 0;
            }
            for (int i = 0; i < nOutDims; i++) {
                bsd[3 + nOutDims + i] = bsd[MAXDIMSPLUS3 + i];
            }
            bsd[0] = ((uint)nOutDims);
            bsd[1] = (outLen);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nIters">Number of multidim iterators in <paramref name="iterators"/>.</param>
        /// <param name="iterators">MultidimIterators according to the indices provided.</param>
        /// <param name="buffer">temp buffer, may used for additional, trailing MultidimIterators.</param>
        /// <param name="firstIDXArrayPos">Index of the first occurence of an index array in the list of iterators.</param>
        /// <param name="nrIdxOutDims">Number of dimensions stored in the array of index space dimension lenghts <paramref name="idxOutDims"/>.</param>
        /// <param name="idxOutDims">Index space dimension lengths of length <paramref name="nrIdxOutDims"/>.</param>
        /// <returns></returns>
        
        private StorageT iterateSubarray_np(int nIters, MultidimIterator* iterators, int firstIDXArrayPos, long nrIdxOutDims, long* idxOutDims, long** buffer) {

            //// deal with virtual dimensions
            //if (nIters >= m_size.NumberOfDimensions) {
            //    // all iterators outside of my dimensions must address 0 index only.
            //    for (uint i = m_size.NumberOfDimensions; i < nIters; i++) {
            //        (iterators + i)->
            //    }
            //}

            uint nMissingDims = (uint)Math.Max((int)m_size.NumberOfDimensions - nIters, 0);
            // reorder iterators according to (broadcasted, merged) idx dimensions
            // compute output shape 
            long inBaseOffset = 0;
            uint nrOutDims = 0;
            // prepare out storage
            var ret = Create();
            // set out dims + nroutdims only
            var outBSD = ret.m_size.GetBSD(true);
            findOutDimensionOrder(iterators, ref nIters, ref nrOutDims, outBSD + 3, firstIDXArrayPos,
                                nrIdxOutDims, idxOutDims, ref inBaseOffset, nMissingDims, buffer);

            // finalize BSD, row major
            long s = 1;
            outBSD[0] = nrOutDims; outBSD[2] = 0;
            for (int i = 0; i < nrOutDims; i++) {
                var d = outBSD[2 + nrOutDims - i];
                outBSD[2 + 2 * nrOutDims - i] = (d == 1 ? 0 : s);
                s *= d;
            }
            outBSD[1] = s;
            //ret.m_handles = CountableArray.Create();
            ret.m_handles[0] = ret.New((ulong)s, clear: false); // CellStorage: forces clear = true

            // CopyTo...
            if (m_handles[0] is NativeHostHandle) {
                switch (SizeOfT) {
                    case 1:
                        CopyTo_1_np(this as StorageT, ret, iterators, (uint)nIters, inBaseOffset);
                        break;
                    case 2:
                        CopyTo_2_np(this as StorageT, ret, iterators, (uint)nIters, inBaseOffset);
                        break;
                    case 4:
                        CopyTo_4_np(this as StorageT, ret, iterators, (uint)nIters, inBaseOffset);
                        break;
                    case 8:
                        CopyTo_8_np(this as StorageT, ret, iterators, (uint)nIters, inBaseOffset);
                        break;
                    case 16:
                        CopyTo_16_np(this as StorageT, ret, iterators, (uint)nIters, inBaseOffset);
                        break;
                    default:
                        CopyTo_Arbitrary_np(this as StorageT, ret, iterators, (uint)nIters, inBaseOffset, SizeOfT);
                        //throw new InvalidProgramException($"Unsupported element type: {typeof(T).Name}");
                        break; 
                }
            } else {
                CopyTo_T_np(this as StorageT, ret, iterators, (uint)nIters, inBaseOffset);

            }
            (ret as LogicalStorage)?.SetNumberTrues(-1);
            return ret;
        }

        #region Copy_To_?_np

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcStorage"></param>
        /// <param name="destStorage"></param>
        /// <param name="itP">prepared array of iterators.</param>
        /// <param name="nIter"></param>
        /// <param name="baseOffset">Base offset in element unit.</param>
        
        internal static void CopyTo_2_np(StorageT srcStorage, StorageT destStorage,
                                        Iterators.MultidimIterator* itP, uint nIter,
                                        long baseOffset) {

            // This handles empty storages! (by handling empty iterators)

            // iterators are provided for ndimsOut. Scalar dims have been removed and accumulated into baseOffset. 

            var srcSize = srcStorage.m_size;

            ushort* pOut = (ushort*)destStorage.m_handles[0].Pointer + destStorage.m_size.BaseOffset;
            ushort* pIn = (ushort*)srcStorage.m_handles[0].Pointer + srcSize.BaseOffset + baseOffset;

            if (nIter == 0) {  // all indices are scalars
                pOut[0] = pIn[0];
                return;
            }

            // initialize all dims
            // we work ROW MAJOR! itP is renamed to the LAST dim
            itP = itP + nIter - 1;
            if (!itP->MoveNext()) {
                return; // nothing to do. Empty right side.
            }
            uint setCount0 = itP->m_setCount != (uint*)0 ? itP->m_setCount[0] : 1;
            long higdims = 0;
            uint i = setCount0;
            uint ndimsIn = srcSize.NumberOfDimensions;
            // highdims up from 2nd "dimension" /or starts after index array set 
            for (; i < nIter;) {
                if (!(itP - i)->MoveNext()) {
                    return; // nothing to do. Empty right side.
                }
                // OOR should not happen, since IIterators are always checked for OOR during creation (in this path): TODO: make sure indices are in range! 
                // System.Diagnostics.Debug.Assert(val <= iterators[i].GetLastDimensionIndex());
                // throw new IndexOutOfRangeException($"Index '{val}' exceeds the valid range of dimension {i}: 0 <= i < {iterators[i].GetLastDimensionIndex()}.");
                var setCount = (itP - i)->m_setCount != (uint*)0 ? (itP - i)->m_setCount[0] : 1;
                for (int j = 0; j < setCount; j++) {
                    higdims += ((itP - (i + j))->Current * (itP - (i + j))->m_inStride);
                }
                i += setCount;
            }

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            while (true) {

                do {
                    var idx = higdims + itP->Current * itP->m_inStride;

                    for (int j = 1; j < setCount0; j++) {
                        idx += (itP - j)->Current * (itP - j)->m_inStride;
                    }
                    pOut[0] = pIn[idx];
                    pOut++;
                } while (itP->MoveNext());
                itP->Reset();

                // increase higher dims. "Higher dims" start with the first non-leading dim. Leading dims may refer to the set of dimensions formed by the set of index arrays.
                uint d = setCount0;
                while (d < nIter) {

                    if ((itP - d)->MoveNext()) {
                        break;
                    } else {
                        (itP - d)->Reset();
                        (itP - d)->MoveNext();  // assuming this succeeds. It fails if it[d] is empty. but in this case we should have exited already.
                        d += (itP - d)->m_setCount != (uint*)0 ? (itP - d)->m_setCount[0] : 1;
                    }
                }
                if (d == nIter) {
                    return;
                } else {
                    System.Diagnostics.Debug.Assert(d < nIter);
                    // recompute highdims
                    higdims = 0;
                    for (var k = setCount0; k < nIter;) {
                        higdims += ((itP - k)->Current * (itP - k)->m_inStride);
                        var setCount = (itP - k)->m_setCount != (uint*)0 ? (itP - k)->m_setCount[0] : 1;
                        for (int j = 1; j < setCount; j++) {
                            higdims += ((itP - (k + j))->Current * (itP - (k + j))->m_inStride);
                        }
                        k += setCount;
                    }
                    itP->MoveNext();
                }
            }
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 

       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcStorage"></param>
        /// <param name="destStorage"></param>
        /// <param name="itP">prepared array of iterators.</param>
        /// <param name="nIter"></param>
        /// <param name="baseOffset">Base offset in element unit.</param>
        
        internal static void CopyTo_16_np(StorageT srcStorage, StorageT destStorage,
                                        Iterators.MultidimIterator* itP, uint nIter,
                                        long baseOffset) {

            // This handles empty storages! (by handling empty iterators)

            // iterators are provided for ndimsOut. Scalar dims have been removed and accumulated into baseOffset. 

            var srcSize = srcStorage.m_size;

            complex* pOut = (complex*)destStorage.m_handles[0].Pointer + destStorage.m_size.BaseOffset;
            complex* pIn = (complex*)srcStorage.m_handles[0].Pointer + srcSize.BaseOffset + baseOffset;

            if (nIter == 0) {  // all indices are scalars
                pOut[0] = pIn[0];
                return;
            }

            // initialize all dims
            // we work ROW MAJOR! itP is renamed to the LAST dim
            itP = itP + nIter - 1;
            if (!itP->MoveNext()) {
                return; // nothing to do. Empty right side.
            }
            uint setCount0 = itP->m_setCount != (uint*)0 ? itP->m_setCount[0] : 1;
            long higdims = 0;
            uint i = setCount0;
            uint ndimsIn = srcSize.NumberOfDimensions;
            // highdims up from 2nd "dimension" /or starts after index array set 
            for (; i < nIter;) {
                if (!(itP - i)->MoveNext()) {
                    return; // nothing to do. Empty right side.
                }
                // OOR should not happen, since IIterators are always checked for OOR during creation (in this path): TODO: make sure indices are in range! 
                // System.Diagnostics.Debug.Assert(val <= iterators[i].GetLastDimensionIndex());
                // throw new IndexOutOfRangeException($"Index '{val}' exceeds the valid range of dimension {i}: 0 <= i < {iterators[i].GetLastDimensionIndex()}.");
                var setCount = (itP - i)->m_setCount != (uint*)0 ? (itP - i)->m_setCount[0] : 1;
                for (int j = 0; j < setCount; j++) {
                    higdims += ((itP - (i + j))->Current * (itP - (i + j))->m_inStride);
                }
                i += setCount;
            }

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            while (true) {

                do {
                    var idx = higdims + itP->Current * itP->m_inStride;

                    for (int j = 1; j < setCount0; j++) {
                        idx += (itP - j)->Current * (itP - j)->m_inStride;
                    }
                    pOut[0] = pIn[idx];
                    pOut++;
                } while (itP->MoveNext());
                itP->Reset();

                // increase higher dims. "Higher dims" start with the first non-leading dim. Leading dims may refer to the set of dimensions formed by the set of index arrays.
                uint d = setCount0;
                while (d < nIter) {

                    if ((itP - d)->MoveNext()) {
                        break;
                    } else {
                        (itP - d)->Reset();
                        (itP - d)->MoveNext();  // assuming this succeeds. It fails if it[d] is empty. but in this case we should have exited already.
                        d += (itP - d)->m_setCount != (uint*)0 ? (itP - d)->m_setCount[0] : 1;
                    }
                }
                if (d == nIter) {
                    return;
                } else {
                    System.Diagnostics.Debug.Assert(d < nIter);
                    // recompute highdims
                    higdims = 0;
                    for (var k = setCount0; k < nIter;) {
                        higdims += ((itP - k)->Current * (itP - k)->m_inStride);
                        var setCount = (itP - k)->m_setCount != (uint*)0 ? (itP - k)->m_setCount[0] : 1;
                        for (int j = 1; j < setCount; j++) {
                            higdims += ((itP - (k + j))->Current * (itP - (k + j))->m_inStride);
                        }
                        k += setCount;
                    }
                    itP->MoveNext();
                }
            }
        }

       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcStorage"></param>
        /// <param name="destStorage"></param>
        /// <param name="itP">prepared array of iterators.</param>
        /// <param name="nIter"></param>
        /// <param name="baseOffset">Base offset in element unit.</param>
        
        internal static void CopyTo_8_np(StorageT srcStorage, StorageT destStorage,
                                        Iterators.MultidimIterator* itP, uint nIter,
                                        long baseOffset) {

            // This handles empty storages! (by handling empty iterators)

            // iterators are provided for ndimsOut. Scalar dims have been removed and accumulated into baseOffset. 

            var srcSize = srcStorage.m_size;

            ulong* pOut = (ulong*)destStorage.m_handles[0].Pointer + destStorage.m_size.BaseOffset;
            ulong* pIn = (ulong*)srcStorage.m_handles[0].Pointer + srcSize.BaseOffset + baseOffset;

            if (nIter == 0) {  // all indices are scalars
                pOut[0] = pIn[0];
                return;
            }

            // initialize all dims
            // we work ROW MAJOR! itP is renamed to the LAST dim
            itP = itP + nIter - 1;
            if (!itP->MoveNext()) {
                return; // nothing to do. Empty right side.
            }
            uint setCount0 = itP->m_setCount != (uint*)0 ? itP->m_setCount[0] : 1;
            long higdims = 0;
            uint i = setCount0;
            uint ndimsIn = srcSize.NumberOfDimensions;
            // highdims up from 2nd "dimension" /or starts after index array set 
            for (; i < nIter;) {
                if (!(itP - i)->MoveNext()) {
                    return; // nothing to do. Empty right side.
                }
                // OOR should not happen, since IIterators are always checked for OOR during creation (in this path): TODO: make sure indices are in range! 
                // System.Diagnostics.Debug.Assert(val <= iterators[i].GetLastDimensionIndex());
                // throw new IndexOutOfRangeException($"Index '{val}' exceeds the valid range of dimension {i}: 0 <= i < {iterators[i].GetLastDimensionIndex()}.");
                var setCount = (itP - i)->m_setCount != (uint*)0 ? (itP - i)->m_setCount[0] : 1;
                for (int j = 0; j < setCount; j++) {
                    higdims += ((itP - (i + j))->Current * (itP - (i + j))->m_inStride);
                }
                i += setCount;
            }

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            while (true) {

                do {
                    var idx = higdims + itP->Current * itP->m_inStride;

                    for (int j = 1; j < setCount0; j++) {
                        idx += (itP - j)->Current * (itP - j)->m_inStride;
                    }
                    pOut[0] = pIn[idx];
                    pOut++;
                } while (itP->MoveNext());
                itP->Reset();

                // increase higher dims. "Higher dims" start with the first non-leading dim. Leading dims may refer to the set of dimensions formed by the set of index arrays.
                uint d = setCount0;
                while (d < nIter) {

                    if ((itP - d)->MoveNext()) {
                        break;
                    } else {
                        (itP - d)->Reset();
                        (itP - d)->MoveNext();  // assuming this succeeds. It fails if it[d] is empty. but in this case we should have exited already.
                        d += (itP - d)->m_setCount != (uint*)0 ? (itP - d)->m_setCount[0] : 1;
                    }
                }
                if (d == nIter) {
                    return;
                } else {
                    System.Diagnostics.Debug.Assert(d < nIter);
                    // recompute highdims
                    higdims = 0;
                    for (var k = setCount0; k < nIter;) {
                        higdims += ((itP - k)->Current * (itP - k)->m_inStride);
                        var setCount = (itP - k)->m_setCount != (uint*)0 ? (itP - k)->m_setCount[0] : 1;
                        for (int j = 1; j < setCount; j++) {
                            higdims += ((itP - (k + j))->Current * (itP - (k + j))->m_inStride);
                        }
                        k += setCount;
                    }
                    itP->MoveNext();
                }
            }
        }

       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcStorage"></param>
        /// <param name="destStorage"></param>
        /// <param name="itP">prepared array of iterators.</param>
        /// <param name="nIter"></param>
        /// <param name="baseOffset">Base offset in element unit.</param>
        
        internal static void CopyTo_4_np(StorageT srcStorage, StorageT destStorage,
                                        Iterators.MultidimIterator* itP, uint nIter,
                                        long baseOffset) {

            // This handles empty storages! (by handling empty iterators)

            // iterators are provided for ndimsOut. Scalar dims have been removed and accumulated into baseOffset. 

            var srcSize = srcStorage.m_size;

            uint* pOut = (uint*)destStorage.m_handles[0].Pointer + destStorage.m_size.BaseOffset;
            uint* pIn = (uint*)srcStorage.m_handles[0].Pointer + srcSize.BaseOffset + baseOffset;

            if (nIter == 0) {  // all indices are scalars
                pOut[0] = pIn[0];
                return;
            }

            // initialize all dims
            // we work ROW MAJOR! itP is renamed to the LAST dim
            itP = itP + nIter - 1;
            if (!itP->MoveNext()) {
                return; // nothing to do. Empty right side.
            }
            uint setCount0 = itP->m_setCount != (uint*)0 ? itP->m_setCount[0] : 1;
            long higdims = 0;
            uint i = setCount0;
            uint ndimsIn = srcSize.NumberOfDimensions;
            // highdims up from 2nd "dimension" /or starts after index array set 
            for (; i < nIter;) {
                if (!(itP - i)->MoveNext()) {
                    return; // nothing to do. Empty right side.
                }
                // OOR should not happen, since IIterators are always checked for OOR during creation (in this path): TODO: make sure indices are in range! 
                // System.Diagnostics.Debug.Assert(val <= iterators[i].GetLastDimensionIndex());
                // throw new IndexOutOfRangeException($"Index '{val}' exceeds the valid range of dimension {i}: 0 <= i < {iterators[i].GetLastDimensionIndex()}.");
                var setCount = (itP - i)->m_setCount != (uint*)0 ? (itP - i)->m_setCount[0] : 1;
                for (int j = 0; j < setCount; j++) {
                    higdims += ((itP - (i + j))->Current * (itP - (i + j))->m_inStride);
                }
                i += setCount;
            }

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            while (true) {

                do {
                    var idx = higdims + itP->Current * itP->m_inStride;

                    for (int j = 1; j < setCount0; j++) {
                        idx += (itP - j)->Current * (itP - j)->m_inStride;
                    }
                    pOut[0] = pIn[idx];
                    pOut++;
                } while (itP->MoveNext());
                itP->Reset();

                // increase higher dims. "Higher dims" start with the first non-leading dim. Leading dims may refer to the set of dimensions formed by the set of index arrays.
                uint d = setCount0;
                while (d < nIter) {

                    if ((itP - d)->MoveNext()) {
                        break;
                    } else {
                        (itP - d)->Reset();
                        (itP - d)->MoveNext();  // assuming this succeeds. It fails if it[d] is empty. but in this case we should have exited already.
                        d += (itP - d)->m_setCount != (uint*)0 ? (itP - d)->m_setCount[0] : 1;
                    }
                }
                if (d == nIter) {
                    return;
                } else {
                    System.Diagnostics.Debug.Assert(d < nIter);
                    // recompute highdims
                    higdims = 0;
                    for (var k = setCount0; k < nIter;) {
                        higdims += ((itP - k)->Current * (itP - k)->m_inStride);
                        var setCount = (itP - k)->m_setCount != (uint*)0 ? (itP - k)->m_setCount[0] : 1;
                        for (int j = 1; j < setCount; j++) {
                            higdims += ((itP - (k + j))->Current * (itP - (k + j))->m_inStride);
                        }
                        k += setCount;
                    }
                    itP->MoveNext();
                }
            }
        }

       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcStorage"></param>
        /// <param name="destStorage"></param>
        /// <param name="itP">prepared array of iterators.</param>
        /// <param name="nIter"></param>
        /// <param name="baseOffset">Base offset in element unit.</param>
        
        internal static void CopyTo_1_np(StorageT srcStorage, StorageT destStorage,
                                        Iterators.MultidimIterator* itP, uint nIter,
                                        long baseOffset) {

            // This handles empty storages! (by handling empty iterators)

            // iterators are provided for ndimsOut. Scalar dims have been removed and accumulated into baseOffset. 

            var srcSize = srcStorage.m_size;

            byte* pOut = (byte*)destStorage.m_handles[0].Pointer + destStorage.m_size.BaseOffset;
            byte* pIn = (byte*)srcStorage.m_handles[0].Pointer + srcSize.BaseOffset + baseOffset;

            if (nIter == 0) {  // all indices are scalars
                pOut[0] = pIn[0];
                return;
            }

            // initialize all dims
            // we work ROW MAJOR! itP is renamed to the LAST dim
            itP = itP + nIter - 1;
            if (!itP->MoveNext()) {
                return; // nothing to do. Empty right side.
            }
            uint setCount0 = itP->m_setCount != (uint*)0 ? itP->m_setCount[0] : 1;
            long higdims = 0;
            uint i = setCount0;
            uint ndimsIn = srcSize.NumberOfDimensions;
            // highdims up from 2nd "dimension" /or starts after index array set 
            for (; i < nIter;) {
                if (!(itP - i)->MoveNext()) {
                    return; // nothing to do. Empty right side.
                }
                // OOR should not happen, since IIterators are always checked for OOR during creation (in this path): TODO: make sure indices are in range! 
                // System.Diagnostics.Debug.Assert(val <= iterators[i].GetLastDimensionIndex());
                // throw new IndexOutOfRangeException($"Index '{val}' exceeds the valid range of dimension {i}: 0 <= i < {iterators[i].GetLastDimensionIndex()}.");
                var setCount = (itP - i)->m_setCount != (uint*)0 ? (itP - i)->m_setCount[0] : 1;
                for (int j = 0; j < setCount; j++) {
                    higdims += ((itP - (i + j))->Current * (itP - (i + j))->m_inStride);
                }
                i += setCount;
            }

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            while (true) {

                do {
                    var idx = higdims + itP->Current * itP->m_inStride;

                    for (int j = 1; j < setCount0; j++) {
                        idx += (itP - j)->Current * (itP - j)->m_inStride;
                    }
                    pOut[0] = pIn[idx];
                    pOut++;
                } while (itP->MoveNext());
                itP->Reset();

                // increase higher dims. "Higher dims" start with the first non-leading dim. Leading dims may refer to the set of dimensions formed by the set of index arrays.
                uint d = setCount0;
                while (d < nIter) {

                    if ((itP - d)->MoveNext()) {
                        break;
                    } else {
                        (itP - d)->Reset();
                        (itP - d)->MoveNext();  // assuming this succeeds. It fails if it[d] is empty. but in this case we should have exited already.
                        d += (itP - d)->m_setCount != (uint*)0 ? (itP - d)->m_setCount[0] : 1;
                    }
                }
                if (d == nIter) {
                    return;
                } else {
                    System.Diagnostics.Debug.Assert(d < nIter);
                    // recompute highdims
                    higdims = 0;
                    for (var k = setCount0; k < nIter;) {
                        higdims += ((itP - k)->Current * (itP - k)->m_inStride);
                        var setCount = (itP - k)->m_setCount != (uint*)0 ? (itP - k)->m_setCount[0] : 1;
                        for (int j = 1; j < setCount; j++) {
                            higdims += ((itP - (k + j))->Current * (itP - (k + j))->m_inStride);
                        }
                        k += setCount;
                    }
                    itP->MoveNext();
                }
            }
        }

#endregion HYCALPER AUTO GENERATED CODE

        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcStorage"></param>
        /// <param name="destStorage"></param>
        /// <param name="itP">prepared array of iterators.</param>
        /// <param name="nIter"></param>
        /// <param name="baseOffset">Base offset in element unit.</param>
        /// <param name="elementSize">m_size of a single element in bytes</param>
        
        internal static void CopyTo_Arbitrary_np(StorageT srcStorage, StorageT destStorage,
                                        Iterators.MultidimIterator* itP, uint nIter,
                                        long baseOffset, uint elementSize) {

            // This handles empty storages! (by handling empty iterators)

            // iterators are provided for ndimsOut. Scalar dims have been removed and accumulated into baseOffset. 

            var srcSize = srcStorage.m_size;

            byte* pOut = (byte*)destStorage.m_handles[0].Pointer + destStorage.m_size.BaseOffset * elementSize;
            byte* pIn = (byte*)srcStorage.m_handles[0].Pointer + (srcSize.BaseOffset + baseOffset) * elementSize;

            if (nIter == 0) {  // all indices are scalars
                for (int j = 0; j < elementSize; j++) {
                    pOut[j] = pIn[j];
                }
                return;
            }

            // initialize all dims
            // we work ROW MAJOR! itP is renamed to the LAST dim
            itP = itP + nIter - 1;
            if (!itP->MoveNext()) {
                return; // nothing to do. Empty right side.
            }
            uint setCount0 = itP->m_setCount != (uint*)0 ? itP->m_setCount[0] : 1;
            long higdims = 0;
            uint i = setCount0;
            uint ndimsIn = srcSize.NumberOfDimensions;
            // highdims up from 2nd "dimension" /or starts after index array set 
            for (; i < nIter;) {
                if (!(itP - i)->MoveNext()) {
                    return; // nothing to do. Empty right side.
                }
                // OOR should not happen, since IIterators are always checked for OOR during creation (in this path): TODO: make sure indices are in range! 
                // System.Diagnostics.Debug.Assert(val <= iterators[i].GetLastDimensionIndex());
                // throw new IndexOutOfRangeException($"Index '{val}' exceeds the valid range of dimension {i}: 0 <= i < {iterators[i].GetLastDimensionIndex()}.");
                var setCount = (itP - i)->m_setCount != (uint*)0 ? (itP - i)->m_setCount[0] : 1;
                for (int j = 0; j < setCount; j++) {
                    higdims += ((itP - (i + j))->Current * (itP - (i + j))->m_inStride);
                }
                i += setCount;
            }

            System.Diagnostics.Debug.Assert(typeof(T).IsValueType);

            while (true) {

                do {
                    var idx = higdims + itP->Current * itP->m_inStride;

                    for (int j = 1; j < setCount0; j++) {
                        idx += (itP - j)->Current * (itP - j)->m_inStride;
                    }
                    // copy all bytes of the arbitrary sized element
                    for (int k = 0; k < elementSize; k++) {
                        pOut[k] = pIn[idx * elementSize + k];
                    }
                    pOut += elementSize;
                } while (itP->MoveNext());
                itP->Reset();

                // increase higher dims. "Higher dims" start with the first non-leading dim. Leading dims may refer to the set of dimensions formed by the set of index arrays.
                uint d = setCount0;
                while (d < nIter) {

                    if ((itP - d)->MoveNext()) {
                        break;
                    } else {
                        (itP - d)->Reset();
                        (itP - d)->MoveNext();  // assuming this succeeds. It fails if it[d] is empty. but in this case we should have exited already.
                        d += (itP - d)->m_setCount != (uint*)0 ? (itP - d)->m_setCount[0] : 1;
                    }
                }
                if (d == nIter) {
                    return;
                } else {
                    System.Diagnostics.Debug.Assert(d < nIter);
                    // recompute highdims
                    higdims = 0;
                    for (var k = setCount0; k < nIter;) {
                        higdims += ((itP - k)->Current * (itP - k)->m_inStride);
                        var setCount = (itP - k)->m_setCount != (uint*)0 ? (itP - k)->m_setCount[0] : 1;
                        for (int j = 1; j < setCount; j++) {
                            higdims += ((itP - (k + j))->Current * (itP - (k + j))->m_inStride);
                        }
                        k += setCount;
                    }
                    itP->MoveNext();
                }
            }
        }

        internal virtual void CopyTo_T_np(StorageT srcStorage, StorageT destStorage,
                                Iterators.MultidimIterator* itP, uint nIter,
                                long baseOffset) {

            // This handles empty storages! (by handling empty iterators)
            System.Diagnostics.Debug.Assert(!typeof(T).IsValueType);
            T[] outA = (destStorage.m_handles[0] as MemoryLayer.ManagedHostHandle<T>).HostArray;
            long outP = destStorage.m_size.BaseOffset;
            T[] pIn = (srcStorage.m_handles[0] as MemoryLayer.ManagedHostHandle<T>).HostArray;

            // iterators are provided for ndimsOut. Scalar dims have been removed and accumulated into baseOffset. 
            var srcSize = srcStorage.m_size;
            if (nIter == 0) {  // all indices are scalars
                outA[outP] = pIn[srcSize.BaseOffset + baseOffset];
                return;
            }

            uint ndimsIn = srcSize.NumberOfDimensions;

            long higdims = srcSize.BaseOffset + baseOffset;
            uint setCount0 = itP->m_setCount != (uint*)0 ? itP->m_setCount[0] : 1;
            uint i = 0;

            // initialize all dims
            // we work ROW MAJOR! itP is renamed to the LAST dim
            itP = itP + nIter - 1;
            if (!itP->MoveNext()) {
                return; // nothing to do. Empty right side.
            }
            i += setCount0;
            // highdims up from 2nd "dimension" /or starts after index array set 
            for (; i < nIter;) {
                if (!(itP - i)->MoveNext()) {
                    return; // nothing to do. Empty right side.
                }
                // OOR should not happen, since IIterators are always checked for OOR during creation (in this path): TODO: make sure indices are in range! 
                // System.Diagnostics.Debug.Assert(val <= iterators[i].GetLastDimensionIndex());
                // throw new IndexOutOfRangeException($"Index '{val}' exceeds the valid range of dimension {i}: 0 <= i < {iterators[i].GetLastDimensionIndex()}.");
                var setCount = (itP - i)->m_setCount != (uint*)0 ? (itP - i)->m_setCount[0] : 1;
                for (int j = 0; j < setCount; j++) {
                    higdims += ((itP - (i + j))->Current * (itP - (i + j))->m_inStride);
                }
                i += setCount;
            }

            while (true) {

                do {
                    var idx = higdims + itP->Current * itP->m_inStride;

                    for (int j = 1; j < setCount0; j++) {
                        idx += (itP - j)->Current * (itP - j)->m_inStride;
                    }
                    outA[outP++] = pIn[idx];

                } while (itP->MoveNext());
                itP->Reset();

                // increase higher dims. "Higher dims" start with the first non-leading dim. Leading dims may refer to the set of dimensions formed by the set of index arrays.
                uint d = setCount0;
                while (d < nIter) {

                    if ((itP - d)->MoveNext()) {
                        break;
                    } else {
                        (itP - d)->Reset();
                        (itP - d)->MoveNext();  // assuming this succeeds. It fails if it[d] is empty. but in this case we should have exited already.
                        d += (itP - d)->m_setCount != (uint*)0 ? (itP - d)->m_setCount[0] : 1;
                    }
                }
                if (d == nIter) {
                    return;
                } else {
                    System.Diagnostics.Debug.Assert(d < nIter);
                    // recompute highdims
                    higdims = srcSize.BaseOffset + baseOffset;
                    for (var k = setCount0; k < nIter;) {
                        higdims += ((itP - k)->Current * (itP - k)->m_inStride);
                        var setCount = (itP - k)->m_setCount != (uint*)0 ? (itP - k)->m_setCount[0] : 1;
                        for (int j = 1; j < setCount; j++) {
                            higdims += ((itP - (k + j))->Current * (itP - (k + j))->m_inStride);
                        }
                        k += setCount;
                    }
                    itP->MoveNext();
                }
            }
        }
        #endregion Copy_To_?_np

        internal unsafe void collectArrayIndex(BaseArray d0, ref uint nrIters, ref uint inDimIdx,
            ref int firstIDXArrayPos, long** tmpBuffer, uint* nrIdxOutDims,
            uint* setCount, long* idxOutDims, long* idxCurPos,
            Iterators.MultidimIterator* iterators) {

            if (d0 is DimSpec || object.Equals(d0, null)) {

                #region dimspec index
                if (d0 is NewaxisSpec || object.Equals(d0, null)) {
                    tmpBuffer[0][0] = 1;
                    tmpBuffer[0][1] = 1;
                    tmpBuffer[0][2] = 0;
                    Iterators.MultidimIterator.Set(ref iterators[nrIters], inDimIdx,
                        null, NumericType.Int64, (uint*)(*tmpBuffer + 1),
                        0, *tmpBuffer, *tmpBuffer + 1,
                        *tmpBuffer + 2, 0, null,
                        0);
                    *tmpBuffer += 3;
                    nrIters++;
                    // no inDimIdx increment!
                } else {
                    var ds = d0 as DimSpec;
                    ds.Evaluate(m_size[inDimIdx] - 1);
                    tmpBuffer[0][0] = ds.Step;
                    tmpBuffer[0][1] = ds.Length;
                    tmpBuffer[0][2] = 0;
                    tmpBuffer[0][3] = (ds.IsSingleIndex || inDimIdx >= m_size.NumberOfDimensions) ? 0 : 1; // nrDims. marks as scalar index or virt.dimension, so that the dim is sorted out later. 
                    Iterators.MultidimIterator.Set(ref iterators[nrIters], inDimIdx,
                        null, NumericType.Int64, (uint*)(*tmpBuffer + 3),
                        ds.Start, *tmpBuffer, *tmpBuffer + 1,
                        *tmpBuffer + 2, m_size[(long)inDimIdx] - 1, null,
                        m_size.GetStride(inDimIdx));
                    *tmpBuffer += 4;
                    inDimIdx++;
                    nrIters++;
                }
                #endregion dimspec index

            } else if (d0 is BaseArray<bool>) {

                #region bool index
                var logical = (d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>).Storage;
                if (logical.m_size.NumberOfDimensions == 0) {

                    if (firstIDXArrayPos < 0) {
                        // first index array of the set
                        firstIDXArrayPos = (int)inDimIdx;
                    }

                    // np scalars are handled almost like slices: setcount is set but not incremented, no BasePointer is used 
                    // Note: it is important though, that the iterator is distinguishable from slices later! The 
                    // position of scalars influences the outdim / the out position of the index set's dimensions. 
                    // The differenciation will be done based on a non-Null setCount member (while nrDims being 0). 
                    var val = logical.GetValue(0);

                    tmpBuffer[0][0] = 0;
                    tmpBuffer[0][1] = val ? 1 : 0;
                    tmpBuffer[0][2] = 0;
                    tmpBuffer[0][3] = 0;
                    Iterators.MultidimIterator.Set(ref iterators[nrIters], inDimIdx,
                        null, NumericType.Int64, (uint*)(*tmpBuffer + 3),
                        0, *tmpBuffer, *tmpBuffer + 1,
                        *tmpBuffer + 2, m_size[(long)inDimIdx] - 1, setCount,
                        m_size.GetStride(inDimIdx));
                    *tmpBuffer += 4;
                    inDimIdx++;
                    nrIters++;

                } else {

                    if (firstIDXArrayPos < 0) {
                        // first index array of the set
                        firstIDXArrayPos = (int)inDimIdx;
                    }
                    // bool arrays may address multiple input dimensions
                    if (inDimIdx + logical.m_size.NumberOfDimensions > m_size.NumberOfDimensions && logical.NumberTrues > 1) {
                        throw new ArgumentOutOfRangeException($"The number of dimensions in this array is not sufficient to be addressed by the boolean array provided for dimension #{inDimIdx}.");
                    }
                    for (uint l = 0; l < logical.m_size.NumberOfDimensions; l++) {

                        var reversedDimIdx = l + logical.m_size.NumberOfDimensions - 1;
                        createMultidimIteratorBool(logical, l, setCount,
                                                nrIdxOutDims, idxOutDims, idxCurPos,
                                                tmpBuffer,
                                                ref iterators[nrIters],
                                                m_size[(long)inDimIdx] - 1, // lastDimIdx is used for lazy OOR error checking in MultidimIterator
                                                inDimIdx);
                        inDimIdx++;
                        nrIters++;
                    }
                }
                #endregion 

            } else if (d0 is BaseArray<string>) {

                #region string index
                var strStorage = (d0 as ConcreteArray<string, Array<string>, InArray<string>, OutArray<string>, Array<string>, Storage<string>>).Storage;
                if (strStorage.m_size.NumberOfElements != 1) {
                    throw new ArgumentException($"Invalid subarray specification for dimension #{inDimIdx}. In numpy ArrayStyle and when using strings as index specification a string must address exactly one single, simple range within a single dimension. Multiple strings found. See: https://ilnumerics.net/subarrays-v5.html");
                }
                var strVal = strStorage.GetValue(0);
                long start, step, length;
                if (string.IsNullOrWhiteSpace(strVal)) {

                    #region empty string range translates to empty range: 0:1:-1
                    start = 0;
                    step = 1;
                    length = 0;
                    #endregion

                } else {

                    #region regular range (single)
                    if (strVal.Contains(';')) {
                        throw new ArgumentException($"Invalid subarray specification for dimension #{inDimIdx}. In numpy ArrayStyle and when using strings as index specification a string must address exactly one single, simple range within a single dimension. Found: multiple dimensions addressed. See: https://ilnumerics.net/subarrays-v5.html");
                    } else if (strVal.Contains(',')) {
                        throw new ArgumentException($"Invalid subarray specification for dimension #{inDimIdx}. In numpy ArrayStyle and when using strings as index specification a string must address exactly one single, simple range within a single dimension. Found: multiple ranges addressed. See: https://ilnumerics.net/subarrays-v5.html");
                    }
                    try {
                        var strIt = new StringIndicesIterator(strVal, m_size[(long)inDimIdx] - 1, true);

                        if (!strIt.GetStepSize().HasValue) {
                            throw new ArgumentException();
                        }
                        step = strIt.GetStepSize().GetValueOrDefault();
                        start = strIt.GetMinimum().GetValueOrDefault();
                        length = strIt.GetLength();
                    } catch (ArgumentException exc) {
                        throw new ArgumentException($"Invalid string index specification for dimension #{inDimIdx}. Inspect the inner exception for more details!", exc);
                    }
                    #endregion
                }
                tmpBuffer[0][0] = step;
                tmpBuffer[0][1] = length;
                tmpBuffer[0][2] = 0;
                tmpBuffer[0][3] = 1;
                Iterators.MultidimIterator.Set(ref iterators[nrIters], inDimIdx,
                    null, NumericType.Int64, nrDims: (uint*)(*tmpBuffer + 3),
                    offset: start, strides: *tmpBuffer, outDims: *tmpBuffer + 1,
                    curPos: *tmpBuffer + 2, lastDimIdx: m_size[(long)inDimIdx] - 1,
                    setCount: null, outStride: m_size.GetStride(inDimIdx));
                *tmpBuffer += 4;
                nrIters++;
                inDimIdx++;
                #endregion

            } else {
                #region multidim / scalar, numeric / expression index array
                if (false) {

                    #region HYCALPER LOOPSTART

                    /*!HC:TYPELIST:
                    <hycalper>
                    <type>
                        <source locate="here">
                            int
                        </source>
                        <destination>sbyte</destination>
                        <destination>byte</destination>
                        <destination>ushort</destination>
                        <destination>short</destination>
                        <destination>ulong</destination>
                        <destination>float</destination>
                        <destination>long</destination>
                        <destination>uint</destination>
                        <destination>double</destination>
                    </type>
                    <type>
                        <source locate="after" endmark=",">
                            elementType
                        </source>
                        <destination>NumericType.SByte</destination>
                        <destination>NumericType.Byte</destination>
                        <destination>NumericType.UInt16</destination>
                        <destination>NumericType.Int16</destination>
                        <destination>NumericType.UInt64</destination>
                        <destination>NumericType.Single</destination>
                        <destination>NumericType.Int64</destination>
                        <destination>NumericType.UInt32</destination>
                        <destination>NumericType.Double</destination>
                    </type>
                    </hycalper>
                    */

                } else if (d0 is BaseArray<int>) {
                    var Astorage = (d0 as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>).Storage;
                    if (Astorage.m_size.NumberOfDimensions == 0) {

                        if (firstIDXArrayPos < 0) {
                            // first index array of the set
                            firstIDXArrayPos = (Int32)nrIters;
                        }

                        // np scalars are handled almost like slices: setcount is set but not incremented, no BasePointer is used 
                        // Note: it is important though, that the iterator is distinguishable from slices later! The 
                        // position of scalars influences the outdim / the out position of the index set's dimensions. 
                        // The differenciation will be done based on a non-Null setCount member (while nrDims being 0). 
                        long val = (long)Astorage.GetValue(0);
                        if (val < 0) {
                            val += m_size[(long)inDimIdx];
                        }
                        // OOR is not checked during iteration! Scalar dimensions are removed before iteration starts. Thus, we check OOR here: 
                        if (m_size[(long)inDimIdx] == 0 || (ulong)val > (ulong)(m_size[(long)inDimIdx] - 1)) {
                            throw new IndexOutOfRangeException($"Scalar index provided for dimension {inDimIdx} was out of range. Expected: -{m_size[(long)inDimIdx]} <= d < {m_size[(long)inDimIdx]}. Found: {Astorage.GetValue(0)}.");
                        }
                        tmpBuffer[0][0] = 0;
                        tmpBuffer[0][1] = 1;
                        tmpBuffer[0][2] = 0;
                        tmpBuffer[0][3] = 0;
                        Iterators.MultidimIterator.Set(ref iterators[nrIters], inDimIdx,
                            null, NumericType.Int64, (UInt32*)(*tmpBuffer + 3),
                            val, *tmpBuffer, *tmpBuffer + 1,
                            *tmpBuffer + 2, m_size[(long)inDimIdx] - 1, setCount,
                            m_size.GetStride(inDimIdx));
                        *tmpBuffer += 4;
                        nrIters++;

                    } else {

                        var lastDimIdx = m_size[(long)inDimIdx] - 1;
                        if (Astorage.m_size.NumberOfElements > 0) {
                            int min, max;
                            if (!Astorage.GetLimits(out min, out max)) {
                                throw new IndexOutOfRangeException($"The array for dimension #{inDimIdx} is not a valid index array. Its limits could not be determined.");
                            }
                            if ((long)min < -(lastDimIdx + 1)) {
                                throw new IndexOutOfRangeException($"The index array for dimension #{inDimIdx} contains elements outside of the allowed range -{lastDimIdx + 1} <= i <= {lastDimIdx}. Invalid: {min}.");
                            }
                            if ((long)max > lastDimIdx) {
                                throw new IndexOutOfRangeException($"The index array for dimension #{inDimIdx} contains elements outside of the allowed range -{lastDimIdx + 1} <= i <= {lastDimIdx}. Invalid: {max}.");
                            }
                        }
                        createMultidimIterator<int>(Astorage, nrIters, /*!HC:elementType*/ NumericType.Int32, sizeof(int),
                                                ref firstIDXArrayPos, setCount,
                                                nrIdxOutDims, idxOutDims, idxCurPos,
                                                tmpBuffer,
                                                ref iterators[nrIters],
                                                lastDimIdx, inDimIdx);
                        nrIters++;
                    }
                    #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 

                   

                } else if (d0 is BaseArray<double>) {
                    var Astorage = (d0 as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>).Storage;
                    if (Astorage.m_size.NumberOfDimensions == 0) {

                        if (firstIDXArrayPos < 0) {
                            // first index array of the set
                            firstIDXArrayPos = (Int32)nrIters;
                        }

                        // np scalars are handled almost like slices: setcount is set but not incremented, no BasePodoubleer is used 
                        // Note: it is important though, that the iterator is distinguishable from slices later! The 
                        // position of scalars influences the outdim / the out position of the index set's dimensions. 
                        // The differenciation will be done based on a non-Null setCount member (while nrDims being 0). 
                        long val = (long)Astorage.GetValue(0);
                        if (val < 0) {
                            val += m_size[(long)inDimIdx];
                        }
                        // OOR is not checked during iteration! Scalar dimensions are removed before iteration starts. Thus, we check OOR here: 
                        if (m_size[(long)inDimIdx] == 0 || (ulong)val > (ulong)(m_size[(long)inDimIdx] - 1)) {
                            throw new IndexOutOfRangeException($"Scalar index provided for dimension {inDimIdx} was out of range. Expected: -{m_size[(long)inDimIdx]} <= d < {m_size[(long)inDimIdx]}. Found: {Astorage.GetValue(0)}.");
                        }
                        tmpBuffer[0][0] = 0;
                        tmpBuffer[0][1] = 1;
                        tmpBuffer[0][2] = 0;
                        tmpBuffer[0][3] = 0;
                        Iterators.MultidimIterator.Set(ref iterators[nrIters], inDimIdx,
                            null, NumericType.Int64, (UInt32*)(*tmpBuffer + 3),
                            val, *tmpBuffer, *tmpBuffer + 1,
                            *tmpBuffer + 2, m_size[(long)inDimIdx] - 1, setCount,
                            m_size.GetStride(inDimIdx));
                        *tmpBuffer += 4;
                        nrIters++;

                    } else {

                        var lastDimIdx = m_size[(long)inDimIdx] - 1;
                        if (Astorage.m_size.NumberOfElements > 0) {
                            double min, max;
                            if (!Astorage.GetLimits(out min, out max)) {
                                throw new IndexOutOfRangeException($"The array for dimension #{inDimIdx} is not a valid index array. Its limits could not be determined.");
                            }
                            if ((long)min < -(lastDimIdx + 1)) {
                                throw new IndexOutOfRangeException($"The index array for dimension #{inDimIdx} contains elements outside of the allowed range -{lastDimIdx + 1} <= i <= {lastDimIdx}. Invalid: {min}.");
                            }
                            if ((long)max > lastDimIdx) {
                                throw new IndexOutOfRangeException($"The index array for dimension #{inDimIdx} contains elements outside of the allowed range -{lastDimIdx + 1} <= i <= {lastDimIdx}. Invalid: {max}.");
                            }
                        }
                        createMultidimIterator<double>(Astorage, nrIters,  NumericType.Double, sizeof(double),
                                                ref firstIDXArrayPos, setCount,
                                                nrIdxOutDims, idxOutDims, idxCurPos,
                                                tmpBuffer,
                                                ref iterators[nrIters],
                                                lastDimIdx, inDimIdx);
                        nrIters++;
                    }

                   

                } else if (d0 is BaseArray<uint>) {
                    var Astorage = (d0 as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>).Storage;
                    if (Astorage.m_size.NumberOfDimensions == 0) {

                        if (firstIDXArrayPos < 0) {
                            // first index array of the set
                            firstIDXArrayPos = (Int32)nrIters;
                        }

                        // np scalars are handled almost like slices: setcount is set but not incremented, no BasePouinter is used 
                        // Note: it is important though, that the iterator is distinguishable from slices later! The 
                        // position of scalars influences the outdim / the out position of the index set's dimensions. 
                        // The differenciation will be done based on a non-Null setCount member (while nrDims being 0). 
                        long val = (long)Astorage.GetValue(0);
                        if (val < 0) {
                            val += m_size[(long)inDimIdx];
                        }
                        // OOR is not checked during iteration! Scalar dimensions are removed before iteration starts. Thus, we check OOR here: 
                        if (m_size[(long)inDimIdx] == 0 || (ulong)val > (ulong)(m_size[(long)inDimIdx] - 1)) {
                            throw new IndexOutOfRangeException($"Scalar index provided for dimension {inDimIdx} was out of range. Expected: -{m_size[(long)inDimIdx]} <= d < {m_size[(long)inDimIdx]}. Found: {Astorage.GetValue(0)}.");
                        }
                        tmpBuffer[0][0] = 0;
                        tmpBuffer[0][1] = 1;
                        tmpBuffer[0][2] = 0;
                        tmpBuffer[0][3] = 0;
                        Iterators.MultidimIterator.Set(ref iterators[nrIters], inDimIdx,
                            null, NumericType.Int64, (UInt32*)(*tmpBuffer + 3),
                            val, *tmpBuffer, *tmpBuffer + 1,
                            *tmpBuffer + 2, m_size[(long)inDimIdx] - 1, setCount,
                            m_size.GetStride(inDimIdx));
                        *tmpBuffer += 4;
                        nrIters++;

                    } else {

                        var lastDimIdx = m_size[(long)inDimIdx] - 1;
                        if (Astorage.m_size.NumberOfElements > 0) {
                            uint min, max;
                            if (!Astorage.GetLimits(out min, out max)) {
                                throw new IndexOutOfRangeException($"The array for dimension #{inDimIdx} is not a valid index array. Its limits could not be determined.");
                            }
                            if ((long)min < -(lastDimIdx + 1)) {
                                throw new IndexOutOfRangeException($"The index array for dimension #{inDimIdx} contains elements outside of the allowed range -{lastDimIdx + 1} <= i <= {lastDimIdx}. Invalid: {min}.");
                            }
                            if ((long)max > lastDimIdx) {
                                throw new IndexOutOfRangeException($"The index array for dimension #{inDimIdx} contains elements outside of the allowed range -{lastDimIdx + 1} <= i <= {lastDimIdx}. Invalid: {max}.");
                            }
                        }
                        createMultidimIterator<uint>(Astorage, nrIters,  NumericType.UInt32, sizeof(uint),
                                                ref firstIDXArrayPos, setCount,
                                                nrIdxOutDims, idxOutDims, idxCurPos,
                                                tmpBuffer,
                                                ref iterators[nrIters],
                                                lastDimIdx, inDimIdx);
                        nrIters++;
                    }

                   

                } else if (d0 is BaseArray<long>) {
                    var Astorage = (d0 as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>).Storage;
                    if (Astorage.m_size.NumberOfDimensions == 0) {

                        if (firstIDXArrayPos < 0) {
                            // first index array of the set
                            firstIDXArrayPos = (Int32)nrIters;
                        }

                        // np scalars are handled almost like slices: setcount is set but not incremented, no BasePolonger is used 
                        // Note: it is important though, that the iterator is distinguishable from slices later! The 
                        // position of scalars influences the outdim / the out position of the index set's dimensions. 
                        // The differenciation will be done based on a non-Null setCount member (while nrDims being 0). 
                        long val = (long)Astorage.GetValue(0);
                        if (val < 0) {
                            val += m_size[(long)inDimIdx];
                        }
                        // OOR is not checked during iteration! Scalar dimensions are removed before iteration starts. Thus, we check OOR here: 
                        if (m_size[(long)inDimIdx] == 0 || (ulong)val > (ulong)(m_size[(long)inDimIdx] - 1)) {
                            throw new IndexOutOfRangeException($"Scalar index provided for dimension {inDimIdx} was out of range. Expected: -{m_size[(long)inDimIdx]} <= d < {m_size[(long)inDimIdx]}. Found: {Astorage.GetValue(0)}.");
                        }
                        tmpBuffer[0][0] = 0;
                        tmpBuffer[0][1] = 1;
                        tmpBuffer[0][2] = 0;
                        tmpBuffer[0][3] = 0;
                        Iterators.MultidimIterator.Set(ref iterators[nrIters], inDimIdx,
                            null, NumericType.Int64, (UInt32*)(*tmpBuffer + 3),
                            val, *tmpBuffer, *tmpBuffer + 1,
                            *tmpBuffer + 2, m_size[(long)inDimIdx] - 1, setCount,
                            m_size.GetStride(inDimIdx));
                        *tmpBuffer += 4;
                        nrIters++;

                    } else {

                        var lastDimIdx = m_size[(long)inDimIdx] - 1;
                        if (Astorage.m_size.NumberOfElements > 0) {
                            long min, max;
                            if (!Astorage.GetLimits(out min, out max)) {
                                throw new IndexOutOfRangeException($"The array for dimension #{inDimIdx} is not a valid index array. Its limits could not be determined.");
                            }
                            if ((long)min < -(lastDimIdx + 1)) {
                                throw new IndexOutOfRangeException($"The index array for dimension #{inDimIdx} contains elements outside of the allowed range -{lastDimIdx + 1} <= i <= {lastDimIdx}. Invalid: {min}.");
                            }
                            if ((long)max > lastDimIdx) {
                                throw new IndexOutOfRangeException($"The index array for dimension #{inDimIdx} contains elements outside of the allowed range -{lastDimIdx + 1} <= i <= {lastDimIdx}. Invalid: {max}.");
                            }
                        }
                        createMultidimIterator<long>(Astorage, nrIters,  NumericType.Int64, sizeof(long),
                                                ref firstIDXArrayPos, setCount,
                                                nrIdxOutDims, idxOutDims, idxCurPos,
                                                tmpBuffer,
                                                ref iterators[nrIters],
                                                lastDimIdx, inDimIdx);
                        nrIters++;
                    }

                   

                } else if (d0 is BaseArray<float>) {
                    var Astorage = (d0 as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>).Storage;
                    if (Astorage.m_size.NumberOfDimensions == 0) {

                        if (firstIDXArrayPos < 0) {
                            // first index array of the set
                            firstIDXArrayPos = (Int32)nrIters;
                        }

                        // np scalars are handled almost like slices: setcount is set but not incremented, no BasePofloater is used 
                        // Note: it is important though, that the iterator is distinguishable from slices later! The 
                        // position of scalars influences the outdim / the out position of the index set's dimensions. 
                        // The differenciation will be done based on a non-Null setCount member (while nrDims being 0). 
                        long val = (long)Astorage.GetValue(0);
                        if (val < 0) {
                            val += m_size[(long)inDimIdx];
                        }
                        // OOR is not checked during iteration! Scalar dimensions are removed before iteration starts. Thus, we check OOR here: 
                        if (m_size[(long)inDimIdx] == 0 || (ulong)val > (ulong)(m_size[(long)inDimIdx] - 1)) {
                            throw new IndexOutOfRangeException($"Scalar index provided for dimension {inDimIdx} was out of range. Expected: -{m_size[(long)inDimIdx]} <= d < {m_size[(long)inDimIdx]}. Found: {Astorage.GetValue(0)}.");
                        }
                        tmpBuffer[0][0] = 0;
                        tmpBuffer[0][1] = 1;
                        tmpBuffer[0][2] = 0;
                        tmpBuffer[0][3] = 0;
                        Iterators.MultidimIterator.Set(ref iterators[nrIters], inDimIdx,
                            null, NumericType.Int64, (UInt32*)(*tmpBuffer + 3),
                            val, *tmpBuffer, *tmpBuffer + 1,
                            *tmpBuffer + 2, m_size[(long)inDimIdx] - 1, setCount,
                            m_size.GetStride(inDimIdx));
                        *tmpBuffer += 4;
                        nrIters++;

                    } else {

                        var lastDimIdx = m_size[(long)inDimIdx] - 1;
                        if (Astorage.m_size.NumberOfElements > 0) {
                            float min, max;
                            if (!Astorage.GetLimits(out min, out max)) {
                                throw new IndexOutOfRangeException($"The array for dimension #{inDimIdx} is not a valid index array. Its limits could not be determined.");
                            }
                            if ((long)min < -(lastDimIdx + 1)) {
                                throw new IndexOutOfRangeException($"The index array for dimension #{inDimIdx} contains elements outside of the allowed range -{lastDimIdx + 1} <= i <= {lastDimIdx}. Invalid: {min}.");
                            }
                            if ((long)max > lastDimIdx) {
                                throw new IndexOutOfRangeException($"The index array for dimension #{inDimIdx} contains elements outside of the allowed range -{lastDimIdx + 1} <= i <= {lastDimIdx}. Invalid: {max}.");
                            }
                        }
                        createMultidimIterator<float>(Astorage, nrIters,  NumericType.Single, sizeof(float),
                                                ref firstIDXArrayPos, setCount,
                                                nrIdxOutDims, idxOutDims, idxCurPos,
                                                tmpBuffer,
                                                ref iterators[nrIters],
                                                lastDimIdx, inDimIdx);
                        nrIters++;
                    }

                   

                } else if (d0 is BaseArray<ulong>) {
                    var Astorage = (d0 as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>).Storage;
                    if (Astorage.m_size.NumberOfDimensions == 0) {

                        if (firstIDXArrayPos < 0) {
                            // first index array of the set
                            firstIDXArrayPos = (Int32)nrIters;
                        }

                        // np scalars are handled almost like slices: setcount is set but not incremented, no BasePoulonger is used 
                        // Note: it is important though, that the iterator is distinguishable from slices later! The 
                        // position of scalars influences the outdim / the out position of the index set's dimensions. 
                        // The differenciation will be done based on a non-Null setCount member (while nrDims being 0). 
                        long val = (long)Astorage.GetValue(0);
                        if (val < 0) {
                            val += m_size[(long)inDimIdx];
                        }
                        // OOR is not checked during iteration! Scalar dimensions are removed before iteration starts. Thus, we check OOR here: 
                        if (m_size[(long)inDimIdx] == 0 || (ulong)val > (ulong)(m_size[(long)inDimIdx] - 1)) {
                            throw new IndexOutOfRangeException($"Scalar index provided for dimension {inDimIdx} was out of range. Expected: -{m_size[(long)inDimIdx]} <= d < {m_size[(long)inDimIdx]}. Found: {Astorage.GetValue(0)}.");
                        }
                        tmpBuffer[0][0] = 0;
                        tmpBuffer[0][1] = 1;
                        tmpBuffer[0][2] = 0;
                        tmpBuffer[0][3] = 0;
                        Iterators.MultidimIterator.Set(ref iterators[nrIters], inDimIdx,
                            null, NumericType.Int64, (UInt32*)(*tmpBuffer + 3),
                            val, *tmpBuffer, *tmpBuffer + 1,
                            *tmpBuffer + 2, m_size[(long)inDimIdx] - 1, setCount,
                            m_size.GetStride(inDimIdx));
                        *tmpBuffer += 4;
                        nrIters++;

                    } else {

                        var lastDimIdx = m_size[(long)inDimIdx] - 1;
                        if (Astorage.m_size.NumberOfElements > 0) {
                            ulong min, max;
                            if (!Astorage.GetLimits(out min, out max)) {
                                throw new IndexOutOfRangeException($"The array for dimension #{inDimIdx} is not a valid index array. Its limits could not be determined.");
                            }
                            if ((long)min < -(lastDimIdx + 1)) {
                                throw new IndexOutOfRangeException($"The index array for dimension #{inDimIdx} contains elements outside of the allowed range -{lastDimIdx + 1} <= i <= {lastDimIdx}. Invalid: {min}.");
                            }
                            if ((long)max > lastDimIdx) {
                                throw new IndexOutOfRangeException($"The index array for dimension #{inDimIdx} contains elements outside of the allowed range -{lastDimIdx + 1} <= i <= {lastDimIdx}. Invalid: {max}.");
                            }
                        }
                        createMultidimIterator<ulong>(Astorage, nrIters,  NumericType.UInt64, sizeof(ulong),
                                                ref firstIDXArrayPos, setCount,
                                                nrIdxOutDims, idxOutDims, idxCurPos,
                                                tmpBuffer,
                                                ref iterators[nrIters],
                                                lastDimIdx, inDimIdx);
                        nrIters++;
                    }

                   

                } else if (d0 is BaseArray<short>) {
                    var Astorage = (d0 as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>).Storage;
                    if (Astorage.m_size.NumberOfDimensions == 0) {

                        if (firstIDXArrayPos < 0) {
                            // first index array of the set
                            firstIDXArrayPos = (Int32)nrIters;
                        }

                        // np scalars are handled almost like slices: setcount is set but not incremented, no BasePoshorter is used 
                        // Note: it is important though, that the iterator is distinguishable from slices later! The 
                        // position of scalars influences the outdim / the out position of the index set's dimensions. 
                        // The differenciation will be done based on a non-Null setCount member (while nrDims being 0). 
                        long val = (long)Astorage.GetValue(0);
                        if (val < 0) {
                            val += m_size[(long)inDimIdx];
                        }
                        // OOR is not checked during iteration! Scalar dimensions are removed before iteration starts. Thus, we check OOR here: 
                        if (m_size[(long)inDimIdx] == 0 || (ulong)val > (ulong)(m_size[(long)inDimIdx] - 1)) {
                            throw new IndexOutOfRangeException($"Scalar index provided for dimension {inDimIdx} was out of range. Expected: -{m_size[(long)inDimIdx]} <= d < {m_size[(long)inDimIdx]}. Found: {Astorage.GetValue(0)}.");
                        }
                        tmpBuffer[0][0] = 0;
                        tmpBuffer[0][1] = 1;
                        tmpBuffer[0][2] = 0;
                        tmpBuffer[0][3] = 0;
                        Iterators.MultidimIterator.Set(ref iterators[nrIters], inDimIdx,
                            null, NumericType.Int64, (UInt32*)(*tmpBuffer + 3),
                            val, *tmpBuffer, *tmpBuffer + 1,
                            *tmpBuffer + 2, m_size[(long)inDimIdx] - 1, setCount,
                            m_size.GetStride(inDimIdx));
                        *tmpBuffer += 4;
                        nrIters++;

                    } else {

                        var lastDimIdx = m_size[(long)inDimIdx] - 1;
                        if (Astorage.m_size.NumberOfElements > 0) {
                            short min, max;
                            if (!Astorage.GetLimits(out min, out max)) {
                                throw new IndexOutOfRangeException($"The array for dimension #{inDimIdx} is not a valid index array. Its limits could not be determined.");
                            }
                            if ((long)min < -(lastDimIdx + 1)) {
                                throw new IndexOutOfRangeException($"The index array for dimension #{inDimIdx} contains elements outside of the allowed range -{lastDimIdx + 1} <= i <= {lastDimIdx}. Invalid: {min}.");
                            }
                            if ((long)max > lastDimIdx) {
                                throw new IndexOutOfRangeException($"The index array for dimension #{inDimIdx} contains elements outside of the allowed range -{lastDimIdx + 1} <= i <= {lastDimIdx}. Invalid: {max}.");
                            }
                        }
                        createMultidimIterator<short>(Astorage, nrIters,  NumericType.Int16, sizeof(short),
                                                ref firstIDXArrayPos, setCount,
                                                nrIdxOutDims, idxOutDims, idxCurPos,
                                                tmpBuffer,
                                                ref iterators[nrIters],
                                                lastDimIdx, inDimIdx);
                        nrIters++;
                    }

                   

                } else if (d0 is BaseArray<ushort>) {
                    var Astorage = (d0 as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>).Storage;
                    if (Astorage.m_size.NumberOfDimensions == 0) {

                        if (firstIDXArrayPos < 0) {
                            // first index array of the set
                            firstIDXArrayPos = (Int32)nrIters;
                        }

                        // np scalars are handled almost like slices: setcount is set but not incremented, no BasePoushorter is used 
                        // Note: it is important though, that the iterator is distinguishable from slices later! The 
                        // position of scalars influences the outdim / the out position of the index set's dimensions. 
                        // The differenciation will be done based on a non-Null setCount member (while nrDims being 0). 
                        long val = (long)Astorage.GetValue(0);
                        if (val < 0) {
                            val += m_size[(long)inDimIdx];
                        }
                        // OOR is not checked during iteration! Scalar dimensions are removed before iteration starts. Thus, we check OOR here: 
                        if (m_size[(long)inDimIdx] == 0 || (ulong)val > (ulong)(m_size[(long)inDimIdx] - 1)) {
                            throw new IndexOutOfRangeException($"Scalar index provided for dimension {inDimIdx} was out of range. Expected: -{m_size[(long)inDimIdx]} <= d < {m_size[(long)inDimIdx]}. Found: {Astorage.GetValue(0)}.");
                        }
                        tmpBuffer[0][0] = 0;
                        tmpBuffer[0][1] = 1;
                        tmpBuffer[0][2] = 0;
                        tmpBuffer[0][3] = 0;
                        Iterators.MultidimIterator.Set(ref iterators[nrIters], inDimIdx,
                            null, NumericType.Int64, (UInt32*)(*tmpBuffer + 3),
                            val, *tmpBuffer, *tmpBuffer + 1,
                            *tmpBuffer + 2, m_size[(long)inDimIdx] - 1, setCount,
                            m_size.GetStride(inDimIdx));
                        *tmpBuffer += 4;
                        nrIters++;

                    } else {

                        var lastDimIdx = m_size[(long)inDimIdx] - 1;
                        if (Astorage.m_size.NumberOfElements > 0) {
                            ushort min, max;
                            if (!Astorage.GetLimits(out min, out max)) {
                                throw new IndexOutOfRangeException($"The array for dimension #{inDimIdx} is not a valid index array. Its limits could not be determined.");
                            }
                            if ((long)min < -(lastDimIdx + 1)) {
                                throw new IndexOutOfRangeException($"The index array for dimension #{inDimIdx} contains elements outside of the allowed range -{lastDimIdx + 1} <= i <= {lastDimIdx}. Invalid: {min}.");
                            }
                            if ((long)max > lastDimIdx) {
                                throw new IndexOutOfRangeException($"The index array for dimension #{inDimIdx} contains elements outside of the allowed range -{lastDimIdx + 1} <= i <= {lastDimIdx}. Invalid: {max}.");
                            }
                        }
                        createMultidimIterator<ushort>(Astorage, nrIters,  NumericType.UInt16, sizeof(ushort),
                                                ref firstIDXArrayPos, setCount,
                                                nrIdxOutDims, idxOutDims, idxCurPos,
                                                tmpBuffer,
                                                ref iterators[nrIters],
                                                lastDimIdx, inDimIdx);
                        nrIters++;
                    }

                   

                } else if (d0 is BaseArray<byte>) {
                    var Astorage = (d0 as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>).Storage;
                    if (Astorage.m_size.NumberOfDimensions == 0) {

                        if (firstIDXArrayPos < 0) {
                            // first index array of the set
                            firstIDXArrayPos = (Int32)nrIters;
                        }

                        // np scalars are handled almost like slices: setcount is set but not incremented, no BasePobyteer is used 
                        // Note: it is important though, that the iterator is distinguishable from slices later! The 
                        // position of scalars influences the outdim / the out position of the index set's dimensions. 
                        // The differenciation will be done based on a non-Null setCount member (while nrDims being 0). 
                        long val = (long)Astorage.GetValue(0);
                        if (val < 0) {
                            val += m_size[(long)inDimIdx];
                        }
                        // OOR is not checked during iteration! Scalar dimensions are removed before iteration starts. Thus, we check OOR here: 
                        if (m_size[(long)inDimIdx] == 0 || (ulong)val > (ulong)(m_size[(long)inDimIdx] - 1)) {
                            throw new IndexOutOfRangeException($"Scalar index provided for dimension {inDimIdx} was out of range. Expected: -{m_size[(long)inDimIdx]} <= d < {m_size[(long)inDimIdx]}. Found: {Astorage.GetValue(0)}.");
                        }
                        tmpBuffer[0][0] = 0;
                        tmpBuffer[0][1] = 1;
                        tmpBuffer[0][2] = 0;
                        tmpBuffer[0][3] = 0;
                        Iterators.MultidimIterator.Set(ref iterators[nrIters], inDimIdx,
                            null, NumericType.Int64, (UInt32*)(*tmpBuffer + 3),
                            val, *tmpBuffer, *tmpBuffer + 1,
                            *tmpBuffer + 2, m_size[(long)inDimIdx] - 1, setCount,
                            m_size.GetStride(inDimIdx));
                        *tmpBuffer += 4;
                        nrIters++;

                    } else {

                        var lastDimIdx = m_size[(long)inDimIdx] - 1;
                        if (Astorage.m_size.NumberOfElements > 0) {
                            byte min, max;
                            if (!Astorage.GetLimits(out min, out max)) {
                                throw new IndexOutOfRangeException($"The array for dimension #{inDimIdx} is not a valid index array. Its limits could not be determined.");
                            }
                            if ((long)min < -(lastDimIdx + 1)) {
                                throw new IndexOutOfRangeException($"The index array for dimension #{inDimIdx} contains elements outside of the allowed range -{lastDimIdx + 1} <= i <= {lastDimIdx}. Invalid: {min}.");
                            }
                            if ((long)max > lastDimIdx) {
                                throw new IndexOutOfRangeException($"The index array for dimension #{inDimIdx} contains elements outside of the allowed range -{lastDimIdx + 1} <= i <= {lastDimIdx}. Invalid: {max}.");
                            }
                        }
                        createMultidimIterator<byte>(Astorage, nrIters,  NumericType.Byte, sizeof(byte),
                                                ref firstIDXArrayPos, setCount,
                                                nrIdxOutDims, idxOutDims, idxCurPos,
                                                tmpBuffer,
                                                ref iterators[nrIters],
                                                lastDimIdx, inDimIdx);
                        nrIters++;
                    }

                   

                } else if (d0 is BaseArray<sbyte>) {
                    var Astorage = (d0 as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>).Storage;
                    if (Astorage.m_size.NumberOfDimensions == 0) {

                        if (firstIDXArrayPos < 0) {
                            // first index array of the set
                            firstIDXArrayPos = (Int32)nrIters;
                        }

                        // np scalars are handled almost like slices: setcount is set but not incremented, no BasePosbyteer is used 
                        // Note: it is important though, that the iterator is distinguishable from slices later! The 
                        // position of scalars influences the outdim / the out position of the index set's dimensions. 
                        // The differenciation will be done based on a non-Null setCount member (while nrDims being 0). 
                        long val = (long)Astorage.GetValue(0);
                        if (val < 0) {
                            val += m_size[(long)inDimIdx];
                        }
                        // OOR is not checked during iteration! Scalar dimensions are removed before iteration starts. Thus, we check OOR here: 
                        if (m_size[(long)inDimIdx] == 0 || (ulong)val > (ulong)(m_size[(long)inDimIdx] - 1)) {
                            throw new IndexOutOfRangeException($"Scalar index provided for dimension {inDimIdx} was out of range. Expected: -{m_size[(long)inDimIdx]} <= d < {m_size[(long)inDimIdx]}. Found: {Astorage.GetValue(0)}.");
                        }
                        tmpBuffer[0][0] = 0;
                        tmpBuffer[0][1] = 1;
                        tmpBuffer[0][2] = 0;
                        tmpBuffer[0][3] = 0;
                        Iterators.MultidimIterator.Set(ref iterators[nrIters], inDimIdx,
                            null, NumericType.Int64, (UInt32*)(*tmpBuffer + 3),
                            val, *tmpBuffer, *tmpBuffer + 1,
                            *tmpBuffer + 2, m_size[(long)inDimIdx] - 1, setCount,
                            m_size.GetStride(inDimIdx));
                        *tmpBuffer += 4;
                        nrIters++;

                    } else {

                        var lastDimIdx = m_size[(long)inDimIdx] - 1;
                        if (Astorage.m_size.NumberOfElements > 0) {
                            sbyte min, max;
                            if (!Astorage.GetLimits(out min, out max)) {
                                throw new IndexOutOfRangeException($"The array for dimension #{inDimIdx} is not a valid index array. Its limits could not be determined.");
                            }
                            if ((long)min < -(lastDimIdx + 1)) {
                                throw new IndexOutOfRangeException($"The index array for dimension #{inDimIdx} contains elements outside of the allowed range -{lastDimIdx + 1} <= i <= {lastDimIdx}. Invalid: {min}.");
                            }
                            if ((long)max > lastDimIdx) {
                                throw new IndexOutOfRangeException($"The index array for dimension #{inDimIdx} contains elements outside of the allowed range -{lastDimIdx + 1} <= i <= {lastDimIdx}. Invalid: {max}.");
                            }
                        }
                        createMultidimIterator<sbyte>(Astorage, nrIters,  NumericType.SByte, sizeof(sbyte),
                                                ref firstIDXArrayPos, setCount,
                                                nrIdxOutDims, idxOutDims, idxCurPos,
                                                tmpBuffer,
                                                ref iterators[nrIters],
                                                lastDimIdx, inDimIdx);
                        nrIters++;
                    }

#endregion HYCALPER AUTO GENERATED CODE

                } else if (d0 is BaseArray<ILExpression>) {
                    var exprStorage = (d0 as ConcreteArray<ILExpression, Array<ILExpression>, InArray<ILExpression>, OutArray<ILExpression>, Array<ILExpression>, Storage<ILExpression>>).Storage;
                    if (exprStorage.m_size.NumberOfElements != 1) {
                        throw new ArgumentException($"The array for dimension #{inDimIdx} is not a valid index array. 'end' dimension specifiers may be used to address a single element only and not within an array of multiple indices. Ex: A[end,2]");
                    }
                    var expr = exprStorage.GetValue(0);
                    var endValue = ILExpression.Evaluate(expr.Expression, m_size[inDimIdx] - 1);
                    tmpBuffer[0][0] = 0;
                    tmpBuffer[0][1] = 1;
                    tmpBuffer[0][2] = 0;
                    Iterators.MultidimIterator.Set(ref iterators[nrIters], inDimIdx,
                        null, NumericType.Int64, (uint*)(*tmpBuffer + 1),
                        endValue, *tmpBuffer, *tmpBuffer + 1,
                        *tmpBuffer + 2, m_size[(long)inDimIdx] - 1, null,
                        m_size.GetStride(inDimIdx));
                    *tmpBuffer += 3;
                    nrIters++;

                } else {
                    throw new ArgumentException($"Invalid type of index array provided as index at position #{nrIters}. Type: '{(d0.GetType().GetGenericArguments().Length > 0 ? d0.GetType().GetGenericArguments()[0].Name : d0.GetType().Name)}'");
                }
                inDimIdx++;
                #endregion

            }

        }

        private string showAllowedRange(long lastDimIdx, string i = "i") {
            if (lastDimIdx < 0) {
                return "[()] (empty range)"; 
            } else {
                return $"-{lastDimIdx + 1} <= {i} <= {lastDimIdx}"; 
            }
        }

        
        private void createMultidimIterator<ElemT>(Storage<ElemT> A,
                                                uint nrIter, NumericType numType, uint elementLength,
                                                ref int firstIDXArrayPos, uint* setCount,
                                                uint* nrIdxOutDims, long* idxOutDims, long* idxCurPos,
                                                long** buffer,
                                                ref Iterators.MultidimIterator iter,
                                                long lastDimIdx, uint inputDimIdx) {

            var A_S = A.m_size;

            System.Diagnostics.Debug.Assert(A_S.NumberOfDimensions != 0); // scalars are handled with slices

            #region is this first part of the set? setup idx properties
            if (firstIDXArrayPos < 0) {
                // first index array of the set
                firstIDXArrayPos = (int)nrIter;
            }

            for (uint i = Math.Min(A_S.NumberOfDimensions, nrIdxOutDims[0]); i-- > 0;) {
                // A is not the first non-scalar idx array in the set. Thus we know some idx dimensions already. 
                // Check if A broadcasts to the existing idx set shape

                // this was not correct: later idx arrays may increase the indexing subspace or have fewer dimensions! 
                //if (A_S.NumberOfDimensions != nrIdxOutDims) {
                //    throw new ArgumentException($"The index array provided at position #{nrIter} was expected to have {nrIdxOutDims} dimensions or to be a scalar array but was found to have {A_S.NumberOfDimensions} dimensions.");
                //}
                uint A_Sdim = A_S.NumberOfDimensions - 1 - i;
                if (idxOutDims[i] != A_S[A_Sdim]) {

                    if (idxOutDims[i] == 1) {
                        idxOutDims[i] = A_S[A_Sdim];

                    } else if (A_S[A_Sdim] != 1) {
                        var outDimsString = Global.Helper.dims2string(idxOutDims, nrIdxOutDims[0], reverse: true);
                        throw new ArgumentException($"Index array provided for dimension #{nrIter} with shape {A_S.ToString()} is not broadcastable to the shape of other index arrays: [{outDimsString}]");
#if DEBUG
                    } else {
                        System.Diagnostics.Debug.Assert(A_S[A_Sdim] == 1);
#endif
                    }
                }
            }
            while (nrIdxOutDims[0] < A_S.NumberOfDimensions) {
                // so far there were none / fewer dimensional index arrays found only. This array determines / increases the number of output dimensions.
                idxOutDims[nrIdxOutDims[0]] = A_S[A_S.NumberOfDimensions - 1 - nrIdxOutDims[0]];
                nrIdxOutDims[0]++;
            }
            // ...or A_S is smaller. But this is covered by m_size.GetStride() via virtual dimensions below.
            setCount[0]++;
            #endregion

            // setup iterator
            var strides = *buffer;
            *buffer += Size.MaxNumberOfDimensions; // would be wrong: nrIdxOutDims; -> we may find a larger idx array later!

            for (uint i = 0; i < Size.MaxNumberOfDimensions; i++) {
                strides[i] = A_S.GetStride(A_S.NumberOfDimensions - 1 - i) * elementLength;
            }
            MultidimIterator.Set(ref iter, nrIter,
                (byte*)A.m_handles[0].Pointer + A_S.BaseOffset * elementLength,
                numType, nrIdxOutDims, 0, strides,
                idxOutDims, idxCurPos, lastDimIdx, setCount,
                m_size.GetStride(inputDimIdx));
        }

        /// <summary>
        /// Setup multidim iterator for one dimension of a given boolean array.
        /// </summary>
        /// <param name="A"></param>
        /// <param name="index">The dimension index of the bool index array this iterator it to be set up for. </param>
        /// <param name="setCount"></param>
        /// <param name="nrIdxOutDims"></param>
        /// <param name="idxOutDims"></param>
        /// <param name="idxCurPos"></param>
        /// <param name="buffer"></param>
        /// <param name="iter"></param>
        /// <param name="lastDimIdx"></param>
        /// <param name="inputDimIdx"></param>
        
        private void createMultidimIteratorBool(LogicalStorage A,
                                                uint index, uint* setCount,
                                                uint* nrIdxOutDims, long* idxOutDims, long* idxCurPos,
                                                long** buffer,
                                                ref Iterators.MultidimIterator iter,
                                                long lastDimIdx, uint inputDimIdx) {

            // A MAY BE RetArray<ElemT> !!! Don't accidently dispose it off too early!
            var A_S = A.m_size;

            System.Diagnostics.Debug.Assert(A_S.NumberOfDimensions != 0); // scalars are handled with slices

            #region is this first part of the set? setup idx properties

            if (nrIdxOutDims[0] == 0) {
                // so far there were none or only scalar index arrays. This array determines the number of output dimensions.
                // set up broadcasting dimensions: 
                // A bool index always causes vector sized index arrays (-> nonzero() analogy) 
                nrIdxOutDims[0] = 1;
                idxOutDims[0] = A.NumberTrues;
            } else {
                // A is not the first non-scalar idx array in the set. Thus we know the idx dimensions (so far) already. 
                // Check if A broadcasts to the existing idx set shape

                // Note: this check is not necessary. Other (numeric index) arrays may establish a multidim. indexing subspace. We only check for the first 
                // dimension here. The bool version of MultidimIterator will iterate along the first dimension only, hence is prepared for implicit broadcasting of virtual dimensions. 
                //if (nrIdxOutDims != 1) {
                //    var outDimsString = Global.Helper.dims2string(idxOutDims, nrIdxOutDims, reverse: true);
                //    throw new ArgumentException($"Cannot broadcast boolean index array [{A.NumberTrues}] to the indexing subspace [{outDimsString}] indiced by other indices."); 
                //}
                if (idxOutDims[0] != A.NumberTrues) {

                    if (idxOutDims[0] == 1) {
                        idxOutDims[0] = A.NumberTrues;

                    } else if (A.NumberTrues != 1) {
                        var outDimsString = Global.Helper.dims2string(idxOutDims, nrIdxOutDims[0], reverse: true);
                        throw new ArgumentException($"Boolean index array provided for dimension #{inputDimIdx} with shape [{A_S.ToString()}] is not broadcastable to the advanced indexing subspace given by other index arrays: [{outDimsString}]");
#if DEBUG
                    } else {
                        System.Diagnostics.Debug.Assert(A.NumberTrues == 1);
#endif
                    }
                }
            }
            // each dimension of the bool input counts as an individual set member.
            setCount[0]++;
            #endregion

            // setup iterator
            // make sure strides are properly configured for broadcasting
            System.Diagnostics.Debug.Assert(nrIdxOutDims[0] == 1);

            var strides = *buffer;
            // strides stored in the iterator serve a special purpose for bool arrays. They store the following info: 
            // [version][nrDims][highdims][curpos][dims - 1][strides]
            // highdims are * strides + offset. 0. (base offset -> base array pointer)  
            // dims & strides are reversed! 
            // dims is minus 1! 
            // curPos has element unit (here: == bytes)! 
            if (index == A_S.NumberOfDimensions - 1) {
                // last index array: mark buffer as used
                *buffer += Size.MaxNumberOfDimensions * 3 + 3;
            }

            if (index == 0) {
                // first index array: setup strides. (The same buffer will be used for subsequent dim iterators for the same bool index array later.)
                // Note that we prepare the logical iterator to handle any potential number of leading singleton dimensions. It actual 
                // number becomes only clear after all dimension specifiers have been visited and parsed. 
                strides[0] = -1;
                strides[1] = A_S.NumberOfDimensions;
                strides[2] = 0;
                int i = 0;
                for (; i < Size.MaxNumberOfDimensions; i++) {
                    // dims - 1
                    strides[3 + Size.MaxNumberOfDimensions * 1 + i] = A_S[(uint)((int)A_S.NumberOfDimensions - 1 - i)] - 1;
                    // strides
                    strides[3 + Size.MaxNumberOfDimensions * 2 + i] = A_S.GetStride((uint)((int)A_S.NumberOfDimensions - 1 - i));
                    // curpos
                    strides[3 + i] = 0;
                }

                strides[3] = -1; // curPos before first MoveNext() call
            }
            MultidimIterator.Set(ref iter,
                // since the dimensions are stored reversed in the iterator (for row major iteration order) 
                // we will read back the iteration positions (.Current) also from reversed positions. 
                A_S.NumberOfDimensions - index - 1,
                (byte*)A.m_handles[0].Pointer + A_S.BaseOffset,
                NumericType.Boolean, nrIdxOutDims, 0, strides,  // nrIdxOutDims may yet to change! Later it is added to strides->nrDims for adding singleton dimensions for broadcasting iteration
                idxOutDims, idxCurPos, lastDimIdx, setCount,
                m_size.GetStride(inputDimIdx));
        }
        // Performs reordering of advanced indices if necessary. 
        // removes any scalar dimensions and assembles the output dimension vector.
        // removed scalar dimensions are accumulated into baseOffset for later iteration.
        private void findOutDimensionOrder(MultidimIterator* iterators, ref int nrIterDims,
                                        ref uint nrOutDims, long* outDims, int firstIDXArrayPos,
                                        long nrIdxOutDims, long* idxOutDims,
                                        ref long baseOffset, uint nMissingDims, long** buffer) {
            bool mustReorder = false, outOfSet = false;
            // when the only index is a single scalar array 'firstIDXArrayPos' == -1 and nIter will be 0. No 'index set' in this case.
            System.Diagnostics.Debug.Assert(firstIDXArrayPos < 0 || (iterators + firstIDXArrayPos)->m_setCount != (uint*)0);
            int i = firstIDXArrayPos + 1;
            if (i > 0) {
                while (i < nrIterDims) {
                    if ((iterators + i)->m_setCount != (uint*)0) {
                        if (outOfSet) {
                            mustReorder = true;
                            break;
                        }
                    } else {
                        outOfSet = true;
                    }
                    i++;
                }
                if (mustReorder) {
                    // this is not very fast, though ... :| 
                    var ll = new List<MultidimIterator>();
                    //uint setCount = iterators[firstIDXArrayPos].m_setCount[0];
                    int idxHandled = 0;
                    for (i = 0; i < nrIterDims; i++) {
                        if ((iterators + i)->m_setCount != (uint*)0) { // && (iterators + i)->m_nrDims > 0) {  <- reorder scalars also (will be removed below)
                            ll.Insert(idxHandled++, iterators[i]);
                        } else {
                            ll.Add(iterators[i]);
                        }
                    }
                    //System.Diagnostics.Debug.Assert(idxHandled == setCount); wrong! == setCount + scalarCount! 
                    i = 0;
                    foreach (var item in ll) {
                        iterators[i++] = item;
                    }
                }
            }
            nrOutDims = 0;
            bool idxSpaceInserted = false;
            // assemble output dimensions
            for (i = 0; i < nrIterDims;) {
                if (iterators[i].m_nrDims[0] == 0) {
                    //scalar dimension -> remove, OOR has been checked during creation of the MultidimIterator
                    insertIdxDims(ref nrOutDims, outDims, nrIdxOutDims, idxOutDims, ref idxSpaceInserted);
                    baseOffset += iterators[i].m_inStride * iterators[i].m_offset;
                    for (int k = i + 1; k < nrIterDims; k++) {
                        iterators[k - 1] = iterators[k];
                    }
                    nrIterDims--;

                } else if (iterators[i].m_setCount != (uint*)0) { // iterators[i].m_setCount[0] >= 1) {
                    // index arrays: numeric multidim or 'bool' 1D
                    insertIdxDims(ref nrOutDims, outDims, nrIdxOutDims, idxOutDims, ref idxSpaceInserted);
                    i += 1; // (int)iterators[i].m_setCount[0]; // would ignore other set members. but also scalars, which cannot be ignored but must be removed!
                } else if (iterators[i].m_nrDims[0] == 1) {
                    // slices, newaxis 
                    outDims[nrOutDims++] = iterators[i].m_outDims[0];
                    i++;
                }
            }
            // fill trailing ':'
            if (nrOutDims + nMissingDims > Size.MaxNumberOfDimensions) {
                throw new ArgumentOutOfRangeException($"The number of dimensions for the result ({nrOutDims + nMissingDims}) exceeds the maximum number of dimensions: {Size.MaxNumberOfDimensions}. Try to address more dimensions via scalars, which removes the corresponding dimensions from the result.");
            }
            for (i = 0; i < nMissingDims; i++) {
                var outDimIdx = m_size.NumberOfDimensions - nMissingDims + (uint)i;
                buffer[0][0] = 1; buffer[0][1] = m_size[outDimIdx]; buffer[0][2] = 0;
                MultidimIterator.Set(ref iterators[nrIterDims + i], (uint)(nrIterDims + i), null, NumericType.Int64, (uint*)*buffer, 0,
                    *buffer, *buffer + 1, *buffer + 2, m_size[outDimIdx] - 1, null, m_size.GetStride(outDimIdx));
                *buffer += 3;
                outDims[nrOutDims] = m_size[outDimIdx];
                nrOutDims++;
            }
            nrIterDims += (int)nMissingDims;
        }

        private static void insertIdxDims(ref uint nrOutDims, long* outDims, long nrIdxOutDims, long* idxOutDims, ref bool idxSpaceInserted) {
            if (!idxSpaceInserted) {
                // insert idx space here
                if (nrOutDims + nrIdxOutDims > Size.MaxNumberOfDimensions) {
                    throw new ArgumentOutOfRangeException($"The number of dimensions for the result ({nrOutDims + nrIdxOutDims}) exceeds the maximum number of dimensions: {Size.MaxNumberOfDimensions}. Try to address more dimensions via scalars, which removes the corresponding dimensions from the result.");
                }
                idxSpaceInserted = true;
                for (int j = 0; j < nrIdxOutDims; j++) {
                    outDims[nrOutDims++] = idxOutDims[nrIdxOutDims - j - 1];
                }
            }
        }
        #endregion

    }
}

