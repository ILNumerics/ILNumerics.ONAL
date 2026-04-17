//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////
using ILNumerics.Core.MemoryLayer;
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;

namespace ILNumerics {

    /// <summary>
    /// Size - dimensions for array objects. This object is immutable.
    /// </summary>
    /// <remarks><para>The class manages /stores all dimensions information of ILNumerics arrays. </para>
    /// <para>Up from version 5.0 every storage holds a permanent instance of one single Size object. 
    /// Size objects get pooled with the array, reused with the array's storage and become modified 
    /// when the storage is reassigned a new data array to. </para>
    /// <para>Size uses the full Buffer Size Descriptor format as is used in the 
    /// Accelerator API. Since version 5.0 arrays in ILNumerics CE can have any 
    /// storage format! Elements can be storage in column major, row major or any other order which is 
    /// representable with <i>positive strides</i>. Commonly, array elements address a continous area 
    /// of elements stored in the host array.</para>
    /// <para>The base offset - the starting index in the host array of the first element - is stored 
    /// in the BSD long[2] element.</para></remarks>
    [Serializable]
    [System.Diagnostics.DebuggerDisplay("{ToString(),nq}")]
    public unsafe partial class Size {

        /// <summary>
        /// Number of elements for a BSD utilizing the maximum number of dimensions supported: 17. 
        /// </summary>
        private const int s_maxBSDLength = MaxNumberOfDimensions * 2 + 3; // 17 => 7 dims * 2 + 3

        /// <summary>
        /// The value / bit No# 32 storing the property: continous storage layout in the <see cref="m_flags"/> variable. 
        /// </summary>
        public const uint CONT_FLAG = (1u << 31);
        /// <summary>
        /// Internal buffer size descriptor (BSD).
        /// </summary>
        /// <remarks>This size implementation relies on the format of Buffer Size Descriptors (BSD)
        /// of the ILNumerics Accelerator project. It wraps the BSD storage (an long[] array) for 
        /// the common, (mostly) row major storages of Array &amp; Co. This attribute represents the
        /// actual BSD storage array.</remarks>
        private readonly long* m_descriptor;

        private readonly NativeHostHandle m_handle;

        /// <summary>
        /// Flag indicating storage order, continuous storage property, ... ? of this storage. For lazy evaluation. 
        /// </summary>
        private uint m_flags;

        /// <summary>
        /// The maximum number of dimensions a size descriptor is able to handle. Currently, from version 5.2: 32. 
        /// </summary>
        public const int MaxNumberOfDimensions = 32;

        /// <summary>
        /// The maximum number of entries for a size descriptor. 
        /// </summary>
        public const int BSDLength = 3 + 2 * MaxNumberOfDimensions; // 17 buckets in m_descriptor


        ///// <summary>
        ///// A size descriptor of size 0x0 (empty).
        ///// </summary>
        //public static readonly Size Empty00 = new Size(0, 0);
        ///// <summary>
        ///// A size descriptor of size 1x1 (scalar).
        ///// </summary>
        //public static readonly Size Scalar1_1 = new Size(1, 1);
        ///// <summary>
        ///// A size descriptor of size 2x1.
        ///// </summary>
        //public static Size Column2_1 = new Size(2, 1);
        ///// <summary>
        ///// A size descriptor of size 3x1.
        ///// </summary>
        //public static Size Column3_1 = new Size(3, 1);
        ///// <summary>
        ///// A size descriptor of size 1x3.
        ///// </summary>
        //public static Size Row1_2 = new Size(1, 2);
        ///// <summary>
        ///// A size descriptor of size 1x3.
        ///// </summary>
        //public static Size Row1_3 = new Size(1, 3);

        #region properties
        /// <summary>
        /// Gets the current value of the flags describing this size. This may or may not be up to-date! 
        /// </summary>
        internal uint Flags {
            get {
                return m_flags;
            }
            set {
                m_flags = value;
            }
        }


        /// <summary>
        /// Gets the number of dimensions referenced by this size descriptor.
        /// </summary>
        public uint NumberOfDimensions {
            get {
                return *(uint*)m_descriptor;
            }
        }

        /// <summary>
        /// Gets the overall number of elements managed by this size descriptor. 
        /// </summary>
        public long NumberOfElements {
            get {
                return m_descriptor[1];
            }
        }

        /// <summary>
        /// Gets the element index of the first element addressed relative to the  
        /// beginning of the underlying buffer storage.
        /// </summary>
        public long BaseOffset {
            get {
                return m_descriptor[2];
            }
            internal set {
                m_descriptor[2] = value;
            }
        }

        /// <summary>
        /// Length of the longest dimension.
        /// </summary>
        public long Longest {
            get {
                if (NumberOfDimensions == 0) {
                    return 1; 
                }
                long ret = 0;
                for (uint i = 0; i < NumberOfDimensions; i++) {
                    var l = m_descriptor[i + 3];
                    if (l > ret) {
                        ret = l;
                    }
                }
                return ret;
            }
        }

        /// <summary>
        /// Number of non singleton dimensions of this size.
        /// </summary>
        /// <remarks><para>Non singleton dimensions are dimensions which length is larger than 1.</para> 
        /// <para>Since version 4.13 empty dimensions (length = 0) are also considered as being non-singleton dimensions.</para></remarks>
        public int NonSingletonDimensions {
            // ToDo: Definition of 'non-singelton' (= not 1) should be reconsidered. Here 0 is now counted as 'non-singleton'
            get {
                int ret = 0;
                for (uint i = 0; i < NumberOfDimensions; i++) {
                    if (this[i] != 1) ret++;
                }
                return ret;
            }
        }
        /// <summary>
        /// The storage order this size descriptor represents. This is lazily evaluated once for any unchanged descriptor.
        /// </summary>
        public StorageOrders StorageOrder {
            get {
                if (m_flags == 0) {
                    CheckFlags(m_descriptor, ref m_flags);
                }
                return (StorageOrders)(m_flags & 63); // 2^8 - 1 => lowest 7 bits set
            }
        }

        internal long GetLastDimIdxForMLSubarray(uint v) {
            long ret;
            uint ndims = NumberOfDimensions;
            if (v < ndims) {
                ret = (long)m_descriptor[3 + v];
                for (uint i = v + 1; i < ndims; i++) {
                    ret *= (long)m_descriptor[3 + i];
                }
                return ret - 1;
            } else {
                return 0;
            }
        }

        /// <summary>
        /// Determines if elements stored according to this size descriptor are layed-out continously in memory.
        /// </summary>
        /// <value><c>True</c> if the elements are continously layed out in memory, <c>false</c> otherwise.</value>
        /// <remarks><para>Arrays with continously layed-out elements offer the potential of speedy copies and iteration. Many 
        /// functions in ILNumerics make use of this property.</para>
        /// <para>Typical cases of continous layout are <see cref="StorageOrders.ColumnMajor"/> and <see cref="StorageOrders.RowMajor"/>. 
        /// However, other storage schemes exist for higher dimensional arrays which do also store elements in memory 'without holes'.
        /// Therefore, <see cref="IsContinuous"/> will always be <see langword="true"/> on storages with a layout of 
        /// <see cref="StorageOrders.ColumnMajor"/> or <see cref="StorageOrders.RowMajor"/></para>. But there is no guarantee that 
        /// the storage is stored in either one of these layouts (row / column major) when <see cref="IsContinuous"/> returns <see langword="true"/>. 
        /// </remarks>
        public bool IsContinuous {
            get {
                if (m_flags == 0) {
                    CheckFlags(m_descriptor, ref m_flags);
                }
                return (m_flags & CONT_FLAG) != 0;
            }
        }

        /// <summary>
        /// Pretty print dimensions in the format "[a,b,c]".
        /// </summary>
        /// <returns>Dimensions as String.</returns>
        
        public override String ToString() {
            if (m_handle != default(NativeHostHandle)) {
                String s = "[";
                var ndims = NumberOfDimensions;
                for (uint t = 0; t < ndims; t++) {
                    long dim = m_descriptor[3 + t];
                    s = s + (dim < uint.MaxValue ? (uint)dim : dim).ToString();
                    if (t < ndims - 1) {
                        s = s + ",";
                    }
                }
                s = s + "]";
                return s;
            } else {
                // used to mark pending sizes
                return "[pending]"; 
            }
        }

        /// <summary>
        /// Gets the lengths of the dimensions of the array associated with this size object. 
        /// </summary>
        /// <returns>The length of dimensions as elements of a vector of length [NumberOfDimensions].</returns>
        /// <remarks>Note that numpy scalar arrays (0 dimensions, 1 element) return an empty vector of length 0.</remarks>
        
        [Obsolete("In order to get the size of an array A as vector of <long> use A.shape instead!")]
        public Array<long> ToIntArray() {
            var ret = Storage<long>.Create();
            ret.S.SetAll(NumberOfDimensions);
            ret.m_handles[0] = Core.DeviceManagement.DeviceManager.GetDevice(0).New<long>((ulong)NumberOfDimensions);
            for (int i = 0; i < NumberOfDimensions; i++) {
                ret.SetValue(this[i], i);                 
            }
            return ret.RetArray; 
        }

        ///// <summary>
        ///// Get length of dimension at index <paramref name="idx"/> (readonly).
        ///// </summary>
        ///// <param name="idx">The index of the dimension to retrieve the length for.</param>
        ///// <returns>Length of dimension specified by idx</returns>
        ///// <remarks><para>For <paramref name="idx"/> corresponding to an existing dimension 
        ///// the length of that dimension is returned. If <paramref name="idx"/> is equal to or larger than 
        ///// the number of dimensions 1 is returned.</para>
        ///// </remarks>
        ///// <seealso cref="this[long]"/>
        //public uint this[uint idx] {
        //    get {
        //        if (idx >= NumberOfDimensions) {
        //            return 1;
        //        }
        //        return (uint)m_descriptor[3 + idx];
        //    }
        //}
        /// <summary>
        /// Get length of dimension at index <paramref name="idx"/>.
        /// </summary>
        /// <param name="idx">The index of the dimension to retrieve the length for.</param>
        /// <returns>Length of dimension specified by idx.</returns>
        /// <remarks><para>For <paramref name="idx"/> corresponding to an existing dimension 
        /// the length of that dimension is returned. If <paramref name="idx"/> is equal to or larger than 
        /// the number of dimensions 1 is returned.</para>
        /// </remarks>
        /// <seealso cref="GetStride(uint)"/>
        public long this[long idx] {
            get {
                if ((ulong)idx >= NumberOfDimensions) {
                    return 1;
                }
                return (long)m_descriptor[3 + idx];
            }
        }
        ///// <summary>
        ///// Get length of dimension (readonly).
        ///// </summary>
        ///// <param name="idx">The index of the dimension to retrieve the length for.</param>
        ///// <returns>Length of dimension specified by idx</returns>
        ///// <exception cref="IndexOutOfRangeException">If <paramref name="idx"/> is negative</exception>
        ///// <remarks><para>For idx corresponding to an existing dimension 
        ///// the length of that dimension is returned. If idx is equal to or larger than 
        ///// the number of dimensions 1 will be returned.</para>
        ///// </remarks>
        //[Obsolete("Use uint indexing instead! For number literals you can ignore this warning. It will go away once the 'int' overload has been removed. For variables change their type to 'uint' instead.")]
        //public uint this[int idx] {
        //    get {
        //        if (idx < 0) {
        //            throw new IndexOutOfRangeException("idx must be positive!");
        //        }
        //        if (idx >= NumberOfDimensions) {
        //            return 1;
        //        }
        //        return (uint)m_descriptor[3 + idx];
        //    }
        //}

        #endregion

        #region constructors

        /// <summary>
        /// Create a new, empty size descriptor.
        /// </summary>
        
        internal Size() {
            m_handle = (NativeHostHandle)Core.DeviceManagement.DeviceManager.GetDevice(0).New<long>(s_maxBSDLength);
            m_descriptor = (long*)m_handle.Pointer;
            m_descriptor[0] = 0;
            m_descriptor[1] = 0;
            m_descriptor[2] = 0;
            m_flags = 0;
        }

        // Edit: following had been used by PendingSize only. Not used anymore. It would create a corrupt instance... 
        //protected Size(bool createHandle) {
        //    if (createHandle) {
        //        m_handle = (NativeHostHandle)Core.DeviceManagement.DeviceManager.GetDevice(0).New<long>(s_maxBSDLength);
        //        m_descriptor = (long*)m_handle.Pointer;
        //        m_descriptor[0] = 0;
        //        m_descriptor[1] = 0;
        //        m_descriptor[2] = 0;
        //    } else {
        //        m_handle = default(NativeHostHandle);
        //        m_descriptor = (long*)0; 
        //    }
        //    m_flags = 0;

