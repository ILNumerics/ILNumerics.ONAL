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
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.Functions.Builtin {
    internal static partial class MathInternal {

        /// <summary>
        /// Creates an array similar to this array, having singleton dimensions removed.
        /// </summary>
        /// <param name="A">Input array, will not be altered.</param>
        /// <returns>New array without singleton dimension.</returns>
        /// <remarks>This function (as all functions in ILNumerics) respects the setting of <see cref="Settings.MinNumberOfArrayDimensions"/>. 
        /// Thus, commonly, the array returned may still has up to two singleton dimensions, if <see cref="Settings.ArrayStyle"/> is <see cref="ArrayStyles.ILNumericsV4"/>.
        /// <para>Note that removing singleton dimensions does not change the number of elements of the array. Hence, no copy is made 
        /// for <see cref="squeeze{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT})"/>.</para></remarks>
        /// <seealso cref="reshape{T}(BaseArray{T}, long, long, long, long, StorageOrders?)"/>
        internal static RetT squeeze<T, LocalT, InT, OutT, RetT, StorageT>(ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            using var _1 = ReaderLock.Create(A, out var storage); 
 
            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            StorageT ret = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Create(storage.Handles, storage.Size);

            for (uint i = ret.Size.NumberOfDimensions; i-- > 0;) {
                if (ret.Size[i] == 1) {
                    ret.Size.RemoveDimension(i);
                }
            }
            return ret.RetArray;
        }
    }
}
