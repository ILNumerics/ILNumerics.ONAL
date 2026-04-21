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
using System.Diagnostics;
using ILNumerics;
using static ILNumerics.ILMath;
using static ILNumerics.Globals;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public class SubarrayReadTests {

        [TestMethod]
        public void SubarrayReadSimpleTest() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new[,] {
                    { 1, 2, 3, 4 },
                    { 5, 6, 7, 8 }
                };

                Array<double> B = A[1];
                Assert.IsTrue(B.IsScalar && B.GetValue(0) == 5);

                Array<double> C = A[1, 1];
                Assert.IsTrue(C.IsScalar && C.GetValue(0) == 6);

                Array<double> D = A[1, 2, 0];
                Assert.IsTrue(D.IsScalar && D.GetValue(0) == 7);
                D = A[1, 2, 0, 0];
                Assert.IsTrue(D.IsScalar && D.GetValue(0) == 7);
                D = A[1, 2, 0, 0, 0];
                Assert.IsTrue(D.IsScalar && D.GetValue(0) == 7);
                D = A[1, 2, 0, 0, 0, 0];
                Assert.IsTrue(D.IsScalar && D.GetValue(0) == 7);
                D = A[1, 3, 0, 0, 0, 0, 0];
                Assert.IsTrue(D.IsScalar && D.GetValue(0) == 8);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SubarrayReadSimpleOORangeTest() {

            Array<double> A = new[,] {
                { 1, 2, 3, 4 },
                { 5, 6, 7, 8 }
            };

            Array<double> B = A[8];

        }

        [TestMethod]
        public void Subarray_ML_3D_LastDimExpansion_InArrayTest() {
            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {

                Array<double> A = counter<double>(1.0, 1.0, 10, 20, 30);
                Array<double> R = func<double>(A);

                Assert.IsTrue(R.shape.Equals(vector<long>(10, 600)));
                Assert.IsTrue(R.Equals(counter<double>(1.0, 1.0, 10, 600)));

                // leave A untouched (InArray!)
                Assert.IsTrue(A.shape.Equals(vector<long>(10, 20, 30)));
                Assert.IsTrue(A.Equals(counter<double>(1.0, 1.0, 10, 20, 30)));

                Array<T> func<T>(InArray<T> a) {

                    return a[full, full];

                }
            }
        }
        [TestMethod]
        public void Subarray_ML_2D_LastDimExpansion_InArrayTest() {
            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {

                Array<double> A = counter<double>(1.0, 1.0, 10, 2);
                Array<double> R = func<double>(A);

                Assert.IsTrue(R.shape.Equals(vector<long>(20, 1)));
                Assert.IsTrue(R.Equals(counter<double>(1.0, 1.0, 20)));

                // leave A untouched (InArray!)
                Assert.IsTrue(A.shape.Equals(vector<long>(10, 2)));
                Assert.IsTrue(A.Equals(counter<double>(1.0, 1.0, 10, 2)));

                Array<T> func<T>(InArray<T> a) {

                    return a[full];

                }
            }
        }

    }
}
