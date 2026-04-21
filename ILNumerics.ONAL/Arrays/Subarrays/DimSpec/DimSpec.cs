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
using System;
using System.Collections.Generic;
using System.IO;
using ILNumerics.Core.Misc;
using System.Collections;
using ILNumerics.Core.StorageLayer;
using System.Xml;
using ILNumerics.Core;
using ILNumerics.Core.Global;
using System.Threading;

namespace ILNumerics {

    /// <summary>
    /// Class representing a one-dimensional slice / range or single index along a dimension. Used for subarray indexing.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{ToString(),nq}")]
    [Serializable]
    public abstract class DimSpec : BaseArray, IIndexIterator, IStorage {

        public sealed class Internal : DimSpec, ICacheable<Internal> {

#if DEBUG
            static long counter = 0;
            public readonly long ID;
#endif

            private Internal m_previous;
            //private int m_deletionMark;

            public Internal() {
#if DEBUG
                ID = Interlocked.Increment(ref counter);
#endif
            }

            ref Internal ICacheable<Internal>.Previous { get { return ref m_previous; } }

            //ref int ICacheable<Internal>.DeletionMark => ref m_deletionMark; 
        }

        #region attributes

        protected long m_start;  // 'long' for neg. indices! 
        protected long m_step;
        protected long m_end;
        protected long m_cur;
        protected long m_lastElementIDX;
        protected bool m_isSingleIndex;  // needed because in numpy A[1] is not the same as A[1:2]! Former removes the dim. Latter does not.
        protected bool m_isSlice;        // needed for delayed evaluation of end indices. We tried to -1 from user end immediately, but this causes ambiquities with 0:0 empty slices. Therefore, we check in Evaluate() only. 
        #endregion

        #region properties 
        //internal static DimSpec Cache => s_cache ?? s_root; 

        public bool IsSingleIndex {
            get {
                return m_isSingleIndex;
            }
            internal set {
                m_isSingleIndex = value;
            }
        }

        public bool IsSlice {
            get {
                return m_isSlice;
            }
            internal set {
                m_isSlice = value;
            }
        }
        #endregion

#if !CORE_ESSENTIAL_

        #region constructors
        protected DimSpec() { }

        /// <summary>
        /// Create uninitialized dimension specifier.
        /// </summary>
        /// <returns>Uninitialized dimension specifier.</returns>
        internal static DimSpec Create() {
            var ret = InMemoryCache<Internal>.Retrieve();
            return ret;
        }

        internal static DimSpec Create(long start, long step, long end) {
            var ret = Create();
            ret.m_start = start;
            ret.m_step = step;
            ret.m_end = end;
            ret.m_cur = -1;
            ret.m_isSingleIndex = false;
            ret.m_isSlice = false;
            ret.m_lastElementIDX = -1;
            return ret;
        }
        internal static DimSpec Create(long singleIndex) {
            var ret = Create();
            ret.m_start = singleIndex;
            ret.m_step = 1;
            ret.m_end = singleIndex;
            ret.m_cur = -1;
            ret.m_isSingleIndex = true;
            ret.m_isSlice = false;
            ret.m_lastElementIDX = -1;
            return ret;
        }
        //internal DimSpec(BaseArray start, IConvertible step, BaseArray end) {
        //    using (Scope.Enter(start, end)) {

        //#region parameter checks
        //        if (object.Equals(start, null)) {
        //            throw new ArgumentNullException(nameof(start));
        //        }
        //        if (object.Equals(end, null)) {
        //            throw new ArgumentNullException(nameof(end));
        //        }
        //        if (!start.IsScalar || !end.IsScalar) {
        //            throw new ArgumentException("Input arguments for a dimension specifier must be scalars.");
        //        }
        //        if (start.IsNumeric) {
        //            m_start = ILMathInternal.touint32(start).GetValue(0);
        //        } else if (start.IsOfType<ILExpression>()) {
        //            m_startExpr = start.AsArray<ILExpression>().GetValue(0);
        //        } else {
        //            throw new ArgumentException($"The argument '{nameof(start)}' is of an unsupported element type. Expected: numeric or expression involving 'end'.");
        //        }
        //        if (end.IsNumeric) {
        //            m_end = ILMathInternal.touint32(end).GetValue(0);
        //        } else if (end.IsOfType<ILExpression>()) {
        //            m_endExpr = end.AsArray<ILExpression>().GetValue(0);
        //        } else {
        //            throw new ArgumentException($"The argument '{nameof(end)}' is of an unsupported element type. Expected: numeric or expression involving 'end'.");
        //        }
        //        m_step = 1; 
        //#endregion
        //        m_isEvaluated = false;
        //        m_isFull = false;
        //    }
        //}
        //internal DimSpec(BaseArray start, BaseArray step, BaseArray end) {
        //    using (Scope.Enter(start, step, end)) {

