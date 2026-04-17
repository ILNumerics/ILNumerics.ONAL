using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;
using static ILNumerics.Globals;
using System.Diagnostics;


namespace ILNumerics.Core.UnitTests.MathInternalTests {

    [TestClass]
    public class linsolveTests {

        [TestMethod]
        public void linsolve_UpperTriag() {

            // construct 4 x 4 matrix, upper triagonal
            Array<double> A = zeros(4, 4);
            A["0;:"] = new double[] { 1.0, 2.0, 3.0, 4.0 };
            A["1;:"] = new double[] { 0.0, 2.0, 3.0, 4.0 };
            A["2;:"] = new double[] { 0.0, 0.0, 3.0, 4.0 };
            A["3;:"] = new double[] { 0.0, 0.0, 0.0, 4.0 };

            // now construct a right side and solve the equations: 
            Array<double> B = vector<double>(new double[4] { 1.0, 2.0, 3.0, 4.0 }).Reshape(1, 4);
            Array<double> x = linsolve(A, B.T);
            Array<double> bTest = multiply(A, x);
            Array<double> err = norm(bTest - B.T);
            Assert.IsFalse(err > eps, "invalid results! err=" + err);

        }

        [TestMethod]
        public void linsolve_MatrProp() {
            // construct 4 x 4 matrix, symmetric, positiv definite
            Array<double> A = randn(4, 4);
            A = A + A.T;                  // <- construct symmetry
            A = A + diag(abs(diag(A) * 100)); // <- construct pos.def.

            // now construct a right side and solve the equations: 
            Array<double> B = vector<double>(new double[] { 1.0, 2.0, 3.0, 4.0 }).Reshape(1, 4);
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
        [ExpectedException(typeof(ArgumentException))]
        public void linsolve_QRSpecialShapes() {

            Array<double> A = empty<double>();
            Array<double> B = empty<double>();
            Array<double> Res = linsolve(A, B);
            //Assert.IsTrue(Res.IsEmpty, "Result should be empty!");
        }

        [TestMethod]
        public void linsolve_QRRankDeficient() {

            double tol = 1e-5;
            Array<double> A = zeros(5, 4);
            A[":;0"] = 1.0;
            Array<double> B = ones(5, 10);
            MatrixProperties props = 0;
            Array<double> Res = linsolve(A, B, ref props);
            var ResTol = norm(multiply(A, Res) - B); 
            Assert.IsTrue(ResTol < tol, $"result out of tolerance (double). {Res}, tol: {ResTol}. allowed tol.: {tol}.");
            Assert.IsTrue(!((props & MatrixProperties.RankDeficient) == 0), "linsolve should signal rank deficiency! (double)");

            props = MatrixProperties.None;
            Array<float> Af = tosingle(A);
            Array<float> Bf = tosingle(B);
            Array<float> retf = linsolve(Af, Bf, ref props);
            var tolerance = norm(multiply(Af, retf) - Bf); 
            Assert.IsTrue(tolerance < (float)tol, "result out of tolerance (float): " + tolerance);
            Assert.IsTrue(!((props & MatrixProperties.RankDeficient) == 0), "linsolve should signal rank deficiency! (float)");

            props = MatrixProperties.None;
            Array<fcomplex> Afc = tofcomplex(A);
            Array<fcomplex> Bfc = tofcomplex(B);
            Array<fcomplex> retFc = linsolve(Afc, Bfc, ref props);
            tolerance = norm(multiply(Afc, retFc) - Bfc); 
            Assert.IsTrue(tolerance < (float)tol, "result out of tolerance (fcomplex)");

            Assert.IsTrue(!((props & MatrixProperties.RankDeficient) == 0), "linsolve should signal rank deficiency! (fcomplex)");
            props = MatrixProperties.None;
            Array<complex> Ac = tocomplex(A);
            Array<complex> Bc = tocomplex(B);
            Array<complex> retc = linsolve(Ac, Bc, ref props);
            Assert.IsTrue(norm(multiply(Ac, retc) - Bc) < tol, "result out of tolerance (complex)");
            Assert.IsTrue(!((props & MatrixProperties.RankDeficient) == 0), "linsolve should signal rank deficiency! (complex)");
        }


        [TestMethod]
        public void Call_linsolve_mNEQn() {
            linsolve_mNEQn(counter(1.0, 1.0, 1, 5), counter(1.0, 2.0, 1, 4), 1e-3);
            linsolve_mNEQn(counter(1.0, 1.0, 5, 1), vector<double>(new double[] { 1, 2, 3, 4, 5, 10, 20, 30, 40, 50, 20, 40, 60, 80, 100, 200, 400, 600, 800, 1000 }).Reshape(5, 4), 1e-3);
            linsolve_mNEQn(counter<double>(1.0, 1.0, 4, 10), counter(3.0, 2.0, 4, 15), 1e-3);
            linsolve_mNEQn(counter<double>(1.0, 1.0, 10, 4), counter(3.0, 2.0, 10, 5), 1e-3);
        }


        private void linsolve_mNEQn(Array<double> A, Array<double> B, double tol) {
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
        public void Call_linsolve() {
            Array<double> A1 = rand(150, 150), A2 = rand(150, 100);
            // THIS TEST IS CRAP! Don't use rand for creating test data! Make a conscious decision about decent test matrices and tolerances and hard code that!! 
            linsolve_run_types(A1, A2, MatrixProperties.Square, 1e-2);
        }

        private void linsolve_run_types(Array<double> A, Array<double> B, MatrixProperties props, double tol) {
            Array<double> Res = linsolve(A, B, ref props);
            Assert.IsTrue(norm(multiply(A, Res) - B) < tol, "linsolve: invalid result (double)");
            Array<float> AF = tosingle(A);
            Array<float> BF = tosingle(B);
            Array<float> ResF = linsolve(AF, BF, ref props);
            Assert.IsTrue(norm(multiply(AF, ResF) - BF) < (float)(tol * 10), "linsolve: invalid result (float)");
            Array<fcomplex> AFc = tofcomplex(A);
            Array<fcomplex> BFc = tofcomplex(B);
            Array<fcomplex> ResFc = linsolve(AFc, BFc, ref props);
            Array<float> errFc = norm(multiply(AFc, ResFc) - BFc);  
            Assert.IsTrue(errFc < (float)tol, $"linsolve: invalid result (fcomplex): {errFc}");
            Array<complex> AC = tocomplex(A);
            Array<complex> BC = tocomplex(B);
            Array<complex> ResC = linsolve(AC, BC, ref props);
            Assert.IsTrue(norm(multiply(AC, ResC) - BC) < tol, "linsolve: invalid result (complex)");
        }


    }
}
