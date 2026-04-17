using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILNumerics; 
using static ILNumerics.Globals;
using static ILNumerics.ILMath;
using ILNumerics.Core.Internal;

namespace Core_Tests_small {

    [TestClass]
    public class SpanToFromArrayTests {

        [TestMethod]
        public void Array2SpanSimple() {
            Array<uint> A = new uint[] { 0, 1, 2, 3 };

            var span = A.AsSpan(); 

            Assert.IsTrue(span.Length == 4);
            Assert.IsTrue(span[0] == 0);
            Assert.IsTrue(span[1] == 1);
            Assert.IsTrue(span[2] == 2);
            Assert.IsTrue(span[3] == 3);

        }

        [TestMethod]
        public void Array2SpanRowMajorTest() {
            Array<double> A = counter<double>(0.0, 1.0, 3, 4, StorageOrders.RowMajor);

            var span = A.AsSpan(order: StorageOrders.RowMajor);

            Assert.IsTrue(span.Length == 12);
            Assert.IsTrue(span[0] == 0); Assert.IsTrue(span[1] == 1); Assert.IsTrue(span[2] == 2); Assert.IsTrue(span[3] == 3);
            Assert.IsTrue(span[4] == 4); Assert.IsTrue(span[5] == 5); Assert.IsTrue(span[6] == 6); Assert.IsTrue(span[7] == 7);
            Assert.IsTrue(span[8] == 8); Assert.IsTrue(span[9] == 9); Assert.IsTrue(span[10] == 10); Assert.IsTrue(span[11] == 11);

        }
        [TestMethod]
        public void Array2SpanColMajorTest() {
            Array<double> A = counter<double>(0.0, 1.0, 3, 4, StorageOrders.RowMajor);

            var span = A.AsSpan(order: StorageOrders.ColumnMajor);

            Assert.IsTrue(span.Length == 12);
            Assert.IsTrue(span[0] == 0); Assert.IsTrue(span[3] == 1); Assert.IsTrue(span[6] == 2); Assert.IsTrue(span[9] == 3);
            Assert.IsTrue(span[1] == 4); Assert.IsTrue(span[4] == 5); Assert.IsTrue(span[7] == 6); Assert.IsTrue(span[10] == 7);
            Assert.IsTrue(span[2] == 8); Assert.IsTrue(span[5] == 9); Assert.IsTrue(span[8] == 10); Assert.IsTrue(span[11] == 11);

        }

        [TestMethod]
        public unsafe void Span2ArraySimpleTest() {

            Span<double> spanS = stackalloc double[4] { 0, 1, 2, 3 }; 
            
            Array<double> A = vector<double>(spanS);

            Assert.IsTrue(A.Length == 4);
            Assert.IsTrue(A[0] == 0); 
            Assert.IsTrue(A[1] == 1); 
            Assert.IsTrue(A[2] == 2); 
            Assert.IsTrue(A[3] == 3); 

        }


    }
}
