using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILNumerics;
using static ILNumerics.Globals;
using static ILNumerics.ILMath;

namespace ILNumerics.Core.Tests {
    [TestClass]
    public class Size_params {

        [TestMethod]
        public void sizeOverloadResolution32() {

            using (Scope.Enter(ArrayStyles.numpy)) {
                // check the content 
                Array<long> A = size();
                Assert.IsTrue(A.Equals(empty<long>(0)));
                Assert.IsTrue(size(1).Equals(vector<long>(1)));
                Assert.IsTrue(size(1, 2).Equals(vector<long>(1, 2)));
                Assert.IsTrue(size(1, 2, 3).Equals(vector<long>(1, 2, 3)));
                Assert.IsTrue(size(1, 2, 3, 4).Equals(vector<long>(1, 2, 3, 4)));

                Assert.IsTrue(size(1, 2, 3, 4, 1, 2, 3).Equals(vector<long>(1, 2, 3, 4, 1, 2, 3))); // -> last explicit long,long,...
                Assert.IsTrue(size(1, 2, 3, 4, 1, 2, 3, 4).Equals(vector<long>(1, 2, 3, 4, 1, 2, 3, 4))); // -> first params[]

                Assert.IsTrue(size(1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4)
                    .Equals(vector<long>(1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4))); // -> first params[]

                // checks the shape. ( this is redundant, no?)
                Assert.IsTrue(size().shape.Equals(vector<long>(0)));
                Assert.IsTrue(size(1).shape.Equals(vector<long>(1)));
                Assert.IsTrue(size(1, 2).shape.Equals(vector<long>(2)));
                Assert.IsTrue(size(1, 2, 3).shape.Equals(vector<long>(3)));
                Assert.IsTrue(size(1, 2, 3, 4).shape.Equals(vector<long>(4)));

                Assert.IsTrue(size(1, 2, 3, 4, 1, 2, 3).shape.Equals(vector<long>(7))); // -> last explicit long,long,...
                Assert.IsTrue(size(1, 2, 3, 4, 1, 2, 3, 4).shape.Equals(vector<long>(8))); // -> first params[]

                Assert.IsTrue(size(1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4)
                    .shape.Equals(vector<long>(32))); // -> last allowed dim length

            }

        }
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        [TestMethod]
        public void SizeDimsNotNull() {

            size(null); 
        }

        public static Array<double> Dot1(InArray<double> left, InArray<double> right) {
            using (Scope.Enter(left, right)) {
                return sumall(left[full] * right[full]);
            }
        }
        public static Array<double> Dot2(InArray<double> left, InArray<double> right) {
            using (Scope.Enter(left, right)) {
                return multiply(left[full].T, right[full]);
            }
        }
        public static Array<double> Dot3(InArray<double> left, InArray<double> right) {
            using (Scope.Enter(left, right)) {
                return sum(left[full] * right[full]);
            }
        }
        public static Array<double> Dot4(InArray<double> left, InArray<double> right) {
            using (Scope.Enter(left, right)) {
                var firstNonZeroDimenson = (long)find(left.shape != 0); // works only if non-empty
                // do also consider: 
                var firstNonZeroAlt = left.S.WorkingDimension();
                return sum(left[full] * right[full], (int)firstNonZeroDimenson);
            }
        }

    }
}
