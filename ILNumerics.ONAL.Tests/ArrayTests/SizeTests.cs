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
using System.Linq;
using static ILNumerics.ILMath; 

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public class SizeTests {

        [TestMethod]
        public unsafe void SizeCreation001() {
            var size = new Size();
            Assert.IsTrue(size.NumberOfDimensions == 0);
            Assert.IsTrue(size.NumberOfElements == 0);
            Assert.IsTrue(size.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(size.IsContinuous);
            Assert.IsTrue(size.GetBSD() != null); 
        }

        [TestMethod]
        public void SizeCreation002() {
            var size = new Size();
            size.SetAll(10, 1);
            Assert.IsTrue(Settings.MinNumberOfArrayDimensions == 2);
            Assert.IsTrue(size.NumberOfDimensions == 2);
            Assert.IsTrue(size.NumberOfElements == 10);
            Assert.IsTrue(size[0] == 10);
            Assert.IsTrue(size[1] == 1);
            Assert.IsTrue(size[2] == 1);
            Assert.IsTrue(size.GetStride(0) == 1);
            Assert.IsTrue(size.GetStride(1) == 0);
            Assert.IsTrue(size.GetStride(2) == 0);
            Assert.IsTrue(size.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(size.IsContinuous);
        }
        [TestMethod]
        public void SizeCreation0003() {
            var size = new Size();
            size.SetAll(10, 20, 1, 10);
            Assert.IsTrue(Settings.MinNumberOfArrayDimensions == 2);
            Assert.IsTrue(size.NumberOfDimensions == 2);
            Assert.IsTrue(size.NumberOfElements == 200);
            Assert.IsTrue(size[0u] == 10);
            Assert.IsTrue(size[1u] == 20);
            Assert.IsTrue(size[2u] == 1);
            Assert.IsTrue(size.GetStride(0) == 1);
            Assert.IsTrue(size.GetStride(1) == 10);
            Assert.IsTrue(size.GetStride(2) == 0);
            Assert.IsTrue(size.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(size.IsContinuous);
        }
        [TestMethod]
        public void SizeCreation0004() {
            var size = new Size();
            size.SetAll(10, 20, 20, 1);
            Assert.IsTrue(Settings.MinNumberOfArrayDimensions == 2);
            Assert.IsTrue(size.NumberOfDimensions == 2);
            Assert.IsTrue(size.NumberOfElements == 200);
            Assert.IsTrue(size[0] == 10);
            Assert.IsTrue(size[1] == 20);
            Assert.IsTrue(size[2] == 1);
            Assert.IsTrue(size.GetStride(0) == 20);
            Assert.IsTrue(size.GetStride(1) == 1);
            Assert.IsTrue(size.GetStride(2) == 0);
            Assert.IsTrue(size.StorageOrder == StorageOrders.RowMajor);
            Assert.IsTrue(size.IsContinuous);
        }
        [TestMethod]
        public void SizeCreation0005() {
            var size = new Size();
            size.SetAll(10, 20, 20, 2);
            Assert.IsTrue(Settings.MinNumberOfArrayDimensions == 2);
            Assert.IsTrue(size.NumberOfDimensions == 2);
            Assert.IsTrue(size.NumberOfElements == 200);
            Assert.IsTrue(size[0] == 10);
            Assert.IsTrue(size[1] == 20);
            Assert.IsTrue(size[2] == 1);
            Assert.IsTrue(size.GetStride(0) == 20);
            Assert.IsTrue(size.GetStride(1) == 2);
            Assert.IsTrue(size.GetStride(2) == 0);
            Assert.IsTrue(size.StorageOrder == StorageOrders.Other);
            Assert.IsTrue(!size.IsContinuous);
        }

        [TestMethod]
        public void GetShiftedTests() {
            TestShifted(0,
                new uint[] { 7, 28, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 },
                new uint[] { 7, 28, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 });
            TestShifted(1,
                new uint[] { 7, 28, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 },
                new uint[] { 7, 28, 0, 2, 3, 4, 5, 6, 7, 1, 9, 10, 11, 12, 13, 14, 8 });
            TestShifted(2,
                new uint[] { 7, 28, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 },
                new uint[] { 7, 28, 0, 3, 4, 5, 6, 7, 1, 2, 10, 11, 12, 13, 14, 8, 9 });
            TestShifted(3,
                new uint[] { 7, 28, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 },
                new uint[] { 7, 28, 0, 4, 5, 6, 7, 1, 2, 3, 11, 12, 13, 14, 8, 9, 10 });
            TestShifted(4,
                new uint[] { 7, 28, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 },
                new uint[] { 7, 28, 0, 5, 6, 7, 1, 2, 3, 4, 12, 13, 14, 8, 9, 10, 11 });
            TestShifted(5,
                new uint[] { 7, 28, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 },
                new uint[] { 7, 28, 0, 6, 7, 1, 2, 3, 4, 5, 13, 14, 8, 9, 10, 11, 12 });
            TestShifted(6,
                new uint[] { 7, 28, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 },
                new uint[] { 7, 28, 0, 7, 1, 2, 3, 4, 5, 6, 14, 8, 9, 10, 11, 12, 13 });
            TestShifted(7,
                new uint[] { 7, 28, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 },
                new uint[] { 7, 28, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 });
            TestShifted(8,
                new uint[] { 7, 28, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 },
                new uint[] { 7, 28, 0, 2, 3, 4, 5, 6, 7, 1, 9, 10, 11, 12, 13, 14, 8 });
        }

        [TestMethod]
        public void SizeLrotTests() {
            // this covers roughly the whole parameter space of Size.lrot(...)
            for (int i = 0; i < 38; i++) {
                var arr = new long[i];
                for (int e = 0; e < arr.Length; e++) {
                    arr[e] = e; 
                }
                TestLRotTestPermute(arr);
            }
        }

        [TestMethod]
        public void SizeGetSpanTest001() {
            var size = new Size();
            size.SetAll(10, 2);
            Assert.IsTrue(size.GetElementSpan() == 18);
        }
        [TestMethod]
        public void SizeGetSpanTest002() {
            var size = new Size();
            size.SetAll(10, 20, 2, 20);
            Assert.IsTrue(size.GetElementSpan() == 398);
        }
        [TestMethod]
        public void SizeGetSpanTest003() {
            var size = new Size();
            size.SetAll(10, 20, 2, 1, 22, 50);
            Assert.IsTrue(size.GetElementSpan() == 477);
        }
        [TestMethod]
        public void SizeGetSpanTest004empty() {
            var size = new Size();
            size.SetAll(10, 0, 2, 1, 22, 50);
            Assert.IsTrue(size.GetElementSpan() == 0);
        }
        [TestMethod]
        public void SizeIsContinousTest001() {
            var size = new Size();
            size.SetAll(10, 2);
            Assert.IsTrue(!size.IsContinuous);
        }
        [TestMethod]
        public void SizeIsContinousTest001T() {
            var size = new Size();
            size.SetAll(10, 1);
            Assert.IsTrue(size.IsContinuous);
        }
        [TestMethod]
        public void SizeIsContinousTest002T() {
            var size = new Size();
            size.SetAll(2,3,4,5,6, 1,2,6,24,120);
            Assert.IsTrue(size.IsContinuous);
        }
        [TestMethod]
        public void SizeIsContinousTest002() {
            var size = new Size();
            size.SetAll(10, 20, 2, 20);
            Assert.IsTrue(!size.IsContinuous);
        }
        [TestMethod]
        public void SizeIsContinousTest003() {
            var size = new Size();
            size.SetAll(10, 20, 2, 1, 22, 50);
            Assert.IsTrue(!size.IsContinuous);
        }
        [TestMethod]
        public void SizeIsContinousempty() {
            var size = new Size();
            size.SetAll(10, 0, 2, 1, 22, 50);
            Assert.IsTrue(size.IsContinuous);
        }
        [TestMethod]
        public unsafe void SizeIsContinousEmpty2() {
            var size = new Size();
            var bsd = size.GetBSD();
            Assert.IsTrue(bsd[0] == 0); 
            Assert.IsTrue(bsd[1] == 0); 
            Assert.IsTrue(bsd[2] == 0);
            Assert.IsTrue(size.IsContinuous);
        }

        [TestMethod]
        public void SizeNumberOfSingletonDimensionsTest() {
            var size = new Size();
            Assert.IsTrue(Settings.MinNumberOfArrayDimensions == 2); 
            size.SetAll(10, 1);  Assert.IsTrue(size.NonSingletonDimensions == 1); 
            size.SetAll(10, 1, 10, 20);  Assert.IsTrue(size.NonSingletonDimensions == 1);
            size.SetAll(10, 1, 1, 10);  Assert.IsTrue(size.NonSingletonDimensions == 1); 
            size.SetAll(10, 0, 1, 10);  Assert.IsTrue(size.NonSingletonDimensions == 2); 
            size.SetAll(0, 0, 1, 10);  Assert.IsTrue(size.NonSingletonDimensions == 2); 
            size.SetAll(0, 10, 1, 10);  Assert.IsTrue(size.NonSingletonDimensions == 2); 
            size.SetAll(1, 10, 1, 10);  Assert.IsTrue(size.NonSingletonDimensions == 1);
            size.SetAll(10, 10, 3, 1, 10, 100);  Assert.IsTrue(size.NonSingletonDimensions == 3);

        }

        [TestMethod]
        public void SizeContinousFlagIgnoresSingletonDimsColumnTest() {
            var size = new Size();
            Assert.IsTrue(Settings.MinNumberOfArrayDimensions == 2);
            // third dimension is singleton. Its (otherwise unmatching) strides are ignored and the dim does not disturb the evaluation as column major order.
            size.SetAll(100, 4, 1, 2, 1, 100, 4, 400);
            Assert.IsTrue(size.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(size.IsContinuous);
        }
        [TestMethod]
        public void SizeContinousFlagIgnoresSingletonDimsRowTest() {
            var size = new Size();
            Assert.IsTrue(Settings.MinNumberOfArrayDimensions == 2);
            // third dimension is singleton. Its (otherwise unmatching) strides are ignored and the dim does not disturb the evaluation as column major order.
            size.SetAll(100, 4, 1, 2, /**/ 8, 2, 20, 1);
            Assert.IsTrue(size.StorageOrder == StorageOrders.RowMajor);
            Assert.IsTrue(size.IsContinuous);
        }
        [TestMethod]
        public void SizeTranspose3D_StorageOrderTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,,]
                {
                    { { 1, 2, 3 }, {  4,  5,  6 },
                      { 7, 8, 9 }, { 10, 11, 12 } },
                    { {-1,-2,-3 }, { -4, -5, -6 },
                      {-7,-8,-9 }, {-10,-11,-12 } },
                };
                Assert.IsTrue(A.Storage.S.StorageOrder == StorageOrders.RowMajor);
                Assert.IsTrue(A.S.GetStride(0) == 12);
                Assert.IsTrue(A.S.GetStride(1) == 3);
                Assert.IsTrue(A.S.GetStride(2) == 1);

                A.a = A.T;  // A is now:
                Assert.IsTrue(A.S.GetStride(2) == 12);
                Assert.IsTrue(A.S.GetStride(0) == 3);
                Assert.IsTrue(A.S.GetStride(1) == 1);

                Assert.IsTrue(A.Storage.S.StorageOrder == StorageOrders.Other);
                A.a = A.T;  // A is now:
                Assert.IsTrue(A.S.GetStride(1) == 12);
                Assert.IsTrue(A.S.GetStride(2) == 3);
                Assert.IsTrue(A.S.GetStride(0) == 1);

                Assert.IsTrue(A.Storage.S.StorageOrder == StorageOrders.Other);
            }
        }
        [TestMethod]
        public unsafe void CheckIsBroadcastable_matrixout_Test() {
            long* outdims = stackalloc long[2];
            outdims[0] = 3; outdims[1] = 4;

            counter(1.0, 1.0, 1, 4).S.CheckIsBroadcastableTo_np(outdims, 2);
            counter(1.0, 1.0, 3, 1).S.CheckIsBroadcastableTo_np(outdims, 2);
            counter(1.0, 1.0, 1, 3, 4).S.CheckIsBroadcastableTo_np(outdims, 2);
            counter(1.0, 1.0, 1, 3, 1).S.CheckIsBroadcastableTo_np(outdims, 2);
            counter(1.0, 1.0, 1, 1, 4).S.CheckIsBroadcastableTo_np(outdims, 2);
            Array<double> A = 1; 
            A.S.CheckIsBroadcastableTo_np(outdims, 2);
        }
        [TestMethod]
        public unsafe void CheckIsBroadcastable_vectorout_Test() {
            long* outdims = stackalloc long[1];
            outdims[0] = 3;
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                counter(1.0, 1.0, 1, 3).S.CheckIsBroadcastableTo_np(outdims, 1);
                counter(1.0, 1.0, 3).S.CheckIsBroadcastableTo_np(outdims, 1);
                counter(1.0, 1.0, 1, 1, 3).S.CheckIsBroadcastableTo_np(outdims, 1);

                Array<double> A = 1;
                A.S.CheckIsBroadcastableTo_np(outdims, 1);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public unsafe void CheckIsBroadcastable_vectorout_TestFail() {
            long* outdims = stackalloc long[1];
            outdims[0] = 3;
            // leading dimensions must be singletons
            counter(1.0, 1.0, 5, 3, 4).S.CheckIsBroadcastableTo_np(outdims, 1);

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public unsafe void CheckIsBroadcastable_vectorout_TestFail2() {
            long* outdims = stackalloc long[1];
            outdims[0] = 3;
            // leading dimensions must be singletons
            counter(1.0, 1.0, 5, 3).S.CheckIsBroadcastableTo_np(outdims, 1);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public unsafe void CheckIsBroadcastable_vectorout_TestFail3() {
            long* outdims = stackalloc long[1];
            outdims[0] = 3;
            // leading dimensions must be singletons
            counter(1.0, 1.0, 5).S.CheckIsBroadcastableTo_np(outdims, 1);

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public unsafe void CheckIsBroadcastable_matrixout_TestFail() {
            long* outdims = stackalloc long[2];
            outdims[0] = 3; outdims[1] = 4;
            // leading dimensions must be singletons
            counter(1.0, 1.0, 5, 3, 4).S.CheckIsBroadcastableTo_np(outdims, 2);

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public unsafe void CheckIsBroadcastable_matrixout_TestFail2() {
            long* outdims = stackalloc long[2];
            outdims[0] = 3; outdims[1] = 4;
            // leading dimensions must be singletons
            counter(1.0, 1.0, 5, 1, 1).S.CheckIsBroadcastableTo_np(outdims, 2);

        }

        [TestMethod]
        public void Size_RemoveDimensionSimple() {
            var s = counter<int>(1, 1, 1, 4, 3).S;
            Assert.IsTrue(s.NumberOfDimensions == 3);
            Assert.IsTrue(s[0] == 1);
            Assert.IsTrue(s[1] == 4);
            Assert.IsTrue(s[2] == 3);
            Assert.IsTrue(s.GetStride(0) == 0);
            Assert.IsTrue(s.GetStride(1) == 1);
            Assert.IsTrue(s.GetStride(2) == 4);


            s.RemoveDimension(0);
            Assert.IsTrue(s.NumberOfDimensions == 2);
            Assert.IsTrue(s[0] == 4);
            Assert.IsTrue(s[1] == 3);
            Assert.IsTrue(s.GetStride(0) == 1);
            Assert.IsTrue(s.GetStride(1) == 4);

        }
        [TestMethod]
        public void Size_RemoveDimensionSimple2() {
            var s = counter<int>(1, 1, 4, 1, 3).S;
            Assert.IsTrue(s.NumberOfDimensions == 3);
            Assert.IsTrue(s[0] == 4);
            Assert.IsTrue(s[1] == 1);
            Assert.IsTrue(s[2] == 3);
            Assert.IsTrue(s.GetStride(0) == 1);
            Assert.IsTrue(s.GetStride(1) == 0);
            Assert.IsTrue(s.GetStride(2) == 4);

            s.RemoveDimension(1);
            Assert.IsTrue(s.NumberOfDimensions == 2);
            Assert.IsTrue(s[0] == 4);
            Assert.IsTrue(s[1] == 3);
            Assert.IsTrue(s.GetStride(0) == 1);
            Assert.IsTrue(s.GetStride(1) == 4);

        }
        [TestMethod]
        public void Size_RemoveDimensionSimple3() {
            var s = counter<int>(1, 1, 4, 3, 1).S;
            Assert.IsTrue(s.NumberOfDimensions == 3);
            Assert.IsTrue(s[0] == 4);
            Assert.IsTrue(s[1] == 3);
            Assert.IsTrue(s[2] == 1);
            Assert.IsTrue(s.GetStride(0) == 1);
            Assert.IsTrue(s.GetStride(1) == 4);
            Assert.IsTrue(s.GetStride(2) == 0);

            s.RemoveDimension(2);
            Assert.IsTrue(s.NumberOfDimensions == 2);
            Assert.IsTrue(s[0] == 4);
            Assert.IsTrue(s[1] == 3);
            Assert.IsTrue(s.GetStride(0) == 1);
            Assert.IsTrue(s.GetStride(1) == 4);

        }
        [TestMethod]
        public void Size_RemoveDimensionSimple2_ML() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {
                var s = counter<int>(1, 1, 4, 1).S;
                Assert.IsTrue(s.NumberOfDimensions == 2);
                Assert.IsTrue(s[0] == 4);
                Assert.IsTrue(s[1] == 1);
                Assert.IsTrue(s.GetStride(0) == 1);
                Assert.IsTrue(s.GetStride(1) == 0);

                s.RemoveDimension(1);
                Assert.IsTrue(s.NumberOfDimensions == 2);
                Assert.IsTrue(s[0] == 4);
                Assert.IsTrue(s[1] == 1);
                Assert.IsTrue(s.GetStride(0) == 1);
                Assert.IsTrue(s.GetStride(1) == 0);
            }
        }
        [TestMethod]
        public void Size_RemoveDimensionSimple2_NP() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                var s = counter<int>(1, 1, 4, 1).S;
                Assert.IsTrue(s.NumberOfDimensions == 2);
                Assert.IsTrue(s[0] == 4);
                Assert.IsTrue(s[1] == 1);
                Assert.IsTrue(s.GetStride(0) == 1);
                Assert.IsTrue(s.GetStride(1) == 0);

                s.RemoveDimension(1);
                Assert.IsTrue(s.NumberOfDimensions == 1);
                Assert.IsTrue(s[0] == 4);
                Assert.IsTrue(s[1] == 1);
                Assert.IsTrue(s.GetStride(0) == 1);
                Assert.IsTrue(s.GetStride(1) == 0);
            }
        }
        [TestMethod]
        public void Size_RemoveDimensionSimple1_NP() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                var s = counter<int>(1, 1, 1, 4).S;
                Assert.IsTrue(s.NumberOfDimensions == 2);
                Assert.IsTrue(s[0] == 1);
                Assert.IsTrue(s[1] == 4);
                Assert.IsTrue(s.GetStride(0) == 0);
                Assert.IsTrue(s.GetStride(1) == 1);

                s.RemoveDimension(0);
                Assert.IsTrue(s.NumberOfDimensions == 1);
                Assert.IsTrue(s[0] == 4);
                Assert.IsTrue(s[1] == 1);
                Assert.IsTrue(s.GetStride(0) == 1);
                Assert.IsTrue(s.GetStride(1) == 0);
            }
        }
        [TestMethod]
        public void Size_RemoveDimensionSimple1_ML() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {
                var s = counter<int>(1, 1, 1, 4).S;
                Assert.IsTrue(s.NumberOfDimensions == 2);
                Assert.IsTrue(s[0] == 1);
                Assert.IsTrue(s[1] == 4);
                Assert.IsTrue(s.GetStride(0) == 0);
                Assert.IsTrue(s.GetStride(1) == 1);

                s.RemoveDimension(0);
                Assert.IsTrue(s.NumberOfDimensions == 2);
                Assert.IsTrue(s[0] == 4);
                Assert.IsTrue(s[1] == 1);
                Assert.IsTrue(s.GetStride(0) == 1);
                Assert.IsTrue(s.GetStride(1) == 0);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Size_RemoveDimensionNonSingletonFail() {
            var s = counter<int>(1, 1, 1, 4).S;
            s.RemoveDimension(1);
        }

        #region private helpers 

        private void TestLRotTestPermute(long[] arr) {

            for (int start = 0; start < arr.Length; start++) {
                for (int n = 0; n < arr.Length - start; n++) {
                    for (int d = 0; d <= n; d++) {
                        TestLRot(arr, start, n, d); 
                    }
                }
            }
        }
        private unsafe void TestLRot(long[] arr, int start, int n, int d) {
            var lrcopy = (long[])arr.Clone();
            fixed (long* lrcopyP = lrcopy)
            {
                Size.lrot(lrcopyP, start, n, d);

                var res = (long[])arr.Clone();
                for (int i = 0; i < n; i++) {
                    res[start + i] = arr[start + ((i + d) % n)];
                }

                for (int i = 0; i < arr.Length; i++) {
                    Assert.IsTrue(res[i].ToString() == lrcopyP[i].ToString());
                }
            }
        }
        private unsafe void TestShifted(int v1, uint[] v2, uint[] v3) {
            var bsd1 = Array.ConvertAll<uint, long>(v2, v => (v)); 
            var bsd2 = Array.ConvertAll<uint, long>(v3, v => (v));

            ensureMinArrayDimensions(ref bsd1);
            ensureMinArrayDimensions(ref bsd2); 
            
            var size = new Size();
            fixed (long* bsd1p = bsd1)
            {
                size.SetAll(bsd1p);
            }

            // test offline 
            long[] outBSD_tmp = new long[v3.Length];
            fixed (long* outBSDp = outBSD_tmp)
            {

                size.GetShifted(v1, outBSDp);

                var inBSD = size.GetBSD();
                long* outBSD = outBSDp; 

                Assert.IsTrue(bsd1.Length == bsd2.Length);
                for (int i = 3; i < (uint)outBSD[0] + 3; i++) {
                    Assert.IsTrue(v3[i] == outBSD[i]);
                    Assert.IsTrue(v2[i] == inBSD[i]);
                }

                // test inline
                outBSD = size.GetBSD();
                Assert.IsTrue(outBSD == size.GetBSD());

                size.GetShifted(v1, outBSD);
                inBSD = size.GetBSD();

                Assert.IsTrue(bsd1.Length == bsd2.Length);
                for (int i = 3; i < (uint)outBSD[0] + 3; i++) {
                    Assert.IsTrue(v3[i] == outBSD[i]);
                    Assert.IsTrue(v3[i] == inBSD[i]);
                }
            }
        }

        private void ensureMinArrayDimensions(ref long[] bsd1) {
            long[] ret = bsd1; 
            if (bsd1 == null || bsd1.Length < ILNumerics.Settings.MinNumberOfArrayDimensions * 2 + 3) {
                ret = new long[Settings.MinNumberOfArrayDimensions * 2 + 3];
                Array.Copy(bsd1, ret, bsd1.Length); 
            }
            bsd1 = ret; 
        }
        #endregion

    }
}
