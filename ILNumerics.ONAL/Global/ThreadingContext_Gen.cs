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
