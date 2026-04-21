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
using static ILNumerics.Globals;
using static ILNumerics.ILMath;
using System.Security;
using System.Diagnostics;

namespace ILNumerics.Core.UnitTests {

    [TestClass]
    public class SetRange_MLTests {

        [TestMethod]
        public void SetRangeML_2D_2DTest() {

            Array<double> A = counter(1.0, 1.0, 5, 4);
            Array<double> B = counter(-99.0, 0.0, 2, 2);
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 2u)) {
                A[r(1, 2), r(2, 3)] = B;
            }
        }
        [TestMethod]
        public void SetRangeML_2D_2DLarge_2ThreadsTest() {

            Array<double> A = counter(1.0, 1.0, 50, 40); 
            Array<double> B = counter(-99.0, 0.0, 20, 20);

            using (Settings.Ensure(() => Settings.MaxNumberThreads, 2u)) {
                A[r(end - 19, end), r(end - 19, end)] = B;
            }
            Assert.IsTrue(A[r(end - 19, end), r(end - 19, end)].Equals(B)); 
        }
        [TestMethod]
        public void SetRangeML_2D_2DLarge_3ThreadsTest() {

            Array<double> A = counter(1.0, 1.0, 50, 40); 
            Array<double> B = counter(-99.0, 0.0, 20, 20);

            using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {
                A[r(end - 19, end), r(end - 19, end)] = B;
            }
            Assert.IsTrue(A[r(end - 19, end), r(end - 19, end)].Equals(B)); 
        }
        [TestMethod]
        public void SetRangeML_2D_2DLarge_4ThreadsTest() {

            Array<double> A = counter(1.0, 1.0, 50, 40); 
            Array<double> B = counter(-99.0, 0.0, 20, 20);

            using (Settings.Ensure(() => Settings.MaxNumberThreads, 4u)) {
                A[r(end - 19, end), r(end - 19, end)] = B;
            }
            Assert.IsTrue(A[r(end - 19, end), r(end - 19, end)].Equals(B)); 
        }
        [TestMethod]
        public void SetRangeML_0D_2DTest() {

            using (Settings.Ensure(()=>Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = 4; 
                Array<double> B = counter(-99.0, 0.0, 2, 2);
                Settings.ArrayStyle = ArrayStyles.ILNumericsV4; 
                A[r(0, end), r(-1,0)] = B[0,0];

                Assert.IsTrue(A.GetValue(0) == -99);
                Assert.IsTrue(A.S.NumberOfDimensions == 0 && A.S.NumberOfElements == 1);
                Assert.IsTrue(A.IsScalar); 

            }

        }

        [TestMethod]
        public void BaseStorage_EnsureStorageOrderTestCM() {

            Array<double> A = counter(1.0, 1.0, 5, 4);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);

            var oldCA = A.Storage.m_handles[0].Pointer;
            // don't do anything 
            A.Storage.EnsureStorageOrder(StorageOrders.ColumnMajor);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor); 
            Assert.IsTrue(object.Equals(A.Storage.m_handles[0].Pointer, oldCA));

            A.Storage.EnsureStorageOrder(StorageOrders.RowMajor);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor); 
            Assert.IsTrue(!object.Equals(A.Storage.m_handles[0].Pointer, oldCA));

            Assert.IsTrue(A.Equals(counter(1.0, 1.0, 5, 4))); 

        }
        [TestMethod]
        [SecuritySafeCritical]
        public void BaseStorage_EnsureStorageOrderTestRM() {

            Array<double> A = counter(1.0, 1.0, 4, 5, order: StorageOrders.RowMajor);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor);

            var oldCA = A.Storage.m_handles[0].Pointer;
            // don't do anything 
            A.Storage.EnsureStorageOrder(StorageOrders.RowMajor);
            Assert.IsTrue(object.Equals(A.Storage.m_handles[0].Pointer, oldCA));

            A.Storage.EnsureStorageOrder(StorageOrders.ColumnMajor);
            Assert.IsTrue(!object.Equals(A.Storage.m_handles[0].Pointer, oldCA));

            Assert.IsTrue(A.Equals(counter(1.0, 1.0, 4, 5, StorageOrders.RowMajor)));

        }

        [TestMethod]
        public void SubarraySetRange_MLTest_2D_r0endr1end_Bdims0() {
            Array<double> A = counter(1.0, 1.0, 4, 3);

            A[r(0, end), r(1, end-1)] = -9;

            Array<double> R = new double[,] {
                { 1,-9,9 },
                { 2,-9,10},
                { 3,-9,11},
                { 4,-9,12},
            };
            Assert.IsTrue(R.Equals(A)); 
        }
        [TestMethod]
        public void SubarraySetRange_ML_2D_BaseArrayEndExpr() {
            Array<double> A = counter(1.0, 1.0, 4, 3);

            A[end, r(1, end-1)] = -9;

            Array<double> R = new double[,] {
                { 1,5,9 },
                { 2,6,10},
                { 3,7,11},
                { 4,-9,12},
            };
            Assert.IsTrue(R.Equals(A)); 
        }
        [TestMethod]
        public void SetRange_ML_endSpecMultipleAll() {
            Array<double> A = counter(1.0, 1.0, 5, 4);
            // end via BaseArray
            A[end, end + 1, "0"] = 99;
            Assert.IsTrue(A.GetValue(-2, -1) == 0);
            Assert.IsTrue(A.GetValue(-1, -1) == 99);

            // end via DimSpec
            A[end, end + 1, 0] = 100;
            Assert.IsTrue(A.GetValue(-2, -1) == 0);
            Assert.IsTrue(A.GetValue(-1, -1) == 100);
            Assert.IsTrue(A.GetValue(-1, -2) == 99);

        }
        [TestMethod]
        public void SubarraySetRange_MLTest_2D_r0endr1end_Bdims1() {
            Array<double> A = counter(1.0, 1.0, 4, 3);
            Array<double> B = new double[] { -9, -8, -7, -6 }; 

            A[r(0, -1), r(end-1, end-1)] = B;
            Array<double> R = new double[,] {
                { 1,-9,9 },
                { 2,-8,10},
                { 3,-7,11},
                { 4,-6,12},
            };
            Assert.IsTrue(R.Equals(A)); 
        }
        [TestMethod]
        public void SubarraySetRange_MLTest_2D_r0endr1end_Bdims2() {
            Array<double> A = counter(1.0, 1.0, 4, 3);
            Array<double> B = new double[,] {
                { -5, -9 },
                { -6, -10 },
                { -7, -11 }
            }; 

            A[r(0, -2), r(1, -1)] = B;
            Array<double> R = new double[,] {
                { 1,-5,-9 },
                { 2,-6,-10},
                { 3,-7,-11},
                { 4, 8, 12},
            };
            Assert.IsTrue(R.Equals(A)); 
        }
        [TestMethod]
        public void SubarraySetRange_MLTest_2D_r0S2endr1end_Bdims2() {
            Array<double> A = counter(1.0, 1.0, 4, 3);
            Array<double> B = new double[,] {
                { -5, -9 },
                { -7, -11 },
            }; 

            A[r(0, 2, -2), r(1, -1)] = B;
            Array<double> R = new double[,] {
                { 1,-5,-9 },
                { 2, 6, 10},
                { 3,-7,-11},
                { 4, 8, 12},
            };
            Assert.IsTrue(R.Equals(A)); 
        }
        [TestMethod]
        public void SubarraySetRange_MLTest_2D_r0S2endr1end_Bdims3() {
            Array<double> A = counter(1.0, 1.0, 4, 3);
            Array<double> B = new double[,,] { {
                { -5, -9 },
                { -7, -11 },
            } };
            B.a = B.T; // flips the leading singleton dim to end

            A[r(0, 2, -2), r(1, -1)] = B;
            Array<double> R = new double[,] {
                { 1,-5,-9 },
                { 2, 6, 10},
                { 3,-7,-11},
                { 4, 8, 12},
            };
            Assert.IsTrue(R.Equals(A)); 
        }
        [TestMethod]
        public void SubarraySetRange_MLTest_2D_fullr1end_Bdims3() {
            Array<double> A = counter(1.0, 1.0, 4, 3);
            Array<double> B = new double[,,] { {
                { -5, -9 },
                { -6, -10 },
                { -7, -11 },
                { -8, -12 },
            } };
            B.a = B.T; // flips the leading singleton dim to end

            A[full, r(1, -1)] = B;
            Array<double> R = new double[,] {
                { 1,-5, -9 },
                { 2,-6, -10},
                { 3,-7, -11},
                { 4,-8, -12},
            };
            Assert.IsTrue(R.Equals(A)); 
        }
        [TestMethod]
        public void SubarraySetRange_MLTest_2D_r1endfull_Bdims3() {
            Array<double> A = counter(1.0, 1.0, 4, 3);
            Array<double> B = new double[,,] { {
                { -1, -5, -9 },
                { -3, -7,-11 },
            } };
            B.a = B.T; // flips the leading singleton dim to end

            A[r(0,2, -1),full] = B;
            Array<double> R = new double[,] {
                { -1,-5,-9 },
                {  2, 6, 10},
                { -3,-7,-11},
                {  4, 8, 12},
            };
            Assert.IsTrue(R.Equals(A)); 
        }

        #region ML specific
        [TestMethod]
        public void SubarraySetRange_MLTest_Exp2D_r1end_Bdims0() {
            Array<double> A = counter(1.0, 1.0, 4, 3);
            Array<double> B = -9;

            A[r(6, 6), 1] = B;
            Array<double> R = new double[,] {
                {  1, 5, 9 },
                {  2, 6, 10},
                {  3, 7, 11},
                {  4, 8, 12},
                {  0, 0, 0},
                {  0, 0, 0},
                {  0, -9, 0},
            };
            Assert.IsTrue(R.Equals(A), $"A:{A.ToString()} - R:{R.ToString()}");

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SubarraySetRange_MLAmbigousDimensionExpandTestFail() {

            Array<double> A = counter(1.0, 1.0, 4, 3, 2);
            A[full, r(1, 10)] = -1;

        }
        #endregion

        #region simple over all dims overloads

        [TestMethod]
        public void SetRange_ML_1Dnp_1end() {
            Array<float> A;
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                A = tosingle(counter(0.0, 0.0, 10)); 
            }
            Assert.IsTrue(A.S[0] == 10 && A.S.NumberOfDimensions == 1);
            Array<float> B = tosingle(counter(1.0, 1.0, 9));
            A[r(1, end)] = B;
            Assert.IsTrue(A[r(1, end)].Equals(B)); 
        }
        [TestMethod]
        public void SetRange_ML_1Dml_1end() {
            Array<float> A = tosingle(counter(0.0, 0.0, 10));

            Assert.IsTrue(A.S[0] == 10 && A.S.NumberOfDimensions == 2);
            Array<float> B = tosingle(counter(1.0, 1.0, 9));
            A[r(1, end)] = B;
            Assert.IsTrue(A[r(1, end)].Equals(B)); 
        }
        [TestMethod]
        public void SetRange_ML_2Dml_1end() {
            Array<float> A = tosingle(counter(0.0, 0.0, 10, 9));

            Assert.IsTrue(A.S[0] == 10 && A.S[1] == 9 && A.S.NumberOfDimensions == 2);
            Array<float> B = tosingle(counter(1.0, 1.0, 9, 8));
            A[r(1, end), r(1,end)] = B;
            Assert.IsTrue(A[r(1, end), r(1,end)].Equals(B)); 
        }
        [TestMethod]
        public void SetRange_ML_3Dml_1end() {
            Array<float> A = tosingle(counter(0.0, 0.0, 10, 9, 8));

            Assert.IsTrue(A.S[0] == 10 && A.S[1] == 9 && A.S[2] == 8 && A.S.NumberOfDimensions == 3);
            Array<float> B = tosingle(counter(1.0, 1.0, 9, 8, 7));
            A[r(1, end), r(1, end), r(1, end)] = B;
            Assert.IsTrue(A[r(1, end), r(1, end), r(1, end)].Equals(B));
        }
        [TestMethod]
        public void SetRange_ML_4Dml_1end() {
            Array<float> A = tosingle(counter(0.0, 0.0, 10, 9, 8, 7));

            Array<float> B = tosingle(counter(1.0, 1.0, 9, 8, 7, 6));
            A[r(1, end), r(1, end), r(1, end), r(1, end)] = B;
            Assert.IsTrue(A[r(1, end), r(1, end), r(1, end), r(1, end)].Equals(B));
        }
        [TestMethod]
        public void SetRange_ML_5Dml_1end() {
            Array<float> A = tosingle(counter(0.0, 0.0, 10, 9, 8, 7, 6));

            Array<float> B = tosingle(counter(1.0, 1.0, 9, 8, 7, 6, 5));
            A[r(1, end), r(1, end), r(1, end), r(1, end), r(1, end)] = B;
            Assert.IsTrue(A[r(1, end), r(1, end), r(1, end), r(1, end), r(1, end)].Equals(B));
        }
        [TestMethod]
        public void SetRange_ML_6Dml_1end() {
            Array<float> A = tosingle(counter(0.0, 0.0, 10, 9, 8, 7, 6, 5));

            Array<float> B = tosingle(counter(1.0, 1.0, 9, 8, 7, 6, 5, 4));
            A[r(1, end), r(1, end), r(1, end), r(1, end), r(1, end), r(1, end)] = B;
            Assert.IsTrue(A[r(1, end), r(1, end), r(1, end), r(1, end), r(1, end), r(1, end)].Equals(B));
        }
        [TestMethod]
        public void SetRange_ML_7Dml_1end() {
            Array<float> A = tosingle(counter(0.0, 0.0, 10, 9, 8, 7, 6, 5, 4));

            Array<float> B = tosingle(counter(1.0, 1.0, 9, 8, 7, 6, 5, 4, 3));
            A[r(1, end), r(1, end), r(1, end), r(1, end), r(1, end), r(1, end), r(1, end)] = B;
            Assert.IsTrue(A[r(1, end), r(1, end), r(1, end), r(1, end), r(1, end), r(1, end), r(1, end)].Equals(B));
        }

        #endregion

        #region simple broadcasting, all dims, full dim
        [TestMethod]
        public void SetRange_ML_simpleBroadCastingAllDimsNTypes1() {
            TestBroadcastingAllDimsOneType(tocomplex);
        }
        [TestMethod]
        public void SetRange_ML_simpleBroadCastingAllDimsNTypes2() {
            TestBroadcastingAllDimsOneType(tofcomplex);
        }
        [TestMethod]
        public void SetRange_ML_simpleBroadCastingAllDimsNTypes3() {
            TestBroadcastingAllDimsOneType(toint64);
        }
        [TestMethod]
        public void SetRange_ML_simpleBroadCastingAllDimsNTypes4() {
            TestBroadcastingAllDimsOneType(todouble);
        }
        [TestMethod]
        public void SetRange_ML_simpleBroadCastingAllDimsNTypes5() {
            TestBroadcastingAllDimsOneType(touint64);
        }
        [TestMethod]
        public void SetRange_ML_simpleBroadCastingAllDimsNTypes6() {
            TestBroadcastingAllDimsOneType(tosingle);
        }
        [TestMethod]
        public void SetRange_ML_simpleBroadCastingAllDimsNTypes7() {
            TestBroadcastingAllDimsOneType(touint32);
        }
        [TestMethod]
        public void SetRange_ML_simpleBroadCastingAllDimsNTypes8() {
            TestBroadcastingAllDimsOneType(touint16);
        }
        [TestMethod]
        public void SetRange_ML_simpleBroadCastingAllDimsNTypes9() {
            TestBroadcastingAllDimsOneType(toint16);
        }
        [TestMethod]
        public void SetRange_ML_simpleBroadCastingAllDimsNTypes10() {
            TestBroadcastingAllDimsOneType(toint32);
        }
        [TestMethod]
        public void SetRange_ML_simpleBroadCastingAllDimsNTypes11() {
            TestBroadcastingAllDimsOneType(touint8);
        }
        [TestMethod]
        public void SetRange_ML_simpleBroadCastingAllDimsNTypes12() {
            TestBroadcastingAllDimsOneType(toint8);
        }
        void TestBroadcastingAllDimsOneType<T>(Func<InArray<double>,Array<T>> conv) {
            // this creates arrays of each dimensionality and performs 
            // broadcasting assignment with a single slice from any index from any dimension on it. 
            // The result is evaluated by comparing each slice of it with the source of the broadcasting. 
            // This test gets close to a "full parameter space test"- hence, be patient, please! 
            Array<T> A = conv(counter(1.0, 1.0, 7));
            for (uint i = 0; i < A.S.NumberOfDimensions + 1; i++) {
                TestFullDim<T>(A, i);
            }
            A = conv(counter(1.0, 1.0, 7, 2));
            for (uint i = 0; i < A.S.NumberOfDimensions + 1; i++) {
                TestFullDim<T>(A, i);
            }
            A = conv(counter(1.0, 1.0, 7, 2, 6));
            for (uint i = 0; i < A.S.NumberOfDimensions + 1; i++) {
                TestFullDim<T>(A, i);
            }
            A = conv(counter(1.0, 1.0, 7, 2, 6, 5));
            for (uint i = 0; i < A.S.NumberOfDimensions + 1; i++) {
                TestFullDim<T>(A, i);
            }
            A = conv(counter(1.0, 1.0, 7, 2, 6, 5, 4));
            for (uint i = 0; i < A.S.NumberOfDimensions + 1; i++) {
                TestFullDim<T>(A, i);
            }
            A = conv(counter(1.0, 1.0, 7, 2, 6, 5, 4, 3));
            for (uint i = 0; i < A.S.NumberOfDimensions + 1; i++) {
                TestFullDim<T>(A, i);
            }
            A = conv(counter(1.0, 1.0, 7, 2, 6, 5, 4, 3, 2));
            for (uint i = 0; i < A.S.NumberOfDimensions + 1; i++) {
                TestFullDim<T>(A, i);
            }

        }
        void TestFullDim<T>(InArray<T> A, uint dim) {
            for (uint i = 0; i < A.S[dim]; i++) {
                using (Scope.Enter()) {
                    Array<T> copyA = A.C; 
                    // create a dims spec array for all but the 'dim's dimensions
                    DimSpec[] dims = new DimSpec[Math.Max(dim, A.S.NumberOfDimensions)];
                    for (int d = 0; d < dims.Length; d++) {
                        if (d == dim) {
                            dims[d] = i;
                        } else {
                            dims[d] = r(0, end);
                        }
                    }
                    Array<T> rhs = A[dims];
                    copyA[ellipsis] = rhs;

                    for (int k = 0; k < A.S[dim]; k++) {
                        dims = new DimSpec[Math.Max(dim, A.S.NumberOfDimensions)];
                        for (int d = 0; d < dims.Length; d++) {
                            if (d == dim) {
                                dims[d] = k;
                            } else {
                                dims[d] = r(0, end);
                            }
                        }
                        Assert.IsTrue(copyA[dims].Equals(rhs)); 
                    }
                    System.Diagnostics.Debug.WriteLine($"Broadcasting performed on {A.S.NumberOfDimensions}-d array ({A.S.ToString()}), dim: {dim},index:{i}.");
                }
            }
        }

        [TestMethod]
        public void SetRange_ML_BaseArray_BC_ScalarTo4D_expand() {
            Array<float> A = 1;
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                A = 1; // make a true numpy scalar
            }
            Assert.IsTrue(A.S.NumberOfDimensions == 0); 
            Assert.IsTrue(A.S.NumberOfElements == 1);

            A[0, 0, 0, 3] = -1;
            Assert.IsTrue(A.S.NumberOfDimensions == 4);
            Assert.IsTrue(A.S.NumberOfElements == 4);
            Assert.IsTrue(A.GetValue(0) == 1); 
            Assert.IsTrue(A.GetValue(-1) == -1);
        }
        [TestMethod]
        public void SetRange_ML_BC_Scalar_SetAndExpand() {
            Array<float> AS = 1, BS = 1, AS2 = 1, BS2 = 1;
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                AS = 1; // make a true numpy scalar
                BS = -1; 
            }

            AS[r(2,2), 2] = BS; // expanding via DimSpec
            AS[r(2,2), "3"] = BS; // expanding via BaseArray

            BS["0", 0] = -1; // set via BA
            Assert.IsTrue(BS.GetValue(0) == -1);
            Assert.IsTrue(BS.S[0] == 1 && BS.S[1] == 1 &&  BS.S.NumberOfDimensions == 0); // still a np.scalar

            BS[r(0, 0), 0, 0] = -11; // set via DimSpec
            Assert.IsTrue(BS.GetValue(0) == -11);
            Assert.IsTrue(BS.S[0] == 1 && BS.S[1] == 1 && BS.S.NumberOfDimensions == 0); // still a np.scalar

            BS[0, 0, 0, 0] = -111;
            Assert.IsTrue(BS.GetValue(0) == -111);
            Assert.IsTrue(BS.S[0] == 1 && BS.S[1] == 1 && BS.S.NumberOfDimensions == 0); // still a np.scalar

        }
        [TestMethod]
        public void SetRange_ML_BC_EmptySetScalar() {

            Array<double> A = 1, B = 1;
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                A = 1; // make a true numpy scalar
            }
            B[full, 0] = null; // makes [0,0]
            Assert.IsTrue(B.IsEmpty); 
            Assert.IsTrue(B.S[0] == 0); 
            Assert.IsTrue(B.S[1] == 0);

            A[r(0,5)] = -11; // expand numpy scalar
            Assert.IsTrue(!A.IsEmpty);
            Assert.IsTrue(A.S.NonSingletonDimensions == 1);
            Assert.IsTrue(A.S[0] == 6);
            Assert.IsTrue(A.S[1] == 1);

            B[r(0, 2), 0] = -12;
            Assert.IsTrue(!B.IsEmpty);
            Assert.IsTrue(B.S.NumberOfDimensions == 2);
            Assert.IsTrue(B.S[0] == 3);
            Assert.IsTrue(B.S[1] == 1);
            Assert.IsTrue(B.GetValue(0) == -12); 
            Assert.IsTrue(B.GetValue(1) == -12); 
            Assert.IsTrue(B.GetValue(2) == -12);

            B[full, full] = null;
            B[2, 0] = -12;  // expansion via uint indices
            Assert.IsTrue(!B.IsEmpty);
            Assert.IsTrue(B.S.NumberOfDimensions == 2);
            Assert.IsTrue(B.S[0] == 3);
            Assert.IsTrue(B.S[1] == 1);
            Assert.IsTrue(B.GetValue(0) == 0);
            Assert.IsTrue(B.GetValue(1) == 0);
            Assert.IsTrue(B.GetValue(2) == -12);
        }

        [TestMethod]
        public void SetRange_ML_NonMatchingShapeRS() {
            Array<float> A = tosingle(counter(1.0, 1.0, 5, 4));
            Array<float> B = tosingle(counter(-1.0, -1.0, 10, 2));
            A[full, full] = B;

            // Matlab allows the right side to be be of different size as long 
            // as the number of elements match. It takes the values via a 
            // column major iterator in this case. 
            Assert.IsTrue(A.Equals(tosingle(counter(-1.0, -1.0, 5, 4))));

            A = tosingle(counter(1.0, 1.0, 5, 4, 3, 2, 1, 2));
            B = tosingle(counter(-1.0, -1.0, 10, 2, 3, 1, 2, 2));
            A[full, r(0, end), ellipsis] = B[full, full]; 
            Assert.IsTrue(A.Equals(tosingle(counter(-1.0, -1.0, 5, 4, 3, 2, 1, 2))));
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetRange_ML_BaseArray_RigthSideNoMatchFail() {
            Array<int> A = toint32(counter<int>(1, 1, 5, 4));
            Array<int> B = toint32(counter<int>(1, 1, 3, 2)); // wrong size! 
            A["1,2", "1,2"] = B;
 
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetRange_ML_DimSpec_RigthSideNoMatchFail() {
            Array<int> A = toint32(counter<int>(1, 1, 5, 4));
            Array<int> B = toint32(counter<int>(1, 1, 3, 2)); // wrong size! 
            A[r(1, 2), r(1, 2)] = B;
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetRange_ML_UIntIndx_RigthSideNoMatchFail() {
            Array<int> A = toint32(counter<int>(1, 1, 5, 4));
            Array<int> B = toint32(counter<int>(1, 1, 3, 2)); // wrong size! 
            A[1, 2] = B;
        }
        [TestMethod]
        public void SetRange_ML_BaseArrayIndicesTypes() {
            // are all index array element types implemented (index iterators)
            Array<double> A = counter(-1.0, -1.0, 50);
            Array<double> B = counter(0.0, 1.0, 50);
            Array<double> C = null;

            C = A.C; C[B] = B; Assert.IsTrue(C.Equals(B));
            C = A.C; C[tosingle(B)] = B; Assert.IsTrue(C.Equals(B));
            C = A.C; C[touint64(B)] = B; Assert.IsTrue(C.Equals(B));
            C = A.C; C[toint64(B)] = B; Assert.IsTrue(C.Equals(B));
            C = A.C; C[touint32(B)] = B; Assert.IsTrue(C.Equals(B));
            C = A.C; C[toint32(B)] = B; Assert.IsTrue(C.Equals(B));
            C = A.C; C[touint16(B)] = B; Assert.IsTrue(C.Equals(B));
            C = A.C; C[toint16(B)] = B; Assert.IsTrue(C.Equals(B));
            C = A.C; C[touint8(B)] = B; Assert.IsTrue(C.Equals(B));
            C = A.C; C[toint8(B)] = B; Assert.IsTrue(C.Equals(B));
        }
        #endregion
        [TestMethod]
        public void SetRange_ML_RefTypeRoundTrip() {
            Array<string> S = "0";

            S[3, 3] = "3,3";  // expanding on matrix (/Matlab scalar)
            Assert.IsTrue(S.S[0] == 4 && S.S[1] == 4);
            Assert.IsTrue((string)S.GetItem(0u) == "0"); 
            Assert.IsTrue((string)S.GetItem(-1) == "3,3");

            S[full, end - 1] = null;
            Assert.IsTrue(S.S[0] == 4 && S.S[1] == 3);
            Assert.IsTrue((string)S.GetItem(0u) == "0");
            Assert.IsTrue((string)S.GetItem(-1) == "3,3");

            S[full, r(0, end - 1)] = "hello";  // broadcasting with ref types
            Assert.IsTrue((string)S.GetItem((long)S.S.GetSeqIndex(0, -2)) == "hello"); 
            Assert.IsTrue((string)S.GetItem((long)S.S.GetSeqIndex(1, -2)) == "hello"); 
            Assert.IsTrue((string)S.GetItem((long)S.S.GetSeqIndex(2, -2)) == "hello");

            Array<long> I = new long[] { 0, 2, 1 };
            Array<string> RS = new string[] { "0", "1", "2" };
            S[end, I, 0] = RS; // non broadasting, non-expanding, array indexing
            Assert.IsTrue((string)S.GetItem((long)S.S.GetSeqIndex(-1, 0)) == "0");
            Assert.IsTrue((string)S.GetItem((long)S.S.GetSeqIndex(-1, 1)) == "2");
            Assert.IsTrue((string)S.GetItem((long)S.S.GetSeqIndex(-1, 2)) == "1");
            S[I, "-1,0,-2", 0] = RS.T; // non broadcasting, non-expanding, array indexing
            S[-2, -2] = "ein sehr langer Text kann auch schön sein. Vor allem mit Ö!";

            Array<string> S2;
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                S2 = "sCalar________________________very long text";
                Assert.IsTrue(S2.IsScalar && S2.S.NumberOfDimensions == 0);
            }
            S2[0, 2] = "E"; // expanding with true numpy scalar, int indexing
            Assert.IsTrue(S2.S[0] == 1); 
            Assert.IsTrue(S2.S[1] == 3);
            Assert.IsTrue(S2.GetValue(2) == "E");
            Assert.IsTrue(S2.GetValue(0,2) == "E");
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                S2[0,1] = "second";  // WriteTo_np working?
                Assert.IsTrue(S2.GetValue(1) == "second");
            }
            // test RefT implementation for CopyT
            Array<string> B = S.T[0, r(1, end)]; // should Ensure Column Major -> CopyTo()
            Assert.IsTrue(B.GetValue(0) == "1"); 
            Assert.IsTrue(B.GetValue(1) == "1");
            Assert.IsTrue(B.GetValue(2) == "0");

        }

        [TestMethod]
        public void SetRangeML_detach_NoAcc() {
            //ILN(enabled=false)
            Array<double> A = counter(1.0, 1.0, 5, 4, 3);
            Array<double> B = A.C;

            A[r(1, 2), full] = -1;


            Assert.IsTrue(B.Equals(counter(1.0, 1.0, 5, 4, 3)));
            Array<double> Res = new double[] { 1, -1, -1, 4, 5, 6, -1, -1, 9, 10, 11, -1, -1, 14, 15, 16, -1, -1, 19, 20, 21, -1, -1, 24, 25, 26, -1, -1, 29, 30, 31, -1, -1, 34, 35, 36, -1, -1, 39, 40, 41, -1, -1, 44, 45, 46, -1, -1, 49, 50, 51, -1, -1, 54, 55, 56, -1, -1, 59, 60 };
            Res.a = Res.Reshape(5, 4, 3);
            Assert.IsTrue(A.Equals(Res)); 
            //ILN(enabled=true)
        }

        #region  SetRange_ML BaseArray tests
        [TestMethod]
        public void SetRange_ML_BaseArray_2D_NrElementsEqual() {

            Array<short> A = toint16(counter(1.0, 1.0, 5, 4));
            Array<short> C = A.C; 
            Array<short> B = toint16(counter(-1.0, -1.0, 5, 4)); 
            C["1","0"] = -99;

            Assert.IsTrue(C.GetValue(1) == -99);
            C = A.C; C["0,1,2,3,4", full] = B; Assert.IsTrue(C.Equals(B));
            C = A.C; C["0,1,2,3,4", full] = B.T; Assert.IsTrue(C.Equals(B.T.Reshape(5,4)));
            C = A.C; C["1,2,3,4", full] = B["1:end", full]; Assert.IsTrue(C["1:end", full].Equals(B["1:end",full]));
            C = A.C; C["2,3,4", 0001] = B["2,3,4", 0001]; Assert.IsTrue(C["2:end", 0001].Equals(B["2:end", 0001]));
            C = A.C; C["3,4", 0001] = B["3:end", 0001]; Assert.IsTrue(C["3,4", 0001].Equals(B["3:end", 0001]));
            C = A.C; C[r(1,end-1), 0002] = B[r(1, end - 1), 0002]; Assert.IsTrue(C[r(1, end - 1), 0002].Equals(B[r(1, end - 1), 0002]));

        }

        [TestMethod]
        public void SetRange_ML_BaseArray_2D_NrElemEquVec01() {
            Array<double> A = counter(1.0, 1.0, 4, 3, 2);
            Array<double> B = counter<double>(-1, -1, 3, 1);
            Array<double> C = null;
            C = A.C; C["0,1,2,", 0, 0] = B;

            #region
            Array<double> Res = new double[] {
                -1,  -2 ,  -3 ,   4 ,   5 ,   6 ,   7 ,   8 ,   9 ,  10 ,   11,   12,   13,   14,   15,  16,   17,   18,   19,   20,   21,   22,   23,   24
            };
            Res.a = Res.Reshape(4, 3, 2);
            #endregion
            Assert.IsTrue(C.Equals(Res)); 
        }
        [TestMethod]
        public void SetRange_ML_BaseArray_2D_NrElemEquVec02() {
            Array<double> A = counter(1.0, 1.0, 4, 3, 2);
            Array<double> B = counter<double>(-1, -1, 3, 1);
            Array<double> C = null;
            C = A.C; C["0,1,3", 0, 0] = B;

            #region
            Array<double> Res = new double[] {
                 -1 ,  -2 ,   3,  -3 , 5 ,   6 ,   7 ,   8 ,   9 ,  10 ,   11,   12,   13,   14,   15,  16,   17,   18,   19,   20,   21,   22,   23,   24
            };
            Res.a = Res.Reshape(4, 3, 2);
            #endregion
            Assert.IsTrue(C.Equals(Res)); 
        }
        [TestMethod]
        public void SetRange_ML_BaseArray_2D_NrElemEquVec03() {
            Array<double> A = counter(1.0, 1.0, 4, 3, 2);
            Array<double> B = counter<double>(-1, -1, 3, 1);
            Array<double> C = null;
            C = A.C; C[1, "0,1,2", 0] = B; // this goes via iterators (non-broadcastable)

            #region
            Array<double> Res = new double[] {
                1,  -1 ,  3 ,   4 ,   5 ,   -2 ,   7 ,   8 ,   9 ,  -3 ,   11,   12,   13,   14,   15,  16,   17,   18,   19,   20,   21,   22,   23,   24
            };
            Res.a = Res.Reshape(4, 3, 2);
            #endregion
            Assert.IsTrue(C.Equals(Res));

            C = A.C; C[1, "0,1,2", 0] = B.Reshape(1,3); // this goes via BSD iteration (broadcastable)
            Assert.IsTrue(C.Equals(Res));

        }

        [TestMethod]
        public void SetRange_ML_BaseArray_2D_RemoveByempty() {
            Array<long> A = toint64(counter(1.0, 1.0, 5, 4, 3));
            Array<long> B = toint64(counter(1.0, 1.0, 0, 4, 3));

            Array<long> C = A.C;
            C["1,2", ":", full] = B;
            Assert.IsTrue(!C.IsEmpty); 
            Assert.IsTrue(C.S[0L] == 3); 
            Assert.IsTrue(C.S[1L] == 4); 
            Assert.IsTrue(C.S[2L] == 3); 
        }

        [TestMethod]
        public void SetRange_ML_AllSizesInclEmptyBothSides() {
            SetRangeBothSidesDo<int,float>(counter<int>(1, 1, 5, 4, 3), empty<float>(0, 5), empty<int>(0, 5)); 
            SetRangeBothSidesDo<int,float>(counter<int>(1, 1, 5, 4, 3), empty<float>(5, dim1: 0), empty<int>(5, dim1: 0)); 
            SetRangeBothSidesDo<int,float>(counter<int>(1, 1, 5, 4, 3), empty<float>(0), empty<int>(0)); 
            SetRangeBothSidesDo<int,float>(counter<int>(1, 1, 5, 4, 3), vector<float>(1), vector<int>(-1));
            using (Scope.Enter(ArrayStyles.numpy)) {
                SetRangeBothSidesDo<int, float>(counter<int>(1, 1, 5, 4, 3), 3, 5);
                SetRangeBothSidesDo<int, float>(counter<int>(1, 1, 5, 4, 3), vector<float>(1), vector<int>(-1));
            }
        }
        private void SetRangeBothSidesDo<Ta,Ti>(InArray<Ta> A, InArray<Ti> I, InArray<Ta> RS) {
            using (Scope.Enter(A,I,RS, ArrayStyles.ILNumericsV4)) {
                Assert.IsTrue(!(default(Ta) is bool));
                Assert.IsTrue(A.IsNumeric);
                Assert.IsTrue(default(Ta) is ValueType); 

                Array<Ta> Loc = A;  
                Loc[I] = RS;
                Array<Ta> S = Loc[I];

                Assert.IsTrue(S.S.IsSameSize(I.S));
                Assert.IsTrue(RS.S.NumberOfElements == I.S.NumberOfElements);
                Assert.IsTrue(allall(Loc[I] == RS));
                //Assert.IsTrue(S.flat.Equals(RS.flat)); 
            }
        }
        [TestMethod]
        public void GetRange_ML_SingleIndexPreserveShape() {
            Array<float> A = counter<float>(1f, 1f, 5, 4, 3);
            Array<int> I = ones<int>(2, 3);

            Array<float> R = A[I];
            Assert.IsTrue(R.shape.Equals(vector<long>(2, 3)));
            Assert.IsTrue(R.Equals(A.GetValue(1) * ones<float>(2, 3)));

        }
        [TestMethod]
        public void GetRange_ML_SingleNonNumericIndex_ColVecOut() {
            Array<float> A = counter<float>(1f, 1f, 5, 4, 3);
            Assert.IsTrue(A[vector<double>(1, 2, 3)].Equals(vector<float>(2,3,4)));
            Assert.IsTrue(A[vector<double>(1, 2, 3)].IsColumnVector);
            Array<float> R = A["1:3"]; 
            Assert.IsTrue(ReferenceEquals(R.Storage.m_handles, A.Storage.m_handles));
            R = A[r(1, 3)]; 
            Assert.IsTrue(ReferenceEquals(A[r(1,3)].Storage.m_handles, A.Storage.m_handles));
            // non-simple ranges copy the data
            R = A["1,2,3"]; 
            Assert.IsFalse(ReferenceEquals(A["1,2,3"].Storage.m_handles, A.Storage.m_handles));
            R = A[vector<double>(1, 2, 3)]; 
            Assert.IsFalse(ReferenceEquals(A[vector<double>(1, 2, 3)].Storage.m_handles, A.Storage.m_handles));
        }
        [TestMethod]
        public void GetRange_ML_SimpleRangeViewOptimization() {

            Array<int> A = counter<int>(1, 1, 5, 4, 3);
            Array<int> R = A["-3:end"];
            // expected: simple range -> same memory, base offset != 0, column vector shape, {3,4,5}
            Assert.IsTrue(R.Equals(vector<int>(58,59,60))); 
            Assert.IsTrue(ReferenceEquals(R.Storage.m_handles,A.Storage.m_handles)); 
            Assert.IsTrue(R.Size.BaseOffset == 57);
        }

        [TestMethod]
        public void GetRange_ML_SimpleRangeViewOptimizationRowVector() {
            Array<int> A = counter<int>(1, 1, 5, 4, 3);
            Array<int> R = A[vector<long>(11,17,23,48,0).T];
            // expected: simple range -> same memory, base offset != 0, column vector shape, {3,4,5}
            Assert.IsTrue(R.Equals(vector<int>(11, 17, 23, 48, 0).T + 1));
            Assert.IsFalse(ReferenceEquals(A[vector<int>(11, 17, 23, 48, 0).T].Storage.m_handles, A.Storage.m_handles));
            Assert.IsTrue(A[vector<int>(11, 17, 23, 48, 0).T].Size.BaseOffset == 0);
            // 5 x 4 x 3 array
            // index with index range as row vector. 
            // -> out must be row vector, attached to new memory 
        }
        [TestMethod]
        public void SetRange_ML_BaseArray_2D_Scalar() {
            Array<long> A = toint64(counter(1.0, 1.0, 4, 3, 2));
            Array<long> B = 44;

            Array<long> C = A.C;
            C["1,2", ":", 0] = B;
            Array<long> Res = new long[] { 1, 44, 44, 4, 5, 44, 44, 8, 9, 44, 44, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
            Res.a = Res.Reshape(4, 3, 2); 
            Assert.IsTrue(C.Equals(Res));
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SetRange_ML_BaseArrayDim0NegOORStartFail() {
            Array<complex> A = tocomplex(counter(0.0, -1.0, 4, 3, 2));
            A["-5,1", full] = new complex();
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SetRange_ML_BaseArrayDim0NegOOREndFail() {
            Array<complex> A = tocomplex(counter(0.0, -1.0, 4, 3, 2));
            A["1:-5", full] = new complex();
 
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SetRange_ML_BaseArrayDim0NegOOR_BAFail() {
            Array<complex> A = tocomplex(counter(0.0, -1.0, 4, 3, 2));
            Array<int> I = new int[] { 1, -5 }; 
            A[I, full] = new complex();
 
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SetRange_ML_BaseArrayDim0NegOORStartBAFail() {
            Array<complex> A = tocomplex(counter(0.0, -1.0, 4, 3, 2));
            Array<int> I = new int[] { -5, -3 }; 
            A[1, I, 1] = new complex();
        }
        [TestMethod]
        public void SetRange_ML_BaseArrayDim0ExpandAllOOR() {
            Array<double> A = counter(1.0, 1.0, 4, 2);
            Array<float> I = new float[] { 4, 5, 6 };
            A[I, 1] = -1;
            Array<double> Res = new double[] { 1, 2, 3, 4, 0, 0, 0, 5, 6, 7, 8, -1, -1, -1 };
            Res.a = Res.Reshape(7, 2);
            Assert.IsTrue(A.Equals(Res)); 
        }
        [TestMethod]
        public void SetRange_ML_cell_fullRow_StringFloatCell2() {
            Array<double> A = counter(1.0, 1.0, 3, 4);
            Array<double> B = A + 100;
            A["0:2", cellv("0:3")] = B; 
            Assert.IsTrue(A.Equals(B));

        }


        [TestMethod]
        public void SetRange_ML_BaseArray_2D_BC_Elispsis() {
            Array<double> A = counter(1.0, 1.0, 5, 4, 3);
            Array<double> C = null;
            C = A.C; C[ellipsis, "0,1,2"] = counter(-1.0, -1.0, 5, 4, 3);
            Assert.IsTrue(C.Equals(counter(-1.0, -1.0, 5, 4, 3)));

            C = A.C; C[ellipsis, counter(1.0, 1.0, 1)] = counter(-1.0, -1.0, 5, 4);
            Assert.IsTrue(C[":;:;0"].Equals(counter(1.0, 1.0, 5, 4)));
            Assert.IsTrue(C[":;:;1"].Equals(counter(-1.0, -1.0, 5, 4)));
            Assert.IsTrue(C[":;:;2"].Equals(counter((5 * 4 * 2 + 1), 1.0, 5, 4)));

            // broadcasting along the 1st dimension, picked 3rd
            A = counter(1.0, 1.0, 4, 3, 2);
            C = A.C; C[ellipsis, counter(1.0, 1.0, 1)] = counter(-1.0, -1.0, 1, 3);
            Assert.IsTrue(C[":;:;0"].Equals(counter(1.0, 1.0, 4, 3)));
            Assert.IsTrue(C["1;:;1"].Equals(counter(-1.0, -1.0, 3)));
            Assert.IsTrue(C["2;:;1"].Equals(counter(-1.0, -1.0, 3)));
            Assert.IsTrue(C["3;:;1"].Equals(counter(-1.0, -1.0, 3)));

            // broadcasting along the 2nd dimension, picked 3rd
            A = counter(1.0, 1.0, 4, 3, 2);
            C = A.C; C[ellipsis, 1] = counter(-1.0, -1.0, 4);
            Assert.IsTrue(C[":;:;0"].Equals(counter(1.0, 1.0, 4, 3)));
            Assert.IsTrue(C[":;0;1"].Equals(counter(-1.0, -1.0, 4)));
            Assert.IsTrue(C[":;1;1"].Equals(counter(-1.0, -1.0, 4)));
            Assert.IsTrue(C[":;2;1"].Equals(counter(-1.0, -1.0, 4)));

            // broadcasting along the 2nd dimension, picked 3rd
            A = counter(1.0, 1.0, 4, 3, 2);
            C = A.C; C[ellipsis, 0] = counter(-1.0, -1.0, 4);
            Assert.IsTrue(C[":;0;0"].Equals(counter(-1.0, -1.0, 4)));
            Assert.IsTrue(C[":;1;0"].Equals(counter(-1.0, -1.0, 4)));
            Assert.IsTrue(C[":;2;0"].Equals(counter(-1.0, -1.0, 4)));
            Assert.IsTrue(C[":;:;1"].Equals(counter((4 * 3 + 1.0), 1.0, 4, 3)));

        }

        [TestMethod]
        public void SetRange_ML_BaseArray_BC_2D() {
            Array<double> A = counter(1.0, 1.0, 5, 4, 3);
            Array<double> C = null;
            // BC along 2nd dimension
            C = A.C; C["0,1,2,3,4", full] = counter(-1.0, -1.0, 5);
            Assert.IsTrue(C[":;0;0"].Equals(counter(-1.0, -1.0, 5)));
            Assert.IsTrue(C[":;1;0"].Equals(counter(-1.0, -1.0, 5)));
            Assert.IsTrue(C[":;2;0"].Equals(counter(-1.0, -1.0, 5)));
            Assert.IsTrue(C[":;3;0"].Equals(counter(-1.0, -1.0, 5)));
            Assert.IsTrue(C[":;0;1"].Equals(counter(-1.0, -1.0, 5)));
            Assert.IsTrue(C[":;1;1"].Equals(counter(-1.0, -1.0, 5)));
            Assert.IsTrue(C[":;2;1"].Equals(counter(-1.0, -1.0, 5)));
            Assert.IsTrue(C[":;3;1"].Equals(counter(-1.0, -1.0, 5)));
            Assert.IsTrue(C[":;0;2"].Equals(counter(-1.0, -1.0, 5)));
            Assert.IsTrue(C[":;1;2"].Equals(counter(-1.0, -1.0, 5)));
            Assert.IsTrue(C[":;2;2"].Equals(counter(-1.0, -1.0, 5)));
            Assert.IsTrue(C[":;3;2"].Equals(counter(-1.0, -1.0, 5)));
            
            // BC along 1st dimension
            C = A.C; C["0,1,2,3,4", "0,1,2,3"] = counter(-1.0, -1.0, 1, 4); // implicit 3rd dim: 0
            Assert.IsTrue(C["0;:;0"].Equals(counter(-1.0, -1.0, 1, 4)));
            Assert.IsTrue(C["1;:;0"].Equals(counter(-1.0, -1.0, 1, 4)));
            Assert.IsTrue(C["2;:;0"].Equals(counter(-1.0, -1.0, 1, 4)));
            Assert.IsTrue(C["3;:;0"].Equals(counter(-1.0, -1.0, 1, 4)));
            Assert.IsTrue(C["4;:;0"].Equals(counter(-1.0, -1.0, 1, 4)));
            Assert.IsTrue(C[":;:;1:"].Equals(counter(5 * 4 + 1, 1.0, 5, 4, 2)));

            // ML specific: indices on 2nd dim reach over to merged 3rd dim
            C = A.C; C["0,1,2,3,4", "0,1,2,3,4,7,11"] = counter(-1.0, -1.0, 1, 7); // implicit 3rd dim: 0
            Assert.IsTrue(C[0,"0,1,2,3,4,7,11"].Equals(counter(-1.0, -1.0, 7)));
            Assert.IsTrue(C[1,"0,1,2,3,4,7,11"].Equals(counter(-1.0, -1.0, 7)));
            Assert.IsTrue(C[2,"0,1,2,3,4,7,11"].Equals(counter(-1.0, -1.0, 7)));
            Assert.IsTrue(C[3,"0,1,2,3,4,7,11"].Equals(counter(-1.0, -1.0, 7)));
            Assert.IsTrue(C[4,"0,1,2,3,4,7,11"].Equals(counter(-1.0, -1.0, 7)));
            //Assert.IsTrue(C[":;:;1:"].Equals(counter(5 * 4 + 1, 1.0, 5, 4, 2)));


        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetRange_ML_BaseArray_OORFail() {
            Array<double> A = counter(1.0, 1.0, 5, 4, 3);
            A["0,1,2,3,4", "0,1,2,3,4,7,11"] = counter(-1.0, -1.0, 1, 6); // right side not broadcastable to left size
 
        }
        #endregion

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetIndexerIntegerNonScalarRightSideFails() {

            Array<double> A = counter(1.0, 1.0, 2, 2);
            A[1, 1] = counter<double>(-1, -1, 2, 3);
 
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetIndexerDimSpecNonScalarRightSideFails() {

            Array<double> A = counter(1.0, 1.0, 2, 2);
            A[r(1, 1), 1] = counter<double>(-1, -1, 2, 3);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetIndexerBaseArrayNonScalarRightSideFails() {

            Array<double> A = counter(1.0, 1.0, 2, 2);
            Array<float> I = 1; 
            A[I, 1] = counter(-1, -1.0, 1, 2);
 
        }
        [TestMethod]
        public void SetRange_ML_leadingSingletonDims() {
            Array<double> A = counter(0.0, 1.0, 1, 1, 1, 1, 5);
            Array<uint> I = new uint[] { 1, 3, 2, 0, 2 };

            DoSetRange_MLleadingSingeltinDimsTest<double, uint>(A, I, 2, (a, b) => a == b);
            DoSetRange_MLleadingSingeltinDimsTest<float, uint>(tosingle(A), I, 2, (a, b) => a == b);
            DoSetRange_MLleadingSingeltinDimsTest<uint, uint>(touint32(A), I, 2, (a, b) => a == b);
            DoSetRange_MLleadingSingeltinDimsTest<int, uint>(toint32(A), I, 2, (a, b) => a == b);
            DoSetRange_MLleadingSingeltinDimsTest<long, uint>(toint64(A), I, 2, (a, b) => a == b);
            DoSetRange_MLleadingSingeltinDimsTest<ulong, uint>(touint64(A), I, 2, (a, b) => a == b);

            Array<string> S = new string[] { "1", "-99", "3", "4", "5" };
            S.a = S.Reshape(1, 1, 1, 1, 5);
            DoSetRange_MLleadingSingeltinDimsTest<string, uint>(S, I, "2", (a, b) => a == b);
        }
        private void DoSetRange_MLleadingSingeltinDimsTest<T1, T2>(InArray<T1> A, InArray<T2> I, T1 val, Func<T1, T1, bool> eq) where T1 : IEquatable<T1> {
            //this tests the Core.Functions.Builtin.WriteToBSDIterOperators.WriteTo_BSD_Iter<T> path for the BA overloads only 
            using (Scope.Enter(A, I)) {
                // 3D with 2 leading singleton dims
                Array<T1> C = A.C;
                C = A.C; C[1] = val;        Assert.IsTrue(eq(C[1].GetValue(0), val));
                C = A.C; C[r(1, 1)] = val;  Assert.IsTrue(eq(C[r(1, 1)].GetValue(0), val));
                C = A.C; C[I] = val;        Assert.IsTrue(eq(C[I].GetValue(0), val));
                C = A.C; C[I[0]] = val;     Assert.IsTrue(eq(C[I[0]].GetValue(0), val));
                C = A.C; C[I[full]] = val;  Assert.IsTrue(eq(C[I[full]].GetValue(0), val));
                C = A.C; C["1:2,2"] = val;  Assert.IsTrue(eq(C["1:2,2"].GetValue(0), val));

                C = A.C; C[0,1] = val;          Assert.IsTrue(eq(C[0, 1].GetValue(0), val));
                C = A.C; C[0,r(1, 1)] = val;    Assert.IsTrue(eq(C[0, r(1, 1)].GetValue(0), val));
                C = A.C; C[0,I] = val;          Assert.IsTrue(eq(C[0, I].GetValue(0), val));
                C = A.C; C[I[3],I] = val;       Assert.IsTrue(eq(C[I[3], I].GetValue(0), val));
                C = A.C; C[0,I[0]] = val;       Assert.IsTrue(eq(C[0, I[0]].GetValue(0), val));
                C = A.C; C[0,I[full]] = val;    Assert.IsTrue(eq(C[0, I[full]].GetValue(0), val));
                C = A.C; C[0,"1:2,2"] = val;    Assert.IsTrue(eq(C[0, "1:2,2"].GetValue(0), val));

                C = A.C; C[0,0,1] = val;        Assert.IsTrue(eq(C[0, 0, 1].GetValue(0), val));
                C = A.C; C[0,0,r(1, 1)] = val;  Assert.IsTrue(eq(C[0, 0, r(1, 1)].GetValue(0), val));
                C = A.C; C[0,0,I] = val;        Assert.IsTrue(eq(C[0, 0, I].GetValue(0), val));
                C = A.C; C[0,0,I[0]] = val;     Assert.IsTrue(eq(C[0, 0, I[0]].GetValue(0), val));
                C = A.C; C[0,0,I[full]] = val;  Assert.IsTrue(eq(C[0, 0, I[full]].GetValue(0), val));
                C = A.C; C[0,0,"1:2,2"] = val; Assert.IsTrue(eq(C[0, 0, "1:2,2"].GetValue(0), val));

                C = A.C; C[0,0,0,1] = val;        Assert.IsTrue(eq(C[0, 0, 0, 1].GetValue(0), val));
                C = A.C; C[0,0,0,r(1, 1)] = val;  Assert.IsTrue(eq(C[0, 0, 0, r(1, 1)].GetValue(0), val));
                C = A.C; C[0,0,0,I] = val;        Assert.IsTrue(eq(C[0, 0, 0, I].GetValue(0), val));
                C = A.C; C[0,0,0,I[0]] = val;     Assert.IsTrue(eq(C[0, 0, 0, I[0]].GetValue(0), val));
                C = A.C; C[0,0,0,I[full]] = val;  Assert.IsTrue(eq(C[0, 0, 0, I[full]].GetValue(0), val));
                C = A.C; C[0,0,0,"1:2,2"] = val; Assert.IsTrue(eq(C[0, 0, 0, "1:2,2"].GetValue(0), val));

                C = A.C; C[0,0,0,0,1] = val;        Assert.IsTrue(eq(C[0, 0, 0, 0, 1].GetValue(0), val));
                C = A.C; C[0,0,0,0,r(1, 1)] = val;  Assert.IsTrue(eq(C[0, 0, 0, 0, r(1, 1)].GetValue(0), val));
                C = A.C; C[0,0,0,0,I] = val;        Assert.IsTrue(eq(C[0, 0, 0, 0, I].GetValue(0), val));
                C = A.C; C[0,0,0,0,I[0]] = val;     Assert.IsTrue(eq(C[0, 0, 0, 0, I[0]].GetValue(0), val));
                C = A.C; C[0,0,0,0,I[full]] = val;  Assert.IsTrue(eq(C[0, 0, 0, 0, I[full]].GetValue(0), val));
                C = A.C; C[0,0,0,0,"1:2,2"] = val;  Assert.IsTrue(eq(C[0, 0, 0, 0, "1:2,2"].GetValue(0), val));
            }

        }

        [TestMethod]
        public void SetRange_ML_singletonLeadDims_DimSpec_viaBSDIter() {
            // this tests WriteTo_ML_BSDIter for BaseArr indices
            Array<double> A = counter(1.0, 1.0, 1, 1, 1, 5);
            Array<double> B = counter<double>(-1, -1, 1, 2, 2);

            A[0, r(1, 4)] = B; // straight forward, same number of elements on both sides: no broadcasting, just plain shape-ignoring iteration
            Array<double> R = new double[] { 1, -1, -2, -3, -4 };
            Assert.IsTrue(A.Equals(R)); 

            A = counter(1.0, 1.0, 1, 1, 1, 5);
            A[0, r(1, 4)] = B[2];  // broadcasting!!
            R = new double[] { 1, -3, -3, -3, -3 };
            Assert.IsTrue(A.Equals(R)); 


        }
        [TestMethod]
        public void SetRange_ML_setfullOptimization() {
            Array<double> A = counter(1.0, 1.0, 5, 4);
            Array<double> B = counter(-1.0, -1.0, 5, 4, order: StorageOrders.RowMajor);

            Array<double> C = A.C;
            C[full, r(0, end)] = B;

            Assert.IsTrue(C.Equals(B)); 
        }

        [TestMethod]
        public void SetRange_ML_boolIndexSimple() {

            Array<double> A = counter(1.0, 1.0, 5, 4);
            A[A[full, 1] % 2 == 0, 3] = -1;

            Array<double> Res = new double[,] {
                { 1, 6, 11, -1 },
                { 2, 7, 12, 17 },
                { 3, 8, 13, -1 },
                { 4, 9, 14, 19 },
                { 5, 10, 15, -1 }
            }; 
        }
        [TestMethod]
        public void SetRange_ML_boolIndexBaseOffset() {

            Array<double> A = counter(1.0, 1.0, 5, 4);
            A[A[r(1, end), 1] % 2 == 0, 3] = -1;

            Array<double> Res = new double[,] {
                { 1, 6, 11, 16 },
                { 2, 7, 12, 17 },
                { 3, 8, 13, -1 },
                { 4, 9, 14, 19 },
                { 5, 10, 15, -1 }
            };
        }
        [TestMethod]
        public void SetRange_ML_boolIndexBaseOffset2() {

            Array<double> A = counter(1.0, 1.0, 5, 4);
            A[(A[r(0, end), 1] % 2 == 0)[r(1,end)], 3] = -1;

            Array<double> Res = new double[,] {
                { 1, 6, 11, 16 },
                { 2, 7, 12, 17 },
                { 3, 8, 13, -1 },
                { 4, 9, 14, 19 },
                { 5, 10, 15, -1 }
            };
        }

        [TestMethod]
        public void SetRange_SetLastDimExpansion_CM_WithDetachAccTest() {

            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {

                Array<double> A = counter<double>(1.0, 1.0, 10, 2);
                Array<double> B = A.C;  // shared buffer set. Does not! lead to detaching A.

                A.SetRange(-1.0, 14); // out of range of dim0

                Assert.IsTrue(A.GetValue(14) == -1.0);
                Assert.IsTrue(B.GetValue(14) == 15);

            }

        }
        [TestMethod]
        public void SetRange_SetLastDimExpansion_CM_WithDetachTest_NoAcc() {
            //ILN(enabled=false)
            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {

                Array<double> A = counter<double>(1.0, 1.0, 10, 2);
                Array<double> B = A.C;  // shared buffer set leads to Detaching A

                var aStor = A.Storage;
                var bStor = B.Storage;
                Assert.IsTrue(aStor.Handles.ReferenceCount > 1);

                A.SetRange(-1.0, 14); // out of range of dim0

                Assert.IsTrue(A.GetValue(14) == -1.0);
                Assert.IsTrue(B.GetValue(14) == 15);
                Assert.IsTrue(A.Storage.ID != aStor.ID);
                Assert.IsTrue(B.Storage.ID == bStor.ID);

            }
            //ILN(enabled=true)
        }
        [TestMethod]
        public void SetRange_SetLastDimExpansion_RM_WithDetachAccTest() {

            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {

                Array<double> A = counter<double>(1.0, 1.0, 10, 2, StorageOrders.RowMajor);
                Array<double> B = A.C;  // shared buffer set leads to Detaching A

                A.SetRange(-1.0, 14); // out of range of dim0

                Assert.IsTrue(A.GetValue(14) == -1.0);
                Assert.IsTrue(B.GetValue(14) == 10.0);

            }

        }
        [TestMethod]
        public void SetRange_SetLastDimExpansion_RM_WithDetachTest_NoAcc() {
            //ILN(enabled=false)
            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {

                Array<double> A = counter<double>(1.0, 1.0, 10, 2, StorageOrders.RowMajor);
                Array<double> B = A.C;  // shared buffer set leads to Detaching A

                var aStor = A.Storage;
                var bStor = B.Storage;
                Assert.IsTrue(aStor.Handles.ReferenceCount > 1);

                A.SetRange(-1.0, 14); // out of range of dim0

                Assert.IsTrue(A.GetValue(14) == -1.0);
                Assert.IsTrue(B.GetValue(14) == 10.0);
                Assert.IsTrue(A.Storage.ID != aStor.ID);
                Assert.IsTrue(B.Storage.ID == bStor.ID);

            }
            //ILN(enabled=true)

        }
        [TestMethod]
        public void SetRange_Expand_CM_WithDetachAccTest() {

            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {

                Array<double> A = counter<double>(1.0, 1.0, 10, 2, StorageOrders.ColumnMajor);
                Array<double> B = A.C;  // shared buffer set leads to Detaching A

                A.SetRange(-1.0, 14, 1); // out of range of dim0 -> expand

                Assert.IsTrue(A.GetValue(14, 1) == -1.0);
                Assert.IsTrue(B.GetValue(-1,-1) == 20.0);
            }

        }
        [TestMethod]
        public void SetRange_Expand_CM_WithDetachTest_NoAcc() {
            //ILN(enabled=false)

            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {

                Array<double> A = counter<double>(1.0, 1.0, 10, 2, StorageOrders.ColumnMajor);
                Array<double> B = A.C;  // shared buffer set leads to Detaching A

                var aStor = A.Storage;
                var bStor = B.Storage;
                Assert.IsTrue(aStor.Handles.ReferenceCount > 1);

                A.SetRange(-1.0, 14, 1); // out of range of dim0 -> expand

                Assert.IsTrue(A.GetValue(14, 1) == -1.0);
                Assert.IsTrue(B.GetValue(-1,-1) == 20.0);
                Assert.IsTrue(A.Storage.ID != aStor.ID);   // in sequential code A's storage was exchanged for expand / for protecting B from changes. Acc code does not! 
                Assert.IsTrue(B.Storage.ID == bStor.ID);

            }
            //ILN(enabled=true)

        }
        [TestMethod]
        public void SetRange_Expand_RM_WithDetachTest_NoAcc() {
            //ILN(enabled=false)

            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {

                Array<double> A = counter<double>(1.0, 1.0, 10, 2, StorageOrders.RowMajor);
                Array<double> B = A.C;  // shared buffer set leads to Detaching A

                var aStor = A.Storage;
                var bStor = B.Storage;
                Assert.IsTrue(aStor.Handles.ReferenceCount > 1);

                A.SetRange(-1.0, 14, 1); // out of range of dim0

                Assert.IsTrue(A.GetValue(14, 1) == -1.0);
                Assert.IsTrue(B.GetValue(-1,-1) == 20.0);
                Assert.IsTrue(A.Storage.ID != aStor.ID);
                Assert.IsTrue(B.Storage.ID == bStor.ID);

            }
            //ILN(enabled=true)

        }
        [TestMethod]
        public void SetRange_Expand_RM_WithDetachAccTest() {

            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {

                Array<double> A = counter<double>(1.0, 1.0, 10, 2, StorageOrders.RowMajor);
                Array<double> B = A.C;  // shared buffer set leads to Detaching A

                A.SetRange(-1.0, 14, 1); // out of range of dim0

                Assert.IsTrue(A.GetValue(14, 1) == -1.0);
                Assert.IsTrue(B.GetValue(-1,-1) == 20.0);

            }

        }
        [TestMethod]
        public void SetRange_Remove_CM_WithDetachAccTest() {

            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {

                Array<double> A = counter<double>(1.0, 1.0, 10, 2, StorageOrders.ColumnMajor);
                Array<double> B = A.C;  // shared buffer set leads to Detaching A

                A.SetRange(null, 4, full); // removes 5th row

                Assert.IsTrue(A.GetValue(4, 1) == 16);
                Assert.IsTrue(B.GetValue(4, 1) == 15.0);

            }

        }
        [TestMethod]
        public void SetRange_Remove_CM_WithDetachTest_NoAcc() {
            //ILN(enabled=false)

            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {

                Array<double> A = counter<double>(1.0, 1.0, 10, 2, StorageOrders.ColumnMajor);
                Array<double> B = A.C;  // shared buffer set leads to Detaching A

                var aStor = A.Storage;
                var bStor = B.Storage;
                Assert.IsTrue(aStor.Handles.ReferenceCount > 1);

                A.SetRange(null, 4, full); // removes 5th row

                Assert.IsTrue(A.GetValue(4, 1) == 16);
                Assert.IsTrue(B.GetValue(4, 1) == 15.0);
                Assert.IsTrue(A.Storage.ID != aStor.ID);
                Assert.IsTrue(B.Storage.ID == bStor.ID);

            }
            //ILN(enabled=true)

        }
        [TestMethod]
        public void SetRange_Remove_RM_WithDetachAccTest() {

            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {

                Array<double> A = counter<double>(1.0, 1.0, 10, 2, StorageOrders.RowMajor);
                Array<double> B = A.C;  // shared buffer set leads to Detaching A

                A.SetRange(null, 4, full); // removes 5th row

                Assert.IsTrue(A.GetValue(4, 1) == 12);
                Assert.IsTrue(B.GetValue(4, 1) == 10.0);

            }

        }
        [TestMethod]
        public void SetRange_Remove_RM_WithDetachTest_NoAcc() {
            //ILN(enabled=false)

            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {

                Array<double> A = counter<double>(1.0, 1.0, 10, 2, StorageOrders.RowMajor);
                Array<double> B = A.C;  // shared buffer set leads to Detaching A

                var aStor = A.Storage;
                var bStor = B.Storage;
                Assert.IsTrue(aStor.Handles.ReferenceCount > 1);

                A.SetRange(null, 4, full); // removes 5th row

                Assert.IsTrue(A.GetValue(4, 1) == 12);
                Assert.IsTrue(B.GetValue(4, 1) == 10.0);
                Assert.IsTrue(A.Storage.ID != aStor.ID);
                Assert.IsTrue(B.Storage.ID == bStor.ID);

            }
            //ILN(enabled=true)

        }

    }
}
