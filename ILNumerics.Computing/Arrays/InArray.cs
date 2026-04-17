using ILNumerics.Core.StorageLayer;
using ILNumerics.Core.Arrays;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ILNumerics.Core.DeviceManagement;
using System.Security;
using System.Threading;

namespace ILNumerics {

    /// <summary>
    /// Main input array type. Use this in a function signature indicating an immutable, dense input 
    /// array as function argument.
    /// </summary>
    /// <remarks>This </remarks>
    /// <typeparam name="T">Element type.</typeparam>
    public sealed partial class InArray<T>

        : Immutable<T, Array<T>, InArray<T>, OutArray<T>, Array<T>, Storage<T>> {

        internal InArray(Storage<T> storage) : base(storage) { }

        internal override void Release() {
            m_storage.Release();
        }

        #region implicit casts

        #region constructional operators

        /// <summary>
        /// Wraps single value into scalar array.
        /// </summary>
        /// <param name="a">System type.</param>
        /// <returns>New scalar array of type <see cref="InArray{T}"/>. 
        /// The only element has a value of <paramref name="a"/>.
        /// </returns>
        /// <remarks><para>The array returned will have a single element. However, the 
        /// number of dimensions depends on the current setting of <see cref="Settings.MinNumberOfArrayDimensions"/>.
        /// By default (value: 2) the array returned will be of size [1 x 1].</para></remarks>
        public static implicit operator InArray<T>(T a) {
            return Storage<T>.Create(a).GetInArray();
        }

        /// <summary>
        /// Implicitly convert n-dimensional System.Array to ILNumerics array.
        /// </summary>
        /// <param name="A">System.Array of arbitrary size</param>
        /// <returns>If A is null: empty array. Otherwise a new ILNumerics array of the same size as A.</returns>
        /// <exception cref="InvalidCastException">If the type of the elements in A cannot get converted 
        /// to <typeparamref name="T"/>.</exception>
        /// <exception cref="InvalidOperationException">If the type of the elements in A is not supported.</exception>
        /// <remarks><para>Elements of A will be copied to elements of the output array (shallow copy). The following element types are 
        /// supported for <paramref name="A"/> and <typeparamref name="T"/>: double, float, int, uint, long, ulong, short, ushort, sbyte, byte.</para> 
        /// <para>Alternatively, <paramref name="A"/> may consists out of scalar Arrays (or RetArray, InArray, OutArray) of one of the above element types.</para>
        /// <para>The resulting Array will reflect all dimensions of A. Due to the fact that .NET System.Arrays are stored in row major order and 
        /// ILNumerics stores array in column major (for compatibility with e.g. Matlab and Fortran) the resulting Array will have its dimensions reverted!
        /// For matrices this corresponds to a matrix transpose.</para>
        /// <para>System.Convert is used for the conversion of elements in A to destination elements. This includes widening and narrowing conversions. When, for example, 
        /// array elements of type double are provided and <typeparamref name="T"/> is Int32 the conversion will <b>round</b> the 
        /// source elements. See the examples below.</para></remarks>
        /// <example>
        /// <code lang="VB" title="VB Code Example">
        /// '' Elements of System.Value types:
        /// Dim A1 As Array(Of Double) = { 1, 2, 3 }  ' provide int elements
        /// Dim A2 As Array(Of Double) = { 1.0, 2.9, 3.4, 5.12 }  ' provide double elements
        /// Dim A As Double = 5
        /// Dim B As Double = 6
        /// Dim C As Double = 7
        /// 
        /// Dim A3 As Array(Of Double) = { A, B, C } ' provide double variables
        /// 
        /// Dim A4 As Array(Of Double) = { ILMath.cos(A), ILMath.tan(B) * C, C, 3 } ' provide mixed element types, including scalar Array  
        /// </code>
        /// <code lang="C#" title="C# Code Example">
        /// <![CDATA[
        /// Array<double> A1 = new[] { 1, ILMath.cos(2.0), 3, 4 };
        /// double B = -1, C = 10;
        /// Array<double> A2 = new[] { ILMath.cos(A1[1]), ILMath.tan(B) * C, C, 3 };
        /// // create from multidimensional System.Array 
        /// Array<int> A3 = new[,] { { 11, 12, 13 }, { 21, 22, 23 } };
        /// // narrowing conversion: from double to int (note the rounding rules!)
        /// Array<int> A4 = new[,] { { 11.9, 12.1, 13 }, { 21.5, 22.5, 23 } };
        /// //<Int32> [3,2]
        /// //    12         22 
        /// //    12         22 
        /// //    13         23 ]]>
        /// </code></example>
        
        public static implicit operator InArray<T>(System.Array A) {
            if (object.Equals(A, null)) {
                return null;
            }
            var host = DeviceManager.GetDevice(0) as HostDevice;
            System.Diagnostics.Debug.Assert(host != null);
            if (!typeof(T).IsValueType && A.Rank > 1) {
                throw new NotSupportedException("Casting from reference type System.Array is only supported if the source array is one-dimensional. Use a one dimenisonal array and reshape the Array<T>!");
            }

            var handle = host.New<T>((ulong)(object.Equals(A, null)? 0 : A.Length));
            var ret = Storage<T>.Create();
            ret.Handles[0] = handle;
            ret.Handles.CurrentDeviceIdx = 0; 
            Storage<T>.MarshalCopy(A, handle, ret.Size);

            return ret.GetInArray();
        }

        #endregion

        #region conversional operators 
        /// <summary>
        /// Convert persistent to input parameter array
        /// </summary>
        /// <param name="array">Persistent array</param>
        /// <returns>Input parameter array, will survive the current scope only</returns>
        public static implicit operator InArray<T>(Array<T> array) {
            if (object.Equals(array, null)) {
                return null;
            }
            var sto = array.Storage.Clone() as Storage<T>;
            return sto.GetInArray(retain: false);

            // version 5 scoping: LocalT and InT enter the scope during this conversion only.
            // They are freed after the current scope is left. If no current scope exists
            // they never enter any scope and are purely a matter of the GC. 

            // Once we have compiler support for scoping, investigate the various other 
            // attempts and their comments in the SVN repository for a working scheme! 
            // Revision 2172 came close...
        }
        /// <summary>
        /// Convert output parameter array to input parameter array
        /// </summary>
        /// <param name="array">Output parameter array</param>
        /// <returns>Input parameter array, will survive the current scope only</returns>
        public static implicit operator InArray<T>(OutArray<T> array) {
            if (object.Equals(array, null)) {
                return null;
            }
            // we create a clone here in order not to block the source against modifications.
            var sto = array.Storage.Clone() as Storage<T>; // Clone() returns  reference count of 1.
            return sto.GetInArray(retain: false);
        }
        #endregion

        #endregion

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
        
        public ReadOnlySpan<T> AsReadOnlySpan(StorageOrders? order = null) {
            return AsReadOnlySpanInternal(order);
        }

    }
}
