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
using ILNumerics.Core.Functions.Builtin;
using ILNumerics.Core.Misc;
using System;

namespace ILNumerics {

    /// <summary>
    /// Static class defining useful constants, shortcuts, aliases and functions for working with ILNumerics arrays. 
    /// </summary>
    public static partial class Globals {

        static Globals() {

            #region initialize machine parameter infos 
            macharD(ref m_machparDouble.ibeta, ref m_machparDouble.it, ref m_machparDouble.irnd, ref m_machparDouble.ngrd, ref m_machparDouble.machep, ref m_machparDouble.negep, ref m_machparDouble.iexp, ref m_machparDouble.minexp, ref m_machparDouble.maxexp, ref m_machparDouble.eps, ref m_machparDouble.epsneg, ref m_machparDouble.xmin, ref m_machparDouble.xmax);
            macharF(ref m_machparSingle.ibeta, ref m_machparSingle.it, ref m_machparSingle.irnd, ref m_machparSingle.ngrd, ref m_machparSingle.machep, ref m_machparSingle.negep, ref m_machparSingle.iexp, ref m_machparSingle.minexp, ref m_machparSingle.maxexp, ref m_machparSingle.eps, ref m_machparSingle.epsneg, ref m_machparSingle.xmin, ref m_machparSingle.xmax);
            #endregion

        }

        /// <summary>
        /// Addresses all unspecified dimension as ':' / 'full'.
        /// </summary>
        public readonly static EllipsisSpec ellipsis = new EllipsisSpec();
        /// <summary>
        /// Insert a new dimension. For <see cref="Settings.ArrayStyle"/>=<see cref="ArrayStyles.numpy"/> only.
        /// </summary>
        private readonly static NewaxisSpec s_newaxis = new NewaxisSpec(); 
        public static NewaxisSpec newaxis {
            get {
                if (ILNumerics.Settings.ArrayStyle != ArrayStyles.numpy) {
                    throw new ArgumentException($"The 'newaxis' dimension specifier is only supported in numpy mode. Current 'Settings.ArrayStyle' = {Settings.ArrayStyle}.");
                }
                return s_newaxis; 
            }
        }
        /// <summary>
        /// Identifier refering to the index of the last element in a dimension. Used in indexing expressions. 
        /// </summary>
        public readonly static EndExpression end = new EndExpression();

        /// <summary>
        /// Addresses the full dimension, from 0 ... end, step size 1.
        /// </summary>
        public static FullDimSpec full {
            get {
                return FullDimSpec.Create();
            }
        }

