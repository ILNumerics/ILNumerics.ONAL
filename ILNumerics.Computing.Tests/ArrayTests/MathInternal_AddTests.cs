using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILNumerics.Core.Functions.Builtin;
using static ILNumerics.ILMath;
using ILNumerics.Core.Functions.Builtin.InnerLoops.Add;
using System.Diagnostics;
using static ILNumerics.Globals; 

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public class MathInternal_AddTests {

        [TestMethod]
        public void Add_Simple() {

            Array<double> A = counter(1.0, 1.0, 5, 4);
            Array<double> B = counter(10.0, 10.0, 5, 4);
            Array<double> C = A + B;

            Assert.IsTrue(C.Equals(counter(11.0, 11.0, 5, 4)));

        }
        [TestMethod]
        public void Add_BCMatVec() {

            Array<double> A = counter(1.0, 1.0, 5, 4);
            Array<double> B = counter(10.0, 10.0, 5, 1);
            Array<double> C = A + B;
            Array<double> Res = new double[] { 11, 22, 33, 44, 55, 16, 27, 38, 49, 60, 21, 32, 43, 54, 65, 26, 37, 48, 59, 70 };
            Res.a = Res.Reshape(5, 4);

            Assert.IsTrue(C.Equals(Res));

        }
        [TestMethod]
        public void Add_BCMatVecR() {

            Array<double> A = counter(1.0, 1.0, 5, 4);
            Array<double> B = counter(10.0, 10.0, 1, 4);
            Array<double> C = A + B;
            Array<double> Res = new double[] { 11,12,13,14,15,26,27,28,29,30,41,42,43,44,45,56,57,58,59,60 };
            Res.a = Res.Reshape(5, 4);

            Assert.IsTrue(C.Equals(Res));

        }
        [TestMethod]
        public void Add_BCVecMat() {

            Array<double> A = counter(1.0, 1.0, 5, 4);
            Array<double> B = counter(10.0, 10.0, 5, 1);
            Array<double> C = B + A;
            Array<double> Res = new double[] { 11, 22, 33, 44, 55, 16, 27, 38, 49, 60, 21, 32, 43, 54, 65, 26, 37, 48, 59, 70 };
            Res.a = Res.Reshape(5, 4);

            Assert.IsTrue(C.Equals(Res));

        }
        [TestMethod]
        public void Add_BCVecRMat() {

            Array<double> A = counter(1.0, 1.0, 5, 4);
            Array<double> B = counter(10.0, 10.0, 1, 4);
            Array<double> C = B + A;
            Array<double> Res = new double[] { 11,12,13,14,15,26,27,28,29,30,41,42,43,44,45,56,57,58,59,60 };
            Res.a = Res.Reshape(5, 4);

            Assert.IsTrue(C.Equals(Res));

        }

        [TestMethod]
        public void Add_BCVecMat_RowMaj() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = counter(1.0, 1.0, 5, 4);
                Array<double> B = counter(10.0, 10.0, 5, 1);
                Array<double> C = B + A;
                Array<double> Res = new double[] { 11, 22, 33, 44, 55, 16, 27, 38, 49, 60, 21, 32, 43, 54, 65, 26, 37, 48, 59, 70 };
                Res.a = Res.Reshape(5, 4, StorageOrders.ColumnMajor);

                Assert.IsTrue(C.Equals(Res));
                // took storage order from A : EDIT V6: don't rely on certain storage order of operation results!!
                //Assert.IsTrue(C.S.StorageOrder == StorageOrders.ColumnMajor);
            }

        }
        [TestMethod]
        public void Add_BCVecMat_RowMajA() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = counter(1.0, 1.0, 5, 4, StorageOrders.RowMajor);
                Array<double> B = counter(10.0, 10.0, 5, 1);
                Array<double> C = B + A;
                Array<double> Res = new double[] { 11, 12, 13, 14, 25, 26, 27, 28, 39, 40, 41, 42, 53, 54, 55, 56, 67, 68, 69, 70 };
                Res.a = Res.Reshape(5, 4, StorageOrders.RowMajor);

                Assert.IsTrue(C.Equals(Res));
                // took storage order from A; EDIT V6 don't assum certain SO from op results! (below was wrong anyways ;))
                //Assert.IsTrue(C.S.StorageOrder == StorageOrders.RowMajor);
            }

        }
        [TestMethod]
        public void Add_BCVecMat_RowMajA_inplace() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                var A = counter(1.0, 1.0, 5, 4, StorageOrders.RowMajor);
                Array<double> B = counter(10.0, 10.0, 5, 1);
                // use A as output - inplace
                Array<double> C = B + A;
                Array<double> Res = new double[] { 11, 12, 13, 14, 25, 26, 27, 28, 39, 40, 41, 42, 53, 54, 55, 56, 67, 68, 69, 70 };
                Res.a = Res.Reshape(5, 4, StorageOrders.RowMajor);

                Assert.IsTrue(C.Equals(Res));
                // took storage order from AEDIT V6: don't rely on certain storage order of operation results!!
                //Assert.IsTrue(C.S.StorageOrder == StorageOrders.RowMajor);
                // Assert.IsTrue(Equals(A.Storage, C.Storage));
            }

        }
        [TestMethod]
        public void Add_BCVecMat_RowMajA_inplaceBaseOffset() {
            //ILN(enabled=false)

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                var A = counter(-3.0, 1.0, 6, 4, StorageOrders.RowMajor)[Globals.r(1, Globals.end), Globals.full];
                // A looks like counter(1.0, 1.0, 5,4) now. But it has base offset = 4 !
                Assert.IsTrue(A.Storage.S.BaseOffset == 4);

                Array<double> B = counter(10.0, 10.0, 5, 1);

                Array<double> C = B + A;  
                // Note, this test was once checking that B + A uses A (RetT) for storing the result (inplace). But it is an implementation detail 
                // and in general not guaranteed (and in fact not true in ILNumerics.ONAL). 

                Array<double> Res = new double[] { 11, 12, 13, 14, 25, 26, 27, 28, 39, 40, 41, 42, 53, 54, 55, 56, 67, 68, 69, 70 };
                Res.a = Res.Reshape(5, 4, StorageOrders.RowMajor);

                Assert.IsTrue(C.Equals(Res));
            }
            //ILN(enabled=true)

        }

        #region multithreading tests
        [TestMethod]
        public void Add_3Threads() {
            Array<double> A = counter(1.0, 1.0, 100, 200, 1000); 
            Array<double> B = counter(1.0, 1.0, 100, 200, 1000);
            Array<double> Res = counter(2.0, 2.0, 100, 200, 1000); 
            Array<double> C;
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {
                C = A + B;
                var sw = Stopwatch.StartNew();
                C = A + B;
                sw.Stop(); 
                Trace.Write($"A + B {B.S.ToString()} took: {sw.ElapsedMilliseconds} (Ticks: {sw.ElapsedTicks}, Threads: {Settings.MaxNumberThreads}).");
            }
            Assert.IsTrue(C.Equals(Res));
        }
        #endregion

        #region completeness tests - check if all hycalper overloads are there 
        [TestMethod]
        public void Add_BC_allTypes() {
            Array<double> A = counter(2.0, 2.0, 3, 4);
            Array<double> B = 2;
            Assert.IsTrue(TestPlusAgainstDouble<double>(A, B, todouble));
            Assert.IsTrue(TestPlusAgainstDouble<float>(A, B, tosingle));
            Assert.IsTrue(TestPlusAgainstDouble<complex>(A, B, tocomplex));
            Assert.IsTrue(TestPlusAgainstDouble<fcomplex>(A, B, tofcomplex));
            // careful with rationals! conversions are not commutative!
            Assert.IsTrue(TestPlusAgainstDouble<sbyte>(A, B, toint8));
            Assert.IsTrue(TestPlusAgainstDouble<byte>(A, B, touint8));
            Assert.IsTrue(TestPlusAgainstDouble<short>(A, B, toint16));
            Assert.IsTrue(TestPlusAgainstDouble<ushort>(A, B, touint16));
            Assert.IsTrue(TestPlusAgainstDouble<int>(A, B, toint32));
            Assert.IsTrue(TestPlusAgainstDouble<uint>(A, B, touint32));
            Assert.IsTrue(TestPlusAgainstDouble<long>(A, B, toint64));
            Assert.IsTrue(TestPlusAgainstDouble<ulong>(A, B, touint64));
        }

        private bool TestPlusAgainstDouble<T>(InArray<double> A, InArray<double> B, Func<InArray<double>, Array<T>> conv) {
            using (Scope.Enter(A, B)) 
                return ((A + B).Equals(todouble(conv(A) + conv(B)))) && TestMinusAgainstDouble(A, B, conv);
        }
        private bool TestMinusAgainstDouble<T>(InArray<double> A, InArray<double> B, Func<InArray<double>, Array<T>> conv) {
            using (Scope.Enter(A, B))
                return ((A - B).Equals(todouble(conv(A) - conv(B)))) && TestMultiplyElemAgainstDouble(A,B,conv);
        }
        private bool TestMultiplyElemAgainstDouble<T>(InArray<double> A, InArray<double> B, Func<InArray<double>, Array<T>> conv) {
            using (Scope.Enter(A, B))
                return ((A * B).Equals(todouble(conv(A) * conv(B)))) && TestDivideAgainstDouble(A, B, conv);
        }
        private bool TestDivideAgainstDouble<T>(InArray<double> A, InArray<double> B, Func<InArray<double>, Array<T>> conv) {
            using (Scope.Enter(A, B))
                return ((A / B).Equals(todouble(conv(A) / conv(B))));
        }
        [TestMethod]
        public void Subtract_emptyVectorTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = empty<double>(0);
                Array<double> B = empty<double>(0);
                Array<double> R = A - B;

                Assert.IsTrue(R.IsEmpty);
                Assert.IsTrue(R.S.NumberOfDimensions == 1);
                Assert.IsTrue(R.S.NumberOfElements == 0);
            }
        }

        #endregion

        #region Logical Tests

        [TestMethod]
        public void LowerOrEqualTest_Simple() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {
                Array<double> A = counter(1.0, 1.0, 5, 4);
                A[A[full, 1] % 2 == 0, full] = -1;

                Array<double> Res = new double[] { -1, 2, -1, 4, -1, -1, 7, -1, 9, -1, -1, 12, -1, 14, -1, -1, 17, -1, 19, -1 };
                Res.a = Res.Reshape(5, 4, StorageOrders.ColumnMajor);
                Assert.IsTrue(A.Equals(Res));
            }
        }
        [TestMethod]
        public void EqualTo_emptyVectorTest() {
            using (Settings.Ensure(()=> Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = empty<double>(0); 
                Array<double> B = empty<double>(0);
                Logical L = A == B;

                Assert.IsTrue(L.IsEmpty); 
                Assert.IsTrue(L.S.NumberOfDimensions == 1);
                Assert.IsTrue(L.S.NumberOfElements == 0);
            }

        }
        [TestMethod]
        public void LowerOrEqualTest_SimpleMultithreaded2() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy))
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 2u)) {
                Array<double> A = counter(1.0, 1.0, 10, 20, StorageOrders.ColumnMajor);
                Logical L = A % 2 == 0;
                Assert.IsTrue(L.Storage.NumberTrues == 100);
                A[L, full] = -1; // no error? 

                Logical R = A.Reshape(1, A.S.NumberOfElements, StorageOrders.ColumnMajor)[0, r(1, 2, end)] == -1;
                Assert.IsTrue(R.Storage.NumberTrues == R.S.NumberOfElements);
            }
        }
        [TestMethod]
        public void LowerOrEqualTest_SimpleMultithreaded3() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy))
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {
                Array<double> A = counter(1.0, 1.0, 10, 20, StorageOrders.ColumnMajor);
                Logical L = A % 2 == 0;
                Assert.IsTrue(L.Storage.NumberTrues == 100);
                A[L, full] = -1; // no error? 

                Logical R = A.Reshape(1, A.S.NumberOfElements, StorageOrders.ColumnMajor)[0, r(1, 2, end)] == -1;
                Assert.IsTrue(R.Storage.NumberTrues == R.S.NumberOfElements);
            }
        }
        #endregion


    }
}
