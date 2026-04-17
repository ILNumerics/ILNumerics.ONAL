using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILNumerics;
using static ILNumerics.Globals;
using static ILNumerics.ILMath;

namespace ILNumerics.Core.Tests {

    [TestClass]
    public class numpyAPI_sepModule {

        [TestMethod]
        public void NumpyModuleBasicTest() {
            
            Array<int> A = 1;
            Assert.IsTrue(A.item(0) == 1); // requires ILNumerics.numpy module reference and license

            Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.ILNumericsV4); 
            using (Scope.Enter(ArrayStyles.numpy)) {
                A[ellipsis] = -2;
                A.put(size(0,0,0), -1); 
                Assert.IsTrue(A.item(0, 0) == -1); 
                Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.numpy);
            }
            Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);

        }
    }
}
