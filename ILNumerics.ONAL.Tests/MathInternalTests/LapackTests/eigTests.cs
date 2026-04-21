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
    public class eigTests {

        [TestMethod]
        public void eigNonSymm1() {
            Test_eigNonSymm(counter(1.0, 1.0, 4, 4), 1e-4, true);
        }
         [TestMethod]
        public void eigNonSymm2() {
            Test_eigNonSymm(counter(1.0, 1.0, 4, 4), 1e-4, false);
        }
        [TestMethod]
        public void eigNonSymm3() {
            Test_eigNonSymm(vector<double>(new double[] { 0.304617, 0.189654, 0.193431, 0.682223, 0.302764, 0.541674, 0.150873, 0.697898, 0.378373, 0.860012, 0.853655, 0.593563, 0.496552, 0.899769, 0.821629, 0.64491 }).Reshape(4, 4), 1e-4, false);
        }
        [TestMethod]
        public void eigNonSymm4() {
            Array<double> tmp = randn(4);
            Test_eigNonSymm(tmp, 1e-4, true);
        }
        [TestMethod]
        public void eigNonSymm5() {
            Test_eigNonSymm(randn(0), 1e-4, false);
        }
        [TestMethod]
        public void eigNonSymm6() {
            Test_eigNonSymm(randn(1), 1e-4, false);
        }
        [TestMethod]
        public void eigNonSymm7() {
            Test_eigNonSymm(randn(2), 1e-4, false);

        }

        private void Test_eigNonSymm(Array<double> A, double tol, bool bal) {

            MatrixProperties props = MatrixProperties.None;
            Array<complex> V = null;
            Array<complex> res1 = eig(A, V, ref props, bal);
            V = empty<complex>();
            Array<complex> res2 = eig(A, V, ref props, bal);
            if (!A.IsEmpty) {
                Assert.IsFalse(norm(res1 - diag(res2)) > tol, "eig.values differ betw. e = eig(A) and [e,v] = eig(A)! - (double)");
                Assert.IsFalse(norm(V) < tol, "result too small! trivial solution? (double)");
                Assert.IsFalse(norm(res1) < tol, "result too small! trivial solution? (double)");
                Assert.IsFalse(norm(multiply(tocomplex(A), V) - multiply(V, res2)) > tol, 
                    $"wrong result detected (double). {V} - {multiply(V, res2)} - Norm: {norm(multiply(tocomplex(A), V) - multiply(V, res2))}");
            }

            Array<float> Af = tosingle(A);
            Array<fcomplex> Vf = null;
            Array<fcomplex> res1f = eig(Af, Vf, ref props, bal);
            Vf = empty<fcomplex>();
            Array<fcomplex> res2f = eig(Af, Vf, ref props, bal);
            if (!A.IsEmpty) {
                Assert.IsTrue(res1f.Equals(diag(res2f)), "eig.values differ betw. e = eig(A) and [e,v] = eig(A)! - (float)");
                Assert.IsFalse(norm(Vf) < (float)tol, "result too small! trivial solution? (float)");
                Assert.IsFalse(norm(res1f) < (float)tol, "result too small! trivial solution? (float)");
                Assert.IsFalse(norm(multiply(tofcomplex(Af), Vf) - multiply(Vf, res2f)) > (float)tol, $"wrong result detected (float) Norm:{norm(multiply(tofcomplex(Af), Vf) - multiply(Vf, res2f))}");
            }
            Array<fcomplex> Afc = tofcomplex(A);
            Array<fcomplex> Vfc = null;
            Array<fcomplex> res1fc = eig(Afc, Vfc, ref props, bal);
            Vfc = empty<fcomplex>();
            Array<fcomplex> res2fc = eig(Afc, Vfc, ref props, bal);
            if (!A.IsEmpty) {
                Assert.IsTrue(res1fc.Equals(diag(res2fc)), "eig.values differ betw. e = eig(A) and [e,v] = eig(A)! - (fcomplex)");
                Assert.IsFalse(norm(Vfc) < (float)tol, "result too small! trivial solution? (fcomplex)");
                Assert.IsFalse(norm(res1fc) < (float)tol, "result too small! trivial solution? (fcomplex)");
                Assert.IsFalse(norm(multiply(tofcomplex(Afc), Vfc) - multiply(Vfc, res2fc)) > (float)tol, $"wrong result detected (fcomplex) Norm: {norm(multiply(tofcomplex(Afc), Vfc) - multiply(Vfc, res2fc))}");
            }
            Array<complex> Ac = tocomplex(A);
            Array<complex> Vc = null;
            Array<complex> res1c = eig(Ac, Vc, ref props, bal);
            Vc = empty<complex>();
            Array<complex> res2c = eig(Ac, Vc, ref props, bal);
            if (!A.IsEmpty) {
                Assert.IsTrue(res1c.Equals(diag(res2c)), "eig.values differ betw. e = eig(A) and [e,v] = eig(A)! - (complex)");
                Assert.IsFalse(norm(Vc) < tol, "result too small! trivial solution? (complex)");
                Assert.IsFalse(norm(res1c) < tol, "result too small! trivial solution? (complex)");
                Assert.IsFalse(norm(multiply(tocomplex(Ac), Vc) - multiply(Vc, res2c)) > tol, $"wrong result detected (complex) Norm: {norm(multiply(tocomplex(Ac), Vc) - multiply(Vc, res2c))}");
            }
        }

        [TestMethod]
        public void eigSymmRangeVal() {
            Test_eigSymmRangeVal(vector<double>(new double[] { 1, 2, 3, 4, 2, 3, 5, 7, 3, 5, 6, 9, 4, 7, 9, 10 }).Reshape(4, 4), 1e-4, -1.0, 3.0);
        }

        private void Test_eigSymmRangeVal(Array<double> A, double tol, double start, double end) {

            Array<double> V = empty<double>();
            Array<double> res = eigSymm(A, V, start, end);
            Assert.IsTrue(norm(V) > tol, "result too small! trivial solution?");
            Assert.IsTrue(norm(res) > tol, "result too small! trivial solution?");
            for (int i = 0; i < V.Size[1]; i++) {
                Assert.IsTrue(norm(multiply(A, V[full, i]) - V[full, i] * res[i, i]) < tol, $"invalid values (out of tolerance) - double. Norm, col {i}:{norm(multiply(A, V[full, i]) - V[full, i] * res[i, i])}");

            }
            V = null;
            Assert.IsTrue(norm(diag(res) - eigSymm(A, V, start, end)) < tol, "invalid values (out of tolerance) - double");
            Array<float> Vf = empty<float>();
            Array<float> Af = tosingle(A);
            Array<float> resf = eigSymm(Af, Vf, (float)start, (float)end);

            Assert.IsTrue(norm(Vf) > (float)tol, "result too small! trivial solution?");
            Assert.IsTrue(norm(resf) > (float)tol, "result too small! trivial solution?");

            for (int i = 0; i < Vf.Size[1]; i++) {
                Assert.IsTrue(norm(multiply(Af, Vf[full, i]) - Vf[full, i] * resf[i, i]) < (float)tol, "invalid values (out of tolerance) - float");
            }
            Vf = null;
            Assert.IsTrue(norm(diag(resf) - eigSymm(Af, Vf, (float)start, (float)end)) < (float)tol, "invalid values (out of tolerance) - float");
            Array<fcomplex> Vfc = empty<fcomplex>();
            Array<fcomplex> Afc = tofcomplex(A);
            Array<float> resfc = eigSymm(Afc, Vfc, (float)start, (float)end);
            Assert.IsTrue(norm(Vfc) > (float)tol, "result too small! trivial solution?");
            Assert.IsTrue(norm(resfc) > (float)tol, "result too small! trivial solution?");

            for (int i = 0; i < Vfc.Size[1]; i++) {
                Assert.IsTrue(norm(multiply(Afc, Vfc[full, i]) - Vfc[full, i] * tofcomplex(resfc[i, i])) < (float)tol, "invalid values (out of tolerance) - fcomplex");
            }
            Vfc = null;
            Assert.IsTrue(norm(diag(resfc) - eigSymm(Afc, Vfc, (float)start, (float)end)) < (float)tol, "invalid values (out of tolerance) - complex");
            Array<complex> Vc = empty<complex>();
            Array<complex> Ac = tocomplex(A);
            Array<double> resc = eigSymm(Ac, Vc, start, end);
            Assert.IsTrue(norm(Vc) > tol, "result too small! trivial solution?");
            Assert.IsTrue(norm(resc) > tol, "result too small! trivial solution?");
            for (int i = 0; i < Vc.Size[1]; i++) {
                Assert.IsTrue(norm(multiply(Ac, Vc[full, i]) - Vc[full, i] * tocomplex(resc[i, i])) < tol, "invalid values (out of tolerance) - complex");

            }
            Vc = null;
            Assert.IsTrue(norm(diag(resc) - eigSymm(Ac, Vc, start, end)) < tol, "invalid values (out of tolerance) - complex");

        }

        [TestMethod]
        public void eigSymmRangeIdx() {
            Test_eigSymmRangeIdx(vector<double>(new double[] { 1, 2, 3, 4, 2, 3, 5, 7, 3, 5, 6, 9, 4, 7, 9, 10 }).Reshape(4, 4), 1e-4, 1, 3);
        }


        private void Test_eigSymmRangeIdx(Array<double> A, double tol, int start, int end) {

            Array<double> V = empty<double>();
            Array<double> res = eigSymm(A, V, start, end);
            Assert.IsTrue(norm(V) > tol, "result too small! trivial solution?");
            Assert.IsTrue(norm(res) > tol, "result too small! trivial solution?");

            for (int i = 0; i < end - start + 1; i++) {
                Assert.IsTrue(norm(multiply(A, V[full, i]) - V[full, i] * res[i, i]) < tol, "invalid values (out of tolerance) - double");
            }
            V = null;
            Assert.IsTrue(norm(diag(res) - eigSymm(A, V, start, end)) < tol, "invalid values (out of tolerance) - double");
            Array<float> Vf = empty<float>();
            Array<float> Af = tosingle(A);
            Array<float> resf = eigSymm(Af, Vf, start, end);
            Assert.IsTrue((bool)(norm(Vf) > (float)tol) || (bool)(norm(resf) < (float)tol), "result too small! trivial solution?");
            Assert.IsTrue((bool)(norm(resf) > (float)tol) || (bool)(norm(resf) < (float)tol), "result too small! trivial solution?");
            for (int i = 0; i < end - start + 1; i++) {
                Assert.IsTrue(norm(multiply(Af, Vf[full, i]) - Vf[full, i] * resf[i, i]) < (float)tol, "invalid values (out of tolerance) - float");
            }
            Vf = null;
            Assert.IsTrue(norm(diag(resf) - eigSymm(Af, Vf, start, end)) < (float)tol, "invalid values (out of tolerance) - float");
            Array<fcomplex> Vfc = empty<fcomplex>();
            Array<fcomplex> Afc = tofcomplex(A);
            Array<float> resfc = eigSymm(Afc, Vfc, start, end);
            Assert.IsTrue(norm(Vfc) > (float)tol, "result too small! trivial solution?");
            Assert.IsTrue(norm(resfc) > (float)tol, "result too small! trivial solution?");
            for (int i = 0; i < end - start + 1; i++) {
                Assert.IsTrue(norm(multiply(Afc, Vfc[full, i]) - Vfc[full, i] * tofcomplex(resfc[i, i])) < (float)tol, "invalid values (out of tolerance) - fcomplex");
            }
            Vfc = null;
            Assert.IsTrue(norm(diag(resfc) - eigSymm(Afc, Vfc, start, end)) < (float)tol, "invalid values (out of tolerance) - complex");
            Array<complex> Vc = empty<complex>();
            Array<complex> Ac = tocomplex(A);
            Array<double> resc = eigSymm(Ac, Vc, start, end);
            Assert.IsTrue(norm(Vc) > tol, "result too small! trivial solution?");
            Assert.IsTrue(norm(resc) > tol, "result too small! trivial solution?");
            for (int i = 0; i < end - start + 1; i++) {
                Assert.IsTrue(norm(multiply(Ac, Vc[full, i]) - Vc[full, i] * tocomplex(resc[i, i])) < tol, "invalid values (out of tolerance) - complex");
            }
            Vc = null;
            Assert.IsTrue(norm(diag(resc) - eigSymm(Ac, Vc, start, end)) < tol, "invalid values (out of tolerance) - complex");

        }

        [TestMethod]
        public void eigSymmFull() {
            Test_eigSymmFull(vector<double>(new double[] { 1, 2, 3, 4, 2, 3, 5, 7, 3, 5, 6, 9, 4, 7, 9, 10 }).Reshape(4, 4), 1e-4);
        }


        private void Test_eigSymmFull(Array<double> A, double tol) {

            Array<double> V = empty<double>();
            Array<double> res = eigSymm(A, V);
            Assert.IsTrue(norm(V) > tol, "result too small! trivial solution?");
            Assert.IsTrue(norm(res) > tol, "result too small! trivial solution?");

            Assert.IsTrue(norm(multiply(A, V) - multiply(V, res)) < tol, "invalid values (out of tolerance) - double");

            V = null;
            Assert.IsTrue(norm(diag(res) - eigSymm(A, V)) < tol, "invalid values (out of tolerance) - double");


            Array<float> Vf = empty<float>();
            Array<float> Af = tosingle(A);
            Array<float> resf = eigSymm(Af, Vf);
            Assert.IsTrue(norm(Vf) > (float)tol, "result too small! trivial solution?");
            Assert.IsTrue(norm(resf) > (float)tol, "result too small! trivial solution?");

            Assert.IsTrue(norm(multiply(Af, Vf) - multiply(Vf, resf)) < (float)tol, "invalid values (out of tolerance) - float");


            // continue here!

            Vf = null;
            Assert.IsTrue(norm(diag(resf) - eigSymm(Af, Vf)) < (float)tol, "invalid values(out of tolerance) - float");
            Array<fcomplex> Vfc = empty<fcomplex>();

            Array<fcomplex> Afc = tofcomplex(A);
            Array<float> resfc = eigSymm(Afc, Vfc);

            Assert.IsTrue(norm(Vfc) > (float)tol, "result too small! trivial solution?");
            Assert.IsTrue(norm(resfc) > (float)tol, "result too small! trivial solution?");
            Assert.IsTrue(norm(multiply(Afc, Vfc) - multiply(Vfc, tofcomplex(resfc))) < (float)tol, "invalid values (out of tolerance) - fcomplex");

            Vfc = null;
            Assert.IsTrue(norm(diag(resfc) - eigSymm(Afc, Vfc)) < (float)tol, "invalid values (out of tolerance) - complex");


            Array<complex> Vc = empty<complex>();
            Array<complex> Ac = tocomplex(A);
            Array<double> resc = eigSymm(Ac, Vc);
            Assert.IsTrue(norm(Vc) > tol, "result too small! trivial solution?");
            Assert.IsTrue(norm(resc) > tol, "result too small! trivial solution?");
            Assert.IsTrue(norm(multiply(Ac, Vc) - multiply(Vc, tocomplex(resc))) < tol, "invalid values (out of tolerance) - complex");

            Vc = null;
            Assert.IsTrue(norm(diag(resc) - eigSymm(Ac, Vc)) < tol, "invalid values (out of tolerance) - complex");

        }


        [TestMethod]
        public void eigSymmGeneralized() {

            Array<double> A = new double[,] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 8, 7, 6, 5 }, { 5, 4, 3, 1 } };
            A = (A * A.T) / 2.0;
            Array<double> B = vector<double>(new double[] { 9, 2, 3, 4, 2, 9, 4, 5, 3, 4, 9, 6, 4, 5, 6, 9 }).Reshape(4, 4);

            Array<double> V = null;
            Array<double> W = eigGen(A, B, V, GenEigenType.Ax_eq_lambBx, false);
            // save eigenvalues for later comparison
            Array<double> oldEigVals = W.C;
            V = empty<double>();
            W = eigGen(A, B, V, GenEigenType.Ax_eq_lambBx, false);
            Assert.IsTrue(any((diag(W).T - oldEigVals) < eps * 100), "Eigenvalues did not match for 'EV only' <=> 'EV & EVectors'");

            // test for A*V = V*D
            Array<double> res = multiply(A, V) - multiply(B, multiply(V, W));
            Assert.IsTrue((res > eps * 100).NumberTrues == 0, "result should solve for: A*V = V*W (double)");


            ////////////////// float //////////////////////////////////////////////

            Array<float> Af = new float[,] { { 1f, 2f, 3f, 4f }, { 5f, 6f, 7f, 8f }, { 8f, 7f, 6f, 5f }, { 5f, 4f, 3f, 1f } };
            Af = (Af * Af.T) / 2.0f;
            Array<float> Bf = vector<float>(new float[] { 9f, 2f, 3f, 4f, 2f, 9f, 4f, 5f, 3f, 4f, 9f, 6f, 4f, 5f, 6f, 9f }).Reshape(4, 4);

            Array<float> Vf = null;
            Array<float> Wf = eigGen(Af, Bf, Vf, GenEigenType.Ax_eq_lambBx, false);
            // save eigenvalues for later comparison
            Array<float> oldEigValsf = Wf.C;
            Vf = empty<float>();

            Wf = eigGen(Af, Bf, Vf, GenEigenType.Ax_eq_lambBx, false);
            Assert.IsTrue(any((diag(Wf).T - oldEigValsf) < epsf * 100f), "Eigenvalues did not match for 'EV only' <=> 'EV & EVectors'");

            // test for A*V = V*D
            Array<float> resf = multiply(Af, Vf) - multiply(Bf, multiply(Vf, Wf));
            Assert.IsTrue((resf > epsf * 100f).NumberTrues == 0, "result should solve for: A*V = V*W (float)");

            ////////////////// fcomplex //////////////////////////////////////////////

            Array<fcomplex> Afc = ccomplex(Af,
                                    (Array<float>)new float[,] { { 0f, -1f, -2f, 3f }, { 1f, 0f, -2.5f, -1f }, { 2f, 2.5f, 0f, 0.81f }, { -3f, 1f, -0.81f, 0f } });
            Array<fcomplex> Bfc = ccomplex(Bf,
                                    (Array<float>)new float[,] { { 0f, -5f, -4f, -3f }, { 5f, 0f, -2f, -1f }, { 4f, 2f, 0f, 1f }, { 3f, 1f, -1f, 0f } });
            Array<fcomplex> Vfc = null;
            Array<float> Wfc = eigGen(Afc, Bfc, Vfc, GenEigenType.Ax_eq_lambBx, false);
            // save eigenvalues for later comparison
            Array<float> oldEigValsfc = Wfc.C;
            Vfc = empty<fcomplex>();
            Wfc = eigGen(Afc, Bfc, Vfc, GenEigenType.Ax_eq_lambBx, false);
            Assert.IsTrue(any((diag(Wfc).T - oldEigValsfc) < epsf * 100f), "Eigenvalues did not match for 'EV only' <=> 'EV & EVectors'");

            // test for A*V = V*D
            Array<fcomplex> resfc = multiply(Afc, Vfc) - multiply(Bfc, multiply(Vfc, tofcomplex(Wfc)));
            Assert.IsTrue((real(resfc) > epsf * 100).NumberTrues == 0, "result should solve for: A*V = V*W (fcomplex)");

            ///////////////////////////// complex /////////////////////////////////

            Array<complex> Ac = tocomplex(Afc);
            Array<complex> Bc = tocomplex(Bfc);

            Array<complex> Vc = null;
            Array<double> Wc = eigGen(Ac, Bc, Vc, GenEigenType.Ax_eq_lambBx, false);
            // save eigenvalues for later comparison
            Array<double> oldEigValsc = Wc.C;
            Vc = empty<complex>();
            Wc = eigGen(Ac, Bc, Vc, GenEigenType.Ax_eq_lambBx, false);
            Assert.IsTrue(any((diag(Wc).T - oldEigValsc) < eps * 100), "Eigenvalues did not match for 'EV only' <=> 'EV & EVectors'");
            // test for A*V = V*D
            Array<complex> resc = multiply(Ac, Vc) - multiply(Bc, multiply(Vc, tocomplex(Wc)));
            Assert.IsTrue(allall(abs(resc) < eps * Math.Pow(resc.S.NumberOfElements, 3)), "result should solve for: A*V = V*W (complex)");

        }

        [TestMethod]
        public void eig_empty() {

            Array<fcomplex> A = empty<fcomplex>(0, dim1: 1);
            Array<fcomplex> V = (fcomplex)1;
            Assert.IsTrue(eig(A).S.NumberOfDimensions == 2);
            Assert.IsTrue(eig(A).S[0] == 0);
            Assert.IsTrue(eig(A).S[1] == 1);

            Assert.IsTrue(eigSymm(A).S.NumberOfDimensions == 2);
            Assert.IsTrue(eigSymm(A).S[0] == 0);
            Assert.IsTrue(eigSymm(A).S[1] == 1);

        }
        [TestMethod]
        public void eigGen_empty() {

            Array<fcomplex> A = empty<fcomplex>(0, dim1: 0);
            Array<fcomplex> B = empty<fcomplex>(0, dim1: 0);
            Array<fcomplex> V = (fcomplex)1;
            Assert.IsTrue(eigGen(A, B).S.NumberOfDimensions == 2);
            Assert.IsTrue(eigGen(A, B).S[0] == 0);
            Assert.IsTrue(eigGen(A, B).S[1] == 0);

            Assert.IsTrue(eigGen(A, B, V).S.NumberOfDimensions == 2);
            Assert.IsTrue(eigGen(A, B, V).S[0] == 0);
            Assert.IsTrue(eigGen(A, B, V).S[1] == 0);

            Assert.IsTrue(V.S.NumberOfDimensions == 2);
            Assert.IsTrue(V.S[0] == 0); 
            Assert.IsTrue(V.S[1] == 0); 

        }
        [TestMethod]
        public void eigGen_scalar() {

            Array<fcomplex> A = (fcomplex)(10);
            Array<fcomplex> B = (fcomplex)(10);
            Array<fcomplex> V = (fcomplex)1;
            Assert.IsTrue(eigGen(A, B).S.NumberOfDimensions == 2);
            Assert.IsTrue(eigGen(A, B).S[0] == 1);
            Assert.IsTrue(eigGen(A, B).S[1] == 1);

            Assert.IsTrue(eigGen(A, B, V).S.NumberOfDimensions == 2);
            Assert.IsTrue(eigGen(A, B, V).S[0] == 1);
            Assert.IsTrue(eigGen(A, B, V).S[1] == 1);

            Assert.IsTrue(V.S.NumberOfDimensions == 2);
            Assert.IsTrue(V.S[0] == 1);
            Assert.IsTrue(V.S[1] == 1);


        }
        [TestMethod]
        public void eig_scalar() {

            Array<fcomplex> A = (fcomplex)(10);
            Array<fcomplex> V = (fcomplex)1;
            Assert.IsTrue(eig(A).S.NumberOfDimensions == 2);
            Assert.IsTrue(eig(A).S[0] == 1);
            Assert.IsTrue(eig(A).S[1] == 1);

            Assert.IsTrue(eig(A, V).S.NumberOfDimensions == 2);
            Assert.IsTrue(eig(A, V).S[0] == 1);
            Assert.IsTrue(eig(A, V).S[1] == 1);

            Assert.IsTrue(V.S.NumberOfDimensions == 2);
            Assert.IsTrue(V.S[0] == 1);
            Assert.IsTrue(V.S[1] == 1);


        }
        [TestMethod]
        public void eigSymm_scalar() {

            Array<fcomplex> A = (fcomplex)(10);
            Array<fcomplex> V = (fcomplex)1;
            Assert.IsTrue(eigSymm(A).S.NumberOfDimensions == 2);
            Assert.IsTrue(eigSymm(A).S[0] == 1);
            Assert.IsTrue(eigSymm(A).S[1] == 1);

            Assert.IsTrue(eigSymm(A, V).S.NumberOfDimensions == 2);
            Assert.IsTrue(eigSymm(A, V).S[0] == 1);
            Assert.IsTrue(eigSymm(A, V).S[1] == 1);

            Assert.IsTrue(V.S.NumberOfDimensions == 2);
            Assert.IsTrue(V.S[0] == 1);
            Assert.IsTrue(V.S[1] == 1);


        }
        [TestMethod]
        public void eigGen_scalarNP() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                Array<fcomplex> A = (fcomplex)(10);
                Array<fcomplex> B = (fcomplex)(10);
                Array<fcomplex> V = (fcomplex)1;
                Assert.IsTrue(eigGen(A, B).S.NumberOfDimensions == 2);
                Assert.IsTrue(eigGen(A, B).S[0] == 1);
                Assert.IsTrue(eigGen(A, B).S[1] == 1);

                Assert.IsTrue(eigGen(A, B, V).S.NumberOfDimensions == 2);
                Assert.IsTrue(eigGen(A, B, V).S[0] == 1);
                Assert.IsTrue(eigGen(A, B, V).S[1] == 1);

                Assert.IsTrue(V.S.NumberOfDimensions == 2);
                Assert.IsTrue(V.S[0] == 1);
                Assert.IsTrue(V.S[1] == 1);
            }
        }
        [TestMethod]
        public void eig_scalarNP() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<fcomplex> A = (fcomplex)(10);
                Array<fcomplex> V = (fcomplex)1;
                Assert.IsTrue(eig(A).S.NumberOfDimensions == 1);
                Assert.IsTrue(eig(A).S[0] == 1);
                Assert.IsTrue(eig(A).S[1] == 1);

                Assert.IsTrue(eig(A, V).S.NumberOfDimensions == 1);
                Assert.IsTrue(eig(A, V).S[0] == 1);
                Assert.IsTrue(eig(A, V).S[1] == 1);

                Assert.IsTrue(V.S.NumberOfDimensions == 2);
                Assert.IsTrue(V.S[0] == 1);
                Assert.IsTrue(V.S[1] == 1);

            }
        }
        [TestMethod]
        public void eigSymm_scalarNP() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<fcomplex> A = (fcomplex)(10);
                Array<fcomplex> V = (fcomplex)1;
                Assert.IsTrue(eigSymm(A).S.NumberOfDimensions == 1);
                Assert.IsTrue(eigSymm(A).S[0] == 1);
                Assert.IsTrue(eigSymm(A).S[1] == 1);

                Assert.IsTrue(eigSymm(A, V).S.NumberOfDimensions == 1);
                Assert.IsTrue(eigSymm(A, V).S[0] == 1);
                Assert.IsTrue(eigSymm(A, V).S[1] == 1);

                Assert.IsTrue(V.S.NumberOfDimensions == 2);
                Assert.IsTrue(V.S[0] == 1);
                Assert.IsTrue(V.S[1] == 1);
            }

        }
    }
}
