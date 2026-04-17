using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public class ArrayGetHashCodeTests {
        [TestMethod]
        public void ArrayGetHashCodeTest() {

            Array<uint> A = new uint[,] {
                { 1, 2, 3 },
                { 4, 5, 6 }
            };

            Array<uint> B = new uint[,] {
                { 1, 2 },
                { 3, 4 },
                { 5, 6 }
            };
            var ha = A.GetHashCode();
            var hb = B.GetHashCode();
            var ha2 = A.GetHashCode();
            Assert.IsTrue(ha != 0);
            Assert.IsTrue(ha2 != 0);
            Assert.IsTrue(ha == ha2);

            Assert.IsTrue(hb != 0);
            Assert.IsTrue(ha != hb);

            Array<uint> C = A.C;
            Assert.IsTrue(ha == C.GetHashCode());

        }
        [TestMethod]
        public void ArrayGetHashCodeTestempty() {

            Array<double> A = new double[0];

            Assert.IsTrue(A.GetHashCode() != 0);

        }
    }
}
