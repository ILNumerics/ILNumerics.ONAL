using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath; 

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public class numelTests {

        [TestMethod]
        public void MathInternal_numel_allTests() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = 1;
                Assert.IsTrue(numel(A) == 1);
                Assert.IsTrue(numel(ones<float>(1)) == 1);
                Assert.IsTrue(numel(ones<float>(4)) == 16, $"{numel(ones<float>(4))}");
                Assert.IsTrue(numel(ones<float>(4,1)) == 4);
                Assert.IsTrue(numel(ones<float>(4,6)) == 24);
                Assert.IsTrue(numel(ones<float>(1,4,6)) == 24);
                Assert.IsTrue(numel(ones<float>(1, 0, 6)) == 0);
                Assert.IsTrue(numel(ones<float>(0, 0, dim2: 0)) == 0);
            }
        }
    }
}
