using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public class ArrayGetElementTypeTests {
        [TestMethod]
        public void ArrayGetElementTypeTest_empty() {

            Array<byte> A = new byte[0];

            Assert.IsTrue(A.GetElementType() == typeof(byte)); 
        }
    }
}
