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
    public class BetaFunctionTest 
    {
        [TestMethod]
        public void TestInputScalarbeta()
        {
            Assert.IsTrue((beta(25, 23) - (factorial(24) * factorial(22)) / factorial(47)) < 1e-5, "Should be the same result");
            Assert.IsTrue((beta(25, 1) - (factorial(24)) / factorial(25)) < 1e-7, "Should be the same result");
            Assert.IsTrue(isposinf(beta(0, 100)), "Should return +Inf");
            Assert.IsTrue(isposinf(beta(100, 0)), "Should return +Inf");
            Assert.IsFalse(isposinf(beta(-10, 100)), "Should not return +Inf");
            Assert.IsFalse(isposinf(beta(100, -10)), "Should not return +Inf");
        }

        [TestMethod]
        public void TestInputArraybeta()
        {
            Array<int> A = 5 * ones<int>(1000, 200);
            Array<int> B = 3 * ones<int>(1000, 200);
            Array<double> combiAB = beta(5,3) * ones<double>(1000, 200);
            Assert.IsTrue(beta(todouble(A), todouble(B)).Equals(combiAB), "Should return a matrix of 10");
        }

       [TestMethod]
        public void TestInputArrayCounterbeta()
        {
            Array<int> A = counter<int>(1, 1, 5, 2);
            Array<int> B = counter<int>(-3, 1, 5, 2);
            Array<double> conbiAB = vector<double>((double)beta(todouble(A[0, 0]), todouble(B[0, 0])), (double)beta(todouble(A[1, 0]), todouble(B[1, 0])), 
                (double)beta(todouble(A[2, 0]), todouble(B[2, 0])), (double)beta(todouble(A[3, 0]), todouble(B[3, 0])),
                (double)beta(todouble(A[4, 0]), todouble(B[4, 0])), (double)beta(todouble(A[0, 1]), todouble(B[0, 1])), 
                (double)beta(todouble(A[1, 1]), todouble(B[1, 1])), (double)beta(todouble(A[2, 1]), todouble(B[2, 1])),
                (double)beta(todouble(A[3, 1]), todouble(B[3, 1])), (double)beta(todouble(A[4, 1]), todouble(B[4, 1]))).Reshape(5, 2);
            Assert.IsTrue(isequalwithequalnans(beta(todouble(A), todouble(B)), conbiAB), "Should return matrix of betas");
        }
        
        [TestMethod]
        public void TestInputArrayNaNbeta()
        {
            Array<int> A = 6 * ones<int>(5, 5);
            A[4, 4] = -2000;
            A[3, 3] = -20;
            A[2, 2] = -30;
            A[1, 1] = -10;
            A[0, 0] = -10;
            Array<int> B = 6 * ones<int>(5, 5);
            B[4, 4] = -2000;
            B[3, 3] = -20;
            B[2, 2] = 30;
            B[1, 1] = -10;
            B[0, 0] = -10;
            Array<double> combiAB = array<double>(new double[] { double.NaN, (double)beta(todouble(A[1,0]),todouble(B[1, 0])), (double)beta(todouble(A[2,0]),todouble(B[2, 0])),
                (double)beta(todouble(A[3,0]),todouble(B[3, 0])), (double)beta(todouble(A[4,0]),todouble(B[4, 0])),
                (double)beta(todouble(A[0, 1]),todouble(B[0, 1])), double.NaN, (double)beta(todouble(A[2, 1]),todouble(B[2, 1])),
                (double)beta(todouble(A[3, 1]),todouble(B[3, 1])), (double)beta(todouble(A[4, 1]),todouble(B[4, 1])),
                (double)beta(todouble(A[0, 2]),todouble(B[0, 2])), (double)beta(todouble(A[1, 2]),todouble(B[1, 2])), double.NaN,
                (double)beta(todouble(A[3, 2]),todouble(B[3, 2])), (double)beta(todouble(A[4, 2]),todouble(B[4, 2])),
                (double)beta(todouble(A[0, 3]),todouble(B[0, 3])), (double)beta(todouble(A[1, 3]),todouble(B[1, 3])), 
                (double)beta(todouble(A[2, 3]),todouble(B[2, 3])), double.NaN, 
                (double)beta(todouble(A[4, 3]),todouble(B[4, 3])),(double)beta(todouble(A[0, 4]),todouble(B[0, 4])),
                (double)beta(todouble(A[1, 4]),todouble(B[1, 4])), (double)beta(todouble(A[2, 4]),todouble(B[2, 4])),
                (double)beta(todouble(A[3, 4]),todouble(B[3, 4])), double.NaN}, 5, 5);
            Assert.IsTrue(isequalwithequalnans(beta(todouble(A), todouble(B)), combiAB), "Should return  the componentwise beta inccluding NaN");
        }

        [TestMethod]
        public void TestBetaShape()
        {
            // zero -> +inf (nan?)
            Assert.IsTrue(isinf(beta(0, 1)));
            Assert.IsTrue(isinf(beta(1, 0)));
            Assert.IsTrue(isnan(beta(0, -1)));
            Assert.IsTrue(isnan(beta(-1, 0)));

            // around (0,0) it is high
            // moving in each direction to the positive half of R will decrease it
            double center = beta(0.25, 0.25).GetValue(0);
            double center1Axis = beta(0.25, 3).GetValue(0); // should be also symmetric
            double center2Axis = beta(3, 0.25).GetValue(0);
            double bothDir = beta(5, 5).GetValue(0);

            Assert.IsTrue(center > center1Axis);
            Assert.IsTrue(center > center2Axis);
            Assert.AreEqual(center1Axis, center2Axis, 1e-6);
            Assert.IsTrue(bothDir < center);

            // check around negative directions - it is not supported: NaN
            double centern = beta(-0.8, -0.8).GetValue(0);
            double center1Axisn = beta(-0.8, -0.5).GetValue(0); 
            double center2Axisn = beta(-0.5, -0.8).GetValue(0);
            double bothDirn = beta(-0.4, -0.4).GetValue(0);

            Assert.IsTrue(isnan(centern));
            Assert.IsTrue(isnan(center1Axisn));
            Assert.IsTrue(isnan(center2Axisn));
            Assert.IsTrue(isnan(bothDirn));
        }
    }
}
