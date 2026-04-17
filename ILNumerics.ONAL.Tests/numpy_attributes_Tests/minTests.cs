using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;

namespace ILNumerics.Core.UnitTests.numpy_attributes_Tests {

    [TestClass]
    public class minTests {
        [TestMethod]
        public void numpy_min_simple() {

            Array<double> A = counter<double>(1, 1, 5, 4, 3);
            Array<long> I = 0;
            // axis null
            Assert.IsTrue(A.min().shape.Equals(vector<long>(1,1)));
            Assert.IsTrue(A.min().Equals(vector<double>(1)));
            Assert.IsTrue(A.min(null, I).Equals(vector<double>(1)));
            Assert.IsTrue(I.shape.Equals(vector<long>(1,1)));
            Assert.IsTrue(I.Equals(vector<long>(0)));

            // axis -1
            Assert.IsTrue(A.min(-1).shape.Equals(vector<long>(5, 4)));
            Assert.IsTrue(A.min(-1).Equals(counter<double>(1, 1, 5, 4)));
            Assert.IsTrue(A.min(-1, I).Equals(counter<double>(1, 1, 5, 4)));
            Assert.IsTrue(I.shape.Equals(vector<long>(5, 4)));
            Assert.IsTrue(I.Equals(ones<long>(5, 4) * 0));

            // axis 0
            Assert.IsTrue(A.min(0).shape.Equals(vector<long>(4, 3)));
            Assert.IsTrue(A.min(0).Equals(counter<double>(1, 5, 4, 3)));
            Assert.IsTrue(A.min(0, I).Equals(counter<double>(1, 5, 4, 3)));
            Assert.IsTrue(I.shape.Equals(vector<long>(4, 3)));
            Assert.IsTrue(I.Equals(ones<long>(4, 3) * 0));

            // axis 1
            Assert.IsTrue(A.min(1).shape.Equals(vector<long>(5, 3)));
            Assert.IsTrue(A.min(1).Equals(counter<double>(1 + 0 * 3, 1, 5, 1) + counter<double>(0, 20, 1, 3)));
            Assert.IsTrue(A.min(1, I).Equals(counter<double>(1 + 0 * 3, 1, 5, 1) + counter<double>(0, 20, 1, 3)));
            Assert.IsTrue(I.shape.Equals(vector<long>(5, 3)));
            Assert.IsTrue(I.Equals(ones<long>(5, 3) * 0));

            // axis 2
            Assert.IsTrue(A.min(2).shape.Equals(vector<long>(5, 4)));
            Assert.IsTrue(A.min(2).Equals(counter<double>(1, 1, 5, 4)));
            Assert.IsTrue(A.min(2, I).Equals(counter<double>(1, 1, 5, 4)));
            Assert.IsTrue(I.shape.Equals(vector<long>(5, 4)));
            Assert.IsTrue(I.Equals(ones<long>(5, 4) * 0));

        }

    }
}
