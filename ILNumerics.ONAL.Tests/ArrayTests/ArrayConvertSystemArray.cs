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

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public class ArrayConvertSystemArray {

        [TestMethod]
        public void Convert1DColumnMajorTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Assert.IsTrue(Settings.DefaultStorageOrder == StorageOrders.ColumnMajor);
                Array<uint> A = new uint[] { 1, 2, 3, 4, 5 };
                Assert.IsTrue(Settings.MinNumberOfArrayDimensions == 2);
                // single dim vectors are both: column and row major
                Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor || A.S.StorageOrder == StorageOrders.ColumnMajor);

                Assert.IsTrue(A.S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions);
                Assert.IsTrue(A.S[0] == 5);
                Assert.IsTrue(A.S[1] == 1);
                Assert.IsTrue(A.S.GetStride(0) == 1);
                Assert.IsTrue(A.S.GetStride(1) == 0);

            }
        }

        [TestMethod]
        public void Convert1DRowMajorTest() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) { 

                Assert.IsTrue(Settings.DefaultStorageOrder == StorageOrders.RowMajor);
                Array<uint> A = new uint[] { 1, 2, 3, 4, 5 };
                Assert.IsTrue(Settings.MinNumberOfArrayDimensions == 0);
                // single dim vectors are both: column and row major
                Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor || A.S.StorageOrder == StorageOrders.ColumnMajor);

                Assert.IsTrue(A.S.NumberOfDimensions == 1);
                Assert.IsTrue(A.S[0] == 5);
                Assert.IsTrue(A.S[1] == 1);
                Assert.IsTrue(A.S.GetStride(0) == 1);
                Assert.IsTrue((ulong)A.S.GetStride(1) == 0);

            }
        }
    }
}
