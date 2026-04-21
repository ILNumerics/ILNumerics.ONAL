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
using static ILNumerics.Globals;
using static ILNumerics.ILMath;

namespace ILNumerics.Core.UnitTests.MathInternalTests {
    [TestClass]
    public class MathInternal_minTests {
        [TestMethod]
        public void MathInternal_minBinary_simple() {

            Array<double> A = counter(1.0, 1.0, 5, 4);
            Array<double> B = ones<double>(1,4) + 6;

            Array<double> Res = ILNumerics.Core.Functions.Builtin.MathInternal.min(A, B);

            Array<double> TrueRes = counter(1.0, 1.0, 5, 4);
            TrueRes[r(7, end)] = 7;

            Assert.IsTrue(Res.Equals(TrueRes)); 

        }
        [TestMethod]
        public void MathInternal_min_emptyTest() {

            Array<ushort> A = array<ushort>(0, vector<long>(4, 0, 5));

            Assert.IsTrue(A.IsEmpty);
            Assert.IsTrue(min(A).S[0] == 1);
            Assert.IsTrue(min(A).S[1] == 0);
            Assert.IsTrue(min(A).S[2] == 5);

            Assert.IsTrue(min(A, dim: 0).S[0] == 1);
            Assert.IsTrue(min(A, dim: 0).S[1] == 0);
            Assert.IsTrue(min(A, dim: 0).S[2] == 5);

            Assert.IsTrue(min(A, dim: 1).S[0] == 4);
            Assert.IsTrue(min(A, dim: 1).S[1] == 0);
            Assert.IsTrue(min(A, dim: 1).S[2] == 5);

            Assert.IsTrue(min(A, dim: 2).S[0] == 4);
            Assert.IsTrue(min(A, dim: 2).S[1] == 0);
            Assert.IsTrue(min(A, dim: 2).S[2] == 1);

            Assert.IsTrue(min(A, dim: 3).S[0] == 4);
            Assert.IsTrue(min(A, dim: 3).S[1] == 0);
            Assert.IsTrue(min(A, dim: 3).S[2] == 5);

        }

        [TestMethod]
        public void MathInternal_min_bin_NaN1() {
            Array<fcomplex> A = ccomplex(counter<float>(1f, 1f, 50, 100, 3), vector<float>(1, 1, float.NaN).Reshape(1, 1, 3));

            Assert.IsTrue(min(A, new fcomplex(1f, 0))[ellipsis, "0,1"].real().Equals(ones<float>(50, 100, 2)));

            Assert.IsTrue(min(A, new fcomplex(1f, 0))[ellipsis, 2].real().Equals(ones<float>(50, 100, 1)));
            Assert.IsTrue(min(A, new fcomplex(1f, 0))[ellipsis, 2].imag().Equals(zeros<float>(50, 100, 1)));

            Assert.IsTrue(min(A, new fcomplex(1f, 0), false)[ellipsis, 2].real().Equals(counter<float>(50 * 100 * 2f + 1, 1f, 50, 100, 1)));
            Assert.IsTrue(min(A, new fcomplex(1f, 0), false)[ellipsis, 2].imag().Equals(zeros<float>(50, 100, 1) * float.NaN));

            using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {

                Assert.IsTrue(min(A, new fcomplex(1f, 0))[ellipsis, "0,1"].real().Equals(ones<float>(50, 100, 2)));

                Assert.IsTrue(min(A, new fcomplex(1f, 0))[ellipsis, 2].real().Equals(ones<float>(50, 100, 1)));
                Assert.IsTrue(min(A, new fcomplex(1f, 0))[ellipsis, 2].imag().Equals(zeros<float>(50, 100, 1)));

                Assert.IsTrue(min(A, new fcomplex(1f, 0), false)[ellipsis, 2].real().Equals(counter<float>(50 * 100 * 2f + 1, 1f, 50, 100, 1)));
                Assert.IsTrue(min(A, new fcomplex(1f, 0), false)[ellipsis, 2].imag().Equals(zeros<float>(50, 100, 1) * float.NaN));

            }
        } 
        [TestMethod]
        public void MathInternal_max_bin_NaN1() {
            Array<fcomplex> A = ccomplex(counter<float>(1f, 1f, 50, 100, 3), vector<float>(1, 1, float.NaN).Reshape(1, 1, 3));

            Assert.IsTrue(max(A, new fcomplex(1f, 0))[ellipsis, "0,1"].real().Equals(counter<float>(1f, 1f, 50, 100, 2)));

            Assert.IsTrue(max(A, new fcomplex(1f, 0))[ellipsis, 2].real().Equals(ones<float>(50, 100, 1)));
            Assert.IsTrue(max(A, new fcomplex(1f, 0))[ellipsis, 2].imag().Equals(zeros<float>(50, 100, 1)));

            Assert.IsTrue(max(A, new fcomplex(1f, 0), false)[ellipsis, 2].real().Equals(counter<float>(50 * 100 * 2f + 1, 1f, 50, 100, 1)));
            Assert.IsTrue(max(A, new fcomplex(1f, 0), false)[ellipsis, 2].imag().Equals(zeros<float>(50, 100, 1) * float.NaN));

            using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {

                Assert.IsTrue(max(A, new fcomplex(1f, 0))[ellipsis, "0,1"].real().Equals(counter<float>(1f, 1f, 50, 100, 2)));

                Assert.IsTrue(max(A, new fcomplex(1f, 0))[ellipsis, 2].real().Equals(ones<float>(50, 100, 1)));
                Assert.IsTrue(max(A, new fcomplex(1f, 0))[ellipsis, 2].imag().Equals(zeros<float>(50, 100, 1)));

                Assert.IsTrue(max(A, new fcomplex(1f, 0), false)[ellipsis, 2].real().Equals(counter<float>(50 * 100 * 2f + 1, 1f, 50, 100, 1)));
                Assert.IsTrue(max(A, new fcomplex(1f, 0), false)[ellipsis, 2].imag().Equals(zeros<float>(50, 100, 1) * float.NaN));

            }
        }


    }
}
