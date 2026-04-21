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
    public class SubarrayDimSpec1D_ML_Tests {

        [TestMethod]
        public void SubarrayDimSpec1D_MLFullTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,] {
                    { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 }
                };

                Array<double> B = A[full];

                Assert.IsTrue(!Equals(B,null) && B.IsColumnVector);
                Assert.IsTrue(B.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.S[0] == 12 && B.S[1] == 1);
                Assert.IsTrue(B.S.StorageOrder == StorageOrders.ColumnMajor);
                // Copy was made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(!object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles));

                ArrayAssert.ValuesEqual(A.GetArrayForRead(), new double[] { 1, 4, 7, 10, 2, 5, 8, 11, 3, 6, 9, 12 });
            }
        }
        [TestMethod]
        public void SubarrayDimSpec1D_MLFullFromColMajTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,] {
                    { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 }
                };
                A.a = A.Reshape(4, 3, StorageOrders.ColumnMajor);

                Array<double> B = A[full];

                Assert.IsTrue(!Equals(B,null) && B.IsColumnVector);
                Assert.IsTrue(B.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.S[0] == 12 && B.S[1] == 1);
                Assert.IsTrue(B.S.StorageOrder == StorageOrders.ColumnMajor);
                // no copy was made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles));

                ArrayAssert.ValuesEqual(A.GetArrayForRead(), new double[] { 1, 4, 7, 10, 2, 5, 8, 11, 3, 6, 9, 12 });
            }
        }
        [TestMethod]
        public void SubarrayDimSpec1D_MLEllipsisTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,] {
                    { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 }
                };

                var B = A[ellipsis];

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 4 && B.Storage.S[1] == 3);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.RowMajor);
                // No copy was made 
                // Update v7: We don't reuse storages for aliases anymore. Doing a shallow copy allows to 
                // detach B from A if need arise. 
                Assert.IsFalse(object.ReferenceEquals(B.Storage, A.Storage));

            }
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SubarrayDimSpec1DML_NewaxisTestFails() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {
                Array<double> A = new double[,] {
                    { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 }
                };

                var B = A[newaxis];
 
            }

        }
        [TestMethod]
        public void SubarrayDimSpec1D_MLSingleIntTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,] {
                    { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 }
                };

                var B = A[1];

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 1 && B.Storage.S[1] == 1);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.ColumnMajor);
                // No copy was made 
                // Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));  <- implementation detail. A copy may is made for ACC.
                Assert.IsTrue(B.Storage.GetValue(0) == 4);
                //Assert.IsTrue(B.Storage.S.BaseOffset == A.S.GetStride(0));      -<- may not hold for ACC code

                // identical!    // Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles));

                // identical!    // ArrayAssert.ValuesEqual(A.GetArrayForRead(), new double[] { 1, 4, 7, 10, 2, 5, 8, 11, 3, 6, 9, 12 });
            }
        }
        [TestMethod]
        public void SubarrayDimSpec1D_MLSingleLongTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,] {
                    { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 }
                };

                var B = A[(long)1];

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 1 && B.Storage.S[1] == 1);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.ColumnMajor);
                // No copy was made 
                // Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));  <- implementation detail. A copy may is made for ACC.
                Assert.IsTrue(B.Storage.GetValue(0) == 4);
                Assert.IsTrue(B.Storage.S.BaseOffset == A.S.GetStride(0));



                // identical!    // Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles));

                // identical!    // ArrayAssert.ValuesEqual(A.GetArrayForRead(), new double[] { 1, 4, 7, 10, 2, 5, 8, 11, 3, 6, 9, 12 });
            }
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SubarrayDimSpec1D_MLSingleLongOORTestFail() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,] {
                    { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 }
                };

                var B = A[(long)12];
 
            }
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SubarrayDimSpec1D_MLSingleUIntOORTestFail() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,] {
                    { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 }
                };

                var B = A[12];
 
            }
        }

        [TestMethod]
        public void SubarrayDimSpec2D_MLSingleLongNegTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,] {
                    { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 }
                };

                var B = A[-1];

                Assert.IsTrue(B.Storage.GetValue(0) == 12);

            }
        }
        [TestMethod]
        public void SubarrayDimSpec1D_MLRange001Test() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,] {
                    { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 }
                };

                var B = A[r(0,1,11)];

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 12 && B.Storage.S[1] == 1);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.ColumnMajor);
                // A was changed
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles));

                Assert.IsTrue(B.Storage.S.BaseOffset == 0);

                ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { 1, 4, 7, 10, 2, 5, 8, 11, 3, 6, 9, 12 });

            }
        }
        [TestMethod]
        public void SubarrayDimSpec1D_MLRange223Test() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,] {
                    { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 }
                };

                var B = A[r(2,2,3)];

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 1 && B.Storage.S[1] == 1);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.ColumnMajor);
                // Copy was made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles));

                Assert.IsTrue(B.Storage.S.BaseOffset == 6);

                ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { 7 });

            }
        }
        [TestMethod]
        public void SubarrayDimSpec1D_MLRangeReuseTest() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = new double[,] {
                    { 1,2,3}, { 4,5,6 },
                    {7,8,9 }, {10,11,12 }
                };
                var T = A.T;
                var B = T[r(2,2,11)];  // indexer acts on the RetT returned from .T

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 5 && B.Storage.S[1] == 1);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.Other);
                // Copy was made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles));

                Assert.IsTrue(B.Storage.S.BaseOffset == 2);

                ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { 3, 5, 7, 9, 11 });

            }
        }
        [TestMethod]
        public void SubarrayDimSpec1DML_SteppedRangeOn3DInsideFirstDimTest() {

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

                var B = A[r(0, 2, 2)];  // indexer 

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 2 && B.Storage.S[1] == 1);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.Other);
                // Copy was made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles));

                Assert.IsTrue(B.Storage.S.BaseOffset == 0);

                ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { 1, 7 });
            }
        }
        [TestMethod]
        public void SubarrayDimSpec1DML_SteppedRangeOn3DInsideFirstDimRetTest() {
            // ILN(enabled=false)
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

                var B = A.T[r(0, 2, 2)];  // T uses same buffer as A, Indexer uses same buffer as T

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions);
                Assert.IsTrue(B.Storage.S[0] == 2 && B.Storage.S[1] == 1);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.Other);
                // Buffer copy was not made -> implementation detail!!
                //Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles));

                Assert.IsTrue(B.Storage.S.BaseOffset == 0);

                ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { 1, 7 });
            }
            // ILN(enabled=true)
        }

        [TestMethod]
        public void SubarrayDimSpec_MLleadingSingletonDims() {
            Array<double> A = counter(1.0, 1.0, 1, 1, 1, 1, 5);
            Array<uint> I = new uint[] { 1, 3, 2, 0, 2 };
            
            DoSubarrayDimSpec_MLleadingSingeltinDimsTest<double,uint>(counter(1.0, 1.0, 1, 1, 1, 1, 5), I, 2, (a, b) => a == b);
            DoSubarrayDimSpec_MLleadingSingeltinDimsTest<float,uint>(tosingle(counter(1.0, 1.0, 1, 1, 1, 1, 5)), I, 2, (a, b) => a == b);
            DoSubarrayDimSpec_MLleadingSingeltinDimsTest<uint,uint>(touint32(counter(1.0, 1.0, 1, 1, 1, 1, 5)), I, 2, (a, b) => a == b);
            DoSubarrayDimSpec_MLleadingSingeltinDimsTest<int,uint>(toint32(counter(1.0, 1.0, 1, 1, 1, 1, 5)), I, 2, (a, b) => a == b);
            DoSubarrayDimSpec_MLleadingSingeltinDimsTest<long,uint>(toint64(counter(1.0, 1.0, 1, 1, 1, 1, 5)), I, 2, (a, b) => a == b);
            DoSubarrayDimSpec_MLleadingSingeltinDimsTest<ulong,uint>(touint64(counter(1.0, 1.0, 1, 1, 1, 1, 5)), I, 2, (a, b) => a == b);

            Array<string> S = new string[] { "1", "2", "3", "4", "5" };
            S.a = S.Reshape(1, 1, 1, 1, 5); 
            DoSubarrayDimSpec_MLleadingSingeltinDimsTest<string,uint>(S, I, "2", (a, b) => a == b);
        }
        private void DoSubarrayDimSpec_MLleadingSingeltinDimsTest<T1, T2>(InArray<T1> A, InArray<T2> I, T1 val, Func<T1, T1, bool> eq) where T1 : IEquatable<T1> {
            using (Scope.Enter(A, I)) {
                // 3D with 2 leading singleton dims

                Assert.IsTrue(eq(A[1].GetValue(0),val));
                Assert.IsTrue(eq(A[r(1, 1)].GetValue(0),val));
                Assert.IsTrue(eq(A[I].GetValue(0),val));
                Assert.IsTrue(eq(A[I[0]].GetValue(0),val));
                Assert.IsTrue(eq(A[I[full]].GetValue(0),val));
                Assert.IsTrue(eq(A["1:2,2"].GetValue(0),val));

                Assert.IsTrue(eq(A[0, 1].GetValue(0),val));
                Assert.IsTrue(eq(A[0, r(1, 1)].GetValue(0),val));
                Assert.IsTrue(eq(A[0, I].GetValue(0),val));
                Assert.IsTrue(eq(A[I[3], I].GetValue(0),val));
                Assert.IsTrue(eq(A[0, I[0]].GetValue(0),val));
                Assert.IsTrue(eq(A[0, I[full]].GetValue(0),val));
                Assert.IsTrue(eq(A[0, "1:2,2"].GetValue(0),val));

                Assert.IsTrue(eq(A[0, 0, 1].GetValue(0),val));
                Assert.IsTrue(eq(A[0, 0, r(1, 1)].GetValue(0),val));
                Assert.IsTrue(eq(A[0, 0, I].GetValue(0),val));
                Assert.IsTrue(eq(A[0, 0, I[0]].GetValue(0),val));
                Assert.IsTrue(eq(A[0, 0, I[full]].GetValue(0),val));
                Assert.IsTrue(eq(A[0, 0, "1:2,2"].GetValue(0),val));

                Assert.IsTrue(eq(A[0, 0, 0, 1].GetValue(0),val));
                Assert.IsTrue(eq(A[0, 0, 0, r(1, 1)].GetValue(0),val));
                Assert.IsTrue(eq(A[0, 0, 0, I].GetValue(0),val));
                Assert.IsTrue(eq(A[0, 0, 0, I[0]].GetValue(0),val));
                Assert.IsTrue(eq(A[0, 0, 0, I[full]].GetValue(0),val));
                Assert.IsTrue(eq(A[0, 0, 0, "1:2,2"].GetValue(0),val));

                Assert.IsTrue(eq(A[0, 0, 0, 1].GetValue(0),val));
                Assert.IsTrue(eq(A[0, 0, 0, r(1, 1)].GetValue(0),val));
                Assert.IsTrue(eq(A[0, 0, 0, I].GetValue(0),val));
                Assert.IsTrue(eq(A[0, 0, 0, I[0]].GetValue(0),val));
                Assert.IsTrue(eq(A[0, 0, 0, I[full]].GetValue(0),val));
                Assert.IsTrue(eq(A[0, 0, 0, "1:2,2"].GetValue(0),val));
            }

        }

        [TestMethod]
        public void SubarrayDimSpec1DML_SteppedRangeOn3DInsideSecondDimTest() {
            Settings.MaxNumberThreads = 4; 
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

                var B = A[r(1, 2, 7)];  // indexer 

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 4 && B.Storage.S[1] == 1);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.Other);
                // Copy was made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // no copy. It was in Column Major already.

                Assert.IsTrue(B.Storage.S.BaseOffset == 1);

                ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { 4, 10, 5, 11 });
            }
        }
        [TestMethod]
        public void SubarrayDimSpec1DML_SteppedRangeOn3DInsideSecondDimRetTest() {
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

                var B = A.T[r(1, 2, 7)];  // indexer uses new storage (due to reshape)!!

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 4 && B.Storage.S[1] == 1);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.Other);
                // Copy was made 
                //Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage)); // 
                
                // note: A is dead here already. It died after .T and is reused for B, PROBABLY! But this 
                // is an implementation detail. We should not rely on it! 

                //Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // had to reshape -> copy

                Assert.IsTrue(B.Storage.S.BaseOffset == 1);

                ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { 4, 10, 5, 11 });
            }
        }
        [TestMethod]
        public void SubarrayDimSpec1DML_SteppedRangeOn3DInsideSecondDimExactEndofRangeTest() {
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

                var B = A[r(1, 2, 6)];  // indexer 

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 3 && B.Storage.S[1] == 1);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.Other);
                // Copy was made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // had to reshape -> copy

                Assert.IsTrue(B.Storage.S.BaseOffset == 1);

                ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { 4, 10, 5 });
            }
        }
        [TestMethod]
        public void SubarrayDimSpec1DML_SteppedRangeOn3DInsideThirdDimTest() {
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

                var B = A[r(0, 2, 21)];  // indexer 

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 11 && B.Storage.S[1] == 1);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.Other);
                // A was changed  
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // had to reshape -> copy

                Assert.IsTrue(B.Storage.S.BaseOffset == 0);

                ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { 1, 7,2,8,3,9,-1,-7,-2,-8,-3 });
            }
        }
        [TestMethod]
        public void SubarrayDimSpec1DML_SteppedRangeOnRowVectorInsideFirstDimTest() {
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

                var B = A[r(1, 2, 21)];  // indexer 

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 11 && B.Storage.S[1] == 1);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.Other);
                // Copy was made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // had to reshape -> copy

                Assert.IsTrue(B.Storage.S.BaseOffset == 1);

                ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { 4, 10, 5, 11, 6, 12, -4, -10, -5, -11, -6 });
            }
        }
        [TestMethod]
        public void SubarrayDimSpec1DML_SteppedRangeOnRowVectorInsideFirstDim_NoNewStorageInGetRangeTest() {
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

                // make sure GetRange() does not need to EnsureStorageOrder(), hence does not create a new storage: 
                A.Storage.EnsureStorageOrder(StorageOrders.ColumnMajor);
                Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor); 

                var B = A[r(1, 2, 21)];  // indexer 

                Assert.IsTrue(!Equals(B, null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 11 && B.Storage.S[1] == 1);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.Other);
                // Copy was made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // had to reshape -> copy

                Assert.IsTrue(B.Storage.S.BaseOffset == 1);

                ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { 4, 10, 5, 11, 6, 12, -4, -10, -5, -11, -6 });
            }
        }
        [TestMethod]
        public void SubarrayDimSpec1DML_SteppedRangeOnRowVectorInsideSecondDimTest() {
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

                var B = A[r(1, 2, 21)];  // indexer 

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 11 && B.Storage.S[1] == 1);

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
        public void SubarrayDimSpec1DML_SteppedRangeOnRowVectorInsideSecondDimBase0Test() {
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

                var B = A[r(0, 2, 21)];  // indexer 

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 11 && B.Storage.S[1] == 1);

                Assert.IsTrue(B.Storage.S.StorageOrder == StorageOrders.Other);
                // Copy was made 
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // had to reshape -> but without copy, A was column maj. 

                Assert.IsTrue(B.Storage.S.BaseOffset == 0);

                ArrayAssert.ValuesEqual(B.GetArrayForRead(), new double[] { 1, 4, 7, 10, 2, 5, 8, 11, 3, 6, 9, });
            }
        }
        [TestMethod]
        public void SubarrayDimSpec1DML_SteppedRangeOnRowVectorInsideSecondDimBase10Test() {
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

                var B = A[r(10, 2, 21)];  // indexer 

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.Storage.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.Storage.S[0] == 6 && B.Storage.S[1] == 1);

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
        public void SubarrayDimSpec1DML_SteppedRangeOnRowVectorEndOORFailTest() {
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

                var B = A[r(10, 2, 24)];  // indexer 
 
            }
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SubarrayDimSpec1DML_SteppedRangeOnRowVectorStartOORFailTest() {
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

                var B = A[r(24, 2, 10)];  // indexer 
            }
        }
        [TestMethod]
