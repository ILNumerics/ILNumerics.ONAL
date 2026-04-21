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

/*!HC:TYPELIST:
<hycalper>
    <type>
        <source locate="here">
            double
        </source>
        <destination>complex</destination>
        <destination>fcomplex</destination>
        <destination>float</destination>
        <destination>Int32</destination>
        <destination>Int64</destination>
    </type>
    <type>
        <source locate="here">
            Double
        </source>
        <destination>double</destination>
        <destination>float</destination>
        <destination>float</destination>
        <destination>Int32</destination>
        <destination>Int64</destination>
    </type>
 </hycalper>
 */

namespace ILNumerics.Core.Functions.Builtin {

    internal partial class MathInternal {

        #region HYCALPER LOOPSTART 
        /// <summary>
        /// Pairwise L1 distance. Aka: Manhattan distance. 
        /// </summary>
        /// <param name="A">Input points (matrix).</param>
        /// <param name="B">Input point (vector).</param>
        /// <returns>pairwise L1 distances between the data point provided in the input vector <paramref name="B"/> and the data points stored in the matrix <paramref name="A"/>.</returns>
        /// <remarks>
        /// <para>If <paramref name="B"/> is a colum vector, the distances between <paramref name="B"/> and the columns of <paramref name="A"/> are calculated. The number of rows of <paramref name="A"/> 
        /// must match the length of vector <paramref name="B"/> than. Therefore, the length of the returned vector of distances matches the number of columns of <paramref name="A"/>: <code>A.S[1]</code>.</para>
        /// <para>If <paramref name="B"/> is a row vector, the distances between <paramref name="B"/> and the rows of <paramref name="A"/> are calculated. The number of columns of <paramref name="A"/> 
        /// must match the length of vector <paramref name="B"/> than. Therefore, the length of the returned vector of distances matches the number of rows of <paramref name="A"/>: <code>A.S[0]</code>.</para>
        /// <para>This function is commutative, the single data point may be provided in <paramref name="A"/> and the data point matrix may be provided in <paramref name="B"/> as well.</para>
        /// </remarks>
        internal static unsafe Array<Double> distL1(InArray<double> A, InArray<double> B) {
            using (Scope.Enter()) {

                Array<double> A_ = A, B_ = B; 
                #region parameter checking
                if (isnull(A_) || isnull(B_))
                    throw new ArgumentException($"Input arguments A and B must not be null! Found: A={A_?.S.ToString()}, B={B_?.S.ToString()}.");
                // early exit: make the function cummutative
                if (A_.IsVector && !B_.IsVector) {
                    return distL1(B_, A_); 
                }
                if (!B_.IsVector) {
                    throw new ArgumentException($"One of the inputs must be a vector! Found: A={A_.S.ToString()}, B={B_.S.ToString()}.");
                }
                Array<Double> ret = sum(abs(A_ - B_), B_.IsColumnVector ? 0 : 1);
                return ret; 
                #endregion
            }
        }
#endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
        /// <summary>
        /// Pairwise L1 distance. Aka: Manhattan distance. 
        /// </summary>
        /// <param name="A">Input points (matrix).</param>
        /// <param name="B">Input point (vector).</param>
        /// <returns>pairwise L1 distances between the data point provided in the input vector <paramref name="B"/> and the data points stored in the matrix <paramref name="A"/>.</returns>
        /// <remarks>
        /// <para>If <paramref name="B"/> is a colum vector, the distances between <paramref name="B"/> and the columns of <paramref name="A"/> are calculated. The number of rows of <paramref name="A"/> 
        /// must match the length of vector <paramref name="B"/> than. Therefore, the length of the returned vector of distances matches the number of columns of <paramref name="A"/>: <code>A.S[1]</code>.</para>
        /// <para>If <paramref name="B"/> is a row vector, the distances between <paramref name="B"/> and the rows of <paramref name="A"/> are calculated. The number of columns of <paramref name="A"/> 
        /// must match the length of vector <paramref name="B"/> than. Therefore, the length of the returned vector of distances matches the number of rows of <paramref name="A"/>: <code>A.S[0]</code>.</para>
        /// <para>This function is commutative, the single data point may be provided in <paramref name="A"/> and the data point matrix may be provided in <paramref name="B"/> as well.</para>
        /// </remarks>
        internal static unsafe Array<Int64> distL1(InArray<Int64> A, InArray<Int64> B) {
            using (Scope.Enter()) {

                Array<Int64> A_ = A, B_ = B; 
                #region parameter checking
                if (isnull(A_) || isnull(B_))
                    throw new ArgumentException($"Input arguments A and B must not be null! Found: A={A_?.S.ToString()}, B={B_?.S.ToString()}.");
                // early exit: make the function cummutative
                if (A_.IsVector && !B_.IsVector) {
                    return distL1(B_, A_); 
                }
                if (!B_.IsVector) {
                    throw new ArgumentException($"One of the inputs must be a vector! Found: A={A_.S.ToString()}, B={B_.S.ToString()}.");
                }
                Array<Int64> ret = sum(abs(A_ - B_), B_.IsColumnVector ? 0 : 1);
                return ret; 
                #endregion
            }
        }
        /// <summary>
        /// Pairwise L1 distance. Aka: Manhattan distance. 
        /// </summary>
        /// <param name="A">Input points (matrix).</param>
        /// <param name="B">Input point (vector).</param>
        /// <returns>pairwise L1 distances between the data point provided in the input vector <paramref name="B"/> and the data points stored in the matrix <paramref name="A"/>.</returns>
        /// <remarks>
        /// <para>If <paramref name="B"/> is a colum vector, the distances between <paramref name="B"/> and the columns of <paramref name="A"/> are calculated. The number of rows of <paramref name="A"/> 
        /// must match the length of vector <paramref name="B"/> than. Therefore, the length of the returned vector of distances matches the number of columns of <paramref name="A"/>: <code>A.S[1]</code>.</para>
        /// <para>If <paramref name="B"/> is a row vector, the distances between <paramref name="B"/> and the rows of <paramref name="A"/> are calculated. The number of columns of <paramref name="A"/> 
        /// must match the length of vector <paramref name="B"/> than. Therefore, the length of the returned vector of distances matches the number of rows of <paramref name="A"/>: <code>A.S[0]</code>.</para>
        /// <para>This function is commutative, the single data point may be provided in <paramref name="A"/> and the data point matrix may be provided in <paramref name="B"/> as well.</para>
        /// </remarks>
        internal static unsafe Array<Int32> distL1(InArray<Int32> A, InArray<Int32> B) {
            using (Scope.Enter()) {

                Array<Int32> A_ = A, B_ = B; 
                #region parameter checking
                if (isnull(A_) || isnull(B_))
                    throw new ArgumentException($"Input arguments A and B must not be null! Found: A={A_?.S.ToString()}, B={B_?.S.ToString()}.");
                // early exit: make the function cummutative
                if (A_.IsVector && !B_.IsVector) {
                    return distL1(B_, A_); 
                }
                if (!B_.IsVector) {
                    throw new ArgumentException($"One of the inputs must be a vector! Found: A={A_.S.ToString()}, B={B_.S.ToString()}.");
                }
                Array<Int32> ret = sum(abs(A_ - B_), B_.IsColumnVector ? 0 : 1);
                return ret; 
                #endregion
            }
        }
        /// <summary>
        /// Pairwise L1 distance. Aka: Manhattan distance. 
        /// </summary>
        /// <param name="A">Input points (matrix).</param>
        /// <param name="B">Input point (vector).</param>
        /// <returns>pairwise L1 distances between the data point provided in the input vector <paramref name="B"/> and the data points stored in the matrix <paramref name="A"/>.</returns>
        /// <remarks>
        /// <para>If <paramref name="B"/> is a colum vector, the distances between <paramref name="B"/> and the columns of <paramref name="A"/> are calculated. The number of rows of <paramref name="A"/> 
        /// must match the length of vector <paramref name="B"/> than. Therefore, the length of the returned vector of distances matches the number of columns of <paramref name="A"/>: <code>A.S[1]</code>.</para>
        /// <para>If <paramref name="B"/> is a row vector, the distances between <paramref name="B"/> and the rows of <paramref name="A"/> are calculated. The number of columns of <paramref name="A"/> 
        /// must match the length of vector <paramref name="B"/> than. Therefore, the length of the returned vector of distances matches the number of rows of <paramref name="A"/>: <code>A.S[0]</code>.</para>
        /// <para>This function is commutative, the single data point may be provided in <paramref name="A"/> and the data point matrix may be provided in <paramref name="B"/> as well.</para>
        /// </remarks>
        internal static unsafe Array<float> distL1(InArray<float> A, InArray<float> B) {
            using (Scope.Enter()) {

                Array<float> A_ = A, B_ = B; 
                #region parameter checking
                if (isnull(A_) || isnull(B_))
                    throw new ArgumentException($"Input arguments A and B must not be null! Found: A={A_?.S.ToString()}, B={B_?.S.ToString()}.");
                // early exit: make the function cummutative
                if (A_.IsVector && !B_.IsVector) {
                    return distL1(B_, A_); 
                }
                if (!B_.IsVector) {
                    throw new ArgumentException($"One of the inputs must be a vector! Found: A={A_.S.ToString()}, B={B_.S.ToString()}.");
                }
                Array<float> ret = sum(abs(A_ - B_), B_.IsColumnVector ? 0 : 1);
                return ret; 
                #endregion
            }
        }
        /// <summary>
        /// Pairwise L1 distance. Aka: Manhattan distance. 
        /// </summary>
        /// <param name="A">Input points (matrix).</param>
        /// <param name="B">Input point (vector).</param>
        /// <returns>pairwise L1 distances between the data point provided in the input vector <paramref name="B"/> and the data points stored in the matrix <paramref name="A"/>.</returns>
        /// <remarks>
        /// <para>If <paramref name="B"/> is a colum vector, the distances between <paramref name="B"/> and the columns of <paramref name="A"/> are calculated. The number of rows of <paramref name="A"/> 
        /// must match the length of vector <paramref name="B"/> than. Therefore, the length of the returned vector of distances matches the number of columns of <paramref name="A"/>: <code>A.S[1]</code>.</para>
        /// <para>If <paramref name="B"/> is a row vector, the distances between <paramref name="B"/> and the rows of <paramref name="A"/> are calculated. The number of columns of <paramref name="A"/> 
        /// must match the length of vector <paramref name="B"/> than. Therefore, the length of the returned vector of distances matches the number of rows of <paramref name="A"/>: <code>A.S[0]</code>.</para>
        /// <para>This function is commutative, the single data point may be provided in <paramref name="A"/> and the data point matrix may be provided in <paramref name="B"/> as well.</para>
        /// </remarks>
        internal static unsafe Array<float> distL1(InArray<fcomplex> A, InArray<fcomplex> B) {
            using (Scope.Enter()) {

                Array<fcomplex> A_ = A, B_ = B; 
                #region parameter checking
                if (isnull(A_) || isnull(B_))
                    throw new ArgumentException($"Input arguments A and B must not be null! Found: A={A_?.S.ToString()}, B={B_?.S.ToString()}.");
                // early exit: make the function cummutative
                if (A_.IsVector && !B_.IsVector) {
                    return distL1(B_, A_); 
                }
                if (!B_.IsVector) {
                    throw new ArgumentException($"One of the inputs must be a vector! Found: A={A_.S.ToString()}, B={B_.S.ToString()}.");
                }
                Array<float> ret = sum(abs(A_ - B_), B_.IsColumnVector ? 0 : 1);
                return ret; 
                #endregion
            }
        }
        /// <summary>
        /// Pairwise L1 distance. Aka: Manhattan distance. 
        /// </summary>
        /// <param name="A">Input points (matrix).</param>
        /// <param name="B">Input point (vector).</param>
        /// <returns>pairwise L1 distances between the data point provided in the input vector <paramref name="B"/> and the data points stored in the matrix <paramref name="A"/>.</returns>
        /// <remarks>
        /// <para>If <paramref name="B"/> is a colum vector, the distances between <paramref name="B"/> and the columns of <paramref name="A"/> are calculated. The number of rows of <paramref name="A"/> 
        /// must match the length of vector <paramref name="B"/> than. Therefore, the length of the returned vector of distances matches the number of columns of <paramref name="A"/>: <code>A.S[1]</code>.</para>
        /// <para>If <paramref name="B"/> is a row vector, the distances between <paramref name="B"/> and the rows of <paramref name="A"/> are calculated. The number of columns of <paramref name="A"/> 
        /// must match the length of vector <paramref name="B"/> than. Therefore, the length of the returned vector of distances matches the number of rows of <paramref name="A"/>: <code>A.S[0]</code>.</para>
        /// <para>This function is commutative, the single data point may be provided in <paramref name="A"/> and the data point matrix may be provided in <paramref name="B"/> as well.</para>
        /// </remarks>
        internal static unsafe Array<double> distL1(InArray<complex> A, InArray<complex> B) {
            using (Scope.Enter()) {

                Array<complex> A_ = A, B_ = B; 
                #region parameter checking
                if (isnull(A_) || isnull(B_))
                    throw new ArgumentException($"Input arguments A and B must not be null! Found: A={A_?.S.ToString()}, B={B_?.S.ToString()}.");
                // early exit: make the function cummutative
                if (A_.IsVector && !B_.IsVector) {
                    return distL1(B_, A_); 
                }
                if (!B_.IsVector) {
                    throw new ArgumentException($"One of the inputs must be a vector! Found: A={A_.S.ToString()}, B={B_.S.ToString()}.");
                }
                Array<double> ret = sum(abs(A_ - B_), B_.IsColumnVector ? 0 : 1);
                return ret; 
                #endregion
            }
        }

#endregion HYCALPER AUTO GENERATED CODE

    }
}