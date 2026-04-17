using ILNumerics.Core.Arrays;
using ILNumerics.Core.Global;
using ILNumerics.Core.MemoryLayer;
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;

namespace ILNumerics.Core.Misc {

    /// <summary>
    /// This class provides helper temp storages / buffers for various functions within the Core lib. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="LocalT"></typeparam>
    /// <typeparam name="InT"></typeparam>
    /// <typeparam name="OutT"></typeparam>
    /// <typeparam name="RetT"></typeparam>
    /// <typeparam name="StorageT"></typeparam>
    
    internal sealed unsafe class ThreadingContext<T, LocalT, InT, OutT, RetT, StorageT>
        where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

        #region thread static singleton
        [ThreadStatic]
        private static ThreadingContext<T, LocalT, InT, OutT, RetT, StorageT> s_context;

        public static ThreadingContext<T, LocalT, InT, OutT, RetT, StorageT> Context {
            get {
                if (s_context == null) {
                    s_context = new ThreadingContext<T, LocalT, InT, OutT, RetT, StorageT>();
                }
                return s_context;
            }
        }

        internal DimSpec[] DimSpecArray => m_dimSpecArray;
        internal BaseArray[] BaseArrayArray => m_baseArrayArray; 

        internal IIndexIterator[] IndexIteratorArray => m_indexIteratorArray;
        /// <summary>
        /// Thread local itarator array for np advanced indexing. Length <see cref="Size.MaxNumberOfDimensions"/> + 1 (space for reordering).
        /// </summary>
        internal Iterators.MultidimIterator* MultidimIterators => (Iterators.MultidimIterator*)m_multidimIteratorArray.Pointer;
        /// <summary>
        /// Temp buffer of 1000 long elements. Used for numpy advanced indexing and others. Not for users use. 
        /// </summary>
        internal long* TmpBuffer1000 => (long*)m_tempBufferLong1000.Pointer;
        #endregion

        #region attributes / properties
        // multidim iterators are required for np_indexing. They are structs which live on the unmanaged heap. Hence we create the buffer 
        // via a memory pool here, which might look wasteful, though.
        private MemoryHandle m_multidimIteratorArray = DeviceManagement.DeviceManager.GetDevice(0).New< Iterators.MultidimIterator>(Size.MaxNumberOfDimensions + 1); // +1 -> right side value in SetRange_np()private MemoryHandle m_tempBufferLong1000 = DeviceManagement.DeviceManager.GetDevice(0).New<long>(1000); 
        private MemoryHandle m_tempBufferLong1000 = DeviceManagement.DeviceManager.GetDevice(0).New<long>(1000);
        private IIndexIterator[] m_indexIteratorArray = new IIndexIterator[Size.MaxNumberOfDimensions];
        private DimSpec[] m_dimSpecArray = new DimSpec[Size.MaxNumberOfDimensions * 2]; //  '* 2' is required, so that more dimspec parameters can be provided (newaxis, ellipsis, 0) than dimensions existing in the array.
        private BaseArray[] m_baseArrayArray = new BaseArray[Size.MaxNumberOfDimensions * 2]; //  '* 2' is required, so that more dimspec parameters can be provided (newaxis, ellipsis, 0) than dimensions existing in the array.

#endregion

#region constructors
        ThreadingContext() {  }

#endregion

    }
}
