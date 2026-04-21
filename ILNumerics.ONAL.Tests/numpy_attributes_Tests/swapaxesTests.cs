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
using static ILNumerics.ILMath;

namespace ILNumerics.Core.UnitTests.numpy_attributes_Tests {

    [TestClass]
    public class swapaxesTests {
        [TestMethod]
        public void numpy_swapaxes_simple() {

            Array<double> A = counter<double>(1, 1, 5, 4, 3);

            Array<double> B = A.swapaxes(0, 1);

            Assert.IsTrue(A.S[0] == 5);
            Assert.IsTrue(A.S[1] == 4);
            Assert.IsTrue(A.S[2] == 3);
            Assert.IsTrue(A.S.NumberOfDimensions == 3);
            Assert.IsTrue(A.S.NumberOfElements == 5 * 4 * 3);

            Assert.IsTrue(B.S[0] == 4);
            Assert.IsTrue(B.S[1] == 5);
            Assert.IsTrue(B.S[2] == 3);
            Assert.IsTrue(B.S.NumberOfDimensions == 3);
            Assert.IsTrue(B.S.NumberOfElements == 5 * 4 * 3);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void numpy_swapaxes_OORfail() {
            Array<float> A = reshape<float>(vector<float>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10), 2, 1, 5);

            A.swapaxes(0, 4); 
        }
        [TestMethod]
        public void numpy_swapaxes_RetRelease() {
            Array<float> A = linspace<float>(1, 10, 10) + vector(1f, 10f);

            Assert.IsTrue(A.S[0] == 2);
            Assert.IsTrue(A.S[1] == 10);

            var t = A.T;
            var s = t.swapaxes(0,1); // this would return the same storage as A.T. Edit, v7: not anymore (implementation detail).


            Assert.IsTrue(s.S[0] == 2);
            Assert.IsTrue(s.S[1] == 10);
        }


    }
}
