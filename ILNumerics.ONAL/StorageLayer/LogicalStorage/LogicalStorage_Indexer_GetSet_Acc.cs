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
