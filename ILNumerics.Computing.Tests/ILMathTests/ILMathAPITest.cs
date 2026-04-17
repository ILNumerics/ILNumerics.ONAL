using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;

namespace ILNumerics.Core.UnitTests.ILMathTests {

    [TestClass]
    public class ILMathAPITest {
        [TestMethod]
        public void TestMethod1() {
            Array<long> L1 = 1;
            Array<long> L2 = 2; 
            Array<long> A = add(L1, L2); 
        }

        [TestMethod] 
        public void Sum0_1Test() {
            var A = vector(
                0, 0, -.05f,
                1, 0, -.05f,
                1, 1, -.05f,
                0, 1, -.05f).Reshape(3, 4, StorageOrders.ColumnMajor);

            Array<float> A2 = array(new float[,] {
                                                        {0,0,-.05f},
                                                        {1,0,-.05f},
                                                        {1,1,-.05f},
                                                        {0,1,-.05f} }).T;

            Array<int> ones = ILNumerics.Core.Functions.Builtin.MathInternal.ones<int>(2, 3, 2);
            Array<int> sum0 = ILNumerics.Core.Functions.Builtin.MathInternal.sum(ones, 2, false);
            Array<int> sum1 = ILNumerics.Core.Functions.Builtin.MathInternal.sum(ones, -1, false);
            var expectedShape = new[] { 2L, 3L };

            Array<double> arrayA = new double[,] { { 1.0, 2.0, 3.0 }, { 4.0, 5.0, 6.0 } };
            Array<double> arrayB = new double[] { 2.0, 3.0, 4.0 };
            Array<double> arrayC = new double[,] { { 2.0 }, { 3.0 }, { 4.0 } };

            using (Scope.Enter(ArrayStyles.numpy)) {
                Array<double> result1 = multiply(arrayA, arrayB);
                Array<double> result2 = multiply(arrayA, arrayB);

                Array<double> expected = new[,] { { 20.0 }, { 47.0 } };
                Assert.IsTrue(expected.Equals(result1));
                Assert.IsTrue(expected.Equals(result2));
            }

        }
        //public static void AssertArraysEqual(Array expectedArray, Array actualArray) {
        //    CollectionAssert.AreEqual(expectedArray, actualArray);
        //    Assert.AreEqual(expectedArray.Rank, actualArray.Rank);
        //    for (int i = 0; i < expectedArray.Rank; i++) {
        //        Assert.AreEqual(expectedArray.GetLength(i), actualArray.GetLength(i));
        //    }
        //}

    }
}
