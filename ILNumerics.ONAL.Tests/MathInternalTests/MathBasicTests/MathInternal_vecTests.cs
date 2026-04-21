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
    public class MathInternal_vecTests {

        [TestMethod]
        public void MathInternal_vec_simple() {

            Array<double> A = arange(1.0, 4.0); 
            Assert.IsTrue(A.Equals(vector<double>(1, 2, 3, 4)));
        }
        [TestMethod]
        public void MathInternal_vec_rev() {

            Array<double> A = arange(4.0, -1, 1.0);
            Assert.IsTrue(A.Equals(vector<double>(4, 3, 2, 1)));
        }
        [TestMethod]
        public void MathInternal_vec_lgFloat() {

            Array<float> A = arange(10000f, -1, 0);
            Assert.IsTrue(A.Equals(counter<float>(10000, -1, 10001)));
        }
        [TestMethod]
        public void MathInternal_vec_empty() {

            Assert.IsTrue(arange(1.0, 1, 1.0).Equals(vector<double>(1)));
            Assert.IsTrue(arange(1.0, 1, 1.0).Equals(vector<double>(1)));
        }
    }
}