        #region range interface
        /// <summary>
        /// Range for indexing / subarray operations. ILNumericsV4 / Matlab / Octave / Julia style. 
        /// </summary>
        /// <param name="start">Inclusive start index.</param>
        /// <param name="step">Step size. Spacing between adjacent, selected elements.</param>
        /// <param name="end">Last element (inclusive).</param>
        /// <returns>Stepped range to be used in indexing expressions.</returns>
        public static DimSpec r(uint start, uint step, uint end) {
            var ret = DimSpec.Create();
            ret.Start = start;
            ret.Step = step;
            ret.End = end;
            ret.IsSingleIndex = false;
            ret.IsSlice = false; 
            return ret;
        }
        /// <summary>
        /// Range for indexing / subarray operations. ILNumericsV4 / Matlab / Octave / Julia style. 
        /// </summary>
        /// <param name="start">Inclusive start index.</param>
        /// <param name="end">Last element (inclusive).</param>
        /// <returns>Stepped range to be used in indexing expressions.</returns>
        public static DimSpec r(uint start, uint end) {
            return r(start, 1, end);
        }
        /// <summary>
        /// Range for indexing / subarray operations. ILNumericsV4 / Matlab / Octave / Julia style. 
        /// </summary>
        /// <param name="start">Inclusive start index.</param>
        /// <param name="step">Step size. Spacing between adjacent, selected elements.</param>
        /// <param name="end">Last element (inclusive).</param>
        /// <returns>Stepped range to be used in indexing expressions.</returns>
        public static DimSpec r(long start, long step, long end) {
            var ret = DimSpec.Create();
            ret.Start = start;
            ret.Step = step;
            ret.End = end;
            ret.IsSingleIndex = false;
            ret.IsSlice = false;
            return ret;
        }
        /// <summary>
        /// Range for indexing / subarray operations. ILNumericsV4 / Matlab / Octave / Julia style. 
        /// </summary>
        /// <param name="start">Inclusive start index.</param>
        /// <param name="end">Last element (inclusive).</param>
        /// <returns>Stepped range to be used in indexing expressions.</returns>
        public static DimSpec r(long start, long end) {
            return r(start, 1, end);
        }
        /// <summary>
        /// Range for indexing / subarray operations. ILNumericsV4 / Matlab / Octave / Julia style. Step size: 1.
        /// </summary>
        /// <param name="start">Inclusive start index: scalar, numeric array with the starting index of this range.</param>
        /// <param name="end">Scalar, numeric array with the last index of the selected range (inclusive).</param>
        /// <returns>Stepped range to be used in indexing expressions.</returns>
        public static DimSpec r(BaseArray start, BaseArray end) {
            return r(start, 1, end);
        }
        /// <summary>
        /// Range for indexing / subarray operations. ILNumericsV4 / Matlab / Octave / Julia style.
        /// </summary>
        /// <param name="start">Inclusive start index: scalar, numeric array with the starting index of this range.</param>
        /// <param name="step">Step size between adjacent, selected elements.</param>
        /// <param name="end">Scalar, numeric array with the last index of this range (inclusive).</param>
        /// <returns>Stepped range to be used in indexing expressions.</returns>
        /// <exception cref="ArgumentException"> if <paramref name="start"/> array or <paramref name="end"/> array is not scalar or not numeric or if the conversion to a long value failed.</exception>
        public static DimSpec r(BaseArray start, ulong step, BaseArray end) {
            var ret = DimSpec.Create();
            try {
                using (Scope.Enter()) {
                    Array<long> startA = MathInternal.toint64(start); // release
                    if (!startA.IsScalar) {
                        throw new ArgumentException($"The start for the subarray range must be a numeric scalar.");
                    }
                    ret.Start = startA.GetValue(0);
                }
            } catch (InvalidCastException) {
                throw new ArgumentException($"The start for the subarray range must be a numeric scalar.");
            } catch (IndexOutOfRangeException) {
                throw new ArgumentException($"The start for the subarray range must be a numeric scalar. Found: {start}.");
            }
            try {
                using (Scope.Enter()) {
                    Array<long> endA = MathInternal.toint64(end); // release 
                    if (!endA.IsScalar) {
                        throw new ArgumentException($"The end for the subarray range must be a numeric scalar.");
                    }
                    ret.End = endA.GetValue(0);
                }
            } catch (InvalidCastException) {
                throw new ArgumentException($"The end for the subarray range must be a numeric scalar.");
            } catch (IndexOutOfRangeException) {
                throw new ArgumentException($"The end for the subarray range must be a numeric scalar. Found: {end}.");
            }
            if (step > long.MaxValue) {
                throw new ArgumentException($"The maximum value allowed for step is long.MaxValue ({long.MaxValue}). Found: {step}");
            }
            ret.Step = (long)step;
            ret.IsSingleIndex = false;
            ret.IsSlice = false;
            return ret;
        }
        /// <summary>
        /// Range for indexing / subarray operations. ILNumericsV4 / Matlab / Octave / Julia style.
        /// </summary>
        /// <param name="start">Inclusive start index: scalar, numeric array with the starting index of this range.</param>
        /// <param name="step">Positive step size between adjacent elements. Unit: elements.</param>
        /// <param name="end">Inclusive end index: simple computational expression (+,-,/,*) involving the <see cref="end"/> specifier which evaluates to the last index in this dimension.</param>
        /// <returns>Stepped range to be used in indexing expressions.</returns>
        /// <exception cref="ArgumentException"> if <paramref name="start"/> or <paramref name="end"/> are not scalar or not numeric or if the conversion to a long value failed.</exception>
        /// <exception cref="ArgumentException"> if <paramref name="step"/> is larger than <see cref="long.MaxValue"/>.</exception>
        public static ExpressionDimSpec r(BaseArray start, ulong step, ILExpression end) {
            var ret = ExpressionDimSpec.Create();
            try {
                using (Scope.Enter()) {
                    Array<long> startA = MathInternal.toint64(start); // releases start
                    if (!startA.IsScalar) {
                        throw new ArgumentException($"The start for the subarray range must be a numeric scalar.");
                    }
                    ret.Start = startA.GetValue(0);
                }
            } catch (InvalidCastException) {
                throw new ArgumentException($"The start for the subarray range must be a numeric scalar.");
            } catch (IndexOutOfRangeException) {
                throw new ArgumentException($"The start for the subarray range must be a numeric scalar. Found: {start}.");
            }
            ret.m_endExpression = end;
            if (step > long.MaxValue) {
                throw new ArgumentException($"The maximum value allowed for step is long.MaxValue ({long.MaxValue}). Found: {step}");
            }
            ret.Step = (long)step;
            ret.IsSingleIndex = false;
            ret.IsSlice = false; 
            return ret;
        }

