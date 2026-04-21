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

#pragma warning disable CS0169
//ILN(enabled=false)
namespace ILNumerics.Core.Tests {
    [TestClass]
    public class ReinterpreteCast_Tests {

        [TestMethod]
        public void reinterpret_castExpandTests001() {

            Array<int> A = ones<int>(2, 3, StorageOrders.ColumnMajor);
            Array<byte> R = reinterpret_cast<int, byte>(A);

            Assert.IsTrue(R.S.NumberOfDimensions == 3);
            Assert.IsTrue(R.S.NumberOfElements == 24);

            Assert.IsTrue(R.S[0] == 2);
            Assert.IsTrue(R.S[1] == 3);
            Assert.IsTrue(R.S[2] == 4);

            Assert.IsTrue(allall(R[full, full, 0] == ones<byte>(2, 3)));
            Assert.IsTrue(allall(R[full, full, 1] == zeros<byte>(2, 3)));
            Assert.IsTrue(allall(R[full, full, 2] == zeros<byte>(2, 3)));
            Assert.IsTrue(allall(R[full, full, 3] == zeros<byte>(2, 3)));

        }

        [TestMethod]
        public void reinterpret_castCompressTests() {

            Array<int> A = ones<int>(4, 3, StorageOrders.ColumnMajor);
            Array<long> R = reinterpret_cast<int, long>(A);

            Assert.IsTrue(R.S.NumberOfDimensions == 2);
            Assert.IsTrue(R.S.NumberOfElements == 6);

            Assert.IsTrue(R.S[0] == 2);
            Assert.IsTrue(R.S[1] == 3);

            Assert.IsTrue(allall(R == ones<long>(2, 3) * 0x0000000100000001));

        }

        [TestMethod]
        public void reinterpret_castEqualSizeTests() {

            Array<int> A = ones<int>(4, 3, StorageOrders.ColumnMajor) * -1;
            Array<uint> R = reinterpret_cast<int, uint>(A);

            Assert.IsTrue(R.S.NumberOfDimensions == 2);
            Assert.IsTrue(R.S.NumberOfElements == 12);

            Assert.IsTrue(R.S[0] == 4);
            Assert.IsTrue(R.S[1] == 3);

            Assert.IsTrue(allall(R == ones<uint>(4, 3) * 0xffffffff));

        }

        [TestMethod]
        public void reinterpret_castComplexRoundtripTest() {

            Array<double> A = rand(10, 20, 2, StorageOrders.RowMajor);  // forces complex compression along 3rd dimension
            Array<complex> C = reinterpret_cast<double, complex>(A);

            Array<double> R_ = reinterpret_cast<complex, double>(squeeze(C));

            Assert.IsTrue(allall(R_ == A));

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "should throw due to: continous dimension #0 length is not even / divisable by 2")]
        public void ReinterpreteWrongDimensionLengthTest() {

            Array<float> A = ones<float>(5, 4, StorageOrders.ColumnMajor);
            Array<double> R = reinterpret_cast<float, double>(A); //throws: continues dimension #0 length is not even / divisable by 2 
 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "should throw due to: no 1-strided dimension found")]
        public void ReinterpreteWrongStrideTest() {

            Array<int> A = arange<int>(1, 100).Reshape(20, 5)[r(1, 2, 7), full];
            Array<long> R = reinterpret_cast<int, long>(A); // throws: no 1-strided dimension

        }

        struct ThreeBytes {
            byte byte1;
            byte byte2;
            byte byte3;
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "should throw due to: incompatible element type lengths")]
        public void ReinterpreteWrongElementTypesTest() {
            // ??? not possible ? 
            Array<double> A = ones<double>(10, 10, StorageOrders.ColumnMajor);
            Array<ThreeBytes> B = reinterpret_cast<double, ThreeBytes>(A);

        }

    }
}
