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
