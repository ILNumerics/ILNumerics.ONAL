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
using static ILNumerics.Core.Functions.Builtin.numpyInternal;
using static ILNumerics.ILMath;

/*!HC:TYPELIST:
<hycalper>
    <type>
        <source locate="here">
            double
        </source>
        <destination>float</destination>
    </type>
 </hycalper>
 */
namespace ILNumerics {

    public static partial class ILMath {

        #region HYCALPER LOOPSTART 

        /// <summary>
        /// Computes the cross product along a dimension of two arrays.
        /// </summary>
        /// <param name="A">First array.</param>
        /// <param name="B">Second array.</param>
        /// <param name="normalize">[Optional] If true: normalize resulting vectors to length 1.0. Default: false.</param>
        /// <param name="dimension">[Optional] The working dimension. Default: (null) the first dimension of length 3.</param>
        /// <returns>Array with cross products of vectors from <paramref name="A"/> and <paramref name="B"/>.</returns>
        /// <remarks><para><paramref name="A"/> and <paramref name="B"/> are expected to store the vectors inside the same respective dimensions. 
        /// This dimension must be of length 3 or longer. If <paramref name="dimension"/> is not specified the working dimension is found by 
        /// searching for the first dimension of length 3, starting with dimension #0. If no dimension of length 3 is found, <see cref="cross(InArray{double}, InArray{double}, bool, uint?)"/>
        /// uses the first dimension which has more than 3 elements. If no such dimension is found an exception is thrown.</para>
        /// <para>The <paramref name="dimension"/> argument allows to explicitly determine the index of the working dimension. The length of 
        /// this dimension must be equal to or larger than 3. Both, <paramref name="A"/> and <paramref name="B"/> must be of the same size.</para>
        /// <para>Elements in the working dimension with indices > #2 are ignored. Corresponding elements in the output array become 0.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If the size of <paramref name="A"/> and <paramref name="B"/> is not the same or 
        /// if either of <paramref name="A"/> or <paramref name="B"/> is null, if the working dimension could not be determined or if the determined 
        /// workign dimension is not at least of length 3.</exception>
        public static Array<double> cross(InArray<double> A, 
                                            InArray<double> B,
                                            bool normalize = false, 
                                            uint? dimension = null) {
            using (Scope.Enter()) {
                Array<double> _A = A;
                Array<double> _B = B;

                if (isnull(_A) || isnull(_B)) {
                    throw new ArgumentException($"A and B may not be null.");
                }
                if (!_A.Size.IsSameSize(_B.Size)) {
                    throw new ArgumentException("A and B must have the same size!");
                }
                if (!dimension.HasValue) {
                    Array<int> dim = find32(_A.shape == 3);
                    if (dim.IsEmpty) {
                        dim.a = find32(_A.shape > 3);
                        if (dim.IsEmpty) {
                            throw new ArgumentException($"Cross requires the input arrays to have at least one dimension of length >= 3. Found: {_A.S.ToString()}.");
                        }
                    }
                    dimension = (uint)dim.GetValue(0); 
                } else {
                    if (dimension.GetValueOrDefault() >= _A.S.NumberOfDimensions) {
                        throw new ArgumentException($"The dimension argument must be null or equal to the index of an existing, non-virtual dimension. Found: {dimension}.");
                    }
                }
                if (_A.S[dimension.GetValueOrDefault()] < 3) {
                    throw new ArgumentException($"Cross requires the input parameters to have at least 3 elements in the working dimension. Found: {_A.S.ToString()}, working dimension: #{dimension}."); 
                }
                if (dimension.GetValueOrDefault() > 0) {
                    Array<double> swapped = cross(
                        _A.Storage.Swapaxis(0,dimension.GetValueOrDefault(),false).RetArray,
                        _B.Storage.Swapaxis(0, dimension.GetValueOrDefault(), false).RetArray, 
                        normalize);
                    return swapped.Storage.Swapaxis(0, dimension.GetValueOrDefault(), true).RetArray; 
                }
                Array<double> ret = _A.Size[0] == 3 ? empty<double>(_A.Size) : zeros<double>(_A.Size);
                using (Scope.Enter(arrayStyle: ArrayStyles.ILNumericsV4)) {
                    Array<double> Ax = _A[0, full];
                    Array<double> Ay = _A[1, full];
                    Array<double> Az = _A[2, full];
                    Array<double> Bx = _B[0, full];
                    Array<double> By = _B[1, full];
                    Array<double> Bz = _B[2, full];
                    ret[0, full] = Ay * Bz - Az * By;
                    ret[1, full] = Az * Bx - Ax * Bz;
                    ret[2, full] = Ax * By - Ay * Bx;
                }
                if (!normalize) {
                    return ret;
                } else {
                    return ret / ILMath.sqrt(ILMath.sum(ret * ret, 0));
                }
            }
        }
        #endregion HYCALPER LOOPEND 
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 

