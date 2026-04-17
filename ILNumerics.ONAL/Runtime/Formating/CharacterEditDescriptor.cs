using System;
using System.Collections.Generic;
using System.Text;
#pragma warning disable CS1591

namespace ILNumerics.F2NET.Formatting {
    public class CharacterEditDescriptor : FormatItem {

        public string Literal { get; set; }

        public CharacterEditDescriptor(string literal, bool reverted = false) : base(1, reverted: reverted) {
            Literal = literal;
        }
        public CharacterEditDescriptor(bool reverted, params char [] chars) : base(1, reverted) {
            Literal = new string(chars);
        }

        public override string ToString() {
            return $"'{Literal}'";
        }

        public override FormatItem Clone() {
            return new CharacterEditDescriptor(this); 
        }

        protected CharacterEditDescriptor(CharacterEditDescriptor source) : base(source) {
            this.Literal = source.Literal; 
        }
    }
}
