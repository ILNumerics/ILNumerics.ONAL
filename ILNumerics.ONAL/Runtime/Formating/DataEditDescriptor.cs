using System;
using System.Collections.Generic;
using System.Text;
#pragma warning disable CS1591

namespace ILNumerics.F2NET.Formatting {
    public class DataEditDescriptor : FormatItem {

        public EditDescriptorManner Manner { get; set; }
        public int? W { get; set; }
        public int? D { get; set; }
        public int? E { get; set; }
        public int? M { get; set; }

        public DataEditDescriptor(int repeat, EditDescriptorManner manner, int? w, int? d, int? e, int? m, bool reverted = false) : base(repeat, reverted) {
            Manner = manner;
            if (manner != EditDescriptorManner.A && !w.HasValue) {
                throw new ArgumentException($"Format descriptor: missing W argument. It must be positive provided for all but the A descriptor."); 
            }
            if ((manner != EditDescriptorManner.A && w < 0) || e < 0) throw new ArgumentException($"w and e in a data-edit-desc must be positive (see: spec 90, 10.2.1)."); 
            W = w;
            D = d;
            E = e;
            M = m; 
        }
        public override string ToString() {
            switch (Manner) {
                case EditDescriptorManner.I:
                case EditDescriptorManner.B:
                case EditDescriptorManner.O:
                case EditDescriptorManner.Z:
                    return $"{RepeatFactor}{Manner}{W}{(M.HasValue? $".{M}" : "")}";

                case EditDescriptorManner.F:
                    return $"{RepeatFactor}{Manner}{W}.{D}";
                case EditDescriptorManner.E:
                case EditDescriptorManner.EN:
                case EditDescriptorManner.ES:
                case EditDescriptorManner.G:
                    return $"{RepeatFactor}{Manner}{W}.{D}{(E.HasValue ? $"E{E}" : "")}";
                case EditDescriptorManner.L:
                    return $"{RepeatFactor}{Manner}{W}";
                case EditDescriptorManner.A:
                    return $"{RepeatFactor}{Manner}{(W.HasValue? $"{W}" : "")}";
                case EditDescriptorManner.D:
                    return $"{RepeatFactor}{Manner}{W}.{D}";
                case EditDescriptorManner.Asterisc:
                    return $"{RepeatFactor} -List Directed-";
                default:
                    return $"{nameof(DataEditDescriptor)} - unknown format."; 
            }
        }
        public override FormatItem Clone() {
            return new DataEditDescriptor(this);
        }

        protected DataEditDescriptor(DataEditDescriptor source) : base(source) {
            this.Manner = source.Manner;
            this.W = source.W;
            this.D = source.D;
            this.E = source.E;
            this.M = source.M;
        }


    }
}
