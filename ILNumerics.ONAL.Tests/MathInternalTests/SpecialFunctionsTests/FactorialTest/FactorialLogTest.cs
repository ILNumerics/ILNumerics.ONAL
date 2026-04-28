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
using ILNumerics; 
using static ILNumerics.Globals;
using static ILNumerics.ILMath;
namespace SpecialFunctionsTest
{
    [TestClass]
    public class FactorialLogTest
    {
        [TestMethod]
        public void TestLogInputScalar()
        {
            Assert.AreEqual(factorialLog(5).GetValue(0), Math.Log(120), 0.001, "The factorialLog of 5 should return 4.7xxx");
            Assert.AreEqual(factorialLog(0), 0, "The factorialLog of 0 should return 0");
            Assert.IsTrue(isnan(factorialLog(-5)), "The factorialLog of a negative number should return NaN");
            Assert.IsFalse(isposinf(factorialLog(170)), "The factorialLog of a 170 should not return +Inf");
            Assert.IsFalse(isposinf(factorialLog(171)), "The factorialLog of a 171 should not return +Inf");
        }

        [TestMethod]
        public void TestLogInputArray()
        {
            Array<int> B = 5 * ones<int>(1000, 200);
            Array<double> FactB = Math.Log(120) * ones<double>(1000, 200);
            Array<double> logFact = factorialLog(B);
            double diff = sumall(logFact - FactB).GetValue(0);
            Assert.IsTrue(diff <= 1e-4, "Should return a vector of 120");
        }

        [TestMethod]
        public void TestLogInputArrayCounter()
        {
            Array<int> B = counter<int>(1, 1, 5, 2);
            Array<double> FactB = vector((double)factorialLog(1), (double)factorialLog(2), 
                (double)factorialLog(3), (double)factorialLog(4), 
                (double)factorialLog(5), (double)factorialLog(6), 
                (double)factorialLog(7), (double)factorialLog(8), 
                (double)factorialLog(9), (double)factorialLog(10)).Reshape(5, 2);

            Assert.IsTrue(isequalwithequalnans(factorialLog(B), FactB), "Should return componentwise factorials");
        }

        [TestMethod]
        public void TestLogInputArrayNaN()
        {
            Array<int> B = 6 * ones<int>(5, 5);
            B[4, 4] = -2000;
            B[3, 3] = -20;
            B[2, 2] = -30;
            B[1, 1] = -10;
            B[0, 0] = -10;
            Array<double> FactB = vector(
                 double.NaN, (double)factorialLog(B[0, 1]),
                (double)factorialLog(B[0, 2]), (double)factorialLog(B[0, 3]), 
                (double)factorialLog(B[0, 4]),
                (double)factorialLog(B[1, 0]), double.NaN, 
                (double)factorialLog(B[1, 2]), (double)factorialLog(B[1, 3]), 
                (double)factorialLog(B[1, 4]),
                (double)factorialLog(B[2, 0]), (double)factorialLog(B[2, 1]),
                double.NaN, (double)factorialLog(B[2, 3]), (double)factorialLog(B[2, 4]),
                (double)factorialLog(B[3, 0]), (double)factorialLog(B[3, 1]), 
                (double)factorialLog(B[3, 2]), double.NaN, (double)factorialLog(B[3, 4]),
                (double)factorialLog(B[4, 0]), (double)factorialLog(B[4, 1]), 
                (double)factorialLog(B[4, 2]), (double)factorialLog(B[4, 3]), double.NaN).Reshape(5, 5);
            Assert.IsTrue(isequalwithequalnans(factorialLog(B), FactB), "Should return  the componentwise factorials with NaN");
        }

        [TestMethod]
        public void TestLogInputNegativeWithNaN()
        {
            Array<int> C = counter<int>(-30, 1, 5, 5);
            Array<double> FactC = vector((double)factorialLog(C[0, 0]), (double)factorialLog(C[1, 0]),
                (double)factorialLog(C[2, 0]), (double)factorialLog(C[3, 0]),
                (double)factorialLog(C[4, 0]), (double)factorialLog(C[0, 1]), 
                (double)factorialLog(C[1, 1]), (double)factorialLog(C[2, 1]), 
                (double)factorialLog(C[3, 1]), (double)factorialLog(C[4, 1]), 
                (double)factorialLog(C[0, 2]), (double)factorialLog(C[1, 2]),
                (double)factorialLog(C[2, 2]), (double)factorialLog(C[3, 2]), 
                (double)factorialLog(C[4, 2]), (double)factorialLog(C[0, 3]), 
                (double)factorialLog(C[1, 3]), (double)factorialLog(C[2, 3]), 
                (double)factorialLog(C[3, 3]), (double)factorialLog(C[4, 3]), 
                (double)factorialLog(C[0, 4]), (double)factorialLog(C[1, 4]), 
                (double)factorialLog(C[2, 4]), (double)factorialLog(C[3, 4]),
                (double)factorialLog(C[4, 4])).Reshape(5, 5);
            Assert.IsTrue(isequalwithequalnans(factorialLog(C), FactC), "Should return  the componentwise factorials with NaN");
        }
    }
}