        //}

#if OBSOLETE

        /// <summary>
        /// return dimension vector, fixed length, for subarray operations
        /// </summary>
        /// <param name="length">Number of dimensions to consider</param>
        /// <returns>Dimension vector, corresponds to reshaped or unlimited dimensions.</returns>
        internal uint[] ToIntArrayEx(uint length) {
            uint[] ret = new uint[length];
            if (length == NumberOfDimensions) {
                for (int i = 0; i < length; i++) {
                    ret[i] = (uint)m_descriptor[i + 3];
                }
            } else if (length > NumberOfDimensions) {
                int i = 0;
                for (; i < (uint)m_descriptor[0]; i++) {
                    ret[i] = (uint)m_descriptor[i + 3];
                }
                for (; i < length; i++) {
                    ret[i] = 1;
                }
            } else if (length > 0) {
                int i = 0;
                for (; i < length; i++) {
                    ret[i] = (uint)m_descriptor[i + 3];
                }
                for (int a = i--; a < (uint)m_descriptor[0]; a++) {
                    ret[i] *= (uint)m_descriptor[a + 3];
                }
            } else
                throw new ArgumentException("The length parameter must be positive.");
            return ret;
        }
        /// <summary>
        /// Transform dimension position into sequential index, gather expand 
        /// information
        /// </summary>
        /// <param name="idx">int array of arbitrary length</param>
        /// <param name="MustExpand">[output] true, if the indices 
        /// given address an element outside of 
        /// this dimensions size. In this case, the output parameter 
        /// 'Dimensions' carry the sizes 
        /// of new dimensions needed. False otherwise</param>
        /// <param name="dimensions">sizes of dimension if expansion is needed. 
        /// Must be predefined to length of max(idx.Length,m_nrDims) at least</param>
        /// <returns>Index number pointing to the value's position in 
        /// sequential storage.</returns>
        /// <remarks>no checks are made for idx to fit inside dimensions! 
        /// This functions is used for left side assignments. Therefore it 
        /// computes the destination index also if it lays outside 
        /// the array bounds.</remarks>
        internal uint IndexFromArray(ref bool MustExpand, ref uint[] dimensions, uint[] idx) {
            uint nrDims = NumberOfDimensions;
            long numElements = NumberOfElements;
            uint tmp;
            if (idx.Length < nrDims) {
        #region idx < nrDims
                // expanding is allowed for all but the last specified dimension
                // reason: if less than m_nrDims idx entries exist, the array is 
                // reshaped to that number of dimensions and the index access 
                // computed according to the reshaped version. Attempts to expand
                // that last (virtual) dimension is not allowed than since that 
                // dimension would be ambigous. 
        #region special case: sequential addressing
                if (idx.Length < 2) {
                    if (idx.Length == 1) {
                        tmp = idx[0];
                        if (tmp >= numElements) {
                            // allowed only for scalars and vectors
                            if (this[0u] == numElements) {
                                dimensions[0] = tmp + 1;
                                MustExpand = true;
                                return tmp;
                            } else if (this[1u] == numElements) {
                                dimensions[1] = tmp + 1;
                                MustExpand = true;
                                return tmp;
                            } else
                                throw new ArgumentException("invalid attempt to expand non vector sized array");
                        } else {
                            return tmp;
                        }
                    }
                    throw new ArgumentException("invalid index specification: must not be empty");
                }
        #endregion
                uint d = 0, faktor = 1;
                uint ret = 0;
                tmp = idx[0];
                while (d < idx.Length - 1) {
                    if (tmp < 0)
                        throw new ArgumentException("check index at dimension #" + d.ToString() + "!");
                    if (tmp >= this[d]) {
                        dimensions[d] = tmp + 1;
                        MustExpand = true;
                        ret += (uint)(faktor * tmp);
                        faktor *= (uint)tmp + 1;
                    } else {
                        ret += (uint)(faktor * tmp);
                        faktor *= this[d];
                    }
                    tmp = idx[++d];
                }
                while (d < nrDims) {
                    ret += (uint)(faktor * ((tmp % this[d])));
                    tmp /= this[d];
                    faktor *= (uint)m_descriptor[3 + d++];
                }
                if (tmp > 0)
                    throw new ArgumentException("expanding is allowed for explicitly bounded dimensions only! You must specify indices into all existing dimensions.");
                return ret;
        #endregion
            } else if (idx.Length == nrDims) {
                // expanding is allowed for all dimensions 
                uint d = 0, faktor = 1;
                uint ret = 0;
                while (d < idx.Length) {
                    tmp = idx[d];
                    if (tmp < 0)
                        throw new ArgumentException("check index at dimension #" + d.ToString() + " !");
                    if (tmp >= this[d]) {
                        dimensions[d] = tmp + 1;
                        MustExpand = true;
                        ret += (faktor * tmp);
                        faktor *= tmp + 1;
                    } else {
                        ret += (faktor * tmp);
                        faktor *= this[d];
                    }
                    d++;
                }
                return ret;
            } else {
                // idx dimensions are larger than my dimensions
                uint d = 0, faktor = 1;
                uint ret = 0;
                tmp = idx[0];
                while (d < nrDims) {
                    tmp = idx[d];
                    if (tmp < 0)
                        throw new ArgumentException("check index at dimension " + d.ToString() + "!");
                    if (tmp >= this[d]) {
                        dimensions[d] = tmp + 1;
                        MustExpand = true;
                        ret += (faktor * tmp);
                        faktor *= tmp + 1;
                    } else {
                        ret += (faktor * tmp);
                        faktor *= this[d];
                    }
                    d++;
                }
                while (d < idx.Length) {
                    tmp = idx[d];
                    if (tmp > 0) {
                        dimensions[d] = tmp + 1;
                        MustExpand = true;
                    }
                    d++;
                    faktor *= tmp;
                    ret += faktor;
                }
                return ret;
            }
        }
#endif

        #endregion

        #region mutation functions
        /// <summary>
        /// Removes the specified dimension from this size descriptor. Does not change the number of elements / striding / base offset.
        /// </summary>
        /// <param name="dim">Index of the dimension to be removed.</param>
        /// <remarks><paramref name="dim"/> must point to an existing, _singleton_ dimension.
        /// <para>The current value of <see cref="Settings.MinNumberOfArrayDimensions"/> is respected.</para></remarks>
        internal void RemoveDimension(uint dim) {
            if (dim >= NumberOfDimensions) return; 
            if (this[dim] != 1) {
                throw new ArgumentException($"The specified dimension cannot be removed. It is not a singleton dimension. My size:{ToString()}, dim: {dim}."); 
            }
            if (NumberOfDimensions > Settings.MinNumberOfArrayDimensions) {
                uint i; 
                // move last part of dims & first part of strides
                for (i = 0; i < NumberOfDimensions - 1; i++) {
                    m_descriptor[3 + dim + i] = m_descriptor[4 + dim + i];
                }
                for (i = dim; i < NumberOfDimensions - 1; i++) {
                    m_descriptor[3 + NumberOfDimensions - 1 + i] = m_descriptor[3 + NumberOfDimensions + 1 + i];
                    System.Diagnostics.Debug.Assert(m_descriptor[3 + NumberOfDimensions - 1 + i] == 0 || m_descriptor[3 + i] != 1); 
                }
                m_descriptor[0]--;
            } else {
                // must respect the minimum number of dimensions (Matlab style)
                // nr.of dimension stays the same. values are rotated and padded with 1 / 0 at the end
                for (uint i = dim; i < NumberOfDimensions - 1; i++) {
                    m_descriptor[3 + i] = m_descriptor[4 + i]; 
                    m_descriptor[3 + NumberOfDimensions + i] = m_descriptor[4 + NumberOfDimensions + i];
                }
                m_descriptor[2 + NumberOfDimensions] = 1;
                m_descriptor[2 + 2 * NumberOfDimensions] = 0;
            }
        }

        /// <summary>
        /// [EXPERT INTERFACE] This function is left for ILNumerics expert users. Don't use unless you really know what you are doing!
        /// </summary>
        /// <param name="size_">New size</param>
        /// <param name="order">New order</param>
        internal void SetAll(InArray<long> size_, StorageOrders order) {
            using (Scope.Enter()) {
                Array<long> size = size_; 
                if (Equals(size, null) || !size.IsVector || size.S.NumberOfElements > Size.MaxNumberOfDimensions 
                    || (bool)ILNumerics.Core.Functions.Builtin.MathInternal.anyall(size < 0)) {
                    throw new ArgumentException($"The size for the new array must be specified as a vector of length l, 0 <= l <= {Size.MaxNumberOfDimensions} with non-negative values.");
                }
                if (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor) {
                    throw new ArgumentException("New arrays must be created in row major or column major storage order.");
                }

                var ndims = Math.Max(size.Length, Settings.MinNumberOfArrayDimensions);
                m_descriptor[0] = ndims;
                m_descriptor[2] = 0;
                long stride = 1;
                for (int i = 0; i < ndims; i++) {
                    if (order == StorageOrders.ColumnMajor) {
                        var d = size.S.NumberOfElements > i ? size.GetValue(i) : 1;
                        m_descriptor[3 + i] = d;
                        m_descriptor[3 + ndims + i] = d == 1 ? 0 : stride;  // broadcasting! 
                        stride *= d;
                    } else {
                        // RowMajor
                        var d = size.S.NumberOfElements > i ? size.GetValue(-(i + 1)) : 1; // size.GetValue((uint)(size.S.NumberOfElements - 1 - i));  // (uint)(size.S.NumberOfElements - ndims + i)); // sic: uint wrapping intended
                        m_descriptor[2 + ndims - i] = d;
                        m_descriptor[2 + ndims + ndims - i] = d == 1 ? 0 : stride;  // broadcasting!
                        stride *= d;
                    }
                }
                m_descriptor[1] = stride;
                m_flags = 0x80000000 + (uint)order;
            }
        }
        /// <summary>
        /// This copies the full dimension specification from <paramref name="size"/>, incl. base offset and stride to this size descriptor, overwriting the BSD of the local object instance. 
        /// </summary>
        /// <param name="size">Source dimension description (BSD).</param>
        /// <param name="baseOffset">[Optional] Alternative value for base offset. Default: the value from this instance.</param>
        /// <remarks><para>All BSD information are copied from the source <paramref name="size"/>, including the 
        /// number of dimensions, the lengths of the dimensions, the strides and the base offset.</para></remarks>
        internal void SetAll(Size size, long? baseOffset = null) {
            System.Diagnostics.Debug.Assert(m_descriptor != null);
            System.Diagnostics.Debug.Assert(size.m_descriptor != null);
            System.Diagnostics.Debug.Assert(size.NumberOfDimensions <= MaxNumberOfDimensions);
            var src = size.m_descriptor;
            for (uint k = 0; k < size.NumberOfDimensions * 2 + 3; k++) {
                m_descriptor[k] = src[k];
            }
            m_flags = size.m_flags;
            if (baseOffset.HasValue) {
                m_descriptor[2] = baseOffset.GetValueOrDefault();
            }
#if DEBUG
            Size.CheckSizeBroadcastableStrides(this); 
#endif

        }
        /// <summary>
        /// This copies the BSD heads (number dims, number elements, offset) and optionally the lengths and strides. 
        /// </summary>
        /// <param name="size">Source descriptor (BSD).</param>
        /// <param name="setLengths">[Optional] Flag determining whether to copy the length information. Default: true.</param>
        /// <param name="setStrides">[Optional] Flag determining whether to copy the stride information. Default: true.</param>
        /// <remarks>This functino is used by accelerated code / segment awaiter. It partially copies the information of <paramref name="size"/> to this size descriptor. 
        /// This Size can be left in inconsistent state! It is fine acc. to the various states of an array instruction processing pipeline. 
        /// <para>Do not rely on the size information existing prior to calling <see cref="Set"/>! If you need a <i>consistent</i> size 
        /// afterwards, this method will not create such (only after copying all parts from incoming size and only if the incoming size 
        /// is consistent with this size. Something which must be ensured somehow!</para></remarks>
        internal void Set(Size size, bool setLengths = true, bool setStrides = true) {
            System.Diagnostics.Debug.Assert(m_descriptor != null);
            System.Diagnostics.Debug.Assert(size.m_descriptor != null);
            System.Diagnostics.Debug.Assert(size.NumberOfDimensions <= MaxNumberOfDimensions);
            var oldNDims = NumberOfDimensions;
            var src = size.m_descriptor;
            m_descriptor[0] = src[0];
            m_descriptor[1] = src[1];
            m_descriptor[2] = src[2];
            var start = 3 + (setLengths ? 0 : (setStrides ? src[0] : src[0] * 2)); 
            var len = size.NumberOfDimensions * ((setLengths ? 1 : 0) + (setStrides ? 1 : 0)); 
            for (var k = start; k < start + len; k++) {
                m_descriptor[k] = src[k];
            }
            if (setStrides) {
                m_flags = size.m_flags;
#if DEBUG
            Size.CheckSizeBroadcastableStrides(this);
#endif
            } else {
                // make sure all dimensions have a stride defined. Former size may had fewer dims than new incoming size! 
                var i = oldNDims; 
                while (i < NumberOfDimensions) {
                    m_descriptor[3 + NumberOfDimensions + i] = 0; 
                    ++i;
                }
            }

        }

