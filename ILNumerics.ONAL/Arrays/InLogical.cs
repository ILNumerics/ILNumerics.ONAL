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
using ILNumerics.Core.StorageLayer;
using ILNumerics.Core.Arrays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using ILNumerics.Core.DeviceManagement;

namespace ILNumerics {

    /// <summary>
    /// Input logical array type to be used as input parameter in user defined functions. 
    /// </summary>
    public sealed class InLogical

        : Immutable<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage> {

        #region construction

        internal InLogical(LogicalStorage storage) : base(storage) { }

        #endregion

        #region conversions, cast operators

        #region constructional operators 

        /// <summary>
        /// Converts single value into scalar array.
        /// </summary>
        /// <param name="a">System type.</param>
        /// <returns>New scalar array of type <see cref="InLogical"/>. 
        /// The only element has a value of <paramref name="a"/>.
        /// </returns>
        /// <remarks><para>The array returned will have a single element. However, the 
        /// number of dimensions depends on the current setting of <see cref="Settings.MinNumberOfArrayDimensions"/>.
        /// By default (value: 2) the array returned will be of size [1 x 1].</para></remarks>
        public static implicit operator InLogical(bool a) {
            var ret = LogicalStorage.Create(a).GetInArray();
            ret.Storage.NumberTrues = (a ? 1u : 0u);
            return ret;
        }

        /// <summary>
        /// Convert n-dimensional <see cref="System.Array"/> to ILNumerics array.
        /// </summary>
        /// <param name="A">System.Array of arbitrary size</param>
        /// <returns>If A is null: empty array. Otherwise a new ILNumerics array of the same size as A.</returns>
        /// <exception cref="InvalidCastException">If the type of the elements in A cannot get converted to <see cref="bool"/>.</exception>
        /// <exception cref="NotSupportedException">if As elements are reference types and A is a multidimensional array.</exception>
        /// <remarks><para>Elements of A will be converted to elements of the output array, where non-null elements (reference types) and 
        /// '!= 0' values (value types) in <paramref name="A"/> create 'true' values in the result. The following element types are 
        /// supported for <paramref name="A"/>: all <see cref="IConvertible"/> types (double, float, int, uint, long, ulong, short, ushort, sbyte, byte etc.),
        /// and reference types.</para> 
        /// <para>The resulting Array will reflect all dimensions of A. Due to the fact that .NET System.Arrays are stored in row major order 
        /// the storage layout of the resulting Array will be <see cref="StorageOrders.RowMajor"/> also. Elements of an one-dimensional <paramref name="A"/> 
        /// will be ordered along the first dimension of the result. Hence, one-dimensional input arrays will produce column vectors.</para>
        /// <para><see cref="System.Convert"/> is used for the conversion of elements in A to bool elements.</para>
        /// <para>The current value of <see cref="Settings.MinNumberOfArrayDimensions"/> is considered. Its default value of 2 will cause a 
        /// matrix to be produced, even though the input <paramref name="A"/> may be a one-dimensional array.</para>
        /// </remarks>
        /// <example>
        /// <code lang="C#" title="C# Code Example">
        /// <![CDATA[
        ///        // convert from bool[] array 
        ///        Logical A = new bool[] { true, true, false, true };
        ///        //A.T
        ///        //Logical[1, 4] True True ...
        ///        //[0]:  ▮ ▮ ▯ ▮
        ///        
        ///        // convert from double[] array
        ///        Logical B = new double[] { 1, 2, 0, 4 };
        ///        //B.T
        ///        //Logical[1, 4] True True ...
        ///        //[0]:  ▮ ▮ ▯ ▮
        ///        
        ///        // convert from 2dim float[,] array
        ///        Logical C = new float[,] {
        ///         { 9, 9, float.MaxValue, float.NegativeInfinity },
        ///         { 0, 0, float.NaN, 0 },
        ///        };
        ///        //C
        ///        //Logical[2, 4] True False ...
        ///        //[0]:  ▮ ▮ ▮ ▮
        ///        //[1]:  ▯ ▯ ▯ ▯
        ///
        ///        // convert from 2dim reference type array
        ///        Logical D = new[,] {
        ///         { new object(), new Double() + 1, new List<object>() },
        ///         { null, null, null }
        ///        };
        ///        //D
        ///        //Logical[2, 3] True False ...
        ///        //[0]:  ▮ ▮ ▮
        ///        //[1]:  ▯ ▯ ▯ 
        /// ]]>
        /// </code></example>
        
        public static unsafe implicit operator InLogical(Array A) {
            var host = DeviceManager.GetDevice(0) as HostDevice;

            System.Diagnostics.Debug.Assert(host != null);

            if (!typeof(bool).IsValueType && A.Rank > 1) {
                throw new NotSupportedException("Casting from reference type System.Array is only supported if the source array is one-dimensional. Use a one dimenisonal array and reshape the resulting Logical array!");
            }
            var handle = host.New<bool>((ulong)(object.Equals(A, null) ? 0 : A.LongLength));
            var ret = LogicalStorage.Create();
            ret.Handles[0] = handle;
            ret.Handles.CurrentDeviceIdx = 0;

            ret.NumberTrues = LogicalStorage.MarshalConvert2Bool(A, handle, ret.Size);

            return ret.GetInArray();
        }

        #endregion 

        #region conversional operators
        /// <summary>
        /// Converts output array to an input array (immutable).
        /// </summary>
        /// <param name="array">Source array.</param>
        /// <returns>Input array.</returns>
        public static implicit operator InLogical(OutLogical array) {
            if (object.Equals(array, null))
                return null;
            // we create a clone here in order to not to block the source against modifications
            var sto = array.Storage.Clone() as LogicalStorage;
            return sto.GetInArray(retain: false);
        }
        /// <summary>
        /// Converts local array to an input array (immutable).
        /// </summary>
        /// <param name="array">Source array.</param>
        /// <returns>Input array.</returns>
        public static implicit operator InLogical(Logical array) {
            if (object.Equals(array, null))
                return null;
            // we create a clone here in order to not to block the source against modifications
            var sto = array.Storage.Clone() as LogicalStorage;
            return sto.GetInArray(retain: false);
        }

        #endregion

        #region conversion to system boolean
        /// <summary>
        /// Convert scalar <see cref="Logical"/> array to <see cref="System.Boolean"/>.
        /// </summary>
        /// <param name="A">Source logical array. Must be scalar (i.e.: must have exactly one element).</param>
        /// <remarks><para>Empty arrays or null is also accepted for <paramref name="A"/>. <see langword="false"/> is returned in this case.</para></remarks>
        public static implicit operator bool(InLogical A) {
            if (object.Equals(A, null) || A.IsEmpty) {
                return false;
            }
            if (A.S.NumberOfElements > 1) {
                throw new InvalidCastException($"Only scalar logical arrays can be implicitly casted to System.Boolean. The incoming Logical has {A.S.NumberOfElements} elements. Bool conversion is ambiguous.");
            }
            return A.GetValue(0);
        }
        #endregion

        #endregion

        internal override void Release() {
            m_storage.Release();
        }

        /// <summary>
        /// Gives the number of elements evaluating to true.
        /// </summary>
        public long NumberTrues {
            get {
                return m_storage.NumberTrues;
            }
        }

        /// <summary>
        /// Returns a span providing access to this array's internal storage for reading. 
        /// </summary>
        /// <param name="order">[Optional] The order of elements returned. If order is null (default) the current value of <see cref="Settings.DefaultStorageOrder"/> is assumed, which depends on <see cref="Settings.ArrayStyle"/>.</param>
        /// <returns>A span with the exact length needed to hold all elements of this array, referencing the elements of this array, and having the same lifetime as this array.</returns>
        /// <remarks><para>Depending on the current internal storage layout this array is reordered internally as required
        /// before returning the span representing the elements in the requested storage order. </para>
        /// <para>Note, that for returning an array as span it must be ensured that the arrays elements are layed-out continously in memory. 
        /// Thus, the requested storage order is enforced in advance. This may or may not involve an internal copy of the elements of this array and a (transparent to the user) modification in its storage layout.</para>
        /// <para>The Span returned is only valid as long as this array is alive and before a subsequent modification is performed to this array.</para>
        /// </remarks>
        
        public ReadOnlySpan<bool> AsReadOnlySpan(StorageOrders? order = null) {
            return AsReadOnlySpanInternal(order);
        }


    }
}
