using ILNumerics.Core.Arrays;
using ILNumerics.Core.Functions.Builtin;
using ILNumerics.Core.StorageLayer;
using System;
using System.Security;

namespace ILNumerics {

    public static partial class ExtensionMethods {

        #region GetArray{T}(....)

        /// <summary>
        /// Gives the content of a cell element as ILNumerics array of specified type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="A">The source cell.</param>
        /// <param name="indices">Indices into the dimensions of <paramref name="A"/> addressing the single cell storing the array.</param>
        /// <returns>Lazy, shallow copy of the element found at given <paramref name="indices"/>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="indices"/> is null.</exception>
        /// <seealso cref="ILNumerics.ILMath.size(long, long, long, long, long)"/>
        /// <seealso cref="ILNumerics.ILMath.size(long)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.C"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        /// <seealso cref="GetArray{T}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long, long)"/>
        
        public unsafe static Array<T> GetArray<T>(
                        this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A,
                        InArray<long> indices) {
            using (Scope.Enter(indices)) {
                if (Equals(indices,null)) {
                    throw new ArgumentNullException(nameof(indices)); 
                }
                switch (indices.S.NumberOfElements) {
                    case 0:
                        return A.GetValue()?.ToArray<T>();
                    case 1:
                        return A.GetValue(indices.GetValue(0))?.ToArray<T>();
                    case 2:
                        return A.GetValue(indices.GetValue(0), indices.GetValue(1))?.ToArray<T>();
                    case 3:
                        return A.GetValue(indices.GetValue(0), indices.GetValue(1), indices.GetValue(2))?.ToArray<T>();
                    case 4:
                        return A.GetValue(indices.GetValue(0), indices.GetValue(1), indices.GetValue(2), indices.GetValue(3))?.ToArray<T>();
                    case 5:
                        return A.GetValue(indices.GetValue(0), indices.GetValue(1), indices.GetValue(2), indices.GetValue(3), indices.GetValue(4))?.ToArray<T>();
                    case 6:
                        return A.GetValue(indices.GetValue(0), indices.GetValue(1), indices.GetValue(2), indices.GetValue(3), indices.GetValue(4), indices.GetValue(5))?.ToArray<T>();
                    case 7:
                        return A.GetValue(indices.GetValue(0), indices.GetValue(1), indices.GetValue(2), indices.GetValue(3), indices.GetValue(4), indices.GetValue(5), indices.GetValue(6))?.ToArray<T>();
                    default:
                        throw new ArgumentException($"'indices' must address a single element in cell A. Found too many indices: {indices.S.NumberOfElements}."); 
                }
             }
        }

