using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;
using static ILNumerics.Globals;


namespace ILNumerics.Core.UnitTests.MathInternalTests {

    [TestClass]
    public class LUTests {

        [TestMethod]
        public void Test_LU_retLUP() {

            // double
            Array<double> U = empty<double>();
            Array<double> A = vector<double>(
                new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 }).Reshape(4, 6);
            Array<double> P = empty<double>();
            Array<double> L = lu(A, U, P);
            Assert.IsTrue(allall((multiply(L, U) - multiply(P, A)) < 1e-6), "lu: invalid result (double,L)");

            // complex 
            Array<complex> UC = empty<complex>();
            Array<complex> PC = empty<complex>();
            Array<complex> AC = tocomplex(A);
            Array<complex> LC = lu(AC, UC, PC);
            Assert.IsTrue(allall(abs(multiply(LC, UC) - multiply(PC, AC)) < 1e-6), "lu: invalid result (complex,L)");

            // float 
            Array<float> UF = empty<float>();
            Array<float> PF = empty<float>();
            Array<float> AF = tosingle(A);
            Array<float> LF = lu(AF, UF, PF);
            Assert.IsTrue(allall(abs(multiply(LF, UF) - multiply(PF, AF)) < 1e-6f), "lu: invalid result (float,L)");

            // fcomplex 
            Array<fcomplex> UFc = empty<fcomplex>();
            Array<fcomplex> PFc = empty<fcomplex>();
            Array<fcomplex> AFc = tofcomplex(A);
            Array<fcomplex> LFc = lu(AFc, UFc, PFc);
            Assert.IsTrue(allall(abs(multiply(LFc, UFc) - multiply(PFc, AFc)) < 1e-6f), "lu: invalid result (fcomplex,L)");

        }

        [TestMethod]
        public void Test_LU_retLU() {

            // double
            Array<double> U = empty<double>();
            Array<double> A = vector<double>(new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 }).Reshape(4, 6, StorageOrders.ColumnMajor);
            Array<double> L = lu(A, U);
            Assert.IsTrue(allall(multiply(L, U) - A < 1e-6), "lu: invalid result (double,L)");

            // complex 
            Array<complex> UC = empty<complex>();
            Array<complex> AC = tocomplex(A);
            Array<complex> LC = lu(AC, UC);
            Assert.IsTrue(allall(abs(multiply(LC, UC) - AC) < 1e-6), "lu: invalid result (complex,L)");

            // float 
            Array<float> UF = empty<float>();
            Array<float> AF = tosingle(A);
            Array<float> LF = lu(AF, UF);
            Assert.IsTrue(allall(multiply(LF, UF) - AF < 1e-4f), "lu: invalid result (float,L)");

