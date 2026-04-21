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
    public class transposeTests {
        [TestMethod]
        public void numpy_transpose_simple() {

            Array<double> A = counter<double>(1, 1, 5, 4, 3);

            A.a = A.transpose(MathInternal.vector<long>(0,2,1));
            Assert.IsTrue(A.S[0] == 5);
            Assert.IsTrue(A.S[1] == 3);
            Assert.IsTrue(A.S[2] == 4);
            Assert.IsTrue(A.S.NumberOfDimensions == 3);
            Assert.IsTrue(A.S.NumberOfElements == 5* 4* 3);

        }
        [TestMethod]
        public void numpy_transpose_RM() {

            Array<double> A = counter<double>(1, 1, 5, 4, 3, StorageOrders.RowMajor);
            Array<fcomplex> B;
            A.a = A.transpose(MathInternal.vector<long>(0, 2, 1));
            Assert.IsTrue(A.S[0] == 5);
            Assert.IsTrue(A.S[1] == 3);
            Assert.IsTrue(A.S[2] == 4);
            Assert.IsTrue(A.S.NumberOfDimensions == 3);
            Assert.IsTrue(A.S.NumberOfElements == 5 * 4 * 3);

        }

        [TestMethod]
        public void numpy_transpose_empty() {

            Array<double> A = counter<double>(1, 1, 5, 1, 0, 2, StorageOrders.RowMajor);
            Array<double> B = A.transpose(MathInternal.vector<long>(3,2,1,0));

            Assert.IsTrue(B.Equals(A.transpose()));
            Assert.IsTrue(B.S.NumberOfDimensions == 4);
            Assert.IsTrue(B.IsEmpty);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void numpy_transpose_unmatching_dimensionCount() {

            Array<double> A = ones<double>(10, 4, 3);
            A.transpose(vector<long>(1, 0, 2, 3));
 
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void numpy_transpose_duplicate_dimensions() {

            Array<double> A = ones<double>(10, 4, 3);
            A.transpose(vector<long>(1, 0, 1));

        }

    }
}