        /// <summary>
        /// Gives the content of a scalar cell as ILNumerics array of specified type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="A">The source cell.</param>
        /// <returns>Lazy, shallow copy of the matching element found at given position or null.</returns>
        /// <seealso cref="GetArray{T}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, InArray{long})"/>
        /// <seealso cref="ILNumerics.ILMath.size(long, long, long, long, long)"/>
        /// <seealso cref="ILNumerics.ILMath.size(long)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.C"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        public static Array<T> GetArray<T>(this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A) {
            return A.GetValue()?.ToArray<T>();
        }
        /// <summary>
        /// Gives the content of a cell element as ILNumerics array of specified type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="A">The source cell.</param>
        /// <param name="d0">Index into the first dimensions of <paramref name="A"/> or sequential index (<see cref="StorageOrders.ColumnMajor"/>) addressing the single cell storing the array.</param>
        /// <returns>Lazy, shallow copy of the matching element found at given position or null.</returns>
        /// <seealso cref="GetArray{T}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, InArray{long})"/>
        /// <seealso cref="ILNumerics.ILMath.size(long, long, long, long, long)"/>
        /// <seealso cref="ILNumerics.ILMath.size(long)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.C"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        public static Array<T> GetArray<T>(this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, long d0) {
            return A.GetValue(d0)?.ToArray<T>();
        }
        /// <summary>
        /// Gives the content of a cell element as ILNumerics array of specified type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="A">The source cell.</param>
        /// <param name="d0">Index into the first dimensions of <paramref name="A"/> addressing the single cell storing the array.</param>
        /// <param name="d1">Index into the second dimensions of <paramref name="A"/>.</param>
        /// <returns>Lazy, shallow copy of the matching element found at given position or null.</returns>
        /// <seealso cref="GetArray{T}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, InArray{long})"/>
        /// <seealso cref="ILNumerics.ILMath.size(long, long, long, long, long)"/>
        /// <seealso cref="ILNumerics.ILMath.size(long)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.C"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        public static Array<T> GetArray<T>(this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, long d0, long d1) {
            return A.GetValue(d0, d1)?.ToArray<T>();
        }
        /// <summary>
        /// Gives the content of a cell element as ILNumerics array of specified type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="A">The source cell.</param>
        /// <param name="d0">Index into the first dimensions of <paramref name="A"/> addressing the single cell storing the array.</param>
        /// <param name="d1">Index into the second dimensions of <paramref name="A"/>.</param>
        /// <param name="d2">Index into the third dimensions of <paramref name="A"/>.</param>
        /// <returns>Lazy, shallow copy of the matching element found at given position or null.</returns>
        /// <seealso cref="GetArray{T}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, InArray{long})"/>
        /// <seealso cref="ILNumerics.ILMath.size(long, long, long, long, long)"/>
        /// <seealso cref="ILNumerics.ILMath.size(long)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.C"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        public static Array<T> GetArray<T>(this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, long d0, long d1, long d2) {
            return A.GetValue(d0, d1, d2)?.ToArray<T>();
        }
        /// <summary>
        /// Gives the content of a cell element as ILNumerics array of specified type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="A">The source cell.</param>
        /// <param name="d0">Index into the first dimensions of <paramref name="A"/> addressing the single cell storing the array.</param>
        /// <param name="d1">Index into the second dimensions of <paramref name="A"/>.</param>
        /// <param name="d2">Index into the third dimensions of <paramref name="A"/>.</param>
        /// <param name="d3">Index into the 4-th dimensions of <paramref name="A"/>.</param>
        /// <returns>Lazy, shallow copy of the matching element found at given position or null.</returns>
        /// <seealso cref="GetArray{T}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, InArray{long})"/>
        /// <seealso cref="ILNumerics.ILMath.size(long, long, long, long, long)"/>
        /// <seealso cref="ILNumerics.ILMath.size(long)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.C"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        public static Array<T> GetArray<T>(this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, long d0, long d1, long d2, long d3) {
            return A.GetValue(d0, d1, d2, d3)?.ToArray<T>();
        }
        /// <summary>
        /// Gives the content of a cell element as ILNumerics array of specified type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="A">The source cell.</param>
        /// <param name="d0">Index into the first dimensions of <paramref name="A"/> addressing the single cell storing the array.</param>
        /// <param name="d1">Index into the second dimensions of <paramref name="A"/>.</param>
        /// <param name="d2">Index into the third dimensions of <paramref name="A"/>.</param>
        /// <param name="d3">Index into the 4-th dimensions of <paramref name="A"/>.</param>
        /// <param name="d4">Index into the 5-th dimensions of <paramref name="A"/>.</param>
        /// <returns>Lazy, shallow copy of the matching element found at given position or null.</returns>
        /// <seealso cref="GetArray{T}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, InArray{long})"/>
        /// <seealso cref="ILNumerics.ILMath.size(long, long, long, long, long)"/>
        /// <seealso cref="ILNumerics.ILMath.size(long)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.C"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        public static Array<T> GetArray<T>(this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, long d0, long d1, long d2, long d3, long d4) {
            return A.GetValue(d0, d1, d2, d3, d4)?.ToArray<T>();
        }
        /// <summary>
        /// Gives the content of a cell element as ILNumerics array of specified type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="A">The source cell.</param>
        /// <param name="d0">Index into the first dimensions of <paramref name="A"/> addressing the single cell storing the array.</param>
        /// <param name="d1">Index into the second dimensions of <paramref name="A"/>.</param>
        /// <param name="d2">Index into the third dimensions of <paramref name="A"/>.</param>
        /// <param name="d3">Index into the 4-th dimensions of <paramref name="A"/>.</param>
        /// <param name="d4">Index into the 5-th dimensions of <paramref name="A"/>.</param>
        /// <param name="d5">Index into the 6-th dimensions of <paramref name="A"/>.</param>
        /// <returns>Lazy, shallow copy of the matching element found at given position or null.</returns>
        /// <seealso cref="GetArray{T}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, InArray{long})"/>
        /// <seealso cref="ILNumerics.ILMath.size(long, long, long, long, long)"/>
        /// <seealso cref="ILNumerics.ILMath.size(long)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.C"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        public static Array<T> GetArray<T>(this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, long d0, long d1, long d2, long d3, long d4, long d5) {
            return A.GetValue(d0, d1, d2, d3, d4, d5)?.ToArray<T>();
        }
        /// <summary>
        /// Gives the content of a cell element as ILNumerics array of specified type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="A">The source cell.</param>
        /// <param name="d0">Index into the first dimensions of <paramref name="A"/> addressing the single cell storing the array.</param>
        /// <param name="d1">Index into the second dimensions of <paramref name="A"/>.</param>
        /// <param name="d2">Index into the third dimensions of <paramref name="A"/>.</param>
        /// <param name="d3">Index into the 4-th dimensions of <paramref name="A"/>.</param>
        /// <param name="d4">Index into the 5-th dimensions of <paramref name="A"/>.</param>
        /// <param name="d5">Index into the 6-th dimensions of <paramref name="A"/>.</param>
        /// <param name="d6">Index into the 6-th dimensions of <paramref name="A"/>.</param>
        /// <returns>Lazy, shallow copy of the matching element found at given position or null.</returns>
        /// <seealso cref="GetArray{T}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, InArray{long})"/>
        /// <seealso cref="ILNumerics.ILMath.size(long, long, long, long, long)"/>
        /// <seealso cref="ILNumerics.ILMath.size(long)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.C"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        public static Array<T> GetArray<T>(this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, long d0, long d1, long d2, long d3, long d4, long d5, long d6) {
            return A.GetValue(d0, d1, d2, d3, d4, d5, d6)?.ToArray<T>();
        }

        #endregion
        
        #region GetCell(....)

        /// <summary>
        /// Gives the content of a cell element of element type cell.
        /// </summary>
        /// <param name="A">The source cell.</param>
        /// <param name="indices">Indices into the dimensions of <paramref name="A"/> addressing the single cell storing the inner cell to retrieve.</param>
        /// <returns>Lazy, shallow copy of the element found at given <paramref name="indices"/>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="indices"/> is null.</exception>
        /// <seealso cref="ILNumerics.ILMath.size(long, long, long, long, long)"/>
        /// <seealso cref="ILNumerics.ILMath.size(long)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.C"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        /// <seealso cref="GetArray{T}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long, long)"/>
        
        public unsafe static Cell GetCell(
                        this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A,
                        InArray<long> indices) {
            using (Scope.Enter(indices)) {
                if (Equals(indices, null)) {
                    throw new ArgumentNullException(nameof(indices));
                }
                switch (indices.S.NumberOfElements) {
                    case 0:
                        return A.GetValue()?.ToCell();
                    case 1:
                        return A.GetValue(indices.GetValue(0))?.ToCell();
                    case 2:
                        return A.GetValue(indices.GetValue(0), indices.GetValue(1))?.ToCell();
                    case 3:
                        return A.GetValue(indices.GetValue(0), indices.GetValue(1), indices.GetValue(2))?.ToCell();
                    case 4:
                        return A.GetValue(indices.GetValue(0), indices.GetValue(1), indices.GetValue(2), indices.GetValue(3))?.ToCell();
                    case 5:
                        return A.GetValue(indices.GetValue(0), indices.GetValue(1), indices.GetValue(2), indices.GetValue(3), indices.GetValue(4))?.ToCell();
                    case 6:
                        return A.GetValue(indices.GetValue(0), indices.GetValue(1), indices.GetValue(2), indices.GetValue(3), indices.GetValue(4), indices.GetValue(5))?.ToCell();
                    case 7:
                        return A.GetValue(indices.GetValue(0), indices.GetValue(1), indices.GetValue(2), indices.GetValue(3), indices.GetValue(4), indices.GetValue(5), indices.GetValue(6))?.ToCell();
                    default:
                        throw new ArgumentException($"'indices' must address a single element in cell A. Found too many indices: {indices.S.NumberOfElements}.");
                }
            }
        }

        /// <summary>
        /// Gives the content of a scalar cell of element type cell.
        /// </summary>
        /// <param name="A">The source cell.</param>
        /// <returns>Lazy, shallow copy of the element found at given position.</returns>
        /// <seealso cref="GetArray{T}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, InArray{long})"/>
        /// <seealso cref="ILNumerics.ILMath.size(long, long, long, long, long)"/>
        /// <seealso cref="ILNumerics.ILMath.size(long)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.C"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        public static Cell GetCell(this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A) {
            return A.GetValue()?.ToCell();
        }
        /// <summary>
        /// Gives the content of a cell element of element type cell.
        /// </summary>
        /// <param name="A">The source cell.</param>
        /// <param name="d0">Index into the first dimensions of <paramref name="A"/> or sequential index (<see cref="StorageOrders.ColumnMajor"/>) addressing the single cell storing the array.</param>
        /// <returns>Lazy, shallow copy of the element found at given position.</returns>
        /// <seealso cref="GetArray{T}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, InArray{long})"/>
        /// <seealso cref="ILNumerics.ILMath.size(long, long, long, long, long)"/>
        /// <seealso cref="ILNumerics.ILMath.size(long)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.C"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        public static Cell GetCell(this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, long d0) {
            return A.GetValue(d0)?.ToCell();
        }
        /// <summary>
        /// Gives the content of a cell element of element type cell.
        /// </summary>
        /// <param name="A">The source cell.</param>
        /// <param name="d0">Index into the first dimensions of <paramref name="A"/> addressing the single cell storing the array.</param>
        /// <param name="d1">Index into the second dimensions of <paramref name="A"/>.</param>
        /// <returns>Lazy, shallow copy of the element found at given position.</returns>
        /// <seealso cref="GetArray{T}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, InArray{long})"/>
        /// <seealso cref="ILNumerics.ILMath.size(long, long, long, long, long)"/>
        /// <seealso cref="ILNumerics.ILMath.size(long)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.C"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        public static Cell GetCell(this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, long d0, long d1) {
            return A.GetValue(d0, d1)?.ToCell();
        }
        /// <summary>
        /// Gives the content of a cell element of element type cell.
        /// </summary>
        /// <param name="A">The source cell.</param>
        /// <param name="d0">Index into the first dimensions of <paramref name="A"/> addressing the single cell storing the array.</param>
        /// <param name="d1">Index into the second dimensions of <paramref name="A"/>.</param>
        /// <param name="d2">Index into the third dimensions of <paramref name="A"/>.</param>
        /// <returns>Lazy, shallow copy of the element found at given position.</returns>
        /// <seealso cref="GetArray{T}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, InArray{long})"/>
        /// <seealso cref="ILNumerics.ILMath.size(long, long, long, long, long)"/>
        /// <seealso cref="ILNumerics.ILMath.size(long)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.C"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        public static Cell GetCell(this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, long d0, long d1, long d2) {
            return A.GetValue(d0, d1, d2)?.ToCell();
        }
        /// <summary>
        /// Gives the content of a cell element of element type cell.
        /// </summary>
        /// <param name="A">The source cell.</param>
        /// <param name="d0">Index into the first dimensions of <paramref name="A"/> addressing the single cell storing the array.</param>
        /// <param name="d1">Index into the second dimensions of <paramref name="A"/>.</param>
        /// <param name="d2">Index into the third dimensions of <paramref name="A"/>.</param>
        /// <param name="d3">Index into the 4-th dimensions of <paramref name="A"/>.</param>
        /// <returns>Lazy, shallow copy of the element found at given position.</returns>
        /// <seealso cref="GetArray{T}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, InArray{long})"/>
        /// <seealso cref="ILNumerics.ILMath.size(long, long, long, long, long)"/>
        /// <seealso cref="ILNumerics.ILMath.size(long)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.C"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        public static Cell GetCell(this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, long d0, long d1, long d2, long d3) {
            return A.GetValue(d0, d1, d2, d3)?.ToCell();
        }
        /// <summary>
        /// Gives the content of a cell element of element type cell.
        /// </summary>
        /// <param name="A">The source cell.</param>
        /// <param name="d0">Index into the first dimensions of <paramref name="A"/> addressing the single cell storing the array.</param>
        /// <param name="d1">Index into the second dimensions of <paramref name="A"/>.</param>
        /// <param name="d2">Index into the third dimensions of <paramref name="A"/>.</param>
        /// <param name="d3">Index into the 4-th dimensions of <paramref name="A"/>.</param>
        /// <param name="d4">Index into the 5-th dimensions of <paramref name="A"/>.</param>
        /// <returns>Lazy, shallow copy of the element found at given position.</returns>
        /// <seealso cref="GetArray{T}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, InArray{long})"/>
        /// <seealso cref="ILNumerics.ILMath.size(long, long, long, long, long)"/>
        /// <seealso cref="ILNumerics.ILMath.size(long)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.C"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        public static Cell GetCell(this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, long d0, long d1, long d2, long d3, long d4) {
            return A.GetValue(d0, d1, d2, d3, d4)?.ToCell();
        }
        /// <summary>
        /// Gives the content of a cell element of element type cell.
        /// </summary>
        /// <param name="A">The source cell.</param>
        /// <param name="d0">Index into the first dimensions of <paramref name="A"/> addressing the single cell storing the array.</param>
        /// <param name="d1">Index into the second dimensions of <paramref name="A"/>.</param>
        /// <param name="d2">Index into the third dimensions of <paramref name="A"/>.</param>
        /// <param name="d3">Index into the 4-th dimensions of <paramref name="A"/>.</param>
        /// <param name="d4">Index into the 5-th dimensions of <paramref name="A"/>.</param>
        /// <param name="d5">Index into the 6-th dimensions of <paramref name="A"/>.</param>
        /// <returns>Lazy, shallow copy of the element found at given position.</returns>
        /// <seealso cref="GetArray{T}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, InArray{long})"/>
        /// <seealso cref="ILNumerics.ILMath.size(long, long, long, long, long)"/>
        /// <seealso cref="ILNumerics.ILMath.size(long)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.C"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        public static Cell GetCell(this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, long d0, long d1, long d2, long d3, long d4, long d5) {
            return A.GetValue(d0, d1, d2, d3, d4, d5)?.ToCell();
        }
        /// <summary>
        /// Gives the content of a cell element of element type cell.
        /// </summary>
        /// <param name="A">The source cell.</param>
        /// <param name="d0">Index into the first dimensions of <paramref name="A"/> addressing the single cell storing the array.</param>
        /// <param name="d1">Index into the second dimensions of <paramref name="A"/>.</param>
        /// <param name="d2">Index into the third dimensions of <paramref name="A"/>.</param>
        /// <param name="d3">Index into the 4-th dimensions of <paramref name="A"/>.</param>
        /// <param name="d4">Index into the 5-th dimensions of <paramref name="A"/>.</param>
        /// <param name="d5">Index into the 6-th dimensions of <paramref name="A"/>.</param>
        /// <param name="d6">Index into the 6-th dimensions of <paramref name="A"/>.</param>
        /// <returns>Lazy, shallow copy of the element found at given position.</returns>
        /// <seealso cref="GetArray{T}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, InArray{long})"/>
        /// <seealso cref="ILNumerics.ILMath.size(long, long, long, long, long)"/>
        /// <seealso cref="ILNumerics.ILMath.size(long)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.C"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        public static Cell GetCell(this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, long d0, long d1, long d2, long d3, long d4, long d5, long d6) {
            return A.GetValue(d0, d1, d2, d3, d4, d5, d6)?.ToCell();
        }

        #endregion

        #region GetLogical(....)

        /// <summary>
        /// Gives the content of a cell element storing a <see cref="Logical"/> array.
        /// </summary>
        /// <param name="A">The source Logical.</param>
        /// <param name="indices">Indices into the dimensions of <paramref name="A"/> addressing the single Logical storing the inner Logical to retrieve.</param>
        /// <returns>Lazy, shallow copy of the element found at given <paramref name="indices"/>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="indices"/> is null.</exception>
        /// <seealso cref="ILNumerics.ILMath.size(long, long, long, long, long)"/>
        /// <seealso cref="ILNumerics.ILMath.size(long)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.C"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        /// <seealso cref="GetLogical(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long, long)"/>

        public unsafe static Logical GetLogical(
                        this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A,
                        InArray<long> indices) {
            using (Scope.Enter(indices)) {
                if (Equals(indices, null)) {
                    throw new ArgumentNullException(nameof(indices));
                }
                switch (indices.S.NumberOfElements) {
                    case 0:
                        return A.GetValue().ToLogical();
                    case 1:
                        return A.GetValue(indices.GetValue(0)).ToLogical();
                    case 2:
                        return A.GetValue(indices.GetValue(0), indices.GetValue(1)).ToLogical();
                    case 3:
                        return A.GetValue(indices.GetValue(0), indices.GetValue(1), indices.GetValue(2)).ToLogical();
                    case 4:
                        return A.GetValue(indices.GetValue(0), indices.GetValue(1), indices.GetValue(2), indices.GetValue(3)).ToLogical();
                    case 5:
                        return A.GetValue(indices.GetValue(0), indices.GetValue(1), indices.GetValue(2), indices.GetValue(3), indices.GetValue(4)).ToLogical();
                    case 6:
                        return A.GetValue(indices.GetValue(0), indices.GetValue(1), indices.GetValue(2), indices.GetValue(3), indices.GetValue(4), indices.GetValue(5)).ToLogical();
                    case 7:
                        return A.GetValue(indices.GetValue(0), indices.GetValue(1), indices.GetValue(2), indices.GetValue(3), indices.GetValue(4), indices.GetValue(5), indices.GetValue(6)).ToLogical();
                    default:
                        throw new ArgumentException($"'indices' must address a single element in cell A. Found too many indices: {indices.S.NumberOfElements}.");
                }
            }
        }

        /// <summary>
        /// Gives the content of a scalar cell storing a <see cref="Logical"/> array.
        /// </summary>
        /// <param name="A">The source Logical.</param>
        /// <returns>Lazy, shallow copy of the element found at given position.</returns>
        /// <seealso cref="GetArray{T}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, InArray{long})"/>
        /// <seealso cref="ILNumerics.ILMath.size(long, long, long, long, long)"/>
        /// <seealso cref="ILNumerics.ILMath.size(long)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.C"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        public static Logical GetLogical(this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A) {
            return A.GetValue()?.ToLogical();
        }
        /// <summary>
        /// Gives the content of a cell element storing a <see cref="Logical"/> array.
        /// </summary>
        /// <param name="A">The source Logical.</param>
        /// <param name="d0">Index into the first dimensions of <paramref name="A"/> or sequential index (<see cref="StorageOrders.ColumnMajor"/>) addressing the single Logical storing the array.</param>
        /// <returns>Lazy, shallow copy of the element found at given position.</returns>
        /// <seealso cref="GetArray{T}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, InArray{long})"/>
        /// <seealso cref="ILNumerics.ILMath.size(long, long, long, long, long)"/>
        /// <seealso cref="ILNumerics.ILMath.size(long)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.C"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        public static Logical GetLogical(this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, long d0) {
            return A.GetValue(d0)?.ToLogical();
        }
        /// <summary>
        /// Gives the content of a cell element storing a <see cref="Logical"/> array.
        /// </summary>
        /// <param name="A">The source Logical.</param>
        /// <param name="d0">Index into the first dimensions of <paramref name="A"/> addressing the single Logical storing the array.</param>
        /// <param name="d1">Index into the second dimensions of <paramref name="A"/>.</param>
        /// <returns>Lazy, shallow copy of the element found at given position.</returns>
        /// <seealso cref="GetArray{T}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, InArray{long})"/>
        /// <seealso cref="ILNumerics.ILMath.size(long, long, long, long, long)"/>
        /// <seealso cref="ILNumerics.ILMath.size(long)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.C"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        public static Logical GetLogical(this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, long d0, long d1) {
            return A.GetValue(d0, d1)?.ToLogical();
        }
        /// <summary>
        /// Gives the content of a cell element storing a <see cref="Logical"/> array.
        /// </summary>
        /// <param name="A">The source Logical.</param>
        /// <param name="d0">Index into the first dimensions of <paramref name="A"/> addressing the single Logical storing the array.</param>
        /// <param name="d1">Index into the second dimensions of <paramref name="A"/>.</param>
        /// <param name="d2">Index into the third dimensions of <paramref name="A"/>.</param>
        /// <returns>Lazy, shallow copy of the element found at given position.</returns>
        /// <seealso cref="GetArray{T}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, InArray{long})"/>
        /// <seealso cref="ILNumerics.ILMath.size(long, long, long, long, long)"/>
        /// <seealso cref="ILNumerics.ILMath.size(long)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.C"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        public static Logical GetLogical(this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, long d0, long d1, long d2) {
            return A.GetValue(d0, d1, d2)?.ToLogical();
        }
        /// <summary>
        /// Gives the content of a cell element storing a <see cref="Logical"/> array.
        /// </summary>
        /// <param name="A">The source Logical.</param>
        /// <param name="d0">Index into the first dimensions of <paramref name="A"/> addressing the single Logical storing the array.</param>
        /// <param name="d1">Index into the second dimensions of <paramref name="A"/>.</param>
        /// <param name="d2">Index into the third dimensions of <paramref name="A"/>.</param>
        /// <param name="d3">Index into the 4-th dimensions of <paramref name="A"/>.</param>
        /// <returns>Lazy, shallow copy of the element found at given position.</returns>
        /// <seealso cref="GetArray{T}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, InArray{long})"/>
        /// <seealso cref="ILNumerics.ILMath.size(long, long, long, long, long)"/>
        /// <seealso cref="ILNumerics.ILMath.size(long)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.C"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        public static Logical GetLogical(this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, long d0, long d1, long d2, long d3) {
            return A.GetValue(d0, d1, d2, d3)?.ToLogical();
        }
        /// <summary>
        /// Gives the content of a cell element storing a <see cref="Logical"/> array.
        /// </summary>
        /// <param name="A">The source Logical.</param>
        /// <param name="d0">Index into the first dimensions of <paramref name="A"/> addressing the single Logical storing the array.</param>
        /// <param name="d1">Index into the second dimensions of <paramref name="A"/>.</param>
        /// <param name="d2">Index into the third dimensions of <paramref name="A"/>.</param>
        /// <param name="d3">Index into the 4-th dimensions of <paramref name="A"/>.</param>
        /// <param name="d4">Index into the 5-th dimensions of <paramref name="A"/>.</param>
        /// <returns>Lazy, shallow copy of the element found at given position.</returns>
        /// <seealso cref="GetArray{T}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, InArray{long})"/>
        /// <seealso cref="ILNumerics.ILMath.size(long, long, long, long, long)"/>
        /// <seealso cref="ILNumerics.ILMath.size(long)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.C"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        public static Logical GetLogical(this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, long d0, long d1, long d2, long d3, long d4) {
            return A.GetValue(d0, d1, d2, d3, d4)?.ToLogical();
        }
        /// <summary>
        /// Gives the content of a cell element storing a <see cref="Logical"/> array.
        /// </summary>
        /// <param name="A">The source Logical.</param>
        /// <param name="d0">Index into the first dimensions of <paramref name="A"/> addressing the single Logical storing the array.</param>
        /// <param name="d1">Index into the second dimensions of <paramref name="A"/>.</param>
        /// <param name="d2">Index into the third dimensions of <paramref name="A"/>.</param>
        /// <param name="d3">Index into the 4-th dimensions of <paramref name="A"/>.</param>
        /// <param name="d4">Index into the 5-th dimensions of <paramref name="A"/>.</param>
        /// <param name="d5">Index into the 6-th dimensions of <paramref name="A"/>.</param>
        /// <returns>Lazy, shallow copy of the element found at given position.</returns>
        /// <seealso cref="GetArray{T}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, InArray{long})"/>
        /// <seealso cref="ILNumerics.ILMath.size(long, long, long, long, long)"/>
        /// <seealso cref="ILNumerics.ILMath.size(long)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.C"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        public static Logical GetLogical(this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, long d0, long d1, long d2, long d3, long d4, long d5) {
            return A.GetValue(d0, d1, d2, d3, d4, d5)?.ToLogical();
        }
        /// <summary>
        /// Gives the content of a cell element storing a <see cref="Logical"/> array.
        /// </summary>
        /// <param name="A">The source Logical.</param>
        /// <param name="d0">Index into the first dimensions of <paramref name="A"/> addressing the single Logical storing the array.</param>
        /// <param name="d1">Index into the second dimensions of <paramref name="A"/>.</param>
        /// <param name="d2">Index into the third dimensions of <paramref name="A"/>.</param>
        /// <param name="d3">Index into the 4-th dimensions of <paramref name="A"/>.</param>
        /// <param name="d4">Index into the 5-th dimensions of <paramref name="A"/>.</param>
        /// <param name="d5">Index into the 6-th dimensions of <paramref name="A"/>.</param>
        /// <param name="d6">Index into the 6-th dimensions of <paramref name="A"/>.</param>
        /// <returns>Lazy, shallow copy of the element found at given position.</returns>
        /// <seealso cref="GetArray{T}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, InArray{long})"/>
        /// <seealso cref="ILNumerics.ILMath.size(long, long, long, long, long)"/>
        /// <seealso cref="ILNumerics.ILMath.size(long)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.C"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        public static Logical GetLogical(this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, long d0, long d1, long d2, long d3, long d4, long d5, long d6) {
            return A.GetValue(d0, d1, d2, d3, d4, d5, d6)?.ToLogical();
        }

        #endregion

        #region IsTypeOf

        /// <summary>
        /// Tests if a cell element is of the given element type. 
        /// </summary>
        /// <typeparam name="CellT">The array element type to probe the cell element for.</typeparam>
        /// <param name="A">The cell.</param>
        /// <param name="indices">Dimensional indices defining the position of the cell element to be probed.</param>
        /// <returns>true if the element found at the given position is an array of the given element type <typeparamref name="CellT"/>, false otherwise.</returns>
        /// <remarks><para>The method is helpful in order to inspect the contents of a cell array. If the actual types of cells in the cell array
        /// are unknown this function can be used to determine the type of the content of individual cells before attempting to retrieve any values.</para>
        /// <para>In most situations, elements of a cell are stored arrays of a distinct element type. That element type is given to 
        /// <see cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, InArray{long})"/> as 
        /// typeparameter <typeparamref name="CellT"/>. That means, in order to find out, if the first cell element stores an array of int 
        /// <c><![CDATA[cell.IsTypeOf<int>(0)]]></c> is used.</para>
        /// <para>When testing for <see cref="Logical"/> arrays <see cref="bool"/> is used for <typeparamref name="CellT"/>.</para>
        /// <para>In order to test for cell element type <typeparamref name="CellT"/> can be <see cref="Cell"/> or <see cref="BaseArray"/>: <c><![CDATA[cell.IsTypeOf<BaseArray>(0)]]></c>.</para>
        /// </remarks>
        /// <example>
        /// <para>In the following example a Cell of size 3x2 is created. It stores several array types, among which other cells are stored as elements of the outer cell.</para>
        /// <code><![CDATA[Cell cell1 = cell(size(3, 2), vector<BaseArray>(
        ///                     "first element"
        ///                     , 2.0
        ///                     , cell(arrays: vector<BaseArray>(Math.PI, 100f))
        ///                     , vector<short>(1, 2, 3, 4, 5, 6)
        ///                     , vector<double>(-1.4, -1.5, -1.6 )));
        /// 
        /// Console.Out.WriteLine($"cell[0,0] is element type 'string': {cell1.IsTypeOf<string>(0)}");
        /// Console.Out.WriteLine($"cell[0,0] is element type 'double': {cell1.IsTypeOf<double>(0)}");
        /// 
        /// Console.Out.WriteLine($"cell[1,0] is element type 'double': {cell1.IsTypeOf<double>(1,0)}");
        /// Console.Out.WriteLine($"cell[2,0] is element type 'Cell': {cell1.IsTypeOf<Cell>(2,0)}");
        /// 
        /// Console.Out.WriteLine($"cell[0,1] is element type 'short': {cell1.IsTypeOf<short>(0, 1)}");
        /// Console.Out.WriteLine($"cell[1,1] is element type 'Cell': {cell1.IsTypeOf<Cell>(1, 1)}");
        /// Console.Out.WriteLine($"cell[2,1] is element type 'double': {cell1.IsTypeOf<double>(2, 1)}");]]>
        /// </code>
        /// This gives the following output: 
        /// <code>
        /// cell[0,0] is element type 'string': True
        /// cell[0,0] is element type 'double': False
        /// cell[1,0] is element type 'double': True
        /// cell[2,0] is element type 'Cell': True
        /// cell[0,1] is element type 'short': True
        /// cell[1,1] is element type 'Cell': True
        /// cell[2,1] is element type 'double': False (element is null.)
        /// </code></example>
        public static bool IsTypeOf<CellT>(this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, InArray<long> indices) {
            var ret = A.Storage.IsTypeOf<CellT>(indices);
            
            return ret;
        }

        /// <summary>
        /// Tests if a cell element is of the given element type. 
        /// </summary>
        /// <typeparam name="CellT">The array element type to probe the cell element for.</typeparam>
        /// <param name="A">The cell.</param>
        /// <param name="d0">Dimensional index into dimension #0.</param>
        /// <returns>true if the element found at the given position is an array of the given element type <typeparamref name="CellT"/>, false otherwise.</returns>
        /// <remarks><para>The method is helpful in order to inspect the contents of a cell array. If the actual types of cells in the cell array
        /// are unknown this function can be used to determine the type of the content of individual cells before attempting to retrieve any values.</para>
        /// <para>In most situations, elements of a cell are arrays of a distinct element type. This element type is given to 
        /// <see cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, InArray{long})"/> as 
        /// typeparameter <typeparamref name="CellT"/>. That means, in order to find out, if the first cell element stores an array of int 
        /// <c><![CDATA[cell.IsTypeOf<int>(0)]]></c> is used.</para>
        /// <para>When testing for <see cref="Logical"/> arrays <see cref="bool"/> is used for <typeparamref name="CellT"/>.</para>
        /// <para>In order to test for cell element type <typeparamref name="CellT"/> can be <see cref="Cell"/> or <see cref="BaseArray"/>: <c><![CDATA[cell.IsTypeOf<BaseArray>(0)]]></c>.</para>
        /// </remarks>
        /// <example>
        /// <para>In the following example a Cell of size 3x2 is created. It stores several array types, among which other cells are stored as elements of the outer cell.</para>
        /// <code><![CDATA[Cell cell1 = cell(size(3, 2), vector<BaseArray>(
        ///                     "first element"
        ///                     , 2.0
        ///                     , cell(arrays: vector<BaseArray>(Math.PI, 100f))
        ///                     , vector<short>(1, 2, 3, 4, 5, 6)
        ///                     , vector<double>(-1.4, -1.5, -1.6 )));
        /// 
        /// Console.Out.WriteLine($"cell[0,0] is element type 'string': {cell1.IsTypeOf<string>(0)}");
        /// Console.Out.WriteLine($"cell[0,0] is element type 'double': {cell1.IsTypeOf<double>(0)}");
        /// 
        /// Console.Out.WriteLine($"cell[1,0] is element type 'double': {cell1.IsTypeOf<double>(1,0)}");
        /// Console.Out.WriteLine($"cell[2,0] is element type 'Cell': {cell1.IsTypeOf<Cell>(2,0)}");
        /// 
        /// Console.Out.WriteLine($"cell[0,1] is element type 'short': {cell1.IsTypeOf<short>(0, 1)}");
        /// Console.Out.WriteLine($"cell[1,1] is element type 'Cell': {cell1.IsTypeOf<Cell>(1, 1)}");
        /// Console.Out.WriteLine($"cell[2,1] is element type 'double': {cell1.IsTypeOf<double>(2, 1)}");]]>
        /// </code>
        /// This gives the following output: 
        /// <code>
        /// cell[0,0] is element type 'string': True
        /// cell[0,0] is element type 'double': False
        /// cell[1,0] is element type 'double': True
        /// cell[2,0] is element type 'Cell': True
        /// cell[0,1] is element type 'short': True
        /// cell[1,1] is element type 'Cell': True
        /// cell[2,1] is element type 'double': False (element is null.)
        /// </code></example>
        public static bool IsTypeOf<CellT>(this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, long d0) {
            var ret = A.Storage.IsTypeOf<CellT>(MathInternal.size(d0));
            
            return ret;
        }
        /// <summary>
        /// Tests if a cell element is of the given element type. 
        /// </summary>
        /// <typeparam name="CellT">The array element type to probe the cell element for.</typeparam>
        /// <param name="A">The cell.</param>
        /// <param name="d0">Dimensional index into dimension #0.</param>
        /// <param name="d1">Dimensional index into dimension #1.</param>
        /// <returns>true if the element found at the given position is an array of the given element type <typeparamref name="CellT"/>, false otherwise.</returns>
        /// <remarks><para>The method is helpful in order to inspect the contents of a cell array. If the actual types of cells in the cell array
        /// are unknown this function can be used to determine the type of the content of individual cells before attempting to retrieve any values.</para>
        /// <para>In most situations, elements of a cell are arrays of a distinct element type. This element type is given to 
        /// <see cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, InArray{long})"/> as 
        /// typeparameter <typeparamref name="CellT"/>. That means, in order to find out, if the first cell element stores an array of int 
        /// <c><![CDATA[cell.IsTypeOf<int>(0)]]></c> is used.</para>
        /// <para>When testing for <see cref="Logical"/> arrays <see cref="bool"/> is used for <typeparamref name="CellT"/>.</para>
        /// <para>In order to test for cell element type <typeparamref name="CellT"/> can be <see cref="Cell"/> or <see cref="BaseArray"/>: <c><![CDATA[cell.IsTypeOf<BaseArray>(0)]]></c>.</para>
        /// </remarks>
        /// <example>
        /// <para>In the following example a Cell of size 3x2 is created. It stores several array types, among which other cells are stored as elements of the outer cell.</para>
        /// <code><![CDATA[Cell cell1 = cell(size(3, 2), vector<BaseArray>(
        ///                     "first element"
        ///                     , 2.0
        ///                     , cell(arrays: vector<BaseArray>(Math.PI, 100f))
        ///                     , vector<short>(1, 2, 3, 4, 5, 6)
        ///                     , vector<double>(-1.4, -1.5, -1.6 )));
        /// 
        /// Console.Out.WriteLine($"cell[0,0] is element type 'string': {cell1.IsTypeOf<string>(0)}");
        /// Console.Out.WriteLine($"cell[0,0] is element type 'double': {cell1.IsTypeOf<double>(0)}");
        /// 
        /// Console.Out.WriteLine($"cell[1,0] is element type 'double': {cell1.IsTypeOf<double>(1,0)}");
        /// Console.Out.WriteLine($"cell[2,0] is element type 'Cell': {cell1.IsTypeOf<Cell>(2,0)}");
        /// 
        /// Console.Out.WriteLine($"cell[0,1] is element type 'short': {cell1.IsTypeOf<short>(0, 1)}");
        /// Console.Out.WriteLine($"cell[1,1] is element type 'Cell': {cell1.IsTypeOf<Cell>(1, 1)}");
        /// Console.Out.WriteLine($"cell[2,1] is element type 'double': {cell1.IsTypeOf<double>(2, 1)}");]]>
        /// </code>
        /// This gives the following output: 
        /// <code>
        /// cell[0,0] is element type 'string': True
        /// cell[0,0] is element type 'double': False
        /// cell[1,0] is element type 'double': True
        /// cell[2,0] is element type 'Cell': True
        /// cell[0,1] is element type 'short': True
        /// cell[1,1] is element type 'Cell': True
        /// cell[2,1] is element type 'double': False (element is null.)
        /// </code></example>
        public static bool IsTypeOf<CellT>(this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, long d0, long d1) {
            var ret = A.Storage.IsTypeOf<CellT>(MathInternal.size(d0, d1));
            
            return ret;
        }
        /// <summary>
        /// Tests if a cell element is of the given element type. 
        /// </summary>
        /// <typeparam name="CellT">The array element type to probe the cell element for.</typeparam>
        /// <param name="A">The cell.</param>
        /// <param name="d0">Dimensional index into dimension #0.</param>
        /// <param name="d1">Dimensional index into dimension #1.</param>
        /// <param name="d2">Dimensional index into dimension #2.</param>
        /// <returns>true if the element found at the given position is an array of the given element type <typeparamref name="CellT"/>, false otherwise.</returns>
        /// <remarks><para>The method is helpful in order to inspect the contents of a cell array. If the actual types of cells in the cell array
        /// are unknown this function can be used to determine the type of the content of individual cells before attempting to retrieve any values.</para>
        /// <para>In most situations, elements of a cell are arrays of a distinct element type. This element type is given to 
        /// <see cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, InArray{long})"/> as 
        /// typeparameter <typeparamref name="CellT"/>. That means, in order to find out, if the first cell element stores an array of int 
        /// <c><![CDATA[cell.IsTypeOf<int>(0)]]></c> is used.</para>
        /// <para>When testing for <see cref="Logical"/> arrays <see cref="bool"/> is used for <typeparamref name="CellT"/>.</para>
        /// <para>In order to test for cell element type <typeparamref name="CellT"/> can be <see cref="Cell"/> or <see cref="BaseArray"/>: <c><![CDATA[cell.IsTypeOf<BaseArray>(0)]]></c>.</para>
        /// </remarks>
        /// <example>
        /// <para>In the following example a Cell of size 3x2 is created. It stores several array types, among which other cells are stored as elements of the outer cell.</para>
        /// <code><![CDATA[Cell cell1 = cell(size(3, 2), vector<BaseArray>(
        ///                     "first element"
        ///                     , 2.0
        ///                     , cell(arrays: vector<BaseArray>(Math.PI, 100f))
        ///                     , vector<short>(1, 2, 3, 4, 5, 6)
        ///                     , vector<double>(-1.4, -1.5, -1.6 )));
        /// 
        /// Console.Out.WriteLine($"cell[0,0] is element type 'string': {cell1.IsTypeOf<string>(0)}");
        /// Console.Out.WriteLine($"cell[0,0] is element type 'double': {cell1.IsTypeOf<double>(0)}");
        /// 
        /// Console.Out.WriteLine($"cell[1,0] is element type 'double': {cell1.IsTypeOf<double>(1,0)}");
        /// Console.Out.WriteLine($"cell[2,0] is element type 'Cell': {cell1.IsTypeOf<Cell>(2,0)}");
        /// 
        /// Console.Out.WriteLine($"cell[0,1] is element type 'short': {cell1.IsTypeOf<short>(0, 1)}");
        /// Console.Out.WriteLine($"cell[1,1] is element type 'Cell': {cell1.IsTypeOf<Cell>(1, 1)}");
        /// Console.Out.WriteLine($"cell[2,1] is element type 'double': {cell1.IsTypeOf<double>(2, 1)}");]]>
        /// </code>
        /// This gives the following output: 
        /// <code>
        /// cell[0,0] is element type 'string': True
        /// cell[0,0] is element type 'double': False
        /// cell[1,0] is element type 'double': True
        /// cell[2,0] is element type 'Cell': True
        /// cell[0,1] is element type 'short': True
        /// cell[1,1] is element type 'Cell': True
        /// cell[2,1] is element type 'double': False (element is null.)
        /// </code></example>
        public static bool IsTypeOf<CellT>(this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, long d0, long d1, long d2) {
            var ret = A.Storage.IsTypeOf<CellT>(MathInternal.size(d0, d1, d2));
            
            return ret;
        }
        /// <summary>
        /// Tests if a cell element is of the given element type. 
        /// </summary>
        /// <typeparam name="CellT">The array element type to probe the cell element for.</typeparam>
        /// <param name="A">The cell.</param>
        /// <param name="d0">Dimensional index into dimension #0.</param>
        /// <param name="d1">Dimensional index into dimension #1.</param>
        /// <param name="d2">Dimensional index into dimension #2.</param>
        /// <param name="d3">Dimensional index into dimension #3.</param>
        /// <returns>true if the element found at the given position is an array of the given element type <typeparamref name="CellT"/>, false otherwise.</returns>
        /// <remarks><para>The method is helpful in order to inspect the contents of a cell array. If the actual types of cells in the cell array
        /// are unknown this function can be used to determine the type of the content of individual cells before attempting to retrieve any values.</para>
        /// <para>In most situations, elements of a cell are arrays of a distinct element type. This element type is given to 
        /// <see cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, InArray{long})"/> as 
        /// typeparameter <typeparamref name="CellT"/>. That means, in order to find out, if the first cell element stores an array of int 
        /// <c><![CDATA[cell.IsTypeOf<int>(0)]]></c> is used.</para>
        /// <para>When testing for <see cref="Logical"/> arrays <see cref="bool"/> is used for <typeparamref name="CellT"/>.</para>
        /// <para>In order to test for cell element type <typeparamref name="CellT"/> can be <see cref="Cell"/> or <see cref="BaseArray"/>: <c><![CDATA[cell.IsTypeOf<BaseArray>(0)]]></c>.</para>
        /// </remarks>
        /// <example>
        /// <para>In the following example a Cell of size 3x2 is created. It stores several array types, among which other cells are stored as elements of the outer cell.</para>
        /// <code><![CDATA[Cell cell1 = cell(size(3, 2), vector<BaseArray>(
        ///                     "first element"
        ///                     , 2.0
        ///                     , cell(arrays: vector<BaseArray>(Math.PI, 100f))
        ///                     , vector<short>(1, 2, 3, 4, 5, 6)
        ///                     , vector<double>(-1.4, -1.5, -1.6 )));
        /// 
        /// Console.Out.WriteLine($"cell[0,0] is element type 'string': {cell1.IsTypeOf<string>(0)}");
        /// Console.Out.WriteLine($"cell[0,0] is element type 'double': {cell1.IsTypeOf<double>(0)}");
        /// 
        /// Console.Out.WriteLine($"cell[1,0] is element type 'double': {cell1.IsTypeOf<double>(1,0)}");
        /// Console.Out.WriteLine($"cell[2,0] is element type 'Cell': {cell1.IsTypeOf<Cell>(2,0)}");
        /// 
        /// Console.Out.WriteLine($"cell[0,1] is element type 'short': {cell1.IsTypeOf<short>(0, 1)}");
        /// Console.Out.WriteLine($"cell[1,1] is element type 'Cell': {cell1.IsTypeOf<Cell>(1, 1)}");
        /// Console.Out.WriteLine($"cell[2,1] is element type 'double': {cell1.IsTypeOf<double>(2, 1)}");]]>
        /// </code>
        /// This gives the following output: 
        /// <code>
        /// cell[0,0] is element type 'string': True
        /// cell[0,0] is element type 'double': False
        /// cell[1,0] is element type 'double': True
        /// cell[2,0] is element type 'Cell': True
        /// cell[0,1] is element type 'short': True
        /// cell[1,1] is element type 'Cell': True
        /// cell[2,1] is element type 'double': False (element is null.)
        /// </code></example>
        public static bool IsTypeOf<CellT>(this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, long d0, long d1, long d2, long d3) {
            var ret = A.Storage.IsTypeOf<CellT>(MathInternal.size(d0, d1, d2, d3));
            
            return ret;
        }
        /// <summary>
        /// Tests if a cell element is of the given element type. 
        /// </summary>
        /// <typeparam name="CellT">The array element type to probe the cell element for.</typeparam>
        /// <param name="A">The cell.</param>
        /// <param name="d0">Dimensional index into dimension #0.</param>
        /// <param name="d1">Dimensional index into dimension #1.</param>
        /// <param name="d2">Dimensional index into dimension #2.</param>
        /// <param name="d3">Dimensional index into dimension #3.</param>
        /// <param name="d4">Dimensional index into dimension #4.</param>
        /// <returns>true if the element found at the given position is an array of the given element type <typeparamref name="CellT"/>, false otherwise.</returns>
        /// <remarks><para>The method is helpful in order to inspect the contents of a cell array. If the actual types of cells in the cell array
        /// are unknown this function can be used to determine the type of the content of individual cells before attempting to retrieve any values.</para>
        /// <para>In most situations, elements of a cell are arrays of a distinct element type. This element type is given to 
        /// <see cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, InArray{long})"/> as 
        /// typeparameter <typeparamref name="CellT"/>. That means, in order to find out, if the first cell element stores an array of int 
        /// <c><![CDATA[cell.IsTypeOf<int>(0)]]></c> is used.</para>
        /// <para>When testing for <see cref="Logical"/> arrays <see cref="bool"/> is used for <typeparamref name="CellT"/>.</para>
        /// <para>In order to test for cell element type <typeparamref name="CellT"/> can be <see cref="Cell"/> or <see cref="BaseArray"/>: <c><![CDATA[cell.IsTypeOf<BaseArray>(0)]]></c>.</para>
        /// </remarks>
        /// <example>
        /// <para>In the following example a Cell of size 3x2 is created. It stores several array types, among which other cells are stored as elements of the outer cell.</para>
        /// <code><![CDATA[Cell cell1 = cell(size(3, 2), vector<BaseArray>(
        ///                     "first element"
        ///                     , 2.0
        ///                     , cell(arrays: vector<BaseArray>(Math.PI, 100f))
        ///                     , vector<short>(1, 2, 3, 4, 5, 6)
        ///                     , vector<double>(-1.4, -1.5, -1.6 )));
        /// 
        /// Console.Out.WriteLine($"cell[0,0] is element type 'string': {cell1.IsTypeOf<string>(0)}");
        /// Console.Out.WriteLine($"cell[0,0] is element type 'double': {cell1.IsTypeOf<double>(0)}");
        /// 
        /// Console.Out.WriteLine($"cell[1,0] is element type 'double': {cell1.IsTypeOf<double>(1,0)}");
        /// Console.Out.WriteLine($"cell[2,0] is element type 'Cell': {cell1.IsTypeOf<Cell>(2,0)}");
        /// 
        /// Console.Out.WriteLine($"cell[0,1] is element type 'short': {cell1.IsTypeOf<short>(0, 1)}");
        /// Console.Out.WriteLine($"cell[1,1] is element type 'Cell': {cell1.IsTypeOf<Cell>(1, 1)}");
        /// Console.Out.WriteLine($"cell[2,1] is element type 'double': {cell1.IsTypeOf<double>(2, 1)}");]]>
        /// </code>
        /// This gives the following output: 
        /// <code>
        /// cell[0,0] is element type 'string': True
        /// cell[0,0] is element type 'double': False
        /// cell[1,0] is element type 'double': True
        /// cell[2,0] is element type 'Cell': True
        /// cell[0,1] is element type 'short': True
        /// cell[1,1] is element type 'Cell': True
        /// cell[2,1] is element type 'double': False (element is null.)
        /// </code></example>
        public static bool IsTypeOf<CellT>(this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, long d0, long d1, long d2, long d3, long d4) {
            var ret = A.Storage.IsTypeOf<CellT>(MathInternal.size(d0, d1, d2, d3, d4));
            
            return ret;
        }
        /// <summary>
        /// Tests if a cell element is of the given element type. 
        /// </summary>
        /// <typeparam name="CellT">The array element type to probe the cell element for.</typeparam>
        /// <param name="A">The cell.</param>
        /// <param name="d0">Dimensional index into dimension #0.</param>
        /// <param name="d1">Dimensional index into dimension #1.</param>
        /// <param name="d2">Dimensional index into dimension #2.</param>
        /// <param name="d3">Dimensional index into dimension #3.</param>
        /// <param name="d4">Dimensional index into dimension #4.</param>
        /// <param name="d5">Dimensional index into dimension #5.</param>
        /// <returns>true if the element found at the given position is an array of the given element type <typeparamref name="CellT"/>, false otherwise.</returns>
        /// <remarks><para>The method is helpful in order to inspect the contents of a cell array. If the actual types of cells in the cell array
        /// are unknown this function can be used to determine the type of the content of individual cells before attempting to retrieve any values.</para>
        /// <para>In most situations, elements of a cell are arrays of a distinct element type. This element type is given to 
        /// <see cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, InArray{long})"/> as 
        /// typeparameter <typeparamref name="CellT"/>. That means, in order to find out, if the first cell element stores an array of int 
        /// <c><![CDATA[cell.IsTypeOf<int>(0)]]></c> is used.</para>
        /// <para>When testing for <see cref="Logical"/> arrays <see cref="bool"/> is used for <typeparamref name="CellT"/>.</para>
        /// <para>In order to test for cell element type <typeparamref name="CellT"/> can be <see cref="Cell"/> or <see cref="BaseArray"/>: <c><![CDATA[cell.IsTypeOf<BaseArray>(0)]]></c>.</para>
        /// </remarks>
        /// <example>
        /// <para>In the following example a Cell of size 3x2 is created. It stores several array types, among which other cells are stored as elements of the outer cell.</para>
        /// <code><![CDATA[Cell cell1 = cell(size(3, 2), vector<BaseArray>(
        ///                     "first element"
        ///                     , 2.0
        ///                     , cell(arrays: vector<BaseArray>(Math.PI, 100f))
        ///                     , vector<short>(1, 2, 3, 4, 5, 6)
        ///                     , vector<double>(-1.4, -1.5, -1.6 )));
        /// 
        /// Console.Out.WriteLine($"cell[0,0] is element type 'string': {cell1.IsTypeOf<string>(0)}");
        /// Console.Out.WriteLine($"cell[0,0] is element type 'double': {cell1.IsTypeOf<double>(0)}");
        /// 
        /// Console.Out.WriteLine($"cell[1,0] is element type 'double': {cell1.IsTypeOf<double>(1,0)}");
        /// Console.Out.WriteLine($"cell[2,0] is element type 'Cell': {cell1.IsTypeOf<Cell>(2,0)}");
        /// 
        /// Console.Out.WriteLine($"cell[0,1] is element type 'short': {cell1.IsTypeOf<short>(0, 1)}");
        /// Console.Out.WriteLine($"cell[1,1] is element type 'Cell': {cell1.IsTypeOf<Cell>(1, 1)}");
        /// Console.Out.WriteLine($"cell[2,1] is element type 'double': {cell1.IsTypeOf<double>(2, 1)}");]]>
        /// </code>
        /// This gives the following output: 
        /// <code>
        /// cell[0,0] is element type 'string': True
        /// cell[0,0] is element type 'double': False
        /// cell[1,0] is element type 'double': True
        /// cell[2,0] is element type 'Cell': True
        /// cell[0,1] is element type 'short': True
        /// cell[1,1] is element type 'Cell': True
        /// cell[2,1] is element type 'double': False (element is null.)
        /// </code></example>
        public static bool IsTypeOf<CellT>(this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, long d0, long d1, long d2, long d3, long d4, long d5) {
            var ret = A.Storage.IsTypeOf<CellT>(MathInternal.size(d0, d1, d2, d3, d4, d5));
            
            return ret;
        }
        /// <summary>
        /// Tests if a cell element is of the given element type. 
        /// </summary>
        /// <typeparam name="CellT">The array element type to probe the cell element for.</typeparam>
        /// <param name="A">The cell.</param>
        /// <param name="d0">Dimensional index into dimension #0.</param>
        /// <param name="d1">Dimensional index into dimension #1.</param>
        /// <param name="d2">Dimensional index into dimension #2.</param>
        /// <param name="d3">Dimensional index into dimension #3.</param>
        /// <param name="d4">Dimensional index into dimension #4.</param>
        /// <param name="d5">Dimensional index into dimension #5.</param>
        /// <param name="d6">Dimensional index into dimension #6.</param>
        /// <returns>true if the element found at the given position is an array of the given element type <typeparamref name="CellT"/>, false otherwise.</returns>
        /// <remarks><para>The method is helpful in order to inspect the contents of a cell array. If the actual types of cells in the cell array
        /// are unknown this function can be used to determine the type of the content of individual cells before attempting to retrieve any values.</para>
        /// <para>In most situations, elements of a cell are arrays of a distinct element type. This element type is given to 
        /// <see cref="IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, InArray{long})"/> as 
        /// typeparameter <typeparamref name="CellT"/>. That means, in order to find out, if the first cell element stores an array of int 
        /// <c><![CDATA[cell.IsTypeOf<int>(0)]]></c> is used.</para>
        /// <para>When testing for <see cref="Logical"/> arrays <see cref="bool"/> is used for <typeparamref name="CellT"/>.</para>
        /// <para>In order to test for cell element type <typeparamref name="CellT"/> can be <see cref="Cell"/> or <see cref="BaseArray"/>: <c><![CDATA[cell.IsTypeOf<BaseArray>(0)]]></c>.</para>
        /// </remarks>
        /// <example>
        /// <para>In the following example a Cell of size 3x2 is created. It stores several array types, among which other cells are stored as elements of the outer cell.</para>
        /// <code><![CDATA[Cell cell1 = cell(size(3, 2), vector<BaseArray>(
        ///                     "first element"
        ///                     , 2.0
        ///                     , cell(arrays: vector<BaseArray>(Math.PI, 100f))
        ///                     , vector<short>(1, 2, 3, 4, 5, 6)
        ///                     , vector<double>(-1.4, -1.5, -1.6 )));
        /// 
        /// Console.Out.WriteLine($"cell[0,0] is element type 'string': {cell1.IsTypeOf<string>(0)}");
        /// Console.Out.WriteLine($"cell[0,0] is element type 'double': {cell1.IsTypeOf<double>(0)}");
        /// 
        /// Console.Out.WriteLine($"cell[1,0] is element type 'double': {cell1.IsTypeOf<double>(1,0)}");
        /// Console.Out.WriteLine($"cell[2,0] is element type 'Cell': {cell1.IsTypeOf<Cell>(2,0)}");
        /// 
        /// Console.Out.WriteLine($"cell[0,1] is element type 'short': {cell1.IsTypeOf<short>(0, 1)}");
        /// Console.Out.WriteLine($"cell[1,1] is element type 'Cell': {cell1.IsTypeOf<Cell>(1, 1)}");
        /// Console.Out.WriteLine($"cell[2,1] is element type 'double': {cell1.IsTypeOf<double>(2, 1)}");]]>
        /// </code>
        /// This gives the following output: 
        /// <code>
        /// cell[0,0] is element type 'string': True
        /// cell[0,0] is element type 'double': False
        /// cell[1,0] is element type 'double': True
        /// cell[2,0] is element type 'Cell': True
        /// cell[0,1] is element type 'short': True
        /// cell[1,1] is element type 'Cell': True
        /// cell[2,1] is element type 'double': False (element is null.)
        /// </code></example>
        public static bool IsTypeOf<CellT>(this ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, long d0, long d1, long d2, long d3, long d4, long d5, long d6) {
            var ret = A.Storage.IsTypeOf<CellT>(MathInternal.size(d0, d1, d2, d3, d4, d5, d6));
            
            return ret;
        }

        #endregion
    }
}
