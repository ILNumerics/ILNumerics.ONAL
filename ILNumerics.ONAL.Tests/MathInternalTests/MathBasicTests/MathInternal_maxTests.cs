using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;
using static ILNumerics.Globals; 

namespace ILNumerics.Core.UnitTests.MathInternalTests {
    [TestClass]
    public class MathInternal_maxTests {

        [TestMethod]
        public void MathInternal_max_noindicesSimpleTest() {

            Array<double> A = counter<double>(1.1, 1.0, 5, 4);
            Array<double> B = max(A, dim: 0);
            Array<double> R = new[] { 5.1, 10.1, 15.1, 20.1 };

            Assert.IsTrue(R.T.Equals(B));
            A = counter<double>(20.0, -1.0, 5, 4);
            B = max(A, dim: 0);
            R = new[] { 20, 15, 10, 5 };
        }

        [TestMethod]
        public void MathInternal_max_noindicesNANTest() {
            // NaN values are ignored
            Array<double> A = counter<double>(1.2, 1.0, 5, 4);
            A[full, 1] = double.NaN; 

            Array<double> B = max(A, dim: 0);
            Array<double> R = new[] { 5.2, double.NaN, 15.2, 20.2 };

            Assert.IsTrue(R.T.Equals(B)); // object.Equals(double.NaN, double.NaN) -> true!!

            A[end, 1] = -1.0;
            B = max(A, dim: 0);
            R[1] = -1;
            Assert.IsTrue(R.T.Equals(B));

            A[1, 1] = 2.0;
            B = max(A, dim: 0);
            R[1] = 2;
            Assert.IsTrue(R.T.Equals(B));
        }
        [TestMethod]
        public void MathInternal_max_NaNIndices() {
            Array<double> A = counter<double>(1.2, -1.0, 4,3);
            A[r(1, -1), 0] = double.NaN;
            Array<long> I = 0;
            Array<double> B = max(A, I: I);
            Assert.IsTrue(B.GetValue(0) == 1.2); 
        }

