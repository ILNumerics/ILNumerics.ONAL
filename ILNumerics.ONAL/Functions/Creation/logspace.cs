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
using System.Collections.Generic;
using System.Text;
using ILNumerics;
using static ILNumerics.Globals;

namespace ILNumerics.Core.Functions.Builtin {
    internal static partial class MathInternal {
        /// <summary>
        /// Create logarithmically spaced row vector of 50 elements. 
        /// </summary>
        /// <param name="start">First exponent value.</param>
        /// <param name="end">Last exponent value.</param>
        /// <returns>Row vector with 50 elements logarithmically spaced between 10<sup>start</sup> and 10<sup>end</sup>.</returns>
        /// <remarks><para>If <paramref name="end"/> equals <see cref="Globals.pi"/> than the upper interval for the 
        /// values returned is π and the range is: [10^start...π]</para>
        /// </remarks>
        internal static Array<double> logspace (InArray<double> start, InArray<double> end) {
            return logspace(start, end, 50); 
        }

        /// <summary>
        /// Create logarithmically spaced row vector.
        /// </summary>
        /// <param name="start">First exponent value.</param>
        /// <param name="end">Last exponent value.</param>
        /// <param name="length">Number of elements to create.</param>
        /// <returns>Row vector with 'length' elements logarithmically spaced between 10<sup>start</sup> and 10<sup>end</sup>.</returns>
        /// <remarks><para>If <paramref name="end"/> equals <see cref="Globals.pi"/> than the upper interval for the 
        /// values returned is π and the range is: [10^start...π].</para>
        /// <para>If <paramref name="length"/> is 1 than a single value of 10<sup>end</sup> is returned.</para>
        /// </remarks>
        internal static Array<double> logspace (InArray<double> start, InArray<double> end, InArray<double> length) {
            using (Scope.Enter()) {
                Array<double> start_ = start, end_ = end, length_ = length;
                if (end_ == Globals.pi)
                    end_ = Math.Log10(pi);
                if (length_ < 1) {
                    return zeros<double>(0, dim1: 1);
                //} else if (length < 2) {   // linspace does just that
                //    return array<double>(Math.Pow(10.0, (double)end), vector<long>(1, 1));  
                } else {
                    //Array<double> fact = (end - start) / (length - 1);
                    return pow(10.0, linspace<double>(start_, end_, length_));
                }
            }
        }    
    }
}
