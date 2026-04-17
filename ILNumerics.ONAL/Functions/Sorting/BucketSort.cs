using ILNumerics.Core.Functions.Builtin;
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ILNumerics.Core.Misc {

    /// <summary>
    /// Bucket sort algorithm (for internal use)
    /// </summary>
    /// <remarks>This class is not intended to be used directly. Sorting functionality is supplied by <see cref="M:ILMath.sort(InArray{double})"/></remarks>
    public class BucketSort {
        /// <summary>
        /// Sort method for bucket sorts
        /// </summary>
        public enum SortMethod {
            /// <summary>
            /// Constant length
            /// </summary>
            ConstantLength,
            /// <summary>
            /// Variable length
            /// </summary>
            VariableLenth
        }

        /// <summary>
        /// Bucket sort algorithm 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="indices">Return corresponding source element indices</param>
        /// <param name="mapper"></param>
        /// <param name="method"></param>
        public static QueueList<ElementType,IndexType> BucketSortDo<ElementType,SubelementType,IndexType> (
                                            IEnumerable<ElementType> input,
                                            IEnumerable<IndexType> indices,
                                            KeyMapper<ElementType,SubelementType> mapper,
                                            SortMethod method) {
            if (mapper == null) 
                throw new InvalidOperationException("BucketSort: key mapper must not be null!");
            if (input == null) {
                return new QueueList<ElementType,IndexType>(); 
            }
            // do the sort now
            switch (method) {
                case SortMethod.VariableLenth: 
                    return bucketSort_variableLength<ElementType,SubelementType,IndexType>(input,mapper); 
                case SortMethod.ConstantLength:
                    if (indices != null) 
                        return bucketSort_constantLength(input,indices,mapper);
                    else 
                        return bucketSort_constantLength<ElementType,SubelementType,IndexType>(input,mapper);
                default: 
                    throw new ArgumentException("BucketSort: unknown sort method specified."); 
            }
        }

        private static QueueList<ElementType, IndexType>
            bucketSort_variableLength<ElementType, SubelementType, IndexType>(
                                    IEnumerable<ElementType> input,
                                    KeyMapper<ElementType, SubelementType> mapper) {
            using (Scope.Enter()) {
                int m = mapper.NumberOfKeys;
                QueueList<ElementType, IndexType>[] buckets = new QueueList<ElementType, IndexType>[m];
                QueueList<ElementType, IndexType> Q = new QueueList<ElementType, IndexType>();
                IEnumerable<ElementType> inp = input;

                #region compute lmax
                int maxLen = 0, tmp = 0;
                foreach (ElementType elem in inp) {
                    tmp = mapper.SubelementsCount(elem);
                    if (tmp > maxLen)
                        maxLen = tmp;
                }
                #endregion
                #region create Lengths array
                QueueList<ElementType, IndexType>[] Lengths = new QueueList<ElementType, IndexType>[maxLen];
                foreach (ElementType elem in inp) {
                    int len = mapper.SubelementsCount(elem) - 1;
                    if (Lengths[len] == null) {
                        Lengths[len] = new QueueList<ElementType, IndexType>();
                    }
                    Lengths[len].Enqueue(elem);
                }
                #endregion
                #region create bucket indices for each position: Noempty array
                QueueList<int, byte>[] Noempty = new QueueList<int, byte>[maxLen];
                Logical alreadyFound = LogicalStorage.Create(MathInternal.vector<long>(maxLen, m), StorageOrders.ColumnMajor).RetArray;

                foreach (ElementType s in inp) {
                    for (int l = mapper.SubelementsCount(s); l-- > 0; ) {
                        int hpos = mapper.Map(s, l, 0);
                        if (!alreadyFound.GetValue((uint)l, (uint)hpos)) {
                            alreadyFound.SetValue(true, l, hpos);
                            if (Noempty[l] == null) {
                                // create list and init with new value
                                Noempty[l] = new QueueList<int, byte>();
                            }
                            Noempty[l].Enqueue(hpos);
                        }
                    }
                }
                alreadyFound.Dispose();
                for (int i = 0; i < maxLen; i++) {
                    // sort lists for each length
                    Noempty[i] = bucketSort_constantLength<int, int, byte>(Noempty[i], new IntLimitedKeyMapper(m));
                }
                #endregion
                #region sort
                ListItem<ElementType, IndexType> curElement;
                for (int l = maxLen; l-- > 0; ) {
                    // sort Length[l] list
                    if (Lengths[l] != null) {
                        Q.AddToStart(Lengths[l]);
                    }
                    // sort from last sorting loop (l+1)
                    while (Q.Count > 0) {
                        curElement = Q.Dequeue();
                        int hpos = mapper.Map(curElement.Data, l, 0);
                        if (buckets[hpos] == null) {
                            buckets[hpos] = new QueueList<ElementType, IndexType>();
                        }
                        buckets[hpos].Enqueue(curElement);
                    }
                    // collect queues
                    foreach (int c in Noempty[l]) {
                        Q.Enqueue(buckets[c]);
                        buckets[c].Clear();
                    }
                }
                #endregion
                return Q;
            }
        }
        internal static QueueList<ElementType,IndexType> 
            bucketSort_constantLength<ElementType,SubelementType,IndexType>(
                                    IEnumerable<ElementType> input,
                                    KeyMapper<ElementType,SubelementType> mapper) {
            int m = mapper.NumberOfKeys;
            int tmp = 0; 
            QueueList<ElementType,IndexType>[] buckets = new QueueList<ElementType,IndexType>[m];
            QueueList<ElementType,IndexType> Q = new QueueList<ElementType,IndexType>(); 
            // find longest element 
            int maxLen = 0;
            foreach (ElementType elem in input) {
                tmp = mapper.SubelementsCount( elem );  
                if (tmp > maxLen)
                    maxLen = tmp;
            }
            // sort into buckets 
            tmp = 0; 
            for (int k = maxLen; k-- > 0; ) {
                // walk along the input 
                if (k == maxLen - 1) {
                    foreach (ElementType elem in input) {
                        // put current into bucket
                        int bpos = mapper.Map(elem,k,0);
                        if (buckets[bpos] == null) {
                            // must create list first
                            buckets[bpos] = new QueueList<ElementType,IndexType>();
                        } 
                        // append to list 
                        buckets[bpos].Enqueue(elem); 
                    }
                } else {
                    while (Q.Count > 0) {
                        // put current into bucket
                        ListItem<ElementType,IndexType> elem = Q.Dequeue(); 
                        int bpos = mapper.Map(elem.Data,k,0);
                        if (buckets[bpos] == null) {
                            // must create list first
                            buckets[bpos] = new QueueList<ElementType,IndexType>();
                        } 
                        // append to list 
                        buckets[bpos].Enqueue(elem); 
                    }
                }
                // concatenate all buckets 
                for (int i = 0; i < buckets.Length; i++) {
                    if (buckets[i] != null && buckets[i].Count > 0) {
                        Q.Enqueue(buckets[i]); 
                        buckets[i].Clear();
                    }
                }
                // goto previous position in strings 
            }
            return Q;
        }

        internal static QueueList<ElementType,IndexType> 
            bucketSort_constantLength<ElementType,SubelementType,IndexType>(
                                            IEnumerable<ElementType> input,
                                            IEnumerable<IndexType> indices, 
                                            KeyMapper<ElementType,SubelementType> mapper) {
            int m = mapper.NumberOfKeys;
            int tmp = 0; 
            QueueList<ElementType,IndexType>[] buckets = new QueueList<ElementType,IndexType>[m];
            QueueList<ElementType,IndexType> Q = new QueueList<ElementType,IndexType>(); 
            // find longest element 
            int maxLen = 0;
            foreach (ElementType elem in input) {
                tmp = mapper.SubelementsCount( elem );  
                if (tmp > maxLen)
                    maxLen = tmp;
            }
            // sort into buckets 
            IEnumerator<IndexType> indIt = indices.GetEnumerator();
            indIt.MoveNext(); 
            for (int k = maxLen; k-- > 0; ) {
                // walk along the input 
                if (k == maxLen - 1) {
                    foreach (ElementType elem in input) {
                        // put current into bucket
                        int bpos = mapper.Map(elem,k,0);
                        if (buckets[bpos] == null) {
                            // must create list first
                            buckets[bpos] = new QueueList<ElementType,IndexType>();
                        } 
                        // append to list 
                        buckets[bpos].Enqueue(elem,indIt.Current);
                        indIt.MoveNext(); 
                    }
                } else {
                    while (Q.Count > 0) {
                        // put current into bucket
                        ListItem<ElementType,IndexType> elem = Q.Dequeue(); 
                        int bpos = mapper.Map(elem.Data,k,0);
                        if (buckets[bpos] == null) {
                            // must create list first
                            buckets[bpos] = new QueueList<ElementType,IndexType>();
                        } 
                        // append to list 
                        buckets[bpos].Enqueue(elem); 
                    }
                }
                // concatenate all buckets 
                for (int i = 0; i < buckets.Length; i++) {
                    if (buckets[i] != null && buckets[i].Count > 0) {
                        Q.Enqueue(buckets[i]); 
                        buckets[i].Clear();
                    }
                }
                // goto previous position in strings 
            }
            return Q;
        }

    }
}
