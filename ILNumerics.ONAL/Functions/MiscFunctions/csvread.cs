using System;
using System.Text;
using System.Globalization;
using System.IO;
using ILNumerics;
using static ILNumerics.Globals;
using ILNumerics.Core.Misc;

namespace ILNumerics.Core.Functions.Builtin {

    internal partial class MathInternal {

        /// <summary>
        /// Reads comma separated values from lines in a string, optionally using custom separators, element and number formats.
        /// </summary>
        /// <typeparam name="T">Element type to return.</typeparam>
        /// <param name="csvData">String with CSV data in lines.</param>
        /// <param name="startRow">[Optional] Start reading the values from that row. Default: 0.</param>
        /// <param name="startCol">[Optional] Start reading values from that column. Default: 0.</param>
        /// <param name="endRow">[Optional] Index of the last row to take into account. Default: last row in <paramref name="csvData"/>.</param>
        /// <param name="endCol">[Optional] Index of the last column to take into account. Default: last column in <paramref name="csvData"/>.</param>
        /// <param name="elementConverter">[Optional] Custom function to convert each element from its string representation. Default: parse as <see cref="System.Double"/>.</param>
        /// <param name="elementSeparator">[Optional] String separating individual elements. Default: , (comma)</param>
        /// <param name="culture">[Optional] Number format used while parsing individual elements. Default: invariant culture (not current thread culture!)</param>
        /// <returns>Matrix with elements read from <paramref name="csvData"/>.</returns>
        /// <remarks><para>The data lines in <paramref name="csvData"/> is parsed using <paramref name="elementSeparator"/> as element 
        /// separator. Arbitrary element types <typeparamref name="T"/> can be parsed by providing a customized function 
        /// <paramref name="elementConverter"/> for parsing individual elements. The current threads culture is temporarily changed to use the 
        /// <see href="System.Globalization.NumberFormatInfo">number format info</see> provided in <paramref name="culture"/>. The number 
        /// format provides the parser with info about decimal separators among others used to determine number formats for parsing.</para>
        /// </remarks>
        internal static Array<T> csvread<T>(string csvData, int startRow = 0, int startCol = 0, int? endRow = null, int? endCol = null,
            Func<string, T> elementConverter = null, string elementSeparator = ",", CultureInfo culture = null)
        {
            var currentFI = CultureInfo.CurrentCulture;
            try {
                if (culture == null) {
                    culture = CultureInfo.InvariantCulture;
                }
                System.Threading.Thread.CurrentThread.CurrentCulture = culture;
                // sanity check
                if (isfloatingpointtype<T>() && elementConverter == null && 
                    CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator == elementSeparator) {
                        throw new ArgumentException("The configured decimal separator for the current culture equals the element separator. Parsing of floating point numbers will not be possible. Please make sure to specify different separators by using a different culture, a different element separator or a custom elementConverter function matching the actual CSV configuration!"); 
                }
                using (CSVLinesProvider<T> csvHelper = new CSVLinesProvider<T>(csvData)) {
                    return csvreadinternal<T>(csvHelper, startRow, startCol, endRow, endCol, elementConverter, elementSeparator);
                }
            } finally {
                System.Threading.Thread.CurrentThread.CurrentCulture = currentFI;
            }
        }

