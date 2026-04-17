using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public class ArrayShortInfoTests {

        [TestMethod]
        public void ArrayShortInfoTestempty() {

            Array<int> A = new int[0];

            Assert.IsTrue(A.ShortInfo().StartsWith("<Int32> [0,1]"));
        }
        [TestMethod]
        public void ArrayShortInfoTestScalar() {

            Array<double> A = new double[] { 1.2345 };

            Assert.IsTrue(A.ShortInfo().StartsWith($"<Double> [1,1] {A.GetValue(0).ToString()}"));
        }

        [TestMethod]
        public void ArrayShortInfoTestScalar2() {

            Array<double> A = new double[,,] { { { 1.2345 } } };

            Assert.IsTrue(A.ShortInfo().StartsWith($"<Double> [1,1,1] {A.GetValue(0).ToString()}"));
        }

        [TestMethod]
        public void ArrayShortInfoTestVector2() {

            Array<double> A = new double[] { 1.2345, -6.543210001 };

            Assert.IsTrue(A.ShortInfo().StartsWith($"<Double> [2,1] {A.GetValue(0).ToString()}, {A.GetValue(-1).ToString()}"));
        }
        [TestMethod]
        public void ArrayShortInfoTestVector3i4() {

            Array<float> A = new float[,] { { 1.2345f, -6.543210001f, 332.34f } };
            var s = A.ShortInfo();
            Assert.IsTrue(s.StartsWith($"<Single> [1,3] {A.GetValue(0).ToString()}...{A.GetValue(-1).ToString()}"));

            A.a = new float[,] { { 1.2345f, -6.543210001f, 332.34f, .00044444f } };
            s = A.ShortInfo();
            Assert.IsTrue(s.StartsWith($"<Single> [1,4] {A.GetValue(0).ToString()}...{A.GetValue(-1).ToString()}"));
        }
    }
}
