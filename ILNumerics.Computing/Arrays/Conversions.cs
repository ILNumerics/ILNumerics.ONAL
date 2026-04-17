using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.Arrays {

    public abstract partial class ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT>   // T1 must not read "T" to prevent from conflicts with the .T (transpose) property.
        : BaseArray<T1>, IEnumerable<T1>
        where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {

        #region implicit casts: scalar -> system type
        /// <summary>
        /// Cast scalar array to system value type.
        /// </summary>
        /// <param name="a">Source array.</param>
        /// <exception cref="InvalidCastException">if the source array <paramref name="a"/> is not scalar or null.</exception>
        public static explicit operator T1(ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> a) {
            if (object.Equals(a, null) || a.m_storage.Size.NumberOfElements != 1) {
                throw new InvalidCastException($"Only scalar arrays can be cast to system value types. The source array has {a.m_storage.Size.NumberOfElements} elements.");
            }
            var ret = (T1)a.GetItem(0); // etItem() disposes RetT! 
            return ret;
        }

        #endregion

    }
}
