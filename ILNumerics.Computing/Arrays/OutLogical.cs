using ILNumerics.Core.StorageLayer;
using ILNumerics.Core.Arrays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILNumerics {

    /// <summary>
    /// Input /output logical array type to be used as parameter in user defined functions with multiple return values. 
    /// </summary>
    public sealed class OutLogical
        
        : Mutable<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage> {

        #region construction
        internal OutLogical(
            LogicalStorage storage, object synch) : base(storage, synch) { }

        #endregion

        #region conversions, cast operators

        #region constructional operators 

        // nothing here on purpose!    
        #endregion 

        #region conversional operators

        /// <summary>
        /// Converts local array to an in/output array (proxy).
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <returns>Input array.</returns>
        public static implicit operator OutLogical(Logical A) {
            if (object.Equals(A, null))
                return null;
            return A.Storage.OutArray;
        }

        #endregion

        #region conversion to system boolean
        /// <summary>
        /// Convert scalar <see cref="Logical"/> array to <see cref="System.Boolean"/>.
        /// </summary>
        /// <param name="A">Source logical array. Must be scalar (i.e.: must have exactly one element).</param>
        /// <remarks><para>Empty arrays or null is also accepted for <paramref name="A"/>. <see langword="false"/> is returned in this case.</para></remarks>
        /// <exception cref="InvalidCastException"> if <paramref name="A"/> has more than one element.</exception>
        public static implicit operator bool(OutLogical A) {
            if (object.Equals(A, null) || A.IsEmpty) {
                return false;
            }
            if (A.S.NumberOfElements > 1) {
                throw new InvalidCastException($"Only scalar logical arrays can be implicitly casted to System.Boolean. The incoming Logical has {A.S.NumberOfElements} elements. Bool conversion is ambiguous.");
            }
            return A.GetValue(0);
        }
        #endregion

        #endregion

        /// <summary>
        /// Gives the number of elements evaluating to true.
        /// </summary>
        public long NumberTrues {
            get {
                return m_storage.NumberTrues;
            }
        }
        
    }
}
