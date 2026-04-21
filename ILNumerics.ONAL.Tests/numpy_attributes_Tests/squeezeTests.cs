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

namespace ILNumerics.Core.UnitTests.numpy_attributes_Tests {

    [TestClass]
    public class squeezeTests {
        [TestMethod]
        public unsafe void numpy_squeeze_simple() {

            Array<double> A = counter<double>(1, 1, 1, 5, 1, 4, 3);
            A.squeeze(); 

            Assert.IsTrue(A.Equals(counter<double>(1, 1, 5, 4, 3)));
        }

        [TestMethod]
        public void numpy_squeeze_emptyA() {
            Array<int> A = empty<int>(1, 0, 3);
            A.squeeze(0);
            Assert.IsTrue(A.S.NumberOfDimensions == 2); 
            Assert.IsTrue(all(A.shape == vector<long>(0,3)));
            Assert.IsTrue(A.IsEmpty); 
        }
        [TestMethod]
        public void numpy_squeeze_emptyAxes() {
            Array<int> A = ones<int>(1, 2, 3);
            Array<long> ax = empty<long>(0,1); 
            A.squeeze(ax); // empty axes -> all singletons
            Assert.IsTrue(A.S.NumberOfDimensions == 2);
            Assert.IsTrue(all(A.shape == vector<long>(2, 3)));
            Assert.IsTrue(A.S.NumberOfElements == 6);
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void numpy_squeeze_axisOOR1Fail() {
            Array<int> A = ones<int>(1, 2, 3);
            Array<long> ax = -1;
            A.squeeze(ax); // non-singleton -> fail
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void numpy_squeeze_axisOOR2Fail() {
            Array<int> A = ones<int>(1, 2, 3);
            Array<long> ax = 3;
            A.squeeze(ax); // non-singleton -> fail
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void numpy_squeeze_axisNonSingletonFail() {
            Array<int> A = ones<int>(1, 2, 3);
            Array<long> ax = 1;
            A.squeeze(ax); // non-singleton -> fail
        }

        [TestMethod]
        public void numpy_squeeze_Shared() {
            using (Scope.Enter(ArrayStyles.numpy)) {

                Array<long> A = ones<long>(3, 1);

                A.squeeze();

                Assert.IsTrue(A.shape.Equals(vector<long>(3)));
                Array<long> R = ones<long>(3, 1);
                Assert.IsTrue(R.shape.Equals(vector<long>(3, 1)));
            }
        }
        [TestMethod]
        public void numpy_squeeze_NPScalar() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = 2;
                Assert.IsTrue(A.S.NumberOfDimensions == 0);
                Assert.IsTrue(A.S.NumberOfElements == 1);
                Array<long> ax = 0;
                A.squeeze(ax);
                Assert.IsTrue(A.S.NumberOfDimensions == 0);
                Assert.IsTrue(A.S.NumberOfElements == 1);
                Assert.IsTrue(A == 2);

                A.squeeze();
                Assert.IsTrue(A.S.NumberOfDimensions == 0);
                Assert.IsTrue(A.S.NumberOfElements == 1);
                Assert.IsTrue(A == 2);

            }
        }
        [TestMethod]
        public void numpy_squeeze_axesNPScalar() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = ones<double>(1, 3, 1, 2, 1);
                Assert.IsTrue(A.S.NumberOfDimensions == 5);
                Assert.IsTrue(A.S.NumberOfElements == 6);
                Array<long> ax = 0;
                A.squeeze(ax);
                Assert.IsTrue(all(A.shape == vector<long>(3, 1, 2, 1)));
                Assert.IsTrue(A.S.NumberOfDimensions == 4);
                Assert.IsTrue(A.S.NumberOfElements == 6);

                ax = 1;
                A.squeeze(ax);
                Assert.IsTrue(all(A.shape == vector<long>(3, 2, 1)));
                Assert.IsTrue(A.S.NumberOfDimensions == 3);
                Assert.IsTrue(A.S.NumberOfElements == 6);

                ax = 2;
                A.squeeze(ax);
                Assert.IsTrue(all(A.shape == vector<long>(3, 2)));
                Assert.IsTrue(A.S.NumberOfDimensions == 2);
                Assert.IsTrue(A.S.NumberOfElements == 6);

            }

        }
        [TestMethod]
        public void numpy_squeeze_allSingletonsRemove2NPScalar() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = ones<double>(1, 1, 1, 1, 1);
                Assert.IsTrue(A.IsScalar);
                Assert.IsTrue(A.S.NumberOfDimensions == 5);
                Assert.IsTrue(A.S.NumberOfElements == 1);
                A.squeeze();
                Assert.IsTrue(A.IsScalar);
                Assert.IsTrue(A.S.NumberOfDimensions == 0);
                Assert.IsTrue(A.S.NumberOfElements == 1);

            }

        }
        [TestMethod]
        public void numpy_squeeze_allSingletonsRemove2MLScalar() {
            Array<double> A = ones<double>(1, 1, 1, 1, 1);
            Assert.IsTrue(A.IsScalar);
            Assert.IsTrue(A.S.NumberOfDimensions == 5);
            Assert.IsTrue(A.S.NumberOfElements == 1);
            A.squeeze();
            Assert.IsTrue(A.IsScalar);
            Assert.IsTrue(A.S.NumberOfDimensions == 2);
            Assert.IsTrue(A.S.NumberOfElements == 1);

        }
    }
}
