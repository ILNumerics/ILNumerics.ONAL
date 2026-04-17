using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;
using static ILNumerics.Globals;

namespace ILNumerics.Core.UnitTests.MathInternalTests {
    [TestClass]
    public class MathInternal_roundTests {
        [TestMethod]
        public void MathInternal_round_SimpleTest() {
            Array<double> A = counter<double>(1, 0.25, 5, 3);
            Array<double> B = round(A);
            Array<double> Res = vector<double>(1, 2, 3, 4).repeat(vector<int>(2, 5, 3, 5)).Reshape(5, 3);
            Assert.IsTrue(Res.Equals(B));
            Assert.IsTrue(B.Equals(round(A, 0)));
        }
        [TestMethod]
        public void MathInternal_round_Digits() {
            Array<double> A = counter<double>(1, 0.25, 5, 3);
            Array<double> B = round(A, 1); // contiguous OOP

            Array<double> Res = vector<double>(1, 2.2, 3.5, 1.2, 2.5, 3.8, 1.5, 2.8, 4, 1.8, 3, 4.2, 2, 3.2, 4.5)
                                .Reshape(5, 3, StorageOrders.RowMajor);
            Assert.IsTrue(Res.Equals(B));
            var tmp = counter<double>(1, 0.25, 5, 3);
            var tmp2 = round(tmp, 1);
            Assert.IsTrue(Res.Equals(tmp2)); // contiguous, inplace
            //Assert.IsTrue(ReferenceEquals(tmp.Storage, tmp2.Storage));

            A = counter<double>(1, 0.25 / 2, 10, 3)[r(0, 2, end), full];
            Assert.IsTrue(A.S.IsContinuous == false);
            Assert.IsTrue(Res.Equals(round(A, 1))); // strided, 32 

        }
        [TestMethod]
        public void MathInternal_round_DigitsFComplex() {
            Array<fcomplex> A = ccomplex(counter<float>(1, 0.25f, 5, 3),-counter<float>(1, 0.25f, 5, 3));
            Array<fcomplex> B = round(A, 1); // contiguous OOP

            Array<fcomplex> Res = ccomplex(
                                vector<float>(1, 2.2f, 3.5f, 1.2f, 2.5f, 3.8f, 1.5f, 2.8f, 4, 1.8f, 3, 4.2f, 2, 3.2f, 4.5f).Reshape(5, 3, StorageOrders.RowMajor),
                                -vector<float>(1, 2.2f, 3.5f, 1.2f, 2.5f, 3.8f, 1.5f, 2.8f, 4, 1.8f, 3, 4.2f, 2, 3.2f, 4.5f).Reshape(5, 3, StorageOrders.RowMajor));
            Assert.IsTrue(Res.Equals(B));
            var tmp = ccomplex(vector<float>(1, 2.2f, 3.5f, 1.2f, 2.5f, 3.8f, 1.5f, 2.8f, 4, 1.8f, 3, 4.2f, 2, 3.2f, 4.5f).Reshape(5, 3, StorageOrders.RowMajor),
                                -vector<float>(1, 2.2f, 3.5f, 1.2f, 2.5f, 3.8f, 1.5f, 2.8f, 4, 1.8f, 3, 4.2f, 2, 3.2f, 4.5f).Reshape(5, 3, StorageOrders.RowMajor));
            var tmp2 = round(tmp, 1);
            Assert.IsTrue(Res.Equals(tmp2)); // contiguous, inplace
            //Assert.IsTrue(ReferenceEquals(tmp.Storage, tmp2.Storage));

            A = ccomplex(counter<float>(1, 0.25f / 2, 10, 3),-counter<float>(1, 0.25f / 2, 10, 3))[r(0, 2, end), full];
            Assert.IsTrue(A.S.IsContinuous == false);
            Assert.IsTrue(Res.Equals(round(A, 1))); // strided, 32 

        }

        [TestMethod]
        public void MathInternal_round_empty() {
            Array<double> A = counter<double>(1, 0.25, 5, 0, 3);
            Array<double> B = round(A, 1); // contiguous OOP

            Array<double> Res = empty<double>(5, 0, 3);
            Assert.IsTrue(Res.Equals(B));

        }
        [TestMethod]
        public void MathInternal_round_npscalar() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                Array<double> A = 1.25;
                Array<double> B = round(A, 1); // contiguous OOP

                Array<double> Res = 1.2;
                Assert.IsTrue(Res.Equals(B));
            }

        }
    }
}
