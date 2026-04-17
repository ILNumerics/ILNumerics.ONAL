using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath; 
using static ILNumerics.Globals;

namespace ILNumerics.Core.UnitTests.MathInternalTests {

    [TestClass]
    public class MathInternal_vecTests {

        [TestMethod]
        public void MathInternal_vec_simple() {

            Array<double> A = arange(1.0, 4.0); 
            Assert.IsTrue(A.Equals(vector<double>(1, 2, 3, 4)));
        }
        [TestMethod]
        public void MathInternal_vec_rev() {

            Array<double> A = arange(4.0, -1, 1.0);
            Assert.IsTrue(A.Equals(vector<double>(4, 3, 2, 1)));
        }
        [TestMethod]
        public void MathInternal_vec_lgFloat() {

            Array<float> A = arange(10000f, -1, 0);
            Assert.IsTrue(A.Equals(counter<float>(10000, -1, 10001)));
        }
        [TestMethod]
        public void MathInternal_vec_empty() {

            Assert.IsTrue(arange(1.0, 1, 1.0).Equals(vector<double>(1)));
            Assert.IsTrue(arange(1.0, 1, 1.0).Equals(vector<double>(1)));
        }
    }
}
