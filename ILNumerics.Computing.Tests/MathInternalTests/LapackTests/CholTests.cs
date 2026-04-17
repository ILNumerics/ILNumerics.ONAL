using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;
using static ILNumerics.Globals;


namespace ILNumerics.Core.UnitTests.MathInternalTests.LapackTests {
    [TestClass]
    public class CholTests {
        [TestMethod]
        public void Test_CholPosDef() {

            Array<double> A = vector<double>(new double[] { 1, 1, 1, 1, 1, 2, 3, 4, 1, 3, 6, 10, 1, 4, 10, 20 }).Reshape(4, 4);
            Array<double> Res = vector<double>(new double[] { 1, 0, 0, 0, 1, 1, 0, 0, 1, 2, 1, 0, 1, 3, 3, 1 }).Reshape(4, 4);
            Array<double> B = chol(A, true);
            Assert.IsTrue(B.Equals(Res), "chol: invalid result (double)");

            Array<float> AF = tosingle(A);
            Array<float> ResF = tosingle(Res);
            Array<float> BF = chol(AF, true);
            Assert.IsTrue(BF.Equals(ResF), "chol: invalid result (float)");

            Array<fcomplex> AC = tofcomplex(A);
            Array<fcomplex> ResC = tofcomplex(Res);
            Array<fcomplex> BC = chol(AC, true);
            Assert.IsTrue(BC.Equals(ResC), "chol: invalid result (fcomplex)");

            Array<complex> AfC = tocomplex(A);
            Array<complex> ResfC = tocomplex(Res);
            Array<complex> BfC = chol(AfC, true);
            Assert.IsTrue(BfC.Equals(ResfC), "chol: invalid result (complex)");

        }
        [TestMethod]
        public void Test_CholPosDef_RM() {

            Array<double> A = vector<double>(new double[] { 1, 1, 1, 1, 1, 2, 3, 4, 1, 3, 6, 10, 1, 4, 10, 20 }).Reshape(4, 4, StorageOrders.RowMajor);
            Array<double> Res = vector<double>(new double[] { 1, 0, 0, 0, 1, 1, 0, 0, 1, 2, 1, 0, 1, 3, 3, 1 }).Reshape(4, 4);
            Array<double> B = chol(A, true);
            Assert.IsTrue(B.Equals(Res), "chol: invalid result (double)");

            Array<float> AF = tosingle(A);
            Array<float> ResF = tosingle(Res);
            Array<float> BF = chol(AF, true);
            Assert.IsTrue(BF.Equals(ResF), "chol: invalid result (float)");

            Array<fcomplex> AC = tofcomplex(A);
            Array<fcomplex> ResC = tofcomplex(Res);
            Array<fcomplex> BC = chol(AC, true);
            Assert.IsTrue(BC.Equals(ResC), "chol: invalid result (fcomplex)");

            Array<complex> AfC = tocomplex(A);
            Array<complex> ResfC = tocomplex(Res);
            Array<complex> BfC = chol(AfC, true);
            Assert.IsTrue(BfC.Equals(ResfC), "chol: invalid result (complex)");
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor); 

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "chol: test for positive definiteness successfull. Exception was thrown.")]
        public void Test_CholPosDefI() {
            Array<double> A = vector<double>(new double[] { 1, 1, 1, 1, 1, 2, 3, 4, 1, 3, 6, 10, 1, 4, 10, 19 }).Reshape(4, 4);
            Array<double> B;
            B = chol(A, true);
 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "chol: test for positive definiteness successfull. Exception was thrown.")]
        public void Test_CholPosDefII() {

            Array<double> A = vector<double>(new double[] { 1, 1, 1, 1, 1, 2, 3, 4, 1, 3, 6, 10, 1, 4, 10, 19 }).Reshape(4, 4);
            Array<float> AF = tosingle(A);
            Array<float> BF;
            BF = chol(AF, true);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "chol: test for positive definiteness successfull. Exception was thrown.")]
        public void Test_CholPosDefII_RM() {

            Array<double> A = vector<double>(new double[] { 1, 1, 1, 1, 1, 2, 3, 4, 1, 3, 6, 10, 1, 4, 10, 19 }).Reshape(4, 4, StorageOrders.RowMajor);
            Array<float> AF = tosingle(A);
            Array<float> BF;
            BF = chol(AF, true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "chol: test for positive definiteness successfull. Exception was thrown.")]
        public void Test_CholPosDefIII() {

            Array<double> A = vector<double>(new double[] { 1, 1, 1, 1, 1, 2, 3, 4, 1, 3, 6, 10, 1, 4, 10, 19 }).Reshape(4, 4);
            Array<fcomplex> AC = tofcomplex(A);
            Array<fcomplex> BC;
            BC = chol(AC, true);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "chol: test for positive definiteness successfull. Exception was thrown.")]
        public void Test_CholPosDefIV() {

            Array<double> A = vector<double>(new double[] { 1, 1, 1, 1, 1, 2, 3, 4, 1, 3, 6, 10, 1, 4, 10, 19 }).Reshape(4, 4);
            Array<complex> AfC = tocomplex(A);
            Array<complex> BfC;
            BfC = chol(AfC, true);
        }




        [TestMethod]
        public void Test_CholNonPosDef() {
            // test non pos def. matrix - last column 

            Array<double> A = vector<double>(new double[] { 1, 1, 1, 1, 1, 2, 3, 4, 1, 3, 6, 10, 1, 4, 10, 19 }).Reshape(4, 4);
            Array<double> Res = vector<double>(new double[] { 1, 0, 0, 1, 1, 0, 1, 2, 1 }).Reshape(3, 3);
            Array<double> B;

            B = chol(A, false);
            Assert.IsTrue(B.Equals(Res), "chol: invalid result (double)");

            Array<float> AF = tosingle(A);
            Array<float> ResF = tosingle(Res);
            Array<float> BF;

            BF = chol(AF, false);
            Assert.IsTrue(BF.Equals(ResF), "chol: invalid result (float)");

            Array<fcomplex> AC = tofcomplex(A);
            Array<fcomplex> ResC = tofcomplex(Res);
            Array<fcomplex> BC;
            BC = chol(AC, false);
            Assert.IsTrue(BC.Equals(ResC), "chol: invalid result (fcomplex)");

            Array<complex> AfC = tocomplex(A);
            Array<complex> ResfC = tocomplex(Res);
            Array<complex> BfC;
            BfC = chol(AfC, false);
            Assert.IsTrue(BfC.Equals(ResfC), "chol: invalid result (complex)");
        }
    }
}
