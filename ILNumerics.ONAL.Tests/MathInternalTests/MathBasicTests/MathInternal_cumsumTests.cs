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
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;
using static ILNumerics.Globals; 

namespace ILNumerics.Core.UnitTests.MathInternalTests {
    /// <summary>
    /// Summary description for MathInternal_cumsumTests
    /// </summary>
    [TestClass]
    public class MathInternal_cumsumTests {

        [TestMethod]
        public void MathInternal_cumsum_simpleTest() {

            Array<double> A = counter(1.0, 1.0, 4, 3);
            Array<double> B = cumsum(A);
            Array<double> Res = new double[,] {
                { 1, 5, 9 },
                { 3, 11, 19 },
                { 6, 18, 30 },
                { 10, 26, 42 }
            };
            Assert.IsTrue(B.Equals(Res));

            A = counter(1.0, 1.0, 3, 4, StorageOrders.RowMajor);
            B = cumsum(A, 1);
            Assert.IsTrue(B.Equals(Res.T)); 

        }

        [TestMethod]
        public void MathInternal_cumsum_emptyTest() {

            Array<ushort> A = array<ushort>(0, vector<long>(4, 0, 5));

            Assert.IsTrue(A.IsEmpty);
            Assert.IsTrue(cumsum(A).S[0] == 4);
            Assert.IsTrue(cumsum(A).S[1] == 0);
            Assert.IsTrue(cumsum(A).S[2] == 5);

            Assert.IsTrue(cumsum(A, dim: 0).S[0] == 4);
            Assert.IsTrue(cumsum(A, dim: 0).S[1] == 0);
            Assert.IsTrue(cumsum(A, dim: 0).S[2] == 5);

            Assert.IsTrue(cumsum(A, dim: 1).S[0] == 4);
            Assert.IsTrue(cumsum(A, dim: 1).S[1] == 0);
            Assert.IsTrue(cumsum(A, dim: 1).S[2] == 5);

            Assert.IsTrue(cumsum(A, dim: 2).S[0] == 4);
            Assert.IsTrue(cumsum(A, dim: 2).S[1] == 0);
            Assert.IsTrue(cumsum(A, dim: 2).S[2] == 5);

            Assert.IsTrue(cumsum(A, dim: 3).S[0] == 4);
            Assert.IsTrue(cumsum(A, dim: 3).S[1] == 0);
            Assert.IsTrue(cumsum(A, dim: 3).S[2] == 5);

        }

        [TestMethod]
        public void MathInternal_cumsum_scalarTest() {

            Array<uint> A = 91;

            Assert.IsTrue(cumsum(A).IsScalar);
            Assert.IsTrue((uint)cumsum(A) == 91);
            Assert.IsTrue(cumsum(A).S.NumberOfDimensions == 2);
            Assert.IsTrue(cumsum(A).S[0] == 1); 
            Assert.IsTrue(cumsum(A).S[1] == 1);

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                A = 92;
                Assert.IsTrue(cumsum(A).IsScalar);
                Assert.IsTrue((uint)cumsum(A) == 92);
                Assert.IsTrue(cumsum(A).S.NumberOfDimensions == 0);
                Assert.IsTrue(cumsum(A).S[0] == 1);
                Assert.IsTrue(cumsum(A).S[1] == 1);

            }
        }
    }
}
