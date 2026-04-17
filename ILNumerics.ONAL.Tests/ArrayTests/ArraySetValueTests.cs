using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public class ArraySetValueTests {

        [TestMethod]
        public void MutableSetValueSimpleTest() {
            Array<ushort> A = new ushort[,,] {
                { { 1, 2 }, { 3, 4 } },
                { { 5, 6 }, { 7, 8 } },
            };
            A.SetValue((ushort)99, 0);
            Assert.IsTrue(A.GetValue(0) == 99);

            Assert.IsTrue(A.GetValue(0, 1) == 3);
            A.SetValue((ushort)199, 0, 1);
            Assert.IsTrue(A.GetValue(0, 1) == 199);

            Assert.IsTrue(A.GetValue(0, 0, 1) == 2);
            A.SetValue((ushort)299, 0, 0, 1);
            Assert.IsTrue(A.GetValue(0, 0, 1) == 299);

        }
    }
}
