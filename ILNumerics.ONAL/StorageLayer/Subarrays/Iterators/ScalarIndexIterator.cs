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