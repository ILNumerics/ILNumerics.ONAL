using ILNumerics.Core.Arrays;
using ILNumerics.Core.StorageLayer;
using System;
using static ILNumerics.Core.Functions.Builtin.MathInternal;

namespace ILNumerics {

    /// <summary>
    /// This class implements extension methods on the main array classes.
    /// </summary>
    public static partial class ExtensionMethods {

        /// <summary>
        /// Retrieves an ILNumerics array from a cell <paramref name="A"/> element of 'compatible' 
        /// element type. Only the target type is to be determined. 
        /// </summary>
        /// <typeparam name="T">Target element type. Must be a <see cref="ValueType"/>.</typeparam>
        /// <param name="i">Sequential element index into <paramref name="A"/></param>
        /// <param name="A">The source cell array.</param>
        /// <returns>Array retrieved at sequential index position <paramref name="i"/> in column major order. 
        /// Elements are converted into the target element type, if possible.</returns>
        /// <remarks><para>Note that the cell element position is specified as sequential index position only. Use <see cref="ind2sub{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, InArray{uint}, int)"/>
        /// in order to convert multidimensional indices into a sequential index.</para></remarks>
        public static Array<T> GetValueSeqAs<T>(this ConcreteArray<BaseArray,Cell, InCell, OutCell, Cell, CellStorage> A, long i)
            where T : struct {
            if (object.Equals(A, null)) {
                throw new NullReferenceException("The variable instance was null. Expected: cell type instance."); 
            }
            using (Scope.Enter()) {
                var storage = A.Storage;
                Array<T> ret = convert<T>(storage.GetValueSeq(i));
                return ret;
            }
        }
        /// <summary>
        /// Test if the cell element at a given sequential position is null (Nothing in VB).
        /// </summary>
        /// <param name="i">Sequential position of the cell <paramref name="A"/> element to test.</param>
        /// <param name="A">The cell array to test for a null element.</param>
        /// <returns>true if the cell does not store an element at cell position <paramref name="i"/>.</returns>
        public static bool IsNullAt(this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, long i) {
            var ret = Equals(A.GetValue(i),null);
            return ret;
        }

        /// <summary>
        /// Retrieves a clone of the value at the position <paramref name="d0"/> as an array.
        /// </summary>
        /// <param name="A">The cell instance.</param>
        /// <param name="d0">Index into the first dimension or sequential index into the array.</param>
        /// <returns>Copy/clone of the elements value. Scalar values are wrapped into a scalar array.</returns>
        /// <remarks><para>This functions retrieves the value of the element at <b>sequential</b> index <paramref name="d0"/>. Note that 
        /// the index provided in <paramref name="d0"/> does not correspond to the storage position of the element to retrieve. This 
        /// would only be true for special cases of storage layout and dimension numbers. For arbitrary storage layouts both are likely to differ.</para> 
        /// <para>The type of the array returned corresponds to the type of the element addressed by <paramref name="d0"/>. Use 
        /// <see cref="ExtensionMethods.GetValue{T}(BaseArray{BaseArray}, long)"/> in order to retrieve the element as its natural type. 
        /// <see cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long)"/> can be used to inspect 
        /// the element type.</para>
        /// <para>Note, that the value addressed is not wrapped into a scalar cell before returning as would be the case for indexers on cells, 
        /// which always return cells. Thus the return value might be a cell or an ILNumerics array of any element type.
        /// Commonly, the value returned will be a volatile return type array. Thus, it will be released after the first use.</para>
        /// </remarks>
        /// <seealso cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long)"/>
        /// <seealso cref="ToArray{T}(BaseArray)"/>
        /// <seealso cref="GetValue{T}(BaseArray{BaseArray}, long, long)"/>
        /// <seealso cref="Size.GetSeqIndex(uint)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="IndexOutOfRangeException">if the value of <paramref name="d0"/> exceeds the <see cref="Size.NumberOfElements"/> of <paramref name="A"/>.</exception>
        [Obsolete("Use A.GetValue(d0) on cell array A instead!")]
        public unsafe static BaseArray GetBaseArray(this BaseArray<BaseArray> A, long d0) {
            return A.GetValue(d0); 
        }

    }
}
