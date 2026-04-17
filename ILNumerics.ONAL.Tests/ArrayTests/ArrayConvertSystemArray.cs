using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public class ArrayConvertSystemArray {

        [TestMethod]
        public void Convert1DColumnMajorTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Assert.IsTrue(Settings.DefaultStorageOrder == StorageOrders.ColumnMajor);
                Array<uint> A = new uint[] { 1, 2, 3, 4, 5 };
                Assert.IsTrue(Settings.MinNumberOfArrayDimensions == 2);
                // single dim vectors are both: column and row major
                Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor || A.S.StorageOrder == StorageOrders.ColumnMajor);

                Assert.IsTrue(A.S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions);
                Assert.IsTrue(A.S[0] == 5);
                Assert.IsTrue(A.S[1] == 1);
                Assert.IsTrue(A.S.GetStride(0) == 1);
                Assert.IsTrue(A.S.GetStride(1) == 0);

            }
        }

        [TestMethod]
        public void Convert1DRowMajorTest() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) { 

                Assert.IsTrue(Settings.DefaultStorageOrder == StorageOrders.RowMajor);
                Array<uint> A = new uint[] { 1, 2, 3, 4, 5 };
                Assert.IsTrue(Settings.MinNumberOfArrayDimensions == 0);
                // single dim vectors are both: column and row major
                Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor || A.S.StorageOrder == StorageOrders.ColumnMajor);

                Assert.IsTrue(A.S.NumberOfDimensions == 1);
                Assert.IsTrue(A.S[0] == 5);
                Assert.IsTrue(A.S[1] == 1);
                Assert.IsTrue(A.S.GetStride(0) == 1);
                Assert.IsTrue((ulong)A.S.GetStride(1) == 0);

            }
        }
    }
}
