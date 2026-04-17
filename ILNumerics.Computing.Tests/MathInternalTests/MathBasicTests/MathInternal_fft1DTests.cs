using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;
using static ILNumerics.Globals;
using System.Diagnostics;

namespace ILNumerics.Core.UnitTests.MathInternalTests {

    [TestClass]
    public class MathInternal_fft1DTests {

        [TestMethod]
        public void MathInternal_fft1D_simpleTest() {
            //Assert.IsTrue(Settings.DefaultStorageOrder == StorageOrders.ColumnMajor); 
            Array<double> A = zeros<double>(5, 4, 3);
            A[0, ellipsis] = counter<double>(1.0, 1.0, 1, 4, 3);

            Array<complex> B = fft(A);
            Array<complex> Res = tocomplex(counter<double>(1, 1, 1, 4, 3) + zeros<double>(5, 1, 1));
            Assert.IsTrue(B.Equals(Res));

            // test row major
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                A.Storage.EnsureStorageOrder(StorageOrders.RowMajor);
                A = zeros<double>(5, 4, 3, StorageOrders.RowMajor);
                A[ellipsis, 0] = counter<double>(1.0, 1.0, 5, 4);

                // following RetT handling was required to catch a bug in binary operators (broadcasting). It is left here for no specific reason. 
                var tmpC = counter<double>(1, 1, 5, 4, 1);
                Assert.IsTrue(tmpC.Storage.GetValueSeq(1) == 2 && tmpC.Storage.GetValueSeq(4) < 10, $"tmpC[2]: {tmpC.Storage.GetValueSeq(2)}, tmpC[4]: {tmpC.Storage.GetValueSeq(4)}");
                var tmpZ = array<double>(0, dim0: 3);
                Assert.IsTrue(tmpZ.Storage.GetValueSeq(0) == 0, $"tmpZ[0]: {tmpZ.Storage.GetValueSeq(0)}");
                Assert.IsTrue(tmpZ.Storage.GetValueSeq(1) == 0, $"tmpZ[1]: {tmpZ.Storage.GetValueSeq(1)}");
                Assert.IsTrue(tmpZ.Storage.GetValueSeq(2) == 0, $"tmpZ[2]: {tmpZ.Storage.GetValueSeq(2)}");

                var tmp = tmpC + tmpZ; 
                Assert.IsTrue(tmp.Storage.GetValueSeq(1) == 2 && tmp.Storage.GetValueSeq(4) < 10, $"tmp[2]: {tmp.Storage.GetValueSeq(2)}, tmp[4]: {tmp.Storage.GetValueSeq(4)}");

                System.Diagnostics.Trace.WriteLine($"41 tmp.Storage.ID:{tmp.Storage.ID}, tmp.Storage.ReferenceCount: {tmp.Storage.ReferenceCount}");
                System.Diagnostics.Trace.WriteLine($"42 Res={Res.Storage.ShortInfo(includeCounters: true, includeIDs: true)}");
                Res = tocomplex(tmp);
                System.Diagnostics.Trace.WriteLine($"44 tmp.Storage.ID:{tmp.Storage.ID}, tmp.Storage.ReferenceCount: {tmp.Storage.ReferenceCount}");
                System.Diagnostics.Trace.WriteLine($"45 Res={Res.Storage.ShortInfo(includeCounters: true, includeIDs: true)}");
                Assert.IsTrue(Res.GetValue(1, 0).real == 2 && Res.GetValue(4,0).real < 10, $"Res: {Res}, Res[1,0]: {Res.GetValue(1, 0)}, Res[4,0]: {Res.GetValue(4, 0)}");

                B = fft(A);
                Assert.IsTrue(Res.GetValue(1, 0).real == 2 && Res.GetValue(4, 0).real < 10, $"Res: {Res}, Res[1,0]: {Res.GetValue(1, 0)}, Res[4,0]: {Res.GetValue(4, 0)}");

                Assert.IsTrue(B.Equals(Res), $"B: {B}, Res: {Res}, Res[1,0]: {Res.GetValue(1,0)}, Res[4,0]: {Res.GetValue(4,0)}"); 
            }
        }

