using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;

namespace ILNumerics.Core.UnitTests.numpy_attributes_Tests {
    [TestClass]
    public class numpy_attributes2_Tests {
        [TestMethod]
        public void numpy_strides_Test() {

            Assert.IsTrue(counter(1.0, 1.0, 5, 4, 3).strides.Equals(vector<long>(1, 5, 20))); 
            Assert.IsTrue(counter(1.0, 1.0, 5, 4, 3, StorageOrders.RowMajor).strides.Equals(vector<long>(12, 3, 1))); 

        }

        [TestMethod]
        public void numpy_size_Test() {

            Assert.IsTrue(counter(1.0, 1.0, 5, 4, 3).size_ == prod(vector<long>(5,4,3)));

            //Array<double> Ar = counter<double>(1.0, 1.0, 5, 4, 3); 
            //Array<complex> A = tocomplex(Ar);
            //Array<double> R = A.real();


        }

        [TestMethod]
        public void numpy_ndim_Test() {

            Array<double> A = counter(1.0, 1.0, 5, 4, 3);
            Assert.IsTrue(A.ndim == 3);    // mutable
            Assert.IsTrue(A.T.ndim == 3);  // immutable

        }

        [TestMethod]
        public void numpy_itemsize_Test() {

            Array<double> A = counter(1.0, 1.0, 5, 4, 3);
            Assert.IsTrue(A.itemsize == 8);    // mutable
            var Ret = A.T;
            var sto = Ret.Storage; 
            Assert.IsTrue(Ret.itemsize == 8);  // immutable


        }


    }
}