        /// <summary>
        /// Reads comma separated values from stream, optionally using custom separators, element and number formats.
        /// </summary>
        /// <typeparam name="T">Element type to return.</typeparam>
        /// <param name="stream">Stream with CSV data in lines.</param>
        /// <param name="startRow">[Optional] Start reading the values from that row. Default: 0.</param>
        /// <param name="startCol">[Optional] Start reading values from that column. Default: 0.</param>
        /// <param name="endRow">[Optional] Index of the last row to take into account. Default: last row in <paramref name="stream"/>.</param>
        /// <param name="endCol">[Optional] Index of the last column to take into account. Default: last column in <paramref name="stream"/>.</param>
        /// <param name="elementConverter">[Optional] Custom function to convert each element from its string representation. Default: parse as <see cref="System.Double"/>.</param>
        /// <param name="elementSeparator">[Optional] String separating individual elements. Default: , (comma)</param>
        /// <param name="culture">[Optional] Number format used while parsing individual elements. Default: invariant culture (not current thread culture!)</param>
        /// <param name="encoding">[Optional] Encoding used to read bytes from stream. Default: ASCII encoding.</param>
        /// <param name="leaveStreamOpen">[Optional] If set to true <paramref name="stream"/> will not be closed by this method. Default: false.</param>
        /// <returns>Matrix with elements read from <paramref name="stream"/>.</returns>
        /// <remarks><para>The data lines in <paramref name="stream"/> are parsed using <paramref name="elementSeparator"/> as element 
        /// separator. Arbitrary element types <typeparamref name="T"/> can be parsed by providing a customized function 
        /// <paramref name="elementConverter"/> for parsing individual elements. The current threads culture is temporarily changed to use the 
        /// <see href="System.Globalization.CultureInfo">number format info</see> provided in <paramref name="culture"/>. The number 
        /// format of the given culture provides the parser with info about decimal separators among others used to determine number formats for parsing.</para>
        /// </remarks>
        internal static Array<T> csvread<T>(Stream stream, int startRow = 0, int startCol = 0, int? endRow = null, int? endCol = null,
            Func<string, T> elementConverter = null, string elementSeparator = ",", CultureInfo culture = null, Encoding encoding = null, bool leaveStreamOpen = false)
        {
            var currentFI = CultureInfo.CurrentCulture;
            try {
                if (culture == null) {
                    culture = CultureInfo.InvariantCulture; 
                }
                System.Threading.Thread.CurrentThread.CurrentCulture = culture;
                // sanity check
                if (isfloatingpointtype<T>() && elementConverter == null &&
                    CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator == elementSeparator) {
                    throw new ArgumentException("The configured decimal separator for the current culture equals the element separator. Parsing of floating point numbers will not be possible. Please make sure to specify different separators by using a different culture, a different element separator or a custom elementConverter function matching the actual CSV configuration!");
                }
                using (CSVLinesProvider<T> csvHelper = new CSVLinesProvider<T>(stream, encoding, leaveStreamOpen)) {
                    return csvreadinternal<T>(csvHelper, startRow, startCol, endRow, endCol, elementConverter, elementSeparator);
                }
            } finally {
                System.Threading.Thread.CurrentThread.CurrentCulture = currentFI; 
            }
        }

        /// <summary>
        /// Parses an input string to double first, than converts it if the given type <typeparamref name="T"/> is not <see cref="System.Double"/>.
        /// </summary>
        /// <typeparam name="T">Type of array elements to return.</typeparam>
        /// <param name="element">CSV element to parse.</param>
        /// <returns>Parsed CSV element with a type of <typeparamref name="T"/>.</returns>
        private static T CSVParseDoubleAs<T>(string element) {
            Type t = typeof(T);
            var style = NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent;
            if (t == typeof(complex)) {
                return (T)Convert.ChangeType(complex.Parse(element, style, CultureInfo.CurrentCulture), t);
            } else if (t == typeof(fcomplex)) {
                return (T)Convert.ChangeType(fcomplex.Parse(element, style, CultureInfo.CurrentCulture), t);
            } else {
                double val = double.Parse(element.Trim(), style, CultureInfo.CurrentCulture);
                return (T)Convert.ChangeType(val, t);
            }
        }

        #region private helpers
        private static bool isfloatingpointtype<T1>() {
            T1 item = default(T1);
            return item is System.Double || item is System.Single || item is complex || item is fcomplex;
        }

