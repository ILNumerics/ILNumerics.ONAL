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
    public class Subarray_GetRangeNP_paramsBA : ILNumerics.Core.UnitTests.NumpyTestClass {

        [TestMethod]
        public void SubarrayNP_ParamsBA_8() {

            Array<double> A = counter<double>(1.0, 1.0, 5, 4, 3, StorageOrders.ColumnMajor);
            Assert.IsTrue((double)A.Subarray(zeros(1), 0, 0, 0, 0, 0, 0, 0) == 1.0);
            Assert.IsTrue((double)A.Subarray(-ones(1), -1, -1, -1, -1, -1, -1, -1) == 5 * 4 * 3);

            // ellipsis 
            Array<double> B = A.Reshape(1, 1, 1, 1, 1, 5, 4, 3);
            Assert.IsTrue((double)A.Subarray(zeros(1), ellipsis, 1, 0, 0) == 6);
            Assert.IsTrue((double)B.Subarray(zeros(1), ellipsis, 1, 0, 0) == 2);
        }

        [TestMethod]
        public void GetRange_NP_paramsDimSpec_scalars() {

            Array<double> A = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            Assert.IsTrue(A.Subarray(vector(1)).Equals(A[1, full, full, full, full, full, full, full, full]));
            Assert.IsTrue(A.Subarray(vector(1), 0).Equals(A[1, 0, full, full, full, full, full, full, full]));
            Assert.IsTrue(A.Subarray(vector(1), 0, 0).Equals(A[1, 0, 0, full, full, full, full, full, full]));
            Assert.IsTrue(A.Subarray(vector(1), 0, 0, 0).Equals(A[1, 0, 0, 0, full, full, full, full, full]));
            Assert.IsTrue(A.Subarray(vector(1), 0, 0, 0, 0).Equals(A[1, 0, 0, 0, 0, full, full, full, full]));
            Assert.IsTrue(A.Subarray(vector(1), 0, 0, 0, 0, 0, 0).Equals(A[1, 0, 0, 0, 0, 0, 0, full, full]));
            Assert.IsTrue(A.Subarray(vector(1), 0, 0, 0, 0, 0, 0, 0).Equals(A[1, 0, 0, 0, 0, 0, 0, 0, full]));
            Assert.IsTrue(A.Subarray(vector(1), 0, 0, 0, 0, 0, 0, 0, 0).Equals(A[1, 0, 0, 0, 0, 0, 0, 0, 0]));
            Assert.IsTrue(A.Subarray(vector(1), 0, 0, 0, 0, 0, 0, 0, 0, 0).Equals(A[1, 0, 0, 0, 0, 0, 0, 0, 0, 0]));
            Assert.IsTrue(A[vector(1), 0, 0, 0, 0, 0, 0, 0, 0, 0].shape.Equals(size(1)));
            Assert.IsTrue(A.Subarray(vector(1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0).Equals(A.GetValue(1)));
            Assert.IsTrue(A.Subarray(vector(1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0).Equals(A.GetValue(1)));
            Assert.IsTrue(A.Subarray(vector(1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0).Equals(A.GetValue(1)));

            Assert.IsTrue(A[1, full, full, full, full, full, full, full, full].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[1]));
            Assert.IsTrue(A[1, 0, full, full, full, full, full, full, full].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[1, 0]));
            Assert.IsTrue(A[1, 0, 0, full, full, full, full, full, full].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[1, 0, 0]));
            Assert.IsTrue(A[1, 0, 0, 0, full, full, full, full, full].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[1, 0, 0, 0]));
            Assert.IsTrue(A[1, 0, 0, 0, 0, full, full, full, full].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[1, 0, 0, 0, 0]));
            Assert.IsTrue(A[1, 0, 0, 0, 0, 0, 0, full, full].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[1, 0, 0, 0, 0, 0, 0]));
            Assert.IsTrue(A[1, 0, 0, 0, 0, 0, 0, 0, full].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[1, 0, 0, 0, 0, 0, 0, 0, full]));
            Assert.IsTrue(A[1, 0, 0, 0, 0, 0, 0, 0, 0].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[1, 0, 0, 0, 0, 0, 0, 0, 0, 0]));
            Assert.IsTrue(A[1, 0, 0, 0, 0, 0, 0, 0, 0, 0].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[1, 0, 0, 0, 0, 0, 0, 0, 0]));
            Assert.IsTrue(A[vector(1), 0, 0, 0, 0, 0, 0, 0, 0, 0].shape.Equals(size(1)));
            Assert.IsTrue(A[vector(1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0].shape.Equals(size(1)));
            Assert.IsTrue(A[vector(1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0].GetValue(0) == 2.0);
            Assert.IsTrue(A[vector(1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0].shape.Equals(size(1)));
            Assert.IsTrue(A[vector(1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0].shape.Equals(size(1)));

            Assert.IsTrue(A[vector(1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0].Equals(A.GetValue(1)));
            Assert.IsTrue(A.Subarray(vector(1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0).Equals(A.GetValue(1)));
        }

        [TestMethod]
        public void GetRange_NP_paramsBA_ellipsis() {
            Array<double> A = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            Assert.IsTrue(A.Subarray(ellipsis).Equals(A));
            Assert.IsTrue(A.Subarray(vector(1),ellipsis).Equals(A[1,full,full].Reshape(1,4,3,1,1,1,1,1,1)));
            Assert.IsTrue(A.Subarray(vector(1),0,ellipsis).Equals(A[1,0,full].Reshape(1,1,3,1,1,1,1,1,1)));

            Assert.IsTrue((double)A.Subarray(vector(1),0,0,ellipsis) == 2.0);
            Assert.IsTrue(A.Subarray(vector(1),0,0,ellipsis).shape.Equals(size(1,1,1,1,1,1,1)));

            Assert.IsTrue((double)A.Subarray(vector(1), 0, 0, 0, ellipsis) == 2.0);
            Assert.IsTrue((double)A.Subarray(vector(1), 0, 0, 0, 0, ellipsis) == 2.0);
            Assert.IsTrue((double)A.Subarray(vector(1), 0, 0, 0, 0, 0, ellipsis) == 2.0);
            Assert.IsTrue((double)A.Subarray(vector(1), 0, 0, 0, 0, 0, 0, ellipsis) == 2.0);
            Assert.IsTrue((double)A.Subarray(vector(1), 0, 0, 0, 0, 0, 0, 0, ellipsis) == 2.0); // ellipsis being erased. 
            Assert.IsTrue((double)A.Subarray(vector(1), 0, 0, 0, 0, 0, 0, 0, 0, ellipsis) == 2.0); // erased 
            Assert.IsTrue((double)A.Subarray(vector(1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, ellipsis) == 2.0); // erased 
            Assert.IsTrue(A.Subarray(vector(1), r(0, 0), r(0, 0), r(0, 0), r(0, 0), r(0, 0), r(0, 0), r(0, 0), r(0, 0), r(0, 0), ellipsis, ellipsis, ellipsis).S.NumberOfDimensions == 9); // 1 virt. dim addressed! -> no out dim for it. 
            Assert.IsTrue(A.Subarray(vector(1), r(0, 0), r(0, 0), r(0, 0), r(0, 0), r(0, 0), r(0, 0), r(0, 0),
                r(0, 0), r(0, 0), r(0, 0), r(0, 0), r(0, 0), r(0, 0), r(0, 0), r(0, 0),
                r(0, 0), r(0, 0), r(0, 0), r(0, 0), r(0, 0), r(0, 0), r(0, 0), r(0, 0),
                r(0, 0), r(0, 0), r(0, 0), r(0, 0), r(0, 0), r(0, 0), r(0, 0), r(0, 0)).S.NumberOfDimensions == 9); // erased all vir. dims

            Assert.IsTrue(A[newaxis, newaxis, newaxis, newaxis, newaxis, newaxis, newaxis, newaxis, newaxis, newaxis, newaxis, newaxis, newaxis, newaxis, newaxis, newaxis, newaxis, newaxis, newaxis, newaxis, newaxis, newaxis, r(0, 0), newaxis]
                .shape.Equals(size(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 3, 1, 1, 1, 1, 1, 1)));
        }

        [TestMethod]
        public void GetRange_NP_paramsBA_empty() {

            Array<short> A = new short[0,1];
            Assert.IsTrue(A.shape.Equals(size(0, 1)));
            Assert.IsTrue(A.Subarray(ellipsis, full).shape.Equals(size(0, 1))); 
            Assert.IsTrue(A.Subarray(ellipsis, full, ellipsis).shape.Equals(size(0, 1)));
            Assert.IsTrue(A.Subarray(full, ellipsis, ellipsis).shape.Equals(size(0, 1)));
            Assert.IsTrue(A.Subarray(ellipsis, full, ellipsis, ellipsis, ellipsis, ellipsis, ellipsis).shape.Equals(size(0, 1)));
            Assert.IsTrue(A.Subarray(ellipsis, full, ellipsis, ellipsis, ellipsis, ellipsis, ellipsis).shape.Equals(size(0, 1)));
            Assert.IsTrue(A.Subarray(ellipsis, full, ellipsis, ellipsis, ellipsis, full, ellipsis).shape.Equals(size(0, 1)));
            Assert.IsTrue(A.Subarray(ellipsis, full, ellipsis, ellipsis, full, full, ellipsis).shape.Equals(size(0, 1)));
            Assert.IsTrue(A.Subarray(ellipsis, full, ellipsis, ellipsis, full, full, ellipsis, 0, ellipsis).shape.Equals(size(0, 1)));
        }

        [TestMethod]
        public void GetRange_NP_paramsLong_scalars() {
            Array<double> A = counter<double>(1.0, 1.0, size(5, 4, 3));
            Assert.IsTrue((double)A.Subarray(-1L, -1, -1) == 5 * 4 * 3);
            Assert.IsTrue((double)A.Subarray(1L, 0, 0) == 2.0);
            Assert.IsTrue((double)A.Subarray(1L, 0, 0, 0) == 2.0);
            Assert.IsTrue((double)A.Subarray(1L, 0, 0, 0, 0) == 2.0);
            Assert.IsTrue((double)A.Subarray(1L, 0, 0, 0, 0, 0) == 2.0);
            Assert.IsTrue((double)A.Subarray(1L, 0, 0, 0, 0, 0, 0) == 2.0);
            Assert.IsTrue((double)A.Subarray(1L, 0, 0, 0, 0, 0, 0, 0) == 2.0);
            Assert.IsTrue((double)A.Subarray(1L, 0, 0, 0, 0, 0, 0, 0, 0) == 2.0);
            Assert.IsTrue((double)A.Subarray(1L, 0, 0, 0, 0, 0, 0, 0, 0, 0) == 2.0);
            Assert.IsTrue((double)A.Subarray(1L, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0) == 2.0);
            Assert.IsTrue((double)A.Subarray(1L, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0) == 2.0);
        }

        [TestMethod]
        public void GetRange_NP_paramsUInt_scalars() {
            Array<double> A = counter<double>(1.0, 1.0, size(5, 4, 3));
            Assert.IsTrue((double)A.Subarray(1u, 0, 0) == 2.0);
            Assert.IsTrue((double)A.Subarray(1u, 0, 0, 0) == 2.0);
            Assert.IsTrue((double)A.Subarray(1u, 0, 0, 0, 0) == 2.0);
            Assert.IsTrue((double)A.Subarray(1u, 0, 0, 0, 0, 0) == 2.0);
            Assert.IsTrue((double)A.Subarray(1u, 0, 0, 0, 0, 0, 0) == 2.0);
            Assert.IsTrue((double)A.Subarray(1u, 0, 0, 0, 0, 0, 0, 0) == 2.0);
            Assert.IsTrue((double)A.Subarray(1u, 0, 0, 0, 0, 0, 0, 0, 0) == 2.0);
            Assert.IsTrue((double)A.Subarray(1u, 0, 0, 0, 0, 0, 0, 0, 0, 0) == 2.0);
            Assert.IsTrue((double)A.Subarray(1u, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0) == 2.0);
            Assert.IsTrue((double)A.Subarray(1u, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0) == 2.0);
        }
    }
}