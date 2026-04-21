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
    public class itemTests {

        [TestMethod]
        public void numpy_itemSimpleNumpyStyleTest() {

            using (Scope.Enter(ArrayStyles.numpy)) {
                Array<double> A = -9;
                Assert.IsTrue(A.item(0) == -9);
                Assert.IsTrue(A.item(0, 0) == -9);
                Assert.IsTrue(A.item(0, 0, 0) == -9);
                Assert.IsTrue(A.item(0, 0, 0, 0) == -9);
                Assert.IsTrue(A.item(0, 0, 0, 0, 0) == -9);
                Assert.IsTrue(A.item(0, 0, 0, 0, 0, 0) == -9);
                Assert.IsTrue(A.item(0, 0, 0, 0, 0, 0, 0) == -9);
                Assert.IsTrue(A.item(0, 0, 0, 0, 0, 0, 0, 0) == -9);
                Assert.IsTrue(A.item(0, 0, 0, 0, 0, 0, 0, 0, 0) == -9);
                Assert.IsTrue(A.item(0, 0, 0, 0, 0, 0, 0, 0, 0, 0) == -9);
                Assert.IsTrue(A.item(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0) == -9);
            }

        }
        [TestMethod]
        public void numpy_itemSimpleTest() {

            Array<double> A = -9;
            Assert.IsTrue(A.item(0) == -9);
            Assert.IsTrue(A.item(0, 0) == -9);
            Assert.IsTrue(A.item(0, 0, 0) == -9);
            Assert.IsTrue(A.item(0, 0, 0, 0) == -9);
            Assert.IsTrue(A.item(0, 0, 0, 0, 0) == -9);
            Assert.IsTrue(A.item(0, 0, 0, 0, 0, 0) == -9);
            Assert.IsTrue(A.item(0, 0, 0, 0, 0, 0, 0) == -9);
            Assert.IsTrue(A.item(0, 0, 0, 0, 0, 0, 0, 0) == -9);
            Assert.IsTrue(A.item(0, 0, 0, 0, 0, 0, 0, 0, 0) == -9);
            Assert.IsTrue(A.item(0, 0, 0, 0, 0, 0, 0, 0, -1) == -9);
            Assert.IsTrue(A.item(-1, 0, 0, 0, 0, 0, 0, -1, -1) == -9);
            Assert.IsTrue(A.item(0, 0, 0, 0, 0, 0, 0, 0, 0, 0) == -9);
            Assert.IsTrue(A.item(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0) == -9);

        }

        [TestMethod]
        public void numpy_itemNpScalar() {
            using (Scope.Enter(ArrayStyles.numpy)) {
                Array<double> A = -1;
                Assert.IsTrue(A.ndim == 0);
                Assert.IsTrue(A.item() == -1); 
                Assert.IsTrue(A.item(0) == -1); 
                Assert.IsTrue(A.item(0,0) == -1); 
                Assert.IsTrue(A.item(-1,0) == -1); 
                Assert.IsTrue(A.item(0, -1) == -1); 
                Assert.IsTrue(A.item(0, -1, -1) == -1); 
                Assert.IsTrue(A.item(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0) == -1);
            }

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void numpy_itemTooManyDimsFail() {
            Array<double> A = -9;
            Assert.IsTrue(A.item(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0) == -9);
        }
        [TestMethod]
        public void numpy_itemSequential() {
            Array<ushort> A = counter<ushort>(0, 1, 4, 3, 2, StorageOrders.RowMajor);
            for (int i = 0; i < 4 * 3 * 2; i++) {
                Assert.IsTrue(A.item(i) == i); 
            }
            A.Storage.EnsureStorageOrder(StorageOrders.ColumnMajor);
            for (int i = 0; i < 4 * 3 * 2; i++) {
                Assert.IsTrue(A.item(i) == i);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void numpy_item_OORLastDimFail() {
            Array<ushort> A = counter<ushort>(1, 1, 1, 2);
            Assert.IsTrue(A.item(0, 1) == 2);
            var b = A.item(0,2);  // fails in numpy (works in ML)
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void numpy_item_OORLastDimNegFail() {
            Array<ushort> A = counter<ushort>(1, 1, 1, 2);
            Assert.IsTrue(A.item(0, 1) == 2);
            var b = A.item(0,-3);  // fails in numpy (works in ML)
        }

        [TestMethod]
        public void numpy_itemset_Simple() {

            Array<ulong> A = counter<ulong>(1, 1, 5, 4, 3);
            A.itemset(1, 1, 999u);
            Assert.IsTrue(A[1, 1] == 999); 

        }

        [TestMethod]
        public void numpy_itemset_sequential() {
            Array<float> A = zeros<float>(5, 4, 3);
            for (int i = 0; i < 5 * 4 *3; i++) {
                A.itemset(i, i); 
            }
            Assert.IsTrue(A.Equals(counter<float>(0f, 1f, 5, 4, 3, StorageOrders.RowMajor)));

            A.a = A.T;
            for (int i = 0; i < 5 * 4 * 3; i++) {
                A.itemset(i, i);
            }
            Assert.IsTrue(A.Equals(counter<float>(0f, 1f, 4, 3, 5, StorageOrders.RowMajor)));

        }
    }
}
