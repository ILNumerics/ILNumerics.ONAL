using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath; 

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public class zerosTests {
        [TestMethod]
        public void MathInternal_zerosCreation_1D() {

            Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
            Array<double> A = zeros(2);

            Assert.IsTrue(A.S[0] == 2);
            Assert.IsTrue(A.S[1] == 2);
            Assert.IsTrue(A.GetValue(0) == 0);
            Assert.IsTrue(A.GetValue(1) == 0);
            Assert.IsTrue(A.GetValue(2) == 0);
            Assert.IsTrue(A.GetValue(3) == 0);
            Assert.IsTrue(A.S.NumberOfDimensions == 2);

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                A = zeros<double>(2);

                Assert.IsTrue(A.S[0] == 2);
                Assert.IsTrue(A.S[1] == 2);
                Assert.IsTrue(A.GetValue(0) == 0);
                Assert.IsTrue(A.GetValue(1) == 0);
                Assert.IsTrue(A.GetValue(2) == 0);
                Assert.IsTrue(A.GetValue(3) == 0);
                Assert.IsTrue(A.S.NumberOfDimensions == 2);

            }
        }
        [TestMethod]
        public void MathInternal_onesCreation_2D() {

            Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
            Array<double> A = zeros<double>(2, 3);

            Assert.IsTrue(A.S[0] == 2);
            Assert.IsTrue(A.S[1] == 3);
            foreach (var v in A) {
                Assert.IsTrue(v == 0);
            }
            Assert.IsTrue(A.Equals(zeros(2, 3))); 
        }

        [TestMethod]
        public void MathInternal_arrayCreation_3D_Bool() {
            Array<bool> A = zeros<bool>(2, 3); // not a known element type
            // this works! (not as for ones()!)
            foreach (var a in A)
                Assert.IsTrue(a == false);
        }
        [TestMethod]
        public void MathInternal_arrayCreation_3D_empty() {
            Array<bool> A = zeros<bool>(0, 3, 5); // not a known element type
            // this works! (not as for ones()!)
            foreach (var a in A)
                Assert.IsTrue(a == false);
        }

        [TestMethod]
        public void MathInternal_arrayCreation_StorageOrderDefColumnMaj() {
            Array<uint> A = zeros<uint>(2, 3); 
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                A = zeros<uint>(2, 3);
                Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor); // anyway! This is a Math. function! 

                A = zeros<uint>(2, 3, StorageOrders.RowMajor); // must be explicit! 
                Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor); 
            }

        }
    }
}
