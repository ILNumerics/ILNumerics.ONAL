using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static ILNumerics.ILMath;
using static ILNumerics.Globals;

#if OBSOLETE
namespace ILNumerics.UnitTests.Legacy_Tests {
    [TestClass]
    public class LapackTests {

        /*
        [TestMethod]
        public void Test_Temp()
        {
            
            Array<complex> A = zeros<complex>(3, 3);

            A[0, 0] = new complex(0.1, 0);
            A[0, 1] = new complex();
            A[0, 2] = new complex();

            A[1, 0] = new complex(0, 0);
            A[1, 1] = new complex(0.1, 0);
            A[1, 2] = new complex(-0.1, 0);

            A[2, 0] = new complex(0, 0);
            A[2, 1] = new complex(-0.1, 0);
            A[2, 2] = new complex(0.1, 0);

            Console.WriteLine("{0}", A);

            Array<complex> b = new complex[] { new complex(0.1, 0), new complex(-0.1, 0), new complex(0.1, 0) };
            b[0] = new complex(0.1, 0);
            b[1] = new complex(-0.1, 0);
            b[2] = new complex(0.1, 0);

            Console.WriteLine(b);

            Array<complex> x;
            x = linsolve(A, b.T);
            Console.WriteLine("LinSolve Diretto:\n", x.ToString()); //<---------- ERROR!!!

            //Can you help me?
            //Is this the best way to solve this kind of system? If I don't use Linsolve but I try:

            Array<complex> Q;
            Array<complex> R = zeros<complex>(3, 3);
            Q = qr(A, R, true);
            x = linsolve(R, multiply(Q.T, b.T));

        }
        */

    
        [TestMethod]
        public void Test_slashUpperTriag()
        {

            // construct 4 x 4 matrix, upper triagonal
            Array<double> A = zeros(4, 4);
            A["0;:"] = new double[] { 1.0, 2.0, 3.0, 4.0 };
            A["1;:"] = new double[] { 0.0, 2.0, 3.0, 4.0 };
            A["2;:"] = new double[] { 0.0, 0.0, 3.0, 4.0 };
            A["3;:"] = new double[] { 0.0, 0.0, 0.0, 4.0 };

            // now construct a right side and solve the equations: 
            Array<double> B = array<double>(new double[4] { 1.0, 2.0, 3.0, 4.0 }, 1, 4);
            Array<double> x = linsolve(A, B.T);
            Array<double> bTest = multiply(A, x);
            Array<double> err = norm(bTest - B.T);
            Assert.IsFalse(err > MachineParameterDouble.eps, "invalid results!");

        }

        [TestMethod]
        public void Test_slashMatrProp()
        {
            // construct 4 x 4 matrix, symmetric, positiv definite
            Array<double> A = randn(4, 4);
            A = A + A.T;                  // <- construct symmetry
            A = A + diag(abs(diag(A) * 100)); // <- construct pos.def.

            // now construct a right side and solve the equations: 
            Array<double> B = array<double>(new double[] { 1.0, 2.0, 3.0, 4.0 }, 1, 4);
            MatrixProperties prop = new MatrixProperties();
            prop |= MatrixProperties.Hermitian;
            prop |= MatrixProperties.PositivDefinite;
            Array<double> x = linsolve(A, B.T, ref prop);
            //[test: if A was not symm.,pos.def. linsolve returnes null]
            System.Diagnostics.Debug.Assert(!Object.Equals(x, null));
            Array<double> bTest = multiply(A, x);
            Array<double> err = norm(bTest - B.T);

        }



        [TestMethod]
        public void Test_slashQRSpecialShapes()
        {

            Array<double> A = empty<double>();
            Array<double> B = empty<double>();
            Array<double> Res = linsolve(A, B);
            Assert.IsTrue(Res.IsEmpty, "Result should be empty!");

        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "det: empty matrix threw exception - ok")]
        public void Test_detEmptyMatrix() {
            // special shapes
            Array<double> A = empty<double>();
            det(A);
        }


