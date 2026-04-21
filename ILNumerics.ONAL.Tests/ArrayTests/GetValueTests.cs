// MIT License
// 
// Copyright (c) 2026 ILNumerics GmbH
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
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
