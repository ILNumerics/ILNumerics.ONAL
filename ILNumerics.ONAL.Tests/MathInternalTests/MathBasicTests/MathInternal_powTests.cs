using System;
using ILNumerics.Core.Functions.Builtin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ILNumerics.Core.UnitTests.MathInternalTests {
    [TestClass]
    public class MathInternal_powTests {
        [TestMethod]
        public void MathInternal_pow_SimpleTest() {
            Array<double> A = 0.1;
            Array<double> B = -1;

            Array<double> R = MathInternal.pow(A, B);
            Assert.IsTrue(R.GetValue(0).Equals(1.0 / 0.1));
        }
        [TestMethod]
        public void MathInternal_pow_sat_SimpleTest() {
            Array<short> A = short.MaxValue / 2;
            Array<short> B = 2;

            Array<short> R = MathInternal.pow_sat(A, B);
            Assert.IsTrue(R.GetValue(0).Equals(short.MaxValue));
        }
    }
}
