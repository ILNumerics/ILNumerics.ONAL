using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILNumerics; 
using static ILNumerics.Globals;
using static ILNumerics.ILMath;


namespace SpecialFunctionsTest
{
    [TestClass]
    public class BesselTestnull
    {
        [TestMethod]
        public void BesselI0TestNull()
        {
            using (Scope.Enter())
            {
                Array<double> x = null;
                try
                {
                    x = besselModifiedI0(x);
                    Assert.Fail("The modified bessel function Method failed to throw the ArgumentNullException for null input value");
                }
                catch (ArgumentNullException exc)
                {
                    Assert.IsTrue(exc.Message.Contains("null"), "The modified bessel function Method failed to throw the ArgumentNullException for null input value");
                }
            }
        }
        [TestMethod]
        public void BesselI0TestCoeffZeros()
        {
            using (Scope.Enter())
            {
                Array<double> x = zeros(1, 10);
                x = besselModifiedI0(x);
                Assert.IsTrue((maxall(x) - 1) < 1e-6, "The modified bessel function Method failed to evaluate an array of zeros as input value");
            }
        }
        [TestMethod]
        public void BesselI0TestCoeffOnes()
        {
            using (Scope.Enter())
            {
                Array<double> x = 20*ones(1, 10);
                x = besselModifiedI0(x);
                Assert.IsTrue(maxall(x) == besselModifiedI0(20.0), "The modified bessel function Method failed to evaluate an array of values bigger than 8 as input value");
            }
        }
        [TestMethod]
        public void BesselI1TestCoeffOnes()
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
                x = besselModifiedI0(x);
                Array<double> Sol = besselModifiedI0(1.0) * ones(1, 10);
                Sol[2] = besselModifiedI0(0.5);
                Sol[4] = besselModifiedI0(0.25);
                Sol[6] = besselModifiedI0(0.75);
                Sol[1] = besselModifiedI0(50.0);
                Sol[3] = besselModifiedI0(double.MaxValue);
                Sol[9] = besselModifiedI0(double.NaN);
                Assert.IsTrue(isequalwithequalnans(x, Sol), "The modified bessel function Method failed to evaluate an array of different magnitude values");
            }
        }
    }
}
