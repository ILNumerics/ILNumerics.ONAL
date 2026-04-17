using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public class GetValueTests {

        [TestMethod]
        public void GetValueNegInd1dOnNpScalarTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = 4;
                Assert.IsTrue(A.S.NumberOfDimensions == 0);
                Assert.IsTrue(A.GetValue(-1) == 4);
                Assert.IsTrue(A.GetValue(0) == 4);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetValueNegInd1dOnNpScalarOORNegFailTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = 4;
                Assert.IsTrue(A.S.NumberOfDimensions == 0);
                Assert.IsTrue(A.GetValue(-2) == 4);
            }

        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetValueNegInd1dOnNpScalarOORPosFailTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = 4;
                Assert.IsTrue(A.S.NumberOfDimensions == 0);
                Assert.IsTrue(A.GetValue(1) == 4);
            }

        }
        [TestMethod]
        public void GetValueNegInd1dOnMLScalarTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {
                Array<double> A = 3;
                Assert.IsTrue(A.S.NumberOfDimensions == 2);
                Assert.IsTrue(A.GetValue(-1) == 3);
                Assert.IsTrue(A.GetValue(0) == 3);
            }
        }
        [TestMethod]
        public void GetValueNegInd1dOnMLVectorTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {
                Array<double> A = new double[] { 1, 2, 3, 4 };
                Assert.IsTrue(A.S.NumberOfDimensions == 2);
                Assert.IsTrue(A.GetValue(-1) == 4);
                Assert.IsTrue(A.GetValue(-2) == 3);
                Assert.IsTrue(A.GetValue(-3) == 2);
                Assert.IsTrue(A.GetValue(-4) == 1);
            }
        }
        [TestMethod]
        public void GetValueNegInd1dOnVectorTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                Array<double> A = new double[] {  1, 2, 3, 4 };
                Assert.IsTrue(A.S.NumberOfDimensions == 1);
                Assert.IsTrue(A.GetValue(-1) == 4);
                Assert.IsTrue(A.GetValue(-2) == 3);
                Assert.IsTrue(A.GetValue(-3) == 2);
                Assert.IsTrue(A.GetValue(-4) == 1);
            }
        }
        [TestMethod]
        public void GetValueNegInd1dOnMatrixTest() {
            Array<double> A = new double[,] { { 1, 2 }, { 3, 4 } };
            Assert.IsTrue(A.GetValue(-1) == 4);
            Assert.IsTrue(A.GetValue(-2) == 2);
            Assert.IsTrue(A.GetValue(-3) == 3);
            Assert.IsTrue(A.GetValue(-4) == 1);
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetValue1dOORPosFailTest() {
            Array<double> A = new double[] { 1, 2 };
            A.GetValue(2);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetValue1dOORNegFailTest() {
            Array<double> A = new double[] { 1, 2 };
            A.GetValue(-3); 
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetValue1dOOREmptyFailLongTest() {
            Array<double> A = new double[] { };

            A.GetValue((long)0); 
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetValue1dOOREmptyFailUIntTest() {
            Array<double> A = new double[] { };

            A.GetValue((uint)0); 
        }
    }
}
