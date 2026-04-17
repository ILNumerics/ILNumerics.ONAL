using System;
using System.Collections.Generic;
using System.Text;
using ILNumerics;
using ILNumerics.Core.Misc;
//using static ILNumerics.Core.Functions.Builtin.MathInternal; 

namespace ILNumerics.Core.Functions.Builtin {
    internal static partial class MathInternal {

        /// <summary>
        /// Create linearly spaced row vector of 100 elements 
        /// </summary>
        /// <param name="start">First value</param>
        /// <param name="end">Last value</param>
        /// <returns>Row vector with 100 elements linearly spaced between start and end</returns>
        internal static Array<T> linspace<T>(InArray<T> start, InArray<T> end) where T : struct, IConvertible {
            return linspace<T>(start, end, convert<T>(100.0)); 
        }

        /// <summary>
        /// Create linearly spaced row vector, generic element type.
        /// </summary>
        /// <param name="start">First value, scalar, numeric</param>
        /// <param name="end">Last value, scalar, numeric</param>
        /// <param name="length">Number of elements to create, scalar, numeric</param>
        /// <returns>Row vector with 'length' elements linearly spaced between start and end</returns>
        /// <remarks>
        /// <para><see cref="F:linspace{T}(InArray{T}, InArray{T}, InArray{T})"/> returns an empty array if <paramref name="length"/> is 0.</para>
        /// <para><see cref="linspace{T}(InArray{T}, InArray{T}, InArray{T})"/> returns a scalar array with the value of <paramref name="end"/> if <paramref name="length"/> is 1.</para>
        /// </remarks>
        internal static Array<T> linspace<T>(Array<T> start, Array<T> end, Array<T> length)
            where T : struct, IConvertible {
            using (Scope.Enter(start, end, length)) {
                if (object.Equals(start, null)) throw new ArgumentException("'start' cannot be null.");
                if (object.Equals(end, null)) throw new ArgumentException("'end' cannot be null.");
                if (object.Equals(length, null)) throw new ArgumentException("'length' cannot be null.");
                Array<double> dStart = todouble(start);
                Array<double> dEnd = todouble(end);
                Array<double> dLength = todouble(length);
                if (!dStart.IsScalar || !dStart.IsNumeric) throw new ArgumentException("'start' must be a numeric scalar");
                if (!dEnd.IsScalar || !dEnd.IsNumeric) throw new ArgumentException("'end' must be a numeric scalar");
                if (!dLength.IsScalar || !dLength.IsNumeric) throw new ArgumentException("'length' must be a numeric scalar");

                if (dLength < 1) {
                    return empty<T>(0);
                } else if (dLength < 2) {
                    return convert<double, T>(dEnd);
                } else {
                    Array<double> fact = (dEnd - dStart) / (dLength - 1.0);
                    Array<double> i;
                    // specified: ROW vector!
                    i = counter(0.0, 1.0, 1, (int)dLength.GetValue(0));
                    return convert<double, T>(dStart + i * fact);
                }
            }
        }
        /// <summary>
        /// Create linearly spaced row vector, double precision.
        /// </summary>
        /// <param name="start">The first value.</param>
        /// <param name="end">The last value.</param>
        /// <param name="length">Number of elements to create.</param>
        /// <returns>Vector with 'length' elements linearly spaced between start and end.</returns>
        /// <remarks>
        /// <para><see cref="linspace(InArray{double}, InArray{double}, InArray{double})"/> returns an empty array if <paramref name="length"/> is 0.</para>
        /// <para><see cref="linspace(InArray{double}, InArray{double}, InArray{double})"/> returns a scalar array with the value of <paramref name="end"/> if <paramref name="length"/> is 1.</para>
        /// </remarks>
        internal static Array<double> linspace (InArray<double> start, InArray<double> end, InArray<double> length) {
            return linspace<double>(start,end,length); 
        }    
    }
}
