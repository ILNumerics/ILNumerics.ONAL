using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;
using ILNumerics.Core.Functions.Builtin;

namespace ILNumerics.Core.UnitTests.numpy_attributes_Tests {

    [TestClass]
    public class sumTests {

        [TestMethod]
        public void numpy_sum_simple() {

            Array<double> A = counter<double>(1, 1, 5, 4, 3);
            Array<double> B = A.sum();

            Assert.IsTrue(B.IsScalar);
            Assert.IsTrue(B == MathInternal.sumall(A));

        }

        [TestMethod]
        public void numpy_sum_scalarKeepdims() {

            Array<double> A = array<double>(-1, 1, 1, 1, dim3: 1);
            Assert.IsTrue(A.IsScalar);

            Array<double> B = A.sum(true);

            Assert.IsTrue(B == -1);
            Assert.IsTrue(B.Size.NumberOfDimensions == 4);
            Assert.IsTrue(B.IsScalar);

        }
        [TestMethod]
        public void numpy_sum_1DscalarKeepdims() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = array<double>(-1, dim0: 1);
                Assert.IsTrue(A.IsScalar);

                Array<double> B = A.sum(true);

                Assert.IsTrue(B == -1);
                Assert.IsTrue(B.Size.NumberOfDimensions == 1);
                Assert.IsTrue(B.IsScalar);
            }
        }

        [TestMethod]
        public void numpy_sum_1Dim_vector() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = vector<double>(-1, 0, 1, 2);
                Array<double> B = A.sum(0);

                Assert.IsTrue(B == 2);
                Assert.IsTrue(B.Size.NumberOfDimensions == 0);
                Assert.IsTrue(B.IsScalar);

                B = A.sum(-1);

                Assert.IsTrue(B == 2);
                Assert.IsTrue(B.Size.NumberOfDimensions == 0);
                Assert.IsTrue(B.IsScalar);

                B = A.sum(-1, true);

                Assert.IsTrue(B == 2);
                Assert.IsTrue(B.Size.NumberOfDimensions == 1);
                Assert.IsTrue(B.IsScalar);
            }
        }
        [TestMethod]
        public void numpy_sum_1Dim_vectorVirtDim() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = vector<double>(-1, 0, 1, 2);
                Array<double> B = A.sum(1);
                Assert.IsTrue(B.IsVector);
                Assert.IsTrue(B.Equals(vector<double>(-1, 0, 1, 2)));
            }
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void numpy_sum_1Dim_vectorOORNeg_Fai() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = vector<double>(-1, 0, 1, 2);
                Array<double> B = A.sum(-2);
            }
        }
        [TestMethod]
        public void numpy_sum_1Dim_NPScalar() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = 2;
                Array<double> B = A.sum();
                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B.S.NumberOfDimensions == 0);
                Assert.IsTrue(B == 2);

                // sum over virtu. dimension
                B = A.sum(3);
                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B.S.NumberOfDimensions == 0);
                Assert.IsTrue(B == 2);

                B = A.sum(0);
                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B.S.NumberOfDimensions == 0);
                Assert.IsTrue(B == 2);

                B = A.sum(0, true);
                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B.S.NumberOfDimensions == 0);
                Assert.IsTrue(B == 2);

            }
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void numpy_sum_1D_NPScalarNegOOR_Fails() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = 2;
                Array<double> B = A.sum(-1);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void numpy_sum_axes_TooManyIndices_Fails() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = vector<double>(1, 2, 3, 4); // 1D
                Array<double> B = A.sum(vector<int>(0, 1, 2, 3, 4));
            }
        }
        [TestMethod]
        public void numpy_sum_axesVec_Simple() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = vector<double>(1, 2, 3, 4); // 1D
                Array<double> B = A.sum(vector<int>(0));

                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B == sumall(A));
                Assert.IsTrue(B.S.NumberOfDimensions == 0);
            }
        }
        [TestMethod]
        public void numpy_sum_axesMat_Simple() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = ones<double>(3, 4); // 2D
                Array<double> B = A.sum(vector<int>(0, 1));

                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B == sumall(A));
                Assert.IsTrue(B.S.NumberOfDimensions == 0);
            }
        }

        [TestMethod]
        public void numpy_sum_axesMat_KeepDims() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = ones<double>(3, 4); // 2D
                Array<double> B = A.sum(vector<int>(0, 1), true);

                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B == sumall(A));
                Assert.IsTrue(B.S.NumberOfDimensions == 2);
            }
        }

        [TestMethod]
        public void numpy_sum_axesMat_NegDims() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = ones<double>(3, 4); // 2D
                Array<double> B = A.sum(vector<int>(-2, -1), true);

                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B == sumall(A));
                Assert.IsTrue(B.S.NumberOfDimensions == 2);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void numpy_sum_axesMat_MultipleNegDimsFail() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = ones<double>(3, 4); // 2D
                Array<double> B = A.sum(vector<int>(-2, -2), true);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void numpy_sum_axesMat_MultipleDimsFail() {

            Array<double> A = ones<double>(3, 4, 5); // 2D
            Array<double> B = A.sum(vector<int>(1, 2, 1), true);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void numpy_sum_axesMat_NegIndexOORFail() {

            Array<double> A = ones<double>(3, 4, 5); // 2D
            Array<double> B = A.sum(vector<int>(1, -4), true);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void numpy_sum_axesMat_MultipleNegIndexOORFail() {

            Array<double> A = ones<double>(3, 4, 5); // 2D
            Array<double> B = A.sum(vector<int>(1, -2, 0), false);
        }

        [TestMethod]
        public void numpy_sum_axesMat_IndicesMixed() {

            Array<double> A = ones<double>(3, 4, 5); // 2D
            Array<double> B = A.sum(vector<int>(2, -2, 0));

            Assert.IsTrue(B.IsScalar);
            Assert.IsTrue(B == 60);
            Assert.IsTrue(B.S.NumberOfDimensions == 2);

            Assert.IsTrue(A.sum(vector<int>(0, 1), true).shape.Equals(vector<long>(1,1,5))); 
            Assert.IsTrue(A.sum(vector<int>(0, 2), true).shape.Equals(vector<long>(1,4,1)));
            Assert.IsTrue(A.sum(vector<int>(1, 0), true).shape.Equals(vector<long>(1,1,5)));
            Assert.IsTrue(A.sum(vector<int>(2, 0), true).shape.Equals(vector<long>(1,4,1)));
            Assert.IsTrue(A.sum(vector<int>(2, 1), true).shape.Equals(vector<long>(3,1,1)));
            Assert.IsTrue(A.sum(vector<int>(2, 1), false).shape.Equals(vector<long>(3,1)));
            Assert.IsTrue(A.sum(vector<int>(3, 1), false).shape.Equals(vector<long>(3,5)));

        }

        [TestMethod]
        public void numpy_sum_axes_MemManagm1() {
            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {
                var A = ones<double>(3, 4, 5);
                var ax = vector<int>(2, -2, 0);
                var B_ret = A.sum(ax);

                Array<double> B = B_ret;

                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B == 60);
                Assert.IsTrue(B.S.NumberOfDimensions == 2);
            }

            using (Scope.Enter(ArrayStyles.numpy)) {
                var A = ones<double>(3, 4, 5);
                var ax = vector<int>(2, -2, 0);
                var B_ret = A.sum(ax);

                Array<double> B = B_ret;

                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B == 60);
                Assert.IsTrue(B.S.NumberOfDimensions == 0);
            }
        }

        [TestMethod]
        public void numpy_sum_axes_MemManagm2() {

            var A = ones<double>(3, 4, 5);
            Array<double> B = A.sum();

            Assert.IsTrue(B.IsScalar);
            Assert.IsTrue(B == 60);
            Assert.IsTrue(B.S.NumberOfDimensions == 2);


        }
        [TestMethod]
        public void numpy_sum_axes_MemManagm2keepdims() {

            var A = ones<double>(3, 4, 5);
            Array<double> B = A.sum(true);

            Assert.IsTrue(B.IsScalar);
            Assert.IsTrue(B == 60);
            Assert.IsTrue(B.S.NumberOfDimensions == 3);


        }
        [TestMethod]
        public void numpy_sum_axes_MemManagm3keepdims() {

            var A = ones<double>(3, 4, 5);
            Array<double> B = A.sum(1, true);

            Assert.IsTrue(B.Equals(ones<double>(3, 1, 5) * 4));
            Assert.IsTrue(B.S.NumberOfDimensions == 3);


        }
        [TestMethod]
        public void numpy_sum_axes_MemManagmFailsNegOOR() {

            var A = ones<double>(3, 4, 5);
            try {
                Array<double> B = A.sum(-4, true);
            } catch (ArgumentException) { }


        }
        [TestMethod]
        public void numpy_sum_emptyA() {
            Array<float> A = empty<float>(1, 0, dim2: 3);
            Array<float> B = A.sum();

            Assert.IsTrue(B.IsScalar);
            Assert.IsTrue(B == 0); 
            Assert.IsTrue(B.ndim == 2);


            B = A.sum(true);

            Assert.IsTrue(B.IsScalar);
            Assert.IsTrue(B == 0);
            Assert.IsTrue(B.ndim == 3);
        }
    }
}