        [TestMethod]
        public void Test_det()
        {

            Array<double> A = counter(1.0, 1.0, 4, 4);
            A[1] = 0.0;  // makes A nonsingular ..
            A[14] = 0.0; // - '' - 
            Assert.IsTrue(det(A).Equals(-360.0), "invalid result");

            // float 

            Array<float> Af = tosingle(A);
            Assert.IsTrue(det(Af).Equals(-360.0f), "invalid result: float");

            // complex 

            Array<complex> Ac = real2complex(A, A);
            Assert.IsTrue(det(Ac).Equals(new complex(1440, 0.0)), "invalid result: complex");

            Ac.SetValue(new complex(3, -4.0), 0);
            Assert.IsFalse(det(Ac).GetValue(0) - new complex(7.2000e+002, -1.6800e+003) >= 10e-5, "invalid result: complex (2)");
                 // fcomplex 

            Array<fcomplex> Afc = real2fcomplex(A, A);
            Assert.IsTrue(det(Afc).Equals(new fcomplex(1440.0f, 0.0f)), "invalid result: fcomplex");
            A = -4.70;
            Assert.IsTrue(det(A).Equals(A), "det: scalar double: invalid result");
            Af = -4.70f;
            Assert.IsTrue(det(A).Equals(A), "det: scalar float: invalid result");
            Ac = new complex(-4.70, 45.0);
            Assert.IsTrue(det(Ac).Equals(Ac), "det: scalar complex: invalid result");
            Afc = new fcomplex(-4.70f, -234.0f);
            Assert.IsTrue(det(Afc).Equals(Afc), "det: scalar fcomplex: invalid result");

        }

        [TestMethod]
        public void Test_slashQRRankDeficient()
        {

            double tol = 1e-5;
            Array<double> A = zeros(5, 4);
            A[":;0"] = 1.0;
            Array<double> B = ones(5, 10);
            MatrixProperties props = 0;
            Array<double> Res = linsolve(A, B, ref props);
            Assert.IsTrue(norm(multiply(A, Res) - B) < tol, "result out of tolerance (double)");
            Assert.IsTrue(!((props & MatrixProperties.RankDeficient) == 0), "linsolve should signal rank deficiency! (double)");
          
            props = MatrixProperties.None;
            Array<float> Af = tosingle(A);
            Array<float> Bf = tosingle(B);
            Array<float> retf = linsolve(Af, Bf, ref props);
            Assert.IsTrue(norm(multiply(Af, retf) - Bf) < (float)tol, "result out of tolerance (float)");
            Assert.IsTrue(!((props & MatrixProperties.RankDeficient) == 0), "linsolve should signal rank deficiency! (float)");

            props = MatrixProperties.None;
            Array<fcomplex> Afc = tofcomplex(A);
            Array<fcomplex> Bfc = tofcomplex(B);
            Array<fcomplex> retFc = linsolve(Afc, Bfc, ref props);
            
            Assert.IsTrue(norm(multiply(Afc, retFc) - Bfc) < (float)tol, "result out of tolerance (fcomplex)");
          
            Assert.IsTrue(!((props & MatrixProperties.RankDeficient) == 0), "linsolve should signal rank deficiency! (fcomplex)");
            props = MatrixProperties.None;
            Array<complex> Ac = tocomplex(A);
            Array<complex> Bc = tocomplex(B);
            Array<complex> retc = linsolve(Ac, Bc, ref props);
            Assert.IsTrue(norm(multiply(Ac, retc) - Bc) < tol, "result out of tolerance (complex)");
            Assert.IsTrue(!((props & MatrixProperties.RankDeficient) == 0), "linsolve should signal rank deficiency! (complex)");
        }


        [TestMethod]
        public void Call_Test_slash_mNEQn()
        {
            Test_slash_mNEQn(counter(1.0, 1.0, 1, 5), counter(1.0, 2.0, 1, 4), 1e-3);
            Test_slash_mNEQn(counter(1.0, 1.0, 5, 1), array<double>(new double[] { 1, 2, 3, 4, 5, 10, 20, 30, 40, 50, 20, 40, 60, 80, 100, 200, 400, 600, 800, 1000 }, 5, 4), 1e-3);
            Test_slash_mNEQn(counter(4, 10), counter(3.0, 2.0, 4, 15), 1e-3);
            Test_slash_mNEQn(counter(10, 4), counter(3.0, 2.0, 10, 5), 1e-3);
        }


        private void Test_slash_mNEQn(Array<double> A, Array<double> B, double tol)
        {
            Array<double> ret = linsolve(A, B);
            Assert.IsTrue(norm(multiply(A, ret) - B) < tol, "result out of tolerance (double)");
            Assert.IsTrue(ret.Size[0] == A.Size[1], "invalid size of result (double)");
            Assert.IsTrue(ret.Size[1] == B.Size[1], "invalid size of result (double)");
           
            Array<float> Af = tosingle(A);
            Array<float> Bf = tosingle(B);
            Array<float> retf = linsolve(Af, Bf);
            Assert.IsTrue(norm(multiply(Af, retf) - Bf) < (float)tol, "result out of tolerance (float)");
            Assert.IsTrue(retf.Size[0] == Af.Size[1], "invalid size of result (float)");
            Assert.IsTrue(retf.Size[1] == Bf.Size[1], "invalid size of result (float)");
            
            Array<fcomplex> Afc = tofcomplex(A);
            Array<fcomplex> Bfc = tofcomplex(B);
            Array<fcomplex> retFc = linsolve(Afc, Bfc);
            Assert.IsTrue(norm(multiply(Afc, retFc) - Bfc) < (float)tol, "result out of tolerance (fcomplex)");
            Assert.IsTrue(retFc.Size[0] == Afc.Size[1], "invalid size of result (fcomplex)");
            Assert.IsTrue(retFc.Size[1] == Bfc.Size[1], "invalid size of result (fcomplex)");
            Array<complex> Ac = tocomplex(A);
            Array<complex> Bc = tocomplex(B);
            Array<complex> retc = linsolve(Ac, Bc);
            Assert.IsTrue(norm(multiply(Ac, retc) - Bc) < tol, "result out of tolerance (complex)");
            Assert.IsTrue(retc.Size[0] == Ac.Size[1], "invalid size of result (complex)");
            Assert.IsTrue(retc.Size[1] == Bc.Size[1], "invalid size of result (complex)");

        }