        //#region parameter checks
        //        if (object.Equals(start, null)) {
        //            throw new ArgumentNullException(nameof(start));
        //        }
        //        if (object.Equals(step, null)) {
        //            throw new ArgumentNullException(nameof(step));
        //        }
        //        if (object.Equals(end, null)) {
        //            throw new ArgumentNullException(nameof(end));
        //        }
        //        if (!start.IsScalar || !step.IsScalar || !end.IsScalar) {
        //            throw new ArgumentException("Input arguments for a dimension specifier must be scalars.");
        //        }
        //        if (start.IsNumeric) {
        //            m_start = ILMathInternal.touint32(start).GetValue(0);
        //        } else if (start.IsOfType<ILExpression>()) {
        //            m_startExpr = start.AsArray<ILExpression>().GetValue(0);
        //        } else {
        //            throw new ArgumentException($"The argument '{nameof(start)}' is of an unsupported element type. Expected: numeric or expression involving 'end'.");
        //        }
        //        if (end.IsNumeric) {
        //            m_end = ILMathInternal.touint32(end).GetValue(0);
        //        } else if (end.IsOfType<ILExpression>()) {
        //            m_endExpr = end.AsArray<ILExpression>().GetValue(0);
        //        } else {
        //            throw new ArgumentException($"The argument '{nameof(end)}' is of an unsupported element type. Expected: numeric or expression involving 'end'.");
        //        }
        //        if (step.IsNumeric) {
        //            m_step = ILMathInternal.toint32(step).GetValue(0);
        //        } else {
        //            throw new ArgumentException($"The argument '{nameof(step)}' is of an unsupported element type. Expected: numeric scalar.");
        //        }
        //#endregion
        //        m_isEvaluated = false;
        //        m_isFull = false;
        //    }
        //}

        #endregion

        #region properties 
        public long Start {
            get { return m_start; }
            internal set { m_start = value; }
        }
        public long Step {
            get { return m_step; }
            internal set { m_step = value; }
        }
        public long End {
            get { return m_end; }
            internal set { m_end = value; }
        }
        /// <summary>
        /// Number of elements referenced by this dimension specifier.
        /// </summary>
        public override long Length {
            get {
                return (long)Math.Max(((m_end - m_start) / (double)m_step + 1), 0);
            }
        }
        #endregion

        #region public interface

        internal virtual void Evaluate(long lastElementIdx) {
            if (lastElementIdx < 0) { // empty arrays
                // TODO: this should work! Indexing into any dimension should work if the index is an empty slice... (leaving it for now, though)
                throw new IndexOutOfRangeException($"Attempt to index into an empty dimension failed. Use 'full' instead!");
            }

            if (m_start < 0) {
                m_start = lastElementIdx + 1 + m_start;
            }
            if (m_end < 0) {
                m_end = lastElementIdx + 1 + m_end;
            }
            if ((ulong)m_start > (ulong)lastElementIdx) {
                throw new IndexOutOfRangeException($"Start index out of range: 0 <= start <= end <= {lastElementIdx}. Found: {m_start}.");
            }
            // for slices: end index may lays outside of the dimensions range. Additional check is not performed on the main execution line, though.
            if ((ulong)m_end > (ulong)lastElementIdx && (m_end - m_step > lastElementIdx || !m_isSlice)) {
                throw new IndexOutOfRangeException($"End index out of range: 0 <= start <= end <= {lastElementIdx}. Found: {m_end}.");
            }
            System.Diagnostics.Debug.Assert(lastElementIdx <= long.MaxValue); // ensured in storage creation and by the universe
            if (m_isSlice) {
                // in case the user gave: slice(0,0) -> empty intended and we accept the wrapping around here.
                m_end -= 1;  // realizes the numpy behavior to exclude the end index
            }
            if (m_step <= 0) { // disabling slices, empty ranges:  || m_start > m_end) {
                throw new NotSupportedException("Step size must be positive! Make sure that start index <= end index and step > 0.");
            }
            m_cur = m_start - m_step;
            m_lastElementIDX = lastElementIdx;
        }
        internal virtual void EvaluateLeft(long lastElementIdx, ref bool expand) {
            if (m_start < 0) {
                m_start = lastElementIdx + 1 + m_start;
            }
            if (m_end < 0) {
                m_end = lastElementIdx + 1 + m_end;
            }
            if (m_isSlice) { // in case the user gave: slice(0,0) -> empty intended. but no wrapping around!
                m_end -= 1;  // realizes the numpy behavior to exclude the end index
            }
            if (m_start > lastElementIdx || m_end > lastElementIdx) {
                expand = true;
            }
            if (m_start < 0 && m_start < lastElementIdx) { // special case: empty arrays!
                throw new IndexOutOfRangeException($"Start index out of range: 0 <= start <= end <= {lastElementIdx}. Found: {m_start}.");
            }
            if (m_end < 0 && m_end < lastElementIdx) { // special case: empty arrays!
                throw new IndexOutOfRangeException($"End index out of range: 0 <= start <= end <= {lastElementIdx}. Found: {m_end}.");
            }
            System.Diagnostics.Debug.Assert(lastElementIdx <= long.MaxValue); // ensured in storage creation and by the universe
            if (m_step <= 0) { // disabling slices, empty ranges: || m_start > m_end) {
                throw new NotSupportedException("Step size must be positive! Make sure that start index <= end index and step > 0.");
            }
            m_cur = m_start - m_step;
            m_lastElementIDX = lastElementIdx;
        }
        #endregion

