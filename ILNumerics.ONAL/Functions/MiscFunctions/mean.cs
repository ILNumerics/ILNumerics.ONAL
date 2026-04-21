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
using System.Collections.Generic;
using System.Text;
using ILNumerics;
using static ILNumerics.Globals; 

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
</hycalper>
*/

namespace ILNumerics.Core.Functions.Builtin  {
	internal partial class MathInternal {

        #region HYCALPER LOOPSTART 
        /// <summary>
        /// Mean of <paramref name="A"/> along dimension <paramref name="dim"/>.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] Index of the dimension to operate along. If <paramref name="dim"/> 
        /// is omitted <see cref="mean(InArray{double}, int)"/> operates along the first non singleton 
        /// dimension.</param>
        /// <returns>Mean of elements along specified or first non singleton dimension.</returns>
        /// <remarks>The return array has the same shape as <paramref name="A"/>, except that the 
        /// working dimension is reduced/expanded to length 1.</remarks>
        internal static Array<double> mean(InArray<double> A, int dim = -1) {
            using (Scope.Enter()) {
                Array<double> _A = A; 
                if (dim < 0)
                    dim = (int)_A.Size.WorkingDimension();
                if (_A.S[(uint)dim] != 0) {
                    return sum(_A, dim) / (double)_A.Size[(uint)dim];
                } else {
                    Array<long> size = _A.shape;
                    size[dim] = 1;
                    Array<double> ret = array<double>(double.NaN, size);
                    return ret;
                }
            }
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
        /// <summary>
        /// Mean of <paramref name="A"/> along dimension <paramref name="dim"/>.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] Index of the dimension to operate along. If <paramref name="dim"/> 
        /// is omitted <see cref="mean(InArray{fcomplex}, int)"/> operates along the first non singleton 
        /// dimension.</param>
        /// <returns>Mean of elements along specified or first non singleton dimension.</returns>
        /// <remarks>The return array has the same shape as <paramref name="A"/>, except that the 
        /// working dimension is reduced/expanded to length 1.</remarks>
        internal static Array<fcomplex> mean(InArray<fcomplex> A, int dim = -1) {
            using (Scope.Enter()) {
                Array<fcomplex> _A = A; 
                if (dim < 0)
                    dim = (int)_A.Size.WorkingDimension();
                if (_A.S[(uint)dim] != 0) {
                    return sum(_A, dim) / (fcomplex)_A.Size[(uint)dim];
                } else {
                    Array<long> size = _A.shape;
                    size[dim] = 1;
                    Array<fcomplex> ret = array<fcomplex>(fcomplex.NaN, size);
                    return ret;
                }
            }
        }
        /// <summary>
        /// Mean of <paramref name="A"/> along dimension <paramref name="dim"/>.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] Index of the dimension to operate along. If <paramref name="dim"/> 
        /// is omitted <see cref="mean(InArray{float}, int)"/> operates along the first non singleton 
        /// dimension.</param>
        /// <returns>Mean of elements along specified or first non singleton dimension.</returns>
        /// <remarks>The return array has the same shape as <paramref name="A"/>, except that the 
        /// working dimension is reduced/expanded to length 1.</remarks>
        internal static Array<float> mean(InArray<float> A, int dim = -1) {
            using (Scope.Enter()) {
                Array<float> _A = A; 
                if (dim < 0)
                    dim = (int)_A.Size.WorkingDimension();
                if (_A.S[(uint)dim] != 0) {
                    return sum(_A, dim) / (float)_A.Size[(uint)dim];
                } else {
                    Array<long> size = _A.shape;
                    size[dim] = 1;
                    Array<float> ret = array<float>(float.NaN, size);
                    return ret;
                }
            }
        }
        /// <summary>
        /// Mean of <paramref name="A"/> along dimension <paramref name="dim"/>.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="dim">[Optional] Index of the dimension to operate along. If <paramref name="dim"/> 
        /// is omitted <see cref="mean(InArray{complex}, int)"/> operates along the first non singleton 
        /// dimension.</param>
        /// <returns>Mean of elements along specified or first non singleton dimension.</returns>
        /// <remarks>The return array has the same shape as <paramref name="A"/>, except that the 
        /// working dimension is reduced/expanded to length 1.</remarks>
        internal static Array<complex> mean(InArray<complex> A, int dim = -1) {
            using (Scope.Enter()) {
                Array<complex> _A = A; 
                if (dim < 0)
                    dim = (int)_A.Size.WorkingDimension();
                if (_A.S[(uint)dim] != 0) {
                    return sum(_A, dim) / (complex)_A.Size[(uint)dim];
                } else {
                    Array<long> size = _A.shape;
                    size[dim] = 1;
                    Array<complex> ret = array<complex>(complex.NaN, size);
                    return ret;
                }
            }
        }

#endregion HYCALPER AUTO GENERATED CODE
   }
}
