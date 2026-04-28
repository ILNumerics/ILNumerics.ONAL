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
        /// <remarks>The <see cref="OutCell"/> shares the same storage with <paramref name="A"/>
        /// and serves as a proxy to <paramref name="A"/>. Changing the output type cell in a function
        /// changes the associated local cell <paramref name="A"/> in the calling scope directly.</remarks>
        public static implicit operator OutCell(Cell A) {
            return A.Storage.OutArray;
        }
        #endregion


    }
}
