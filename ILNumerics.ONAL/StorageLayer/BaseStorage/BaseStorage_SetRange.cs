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
using ILNumerics.Core.MemoryLayer;
using ILNumerics.Core.Native;
using System;
using System.Security;

namespace ILNumerics.Core.StorageLayer {

    public abstract partial class BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> : IDisposable, IStorage
        where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

        #region SetRange common entry point for all array/storage types and all array styles
        internal virtual StorageT SetRange(StorageT value, DimSpec d0) {
            if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                return this.SetRange_ML(value, d0); // SetValue() handles Expand also
            } else {
                // numpy mode
                return this.SetRange_np(value, d0);
            }
        }
        internal virtual StorageT SetRange(StorageT value, DimSpec d0, DimSpec d1) {
            if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                return this.SetRange_ML(value, d0, d1); // SetValue() handles Expand also
            } else {
                // numpy mode
                return this.SetRange_np(value, d0, d1);
            }
        }
        internal virtual StorageT SetRange(StorageT value, DimSpec d0, DimSpec d1, DimSpec d2) {
            if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                return this.SetRange_ML(value, d0, d1, d2); // SetValue() handles Expand also
            } else {
                // numpy mode
                return this.SetRange_np(value, d0, d1, d2);
            }
        }
        internal virtual StorageT SetRange(StorageT value, DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3) {
            if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                return this.SetRange_ML(value, d0, d1, d2, d3); // SetValue() handles Expand also
            } else {
                // numpy mode
                return this.SetRange_np(value, d0, d1, d2, d3);
            }
        }
        internal virtual StorageT SetRange(StorageT value, DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3, DimSpec d4) {
            if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                return this.SetRange_ML(value, d0, d1, d2, d3, d4); // SetValue() handles Expand also
            } else {
                // numpy mode
                return this.SetRange_np(value, d0, d1, d2, d3, d4);
            }
        }
        internal virtual StorageT SetRange(StorageT value, DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3, DimSpec d4, DimSpec d5) {
            if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                return this.SetRange_ML(value, d0, d1, d2, d3, d4, d5); // SetValue() handles Expand also
            } else {
                // numpy mode
                return this.SetRange_np(value, d0, d1, d2, d3, d4, d5);
            }
        }
        internal virtual StorageT SetRange(StorageT value, DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3, DimSpec d4, DimSpec d5, DimSpec d6) {
            if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                return this.SetRange_ML(value, d0, d1, d2, d3, d4, d5, d6); // SetValue() handles Expand also
            } else {
                // numpy mode
                return this.SetRange_np(value, d0, d1, d2, d3, d4, d5, d6);
            }
        }
        internal virtual StorageT SetRange(StorageT value, DimSpec[] dims) {
            if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                return this.SetRange_ML(value, dims); // SetValue() handles Expand also
            } else {
                // numpy mode
                return this.SetRange_np(value, dims);
            }
        }
        #endregion

        #region SetRange BaseArray interface
        internal virtual StorageT SetRange(StorageT value, BaseArray d0) {
            if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                return this.SetRange_ML(value, d0); // SetValue() handles Expand also
            } else {
                // numpy mode
                return this.SetRange_np(value, d0);
            }
        }
        internal virtual StorageT SetRange(StorageT value, BaseArray d0, BaseArray d1) {
            if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                return this.SetRange_ML(value, d0, d1); // SetValue() handles Expand also
            } else {
                // numpy mode
                return this.SetRange_np(value, d0, d1);
            }
        }
        internal virtual StorageT SetRange(StorageT value, BaseArray d0, BaseArray d1, BaseArray d2) {
            if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                return this.SetRange_ML(value, d0, d1, d2); // SetValue() handles Expand also
            } else {
                // numpy mode
                return this.SetRange_np(value, d0, d1, d2);
            }
        }
        internal virtual StorageT SetRange(StorageT value, BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3) {
            if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                return this.SetRange_ML(value, d0, d1, d2, d3); // SetValue() handles Expand also
            } else {
                // numpy mode
                return this.SetRange_np(value, d0, d1, d2, d3);
            }
        }
        internal virtual StorageT SetRange(StorageT value, BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3, BaseArray d4) {
            if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                return this.SetRange_ML(value, d0, d1, d2, d3, d4); // SetValue() handles Expand also
            } else {
                // numpy mode
                return this.SetRange_np(value, d0, d1, d2, d3, d4);
            }
        }
        internal virtual StorageT SetRange(StorageT value, BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3, BaseArray d4, BaseArray d5) {
            if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                return this.SetRange_ML(value, d0, d1, d2, d3, d4, d5); // SetValue() handles Expand also
            } else {
                // numpy mode
                return this.SetRange_np(value, d0, d1, d2, d3, d4, d5);
            }
        }
        internal virtual StorageT SetRange(StorageT value, BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3, BaseArray d4, BaseArray d5, BaseArray d6) {
            if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                return this.SetRange_ML(value, d0, d1, d2, d3, d4, d5, d6); // SetValue() handles Expand also
            } else {
                // numpy mode
                return this.SetRange_np(value, d0, d1, d2, d3, d4, d5, d6);
            }
        }
        internal virtual StorageT SetRange(StorageT value, BaseArray[] dims) {
            if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                return this.SetRange_ML(value, dims); // SetValue() handles Expand also
            } else {
                // numpy mode
                return this.SetRange_np(value, dims);
            }
        }
        #endregion
    }
}
