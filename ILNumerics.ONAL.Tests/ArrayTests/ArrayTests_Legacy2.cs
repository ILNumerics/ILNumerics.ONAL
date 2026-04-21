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

namespace ILNumerics.UnitTests.Legacy_Tests {

    [TestClass]
    public partial class ArrayTests2 {

        [TestMethod]
        public void Test_CreatePhysicalDoubleSubarrayFromPhysical_stringDef() {
            Array<double> A = todouble(arange<float>(1, 24));
            A = reshape<double>(A, 4, 3, 2);
        }

        [TestMethod]
        public void Test_CreatePhysicalSubarrayFromPhysical_stringDef() {
            Array<float> A = tosingle(arange<float>(1, 24));
            A = reshape<float>(A, 4, 3, 2);
            Array<float> B = A[":"];
            Array<float> Res = vector<float>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24).Reshape(24, 1);
            Assert.IsTrue(B.Equals(Res), "A[\":\"] failed.");

            // shift negative
            //B = A[":"].Shifted(-3);
            //Res = array<float>(new float[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 }, 24, 1);
            //Assert.IsTrue(B.Equals(Res), "A[\":\"] failed.");

            // shift inside dims 
            B = A[":,:"];
            Res = vector<float>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24).Reshape(1, 48);
            Assert.IsTrue(B.Equals(Res), "A[\":,:\"] failed.");

            B = A[":;:"];
            Res = vector<float>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24).Reshape(4, 6);
            Assert.IsTrue(B.Equals(Res), "A[\":;:\"] failed.");

            B = A[":;:;:"];
            Res = vector<float>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24).Reshape(4, 3, 2);
            Assert.IsTrue(B.Equals(Res), "A[\":;:;:\"] failed.");

            // check if returned array is trimmed correctly
            // trim singleton at end & check regularity of spacing 
            B = A[":;:;1"];
            Res = vector<float>(13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24).Reshape(4, 3);
            Assert.IsTrue(B.Equals(Res), "A[\":;:;1\"] failed.");

            B = A["1,2;:;0"];
            Res = vector<float>(2, 3, 6, 7, 10, 11).Reshape(2, 3);
            Assert.IsTrue(B.Equals(Res), "A[\":;:;1\"] failed.");
            Assert.IsTrue(B.Size[0].Equals(2), "A[\":;:;1\"] failed.");
            Assert.IsTrue(B.Size[1].Equals(3), "A[\":;:;1\"] failed.");

            B = A["1,2;1,:;1"];
            Res = vector<float>(18, 19, 14, 15, 18, 19, 22, 23).Reshape(2, 4);
            Assert.IsTrue(B.Equals(Res), "A[\":;:;1\"] failed.");
            Assert.IsTrue(B.Size[0].Equals(2), "A[\":;:;1\"] failed.");
            Assert.IsTrue(B.Size[1].Equals(4), "A[\":;:;1\"] failed.");

            B = A["2;1;0"];
            Res = 7;
            Assert.IsTrue(B.Equals(Res), "A[\"2;1;0\"] failed.");

            B = A["2,1;1,0;0"];
            Res = vector<float>(7, 6, 3, 2 ).Reshape(2, 2);
            Assert.IsTrue(B.Equals(Res), "A[\"2,1;1,0;0\"] failed.");

            // test result if matrix             
            B = A["2,1;2,1,0;0"];
            Res = vector<float>(11, 10, 7, 6, 3, 2 ).Reshape(2, 3);
            Assert.IsTrue(B.Equals(Res), "A[\"2,1,0;1,0;0\"] failed. Result: " + Res);

            B = A["0,1,2,3;0,1,2,3,4,5"];
            Res = vector<float>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24).Reshape(4, 6);
            Assert.IsTrue(B.Equals(Res), "A[\"0,1,2,3;0,1,2,3,4,5\"] failed. Result: " + Res);

        }

    }
}
