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
using ILNumerics.Core.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ILNumerics;


namespace ILNumerics.Core.Functions.Builtin {

    internal static partial class MathInternal {

        #region bucket sort - string
        /// <summary>
        /// Sort strings in A along first non singleton dimension, ascending order.
        /// </summary>
        /// <param name="A">Input array. A may be an empty, scalar, vector or matrix.</param>
        /// <returns>Sorted array of the same size/shape as A</returns>
        /// <remarks><para>The strings in A will be sorted lexicographically in ascending order using the bucket sort algorithm. Data 
        /// along the first non singleton dimension will get sorted independently from data 
        /// in the other rows/columns.</para>
        /// <para>The sorting order of strings is determined char-wise by comparing the ASCII codes of the characters.</para></remarks>
        internal static Array<string> sort(InArray<string> A) {
            using (Scope.Enter()) {
                Array<string> _A = A; 
                int fnsd = (int)_A.Size.WorkingDimension();
                if (fnsd < 0) return _A.C;
                return sort(_A, fnsd, false);
            }
        }
        /// <summary>
        /// Sort strings in A along first non singleton dimension
        /// </summary>
        /// <param name="A">Input array. A may be an empty, scalar, vector or matrix.</param>
        /// <param name="descending">Specifies the direction of sorting: true: descending sort direction; false: ascending</param>
        /// <returns>Sorted array of the same size/shape as A</returns>
        /// <remarks><para>The strings in A will be sorted lexicographically in ascending order using the bucket sort algorithm. Data 
        /// along the first non singleton dimension will get sorted independently from data 
        /// in the other rows/columns.</para>
        /// <para>The sorting order of strings is determined char-wise by comparing the ASCII codes of the characters.</para></remarks>
        internal static Array<string> sort(InArray<string> A, bool descending) {
            using (Scope.Enter(A)) {
                Array<string> _A = A;
                int fnsd = (int)_A.Size.WorkingDimension();
                if (fnsd < 0) return _A.C;
                return sort(_A, fnsd, descending);
            }
        }
        /// <summary>
        /// Sort strings in A along dimension 'dim'
        /// </summary>
        /// <param name="A">Input array. A may be an empty, scalar, vector or matrix.</param>
        /// <param name="dim">Dimension to sort along</param>
        /// <param name="descending">Specifies the direction of sorting: true: descending sort direction; false: ascending</param>
        /// <returns>Sorted array of the same size/shape as A</returns>
        /// <remarks><para>The strings in A will be sorted lexicographically in ascending order using the bucket sort algorithm. Data 
        /// along the first non singleton dimension will get sorted independently from data 
        /// in the other rows/columns.</para>
        /// <para>The sorting order of strings is determined char-wise by comparing the ASCII codes of the characters.</para></remarks>
        internal static Array<string> sort(InArray<string> A, int dim, bool descending) {
            if (object.Equals(A, null))
                throw new Exception("Parameter 'A' must not be null.");
            using (Scope.Enter(A)) {
                Array<string> _A = A;
                if (_A.Size.NumberOfDimensions > 2)
                    throw new ArgumentException("for element type string only matrices are supported!");
                if (dim < 0 || dim >= _A.Size.NumberOfDimensions)
                    throw new ArgumentException("invalid dimension argument");
                // early exit: scalar/ empty
                if (_A.IsEmpty || _A.IsScalar)
                    return _A.C;
                Array<string> ret = zeros<string>(_A.Size);
                uint dim1 = (uint)(dim % _A.Size.NumberOfDimensions);
                uint dim2 = (uint)((dim1 + 1) % _A.Size.NumberOfDimensions);
                long maxRuns = _A.Size[dim2];
                QueueList<string, byte> ql;
                Array<int>[] ind = new Array<int>[2];
                int[] indI = new int[2];
                ASCIIKeyMapper km = new ASCIIKeyMapper();
                for (int c = 0; c < maxRuns; c++) {
                    ind[dim2] = c;
                    ind[dim1] = null;
                    ql = BucketSort.BucketSortDo<string, char, byte>(_A[ind].Iterator(StorageOrders.ColumnMajor), null, km, BucketSort.SortMethod.ConstantLength);
                    indI[dim2] = c;
                    if (descending) {
                        for (int i = ql.Count; i-- > 0;) {
                            indI[dim1] = i;
                            ret.SetValue(ql.Dequeue().Data, indI[0], indI[1]);
                        }
                    } else {
                        for (int i = 0; ql.Count > 0; i++) {
                            indI[dim1] = i;
                            ret.SetValue(ql.Dequeue().Data, indI[0], indI[1]);
                        }
                    }
                }
                return ret;
            }
        }
        /// <summary>
        /// Sort data in A along dimension 'dim'
        /// </summary>
        /// <param name="A">Input array: empty, scalar, vector or matrix</param>
        /// <param name="descending">Specifies the direction of sorting</param>
        /// <param name="dim">Dimension to sort along</param>
        /// <param name="Indices">[Output] Returns permutation matrix also</param>
        /// <returns>Sorted array of the same size as A</returns>
        /// <remarks><para>The data in A will be sorted using the bucket sort algorithm. Data 
        /// along the dimension <paramref name="dim"/> will get sorted independently from data 
        /// in the next row/column.</para>
        /// <para>This overload also returns an array 'Indices' which will hold the indices into the original 
        /// elements <b>after sorting</b>. Elements of 'Indices' are of type double.</para>
        ///</remarks>
        internal static Array<string> sort(InArray<string> A, OutArray<double> Indices, int dim, bool descending) {
            if (object.Equals(A, null))
                throw new ArgumentNullException("Parameter 'A' must not be null.");
            if (Equals(Indices, null)) {
                throw new ArgumentNullException("Out-parameter 'Indices' must not be null on entry.");
            }
            using (Scope.Enter(A)) {
                Array<string> _A = A;
                if (_A.Size.NumberOfDimensions > 2)
                    throw new ArgumentException("for element type string only matrices are supported!");
                if (dim < 0 || dim >= _A.Size.NumberOfDimensions)
                    throw new ArgumentException("invalid dimension argument");
                // early exit: scalar/ empty
                if (_A.IsEmpty || _A.IsScalar) {
                    
                    lock(Indices.SynchObj)
                        Indices.a = 0.0;
                    return _A.C;
                }
                Array<string> ret = zeros<string>(_A.Size);
                uint dim1 = (uint)(dim % _A.Size.NumberOfDimensions);
                uint dim2 = (uint)((dim1 + 1) % _A.Size.NumberOfDimensions);
                long maxRuns = _A.Size[dim2];
                QueueList<string, double> ql;
                BaseArray[] ind = new BaseArray[2];
                int[] indI = new int[2];
                ASCIIKeyMapper km = new ASCIIKeyMapper();
                lock (Indices.SynchObj) {
                    Indices.a = MathInternal.counter<double>(0.0, 1.0, _A.S[0], _A.S[1]);
                    for (int c = 0; c < maxRuns; c++) {
                        ind[dim2] = (Array<int>)c;
                        ind[dim1] = Globals.full;
                        ql = BucketSort.BucketSortDo<string, char, double>(_A[ind].Iterator(StorageOrders.ColumnMajor), Indices[ind].Iterator(StorageOrders.ColumnMajor), km, BucketSort.SortMethod.ConstantLength);
                        indI[dim2] = c;
                        if (descending) {
                            for (int i = ql.Count; i-- > 0;) {
                                indI[dim1] = i;
                                ListItem<string, double> item = ql.Dequeue();
                                ret.SetValue(item.Data, indI[0], indI[1]);
                                Indices.SetValue(item.m_index, indI[0], indI[1]);

                            }
                        } else {
                            for (int i = 0; ql.Count > 0; i++) {
                                indI[dim1] = i;
                                ListItem<string, double> item = ql.Dequeue();
                                ret.SetValue(item.Data, indI[0], indI[1]);
                                Indices.SetValue(item.m_index, indI[0], indI[1]);
                            }
                        }
                    }
                }
                return ret;
            }
        }
        /// <summary>
        /// Generic bucket sort algorithm in A along arbitrary dimension 
        /// </summary>
        /// <param name="A">Input array: empty, scalar, vector or matrix</param>
        /// <param name="descending">Specifies the direction of sorting</param>
        /// <param name="dim">Dimension to sort along</param>
        /// <param name="Indices">[Input/Output] The values in Indices will be returned in the same sorted order as the elements 
        /// in A. This can be used to derive a permutation matrix of the sorted indices.</param>
        /// <typeparam name="T">Element type of values of A</typeparam>
        /// <typeparam name="S">Subelement type. For element type of string this would be 'char'</typeparam>
        /// <typeparam name="I">Element type of indices</typeparam>
        /// <param name="keymapper">Instancce of an object of type ILKeyMapper. This object must 
        /// be derived from ILKeyMapper{T,SubelementType} and match the generic argument <typeparamref name="T"/>. It will be 
        /// used to split single elements into its subelements and map their content into bucket numbers. For all 
        /// reference types except those of type string you will have to write your own ILKeyMapper class for that purpose.</param>
        /// <returns>Sorted array of the same size as A</returns>
        /// <remarks><para>The data in A will be sorted using the bucket sort algorithm. Data 
        /// along the dimension <paramref name="dim"/> will get sorted independently. I.e., for dim = 0, columns are sorted independently.</para>
        /// <para>This overload also returns an array 'Indices' which will hold the indices into the original 
        /// elements <b>after sorting</b>. Therefore, the unsorted indices must be provided by the user on entry. Indices must not be null.</para>
        /// <para>This generic version is able to sort arbitrary element types. Even user defined reference types can be sorted 
        /// by specifying a user defined ILKeyMapper class instance. Also the type of Indices may be arbitrarily choosen. In difference 
        /// to the regular sort function overload, Indices must manually be given to the function on entry. Elements in 'Indices'
        /// are sorted in the same order as the elements of A.</para>
        /// <para>By using this overload you may use the same permutation matrix several times to reflect the 
        /// manipulations done to A due multiple sort processes. The Indices given will directly be used for the sorting 
        /// disregarding initial order.</para>
        /// </remarks>
        internal static Array<T> sort<T, S, I>(InArray<T> A, OutArray<I> Indices, int dim, bool descending, KeyMapper<T, S> keymapper) {
            if (object.Equals(A, null))
                throw new Exception("Parameter A must not be null.");
            if (object.Equals(Indices, null))
                throw new Exception("Parameter 'Indices' must not be null.");
            using (Scope.Enter(A)) {
                if (!A.IsMatrix)
                    throw new ArgumentException("Only matrices are supported for sorting generic data.");
                Array<T> _A = A; 
                if (dim < 0 || dim >= _A.Size.NumberOfDimensions)
                    throw new ArgumentException("Invalid dimension argument: " + dim + ".");
                if (keymapper == null)
                    throw new ArgumentException("The keymapper argument must not be null.");

                Array<T> ret = zeros<T>(_A.Size);
                lock (Indices.SynchObj) {

                    if (!Indices.Size.IsSameSize(_A.Size))
                        throw new ArgumentException("'indices' argument must have same size and shape as input argument 'A'.");
                    // early exit: scalar/ empty
                    if (_A.IsEmpty || _A.IsScalar) {
                        Indices.a = default(I);
                        return _A.C;
                    }
                    uint dim1 = (uint)(dim % _A.Size.NumberOfDimensions);
                    uint dim2 = (uint)((dim1 + 1) % _A.Size.NumberOfDimensions);
                    long maxRuns = _A.Size[dim2];
                    QueueList<T, I> ql;
                    BaseArray[] ind = new BaseArray[2];
                    int[] indI = new int[2];
                    for (int c = 0; c < maxRuns; c++) {
                        ind[dim2] = (Array<int>)c;
                        ind[dim1] = Globals.full;
                        ql = BucketSort.BucketSortDo<T, S, I>(_A[ind].Iterator<T>(StorageOrders.ColumnMajor), Indices[ind].Iterator(StorageOrders.ColumnMajor), keymapper, BucketSort.SortMethod.ConstantLength);
                        indI[dim2] = c;
                        if (descending) {
                            for (int i = ql.Count; i-- > 0;) {
                                indI[dim1] = i;
                                ListItem<T, I> item = ql.Dequeue();
                                ret.SetValue(item.Data, indI[0], indI[1]);
                                Indices.SetValue(item.m_index, indI[0], indI[1]);
                            }
                        } else {
                            for (int i = 0; ql.Count > 0; i++) {
                                indI[dim1] = i;
                                ListItem<T, I> item = ql.Dequeue();
                                ret.SetValue(item.Data, indI[0], indI[1]);
                                Indices.SetValue(item.m_index, indI[0], indI[1]);
                            }
                        }
                    }
                }
                return ret;
            }
        }
        #endregion



    }
}
