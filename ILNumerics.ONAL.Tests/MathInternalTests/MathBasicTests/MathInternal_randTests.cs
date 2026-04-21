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

namespace ILNumerics.Core.UnitTests.MathInternalTests {
    /// <summary>
    /// Summary description for MathInternal_randTests
    /// </summary>
    [TestClass]
    public class MathInternal_randTests {

        [TestMethod]
        public void MathInternal_rand_simple1D() {
            Array<double> A = rand(5);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.ILNumericsV4 && A.S.NumberOfDimensions == 2);
            Assert.IsTrue(A.S[0] == 5 && A.S[1] == 5);

            double last = -1;
            foreach (var a in A) {
                Assert.IsTrue(a != last);
                last = a;
            }
        }
        [TestMethod]
        public void MathInternal_rand_simple2D_rowMaj() {
            Array<double> A = rand(5, 4, StorageOrders.RowMajor);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor);
            Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.ILNumericsV4 && A.S.NumberOfDimensions == 2);
            Assert.IsTrue(A.S[0] == 5 && A.S[1] == 4);

            double last = -1;
            foreach (var a in A) {
                Assert.IsTrue(a != last);
                last = a;
            }
        }
        [TestMethod]
        public void MathInternal_rand_simple3D_rowMaj_empty() {
            Array<double> A = rand(5, 4, 0, StorageOrders.RowMajor);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor);
            Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.ILNumericsV4 && A.S.NumberOfDimensions == 3);
            Assert.IsTrue(A.S[0] == 5 && A.S[1] == 4 && A.S[2] == 0);

            double last = -1;
            foreach (var a in A) {
                Assert.IsTrue(a != last);
                last = a;
            }
        }
        [TestMethod]
        public void MathInternal_rand_simple7D_colMaj() {
            Array<double> A = rand(5, 4, 1, 2, 3, 4, 2, StorageOrders.ColumnMajor);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.ILNumericsV4 && A.S.NumberOfDimensions == 7);
            Assert.IsTrue(A.S[0] == 5 && A.S[1] == 4 && A.S[2] == 1 && A.S[3] == 2);
            Assert.IsTrue(A.S[4] == 3 && A.S[5] == 4 && A.S[6] == 2);

            double last = -1;
            foreach (var a in A) {
                Assert.IsTrue(a != last);
                last = a;
            }
        }
        [TestMethod]
        public void MathInternal_rand_LargeParall7D_rowMaj() {
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {
                Array<double> A = rand(50, 40, 10, 2, 3, 4, 2, StorageOrders.RowMajor);
                Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor);
                Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.ILNumericsV4 && A.S.NumberOfDimensions == 7);
                Assert.IsTrue(A.S[0] == 50 && A.S[1] == 40 && A.S[2] == 10 && A.S[3] == 2);
                Assert.IsTrue(A.S[4] == 3 && A.S[5] == 4 && A.S[6] == 2);

                double last = -1;
                foreach (var a in A) {
                    Assert.IsTrue(a != last);
                    last = a;
                }
            }
        }
    }
}
