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
using ILNumerics.Core.Functions.Builtin;

namespace ILNumerics.Core.UnitTests.numpy_attributes_Tests {

    [TestClass]
    public class cumprodTests {

        [TestMethod]
        public void numpy_cumprod_simple() {

            Array<double> A = counter<double>(1, 1, 5, 4, 3);
            Array<double> B = A.cumprod();

            Assert.IsTrue(B.Equals(MathInternal.cumprod(A.flatten())));
            Assert.IsTrue(B.IsVector);
            Assert.IsTrue(B.Length == A.S.NumberOfElements); 

        }

        [TestMethod]
        public void numpy_cumprod_empty_givesScalar() {

            Array<float> A = empty<float>(0, 3, 1);
            Array<float> B = A.cumprod(2);

            Assert.IsTrue(A.cumprod(0).shape.Equals(vector<long>(0, 3, 1))); 
            Assert.IsTrue(A.cumprod(1).shape.Equals(vector<long>(0, 3, 1))); 
            Assert.IsTrue(A.cumprod(2).shape.Equals(vector<long>(0, 3, 1))); 
        }



    }
}
