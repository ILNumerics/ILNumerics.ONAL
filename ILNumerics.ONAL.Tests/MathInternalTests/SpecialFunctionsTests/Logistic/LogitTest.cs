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
    public class LogitTest 
    {
        [TestMethod]
        public void logitTestNull()
        {
            using (Scope.Enter())
            {
                Array<double> x = null;
                try
                {
                    x = logit(x);
                    Assert.Fail("The logit function Method failed to throw the ArgumentNullException for null input value");
                }
                catch (ArgumentNullException exc)
                {
                    Assert.IsTrue(exc.Message.Contains("null"), "The logit function Method failed to throw the ArgumentNullException for null input value");
                }
            }
        }
        [TestMethod]
        public void logitTestCoeffZeros()
        {
            using (Scope.Enter())
            {
                Array<double> x = zeros(1, 10);
                x = logit(x);
                Assert.IsTrue(isneginf((double)x[0]), "The logit function failed to return -infinity for zeros as input value");
            }
        }
        [TestMethod]
        public void logitTestCoeffOnes()
        {
            using (Scope.Enter())
            {
                Array<double> x = ones(1, 10);
                x[2] = 0.5;
                x[4] = 0.25;
                x[6] = 0.75;
                x = logit(x);
                Array<double> Sol = logit(1.0) * ones(1, 10);
                Sol[2] = logit(0.5);
                Sol[4] = logit(0.25);
                Sol[6] = logit(0.75);
                Assert.IsTrue(isequalwithequalnans(x, Sol), "The logit Method failed to evaluate return infinity at 1");
            }
        }
        
        [TestMethod]
        public void logitTestBigCoeffOnes()
        {
            using (Scope.Enter())
            {
                Array<double> x = 5.0 * ones(1, 200);
                x[2] = 0.5;
                x[4] = 4.25;
                x[6] = 0.75;
                x = logit(x);
                Array<double> Sol = logit(5.0) * ones(1, 200);
                Sol[2] = logit(0.5);
                Sol[4] = logit(4.25);
                Sol[6] = logit(0.75);
                Assert.IsTrue(isequalwithequalnans(x, Sol), "The logit Method failed to evaluate pointwise  an array");
            }
        }

        [TestMethod]
        public void LogitTestShape()
        {
            Assert.AreEqual(double.NaN, logit(-1));
            Assert.AreEqual(double.NegativeInfinity, logit(0));
            Assert.AreEqual(double.PositiveInfinity, logit(1));

            Assert.AreEqual(0, (double)logit(0.5), 1e-10);
            Assert.IsTrue(0 < (double)logit(0.51));
            Assert.IsTrue(0 > (double)logit(0.49));

            Assert.IsTrue(logit(0.2) < logit(0.4));
            Assert.IsTrue(logit(0.4) < logit(0.5));
            Assert.IsTrue(logit(0.5) < logit(0.6));
            Assert.IsTrue(logit(0.6) < logit(0.8));
        }

        [TestMethod]
        public void LogitTestInverse()
        {
            Array<double> x = linspace<double>(0.1, 0.9, 1000);
            Array<double> sol = logit(x);
            Array<double> inv = logistic(sol);

            Assert.IsTrue(sumall(inv - x) < 1e-6);
        }
    }
}
