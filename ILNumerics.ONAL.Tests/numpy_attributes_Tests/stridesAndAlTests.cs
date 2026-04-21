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
    public class numpy_attributes2_Tests {
        [TestMethod]
        public void numpy_strides_Test() {

            Assert.IsTrue(counter(1.0, 1.0, 5, 4, 3).strides.Equals(vector<long>(1, 5, 20))); 
            Assert.IsTrue(counter(1.0, 1.0, 5, 4, 3, StorageOrders.RowMajor).strides.Equals(vector<long>(12, 3, 1))); 

        }

        [TestMethod]
        public void numpy_size_Test() {

            Assert.IsTrue(counter(1.0, 1.0, 5, 4, 3).size_ == prod(vector<long>(5,4,3)));

            //Array<double> Ar = counter<double>(1.0, 1.0, 5, 4, 3); 
            //Array<complex> A = tocomplex(Ar);
            //Array<double> R = A.real();


        }

        [TestMethod]
        public void numpy_ndim_Test() {

            Array<double> A = counter(1.0, 1.0, 5, 4, 3);
            Assert.IsTrue(A.ndim == 3);    // mutable
            Assert.IsTrue(A.T.ndim == 3);  // immutable

        }

        [TestMethod]
        public void numpy_itemsize_Test() {

            Array<double> A = counter(1.0, 1.0, 5, 4, 3);
            Assert.IsTrue(A.itemsize == 8);    // mutable
            var Ret = A.T;
            var sto = Ret.Storage; 
            Assert.IsTrue(Ret.itemsize == 8);  // immutable


        }


    }
}
