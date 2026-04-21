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
    public class argmaxTests {
        [TestMethod]
        public void numpy_argmax_all_simple() {

            Array<double> A = counter<double>(1, 1, 5, 4, 3);
            Array<long> I = A.nanargmax();
            Assert.IsTrue(I == 59, I.ToString());


            Assert.IsTrue((10 - abs(counter<double>(-11, 1, 5, 4, 3))).nanargmax() == 18);
            Assert.IsTrue((10 - abs(counter<double>(-11, -1, 5, 4, 3))).nanargmax() == 0);
            Assert.IsTrue((10 - abs(counter<double>(-22, 1, 5, 4, 3))).nanargmax() == 25);

        }
        [TestMethod]
        public void numpy_argmax_all_simple_INT() {

            var A = counter<int>(1, 1, 5, 4, 3);
            Array<long> I = A.nanargmax();  // heajhh? NAN on Int32!??? ... -> remove!?
            Assert.IsTrue(I == 59, I.ToString());


            Assert.IsTrue((10 - abs(counter<int>(-11, 1, 5, 4, 3))).nanargmax() == 18);
            Assert.IsTrue((10 - abs(counter<int>(-11, -1, 5, 4, 3))).nanargmax() == 0);
            Assert.IsTrue((10 - abs(counter<int>(-22, 1, 5, 4, 3))).nanargmax() == 25);
        }
        [TestMethod]
        public void numpy_argmax_ax0_simple() {

            Array<double> A = counter<double>(1, 1, 5, 4, 3);
            Array<long> I = A.nanargmax(0);
            Assert.IsTrue(I.Equals(array<long>(4, vector<long>(4, 3))));

            I = A.nanargmax(1);
            Assert.IsTrue(I.Equals(array<long>(3, vector<long>(5, 3))));

            I = A.nanargmax(2);
            Assert.IsTrue(I.Equals(array<long>(2, vector<long>(5, 4))));

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void numpy_argmax_emptyFail() {

            Array<long> A = zeros<long>(0); 
            A = A.argmax();
 
        }

        [TestMethod]
        public void numpy_argmax_axisOOR() {
            Array<long> A = 0;
            Array<long> I = ones<long>(4, 3).argmax(3, values: A);
            Assert.IsTrue(I.Equals(zeros<long>(4, 3)));
            Assert.IsTrue(A.Equals(ones<long>(4, 3)));
        }
        [TestMethod]
        public void numpy_argmax_axisNeg() {
            Array<long> A = 0;
            Array<long> I = counter<long>(1, 1, 5, 4, 3).argmax(-1, values: A);
            Assert.IsTrue(I.Equals(zeros<long>(5, 4) + 2));
            Assert.IsTrue(A.Equals(counter<long>(41, 1, 5, 4)));

            A.a = toint64(rand(5, 4, 3) * 1000);
            Assert.IsTrue(A.argmax(-1).Equals(A.argmax(2)));
            Assert.IsTrue(A.argmax(-2).Equals(A.argmax(1)));
            Assert.IsTrue(A.argmax(-3).Equals(A.argmax(0)));
        }
        [TestMethod]
        public void numpy_argmax_NpScalar() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<long> A = -11;
                Array<long> B = 0; 
                Assert.IsTrue(A.argmax() == 0);
                Assert.IsTrue(A.argmax(values: B) == 0);
                Assert.IsTrue(B == A);
                Assert.IsTrue(A.argmax(0, values: B) == 0);
                Assert.IsTrue(B == A);
                Assert.IsTrue(A.argmax(1, values: B) == 0);
                Assert.IsTrue(B == A);

                Assert.IsTrue(A.argmax().S.NumberOfDimensions == 0);
                Assert.IsTrue(A.argmax(values: B).S.NumberOfDimensions == 0);
                Assert.IsTrue(B.S.NumberOfDimensions == 0);
                Assert.IsTrue(A.argmax(0, values: B).S.NumberOfDimensions == 0);
                Assert.IsTrue(B.S.NumberOfDimensions == 0);
                Assert.IsTrue(A.argmax(1, values: B).S.NumberOfDimensions == 0);
                Assert.IsTrue(B.S.NumberOfDimensions == 0);


            }
        }

    }
}
