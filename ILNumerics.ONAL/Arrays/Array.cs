using ILNumerics.Core.Arrays;
using ILNumerics.Core.DeviceManagement;
using ILNumerics.Core.StorageLayer;
using System;

namespace ILNumerics
{

    /// <summary>
    /// The main n-dimensional, rectilinear array to be used in algorithms. 
    /// </summary>
    /// <typeparam name="T">Inner type. This will mostly be one of the predefined numeric <see cref="System.ValueType"/> 
    /// types or one of the complex ILNumerics floating point types: <see cref="ILNumerics.complex"/> and <see cref="ILNumerics.fcomplex"/>.</typeparam>
    /// <remarks>This class serves as the main rectilinear array, holding arbitrary elements (usually numeric types or <see cref="BaseArray"/>) 
    /// in arbitrary dimensions.
    /// <para>Arrays of this type may use any type as generic element. However, common mathematical functions and operators 
    /// are defined for a limited number of value type elements only. All binary operations (+,-,*,/,<![CDATA[<,>,<=]]>,etc.) are 
    /// defined for two arrays with the same <i>numeric type</i>, would it be from the <c>System</c> namespace (<see cref="double"/>, 
    /// <see cref="int"/>, ...) or <see cref="ILNumerics.complex"/>/ <see cref="ILNumerics.fcomplex"/>. Most algebraic functions require floating point 
    /// types. See the <c>ILNumerics.ILMath</c> class for a list of all computational functions.</para>
    /// <para>Arrays are capable of creating flexible <a href="http://ilnumerics.net/$Subarray0.html" target="ILMain">subarrays</a> 
    /// and are mutable at runtime. Read about all details of ILNumerics arrays in the 
    /// <a href="http://ilnumerics.net/$Arrays.html" target="ILMain">ILNumerics Array documentation</a>.</para>
    /// <para>Arrays of this type are dense arrays. Cloning arrays is done as lazy 
    /// copy on write, i.e.: clones of existing arrays do only use new memory when attempting to alter one of them. Arrays integrate into the memory 
    /// management of ILNumerics. Read about the most <a href="http://ilnumerics.net/$GeneralRules.html" target="ILMain">important 
    /// simple rules</a> for using arrays in custom computational functions.</para>
    /// <para>Arrays come with overloaded mathematical operators, allowing for a convenient syntax. A 
    /// sophisticated memory management in the back will make sure, that as little memory is used as absolutely needed, even in 
    /// non-trivial expressions, like: a + c * 2 / abs(sin(c) * -b / log(a)). Here all arrays are of the same or broadcastable size. Evaluating 
    /// this expression does only need the memory of twice the size of one array. Memory gets collected and reused 
    /// for every subexpression evaluation. Further optimization options exist, as described in 
    /// <a href="http://ilnumerics.net/$PerfMemoryOpt.html" target="ILMain">Optimizing Algorithm Performance</a>.</para>
    /// </remarks>
    /// <example><para>A simple example demonstrating the uses of Array&lt;double&gt; in a very simple application:</para>
    /// <code>using System;
    ///using System.Collections.Generic;
    ///using System.Linq;
    ///using System.Text;
    ///using ILNumerics; 
    ///using static ILNumerics.ILMath;
    ///using static ILNumerics.Globals;
    ///
    ///namespace ConsoleApplication1 {
    ///    class Program {
    ///        static void Main(string[] args) {
    ///            using (Scope.Enter()) {
    ///                Array&lt;double> A = rand(10,20);
    ///                Array&lt;double> B = A * 30 + 100; 
    ///                Logical C = any(multiply(B,B.T)); 
    ///                Console.Out.Write(-B); 
    ///                Console.ReadKey(); 
    ///            }
    ///        }
    ///    }
    ///}
    ///</code>
    /// </example>
    /// <seealso cref="ILNumerics.Logical"/>
    /// <seealso cref="ILNumerics.Cell"/>
    /// <seealso href="http://ilnumerics.net/$Arrays.html"/>
    /// <seealso href="http://ilnumerics.net/$GeneralRules.html"/>
    /// <seealso href="http://ilnumerics.net/$Subarray0.html"/>
    [Serializable]
    public sealed partial class Array<T>

