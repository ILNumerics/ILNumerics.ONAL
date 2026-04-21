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
using ILNumerics.Core.Functions.Builtin;
using static ILNumerics.Globals;
using static ILNumerics.ILMath;
using System.Security;

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public class EnsureStorageOrder_np : NumpyTestClass {

        #region EnsureStorageOrder
        [TestMethod]
        public void BaseStorage_EnsureStorageOrderTestCM() {

            Array<double> A = counter(1.0, 1.0, 5, 4);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);

            var oldCA = A.Storage.m_handles[0].Pointer;
            // don't do anything 
            A.Storage.EnsureStorageOrder(StorageOrders.ColumnMajor);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(object.Equals(A.Storage.m_handles[0].Pointer, oldCA));

            A.Storage.EnsureStorageOrder(StorageOrders.RowMajor);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor);
            Assert.IsTrue(!object.Equals(A.Storage.m_handles[0].Pointer, oldCA));

            Assert.IsTrue(A.Equals(counter(1.0, 1.0, 5, 4)));

        }
        [TestMethod]
        [SecuritySafeCritical]
        public void BaseStorage_EnsureStorageOrderTestRM() {

            Array<double> A = counter(1.0, 1.0, 4, 5, order: StorageOrders.RowMajor);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor);

            var oldCA = A.Storage.m_handles[0].Pointer;
            // don't do anything 
            A.Storage.EnsureStorageOrder(StorageOrders.RowMajor);
            Assert.IsTrue(object.Equals(A.Storage.m_handles[0].Pointer, oldCA));

            A.Storage.EnsureStorageOrder(StorageOrders.ColumnMajor);
            Assert.IsTrue(!object.Equals(A.Storage.m_handles[0].Pointer, oldCA));

            Assert.IsTrue(A.Equals(counter(1.0, 1.0, 4, 5, StorageOrders.RowMajor)));

        }
        [TestMethod]
        [SecuritySafeCritical]
        public void BaseStorage_EnsureStorageOrderTestNPscalar() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = counter(1.0, 1.0, 4, 5, order: StorageOrders.RowMajor);
                Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor);
                A = 1; // true numpy scalar

                var oldCA = A.Storage.m_handles[0].Pointer;
                // don't do anything 
                A.Storage.EnsureStorageOrder(StorageOrders.RowMajor);
                Assert.IsTrue(object.Equals(A.Storage.m_handles[0].Pointer, oldCA));

                A.Storage.EnsureStorageOrder(StorageOrders.ColumnMajor);
                Assert.IsTrue(object.Equals(A.Storage.m_handles[0].Pointer, oldCA));

                Assert.IsTrue(A.Equals(1.0));
            }
        }
        #endregion
    }
}
