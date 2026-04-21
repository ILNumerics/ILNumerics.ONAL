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
    public class ArrayIndexersSetTests {

        [TestMethod]
        public void MutableSetSingle1Test() {
            Array<uint> A = new uint[,] { { 1, 2, 3 }, { 4, 5, 6 } };

            A[1] = 99;

            Assert.IsTrue(A.GetValue(1) == 99); 
        }
        [TestMethod]
        public void MutableSetSingle2Test() {
            Array<uint> A = new uint[,] { { 1, 2, 3 }, { 4, 5, 6 } };

            A[1,2] = 99;

            Assert.IsTrue(A.GetValue(1,2) == 99); 
        }
        [TestMethod]
        public void MutableSetSingle1DoubleTest() {
            Array<double> A = new double[,] { { 1, 2, 3 }, { 4, 5, 6 } };

            A[4] = -99;

            Assert.IsTrue(A.GetValue(0) == 1);
            Assert.IsTrue(A.GetValue(1) == 4);
            Assert.IsTrue(A.GetValue(2) == 2);
            Assert.IsTrue(A.GetValue(3) == 5);
            Assert.IsTrue(A.GetValue(4) == -99);
            Assert.IsTrue(A.GetValue(5) == 6); 
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MutableSetNonScalarSourceErrorTest() {
            Array<uint> A = new uint[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            // scalar required on right side
            A[1] = new uint[] { 99, 100 };
             
        }

        [TestMethod]
        public void MutableSetRemove1Test() {
            Array<uint> A = new uint[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            // scalar required on right side
            A[1] = null;

            // result is a flattened (along the first dimension) array with the given sequential index removed.
            Assert.IsTrue(A.S[0] == 5); 
            Assert.IsTrue(A.S[1] == 1);
            Array<uint> R = new[] { 1, 2, 5, 3, 6 }; 
            Assert.IsTrue(A.Equals(R));

            // test BA indices path
            A = new uint[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            A[A[0]] = null;
            Assert.IsTrue(A.Equals(R));

        }
        [TestMethod]
        public void MutableSetSharedAutoDetachTest() {

            Array<uint> A = new uint[,] { { 1, 2, 3 }, { 4, 5, 6 } };

            var R = A.C;
            // R points to the same buffer set as A now. But it uses a different storage instance.

            Array<uint> B = new uint[] { 99 };
            A.SetValue(B.GetValue(0), 1);   // A's buffer (also used by R) is detached, A gets a new storage and is renamed

            Assert.IsTrue(A[1] == B);
            Array<uint> r = new uint[,] { { 1, 2, 3 }, { 99, 5, 6 } };
            Assert.IsTrue(A.Equals(r));

            r.a = new uint[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            Assert.IsTrue(R.Storage.GetAsynchReferencesCount() == 1);
            Assert.IsTrue(R.Equals(r)); 

        }
    }
}
