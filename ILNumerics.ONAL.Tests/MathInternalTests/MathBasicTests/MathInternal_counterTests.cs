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

namespace ILNumerics.Core.UnitTests.MathInternalTests {
    [TestClass]
    public class MathInternal_counterTests {
        [TestMethod]
        public void MathInternal_counter_operatorDblTest() {
            // (this test is redundant with a ___sum_ test)

            Array<double> A = counter(1.0, 1.0, 5, 4); // new[] { 10, 20 }; // 

            Array<double> B = sum(A, 0);
            Array<double> Res = new double[] { 15, 40, 65, 90 };
            Assert.IsTrue(Res.Equals(B.T));
        }
        [TestMethod]
        public void MathInternal_counter_operatorDblParallelTest() {

            Array<double> A = 1; 
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 4u)) {
                A.a = counter(1.0, 1.0, 500, 4);
            }
            Array<double> B = sum(A, 0);
            Array<double> Res = new double[] { 125250, 375250, 625250, 875250 };
            Assert.IsTrue(Res.Equals(B.T));
        }
    }
}