//        [ExpectedException(typeof(NotSupportedException))]
        public void SubarrayDimSpec1DML_SteppedRangeOnRowVectorNegStepsFailTest() {
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

                Array<double> B = A[r(23, 2, 22)];  // indexer 
                // creates an empty array
                Assert.IsTrue(B.S[0] == 0);
                Assert.IsTrue(B.S[1] == 1); 

            }
        }
        [TestMethod]
        public void SubarrayDimSpec1DML_SteppedRangeOnRowVectorStartEqualEndScalarTest() {
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

                var B = A[r(23, 2, 23)];  // indexer 
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
        public void SubarrayDimSpec1DML_SteppedRangeOnRowVectorNegStartTest() {
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

                var B = A[r(-1, 2, 23)];  // indexer 
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
        public void SubarrayDimSpec1DML_SteppedRangeOnRowVectorNegEndTest() {
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

                var B = A[r(23, 2, -1)];  // indexer 
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
        public void SubarrayDimSpec1DML_UnsteppedRangeOnRowVectorNegStartTest() {
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

                var B = A[r(-2,22)];  // indexer 
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
        public void SubarrayDimSpec1DML_UnsteppedRangeOnRowVectorNegEndTest() {
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

                var B = A[r(22, -2)];  // indexer 
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
        public void SubarrayDimSpec1DML_SteppedRangeNegStepFailTest() {

            Array<uint> A = new[] { 1, 2 };

            var B = A[r(0, -1, 1)];

        }
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void SubarrayDimSpec1DML_SteppedRangeSize0FailTest() {

            Array<uint> A = new[] { 1, 2 };

            var B = A[r(0, 0, 1)];

        }

        [TestMethod]
        public void SubarrayDimSpec1DM_On0DimScalarTest() {

            Array<float> A; 
            using (Settings.Ensure(()=> Settings.ArrayStyle, ArrayStyles.numpy)) {
                A = -1; 
            }
            Assert.IsTrue(A.S.NumberOfDimensions == 0 && A.IsScalar && A.S.NumberOfElements == 1);
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {
                Array<float> B = A[0];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[r(0, 0)];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[r(0, 1, 0)];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[r(0, end)];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[r(end, 0)];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[r(end, end)];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[r(end, 1, end)];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[-1];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[full];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[ellipsis];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);
                B.a = A[end];
                Assert.IsTrue(B.GetValue(0) == -1 && B.S.NumberOfElements == 1);

            }

        }
    }
}
