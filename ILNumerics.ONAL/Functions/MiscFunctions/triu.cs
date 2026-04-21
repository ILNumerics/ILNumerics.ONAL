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
using System;
using ILNumerics;
using static ILNumerics.Globals; 

namespace ILNumerics.Core.Functions.Builtin {
    internal static partial class MathInternal {

        /// <summary>
        /// Extract upper triangular part of matrix <paramref name="A"/>.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="A">Input matrix, size [m x n].</param>
        /// <returns>Array of size [m x n], holding upper triangular part of <paramref name="A"/>.</returns>
        /// <exception cref="ArgumentException">If <paramref name="A"/> has more than 2 dimensions.</exception>
        /// <remarks><para>The diagonal elements of <paramref name="A"/> are included in the upper triangular matrix returned.</para></remarks>
        /// <seealso cref="tril{T}(InArray{T})"/>
        internal static Array<T> triu <T>(InArray<T> A) {
            using (Scope.Enter()) {
                Array<T> _A = A; 
                if (_A.Size.NumberOfDimensions > 2)
                    throw new ArgumentException($"A must be a matrix. Found: [{_A.S.ToString()}].");
                return _A.Storage.copyUpperTriangle(_A.Size[0]);
            }
        }
        /// <summary>
        /// Extract lower triangular part of matrix <paramref name="A"/>.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="A">Input matrix, size [m x n].</param>
        /// <returns>Array of size [m x n], holding lower triangular part of <paramref name="A"/>.</returns>
        /// <exception cref="ArgumentException">If <paramref name="A"/> has more than 2 dimensions.</exception>
        /// <remarks><para>The diagonal elements of <paramref name="A"/> are not included in the lower triangular matrix returned.</para></remarks>
        /// <seealso cref="triu{T}(InArray{T})"/>
        internal static Array<T> tril<T>(InArray<T> A) {
            using (Scope.Enter()) {
                Array<T> _A = A;
                // do some checks for the size of A...
                long n = _A.S[0], m = _A.S[1];
                Array<T> ret = zeros<T>(n,m,StorageOrders.ColumnMajor);

                for (long c = 0; c < m; c++) {
                    for (long r = c + 1; r < n; r++) {
                        ret.SetValue(_A.GetValue(r,c), r, c); 
                    }
                }
                return ret;
            }
        }

    }
}
