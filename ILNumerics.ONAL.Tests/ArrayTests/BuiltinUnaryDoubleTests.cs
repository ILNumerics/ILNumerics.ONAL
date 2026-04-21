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
    public unsafe class BuiltinUnaryDoubleTests {

        [TestMethod]
        public void UnaryDoubleTestContinous32RMaj() {

            double[] values = new double[0], dummy = null;
            Array<double> A = Helper.generateSystemArray<double>(17, 2, ref values, ref dummy);

            Array<double> B = MathInternal.sin(A); 

            Assert.IsTrue(B.S.StorageOrder == A.S.StorageOrder);
            Assert.IsTrue(B.S.StorageOrder == StorageOrders.RowMajor);
            Assert.IsTrue(B.S.IsSameShape(A.S));

            //Assert.IsTrue(!object.Equals(B.Storage, A.Storage));
            //Assert.IsTrue(!object.Equals(B.Storage.m_handles, A.Storage.m_handles)); 

            double[] res = new double[B.S.NumberOfElements];
            B.ExportValues(ref res, StorageOrders.RowMajor);

            double[] correct = new double[B.S.NumberOfElements];
            for (int i = 0; i < correct.Length; i++) {
                correct[i] = Math.Sin(values[i]); 
            }
            ArrayAssert.ValuesEqual(res, correct);
        }

        [TestMethod]
        public void UnaryDoubleTestContinous32CMaj() {

            double[] values = new double[0], dummy = null;
            Array<double> A = Helper.generateSystemArray<double>(17, 2, ref values, ref dummy);
            // transpose A
            A.S.GetShifted(1, A.S.GetBSD(true));
            Assert.IsTrue(A.S[0] == 2 && A.S[1] == 17);

            Array<double> B = MathInternal.sin(A);

            Assert.IsTrue(B.S.StorageOrder == A.S.StorageOrder);
            Assert.IsTrue(B.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(B.S.IsSameShape(A.S));

            //Assert.IsTrue(!object.Equals(B.Storage, A.Storage));
            //Assert.IsTrue(!object.Equals(B.Storage.m_handles, A.Storage.m_handles));

            double[] res = new double[B.S.NumberOfElements];
            B.ExportValues(ref res, StorageOrders.ColumnMajor);

            double[] correct = new double[B.S.NumberOfElements];
            for (int i = 0; i < correct.Length; i++) {
                correct[i] = Math.Sin(values[i]);
            }
            ArrayAssert.ValuesEqual(res, correct);
        }

        [TestMethod]
        public void InplaceRMaj() {

            double[] values = new double[0], dummy = null;
            Array<double> A = Helper.generateSystemArray<double>(17, 2, ref values, ref dummy);

            var AR = A.C;
            A.Release();
            var oldHandles = AR.Storage.m_handles;
            var oldSize = AR.Storage.S;
            var oldStorage = AR.Storage;

            var oldRefCount = AR.Storage.ReferenceCount; 
            Array<double> B = MathInternal.sin(AR);

            // Assert.IsTrue(!AR.Storage.IsDisposed);  // is disposed
            // Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, oldHandles));  // inplace?

            Assert.IsTrue(B.S.StorageOrder == StorageOrders.RowMajor);
            Assert.IsTrue(B.S.IsSameShape(oldSize));

            //Assert.IsTrue(object.Equals(B.Storage, oldStorage));

            double[] res = new double[B.S.NumberOfElements];
            B.ExportValues(ref res, StorageOrders.RowMajor);

            double[] correct = new double[B.S.NumberOfElements];
            for (int i = 0; i < correct.Length; i++) {
                correct[i] = Math.Sin(values[i]);
            }
            ArrayAssert.ValuesEqual(res, correct);

        }
        [TestMethod]
        public void InplaceRMajMultiThr() {
            double[] values = new double[0], dummy = null;
            Array<double> A = Helper.generateSystemArray<double>(519, 501, ref values, ref dummy);
            var oldThreadsSetting = Settings.MaxNumberThreads;
            try {
                Settings.MaxNumberThreads = 2; 
                Assert.IsTrue(Settings.MaxNumberThreads > 1);

                var AR = A.C;
                A.Release();
                var oldHandles = AR.Storage.m_handles;
                var oldSize = AR.Storage.S;
                var oldStorage = AR.Storage;

                var oldRefCount = AR.Storage.ReferenceCount;
                Array<double> B = MathInternal.sin(AR);


                Assert.IsTrue(B.S.StorageOrder == StorageOrders.RowMajor);
                Assert.IsTrue(B.S.IsSameShape(oldSize));

                //Assert.IsTrue(object.Equals(B.Storage, oldStorage));

                double[] res = new double[B.S.NumberOfElements];
                B.ExportValues(ref res, StorageOrders.RowMajor);

                double[] correct = new double[B.S.NumberOfElements];
                for (int i = 0; i < correct.Length; i++) {
                    correct[i] = Math.Sin(values[i]);
                }
                ArrayAssert.ValuesEqual(res, correct);
            } finally {
                Settings.MaxNumberThreads = oldThreadsSetting; 
            }
        }
        [TestMethod]
        public void InplaceCMaj() {
            double[] values = new double[0], dummy = null;
            Array<double> A = Helper.generateSystemArray<double>(17, 2, ref values, ref dummy);
            A.S.GetShifted(1, A.S.GetBSD(true)); // transpose inplace

            var AR = A.C;
            A.Release();
            var oldHandles = AR.Storage.m_handles;
            var oldSize = AR.Storage.S;
            var oldStorage = AR.Storage;

            // V6 disabled. was: this ret array will be used in-place by Sin
            var refCountBefore = AR.Storage.ReferenceCount; 
            Array<double> B = MathInternal.sin(AR);


            // V6: don't imply internal implementation details!
            //Assert.IsTrue(AR.Storage.IsDisposed);  // is disposed
            //Assert.IsTrue(!object.ReferenceEquals(B.Storage.m_handles, oldHandles));  // inplace?

            Assert.IsTrue(B.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(B.S.IsSameShape(oldSize));

            //Assert.IsTrue(object.Equals(B.Storage, oldStorage));

            double[] res = new double[B.S.NumberOfElements];
            B.ExportValues(ref res, StorageOrders.ColumnMajor);

            double[] correct = new double[B.S.NumberOfElements];
            for (int i = 0; i < correct.Length; i++) {
                correct[i] = Math.Sin(values[i]);
            }
            ArrayAssert.ValuesEqual(res, correct);

        }
        [TestMethod]
        public void OOP3dimContMultiThread2() {

            double[] values = new double[0], dummy = null;
            Array<double> A = Helper.generateSystemArray<double>(200, 2, ref values, ref dummy);
            var bsd = A.S.GetBSD(true);
            bsd[0] = (3);
            bsd[3] = (100);
            bsd[4] = (2); 
            bsd[5] = (2); 
            bsd[6] = (4); 
            bsd[7] = (2); 
            bsd[8] = (1);
            // 200, 2 (2, 1) -> 100, 2, 2 (4, 2, 1)
            // CM: 
            // 1, 100, 200
            // RM: 
            // 4, 2, 1

            Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor);
            uint oldThreads = Settings.MaxNumberThreads;
            try {
                Settings.MaxNumberThreads = 2;

                Array<double> B = MathInternal.sin(A);

                Assert.IsTrue(B.S.StorageOrder == A.S.StorageOrder);
                Assert.IsTrue(B.S.StorageOrder == StorageOrders.RowMajor);
                Assert.IsTrue(B.S.IsSameShape(A.S));

                Assert.IsTrue(!object.Equals(B.Storage, A.Storage));
                Assert.IsTrue(!object.Equals(B.Storage.m_handles, A.Storage.m_handles));

                double[] res = new double[B.S.NumberOfElements];
                B.ExportValues(ref res, StorageOrders.RowMajor);

                double[] correct = new double[B.S.NumberOfElements];
                for (int i = 0; i < correct.Length; i++) {
                    correct[i] = Math.Sin(values[i]);
                }
                ArrayAssert.ValuesEqual(res, correct);
            } finally {
                Settings.MaxNumberThreads = oldThreads;
            }
        }

        [TestMethod]
        public void OOP3dimContMultiThread3() {

            double[] values = new double[0], dummy = null;
            Array<double> A = Helper.generateSystemArray<double>(200, 2, ref values, ref dummy);
            var bsd = A.S.GetBSD(true);
            bsd[0] = (3);
            bsd[3] = (100);
            bsd[4] = (2);
            bsd[5] = (2);
            bsd[6] = (4);
            bsd[7] = (2);
            bsd[8] = (1);
            // 200, 2 (2, 1) -> 100, 2, 2 (4, 2, 1)
            // CM: 
            // 1, 100, 200
            // RM: 
            // 4, 2, 1

            Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor);
            uint oldThreads = Settings.MaxNumberThreads;
            try {
                Settings.MaxNumberThreads = 3;

                Array<double> B = MathInternal.sin(A);

                Assert.IsTrue(B.S.StorageOrder == A.S.StorageOrder);
                Assert.IsTrue(B.S.StorageOrder == StorageOrders.RowMajor);
                Assert.IsTrue(B.S.IsSameShape(A.S));

                Assert.IsTrue(!object.Equals(B.Storage, A.Storage));
                Assert.IsTrue(!object.Equals(B.Storage.m_handles, A.Storage.m_handles));

                double[] res = new double[B.S.NumberOfElements];
                B.ExportValues(ref res, StorageOrders.RowMajor);

                double[] correct = new double[B.S.NumberOfElements];
                for (int i = 0; i < correct.Length; i++) {
                    correct[i] = Math.Sin(values[i]);
                }
                ArrayAssert.ValuesEqual(res, correct);
            } finally {
                Settings.MaxNumberThreads = oldThreads;
            }
        }
        [TestMethod]
        public void OOP3dimContMultiThread5() {

            double[] values = new double[0], dummy = null;
            Array<double> A = Helper.generateSystemArray<double>(200, 2, ref values, ref dummy);
            var bsd = A.S.GetBSD(true);
            bsd[0] = (3);
            bsd[3] = (100);
            bsd[4] = (2);
            bsd[5] = (2);
            bsd[6] = (4);
            bsd[7] = (2);
            bsd[8] = (1);
            // 200, 2 (2, 1) -> 100, 2, 2 (4, 2, 1)
            // CM: 
            // 1, 100, 200
            // RM: 
            // 4, 2, 1

            Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor);
            uint oldThreads = Settings.MaxNumberThreads;
            try {
                Settings.MaxNumberThreads = 5;

                Array<double> B = MathInternal.sin(A);

                Assert.IsTrue(B.S.StorageOrder == A.S.StorageOrder);
                Assert.IsTrue(B.S.StorageOrder == StorageOrders.RowMajor);
                Assert.IsTrue(B.S.IsSameShape(A.S));

                Assert.IsTrue(!object.Equals(B.Storage, A.Storage));
                Assert.IsTrue(!object.Equals(B.Storage.m_handles, A.Storage.m_handles));

                double[] res = new double[B.S.NumberOfElements];
                B.ExportValues(ref res, StorageOrders.RowMajor);

                double[] correct = new double[B.S.NumberOfElements];
                for (int i = 0; i < correct.Length; i++) {
                    correct[i] = Math.Sin(values[i]);
                }
                ArrayAssert.ValuesEqual(res, correct);
            } finally {
                Settings.MaxNumberThreads = oldThreads;
            }
        }
        [TestMethod]
        public void OOP3dimStridedMultiThread5() {

            double[] values = new double[0], dummy = null;
            Array<double> A = Helper.generateSystemArray<double>(200, 2, ref values, ref dummy);
            var bsd = A.S.GetBSD(true);
            bsd[0] = (3);
            bsd[1] = (200);
            bsd[3] = (100);
            bsd[4] = (2);
            bsd[5] = (1);
            bsd[6] = (4);
            bsd[7] = (2);
            bsd[8] = (2);
            // 200, 2 (2, 1) -> 100, 2, 2 (4, 2, 1)
            // CM: 
            // 1, 100, 200
            // RM: 
            // 4, 2, 1

            Assert.IsTrue(A.S.StorageOrder == StorageOrders.Other);
            Assert.IsFalse(A.S.IsContinuous);

            uint oldThreads = Settings.MaxNumberThreads;
            try {
                Settings.MaxNumberThreads = 5;

                Array<double> B = MathInternal.sin(A);

                Assert.IsTrue(B.S.StorageOrder == Settings.DefaultStorageOrder);
                Assert.IsTrue(B.S.StorageOrder == StorageOrders.ColumnMajor);
                Assert.IsTrue(B.S.IsSameSize(A.S));
                Assert.IsTrue(B.S.IsSameShape(A.S));

                Assert.IsTrue(!object.Equals(B.Storage, A.Storage));
                Assert.IsTrue(!object.Equals(B.Storage.m_handles, A.Storage.m_handles));

                double[] res = new double[B.S.NumberOfElements];
                B.ExportValues(ref res, StorageOrders.RowMajor); // ! much easier to test along the rows

                double[] correct = new double[B.S.NumberOfElements];
                for (int i = 0; i < correct.Length; i++) {
                    correct[i] = Math.Sin(values[i * 2]);
                }
                ArrayAssert.ValuesEqual(res, correct);
            } finally {
                Settings.MaxNumberThreads = oldThreads;
            }
        }
        [TestMethod]
        public void OOP3dimStridedMultiThread5Chunkin1stDim() {

            double[] values = new double[0], dummy = null;
            Array<double> A = Helper.generateSystemArray<double>(200, 2, ref values, ref dummy);
            var bsd = A.S.GetBSD(true);
            bsd[0] = (3);
            bsd[1] = (200);
            bsd[2] = (0);
            bsd[3] = (100);
            bsd[4] = (2);
            bsd[5] = (1);
            bsd[6] = (4);
            bsd[7] = (2);
            bsd[8] = (2);
            // 200, 2 (2, 1) -> 100, 2, 2 (4, 2, 1)
            // CM: 
            // 1, 100, 200
            // RM: 
            // 4, 2, 1

            Assert.IsTrue(A.S.StorageOrder == StorageOrders.Other);
            Assert.IsFalse(A.S.IsContinuous);

            uint oldThreads = Settings.MaxNumberThreads;
            try {
                Settings.MaxNumberThreads = 5;

                Array<double> B = MathInternal.sin(A);

                Assert.IsTrue(B.S.StorageOrder == Settings.DefaultStorageOrder);
                Assert.IsTrue(B.S.StorageOrder == StorageOrders.ColumnMajor);
                Assert.IsTrue(B.S.IsContinuous);
                Assert.IsTrue(B.S.IsSameSize(A.S));
                Assert.IsTrue(B.S.IsSameShape(A.S));

                Assert.IsTrue(!object.Equals(B.Storage, A.Storage));
                Assert.IsTrue(!object.Equals(B.Storage.m_handles, A.Storage.m_handles));

                double[] res = new double[B.S.NumberOfElements];
                B.ExportValues(ref res, StorageOrders.RowMajor); // ! much easier to test along the rows

                double[] correct = new double[B.S.NumberOfElements];
                for (int i = 0; i < correct.Length; i++) {
                    correct[i] = Math.Sin(values[i * 2]);
                }
                ArrayAssert.ValuesEqual(res, correct);
            } finally {
                Settings.MaxNumberThreads = oldThreads;
            }
        }
        [TestMethod]
        public void Strided32ToColumnMajorEvenChunk() {

            double[] values = new double[0], dummy = null;
            Array<double> A = Helper.generateSystemArray<double>(200, 2, ref values, ref dummy);
            var bsd = A.S.GetBSD(true);
            bsd[0] = (3);
            bsd[1] = (200);
            bsd[3] = (100);
            bsd[4] = (2);
            bsd[5] = (1);
            bsd[6] = (4);
            bsd[7] = (2);
            bsd[8] = (2);
            // 200, 2 (2, 1) -> 100, 2, 2 (4, 2, 1)
            // CM: 
            // 1, 100, 200
            // RM: 
            // 4, 2, 1

            Assert.IsTrue(A.S.StorageOrder == StorageOrders.Other);
            Assert.IsFalse(A.S.IsContinuous);

            uint oldThreads = Settings.MaxNumberThreads;
            try {
                Settings.MaxNumberThreads = 5;

                Array<double> B = MathInternal.sin(A);

                Assert.IsTrue(B.S.StorageOrder == Settings.DefaultStorageOrder);
                Assert.IsTrue(B.S.IsSameSize(A.S));
                Assert.IsTrue(B.S.IsSameShape(A.S));

                Assert.IsTrue(!object.Equals(B.Storage, A.Storage));
                Assert.IsTrue(!object.Equals(B.Storage.m_handles, A.Storage.m_handles));

                double[] res = new double[B.S.NumberOfElements];
                B.ExportValues(ref res, StorageOrders.RowMajor);

                double[] correct = new double[B.S.NumberOfElements];
                for (int i = 0; i < correct.Length; i++) {
                    correct[i] = Math.Sin(values[i * 2]);
                }
                ArrayAssert.ValuesEqual(res, correct);
            } finally {
                Settings.MaxNumberThreads = oldThreads;
            }
        }
        [TestMethod]
        public void Strided32ToColumnMajorOddChunk39() {

            double[] values = new double[0], dummy = null;
            Array<double> A = Helper.generateSystemArray<double>(200, 2, ref values, ref dummy);
            var bsd = A.S.GetBSD(true);
            bsd[0] = (3);
            bsd[1] = (200);
            Assert.IsTrue(bsd[2] == 0); 
            bsd[3] = (100);
            bsd[4] = (2);
            bsd[5] = (1);
            bsd[6] = (4);
            bsd[7] = (2);
            bsd[8] = (2);
            // 200, 2 (2, 1) -> 100, 2, 2 (4, 2, 1)
            // CM: 
            // 1, 100, 200
            // RM: 
            // 4, 2, 1

            Assert.IsTrue(A.S.StorageOrder == StorageOrders.Other);
            Assert.IsFalse(A.S.IsContinuous);

            uint oldThreads = Settings.MaxNumberThreads;
            try {
                Settings.MaxNumberThreads = 5;

                Array<double> B = MathInternal.sin(A);

                Assert.IsTrue(B.S.StorageOrder == Settings.DefaultStorageOrder);
                Assert.IsTrue(B.S.IsSameSize(A.S));
                Assert.IsTrue(B.S.IsSameShape(A.S));

                Assert.IsTrue(!object.Equals(B.Storage, A.Storage));
                Assert.IsTrue(!object.Equals(B.Storage.m_handles, A.Storage.m_handles));

                double[] res = new double[B.S.NumberOfElements];
                B.ExportValues(ref res, StorageOrders.RowMajor);

                double[] correct = new double[B.S.NumberOfElements];
                for (int i = 0; i < correct.Length; i++) {
                    correct[i] = Math.Sin(values[i * 2]);
                }
                System.Diagnostics.Debug.WriteLine(A); 
                System.Diagnostics.Debug.WriteLine(B);
                ArrayAssert.ValuesEqual(res, correct);
            } finally {
                Settings.MaxNumberThreads = oldThreads;
            }
        }

        [TestMethod]
        public void Strided64ToColumnMajor() {
            // in order for this test to succeed you must have compiled the core module 
            // with TEST_100ASUINTMAXVALUE compiler switch enabled!

            double[] values = new double[0], dummy = null;
            Array<double> A = Helper.generateSystemArray<double>(2000, 2, ref values, ref dummy);
            var bsd = A.S.GetBSD(true);
            bsd[0] = (3);
            bsd[1] = (2000);
            bsd[3] = (1000);
            bsd[4] = (2);
            bsd[5] = (1);
            bsd[6] = (4);
            bsd[7] = (2);
            bsd[8] = (2);
            // 200, 2 (2, 1) -> 100, 2, 2 (4, 2, 1)
            // CM: 
            // 1, 100, 200
            // RM: 
            // 4, 2, 1

            Assert.IsTrue(A.S.StorageOrder == StorageOrders.Other);
            Assert.IsFalse(A.S.IsContinuous);

            uint oldThreads = Settings.MaxNumberThreads;
            try {
                Settings.MaxNumberThreads = 1;

                Array<double> B = MathInternal.sin(A);

                Assert.IsTrue(B.S.StorageOrder == Settings.DefaultStorageOrder);
                Assert.IsTrue(B.S.IsSameSize(A.S));
                Assert.IsTrue(B.S.IsSameShape(A.S));

                //Assert.IsTrue(!object.Equals(B.Storage, A.Storage));
                //Assert.IsTrue(!object.Equals(B.Storage.m_handles, A.Storage.m_handles));

                double[] res = new double[B.S.NumberOfElements];
                B.ExportValues(ref res, StorageOrders.RowMajor);

                double[] correct = new double[B.S.NumberOfElements];
                for (int i = 0; i < correct.Length; i++) {
                    correct[i] = Math.Sin(values[i * 2]);
                }
                ArrayAssert.ValuesEqual(res, correct);
            } finally {
                Settings.MaxNumberThreads = oldThreads;
            }
        }
        [TestMethod]
        public void Strided64ToColumnMajorMultiThread() {
            // in order for this test to succeed you must have compiled the core module 
            // with TEST_100ASUINTMAXVALUE compiler switch enabled!
            double[] values = new double[0], dummy = null;
            Array<double> A = Helper.generateSystemArray<double>(2000, 2, ref values, ref dummy);
            var bsd = A.S.GetBSD(true);
            bsd[0] = (3);
            bsd[1] = (2000);
            bsd[3] = (1000);
            bsd[4] = (2);
            bsd[5] = (1);
            bsd[6] = (4);
            bsd[7] = (2);
            bsd[8] = (2);
            // 200, 2 (2, 1) -> 100, 2, 2 (4, 2, 1)
            // CM: 
            // 1, 100, 200
            // RM: 
            // 4, 2, 1

            Assert.IsTrue(A.S.StorageOrder == StorageOrders.Other);
            Assert.IsFalse(A.S.IsContinuous);

            uint oldThreads = Settings.MaxNumberThreads;
            try {
                Settings.MaxNumberThreads = 5;

                Array<double> B = MathInternal.sin(A);

                Assert.IsTrue(B.S.StorageOrder == Settings.DefaultStorageOrder);
                Assert.IsTrue(B.S.IsSameSize(A.S));
                Assert.IsTrue(B.S.IsSameShape(A.S));

                //Assert.IsTrue(!object.Equals(B.Storage, A.Storage));
                //Assert.IsTrue(!object.Equals(B.Storage.m_handles, A.Storage.m_handles));

                double[] res = new double[B.S.NumberOfElements];
                B.ExportValues(ref res, StorageOrders.RowMajor);

                double[] correct = new double[B.S.NumberOfElements];
                for (int i = 0; i < correct.Length; i++) {
                    correct[i] = Math.Sin(values[i * 2]);
                }
                ArrayAssert.ValuesEqual(res, correct);
            } finally {
                Settings.MaxNumberThreads = oldThreads;
            }

        }
        [TestMethod]
        public void Strided32ToRowMajor() {
            double[] values = new double[0], dummy = null;
            Array<double> A = Helper.generateSystemArray<double>(200, 2, ref values, ref dummy);
            var bsd = A.S.GetBSD(true);
            bsd[0] = (3);
            bsd[1] = (200);
            bsd[3] = (100);
            bsd[4] = (2);
            bsd[5] = (1);
            bsd[6] = (4);
            bsd[7] = (2);
            bsd[8] = (2);
            // 200, 2 (2, 1) -> 100, 2, 2 (4, 2, 1)
            // CM: 
            // 1, 100, 200
            // RM: 
            // 4, 2, 1

            Assert.IsTrue(A.S.StorageOrder == StorageOrders.Other);
            Assert.IsFalse(A.S.IsContinuous);

            using (Settings.Ensure(nameof(Settings.MaxNumberThreads), 1u))
            using (Settings.Ensure(nameof(Settings.ArrayStyle), ArrayStyles.numpy)) {

                Array<double> B = MathInternal.sin(A);

                Assert.IsTrue(B.S.StorageOrder == Settings.DefaultStorageOrder);
                Assert.IsTrue(B.S.IsSameSize(A.S));
                Assert.IsTrue(B.S.IsSameShape(A.S));

                //Assert.IsTrue(!object.Equals(B.Storage, A.Storage));
                //Assert.IsTrue(!object.Equals(B.Storage.m_handles, A.Storage.m_handles));

                double[] res = new double[B.S.NumberOfElements];
                B.ExportValues(ref res, StorageOrders.RowMajor);

                double[] correct = new double[B.S.NumberOfElements];
                for (int i = 0; i < correct.Length; i++) {
                    correct[i] = Math.Sin(values[i * 2]);
                }
                ArrayAssert.ValuesEqual(res, correct);

            }

        }
        [TestMethod]
        public void Strided32ToRowMajorMultiThread() {

            double[] values = new double[0], dummy = null;
            Array<double> A = Helper.generateSystemArray<double>(200, 2, ref values, ref dummy);
            var bsd = A.S.GetBSD(true);
            bsd[0] = (3);
            bsd[1] = (200);
            bsd[3] = (100);
            bsd[4] = (2);
            bsd[5] = (1);
            bsd[6] = (4);
            bsd[7] = (2);
            bsd[8] = (2);
            // 200, 2 (2, 1) -> 100, 2, 2 (4, 2, 1)
            // CM: 
            // 1, 100, 200
            // RM: 
            // 4, 2, 1

            Assert.IsTrue(A.S.StorageOrder == StorageOrders.Other);
            Assert.IsFalse(A.S.IsContinuous);

            using (Settings.Ensure(nameof(Settings.MaxNumberThreads), 5u))
            using (Settings.Ensure(nameof(Settings.ArrayStyle), ArrayStyles.numpy)) {

                Array<double> B = MathInternal.sin(A);

                Assert.IsTrue(B.S.StorageOrder == Settings.DefaultStorageOrder);
                Assert.IsTrue(B.S.IsSameSize(A.S));
                Assert.IsTrue(B.S.IsSameShape(A.S));

                //Assert.IsTrue(!object.Equals(B.Storage, A.Storage));
                //Assert.IsTrue(!object.Equals(B.Storage.m_handles, A.Storage.m_handles));

                double[] res = new double[B.S.NumberOfElements];
                B.ExportValues(ref res, StorageOrders.RowMajor);

                double[] correct = new double[B.S.NumberOfElements];
                for (int i = 0; i < correct.Length; i++) {
                    correct[i] = Math.Sin(values[i * 2]);
                }
                ArrayAssert.ValuesEqual(res, correct);

            }

        }
        [TestMethod]
        public void Strided64ToRowMajor() {

            double[] values = new double[0], dummy = null;
            Array<double> A = Helper.generateSystemArray<double>(2000, 2, ref values, ref dummy);
            var bsd = A.S.GetBSD(true);
            bsd[0] = (3);
            bsd[1] = (2000);
            bsd[3] = (1000);
            bsd[4] = (2);
            bsd[5] = (1);
            bsd[6] = (4);
            bsd[7] = (2);
            bsd[8] = (2);
            // 200, 2 (2, 1) -> 100, 2, 2 (4, 2, 1)
            // CM: 
            // 1, 100, 200
            // RM: 
            // 4, 2, 1

            Assert.IsTrue(A.S.StorageOrder == StorageOrders.Other);
            Assert.IsFalse(A.S.IsContinuous);

            using (Settings.Ensure(nameof(Settings.MaxNumberThreads), 1u))
            using (Settings.Ensure(nameof(Settings.ArrayStyle), ArrayStyles.numpy)) {

                Array<double> B = MathInternal.sin(A);

                Assert.IsTrue(B.S.StorageOrder == Settings.DefaultStorageOrder);
                Assert.IsTrue(B.S.IsSameSize(A.S));
                Assert.IsTrue(B.S.IsSameShape(A.S));

                //Assert.IsTrue(!object.Equals(B.Storage, A.Storage));
                //Assert.IsTrue(!object.Equals(B.Storage.m_handles, A.Storage.m_handles));

                double[] res = new double[B.S.NumberOfElements];
                B.ExportValues(ref res, StorageOrders.RowMajor);

                double[] correct = new double[B.S.NumberOfElements];
                for (int i = 0; i < correct.Length; i++) {
                    correct[i] = Math.Sin(values[i * 2]);
                }
                ArrayAssert.ValuesEqual(res, correct);
            }

        }
        [TestMethod]
        public void Strided64ToRowMajorMultiThread() {
            double[] values = new double[0], dummy = null;
            Array<double> A = Helper.generateSystemArray<double>(2000, 2, ref values, ref dummy);
            var bsd = A.S.GetBSD(true);
            bsd[0] = (3);
            bsd[1] = (2000);
            bsd[3] = (1000);
            bsd[4] = (2);
            bsd[5] = (1);
            bsd[6] = (4);
            bsd[7] = (2);
            bsd[8] = (2);
            // 200, 2 (2, 1) -> 100, 2, 2 (4, 2, 1)
            // CM: 
            // 1, 100, 200
            // RM: 
            // 4, 2, 1

            Assert.IsTrue(A.S.StorageOrder == StorageOrders.Other);
            Assert.IsFalse(A.S.IsContinuous);

            using (Settings.Ensure(nameof(Settings.MaxNumberThreads), 5u))
            using (Settings.Ensure(nameof(Settings.ArrayStyle), ArrayStyles.numpy)) {

                Array<double> B = MathInternal.sin(A);

                Assert.IsTrue(B.S.StorageOrder == Settings.DefaultStorageOrder);
                Assert.IsTrue(B.S.IsSameSize(A.S));
                Assert.IsTrue(B.S.IsSameShape(A.S));

                //Assert.IsTrue(!object.Equals(B.Storage, A.Storage));
                //Assert.IsTrue(!object.Equals(B.Storage.m_handles, A.Storage.m_handles));

                double[] res = new double[B.S.NumberOfElements];
                B.ExportValues(ref res, StorageOrders.RowMajor);

                double[] correct = new double[B.S.NumberOfElements];
                for (int i = 0; i < correct.Length; i++) {
                    correct[i] = Math.Sin(values[i * 2]);
                }
                ArrayAssert.ValuesEqual(res, correct);

            }
        }
        [TestMethod]
        public void empty() {
            Array<float> A = new float[0];
            Array<float> B = MathInternal.sin(A);

            Assert.IsTrue(B.S.StorageOrder == A.S.StorageOrder);
            Assert.IsTrue(B.S.IsSameSize(A.S));
            Assert.IsTrue(B.S.IsSameShape(A.S));

        }
        [TestMethod]
        public void Scalar() {

            Array<float> A = new float[] { 15 };
            Array<float> B = MathInternal.sin(A);

            Assert.IsTrue(B.S.StorageOrder == A.S.StorageOrder);
            Assert.IsTrue(B.S.IsSameSize(A.S));
            Assert.IsTrue(B.S.IsSameShape(A.S));

            Assert.IsTrue(B.GetValue(0) == (float)Math.Sin(A.GetValue(0)));

        }
    }
}