        [TestMethod]
        public void Call_Test_slash()
        {
            Array<double> A1 = rand(150, 150), A2 = rand(150, 100);
            Test_slash(A1, A2, MatrixProperties.Square, 1e-2);
        }

        private void Test_slash(Array<double> A, Array<double> B, MatrixProperties props, double tol)
        {
            Array<double> Res = linsolve(A, B, ref props);
            Assert.IsTrue(norm(multiply(A, Res) - B) < tol, "Test_slash: invalid result (double)");
            Array<float> AF = tosingle(A);
            Array<float> BF = tosingle(B);
            Array<float> ResF = linsolve(AF, BF, ref props);
            Assert.IsTrue(norm(multiply(AF, ResF) - BF) < (float)(tol * 10), "Test_slash: invalid result (float)");
            Array<fcomplex> AFc = tofcomplex(A);
            Array<fcomplex> BFc = tofcomplex(B);
            Array<fcomplex> ResFc = linsolve(AFc, BFc, ref props);
            Assert.IsTrue(norm(multiply(AFc, ResFc) - BFc) < (float)tol, "Test_slash: invalid result (fcomplex)");
            Array<complex> AC = tocomplex(A);
            Array<complex> BC = tocomplex(B);
            Array<complex> ResC = linsolve(AC, BC, ref props);
            Assert.IsTrue(norm(multiply(AC, ResC) - BC) < tol, "Test_slash: invalid result (complex)");
        }

        [TestMethod]
        public void Call_Test_QR_QREecoSize()
        {
            Test_QR_QREecoSize(counter(5, 4), 1e-3);
            Test_QR_QREecoSize(counter(5, 5), 1e-3);
            Test_QR_QREecoSize(counter(4, 5), 1e-3);
            Test_QR_QREecoSize(empty<double>(), 1e-3);
        }


