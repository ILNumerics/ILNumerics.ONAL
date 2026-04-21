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
    public class FactorialTest
    {
        [TestMethod]
        public void TestInputScalar()
        {
            Assert.IsTrue(factorial(5) == 120, "The factorial of 5 should return 120");
            Assert.IsTrue(factorial(0) == 1, "The factorial of 0 should return 1");
            Assert.IsTrue(isnan(factorial(-5)), "The factorial of a negative number should return NaN");
            Assert.IsFalse(isposinf(factorial(170)), "The factorial of a 170 should not return +Inf");
            Assert.IsTrue(isposinf(factorial(171)), "The factorial of a 171 should return +Inf");
        }

        [TestMethod]
        public void TestInputArray()
        {
            Array<int> B = 5 * ones<int>(1000, 200);
            Array<double> FactB = 120 * ones<double>(1000, 200);
            Assert.IsTrue(factorial(B).Equals(FactB), "Should return a vector of 120");
        }

        [TestMethod]
        public void TestInputArrayCounter()
        {
            Array<int> B = counter<int>(1, 1, 5, 2);
            Array<double> FactB = vector(new double[] { (double)factorial(1), (double)factorial(2), 
                (double)factorial(3), (double)factorial(4), 
                (double)factorial(5), (double)factorial(6), 
                (double)factorial(7), (double)factorial(8), 
                (double)factorial(9), (double)factorial(10) }).Reshape(5, 2);
            Assert.IsTrue(isequalwithequalnans(factorial(B), FactB), "Should return componentwise factorials");
        }

        [TestMethod]
        public void TestInputArrayNaN()
        {
            Array<int> B = 6 * ones<int>(5, 5);
            B[4, 4] = -2000;
            B[3, 3] = -20;
            B[2, 2] = -30;
            B[1, 1] = -10;
            B[0, 0] = -10;
            Array<double> FactB = vector<double>(double.NaN, (double)factorial(B[0, 1]),
                (double)factorial(B[0, 2]), (double)factorial(B[0, 3]), 
                (double)factorial(B[0, 4]),
                (double)factorial(B[1, 0]), double.NaN, 
                (double)factorial(B[1, 2]), (double)factorial(B[1, 3]), 
                (double)factorial(B[1, 4]),
                (double)factorial(B[2, 0]), (double)factorial(B[2, 1]),
                double.NaN, (double)factorial(B[2, 3]), (double)factorial(B[2, 4]),
                (double)factorial(B[3, 0]), (double)factorial(B[3, 1]), 
                (double)factorial(B[3, 2]), double.NaN, (double)factorial(B[3, 4]),
                (double)factorial(B[4, 0]), (double)factorial(B[4, 1]), 
                (double)factorial(B[4, 2]), (double)factorial(B[4, 3]), double.NaN).Reshape(5, 5);
            Assert.IsTrue(isequalwithequalnans(factorial(B), FactB), "Should return  the componentwise factorials with NaN");
        }

        [TestMethod]
        public void TestInputNegativeWithNaN()
        {
            Array<int> C = counter<int>(-30, 1, 5, 5);
            Array<double> FactC = vector<double>( (double)factorial(C[0, 0]), (double)factorial(C[1, 0]),
                (double)factorial(C[2, 0]), (double)factorial(C[3, 0]),
                (double)factorial(C[4, 0]), (double)factorial(C[0, 1]), 
                (double)factorial(C[1, 1]), (double)factorial(C[2, 1]), 
                (double)factorial(C[3, 1]), (double)factorial(C[4, 1]), 
                (double)factorial(C[0, 2]), (double)factorial(C[1, 2]),
                (double)factorial(C[2, 2]), (double)factorial(C[3, 2]), 
                (double)factorial(C[4, 2]), (double)factorial(C[0, 3]), 
                (double)factorial(C[1, 3]), (double)factorial(C[2, 3]), 
                (double)factorial(C[3, 3]), (double)factorial(C[4, 3]), 
                (double)factorial(C[0, 4]), (double)factorial(C[1, 4]), 
                (double)factorial(C[2, 4]), (double)factorial(C[3, 4]),
                (double)factorial(C[4, 4])).Reshape(5, 5);
            Assert.IsTrue(isequalwithequalnans(factorial(C), FactC), "Should return  the componentwise factorials with NaN");
        }
    }
}
