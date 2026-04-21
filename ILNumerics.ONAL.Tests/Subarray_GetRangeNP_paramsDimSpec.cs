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
    public class Subarray_GetRangeNP_paramsDimSpec : ILNumerics.Core.UnitTests.NumpyTestClass {


        [TestMethod]
        public void GetRange_NP_paramsDimSpec_scalars() {
            Array<double> A = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            Assert.IsTrue(A.Subarray(r(1, 1)).Equals(A[1, full, full, full, full, full, full, full, full]));
            Assert.IsTrue(A.Subarray(r(1, 1),0).Equals(A[1, 0, full, full, full, full, full, full, full]));
            Assert.IsTrue(A.Subarray(r(1, 1),0,0).Equals(A[1, 0, 0, full, full, full, full, full, full]));
            Assert.IsTrue(A.Subarray(r(1, 1),0,0,0).Equals(A[1, 0,0,0, full, full, full, full, full]));
            Assert.IsTrue(A.Subarray(r(1, 1),0,0,0,0).Equals(A[1, 0, 0, 0, 0, full, full, full, full]));
            Assert.IsTrue(A.Subarray(r(1, 1),0,0,0,0,0,0).Equals(A[1, 0, 0, 0, 0, 0, 0, full, full]));
            Assert.IsTrue(A.Subarray(r(1, 1),0,0,0,0,0,0,0).Equals(A[1, 0, 0, 0, 0, 0, 0, 0, full]));
            Assert.IsTrue(A.Subarray(r(1, 1),0,0,0,0,0,0,0,0).Equals(A[1, 0, 0, 0, 0, 0, 0, 0, 0]));
            Assert.IsTrue(A.Subarray(r(1, 1),0,0,0,0,0,0,0,0,0).Equals(A[1, 0, 0, 0, 0, 0, 0, 0, 0, 0]));
            Assert.IsTrue(A[r(1,1), 0, 0, 0, 0, 0, 0, 0, 0, 0].shape.Equals(size(1))); 
            Assert.IsTrue(A.Subarray(r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0).Equals(A.GetValue(1)));
            Assert.IsTrue(A.Subarray(r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0).Equals(A.GetValue(1)));
            Assert.IsTrue(A.Subarray(r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0).Equals(A.GetValue(1)));

            Assert.IsTrue(A[1, full, full, full, full, full, full, full, full].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[1]));
            Assert.IsTrue(A[1, 0, full, full, full, full, full, full, full].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[1, 0]));
            Assert.IsTrue(A[1, 0, 0, full, full, full, full, full, full].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[1, 0, 0]));
            Assert.IsTrue(A[1, 0, 0, 0, full, full, full, full, full].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[1, 0, 0, 0]));
            Assert.IsTrue(A[1, 0, 0, 0, 0, full, full, full, full].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[1, 0, 0, 0, 0]));
            Assert.IsTrue(A[1, 0, 0, 0, 0, 0, 0, full, full].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[1, 0, 0, 0, 0, 0, 0]));
            Assert.IsTrue(A[1, 0, 0, 0, 0, 0, 0, 0, full].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[1, 0, 0, 0, 0, 0, 0, 0, full]));
            Assert.IsTrue(A[1, 0, 0, 0, 0, 0, 0, 0, 0].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[1, 0, 0, 0, 0, 0, 0, 0, 0, 0]));
            Assert.IsTrue(A[1, 0, 0, 0, 0, 0, 0, 0, 0, 0].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[1, 0, 0, 0, 0, 0, 0, 0, 0]));
            Assert.IsTrue(A[r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, 0].shape.Equals(size(1)));
            Assert.IsTrue(A[r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0].shape.Equals(size(1)));
            Assert.IsTrue(A[r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0].GetValue(0) == 2.0);
            Assert.IsTrue(A[r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0].shape.Equals(size(1)));
            Assert.IsTrue(A[r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0].shape.Equals(size(1)));

            Assert.IsTrue(A[r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0].Equals(A.GetValue(1)));
            Assert.IsTrue(A.Subarray(r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0).Equals(A.GetValue(1)));

        }
        [TestMethod]
        public void GetRange_NP_paramsDimSpec_ellipsis() {
            Array<double> A = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            Assert.IsTrue(A.Subarray((BaseArray)ellipsis).Equals(A));
            Assert.IsTrue(A.Subarray(r(1, 1), ellipsis).Equals(A[1, full, full].Reshape(1, 4, 3, 1, 1, 1, 1, 1, 1)));
            Assert.IsTrue(A.Subarray(r(1, 1), 0, ellipsis).Equals(A[1, 0, full].Reshape(1, 1, 3, 1, 1, 1, 1, 1, 1)));

            Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, ellipsis) == 2.0);
            Assert.IsTrue(A.Subarray(r(1, 1), 0, 0, ellipsis).shape.Equals(size(1, 1, 1, 1, 1, 1, 1)));

            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, ellipsis) == 2.0);
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, 0, ellipsis) == 2.0);
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, 0, 0, ellipsis) == 2.0);
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, 0, 0, 0, ellipsis) == 2.0);
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, 0, 0, 0, 0, ellipsis) == 2.0); // ellipsis being erased. 
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, ellipsis) == 2.0); // erased 
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, ellipsis) == 2.0); // erased 
            Assert.IsTrue(A.Subarray(r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, ellipsis).S.NumberOfDimensions == 1); // erased 

        }
        [TestMethod]
        public void GetRange_NP_paramsDimSpec_ellipsisMultiple() {

            Array<double> A = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            Assert.IsTrue(A.Subarray((BaseArray)ellipsis).Equals(A));
            Assert.IsTrue(A.Subarray(r(1, 1), ellipsis).Equals(A[1, full, full].Reshape(1, 4, 3, 1, 1, 1, 1, 1, 1)));
            Assert.IsTrue(A.Subarray(r(1, 1), 0, ellipsis).Equals(A[1, 0, full].Reshape(1, 1, 3, 1, 1, 1, 1, 1, 1)));

            Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, ellipsis) == 2.0);
            Assert.IsTrue(A.Subarray(r(1, 1), 0, 0, ellipsis).shape.Equals(size(1, 1, 1, 1, 1, 1, 1)));

            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, ellipsis) == 2.0);
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, 0, ellipsis) == 2.0);
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, 0, 0, ellipsis) == 2.0);
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, 0, 0, 0, ellipsis) == 2.0);
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, 0, 0, 0, 0, ellipsis) == 2.0); // ellipsis being erased. 
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, ellipsis) == 2.0); // erased 
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, ellipsis, 0, ellipsis, 0, ellipsis, ellipsis) == 2.0); // erased 
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, ellipsis) == 2.0); // erased 
            Assert.IsTrue(A.Subarray(r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, ellipsis, ellipsis, ellipsis, ellipsis, ellipsis, ellipsis, ellipsis, ellipsis, ellipsis, ellipsis, ellipsis, ellipsis, ellipsis).S.NumberOfDimensions == 1); // erased 

        }

        [TestMethod]
        public void GetRange_np_paramDimSpecEllipsisNewaxis() {

            Array<double> A = counter<double>(1.0, 1.0, size(5, 4, 3, 2, 1, 2, 2, 2));

            Assert.IsTrue(A[newaxis, ellipsis].Equals(A[newaxis, full, full, full, full, full, full, full, full]));
            Assert.IsTrue(A[ellipsis, newaxis].Equals(A[full, full, full, full, full, full, full, full, newaxis]));
            Assert.IsTrue(A[newaxis, newaxis].Equals(A[newaxis, newaxis, full, full, full, full, full, full, full, full]));
            Assert.IsTrue(A[ellipsis, ellipsis].Equals(A[full, full, full, full, full, full, full, full]));
            Assert.IsTrue(A[0, newaxis].Equals(A[0, newaxis, full, full, full, full, full, full, full]));
            Assert.IsTrue(A[0, full, full, full, full, full, full, full, newaxis].size_ == A.S.NumberOfElements / A.S[0]);
            Assert.IsTrue(A[newaxis, 0].Equals(A[newaxis, 0, full, full, full, full, full, full, full]));

            Assert.IsTrue(A[newaxis, ellipsis, newaxis].Equals(A[newaxis, full, full, full, full, full, full, full, full, newaxis]));
            Assert.IsTrue(A[newaxis, newaxis, ellipsis, newaxis].Equals(A[newaxis, newaxis, full, full, full, full, full, full, full, full, newaxis]));
            Assert.IsTrue(A[newaxis, ellipsis, newaxis, newaxis].Equals(A[newaxis, full, full, full, full, full, full, full, full, newaxis, newaxis]));
            Assert.IsTrue(A[ellipsis, newaxis, newaxis, newaxis].Equals(A[full, full, full, full, full, full, full, full, newaxis, newaxis, newaxis]));
            Assert.IsTrue(A[ellipsis, newaxis, 0, newaxis].Equals(A[full, full, full, full, full, full, full, newaxis, 0, newaxis]));
            Assert.IsTrue(A[0, ellipsis, newaxis, newaxis].Equals(A[0, full, full, full, full, full, full, full, newaxis, newaxis]));

            A = counter<double>(1.0, 1.0, size(5));
            Assert.IsTrue(A[ellipsis, newaxis, 0, newaxis].Equals(A[newaxis, 0, newaxis]));
            Assert.IsTrue(A[0, ellipsis, newaxis, newaxis].Equals(A[0, newaxis, newaxis]));
            Assert.IsTrue(A[0, newaxis, ellipsis, newaxis].Equals(A[0, newaxis, newaxis]));

            A = counter<double>(1.0, 1.0, size(5, 4));
            Assert.IsTrue(A[ellipsis, newaxis, 0, newaxis].Equals(A[full, newaxis, 0, newaxis]));
            Assert.IsTrue(A[0, ellipsis, newaxis, newaxis].Equals(A[0, full, newaxis, newaxis]));
            Assert.IsTrue(A[0, newaxis, ellipsis, newaxis].Equals(A[0, newaxis, full, newaxis]));

            A = counter<double>(1.0, 1.0, size(5, 4, 3));
            Assert.IsTrue(A[ellipsis, newaxis, 0, newaxis].Equals(A[full, full, newaxis, 0, newaxis]));
            Assert.IsTrue(A[0, ellipsis, newaxis, newaxis].Equals(A[0, full, full, newaxis, newaxis]));
            Assert.IsTrue(A[0, newaxis, ellipsis, newaxis].Equals(A[0, newaxis, full, full, newaxis]));

            A = counter<double>(1.0, 1.0, size(5, 4, 3, 2));
            Assert.IsTrue(A[ellipsis, newaxis, 0, newaxis].Equals(A[full, full, full, newaxis, 0, newaxis]));
            Assert.IsTrue(A[0, ellipsis, newaxis, newaxis].Equals(A[0, full, full, full, newaxis, newaxis]));
            Assert.IsTrue(A[0, newaxis, ellipsis, newaxis].Equals(A[0, newaxis, full, full, full, newaxis]));

            // erase superfluous ellipsis
            Assert.IsTrue(A[0, ellipsis, newaxis, newaxis].Equals(A[0, full, full, full, newaxis, newaxis]));
            Assert.IsTrue(A[0, ellipsis, ellipsis, newaxis, newaxis].Equals(A[0, full, full, full, newaxis, newaxis]));
            Assert.IsTrue(A[0, ellipsis, newaxis, ellipsis, newaxis, ellipsis, ellipsis].Equals(A[0, full, full, full, newaxis, newaxis]));

            A = counter<double>(1.0, 1.0, size(5, 4, 3, 2, 1));
            Assert.IsTrue(A[ellipsis, newaxis, 0, newaxis].Equals(A[full, full, full, full, newaxis, 0, newaxis]));
            Assert.IsTrue(A[0, ellipsis, newaxis, newaxis].Equals(A[0, full, full, full, full, newaxis, newaxis]));
            Assert.IsTrue(A[0, newaxis, ellipsis, newaxis].Equals(A[0, full, full, full, full, newaxis, newaxis]));

        }

    }
}
