using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath; 

namespace ILNumerics.Core.UnitTests.MathInternalTests {
    /// <summary>
    /// Summary description for MathInternal_randTests
    /// </summary>
    [TestClass]
    public class MathInternal_randTests {

        [TestMethod]
        public void MathInternal_rand_simple1D() {
            Array<double> A = rand(5);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.ILNumericsV4 && A.S.NumberOfDimensions == 2);
            Assert.IsTrue(A.S[0] == 5 && A.S[1] == 5);

            double last = -1;
            foreach (var a in A) {
                Assert.IsTrue(a != last);
                last = a;
            }
        }
        [TestMethod]
        public void MathInternal_rand_simple2D_rowMaj() {
            Array<double> A = rand(5, 4, StorageOrders.RowMajor);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor);
            Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.ILNumericsV4 && A.S.NumberOfDimensions == 2);
            Assert.IsTrue(A.S[0] == 5 && A.S[1] == 4);

            double last = -1;
            foreach (var a in A) {
                Assert.IsTrue(a != last);
                last = a;
            }
        }
        [TestMethod]
        public void MathInternal_rand_simple3D_rowMaj_empty() {
            Array<double> A = rand(5, 4, 0, StorageOrders.RowMajor);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor);
            Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.ILNumericsV4 && A.S.NumberOfDimensions == 3);
            Assert.IsTrue(A.S[0] == 5 && A.S[1] == 4 && A.S[2] == 0);

            double last = -1;
            foreach (var a in A) {
                Assert.IsTrue(a != last);
                last = a;
            }
        }
        [TestMethod]
        public void MathInternal_rand_simple7D_colMaj() {
            Array<double> A = rand(5, 4, 1, 2, 3, 4, 2, StorageOrders.ColumnMajor);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.ILNumericsV4 && A.S.NumberOfDimensions == 7);
            Assert.IsTrue(A.S[0] == 5 && A.S[1] == 4 && A.S[2] == 1 && A.S[3] == 2);
            Assert.IsTrue(A.S[4] == 3 && A.S[5] == 4 && A.S[6] == 2);

            double last = -1;
            foreach (var a in A) {
                Assert.IsTrue(a != last);
                last = a;
            }
        }
        [TestMethod]
        public void MathInternal_rand_LargeParall7D_rowMaj() {
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {
                Array<double> A = rand(50, 40, 10, 2, 3, 4, 2, StorageOrders.RowMajor);
                Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor);
                Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.ILNumericsV4 && A.S.NumberOfDimensions == 7);
                Assert.IsTrue(A.S[0] == 50 && A.S[1] == 40 && A.S[2] == 10 && A.S[3] == 2);
                Assert.IsTrue(A.S[4] == 3 && A.S[5] == 4 && A.S[6] == 2);

                double last = -1;
                foreach (var a in A) {
                    Assert.IsTrue(a != last);
                    last = a;
                }
            }
        }
    }
}
