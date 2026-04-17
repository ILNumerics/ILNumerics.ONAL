using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;
using ILNumerics.Core.Functions.Builtin;

namespace ILNumerics.Core.UnitTests.numpy_attributes_Tests {

    [TestClass]
    public class transposeTests {
        [TestMethod]
        public void numpy_transpose_simple() {

            Array<double> A = counter<double>(1, 1, 5, 4, 3);

            A.a = A.transpose(MathInternal.vector<long>(0,2,1));
            Assert.IsTrue(A.S[0] == 5);
            Assert.IsTrue(A.S[1] == 3);
            Assert.IsTrue(A.S[2] == 4);
            Assert.IsTrue(A.S.NumberOfDimensions == 3);
            Assert.IsTrue(A.S.NumberOfElements == 5* 4* 3);

        }
        [TestMethod]
        public void numpy_transpose_RM() {

            Array<double> A = counter<double>(1, 1, 5, 4, 3, StorageOrders.RowMajor);
            Array<fcomplex> B;
            A.a = A.transpose(MathInternal.vector<long>(0, 2, 1));
            Assert.IsTrue(A.S[0] == 5);
            Assert.IsTrue(A.S[1] == 3);
            Assert.IsTrue(A.S[2] == 4);
            Assert.IsTrue(A.S.NumberOfDimensions == 3);
            Assert.IsTrue(A.S.NumberOfElements == 5 * 4 * 3);

        }

        [TestMethod]
        public void numpy_transpose_empty() {

            Array<double> A = counter<double>(1, 1, 5, 1, 0, 2, StorageOrders.RowMajor);
            Array<double> B = A.transpose(MathInternal.vector<long>(3,2,1,0));

            Assert.IsTrue(B.Equals(A.transpose()));
            Assert.IsTrue(B.S.NumberOfDimensions == 4);
            Assert.IsTrue(B.IsEmpty);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void numpy_transpose_unmatching_dimensionCount() {

            Array<double> A = ones<double>(10, 4, 3);
            A.transpose(vector<long>(1, 0, 2, 3));
 
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void numpy_transpose_duplicate_dimensions() {

            Array<double> A = ones<double>(10, 4, 3);
            A.transpose(vector<long>(1, 0, 1));

        }

    }
}
