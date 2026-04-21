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
    public class ArrayGetPointerTests {
        [TestMethod]
        public void ArrayGetPointerForReadTest() {

            Array<int> A = new int[,] {
                { 1, 2, 3 },
                { 4, 5, 6 }
            };
            Array<int> B = A.C;

            Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor);
            var p = A.GetHostPointerForRead();

            Assert.IsTrue(p != IntPtr.Zero); 
            unsafe
            {
                int* pA = (int*)p;
                Assert.IsTrue(pA[1] == 2);
                // illigally modifying the element will change both: A and B
                pA[1] = -99;
                Assert.IsTrue(pA[1] == -99);
                Assert.IsTrue(A.GetValue(0, 1) == -99); 
                Assert.IsTrue(B.GetValue(0, 1) == -99);

                // working the same on input array?
                InArray<int> BIn = B;
                var p2 = BIn.GetHostPointerForRead();
                int* pB = (int*)p2;

                Assert.AreEqual<IntPtr>(p, p2);
                Assert.IsTrue(pB[1] == -99);

                // illigally modifying the element will change all: A, BIn and B

                pA[1] = 55;
                Assert.IsTrue(pB[1] == 55);
                Assert.IsTrue(A.GetValue(0, 1) == 55);
                Assert.IsTrue(B.GetValue(0, 1) == 55);
                Assert.IsTrue(BIn.GetValue(0, 1) == 55);

            }

        }

        [TestMethod]
        public void ArrayGetPointerForWrite() {

            using (Scope.Enter()) {
                // create a local array: 
                Array<double> A = new double[,] {
                { 1, 2, 3 },
                { 4, 5, 6 }
            };
                // or use any other way of creating the array: 
                // Array<double> A = ILMath.zeros<double>(100,200); 
                // or from another array: 
                // Array<double> A = otherArray.C;  

                // get a pointer to the first element
                unsafe
                {
                    double* pA = (double*)A.GetHostPointerForWrite();
                    // the pointer can be used for reading AND writing
                    pA[0] = -99;

                    // A
                    // [2 x 3] <double>
                    // -99   2   3
                    //   4   5   6

                    // Watch the storage order 'A.S.StorageOrder'! To find specific elements: 
                    double el12 = pA[A.S.GetSeqIndex(1, 2)];
                    // el12: 6
                    Assert.IsTrue(el12 == 6);
                    Assert.IsTrue(A.GetValue(0) == -99);
                }
                // work with A commonly...
                GC.KeepAlive(A);  // A is hold-on to in the Scope. But only as a weak reference!!
            }
            // don't use the pointer outside this scope block!
        }

        [TestMethod]
        public unsafe void ArrayGetPointerForWriteThrowsonSharedInArray() {

            Array<double> A = 1.0;

            var AI = A.C;
            // in practice this should not happen: AI would be used + disposed immediately.
            var p = A.GetHostPointerForWrite();
            Assert.IsTrue(*(double*)p == 1.0);
            Assert.IsTrue(A == 1.0); 

        }
        [TestMethod]
        public void ArrayGetPointerForWriteDetachesOnSharedMemory() {

            Array<double> A = 1.0;
            Array<double> B = A.C;
            Assert.IsTrue(!object.ReferenceEquals(A.Storage, B.Storage)); 
            Assert.IsTrue(object.ReferenceEquals(A.Storage.m_handles, B.Storage.m_handles));
            // invalid! keep RetArray around
            var pA = A.GetHostPointerForWrite();
            Assert.IsTrue(pA != IntPtr.Zero);

            // both must be detached now

            Assert.IsTrue(A.GetValue(0) == 1.0); 
            Assert.IsTrue(B.GetValue(0) == 1.0);

            // modify A via pointer, must keep B the same
            unsafe
            {
                ((double*)pA)[0] = -5; 
            }
            Assert.IsTrue(A.GetValue(0) == -5.0);
            Assert.IsTrue(B.GetValue(0) == 1.0);

        }
    }
}
