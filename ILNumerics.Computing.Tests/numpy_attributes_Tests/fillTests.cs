using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;

namespace ILNumerics.Core.UnitTests.numpy_attributes_Tests {

    [TestClass]
    public class fillTests {

        [TestMethod]
        public void numpy_fill_simple() {
            Array<float> A = zeros<float>(5, 6);
            A.fill(1.0f);

            Assert.IsTrue(A.Equals(ones<float>(5, 6)));

        }

        [TestMethod]
        public void numpy_fill_empty() {
            Array<float> A = empty<float>();
            A.fill(1.0f);

            Assert.IsTrue(A.IsEmpty);

        }

    }
}
