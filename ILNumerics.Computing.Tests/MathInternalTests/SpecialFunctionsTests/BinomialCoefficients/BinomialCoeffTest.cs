using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILNumerics; 
using static ILNumerics.Globals;
using static ILNumerics.ILMath;
namespace SpecialFunctionsTest
{
    [TestClass]
    public class BinomialCoeffTest
    {
        [TestMethod]
        public void TestInputScalarbinomial()
        {
            Assert.IsTrue(binomialCoefficients(25, 23) == 300, "Should return 300");
            Assert.IsTrue(binomialCoefficients(25, 0) == 1, "Should return 1");
            Assert.IsTrue(isnan(binomialCoefficients(0, 100)), "Should return NaN");
            Assert.IsTrue(binomialCoefficients(100, 100) == 1, "Should return 1");
            Assert.IsTrue(binomialCoefficients(100, 1) == 100, "Should return 100");
        }
        [TestMethod]
        public void TestInputArraybinomial()
        {
            Array<int> A = 5 * ones<int>(1000, 200);
            Array<int> B = 3 * ones<int>(1000, 200);
            Array<double> combiAB = 10 * ones<double>(1000, 200);
            Assert.IsTrue(binomialCoefficients(A, B).Equals(combiAB), "Should return a matrix of 10");
        }
        [TestMethod]
        public void TestInputArrayCounterbinomial()
        {
            Array<int> A = counter<int>(1, 1, 5, 2);
            Array<int> B = counter<int>(-3, 1, 5, 2);
            Array<double> conbiAB = array<double>(new double[] { (double)binomialCoefficients(A[0, 0], B[0, 0]), (double)binomialCoefficients(A[1, 0], B[1, 0]), (double)binomialCoefficients(A[2, 0], B[2, 0]), (double)binomialCoefficients(A[3, 0], B[3, 0]), (double)binomialCoefficients(A[4, 0], B[4, 0]),
                 (double)binomialCoefficients(A[0, 1], B[0, 1]), (double)binomialCoefficients(A[1, 1], B[1, 1]), (double)binomialCoefficients(A[2, 1], B[2, 1]), (double)binomialCoefficients(A[3, 1], B[3, 1]), (double)binomialCoefficients(A[4, 1], B[4, 1]), }, 5, 2);
            Assert.IsTrue(isequalwithequalnans(binomialCoefficients(A, B), conbiAB), "Should return matrix of binomials");
        }
        [TestMethod]
        public void TestInputArrayNaNBinomial()
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
            Array<double> combiAB = array<double>(new double[] { double.NaN, (double)binomialCoefficients(A[1,0],B[1, 0]), (double)binomialCoefficients(A[2,0],B[2, 0]), (double)binomialCoefficients(A[3,0],B[3, 0]), (double)binomialCoefficients(A[4,0],B[4, 0]),
                (double)binomialCoefficients(A[0, 1],B[0, 1]), double.NaN, (double)binomialCoefficients(A[2, 1],B[2, 1]), (double)binomialCoefficients(A[3, 1],B[3, 1]), (double)binomialCoefficients(A[4, 1],B[4, 1]),
                (double)binomialCoefficients(A[0, 2],B[0, 2]), (double)binomialCoefficients(A[1, 2],B[1, 2]), double.NaN, (double)binomialCoefficients(A[3, 2],B[3, 2]), (double)binomialCoefficients(A[4, 2],B[4, 2]),
                (double)binomialCoefficients(A[0, 3],B[0, 3]), (double)binomialCoefficients(A[1, 3],B[1, 3]), (double)binomialCoefficients(A[2, 3],B[2, 3]), double.NaN, (double)binomialCoefficients(A[4, 3],B[4, 3]),
                (double)binomialCoefficients(A[0, 4],B[0, 4]), (double)binomialCoefficients(A[1, 4],B[1, 4]), (double)binomialCoefficients(A[2, 4],B[2, 4]), (double)binomialCoefficients(A[3, 4],B[3, 4]), double.NaN}, 5, 5);
            Assert.IsTrue(isequalwithequalnans(binomialCoefficients(A, B), combiAB), "Should return  the componentwise binomial inccluding NaN");
        }

        [TestMethod]
        public void TestBinomialShape()
        {
            Assert.AreEqual(1, (double)binomialCoefficients(0, 0));
            Assert.AreEqual(1, (double)binomialCoefficients(1, 1));
            Assert.AreEqual(1, (double)binomialCoefficients(10, 10));
            Assert.AreEqual(1, (double)binomialCoefficients(100, 100));
            Assert.AreEqual(1, (double)binomialCoefficients(1000, 1000));

            Assert.AreEqual(double.NaN, (double)binomialCoefficients(5, 10));
            Assert.AreEqual(252, (double)binomialCoefficients(10, 5));

            Assert.AreEqual(double.NaN, (double)binomialCoefficients(-5, 0));
            Assert.AreEqual(double.NaN, (double)binomialCoefficients(0, -5));
        }
    }
}
