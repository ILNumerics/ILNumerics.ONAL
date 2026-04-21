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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using static ILNumerics.ILMath;

namespace ILNumerics.Core.UnitTests.numpy_attributes_Tests {
    [TestClass]
    public class astypeTests {
        [TestMethod]
        public void numpy_astype_simple() {
            Array<double> A = counter<double>(1, 1, 5, 4, 3);

            Assert.IsTrue(A.astype<float>().Equals(counter<float>(1, 1, 5, 4, 3)));
            Assert.IsTrue(A.astype<ushort>().Equals(counter<ushort>(1, 1, 5, 4, 3)));
            Assert.IsTrue(A.astype<short>().Equals(counter<short>(1, 1, 5, 4, 3)));
            Assert.IsTrue(A.astype<int>().Equals(counter<int>(1, 1, 5, 4, 3)));
            Assert.IsTrue(A.astype<uint>().Equals(counter<uint>(1, 1, 5, 4, 3)));
            Assert.IsTrue(A.astype<byte>().Equals(counter<byte>(1, 1, 5, 4, 3)));
            Assert.IsTrue(A.astype<sbyte>().Equals(counter<sbyte>(1, 1, 5, 4, 3)));
            Assert.IsTrue(A.astype<long>().Equals(counter<long>(1, 1, 5, 4, 3)));
            Assert.IsTrue(A.astype<ulong>().Equals(counter<ulong>(1, 1, 5, 4, 3)));
            Assert.IsTrue(A.astype<complex>().Equals(ccomplex(counter<double>(1, 1, 5, 4, 3),0))); 
            Assert.IsTrue(A.astype<fcomplex>().Equals(ccomplex(counter<float>(1, 1, 5, 4, 3),0))); 

        }
        [TestMethod]
        public void numpy_astype_truncate() {
            Array<double> A = vector(1.0, 2.0, 3.0, 4.5, 4.4, 4.6);
            Array<short> B = A.astype<short>();
            Assert.IsTrue(B.Equals(vector<short>(1, 2, 3, 4, 4, 4))); 
        }

        [TestMethod]
        public void numpy_astype_order() {

            Array<double> A = counter<double>(1, 1, 5, 4, 3);

            Assert.IsTrue(A.astype<float>(StorageOrders.RowMajor).Equals(counter<float>(1, 1, 5, 4, 3)));
            Assert.IsTrue(A.astype<ushort>(StorageOrders.RowMajor).Equals(counter<ushort>(1, 1, 5, 4, 3)));
            Assert.IsTrue(A.astype<short>(StorageOrders.RowMajor).Equals(counter<short>(1, 1, 5, 4, 3)));
            Assert.IsTrue(A.astype<int>(StorageOrders.RowMajor).Equals(counter<int>(1, 1, 5, 4, 3)));
            Assert.IsTrue(A.astype<uint>(StorageOrders.RowMajor).Equals(counter<uint>(1, 1, 5, 4, 3)));
            Assert.IsTrue(A.astype<byte>(StorageOrders.RowMajor).Equals(counter<byte>(1, 1, 5, 4, 3)));
            Assert.IsTrue(A.astype<sbyte>(StorageOrders.RowMajor).Equals(counter<sbyte>(1, 1, 5, 4, 3)));
            Assert.IsTrue(A.astype<long>(StorageOrders.RowMajor).Equals(counter<long>(1, 1, 5, 4, 3)));
            Assert.IsTrue(A.astype<ulong>(StorageOrders.RowMajor).Equals(counter<ulong>(1, 1, 5, 4, 3)));
            Assert.IsTrue(A.astype<complex>(StorageOrders.RowMajor).Equals(ccomplex(counter<double>(1, 1, 5, 4, 3), 0)));
            Assert.IsTrue(A.astype<fcomplex>(StorageOrders.RowMajor).Equals(ccomplex(counter<float>(1, 1, 5, 4, 3), 0)));

            Assert.IsTrue(A.astype<float>(StorageOrders.RowMajor).S.StorageOrder == StorageOrders.RowMajor);
            Assert.IsTrue(A.astype<ushort>(StorageOrders.RowMajor).S.StorageOrder == StorageOrders.RowMajor);
            Assert.IsTrue(A.astype<short>(StorageOrders.RowMajor).S.StorageOrder == StorageOrders.RowMajor);
            Assert.IsTrue(A.astype<int>(StorageOrders.RowMajor).S.StorageOrder == StorageOrders.RowMajor);
            Assert.IsTrue(A.astype<uint>(StorageOrders.RowMajor).S.StorageOrder == StorageOrders.RowMajor);
            Assert.IsTrue(A.astype<byte>(StorageOrders.RowMajor).S.StorageOrder == StorageOrders.RowMajor);
            Assert.IsTrue(A.astype<sbyte>(StorageOrders.RowMajor).S.StorageOrder == StorageOrders.RowMajor);
            Assert.IsTrue(A.astype<long>(StorageOrders.RowMajor).S.StorageOrder == StorageOrders.RowMajor);
            Assert.IsTrue(A.astype<ulong>(StorageOrders.RowMajor).S.StorageOrder == StorageOrders.RowMajor);
            Assert.IsTrue(A.astype<complex>(StorageOrders.RowMajor).S.StorageOrder == StorageOrders.RowMajor);
            Assert.IsTrue(A.astype<fcomplex>(StorageOrders.RowMajor).S.StorageOrder == StorageOrders.RowMajor);

        }

        [TestMethod]
        public void numpy_astype_empyScalar() {

            using (Settings.Ensure(()=> Settings.ArrayStyle, ArrayStyles.numpy)) {
                Array<uint> A = 1;
                Array<double> B = A.astype<double>();
                Assert.IsTrue(B.S.NumberOfDimensions == 0); 
                Assert.IsTrue(B.S.NumberOfElements == 1);
                Assert.IsTrue(B == 1);

                Array<byte> C = empty<byte>(2, 1, 0, 2);
                Array<complex> D = C.astype<complex>(StorageOrders.RowMajor);
                Assert.IsTrue(D.IsEmpty); 
                Assert.IsTrue(D.S[0] == 2);
                Assert.IsTrue(D.S[1] == 1);
                Assert.IsTrue(D.S[2] == 0);
                Assert.IsTrue(D.S[3] == 2);
                Assert.IsTrue(D.S.NumberOfDimensions == 4);
                Assert.IsTrue(D.S.NumberOfElements == 0);

                C = empty<byte>(0);
                Array<fcomplex> E = C.astype<fcomplex>(StorageOrders.RowMajor);
                Assert.IsTrue(E.IsEmpty);
                Assert.IsTrue(E.S[0] == 0);
                Assert.IsTrue(E.S[1] == 1);
                Assert.IsTrue(E.S[2] == 1);
                Assert.IsTrue(E.S[3] == 1);
                Assert.IsTrue(E.S.NumberOfDimensions == 1);
                Assert.IsTrue(E.S.NumberOfElements == 0);
            }
        }
    }
}
