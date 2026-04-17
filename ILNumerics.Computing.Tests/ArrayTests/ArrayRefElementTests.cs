using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ILNumerics.Core.UnitTests {

    class RefTest { }

    [TestClass]
    public class ArrayRefElementTests {


        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void ArrayIsStateTestMatrixClassFail() {

            Array<RefTest> A = new RefTest[3, 2];
 

        }

    }
}
