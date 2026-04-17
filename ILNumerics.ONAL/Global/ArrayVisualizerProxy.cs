using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using ILNumerics;
using System.Diagnostics;
using System.Security;
using ILNumerics.Core.StorageLayer;
using ILNumerics.Core.Arrays;

namespace ILNumerics.Core.Misc {
    /// <summary>
    /// ILNumerics Visual Studio Array Visualizer Helper (do not use this class directly!)
    /// </summary>
    public class ArrayVisualizerProxy  {

        /*
         * Version 5 modifications: ArrayVisualizerProxy 
         * * should store a reference to the _original_ array. Be prepared for huge 
         *   arrays! i.e.: don't convert all elements. Don't even access all elements! 
         * * handle both: managed array T[] and pointer based arrays. 
         * * Handle arrays with pending storages! 
         */

#pragma warning disable CS1591// Not used directly
        public static readonly string SourceTypeILArray = "Array";
        public static readonly string SourceTypeName = "SourceType";
        public static readonly string Is64BitProcessName = "Is64BitProcess"; 
        public static readonly string SizeName = "Size";
        public static readonly string StridesName = "Strides";
        public static readonly string OffsetName = "Offset";
        public static readonly string AddressName = "Address";
        public static readonly string SystemArrayName = "SystemArray";
        public static readonly string ElementTypeName = "ElementType";
#pragma warning restore CS1591// Not used directly

        private ulong m_address;  // this must be ulong (and not IntPtr) since it must work in a x86 process (VS) targeting a x64 debuggee! 

        /// <summary>
        /// (not used)
        /// </summary>
        public bool CanVisualize {
            get {
                return !Equals(Size, null) && Address != 0 && !String.IsNullOrEmpty(ElementType);
            }
        }
        /// <summary>
        /// The size of the array expression this proxy represents
        /// </summary>
        public Size Size {
            get; private set;
        }
        public string Strides {
            get {
                var ret = new StringBuilder("[");
                for (uint i = 0; i < Size.NumberOfDimensions; i++) {
                    if (i > 0) {
                        ret.Append(",");
                    }
                    ret.Append(Size.GetStride(i));
                }
                ret.Append("]");
                return ret.ToString();
            }
        }
        public string Offset {
            get {
                if (Equals(Size,null)) {
                    return "0"; 
                } else {
                    return this.Size.BaseOffset.ToString();
                }
            }
        }
        /// <summary>
        /// The physical address of the storage array for the result. For managed arrays the _current_, unpinned address on the managed heap.
        /// </summary>
        public UInt64 Address {
            get {
                if (!Equals(SystemArray, null)) {
                    var gh = GCHandle.Alloc(SystemArray, GCHandleType.Pinned);
                    long p = gh.AddrOfPinnedObject().ToInt64();
                    gh.Free();
                    return (ulong)p;
                } else {
                    return m_address; 
                }
            }
            private set {
                m_address = value;
                if (m_address != 0) {
                    // can have only one cake
                    SystemArray = null;
                }
            }
        }
        /// <summary>
        /// Property exposing the underlying System.Array for the expression result 
        /// </summary>
        public Array SystemArray {
            get; private set; 
        }
        /// <summary>
        /// String representation of the element types of the result
        /// </summary>
        public string ElementType {
            get; private set;
        }

        /// <summary>
        /// Helper for Array Visualizer process bitrate detection. This eases the determiniation of the appropriate memory read attempt during evaluations of array expressions in Visual Studio debug sessions. 
        /// </summary>
        /// <remarks>The reason why this has been (re)included is that it makes the info available at a point 
        /// where using the expression evaluator to query "Environment.Is64BitProcess" directly would be more demanding.</remarks>
        public bool Is64BitProcess {
            get { return Environment.Is64BitProcess; }
        }
        /// <summary>
        /// string representation of the (array) type of the original expression result 
        /// </summary>
        public string SourceType { get; private set; }

