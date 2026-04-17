using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;

namespace ILNumerics.Core.UnitTests.numpy_attributes_Tests {

    [TestClass]
    public class flattenTests {

        [TestMethod]
        public void numpy_flatten_simple() {
            Array<float> A = counter<float>(1f, 1f, 5, 6, StorageOrders.RowMajor);
            Array<float> B = A.flatten();

            Assert.IsTrue(B.Equals(counter<float>(1f, 1f, 5 * 6, StorageOrders.RowMajor)));

        }
        [TestMethod]
        public void numpy_flatten_ColumnMajor() {

            Array<float> A = counter<float>(1f, 1f, 5, 6);
            Array<float> B = A.flatten(StorageOrders.ColumnMajor);

            Assert.IsTrue(B.Equals(counter<float>(1f, 1f, 5 * 6)));

        }

        [TestMethod]
        public void numpy_flatten_scalarempty() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                Array<double> A = 1;
                var B = A.flatten();

                Assert.IsTrue(B.Storage.GetValue(0) == 1);
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 1); //sic: 1D
                Assert.IsTrue(B.Storage.S.NumberOfElements == 1);

                Array<ulong> C = empty<ulong>(2, dim1: 0);
                var D = C.flatten();
                Assert.IsTrue(D.Storage.S.NumberOfDimensions == 1); //sic: 1D
                Assert.IsTrue(D.Storage.S.NumberOfElements == 0);

                Array<double> E = B;
                E.SetValue(-1, 0); 

                Assert.IsTrue(E == -1); 
                Assert.IsTrue(A == 1); 
            }
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void numpy_flatten_OtherOrderFails() {
            Array<double> A = 1;
            var B = A.flatten(StorageOrders.Other);
        }
        [TestMethod]
        public void numpy_flatten_releaseRetT() {
            var A = counter<double>(1.0, 1.0, 5, 4, 3);
            Array<double> B = A.flatten();
            B.flatten();

        }
    }
}
