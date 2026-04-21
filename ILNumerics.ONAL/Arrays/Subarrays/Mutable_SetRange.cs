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
using ILNumerics.Core.Global;
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.Arrays {

    /// <summary>
    /// Base class for all mutable array types. Internal use.
    /// </summary>
    public abstract partial class Mutable<T1, LocalT, InT, OutT, RetT, StorageT>   // T1 must not read "T" to prevent from conflicts with the .T (transpose) property.
        : ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT>
        where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {

        #region SetRange dimspec
        /// <summary>
        /// Overwrites the values from elements of this array at positions corresponding to the indices / ranges provided with values of elements from <paramref name="value"/>.
        /// </summary>
        /// <param name="value">Array with new values.</param>
        /// <param name="d0">Range / index defining the 1st dimension of the region to change.</param>
        /// <remarks><para>This function supports Matlab and numpy indexing and array styles. Expansion, removal and broadcasting / advanced indexing is supported 
        /// as specified.</para>
        /// <para>This API is threadsafe: concurrent writes and reads to this array are allowed without corrupting the array's internal data structures. However, 
        /// concurrent reads may see partial element modifications, though. 
        /// </para></remarks>
        /// <seealso href="https://ilnumerics.net/ilnumerics-threading-model.html"/>
        public void SetRange(BaseArray<T1> value, DimSpec d0) {
            lock (SynchObj) {


 // if pending asynch segments reading from this storage still exist we must clone + rename first: 
                using var _1 = ReaderLock.Create(value as ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT>, out var valStorage); 
                m_storage.Assign(m_storage.SetRange(valStorage, d0), true, true);
                d0.Dispose();
            }        
        }
        /// <summary>
        /// Overwrites the values from elements of this array at positions corresponding to the indices / ranges provided with values of elements from <paramref name="value"/>.
        /// </summary>
        /// <param name="value">Array with new values.</param>
        /// <param name="d0">Range / index defining the 1st dimension of the region to change.</param>
        /// <param name="d1"></param>
        /// <remarks><para>This function supports Matlab and numpy indexing and array styles. Expansion, removal and broadcasting / advanced indexing is supported 
        /// as specified.</para>
        /// <para>This API is threadsafe: concurrent writes and reads to this array are allowed without corrupting the array's internal data structures. However, 
        /// concurrent reads may see partial element modifications, though. 
        /// </para></remarks>
        /// <seealso href="https://ilnumerics.net/ilnumerics-threading-model.html"/>
        public void SetRange(BaseArray<T1> value, DimSpec d0, DimSpec d1) {

            lock (SynchObj) {


 // if pending asynch segments reading from this storage still exist we must clone + rename first: 
                using var _1 = ReaderLock.Create(value as ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT>, out var valStorage);
                m_storage.Assign(m_storage.SetRange(valStorage, d0, d1), true, true);
                d0.Dispose(); d1.Dispose();
            }
        }
        /// <summary>
        /// Overwrites the values from elements of this array at positions corresponding to the indices / ranges provided with values of elements from <paramref name="value"/>.
        /// </summary>
        /// <param name="value">Array with new values.</param>
        /// <param name="d0">Range / index defining the 1st dimension of the region to change.</param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <remarks><para>This function supports Matlab and numpy indexing and array styles. Expansion, removal and broadcasting / advanced indexing is supported 
        /// as specified.</para>
        /// <para>This API is threadsafe: concurrent writes and reads to this array are allowed without corrupting the array's internal data structures. However, 
        /// concurrent reads may see partial element modifications, though. 
        /// </para></remarks>
        /// <seealso href="https://ilnumerics.net/ilnumerics-threading-model.html"/>
        public void SetRange(BaseArray<T1> value, DimSpec d0, DimSpec d1, DimSpec d2) {
            lock (SynchObj) {


 // if pending asynch segments reading from this storage still exist we must clone + rename first: 
                using var _1 = ReaderLock.Create(value as ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT>, out var valStorage);
                m_storage.Assign(m_storage.SetRange(valStorage, d0, d1, d2), true, true);
                d0.Dispose(); d1.Dispose(); d2.Dispose();
            }
        }
        /// <summary>
        /// Overwrites the values from elements of this array at positions corresponding to the indices / ranges provided with values of elements from <paramref name="value"/>.
        /// </summary>
        /// <param name="value">Array with new values.</param>
        /// <param name="d0">Range / index defining the 1st dimension of the region to change.</param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <param name="d3"></param>
        /// <remarks><para>This function supports Matlab and numpy indexing and array styles. Expansion, removal and broadcasting / advanced indexing is supported 
        /// as specified.</para>
        /// <para>This API is threadsafe: concurrent writes and reads to this array are allowed without corrupting the array's internal data structures. However, 
        /// concurrent reads may see partial element modifications, though. 
        /// </para></remarks>
        /// <seealso href="https://ilnumerics.net/ilnumerics-threading-model.html"/>
        public void SetRange(BaseArray<T1> value, DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3) {
            lock (SynchObj) {


 // if pending asynch segments reading from this storage still exist we must clone + rename first: 
                using var _1 = ReaderLock.Create(value as ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT>, out var valStorage);
                m_storage.Assign(m_storage.SetRange(valStorage, d0, d1, d2, d3), true, true);
                d0.Dispose(); d1.Dispose(); d2.Dispose(); d3.Dispose();
            }
        }
        /// <summary>
        /// Overwrites the values from elements of this array at positions corresponding to the indices / ranges provided with values of elements from <paramref name="value"/>.
        /// </summary>
        /// <param name="value">Array with new values.</param>
        /// <param name="d0">Range / index defining the 1st dimension of the region to change.</param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <param name="d3"></param>
        /// <param name="d4"></param>
        /// <remarks><para>This function supports Matlab and numpy indexing and array styles. Expansion, removal and broadcasting / advanced indexing is supported 
        /// as specified.</para>
        /// <para>This API is threadsafe: concurrent writes and reads to this array are allowed without corrupting the array's internal data structures. However, 
        /// concurrent reads may see partial element modifications, though. 
        /// </para></remarks>
        /// <seealso href="https://ilnumerics.net/ilnumerics-threading-model.html"/>
        public void SetRange(BaseArray<T1> value, DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3, DimSpec d4) {
            lock (SynchObj) {


 // if pending asynch segments reading from this storage still exist we must clone + rename first: 
                using var _1 = ReaderLock.Create(value as ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT>, out var valStorage);
                m_storage.Assign(m_storage.SetRange(valStorage, d0, d1, d2, d3, d4), true, true);
                d0.Dispose(); d1.Dispose(); d2.Dispose(); d3.Dispose(); d4.Dispose();
            }
        }
        /// <summary>
        /// Overwrites the values from elements of this array at positions corresponding to the indices / ranges provided with values of elements from <paramref name="value"/>.
        /// </summary>
        /// <param name="value">Array with new values.</param>
        /// <param name="d0">Range / index defining the 1st dimension (index #0) of the region to update / set.</param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <param name="d3"></param>
        /// <param name="d4"></param>
        /// <param name="d5"></param>
        /// <remarks><para>This function supports Matlab and numpy indexing and array styles. Expansion, removal and broadcasting / advanced indexing is supported 
        /// as specified.</para>
        /// <para>This API is threadsafe: concurrent writes and reads to this array are allowed without corrupting the array's internal data structures. However, 
        /// concurrent reads may see partial element modifications, though. 
        /// </para></remarks>
        /// <seealso href="https://ilnumerics.net/ilnumerics-threading-model.html"/>
        public void SetRange(BaseArray<T1> value, DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3, DimSpec d4, DimSpec d5) {
            lock (SynchObj) {


 // if pending asynch segments reading from this storage still exist we must clone + rename first: 
                using var _1 = ReaderLock.Create(value as ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT>, out var valStorage);
                m_storage.Assign(m_storage.SetRange(valStorage, d0, d1, d2, d3, d4, d5), true, true);
                d0.Dispose(); d1.Dispose(); d2.Dispose(); d3.Dispose(); d4.Dispose(); d5.Dispose();
            }
        }
        public void SetRange(BaseArray<T1> value, DimSpec d0, DimSpec d1, DimSpec d2, DimSpec d3, DimSpec d4, DimSpec d5, DimSpec d6) {
            lock (SynchObj) {
 // if pending asynch segments reading from this storage still exist we must clone + rename first: 


                using var _1 = ReaderLock.Create(value as ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT>, out var valStorage);
                m_storage.Assign(m_storage.SetRange(valStorage, d0, d1, d2, d3, d4, d5, d6), true, true);
                d0.Dispose(); d1.Dispose(); d2.Dispose(); d3.Dispose(); d4.Dispose(); d5.Dispose(); d6.Dispose();
            }
        }
        /// <summary>
        /// Overwrites the values from elements of this array at positions corresponding to the indices / ranges provided with values of elements from <paramref name="value"/>.
        /// </summary>
        /// <param name="value">Array with new values.</param>
        /// <param name="dims">System.Array with indices / ranges defining the region to update / set.</param>
        /// <remarks><para>This function supports Matlab and numpy indexing and array styles. Expansion, removal and broadcasting / advanced indexing is supported 
        /// as specified.</para>
        /// <para>This API is threadsafe: concurrent writes and reads to this array are allowed without corrupting the array's internal data structures. However, 
        /// concurrent reads on the same array instance may see partial element modifications, though. 
        /// </para></remarks>
        /// <seealso href="https://ilnumerics.net/ilnumerics-threading-model.html"/>
        public void SetRange(BaseArray<T1> value, params DimSpec[] dims) {
            lock (SynchObj) {


 // if pending asynch segments reading from this storage still exist we must clone + rename first: 
                using var _1 = ReaderLock.Create(value as ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT>, out var valStorage);
                m_storage.Assign(m_storage.SetRange(valStorage, dims), true, true);
                foreach (var dim in dims) dim?.Dispose(); 
            }
        }

        #endregion

        #region SetRange BaseArray indices
        public void SetRange(BaseArray<T1> value, BaseArray d0) {
            lock (SynchObj) {


 // if pending asynch segments reading from this storage still exist we must clone + rename first: 
                using var _1 = ReaderLock.Create(value as ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT>, out var valStorage);
                m_storage.Assign(m_storage.SetRange(valStorage, d0), true, true);
            }
        }
        public void SetRange(BaseArray<T1> value, BaseArray d0, BaseArray d1) {
            lock (SynchObj) {


 // if pending asynch segments reading from this storage still exist we must clone + rename first: 
                using var _1 = ReaderLock.Create(value as ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT>, out var valStorage);
                m_storage.Assign(m_storage.SetRange(valStorage, d0, d1), true, true);
            }
        }
        public void SetRange(BaseArray<T1> value, BaseArray d0, BaseArray d1, BaseArray d2) {
            lock (SynchObj) {


 // if pending asynch segments reading from this storage still exist we must clone + rename first: 
                using var _1 = ReaderLock.Create(value as ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT>, out var valStorage);
                m_storage.Assign(m_storage.SetRange(valStorage, d0, d1, d2), true, true);
            }
        }
        public void SetRange(BaseArray<T1> value, BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3) {
            lock (SynchObj) {


 // if pending asynch segments reading from this storage still exist we must clone + rename first: 
                using var _1 = ReaderLock.Create(value as ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT>, out var valStorage);
                m_storage.Assign(m_storage.SetRange(valStorage, d0, d1, d2, d3), true, true);
            }
        }
        public void SetRange(BaseArray<T1> value, BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3, BaseArray d4) {
            lock (SynchObj) {


 // if pending asynch segments reading from this storage still exist we must clone + rename first: 
                using var _1 = ReaderLock.Create(value as ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT>, out var valStorage);
                m_storage.Assign(m_storage.SetRange(valStorage, d0, d1, d2, d3, d4), true, true);
            }
        }
        public void SetRange(BaseArray<T1> value, BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3, BaseArray d4, BaseArray d5) {
            lock (SynchObj) {


 // if pending asynch segments reading from this storage still exist we must clone + rename first: 
                using var _1 = ReaderLock.Create(value as ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT>, out var valStorage);
                m_storage.Assign(m_storage.SetRange(valStorage, d0, d1, d2, d3, d4, d5), true, true);
            }
        }
        public void SetRange(BaseArray<T1> value, BaseArray d0, BaseArray d1, BaseArray d2, BaseArray d3, BaseArray d4, BaseArray d5, BaseArray d6) {
            lock (SynchObj) {


 // if pending asynch segments reading from this storage still exist we must clone + rename first: 
                using var _1 = ReaderLock.Create(value as ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT>, out var valStorage);
                m_storage.Assign(m_storage.SetRange(valStorage, d0, d1, d2, d3, d4, d5, d6), true, true);
            }
        }
        public void SetRange(BaseArray<T1> value, params BaseArray[] dims) {
            lock (SynchObj) {


 // if pending asynch segments reading from this storage still exist we must clone + rename first: 
                using var _1 = ReaderLock.Create(value as ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT>, out var valStorage);
                m_storage.Assign(m_storage.SetRange(valStorage, dims), true, true);
            }
        }
        #endregion

        /// <summary>
        /// Assign another array to this array variable. This is an optional, yet more efficient alternative to using '=' for assigning to the array directly.
        /// </summary>
        /// <param name="value">The new array.</param>
        /// <remarks>By using this method, the storage of this array is immediately released to the memory pool and replaced by the new arrays content. In difference to that, 
        /// by using the common '=' assignment operator, the existing arrays storage is released only at the time, the current 
        /// <see cref="M:ILNumerics.Scope.Enter"/> block  is left. Therefeore, prefer this method if a 
        /// smaller memory pool is crucial.</remarks>
        /// <seealso cref="ILNumerics.OutArray{T}.a"/>
        public RetT a {
            set {
                if (!Equals(value, null) &&
                    m_storage.IsReady &&
                    ReferenceEquals(this.m_storage, value.m_storage) && 
                    m_storage.ReferenceCount == 2) { 
                    // special case A.a = A; (as returned from optimizing attempts in some functions)                   
                    m_storage.Release(); 
                    // nothing more to do ...
                } else {
                    Assign(value);
                }
            }
        }

        /// <summary>
        /// Assign another array to this array variable. A more efficient way to using '=' for assigning to a local array directly.
        /// </summary>
        /// <param name="value">The new array.</param>
        /// <remarks>By using this method, the storage of this array is immediately released to the memory pool and replaced by the new arrays content. In difference to that, 
        /// by using the common '=' assignment operator, the existing arrays storage is released only at the time, the current 
        /// <see cref="M:ILNumerics.Scope.Enter"/> block  is left. Therefeore, prefer this method if a 
        /// smaller memory footprint is important.</remarks>
        /// <seealso cref="ILNumerics.OutArray{T}.a"/>
        public void Assign(ConcreteArray<T1,LocalT,InT,OutT,RetT,StorageT> value) {
            StorageT rsValueStorage = (value as RetT)?.Storage;  // below Assign op may flip storages! We store the _original_ for later disposal.
            m_storage.Assign(value?.m_storage, this is OutT, false);
            rsValueStorage?.Release();
        }
    }
}
