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
using System;

namespace ILNumerics.Core.StorageLayer {

    public abstract partial class BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> : IDisposable, IStorage
        where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

        #region Set Indexer High Level API - used by Accelerator and by A[i,..] = ... indexers
        // This reflects the Mutable<T,..> indexer API - but for Storage<T> instead of arrays. 
        internal virtual StorageT MutableIndexer_Set(StorageT value, long d0) {

            if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                if (object.Equals(value, null) || value.m_size.NumberOfElements == 0) {
                    return this.SetRange_ML(null, d0);
                } else {
                    return this.SetValue(value, d0, allowExpand: true); // SetValue() handles Expand also
                }
            } else {
                // numpy mode - check for value = null was also done in SetValue.Run().
                if (object.Equals(value, null)) {
                    throw new ArgumentNullException($"'null' is not a valid right side value in assignment expressions in '{nameof(Settings)}.{nameof(Settings.ArrayStyle)}.{nameof(ArrayStyles.numpy)}' mode. Did you mean to use removals with '{nameof(ArrayStyles.ILNumericsV4)}' array mode? See: {nameof(Settings.ArrayStyle)}.");
                } else if (m_size.NumberOfDimensions <= 1) {
                    return this.SetValue(value, d0, allowExpand: false);
                } else {
                    return this.SetRange_np(value, d0);
                }
            }

        }
        internal virtual StorageT MutableIndexer_Set(StorageT value, long d0, long d1) {

            if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                if (object.Equals(value, null) || value.m_size.NumberOfElements == 0) {
                    return this.SetRange_ML(null, d0, d1);
                } else {
                    return this.SetValue(value, d0, d1, allowExpand: true); // SetValue() handles Expand also
                }
            } else {
                // numpy mode - check for value = null was done in SetValue.Run().
                if (object.Equals(value, null)) {
                    throw new ArgumentNullException($"'null' is not a valid right side value in assignment expressions in '{nameof(ArrayStyles.numpy)}' mode. Did you mean to use removals with '{nameof(ArrayStyles.ILNumericsV4)}' array mode? See: {nameof(Settings.ArrayStyle)}.");
                } else if (m_size.NumberOfDimensions <= 2) {
                    return this.SetValue(value, d0, d1, allowExpand: false);
                } else {
                    return this.SetRange_np(value, d0, d1);
                }
            }

        }
        internal virtual StorageT MutableIndexer_Set(StorageT value, long d0, long d1, long d2) {

            if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                if (object.Equals(value, null) || value.m_size.NumberOfElements == 0) {
                    return this.SetRange_ML(null, d0, d1, d2);
                } else {
                    return this.SetValue(value, d0, d1, d2, allowExpand: true); // SetValue() handles Expand also
                }
            } else {
                // numpy mode - check for value = null was done in SetValue.Run().
                if (object.Equals(value, null)) {
                    throw new ArgumentNullException($"'null' is not a valid right side value in assignment expressions in '{nameof(ArrayStyles.numpy)}' mode. Did you mean to use removals with '{nameof(ArrayStyles.ILNumericsV4)}' array mode? See: {nameof(Settings.ArrayStyle)}.");
                } else if (m_size.NumberOfDimensions <= 3) {
                    return this.SetValue(value, d0, d1, d2, allowExpand: false);
                } else {
                    return this.SetRange_np(value, d0, d1, d2);
                }
            }

        }
        internal virtual StorageT MutableIndexer_Set(StorageT value, long d0, long d1, long d2, long d3) {

            if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                if (object.Equals(value, null) || value.m_size.NumberOfElements == 0) {
                    return this.SetRange_ML(null, d0, d1, d2, d3);
                } else {
                    return this.SetValue(value, d0, d1, d2, d3, allowExpand: true); // SetValue() handles Expand also
                }
            } else {
                // numpy mode - check for value = null was done in SetValue.Run().
                if (object.Equals(value, null)) {
                    throw new ArgumentNullException($"'null' is not a valid right side value in assignment expressions in '{nameof(ArrayStyles.numpy)}' mode. Did you mean to use removals with '{nameof(ArrayStyles.ILNumericsV4)}' array mode? See: {nameof(Settings.ArrayStyle)}.");
                } else if (m_size.NumberOfDimensions <= 4) {
                    return this.SetValue(value, d0, d1, d2, d3, allowExpand: false);
                } else {
                    return this.SetRange_np(value, d0, d1, d2, d3);
                }
            }

        }
        internal virtual StorageT MutableIndexer_Set(StorageT value, long d0, long d1, long d2, long d3, long d4) {

            if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                if (object.Equals(value, null) || value.m_size.NumberOfElements == 0) {
                    return this.SetRange_ML(null, d0, d1, d2, d3, d4);
                } else {
                    return this.SetValue(value, d0, d1, d2, d3, d4, allowExpand: true); // SetValue() handles Expand also
                }
            } else {
                // numpy mode - check for value = null was done in SetValue.Run().
                if (object.Equals(value, null)) {
                    throw new ArgumentNullException($"'null' is not a valid right side value in assignment expressions in '{nameof(ArrayStyles.numpy)}' mode. Did you mean to use removals with '{nameof(ArrayStyles.ILNumericsV4)}' array mode? See: {nameof(Settings.ArrayStyle)}.");
                } else if (m_size.NumberOfDimensions <= 5) {
                    return this.SetValue(value, d0, d1, d2, d3, d4, allowExpand: false);
                } else {
                    return this.SetRange_np(value, d0, d1, d2, d3, d4);
                }
            }

        }
        internal virtual StorageT MutableIndexer_Set(StorageT value, long d0, long d1, long d2, long d3, long d4, long d5) {

            if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                if (object.Equals(value, null) || value.m_size.NumberOfElements == 0) {
                    return this.SetRange_ML(null, d0, d1, d2, d3, d4, d5);
                } else {
                    return this.SetValue(value, d0, d1, d2, d3, d4, d5, allowExpand: true); // SetValue() handles Expand also
                }
            } else {
                // numpy mode - check for value = null was done in SetValue.Run().
                if (object.Equals(value, null)) {
                    throw new ArgumentNullException($"'null' is not a valid right side value in assignment expressions in '{nameof(ArrayStyles.numpy)}' mode. Did you mean to use removals with '{nameof(ArrayStyles.ILNumericsV4)}' array mode? See: {nameof(Settings.ArrayStyle)}.");
                } else if (m_size.NumberOfDimensions <= 6) {
                    return this.SetValue(value, d0, d1, d2, d3, d4, d5, allowExpand: false);
                } else {
                    return this.SetRange_np(value, d0, d1, d2, d3, d4, d5);
                }
            }

        }
        internal virtual StorageT MutableIndexer_Set(StorageT value, long d0, long d1, long d2, long d3, long d4, long d5, long d6) {

            if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                if (object.Equals(value, null) || value.m_size.NumberOfElements == 0) {
                    return this.SetRange_ML(null, d0, d1, d2, d3, d4, d5, d6);
                } else {
                    return this.SetValue(value, d0, d1, d2, d3, d4, d5, d6, allowExpand: true); // SetValue() handles Expand also
                }
            } else {
                // numpy mode - check for value = null was done in SetValue.Run().
                if (object.Equals(value, null)) {
                    throw new ArgumentNullException($"'null' is not a valid right side value in assignment expressions in '{nameof(ArrayStyles.numpy)}' mode. Did you mean to use removals with '{nameof(ArrayStyles.ILNumericsV4)}' array mode? See: {nameof(Settings.ArrayStyle)}.");
                } else if (m_size.NumberOfDimensions <= 7) {
                    return this.SetValue(value, d0, d1, d2, d3, d4, d5, d6, allowExpand: false);
                } else {
                    return this.SetRange_np(value, d0, d1, d2, d3, d4, d5, d6);
                }
            }

        }

        #endregion

        #region helper
        /// <summary>
        /// like Assign, take strides but keep current size
        /// </summary>
        /// <param name="value">right side of assignment</param>
        internal unsafe StorageT SetFullOptim(StorageT value) {

            // hands on approach for memory management: 
            // If value has refcount = 0 we reuse it. Otherwise, we create a clone.

            StorageT ret = value.ReferenceCount == 0 ? value : Create(value.Handles, value.S); 
            //m_handles.Release();
            //m_handles = value.m_handles; // no race cond.: we assume that value is kept alive from the array reference, this storage stems from (and which must still be around). 
            //m_handles.Retain();
            
            // Edit: Size.Set() is not well suited here! It leaves the size in an inconsistent state, expecting the caller to finish its configuration at some point. 
            //ret.S.Set(Size, setLengths: true, setStrides: false);

            long* mybsd = Size.GetBSD(false);
            long* retBsd = ret.Size.GetBSD(true);
            var ndims = Size.NumberOfDimensions;
            retBsd[0] = ndims;
            retBsd[1] = value.S.NumberOfElements;
            retBsd[2] = value.S.BaseOffset;

            for (uint i = 0; i < ndims; i++) {
                retBsd[3 + i] = mybsd[3 + i];
                retBsd[3 + ndims + i] = value.Size.GetStride(i);
            }
            //Assign(ret, true, true); 

            return ret; 
        }

        #endregion
    }
}
