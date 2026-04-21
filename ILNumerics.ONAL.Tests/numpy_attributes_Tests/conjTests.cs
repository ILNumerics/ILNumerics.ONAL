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
    public class conjTests {

        [TestMethod]
        public void numpy_conj_simple() {

            Array<complex> A = ccomplex(ones<double>(5, 4, 3), counter<double>(1, 1, 5, 4, 3));

            A.conj();

            Assert.IsTrue(A.Equals(ccomplex(ones<double>(5, 4, 3), -counter<double>(1, 1, 5, 4, 3))));
        }

        [TestMethod]
        public void numpy_conj_strided() {

            Array<complex> A = ccomplex(ones<double>(5, 4, 3), counter<double>(1, 1, 5, 4, 3));
            A.a = A[Globals.ellipsis, Globals.r(0,2,Globals.end)]; 
            Assert.IsTrue(A.strides.Equals(vector<long>(1, 5, 20 * 2))); 
            Assert.IsTrue(A.shape.Equals(vector<long>(5, 4, 2)));
            A.conj();
            Array<double> real = counter<double>(1, 1, 5, 4, 1) + counter<double>(0, 40, 1, 1, 2);
            Array<complex> R = ccomplex(ones<double>(5, 4, 2), -real);
 
            if (!A.Equals(R)) {

                Assert.IsTrue(false, $"A={A} R={R} diff={maxall(abs(A - R))}");
            }
        }

    }
}
