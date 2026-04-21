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
    public unsafe class ArrayIsStateTests {

        [TestMethod]
        public void ArrayIsStateTestEmptyShort() {

            Array<short> A = new short[0];

            Assert.IsTrue(A.IsColumnVector == true);
            Assert.IsTrue(A.IsComplex == false);
            Assert.IsTrue(A.IsEmpty == true);
            Assert.IsTrue(A.IsMatrix == (Settings.MinNumberOfArrayDimensions > 1));
            Assert.IsTrue(A.IsNumeric == true);
            Assert.IsTrue(A.IsOfType<short>() == true);
            Assert.IsTrue(A.IsOfType<double>() == false);
            Assert.IsTrue(A.IsRowVector == false);
            Assert.IsTrue(A.IsScalar == false);
            Assert.IsTrue(A.IsVector == true);

            A = new short[,] { { } }; // [1,0]
            Assert.IsTrue(A.IsRowVector == true);

        }
        [TestMethod]
        public void ArrayIsStateTestScalarUInt() {

            Array<uint> A = new uint[1];

            Assert.IsTrue(A.IsColumnVector == true);
            Assert.IsTrue(A.IsComplex == false);
            Assert.IsTrue(A.IsEmpty == false);
            Assert.IsTrue(A.IsMatrix == (Settings.MinNumberOfArrayDimensions > 1));
            Assert.IsTrue(A.IsNumeric == true);
            Assert.IsTrue(A.IsOfType<uint>() == true);
            Assert.IsTrue(A.IsOfType<double>() == false);
            Assert.IsTrue(A.IsOfType<complex>() == false);
            Assert.IsTrue(A.IsRowVector == true);
            Assert.IsTrue(A.IsScalar == true);
            Assert.IsTrue(A.IsVector == true);
        }
        [TestMethod]
        public void ArrayIsStateTestColVectorInt() {

            Array<int> A = new int[2] { 1, 2 };
            Assert.IsTrue(A.S[0] == 2 && A.S[1] == 1);

            Assert.IsTrue(A.IsColumnVector == true);
            Assert.IsTrue(A.IsComplex == false);
            Assert.IsTrue(A.IsEmpty == false);
            Assert.IsTrue(A.IsMatrix == (Settings.MinNumberOfArrayDimensions > 1));
            Assert.IsTrue(A.IsNumeric == true);
            Assert.IsTrue(A.IsOfType<int>() == true);
            Assert.IsTrue(A.IsOfType<double>() == false);
            Assert.IsTrue(A.IsOfType<complex>() == false);
            Assert.IsTrue(A.IsRowVector == false);
            Assert.IsTrue(A.IsScalar == false);
            Assert.IsTrue(A.IsVector == true);
        }
        [TestMethod]
        public void ArrayIsStateTestRowVectorUShort() {

            Array<ushort> A = new ushort[1, 2] { { 1, 2 } };

            Assert.IsTrue(A.S[1] == 2 && A.S[0] == 1);

            Assert.IsTrue(A.IsColumnVector == false);
            Assert.IsTrue(A.IsComplex == false);
            Assert.IsTrue(A.IsEmpty == false);
            Assert.IsTrue(A.IsMatrix == (Settings.MinNumberOfArrayDimensions > 1));
            Assert.IsTrue(A.IsNumeric == true);
            Assert.IsTrue(A.IsOfType<ushort>() == true);
            Assert.IsTrue(A.IsOfType<double>() == false);
            Assert.IsTrue(A.IsOfType<complex>() == false);
            Assert.IsTrue(A.IsRowVector == true);
            Assert.IsTrue(A.IsScalar == false);
            Assert.IsTrue(A.IsVector == true);
        }

        [TestMethod]
        public void ArrayIsStateTestMatrixClass() {

            Array<RefTest> A = new RefTest[3 * 2];
            // do the reshape illegally here. Since we don't have reshape yet...
            var bsd = A.S.GetBSD();
            bsd[3] = (3);
            bsd[4] = (2);
            bsd[5] = (1);
            bsd[6] = (3);

            Assert.IsTrue(A.S[0] == 3 && A.S[1] == 2);

            Assert.IsTrue(A.IsColumnVector == false);
            Assert.IsTrue(A.IsComplex == false);
            Assert.IsTrue(A.IsEmpty == false);
            Assert.IsTrue(A.IsMatrix == true);
            Assert.IsTrue(A.IsNumeric == false);
            Assert.IsTrue(A.IsOfType<ushort>() == false);
            Assert.IsTrue(A.IsOfType<double>() == false);
            Assert.IsTrue(A.IsOfType<RefTest>() == true);
            Assert.IsTrue(A.IsRowVector == false);
            Assert.IsTrue(A.IsScalar == false);
            Assert.IsTrue(A.IsVector == false);
        }

    }
}
