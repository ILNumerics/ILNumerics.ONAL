using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILNumerics; 
using static ILNumerics.Globals;
using static ILNumerics.ILMath;
namespace SpecialFunctionsTest
{
    [TestClass]
    public class InCompBetaTest 
    {
        [TestMethod]
        public void IncompBetaTestNull()
        {
            using (Scope.Enter())
            {
                Array<double> x = null;
                try
                {
                    x = betaIncomplete(5.0, 2.0, x);
                    Assert.Fail("The incomplete beta Method failed to throw the ArgumentNullException for null input value");
                }
                catch (ArgumentNullException exc)
                {
                    Assert.IsTrue(exc.Message.Contains("null"), "The incomplete beta Method failed to throw the ArgumentNullException for null input value");
                }
            }
        }

        [TestMethod]
        public void IncompBetaTestCoeffZeros()
        {
            using (Scope.Enter())
            {
                Array<double> x = zeros(1, 10);
                x = betaIncomplete(3.0, 2.0, x);
                Assert.IsTrue((norm(x)) <= 1e-10, "The gamma Method failed to throw the ArgumentOutOfRangeException for zeros input value");
            }
        }

        [TestMethod]
        public void IncompBetaTestCoeffOnes()
        {
            using (Scope.Enter())
            {
                Array<double> x = ones(1, 10);
                x[2] = 0.5;
                x[4] = 0.25;
                x[6] = 0.75;
                x = betaIncomplete(3.0, 2.0, x);
                Array<double> Sol = ones(1, 10);
                Sol[2] = betaIncomplete(3.0, 2.0, 0.5);
                Sol[4] = betaIncomplete(3.0, 2.0, 0.25);
                Sol[6] = betaIncomplete(3.0, 2.0, 0.75);
                Assert.IsTrue((norm(x-Sol)) <= 1e-10, "The gamma Method failed to throw the ArgumentOutOfRangeException for zeros input value");
            }
        }

        [TestMethod]
        public void IncompBetaTestBigCoeffOnes()
        {
            using (Scope.Enter())
            {
                Array<double> x = 5.0*ones(1, 10);
                x[2] = 0.5;
                x[4] = 4.25;
                x[6] = 0.75;
                x = betaIncomplete(3.0, 2.0, x);
                Array<double> Sol = betaIncomplete(3.0, 2.0, 5.0)*ones(1, 10);
                Sol[2] = betaIncomplete(3.0, 2.0, 0.5);
                Sol[4] = betaIncomplete(3.0, 2.0, 4.25);
                Sol[6] = betaIncomplete(3.0, 2.0, 0.75);
                Assert.IsTrue(isequalwithequalnans(x, Sol), "The gamma Method failed to throw the ArgumentOutOfRangeException for zeros input value");
            }
        }

        [TestMethod]
        public void IncompBetaTestBigCoeff()
        {
            using (Scope.Enter())
            {
                Array<double> x = 5.0 * ones(1, 10);
                x[2] = 0.5;
                x[4] = 4.25;
                x[6] = 0.75;
                x = betaIncomplete(6000.0, 5000.0, x);
                Array<double> Sol = betaIncomplete(36000.0, 5000.0, 5.0) * ones(1, 10);
                Sol[2] = betaIncomplete(6000.0, 5000.0, 0.5);
                Sol[4] = betaIncomplete(6000.0, 5000.0, 4.25);
                Sol[6] = betaIncomplete(6000.0, 5000.0, 0.75);
                Assert.IsTrue(isequalwithequalnans(x, Sol), "The gamma Method failed to throw the ArgumentOutOfRangeException for zeros input value");
            }
        }

        [TestMethod]
        public void IncompBetaTestShape()
        {            
            // test 'edges'
            Assert.AreEqual(0, betaIncomplete(0, 0, 0));
            Assert.AreEqual(0, betaIncomplete(1, 1, 0));
            Assert.AreEqual(0, betaIncomplete(0.5, 0.5, 0));
            Assert.AreEqual(0, betaIncomplete(2, 2, 0));
            Assert.AreEqual(0, betaIncomplete(5, 5, 0));
            Assert.AreEqual(0, betaIncomplete(10, 10, 0));
            Assert.AreEqual(0, betaIncomplete(100, 100, 0));

            Assert.AreEqual(1, betaIncomplete(0, 0, 1));
            Assert.AreEqual(1, betaIncomplete(0.5, 0.5, 1));
            Assert.AreEqual(1, betaIncomplete(1, 1, 1));
            Assert.AreEqual(1, betaIncomplete(2, 2, 1));
            Assert.AreEqual(1, betaIncomplete(5, 5, 1));
            Assert.AreEqual(1, betaIncomplete(10, 10, 1));
            Assert.AreEqual(1, betaIncomplete(100, 100, 1));

            // test monotonity
            Assert.IsTrue(betaIncomplete(0.5, 0.5, 0.2) < betaIncomplete(0.5, 0.5, 0.8));
            Assert.IsTrue(betaIncomplete(1, 1, 0.2) < betaIncomplete(1, 1, 0.8));
            Assert.IsTrue(betaIncomplete(2, 2, 0.2) < betaIncomplete(2, 2, 0.8));
            Assert.IsTrue(betaIncomplete(5, 5, 0.2) < betaIncomplete(5, 5, 0.8));
            Assert.IsTrue(betaIncomplete(10, 10, 0.2) < betaIncomplete(10, 10, 0.8));
            Assert.IsTrue(betaIncomplete(100, 100, 0.2) < betaIncomplete(100, 100, 0.8));

            Assert.IsTrue(betaIncomplete(0.5, 1, 0.2) < betaIncomplete(0.5, 1, 0.8));
            Assert.IsTrue(betaIncomplete(1, 2, 0.2) < betaIncomplete(1, 2, 0.8));
            Assert.IsTrue(betaIncomplete(2, 5, 0.2) < betaIncomplete(2, 5, 0.8));
            Assert.IsTrue(betaIncomplete(5, 10, 0.2) < betaIncomplete(5, 10, 0.8));

            Assert.IsTrue(betaIncomplete(1, 0.5, 0.2) < betaIncomplete(1, 0.5, 0.8));
            Assert.IsTrue(betaIncomplete(2, 1, 0.2) < betaIncomplete(2, 1, 0.8));
            Assert.IsTrue(betaIncomplete(5, 2, 0.2) < betaIncomplete(5, 2, 0.8));
            Assert.IsTrue(betaIncomplete(10, 5, 0.2) < betaIncomplete(10, 5, 0.8));
        }
    }
}
