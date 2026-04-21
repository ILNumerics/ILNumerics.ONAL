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
