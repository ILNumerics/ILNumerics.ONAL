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
