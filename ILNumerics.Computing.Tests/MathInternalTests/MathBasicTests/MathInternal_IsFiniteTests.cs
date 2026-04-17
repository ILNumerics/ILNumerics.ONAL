using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.Globals; 
using static ILNumerics.ILMath;

namespace ILNumerics.Core.UnitTests.MathInternalTests {
    [TestClass]
    public class MathInternal_isfiniteTests {

        [TestMethod]
        public void MathInternal_isfinite_Simple() {

            Array<double> A = counter(-2.0, 1.0, 5);
            Logical B = isfinite(A);
            Assert.IsTrue(B.Storage.IsNumberTruesCached);
            Assert.IsTrue(B.Storage.NumberTrues == A.S.NumberOfElements);
            Assert.IsTrue(B.Equals(A != -3));

        }
        [TestMethod]
        public void MathInternal_isfinite_SimpleLarge() {

            Array<double> A = counter(-200.0, 1.0, 200, 200, 10);
            A[r(0, 2, end), ellipsis] = double.PositiveInfinity;
            Logical B;
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {
                B = isfinite(A);
            }
            Assert.IsTrue(B.Storage.IsNumberTruesCached);
            Assert.IsTrue(B.Storage.NumberTrues == A.S.NumberOfElements / 2);
            Assert.IsTrue(B.Equals(abs(A % 2) == 1));

        }
        [TestMethod]
        public void MathInternal_isfinite_SimpleLarge_RM() {

            Array<double> A = counter(-200.0, 1.0, 200, 200, 10);
            A.Storage.EnsureStorageOrder(StorageOrders.RowMajor);

            A[r(0, 2, end), ellipsis] = double.PositiveInfinity;
            Logical B;
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {
                B = isfinite(A);
            }
            Assert.IsTrue(B.Storage.IsNumberTruesCached);
            Assert.IsTrue(B.Storage.NumberTrues == A.S.NumberOfElements / 2);
            Assert.IsTrue(B.Equals(abs(A % 2) == 1));

        }
        [TestMethod]
        public void MathInternal_isfinite_SimpleLarge_Strided() {

            Array<double> A = counter(-200.0, 1.0, 200, 200, 10);
            A.a = A[ellipsis, r(0, 2, end)];
            Assert.IsFalse(A.S.IsContinuous);

            A[r(0, 2, end), ellipsis] = double.PositiveInfinity;
            Logical B;
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {
                B = isfinite(A);
            }
            Assert.IsTrue(B.Storage.IsNumberTruesCached);
            Assert.IsTrue(B.Storage.NumberTrues == A.S.NumberOfElements / 2);
            Assert.IsTrue(B.Equals(abs(A % 2) == 1));

        }

        [TestMethod]
        public void MathInternal_isfinite_scalar() {

            Assert.IsTrue(isfinite(10.0));

            Array<float> A = float.NegativeInfinity;
            Assert.IsFalse(isfinite(A));

            A = float.NaN;
            Assert.IsFalse(isfinite(A));

            A = float.PositiveInfinity;
            Assert.IsFalse(isfinite(A));

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                A = -203;
                Assert.IsTrue(isfinite(A));

                A = float.NegativeInfinity;
                Assert.IsFalse(isfinite(A));

                A = float.NaN;
                Assert.IsFalse(isfinite(A));

                A = float.PositiveInfinity;
                Assert.IsFalse(isfinite(A));
            }
        }
        [TestMethod]
        public void MathInternal_isfinite_empty() {

            Assert.IsTrue(isfinite(empty<complex>()).IsEmpty);

            Array<fcomplex> A = empty<fcomplex>(1,2,0, order: StorageOrders.RowMajor);
            Logical B = isfinite(A); 
            Assert.IsTrue(B.S.IsSameShape(A.S));
        }

