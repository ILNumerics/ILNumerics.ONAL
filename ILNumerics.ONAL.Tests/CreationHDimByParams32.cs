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
using ILNumerics;
using static ILNumerics.Globals;
using static ILNumerics.ILMath;

namespace ILNumerics.Core.Tests {
    [TestClass]
    public class CreationHDimByParams32
        {

        [TestMethod]
        public void CreateEmptyParams32() {
            Array<double> A = empty<double>(1, 2, 3, 4, 5, 6, 7,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8);
            Assert.IsTrue(A.S.NumberOfDimensions == 32);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7 * 8);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);

            A = empty<double>(1, 2, 3, 4, 5, 6, 7,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8);
            Assert.IsTrue(A.S.NumberOfDimensions == 32);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7 * 8);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);

        }

        [TestMethod]
        public void Create_ZerosParams32() {
            Array<double> A = zeros<double>(1, 2, 3, 4, 5, 6, 7,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8);
            Assert.IsTrue(A.S.NumberOfDimensions == 32);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7 * 8);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(!anyall(A));

            A = zeros<double>(1, 2, 3, 4, 5, 6, 7,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8);
            Assert.IsTrue(A.S.NumberOfDimensions == 32);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7 * 8);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(!anyall(A));

        }
        [TestMethod]
        public void Create_OnesParams32() {
            Array<double> A = ones<double>(1, 2, 3, 4, 5, 6, 7,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8);
            Assert.IsTrue(A.S.NumberOfDimensions == 32);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7 * 8);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(allall(A == 1));

            A = ones<double>(1, 2, 3, 4, 5, 6, 7,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8);
            Assert.IsTrue(A.S.NumberOfDimensions == 32);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7 * 8);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(allall(A == 1));


        }
        [TestMethod]
        public void Create_RandParams32() {
            Array<double> A = rand(1, 2, 3, 4, 5, 6, 7,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8);
            Assert.IsTrue(A.S.NumberOfDimensions == 32);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7 * 8);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(!anyall(A < 0 | A > 1));

            A = rand(1, 2, 3, 4, 5, 6, 7,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8);
            Assert.IsTrue(A.S.NumberOfDimensions == 32);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7 * 8);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(!anyall(A < 0 | A > 1));

        }
        [TestMethod]
        public void Create_RandNParams32() {
            Array<double> A = randn(1, 2, 3, 4, 5, 6, 7,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8);
            Assert.IsTrue(A.S.NumberOfDimensions == 32);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7 * 8);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(!anyall(A < -100 | A > 100));

            A = randn(1, 2, 3, 4, 5, 6, 7,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,8);
            Assert.IsTrue(A.S.NumberOfDimensions == 32);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7 * 8);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(!anyall(A < -100 | A > 100));

        }
    }
}
