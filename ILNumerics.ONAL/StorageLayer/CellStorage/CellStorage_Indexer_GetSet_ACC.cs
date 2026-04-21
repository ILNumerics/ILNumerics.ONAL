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
using ILNumerics.Core.DeviceManagement;
using ILNumerics.Core.MemoryLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.StorageLayer {

    /// <summary>
    /// Internal class, storage container for generic, partially typed ILNumerics arrays. This class is used internally. 
    /// </summary>
    public partial class CellStorage : BaseStorage<BaseArray, Cell, InCell, OutCell, Cell, CellStorage>, IStorage {

        #region MutableIndexers_Set_long_ACC overrides
        internal override CellStorage MutableIndexer_Set(CellStorage value, long d0) {

            if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                if (object.Equals(value, null) || value.Size.NumberOfElements == 0) {
                    return this.SetRange_ML(null, d0);
                } else {
                    return this.SetValue(value, d0, allowExpand: true); // SetValue() handles Expand also
                }
            } else {
                // numpy mode - check for value = null was also done in SetValue.Run().
                if (object.Equals(value, null)) {
                    throw new ArgumentNullException($"'null' is not a valid right side value in assignment expressions in '{nameof(Settings)}.{nameof(Settings.ArrayStyle)}.{nameof(ArrayStyles.numpy)}' mode. Did you mean to use removals with '{nameof(ArrayStyles.ILNumericsV4)}' array mode? See: {nameof(Settings.ArrayStyle)}.");
                } else if (Size.NumberOfDimensions <= 1) {
                    return this.SetValue(value, d0, allowExpand: false);
                } else {
                    return this.SetRange_np(value, d0);
                }
            }

        }
        internal override CellStorage MutableIndexer_Set(CellStorage value, long d0, long d1) {

            if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                if (object.Equals(value, null) || value.Size.NumberOfElements == 0) {
                    return this.SetRange_ML(null, d0, d1);
                } else {
                    return this.SetValue(value, d0, d1, allowExpand: true); // SetValue() handles Expand also
                }
            } else {
                // numpy mode - check for value = null was done in SetValue.Run().
                if (object.Equals(value, null)) {
                    throw new ArgumentNullException($"'null' is not a valid right side value in assignment expressions in '{nameof(ArrayStyles.numpy)}' mode. Did you mean to use removals with '{nameof(ArrayStyles.ILNumericsV4)}' array mode? See: {nameof(Settings.ArrayStyle)}.");
                } else if (Size.NumberOfDimensions <= 2) {
                    return this.SetValue(value, d0, d1, allowExpand: false);
                } else {
                    return this.SetRange_np(value, d0, d1);
                }
            }

        }
        internal override CellStorage MutableIndexer_Set(CellStorage value, long d0, long d1, long d2) {

            if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                if (object.Equals(value, null) || value.Size.NumberOfElements == 0) {
                    return this.SetRange_ML(null, d0, d1, d2);
                } else {
                    return this.SetValue(value, d0, d1, d2, allowExpand: true); // SetValue() handles Expand also
                }
            } else {
                // numpy mode - check for value = null was done in SetValue.Run().
                if (object.Equals(value, null)) {
                    throw new ArgumentNullException($"'null' is not a valid right side value in assignment expressions in '{nameof(ArrayStyles.numpy)}' mode. Did you mean to use removals with '{nameof(ArrayStyles.ILNumericsV4)}' array mode? See: {nameof(Settings.ArrayStyle)}.");
                } else if (Size.NumberOfDimensions <= 3) {
                    return this.SetValue(value, d0, d1, d2, allowExpand: false);
                } else {
                    return this.SetRange_np(value, d0, d1, d2);
                }
            }

        }
        internal override CellStorage MutableIndexer_Set(CellStorage value, long d0, long d1, long d2, long d3) {

            if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                if (object.Equals(value, null) || value.Size.NumberOfElements == 0) {
                    return this.SetRange_ML(null, d0, d1, d2, d3);
                } else {
                    return this.SetValue(value, d0, d1, d2, d3, allowExpand: true); // SetValue() handles Expand also
                }
            } else {
                // numpy mode - check for value = null was done in SetValue.Run().
                if (object.Equals(value, null)) {
                    throw new ArgumentNullException($"'null' is not a valid right side value in assignment expressions in '{nameof(ArrayStyles.numpy)}' mode. Did you mean to use removals with '{nameof(ArrayStyles.ILNumericsV4)}' array mode? See: {nameof(Settings.ArrayStyle)}.");
                } else if (Size.NumberOfDimensions <= 4) {
                    return this.SetValue(value, d0, d1, d2, d3, allowExpand: false);
                } else {
                    return this.SetRange_np(value, d0, d1, d2, d3);
                }
            }

        }
        internal override CellStorage MutableIndexer_Set(CellStorage value, long d0, long d1, long d2, long d3, long d4) {

            if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                if (object.Equals(value, null) || value.Size.NumberOfElements == 0) {
                    return this.SetRange_ML(null, d0, d1, d2, d3, d4);
                } else {
                    return this.SetValue(value, d0, d1, d2, d3, d4, allowExpand: true); // SetValue() handles Expand also
                }
            } else {
                // numpy mode - check for value = null was done in SetValue.Run().
                if (object.Equals(value, null)) {
                    throw new ArgumentNullException($"'null' is not a valid right side value in assignment expressions in '{nameof(ArrayStyles.numpy)}' mode. Did you mean to use removals with '{nameof(ArrayStyles.ILNumericsV4)}' array mode? See: {nameof(Settings.ArrayStyle)}.");
                } else if (Size.NumberOfDimensions <= 5) {
                    return this.SetValue(value, d0, d1, d2, d3, d4, allowExpand: false);
                } else {
                    return this.SetRange_np(value, d0, d1, d2, d3, d4);
                }
            }

        }
        internal override CellStorage MutableIndexer_Set(CellStorage value, long d0, long d1, long d2, long d3, long d4, long d5) {

            if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                if (object.Equals(value, null) || value.Size.NumberOfElements == 0) {
                    return this.SetRange_ML(null, d0, d1, d2, d3, d4, d5);
                } else {
                    return this.SetValue(value, d0, d1, d2, d3, d4, d5, allowExpand: true); // SetValue() handles Expand also
                }
            } else {
                // numpy mode - check for value = null was done in SetValue.Run().
                if (object.Equals(value, null)) {
                    throw new ArgumentNullException($"'null' is not a valid right side value in assignment expressions in '{nameof(ArrayStyles.numpy)}' mode. Did you mean to use removals with '{nameof(ArrayStyles.ILNumericsV4)}' array mode? See: {nameof(Settings.ArrayStyle)}.");
                } else if (Size.NumberOfDimensions <= 6) {
                    return this.SetValue(value, d0, d1, d2, d3, d4, d5, allowExpand: false);
                } else {
                    return this.SetRange_np(value, d0, d1, d2, d3, d4, d5);
                }
            }

        }
        internal override CellStorage MutableIndexer_Set(CellStorage value, long d0, long d1, long d2, long d3, long d4, long d5, long d6) {

            if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                if (object.Equals(value, null) || value.Size.NumberOfElements == 0) {
                    return this.SetRange_ML(null, d0, d1, d2, d3, d4, d5, d6);
                } else {
                    return this.SetValue(value, d0, d1, d2, d3, d4, d5, d6, allowExpand: true); // SetValue() handles Expand also
                }
            } else {
                // numpy mode - check for value = null was done in SetValue.Run().
                if (object.Equals(value, null)) {
                    throw new ArgumentNullException($"'null' is not a valid right side value in assignment expressions in '{nameof(ArrayStyles.numpy)}' mode. Did you mean to use removals with '{nameof(ArrayStyles.ILNumericsV4)}' array mode? See: {nameof(Settings.ArrayStyle)}.");
                } else if (Size.NumberOfDimensions <= 7) {
                    return this.SetValue(value, d0, d1, d2, d3, d4, d5, d6, allowExpand: false);
                } else {
                    return this.SetRange_np(value, d0, d1, d2, d3, d4, d5, d6);
                }
            }

        }
        #endregion

    }
}
