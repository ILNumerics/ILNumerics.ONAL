using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security;
using static ILNumerics.ILMath;
using static ILNumerics.Globals; 

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public class LogicalArrayTests {

        [TestMethod]
        [SecuritySafeCritical]
        public unsafe void LogicalSubarrayResetsNumberTrues() {
            Logical A = (counter(1.0, 1.0, 5, 4).T % 2 == 0)[r(1, end), full];
            Assert.IsTrue(A.Storage.NumberTrues == 8);
        }
        [TestMethod]
        [SecuritySafeCritical]
        public unsafe void LogicalAdvancedSubarrayResetsNumberTrues() {
            Array<uint> I = new uint[] { 1, 2, 3 };
            Settings.MaxNumberThreads = 4; 
            Assert.IsTrue(I.GetValue(0) == 1);
            Assert.IsTrue(I.GetValue(1) == 2);
            Assert.IsTrue(I.GetValue(2) == 3);

            Logical A = (counter(1.0, 1.0, 5, 4).T % 2 == 0)[I, full];
            Assert.IsTrue(A.Storage.NumberTrues == 8);

        }

        [TestMethod]
        public void LogicalSetValueResetsNumberTrues() {

            Array<short> A = counter<short>(1, 1, 5, 4);
            Logical L = A >= 10;
            Assert.IsTrue(L.Storage.NumberTrues == 11);

            //L[1] = true;
            L.SetValue(true, 1);
            Assert.IsTrue(L.Storage.NumberTrues == 12);
            L.SetValue(true, 1, 1);
            Assert.IsTrue(L.Storage.NumberTrues == 13);
            L.SetValue(true, 2, 1, 0);
            Assert.IsTrue(L.Storage.NumberTrues == 14);
            L.SetValue(true, 3, 1, 0, 0);
            Assert.IsTrue(L.Storage.NumberTrues == 15);
            L.SetValue(false, 4, 1, 0, 0, 0, 0, 0);
            Assert.IsTrue(L.Storage.NumberTrues == 14);

        }
        [TestMethod]
        public void LogicalSetIndexersResetNumberTrues() {

            Array<short> A = counter<short>(1, 1, 5, 4);
            Logical L = A >= 10;
            Assert.IsTrue(L.Storage.NumberTrues == 11);

            L[1] = true;
            Assert.IsTrue(L.Storage.NumberTrues == 12);
            L[1, 1] = true;
            Assert.IsTrue(L.Storage.NumberTrues == 13);
            L[2, 1, 0] = true;
            Assert.IsTrue(L.Storage.NumberTrues == 14);
            L[3, 1, 0, 0] = true;
            Assert.IsTrue(L.Storage.NumberTrues == 15);
            L[4, 1, 0, 0, 0, 0, 0] = false;
            Assert.IsTrue(L.Storage.NumberTrues == 14);

        }
        [TestMethod]
        public void LogicalSetRangeResetsNumberTrues() {

            Array<short> A = counter<short>(1, 1, 5, 4);
            Logical L = A >= 10;
            Assert.IsTrue(L.Storage.NumberTrues == 11);

            L["1"] = true;
            Assert.IsTrue(L.Storage.NumberTrues == 12);
            L["1", 1] = true;
            Assert.IsTrue(L.Storage.NumberTrues == 13);
            L[2, "1", 0] = true;
            Assert.IsTrue(L.Storage.NumberTrues == 14);
            L[3, "1", 0, 0] = true;
            Assert.IsTrue(L.Storage.NumberTrues == 15);
            L[4, "1", 0, 0, 0, 0, 0] = false;
            Assert.IsTrue(L.Storage.NumberTrues == 14);

        }

        [TestMethod]
        public void Logcial_Detach_CopyTo() {

            Array<double> A = counter<double>(1.0, 1.0, 4, 3, StorageOrders.RowMajor);
            Logical L = A % 3 == 1;

            Assert.IsTrue(L.NumberTrues == A.S.NumberOfElements / 3); 
            Assert.IsTrue(L.S.StorageOrder == Settings.DefaultStorageOrder);

            L.Storage.EnsureStorageOrder(StorageOrders.ColumnMajor);

            Assert.IsTrue(L.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(L.NumberTrues == A.S.NumberOfElements / 3);
            
        }
    }
}
