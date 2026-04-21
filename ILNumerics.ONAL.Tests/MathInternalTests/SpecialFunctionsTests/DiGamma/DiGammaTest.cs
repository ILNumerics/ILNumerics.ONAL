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

namespace ILNumerics.UnitTests.SpecialFunctionsTest.DiGamma
{
    [TestClass]
    public class DiGammaTest
    {
        [TestMethod]
        public void DiGammaShapeTest()
        {
            Assert.AreEqual(double.NegativeInfinity, diGamma(0));
            Assert.IsTrue(double.NegativeInfinity < diGamma(0.1));
            Assert.IsTrue(diGamma(0.1) < diGamma(0.5));
            Assert.IsTrue(diGamma(0.5) < diGamma(1.5));
            Assert.IsTrue(diGamma(1.0) < diGamma(2.5));

            Assert.AreEqual(double.NegativeInfinity, diGamma(-1));
            Assert.AreEqual(double.NegativeInfinity, diGamma(-2));
            Assert.AreEqual(double.NegativeInfinity, diGamma(-3));
            Assert.AreEqual(double.NegativeInfinity, diGamma(-4));
            Assert.AreEqual(double.NegativeInfinity, diGamma(-5));

            Assert.IsTrue(1000 < diGamma(-1 - 1e-6));
            Assert.IsTrue(1000 < diGamma(-2 - 1e-6));
            Assert.IsTrue(1000 < diGamma(-3 - 1e-6));
            Assert.IsTrue(1000 < diGamma(-4 - 1e-6));
            Assert.IsTrue(1000 < diGamma(-5 - 1e-6));

            Assert.AreEqual(0, (double)diGamma(-0.5), 1e-1);
        }

        [TestMethod]
        public void DiGammaArgEdgesTest()
        {
            Assert.AreEqual(double.NaN, diGamma(double.NaN));
            Assert.AreEqual(double.NaN, diGamma(double.NegativeInfinity));
            Assert.AreEqual(double.PositiveInfinity, diGamma(double.PositiveInfinity));
            Assert.AreEqual(ones(1, 10) * double.NegativeInfinity, diGamma(linspace<double>(-10, -1, 10)));
        }


        [TestMethod]
        public void DiGammaArgPosTest()
        {
            Array<double> points = linspace<double>(0.1, 100, 1000);
            Array<double> diGammaValues = diGamma(points);

            Array<double> diff = Core.Functions.Builtin.MathInternal.diff(diGammaValues);
            Assert.IsTrue(minall(diff) > 0); // it should increase monotonically, diff1 shall be positive for all
        }

        [TestMethod]
        public void DiGammaArgNegTest()
        {

            using (Scope.Enter())
            {
                for (int i = -100; i < -1; i++)
                {
                    Array<double> points = linspace<double>(i + 0.01, i + 0.99, 1001);
                    Array<double> diGammaValues = diGamma(points);

                    // shall be running from neg inf to pos inf
                    Array<double> diff = Core.Functions.Builtin.MathInternal.diff(diGammaValues);
                    Assert.IsTrue(minall(diff) > 0);
                }
            }
        }
    }
}
