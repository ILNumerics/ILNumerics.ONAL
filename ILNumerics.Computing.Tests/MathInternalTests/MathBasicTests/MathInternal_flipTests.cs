using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;
using static ILNumerics.Globals; 

namespace ILNumerics.Core.UnitTests.MathInternalTests {

    [TestClass]
    public class MathInternal_flipTests {

        [TestMethod]
        public void mathInternal_flip_simpleTest() {

            Array<double> A = counter<double>(1.0, 1.0, 5);
            Array<double> B = flipud(A);
            Assert.IsTrue(B.Equals(counter<double>(5.0, -1.0, 5)));

            Assert.IsTrue(flipud(flipud(A)).Equals(A));
            Assert.IsTrue(flipud(flipud(empty<float>(0,1))).Equals(empty<float>(0, 1)));
            Assert.IsTrue(flipud(flipud(empty<float>(1, dim1: 0))).Equals(empty<float>(1, dim1: 0)));
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                Assert.IsTrue(fliplr(A).Equals(A));
                Assert.IsTrue(fliplr(fliplr(A)).Equals(A));

                A = 1;
                Assert.IsTrue(fliplr(A).Equals(A));
                Assert.IsTrue(fliplr(fliplr(A)).Equals(A));
                Assert.IsTrue(flipud(flipud(A)).Equals(A));

            }
        }

        [TestMethod]
        public void mathInternal_flip_ext1() {

            Array<double> A = counter<double>(1.0, 1.0, 50, 100, 2, 3);
            Array<double> B = flipud(A);
            Assert.IsTrue(B.Equals(A[counter<double>(49.0, -1.0, 50), ellipsis]));
            Assert.IsTrue(flipud(flipud(A)).Equals(A));

            Assert.IsTrue(fliplr(fliplr(A)).Equals(A));

            for (int a = 0; a < 10; a++) {
                A.a = counter<double>(1.0, 1.0, 50, a, 3);
                for (int i = 0; i < A.S.NumberOfDimensions; i++) {
                    Assert.IsTrue(flip(flip(A, i), i).Equals(A)); // tests both: inplace and OOplace
                }
            }
        }

        [TestMethod]
        public void mathInternal_flip_bool1() {

            Array<double> A = counter<double>(1.0, 1.0, 50, 100, 2, 3);
            Logical B = flipud(A % 3 == 1);
            Assert.IsTrue(B.Equals(A[counter<double>(49.0, -1.0, 50), ellipsis] % 3 == 1));
            Assert.IsTrue(flipud(flipud(B)).Equals(B));

            Assert.IsTrue(fliplr(fliplr(B)).Equals(B));

            for (int a = 0; a < 10; a++) {
                B.a = counter<double>(1.0, 1.0, 50, a, 3) % 3 == 1;
                for (int i = 0; i < B.S.NumberOfDimensions; i++) {
                    Assert.IsTrue(flip(flip(B, i), i).Equals(B)); // tests both: inplace and OOplace
                    var dummy = flip(flip(B, i), i);
                    Assert.IsTrue(dummy.Storage.IsNumberTruesCached);
                    dummy.Release();
                    Assert.IsTrue(flip(flip(B, i), i).NumberTrues == B.NumberTrues);
                }
            }
        }

        [TestMethod]
        public void MathInternal_flip_lg3() {
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {

                Array<short> A = counter<short>(1, 1, 50, 10, 30, 20, 5);
                for (int i = 0; i < A.S.NumberOfDimensions; i++) {
                    using (Scope.Enter()) {
                        Array<short> T = flip(A, i);
                        Assert.IsFalse(T.Equals(A));
                        Assert.IsTrue(flip(T, i).Equals(A)); // tests out of-place only
                    }
                }

                for (int i = 0; i < A.S.NumberOfDimensions; i++) {
                    using (Scope.Enter()) {
                        Assert.IsTrue(flip(flip(A, i), i).Equals(A)); // tests both: inplace and OOplace
                    }
                }


            }
        }
    }
}
