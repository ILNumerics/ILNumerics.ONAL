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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using ILNumerics.Core.Global;
using ILNumerics.Core.StorageLayer;
using static ILNumerics.Globals;

namespace ILNumerics.Core.Functions.Builtin {
    internal static partial class MathInternal {

        #region generic (numeric)

        /// <summary>
        /// Creates a new ILNumerics array, all elements initialized with the given value.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="val">The value for the new elements.</param>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="order">[Optional] The storage order for the new array. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>New ILNumerics array initialized with <paramref name="val"/>.</returns>
        internal unsafe static Array<T> array<T>(T val, long dim0, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0: dim0, order: order);
            return arrayInternal<T>(val, ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array, all elements initialized with the given value.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="val">The value for the new elements.</param>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="order">[Optional] The storage order for the new array. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>New ILNumerics array initialized with <paramref name="val"/>.</returns>
        internal unsafe static Array<T> array<T>(T val, long dim0, long dim1, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, order: order);
            return arrayInternal<T>(val, ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array, all elements initialized with the given value.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="val">The value for the new elements.</param>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="order">[Optional] The storage order for the new array. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>New ILNumerics array initialized with <paramref name="val"/>.</returns>
        internal unsafe static Array<T> array<T>(T val, long dim0, long dim1, long dim2, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, dim2, order: order);
            return arrayInternal<T>(val, ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array, all elements initialized with the given value.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="val">The value for the new elements.</param>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="dim3">Length of dimension #3.</param>
        /// <param name="order">[Optional] The storage order for the new array. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>New ILNumerics array initialized with <paramref name="val"/>.</returns>
        internal unsafe static Array<T> array<T>(T val, long dim0, long dim1, long dim2, long dim3, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, order: order);
            return arrayInternal<T>(val, ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array, all elements initialized with the given value.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="val">The value for the new elements.</param>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="dim3">Length of dimension #3.</param>
        /// <param name="dim4">Length of dimension #4.</param>
        /// <param name="order">[Optional] The storage order for the new array. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>New ILNumerics array initialized with <paramref name="val"/>.</returns>
        internal unsafe static Array<T> array<T>(T val, long dim0, long dim1, long dim2, long dim3, long dim4, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, dim4, order: order);
            return arrayInternal<T>(val, ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array, all elements initialized with the given value.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="val">The value for the new elements.</param>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="dim3">Length of dimension #3.</param>
        /// <param name="dim4">Length of dimension #4.</param>
        /// <param name="dim5">Length of dimension #5.</param>
        /// <param name="order">[Optional] The storage order for the new array. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>New ILNumerics array initialized with <paramref name="val"/>.</returns>
        internal unsafe static Array<T> array<T>(T val, long dim0, long dim1, long dim2, long dim3, long dim4, long dim5, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, dim4, dim5, order: order);
            return arrayInternal<T>(val, ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array, all elements initialized with the given value.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="val">The value for the new elements.</param>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="dim3">Length of dimension #3.</param>
        /// <param name="dim4">Length of dimension #4.</param>
        /// <param name="dim5">Length of dimension #5.</param>
        /// <param name="dim6">Length of dimension #6.</param>
        /// <param name="order">[Optional] The storage order for the new array. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>New ILNumerics array initialized with <paramref name="val"/>.</returns>
        internal unsafe static Array<T> array<T>(T val, long dim0, long dim1, long dim2, long dim3, long dim4, long dim5, long dim6, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, dim4, dim5, dim6, order: order);
            return arrayInternal<T>(val, ret);
        }

        #endregion

        #region compatibility functions
        /// <summary>
        /// Create new array, fill elements with constant value
        /// </summary>
        /// <typeparam name="T">Element type</typeparam>
        /// <param name="value">Constant value for all elements.</param>
        /// <param name="size">Size of new array</param>
        /// <param name="order">[Optional] storage order. Default: <see cref="F:ILNumerics.StorageOrders.ColumnMajor"></see>.</param>
        /// <returns>New array according to size with all elements set to 'value'</returns>
        internal unsafe static Array<T> array<T>(T value, Size size, StorageOrders order = StorageOrders.ColumnMajor) {
            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetDimensionLengths(size.GetBSD(false), order: order);
            return arrayInternal<T>(value, ret);
        }
        /// <summary>
        /// Creates new array of dimension lengths, with all elements having the same value <paramref name="val"/>. 
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="val">The target element value.</param>
        /// <param name="size">Vector with dimension lengths, as created by Math.size().</param>
        /// <param name="order">[Optional] storage order. Default: <see cref="StorageOrders.ColumnMajor"></see>.</param>
        /// <returns>New array as specified.</returns>
        internal unsafe static Array<T> array<T>(T val, InArray<long> size, StorageOrders order = StorageOrders.ColumnMajor) {

            if (Equals(size, null)) {
                throw new ArgumentNullException(nameof(size));
            }

            using var s = Scope.Enter();
            
            Array<long> mySize = size; 
            if (!mySize.IsVector) {
                throw new ArgumentException("'size' must be a vector of positive values.");
            }
            Storage<T> ret = Storage<T>.Create(mySize, order);
                
            return arrayInternal<T>(val, ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array from the values provided in <paramref name="values"/> with the shape given by <paramref name="size"/>.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="values">Values to be copied to the new ILNumerics array.</param>
        /// <param name="size">The size of the new ILNumerics array. Number of elements must correspond to the number of values in <paramref name="values"/>.</param>
        /// <param name="order">[Optional] Storage order for the new ILNumerics array. Default: (<see cref="StorageOrders.ColumnMajor"/>).</param>
        /// <returns>A new ILNumerics array with copies of the given values and the given shape.</returns>
        /// <remarks>A copy is made from <paramref name="values"/>. The array <paramref name="values"/> is not referenced 
        /// by ILNumerics after the function returns.
        /// <para>Make sure that the number of elements configured by the dimension lengths in <paramref name="size"/>
        /// match the number of values of <paramref name="values"/>.</para>
        /// <para>This function is a convenience alias for 'vector(<paramref name="values"/>).Reshape(<paramref name="size"/>,<paramref name="order"/>)'.</para></remarks>
        internal static Array<T> array<T>(T[] values, InArray<long> size, StorageOrders order = StorageOrders.ColumnMajor) {
            using (Scope.Enter()) {
                return vector<T>(values).Reshape(size, order);
            }
        }
        /// <summary>
        /// Creates a new ILNumerics array from a 2D System.Array <paramref name="values"/> with the shape of <paramref name="values"/> in <see cref="StorageOrders.RowMajor"/> order.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="values">Values to be copied to the new ILNumerics array.</param>
        /// <returns>A new ILNumerics array with copies of the given values and the given shape.</returns>
        /// <remarks>A copy is made from <paramref name="values"/>. The array <paramref name="values"/> is not referenced 
        /// by ILNumerics after the function returns.</remarks>
        internal static Array<T> array<T>(T[,] values) {
            return values;
        }
        /// <summary>
        /// Creates a new ILNumerics array from a 3D System.Array <paramref name="values"/> with the shape of <paramref name="values"/> in <see cref="StorageOrders.RowMajor"/> order.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="values">Values to be copied to the new ILNumerics array.</param>
        /// <returns>A new ILNumerics array with copies of the given values and the given shape.</returns>
        /// <remarks>A copy is made from <paramref name="values"/>. The array <paramref name="values"/> is not referenced 
        /// by ILNumerics after the function returns.</remarks>
        internal static Array<T> array<T>(T[,,] values) {
            return values;
        }
        /// <summary>
        /// Creates a new ILNumerics array from a 4D System.Array <paramref name="values"/> with the shape of <paramref name="values"/> in <see cref="StorageOrders.RowMajor"/> order.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="values">Values to be copied to the new ILNumerics array.</param>
        /// <returns>A new ILNumerics array with copies of the given values and the given shape.</returns>
        /// <remarks>A copy is made from <paramref name="values"/>. The array <paramref name="values"/> is not referenced 
        /// by ILNumerics after the function returns.</remarks>
        internal static Array<T> array<T>(T[,,,] values) {
            return values;
        }

        #endregion compatibility functions

        
        private unsafe static Array<T> arrayInternal<T>(T val, Storage<T> ret) {
            System.Diagnostics.Debug.Assert(ret != null && ret.S.IsContinuous);
            var outLen = ret.S.NumberOfElements;
            if (ret.Handles == null) {
                ret.Handles = CountableArray.Create();
            }
            if (!ret.Handles.IsOnDevice(0) || (long)ret.Handles[0].Length.ToUInt64() < outLen * Storage<T>.SizeOfT) {
                ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<T>((ulong)outLen);
            }

            uint elemLength = StorageLayer.Storage<T>.SizeOfT;

            if (val is System.ValueType) {

                if (false) {
                    #region HYCALPER LOOPSTART 
                    /*!HC:TYPELIST:
                    <hycalper>
                        <type>
                            <source locate="here">
                                double
                            </source>
                            <destination>char</destination>
                            <destination>float</destination>
                            <destination>complex</destination>
                            <destination>fcomplex</destination>
                            <destination>sbyte</destination>
                            <destination>byte</destination>
                            <destination>short</destination>
                            <destination>ushort</destination>
                            <destination>int</destination>
                            <destination>uint</destination>
                            <destination>long</destination>
                            <destination>ulong</destination>
                        </type>
                    </hycalper>
                    */

                } else if (val is double) {

                    double v = (double)(object)val;
                    double* p = (double*)ret.Handles[0].Pointer + ret.S.BaseOffset;

                    for (long i = outLen; i-- > 0;) {
                        *p++ = v;
                    }
                    #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
                   

                } else if (val is ulong) {

                    ulong v = (ulong)(object)val;
                    ulong* p = (ulong*)ret.Handles[0].Pointer + ret.S.BaseOffset;

                    for (long i = outLen; i-- > 0;) {
                        *p++ = v;
                    }
                   

                } else if (val is long) {

                    long v = (long)(object)val;
                    long* p = (long*)ret.Handles[0].Pointer + ret.S.BaseOffset;

                    for (long i = outLen; i-- > 0;) {
                        *p++ = v;
                    }
                   

                } else if (val is uint) {

                    uint v = (uint)(object)val;
                    uint* p = (uint*)ret.Handles[0].Pointer + ret.S.BaseOffset;

                    for (long i = outLen; i-- > 0;) {
                        *p++ = v;
                    }
                   

                } else if (val is int) {

                    int v = (int)(object)val;
                    int* p = (int*)ret.Handles[0].Pointer + ret.S.BaseOffset;

                    for (long i = outLen; i-- > 0;) {
                        *p++ = v;
                    }
                   

                } else if (val is ushort) {

                    ushort v = (ushort)(object)val;
                    ushort* p = (ushort*)ret.Handles[0].Pointer + ret.S.BaseOffset;

                    for (long i = outLen; i-- > 0;) {
                        *p++ = v;
                    }
                   

                } else if (val is short) {

                    short v = (short)(object)val;
                    short* p = (short*)ret.Handles[0].Pointer + ret.S.BaseOffset;

                    for (long i = outLen; i-- > 0;) {
                        *p++ = v;
                    }
                   

                } else if (val is byte) {

                    byte v = (byte)(object)val;
                    byte* p = (byte*)ret.Handles[0].Pointer + ret.S.BaseOffset;

                    for (long i = outLen; i-- > 0;) {
                        *p++ = v;
                    }
                   

                } else if (val is sbyte) {

                    sbyte v = (sbyte)(object)val;
                    sbyte* p = (sbyte*)ret.Handles[0].Pointer + ret.S.BaseOffset;

                    for (long i = outLen; i-- > 0;) {
                        *p++ = v;
                    }
                   

                } else if (val is fcomplex) {

                    fcomplex v = (fcomplex)(object)val;
                    fcomplex* p = (fcomplex*)ret.Handles[0].Pointer + ret.S.BaseOffset;

                    for (long i = outLen; i-- > 0;) {
                        *p++ = v;
                    }
                   

                } else if (val is complex) {

                    complex v = (complex)(object)val;
                    complex* p = (complex*)ret.Handles[0].Pointer + ret.S.BaseOffset;

                    for (long i = outLen; i-- > 0;) {
                        *p++ = v;
                    }
                   

                } else if (val is float) {

                    float v = (float)(object)val;
                    float* p = (float*)ret.Handles[0].Pointer + ret.S.BaseOffset;

                    for (long i = outLen; i-- > 0;) {
                        *p++ = v;
                    }
                   

                } else if (val is char) {

                    char v = (char)(object)val;
                    char* p = (char*)ret.Handles[0].Pointer + ret.S.BaseOffset;

                    for (long i = outLen; i-- > 0;) {
                        *p++ = v;
                    }

#endregion HYCALPER AUTO GENERATED CODE

                } else {
                    throw new ArgumentException($"The element type '{typeof(T).Name}' is currently not supported in this context.");
                }
            } else {
                // generic reference type
                T[] retArr = (ret.Handles[0] as MemoryLayer.ManagedHostHandle<T>)?.HostArray;
                System.Diagnostics.Debug.Assert(retArr != null);
                for (long i = 0; i < outLen; i++) {
                    retArr[i] = val;
                }
            }
            return ret.RetArray;
        }

        #region params, obsolete
        /// <summary>
        /// Create new array, fill element with constant value
        /// </summary>
        /// <typeparam name="T">Element type</typeparam>
        /// <param name="value">Constant value for all elements</param>
        /// <param name="size">Size of new array</param>
        /// <returns>New array according to size with all elements set to 'value'</returns>
        [Obsolete("-> replace: array(value, s1, s2) with: array(value, size(s1,s2))!")]
        internal static Array<T> array<T>(T value, params int[] size) {
            return array<T>(value, size, StorageOrders.ColumnMajor);
        }

        /// <summary>
        /// Creates a vector from <paramref name="values"/>. Column major storage.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="values">Variable number of values for the new vector.</param>
        /// <returns>A vector of <typeparamref name="T"/> elements, 1D in numpy array style, a column vector otherwise.</returns>
        [Obsolete("-> replace: array(v1,v2,v3,...) with: vector<T>(v1,v2,v3,...)!")]
        internal static Array<T> array<T>(params T[] values) {
            return values;
        }

        /// <summary>
        /// Creates an array from <paramref name="values"/> and <paramref name="size"/>. Column major storage.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="size">Source size descriptor providing the dimension lengths for the new array.</param>
        /// <param name="values">Values for the new array.</param>
        /// <returns>Array of <typeparamref name="T"/> elements and a size according to <paramref name="size"/>.</returns>
        /// <exception cref="ArgumentException">if the size given by <paramref name="size"/> does not match the number of <paramref name="values"/> provided.</exception>
        [Obsolete("-> replace: array(size, v1,v2,v3...) with: vector(v1,v2,v3,..).Reshape(size)!")]
        internal static Array<T> array<T>(Size size, params T[] values) {
            using (Scope.Enter()) {
                Array<T> ret = values;
                if (Equals(ret, null) || ret.S.NumberOfElements != size.NumberOfElements) {
                    throw new ArgumentException($"The number of values provided must match the specified 'size'.");
                }
                ret.S.SetDimensionLengths(size);
                return ret;
            }
        }
        /// <summary>
        /// Creates an array from <paramref name="values"/> and <paramref name="size"/>. Column major storage.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="size">Size descriptor providing the dimension lengths for the new array.</param>
        /// <param name="values">Values for the new array will be copied from this System.Array.</param>
        /// <returns>Array of <typeparamref name="T"/> elements and a size according to <paramref name="size"/>.</returns>
        /// <exception cref="ArgumentException">if the size given by <paramref name="size"/> does not match the number of <paramref name="values"/> provided.</exception>
        [Obsolete("Use vector(..).Reshape(..) instead!")]
        internal unsafe static Array<T> array<T>(T[] values, params int[] size) {
            if (Equals(size, null) || Equals(values, null)) {
                throw new ArgumentException("'values' and 'size' parameters may not be null!");
            }
            if (size.Length > Size.MaxNumberOfDimensions) {
                throw new ArgumentException($"The maximum number of dimensions is {Size.MaxNumberOfDimensions}.");
            }
            return vector(values).Reshape(Array.ConvertAll(size, a => (uint)a));

        }
        /// <summary>
        /// Creates an array from <paramref name="values"/>. Column major storage.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="size">Source size descriptor providing the dimension lengths for the new array.</param>
        /// <param name="values">Values for the new array.</param>
        /// <returns>Array of <typeparamref name="T"/> elements and a size according to <paramref name="size"/>.</returns>
        /// <exception cref="ArgumentException">if the size given by <paramref name="size"/> does not match the number of <paramref name="values"/> provided.</exception>
        [Obsolete("Use vector(..).Reshape(..) instead!")]
        internal static Array<T> array<T>(T[] values, Size size) {
            return array(size, values);
        }

        /// <summary>
        /// Creates an ILNumerics array from <see cref="IEnumerable{T}"/>. 
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="values">Enumerable with values for the new array.</param>
        /// <param name="size">[Optional] size for the new array. Default: (null) creates a vector according to the number of given values.</param>
        /// <param name="order">[Optional] Storage order for the new cell. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>ILNumerics array with shallow copy of the values taken from <paramref name="values"/>.</returns>
        /// <remarks><para>If <paramref name="size"/> is provided the number of elements as determined by the number and lengths of 
        /// dimensions in <paramref name="size"/> must match the actual number of elements found in <paramref name="values"/>.</para>
        /// <para><paramref name="values"/> is iterated exactly once.</para>
        /// <para>If <typeparamref name="T"/> is a reference type and/or <see cref="IDisposable"/> a shallow copy 
        /// of the reference value is made only. No interface methods are called on the elements of <paramref name="values"/>.</para>
        /// <para>If both, <paramref name="size"/> and <paramref name="values"/> are null, an empty array is returned.</para>
        /// </remarks>
        internal static Array<T> array<T>(InArray<long> size = null, IEnumerable<T> values = null, StorageOrders order = StorageOrders.ColumnMajor) {
            using (Scope.Enter(size)) { // allow both array styles! 
                var ret = Storage<T>.Create();
                if (Equals(size, null)) {
                    if (!Equals(values, null)) {
                        ret.S.SetAll(dim0: 10L, order: order); // start large at 10, shrink potom
                    } else {
                        ret.S.SetAll(dim0: 0, order: order);
                    }
                } else {
                    ret.S.SetAll(size, order);
                }

                ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<T>((ulong)ret.S.NumberOfElements, clear: true);
                Array<T> R = ret.GetLocalArray(); 

                if (!object.Equals(values, null)) {

                    long i = 0;
                    using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {
                        foreach (var a in values) {
                            if (i >= R.S.NumberOfElements && !Equals(size, null)) {
                                throw new ArgumentException($"The number of elements provided in '{nameof(values)}' must not exceed the specified size for the new array. Found: size={R.S.ToString()}. {nameof(values)}.Count > {i}.");
                            }
                            R.SetValue(a, i++);
                        }
                        if (i < 10 && Equals(size, null)) { // initial size too large
                            return R.Subarray(slice(0, i));
                        }
                    }

                }
                return R;
            }
        }
        #endregion
    }
}
