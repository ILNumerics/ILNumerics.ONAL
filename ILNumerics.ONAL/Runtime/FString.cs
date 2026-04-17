using ILNumerics.F2NET;
using System;
using System.Collections.Generic;
using System.Text;
#pragma warning disable CS1591

namespace ILNumerics.F2NET {
    public sealed class FString {

        private char[] m_string { get; set; }
        internal char[] StringInternal { get { return m_string; } }
        private int m_length { get; set; } 
        /// <summary>
        /// Gets the length configured for the current CHARACTER type. Variable length strings return -1. 
        /// </summary>
        public int Length { get { return m_length; } internal set { m_length = value; } }
        /// <summary>
        /// Gets the actual length of the current FString instance. 
        /// </summary>
        public int ActualLength {
            get {
                var actualLength = m_length >= 0 ? m_length : m_string.Length;
                return actualLength; 
            }
        }

        internal FString(char[] source, int? length = null) {
            if (source == null) {
                throw new ArgumentNullException(nameof(source)); 
            }
            if (length == null) {
                length = source.Length; 
            }
            if (length > source.Length) {
                throw new ArgumentException($"The '{nameof(length)}' parameter must not be larger than the length of the string provided in '{nameof(source)}'.");
            }
            m_string = source;
            m_length = length.GetValueOrDefault(); 
        }
        /// <summary>
        /// Creates a new FString object of specified length.
        /// </summary>
        /// <param name="len"></param>
        public FString(uint len) : this (new char[len], (int)len) {}

        public static FString Create(string source) {
            return new FString(source.ToCharArray());
        }
        public static implicit operator FString(string source) {
            return new FString(source.ToCharArray());
        }
        public static implicit operator FString(char source) {
            return new FString(new char[1] { source }, 1);
        }
        public static implicit operator string(FString source) {
            return source.ToString(); 
        }
        public static bool operator ==(FString a, FString b) {
            /*A character relational intrinsic operation is interpreted as having the logical value true if the values of the
                operands satisfy the relation specified by the operator. A character relational intrinsic operation is interpreted as
                having the logical value false if the values of the operands do not satisfy the relation specified by the operator.
                For a character relational intrinsic operation, the operands are compared one character at a time in order,
                beginning with the first character of each character operand. If the operands are of unequal length, the shorter
                operand is treated as if it were extended on the right with blanks to the length of the longer operand. If both
                x1 and x2 are of zero length,x1 is equal to x2; if every character of x1 is the same as the character in the
                corresponding position in x2, x1is equal to x2. Otherwise, at the first position where the character operands
                differ, the character operand is considered to be less than if the character value of at this position
                precedes the value of in the collating sequence (4.3.2.1.1);x1 is greater than x2 if the character value of x1
                at this position follows the value of x2 in the collating sequence. Note that the collating sequence depends
                partially on the processor; however, the result of the use of the operators .EQ., .NE., ==, and /= does not depend
                on the collating sequence.
            */
            var lena = a.Length >= 0 ? a.Length : a.StringInternal.Length; 
            var lenb = b.Length >= 0 ? b.Length : b.StringInternal.Length;
            System.Diagnostics.Debug.Assert(lena <= a.StringInternal.Length);
            System.Diagnostics.Debug.Assert(lenb <= b.StringInternal.Length); 

            int i = 0; 
            for (; i < Math.Min(lena, lenb); i++) {
                if (a.StringInternal[i] != b.StringInternal[i]) return false; 
            }
            for (; i < lena; i++) {
                if (a.StringInternal[i] != ' ') return false;
            }
            for (; i < lenb; i++) {
                if (b.StringInternal[i] != ' ') return false;
            }
            return true; 
        }
        public static bool operator !=(FString a, FString b) {
            return !(a == b);
        }

        public override bool Equals(object obj) {
            FString other = obj as FString; 
            if (other == null) return false;
            return GetHashCode() == other.GetHashCode();  
        }
        public override int GetHashCode() {
            int ret = 17; 
            var len = m_length < 0 ? m_string.Length : m_length; 
            for (int i = 0; i < len; i++) {
                ret = 23 + ret * m_string[i];
            }
            return ret; 
        }

        /// <summary>
        /// Fortran CHARACTER assignment semantic for FString. 
        /// </summary>
        /// <param name="length">Target length.</param>
        /// <returns>A new instance of FString, as a deep copy of this FString, with a length of 'length'.</returns>
        public FString AssignTo(int length) {
            var charLen = length >= 0 ? length : (Length >= 0 ? Length : m_string.Length);
            var retChars = new char[charLen];
            var piv = Math.Min(m_string.Length, retChars.Length); 
            System.Array.Copy(m_string, retChars, piv);
            for (int i = piv; i < retChars.Length; i++) {
                retChars[i] = ' '; 
            }
            return new FString(retChars, length); 
        }

        public override string ToString() {
            if (m_length > m_string.Length) {
                throw new InvalidProgramException(); 
            } else if (m_length == m_string.Length || m_length < 0) {
                return new string(m_string);
            } else {
                // m_fType.Length < m_string.Length
                return new string(m_string, 0, m_length); 
            }
        }

        /// <summary>
        /// 1-based (!) subscript into this FString. 
        /// </summary>
        /// <param name="start">First character position, 1-based!</param>
        /// <param name="end">last included character position, 1-based.</param>
        /// <value>New string value to be copied into the specified part of this array. Note, that the underlying string is mutated(!), hence, all FString instances, referencing this string are changed, too.</value>
        /// <returns>New (!) FString as a copy of the substring specified by start &amp; end.</returns>
        public FString this[long? start = null, long? end = null] {
            get {
                // m_length can be -1 !!
                if (!start.HasValue) start = 1;
                if (start < 1) start = 1;
                if (!end.HasValue) {
                    end = ActualLength;
                } else if (end > ActualLength) {
                    throw new IndexOutOfRangeException($"Subscript end index out of range: {end}. Length of current CHARACTER instance: {ActualLength}.");
                }
                if (start > ActualLength || end < start) {
                    return new FString(new char[0], 0);
                }
                int len = (int)end.GetValueOrDefault() - (int)start.GetValueOrDefault() + 1;
                return new FString(new string(m_string, (int)start.GetValueOrDefault() - 1, len).ToCharArray(), len); 
            }
            set {
                // m_length can be -1 !!
                if (!start.HasValue) start = 1;
                if (!end.HasValue) {
                    end = ActualLength;
                } else if (end > ActualLength) {
                    throw new IndexOutOfRangeException($"Subscript end index out of range: {end}. Length of current CHARACTER instance: {ActualLength}.");
                }
                if (start < 1) start = 1;
                if (start > ActualLength || end < start) {
                    return;
                }
                int len = (int)end.GetValueOrDefault() - (int)start.GetValueOrDefault() + 1;

                // EDIT: not required...!?
                //if (value.Length != len) {
                //    throw new ArgumentException($"In a string subscript assignment the length of the right side value must match the length of the left side subscript. Found: [{len}] <= [{value.Length}].");
                //}

                if (len <= value.ActualLength) {
                    System.Array.Copy(value.m_string, 0, m_string, start.GetValueOrDefault() - 1, len); 
                } else {
                    System.Array.Copy(value.m_string, 0, m_string, start.GetValueOrDefault() - 1, Math.Min(len,value.ActualLength));
                    for (var i = start.GetValueOrDefault() + value.ActualLength; i < end; i++) {
                        m_string[i] = ' '; 
                    }
                }
            }
        }

    }
}
