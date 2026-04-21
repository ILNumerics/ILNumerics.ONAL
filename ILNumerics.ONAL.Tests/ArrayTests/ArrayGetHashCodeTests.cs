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
    public class ArrayGetHashCodeTests {
        [TestMethod]
        public void ArrayGetHashCodeTest() {

            Array<uint> A = new uint[,] {
                { 1, 2, 3 },
                { 4, 5, 6 }
            };

            Array<uint> B = new uint[,] {
                { 1, 2 },
                { 3, 4 },
                { 5, 6 }
            };
            var ha = A.GetHashCode();
            var hb = B.GetHashCode();
            var ha2 = A.GetHashCode();
            Assert.IsTrue(ha != 0);
            Assert.IsTrue(ha2 != 0);
            Assert.IsTrue(ha == ha2);

            Assert.IsTrue(hb != 0);
            Assert.IsTrue(ha != hb);

            Array<uint> C = A.C;
            Assert.IsTrue(ha == C.GetHashCode());

        }
        [TestMethod]
        public void ArrayGetHashCodeTestempty() {

            Array<double> A = new double[0];

            Assert.IsTrue(A.GetHashCode() != 0);

        }
    }
}
