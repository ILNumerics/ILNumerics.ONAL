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
    public class Besselk1Test 
    {
        [TestMethod]
        public void BesselK1TestNull()
        {
            using (Scope.Enter())
            {
                Array<double> x = null;
                try
                {
                    x = besselModifiedK1(x);
                    Assert.Fail("The modified bessel function Method failed to throw the ArgumentNullException for null input value");
                }
                catch (ArgumentNullException exc)
                {
                    Assert.IsTrue(exc.Message.Contains("null"), "The modified bessel function of second type failed to throw the ArgumentNullException for null input value");
                }
            }
        }
        [TestMethod]
        public void BesselK1TestCoeffZeros()
        {
            using (Scope.Enter())
            {
                Array<double> x = zeros(1, 10);
                x = besselModifiedK1(x);
                Assert.IsTrue(isposinf(x[0]), "The modified bessel function Method failed to give back +Inf from an array of zeros as input value");
            }
        }
        [TestMethod]
        public void BesselK1BigTestCoeffOnes()
        {
            using (Scope.Enter())
            {
                Array<double> x = double.MaxValue * ones(1, 10);
                x = besselModifiedK1(x);
                Assert.IsTrue((maxall(x)<=1e-150),"The modified bessel function Method failed to evaluate an array of values bigger than 8 as input value");
            }
        }
        [TestMethod]
        public void BesselK1TestCoeffOnes()
        {
            using (Scope.Enter())
            {
                Array<double> x = ones(1, 10);
                x[2] = 500;
                x[4] = 25;
                x[6] = 0.75;
                x[1] = 50;
                x[3] = double.MaxValue;
                x[9] = double.NaN;
                x = besselModifiedK1(x);
                Array<double> Sol = besselModifiedK1(1.0) * ones(1, 10);
                Sol[2] = besselModifiedK1(500.0);
                Sol[4] = besselModifiedK1(25.0);
                Sol[6] = besselModifiedK1(0.75);
                Sol[1] = besselModifiedK1(50.0);
                Sol[3] = besselModifiedK1(double.MaxValue);
                Sol[9] = besselModifiedK1(double.NaN);
                Assert.IsTrue(isequalwithequalnans(x, Sol), "The modified bessel function Method failed to evaluate an array of different magnitude values");
            }
        }
    }
}
