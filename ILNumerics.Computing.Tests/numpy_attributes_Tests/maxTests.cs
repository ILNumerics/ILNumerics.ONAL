using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILNumerics; 
using static ILNumerics.ILMath;

namespace ILNumerics.Core.UnitTests.numpy_attributes_Tests {

    [TestClass]
    public class maxTests {
        [TestMethod]
        public void numpy_max_simple() {

            Array<double> A = counter<double>(1, 1, 5, 4, 3);
            Array<long> I = 0;
            // axis null
            Assert.IsTrue(A.max().shape.Equals(vector<long>(1,1)));
            Assert.IsTrue(A.max().Equals(vector<double>(60)));
            Assert.IsTrue(A.max(null, I).Equals(vector<double>(60)));
            Assert.IsTrue(I.shape.Equals(vector<long>(1,1)));
            Assert.IsTrue(I.Equals(vector<long>(59)));

            // axis -1
            Assert.IsTrue(A.max(-1).shape.Equals(vector<long>(5, 4)));
            Assert.IsTrue(A.max(-1).Equals(counter<double>(41, 1, 5, 4)));
            Assert.IsTrue(A.max(-1, I).Equals(counter<double>(41, 1, 5, 4)));
            Assert.IsTrue(I.shape.Equals(vector<long>(5, 4)));
            Assert.IsTrue(I.Equals(ones<long>(5, 4) * 2));

            // axis 0
            Assert.IsTrue(A.max(0).shape.Equals(vector<long>(4, 3)));
            Assert.IsTrue(A.max(0).Equals(counter<double>(5, 5, 4, 3)));
            Assert.IsTrue(A.max(0, I).Equals(counter<double>(5, 5, 4, 3)));
            Assert.IsTrue(I.shape.Equals(vector<long>(4, 3)));
            Assert.IsTrue(I.Equals(ones<long>(4, 3) * 4));

            // axis 1
            Assert.IsTrue(A.max(1).shape.Equals(vector<long>(5, 3)));
            Assert.IsTrue(A.max(1).Equals(counter<double>(1 + 5 * 3, 1, 5, 1) + counter<double>(0, 20, 1, 3)));
            Assert.IsTrue(A.max(1, I).Equals(counter<double>(1 + 5 * 3, 1, 5, 1) + counter<double>(0, 20, 1, 3)));
            Assert.IsTrue(I.shape.Equals(vector<long>(5, 3)));
            Assert.IsTrue(I.Equals(ones<long>(5, 3) * 3));

            // axis 2
            Assert.IsTrue(A.max(2).shape.Equals(vector<long>(5, 4)));
            Assert.IsTrue(A.max(2).Equals(counter<double>(41, 1, 5, 4)));
            Assert.IsTrue(A.max(2, I).Equals(counter<double>(41, 1, 5, 4)));
            Assert.IsTrue(I.shape.Equals(vector<long>(5, 4)));
            Assert.IsTrue(I.Equals(ones<long>(5, 4) * 2));

        }

    }
}