        /// <summary>
        /// This copies the full dimension specification from <paramref name="size"/> to this size descriptor, overwriting the BSD of the local object instance. 
        /// </summary>
        /// <param name="size">Source dimension description (BSD).</param>
        /// <param name="baseOffset">New base offset for the resulting size descriptor.</param>
        /// <param name="storageOrder">The target storage order of this size descriptor. Allowed values: <see cref="StorageOrders.RowMajor"/>, <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <remarks><para>All BSD information are copied from the source <paramref name="size"/>, including the 
        /// number of dimensions, the lengths of the dimensions. The strides and the base offset are 
        /// set according to <paramref name="baseOffset"/> and <paramref name="storageOrder"/>.</para></remarks>
        /// <exception cref="ArgumentException">if <paramref name="storageOrder"/> is neither of <see cref="StorageOrders.RowMajor"/> or <see cref="StorageOrders.ColumnMajor"/>.</exception>
        internal void SetAll(Size size, long baseOffset, StorageOrders storageOrder) {
            System.Diagnostics.Debug.Assert(m_descriptor != null);
            System.Diagnostics.Debug.Assert(size.m_descriptor != null);
            System.Diagnostics.Debug.Assert(size.NumberOfDimensions <= MaxNumberOfDimensions);
            if (storageOrder != StorageOrders.ColumnMajor && storageOrder != StorageOrders.RowMajor) {
                throw new ArgumentException("A given storage order for new storages must either be 'row major' or 'column major'.");
            }

            var src = size.m_descriptor;
            var ndims = src[0];
            m_descriptor[0] = src[0];
            m_descriptor[1] = src[1];
            m_descriptor[2] = baseOffset;
            if (storageOrder != size.StorageOrder) {
                long stride = 1;
                for (int i = 0; i < ndims; i++) {
                    if (storageOrder == StorageOrders.ColumnMajor) {
                        var d = src[3 + i];
                        m_descriptor[3 + i] = d;
                        m_descriptor[3 + ndims + i] = d != 1 ? stride : 0;
                        stride *= d;
                    } else {
                        // RowMajor
                        var d = src[2 + ndims - i];
                        m_descriptor[2 + ndims - i] = d;
                        m_descriptor[2 + ndims + ndims - i] = d != 1 ? stride : 0;
                        stride *= d;
                    }
                }
                System.Diagnostics.Debug.Assert((long)stride == NumberOfElements);
            } else {
                var dims = m_descriptor + 3;
                var strs = m_descriptor + 3 + ndims;
                for (int i = 0; i < ndims; i++) {
                    dims[i] = src[3 + i];
                    strs[i] = dims[i] != 1 ? src[3 + i + ndims] : 0;
                }
            }
            m_flags = 0x80000000 + (uint)storageOrder;
        }

#region SetAll dims (long) + order
        /// <summary>
        /// Sets all dimensions and strides of this size descriptor, continoues storage.
        /// </summary>
        /// <param name="dim0"></param>
        /// <param name="order"></param>
        internal unsafe void SetAll(long dim0, StorageOrders? order = null) {
            var ndims = Math.Max(1, Settings.MinNumberOfArrayDimensions);
            m_descriptor[0] = ndims;
            m_descriptor[1] = dim0;
            m_descriptor[2] = 0;
            m_descriptor[3] = dim0;

            if (!order.HasValue) {
                order = Settings.DefaultStorageOrder;
            }
            if (ndims == 1) {
                m_descriptor[4] = dim0 == 1 ? 0 : 1 ;
            } else {
                // matlab compatibility 
                System.Diagnostics.Debug.Assert(ndims >= 2);
                m_descriptor[4] = 1;

                if (order == StorageOrders.ColumnMajor) {
                    m_descriptor[5] = dim0 == 1 ? 0 : 1;
                    m_descriptor[6] = 0;
                } else {
                    m_descriptor[6] = 0;
                    m_descriptor[5] = dim0 == 1 ? 0 : 1;
                }
            }
            System.Diagnostics.Debug.Assert(order == StorageOrders.ColumnMajor || order == StorageOrders.RowMajor, "This function allows continous storage only: StorageOrders.ColumnMajor or StorageOrders.RowMajor. This error is likely caused by using a creation function with a '0' literal as last dimension length specifier: ones(1,0), zeros<int>(1,0),... Use: ones(1, dim1:0) and zeros<int>(1, dim1: 0) instead.");
            m_flags = (uint)order.GetValueOrDefault() | CONT_FLAG;
        }
        /// <summary>
        /// Sets all dimensions and strides of this size descriptor, continous storage.
        /// </summary>
        /// <param name="dim0"></param>
        /// <param name="dim1"></param>
        /// <param name="order"></param>
        internal unsafe void SetAll(long dim0, long dim1, StorageOrders? order = null) {
            m_descriptor[0] = Math.Max(2, Settings.MinNumberOfArrayDimensions);
            m_descriptor[1] = (dim0 * dim1);
            m_descriptor[2] = 0;
            m_descriptor[3] = (dim0);
            m_descriptor[4] = (dim1);
            if (!order.HasValue) {
                order = Settings.DefaultStorageOrder;
            }
            switch (order.GetValueOrDefault()) {
                case StorageOrders.ColumnMajor:
                    m_descriptor[5] = dim0 == 1 ? 0 : 1;
                    m_descriptor[6] = dim1 == 1 ? 0 : dim0;
                    break;
                case StorageOrders.RowMajor:
                    m_descriptor[5] = dim0 == 1 ? 0 : dim1;
                    m_descriptor[6] = dim1 == 1 ? 0 : 1;
                    break;
                default:
                    throw new ArgumentException("This function allows continous storage only: StorageOrders.ColumnMajor or StorageOrders.RowMajor.");
            }
            m_flags = (uint)order.GetValueOrDefault() | CONT_FLAG;
        }
        /// <summary>
        /// Sets all dimensions and strides of this size descriptor, continoues storage.
        /// </summary>
        /// <param name="dim0"></param>
        /// <param name="dim1"></param>
        /// <param name="dim2"></param>
        /// <param name="order"></param>
        internal unsafe void SetAll(long dim0, long dim1, long dim2, StorageOrders? order = null) {
            m_descriptor[0] = (Math.Max(3, Settings.MinNumberOfArrayDimensions));
            m_descriptor[1] = dim0 * dim1 * dim2;
            m_descriptor[2] = 0;
            m_descriptor[3] = (dim0);
            m_descriptor[4] = (dim1);
            m_descriptor[5] = (dim2);
            if (!order.HasValue) {
                order = Settings.DefaultStorageOrder;
            }
            switch (order.GetValueOrDefault()) {
                case StorageOrders.ColumnMajor:
                    m_descriptor[6] = dim0 == 1 ? 0 : 1;
                    m_descriptor[7] = dim1 == 1 ? 0 : dim0;
                    m_descriptor[8] = dim2 == 1 ? 0 : (dim1 * dim0);
                    break;

                case StorageOrders.RowMajor:
                    m_descriptor[6] = dim0 == 1 ? 0 : dim1 * dim2;
                    m_descriptor[7] = dim1 == 1 ? 0 : dim2;
                    m_descriptor[8] = dim2 == 1 ? 0 : 1;
                    break;

                default:
                    throw new ArgumentException("This function allows continous storage only: StorageOrders.ColumnMajor or StorageOrders.RowMajor.");
            }
            m_flags = (uint)order.GetValueOrDefault() | CONT_FLAG;
        }
        /// <summary>
        /// Sets all dimensions and strides of this size descriptor, continoues storage.
        /// </summary>
        /// <param name="dim0"></param>
        /// <param name="dim1"></param>
        /// <param name="dim2"></param>
        /// <param name="dim3"></param>
        /// <param name="order"></param>
        internal unsafe void SetAll(long dim0, long dim1, long dim2, long dim3, StorageOrders? order = null) {
            m_descriptor[0] = (Math.Max(4, Settings.MinNumberOfArrayDimensions));
            m_descriptor[1] = dim0 * dim1 * dim2 * dim3;
            m_descriptor[2] = 0;
            m_descriptor[3] = (dim0);
            m_descriptor[4] = (dim1);
            m_descriptor[5] = (dim2);
            m_descriptor[6] = (dim3);
            if (!order.HasValue) {
                order = Settings.DefaultStorageOrder;
            }
            switch (order.GetValueOrDefault()) {
                case StorageOrders.ColumnMajor:
                    m_descriptor[7] = dim0 == 1 ? 0 : (1);
                    m_descriptor[8] = dim1 == 1 ? 0 : (dim0);
                    m_descriptor[9] = dim2 == 1 ? 0 : (dim0 * dim1);
                    m_descriptor[10] =dim3 == 1 ? 0 : (dim0 * dim1 * dim2);

                    break;
                case StorageOrders.RowMajor:
                    m_descriptor[10] =dim3 == 1 ? 0 : (1);
                    m_descriptor[9] = dim2 == 1 ? 0 : (dim3);
                    m_descriptor[8] = dim1 == 1 ? 0 : (dim3 * dim2);
                    m_descriptor[7] = dim0 == 1 ? 0 : (dim3 * dim2 * dim1);

                    break;
                default:
                    throw new ArgumentException("This function allows continous storage only: StorageOrders.ColumnMajor or StorageOrders.RowMajor.");
            }
            m_flags = (uint)order.GetValueOrDefault() | CONT_FLAG;
        }
        /// <summary>
        /// Sets all dimensions and strides of this size descriptor, continoues storage.
        /// </summary>
        /// <param name="dim0"></param>
        /// <param name="dim1"></param>
        /// <param name="dim2"></param>
        /// <param name="dim3"></param>
        /// <param name="dim4"></param>
        /// <param name="order"></param>
        internal unsafe void SetAll(long dim0, long dim1, long dim2, long dim3, long dim4, StorageOrders? order = null) {
            m_descriptor[0] = (Math.Max(5, Settings.MinNumberOfArrayDimensions));
            m_descriptor[1] = dim0 * dim1 * dim2 * dim3 * dim4;
            m_descriptor[2] = 0;
            m_descriptor[3] = (dim0);
            m_descriptor[4] = (dim1);
            m_descriptor[5] = (dim2);
            m_descriptor[6] = (dim3);
            m_descriptor[7] = (dim4);
            if (!order.HasValue) {
                order = Settings.DefaultStorageOrder;
            }
            switch (order.GetValueOrDefault()) {
                case StorageOrders.ColumnMajor:
                    m_descriptor[8] = dim0 == 1 ? 0 : (1);
                    m_descriptor[9] = dim1 == 1 ? 0 : (dim0);
                    m_descriptor[10] = dim2 == 1 ? 0 : (dim0 * dim1);
                    m_descriptor[11] = dim3 == 1 ? 0 : (dim0 * dim1 * dim2);
                    m_descriptor[12] = dim4 == 1 ? 0 : (dim0 * dim1 * dim2 * dim3);

                    break;
                case StorageOrders.RowMajor:
                    m_descriptor[12] = dim4 == 1 ? 0 : (1);
                    m_descriptor[11] = dim3 == 1 ? 0 : (dim4);
                    m_descriptor[10] = dim2 == 1 ? 0 : (dim4 * dim3);
                    m_descriptor[9] = dim1 == 1 ? 0 : (dim4 * dim3 * dim2);
                    m_descriptor[8] = dim0 == 1 ? 0 : (dim4 * dim3 * dim2 * dim1);

                    break;
                default:
                    throw new ArgumentException("This function allows continous storage only: StorageOrders.ColumnMajor or StorageOrders.RowMajor.");
            }
            m_flags = (uint)order.GetValueOrDefault() | CONT_FLAG;
        }
        /// <summary>
        /// Sets all dimensions and strides of this size descriptor, continoues storage.
        /// </summary>
        /// <param name="dim0"></param>
        /// <param name="dim1"></param>
        /// <param name="dim2"></param>
        /// <param name="dim3"></param>
        /// <param name="dim4"></param>
        /// <param name="dim5"></param>
        /// <param name="order"></param>
        internal unsafe void SetAll(long dim0, long dim1, long dim2, long dim3, long dim4, long dim5, StorageOrders? order = null) {
            m_descriptor[0] = (Math.Max(6, Settings.MinNumberOfArrayDimensions));
            m_descriptor[1] = dim0 * dim1 * dim2 * dim3 * dim4 * dim5;
            m_descriptor[2] = 0;
            m_descriptor[3] = (dim0);
            m_descriptor[4] = (dim1);
            m_descriptor[5] = (dim2);
            m_descriptor[6] = (dim3);
            m_descriptor[7] = (dim4);
            m_descriptor[8] = (dim5);
            if (!order.HasValue) {
                order = Settings.DefaultStorageOrder;
            }
            switch (order.GetValueOrDefault()) {
                case StorageOrders.ColumnMajor:
                    m_descriptor[9] = dim0 == 1 ? 0 : (1);
                    m_descriptor[10] = dim1 == 1 ? 0 : (dim0);
                    m_descriptor[11] = dim2 == 1 ? 0 : (dim0 * dim1);
                    m_descriptor[12] = dim3 == 1 ? 0 : (dim0 * dim1 * dim2);
                    m_descriptor[13] = dim4 == 1 ? 0 : (dim0 * dim1 * dim2 * dim3);
                    m_descriptor[14] = dim5 == 1 ? 0 : (dim0 * dim1 * dim2 * dim3 * dim4);
                    break;

                case StorageOrders.RowMajor:
                    m_descriptor[14] = dim5 == 1 ? 0 : (1);
                    m_descriptor[13] = dim4 == 1 ? 0 : (dim5);
                    m_descriptor[12] = dim3 == 1 ? 0 : (dim5 * dim4);
                    m_descriptor[11] = dim2 == 1 ? 0 : (dim5 * dim4 * dim3);
                    m_descriptor[10] = dim1 == 1 ? 0 : (dim5 * dim4 * dim3 * dim2);
                    m_descriptor[9] = dim0 == 1 ? 0 : (dim5 * dim4 * dim3 * dim2 * dim1);
                    break;

                default:
                    throw new ArgumentException("This function allows continous storage only: StorageOrders.ColumnMajor or StorageOrders.RowMajor.");
            }
            m_flags = (uint)order.GetValueOrDefault() | CONT_FLAG;
        }
        /// <summary>
        /// Sets all dimensions and strides of this size descriptor, continoues storage.
        /// </summary>
        /// <param name="dim0"></param>
        /// <param name="dim1"></param>
        /// <param name="dim2"></param>
        /// <param name="dim3"></param>
        /// <param name="dim4"></param>
        /// <param name="dim5"></param>
        /// <param name="dim6"></param>
        /// <param name="order"></param>
        internal unsafe void SetAll(long dim0, long dim1, long dim2, long dim3, long dim4, long dim5, long dim6, StorageOrders? order = null) {
            m_descriptor[0] = (Math.Max(7, Settings.MinNumberOfArrayDimensions));
            m_descriptor[1] = dim0 * dim1 * dim2 * dim3 * dim4 * dim5 * dim6;
            m_descriptor[2] = 0;
            m_descriptor[3] = (dim0);
            m_descriptor[4] = (dim1);
            m_descriptor[5] = (dim2);
            m_descriptor[6] = (dim3);
            m_descriptor[7] = (dim4);
            m_descriptor[8] = (dim5);
            m_descriptor[9] = (dim6);
            if (!order.HasValue) {
                order = Settings.DefaultStorageOrder;
            }
            switch (order.GetValueOrDefault()) {
                case StorageOrders.ColumnMajor:
                    m_descriptor[10] = dim0 == 1 ? 0 : (1);
                    m_descriptor[11] = dim1 == 1 ? 0 : (dim0);
                    m_descriptor[12] = dim2 == 1 ? 0 : (dim0 * dim1);
                    m_descriptor[13] = dim3 == 1 ? 0 : (dim0 * dim1 * dim2);
                    m_descriptor[14] = dim4 == 1 ? 0 : (dim0 * dim1 * dim2 * dim3);
                    m_descriptor[15] = dim5 == 1 ? 0 : (dim0 * dim1 * dim2 * dim3 * dim4);
                    m_descriptor[16] = dim6 == 1 ? 0 : (dim0 * dim1 * dim2 * dim3 * dim4 * dim5);
                    break;

                case StorageOrders.RowMajor:
                    m_descriptor[16] = dim6 == 1 ? 0 : (1);
                    m_descriptor[15] = dim5 == 1 ? 0 : (dim6);
                    m_descriptor[14] = dim4 == 1 ? 0 : (dim6 * dim5);
                    m_descriptor[13] = dim3 == 1 ? 0 : (dim6 * dim5 * dim4);
                    m_descriptor[12] = dim2 == 1 ? 0 : (dim6 * dim5 * dim4 * dim3);
                    m_descriptor[11] = dim1 == 1 ? 0 : (dim6 * dim5 * dim4 * dim3 * dim2);
                    m_descriptor[10] = dim0 == 1 ? 0 : (dim6 * dim5 * dim4 * dim3 * dim2 * dim1);
                    break;

                default:
                    throw new ArgumentException("This function allows continous storage only: StorageOrders.ColumnMajor or StorageOrders.RowMajor.");
            }
            m_flags = (uint)order.GetValueOrDefault() | CONT_FLAG;
        }
        #endregion

