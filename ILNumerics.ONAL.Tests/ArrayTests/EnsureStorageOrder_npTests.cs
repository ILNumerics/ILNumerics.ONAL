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
