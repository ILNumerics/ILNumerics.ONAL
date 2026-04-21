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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.Misc {
    /// <summary>
    /// This class provides a thread local set of helper data and is used by individual threads of the QuickSort algorithms.
    /// </summary>
    internal class QuickSortThreadingContext {

        [ThreadStatic]
        private static QuickSortThreadingContext s_instance; 
        internal static QuickSortThreadingContext Local {
            get {
                if (s_instance == null) {
                    s_instance = new QuickSortThreadingContext(); 
                }
                return s_instance; 
            }
        }


        private QSChunkDefinition[] m_quickSortChunks = new QSChunkDefinition[100];
        internal int QuickSortChunksCount = 0;
        internal void PushQuickSortChunk(long lo, long hi) {
            if (QuickSortChunksCount + 1 >= m_quickSortChunks.Length) {
                // must expand
                var newArray = new QSChunkDefinition[m_quickSortChunks.Length * 2];
                System.Diagnostics.Trace.WriteLine($"Expanding working buffer for quicksort. New size: {newArray.Length}.");
                Array.Copy(m_quickSortChunks, newArray, QuickSortChunksCount);
                m_quickSortChunks = newArray;
            }
            QSChunkDefinition.Set(ref m_quickSortChunks[QuickSortChunksCount++], lo: lo, hi: hi);
        }
        internal bool PopQuickSortChunk(out long lo, out long hi) {
            if (QuickSortChunksCount == 0) {
                lo = 0;
                hi = 0;
                return false;
            } else {
                m_quickSortChunks[--QuickSortChunksCount].Get(out lo, out hi);
                return true;
            }
        }

    }
}
