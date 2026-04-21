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
#if NETFRAMEWORK
#endif
    public partial class CellStorage : BaseStorage<BaseArray, Cell, InCell, OutCell, Cell, CellStorage>, IStorage {

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool m_fromImplicitCast;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal bool FromImplicitCast {
            get { return m_fromImplicitCast; }
            set { m_fromImplicitCast = value; }
        }

        public CellStorage() : base() {
            var synch = new object();
            m_array = new Cell(this, synch);
            m_inArray = new InCell(this);
            m_outArray = new OutCell(this, synch);
            m_retArray = new Cell(this, synch);
        }
        /// <summary>
        /// Create new storage for the given size. Elements are uninitialized! 
        /// </summary>
        /// <param name="size">Size for the new storage.</param>
        /// <returns>Storage, capable of storing an array of a size according to <paramref name="size"/>.</returns>
        /// <remarks><para>This will get the storage from the storage pool if possible. A new storage 
        /// is returned only if no matchine storage was available in the pool.</para>
        /// <para>The storage is created as column major storage. Only the number and lengths of <paramref name="size"/>
        /// are considered. Striding is recreated (column major) and base offset will be 0.</para></remarks>
        public static CellStorage Create(Size size) {
            var ret = CellStorage.Create();
            ret.Handles[0] = DeviceManager.GetDevice(0).New<IStorage>((ulong)size.NumberOfElements);
            ret.Size.SetDimensionLengths(size);
            return ret;
        }
        /// <summary>
        /// Extract (not so) quick scalar from this cell storage.
        /// </summary>
        /// <param name="v">BaseOffset for the new scalar.</param>
        /// <param name="fromRetArray">ignored</param>
        /// <param name="outStorage">[Optional] when set and not null the storage to be used as return value.</param>
        /// <returns>New storage referencing a clone made from a single element of this storage.</returns>
        internal override CellStorage Create(long v, bool fromRetArray = false, CellStorage outStorage = null) {
            CellStorage ret = outStorage ?? Create();
            ret.S.SetScalar(0, Settings.MinNumberOfArrayDimensions);
            ret.Handles[0] = New(1, true);
            var arr = (ret.Handles[0] as ManagedHostHandle<IStorage>).HostArray;
            ret.FromImplicitCast = true; // causes cell-'boxed' values to get unwrapped in indexing assignments.
            replace(ref arr[0], ref (Handles[0] as ManagedHostHandle<IStorage>).HostArray[v]); 
            return ret;
        }


        /// <summary>
        /// Create a new memory handle of <paramref name="elementLength"/> elements of the <see cref="IStorage"/> element type. 
        /// </summary>
        /// <param name="elementLength">Number of elements for the new handle.</param>
        /// <param name="clear">[optional] clear the new memory. Default: false.</param>
        /// <returns>The new handle according to the storage's internal element type.</returns>
        /// <seealso cref="LogicalStorage.New(ulong, bool)"/>
        /// <seealso cref="CellStorage.New(ulong, bool)"/>
        
        protected internal override MemoryHandle New(ulong elementLength, bool clear = true) {
            return DeviceManager.GetDevice(0).New<IStorage>(elementLength, true); // forces to clear any old garbage cell elements. They likely point to currently active storages! :) 
        }

        #region overriding BaseStorage

        #region Reshape
        /// <summary>
        /// Prepares the return storage. Commonly, this copies the values if needed or gives a self-reference. CellStorage overrides it to ensure proper cloning.
        /// </summary>
        /// <param name="order"></param>
        /// <param name="outStorage"></param>
        /// <returns></returns>
        protected override CellStorage CreateSelf4Reshape(StorageOrders? order, CellStorage outStorage = null) {
            CellStorage ret;

            // must copy first
            ret = outStorage ?? Create();
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<IStorage>((ulong)Size.NumberOfElements);
            ret.Handles.CurrentDeviceIdx = 0;
            CopyTo(ret.Handles[0], null, order);

            return ret;
        }

        #endregion

        #region Get_/SetValueSeq
        /// <summary>
        /// Replaces a single element of this cell storage with a clone of <paramref name="value"/>.
        /// </summary>
        /// <param name="value">Array to be cloned and stored. This method does not release <paramref name="value"/>!</param>
        /// <param name="byteIdx">BYTE offset of the storage element position. This will be transformed into an element offset in the method.</param>
        
        internal override void SetValueSeq(BaseArray value, long byteIdx) {

            System.Diagnostics.Debug.Assert(byteIdx <= (Size.GetElementSpan() + Size.BaseOffset) * SizeOfT);
            var ret = this; 
            if (Handles.ReferenceCount > 1) {
                DetachBufferSetInplace();
            }

            // The incoming value is a BaseArray, it must be cloned before storing into this cell.  
            System.Diagnostics.Debug.Assert(value is BaseArray || Equals(value, null));

            var managedHostHandle = Handles[0] as ManagedHostHandle<IStorage>;
            var idx = byteIdx / SizeOfT;
            // make a clone of the incoming value
            IStorage iStorage = value?.GetClonedStorage();

            // If there is another array at this position: release it!  -- don't release before value.GetCloneStorage() above! value could be me: C[2] = C !
            managedHostHandle.HostArray[idx]?.Release();

            managedHostHandle.HostArray[idx] = iStorage;
            // storing the clone means: using it -> ref count increase, but GetCloneStorage() return refCount 1! 
            //iStorage?.Retain();
        }

        /// <summary>
        /// Retrieves a clone of the cell content at sequential position <paramref name="offset"/>.
        /// </summary>
        /// <param name="offset">Element position as element offset relative to the base address, including strides.</param>
        /// <returns>Clone of the element as position <paramref name="offset"/> or null.</returns>
        
        internal override BaseArray GetValueSeq(long offset) {
            var arr = (Handles[0] as ManagedHostHandle<IStorage>)?.HostArray;
            return arr[offset]?.GetBaseArrayClone();
        }
        #endregion  

        public override string ShortInfo() {
            return ShortInfo(true, true, false, false);
        }

        #region helper overrides for SetRange_ML
        /// <summary>
        /// Clones cell values from <paramref name="value"/>, regardless of shape(!) to the parts of this storage as determined by <paramref name="outBSD"/>. No broadcasting! 
        /// </summary>
        /// <param name="value">src values of the cell. The right side of the assignment.</param>
        /// <param name="outBSD">set of BSD describing the parts to be overwritten.</param>
        
        protected unsafe override void WriteTo_ML_BSDIter_T(CellStorage value, long* outBSD) {

            // This handles empty storages! 

            // value must have the same number of elements as the range addressed by iterators! 

            var valueIter = value.S.Iterator(order: StorageOrders.ColumnMajor).GetEnumerator();
            if (!valueIter.MoveNext())
                return;  // nothing to do

            uint ndims = (uint)outBSD[0]; // number of dimensions addressed by the user
            if (outBSD[1] == 0) return;

            long higdims = outBSD[2]; // this includes the initial base offset already!

            IStorage[] pOut = (Handles[0] as ManagedHostHandle<IStorage>).HostArray;
            IStorage[] pIn = (value.Handles[0] as ManagedHostHandle<IStorage>).HostArray;
            long stride0 = outBSD[3 + ndims];
            long dimLen0 = outBSD[3];
            long* cur = stackalloc long[(int)ndims];
            for (int i = 0; i < ndims; i++) {
                cur[i] = 0;
            }
            while (true) {
                long cur0 = 0;
                while (cur0 < dimLen0) {
                    pOut[higdims + cur0]?.Release(); 
                    var clone = pIn[valueIter.Current]?.Clone();
                    pOut[higdims + cur0 * stride0] = clone;
                    valueIter.MoveNext();
                    cur0++;
                }

                // increase higher dims
                uint d = 1;
                while (d < ndims) {
                    if (cur[d] < outBSD[3 + d] - 1) {
                        higdims += outBSD[3 + ndims + d];
                        cur[d]++;
                        break;
                    } else {
                        higdims -= outBSD[3 + ndims + d] * cur[d];
                        cur[d] = 0;
                        d++;
                    }
                }
                if (d == ndims) return;
            }
        }
        
        protected override unsafe void WriteTo_ML_IterIter_T(CellStorage value, IIndexIterator[] iterators, uint ndimsOut) {

            // This handles empty storages! (by handling empty iterators)

            var valueIter = value.GetEnumerator(storageOrder: StorageOrders.ColumnMajor, dispose: false);
            if (!valueIter.MoveNext())
                return;  // nothing to do

            long stride0 = S.GetStride(0);
            var it0 = iterators[0];
            if (it0.GetLength() == 0) return;
            long higdims = S.BaseOffset;
            for (uint i = 1; i < ndimsOut; i++) {
                if (!iterators[i].MoveNext()) {
                    return; // nothing to do. Empty right side.
                }
                // OOR should not happen. Otherwise we would end up in the 'expand' path.
                // System.Diagnostics.Debug.Assert((ulong)val <= (ulong)iterators[i].GetLastDimensionIndex());
                // throw new IndexOutOfRangeException($"Index '{val}' exceeds the valid range of dimension {i}: 0 <= i < {iterators[i].GetLastDimensionIndex()}.");
                higdims += (iterators[i].Current * S.GetStride(i));
            }

            System.Diagnostics.Debug.Assert(Handles[0] is ManagedHostHandle<IStorage>);


            IStorage[] pOut = (Handles[0] as ManagedHostHandle<IStorage>).HostArray;

            while (true) {

                while (it0.MoveNext()) {

                    var outIDx = higdims + it0.Current * stride0;
                    pOut[outIDx]?.Release();
                    var newStor = valueIter.Current?.GetClonedStorage();
                    pOut[higdims + it0.Current * stride0] = newStor;
                    valueIter.MoveNext(); // assumes succeess (?!)

                };
                it0.Reset();

                // increase higher dims
                uint d = 1;
                while (d < ndimsOut) {
                    var itD = iterators[d];
                    var oldIdx = itD.Current;

                    if (itD.MoveNext()) {
                        var val = itD.Current;
                        higdims += (val - oldIdx) * S.GetStride(d);
                        break;
                    } else {
                        itD.Reset();
                        itD.MoveNext();  // assuming this succeeds
                        higdims += (itD.Current - oldIdx) * S.GetStride(d);
                        if (++d == ndimsOut) return;
                    }
                }
                if (d == ndimsOut) return; 
            }
        }

        #endregion

        /// <summary>
        /// Gives a shallow clone of this array: new array object, same memory. 
        /// </summary>
        /// <returns>Clone of this array as return type array.</returns>
        /// <see cref="BaseArray.IsOfType{ElementType}"/>
        BaseArray IStorage.GetBaseArrayClone() {
            var stor = Clone() as CellStorage;
            var ret = stor.m_retArray;  // no retain! Clone() returns ref count 1.
            return ret;
        }

        /// <summary>
        /// Creates a shallow copy of this storage. It copies the cells recursively but shares the memory of arrays in cell elements.
        /// </summary>
        /// <returns>Clone of this storage, sharing the memory handles with this storage.</returns>
        /// <remarks><para>The storage returned has an array reference count of 1. Reference
        /// counts for the (shared) handles are increased accordingly.</para>
        /// <para>Cells do not use asynch features currently. Thus, the asynch counter of the clone returned will be set to 1.</para></remarks>
        
        public override IStorage Clone() {
            var ret = base.Clone() as CellStorage; // gives a new cell storage, same handles, refcount: 1
            ret.DetachBufferSetInplace();
            ret.FromImplicitCast = FromImplicitCast;
            
            // Note: below detaching is redundant. DetachBifferSetInplace does already perform Clone on cell elements! 
            //var arr = (ret.Handles[0] as ManagedHostHandle<IStorage>).HostArray;
            //foreach (var ind in Size.Iterator()) {
            //    var cellElem = arr[ind];
            //    if (!Equals(cellElem, null)) {
            //        var clonedElem = cellElem.Clone();
            //        arr[ind] = clonedElem;
            //    }
            //}
            return ret;
        }

        
        public override void Assign(CellStorage value) {
            // Assign for cells does not work as for regular (value typed) arrays!
            // we need to create clones of the incoming values and assign them instead. 

            // First release all instances of IStorage in my cells before taking over a new storage. 
            if (!ReferenceEquals(Handles, value?.Handles)) {
                var arr = (Handles[0] as ManagedHostHandle<IStorage>).HostArray; 
                foreach (var i in Size.Iterator()) {
                    arr[i]?.Release();
                    arr[i] = null; 
                }
            }
            // base.Assign just copies the Size and reuses the buffer set. No need to create a clone! 

            base.Assign(value); // simple, straight, traditional Assign(). copies size and buffer set. 
        }

        /// <summary>
        /// Copy the data of this array to another memory region, specify element storage order for writing. 
        /// </summary>
        /// <param name="dest">Pointer to a memory region, large enough to store all elements of this array in the storage layout given by <paramref name="layout"/>.</param>
        /// <param name="outSize">[Output] On return the size descriptor holds the dimension lengths and strides according to the size of this array and the specified <paramref name="layout"/>.</param>
        /// <param name="layout">[Optional] The storage order used to write the elements to <paramref name="dest"/>. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <remarks><para><paramref name="outSize"/> can be <c>null</c> on entry in which case it will be ignored.</para>
        /// <para>If <paramref name="layout"/> is <c>null</c> or <see cref="StorageOrders.Other"/> the storage layout of the array 
        /// returned will be automatically determined based on the current storage layout: copying from continous storages will keep 
        /// the source storage layout (column- or row major layout). Copying from non-continous storages creates a storage according to 
        /// <see cref="Settings.DefaultStorageOrder"/>. </para>
        /// <para>If <paramref name="layout"/> is one of <see cref="StorageOrders.ColumnMajor"/> or <see cref="StorageOrders.RowMajor"/> the 
        /// elements are stored into <paramref name="dest"/> with this layout.</para>
        /// <para>Make sure that the memory region addressed by <paramref name="dest"/> is large enough, even if the current storage layout 
        /// corresponds to non-continously stored elements! Note further, that 
        /// any potentially existing holes in the element storage layout will not be cleared!</para>
        /// </remarks>
        
        internal override void CopyTo(MemoryHandle dest, Size outSize, StorageOrders? layout = null) {

            System.Diagnostics.Debug.Assert(Handles[0] is ManagedHostHandle<IStorage>);  
            Core.Functions.Builtin.CopyToOperators.CopyTo_Cell((Handles[0] as ManagedHostHandle<IStorage>).HostArray, Size,
                                                                (dest as ManagedHostHandle<IStorage>).HostArray, outSize, layout);

        }
        #endregion

        #region GetRange_ML - DimSpecs fast subarray

        internal override CellStorage GetRange_ML(DimSpec d0, bool fromRetT) {
            return GetRange_ML((BaseArray)d0, fromRetT); 
        }
        internal override CellStorage GetRange_ML(DimSpec d0, DimSpec d1, bool fromRetT) {
            return GetRange_ML((BaseArray)d0, d1, fromRetT);
        }
        internal override CellStorage GetRange_ML(DimSpec d0, DimSpec d1, DimSpec d2, bool fromRetT) {
            return GetRange_ML((BaseArray)d0, d1, d2, fromRetT);
        }
        internal override CellStorage GetRange_ML(DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3, bool fromRetT) {
            return GetRange_ML((BaseArray)d0, d1, d2, d3, fromRetT);
        }
        internal override CellStorage GetRange_ML(DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3, DimSpec d4, bool fromRetT) {
            return GetRange_ML((BaseArray)d0, d1, d2, d3, d4, fromRetT);
        }
        internal override CellStorage GetRange_ML(DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3, DimSpec d4, DimSpec d5, bool fromRetT) {
            return GetRange_ML((BaseArray)d0, d1, d2, d3, d4, d5, fromRetT);
        }
        internal override CellStorage GetRange_ML(DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3, DimSpec d4, DimSpec d5, DimSpec d6, bool fromRetT) {
            return GetRange_ML((BaseArray)d0, d1, d2, d3, d4, d5, d6, fromRetT);
        }
        internal override CellStorage GetRange_ML(DimSpec[] dims, bool fromRetT, uint? len) {

            BaseArray[] ba = new BaseArray[Math.Min(len ?? Size.MaxNumberOfDimensions, (Equals(dims, null) ? 0 : dims.Length))];
            for (int i = 0; i < ba.Length; i++) {
                ba[i] = dims[i] as BaseArray; 
            }
            return GetRange_ML(ba, fromRetT);
        }

        #endregion

        #region GetRange_np - DimSpecs fast subarray
        internal override CellStorage GetRange_np(DimSpec d0) {
            if (d0 is EllipsisSpec || d0 is FullDimSpec) {
                return Clone() as CellStorage;
            }
            return GetRange_np((BaseArray)d0); 
        }
        internal override CellStorage GetRange_np(DimSpec d0, DimSpec d1) {
            return GetRange_np((BaseArray)d0, d1);
        }
        internal override CellStorage GetRange_np(DimSpec d0, DimSpec d1, DimSpec d2) {
            return GetRange_np((BaseArray)d0, d1, d2);
        }
        internal override CellStorage GetRange_np(DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3) {
            return GetRange_np((BaseArray)d0, d1, d2, d3);
        }
        internal override CellStorage GetRange_np(DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3, DimSpec d4) {
            return GetRange_np((BaseArray)d0, d1, d2, d3, d4);
        }
        internal override CellStorage GetRange_np(DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3, DimSpec d4, DimSpec d5) {
            return GetRange_np((BaseArray)d0, d1, d2, d3, d4, d5);
        }
        internal override CellStorage GetRange_np(DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3, DimSpec d4, DimSpec d5, DimSpec d6) {
            return GetRange_np((BaseArray)d0, d1, d2, d3, d4, d5, d6);
        }
        internal override CellStorage GetRange_np(DimSpec[] dims, uint? len) {

            BaseArray[] ba = new BaseArray[(Equals(dims, null) ? 0 : (len?? (uint)dims.Length))];
            for (int i = 0; i < ba.Length; i++) {
                ba[i] = dims[i] as BaseArray; 
            }
            return GetRange_np(ba);
        }

        #endregion

        #region GetRange_ML overloads, via iterators, from BaseArray

        
        internal override CellStorage GetRange_ML(BaseArray d0, bool fromRetT) {

            //return base.GetRange_ML(d0, fromRetT);

            const uint HANDLES_NDIM = 1;
            if (d0 is EllipsisSpec) {
                // insider: ellipsis is a singleton. No need to dispose it! 
                return Clone() as CellStorage;
            } 
            if (d0 is BaseArray<string>) {
                #region single dim string spec may contain ';', addressing multiple dimensions
                var strStorage = (d0 as ConcreteArray<string, Array<string>, InArray<string>, OutArray<string>, Array<string>, Storage<string>>).Storage;
                if (strStorage == null || strStorage.S.NumberOfElements != 1) {
                    throw new ArgumentException($"Invalid index specification. Scalar string (array) expected. Found: {strStorage?.S.ToString()}");
                }

                var strVal = strStorage.GetValue(0);
                if (strVal.Contains(';')) {

                    // this does not focus on speed anymore! 
                    var dims = strVal.Split(new char[] { ';' }, StringSplitOptions.None);
                    if (dims == null || dims.Length < 1 || dims.Length > Size.MaxNumberOfDimensions) {
                        throw new ArgumentException($"Invalid index specification: \"{strVal}\". Unmatching dimension number or too many ';' provided.");
                    }
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
                            throw new ArgumentException($"Number of ; separated ranges in string index definition ({dims.Length}) exceed the maximum number of dimensions: {Size.MaxNumberOfDimensions}.");
                    }
                }
                #endregion
            }

            bool allSimpleRanges = false;
            var iterator0 = getCheckIterator(d0, 0, (long)Size.NumberOfElements - 1, ref allSimpleRanges);

            var storage = this; 
            if (iterator0.GetMaximum() >= storage.Size[HANDLES_NDIM - 1u]) {
                storage = storage.EnsureStorageOrder(StorageOrders.ColumnMajor, inplace: fromRetT);
            }

            CellStorage ret;
            // the index d0 is an index array or a non-simple range (f.e. by strings including ',')
            // In this 1D case Matlab gives the output array the same shape as the index array. 
            Size sd0 = null;
            #region acquire the size of d0, assuming that d0 is still alive since the iterator0 is not disposed yet. 
            if (d0 is BaseArray<double>) {
                sd0 = (d0 as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>).Storage.Size;
            } else if (d0 is BaseArray<int>) {
                sd0 = (d0 as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>).Storage.Size;
            } else if (d0 is BaseArray<uint>) {
                sd0 = (d0 as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>).Storage.Size;
            } else if (d0 is BaseArray<long>) {
                sd0 = (d0 as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>).Storage.Size;
            } else if (d0 is BaseArray<ulong>) {
                sd0 = (d0 as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>).Storage.Size;
            } else if (d0 is BaseArray<float>) {
                sd0 = (d0 as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>).Storage.Size;
            } else if (d0 is BaseArray<short>) {
                sd0 = (d0 as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>).Storage.Size;
            } else if (d0 is BaseArray<ushort>) {
                sd0 = (d0 as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>).Storage.Size;
            } else if (d0 is BaseArray<sbyte>) {
                sd0 = (d0 as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>).Storage.Size;
            } else if (d0 is BaseArray<byte>) {
                sd0 = (d0 as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>).Storage.Size;
            }
            System.Diagnostics.Debug.Assert(Equals(sd0, null) || sd0.NumberOfElements == iterator0.GetLength());
            #endregion

            #region iterate fast 
            ret = Create();
            ret.Handles[0] = ret.New((ulong)iterator0.GetLength(), clear: true);
            if (!Equals(sd0, null)) {
                ret.Size.SetAll(sd0, 0, StorageOrders.ColumnMajor);
            } else {
                // all non-numeric index arrays produce a column vector
                ret.Size.SetAll(dim0: iterator0.GetLength(), order: StorageOrders.ColumnMajor);
            }

            var cur = 0;
            foreach (var i in iterator0) {
                var val = storage.GetValue(i); 
                ret.SetValue(val, cur++, true);
            }
            // foreach has disposed the iterator already!
            #endregion

            return ret;
        }

        internal static void replace(ref IStorage dest, ref IStorage src, bool cloneSrc = true) {
            var clone = cloneSrc ? src?.Clone() : src; 
            dest?.Release();

            dest = clone; // -> ref count should be 1 (must be ensured by caller!)
        }

        
        internal override void CopyTo_T(CellStorage srcStorage, CellStorage destStorage, IIndexIterator[] iterators, uint ndimsOut) {

            // This handles empty storages! (by handling empty iterators)

            var srcSize = srcStorage.S;

            long stride0 = (ndimsOut == 1) ? srcSize.GetStride4MLlastDimExpansion(0) : srcSize.GetStride(0);
            var it0 = iterators[0];
            if (it0.GetLength() == 0) return;
            long higdims = srcStorage.S.BaseOffset;
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


            IStorage[] pOut = (destStorage.Handles[0] as ManagedHostHandle<IStorage>).HostArray; // + destStorage.S.BaseOffset;
            IStorage[] pIn = (srcStorage.Handles[0] as ManagedHostHandle<IStorage>).HostArray; // + srcBSD[2].ToUInt64();
            long posOut = destStorage.S.BaseOffset;
            while (true) {

                while (it0.MoveNext()) {
                    replace(ref pOut[posOut], ref pIn[higdims + it0.Current * stride0]);
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


        #endregion

        #region GetRange_NP overloads

        
        internal override unsafe void CopyTo_T_np(CellStorage srcStorage, CellStorage destStorage,
                        Iterators.MultidimIterator* itP, uint nIter,
                        long baseOffset) {

            // This handles empty storages! (by handling empty iterators)

            IStorage[] outA = (destStorage.Handles[0] as MemoryLayer.ManagedHostHandle<IStorage>).HostArray;
            long outP = destStorage.S.BaseOffset;
            IStorage[] pIn = (srcStorage.Handles[0] as MemoryLayer.ManagedHostHandle<IStorage>).HostArray;

            // iterators are provided for ndimsOut. Scalar dims have been removed and accumulated into baseOffset. 
            var srcSize = srcStorage.S;
            if (nIter == 0) {  // all indices are scalars
                replace(ref outA[outP], ref pIn[srcSize.BaseOffset + baseOffset]);
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
                    replace(ref outA[outP++], ref pIn[idx]);

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

        #endregion

        #region deep indexing
        /// <summary>
        /// Retrieves the cell _content_ of the cell addressed by <paramref name="indices"/> without cloning. 
        /// </summary>
        /// <param name="indices">Vector of the full index path, from root cell to the target element.</param>
        /// <param name="lenIndices">Number of indices stored in <paramref name="indices"/>.</param>
        /// <param name="start">First index in <paramref name="indices"/> to be considered.</param>
        /// <returns>The storage object found at <paramref name="indices"/>, potentially wrapped into a scalar <see cref="Storage{T}"/>.</returns>
        
        unsafe IStorage IStorage.GetCellContentDirect(long* indices, uint lenIndices, uint start) {
            if (start >= lenIndices || start < 0) {
                if (start == lenIndices &&  lenIndices == 0 && Size.NumberOfDimensions == 0) {
                    // includes NP scalar
                    return (Handles[0] as ManagedHostHandle<IStorage>).HostArray[0];
                }
                throw new ArgumentException($"The start index {start} for deep cell position {ILNumerics.Core.Global.Helper.dims2string(indices, lenIndices)} is out of range."); 
            } 
            // pick my indices 
            IStorage ret; 
            if (start + Size.NumberOfDimensions < lenIndices) {

                // deep indexing into one of my cell elements
                long ind = Size.GetSeqIndex(indices + start, Size.NumberOfDimensions);
                var val = (Handles[0] as ManagedHostHandle<IStorage>).HostArray[ind]; 
                if (Equals(val,null)) {
                    throw new ArgumentException($"The cell element at [{ILNumerics.Core.Global.Helper.dims2string(indices, lenIndices)}] was not found because the path to the addressed cell contains a 'null' value at index {start}."); 
                }
                start += Size.NumberOfDimensions;
                ret = val.GetCellContentDirect(indices, lenIndices, start);

            } else {

                // addressing one of my values, potentially with fewer indices than S.NumberOfDimensions
                long ind = Size.GetSeqIndex(indices + start, lenIndices - start);
                var val = (Handles[0] as ManagedHostHandle<IStorage>).HostArray[ind];
                // null values as target are allowed!
                ret = val; 

            }
            return ret; 
        }
        /// <summary>
        /// Sets a new value at the element / cell element as defined by <paramref name="indices"/>.
        /// </summary>
        /// <param name="value">The new value to be set directly. No clone will be made!</param>
        /// <param name="indices">Vector of the full index path, from root cell to the target element.</param>
        /// <param name="start">First index in <paramref name="indices"/> to be considered.</param>
        /// <param name="allowExpand">Flag indicating which expansion mode to apply (numpy=false, ILNumericsV4=true).</param>
        
        unsafe IStorage IStorage.SetCellContentDirect(BaseArray value, Span<long> indices, uint start, bool allowExpand) {
            if (start >= indices.Length || start < 0) {
                if (start == indices.Length && indices.Length == 0 && Size.NumberOfDimensions == 0) {
                    // includes NP scalar
                    var stor = value?.GetClonedStorage();
                    replace(ref (Handles[0] as ManagedHostHandle<IStorage>).HostArray[0], ref stor);
                    return this; 
                }
                throw new ArgumentException($"The start index {start} for deep cell position {ILNumerics.Core.Global.Helper.dims2string(AsPointer(indices), (uint)indices.Length)} is out of range.");
            }
            // pick my indices 
            if (start + Size.NumberOfDimensions < indices.Length) {

                // deep indexing into one of my cell elements
                long ind = Size.GetSeqIndex(AsPointer(indices, (int)start), Size.NumberOfDimensions);
                var val = (Handles[0] as ManagedHostHandle<IStorage>).HostArray[ind];
                if (Equals(val, null)) {
                    throw new ArgumentException($"The deep index [{ILNumerics.Core.Global.Helper.dims2string(AsPointer(indices), (uint)indices.Length)}] is invalid because the path to the addressed cell contains a 'null' value at index #{start}.");
                }
                start += Size.NumberOfDimensions;
                var setElement = val.SetCellContentDirect(value, indices, start, allowExpand);  // setElement is a new storage with ref. count = 0 !
                // the inner-most cell must update its array value. It was set out of-place! 
                // But we perform this update inplace here, since it will not involve a size change. 
                if (!object.ReferenceEquals(setElement, val)) {
                    setElement.Retain(); 
                    replace(ref (Handles[0] as ManagedHostHandle<IStorage>).HostArray[ind], ref setElement, cloneSrc: false);
                } 
                return this; 

            } else {

                var setElement = this.SetValue(value, AsPointer(indices, (int)start), (uint)(indices.Length - start), true); // enable Expansion + removal for ALL array styles
                // a changed value may causes this storage to be replaced (atomic mutation on Expand / Remove). 
                // In this case the change will populate up to the parent, where the old value is replaced. 
                return setElement;
            }
        }

        /// <summary>
        /// Wraps deep indexing value into a cell for returning in Indexers.
        /// </summary>
        /// <param name="d0"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <param name="d3"></param>
        /// <param name="d4"></param>
        /// <param name="d5"></param>
        /// <param name="d6"></param>
        /// <param name="len"></param>
        /// <returns>Cell wrapping the addressed value.</returns>
        
        internal CellStorage GetCell4Indexer_DeepIndex(long d0, long? d1 = null, long? d2 = null, long? d3 = null, long? d4 = null, long? d5 = null, long? d6 = null, uint len = 0) {
            var ret = CellStorage.Create();
            ret.S.SetScalar(0, Settings.MinNumberOfArrayDimensions);
            ret.Handles[0] = New(1);
            (ret.Handles[0] as ManagedHostHandle<IStorage>).HostArray[0] = GetStorage_DeepIndex(d0, d1, d2, d3, d4, d5, d6, len);
            return ret; 
        }
        /// <summary>
        /// Returns deep indexed value for returning in GetValue extensions.
        /// </summary>
        /// <param name="d0"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <param name="d3"></param>
        /// <param name="d4"></param>
        /// <param name="d5"></param>
        /// <param name="d6"></param>
        /// <param name="len"></param>
        /// <returns>Cell wrapping the addressed value.</returns>
        
        internal BaseArray GetValue_DeepIndex(long d0, long? d1 = null, long? d2 = null, long? d3 = null, long? d4 = null, long? d5 = null, long? d6 = null, uint len = 0) {
            return GetStorage_DeepIndex(d0, d1, d2, d3, d4, d5, d6, len).GetBaseArray();
        }
        
        internal unsafe IStorage GetStorage_DeepIndex(long? d0 = null, long? d1 = null, long? d2 = null, long? d3 = null, long? d4 = null, long? d5 = null, long? d6 = null, uint len = 0) {
            long* indices = (long*)Context.TmpBuffer1000;
            switch (len) {
                case 0:
                    break;
                case 1:
                    indices[0] = d0.GetValueOrDefault();
                    break;
                case 2:
                    indices[0] = d0.GetValueOrDefault(); indices[1] = d1.GetValueOrDefault();
                    break;
                case 3:
                    indices[0] = d0.GetValueOrDefault(); indices[1] = d1.GetValueOrDefault(); indices[2] = d2.GetValueOrDefault();
                    break;
                case 4:
                    indices[0] = d0.GetValueOrDefault(); indices[1] = d1.GetValueOrDefault(); indices[2] = d2.GetValueOrDefault(); indices[3] = d3.GetValueOrDefault();
                    break;
                case 5:
                    indices[0] = d0.GetValueOrDefault(); indices[1] = d1.GetValueOrDefault(); indices[2] = d2.GetValueOrDefault(); indices[3] = d3.GetValueOrDefault();
                    indices[4] = d4.GetValueOrDefault();
                    break;
                case 6:
                    indices[0] = d0.GetValueOrDefault(); indices[1] = d1.GetValueOrDefault(); indices[2] = d2.GetValueOrDefault(); indices[3] = d3.GetValueOrDefault();
                    indices[4] = d4.GetValueOrDefault(); indices[5] = d5.GetValueOrDefault();
                    break;
                case 7:
                    indices[0] = d0.GetValueOrDefault(); indices[1] = d1.GetValueOrDefault(); indices[2] = d2.GetValueOrDefault(); indices[3] = d3.GetValueOrDefault();
                    indices[4] = d4.GetValueOrDefault(); indices[5] = d5.GetValueOrDefault(); indices[6] = d6.GetValueOrDefault();
                    break;
                default:
                    throw new NotSupportedException($"Invalid number of indices provided for deep indexing: {len}.");
            }
            return (this as IStorage).GetCellContentDirect(indices, len, 0); 
        }

        #endregion

        /// <summary>
        /// Tests if a cell element is of the given array kind/element type. 
        /// </summary>
        /// <typeparam name="T">The array element type to probe the cell element for.</typeparam>
        /// <param name="indices">Dimensional indices defining the position of the cell element to be probed.</param>
        /// <returns>true if the element found at the given position is an array of the given element type <typeparamref name="T"/>, false otherwise.</returns>
        /// <remarks><para>The method is helpful in order to investigate the contents of a cell array. If you are not sure about the 
        /// types of elements in the cell, this function can be used to make sure that elements actually are of the expected type before attempting to retrieve them.</para>
        /// <para>In most situations, elements of a cell are stored arrays of a distinct element type. That element type is given to <see cref="IsTypeOf"/> as 
        /// typeparameter <typeparamref name="T"/>. That means, in order to find out, if the first cell element stores an array of int 
        /// <c><![CDATA[cell.IsTypeOf<int>(0)]]></c> is used.</para>
        /// <para>In order to test for cell element type <typeparamref name="T"/> can be <see cref="Cell"/> or <see cref="BaseArray"/>: <c><![CDATA[cell.IsTypeOf<Cell>(0)]]></c>.</para>
        /// <para>In order to test for logical element type <typeparamref name="T"/> must be <see cref="bool"/>: <c><![CDATA[cell.IsTypeOf<bool>(0)]]></c>.</para>
        /// </remarks>
        /// <example>
        /// <para>In the following example a Cell of size 3x2 is created. It stores several array types, among which other cells are stored as elements of the outer cell.</para>
        /// <code>Cell cell = ILMath.cell(new Size(3, 2) 
        ///                      , "first element"
        ///                      , 2.0
        ///                      , ILMath.cell(Math.PI, 100f)
        ///                      , ILMath.create&lt;short>(1, 2, 3, 4, 5, 6)
        ///                      , new double[] {-1.4, -1.5, -1.6});
        /// </code>
        /// The cell is now: 
        /// <code>Cell [3,2]
        ///          &lt;String>      first element  &lt;Int16> [2,3,4,5,6] 
        ///          &lt;Double>          2          Cell [1,3]           
        ///          Cell [2,1]                                    (null)	
        /// </code>
        /// We test the element type of each element in the cell: 
        /// <code>
        /// Console.Out.WriteLine("cell[0,0] is of type 'string': {0}", cell.IsTypeOf&lt;string>(0));
        /// Console.Out.WriteLine("cell[0,0] is of type 'double': {0}", cell.IsTypeOf&lt;double>(0));
        ///                                      
        /// Console.Out.WriteLine("cell[1,0] is of type 'double': {0}", cell.IsTypeOf&lt;double>(1));
        /// Console.Out.WriteLine("cell[2,0] is of type 'Cell': {0}", cell.IsTypeOf&lt;Cell>(2));
        ///                                                                         
        /// Console.Out.WriteLine("cell[0,1] is of type 'short': {0}", cell.IsTypeOf&lt;short>(0, 1));
        /// Console.Out.WriteLine("cell[1,1] is of type 'Cell': {0}", cell.IsTypeOf&lt;Cell>(1, 1));
        /// Console.Out.WriteLine("cell[2,1] is of type 'double': {0}", cell.IsTypeOf&lt;double>(2, 1));
        /// </code>
        /// This gives the following output: 
        /// <code>
        /// cell[0,0] is element type 'string': True
        /// cell[0,0] is element type 'double': False
        /// cell[1,0] is element type 'double': True
        /// cell[2,0] is element type 'Cell': True
        /// cell[0,1] is element type 'short': True
        /// cell[1,1] is element type 'Cell': True
        /// cell[2,1] is element type 'double': False  (element is null)
        /// </code></example>
        
        public unsafe bool IsTypeOf<T>(InArray<long> indices) {
            using (Scope.Enter(indices)) {

                var arr = (Handles[0] as ManagedHostHandle<IStorage>).HostArray; 
                var cell = arr[Size.GetSeqIndex(indices)];
                if (typeof(T) == typeof(Cell) || typeof(T) == typeof(BaseArray)) {
                    return cell is CellStorage;
                } else if (typeof(T) == typeof(bool)) {
                    return cell is LogicalStorage; 
                } else { 
                    return cell is Storage<T>; 
                }
            }
        }

    }
}
