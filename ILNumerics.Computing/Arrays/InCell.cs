using ILNumerics.Core.StorageLayer;
using ILNumerics.Core.Arrays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics {

    /// <summary>
    /// Input cell array type to be used as input parameter in user defined functions. 
    /// </summary>
    public sealed class InCell
        : Immutable<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> {

        #region construction
        internal InCell(
            CellStorage storage) 
            : base(storage) { }

        #endregion

        #region implicit casts

        #region conversional operators 
        /// <summary>
        /// Convert persistent to input parameter array
        /// </summary>
        /// <param name="array">Persistent array</param>
        /// <returns>Input parameter array, will survive the current scope only</returns>
        public static implicit operator InCell(Cell array) {
            if (object.Equals(array, null))
                return null;
            // we create a clone here in order to not to block the source against modifications
            var sto = array.Storage.Clone() as CellStorage;
            var ret = sto.GetInArray(retain: false);
            return ret;
        }
        /// <summary>
        /// Convert output paramter array to input parameter array
        /// </summary>
        /// <param name="array">Output parameter array</param>
        /// <returns>Input parameter array, will survive the current scope only</returns>
        public static implicit operator InCell(OutCell array) {
            if (object.Equals(array, null))
                return null;
            // we create a clone here in order to not to block the source against modifications
            var sto = array.Storage.Clone() as CellStorage;
            var ret = sto.GetInArray(retain: false);
            return ret;
        }
        #endregion

        #endregion


    }
}
