using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILNumerics;
using static ILNumerics.Globals;
using static ILNumerics.ILMath;

namespace ILNumerics.Core.Tests {
    [TestClass]
    public class CreationHDimByArray {

        [TestMethod]
        public void CreateEmpty8() {
            Array<double> A = empty<double>(vector<long>(1, 2, 3, 4, 5, 6, 7, 8));
            Assert.IsTrue(A.S.NumberOfDimensions == 8);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7 * 8);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);

            A = empty<double>(vector<long>(1, 2, 3, 4, 5, 6, 7, 8), StorageOrders.ColumnMajor);
            Assert.IsTrue(A.S.NumberOfDimensions == 8);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7 * 8);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);

            A = empty<double>(vector<long>(1, 2, 3, 4, 5, 6, 7, 8), StorageOrders.RowMajor);
            Assert.IsTrue(A.S.NumberOfDimensions == 8);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7 * 8);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor);

        }

        [TestMethod]
        public void Create_Zeros8() {
            Array<double> A = zeros<double>(vector<long>(1, 2, 3, 4, 5, 6, 7, 8));
            Assert.IsTrue(A.S.NumberOfDimensions == 8);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7 * 8);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(!anyall(A));

            A = zeros<double>(vector<long>(1, 2, 3, 4, 5, 6, 7, 8), StorageOrders.ColumnMajor);
            Assert.IsTrue(A.S.NumberOfDimensions == 8);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7 * 8);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(!anyall(A));

            A = zeros<double>(vector<long>(1, 2, 3, 4, 5, 6, 7, 8), StorageOrders.RowMajor);
            Assert.IsTrue(A.S.NumberOfDimensions == 8);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7 * 8);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor);
            Assert.IsTrue(!anyall(A));

        }
        [TestMethod]
        public void Create_Ones8() {
            Array<double> A = ones<double>(vector<long>(1, 2, 3, 4, 5, 6, 7, 8));
            Assert.IsTrue(A.S.NumberOfDimensions == 8);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7 * 8);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(allall(A == 1));

            A = ones<double>(vector<long>(1, 2, 3, 4, 5, 6, 7, 8), StorageOrders.ColumnMajor);
            Assert.IsTrue(A.S.NumberOfDimensions == 8);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7 * 8);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(allall(A == 1));

            A = ones<double>(vector<long>(1, 2, 3, 4, 5, 6, 7, 8), StorageOrders.RowMajor);
            Assert.IsTrue(A.S.NumberOfDimensions == 8);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7 * 8);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor);
            Assert.IsTrue(allall(A == 1));

        }
        [TestMethod]
        public void Create_Rand8() {
            Array<double> A = rand(vector<long>(1, 2, 3, 4, 5, 6, 7, 8));
            Assert.IsTrue(A.S.NumberOfDimensions == 8);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7 * 8);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(!anyall(A < 0 | A > 1));

            A = rand(vector<long>(1, 2, 3, 4, 5, 6, 7, 8), StorageOrders.ColumnMajor);
            Assert.IsTrue(A.S.NumberOfDimensions == 8);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7 * 8);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(!anyall(A < 0 | A > 1));

            A = rand(vector<long>(1, 2, 3, 4, 5, 6, 7, 8), StorageOrders.RowMajor);
            Assert.IsTrue(A.S.NumberOfDimensions == 8);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7 * 8);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor);
            Assert.IsTrue(!anyall(A < 0 | A > 1));

        }
        [TestMethod]
        public void Create_RandN8() {
            Array<double> A = randn(vector<long>(1, 2, 3, 4, 5, 6, 7, 8));
            Assert.IsTrue(A.S.NumberOfDimensions == 8);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7 * 8);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(!anyall(A < -100 | A > 100));

            A = randn(vector<long>(1, 2, 3, 4, 5, 6, 7, 8), StorageOrders.ColumnMajor);
            Assert.IsTrue(A.S.NumberOfDimensions == 8);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7 * 8);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(!anyall(A < -100 | A > 100));

            A = randn(vector<long>(1, 2, 3, 4, 5, 6, 7, 8), StorageOrders.RowMajor);
            Assert.IsTrue(A.S.NumberOfDimensions == 8);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7 * 8);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor);
            Assert.IsTrue(!anyall(A < -100 | A > 100));

        }

        [TestMethod]
        public void Casting_multidim_32() {
            using (Scope.Enter(ArrayStyles.numpy)) {
                for (int i = 1; i <= Size.MaxNumberOfDimensions; i++) {
                    var lengths = new int[i];
                    for (int l = 1; l <= i; l++) {
                        lengths[l - 1] = (l - 1) % 2 + 1;
                    }
                    try {
                        var a = Array.CreateInstance(typeof(double), lengths);
                        Assert.IsTrue(a != null && a.Rank == i);
                        Array<double> A = a;
                        Assert.IsTrue(A.shape.Equals(toint64(vector(lengths))), A.shape.ToString());
                    } catch (Exception exc) {
                        Assert.Fail($"Failed at: {i}. Exception: {exc.ToString()}");
                    }
                }
            }
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void Counter33Fail() {

            counter<double>(1.0, 1.0, ones<long>(33, 1), order: StorageOrders.ColumnMajor);

        }

        //[TestMethod]
        //public void MyTestMethod() {
        //    Array<double> array = arange(0.0, 10.0);
        //    // Array<double> subArray = array[slice(9, 0, -2)]; // error: no neg. step allowed
        //    Array<double> subArray = array[arange(9, -2, 0)];

        //    subArray = array[vector(9,7,5,3,1)];
        //    subArray = array[counter<int>(9, -2, 5)];
        //    subArray = flipud(array[slice(1, end, 2)]); 

        //}
    }
}
