using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;
using ILNumerics.Core.Functions.Builtin;

namespace ILNumerics.Core.UnitTests.numpy_attributes_Tests {

    [TestClass]
    public class resizeTests {
        [TestMethod]
        public void numpy_resize_simple() {

            Array<double> A = counter<double>(1, 1, 5, 4, 3);

            A.resize(MathInternal.vector<long>(6, 4, 3));
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);

            Assert.IsTrue(all(A.shape == MathInternal.vector<long>(6, 4, 3)));
            Assert.IsTrue(A["end"] == 0);
            Assert.IsTrue(allall(A[":", "-2:-1", -1] == 0));
            
        }
        [TestMethod]
        public void numpy_resize_RM() {

            Array<double> A = counter<double>(1, 1, 5, 4, 3, StorageOrders.RowMajor);

            A.resize(MathInternal.vector<long>(6, 4, 3));
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor);

            Assert.IsTrue(all(A.shape == MathInternal.vector<long>(6, 4, 3)));
            Assert.IsTrue(A[-1] == 0);
            Assert.IsTrue(allall(A[-1,":", ":"] == 0));

        }

        [TestMethod]
        public void numpy_resize_2_empty() {
            Array<double> A = counter<double>(1, 1, 5, 4, 3, StorageOrders.RowMajor);
            A.resize(MathInternal.empty<long>(0));

            Assert.IsTrue(A.IsScalar);
            Assert.IsTrue(A.S.NumberOfDimensions == Math.Max(0, Settings.MinNumberOfArrayDimensions));
            Assert.IsTrue(A.GetValue(0) == 1);

        }
        [TestMethod]
        public void numpy_resize_from_empty() {

            Array<double> A = empty<double>(1, 0, 3); 
            A.resize(MathInternal.vector<long>(10,20));

            Assert.IsTrue(A.S[0] == 10);
            Assert.IsTrue(A.S[1] == 20);
            Assert.IsTrue(A.S.NumberOfDimensions == 2);
            Assert.IsTrue(allall(A == 0));

        }

    }
}
