using System;
using System.Collections.Generic;
using System.Text;
using ILNumerics;
using System.Globalization;
using System.IO;
using static ILNumerics.Globals; 

namespace ILNumerics.Core.Functions.Builtin {

    internal partial class MathInternal {

        /// <summary>
        /// Write ILNumerics array as separated strings to stream
        /// </summary>
        /// <typeparam name="T">Element type of input array A</typeparam>
        /// <param name="A">ILNumerics array to write as CSV. This is expected to be a matrix.</param>
        /// <param name="stream">Stream to write values to.</param>
        /// <param name="elementConverter">[optional] Custom function used to convert elements of A to string. Default: [T].ToString()</param>
        /// <param name="elementSeparator">[optional] Character chain used to separate individual elements. Default: ',' (comma).</param>
        /// <param name="culture">[optional] Number format info used to acquire formatting information for converting numbers to string. Default: invariant culture (not current thread culture!)</param>
        /// <param name="encoding">[optional] Encoding used to write the string representation of individual elements to the bytes of the stream.</param>
        /// <param name="leaveStreamOpen">[Optional] If set to true <paramref name="stream"/> will not be closed by this method. Default: false.</param>
        /// <remarks><para>A is expected to be a matrix. If A has more than 2 dimensions trailing dimensions will be joined to the second dimension. This corresponds to: A[":;:"].</para>
        /// <para>The optional parameters <paramref name="elementConverter"/>, <paramref name="elementSeparator"/> and <paramref name="culture"/>
        /// are used to control the format of the resulting CSV result.<paramref name="elementConverter"/> is used to convert a single element of <paramref name="A"/> into its string representation, using the current cultures number format or the <see cref="System.Globalization.CultureInfo"/> provided by <paramref name="culture"/>.</para>
        /// <para>Individual elements will be separated in the CSV result by ',' (comma) or by the character chain provided by <paramref name="elementSeparator"/>.</para></remarks>
        internal static void csvwrite<T>(InArray<T> A, Stream stream, Func<T, string> elementConverter = null, string elementSeparator = ",", 
            CultureInfo culture = null, Encoding encoding = null, bool leaveStreamOpen = false) {
            var currentFI = CultureInfo.CurrentCulture;
            StreamWriter sw = null; 
            try {
                if (culture == null) {
                    culture = CultureInfo.InvariantCulture; 
                }
                System.Threading.Thread.CurrentThread.CurrentCulture = culture;
                using (Scope.Enter()) {
                    // some sanity checks 
                    if (elementConverter == null) {
                        if (isfloatingpointtype<T>() && System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator == elementSeparator) {
                            throw new ArgumentException("The decimal separator of the current culture equals the element separator. Writing of floating point numbers will produce invalid CSV data. Please make sure to specify different separators by using a different element separator or a custom elementConverter function!");
                        }
                    }
                    Array<T> A_ = A; 
                    if (isnull(A_) || A_.IsEmpty) return;
                    if (encoding == null) {
                        encoding = Encoding.ASCII;
                    }
                    sw = new StreamWriter(stream, encoding); 
                    if (elementConverter == null) {
                        for (int r = 0; r < A_.S[0]; r++) {
                            string line = string.Join<T>(elementSeparator, A_[r, full].Iterator());
                            sw.Write(line);
                            sw.Write(Environment.NewLine);
                        }
                    } else {
                        var sb = new StringBuilder();
                        for (int r = 0; r < A_.S[0]; r++) {
                            bool inFirstCol = true;
                            foreach (T elem in A_[r, full]) {
                                if (!inFirstCol) {
                                    sb.Append(elementSeparator);
                                } else {
                                    inFirstCol = false;
                                }
                                sb.Append(elementConverter(elem));
                            }
                            sw.Write(sb.ToString());
                            sb.Clear(); 
                            sw.Write(Environment.NewLine);
                        }
                    }
                }
            } finally {
                if (!leaveStreamOpen) {
                    if (sw != null) {
                        sw.Dispose();
                    }
                } else {
                    if (sw != null) {
                        sw.Flush();
                    }
                }
                System.Threading.Thread.CurrentThread.CurrentCulture = currentFI;
            }
        }

        /// <summary>
        /// Write ILNumerics array as separated strings to file
        /// </summary>
        /// <typeparam name="T">Element type of input array A</typeparam>
        /// <param name="A">ILNumerics array to write as CSV. This is expected to be a matrix.</param>
        /// <param name="filename">Name of the file to write values to.</param>
        /// <param name="elementConverter">[optional] Custom function used to convert elements of A to string. Default: [T].ToString()</param>
        /// <param name="elementSeparator">[optional] Character chain used to separate individual elements. Default: ',' (comma).</param>
        /// <param name="culture">[optional] Number format info used to acquire formatting information for converting numbers to string. Default: invariant culture (not current thread culture!)</param>
        /// <param name="encoding">[optional] Encoding used to write the string representation of individual elements to the bytes of the file.</param>
        /// <remarks><para>A is expected to be a matrix. If A has more than 2 dimensions trailing dimensions will be joined to the second dimension. This corresponds to: A[":;:"].</para>
        /// <para>The optional parameters <paramref name="elementConverter"/>, <paramref name="elementSeparator"/> and <paramref name="culture"/>
        /// are used to control the format of the resulting CSV result.<paramref name="elementConverter"/> is used to convert a single element of <paramref name="A"/> into its string representation, using the current cultures number format or the <see cref="System.Globalization.CultureInfo"/> provided by <paramref name="culture"/>.</para>
        /// <para>Individual elements will be separated in the CSV result by ',' (comma) or by the character chain provided by <paramref name="elementSeparator"/>.</para></remarks>
        internal static void csvwrite<T>(InArray<T> A, string filename, Func<T, string> elementConverter = null, string elementSeparator = ",", CultureInfo culture = null, Encoding encoding = null) {
            using (var file = File.Create(filename)) {
                csvwrite(A, file, elementConverter: elementConverter, elementSeparator: elementSeparator, culture: culture, encoding: encoding); 
            }
        }

    }
}