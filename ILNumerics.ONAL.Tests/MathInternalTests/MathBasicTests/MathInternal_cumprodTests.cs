using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;
using static ILNumerics.Globals; 

namespace ILNumerics.Core.UnitTests.MathInternalTests {
    /// <summary>
    /// Summary description for MathInternal_cumprodTests
    /// </summary>
    [TestClass]
    public class MathInternal_cumprodTests {

        [TestMethod]
        public void MathInternal_cumprod_simpleTest() {

            Array<double> A = counter(1.0, 1.0, 4, 3);
            Array<double> B = cumprod(A);
            Array<double> Res = new double[,] {
                { 1, 5, 9 },
                { 2, 30, 90 },
                { 6, 210, 90*11 },
                { 24, 1680, 90*11*12 }
            };
            Assert.IsTrue(B.Equals(Res));

            A = counter(1.0, 1.0, 3, 4, StorageOrders.RowMajor);
            B = cumprod(A, 1);
            Assert.IsTrue(B.Equals(Res.T));

            A.Storage.EnsureStorageOrder(StorageOrders.ColumnMajor);
            B = cumprod(A, 1);
            Assert.IsTrue(B.Equals(Res.T));

        }

        [TestMethod]
        public void MathInternal_cumprod_emptyTest() {

            Array<ushort> A = array<ushort>(0, vector<long>(4, 0, 5));

            Assert.IsTrue(A.IsEmpty);
            Assert.IsTrue(cumprod(A).S[0] == 4);
            Assert.IsTrue(cumprod(A).S[1] == 0);
            Assert.IsTrue(cumprod(A).S[2] == 5);

            Assert.IsTrue(cumprod(A, dim: 0).S[0] == 4);
            Assert.IsTrue(cumprod(A, dim: 0).S[1] == 0);
            Assert.IsTrue(cumprod(A, dim: 0).S[2] == 5);

            Assert.IsTrue(cumprod(A, dim: 1).S[0] == 4);
            Assert.IsTrue(cumprod(A, dim: 1).S[1] == 0);
            Assert.IsTrue(cumprod(A, dim: 1).S[2] == 5);

            Assert.IsTrue(cumprod(A, dim: 2).S[0] == 4);
            Assert.IsTrue(cumprod(A, dim: 2).S[1] == 0);
            Assert.IsTrue(cumprod(A, dim: 2).S[2] == 5);

            Assert.IsTrue(cumprod(A, dim: 3).S[0] == 4);
            Assert.IsTrue(cumprod(A, dim: 3).S[1] == 0);
            Assert.IsTrue(cumprod(A, dim: 3).S[2] == 5);

        }

        [TestMethod]
        public void MathInternal_cumprod_scalarTest() {

            Array<uint> A = 91;

            Assert.IsTrue(cumprod(A).IsScalar);
            Assert.IsTrue((uint)cumprod(A) == 91);
            Assert.IsTrue(cumprod(A).S.NumberOfDimensions == 2);
            Assert.IsTrue(cumprod(A).S[0] == 1); 
            Assert.IsTrue(cumprod(A).S[1] == 1);

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                A = 92;
                Assert.IsTrue(cumprod(A).IsScalar);
                Assert.IsTrue((uint)cumprod(A) == 92);
                Assert.IsTrue(cumprod(A).S.NumberOfDimensions == 0);
                Assert.IsTrue(cumprod(A).S[0] == 1);
                Assert.IsTrue(cumprod(A).S[1] == 1);

            }
        }
    }
}