        /// <summary>
        /// Set all values of this size descriptor. 
        /// </summary>
        /// <param name="bsd"></param>
        /// <param name="flags"></param>
        
        public unsafe void SetAll(long* bsd, uint flags = 0) {
            SetAll(bsd, bsd[2], flags); 
        }

        /// <summary>
        /// Set all values of this size descriptor. 
        /// </summary>
        /// <param name="bsd"></param>
        /// <param name="baseOffset">destination base offset</param>
        /// <param name="flags"></param>
        
        internal unsafe void SetAll(long* bsd, long baseOffset, uint flags = 0) {
            System.Diagnostics.Debug.Assert(bsd != (long*)0);
            uint ndims = (uint)bsd[0];
            System.Diagnostics.Debug.Assert(ndims <= MaxNumberOfDimensions);
            m_descriptor[0] = bsd[0];
            m_descriptor[1] = bsd[1];
            m_descriptor[2] = baseOffset;
            for (uint i = 0; i < ndims; i++) {
                m_descriptor[3 + i] = bsd[3 + i];

                // this ensures broadcastability of the strides. It may can be removed, if attention is taken at the source, though. 
                m_descriptor[3 + i + ndims] = bsd[3 + i] == 1 ? 0 : bsd[3 + i + ndims];
            }
            m_flags = flags;
        }
        /// <summary>
        /// Set dimensions and strides of this size descriptor explicitly, 1 dimension. 
        /// </summary>
        /// <param name="len0">Length of dim #0.</param>
        /// <param name="stride0">Stride of dim #0.</param>
        /// <remarks><para>This creates a vector array size descriptor with one dimension of length <paramref name="len0"/>.
        /// </para>
        /// <para>The actual size / number of dimensions is affected by the setting of the global configuration value <see cref="Settings.MinNumberOfArrayDimensions"/>. 
        /// By default this setting has a value of 2 (Matlab / Octave compatibility) which leads to the creation of 
        /// arrays with at least 2 dimensions (all arrays are matrices or higher-dimensional). In order to create truely 1-dimensional 
        /// arrays one must configure the <see cref="Settings.MinNumberOfArrayDimensions"/> with a value of 1 or 0.</para>
        /// </remarks>
        internal void SetAll(uint len0, uint stride0) {
            uint n = Math.Max(1, Settings.MinNumberOfArrayDimensions);
            m_descriptor[0] = (n);
            m_descriptor[1] = (len0);
            m_descriptor[2] = (0);
            m_descriptor[3] = (len0);
            if (n == 1) {
                m_descriptor[4] = (stride0);
            } else {
                // matlab compatibility 
                System.Diagnostics.Debug.Assert(n >= 2);
                m_descriptor[4] = (1);
                m_descriptor[5] = (stride0);
                m_descriptor[6] = (len0 * stride0);
            }
            //m_storageOrder = storageOrder ?? ((stride0 == 1) ? StorageOrders.ColumnMajor : StorageOrders.Other);
            m_flags = 0;
        }
        /// <summary>
        /// Set dimensions and strides of this size descriptor explicitly, 2 dimensions. 
        /// </summary>
        /// <param name="len0">BSD data #0 (length dim 0).</param>
        /// <param name="len1">BSD data #1.</param>
        /// <param name="stride0">BSD data #2.</param>
        /// <param name="stride1">BSD data #3.</param>
        internal void SetAll(uint len0, uint len1, uint stride0, uint stride1) {
            m_descriptor[0] = (2);
            m_descriptor[1] = (len0 * len1);
            m_descriptor[2] = (0);
            m_descriptor[3] = (len0);
            m_descriptor[4] = (len1);
            m_descriptor[5] = (stride0);
            m_descriptor[6] = (stride1);
            m_flags = 0;
        }
        /// <summary>
        /// Set dimensions and strides of this size descriptor explicitly, 3 dimensions. 
        /// </summary>
        /// <param name="len0">BSD data #0 (length dim 0).</param>
        /// <param name="len1">BSD data #1.</param>
        /// <param name="len2">BSD data #2.</param>
        /// <param name="stride0">BSD data #3.</param>
        /// <param name="stride1">BSD data #4.</param>
        /// <param name="stride2">BSD data #5.</param>
        internal void SetAll(uint len0, uint len1, uint len2, uint stride0, uint stride1, uint stride2) {
            m_descriptor[0] = (3);
            m_descriptor[1] = (len0 * len1 * len2);
            m_descriptor[2] = (0);
            m_descriptor[3] = (len0);
            m_descriptor[4] = (len1);
            m_descriptor[5] = (len2);
            m_descriptor[6] = (stride0);
            m_descriptor[7] = (stride1);
            m_descriptor[8] = (stride2);
            m_flags = 0;
        }
        /// <summary>
        /// Set dimensions and strides of this size descriptor explicitly, 4 dimensions. 
        /// </summary>
        /// <param name="len0">BSD data #0 (length dim 0).</param>
        /// <param name="len1">BSD data #1.</param>
        /// <param name="len2">BSD data #2.</param>
        /// <param name="len3">BSD data #3.</param>
        /// <param name="stride0">BSD data #4.</param>
        /// <param name="stride1">BSD data #5.</param>
        /// <param name="stride2">BSD data.</param>
        /// <param name="stride3">BSD data.</param>
        internal void SetAll(uint len0, uint len1, uint len2, uint len3, uint stride0, uint stride1, uint stride2,
            uint stride3) {
            m_descriptor[0] = (4);
            m_descriptor[1] = (len0 * len1 * len2 * len3);
            m_descriptor[2] = (0);
            m_descriptor[3] = (len0);
            m_descriptor[4] = (len1);
            m_descriptor[5] = (len2);
            m_descriptor[6] = (len3);
            m_descriptor[7] = (stride0);
            m_descriptor[8] = (stride1);
            m_descriptor[9] = (stride2);
            m_descriptor[10] = (stride3);
            m_flags = 0;
        }
        /// <summary>
        /// Set dimensions and strides of this size descriptor explicitly, 5 dimensions. 
        /// </summary>
        /// <param name="len0">BSD data #0 (length dim 0).</param>
        /// <param name="len1">BSD data #1.</param>
        /// <param name="len2">BSD data #2.</param>
        /// <param name="len3">BSD data #3.</param>
        /// <param name="len4">BSD data.</param>
        /// <param name="stride0">BSD data.</param>
        /// <param name="stride1">BSD data.</param>
        /// <param name="stride2">BSD data.</param>
        /// <param name="stride3">BSD data.</param>
        /// <param name="stride4">BSD data.</param>
        internal void SetAll(uint len0, uint len1, uint len2, uint len3, uint len4, uint stride0, uint stride1, uint stride2,
            uint stride3, uint stride4) {
            m_descriptor[0] = (5);
            m_descriptor[1] = (len0 * len1 * len2 * len3 * len4);
            m_descriptor[2] = (0);
            m_descriptor[3] = (len0);
            m_descriptor[4] = (len1);
            m_descriptor[5] = (len2);
            m_descriptor[6] = (len3);
            m_descriptor[7] = (len4);
            m_descriptor[8] = (stride0);
            m_descriptor[9] = (stride1);
            m_descriptor[10] = (stride2);
            m_descriptor[11] = (stride3);
            m_descriptor[12] = (stride4);
            m_flags = 0;
        }
        /// <summary>
        /// Set dimensions and strides of this size descriptor explicitly, 6 dimensions. 
        /// </summary>
        /// <param name="len0">BSD data #0 (length dim 0).</param>
        /// <param name="len1">BSD data #1.</param>
        /// <param name="len2">BSD data #2.</param>
        /// <param name="len3">BSD data #3.</param>
        /// <param name="len4">BSD data.</param>
        /// <param name="len5">BSD data.</param>
        /// <param name="stride0">BSD data.</param>
        /// <param name="stride1">BSD data.</param>
        /// <param name="stride2">BSD data.</param>
        /// <param name="stride3">BSD data.</param>
        /// <param name="stride4">BSD data.</param>
        /// <param name="stride5">BSD data.</param>
        internal void SetAll(uint len0, uint len1, uint len2, uint len3, uint len4, uint len5,
                            uint stride0, uint stride1, uint stride2, uint stride3, uint stride4, uint stride5) {
            m_descriptor[0] = (6);
            m_descriptor[1] = (len0 * len1 * len2 * len3 * len4 * len5);
            m_descriptor[2] = (0);
            m_descriptor[3] = (len0);
            m_descriptor[4] = (len1);
            m_descriptor[5] = (len2);
            m_descriptor[6] = (len3);
            m_descriptor[7] = (len4);
            m_descriptor[8] = (len5);
            m_descriptor[9] = (stride0);
            m_descriptor[10] = (stride1);
            m_descriptor[11] = (stride2);
            m_descriptor[12] = (stride3);
            m_descriptor[13] = (stride4);
            m_descriptor[14] = (stride5);
            m_flags = 0;
        }
        /// <summary>
        /// Merges dimensions l to k of a continguous storage for iteration over all those dims with a single stride. 
        /// </summary>
        /// <param name="d"> starting / ending dimension.</param>
        /// <param name="stride">[output] receives the single stride for iteration over all specified dims.</param>
        /// <returns>The number of elements for iteration.</returns>
        /// <remarks><para>This function works on continous storages only. The behavior is different for both valid cases:</para>
        /// <para>ColumnMajor: l = d + 1, k = <see cref="NumberOfDimensions"/>-1. </para>
        /// <para>RowMajor: l = 0, k = d-1. </para>
        /// <para>Thus, column major storages merge the trailing dimension, above <paramref name="d"/>, while row 
        /// major storages merge the leading dimensions below <paramref name="d"/>.</para>
        /// </remarks>
        internal long MergeNextToEnd(uint d, ref long stride) {
            // we must recognize singleton dimensions! they (should!) give a striding of 0. In this case we must look for the next/previous non-singleton. 
            long ret = 1;
            stride = 0;
            switch (StorageOrder) {
                case StorageOrders.ColumnMajor:
                    for (uint i = d + 1; i < NumberOfDimensions; i++) {
                        if (stride == 0) {
                            var s = GetStride(i);
                            if (s != 0) stride = s;
                        }
                        ret *= this[i]; 
                    }        
                    break;
                case StorageOrders.RowMajor:
                    for (uint i = d; i--> 0;) {
                        if (stride == 0) {
                            var s = GetStride(i);
                            if (s != 0) stride = s;
                        }
                        ret *= this[i];
                    }
                    break; 
                default:
                    throw new InvalidOperationException($"This method requires ColumnMajor or RowMajor storage and cannot be performed on this array. This indicates a bug. Please report it to ILNumerics!"); 
            }
            return ret; 
        }
        /// <summary>
        /// Determins whether this size descriptor has the exact same shape as the range addressed by <paramref name="iterators"/>.
        /// </summary>
        /// <param name="iterators">Predefined index iterators. </param>
        /// <param name="ndims">Number of index iterators provided in <paramref name="iterators"/>.</param>
        /// <returns>True if both, the range and this <see cref="Size"/> have the same size.</returns>
        internal bool IsSameSize(IIndexIterator[] iterators, uint ndims) {

                if (ndims != NumberOfDimensions) {
                    return false;
                }

                for (uint i = 0; i < ndims; i++) {
                    var si = iterators[i].GetLength();
                    var mi = this[i];
                    if (si != mi) {
                        return false;
                    }
                }
                return true;
            }

