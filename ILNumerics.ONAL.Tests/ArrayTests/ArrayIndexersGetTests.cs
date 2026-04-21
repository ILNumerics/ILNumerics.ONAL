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
    public unsafe class ArrayIndexersGetTests {

        [TestMethod]
        public void IndexerGetMutableSimple2DimTest() {

            Array<double> A = new double[,] { { 1, 2, 3 }, { 4, 5, 6 } };

            Assert.IsTrue((double)A[0] == 1);
            Assert.IsTrue((double)A[2] == 2);
            Assert.IsTrue((double)A[4] == 3);
            Assert.IsTrue((double)A[1] == 4);
            Assert.IsTrue((double)A[3] == 5);
            Assert.IsTrue((double)A[5] == 6);

            Assert.IsTrue((double)A[0, 0] == 1);
            Assert.IsTrue((double)A[0, 1] == 2);
            Assert.IsTrue((double)A[0, 2] == 3);
            Assert.IsTrue((double)A[1, 0] == 4);
            Assert.IsTrue((double)A[1, 1] == 5);
            Assert.IsTrue((double)A[1, 2] == 6);

            Assert.IsTrue((double)A[0, 0, 0, 0, 0, 0, 0] == 1);
            Assert.IsTrue((double)A[0, 1, 0, 0, 0, 0, 0] == 2);
            Assert.IsTrue((double)A[0, 2, 0, 0, 0, 0, 0] == 3);
            Assert.IsTrue((double)A[1, 0, 0, 0, 0, 0, 0] == 4);
            Assert.IsTrue((double)A[1, 1, 0, 0, 0, 0, 0] == 5);
            Assert.IsTrue((double)A[1, 2, 0, 0, 0, 0, 0] == 6);

        }
        [TestMethod]
        public void IndexerGetImmutableSimple2DimTest() {

            InArray<double> A = new double[,] { { 1, 2, 3 }, { 4, 5, 6 } };

            Assert.IsTrue((double)A[0] == 1);
            Assert.IsTrue((double)A[2] == 2);
            Assert.IsTrue((double)A[4] == 3);
            Assert.IsTrue((double)A[1] == 4);
            Assert.IsTrue((double)A[3] == 5);
            Assert.IsTrue((double)A[5] == 6);

            Assert.IsTrue((double)A[0, 0] == 1);
            Assert.IsTrue((double)A[0, 1] == 2);
            Assert.IsTrue((double)A[0, 2] == 3);
            Assert.IsTrue((double)A[1, 0] == 4);
            Assert.IsTrue((double)A[1, 1] == 5);
            Assert.IsTrue((double)A[1, 2] == 6);

            Assert.IsTrue((double)A[0, 0, 0, 0, 0, 0, 0] == 1);
            Assert.IsTrue((double)A[0, 1, 0, 0, 0, 0, 0] == 2);
            Assert.IsTrue((double)A[0, 2, 0, 0, 0, 0, 0] == 3);
            Assert.IsTrue((double)A[1, 0, 0, 0, 0, 0, 0] == 4);
            Assert.IsTrue((double)A[1, 1, 0, 0, 0, 0, 0] == 5);
            Assert.IsTrue((double)A[1, 2, 0, 0, 0, 0, 0] == 6);

        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void IndexerGetMutableOORTest1() {
            Array<double> A = new double[,] { { 1, 2, 3 }, { 4, 5, 6 } };

            Array<double> a = A[7];
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void IndexerGetMutableOORTest2() {
            Array<double> A = new double[,] { { 1, 2, 3 }, { 4, 5, 6 } };

            Array<double> a = A[0, 7];
 
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void IndexerGetImmutableOORTest1() {
            InArray<double> A = new double[,] { { 1, 2, 3 }, { 4, 5, 6 } };

            Array<double> a = A[7];
 
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void IndexerGetImmutableOORTest2() {
            InArray<double> A = new double[,] { { 1, 2, 3 }, { 4, 5, 6 } };

            Array<double> a = A[0, 7];
 
        }

        [TestMethod]
        public void IndexerGetImmutableRetReuseTest() {
            // This test attempt(ed) to test implementation details, which (at least) 
            // partly did not exist in version 7.0. Such details are disabled below. 

            // ILN(enabled = false)
            // do not do this in your code! Use Array<T> only (no RetArray<T>)! 
            Array<double> A = new double[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            Assert.IsTrue(A.Storage.S.NumberOfElements == 6);
            var seqIdx = A.Storage.S.GetSeqIndex(0, 1);

            var a = A[0, 1]; // this should create a new storage but may reuse the same buffer set

            //Assert.IsFalse(object.ReferenceEquals(a, A)); // implementation detail

            Assert.IsTrue(a.Storage.S.NumberOfElements == 1);
            Assert.IsTrue(a.Storage.S.NumberOfElements == 1);
            Assert.IsTrue(a.Storage.GetValue(0) == 2);

            // the array returned has its base offset adjusted
            // this tests Size.SetScalar(..)
            Assert.IsTrue((a.Storage.Size.Flags & Size.CONT_FLAG) != 0);
            Assert.IsTrue(a.Storage.Size.StorageOrder == Settings.DefaultStorageOrder);
            
            var bsd = a.Storage.S.GetBSD(false);
            Assert.IsTrue(bsd[0] == (Settings.MinNumberOfArrayDimensions));
            Assert.IsTrue(bsd[1] == (1));
            Assert.IsTrue(bsd[2] == (seqIdx));
            for (int i = 0; i < Settings.MinNumberOfArrayDimensions; i++) {
                Assert.IsTrue(bsd[3 + i] == (1));
                Assert.IsTrue(bsd[3 + Settings.MinNumberOfArrayDimensions + i] == (0));
            }
            // ILN(enabled = true)
        }
        [TestMethod]
        public void IndexerGetImmutableRetNonReuseTest() {
            // ILN(enabled = false)
            Array<double> A = new double[,] { { 1, 2, 3 }, { 4, 5, 6 } };

            var a = A.C[0, 1]; 

            Assert.IsFalse(object.ReferenceEquals(a, A));
            Assert.IsTrue(a is Array<double>); 

            Assert.IsTrue(A.Storage.S.NumberOfElements == 6);
            Assert.IsTrue(a.Storage.S.NumberOfElements == 1);
            Assert.IsTrue(a.Storage.GetValue(0) == 2);

            // the array returned has its base offset adjusted
            // this tests Size.SetScalar(..)
            Assert.IsTrue((a.Storage.Size.Flags & Size.CONT_FLAG) != 0);
            Assert.IsTrue(a.Storage.Size.StorageOrder == Settings.DefaultStorageOrder);

            var bsd = a.Storage.S.GetBSD(false);
            Assert.IsTrue(bsd[0] == (Settings.MinNumberOfArrayDimensions));
            Assert.IsTrue(bsd[1] == (1));
            Assert.IsTrue(bsd[2] == (1));
            for (int i = 0; i < Settings.MinNumberOfArrayDimensions; i++) {
                Assert.IsTrue(bsd[3 + i] == (1));
                Assert.IsTrue(bsd[3 + Settings.MinNumberOfArrayDimensions + i] == (0));
            }
            // ILN(enabled = true)
        }

        [TestMethod]
        public void IndexerGetLogical1Test() {
            Logical A = new bool[,] { { true, false, false, true, true }, { true, false, false, true, true } };

            Assert.IsTrue(A[0]); 
            Assert.IsFalse(A[1, 1]); 
            Assert.IsTrue(A[0, 3]); 
            Assert.IsTrue(A[0, 3, 0, 0, 0, 0]); 

        }
    }
}
