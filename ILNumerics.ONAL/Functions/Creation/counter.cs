//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.Functions.Builtin {
    internal static partial class MathInternal {

        #region double
        /// <summary>
        /// Creates a new ILNumerics array with regularly spaced element values in double precision.
        /// </summary>
        /// <param name="start">The start value for the new elements.</param>
        /// <param name="step">The step size value for the new elements.</param>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="order">[Optional] The storage order for the new array. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>New ILNumerics array initialized with linearly spaced elements according to <paramref name="start"/> and counting up/downwards by <paramref name="step"/> along the storage order.</returns>
        internal static Array<double> counter(double start, double step, long dim0, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = StorageLayer.Storage<double>.Create();
            ret.S.SetAll(dim0: dim0, order: order);
            return counterInternal(start, step, ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array with regularly spaced element values, double precision.
        /// </summary>
        /// <param name="start">The start value for the new elements.</param>
        /// <param name="step">The step size value for the new elements.</param>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="order">[Optional] The storage order for the new array. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>New ILNumerics array initialized with linearly spaced elements according to <paramref name="start"/> and counting up/downwards by <paramref name="step"/> along the storage order.</returns>
        internal static Array<double> counter(double start, double step, long dim0, long dim1, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = StorageLayer.Storage<double>.Create();
            ret.S.SetAll(dim0, dim1, order: order);
            return counterInternal(start, step, ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array with regularly spaced element values, double precision.
        /// </summary>
        /// <param name="start">The start value for the new elements.</param>
        /// <param name="step">The step size value for the new elements.</param>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="order">[Optional] The storage order for the new array. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>New ILNumerics array initialized with linearly spaced elements according to <paramref name="start"/> and counting up/downwards by <paramref name="step"/> along the storage order.</returns>
        internal static Array<double> counter(double start, double step, long dim0, long dim1, long dim2, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = StorageLayer.Storage<double>.Create();
            ret.S.SetAll(dim0, dim1, dim2, order: order);
            return counterInternal(start, step, ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array with regularly spaced element values, double precision.
        /// </summary>
        /// <param name="start">The start value for the new elements.</param>
        /// <param name="step">The step size value for the new elements.</param>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="dim3">Length of dimension #3.</param>
        /// <param name="order">[Optional] The storage order for the new array. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>New ILNumerics array initialized with linearly spaced elements according to <paramref name="start"/> and counting up/downwards by <paramref name="step"/> along the storage order.</returns>
        internal static Array<double> counter(double start, double step, long dim0, long dim1, long dim2, long dim3, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = StorageLayer.Storage<double>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, order: order);
            return counterInternal(start, step, ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array with regularly spaced element values, double precision.
        /// </summary>
        /// <param name="start">The start value for the new elements.</param>
        /// <param name="step">The step size value for the new elements.</param>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="dim3">Length of dimension #3.</param>
        /// <param name="dim4">Length of dimension #4.</param>
        /// <param name="order">[Optional] The storage order for the new array. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>New ILNumerics array initialized with linearly spaced elements according to <paramref name="start"/> and counting up/downwards by <paramref name="step"/> along the storage order.</returns>
        internal static Array<double> counter(double start, double step, long dim0, long dim1, long dim2, long dim3, long dim4, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = StorageLayer.Storage<double>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, dim4, order: order);
            return counterInternal(start, step, ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array with regularly spaced element values, double precision.
        /// </summary>
        /// <param name="start">The start value for the new elements.</param>
        /// <param name="step">The step size value for the new elements.</param>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="dim3">Length of dimension #3.</param>
        /// <param name="dim4">Length of dimension #4.</param>
        /// <param name="dim5">Length of dimension #5.</param>
        /// <param name="order">[Optional] The storage order for the new array. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>New ILNumerics array initialized with linearly spaced elements according to <paramref name="start"/> and counting up/downwards by <paramref name="step"/> along the storage order.</returns>
        internal static Array<double> counter(double start, double step, long dim0, long dim1, long dim2, long dim3, long dim4, long dim5, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = StorageLayer.Storage<double>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, dim4, dim5, order: order);
            return counterInternal(start, step, ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array with regularly spaced element values, double precision.
        /// </summary>
        /// <param name="start">The start value for the new elements.</param>
        /// <param name="step">The step size value for the new elements.</param>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="dim3">Length of dimension #3.</param>
        /// <param name="dim4">Length of dimension #4.</param>
        /// <param name="dim5">Length of dimension #5.</param>
        /// <param name="dim6">Length of dimension #6.</param>
        /// <param name="order">[Optional] The storage order for the new array. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>New ILNumerics array initialized with linearly spaced elements according to <paramref name="start"/> and counting up/downwards by <paramref name="step"/> along the storage order.</returns>
        internal static Array<double> counter(double start, double step, long dim0, long dim1, long dim2, long dim3, long dim4, long dim5, long dim6, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = StorageLayer.Storage<double>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, dim4, dim5, dim6, order: order);
            return counterInternal(start, step, ret);
        }
        #endregion

        #region deprecated, compatibility versions
        /// <summary>
        /// Create an array with regularly (stepsize 1) spaced elements in multiple dimensions of provided lenghts. 
        /// </summary>
        /// <param name="sizes">Dimension lengths.</param>
        /// <returns>New ILNumerics array, initialized with elements starting at 1, increasing by 1 for each next element along the columns.</returns>
        [Obsolete("Use counter<T>(T,T,long,...) instead, specifying output type T, start, step and sizes explicitly! This overload is kept for compatibility only and will soon be removed!")]
        internal static Array<double> counter(params int[] sizes) {
            if (sizes == null || sizes.Length == 0) {
                throw new ArgumentException($"counter(): '{nameof(sizes)}' argument may not be null."); 
            }
            switch (sizes.Length) {
                case 1:
                    return counter<double>(1.0, 1.0, sizes[0], StorageOrders.ColumnMajor);
                case 2:
                    return counter<double>(1.0, 1.0, sizes[0], sizes[1], StorageOrders.ColumnMajor);
                case 3:
                    return counter<double>(1.0, 1.0, sizes[0], sizes[1], sizes[2], StorageOrders.ColumnMajor);
                case 4:
                    return counter<double>(1.0, 1.0, sizes[0], sizes[1], sizes[2], sizes[3], StorageOrders.ColumnMajor);
                case 5:
                    return counter<double>(1.0, 1.0, sizes[0], sizes[1], sizes[2], sizes[3], sizes[4], StorageOrders.ColumnMajor);
                case 6:
                    return counter<double>(1.0, 1.0, sizes[0], sizes[1], sizes[2], sizes[3], sizes[4], sizes[5], StorageOrders.ColumnMajor);
                case 7:
                    return counter<double>(1.0, 1.0, sizes[0], sizes[1], sizes[2], sizes[3], sizes[4], sizes[5], sizes[6], StorageOrders.ColumnMajor);
                default:
                    throw new ArgumentException($"The number of dimensions currently supported is limited to 7. Found: {sizes.Length}."); 
            }
        }
        #endregion

        #region generic (numeric)
        /// <summary>
        /// Creates a new ILNumerics array with regularly spaced element values.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="start">The start value for the new elements.</param>
        /// <param name="step">The step size value for the new elements.</param>
        /// <param name="size">Dimension lengths as vector.</param>
        /// <param name="order">[Optional] The storage order for the new array. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>New ILNumerics array initialized with linearly spaced elements according to <paramref name="start"/> and counting up/downwards by <paramref name="step"/> along the storage order.</returns>
        /// <remarks><para>The <see cref="counter{T}(T, T, InArray{long}, StorageOrders)"/> function and the corresponding
        /// overloads for providing individual dimension length parameters <see cref="counter{T}(T, T, long, StorageOrders)"/>,... 
        /// create n-dimensional numeric arrays with automatically generated values based on a given <paramref name="start"/> value and a <paramref name="step"/> value. 
        /// The size of the new array is determined by the <paramref name="size"/> parameter(s). The firsts element value is assigned 
        /// the <paramref name="start"/> value. Subsequent values are computed by adding the value of <paramref name="step"/> to each elements predecessor.</para>
        /// <para>The term 'predecessor' here means: the predecessor E0 of element E1 is the element having an index I(E0) of I(E1) - 1, 
        /// where the index I() corresponds to the storage location of the element in memory, relative to the first element.</para>
        /// <para>The storage order is determined by <paramref name="order"/>. By default, elements are lined up in memory 
        /// in column major storage order. <paramref name="order"/> must be one out of <see cref="StorageOrders.ColumnMajor"/> 
        /// or <see cref="StorageOrders.RowMajor"/>. Note, how the storage order affects the order of values in the new array.</para></remarks>
        /// <example>
        /// <para>Creating a new array of size [4,3], filled with upwards counting values, starting at 1.</para>
        /// <code><![CDATA[Array<double> A = counter<double>(1, 1, 4, 3);
        /// //<Double> [4,3] 1...12 |
        /// //    [0]:           1          5          9
        /// //    [1]:           2          6         10
        /// //    [2]:           3          7         11
        /// //    [3]:           4          8         12]]>
        ///</code> 
        ///<para>Creating a new array of size [4,3,2] of ushort values: </para>
        ///<code><![CDATA[counter<sbyte>(10, -10, 4,3,2)
        /// //<SByte> [4,3] 1...12 |
        /// //    [0]: (:,:,0)
        /// //    [1]:    10  -30  -70
        /// //    [2]:     0  -40  -80
        /// //    [3]:   -10  -50  -90
        /// //    [4]:   -20  -60 -100
        /// //    [5]: (:,:,1)
        /// //    [6]:  -110  106   66
        /// //    [7]:  -120   96   56
        /// //    [8]:   126   86   46
        /// //    [9]:   116   76   36]]></code> 
        /// <para>Note, how the computed values of data type 'sbyte' wrap around at the limits of 'sbyte' 
        /// without notice!</para>
        ///<para>Creating a new array of size [4,3] with upwards counting values in row major order: </para>
        ///<code><![CDATA[counter<float>(1, 1, 4,3, StorageOrders.RowMajor)
        /// //<Single> [4,3] 1...12 -
        /// //    [0]:           1          2          3
        /// //    [1]:           4          5          6
        /// //    [2]:           7          8          9
        /// //    [3]:          10         11         12
        /// //]]></code> 
        /// </example>
        internal static Array<T> counter<T>(T start, T step, InArray<long> size, StorageOrders order = StorageOrders.ColumnMajor) where T : struct, IConvertible, IEquatable<T>  {

            Storage<T> ret = Storage<T>.Create(size, order);
            return counterInternal(start, step, ret);
        }

        /// <summary>
        /// Creates a new ILNumerics array with regularly spaced element values.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="start">The start value for the new elements.</param>
        /// <param name="step">The step size value for the new elements.</param>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="order">[Optional] The storage order for the new array. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>New ILNumerics array initialized with linearly spaced elements according to <paramref name="start"/> and counting up/downwards by <paramref name="step"/> along the storage order.</returns>
        /// <remarks><para>The <see cref="counter{T}(T, T, InArray{long}, StorageOrders)"/> function and the corresponding
        /// overloads for providing individual dimension length parameters <see cref="counter{T}(T, T, long, StorageOrders)"/>,... 
        /// create n-dimensional numeric arrays with automatically generated values based on a given <paramref name="start"/> value and a <paramref name="step"/> value. 
        /// The size of the new array is determined by the 'size' parameter(s). The firsts element value is assigned 
        /// the <paramref name="start"/> value. Subsequent values are computed by adding the value of <paramref name="step"/> to each elements predecessor.</para>
        /// <para>The term 'predecessor' here means: the predecessor E0 of element E1 is the element having an index I(E0) of I(E1) - 1, 
        /// where the index I() corresponds to the storage location of the element in memory, relative to the first element.</para>
        /// <para>The storage order is determined by <paramref name="order"/>. By default, elements are lined up in memory 
        /// in column major storage order. <paramref name="order"/> must be one out of <see cref="StorageOrders.ColumnMajor"/> 
        /// or <see cref="StorageOrders.RowMajor"/>. Note, how the storage order affects the order of values in the new array.</para></remarks>
        /// <example>
        /// <para>Creating a new array of size [4,3], filled with upwards counting values, starting at 1.</para>
        /// <code><![CDATA[Array<double> A = counter<double>(1, 1, 4, 3);
        /// //<Double> [4,3] 1...12 |
        /// //    [0]:           1          5          9
        /// //    [1]:           2          6         10
        /// //    [2]:           3          7         11
        /// //    [3]:           4          8         12]]>
        ///</code> 
        ///<para>Creating a new array of size [4,3,2] of ushort values: </para>
        ///<code><![CDATA[counter<sbyte>(10, -10, 4,3,2)
        /// //<SByte> [4,3] 1...12 |
        /// //    [0]: (:,:,0)
        /// //    [1]:    10  -30  -70
        /// //    [2]:     0  -40  -80
        /// //    [3]:   -10  -50  -90
        /// //    [4]:   -20  -60 -100
        /// //    [5]: (:,:,1)
        /// //    [6]:  -110  106   66
        /// //    [7]:  -120   96   56
        /// //    [8]:   126   86   46
        /// //    [9]:   116   76   36]]></code> 
        /// <para>Note, how the computed values of data type 'sbyte' wrap around at the limits of 'sbyte' 
        /// without notice!</para>
        ///<para>Creating a new array of size [4,3] with upwards counting values in row major order: </para>
        ///<code><![CDATA[counter<float>(1, 1, 4,3, StorageOrders.RowMajor)
        /// //<Single> [4,3] 1...12 -
        /// //    [0]:           1          2          3
        /// //    [1]:           4          5          6
        /// //    [2]:           7          8          9
        /// //    [3]:          10         11         12
        /// //]]></code> 
        /// </example>
        internal static Array<T> counter<T>(T start, T step, long dim0, StorageOrders order = StorageOrders.ColumnMajor) where T : struct, IConvertible, IEquatable<T>  {

            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0: dim0, order: order);
            return counterInternal(start, step, ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array with regularly spaced element values.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="start">The start value for the new elements.</param>
        /// <param name="step">The step size value for the new elements.</param>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="order">[Optional] The storage order for the new array. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>New ILNumerics array initialized with linearly spaced elements according to <paramref name="start"/> and counting up/downwards by <paramref name="step"/> along the storage order.</returns>
        /// <remarks><para>The <see cref="counter{T}(T, T, InArray{long}, StorageOrders)"/> function and the corresponding
        /// overloads for providing individual dimension length parameters <see cref="counter{T}(T, T, long, StorageOrders)"/>,... 
        /// create n-dimensional numeric arrays with automatically generated values based on a given <paramref name="start"/> value and a <paramref name="step"/> value. 
        /// The size of the new array is determined by the 'size' parameter(s). The firsts element value is assigned 
        /// the <paramref name="start"/> value. Subsequent values are computed by adding the value of <paramref name="step"/> to each elements predecessor.</para>
        /// <para>The term 'predecessor' here means: the predecessor E0 of element E1 is the element having an index I(E0) of I(E1) - 1, 
        /// where the index I() corresponds to the storage location of the element in memory, relative to the first element.</para>
        /// <para>The storage order is determined by <paramref name="order"/>. By default, elements are lined up in memory 
        /// in column major storage order. <paramref name="order"/> must be one out of <see cref="StorageOrders.ColumnMajor"/> 
        /// or <see cref="StorageOrders.RowMajor"/>. Note, how the storage order affects the order of values in the new array.</para></remarks>
        /// <example>
        /// <para>Creating a new array of size [4,3], filled with upwards counting values, starting at 1.</para>
        /// <code><![CDATA[Array<double> A = counter<double>(1, 1, 4, 3);
        /// //<Double> [4,3] 1...12 |
        /// //    [0]:           1          5          9
        /// //    [1]:           2          6         10
        /// //    [2]:           3          7         11
        /// //    [3]:           4          8         12]]>
        ///</code> 
        ///<para>Creating a new array of size [4,3,2] of ushort values: </para>
        ///<code><![CDATA[counter<sbyte>(10, -10, 4,3,2)
        /// //<SByte> [4,3] 1...12 |
        /// //    [0]: (:,:,0)
        /// //    [1]:    10  -30  -70
        /// //    [2]:     0  -40  -80
        /// //    [3]:   -10  -50  -90
        /// //    [4]:   -20  -60 -100
        /// //    [5]: (:,:,1)
        /// //    [6]:  -110  106   66
        /// //    [7]:  -120   96   56
        /// //    [8]:   126   86   46
        /// //    [9]:   116   76   36]]></code> 
        /// <para>Note, how the computed values of data type 'sbyte' wrap around at the limits of 'sbyte' 
        /// without notice!</para>
        ///<para>Creating a new array of size [4,3] with upwards counting values in row major order: </para>
        ///<code><![CDATA[counter<float>(1, 1, 4,3, StorageOrders.RowMajor)
        /// //<Single> [4,3] 1...12 -
        /// //    [0]:           1          2          3
        /// //    [1]:           4          5          6
        /// //    [2]:           7          8          9
        /// //    [3]:          10         11         12
        /// //]]></code> 
        /// </example>
        internal static Array<T> counter<T>(T start, T step, long dim0, long dim1, StorageOrders order = StorageOrders.ColumnMajor) where T : struct, IConvertible, IEquatable<T> {

            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, order: order);
            return counterInternal<T>(start, step, ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array with regularly spaced element values.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="start">The start value for the new elements.</param>
        /// <param name="step">The step size value for the new elements.</param>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="order">[Optional] The storage order for the new array. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>New ILNumerics array initialized with linearly spaced elements according to <paramref name="start"/> and counting up/downwards by <paramref name="step"/> along the storage order.</returns>
        /// <remarks><para>The <see cref="counter{T}(T, T, InArray{long}, StorageOrders)"/> function and the corresponding
        /// overloads for providing individual dimension length parameters <see cref="counter{T}(T, T, long, StorageOrders)"/>,... 
        /// create n-dimensional numeric arrays with automatically generated values based on a given <paramref name="start"/> value and a <paramref name="step"/> value. 
        /// The size of the new array is determined by the 'size' parameter(s). The firsts element value is assigned 
        /// the <paramref name="start"/> value. Subsequent values are computed by adding the value of <paramref name="step"/> to each elements predecessor.</para>
        /// <para>The term 'predecessor' here means: the predecessor E0 of element E1 is the element having an index I(E0) of I(E1) - 1, 
        /// where the index I() corresponds to the storage location of the element in memory, relative to the first element.</para>
        /// <para>The storage order is determined by <paramref name="order"/>. By default, elements are lined up in memory 
        /// in column major storage order. <paramref name="order"/> must be one out of <see cref="StorageOrders.ColumnMajor"/> 
        /// or <see cref="StorageOrders.RowMajor"/>. Note, how the storage order affects the order of values in the new array.</para></remarks>
        /// <example>
        /// <para>Creating a new array of size [4,3], filled with upwards counting values, starting at 1.</para>
        /// <code><![CDATA[Array<double> A = counter<double>(1, 1, 4, 3);
        /// //<Double> [4,3] 1...12 |
        /// //    [0]:           1          5          9
        /// //    [1]:           2          6         10
        /// //    [2]:           3          7         11
        /// //    [3]:           4          8         12]]>
        ///</code> 
        ///<para>Creating a new array of size [4,3,2] of ushort values: </para>
        ///<code><![CDATA[counter<sbyte>(10, -10, 4,3,2)
        /// //<SByte> [4,3] 1...12 |
        /// //    [0]: (:,:,0)
        /// //    [1]:    10  -30  -70
        /// //    [2]:     0  -40  -80
        /// //    [3]:   -10  -50  -90
        /// //    [4]:   -20  -60 -100
        /// //    [5]: (:,:,1)
        /// //    [6]:  -110  106   66
        /// //    [7]:  -120   96   56
        /// //    [8]:   126   86   46
        /// //    [9]:   116   76   36]]></code> 
        /// <para>Note, how the computed values of data type 'sbyte' wrap around at the limits of 'sbyte' 
        /// without notice!</para>
        ///<para>Creating a new array of size [4,3] with upwards counting values in row major order: </para>
        ///<code><![CDATA[counter<float>(1, 1, 4,3, StorageOrders.RowMajor)
        /// //<Single> [4,3] 1...12 -
        /// //    [0]:           1          2          3
        /// //    [1]:           4          5          6
        /// //    [2]:           7          8          9
        /// //    [3]:          10         11         12
        /// //]]></code> 
        /// </example>
        internal static Array<T> counter<T>(T start, T step, long dim0, long dim1, long dim2, StorageOrders order = StorageOrders.ColumnMajor) where T : struct, IConvertible, IEquatable<T> {

            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, dim2, order: order);
            return counterInternal<T>(start, step, ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array with regularly spaced element values.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="start">The start value for the new elements.</param>
        /// <param name="step">The step size value for the new elements.</param>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="dim3">Length of dimension #3.</param>
        /// <param name="order">[Optional] The storage order for the new array. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>New ILNumerics array initialized with linearly spaced elements according to <paramref name="start"/> and counting up/downwards by <paramref name="step"/> along the storage order.</returns>
        /// <remarks><para>The <see cref="counter{T}(T, T, InArray{long}, StorageOrders)"/> function and the corresponding
        /// overloads for providing individual dimension length parameters <see cref="counter{T}(T, T, long, StorageOrders)"/>,... 
        /// create n-dimensional numeric arrays with automatically generated values based on a given <paramref name="start"/> value and a <paramref name="step"/> value. 
        /// The size of the new array is determined by the 'size' parameter(s). The firsts element value is assigned 
        /// the <paramref name="start"/> value. Subsequent values are computed by adding the value of <paramref name="step"/> to each elements predecessor.</para>
        /// <para>The term 'predecessor' here means: the predecessor E0 of element E1 is the element having an index I(E0) of I(E1) - 1, 
        /// where the index I() corresponds to the storage location of the element in memory, relative to the first element.</para>
        /// <para>The storage order is determined by <paramref name="order"/>. By default, elements are lined up in memory 
        /// in column major storage order. <paramref name="order"/> must be one out of <see cref="StorageOrders.ColumnMajor"/> 
        /// or <see cref="StorageOrders.RowMajor"/>. Note, how the storage order affects the order of values in the new array.</para></remarks>
        /// <example>
        /// <para>Creating a new array of size [4,3], filled with upwards counting values, starting at 1.</para>
        /// <code><![CDATA[Array<double> A = counter<double>(1, 1, 4, 3);
        /// //<Double> [4,3] 1...12 |
        /// //    [0]:           1          5          9
        /// //    [1]:           2          6         10
        /// //    [2]:           3          7         11
        /// //    [3]:           4          8         12]]>
        ///</code> 
        ///<para>Creating a new array of size [4,3,2] of ushort values: </para>
        ///<code><![CDATA[counter<sbyte>(10, -10, 4,3,2)
        /// //<SByte> [4,3] 1...12 |
        /// //    [0]: (:,:,0)
        /// //    [1]:    10  -30  -70
        /// //    [2]:     0  -40  -80
        /// //    [3]:   -10  -50  -90
        /// //    [4]:   -20  -60 -100
        /// //    [5]: (:,:,1)
        /// //    [6]:  -110  106   66
        /// //    [7]:  -120   96   56
        /// //    [8]:   126   86   46
        /// //    [9]:   116   76   36]]></code> 
        /// <para>Note, how the computed values of data type 'sbyte' wrap around at the limits of 'sbyte' 
        /// without notice!</para>
        ///<para>Creating a new array of size [4,3] with upwards counting values in row major order: </para>
        ///<code><![CDATA[counter<float>(1, 1, 4,3, StorageOrders.RowMajor)
        /// //<Single> [4,3] 1...12 -
        /// //    [0]:           1          2          3
        /// //    [1]:           4          5          6
        /// //    [2]:           7          8          9
        /// //    [3]:          10         11         12
        /// //]]></code> 
        /// </example>
        internal static Array<T> counter<T>(T start, T step, long dim0, long dim1, long dim2, long dim3, StorageOrders order = StorageOrders.ColumnMajor) where T : struct, IConvertible, IEquatable<T> {

            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, order: order);
            return counterInternal<T>(start, step, ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array with regularly spaced element values.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="start">The start value for the new elements.</param>
        /// <param name="step">The step size value for the new elements.</param>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="dim3">Length of dimension #3.</param>
        /// <param name="dim4">Length of dimension #4.</param>
        /// <param name="order">[Optional] The storage order for the new array. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>New ILNumerics array initialized with linearly spaced elements according to <paramref name="start"/> and counting up/downwards by <paramref name="step"/> along the storage order.</returns>
        /// <remarks><para>The <see cref="counter{T}(T, T, InArray{long}, StorageOrders)"/> function and the corresponding
        /// overloads for providing individual dimension length parameters <see cref="counter{T}(T, T, long, StorageOrders)"/>,... 
        /// create n-dimensional numeric arrays with automatically generated values based on a given <paramref name="start"/> value and a <paramref name="step"/> value. 
        /// The size of the new array is determined by the 'size' parameter(s). The firsts element value is assigned 
        /// the <paramref name="start"/> value. Subsequent values are computed by adding the value of <paramref name="step"/> to each elements predecessor.</para>
        /// <para>The term 'predecessor' here means: the predecessor E0 of element E1 is the element having an index I(E0) of I(E1) - 1, 
        /// where the index I() corresponds to the storage location of the element in memory, relative to the first element.</para>
        /// <para>The storage order is determined by <paramref name="order"/>. By default, elements are lined up in memory 
        /// in column major storage order. <paramref name="order"/> must be one out of <see cref="StorageOrders.ColumnMajor"/> 
        /// or <see cref="StorageOrders.RowMajor"/>. Note, how the storage order affects the order of values in the new array.</para></remarks>
        /// <example>
        /// <para>Creating a new array of size [4,3], filled with upwards counting values, starting at 1.</para>
        /// <code><![CDATA[Array<double> A = counter<double>(1, 1, 4, 3);
        /// //<Double> [4,3] 1...12 |
        /// //    [0]:           1          5          9
        /// //    [1]:           2          6         10
        /// //    [2]:           3          7         11
        /// //    [3]:           4          8         12]]>
        ///</code> 
        ///<para>Creating a new array of size [4,3,2] of ushort values: </para>
        ///<code><![CDATA[counter<sbyte>(10, -10, 4,3,2)
        /// //<SByte> [4,3] 1...12 |
        /// //    [0]: (:,:,0)
        /// //    [1]:    10  -30  -70
        /// //    [2]:     0  -40  -80
        /// //    [3]:   -10  -50  -90
        /// //    [4]:   -20  -60 -100
        /// //    [5]: (:,:,1)
        /// //    [6]:  -110  106   66
        /// //    [7]:  -120   96   56
        /// //    [8]:   126   86   46
        /// //    [9]:   116   76   36]]></code> 
        /// <para>Note, how the computed values of data type 'sbyte' wrap around at the limits of 'sbyte' 
        /// without notice!</para>
        ///<para>Creating a new array of size [4,3] with upwards counting values in row major order: </para>
        ///<code><![CDATA[counter<float>(1, 1, 4,3, StorageOrders.RowMajor)
        /// //<Single> [4,3] 1...12 -
        /// //    [0]:           1          2          3
        /// //    [1]:           4          5          6
        /// //    [2]:           7          8          9
        /// //    [3]:          10         11         12
        /// //]]></code> 
        /// </example>
        internal static Array<T> counter<T>(T start, T step, long dim0, long dim1, long dim2, long dim3, long dim4, StorageOrders order = StorageOrders.ColumnMajor) where T : struct, IConvertible, IEquatable<T> {

            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, dim4, order: order);
            return counterInternal<T>(start, step, ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array with regularly spaced element values.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="start">The start value for the new elements.</param>
        /// <param name="step">The step size value for the new elements.</param>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="dim3">Length of dimension #3.</param>
        /// <param name="dim4">Length of dimension #4.</param>
        /// <param name="dim5">Length of dimension #5.</param>
        /// <param name="order">[Optional] The storage order for the new array. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>New ILNumerics array initialized with linearly spaced elements according to <paramref name="start"/> and counting up/downwards by <paramref name="step"/> along the storage order.</returns>
        /// <remarks><para>The <see cref="counter{T}(T, T, InArray{long}, StorageOrders)"/> function and the corresponding
        /// overloads for providing individual dimension length parameters <see cref="counter{T}(T, T, long, StorageOrders)"/>,... 
        /// create n-dimensional numeric arrays with automatically generated values based on a given <paramref name="start"/> value and a <paramref name="step"/> value. 
        /// The size of the new array is determined by the 'size' parameter(s). The firsts element value is assigned 
        /// the <paramref name="start"/> value. Subsequent values are computed by adding the value of <paramref name="step"/> to each elements predecessor.</para>
        /// <para>The term 'predecessor' here means: the predecessor E0 of element E1 is the element having an index I(E0) of I(E1) - 1, 
        /// where the index I() corresponds to the storage location of the element in memory, relative to the first element.</para>
        /// <para>The storage order is determined by <paramref name="order"/>. By default, elements are lined up in memory 
        /// in column major storage order. <paramref name="order"/> must be one out of <see cref="StorageOrders.ColumnMajor"/> 
        /// or <see cref="StorageOrders.RowMajor"/>. Note, how the storage order affects the order of values in the new array.</para></remarks>
        /// <example>
        /// <para>Creating a new array of size [4,3], filled with upwards counting values, starting at 1.</para>
        /// <code><![CDATA[Array<double> A = counter<double>(1, 1, 4, 3);
        /// //<Double> [4,3] 1...12 |
        /// //    [0]:           1          5          9
        /// //    [1]:           2          6         10
        /// //    [2]:           3          7         11
        /// //    [3]:           4          8         12]]>
        ///</code> 
        ///<para>Creating a new array of size [4,3,2] of ushort values: </para>
        ///<code><![CDATA[counter<sbyte>(10, -10, 4,3,2)
        /// //<SByte> [4,3] 1...12 |
        /// //    [0]: (:,:,0)
        /// //    [1]:    10  -30  -70
        /// //    [2]:     0  -40  -80
        /// //    [3]:   -10  -50  -90
        /// //    [4]:   -20  -60 -100
        /// //    [5]: (:,:,1)
        /// //    [6]:  -110  106   66
        /// //    [7]:  -120   96   56
        /// //    [8]:   126   86   46
        /// //    [9]:   116   76   36]]></code> 
        /// <para>Note, how the computed values of data type 'sbyte' wrap around at the limits of 'sbyte' 
        /// without notice!</para>
        ///<para>Creating a new array of size [4,3] with upwards counting values in row major order: </para>
        ///<code><![CDATA[counter<float>(1, 1, 4,3, StorageOrders.RowMajor)
        /// //<Single> [4,3] 1...12 -
        /// //    [0]:           1          2          3
        /// //    [1]:           4          5          6
        /// //    [2]:           7          8          9
        /// //    [3]:          10         11         12
        /// //]]></code> 
        /// </example>
        internal static Array<T> counter<T>(T start, T step, long dim0, long dim1, long dim2, long dim3, long dim4, long dim5, StorageOrders order = StorageOrders.ColumnMajor) where T : struct, IConvertible, IEquatable<T> {

            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, dim4, dim5, order: order);
            return counterInternal<T>(start, step, ret);
        }
        /// <summary>
        /// Creates a new ILNumerics array with regularly spaced element values.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="start">The start value for the new elements.</param>
        /// <param name="step">The step size value for the new elements.</param>
        /// <param name="dim0">Length of dimension #0.</param>
        /// <param name="dim1">Length of dimension #1.</param>
        /// <param name="dim2">Length of dimension #2.</param>
        /// <param name="dim3">Length of dimension #3.</param>
        /// <param name="dim4">Length of dimension #4.</param>
        /// <param name="dim5">Length of dimension #5.</param>
        /// <param name="dim6">Length of dimension #6.</param>
        /// <param name="order">[Optional] The storage order for the new array. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>New ILNumerics array initialized with linearly spaced elements according to <paramref name="start"/> and counting up/downwards by <paramref name="step"/> along the storage order.</returns>
        /// <remarks><para>The <see cref="counter{T}(T, T, InArray{long}, StorageOrders)"/> function and the corresponding
        /// overloads for providing individual dimension length parameters <see cref="counter{T}(T, T, long, StorageOrders)"/>,... 
        /// create n-dimensional numeric arrays with automatically generated values based on a given <paramref name="start"/> value and a <paramref name="step"/> value. 
        /// The size of the new array is determined by the 'size' parameter(s). The firsts element value is assigned 
        /// the <paramref name="start"/> value. Subsequent values are computed by adding the value of <paramref name="step"/> to each elements predecessor.</para>
        /// <para>The term 'predecessor' here means: the predecessor E0 of element E1 is the element having an index I(E0) of I(E1) - 1, 
        /// where the index I() corresponds to the storage location of the element in memory, relative to the first element.</para>
        /// <para>The storage order is determined by <paramref name="order"/>. By default, elements are lined up in memory 
        /// in column major storage order. <paramref name="order"/> must be one out of <see cref="StorageOrders.ColumnMajor"/> 
        /// or <see cref="StorageOrders.RowMajor"/>. Note, how the storage order affects the order of values in the new array.</para></remarks>
        /// <example>
        /// <para>Creating a new array of size [4,3], filled with upwards counting values, starting at 1.</para>
        /// <code><![CDATA[Array<double> A = counter<double>(1, 1, 4, 3);
        /// //<Double> [4,3] 1...12 |
        /// //    [0]:           1          5          9
        /// //    [1]:           2          6         10
        /// //    [2]:           3          7         11
        /// //    [3]:           4          8         12]]>
        ///</code> 
        ///<para>Creating a new array of size [4,3,2] of ushort values: </para>
        ///<code><![CDATA[counter<sbyte>(10, -10, 4,3,2)
        /// //<SByte> [4,3] 1...12 |
        /// //    [0]: (:,:,0)
        /// //    [1]:    10  -30  -70
        /// //    [2]:     0  -40  -80
        /// //    [3]:   -10  -50  -90
        /// //    [4]:   -20  -60 -100
        /// //    [5]: (:,:,1)
        /// //    [6]:  -110  106   66
        /// //    [7]:  -120   96   56
        /// //    [8]:   126   86   46
        /// //    [9]:   116   76   36]]></code> 
        /// <para>Note, how the computed values of data type 'sbyte' wrap around at the limits of 'sbyte' 
        /// without notice!</para>
        ///<para>Creating a new array of size [4,3] with upwards counting values in row major order: </para>
        ///<code><![CDATA[counter<float>(1, 1, 4,3, StorageOrders.RowMajor)
        /// //<Single> [4,3] 1...12 -
        /// //    [0]:           1          2          3
        /// //    [1]:           4          5          6
        /// //    [2]:           7          8          9
        /// //    [3]:          10         11         12
        /// //]]></code> 
        /// </example>
        internal static Array<T> counter<T>(T start, T step, long dim0, long dim1, long dim2, long dim3, long dim4, long dim5, long dim6, StorageOrders order = StorageOrders.ColumnMajor) where T : struct, IConvertible, IEquatable<T> {

            var ret = StorageLayer.Storage<T>.Create();
            ret.S.SetAll(dim0, dim1, dim2, dim3, dim4, dim5, dim6, order: order);
            return counterInternal<T>(start, step, ret);
        }
        #endregion

        #region counter internal
        
        private static unsafe Array<double> counterInternal(double start, double step, StorageLayer.Storage<double> ret) {
            var outLen = ret.S.NumberOfElements;
            ret.Handles = CountableArray.Create();
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<double>((ulong)outLen);

            InnerLoops.Counter.Double.Instance.operate(ret, start, step); // does not! release retarray
            return ret.RetArray; 
        }
        
        private static unsafe Array<T> counterInternal<T>(T start, T step, StorageLayer.Storage<T> ret) where T : struct, IConvertible, IEquatable<T> {
            var outLen = ret.S.NumberOfElements;
            ret.Handles = CountableArray.Create();
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<T>((ulong)outLen);
            if (false) {
            } else if (start is double) {
                InnerLoops.Counter.Double.Instance.operate(ret as Storage<double>, (double)(object)start, (double)(object)step);
            } else if (start is ulong) {
                InnerLoops.Counter.UInt64.Instance.operate(ret as Storage<ulong>, (ulong)(object)start, (ulong)(object)step);
            } else if (start is long) {
                InnerLoops.Counter.Int64.Instance.operate(ret as Storage<long>, (long)(object)start, (long)(object)step);
            } else if (start is uint) {
                InnerLoops.Counter.UInt32.Instance.operate(ret as Storage<uint>, (uint)(object)start, (uint)(object)step);
            } else if (start is int) {
                InnerLoops.Counter.Int32.Instance.operate(ret as Storage<int>, (int)(object)start, (int)(object)step);
            } else if (start is ushort) {
                InnerLoops.Counter.UInt16.Instance.operate(ret as Storage<ushort>, (ushort)(object)start, (ushort)(object)step);
            } else if (start is short) {
                InnerLoops.Counter.Int16.Instance.operate(ret as Storage<short>, (short)(object)start, (short)(object)step);
            } else if (start is byte) {
                InnerLoops.Counter.Byte.Instance.operate(ret as Storage<byte>, (byte)(object)start, (byte)(object)step);
            } else if (start is sbyte) {
                InnerLoops.Counter.SByte.Instance.operate(ret as Storage<sbyte>, (sbyte)(object)start, (sbyte)(object)step);
            } else if (start is fcomplex) {
                InnerLoops.Counter.FComplex.Instance.operate(ret as Storage<fcomplex>, (fcomplex)(object)start, (fcomplex)(object)step);
            } else if (start is complex) {
                InnerLoops.Counter.Complex.Instance.operate(ret as Storage<complex>, (complex)(object)start, (complex)(object)step);
            } else if (start is float) {
                InnerLoops.Counter.Single.Instance.operate(ret as Storage<float>, (float)(object)start, (float)(object)step);
            } else {
                throw new NotSupportedException($"Unsupported element type: {typeof(T).Name}. Supported are all numeric system types and ILNumerics complex types."); 
            }
            return ret.RetArray;
        }
        
        #endregion
    }

    #region inner loops

    #region HYCALPER LOOPSTART 
    /*!HC:TYPELIST:
    <hycalper>
        <type>
            <source locate="here">
                double
            </source>
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
        <type>
            <source locate="here">
                Double
            </source>
            <destination>Single</destination>
            <destination>Complex</destination>
            <destination>FComplex</destination>
            <destination>SByte</destination>
            <destination>Byte</destination>
            <destination>Int16</destination>
            <destination>UInt16</destination>
            <destination>Int32</destination>
            <destination>UInt32</destination>
            <destination>Int64</destination>
            <destination>UInt64</destination>
        </type>
    </hycalper>
    */
    namespace InnerLoops {
        namespace Counter {
            internal unsafe class Double : UnaryBaseInPlaceParameterized2<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> {

                internal static Double Instance = new Double();

                public override void Continous64Inplace(byte* pIn, long start, long len, double first, double step) {

                    System.Diagnostics.Debug.Assert(pIn != (byte*)0);
                    //System.Diagnostics.Trace.WriteLine($"Called with start: {start}, len:{len}, first:{first}, step:{step}"); 

                    pIn += start * sizeof(double);

                    len += start;
                    while (start < len) {
                        *(double*)pIn = (double)(first + (double)start++ * step);
                        pIn += sizeof(double);
                    }
                }

            }
        }
    }
    #endregion HYCALPER LOOPEND 
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
   
    namespace InnerLoops {
        namespace Counter {
            internal unsafe class UInt64 : UnaryBaseInPlaceParameterized2<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> {

                internal static UInt64 Instance = new UInt64();

                public override void Continous64Inplace(byte* pIn, long start, long len, ulong first, ulong step) {

                    System.Diagnostics.Debug.Assert(pIn != (byte*)0);
                    //System.Diagnostics.Trace.WriteLine($"Called with start: {start}, len:{len}, first:{first}, step:{step}"); 

                    pIn += start * sizeof(ulong);

                    len += start;
                    while (start < len) {
                        *(ulong*)pIn = (ulong)(first + (ulong)start++ * step);
                        pIn += sizeof(ulong);
                    }
                }

            }
        }
    }
   
    namespace InnerLoops {
        namespace Counter {
            internal unsafe class Int64 : UnaryBaseInPlaceParameterized2<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> {

                internal static Int64 Instance = new Int64();

                public override void Continous64Inplace(byte* pIn, long start, long len, long first, long step) {

                    System.Diagnostics.Debug.Assert(pIn != (byte*)0);
                    //System.Diagnostics.Trace.WriteLine($"Called with start: {start}, len:{len}, first:{first}, step:{step}"); 

                    pIn += start * sizeof(long);

                    len += start;
                    while (start < len) {
                        *(long*)pIn = (long)(first + (long)start++ * step);
                        pIn += sizeof(long);
                    }
                }

            }
        }
    }
   
    namespace InnerLoops {
        namespace Counter {
            internal unsafe class UInt32 : UnaryBaseInPlaceParameterized2<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> {

                internal static UInt32 Instance = new UInt32();

                public override void Continous64Inplace(byte* pIn, long start, long len, uint first, uint step) {

                    System.Diagnostics.Debug.Assert(pIn != (byte*)0);
                    //System.Diagnostics.Trace.WriteLine($"Called with start: {start}, len:{len}, first:{first}, step:{step}"); 

                    pIn += start * sizeof(uint);

                    len += start;
                    while (start < len) {
                        *(uint*)pIn = (uint)(first + (uint)start++ * step);
                        pIn += sizeof(uint);
                    }
                }

            }
        }
    }
   
    namespace InnerLoops {
        namespace Counter {
            internal unsafe class Int32 : UnaryBaseInPlaceParameterized2<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> {

                internal static Int32 Instance = new Int32();

                public override void Continous64Inplace(byte* pIn, long start, long len, int first, int step) {

                    System.Diagnostics.Debug.Assert(pIn != (byte*)0);
                    //System.Diagnostics.Trace.WriteLine($"Called with start: {start}, len:{len}, first:{first}, step:{step}"); 

                    pIn += start * sizeof(int);

                    len += start;
                    while (start < len) {
                        *(int*)pIn = (int)(first + (int)start++ * step);
                        pIn += sizeof(int);
                    }
                }

            }
        }
    }
   
    namespace InnerLoops {
        namespace Counter {
            internal unsafe class UInt16 : UnaryBaseInPlaceParameterized2<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> {

                internal static UInt16 Instance = new UInt16();

                public override void Continous64Inplace(byte* pIn, long start, long len, ushort first, ushort step) {

                    System.Diagnostics.Debug.Assert(pIn != (byte*)0);
                    //System.Diagnostics.Trace.WriteLine($"Called with start: {start}, len:{len}, first:{first}, step:{step}"); 

                    pIn += start * sizeof(ushort);

                    len += start;
                    while (start < len) {
                        *(ushort*)pIn = (ushort)(first + (ushort)start++ * step);
                        pIn += sizeof(ushort);
                    }
                }

            }
        }
    }
   
    namespace InnerLoops {
        namespace Counter {
            internal unsafe class Int16 : UnaryBaseInPlaceParameterized2<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> {

                internal static Int16 Instance = new Int16();

                public override void Continous64Inplace(byte* pIn, long start, long len, short first, short step) {

                    System.Diagnostics.Debug.Assert(pIn != (byte*)0);
                    //System.Diagnostics.Trace.WriteLine($"Called with start: {start}, len:{len}, first:{first}, step:{step}"); 

                    pIn += start * sizeof(short);

                    len += start;
                    while (start < len) {
                        *(short*)pIn = (short)(first + (short)start++ * step);
                        pIn += sizeof(short);
                    }
                }

            }
        }
    }
   
    namespace InnerLoops {
        namespace Counter {
            internal unsafe class Byte : UnaryBaseInPlaceParameterized2<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> {

                internal static Byte Instance = new Byte();

                public override void Continous64Inplace(byte* pIn, long start, long len, byte first, byte step) {

                    System.Diagnostics.Debug.Assert(pIn != (byte*)0);
                    //System.Diagnostics.Trace.WriteLine($"Called with start: {start}, len:{len}, first:{first}, step:{step}"); 

                    pIn += start * sizeof(byte);

                    len += start;
                    while (start < len) {
                        *(byte*)pIn = (byte)(first + (byte)start++ * step);
                        pIn += sizeof(byte);
                    }
                }

            }
        }
    }
   
    namespace InnerLoops {
        namespace Counter {
            internal unsafe class SByte : UnaryBaseInPlaceParameterized2<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> {

                internal static SByte Instance = new SByte();

                public override void Continous64Inplace(byte* pIn, long start, long len, sbyte first, sbyte step) {

                    System.Diagnostics.Debug.Assert(pIn != (byte*)0);
                    //System.Diagnostics.Trace.WriteLine($"Called with start: {start}, len:{len}, first:{first}, step:{step}"); 

                    pIn += start * sizeof(sbyte);

                    len += start;
                    while (start < len) {
                        *(sbyte*)pIn = (sbyte)(first + (sbyte)start++ * step);
                        pIn += sizeof(sbyte);
                    }
                }

            }
        }
    }
   
    namespace InnerLoops {
        namespace Counter {
            internal unsafe class FComplex : UnaryBaseInPlaceParameterized2<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> {

                internal static FComplex Instance = new FComplex();

                public override void Continous64Inplace(byte* pIn, long start, long len, fcomplex first, fcomplex step) {

                    System.Diagnostics.Debug.Assert(pIn != (byte*)0);
                    //System.Diagnostics.Trace.WriteLine($"Called with start: {start}, len:{len}, first:{first}, step:{step}"); 

                    pIn += start * sizeof(fcomplex);

                    len += start;
                    while (start < len) {
                        *(fcomplex*)pIn = (fcomplex)(first + (fcomplex)start++ * step);
                        pIn += sizeof(fcomplex);
                    }
                }

            }
        }
    }
   
    namespace InnerLoops {
        namespace Counter {
            internal unsafe class Complex : UnaryBaseInPlaceParameterized2<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> {

                internal static Complex Instance = new Complex();

                public override void Continous64Inplace(byte* pIn, long start, long len, complex first, complex step) {

                    System.Diagnostics.Debug.Assert(pIn != (byte*)0);
                    //System.Diagnostics.Trace.WriteLine($"Called with start: {start}, len:{len}, first:{first}, step:{step}"); 

                    pIn += start * sizeof(complex);

                    len += start;
                    while (start < len) {
                        *(complex*)pIn = (complex)(first + (complex)start++ * step);
                        pIn += sizeof(complex);
                    }
                }

            }
        }
    }
   
    namespace InnerLoops {
        namespace Counter {
            internal unsafe class Single : UnaryBaseInPlaceParameterized2<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> {

                internal static Single Instance = new Single();

                public override void Continous64Inplace(byte* pIn, long start, long len, float first, float step) {

                    System.Diagnostics.Debug.Assert(pIn != (byte*)0);
                    //System.Diagnostics.Trace.WriteLine($"Called with start: {start}, len:{len}, first:{first}, step:{step}"); 

                    pIn += start * sizeof(float);

                    len += start;
                    while (start < len) {
                        *(float*)pIn = (float)(first + (float)start++ * step);
                        pIn += sizeof(float);
                    }
                }

            }
        }
    }

#endregion HYCALPER AUTO GENERATED CODE
    
    #endregion

}
