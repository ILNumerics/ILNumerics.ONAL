using System;
using System.Collections.Generic;
using System.Text;
#pragma warning disable CS1591

namespace ILNumerics.F2NET.Formatting {
    public class PositionEditDescriptor : FormatItem {

        public EditDescriptorManner Manner { get; set; }
        public int N { get; set; }

        public PositionEditDescriptor(EditDescriptorManner manner, int n, bool reverted = false) : base(1, reverted) {
            Manner = manner;
            N = n; 
        }

        public override string ToString() {
            switch (Manner) {
                case EditDescriptorManner.T:
                case EditDescriptorManner.TL:
                case EditDescriptorManner.TR:
                    return $"{Manner}{N}";
                case EditDescriptorManner.X:
                    return $"{N}X"; 
                default:
                    return $"{nameof(PositionEditDescriptor)} - unknown format.";
            }
        }
        public override FormatItem Clone() {
            return new PositionEditDescriptor(this);
        }

        protected PositionEditDescriptor(PositionEditDescriptor source) : base(source) {
            this.Manner = source.Manner;
            this.N = source.N;
        }


    }
}
