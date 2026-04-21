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
using System.Diagnostics;

namespace ILNumerics.Core.Tests {


    [DebuggerDisplay("{ToString(),nq}")]
    public struct MyStruct {
        public double A;
        public double B;
        public double C;

        public override string ToString() {
            return $"MyStruct: {A},{B},{C}  ";
        }
    }

    [TestClass]
    public class Subarray_GetRangeML_paramsDimSpec {
        [TestMethod]
        public void SubarrayML_ParamsDimSpec8() {

            Array<double> A = counter<double>(1.0, 1.0, 5, 4, 3, StorageOrders.ColumnMajor);
            Assert.IsTrue((double)A.Subarray(zeros(1), 0, 0, 0, 0, 0, 0, 0) == 1.0);
            Assert.IsTrue((double)A.Subarray(-ones(1), -1, -1, -1, -1, -1, -1, -1) == 5 * 4 * 3);

            // ellipsis 
            Array<double> B = A.Reshape(1, 1, 1, 1, 1, 5, 4, 3);
            Assert.IsTrue((double)A.Subarray(zeros(1), ellipsis, 1, 0, 0) == 6);
            Assert.IsTrue((double)B.Subarray(zeros(1), ellipsis, 1, 0, 0) == 2);


        }

        [TestMethod]
        public void GetRange_ML_paramsDimSpec_scalars() {
            Array<double> A = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            Assert.IsTrue((double)A.Subarray(r(1, 1)) == 2.0);
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0) == 2.0);
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0) == 2.0);
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0) == 2.0);
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, 0) == 2.0);
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, 0, 0) == 2.0);
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, 0, 0, 0) == 2.0);
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, 0, 0, 0, 0) == 2.0);
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0) == 2.0);
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, 0) == 2.0);
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0) == 2.0);
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0) == 2.0);

        }
        [TestMethod]
        public void GetRange_ML_paramsDimSpec_ellipsis() {
            Array<double> A = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            Assert.IsTrue(A.Subarray((BaseArray)ellipsis).Equals(A));
            Assert.IsTrue(A.Subarray(r(1, 1), ellipsis).Equals(A[1, full, full].Reshape(1, 4, 3, 1, 1, 1, 1, 1, 1)));
            Assert.IsTrue(A.Subarray(r(1, 1), 0, ellipsis).Equals(A[1, 0, full].Reshape(1, 1, 3, 1, 1, 1, 1, 1, 1)));

            Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, ellipsis) == 2.0);
            Assert.IsTrue(A.Subarray(r(1, 1), 0, 0, ellipsis).shape.Equals(size(1, 1, 1, 1, 1, 1, 1, 1, 1)));

            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, ellipsis) == 2.0);
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, 0, ellipsis) == 2.0);
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, 0, 0, ellipsis) == 2.0);
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, 0, 0, 0, ellipsis) == 2.0);
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, 0, 0, 0, 0, ellipsis) == 2.0); // ellipsis being erased. 
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, ellipsis) == 2.0); // erased 
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, ellipsis) == 2.0); // erased 
            Assert.IsTrue(A.Subarray(r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, ellipsis).S.NumberOfDimensions == 32); // erased 

        }
        [TestMethod]
        public void GetRange_ML_paramsDimSpec_ellipsisMultiple() {
            Array<double> A = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            Assert.IsTrue(A.Subarray((BaseArray)ellipsis).Equals(A));
            Assert.IsTrue(A.Subarray(r(1, 1), ellipsis).Equals(A[1, full, full].Reshape(1, 4, 3, 1, 1, 1, 1, 1, 1)));
            Assert.IsTrue(A.Subarray(r(1, 1), 0, ellipsis).Equals(A[1, 0, full].Reshape(1, 1, 3, 1, 1, 1, 1, 1, 1)));

            Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, ellipsis) == 2.0);
            Assert.IsTrue(A.Subarray(r(1, 1), 0, 0, ellipsis).shape.Equals(size(1, 1, 1, 1, 1, 1, 1, 1, 1)));

            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, ellipsis) == 2.0);
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, 0, ellipsis) == 2.0);
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, 0, 0, ellipsis) == 2.0);
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, 0, 0, 0, ellipsis) == 2.0);
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, 0, 0, 0, 0, ellipsis) == 2.0); // ellipsis being erased. 
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, ellipsis) == 2.0); // erased 
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, ellipsis, 0, ellipsis, 0, ellipsis, ellipsis) == 2.0); // erased 
            Assert.IsTrue((double)A.Subarray(r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, ellipsis) == 2.0); // erased 
            Assert.IsTrue(A.Subarray(r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, ellipsis, ellipsis, ellipsis, ellipsis, ellipsis, ellipsis, ellipsis, ellipsis, ellipsis, ellipsis, ellipsis, ellipsis, ellipsis).S.NumberOfDimensions == 20); // erased 

        }

        [TestMethod]
        public void GetRange_ML_customStructs_simpleRoundtrip() {

            var myStructs = new MyStruct[4] {
                new MyStruct() { A = 1, B = 2, C = 3 },
                new MyStruct() { A = 4, B = 5, C = 6 },
                new MyStruct() { A = 7, B = 8, C = 9 },
                new MyStruct() { A = 10, B = 11, C = 12 }
            };

            Array<MyStruct> A = myStructs; 

            A.a = A.Reshape(2, 2);
            Assert.IsTrue(A.shape.Equals(size(2,2))); 

            Array<MyStruct> B = A[1, 1];

            Assert.IsTrue(A.GetValue(0).Equals(myStructs[0])); 
            Assert.IsTrue(A.GetValue(1).Equals(myStructs[1])); 
            Assert.IsTrue(A.GetValue(2).Equals(myStructs[2]));
            Assert.IsTrue(A.GetValue(3).Equals(myStructs[3]));

            Assert.IsTrue(B.GetValue(0).Equals(myStructs[3]));

            A[1] = new MyStruct() { A = -1, B = -2, C = -3 };
            Assert.IsTrue(A.GetValue(1).Equals(new MyStruct() { A = -1, B = -2, C = -3 }));

            A.GetValue(1, 1).Equals(myStructs[3]); 

        }

    }
}
