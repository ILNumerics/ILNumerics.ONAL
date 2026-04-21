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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;
using static ILNumerics.Globals; 

namespace ILNumerics.Core.UnitTests.MathInternalTests {
    [TestClass]
    public class MathInternal_maxTests2 {

        [TestMethod]
        public void MathInternal_max_reuse_IndicesArray() {
            Array<int> A = counter<int>(1, 1, 4, 3, 5, StorageOrders.RowMajor);

            Array<long> I = zeros<long>(1, 3, 6); // enough to store the indices
            var stor = I.Storage;
            var mem = I.Storage.m_handles;
            Array<int> B = max(A, I, keepdim: true);

            Assert.IsTrue(ReferenceEquals(I.Storage, stor));
            Assert.IsTrue(ReferenceEquals(I.Storage.m_handles, mem));

            max(counter<int>(1, 1, 4, 6, 7), I).Release();
            // had to create new I storage internally. This has "renamed" I. 
            Assert.IsFalse(ReferenceEquals(I.Storage, stor));
            Assert.IsFalse(ReferenceEquals(I.Storage.m_handles, mem));

            Assert.IsTrue(I.Equals(ones<long>(1, 6, 7) * 3));


        }

    }
}
