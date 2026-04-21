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
using static ILNumerics.Globals;
using static ILNumerics.ILMath; 

namespace ILNumerics.Core.UnitTests {

    [TestClass]
    public class SubarrayDimSpec2D_ML_Tests {

        [TestMethod]
        public void SubarrayDimSpec2D_MLSimpleTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,] {
                    { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 }
                };

                Array<double> B = A[r(0, end), r(1, end)];
                Array<double> Res = new double[,] {
                    { 2,3 },
                    { 5,6 },
                    { 8,9 },
                    { 11,12 }
                };

                if (!B.Equals(Res)) {
                    System.Diagnostics.Debugger.Break(); 
                }

                Assert.IsTrue(B.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.S.NumberOfElements == 8);
                Assert.IsTrue(B.S.StorageOrder == StorageOrders.Other);
                Assert.IsTrue(B.S.BaseOffset == 1);
                Assert.IsTrue(B.S[0] == 4);
                Assert.IsTrue(B.S[1] == 2);
                Assert.IsTrue(!object.Equals(A.Storage, B.Storage)); 
                Assert.IsTrue(object.Equals(A.Storage.m_handles, B.Storage.m_handles));

                Assert.IsTrue(B.Equals(Res));
            }
        }

        [TestMethod]
        public void SubarrayDimSpec2D_MLFullTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,] {
                    { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 }
                };

                Array<double> B = A[full, full];

                Assert.IsTrue(!Equals(B,null) && B.IsMatrix);
                Assert.IsTrue(B.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.S[0] == 4 && B.S[1] == 3);
                Assert.IsTrue(B.S.StorageOrder == StorageOrders.RowMajor);
                // Copy was not made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles));

                ArrayAssert.ValuesEqual(A.GetArrayForRead(), new double[] { 1, 4, 7, 10, 2, 5, 8, 11, 3, 6, 9, 12 });


            }
        }

        [TestMethod]
        public void SubarrayDimSpec2D_MLFullFromColMajTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,] {
                    { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 }
                };
                A.a = A.T;

                Array<double> B = A[full, full];

                Assert.IsTrue(!Equals(B,null) && B.IsMatrix);
                Assert.IsTrue(B.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.S[0] == 3 && B.S[1] == 4);
                Assert.IsTrue(B.S.StorageOrder == StorageOrders.ColumnMajor);
                // no copy was made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles));

                ArrayAssert.ValuesEqual(A.GetArrayForRead(), new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 });
            }
        }
        [TestMethod]
        public void SubarrayDimSpec2D_MLEllipsis01Test() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,] {
                    { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 }
                };

                var B = A[full, ellipsis];

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 4 && B.Storage.S[1] == 3);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.RowMajor);
                // No copy was made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(B.Equals(A));

                B = A[ellipsis, full];
                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 4 && B.Storage.S[1] == 3);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.RowMajor);
                // No copy was made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(B.Equals(A));

            }
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SubarrayDimSpec2DML_NewaxisTestFails() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {
                Array<double> A = new double[,] {
                    { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 }
                };

                var B = A[1, newaxis];
            }

        }
        [TestMethod]
        public void SubarrayDimSpec2D_MLSingleIntTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,] {
                    { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 }
                };

                var B = A[1, 0];

                Assert.IsTrue(!Equals(B, null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 1 && B.Storage.S[1] == 1);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.ColumnMajor);
                // No copy was made 
                // Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));  <- implementation detail. A copy may be made for ACC.

                Assert.IsTrue(B.Storage.GetValue(0) == 4);
                //Assert.IsTrue(B.Storage.S.BaseOffset == A.S.GetStride(0));
                //Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles));

            }
        }
        [TestMethod]
        public void SubarrayDimSpec2D_MLSingleIntTest_NoAcc() {
            //ILN(enabled=false)
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,] {
                    { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 }
                };

                var B = A[1, 0];

                Assert.IsTrue(!Equals(B, null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 1 && B.Storage.S[1] == 1);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.ColumnMajor);
                // No copy was made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));  // < -implementation detail.A copy may be made for ACC.

                Assert.IsTrue(B.Storage.GetValue(0) == 4);
                Assert.IsTrue(B.Storage.S.BaseOffset == A.S.GetStride(0));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles));

            }
            //ILN(enabled=true)
        }
        [TestMethod]
        public void SubarrayDimSpec2D_MLSingleLongTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,] {
                    { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 }
                };

                var B = A[(long)1,-1];

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 1 && B.Storage.S[1] == 1);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.ColumnMajor);
                // No copy was made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(B.Storage.GetValue(0) == 6);
                Assert.IsTrue(B.Storage.S.BaseOffset == A.S.GetStride(0) + 2 * A.S.GetStride(1));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles));



                // identical!    // Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles));

                // identical!    // ArrayAssert.ValuesEqual(A.GetArrayForRead(), new double[] { 1, 4, 7, 10, 2, 5, 8, 11, 3, 6, 9, 12 });
            }
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SubarrayDimSpec2D_MLSingleLongOORTestFail() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,] {
                    { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 }
                };

                var B = A[(long)1, 3];

            }
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SubarrayDimSpec2D_MLSingleUIntOORTestFail() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,] {
                    { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 }
                };

                var B = A[1,3];

            }
        }

        [TestMethod]
        public void SubarrayDimSpec2D_MLSingleLongNegTestFail() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,] {
                    { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 }
                };

                var B = A[-1, -1];

                Assert.IsTrue(B.Storage.GetValue(0) == 12);

                A = new double[,,] {
                    { { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 } },
                    { { -1,2,3}, { 4,5,6 },
                    {7,8,-9 }, {10,-11,-12 } },
                };

                B = A[-1, -1];

                Assert.IsTrue(B.Storage.GetValue(0) == -12);
                B = A[-1, -2];

                Assert.IsTrue(B.Storage.GetValue(0) == -9);
            }
        }
        [TestMethod]
        public void SubarrayDimSpec2D_MLRange001Test() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,] {
                    { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 }
                };

                var B = A[0,r(0,2)];

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 1 && B.Storage.S[1] == 3);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.ColumnMajor); // vector
                // Copy was made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles));

                Assert.IsTrue(B.Storage.S.BaseOffset == 0);

                ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { 1, 2, 3 });

            }
        }

        [TestMethod]
        public void SubarrayDimSpec2D_MLRange002Test() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,] {
                    { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 }
                };

                var B = A[1,r(0,-1)];

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 1 && B.Storage.S[1] == 3);

                //Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.RowMajor);
                // Copy was made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles));

                Assert.IsTrue(B.Storage.S.BaseOffset == 3);

                ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { 4, 5, 6 });

            }
        }
        [TestMethod]
        public void SubarrayDimSpec2D_MLRange223Test() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,] {
                    {  1, 2, 3 }, 
                    {  4, 5, 6 },
                    {  7, 8, 9 }, 
                    { 10,11,12 }
                };

                var B = A[r(1,end),r(0,-1)];

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S.NumberOfElements == 9);
                Assert.IsTrue(B.Storage.S[0] == 3 && B.Storage.S[1] == 3);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.RowMajor);
                // Copy was made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles));

                Assert.IsTrue(B.Storage.S.BaseOffset == 3);

                ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { 4,7,10,5,8,11,6,9,12 });

            }
        }
        [TestMethod]
        public void SubarrayDimSpec2D_MLRangeReuseTest() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,] {
                    { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 }
                };
                var T = A.T;
                var B = T[1,r(1,end)];  // indexer acts on the RetT returned from .T

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 1 && B.Storage.S[1] == 3);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.Other);
                // Copy was made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles));

                Assert.IsTrue(B.Storage.S.BaseOffset == 4);

                ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { 5, 8, 11 });

            }
        }
        [TestMethod]
        public void SubarrayDimSpec2DML_SteppedRangeOn3DInsideFirstDimTest() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,,] {
                    { { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 } },
                    { { -1,-2,-3}, { -4,-5,-6 },
                    {-7,-8,-9 }, {-10,-11,-12 } },
                    };
                A.a = A.T;  // A is now:
                // <Double> [4, 3, 2] 1 4...
                // [0]: (:,:,0)
                // [1]:           1          2          3
                // [2]:           4          5          6
                // [3]:           7          8          9
                // [4]:          10         11         12
                // [5]: (:,:,1)
                // [6]:          -1         -2         -3
                // [7]:          -4         -5         -6
                // [8]:          -7         -8         -9
                // [9]:         -10        -11        -12

                var B = A[end,r(0, 2, 2)];  // indexer 

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 1 && B.Storage.S[1] == 2);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.Other);
                // Copy was made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles));

                Assert.IsTrue(B.Storage.S.BaseOffset == A.S.GetSeqIndex(-1,0));

                ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { 10, 12 });
            }
        }
        [TestMethod]
        public void SubarrayDimSpec2DML_SteppedRangeOn3DInsideFirstDimRetTest() {
            //ILN(enabled=false)
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,,] {
                    { { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 } },
                    { { -1,-2,-3}, { -4,-5,-6 },
                    {-7,-8,-9 }, {-10,-11,-12 } },
                    };
                // A.T is now:
                // <Double> [4, 3, 2] 1 4...
                // [0]: (:,:,0)
                // [1]:           1          2          3
                // [2]:           4          5          6
                // [3]:           7          8          9
                // [4]:          10         11         12
                // [5]: (:,:,1)
                // [6]:          -1         -2         -3
                // [7]:          -4         -5         -6
                // [8]:          -7         -8         -9
                // [9]:         -10        -11        -12

                var B = A.T[end, r(0, 2, 2)];  // T uses same storage as A, Indexer uses same storage as T

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 1 && B.Storage.S[1] == 2);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.Other);
                // Copy was not made -> Implementation detail!
                // Assert.IsTrue(object.ReferenceEquals(B, A)); //!!!

                //Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles));

                Assert.IsTrue(B.Storage.S.BaseOffset == 9);

                ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { 10, 12 });
            }
            //ILN(enabled=true)
        }
        [TestMethod]
        public void SubarrayDimSpec2DML_SteppedRangeOn3DInsideSecondDimTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,,] {
                    { { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 } },
                    { { -1,-2,-3}, { -4,-5,-6 },
                    {-7,-8,-9 }, {-10,-11,-12 } },
                    };
                A.a = A.T;  // A is now:
                // <Double> [4, 3, 2] 1 4...
                // [0]: (:,:,0)
                // [1]:           1          2          3
                // [2]:           4          5          6
                // [3]:           7          8          9
                // [4]:          10         11         12
                // [5]: (:,:,1)
                // [6]:          -1         -2         -3
                // [7]:          -4         -5         -6
                // [8]:          -7         -8         -9
                // [9]:         -10        -11        -12

                var B = A[end, r(1, 2, 5)];  // indexer 

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 1 && B.Storage.S[1] == 3);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.Other);
                // Copy was made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // reshape!

                Assert.IsTrue(B.Storage.S.BaseOffset == 7);

                ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { 11, -10, -12 });
            }
        }
        [TestMethod]
        public void SubarrayDimSpec2DML_SteppedRangeOn3DInsideSecondDimRetTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,,] {
                    { { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 } },
                    { { -1,-2,-3}, { -4,-5,-6 },
                    {-7,-8,-9 }, {-10,-11,-12 } },
                    };
                 // A.T is now:
                // <Double> [4, 3, 2] 1 4...
                // [0]: (:,:,0)
                // [1]:           1          2          3
                // [2]:           4          5          6
                // [3]:           7          8          9
                // [4]:          10         11         12
                // [5]: (:,:,1)
                // [6]:          -1         -2         -3
                // [7]:          -4         -5         -6
                // [8]:          -7         -8         -9
                // [9]:         -10        -11        -12

                var B = A.T[end-1, r(1, 2, -1)];  // indexer uses same storage

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 1 && B.Storage.S[1] == 3);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.Other);
                // A is dead here! It might be reused. But we don't know! 
                //
                //Assert.IsTrue(!object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // had to reshape -> copy

                Assert.IsTrue(B.Storage.S.BaseOffset == 6);

                ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { 8, -7, -9 });
            }
        }
        [TestMethod]
        public void SubarrayDimSpec2DML_SteppedRangeOn3DInsideSecondDimExactEndofRangeTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,,] 
                {
                    { { 1, 2, 3 }, {  4,  5,  6 },
                      { 7, 8, 9 }, { 10, 11, 12 } },
                    { {-1,-2,-3 }, { -4, -5, -6 },
                      {-7,-8,-9 }, {-10,-11,-12 } },
                };
                A.a = A.T;  // A is now:
                // <Double> [4, 3, 2] 1 4...
                // [0]: (:,:,0)
                // [1]:           1          2          3
                // [2]:           4          5          6
                // [3]:           7          8          9
                // [4]:          10         11         12
                // [5]: (:,:,1)
                // [6]:          -1         -2         -3
                // [7]:          -4         -5         -6
                // [8]:          -7         -8         -9
                // [9]:         -10        -11        -12

                var B = A[r(1,2,-1),r(1, 2, 5)];  // indexer  // performs copy, since A.T is NOT column major! 

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 2 && B.Storage.S[1] == 3);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.Other);
                // Copy was made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // had to reshape -> copy

                Assert.IsTrue(B.Storage.S.BaseOffset == 5);

                ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { 5, 11, -4, -10, -6, -12 });
            }
        }
        [TestMethod]
        public void SubarrayDimSpec2DML_SteppedRangeOn3DInsideThirsDimExactEndOfRangeNoReshapeTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,,]
                {
                    { { 1, 2, 3 }, {  4,  5,  6 },
                      { 7, 8, 9 }, { 10, 11, 12 } },
                    { {-1,-2,-3 }, { -4, -5, -6 },
                      {-7,-8,-9 }, {-10,-11,-12 } },
                };
                A.a = A.T;  // A is now:
                // <Double> [4, 3, 2] 1 4...
                // [0]: (:,:,0)
                // [1]:           1          2          3
                // [2]:           4          5          6
                // [3]:           7          8          9
                // [4]:          10         11         12
                // [5]: (:,:,1)
                // [6]:          -1         -2         -3
                // [7]:          -4         -5         -6
                // [8]:          -7         -8         -9
                // [9]:         -10        -11        -12

                // strides: [12,3,1] .T -> [3,1,12] 

                var B = A[r(1, 2, -1), r(1, 2)];  // indexer  // no copy, all in dims lengths 

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 2 && B.Storage.S[1] == 2);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.Other);
                // Copy was made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // NO copy

                Assert.IsTrue(B.Storage.S.BaseOffset == 4);

                ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { 5, 11, 6, 12 });
            }
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SubarrayDimSpec2DML_SteppedRangeOnRowVectorInsideFirstDim_2OORFailTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,,] {
                    { { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 } },
                    { { -1,-2,-3}, { -4,-5,-6 },
                    {-7,-8,-9 }, {-10,-11,-12 } },
                    };
                A.a = A.T;  // A is now:
                // <Double> [4, 3, 2] 1 4...
                // [0]: (:,:,0)
                // [1]:           1          2          3
                // [2]:           4          5          6
                // [3]:           7          8          9
                // [4]:          10         11         12
                // [5]: (:,:,1)
                // [6]:          -1         -2         -3
                // [7]:          -4         -5         -6
                // [8]:          -7         -8         -9
                // [9]:         -10        -11        -12

                var B = A[r(1, 2, 21)][r(2, 3, -1), 1];  // indexer 
            }
        }
        [TestMethod]
        public void SubarrayDimSpec2DML_SteppedRangeOnRowVectorInsideFirstDimTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,,] {
                    { { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 } },
                    { { -1,-2,-3}, { -4,-5,-6 },
                    {-7,-8,-9 }, {-10,-11,-12 } },
                    };
                A.a = A.T;  // A is now:
                // <Double> [4, 3, 2] 1 4...
                // [0]: (:,:,0)
                // [1]:           1          2          3
                // [2]:           4          5          6
                // [3]:           7          8          9
                // [4]:          10         11         12
                // [5]: (:,:,1)
                // [6]:          -1         -2         -3
                // [7]:          -4         -5         -6
                // [8]:          -7         -8         -9
                // [9]:         -10        -11        -12

                var B = A[r(1, 2, 21)][r(2,3,-1), 0];  // indexer 

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 3 && B.Storage.S[1] == 1);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.Other);
                // Copy was made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // copy for vector

                Assert.IsTrue(B.Storage.S.BaseOffset == 5); // 1 + 2 * 2
 
                // row vec: ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { 4, 10, 5, 11, 6, 12, -4, -10, -5, -11, -6 });
                ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { 5, 12, -5 });
            }
        }
        [TestMethod]
        public void SubarrayDimSpec2DML_SteppedRangeOnRowVectorInsideFirstDim2DimMinus1Test() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,,] {
                    { { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 } },
                    { { -1,-2,-3}, { -4,-5,-6 },
                    {-7,-8,-9 }, {-10,-11,-12 } },
                    };
                A.a = A.T;  // A is now:
                // <Double> [4, 3, 2] 1 4...
                // [0]: (:,:,0)
                // [1]:           1          2          3
                // [2]:           4          5          6
                // [3]:           7          8          9
                // [4]:          10         11         12
                // [5]: (:,:,1)
                // [6]:          -1         -2         -3
                // [7]:          -4         -5         -6
                // [8]:          -7         -8         -9
                // [9]:         -10        -11        -12

                var B = A[r(1, 2, 21)][r(2,3,-1), -1];  // indexer 

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 3 && B.Storage.S[1] == 1);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.Other);
                // Copy was made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // copy for vector

                Assert.IsTrue(B.Storage.S.BaseOffset == 5); // 1 + 2 * 2
 
                // row vec: ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { 4, 10, 5, 11, 6, 12, -4, -10, -5, -11, -6 });
                ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { 5, 12, -5 });
            }
        }
        [TestMethod]
        public void SubarrayDimSpec2DML_SteppedRangeOnRowVectorInsideFirstDim2DimEndTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,,] {
                    { { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 } },
                    { { -1,-2,-3}, { -4,-5,-6 },
                    {-7,-8,-9 }, {-10,-11,-12 } },
                    };
                A.a = A.T;  // A is now:
                // <Double> [4, 3, 2] 1 4...
                // [0]: (:,:,0)
                // [1]:           1          2          3
                // [2]:           4          5          6
                // [3]:           7          8          9
                // [4]:          10         11         12
                // [5]: (:,:,1)
                // [6]:          -1         -2         -3
                // [7]:          -4         -5         -6
                // [8]:          -7         -8         -9
                // [9]:         -10        -11        -12

                var B = A[r(1, 2, 21)][r(2,3,-1), end];  // indexer 

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 3 && B.Storage.S[1] == 1);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.Other);
                // Copy was made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // copy for vector

                Assert.IsTrue(B.Storage.S.BaseOffset == 5); // 1 + 2 * 2
 
                // row vec: ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { 4, 10, 5, 11, 6, 12, -4, -10, -5, -11, -6 });
                ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { 5, 12, -5 });
            }
        }
        [TestMethod]
        public void SubarrayDimSpec2DML_SteppedRangeOnRowVectorInsideSecondDimTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,,] {
                    { { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 } },
                    { { -1,-2,-3}, { -4,-5,-6 },
                    {-7,-8,-9 }, {-10,-11,-12 } },
                    };
                A.a = A[full].T;  // A is now:
               // <Double> [1, 24] 1 - 1...
               //[0]:           1 - 1          4 - 4          7 - 7         10 - 10          2 - 2          5 - 5          8 - 8         11 - 11          3 - 3          6 - 6          9 - 9         12 - 12

                var B = A[0, r(1, 2, 21)];  // indexer 

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 1 && B.Storage.S[1] == 11);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.Other);
                // Copy was made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // had to reshape -> but without copy, A was column maj. 

                Assert.IsTrue(B.Storage.S.BaseOffset == 1);

                ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { -1, -4, -7, -10, -2, -5, -8, -11, -3, -6, -9, });
                //ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { 1, 4, 7, 10, 2, 5, 8, 11, 3, 6, 9, });
            }
        }
        [TestMethod]
        public void SubarrayDimSpec2DML_SteppedRangeOnRowVectorInsideSecondDim1DimEndTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,,] {
                    { { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 } },
                    { { -1,-2,-3}, { -4,-5,-6 },
                    {-7,-8,-9 }, {-10,-11,-12 } },
                    };
                A.a = A[full].T;  // A is now:
               // <Double> [1, 24] 1 - 1...
               //[0]:           1 - 1          4 - 4          7 - 7         10 - 10          2 - 2          5 - 5          8 - 8         11 - 11          3 - 3          6 - 6          9 - 9         12 - 12

                var B = A[end, r(1, 2, 21)];  // indexer 

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 1 && B.Storage.S[1] == 11);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.Other);
                // Copy was made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // had to reshape -> but without copy, A was column maj. 

                Assert.IsTrue(B.Storage.S.BaseOffset == 1);

                ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { -1, -4, -7, -10, -2, -5, -8, -11, -3, -6, -9, });
                //ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { 1, 4, 7, 10, 2, 5, 8, 11, 3, 6, 9, });
            }
        }
        [TestMethod]
        public void SubarrayDimSpec2DML_SteppedRangeOnRowVectorInsideSecondDim1DimMinus1Test() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,,] {
                    { { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 } },
                    { { -1,-2,-3}, { -4,-5,-6 },
                    {-7,-8,-9 }, {-10,-11,-12 } },
                    };
                A.a = A[full].T;  // A is now:
               // <Double> [1, 24] 1 - 1...
               //[0]:           1 - 1          4 - 4          7 - 7         10 - 10          2 - 2          5 - 5          8 - 8         11 - 11          3 - 3          6 - 6          9 - 9         12 - 12

                var B = A[-1, r(1, 2, 21)];  // indexer 

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 1 && B.Storage.S[1] == 11);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.Other);
                // Copy was made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // had to reshape -> but without copy, A was column maj. 

                Assert.IsTrue(B.Storage.S.BaseOffset == 1);

                ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { -1, -4, -7, -10, -2, -5, -8, -11, -3, -6, -9, });
                //ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { 1, 4, 7, 10, 2, 5, 8, 11, 3, 6, 9, });
            }
        }
        [TestMethod]
        public void SubarrayDimSpec2DML_SteppedRangeOnRowVectorInsideSecondDim1DimEllipsisTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,,] {
                    { { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 } },
                    { { -1,-2,-3}, { -4,-5,-6 },
                    {-7,-8,-9 }, {-10,-11,-12 } },
                    };
                A.a = A[full].T;  // A is now:
               // <Double> [1, 24] 1 - 1...
               //[0]:           1 - 1          4 - 4          7 - 7         10 - 10          2 - 2          5 - 5          8 - 8         11 - 11          3 - 3          6 - 6          9 - 9         12 - 12

                var B = A[ellipsis, r(0, 2, 21)];  // indexer 

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 1 && B.Storage.S[1] == 11);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.Other);

                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // had to reshape -> but without copy, A was column maj. 

                Assert.IsTrue(B.Storage.S.BaseOffset == 0);

                ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { 1, 4, 7, 10, 2, 5, 8, 11, 3, 6, 9, });
            }
        }
        [TestMethod]
        public void SubarrayDimSpec2DML_SteppedRangeOnRowVectorInsideSecondDimBase10Test() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,,] {
                    { { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 } },
                    { { -1,-2,-3}, { -4,-5,-6 },
                    {-7,-8,-9 }, {-10,-11,-12 } },
                    };
                A.a = A[full].T;  // A is now:
               // <Double> [1, 24] 1 - 1...
               //[0]:           1 - 1          4 - 4          7 - 7         10 - 10          2 - 2          5 - 5          8 - 8         11 - 11          3 - 3          6 - 6          9 - 9         12 - 12

                var B = A[full, r(10, 2, 21)];  // indexer 

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 1 && B.Storage.S[1] == 6);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.Other);
                // Copy was made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // had to reshape -> but without copy, A was column maj. 

                Assert.IsTrue(B.Storage.S.BaseOffset == 10);

                ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { 5, 8, 11, 3, 6, 9, });
            }
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SubarrayDimSpec2DML_SteppedRangeOnRowVectorEndOORFailTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,,] {
                    { { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 } },
                    { { -1,-2,-3}, { -4,-5,-6 },
                    {-7,-8,-9 }, {-10,-11,-12 } },
                    };
                A.a = A[full].T;  // A is now:
                                  // <Double> [1, 24] 1 - 1...
                                  //[0]:           1 - 1          4 - 4          7 - 7         10 - 10          2 - 2          5 - 5          8 - 8         11 - 11          3 - 3          6 - 6          9 - 9         12 - 12

                var B = A[0,r(10, 2, 24)];  // indexer 
            }
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SubarrayDimSpec2DML_SteppedRangeOnRowVectorStartOORFailTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,,] {
                    { { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 } },
                    { { -1,-2,-3}, { -4,-5,-6 },
                    {-7,-8,-9 }, {-10,-11,-12 } },
                    };
                A.a = A[full].T;  // A is now:
                                  // <Double> [1, 24] 1 - 1...
                                  //[0]:           1 - 1          4 - 4          7 - 7         10 - 10          2 - 2          5 - 5          8 - 8         11 - 11          3 - 3          6 - 6          9 - 9         12 - 12

                var B = A[1, r(2, 2, -1)];  // indexer 
            }
        }

        [TestMethod]
        public void SubarrayDimSpec2DML_SteppedRangeOnRowVectorNegStepsNotFailTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,,] {
                    { { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 } },
                    { { -1,-2,-3}, { -4,-5,-6 },
                    {-7,-8,-9 }, {-10,-11,-12 } },
                    };
                A.a = A[full].T;  // A is now:
                                  // <Double> [1, 24] 1 - 1...
                                  //[0]:           1 - 1          4 - 4          7 - 7         10 - 10          2 - 2          5 - 5          8 - 8         11 - 11          3 - 3          6 - 6          9 - 9         12 - 12

                Array<double> B = A[0, r(23, 2, 22)];  // indexer 
                // returns empty 
                Assert.IsTrue(B.S[0] == 1); 
                Assert.IsTrue(B.S[1] == 0);
                Assert.IsTrue(B.S.NumberOfDimensions == 2); 
            }
        }
        [TestMethod]
        public void SubarrayDimSpec2DML_SteppedRangeOnRowVectorStartEqualEndScalarTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,,] {
                    { { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 } },
                    { { -1,-2,-3}, { -4,-5,-6 },
                    {-7,-8,-9 }, {-10,-11,-12 } },
                    };
                A.a = A[full].T;  // A is now:
                                  // <Double> [1, 24] 1 - 1...
                                  //[0]:           1 - 1          4 - 4          7 - 7         10 - 10          2 - 2          5 - 5          8 - 8         11 - 11          3 - 3          6 - 6          9 - 9         12 - 12

                var B = A[-1, r(23, 2, 23)];  // indexer 
                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 1 && B.Storage.S[1] == 1);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.ColumnMajor);
                // Copy was made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // had to reshape -> but without copy, A was column maj. 

                Assert.IsTrue(B.Storage.S.BaseOffset == 23);

                ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { -12 });

            }
        }
        [TestMethod]
        public void SubarrayDimSpec2DML_SteppedRangeOnRowVectorNegStartTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,,] {
                    { { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 } },
                    { { -1,-2,-3}, { -4,-5,-6 },
                    {-7,-8,-9 }, {-10,-11,-12 } },
                    };
                A.a = A[full].T;  // A is now:
                                  // <Double> [1, 24] 1 - 1...
                                  //[0]:           1 - 1          4 - 4          7 - 7         10 - 10          2 - 2          5 - 5          8 - 8         11 - 11          3 - 3          6 - 6          9 - 9         12 - 12

                var B = A[-1, r(-1, 2, 23)];  // indexer 
                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 1 && B.Storage.S[1] == 1);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.ColumnMajor);
                // Copy was made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // had to reshape -> but without copy, A was column maj. 

                Assert.IsTrue(B.Storage.S.BaseOffset == 23);

                ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { -12 });

            }
        }
        [TestMethod]
        public void SubarrayDimSpec2DML_SteppedRangeOnRowVectorNegEndTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,,] {
                    { { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 } },
                    { { -1,-2,-3}, { -4,-5,-6 },
                    {-7,-8,-9 }, {-10,-11,-12 } },
                    };
                A.a = A[r(1,-1)].T;  // A is now:
                                  // <Double> [1, 23] - 1...
                                  //[0]:            - 1          4 - 4          7 - 7         10 - 10          2 - 2          5 - 5          8 - 8         11 - 11          3 - 3          6 - 6          9 - 9         12 - 12

                var B = A[ellipsis, r(22, 2, -1)];  // indexer 
                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 1 && B.Storage.S[1] == 1);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.ColumnMajor);
                // Copy was made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // had to reshape -> but without copy, A was column maj. 

                Assert.IsTrue(B.Storage.S.BaseOffset == 23);

                ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { -12 });

            }
        }
        [TestMethod]
        public void SubarrayDimSpec2DML_UnsteppedRangeOnRowVectorNegStartTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,,] {
                    { { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 } },
                    { { -1,-2,-3}, { -4,-5,-6 },
                    {-7,-8,-9 }, {-10,-11,-12 } },
                    };
                A.a = A[r(2, 1+end-1)].T;  // A is now:
                                  // <Double> [1, 24] 1 - 1...
                                  //[0]:           1 - 1          4 - 4          7 - 7         10 - 10          2 - 2          5 - 5          8 - 8         11 - 11          3 - 3          6 - 6          9 - 9         12 - 12

                var B = A[end, r(-2,-2)];  // indexer 
                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 1 && B.Storage.S[1] == 1);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.ColumnMajor);
                // Copy was made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // had to reshape -> but without copy, A was column maj. 

                Assert.IsTrue(B.Storage.S.BaseOffset == 22);

                ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { 12 });

            }
        }
        [TestMethod]
        public void SubarrayDimSpec2DML_UnsteppedRangeOnRowVectorNegEndTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,,] {
                    { { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 } },
                    { { -1,-2,-3}, { -4,-5,-6 },
                    {-7,-8,-9 }, {-10,-11,-12 } },
                    };
                A.a = A[full].T;  // A is now:
                                  // <Double> [1, 24] 1 - 1...
                                  //[0]:           1 - 1          4 - 4          7 - 7         10 - 10          2 - 2          5 - 5          8 - 8         11 - 11          3 - 3          6 - 6          9 - 9         12 - 12

                var B = A[-1, r(22, -2)];  // indexer 
                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 1 && B.Storage.S[1] == 1);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.ColumnMajor);
                // Copy was made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // had to reshape -> but without copy, A was column maj. 

                Assert.IsTrue(B.Storage.S.BaseOffset == 22);

                ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { 12 });

            }
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void SubarrayDimSpec2DML_SteppedRangeNegStepFailTest() {

            Array<uint> A = new[] { 1, 2 };

            var B = A[r(0, -1, 1), 0];

        }

        [TestMethod]
        public void SubarrayDimSpec2DM_On0DimScalarTest() {

            Array<float> A; 
            using (Settings.Ensure(()=> Settings.ArrayStyle, ArrayStyles.numpy)) {
                A = -1; 
            }
            Assert.IsTrue(A.S.NumberOfDimensions == 0 && A.IsScalar && A.S.NumberOfElements == 1);
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {
                Array<float> B = A[0];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[0,r(0, 0)];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[0,r(0, 1, 0)];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[0,r(0, end)];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[0,r(end, 0)];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[0, r(end, end)];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[0, r(end, 1, end)];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[0, -1];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[0, full];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[0, ellipsis];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[0, end];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);

                B.a = A[r(0, 0),0];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[r(0, 1, 0), 0];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[r(0, end), 0];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[r(end, 0), 0];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[r(end, end), 0];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[r(end, 1, end), 0];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[-1, 0];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[full, 0];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[ellipsis, 0];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[end, 0];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);

                B.a = A[0,r(0, 0),0];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[0,r(0, 1, 0), 0];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[0,r(0, end), 0];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[0,r(end, 0), 0];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[0, r(end, end), 0];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[0, r(end, 1, end), 0];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[0, -1, 0];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[0, full, 0];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[0, ellipsis, 0];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[0, end, 0];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
            }

        }

        [TestMethod]
        public void GetRange_ML_emptyDim_full() {

            Array<double> A = zeros<double>(3, dim1: 0);
            Array<double> B = A[0, full];   // should work
            Assert.IsTrue(B.S[0] == 1);
            Assert.IsTrue(B.S[1] == 0);

            Assert.IsTrue(B.S.NumberOfDimensions == 2);
            Assert.IsTrue(B.S.NumberOfElements == 0);

            Assert.IsTrue(zeros<double>(0, dim1: 0)[full, full].shape.Equals(vector<long>(0, 0)));  
            Assert.IsTrue(zeros<double>(0, dim1: 0, dim2: 3)[full, full, 2].shape.Equals(vector<long>(0, 0, 1)));  
            Assert.IsTrue(zeros<double>(3, dim1: 2, dim2: 0)[1, 1, full].shape.Equals(vector<long>(1, 1, 0)));  
            Assert.IsTrue(zeros<double>(3,1,2,dim3:0)[1, 0, full, full].shape.Equals(vector<long>(1, 1, 2, 0)));
            Assert.IsTrue(zeros<double>(3,1,2,2, dim4: 0)[1, 0, full, 1, full].shape.Equals(vector<long>(1, 1, 2, 1, 0)));
            Assert.IsTrue(zeros<double>(3,1,2,2,0,2)[1, 0, full, 1, full, full].shape.Equals(vector<long>(1, 1, 2, 1, 0, 2)));
            Assert.IsTrue(zeros<double>(3,1,2,2,0,2, dim6: 0)[1, 0, full, 1, full, full, full].shape
                                .Equals(vector<long>(1, 1, 2, 1, 0, 2, 0)));

        }

    }
}
