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

namespace ILNumerics.Core.UnitTests.numpy_attributes_Tests {

    [TestClass]
    public class argminTests {
        [TestMethod]
        public void numpy_argmin_all_simple() {

            var A = counter<double>(1, -1, 5, 4, 3);
            Array<long> I = A. argmin();
            Assert.IsTrue(I == 59);


            Assert.IsTrue((-(10 - abs(counter<double>(-11, 1, 5, 4, 3)))).argmin() == 18);
            Assert.IsTrue((-(10 - abs(counter<double>(-11, -1, 5, 4, 3)))).argmin() == 0);
            Assert.IsTrue((-(10 - abs(counter<double>(-22, 1, 5, 4, 3)))).argmin() == 25);

        }
        [TestMethod]
        public void numpy_argmin_ax0_simple() {

            Array<double> A = -counter<double>(1, 1, 5, 4, 3);
            Array<long> I = A.argmin(0);
            Assert.IsTrue(I.Equals(array<long>(4, vector<long>(4, 3))));

            I = A.argmin(1);
            Assert.IsTrue(I.Equals(array<long>(3, vector<long>(5, 3))));

            I = A.argmin(2);
            Assert.IsTrue(I.Equals(array<long>(2, vector<long>(5, 4))));

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void numpy_argmin_emptyFail() {

            Array<long> A = -zeros<long>(0); 
            A = A.argmin();
        }

        [TestMethod]
        public void numpy_argmin_axisOOR() {
            Array<long> A = 0;
            Array<long> I = ones<long>(4, 3).argmin(3, values: A);
            Assert.IsTrue(I.Equals(zeros<long>(4, 3)));
            Assert.IsTrue(A.Equals(ones<long>(4, 3)));
        }
        [TestMethod]
        public void numpy_argmin_axisNeg() {
            Array<long> A = 0;
            Array<long> I = (-counter<long>(1, 1, 5, 4, 3)).argmin(-1, values: A);
            Assert.IsTrue(I.Equals(zeros<long>(5, 4) + 2));
            Assert.IsTrue(A.Equals(-counter<long>(41, 1, 5, 4)));

            A.a = -toint64(rand(5, 4, 3) * 1000);
            Assert.IsTrue(A.argmin(-1).Equals(A.argmin(2)));
            Assert.IsTrue(A.argmin(-2).Equals(A.argmin(1)));
            Assert.IsTrue(A.argmin(-3).Equals(A.argmin(0)));
        }
        [TestMethod]
        public void numpy_argmin_NpScalar() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<long> A = -11;
                Array<long> B = 0; 
                Assert.IsTrue(A.argmin() == 0);
                Assert.IsTrue(A.argmin(values: B) == 0);
                Assert.IsTrue(B == A);
                Assert.IsTrue(A.argmin(0, values: B) == 0);
                Assert.IsTrue(B == A);
                Assert.IsTrue(A.argmin(1, values: B) == 0);
                Assert.IsTrue(B == A);
                Assert.IsTrue(A.argmin(-1, values: B) == 0);
                Assert.IsTrue(B == A);

                Assert.IsTrue(A.argmin().S.NumberOfDimensions == 0);
                Assert.IsTrue(A.argmin(values: B).S.NumberOfDimensions == 0);
                Assert.IsTrue(B.S.NumberOfDimensions == 0);
                Assert.IsTrue(A.argmin(0, values: B).S.NumberOfDimensions == 0);
                Assert.IsTrue(B.S.NumberOfDimensions == 0);
                Assert.IsTrue(A.argmin(1, values: B).S.NumberOfDimensions == 0);
                Assert.IsTrue(B.S.NumberOfDimensions == 0);


            }
        }

        #region argmin / nanargmin tests
        [TestMethod]
        public void numpy_argmin_allNaN() {
            Array<fcomplex> A = new fcomplex[] {
                new fcomplex(1,2),
                new fcomplex(3,float.NaN),
                new fcomplex(5,6)
            };
            Array<long> B = A.argmin();
            Assert.IsTrue(B == 1);

            Array<fcomplex> V = new fcomplex();

            B = A.argmin(values: V);
            Assert.IsTrue(B == 1);
            Assert.IsTrue(fcomplex.IsNaN(V.GetValue(0)));

        }
        [TestMethod]
        public void numpy_nanargmin_allNaN() {
            Array<fcomplex> A = new fcomplex[] {
                new fcomplex(1,2),
                new fcomplex(3,float.NaN),
                new fcomplex(5,6)
            };
            Array<long> B = A.nanargmin();
            Assert.IsTrue(B == 0);

            Array<fcomplex> V = new fcomplex();

            B = A.nanargmin(values: V);
            Assert.IsTrue(B == 0);
            Assert.IsTrue(V.GetValue(0) == A.GetValue((long)B));
            Assert.IsTrue(V.IsScalar);

        }
        [TestMethod]
        public void numpy_nanargmin_axisNaN() {
            Array<fcomplex> A = new fcomplex[,] {
                {
                    new fcomplex(1,2),
                    new fcomplex(3,float.NaN),
                    new fcomplex(5,6)
                },
                {
                    new fcomplex(7,8),
                    new fcomplex(9,10),
                    new fcomplex(float.NaN,12)
                }
            };
            Array<long> B = A.nanargmin(1); // uses MathInternal.min with indices, ignores values
            Assert.IsTrue(B.shape.Equals(vector<long>(2,1)));
            Assert.IsTrue(B.GetValue(0) == 0);
            Assert.IsTrue(B.GetValue(1) == 0);

            Array<fcomplex> V = default(fcomplex);

            B = A.nanargmin(axis:1, values: V); // uses MathInternal.min, with indices, gives values
            Assert.IsTrue(B.shape.Equals(vector<long>(2,1)));
            Assert.IsTrue(B.GetValue(0) == 0);
            Assert.IsTrue(B.GetValue(1) == 0);

            Assert.IsTrue(V.GetValue(0) == A.GetValue(0, B.GetValue(0))); 
            Assert.IsTrue(V.GetValue(1) == A.GetValue(1, B.GetValue(1))); 
        }
        [TestMethod]
        public void numpy_argmin_axisNaN() {
            Array<fcomplex> A = new fcomplex[,] {
                { new fcomplex(1,2), new fcomplex(3,float.NaN), new fcomplex(5,6) },
                { new fcomplex(7,8), new fcomplex(9,10), new fcomplex(float.NaN,12) }
            };
            Array<long> B = A.argmin(1); // uses numpy.min with indices, ignores values
            Assert.IsTrue(B.shape.Equals(vector<long>(2,1)));
            Assert.IsTrue(B.GetValue(0) == 1);
            Assert.IsTrue(B.GetValue(1) == 2);

            Array<fcomplex> V = default(fcomplex);

            B = A.argmin(axis:1, values: V); // uses numpy.min, with indices, gives values
            Assert.IsTrue(B.shape.Equals(vector<long>(2,1)));
            Assert.IsTrue(B.GetValue(0) == 1);
            Assert.IsTrue(B.GetValue(1) == 2);

            Assert.IsTrue(fcomplex.IsNaN(V.GetValue(0)) && V.GetValue(0).real == A.GetValue(0, B.GetValue(0)).real); 
            Assert.IsTrue(fcomplex.IsNaN(V.GetValue(1)) && V.GetValue(1).imag == A.GetValue(1, B.GetValue(1)).imag); 
        }
        #endregion


    }
}
