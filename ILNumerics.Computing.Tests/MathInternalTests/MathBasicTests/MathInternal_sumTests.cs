using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.Globals; 
using static ILNumerics.ILMath;

namespace ILNumerics.Core.UnitTests.MathInternalTests {
    [TestClass]
    public class MathInternal_sumTests {

        [TestMethod]
        public void MathInternal_sum_Simple() {

            Array<double> A = counter(1.0, 1.0, 5, 4);

            Array<double> B = sum(A, 0);
            Array<double> Res = new double[] { 15, 40, 65, 90 };

            Assert.IsTrue(B.Equals(Res.T));

            Res = new double[] { 34, 38, 42, 46, 50 };
            Assert.IsTrue(Res.Equals(sum(A, 1)));

        }
        [TestMethod]
        public void MathInternal_sum_SimpleRowMajor() {

            Array<double> A = counter(1.0, 1.0, 5, 4);
            A.Storage.EnsureStorageOrder(StorageOrders.RowMajor);

            Array<double> B = sum(A, 0);
            Array<double> Res = new double[] { 15, 40, 65, 90 };

            Assert.IsTrue(B.Equals(Res.T));

            Res = new double[] { 34, 38, 42, 46, 50 };
            Assert.IsTrue(Res.Equals(sum(A, 1)));

        }
        [TestMethod]
        public void MathInternal_sum_3D_ColumnRet() {

            Array<double> A = counter(1.0, 1.0, 5, 4, 3, StorageOrders.RowMajor);
            A.Storage.EnsureStorageOrder(StorageOrders.ColumnMajor);

            Array<double> B = sum(A[ellipsis], 0);
            Array<double> Res = new double[,,] {
            {
                   { 125, 130, 135 },
                   { 140, 145, 150 },
                   { 155, 160, 165 },
                   { 170, 175, 180 },
            } };
            Assert.IsTrue(B.Equals(Res));

            B = sum(A[ellipsis], 1);
            Res = new double[,,] {
            {{ 22,  26,  30} },
            {{ 70,  74,  78} },
            {{118, 122, 126} },
            {{166, 170, 174} },
            {{214, 218, 222} }};
            Assert.IsTrue(Res.Equals(B));

            B = sum(A[ellipsis], 2);
            Res = new double[] { 6, 15, 24, 33, 42, 51, 60, 69, 78, 87, 96, 105, 114, 123, 132, 141, 150, 159, 168, 177 };
            Res.a = Res.Reshape(5, 4, 1, StorageOrders.RowMajor);
            Assert.IsTrue(Res.Equals(B));


        }

