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
