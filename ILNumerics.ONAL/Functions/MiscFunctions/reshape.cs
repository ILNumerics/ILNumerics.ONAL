using ILNumerics.Core.Arrays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.Functions.Builtin {
    internal static partial class MathInternal {
        /// <summary>
        /// Create reshaped, one dimensional version of <paramref name="A"/>. Flattens the array to a vector.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="d0">Length of the vector produced. This must be equal to <see cref="Size.NumberOfElements"/> or a negative number.</param>
        /// <param name="order">[Optional] Storage order for the new array. Default: null (<see cref="Settings.DefaultStorageOrder"/>).</param>
        /// <returns>A new array with the same values as this array, lined up in a 1-dimensional vector.</returns>
        /// <remarks><para>The (redundant) parameter <paramref name="d0"/> indicates the number of elements along the first 
        /// dimension for the returned array. If <paramref name="d0"/> is positive, its value must equal the number of
        /// elements in array <paramref name="A"/>. If <paramref name="d0"/> is negative, the correct number of elements is substituted automatically.</para>
        /// <para>The setting of <see cref="Settings.MinNumberOfArrayDimensions"/> is taken into account. 
        /// Therefore - by default - the array returned will have 2 dimensions, since the default value for 
        /// <see cref="Settings.MinNumberOfArrayDimensions"/> is 2.</para>
        /// <para>The storage order for both: the order for reading 
        /// the elements from this array and storing the element into the reshaped array is 
        /// determined as follows: </para>
        /// <list type="bullets">
        /// <item>If <paramref name="order"/> is specified (<see cref="StorageOrders.ColumnMajor"/>
        /// or <see cref="StorageOrders.RowMajor"/>) its value is used for the output. A copy is 
        /// made only if really needed.</item>
        /// <item>Otherwise, the value of <see cref="Settings.DefaultStorageOrder"/> determins the 
        /// target storage order.</item>
        /// </list>
        /// <para>A copy of the elements is required for non-continous arrays and - except for 
        /// vector shaped arrays - for unmatching order settings. Otherwise the returned 
        /// array references the same memory as this array with a modified size descriptor.</para>
        /// </remarks>
        /// <seealso cref="ILNumerics.Core.Arrays.ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Reshape(long, long, long, long, StorageOrders?)"/>
        /// <seealso cref="reshape{T}(BaseArray{T}, InArray{long}, StorageOrders?)"/>
        internal static Array<T> reshape<T>(BaseArray<T> A, long d0, StorageOrders? order = null) {
            {
                return (A as ConcreteArray<T,Array<T>,InArray<T>,OutArray<T>,Array<T>,StorageLayer.Storage<T>>).Reshape(d0, order);
            }
        }
        /// <summary>
        /// Create reshaped version of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="d0">Number of elements along dim 0.</param>
        /// <param name="d1">Number of elements along dim 1.</param>
        /// <param name="order">[Optional] Storage order for the output array. Default: 
        /// null (Settings.DefaultStorageOrder).</param>
        /// <returns>Reshaped array.</returns>
        /// <remarks><para>The storage order for both: the order for reading 
        /// the elements from this array and storing the element into the reshaped array is 
        /// determined as follows: </para>
        /// <list type="bullets">
        /// <item>If <paramref name="order"/> is specified as the values (<see cref="StorageOrders.ColumnMajor"/>
        /// or <see cref="StorageOrders.RowMajor"/>) its value is used for the output. A copy is 
        /// made only if really needed.</item>
        /// <item>Otherwise, the value of <see cref="Settings.DefaultStorageOrder"/> determins the 
        /// target storage order.</item>
        /// </list>
        /// <para>A copy of the elements is required for non-continous arrays and - except for 
        /// vector shaped arrays - for unmatching order settings. Otherwise the returned 
        /// array references the same memory as this array and uses another size descriptor only.</para>
        /// </remarks>
        /// <seealso cref="ILNumerics.Core.Arrays.ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Reshape(long, long, long, long, StorageOrders?)"/>
        /// <seealso cref="reshape{T}(BaseArray{T}, InArray{long}, StorageOrders?)"/>
        internal static Array<T> reshape<T>(BaseArray<T> A, long d0, long d1, StorageOrders? order = null) {
            {
                return (A as ConcreteArray<T,Array<T>,InArray<T>,OutArray<T>,Array<T>,StorageLayer.Storage<T>>).Reshape(d0, d1, order);
            }
        }
        /// <summary>
        /// A reshaped version of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="d0">Number of elements along dim 0.</param>
        /// <param name="d1">Number of elements along dim 1.</param>
        /// <param name="d2">Number of elements along dim 2.</param>
        /// <param name="order">[Optional] Storage order for the output array. Default: 
        /// null (Settings.DefaultStorageOrder).</param>
        /// <returns>Reshaped array.</returns>
        /// <remarks><para>The storage order for both: the order for reading 
        /// the elements from <paramref name="A"/> and storing the element into the reshaped array is 
        /// determined as follows: </para>
        /// <list type="bullets">
        /// <item>If <paramref name="order"/> is specified (<see cref="StorageOrders.ColumnMajor"/>
        /// or <see cref="StorageOrders.RowMajor"/>) its value is used for the output. A copy is 
        /// made only if really needed.</item>
        /// <item>Otherwise, the value of <see cref="Settings.DefaultStorageOrder"/> determins the 
        /// target storage order.</item>
        /// </list>
        /// <para>A copy of the elements is required for non-continous arrays and - except for 
        /// vector shaped arrays - for unmatching order settings. Otherwise the returned 
        /// array references the same memory as this array and uses another size descriptor only.</para>
        /// </remarks>
        /// <seealso cref="ILNumerics.Core.Arrays.ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Reshape(long, long, long, long, StorageOrders?)"/>
        /// <seealso cref="reshape{T}(BaseArray{T}, InArray{long}, StorageOrders?)"/>
        internal static Array<T> reshape<T>(BaseArray<T> A, long d0, long d1, long d2, StorageOrders? order = null) {
            using (Scope.Enter(A))
                return (A as ConcreteArray<T,Array<T>,InArray<T>,OutArray<T>,Array<T>,StorageLayer.Storage<T>>).Reshape(d0, d1, d2, order);
        }
        /// <summary>
        /// A reshaped version of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="d0">Number of elements along dim #0.</param>
        /// <param name="d1">Number of elements along dim #1.</param>
        /// <param name="d2">Number of elements along dim #2.</param>
        /// <param name="d3">Number of elements along dim #3.</param>
        /// <param name="order">[Optional] Storage order for the output array. Default: 
        /// null (Settings.DefaultStorageOrder).</param>
        /// <returns>Reshaped array.</returns>
        /// <remarks><para>The storage order for both: the order for reading 
        /// the elements from <paramref name="A"/> and storing the element into the reshaped array is 
        /// determined as follows: </para>
        /// <list type="bullets">
        /// <item>If <paramref name="order"/> is specified (<see cref="StorageOrders.ColumnMajor"/>
        /// or <see cref="StorageOrders.RowMajor"/>) its value is used for the output. A copy is 
        /// made only if really needed.</item>
        /// <item>Otherwise, the value of <see cref="Settings.DefaultStorageOrder"/> determins the 
        /// target storage order.</item>
        /// </list>
        /// <para>A copy of the elements is required for non-continous arrays and - except for 
        /// vector shaped arrays - for unmatching order settings. Otherwise the returned 
        /// array references the same memory as this array and uses another size descriptor only.</para>
        /// </remarks>
        /// <see cref="ILNumerics.Core.Arrays.ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Reshape(long, long, long, StorageOrders?)"/>
        internal static Array<T> reshape<T>(BaseArray<T> A, long d0, long d1, long d2, long d3, StorageOrders? order = null) {
            {
                return (A as ConcreteArray<T,Array<T>,InArray<T>,OutArray<T>,Array<T>,StorageLayer.Storage<T>>).Reshape(d0, d1, d2, d3, order);
            }
        }
        /// <summary>
        /// A reshaped version of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="d0">Number of elements along dim #0.</param>
        /// <param name="d1">Number of elements along dim #1.</param>
        /// <param name="d2">Number of elements along dim #2.</param>
        /// <param name="d3">Number of elements along dim #3.</param>
        /// <param name="d4">Number of elements along dim #4.</param>
        /// <param name="order">[Optional] Storage order for the output array. Default: 
        /// null (Settings.DefaultStorageOrder).</param>
        /// <returns>Reshaped array.</returns>
        /// <remarks><para>The storage order for both: the order for reading 
        /// the elements from <paramref name="A"/> and storing the element into the reshaped array is 
        /// determined as follows: </para>
        /// <list type="bullets">
        /// <item>If <paramref name="order"/> is specified (<see cref="StorageOrders.ColumnMajor"/>
        /// or <see cref="StorageOrders.RowMajor"/>) its value is used for the output. A copy is 
        /// made only if really needed.</item>
        /// <item>Otherwise, the value of <see cref="Settings.DefaultStorageOrder"/> determins the 
        /// target storage order.</item>
        /// </list>
        /// <para>A copy of the elements is required for non-continous arrays and - except for 
        /// vector shaped arrays - for unmatching order settings. Otherwise the returned 
        /// array references the same memory as this array and uses another size descriptor only.</para>
        /// </remarks>
        /// <seealso cref="ILNumerics.Core.Arrays.ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Reshape(long, long, long, long, StorageOrders?)"/>
        /// <seealso cref="reshape{T}(BaseArray{T}, InArray{long}, StorageOrders?)"/>
        internal static Array<T> reshape<T>(BaseArray<T> A, long d0, long d1, long d2, long d3, long d4, StorageOrders? order = null) {
            {
                return (A as ConcreteArray<T,Array<T>,InArray<T>,OutArray<T>,Array<T>,StorageLayer.Storage<T>>).Reshape(d0, d1, d2, d3, d4, order);
            }
        }
        /// <summary>
        /// A reshaped version of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="d0">Number of elements along dim #0.</param>
        /// <param name="d1">Number of elements along dim #1.</param>
        /// <param name="d2">Number of elements along dim #2.</param>
        /// <param name="d3">Number of elements along dim #3.</param>
        /// <param name="d4">Number of elements along dim #4.</param>
        /// <param name="d5">Number of elements along dim #5.</param>
        /// <param name="order">[Optional] Storage order for the output array. Default: 
        /// null (Settings.DefaultStorageOrder).</param>
        /// <returns>Reshaped array.</returns>
        /// <remarks><para>The storage order for both: the order for reading 
        /// the elements from <paramref name="A"/> and storing the element into the reshaped array is 
        /// determined as follows: </para>
        /// <list type="bullets">
        /// <item>If <paramref name="order"/> is specified (<see cref="StorageOrders.ColumnMajor"/>
        /// or <see cref="StorageOrders.RowMajor"/>) its value is used for the output. A copy is 
        /// made only if really needed.</item>
        /// <item>Otherwise, the value of <see cref="Settings.DefaultStorageOrder"/> determins the 
        /// target storage order.</item>
        /// </list>
        /// <para>A copy of the elements is required for non-continous arrays and - except for 
        /// vector shaped arrays - for unmatching order settings. Otherwise the returned 
        /// array references the same memory as this array and uses another size descriptor only.</para>
        /// </remarks>
        /// <seealso cref="ILNumerics.Core.Arrays.ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Reshape(long, long, long, long, StorageOrders?)"/>
        /// <seealso cref="reshape{T}(BaseArray{T}, InArray{long}, StorageOrders?)"/>
        internal static Array<T> reshape<T>(BaseArray<T> A, long d0, long d1, long d2, long d3, long d4, long d5, StorageOrders? order = null) {
            {
                return (A as ConcreteArray<T,Array<T>,InArray<T>,OutArray<T>,Array<T>,StorageLayer.Storage<T>>).Reshape(d0, d1, d2, d3, d4, d5, order);
            }
        }
        /// <summary>
        /// A reshaped version of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="d0">Number of elements along dim #0.</param>
        /// <param name="d1">Number of elements along dim #1.</param>
        /// <param name="d2">Number of elements along dim #2.</param>
        /// <param name="d3">Number of elements along dim #3.</param>
        /// <param name="d4">Number of elements along dim #4.</param>
        /// <param name="d5">Number of elements along dim #5.</param>
        /// <param name="d6">Number of elements along dim #6.</param>
        /// <param name="order">[Optional] Storage order for the output array. Default: 
        /// null (Settings.DefaultStorageOrder).</param>
        /// <returns>Reshaped array.</returns>
        /// <remarks><para>The storage order for both: the order for reading 
        /// the elements from <paramref name="A"/> and storing the element into the reshaped array is 
        /// determined as follows: </para>
        /// <list type="bullets">
        /// <item>If <paramref name="order"/> is specified (<see cref="StorageOrders.ColumnMajor"/>
        /// or <see cref="StorageOrders.RowMajor"/>) its value is used for the output. A copy is 
        /// made only if really needed.</item>
        /// <item>Otherwise, the value of <see cref="Settings.DefaultStorageOrder"/> determins the 
        /// target storage order.</item>
        /// </list>
        /// <para>A copy of the elements is required for non-continous arrays and - except for 
        /// vector shaped arrays - for unmatching order settings. Otherwise the returned 
        /// array references the same memory as this array and uses another size descriptor only.</para>
        /// </remarks>
        /// <seealso cref="ILNumerics.Core.Arrays.ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Reshape(long, long, long, long, StorageOrders?)"/>
        /// <seealso cref="reshape{T}(BaseArray{T}, InArray{long}, StorageOrders?)"/>
        internal static Array<T> reshape<T>(BaseArray<T> A, long d0, long d1, long d2, long d3, long d4, long d5, long d6, StorageOrders? order = null) {
            {
                return (A as ConcreteArray<T,Array<T>,InArray<T>,OutArray<T>,Array<T>,StorageLayer.Storage<T>>).Reshape(d0, d1, d2, d3, d4, d5, d6, order);
            }
        }
        /// <summary>
        /// A reshaped version of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="newdims">New dimension lengths.</param>
        /// <param name="order">[Optional] Storage order for the output array. Default: 
        /// null (Settings.DefaultStorageOrder).</param>
        /// <returns>Reshaped array.</returns>
        /// <remarks><para>The storage order for both: the order for reading 
        /// the elements from <paramref name="A"/> and storing the element into the reshaped array is 
        /// determined as follows: </para>
        /// <list type="bullets">
        /// <item>If <paramref name="order"/> is specified (<see cref="StorageOrders.ColumnMajor"/>
        /// or <see cref="StorageOrders.RowMajor"/>) its value is used for the output. A copy is 
        /// made only if really needed.</item>
        /// <item>Otherwise, the value of <see cref="Settings.DefaultStorageOrder"/> determins the 
        /// target storage order.</item>
        /// </list>
        /// <para>A copy of the elements is required for non-continous arrays and - except for 
        /// vector shaped arrays - for unmatching order settings. Otherwise the returned 
        /// array references the same memory as this array and uses another size descriptor only.</para>
        /// </remarks>
        /// <see cref="ILNumerics.Core.Arrays.ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Reshape(long, long, long, long, StorageOrders?)"/>
        internal static Array<T> reshape<T>(BaseArray<T> A, InArray<long> newdims, StorageOrders? order = null) {
            using (Scope.Enter(newdims)) {
                return (A as ConcreteArray<T,Array<T>,InArray<T>,OutArray<T>,Array<T>,StorageLayer.Storage<T>>).Reshape(newdims, order);
            }
        }

    }
}
