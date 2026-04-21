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
using ILNumerics;

namespace ILNumerics.Core.UnitTests.MathInternalTests {
    [TestClass]
    public class MathInternal_logspaceTest {
        [TestMethod]
        public void MathInternal_logspace_simpleTest() {

            Array<double> A = logspace(1, 5.0);
            Array<double> Res = pow(10, linspace<double>(1.0, 5.0, 50));
            Array<double> I1 = 1, I2 = 5, I3 = 50; 
            
            Assert.IsTrue(A.Equals(Res)); 
            Assert.IsTrue(A.Equals(logspace(1, 5, 50))); 
            Assert.IsTrue(A.Equals(logspace(I1, 5, 50))); 
            Assert.IsTrue(A.Equals(logspace(1, I2, 50)));
            Assert.IsTrue(A.Equals(logspace(1, I2.C, I3.T)));
            Assert.IsTrue(A.Equals(logspace(1, 5, I3)));
        }
        [TestMethod]
        public void MathInternal_logspace_emptyTest() {

            Array<double> A = logspace(1, 5.0, 0);
            Array<double> Res = counter<double>(5.0, 1.0, 0);
            Array<double> I1 = 1, I2 = 100, I3 = 0;

            Assert.IsTrue(A.Equals(Res));
            Assert.IsTrue(A.Equals(logspace(1, 100, 0)));
            Assert.IsTrue(A.Equals(logspace(I1, 100, 0)));
            Assert.IsTrue(A.Equals(logspace(1, I2, 0)));
            Assert.IsTrue(A.Equals(logspace(1, I2.C, I3.T)));
            Assert.IsTrue(A.Equals(logspace(1, 100, I3)));
        }
        [TestMethod]
        public void MathInternal_logspace_Len1Test() {

            Array<double> A = logspace(1, 5.0, 1);
            Array<double> Res = pow(10, counter<double>(5.0, 1.0, 1));
            Array<double> I1 = 1, I2 = 5, I3 = 1;

            Assert.IsTrue(A.Equals(Res));
            Assert.IsTrue(A.Equals(logspace(1, 5, 1)));
            Assert.IsTrue(A.Equals(logspace(I1, 5, 1)));
            Assert.IsTrue(A.Equals(logspace(1, I2, 1)));
            Assert.IsTrue(A.Equals(logspace(1, I2.C, I3.T)));
            Assert.IsTrue(A.Equals(logspace(1, 5, I3)));
        }
    }
}
