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
using ILNumerics.Core.DeviceManagement;
using ILNumerics.Core.MemoryLayer;
using System;
using System.Security;

namespace ILNumerics.Core.StorageLayer {

    /// <summary>
    /// Internal storage container for logical arrays. This class is used internally. 
    /// </summary>
    public partial class LogicalStorage

        : BaseStorage<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>, IStorage {

        long m_numberTrues;

        /// <summary>
        /// Gets or sets the number of boolean elements stored by this logical array, evaluating to 'true'.
        /// </summary>
        public long NumberTrues {
           get {
                if (m_numberTrues < 0) {
                    countTrues();
                }
                return m_numberTrues;
            }
            internal set { 
                m_numberTrues = value; 
            }
        }
        internal bool IsNumberTruesCached {
            get {
                return m_numberTrues >= 0; 
            }
        }

        internal void SetNumberTrues(long value) {
            m_numberTrues = value;
        }

        public LogicalStorage() : base() {
            var synch = new object(); 
            m_array = new Logical(this, synch);
            m_inArray = new InLogical(this);
            m_outArray = new OutLogical(this, synch);
            m_retArray = new Logical(this, synch);
            m_numberTrues = -1;
        }

        /// <summary>
        /// Creates an <i>uninitialized</i> storage which remains in an INCONSISTENT STATE until configured! 
        /// </summary>
        /// <returns>INCONSTISTENT storage - needs configuration of host memory handle and size!</returns>
        internal static LogicalStorage Create() {
            var ret = BaseStorage<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>.Create();
            ret.Handles = CountableArray.Create();
            ret.NumberTrues = -1;
            System.Diagnostics.Debug.Assert(ret.Handles != null);
            System.Diagnostics.Debug.Assert(ret.Handles.ReferenceCount == 1);
            return ret;
        }
        /// <summary>
        /// Create a new memory handle of <paramref name="elementLength"/> elements of the natural element type of this storage. 
        /// </summary>
        /// <param name="elementLength">Number of elements for the new handle.</param>
        /// <param name="clear">[optional] clear the new memory. Default: false.</param>
        /// <returns>The new handle according to the storage's internal element type.</returns>
        /// <seealso cref="LogicalStorage.New(ulong, bool)"/>
        /// <seealso cref="CellStorage.New(ulong, bool)"/>
        protected internal override MemoryHandle New(ulong elementLength, bool clear = false) {
            return DeviceManager.GetDevice(0).New<byte>(elementLength, clear);
        }

        /// <summary>
        /// Creates a shallow copy of this storage. Does not copy the handles but only the storage object.
        /// </summary>
        /// <returns>Clone of this storage, sharing the memory handles with this storage.</returns>
        /// <remarks><para>The storage returned has an array reference count of 1. Reference
        /// counts for the (shared) handles are increased accordingly.</para></remarks>
        public override IStorage Clone() {
            var ret = base.Clone() as LogicalStorage;
            ret.NumberTrues = NumberTrues;
            return ret; 
        }

        
        internal unsafe static long MarshalConvert2Bool(Array A, MemoryHandle handle, Size outSize) {

            System.Diagnostics.Debug.Assert(A != null);

            GetSizeFromSystemArray(A, outSize);

            var aType = A.GetType().GetElementType();
            long ret = 0;
            // bool is 4 on CLR, 1 on unmanaged memory
            byte* hP = (byte*)handle.Pointer;
            // we cannot use IConvertible for all types, since Convert.ToBoolean does not recognize NaNs! 
            if (aType.Name == "Double") {
                foreach (double item in A) {
                    var val = (!double.IsNaN(item) && item != 0) ? (byte)1 : (byte)0;
                    *(hP++) = val;
                    if (val != 0) ret += 1;
                }
            } else if (aType.Name == "Single") {
                foreach (float item in A) {
                    var val = (!float.IsNaN(item) && item != 0) ? (byte)1 : (byte)0;
                    *(hP++) = val;
                    if (val != 0) ret += 1;
                }
            } else if (aType.Name == "ILNumerics.complex") {
                foreach (complex item in A) {
                    var val = (!complex.IsNaN(item) && item != 0) ? (byte)1 : (byte)0;
                    *(hP++) = val;
                    if (val != 0) ret += 1;
                }
            } else if (aType.Name == "ILNumerics.fomplex") {
                foreach (fcomplex item in A) {
                    var val = (!fcomplex.IsNaN(item) && item != 0) ? (byte)1 : (byte)0;
                    *(hP++) = val;
                    if (val != 0) ret += 1;
                }
            } else if (aType.GetInterface("IConvertible") != null) {
                foreach (object item in A) {
                    var val = (Convert.ToBoolean(item)) ? (byte)1 : (byte)0;
                    *(hP++) = val;
                    if (val != 0) ret += 1;
                }
            } else if (!aType.IsValueType) {
                // reference types are checked for null only
                foreach (object item in A) {
                    var val = (item != null) ? (byte)1 : (byte)0;
                    *(hP++) = val;
                    if (val != 0) ret += 1;
                }
            } else {
                throw new InvalidCastException($"Unable to convert System.Array of element type {aType.Name} to Logical array. Please use IConvertible elements for A!");
            }
            return ret;
        }

        
        private unsafe void countTrues() {
            long trues = 0;
            byte* p = (byte*)Handles[0].Pointer;
            if (Size.IsContinuous) {
                p += Size.BaseOffset; 
                long n = Size.NumberOfElements;
                while (n-- > 0) {
                    if (*p++ != 0) {
                        trues++;
                    }
                }
            } else {
                foreach (var i in Size.Iterator()) {
                    if (p[i] != 0) {
                        trues++;
                    }
                }
            }
            m_numberTrues = trues;
        }

        /// <summary>
        /// Sets a single value at a sequential position in this logical storage and updates <see cref="NumberTrues"/> efficiently. 
        /// </summary>
        /// <param name="value">The new element value.</param>
        /// <param name="byteIdx">The byte position for the value to set.</param>
        /// <returns></returns>
        
        internal unsafe override void SetValueSeq(bool value, long byteIdx) {
            if (Handles.ReferenceCount > 1) {
                var baseOffs = Size.BaseOffset;
                DetachBufferSetInplace();
                byteIdx -= baseOffs;
            }

            var p = (byte*)m_handles[0].Pointer + byteIdx;
            // base.SetValueSeq(bval, byteIdx);
            byte newVal = (byte)(value ? 1 : 0); 
            m_numberTrues = m_numberTrues - *p + newVal;
            *p = newVal;
        }
        
        internal unsafe override LogicalStorage SetValueSeq_OOP(bool value, long byteIdx) {

            var storage = this; 
            if (storage.Handles.ReferenceCount > 1) {
                var baseOffs = storage.Size.BaseOffset;
                var newStorage = storage.GetDetached(0) as LogicalStorage;
                if (!object.ReferenceEquals(storage, newStorage)) {
                    // adjust the target address for new value. BaseOffset was reset' by DetachBufferset().
                    byteIdx -= baseOffs;
                    newStorage.SetValueSeq(value, byteIdx);
                    storage = newStorage; 
                }
            }

            storage.SetValueSeq(value, byteIdx);
            return storage; 
        }
        #region SetRange common entry point for all array/storage types and all array styles
        internal override LogicalStorage SetRange(LogicalStorage value, DimSpec d0) {
            var ret = base.SetRange(value, d0);
            ret.m_numberTrues = -1;
            return ret;
        }
        internal override LogicalStorage SetRange(LogicalStorage value, DimSpec d0, DimSpec d1) {
            var ret = base.SetRange(value, d0, d1);
            ret.m_numberTrues = -1;
            return ret;
        }
        internal override LogicalStorage SetRange(LogicalStorage value, DimSpec d0, DimSpec d1, DimSpec d2) {
            var ret = base.SetRange(value, d0, d1, d2);
            ret.m_numberTrues = -1;
            return ret;
        }
        internal override LogicalStorage SetRange(LogicalStorage value, DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3) {
            var ret = base.SetRange(value, d0, d1, d2, d3);
            ret.m_numberTrues = -1;
            return ret;
        }
        internal override LogicalStorage SetRange(LogicalStorage value, DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3, DimSpec d4) {
            var ret = base.SetRange(value, d0, d1, d2, d3, d4);
            ret.m_numberTrues = -1;
            return ret;
        }
        internal override LogicalStorage SetRange(LogicalStorage value, DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3, DimSpec d4, DimSpec d5) {
            var ret = base.SetRange(value, d0, d1, d2, d3, d4, d5);
            ret.m_numberTrues = -1;
            return ret;
        }
        internal override LogicalStorage SetRange(LogicalStorage value, DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3, DimSpec d4, DimSpec d5, DimSpec d6) {
            var ret = base.SetRange(value, d0, d1, d2, d3, d4, d5, d6);
            ret.m_numberTrues = -1;
            return ret; 
        }
        internal override LogicalStorage SetRange(LogicalStorage value, DimSpec[] dims) {
            var ret = base.SetRange(value, dims);
            ret.m_numberTrues = -1;
            return ret;
        }
        #endregion

        #region SetRange BaseArray interface
        internal override LogicalStorage SetRange(LogicalStorage value, BaseArray d0) {
            var ret = base.SetRange(value, d0);
            ret.m_numberTrues = -1;
            return ret;
        }
        internal override LogicalStorage SetRange(LogicalStorage value, BaseArray d0, BaseArray d1) {
            var ret = base.SetRange(value, d0, d1);
            ret.m_numberTrues = -1;
            return ret;
        }
        internal override LogicalStorage SetRange(LogicalStorage value, BaseArray d0, BaseArray d1, BaseArray d2) {
            var ret = base.SetRange(value, d0, d1, d2);
            ret.m_numberTrues = -1;
            return ret;
        }
        internal override LogicalStorage SetRange(LogicalStorage value, BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3) {
            var ret = base.SetRange(value, d0, d1, d2, d3);
            ret.m_numberTrues = -1;
            return ret;
        }
        internal override LogicalStorage SetRange(LogicalStorage value, BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3, BaseArray d4) {
            var ret = base.SetRange(value, d0, d1, d2, d3, d4);
            ret.m_numberTrues = -1;
            return ret;
        }
        internal override LogicalStorage SetRange(LogicalStorage value, BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3, BaseArray d4, BaseArray d5) {
            var ret = base.SetRange(value, d0, d1, d2, d3, d4, d5);
            ret.m_numberTrues = -1;
            return ret;
        }
        internal override LogicalStorage SetRange(LogicalStorage value, BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3, BaseArray d4, BaseArray d5, BaseArray d6) {
            var ret = base.SetRange(value, d0, d1, d2, d3, d4, d5, d6);
            ret.m_numberTrues = -1;
            return ret;
        }
        internal override LogicalStorage SetRange(LogicalStorage value, BaseArray[] dims) {
            var ret = base.SetRange(value, dims);
            ret.m_numberTrues = -1;
            return ret; 
        }
        #endregion

        /// <summary>
        /// Sets a new value at the element as defined by <paramref name="indices"/>.
        /// </summary>
        /// <param name="value">The new value to be set directly. No clone will be made!</param>
        /// <param name="indices">Vector of the full index path, from root cell to the target element.</param>
        /// <param name="start">First index in <paramref name="indices"/> to be considered.</param>
        /// <param name="allowExpand">Flag indicating which expansion mode to apply (numpy=false, ILNumericsV4=true).</param>
        
        unsafe IStorage IStorage.SetCellContentDirect(BaseArray value, Span<long> indices, uint start, bool allowExpand) {

            if (start >= indices.Length || start < 0) {  // should not happen ?!?
                throw new ArgumentException($"The start index {start} for deep cell position {ILNumerics.Core.Global.Helper.dims2string(AsPointer(indices), (uint)indices.Length)} is out of range.");
            } else {
                // pointing at a value in this array: wrapping it into self typed Storage<T>
                var val = value;
                if (Equals(val, null)) {
                    throw new ArgumentException($"Cannot assign value of type '{value?.GetElementType().Name}' to the element at {ILNumerics.Core.Global.Helper.dims2string(AsPointer(indices), (uint)indices.Length)}. Expected was: <bool>.");
                } else if (val.S.NumberOfElements != 1) {
                    throw new ArgumentException($"Replacing an <bool> element via deep indexing requires a new value as _scalar_ logical array. Found: {val.S.ToString()}.");
                }

                return this.SetValue((bool)val.GetItem(0), AsPointer(indices, (int)start), (uint)(indices.Length - start), allowExpand);
            }
        }


    }
}
