using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using static ILNumerics.Globals;
using static ILNumerics.ILMath; 

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public class LogicalCreationTests {

        [TestMethod]
        public void LogicalCreationScalarTest() {

            Logical A = 0 > 1;
            Assert.IsTrue(!Equals(A, null) && A.S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions); 
            Assert.IsFalse(A.GetValue(0));
            Assert.IsTrue(A.Storage.NumberTrues == 0);

            A = 0 < 1;
            Assert.IsTrue(!Equals(A, null) && A.S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions);
            Assert.IsTrue(A.GetValue(0));
            Assert.IsTrue(A.Storage.NumberTrues == 1);

        }

        [TestMethod]
        public void LogicalCreationArrayTest() {

            Logical A = new[] { true, false, false, true };
            Assert.IsTrue(A.S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions);
            Assert.IsTrue(A.S.NumberOfElements == 4);
            Assert.IsTrue(A.S[0] == 4);
            Assert.IsTrue(A.GetValue(0));
            Assert.IsFalse(A.GetValue(1));
            Assert.IsFalse(A.GetValue(2));
            Assert.IsTrue(A.GetValue(3));
            Assert.IsTrue(A.Storage.NumberTrues == 2);
        }
        [TestMethod]
        public void LogicalCreationFloatArrayTest() {

            Logical A = new float[] { 12, 0, 0, -44, float.NaN };
            Assert.IsTrue(A.S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions);
            Assert.IsTrue(A.S.NumberOfElements == 5);
            Assert.IsTrue(A.S[0] == 5);
            Assert.IsTrue(A.GetValue(0));
            Assert.IsFalse(A.GetValue(1));
            Assert.IsFalse(A.GetValue(2));
            Assert.IsTrue(A.GetValue(3));
            Assert.IsFalse(A.GetValue(4));
            Assert.IsTrue(A.Storage.NumberTrues == 2);
        }
        [TestMethod]
        public void LogicalCreationDoubleArrayTest() {

            Logical A = new double[,] { { 12, 0, double.NegativeInfinity }, { 0, -44, float.NaN } };
            Assert.IsTrue(A.S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions);
            Assert.IsTrue(A.S.NumberOfElements == 6);
            Assert.IsTrue(A.S[0] == 2);
            Assert.IsTrue(A.S[1] == 3);
            Assert.IsTrue(A.GetValue(0));
            Assert.IsFalse(A.GetValue(1));
            Assert.IsFalse(A.GetValue(2));
            Assert.IsTrue(A.GetValue(3));
            Assert.IsTrue(A.GetValue(4));
            Assert.IsFalse(A.GetValue(5));
            Assert.IsTrue(A.Storage.NumberTrues == 3);
        }
        [TestMethod]
        public void LogicalCreationUintArrayTest() {

            Logical A = new uint[,] { { 12, uint.MaxValue, 5 }, { 0, 0, 0 } };
            Assert.IsTrue(A.S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions);
            Assert.IsTrue(A.S.NumberOfElements == 6);
            Assert.IsTrue(A.S[0] == 2);
            Assert.IsTrue(A.S[1] == 3);
            Assert.IsTrue(A.GetValue(0));
            Assert.IsFalse(A.GetValue(1));
            Assert.IsTrue(A.GetValue(2));
            Assert.IsFalse(A.GetValue(3));
            Assert.IsTrue(A.GetValue(4));
            Assert.IsFalse(A.GetValue(5));
            Assert.IsTrue(A.Storage.NumberTrues == 3);
        }
        [TestMethod]
        public void LogicalCreationEmptyArrayTest() {

            Logical A = new uint[,] { { }, { } };
            Assert.IsTrue(A.S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions);
            Assert.IsTrue(A.S.NumberOfElements == 0);
            Assert.IsTrue(A.S[0] == 2);
            Assert.IsTrue(A.S[1] == 0);
            Assert.IsTrue(A.Storage.NumberTrues == 0);
        }
        [TestMethod]
        public void LogicalCreationRefereceArrayTest() {

            Logical A = new object[,] { 
                { null, new object(), new object() }, 
                { 1.0, new System.Collections.Generic.KeyValuePair<int, double>(), null }
            };
            Assert.IsTrue(A.S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions);
            Assert.IsTrue(A.S.NumberOfElements == 6);
            Assert.IsTrue(A.S[0] == 2);
            Assert.IsTrue(A.S[1] == 3);
            Assert.IsFalse(A.GetValue(0));
            Assert.IsTrue(A.GetValue(1));
            Assert.IsTrue(A.GetValue(2));
            Assert.IsTrue(A.GetValue(3));
            Assert.IsTrue(A.GetValue(4));
            Assert.IsFalse(A.GetValue(5));
            Assert.IsTrue(A.Storage.NumberTrues == 4);
        }
        [TestMethod]
        public void LogicalCreateMultiDimBoolTest() {

            var SysArr = new bool[,] {
                { true, true, true  },
                { false, false, false  },
                { true, true, true  },
                { false, false, false },
            };
            Logical A = SysArr;

            Assert.IsNotNull(A);
            Assert.IsTrue(A.S[0] == 4);
            Assert.IsTrue(A.S[1] == 3);
            Assert.IsTrue(A.S[2] == 1);
            Assert.IsTrue(A.S[3] == 1);
            Assert.IsTrue(A.S.NumberOfDimensions == 2);
            Assert.IsTrue(A.S.NumberOfElements == 12);

            Assert.IsTrue(A.GetValue(0) == true);
            Assert.IsTrue(A.GetValue(1) == false);
            Assert.IsTrue(A.GetValue(2) == true);
            Assert.IsTrue(A.GetValue(3) == false);
            Assert.IsTrue(A.GetValue(4) == true);
            Assert.IsTrue(A.GetValue(5) == false);
            Assert.IsTrue(A.GetValue(6) == true);
            Assert.IsTrue(A.GetValue(7) == false);
            Assert.IsTrue(A.GetValue(8) == true);
            Assert.IsTrue(A.GetValue(9) == false);
            Assert.IsTrue(A.GetValue(10) == true);
            Assert.IsTrue(A.GetValue(11) == false);

            Assert.IsTrue(A.GetValue(0, 0) == true);
            Assert.IsTrue(A.GetValue(0, 1) == true);
            Assert.IsTrue(A.GetValue(0, 2) == true);
            Assert.IsTrue(A.GetValue(1, 0) == false);
            Assert.IsTrue(A.GetValue(1, 1) == false);
            Assert.IsTrue(A.GetValue(1, 2) == false);
            Assert.IsTrue(A.GetValue(2, 0) == true);
            Assert.IsTrue(A.GetValue(2, 1) == true);
            Assert.IsTrue(A.GetValue(2, 2) == true);
            Assert.IsTrue(A.GetValue(3, 0) == false);
            Assert.IsTrue(A.GetValue(3, 1) == false);
            Assert.IsTrue(A.GetValue(3, 2) == false);

            Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor);
            Assert.IsTrue(A.Storage.NumberTrues == 6);
        }

        [TestMethod]
        public void LogicalImplicitCastBoolTest() {

            Logical A = true;
            Assert.IsTrue(A);

            A = false;
            Assert.IsFalse(A); 
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void LogicalNonScalarImplicitCastThrowsTest() {
            Logical A = new bool[] { true, true };
            var dummy = (bool)A;
            
            Assert.Fail(); 
        }

        [TestMethod]
        public void LogicalCreationTestExamples() {
Logical A = new bool[] { true, true, false, true };
//A.T
//Logical[1, 4] True True ...
//[0]:  ▮ ▮ ▯ ▮

Logical B = new double[] { 1, 2, 0, 4 };
//B.T
//Logical[1, 4] True True ...
//[0]:  ▮ ▮ ▯ ▮

Logical C = new float[,] {
    { 9, 9, float.MaxValue, float.NegativeInfinity },
    { 0, 0, float.NaN, 0 },
};
//C
//Logical[2, 4] True False ...
//[0]:  ▮ ▮ ▮ ▮
//[1]:  ▯ ▯ ▯ ▯
            
Logical D = new[,] {
    { new object(), new Double() + 1, new List<object>() },
    { null, null, null }
};
//D
//Logical[2, 3] True False ...
//[0]:  ▮ ▮ ▮
//[1]:  ▯ ▯ ▯

        }

        [TestMethod]
        public void LogicalConversionTest() {

            Logical A = true;

            var I = (InLogical)A;
            Assert.IsTrue(!object.ReferenceEquals(I.Storage, A.Storage));

            using (Scope.Enter(I)) {
                //Assert.IsTrue(I.Storage.ScopeState == 0); 
            }

            var R = A;
            Assert.IsTrue(object.ReferenceEquals(R.Storage, A.Storage));
            Assert.IsTrue(R.S.NumberOfElements == 1, $"{R.Storage.S.NumberOfElements}"); // release

            var O = (OutLogical)A;
            Assert.IsTrue(object.ReferenceEquals(O.Storage, A.Storage));

        }

        [TestMethod]
        public void Logical_tological_simple() {
            Array<double> A = zeros<double>(4, 3);

            A[vector<int>(1, 5, 7, 8, 11)] = -1;

            Logical L = tological(A);
            Assert.IsTrue(L.Storage.IsNumberTruesCached);
            Assert.IsTrue(L.NumberTrues == 5); 

        }

    }
}
