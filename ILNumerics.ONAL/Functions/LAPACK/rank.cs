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
//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////
using System;
using ILNumerics;
using static ILNumerics.Globals; 

namespace ILNumerics {
    public static partial class ILMath {

        #region HYCALPER LOOPSTART
        /*!HC:TYPELIST:
        <hycalper>
        <type>
            <source locate="here">
                double
            </source>
            <destination>complex</destination>
            <destination>float</destination>
            <destination>fcomplex</destination>
        </type>
        <type>
            <source locate="here">
                Double
            </source>
            <destination>double</destination>
            <destination>float</destination>
            <destination>float</destination>
        </type>
        <type>
            <source locate="here">
                eps 
            </source>
            <destination>eps</destination>
            <destination>epsf</destination>
            <destination>epsf</destination>
        </type>
        </hycalper>
        */

        /// <summary>
        /// Rank of matrix <paramref name="A"/>:
        /// </summary>
        /// <param name="A">Input matrix:</param>
        /// <param name="tolerance">[Optional] Threshold below which a singular value is considered zero. Default: (-1) auto.</param>
        /// <returns>Rank of matrix <paramref name="A"/>.</returns>
        /// <remarks>The rank is the number of singular values greater than 
        /// tolerance. If tolerance is smaller than 0 (default), the following default value is used: <code>tol = length(A) * norm(A) * eps</code>,
        /// with 
        /// <list type="bullet">
        /// <item>length(A) - the longest dimension of <paramref name="A"/></item>
        /// <item>norm(A) - the largest singular value of <paramref name="A"/>, see: <see cref="svd(InArray{double})"/>,</item>
        /// <item>eps - the distance between 1 and the smallest next greater value.</item>
        /// </list>
        /// </remarks>
        /// <exception cref="ArgumentException"> if <paramref name="A"/> has more than 2 dimensions.</exception>
        public static long rank(InArray<double> A, Double tolerance = -1) {
            using (Scope.Enter()) {

                Array<double> _A = A;

                if (_A.Size.NumberOfDimensions > 2)
                    throw new ArgumentException("The input array must be a matrix or a vector.");
                Array<Double> ret = svd(_A);
                if (ret.S.NumberOfElements < 1) {
                    return 0; 
                }
                if (tolerance < 0) {
                    tolerance = _A.Size.Longest * max(ret).GetValue(0) * eps;
                }
                // count vector elements: ret is vector returned from svd
                return (ret > (Double)tolerance).NumberTrues;

            }
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        /// <summary>
        /// Rank of matrix <paramref name="A"/>:
        /// </summary>
        /// <param name="A">Input matrix:</param>
        /// <param name="tolerance">[Optional] Threshold below which a singular value is considered zero. Default: (-1) auto.</param>
        /// <returns>Rank of matrix <paramref name="A"/>.</returns>
        /// <remarks>The rank is the number of singular values greater than 
        /// tolerance. If tolerance is smaller than 0 (default), the following default value is used: <code>tol = length(A) * norm(A) * epsf</code>,
        /// with 
        /// <list type="bullet">
        /// <item>length(A) - the longest dimension of <paramref name="A"/></item>
        /// <item>norm(A) - the largest singular value of <paramref name="A"/>, see: <see cref="svd(InArray{fcomplex})"/>,</item>
        /// <item>epsf - the distance between 1 and the smallest next greater value.</item>
        /// </list>
        /// </remarks>
        /// <exception cref="ArgumentException"> if <paramref name="A"/> has more than 2 dimensions.</exception>
        public static long rank(InArray<fcomplex> A, float tolerance = -1) {
            using (Scope.Enter()) {

                Array<fcomplex> _A = A;

                if (_A.Size.NumberOfDimensions > 2)
                    throw new ArgumentException("The input array must be a matrix or a vector.");
                Array<float> ret = svd(_A);
                if (ret.S.NumberOfElements < 1) {
                    return 0; 
                }
                if (tolerance < 0) {
                    tolerance = _A.Size.Longest * max(ret).GetValue(0) * epsf;
                }
                // count vector elements: ret is vector returned from svd
                return (ret > (float)tolerance).NumberTrues;

            }
        }
       

        /// <summary>
        /// Rank of matrix <paramref name="A"/>:
        /// </summary>
        /// <param name="A">Input matrix:</param>
        /// <param name="tolerance">[Optional] Threshold below which a singular value is considered zero. Default: (-1) auto.</param>
        /// <returns>Rank of matrix <paramref name="A"/>.</returns>
        /// <remarks>The rank is the number of singular values greater than 
        /// tolerance. If tolerance is smaller than 0 (default), the following default value is used: <code>tol = length(A) * norm(A) * epsf</code>,
        /// with 
        /// <list type="bullet">
        /// <item>length(A) - the longest dimension of <paramref name="A"/></item>
        /// <item>norm(A) - the largest singular value of <paramref name="A"/>, see: <see cref="svd(InArray{float})"/>,</item>
        /// <item>epsf - the distance between 1 and the smallest next greater value.</item>
        /// </list>
        /// </remarks>
        /// <exception cref="ArgumentException"> if <paramref name="A"/> has more than 2 dimensions.</exception>
        public static long rank(InArray<float> A, float tolerance = -1) {
            using (Scope.Enter()) {

                Array<float> _A = A;

                if (_A.Size.NumberOfDimensions > 2)
                    throw new ArgumentException("The input array must be a matrix or a vector.");
                Array<float> ret = svd(_A);
                if (ret.S.NumberOfElements < 1) {
                    return 0; 
                }
                if (tolerance < 0) {
                    tolerance = _A.Size.Longest * max(ret).GetValue(0) * epsf;
                }
                // count vector elements: ret is vector returned from svd
                return (ret > (float)tolerance).NumberTrues;

            }
        }
       

        /// <summary>
        /// Rank of matrix <paramref name="A"/>:
        /// </summary>
        /// <param name="A">Input matrix:</param>
        /// <param name="tolerance">[Optional] Threshold below which a singular value is considered zero. Default: (-1) auto.</param>
        /// <returns>Rank of matrix <paramref name="A"/>.</returns>
        /// <remarks>The rank is the number of singular values greater than 
        /// tolerance. If tolerance is smaller than 0 (default), the following default value is used: <code>tol = length(A) * norm(A) * eps</code>,
        /// with 
        /// <list type="bullet">
        /// <item>length(A) - the longest dimension of <paramref name="A"/></item>
        /// <item>norm(A) - the largest singular value of <paramref name="A"/>, see: <see cref="svd(InArray{complex})"/>,</item>
        /// <item>eps - the distance between 1 and the smallest next greater value.</item>
        /// </list>
        /// </remarks>
        /// <exception cref="ArgumentException"> if <paramref name="A"/> has more than 2 dimensions.</exception>
        public static long rank(InArray<complex> A, double tolerance = -1) {
            using (Scope.Enter()) {

                Array<complex> _A = A;

                if (_A.Size.NumberOfDimensions > 2)
                    throw new ArgumentException("The input array must be a matrix or a vector.");
                Array<double> ret = svd(_A);
                if (ret.S.NumberOfElements < 1) {
                    return 0; 
                }
                if (tolerance < 0) {
                    tolerance = _A.Size.Longest * max(ret).GetValue(0) * eps;
                }
                // count vector elements: ret is vector returned from svd
                return (ret > (double)tolerance).NumberTrues;

            }
        }

#endregion HYCALPER AUTO GENERATED CODE

    }
}