            // fcomplex 
            Array<fcomplex> UFc = empty<fcomplex>();
            Array<fcomplex> AFc = tofcomplex(A);
            Array<fcomplex> LFc = lu(AFc, UFc);
            Assert.IsTrue(allall(abs(multiply(LFc, UFc) - AFc) < 1e-4f), "lu: invalid result (fcomplex,L)");

        }
        [TestMethod]
        public void Test_LU_retLU_RM() {

            // double
            Array<double> U = empty<double>();
            Array<double> A = vector<double>(new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 }).Reshape(4, 6, StorageOrders.RowMajor);
            Array<double> L = lu(A, U);
            Assert.IsTrue(allall(multiply(L, U) - A < 1e-6), "lu: invalid result (double,L)");

            // complex 
            Array<complex> UC = empty<complex>();
            Array<complex> AC = tocomplex(A);
            Array<complex> LC = lu(AC, UC);
            Assert.IsTrue(allall(abs(multiply(LC, UC) - AC) < 1e-6), "lu: invalid result (complex,L)");

            // float 
            Array<float> UF = empty<float>();
            Array<float> AF = tosingle(A);
            Array<float> LF = lu(AF, UF);
            Assert.IsTrue(allall(multiply(LF, UF) - AF < 1e-4f), "lu: invalid result (float,L)");

            // fcomplex 
            Array<fcomplex> UFc = empty<fcomplex>();
            Array<fcomplex> AFc = tofcomplex(A);
            Array<fcomplex> LFc = lu(AFc, UFc);
            Assert.IsTrue(allall(abs(multiply(LFc, UFc) - AFc) < 1e-4f), "lu: invalid result (fcomplex,L)");

        }

        [TestMethod]
        public void Test_LU_rawLapackForm() {
            // double
            Array<double> A = vector<double>(new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 }).Reshape( 4, 6);
            Array<double> ResRaw = vector<double>(new double[] { 4.000000, 0.250000, 0.750000, 0.500000, 8.000000, 3.000000, 0.333333, 0.666667, 12.000000, 6.000000, 0.000000, 0.000000, 16.000000, 9.000000, 0.000000, 0.000000, 20.000000, 12.000000, 0.000000, 0.000000, 24.000000, 15.000000, 0.000000, 0.000000 }).Reshape( 4, 6);
            Array<double> L = lu(A);
            //if (find64(L - ResRaw > 1e-6).Length > 0)
            //Info("Test_LU_rawLapackForm: unexpected result (double,L) - this might be ok!? (check return values)");
            // complex 
            Array<complex> AC = tocomplex(A);
            Array<complex> ResRawC = tocomplex(ResRaw);
            Array<complex> LC = lu(AC);
            //if (find64(abs(LC - ResRawC) > 1e-6).Length > 0)
            //Info("Test_LU_rawLapackForm: unexpected result (complex,L) - this might be ok!? (check return values)");
            // float 
            ResRaw = vector<double>(new double[] { 4.000000, 0.250000, 0.500000, 0.750000, 8.000000, 3.000000, 0.666667, 0.333333, 12.000000, 6.000000, -0.000000, 0.500000, 16.000000, 9.000000, -0.000000, 0.000000, 20.000000, 12.000000, -0.000000, 0.000000, 24.000000, 15.000000, -0.000000, 0.000000 }).Reshape( 4, 6);
            Array<float> AF = tosingle(A);
            Array<float> ResRawF = tosingle(ResRaw);
            Array<float> LF = lu(AF);
            //if (find64(abs(LF - ResRawF) > 1e-6f).Length > 0)
            //Info("Test_LU_rawLapackForm: unexpected result (float,L) - this might be ok!? (check return values)");
            // fcomplex 
            Array<fcomplex> AFc = tofcomplex(A);
            Array<fcomplex> ResRawFc = tofcomplex(ResRawF);
            Array<fcomplex> LFc = lu(AFc);
            //if (find64(abs(LFc - ResRawFc) > 1e-6f).Length > 0)
            //Info("Test_LU_rawLapackForm: unexpected result (fcomplex,L) - this might be ok!? (check return values)");

        }

        [TestMethod]
        public void Test_LU_empty() {

            Array<complex> A = empty<complex>(2, dim1: 0);
            Array<complex> U = new complex();
            Array<complex> L = lu(A, U);

            Array<complex> Res = multiply(L, U) - A;
            Assert.IsTrue(Res.IsEmpty); 
            Assert.IsTrue(Res.S[0] == 2);
            Assert.IsTrue(Res.S[1] == 0);

            Array<complex> P = new complex();
            L = lu(A, U, P);
            Res = multiply(L, U) - multiply(P, A);
            Assert.IsTrue(Res.IsEmpty);
            Assert.IsTrue(Res.S[0] == 2);
            Assert.IsTrue(Res.S[1] == 0);

        }

        [TestMethod]
        public void Test_LU_docuExamples() {

            Array<double> A = new double[,]{
            {1, 2, 3},
            {4, 4, 4},
            {5, 6, 7}
            };

            Array<double> U = 1;
            Array<double> P = 1;
            Array<double> L = lu(A, U, P);

            Assert.IsTrue(allall(abs(multiply(P, A) - multiply(L, U)) < eps * 9));

            L = lu(A, U);
            Assert.IsTrue(allall(abs(A - multiply(L, U)) < eps * 9));

        }

        [TestMethod]
        public void LU_MgtN() {
            Array<float> A = counter<float>(-1f, 2f, 5, 3) * counter<float>(1, 1, 5, 3);
            Array<float> U = 1, P = 1;

            Array<float> L = lu(A, U, P);

            Assert.IsTrue(allall(multiply(L, U) - multiply(P, A) < epsf * 3375)); 
        }
    }
}
