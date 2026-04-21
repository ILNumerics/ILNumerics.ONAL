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
using ILNumerics; 
using static ILNumerics.ILMath;

namespace ILNumerics.Core.UnitTests.numpy_attributes_Tests {

    [TestClass]
    public class maxTests {
        [TestMethod]
        public void numpy_max_simple() {

            Array<double> A = counter<double>(1, 1, 5, 4, 3);
            Array<long> I = 0;
            // axis null
            Assert.IsTrue(A.max().shape.Equals(vector<long>(1,1)));
            Assert.IsTrue(A.max().Equals(vector<double>(60)));
            Assert.IsTrue(A.max(null, I).Equals(vector<double>(60)));
            Assert.IsTrue(I.shape.Equals(vector<long>(1,1)));
            Assert.IsTrue(I.Equals(vector<long>(59)));

            // axis -1
            Assert.IsTrue(A.max(-1).shape.Equals(vector<long>(5, 4)));
            Assert.IsTrue(A.max(-1).Equals(counter<double>(41, 1, 5, 4)));
            Assert.IsTrue(A.max(-1, I).Equals(counter<double>(41, 1, 5, 4)));
            Assert.IsTrue(I.shape.Equals(vector<long>(5, 4)));
            Assert.IsTrue(I.Equals(ones<long>(5, 4) * 2));

            // axis 0
            Assert.IsTrue(A.max(0).shape.Equals(vector<long>(4, 3)));
            Assert.IsTrue(A.max(0).Equals(counter<double>(5, 5, 4, 3)));
            Assert.IsTrue(A.max(0, I).Equals(counter<double>(5, 5, 4, 3)));
            Assert.IsTrue(I.shape.Equals(vector<long>(4, 3)));
            Assert.IsTrue(I.Equals(ones<long>(4, 3) * 4));

            // axis 1
            Assert.IsTrue(A.max(1).shape.Equals(vector<long>(5, 3)));
            Assert.IsTrue(A.max(1).Equals(counter<double>(1 + 5 * 3, 1, 5, 1) + counter<double>(0, 20, 1, 3)));
            Assert.IsTrue(A.max(1, I).Equals(counter<double>(1 + 5 * 3, 1, 5, 1) + counter<double>(0, 20, 1, 3)));
            Assert.IsTrue(I.shape.Equals(vector<long>(5, 3)));
            Assert.IsTrue(I.Equals(ones<long>(5, 3) * 3));

            // axis 2
            Assert.IsTrue(A.max(2).shape.Equals(vector<long>(5, 4)));
            Assert.IsTrue(A.max(2).Equals(counter<double>(41, 1, 5, 4)));
            Assert.IsTrue(A.max(2, I).Equals(counter<double>(41, 1, 5, 4)));
            Assert.IsTrue(I.shape.Equals(vector<long>(5, 4)));
            Assert.IsTrue(I.Equals(ones<long>(5, 4) * 2));

        }

    }
}
