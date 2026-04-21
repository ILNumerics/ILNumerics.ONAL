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
using static ILNumerics.Globals; 

namespace ILNumerics.Core.UnitTests.MathInternalTests {

    [TestClass]
    public class MathInternal_diffTests {
        [TestMethod]
        public void mathInternal_Diff_simpleTest() {

            Array<double> A = counter<double>(1.0, 1.0, 5, 4);
            Array<double> B = ones<double>(4, 4);

            Assert.IsTrue(B.Equals(diff(A)));

            Assert.IsTrue(diff(A, dim: 1).Equals(ones<double>(5, 3) + 4));
        }

        [TestMethod]
        public void MathInternal_Diff_NF0Test() {
            Array<uint> A = counter<uint>(1, 1, 4, 1);
            Assert.IsTrue(diff(A, 0).Equals(A));
            Assert.IsTrue(diff(A, 0, 0).Equals(A));
            Assert.IsTrue(diff(A, 0, 1).Equals(A));
        }
        [TestMethod]
        public void MathInternal_Diff_NF1Test() {
            Array<uint> A = counter<uint>(1, 1, 4, 1);

            Assert.IsTrue(diff(A).Equals(ones<uint>(3, 1)));
            Assert.IsTrue(diff(A, 1).Equals(ones<uint>(3, 1)));
            Assert.IsTrue(diff(A, 1, 0).Equals(ones<uint>(3, 1)));
            Assert.IsTrue(diff(A, 1, 1).Equals(ones<uint>(4, dim1: 0)));
        }
        [TestMethod]
        public void MathInternal_Diff_NF2Test() {
            Array<uint> A = counter<uint>(1, 2, 4, 2);

            Assert.IsTrue(diff(A, 1).Equals(ones<uint>(3, 2) * 2));
            Assert.IsTrue(diff(A, 2).Equals(zeros<uint>(2, 2)));
            Assert.IsTrue(diff(A, 2, 0).Equals(zeros<uint>(2, 2)));
            Assert.IsTrue(diff(A, 2, 1).Equals(zeros<uint>(4, dim1: 0)));
            Assert.IsTrue(diff(counter<uint>(1, 2, 4, 1), 2, 1).Equals(zeros<uint>(4, dim1: 0)));
        }
        [TestMethod]
        public void MathInternal_Diff_NF3u4Test() {
            Array<uint> A = counter<uint>(1, 2, 4, 2);

            Assert.IsTrue(diff(A, 1).Equals(ones<uint>(3, 2) * 2));
            Assert.IsTrue(diff(A, 2).Equals(zeros<uint>(2, 2)));
            Assert.IsTrue(diff(A, 3, 0).Equals(zeros<uint>(1, 2)));
            Assert.IsTrue(diff(A, 4, 0).Equals(zeros<uint>(0, 2)));
            Assert.IsTrue(diff(A, 3, 1).Equals(zeros<uint>(4, dim1: 0)));
            Assert.IsTrue(diff(A, 4, 1).Equals(zeros<uint>(4, dim1: 0)));
        }
        [TestMethod]
        public void MathInternal_Diff_N4PolyTest() {
            Array<uint> A = counter<uint>(0, 1, 5, 4);
            Array<uint> A3 = A * A * A;
            Array<uint> A2 = A * A;
            Assert.IsTrue(diff(A + 2 * A2 + 3 * A3, 3).Equals(ones<uint>(2,4) * 18));
        }
    }
}
