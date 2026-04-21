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
#pragma warning disable CS1591

namespace ILNumerics.F2NET {
    public static class ExtensionMethods {

        /// <summary>
        /// Fortran CHARACTER assignment semantic for FString. 
        /// </summary>
        /// <param name="length">Target length.</param>
        /// <returns>A new instance of FString, as a deep copy of this FString, with a length of 'length'.</returns>
        public static FString AssignTo(this FString fstring, int length) {
            if (object.Equals(fstring, null)) {
                return null;
            }
            var charLen = length >= 0 ? length : (fstring.Length >= 0 ? fstring.Length : fstring.StringInternal.Length);
            var retChars = new char[charLen];
            var piv = Math.Min(fstring.StringInternal.Length, retChars.Length);
            System.Array.Copy(fstring.StringInternal, retChars, piv);
            for (int i = piv; i < retChars.Length; i++) {
                retChars[i] = ' ';
            }
            return new FString(retChars, length);
        }
        public static FString AssignTo(this string fstring, int length) {
            if (object.Equals(fstring, null)) {
                return null;
            }
            var charLen = length >= 0 ? length : fstring.Length;
            var retChars = new char[charLen];
            var piv = Math.Min(fstring.Length, retChars.Length);
            System.Array.Copy(fstring.ToCharArray(), retChars, piv);
            for (int i = piv; i < retChars.Length; i++) {
                retChars[i] = ' ';
            }
            return new FString(retChars, length);
        }

        /// <summary>
        /// Converts current FString instance into different length instance, keeping the same reference. 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static FString Convert(this FString val, int length) {
            if (length < 0 || length == val.Length) {
                return val;
            }
            return new FString(val.StringInternal, length);
        }

        public static void Assign(this FString left, FString right) {
            if (object.Equals(left, null)) {
                throw new ArgumentNullException(nameof(left));
            }
            if (object.Equals(right, null)) {
                throw new ArgumentNullException(nameof(right));
            }
            int copyLen = 0; 
            if (left.Length < 0) {
                copyLen = Math.Min(left.StringInternal.Length, right.Length); 
            } else {
                copyLen = Math.Min(left.Length, right.Length);
            }
            System.Array.Copy(right.StringInternal, left.StringInternal, copyLen);
            for (int i = copyLen; i < left.StringInternal.Length; i++) {
                left.StringInternal[i] = ' ';
            }
        }

    }
}
