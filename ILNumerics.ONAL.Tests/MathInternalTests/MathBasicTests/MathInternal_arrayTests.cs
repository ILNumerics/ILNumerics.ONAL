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

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public class arrayTests {
        [TestMethod]
        public void MathInternal_arrayCreation_1D() {

            Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
            Array<double> A = array(-2.0, 2L);

            Assert.IsTrue(A.S[0] == 2);
            Assert.IsTrue(A.S[1] == 1);
            Assert.IsTrue(A.GetValue(0) == -2.0);
            Assert.IsTrue(A.GetValue(1) == -2.0);
            Assert.IsTrue(A.S.NumberOfDimensions == 2);

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                A = array(-2.0, 2L);

                Assert.IsTrue(A.S[0] == 2);
                Assert.IsTrue(A.S[1] == 1);
                Assert.IsTrue(A.GetValue(0) == -2.0);
                Assert.IsTrue(A.GetValue(1) == -2.0);
                Assert.IsTrue(A.S.NumberOfDimensions == 1);

            }
        }
        [TestMethod]
        public void MathInternal_arrayCreation_2D() {

            Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
            Array<double> A = array(-2.0, 2L, 3L);

            Assert.IsTrue(A.S[0] == 2);
            Assert.IsTrue(A.S[1] == 3);
            foreach (var v in A) {
                Assert.IsTrue(v == -2.0);
            }
        }
        [TestMethod]
        public void MathInternal_arrayCreation_3D_Ref() {

            Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
            Array<string> A = array("-2.0", 2L, 3L);

            Assert.IsTrue(A.S[0] == 2);
            Assert.IsTrue(A.S[1] == 3);
            foreach (var v in A) {
                Assert.IsTrue(v == "-2.0");
            }
        }
        [TestMethod]
        public void MathInternal_arrayGivenVector() {
            var vals = new float[] { 1f, 2f, 3f, 4f, 5f, 6f };
            Array<double> S = zeros(3, 2);

            Assert.IsTrue(vector<float>(1f, 2f, 3f, 4f, 5f, 6f).Equals(vector<float>(1, 2, 3, 4, 5, 6)));
            Assert.IsTrue(vector<float>(vals).Reshape(S.shape).Equals(vector<float>(1, 2, 3, 4, 5, 6).Reshape(3, 2)));
            Assert.IsTrue(vector<float>(-99).Repmat(2, 3).Equals(counter<float>(-99f, 0f, 2, 3)));
            Assert.IsTrue(vector(vals).Reshape(2, 3).Equals(vector(vals).Reshape(2, 3)));  // :| ?!

            Assert.IsTrue(array(-99, S.S).Equals(counter<int>(-99, 0, 3, 2)));
            Assert.IsTrue(array(-99, S.S).S.StorageOrder == StorageOrders.ColumnMajor);

            Assert.IsTrue(array(-99, S.S, StorageOrders.RowMajor).Equals(counter<int>(-99, 0, 3, 2)));
            Assert.IsTrue(array(-99, S.S, StorageOrders.RowMajor).S.StorageOrder == StorageOrders.RowMajor);

            Array<long> dims = new long[] { 5, 4, 3 };
            Assert.IsTrue(array(-99, dims).Equals(counter<int>(-99, 0, 5, 4, 3)));
            Assert.IsTrue(array(-99, dims).S.StorageOrder == StorageOrders.ColumnMajor);

            Assert.IsTrue(array(-99, dims, StorageOrders.RowMajor).Equals(counter<int>(-99, 0, 5, 4, 3)));
            Assert.IsTrue(array(-99, dims, StorageOrders.RowMajor).S.StorageOrder == StorageOrders.RowMajor);

        }
    }
}
