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
using ILNumerics.Core.StorageLayer;
using static ILNumerics.ILMath;
using System.Security;
using static ILNumerics.Globals;

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public class BoolIndexIteratorTests {
        [TestMethod]
        [SecuritySafeCritical]
        public unsafe void BoolIndexIteratorTest_Simple() {
            Logical A = counter(1.0, 1.0, 5, 4) % 2 == 0;

            var bsd = A.S.GetBSD(false);
            var it = A.IndexIterator(A.S.NumberOfElements, false);

            Assert.IsTrue(it.Current == -1);
            for (int i = 0; i < 10; i++) {
                Assert.IsTrue(it.MoveNext());
                Assert.IsTrue(it.Current == i * 2 + 1);
            }
            Assert.IsFalse(it.MoveNext());
            Assert.IsTrue(it.Current == 19);
            it.Reset();
            Assert.IsTrue(it.Current == -1);
        }
        [TestMethod]
        public unsafe void BoolIndexIteratorTest_Strided() {
            Logical A = (counter(1.0, 1.0, 5, 4).T % 2 == 0)[r(1, end), full];

            var it = A.IndexIterator(lastDimensionIdx: A.S.NumberOfElements);

            Assert.IsTrue(it.Current == -1);
            for (int i = 0; i < 8; i++) {
                Assert.IsTrue(it.MoveNext());
                Assert.IsTrue(it.Current == i * 2);
            }
            Assert.IsFalse(it.MoveNext());
            Assert.IsTrue(it.Current == 14);  // undefined!!
            it.Reset();
            Assert.IsTrue(it.Current == -1);
        }
        [TestMethod]
        public unsafe void BoolIndexIteratorTest_empty() {
            Logical A = (counter(1.0, 1.0, 5, 4).T < 0)[slice(0,0),full];

            var it = A.IndexIterator(lastDimensionIdx: A.S.NumberOfElements - 1);

            Assert.IsTrue(it.Current == -1);
            Assert.IsFalse(it.MoveNext());
            Assert.IsTrue(it.Current == -1);  // undefined!!
            it.Reset();
            Assert.IsTrue(it.Current == -1);
        }
        [TestMethod]
        public unsafe void BoolIndexIteratorTest_npScalar() {
            Logical A = true;

            var it = A.IndexIterator(lastDimensionIdx: A.S.NumberOfElements - 1);

            Assert.IsTrue(it.Current == -1);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 0);
            Assert.IsFalse(it.MoveNext());
            Assert.IsTrue(it.Current == 0); // undefined! 
            it.Reset();
            Assert.IsTrue(it.Current == -1);
        }
        [TestMethod]
        public unsafe void BoolIndexIteratorTest_npScalarFalse() {
            Logical A = false;

            var it = A.IndexIterator(lastDimensionIdx: A.S.NumberOfElements - 1);

            Assert.IsTrue(it.Current == -1);
            Assert.IsFalse(it.MoveNext());
            // Assert.IsTrue(it.Current == -1); UNDEFINED!! After MoveNext() returns false, Current may stay somewhere (here: the last inspected element).
            it.Reset();
            Assert.IsTrue(it.Current == -1);
        }
    }
}