        [TestMethod]
        public void MathInternal_isNaN_simple() {

            Array<float> A = counter<float>(1.0f, 1.0f, 400, 300, 3);

            Array<fcomplex> C = ccomplex(A, A);
            C[100, 150, 1] = new fcomplex(1, float.NaN);

            using (Settings.Ensure(() => Settings.MaxNumberThreads, 1u)) {
                Assert.IsTrue(isnan(C).Storage.IsNumberTruesCached);
                Assert.IsTrue(isnan(C).Storage.NumberTrues == 1);
                Assert.IsTrue(find(isnan(C)) == A.S.GetSeqIndex(100, 150, 1));
            }
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 2u)) {
                Assert.IsTrue(isnan(C).Storage.IsNumberTruesCached);
                Assert.IsTrue(isnan(C).Storage.NumberTrues == 1);
                Assert.IsTrue(find(isnan(C)) == A.S.GetSeqIndex(100, 150, 1));
            }
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {
                Assert.IsTrue(isnan(C).Storage.IsNumberTruesCached);
                Assert.IsTrue(isnan(C).Storage.NumberTrues == 1);
                Assert.IsTrue(find(isnan(C)) == A.S.GetSeqIndex(100, 150, 1));
            }

        }
        [TestMethod]
        public void MathInternal_isPosInf_simple() {

            Array<float> A = counter<float>(1.0f, 1.0f, 400, 300, 3);

            Array<fcomplex> C = ccomplex(A, A);
            C[100, 150, 1] = new fcomplex(1, float.PositiveInfinity);

            using (Settings.Ensure(() => Settings.MaxNumberThreads, 1u)) {
                Assert.IsTrue(isposinf(C).Storage.IsNumberTruesCached);
                Assert.IsTrue(isposinf(C).Storage.NumberTrues == 1);
                Assert.IsTrue(find(isposinf(C)) == A.S.GetSeqIndex(100, 150, 1));
            }
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 2u)) {
                Assert.IsTrue(isposinf(C).Storage.IsNumberTruesCached);
                Assert.IsTrue(isposinf(C).Storage.NumberTrues == 1);
                Assert.IsTrue(find(isposinf(C)) == A.S.GetSeqIndex(100, 150, 1));
            }
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {
                Assert.IsTrue(isposinf(C).Storage.IsNumberTruesCached);
                Assert.IsTrue(isposinf(C).Storage.NumberTrues == 1);
                Assert.IsTrue(find(isposinf(C)) == A.S.GetSeqIndex(100, 150, 1));
            }

        }
        [TestMethod]
        public void MathInternal_isNegInf_simple() {

            Array<float> A = counter<float>(1.0f, 1.0f, 400, 300, 3);

            Array<fcomplex> C = ccomplex(A, A);
            C[100, 150, 1] = new fcomplex(1, float.NegativeInfinity);

            using (Settings.Ensure(() => Settings.MaxNumberThreads, 1u)) {
                Assert.IsTrue(isneginf(C).Storage.IsNumberTruesCached);
                Assert.IsTrue(isneginf(C).Storage.NumberTrues == 1);
                Assert.IsTrue(find(isneginf(C)) == A.S.GetSeqIndex(100, 150, 1));
            }
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 2u)) {
                Assert.IsTrue(isneginf(C).Storage.IsNumberTruesCached);
                Assert.IsTrue(isneginf(C).Storage.NumberTrues == 1);
                Assert.IsTrue(find(isneginf(C)) == A.S.GetSeqIndex(100, 150, 1));
            }
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {
                Assert.IsTrue(isneginf(C).Storage.IsNumberTruesCached);
                Assert.IsTrue(isneginf(C).Storage.NumberTrues == 1);
                Assert.IsTrue(find(isneginf(C)) == A.S.GetSeqIndex(100, 150, 1));
            }

        }
        [TestMethod]
        public void MathInternal_isNaN_strided() {

            Array<float> A = counter<float>(1.0f, 1.0f, 400, 300, 6)[ellipsis, r(0,2,end)];

            Array<fcomplex> C = ccomplex(A, A);
            C[100, 150, 1] = new fcomplex(1, float.NaN);

            using (Settings.Ensure(() => Settings.MaxNumberThreads, 1u)) {
                Assert.IsTrue(isnan(C).Storage.IsNumberTruesCached);
                Assert.IsTrue(isnan(C).Storage.NumberTrues == 1);
                Assert.IsTrue(find(isnan(C)) == 180100);
            }
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 2u)) {
                Assert.IsTrue(isnan(C).Storage.IsNumberTruesCached);
                Assert.IsTrue(isnan(C).Storage.NumberTrues == 1);
                Assert.IsTrue(find(isnan(C)) == 180100);
            }
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {
                Assert.IsTrue(isnan(C).Storage.IsNumberTruesCached);
                Assert.IsTrue(isnan(C).Storage.NumberTrues == 1);
                Assert.IsTrue(find(isnan(C)) == 180100);
            }

        }
        [TestMethod]
        public void MathInternal_isPosInf_strided() {

            Array<float> A = counter<float>(1.0f, 1.0f, 400, 300, 3)[ellipsis, r(0, 2, end)];

            Array<fcomplex> C = ccomplex(A, A);
            C[100, 150, 1] = new fcomplex(1, float.PositiveInfinity);

            using (Settings.Ensure(() => Settings.MaxNumberThreads, 1u)) {
                Assert.IsTrue(isposinf(C).Storage.IsNumberTruesCached);
                Assert.IsTrue(isposinf(C).Storage.NumberTrues == 1);
                Assert.IsTrue(find(isposinf(C)) == 180100);
            }
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 2u)) {
                Assert.IsTrue(isposinf(C).Storage.IsNumberTruesCached);
                Assert.IsTrue(isposinf(C).Storage.NumberTrues == 1);
                Assert.IsTrue(find(isposinf(C)) == 180100);
            }
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {
                Assert.IsTrue(isposinf(C).Storage.IsNumberTruesCached);
                Assert.IsTrue(isposinf(C).Storage.NumberTrues == 1);
                Assert.IsTrue(find(isposinf(C)) == 180100);
            }

        }
        [TestMethod]
        public void MathInternal_isNegInf_strided() {

            Array<float> A = counter<float>(1.0f, 1.0f, 400, 300, 3)[ellipsis, r(0, 2, end)];

            Array<fcomplex> C = ccomplex(A, A);
            C[100, 150, 1] = new fcomplex(1, float.NegativeInfinity);

            using (Settings.Ensure(() => Settings.MaxNumberThreads, 1u)) {
                Assert.IsTrue(isneginf(C).Storage.IsNumberTruesCached);
                Assert.IsTrue(isneginf(C).Storage.NumberTrues == 1);
                Assert.IsTrue(find(isneginf(C)) == 180100);
            }
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 2u)) {
                Assert.IsTrue(isneginf(C).Storage.IsNumberTruesCached);
                Assert.IsTrue(isneginf(C).Storage.NumberTrues == 1);
                Assert.IsTrue(find(isneginf(C)) == 180100);
            }
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {
                Assert.IsTrue(isneginf(C).Storage.IsNumberTruesCached);
                Assert.IsTrue(isneginf(C).Storage.NumberTrues == 1);
                Assert.IsTrue(find(isneginf(C)) == 180100);
            }

        }
    }
}
