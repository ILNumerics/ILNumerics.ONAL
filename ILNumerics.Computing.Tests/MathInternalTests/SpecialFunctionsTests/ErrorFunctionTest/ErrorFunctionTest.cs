using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILNumerics; 
using static ILNumerics.Globals;
using static ILNumerics.ILMath;
namespace SpecialFunctionsTest
{
    [TestClass]
    public class ErrorFunctionTest 
    {
        [TestMethod]
        public void ErfTestNull()
        {
            using (Scope.Enter())
            {
                Array<double> x = null;
                try
                {
                    x = errorFunction(x);
                    Assert.Fail("The error function Method failed to throw the ArgumentNullException for null input value");
                }
                catch (ArgumentNullException exc)
                {
                    Assert.IsTrue(exc.Message.Contains("null"), "The error function Method failed to throw the ArgumentNullException for null input value");
                }
            }
        }
        [TestMethod]
        public void ErfTestCoeffZeros()
        {
            using (Scope.Enter())
            {
                Array<double> x = zeros(1, 10);
                x = errorFunction(x);
                Assert.IsTrue((norm(x)) <= 1e-10, "The error function Method failed to throw the ArgumentOutOfRangeException for zeros input value");
            }
        }
        [TestMethod]
        public void ErfTestCoeffOnes()
        {
            using (Scope.Enter())
            {
                Array<double> x = ones(1, 10);
                x[2] = 0.5;
                x[4] = 0.25;
                x[6] = 0.75;
                x = errorFunction(x);
                Array<double> Sol = errorFunction(1.0)*ones(1, 10);
                Sol[2] = errorFunction(0.5);
                Sol[4] = errorFunction(0.25);
                Sol[6] = errorFunction(0.75);
                Assert.IsTrue(isequalwithequalnans(x, Sol), "The error function Method failed to throw the ArgumentOutOfRangeException for zeros input value");
            }
        }
        [TestMethod]
        public void ErfTestBigCoeffOnes()
        {
            using (Scope.Enter())
            {
                Array<double> x = 5.0 * ones(1, 10);
                x[2] = 0.5;
                x[4] = 4.25;
                x[6] = 0.75;
                x = errorFunction(x);
                Array<double> Sol = errorFunction(5.0) * ones(1, 10);
                Sol[2] = errorFunction(0.5);
                Sol[4] = errorFunction(4.25);
                Sol[6] = errorFunction(0.75);
                Assert.IsTrue(isequalwithequalnans(x, Sol), "The error function Method failed to throw the ArgumentOutOfRangeException for zeros input value");
            }
        }
        [TestMethod]
        public void ErfTestNaNinftyZeros()
        {
            using (Scope.Enter())
            {
                Array<double> x = 5.0 * ones(1, 10);
                x[2] = 0.0;
                x[4] = double.NegativeInfinity;
                x[6] = double.PositiveInfinity;
                x[7] = double.NaN;
                x = errorFunction(x);
                Array<double> Sol = errorFunction(5.0) * ones(1, 10);
                Sol[2] = 0.0;
                Sol[4] = -1.0;
                Sol[6] = 1.0;
                Sol[7] = double.NaN;
                Assert.IsTrue(isequalwithequalnans(x, Sol), "The error function Method failed to throw the ArgumentOutOfRangeException for zeros input value");
            }
        }

        [TestMethod]
        public void ErfShapeTest()
        {
            double val = errorFunction(-100).GetValue(0);
            Assert.AreEqual(val, -1.0, 0.00005);

            val = errorFunction(-50).GetValue(0);
            Assert.AreEqual(val, -1.0, 0.00005);

            val = errorFunction(-10).GetValue(0);
            Assert.AreEqual(val, -1.0, 0.00005);

            val = errorFunction(+10).GetValue(0);
            Assert.AreEqual(val, +1.0, 0.00005);

            val = errorFunction(+50).GetValue(0);
            Assert.AreEqual(val, +1.0, 0.00005);

            val = errorFunction(+100).GetValue(0);
            Assert.AreEqual(val, +1.0, 0.00005);

            val = errorFunction(0).GetValue(0);
            Assert.AreEqual(val, 0, 0.00005);

            // test monotonity +half
            Assert.IsTrue(errorFunction(0) < errorFunction(0.5));
            Assert.IsTrue(errorFunction(0.5) < errorFunction(1));
            Assert.IsTrue(errorFunction(2) < errorFunction(5));
            Assert.IsTrue(errorFunction(5) < errorFunction(10));

            // test monotonity -half
            Assert.IsTrue(errorFunction(0) > errorFunction(-0.5));
            Assert.IsTrue(errorFunction(-0.5) > errorFunction(-1));
            Assert.IsTrue(errorFunction(-2) > errorFunction(-5));
            Assert.IsTrue(errorFunction(-5) > errorFunction(-10));

            // test symmetry
            Assert.AreEqual((double)-errorFunction(0.1), (double)errorFunction(-0.1), 1e-6);
            Assert.AreEqual((double)-errorFunction(0.5), (double)errorFunction(-0.5), 1e-6);
            Assert.AreEqual((double)-errorFunction(1), (double)errorFunction(-1), 1e-6);
            Assert.AreEqual((double)-errorFunction(2), (double)errorFunction(-2), 1e-6);
            Assert.AreEqual((double)-errorFunction(5), (double)errorFunction(-5), 1e-6);
            Assert.AreEqual((double)-errorFunction(10), (double)errorFunction(-10), 1e-6);

            // edges
            Assert.AreEqual(1.0, errorFunction(double.PositiveInfinity));
            Assert.AreEqual(-1.0, errorFunction(double.NegativeInfinity));
        }
    }
}