        /// <summary>
        /// Range for indexing / subarray operations. ILNumericsV4 / Matlab / Octave / Julia style.
        /// </summary>
        /// <param name="start">Inclusive start index: simple computational expression (+,-,/,*) involving the <see cref="end"/> specifier which evaluates to the last index in this dimension.</param>
        /// <param name="step">Positive step size between adjacent elements. Unit: elements.</param>
        /// <param name="end">Inclusive end index: simple computational expression (+,-,/,*) involving the <see cref="end"/> specifier which evaluates to the last index in this dimension.</param>
        /// <returns>Stepped range to be used in indexing expressions.</returns>
        /// <exception cref="ArgumentException"> if <paramref name="step"/> is larger than <see cref="long.MaxValue"/>.</exception>
        public static ExpressionDimSpec r(ILExpression start, ulong step, ILExpression end) {
            var ret = ExpressionDimSpec.Create();
            ret.m_startExpression = start;
            ret.m_endExpression = end;
            if (step > long.MaxValue) {
                throw new ArgumentException($"The maximum value allowed for step is long.MaxValue ({long.MaxValue}). Found: {step}");
            }
            ret.Step = (long)step;
            ret.IsSingleIndex = false;
            ret.IsSlice = false; 
            return ret;

        }
        /// <summary>
        /// Range for indexing / subarray operations. ILNumericsV4 / Matlab / Octave / Julia style.
        /// </summary>
        /// <param name="start">Inclusive start index: simple computational expression (+,-,/,*) involving the <see cref="end"/> specifier which evaluates to the last index in this dimension.</param>
        /// <param name="step">Positive step size between adjacent, selected elements.</param>
        /// <param name="end">Scalar, numeric array with the last index of this range.</param>
        /// <returns>Stepped range to be used in indexing expressions.</returns>
        /// <exception cref="ArgumentException"> if <paramref name="start"/> or <paramref name="end"/> are not scalar or not numeric or if the conversion to a long value failed.</exception>
        /// <exception cref="ArgumentException"> if <paramref name="step"/> is larger than <see cref="long.MaxValue"/>.</exception>
        public static ExpressionDimSpec r(ILExpression start, ulong step, BaseArray end) {
            var ret = ExpressionDimSpec.Create();
            ret.m_startExpression = start;
            try {
                using (Scope.Enter()) {
                    Array<long> endA = MathInternal.toint64(end); // releases end
                    if (!endA.IsScalar) {
                        throw new ArgumentException($"The end for the subarray range must be a numeric scalar.");
                    }
                    ret.End = endA.GetValue(0);
                }
            } catch (InvalidCastException) {
                throw new ArgumentException($"The end for the subarray range must be a numeric scalar.");
            } catch (IndexOutOfRangeException) {
                throw new ArgumentException($"The end for the subarray range must be a numeric scalar. Found: {end}.");
            }
            if (step > long.MaxValue) {
                throw new ArgumentException($"The maximum value allowed for step is long.MaxValue ({long.MaxValue}). Found: {step}");
            }
            ret.Step = (long)step;
            ret.IsSingleIndex = false;
            ret.IsSlice = false; 
            return ret;
        }
        /// <summary>
        /// Range for indexing / subarray operations. ILNumericsV4 / Matlab / Octave / Julia style. Step size: 1.
        /// </summary>
        /// <param name="start">Inclusive start index: simple computational expression (+,-,/,*) involving the <see cref="end"/> specifier which evaluates to the last index in this dimension.</param>
        /// <param name="end">Scalar, numeric array with the last index of this range.</param>
        /// <returns>Stepped range to be used in indexing expressions.</returns>
        /// <seealso cref="r(ILExpression, ulong, BaseArray)"/>
        /// <exception cref="ArgumentException"> if <paramref name="start"/> or <paramref name="end"/> are not scalar or not numeric or if the conversion to a long value failed.</exception>
        public static ExpressionDimSpec r(ILExpression start, BaseArray end) {
            return r(start, 1, end);
        }
        /// <summary>
        /// Range for indexing / subarray operations. ILNumericsV4 / Matlab / Octave / Julia style. Step size: 1.
        /// </summary>
        /// <param name="start">Inclusive start index: simple computational expression (+,-,/,*) involving the <see cref="end"/> specifier which evaluates to the last index in this dimension.</param>
        /// <param name="end">Inclusive end index: simple computational expression (+,-,/,*) involving the <see cref="end"/> specifier which evaluates to the last index in this dimension.</param>
        /// <returns>Stepped range to be used in indexing expressions.</returns>
        /// <exception cref="ArgumentException"> if <paramref name="start"/> or <paramref name="end"/> are not scalar or not numeric or if the conversion to a long value failed.</exception>
        public static ExpressionDimSpec r(ILExpression start, ILExpression end) {
            return r(start, 1, end);
        }
        /// <summary>
        /// Range for indexing / subarray operations. ILNumericsV4 / Matlab / Octave / Julia style. Step size: 1.
        /// </summary>
        /// <param name="start">Inclusive start index: scalar, numeric array with the starting index of this range.</param>
        /// <param name="end">Inclusive end index: simple computational expression (+,-,/,*) involving the <see cref="end"/> specifier which evaluates to the last index in this dimension.</param>
        /// <returns>Stepped range to be used in indexing expressions.</returns>
        /// <exception cref="ArgumentException"> if <paramref name="start"/> array or <paramref name="end"/> expression is not scalar or not numeric or if the conversion to a long value failed.</exception>
        public static ExpressionDimSpec r(BaseArray start, ILExpression end) {
            return r(start, 1, end);
        }

