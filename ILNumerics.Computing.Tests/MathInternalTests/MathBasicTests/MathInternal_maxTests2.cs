using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;
using static ILNumerics.Globals; 

namespace ILNumerics.Core.UnitTests.MathInternalTests {
    [TestClass]
    public class MathInternal_maxTests2 {

        [TestMethod]
        public void MathInternal_max_reuse_IndicesArray() {
            Array<int> A = counter<int>(1, 1, 4, 3, 5, StorageOrders.RowMajor);

            Array<long> I = zeros<long>(1, 3, 6); // enough to store the indices
            var stor = I.Storage;
            var mem = I.Storage.m_handles;
            Array<int> B = max(A, I, keepdim: true);

            Assert.IsTrue(ReferenceEquals(I.Storage, stor));
            Assert.IsTrue(ReferenceEquals(I.Storage.m_handles, mem));

            max(counter<int>(1, 1, 4, 6, 7), I).Release();
            // had to create new I storage internally. This has "renamed" I. 
            Assert.IsFalse(ReferenceEquals(I.Storage, stor));
            Assert.IsFalse(ReferenceEquals(I.Storage.m_handles, mem));

            Assert.IsTrue(I.Equals(ones<long>(1, 6, 7) * 3));


        }

    }
}
