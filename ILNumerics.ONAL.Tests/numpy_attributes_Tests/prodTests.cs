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
using ILNumerics.Core.Functions.Builtin;

namespace ILNumerics.Core.UnitTests.numpy_attributes_Tests {

    [TestClass]
    public class prodTests {

        [TestMethod]
        public void numpy_prod_simple() {
            // prod produces huge numbers. floating point rounding makes us having to ensure that we compute the result
            // on the very same path. Every time.
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 1u)) {
                Array<double> A = counter<double>(1, 1, 5, 4, 3);
                Array<double> B = A.prod();

                Assert.IsTrue(B.IsScalar);
                double b = B.GetValue(0);
                double c = (double)MathInternal.prodall(A);
                Assert.IsTrue(B == MathInternal.prodall(A), $"B:{b}, prodall(A):{c}, Diff: {Math.Abs(b - c)} - Equal: {b == c} - Threads: {Settings.MaxNumberThreads}");
            }
        }

        [TestMethod]
        public void numpy_prod_scalarKeepdims() {

            Array<double> A = array<double>(-1, 1, 1, 1, dim3: 1);
            Assert.IsTrue(A.IsScalar);

            Array<double> B = A.prod(true);

            Assert.IsTrue(B == -1);
            Assert.IsTrue(B.Size.NumberOfDimensions == 4);
            Assert.IsTrue(B.IsScalar);

        }
        [TestMethod]
        public void numpy_prod_1DscalarKeepdims() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = array<double>(-1, dim0: 1);
                Assert.IsTrue(A.IsScalar);

                Array<double> B = A.prod(true);

                Assert.IsTrue(B == -1);
                Assert.IsTrue(B.Size.NumberOfDimensions == 1);
                Assert.IsTrue(B.IsScalar);
            }
        }

        [TestMethod]
        public void numpy_prod_1Dim_vector() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = vector<double>(-1, 0, 1, 2);
                Array<double> B = A.prod(0);

                Assert.IsTrue(B == 0);
                Assert.IsTrue(B.Size.NumberOfDimensions == 0);
                Assert.IsTrue(B.IsScalar);

                B = A.prod(-1);

                Assert.IsTrue(B == 0);
                Assert.IsTrue(B.Size.NumberOfDimensions == 0);
                Assert.IsTrue(B.IsScalar);

                B = A.prod(-1, true);

                Assert.IsTrue(B == 0);
                Assert.IsTrue(B.Size.NumberOfDimensions == 1);
                Assert.IsTrue(B.IsScalar);
            }
        }
        [TestMethod]
        public void numpy_prod_1Dim_vector05() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = vector<double>(-1, .5, 1, 2);
                Array<double> B = A.prod(0);

                Assert.IsTrue(B == -1);
                Assert.IsTrue(B.Size.NumberOfDimensions == 0);
                Assert.IsTrue(B.IsScalar);

                B = A.prod(-1);

                Assert.IsTrue(B == -1);
                Assert.IsTrue(B.Size.NumberOfDimensions == 0);
                Assert.IsTrue(B.IsScalar);

                B = A.prod(-1, true);

                Assert.IsTrue(B == -1);
                Assert.IsTrue(B.Size.NumberOfDimensions == 1);
                Assert.IsTrue(B.IsScalar);
            }
        }
        [TestMethod]
        public void numpy_prod_1Dim_vectorVirtDim() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = vector<double>(-1, 0, 1, 2);
                Array<double> B = A.prod(1);
                Assert.IsTrue(B.IsVector);
                Assert.IsTrue(B.Equals(vector<double>(-1, 0, 1, 2)));
            }
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void numpy_prod_1Dim_vectorOORNeg_Fai() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = vector<double>(-1, 0, 1, 2);
                Array<double> B = A.prod(-2);
            }
        }
        [TestMethod]
        public void numpy_prod_1Dim_NPScalar() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = 2;
                Array<double> B = A.prod();
                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B.S.NumberOfDimensions == 0);
                Assert.IsTrue(B == 2);

                // prod over virtu. dimension
                B = A.prod(3);
                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B.S.NumberOfDimensions == 0);
                Assert.IsTrue(B == 2);

                B = A.prod(0);
                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B.S.NumberOfDimensions == 0);
                Assert.IsTrue(B == 2);

                B = A.prod(0, true);
                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B.S.NumberOfDimensions == 0);
                Assert.IsTrue(B == 2);

            }
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void numpy_prod_1D_NPScalarNegOOR_Fails() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = 2;
                Array<double> B = A.prod(-1);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void numpy_prod_axes_TooManyIndices_Fails() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = vector<double>(1, 2, 3, 4); // 1D
                Array<double> B = A.prod(vector<int>(0, 1, 2, 3, 4));
            }
        }
        [TestMethod]
        public void numpy_prod_axesVec_Simple() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = vector<double>(1, 2, 3, 4); // 1D
                Array<double> B = A.prod(vector<int>(0));

                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B == prodall(A));
                Assert.IsTrue(B == 24);
                Assert.IsTrue(B.S.NumberOfDimensions == 0);
            }
        }
        [TestMethod]
        public void numpy_prod_axesMat_Simple() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = ones<double>(3, 4); // 2D
                Array<double> B = A.prod(vector<int>(0, 1));

                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B == 1);
                Assert.IsTrue(B.S.NumberOfDimensions == 0);
            }
        }

        [TestMethod]
        public void numpy_prod_axesMat_KeepDims() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = ones<double>(3, 4); // 2D
                Array<double> B = A.prod(vector<int>(0, 1), true);

                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B == 1);
                Assert.IsTrue(B.S.NumberOfDimensions == 2);
            }
        }

        [TestMethod]
        public void numpy_prod_axesMat_NegDims() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = ones<double>(3, 4); // 2D
                Array<double> B = A.prod(vector<int>(-2, -1), true);

                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B == 1);
                Assert.IsTrue(B.S.NumberOfDimensions == 2);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void numpy_prod_axesMat_MultipleNegDimsFail() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = ones<double>(3, 4); // 2D
                Array<double> B = A.prod(vector<int>(-2, -2), true);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void numpy_prod_axesMat_MultipleDimsFail() {

            Array<double> A = ones<double>(3, 4, 5); // 2D
            Array<double> B = A.prod(vector<int>(1, 2, 1), true);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void numpy_prod_axesMat_NegIndexOORFail() {

            Array<double> A = ones<double>(3, 4, 5); // 2D
            Array<double> B = A.prod(vector<int>(1, -4), true);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void numpy_prod_axesMat_MultipleNegIndexOORFail() {

            Array<double> A = ones<double>(3, 4, 5); // 2D
            Array<double> B = A.prod(vector<int>(1, -2, 0), false);
        }

        [TestMethod]
        public void numpy_prod_axesMat_IndicesMixed() {

            Array<double> A = ones<double>(3, 4, 5); // 2D
            Array<double> B = A.prod(vector<int>(2, -2, 0));

            Assert.IsTrue(B.IsScalar);
            Assert.IsTrue(B == 1);
            Assert.IsTrue(B.S.NumberOfDimensions == 2);

            Assert.IsTrue(A.prod(vector<int>(0, 1), true).shape.Equals(vector<long>(1,1,5))); 
            Assert.IsTrue(A.prod(vector<int>(0, 2), true).shape.Equals(vector<long>(1,4,1)));
            Assert.IsTrue(A.prod(vector<int>(1, 0), true).shape.Equals(vector<long>(1,1,5)));
            Assert.IsTrue(A.prod(vector<int>(2, 0), true).shape.Equals(vector<long>(1,4,1)));
            Assert.IsTrue(A.prod(vector<int>(2, 1), true).shape.Equals(vector<long>(3,1,1)));
            Assert.IsTrue(A.prod(vector<int>(2, 1), false).shape.Equals(vector<long>(3,1)));
            Assert.IsTrue(A.prod(vector<int>(3, 1), false).shape.Equals(vector<long>(3,5)));

        }

        [TestMethod]
        public void numpy_prod_axes_MemManagm1() {

            var A = ones<double>(3, 4, 5);
            var ax = vector<int>(2, -2, 0);
            var B_ret = A.prod(ax);

            // must immediately check for return array reference count! Assigning it to local array B
            // (may) reuse A!
            // Update: even more, prod() uses disposed A's storage for ret array returned (It requires conversion from local "ret" array inside prod()). 

            Array<double> B = B_ret; 
            Assert.IsTrue(B.IsScalar);
            Assert.IsTrue(B == 1);
            Assert.IsTrue(B.S.NumberOfDimensions == 2);


        }

        [TestMethod]
        public void numpy_prod_axes_MemManagm2() {

            var A = ones<double>(3, 4, 5);
            var B_ret = A.prod();

            Array<double> B = B_ret; 
            Assert.IsTrue(B.IsScalar);
            Assert.IsTrue(B == 1);
            Assert.IsTrue(B.S.NumberOfDimensions == 2);


        }
        [TestMethod]
        public void numpy_prod_axes_MemManagm2keepdims() {

            var A = ones<double>(3, 4, 5);
            var B_ret = A.prod(true);

            Array<double> B = B_ret; 

            Assert.IsTrue(B.IsScalar);
            Assert.IsTrue(B == 1);
            Assert.IsTrue(B.S.NumberOfDimensions == 3);


        }
        [TestMethod]
        public void numpy_prod_axes_MemManagm3keepdims() {

            var A = ones<double>(3, 4, 5);
            var B_ret = A.prod(1, true);

            Array<double> B = B_ret; 
            Assert.IsTrue(B.Equals(ones<double>(3, 1, 5) * 1));
            Assert.IsTrue(B.S.NumberOfDimensions == 3);


        }
        [TestMethod]
        public void numpy_prod_axes_MemManagmFailsNegOOR() {

            var A = ones<double>(3, 4, 5);
            try {
                Array<double> B = A.prod(-4, true);
            } catch (ArgumentException) { }


        }
        [TestMethod]
        public void numpy_prod_emptyA() {
            Array<float> A = empty<float>(1, 0, dim2: 3);
            Array<float> B = A.prod();

            Assert.IsTrue(B.IsScalar);
            Assert.IsTrue(B == 0);
            Assert.IsTrue(B.ndim == 2);


            B = A.prod(true);

            Assert.IsTrue(B.IsScalar);
            Assert.IsTrue(B == 0);
            Assert.IsTrue(B.ndim == 3);
        }
    }
}
