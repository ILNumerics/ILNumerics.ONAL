using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;
using ILNumerics;

namespace ILNumerics.Core.UnitTests.MathInternalTests {
    [TestClass]
    public class MathInternal_linspaceTest {
        [TestMethod]
        public void MathInternal_linspace_simpleTest() {

            Array<double> A = linspace<double>(1, 100.0);
            Array<double> Res = counter<double>(1.0, 1.0, 100);
            Array<double> I1 = 1, I2 = 100, I3 = 100; 
            
            Assert.IsTrue(A.Equals(Res)); 
            Assert.IsTrue(A.Equals(linspace<double>(1, 100, 100))); 
            Assert.IsTrue(A.Equals(linspace<double>(I1, 100, 100))); 
            Assert.IsTrue(A.Equals(linspace<double>(1, I2, 100)));
            Assert.IsTrue(A.Equals(linspace<double>(1, I2.C, I3.T)));
            Assert.IsTrue(A.Equals(linspace<double>(1, 100, I3)));
        }
        [TestMethod]
        public void MathInternal_linspace_emptyTest() {

            Array<double> A = linspace<double>(1, 100.0, 0);
            Array<double> Res = counter<double>(100.0, 1.0, 0);
            Array<double> I1 = 1, I2 = 100, I3 = 0;

            Assert.IsTrue(A.Equals(Res));
            Assert.IsTrue(A.Equals(linspace<double>(1, 100, 0)));
            Assert.IsTrue(A.Equals(linspace<double>(I1, 100, 0)));
            Assert.IsTrue(A.Equals(linspace<double>(1, I2, 0)));
            Assert.IsTrue(A.Equals(linspace<double>(1, I2.C, I3.T)));
            Assert.IsTrue(A.Equals(linspace<double>(1, 100, I3)));
        }
        [TestMethod]
        public void MathInternal_linspace_Len1Test() {

            Array<double> A = linspace<double>(1, 100.0, 1);
            Array<double> Res = counter<double>(100.0, 1.0, 1);
            Array<double> I1 = 1, I2 = 100, I3 = 1;

            Assert.IsTrue(A.Equals(Res));
            Assert.IsTrue(A.Equals(linspace<double>(1, 100, 1)));
            Assert.IsTrue(A.Equals(linspace<double>(I1, 100, 1)));
            Assert.IsTrue(A.Equals(linspace<double>(1, I2, 1)));
            Assert.IsTrue(A.Equals(linspace<double>(1, I2.C, I3.T)));
            Assert.IsTrue(A.Equals(linspace<double>(1, 100, I3)));
        }
        [TestMethod]
        public void MathInternal_linspace_UInt32Test() {

            Array<uint> A = linspace<uint>(1, 100, 1);
            Array<uint> Res = counter<uint>(100, 1, 1);
            Array<uint> I1 = 1, I2 = 100, I3 = 1;

            Assert.IsTrue(A.Equals(Res));
            Assert.IsTrue(A.Equals(linspace<uint>(1, 100, 1)));
            Assert.IsTrue(A.Equals(linspace<uint>(I1, 100, 1)));
            Assert.IsTrue(A.Equals(linspace<uint>(1, I2, 1)));
            Assert.IsTrue(A.Equals(linspace<uint>(1, I2.C, I3.T)));
            Assert.IsTrue(A.Equals(linspace<uint>(1, 100, I3)));
        }

    }
}
