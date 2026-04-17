using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILNumerics;
using static ILNumerics.Globals;
using static ILNumerics.ILMath;

namespace ILNumerics.Core.Tests {
    [TestClass]
    public class CreationHDimByParams32
        {

        [TestMethod]
        public void CreateEmptyParams32() {
            Array<double> A = empty<double>(1, 2, 3, 4, 5, 6, 7,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8);
            Assert.IsTrue(A.S.NumberOfDimensions == 32);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7 * 8);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);

            A = empty<double>(1, 2, 3, 4, 5, 6, 7,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8);
            Assert.IsTrue(A.S.NumberOfDimensions == 32);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7 * 8);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);

        }

        [TestMethod]
        public void Create_ZerosParams32() {
            Array<double> A = zeros<double>(1, 2, 3, 4, 5, 6, 7,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8);
            Assert.IsTrue(A.S.NumberOfDimensions == 32);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7 * 8);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(!anyall(A));

            A = zeros<double>(1, 2, 3, 4, 5, 6, 7,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8);
            Assert.IsTrue(A.S.NumberOfDimensions == 32);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7 * 8);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(!anyall(A));

        }
        [TestMethod]
        public void Create_OnesParams32() {
            Array<double> A = ones<double>(1, 2, 3, 4, 5, 6, 7,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8);
            Assert.IsTrue(A.S.NumberOfDimensions == 32);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7 * 8);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(allall(A == 1));

            A = ones<double>(1, 2, 3, 4, 5, 6, 7,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8);
            Assert.IsTrue(A.S.NumberOfDimensions == 32);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7 * 8);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(allall(A == 1));


        }
        [TestMethod]
        public void Create_RandParams32() {
            Array<double> A = rand(1, 2, 3, 4, 5, 6, 7,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8);
            Assert.IsTrue(A.S.NumberOfDimensions == 32);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7 * 8);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(!anyall(A < 0 | A > 1));

            A = rand(1, 2, 3, 4, 5, 6, 7,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8);
            Assert.IsTrue(A.S.NumberOfDimensions == 32);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7 * 8);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(!anyall(A < 0 | A > 1));

        }
        [TestMethod]
        public void Create_RandNParams32() {
            Array<double> A = randn(1, 2, 3, 4, 5, 6, 7,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8);
            Assert.IsTrue(A.S.NumberOfDimensions == 32);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7 * 8);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(!anyall(A < -100 | A > 100));

            A = randn(1, 2, 3, 4, 5, 6, 7,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8);
            Assert.IsTrue(A.S.NumberOfDimensions == 32);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7 * 8);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(!anyall(A < -100 | A > 100));

        }
    }
}
