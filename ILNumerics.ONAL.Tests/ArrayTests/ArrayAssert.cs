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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.InteropServices;

namespace ILNumerics.Core.UnitTests {
    public class ArrayAssert { 

        public static unsafe void ValuesEqual<T>(Array a, T[] b) {

            Assert.IsTrue(a != null && b != null);
            Assert.IsTrue(a.LongLength == b.LongLength);  
            var aType = a.GetType().GetElementType();
            Assert.IsTrue(aType == b.GetType().GetElementType());

            var bSize = Marshal.SizeOf<T>();
            GCHandle hA = GCHandle.Alloc(a, GCHandleType.Pinned);
            GCHandle hB = GCHandle.Alloc(b, GCHandleType.Pinned); 
            try {
                byte* pA = (byte*)hA.AddrOfPinnedObject(); 
                byte* pB = (byte*)hB.AddrOfPinnedObject();
                for (ulong i = 0; i < (ulong) (bSize * a.LongLength); i++) {
                    Assert.IsTrue(pA[i] == pB[i], $"Bytes at position i={i}, element# {i / (ulong)bSize} are not equal. left:{pA[i]} right:{pB[i]}"); 
                }
            } finally {
                if (hA.IsAllocated) hA.Free(); 
                if (hB.IsAllocated) hB.Free(); 
            }
        }
    }
}