        [TestMethod]
        public void MathInternal_max_numpyScalar() {
            using (Settings.Ensure(()=> Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = -10;
                Array<double> B = max(A,dim:0);
                Assert.IsTrue(B.Equals(A));

                Assert.IsTrue(max(double.NaN).Equals(double.NaN)); 
            }
        }

        [TestMethod]
        public void MathInternal_max_IndicesSimpleTest() {
            Array<double> A = counter(2.0, 1.0, 5, 4);
            Array<long> I = 1;
            Array<double> B = max(A, I, dim: 0);

            Array<long> Res = new long[] { 4, 4, 4, 4 };
            Array<double> ResB = new double[] { 6, 11, 16, 21 };

            Assert.IsTrue(I.S.IsSameShape(B.S));
            Assert.IsTrue(I.Equals(Res.T)); 
            Assert.IsTrue(B.Equals(ResB.T));
        }

        [TestMethod]
        public void MathInternal_max_IndicesIntTestRowMaj() {

            Array<int> A = counter<int>(1, 1, 4, 3, 5, StorageOrders.RowMajor);

            Array<long> I = 1;
            Array<int> B = max(A, I, keepdim: false);
            Array<int> Res = new int[,] {
                { 46,          47,          48,          49,          50 },
                { 51,          52,          53,          54,          55 },
                { 56,          57,          58,          59,          60 }
            };
            Array<long> ResI = ones<long>(3, 5) + 2;
            Assert.IsTrue(I.Equals(ResI));
            Assert.IsTrue(B.S.IsSameShape(I.S));
            Assert.IsTrue(B.Equals(Res)); 
        }

        [TestMethod]
        public void MathInternal_max_IndicesIntTestColMaj() {

            Array<int> A = counter<int>(1, 1, 4, 3, 5, StorageOrders.RowMajor);
            A.Storage.EnsureStorageOrder(StorageOrders.ColumnMajor); 

            Array<long> I = 1;
            Array<int> B = max(A, I, keepdim: false);
            Array<int> Res = new int[,] {
                { 46,          47,          48,          49,          50 },
                { 51,          52,          53,          54,          55 },
                { 56,          57,          58,          59,          60 }
            };
            Array<long> ResI = ones<long>(3, 5) + 2;
            Assert.IsTrue(I.Equals(ResI));
            Assert.IsTrue(B.S.IsSameShape(I.S));
            Assert.IsTrue(B.Equals(Res)); 
        }
        [TestMethod]
        public void MathInternal_max_IndicesIntTestColMaj_Keepdim() {
            // like MathInternal_max_IndicesIntTestColMaj
            Array<int> A = counter<int>(1, 1, 4, 3, 5, StorageOrders.RowMajor);
            A.Storage.EnsureStorageOrder(StorageOrders.ColumnMajor);

            Array<long> I = 1;
            Array<int> B = max(A, I, keepdim: true);
            Array<int> Res = new int[,,] {{
                { 46,          47,          48,          49,          50 },
                { 51,          52,          53,          54,          55 },
                { 56,          57,          58,          59,          60 }
            } };
            Array<long> ResI = ones<long>(1, 3, 5) + 2;
            Assert.IsTrue(I.Equals(ResI));
            Assert.IsTrue(B.S.IsSameShape(I.S));
            Assert.IsTrue(B.Equals(Res));
        }
        [TestMethod]
        public void MathInternal_max_IndicesIntTestRowMaj_Keepdim() {
            // like MathInternal_max_IndicesIntTestRowMaj
            Array<int> A = counter<int>(1, 1, 4, 3, 5, StorageOrders.RowMajor);

            Array<long> I = 1;
            Array<int> B = max(A, I, keepdim: true);
            Array<int> Res = new int[,,] {{
                { 46,          47,          48,          49,          50 },
                { 51,          52,          53,          54,          55 },
                { 56,          57,          58,          59,          60 }
            } };
            Array<long> ResI = ones<long>(1, 3, 5) + 2;
            Assert.IsTrue(I.Equals(ResI));
            Assert.IsTrue(B.S.IsSameShape(I.S));
            Assert.IsTrue(B.Equals(Res));
        }

        [TestMethod]
        public void MathInternal_max_largeParallel3Test() {
            uint cols = 1000;
            Array<ulong> A = counter<ulong>(1, 1, 1000, cols);
            Array<long> I = 1;
            Array<ulong> Res = counter<ulong>(1000, 1000, 1, cols), B;
            // warm up
            B = ILNumerics.Core.Functions.Builtin.MathInternal.max(A, I: I, dim: 0);
            
            //Res = Res * (Res + 1) / 2;
            //Res[r(1, end)] = Res[r(1, end)] - Res[r(0, end - 1)];
            var watch = new Stopwatch();
            var iters = 1000; 
            for (int i = 0; i < iters; i++)
                using (Scope.Enter()) {
                    using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {
                        watch.Start(); 
                        B = ILNumerics.Core.Functions.Builtin.MathInternal.max(A, I: I, dim: 0);
                        watch.Stop(); 
                    }
                    Assert.IsTrue(all(B == Res));
                    Assert.IsTrue(all(I == 999));
                }
            Trace.WriteLine($"Time elapsed: {watch.ElapsedMilliseconds / (double)iters}ms"); 
            
        }
        [TestMethod]
        public void MathInternal_min_largeParallel3Test() {
            uint cols = 1000;
            Array<long> A = counter<long>(-1, -1, 1000, cols);
            Array<long> I = 1;
            Array<long> Res = counter<long>(-1000, -1000, 1, cols), B;
            // warm up
            B = ILNumerics.Core.Functions.Builtin.MathInternal.min(A, I: I, dim: 0);

            //Res = Res * (Res + 1) / 2;
            //Res[r(1, end)] = Res[r(1, end)] - Res[r(0, end - 1)];
            var watch = new Stopwatch();
            var iters = 1000; 
            for (int i = 0; i < iters; i++)
                using (Scope.Enter()) {
                    using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {
                        watch.Start(); 
                        B = ILNumerics.Core.Functions.Builtin.MathInternal.min(A, I: I); //auto work dim: 0
                        watch.Stop(); 
                    }
                    Assert.IsTrue(all(B == Res));
                    Assert.IsTrue(all(I == 999));
                }
            Trace.WriteLine($"Time elapsed: {watch.ElapsedMilliseconds / (double)iters}ms"); 
            
        }

        [TestMethod]
        public void MathInternal_max1D2npScalar() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = vector<double>(1, 2, 3, 4, 5, 6, 17, 8, 9, 10, 11, 12);
                Assert.IsTrue(A.S.NumberOfDimensions == 1);
                Assert.IsTrue(A.S.NumberOfElements == 12);

                Array<double> R = max(A);
                Assert.IsTrue(R.S.NumberOfDimensions == 1);
                Assert.IsTrue(R.S.NumberOfElements == 1);
                Assert.IsTrue(R == 17);

                R = max(A, keepdim: false);
                Assert.IsTrue(R.S.NumberOfDimensions == 0);
                Assert.IsTrue(R.S.NumberOfElements == 1);
                Assert.IsTrue(R == 17);

            }
        }

    }
}
