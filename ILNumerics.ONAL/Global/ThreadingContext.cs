namespace ILNumerics.Core.Segments {
    /// <summary>
    /// This class collects settings and configuration values for segment execution and for quick distribution to other threads.
    /// </summary>
    public class ThreadingContext {

        // settings from ILNumerics.Settings
        private uint m_maxNumberThreads;
        private ArrayStyles m_arrayStyle;
        private bool? m_saturateIntegerOps;
        private bool? m_threadIgnoreSegmentLocks;
        private bool? m_threadBinLoggingEnabled;
        internal ThreadingContext() { }
        public uint MaxNumberThreads { 
            get { return m_maxNumberThreads; } 
            set { 
                m_maxNumberThreads = value;
                Settings.UpdateMaxNumberThreads(); 
            } 
        }
        public ArrayStyles ArrayStyle { 
            get { return m_arrayStyle; } 
            set { m_arrayStyle = value; } 
        }
        public bool? SaturateIntegerOps { 
            get { return m_saturateIntegerOps; } 
            set { m_saturateIntegerOps = value; } 
        }
        public bool? ThreadIgnoreSegmentLocks { 
            get { return m_threadIgnoreSegmentLocks; } 
            set { m_threadIgnoreSegmentLocks = value; } 
        }
        public bool? ThreadBinLoggingEnabled { 
            get { return m_threadBinLoggingEnabled; } 
            set { m_threadBinLoggingEnabled = value; } 
        }

        public ThreadingContext Copy() {
            return new ThreadingContext() {
                m_arrayStyle = m_arrayStyle,
                m_saturateIntegerOps = m_saturateIntegerOps,
                m_threadIgnoreSegmentLocks = m_threadIgnoreSegmentLocks,
                m_threadBinLoggingEnabled = m_threadBinLoggingEnabled,
                m_maxNumberThreads = m_maxNumberThreads
            };
        }

        public void Activate() {
            // Settings.ThreadStatic.Value.CopyFrom(this); 
            Settings.ThreadStatic.CopyFrom(this);   // reuse this instance! It should be ok and never affect the main thread ... ?! 
        }

        internal void CopyFrom(ThreadingContext other) {
            m_arrayStyle = other.m_arrayStyle;
            m_saturateIntegerOps = other.m_saturateIntegerOps;
            m_threadIgnoreSegmentLocks = other.m_threadIgnoreSegmentLocks;
            m_threadBinLoggingEnabled = other.m_threadBinLoggingEnabled;
            m_maxNumberThreads = other.m_maxNumberThreads;
        }

        internal void Reset() {
            CopyFrom(Settings.ThreadStatic);
            MaxNumberThreads = 1;  // default for all pipelined processing! 
        }
    }
}
