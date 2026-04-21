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

        #region Get Indexer High Level API, common implementation WITHOUT memory management. To be called by Mutable<> and by Immutable<> array get {} indexers.

        #region StorageT GET (long, ...)
        // this reflects the Mutable<T,..> indexer API - but is implemented on
        // the Storage<T> layer instead of the array layer. It respects pending 
        // storages and alwasy creates a new output storage (no reutilization of incoming RetT [for simplicity only]).
        // This API is used by Accelerator (long,long,..) and substitutes the regular this[...] indexers. 
        internal StorageT Indexer_Get(long d0) {

            const int NDIMS = 1;
            if (S.NumberOfDimensions == NDIMS) {
                // early, fast exit
                return Create(Size.GetSeqIndex(d0));

            } else if (S.NumberOfDimensions < NDIMS) {

                if (this is CellStorage) {
                    // deep indexing, root / entry cell
                    return (this as CellStorage).GetCell4Indexer_DeepIndex(d0, len: NDIMS) as StorageT;

                } else {
                    return Create(Size.GetSeqIndex(d0));
                }

            } else if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {

                return Create(Size.GetSeqIndex(d0));

            } else {
                // numpy mode
                DimSpec s0 = d0;
                var ret = GetRange_np(s0);
                s0.Dispose();
                return ret;

            }
        }
        internal StorageT Indexer_Get(long d0, long d1) {

            const int NDIMS = 2;
            if (S.NumberOfDimensions == NDIMS) {
                // early, fast exit
                return Create(Size.GetSeqIndex(d0, d1));

            } else if (S.NumberOfDimensions < NDIMS) {

                if (this is CellStorage) {
                    // deep indexing, root / entry cell
                    return (this as CellStorage).GetCell4Indexer_DeepIndex(d0, d1, len: NDIMS) as StorageT;

                } else {
                    return Create(Size.GetSeqIndex(d0, d1));

                }
                // else : my ndims > 7
            } else if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                return Create(Size.GetSeqIndex(d0, d1));

            } else {
                // numpy mode
                DimSpec s0 = d0, s1 = d1;
                var ret = GetRange_np(s0, s1);
                s0.Dispose(); s1.Dispose();
                return ret;

            }

        }
        internal StorageT Indexer_Get(long d0, long d1, long d2) {

            const int NDIMS = 3;
            if (S.NumberOfDimensions == NDIMS) {
                // early, fast exit
                return Create(Size.GetSeqIndex(d0, d1, d2));

            } else if (S.NumberOfDimensions < NDIMS) {

                if (this is CellStorage) {
                    // deep indexing, root / entry cell
                    return (this as CellStorage).GetCell4Indexer_DeepIndex(d0, d1, d2, len: NDIMS) as StorageT;

                } else {
                    return Create(Size.GetSeqIndex(d0, d1, d2));

                }
                // else : my ndims > 7
            } else if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                return Create(Size.GetSeqIndex(d0, d1, d2));

            } else {
                // numpy mode
                DimSpec s0 = d0, s1 = d1, s2 = d2;
                var ret = GetRange_np(s0, s1, s2);
                s0.Dispose(); s1.Dispose(); s2.Dispose();
                return ret;

            }

        }
        internal StorageT Indexer_Get(long d0, long d1, long d2, long d3) {

            const int NDIMS = 4;
            if (S.NumberOfDimensions == NDIMS) {
                // early, fast exit
                return Create(Size.GetSeqIndex(d0, d1, d2, d3));

            } else if (S.NumberOfDimensions < NDIMS) {

                if (this is CellStorage) {
                    // deep indexing, root / entry cell
                    return (this as CellStorage).GetCell4Indexer_DeepIndex(d0, d1, d2, d3, len: NDIMS) as StorageT;

                } else {
                    return Create(Size.GetSeqIndex(d0, d1, d2, d3));

                }
                // else : my ndims > 7
            } else if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                return Create(Size.GetSeqIndex(d0, d1, d2, d3));

            } else {
                // numpy mode
                DimSpec s0 = d0, s1 = d1, s2 = d2, s3 = d3;
                var ret = GetRange_np(s0, s1, s2, s3);
                s0.Dispose(); s1.Dispose(); s2.Dispose(); s3.Dispose();
                return ret;

            }

        }
        internal StorageT Indexer_Get(long d0, long d1, long d2, long d3, long d4) {

            const int NDIMS = 5;
            if (S.NumberOfDimensions == NDIMS) {
                // early, fast exit
                return Create(Size.GetSeqIndex(d0, d1, d2, d3, d4));

            } else if (S.NumberOfDimensions < NDIMS) {

                if (this is CellStorage) {
                    // deep indexing, root / entry cell
                    return (this as CellStorage).GetCell4Indexer_DeepIndex(d0, d1, d2, d3, d4, len: NDIMS) as StorageT;

                } else {
                    return Create(Size.GetSeqIndex(d0, d1, d2, d3, d4));

                }
                // else : my ndims > 7
            } else if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                return Create(Size.GetSeqIndex(d0, d1, d2, d3, d4));

            } else {
                // numpy mode
                DimSpec s0 = d0, s1 = d1, s2 = d2, s3 = d3, s4 = d4;
                var ret = GetRange_np(s0, s1, s2, s3, s4);
                s0.Dispose(); s1.Dispose(); s2.Dispose(); s3.Dispose(); s4.Dispose();
                return ret;

            }

        }
        internal StorageT Indexer_Get(long d0, long d1, long d2, long d3, long d4, long d5) {

            const int NDIMS = 6; 
            if (S.NumberOfDimensions == NDIMS) {
                // early, fast exit
                return Create(Size.GetSeqIndex(d0, d1, d2, d3, d4, d5));

            } else if (S.NumberOfDimensions < NDIMS) {

                if (this is CellStorage) {
                    // deep indexing, root / entry cell
                    return (this as CellStorage).GetCell4Indexer_DeepIndex(d0, d1, d2, d3, d4, d5, len: NDIMS) as StorageT;

                } else {
                    return Create(Size.GetSeqIndex(d0, d1, d2, d3, d4, d5));

                }
                // else : my ndims > 7
            } else if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                return Create(Size.GetSeqIndex(d0, d1, d2, d3, d4, d5));

            } else {
                // numpy mode
                DimSpec s0 = d0, s1 = d1, s2 = d2, s3 = d3, s4 = d4, s5 = d5;
                var ret = GetRange_np(s0, s1, s2, s3, s4, s5);
                s0.Dispose(); s1.Dispose(); s2.Dispose(); s3.Dispose(); s4.Dispose(); s5.Dispose();
                return ret;

            }

        }
        internal StorageT Indexer_Get(long d0, long d1, long d2, long d3, long d4, long d5, long d6) {

            const int NDIMS = 7;
            if (S.NumberOfDimensions == NDIMS) {
                // early, fast exit
                return Create(Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6));
            } else if (S.NumberOfDimensions < NDIMS) {
                if (this is CellStorage) {
                    // deep indexing, root / entry cell
                    return (this as CellStorage).GetCell4Indexer_DeepIndex(d0, d1, d2, d3, d4, d5, d6, len: NDIMS) as StorageT;
                } else {
                    return Create(Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6));
                }
                // else : my ndims > 7
            } else if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                return Create(Size.GetSeqIndex(d0, d1, d2, d3, d4, d5, d6));
            } else {
                // numpy mode
                DimSpec s0 = d0, s1 = d1, s2 = d2, s3 = d3, s4 = d4, s5 = d5, s6 = d6;
                var ret = GetRange_np(s0, s1, s2, s3, s4, s5, s6);
                s0.Dispose(); s1.Dispose(); s2.Dispose(); s3.Dispose(); s4.Dispose(); s5.Dispose(); s6.Dispose();
                return ret;
            }

        }

        #endregion

        #endregion
    }
}
