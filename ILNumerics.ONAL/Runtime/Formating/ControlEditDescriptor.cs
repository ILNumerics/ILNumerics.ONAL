using System;
using System.Collections.Generic;
using System.Text;
#pragma warning disable CS1591

namespace ILNumerics.F2NET.Formatting {
    public class ControlEditDescriptor : FormatItem {

        public EditDescriptorManner Manner { get; set; }
        public int? K { get; set; }

        public ControlEditDescriptor(EditDescriptorManner manner, int? k = null, int repeat = 1, bool reverted = false) : base(repeat, reverted) {
            Manner = manner;
            K = k;
        }

        public override string ToString() {
            switch (Manner) {
                case EditDescriptorManner.Slash:
                    return $"{RepeatFactor}/";
                case EditDescriptorManner.Colon:
                    return ":";
                case EditDescriptorManner.S:
                case EditDescriptorManner.SP:
                case EditDescriptorManner.SS:
                case EditDescriptorManner.BN:
                case EditDescriptorManner.BZ:
                    return $"{Manner}";
                case EditDescriptorManner.P:
                    return $"{K}P"; 
                default:
                    return $"{nameof(ControlEditDescriptor)} - unknown format.";

            }
        }
        public override FormatItem Clone() {
            return new ControlEditDescriptor(this);
        }

        protected ControlEditDescriptor(ControlEditDescriptor source) : base(source) {
            this.Manner = source.Manner;
            this.K = source.K; 
        }

    }
}
