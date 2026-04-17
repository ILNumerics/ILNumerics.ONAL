using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILNumerics.Core.Functions.Builtin;
using static ILNumerics.Globals;
using static ILNumerics.ILMath;
using System.Security;

namespace ILNumerics.Core.UnitTests {

    [TestClass]
    public class DetachTests {

        [TestMethod]
        public void SetRange_np_BA_2D() {

            Array<double> A = counter(-4.0, 1.0, 5, 6, 3)[full, r(1, -1)]; // non-0 base offset! same as counter(1, 1, 5,5,3)

            Assert.IsTrue(A.GetValue(0) == 1);
            Assert.IsTrue(A.GetValue(1) == 2);
            Assert.IsTrue(A.S.BaseOffset == 5);

            Array<double> dummy = A.C;

            A.Detach();

            Assert.IsTrue(A.GetValue(0) == 1);
            Assert.IsTrue(A.GetValue(1) == 2);
            Assert.IsTrue(A.S.BaseOffset == 0);

            Assert.IsTrue(dummy.GetValue(0) == 1);
            Assert.IsTrue(dummy.GetValue(1) == 2);
            Assert.IsTrue(dummy.S.BaseOffset == 5);

        }
    }
}