        #region constructors + static creation funcs
        /// <summary>
        /// Create a new proxy representing an Array vor visualizing in ILNumerics Array Visualizer
        /// </summary>
        /// <param name="array">the system array</param>
        /// <param name="size">the final n-dim array size</param>
        /// <param name="elementType">the name of the elements type</param>
        public ArrayVisualizerProxy(Array array, Size size, String elementType) {
            SystemArray = array;
            m_address = 0; 
            Size = size;
            ElementType = elementType;
        }
        /// <summary>
        /// Create a new proxy representing an Array for visualizing in ILNumerics Array Visualizer
        /// </summary>
        /// <param name="address">a pointer to array memory</param>
        /// <param name="size">the final n-dim array size</param>
        /// <param name="elementType">the name of the elements type</param>
        public ArrayVisualizerProxy(ulong address, Size size, String elementType) {
            SystemArray = null;
            m_address = address; 
            Size = size;
            ElementType = elementType;
        }
        /// <summary>
        /// Create a new proxy for an existing ILNumerics array (of any kind)
        /// </summary>
        /// <typeparam name="T">The element type of the Array</typeparam>
        /// <param name="A">The n-dim Array to create a new proxy for</param>
        /// <returns>Array proxy</returns>
        public static ArrayVisualizerProxy Create<T>(BaseArray<T> A) {
            if (object.Equals(A, null)) {
                return new ArrayVisualizerProxy(null, null, null);
            }

            ArrayVisualizerProxy ret; 
            if (typeof(T).IsValueType) {
                if (A is BaseArray<bool>) {
                    var storage = (A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage; 
                    ret = new ArrayVisualizerProxy((ulong)storage.Handles[0].Pointer.ToInt64(), storage.S, typeof(bool).Name); 
                    ret.SourceType = "Array:" + storage.S.NumberOfDimensions; 
                } else {
                    var storage = (A as ConcreteArray<T, Array<T>, InArray<T>, OutArray<T>, Array<T>, Storage<T>>)?.Storage;
                    ret = new ArrayVisualizerProxy((ulong)storage.Handles[0].Pointer.ToInt64(), storage.S, typeof(T).Name);
                    ret.SourceType = "Array:" + storage.S.NumberOfDimensions; 
                }
            } else {
                throw new NotSupportedException($"Unsupported ILNumerics array type: {A.GetType().Name}."); // may releases A!
            }
            return ret; 
            // we don't release A here! Don't be invasive! 
        }
        /// <summary>
        /// Create a new proxy for an existing Array (of any kind). Overload
        /// </summary>        
        /// <param name="A">The n-dim Array to create a new proxy for</param>
        /// <returns>Array proxy</returns>
        public unsafe static ArrayVisualizerProxy Create(Array A) {
            if (object.Equals(A, null)) {
                return new ArrayVisualizerProxy(null, null, null);
            }
            string elementType = A.GetType().GetElementType().Name;
            // create the size descriptor
            var s = new Size();
            var ndims = Math.Min(A.Rank, Size.MaxNumberOfDimensions);
            var bsd = s.GetBSD(true); 
            for (int i = 0; i < ndims; i++) {
                bsd[3 + i] = A.GetLength(i);
            }
            bsd[0] = ndims;
            bsd[1] = A.LongLength;
            bsd[2] = 0; 
            s.SetDimensionLengths(bsd, StorageOrders.RowMajor);

            var ret = new ArrayVisualizerProxy(A, s, elementType);
            ret.SourceType = A.GetType().Name + ":" + A.Rank;
            return ret; 

        }
        /// <summary>
        /// Creates a new proxy for an existing scalar.
        /// </summary>        
        public static ArrayVisualizerProxy Create(ValueType A) {
            string elementType = A.GetType().Name;
            // create the size descriptor
            ArrayVisualizerProxy ret;
            var S = new Size();
            S.SetScalar(0,0); 
            if (A is double) {
                ret = new ArrayVisualizerProxy(new double[] { (double)A }, S, elementType);
            } else if (A is float) {
                ret = new ArrayVisualizerProxy(new float[] { (float)A }, S, elementType);
            } else if (A is int) {
                ret = new ArrayVisualizerProxy(new int[] { (int)A }, S, elementType);
            } else if (A is long) {
                ret = new ArrayVisualizerProxy(new long[] { (long)A }, S, elementType);
            } else if (A is short) {
                ret = new ArrayVisualizerProxy(new short[] { (short)A }, S, elementType);
            } else if (A is sbyte) {
                ret = new ArrayVisualizerProxy(new sbyte[] { (sbyte)A }, S, elementType);
            } else if (A is uint) {
                ret = new ArrayVisualizerProxy(new uint[] { (uint)A }, S, elementType);
            } else if (A is ulong) {
                ret = new ArrayVisualizerProxy(new ulong[] { (ulong)A }, S, elementType);
            } else if (A is ushort) {
                ret = new ArrayVisualizerProxy(new ushort[] { (ushort)A }, S, elementType);
            } else if (A is byte) {
                ret = new ArrayVisualizerProxy(new byte[] { (byte)A }, S, elementType);
            } else if (A is complex) {
                ret = new ArrayVisualizerProxy(new complex[] { (complex)A }, S, elementType);
            } else if (A is fcomplex) {
                ret = new ArrayVisualizerProxy(new fcomplex[] { (fcomplex)A }, S, elementType);
            } else {
                ret = new ArrayVisualizerProxy(new object[] { A }, S, elementType);
            }
            ret.SourceType = "Array:0";
            return ret;
        }
        #endregion

#if OBSOLETE
        // In version 5 this proxy is not invasive anymore: it does not change the state of the 
        // object in the debuggee. Thus, it does not need to be disposed.
        ///// <summary>
        /////  Dispose off this proxy
        ///// </summary>
        //public void Dispose() {
        //    Dispose(true); 
        //}
        ///// <summary>
        ///// manually dispose off this proxy
        ///// </summary>
        ///// <param name="manual"></param>
        //private void Dispose(bool manual) {
        //    if (IsDisposed) return;
        //    IsDisposed = true; 
        //    if (manual) {
        //        GC.SuppressFinalize(this); 
        //    }
        //    if (!object.Equals(m_ilarray, null)) {
        //        m_ilarray.m_scopeCounter--; 
        //    }
        //}
        ///// <summary>
        ///// Finalizer disposing off this proxy
        ///// </summary>
        //~ArrayVisualizerProxy() {
        //    Dispose(false); 
        //}
        ///// <summary>
        ///// Determines if this proxy has been disposed of
        ///// </summary>
        //public bool IsDisposed { get; private set; }

#endif

    }
}
