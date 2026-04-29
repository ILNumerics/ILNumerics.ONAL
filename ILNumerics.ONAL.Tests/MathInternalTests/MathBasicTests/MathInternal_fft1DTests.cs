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
            var diff = maxall(abs(B - Res));
            var maxDiff = eps * maxall(A) * A.S.NumberOfElements; 
            Assert.IsTrue(diff <= maxDiff, $"Result differs by: " + diff + ". Expected max difference: " + maxDiff);

            // test row major
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                A.Storage.EnsureStorageOrder(StorageOrders.RowMajor);
                A = zeros<double>(5, 4, 3, StorageOrders.RowMajor);
                A[ellipsis, 0] = counter<double>(1.0, 1.0, 5, 4);

                var tmpC = counter<double>(1, 1, 5, 4, 1);
                var tmpZ = array<double>(0, dim0: 3);

                Res = tocomplex(tmpC + tmpZ);

                B = fft(A);

                diff = maxall(abs(B - Res));
                maxDiff = eps * maxall(A) * A.S.NumberOfElements;
                Assert.IsTrue(diff <= maxDiff, $"Result differs by: " + diff + ". Expected max difference: " + maxDiff);
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
