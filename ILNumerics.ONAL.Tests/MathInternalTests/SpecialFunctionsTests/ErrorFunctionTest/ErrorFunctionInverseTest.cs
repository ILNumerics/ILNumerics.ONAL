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
    public class ErrorFunctionInverseTest 
    {
        [TestMethod]
        public void IerfTestNull()
        {
            using (Scope.Enter())
            {
                Array<double> x = null;
                try
                {
                    x = errorFunctionInverse(x);
                    Assert.Fail("The errorFunctionInverse failed to throw the ArgumentNullException for null input value");
                }
                catch (ArgumentNullException exc)
                {
                    Assert.IsTrue(exc.Message.Contains("null"), "The error function Method failed to throw the ArgumentNullException for null input value");
                }
            }
        }
        [TestMethod]
        public void IerfTestCoeffZeros()
        {
            using (Scope.Enter())
            {
                Array<double> x = zeros(1, 10);
                x = errorFunctionInverse(x);
                Assert.IsTrue(isequalwithequalnans(zeros(1,10), x));
            }
        }
        [TestMethod]
        public void IerfTestCoeffOnes()
        {
            using (Scope.Enter())
            {
                Array<double> x = ones(1, 10);
                x[2] = 0.5;
                x[4] = 0.25;
                x[6] = 0.75;
                x = errorFunctionInverse(x);
                Array<double> Sol = errorFunctionInverse(1.0) * ones(1, 10);
                Sol[2] = errorFunctionInverse(0.5);
                Sol[4] = errorFunctionInverse(0.25);
                Sol[6] = errorFunctionInverse(0.75);
                Assert.IsTrue(isequalwithequalnans(x, Sol), "The errorFunctionInverse failed to evaluate a vector of double");
            }
        }
        [TestMethod]
        public void IerfTestBigCoeffOnes()
        {
            using (Scope.Enter())
            {
                Array<double> x = 5.0 * ones(1, 10);
                x[2] = 0.5;
                x[4] = 4.25;
                x[6] = 0.75;
                x = errorFunctionInverse(x);
                Array<double> Sol = errorFunctionInverse(5.0) * ones(1, 10);
                Sol[2] = errorFunctionInverse(0.5);
                Sol[4] = errorFunctionInverse(4.25);
                Sol[6] = errorFunctionInverse(0.75);
                Assert.IsTrue(isequalwithequalnans(x, Sol), "The errorFunctionInverse failed to evaluate a vector of double");
            }
        }

        [TestMethod]
        public void IerfTestLinspaceInverse1()
        {
            using (Scope.Enter())
            {
                Array<double> x = linspace<double>(-0.99, 0.99, 1000);
                Array<double> Sol = errorFunctionInverse(x);
                Array<double> inverse = errorFunction(Sol);
                Assert.IsTrue(sumall(abs(x - inverse)) < 1e-6, "The errorFunctionInverse failed");
            }
        }

        [TestMethod]
        public void IerfTestLinspaceInverse2()
        {
            using (Scope.Enter())
            {
                Array<double> x = linspace<double>(-2, 2, 1000);
                Array<double> Sol = errorFunction(x);
                Array<double> inverse = errorFunctionInverse(Sol);
                Assert.IsTrue(sumall(abs(x - inverse)) < 1e-6, "The errorFunctionInverse failed");
            }
        }
        
        
        [TestMethod]
        public void IerfTestRandInverse1()
        {
            using (Scope.Enter())
            {
                Array<double> x = rand(1, 100);
                Array<double> Sol = errorFunctionInverse(x);
                Array<double> inverse = errorFunction(Sol);
                Assert.IsTrue(sumall(abs(x - inverse)) < 1e-6, "The errorFunctionInverse failed");
            }
        }

        [TestMethod]
        public void IerfShapeTest()
        {
            // check edges
            Assert.AreEqual(double.PositiveInfinity, errorFunctionInverse(1.0));
            Assert.AreEqual(double.NegativeInfinity, errorFunctionInverse(-1.0));

            // check monoton increasing
            Assert.IsTrue(errorFunctionInverse(-1) < errorFunctionInverse(-0.8));
            Assert.IsTrue(errorFunctionInverse(-0.8) < errorFunctionInverse(-0.6));
            Assert.IsTrue(errorFunctionInverse(-0.6) < errorFunctionInverse(-0.4));
            Assert.IsTrue(errorFunctionInverse(-0.4) < errorFunctionInverse(-0.2));
            Assert.IsTrue(errorFunctionInverse(-0.2) < errorFunctionInverse(0));
            Assert.IsTrue(errorFunctionInverse(0) < errorFunctionInverse(0.2));
            Assert.IsTrue(errorFunctionInverse(0.2) < errorFunctionInverse(0.4));
            Assert.IsTrue(errorFunctionInverse(0.4) < errorFunctionInverse(0.6));
            Assert.IsTrue(errorFunctionInverse(0.6) < errorFunctionInverse(0.8));
            Assert.IsTrue(errorFunctionInverse(0.8) < errorFunctionInverse(1));
        }
    }
}
