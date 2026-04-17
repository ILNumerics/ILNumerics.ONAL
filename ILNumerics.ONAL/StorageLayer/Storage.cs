namespace ILNumerics.Core.StorageLayer {
    /// <summary>
    /// Main rectilinear storage object. This storage type is used by Array{T} &amp; Co internally.
    /// </summary>
    /// <typeparam name="T">Element type.</typeparam>
    public sealed class Storage<T> 
        : BaseStorage<T, Array<T>, InArray<T>, OutArray<T>, Array<T>, Storage<T>> {

#region constructors

        /// <summary>
        /// Creates new storage of undefined size.
        /// </summary>
        public Storage() : base() {
            var synch = new object(); 
            m_array = new Array<T>(this, synch);
            m_inArray = new InArray<T>(this);
            m_outArray = new OutArray<T>(this, synch);
            m_retArray = new Array<T>(this, synch);
        }

#endregion

    }
}