        [TestMethod]
        public void MathInternal_fft1D_backw_simple() {
            Array<double> A = counter<double>(5, 4, 3) / 100;

            Array<double> B = ifftsym(fft(A));

            Assert.IsTrue(allall(abs(A - B) < eps * maxall(abs(A)) * A.S.NumberOfElements)); 

        }

        [TestMethod]
        public void MathInternal_fft1D_empty() {
            Array<float> A = zeros<float>(5, 0, 3);
            Array<fcomplex> B = fft(A);
            Assert.IsTrue(B.S[0] == 5);
            Assert.IsTrue(B.S[1] == 0);
            Assert.IsTrue(B.S[2] == 3);
            Assert.IsTrue(B.S.NumberOfDimensions == A.S.NumberOfDimensions);
        }
        [TestMethod]
        public void MathInternal_fft1D_npscalar() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<float> A = 3;
                Array<fcomplex> B = fft(A);
                Assert.IsTrue(B.S.NumberOfDimensions == 0);
                Assert.IsTrue(B.S.NumberOfElements == 1);
                Assert.IsTrue(B.S[0] == 1);
                Assert.IsTrue(A.GetValue(0) == 3);
            }
        }
        [TestMethod]
        public void MathInternal_ifft1D_npscalar() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<fcomplex> A = new fcomplex(3f, 0);
                Array<fcomplex> B = ifft(A);
                Assert.IsTrue(B.S.NumberOfDimensions == 0);
                Assert.IsTrue(B.S.NumberOfElements == 1);
                Assert.IsTrue(B.S[0] == 1);
                Assert.IsTrue(A.GetValue(0) == new fcomplex(3f, 0));
            }
        }
        [TestMethod]
        public void MathInternal_fft1D_3Dscalar() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<float> A = ones<float>(1,1,1) * 3;
                Array<fcomplex> B = fft(A);
                Assert.IsTrue(B.S.NumberOfDimensions == 3);
                Assert.IsTrue(B.S.NumberOfElements == 1);
                Assert.IsTrue(B.S[0] == 1);
                Assert.IsTrue(B.S[2] == 1);
                Assert.IsTrue(A.GetValue(0) == 3);
            }
        }
        [TestMethod]
        public void MathInternal_ifft1D_3Dscalar() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<fcomplex> A = tofcomplex(ones<float>(1, 1, 1));
                Array<fcomplex> B = ifft(A);
                Assert.IsTrue(B.S.NumberOfDimensions == 3);
                Assert.IsTrue(B.S.NumberOfElements == 1);
                Assert.IsTrue(B.S[0] == 1);
                Assert.IsTrue(B.S[1] == 1);
                Assert.IsTrue(B.S[2] == 1);
                Assert.IsTrue(A.GetValue(0) == new fcomplex(1f, 0));
            }
        }

        [TestMethod]
        public void MathInternal_fft1D_2DArray_dim0() {
            Array<complex> A = ccomplex(
                    counter<double>(1.0, 1.0, 100, 500),
                    counter<double>(-1.0, -1.0, 100, 500)
                ) / new complex(10000.0, 1);

            Array<complex> B = fft(A, 0);

            Assert.IsTrue(B.Equals(fft(A)));

            using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {

                Assert.IsTrue(allall(abs(A - ifft(fft(A, 0), 0)) < eps * maxall(A).real() * A.S.NumberOfElements));

            }

        }
        [TestMethod]
        public void MathInternal_fft1D_2DArray_dim1() {
            Array<complex> A = ccomplex(
                    counter<double>(1.0, 1.0, 100, 500),
                    counter<double>(-1.0, -1.0, 100, 500)
                ) / new complex(10000.0, 1);

            Array<complex> B = fft(A, 1);

            using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {

                Assert.IsTrue(allall(abs(A - ifft(fft(A, 1), 1)) < eps * maxall(A).real() * A.S.NumberOfElements));

            }

        }
        [TestMethod]
        public void MathInternal_fft1D_2DArray_dimOOR() {
            Array<complex> A = ccomplex(
                    counter<double>(1.0, 1.0, 100, 500),
                    counter<double>(-1.0, -1.0, 100, 500)
                ) / new complex(10000.0, 1);

            Assert.IsTrue(fft(A, 2).Equals(A)); 
            Assert.IsTrue(fft(A, 3).Equals(A));

        }

        [TestMethod]
        public void MathInternal_fft1D_3DCont_0_CM() {
            Array<float> A = counter<float>(1f, 1f, 1, 4, 5) * vector<float>(1, 0, 0);
            //Assert.IsTrue(A.S.IsContinuous && A.S.StorageOrder == StorageOrders.ColumnMajor); 

            Array<fcomplex> B = fft(A, 0);
            Assert.IsTrue(B.Equals(tofcomplex(
                counter<float>(1f, 1f, 1, 4, 5) * vector<float>(1, 1, 1)))
            );

            Assert.IsTrue(anyall(abs(A - real(ifft(B, 0))) < epsf * maxall(A) * A.S.NumberOfElements));

        }
        [TestMethod]
        public void MathInternal_fft1D_3DCont_1_CM() {
            Array<float> A = counter<float>(1f, 1f, 3, 1, 5) * vector<float>(1, 0, 0, 0).Reshape(1,4,1);
            // Assert.IsTrue(A.S.IsContinuous && A.S.StorageOrder == StorageOrders.ColumnMajor, "A.S=" + A.S.ToString() + " order=" + A.S.StorageOrder);  // undefined with ACC!!

            Array<fcomplex> B = fft(A, 1);
            Assert.IsTrue(B.Equals(tofcomplex(
                counter<float>(1f, 1f, 3, 1, 5) * vector<float>(1, 1, 1, 1).Reshape(1, 4, 1)))
            );
            Assert.IsTrue(anyall(abs(A - real(ifft(B, 1))) < epsf * maxall(A) * A.S.NumberOfElements));
        }
        [TestMethod]
        public void MathInternal_fft1D_3DCont_2_CM() {
            //Segment.Default.AllowLegacyFallback = false;
            Array<float> A = counter<float>(1f, 1f, 3, 4, 1) * vector<float>(1, 0, 0, 0, 0).Reshape(1, 1, 5);
            // Assert.IsTrue(A.S.IsContinuous && A.S.StorageOrder == StorageOrders.ColumnMajor, "A.S=" + A.S.ToString() + " order=" + A.S.StorageOrder);  // undefined with ACC!!

            Array<fcomplex> B = fft(A, 2);
            Assert.IsTrue(B.Equals(tofcomplex(
                counter<float>(1f, 1f, 3, 4, 1) * vector<float>(1, 1, 1, 1, 1).Reshape(1, 1, 5)))
            );

            Assert.IsTrue(anyall(abs(A - real(ifft(B, 2))) < epsf * maxall(A) * A.S.NumberOfElements));
        }
        [TestMethod]
        public void MathInternal_fft1D_3DCont_0_RM() {
            Array<float> A = counter<float>(1f, 1f, 1, 4, 5, StorageOrders.RowMajor) * vector<float>(1, 0, 0);
            // Assert.IsTrue(A.S.IsContinuous); //&& A.S.StorageOrder == StorageOrders.RowMajor);

            Array<fcomplex> B = fft(A, 0);
            Assert.IsTrue(B.Equals(tofcomplex(
                counter<float>(1f, 1f, 1, 4, 5, StorageOrders.RowMajor) * vector<float>(1, 1, 1)))
            );

            Assert.IsTrue(anyall(abs(A - real(ifft(B, 0))) < epsf * maxall(A) * A.S.NumberOfElements));
        }
        [TestMethod]
        public void MathInternal_fft1D_3DCont_1_RM() {
            {
                Array<float> A = counter<float>(1f, 1f, 3, 1, 5, StorageOrders.RowMajor) * vector<float>(1, 0, 0, 0).Reshape(1, 4, 1);
                // Assert.IsTrue(A.S.IsContinuous); // && A.S.StorageOrder == StorageOrders.RowMajor);

                Array<fcomplex> B = fft(A, 1);
                Assert.IsTrue(B.Equals(tofcomplex(
                    counter<float>(1f, 1f, 3, 1, 5, StorageOrders.RowMajor) * vector<float>(1, 1, 1, 1).Reshape(1, 4, 1)))
                );
                Assert.IsTrue(anyall(abs(A - real(ifft(B, 1))) < epsf * maxall(A) * A.S.NumberOfElements));
            }
        }
        [TestMethod]
        public void MathInternal_fft1D_3DCont_2_RM() {
            Array<float> A = counter<float>(1f, 1f, 3, 4, 1, StorageOrders.RowMajor) * vector<float>(1, 0, 0, 0, 0).Reshape(1, 1, 5);
            // Assert.IsTrue(A.S.IsContinuous); // && A.S.StorageOrder == StorageOrders.RowMajor);

            Array<fcomplex> B = fft(A, 2);
            Assert.IsTrue(B.Equals(tofcomplex(
                counter<float>(1f, 1f, 3, 4, 1, StorageOrders.RowMajor) * vector<float>(1, 1, 1, 1, 1).Reshape(1, 1, 5)))
            );

            Assert.IsTrue(anyall(abs(A - real(ifft(B, 2))) < epsf * maxall(A) * A.S.NumberOfElements));
        }

        [TestMethod]
        public void MathInternal_fft1D_3DStrided_0() {
            Array<float> A = counter<float>(1f, 0.5f, 1, 8, 5) * vector<float>(1, 0, 0);
            A.a = A[full, r(0, 2, end), full];
            Assert.IsTrue(!A.S.IsContinuous);
            Assert.IsTrue(A.shape.Equals(vector<long>(3, 4, 5))); 

            Array<fcomplex> B = fft(A, 0);
            Assert.IsTrue(B.Equals(tofcomplex(
                counter<float>(1f, 0.5f, 1, 8, 5)[full,r(0,2,end),full] * vector<float>(1, 1, 1)))
            );

            Assert.IsTrue(anyall(abs(A - real(ifft(B, 0))) < epsf * maxall(A) * A.S.NumberOfElements));
        }
        [TestMethod]
        public void MathInternal_fft1D_3DStrided_1() {
            Array<float> A = counter<float>(1f, 0.5f, 6, 1, 5) * vector<float>(1, 0, 0, 0).Reshape(1,4,1);
            A.a = A[r(0, 2, end), full, full];
            Assert.IsTrue(!A.S.IsContinuous);
            Assert.IsTrue(A.shape.Equals(vector<long>(3, 4, 5)));

            Array<fcomplex> B = fft(A, 1);
            Assert.IsTrue(B.Equals(tofcomplex(
                counter<float>(1f, 1f, 3, 1, 5) * vector<float>(1, 1, 1, 1).Reshape(1, 4, 1)))
            );

            Assert.IsTrue(anyall(abs(A - real(ifft(B, 1))) < epsf * maxall(A) * A.S.NumberOfElements));
        }
        [TestMethod]
        public void MathInternal_fft1D_3DStrided_2() {
            Array<float> A = counter<float>(1f, 0.5f, 6, 4, 1) * vector<float>(1, 0, 0, 0, 0).Reshape(1, 1, 5);
            A.a = A[r(0, 2, end), full, full];
            Assert.IsTrue(!A.S.IsContinuous);
            Assert.IsTrue(A.shape.Equals(vector<long>(3, 4, 5)));

            Array<fcomplex> B = fft(A, 2);
            Assert.IsTrue(B.Equals(tofcomplex(
                counter<float>(1f, 1f, 3, 4, 1) * vector<float>(1, 1, 1, 1, 1).Reshape(1, 1, 5)))
            );

            Assert.IsTrue(anyall(abs(A - real(ifft(B, 2))) < epsf * maxall(A) * A.S.NumberOfElements));
        }
    }
}
