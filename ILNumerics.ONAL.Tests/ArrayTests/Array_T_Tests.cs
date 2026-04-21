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
using ILNumerics.Core.Functions.Builtin;

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public class Array_T_Tests {
        [TestMethod]
        public void Array_T_NPscalar() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = -1;
                Assert.IsTrue(A.T == -1);
                Assert.IsTrue(A.T.IsScalar);
                Assert.IsTrue(A.T.S.NumberOfDimensions == 0);
                Assert.IsTrue(A.T.S.NumberOfElements == 1); 
                Assert.IsTrue(ReferenceEquals(A.T.Storage.GetHandlesUnsafe(), A.Storage.GetHandlesUnsafe()));
                Assert.IsTrue(ReferenceEquals(A.T.Storage.GetHandlesUnsafe(), A.T.Storage.GetHandlesUnsafe()));

                A = new double[] { 1, 2, 3 };
                Assert.IsTrue(A.T.GetValue(0) == 1);
                Assert.IsTrue(A.T.GetValue(1) == 2);
                Assert.IsTrue(A.T.GetValue(2) == 3);
                Assert.IsTrue(A.T.IsVector);
                Assert.IsTrue(A.T.S.NumberOfDimensions == 1);
                Assert.IsTrue(A.T.S.NumberOfElements == 3);
                Assert.IsTrue(ReferenceEquals(A.T.Storage.GetHandlesUnsafe(), A.Storage.GetHandlesUnsafe()));
                Assert.IsTrue(ReferenceEquals(A.T.Storage.GetHandlesUnsafe(), A.T.Storage.GetHandlesUnsafe()));

            }
        }

        [TestMethod]
        public void Array_T_MLscalar() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = -1;
                Assert.IsTrue(A.T == -1);
                Assert.IsTrue(A.T.IsScalar);
                Assert.IsTrue(A.T.S.NumberOfDimensions == 2);
                Assert.IsTrue(A.T.S.NumberOfElements == 1);
                Assert.IsTrue(ReferenceEquals(A.T.Storage.GetHandlesUnsafe(), A.Storage.GetHandlesUnsafe()));
                Assert.IsTrue(ReferenceEquals(A.T.Storage.GetHandlesUnsafe(), A.T.Storage.GetHandlesUnsafe()));

                A = new double[] { 1, 2, 3 };
                Assert.IsTrue(A.T.GetValue(0) == 1);
                Assert.IsTrue(A.T.GetValue(1) == 2);
                Assert.IsTrue(A.T.GetValue(2) == 3);
                Assert.IsTrue(A.T.IsVector);
                Assert.IsTrue(A.T.S.NumberOfDimensions == 2);
                Assert.IsTrue(A.T.S.NumberOfElements == 3);
                Assert.IsFalse(ReferenceEquals(A.T.Storage, A.Storage));
                Assert.IsTrue(ReferenceEquals(A.T.Storage.GetHandlesUnsafe(), A.T.Storage.GetHandlesUnsafe()));

            }
        }
        [TestMethod]
        public void Array_T_2DVector() {

            Array<double> A = new double[,] { { 1, 2, 3 } };
            Assert.IsTrue(A.IsRowVector);

            Assert.IsTrue(A.T.GetValue(0) == 1);
            Assert.IsTrue(A.T.GetValue(1) == 2);
            Assert.IsTrue(A.T.GetValue(2) == 3);
            Assert.IsTrue(A.T.IsColumnVector);
            Assert.IsTrue(A.T.S.NumberOfDimensions == 2);
            Assert.IsTrue(A.T.S.NumberOfElements == 3);
            Assert.IsFalse(ReferenceEquals(A.T.Storage, A.Storage));
            Assert.IsFalse(ReferenceEquals(A.T.Storage, A.T.Storage));
            Assert.IsTrue(ReferenceEquals(A.T.Storage.m_handles, A.T.Storage.m_handles));

        }
        [TestMethod]
        public void Array_T_ML_3DArray() {

            Array<double> A = MathInternal.counter<double>(1.0, 1.0, 4, 3, 2);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);

            Array<double> Res = new double[] { 1, 5, 9, 13, 17, 21, 2, 6, 10, 14, 18, 22, 3, 7, 11, 15, 19, 23, 4, 8, 12, 16, 20, 24 };
            Res.a = Res.Reshape(3, 2, 4);
            Assert.IsTrue(A.T.Equals(Res));
            Assert.IsTrue(A.T.S.IsContinuous);
            Assert.IsTrue(A.T.S.StorageOrder != StorageOrders.ColumnMajor && A.T.S.StorageOrder != StorageOrders.RowMajor);
            Assert.IsTrue(A.T.T.S.IsContinuous);
            Assert.IsTrue(A.T.T.T.S.StorageOrder == StorageOrders.ColumnMajor);

            Assert.IsFalse(ReferenceEquals(A.T.Storage, A.Storage));
            Assert.IsFalse(ReferenceEquals(A.T.Storage, A.T.Storage));
            Assert.IsTrue(ReferenceEquals(A.T.Storage.m_handles, A.T.Storage.m_handles));

        }
        [TestMethod]
        public void Array_T_NP_3DArray() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = MathInternal.counter<double>(1.0, 1.0, 4, 3, 2);
                Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);

                Array<double> Res = A.C;
                Res.S.SwapDimensions();

                Assert.IsTrue(A.T.Equals(Res));
                Assert.IsTrue(A.T.S.IsContinuous);
                Assert.IsTrue(A.T.S.StorageOrder == StorageOrders.RowMajor);
                Assert.IsTrue(A.T.T.S.IsContinuous);
                Assert.IsTrue(A.T.T.S.StorageOrder == StorageOrders.ColumnMajor);

                Assert.IsFalse(ReferenceEquals(A.T.Storage, A.Storage));
                Assert.IsFalse(ReferenceEquals(A.T.Storage, A.T.Storage));
                Assert.IsTrue(ReferenceEquals(A.T.Storage.m_handles, A.T.Storage.m_handles));

            }
        }
    }
}
