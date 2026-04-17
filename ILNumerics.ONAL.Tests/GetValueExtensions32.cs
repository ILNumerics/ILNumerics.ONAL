using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILNumerics;
using static ILNumerics.Globals;
using static ILNumerics.ILMath;

namespace ILNumerics.Core.Tests {

    [TestClass]
    public class GetValueExtensions32 {

        [TestMethod]
        public void GetValue32() {

            Array<double> A = counter<double>(1.0, 1.0, size(1, 2, 3, 4, 5, 6, 7, 8), order: StorageOrders.ColumnMajor);

            Assert.IsTrue(A.GetValue(0) == 1.0);
            Assert.IsTrue(A.GetValue(-1) == 40320);
            Assert.IsTrue(A.GetValue(0, 1) == 2.0);
            Assert.IsTrue(A.GetValue(0, 0, 1) == 3.0);
            Assert.IsTrue(A.GetValue(0, 0, 0, 0, 0, 0, 0, 0) == 1.0);
            Assert.IsTrue(A.GetValue(0, 0, 0, 0, 0, 0, 0, 0, 0) == 1.0);
            Assert.IsTrue(A.GetValue(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0) == 1.0);
            Assert.IsTrue(A.GetValue(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, /*34:*/ 0) == 1.0);
        }

        [TestMethod]
        public void GetValue32Neg() {

            Array<long> s = (ones<long>(1, 16) * vector<long>(1,2))[full];
 
            Array<double> A = counter<double>(1.0, 1.0, s, order: StorageOrders.ColumnMajor);
            Assert.IsTrue(A.GetValue(-1) == 1 << 16); 
            Assert.IsTrue(A.GetValue(0,-1) == 1 << 16); 
            Assert.IsTrue(A.GetValue(0,1,-1) == 1 << 16); 
            Assert.IsTrue(A.GetValue(0,1,0, -1) == 1 << 16); 
            // ...
            Assert.IsTrue(A.GetValue(0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, -1) == 1 << 16);
            Assert.IsTrue(A.GetValue(0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, -1) == 1 << 16);
            Assert.IsTrue(A.GetValue(0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, -1) == 1 << 16);
            Assert.IsTrue(A.GetValue(0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, -1) == 1 << 16);
            Assert.IsTrue(A.GetValue(0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, -1) == 1 << 16);
            Assert.IsTrue(A.GetValue(0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, -1, /*34:*/ 0) == 1 << 16);
            Assert.IsTrue(A.GetValue(-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, /*34:*/ 0) == 1 << 16);
        }

        [ExpectedException(typeof(IndexOutOfRangeException))]
        [TestMethod]
        public void GetValue33Fail() {

            Array<double> A = counter<double>(1.0, 1.0, ones<long>(8, 1), order: StorageOrders.ColumnMajor);
            
            Assert.IsTrue(A.GetValue(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0) == 1.0);

            // fails: 
            A.GetValue(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1);  // 33 indices
        }

        [TestMethod]
        public void SetValueExt13() {
            Array<int> A = ones<int>(1, 2, 3, 4, 5, 6, 7);
            A.a = A.Reshape(1, 1, 1, 1, 1, 1, 1, 2, 3, 4, 5, 6, -1);

            Assert.IsTrue(A.S.NumberOfDimensions == 13);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7);
            Assert.IsTrue(prodall(A) == 1);
            Assert.IsTrue(A.S[12] == 7);

            A.SetValue(-1, 0, 1);
            Assert.IsTrue(A.S.NumberOfDimensions == 13);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7);
            Assert.IsTrue(prodall(A) == -1);
            Assert.IsTrue(allall(abs(A) == 1));
            Assert.IsTrue(A.GetValue(0, 1) == -1);


        }
        [TestMethod]
        public void SetValueExt8() {
            Array<int> A = ones<int>(1, 2, 3, 4, 5, 6, 7);
            A.a = A.Reshape(vector<long>(1, 1, 2, 3, 4, 5, 6, -1));
            Assert.IsTrue(A.S.NumberOfDimensions == 8);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7);
            Assert.IsTrue(prodall(A) == 1);
            Assert.IsTrue(A.S[7] == 7);

            Settings.MaxNumberThreads = 4; 
            A.SetValue(-1, 0, 1);
            Assert.IsTrue(A.S.NumberOfDimensions == 8);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7);
            Assert.IsTrue(prodall(A) == -1);
            Assert.IsTrue(allall(abs(A) == 1));
            Assert.IsTrue(A.GetValue(0, 1) == -1);

            A.SetValue(-2, 0, 1, 0); // expands
            Assert.IsTrue(A.S.NumberOfDimensions == 8);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 2 * 3 * 4 * 5 * 6 * 7);
            Assert.IsTrue(prodall(A) == 0);
            Assert.IsTrue(A.GetValue(0, 1) == -2);
            Assert.IsTrue(A.GetValue(0, 0, 1) == -1);

            var c = A[full, 0, ellipsis];
            Assert.IsTrue(!object.Equals(c,null));
            var d = abs(c);

            Assert.IsTrue(!object.Equals(d,null));
            var e = d == 1;
            Assert.IsTrue(!object.Equals(e,null));
            var f = allall(e);
            Assert.IsTrue(!object.Equals(f,null));
            Assert.IsTrue(f); 
            //Assert.IsTrue(allall(abs(A[full, 0, ellipsis]) == 1));


        }
        [TestMethod]
        public void SetValueExt32() {
            Array<int> A = ones<int>(1, 2, 3, 4, 5, 6, 7);
            A.a = A.Reshape(ones<long>(32-7,1).Concat(A.shape[r(0,-2)],0).Concat(vector<long>(-2),0));

            Assert.IsTrue(A.S.NumberOfDimensions == 32);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 3 * 4 * 5 * 6 * 7);
            Assert.IsTrue(prodall(A) == 1);
            Assert.IsTrue(A.S[31] == 7);

            A.SetValue(-1, 0, 1, 0, 0, 0, 0, 0); // expands
            Assert.IsTrue(A.S.NumberOfDimensions == 32);
            Assert.IsTrue(A.S.NumberOfElements == 2 * 2 * 3 * 4 * 5 * 6 * 7);
            Assert.IsTrue(A.shape.Equals(size(1,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,3,4,5,6,7)));
            //Assert.IsTrue(allall(abs(A) == 1));
            Assert.IsTrue(A.GetValue(0, 1) == -1);

            A.SetValue(-2, size(0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1)); 
            Assert.IsTrue(A.S.NumberOfDimensions == 32);
            Assert.IsTrue(A.S.NumberOfElements ==  2 * 2 * 3 * 4 * 5 * 6 * 7);
            //Assert.IsTrue(prodall(A) == 0);
            Assert.IsTrue(A.GetValue(0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1) == -2);
            Assert.IsTrue(A.GetValue(size(0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)) == -1);
            Assert.IsTrue(A.GetValue(size(0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0)) == 0);

            //Assert.IsTrue(allall(abs(A[full, 0, ellipsis]) == 1));


        }

    }
}
