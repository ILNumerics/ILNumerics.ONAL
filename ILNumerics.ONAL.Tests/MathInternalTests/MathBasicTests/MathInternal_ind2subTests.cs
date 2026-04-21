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
    public class MathInternal_ind2subTests {

        [TestMethod]
        public void mathInternal_ind2sub_simpleTest() {

            Array<double> A1 = counter<double>(0.0, 1.0, 5, 4, 3, StorageOrders.ColumnMajor);
            Array<long> B = ind2sub(A1, counter<long>(0, 1, 5 * 4 * 3));

            Assert.IsTrue(B.S[1] == A1.S.NumberOfDimensions); 
            Assert.IsTrue(B.S[0] == 5 * 4 * 3);

            Array<long> I1 = (counter<long>(0, 1, 5, 1) * ones<long>(1, 4 * 3)).Reshape(5 * 4 * 3, 1, StorageOrders.ColumnMajor);
            Assert.IsTrue(B[full, 0].Equals(I1));

            Array<long> I2 = (counter<long>(0, 1, 4, 1).repeat(5).T * ones<long>(1, 3)).Reshape(5 * 4 * 3, 1, StorageOrders.ColumnMajor);
            Assert.IsTrue(B[full, 1].Equals(I2));

            Array<long> I3 = counter<long>(0, 1, 1, 3).repeat(5 * 4).T;
            Assert.IsTrue(B[full, 2].Equals(I3));

        }
        [TestMethod]
        public void mathInternal_ind2sub_3D_2Out() {

            Array<double> A1 = counter<double>(0.0, 1.0, 5, 4, 3, StorageOrders.ColumnMajor);
            Array<long> B = ind2sub(A1, counter<long>(0, 1, 5 * 4 * 3), nrOutDims: 2);

            Assert.IsTrue(B.S[1] == 2);
            Assert.IsTrue(B.S[0] == 5 * 4 * 3);

            Array<long> I1 = (counter<long>(0, 1, 5, 1) * ones<long>(1, 4 * 3)).Reshape(5 * 4 * 3, 1, StorageOrders.ColumnMajor);
            Assert.IsTrue(B[full, 0].Equals(I1));

            Array<long> I2 = (counter<long>(0, 1, 4, 1).repeat(5).T * ones<long>(1, 3)).Reshape(5 * 4 * 3, 1, StorageOrders.ColumnMajor);
            Array<long> I3 = counter<long>(0, 1, 1, 3).repeat(5 * 4).T;
            Assert.IsTrue(B[full, 1].Equals(I2 + 4 * I3));

        }
        [TestMethod]
        public void mathInternal_ind2sub_3D_4Out() {

            Array<double> A1 = counter<double>(0.0, 1.0, 5, 4, 3, StorageOrders.ColumnMajor);
            Array<long> B = ind2sub(A1, counter<long>(0, 1, 5 * 4 * 3), nrOutDims: 4);

            Assert.IsTrue(B.S[1] == 4);
            Assert.IsTrue(B.S[0] == 5 * 4 * 3);

            Array<long> I1 = (counter<long>(0, 1, 5, 1) * ones<long>(1, 4 * 3)).Reshape(5 * 4 * 3, 1, StorageOrders.ColumnMajor);
            Assert.IsTrue(B[full, 0].Equals(I1));

            Array<long> I2 = (counter<long>(0, 1, 4, 1).repeat(5).T * ones<long>(1, 3)).Reshape(5 * 4 * 3, 1, StorageOrders.ColumnMajor);
            Assert.IsTrue(B[full, 1].Equals(I2));

            Array<long> I3 = counter<long>(0, 1, 1, 3).repeat(5 * 4).T;
            Assert.IsTrue(B[full, 2].Equals(I3));

            Assert.IsTrue(allall(B[full, 3] == 0)); 
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void mathInternal_ind2sub_0D_fail() {
            using (Scope.Enter(arrayStyle: ArrayStyles.numpy)) {
                Array<double> A = 1;
                ind2sub(A, vector<long>(0, 0)); 
            }
        }

    }
}
