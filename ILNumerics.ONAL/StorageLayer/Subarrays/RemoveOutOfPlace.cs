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
using ILNumerics.Core.Arrays;
using ILNumerics.Core.Functions.Builtin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.StorageLayer {
    public static partial class ExtensionMethods {

        #region Mutable Remove 
        /// <summary>
        /// Remove all but specified dimensions from <paramref name="iterators"/>.
        /// </summary>
        /// <param name="storage"></param>
        /// <param name="iterators"></param>
        /// <param name="iteratorsLength"></param>
        /// <returns>New storage with ref.count = 0.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentException"></exception>

        internal unsafe static StorageT Remove<T, LocalT, InT, OutT, RetT, StorageT>(this StorageT storage, IIndexIterator[] iterators, uint iteratorsLength) 
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {
            using (Scope.Enter()) {

                //if (storage.ReferenceCount > 1 && storage.IsReady) {
                //    // should not happen in regular situations: 
                //    throw new InvalidOperationException("This array is currently shared in multiple instances and cannot get altered.");
                //}

                if (iterators == null || iteratorsLength == 0) {
                    // nothing to do
                    return storage;
                }
                // valid input? 
                int removalDimIdx = -1;

                IIndexIterator removalIndices = null;
                long removalOrigDimLen = -1;
                for (uint i = 0; i < iteratorsLength; i++) {
                    if (!Global.Helper.isFullDimSpec(iterators[i])) {
                        if (removalDimIdx >= 0) {
                            throw new ArgumentException($"All but exactly one dimension must be specified as 'full' or simple range, as r(?,?,?) or '?:?:?'. Found two non-full dimensions: #{removalDimIdx} and {i}.");
                        }
                        removalDimIdx = (int)i;
                        removalIndices = iterators[i];
                        removalOrigDimLen = removalIndices.GetLastDimensionIndex() + 1;
                    }
                }
                // in cells we must release to-be-removed values. Caution! Performing this here (already) may leaves the storage crippled if something goes wrong afterwards! 
                // The first attempt was to release all affected values in the next line.
                //(this as CellStorage)?.WriteTo_ML_IterIter_T(CellStorage.Create((BaseArray)null), iterators, iteratorsLength);
                // But according to how Remove works we must ensure that all Releases are done by Assign()!

                // indices are valid for removal
                if (removalIndices == null) {
                    #region special case: remove all
                    //all dims fully specified
                    var emptyStorage = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Create(default(T), iteratorsLength);
                    var bsd = emptyStorage.S.GetBSD(true);
                    for (int i = 0; i < iteratorsLength; i++) {
                        bsd[3 + i] = 0; 
                        bsd[3 + i + iteratorsLength] = 0;
                    }
                    bsd[1] = bsd[3];
                    bsd[2] = 0;
                    bsd[0] = iteratorsLength;

                    storage.Assign(emptyStorage, toOutT: true, fromRetT: true);

                    return emptyStorage; 
                    #endregion
                } else {

                    #region prepare / invert the missing subarray parameter for copying
                    long[] removeIndices = removalIndices.Distinct().ToArray();

                    if (removeIndices == null || removeIndices.Length == 0) {
                        return storage as StorageT;
                    }
                    System.Array.Sort(removeIndices);
                    // prepare array
                    Storage<long> removeStorage = Storage<long>.Create();
                    var handle = removeStorage.New((ulong)(removalOrigDimLen - removeIndices.Length), clear: false);
                    removeStorage.Handles[0] = handle;
                    long* iP = (long*)handle.Pointer;

                    int j = 0;
                    foreach (var ind in removeIndices) {
                        if (ind >= removalOrigDimLen || ind < 0) throw new ArgumentException($"Removal index out of range in dimension #{removalDimIdx}: {ind}.");
                        while (j < ind && j < removalOrigDimLen) {
                            *(iP++) = j++;
                        }
                        if (j == ind) j++;
                    } // <- disposes removeindices here !!!
                    while (j < removalOrigDimLen) {
                        *(iP++) = j++;
                    }
                    // size of index array
                    removeStorage.S.SetAll((iP - (long*)handle.Pointer), 1);
                    iterators[removalDimIdx] = removeStorage.RetArray.IndexIterator(lastDimensionIdx: removalOrigDimLen);
                    #endregion

                    // make sure iterators[removalIdx] and this storage are compatible
                    if (iterators[iteratorsLength - 1].GetMaximum() >= storage.Size[iteratorsLength - 1]) {
                        System.Diagnostics.Debug.Assert(iterators[iteratorsLength - 1].GetMaximum() <= iterators[iteratorsLength - 1].GetLastDimensionIndex()); 
                        storage = storage.EnsureStorageOrder(StorageOrders.ColumnMajor); 
                    }
                    var tmpVal = storage.iterateSubarrayML_StrgT(iterators, iteratorsLength, false);

                    storage.Assign(tmpVal, toOutT: true, fromRetT: true);

                    return tmpVal;
                }

            }

        }
        #endregion

    }
}
