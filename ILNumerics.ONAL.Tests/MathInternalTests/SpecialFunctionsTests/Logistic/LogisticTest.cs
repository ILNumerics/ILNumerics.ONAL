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
    public class LogisticTest 
    {
        [TestMethod]
        public void logisticTestNull()
        {
            using (Scope.Enter())
            {
                Array<double> x = null;
                try
                {
                    x = logistic(x);
                    Assert.Fail("The logistic function Method failed to throw the ArgumentNullException for null input value");
                }
                catch (ArgumentNullException exc)
                {
                    Assert.IsTrue(exc.Message.Contains("null"), "The logistic function Method failed to throw the ArgumentNullException for null input value");
                }
            }
        }
        [TestMethod]
        public void logisticTestCoeffZeros()
        {
            using (Scope.Enter())
            {
                Array<double> x = zeros(1, 10);
                x = logistic(x);
                Assert.IsTrue(maxall(x) == 0.5, "The logistic function failed to throw the ArgumentOutOfRangeException for zeros input value");
            }
        }
        [TestMethod]
        public void logisticTestCoeffOnes()
        {
            using (Scope.Enter())
            {
                Array<double> x = ones(1, 10);
                x[2] = 0.5;
                x[4] = 0.25;
                x[6] = 0.75;
                x = logistic(x);
                Array<double> Sol = logistic(1.0) * ones(1, 10);
                Sol[2] = logistic(0.5);
                Sol[4] = logistic(0.25);
                Sol[6] = logistic(0.75);
                Assert.IsTrue(isequalwithequalnans(x, Sol), "The logistic Method failed to throw the ArgumentOutOfRangeException for zeros input value");
            }
        }
        [TestMethod]
        public void logisticTestBigCoeffOnes()
        {
            using (Scope.Enter())
            {
                Array<double> x = 5.0 * ones(1, 10);
                x[2] = 0.5;
                x[4] = 4.25;
                x[6] = 0.75;
                x = logistic(x);
                Array<double> Sol = logistic(5.0) * ones(1, 10);
                Sol[2] = logistic(0.5);
                Sol[4] = logistic(4.25);
                Sol[6] = logistic(0.75);
                Assert.IsTrue(isequalwithequalnans(x, Sol), "The logistic Method failed to throw the ArgumentOutOfRangeException for zeros input value");
            }
        }

        [TestMethod]
        public void LogisticTestShape()
        {
            Assert.AreEqual(0, logistic(double.NegativeInfinity));
            Assert.AreEqual(0, logistic(-1000));
            Assert.AreEqual(0, (double)logistic(-100), 1e-10);
            
            Assert.AreEqual(1, (double)logistic(+100), 1e-10);
            Assert.AreEqual(1, logistic(+1000));
            Assert.AreEqual(1, logistic(double.PositiveInfinity));

            Assert.AreEqual(0.5, (double)logistic(0), 1e-10);
            Assert.IsTrue(0.5 > (double)logistic(-0.01));
            Assert.IsTrue(0.5 < (double)logistic(+0.01));

            Assert.IsTrue(logistic(-10) < logistic(-8));
            Assert.IsTrue(logistic(-8) < logistic(-6));
            Assert.IsTrue(logistic(-6) < logistic(-4));
            Assert.IsTrue(logistic(-4) < logistic(-2));
            Assert.IsTrue(logistic(-2) < logistic(0));
            Assert.IsTrue(logistic(0) < logistic(+2));
            Assert.IsTrue(logistic(+2) < logistic(+4));
            Assert.IsTrue(logistic(+4) < logistic(+6));
            Assert.IsTrue(logistic(+6) < logistic(+8));
            Assert.IsTrue(logistic(+8) < logistic(+10));
        }

        [TestMethod]
        public void LogisticTestInverse()
        {
            Array<double> x = linspace<double>(-10, 10, 1000);
            Array<double> sol = logistic(x);
            Array<double> inv = logit(sol);

            Assert.IsTrue(sumall(inv - x) < 1e-6);
        }
    }
}
