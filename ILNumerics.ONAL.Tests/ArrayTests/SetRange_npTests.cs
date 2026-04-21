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

namespace ILNumerics.Core.UnitTests {

    [TestClass]
    public class SetRange_npTests : NumpyTestClass {

        [TestMethod]
        public void SetRange_np_Ranged_docuClear() {

            Array<long> I = new long[,] {
  { 1, 2, 3, 4 },
  { 5, 6, 7, 8 }
};
            Array<long> J = -I * 2;
            I = J * (J + 10);
            Array<int> A = counter<int>(0, 1, 10, 1) * ones<int>(1, 8);
            A[-1, -1] = -9;
            A[r(2, 1, end - 2), "1:3:4"] = 888888888;

            Array<uint> K = new uint[] { 5, 6 };
            A["6:7", K] = -999999999; 

            //A[ellipsis, 0] = counter<int>(0, 0, 4, 3);

            //Assert.IsTrue(A[ellipsis, 0].Equals(counter(0.0, 0.0, 4, 3)));

        }

        [TestMethod]
        public void docu_ArrayImExport() {

            unsafe {
                Array<double> A = counter<double>(1.0, 1.0, 4, 3, 2);
                double sum = 0;
                double* sA = (double*)A.GetHostPointerForRead();
                // elements are not always continously layed out! 
                // but we could iterate over the elements via A.S.Iterator:
                foreach (var i in A.S.Iterator()) {
                    sum += *(sA + i);
                }
                // sum: 300
                Assert.IsTrue(sum == 300); 
            }

        }
        [TestMethod]
        public unsafe void docu_ArrayImExport_GetHostPointerForWrite_Size_Iterator_RMstrided() {

            // creates a new array
            Array<double> A = counter<double>(1, .5, 5, 4, 6, StorageOrders.RowMajor);
            // makes it a strided array 
            A.a = A[Globals.ellipsis, Globals.r(0, 2, -1)];

            // create an iterator over the elements of A, row-major iteration order
            var it = A.S.Iterator(StorageOrders.RowMajor);
            // acquire a writable pointer to A's elements
            double* pA = (double*)A.GetHostPointerForWrite();
            long i = 0;
            // iterate over the element indices
            foreach (var idx in it) {
                // idx is the _element offset_ into pA for the current element.
                // For contiguous, row major arrays this is simply sequentially increasing 
                // with each element. For strides arrays, iteration order is arbitrary. 
                // idx takes the striding and the base offset into account. It can directly 
                // be used to index into a _typed_ pointer of the correct size. Here: double*.
                System.Diagnostics.Trace.WriteLine($"{i++}: idx:{idx} val:{pA[idx]}");
                // Since the pointer to A's elements is writeable we can change A's values: 
                pA[idx] = -i;
            }
            Assert.IsTrue(A.Equals(-counter<double>(1, 1, 5, 4, 3, StorageOrders.RowMajor)));
        }


        [TestMethod]
        public void SetRange_Subarray_logical_KeepDimsArrayStyles() {

            Array<double> A = counter<double>(1.0, 1, 4, 3); //should be “Array<double> A = counter(1.0, 1, 4,3);

            A[full, all(A != 7, dim: 0, keepdim: false)] = -A[full, all(A != 7, dim: 0, keepdim: false)];
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {
                A[full, all(A != 7)] = -A[full, all(A != 7)];
            }

        }
        #region Dimspecs
        [TestMethod]
        public void SetRange_np_detach_NoAcc() {
            //ILN(enabled=false)
            Array<double> A = counter(1.0, 1.0, 5, 4, 3);
            Array<double> B = A.C;

            A[r(1, 2), full] = -1;


            Assert.IsTrue(B.Equals(counter(1.0, 1.0, 5, 4, 3)));
            Array<double> Res = new double[] { 1, -1, -1, 4, 5, 6, -1, -1, 9, 10, 11, -1, -1, 14, 15, 16, -1, -1, 19, 20, 21, -1, -1, 24, 25, 26, -1, -1, 29, 30, 31, -1, -1, 34, 35, 36, -1, -1, 39, 40, 41, -1, -1, 44, 45, 46, -1, -1, 49, 50, 51, -1, -1, 54, 55, 56, -1, -1, 59, 60 };
            Res.a = Res.Reshape(5, 4, 3, order: StorageOrders.ColumnMajor);
            Assert.IsTrue(A.Equals(Res));
            //ILN(enabled=true)
        }
        [TestMethod]
        public void SetRange_np_detach_0_NoAcc() {
            //ILN(enabled=false)
            Array<double> A = counter(1.0, 1.0, 5, 4, 3);
            Array<double> B = A.C;
            

            A[r(1, 2), full, 0] = -1;


            Assert.IsTrue(B.Equals(counter(1.0, 1.0, 5, 4, 3)));
            Array<double> Res = new double[] { 1, -1, -1, 4, 5, 6, -1, -1, 9, 10, 11, -1, -1, 14, 15, 16, -1, -1, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60 };
            Res.a = Res.Reshape(5, 4, 3, order: StorageOrders.ColumnMajor);
            Assert.IsTrue(A.Equals(Res));
            Assert.IsTrue(B.Equals(counter(1.0, 1.0, 5, 4, 3)));
            //ILN(enabled=true)
        }
        [TestMethod]
        public void SetRange_np_detach_0_ACC() {

            Array<double> A = counter(1.0, 1.0, 5, 4, 3);
            Array<double> B = A.C;

            A[r(1, 2), full, 0] = -1;

 
            //ILN(enabled=false)
            Assert.IsTrue(B.Equals(counter(1.0, 1.0, 5, 4, 3)));
            Array<double> Res = new double[] { 1, -1, -1, 4, 5, 6, -1, -1, 9, 10, 11, -1, -1, 14, 15, 16, -1, -1, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60 };
            Res.a = Res.Reshape(5, 4, 3, order: StorageOrders.ColumnMajor);
            Assert.IsTrue(A.Equals(Res));
            Assert.IsTrue(B.Equals(counter(1.0, 1.0, 5, 4, 3)));
            //ILN(enabled=true)
        }

