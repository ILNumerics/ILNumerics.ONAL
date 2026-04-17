using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;

namespace ILNumerics.Core.UnitTests.numpy_attributes_Tests {

    [TestClass]
    public class conjTests {

        [TestMethod]
        public void numpy_conj_simple() {

            Array<complex> A = ccomplex(ones<double>(5, 4, 3), counter<double>(1, 1, 5, 4, 3));

            A.conj();

            Assert.IsTrue(A.Equals(ccomplex(ones<double>(5, 4, 3), -counter<double>(1, 1, 5, 4, 3))));
        }

        [TestMethod]
        public void numpy_conj_strided() {

            Array<complex> A = ccomplex(ones<double>(5, 4, 3), counter<double>(1, 1, 5, 4, 3));
            A.a = A[Globals.ellipsis, Globals.r(0,2,Globals.end)]; 
            Assert.IsTrue(A.strides.Equals(vector<long>(1, 5, 20 * 2))); 
            Assert.IsTrue(A.shape.Equals(vector<long>(5, 4, 2)));
            A.conj();
            Array<double> real = counter<double>(1, 1, 5, 4, 1) + counter<double>(0, 40, 1, 1, 2);
            Array<complex> R = ccomplex(ones<double>(5, 4, 2), -real);
 
            if (!A.Equals(R)) {

                Assert.IsTrue(false, $"A={A} R={R} diff={maxall(abs(A - R))}");
            }
        }

    }
}
