using ILNumerics;
using ILNumerics.Core.Internal;
using static ILNumerics.Globals;
using static ILNumerics.ILMath; 

namespace ILNumerics.Computing.Tests {

    [TestClass]
    public sealed class ArrayTests {

        [TestMethod]
        public void ArrayCreationTest001() {
            
            var A = vector(1.0, 2.0, 3.0);  
            Array<double> B = A + 100;
            B[1] = 102.8;
            Array<double> C = B - A;
            Array<double> Res = new double[] { 100, 100.8, 100 };
            var msg = $"Res: {Res}, C: {C}"; 
            Assert.IsTrue(C.Equals(Res), msg);

        }
    }
}
