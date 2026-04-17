using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath; 

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public class onesTests {
        [TestMethod]
        public void MathInternal_onesCreation_1D() {

            Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
            Array<double> A = ones<double>(2);

            Assert.IsTrue(A.S[0] == 2);
            Assert.IsTrue(A.S[1] == 2);
            Assert.IsTrue(A.GetValue(0) == 1);
            Assert.IsTrue(A.GetValue(1) == 1);
            Assert.IsTrue(A.S.NumberOfDimensions == 2);

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                A = ones<double>(2);

                Assert.IsTrue(A.S[0] == 2);
                Assert.IsTrue(A.S[1] == 2);
                Assert.IsTrue(A.GetValue(0) == 1.0);
                Assert.IsTrue(A.GetValue(1) == 1.0);
                Assert.IsTrue(A.S.NumberOfDimensions == 2);

            }
        }
        [TestMethod]
        public void MathInternal_onesCreation_2D() {

            Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
            Array<double> A = ones<double>(2, 3);

            Assert.IsTrue(A.S[0] == 2);
            Assert.IsTrue(A.S[1] == 3);
            foreach (var v in A) {
                Assert.IsTrue(v == 1.0);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MathInternal_arrayCreation_3D_Ref() {
            Array<bool> A = ones<bool>(2, 3); // not a known element type
        }

        [TestMethod]
        public void MathInternal_arrayCreation_StorageOrderDefColumnMaj() {
            Array<uint> A = ones<uint>(2, 3); // not a known element type
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                A = ones<uint>(2, 3);
                Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor); // anyway! This is a Math. function! 

                A = ones<uint>(2, 3, StorageOrders.RowMajor); // must be explicit! 
                Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor); 
            }

        }
    }
}