        /// <summary>
        /// Set dimensions and strides of this size descriptor explicitly, 7 dimensions. 
        /// </summary>
        /// <param name="len0">BSD data #0 (length dim 0).</param>
        /// <param name="len1">BSD data #1.</param>
        /// <param name="len2">BSD data #2.</param>
        /// <param name="len3">BSD data #3.</param>
        /// <param name="len4">BSD data.</param>
        /// <param name="len5">BSD data.</param>
        /// <param name="len6">BSD data.</param>
        /// <param name="stride0">BSD data.</param>
        /// <param name="stride1">BSD data.</param>
        /// <param name="stride2">BSD data.</param>
        /// <param name="stride3">BSD data.</param>
        /// <param name="stride4">BSD data.</param>
        /// <param name="stride5">BSD data.</param>
        /// <param name="stride6">BSD data.</param>
        internal void SetAll(uint len0, uint len1, uint len2, uint len3, uint len4, uint len5, uint len6,
                            uint stride0, uint stride1, uint stride2, uint stride3, uint stride4, uint stride5, uint stride6) {
            m_descriptor[0] = (7);
            m_descriptor[1] = (len0 * len1 * len2 * len3 * len4 * len5 * len6);
            m_descriptor[2] = (0);
            m_descriptor[3] = (len0);
            m_descriptor[4] = (len1);
            m_descriptor[5] = (len2);
            m_descriptor[6] = (len3);
            m_descriptor[7] = (len4);
            m_descriptor[8] = (len5);
            m_descriptor[9] = (len6);
            m_descriptor[10] = (stride0);
            m_descriptor[11] = (stride1);
            m_descriptor[12] = (stride2);
            m_descriptor[13] = (stride3);
            m_descriptor[14] = (stride4);
            m_descriptor[15] = (stride5);
            m_descriptor[16] = (stride6);
            m_flags = 0;
        }

        /// <summary>
        /// This copies the dimension number and lengths from <paramref name="size"/> to this size descriptor, 
        /// overwriting the BSD of the local object instance and creating a column-major storage ordered 
        /// size descriptor with 0 base offset. 
        /// </summary>
        /// <param name="size">Source dimension description (BSD).</param>
        /// <remarks><para>The resulting dimension specification will have strides 
        /// according to a column major storage layout.</para></remarks>
        internal void SetDimensionLengths(Size size) {
            System.Diagnostics.Debug.Assert(m_descriptor != null);
            System.Diagnostics.Debug.Assert(size.m_descriptor != null);
            System.Diagnostics.Debug.Assert(NumberOfDimensions <= MaxNumberOfDimensions);

            var src = size.m_descriptor;
            var n = size.NumberOfDimensions;
            m_descriptor[0] = src[0]; // ndims
            m_descriptor[1] = src[1]; // nelem
            m_descriptor[2] = 0; // base offset  
            var stride = 1u;
            for (int i = 0; i < n; i++) {
                m_descriptor[i + 3] = src[3 + i];
                m_descriptor[i + n + 3] = (m_descriptor[i + 3] == 1 ? 0 : stride);
                stride *= (uint)src[i + 3];
            }
            m_flags = 0x80000001;
        }
        /// <summary>
        /// Copies the dimension lengths from the BSD and completes stride info according to <paramref name="order"/>.
        /// </summary>
        /// <param name="bsd">(potentially incomplete) other BSD.</param>
        /// <param name="order">Array style, determins the strides for this size.</param>
        /// <remarks><paramref name="bsd"/> is expected to have ndims (bsd[0]), nelem (bsd[1]) and dims (bsd[2 + [1 ... ndims]]) configured. 
        /// this function takes all this info and completes base offset (0) and strides to completely configure this size.</remarks>
        public void SetDimensionLengths(long* bsd, StorageOrders order) {
            var n = bsd[0];
            System.Diagnostics.Debug.Assert(n >= 0 && n <= Size.MaxNumberOfDimensions);
            m_descriptor[0] = n; // ndims
            m_descriptor[1] = bsd[1]; // nelem
            m_descriptor[2] = 0; // base offset  
            long stride = 1u;
            if (order == StorageOrders.ColumnMajor) {
                for (int i = 0; i < n; i++) {
                    m_descriptor[i + 3] = bsd[3 + i];
                    m_descriptor[i + n + 3] = (bsd[i + 3] == 1 ? 0 : stride);
                    stride *= bsd[i + 3];
                }
            } else {
                // for order == Other or RowMajor -> RowMajor
                //System.Diagnostics.Debug.Assert(order == StorageOrders.RowMajor);
                for (int i = 0; i < n; i++) {
                    var s = bsd[2 + n - i];
                    m_descriptor[2 + n - i] = s;
                    m_descriptor[2 + n + n - i] = (s == 1 ? 0 : stride);
                    stride *= s;
                }
            }
            System.Diagnostics.Debug.Assert(stride == bsd[1]);
            // order may be ANY enum value, including "Other"! But we need this to be Row- or ColumMajor only!! (All implementations expect the output to be stored in one of these two variants!)
            m_flags = (uint)(order == StorageOrders.ColumnMajor ? StorageOrders.ColumnMajor : StorageOrders.RowMajor) | CONT_FLAG;
        }

        internal void SetScalar(uint baseOffset, uint ndims) {
            m_descriptor[0] = (ndims);
            m_descriptor[1] = (1);
            m_descriptor[2] = (baseOffset);
            var one = (1);
            for (int i = 0; i < ndims; i++) {
                m_descriptor[3 + i] = one;
                m_descriptor[3 + i + ndims] = 0;
            }
            m_flags = (uint)Settings.DefaultStorageOrder | CONT_FLAG;
        }
        internal void SetScalar(long baseOffset, uint ndims) {
            m_descriptor[0] = (ndims);
            m_descriptor[1] = (1);
            m_descriptor[2] = (baseOffset);
            var one = (1);
            for (int i = 0; i < ndims; i++) {
                m_descriptor[3 + i] = one;
                m_descriptor[3 + i + ndims] = 0;
            }
            m_flags = (uint)Settings.DefaultStorageOrder | CONT_FLAG;
        }
        internal static unsafe int GetHashCode(int* bsd) {
            long ret = 19;
            uint e = 2 * (uint)bsd[0] + 3;
            for (int i = 0; i < e; i++) {
                ret = unchecked((ret * 17) + bsd[i]);
            }
            return (int)ret;
        }
        internal static unsafe int GetHashCode(long* bsd) {
            long ret = 19;
            uint e = 2 * (uint)bsd[0] + 3;
            for (int i = 0; i < e; i++) {
                ret = unchecked((ret * 17) + bsd[i]);
            }
            return (int)ret;
        }
        #endregion

        #region public interface

