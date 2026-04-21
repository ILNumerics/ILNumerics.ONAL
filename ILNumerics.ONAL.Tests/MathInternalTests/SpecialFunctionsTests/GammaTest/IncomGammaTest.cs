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
    public class IncomGammaTest 
    {
        [TestMethod]
        public void IncompGammaTestNull()
        {
            using (Scope.Enter())
            {
                Array<double> x = null;
                try
                {
                    x = gammaIncomplete(5.0, x);
                    Assert.Fail("The gamma Method failed to throw the ArgumentNullException for null input value");
                }
                catch (ArgumentNullException exc)
                {
                    Assert.IsTrue(exc.Message.Contains("null"), "The gamma Method failed to throw the ArgumentNullException for null input value");
                }
            }
        }

        [TestMethod]
        public void IncomGammaTestZeros()
        {
            using (Scope.Enter())
            {
                Array<double> x = zeros(1, 10);
                x = gammaIncomplete(5.0,x);
                Assert.IsTrue(norm(x)==0.0, "The gamma Method failed to throw the ArgumentOutOfRangeException for zeros input value");
            }
        }

        [TestMethod]
        public void IncomGammaTestBigNumber()
        {
            using (Scope.Enter())
            {
                Array<double> x = double.MaxValue*ones(1, 10);
                x = gammaIncomplete(5.0, x);
                Assert.IsTrue((norm(x - 1.0)) <= 1e-10, "The gamma Method failed to throw the ArgumentOutOfRangeException for zeros input value");
            }
        }

        [TestMethod]
        public void IncomGammaTestInputArray()
        {
            Array<double> B = 5 * ones<double>(1000, 200);
            Array<double> FactB = gammaIncomplete(5.0,5.0) * ones<double>(1000, 200);
            Assert.IsTrue(gammaIncomplete(5.0, B).Equals(FactB), "Should return a vector of same values");
        }

        [TestMethod]
        public void IncomGammaInputArrayCounter()
        {
            Array<double> B = counter<double>(1, 1, 5, 2);
            Array<double> FactB = vector<double>((double)gammaIncomplete(5.0,1), (double)gammaIncomplete(5.0,2), 
                (double)gammaIncomplete(5.0,3), (double)gammaIncomplete(5.0,4), 
                (double)gammaIncomplete(5.0,5), (double)gammaIncomplete(5.0,6), 
                (double)gammaIncomplete(5.0,7), (double)gammaIncomplete(5.0,8), 
                (double)gammaIncomplete(5.0,9), (double)gammaIncomplete(5.0,10)).Reshape(5, 2);
            Assert.IsTrue(isequalwithequalnans(gammaIncomplete(5.0, B), FactB), "Should return componentwise incomplete gamma values");
        }

        [TestMethod]
        public void IncomGammaTestInputArrayNaN()
        {
            Array<double> B = 6 * ones<double>(5, 5);
            B[4, 4] = 2000;
            B[3, 3] = 20;
            B[2, 2] = 30;
            B[1, 1] = 10;
            B[0, 0] = 10;
            Array<double> FactB = vector(new double[] { (double)gammaIncomplete(5.0,B[0, 0]), (double)gammaIncomplete(5.0,B[0, 1]),
                (double)gammaIncomplete(5.0,B[0, 2]), (double)gammaIncomplete(5.0,B[0, 3]), 
                (double)gammaIncomplete(5.0,B[0, 4]),
                (double)gammaIncomplete(5.0,B[1, 0]), (double)gammaIncomplete(5.0,B[1, 1]), 
                (double)gammaIncomplete(5.0,B[1, 2]), (double)gammaIncomplete(5.0,B[1, 3]), 
                (double)gammaIncomplete(5.0,B[1, 4]),
                (double)gammaIncomplete(5.0,B[2, 0]), (double)gammaIncomplete(5.0,B[2, 1]),
                (double)gammaIncomplete(5.0,B[2, 2]), (double)gammaIncomplete(5.0,B[2, 3]), (double)gammaIncomplete(5.0,B[2, 4]),
                (double)gammaIncomplete(5.0,B[3, 0]), (double)gammaIncomplete(5.0,B[3, 1]), 
                (double)gammaIncomplete(5.0,B[3, 2]), (double)gammaIncomplete(5.0,B[3, 3]), (double)gammaIncomplete(5.0,B[3, 4]),
                (double)gammaIncomplete(5.0,B[4, 0]), (double)gammaIncomplete(5.0,B[4, 1]), 
                (double)gammaIncomplete(5.0,B[4, 2]), (double)gammaIncomplete(5.0,B[4, 3]), (double)gammaIncomplete(5.0,B[4, 4])}).Reshape(5, 5);
            Assert.IsTrue(isequalwithequalnans(gammaIncomplete(5.0,B), FactB), "Should return  the componentwise IncomGammas with NaN");
        }

        [TestMethod]
        public void IncomGammaTestShape()
        {
            Assert.AreEqual(0, gammaIncomplete(1, 0));
            Assert.AreEqual(0, gammaIncomplete(2, 0));
            Assert.AreEqual(0, gammaIncomplete(5, 0));
            Assert.AreEqual(0, gammaIncomplete(10, 0));
            Assert.AreEqual(0, gammaIncomplete(50, 0));
            Assert.AreEqual(0, gammaIncomplete(100, 0));

            Assert.IsTrue(gammaIncomplete(1, 1) > 0);
            Assert.IsTrue(gammaIncomplete(2, 1) > 0);
            Assert.IsTrue(gammaIncomplete(5, 1) > 0);
            Assert.IsTrue(gammaIncomplete(10, 1) > 0);
            Assert.IsTrue(gammaIncomplete(50, 1) > 0);
            Assert.IsTrue(gammaIncomplete(100, 1) > 0);

            Assert.IsTrue(gammaIncomplete(1, 10) > gammaIncomplete(1, 1));
            Assert.IsTrue(gammaIncomplete(2, 10) > gammaIncomplete(2, 1));
            Assert.IsTrue(gammaIncomplete(5, 10) > gammaIncomplete(5, 1));
            Assert.IsTrue(gammaIncomplete(10, 10) > gammaIncomplete(10, 1));
            Assert.IsTrue(gammaIncomplete(50, 10) > gammaIncomplete(50, 1));
            Assert.IsTrue(gammaIncomplete(100, 10) > gammaIncomplete(100, 1));

            Assert.AreEqual(1, (double)gammaIncomplete(1, 11), 1e-3);
            Assert.AreEqual(1, (double)gammaIncomplete(2, 21), 1e-5);
            Assert.AreEqual(1, (double)gammaIncomplete(5, 51), 1e-5);
            Assert.AreEqual(1, (double)gammaIncomplete(10, 101), 1e-5);
            Assert.AreEqual(1, (double)gammaIncomplete(50, 501), 1e-5);
            Assert.AreEqual(1, (double)gammaIncomplete(100, 1001), 1e-5);
        }
    }
}