        /// <summary>
        /// Private generalization of reading CSV files. 
        /// The method hides stream/string input with CsvHelper class. 
        /// It allows the user to specify special functions to convert their special data.
        /// </summary>
        /// <typeparam name="T">Element type to return.</typeparam>
        /// <param name="helper">CsvHelper to hide stream/string inputs.</param>
        /// <param name="startRow">[Optional] Start reading the values from that row. Default: 0.</param>
        /// <param name="startCol">[Optional] Start reading values from that column. Default: 0.</param>
        /// <param name="endRow">[Optional] Index of the last row to take into account. Default: last row in <paramref name="helper"/>.</param>
        /// <param name="endCol">[Optional] Index of the last column to take into account. Default: last column in <paramref name="helper"/>.</param>
        /// <param name="elementParser">[Optional] Element parser funtion.</param>
        /// <param name="elementSeparator">[Optional] String separating individual elements. Default: , (comma)</param>
        /// <returns>Matrix with elements read from source wrapped by helper.</returns>
        private static Array<T> csvreadinternal<T>(CSVLinesProvider<T> helper, int startRow = 0, int startCol = 0, int? endRow = null, int? endCol = null, Func<string,T> elementParser = null, string elementSeparator = ",")
        {
            using (Scope.Enter())
            {
                // check arguments
                if (startRow < 0)
                    throw new ArgumentException("startRow must be greater or equal to 0");

                if (startCol < 0)
                    throw new ArgumentException("startCol must be greater or equal to 0");

                if (endRow < 0)
                    throw new ArgumentException("endRow must be greater or equal to 0");

                if (endCol < 0)
                    throw new ArgumentException("endCol must be greater or equal to 0");

                if (endRow < startRow)
                    throw new ArgumentException("endRow must be greater than startRow");

                if (endCol < startCol)
                    throw new ArgumentException("endCol must be greater than startCol");

                // we dont know the length of the stream - so we are guessing iteratively
                int rowSizeTracker = 16;
                int numCols = 0;
                int currentRow = 0;
                int currentIdx = 0;
                bool firstLine = true;
                int lastCol = 0;
                int totalRows = (endRow.HasValue) ? endRow.GetValueOrDefault() - startRow : 0;

                Array<T> ret = null;
                string line = null;

                if (elementParser == null) {
                    // guess most important element parse functions
                    elementParser = CSVParseDoubleAs<T>; 
                }

                while (helper.GetNextLine(out line))
                {
                    if (firstLine)
                    {
                        // init everything
                        numCols = parseCSVLine(line, null, 0, startRow, int.MaxValue, elementParser, elementSeparator);
                        lastCol = (endCol.HasValue) ? Math.Min(endCol.GetValueOrDefault(), numCols - 1) : numCols - 1;

                        //lastRow - startRow + 1 => tracked now for unknown data length, lastCol - startCol + 1
                        ret = zeros<T>(rowSizeTracker, lastCol - startCol + 1);
                        firstLine = false;
                    }

                    if (rowSizeTracker <= currentIdx)
                    {
                        // expanding
                        rowSizeTracker *= 2;
                        ret.SetValue(default(T), rowSizeTracker - 1, 0);
                    }

                    if (currentRow++ >= startRow)
                    {
                        if (currentIdx <= totalRows || totalRows == 0)
                        {
                            parseCSVLine<T>(line, ret, currentIdx++, startCol, lastCol, elementParser, elementSeparator);
                        }
                        else
                        {
                            // we are also out
                            break;
                        }
                    }
                }

                // shrink it to last size
                return ret[r(0, currentIdx - 1), r(0, lastCol - startCol)];
            }
        }

        private static int parseCSVLine<T>(string line, OutArray<T> A, int rowID, int startCol, int endCol, Func<string, T> elementParser, string elementSeparator = ",")
        {
            if (string.IsNullOrEmpty(line)) 
                return 0;

            var items = line.Split(new string[] { elementSeparator}, StringSplitOptions.None); 
         
            if (!isnull(A)) {
                // parse into T
                for (int i = startCol; i < items.Length && i <= endCol; i++) {
                    T val = elementParser(items[i]);
                    A.SetValue(val, rowID, i - startCol);
                }
            }

            return items.Length;
        }
#endregion

    }
}