        #endregion

        #region slice interface
        /// <summary>
        /// Slice for indexing / subarray operations. numpy style. 
        /// </summary>
        /// <param name="start">Start index of the selection.</param>
        /// <param name="step">Step size. Spacing between adjacent, selected elements.</param>
        /// <param name="end">Stopping index of the selection (exclusive).</param>
        /// <returns>Stepped slice to be used in indexing expressions.</returns>
        public static DimSpec slice(uint start, uint end, uint step) {
            var ret = r(start, step, end);
            ret.IsSlice = true;
            return ret;
        }
        /// <summary>
        /// Slice for indexing / subarray operations. Step size: 1. numpy style. 
        /// </summary>
        /// <param name="start">First element index in this dimension.</param>
        /// <param name="end">Last element (exclusive).</param>
        /// <returns>Stepped range to be used in indexing expressions.</returns>
        public static DimSpec slice(uint start, uint end) {
            var ret = r(start, 1, end);
            ret.IsSlice = true;
            return ret;
        }
        /// <summary>
        /// Slice for indexing / subarray operations. numpy style. 
        /// </summary>
        /// <param name="start">Start index of the selection.</param>
        /// <param name="step">Step size. Spacing between adjacent elements addressed.</param>
        /// <param name="end">Last element (exclusive).</param>
        /// <returns>Stepped range to be used in indexing expressions.</returns>
        /// <exception cref="ArgumentException"> if <paramref name="step"/> is negative.</exception>
        public static DimSpec slice(long start, long end, long step) {
            var ret = r(start, step, end);
            ret.IsSlice = true;
            return ret;
        }
        /// <summary>
        /// Slice for indexing / subarray operations. numpy style. 
        /// </summary>
        /// <param name="start">Start index of the selection.</param>
        /// <param name="end">Last element (exclusive).</param>
        /// <returns>Stepped range to be used in indexing expressions.</returns>
        public static DimSpec slice(long start, long end) {
            var ret = r(start, 1, end);
            ret.IsSlice = true;
            return ret;
        }
        /// <summary>
        /// Slice for indexing / subarray operations. numpy style. 
        /// </summary>
        /// <param name="start">Scalar, numeric array with the starting index of the selection.</param>
        /// <param name="end">Scalar, numeric array with the stopping index of this range.</param>
        /// <returns>Stepped range to be used in indexing expressions.</returns>
        /// <exception cref="ArgumentException"> if <paramref name="start"/> or <paramref name="end"/> are not scalar or not numeric or if the conversion to a long value failed.</exception>
        public static DimSpec slice(BaseArray start, BaseArray end) {
            return slice(start, end, 1);
        }
        /// <summary>
        /// Slice for indexing / subarray operations. numpy style. 
        /// </summary>
        /// <param name="start">Scalar, numeric array with the starting index of the selection.</param>
        /// <param name="step">Positive step size between adjacent elements. Unit: elements.</param>
        /// <param name="end">Scalar, numeric array with the stopping index of this range.</param>
        /// <returns>Stepped range to be used in indexing expressions.</returns>
        /// <exception cref="ArgumentException"> if <paramref name="start"/> or <paramref name="end"/> are not scalar or not numeric or if the conversion to a long value failed.</exception>
        /// <exception cref="ArgumentException"> if <paramref name="step"/> is larger than <see cref="long.MaxValue"/>.</exception>
        public static DimSpec slice(BaseArray start, BaseArray end, ulong step) {
            var ret = DimSpec.Create();
            try {
                using (Scope.Enter()) {
                    Array<long> startA = MathInternal.toint64(start); // release
                    if (!startA.IsScalar) {
                        throw new ArgumentException($"The start for the subarray range must be a numeric scalar.");
                    }
                    ret.Start = startA.GetValue(0);
                }
            } catch (InvalidCastException) {
                throw new ArgumentException($"The start for the subarray range must be a numeric scalar.");
            } catch (IndexOutOfRangeException) {
                throw new ArgumentException($"The start for the subarray range must be a numeric scalar. Found: {start}.");
            }
            try {
                using (Scope.Enter()) {
                    Array<long> endA = MathInternal.toint64(end); // release 
                    if (!endA.IsScalar) {
                        throw new ArgumentException($"The end for the subarray range must be a numeric scalar.");
                    }
                    ret.End = endA.GetValue(0);
                }
            } catch (InvalidCastException) {
                throw new ArgumentException($"The end for the subarray range must be a numeric scalar.");
            } catch (IndexOutOfRangeException) {
                throw new ArgumentException($"The end for the subarray range must be a numeric scalar. Found: {end}.");
            }
            if (step > long.MaxValue) {
                throw new ArgumentException($"The maximum value allowed for step is long.MaxValue ({long.MaxValue}). Found: {step}");
            }
            ret.Step = (long)step;
            ret.IsSingleIndex = false;
            ret.IsSlice = true; 
            return ret;
        }
        /// <summary>
        /// Slice for indexing / subarray operations. numpy style. 
        /// </summary>
        /// <param name="start">Scalar, numeric array with the starting index of the selection.</param>
        /// <param name="step">Positive step size between adjacent elements. Unit: elements.</param>
        /// <param name="end">(Exclusive) selection stopping index: simple computational expression (+,-,/,*) involving the <see cref="end"/> specifier which evaluates to the last index in this dimension.</param>
        /// <returns>Stepped range to be used in indexing expressions.</returns>
        /// <exception cref="ArgumentException"> if <paramref name="start"/> or <paramref name="end"/> are not scalar or not numeric or if the conversion to a long value failed.</exception>
        /// <exception cref="ArgumentException"> if <paramref name="step"/> is larger than <see cref="long.MaxValue"/>.</exception>
        public static ExpressionDimSpec slice(BaseArray start, ILExpression end, ulong step) {
            var ret = r(start, step, end);
            ret.IsSlice = true;
            return ret;
        }
        /// <summary>
        /// Slice for indexing / subarray operations. numpy style. 
        /// </summary>
        /// <param name="start">Selection start index: simple computational expression (+,-,/,*) involving the <see cref="end"/> specifier which evaluates to the last index in this dimension.</param>
        /// <param name="step">Positive step size between adjacent elements. Unit: elements.</param>
        /// <param name="end">(Exclusive) selection stopping index: simple computational expression (+,-,/,*) involving the <see cref="end"/> specifier which evaluates to the last index in this dimension.</param>
        /// <returns>Stepped range to be used in indexing expressions.</returns>
        /// <exception cref="ArgumentException"> if <paramref name="step"/> is larger than <see cref="long.MaxValue"/>.</exception>
        public static ExpressionDimSpec slice(ILExpression start, ILExpression end, ulong step) {
            var ret = r(start, step, end);
            ret.IsSlice = true;
            return ret;
        }
        /// <summary>
        /// Slice for indexing / subarray operations. numpy style. 
        /// </summary>
        /// <param name="start">Selection start index: simple computational expression (+,-,/,*) involving the <see cref="end"/> specifier which evaluates to the last index in this dimension.</param>
        /// <param name="step">Positive step size between adjacent elements. Unit: elements.</param>
        /// <param name="end">Scalar, numeric array with the stopping index of this range.</param>
        /// <returns>Stepped range to be used in indexing expressions.</returns>
        /// <exception cref="ArgumentException"> if <paramref name="start"/> or <paramref name="end"/> are not scalar or not numeric or if the conversion to a long value failed.</exception>
        /// <exception cref="ArgumentException"> if <paramref name="step"/> is larger than <see cref="long.MaxValue"/>.</exception>
        public static ExpressionDimSpec slice(ILExpression start, BaseArray end, ulong step) {
            var ret = ExpressionDimSpec.Create();
            ret.m_startExpression = start;
            try {
                using (Scope.Enter()) {
                    Array<long> endA = MathInternal.toint64(end); // releases end
                    if (!endA.IsScalar) {
                        throw new ArgumentException($"The end for the subarray range must be a numeric scalar.");
                    }
                    ret.End = endA.GetValue(0);
                }
            } catch (InvalidCastException) {
                throw new ArgumentException($"The end for the subarray range must be a numeric scalar.");
            } catch (IndexOutOfRangeException) {
                throw new ArgumentException($"The end for the subarray range must be a numeric scalar. Found: {end}.");
            }
            if (step > long.MaxValue) {
                throw new ArgumentException($"The maximum value allowed for step is long.MaxValue ({long.MaxValue}). Found: {step}");
            }
            ret.Step = (long)step;
            ret.IsSingleIndex = false;
            ret.IsSlice = true; 
            return ret;
        }
        /// <summary>
        /// Slice for indexing / subarray operations. numpy style. 
        /// </summary>
        /// <param name="start">Selection start index: simple computational expression (+,-,/,*) involving the <see cref="end"/> specifier which evaluates to the last index in this dimension.</param>
        /// <param name="end">Scalar, numeric array with the stopping index of this range.</param>
        /// <returns>Stepped range to be used in indexing expressions.</returns>
        /// <seealso cref="slice(ILExpression, BaseArray, ulong)"/>
        /// <exception cref="ArgumentException"> if <paramref name="start"/> or <paramref name="end"/> are not scalar or not numeric or if the conversion to a long value failed.</exception>
        public static ExpressionDimSpec slice(ILExpression start, BaseArray end) {
            return slice(start, end, 1);
        }
        /// <summary>
        /// Slice for indexing / subarray operations. numpy style. 
        /// </summary>
        /// <param name="start">Selection start index: simple computational expression (+,-,/,*) involving the <see cref="end"/> specifier which evaluates to the last index in this dimension.</param>
        /// <param name="end">(Exclusive) selection stopping index: simple computational expression (+,-,/,*) involving the <see cref="end"/> specifier which evaluates to the last index in this dimension.</param>
        /// <returns>Stepped range to be used in indexing expressions.</returns>
        /// <seealso cref="r(BaseArray, ILExpression)"/>
        public static ExpressionDimSpec slice(ILExpression start, ILExpression end) {
            var ret = r(start, 1, end);
            ret.IsSlice = true;
            return ret;
        }
        /// <summary>
        /// Slice for indexing / subarray operations. numpy style. 
        /// </summary>
        /// <param name="start">Scalar, numeric array with the starting index of the selection.</param>
        /// <param name="end">(Exclusiv) selection stopping index: simple computational expression (+,-,/,*) involving the <see cref="end"/> specifier which evaluates to the last index in this dimension.</param>
        /// <returns>Stepped range to be used in indexing expressions.</returns>
        /// <exception cref="ArgumentException"> if <paramref name="start"/> or <paramref name="end"/> are not scalar or not numeric or if the conversion to a long value failed.</exception>
        /// <seealso cref="r(BaseArray, ILExpression)"/>
        public static ExpressionDimSpec slice(BaseArray start, ILExpression end) {
            var ret = r(start, 1, end);
            ret.IsSlice = true;
            return ret; 
        }

        #endregion

        #region constants
        /// <summary>
        /// The constant π as specified in <see cref="System.Math.PI"/>.
        /// </summary>
        public const double pi = Math.PI;
        /// <summary>
        /// The constant π as specified in <see cref="System.Math.PI"/>, single precision.
        /// </summary>
        public const float pif = (float)Math.PI;
        #endregion

        #region  Misc info
        /// <summary>
        /// The date of the build of this module
        /// </summary>
        public static DateTime BuildDate {
            
            get {
                Version myVersion = typeof(Globals).Assembly.GetName().Version;
                // SVN Revision is introduced instead of time in seconds
                DateTime libBuildDate = new DateTime(2000, 1, 1).AddDays(myVersion.Build);
                return libBuildDate;
            }
        }
        #endregion

    }
}

