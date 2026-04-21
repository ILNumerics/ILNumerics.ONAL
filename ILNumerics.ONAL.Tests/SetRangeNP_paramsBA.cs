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
using ILNumerics;
using static ILNumerics.Globals;
using static ILNumerics.ILMath;

namespace ILNumerics.Core.Tests {
    [TestClass]
    public class SetRangeNP_paramsBA : ILNumerics.Core.UnitTests.NumpyTestClass {

        [TestMethod]
        public void SetRange_NP_ParamsBA_scalars() {
            Array<double> A = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            A[ vector(1)] = -1; Assert.IsTrue((double)A.Subarray(vector(1), 0, 0) == -1); A[ r(1, 1)] = 99;
            A[ vector(1), 0] = -1; Assert.IsTrue((double)A.Subarray(vector(1), 0, 0) == -1); A[ r(1, 1), 0] = 99;
            A[ vector(1), 0, 0] = -1; Assert.IsTrue((double)A.Subarray(vector(1), 0, 0) == -1); A[ r(1, 1), 0] = 99;
            A[ vector(1), 0, 0, 0] = -1; Assert.IsTrue((double)A.Subarray(vector(1), 0, 0, 0) == -1); A[ r(1, 1), 0] = 99;
            A[ vector(1), 0, 0, 0, 0] = -1; Assert.IsTrue((double)A.Subarray(vector(1), 0, 0, 0, 0) == -1); A[ r(1, 1), 0] = 99;
            A[ vector(1), 0, 0, 0, 0, 0] = -1; Assert.IsTrue((double)A.Subarray(vector(1), 0, 0, 0, 0, 0) == -1); A[ r(1, 1), 0] = 99;
            A[ vector(1), 0, 0, 0, 0, 0, 0] = -1; Assert.IsTrue((double)A.Subarray(vector(1), 0, 0, 0, 0, 0, 0) == -1); A[ r(1, 1), 0] = 99;
            A[ vector(1), 0, 0, 0, 0, 0, 0, 0] = -1; Assert.IsTrue((double)A.Subarray(vector(1), 0, 0, 0, 0, 0, 0, 0) == -1); A[ r(1, 1), 0] = 99;
            A[ vector(1), 0, 0, 0, 0, 0, 0, 0, 0] = -1; Assert.IsTrue((double)A.Subarray(vector(1), 0, 0, 0, 0, 0, 0, 0, 0) == -1); A[ r(1, 1), 0] = 99;
            A[ vector(1), 0, 0, 0, 0, 0, 0, 0, 0, 0] = -1; Assert.IsTrue((double)A.Subarray(vector(1), 0, 0, 0, 0, 0, 0, 0, 0, 0) == -1); A[ r(1, 1), 0] = 99;
            A[ vector(1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0] = -1; Assert.IsTrue((double)A.Subarray(vector(1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0) == -1); A[ r(1, 1), 0] = 99;
            A[ vector(1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0] = -1; Assert.IsTrue((double)A.Subarray(vector(1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0) == -1); A[ r(1, 1), 0] = 99;

        }

        [TestMethod]
        public void SetRange_ML_paramsBA_ellipsis() {
            Array<double> A = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));  
            // returns range
            A[ ellipsis] = -1; Assert.IsTrue(allall(A == -1)); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            A[ ones(1), ellipsis] = -1;
            {
                Assert.IsTrue(A[0, full, full].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[0, full, full]));
                Assert.IsTrue(allall(A[1, full, full] == -1));
                Assert.IsTrue(A[r(2, end), full, full].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[r(2,end), full, full]));

                A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            }
            A[ ones(1), 0, ellipsis] = -1; 
            {
                Assert.IsTrue(A[full, r(1,end), full].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[full, r(1, end), full]));
                Assert.IsTrue(A[0, full, full].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[0, full, full]));
                Assert.IsTrue(allall(A[1, 0, full] == -1));
                Assert.IsTrue(A[r(2,end), full, full].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[r(2,end), full, full]));
                A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            }
            //returns scalar
            A[ ones(1), 0, 0, ellipsis] = -1; Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0) == -1); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            A[ ones(1), 0, 0, 0, ellipsis] = -1; Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0) == -1); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            A[ ones(1), 0, 0, 0, 0, ellipsis] = -1; Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0) == -1); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            A[ ones(1), 0, 0, 0, 0, 0, ellipsis] = -1; Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0) == -1); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            A[ ones(1), 0, 0, 0, 0, 0, 0, ellipsis] = -1; Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0, 0) == -1); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            A[ ones(1), 0, 0, 0, 0, 0, 0, 0, ellipsis] = -1; Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0, 0, 0) == -1); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            A[ ones(1), 0, 0, 0, 0, 0, 0, 0, 0, ellipsis] = -1; Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0, 0, 0, 0) == -1); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            A[ ones(1), 0, 0, 0, 0, 0, 0, 0, 0, 0, ellipsis] = -1; Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0) == -1); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            A[ ones(1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, ellipsis] = -1; Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, ellipsis) == -1);

        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SetRange_ML_paramsBA_empty() {

            Array<short> A = new short[0,1];
            A.SetRange(-1, vector<long>(2), 1);
 
        }

        [TestMethod]
        public void SetRange_ML_paramsLong_scalars() {
            Array<double> A = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));

            A.SetRange(-1L, 0); Assert.IsTrue((double)A[0,0,0] == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A[0,0,0] == 1);
            A.SetRange(-1L, 0, 0); Assert.IsTrue((double)A[0,0,0] == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A[0,0,0] == 1);
            A.SetRange(-1L, 0, 0, 0); Assert.IsTrue((double)A[0,0,0] == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A[0,0,0] == 1);
            A.SetRange(-1L, 0, 0, 0, 0); Assert.IsTrue((double)A[0,0,0] == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A[0,0,0] == 1);
            A.SetRange(-1L, 0, 0, 0, 0, 0); Assert.IsTrue((double)A[0,0,0] == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A[0,0,0] == 1);
            A.SetRange(-1L, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A[0,0,0] == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A[0,0,0] == 1);
            A.SetRange(-1L, 0, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A[0,0,0] == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A[0,0,0] == 1);
            A.SetRange(-1L, 0, 0, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A[0,0,0] == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A[0,0,0] == 1);
            A.SetRange(-1L, 0, 0, 0, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A[0,0,0] == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A[0,0,0] == 1);
            A.SetRange(-1L, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A[0,0,0] == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A[0,0,0] == 1);
            A.SetRange(-1L, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A[0,0,0] == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A[0,0,0] == 1);

        }
        [TestMethod]
        public void SetRange_ML_paramsUInt_scalars() {
            Array<double> A = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));

            A.SetRange(-1u, 0); Assert.IsTrue((double)A[0,0,0] == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A[0,0,0] == 1);
            A.SetRange(-1u, 0, 0); Assert.IsTrue((double)A[0, 0, 0] == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A[0,0,0] == 1);
            A.SetRange(-1u, 0, 0, 0); Assert.IsTrue((double)A[0, 0, 0] == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A[0,0,0] == 1);
            A.SetRange(-1u, 0, 0, 0, 0); Assert.IsTrue((double)A[0, 0, 0] == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A[0,0,0] == 1);
            A.SetRange(-1u, 0, 0, 0, 0, 0); Assert.IsTrue((double)A[0,0,0] == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A[0,0,0] == 1);
            A.SetRange(-1u, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A[0,0,0] == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A[0,0,0] == 1);
            A.SetRange(-1u, 0, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A[0,0,0] == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A[0,0,0] == 1);
            A.SetRange(-1u, 0, 0, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A[0,0,0] == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A[0,0,0] == 1);
            A.SetRange(-1u, 0, 0, 0, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A[0,0,0] == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A[0,0,0] == 1);
            A.SetRange(-1u, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A[0,0,0] == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A[0,0,0] == 1);
            A.SetRange(-1u, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A[0,0,0] == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A[0,0,0] == 1);
        }

        [TestMethod]
        public void SetRange_Non0BaseOffset_BA_NP() {
            //ILN(enabled=false)
            Array<double> A = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1), StorageOrders.RowMajor)[2];
            //ILN(enabled=true)
            Assert.IsTrue(A.S.BaseOffset == 24);
            Assert.IsTrue(A.S.NumberOfDimensions == 8);
            Array<double> R = counter<double>(25, 1.0, size(4, 3, 1, 1, 1, 1, 1, 1), StorageOrders.RowMajor);
            Assert.IsTrue(A.Equals(R));

            A[vector(1), 0, 0, ellipsis] = -1;
            //ILN(enabled=false)
            R.SetValue(-1, 1, 0);
            //ILN(enabled=true)

            Assert.IsTrue(A.Equals(R));
            Assert.IsTrue(A.GetValue(1, 0) == -1);

        }
        [TestMethod]
        public void SetRange_ValNon0BaseOffset_BA_NP() {

            //ILN(enabled=false)
            Array<double> A = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1), StorageOrders.RowMajor)[2];
            //ILN(enabled=true)

            Assert.IsTrue(A.S.BaseOffset == 24);
            Assert.IsTrue(A.S.NumberOfDimensions == 8);
            Array<double> R = counter<double>(25, 1.0, size(4, 3, 1, 1, 1, 1, 1, 1), StorageOrders.RowMajor);
            Assert.IsTrue(A.Equals(R));

            //ILN(enabled=false)
            Array<double> Val = vector(1.0, 2.0, -3.0)[2];
            //ILN(enabled=true)
            Assert.IsTrue(Val.S.BaseOffset == 2);
            Assert.IsTrue(Val.GetValue(0) == -3.0);

            Array<double> Val2 = vector(1.0, 2.0, -3.0)[2];
            // Assert.IsTrue(Val2.S.BaseOffset == 0);  // when acc: commonly 0 baseoffset
            Assert.IsTrue(Val2.GetValue(0) == -3.0);

            A[vector(1), 0, 0, ellipsis] = Val;
            R.SetValue(-3, 1, 0);

            Assert.IsTrue(A.Equals(R));
            Assert.IsTrue(A.GetValue(1, 0) == -3);

        }

    }
}
