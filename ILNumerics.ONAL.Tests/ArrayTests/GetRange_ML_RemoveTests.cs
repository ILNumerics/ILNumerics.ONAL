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
using static ILNumerics.ILMath;
using static ILNumerics.Globals;

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public class GetRange_ML_RemoveTests {
        [TestMethod]
        public void RemoveSimple1() {
            Array<double> A = counter(1.0, 1.0, 5);

            A[2] = null;
            Assert.IsTrue(A.S.NumberOfDimensions == 2);
            Assert.IsTrue(A.S.NumberOfElements == 4, $"A.S.NumberOfElements:{A.S.NumberOfElements}.");
            Array<double> R = new double[] { 1, 2, 4, 5 };
            Assert.IsTrue(A.Equals(R));

        }
        [TestMethod]
        public void RemoveSimple2D() {
            Array<double> O = counter(1.0, 1.0, 5, 4);
            Array<double> A = O.C;

            A[full, 2] = null;
            Assert.IsTrue(A.S.NumberOfDimensions == 2);
            Assert.IsTrue(A.S.NumberOfElements == 5 * 3);

            Assert.IsTrue(A.Equals(O[full, "0,1,3"]));
            A = O.C; A[full, 0] = null; Assert.IsTrue(A.Equals(O[full, "1,2,3"]));
            A = O.C; A[full, 1] = null; Assert.IsTrue(A.Equals(O[full, "0,2,3"]));
            A = O.C; A[full, 2] = null; Assert.IsTrue(A.Equals(O[full, "0,1,3"]));
            A = O.C; A[full, 3] = null; Assert.IsTrue(A.Equals(O[full, "0,1,2"]));

            A = O.C; A[full, r(1, 2)] = null; Assert.IsTrue(A.Equals(O[full, "0,3"]));
            A = O.C; A[full, r(0, 3, 3)] = null; Assert.IsTrue(A.Equals(O[full, "1,2"]));
            A = O.C; A[full, r(0, 2, 2)] = null; Assert.IsTrue(A.Equals(O[full, "1,3"]));
            A = O.C; A[full, r(0, 2)] = null; Assert.IsTrue(A.Equals(O[full, "3"]));

            A = O.C; A[r(1, 2), full] = null; Assert.IsTrue(A.Equals(O["0,3,4", full]));
            A = O.C; A[r(0, 3, 3), full] = null; Assert.IsTrue(A.Equals(O["1,2,4", full]));
            A = O.C; A[r(0, 2, 2), full] = null; Assert.IsTrue(A.Equals(O["1,3,4", full]));
            A = O.C; A[r(0, 2), full] = null; Assert.IsTrue(A.Equals(O["3,4", full]));
            A = O.C; A[r(1, end), full] = null; Assert.IsTrue(A.Equals(O["0", full]));
            // removes all
            A = O.C; A[full, full] = null; Assert.IsTrue(A.Equals(O["", ""]));

        }
        [TestMethod]
        public void RemoveSimple3Dreshaping() {
            Array<double> O = counter(1.0, 1.0, 5, 4, 3);
            Array<double> A = O.C;

            A[full, 2] = null;
            Assert.IsTrue(A.S.NumberOfDimensions == 2, $"numdims:{A.S.NumberOfDimensions}");
            Assert.IsTrue(A.S.NumberOfElements == 5 * (4 * 3 - 1));

            A = O.C; A[full, 0] = null; Assert.IsTrue(A.Equals(O[full, "1,2,3,4,5,6,7,8,9,10,11"]));
            A = O.C; A[full, 1] = null; Assert.IsTrue(A.Equals(O[full, "0,2,3,4,5,6,7,8,9,10,11"]));
            A = O.C; A[full, 2] = null; Assert.IsTrue(A.Equals(O[full, "0,1,3,4,5,6,7,8,9,10,11"]));
            A = O.C; A[full, r(3,end)] = null; Assert.IsTrue(A.Equals(O[full, "0,1,2"]));
            A = O.C; A[full, r(3,2,end)] = null; Assert.IsTrue(A.Equals(O[full, "0,1,2,4,6,8,10"]));

        }

        [TestMethod]
        public void RemoveSimple3D_repeatIndices() {
            Array<float> A = tosingle(counter(1.0, 1.0, 5, 4, 3));
            A["1,2,1,3", ellipsis] = null;
            A[full, "0:2:", ellipsis] = null;
            A[ellipsis, 1, full] = null;  
            A[ellipsis, 1] = null;
            A[ellipsis, 0] = null;
            A[0,full] = null;
            A[full, 0] = null;
            Assert.IsTrue(A.IsEmpty, $"A.IsEmpty:{A.IsEmpty} - A.S:{A.S.ToString()}"); 
            Assert.IsTrue(A.S[0] == 0, $"A.S[0]:{A.S[0]}");
            Assert.IsTrue(A.S.NumberOfElements == 0,$"numel:{A.S.NumberOfElements}");
            Assert.IsTrue(A.S.NumberOfDimensions == 2, $"ArrayStyle:{Settings.ArrayStyle}, numdims: {A.S.NumberOfDimensions}"); 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveSimple3D_MultipleNonFullDimFail() {
            Array<double> A = counter(1.0, 1.0, 5, 4, 3);
            A["1,2", 2, full] = null;
 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveSimpleIntIndices() {
            Array<int> A = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            A[1, 0] = null;
            A[2, 0] = null; 
            Array<int> R = new int[] { 1, 3, 5, 6, 7, 8, 9, 10 };
            Assert.IsTrue(A.Equals(R)); 

            A.a = A.Reshape(2, 4);
            A[1, 2] = null; // more than 1 non-full dim provided
 
        }
    }
}