        [TestMethod]
        public void SetRange_np_detach_1_NoAcc() {
            //ILN(enabled=false)
            Array<double> A = counter(1.0, 1.0, 5, 4, 3);
            Array<double> B = A.C;

            A[r(1, 2, end), full, 0] = -1;


            Assert.IsTrue(B.Equals(counter(1.0, 1.0, 5, 4, 3)));
            Array<double> Res = new double[] { 1, -1, 3, -1, 5, 6, -1, 8, -1, 10, 11, -1, 13, -1, 15, 16, -1, 18, -1, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60 };
            Res.a = Res.Reshape(5, 4, 3, order: StorageOrders.ColumnMajor);
            Assert.IsTrue(A.Equals(Res));
            Assert.IsTrue(B.Equals(counter(1.0, 1.0, 5, 4, 3)));
            //ILN(enabled=true)
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetIndexerDimSpecNonScalarRightSideFails() {

            Array<double> A = counter(1.0, 1.0, 2, 2);
            A[r(1, 1), 1] = counter<double>(-1, -1, 2, 3);

        }

        [TestMethod]
        public void SetRange_np_singletonLeadDims_DimSpec_viaBSDIter() {
            // this tests WriteTo_np_BSDIter for BaseArr indices
            Array<double> A = counter(1.0, 1.0, 1, 1, 1, 5);
            Array<double> B = counter<double>(-1, -1, 1, 2, 4);

            A[ellipsis, r(1, 4)] = B[0, r(1, 1)];  // broadcasting!!  rs = {-2,-4,-6,-8}
            Array<double> R = new double[,,,] { { { { 1, -2, -4, -6, -8 } } } };
            Assert.IsTrue(A.Equals(R)); // equals compares the squeezed dimensions only!
            Assert.IsTrue(A.S.IsSameShape(R.S));
        }

        [TestMethod]
        public void SetRange_np_RangedSliceALL1_elips2() {
            Array<double> A = counter(1.0, 1.0, 4, 3, 2);

            A[ellipsis, 1] = -99;

            Assert.IsTrue(A[ellipsis, 0].Equals(counter(1.0, 1.0, 4, 3)));
            Assert.IsTrue(A[ellipsis, 1].Equals(counter(-99.0, 0.0, 4, 3)));

        }
        [TestMethod]
        public void SetRange_np_RangedSlice1AutoElips() {
            Array<double> A = counter(1.0, 1.0, 4, 3, 2);

            A[1] = -99;

            Assert.IsTrue(A[1].Equals(counter(-99.0, 0.0, 3, 2)));
            Array<double> B = counter(1.0, 1.0, 4, 3, 2);
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {
                B["1", full, full] = null;

                Assert.IsTrue(A["0,2,3", ellipsis].Equals(B));

            }

        }
        [TestMethod]
        public void SetRange_np_RangedSlice1AutoElips2() {
            Array<double> A = counter(1.0, 1.0, 4, 3, 2);

            A[full, 1] = -99;

            Assert.IsTrue(A[full, 1].Equals(counter(-99.0, 0.0, 4, 2)));
            Array<double> B = counter(1.0, 1.0, 4, 3, 2);
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {
                B[full, "1", full] = null;
                Assert.IsTrue(A[full, "0,2", ellipsis].Equals(B));
            }
        }
        [TestMethod]
        [SecuritySafeCritical]
        public void SetRange_np_FullOptim_1D() {
            Array<double> A = counter(1.0, 1.0, 4, 3, 2);
            Array<double> B = counter(-99.0, 0.0, 4, 3, 2);
            A[full] = B;

            Assert.IsTrue(A.Equals(B));
            // the "set full array optimization" should have kicked in and 
            // the storage from the right side was reused without copying 
            Assert.IsTrue(A.Storage.m_handles[0].Pointer == B.Storage.m_handles[0].Pointer);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void SetRange_np_Step0() {

            Array<double> A = 1;
            Array<double> B = A[r(0, 0, 0)];
 
        }

        [TestMethod]
        public void SetRange_np_moreDimsProvided() {
            Array<double> A = 1;
            A[0, r(0, 0), r(0, 0)] = 2;

            Assert.IsTrue(A.GetValue(0) == 2);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(A.S.NumberOfElements == 1);
        }



        [TestMethod]
        public void SetRange_np_2D_2DTest() {

            Array<double> A = counter(1.0, 1.0, 5, 4);
            Array<double> B = counter(-99.0, 0.0, 2, 2);
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 2u)) {
                A[r(1, 2), r(2, 3)] = B;
            }
            Assert.IsTrue(A[r(1, 2), r(2, 3)].Equals(B));
        }

        [TestMethod]
        public void SetRange_np_2D_2DLarge_2ThreadsTest() {

            Array<double> A = counter(1.0, 1.0, 50, 40);
            Array<double> B = counter(-99.0, 0.0, 20, 20);

            using (Settings.Ensure(() => Settings.MaxNumberThreads, 2u)) {
                A[r(end - 19, end), r(end - 19, end)] = B;
            }
            Assert.IsTrue(A[r(end - 19, end), r(end - 19, end)].Equals(B));
        }
        [TestMethod]
        public void SetRange_np_2D_2DLarge_3ThreadsTest() {

            Array<double> A = counter(1.0, 1.0, 50, 40);
            Array<double> B = counter(-99.0, 0.0, 20, 20);

            using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {
                A[r(end - 19, end), r(end - 19, end)] = B;
            }
            Assert.IsTrue(A[r(end - 19, end), r(end - 19, end)].Equals(B));
        }
        [TestMethod]
        public void SetRange_np_2D_2DLarge_4ThreadsTest() {

            Array<double> A = counter(1.0, 1.0, 50, 40);
            Array<double> B = counter(-99.0, 0.0, 20, 20);

            using (Settings.Ensure(() => Settings.MaxNumberThreads, 4u)) {
                A[r(end - 19, end), r(end - 19, end)] = B;
            }
            Assert.IsTrue(A[r(end - 19, end), r(end - 19, end)].Equals(B));
        }
        [TestMethod]
        public void SetRange_np_0D_2DTest() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = 4;
                Array<double> B = counter(-99.0, 0.0, 2, 2);
                Settings.ArrayStyle = ArrayStyles.ILNumericsV4;
                A[r(0, end), r(-1, 0)] = B[0, 0];

