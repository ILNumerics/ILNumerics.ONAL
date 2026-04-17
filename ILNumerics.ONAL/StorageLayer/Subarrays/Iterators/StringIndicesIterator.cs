using ILNumerics.Core.StorageLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.StorageLayer {
    internal struct StringIndicesIterator : IIndexIterator {

        string m_string;
        long m_currentValue;
        int m_currentCharPosition;
        long m_step;
        long m_end;
        long m_start;
        int m_subrangeCount; 
        long m_lastDimIndex;
        long m_length;
        long m_maxValue;

        internal StringIndicesIterator(string a, long endIndex, bool checkLimits) {
            System.Diagnostics.Debug.Assert(endIndex >= -1); // empty array: endIndex = -1!
            m_string = a;
            m_lastDimIndex = endIndex;
            m_currentValue = -1;
            m_currentCharPosition = -1;
            m_step = -1;
            m_end = -1;
            m_start = -1;
            m_subrangeCount = 0;
            m_length = -1;
            m_maxValue = -1;
            computeLengthInternal(checkLimits); 
        }
        /// <summary>
        /// increments to the next subrange and sets pointer to the last char of it
        /// </summary>
        bool nextSubrange(bool checkLimits = false) {
            int start = m_currentCharPosition;
            while (++start < m_string.Length
                && !char.IsLetterOrDigit(m_string[start])
                && m_string[start] != ':'
                && m_string[start] != '-') { }
            if (start == m_string.Length) return false;
            int end = start;
            int colonCount = 0;
            int colonPos1 = -1; 
            int colonPos2 = -1;
            while (end < m_string.Length
                && m_string[end] != ',') {
                if (m_string[end] == ':') {
                    colonCount++;
                    if (colonCount == 1)
                        colonPos1 = end;
                    else if (colonCount == 2) {
                        colonPos2 = end;
                    } else {
                        throw new ArgumentException("Invalid format of index specification: too many colons ':' found."); 
                    }
                }
                end++;
            }
            end--;

            // we have a valid subrange! It might be empty, though. 
            m_currentCharPosition = end;
            var rangeString = m_string.Substring(start, end - start + 1).Trim();
            if (rangeString.Length > 0) {
                switch (colonCount) {
                    case 0:
                        // single index
                        parseStart(rangeString, checkLimits); 
                        m_end = m_start;
                        m_step = 1;
                        break;
                    case 1:
                        // simple range
                        var startRangeString = rangeString.Substring(0, colonPos1 - start).Trim();
                        parseStart(startRangeString, checkLimits);
                        var endRangeString = m_string.Substring(colonPos1 + 1, end - colonPos1).Trim();
                        parseEnd(endRangeString, checkLimits);
                        m_step = 1;
                        break;
                    case 2:
                        // stepped range
                        startRangeString = rangeString.Substring(0, colonPos1 - start).Trim();
                        parseStart(startRangeString, checkLimits);
                        var stepRangeString = m_string.Substring(colonPos1 + 1, colonPos2 - colonPos1 - 1).Trim();
                        if (stepRangeString.Length == 0) {
                            m_step = 1; 
                        } else if (!long.TryParse(stepRangeString, out m_step) || m_step < 0) {
                            throw new ArgumentException($"Invalid step size for subrange specified: the term '{stepRangeString}' could not be translated to a valid, positive step size.");
                        }
                        endRangeString = m_string.Substring(colonPos2 + 1, end - colonPos2);
                        parseEnd(endRangeString, checkLimits);
                        break;
                    default:
                        throw new ArgumentException($"Invalid subrange specified: the term '{m_string.Substring(start, end - start + 1)}' could not be tranlated to a valid index /-range.");
                }
                m_currentValue = m_start;
                return true;
            } else {
                // empty subrange
                return nextSubrange(checkLimits); 
            }

        }

        private void parseEnd(string endRangeString, bool checkLimits = false) {
            if (endRangeString.Length == 0 || endRangeString.ToLower() == "end") {
                m_end = m_lastDimIndex;
            } else if (!long.TryParse(endRangeString, out m_end)) {
                throw new ArgumentException($"Invalid end of subrange specified: the term '{endRangeString}' could not be translated to a valid end index.");
            } else if (m_end < 0) {
                m_end = m_lastDimIndex + 1 + m_end; // no effect on empty array
            }
            if ((m_end < 0 && m_lastDimIndex >= 0) || (checkLimits && m_end > m_lastDimIndex)) {
                throw new IndexOutOfRangeException($"End index '{endRangeString}' is out of range. Valid range: -{m_lastDimIndex + 1} <= [index] <= {m_lastDimIndex}.");
            }
        }

        private void parseStart(string startRangeString, bool checkLimits = false) {
            if (startRangeString.Length == 0) {
                m_start = 0; // Start is always on 0, even for empty arrays! This convention supports the correct computation of Length later.
                if (m_lastDimIndex < 0) {
                    return;  // empties are an exception. Don't perform further checks on them!
                }
            } else if (!long.TryParse(startRangeString, out m_start)) {
                if (startRangeString.ToLower() == "end") {
                    m_start = m_lastDimIndex;
                } else {
                    throw new ArgumentException($"Invalid start of subrange specified: the term '{startRangeString}' could not be translated to a valid start index.");
                }
            } else if (m_start < 0) {
                m_start = m_lastDimIndex + 1 + m_start;
            }
            if ((m_start < 0) || (checkLimits && m_start > m_lastDimIndex)) {
                throw new IndexOutOfRangeException($"Start index {startRangeString} is out of range. Valid range: -{m_lastDimIndex + 1} <= [index] <= {m_lastDimIndex}.");
            }
        }

        public long Current {
            get {
                //if (m_currentCharPosition < 0) throw new InvalidOperationException("This iterator does not point to a value. Call MoveNext() first!");
                return m_currentValue; 
            }
        }

        object IEnumerator.Current => Current; 

        public void Dispose() {
            // nothing to do. this lives on the stack only. (Well, except if boxing kicks in)
        }

        public IEnumerator<long> GetEnumerator() {
            return this;
        }

        public bool MoveNext() {
            return moveNextInSubrange() || nextSubrange(false); 
        }
        bool moveNextInSubrange() {
            if (m_currentCharPosition >= 0) {
                if (m_currentValue + m_step <= m_end) {
                    m_currentValue += m_step;
                    return true;
                } else {
                    return false;
                }
            } else if (m_subrangeCount == 1) {
                m_currentValue = m_start;
                m_currentCharPosition = m_string.Length - 1;
                return true;
            } else { 
                return false; 
            }
        }
        public void Reset() {
            m_currentCharPosition = -1; 


            //if (m_subrangeCount == 1 && m_currentCharPosition == m_string.Length - 1) {
            //    // simple range -> reuse! 
            //    m_currentValue = m_start;
            //} else {
            //    m_currentCharPosition = -1;
            //    if (!nextSubrange()) {
            //        throw new InvalidOperationException("This iterator does not point to a value."); 
            //    }
            //}
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this; 
        }
        /// <summary>
        /// Gets the iteration length of the current subrange (if exists). 
        /// </summary>
        /// <returns></returns>
        public long GetLength() {

            return m_length; 
        }
        /// <summary>
        /// Gets the end index of the current subrange (if exists). 
        /// </summary>
        /// <returns></returns>
        public long? GetMaximum() {
            return (m_maxValue < 0) ? default(long?) : m_maxValue;
        }
        /// <summary>
        /// Gets the biginning index of the current subrange (if exists). 
        /// </summary>
        /// <returns></returns>
        public long? GetMinimum() {
            return (m_start < 0) ? default(long?) : m_start;
        }
        void computeLengthInternal(bool checkLimits) {
            // this is called only once from the constructor!
            m_length = 0; 
            if (m_string.Contains(";")) {
                throw new ArgumentException("The ; dimension separator character is only supported when dimension indices are provided as a single string.");
            }
            while (nextSubrange(checkLimits)) {
                m_subrangeCount++;
                m_length += 1 + (m_end - m_start) / m_step;
                var srMax = Math.Max(m_end, m_start); 
                if (m_maxValue < srMax) {
                    m_maxValue = srMax; 
                }
            }
            m_currentCharPosition = -1;
        }

        public long GetLastDimensionIndex() {
            return m_lastDimIndex; 
        }
        /// <summary>
        /// Gets the stepsize of the single range defined or null if multiple ranges exist.
        /// </summary>
        /// <returns></returns>
        public long? GetStepSize() {
            return (m_subrangeCount == 1) ? m_step : (long?)null; 
        }
    }
}
namespace ILNumerics {
    public static partial class ExtensionMethods {
        internal static IIndexIterator AsIndices(this string a, long lastIdx, bool checkLimits = true) {
            return new StringIndicesIterator(a, lastIdx, checkLimits: checkLimits);
        }

    }
}
