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
