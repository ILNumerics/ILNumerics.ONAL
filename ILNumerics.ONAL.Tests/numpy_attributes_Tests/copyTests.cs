using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;

namespace ILNumerics.Core.UnitTests.numpy_attributes_Tests {

    [TestClass]
    public class copyTests {
        [TestMethod]
        public void numpy_copy_simple() {

            Array<double> A = counter<double>(1, 1, 5, 4, 3);

            Assert.IsTrue(A.copy().Equals(counter<double>(1, 1, 5, 4, 3)));
            Assert.IsTrue(A.copy(StorageOrders.RowMajor).Equals(counter<double>(1, 1, 5, 4, 3)), $"{A.copy(StorageOrders.RowMajor)} - {counter<double>(1, 1, 5, 4, 3)} - Equals: {A.copy(StorageOrders.RowMajor).Equals(counter<double>(1, 1, 5, 4, 3))}");
            Assert.IsTrue(A.copy(StorageOrders.RowMajor).S.StorageOrder == StorageOrders.RowMajor);
            var AC = A.copy();
            Assert.IsFalse(ReferenceEquals(AC.Storage, A.Storage));

            var AR = counter<double>(1, 1, 5, 4);
            var ARC = AR.copy(); 
            Assert.IsTrue(ReferenceEquals(AR.Storage.m_handles, ARC.Storage.m_handles));

        }

    }
}
