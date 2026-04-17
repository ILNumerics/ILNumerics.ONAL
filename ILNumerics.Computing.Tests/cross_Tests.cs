using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILNumerics; 
using static ILNumerics.Globals;
using static ILNumerics.ILMath;

namespace Core_Tests_small {

    [TestClass]
    public class cross_Tests {
        [TestMethod]
        public void cross_simple() {

            Array<double> A = vector(0.0, 0, 1) * ones(3, 4, 5);
            Array<double> B = vector(0.0, 1, 0) * ones(3, 4, 5);

            Array<double> C = cross(A, B); 

            Array<double> Res = vector(-1.0, 0, 0) * ones(3, 4, 5);

            Array<double> Af = vector(0.0, 0, 1), Bf = vector(0.0, 1, 0), Cf = vector(-1.0, 0, 0);
            
            Assert.IsTrue(cross(Af * ones(3, 4, 5), Bf * ones(3, 4, 5)).Equals(Cf * ones(3, 4, 5))); 
            Assert.IsTrue(cross(Af.T * ones(4, 3, 5), Bf.T * ones(4, 3, 5)).Equals(Cf.T * ones(4, 3, 5)));
            using (Scope.Enter(ArrayStyles.numpy)) {
                Assert.IsTrue(cross(Af[newaxis] * ones(4, 3, 5), Bf[newaxis] * ones(4, 3, 5)).Equals(Cf[newaxis] * ones(4, 3, 5)));
                Assert.IsTrue(cross(Af.T[newaxis] * ones(4, 5, 3), Bf.T[newaxis] * ones(4, 5, 3)).Equals(Cf.T[newaxis] * ones(4, 5, 3)));
            }
        }

        [TestMethod]
        public void cross_simpleDim() {

            Array<double> A = vector(0.0, 0, 1) * ones(3, 4, 5);
            Array<double> B = vector(0.0, 1, 0) * ones(3, 4, 5);

            Array<double> C = cross(A, B, dimension: 0);

            Array<double> Res = vector(-1.0, 0, 0) * ones(3, 4, 5);

            Array<double> Af = vector(0.0, 0, 1), Bf = vector(0.0, 1, 0), Cf = vector(-1.0, 0, 0);

            Assert.IsTrue(cross(Af * ones(3, 4, 5), Bf * ones(3, 4, 5), dimension: 0).Equals(Cf * ones(3, 4, 5)));
            Assert.IsTrue(cross(Af.T * ones(4, 3, 5), Bf.T * ones(4, 3, 5), dimension: 1).Equals(Cf.T * ones(4, 3, 5)));
            using (Scope.Enter(ArrayStyles.numpy)) {
                Assert.IsTrue(cross(Af[newaxis] * ones(4, 3, 5), Bf[newaxis] * ones(4, 3, 5), dimension: 1).Equals(Cf[newaxis] * ones(4, 3, 5)));
                Assert.IsTrue(cross(Af.T[newaxis] * ones(4, 5, 3), Bf.T[newaxis] * ones(4, 5, 3), dimension: 2).Equals(Cf.T[newaxis] * ones(4, 5, 3)));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void cross_invalidDimNon3Fails() {

            cross(ones(3, 2, 5), ones(3, 2, 5), dimension: 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void cross_invalidDimNo3Fails() {

            Array<double> A = cross(ones(2, 2, 1), ones(2, 2, 1));
 
        }

    }
}
