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
using ILNumerics.Core.Arrays;
using ILNumerics.Core.Global;
using ILNumerics.Core.MemoryLayer;
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics {

    public static partial class ExtensionMethods {

        #region generic Concat extensions

        /// <summary>
        /// Create new array with the elements of this array and <paramref name="B"/>, concatenated along dimension <paramref name="dim"/>.
        /// </summary>
        /// <param name="A">This array. </param>
        /// <param name="B">The other array. </param>
        /// <param name="dim">Dimension index to align both storages along.</param>
        /// <returns>New array with elements of this array and elements of <paramref name="B"/>.</returns>
        /// <remarks><para>The size of both arrays must match. This means that all but the dimension #<paramref name="dim"/>
        /// must have the same lengths.</para></remarks>
        /// <exception cref="ArgumentException">If <paramref name="dim"/> is not a valid dimension index.</exception>
        
        public unsafe static RetT Concat<T, LocalT, InT, OutT, RetT, StorageT>(
                        this ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A,
                        ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> B, uint dim)
                where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
                where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
                where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            using var _1 = ReaderLock.Create(A, out var storageA);
            using var _2 = ReaderLock.Create(B, out var storageB);
            var ret = storageA.Concat(storageB, dim); 
            return ret.RetArray;
        }

        #endregion
    }
}
