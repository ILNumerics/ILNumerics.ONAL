using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;
using static ILNumerics.Globals; 

namespace ILNumerics.Core.UnitTests.MathInternalTests {
    [TestClass]
    public class MathInternal_maxBinaryTests {

        [TestMethod]
        public void MathInternal_max_bin_simple() {
            Array<float> A = counter<float>(0.1f, 0.1f, 1, 138);
            Array<float> B = linspace<float>(0.0f, 20f, 138);

            Array<float> Res = max(A, B);
            Assert.IsTrue(Res.S[0] == 1);
            Assert.IsTrue(Res.S[1] == 138);
            Assert.IsTrue(Res.S.NumberOfDimensions == 2);

            Assert.IsTrue(allall(Res[r(0, 2)] == A[r(0, 2)]));
            Assert.IsTrue(allall(Res[r(3, -1)] == B[r(3, end)]));

        }
        [TestMethod]
        public void MathInternal_max_bin_NanLxBug() {
            Array<float> A = zeros<float>(1, 4);
            Array<float> B = ones<float>(4, 4);

            Array<float> Res1 = max(float.NaN * A, B);
            Assert.IsTrue(Res1.S[0] == 4);
            Assert.IsTrue(Res1.S[1] == 4);
            Assert.IsTrue(Res1.S.NumberOfDimensions == 2);

            Array<float> Res2 = max(B, float.NaN * A);
            Assert.IsTrue(Res2.S[0] == 4);
            Assert.IsTrue(Res2.S[1] == 4);
            Assert.IsTrue(Res2.S.NumberOfDimensions == 2);

            //Assert.IsTrue(allall(Res[r(0, 2)] == A[r(0, 2)]));
            //Assert.IsTrue(allall(Res[r(3, -1)] == B[r(3, end)]));

        }

    }
}
