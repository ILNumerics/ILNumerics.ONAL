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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;

namespace ILNumerics.Core.UnitTests.numpy_attributes_Tests {

    [TestClass]
    public class meanTests {
        [TestMethod]
        public void numpy_mean_simple() {

            Array<double> A = counter<double>(1, 1, 5, 4);
            Array<double> B = A.mean();
            Assert.IsTrue(B == sumall(A) / A.S.NumberOfElements);
            Assert.IsTrue(B.IsScalar); 

        }

        [TestMethod]
        public void numpy_mean_axisNull() {
            Array<double> A = counter<double>(1, 1, 5, 4, 3);

            Assert.IsTrue(A.mean(true).Equals((sumall(A) / A.S.NumberOfElements).Reshape(1, 1, 1)));
            Assert.IsTrue(A.mean(true).shape.Equals(vector<long>(1, 1, 1)));

            Assert.IsTrue(A.mean(false).Equals((sumall(A) / A.S.NumberOfElements)));
            Assert.IsTrue(A.mean(false).shape.Equals(vector<long>(1, 1)));

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                Assert.IsTrue(A.mean(false).Equals((sumall(A) / A.S.NumberOfElements)));
                Assert.IsTrue(A.mean(false).shape.Equals(empty<long>(0)));
            }
        }
        [TestMethod]
        public void numpy_mean_axisInt() {
            Array<double> A = counter<double>(1, 1, 5, 4, 3);

            Assert.IsTrue(A.mean(0, true).Equals((sum(A, 0, true) / 5)));
            Assert.IsTrue(A.mean(0, true).shape.Equals(vector<long>(1, 4, 3)));

            Assert.IsTrue(A.mean(1, false).Equals((sum(A, 1, false) / 4)));
            Assert.IsTrue(A.mean(1).shape.Equals(vector<long>(5, 3))); // default keepdims: false

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                Assert.IsTrue(A.mean(2, false).Equals((sum(A, 2, false) / 3)));
                Assert.IsTrue(A.mean(2, false).shape.Equals(vector<long>(5, 4)));
            }
        }
        [TestMethod]
        public void numpy_mean_axisArray0Elem() {
            Array<double> A = counter<double>(1, 1, 5, 4, 3);
            Array<int> Axes = empty<int>(0); 

            Assert.IsTrue(A.mean(vector<long>(0), true).Equals((sum(A, 0, true) / 5)));
            Assert.IsTrue(A.mean(vector<long>(0), true).shape.Equals(vector<long>(1, 4, 3)));

            Assert.IsTrue(A.mean(vector<long>(1), false).Equals((sum(A, 1, false) / 4)));
            Assert.IsTrue(A.mean(vector<long>(1)).shape.Equals(vector<long>(5, 3))); // default keepdims: false

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                Assert.IsTrue(A.mean(vector<long>(2), false).Equals((sum(A, 2, false) / 3)));
                Assert.IsTrue(A.mean(vector<long>(2), false).shape.Equals(vector<long>(5, 4)));
            }
        }
        [TestMethod]
        public void numpy_mean_axisArray1Elem() {
            Array<double> A = counter<double>(1, 1, 5, 4, 3);
            Array<uint> Axes = vector<uint>(1);


            Assert.IsTrue(A.mean(Axes, true).Equals((sum(A, 1, true) / 4)));
            Assert.IsTrue(A.mean(Axes.T, true).Equals((sum(A, 1, true) / 4)));
            Assert.IsTrue(A.mean(Axes, true).shape.Equals(vector<long>(5, 1, 3)));

            Axes.put(vector<int>(0), 0);
            Assert.IsTrue(A.mean(Axes, false).Equals((sum(A, 0, false) / 5)));
            Assert.IsTrue(A.mean(Axes).shape.Equals(vector<long>(4, 3))); // default keepdims: false

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                Axes.put(vector<int>(0), 2);
                Assert.IsTrue(A.mean(Axes, false).Equals((sum(A, 2, false) / 3)));
                Assert.IsTrue(A.mean(Axes, false).shape.Equals(vector<long>(5, 4)));
                Assert.IsTrue(A.mean(Axes, true).shape.Equals(vector<long>(5, 4, 1)));
            }
        }

        [TestMethod]
        public void numpy_mean_emptyA() {
            Array<float> A = empty<float>(1, 0, 5, 4, 3);

            Assert.IsTrue(A.mean().IsScalar);
            Assert.IsTrue(A.mean().S.NumberOfDimensions == 2);
            Assert.IsTrue(float.IsNaN((float)A.mean()));

            Assert.IsTrue(A.mean(true).S.NumberOfDimensions == 5);
            Assert.IsTrue(A.mean(true).shape.Equals(vector<long>(1,1,1,1,1)));
            Assert.IsTrue(float.IsNaN((float)A.mean(true)));

            Assert.IsTrue(A.mean(0).S.NumberOfDimensions == 4);
            Assert.IsTrue(A.mean(0).shape.Equals(vector<long>(0, 5, 4, 3)));
            Assert.IsTrue(A.mean(0).Equals(array<float>(float.NaN, 0, 5, 4, dim3: 3)));
            Assert.IsTrue(A.mean(0,true).S.NumberOfDimensions == 5);
            Assert.IsTrue(A.mean(0,true).shape.Equals(vector<long>(1, 0, 5, 4, 3)));
            Assert.IsTrue(A.mean(0,true).Equals(array<float>(float.NaN, 1, 0, 5, 4, dim4: 3)));


            Assert.IsTrue(A.mean(1).S.NumberOfDimensions == 4);
            Assert.IsTrue(A.mean(1).shape.Equals(vector<long>(1, 5, 4, 3)));
            Assert.IsTrue(A.mean(1).Equals(array<float>(float.NaN, 1,5, 4, dim3: 3)));
            Assert.IsTrue(A.mean(1,true).S.NumberOfDimensions == 5);
            Assert.IsTrue(A.mean(1,true).shape.Equals(vector<long>(1, 1, 5, 4, 3)));
            Assert.IsTrue(A.mean(1,true).Equals(array<float>(float.NaN, 1, 1, 5, 4, dim4: 3)));

            Assert.IsTrue(A.mean(2).S.NumberOfDimensions == 4);
            Assert.IsTrue(A.mean(2).shape.Equals(vector<long>(1, 0, 4, 3)));
            Assert.IsTrue(A.mean(2).Equals(array<float>(float.NaN, 1, 0, 4, dim3: 3)));
            Assert.IsTrue(A.mean(2,true).S.NumberOfDimensions == 5);
            Assert.IsTrue(A.mean(2,true).shape.Equals(vector<long>(1, 0, 1, 4, 3)));
            Assert.IsTrue(A.mean(2,true).Equals(array<float>(float.NaN, 1, 0, 1, 4, dim4: 3)));

            Assert.IsTrue(A.mean(3).S.NumberOfDimensions == 4);
            Assert.IsTrue(A.mean(3).shape.Equals(vector<long>(1, 0, 5, 3)));
            Assert.IsTrue(A.mean(3).Equals(array<float>(float.NaN, 1, 0, 5, dim3: 3)));
            Assert.IsTrue(A.mean(3,true).S.NumberOfDimensions == 5);
            Assert.IsTrue(A.mean(3,true).shape.Equals(vector<long>(1, 0, 5, 1, 3)));
            Assert.IsTrue(A.mean(3,true).Equals(array<float>(float.NaN, 1, 0, 5, 1, dim4: 3)));

            Assert.IsTrue(A.mean(4).S.NumberOfDimensions == 4);
            Assert.IsTrue(A.mean(4).shape.Equals(vector<long>(1, 0, 5, 4)));
            Assert.IsTrue(A.mean(4).Equals(array<float>(float.NaN, 1, 0, 5, dim3: 4)));
            Assert.IsTrue(A.mean(4,true).S.NumberOfDimensions == 5);
            Assert.IsTrue(A.mean(4,true).shape.Equals(vector<long>(1, 0, 5, 4, 1)));
            Assert.IsTrue(A.mean(4,true).Equals(array<float>(float.NaN, 1, 0, 5, 4, dim4: 1)));

        }

    }
}