        #region implicit conversions

        /// <summary>
        /// Implicit cast from single index to dimension specifier. 
        /// </summary>
        /// <param name="a">Single index.</param>
        public static implicit operator DimSpec(uint a) {
            return Create(a);
        }
        /// <summary>
        /// Implicit cast from single index to dimension specifier. 
        /// </summary>
        /// <param name="a">Single index.</param>
        public static implicit operator DimSpec(long a) {
            return Create(a);
        }
        public static implicit operator DimSpec(ILExpression exp) {
            var ret = ExpressionDimSpec.Create();
            ret.m_startExpression = exp;
            ret.Step = 1;
            ret.m_endExpression = exp;
            ret.m_isSingleIndex = true;
            ret.m_isSlice = false;
            return ret;
        }
        #endregion
#endif

        #region dummy BaseArray implementation (not used with very few exceptions)

        ///// <summary>
        ///// Flag indicating if this array instance is a volatile array (true for RetXXX array types). 
        ///// </summary>
        //internal override bool IsVolatile {
        //    get { return false; }
        //}
        internal override IStorage GetIStorage() {
            return null; 
        }

        public override bool IsColumnVector {
            get {
                throw new NotImplementedException();
            }
        }

        public override bool IsComplex {
            get {
                throw new NotImplementedException();
            }
        }
        public override bool IsCell => false;

        [Obsolete("This method has been removed in favor of reusing / low-allocation processing.")]
        public override bool IsDisposed {
            get {
                throw new NotImplementedException();
            }
        }

        public override bool IsEmpty {
            get {
                throw new NotImplementedException();
            }
        }

        public override bool IsMatrix {
            get {
                throw new NotImplementedException();
            }
        }

        public override bool IsNumeric {
            get {
                throw new NotImplementedException();
            }
        }

        public override bool IsRowVector {
            get {
                throw new NotImplementedException();
            }
        }

        public override bool IsScalar {
            get {
                throw new NotImplementedException();
            }
        }

        public override bool IsVector {
            get {
                throw new NotImplementedException();
            }
        }

        //public override uint Length {
        //    get {
        //        if (!m_isEvaluated) {
        //            throw new InvalidOperationException("You need to call Evaluate() on this object first!"); 
        //        }
        //        return (uint)Math.Abs(m_end - m_start) + 1;
        //    }
        //}

        public override Size S {
            get {
                throw new NotImplementedException();
            }
        }

        public override Size Size {
            get {
                throw new NotImplementedException();
            }
        }

        public override int ReferenceCount {
            get {
                return -1;
            }
        }

        public long Current => m_cur;

        object IEnumerator.Current => m_cur;

        public bool IsReady => throw new NotImplementedException();

        int IStorage.ID => throw new NotImplementedException();


        public int Version => throw new NotImplementedException();

        public override string ToString() {
            return String.Format("[{0}:{1}:{2}]",m_start,m_step,m_end);
        }

        public override bool IsOfType<T>() {
            return typeof(T) == typeof(DimSpec); 
        }

        public override bool Equals(object A) {
            throw new NotImplementedException();
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }

        public override Type GetElementType() {
            throw new NotImplementedException();
        }
        public override string Info() {
            throw new NotImplementedException();
        }
        public override string ShortInfo() {
            throw new NotImplementedException();
        }

        [Obsolete]
        public override void ToStream(Stream outStream, string format, ArrayStreamSerializationFlags method) {
            throw new NotImplementedException();
        }

        //public override string ToString(int maxLength) {
        //    throw new NotImplementedException();
        //}

        //public override void Retain() {
        //    throw new NotImplementedException();
        //}

        //public override void Release() {
        //    throw new NotImplementedException();
        //}

        public override object GetItem(long i) {
            throw new NotImplementedException();
        }

        //public override string ToString(uint maxRows = uint.MaxValue, uint maxRowLength = 500, bool includeType = true, bool includeSize = true) {
        //    throw new NotImplementedException();
        //}

