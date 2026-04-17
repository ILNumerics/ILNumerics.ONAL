using ILNumerics.Core.StorageLayer;
using ILNumerics.Core.Arrays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ILNumerics.Core.DeviceManagement;
using System.Security;

namespace ILNumerics {
    /// <summary>
    /// Local logical array type to be used when defining local array variables in custom algorithms. 
    /// </summary>
    /// <remarks>Logical arrays can be converted to numeric arrays by help the function 'touint8()'.</remarks>
    public sealed class Logical

        : Mutable<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>, IDisposable {

        #region construction
        internal Logical(
            LogicalStorage storage,
            object synch) 
            : base(storage, synch) { }

        #endregion

        #region conversions, cast operators

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
        public static implicit operator Logical(bool a) {
            var ret = LogicalStorage.Create(a).GetLocalArray();
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
        ///        //[1]:  ▯ ▯ ▯]]>
        /// </code></example>
        
        public static unsafe implicit operator Logical(System.Array A) {
            var host = DeviceManager.GetDevice(0) as HostDevice;

            System.Diagnostics.Debug.Assert(host != null);

            if (!typeof(bool).IsValueType && A.Rank > 1) {
                throw new NotSupportedException("Casting from reference type System.Array is only supported if the source array is one-dimensional. Use a one dimenisonal array and reshape the resulting Logical array!");
            }
            var handle = host.New<byte>((ulong)(object.Equals(A, null) ? 0 : A.LongLength));
            var ret =LogicalStorage.Create();
            ret.Handles[0] = handle;
            ret.Handles.CurrentDeviceIdx = 0;

            ret.NumberTrues = LogicalStorage.MarshalConvert2Bool(A, handle, ret.Size);
            
            return ret.GetLocalArray();
        }

        #endregion 

        #region conversional operators
        /// <summary>
        /// Convert input array (immutable) to local array (mutable).
        /// </summary>
        /// <param name="A">Input array</param>
        /// <returns>Local mutable array with lifespan corresponding to the current scope.</returns>
        /// <remarks>The new array is detached from the source array <paramref name="A"/>. The new array can 
        /// get used multiple times in the function scope and get altered without modifying the source array <paramref name="A"/>.</remarks>
        public static implicit operator Logical(InLogical A) {
            if (object.Equals(A, null))
                return null;
            var ret = LogicalStorage.Create(A.Storage.Handles, A.Storage.Size).GetLocalArray();  // Create() retains handles, .LocalArray retians array counter + registers in scope
            return ret;
        }
        /// <summary>
        /// Convert output array to a new local array (mutable).
        /// </summary>
        /// <param name="A">Array of OutArray type.</param>
        /// <returns>Local mutable array, detached from the source array <paramref name="A"/>, with lifespan corresponding to the current scope.</returns>
        /// <remarks>The new array is detached from this array. The new array can 
        /// get used multiple times in the function scope and get altered without altering the source array.</remarks>
        public static implicit operator Logical(OutLogical A) {
            if (object.Equals(A, null))
                return null;
            var ret = LogicalStorage.Create(A.Storage.Handles, A.Storage.Size).GetLocalArray();  // Create() retains, .Array registers in scope
            return ret;
        }

        #endregion

        #region conversion to system boolean
        /// <summary>
        /// Convert scalar <see cref="Logical"/> array to <see cref="System.Boolean"/>.
        /// </summary>
        /// <param name="A">Source logical array. Must be scalar (i.e.: must have exactly one element).</param>
        /// <remarks><para>Empty arrays or null is also accepted for <paramref name="A"/>. <see langword="false"/> is returned in this case.</para></remarks>
        public static implicit operator bool(Logical A) {
            if (object.Equals(A,null) || A.IsEmpty) {
                return false; 
            }
            if (A.S.NumberOfElements > 1) {
                throw new InvalidCastException($"Only scalar logical arrays can be implicitly casted to System.Boolean. The incoming Logical has {A.S.NumberOfElements} elements. Bool conversion is ambiguous."); 
            }
            return A.GetValue(0); 
        }
        #endregion

        #endregion

        /// <summary>
        /// Gives the number of elements evaluating to true.
        /// </summary>
        public long NumberTrues {
            get {
                return m_storage.NumberTrues;
            }
        }

    }
}