        /// <summary>
        /// Computes the cross product along a dimension of two arrays.
        /// </summary>
        /// <param name="A">First array.</param>
        /// <param name="B">Second array.</param>
        /// <param name="normalize">[Optional] If true: normalize resulting vectors to length 1.0. Default: false.</param>
        /// <param name="dimension">[Optional] The working dimension. Default: (null) the first dimension of length 3.</param>
        /// <returns>Array with cross products of vectors from <paramref name="A"/> and <paramref name="B"/>.</returns>
        /// <remarks><para><paramref name="A"/> and <paramref name="B"/> are expected to store the vectors inside the same respective dimensions. 
        /// This dimension must be of length 3 or longer. If <paramref name="dimension"/> is not specified the working dimension is found by 
        /// searching for the first dimension of length 3, starting with dimension #0. If no dimension of length 3 is found, <see cref="cross(InArray{float}, InArray{float}, bool, uint?)"/>
        /// uses the first dimension which has more than 3 elements. If no such dimension is found an exception is thrown.</para>
        /// <para>The <paramref name="dimension"/> argument allows to explicitly determine the index of the working dimension. The length of 
        /// this dimension must be equal to or larger than 3. Both, <paramref name="A"/> and <paramref name="B"/> must be of the same size.</para>
        /// <para>Elements in the working dimension with indices > #2 are ignored. Corresponding elements in the output array become 0.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If the size of <paramref name="A"/> and <paramref name="B"/> is not the same or 
        /// if either of <paramref name="A"/> or <paramref name="B"/> is null, if the working dimension could not be determined or if the determined 
        /// workign dimension is not at least of length 3.</exception>
        public static Array<float> cross(InArray<float> A, 
                                            InArray<float> B,
                                            bool normalize = false, 
                                            uint? dimension = null) {
            using (Scope.Enter()) {
                Array<float> _A = A;
                Array<float> _B = B;

                if (isnull(_A) || isnull(_B)) {
                    throw new ArgumentException($"A and B may not be null.");
                }
                if (!_A.Size.IsSameSize(_B.Size)) {
                    throw new ArgumentException("A and B must have the same size!");
                }
                if (!dimension.HasValue) {
                    Array<int> dim = find32(_A.shape == 3);
                    if (dim.IsEmpty) {
                        dim.a = find32(_A.shape > 3);
                        if (dim.IsEmpty) {
                            throw new ArgumentException($"Cross requires the input arrays to have at least one dimension of length >= 3. Found: {_A.S.ToString()}.");
                        }
                    }
                    dimension = (uint)dim.GetValue(0); 
                } else {
                    if (dimension.GetValueOrDefault() >= _A.S.NumberOfDimensions) {
                        throw new ArgumentException($"The dimension argument must be null or equal to the index of an existing, non-virtual dimension. Found: {dimension}.");
                    }
                }
                if (_A.S[dimension.GetValueOrDefault()] < 3) {
                    throw new ArgumentException($"Cross requires the input parameters to have at least 3 elements in the working dimension. Found: {_A.S.ToString()}, working dimension: #{dimension}."); 
                }
                if (dimension.GetValueOrDefault() > 0) {
                    Array<float> swapped = cross(
                        _A.Storage.Swapaxis(0,dimension.GetValueOrDefault(),false).RetArray,
                        _B.Storage.Swapaxis(0, dimension.GetValueOrDefault(), false).RetArray, 
                        normalize);
                    return swapped.Storage.Swapaxis(0, dimension.GetValueOrDefault(), true).RetArray; 
                }
                Array<float> ret = _A.Size[0] == 3 ? empty<float>(_A.Size) : zeros<float>(_A.Size);
                using (Scope.Enter(arrayStyle: ArrayStyles.ILNumericsV4)) {
                    Array<float> Ax = _A[0, full];
                    Array<float> Ay = _A[1, full];
                    Array<float> Az = _A[2, full];
                    Array<float> Bx = _B[0, full];
                    Array<float> By = _B[1, full];
                    Array<float> Bz = _B[2, full];
                    ret[0, full] = Ay * Bz - Az * By;
                    ret[1, full] = Az * Bx - Ax * Bz;
                    ret[2, full] = Ax * By - Ay * Bx;
                }
                if (!normalize) {
                    return ret;
                } else {
                    return ret / ILMath.sqrt(ILMath.sum(ret * ret, 0));
                }
            }
        }

#endregion HYCALPER AUTO GENERATED CODE

    }
}
