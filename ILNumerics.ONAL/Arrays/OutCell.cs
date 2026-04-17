using ILNumerics.Core.StorageLayer;
using ILNumerics.Core.Arrays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics {

    /// <summary>
    /// Input /output cell array type to be used as parameter in user defined functions with multiple return values. 
    /// </summary>
    public class OutCell
        : Mutable<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> {

        #region construction
        internal OutCell(CellStorage storage, object synch) : base(storage, synch) { }

        #endregion

        #region implicit casts
        /// <summary>
        /// Convert local array to output type array. 
        /// </summary>
        /// <param name="A">Local cell to be used as output argument.</param>
        /// <remarks><para>Just like for <see cref="Array{T}"/> to <see cref="RetArray{T1}"/>
        /// conversions the <see cref="OutCell"/> shares the same storage with <paramref name="A"/>
        /// and serves as a proxy to <paramref name="A"/>. Changing the output type cell in a function
        /// changes the associated local cell <paramref name="A"/> in the calling scope.</para></remarks>
        public static implicit operator OutCell(Cell A) {
            return A.Storage.OutArray;
        }
        #endregion


    }
}