        [TestMethod]
        public void MathInternal_sum_3D_RowRet() {
            Settings.MaxNumberThreads = 2; 
            Array<double> A = counter(1.0, 1.0, 5, 4, 3, StorageOrders.RowMajor);
            A.Storage.EnsureStorageOrder(StorageOrders.RowMajor);

            Array<double> B = sum(A[ellipsis], 0);
            Array<double> Res = new double[,,] {
            {
                   { 125, 130, 135 },
                   { 140, 145, 150 },
                   { 155, 160, 165 },
                   { 170, 175, 180 },
            } };
            Assert.IsTrue(B.Equals(Res));

            B = sum(A[ellipsis], 1);
            Res = new double[,,] {
            {{ 22,  26,  30} },
            {{ 70,  74,  78} },
            {{118, 122, 126} },
            {{166, 170, 174} },
            {{214, 218, 222} }};
            Assert.IsTrue(Res.Equals(B));

            B = sum(A[ellipsis], 2);
            Res = new double[] { 6, 15, 24, 33, 42, 51, 60, 69, 78, 87, 96, 105, 114, 123, 132, 141, 150, 159, 168, 177 };
            Res.a = Res.Reshape(5, 4, 1, StorageOrders.RowMajor);
            Assert.IsTrue(Res.Equals(B));
        }
        [TestMethod]
        public void MathInternal_sum_EmptyRow() {
            Array<float> A = counter<float>(1f, 1f, 5, 0, 3, StorageOrders.RowMajor);

            // empty along non-empty dimension works as expected
            Array<float> B = sum(A, 0);
            Assert.IsTrue(B.S[0] == 1);
            Assert.IsTrue(B.S[1] == 0);
            Assert.IsTrue(B.S[2] == 3);
            System.Threading.Thread.Sleep(3000); // allow kernels to complete compilation

            B = sum(A, 0);
            Assert.IsTrue(B.S[0] == 1);
            Assert.IsTrue(B.S[1] == 0);
            Assert.IsTrue(B.S[2] == 3);

            B = sum(A, 2);
            Assert.IsTrue(B.S[0] == 5);
            Assert.IsTrue(B.S[1] == 0);
            Assert.IsTrue(B.S[2] == 1);

            B = sum(A, 1);
            Assert.IsTrue(B.S[0] == 5);
            Assert.IsTrue(B.S[1] == 0);
            Assert.IsTrue(B.S[2] == 3);
        }
        [TestMethod]
        public void MathInternal_sum_EmptyCol() {
            Array<float> A = counter<float>(1f, 1f, 5, 0, 3, StorageOrders.ColumnMajor);

            // empty along non-empty dimension works as expected
            Array<float> B = sum(A, 0);
            Assert.IsTrue(B.S[0] == 1);
            Assert.IsTrue(B.S[1] == 0);
            Assert.IsTrue(B.S[2] == 3);

            B = sum(A, 2);
            Assert.IsTrue(B.S[0] == 5);
            Assert.IsTrue(B.S[1] == 0);
            Assert.IsTrue(B.S[2] == 1);

            B = sum(A, 1);
            Assert.IsTrue(B.S[0] == 5);
            Assert.IsTrue(B.S[1] == 0);
            Assert.IsTrue(B.S[2] == 3);
        }
        [TestMethod]
        public void MathInternal_sum_ScalarML() {

            Array<uint> A = 1;
            Assert.IsTrue(sum(A, 0).Equals(A)); 
            Assert.IsTrue(sum(A, 1).Equals(A)); 
            Assert.IsTrue(sum(A, 2).Equals(A));

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))] 
        public void MathInternal_sum_EmptyReduceKeepDimsFalseFails() {

            const bool keepDimsVal = false; 
            Array<short> A = ones<short>(1, 0, 2);
            sum(A, 1, keepdim: keepDimsVal); // cannot remove non-singleton dimension! 

        }
        [TestMethod]
        public void MathInternal_sum_large3D() {
            System.Diagnostics.Trace.WriteLine($"Threads configured: {Settings.MaxNumberThreads}");
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 2u))
            using (Scope.Enter(arrayStyle: ArrayStyles.ILNumericsV4)) {
            System.Diagnostics.Trace.WriteLine($"ArrayStyle configured: {Settings.ArrayStyle}");
                for (int a = 1; a < 10; a++) {
                    for (int b = 1; b < 20; b++) {
                        for (int c = 1; c < 15; c++) {
                            Test3DSumSize(a, b, c); 
                        }
                    }
                }
                for (int a = 1; a < 100; a += 10) {
                    for (int b = 1; b < 200; b += 19) {
                        for (int c = 1; c < 300; c += 23) {
                            Test3DSumSize(a, b, c);
                        }
                    }
                }
            }
        }
        private void Test3DSumSize(int a, int b, int c) {
            using (Scope.Enter()) {
                Array<long> A = counter<long>(1, 1, a, b, c);
                Array<long> B = sum(sum(sum(A, 0), 1), 2);
                long all = a * b * c;
                Assert.IsTrue((long)B == (all * (all + 1)) / 2, $"a:{a}, b:{b}, c:{c}");
                Assert.IsTrue(B.IsScalar, $"a:{a}, b:{b}, c:{c}" + $"B.IsScalar={B.IsScalar}");
                Assert.IsTrue(B.S.NumberOfDimensions == 3, $"a:{a}, b:{b}, c:{c}" + $"B.S.NumberOfDimensions={B.S.NumberOfDimensions}");
            }
        }

        [TestMethod]
        public void MathInternal_sumKeepDimsFalseTest() { 
            Array<uint> A = counter<uint>(1, 1, 5, 4, 3);
            Array<uint> B = sum(A, 0, false);
            Assert.IsTrue(B.S.NumberOfDimensions == 2);
            Assert.IsTrue(B.S[0] == 4); 
            Assert.IsTrue(B.S[1] == 3); 
            Assert.IsTrue(B.S[2] == 1);

            B = sum(A, 1, false);
            Assert.IsTrue(B.S.NumberOfDimensions == 2);
            Assert.IsTrue(B.S[0] == 5);
            Assert.IsTrue(B.S[1] == 3);
            Assert.IsTrue(B.S[2] == 1);

            B = sum(A, 2, false);
            Assert.IsTrue(B.S.NumberOfDimensions == 2);
            Assert.IsTrue(B.S[0] == 5);
            Assert.IsTrue(B.S[1] == 4);
            Assert.IsTrue(B.S[2] == 1);

            B = sum(A, 3, false);
            Assert.IsTrue(B.S.NumberOfDimensions == 3);
            Assert.IsTrue(B.S[0] == 5);
            Assert.IsTrue(B.S[1] == 4);
            Assert.IsTrue(B.S[2] == 3);

        }

        [TestMethod]
        public void MathInternal_sum1D2npScalar() {

            using (Settings.Ensure(()=> Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = vector<double>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);
                Assert.IsTrue(A.S.NumberOfDimensions == 1);  
                Assert.IsTrue(A.S.NumberOfElements == 12);

                Array<double> R = sum(A);
                Assert.IsTrue(R.S.NumberOfDimensions == 1);
                Assert.IsTrue(R.S.NumberOfElements == 1);
                Assert.IsTrue(R == 78);

                R = sum(A, keepdim: false);
                Assert.IsTrue(R.S.NumberOfDimensions == 0);
                Assert.IsTrue(R.S.NumberOfElements == 1);
                Assert.IsTrue(R == 78);

            }
        }
    }
}
