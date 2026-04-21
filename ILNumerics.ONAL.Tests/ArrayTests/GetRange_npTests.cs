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
using ILNumerics.Core.Functions.Builtin;
using System.Diagnostics;

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public class GetRange_npTests : NumpyTestClass {

        #region dimspec tests

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetRange_np_EmptyBoolScalarIdxDimSpecOORErr() {
            Logical A = new bool[,] {
                { },
                {  }
            };
            // this triggers dimspec subarray overload
            Logical B = A[1, 0];
        }
        [TestMethod]
        public void GetRange_np_EmptyBoolFullIdxDimSpec() {
            Logical A = new bool[,] {
                { },
                {  }
            };
            // this triggers dimspec subarray overload
            Logical B = A[1, full];

            Assert.IsTrue(B.IsEmpty); 
            Assert.IsTrue(B.S[0]== 0); 
            Assert.IsTrue(B.S[1]== 1); 
            Assert.IsTrue(B.S.NumberOfDimensions == 1); 
        }

        [TestMethod]
        public void GetRange_np_1D_DimSpec_Scalar() {
            Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.numpy);

            Array<double> A = 10;
            Assert.IsTrue(A.Size.NumberOfDimensions == 0 && A.S.NumberOfElements == 1);

            Array<double> B = A[0];

            Assert.IsTrue(B.GetValue(0) == 10);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.S.NumberOfDimensions == 0);
            //Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);

            B = A[full];
            Assert.IsTrue(B.GetValue(0) == 10);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.S.NumberOfDimensions == 0);
            //Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);

            B = A[end];
            Assert.IsTrue(B.GetValue(0) == 10);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.S.NumberOfDimensions == 0);
            //Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);

            B = A[r(0, 0)];
            Assert.IsTrue(B.GetValue(0) == 10);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.S.NumberOfDimensions == 0);
            //Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);

            B = A[r(0, 1, 0)];
            Assert.IsTrue(B.GetValue(0) == 10);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.S.NumberOfDimensions == 0);
            //Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);

            B = A[slice(0, 1)];
            Assert.IsTrue(B.GetValue(0) == 10);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.S.NumberOfDimensions == 0);
            //Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);

            B = A[slice(0, 1, 1)];
            Assert.IsTrue(B.GetValue(0) == 10);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.S.NumberOfDimensions == 0);
            //Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);

            B = A[newaxis];
            Assert.IsTrue(B.GetValue(0) == 10);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.S.NumberOfDimensions == 1);
            //Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);

            B = A[ellipsis];
            Assert.IsTrue(B.GetValue(0) == 10);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.S.NumberOfDimensions == 0);
            //Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);

        }
        [TestMethod]
        public void GetRange_np_3D_EllipsisNewaxis() {
            // this deviates from the "assumed" numpy "specification", because numpy determines the number of full dimensions  
            // replacing ellipsis by neglecting newaxis objects. 
            Array<float> A = counter<float>(1f, 1f, 5, 4, 3, StorageOrders.RowMajor);
            Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.numpy);

            Array<float> B = A[ellipsis, newaxis];
            Assert.IsTrue(B.S.NumberOfDimensions == 4);
            Assert.IsTrue(B.S[0] == 5);
            Assert.IsTrue(B.S[1] == 4);
            Assert.IsTrue(B.S[2] == 3);
            Assert.IsTrue(B.S[3] == 1);

        }
        [TestMethod]
        public void GetRange_np_3D_EllipsisFullNewaxis() {

            Array<float> A = counter<float>(1f, 1f, 5, 4, 3, StorageOrders.RowMajor);
            Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.numpy);

            Array<float> B = A[ellipsis, full, newaxis];
            Assert.IsTrue(B.S.NumberOfDimensions == 4);
            Assert.IsTrue(B.S[0] == 5);
            Assert.IsTrue(B.S[1] == 4);
            Assert.IsTrue(B.S[2] == 3);
            Assert.IsTrue(B.S[3] == 1);

        }
        [TestMethod]
        public void GetRange_np_3D_EllipsisNewaxisFullNewaxis() {

            Array<float> A = counter<float>(1f, 1f, 5, 4, 3, StorageOrders.RowMajor);
            Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.numpy);

            Array<float> B = A[newaxis, ellipsis, newaxis];
            Assert.IsTrue(B.S.NumberOfDimensions == 5);
            Assert.IsTrue(B.S[0] == 1);
            Assert.IsTrue(B.S[1] == 5);
            Assert.IsTrue(B.S[2] == 4);
            Assert.IsTrue(B.S[3] == 3);
            Assert.IsTrue(B.S[4] == 1);

        }
        [TestMethod]
        public void GetRange_np_3D_Ellipsis1st() {

            Array<float> A = counter<float>(1f, 1f, 5, 4, 3, StorageOrders.RowMajor);
            Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.numpy);

            Array<float> B = A[ellipsis, newaxis, full, newaxis];
            Assert.IsTrue(B.S.NumberOfDimensions == 5);
            Assert.IsTrue(B.S[0] == 5);
            Assert.IsTrue(B.S[1] == 4);
            Assert.IsTrue(B.S[2] == 1);
            Assert.IsTrue(B.S[3] == 3);
            Assert.IsTrue(B.S[4] == 1);

        }
        [TestMethod]
        public void GetRange_np_3D_Ellipsis2nd() {

            Array<float> A = counter<float>(1f, 1f, 5, 4, 3, StorageOrders.RowMajor);
            Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.numpy);

            Array<float> B = A[newaxis, ellipsis, full, newaxis];
            Assert.IsTrue(B.S.NumberOfDimensions == 5);
            Assert.IsTrue(B.S[0] == 1);
            Assert.IsTrue(B.S[1] == 5);
            Assert.IsTrue(B.S[2] == 4);
            Assert.IsTrue(B.S[3] == 3);
            Assert.IsTrue(B.S[4] == 1);

        }
        [TestMethod]
        public void GetRange_np_3D_EllipsisFullFullNewaxis() {

            Array<float> A = counter<float>(1f, 1f, 5, 4, 3, StorageOrders.RowMajor);
            Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.numpy);

            Array<float> B = A[ellipsis, full, full, newaxis];
            Assert.IsTrue(B.S.NumberOfDimensions == 4);
            Assert.IsTrue(B.S[0] == 5);
            Assert.IsTrue(B.S[1] == 4);
            Assert.IsTrue(B.S[2] == 3);
            Assert.IsTrue(B.S[3] == 1);

        }
        [TestMethod]
        public void GetRange_np_3D_FullNewaxisEllipsis() {

            Array<float> A = counter<float>(1f, 1f, 5, 4, 3, StorageOrders.RowMajor);
            Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.numpy);

            Array<float> B = A[full, newaxis, ellipsis];
            Assert.IsTrue(B.S.NumberOfDimensions == 4);
            Assert.IsTrue(B.S[0] == 5);
            Assert.IsTrue(B.S[1] == 1);
            Assert.IsTrue(B.S[2] == 4);
            Assert.IsTrue(B.S[3] == 3);

        }
        [TestMethod]
        public void GetRange_np_3D_NewaxisEllipsis() {

            Array<float> A = counter<float>(1f, 1f, 5, 4, 3, StorageOrders.RowMajor);
            Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.numpy);

            Array<float> B = A[newaxis, ellipsis];
            Assert.IsTrue(B.S.NumberOfDimensions == 4);
            Assert.IsTrue(B.S[0] == 1);
            Assert.IsTrue(B.S[1] == 5);
            Assert.IsTrue(B.S[2] == 4);
            Assert.IsTrue(B.S[3] == 3);

        }

        [TestMethod]
        public void GetRange_np_1D_DimSpec_Vector() {

            Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.numpy);

            Array<double> A = new double[] { 1, 2, 3, 4, 5 };

            Assert.IsTrue(A.Size.NumberOfDimensions == 1 && A.S.NumberOfElements == 5);

            Array<double> B = A[0];

            Assert.IsTrue(B.GetValue(0) == 1);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.S.NumberOfDimensions == 0);
            //Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);  // no! Acc uses GetValueSeq in matching situations -> does not share handles

            B = A[1];

            Assert.IsTrue(B.GetValue(0) == 2);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.S.NumberOfDimensions == 0);
            //Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);

            B = A[full];
            Assert.IsTrue(B.GetValue(0) == 1 && B.GetValue(1) == 2 && B.GetValue(4) == 5);
            Assert.IsTrue(B.Equals(A));
            Assert.IsTrue(B.S.NumberOfElements == 5);
            Assert.IsTrue(B.S.NumberOfDimensions == 1);
            Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);

            B = A[ellipsis];
            Assert.IsTrue(B.GetValue(0) == 1 && B.GetValue(1) == 2 && B.GetValue(4) == 5);
            Assert.IsTrue(B.Equals(A));
            Assert.IsTrue(B.S.NumberOfElements == 5);
            Assert.IsTrue(B.S.NumberOfDimensions == 1);
            Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);

            B = A[end];
            Assert.IsTrue(B.GetValue(0) == 5);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.S.NumberOfDimensions == 0);
            Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);

            B = A[r(0, 0)];
            Assert.IsTrue(B.GetValue(0) == 1);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.S.NumberOfDimensions == 1);
            Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);

            B = A[r(0, 1, 0)];
            Assert.IsTrue(B.GetValue(0) == 1);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.S.NumberOfDimensions == 1);
            Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);

            B = A[r(1, 1)];
            Assert.IsTrue(B.GetValue(0) == 2);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.S.NumberOfDimensions == 1);
            Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);

            B = A[r(2, 1, 2)];
            Assert.IsTrue(B.GetValue(0) == 3);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.S.NumberOfDimensions == 1);
            Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);


            B = A[slice(0, 1, 1)];
            Assert.IsTrue(B.GetValue(0) == 1);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.S.NumberOfDimensions == 1);
            Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);

            B = A[slice(1, 2)];
            Assert.IsTrue(B.GetValue(0) == 2);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.S.NumberOfDimensions == 1);
            Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);

            B = A[slice(2, 3, 1)];
            Assert.IsTrue(B.GetValue(0) == 3);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.S.NumberOfDimensions == 1);
            Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);

            B = A[newaxis];
            Assert.IsTrue(B.GetValue(0) == 1);
            Assert.IsTrue(B.GetValue(0, 1) == 2);
            Assert.IsTrue(B.S.NumberOfElements == 5);
            Assert.IsTrue(B.S.NumberOfDimensions == 2);
            Assert.IsTrue(B.S[0] == 1 && B.S[1] == A.S.NumberOfElements);
            Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);

            Assert.IsTrue(A.Equals(A[slice(0, 5)])); 
            Assert.IsTrue(A.Equals(A[r(0, 4)])); 

            Assert.IsTrue(A.Equals(A[slice(0, end + 1)])); 
            Assert.IsTrue(A.Equals(A[r(0, end)]));

            Assert.IsTrue(A.Equals(A[slice(0, end + 1, 1)]));
            Assert.IsTrue(A.Equals(A[r(0, 1, end)]));

            Assert.IsTrue(A.Equals(A[full]));

            Array<double> R = new double[] { 2, 3, 4 };
            Assert.IsTrue(R.Equals(A[slice(1, -1)]));
            Assert.IsTrue(R.Equals(A[slice(1, -1, 1)]));
            Assert.IsTrue(R.Equals(A[slice(1, end)]));
            Assert.IsTrue(R.Equals(A[slice(1, end, 1)]));

            R.a = new double[] { 2, 4 };
            Assert.IsTrue(R.Equals(A[slice(1, -1, 2)]));
            Assert.IsTrue(R.Equals(A[slice(1, end, 2)]));

        }
        [TestMethod]
        public void GetRange_np_1D_DimSpec_Matrix() {
            Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.numpy);

            Array<double> A = new double[,] { { 1, 2, 3 }, { 4, 5, 6 } };

            Assert.IsTrue(A.Size.NumberOfDimensions == 2 && A.S.NumberOfElements == 6);

            Array<double> B = A[0];

            Assert.IsTrue(B.GetValue(0) == 1);
            Assert.IsTrue(B.GetValue(-1) == 3);
            Assert.IsTrue(B.S.NumberOfElements == 3);
            Assert.IsTrue(B.S.NumberOfDimensions == 1);
            //Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);

            B = A[1];

            Assert.IsTrue(B.GetValue(0) == 4);
            Assert.IsTrue(B.Equals(A[1, full])); 
            Assert.IsTrue(B.S.NumberOfElements == 3);
            Assert.IsTrue(B.S.NumberOfDimensions == 1);
            //Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);

            B = A[full];
            Assert.IsTrue(B.GetValue(0) == 1 && B.GetValue(1) == 4 && B.GetValue(5) == 6);
            Assert.IsTrue(B.Equals(A));
            Assert.IsTrue(B.S.NumberOfElements == 6);
            Assert.IsTrue(B.S.NumberOfDimensions == 2);
            //Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);

            B = A[ellipsis];
            Assert.IsTrue(B.GetValue(0) == 1 && B.GetValue(1) == 4 && B.GetValue(5) == 6);
            Assert.IsTrue(B.Equals(A));
            Assert.IsTrue(B.S.NumberOfElements == 6);
            Assert.IsTrue(B.S.NumberOfDimensions == 2);
            //Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);

            B = A[end];
            Assert.IsTrue(B.GetValue(0) == 4 && B.GetValue(-1) == 6);
            Assert.IsTrue(B.Equals(A[1, full]));
            Assert.IsTrue(B.S.NumberOfElements == 3);
            Assert.IsTrue(B.S.NumberOfDimensions == 1);
            //Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);

            B = A[r(0, 0)];
            Assert.IsTrue(B.GetValue(0) == 1 && B.GetValue(0, 1) == 2 && B.GetValue(-1, -1) == 3);
            Assert.IsTrue(B.S.NumberOfElements == 3);
            Assert.IsTrue(B.S.NumberOfDimensions == 2);
            //Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);

            B = A[r(0, 1, 0)];
            Assert.IsTrue(B.GetValue(0) == 1 && B.GetValue(0, 1) == 2 && B.GetValue(-1, -1) == 3);
            Assert.IsTrue(B.S.NumberOfElements == 3);
            Assert.IsTrue(B.S.NumberOfDimensions == 2);
            //Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);

            B = A[r(1, 1)];
            Assert.IsTrue(B.GetValue(0) == 4);
            Assert.IsTrue(B.Equals(A[1, full]));
            Assert.IsTrue(B.S.NumberOfElements == 3);
            Assert.IsTrue(B.S.NumberOfDimensions == 2);
            //Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);

            B = A[r(1, 1, 1)];
            Assert.IsTrue(B.GetValue(0) == 4);
            Assert.IsTrue(B.Equals(A[1, full]));
            Assert.IsTrue(B.S.NumberOfElements == 3);
            Assert.IsTrue(B.S.NumberOfDimensions == 2);
            //Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);

            B = A[slice(0, 1)];
            Assert.IsTrue(B.GetValue(0) == 1 && B.GetValue(0, 1) == 2 && B.GetValue(-1, -1) == 3);
            Assert.IsTrue(B.S.NumberOfElements == 3);
            Assert.IsTrue(B.S.NumberOfDimensions == 2);
            //Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);

            B = A[slice(0, 1, 1)];
            Assert.IsTrue(B.GetValue(0) == 1 && B.GetValue(0, 1) == 2 && B.GetValue(-1, -1) == 3);
            Assert.IsTrue(B.S.NumberOfElements == 3);
            Assert.IsTrue(B.S.NumberOfDimensions == 2);
            //Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);

            B = A[slice(1, 2)];
            Assert.IsTrue(B.GetValue(0) == 4);
            Assert.IsTrue(B.Equals(A[1, full]));
            Assert.IsTrue(B.S.NumberOfElements == 3);
            Assert.IsTrue(B.S.NumberOfDimensions == 2);
            //Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);

            B = A[slice(1, 2, 1)];
            Assert.IsTrue(B.GetValue(0) == 4);
            Assert.IsTrue(B.Equals(A[1, full]));
            Assert.IsTrue(B.S.NumberOfElements == 3);
            Assert.IsTrue(B.S.NumberOfDimensions == 2);
            //Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);

            B = A[newaxis];
            Assert.IsTrue(B.GetValue(0) == 1);
            Assert.IsTrue(B.GetValue(0, 1) == 4);
            Assert.IsTrue(B.Equals(A)); // singletons are ignored in Equals()
            Assert.IsTrue(B.S.NumberOfElements == 6);
            Assert.IsTrue(B.S.NumberOfDimensions == 3);
            Assert.IsTrue(B.S[0] == 1 && B.S[1] == 2 && B.S[2] == 3);
            //Assert.IsTrue(B.Storage.m_handles == A.Storage.m_handles);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetRange_np_1D_Int_OORFail() {
            Array<uint> A = new uint[] { 1, 2, 3 };
            A = A[3];
 
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetRange_np_1D_Range_OORFail() {
            Array<uint> A = new uint[] { 1, 2, 3 };
            A = A[r(3, 3)];
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetRange_np_1D_Slice_OORFail() {
            Array<uint> A = new uint[] { 1, 2, 3 };
            A = A[slice(3, 3)]; // Note: numpy 3.6. does not signal an error here! 
        }

        [TestMethod]
        public void GetRange_np_2D_DimSpec_Scalar() { 
            // test: full, newaxis, end, -1, 0, ellipsis, r(0,0), r(0,1,0), 
            Array<float> A = 99;

            Array<float> B = A[ellipsis, 0];

            Assert.IsTrue(B.S.NumberOfDimensions == 0);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.GetValue(0) == 99);

            B = A[full, slice(0, 1)];
            Assert.IsTrue(B.S.NumberOfDimensions == 0);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.GetValue(0) == 99);

            B = A[full, slice(0, 1, 1)];
            Assert.IsTrue(B.S.NumberOfDimensions == 0);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.GetValue(0) == 99);

            B = A[full, slice(0, 2, 2)];
            Assert.IsTrue(B.S.NumberOfDimensions == 0);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.GetValue(0) == 99);

            B = A[full, r(0, 0)];
            Assert.IsTrue(B.S.NumberOfDimensions == 0);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.GetValue(0) == 99);

            B = A[full, r(0, 1, 0)];
            Assert.IsTrue(B.S.NumberOfDimensions == 0);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.GetValue(0) == 99);

            try {
                B = A[full, r(0, 2, 1)];
                Assert.Fail("ranges do not allow indices outside of the array dimension range.");
                Assert.IsTrue(B.S.NumberOfDimensions == 0);
                Assert.IsTrue(B.S.NumberOfElements == 1);
                Assert.IsTrue(B.GetValue(0) == 99);
            } catch (IndexOutOfRangeException) { }

            B = A[full, full];
            Assert.IsTrue(B.S.NumberOfDimensions == 0);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.GetValue(0) == 99);

            B = A[0, full];
            Assert.IsTrue(B.S.NumberOfDimensions == 0);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.GetValue(0) == 99);

        } 
        [TestMethod]
        public void GetRange_np_RangedSlice_Neg1() {
            Array<double> A = counter(1.0, 1.0, 4, 3);

            Array<double> C = A[slice(0, -1), full];
            Assert.IsTrue(C.Equals(A[r(0,end-1),full]));
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetRange_np_2DEmptyEndOOR() {

            Array<double> A = new double[,] { { }, { } };  // [2,0]
            Array<double> B = A[1, end];
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetRange_np_1DEmptyEndOOR() {

            Array<double> A = new double[,] { };  // [0]
            Array<double> B = A[end];
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetRange_np_1DEmptyNegOOR() {

            Array<double> A = new double[,] { };  // [0]
            Array<double> B = A[-1];
        }
        #endregion dimspec tests


        #region BaseArray indices

        [TestMethod]
        public void GetRange_np_scalarsAreFancy() {

            Array<double> A = counter<double>(0.0, 1.0, 4, 3, 1, StorageOrders.RowMajor);
            Array<float> I = new float[] { 0, 0 };
            Array<double> B = A[1, full, I];

            Array<double> Res = new double[,] { { 3, 4, 5 }, { 3, 4, 5 } };
            Assert.IsTrue(B.Equals(Res));
            // the scalar caused the adv.index subspace to move to the start (because the subspace was splitted among dim 0 & 2).
            Assert.IsTrue(B.S[0] == 2); 
            Assert.IsTrue(B.S[1] == 3); 
            Assert.IsTrue(B.S.NumberOfDimensions == 2);
        }

        [TestMethod]
        public void GetRange_np_BA_1D_001() {
            Array<uint> A = touint32(counter(1.0, 1.0, 1000));
            Array<int> I = toint32(counter<double>(0.0, 1.0, 1000));

            Assert.IsTrue(A.Equals(A[I])); 
        }

        [TestMethod]
        public void GetRange_np_BA_simpple000() {
            Array<double> A = counter(1.0, 1.0, 4, 3, 2);
            Array<int> I = new int[] { 1, 2 };
            Array<int> I2 = 0; 
            Array<double> B = A[full, I, I2];

            Assert.IsTrue(B.S.NumberOfDimensions == 2);
            Assert.IsTrue(B.S[0] == 4);
            Assert.IsTrue(B.S[1] == 2);
            Assert.IsTrue(B.S[2] == 1); // virtual dimension

            Array<double> Res = new double[,] {
                { 5, 9 },
                { 6, 10 },
                { 7, 11 },
                { 8, 12 }
            };
            Assert.IsTrue(Res.Equals(B));
            Assert.IsTrue(B.S.StorageOrder == StorageOrders.RowMajor);
        }
        [TestMethod]
        public void GetRange_np_BA_non0scalarLastDim() {
            Array<double> A = counter(1.0, 1.0, 4, 3, 2);
            Array<int> I = new int[] { 1, 2 };
            Array<int> I2 = 1;
            Array<double> B = A[full, I, I2];

            Assert.IsTrue(B.S.NumberOfDimensions == 2);
            Assert.IsTrue(B.S[0] == 4);
            Assert.IsTrue(B.S[1] == 2);
            Assert.IsTrue(B.S[2] == 1); // virtual dimension

            Array<double> Res = new double[,] {
                { 17, 21 },
                { 18, 22 },
                { 19, 23 },
                { 20, 24 }
            };
            Assert.IsTrue(Res.Equals(B));
            Assert.IsTrue(B.S.StorageOrder == StorageOrders.RowMajor);
        }
        [ExpectedException(typeof(IndexOutOfRangeException))]
        [TestMethod]
        public void GetRange_np_BA_OORscalarLastDim() {
            Array<double> A = counter(1.0, 1.0, 4, 3, 2);
            Array<int> I = new int[] { 1, 2 };
            Array<int> I2 = 2;
            Array<double> B = A[full, I, I2];
        }
        [ExpectedException(typeof(IndexOutOfRangeException))]
        [TestMethod]
        public void GetRange_np_BA_OORNegScalarLastDim() {
            Array<double> A = counter(1.0, 1.0, 4, 3, 2);
            Array<int> I = new int[] { 1, 2 };
            Array<int> I2 = -3;
            Array<double> B = A[full, I, I2];
        }
        [ExpectedException(typeof(IndexOutOfRangeException))]
        [TestMethod]
        public void GetRange_np_BA_OORscalarForeLastDim() {
            Array<double> A = counter(1.0, 1.0, 4, 3, 2);
            Array<int> I = new int[] { 1, 2 };
            Array<int> I2 = 2;
            Array<double> B = A[full, I2, I];
        }
        [ExpectedException(typeof(IndexOutOfRangeException))]
        [TestMethod]
        public void GetRange_np_BA_OORNegLastDim() {
            Array<double> A = counter(1.0, 1.0, 4, 3, 2);
            Array<int> I = new int[] { 1, -3 };
            Array<int> I2 = 2;
            Array<double> B = A[full, I2, I];
        }
        [ExpectedException(typeof(IndexOutOfRangeException))]
        [TestMethod]
        public void GetRange_np_BA_OORscalarLastDimLiteral() {
            Array<double> A = counter(1.0, 1.0, 4, 3, 2);
            Array<int> I = new int[] { 1, 2 };

            Array<double> B = A[full, I, 2];
        }
        [TestMethod]
        public void GetRange_np_BA_reorderAdvDims_01() {
            Array<double> A = counter(1.0, 1.0, 4, 3, 2, 3);
            Array<int> I = new int[] { 1, 2 };
            Array<double> B = A[full, I, r(0, 0), 0]; // scalar 0 is considered an advanced index & causes the whole advanced index set to be moved to the start of the output dimensions

            Assert.IsTrue(B.S.NumberOfDimensions == 3);
            Assert.IsTrue(B.S[0] == 2); // I
            Assert.IsTrue(B.S[1] == 4);
            Assert.IsTrue(B.S[2] == 1);
            Assert.IsTrue(B.S[3] == 1); // virtual dimension

            Array<double> Res = new double[,] {
                { 5, 6, 7, 8 },
                { 9, 10, 11, 12 }
            };
            Assert.IsTrue(Res.Equals(B));
        }
        [TestMethod]
        public void GetRange_np_BA_reorderAdvDims_03() {
            Array<double> A = counter(1.0, 1.0, 4, 3, 2, 3);
            Array<int> I = new int[] { 1, 2 };
            Array<double> B = A[full, I, r(0, 0), 1]; // scalar 1 is considered an advanced index & causes the whole advanced index set to be moved to the start of the output dimensions

            Assert.IsTrue(B.S.NumberOfDimensions == 3);
            Assert.IsTrue(B.S[0] == 2); // I
            Assert.IsTrue(B.S[1] == 4);
            Assert.IsTrue(B.S[2] == 1);
            Assert.IsTrue(B.S[3] == 1); // virtual dimension

            Array<double> Res = new double[,] {
                { 29, 30, 31, 32 },
                { 33, 34, 35, 36 }
            };
            Assert.IsTrue(Res.Equals(B));
        }
        [TestMethod]
        public void GetRange_np_BA_reorderByNewaxis() {
            // slice,I1,newaxis,I2 -> I1,I2,slice,1
            Array<float> A = tosingle(counter(1.0, 1.0, 4, 3, 2, StorageOrders.RowMajor));
            Array<float> I1 = tosingle(counter<double>(0.0, 1.0, 1, 3, 1)); 
            Array<float> I2 = tosingle(counter<double>(0.0, 1.0, 1, 1, 2));
            Array<float> B = A[full, I1, newaxis, I2];

            Array<float> Res = A.Reshape(4,6).T.Reshape(1,3,2,4,1); 
            
            Assert.IsTrue(B.Equals(Res)); 
        }

        [TestMethod]
        public void GetRange_np_BA_reorderAdvDims_02() {
            Array<double> A = counter(1.0, 1.0, 4, 3, 2, 3, order: StorageOrders.RowMajor);
            Array<int> I1 = new int[,] { { 1, 2 }, { 0, 2 } };
            Array<int> I2 = new int[,] { { 0, 1 } };
            Array<double> B = A[full, I1, r(0, 0), I2]; // I2 is an advanced index & causes the whole advanced index set to be moved to the start of the output dimensions

            Assert.IsTrue(B.S.NumberOfDimensions == 4);
            Assert.IsTrue(B.S[0] == 2); // I1 & I2
            Assert.IsTrue(B.S[1] == 2); // I1 & I2
            Assert.IsTrue(B.S[2] == 4);
            Assert.IsTrue(B.S[3] == 1); 

            Array<double> Res = new double[,,,] {
                {
                    {
                        { 7 }, {25}, {43}, { 61}
                    },
                    {
                        { 14}, {32}, {50}, {68},
                    }
                },
                {
                    {
                        { 1}, {19}, {37}, {55},
                    },
                    {
                        { 14}, {32}, {50}, {68},
                    }
                }
            };
            Assert.IsTrue(Res.Equals(B));
        }
        [TestMethod]
        public void GetRange_np_BA_AdvDimsLast_BCVect() {
            Array<ulong> A = touint64(counter(1.0, 1.0, 4, 3, 2, 3, order: StorageOrders.RowMajor));
            Array<int> I1 = new int[,] { { 1, 0 }, { 0, 1 } };
            Array<int> I2 = new int[,] { { 2, 1 } };
            // r(1,1) - non 0 offset, iteration order (first AFTER reordering)
            Array<ulong> B = A[full, r(1, 1), I1, I2]; // I2 is an advanced index & causes the whole advanced index set to be moved to the start of the output dimensions

            Assert.IsTrue(B.S.NumberOfDimensions == 4);
            Assert.IsTrue(B.S[0] == 4); 
            Assert.IsTrue(B.S[1] == 1); 
            Assert.IsTrue(B.S[2] == 2); // I1 & I2
            Assert.IsTrue(B.S[3] == 2); // I1 & I2 

            Array<ulong> Res = new ulong[] { 12, 8, 9, 11, 30, 26, 27, 29, 48, 44, 45, 47, 66, 62, 63, 65 }; // 48, 66, 9, 27, 45, 63, 8, 26, 44, 62, 11, 29, 47, 65 }; 
            Assert.IsTrue(Res.Reshape(4,1,2,2, order: StorageOrders.RowMajor).Equals(B));
        }
        [TestMethod]
        public void GetRange_np_BA_AdvDimsLast_BC2x2() {
            Array<short> A = toint16(counter(1.0, 1.0, 4, 3, 3, 2, order: StorageOrders.RowMajor));
            Array<int> I1 = new int[,] { { 1, 0 }, { 0, 1 } };
            Array<int> I2 = new int[,] { { 2, 1 } };
            // r(1,1) - non 0 offset, iteration order (first AFTER reordering)
            Array<short> B = A[r(0,end), r(1, -2), I2, I1]; // I2 is an advanced index & causes the whole advanced index set to be moved to the start of the output dimensions

            Assert.IsTrue(B.S.NumberOfDimensions == 4);
            Assert.IsTrue(B.S[0] == 4);
            Assert.IsTrue(B.S[1] == 1);
            Assert.IsTrue(B.S[2] == 2); // I1 & I2
            Assert.IsTrue(B.S[3] == 2); // I1 & I2 

            Array<short> Res = new short[] { 12, 9, 11, 10, 30, 27, 29, 28, 48, 45, 47, 46, 66,63, 65, 64 }; // 48, 66, 9, 27, 45, 63, 8, 26, 44, 62, 11, 29, 47, 65 }; 
            Assert.IsTrue(Res.Reshape(4, 1, 2, 2, order: StorageOrders.RowMajor).Equals(B));
        }

        [TestMethod]
        public void GetRange_np_BA_simple001() {
            Array<double> A = counter(1.0, 1.0, 4, 3);
            Array<uint> I = new uint[] { 0, 1, 2, 3 }; 
            Array<double> B = A[I[full, newaxis],I[newaxis, r(0,-2)]];

            Assert.IsTrue(A.Equals(B)); 
        }

        [TestMethod]
        public void GetRange_np_BA_scalarArray001() {
            Array<double> A = -1;
            Array<int> I = 0; 
            Assert.IsTrue(A[I].Equals((Array<double>)(-1))); 
            Assert.IsTrue(A[I,0].Equals((Array<double>)(-1)));
            Assert.IsTrue(A[I,0,0].Equals((Array<double>)(-1)));
            Assert.IsTrue(A[I,0,0,0].Equals((Array<double>)(-1)));
            Assert.IsTrue(A[I,0,0,0,0].Equals((Array<double>)(-1)));
            Assert.IsTrue(A[I,0,0,0,0,0].Equals((Array<double>)(-1)));
            Assert.IsTrue(A[I,0,0,0,0,0,0].Equals((Array<double>)(-1)));

            Assert.IsTrue(A[I,full].Equals((Array<double>)(-1)));
            Assert.IsTrue(A[full,I].Equals((Array<double>)(-1)));
            Assert.IsTrue(A[-1,I].Equals((Array<double>)(-1)));
            Assert.IsTrue(A[end,I].Equals((Array<double>)(-1)));
            Assert.IsTrue(A[end,I].Equals((Array<double>)(-1)));
        }

        [TestMethod]
        public void GetRange_np_BA_scalarIndex001() {
            Array<double> A = counter(1.0, 1.0, 100, 20, 3, 4);
            Array<int> I = 1;
            Assert.IsTrue(A[I, 0, 0, 0, 0].GetValue(0) == A.GetValue(1, 0, 0, 0, 0));
            Assert.IsTrue(A[0, I, 0, 0, 0].GetValue(0) == A.GetValue(0, 1, 0, 0, 0));
            Assert.IsTrue(A[0, 0, I, 0, 0].GetValue(0) == A.GetValue(0, 0, 1, 0, 0));
            Assert.IsTrue(A[0, 0, 0, I, 0].GetValue(0) == A.GetValue(0, 0, 0, 1, 0));
            Assert.IsTrue(A[0, I, 0, I, 0].GetValue(0) == A.GetValue(0, 1, 0, 1, 0));

            Assert.IsTrue(A[I, 0, 0, 0, 0].S.NumberOfDimensions == 0);
            Assert.IsTrue(A[0, I, 0, 0, 0].S.NumberOfDimensions == 0);
            Assert.IsTrue(A[0, 0, I, 0, 0].S.NumberOfDimensions == 0);
            Assert.IsTrue(A[0, 0, 0, I, 0].S.NumberOfDimensions == 0);
            Assert.IsTrue(A[0, I, 0, I, 0].S.NumberOfDimensions == 0);
        }

        [TestMethod]
        public void GetRange_np_BA_1DIndex001() {
            Array<double> A = counter(1.0, 1.0, 100, 20, 3, 4);
            Array<int> I = new int[] { 1 };
            Assert.IsTrue(A[I, 0, 0, 0, 0].GetValue(0) == A.GetValue(1, 0, 0, 0, 0));
            Assert.IsTrue(A[0, I, 0, 0, 0].GetValue(0) == A.GetValue(0, 1, 0, 0, 0));
            Assert.IsTrue(A[0, 0, I, 0, 0].GetValue(0) == A.GetValue(0, 0, 1, 0, 0));
            Assert.IsTrue(A[0, 0, 0, I, 0].GetValue(0) == A.GetValue(0, 0, 0, 1, 0));
            Assert.IsTrue(A[0, I, 0, I, 0].GetValue(0) == A.GetValue(0, 1, 0, 1, 0));

            Assert.IsTrue(A[I, 0, 0, 0, 0].S.NumberOfDimensions == 1);
            Assert.IsTrue(A[0, I, 0, 0, 0].S.NumberOfDimensions == 1);
            Assert.IsTrue(A[0, 0, I, 0, 0].S.NumberOfDimensions == 1);
            Assert.IsTrue(A[0, 0, 0, I, 0].S.NumberOfDimensions == 1);
            Assert.IsTrue(A[0, I, 0, I, 0].S.NumberOfDimensions == 1);
        }
        [TestMethod]
        public void GetRange_np_ScalarMultiDim001() {
            // test bool indices with multiple dimensions, exceeding the source dimension number
            Array<double> A = counter<double>(0.0, 1.0, 5, 4, StorageOrders.RowMajor);
            Array<double> I = new double[,] {
                { 0, 1 }, { 1, 2 },{ 2, 3 }, { 3, 0 }
            };
            Array<double> B = A[2, I]; // I addresses the 2nd + 3rd dimension -> ArgumentException

            Assert.IsTrue(B.S.IsSameShape(I.S));
            Array<double> Res = new double[,] {
                { 8, 9 }, { 9, 10 }, { 10, 11 }, { 11, 8 }
            };
            Assert.IsTrue(B.Equals(Res));
            /*
             * a = np.arange(1,21).reshape(5,4);
             * I = np.array([[0,1],[1,2],[2,3],[3,0]])
             * a[2, I]; 
             **/
        }

        #endregion

        #region boolean indices

        [TestMethod]
        public void GetRange_np_BoolEmptyIndex001() {
            Logical A = new bool[,] { 
                { true, false, true, true, false }, 
                { false, false, true, false, true }
            };

            Array<uint> I = new uint[] { };

            Logical B = A[I];
            Assert.IsTrue(B.IsEmpty);
            Assert.IsTrue(B.S[0] == 0); 
            Assert.IsTrue(B.S[1] == 5);
            Assert.IsTrue(B.S[2] == 1);
        }
        [TestMethod]
        public void GetRange_np_BoolEmptyBool001() {
            Logical A = new bool[,] {
                { true, false, true, true, false },
                { false, false, true, false, true }
            };

            Array<uint> I = new uint[] { };

            Logical B = A[A[I]];
            Assert.IsTrue(B.IsEmpty);
            Assert.IsTrue(B.S[0] == 0);
            Assert.IsTrue(B.S[1] == 1);
            Assert.IsTrue(B.S[2] == 1);
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetRange_np_EmptyBoolIdxOORErr() {
            Logical A = new bool[,] {
                { },
                {  }
            };

            Array<uint> I = new uint[1] { 0 };

            Logical B = A[1, I];
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetRange_np_EmptyBoolScalarIdxOORErr() {
            Logical A = new bool[,] {
                { },
                {  }
            };
            Array<uint> I = new uint[1] { 1 };

            Logical B = A[I, 0];
        }

        [TestMethod]
        public void GetRange_np_simpleBoolCombined001() {
            /* 
             * a = np.arange(24).reshape(4,3,2)
             * l = np.array([False,True,True]);
             * b = a[1, l, :];
             * >b
             * array([[ 8,  9],
             *        [10, 11]])
             */
            Array<float> A = tosingle(counter<double>(0.0, 1.0, 4, 3, 2, StorageOrders.RowMajor));
            Logical L = new bool[] { false, true, true };
            Array<float> B = A[1, L, full];

            Array<float> Res = new float[,] { { 8, 9 }, { 10, 11 } };

            Assert.IsTrue(B.Equals(Res));
            Assert.IsTrue(B.S.NumberOfDimensions == 2);
            Assert.IsTrue(B.S[0] == 2);
            Assert.IsTrue(B.S[1] == 2); 
        }
        [TestMethod]
        public void GetRange_np_BoolMultiDim001() {

            //tests bool indices with multiple dimensions, combined with slices and scalars.
            Array<fcomplex> A = tofcomplex(counter<double>(0.0, 1.0, 5, 4, 3, 2, StorageOrders.RowMajor));
            Logical L = new bool[,] {
                { true, false, true },
                { false, true, true },
                { false, false, true },
                { true, false, false },
            };
            Array<fcomplex> B = A[2, L, full];
            Array<long> I1 = new long[] { 0, 0, 1, 1, 2, 3 };
            Array<long> I2 = new long[] { 0, 2, 1, 2, 2, 0 };

            Array<fcomplex> Res = A[2, I1, I2, full];
            Assert.IsTrue(B.Equals(Res));

            /* a = np.arange(120).reshape(5,4,3,2);
             * l = np.array([[True,False,True],[False, True, True],[False, False, True],[True,False,False]]);
             * b = a[2, l, :]; 
             */
        }
        [TestMethod]
        public void GetRange_np_BoolMultiDim_AutoEndFull() {

            //tests bool indices with multiple dimensions, combined with slices and scalars.
            Array<fcomplex> A = tofcomplex(counter<double>(0.0, 1.0, 5, 4, 3, 2, StorageOrders.RowMajor));
            Logical L = new bool[,] {
                { true, false, true },
                { false, true, true },
                { false, false, true },
                { true, false, false },
            };
            Array<fcomplex> B = A[2, L]; // + full
            Array<long> I1 = new long[] { 0, 0, 1, 1, 2, 3 };
            Array<long> I2 = new long[] { 0, 2, 1, 2, 2, 0 };

            Array<fcomplex> Res = A[2, I1, I2, full];
            Assert.IsTrue(B.Equals(Res));

            /* a = np.arange(120).reshape(5,4,3,2);
             * l = np.array([[True,False,True],[False, True, True],[False, False, True],[True,False,False]]);
             * b = a[2, l]; 
             */
        }
        [TestMethod]
        public void GetRange_np_BoolMultiDim002() {
            // test bool indices with multiple dimensions, combined with index arrays, non-resorting.

            Array<fcomplex> A = tofcomplex(counter<double>(0.0, 1.0, 5, 4, 3, 2, StorageOrders.RowMajor));
            Logical L = new bool[,] {
                { true, false, true },
                { false, true, true },
                { false, false, true },
                { true, false, false },
            };
            Array<ulong> I1 = new ulong[] { 1000, 0, 1, 0, 1, 1, 1, 6, 7 };  // too large. for base offset test
            Array<fcomplex> B = A[2, L, I1[r(1,6)]];  // tests RetArray input + base offset
            Array<long> RI1 = new long[] { 0, 0, 1, 1, 2, 3 };
            Array<long> RI2 = new long[] { 0, 2, 1, 2, 2, 0 };

            Array<fcomplex> Res = A[2, RI1, RI2, I1[r(1, 6)]];
            Assert.IsTrue(B.Equals(Res));
            Array<double> ResD = new double[] { 48, 53, 56, 59, 65, 67 };
            Assert.IsTrue(B.Equals(tofcomplex(ResD)));

            /* a = np.arange(120).reshape(5,4,3,2);
             * l = np.array([[True,False,True],[False, True, True],[False, False, True],[True,False,False]]);
             * b = a[2,l,[0,1,0,1,1,1]]; 
             * b
             * array([48, 53, 56, 59, 65, 67])
             */
        }
        [TestMethod]
        public void GetRange_np_BoolMultiDim003() {
            // test bool indices with multiple dimensions, combined with index arrays, dimension resorting.
            Array<fcomplex> A = tofcomplex(counter<double>(0.0, 1.0, 5, 4, 3, 2, StorageOrders.RowMajor));
            Logical L = new bool[,] {
                { true, false, true },
                { false, true, true },
                { false, false, true },
                { true, false, false },
            };
            Array<ulong> I1 = new ulong[] { 1000, 0, 1, 0, 1, 1, 1, 6, 7 };  // too large. for base offset test
            Array<fcomplex> B = A[slice(1,3), L, newaxis, I1[r(1, 6)]];  // tests RetArray input + base offset
            Array<long> RI1 = new long[] { 0, 0, 1, 1, 2, 3 };
            Array<long> RI2 = new long[] { 0, 2, 1, 2, 2, 0 };

            Array<fcomplex> Res = A[r(1,2), RI1, RI2, newaxis, I1[r(1, 6)]];
            Assert.IsTrue(B.Equals(Res));
            Array<double> ResD = new double[,,] {
                { { 24 }, { 48 } },
                { { 29 }, { 53 } },
                { { 32 }, { 56 } },
                { { 35 }, { 59 } },
                { { 41 }, { 65 } },
                { { 43 }, { 67 } } };
            Assert.IsTrue(B.Equals(tofcomplex(ResD)));

            /* a = np.arange(120).reshape(5,4,3,2);
             * l = np.array([[True,False,True],[False, True, True],[False, False, True],[True,False,False]]);
             * a[1:3,l,np.newaxis,[0,1,0,1,1,1]]
             * b
             * array([48, 53, 56, 59, 65, 67])
             */
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetRange_np_BoolMultiDim004() {
            // test bool indices with multiple dimensions, exceeding the source dimension number
            Array<double> A = counter(1.0, 1.0, 50, 40);
            Logical L = new bool[,] { { true, true }, { false, true } };
            Array<double> B = A[2, L]; // I addresses the 2nd + 3rd dimension -> ArgumentException
        }
        [TestMethod]
        public void GetRange_np_BoolMultiDim_nonMatchingDimLengthsSmaller() {
            // test bool indices with multiple dimensions, exceeding any source dimension length.
            Array<double> A = counter<double>(0.0, 1.0, 3, 4, 3);
            // bool dimensions smaller
            Logical L1 = new bool[,] {
                {true, false, true, true },
                {false, true, true, true } }; // NumberTrues: 6 ->  I1, I2 are 6x1, all indices within range

            Array < double > B = A[L1]; // implicit A[L,...] iterating over 3rd dimension with ':'

            Array<int> I1 = new int[] { 0, 0, 0, 1, 1, 1 };
            Array<int> I2 = new int[] { 0, 2, 3, 1, 2, 3 };
            Array<double> Res = A[I1, I2, full];

            Assert.IsTrue(B.Equals(Res));

        }
        [TestMethod]
        public void GetRange_np_BoolMultiDim_nonMatchingDimLengths_largerFalse() {
            // test bool indices with multiple dimensions, exceeding any source dimension length.
            Array<double> A = counter<double>(0.0, 1.0, 3, 4, 3);

            // bool dimensions larger, but only false OOR
            Logical L1 = new bool[,] {
                {true, false, true, true, false },
                {false, true, true, true, false },
                {false, true, false, true, false },
                {false, false, false, false, false },
            };
            Array<double> B = A[L1]; // implicit A[L,...] iterating over 3rd dimension with :

            Array<int> I1 = new int[] { 0, 0, 0, 1, 1, 1, 2, 2 };
            Array<int> I2 = new int[] { 0, 2, 3, 1, 2, 3, 1, 3 };
            Array<double> Res = A[I1, I2, full];

            Assert.IsTrue(B.Equals(Res));

        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetRange_np_BoolMultiDim_nonMatchingDimLengths_largerTrues() {
            // test bool indices with multiple dimensions, exceeding any source dimension length.
            Array<double> A = counter<double>(0.0, 1.0, 3, 4, 3);

            // bool dimensions larger, but only false OOR
            Logical L1 = new bool[,] {
                {true, false, true, true, true },  // <- [0, 4] is OOR for dim #0!
                {false, true, true, true, false },
                {false, true, false, true, false },
                {false, false, false, false, false },
            };
            Array<double> B = A[L1]; // implicit A[L,...] iterating over 3rd dimension with :

            //Array<int> I1 = new int[] { 0, 0, 0, 1, 1, 1, 2, 2, 2 };
            //Array<int> I2 = new int[] { 0, 2, 3, 1, 2, 3, 1, 2, 3 };
            //Array<double> Res = A[I1, I2, full];

            //Assert.IsTrue(B.Equals(Res));

        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetRange_np_BoolMultiDim_nonMatchingDimLengths_largerTruesCol() {
            // test bool indices with multiple dimensions, exceeding any source dimension length.
            Array<double> A = counter<double>(0.0, 1.0, 3, 4, 3);

            // bool dimensions larger, but only false OOR
            Logical L1 = new bool[,] {
                {true, false, true, true, false },
                {false, true, true, true, false },
                {false, true, false, true, false },
                {false, true, false, false, false },
            };
            Array<double> B = A[L1]; // implicit A[L,...] iterating over 3rd dimension with :

        }
        [TestMethod]
        public void GetRange_np_BoolMultiDim_2D_BC2_3D() {
            // test bool indices (with multiple dimensions?), broadcasting to larger index space.
            Array<double> A = counter(1.0, 1.0, 5, 5, 4, 2, 3, 4, StorageOrders.RowMajor);
            Logical L = new bool[,] { { true, false, true, true } };
            Array<int> I = new int[] { 0, 1, 2 }; 

            Array<double> B = A[r(2,2), L, 1, I];

            Array<int> I1 = new int[] { 0, 0, 0 };
            Array<int> I2 = new int[] { 0, 2, 3 };
            Array<double> Res = A[r(2,2), I1, I2, 1, I];
            Assert.IsTrue(B.Equals(Res)); 
        }
        [TestMethod]
        public void GetRange_np_BoolMultiDim007() {
            //test bool indices (with multiple dimensions?), repeatedly iterated (testing Reset()).
            Array<double> A = counter<double>(0.0, 1.0, 5, 4, 3);
            Logical L = new bool[,] {
                {true, false, true}, 
                {false, true, true } };

            Array<double> B = A[L]; // implicit A[L,...] iterating over 3rd dimension with :

            Array<int> I1 = new int[] { 0, 0, 1, 1 }; 
            Array<int> I2 = new int[] { 0, 2, 1, 2 };
            Array<double> Res = A[I1, I2, full];

            Assert.IsTrue(B.Equals(Res));
            /*
             * l = np.array([[True, False, True],[False,True, True]])
             **/

        }

        [TestMethod]
        public void GetRange_np_BoolMixed1DIntArr() {
            // np online docu example: 
            // https://docs.scipy.org/doc/numpy/reference/arrays.indexing.html#boolean-array-indexing
            Array<double> A = counter<double>(0.0, 1.0, 4, 3, StorageOrders.RowMajor);
            Logical L = new bool[] { false, true, false, true };
            Array<int> columns = new int[] { 0, 2 };

            Array<double> Res = new double[,] {
                {3, 5 },
                {9, 11 }
            };
            // this corresponds to the 'without np.ix_()' example
            Array<long> Li = find(L); 
            // columns has fewer dims than Li[full,newaxis]. Must handle this via virt. dimension!
            Assert.IsTrue(A[Li[full,newaxis], columns].Equals(Res)); 
        }

        [TestMethod]
        public void GetRange_np_BoolMixedWith3DimIntArr() {
            /*
             *  a = np.arange(60).reshape(5,4,3);
             *  l = np.arange(20).reshape(5,4) % 2 == 0; 
             *  i = np.zeros([1, 2, 10], dtype=np.int) + [[[1], [2]]]; 
             *  b = a[l,i]; 
             *  c = a[l.nonzero()[0], l.nonzero()[1], i]
             *  // b == c !
             *  
             * [[[ 1  7 13 19 25 31 37 43 49 55]
             *   [ 2  8 14 20 26 32 38 44 50 56]]]
             */
            Array<double> A = counter<double>(0.0, 1.0, 5, 4, 3, StorageOrders.RowMajor);
            Logical l = counter<double>(0.0, 1.0, 5, 4, StorageOrders.RowMajor) % 2 == 0;
            Array<int> i = toint32(counter<double>(0.0, 0.0, 1, 2, 10) + counter(1.0, 1.0, 1, 2, 1));

            Array<double> Res = new double[,] {
                {1,  7, 13, 19, 25, 31, 37, 43, 49, 55 },
                {2,  8, 14, 20, 26, 32, 38, 44, 50, 56 }
            };
            // i has more dims than l. Must handle this via virt. dimension!
            Assert.IsTrue(A[l, i].Equals(Res));

        }
        [TestMethod]
        public void GetRange_np_BoolMixedWith4DimIntArrAndDimSpec() {
            /*
             *  a = np.arange(60).reshape(5,4,3);
             *  l = np.arange(20).reshape(5,4) % 2 == 0; 
             *  i = np.zeros([1, 2, 10], dtype=np.int) + [[[1], [2]]]; 
             *  b = a[l,i]; 
             *  c = a[l.nonzero()[0], l.nonzero()[1], i]
             *  // b == c !
             *  
             * [[[ 1  7 13 19 25 31 37 43 49 55]
             *   [ 2  8 14 20 26 32 38 44 50 56]]]
             */
            Array<double> A = counter<double>(0.0, 1.0, 3, 5, 4, 3, StorageOrders.RowMajor);
            Logical l = counter<double>(0.0, 1.0, 5, 4, StorageOrders.RowMajor) % 2 == 0;
            Array<int> i = toint32(counter<double>(0.0, 0.0, 1, 2, 10) + counter(1.0, 1.0, 1, 2, 1));

            Array<double> Res = new double[,] {
                {1,  7, 13, 19, 25, 31, 37, 43, 49, 55 },
                {2,  8, 14, 20, 26, 32, 38, 44, 50, 56 }
            };
            // i has more dims than l. Must handle this via virt. dimension!
            Assert.IsTrue(A[0, l, i].Reshape(1, 2, 10, StorageOrders.ColumnMajor).Equals(Res)); // l addresses 2 dimensions of A! 
            Assert.IsTrue(A[1, l, i].Reshape(1, 2, 10, StorageOrders.ColumnMajor).Equals(Res + 60));
        }
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void GetRange_np_MultidimBoolAndEllipsisNotSupported() {

            Array<double> A = counter<double>(0.0, 1.0, 3, 5, 4, 3, StorageOrders.RowMajor);
            Logical l = counter<double>(0.0, 1.0, 5, 4, StorageOrders.RowMajor) % 2 == 0;
            Array<int> i = toint32(counter<double>(0.0, 0.0, 1, 2, 10) + counter(1.0, 1.0, 1, 2, 1));

            var dummy = A[ellipsis, l, i, 0, 0]; // multidim L + ellipsis -> not supported!
 
        }
        [TestMethod]
        public void GetRange_np_EllipsisAllowedWith1DBool() {
            Array<double> A = counter<double>(0.0, 1.0, 1, 5, 3, 4, StorageOrders.RowMajor);
            Logical L = new bool[] { true, true, false, true, false };
            Array<int> I = new int[] { 0, 1, 3 };

            Array<double> Res = new double[,,] {
           { {0  ,       1    ,      3 },
            {4  ,       5    ,      7 },
            {8  ,       9    ,     11 },
                        },
           { {12  ,      13    ,     15 },
            {16  ,      17    ,     19 },
            {20  ,      21    ,     23 },
                            },
           { {36 ,       37   ,     39 },
            {40 ,       41   ,     43 },
            {44 ,       45   ,     47 },
                        }
            };
            Array<double> B = A[ellipsis, L, full, I[full, newaxis]];  // -> reordering outdims: [3, 3, 1, 3]
            using (Settings.Ensure(()=> Settings.ArrayStyle, ArrayStyles.ILNumericsV4))
                Assert.IsTrue(Res.Equals(B.T.Reshape(3, 3, 3)));
        }
        [TestMethod]
        public void GetRange_np_SingleBool3D() {
            Array<long> A = toint64(counter(1.0, 1.0, 6, 5, 4, StorageOrders.RowMajor));
            Array<long> B = A[A % 4 == 0];
            Assert.IsTrue(B.S.NumberOfDimensions == 1);
            Assert.IsTrue(B.S.NumberOfElements == 30);
            Array<long> Res = toint64(counter<double>(0.0, 4.0, 30)) + 4;
            Assert.IsTrue(B.Equals(Res));

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {
                A.a = toint64(counter(1.0, 1.0, 6, 5, 4, StorageOrders.ColumnMajor));
                Assert.IsTrue(Res.Equals(A[A % 4 == 0])); 
            }
        }
        [TestMethod]
        public void GetRange_np_EllipsisAllowedWith1DBoolScalar() {
            Array<double> A = counter<double>(0.0, 1.0, 1, 5, 3, 4, StorageOrders.RowMajor);
            Logical L = true;
            Array<int> I = new int[] { 0, 1, 3 };

            Array<double> Res = new double[,,] {
           { {0  ,       1    ,      3 },
            {4  ,       5    ,      7 },
            {8  ,       9    ,     11 },
                        },
            };
            Array<double> B = A[ellipsis, L, full, I];  // -> NO reordering outdims: [3, 1, 3]
            Assert.IsTrue(Res.Equals(B.T.Reshape(3, 3)));

        }
        #endregion

        #region string ranges
        [TestMethod]
        public void GetRange_np_StringRange001() {

            Array<double> A = counter(1.0, 1.0, 5, 4, 3);
            Array<double> B = A["0:end", ":1:end", "-3 :1:-1"];
            Assert.IsTrue(A.Equals(B));

            B = A[" : end", ":1 :", ":"];
            Assert.IsTrue(A.Equals(B));

            B = A[" :", ":1: -1"];
            Assert.IsTrue(A.Equals(B));

            B = A[full, "0 :"];
            Assert.IsTrue(A.Equals(B));
        }
        [TestMethod]
        public void GetRange_np_StringRangeStepped() {

            Array<double> A = counter(1.0, 1.0, 5, 4, 3);
            Array<double> B = A["0:2:end", ":2:end", "-3 :2:-1"];
            Assert.IsTrue(A[r(0, 2, end), r(0, 2, end), r(0, 2, end)].Equals(B));
        }
        [TestMethod]
        public void GetRange_np_StringRangeempty() {

            Array<double> A = counter(1.0, 1.0, 5, 4, 3);
            Array<double> B = A[""];
            Array<int> I = new int[] { };
            Assert.IsTrue(A[I].Equals(B));
        }
        [TestMethod]
        public void GetRange_np_StringRangeEmptyAndInt() {

            Array<double> A = counter(1.0, 1.0, 5, 4, 3);
            Array<int> I1 = new int[] { };
            Array<int> I2 = new int[] { 0, 2, 3 };
            Array<double> B = A["", I2];
            Assert.IsTrue(A[r(1, 0), I2].Equals(B));
        }
        [TestMethod]
        public void GetRange_np_StringRangeSingleElement001() {

            Array<double> A = counter(1.0, 1.0, 5, 4, 3);
            Array<double> B = A["1", "1", "2"];
            Assert.IsTrue(A[1, 1, 2].Equals(B));
        }
        [TestMethod]
        public void GetRange_np_StringRangeSingleElement002() {

            Array<double> A = counter(1.0, 1.0, 5, 4, 3);
            Array<double> B = A["1", 1, "2"];
            Assert.IsTrue(A[1, 1, 2].Equals(B));
        }
        [TestMethod]
        public void GetRange_np_StringRangeSingleElement003() {

            Array<double> A = counter(1.0, 1.0, 5, 4, 3);
            Array<int> I = 1;
            Array<double> B = A[I, "1", 2];
            Assert.IsTrue(A[1, 1, 2].Equals(B));
        }
        [TestMethod]
        public void GetRange_np_StringRangeSingleElement004() {

            Array<double> A = counter(1.0, 1.0, 5, 4, 3);
            Array<int> I = 0;
            Array<double> B = A["1", 1, 2, I, I, I, I];
            Assert.IsTrue(A[1, 1, 2].Equals(B));
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetRange_np_StringRangeMultipleErr() {
            Array<double> A = counter(1.0, 1.0, 5, 4, 3);
            Array<double> B = A[":,1"]; 
        }
        #endregion

        #region OOR errors, index errors
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetRange_np_NaNFloatingPointIndexValuesErr() {
            Array<double> A = counter<double>(0.0, 0.001, 5, 4, 3);
            Array<double> NaN = new double[] { 0, double.NaN, 2 }; 
            Array<double> B = A[NaN]; 
        }
        [TestMethod]
        public void GetRange_np_nonIntegerFloatingPointIndexValuesWorking() {
            Array<double> A = counter<double>(0.0, 0.001, 5, 4, 3);
            Array<int> I = toint32(A); 
            Assert.IsTrue(A[A].Equals(A[I]));
        }
        [TestMethod]
        public void GetRange_np_boolDimsOut32viaNewaxis() {
            Array<double> A = 1;
            // 0 dim
            Array<int> I = new int[5, 4, 3];
            Array<double> B = A[newaxis, I, newaxis, 0, newaxis, newaxis, newaxis,
                newaxis, newaxis, newaxis, newaxis, newaxis, newaxis, newaxis, newaxis,
                newaxis, newaxis, newaxis, newaxis, newaxis, newaxis, newaxis, newaxis,
                newaxis, newaxis, newaxis, newaxis, newaxis, newaxis, newaxis, newaxis];
        }
        [TestMethod]
        public void GetRange_np_boolDimsOut32viaVirtDim() {
            Array<double> A = 1;
            // 0 dim
            Array<int> I = new int[5, 4, 3];
            Array<double> B = A[newaxis, I, newaxis, r(0, 0), newaxis, newaxis, newaxis,
                newaxis, newaxis, newaxis, newaxis, newaxis, newaxis, newaxis, newaxis,
                newaxis, newaxis, newaxis, newaxis, newaxis, newaxis, newaxis, newaxis,
                newaxis, newaxis, newaxis, newaxis, newaxis, newaxis, newaxis, newaxis];
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetRange_np_boolDimsTooLongErr() {
            Array<double> A = vector(1.0);
            // 0 dim
            Array<int> I = new int[5, 4, 3];
            Array<double> B = A[r(0, 0), newaxis, I, newaxis, newaxis, newaxis, newaxis,
                newaxis, newaxis, newaxis, newaxis, newaxis, newaxis, newaxis, newaxis,
                newaxis, newaxis, newaxis, newaxis, newaxis, newaxis, newaxis, newaxis,
                newaxis, newaxis, newaxis, newaxis, newaxis, newaxis, newaxis, newaxis];
 
        }

        [TestMethod]
        public void GetRange_np_ScalarEmptyBool() {
            Array<double> A = 1;
            Logical L = new bool[] { };
            Array<double> B = A[L]; 
        }
        #endregion

        #region dimspec indices array
        [TestMethod]
        public void GetRange_np_BAArrayempty() {
            Array<double> A = counter(1.0, 1.0, 4, 3);
            BaseArray[] I = null;
            Array<double> B = A[I];

            // np considers this as ...
            Assert.IsTrue(B.Equals(A));
        }
        [TestMethod]
        public void GetRange_np_RetT_BAempty() {
            Array<double> A = counter(1.0, 1.0, 4, 3);
            BaseArray[] I = null;
            Array<double> B = A.C[I];

            // np considers this as ...
            Assert.IsTrue(B.Equals(A));
        }
        [TestMethod]
        public void GetRange_np_RetT_BA_7Dfull() {
            Array<double> A = counter(1.0, 1.0, 4, 3, 2, 3, 3, 1, 4);
            Array<uint> I0 = new uint[] { 0, 1, 2, 3 };
            Array<uint> I1 = new uint[] { 0, 1, 2 };
            Array<int> I2 = 0; 
            BaseArray[] I = new BaseArray[7] {
                I0[full, newaxis], I1, r(0,end), full, ":", slice(0,1), r(I2,end) 
            };
            Array<double> B = A.C[I];

            // np considers this as ...
            Assert.IsTrue(B.Equals(A));
        }

        [TestMethod]
        public void GetRange_np_emptyDim_full() {

            Array<double> A = zeros<double>(3, dim1: 0);
            Array<double> B = A[0, full];   // should work
            Assert.IsTrue(B.S[0] == 0);
            Assert.IsTrue(B.S[1] == 1);

            Assert.IsTrue(B.S.NumberOfDimensions == 1); 
            Assert.IsTrue(B.S.NumberOfElements == 0);

            Assert.IsTrue(zeros<double>(0, dim1: 0)[full, full].shape.Equals(vector<long>(0, 0)));  
            Assert.IsTrue(zeros<double>(0, dim1: 0, dim2: 3)[full, full, 2].shape.Equals(vector<long>(0, 0)));  
            Assert.IsTrue(zeros<double>(3, dim1: 2, dim2: 0)[1, 1, full].shape.Equals(vector<long>(0)));  
            Assert.IsTrue(zeros<double>(3,1,2,dim3:0)[1, 0, full, full].shape.Equals(vector<long>(2, 0)));
            Assert.IsTrue(zeros<double>(3,1,2,2, dim4: 0)[1, 0, full, 1, full].shape.Equals(vector<long>(2, 0)));
            Assert.IsTrue(zeros<double>(3,1,2,2,0,2)[1, 0, full, 1, full, full].shape.Equals(vector<long>(2, 0, 2)));
            Assert.IsTrue(zeros<double>(3,1,2,2,0,2, dim6: 0)[1, 0, full, 1, full, full, full].shape
                                .Equals(vector<long>(2, 0, 2, 0)));


        }
        #endregion

        #region large, simple indices, all dims
        [TestMethod]
        public void GetRange_np_BA_large1D() {
            Array<complex> A = tocomplex(counter<double>(1.0, 1.0, 10000));
            Array<short> I = toint16(counter<int>(0, 1, 10000));
            Array<complex> B = A[I];

            Assert.IsTrue(B.Equals(A));
        }
        [TestMethod]
        public void GetRange_np_BA_large2D() {
            Array<complex> A = tocomplex(counter(1.0, 1.0, 1000, 10));
            Array<short> I1 = toint16(counter<int>(0, 1, 1000, 1));
            Array<short> I2 = toint16(counter<int>(0, 1, 1, 10));
            Array<complex> B = A[I1, I2];

            Assert.IsTrue(B.Equals(A));
        }
        [TestMethod]
        public void GetRange_np_BA_large3D() {
            Array<complex> A = tocomplex(counter(1.0, 1.0, 100, 10, 10));
            Array<short> I1 = toint16(counter<int>(0, 1, 100, 1, 1));
            Array<short> I2 = toint16(counter<int>(0, 1, 1, 10, 1));
            Array<short> I3 = toint16(counter<int>(0, 1, 1, 1, 10));
            Array<complex> B = A[I1, I2, I3];

            Assert.IsTrue(B.Equals(A));
        }
        [TestMethod]
        public void GetRange_np_BA_large4D() {
            Array<fcomplex> A = tofcomplex(counter(1.0, 1.0, 100, 10, 10, 10));
            Array<short> I1 = toint16(counter<int>(0, 1, 100, 1, 1, 1));
            Array<short> I2 = toint16(counter<int>(0, 1, 1, 10, 1, 1));
            Array<short> I3 = toint16(counter<int>(0, 1, 1, 1, 10, 1));
            Array<short> I4 = toint16(counter<int>(0, 1, 1, 1, 1, 10));
            Array<fcomplex> B = A[I1, I2, I3, I4];

            Assert.IsTrue(B.Equals(A));

            Assert.IsTrue(A.Equals(A[full, I2, I3, I4]));
            //Assert.IsTrue(A.Equals(A[I1, I2, I3, I4]));
            //Assert.IsTrue(A.Equals(A[I1, I2, full, I4]));
            Assert.IsTrue(A.Equals(A[I1, I2, I3, full]));
            Assert.IsTrue(A.Equals(A[I1, I2, full, full]));
            Assert.IsTrue(A.Equals(A[I1, I2, full]));
            Assert.IsTrue(A.Equals(A[I1, I2]));
            Assert.IsTrue(A.Equals(A[I1, full]));
            Assert.IsTrue(A.Equals(A[I1, ellipsis, full]));
            Assert.IsTrue(A.Equals(A[I1]));
        }
        [TestMethod]
        public void GetRange_np_BA_large7D() {
            Array<fcomplex> A = tofcomplex(counter(1.0, 1.0, 10, 10, 10, 10, 10, 2, 2));
            Array<short> I1 = toint16(counter<int>(0, 1, 10, 1, 1, 1, 1, 1, 1));
            Array<short> I2 = toint16(counter<int>(0, 1, 1, 10, 1, 1, 1, 1, 1));
            Array<short> I3 = toint16(counter<int>(0, 1, 1, 1, 10, 1, 1, 1, 1));
            Array<short> I4 = toint16(counter<int>(0, 1, 1, 1, 1, 10, 1, 1, 1));
            Array<short> I5 = toint16(counter<int>(0, 1, 1, 1, 1, 1, 10, 1, 1));
            Array<short> I6 = toint16(counter<int>(0, 1, 1, 1, 1, 1, 1, 2, 1));
            Array<short> I7 = toint16(counter<int>(0, 1, 1, 1, 1, 1, 1, 1, 2));
            Array<fcomplex> B = A[I1, I2, I3, I4, I5, I6, I7];

            Assert.IsTrue(B.Equals(A));

            Assert.IsTrue(A.Equals(A[I1, I2, I3, I4, I5, I6, I7]));

            Assert.IsTrue(A.Equals(A[I1[full, full, full, full, full, full, 0], 
                                     I2[full, full, full, full, full, full, 0], 
                                     I3[full, full, full, full, full, full, 0], 
                                     I4[full, full, full, full, full, full, 0], 
                                     I5[full, full, full, full, full, full, 0], 
                                     I6[full, full, full, full, full, full, 0]]));

            Assert.IsTrue(A.Equals(A[I1[full, full, full, full, full,0,0],
                                     I2[full, full, full, full, full,0,0],
                                     I3[full, full, full, full, full,0,0],
                                     I4[full, full, full, full, full,0,0],
                                     I5[full, full, full, full, full,0,0]]));

            Assert.IsTrue(A.Equals(A[I1[full, full, full, full,0,0,0],
                                     I2[full, full, full, full,0,0,0],
                                     I3[full, full, full, full,0,0,0],
                                     I4[full, full, full, full,0,0,0]]));

            Assert.IsTrue(A.Equals(A[I1[full, full, full,0,0,0,0],
                                     I2[full, full, full,0,0,0,0],
                                     I3[full, full, full,0,0,0,0]]));

            Assert.IsTrue(A.Equals(A[I1[full, full,0,0,0,0,0],
                                     I2[full, full,0,0,0,0,0]]));

            Assert.IsTrue(A.Equals(A[I1[full,0,0,0,0,0,0]]));

            Assert.IsTrue(A.Equals(A[I1[full, full, full, full, full, full, 0], 
                                     I2[full, full, full, full, full, full, 0], 
                                     I3[full, full, full, full, full, full, 0], 
                                     I4[full, full, full, full, full, full, 0], 
                                     I5[full, full, full, full, full, full, 0], 
                                     I6[full, full, full, full, full, full, 0], full]));

            Assert.IsTrue(A.Equals(A[I1[full, full, full, full, full,0,0],
                                     I2[full, full, full, full, full,0,0],
                                     I3[full, full, full, full, full,0,0],
                                     I4[full, full, full, full, full,0,0],
                                     I5[full, full, full, full, full,0,0], full]));

            Assert.IsTrue(A.Equals(A[I1[full, full, full, full,0,0,0],
                                     I2[full, full, full, full,0,0,0],
                                     I3[full, full, full, full,0,0,0],
                                     I4[full, full, full, full,0,0,0], full]));

            Assert.IsTrue(A.Equals(A[I1[full, full, full,0,0,0,0],
                                     I2[full, full, full,0,0,0,0],
                                     I3[full, full, full,0,0,0,0], full]));

            Assert.IsTrue(A.Equals(A[I1[full, full,0,0,0,0,0],
                                     I2[full, full,0,0,0,0,0], full]));

            Assert.IsTrue(A.Equals(A[I1[full,0,0,0,0,0,0], full]));

            Assert.IsTrue(A.Equals(A[full, 
                                     I2[0, full, full, full, full, 0,0], 
                                     I3[0, full, full, full, full, 0,0], 
                                     I4[0, full, full, full, full, 0,0], 
                                     I5[0, full, full, full, full, 0,0], 
                                     full, full]));
            Assert.IsTrue(A.Equals(A[full,
                                     I2[0, full, full, full, 0,0,0],
                                     I3[0, full, full, full, 0,0,0],
                                     I4[0, full, full, full, 0,0,0], 
                                     full, full, full]));

            Assert.IsTrue(A.Equals(A[full,
                                     I2[0, full, full, 0, 0, 0, 0],
                                     I3[0, full, full, 0, 0, 0, 0], 
                                     full, full, full, full]));

            Assert.IsTrue(A.Equals(A[full,
                                     I2[0, full, 0, 0, 0, 0, 0],
                                     full, full, full, full, full]));

            Assert.IsTrue(A.Equals(A[full,
                                     I2[0, full, 0, 0, 0, 0, 0],
                                     full, ellipsis]));

            Assert.IsTrue(A.Equals(A[full,
                                     I2[0, full, 0, 0, 0, 0, 0],
                                     full, full, ellipsis]));

            Assert.IsTrue(A.Equals(A[full,
                                     I2[0, full, 0, 0, 0, 0, 0],
                                     full, full, full, ellipsis]));

            Assert.IsTrue(A.Equals(A[I1[full,0,0,0,0,0,0], full]));

        }
        #endregion

        #region memory management
        [TestMethod]
        public void GetRange_np_memoryManagement01() {
            //ILN(enabled=false)
            Array<uint> A = touint32(counter(1.0, 1.0, 5, 4, 3, 2));
            Array<int> I = new int[] { 0, 1, 2, 3, 4 };
            Array<uint> B = A[I];

            var tempIndex1 = I[I]; // Mööööggg!! plain RetArray!!! Don't do this in real life!
            B = A.C[tempIndex1];

            Assert.IsTrue(B.Equals(A));

            tempIndex1 = I[I, newaxis];
            var tempIndex2 = I[newaxis, r(0, -2)]; // Mööööggg!! plain RetArray!!! Don't do this in real life!
            B = A.C[tempIndex1, tempIndex2];

            Assert.IsTrue(B.Equals(A));

            tempIndex1 = I[I, newaxis, newaxis];
            tempIndex2 = I[newaxis, r(0, -2), newaxis]; // Mööööggg!! plain RetArray!!! Don't do this in real life!
            var tempIndex3 = I[newaxis, newaxis, r(0, -3)]; // Mööööggg!! plain RetArray!!! Don't do this in real life!
            B = A.C[tempIndex1, tempIndex2, tempIndex3];

            Assert.IsTrue(B.Equals(A));

            tempIndex1 = I[I, newaxis, newaxis, newaxis];
            tempIndex2 = I[newaxis, r(0, -2), newaxis, newaxis]; // Mööööggg!! plain RetArray!!! Don't do this in real life!
            tempIndex3 = I[newaxis, newaxis, r(0, -3), newaxis]; // Mööööggg!! plain RetArray!!! Don't do this in real life!
            var tempIndex4 = I[newaxis, newaxis, newaxis, r(0, -4)]; // Mööööggg!! plain RetArray!!! Don't do this in real life!
            B = A.C[tempIndex1, tempIndex2, tempIndex3, tempIndex4];

            Assert.IsTrue(B.Equals(A));

            tempIndex1 = I[I, newaxis, newaxis, newaxis, newaxis];
            tempIndex2 = I[newaxis, r(0, -2), newaxis, newaxis, newaxis]; // Mööööggg!! plain RetArray!!! Don't do this in real life!
            tempIndex3 = I[newaxis, newaxis, r(0, -3), newaxis, newaxis]; // Mööööggg!! plain RetArray!!! Don't do this in real life!
            tempIndex4 = I[newaxis, newaxis, newaxis, r(0, -4), newaxis]; // Mööööggg!! plain RetArray!!! Don't do this in real life!
            var tempIndex5 = I[newaxis, newaxis, newaxis, newaxis, r(0, -5)]; // Mööööggg!! plain RetArray!!! Don't do this in real life!
            B = A.C[tempIndex1, tempIndex2, tempIndex3, tempIndex4, tempIndex5];

            Assert.IsTrue(B.Equals(A));
            //ILN(enabled=true)
        }
        #endregion

        #region scalar tests
        [TestMethod]
        public void GetRange_np_BA_scalarSpecials() {
            Array<float> A = -5.5f;
            Array<float> I = 0; 
            Array<float> B = A[I];

            Assert.IsTrue(B.IsScalar);
            Assert.IsTrue(B.S.NumberOfDimensions == 0);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.GetValue(0) == -5.5f);

            B = A[I, 0];

            Assert.IsTrue(B.IsScalar);
            Assert.IsTrue(B.S.NumberOfDimensions == 0);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.GetValue(0) == -5.5f);

            B = A[0, I, newaxis, 0];
            Assert.IsTrue(B.IsScalar);
            Assert.IsTrue(B.S.NumberOfDimensions == 1);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.GetValue(0) == -5.5f);

            B = A[newaxis];  // -> DimSpec
            Assert.IsTrue(B.IsScalar);
            Assert.IsTrue(B.S.NumberOfDimensions == 1);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.GetValue(0) == -5.5f);

            B = A[ellipsis, newaxis, full]; // -> DimSpec
            Assert.IsTrue(B.IsScalar);
            Assert.IsTrue(B.S.NumberOfDimensions == 1);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.GetValue(0) == -5.5f);

            B = A[ellipsis]; // -> DimSpec
            Assert.IsTrue(B.IsScalar);
            Assert.IsTrue(B.S.NumberOfDimensions == 0);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.GetValue(0) == -5.5f);

            B = A[full];  // -> DimSpec
            Assert.IsTrue(B.IsScalar);
            Assert.IsTrue(B.S.NumberOfDimensions == 0);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.GetValue(0) == -5.5f);

            B = A[ellipsis, newaxis, full, I];
            Assert.IsTrue(B.IsScalar);
            Assert.IsTrue(B.S.NumberOfDimensions == 1);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.GetValue(0) == -5.5f);

            B = A[I, ellipsis, newaxis, full, I];
            Assert.IsTrue(B.IsScalar);
            Assert.IsTrue(B.S.NumberOfDimensions == 1);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.GetValue(0) == -5.5f);

            B = A[I, I, I, I, I];
            Assert.IsTrue(B.IsScalar);
            Assert.IsTrue(B.S.NumberOfDimensions == 0);
            Assert.IsTrue(B.S.NumberOfElements == 1);
            Assert.IsTrue(B.GetValue(0) == -5.5f);

        }
        #endregion

        #region online docu test samples 
        [TestMethod]
        public void CommonIndexers_steppedRangeinLastDimensionEnd() {
            Array<double> A = counter(1.0, 1.0, 4, 3, 2);
            Array<double> B = A["0:2:3", ":end", "1:"]; 	// stepped range in 1st dimension. end -> last index in 2nd dimension
            Array<double> Res = new double[,] {
                { 13, 17, 21 },
                { 15, 19, 23 }
            };
            Assert.IsTrue(Res.Equals(B)); 
        }

        [TestMethod]
        public void CommonIndexers_2ndColOfA() {
            Array<double> A = counter(1.0, 1.0, 4, 3);
            Array<double> C = A[slice(0, 4), 1];  // select the second (index #1) column from A

            Assert.IsTrue(C.Equals(vector<double>(5, 6, 7, 8))); 

            C = A[slice(0, 4, 2), full]; // select the first and third row via 'full' (see below)
            Assert.IsTrue(C.Equals(A["0:2:2",":"])); 
        }
        [TestMethod]
        public void GetRange_np_DocuSingleElementSpecial() {
            Array<double> A = counter(1.0, 1.0, 4, 3);
            Array<double> B = A[2, 1];
            Assert.IsTrue(B.GetValue(0) == 7);

            Assert.IsTrue(A[0, end - 1].GetValue(0) == 5);
            Assert.IsTrue(A[-4, -2].GetValue(0) == 5);
        }

        [TestMethod]
        public void GetRange_np_RangedSliceALL1_3_column() {
            Array<double> A = counter(1.0, 1.0, 4, 3);

            Array<double> C = A[slice(0, 4, 2), full];

            Array<double> R = new double[,] { { 1, 5, 9 }, { 3, 7, 11 } };
            Assert.IsTrue(R.Equals(C));
        }
        [TestMethod]
        public void GetRange_np_RangedSliceALL1_elips() {
            Array<double> A = counter(1.0, 1.0, 4, 3, 2);

            Array<double> B = A[ellipsis, 1];

            Assert.IsTrue(B.Equals(counter(13.0, 1.0, 4, 3)));
        }

        [TestMethod]
        public void GetRange_np_ML_sameIfSingleAdvVector() {
            Array<double> A = counter(1.0, 1.0, 5, 4);
            Array<int> I = new int[] { 1, 2, -3, 3 };

            Array<double> npRes = A[I, full];

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {
                Assert.IsTrue(A[I, full].Equals(npRes)); 
            }

        }
        [TestMethod]
        public void GetRange_np_ML_sameIfSingleAdvVectorReorder() {
            Array<double> A = counter(1.0, 1.0, 5, 4, 3);
            Array<int> I = new int[] { 1, 2, 0, 2 };

            Array<double> npRes = A[1, full, I];

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {
                // results cannot be equal due to dimension resorting in numpy
                Assert.IsTrue(A[1, full, I].Reshape(4, 4).T.Equals(npRes)); 
            }

        }

        [TestMethod]
        public void GetRange_np_ML_sameBoolean() {

Array<int> A = new int[,] {
    {   1,   2,   3,   4 },
    {  -1,  -2,  -3,  -4 },
    {  10,  20,  30,  40 },
    { -11, -22, -33, -44 },
};

Logical L = new bool[] { false, true, false, true };

Array<int> B = A[L, full]; 

        }

        [TestMethod]
        public void GetRange_np_Idx4D_Full_2D_double_1() {

            using (Scope.Enter(ArrayStyles.numpy)) {

                Array<double> B = counter<double>(0.0, 1.0, 15, 3,StorageOrders.ColumnMajor);  // -> [15,3] 
                Array<int> C = counter<int>(0, 1, 2, 3, 1, 2, StorageOrders.ColumnMajor);       // -> [2,3,1,2]

                Array<double> A = B[C, full];                        // -> [2,3,1,2,4]

                Assert.IsTrue(A.shape.Equals(size(2, 3, 1, 2, 3)));

                Array<double> Res = vector<double>(
                     0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 
                    15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 
                    30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41)
                                    .Reshape(2, 3, 1, 2, 3, StorageOrders.ColumnMajor);
                Assert.IsTrue(B[C, full].Equals(Res));

                // make sure the storage order does not affect the result
                B.Storage.EnsureStorageOrder(StorageOrders.RowMajor);
                Assert.IsTrue(B[C, full].Equals(Res));

                C.Storage.EnsureStorageOrder(StorageOrders.RowMajor);
                Assert.IsTrue(B[C, full].Equals(Res));

            }
        }

        #endregion  

    }
}
