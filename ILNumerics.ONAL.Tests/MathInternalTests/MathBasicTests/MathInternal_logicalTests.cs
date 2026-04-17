using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath; 

namespace ILNumerics.Core.UnitTests {

    [TestClass]
    public class logicalTests {

        [TestMethod]
        public void MathInternal_logical_allTests() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Assert.IsTrue(logical(new[] { true }, vector(1L)) == true); 

                Assert.IsTrue(logical(new[] { true }, size(1)) == true);
                Assert.IsTrue(logical(new[] { false }, size(1)) == false);
                Assert.IsTrue(logical(new[] { false }, size(1)).S.NumberOfDimensions == 1);

                Assert.IsTrue(allall(  logical(new[] { true, false, true }, size(1,3,1)) == (vector<int>(1, 0, 1).Reshape(size(1,3,1)) == 1)  ));

            }
        }
    }
}
