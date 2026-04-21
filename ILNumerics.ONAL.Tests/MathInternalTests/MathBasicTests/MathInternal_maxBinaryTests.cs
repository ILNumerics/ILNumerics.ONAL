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
    public class MathInternal_maxBinaryTests {

        [TestMethod]
        public void MathInternal_max_bin_simple() {
            Array<float> A = counter<float>(0.1f, 0.1f, 1, 138);
            Array<float> B = linspace<float>(0.0f, 20f, 138);

            Array<float> Res = max(A, B);
            Assert.IsTrue(Res.S[0] == 1);
            Assert.IsTrue(Res.S[1] == 138);
            Assert.IsTrue(Res.S.NumberOfDimensions == 2);

            Assert.IsTrue(allall(Res[r(0, 2)] == A[r(0, 2)]));
            Assert.IsTrue(allall(Res[r(3, -1)] == B[r(3, end)]));

        }
        [TestMethod]
        public void MathInternal_max_bin_NanLxBug() {
            Array<float> A = zeros<float>(1, 4);
            Array<float> B = ones<float>(4, 4);

            Array<float> Res1 = max(float.NaN * A, B);
            Assert.IsTrue(Res1.S[0] == 4);
            Assert.IsTrue(Res1.S[1] == 4);
            Assert.IsTrue(Res1.S.NumberOfDimensions == 2);

            Array<float> Res2 = max(B, float.NaN * A);
            Assert.IsTrue(Res2.S[0] == 4);
            Assert.IsTrue(Res2.S[1] == 4);
            Assert.IsTrue(Res2.S.NumberOfDimensions == 2);

            //Assert.IsTrue(allall(Res[r(0, 2)] == A[r(0, 2)]));
            //Assert.IsTrue(allall(Res[r(3, -1)] == B[r(3, end)]));

        }

    }
}
