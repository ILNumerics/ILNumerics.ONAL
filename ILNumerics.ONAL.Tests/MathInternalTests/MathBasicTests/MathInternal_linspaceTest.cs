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
    public class MathInternal_linspaceTest {
        [TestMethod]
        public void MathInternal_linspace_simpleTest() {

            Array<double> A = linspace<double>(1, 100.0);
            Array<double> Res = counter<double>(1.0, 1.0, 100);
            Array<double> I1 = 1, I2 = 100, I3 = 100; 
            
            Assert.IsTrue(A.Equals(Res)); 
            Assert.IsTrue(A.Equals(linspace<double>(1, 100, 100))); 
            Assert.IsTrue(A.Equals(linspace<double>(I1, 100, 100))); 
            Assert.IsTrue(A.Equals(linspace<double>(1, I2, 100)));
            Assert.IsTrue(A.Equals(linspace<double>(1, I2.C, I3.T)));
            Assert.IsTrue(A.Equals(linspace<double>(1, 100, I3)));
        }
        [TestMethod]
        public void MathInternal_linspace_emptyTest() {

            Array<double> A = linspace<double>(1, 100.0, 0);
            Array<double> Res = counter<double>(100.0, 1.0, 0);
            Array<double> I1 = 1, I2 = 100, I3 = 0;

            Assert.IsTrue(A.Equals(Res));
            Assert.IsTrue(A.Equals(linspace<double>(1, 100, 0)));
            Assert.IsTrue(A.Equals(linspace<double>(I1, 100, 0)));
            Assert.IsTrue(A.Equals(linspace<double>(1, I2, 0)));
            Assert.IsTrue(A.Equals(linspace<double>(1, I2.C, I3.T)));
            Assert.IsTrue(A.Equals(linspace<double>(1, 100, I3)));
        }
        [TestMethod]
        public void MathInternal_linspace_Len1Test() {

            Array<double> A = linspace<double>(1, 100.0, 1);
            Array<double> Res = counter<double>(100.0, 1.0, 1);
            Array<double> I1 = 1, I2 = 100, I3 = 1;

            Assert.IsTrue(A.Equals(Res));
            Assert.IsTrue(A.Equals(linspace<double>(1, 100, 1)));
            Assert.IsTrue(A.Equals(linspace<double>(I1, 100, 1)));
            Assert.IsTrue(A.Equals(linspace<double>(1, I2, 1)));
            Assert.IsTrue(A.Equals(linspace<double>(1, I2.C, I3.T)));
            Assert.IsTrue(A.Equals(linspace<double>(1, 100, I3)));
        }
        [TestMethod]
        public void MathInternal_linspace_UInt32Test() {

            Array<uint> A = linspace<uint>(1, 100, 1);
            Array<uint> Res = counter<uint>(100, 1, 1);
            Array<uint> I1 = 1, I2 = 100, I3 = 1;

            Assert.IsTrue(A.Equals(Res));
            Assert.IsTrue(A.Equals(linspace<uint>(1, 100, 1)));
            Assert.IsTrue(A.Equals(linspace<uint>(I1, 100, 1)));
            Assert.IsTrue(A.Equals(linspace<uint>(1, I2, 1)));
            Assert.IsTrue(A.Equals(linspace<uint>(1, I2.C, I3.T)));
            Assert.IsTrue(A.Equals(linspace<uint>(1, 100, I3)));
        }

    }
}
