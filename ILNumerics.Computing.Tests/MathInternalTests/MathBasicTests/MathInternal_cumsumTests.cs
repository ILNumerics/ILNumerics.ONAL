using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;
using static ILNumerics.Globals; 

namespace ILNumerics.Core.UnitTests.MathInternalTests {
    /// <summary>
    /// Summary description for MathInternal_cumsumTests
    /// </summary>
    [TestClass]
    public class MathInternal_cumsumTests {

        [TestMethod]
        public void MathInternal_cumsum_simpleTest() {

            Array<double> A = counter(1.0, 1.0, 4, 3);
            Array<double> B = cumsum(A);
            Array<double> Res = new double[,] {
                { 1, 5, 9 },
                { 3, 11, 19 },
                { 6, 18, 30 },
                { 10, 26, 42 }
            };
            Assert.IsTrue(B.Equals(Res));

            A = counter(1.0, 1.0, 3, 4, StorageOrders.RowMajor);
            B = cumsum(A, 1);
            Assert.IsTrue(B.Equals(Res.T)); 

        }

        [TestMethod]
        public void MathInternal_cumsum_emptyTest() {

            Array<ushort> A = array<ushort>(0, vector<long>(4, 0, 5));

            Assert.IsTrue(A.IsEmpty);
            Assert.IsTrue(cumsum(A).S[0] == 4);
            Assert.IsTrue(cumsum(A).S[1] == 0);
            Assert.IsTrue(cumsum(A).S[2] == 5);

            Assert.IsTrue(cumsum(A, dim: 0).S[0] == 4);
            Assert.IsTrue(cumsum(A, dim: 0).S[1] == 0);
            Assert.IsTrue(cumsum(A, dim: 0).S[2] == 5);

            Assert.IsTrue(cumsum(A, dim: 1).S[0] == 4);
            Assert.IsTrue(cumsum(A, dim: 1).S[1] == 0);
            Assert.IsTrue(cumsum(A, dim: 1).S[2] == 5);

            Assert.IsTrue(cumsum(A, dim: 2).S[0] == 4);
            Assert.IsTrue(cumsum(A, dim: 2).S[1] == 0);
            Assert.IsTrue(cumsum(A, dim: 2).S[2] == 5);

            Assert.IsTrue(cumsum(A, dim: 3).S[0] == 4);
            Assert.IsTrue(cumsum(A, dim: 3).S[1] == 0);
            Assert.IsTrue(cumsum(A, dim: 3).S[2] == 5);

        }

        [TestMethod]
        public void MathInternal_cumsum_scalarTest() {

            Array<uint> A = 91;

            Assert.IsTrue(cumsum(A).IsScalar);
            Assert.IsTrue((uint)cumsum(A) == 91);
            Assert.IsTrue(cumsum(A).S.NumberOfDimensions == 2);
            Assert.IsTrue(cumsum(A).S[0] == 1); 
            Assert.IsTrue(cumsum(A).S[1] == 1);

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                A = 92;
                Assert.IsTrue(cumsum(A).IsScalar);
                Assert.IsTrue((uint)cumsum(A) == 92);
                Assert.IsTrue(cumsum(A).S.NumberOfDimensions == 0);
                Assert.IsTrue(cumsum(A).S[0] == 1);
                Assert.IsTrue(cumsum(A).S[1] == 1);

            }
        }
    }
}
