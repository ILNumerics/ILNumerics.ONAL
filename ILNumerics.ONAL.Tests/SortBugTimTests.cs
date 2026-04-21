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
    public class SortBugTim_Tests {

        [TestMethod]
        public void SortNaNIndicesAsc() {

            Array<float> A = new float[,,]  {
                { {    float.NaN, float.NaN  }, {    float.NaN, float.NaN  } },
                { {    5,          6         }, {    7,          8         } },
                { {    1,          2         }, {    3,          4         } } };

            A.a = A.transpose();

            Array<long> ResI = vector<long>(2, 1, 0).Reshape(1, 1, 3).Repmat(2, 2, 1);
            Array<long> I = 0;

            Array<float> Res = sort(A, I, 2, false);

            Assert.IsTrue(ResI.Equals(I));

        }

        [TestMethod]
        public void SortNaNIndicesAsc2() {

            Array<float> A = new float[,,]  {
                { {    float.NaN, float.NaN  }, {    float.NaN, float.NaN  } },
                { {    5,          float.NaN,         }, {    7,          8         } },
                { {    1,          2         }, {    3,          4         } } };

            A.a = A.transpose();

            Array<long> ResI = vector<long>(2, 1, 0).Reshape(1, 1, 3).Repmat(2, 2, 1);
            Array<long> I = 0;

            Array<float> Res = sort(A, I, 2, false);

            Assert.IsTrue(ResI.Equals(I));

        }

        [TestMethod]
        public void SortNaNIndicesAsc2Dbl() {

            Array<double> A = new double[,,]  {
                { {    double.NaN, double.NaN  }, {    double.NaN, double.NaN  } },
                { {    5,          double.NaN,         }, {    7,          8         } },
                { {    1,          2         }, {    3,          4         } } };

            A.a = A.transpose();

            Array<long> ResI = vector<long>(2, 1, 0).Reshape(1, 1, 3).Repmat(2, 2, 1);
            Array<long> I = 0;

            Array<double> Res = sort(A, I, 2, false);

            Assert.IsTrue(ResI.Equals(I));

        }

        [TestMethod]
        public void SortNaNIndicesDesc() {

            Array<float> A = new float[,,]  {
                { {    5, float.NaN  }, {    float.NaN, 4  } },
                { {    float.NaN,          float.NaN,         }, {    7,          8         } },
                { {    1,          2         }, {    3,          float.NaN         } } };

            A.a = A.transpose();

            Array<long> ResI = vector<long>(2, 2,2,0,0,1,1,1,1,0,0,2).Reshape(2,2,3);
            Array<long> I = 0;

            Array<float> Res = sort(A, I, 2, false);

            Assert.IsTrue(ResI.Equals(I));

        }


        //[TestMethod]
        //public void TestSizeBytesMaxDimensionsOverhead() {
        //    const int c = 1000000; 
        //    object[] g = new object[c]; 
        //    for (int i = 0; i < c; i++) {
        //        g[i] = vector<int>(i); 
        //    }
        //    Console.Out.Write(g); 
        //}


    }
}
