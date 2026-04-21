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


namespace ILNumerics.Core.UnitTests.MathInternalTests {
    [TestClass]
    public class DiagTests {

        [TestMethod]
        public void Test_DiagSimple() {

            Array<double> A = counter<double>(0.0, 1.0, 5, 5);

            Assert.IsTrue(diag(A).Equals(vector<double>(0, 6, 12, 18, 24)));

            Assert.IsTrue(diag(A, -4).Equals(vector<double>(4))); 
            Assert.IsTrue(diag(A, -3).Equals(vector<double>(3,9))); 
            Assert.IsTrue(diag(A, -2).Equals(vector<double>(2,8,14)));
            Assert.IsTrue(diag(A, -1).Equals(vector<double>(1,7,13,19)));
            Assert.IsTrue(diag(A, 1).Equals(vector<double>(5, 11, 17, 23)));
            Assert.IsTrue(diag(A, 2).Equals(vector<double>(10, 16, 22)));
            Assert.IsTrue(diag(A, 3).Equals(vector<double>(15,21)));
            Assert.IsTrue(diag(A, 4).Equals(vector<double>(20)));

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Diag_OOR_PosFail() {
            Array<double> A = counter<double>(0.0, 1.0, 3, 5);
            Array<double> R = diag(A, 5);
 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Diag_OOR_NegFail() {
            Array<double> A = counter<double>(0.0, 1.0, 3, 5);
            Array<double> R = diag(A, -3);
 
        }

        [TestMethod]
        public void Diag_NonSquared() {
            Array<double> A = counter<double>(0.0, 1.0, 3, 5);

            Assert.IsTrue(diag(A, -2).Equals(vector<double>(2)));
            Assert.IsTrue(diag(A, -1).Equals(vector<double>(1, 5)));
            Assert.IsTrue(diag(A, 0).Equals(vector<double>(0, 4, 8)));
            Assert.IsTrue(diag(A).Equals(vector<double>(0, 4, 8)));
            Assert.IsTrue(diag(A, 1).Equals(vector<double>(3, 7, 11)));
            Assert.IsTrue(diag(A, 2).Equals(vector<double>(6, 10, 14)));
            Assert.IsTrue(diag(A, 3).Equals(vector<double>(9, 13)));
            Assert.IsTrue(diag(A, 4).Equals(vector<double>(12)));

        }
        [TestMethod]
        public void Diag_NonSquaredOtherSide() {
            Array<double> A = counter<double>(0.0, 1.0, 3, 5).T;

            Assert.IsTrue(diag(A, 2).Equals(vector<double>(2)));
            Assert.IsTrue(diag(A, 1).Equals(vector<double>(1, 5)));
            Assert.IsTrue(diag(A, 0).Equals(vector<double>(0, 4, 8)));
            Assert.IsTrue(diag(A).Equals(vector<double>(0, 4, 8)));
            Assert.IsTrue(diag(A, -1).Equals(vector<double>(3, 7, 11)));
            Assert.IsTrue(diag(A, -2).Equals(vector<double>(6, 10, 14)));
            Assert.IsTrue(diag(A, -3).Equals(vector<double>(9, 13)));
            Assert.IsTrue(diag(A, -4).Equals(vector<double>(12)));
        }

        [TestMethod]
        public void Diag_empty() {
            Array<int> A = empty<int>();
            Assert.IsTrue(A.S[0] == 0); 
            Assert.IsTrue(A.S[1] == 1);
            Assert.IsTrue(diag(A).S[0] == 0);
            Assert.IsTrue(diag(A).S[1] == 1);
        }
        [TestMethod]
        public void Diag_Scalar() {
            Array<uint> A = 4;
            Assert.IsTrue(A.S[0] == 1);
            Assert.IsTrue(A.S[1] == 1);
            Assert.IsTrue(diag(A).S[0] == 1);
            Assert.IsTrue(diag(A).S[1] == 1);
            Assert.IsTrue(diag(A).S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions); 
        }
        [TestMethod]
        public void Diag_NPScalar() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                Array<uint> A = 4;
                Assert.IsTrue(A.S[0] == 1);
                Assert.IsTrue(A.S[1] == 1);
                Assert.IsTrue(diag(A).S[0] == 1);
                Assert.IsTrue(diag(A).S[1] == 1);
                Assert.IsTrue(diag(A).S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions);
            }
        }

        [TestMethod]
        public void Diag_NPVector() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                Array<uint> A = vector<uint>(1, 2, 3, 5);
                Assert.IsTrue(A.S.NumberOfDimensions == 1);

                Assert.IsTrue(A.S[0] == 4);
                Assert.IsTrue(A.S[1] == 1);
                Assert.IsTrue(diag(A).S[0] == 4);
                Assert.IsTrue(diag(A).S[1] == 4);
                Assert.IsTrue(diag(A).S.NumberOfDimensions == 2);

                Array<uint> Res = zeros<uint>(4, 4);
                Res[vector<int>(0, 1, 2, 3), vector<int>(0, 1, 2, 3)] = vector<uint>(1, 2, 3, 5);
                Assert.IsTrue(diag(A).Equals(Res));

                Assert.IsTrue(diag(diag(A)).Equals(A));
                Assert.IsTrue(diag(diag(Res)).Equals(Res));

            }
        }
        [TestMethod]
        public void Diag_MLVector() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {
                Array<uint> A = vector<uint>(1, 2, 3, 5);
                Assert.IsTrue(A.S.NumberOfDimensions == 2);
                Assert.IsTrue(A.IsVector);

                Assert.IsTrue(A.S[0] == 4);
                Assert.IsTrue(A.S[1] == 1);
                Assert.IsTrue(diag(A).S[0] == 4);
                Assert.IsTrue(diag(A).S[1] == 4);
                Assert.IsTrue(diag(A).S.NumberOfDimensions == 2);

                Array<uint> Res = zeros<uint>(4, 4);
                Res[vector<int>(0, 5, 10, 15)] = vector<uint>(1, 2, 3, 5);
                Assert.IsTrue(diag(A).Equals(Res));

                Assert.IsTrue(diag(diag(A)).Equals(A));
                Assert.IsTrue(diag(diag(Res)).Equals(Res));

            }
        }
    }
}
