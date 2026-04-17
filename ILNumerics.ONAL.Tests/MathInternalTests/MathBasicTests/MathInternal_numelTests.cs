using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath; 

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public class lengthTests {

        [TestMethod]
        public void MathInternal_length_allTests() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = 1;
                Assert.IsTrue(length(A) == 1);
                Assert.IsTrue(length(ones<float>(1)) == 1);
                Assert.IsTrue(length(ones<float>(4)) == 4);
                Assert.IsTrue(length(ones<float>(4,1)) == 4);
                Assert.IsTrue(length(ones<float>(4,6)) == 6);
                Assert.IsTrue(length(ones<float>(1,4,6)) == 6);
                Assert.IsTrue(length(ones<float>(1, 0, 6)) == 6);
                Assert.IsTrue(length(ones<float>(0, 0, dim2: 0)) == 0);
            }
        }
    }
}