                Assert.IsTrue(A.GetValue(0) == -99);
                Assert.IsTrue(A.S.NumberOfDimensions == 0 && A.S.NumberOfElements == 1);
                Assert.IsTrue(A.IsScalar);

            }
        }

        [TestMethod]
        public void SubarraySetRange_MLTest_2D_r0endr1end_Bdims0() {
            Array<double> A = counter(1.0, 1.0, 4, 3);

            A[r(0, end), r(1, end - 1)] = -9;

            Array<double> R = new double[,] {
                { 1,-9,9 },
                { 2,-9,10},
                { 3,-9,11},
                { 4,-9,12},
            };
            Assert.IsTrue(R.Equals(A));
        }
        [TestMethod]
        public void SubarraySetRange_np_2D_BaseArrayEndExpr() {
            Array<double> A = counter(1.0, 1.0, 4, 3);

            A[end, r(1, end - 1)] = -9;

            Array<double> R = new double[,] {
                { 1,5,9 },
                { 2,6,10},
                { 3,7,11},
                { 4,-9,12},
            };
            Assert.IsTrue(R.Equals(A));
        }
        [TestMethod]
        public void SubarraySetRange_MLTest_2D_r0endr1end_Bdims1() {
            Array<double> A = counter(1.0, 1.0, 4, 3);
            Array<double> B = new double[] { -9, -8, -7, -6 };

            A[r(0, -1), r(end - 1, end - 1)] = B[full,null];
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
        public void SubarraySetRange_np_Test_2D_r0S2endr1end_Bdims3() {
            Array<double> A = counter(1.0, 1.0, 4, 3);
            Array<double> B = new double[,,] { {
                { -5, -9 },
                { -7, -11 },
            } };
            //B.a = B.T; // flips the leading singleton dim to end

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
        public void SubarraySetRange_np_Test_2D_r0S2endr1end_Bdims3ReorderThresh() {
            Array<double> A = counter(1.0, 1.0, 40, 30);
            // the range must address > 40 elements - the threshold for searching the smallest leading iteration dimension
            Array<double> B = counter(-1.0, -1.0, 1, 20, 29); 
            //B.a = B.T; // flips the leading singleton dim to end

            A[r(0, 2, -2), r(1, -1)] = B;
            
            Assert.IsTrue(B.Equals(A[r(0, 2, -2), r(1, -1)]));
        }
        [TestMethod]
        public void SubarraySetRange_np_Test_2D_r0S2endr1end_Bdims3ReorderThreshRowMaj() {
            Array<double> A = counter(1.0, 1.0, 40, 30, order: StorageOrders.RowMajor);
            // the range must address > 40 elements - the threshold for searching the smallest leading iteration dimension
            Array<double> B = counter(-1.0, -1.0, 1, 20, 29);
            //B.a = B.T; // flips the leading singleton dim to end

            A[r(0, 2, -2), r(1, -1)] = B;

            Assert.IsTrue(B.Equals(A[r(0, 2, -2), r(1, -1)]));
        }
        [TestMethod]
        public void SubarraySetRange_np_Test_2D_r0S2endr1end_Bdims3BCReorderThreshRowMaj() {
            Array<double> A = counter(1.0, 1.0, 40, 30, order: StorageOrders.RowMajor);
            // the range must address > 40 elements - the threshold for searching the smallest leading iteration dimension
            Array<double> B = counter(-1.0, -1.0, 1, 20, 1);
            //B.a = B.T; // flips the leading singleton dim to end

            A[r(0, 2, -2), r(1, -1)] = B;

            Assert.IsTrue(B.Equals(A[r(0, 2, -2), 1]));
            Assert.IsTrue(B.Equals(A[r(0, 2, -2), -1]));
            Assert.IsTrue(B.Equals(A[r(0, 2, -2), 15]));
        }
        [TestMethod]
        public void SubarraySetRange_MLTest_2D_fullr1end_Bdims3() {
            Array<double> A = counter(1.0, 1.0, 4, 3);
            Array<double> B = new double[,] { 
                { -5, -9 },
                { -6, -10 },
                { -7, -11 },
                { -8, -12 },
            };

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
        public void SubarraySetRange_np_2D_r1endfull_Bdims3() {
            Array<double> A = counter(1.0, 1.0, 4, 3);
            Array<double> B = new double[,] {
                { -1, -5, -9 },
                { -3, -7,-11 },
            };

            A[r(0, 2, -1), full] = B;
            Array<double> R = new double[,] {
                { -1,-5,-9 },
                {  2, 6, 10},
                { -3,-7,-11},
                {  4, 8, 12},
            };
            Assert.IsTrue(R.Equals(A));
        }
        #endregion

        #region ML specific
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SubarraySetRange_MLTest_Exp2D_r1end_Bdims0() {
            Array<double> A = counter(1.0, 1.0, 4, 3);
            Array<double> B = -9;

            A[r(6, 6), 1] = B;
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SubarraySetRange_MLAmbigousDimensionExpandTestFail() {

            Array<double> A = counter(1.0, 1.0, 4, 3, 2);
            A[full, r(1, 10)] = -1;

        }
        #endregion

        #region simple DimSpecs - over all dims overloads 

        [TestMethod]
        public void SetRange_np_1Dnp_1end() {
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
        public void SetRange_np_1Dml_1end() {
            Array<float> A = tosingle(counter(0.0, 0.0, 10));

            Assert.IsTrue(A.S[0] == 10 && A.S.NumberOfDimensions == 1);
            Array<float> B = tosingle(counter(1.0, 1.0, 9));
            A[r(1, end)] = B;
            Assert.IsTrue(A[r(1, end)].Equals(B));
        }
        [TestMethod]
        public void SetRange_np_2Dml_1end() {
            Array<float> A = tosingle(counter(0.0, 0.0, 10, 9));

            Assert.IsTrue(A.S[0] == 10 && A.S[1] == 9 && A.S.NumberOfDimensions == 2);
            Array<float> B = tosingle(counter(1.0, 1.0, 9, 8));
            A[r(1, end), r(1, end)] = B;
            Assert.IsTrue(A[r(1, end), r(1, end)].Equals(B));
        }
        [TestMethod]
        public void SetRange_np_3Dml_1end() {
            Array<float> A = tosingle(counter(0.0, 0.0, 10, 9, 8));

            Assert.IsTrue(A.S[0] == 10 && A.S[1] == 9 && A.S[2] == 8 && A.S.NumberOfDimensions == 3);
            Array<float> B = tosingle(counter(1.0, 1.0, 9, 8, 7));
            A[r(1, end), r(1, end), r(1, end)] = B;
            Assert.IsTrue(A[r(1, end), r(1, end), r(1, end)].Equals(B));
        }
        [TestMethod]
        public void SetRange_np_4Dml_1end() {
            Array<float> A = tosingle(counter(0.0, 0.0, 10, 9, 8, 7));

            Array<float> B = tosingle(counter(1.0, 1.0, 9, 8, 7, 6));
            A[r(1, end), r(1, end), r(1, end), r(1, end)] = B;
            Assert.IsTrue(A[r(1, end), r(1, end), r(1, end), r(1, end)].Equals(B));
        }
        [TestMethod]
        public void SetRange_np_5Dml_1end() {
            Array<float> A = tosingle(counter(0.0, 0.0, 10, 9, 8, 7, 6));

            Array<float> B = tosingle(counter(1.0, 1.0, 9, 8, 7, 6, 5));
            A[r(1, end), r(1, end), r(1, end), r(1, end), r(1, end)] = B;
            Assert.IsTrue(A[r(1, end), r(1, end), r(1, end), r(1, end), r(1, end)].Equals(B));
        }
        [TestMethod]
        public void SetRange_np_6Dml_1end() {
            Array<float> A = tosingle(counter(0.0, 0.0, 10, 9, 8, 7, 6, 5));

            Array<float> B = tosingle(counter(1.0, 1.0, 9, 8, 7, 6, 5, 4));
            A[r(1, end), r(1, end), r(1, end), r(1, end), r(1, end), r(1, end)] = B;
            Assert.IsTrue(A[r(1, end), r(1, end), r(1, end), r(1, end), r(1, end), r(1, end)].Equals(B));
        }
        [TestMethod]
        public void SetRange_np_7Dml_1end() {
            Array<float> A = tosingle(counter(0.0, 0.0, 10, 9, 8, 7, 6, 5, 4));

            Array<float> B = tosingle(counter(1.0, 1.0, 9, 8, 7, 6, 5, 4, 3));
            A[r(1, end), r(1, end), r(1, end), r(1, end), r(1, end), r(1, end), r(1, end)] = B;
            Assert.IsTrue(A[r(1, end), r(1, end), r(1, end), r(1, end), r(1, end), r(1, end), r(1, end)].Equals(B));
        }

        #endregion

        #region simple DimSpec broadcasting, all dims, full dim

        [TestMethod]
        public void SetRange_np_simpleBroadCastingAllDimsNTypes1() {
            TestBroadcastingAllDimsOneType(todouble);
        }
        [TestMethod]
        public void SetRange_np_simpleBroadCastingAllDimsNTypes2() {

            TestBroadcastingAllDimsOneType(tosingle);

        }
        [TestMethod]
        public void SetRange_np_simpleBroadCastingAllDimsNTypes3() {

            TestBroadcastingAllDimsOneType(touint64);

        }
        [TestMethod]
        public void SetRange_np_simpleBroadCastingAllDimsNTypes4() {

            TestBroadcastingAllDimsOneType(touint32);

        }
        [TestMethod]
        public void SetRange_np_simpleBroadCastingAllDimsNTypes5() {

            TestBroadcastingAllDimsOneType(touint16);

        }
        [TestMethod]
        public void SetRange_np_simpleBroadCastingAllDimsNTypes6() {

            TestBroadcastingAllDimsOneType(toint16);

        }
        [TestMethod]
        public void SetRange_np_simpleBroadCastingAllDimsNTypes7() {

            TestBroadcastingAllDimsOneType(toint32);

        }
        [TestMethod]
        public void SetRange_np_simpleBroadCastingAllDimsNTypes8() {

            TestBroadcastingAllDimsOneType(toint64);

        }
        [TestMethod]
        public void SetRange_np_simpleBroadCastingAllDimsNTypes9() {

            TestBroadcastingAllDimsOneType(touint8);

        }
        [TestMethod]
        public void SetRange_np_simpleBroadCastingAllDimsNTypes10() {

            TestBroadcastingAllDimsOneType(toint8);

        }
        [TestMethod]
        public void SetRange_np_simpleBroadCastingAllDimsNTypes11() {

            TestBroadcastingAllDimsOneType(tocomplex);

        }
        [TestMethod]
        public void SetRange_np_simpleBroadCastingAllDimsNTypes12() {

            TestBroadcastingAllDimsOneType(tofcomplex);
        }
        void TestBroadcastingAllDimsOneType<T>(Func<InArray<double>, Array<T>> conv) {
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
                            dims[d] = r(i,i);
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
                                dims[d] = r(k,k);
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
        [ExpectedException(typeof(ArgumentException))]
        public void SetRange_np_BC_EmptySetScalar() {

            Array<double> B = 1;
            B[full, 0] = null; // invalid in numpy mode
         
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetRange_np_NonMatchingShapeRS() {
            Array<float> A = tosingle(counter(1.0, 1.0, 5, 4));
            Array<float> B = tosingle(counter(-1.0, -1.0, 10, 2));
            A[full, full] = B;

            // Matlab allows the right side to be be of different size as long 
            // as the number of elements match. It takes the values via a 
            // column major iterator in this case. 
            // numpy requires the right side to be broadcastable.
 
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetRange_np_BaseArray_RigthSideNoMatchFail() {
            Array<int> A = toint32(counter<int>(1, 1, 5, 4));
            Array<int> B = toint32(counter<int>(1, 1, 3, 2)); // wrong size! 
            A["1:2", "1:2"] = B;
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetRange_np_DimSpec_RigthSideNoMatchFail() {
            Array<int> A = toint32(counter<int>(1, 1, 5, 4));
            Array<int> B = toint32(counter<int>(1, 1, 3, 2)); // wrong size! 
            A[r(1, 2), r(1, 2)] = B;
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetRange_np_UIntIndx_RigthSideNoMatchFail() {
            Array<int> A = toint32(counter<int>(1, 1, 5, 4));
            Array<int> B = toint32(counter<int>(1, 1, 3, 2)); // wrong size! 
            A[1, 2] = B;
        }
        [TestMethod]
        public void SetRange_np_BaseArrayIndicesTypes() {
            // are all index array element types implemented (index iterators)
            Array<double> A = counter<double>(-1.0, -1.0, 50);
            Array<double> B = counter<double>(0.0, 1.0, 50);
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

        #region  SetRange_np BaseArray tests
        [TestMethod]
        public void SetRange_np_BaseArray_2D_NrElementsEqual() {

            Array<short> A = toint16(counter(1.0, 1.0, 5, 4));
            Array<short> C = A.C;
            Array<short> B = toint16(counter(-1.0, -1.0, 5, 4));
            C["1", "0"] = -99;

            Array<int> I = new int[] { 0, 1, 2, 3, 4 }; 
            Assert.IsTrue(C.GetValue(1) == -99);
            C = A.C; C[I, full] = B;
            Assert.IsTrue(C.Equals(B));
            Assert.IsTrue(A.Equals(toint16(counter(1.0, 1.0, 5, 4)))); // detached correctly? 
                 
            C = A.C; C[I, full] = B.C; Assert.IsTrue(C.Equals(B));

        }

        [TestMethod]
        public void SetRange_np_BaseArray_2D_NrElemEquVec01() {
            Array<double> A = counter(1.0, 1.0, 4, 3, 2);
            Array<double> B = counter<double>(-1, -1, 3);
            Array<double> C = null;
            Array<int> I = new int[] { 0, 1, 2 }; 
            C = A.C; C[I, 0, 0] = B;

            #region
            Array<double> Res = new double[] {
                -1,  -2 ,  -3 ,   4 ,   5 ,   6 ,   7 ,   8 ,   9 ,  10 ,   11,   12,   13,   14,   15,  16,   17,   18,   19,   20,   21,   22,   23,   24
            };
            Res.a = Res.Reshape(4, 3, 2, order: StorageOrders.ColumnMajor);
            #endregion
            Assert.IsTrue(C.Equals(Res));
        }
        [TestMethod]
        public void SetRange_np_BaseArray_2D_NrElemEquVec02() {
            Array<double> A = counter(1.0, 1.0, 4, 3, 2);
            Array<double> B = counter<double>(-1, -1, 3);
            Array<ushort> I = new ushort[] { 0, 1, 3 }; 
            A[I, 0, 0] = B;

            #region
            Array<double> Res = new double[] {
                 -1 ,  -2 ,   3,  -3 , 5 ,   6 ,   7 ,   8 ,   9 ,  10 ,   11,   12,   13,   14,   15,  16,   17,   18,   19,   20,   21,   22,   23,   24
            };
            Res.a = Res.Reshape(4, 3, 2, order: StorageOrders.ColumnMajor);
            #endregion
            Assert.IsTrue(A.Equals(Res));
        }
        [TestMethod]
        public void SetRange_np_BaseArray_2D_NrElemEquVec03() {
            Array<double> A = counter(1.0, 1.0, 4, 3, 2);
            Array<double> B = counter<double>(-1, -1, 3);
            Array<ushort> I = new ushort[] { 0, 1, 2 }; 
            Array<double> C = A.C; C[1, I, 0] = B; 

            #region
            Array<double> Res = new double[] {
                1,  -1 ,  3 ,   4 ,   5 ,   -2 ,   7 ,   8 ,   9 ,  -3 ,   11,   12,   13,   14,   15,  16,   17,   18,   19,   20,   21,   22,   23,   24
            };
            Res.a = Res.Reshape(4, 3, 2, order:StorageOrders.ColumnMajor);
            #endregion
            Assert.IsTrue(C.Equals(Res));

            C = A.C; C[1, I, 0] = B.Reshape(1, 3); 
            Assert.IsTrue(C.Equals(Res));

        }

        [TestMethod]
        public void SetRange_np_BaseArray_2D_Scalar() {
            Array<long> A = toint64(counter(1.0, 1.0, 4, 3, 2));
            Array<long> B = 44;

            Array<long> C = A.C;
            C["1:2", full, 0] = B;
            Array<long> Res = new long[] { 1, 44, 44, 4, 5, 44, 44, 8, 9, 44, 44, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
            Res.a = Res.Reshape(4, 3, 2, order: StorageOrders.ColumnMajor);
            Assert.IsTrue(C.Equals(Res));
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SetRange_np_BaseArrayDim0NegOORStartFail() {
            Array<complex> A = tocomplex(counter(0.0, -1.0, 4, 3, 2));
            Array<sbyte> I = new sbyte[] { -5, 1 }; 
            A[I, full] = new complex();
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SetRange_np_BaseArrayDim0NegOOREndFail() {
            Array<complex> A = tocomplex(counter(0.0, -1.0, 4, 3, 2));
            A["1:-5", full] = new complex();
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SetRange_np_BaseArrayDim0NegOOR_BAFail() {
            Array<complex> A = tocomplex(counter(0.0, -1.0, 4, 3, 2));
            Array<int> I = new int[] { 1, -5 };
            A[I, full] = new complex();
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SetRange_np_BaseArrayDim0NegOORStartBAFail() {
            Array<complex> A = tocomplex(counter(0.0, -1.0, 4, 3, 2));
            Array<int> I = new int[] { -5, -3 };
            A[1, I, 1] = new complex();
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SetRange_np_BaseArrayDim0ExpandAllOOR() {
            Array<double> A = counter(1.0, 1.0, 4, 2);
            Array<float> I = new float[] { 4, 5, 6 };
            A[I, 1] = -1;
        }

        [TestMethod]
        public void SetRange_np_BaseArray_2D_BC_Elispsis() {
            Array<double> A = counter(1.0, 1.0, 5, 4, 3);
            Array<double> C = null;
            C = A.C; C[ellipsis, "0:"] = counter(-1.0, -1.0, 5, 4, 3);
            Assert.IsTrue(C.Equals(counter(-1.0, -1.0, 5, 4, 3)));

            C = A.C; C[ellipsis, counter(1.0, 1.0, 1)] = counter(-1.0, -1.0, 5, 4, 1);
            Assert.IsTrue(C[":", ":", "0"].Equals(counter(1.0, 1.0, 5, 4)));
            Assert.IsTrue(C[":", ":", "1"].Equals(counter(-1.0, -1.0, 5, 4)));
            Assert.IsTrue(C[":", ":", "2"].Equals(counter((5 * 4 * 2 + 1), 1.0, 5, 4)));

            // broadcasting along the 1st dimension, picked 3rd
            A = counter(1.0, 1.0, 4, 3, 2);
            C = A.C; C[ellipsis, counter(1.0, 1.0, 1)] = counter(-1.0, -1.0, 1, 3, 1);
            Assert.IsTrue(C[":", ":", "0"].Equals(counter(1.0, 1.0, 4, 3)));
            Assert.IsTrue(C["1", ":", "1"].Equals(counter(-1.0, -1.0, 3)));
            Assert.IsTrue(C["2", ":", "1"].Equals(counter(-1.0, -1.0, 3)));
            Assert.IsTrue(C["3", ":", "1"].Equals(counter(-1.0, -1.0, 3)));

            // broadcasting along the 2nd dimension, picked 3rd
            A = counter(1.0, 1.0, 4, 3, 2);
            C = A.C; C[ellipsis, 1] = counter(-1.0, -1.0, 3);
            Assert.IsTrue(C[":", ":", "0"].Equals(counter(1.0, 1.0, 4, 3)));
            Assert.IsTrue(C["0", ":", "1"].Equals(counter(-1.0, -1.0, 3)));
            Assert.IsTrue(C["1", ":", "1"].Equals(counter(-1.0, -1.0, 3)));
            Assert.IsTrue(C["2", ":", "1"].Equals(counter(-1.0, -1.0, 3)));

            // broadcasting along the 2nd dimension, picked 3rd
            A = counter(1.0, 1.0, 4, 3, 2);
            C = A.C; C[ellipsis, 0, 1] = counter(-1.0, -1.0, 4);
            Assert.IsTrue(C[":", ":", 0].Equals(counter(1.0, 1.0, 4, 3)));
            Assert.IsTrue(C[":", "1:end", 1].Equals(counter(13.0, 1.0, 4, 3)[full,r(1,end)]));
            Assert.IsTrue(C[":","0",1].Equals(counter(-1.0, -1.0, 4)));

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetRange_np_DimSpec_BC_RSMoreDimsErr() {
            Array<double> A = new double[] { 1, 2, 3, 4 };

            Array<double> B = counter(-1, -1.0, 3, 4);
            A[full] = B; // non broadcastable
        }
        [TestMethod]
        public void SetRange_np_DimSpec_BC_RSMoreDimsScalar() {
            Array<double> A = new double[] { 1, 2, 3, 4 };

            Array<double> B = counter(-1, -1.0, 1, 4);
            A[full] = B; // leading singletons are fine!
        }
        [TestMethod]
        public void SetRange_np_DimSpec_BC_ScalarArray() {
            Array<double> A = 1;
            Array<int> I = 0;
            A[slice(0,1)] = 2;
            Assert.IsTrue(A.GetValue(0) == 2);

            A[slice(0, 1), r(0, 0)] = counter(3.0, 1.0, 1, 1, 1, 1); 
        }

        [TestMethod]
        public void SetRange_np_BaseArray_BC_2D() {
            Array<double> A = counter(1.0, 1.0, 5, 4, 3);
            Array<double> C = null;
            // BC along 3rd last dimension
            Array<int> I = new int[] { 0, 1, 2, 3, 4 };

            C = A.C; C[I, full, 1] = counter(-1.0, -1.0, 4);
            Assert.IsTrue(C[full, full, 0].Equals(counter(1.0, 1.0, 5, 4)));
            Assert.IsTrue(C[full, full, 2].Equals(counter(41.0, 1.0, 5, 4)));
            Assert.IsTrue(C[0, full, 1].Equals(counter(-1.0, -1.0, 4)));
            Assert.IsTrue(C[1, full, 1].Equals(counter(-1.0, -1.0, 4)));
            Assert.IsTrue(C[2, full, 1].Equals(counter(-1.0, -1.0, 4)));
            Assert.IsTrue(C[3, full, 1].Equals(counter(-1.0, -1.0, 4)));
            Assert.IsTrue(C[4, full, 1].Equals(counter(-1.0, -1.0, 4)));

            // BC along 2nd dimension
            C = A.C; C[I, full] = counter(-1.0, -1.0, 4, 1);
            for (int i0 = 0; i0 < C.S[0]; i0++) {
                for (int i1 = 0; i1 < C.S[1]; i1++) {
                    for (int i2 = 0; i2 < C.S[2]; i2++) {
                        Assert.IsTrue(C.GetValue(i0, i1, i2) == -i1 - 1);
                    }
                }
            }
            C = A.C; C[full, full] = counter(-1.0, -1.0, 4, 1);
            for (int i0 = 0; i0 < C.S[0]; i0++) {
                for (int i1 = 0; i1 < C.S[1]; i1++) {
                    for (int i2 = 0; i2 < C.S[2]; i2++) {
                        Assert.IsTrue(C.GetValue(i0, i1, i2) == -i1 - 1);
                    }
                }
            }
            C = A.C; C[ellipsis] = counter(-1.0, -1.0, 4, 1);
            for (int i0 = 0; i0 < C.S[0]; i0++) {
                for (int i1 = 0; i1 < C.S[1]; i1++) {
                    for (int i2 = 0; i2 < C.S[2]; i2++) {
                        Assert.IsTrue(C.GetValue(i0, i1, i2) == -i1 - 1);
                    }
                }
            }
            C = A.C; C[":"] = counter(-1.0, -1.0, 4, 1);
            for (int i0 = 0; i0 < C.S[0]; i0++) {
                for (int i1 = 0; i1 < C.S[1]; i1++) {
                    for (int i2 = 0; i2 < C.S[2]; i2++) {
                        Assert.IsTrue(C.GetValue(i0, i1, i2) == -i1 - 1);
                    }
                }
            }
            C = A.C; C[":"] = counter(-11.0, -0.0, 1, 1, 4, 1);  // too many leading (singleton) dims are fine in numpy
            for (int i0 = 0; i0 < C.S[0]; i0++) {
                for (int i1 = 0; i1 < C.S[1]; i1++) {
                    for (int i2 = 0; i2 < C.S[2]; i2++) {
                        Assert.IsTrue(C.GetValue(i0, i1, i2) == -11);
                    }
                }
            }

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetRange_np_BaseArray_OORFail() {
            Array<double> A = counter(1.0, 1.0, 5, 4, 3);
            A["0,1,2,3,4", "0,1,2,3,4,7,11"] = counter(-1.0, -1.0, 1, 6); // right side not broadcastable to left size
        }
        #endregion

        #region SetRange_np_BA newaxis
        [TestMethod]
        public void SetRange_np_BA_ignored_newaxis() {
            Array<double> A = counter(1.0, 1.0, 2);
            Array<double> Res = new double[] { };

            A[newaxis] = Res;

            Res = counter(-1.0, -1.0, 2);
            A[newaxis, full] = counter(-1.0, -1.0, 2);
            Assert.IsTrue(A.Equals(Res));

            A[full, newaxis] = counter(-1.0, -1.0, 2, 1);
            Assert.IsTrue(A.Equals(Res));

            A[full, newaxis, newaxis] = counter(-1.0, -1.0, 2, 1, 1);
            Assert.IsTrue(A.Equals(Res));

        }
        #endregion

        #region SetRange_np_BA

        [TestMethod]
        public void SetRange_np_BA_2D() {

            Array<double> A = counter(-4.0, 1.0, 5, 6, 3)[full, r(1,-1)]; // non-0 base offset! same as counter(1, 1, 5,5,3)

            Assert.IsTrue(A.GetValue(0) == 1);
            Assert.IsTrue(A.GetValue(1) == 2);
            Assert.IsTrue(A.S.BaseOffset == 5); 

            Array<int> I1 = new int[,] {
                { 0, 1, 0 },
                { 1, 2, 1 },
                { 2, 3, 2 },
                { 3, 4, 3 },
            };
            Array<int> I2 = new int[,] {
                { 0, 1, 0 },
                { 1, 2, 1 },
                { 2, 3, 2 },
                { 3, 4, 3 },
            };
            // sets diagonal elements only
            Array<double> B = new double[,] {
                { -1, -2, -3 },
                { -4, -5, -6 },
                { -7, -8, -9 },
                { -10, -11, -12 }
            };

            Assert.IsTrue(A.GetValue(0) == 1);
            Assert.IsTrue(A.GetValue(1) == 2);
            Assert.IsTrue(A.S.BaseOffset == 5);

            // make it a little more complicated: use a detached copy of A
            Array<double> dummy = A.C; 

            A[I1, I2, 1] = B;

            Array<double> Res = new double[,] {
                { -3, 36, 41, 46, 51 },
                { 32, -6, 42, 47, 52 },
                { 33, 38, -9, 48, 53 },
                { 34, 39, 44, -12, 54 },
                { 35, 40, 45, 50, -11 }
            };

            Assert.IsTrue(A[ellipsis,1].Equals(Res));
            Assert.IsTrue(A.GetValue(0) == 1);
            Assert.IsTrue(A.GetValue(1) == 2);
            Assert.IsTrue(A.S.BaseOffset == 0);
            Assert.IsTrue(dummy.GetValue(0) == 1);
            Assert.IsTrue(dummy.GetValue(1) == 2);
            Assert.IsTrue(dummy.GetValue(0,0,1) == 31);
            Assert.IsTrue(dummy.S.BaseOffset == 5);


        }
        [TestMethod]
        public void SetRange_np_BA_Full0() {
            Array<double> A = counter(1.0, 1.0, 5, 4, 3);
            Array<double> I = counter(0.0, 1.0, 5, 1, 1, StorageOrders.RowMajor);
            Array<double> B = -99;

            Array<double> Res = counter(-99.0, 0.0, 5, 4, 3);
            A[I] = B;
            Assert.IsTrue(A.Equals(Res));
        }
        [TestMethod]
        public void SetRange_np_BA_Full1() {
            Array<double> A = counter(1.0, 1.0, 5, 4, 3);
            Array<double> I = counter(0.0, 1.0, 1, 4, 1, StorageOrders.RowMajor);
            Array<double> B = -99;

            Array<double> Res = counter(-99.0, 0.0, 5, 4, 3);
            A[full, I] = B;
            Assert.IsTrue(A.Equals(Res));
        }
        [TestMethod]
        public void SetRange_np_BA_Full1_2() {
            Array<double> A = counter(1.0, 1.0, 5, 4, 3);
            Array<double> I = counter(0.0, 1.0, 4, 1, StorageOrders.RowMajor);
            Array<double> B = -99;

            Array<double> Res = counter(-99.0, 0.0, 5, 4, 3);
            A[full, I] = B;
            Assert.IsTrue(A.Equals(Res));
        }
        [TestMethod]
        public void SetRange_np_BA_Full2() {
            Array<double> A = counter(1.0, 1.0, 5, 4, 3);
            Array<double> I = counter(0.0, 1.0, 1, 1, 3, StorageOrders.RowMajor);
            Array<double> B = -99;

            Array<double> Res = counter(-99.0, 0.0, 5, 4, 3);
            A[ellipsis, I] = B;
            Assert.IsTrue(A.Equals(Res));
        }

        [TestMethod]
        public void SetRange_np_BA_Full2_1() {
            Array<double> A = counter(1.0, 1.0, 5, 4, 3);
            Array<double> I = counter(0.0, 1.0, 1, 3, StorageOrders.RowMajor);
            Array<double> B = -99;

            Array<double> Res = counter(-99.0, 0.0, 5, 4, 3);
            A[ellipsis, I] = B;
            Assert.IsTrue(A.Equals(Res));
        }
        [TestMethod]
        public void SetRange_np_BA_Full2_2() {
            Array<double> A = counter(1.0, 1.0, 5, 4, 3);
            Array<double> I = counter(0.0, 1.0, 3, StorageOrders.RowMajor);
            Array<double> B = -99;

            Array<double> Res = counter(-99.0, 0.0, 5, 4, 3);
            A[ellipsis, I] = B;
            Assert.IsTrue(A.Equals(Res));
        }
        [TestMethod]
        public void SetRange_np_BA_empty() {
            Array<double> A = counter(1.0, 1.0, 5, 4, 3);
            Array<double> I = counter(0.0, 1.0, 1, 1, 0, StorageOrders.RowMajor);
            Array<double> B = -99;

            Array<double> Res = counter(1.0, 1.0, 5, 4, 3);
            A[ellipsis,full, I] = B; // empty I does nothing
            Assert.IsTrue(A.Equals(Res));
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetRange_np_RightSideNullErr() {

            Array<float> A = new float[] { 1, 2, 3, 4 };
            Array<double> I = 1;
            A[I] = null;
        }
        [TestMethod]
        public void SetRange_np_4DimsAddressed() {

            Array<double> A = counter(1.0, 1.0, 5, 4, 3, 2);
            Array<double> I1 = counter(0.0, 1.0, 5, 1, 1, 1);
            Array<double> I2 = counter(0.0, 1.0, 1, 4, 1, 1);
            Array<double> I3 = counter(0.0, 1.0, 1, 1, 3, 1);
            Array<double> I4 = counter(0.0, 1.0, 1, 1, 1, 2);
            long i = 0;
            A[I1, I2, I3, I4] = I1;
            foreach (var a in A) { Assert.IsTrue(a == i % 5 / 1); i++; }
            A[I1, I2, I3, I4] = I2; i = 0;
            foreach (var a in A) { Assert.IsTrue(a == i % 20 / 5); i++; }
            A[I1, I2, I3, I4] = I3;
            foreach (var a in A) { Assert.IsTrue(a == i % 60 / 20); i++; }
            A[I1, I2, I3, I4] = I4;
            foreach (var a in A) { Assert.IsTrue(a == i % 120 / 60); i++; }
        }
        [TestMethod]
        public void SetRange_np_7DimsAddressed() {

            Array<double> A = counter(1.0, 1.0, 5, 4, 3, 2,3,2,2);
            Array<double> I1 = counter(0.0, 1.0, 5, 1, 1, 1, 1, 1, 1);
            Array<double> I2 = counter(0.0, 1.0, 1, 4, 1, 1, 1, 1, 1);
            Array<double> I3 = counter(0.0, 1.0, 1, 1, 3, 1, 1, 1, 1);
            Array<double> I4 = counter(0.0, 1.0, 1, 1, 1, 2, 1, 1, 1);
            Array<double> I5 = counter(0.0, 1.0, 1, 1, 1, 1, 3, 1, 1);
            Array<double> I6 = counter(0.0, 1.0, 1, 1, 1, 1, 1, 2, 1);
            Array<double> I7 = counter(0.0, 1.0, 1, 1, 1, 1, 1, 1, 2);
            long i = 0;
            A[I1, I2, I3, I4, I5, I6, I7] = I1;
            foreach (var a in A) { Assert.IsTrue(a == i % 5 / 1); i++; }
            A[I1, I2, I3, I4, I5, I6, I7] = I2; i = 0;
            foreach (var a in A) { Assert.IsTrue(a == i % 20 / 5); i++; }
            A[I1, I2, I3, I4, I5, I6, I7] = I3;
            foreach (var a in A) { Assert.IsTrue(a == i % 60 / 20); i++; }
            A[I1, I2, I3, I4, I5, I6, I7] = I4;
            foreach (var a in A) { Assert.IsTrue(a == i % 120 / 60); i++; }
            A[I1, I2, I3, I4, I5, I6, I7] = I5;
            foreach (var a in A) { Assert.IsTrue(a == i % 360 / 120); i++; }
            A[I1, I2, I3, I4, I5, I6, I7] = I6;
            foreach (var a in A) { Assert.IsTrue(a == i % 720 / 360); i++; }
            A[I1, I2, I3, I4, I5, I6, I7] = I7;
            foreach (var a in A) { Assert.IsTrue(a == i % 1440 / 720); i++; }
        }
        [TestMethod]
        public void SetRange_np_AssignScalarRetT() {

            Array<float> A = new float[] { 1, 2, 3, 4 };

            Array<double> I = 1;
            Array<float> RS = -1;
            A[I] = RS[full]; // assigns scalar RetT

            Array<float> Res = new float[] { 1, -1, 3, 4 };
            Assert.IsTrue(A.Equals(Res));

        }
        [TestMethod]
        public void SetRange_np_AssignEmptyRangeNonBCScalar() {

            Array<float> A = new float[] { 1, 2, 3, 4 };
            Array<float> Res = A.C;

            Array<double> I = new double[0];
            Array<float> RS = -1; //scalars are always broadcastable
            A[I] = RS[full]; // assigns scalar RetT to empty
            Assert.IsTrue(A.Equals(Res));
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetRange_np_AssignEmptyRangeNonBCRightSideErr() {

            Array<float> A = new float[] { 1, 2, 3, 4 };

            Array<double> I = new double[0];
            Array<float> RS = new float[] { 2f, 3f };

            A[I] = RS[full]; // assigns non - scalar, non broadcastable to I -> failure
        }
        [TestMethod]
        public void SetRange_np_BA_AllScalars() {
            Array<double> A = counter(1.0, 1.0, 2, 3, 4);
            Array<int> I0 = 0;
            Array<int> I1 = 1;
            Array<int> I2 = 2;

            A[I0, I1, I2] = 5;

            Array<double> Res = counter(1.0, 1.0, 2, 3, 4);
            Res.SetValue(5.0, 0, 1, 2);
            Assert.IsTrue(A.Equals(Res));
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetRange_np_BA_AllScalarsNonBroadcastingErr() {
            Array<double> A = counter(1.0, 1.0, 2, 3, 4);
            Array<double> B = new double[] { 1, 2 };
            Array<int> I0 = 0;
            Array<int> I1 = 1;
            Array<int> I2 = 2;

            A[I0, I1, I2] = B;
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetRange_np_BA_RigthSideTooManyDimensions() {
            Array<double> A = counter(1.0, 1.0, 2, 3, 4);
            Array<double> B = counter(1.0, 1.0, 5, 1, 1, 1);
            Array<int> I0 = 0;
            Array<int> I1 = 1;
            Array<int> I2 = 2;

            A[I0, I1, I2] = B;
        }
        [TestMethod]
        public void SetRange_np_BA_RefTypes() {
            Array<object> A = new object[]
                { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            A.a = A.Reshape(4, 3);
            Array<object> Res = new object[]
                { -1, -2, -3, -4, -5, -6, -7, -8, -9, -10, -11, -12 };
            Res.a = Res.Reshape(4, 3);

            Array<uint> I = new uint[] { 0, 1, 2, 3 };
            A[I] = Res;
            Assert.IsTrue(A.Equals(Res));
        }
        [TestMethod]
        public void SetRange_np_BA_RefTypes_BCRows() {
            Array<object> A = new object[]
                { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            A.a = A.Reshape(4, 3);
            Array<object> B = new object[]
                { -1, -2, -3 };
            B.a = B.Reshape(1, 3);

            Array<object> Res = new object[]
                { 1, 2, 3, -1, -2, -3, -1, -2, -3, 10,11,12 };
            Res.a = Res.Reshape(4, 3);

            Array<uint> I = new uint[] { 0, 1, 2 };
            A[r(1, 2), I] = B;
            Assert.IsTrue(A.Equals(Res));
        }
        [TestMethod]
        public void SetRange_np_BA_RefTypes_BCCols() {
            Array<object> A = new object[]
                { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            A.a = A.Reshape(3,4);
            Array<object> B = new object[]
                { -1, -2, -3 };
            B.a = B.Reshape(3,1);

            Array<object> Res = new object[]
                { 1, -1, -1, 4, 5, -2, -2, 8, 9, -3, -3, 12 };
            Res.a = Res.Reshape(3,4);

            Array<uint> I = new uint[] { 0, 1, 2 };
            A[I, r(1, 2)] = B;
            Assert.IsTrue(A.Equals(Res));
        }
        #endregion

        #region UInt indices
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetIndexerIntegerNonScalarRightSideFails() {

            Array<double> A = counter(1.0, 1.0, 2, 2);
            A[1, 1] = counter<double>(-1, -1, 2, 3);
 
        }

        #endregion

        [TestMethod]
        public void SetRange_np_RefTypeRoundTrip() {
            Array<string> S = "0";

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4))
                S[3, 3] = "3,3";  // expanding on matrix (/Matlab scalar)

            Assert.IsTrue(S.S[0] == 4 && S.S[1] == 4);
            Assert.IsTrue((string)S.GetItem(0u) == "0");
            Assert.IsTrue((string)S.GetItem(-1) == "3,3");

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4))
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
            //// numpy is more strict regarding the broadcastable size - NO! it's not! numpy takes this without complains!
            //RS.a = RS.Reshape(3,1); 
            S[end, I, 0] = RS; // non broadasting, non-expanding, array indexing
            Assert.IsTrue((string)S.GetItem((long)S.S.GetSeqIndex(-1, 0)) == "0");
            Assert.IsTrue((string)S.GetItem((long)S.S.GetSeqIndex(-1, 1)) == "2");
            Assert.IsTrue((string)S.GetItem((long)S.S.GetSeqIndex(-1, 2)) == "1");
            Array<int> I2 = new int[] { -1, 0, -2 };
            S[I, I2, 0] = RS.T; // non broadcasting, non-expanding, array indexing
            S[-2, -2] = "ein sehr langer Text kann auch schön sein. Vor allem mit Ö!";

            Array<string> S2 = "sCalar________________________very long text";
            Assert.IsTrue(S2.GetValue(0) == "sCalar________________________very long text");
            Assert.IsTrue(S2.Equals(S2.T) && S2.Equals(S2[0].T.GetValue(0)));
            Assert.IsTrue(S2.IsScalar && S2.S.NumberOfDimensions == 0);
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4))
                S2[0, 2] = "E";

            // test RefT implementation for CopyT
            Array<string> B = S.T[0, r(1, 3)]; // should Ensure Column Major -> CopyTo()
            Assert.IsTrue(B.GetValue(0) == "hello");
            Assert.IsTrue(B.GetValue(1) == "1");
            Assert.IsTrue(B.GetValue(2) == "0");

        }

    }
}
