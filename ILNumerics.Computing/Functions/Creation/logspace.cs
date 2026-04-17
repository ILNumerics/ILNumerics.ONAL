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
