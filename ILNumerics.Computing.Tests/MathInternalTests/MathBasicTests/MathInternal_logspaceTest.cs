using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;
using ILNumerics;

namespace ILNumerics.Core.UnitTests.MathInternalTests {
    [TestClass]
    public class MathInternal_logspaceTest {
        [TestMethod]
        public void MathInternal_logspace_simpleTest() {

            Array<double> A = logspace(1, 5.0);
            Array<double> Res = pow(10, linspace<double>(1.0, 5.0, 50));
            Array<double> I1 = 1, I2 = 5, I3 = 50; 
            
            Assert.IsTrue(A.Equals(Res)); 
            Assert.IsTrue(A.Equals(logspace(1, 5, 50))); 
            Assert.IsTrue(A.Equals(logspace(I1, 5, 50))); 
            Assert.IsTrue(A.Equals(logspace(1, I2, 50)));
            Assert.IsTrue(A.Equals(logspace(1, I2.C, I3.T)));
            Assert.IsTrue(A.Equals(logspace(1, 5, I3)));
        }
        [TestMethod]
        public void MathInternal_logspace_emptyTest() {

            Array<double> A = logspace(1, 5.0, 0);
            Array<double> Res = counter<double>(5.0, 1.0, 0);
            Array<double> I1 = 1, I2 = 100, I3 = 0;

            Assert.IsTrue(A.Equals(Res));
            Assert.IsTrue(A.Equals(logspace(1, 100, 0)));
            Assert.IsTrue(A.Equals(logspace(I1, 100, 0)));
            Assert.IsTrue(A.Equals(logspace(1, I2, 0)));
            Assert.IsTrue(A.Equals(logspace(1, I2.C, I3.T)));
            Assert.IsTrue(A.Equals(logspace(1, 100, I3)));
        }
        [TestMethod]
        public void MathInternal_logspace_Len1Test() {

            Array<double> A = logspace(1, 5.0, 1);
            Array<double> Res = pow(10, counter<double>(5.0, 1.0, 1));
            Array<double> I1 = 1, I2 = 5, I3 = 1;

            Assert.IsTrue(A.Equals(Res));
            Assert.IsTrue(A.Equals(logspace(1, 5, 1)));
            Assert.IsTrue(A.Equals(logspace(I1, 5, 1)));
            Assert.IsTrue(A.Equals(logspace(1, I2, 1)));
            Assert.IsTrue(A.Equals(logspace(1, I2.C, I3.T)));
            Assert.IsTrue(A.Equals(logspace(1, 5, I3)));
        }
    }
}
