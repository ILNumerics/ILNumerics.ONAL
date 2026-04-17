using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;
using static ILNumerics.Globals; 

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public class ExpandTests {
        [TestMethod]
        public void Expand2DSimpleTest() {
            Array<double> A = counter(1.0, 1.0, 4, 3);
            A.Storage.Expand(new[] { 6L, 3 });

            Assert.IsTrue(A.S[0] == 6, $"A.S[0] was: " + A.S[0]);
            Assert.IsTrue(A.S[1] == 3, $"A.S[1] was: " + A.S[1]);

            Assert.IsTrue(A[r(0, 3), r(0, 2)].Equals(counter(1.0, 1.0, 4, 3)));
            Assert.IsTrue(A[r(4, end), full].Equals(counter(0.0, 0.0, 2, 3)));

        }
        [TestMethod]
        public void Expand2DFromVectorNPTest() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = counter(1.0, 1.0, 4);

                using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {
                    A.SetValue(0, new[] { 5L, 2 });

                    Assert.IsTrue(A.S[0] == 6);
                    Assert.IsTrue(A.S[1] == 3);

                    Assert.IsTrue(A[r(0, 3), r(0, 0)].Equals(counter(1.0, 1.0, 4)));
                    Assert.IsTrue(A[r(4, end), full].Equals(counter(0.0, 0.0, 2, 3)));
                    Assert.IsTrue(A[full, r(1, end)].Equals(counter(0.0, 0.0, 6, 2)));

                }

            }
        }
        [TestMethod]
        public void Expand2DFrom3DTest() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = counter(1.0, 1.0, 4, 3, 2);
                
                A.SetValue(0, new[] { 5L, 2 });

                Assert.IsTrue(A.S[0] == 6);
                Assert.IsTrue(A.S[1] == 3);
                Assert.IsTrue(A.S[2] == 2);

                Assert.IsTrue(A[r(0, 3), r(0, 0)].Equals(counter(1.0, 1.0, 4)));
                Assert.IsTrue(A[r(4, end), ellipsis].Equals(counter(0.0, 0.0, 2, 3, 2)));

            }
        }
        [TestMethod]
        public void Expand3DFrom3DTest() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<double> A = counter(1.0, 1.0, 4, 3);
 
                A.Storage.Expand(new[] { 6L, 5, 2 });

                Assert.IsTrue(A.S[0] == 6);
                Assert.IsTrue(A.S[1] == 5);
                Assert.IsTrue(A.S[2] == 2);

                Assert.IsTrue(A[r(0, 3), r(0, 2)].Equals(counter(1.0, 1.0, 4, 3)));
                Assert.IsTrue(A[r(4,end), full, 0].Equals(counter(0.0, 0.0, 2, 5, 1)));
                Assert.IsTrue(A[full, r(3, -1)].Equals(counter(0.0, 0.0, 6, 7)));

            }
        }
    }
}
