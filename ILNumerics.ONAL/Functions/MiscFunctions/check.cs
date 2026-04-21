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
using static ILNumerics.Globals; 

namespace ILNumerics.Core.Functions.Builtin {
    internal static partial class MathInternal {

        /// <summary>
        /// Checks if <paramref name="A"/> is valid, assign default if null. 
        /// </summary>
        /// <typeparam name="T">Element type</typeparam>
        /// <param name="A">Input array.</param>
        /// <param name="onNull">Provides default value when <paramref name="A"/> is null. This is only evaluated when <paramref name="A"/> is null.</param>
        /// <returns><paramref name="A"/> or the result returned from <paramref name="onNull"/>.</returns>
        internal static Array<T> checknull<T>(InArray<T> A, Func<Array<T>> onNull) {
            using (Scope.Enter(A)) {
                if (object.Equals(A, null)) {
                    return onNull(); 
                }
                return A;
            }
        }
        /// <summary>
        /// Checks if <paramref name="A"/> is a valid parameter.
        /// </summary>
        /// <typeparam name="T">Element type</typeparam>
        /// <param name="A">Input array.</param>
        /// <param name="ErrorMessage">[Optional] Exception message.</param>
        /// <param name="evaluation">[Optional] Evaluation function, checks input parameter and transforms it into result, gets only called for <paramref name="A"/> other than null</param>
        /// <param name="allowNullInput">[optional] Only if <paramref name="A"/> is null -> for true: returns null, false: throws exception. If <paramref name="Default"/> was defined, this parameter is ignored.</param>
        /// <param name="Default">[optional] If <paramref name="A"/> is null on input, this value is returned. If no default is given (i.e: null), <paramref name="allowNullInput"/> is evaluated.</param>
        /// <returns>Result of calling <paramref name="evaluation"/>(<paramref name="A"/>) or <paramref name="A"/>.</returns>
        /// <exception cref="ArgumentException">If A was null on entry and <paramref name="allowNullInput"/> is false</exception>
        internal static Array<T> check<T>(InArray<T> A, Func<InArray<T>, Array<T>> evaluation = null, bool allowNullInput = false, string ErrorMessage = "", InArray<T> Default = null) {
            using (Scope.Enter()) {
                if (object.Equals(A, null)) {
                    if (isnull(Default)) {
                        if (!allowNullInput)
                            throw new ArgumentException("A required input parameter was found to be null.");
                        else
                            return null;
                    } else {
                        return Default.C;
                    }
                }
                if (evaluation == null)
                    return A;
                Array<T> ret = evaluation(A);
                if (object.Equals(ret, null))
                    throw new ArgumentException(String.IsNullOrEmpty(ErrorMessage) ? "Invalid input parameter. Consult the documentation!" : ErrorMessage);
                else
                    return ret;
            }
        }

        /// <summary>
        /// Checks if <paramref name="A"/> is a valid parameter.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="ErrorMessage">[Optional] Exception message. Default: empty string.</param>
        /// <param name="evaluation">[Optional] Evaluation function, checks input parameter and transforms it into result, gets only called for <paramref name="A"/> other than null.</param>
        /// <param name="allowNullInput">[Optional] Only if <paramref name="A"/> is null -> for true: returns null, false: throws exception.</param>
        /// <returns>Result of calling <paramref name="evaluation"/>(<paramref name="A"/>) or <paramref name="A"/>.</returns>
        /// <exception cref="ArgumentException">If <paramref name="A"/> was null on entry and <paramref name="allowNullInput"/> is false.</exception>
        internal static Logical check(InLogical A, Func<InLogical, Logical> evaluation = null, bool allowNullInput = false, string ErrorMessage = "") {
            using (Scope.Enter(A)) {
                if (object.Equals(A, null)) {
                    if (!allowNullInput)
                        throw new ArgumentException("A required input parameter was found to be null.");
                    else
                        return null;
                }
                if (evaluation == null)
                    return A;
                Logical ret = evaluation(A);
                if (object.Equals(ret, null))
                    throw new ArgumentException(String.IsNullOrEmpty(ErrorMessage) ? "Invalid input parameter. Consult the documentation!" : ErrorMessage);
                else
                    return ret;
            }
        }
        /// <summary>
        /// Check if <paramref name="A"/> is a valid parameter.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <param name="ErrorMessage">[Optional] Exception message</param>
        /// <param name="evaluation">[Optional] Evaluation function, checks <paramref name="A"/> and transforms it into result, gets only called for <paramref name="A"/> other than null.</param>
        /// <param name="allowNullInput">[Optional] Only if <paramref name="A"/> is null -> true: returns null, false: throws exception.</param>
        /// <returns>Result of calling <paramref name="evaluation"/>(<paramref name="A"/>) or <paramref name="A"/>.</returns>
        /// <exception cref="ArgumentException">If <paramref name="A"/> was null on entry and <paramref name="allowNullInput"/> is false.</exception>
        internal static Cell check(InCell A, Func<InCell, Cell> evaluation = null, bool allowNullInput = false, string ErrorMessage = "") {
            using (Scope.Enter(A)) {
                if (object.Equals(A, null)) {
                    if (!allowNullInput)
                        throw new ArgumentException("A required input parameter was found to be null.");
                    else
                        return null;
                }
                if (evaluation == null)
                    return A;
                Cell ret = evaluation(A);
                if (object.Equals(ret, null))
                    throw new ArgumentException(String.IsNullOrEmpty(ErrorMessage) ? "Invalid input parameter. Consult the documentation!" : ErrorMessage);
                else
                    return ret;
            }
        }
    }
}