        /// <summary>
        /// Gets a hash code representing this size descriptors current content. 
        /// </summary>
        /// <returns>Hash code.</returns>
        public override unsafe int GetHashCode() {
            return GetHashCode(m_descriptor); 
        }
        /// <summary>
        /// Compares this size to another size, ignoring leading and trailing singleton dimensions. 
        /// </summary>
        /// <param name="dim2">size descriptor to compare this to.</param>
        /// <returns>Returns true if the sizes are the same, else returns false. 
        /// The comparison is made by ignoring singleton dimensions. Therefore 
        /// only non singleton dimensions are compared in the order of their 
        /// appearance. </returns>
        /// <remarks>The function returns true, if the <i>squeezed</i> dimensions of 
        /// both size descriptors match. The sizes of empty arrays are not required to match 
        /// exactly in order to be considered equal.</remarks>
        /// <seealso cref="IsSameShape(Size)"/>
        public bool IsSameSize(Size dim2) {
            if (dim2.NumberOfElements != NumberOfElements) return false;
            for (uint d2 = 0, d1 = 0; d1 < NumberOfDimensions && d2 < dim2.NumberOfDimensions;) {
                while (d1 < NumberOfDimensions && this[d1] == 1) d1++;
                while (d2 < dim2.NumberOfDimensions && dim2[d2] == 1) d2++;
                if (d1 < NumberOfDimensions && d2 < dim2.NumberOfDimensions) {
                    if (this[d1] != dim2[d2])
                        return false;

                }
                d1++; d2++;
            }
            return true;
        }
        /// <summary>
        /// Compares the exact shape of this size to another size. Considering all dimensions.
        /// </summary>
        /// <param name="dim2">size descriptor to compare this to.</param>
        /// <returns>Returns true if the shapes are the same, else returns false. </returns>
        /// <remarks>This function is more strict than <see cref="IsSameSize(Size)"/>. In order 
        /// for two size descriptors to have the same shape, ALL dimensions must match - 
        /// including singleton dimensions.</remarks>
        /// <seealso cref="IsSameSize(Size)"/>
        public bool IsSameShape(Size dim2) {
            if (dim2.NumberOfElements != NumberOfElements) return false;
            if (dim2.NumberOfDimensions != NumberOfDimensions) return false;
            for (uint d1 = NumberOfDimensions; d1-- > 0;) {
                if (this[d1] != dim2[d1])
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Compares the exact shape of this size to another size. Considering all dimensions.
        /// </summary>
        /// <param name="dim2">Array with dimension lengths to compare this <see cref="Size"/> to.</param>
        /// <returns>Returns true if the shapes are the same, else returns false. </returns>
        /// <remarks>This function is more strict than <see cref="IsSameSize(Size)"/>. In order 
        /// for two size descriptors to have the same shape, ALL dimensions must match - 
        /// including singleton dimensions and number of dimensions.</remarks>
        /// <seealso cref="IsSameSize(Size)"/>
        public bool IsSameShape(InArray<long> dim2) {
            using (Scope.Enter(dim2)) {
                if (dim2.S.NumberOfElements != NumberOfDimensions) return false;
                if (Core.Functions.Builtin.MathInternal.prodall(dim2) != NumberOfElements) return false;
                for (uint d1 = NumberOfDimensions; d1-- > 0;) {
                    if (this[d1] != dim2[d1])
                        return false;
                }
                return true;
            }
        }
        /// <summary>
        /// Compares the exact shape of this size to another size. Considering all dimensions.
        /// </summary>
        /// <param name="shape">Pointer to memory with dimension lengths to compare this to. Must be at least of length <see cref="NumberOfDimensions"/>.</param>
        /// <param name="strides">Pointer to memory with strides to compare this to. Must be at least of length <see cref="NumberOfDimensions"/>.</param>
        /// <returns>Returns true if the shapes and the strides are the same, else returns false. </returns>
        /// <remarks>This function is more strict than <see cref="IsSameSize(Size)"/>. In order 
        /// for two size descriptors to be the same ALL dimensions and strides must match - 
        /// including number and position of singleton dimensions.</remarks>
        /// <seealso cref="IsSameSize(Size)"/>
        internal bool IsSame(long* shape, long* strides) {
            for (uint d1 = NumberOfDimensions; d1-- > 0;) {
                if (this[d1] != shape[d1] || m_descriptor[3 + NumberOfDimensions + d1] != strides[d1])
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Compares the exact shape of this size to another size. Considering all dimensions.
        /// </summary>
        /// <param name="shape">Pointer to memory with dimension lengths to compare this to. Must be at least of length <see cref="NumberOfDimensions"/>.</param>
        /// <param name="strides">Pointer to memory with strides to compare this to. Must be at least of length <see cref="NumberOfDimensions"/>.</param>
        /// <returns>Returns true if the shapes and the strides are the same, else returns false. </returns>
        /// <remarks>This function is more strict than <see cref="IsSameSize(Size)"/>. In order 
        /// for two size descriptors to be the same ALL dimensions and strides must match - 
        /// including number and position of singleton dimensions.</remarks>
        /// <seealso cref="IsSameSize(Size)"/>
        internal bool IsSame(int* shape, int* strides) {
            for (uint d1 = NumberOfDimensions; d1-- > 0;) {
                if (this[d1] != shape[d1] || m_descriptor[3 + NumberOfDimensions + d1] != strides[d1])
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Copy the strides of this size descriptor into the array of length <paramref name="len"/> pointed to by <paramref name="seqd"/>.
        /// </summary>
        /// <param name="seqd">Preallocated array of length <paramref name="len"/>. All elements will be filled with 
        /// the strides of the corresponding dimension.</param>
        /// <param name="len">Length of array <paramref name="seqd"/>.</param>
        /// <remarks>If <paramref name="len"/> is larger than <see cref="NumberOfDimensions"/> trailing elements will 
        /// be filled with the value of <see cref="NumberOfElements"/> to maintain consistency..</remarks>
        internal unsafe void GetStrides(long* seqd, uint len) {
            uint nrDims = NumberOfDimensions;
            uint minLen = Math.Min(len, nrDims), i = 0;
            for (; i < minLen; i++) {
                seqd[i] = m_descriptor[nrDims + 3 + i];
            }
            for (; i < len; i++) {
                seqd[i] = m_descriptor[1];
            }
        }

        /// <summary>
        /// [EXPERT INTERFACE!] Gets a reference to the internal BSD array describing the sizes / strides of this size object. Use with care!
        /// </summary>
        /// <param name="write">[Optional] Flag indicating the intended use of the BSD array returned. 
        /// True: callee will alter the array. False: the array returned will <b>not</b> be altered (default).</param>
        /// <returns>Reference to the internal BSD array.</returns>
        public unsafe long* GetBSD(bool write = false) {
            if (write) m_flags = 0;
            return &m_descriptor[0];
        }
        /// <summary>
        /// Gets a <i>copy</i> of the BSD array of this size descriptor with arbitrary number of dimensions. 
        /// </summary>
        /// <param name="ndims">Number of dimensions for the new BSD.</param>
        /// <returns>BSD addressing the same number of elements as this size, with arbitrary number of dimensions.</returns>
        /// <remarks><para>If <paramref name="ndims"/> is not equal to <see cref="NumberOfDimensions"/> trailing 
        /// singleton dimensions are added or missing dimensions at the end are merged to the last 
        /// dimension respectively.</para>
        /// <para>The merging of subsequent, unspecified dimensions is only possible if the storage 
        /// layout of this size is suitable. Column major order storage is suitable for merging. If this 
        /// size is non-column major merging is not possible and an exception will be generated.</para>
        /// </remarks>
        /// <exception cref="InvalidOperationException"> if the number of dimensions is larger than <paramref name="ndims"/>
        /// but the storage layout of this size does not allow to merge subsequent dimensions into the last dimension requested. 
        /// In this case the array should be consolidated before attempting such operation.</exception>
        internal long[] GetBSD(uint ndims) {
            long[] ret = new long[ndims * 2 + 3];

            ret[0] = m_descriptor[0];
            ret[1] = m_descriptor[1];
            ret[2] = m_descriptor[2]; // offset

            int iO = 0, myLen = (int)m_descriptor[0];

            for (; iO < Math.Min(ndims, myLen); iO++) {
                ret[iO + 3] = m_descriptor[iO + 3]; // dim lengths
                ret[iO + ndims + 3] = m_descriptor[iO + myLen + 3];  // strides
            }
            // superflous dimensions exist?
            int expectedStride = (int)m_descriptor[iO + myLen + 2] * (int)m_descriptor[iO + 2];
            for (; iO < myLen; iO++) {
                // we stepped in here only when the above loop exited due to iO < length!
                // 
                // The dimensions can be merged if and only if the strides of the new to-be-merged 
                // dimension is the clean continuation of the strides of the former non-merged dimension.  
                if ((int)m_descriptor[iO + myLen + 3] != expectedStride) {
                    throw new InvalidOperationException("The storage format of this array is not suitable for the requested operation. Consolidate the array before merging dimensions!");
                }
                // simply lengthen the last dimension of the BSD returned
                ret[ndims + 2] = ((uint)ret[ndims + 2] * (uint)m_descriptor[iO + 3]);
                expectedStride *= (int)m_descriptor[iO + 3];
            }
            // ... or add singleton dimensions to the end
            for (; iO < ndims; iO++) {
                ret[iO + 3] = (1);
                ret[iO + ndims + 3] = 0; // broadcastable strides!
            }
            return ret;
        }

        /// <summary>
        /// Find working dimension to work on. Array style dependent. 
        /// </summary>
        /// <returns>Index of the last (numpy array style) or the first (ILNumericsV4 array style) non singleton dimension or 0.</returns>
        public uint WorkingDimension() {
            if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                for (uint i = 0; i < NumberOfDimensions; i++) {
                    if (this[i] != 1) return i;
                }
               return 0;
            } else {
                // numpy style: find the first non-singleton from the end
                for (uint i = NumberOfDimensions; i --> 0; ) {
                    if (this[i] != 1) return i;
                }
                return (uint)Math.Max(0, (int)NumberOfDimensions - 1);
            }
        }
        /// <summary>
        /// Create a new BSD with a rotated version of the dimensions of this size descriptor.
        /// </summary>
        /// <param name="shift">Number of dimensions to shift (rotate) this size's dimensions. 
        /// Rotation is done to the left.</param>
        /// <param name="outBSD">[Output] Reference to a bsd array used to store the shifted version of this BSD. <paramref name="outBSD"/> may point to the same instance as the BSD stored within this <see cref="Size"/>, in which case this size will be altered!.</param>
        /// <returns>Buffer Size Descriptor with rotated dimension order and the same dimension 
        /// lengths as this size's descriptor.</returns>
        /// <remarks>The functions returns a BSD using the provided <paramref name="outBSD"/> array, 
        /// having the same dimensions than this size descriptor. The order of dimensions of the returned BSD 
        /// is rotated to the left by <paramref name="shift"/>. 
        /// <para>This function is prepared to work on arbitrarily strided size descriptors and on the _same instance_ (inplace) hold by this size, <see cref="Size.GetBSD(bool)"/>.</para>
        /// <para>The provided array <paramref name="outBSD"/> must be of the same length than the 
        /// size descriptor stored in this <see cref="Size"/>.</para>
        /// <para><see cref="GetShifted(int, long*)"/> copies the incoming BSD unchanged on scalars and 1-dimensional vectors (NOP).</para>
        /// </remarks>
        public void GetShifted(int shift, long* outBSD) {
            System.Diagnostics.Debug.Assert(outBSD != null);
            if (NumberOfDimensions < 2) {
                if (outBSD != m_descriptor) {
                    outBSD[0] = m_descriptor[0];
                    outBSD[1] = m_descriptor[1];
                    outBSD[2] = m_descriptor[2];
                    if (NumberOfDimensions > 0) {
                        outBSD[3] = m_descriptor[3];
                        outBSD[4] = m_descriptor[4];
                    }
                }
                return;
            }
            int n = (int)NumberOfDimensions;
            shift %= n;
            if (shift < 0) shift += n;
            if (outBSD != m_descriptor) {
                outBSD[0] = m_descriptor[0];
                outBSD[1] = m_descriptor[1];
                outBSD[2] = m_descriptor[2];

                int id;
                for (int d = 0; d < n; d++) {
                    id = (d + shift) % n;
                    outBSD[3 + d] = m_descriptor[3 + id];
                    outBSD[3 + n + d] = m_descriptor[3 + n + id];
                }
            } else {
                // inplace rotate dimensions and strides only


                // 0 - 0 1 2 3 4 5 6 7 
                // 1 - 3 1 2 0 4 5 6 7 
                // 2 - 3 4 2 0 1 5 6 7 
                // 3 - 3 4 5 0 1 2 6 7 
                // 4 - 3 4 5 6 1 2 0 7 
                // 5 - 3 4 5 6 7 2 0 1
                // 6 - 3 4 5 6 7 0 0 1 ? // rotate rest of length by n - (length % n) = 1, start by: 2 -> store 
                // 7 - 3 4 5 6 7 0 1 1 
                // 8 - 3 4 5 6 7 0 1 2 <-

                // 0 - 0 1 2 3 4 5 6 7 
                // 1 - 2 1 0 3 4 5 6 7 
                // 2 - 2 3 0 1 4 5 6 7 
                // 2 - 2 3 4 1 0 5 6 7 
                // 2 - 2 3 4 5 0 1 6 7 
                // 2 - 2 3 4 5 6 1 0 7 
                // 2 - 2 3 4 5 6 7 0 1 

                // 0 - 0 1 2 3 4 5 6 
                // 1 - 3 1 2 0 4 5 6 
                // 2 - 3 4 2 0 1 5 6 
                // 3 - 3 4 5 0 1 2 6 
                // 4 - 3 4 5 6 1 2 0 
                // 5 - 3 4 5 6 0 2 1  <- lrot 2
                // 6 - 3 4 5 6 0 2 1  <- lrot 2


                lrot(m_descriptor, 3, n, shift);
                lrot(m_descriptor, 3 + n, n, shift);
                m_flags = 0;
            }
#if DEBUG
            // CheckSizeBroadcastableStrides(this);  fails on partial segments, T_get:DoSize
#endif
        }

        /// <summary>
        /// Permutes the dimension of an existing BSD.
        /// </summary>
        /// <param name="outBSD">Predefined BSD.</param>
        /// <param name="order">Array with new dimension indices order. Each index must be contained exactly once.</param>
        /// <param name="orderLen">Number of entries in <paramref name="order"/>. This must be equal to <see cref="NumberOfDimensions"/>.</param>
        /// <exception cref="ArgumentException"> if <paramref name="orderLen"/> is not the same as <see cref="NumberOfDimensions"/>, 
        /// if any existing dimension index of this <see cref="Size"/> is missing or multiple times defined in <paramref name="order"/>, 
        /// or if any provided pointer is NULL or if <paramref name="outBSD"/> points to the same memory as this Size' BSD.</exception>
        public unsafe void GetPermuted(long* outBSD, uint* order, uint orderLen) {
            if (outBSD == null || order == null || outBSD == m_descriptor) { // exactly, outBSD may overlap with m_descriptor anyways, even after this check! Assuming good mindness here.
                throw new ArgumentException("outBSD and order must not be null.");
            }
            if (orderLen != NumberOfDimensions) {
                throw new ArgumentException("orderLen must be equal to NumberOfDimensions.");
            }

            uint* checkUnique = stackalloc uint[(int)orderLen];
            for (int i = 0; i < orderLen; i++) {
                checkUnique[i] = uint.MaxValue; 
            }
            for (int i = 0; i < orderLen; i++) {
                var c = order[i];
                if (c < 0 || c >= orderLen || checkUnique[c] != uint.MaxValue) {
                    throw new ArgumentException($"Invalid new dimension order: the dimension index provided ({c}) at position {i} is either out of range or a duplicate."); 
                }
                checkUnique[c] = 1; 
                outBSD[3 + i] = m_descriptor[3 + c]; 
                outBSD[3 + orderLen + i] = m_descriptor[3 + orderLen + c];
            }
            outBSD[0] = m_descriptor[0];
            outBSD[1] = m_descriptor[1];
            outBSD[2] = m_descriptor[2]; 
        }
        /// <summary>
        /// Swaps (reverses) the dimensions of this size descriptor inplace. Keep this internal to retain API immutability!
        /// </summary>
        internal unsafe void SwapDimensions() {
            uint ndims = NumberOfDimensions; 
            long* s = m_descriptor + 3;
            long* e = m_descriptor + ndims + 2; 
            while (s < e) {
                // swap dims
                long d = *s;
                *s = *e;
                *e = d;
                // swap strides
                d = s[ndims];
                s[ndims] = e[ndims];
                e[ndims] = d;
                s++; e--; 
            }
            m_flags = 0; 
        }
        /// <summary>
        /// Gets the element index of the last element addressed by this size descriptor. 
        /// </summary>
        /// <returns>Element span.</returns>
        /// <remarks><para>The value computed indicates the size of memory region required to store all elements of 
        /// an array according to this size descriptor. Basically, the value indicates the element position where the 
        /// last (highest address) element is stored in the 1-dimensional array / memory region. Since no negative 
        /// indices are allowed, the 'last' element is the one with index tupels corresponding to the value of "dimension length - 1"
        /// for each dimension. The value returned will simply be the sum of such tuple items.</para>
        /// <para>This value is useful to 1) provide sufficiently large <see cref="System.Array"/> for exporting and copying 
        /// of elements in arbitray storage layouts, and 2) to determine whether a certain storage layout stores the elements 
        /// continously in memory (for cache performant iteration).</para>
        /// <para>The storage layout is continous, if and only if the value returned is equal to <see cref="NumberOfElements"/> minus 1.</para>
        /// </remarks>
        public long GetElementSpan() {
            return GetElementSpan(m_descriptor);
        }

        internal bool IsBroadcastableTo(Size s) {
            var ndims = Math.Min(NumberOfDimensions, s.NumberOfDimensions);
            for (uint i = 0; i < ndims; i++) {
                var si = s[i];
                var mi = this[i];
                if (si != mi && mi != 1) {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Unidirectional broadcasting check. 
        /// </summary>
        /// <param name="bsd"></param>
        /// <returns></returns>
        internal unsafe bool IsBroadcastableTo(long* bsd) {
            var ndims = Math.Min(NumberOfDimensions, bsd[0]);
            uint i = 0;
            for (; i < ndims; i++) {
                var si = bsd[3 + i];
                var mi = this[i];
                if (si != mi && mi != 1) {
                    return false;
                }
            }
            // make sure trailing dimensions are singletons only!
            while (m_descriptor[3 + i] == 1 && i++ < NumberOfDimensions) ;
            return i >= NumberOfDimensions;
        }
        /// <summary>
        /// Checks if this array shape is broadcastable to the other array shape, aligning the last dimensions (numpy style). 
        /// </summary>
        /// <param name="theirDims"></param>
        /// <param name="ndims"></param>
        /// <exception cref="ArgumentException">if this size is not broadcastable to the given size.</exception>
        internal unsafe void CheckIsBroadcastableTo_np(long* theirDims, uint ndims) {
            if (NumberOfDimensions == 0) return; // scalars are always broadcastable
            var sharedNDims = Math.Min(NumberOfDimensions, ndims);
            theirDims = theirDims + ndims - sharedNDims;
            var mydims = m_descriptor + 3;
            var myNDims = NumberOfDimensions;
            while (mydims[0] == 1 && myNDims > ndims) {
                mydims++; myNDims--;
            }
            if (myNDims > ndims) {
                throw new ArgumentException($"The size {ToString()} is not broadcastable to the output size [{Core.Global.Helper.dims2string(theirDims + sharedNDims - ndims, ndims)}] (ArrayStyle=numpy).");
            }
            for (uint i = 0; i < sharedNDims; i++) {
                var si = theirDims[i];
                var mi = mydims[i];
                if (si != mi && mi != 1) {
                    throw new ArgumentException($"The size {ToString()} is not broadcastable to the output size [{Core.Global.Helper.dims2string(theirDims + sharedNDims - ndims, ndims)}] (ArrayStyle=numpy).");
                }
            }
        }

#if DEBUG
        /// <summary>
        /// Check to make sure that all singleton dimensions have 0 strides assigned.
        /// </summary>
        /// <param name="size">Size of check.</param>
        internal static unsafe void CheckSizeBroadcastableStrides(Size size) {
            var bsd = size.GetBSD();
            for (int i = 0; i < bsd[0]; i++) {
                Debug.Assert(bsd[i + 3] != 1 || bsd[3 + bsd[0] + i] == 0); 
            }
        }
#endif


        /// <summary>
        /// Determines if this size if broadcastable to the range specified by <paramref name="iterators"/>. Not the other way around!
        /// </summary>
        /// <param name="iterators"></param>
        /// <param name="nIterDims"></param>
        /// <returns></returns>
        /// <remarks>This is used by SetRangeML.</remarks>
        internal bool IsBroadcastableTo(IIndexIterator[] iterators, uint nIterDims) {
            var ndims = Math.Min(NumberOfDimensions, nIterDims);

            uint i = 0;
            for (; i < nIterDims; i++) {
                var si = iterators[i].GetLength();
                var mi = this[i];
                if (si != mi && mi != 1) {
                    return false;
                }
            }
            for (; i < NumberOfDimensions; i++) {
                // if iterators do not address ALL this dimensions trailing dims must be singleton (or "empty" - but not 0!)
                if (this[i] != 1) {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Gets the element index of the last element addressed by a size descriptor. 
        /// </summary>
        /// <returns>Element span.</returns>
        /// <remarks><para>The value computed indicates the size of memory region required to store all elements of 
        /// an array according to the size descriptor <paramref name="bsd"/>. Basically, the value indicates the element position where the 
        /// last (highest address) element is stored in the 1-dimensional array / memory region. Since no negative 
        /// indices are allowed, the 'last' element is the one with index tupels corresponding to the value of "dimension length - 1"
        /// for each dimension. The value returned will simply be the sum of such tuple items factored with the corresponding strides.</para>
        /// <para>This value is useful to 1) provide sufficiently large <see cref="System.Array"/> for exporting and copying 
        /// of elements in arbitray storage layouts, and 2) to determine whether a certain storage layout stores the elements 
        /// continously in memory (for cache performant iteration).</para>
        /// <para>The storage layout is continous, if and only if the value returned is equal to <see cref="NumberOfElements"/> minus 1.</para>
        /// </remarks>
        public static long GetElementSpan(long* bsd) {
            uint ndims = (uint)bsd[0];
            long ret = 0;
            for (int i = 0; i < ndims; i++) {
                long len = bsd[3 + i];
                if (len == 0) return 0;
                ret += (len - 1) * bsd[3 + ndims + i];
            }
            return ret;
        }

        /// <summary>
        /// Retrieves the sequential index into an array stored according to this size descriptor 
        /// based on the provided index array <paramref name="d"/>. 
        /// </summary>
        /// <param name="d">System.Array with indices into the dimensions of this array.</param>
        /// <returns>Sequential index into the array when stored as 1D array of storage order according to this size descriptor.</returns>
        /// <remarks>
        /// <para>If the array addressed by this size has less dimensions than addressed by <paramref name="d"/>, trailing 
        /// indices (i.e.: such indices dealing with a dimension whose index is higher than existing in the array)
        /// correspond to 'virtual' or singleton dimensions. Those indices must address the 0-th element of the 
        /// virtual dimension. Hence its value must be 0.</para>
        /// <para>Conversely, if the array addressed by this size stores more dimensions than addressed by <paramref name="d"/>
        /// the value of the last index from <paramref name="d"/> may exceed the length of the corresponding dimension. In this 
        /// case the sequential index to be returned is computed by subsequently merging trailing dimensions and folding 
        /// the superflous modulus of the value of 'd[{last}]' to the next dimension, correspondingly. This process 
        /// is repeated until either the number of dimensions is reached or the modified new value of 'd[{last}]' 
        /// reaches 0.</para>
        /// <para>This function recognizes arbitrarily strided size objects.</para>
        /// <para>This function was marked deprecated in version 5. Calls to this function forward to the new uint indexing function <see cref="GetSeqIndex(uint[])"/>. 
        /// Consider using the 'uint[]' overload directly in order to make your code more stable and faster!</para>
        /// </remarks>
        /// <exception cref="IndexOutOfRangeException"> if any of the indices in <paramref name="d"/> (except the 
        /// last index stored, see above) is equal or greater than the length of its corresponding dimension.</exception>
        /// <exception cref="IndexOutOfRangeException"> the resulting index points to a non-existing element.</exception>
        /// <exception cref="IndexOutOfRangeException"> if any of the indices in <paramref name="d"/> are negative.</exception>
        [Obsolete("Use uint/ulong indexing instead!")]
        internal uint GetSeqIndex(int[] d) {
            /* 
             * * Must allow virtual trailing singleton dimensions. 
             * * Must recognize merged dimensions. 
             * * Must throw on IndexOutOfRange. 
             * * - all but last dim spec in range
             * * - ret: overall element count
             * * DELETED: Must recognize arbitrary strides. 
             * * Must be efficient! 
             */

            if (d == null || d.Length == 0) {
                throw new ArgumentException("Indices must be provided as a non-null, non-empty System.Array.");
            }
            return GetSeqIndex(Array.ConvertAll(d, i => {
                if (i < 0) {
                    throw new IndexOutOfRangeException("Indices must be positive. Consider using 'uint' indexing instead - it will improve your design and speed things up!");
                }
                return (uint)i;
            }));
        }
#endregion

        /// <summary>
        /// Get the spacing between elements in the dimension <paramref name="dim"/>.
        /// </summary>
        /// <param name="dim">0 based index of the dimension.</param>
        /// <returns>The stride for the dimension addressed by <paramref name="dim"/>.</returns>
        /// <remarks><para>Strides in ILNumerics arrays are always positive.</para>
        /// <para>For <paramref name="dim"/> larger or equal to <see cref="NumberOfDimensions"/> the 
        /// function returns <see cref="NumberOfElements"/> which is the correct value for <i>column-major storage</i>. 
        /// For non-column major storages the strides of dimensions outside the range of the 
        /// number of dimensions of this size descriptor are not defined.</para>
        /// </remarks>
        /// <seealso cref="NumberOfDimensions"/>
        /// <seealso cref="StorageOrders"/>
        /// <seealso cref="Size.StorageOrder"/>
        public long GetStride(uint dim) {
            // returns Number of Elements for dim >= ndims
            //return (long)m_descriptor[(dim < NumberOfDimensions) ? 3 + NumberOfDimensions + dim : 1]; 

            // returns 0 for trailing singleton dimensions
            return (dim < NumberOfDimensions && (long)m_descriptor[3 + dim] != 1) ? (long)m_descriptor[3 + NumberOfDimensions + dim] : 0;
        }

        /// <summary>
        /// Gives strides of next non-singleton dimension. Optimized for ML, column-maj. subarray operations.
        /// </summary>
        /// <param name="dim"></param>
        /// <returns>next stored non-singular stride or 0. </returns>
        /// <remarks></remarks>
        internal long GetStride4MLlastDimExpansion(uint dim) {
            while (dim < NumberOfDimensions && (long)m_descriptor[3 + dim] == 1) {
                dim++;
            }
            return GetStride(dim);
        }

        #region implicit comparison operators
        /// <summary>
        /// 'Not equal' operator on two instances of <see cref="Size"/>
        /// </summary>
        /// <param name="s1">The first size object.</param>
        /// <param name="s2">The second size object.</param>
        /// <returns>True, if both instances do not have the same <b>shape</b>.</returns>
        /// <remarks>The operator is an alias for <code>!s1.Equals(s2)</code>
        /// <para>If either one of <paramref name="s1"/> or <paramref name="s2"/> is null, the operator returns false.</para>
        /// </remarks>
        public static bool operator !=(Size s1, Size s2) {
            if (object.Equals(s1, null)) return false; // I wonder, how this would be possible to happen ? 
            return !s1.IsSameShape(s2);
        }
        /// <summary>
        /// 'Equal' operator on two instances of <see cref="Size"/>
        /// </summary>
        /// <param name="s1">The first size object.</param>
        /// <param name="s2">The second size object.</param>
        /// <returns>True, if both instances have the same <b>shape</b>.</returns>
        /// <remarks>The operator is an alias for <code>s1.Equals(s2)</code>.
        /// <para>If either one of <paramref name="s1"/> or <paramref name="s2"/> is null, the operator returns false.</para></remarks>
        public static bool operator ==(Size s1, Size s2) {
            if (object.Equals(s1, null)) return false; // I wonder, how this would be possible to happen ? 
            return s1.IsSameShape(s2);
        }
        /// <summary>
        /// 'Not equal' operator, compares this size with a shape vector.
        /// </summary>
        /// <param name="s1">The first size object.</param>
        /// <param name="s2">The shape vector.</param>
        /// <returns>True, if both sizes do not have the same <b>shape</b>.</returns>
        /// <remarks>The operator is an alias for <code>!s1.IsSameShape(s2)</code>
        /// <para>If either one of <paramref name="s1"/> or <paramref name="s2"/> is null, the operator returns false.</para>
        /// </remarks>
        [Obsolete("In order to compare the size of an array A with a shape vector s, use A.shape.Equals(s), or: A.S.IsSameShape(s)!")]
        public static bool operator !=(Size s1, InArray<long> s2) {
            using (Scope.Enter(s2)) {
                if (object.Equals(s1, null)) return false; // I wonder, how this would be possible to happen ? 
                return !s1.IsSameShape(s2);
            }
        }
        /// <summary>
        /// 'Equal' operator, compares this size with a shape vector.
        /// </summary>
        /// <param name="s1">The size object.</param>
        /// <param name="s2">The shape vector.</param>
        /// <returns>True, if both sizes have the same <b>shape</b>.</returns>
        /// <remarks>The operator is an alias for <code>s1.IsSameShape(s2)</code>.
        /// <para>If either one of <paramref name="s1"/> or <paramref name="s2"/> is null, the operator returns false.</para></remarks>
        [Obsolete("In order to compare the size of an array A with a shape vector s, use A.shape.Equals(s), or: A.S.IsSameShape(s)!")]
        public static bool operator ==(Size s1, InArray<long> s2) {
            using (Scope.Enter(s2)) {
                if (object.Equals(s1, null)) return false; // I wonder, how this would be possible to happen ? 
                return s1.IsSameShape(s2);
            }
        }

        /// <summary>
        /// Exact equal, compares number, lengths and strides of all dimensions, base offset. 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) {
            var other = obj as Size; 
            if (object.ReferenceEquals(other, null)) {
                return base.Equals(obj); 
            }
            // compares all of the BSDs 
            unsafe {
                var my = GetBSD(false);
                var ot = other.GetBSD(false);
                if (my[0] != ot[0]) { return false; }
                if (my[1] != ot[1]) { return false; } // most unequal are now found ...
                if (my[2] != ot[2]) { return false; }
                for (int i = 0; i < my[0]; i++) {
                    if (my[3 + i] != ot[3 + i] || my[3 + i + my[0]] != ot[3 + i + my[0]]) return false; 
                }
                return true; 
            }
        }
        #endregion

#if OBSOLETE
        /// <summary>
        /// Consolidate the elements of array <paramref name="src"/> as described by the internal 
        /// BSD of this size to the output array <paramref name="dest"/>. 
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="src">Source array with elements to get consolidated (copied).</param>
        /// <param name="baseOffset">Initial base offset into <paramref name="src"/>.</param>
        /// <param name="dest">Destination array, will receive the consolidated elements.</param>
        /// <param name="destOrder">[Optional] The storage order <paramref name="dest"/> represents on output. Default: ColumnMajor order.</param>
        /// <remarks><para>The data from <paramref name="src"/> are copied into the predefined output 
        /// array <paramref name="dest"/>. Elements from src are read according to this size descriptors 
        /// information: dimension lengths and strides and base offset. The copied elements in <paramref name="dest"/> 
        /// will start at index 0, any base offset from this size descriptor is omitted in the result.</para>
        /// <para>The output is provided in one of the two major storage formats: column major or row major. 
        /// The parameter <paramref name="destOrder"/> determines which one is used.</para></remarks>
        /// <exception cref="ArgumentException">if <paramref name="destOrder"/> is neither <see cref="StorageOrders.ColumnMajor"/>
        /// not <see cref="StorageOrders.RowMajor"/>.</exception>
        public unsafe void Consolidate<T>(T[] src, uint baseOffset, T[] dest, StorageOrders destOrder = StorageOrders.ColumnMajor) {
            // TODO: parallelize Consolidate() function

            throw new NotImplementedException("Check: row major is not correctly iterated? How is arbitrary layout handled? stackalloc does not clear the buffer!!");

            System.Diagnostics.Debug.Assert(dest != null && (long)dest.Length >= NumberOfElements);
            System.Diagnostics.Debug.Assert(src != null && (long)src.Length >= NumberOfElements);
            System.Diagnostics.Debug.Assert(!object.ReferenceEquals(src, dest));
            if (destOrder != StorageOrders.ColumnMajor && destOrder != StorageOrders.RowMajor) {
                throw new ArgumentException("Unsupported storage order: 'unknown'");
            }
            if (destOrder == StorageOrder) {
                Array.Copy(src, m_descriptor[2], dest, 0, (long)NumberOfElements);
            }

            var n = NumberOfDimensions;
            var curpos = stackalloc uint[(int)n];

            // We compute the higher dimension indices only once and _reuse_ their sum along the elements 
            // in the leading dimension of ret. 
            if (NumberOfElements > 0) {
                uint highdims = baseOffset, ii = 0;
                uint leadDim = (destOrder == StorageOrders.RowMajor) ? n - 1 : 0;
                uint l = (uint)m_descriptor[3 + leadDim], sStride = (uint)m_descriptor[3 + n + leadDim];

                do {
                    uint srcId = highdims;
                    uint ind = 0;
                    for (; ind < l - 4; ind += 4) {
                        dest[ii++] = src[srcId]; srcId += sStride;
                        dest[ii++] = src[srcId]; srcId += sStride;
                        dest[ii++] = src[srcId]; srcId += sStride;
                        dest[ii++] = src[srcId]; srcId += sStride;
                    };
                    for (; ind < l; ind++) {
                        dest[ii++] = src[srcId]; srcId += sStride;
                    }
                    // increase higher dims 
                    uint d = (leadDim + 1) % n;
                    while (d != leadDim) {
                        if (curpos[d] < (uint)m_descriptor[d + 3] - 1) {
                            highdims += (uint)m_descriptor[d + 3 + n];
                            curpos[d]++;
                            break;
                        } else {
                            curpos[d] = 0;
                            highdims -= (uint)m_descriptor[d + 3 + n] * ((uint)m_descriptor[3 + d] - 1);
                            d = (d + 1) % n;
                        }
                    }
                    if (d == n) {
                        break;
                    }
                }
                while (true);
            }
        }
#endif

        #region private helpers
        /// <summary>
        /// Tests the BSD for column-/ row-major addressing storage format and continous storage.
        /// </summary>
        /// <param name="bsd">The size descriptor.</param>
        /// <param name="flags">Integer used to store the storage format and continous property in a bit pattern.</param>
        internal static void CheckFlags(long* bsd, ref uint flags) {
            if ((uint)bsd[1] == 1) {
                // scalar early exit
                flags = 0x80000001;
                return;
            }
            ulong stride = 1u;
            flags = 0;
            var nrDims = (uint)bsd[0];  // this loads the full(hopefully) bsd into the cache
            for (uint i = 0; i < nrDims; i++) {
                if ((ulong)bsd[3 + i] > 1 && (ulong)bsd[nrDims + 3 + i] != stride) { // ignore singleton dimensions! 
#region check row major
                    stride = 1;
                    for (i = nrDims; i-- > 0;) {
                        if ((ulong)bsd[3 + i] > 1 && (ulong)bsd[nrDims + 3 + i] != stride) {   // running backwards here should not hurt too much, since we have the bsd in the cache.
                            flags |= (uint)StorageOrders.Other;
                            if ((ulong)bsd[1] < 2 || GetElementSpan(bsd) == bsd[1] - 1) {
                                flags |= CONT_FLAG;      // set continous flag
                            } else {
                                flags &= ~CONT_FLAG;  // delete continous flag
                            }
                            return;
                        }
                        stride *= (uint)bsd[3 + i];
                    }
#endregion
                    flags |= (uint)StorageOrders.RowMajor;
                    flags |= CONT_FLAG;
                    return;

                }
                stride *= (ulong)bsd[3 + i];
            }
            flags |= (uint)StorageOrders.ColumnMajor;
            flags |= CONT_FLAG;
        }


        //private static int[] convert2int(BaseArray[] dimensions) {
        //    using (Scope.Enter(dimensions)) {
        //        if (dimensions == null || dimensions.Length == 0) {
        //            return new int[2];
        //        }
        //        int[] ret = new int[dimensions.Length];
        //        if (dimensions.Length > 1) {
        //            for (int i = 0; i < ret.Length; i++) {
        //                BaseArray A = dimensions[i];
        //                if (object.Equals(A, null) || !A.IsScalar || !A.IsNumeric) {
        //                    throw new ArgumentException("dimension specifiers must be numeric scalars");
        //                }
        //                ret[i] = (int)ILMathInternal.todouble(A).GetValue(0);
        //            }
        //        } else {
        //            // exactly one array was given, it must be a numeric int vector with the dim specification 
        //            BaseArray A = dimensions[0];
        //            if (object.Equals(A, null) || !A.IsVector || !A.IsNumeric) {
        //                throw new ArgumentException("Invalid dimension specification. Numeric vector expected.");
        //            }
        //            Array<int> Aint = ILMathInternal.toint32(A);
        //            Aint.ExportValues(ref ret);
        //        }
        //        return ret;
        //    }
        //}

        ///// <summary>
        ///// Return a new Size object with the same dimension number and -lenghts, column major 
        ///// striding, having any base offset removed.
        ///// </summary>
        ///// <returns>Column major size descriptor with the same dimension lengths as new object.</returns>
        //internal Size AsColumnMajor() {
        //    long[] ret = new long[NumberOfDimensions * 2 + 3];
        //    //Array.Copy(m_descriptor, ret, ret.Length);
        //    ret[0] = m_descriptor[0];
        //    ret[1] = m_descriptor[1];
        //    ret[2] = (0);
        //    uint stride = 1, nrDims = (uint)NumberOfDimensions; 
        //    for (int i = 0; i < nrDims; i++) {
        //        uint s = (uint)m_descriptor[3 + i]; 
        //        ret[3 + i] = (s);
        //        ret[3 + i + nrDims] = (stride);
        //        stride *= s; 
        //    }
        //    return new Size(ret, true); 
        //}


        private static int gcd(int a, int b) {
            if (b == 0)
                return a;
            else
                return gcd(b, a % b);
        }
        //Ref: https://stackoverflow.com/questions/24221279/juggling-algorithm
        /// <summary>
        /// Left rotate array inplace. O(N) computational effort. O(1) memory required. 
        /// </summary>
        /// <param name="arr">The array</param>
        /// <param name="offs">The index of the starting element.</param>
        /// <param name="n">The number of consequtive elements to rotate, starting with <paramref name="offs"/>.</param>
        /// <param name="d">Rotation distance.</param>
        internal static void lrot(long* arr, int offs, int n, int d) {
            if (n == 0 || d % n == 0) return;
            int i, j, k, cgd = gcd(d, n);

            for (i = 0; i < cgd; i++) {
                /* move i-th values of blocks */
                long temp = arr[i + offs];
                j = i;
                while (true) {
                    k = j + d;
                    if (k >= n)
                        k = k - n;
                    if (k == i)
                        break;
                    arr[j + offs] = arr[k + offs];
                    j = k;
                }
                arr[j + offs] = temp;
            }
        }
#endregion

    }
}
