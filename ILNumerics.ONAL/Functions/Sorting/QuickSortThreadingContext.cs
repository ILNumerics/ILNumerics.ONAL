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
