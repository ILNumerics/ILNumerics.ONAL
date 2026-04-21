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

namespace ILNumerics.Core.Tests {
    [TestClass]
    public class Concat_32dimsTests {
        [TestMethod]
        public void Concat_Yuan() {

            Array<uint> A = zeros<uint>(1, 2, 1, 4, 2, 3, 7);
            Array<uint> B = ones<uint>(1, 2, 1, 4, 2, 3, 7);
            Array<uint> R = A.Concat(B, 0);

            Array<long> expected = new long[] { 2, 2, 1, 4, 2, 3, 7 };

            Assert.IsTrue(expected.Equals(R.shape));

            Assert.IsTrue(allall(R[0, ellipsis] == 0)); 
            Assert.IsTrue(allall(R[1, ellipsis] == 1));
        }
        private void DoTestConcat<T>(InArray<T> A, InArray<T> B, uint dim) {
            using (Scope.Enter(A, B)) {
                Array<T> R = A.Concat(B, dim);

                Array<long> expShape = A.shape;
                expShape[dim] = A.S[dim] + B.S[dim];
                if (dim >= A.S.NumberOfDimensions) {
                    for (int i = (int)A.S.NumberOfDimensions; i < dim - 1; i++) {
                        expShape[i] = 1; 
                    }
                }
                Assert.IsTrue(R.shape.Equals(expShape));

                // test content
                var dimSpecs = new DimSpec[Math.Max(dim, A.S.NumberOfDimensions)];
                for (int i = 0; i < dimSpecs.Length; i++) {
                    if (i != dim) {
                        dimSpecs[i] = full; 
                    } else {
                        dimSpecs[i] = slice(0, A.S[i]); 
                    }
                }
                Assert.IsTrue(R[dimSpecs].Equals(A));

                for (int i = 0; i < dimSpecs.Length; i++) {
                    if (i != dim) {
                        dimSpecs[i] = full;
                    } else {
                        dimSpecs[i] = r(A.S[i], end);
                    }
                }
                Assert.IsTrue(R[dimSpecs].Equals(B));

            }
        }
        [TestMethod]
        public void Concat_Yuan7() {

            for (uint i = 0; i < 32; i++) {

                using (Scope.Enter()) {

                    Array<long> shape = ones<long>(i, 1) + 1;

                    if (i > 10) {
                        shape[r(10, end)] = 1; 
                    }


                    Array<uint> A = zeros<uint>(shape);
                    Array<uint> B = ones<uint>(shape);

                    for (uint k = 0; k < i; k++) {
                        DoTestConcat<uint>(A, B, k);
                    }
                }
            }
        }

    }
}
