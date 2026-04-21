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
using static ILNumerics.ILMath; 
using static ILNumerics.Globals;

namespace ILNumerics.Core.UnitTests.MathInternalTests {

    [TestClass]
    public class MathInternal_concatTests {

        [TestMethod]
        public void MathInternal_concat_simple() {

            Array<double> A = counter(1.0, 1.0, 5, 4);
            Array<double> B = counter(21, 1.0, 5, 3);

            Array<double> C = concat(A, B, 1);
            Assert.IsTrue(C.Equals(counter<double>(1.0, 1.0, 5, 7)));
        }

        [TestMethod]
        public void MathInternal_concat_empty1() {

            Array<double> A = counter(1.0, 1.0, 5, dim1: 0);
            Array<double> B = counter(A.S.NumberOfElements + 1, 1.0, 5, 3);

            Array<double> C = concat(A, B, 1);
            Assert.IsTrue(C.Equals(counter<double>(1.0, 1.0, 5, 3)));
        }
        [TestMethod]
        public void MathInternal_concat_empty2() {

            Array<double> A = counter(1.0, 1.0, 5, dim1: 4);
            Array<double> B = counter(A.S.NumberOfElements + 1, 1.0, 5, dim1: 0);

            Array<double> C = concat(A, B, 1);
            Assert.IsTrue(C.Equals(counter<double>(1.0, 1.0, 5, A.S[1] + B.S[1])));
        }
        [TestMethod]
        public void MathInternal_horzcat_empty2() {

            Array<double> A = counter(1.0, 1.0, 5, dim1: 4);
            Array<double> B = counter(A.S.NumberOfElements + 1, 1.0, 5, dim1: 0);

            Array<double> C = horzcat(A, B);
            Assert.IsTrue(C.Equals(counter<double>(1.0, 1.0, 5, A.S[1] + B.S[1])));
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MathInternal_concat_NPScalar1() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = 1;
                Array<double> B = 2;
                concat(A, B, 0); 
            }
        }
        [TestMethod]
        public void MathInternal_concat_NPVector() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<float> A = new float[] { 1, 2, 3, 4 };
                Array<float> B = new float[] { 5, 6, 7 };

                Assert.IsTrue(concat(A, B, 0).Equals(counter<float>(1f, 1f, 7))); 
            }
        }

        [TestMethod]
        public void MathInternal_concat_nonCM() {

            Array<double> A = counter(1.0, 1.0, 5, 4, StorageOrders.ColumnMajor);
            A.Storage.EnsureStorageOrder(StorageOrders.RowMajor);
            Array<double> B = counter(21, 0.5, 10, 3);

            Array<double> C = concat(A, B[r(0, 2, end), full], 1);
            Assert.IsTrue(C.Equals(counter<double>(1.0, 1.0, 5, 7)));
        }
        [TestMethod]
        public void MathInternal_horzcat_nonCM() {

            Array<double> A = counter(1.0, 1.0, 5, 4, StorageOrders.ColumnMajor);
            A.Storage.EnsureStorageOrder(StorageOrders.RowMajor);
            Array<double> B = counter(21, 0.5, 10, 3);

            Array<double> C = horzcat(A, B[r(0, 2, end), full]);
            Assert.IsTrue(C.Equals(counter<double>(1.0, 1.0, 5, 7)));
        }
        [TestMethod]
        public void MathInternal_concat_nonCM_along0() {

            Array<double> A = counter(1.0, 1.0, 5, 40, StorageOrders.RowMajor);
            //A.Storage.EnsureStorageOrder(StorageOrders.RowMajor);
            Array<double> B = counter(A.S.NumberOfElements + 1, 0.5, 10, 80, StorageOrders.RowMajor);

            Array<double> C = concat(A, B[full, r(0, 2, end)], 0);
            Assert.IsTrue(C.Equals(counter<double>(1.0, 1.0, A.S[0] + B.S[0], 40, StorageOrders.RowMajor)));
        }
        [TestMethod]
        public void MathInternal_concat_nonCM_along0_npMode() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = counter(1.0, 1.0, 5, 40, StorageOrders.RowMajor);
                //A.Storage.EnsureStorageOrder(StorageOrders.RowMajor);
                Array<double> B = counter(A.S.NumberOfElements + 1, 0.5, 10, 80, StorageOrders.RowMajor);

                Array<double> C = concat(A, B[full, r(0, 2, end)], 0);
                Assert.IsTrue(C.Equals(counter<double>(1.0, 1.0, A.S[0] + B.S[0], 40, StorageOrders.RowMajor)));
            }
        }

        [TestMethod]
        public void MathInternal_horzcat_nonCM_along0_npMode() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = counter(1.0, 1.0, 5, 40, StorageOrders.RowMajor);
                //A.Storage.EnsureStorageOrder(StorageOrders.RowMajor);
                Array<double> B = counter(A.S.NumberOfElements + 1, 0.5, 10, 80, StorageOrders.RowMajor);

                Array<double> C = vertcat(A, B[full, r(0, 2, end)]);
                Assert.IsTrue(C.Equals(counter<double>(1.0, 1.0, A.S[0] + B.S[0], 40, StorageOrders.RowMajor)));
            }
        }
        [TestMethod]
        public void MathInternal_vertcat_nonCM_along0_npMode_bool() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Logical A = counter(1.0, 1.0, 5, 40, StorageOrders.RowMajor) % 2 == 0;
                //A.Storage.EnsureStorageOrder(StorageOrders.RowMajor);
                Logical B = counter(A.S.NumberOfElements + 1, 0.5, 10, 80, StorageOrders.RowMajor) % 2 == 0;

                Logical C = vertcat(A, B[full, r(0, 2, end)]);
 
                Assert.IsTrue(C.Storage.IsNumberTruesCached);
                Assert.IsTrue(C.Storage.NumberTrues == A.NumberTrues + B[full, r(0, 2, end)].NumberTrues);
                Assert.IsTrue(C.Storage.NumberTrues == 15 * 40 / 2);
                Assert.IsTrue(C.Equals(counter<double>(1.0, 1.0, A.S[0] + B.S[0], 40, StorageOrders.RowMajor) % 2 == 0));
            }
        }
        [TestMethod]
        public void MathInternal_ConcatExt_nonCM_along0_npMode_bool() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Logical A = counter(1.0, 1.0, 5, 40, StorageOrders.RowMajor) % 2 == 0;
                //A.Storage.EnsureStorageOrder(StorageOrders.RowMajor);
                Logical B = counter(A.S.NumberOfElements + 1, 0.5, 10, 80, StorageOrders.RowMajor) % 2 == 0;

                Logical C = A.Concat(B[full, r(0, 2, end)],0);
                Assert.IsTrue(C.Storage.IsNumberTruesCached);
                Assert.IsTrue(C.Storage.NumberTrues == A.NumberTrues + B[full, r(0, 2, end)].NumberTrues);
                Assert.IsTrue(C.Storage.NumberTrues == 15 * 40 / 2);
                Assert.IsTrue(C.Equals(counter<double>(1.0, 1.0, A.S[0] + B.S[0], 40, StorageOrders.RowMajor) % 2 == 0));
            }
        }

    }
}
