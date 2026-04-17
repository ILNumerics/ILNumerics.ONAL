using System;
using ILNumerics;
using static ILNumerics.Globals;

namespace ILNumerics.Core.Functions.Builtin {

    internal partial class MathInternal {

        /// <summary>
        /// Polynomial evaluation
        /// </summary>
        /// <param name="c">Vector of coefficients of the polynomial</param>
        /// <param name="x">Position where the polynomial is to be evaluated. The position may be either a scalar, a vector or a matrix.</param>
        /// <example>
        /// <code>
        /// <![CDATA[
        ///  //Evaluation of the polynomial 1+x+2.1 x + x^2+3x^3 at (1,1,1)
        ///  Array<float> y = ILMath.poly(ILMath.array<float>(1.0, 2.1, 1.0, 3.0),ILMath.ones<float>(3, 1));
        /// ]]>
        /// </code>
        /// </example>
        /// <returns>The value of a polynomial of degree d at position x, with d=c.lenght-1</returns>
        /// <remarks> If the input position is an array, the evaluation will be done elementwise</remarks>
        internal static Array<float> poly(InArray<float> c, InArray<float> x) {
            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {
                Array<float> _c = c, _x = x; 
                if (_x.IsEmpty) {
                    return empty<float>(_x.S);
                }
                if (isnull(_x)) {
                    throw new ArgumentNullException("Argument 'x' must not be null.");
                }
                if (_c.IsEmpty) {
                    return empty<float>(_x.S);
                }
                if (isnull(_c)) {
                    throw new ArgumentNullException("Argument 'c' must not be null.");
                }
                Array<float> p = zeros<float>(_x.S);
                for (int item = 0; item < _x.Length; ++item) {
                    long j = _c.Length - 1;
                    p[item] = _c[j];
                    while (j > 0) {
                        p[item] = p[item] * _x[item] + _c[--j];
                    }
                }
                return p;
            }
        }

        /// <summary>
        /// Polynomial evaluation
        /// </summary>
        /// <param name="c">Vector of coefficients of the polynomial</param>
        /// <param name="x">Position where the polynomial is to be evaluated. The position may be either a scalar, a vector or a matrix.</param>
        /// <example>
        /// <code>
        /// <![CDATA[
        ///  //Evaluation of the polynomial 1+x+2.1 x + x^2+3x^3 at (1,1,1)
        ///  Array<double> y = ILMath.poly(ILMath.array<double>(1.0, 2.1, 1.0, 3.0), ILMath.ones<double>(3, 1));
        /// ]]>
        /// </code>
        /// </example>
        /// <returns>The value of a polynomial of degree d at position x, with d=c.lenght-1</returns>
        /// <remarks> If the input position is an array, the evaluation will be done elementwise</remarks>
        internal static Array<double> poly(InArray<double> c, InArray<double> x) {
            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {
                Array<double> _c = c, _x = x;
                if (_x.IsEmpty) {
                    return empty<double>(_x.S);
                }
                if (isnull(_x)) {
                    throw new ArgumentNullException("Argument 'x' must not be null.");
                }
                if (_c.IsEmpty) {
                    return empty<double>(_x.S);
                }
                if (isnull(_c)) {
                    throw new ArgumentNullException("Argument 'c' must not be null.");
                }
                Array<double> p = zeros<double>(_x.S);
                for (int item = 0; item < _x.Length; ++item) {
                    long j = _c.Length - 1;
                    p[item] = _c[j];
                    while (j > 0) {
                        p[item] = p[item] * _x[item] + _c[--j];
                    }
                }
                return p;
            }
        }


    }
}
