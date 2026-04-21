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
using static ILNumerics.ILMath; 

namespace ILNumerics {

    public partial class ILMath {

        /// <summary>
        /// Computes the [m x n] squared pairwise L2 distances between data points provided in <paramref name="A"/> [d x m] and <paramref name="B"/> [d x n].
        /// </summary>
        /// <param name="A">Input data matrix <paramref name="A"/> [d x m], m points are provided as columns of length d.</param>
        /// <param name="B">Input data matrix <paramref name="B"/> [d x n], n points are provided as columns of length d.</param>
        /// <returns>Distance matrix D of size [m x n]. D<sub>i,j</sub> is the <i>squared</i> euclidian distance between data point <i>i</i> in A and data point <i>j</i> in B.</returns>
        /// <remarks><para>This function computes the square of the pairwise euclidian (L2) distances between data values in <paramref name="A"/> and <paramref name="B"/>.</para>
        /// <para>In order to get the non-squared distances one may use the <see cref="sqrt(BaseArray{double})"/> function on the array returned.</para>
        /// <para>For those coming from languages like Matlab / Octave: the equivalent function there would be: <c>bsxfun(@plus, dot(X, X, 1)',dot(Y,Y,1))-2*(X' * Y)</c> </para></remarks>
        public static Array<double> distL2sq(InArray<double> A, InArray<double> B) {
            using (Scope.Enter(A,B)) {

                if (isnull(A) || isnull(B)) {
                    throw new ArgumentException("Input arguments A and B must be matrices."); 
                }
                Array<double> _A = A, _B = B;
                if (_A.S[0] != _B.S[0]) {
                    throw new ArgumentException("Input _A and _B must have the same number of rows (dimensionality of data points).");
                }

                // the matlab equivalent would be: 
                // bsxfun(@plus, dot(X, X, 1)',dot(Y,Y,1))-2*(X' * Y);
                if (object.Equals(_A, _B)) {
                    Array<double> AsqrtSum = sum(_A * _A, 0);
                    return abs((AsqrtSum.T + AsqrtSum) - 2 * multiply(_A.T, _A)); // abs required for stability: -0.0000 -> 0
                }
                return (sum(_A * _A, 0).T + sum(_B * _B, 0)) - 2 * multiply(_A.T, _B);
            }
        }

        /// <summary>
        /// Computes the [m x n] squared pairwise L2 distances between data points provided in <paramref name="A"/> [d x m] and <paramref name="B"/> [d x n].
        /// </summary>
        /// <param name="A">Input data matrix <paramref name="A"/> [d x m], m points are provided as columns of length d.</param>
        /// <param name="B">Input data matrix <paramref name="B"/> [d x n], n points are provided as columns of length d.</param>
        /// <returns>Distance matrix D of size [m x n]. D<sub>i,j</sub> is the <i>squared</i> euclidian distance between data point <i>i</i> in A and data point <i>j</i> in B.</returns>
        /// <remarks><para>This function computes the square of the pairwise euclidian (L2) distances between data values in <paramref name="A"/> and <paramref name="B"/>.</para>
        /// <para>In order to get the non-squared distances one may use the <see cref="sqrt(BaseArray{double})"/> function on the array returned.</para>
        /// <para>For those coming from languages like Matlab / Octave: the equivalent function there would be: <c>bsxfun(@plus, dot(X, X, 1)',dot(Y,Y,1))-2*(X' * Y)</c> </para></remarks>
        public static Array<float> distL2sq(InArray<float> A, InArray<float> B) {
            using (Scope.Enter(A, B)) {

                if (isnull(A) || isnull(B)) {
                    throw new ArgumentException("Input arguments A and B must be matrices.");
                }
                Array<float> _A = A, _B = B;
                if (_A.S[0] != _B.S[0]) {
                    throw new ArgumentException("Input _A and _B must have the same number of rows (dimensionality of data points).");
                }

                // the matlab equivalent would be: 
                // bsxfun(@plus, dot(X, X, 1)',dot(Y,Y,1))-2*(X' * Y);
                if (object.Equals(_A, _B)) {
                    Array<float> AsqrtSum = sum(_A * _A, 0);
                    return abs((AsqrtSum.T + AsqrtSum) - 2 * multiply(_A.T, _A)); // abs required for stability: -0.0000 -> 0
                }
                return (sum(_A * _A, 0).T + sum(_B * _B, 0)) - 2 * multiply(_A.T, _B);
            }
        }
    }
}
