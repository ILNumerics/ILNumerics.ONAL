using System;
using System.Collections.Generic;
using System.Text;
#pragma warning disable CS1591

namespace ILNumerics.F2NET.Formatting {
    /// <summary>
    /// Class representing simple tokens within Fortran format strings. Examples: I, /, ), 4...
    /// </summary>
    public class FormatToken {

        public string Token { get; set; } = "";
        public object Value { get; set; } = null;
        public int Start { get; set; } = -1;
    }
}