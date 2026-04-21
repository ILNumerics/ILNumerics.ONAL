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
using System.Diagnostics;
using permType = System.String;
using static ILNumerics.Globals;
using static ILNumerics.ILMath;

namespace ILNumerics.UnitTests.BuiltInFunctions {

    [TestClass]
    public class permuteTests {

        [TestMethod]
        public void PermuteBasicTest() {
            Array<double> A = counter(0.0, 1.0, size(4, 3, 2));
            Array<double> R = permute(A, vector<long>(2, 1, 0));
            Array<double> Res = array<double>(new double[] { 0, 12, 4, 16, 8, 20, 1, 13, 5, 17, 9, 21, 2, 14, 6, 18, 10, 22, 3, 15, 7, 19, 11, 23 }, size(2, 3, 4));
            Assert.IsTrue(R.Equals(Res));
        }

        [TestMethod]
        public void PermuteHugeTest() {
            // run once for JIT compilation overhead
            Array<double> A = 1;
            permute(A, vector<long>(0, 1));

            A = counter(0.0, 1.0, size(400, 300, 100));
            var sw = Stopwatch.StartNew();
            permute(A, vector<uint>(2, 1, 0));
            sw.Stop(); 
            Trace.WriteLine("PermuteHugeTest neede[ms, " + A.S.ToString() + "<double>]: " + sw.ElapsedMilliseconds.ToString());
            //Array<double> Res = array<double>(new double[] { 0, 12, 4, 16, 8, 20, 1, 13, 5, 17, 9, 21, 2, 14, 6, 18, 10, 22, 3, 15, 7, 19, 11, 23 }, size(2, 3, 4));
            //Assert.IsTrue(R.Equals(Res));
        }
        [TestMethod]
        public void PermuteHugeTestUInt16() {
            // run once for JIT compilation overhead
            Array<ushort> A = 1;
            permute(A, vector<uint>(0, 1));

            A = counter<ushort>(0, 1, size(400, 300, 100));
            var sw = new Stopwatch();
            sw.Start();
            permute(A, vector<long>(2, 1, 0));
            Trace.WriteLine("PermuteHugeTest needed [ms, " + A.S.ToString() + ",<ushort>]: " + sw.ElapsedMilliseconds.ToString());
            //Array<double> Res = array<double>(new double[] { 0, 12, 4, 16, 8, 20, 1, 13, 5, 17, 9, 21, 2, 14, 6, 18, 10, 22, 3, 15, 7, 19, 11, 23 }, size(2, 3, 4));
            //Assert.IsTrue(R.Equals(Res));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void permuteNullTest() {
            Array<double> A = null;
            permute(A, vector<uint>(1, 0, 2));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void permuteToofewArgumentsTest() {
            Array<double> A = ones(4, 3, 2);
            permute(A, vector<uint>(1, 0));  // expected: 3 dimension indices

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void permuteDimensionsNotUniqueTest() {
            Array<double> A = ones(4, 3, 2);
            permute(A, vector<uint>(1, 0, 1));  // expected: 3 dimension indices
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void permuteDimensionsNegativeTest() {
            Array<double> A = ones(4, 3, 2);
            permute(A, vector<long>(-1, 0, 1));  // expected: 3 dimension indices
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void permuteDimensionsOORangeTest() {
            Array<double> A = ones(4, 3, 2);
            permute(A, vector<uint>(1, 0, 3));  // expected: 3 dimension indices
        }

        [TestMethod]
        public void permuteEmptyTest() {
            Array<double> A = empty<double>(1, 3, dim2: 0);
            Array<double> R = permute(A, vector<uint>(1, 0, 2));
            Array<double> Res = empty<double>(3, 1, dim2: 0);

            Assert.IsTrue(R.Equals(Res));
        }

        [TestMethod]
        public void permuteGenericTest() {
            Array<permType> A = vector(
                new string[] { 
                 "000", "100", "200", "300" , 
                 "010", "110", "210", "310" , 
                 "020", "120", "220", "320" , 
                 "030", "130", "230", "330" , 
                 "001", "101", "201", "301" , 
                 "011", "111", "211", "311" , 
                 "021", "121", "221", "321" , 
                 "031", "131", "231", "331" , 
                }).Reshape(size(4,4,2));

            Array<permType> R = permute(A, vector<uint>(1, 0, 2));
            Array<int> Ind = new int[] { 0, 4, 8, 12, 1, 5, 9, 13, 2, 6, 10, 14, 3, 7, 11, 15, 16, 20, 24, 28, 17, 21, 25, 29, 18, 22, 26, 30, 19, 23, 27, 31 };
            Assert.IsTrue(reshape<permType>(A[Ind],size(4,4,2)).Equals(R)); 
            
        }
    }
}
