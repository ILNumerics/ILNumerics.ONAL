using ILNumerics.Core.StorageLayer;
using ILNumerics.Core.Arrays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILNumerics {
    /// <summary>
    /// Input /output array type to be used as parameter in user defined functions with multiple return values. 
    /// </summary>
    public sealed partial class OutArray<T>

        : Mutable<T, Array<T>, InArray<T>, OutArray<T>, Array<T>, Storage<T>> {

        #region construction
        internal OutArray(Storage<T> storage, object synch) : base(storage, synch) { }

        #endregion

        internal override void Release() {
            // nothing to do here ... 
            // m_storage.Release();
        }

        #region conversional operators
        /// <summary>
        /// Implicitly creates an output parameter type array from the local array <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Local (source) array.</param>
        /// <returns>Output parameter type array, references the original array.</returns>
        public static implicit operator OutArray<T>(Array<T> A) {
            return A?.Storage.OutArray;
        }
        #endregion

        #region mutating functions

        /// <summary>
        /// Assign another array to this array variable. This is an optional, yet more efficient alternative to using '=' for assigning to the array directly.
        /// </summary>
        /// <param name="value">The new array.</param>
        /// <remarks>By using this method, the storage of this array is immediately released to the memory pool and replaced by the new arrays content. In difference to that, 
        /// by using the common '=' assignment operator, the existing arrays storage is released only at the time, the current 
        /// <see cref="M:ILNumerics.Scope.Enter"/> block  is left. Therefeore, prefer this method if a 
        /// smaller memory pool is crucial.</remarks>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.Assign(ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT})"/>
        public new Array<T> a
        {
            set { Assign(value); }
        }

        #endregion

    }
}
