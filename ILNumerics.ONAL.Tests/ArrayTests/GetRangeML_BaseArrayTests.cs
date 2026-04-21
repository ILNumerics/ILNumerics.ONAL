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
using ILNumerics.Core.StorageLayer;
using ILNumerics.Core.Arrays;
using System.Text;

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public class GetRangeML_BaseArray {

        [TestMethod]
        public void GetRange_ML_cell_simple() {
            Array<double> A = counter(1.0, 1.0, 3, 4);
            var r_ = cellv(1);
            Array<double> B = A[r_, 1];
            Assert.IsTrue(B.Equals(5));
        }
        [TestMethod]
        public void GetRange_ML_cell_twoSimple() {
            Array<double> A = counter(1.0, 1.0, 3, 4);
            var r_ = cellv(1, 1);
            Array<double> B = A[r_, 1];
            Assert.IsTrue(B.S[0] == 2);
            Assert.IsTrue(B.S[1] == 1);
            Assert.IsTrue(B.GetValue(0) == 5);
            Assert.IsTrue(B.GetValue(1) == 5);
        }
        [TestMethod]
        public void GetRange_ML_cell_fullRow_StringFloatCell() {
            Array<double> A = counter(1.0, 1.0, 3, 4);
            var r_ = cellv(0, "1", cellv(2f, 3));
            Array<double> B = A[full, r_];
            Assert.IsTrue(B.Equals(A));
        }

        [TestMethod]
        public void GetRange_ML_cell_fullRow_StringFloatCell2() {
            Array<double> A = counter(1.0, 1.0, 3, 4);
            Assert.IsTrue(A.Equals(A[full, cellv("0,1,2", cellv(3f))]));
            Assert.IsTrue(A.Equals(A[full, cellv("0:2", cellv("3:3"))]));
            Assert.IsTrue(A.Equals(A[full, cellv("0:2", cellv(vector<int>(3)))]));
            Assert.IsTrue(A.Equals(A["0:2", cellv("0:3")]));
        }

        [TestMethod]
        public void GetRange_ML_SingleElementSimple2D() {
            Array<double> A = counter(1.0, 0.1, 3, 4);
            Array<double> B = A[1, 1];
            Assert.IsTrue(B.S[0] == 1 && B.S[1] == 1 && B.S.NumberOfDimensions == 2);
            Assert.IsTrue(B.GetValue(0) == 1.4); 
        }
        [TestMethod]
        public void GetRange_ML_SingleElementSimple3D() {
            Array<double> A = counter(1.0, 0.1, 3, 3, 4);
            Array<double> B = A[1, 1];
            Assert.IsTrue(B.S[0] == 1 && B.S[1] == 1 && B.S.NumberOfDimensions == 2);
            Assert.IsTrue(B.GetValue(0) == 1.4); 
        }
        [TestMethod]
        public void GetRangeML_BaseArray_2D_simple() {

            Array<double> A = counter(1.0, 1.0, 5, 4);
            var B = A["0:4", "0:3"];


            Array<double> BL = B;
            //Assert.IsTrue(object.ReferenceEquals(BL.Storage.m_handles, A.Storage.m_handles));

            Assert.IsTrue(A.Equals(BL));

        }
        [TestMethod]
        public void GetRangeML_BaseArray_2D_simpleNoAcc() {
            //ILN(enabled=false)
            Array<double> A = counter(1.0, 1.0, 5, 4);
            var B = A["0:4", "0:3"];


            Array<double> BL = B;
            Assert.IsTrue(object.ReferenceEquals(BL.Storage.m_handles, A.Storage.m_handles));

            Assert.IsTrue(A.Equals(BL));

            //ILN(enabled=true)
        }

        [TestMethod]
        public void GetRange_ML_BaseArray_1D() {

            Array<ushort> A = touint16(counter(1.0, 1.0, 5, 4));

            Assert.IsTrue(A.Equals(A[full].Reshape(5, 4)));
            Assert.IsTrue(A.Equals(A[r(0,end)].Reshape(5, 4)));
            Assert.IsTrue(A.Equals(A[r(0,-1)].Reshape(5, 4)));
            Assert.IsTrue(A.Equals(A[":"].Reshape(5, 4)));
            Assert.IsTrue(A.Equals(A["0:end"].Reshape(5, 4)));
            Assert.IsTrue(A.Equals(A["0:"].Reshape(5, 4)));
            Assert.IsTrue(A.Equals(A[":end"].Reshape(5, 4)));

            Array<byte> I = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };
            Assert.IsTrue(A[":"].Equals(A[I])); 
            Assert.IsTrue(A[":"].T.Equals(A[I].T));
            Assert.IsTrue(A.T["1:2:"].Equals(A.T[I["1:2:"]]));
            Assert.IsTrue(A["3"].Equals(A[I[3]]));
            Assert.IsTrue(A["3"].Equals(A[I[r(3, 3)]]));
            Assert.IsTrue(A["3"].Equals(A[I[r(3, 2, 3)]]));
            Assert.IsTrue(A["3"].Equals(A[I["3"]]));
            Assert.IsTrue(A["3"].Equals(A[I["3:3"]]));
            Assert.IsTrue(A["3"].Equals(A[I["3:3:3"]]));

            Assert.IsTrue(A.T["3"].Equals(A.T[I[3]]));
            Assert.IsTrue(A.T["3"].Equals(A.T[I[r(3, 3)]]));
            Assert.IsTrue(A.T["3"].Equals(A.T[I[r(3, 2, 3)]]));
            Assert.IsTrue(A.T["3"].Equals(A.T[I["3"]]));
            Assert.IsTrue(A.T["3"].Equals(A.T[I["3:3"]]));
            Assert.IsTrue(A.T["3"].Equals(A.T[I["3:3:3"]]));

            A = touint16(counter(1.0, 1.0, 5, 4, 3));
            Assert.IsTrue(A.T["3"].Equals(A.T[I[3]]));
            Assert.IsTrue(A.T["3"].Equals(A.T[I[r(3, 3)]]));
            Assert.IsTrue(A.T["3"].Equals(A.T[I[r(3, 2, 3)]]));
            Assert.IsTrue(A.T["3"].Equals(A.T[I["3"]]));
            Assert.IsTrue(A.T["3"].Equals(A.T[I["3:3"]]));
            Assert.IsTrue(A.T["3"].Equals(A.T[I["3:3:3"]]));

        }
        [TestMethod]
        public void GetRangeML_BaseArray_2D() {

            Array<float> A = tosingle(counter(1.0, 1.0, 5, 4));
            Array<float> B = new float[,] { { 2, 7, 12, 17 }, { 3, 8, 13, 18 } };
            Array<int> I = new int[,] { { 0, 1, 2, 3 } };

            Assert.IsTrue(!object.ReferenceEquals(A.Storage.m_handles, B.Storage.m_handles));
            Assert.IsTrue(B.Equals(A["1:2", full]));
            Assert.IsTrue(B.Equals(A["1:2", "0:end"]));
            Assert.IsTrue(B.Equals(A["1:2", r(0, 3)]));
            Assert.IsTrue(B.Equals(A["1:2", r(0, -1)]));
            Assert.IsTrue(B.Equals(A["1:2", r(0, end)]));
            Assert.IsTrue(B.Equals(A["1,2", ":"]));
            Assert.IsTrue(B.Equals(A["1,2", "0,1,2,3"]));
            Assert.IsTrue(B.Equals(A["1,2", I]));
            Assert.IsTrue(B.Equals(A["1,2", I[full]]));
            Assert.IsTrue(B.Equals(A["1,2", I[":"]]));
            Assert.IsTrue(B.Equals(A["1,2", I["0:"]]));
            Assert.IsTrue(B.Equals(A["1,2", I["0:end"]]));
            Assert.IsTrue(B.Equals(A["1,2", I["0:-1"]]));
            Assert.IsTrue(B.Equals(A["1,2", I[":-1"]]));
            Assert.IsTrue(B.Equals(A["1,2", "0:3"]));
            Assert.IsTrue(B.Equals(A["1,2", "0:1:3"]));
            Assert.IsTrue(B.Equals(A["1,2", "0::3"]));
            Assert.IsTrue(B.Equals(A[r(1, 1, 2), "0::3"]));
            Assert.IsTrue(B.Equals(A[r(1, 2), "0::3"]));
            Assert.IsTrue(B.Equals(A[r(1, 2), r(0, 3)]));
            Assert.IsTrue(A[end, end].Equals(A[r(-1, -1), I[-1]]));
            Assert.IsTrue(B[end, end].Equals(A[r(-3, -3), I[-1]]));
            Array<float> scalar = 20;
            Assert.IsTrue(scalar.Equals(A[r(-1, 1, end), I[end], 0]));
            scalar = 18;
            Assert.IsTrue(scalar.Equals(B[r(-1, 1, end), I[end], 0]));
            Assert.IsTrue(scalar.Equals(B[r(-1, 1, end), I[-1], 0]));
        }
        [TestMethod]
        public void GetRangeML_BaseArray_2D_NoAcc() {
            //ILN(enabled=false)
            Array<float> A = tosingle(counter(1.0, 1.0, 5, 4));
            Array<float> B = new float[,] { { 2, 7, 12, 17 }, { 3, 8, 13, 18 } };
            Array<int> I = new int[,] { { 0, 1, 2, 3 } };

            Assert.IsTrue(!object.ReferenceEquals(A.Storage.m_handles, B.Storage.m_handles));
            Assert.IsTrue(B.Equals(A["1:2", full]));
            Assert.IsTrue(B.Equals(A["1:2", "0:end"]));
            Assert.IsTrue(B.Equals(A["1:2", r(0, 3)]));
            Assert.IsTrue(B.Equals(A["1:2", r(0, -1)]));
            Assert.IsTrue(B.Equals(A["1:2", r(0, end)]));
            Assert.IsTrue(B.Equals(A["1,2", ":"]));
            Assert.IsTrue(B.Equals(A["1,2", "0,1,2,3"]));
            Assert.IsTrue(B.Equals(A["1,2", I]));
            Assert.IsTrue(B.Equals(A["1,2", I[full]]));
            Assert.IsTrue(B.Equals(A["1,2", I[":"]]));
            Assert.IsTrue(B.Equals(A["1,2", I["0:"]]));
            Assert.IsTrue(B.Equals(A["1,2", I["0:end"]]));
            Assert.IsTrue(B.Equals(A["1,2", I["0:-1"]]));
            Assert.IsTrue(B.Equals(A["1,2", I[":-1"]]));
            Assert.IsTrue(B.Equals(A["1,2", "0:3"]));
            Assert.IsTrue(B.Equals(A["1,2", "0:1:3"]));
            Assert.IsTrue(B.Equals(A["1,2", "0::3"]));
            Assert.IsTrue(B.Equals(A[r(1, 1, 2), "0::3"]));
            Assert.IsTrue(B.Equals(A[r(1, 2), "0::3"]));
            Assert.IsTrue(B.Equals(A[r(1, 2), r(0, 3)]));
            Assert.IsTrue(A[end, end].Equals(A[r(-1, -1), I[-1]]));
            Assert.IsTrue(B[end, end].Equals(A[r(-3, -3), I[-1]]));
            Array<float> scalar = 20;
            Assert.IsTrue(scalar.Equals(A[r(-1, 1, end), I[end], 0]));
            scalar = 18;
            Assert.IsTrue(scalar.Equals(B[r(-1, 1, end), I[end], 0]));
            Assert.IsTrue(scalar.Equals(B[r(-1, 1, end), I[-1], 0]));

            //ILN(enabled=true)
        }
        [TestMethod]
        public void GetRangeML_BaseArray_1D_AllSimpleRange_NoAcc() {
            //ILN(enabled=false)
            Array<float> A = tosingle(counter(1.0, 1.0, 5, 4));
            Array<float> B = new float[,] { { 2, 7, 12, 17 }, { 3, 8, 13, 18 } };

            Assert.IsTrue(!object.ReferenceEquals(A.Storage.m_handles, B.Storage.m_handles));
            Assert.IsTrue(B.Equals(A["1:2;:"]));
            Assert.IsTrue(B.Equals(A["1:2;0:end"]));
            Assert.IsTrue(B.Equals(A["1,2;:"]));
            Assert.IsTrue(B.Equals(A["1,2;0,1,2,3"]));
            Assert.IsTrue(B.Equals(A["1,2;0:3"]));
            Assert.IsTrue(B.Equals(A["1,2;0:1:3"]));
            Assert.IsTrue(B.Equals(A["1,2;0::3"]));
            //ILN(enabled=true)
        }
        [TestMethod]
        public void GetRangeML_BaseArray_1D_AllSimpleRange() {

            Array<float> A = tosingle(counter(1.0, 1.0, 5, 4));
            Array<float> B = new float[,] { { 2, 7, 12, 17 }, { 3, 8, 13, 18 } };

            Assert.IsTrue(!object.ReferenceEquals(A.Storage.m_handles, B.Storage.m_handles));
            Assert.IsTrue(B.Equals(A["1:2;:"]));
            Assert.IsTrue(B.Equals(A["1:2;0:end"]));
            Assert.IsTrue(B.Equals(A["1,2;:"]));
            Assert.IsTrue(B.Equals(A["1,2;0,1,2,3"]));
            Assert.IsTrue(B.Equals(A["1,2;0:3"]));
            Assert.IsTrue(B.Equals(A["1,2;0:1:3"]));
            Assert.IsTrue(B.Equals(A["1,2;0::3"]));

        }

        [TestMethod]
        public void GetRangeML_BaseArray_1D_AllSimpleRange2DMerge3D() {

            Array<float> A = tosingle(counter(1.0, 1.0, 5, 2, 2));
            Array<float> B = new float[,] { { 2, 7, 12, 17 }, { 3, 8, 13, 18 } };

            Assert.IsTrue(!object.ReferenceEquals(A.Storage.m_handles, B.Storage.m_handles));
            Assert.IsTrue(B.Equals(A["1:2;:"]));
            Assert.IsTrue(B.Equals(A["1:2;0:end"]));
            Assert.IsTrue(B.Equals(A["1,2;:"]));
            Assert.IsTrue(B.Equals(A["1,2;0,1,2,3"]));
            Assert.IsTrue(B.Equals(A["1,2;0:3"]));
            Assert.IsTrue(B.Equals(A["1,2;0:1:3"]));
            Assert.IsTrue(B.Equals(A["1,2;0::3"]));
        }
        [TestMethod]
        public void GetRangeML_BaseArray_1D_MultiDimSemicolonFailSafe1() {

            Array<float> A = tosingle(counter(1.0, 1.0, 5, 4, 2));
            Array<float> B = A[";;:;"];

            Assert.IsTrue(B.S[0] == 0);
            Assert.IsTrue(B.S[1] == 0);
            Assert.IsTrue(B.S[2] == 2);
            Assert.IsTrue(B.S[3] == 0);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetRangeML_BaseArray_1D_MultiDimSemicolonFail() {

            Array<float> A = tosingle(counter(1.0, 1.0, 5, 4, 2));
            Array<float> B = A[";;:", full];
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetRangeML_BaseArray_1D_MultiDimSemicolonFail2() {

            Array<float> A = tosingle(counter(1.0, 1.0, 5, 4, 2));
            Array<float> B = A[";;:", ":"];
        }

        [TestMethod]
        public void GetRangeML_BaseArray_1D_SingletonNegBAInd() {

            Array<int> A = counter<int>(1, 1, 1, 1, 5, 4, 1, 1, 1);
            Assert.IsTrue((double)A.Subarray(-ones(1)) == 20); 
            Assert.IsTrue((double)A.Subarray(-ones(1), -1) == 20); 
            Assert.IsTrue((double)A.Subarray(-ones(1), -1, -1) == 20); 
            Assert.IsTrue((double)A.Subarray(-ones(1), -1, -1, -1) == 20); 
            Assert.IsTrue((double)A.Subarray(-ones(1), -1, -1, -1, -1) == 20); 
            Assert.IsTrue((double)A.Subarray(-ones(1), -1, -1, -1, -1, -1) == 20); 
            Assert.IsTrue((double)A.Subarray(-ones(1), -1, -1, -1, -1, -1, -1) == 20); 
            Assert.IsTrue((double)A.Subarray(-ones(1), -1, -1, -1, -1, -1, -1, -1) == 20); 
            Assert.IsTrue((double)A.Subarray(-ones(1), -1, -1, -1, -1, -1, -1, -1, -1) == 20); 

        }

        string prepareErrorString(BaseArray A, BaseArray[] I, Exception exc = null) {
            var ret = new StringBuilder(); //  ; 
            ret.AppendLine($"A:{ (object.Equals(A, null) ? "[null]" : A.ToString())}");
            if (I != null) {
                for (int i = 0; i < I.Length; i++) {
                    var Ii = I[i];
                    ret.AppendLine($"I{i}: {Ii.ToString()}");
                }
            } else {
                ret.AppendLine($"I: [null]"); 
            }

            if (exc != null) {
                ret.AppendLine($"Exception: {exc}");
            } else {
                ret.AppendLine($"Exception: [null]");
            }
            return ret.ToString(); 
        }

        #region getrange on logicals 
        [TestMethod]
        public void GetRange_ML_OnLogical() {

            Logical L = counter(1.0, 1.0, 5, 4, 3) % 2 > 0;
            Assert.IsTrue(L.Storage.NumberTrues == 30);
            Assert.IsTrue(L[ellipsis, 1].Storage.NumberTrues == 10); 
        }
        #endregion

        #region getrange with logicals
        [TestMethod]
        public void GetRange_ML_LogicalIndex01() {

            Array<double> A = counter(1.0, 1.0, 5, 4);
            Logical L = new bool[] { false, true, false, true };
            Array<int> I = new int[] { 1, 3 }; 
            // too short is fine 
            Assert.IsTrue(A[L,1].Equals(A[I,1]));
            // empty in fine 
            Assert.IsTrue(A[L[slice(0,0)],1].Equals(A[I[slice(0,0)],1]));
            Assert.IsTrue(A[L[slice(0, 0)], 1].S.NumberOfElements == 0);

            // numpy scalar is fine 
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                I = 1; // creates a 0-dim, numpy scalar 
                L = true; 
            }
            Assert.IsTrue(A[L].Equals(A[0])); 
            Assert.IsTrue(A[L].Equals(A[I - 1]));
        }
        #endregion
    }
}
