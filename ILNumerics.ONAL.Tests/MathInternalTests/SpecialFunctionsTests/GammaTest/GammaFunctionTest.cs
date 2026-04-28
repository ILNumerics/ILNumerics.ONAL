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
    public class GammaFunctionTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestGammaLogNull()
        {
            using (Scope.Enter())
            {
                Array<double> x = null;
                x = gammaLog(x);
 
            }
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestGammaNull()
        {
            using (Scope.Enter())
            {
                Array<double> x = null;
                x = gamma(x);
            }
        }

        [TestMethod]
        public void TestGammaLogZeros()
        {
            using (Scope.Enter())
            {
                Array<double> x = zeros(1, 10);
                x = gammaLog(x);
                Assert.IsTrue(x.Equals(ones(1, 10) * double.PositiveInfinity));
            }
        }

        [TestMethod]
        public void TestGammaZeros()
        {
            using (Scope.Enter())
            {
                Array<double> x = zeros(1, 10);
                x = gamma(x);
                Assert.IsTrue(x.Equals(ones(1, 10) * double.PositiveInfinity));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGammaLogInfNaN()
        {
            using(Scope.Enter())
            {
                Array<double> x = ones(1, 10);
                x[0] = double.NaN;
                x = gammaLog(x);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGammaInfNaN()
        {
            using (Scope.Enter())
            {
                Array<double> x = ones(1, 10);
                x[0] = double.NaN;
                x = gamma(x);
 
            }
        }

        [TestMethod]
        public void TestGammaLogOnes()
        {
            using (Scope.Enter())
            {
                Array<double> x = ones(1, 10);
                Array<double> sol = gammaLog(x);
                Assert.IsTrue((max(abs(exp(sol) - 1)) <= 1e-10), "The gamma Method failed to evaluate for a vector of ones");
            }
       }

        [TestMethod]
        public void TestGammaOnes()
        {
            using (Scope.Enter())
            {
                Array<double> x = ones(1, 10);
                Array<double> sol = gamma(x);
                Assert.IsTrue((max(abs(sol) - 1)) <= 1e-10, "The gamma Method failed to evaluate for a vector of ones");
            }
        }

        [TestMethod]
        public void TestGammaLogVectorOfThrees()
        {
            using (Scope.Enter())
            {
                Array<double> x = 3.0 * ones(1, 10);
                Array<double> sol = gammaLog(x);
                Assert.IsTrue((max(abs(exp(sol) - 2.0)) <= 1e-7), "The gamma Method failed to evaluate for a vectoe of ones");
            }
        }

        [TestMethod]
        public void TestGammaVectorOfThrees()
        {
            using (Scope.Enter())
            {
                Array<double> x = 3.0 * ones(1, 10);
                Array<double> sol = gamma(x);
                Assert.IsTrue((max(abs(sol) - 2.0)) <= 1e-7, "The gamma Method failed to evaluate for a vectoe of ones");
            }
        }

        [TestMethod]
        public void TestGammaVectorOfSeven()
        {
            using (Scope.Enter())
            {
                Array<double> x = 7.0 * ones(1, 10);
                Array<double> sol = gammaLog(x);
                Assert.IsTrue((max(abs(exp(sol) - 720)) <= 1e-7), "The gamma method failed to evaluate for a vector of ones");
            }
        }

        [TestMethod]
        public void TestGammaLogVectorOfDifferentValues()
        {
            using (Scope.Enter())
            {
                Array<double> x = ones(1, 3);
                x[1] = 2.0;
                x[2] = 3.0;
                Array<double> sol = gammaLog(x);
                Assert.IsTrue(max(abs(exp(sol) - vector(1.0,1.0,2.0).Reshape(1,3))) <= 1e-7, "The gamma method failed to evaluate for a vector of ones");
            }
        }

        [TestMethod]
        public void TestGammaVectorOfDifferentValues()
        {
            using (Scope.Enter())
            {
                Array<double> x = ones(1, 3);
                x[1] = 2.0;
                x[2] = 3.0;
                Array<double> sol = gamma(x);
                Assert.IsTrue(max(abs(sol) - vector(1.0, 1.0, 2.0).Reshape(1,3)) <= 1e-7, "The gamma method failed to evaluate for a vector of ones");
            }
        }

        [TestMethod]
        public void TestGammaLogShape()
        {
            using (Scope.Enter())
            {
                double valLeftMost = gammaLog(0.5).GetValue(0);
                double valLeft = gammaLog(1).GetValue(0);
                double valCenter = gammaLog(1.5).GetValue(0);
                double valRight = gammaLog(2).GetValue(0);
                double valRightMost = gammaLog(3).GetValue(0);

                Assert.AreEqual(0, valLeft, 0.001);
                Assert.AreEqual(0, valRight, 0.001);
                Assert.IsTrue(valCenter < 0);
                
                Assert.IsTrue(valCenter < valLeft);
                Assert.IsTrue(valCenter < valLeftMost);

                Assert.IsTrue(valCenter < valRight);
                Assert.IsTrue(valCenter < valRightMost);

                Assert.IsTrue(valLeft < valLeftMost);
                Assert.IsTrue(valRight < valRightMost);

                Assert.IsTrue(isinf(gammaLog(0)));
                Assert.IsTrue(isposinf(gammaLog(0)));
                Assert.IsTrue(isnan(gammaLog(-10)));
            }
        }

        [TestMethod]
        public void TestGammaShape()
        {
            using (Scope.Enter())
            {
                double valLeft = gamma(0.5).GetValue(0);
                double valCenter = gamma(1.5).GetValue(0);
                double valRight = gamma(3).GetValue(0);

                Assert.IsTrue(valCenter > 0);

                Assert.IsTrue(valCenter < valLeft);
                Assert.IsTrue(valCenter < valRight);

                Assert.IsTrue(isinf(gamma(0)));
                Assert.IsTrue(isposinf(gamma(0)));
                Assert.IsTrue(isnan(gamma(-10)));
            }
        }
    }
}
