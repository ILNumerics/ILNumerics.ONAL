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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public class ArrayGetArrayForReadTests {

        [TestMethod]
        public void ArrayGetArrayForReadTestempty() {
            Array<short> A = new short[0];

            var arr = A.GetArrayForRead();

            Assert.IsTrue(arr != null);
            Assert.IsTrue(arr.Rank == 1);
            Assert.IsTrue(arr.LongLength == 0); 


        }

        [TestMethod]
        public void ArrayGetArrayForReadTestColumnMajor() {

            float[] cols = new float[0], rows = null; 
            Array<float> A = Helper.generateSystemArray<float>(500,203, ref rows, ref cols);
            
            var arr = A.GetArrayForRead();

            ArrayAssert.ValuesEqual(arr, cols); 

            Assert.IsTrue(arr != null);
            Assert.IsTrue(arr.Rank == 1);
            Assert.IsTrue(arr.LongLength == A.S.NumberOfElements);

        }

    }
}
