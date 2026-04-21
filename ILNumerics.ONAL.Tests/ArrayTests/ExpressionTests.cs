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
using System.Diagnostics;

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public class ExpressionTests {

        [TestMethod]
        public void SimpleExpressionTest() {

            var exp = Globals.end - 1;
            Assert.IsTrue(ILNumerics.Core.Misc.ILExpression.Evaluate(exp.Expression, 10) == 9);

        }

        [TestMethod]
        public void SimpleExpressionBATest() {

            Array<double> A = -10;
            var exp = Globals.end + A;
            Assert.IsTrue(ILNumerics.Core.Misc.ILExpression.Evaluate(exp.Expression, 20) == 10);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Nonscalar array should throw in subarray expressions.")]
        public void SimpleExpressionBANonScalarFailTest() {

            Array<double> A = new[] { -10, -1 };
            var exp = Globals.end + A;

        }
        [TestMethod]
        public void DimSpecArrayIndexNegativeRangeLimitScalarMLTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {
                Array<double> A = new[,] {
                    { 1, 2, 3, 4 },
                    { 5, 6, 7, 8 }
                };

                Array<double> I = -1;
                Array<double> B = A[Globals.r(I, 1, I)];

                Assert.IsTrue(B.IsScalar && B.GetValue(0) == 8);
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // had to reshape
            }
        }
        [TestMethod]
        public void SimpleExpressionBANegativeRangeLimitFullMLTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {
                Array<double> A = new[,] {
                    { 1, 2, 3, 4 },
                    { 5, 6, 7, 8 }
                };

                Array<double> I = -1;
                Array<double> B = A[Globals.r(0, 1, I)];

                Assert.IsTrue(B.IsColumnVector && B.S.NumberOfDimensions == 2);
                Array<double> Res = new double[] { 1, 5, 2, 6, 3, 7, 4, 8 };
                Assert.IsTrue(B.Equals(Res));
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // had to reshape!

            }
        }
        [TestMethod]
        public void SimpleExpressionBANegativeRangeLimitFullwoReshapeMLTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {
                Array<double> A = new[,] {
                    { 1, 2, 3, 4 },
                    { 5, 6, 7, 8 }
                };
                A.a = A.T; // make this column major, prevents the reshape later 

                Array<double> I = -1;
                Array<double> B = A[Globals.r(0, 1, I)]; //no reshape this time! 

                Assert.IsTrue(B.IsColumnVector && B.S.NumberOfDimensions == 2);
                Array<double> Res = new double[] { 1, 2, 3, 4, 5, 6, 7, 8 };
                Assert.IsTrue(B.Equals(Res));
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // no reshape!

            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Nonscalar array should throw when used as index in ranges.")]
        public void SimpleExpressionBANonScalarStartFailMLTest() {

            Array<double> A = new[] { -1, 2 };
            A = A[Globals.r(A, 1, 1)]; // non scalar index
 
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SimpleExpressionBAOORStartFailMLTest() {

            Array<double> A = new[] { 8 };
            A = A[Globals.r(A, 1, 1)]; // scalar index, 8 is OOR

        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SimpleExpressionBAOOREndFailMLTest() {

            Array<double> A = new[] { 8 };
            A = A[Globals.r(0, 1, A)]; // scalar index, 8 is OOR

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Nonscalar array should throw when used as index in ranges.")]
        public void SimpleExpressionBANonScalarEndFailMLTest() {

            Array<double> A = new[] { -1, 2 };
            A = A[Globals.r(1, 1, A)]; // non scalar index

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Nonnumeric array should throw when used as index in ranges.")]
        public void SimpleExpressionBANumericStartFailMLTest() {

            Array<ExpressionTests> A = new[] { new ExpressionTests() };
            A = A[Globals.r(A, 1, 1)]; // non scalar index

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Nonnumeric array should throw when used as index in ranges.")]
        public void SimpleExpressionBANumericEndFailMLTest() {

            Array<ExpressionTests> A = new[] { new ExpressionTests() };
            A = A[Globals.r(1, 1, A)]; // non scalar index
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Nonscalar array should throw in subarray expressions.")]
        public void SimpleExpressionBANonScalarEmptyFailTest() {

            Array<double> A = new double[] { };
            var exp = Globals.end + A;

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Nonscalar array should throw in subarray expressions.")]
        public void SimpleExpressionBANonConvertibleFailTest() {

            Array<SizeTests> A = new SizeTests[] { new SizeTests() };
            var exp = Globals.end + A;

        }
        [TestMethod]
        public void SimpleExpressionBANegativeRangeLimitFullwoReshapeOnRetArray_MLTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {
                Array<double> A = new[,] {
                    { 1, 2, 3, 4 },
                    { 5, 6, 7, 8 }
                };
                //A.a = A.T; // make this column major, prevents the reshape later 

                Array<double> I = -1;
                Array<double> B = A.T[Globals.r(0, 1, I)]; //no reshape this time! no copy. Reuse .T result!

                Assert.IsTrue(B.IsColumnVector && B.S.NumberOfDimensions == 2);
                Array<double> Res = new double[] { 1, 2, 3, 4, 5, 6, 7, 8 };
                Assert.IsTrue(B.Equals(Res));
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // no reshape!

            }
        }
        [TestMethod]
        public void SimpleExpressionBARetArrayNegativeRangeLimitFullwoReshapeOnRetArray_MLTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {
                Array<double> A = new[,] {
                    { 1, 2, 3, 4 },
                    { 5, 6, 7, 8 }
                };
                //A.a = A.T; // make this column major, prevents the reshape later 

                Array<double> I = -1;
                Array<double> B = A.T[Globals.r(0, 1, I[0])]; //no reshape this time! no copy. Reuse .T result!

                Assert.IsTrue(B.IsColumnVector && B.S.NumberOfDimensions == 2);
                Array<double> Res = new double[] { 1, 2, 3, 4, 5, 6, 7, 8 };
                Assert.IsTrue(B.Equals(Res));
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // no reshape!

            }
        }

        #region expression on dimspec ranges tests
        [TestMethod]
        public void DimSpecScalarExpressionEnd_MLTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {
                Array<double> A = new[,] {
                    { 1, 2, 3, 4 },
                    { 5, 6, 7, 8 }
                };
                //A.a = A.T; // make this column major, prevents the reshape later 

                Array<double> I = -1;
                Array<double> B = A[r(0, 1, end + 1 + I)]; //reshapes !
                Assert.IsTrue(B.IsColumnVector && B.S.NumberOfDimensions == 2);
                Array<double> Res = new double[] { 1, 5, 2, 6, 3, 7, 4, 8 };
                if (!B.Equals(Res))
                    Debugger.Break();  
                Assert.IsTrue(B.Equals(Res));
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // reshape!

                B.a = A.T[r(0, 1, end + 3 + I[0] + I + I.GetValue(0))]; //no reshape this time! no copy. Reuse .T result!

                Assert.IsTrue(B.IsColumnVector && B.S.NumberOfDimensions == 2);
                Res.a = new double[] { 1, 2, 3, 4, 5, 6, 7, 8 };
                Assert.IsTrue(B.Equals(Res));
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(!object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // no reshape!

            }
        }
        [TestMethod]
        public void DimSpecExpressionStartEndScalarOrExpression_MLTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {
                Array<double> A = new[,] {
                    { 1, 2, 3, 4 },
                    { 5, 6, 7, 8 }
                };
                //A.a = A.T; // make this column major, prevents the reshape later 

                Array<double> I = -1;
                Array<double> B = A[r(end - end + 1 + I, 7)]; //reshapes !
                Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor); //! Ensured by GetRange_ML
                Assert.IsTrue(B.IsColumnVector && B.S.NumberOfDimensions == 2);
                Array<double> Res = new double[] { 1, 5, 2, 6, 3, 7, 4, 8 };
                Assert.IsTrue(B.Equals(Res));
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // col maj. ensured!

                B.a = A.T[r(end + 3 + I[0] + I + I.GetValue(0) - end, 7)]; //no reshape this time! no copy. Reuse .T result!

                Assert.IsTrue(B.IsColumnVector && B.S.NumberOfDimensions == 2);
                // B is row major - but for vectors this is the same as being column major
                Res.a = new double[] { 1, 2, 3, 4, 5, 6, 7, 8 };
                Assert.IsTrue(B.Equals(Res));
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(!object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // no reshape!

                B.a = A.T[r(end + 3 + I[0] + I - end + I.GetValue(0), end)]; //no reshape this time! no copy. Reuse .T result!

                Assert.IsTrue(B.IsColumnVector && B.S.NumberOfDimensions == 2);
                Res.a = new double[] { 1, 2, 3, 4, 5, 6, 7, 8 };
                Assert.IsTrue(B.Equals(Res));
                Assert.IsTrue(!object.ReferenceEquals(B.Storage, A.Storage));
                Assert.IsTrue(!object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); // no reshape!
            }
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException),"Negative expression results should throw.")]
        public void DimSpecExpressionStartOORFail_MLTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {
                Array<double> A = new[,] {
                    { 1, 2, 3, 4 },
                    { 5, 6, 7, 8 }
                };
                //A.a = A.T; // make this column major, prevents the reshape later 

                Array<double> B = A[r(end - 1 - end, 7)];
 
            }
        }
        #endregion


    }
}
