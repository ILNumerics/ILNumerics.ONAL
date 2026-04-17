using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;
using static ILNumerics.Globals;


namespace ILNumerics.Core.UnitTests.MathInternalTests {
    [TestClass]
    public class NormTests {

        [TestMethod]
        public void Test_normFrobenius() {

            Array<double> A = counter<double>(0.0, 1.0, 10, 10);
            Array<double> Res = norm(A, 0);
            Assert.IsTrue(Res - 573.018324314328 < eps);
            // test vectors being consistent with matrices
            Assert.IsTrue(norm(0.0, 0) == 0);
            Assert.IsTrue(norm(1.0, 0) == 1);
            Assert.IsTrue(norm(4.0, 0) == 4);

            Assert.IsTrue(norm(empty<double>(2, dim1: 0)).IsScalar);
            Assert.IsTrue(norm(empty<double>(2, dim1: 0))[0] == 0);

            // test single precision
            Assert.IsTrue(norm(tosingle(A), 0) - 573.018324314328f < epsf);

            Array<complex> C = ccomplex(A, -1);
            Assert.IsTrue(abs(norm(C, 0) - 573.10557491617544) < eps);
            // triggers the vector 'frobenius norm' instead 
            Assert.IsTrue(abs(norm(C["0:9"], 0) - 17.175564037317667) < eps);

            Array<fcomplex> Cf = ccomplex(tosingle(A), -1);
            Assert.IsTrue(abs(norm(Cf, 0) - 573.10557491617544f) < epsf);
            // triggers the vector 'frobenius norm' instead 
            Assert.IsTrue(abs(norm(Cf["0:9"], 0) - 17.175564037317667f) < epsf);

        }


    }
}
