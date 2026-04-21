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
    public class resizeTests {
        [TestMethod]
        public void numpy_resize_simple() {

            Array<double> A = counter<double>(1, 1, 5, 4, 3);

            A.resize(MathInternal.vector<long>(6, 4, 3));
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);

            Assert.IsTrue(all(A.shape == MathInternal.vector<long>(6, 4, 3)));
            Assert.IsTrue(A["end"] == 0);
            Assert.IsTrue(allall(A[":", "-2:-1", -1] == 0));
            
        }
        [TestMethod]
        public void numpy_resize_RM() {

            Array<double> A = counter<double>(1, 1, 5, 4, 3, StorageOrders.RowMajor);

            A.resize(MathInternal.vector<long>(6, 4, 3));
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor);

            Assert.IsTrue(all(A.shape == MathInternal.vector<long>(6, 4, 3)));
            Assert.IsTrue(A[-1] == 0);
            Assert.IsTrue(allall(A[-1,":", ":"] == 0));

        }

        [TestMethod]
        public void numpy_resize_2_empty() {
            Array<double> A = counter<double>(1, 1, 5, 4, 3, StorageOrders.RowMajor);
            A.resize(MathInternal.empty<long>(0));

            Assert.IsTrue(A.IsScalar);
            Assert.IsTrue(A.S.NumberOfDimensions == Math.Max(0, Settings.MinNumberOfArrayDimensions));
            Assert.IsTrue(A.GetValue(0) == 1);

        }
        [TestMethod]
        public void numpy_resize_from_empty() {

            Array<double> A = empty<double>(1, 0, 3); 
            A.resize(MathInternal.vector<long>(10,20));

            Assert.IsTrue(A.S[0] == 10);
            Assert.IsTrue(A.S[1] == 20);
            Assert.IsTrue(A.S.NumberOfDimensions == 2);
            Assert.IsTrue(allall(A == 0));

        }

    }
}
