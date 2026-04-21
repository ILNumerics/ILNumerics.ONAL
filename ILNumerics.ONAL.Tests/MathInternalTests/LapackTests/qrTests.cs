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
using static ILNumerics.ILMath;
using static ILNumerics.Globals;


namespace ILNumerics.Core.UnitTests.MathInternalTests {

    [TestClass]
    public class qrTests {

        [TestMethod]
        public void QREconomySize_TauLengthFix() {

            Array<double> X = randn(100, 20);
            Array<double> R = empty<double>();
            Array<double> Q = qr(X, R, false); // works
            Assert.IsTrue(maxall(abs(multiply(Q, R) - X)) < eps * X.S.NumberOfElements); 

            R = empty<double>();
            Q = qr(X, R, economySize: true); 
            Assert.IsTrue(maxall(abs(multiply(Q, R) - X)) < eps * X.S.NumberOfElements);

            Array<double> RR = 1;
            Array<int> EE = 1;
            Array<double> QQ = qr(X, RR, EE, economySize: true);
            Assert.IsTrue(maxall(abs(multiply(QQ, RR) - X[full, EE])) < eps * X.S.NumberOfElements);

        }
        [TestMethod]
        public void Call_Test_QR_QREecoSize() {
            Test_QR_QREecoSize(counter<double>(1.0,1.0, 5, 4), 1e-3);
            Test_QR_QREecoSize(counter<double>(1.0,1.0, 5, 5), 1e-3);
            Test_QR_QREecoSize(counter<double>(1.0,1.0, 4, 5), 1e-3);
            Test_QR_QREecoSize(empty<double>(), 1e-3);
        }


