using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;

namespace ILNumerics.Core.UnitTests.numpy_attributes_Tests {

    [TestClass]
    public class anyTests {

        [TestMethod]
        public void numpy_any_simple() {

            Array<double> A = counter<double>(1, 1, 5, 4, 3);
            Logical B = A.any();

            Assert.IsTrue(B.IsScalar);
            Assert.IsTrue(B == true);

        }

        [TestMethod]
        public void numpy_any_scalarKeepdims() {

            Array<double> A = array<double>(-1, 1, 1, 1, dim3: 1);
            Assert.IsTrue(A.IsScalar);

            Logical B = A.any(true);

            Assert.IsTrue(B == true);
            Assert.IsTrue(B.Size.NumberOfDimensions == 4);
            Assert.IsTrue(B.IsScalar);

        }
        [TestMethod]
        public void numpy_any_1DscalarKeepdims() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = array<double>(-1, dim0: 1);
                Assert.IsTrue(A.IsScalar);

                Logical B = A.any(true);

                Assert.IsTrue(B == true);
                Assert.IsTrue(B.Size.NumberOfDimensions == 1);
                Assert.IsTrue(B.IsScalar);
            }
        }

        [TestMethod]
        public void numpy_any_1Dim_vector() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = vector<double>(-1, 0, 1, 2);
                Logical B = A.any(0);

                Assert.IsTrue(B == true);
                Assert.IsTrue(B.Size.NumberOfDimensions == 0);
                Assert.IsTrue(B.IsScalar);

                B = A.any(-1);

                Assert.IsTrue(B == true);
                Assert.IsTrue(B.Size.NumberOfDimensions == 0);
                Assert.IsTrue(B.IsScalar);

                B = A.any(-1, true);

                Assert.IsTrue(B == true);
                Assert.IsTrue(B.Size.NumberOfDimensions == 1);
                Assert.IsTrue(B.IsScalar);
            }
        }
        [TestMethod]
        public void numpy_any_1Dim_vector05() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = vector<double>(-1, .5, 1, 2);
                Logical B = A.any(0);

                Assert.IsTrue(B == true);
                Assert.IsTrue(B.Size.NumberOfDimensions == 0);
                Assert.IsTrue(B.IsScalar);

                B = A.any(-1);

                Assert.IsTrue(B == true);
                Assert.IsTrue(B.Size.NumberOfDimensions == 0);
                Assert.IsTrue(B.IsScalar);

                B = A.any(-1, true);

                Assert.IsTrue(B == true);
                Assert.IsTrue(B.Size.NumberOfDimensions == 1);
                Assert.IsTrue(B.IsScalar);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void numpy_any_1Dim_vectorVirtDimFail() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = vector<double>(-1, 0, 1, 2);
                Logical B = A.any(1);
 
            }
        }
        [TestMethod]
        public void numpy_any_1Dim_vectorVirtDim() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = vector<double>(-1, 0, 1, 2).Reshape(4,1);
                Logical B = A.any(1);
                Assert.IsTrue(B.IsVector);
                Logical Res = new bool[] { true, false, true, true };
                Assert.IsTrue(B.Equals(Res));
            }
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void numpy_any_1Dim_vectorOORNeg_Fai() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = vector<double>(-1, 0, 1, 2);
                Logical B = A.any(-2);
            }
        }
        [TestMethod]
        public void numpy_any_1Dim_NPScalar() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = 2;
                Logical B = A.any();
                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B.S.NumberOfDimensions == 0);
                Assert.IsTrue(B == true);

                // any over virtu. dimension
                B = A.any(3);
                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B.S.NumberOfDimensions == 0);
                Assert.IsTrue(B == true);

                B = A.any(0);
                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B.S.NumberOfDimensions == 0);
                Assert.IsTrue(B == true);

                B = A.any(0, true);
                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B.S.NumberOfDimensions == 0);
                Assert.IsTrue(B == true);

            }
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void numpy_any_1D_NPScalarNegOOR_Fails() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = 2;
                Logical B = A.any(-1);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void numpy_any_axes_TooManyIndices_Fails() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = vector<double>(1, 2, 3, 4); // 1D
                Logical B = A.any(vector<int>(0, 1, 2, 3, 4));
            }
        }
        [TestMethod]
        public void numpy_any_axesVec_Simple() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = vector<double>(1, 2, 3, 4); // 1D
                Logical B = A.any(vector<int>(0));

                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B == anyall(A));
                Assert.IsTrue(B == true);
                Assert.IsTrue(B.S.NumberOfDimensions == 0);
            }
        }
        [TestMethod]
        public void numpy_any_axesMat_Simple() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = ones<double>(3, 4); // 2D
                Logical B = A.any(vector<int>(0, 1));

                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B == true);
                Assert.IsTrue(B.S.NumberOfDimensions == 0);
            }
        }

        [TestMethod]
        public void numpy_any_axesMat_KeepDims() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = ones<double>(3, 4); // 2D
                Logical B = A.any(vector<int>(0, 1), true);

                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B == true);
                Assert.IsTrue(B.S.NumberOfDimensions == 2);
            }
        }

        [TestMethod]
        public void numpy_any_axesMat_NegDims() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = ones<double>(3, 4); // 2D
                Logical B = A.any(vector<int>(-2, -1), true);

                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B == true);
                Assert.IsTrue(B.S.NumberOfDimensions == 2);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void numpy_any_axesMat_MultipleNegDimsFail() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = ones<double>(3, 4); // 2D
                Logical B = A.any(vector<int>(-2, -2), true);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void numpy_any_axesMat_MultipleDimsFail() {

            Array<double> A = ones<double>(3, 4, 5); // 2D
            Logical B = A.any(vector<int>(1, 2, 1), true);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void numpy_any_axesMat_NegIndexOORFail() {

            Array<double> A = ones<double>(3, 4, 5); // 2D
            Logical B = A.any(vector<int>(1, -4), true);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void numpy_any_axesMat_MultipleNegIndexOORFail() {

            Array<double> A = ones<double>(3, 4, 5); // 2D
            Logical B = A.any(vector<int>(1, -2, 0), false);
        }

        [TestMethod]
        public void numpy_any_axesMat_IndicesMixed() {

            Array<double> A = ones<double>(3, 4, 5); // 2D
            Logical B = A.any(vector<int>(2, -2, 0));

            Assert.IsTrue(B.IsScalar);
            Assert.IsTrue(B == true);
            Assert.IsTrue(B.S.NumberOfDimensions == 2);

            Assert.IsTrue(A.any(vector<int>(0, 1), true).shape.Equals(vector<long>(1,1,5))); 
            Assert.IsTrue(A.any(vector<int>(0, 2), true).shape.Equals(vector<long>(1,4,1)));
            Assert.IsTrue(A.any(vector<int>(1, 0), true).shape.Equals(vector<long>(1,1,5)));
            Assert.IsTrue(A.any(vector<int>(2, 0), true).shape.Equals(vector<long>(1,4,1)));
            Assert.IsTrue(A.any(vector<int>(2, 1), true).shape.Equals(vector<long>(3,1,1)));
            Assert.IsTrue(A.any(vector<int>(2, 1), false).shape.Equals(vector<long>(3,1)));

        }

        [TestMethod]
        public void numpy_any_axes_MemManagm1() {

            var A = ones<double>(3, 4, 5);
            var ax = vector<int>(2, -2, 0);
            Logical B = A.any(ax);

            Assert.IsTrue(B.IsScalar);
            Assert.IsTrue(B == true);
            Assert.IsTrue(B.S.NumberOfDimensions == 2);


        }

        [TestMethod]
        public void numpy_any_axes_MemManagm2() {

            var A = ones<double>(3, 4, 5);
            Logical B = A.any();

            Assert.IsTrue(B.IsScalar);
            Assert.IsTrue(B == true);
            Assert.IsTrue(B.S.NumberOfDimensions == 2);


        }
        [TestMethod]
        public void numpy_any_axes_MemManagm2keepdims() {

            var A = ones<double>(3, 4, 5);
            Logical B = A.any(true);

            Assert.IsTrue(B.IsScalar);
            Assert.IsTrue(B == true);
            Assert.IsTrue(B.S.NumberOfDimensions == 3);


        }
        [TestMethod]
        public void numpy_any_axes_MemManagm3keepdims() {

            var A = ones<double>(3, 4, 5);
            Logical B = A.any(1, true);
            Logical Res = array<int>(1, vector<long>(3, 1, 5)) != 0; 
            Assert.IsTrue(B.Equals(Res));
            Assert.IsTrue(B.S.NumberOfDimensions == 3);


        }
        [TestMethod]
        public void numpy_any_axes_MemManagmFailsNegOOR() {

            var A = ones<double>(3, 4, 5);
            try {
                Logical B = A.any(-4, true);
            } catch (ArgumentException) { }


        }
        [TestMethod]
        public void numpy_any_emptyA() {
            Array<float> A = empty<float>(1, 0, dim2: 3);
            Logical B = A.any();

            Assert.IsTrue(B.IsScalar);
            Assert.IsTrue(B == false);
            Assert.IsTrue(B.ndim == 2);


            B = A.any(true);

            Assert.IsTrue(B.IsScalar);
            Assert.IsTrue(B == false);
            Assert.IsTrue(B.ndim == 3);
        }
    }
}
