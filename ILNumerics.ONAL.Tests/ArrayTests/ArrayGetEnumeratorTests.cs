using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public class ArrayGetEnumeratorTests {

        [TestMethod]
        public void ArrayGetEnumeratorTest() {

            double[] rowVals = new double[0], colVals = new double[0];
            Array<double> A = Helper.generateSystemArray<double>(1000, 1000, ref rowVals, ref colVals);
            ulong i = 0;
            Stopwatch sw = Stopwatch.StartNew();
            foreach (var e in A.Iterator(StorageOrders.RowMajor)) {
                Assert.IsTrue(rowVals[i++] == e);
            }
            System.Diagnostics.Trace.WriteLine($"Row Iterator: {sw.ElapsedMilliseconds}ms");
            Assert.IsTrue(i == (ulong)rowVals.LongLength);
            i = 0;
            sw.Restart();
            foreach (var e in A.Iterator(StorageOrders.ColumnMajor)) {
                Assert.IsTrue(colVals[i++] == e);
            }
            System.Diagnostics.Trace.WriteLine($"Column Iterator: {sw.ElapsedMilliseconds}ms");
            Assert.IsTrue(i == (ulong)rowVals.LongLength);
        }
        [TestMethod]
        public void ArrayGetEnumeratorDisposeTest() {

            double[] rowVals = new double[0], colVals = new double[0];
            Array<double> A = Helper.generateSystemArrayDouble(30, 30, ref rowVals, ref colVals);
            ulong i = 0;
            var AR = A.C; 
            foreach (var e in AR.Iterator(StorageOrders.RowMajor)) {
                Assert.IsTrue(rowVals[i++] == e);
            }
            Assert.IsTrue(i == (ulong)rowVals.LongLength);
            Assert.IsFalse(object.ReferenceEquals(A.Storage, AR.Storage));

            AR = A.C; 
            i = 0;
            foreach (var e in AR.Iterator(StorageOrders.ColumnMajor)) {
                Assert.IsTrue(colVals[i++] == e);
            }
            Assert.IsTrue(i == (ulong)rowVals.LongLength);
            Assert.IsFalse(object.ReferenceEquals(A.Storage, AR.Storage));
        }

        [TestMethod]
        public void ArrayGetEnumeratorTestScalar() {
            Array<complex> A = new complex(1, 2);
            Assert.IsTrue(A.GetValue(0) == new complex(1, 2)); 

            foreach (var e in A.Iterator(StorageOrders.ColumnMajor)) {

                Assert.IsTrue(e.real == 1.0);
                Assert.IsTrue(e.imag == 2.0);
            }
            Assert.IsTrue(A.GetValue(0) == new complex(1, 2));

            foreach (var e in ((InArray<complex>)A).Iterator(StorageOrders.ColumnMajor)) {

                Assert.IsTrue(e.real == 1.0);
                Assert.IsTrue(e.imag == 2.0);
            }
            Assert.IsTrue(A.GetValue(0) == new complex(1, 2));

            foreach (var e in A.Iterator(StorageOrders.RowMajor)) {

                Assert.IsTrue(e.real == 1.0);
                Assert.IsTrue(e.imag == 2.0);
            }
            Assert.IsTrue(A.GetValue(0) == new complex(1, 2));

            foreach (var e in ((InArray<complex>)A).Iterator(StorageOrders.RowMajor)) {

                Assert.IsTrue(e.real == 1.0);
                Assert.IsTrue(e.imag == 2.0);
            }
            Assert.IsTrue(A.GetValue(0) == new complex(1, 2));
        }

        [TestMethod]
        public void ArrayGetEnumeratorTestempty() {
            Array<short> A = new short[0];

            foreach (var e in A.Iterator(StorageOrders.ColumnMajor)) {
                Assert.Fail("Empty iterator should not return a value.");
            }

            foreach (var e in A.C.Iterator(StorageOrders.ColumnMajor)) {
                Assert.Fail("Empty iterator should not return a value.");
            }

            foreach (var e in A.Iterator(StorageOrders.RowMajor)) {
                Assert.Fail("Empty iterator should not return a value.");
            }

            foreach (var e in A.C.Iterator(StorageOrders.RowMajor)) {
                Assert.Fail("Empty iterator should not return a value.");
            }
        }
    }
}