        private void Test_QR_QREecoSize(Array<double> A, double tol)
        {
            
            if (A.Size[0] < A.Size[1])
            {
                //Info("QREecoSize: for matrices m < n, qr(A,R,E,false) is used!");
                return;
            }
            
            Array<double> R = empty<double>();
            Array<double> E = empty<double>();
            Array<double> Q = qr(A, R, E, true);
            Assert.IsTrue(Q.Size[0] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(Q.Size[1] == A.Size[1], "invalid size of: Q matrix");
            Assert.IsTrue(R.Size[0] == A.Size[1], "invalid size of: R matrix");
            Assert.IsTrue(R.Size[1] == A.Size[1], "invalid size of: R matrix");
            Assert.IsTrue(E.Size[0] == 1, "invalid size of: E matrix");
            Assert.IsTrue(E.Size[1] == A.Size[1], "invalid size of: E matrix");
            Assert.IsFalse(find32(norm(multiply(Q, R) - A[full, E]) > tol).Length > 0, "values did not match (double)!");
            
            Array<float> Af = tosingle(A);
            Array<float> Rf = empty<float>();
            Array<float> Ef = empty<float>();
            Array<float> Qf = qr(Af, Rf, Ef, true);
            Assert.IsTrue(Qf.Size[0] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(Qf.Size[1] == A.Size[1], "invalid size of: Q matrix");
            Assert.IsTrue(Rf.Size[0] == A.Size[1], "invalid size of: R matrix");
            Assert.IsTrue(Rf.Size[1] == A.Size[1], "invalid size of: R matrix");
            Assert.IsTrue(Ef.Size[0] == 1, "invalid size of: E matrix");
            Assert.IsTrue(Ef.Size[1] == A.Size[1], "invalid size of: E matrix");
            Assert.IsFalse(find32(norm(multiply(Qf, Rf) - Af[full, Ef]) > (float)tol).Length > 0, "values did not match (float): tol: "+ tol.ToString());
           
            Array<fcomplex> Afc = tofcomplex(A);
            Array<fcomplex> Rfc = empty<fcomplex>();
            Array<fcomplex> Efc = empty<fcomplex>();
            Array<fcomplex> Qfc = qr(Afc, Rfc, Efc, true);
            Assert.IsTrue(Qfc.Size[0] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(Qfc.Size[1] == A.Size[1], "invalid size of: Q matrix");
            Assert.IsTrue(Rfc.Size[0] == A.Size[1], "invalid size of: R matrix");
            Assert.IsTrue(Rfc.Size[1] == A.Size[1], "invalid size of: R matrix");
            Assert.IsTrue(Efc.Size[0] == 1, "invalid size of: E matrix");
            Assert.IsTrue(Efc.Size[1] == A.Size[1], "invalid size of: E matrix");
            Assert.IsFalse(sumall(norm(real(multiply(Qfc, Rfc) - Afc[full, real(Efc)]))) > (float)tol, "values did not match (fcomplex)!");
            
            Array<complex> Ac = tocomplex(A);
            Array<complex> Rc = empty<complex>();
            Array<complex> Ec = empty<complex>();
            Array<complex> Qc = qr(Ac, Rc, Ec, true);
            Assert.IsTrue(Qc.Size[0] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(Qc.Size[1] == A.Size[1], "invalid size of: Q matrix");
            Assert.IsTrue(Rc.Size[0] == A.Size[1], "invalid size of: R matrix");
            Assert.IsTrue(Rc.Size[1] == A.Size[1], "invalid size of: R matrix");
            Assert.IsTrue(Ec.Size[0] == 1, "invalid size of: E matrix");
            Assert.IsTrue(Ec.Size[1] == A.Size[1], "invalid size of: E matrix");
            Assert.IsFalse(find32(norm(real(multiply(Qc, Rc) - Ac[full, real(Ec)])) > tol).Length > 0, "values did not match (complex)!");

        }


        [TestMethod]
        public void Call_Test_QR_QRecoSize()
        {
            Test_QR_QRecoSize(counter(5, 4), 1e-3);
            Test_QR_QRecoSize(counter(5, 5), 1e-3);
            Test_QR_QRecoSize(counter(4, 5), 1e-3);
            Test_QR_QRecoSize(empty<double>(), 1e-3);
        }

        private void Test_QR_QRecoSize(Array<double> A, double tol)
        {

            if (A.Size[0] < A.Size[1])
            {
                //Info("QRecoSize: for matrices m < n, qr(A,R,false) is used!");
                return;
            }
            Array<double> R = empty<double>();
            Array<double> Q = qr(A, R, true);

            Assert.IsTrue(Q.Size[0] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(Q.Size[1] == A.Size[1], "invalid size of: Q matrix");
            Assert.IsTrue(R.Size[0] == A.Size[1], "invalid size of: R matrix");
            Assert.IsTrue(R.Size[1] == A.Size[1], "invalid size of: R matrix");
            Assert.IsFalse(find32(norm(multiply(Q, R) - A) > tol).Length > 0, "values did not match (double)!");

            Array<float> Af = tosingle(A);
            Array<float> Rf = empty<float>();
            Array<float> Qf = qr(Af, Rf, true);
            Assert.IsTrue(Qf.Size[0] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(Qf.Size[1] == A.Size[1], "invalid size of: Q matrix");
            Assert.IsTrue(Rf.Size[0] == A.Size[1], "invalid size of: R matrix");
            Assert.IsTrue(Rf.Size[1] == A.Size[1], "invalid size of: R matrix");
            Assert.IsFalse(find32(multiply(Qf, Rf) - Af > (float)tol).Length > 0, "values did not match (float)!");
        

            Array<fcomplex> Afc = tofcomplex(A);
            Array<fcomplex> Rfc = empty<fcomplex>();
            Array<fcomplex> Qfc = qr(Afc, Rfc, true);
            Assert.IsTrue(Qfc.Size[0] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(Qfc.Size[1] == A.Size[1], "invalid size of: Q matrix");
            Assert.IsTrue(Rfc.Size[0] == A.Size[1], "invalid size of: R matrix");
            Assert.IsTrue(Rfc.Size[1] == A.Size[1], "invalid size of: R matrix");
            Assert.IsFalse(sumall(real(multiply(Qfc, Rfc) - Afc)) > 0, "values did not match (fcomplex)!");
         
            Array<complex> Ac = tocomplex(A);
            Array<complex> Rc = empty<complex>();
            Array<complex> Qc = qr(Ac, Rc, true);
            Assert.IsTrue(Qc.Size[0] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(Qc.Size[1] == A.Size[1], "invalid size of: Q matrix");
            Assert.IsTrue(Rc.Size[0] == A.Size[1], "invalid size of: R matrix");
            Assert.IsTrue(Rc.Size[1] == A.Size[1], "invalid size of: R matrix");
            Assert.IsFalse(find32(real(multiply(Qc, Rc) - Ac) > tol).Length > 0, "values did not match (complex)!");

        }

        [TestMethod]
        public void Call_Test_QR_QRE()
        {
            Test_QR_QRE(counter(5, 4), 1e-3);
            Test_QR_QRE(counter(5, 5), 1e-3);
            Test_QR_QRE(counter(4, 5), 1e-3);
        }


        private void Test_QR_QRE(Array<double> A, double tol)
        {

            Array<double> R = empty<double>();
            Array<double> E = empty<double>();
            Array<double> Q = qr(A, R, E);
            Assert.IsTrue(Q.Size[0] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(Q.Size[1] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(E.Size[0] == A.Size[1], "invalid size of: E matrix");
            Assert.IsTrue(E.Size[1] == A.Size[1], "invalid size of: E matrix");
            Assert.IsFalse(find32(norm(multiply(Q, R) - multiply(A, E)) > tol).Length > 0, "values did not match (double)!");
            
            Array<float> Af = tosingle(A);
            Array<float> Rf = empty<float>();
            Array<float> Ef = empty<float>();
            Array<float> Qf = qr(Af, Rf, Ef);
            Assert.IsTrue(Qf.Size[0] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(Qf.Size[1] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(E.Size[0] == A.Size[1], "invalid size of: E matrix");
            Assert.IsTrue(E.Size[1] == A.Size[1], "invalid size of: E matrix");
            Assert.IsFalse(find32(multiply(Qf, Rf) - multiply(Af, Ef) > (float)tol).Length > 0, "values did not match (float)!");
            

            Array<fcomplex> Afc = tofcomplex(A);
            Array<fcomplex> Rfc = empty<fcomplex>();
            Array<fcomplex> Efc = empty<fcomplex>();
            Array<fcomplex> Qfc = qr(Afc, Rfc, Efc);
            Assert.IsTrue(Qfc.Size[0] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(Qfc.Size[1] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(E.Size[0] == A.Size[1], "invalid size of: E matrix");
            Assert.IsTrue(E.Size[1] == A.Size[1], "invalid size of: E matrix");
            Assert.IsFalse(find32(real(multiply(Qfc, Rfc) - multiply(Afc, Efc)) > (float)tol).Length > 0, "values did not match (fcomplex)!");
          

            Array<complex> Ac = tocomplex(A);
            Array<complex> Rc = empty<complex>();
            Array<complex> Ec = empty<complex>();
            Array<complex> Qc = qr(Ac, Rc, Ec);
            Assert.IsTrue(Qc.Size[0] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(Qc.Size[1] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(E.Size[0] == A.Size[1], "invalid size of: E matrix");
            Assert.IsTrue(E.Size[1] == A.Size[1], "invalid size of: E matrix");
            Assert.IsFalse(find32(real(multiply(Qc, Rc) - multiply(Ac, Ec)) > tol).Length > 0, "values did not match (complex)!");
        }

        [TestMethod]
        public void Call_Test_QR_QR()
        {
            Test_QR_QR(counter(5, 4), 1e-3);
            Test_QR_QR(counter(5, 5), 1e-3);
            Test_QR_QR(counter(4, 5), 1e-3);
            Test_QR_QR(empty<double>(), 1e-3);
            Test_QR_QR(empty<double>(), 1e-3);
        }


        private void Test_QR_QR(Array<double> A, double tol)
        {

            Array<double> R = empty<double>();
            Array<double> Q = qr(A, R);
            Assert.IsTrue(Q.Size[0] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(Q.Size[1] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsFalse(norm(multiply(Q, R) - A) > tol, "values did not match (double)!");
           
            Array<float> Af = tosingle(A);
            Array<float> Rf = empty<float>();
            Array<float> Qf = qr(Af, Rf);
            Assert.IsTrue(Qf.Size[0] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(Qf.Size[1] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsFalse(norm(multiply(Qf, Rf) - Af) > (float)tol, "values did not match (float)!");

            Array<fcomplex> Afc = tofcomplex(A);
            Array<fcomplex> Rfc = empty<fcomplex>();
            Array<fcomplex> Qfc = qr(Afc, Rfc);
            Assert.IsTrue(Qfc.Size[0] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(Qfc.Size[1] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsFalse(norm(multiply(Qfc, Rfc) - Afc) > (float)tol, "values did not match (fcomplex)!");
          
            Array<complex> Ac = tocomplex(A);
            Array<complex> Rc = empty<complex>();
            Array<complex> Qc = qr(Ac, Rc);
            Assert.IsTrue(Qc.Size[0] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsTrue(Qc.Size[1] == A.Size[0], "invalid size of: Q matrix");
            Assert.IsFalse(norm(multiply(Qc, Rc) - Ac) > tol, "values did not match (complex)!");

        }

        //private void Test_QR_rawLapackForm() {
        //    int errorCode = 0; 
        //    try {
        //        // create rank deficient matrix: rank(A) is 2
        //        Array<double> A = counter(5,4); 
        //        Array<double> RawRes = array<double>(new double[]{-7.416198,  0.237637,  0.356455,  0.475274,  0.594092,-17.529196, -4.767313, -0.062681, -0.353440, -0.644200,-27.642194, -9.534626,  0.000000,  0.200000,  0.600000,-37.755192,-14.301939, -0.000000, -0.000000, -0.491996},5,4);
        //        if (find32(norm(qr(A) - RawRes) > 1e-6).Length > 0) 
        //            throw new Exception("values did not match (double)!"); 
        //        Array<complex> AC = tocomplex(counter(5,4)); 
        //        Array<complex> RawResC = tocomplex(RawRes); 
        //        if (find32(norm(qr(AC) - RawResC) > 1e-6).Length > 0) 
        //            throw new Exception("values did not match (complex)!"); 
        //        Array<float> AF = tosingle(A);
        //        Array<float> RawResF = array<float> (new float[]{-7.416199f,  0.237637f,  0.356455f,  0.475274f,  0.594092f,-17.529196f, -4.767312f, -0.062681f, -0.353440f, -0.644200f,-27.642193f, -9.534623f,  0.000001f,  0.5288425f,  0.8431941f, -37.755192f,-14.301935f,  4.278799E-07f, 6.74691E-07f, 0.6276464f},5,4);
        //        if (find32(qr(AF) - RawResF > 1e-5f).Length > 0) 
        //            Info("Test_QR_rawLapackForm (float): " + find32(qr(AF) - RawResF > 1e-5f).Length + " value(s) did not match expected result. continuing... "); 
        //        if (find32(qr(AF) - RawResF > 1e-5f).Length > 2) 
        //            throw new Exception("values did not match (float)!"); 
        //        Array<fcomplex> AFc = tofcomplex(AF);  

        //        Array<fcomplex> RawResFc = tofcomplex(array<float> (new float[]{-7.416199f,  0.237637f,  0.356455f,  0.475274f,  0.594092f,-17.529196f, -4.767312f, -0.062681f, -0.353440f, -0.644200f,-27.642193f, -9.534623f,  0.000001f,  0.7308314f,  0.8431941f, -37.755192f,-14.301935f,  1.783777E-06f, 6.74691E-07f, 0.6276464f},5,4));
        //        if (find32(abs(qr(AFc) - RawResFc) > 1e-5f).Length > 0) 
        //            Info("Test_QR_rawLapackForm(fcomplex): " + find32(abs(qr(AFc) - RawResFc) > 1e-5f).Length + " value(s) did not match expected result. continuing... "); 
        //        if (find32(abs(qr(AFc) - RawResFc) > 1e-5f).Length > 2) 
        //            throw new Exception("values did not match (fcomplex)!"); 
        //        Success(); 
        //    } catch(Exception e) {
        //        Error(errorCode,e.Message); 
        //    }
        //}

        [TestMethod]
        public void Test_LU_retLUP()
        {

            // double
            Array<double> U = empty<double>();
            Array<double> A = array<double>(new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 }, 4, 6);
            Array<double> P = empty<double>();
            Array<double> L = lu(A, U, P);
            Assert.IsFalse(find32(multiply(L, U) - multiply(P, A) > 1e-6).Length > 0, "lu: invalid result (double,L)");
            
            // complex 
            Array<complex> UC = empty<complex>();
            Array<complex> PC = empty<complex>();
            Array<complex> AC = tocomplex(A);
            Array<complex> LC = lu(AC, UC, PC);
            Assert.IsFalse(find32(abs(multiply(LC, UC) - multiply(PC, AC)) > 1e-6).Length > 0, "lu: invalid result (complex,L)");

            // float 
            Array<float> UF = empty<float>();
            Array<float> PF = empty<float>();
            Array<float> AF = tosingle(A);
            Array<float> LF = lu(AF, UF, PF);
            Assert.IsFalse(find32(abs(multiply(LF, UF) - multiply(PF, AF)) > 1e-6f).Length > 0, "lu: invalid result (float,L)");

            // fcomplex 
            Array<fcomplex> UFc = empty<fcomplex>();
            Array<fcomplex> PFc = empty<fcomplex>();
            Array<fcomplex> AFc = tofcomplex(A);
            Array<fcomplex> LFc = lu(AFc, UFc, PFc);
            Assert.IsFalse(find32(abs(multiply(LFc, UFc) - multiply(PFc, AFc)) > 1e-6f).Length > 0, "lu: invalid result (fcomplex,L)");

        }

        [TestMethod]
        public void Test_LU_retLU()
        {

            // double
            Array<double> U = empty<double>();
            Array<double> A = array<double>(new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 }, 4, 6);
            Array<double> L = lu(A, U);
            Assert.IsFalse(find32(multiply(L, U) - A > 1e-6).Length > 0, "lu: invalid result (double,L)");
           
            // complex 
            Array<complex> UC = empty<complex>();
            Array<complex> AC = tocomplex(A);
            Array<complex> LC = lu(AC, UC);
            Assert.IsFalse(find32(abs(multiply(LC, UC) - AC) > 1e-6).Length > 0, "lu: invalid result (complex,L)");
            
            // float 
            Array<float> UF = empty<float>();
            Array<float> AF = tosingle(A);
            Array<float> LF = lu(AF, UF);
            Assert.IsFalse(find32(multiply(LF, UF) - AF > 1e-4f).Length > 0, "lu: invalid result (float,L)");
          
            // fcomplex 
            Array<fcomplex> UFc = empty<fcomplex>();
            Array<fcomplex> AFc = tofcomplex(A);
            Array<fcomplex> LFc = lu(AFc, UFc);
            Assert.IsFalse(find32(abs(multiply(LFc, UFc) - AFc) > 1e-4f).Length > 0, "lu: invalid result (fcomplex,L)");

        }

        [TestMethod]
        public void Test_LU_rawLapackForm()
        {
            // double
            Array<double> A = array<double>(new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 }, 4, 6);
            Array<double> ResRaw = array<double>(new double[] { 4.000000, 0.250000, 0.750000, 0.500000, 8.000000, 3.000000, 0.333333, 0.666667, 12.000000, 6.000000, 0.000000, 0.000000, 16.000000, 9.000000, 0.000000, 0.000000, 20.000000, 12.000000, 0.000000, 0.000000, 24.000000, 15.000000, 0.000000, 0.000000 }, 4, 6);
            Array<double> L = lu(A);
            //if (find32(L - ResRaw > 1e-6).Length > 0)
                //Info("Test_LU_rawLapackForm: unexpected result (double,L) - this might be ok!? (check return values)");
            // complex 
            Array<complex> AC = tocomplex(A);
            Array<complex> ResRawC = tocomplex(ResRaw);
            Array<complex> LC = lu(AC);
            //if (find32(abs(LC - ResRawC) > 1e-6).Length > 0)
                //Info("Test_LU_rawLapackForm: unexpected result (complex,L) - this might be ok!? (check return values)");
            // float 
            ResRaw = array<double>(new double[] { 4.000000, 0.250000, 0.500000, 0.750000, 8.000000, 3.000000, 0.666667, 0.333333, 12.000000, 6.000000, -0.000000, 0.500000, 16.000000, 9.000000, -0.000000, 0.000000, 20.000000, 12.000000, -0.000000, 0.000000, 24.000000, 15.000000, -0.000000, 0.000000 }, 4, 6);
            Array<float> AF = tosingle(A);
            Array<float> ResRawF = tosingle(ResRaw);
            Array<float> LF = lu(AF);
            //if (find32(abs(LF - ResRawF) > 1e-6f).Length > 0)
                //Info("Test_LU_rawLapackForm: unexpected result (float,L) - this might be ok!? (check return values)");
            // fcomplex 
            Array<fcomplex> AFc = tofcomplex(A);
            Array<fcomplex> ResRawFc = tofcomplex(ResRawF);
            Array<fcomplex> LFc = lu(AFc);
            //if (find32(abs(LFc - ResRawFc) > 1e-6f).Length > 0)
                    //Info("Test_LU_rawLapackForm: unexpected result (fcomplex,L) - this might be ok!? (check return values)");

        }


        [TestMethod]
        public void Test_SVD()
        {
            Array<double> A = ones(10, 10);
            Array<double> ResS = zeros(10, 1);
            ResS[0, 0] = 10.0;
            Array<double> U = empty<double>();
            Array<double> V = empty<double>();

            Array<double> B = svd(A);
            Assert.IsTrue(norm(B - ResS) < 1e-10, "Invalid values detected!");
            

            A = ones(3, 3);
            Array<double> ResU = array<double>(
                new double[9] { -0.577350, -0.577350, -0.577350, -0.577350, -0.211325, 0.788675, -0.577350, 0.788675, -0.211325 }, 3, 3);
            Array<double> ResV = array<double>(
                new double[9] { -0.5773503, -0.5773503, -0.5773503, 0, -0.7071068, 0.7071068, 0.8164966, -0.4082483, -0.4082483 }, 3, 3);
            ResS = zeros(3, 3);
            ResS[0, 0] = 3.0;
            B = svd(A, U, V);
            Assert.IsTrue(norm(B - ResS) < 1e-10, "Invalid values detected!");
            Assert.IsTrue(norm(multiply(U, B, V.T) - A) < eps * A.S.Longest * 2, "Invalid values detected!");

            ///////////////////////////////   float ///////////////////////////////////////
            
            Array<float> fA = array<float>(1f, 10, 10);
            Array<float> fResS = zeros<float>(10, 1);
            fResS[0, 0] = 10.0f;
            Array<float> fU = empty<float>(Size.Empty00);
            Array<float> fV = empty<float>(Size.Empty00);

            Array<float> fB = svd(fA);
            Assert.IsTrue(norm(fB - fResS) < 1e-5f, "Invalid values detected!");

            fA = tosingle(reshape(vec(1, 9), 3, 3));
            //Array<float> fResU = create<float>(
            //    new float[9] { -0.577350f, -0.577350f, -0.577350f,0.8164966f, -0.4082483f, -0.4082483f, 3.441276e-08f, -0.7071068f, 0.7071068f
            //                 }, 3, 3);
            //Array<float> fResV = create<float>(
            //    new float[9] { -0.5773503f, -0.5773503f, -0.5773503f, -0.8164966f,0.4082483f, 0.4082483f, 0.0f, 0.7071068f,-0.7071068f
            //                  }, 3, 3);
            //if (Lapack is LapackGeneric)
            //{
            //    fResV = fResV.T; 
            //}
            //fResS = single( zeros(3, 3));
            //fResS[0, 0] = 3.0f;
            fB = svd(fA, fU, fV);
            Assert.IsTrue(norm(multiply(multiply(fU, fB), fV.T) - fA) < 1e-5f, "invalid values detected:svd(fA, ref fU, ref fV);");

            ///////////////////////////////   complex ///////////////////////////////////////

            Array<complex> cA = real2complex(ones(10, 10));
            ResS = zeros(10, 1);
            ResS[0, 0] = 10.0;
            Array<complex> cU = real2complex(empty<double>());
            Array<complex> cV = real2complex(empty<double>());

            B = svd(cA);
            Assert.IsTrue(norm(B - ResS) < 1e-6, "Invalid values detected!");

            cA = real2complex(ones(3, 3));
            //Array<complex> cResU = create<complex>(
            //    new complex[9] { -0.577350, -0.577350, -0.577350, -0.577350, -0.211325, 0.788675, -0.577350, 0.788675, -0.211325
            //                 }, 3, 3);
            //Array<complex> cResV = create<complex>(
            //    new complex[9] { -0.5773503, -0.5773503, -0.5773503, 0, -0.7071068, 0.7071068, 0.8164966, -0.4082483, -0.4082483
            //                  }, 3, 3);
            //ResS = zeros(3, 3);
            //ResS[0, 0] = 3.0;
            B = svd(cA, cU, cV);
            Assert.IsTrue(norm(multiply(multiply(cU, real2complex(B)), cV.T) - cA) < 1e-6, "Invalid values detected!");

            //if (norm(abs(cU - cResU)) > 1e-6)
            //    throw new Exception("Invalid values detected!");
            //if (norm(abs(cV - cResV)) > 1e-6)
            //    throw new Exception("Invalid values detected!");

        }

        [TestMethod]
        public void Test_PINV()
        {

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
            ResS = array<double>(results, 5, 4);
            double[] data = new double[20] { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0, 10.0, 11.0, 12.0, 13.0, 14.0, 15.0, 16.0, 17.0, 18.0, 19.0, 20.0 };
            A = array<double>(data, 4, 5);
            B = pinv(A);
            Assert.IsTrue(norm(B - ResS) < 1e-10, "Invalid values detected!");

            // test scalar 

            ResS = 0.0294117647;
            Assert.IsTrue(norm(pinv(34.0) - ResS) < 1e-8, "Invalid values detected!");

            // test complex 
            Array<fcomplex> fA = tofcomplex(ccomplex(ones(10, 20), ones(10, 20) * 0.5f));
            Array<fcomplex> fRes = pinv(fA);
            Assert.IsFalse(sumall(abs(multiply(fA, multiply(fRes, fA)) - fA) > 2e-5f) > 0, "Invalid result: fcomplex a*res*a-a");
            Assert.IsFalse(sumall(abs(multiply(fRes, multiply(fA, fRes)) - fRes) > 2e-5f) > 0, "Invalid result: fcomplex a*res*a-a");

        }






    }
}
#endif
