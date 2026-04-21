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
using System;
using System.Collections.Generic;
using System.Collections; 

namespace ILNumerics.Core.Misc {
    /// <summary>
    /// List items to be used in QueueList
    /// </summary>
    /// <typeparam name="T1">Data type</typeparam>
    /// <typeparam name="T2">Index type</typeparam>
    /// <remarks>This class is not intended to be used directly. Internally it serves as a helper class for sorting.</remarks>
    public class ListItem<T1, T2> {
        internal T1 Data; 
        internal T2 m_index; 
        internal ListItem<T1,T2> Next; 
        /// <summary>
        /// construct list item by data
        /// </summary>
        /// <param name="item">item data</param>
        /// <remarks>the indet will be set to its default value</remarks>
        public ListItem(T1 item) {
            Data = item; 
            m_index = default(T2); 
        }
        /// <summary>
        /// construct list item, takes item data and index
        /// </summary>
        /// <param name="item">item data</param>
        /// <param name="index">index</param>
        public ListItem(T1 item, T2 index) {
            Data = item; 
            m_index = index; 
        }
        /// <summary>
        /// index stored with this item
        /// </summary>
        public T2 Index {
            get {
                return m_index; 
            } 
            set {
                m_index = value; 
            }
        }
    }
    /// <summary>
    /// Queuelist - a queue with partial list properties 
    /// </summary>
    /// <typeparam name="T1">data type</typeparam>
    /// <typeparam name="T2">index type</typeparam>
    /// <remarks>This class is not intended to be used directly. Internally it serves as a helper class for sorting.</remarks>
    public class QueueList<T1, T2> : IEnumerable<T1> {
        internal ListItem<T1,T2> Head;  
        internal ListItem<T1,T2> Tail;  
        internal int m_count = 0; 
        /// <summary>
        /// number of items currently in the queue (readonly)
        /// </summary>
        public int Count {                    
            get {
                return m_count; 
            }
        }

        /// <summary>
        /// add indexed item at end of queue
        /// </summary>
        public void Enqueue(T1 item, T2 index) {
            if (Head == null) {
                Head = new ListItem<T1,T2>(item, index); 
                Tail = Head; 
            } else {
                ListItem<T1,T2> newItem = new ListItem<T1,T2>(item,index); 
                Tail.Next = newItem; 
                Tail = newItem; 
            }
            m_count++; 
        }

        /// <summary>
        /// add item at end of queue
        /// </summary>
        public void Enqueue(T1 item) {
            if (Head == null) {
                Head = new ListItem<T1,T2>(item); 
                Tail = Head; 
            } else {
                ListItem<T1,T2> newItem = new ListItem<T1,T2>(item); 
                Tail.Next = newItem; 
                Tail = newItem; 
            }
            m_count++; 
        }
        /// <summary>
        /// add item at end of queue
        /// </summary>
        public void Enqueue(ListItem<T1,T2> item) {
            System.Diagnostics.Debug.Assert(item.Next == null); 
            if (Head == null) {
                Head = item; 
                Tail = item; 
            } else {
                Tail.Next = item; 
                Tail = item; 
            }
            m_count++; 
        }
        /// <summary>
        /// add queue list to end of this queue list
        /// </summary>
        /// <param name="list">queue list to be added</param>
        public void Enqueue(QueueList<T1,T2> list) {
            if (list == null || list.Count == 0) return; 
            if (Tail != null) {
                Tail.Next = list.Head; 
                Tail = list.Tail; 
            } else {
                Head = list.Head; 
                Tail = list.Tail; 
            }
            m_count += list.m_count; 
        }
        
        /// <summary>
        /// Remove from start of queue
        /// </summary>
        /// <returns>item from start of queue</returns>
        public ListItem<T1,T2> Dequeue() {
            switch (m_count) {
                case 0: 
                    return null; 
                case 1:
                    ListItem<T1,T2> retIt = Head;
                    Head = null; 
                    Tail = null; 
                    m_count--; 
                    System.Diagnostics.Debug.Assert(retIt.Next == null);
                    return retIt; 
                default: 
                    retIt = Head;
                    Head = Head.Next; 
                    m_count--; 
                    retIt.Next = null; 
                    return retIt; 
            }
        }

        /// <summary>
        /// Add to start of queue
        /// </summary>
        /// <param name="item">item data to add to start of queue</param>
        public void AddToStart(T1 item) {
            ListItem<T1,T2> newLItem = new ListItem<T1,T2>(item); 
            newLItem.Next = Head;
            Head = newLItem; 
            m_count++; 
        }
        /// <summary>
        /// concatenate 2 queuelists
        /// </summary>
        /// <param name="qlist">queue list to be added at start of this queuelist</param>
        public void AddToStart(QueueList<T1,T2> qlist) {
            m_count += qlist.m_count; 
            qlist.Tail.Next = Head; 
            Head = qlist.Head; 
        }
        /// <summary>
        /// sort utilizing bucket sort
        /// </summary>
        /// <typeparam name="SubelementType">subelement type</typeparam>
        /// <param name="mapper">keymapper mapping subelement items to buckets</param>
        public void Sort<SubelementType>(KeyMapper<T1,SubelementType> mapper) {
            QueueList<T1,T2> ret = BucketSort.BucketSortDo<T1,SubelementType,T2>(this,null,mapper,BucketSort.SortMethod.ConstantLength); 
            this.Head = ret.Head;
            this.Tail = ret.Tail; 
        }
        /// <summary>
        /// convert (copy) items to system array
        /// </summary>
        /// <returns>system array with items</returns>
        public T1[] ToArray() {
            T1[] ret = new T1[m_count];
            ListItem<T1,T2> curr = Head; 
            for (int i = 0; curr != null; i++) {
                ret[i] = curr.Data; 
                curr = curr.Next; 
            }
            return ret; 
        }
        /// <summary>
        /// Clear this queue list from all elements 
        /// </summary>
        public void Clear() {
            Head = null; 
            Tail = null; 
            m_count = 0; 
        }

        #region IEnumerable<T> Member
        /// <summary>
        /// Create enumerator utilizing 'foreach'
        /// </summary>
        /// <returns>enumerator for contained elements</returns>
        public IEnumerator<T1> GetEnumerator() {
            ListItem<T1,T2> curr = Head; 
            while (curr != null) { 
                yield return curr.Data; 
                curr = curr.Next; 
            }
        }
        /// <summary>
        /// Gives enumerator for contained items (ILListItem)
        /// </summary>
        public IEnumerable<ListItem<T1,T2>> ListItems {
            get {
                ListItem<T1,T2> curr = Head; 
                while (curr != null) { 
                    yield return curr; 
                    curr = curr.Next; 
                }
            }
        }
        #endregion

        #region IEnumerable Member
        /// <summary>
        /// gives enumerator for internal list items (ILListItem)
        /// </summary>
        /// <returns>ILListItem's</returns>
        IEnumerator IEnumerable.GetEnumerator() {
            ListItem<T1,T2> curr = Head; 
            while (curr != null) {
                yield return curr; 
                curr = curr.Next;
            }
        }

        #endregion
    }       
}
