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

        /*
         * Please read the notes regarding memory management (DimSpec), clones and mutability of storages in SetRange_ML_OOP_Extensions.cs !
         */

        #region SetRange_np DimSpec interface 

        /*
         * SetRange_np is the numpy-like implementation for mutable arrays. 
         */
        internal static unsafe StorageT SetRange_np<T, LocalT, InT, OutT, RetT, StorageT>(
            this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT value, DimSpec d0)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            #region handle ellipsis
            if (d0 is EllipsisSpec) {
                switch (storage.Size.NumberOfDimensions) {
                    case 0:
                    case 1:
                        return storage.SetRange_np(value, Globals.full);

                    case 2:
                        return storage.SetRange_np(value, Globals.full, Globals.full);

                    case 3:
                        return storage.SetRange_np(value, Globals.full, Globals.full, Globals.full);

                    case 4:
                        return storage.SetRange_np(value, Globals.full, Globals.full, Globals.full, Globals.full);

                    case 5:
                        return storage.SetRange_np(value, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full);

                    case 6:
                        return storage.SetRange_np(value, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full);

                    case 7:
                        return storage.SetRange_np(value, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full);

                    default:
                        var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.DimSpecArray; tmp[0] = d0; d0 = null;
                        return storage.SetRange_np(value, tmp, 1);
                }
            }
            #endregion

            uint inDim = 0, outDim = 0;
            long outLen = 1;
            long* outbsd = stackalloc long[Size.MaxNumberOfDimensions * 2 + 3];
            outbsd[2] = storage.Size.BaseOffset;

            storage.checkEvaluate_np(d0, ref inDim, ref outDim, outbsd, ref outLen);
            storage.finalizeBSD_np(outbsd, inDim, outDim, outLen);

            return storage.SetRangeDimSpec_np(value, outbsd);
        }
        internal static unsafe StorageT SetRange_np<T, LocalT, InT, OutT, RetT, StorageT>(
            this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT value, DimSpec d0, DimSpec d1)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            #region handle ellipsis
            if (d0 is EllipsisSpec) {
                switch (storage.Size.NumberOfDimensions + storage.countNewAxes(1, d1)) {
                    case 0:
                    case 1:
                        return storage.SetRange_np(value, d1);
                    case 2:
                        return storage.SetRange_np(value, Globals.full, d1);
                    case 3:
                        return storage.SetRange_np(value, Globals.full, Globals.full, d1);
                    case 4:
                        return storage.SetRange_np(value, Globals.full, Globals.full, Globals.full, d1);
                    case 5:
                        return storage.SetRange_np(value, Globals.full, Globals.full, Globals.full, Globals.full, d1);
                    case 6:
                        return storage.SetRange_np(value, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, d1);
                    case 7:
                        return storage.SetRange_np(value, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, d1);
                    default:
                        var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; d1 = d0 = null;
                        return storage.SetRange_np(value, tmp, 2);
                }
            }
            if (d1 is EllipsisSpec) {
                switch (storage.Size.NumberOfDimensions + storage.countNewAxes(1, d0)) {
                    case 0:
                    case 1:
                        return storage.SetRange_np(value, d0);

                    case 2:
                        return storage.SetRange_np(value, d0, Globals.full);

                    case 3:
                        return storage.SetRange_np(value, d0, Globals.full, Globals.full);

                    case 4:
                        return storage.SetRange_np(value, d0, Globals.full, Globals.full, Globals.full);

                    case 5:
                        return storage.SetRange_np(value, d0, Globals.full, Globals.full, Globals.full, Globals.full);

                    case 6:
                        return storage.SetRange_np(value, d0, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full);

                    case 7:
                        return storage.SetRange_np(value, d0, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full);

                    default:
                        var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; d1 = d0 = null;
                        return storage.SetRange_np(value, tmp, 2);
                }
            }
            #endregion

            uint inDim = 0, outDim = 0;
            long outLen = 1;
            long* outbsd = stackalloc long[Size.MaxNumberOfDimensions * 2 + 3];
            outbsd[2] = storage.Size.BaseOffset;

            storage.checkEvaluate_np(d0, ref inDim, ref outDim, outbsd, ref outLen);
            storage.checkEvaluate_np(d1, ref inDim, ref outDim, outbsd, ref outLen);
            storage.finalizeBSD_np(outbsd, inDim, outDim, outLen);

            return storage.SetRangeDimSpec_np(value, outbsd);
        }
        internal static unsafe StorageT SetRange_np<T, LocalT, InT, OutT, RetT, StorageT>(
            this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT value, DimSpec d0, DimSpec d1, DimSpec d2)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            #region handle ellipsis
            if (d0 is EllipsisSpec) {
                switch (storage.Size.NumberOfDimensions + storage.countNewAxes(2, d1, d2)) {
                    case 0:
                    case 1:
                    case 2:
                        return storage.SetRange_np(value, d1, d2);

                    case 3:
                        return storage.SetRange_np(value, Globals.full, d1, d2);

                    case 4:
                        return storage.SetRange_np(value, Globals.full, Globals.full, d1, d2);

                    case 5:
                        return storage.SetRange_np(value, Globals.full, Globals.full, Globals.full, d1, d2);

                    case 6:
                        return storage.SetRange_np(value, Globals.full, Globals.full, Globals.full, Globals.full, d1, d2);

                    case 7:
                        return storage.SetRange_np(value, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, d1, d2);

                    default:
                        var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; d2 = d1 = d0 = null;
                        return storage.SetRange_np(value, tmp, 3);

                }
            }
            if (d1 is EllipsisSpec) {
                switch (storage.Size.NumberOfDimensions + storage.countNewAxes(2, d0, d2)) {
                    case 0:
                    case 1:
                    case 2:
                        return storage.SetRange_np(value, d0, d2);

                    case 3:
                        return storage.SetRange_np(value, d0, Globals.full, d2);

                    case 4:
                        return storage.SetRange_np(value, d0, Globals.full, Globals.full, d2);

                    case 5:
                        return storage.SetRange_np(value, d0, Globals.full, Globals.full, Globals.full, d2);

                    case 6:
                        return storage.SetRange_np(value, d0, Globals.full, Globals.full, Globals.full, Globals.full, d2);

                    case 7:
                        return storage.SetRange_np(value, d0, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, d2);

                    default:
                        var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; d2 = d1 = d0 = null;
                        return storage.SetRange_np(value, tmp, 3);
                }
            }
            if (d2 is EllipsisSpec) {
                switch (storage.Size.NumberOfDimensions + storage.countNewAxes(2, d0, d1)) {
                    case 0:
                    case 1:
                    case 2:
                        return storage.SetRange_np(value, d0, d1);

                    case 3:
                        return storage.SetRange_np(value, d0, d1, Globals.full);

                    case 4:
                        return storage.SetRange_np(value, d0, d1, Globals.full, Globals.full);

                    case 5:
                        return storage.SetRange_np(value, d0, d1, Globals.full, Globals.full, Globals.full);

                    case 6:
                        return storage.SetRange_np(value, d0, d1, Globals.full, Globals.full, Globals.full, Globals.full);

                    case 7:
                        return storage.SetRange_np(value, d0, d1, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full);

                    default:
                        var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; d2 = d1 = d0 = null;
                        return storage.SetRange_np(value, tmp, 3);
                }
            }
            #endregion

            uint inDim = 0, outDim = 0;
            long outLen = 1;
            long* outbsd = stackalloc long[Size.MaxNumberOfDimensions * 2 + 3];
            outbsd[2] = storage.Size.BaseOffset;

            storage.checkEvaluate_np(d0, ref inDim, ref outDim, outbsd, ref outLen);
            storage.checkEvaluate_np(d1, ref inDim, ref outDim, outbsd, ref outLen);
            storage.checkEvaluate_np(d2, ref inDim, ref outDim, outbsd, ref outLen);
            storage.finalizeBSD_np(outbsd, inDim, outDim, outLen);

            return storage.SetRangeDimSpec_np(value, outbsd);
        }
        internal static unsafe StorageT SetRange_np<T, LocalT, InT, OutT, RetT, StorageT>(
            this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT value, DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            #region handle ellipsis
            if (d0 is EllipsisSpec) {
                switch (storage.Size.NumberOfDimensions + storage.countNewAxes(3, d1, d2, d3)) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                        return storage.SetRange_np(value, d1, d2, d3);

                    case 4:
                        return storage.SetRange_np(value, Globals.full, d1, d2, d3);

                    case 5:
                        return storage.SetRange_np(value, Globals.full, Globals.full, d1, d2, d3);

                    case 6:
                        return storage.SetRange_np(value, Globals.full, Globals.full, Globals.full, d1, d2, d3);

                    case 7:
                        return storage.SetRange_np(value, Globals.full, Globals.full, Globals.full, Globals.full, d1, d2, d3);

                    default:
                        var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; d3 = d2 = d1 = d0 = null;
                        return storage.SetRange_np(value, tmp, 4);

                }
            }
            if (d1 is EllipsisSpec) {
                switch (storage.Size.NumberOfDimensions + storage.countNewAxes(3, d0, d2, d3)) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                        return storage.SetRange_np(value, d0, d2, d3);

                    case 4:
                        return storage.SetRange_np(value, d0, Globals.full, d2, d3);

                    case 5:
                        return storage.SetRange_np(value, d0, Globals.full, Globals.full, d2, d3);

                    case 6:
                        return storage.SetRange_np(value, d0, Globals.full, Globals.full, Globals.full, d2, d3);

                    case 7:
                        return storage.SetRange_np(value, d0, Globals.full, Globals.full, Globals.full, Globals.full, d2, d3);

                    default:
                        var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; d3 = d2 = d1 = d0 = null;
                        return storage.SetRange_np(value, tmp, 4);

                }
            }
            if (d2 is EllipsisSpec) {
                switch (storage.Size.NumberOfDimensions + storage.countNewAxes(3, d0, d1, d3)) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                        return storage.SetRange_np(value, d0, d1, d3);

                    case 4:
                        return storage.SetRange_np(value, d0, d1, Globals.full, d3);

                    case 5:
                        return storage.SetRange_np(value, d0, d1, Globals.full, Globals.full, d3);

                    case 6:
                        return storage.SetRange_np(value, d0, d1, Globals.full, Globals.full, Globals.full, d3);

                    case 7:
                        return storage.SetRange_np(value, d0, d1, Globals.full, Globals.full, Globals.full, Globals.full, d3);

                    default:
                        var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; d3 = d2 = d1 = d0 = null;
                        return storage.SetRange_np(value, tmp, 4);

                }
            }
            if (d3 is EllipsisSpec) {
                switch (storage.Size.NumberOfDimensions + storage.countNewAxes(3, d0, d1, d2)) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                        return storage.SetRange_np(value, d0, d1, d2);

                    case 4:
                        return storage.SetRange_np(value, d0, d1, d2, Globals.full);

                    case 5:
                        return storage.SetRange_np(value, d0, d1, d2, Globals.full, Globals.full);

                    case 6:
                        return storage.SetRange_np(value, d0, d1, d2, Globals.full, Globals.full, Globals.full);

                    case 7:
                        return storage.SetRange_np(value, d0, d1, d2, Globals.full, Globals.full, Globals.full, Globals.full);

                    default:
                        var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; d3 = d2 = d1 = d0 = null;
                        return storage.SetRange_np(value, tmp, 4);

                }
            }
            #endregion

            uint inDim = 0, outDim = 0;
            long outLen = 1;
            long* outbsd = stackalloc long[Size.MaxNumberOfDimensions * 2 + 3];
            outbsd[2] = storage.Size.BaseOffset;

            storage.checkEvaluate_np(d0, ref inDim, ref outDim, outbsd, ref outLen);
            storage.checkEvaluate_np(d1, ref inDim, ref outDim, outbsd, ref outLen);
            storage.checkEvaluate_np(d2, ref inDim, ref outDim, outbsd, ref outLen);
            storage.checkEvaluate_np(d3, ref inDim, ref outDim, outbsd, ref outLen);
            storage.finalizeBSD_np(outbsd, inDim, outDim, outLen);

            return storage.SetRangeDimSpec_np(value, outbsd);
        }
        internal static unsafe StorageT SetRange_np<T, LocalT, InT, OutT, RetT, StorageT>(
            this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT value, DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3, DimSpec d4)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            #region handle ellipsis
            if (d0 is EllipsisSpec) {
                switch (storage.Size.NumberOfDimensions + storage.countNewAxes(4, d1, d2, d3, d4)) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        return storage.SetRange_np(value, d1, d2, d3, d4);

                    case 5:
                        return storage.SetRange_np(value, Globals.full, d1, d2, d3, d4);

                    case 6:
                        return storage.SetRange_np(value, Globals.full, Globals.full, d1, d2, d3, d4);

                    case 7:
                        return storage.SetRange_np(value, Globals.full, Globals.full, Globals.full, d1, d2, d3, d4);

                    default:
                        var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; d4 = d3 = d2 = d1 = d0 = null;
                        return storage.SetRange_np(value, tmp, 5);
                }
            }
            if (d1 is EllipsisSpec) {
                switch (storage.Size.NumberOfDimensions + storage.countNewAxes(4, d0, d2, d3, d4)) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        return storage.SetRange_np(value, d0, d2, d3, d4);

                    case 5:
                        return storage.SetRange_np(value, d0, Globals.full, d2, d3, d4);

                    case 6:
                        return storage.SetRange_np(value, d0, Globals.full, Globals.full, d2, d3, d4);

                    case 7:
                        return storage.SetRange_np(value, d0, Globals.full, Globals.full, Globals.full, d2, d3, d4);

                    default:
                        var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; d4 = d3 = d2 = d1 = d0 = null;
                        return storage.SetRange_np(value, tmp, 5);

                }
            }
            if (d2 is EllipsisSpec) {
                switch (storage.Size.NumberOfDimensions + storage.countNewAxes(4, d0, d1, d3, d4)) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        return storage.SetRange_np(value, d0, d1, d3, d4);

                    case 5:
                        return storage.SetRange_np(value, d0, d1, Globals.full, d3, d4);

                    case 6:
                        return storage.SetRange_np(value, d0, d1, Globals.full, Globals.full, d3, d4);

                    case 7:
                        return storage.SetRange_np(value, d0, d1, Globals.full, Globals.full, Globals.full, d3, d4);

                    default:
                        var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; d4 = d3 = d2 = d1 = d0 = null;
                        return storage.SetRange_np(value, tmp, 5);

                }
            }
            if (d3 is EllipsisSpec) {
                switch (storage.Size.NumberOfDimensions + storage.countNewAxes(4, d0, d1, d2, d4)) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        return storage.SetRange_np(value, d0, d1, d2, d4);

                    case 5:
                        return storage.SetRange_np(value, d0, d1, d2, Globals.full, d4);

                    case 6:
                        return storage.SetRange_np(value, d0, d1, d2, Globals.full, Globals.full, d4);

                    case 7:
                        return storage.SetRange_np(value, d0, d1, d2, Globals.full, Globals.full, Globals.full, d4);

                    default:
                        var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; d4 = d3 = d2 = d1 = d0 = null;
                        return storage.SetRange_np(value, tmp, 5);

                }
            }
            if (d4 is EllipsisSpec) {
                switch (storage.Size.NumberOfDimensions + storage.countNewAxes(4, d0, d1, d2, d3)) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        return storage.SetRange_np(value, d0, d1, d2, d3);

                    case 5:
                        return storage.SetRange_np(value, d0, d1, d2, d3, Globals.full);

                    case 6:
                        return storage.SetRange_np(value, d0, d1, d2, d3, Globals.full, Globals.full);

                    case 7:
                        return storage.SetRange_np(value, d0, d1, d2, d3, Globals.full, Globals.full, Globals.full);

                    default:
                        var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; d4 = d3 = d2 = d1 = d0 = null;
                        return storage.SetRange_np(value, tmp, 5);

                }
            }
            #endregion

            uint inDim = 0, outDim = 0;
            long outLen = 1;
            long* outbsd = stackalloc long[Size.MaxNumberOfDimensions * 2 + 3];
            outbsd[2] = storage.Size.BaseOffset;

            storage.checkEvaluate_np(d0, ref inDim, ref outDim, outbsd, ref outLen);
            storage.checkEvaluate_np(d1, ref inDim, ref outDim, outbsd, ref outLen);
            storage.checkEvaluate_np(d2, ref inDim, ref outDim, outbsd, ref outLen);
            storage.checkEvaluate_np(d3, ref inDim, ref outDim, outbsd, ref outLen);
            storage.checkEvaluate_np(d4, ref inDim, ref outDim, outbsd, ref outLen);
            storage.finalizeBSD_np(outbsd, inDim, outDim, outLen);

            return storage.SetRangeDimSpec_np(value, outbsd);
        }
        internal static unsafe StorageT SetRange_np<T, LocalT, InT, OutT, RetT, StorageT>(
            this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT value, DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3, DimSpec d4, DimSpec d5)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            #region handle ellipsis
            if (d0 is EllipsisSpec) {
                switch (storage.Size.NumberOfDimensions + storage.countNewAxes(5, d1, d2, d3, d4, d5)) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        return storage.SetRange_np(value, d1, d2, d3, d4, d5);

                    case 6:
                        return storage.SetRange_np(value, Globals.full, d1, d2, d3, d4, d5);

                    case 7:
                        return storage.SetRange_np(value, Globals.full, Globals.full, d1, d2, d3, d4, d5);

                    default:
                        var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; d5 = d4 = d3 = d2 = d1 = d0 = null;
                        return storage.SetRange_np(value, tmp, 6);

                }
            }
            if (d1 is EllipsisSpec) {
                switch (storage.Size.NumberOfDimensions + storage.countNewAxes(5, d0, d2, d3, d4, d5)) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        return storage.SetRange_np(value, d0, d2, d3, d4, d5);

                    case 6:
                        return storage.SetRange_np(value, d0, Globals.full, d2, d3, d4, d5);

                    case 7:
                        return storage.SetRange_np(value, d0, Globals.full, Globals.full, d2, d3, d4, d5);

                    default:
                        var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; d5 = d4 = d3 = d2 = d1 = d0 = null;
                        return storage.SetRange_np(value, tmp, 6);

                }
            }
            if (d2 is EllipsisSpec) {
                switch (storage.Size.NumberOfDimensions + storage.countNewAxes(5, d0, d1, d3, d4, d5)) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        return storage.SetRange_np(value, d0, d1, d3, d4, d5);

                    case 6:
                        return storage.SetRange_np(value, d0, d1, Globals.full, d3, d4, d5);

                    case 7:
                        return storage.SetRange_np(value, d0, d1, Globals.full, Globals.full, d3, d4, d5);

                    default:
                        var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; d5 = d4 = d3 = d2 = d1 = d0 = null;
                        return storage.SetRange_np(value, tmp, 6);

                }
            }
            if (d3 is EllipsisSpec) {
                switch (storage.Size.NumberOfDimensions + storage.countNewAxes(5, d0, d1, d2, d4, d5)) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        return storage.SetRange_np(value, d0, d1, d2, d4, d5);

                    case 6:
                        return storage.SetRange_np(value, d0, d1, d2, Globals.full, d4, d5);

                    case 7:
                        return storage.SetRange_np(value, d0, d1, d2, Globals.full, Globals.full, d4, d5);

                    default:
                        var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; d5 = d4 = d3 = d2 = d1 = d0 = null;
                        return storage.SetRange_np(value, tmp, 6);

                }
            }
            if (d4 is EllipsisSpec) {
                switch (storage.Size.NumberOfDimensions + storage.countNewAxes(5, d0, d1, d2, d3, d5)) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        return storage.SetRange_np(value, d0, d1, d2, d3, d5);

                    case 6:
                        return storage.SetRange_np(value, d0, d1, d2, d3, Globals.full, d5);

                    case 7:
                        return storage.SetRange_np(value, d0, d1, d2, d3, Globals.full, Globals.full, d5);

                    default:
                        var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; d5 = d4 = d3 = d2 = d1 = d0 = null;
                        return storage.SetRange_np(value, tmp, 6);

                }
            }
            if (d5 is EllipsisSpec) {
                switch (storage.Size.NumberOfDimensions + storage.countNewAxes(5, d0, d1, d2, d3, d4)) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        return storage.SetRange_np(value, d0, d1, d2, d3, d4);

                    case 6:
                        return storage.SetRange_np(value, d0, d1, d2, d3, d4, Globals.full);

                    case 7:
                        return storage.SetRange_np(value, d0, d1, d2, d3, d4, Globals.full, Globals.full);

                    default:
                        var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; d5 = d4 = d3 = d2 = d1 = d0 = null;
                        return storage.SetRange_np(value, tmp, 6);

                }
            }
            #endregion

            uint inDim = 0, outDim = 0;
            long outLen = 1;
            long* outbsd = stackalloc long[Size.MaxNumberOfDimensions * 2 + 3];
            outbsd[2] = storage.Size.BaseOffset;

            storage.checkEvaluate_np(d0, ref inDim, ref outDim, outbsd, ref outLen);
            storage.checkEvaluate_np(d1, ref inDim, ref outDim, outbsd, ref outLen);
            storage.checkEvaluate_np(d2, ref inDim, ref outDim, outbsd, ref outLen);
            storage.checkEvaluate_np(d3, ref inDim, ref outDim, outbsd, ref outLen);
            storage.checkEvaluate_np(d4, ref inDim, ref outDim, outbsd, ref outLen);
            storage.checkEvaluate_np(d5, ref inDim, ref outDim, outbsd, ref outLen);
            storage.finalizeBSD_np(outbsd, inDim, outDim, outLen);

            return storage.SetRangeDimSpec_np(value, outbsd);
        }
        internal static unsafe StorageT SetRange_np<T, LocalT, InT, OutT, RetT, StorageT>(
            this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT value, DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3, DimSpec d4, DimSpec d5, DimSpec d6)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            #region handle ellipsis
            if (d0 is EllipsisSpec) {
                switch (storage.Size.NumberOfDimensions + storage.countNewAxes(6, d1, d2, d3, d4, d5, d6)) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        return storage.SetRange_np(value, d1, d2, d3, d4, d5, d6);

                    case 7:
                        return storage.SetRange_np(value, Globals.full, d1, d2, d3, d4, d5, d6);

                    default:
                        var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6; d6 = d5 = d4 = d3 = d2 = d1 = d0 = null;
                        return storage.SetRange_np(value, tmp, 7);

                }
            }
            if (d1 is EllipsisSpec) {
                switch (storage.Size.NumberOfDimensions + storage.countNewAxes(6, d0, d2, d3, d4, d5, d6)) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        return storage.SetRange_np(value, d0, d2, d3, d4, d5, d6);

                    case 7:
                        return storage.SetRange_np(value, d0, Globals.full, d2, d3, d4, d5, d6);

                    default:
                        var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6; d6 = d5 = d4 = d3 = d2 = d1 = d0 = null;
                        return storage.SetRange_np(value, tmp, 7);

                }
            }
            if (d2 is EllipsisSpec) {
                switch (storage.Size.NumberOfDimensions + storage.countNewAxes(6, d0, d1, d3, d4, d5, d6)) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        return storage.SetRange_np(value, d0, d1, d3, d4, d5, d6);

                    case 7:
                        return storage.SetRange_np(value, d0, d1, Globals.full, d3, d4, d5, d6);

                    default:
                        var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6; d6 = d5 = d4 = d3 = d2 = d1 = d0 = null;
                        return storage.SetRange_np(value, tmp, 7);

                }
            }
            if (d3 is EllipsisSpec) {
                switch (storage.Size.NumberOfDimensions + storage.countNewAxes(6, d0, d1, d2, d4, d5, d6)) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        return storage.SetRange_np(value, d0, d1, d2, d4, d5, d6);

                    case 7:
                        return storage.SetRange_np(value, d0, d1, d2, Globals.full, d4, d5, d6);

                    default:
                        var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6; d6 = d5 = d4 = d3 = d2 = d1 = d0 = null;
                        return storage.SetRange_np(value, tmp, 7);

                }
            }
            if (d4 is EllipsisSpec) {
                switch (storage.Size.NumberOfDimensions + storage.countNewAxes(6, d0, d1, d2, d3, d5, d6)) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        return storage.SetRange_np(value, d0, d1, d2, d3, d5, d6);

                    case 7:
                        return storage.SetRange_np(value, d0, d1, d2, d3, Globals.full, d5, d6);

                    default:
                        var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6; d6 = d5 = d4 = d3 = d2 = d1 = d0 = null;
                        return storage.SetRange_np(value, tmp, 7);

                }
            }
            if (d5 is EllipsisSpec) {
                switch (storage.Size.NumberOfDimensions + storage.countNewAxes(6, d0, d1, d2, d3, d4, d6)) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        return storage.SetRange_np(value, d0, d1, d2, d3, d4, d6);

                    case 7:
                        return storage.SetRange_np(value, d0, d1, d2, d3, d4, Globals.full, d6);

                    default:
                        var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6; d6 = d5 = d4 = d3 = d2 = d1 = d0 = null;
                        return storage.SetRange_np(value, tmp, 7);

                }
            }
            if (d6 is EllipsisSpec) {
                switch (storage.Size.NumberOfDimensions + storage.countNewAxes(6, d0, d1, d2, d3, d4, d5)) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        return storage.SetRange_np(value, d0, d1, d2, d3, d4, d5);

                    case 7:
                        return storage.SetRange_np(value, d0, d1, d2, d3, d4, d5, Globals.full);

                    default:
                        var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.DimSpecArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6; d6 = d5 = d4 = d3 = d2 = d1 = d0 = null;
                        return storage.SetRange_np(value, tmp, 7);

                }
            }
            #endregion

            uint inDim = 0, outDim = 0;
            long outLen = 1;
            long* outbsd = stackalloc long[Size.MaxNumberOfDimensions * 2 + 3];
            outbsd[2] = storage.Size.BaseOffset;

            storage.checkEvaluate_np(d0, ref inDim, ref outDim, outbsd, ref outLen);
            storage.checkEvaluate_np(d1, ref inDim, ref outDim, outbsd, ref outLen);
            storage.checkEvaluate_np(d2, ref inDim, ref outDim, outbsd, ref outLen);
            storage.checkEvaluate_np(d3, ref inDim, ref outDim, outbsd, ref outLen);
            storage.checkEvaluate_np(d4, ref inDim, ref outDim, outbsd, ref outLen);
            storage.checkEvaluate_np(d5, ref inDim, ref outDim, outbsd, ref outLen);
            storage.checkEvaluate_np(d6, ref inDim, ref outDim, outbsd, ref outLen);
            storage.finalizeBSD_np(outbsd, inDim, outDim, outLen);

            return storage.SetRangeDimSpec_np(value, outbsd);
        }
        internal static unsafe StorageT SetRange_np<T, LocalT, InT, OutT, RetT, StorageT>(
            this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT value, DimSpec[] dims, uint? len = null)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            if (dims == null || (len ?? (uint)dims.Length) == 0) {
                return storage as StorageT;
            }

            var HANDLES_NDIM = len ?? (uint)dims.Length;

            if (storage.Size.NumberOfDimensions <= 7) {
                switch (HANDLES_NDIM) {
                    case 1:
                        return storage.SetRange_np(value, dims[0]);
                    case 2:
                        return storage.SetRange_np(value, dims[0], dims[1]);
                    case 3:
                        return storage.SetRange_np(value, dims[0], dims[1], dims[2]);
                    case 4:
                        return storage.SetRange_np(value, dims[0], dims[1], dims[2], dims[3]);
                    case 5:
                        return storage.SetRange_np(value, dims[0], dims[1], dims[2], dims[3], dims[4]);
                    case 6:
                        return storage.SetRange_np(value, dims[0], dims[1], dims[2], dims[3], dims[4], dims[5]);
                    case 7:
                        return storage.SetRange_np(value, dims[0], dims[1], dims[2], dims[3], dims[4], dims[5], dims[6]);
                    default:
                        break;
                        //throw new ArgumentOutOfRangeException($"The maximum number of dimensions in an array is: '{nameof(m_size)}.{nameof(Size.MaxNumberOfDimensions)}={Size.MaxNumberOfDimensions}'");
                }
            }

            #region handle ellipsis
            int i = 0;
            // We count and substract the number of provided newaxis specifiers from the given indexing arguments.
            var nrNewaxis = storage.countNewAxes(dims, HANDLES_NDIM);
            for (; i < HANDLES_NDIM; i++) {
                if (dims[i] is EllipsisSpec) {

                    BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.substituteEllipsis(dims, BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.DimSpecArray, ref HANDLES_NDIM, storage.Size.NumberOfDimensions + (uint)nrNewaxis, i);
                    dims = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.DimSpecArray;  // required for finally {... } below!
                    return storage.SetRange_np(value, dims, HANDLES_NDIM);

                }
            }
            #endregion

            uint inDim = 0, outDim = 0;
            long outLen = 1;
            long* outbsd = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.TmpBuffer1000; // stackalloc long[Size.MaxNumberOfDimensions * 2 + 3];
            outbsd[2] = storage.Size.BaseOffset;
            for (i = 0; i < HANDLES_NDIM; i++) {
                storage.checkEvaluate_np(dims[i], ref inDim, ref outDim, outbsd, ref outLen);
                dims[i] = null; // cleaning up
            }
            storage.finalizeBSD_np(outbsd, inDim, outDim, outLen);

            return storage.SetRangeDimSpec_np(value, outbsd);

        }
        #endregion

        #region SetRange_np BaseArray interface
        
        internal static unsafe StorageT SetRange_np<T, LocalT, InT, OutT, RetT, StorageT>(
            this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT value, BaseArray d0)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            System.Diagnostics.Debug.Assert(!(d0 is EllipsisSpec)); // DimSpec overload would have been called, no?

            if (d0 is EllipsisSpec) {
                return storage.SetRange_np(value, d0 as EllipsisSpec);
            }

            var iterators = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.MultidimIterators;
            long* idxOutDimsBuffer = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.TmpBuffer1000;

                // these buffers are shared between all index array members of the set: curpos (during iterations) and idxOutDims (broadcasted output size)
                long* idxCurPosBuffer = idxOutDimsBuffer + Size.MaxNumberOfDimensions;
                // this buffer is used as temp. buffer for the iterators
                long* tmpBuffer = idxCurPosBuffer + Size.MaxNumberOfDimensions; // max 3...7 * long per dim 

                int firstIDXArrayPos = -1;
                uint nrIdxOutDims = 0;
                uint inputDimIdx = 0;
                uint nrIters = 0;
                uint setCount = 0;  // will be shared with all IDX iterators

                storage.collectArrayIndex(d0, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);

                storage = storage.WriteTo_np((int)nrIters, iterators, firstIDXArrayPos, nrIdxOutDims, idxOutDimsBuffer, &tmpBuffer, value);
                return storage as StorageT; 
        }

        
        internal static unsafe StorageT SetRange_np<T, LocalT, InT, OutT, RetT, StorageT>(
            this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT value, BaseArray d0, BaseArray d1)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            var iterators = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.MultidimIterators;
            long* idxOutDimsBuffer = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.TmpBuffer1000;

                #region handle ellipsis
                if (d0 is EllipsisSpec) {
                    if ((d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (storage.Size.NumberOfDimensions + storage.countNewAxes(1, d1)) {
                        case 0:
                        case 1:
                            return storage.SetRange_np(value, d1);

                        case 2:
                            return storage.SetRange_np(value, Globals.full, d1);

                        case 3:
                            return storage.SetRange_np(value, Globals.full, Globals.full, d1);

                        case 4:
                            return storage.SetRange_np(value, Globals.full, Globals.full, Globals.full, d1);

                        case 5:
                            return storage.SetRange_np(value, Globals.full, Globals.full, Globals.full, Globals.full, d1);

                        case 6:
                            return storage.SetRange_np(value, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, d1);

                        case 7:
                            return storage.SetRange_np(value, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, d1);

                        default:
                            var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; d1 = d0 = null;
                            return storage.SetRange_np(value, tmp, 2);

                    }
                }
                if (d1 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (storage.Size.NumberOfDimensions + storage.countNewAxes(1, d0)) {
                        case 0:
                        case 1:
                            return storage.SetRange_np(value, d0);

                        case 2:
                            return storage.SetRange_np(value, d0, Globals.full);

                        case 3:
                            return storage.SetRange_np(value, d0, Globals.full, Globals.full);

                        case 4:
                            return storage.SetRange_np(value, d0, Globals.full, Globals.full, Globals.full);

                        case 5:
                            return storage.SetRange_np(value, d0, Globals.full, Globals.full, Globals.full, Globals.full);

                        case 6:
                            return storage.SetRange_np(value, d0, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full);

                        case 7:
                            return storage.SetRange_np(value, d0, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full);

                        default:
                            var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; d1 = d0 = null;
                            return storage.SetRange_np(value, tmp, 2);

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

                storage.collectArrayIndex(d0, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                storage.collectArrayIndex(d1, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                storage = storage.WriteTo_np((int)nrIters, iterators, firstIDXArrayPos,
                                          nrIdxOutDims, idxOutDimsBuffer, &tmpBuffer, value);
                return storage as StorageT; 

        }

        
        internal static unsafe StorageT SetRange_np<T, LocalT, InT, OutT, RetT, StorageT>(
            this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT value, BaseArray d0, BaseArray d1, BaseArray d2)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {
            var iterators = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.MultidimIterators;
            long* idxOutDimsBuffer = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.TmpBuffer1000;

                #region handle ellipsis
                if (d0 is EllipsisSpec) {
                    if ((d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (storage.Size.NumberOfDimensions + storage.countNewAxes(2, d1, d2)) {
                        case 0:
                        case 1:
                        case 2:
                            return storage.SetRange_np(value, d1, d2);

                        case 3:
                            return storage.SetRange_np(value, Globals.full, d1, d2);

                        case 4:
                            return storage.SetRange_np(value, Globals.full, Globals.full, d1, d2);

                        case 5:
                            return storage.SetRange_np(value, Globals.full, Globals.full, Globals.full, d1, d2);

                        case 6:
                            return storage.SetRange_np(value, Globals.full, Globals.full, Globals.full, Globals.full, d1, d2);

                        case 7:
                            return storage.SetRange_np(value, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, d1, d2);

                        default:
                            var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; d2 = d1 = d0 = null;
                            return storage.SetRange_np(value, tmp, 3);

                    }
                }
                if (d1 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (storage.Size.NumberOfDimensions + storage.countNewAxes(2, d0, d2)) {
                        case 0:
                        case 1:
                        case 2:
                            return storage.SetRange_np(value, d0, d2);

                        case 3:
                            return storage.SetRange_np(value, d0, Globals.full, d2);

                        case 4:
                            return storage.SetRange_np(value, d0, Globals.full, Globals.full, d2);

                        case 5:
                            return storage.SetRange_np(value, d0, Globals.full, Globals.full, Globals.full, d2);

                        case 6:
                            return storage.SetRange_np(value, d0, Globals.full, Globals.full, Globals.full, Globals.full, d2);

                        case 7:
                            return storage.SetRange_np(value, d0, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full, d2);

                        default:
                            var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; d2 = d1 = d0 = null;
                            return storage.SetRange_np(value, tmp, 3);

                    }
                }
                if (d2 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (storage.Size.NumberOfDimensions + storage.countNewAxes(2, d0, d1)) {
                        case 0:
                        case 1:
                        case 2:
                            return storage.SetRange_np(value, d0, d1);

                        case 3:
                            return storage.SetRange_np(value, d0, d1, Globals.full);

                        case 4:
                            return storage.SetRange_np(value, d0, d1, Globals.full, Globals.full);

                        case 5:
                            return storage.SetRange_np(value, d0, d1, Globals.full, Globals.full, Globals.full);

                        case 6:
                            return storage.SetRange_np(value, d0, d1, Globals.full, Globals.full, Globals.full, Globals.full);

                        case 7:
                            return storage.SetRange_np(value, d0, d1, Globals.full, Globals.full, Globals.full, Globals.full, Globals.full);

                        default:
                            var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; d2 = d1 = d0 = null;
                            return storage.SetRange_np(value, tmp, 3);

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

                 storage.collectArrayIndex(d0, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                 storage.collectArrayIndex(d1, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                 storage.collectArrayIndex(d2, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                 storage = storage.WriteTo_np((int)nrIters, iterators, firstIDXArrayPos,
                                          nrIdxOutDims, idxOutDimsBuffer, &tmpBuffer, value);
                return storage as StorageT;
        }

        
        internal static unsafe StorageT SetRange_np<T, LocalT, InT, OutT, RetT, StorageT>(
            this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT value, BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3) 
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {
            var iterators = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.MultidimIterators;
            long* idxOutDimsBuffer = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.TmpBuffer1000;

                #region handle ellipsis
                if (d0 is EllipsisSpec) {
                    if ((d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (storage.Size.NumberOfDimensions + storage.countNewAxes(3, d1, d2, d3)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                            return storage.SetRange_np(value, d1, d2, d3);

                        case 4:
                            return storage.SetRange_np(value, Globals.full, d1, d2, d3);

                        case 5:
                            return storage.SetRange_np(value, Globals.full, Globals.full, d1, d2, d3);

                        case 6:
                            return storage.SetRange_np(value, Globals.full, Globals.full, Globals.full, d1, d2, d3);

                        case 7:
                            return storage.SetRange_np(value, Globals.full, Globals.full, Globals.full, Globals.full, d1, d2, d3);

                        default:
                            var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_np(value, tmp, 4);

                    }
                }
                if (d1 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (storage.Size.NumberOfDimensions + storage.countNewAxes(3, d0, d2, d3)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                            return storage.SetRange_np(value, d0, d2, d3);

                        case 4:
                            return storage.SetRange_np(value, d0, Globals.full, d2, d3);

                        case 5:
                            return storage.SetRange_np(value, d0, Globals.full, Globals.full, d2, d3);

                        case 6:
                            return storage.SetRange_np(value, d0, Globals.full, Globals.full, Globals.full, d2, d3);

                        case 7:
                            return storage.SetRange_np(value, d0, Globals.full, Globals.full, Globals.full, Globals.full, d2, d3);

                        default:
                            var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_np(value, tmp, 4);

                    }
                }
                if (d2 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (storage.Size.NumberOfDimensions + storage.countNewAxes(3, d0, d1, d3)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                            return storage.SetRange_np(value, d0, d1, d3);

                        case 4:
                            return storage.SetRange_np(value, d0, d1, Globals.full, d3);

                        case 5:
                            return storage.SetRange_np(value, d0, d1, Globals.full, Globals.full, d3);

                        case 6:
                            return storage.SetRange_np(value, d0, d1, Globals.full, Globals.full, Globals.full, d3);

                        case 7:
                            return storage.SetRange_np(value, d0, d1, Globals.full, Globals.full, Globals.full, Globals.full, d3);

                        default:
                            var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_np(value, tmp, 4);

                    }
                }
                if (d3 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (storage.Size.NumberOfDimensions + storage.countNewAxes(3, d0, d1, d2)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                            return storage.SetRange_np(value, d0, d1, d2);

                        case 4:
                            return storage.SetRange_np(value, d0, d1, d2, Globals.full);

                        case 5:
                            return storage.SetRange_np(value, d0, d1, d2, Globals.full, Globals.full);

                        case 6:
                            return storage.SetRange_np(value, d0, d1, d2, Globals.full, Globals.full, Globals.full);

                        case 7:
                            return storage.SetRange_np(value, d0, d1, d2, Globals.full, Globals.full, Globals.full, Globals.full);

                        default:
                            var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_np(value, tmp, 4);

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

                storage.collectArrayIndex(d0, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                storage.collectArrayIndex(d1, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                storage.collectArrayIndex(d2, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                storage.collectArrayIndex(d3, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                storage = storage.WriteTo_np((int)nrIters, iterators, firstIDXArrayPos,
                                          nrIdxOutDims, idxOutDimsBuffer, &tmpBuffer, value);
                return storage as StorageT;
        }

        
        internal static unsafe StorageT SetRange_np<T, LocalT, InT, OutT, RetT, StorageT>(
            this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT value, BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3, BaseArray d4)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {
            var iterators = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.MultidimIterators;
            long* idxOutDimsBuffer = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.TmpBuffer1000;

                #region handle ellipsis
                if (d0 is EllipsisSpec) {
                    if ((d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d4 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (storage.Size.NumberOfDimensions + storage.countNewAxes(4, d1, d2, d3, d4)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            return storage.SetRange_np(value, d1, d2, d3, d4);

                        case 5:
                            return storage.SetRange_np(value, Globals.full, d1, d2, d3, d4);

                        case 6:
                            return storage.SetRange_np(value, Globals.full, Globals.full, d1, d2, d3, d4);

                        case 7:
                            return storage.SetRange_np(value, Globals.full, Globals.full, Globals.full, d1, d2, d3, d4);

                        default:
                            var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_np(value, tmp, 5);

                    }
                }
                if (d1 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d4 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (storage.Size.NumberOfDimensions + storage.countNewAxes(4, d0, d2, d3, d4)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            return storage.SetRange_np(value, d0, d2, d3, d4);

                        case 5:
                            return storage.SetRange_np(value, d0, Globals.full, d2, d3, d4);

                        case 6:
                            return storage.SetRange_np(value, d0, Globals.full, Globals.full, d2, d3, d4);

                        case 7:
                            return storage.SetRange_np(value, d0, Globals.full, Globals.full, Globals.full, d2, d3, d4);

                        default:
                            var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_np(value, tmp, 5);

                    }
                }
                if (d2 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d4 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (storage.Size.NumberOfDimensions + storage.countNewAxes(4, d0, d1, d3, d4)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            return storage.SetRange_np(value, d0, d1, d3, d4);

                        case 5:
                            return storage.SetRange_np(value, d0, d1, Globals.full, d3, d4);

                        case 6:
                            return storage.SetRange_np(value, d0, d1, Globals.full, Globals.full, d3, d4);

                        case 7:
                            return storage.SetRange_np(value, d0, d1, Globals.full, Globals.full, Globals.full, d3, d4);

                        default:
                            var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_np(value, tmp, 5);

                    }
                }
                if (d3 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d4 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (storage.Size.NumberOfDimensions + storage.countNewAxes(4, d0, d1, d2, d4)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            return storage.SetRange_np(value, d0, d1, d2, d4);

                        case 5:
                            return storage.SetRange_np(value, d0, d1, d2, Globals.full, d4);

                        case 6:
                            return storage.SetRange_np(value, d0, d1, d2, Globals.full, Globals.full, d4);

                        case 7:
                            return storage.SetRange_np(value, d0, d1, d2, Globals.full, Globals.full, Globals.full, d4);

                        default:
                            var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_np(value, tmp, 5);

                    }
                }
                if (d4 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (storage.Size.NumberOfDimensions + storage.countNewAxes(4, d0, d1, d2, d3)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            return storage.SetRange_np(value, d0, d1, d2, d3);

                        case 5:
                            return storage.SetRange_np(value, d0, d1, d2, d3, Globals.full);

                        case 6:
                            return storage.SetRange_np(value, d0, d1, d2, d3, Globals.full, Globals.full);

                        case 7:
                            return storage.SetRange_np(value, d0, d1, d2, d3, Globals.full, Globals.full, Globals.full);

                        default:
                            var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_np(value, tmp, 5);

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

                 storage.collectArrayIndex(d0, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                 storage.collectArrayIndex(d1, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                 storage.collectArrayIndex(d2, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                 storage.collectArrayIndex(d3, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                 storage.collectArrayIndex(d4, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                storage = storage.WriteTo_np((int)nrIters, iterators, firstIDXArrayPos,
                                          nrIdxOutDims, idxOutDimsBuffer, &tmpBuffer, value);
                return storage as StorageT;

        }

        
        internal static unsafe StorageT SetRange_np<T, LocalT, InT, OutT, RetT, StorageT>(
            this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT value, BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3, BaseArray d4, BaseArray d5)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {
            var iterators = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.MultidimIterators;
            long* idxOutDimsBuffer = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.TmpBuffer1000;

                #region handle ellipsis
                if (d0 is EllipsisSpec) {
                    if ((d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d4 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d5 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (storage.Size.NumberOfDimensions + storage.countNewAxes(5, d1, d2, d3, d4, d5)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            return storage.SetRange_np(value, d1, d2, d3, d4, d5);

                        case 6:
                            return storage.SetRange_np(value, Globals.full, d1, d2, d3, d4, d5);

                        case 7:
                            return storage.SetRange_np(value, Globals.full, Globals.full, d1, d2, d3, d4, d5);

                        default:
                            var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; d5 = d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_np(value, tmp, 6);

                    }
                }
                if (d1 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d4 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d5 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (storage.Size.NumberOfDimensions + storage.countNewAxes(5, d0, d2, d3, d4, d5)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            return storage.SetRange_np(value, d0, d2, d3, d4, d5);

                        case 6:
                            return storage.SetRange_np(value, d0, Globals.full, d2, d3, d4, d5);

                        case 7:
                            return storage.SetRange_np(value, d0, Globals.full, Globals.full, d2, d3, d4, d5);

                        default:
                            var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; d5 = d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_np(value, tmp, 6);

                    }
                }
                if (d2 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d4 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d5 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (storage.Size.NumberOfDimensions + storage.countNewAxes(5, d0, d1, d3, d4, d5)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            return storage.SetRange_np(value, d0, d1, d3, d4, d5);

                        case 6:
                            return storage.SetRange_np(value, d0, d1, Globals.full, d3, d4, d5);

                        case 7:
                            return storage.SetRange_np(value, d0, d1, Globals.full, Globals.full, d3, d4, d5);

                        default:
                            var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; d5 = d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_np(value, tmp, 6);

                    }
                }
                if (d3 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d4 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d5 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (storage.Size.NumberOfDimensions + storage.countNewAxes(5, d0, d1, d2, d4, d5)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            return storage.SetRange_np(value, d0, d1, d2, d4, d5);

                        case 6:
                            return storage.SetRange_np(value, d0, d1, d2, Globals.full, d4, d5);

                        case 7:
                            return storage.SetRange_np(value, d0, d1, d2, Globals.full, Globals.full, d4, d5);

                        default:
                            var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; d5 = d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_np(value, tmp, 6);

                    }
                }
                if (d4 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d5 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (storage.Size.NumberOfDimensions + storage.countNewAxes(5, d0, d1, d2, d3, d5)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            return storage.SetRange_np(value, d0, d1, d2, d3, d5);

                        case 6:
                            return storage.SetRange_np(value, d0, d1, d2, d3, Globals.full, d5);

                        case 7:
                            return storage.SetRange_np(value, d0, d1, d2, d3, Globals.full, Globals.full, d5);

                        default:
                            var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; d5 = d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_np(value, tmp, 6);

                    }
                }
                if (d5 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d4 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (storage.Size.NumberOfDimensions + storage.countNewAxes(5, d0, d1, d2, d3, d4)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            return storage.SetRange_np(value, d0, d1, d2, d3, d4);

                        case 6:
                            return storage.SetRange_np(value, d0, d1, d2, d3, d4, Globals.full);

                        case 7:
                            return storage.SetRange_np(value, d0, d1, d2, d3, d4, Globals.full, Globals.full);

                        default:
                            var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; d5 = d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_np(value, tmp, 6);

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

                 storage.collectArrayIndex(d0, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                 storage.collectArrayIndex(d1, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                 storage.collectArrayIndex(d2, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                 storage.collectArrayIndex(d3, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                 storage.collectArrayIndex(d4, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                 storage.collectArrayIndex(d5, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                storage = storage.WriteTo_np((int)nrIters, iterators, firstIDXArrayPos,
                                          nrIdxOutDims, idxOutDimsBuffer, &tmpBuffer, value);
                return storage as StorageT;

        }

        
        internal static unsafe StorageT SetRange_np<T, LocalT, InT, OutT, RetT, StorageT>(
            this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT value, BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3, BaseArray d4, BaseArray d5, BaseArray d6)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {
            var iterators = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.MultidimIterators;
            long* idxOutDimsBuffer = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.TmpBuffer1000;

                #region handle ellipsis
                if (d0 is EllipsisSpec) {
                    if ((d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d4 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d5 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d6 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (storage.Size.NumberOfDimensions + storage.countNewAxes(6, d1, d2, d3, d4, d5, d6)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            return storage.SetRange_np(value, d1, d2, d3, d4, d5, d6);

                        case 7:
                            return storage.SetRange_np(value, Globals.full, d1, d2, d3, d4, d5, d6);

                        default:
                            var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6; d6 = d5 = d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_np(value, tmp, 7);

                    }
                }
                if (d1 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d4 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d5 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d6 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (storage.Size.NumberOfDimensions + storage.countNewAxes(6, d0, d2, d3, d4, d5, d6)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            return storage.SetRange_np(value, d0, d2, d3, d4, d5, d6);

                        case 7:
                            return storage.SetRange_np(value, d0, Globals.full, d2, d3, d4, d5, d6);

                        default:
                            var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6; d6 = d5 = d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_np(value, tmp, 7);

                    }
                }
                if (d2 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d4 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d5 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d6 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (storage.Size.NumberOfDimensions + storage.countNewAxes(6, d0, d1, d3, d4, d5, d6)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            return storage.SetRange_np(value, d0, d1, d3, d4, d5, d6);

                        case 7:
                            return storage.SetRange_np(value, d0, d1, Globals.full, d3, d4, d5, d6);

                        default:
                            var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6; d6 = d5 = d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_np(value, tmp, 7);

                    }
                }
                if (d3 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d4 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d5 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d6 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (storage.Size.NumberOfDimensions + storage.countNewAxes(6, d0, d1, d2, d4, d5, d6)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            return storage.SetRange_np(value, d0, d1, d2, d4, d5, d6);

                        case 7:
                            return storage.SetRange_np(value, d0, d1, d2, Globals.full, d4, d5, d6);

                        default:
                            var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6; d6 = d5 = d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_np(value, tmp, 7);

                    }
                }
                if (d4 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d5 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d6 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (storage.Size.NumberOfDimensions + storage.countNewAxes(6, d0, d1, d2, d3, d5, d6)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            return storage.SetRange_np(value, d0, d1, d2, d3, d5, d6);

                        case 7:
                            return storage.SetRange_np(value, d0, d1, d2, d3, Globals.full, d5, d6);

                        default:
                            var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6; d6 = d5 = d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_np(value, tmp, 7);

                    }
                }
                if (d5 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d4 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d6 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (storage.Size.NumberOfDimensions + storage.countNewAxes(6, d0, d1, d2, d3, d4, d6)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            return storage.SetRange_np(value, d0, d1, d2, d3, d4, d6);

                        case 7:
                            return storage.SetRange_np(value, d0, d1, d2, d3, d4, Globals.full, d6);

                        default:
                            var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6; d6 = d5 = d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_np(value, tmp, 7);

                    }
                }
                if (d6 is EllipsisSpec) {
                    if ((d0 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d1 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d2 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d3 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d4 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1 ||
                        (d5 as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1) {
                        throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is currently not supported.");
                    }
                    switch (storage.Size.NumberOfDimensions + storage.countNewAxes(6, d0, d1, d2, d3, d4, d5)) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            return storage.SetRange_np(value, d0, d1, d2, d3, d4, d5);

                        case 7:
                            return storage.SetRange_np(value, d0, d1, d2, d3, d4, d5, Globals.full);

                        default:
                            var tmp = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.BaseArrayArray; tmp[0] = d0; tmp[1] = d1; tmp[2] = d2; tmp[3] = d3; tmp[4] = d4; tmp[5] = d5; tmp[6] = d6; d6 = d5 = d4 = d3 = d2 = d1 = d0 = null;
                            return storage.SetRange_np(value, tmp, 7);

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

                 storage.collectArrayIndex(d0, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                 storage.collectArrayIndex(d1, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                 storage.collectArrayIndex(d2, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                 storage.collectArrayIndex(d3, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                 storage.collectArrayIndex(d4, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                 storage.collectArrayIndex(d5, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                 storage.collectArrayIndex(d6, ref nrIters, ref inputDimIdx,
                    ref firstIDXArrayPos, &tmpBuffer,
                    &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                    iterators);
                storage = storage.WriteTo_np((int)nrIters, iterators, firstIDXArrayPos,
                                          nrIdxOutDims, idxOutDimsBuffer, &tmpBuffer, value);
                return storage as StorageT; 

        }

        
        internal static unsafe StorageT SetRange_np<T, LocalT, InT, OutT, RetT, StorageT>(
                this BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> storage, StorageT value, BaseArray[] dims, uint? len = null)
                where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
                where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
                where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            if (dims == null || (len ?? (uint)dims.Length) == 0) {
                return storage as StorageT;
            }

            var HANDLES_NDIM = len ?? (uint)dims.Length;

            if (storage.Size.NumberOfDimensions <= 7) {
                switch (HANDLES_NDIM) {
                    case 1:
                        return storage.SetRange_np(value, dims[0]);

                    case 2:
                        return storage.SetRange_np(value, dims[0], dims[1]);

                    case 3:
                        return storage.SetRange_np(value, dims[0], dims[1], dims[2]);

                    case 4:
                        return storage.SetRange_np(value, dims[0], dims[1], dims[2], dims[3]);

                    case 5:
                        return storage.SetRange_np(value, dims[0], dims[1], dims[2], dims[3], dims[4]);

                    case 6:
                        return storage.SetRange_np(value, dims[0], dims[1], dims[2], dims[3], dims[4], dims[5]);

                    case 7:
                        return storage.SetRange_np(value, dims[0], dims[1], dims[2], dims[3], dims[4], dims[5], dims[6]);

                    default:
                        break;
                        //throw new ArgumentOutOfRangeException($"The maximum number of dimensions in an array is: '{nameof(m_size)}.{nameof(Size.MaxNumberOfDimensions)}={Size.MaxNumberOfDimensions}'");
                }
            }

            int i = 0;
            #region handle ellipsis
            var nrNewaxis = storage.countNewAxes(dims, HANDLES_NDIM);
            for (; i < HANDLES_NDIM; i++) {
                if (dims[i] is EllipsisSpec) {
                    for (int ii = 0; ii < HANDLES_NDIM; ii++) {
                        if ((dims[ii] as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage.Size.NumberOfDimensions > 1) {
                            throw new NotSupportedException("Combining multidimensional boolean index arrays with the 'ellipsis' specifier is not supported.");
                        }
                    }
                    storage.substituteEllipsis(dims, BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.BaseArrayArray, ref HANDLES_NDIM, storage.Size.NumberOfDimensions + (uint)nrNewaxis, i);
                    dims = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.BaseArrayArray;
                    return storage.SetRange_np(value, dims, HANDLES_NDIM);
                }
            }
            #endregion

                var iterators = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.MultidimIterators;
                long* idxOutDimsBuffer = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Context.TmpBuffer1000;

                // these buffers are shared between all index array members of the set: curpos (during iterations) and idxOutDims (broadcasted output size)
                long* idxCurPosBuffer = idxOutDimsBuffer + Size.MaxNumberOfDimensions;
                // this buffer is used as temp. buffer for the iterators
                long* tmpBuffer = idxCurPosBuffer + Size.MaxNumberOfDimensions; // max 3...7 * long per dim 

                int firstIDXArrayPos = -1;
                uint nrIdxOutDims = 0;
                uint inputDimIdx = 0;
                uint nrIters = 0;
                uint setCount = 0;  // will be shared with all IDX iterators

                for (i = 0; i < HANDLES_NDIM; i++) {
                     storage.collectArrayIndex(dims[i], ref nrIters, ref inputDimIdx,
                        ref firstIDXArrayPos, &tmpBuffer,
                        &nrIdxOutDims, &setCount, idxOutDimsBuffer, idxCurPosBuffer,
                        iterators);
                }

                storage = storage.WriteTo_np((int)nrIters, iterators, firstIDXArrayPos,
                                          nrIdxOutDims, idxOutDimsBuffer, &tmpBuffer, value);
                return storage as StorageT; 

        }
        #endregion
    }
}