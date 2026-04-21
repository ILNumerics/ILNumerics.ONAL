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
using ILNumerics.Core.Internal;
//ILN(enabled=false)
namespace Core_Tests_small {

    [TestClass]
    public class applyIndexed_Tests {

        [TestMethod]
        public void applyIndexed_Vector() {
            Array<uint> A = zeros<uint>(10, 1);
            Array<uint> B = zeros<uint>(10, 1);
            Array<uint> C = apply(A, B, (a, b, i) => (uint)i);

            Assert.IsTrue(C.Equals(counter<uint>(0, 1, 10)));
        }

        [TestMethod]
        public void applyIndexed_simple() {

            Array<double> A = vector(0.0, 0, 1) * ones(3, 4, 5);
            Array<double> B = vector(0.0, 1, 0) * ones(3, 4, 5);

            Array<double> C = apply(A, B, (a, b, i) => (double)i);
            Assert.IsTrue(C.Equals(counter<double>(0.0, 1.0, 3, 4, 5)));
        }

        [TestMethod]
        public void applyIndexed_doc1() {

            Array<int> C = apply(zeros<int>(10,1), vector(-1), (a, b, i) => (int)(a + i * b));
            Assert.IsTrue(C.Equals(counter<int>(0, -1, 10, 1))); 
            //C
            //<Int32>[10, 1] 0...- 9 |
            //    [0]:            0
            //    [1]:           -1
            //    [2]:           -2
            //    [3]:           -3
            //    [4]:           -4
            //    [5]:           -5
            //    [6]:           -6
            //    [7]:           -7
            //    [8]:           -8
            //    [9]:           -9
            Array<double> D = apply(empty<double>(4, 3, 2, StorageOrders.RowMajor), zeros(1), (a, b, i) => (double)i);
            Assert.IsTrue(D.Equals(counter<double>(0, 1, 4, 3, 2, StorageOrders.RowMajor)));
            //D
            //<Double>[4, 3, 2] 0...23 -
 
            //     [0]: (:,:, 0)
            //     [1]:            0           2           4
            //     [2]:            6           8          10
            //     [3]:           12          14          16
            //     [4]:           18          20          22
            //     [5]: (:,:, 1)
            //     [6]:            1           3           5
            //     [7]:            7           9          11
            //     [8]:           13          15          17
            //     [9]:           19          21          23

        }

        [TestMethod]
        public void applyIndexed_Broadcasting() {

            // assumes ArrayStyle = ILNumericsV4 -> ColumnMajor storage order
            Array<double> A = zeros(3, 4, 5);

            Array<double> C = apply<double, double, double>(A, 0.0, (a, b, i) => i);
            Assert.IsTrue(C.Equals(counter<double>(0.0, 1.0, 3, 4, 5)));
        }


        [TestMethod]
        public void applyIndexed_simpleRowMajor() {

            Array<double> A = vector(0.0, 0, 1) * ones<double>(3, 4, 5, StorageOrders.RowMajor);
            Array<double> B = vector(0.0, 1, 0) * ones<double>(3, 4, 5, StorageOrders.RowMajor);

            Array<double> C = apply(A, B, (a, b, i) => (double)i);
            Assert.IsTrue(C.Equals(counter<double>(0.0, 1.0, 3, 4, 5, StorageOrders.RowMajor)));
        }

        [TestMethod]
        public void vectorGenerator_Simple() {

            Array<double> A = vector(1000000, i => i % 3.0);
            Assert.IsTrue(A.Equals(counter<double>(0.0, 1.0, 1000000) % 3.0)); 
        }

    }
}
