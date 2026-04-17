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
using System.Text;
using ILNumerics;
using ILNumerics.Core.Arrays;
using ILNumerics.Core.StorageLayer;
using static ILNumerics.Globals;



namespace ILNumerics.Core.Functions.Builtin {

    internal partial class MathInternal {


#if OBSOLETE
        /// <summary>
        /// Creates a cell row vector from given arrays. 
        /// </summary>
        /// <param name="arrays">Arrays to be stored inside the cell.</param>
        /// <returns>Cell vector with lazy, shallow copies of given arrays as elements.</returns>
        /// <remarks><para>The new cell will be created as vector, having the array 'values' given as parameter as cell elements. Those 
        /// elements are (lazy) copies of the provided arrays, hence are protected from changes from outside the cell.</para>
        /// <para>Visit the <a href="https://ilnumerics.net/$Cells.html" target="Main">online documentation</a> for cells.</para></remarks>
        /// <example>
        /// <para>A common use of the <c>cell</c> function is the concatenation of arrays and constants for subarray definitions, as shown in the following example:</para>
        /// <code>
        /// Array&lt;double> A = counter(4,3,2); 
        /// A
        /// //&lt;Double> [4,3]
        /// // 1          5          9 
        /// // 2          6         10 
        /// // 3          7         11 
        /// // 4          8         12 
        /// 
        /// // extract 1st, 2nd and last row:
        /// Array&lt;double> B = A[cell(0,1,end),full]; 
        /// B
        /// //&lt;Double> [3,3]
        /// // 1          5          9 
        /// // 2          6         10 
        /// // 4          8         12 
        /// </code>
        /// <para>Here, <c>cell</c> is used to concatenate individual indices determining the rows to select for the subarray. Using a cell here is convenient, because arbitrary types 
        /// can be stored in cells - integer, floating point types or special placeholders, like expressions (<see cref="Globals.end"/> and <see cref="Globals.full"/>).</para>
        /// </example>
        public static RetCell cell(BaseArray a0) {
            using (Scope.Enter(arrays)) {
                if (arrays == null)
                    return cell(Size.Empty00);
                return cell(new Size((uint)arrays.Length, 1), arrays);
            }
        }
#endif

