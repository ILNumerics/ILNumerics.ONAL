using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath; 

namespace ILNumerics.Core.UnitTests.MathInternalTests {
    [TestClass]
    public class MathInternal_counterTests {
        [TestMethod]
        public void MathInternal_counter_operatorDblTest() {
            // (this test is redundant with a ___sum_ test)

            Array<double> A = counter(1.0, 1.0, 5, 4); // new[] { 10, 20 }; // 

            Array<double> B = sum(A, 0);
            Array<double> Res = new double[] { 15, 40, 65, 90 };
            Assert.IsTrue(Res.Equals(B.T));
        }
        [TestMethod]
        public void MathInternal_counter_operatorDblParallelTest() {

            Array<double> A = 1; 
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 4u)) {
                A.a = counter(1.0, 1.0, 500, 4);
            }
            Array<double> B = sum(A, 0);
            Array<double> Res = new double[] { 125250, 375250, 625250, 875250 };
            Assert.IsTrue(Res.Equals(B.T));
        }
    }
}
