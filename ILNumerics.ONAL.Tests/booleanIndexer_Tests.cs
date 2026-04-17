using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILNumerics; 
using static ILNumerics.Globals;
using static ILNumerics.ILMath;

namespace Core_Tests_small {

    [TestClass]
    public class booleanIndexer_Tests {

        [TestMethod]
        public void booleanIndexer_simple() {
            Array<int> A = new[,] {
                {1,2,3,4 },
                {-1,-2,-3,-4 },
                {10,20,30,40 },
                {-11,-22,-33,-44 }
                }; 
            Logical L = new[] { false, true, false, true };
            Array<int> B = A[L, full];

            Array<int> Res = vector<int>(-1, -11, -2, -22, -3, -33, -4, -44).Reshape(size(2, 4), StorageOrders.ColumnMajor); 
            Assert.IsTrue(B.Equals(Res));
        }
    }
}
