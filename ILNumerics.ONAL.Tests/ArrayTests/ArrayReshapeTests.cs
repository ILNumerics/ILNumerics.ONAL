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

namespace ILNumerics.Core.UnitTests {

    [TestClass]
    public class ArrayReshapeTests {

        [TestMethod]
        public void ReshapeSimple1DContinousRowMajorTest() {
            Array<int> A = new int[,] {
                { 0, 1, 2}, {3, 4, 5 }
            };
            Array<int> B;
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {
                B = A.Reshape(6); // requests column major order from settings default

                Assert.IsTrue(B.S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions);
                Assert.IsTrue(B.S.NumberOfElements == A.S.NumberOfElements);
                Assert.IsFalse(object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsFalse(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // copied
                Assert.IsTrue(B.S[0] == 6);
                Assert.IsTrue(B.S[1] == 1);
                Assert.IsTrue(B.GetValue(0, 0) == 0);
                Assert.IsTrue(B.GetValue(1, 0) == 3);
                Assert.IsTrue(B.GetValue(2, 0) == 1);
                Assert.IsTrue(B.GetValue(3, 0) == 4);
                Assert.IsTrue(B.GetValue(4, 0) == 2);
                Assert.IsTrue(B.GetValue(5, 0) == 5);
                Assert.IsTrue(B.S.IsContinuous);
                Assert.IsTrue(B.S.StorageOrder == StorageOrders.ColumnMajor);
            }

            B.a = A.Reshape(6, StorageOrders.RowMajor);

            Assert.IsTrue(B.S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions);
            Assert.IsTrue(B.S.NumberOfElements == A.S.NumberOfElements);
            Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
            Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles));
            Assert.IsTrue(B.S[0] == 1);
            Assert.IsTrue(B.S[1] == 6);
            Assert.IsTrue(B.GetValue(0, 0) == 0);
            Assert.IsTrue(B.GetValue(0, 1) == 1);
            Assert.IsTrue(B.GetValue(0, 2) == 2);
            Assert.IsTrue(B.GetValue(0, 3) == 3);
            Assert.IsTrue(B.GetValue(0, 4) == 4);
            Assert.IsTrue(B.GetValue(0, 5) == 5);
            Assert.IsTrue(B.S.IsContinuous);
            Assert.IsTrue(B.S.StorageOrder == StorageOrders.RowMajor);

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                B = A.Reshape(6); // requests column major order from settings default

                Assert.IsTrue(B.S.NumberOfDimensions == 1);
                Assert.IsTrue(B.S.NumberOfElements == A.S.NumberOfElements);
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles));
                Assert.IsTrue(B.S[0] == 6);
                Assert.IsTrue(B.S[1] == 1);
                Assert.IsTrue(B.GetValue(0) == 0);
                Assert.IsTrue(B.GetValue(1) == 1);
                Assert.IsTrue(B.GetValue(2) == 2);
                Assert.IsTrue(B.GetValue(3) == 3);
                Assert.IsTrue(B.GetValue(4) == 4);
                Assert.IsTrue(B.GetValue(5) == 5);
                Assert.IsTrue(B.S.IsContinuous);
                Assert.IsTrue(B.S.StorageOrder == Settings.DefaultStorageOrder);
                Assert.IsTrue(B.S.StorageOrder == StorageOrders.RowMajor);
            }
        }

        [TestMethod]
        public void ReshapeSimple1DContinousColumnMajorTest() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<int> A = new int[,] {
                    { 0, 1, 2}, {3, 4, 5 }
                };
                Assert.IsTrue(A.S.NumberOfDimensions == 2);
                Assert.IsTrue(A.S.NumberOfElements == 6);

                Array<int> B = A.T.Reshape(6);

                Assert.IsTrue(B.S.NumberOfDimensions == 2);
                Assert.IsTrue(B.S.NumberOfElements == A.S.NumberOfElements);
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles));
                Assert.IsTrue(B.S[1] == 1);
                Assert.IsTrue(B.S[0] == 6);
                Assert.IsTrue(B.GetValue(0, 0) == 0);
                Assert.IsTrue(B.GetValue(1, 0) == 1);
                Assert.IsTrue(B.GetValue(2, 0) == 2);
                Assert.IsTrue(B.GetValue(3, 0) == 3);
                Assert.IsTrue(B.GetValue(4, 0) == 4);
                Assert.IsTrue(B.GetValue(5, 0) == 5);
                Assert.IsTrue(B.S.IsContinuous);

                B.a = A.T.Reshape(6, StorageOrders.ColumnMajor);

                Assert.IsTrue(B.S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions);
                Assert.IsTrue(B.S.NumberOfElements == A.S.NumberOfElements);
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles));
                Assert.IsTrue(B.S[1] == 1);
                Assert.IsTrue(B.S[0] == 6);
                Assert.IsTrue(B.GetValue(0, 0) == 0);
                Assert.IsTrue(B.GetValue(1, 0) == 1);
                Assert.IsTrue(B.GetValue(2, 0) == 2);
                Assert.IsTrue(B.GetValue(3, 0) == 3);
                Assert.IsTrue(B.GetValue(4, 0) == 4);
                Assert.IsTrue(B.GetValue(5, 0) == 5);
                Assert.IsTrue(B.S.IsContinuous);
            }
        }

        [TestMethod]
        public void ReshapeSimple1DCopyColsTest() {

            Array<int> A = new int[,] {
                { 0, 1, 2}, {3, 4, 5 }
            };

            Array<int> B = A.Reshape(6, StorageOrders.ColumnMajor);


            Assert.IsTrue(B.S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions);
            Assert.IsTrue(B.S.NumberOfElements == A.S.NumberOfElements);
            Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
            Assert.IsFalse(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles));
            Assert.IsTrue(B.S[1] == 1);
            Assert.IsTrue(B.S[0] == 6);
            Assert.IsTrue(B.GetValue(0, 0) == 0);
            Assert.IsTrue(B.GetValue(1, 0) == 3);
            Assert.IsTrue(B.GetValue(2, 0) == 1);
            Assert.IsTrue(B.GetValue(3, 0) == 4);
            Assert.IsTrue(B.GetValue(4, 0) == 2);
            Assert.IsTrue(B.GetValue(5, 0) == 5);
            Assert.IsTrue(B.S.IsContinuous);

        }
        [TestMethod]
        public void ReshapeSimple1DCopyRowsTest() {

            Array<int> A = new int[,] {
                { 0, 1, 2},
                { 3, 4, 5 }
            };

            Array<int> B = A.T.Reshape(6, StorageOrders.RowMajor);


            Assert.IsTrue(B.S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions);
            Assert.IsTrue(B.S.NumberOfElements == A.S.NumberOfElements);
            Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
            Assert.IsFalse(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles));
            Assert.IsTrue(B.S[1] == 6);
            Assert.IsTrue(B.S[0] == 1);
            Assert.IsTrue(B.GetValue(0, 0) == 0);
            Assert.IsTrue(B.GetValue(0, 1) == 3);
            Assert.IsTrue(B.GetValue(0, 2) == 1);
            Assert.IsTrue(B.GetValue(0, 3) == 4);
            Assert.IsTrue(B.GetValue(0, 4) == 2);
            Assert.IsTrue(B.GetValue(0, 5) == 5);
            Assert.IsTrue(B.S.IsContinuous);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReshapeSimple1DInvalidD0Test() {

            Array<int> A = new int[,] {
                { 0, 1, 2},
                { 3, 4, 5 }
            };

            Array<int> B = A.Reshape(5);
 
        }

        [TestMethod]
        public void Reshape2DimContinousColTest() {
            Array<uint> A = new uint[,] { { 0, 1, 2, 3, 4, 5, 6, 7 } };

            Array<uint> B = A.Reshape(2, 4, StorageOrders.RowMajor);

            Assert.IsTrue(B.S.NumberOfDimensions == Math.Max(2, Settings.MinNumberOfArrayDimensions));
            Assert.IsTrue(B.S.NumberOfElements == A.S.NumberOfElements);
            Assert.IsFalse(object.ReferenceEquals(B.Storage, A.Storage));
            Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // ! works on vectors without copy
            Assert.IsTrue(B.S[0] == 2);
            Assert.IsTrue(B.S[1] == 4);
            Assert.IsTrue(B.GetValue(0, 0) == 0);
            Assert.IsTrue(B.GetValue(0, 1) == 1);
            Assert.IsTrue(B.GetValue(0, 2) == 2);
            Assert.IsTrue(B.GetValue(0, 3) == 3);
            Assert.IsTrue(B.GetValue(1, 0) == 4);
            Assert.IsTrue(B.GetValue(1, 1) == 5);
            Assert.IsTrue(B.GetValue(1, 2) == 6);
            Assert.IsTrue(B.GetValue(1, 3) == 7);
            Assert.IsTrue(B.S.IsContinuous);
            Assert.IsTrue(B.S.StorageOrder == StorageOrders.RowMajor);

        }
        [TestMethod]
        public void Reshape2DimVectorDefaultOrderTest() {
            Array<uint> A = new uint[] { 0, 1, 2, 3, 4, 5, 6, 7 };

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                Array<uint> B = A.Reshape(2, 4);

                Assert.IsTrue(B.S.NumberOfDimensions == Math.Max(2, Settings.MinNumberOfArrayDimensions));
                Assert.IsTrue(B.S.NumberOfElements == A.S.NumberOfElements);
                Assert.IsFalse(object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // ! works on vectors without copy
                Assert.IsTrue(B.S[0] == 2);
                Assert.IsTrue(B.S[1] == 4);
                Assert.IsTrue(B.GetValue(0, 0) == 0);
                Assert.IsTrue(B.GetValue(0, 1) == 1);
                Assert.IsTrue(B.GetValue(0, 2) == 2);
                Assert.IsTrue(B.GetValue(0, 3) == 3);
                Assert.IsTrue(B.GetValue(1, 0) == 4); 
                Assert.IsTrue(B.GetValue(1, 1) == 5);
                Assert.IsTrue(B.GetValue(1, 2) == 6);
                Assert.IsTrue(B.GetValue(1, 3) == 7);
                Assert.IsTrue(B.S.IsContinuous);
                Assert.IsTrue(B.S.StorageOrder == StorageOrders.RowMajor);

            }

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {
                Array<uint> B = A.Reshape(2, 4);
                Assert.IsTrue(B.S.StorageOrder == StorageOrders.ColumnMajor);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Reshape2DimContinousColWrongDimsProdErrorTest() {
            Array<uint> A = new uint[,] { { 0, 1, 2, 3, 4, 5, 6, 7 } };

            Array<uint> B = A.Reshape(2, 3, StorageOrders.RowMajor);
 
        }


        [TestMethod]
        public void Reshape3DimCopyColumnTest() {
            Array<int> A = new int[,] {
                { 0, 1, 2 }, 
                { 3, 4, 5 }, 
                { 6, 7, 8 },
                { 9, 10, 11 }, 
                {12, 13, 14 }, 
                {15, 16, 17 },
            };

            Array<int> B = A.Reshape(2,3,3);

            Assert.IsTrue(B.S.NumberOfDimensions == Math.Max(3,Settings.MinNumberOfArrayDimensions));
            Assert.IsTrue(B.S.NumberOfElements == A.S.NumberOfElements);
            Assert.IsFalse(object.ReferenceEquals(B.Storage, A.Storage));
            Assert.IsFalse(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles));
            Assert.IsTrue(B.S[0] == 2);
            Assert.IsTrue(B.S[1] == 3);
            Assert.IsTrue(B.S[2] == 3);
            Assert.IsTrue(B.GetValue(0, 0, 0) == 0);
            Assert.IsTrue(B.GetValue(1, 0, 0) == 3);
            Assert.IsTrue(B.GetValue(0, 1, 0) == 6);
            Assert.IsTrue(B.GetValue(1, 1, 0) == 9);
            Assert.IsTrue(B.GetValue(0, 2, 0) == 12);
            Assert.IsTrue(B.GetValue(1, 2, 0) == 15);
            Assert.IsTrue(B.GetValue(0, 0, 1) == 1);
            Assert.IsTrue(B.GetValue(1, 0, 1) == 4);
            Assert.IsTrue(B.GetValue(0, 1, 1) == 7);
            Assert.IsTrue(B.GetValue(1, 1, 1) == 10);
            Assert.IsTrue(B.GetValue(0, 2, 1) == 13);
            Assert.IsTrue(B.GetValue(1, 2, 1) == 16);
            Assert.IsTrue(B.GetValue(0, 0, 2) == 2);
            Assert.IsTrue(B.GetValue(1, 0, 2) == 5);
            Assert.IsTrue(B.GetValue(0, 1, 2) == 8);
            Assert.IsTrue(B.GetValue(1, 1, 2) == 11);
            Assert.IsTrue(B.GetValue(0, 2, 2) == 14);
            Assert.IsTrue(B.GetValue(1, 2, 2) == 17);
            Assert.IsTrue(B.S.IsContinuous);
            Assert.IsTrue(B.S.StorageOrder == Settings.DefaultStorageOrder);

        }
        [TestMethod]
        public void Reshape3DimNoCopyRowTest() {
            Array<int> A = new int[,] {
                { 0, 1, 2 }, 
                { 3, 4, 5 }, 
                { 6, 7, 8 },
                { 9, 10, 11 }, 
                {12, 13, 14 }, 
                {15, 16, 17 },
            };

            Array<int> B = A.Reshape(2,3,3, StorageOrders.RowMajor);

            Assert.IsTrue(B.S.NumberOfDimensions == Math.Max(3,Settings.MinNumberOfArrayDimensions));
            Assert.IsTrue(B.S.NumberOfElements == A.S.NumberOfElements);
            Assert.IsFalse(object.ReferenceEquals(B.Storage, A.Storage));
            Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles));
            Assert.IsTrue(B.S[0] == 2);
            Assert.IsTrue(B.S[1] == 3);
            Assert.IsTrue(B.S[2] == 3);
            Assert.IsTrue(B.GetValue(0, 0, 0) == 0);
            Assert.IsTrue(B.GetValue(0, 0, 1) == 1);
            Assert.IsTrue(B.GetValue(0, 0, 2) == 2);
            Assert.IsTrue(B.GetValue(0, 1, 0) == 3);
            Assert.IsTrue(B.GetValue(0, 1, 1) == 4);
            Assert.IsTrue(B.GetValue(0, 1, 2) == 5);
            Assert.IsTrue(B.GetValue(0, 2, 0) == 6);
            Assert.IsTrue(B.GetValue(0, 2, 1) == 7);
            Assert.IsTrue(B.GetValue(0, 2, 2) == 8);
            Assert.IsTrue(B.GetValue(1, 0, 0) == 9);
            Assert.IsTrue(B.GetValue(1, 0, 1) == 10);
            Assert.IsTrue(B.GetValue(1, 0, 2) == 11);
            Assert.IsTrue(B.GetValue(1, 1, 0) == 12);
            Assert.IsTrue(B.GetValue(1, 1, 1) == 13);
            Assert.IsTrue(B.GetValue(1, 1, 2) == 14);
            Assert.IsTrue(B.GetValue(1, 2, 0) == 15);
            Assert.IsTrue(B.GetValue(1, 2, 1) == 16);
            Assert.IsTrue(B.GetValue(1, 2, 2) == 17);
            Assert.IsTrue(B.S.IsContinuous);
            Assert.IsTrue(B.S.StorageOrder == StorageOrders.RowMajor);

        }
        [TestMethod]
        public void Reshape4DimColCopyTest() {
            // this is 1x2x3x2
            Array<double> A = new double[,,,] {
                {
                    {
                        { 1, 2 }, { 3, 4 }, { 5, 6 }
                    },
                    {
                        { 7, 8 }, { 9, 10 }, { 11, 12 }
                    }
                }
            };
            // turn this into: 2x3x1x2

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {
                Array<double> B = A.Reshape(2, 3, 1, 2);

                Assert.IsTrue(B.S.NumberOfDimensions == Math.Max(4, Settings.MinNumberOfArrayDimensions));
                Assert.IsTrue(B.S.NumberOfElements == A.S.NumberOfElements);

                Assert.IsTrue(A.Storage.Size.StorageOrder == StorageOrders.RowMajor);
                Assert.IsTrue(B.Storage.Size.StorageOrder == StorageOrders.ColumnMajor);
                Assert.IsFalse(object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsFalse(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // copy!
                Assert.IsTrue(B.S[0] == 2);
                Assert.IsTrue(B.S[1] == 3);
                Assert.IsTrue(B.S[2] == 1);
                Assert.IsTrue(B.S[3] == 2);
                Assert.IsTrue(B.GetValue(0, 0, 0, 0) == 1);
                Assert.IsTrue(B.GetValue(1, 0, 0, 0) == 7);
                Assert.IsTrue(B.GetValue(0, 1, 0, 0) == 3);
                Assert.IsTrue(B.GetValue(1, 1, 0, 0) == 9);
                Assert.IsTrue(B.GetValue(0, 2, 0, 0) == 5);
                Assert.IsTrue(B.GetValue(1, 2, 0, 0) == 11);
                Assert.IsTrue(B.GetValue(0, 0, 0, 1) == 2);
                Assert.IsTrue(B.GetValue(1, 0, 0, 1) == 8);
                Assert.IsTrue(B.GetValue(0, 1, 0, 1) == 4);
                Assert.IsTrue(B.GetValue(1, 1, 0, 1) == 10);
                Assert.IsTrue(B.GetValue(0, 2, 0, 1) == 6);
                Assert.IsTrue(B.GetValue(1, 2, 0, 1) == 12);
                Assert.IsTrue(B.S.IsContinuous);
                Assert.IsTrue(B.S.StorageOrder == StorageOrders.ColumnMajor);
            }
        }
        [TestMethod]
        public void Reshape4DimRowNoCopyTest() {
            // this is 1x2x3x2
            Array<double> A = new double[,,,] {
                {
                    {
                        { 1, 2 }, { 3, 4 }, { 5, 6 }
                    },
                    {
                        { 7, 8 }, { 9, 10 }, { 11, 12 }
                    }
                }
            };
            // turn this into: 2x3x1x2

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> B = A.Reshape(2, 3, 1, 2);

                Assert.IsTrue(B.S.NumberOfDimensions == Math.Max(4, Settings.MinNumberOfArrayDimensions));
                Assert.IsTrue(B.S.NumberOfElements == A.S.NumberOfElements);

                Assert.IsTrue(A.Storage.Size.StorageOrder == StorageOrders.RowMajor);
                Assert.IsTrue(B.Storage.Size.StorageOrder == StorageOrders.RowMajor);
                Assert.IsFalse(object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // no copy!
                Assert.IsTrue(B.S[0] == 2);
                Assert.IsTrue(B.S[1] == 3);
                Assert.IsTrue(B.S[2] == 1);
                Assert.IsTrue(B.S[3] == 2);
                Assert.IsTrue(B.GetValue(0, 0, 0, 0) == 01);
                Assert.IsTrue(B.GetValue(0, 0, 0, 1) == 02);
                Assert.IsTrue(B.GetValue(0, 1, 0, 0) == 03);
                Assert.IsTrue(B.GetValue(0, 1, 0, 1) == 04);
                Assert.IsTrue(B.GetValue(0, 2, 0, 0) == 05);
                Assert.IsTrue(B.GetValue(0, 2, 0, 1) == 06);
                Assert.IsTrue(B.GetValue(1, 0, 0, 0) == 07);
                Assert.IsTrue(B.GetValue(1, 0, 0, 1) == 08);
                Assert.IsTrue(B.GetValue(1, 1, 0, 0) == 09);
                Assert.IsTrue(B.GetValue(1, 1, 0, 1) == 10);
                Assert.IsTrue(B.GetValue(1, 2, 0, 0) == 11);
                Assert.IsTrue(B.GetValue(1, 2, 0, 1) == 12);
                Assert.IsTrue(B.S.IsContinuous);
            }
        }
        [TestMethod]
        public void Reshape5DimColCopyTest() {
            // this is 1x2x3x2
            Array<double> A = new double[,,,] {
                {
                    {
                        { 1, 2 }, { 3, 4 }, { 5, 6 }
                    },
                    {
                        { 7, 8 }, { 9, 10 }, { 11, 12 }
                    }
                }
            };
            // turn this into: 2x3x1x1x2

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> B = A.Reshape(2, 3, 1, 1, 2);

                Assert.IsTrue(B.S.NumberOfDimensions == Math.Max(5, Settings.MinNumberOfArrayDimensions));
                Assert.IsTrue(B.S.NumberOfElements == A.S.NumberOfElements);

                Assert.IsTrue(A.Storage.Size.StorageOrder == StorageOrders.RowMajor);
                Assert.IsTrue(B.Storage.Size.StorageOrder == StorageOrders.ColumnMajor);
                Assert.IsFalse(object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsFalse(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // copy!
                Assert.IsTrue(B.S[0] == 2);
                Assert.IsTrue(B.S[1] == 3);
                Assert.IsTrue(B.S[2] == 1);
                Assert.IsTrue(B.S[3] == 1);
                Assert.IsTrue(B.S[4] == 2);
                Assert.IsTrue(B.GetValue(0, 0, 0, 0, 0) == 1);
                Assert.IsTrue(B.GetValue(1, 0, 0, 0, 0) == 7);
                Assert.IsTrue(B.GetValue(0, 1, 0, 0, 0) == 3);
                Assert.IsTrue(B.GetValue(1, 1, 0, 0, 0) == 9);
                Assert.IsTrue(B.GetValue(0, 2, 0, 0, 0) == 5);
                Assert.IsTrue(B.GetValue(1, 2, 0, 0, 0) == 11);
                Assert.IsTrue(B.GetValue(0, 0, 0, 0, 1) == 2);
                Assert.IsTrue(B.GetValue(1, 0, 0, 0, 1) == 8);
                Assert.IsTrue(B.GetValue(0, 1, 0, 0, 1) == 4);
                Assert.IsTrue(B.GetValue(1, 1, 0, 0, 1) == 10);
                Assert.IsTrue(B.GetValue(0, 2, 0, 0, 1) == 6);
                Assert.IsTrue(B.GetValue(1, 2, 0, 0, 1) == 12);
                Assert.IsTrue(B.S.IsContinuous);
                Assert.IsTrue(B.S.StorageOrder == StorageOrders.ColumnMajor);
            }
        }
        [TestMethod]
        public void Reshape5DimRowNoCopyTest() {
            // this is 1x2x3x2
            Array<double> A = new double[,,,] {
                {
                    {
                        { 1, 2 }, { 3, 4 }, { 5, 6 }
                    },
                    {
                        { 7, 8 }, { 9, 10 }, { 11, 12 }
                    }
                }
            };
            // turn this into: 2x3x1x1x2

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> B = A.Reshape(2, 3, 1, 1, 2);

                Assert.IsTrue(B.S.NumberOfDimensions == Math.Max(5, Settings.MinNumberOfArrayDimensions));
                Assert.IsTrue(B.S.NumberOfElements == A.S.NumberOfElements);

                Assert.IsTrue(A.Storage.Size.StorageOrder == StorageOrders.RowMajor);
                Assert.IsTrue(B.Storage.Size.StorageOrder == StorageOrders.RowMajor);
                Assert.IsFalse(object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // no copy!
                Assert.IsTrue(B.S[0] == 2);
                Assert.IsTrue(B.S[1] == 3);
                Assert.IsTrue(B.S[2] == 1);
                Assert.IsTrue(B.S[3] == 1);
                Assert.IsTrue(B.S[4] == 2);
                Assert.IsTrue(B.GetValue(0, 0, 0, 0, 0) == 01);
                Assert.IsTrue(B.GetValue(0, 0, 0, 0, 1) == 02);
                Assert.IsTrue(B.GetValue(0, 1, 0, 0, 0) == 03);
                Assert.IsTrue(B.GetValue(0, 1, 0, 0, 1) == 04);
                Assert.IsTrue(B.GetValue(0, 2, 0, 0, 0) == 05);
                Assert.IsTrue(B.GetValue(0, 2, 0, 0, 1) == 06);
                Assert.IsTrue(B.GetValue(1, 0, 0, 0, 0) == 07);
                Assert.IsTrue(B.GetValue(1, 0, 0, 0, 1) == 08);
                Assert.IsTrue(B.GetValue(1, 1, 0, 0, 0) == 09);
                Assert.IsTrue(B.GetValue(1, 1, 0, 0, 1) == 10);
                Assert.IsTrue(B.GetValue(1, 2, 0, 0, 0) == 11);
                Assert.IsTrue(B.GetValue(1, 2, 0, 0, 1) == 12);
                Assert.IsTrue(B.S.IsContinuous);
            }
        }
        [TestMethod]
        public void Reshape6DimColCopyTest() {
            // this is 1x2x3x2
            Array<double> A = new double[,,,] {
                {
                    {
                        { 1, 2 }, { 3, 4 }, { 5, 6 }
                    },
                    {
                        { 7, 8 }, { 9, 10 }, { 11, 12 }
                    }
                }
            };
            // turn this into: 2x3x1x1x2

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> B = A.Reshape(2, 3, 1, 1, 1, 2);

                Assert.IsTrue(B.S.NumberOfDimensions == Math.Max(6, Settings.MinNumberOfArrayDimensions));
                Assert.IsTrue(B.S.NumberOfElements == A.S.NumberOfElements);

                Assert.IsTrue(A.Storage.Size.StorageOrder == StorageOrders.RowMajor);
                Assert.IsTrue(B.Storage.Size.StorageOrder == StorageOrders.ColumnMajor);
                Assert.IsFalse(object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsFalse(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // copy!
                Assert.IsTrue(B.S[0] == 2);
                Assert.IsTrue(B.S[1] == 3);
                Assert.IsTrue(B.S[2] == 1);
                Assert.IsTrue(B.S[3] == 1);
                Assert.IsTrue(B.S[4] == 1);
                Assert.IsTrue(B.S[5] == 2);
                Assert.IsTrue(B.GetValue(0, 0, 0, 0, 0, 0) == 1);
                Assert.IsTrue(B.GetValue(1, 0, 0, 0, 0, 0) == 7);
                Assert.IsTrue(B.GetValue(0, 1, 0, 0, 0, 0) == 3);
                Assert.IsTrue(B.GetValue(1, 1, 0, 0, 0, 0) == 9);
                Assert.IsTrue(B.GetValue(0, 2, 0, 0, 0, 0) == 5);
                Assert.IsTrue(B.GetValue(1, 2, 0, 0, 0, 0) == 11);
                Assert.IsTrue(B.GetValue(0, 0, 0, 0, 0, 1) == 2);
                Assert.IsTrue(B.GetValue(1, 0, 0, 0, 0, 1) == 8);
                Assert.IsTrue(B.GetValue(0, 1, 0, 0, 0, 1) == 4);
                Assert.IsTrue(B.GetValue(1, 1, 0, 0, 0, 1) == 10);
                Assert.IsTrue(B.GetValue(0, 2, 0, 0, 0, 1) == 6);
                Assert.IsTrue(B.GetValue(1, 2, 0, 0, 0, 1) == 12);
                Assert.IsTrue(B.S.IsContinuous);
            }
        }
        [TestMethod]
        public void Reshape6DimRowNoCopyTest() {
            // this is 1x2x3x2
            Array<double> A = new double[,,,] {
                {
                    {
                        { 1, 2 }, { 3, 4 }, { 5, 6 }
                    },
                    {
                        { 7, 8 }, { 9, 10 }, { 11, 12 }
                    }
                }
            };
            // turn this into: 2x3x1x1x2

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> B = A.Reshape(2, 3, 1, 1, 1, 2);

                Assert.IsTrue(B.S.NumberOfDimensions == Math.Max(6, Settings.MinNumberOfArrayDimensions));
                Assert.IsTrue(B.S.NumberOfElements == A.S.NumberOfElements);

                Assert.IsTrue(A.Storage.Size.StorageOrder == StorageOrders.RowMajor);
                Assert.IsTrue(B.Storage.Size.StorageOrder == StorageOrders.RowMajor);
                Assert.IsFalse(object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // no copy!
                Assert.IsTrue(B.S[0] == 2);
                Assert.IsTrue(B.S[1] == 3);
                Assert.IsTrue(B.S[2] == 1);
                Assert.IsTrue(B.S[3] == 1);
                Assert.IsTrue(B.S[4] == 1);
                Assert.IsTrue(B.S[5] == 2);
                Assert.IsTrue(B.GetValue(0, 0, 0, 0, 0, 0) == 01);
                Assert.IsTrue(B.GetValue(0, 0, 0, 0, 0, 1) == 02);
                Assert.IsTrue(B.GetValue(0, 1, 0, 0, 0, 0) == 03);
                Assert.IsTrue(B.GetValue(0, 1, 0, 0, 0, 1) == 04);
                Assert.IsTrue(B.GetValue(0, 2, 0, 0, 0, 0) == 05);
                Assert.IsTrue(B.GetValue(0, 2, 0, 0, 0, 1) == 06);
                Assert.IsTrue(B.GetValue(1, 0, 0, 0, 0, 0) == 07);
                Assert.IsTrue(B.GetValue(1, 0, 0, 0, 0, 1) == 08);
                Assert.IsTrue(B.GetValue(1, 1, 0, 0, 0, 0) == 09);
                Assert.IsTrue(B.GetValue(1, 1, 0, 0, 0, 1) == 10);
                Assert.IsTrue(B.GetValue(1, 2, 0, 0, 0, 0) == 11);
                Assert.IsTrue(B.GetValue(1, 2, 0, 0, 0, 1) == 12);
                Assert.IsTrue(B.S.IsContinuous);
            }
        }
        [TestMethod]
        public void Reshape7DimColCopyTest() {
            // this is 1x2x3x2
            Array<double> A = new double[,,,] {
                {
                    {
                        { 1, 2 }, { 3, 4 }, { 5, 6 }
                    },
                    {
                        { 7, 8 }, { 9, 10 }, { 11, 12 }
                    }
                }
            };
            // turn this into: 2x3x1x1x2

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> B = A.Reshape(2, 3, 1, 1, 1, 1, 2);

                Assert.IsTrue(B.S.NumberOfDimensions == Math.Max(7, Settings.MinNumberOfArrayDimensions));
                Assert.IsTrue(B.S.NumberOfElements == A.S.NumberOfElements);

                Assert.IsTrue(A.Storage.Size.StorageOrder == StorageOrders.RowMajor);
                Assert.IsTrue(B.Storage.Size.StorageOrder == StorageOrders.ColumnMajor);
                Assert.IsFalse(object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsFalse(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // copy!
                Assert.IsTrue(B.S[0] == 2);
                Assert.IsTrue(B.S[1] == 3);
                Assert.IsTrue(B.S[2] == 1);
                Assert.IsTrue(B.S[3] == 1);
                Assert.IsTrue(B.S[4] == 1);
                Assert.IsTrue(B.S[5] == 1);
                Assert.IsTrue(B.S[6] == 2);
                Assert.IsTrue(B.GetValue(0, 0, 0, 0, 0, 0, 0) == 1);
                Assert.IsTrue(B.GetValue(1, 0, 0, 0, 0, 0, 0) == 7);
                Assert.IsTrue(B.GetValue(0, 1, 0, 0, 0, 0, 0) == 3);
                Assert.IsTrue(B.GetValue(1, 1, 0, 0, 0, 0, 0) == 9);
                Assert.IsTrue(B.GetValue(0, 2, 0, 0, 0, 0, 0) == 5);
                Assert.IsTrue(B.GetValue(1, 2, 0, 0, 0, 0, 0) == 11);
                Assert.IsTrue(B.GetValue(0, 0, 0, 0, 0, 0, 1) == 2);
                Assert.IsTrue(B.GetValue(1, 0, 0, 0, 0, 0, 1) == 8);
                Assert.IsTrue(B.GetValue(0, 1, 0, 0, 0, 0, 1) == 4);
                Assert.IsTrue(B.GetValue(1, 1, 0, 0, 0, 0, 1) == 10);
                Assert.IsTrue(B.GetValue(0, 2, 0, 0, 0, 0, 1) == 6);
                Assert.IsTrue(B.GetValue(1, 2, 0, 0, 0, 0, 1) == 12);
                Assert.IsTrue(B.S.IsContinuous);
                Assert.IsTrue(B.S.StorageOrder == StorageOrders.ColumnMajor);
            }
        }
        [TestMethod]
        public void Reshape7DimRowNoCopyTest() {
            // this is 1x2x3x2
            Array<double> A = new double[,,,] {
                {
                    {
                        { 1, 2 }, { 3, 4 }, { 5, 6 }
                    },
                    {
                        { 7, 8 }, { 9, 10 }, { 11, 12 }
                    }
                }
            };
            // turn this into: 2x3x1x1x2

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> B = A.Reshape(2, 3, 1, 1, 1, 1, 2);

                Assert.IsTrue(B.S.NumberOfDimensions == Math.Max(7, Settings.MinNumberOfArrayDimensions));
                Assert.IsTrue(B.S.NumberOfElements == A.S.NumberOfElements);

                Assert.IsTrue(A.Storage.Size.StorageOrder == StorageOrders.RowMajor);
                Assert.IsTrue(B.Storage.Size.StorageOrder == StorageOrders.RowMajor);
                Assert.IsFalse(object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // no copy!
                Assert.IsTrue(B.S[0] == 2);
                Assert.IsTrue(B.S[1] == 3);
                Assert.IsTrue(B.S[2] == 1);
                Assert.IsTrue(B.S[3] == 1);
                Assert.IsTrue(B.S[4] == 1);
                Assert.IsTrue(B.S[5] == 1);
                Assert.IsTrue(B.S[6] == 2);
                Assert.IsTrue(B.GetValue(0, 0, 0, 0, 0, 0, 0) == 01);
                Assert.IsTrue(B.GetValue(0, 0, 0, 0, 0, 0, 1) == 02);
                Assert.IsTrue(B.GetValue(0, 1, 0, 0, 0, 0, 0) == 03);
                Assert.IsTrue(B.GetValue(0, 1, 0, 0, 0, 0, 1) == 04);
                Assert.IsTrue(B.GetValue(0, 2, 0, 0, 0, 0, 0) == 05);
                Assert.IsTrue(B.GetValue(0, 2, 0, 0, 0, 0, 1) == 06);
                Assert.IsTrue(B.GetValue(1, 0, 0, 0, 0, 0, 0) == 07);
                Assert.IsTrue(B.GetValue(1, 0, 0, 0, 0, 0, 1) == 08);
                Assert.IsTrue(B.GetValue(1, 1, 0, 0, 0, 0, 0) == 09);
                Assert.IsTrue(B.GetValue(1, 1, 0, 0, 0, 0, 1) == 10);
                Assert.IsTrue(B.GetValue(1, 2, 0, 0, 0, 0, 0) == 11);
                Assert.IsTrue(B.GetValue(1, 2, 0, 0, 0, 0, 1) == 12);
                Assert.IsTrue(B.S.IsContinuous);
            }
        }
        [TestMethod]
        public void Reshape4DimColCopyLenghtsArrayTest() {
            // this is 1x2x3x2
            Array<double> A = new double[,,,] {
                {
                    {
                        { 1, 2 }, { 3, 4 }, { 5, 6 }
                    },
                    {
                        { 7, 8 }, { 9, 10 }, { 11, 12 }
                    }
                }
            };
            // turn this into: 2x3x1x2

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> B = A.Reshape(new uint[] { 2, 3, 1, 2 });

                Assert.IsTrue(B.S.NumberOfDimensions == Math.Max(4, Settings.MinNumberOfArrayDimensions));
                Assert.IsTrue(B.S.NumberOfElements == A.S.NumberOfElements);

                Assert.IsTrue(A.Storage.Size.StorageOrder == StorageOrders.RowMajor);
                Assert.IsTrue(B.Storage.Size.StorageOrder == StorageOrders.ColumnMajor);
                Assert.IsFalse(object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsFalse(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // copy!
                Assert.IsTrue(B.S[0] == 2);
                Assert.IsTrue(B.S[1] == 3);
                Assert.IsTrue(B.S[2] == 1);
                Assert.IsTrue(B.S[3] == 2);
                Assert.IsTrue(B.GetValue(0, 0, 0, 0) == 1);
                Assert.IsTrue(B.GetValue(1, 0, 0, 0) == 7);
                Assert.IsTrue(B.GetValue(0, 1, 0, 0) == 3);
                Assert.IsTrue(B.GetValue(1, 1, 0, 0) == 9);
                Assert.IsTrue(B.GetValue(0, 2, 0, 0) == 5);
                Assert.IsTrue(B.GetValue(1, 2, 0, 0) == 11);
                Assert.IsTrue(B.GetValue(0, 0, 0, 1) == 2);
                Assert.IsTrue(B.GetValue(1, 0, 0, 1) == 8);
                Assert.IsTrue(B.GetValue(0, 1, 0, 1) == 4);
                Assert.IsTrue(B.GetValue(1, 1, 0, 1) == 10);
                Assert.IsTrue(B.GetValue(0, 2, 0, 1) == 6);
                Assert.IsTrue(B.GetValue(1, 2, 0, 1) == 12);
                Assert.IsTrue(B.S.IsContinuous);
                Assert.IsTrue(B.S.StorageOrder == StorageOrders.ColumnMajor);
            }
        }
        [TestMethod]
        public void Reshape4DimRowNoCopyLenghtsArrayTest() {
            // this is 1x2x3x2
            Array<double> A = new double[,,,] {
                {
                    {
                        { 1, 2 }, { 3, 4 }, { 5, 6 }
                    },
                    {
                        { 7, 8 }, { 9, 10 }, { 11, 12 }
                    }
                }
            };
            // turn this into: 2x3x1x2

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> B = A.Reshape(new uint[] { 2, 3, 1, 2 });

                Assert.IsTrue(B.S.NumberOfDimensions == Math.Max(4, Settings.MinNumberOfArrayDimensions));
                Assert.IsTrue(B.S.NumberOfElements == A.S.NumberOfElements);

                Assert.IsTrue(A.Storage.Size.StorageOrder == StorageOrders.RowMajor);
                Assert.IsTrue(B.Storage.Size.StorageOrder == StorageOrders.RowMajor);
                Assert.IsFalse(object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // no copy!
                Assert.IsTrue(B.S[0] == 2);
                Assert.IsTrue(B.S[1] == 3);
                Assert.IsTrue(B.S[2] == 1);
                Assert.IsTrue(B.S[3] == 2);
                Assert.IsTrue(B.GetValue(0, 0, 0, 0) == 01);
                Assert.IsTrue(B.GetValue(0, 0, 0, 1) == 02);
                Assert.IsTrue(B.GetValue(0, 1, 0, 0) == 03);
                Assert.IsTrue(B.GetValue(0, 1, 0, 1) == 04);
                Assert.IsTrue(B.GetValue(0, 2, 0, 0) == 05);
                Assert.IsTrue(B.GetValue(0, 2, 0, 1) == 06);
                Assert.IsTrue(B.GetValue(1, 0, 0, 0) == 07);
                Assert.IsTrue(B.GetValue(1, 0, 0, 1) == 08);
                Assert.IsTrue(B.GetValue(1, 1, 0, 0) == 09);
                Assert.IsTrue(B.GetValue(1, 1, 0, 1) == 10);
                Assert.IsTrue(B.GetValue(1, 2, 0, 0) == 11);
                Assert.IsTrue(B.GetValue(1, 2, 0, 1) == 12);
                Assert.IsTrue(B.S.IsContinuous);
            }
        }

        [TestMethod]
        public void ReshapeLongSimpleTest() {

            Array<int> A = new int[10000];
            Array<int> B = A.Reshape((long)10, 100, 1, 2, 5, StorageOrders.RowMajor);

            Assert.IsTrue(B.S[0] == 10); 
            Assert.IsTrue(B.S[1] == 100);
            Assert.IsTrue(B.S[2] == 1); 
            Assert.IsTrue(B.S[3] == 2);
            Assert.IsTrue(B.S[4] == 5);

            Assert.IsFalse(A.Storage == B.Storage); 
            Assert.IsTrue(A.Storage.m_handles == B.Storage.m_handles); 

        }
        [TestMethod]
        public void ReshapeLongInfer0DimTest() {
            Array<int> A = new int[10000];
            Array<int> B = A.Reshape((long)-5, 100, 1, 2, 5, StorageOrders.RowMajor);

            Assert.IsTrue(B.S[0] == 10);
            Assert.IsTrue(B.S[1] == 100);
            Assert.IsTrue(B.S[2] == 1);
            Assert.IsTrue(B.S[3] == 2);
            Assert.IsTrue(B.S[4] == 5);

            Assert.IsFalse(A.Storage == B.Storage);
            Assert.IsTrue(A.Storage.m_handles == B.Storage.m_handles);
        }
        [TestMethod]
        public void ReshapeLongInfer2DimTest() {
            Array<int> A = new int[10000];
            Array<int> B = A.Reshape((long)10, 100, -1, 2, 5, StorageOrders.RowMajor);

            Assert.IsTrue(B.S[0] == 10);
            Assert.IsTrue(B.S[1] == 100);
            Assert.IsTrue(B.S[2] == 1);
            Assert.IsTrue(B.S[3] == 2);
            Assert.IsTrue(B.S[4] == 5);

            Assert.IsFalse(A.Storage == B.Storage);
            Assert.IsTrue(A.Storage.m_handles == B.Storage.m_handles);
        }
        [TestMethod]
        public void ReshapeLongInferLastDimTest() {
            Array<int> A = new int[10000];
            Array<int> B = A.Reshape((long)10, 100, 1, 2, -5, StorageOrders.ColumnMajor);

            Assert.IsTrue(B.S[0] == 10);
            Assert.IsTrue(B.S[1] == 100);
            Assert.IsTrue(B.S[2] == 1);
            Assert.IsTrue(B.S[3] == 2);
            Assert.IsTrue(B.S[4] == 5);

            Assert.IsFalse(A.Storage == B.Storage);
            Assert.IsTrue(A.Storage.m_handles == B.Storage.m_handles); // vector!

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReshapeLongInferMultipleErrorTest() {
            Array<int> A = new int[10000];

            Array<int> B = A.Reshape((long)10, -100, 1, 2, -5, StorageOrders.ColumnMajor);
 
        }

        public void ReshapeLongInferVectorRowTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<int> A = new int[10000];
                Array<int> B = A.Reshape((long)-1);

                Assert.IsTrue(B.S[0] == 10000);
                Assert.IsTrue(B.S[1] == 1);
                Assert.IsTrue(B.S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions);

                Assert.IsFalse(A.Storage == B.Storage);
                Assert.IsTrue(A.Storage.m_handles == B.Storage.m_handles);
            }
        }
        public void ReshapeLongInferVectorColumnTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<int> A = new int[10000];
                Array<int> B = A.Reshape((long)-1);

                Assert.IsTrue(B.S[0] == 10000);
                Assert.IsTrue(B.S[1] == 1);
                Assert.IsTrue(B.S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions);

                Assert.IsFalse(A.Storage == B.Storage);
                Assert.IsTrue(A.Storage.m_handles == B.Storage.m_handles);
            }
        }
        [TestMethod]
        public void ReshapeUIntEmptyTest() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {
                Array<int> A = new int[0];
                Assert.IsTrue(A.S[0] == 0, $"Current default setting: {Settings.DefaultStorageOrder}");
                Assert.IsTrue(A.S[1] == 1);
                Assert.IsTrue(A.S.NumberOfDimensions == 2); 
                Assert.IsTrue(A.S.NumberOfElements == 0);

                Array<int> B = A.Reshape(10,0,2,3);

                Assert.IsTrue(B.S[0] == 10);
                Assert.IsTrue(B.S[1] == 0);
                Assert.IsTrue(B.S[2] == 2);
                Assert.IsTrue(B.S[3] == 3);
                Assert.IsTrue(B.S.NumberOfDimensions >= Settings.MinNumberOfArrayDimensions);

                Assert.IsFalse(A.Storage == B.Storage);
                Assert.IsTrue(A.Storage.m_handles == B.Storage.m_handles);
            }
        }
        [TestMethod]
        public void ReshapeLongEmptyInferTest() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) { 

                Array<int> A = new int[0];
                Assert.IsTrue(A.S[0] == 0);
                Assert.IsTrue(A.S[1] == 1);
                Assert.IsTrue(A.S.NumberOfDimensions == 2);
                Assert.IsTrue(A.S.NumberOfElements == 0);

                Array<int> B = A.Reshape((long)10, 0, -2, 3);

                Assert.IsTrue(B.S[0] == 10);
                Assert.IsTrue(B.S[1] == 0);
                Assert.IsTrue(B.S[2] == 0);
                Assert.IsTrue(B.S[3] == 3);
                Assert.IsTrue(B.S.NumberOfDimensions >= Settings.MinNumberOfArrayDimensions);

                Assert.IsFalse(A.Storage == B.Storage);
                Assert.IsTrue(A.Storage.m_handles == B.Storage.m_handles);
            }

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReshapeVectorInvalidLengthUIntTest() {
                Array<int> A = new int[10];
                Array<int> B = A.Reshape(11);
 
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReshapeVectorInvalidLengthLongTest() {
                Array<int> A = new int[10];
                Array<int> B = A.Reshape((long)11);
        }
        [TestMethod]
        public void ReshapeVectorInferLengthLongTest() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {
                Array<int> A = new int[0];
                Assert.IsTrue(A.S[0] == 0);
                Assert.IsTrue(A.S[1] == 1);
                Assert.IsTrue(A.S.NumberOfDimensions == 2);
                Assert.IsTrue(A.S.NumberOfElements == 0);

                Array<int> B = A.Reshape((long)-10);

                Assert.IsTrue(B.S[0] == 0);
                Assert.IsTrue(B.S[1] == 1);
                Assert.IsTrue(B.S.NumberOfDimensions >= Settings.MinNumberOfArrayDimensions);

                Assert.IsFalse(A.Storage == B.Storage);
                Assert.IsTrue(A.Storage.m_handles == B.Storage.m_handles);
            }
        }
    }
}