        /// <summary>
        /// Creates new cell, initialize size and provide arrays for cell elements.
        /// </summary>
        /// <param name="size">[Optional] Size hint for the new cell array. Default: (null) Derive the size from <paramref name="arrays"/>.</param>
        /// <param name="arrays">[Optional] Enumerable of arrays for the cell elements, column major order. Default: (null) creates cell with all 'null' elements.</param>
        /// <param name="order">[Optional] Storage order for the new cell. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>Cell of specified size, initialized with given arrays.</returns>
        /// <remarks><para>If number of arrays given is smaller than the number of elements given by 
        /// <paramref name="size"/> then trailing elements in the cell returned are set to null.</para>
        /// <para>If <paramref name="size"/> is null a cell vector is produced with a length corresponding to the number of elements in <paramref name="arrays"/>.
        /// Performance wise it is recommended to specify the <paramref name="size"/> explicitly, though.</para>
        /// <para>If <paramref name="arrays"/> contains more elements than expected by <paramref name="size"/> an error is generated.</para>
        /// <para>The <see cref="size(long, long, long, long)"/> function or one of its overloads are
        /// convenient for the specification of dimensional size arguments.</para>
        /// <para>Visit the <a href="http://ilnumerics.net/$Cells.html" target="Main">online documentation</a> for cell.</para>
        /// <para>Note that when <paramref name="arrays"/> is an ILNumerics array (as in the example where <paramref name="arrays"/> 
        /// was created by the <see cref="vector{BaseArray}(BaseArray,BaseArray,BaseArray,BaseArray)"/> function) memory management 
        /// is performed accordingly: arrays stored in the <paramref name="arrays"/> object are released and the associated 
        /// memory is released to the pool after the <see cref="cell(InArray{long}, IEnumerable{BaseArray},StorageOrders)"/> function returns. 
        /// However, for <paramref name="arrays"/> of other (non-ILNumerics) types proper memory management is not guaranteed. For 
        /// example, when providing the target cell element values in a <see cref="System.Array"/> of <see cref="BaseArray"/> a copy 
        /// of individual elements is made by the <see cref="cell(InArray{long}, IEnumerable{BaseArray},StorageOrders)"/> function before storing 
        /// the new element into the cell object. The source object remains an element of the system array and is not released! </para>
        /// <para>Therefore, in situations where performance is critical and inside loops use one of the ILNumerics array intialization 
        /// functions for providing the <paramref name="arrays"/> argument. Otherwise, temporary objects in the <see cref="IEnumerable{BaseArray}"/> 
        /// may remain live and are left for the GC to clean up (which is fine, except for high performance demands).</para>
        /// </remarks>
        /// <example><![CDATA[
        /// <code>
        /// Array<double> A = rand(10,20,30); 
        /// Cell C = cell(size(3,2),vector<BaseArray>(A, A+1, zeros<float>(2,3), "4th element")); 
        /// //Alternatives: 
        /// //Cell C = cell(size(3,2),new [] { A, A+1, zeros(2,3) }); 
        /// //Cell C = cell(size(3,2),row(A, A+1, zeros(2,3))); 
        /// C
        ///>Cell [3,2]
        ///>    [0]:         
        ///>    [1]:     {<Double> [10,20,30]}         {<String> [1,1]}
        ///>    [2]:     {<Double> [10,20,30]}                   {null}
        ///>    [3]:          {<Single> [2,3]}                   {null}
        /// </code>]]>
        /// </example>
        /// <exception cref="ArgumentException"> if <paramref name="size"/> is not null and the number of elements found in <paramref name="arrays"/> exceeds 
        /// the expected number of elements as specified by <paramref name="size"/>.</exception>
        /// <seealso cref="cellv(BaseArray[])"/>
        /// <seealso cref="vector{T}(T, T, T, T, T)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.Reshape{IndT}(BaseArray{IndT}, StorageOrders?)"/>
        internal static Cell cell(InArray<long> size = null, IEnumerable<BaseArray> arrays = null, StorageOrders order = StorageOrders.ColumnMajor) {
            using (Scope.Enter(size)) { // allow both array styles! 
                var ret = CellStorage.Create();
                if (Equals(size, null)) {
                    if (!Equals(arrays, null)) {
                        ret.S.SetAll(dim0: 10L, order: order); // start large at 10, shrink potom
                    } else {
                        ret.S.SetAll(dim0: 0, order: order); 
                    }
                } else {
                    ret.S.SetAll(size, order);
                }
                
                ret.FromImplicitCast = false;
                ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<IStorage>((ulong)ret.S.NumberOfElements, clear: true);

                Cell R = ret.GetLocalArray();
                if (!object.Equals(arrays, null)) {

                    long i = 0;
                    foreach (var a in arrays) {
                        if (i >= R.S.NumberOfElements && !Equals(size, null)) {
                            throw new ArgumentException($"The number of elements provided in 'arrays' must not exceed the specified size for the new cell. Found: size={R.S.ToString()}. arrays.Count > {i}.");
                        }
                        using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {
                            R.SetValue(a, i++); // releases value if RetT, expands R when needed.
                        }
                    }
                    if (i < R.S.NumberOfElements) {
                        if (Equals(size, null)) { // initial size was set too large: cut away the tail
                            Cell shorter = R.Subarray(slice(0, i));
                            return shorter; // TODO: save local cell instance
                        } else {
                            // size was provided but fewer array values were given
                            // Clearing excess elements was done in New<IStorage>(clear: true).
                        }
                    }

                }
                return R;

            }
        }
        /// <summary>
        /// Create cell, initialize with arrays and size.
        /// </summary>
        /// <param name="values">Predefined system array with ILNumerics arrays as elements to be stored into the new cell.</param>
        /// <param name="size">Size of the new cell array.</param>
        /// <returns>Cell with size of <paramref name="size"/> and elements from <paramref name="values"/></returns>
        /// <remarks><para>The arrays given in <paramref name="values"/> are stored into the new cell as 
        /// individual cell elements. As for all cell elements, a (lazy) clone is made prior to 
        /// storing the arrays into the cell. Therefore, the arrays are properly protected from 
        /// changes from outside the cell.</para>
        /// <para>This function is deprecated and will be removed in a future version. Consider 
        /// using <see cref="cell(InArray{long}, IEnumerable{BaseArray},StorageOrders)"/> instead which does not
        /// rely on <c>params</c> arguments.</para>
        /// <para>Visit the <a href="http://ilnumerics.net/$Cells.html" target="Main">online documentation</a> for cell.</para></remarks>
        /// <seealso cref="cell(InArray{long}, IEnumerable{BaseArray},StorageOrders)"/>
        /// <seealso cref="vector{T}(T, T, T)"/>
        [Obsolete("Use cell(InArray<long> size,IEnumerable<BaseArray> arrays) instead!")]
        internal static Cell cell(BaseArray[] values, params int[] size) {
            using (Scope.Enter()) {
                return cell(toint64(vector(size)), values);
            }
        }

    }
}