        : Mutable<T, Array<T>, InArray<T>, OutArray<T>, Array<T>, Storage<T>>, IDisposable { 

        #region construction
        internal Array(
            Storage<T> storage,
            object synch)
            : base(storage, synch) { }

        /// <summary>
        /// Use creation functions from ILNumerics.ILMath to create local arrays! Don't use new Array{T}()!
        /// </summary>
        private Array() : base(null, null) { }
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
        public static implicit operator Array<T>(T a) {
            return Storage<T>.Create(a).GetLocalArray(); 
        }

        /// <summary>
        /// Implicitly convert n-dimensional System.Array to ILNumerics array.
        /// </summary>
        /// <param name="A">System.Array of arbitrary size</param>
        /// <returns>If A is null: empty array. Otherwise a new ILNumerics array of the same size as A.</returns>
        /// <exception cref="InvalidCastException">If the type of the elements in A cannot get converted to <typeparamref name="T"/>.</exception>
        /// <exception cref="InvalidOperationException">If the type of the elements in A is not supported.</exception>
        /// <remarks><para>Elements of A will be copied to elements of the output array (shallow copy). The following element types are 
        /// supported for <paramref name="A"/> and <typeparamref name="T"/>: double, float, int, uint, long, ulong, short, ushort, sbyte, byte.</para> 
        /// <para>Alternatively, <paramref name="A"/> may consists out of scalar Arrays (or InArray / OutArray) of one of the above element types.</para>
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
        public static unsafe implicit operator Array<T>(Array A) {
            if (Equals(A,null)) {
                return null; 
            }
            var host = DeviceManager.GetDevice(0) as HostDevice;

            System.Diagnostics.Debug.Assert(host != null);

            if (!typeof(T).IsValueType && A.Rank > 1) {
                throw new NotSupportedException("Casting from reference type System.Array is only supported if the source array is one-dimensional. Use a one dimenisonal array and reshape the Array<T>!");
            }
            var handle = host.New<T>((ulong)(object.Equals(A, null) ? 0 : A.LongLength));
            var ret = Storage<T>.Create();
            ret.Handles[0] = handle;
            Storage<T>.MarshalCopy(A, handle, ret.Size);

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
        public static implicit operator Array<T>(InArray<T> A) {
            if (object.Equals(A, null))
                return null;
            // registers in scope + Retain().
            // Use Clone() to support pending states. Be cautious: it returns ref count 1!
            var ret = (A.Storage.Clone() as Storage<T>).GetLocalArray(retain: false);  
            return ret;
        }
        /// <summary>
        /// Convert output array to a new local array (mutable).
        /// </summary>
        /// <param name="A">Array of OutArray type.</param>
        /// <returns>Local mutable array, detached from the source array <paramref name="A"/>, with lifespan corresponding to the current scope.</returns>
        /// <remarks>The new array is detached from this array. The new array can 
        /// get used multiple times in the function scope and get altered without altering the source array.</remarks>
        public static implicit operator Array<T>(OutArray<T> A) {
            if (object.Equals(A, null))
                return null;
            // registers in scope + Retain().
            // Use Clone() to support pending states. Be cautious: it returns ref count 1!
            var ret = (A.Storage.Clone() as Storage<T>).GetLocalArray(retain: false);
            return ret;
        }

        #endregion
        
        #endregion

        #region memory management

        /// <summary>
        /// Indicate that this array is no longer be used. 
        /// </summary>
        /// <remarks>This decreases the reference counter of the array to the underlying storage and may release this storage eventually.
        /// <para>This function is marked as internal on purpose. Users should not deal with memory management manually. See: <see cref="BaseArray.Dispose()"/> 
        /// for one exception.</para></remarks>
        internal override void Release() {
            m_storage.Release();
        }

        #endregion

    }

}
