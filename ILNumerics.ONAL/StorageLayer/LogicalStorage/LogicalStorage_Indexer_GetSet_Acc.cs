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
namespace ILNumerics.Core.StorageLayer {
    /// <summary>
    /// Internal storage container for logical arrays. This class is used internally. 
    /// </summary>
    public partial class LogicalStorage

        : BaseStorage<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>, IStorage {

        #region Mutable_IndexerSet overloads
        internal override LogicalStorage MutableIndexer_Set(LogicalStorage value, long d0) {
            var ret = base.MutableIndexer_Set(value, d0);
            ret.m_numberTrues = -1;
            return ret;
        }
        internal override LogicalStorage MutableIndexer_Set(LogicalStorage value, long d0, long d1) {
            var ret = base.MutableIndexer_Set(value, d0, d1);
            ret.m_numberTrues = -1;
            return ret;
        }
        internal override LogicalStorage MutableIndexer_Set(LogicalStorage value, long d0, long d1, long d2) {
            var ret = base.MutableIndexer_Set(value, d0, d1, d2);
            ret.m_numberTrues = -1;
            return ret;
        }
        internal override LogicalStorage MutableIndexer_Set(LogicalStorage value, long d0, long d1, long d2, long d3) {
            var ret = base.MutableIndexer_Set(value, d0, d1, d2, d3);
            ret.m_numberTrues = -1;
            return ret;
        }
        internal override LogicalStorage MutableIndexer_Set(LogicalStorage value, long d0, long d1, long d2, long d3, long d4) {
            var ret = base.MutableIndexer_Set(value, d0, d1, d2, d3, d4);
            ret.m_numberTrues = -1;
            return ret;
        }
        internal override LogicalStorage MutableIndexer_Set(LogicalStorage value, long d0, long d1, long d2, long d3, long d4, long d5) {
            var ret = base.MutableIndexer_Set(value, d0, d1, d2, d3, d4, d5);
            ret.m_numberTrues = -1;
            return ret;
        }
        internal override LogicalStorage MutableIndexer_Set(LogicalStorage value, long d0, long d1, long d2, long d3, long d4, long d5, long d6) {
            var ret = base.MutableIndexer_Set(value, d0, d1, d2, d3, d4, d5, d6);
            ret.m_numberTrues = -1;
            return ret;
        }
        #endregion

    }
}
