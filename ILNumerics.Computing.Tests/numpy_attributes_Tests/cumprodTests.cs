using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;
using ILNumerics.Core.Functions.Builtin;

namespace ILNumerics.Core.UnitTests.numpy_attributes_Tests {

    [TestClass]
    public class cumprodTests {

        [TestMethod]
        public void numpy_cumprod_simple() {

            Array<double> A = counter<double>(1, 1, 5, 4, 3);
            Array<double> B = A.cumprod();

            Assert.IsTrue(B.Equals(MathInternal.cumprod(A.flatten())));
            Assert.IsTrue(B.IsVector);
            Assert.IsTrue(B.Length == A.S.NumberOfElements); 

        }

        [TestMethod]
        public void numpy_cumprod_empty_givesScalar() {

            Array<float> A = empty<float>(0, 3, 1);
            Array<float> B = A.cumprod(2);

            Assert.IsTrue(A.cumprod(0).shape.Equals(vector<long>(0, 3, 1))); 
            Assert.IsTrue(A.cumprod(1).shape.Equals(vector<long>(0, 3, 1))); 
            Assert.IsTrue(A.cumprod(2).shape.Equals(vector<long>(0, 3, 1))); 
        }



    }
}
