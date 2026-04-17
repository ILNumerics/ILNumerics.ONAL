using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILNumerics; 
using static ILNumerics.Globals;
using static ILNumerics.ILMath;

namespace SpecialFunctionsTest
{
    [TestClass]
    public class BesselI1Test
    {
        [TestMethod]
        public void BesselI1TestNull()
        {
            using (Scope.Enter())
            {
                Array<double> x = null;
                try
                {
                    x = besselModifiedI1(x);
                    Assert.Fail("The modified bessel function Method failed to throw the ArgumentNullException for null input value");
                }
                catch (ArgumentNullException exc)
                {
                    Assert.IsTrue(exc.Message.Contains("null"), "The modified bessel function Method failed to throw the ArgumentNullException for null input value");
                }
            }
        }
        [TestMethod]
        public void BesselI1TestCoeffZeros()
        {
            using (Scope.Enter())
            {
                Array<double> x = zeros(1, 10);
                x = besselModifiedI1(x);
                Assert.IsTrue(maxall(x) == 0, "The modified bessel function Method failed to evaluate an array of zeros as input value");
            }
        }
        [TestMethod]
        public void BesselI1BigTestCoeffOnes()
        {
            using (Scope.Enter())
            {
                Array<double> x = 10 * ones(1, 10);
                x = besselModifiedI1(x);
                Assert.IsTrue(maxall(x) < minall(besselModifiedI0(20.0)), "The modified bessel function Method failed to evaluate an array of values bigger than 8 as input value");
            }
        }
        [TestMethod]
        public void BesselI12TestCoeffOnes()
        {
            using (Scope.Enter())
            {
                Array<double> x = ones(1, 10);
                x[2] = 0.5;
                x[4] = 0.25;
                x[6] = 0.75;
                x[1] = 50;
                x[3] = double.MaxValue;
                x[9] = double.NaN;
                x = besselModifiedI1(x);
                Array<double> Sol = besselModifiedI1(1.0) * ones(1, 10);
                Sol[2] = besselModifiedI1(0.5);
                Sol[4] = besselModifiedI1(0.25);
                Sol[6] = besselModifiedI1(0.75);
                Sol[1] = besselModifiedI1(50.0);
                Sol[3] = besselModifiedI1(double.MaxValue);
                Sol[9] = besselModifiedI1(double.NaN);
                Assert.IsTrue(isequalwithequalnans(x, Sol), "The modified bessel function Method failed to evaluate an array of different magnitude values");
            }
        }
    }
}
