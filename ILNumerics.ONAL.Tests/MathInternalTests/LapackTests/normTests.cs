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
using static ILNumerics.Globals;


namespace ILNumerics.Core.UnitTests.MathInternalTests {
    [TestClass]
    public class NormTests {

        [TestMethod]
        public void Test_normFrobenius() {

            Array<double> A = counter<double>(0.0, 1.0, 10, 10);
            Array<double> Res = norm(A, 0);
            Assert.IsTrue(Res - 573.018324314328 < eps);
            // test vectors being consistent with matrices
            Assert.IsTrue(norm(0.0, 0) == 0);
            Assert.IsTrue(norm(1.0, 0) == 1);
            Assert.IsTrue(norm(4.0, 0) == 4);

            Assert.IsTrue(norm(empty<double>(2, dim1: 0)).IsScalar);
            Assert.IsTrue(norm(empty<double>(2, dim1: 0))[0] == 0);

            // test single precision
            Assert.IsTrue(norm(tosingle(A), 0) - 573.018324314328f < epsf);

            Array<complex> C = ccomplex(A, -1);
            Assert.IsTrue(abs(norm(C, 0) - 573.10557491617544) < eps);
            // triggers the vector 'frobenius norm' instead 
            Assert.IsTrue(abs(norm(C["0:9"], 0) - 17.175564037317667) < eps);

            Array<fcomplex> Cf = ccomplex(tosingle(A), -1);
            Assert.IsTrue(abs(norm(Cf, 0) - 573.10557491617544f) < epsf);
            // triggers the vector 'frobenius norm' instead 
            Assert.IsTrue(abs(norm(Cf["0:9"], 0) - 17.175564037317667f) < epsf);

        }


    }
}