        private void Test_QR_QREecoSize(Array<double> A, double tol) {

            if (A.Size[0] < A.Size[1]) {
                //Info("QREecoSize: for matrices m < n, qr(A,R,E,false) is used!");
                return;
            }

            Array<double> R = empty<double>();
            Array<int> E = empty<int>();
            Array<double> Q = qr(A, R, E, true);
            Assert.IsTrue(Q.Size[0] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(Q.Size[1] == A.Size[1], "invalid size of: Q matrix");
            Assert.IsTrue(R.Size[0] == A.Size[1], "invalid size of: R matrix");
            Assert.IsTrue(R.Size[1] == A.Size[1], "invalid size of: R matrix");
            Assert.IsTrue(E.Size[0] == A.Size[1], "invalid size of: E matrix");
            Assert.IsTrue(E.Size[1] == 1, "invalid size of: E matrix");
            Assert.IsTrue(norm(multiply(Q, R) - A[full, E]) < tol, "values did not match (double)!");

            Array<float> Af = tosingle(A);
            Array<float> Rf = empty<float>();
            Array<int> Ef = empty<int>();
            Array<float> Qf = qr(Af, Rf, Ef, true);
            Assert.IsTrue(Qf.Size[0] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(Qf.Size[1] == A.Size[1], "invalid size of: Q matrix");
            Assert.IsTrue(Rf.Size[0] == A.Size[1], "invalid size of: R matrix");
            Assert.IsTrue(Rf.Size[1] == A.Size[1], "invalid size of: R matrix");
            Assert.IsTrue(Ef.Size[0] == A.Size[1], "invalid size of: E matrix");
            Assert.IsTrue(Ef.Size[1] == 1, "invalid size of: E matrix");
            Assert.IsTrue(norm(multiply(Qf, Rf) - Af[full, Ef]) < (float)tol, "values did not match (float): tol: " + tol.ToString());

            Array<fcomplex> Afc = tofcomplex(A);
            Array<fcomplex> Rfc = empty<fcomplex>();
            Array<int> Efc = empty<int>();
            Array<fcomplex> Qfc = qr(Afc, Rfc, Efc, true);
            Assert.IsTrue(Qfc.Size[0] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(Qfc.Size[1] == A.Size[1], "invalid size of: Q matrix");
            Assert.IsTrue(Rfc.Size[0] == A.Size[1], "invalid size of: R matrix");
            Assert.IsTrue(Rfc.Size[1] == A.Size[1], "invalid size of: R matrix");
            Assert.IsTrue(Efc.Size[0] == A.Size[1], "invalid size of: E matrix");
            Assert.IsTrue(Efc.Size[1] == 1, "invalid size of: E matrix");
            Assert.IsTrue(norm(multiply(Qfc, Rfc) - Afc[full, Efc]) < (float)tol, "values did not match (fcomplex)!");

            Array<complex> Ac = tocomplex(A);
            Array<complex> Rc = empty<complex>();
            Array<int> Ec = empty<int>();
            Array<complex> Qc = qr(Ac, Rc, Ec, true);
            Assert.IsTrue(Qc.Size[0] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(Qc.Size[1] == A.Size[1], "invalid size of: Q matrix");
            Assert.IsTrue(Rc.Size[0] == A.Size[1], "invalid size of: R matrix");
            Assert.IsTrue(Rc.Size[1] == A.Size[1], "invalid size of: R matrix");
            Assert.IsTrue(Ec.Size[0] == A.Size[1], "invalid size of: E matrix");
            Assert.IsTrue(Ec.Size[1] == 1, "invalid size of: E matrix");
            Assert.IsTrue(norm(real(multiply(Qc, Rc) - Ac[full, Ec])) < tol, "values did not match (complex)!");

        }


        [TestMethod]
        public void Call_Test_QR_QRecoSize() {
            Test_QR_QRecoSize(counter<double>(1.0, 1.0, 5, 4), 1e-3);
            Test_QR_QRecoSize(counter<double>(1.0, 1.0, 5, 5), 1e-3);
            Test_QR_QRecoSize(counter<double>(1.0, 1.0, 4, 5), 1e-3);
            Test_QR_QRecoSize(empty<double>(), 1e-3);
        }

        private void Test_QR_QRecoSize(Array<double> A, double tol) {

            if (A.Size[0] < A.Size[1]) {
                //Info("QRecoSize: for matrices m < n, qr(A,R,false) is used!");
                return;
            }
            Array<double> R = empty<double>();
            Array<double> Q = qr(A, R, economySize: true);

            Assert.IsTrue(Q.Size[0] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(Q.Size[1] == A.Size[1], "invalid size of: Q matrix");
            Assert.IsTrue(R.Size[0] == A.Size[1], "invalid size of: R matrix");
            Assert.IsTrue(R.Size[1] == A.Size[1], "invalid size of: R matrix");
            Assert.IsTrue(norm(multiply(Q, R) - A) < tol, "values did not match (double)!");

            Array<float> Af = tosingle(A);
            Array<float> Rf = empty<float>();
            Array<float> Qf = qr(Af, Rf, economySize: true);
            Assert.IsTrue(Qf.Size[0] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(Qf.Size[1] == A.Size[1], "invalid size of: Q matrix");
            Assert.IsTrue(Rf.Size[0] == A.Size[1], "invalid size of: R matrix");
            Assert.IsTrue(Rf.Size[1] == A.Size[1], "invalid size of: R matrix");
            Assert.IsTrue(norm(multiply(Qf, Rf) - Af) < (float)tol, "values did not match (float)!");


            Array<fcomplex> Afc = tofcomplex(A);
            Array<fcomplex> Rfc = empty<fcomplex>();
            Array<fcomplex> Qfc = qr(Afc, Rfc, economySize: true);
            Assert.IsTrue(Qfc.Size[0] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(Qfc.Size[1] == A.Size[1], "invalid size of: Q matrix");
            Assert.IsTrue(Rfc.Size[0] == A.Size[1], "invalid size of: R matrix");
            Assert.IsTrue(Rfc.Size[1] == A.Size[1], "invalid size of: R matrix");
            Assert.IsTrue(norm(multiply(Qfc, Rfc) - Afc) < (float)tol, "values did not match (fcomplex)!");

            Array<complex> Ac = tocomplex(A);
            Array<complex> Rc = empty<complex>();
            Array<complex> Qc = qr(Ac, Rc, economySize: true);
            Assert.IsTrue(Qc.Size[0] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(Qc.Size[1] == A.Size[1], "invalid size of: Q matrix");
            Assert.IsTrue(Rc.Size[0] == A.Size[1], "invalid size of: R matrix");
            Assert.IsTrue(Rc.Size[1] == A.Size[1], "invalid size of: R matrix");
            Assert.IsTrue(norm(multiply(Qc, Rc) - Ac) < tol, "values did not match (complex)!");

        }

        [TestMethod]
        public void Call_Test_QR_QRE() {
            Test_QR_QRE(counter<double>(1.0, 1.0, 5, 4), 1e-3);
            Test_QR_QRE(counter<double>(1.0, 1.0, 5, 5), 1e-3);
            Test_QR_QRE(counter<double>(1.0, 1.0, 4, 5), 1e-3);
        }


        private void Test_QR_QRE(Array<double> A, double tol) {

            Array<double> R = empty<double>();
            Array<int> E = empty<int>();
            Array<double> Q = qr(A, R, E);
            Assert.IsTrue(Q.Size[0] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(Q.Size[1] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(E.Size[0] == A.Size[1], "invalid size of: E matrix");
            Assert.IsTrue(E.Size[1] == A.Size[1], "invalid size of: E matrix");
            var error = norm(multiply(Q, R) - multiply(A, todouble(E))); 

			Assert.IsTrue(error < tol, "values did not match (double)! error: " + error);

            Array<float> Af = tosingle(A);
            Array<float> Rf = empty<float>();
            Array<int> Ef = empty<int>();
            Array<float> Qf = qr(Af, Rf, Ef);
            Assert.IsTrue(Qf.Size[0] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(Qf.Size[1] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(E.Size[0] == A.Size[1], "invalid size of: E matrix");
            Assert.IsTrue(E.Size[1] == A.Size[1], "invalid size of: E matrix");
            Assert.IsTrue(norm(multiply(Qf, Rf) - multiply(Af, tosingle(Ef))) < (float)tol, "values did not match (float)!");


            Array<fcomplex> Afc = tofcomplex(A);
            Array<fcomplex> Rfc = empty<fcomplex>();
            Array<int> Efc = empty<int>();
            Array<fcomplex> Qfc = qr(Afc, Rfc, Efc);
            Assert.IsTrue(Qfc.Size[0] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(Qfc.Size[1] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(E.Size[0] == A.Size[1], "invalid size of: E matrix");
            Assert.IsTrue(E.Size[1] == A.Size[1], "invalid size of: E matrix");
            Assert.IsTrue(norm(abs(multiply(Qfc, Rfc) - multiply(Afc, tofcomplex(Efc)))) < (float)tol, "values did not match (fcomplex)!");


            Array<complex> Ac = tocomplex(A);
            Array<complex> Rc = empty<complex>();
            Array<int> Ec = empty<int>();
            Array<complex> Qc = qr(Ac, Rc, Ec);
            Assert.IsTrue(Qc.Size[0] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(Qc.Size[1] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(E.Size[0] == A.Size[1], "invalid size of: E matrix");
            Assert.IsTrue(E.Size[1] == A.Size[1], "invalid size of: E matrix");
            Assert.IsTrue(allall(abs(multiply(Qc, Rc) - multiply(Ac, tocomplex(Ec))) < tol), "values did not match (complex)!");
        }

        [TestMethod]
        public void Call_Test_QR_QR() {
            Test_QR_QR(counter<double>(1.0, 1.0, 5, 4), 1e-3);
            Test_QR_QR(counter<double>(1.0, 1.0, 5, 5), 1e-3);
            Test_QR_QR(counter<double>(1.0, 1.0, 4, 5), 1e-3);
            Test_QR_QR(empty<double>(), 1e-3);
            Test_QR_QR(empty<double>(), 1e-3);
        }


        private void Test_QR_QR(Array<double> A, double tol) {

            Array<double> R = empty<double>();
            Array<double> Q = qr(A, R: R, false); 
            Assert.IsTrue(Q.Size[0] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(Q.Size[1] == (A.IsEmpty ? Math.Min(A.S[1], A.S[0]) : A.Size[0]), "invalid size of: Q matrix");
            Assert.IsFalse(norm(multiply(Q, R) - A) > tol, "values did not match (double)!");

            Array<float> Af = tosingle(A);
            Array<float> Rf = empty<float>();
            Array<float> Qf = qr(Af, Rf, false);
            Assert.IsTrue(Qf.Size[0] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(Qf.Size[1] == (A.IsEmpty ? Math.Min(A.S[1], A.S[0]) : A.Size[0]), "invalid size of: Q matrix");
            Assert.IsFalse(norm(multiply(Qf, Rf) - Af) > (float)tol, "values did not match (float)!");

            Array<fcomplex> Afc = tofcomplex(A);
            Array<fcomplex> Rfc = empty<fcomplex>();
            Array<fcomplex> Qfc = qr(Afc, Rfc, false);
            Assert.IsTrue(Qfc.Size[0] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(Qfc.Size[1] == (A.IsEmpty ? Math.Min(A.S[1], A.S[0]) : A.Size[0]), "invalid size of: Q matrix");
            Assert.IsFalse(norm(multiply(Qfc, Rfc) - Afc) > (float)tol, "values did not match (fcomplex)!");

            Array<complex> Ac = tocomplex(A);
            Array<complex> Rc = empty<complex>();
            Array<complex> Qc = qr(Ac, Rc, false);
            Assert.IsTrue(Qc.Size[0] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(Qc.Size[1] == (A.IsEmpty ? Math.Min(A.S[1], A.S[0]) : A.Size[0]), "invalid size of: Q matrix");
            Assert.IsFalse(norm(multiply(Qc, Rc) - Ac) > tol, "values did not match (complex)!");

        }

        //[TestMethod]
        //public void Test_QR_rawLapackForm() {
        //    // create rank deficient matrix: rank(A) is 2
        //    Array<double> A = counter<double>(1.0, 1.0, 5, 4);
        //    Array<double> RawRes = vector<double>(new double[] { -7.416198, 0.237637, 0.356455, 0.475274, 0.594092, -17.529196, -4.767313, -0.062681, -0.353440, -0.644200, -27.642194, -9.534626, 0.000000, 0.200000, 0.600000, -37.755192, -14.301939, -0.000000, -0.000000, -0.491996 }).Reshape(5, 4);
        //    if ((norm(qr(A) - RawRes) > 1e-6).NumberTrues > 0)
        //        throw new Exception("values did not match (double)!");
        //    Array<complex> AC = tocomplex(counter<double>(1.0, 1.0, 5, 4));
        //    Array<complex> RawResC = tocomplex(RawRes);
        //    if ((norm(qr(AC) - RawResC) > 1e-6).NumberTrues > 0)
        //        throw new Exception("values did not match (complex)!");
        //    Array<float> AF = tosingle(A);
        //    Array<float> RawResF = vector<float>(new float[] { -7.416199f, 0.237637f, 0.356455f, 0.475274f, 0.594092f, -17.529196f, -4.767312f, -0.062681f, -0.353440f, -0.644200f, -27.642193f, -9.534623f, 0.000001f, 0.5288425f, 0.8431941f, -37.755192f, -14.301935f, 4.278799E-07f, 6.74691E-07f, 0.6276464f }).Reshape(5, 4);
        //    if ((qr(AF) - RawResF > 1e-5f).NumberTrues > 0)
        //        Assert.Fail("Test_QR_rawLapackForm (float): " + (qr(AF) - RawResF > 1e-5f).NumberTrues + " value(s) did not match expected result. continuing... ");
        //    if ((qr(AF) - RawResF > 1e-5f).NumberTrues > 2)
        //        throw new Exception("values did not match (float)!");
        //    Array<fcomplex> AFc = tofcomplex(AF);

        //    Array<fcomplex> RawResFc = tofcomplex(vector<float>(new float[] { -7.416199f, 0.237637f, 0.356455f, 0.475274f, 0.594092f, -17.529196f, -4.767312f, -0.062681f, -0.353440f, -0.644200f, -27.642193f, -9.534623f, 0.000001f, 0.7308314f, 0.8431941f, -37.755192f, -14.301935f, 1.783777E-06f, 6.74691E-07f, 0.6276464f }).Reshape(5, 4));
        //    if ((abs(qr(AFc) - RawResFc) > 1e-5f).NumberTrues > 0)
        //        Assert.Fail("Test_QR_rawLapackForm(fcomplex): " + (abs(qr(AFc) - RawResFc) > 1e-5f).NumberTrues + " value(s) did not match expected result. continuing... ");
        //    if ((abs(qr(AFc) - RawResFc) > 1e-5f).NumberTrues > 2)
        //        throw new Exception("values did not match (fcomplex)!");
        //}

        [TestMethod]
        public void QR_scalar() {
            Array<complex> A = (complex)11;

            Assert.IsTrue(qr(A).IsScalar);
            Assert.IsTrue(qr(A).S[0] == 1);
            Assert.IsTrue(qr(A).S[1] == 1);

            Array<complex> R = (complex)1;
            Assert.IsTrue(qr(A, R, false).IsScalar);
            Assert.IsTrue(qr(A, R, false).S[0] == 1);
            Assert.IsTrue(qr(A, R, false).S[1] == 1);

            Assert.IsTrue(R.IsScalar);
            Assert.IsTrue(R.S[0] == 1);
            Assert.IsTrue(R.S[1] == 1);

            Array<int> E = 1;
            Assert.IsTrue(qr(A, R, E).IsScalar);
            Assert.IsTrue(qr(A, R, E).S[0] == 1);
            Assert.IsTrue(qr(A, R, E).S[1] == 1);

            Assert.IsTrue(R.IsScalar);
            Assert.IsTrue(R.S[0] == 1);
            Assert.IsTrue(R.S[1] == 1);

            Assert.IsTrue(E.IsScalar);
            Assert.IsTrue(E.S[0] == 1);
            Assert.IsTrue(E.S[1] == 1);

        }
        [TestMethod]
        public void QR_scalarNP() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                Array<complex> A = (complex)11;

                Assert.IsTrue(qr(A).IsScalar);
                Assert.IsTrue(qr(A).S[0] == 1);
                Assert.IsTrue(qr(A).S[1] == 1);

                Array<complex> R = (complex)1;
                Assert.IsTrue(qr(A, R, false).IsScalar);
                Assert.IsTrue(qr(A, R, false).S[0] == 1);
                Assert.IsTrue(qr(A, R, false).S[1] == 1);

                Assert.IsTrue(R.IsScalar);
                Assert.IsTrue(R.S[0] == 1);
                Assert.IsTrue(R.S[1] == 1);

                Array<int> E = 1;
                Assert.IsTrue(qr(A, R, E).IsScalar);
                Assert.IsTrue(qr(A, R, E).S[0] == 1);
                Assert.IsTrue(qr(A, R, E).S[1] == 1);

                Assert.IsTrue(R.IsScalar);
                Assert.IsTrue(R.S[0] == 1);
                Assert.IsTrue(R.S[1] == 1);

                Assert.IsTrue(E.IsScalar);
                Assert.IsTrue(E.S[0] == 1);
                Assert.IsTrue(E.S[1] == 1);

            }
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void QR_AisNullFail() {
            Array<float> A = null;
            Array<float> R = qr(A);
 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void QR_ARisNullFail() {
            Array<float> A = null;
            qr(A, null, false);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void QR_AREisNullFail() {
            Array<float> A = null;
            qr(A, null, null);
        }

    }
}
