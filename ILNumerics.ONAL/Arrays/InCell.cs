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
