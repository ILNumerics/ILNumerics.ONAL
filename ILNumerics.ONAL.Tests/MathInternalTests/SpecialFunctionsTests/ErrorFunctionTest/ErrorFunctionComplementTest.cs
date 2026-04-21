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
    public class ErrorFunctionComplementTest 
    {
        [TestMethod]
        public void cerfTestNull()
        {
            using (Scope.Enter())
            {
                Array<double> x = null;
                try
                {
                    x = errorFunctionComplement(x);
                    Assert.Fail("The complement error function Method failed to throw the ArgumentNullException for null input value");
                }
                catch (ArgumentNullException exc)
                {
                    Assert.IsTrue(exc.Message.Contains("null"), "The error function Method failed to throw the ArgumentNullException for null input value");
                }
            }
        }
        [TestMethod]
        public void cerfTestCoeffZeros()
        {
            using (Scope.Enter())
            {
                Array<double> x = zeros(1, 10);
                x = errorFunctionComplement(x);
                Assert.IsTrue(maxall(x)==1.0, "The ComplementErrorMethod failed to throw the ArgumentOutOfRangeException for zeros input value");
            }
        }
        [TestMethod]
        public void cerfTestCoeffOnes()
        {
            using (Scope.Enter())
            {
                Array<double> x = ones(1, 10);
                x[2] = 0.5;
                x[4] = 0.25;
                x[6] = 0.75;
                x = errorFunctionComplement(x);
                Array<double> Sol = errorFunctionComplement(1.0) * ones(1, 10);
                Sol[2] = errorFunctionComplement(0.5);
                Sol[4] = errorFunctionComplement(0.25);
                Sol[6] = errorFunctionComplement(0.75);
                Assert.IsTrue(isequalwithequalnans(x, Sol), "The complement error function failed to evaluate a vector of double");
            }
        }
        [TestMethod]
        public void cerfTestBigCoeffOnes()
        {
            using (Scope.Enter())
            {
                Array<double> x = 5.0 * ones(1, 10);
                x[2] = 0.5;
                x[4] = 4.25;
                x[6] = 0.75;
                x = errorFunctionComplement(x);
                Array<double> Sol = errorFunctionComplement(5.0) * ones(1, 10);
                Sol[2] = errorFunctionComplement(0.5);
                Sol[4] = errorFunctionComplement(4.25);
                Sol[6] = errorFunctionComplement(0.75);
                Assert.IsTrue(isequalwithequalnans(x, Sol), "The complement error function failed to evaluate a vector of double");
            }
        }
        [TestMethod]
        public void cerfTestNaNinftyZeros()
        {
            using (Scope.Enter())
            {
                Array<double> x = 10 * rand(1, 10);
                Array<double>Sol = errorFunctionComplement(-x);
                Array<double> Sol2 = 2 - errorFunctionComplement(x);
                Assert.IsTrue(sumall(abs(Sol - Sol2)) < 1e-6, "The complement error function failed to fulfill the asymmetric property for a vector of double");
            }
        }

        [TestMethod]
        public void CerfShapeTest()
        {
            // see wiki definition: http://en.wikipedia.org/wiki/Error_function
            double val = errorFunctionComplement(+100).GetValue(0);            
            double compl = 1 - errorFunction(+100).GetValue(0);
            Assert.AreEqual(val, compl, 0.00005);

            val = errorFunctionComplement(+50).GetValue(0);
            compl = 1 - errorFunction(+50).GetValue(0);
            Assert.AreEqual(val, compl, 0.00005);

            val = errorFunctionComplement(+10).GetValue(0);
            compl = 1 - errorFunction(+10).GetValue(0);
            Assert.AreEqual(val, compl, 0.00005);

            val = errorFunctionComplement(-10).GetValue(0);
            compl = 1 - errorFunction(-10).GetValue(0);
            Assert.AreEqual(val, compl, 0.00005);

            val = errorFunctionComplement(-50).GetValue(0);
            compl = 1 - errorFunction(-50).GetValue(0);
            Assert.AreEqual(val, compl, 0.00005);

            val = errorFunctionComplement(-100).GetValue(0);
            compl = 1 - errorFunction(-100).GetValue(0);
            Assert.AreEqual(val, compl, 0.00005);

            val = errorFunctionComplement(0).GetValue(0);
            compl = 1 - errorFunction(0).GetValue(0);
            Assert.AreEqual(val, compl, 0.00005);

            // check edges
            Assert.AreEqual(2.0, errorFunctionComplement(double.NegativeInfinity));
            Assert.AreEqual(0.0, errorFunctionComplement(double.PositiveInfinity));

            // check monoton decreasing
            Assert.IsTrue(errorFunctionComplement(-5) > errorFunctionComplement(-2));
            Assert.IsTrue(errorFunctionComplement(-2) > errorFunctionComplement(-1));
            Assert.IsTrue(errorFunctionComplement(-1) > errorFunctionComplement(-0.5));
            Assert.IsTrue(errorFunctionComplement(-0.5) > errorFunctionComplement(0));
            Assert.IsTrue(errorFunctionComplement(0) > errorFunctionComplement(0.5));
            Assert.IsTrue(errorFunctionComplement(0.5) > errorFunctionComplement(1));
            Assert.IsTrue(errorFunctionComplement(1) > errorFunctionComplement(2));
            Assert.IsTrue(errorFunctionComplement(2) > errorFunctionComplement(5));
        }
    }
}
