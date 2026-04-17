using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;
using static ILNumerics.Globals;


namespace ILNumerics.Core.UnitTests.MathInternalTests {

    [TestClass]
    public class pinvTests {

        [TestMethod]
        public void pinv_simple() {

            Array<double> A = ones(10, 5);
            Array<double> ResS = ones(5, 10) * 0.02;
            Array<double> B = pinv(A);
            Assert.IsTrue(norm(B - ResS) < 1e-10, "Invalid values detected!");

            // test case m < n 

            B = pinv(ones(10, 5));
            ResS = ResS[0u];
            Assert.IsTrue(norm(B - ResS) < 1e-10, "Invalid values detected!");

            // test len(diag(s)) > 1 

            double[] results = new double[20] { -0.2300000000, -0.1450000000, -0.0600000000, 0.0250000000, 0.1100000000, -0.0850000000, -0.0525000000, -0.0200000000, 0.0125000000, 0.0450000000, 0.0600000000, 0.0400000000, 0.0200000000, -0.0000000000, -0.0200000000, 0.2050000000, 0.1325000000, 0.0600000000, -0.0125000000, -0.0850000000 };
            ResS = vector<double>(results).Reshape(5, 4);
            double[] data = new double[20] { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0, 10.0, 11.0, 12.0, 13.0, 14.0, 15.0, 16.0, 17.0, 18.0, 19.0, 20.0 };
            A = vector<double>(data).Reshape(4, 5);
            B = pinv(A);
            Assert.IsTrue(norm(B - ResS) < 1e-10, "Invalid values detected!");

            // test scalar 

            ResS = 0.0294117647;
            Assert.IsTrue(norm(pinv(34.0) - ResS) < 1e-8, "Invalid values detected!");

            // test complex 
            Array<fcomplex> fA = tofcomplex(ccomplex(ones(10, 20), ones(10, 20) * 0.5f));
            Array<fcomplex> fRes = pinv(fA);
            Assert.IsTrue((abs(multiply(fA, multiply(fRes, fA)) - fA) > 2e-5f).NumberTrues == 0, "Invalid result: fcomplex a*res*a-a");
            Assert.IsTrue((abs(multiply(fRes, multiply(fA, fRes)) - fRes) > 2e-5f).NumberTrues == 0, "Invalid result: fcomplex a*res*a-a");

        }

        [TestMethod]
        public void pinv_empty() {
            Array<float> A = empty<float>(1, dim1: 0);
            Assert.IsTrue(pinv(A).IsEmpty);
            Assert.IsTrue(pinv(A).S.IsSameShape(A.T.S));
        }
        [TestMethod]
        public void pinv_scalarNP() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                Array<float> A = -10f;
                Assert.IsTrue(A.S.NumberOfDimensions == 0); 
                Assert.IsTrue(pinv(A).IsScalar);
                Assert.IsTrue(pinv(A).S.IsSameSize(A.T.S), $"pinv(A).S={pinv(A).S}. A.T.S={A.T.S}.");
                Assert.IsTrue(Math.Abs(pinv(A).GetValue(0) - (1f / A.GetValue(0))) < epsf); 
            }
        }

    }
}
