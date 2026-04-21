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
