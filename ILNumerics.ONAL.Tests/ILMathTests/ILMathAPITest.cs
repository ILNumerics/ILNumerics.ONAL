// MIT License
// 
// Copyright (c) 2026 ILNumerics GmbH
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
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
