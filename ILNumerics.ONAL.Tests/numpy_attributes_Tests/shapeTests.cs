using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.Globals;
using static ILNumerics.ILMath; 

namespace ILNumerics.Core.UnitTests.numpy_attributes_Tests {
    [TestClass]
    public class shapeTests {
        [TestMethod]
        public void numpy_shape_getSimple() {

            Array<double> A = counter(1.0, 1.0, 5,4,3);

            Array<long> S = A.shape;
            Assert.IsTrue(S.Equals(vector<long>(5, 4, 3))); 

        }

        [TestMethod]
        public void numpy_shape_getScalar() {
            using (Settings.Ensure(()=> Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<ulong> A = 1;
                Assert.IsTrue(A.S.NumberOfDimensions == 0 && A.S.NumberOfElements == 1);

                Array<long> S = A.shape;
                // shape always gives a vector
                Assert.IsTrue(S.S.NumberOfElements == 0);
                Assert.IsTrue(S.S.NumberOfDimensions == 1);

            }

        }

        [TestMethod]
        public void numpy_shape_setSimple() {

            Array<int> A = counter<int>(1, 1, 6, 5, 4);
            Assert.IsTrue(all(A.shape == vector<long>(6, 5, 4))); 
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(A.S.GetStride(0) == 1);
            Assert.IsTrue(A.S.GetStride(1) == 6);
            Assert.IsTrue(A.S.GetStride(2) == 30);
            Assert.IsTrue(A.S.NumberOfDimensions == 3);

            A.shape = vector<long>(5, 4, 6);
            Assert.IsTrue(all(A.shape == vector<long>(5, 4, 6)));
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(A.S.GetStride(0) == 1);
            Assert.IsTrue(A.S.GetStride(1) == 5);
            Assert.IsTrue(A.S.GetStride(2) == 20);
            Assert.IsTrue(A.S.NumberOfDimensions == 3);

            A.shape = vector<long>(5, 24);
            Assert.IsTrue(all(A.shape == vector<long>(5, 24)));
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(A.S.GetStride(0) == 1);
            Assert.IsTrue(A.S.GetStride(1) == 5);
            Assert.IsTrue(A.S.GetStride(2) == 0);
            Assert.IsTrue(A.S.NumberOfDimensions == 2);

            A.shape = vector<long>(5 * 24);
            Assert.IsTrue(all(A.shape == vector<long>(5 * 24, 1)));
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(A.S.GetStride(0) == 1);
            Assert.IsTrue(A.S.GetStride(1) == 0);
            Assert.IsTrue(A.S.GetStride(2) == 0);
            Assert.IsTrue(A.S.NumberOfDimensions == 2); // ML mode

        }
        [TestMethod]
        public void numpy_shape_setRowMajor() {

            using (Settings.Ensure(()=> Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<int> A = counter<int>(1, 1, 6, 5, 4, StorageOrders.RowMajor);
                Assert.IsTrue(all(A.shape == vector<long>(6, 5, 4)));
                Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor);
                Assert.IsTrue(A.S.GetStride(0) == 20);
                Assert.IsTrue(A.S.GetStride(1) == 4);
                Assert.IsTrue(A.S.GetStride(2) == 1);
                Assert.IsTrue(A.S.NumberOfDimensions == 3);

                A.shape = vector<long>(5, 4, 6);
                Assert.IsTrue(all(A.shape == vector<long>(5, 4, 6)));
                Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor);
                Assert.IsTrue(A.S.GetStride(0) == 24);
                Assert.IsTrue(A.S.GetStride(1) == 6);
                Assert.IsTrue(A.S.GetStride(2) == 1);
                Assert.IsTrue(A.S.NumberOfDimensions == 3);

                A.shape = vector<long>(5, 24);
                Assert.IsTrue(all(A.shape == vector<long>(5, 24)));
                Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor);
                Assert.IsTrue(A.S.GetStride(0) == 24);
                Assert.IsTrue(A.S.GetStride(1) == 1);
                Assert.IsTrue(A.S.GetStride(2) == 0);
                Assert.IsTrue(A.S.NumberOfDimensions == 2);

                A.shape = vector<long>(5 * 24);
                Assert.IsTrue(all(A.shape == vector<long>(5 * 24)));
                //Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor); // both: column AND rowmajor
                Assert.IsTrue(A.S.GetStride(0) == 1);
                Assert.IsTrue(A.S.GetStride(1) == 0);
                Assert.IsTrue(A.S.GetStride(2) == 0);
                Assert.IsTrue(A.S.NumberOfDimensions == 1); // numpy mode

                // widening the shape
                A = 1;
                Assert.IsTrue(A.IsScalar && A.S.NumberOfDimensions == 0);
                // -> 3 singleton dimensions
                A.shape = ones<long>(3,dim1: 1);
                Assert.IsTrue(all(A.shape == vector<long>(1, 1, 1)));
                // Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor); // scalars are column major?
                Assert.IsTrue(A.S.GetStride(0) == 0);
                Assert.IsTrue(A.S.GetStride(1) == 0);
                Assert.IsTrue(A.S.GetStride(2) == 0);
                Assert.IsTrue(A.S.GetStride(3) == 0);
                Assert.IsTrue(A.S.NumberOfDimensions == 3); // numpy mode

                // shorten the shape -> make scalar array
                Array<long> I = 1; 
                A.shape = empty<long>(0);

                Assert.IsFalse(all(A.shape == empty<long>(0)));
                Assert.IsTrue(A.shape.S.NumberOfDimensions == 1);
                Assert.IsTrue(A.shape.S.NumberOfElements == 0);
                Assert.IsFalse(A.shape == 0); // (kept for checking empty broadcasting only)

                Assert.IsTrue(A.shape.IsEmpty); 
                Assert.IsTrue(A.shape.IsVector);
                Assert.IsTrue(A.shape.ndim == 1);
                Assert.IsTrue(A.shape.size_ == 0);
                //Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor);
                Assert.IsTrue(A.S.GetStride(0) == 0);
                Assert.IsTrue(A.S.GetStride(1) == 0);
                Assert.IsTrue(A.S.GetStride(2) == 0);
                Assert.IsTrue(A.S.GetStride(3) == 0);
                Assert.IsTrue(A.S.NumberOfDimensions == 0); // numpy mode
                Assert.IsTrue(A.S.NumberOfElements == 1); // numpy mode

            }
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void numpy_shape_setNonContinFail() {
            Array<uint> A = counter<uint>(1, 1, 4, 3);
            A.a = A["1:2:", ":"]; // now: 2x3
            A.shape = vector<long>(3, 2);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void numpy_shape_setNegativFail() {
            Array<uint> A = counter<uint>(1, 1, 4, 3);
            A.shape = vector<long>(-12, -1);
        }
        [TestMethod]
        public void numpy_shape_setFailRecover() {
            Array<int> A = ones<int>(5, 4); 
            try {
                A.shape = vector<long>(10, 4); // wrong nr. of elements
            } catch (ArgumentException) {
            }
            // A is still correct
            Assert.IsTrue(all(A.shape == vector<long>(5, 4))); 
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void numpy_shape_setNrElementChangeFail() {
            Array<int> A = ones<int>(5, 4);
            A.shape = vector<long>(10, 4); // wrong nr. of elements
        }
        [TestMethod]
        public void numpy_shape_setSharedNoFail() {
            using (Scope.Enter(ArrayStyles.numpy)) {
                Array<int> A = counter<int>(1, 1, 5, 4);
                InArray<int> InA = A;  // creates a Clone() of A
 
                A.shape = vector<long>(10, 2);
                Assert.IsTrue(A.Equals(counter<int>(1, 1, 10, 2)));
            }
            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {
                Array<int> A = counter<int>(1, 1, 5, 4);
                InArray<int> InA = A;
                A.shape = vector<long>(10, 2);
                Assert.IsTrue(A.Equals(counter<int>(1, 1, 10, 2)));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void numpy_shape_setTooManyDimsFail() {
            Array<int> A = ones<int>(10,2);

            A.shape = vector<long>(5, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1);
        }

    }
}
