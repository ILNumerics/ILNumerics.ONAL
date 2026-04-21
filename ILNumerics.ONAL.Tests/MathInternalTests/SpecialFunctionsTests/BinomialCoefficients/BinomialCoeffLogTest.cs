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
    public class BinomialCoeffLogTest
    {
        [TestMethod]
        public void TestInputScalarBinomialLog()
        {
            Assert.IsTrue((binomialCoefficientsLog(25, 23) - Math.Log(300)) < 1e-6, "Should return Log(300)");
            Assert.IsTrue((binomialCoefficientsLog(25, 0) - Math.Log(1)) < 1e-6, "Should return Log(1)");
            Assert.IsTrue(isnan(binomialCoefficientsLog(0, 100)), "Should return NaN");
            Assert.IsTrue((binomialCoefficientsLog(100, 100) - Math.Log(1)) < 1e-6, "Should return Log(1)");
            Assert.IsTrue((binomialCoefficientsLog(100, 1) - Math.Log(100)) < 1e-6, "Should return Log(100)");
        }

        [TestMethod]
        public void TestInputArrayBinomialLog()
        {
            Array<int> A = 5 * ones<int>(1000, 200);
            Array<int> B = 3 * ones<int>(1000, 200);
            Array<double> combiAB = Math.Log(10) * ones<double>(1000, 200);
            Assert.IsTrue(sumall(binomialCoefficientsLog(A, B) - combiAB) < 1e-5, "Should return a matrix of Log(10)");
        }

        [TestMethod]
        public void TestInputArrayCounterBinomialLog()
        {
            Array<int> A = counter<int>(1, 1, 5, 2);
            Array<int> B = counter<int>(-3, 1, 5, 2);
            Array<double> conbiAB = vector<double>(
                    (double)binomialCoefficientsLog(A[0, 0], B[0, 0]), 
                    (double)binomialCoefficientsLog(A[1, 0], B[1, 0]), 
                    (double)binomialCoefficientsLog(A[2, 0], B[2, 0]), 
                    (double)binomialCoefficientsLog(A[3, 0], B[3, 0]), 
                    (double)binomialCoefficientsLog(A[4, 0], B[4, 0]),
                    (double)binomialCoefficientsLog(A[0, 1], B[0, 1]), 
                    (double)binomialCoefficientsLog(A[1, 1], B[1, 1]), 
                    (double)binomialCoefficientsLog(A[2, 1], B[2, 1]), 
                    (double)binomialCoefficientsLog(A[3, 1], B[3, 1]), 
                    (double)binomialCoefficientsLog(A[4, 1], B[4, 1])).Reshape( 5, 2);
            Array<double> diffArr = binomialCoefficientsLog(A, B) - conbiAB;

            diffArr[isnan(diffArr)] = 0;
            
            Assert.IsTrue(sumall(diffArr) < 1e-6, "Should return matrix of binomials");
        }

        [TestMethod]
        public void TestInputArrayNaNBinomialLog()
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
            Array<double> combiAB = array<double>(
                new double[] { 
                    double.NaN, 
                    (double)binomialCoefficients(A[1,0],B[1, 0]), 
                    (double)binomialCoefficients(A[2,0],B[2, 0]), 
                    (double)binomialCoefficients(A[3,0],B[3, 0]), 
                    (double)binomialCoefficients(A[4,0],B[4, 0]),
                    (double)binomialCoefficients(A[0, 1],B[0, 1]), 
                    double.NaN, 
                    (double)binomialCoefficients(A[2, 1],B[2, 1]), 
                    (double)binomialCoefficients(A[3, 1],B[3, 1]), 
                    (double)binomialCoefficients(A[4, 1],B[4, 1]),
                    (double)binomialCoefficients(A[0, 2],B[0, 2]), 
                    (double)binomialCoefficients(A[1, 2],B[1, 2]),
                    double.NaN, 
                    (double)binomialCoefficients(A[3, 2],B[3, 2]), 
                    (double)binomialCoefficients(A[4, 2],B[4, 2]),
                    (double)binomialCoefficients(A[0, 3],B[0, 3]), 
                    (double)binomialCoefficients(A[1, 3],B[1, 3]), 
                    (double)binomialCoefficients(A[2, 3],B[2, 3]), 
                    double.NaN, 
                    (double)binomialCoefficients(A[4, 3],B[4, 3]),
                    (double)binomialCoefficients(A[0, 4],B[0, 4]), 
                    (double)binomialCoefficients(A[1, 4],B[1, 4]), 
                    (double)binomialCoefficients(A[2, 4],B[2, 4]), 
                    (double)binomialCoefficients(A[3, 4],B[3, 4]), 
                    double.NaN}, 
                    5, 
                    5);
            Assert.IsTrue(isequalwithequalnans(binomialCoefficientsLog(A, B), log(combiAB)), "Should return  the componentwise binomial inccluding NaN");
        }

        [TestMethod]
        public void TestBinomialLogShape()
        {
            Assert.AreEqual(0, (double)binomialCoefficientsLog(0, 0));
            Assert.AreEqual(0, (double)binomialCoefficientsLog(1, 1));
            Assert.AreEqual(0, (double)binomialCoefficientsLog(10, 10));
            Assert.AreEqual(0, (double)binomialCoefficientsLog(100, 100));
            Assert.AreEqual(0, (double)binomialCoefficientsLog(1000, 1000));

            Assert.AreEqual(double.NaN, (double)binomialCoefficientsLog(5, 10));
            Assert.AreEqual(Math.Log(252), (double)binomialCoefficientsLog(10, 5), 1e-6);

            Assert.AreEqual(double.NaN, (double)binomialCoefficientsLog(-5, 0));
            Assert.AreEqual(double.NaN, (double)binomialCoefficientsLog(0, -5));
        }
    }
}
