// ToDo: DOKU Missing all over for ILExpression.cs
#pragma warning disable 1591


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using ILNumerics.Core.StorageLayer;
using System.Security;
using System.Security.Permissions;
using System.Collections.Concurrent;
using System.Threading;
using System.Collections;

namespace ILNumerics.Core.StorageLayer {

    /// <summary>
    /// This class internally supports the ILNumerics iterator infrastructure. 
    /// </summary>
    [Serializable]
    public struct ScalarIndexIterator : IIndexIterator {

        long m_value;
        bool m_isAtEnd; 

        public ScalarIndexIterator(long value) {
            m_value = value;
            m_isAtEnd = false; 
        }
        public long Current => m_value;

        object IEnumerator.Current => m_value;

        public long GetLength() => 1;

        public long? GetMaximum() => m_value; 

        public long? GetMinimum() => m_value; 

        public long GetLastDimensionIndex() {
            throw new NotImplementedException();
        }

        public long? GetStepSize() => 0;

        public IEnumerator<long> GetEnumerator() => this;

        IEnumerator IEnumerable.GetEnumerator() => this; 

        public void Dispose() { }

        public bool MoveNext() {
            var ret = !m_isAtEnd; 
            if (!m_isAtEnd) {
                m_isAtEnd = true; 
            }
            return ret; 
        }

        public void Reset() {
            m_isAtEnd = false; 
        }
    }
}