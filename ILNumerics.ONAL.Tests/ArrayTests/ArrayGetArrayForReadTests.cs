using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public class ArrayGetArrayForReadTests {

        [TestMethod]
        public void ArrayGetArrayForReadTestempty() {
            Array<short> A = new short[0];

            var arr = A.GetArrayForRead();

            Assert.IsTrue(arr != null);
            Assert.IsTrue(arr.Rank == 1);
            Assert.IsTrue(arr.LongLength == 0); 


        }

        [TestMethod]
        public void ArrayGetArrayForReadTestColumnMajor() {

            float[] cols = new float[0], rows = null; 
            Array<float> A = Helper.generateSystemArray<float>(500,203, ref rows, ref cols);
            
            var arr = A.GetArrayForRead();

            ArrayAssert.ValuesEqual(arr, cols); 

            Assert.IsTrue(arr != null);
            Assert.IsTrue(arr.Rank == 1);
            Assert.IsTrue(arr.LongLength == A.S.NumberOfElements);

        }

    }
}