        internal override void Retain() {
            throw new NotImplementedException();
        }

        internal override void Release() {
            throw new NotImplementedException();
        }

        public override void Dispose() {
            InMemoryCache<Internal>.Store(this as Internal);
        }
#endregion

#region IndexIterator interface implementation
        public long GetLength() {
            return (long)this.Length;
        }

        public long? GetMaximum() {
            return GetLength() > 0 ? (Nullable<long>)m_end : null;
        }

        public long? GetMinimum() {
            return GetLength() > 0 ? (Nullable<long>)m_start : null; ; 
        }

        public long GetLastDimensionIndex() => m_lastElementIDX; 
        /// <summary>
        /// Gives the stepsize configured for this dimension specifier.
        /// </summary>
        /// <returns>step size.</returns>
        public long? GetStepSize() => m_step;
        
        /// <summary>
        /// Gives an <see cref="IEnumerator{T}"/> capable of iterating over the index range specified by this object.
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerator<long> GetEnumerator() => this;

        IEnumerator IEnumerable.GetEnumerator() => this;
        /// <summary>
        /// Increments the internal position counter to the next index.
        /// </summary>
        /// <returns></returns>
        public bool MoveNext() {
            if (m_cur + m_step <= m_end) {
                m_cur += m_step;
                return true; 
            }
            return false; 
        }
        /// <summary>
        /// Places the internal position counter back before the first element.
        /// </summary>
        public void Reset() {
            m_cur = m_start - m_step; 
        }
        public override string ShortInfo(bool includeType = true, bool includeSize = true, bool includeValues = true, bool includeStorageOrder = true, bool includeDevice = false) {
            throw new NotImplementedException();
        }

#endregion


        internal override IStorage GetClonedStorage(bool forceRelease = false) {
            throw new NotImplementedException("DimSpec ranges & slices are currently not supported as cell content. Use numeric index vectors or string syntax, utilizing ','. Ex.: \":,:,0:2:end\""); 
            //return (this as IStorage).Clone();  
        }
        internal override BaseArray GetClonedArray() {
            return this; 
        }

        string IStorage.ShortInfo() {
            throw new NotImplementedException();
        }

        string IStorage.ToString() {
            return String.Format("[{0}:{1}:{2}]", m_start, m_step, m_end);
        }

        BaseArray IStorage.GetBaseArrayClone() {
            throw new NotImplementedException();
        }

        BaseArray IStorage.GetBaseArray() {
            throw new NotImplementedException();
        }

        unsafe IStorage IStorage.GetCellContentDirect(long* indices, uint lenIndices, uint start) {
            throw new NotImplementedException();
        }

        unsafe IStorage IStorage.SetCellContentDirect(BaseArray value, Span<long> indices, uint start, bool allowExpand) {
            throw new NotImplementedException();
        }

        void IStorage.Retain() {
            throw new NotImplementedException();
        }

        void IStorage.Release() {
            throw new NotImplementedException();
        }

        IStorage IStorage.Clone() {
            throw new NotImplementedException();
        }

        public override void ToXML(XmlWriter writer) {
            throw new NotImplementedException();
        }

        CountableArray IStorage.GetHandlesInternal() {
            throw new NotImplementedException();
        }

        Size IStorage.GetSizeInternal() {
            throw new NotImplementedException();
        }

        public int GetElementTypeLength() {
            throw new NotImplementedException();
        }

        public void SetHandlesInternal(CountableArray handles) {
            throw new NotImplementedException();
        }

        public int GetReferenceCount() {
            throw new NotImplementedException();
        }

        public int GetAsynchReferencesCount() {
            throw new NotImplementedException();
        }

        public void Detach(uint targetDeviceID) {
            throw new NotImplementedException();
        }

        public string ShortInfo(bool includeType = true, bool includeSize = true, bool includeValues = true, bool includeStorageOrder = true, bool includeDevices = true, bool includeIDs = false, bool includeCounters = false, bool ignoreLocks = false) {
            return GetType().FullName + " " + m_start + ":" + m_step + ":" + m_end; 
        }

        void IStorage.SetHandlesInternal(CountableArray handles) {
            throw new NotImplementedException();
        }

        int IStorage.GetElementTypeLength() {
            throw new NotImplementedException();
        }

        int IStorage.GetReferenceCount() {
            throw new NotImplementedException();
        }

        IStorage IStorage.GetDetached(uint targetDeviceID) {
            throw new NotImplementedException();
        }

        public bool DetachBufferSetInplace(uint deviceIdx) {
            throw new NotImplementedException();
        }

        void IStorage.LeaveScope(Scope.ScopeInfo scope) {
            throw new NotImplementedException();
        }

        void IStorage.EnterScope(Scope.ScopeInfo scope) {
            throw new NotImplementedException();
        }

    }

}
