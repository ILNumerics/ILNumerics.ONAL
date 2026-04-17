using System;
using System.Collections.Generic;
using System.Text;

namespace ILNumerics.Core.Runtime {
    public class FEndOfFileException : Exception {

        public int FUnit { get; set; }

        public FEndOfFileException(int funit) : base($"The file attached to unit {funit} has reached an EOF condition.") {
            FUnit = funit;
        }
    }
}
