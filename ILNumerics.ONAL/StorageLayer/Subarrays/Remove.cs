using ILNumerics.Core.Arrays;
using ILNumerics.Core.Functions.Builtin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.StorageLayer {
    public abstract partial class BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> : IDisposable, IStorage
        where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {


        #region Mutable Remove 

        
        internal unsafe StorageT Remove(IIndexIterator[] iterators, uint iteratorsLength) {

            // The storage returned from Remove() is going to be assigned to the entry array directly. If it is 
            // "This" storage, a NOP results. Another storage is assigned atomically. All fine. 
            using (Scope.Enter()) {
                //if (m_arrayCounter > 1 && IsReady) {
                //    // should not happen in regular situations: 
                //    throw new InvalidOperationException("This array is currently shared in multiple instances and cannot get altered.");
                //}

                if (iterators == null || iteratorsLength == 0) {
                    // nothing to do
                    return this as StorageT;
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
                    var emptyStorage = Create(default(T), iteratorsLength);
                    var bsd = emptyStorage.S.GetBSD(true);
                    for (int i = 0; i < iteratorsLength; i++) {
                        bsd[3 + i] = 0; 
                        bsd[3 + i + iteratorsLength] = 0;
                    }
                    bsd[1] = bsd[3];
                    bsd[2] = 0;
                    bsd[0] = iteratorsLength;

                    //Assign(emptyStorage, true, true);
                    return emptyStorage; 
                    #endregion
                } else {

                    #region prepare (invert) the omitted (unspecified) subarray parameter indices for copying
                    if (removalIndices == null || removalIndices.GetLength() == 0) {
                        return this as StorageT;
                    }
                    long[] removeIndices = removalIndices.Distinct().ToArray();
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
                    StorageT ret = this as StorageT; 
                    if (iterators[iteratorsLength - 1].GetMaximum() >= Size[iteratorsLength - 1]) {
                        System.Diagnostics.Debug.Assert(iterators[iteratorsLength - 1].GetMaximum() <= iterators[iteratorsLength - 1].GetLastDimensionIndex()); 
                        ret = ret.EnsureStorageOrder(StorageOrders.ColumnMajor);  // this may or may not produces a new storage as copy
                    }
                    var final = ret.iterateSubarrayML_StrgT(iterators, iteratorsLength, false);
                    if (!object.ReferenceEquals(final, ret) && !object.ReferenceEquals(ret, this) && ret.ReferenceCount == 0) {
                        // ret is intermediate storage from EnsureStorageOrder. Must clean-up manually for now.
                        ret.Retain(); ret.Release();  // ... TODO: there must be better ways to do this. 
                    }
                    //Assign(ret, true, true); // we are Mutable<>! 
                    return final; 
                }

            }

        }
        #endregion

    }
}
