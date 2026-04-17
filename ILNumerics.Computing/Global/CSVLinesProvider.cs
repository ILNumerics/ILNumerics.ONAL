using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ILNumerics.Core.Misc
{
    /// <summary>
    /// Class to hide Stream / String passed behavior for reading CSV lines iteratively.
    /// </summary>
    internal class CSVLinesProvider<T> : IDisposable
    {
        private string[] lines;
        private StreamReader reader;
        private int currentLine;
        private bool leaveStreamOpen; 

        public CSVLinesProvider(string fromAllText)
        {
            this.lines = fromAllText.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public CSVLinesProvider(Stream fromStream, Encoding encoding = null, bool leaveStreamOpen = false)
        {
            this.reader = new StreamReader(fromStream, encoding == null ? Encoding.ASCII : encoding);
            this.leaveStreamOpen = leaveStreamOpen; 
        }

        /// <summary>
        /// Gets the next line with hiding its origin.
        /// If it is null, the text is reached the end.
        /// </summary>
        /// <returns>Next trimmed line.</returns>
        public bool GetNextLine(out string line)
        {
            bool ret = false;
            line = null;
            if (this.reader != null)
            {
                if (this.reader.Peek() >= 0)
                {
                    line = this.reader.ReadLine().Trim();
                    ret = true;
                }
            }
            else
            {
                try
                {
                    line = lines[currentLine++].Trim();
                    ret = true;
                }
                catch (IndexOutOfRangeException)
                {
                    // end of file
                }
            }
            return ret;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.reader != null && !this.leaveStreamOpen) {
                    this.reader.Dispose();
                }
            }
        }
    }

}
