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
    public class SetRangeML_paramsBA {

        [TestMethod]
        public void SetRange_ML_ParamsBA_scalars() {
            Array<double> A = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            A.SetRange(-1, ones(1)); Assert.IsTrue((double)A.Subarray(ones(1)) == -1); A.SetRange(99, r(1, 1));
            A.SetRange(-1, ones(1), 0); Assert.IsTrue((double)A.Subarray(ones(1), 0) == -1); A.SetRange(99, r(1, 1), 0);
            A.SetRange(-1, ones(1), 0, 0); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0) == -1); A.SetRange(99, r(1, 1), 0);
            A.SetRange(-1, ones(1), 0, 0, 0); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0) == -1); A.SetRange(99, r(1, 1), 0);
            A.SetRange(-1, ones(1), 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0) == -1); A.SetRange(99, r(1, 1), 0);
            A.SetRange(-1, ones(1), 0, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0) == -1); A.SetRange(99, r(1, 1), 0);
            A.SetRange(-1, ones(1), 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0) == -1); A.SetRange(99, r(1, 1), 0);
            A.SetRange(-1, ones(1), 0, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0, 0) == -1); A.SetRange(99, r(1, 1), 0);
            A.SetRange(-1, ones(1), 0, 0, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0, 0, 0) == -1); A.SetRange(99, r(1, 1), 0);
            A.SetRange(-1, ones(1), 0, 0, 0, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0, 0, 0, 0) == -1); A.SetRange(99, r(1, 1), 0);
            A.SetRange(-1, ones(1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0) == -1); A.SetRange(99, r(1, 1), 0);
            A.SetRange(-1, ones(1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0) == -1); A.SetRange(99, r(1, 1), 0);

        }
        [TestMethod]
        public void SetRange_ML_paramsBA_ellipsis() {
            Array<double> A = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));  
            // returns range
            A.SetRange(-1, ellipsis); Assert.IsTrue(allall(A == -1)); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            A.SetRange(-1, ones(1), ellipsis);
            {
                Assert.IsTrue(A[0, full, full].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[0, full, full]));
                Assert.IsTrue(allall(A[1, full, full] == -1));
                Assert.IsTrue(A[r(2, end), full, full].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[r(2,end), full, full]));

                A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            }
            A.SetRange(-1, ones(1), 0, ellipsis); 
            {
                Assert.IsTrue(A[full, r(1,end), full].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[full, r(1, end), full]));
                Assert.IsTrue(A[0, full, full].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[0, full, full]));
                Assert.IsTrue(allall(A[1, 0, full] == -1));
                Assert.IsTrue(A[r(2,end), full, full].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[r(2,end), full, full]));
                A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            }
            //returns scalar
            A.SetRange(-1, ones(1), 0, 0, ellipsis); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0) == -1); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            A.SetRange(-1, ones(1), 0, 0, 0, ellipsis); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0) == -1); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            A.SetRange(-1, ones(1), 0, 0, 0, 0, ellipsis); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0) == -1); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            A.SetRange(-1, ones(1), 0, 0, 0, 0, 0, ellipsis); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0) == -1); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            A.SetRange(-1, ones(1), 0, 0, 0, 0, 0, 0, ellipsis); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0, 0) == -1); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            A.SetRange(-1, ones(1), 0, 0, 0, 0, 0, 0, 0, ellipsis); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0, 0, 0) == -1); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            A.SetRange(-1, ones(1), 0, 0, 0, 0, 0, 0, 0, 0, ellipsis); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0, 0, 0, 0) == -1); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            A.SetRange(-1, ones(1), 0, 0, 0, 0, 0, 0, 0, 0, 0, ellipsis); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0) == -1); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            A.SetRange(-1, ones(1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, ellipsis); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, ellipsis) == -1);

        }

        [TestMethod]
        public void SetRange_ML_paramsBA_empty() {

            Array<short> A = new short[0,1];
            A.SetRange(-1, vector<long>(2), 1); 

            Assert.IsTrue(A.shape.Equals(size(3, 2)));
            Assert.IsTrue(A.Equals(vector<short>(0,0,0,0,0,-1).Reshape(3,2)));

            A = new short[0, 1];
            A.SetRange(-1, ellipsis, vector<long>(0));
            Assert.IsTrue(A.shape.Equals(size(0, 1)));
            Assert.IsTrue(A.Equals(empty<short>(0, 1)));

            A = new short[0, 1];
            A.SetRange(-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, vector<long>(0));
            Assert.IsTrue(A.shape.Equals(size(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1)));
            Assert.IsTrue(A.Equals(-ones<short>(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1)));

            A = new short[0, 1]; A.SetRange(-1, 0, 0, 0, ellipsis, 0, 0, 0, 0, 0, 0, 0, vector<long>(0));
            Assert.IsTrue(A.shape.Equals(size(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1)));
            Assert.IsTrue(A.Equals(-ones<short>(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1)));

        }

        [TestMethod]
        public void SetRange_ML_paramsLong_scalars() {
            Array<double> A = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));

            A.SetRange(-1L, 0); Assert.IsTrue((double)A.Subarray(0) == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A.Subarray(0) == 1);
            A.SetRange(-1L, 0, 0); Assert.IsTrue((double)A.Subarray(0) == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A.Subarray(0) == 1);
            A.SetRange(-1L, 0, 0, 0); Assert.IsTrue((double)A.Subarray(0) == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A.Subarray(0) == 1);
            A.SetRange(-1L, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(0) == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A.Subarray(0) == 1);
            A.SetRange(-1L, 0, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(0) == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A.Subarray(0) == 1);
            A.SetRange(-1L, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(0) == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A.Subarray(0) == 1);
            A.SetRange(-1L, 0, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(0) == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A.Subarray(0) == 1);
            A.SetRange(-1L, 0, 0, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(0) == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A.Subarray(0) == 1);
            A.SetRange(-1L, 0, 0, 0, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(0) == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A.Subarray(0) == 1);
            A.SetRange(-1L, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(0) == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A.Subarray(0) == 1);
            A.SetRange(-1L, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(0) == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A.Subarray(0) == 1);

        }
        [TestMethod]
        public void SetRange_ML_paramsUInt_scalars() {
            Array<double> A = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));

            A.SetRange(-1u, 0); Assert.IsTrue((double)A.Subarray(0) == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A.Subarray(0) == 1);
            A.SetRange(-1u, 0, 0); Assert.IsTrue((double)A.Subarray(0) == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A.Subarray(0) == 1);
            A.SetRange(-1u, 0, 0, 0); Assert.IsTrue((double)A.Subarray(0) == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A.Subarray(0) == 1);
            A.SetRange(-1u, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(0) == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A.Subarray(0) == 1);
            A.SetRange(-1u, 0, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(0) == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A.Subarray(0) == 1);
            A.SetRange(-1u, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(0) == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A.Subarray(0) == 1);
            A.SetRange(-1u, 0, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(0) == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A.Subarray(0) == 1);
            A.SetRange(-1u, 0, 0, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(0) == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A.Subarray(0) == 1);
            A.SetRange(-1u, 0, 0, 0, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(0) == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A.Subarray(0) == 1);
            A.SetRange(-1u, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(0) == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A.Subarray(0) == 1);
            A.SetRange(-1u, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(0) == -1); A.SetRange(1L, 0); Assert.IsTrue((double)A.Subarray(0) == 1);
        }

        [TestMethod]
        public void SetRange_Non0BaseOffset_BA_ML() {

            Array<double> A = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1), StorageOrders.RowMajor)[2, ellipsis]
                .Reshape(size(4, 3, 1, 1, 1, 1, 1, 1), StorageOrders.RowMajor);

            Assert.IsTrue(A.S.BaseOffset == 24);
            Assert.IsTrue(A.S.NumberOfDimensions == 8);
            Array<double> R = counter<double>(25, 1.0, size(4, 3, 1, 1, 1, 1, 1, 1), StorageOrders.RowMajor);
            Assert.IsTrue(A.Equals(R));

            A[vector(1), 0, 0, ellipsis] = -1;
            R.SetValue(-1, 1, 0);

            Assert.IsTrue(A.Equals(R));
            Assert.IsTrue(A.GetValue(1, 0) == -1);

        }
        [TestMethod]
        public void SetRange_ValNon0BaseOffset_BA_ML() {

            Array<double> A = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1), StorageOrders.RowMajor)[2, ellipsis]
                .Reshape(size(4, 3, 1, 1, 1, 1, 1, 1), StorageOrders.RowMajor);

            Assert.IsTrue(A.S.BaseOffset == 24);
            Assert.IsTrue(A.S.NumberOfDimensions == 8);
            Array<double> R = counter<double>(25, 1.0, size(4, 3, 1, 1, 1, 1, 1, 1), StorageOrders.RowMajor);
            Assert.IsTrue(A.Equals(R));

            Array<double> Val = vector(1.0, 2.0, -3.0)[2];
            Assert.IsTrue(Val.S.BaseOffset == 2 || Val.S.BaseOffset == 0);
            Assert.IsTrue(Val.GetValue(0) == -3.0);

            A[vector(1), 0, 0, ellipsis] = Val;
            R.SetValue(-3, 1, 0);

            Assert.IsTrue(A.Equals(R));
            Assert.IsTrue(A.GetValue(1, 0) == -3);

        }

    }
}
