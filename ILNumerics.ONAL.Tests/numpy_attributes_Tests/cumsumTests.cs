using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;
using ILNumerics.Core.Functions.Builtin;

namespace ILNumerics.Core.UnitTests.numpy_attributes_Tests {

    [TestClass]
    public class cumsumTests {

        [TestMethod]
        public void numpy_cumsum_simple() {

            Array<double> A = counter<double>(1, 1, 5, 4, 3);
            Array<double> B = A.cumsum();

            Assert.IsTrue(B.Equals(MathInternal.cumsum(A.flatten())));
            Assert.IsTrue(B.IsVector);
            Assert.IsTrue(B.Length == A.S.NumberOfElements); 

        }

        [TestMethod]
        public void numpy_cumsum_empty_givesScalar() {

            Array<float> A = empty<float>(0, 3, 1);
            Array<float> B = A.cumsum(2);

            Assert.IsTrue(A.cumsum(0).shape.Equals(vector<long>(0, 3, 1))); 
            Assert.IsTrue(A.cumsum(1).shape.Equals(vector<long>(0, 3, 1))); 
            Assert.IsTrue(A.cumsum(2).shape.Equals(vector<long>(0, 3, 1))); 
        }



    }
}
