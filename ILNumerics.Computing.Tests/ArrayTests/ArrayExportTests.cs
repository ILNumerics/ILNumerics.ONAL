using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace ILNumerics.Core.UnitTests {

    [TestClass]
    public class ArrayExportTests {

        [TestMethod]
        public void ArrayExportTest() {

            var SysArr = new uint[,] {
                { 1, 2, 3  },
                { 4, 5, 6  },
                { 7, 8, 9  },
                { 10, 11, 12 },
            };
            Array<uint> A = SysArr;
            uint[] exp = null;
            A.ExportValues(ref exp);

            var res = new uint[] { 1, 4, 7, 10, 2, 5, 8, 11, 3, 6, 9, 12 };
            ArrayAssert.ValuesEqual(res, exp);

        }
        [TestMethod]
        public void ArrayExportTest33_1() {

            var SysArr = new byte[,] {
                {  1,  2,  3,  4,  5,  6,  7,  8,  9, 10, 11 },
                { 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22 },
                { 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33 },
            };
            Array<byte> A = SysArr;
            byte[] exp = null;
            A.ExportValues(ref exp);

            var res = new byte[] { 1, 12, 23, 2, 13, 24, 3, 14, 25, 4, 15, 26, 5, 16, 27, 6, 17, 28, 7, 18, 29, 8, 19, 30, 9, 20, 31, 10, 21, 32, 11, 22, 33 };
            ArrayAssert.ValuesEqual(res, exp);

        }

        [TestMethod]
        public void ArrayExportTest_Large_1Threads() {
            float[] expRows = new float[1];
            float[] expCols = new float[1];
            Array SysArr = Helper.generateSystemArray<float>(1000, 400, ref expRows, ref expCols);
            Array<float> A = SysArr;

            float[] result = null;

            uint oldThreadCount = Settings.MaxNumberThreads;
            try {
                Settings.MaxNumberThreads = 1;
                A.ExportValues(ref result, StorageOrders.ColumnMajor);
                ArrayAssert.ValuesEqual(result, expCols);

                A.ExportValues(ref result, StorageOrders.RowMajor);
                ArrayAssert.ValuesEqual(result, expRows);

            } finally {
                Settings.MaxNumberThreads = oldThreadCount;
            }
        }
        [TestMethod]
        public void ArrayExportTest_Large_2Threads() {
            float[] expRows = new float[1];
            float[] expCols = new float[1];
            Array SysArr = Helper.generateSystemArray<float>(1000, 400, ref expRows, ref expCols);
            Array<float> A = SysArr;

            float[] result = null;

            uint oldThreadCount = Settings.MaxNumberThreads;
            try {
                Settings.MaxNumberThreads = 2;
                A.ExportValues(ref result, StorageOrders.ColumnMajor);
                ArrayAssert.ValuesEqual(result, expCols);

                A.ExportValues(ref result, StorageOrders.RowMajor);
                ArrayAssert.ValuesEqual(result, expRows);

            } finally {
                Settings.MaxNumberThreads = oldThreadCount;
            }
        }
        [TestMethod]
        public void ArrayExportTest_Large_3Threads() {
            float[] expRows = new float[1];
            float[] expCols = new float[1];
            Array SysArr = Helper.generateSystemArray<float>(1000, 400, ref expRows, ref expCols);
            Array<float> A = SysArr;

            float[] result = null;

            uint oldThreadCount = Settings.MaxNumberThreads;
            try {
                Settings.MaxNumberThreads = 3;
                A.ExportValues(ref result, StorageOrders.ColumnMajor);
                ArrayAssert.ValuesEqual(result, expCols);

                A.ExportValues(ref result, StorageOrders.RowMajor);
                ArrayAssert.ValuesEqual(result, expRows);

            } finally {
                Settings.MaxNumberThreads = oldThreadCount;
            }
        }
        [TestMethod]
        public void ArrayExportTest_Large_4Threads() {
            float[] expRows = new float[1];
            float[] expCols = new float[1];
            Array SysArr = Helper.generateSystemArray<float>(1000, 400, ref expRows, ref expCols);
            Array<float> A = SysArr;

            float[] result = null;

            uint oldThreadCount = Settings.MaxNumberThreads;
            try {
                Settings.MaxNumberThreads = 4;
                A.ExportValues(ref result, StorageOrders.ColumnMajor);
                ArrayAssert.ValuesEqual(result, expCols);

                A.ExportValues(ref result, StorageOrders.RowMajor);
                ArrayAssert.ValuesEqual(result, expRows);

            } finally {
                Settings.MaxNumberThreads = oldThreadCount;
            }
        }
        [TestMethod]
        public void ArrayExportTest_Large_5Threads() {
            float[] expRows = new float[1];
            float[] expCols = new float[1];
            Array SysArr = Helper.generateSystemArray<float>(1000, 400, ref expRows, ref expCols);
            Array<float> A = SysArr;

            float[] result = null;

            uint oldThreadCount = Settings.MaxNumberThreads;
            try {
                Settings.MaxNumberThreads = 5;
                A.ExportValues(ref result, StorageOrders.ColumnMajor);
                ArrayAssert.ValuesEqual(result, expCols);

                A.ExportValues(ref result, StorageOrders.RowMajor);
                ArrayAssert.ValuesEqual(result, expRows);

            } finally {
                Settings.MaxNumberThreads = oldThreadCount;
            }
        }
        [TestMethod]
        public void ArrayExportTest_Large_6Threads() {
            float[] expRows = new float[1];
            float[] expCols = new float[1];
            Array SysArr = Helper.generateSystemArray<float>(1000, 400, ref expRows, ref expCols);
            Array<float> A = SysArr;

            float[] result = null;

            uint oldThreadCount = Settings.MaxNumberThreads;
            try {
                Settings.MaxNumberThreads = 6;
                A.ExportValues(ref result, StorageOrders.ColumnMajor);
                ArrayAssert.ValuesEqual(result, expCols);

                A.ExportValues(ref result, StorageOrders.RowMajor);
                ArrayAssert.ValuesEqual(result, expRows);

            } finally {
                Settings.MaxNumberThreads = oldThreadCount;
            }
        }

        [TestMethod]
        public void ArrayExportTest_Large_3Threads64() {
            double[] expRows = new double[1];
            double[] expCols = new double[1];
            Array SysArr = Helper.generateSystemArrayDouble(1000, 400, ref expRows, ref expCols);
            Array<double> A = SysArr;
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor); 
            double[] result = null;

            uint oldThreadCount = Settings.MaxNumberThreads;
            // warm start
                Settings.MaxNumberThreads = 4;
            A.ExportValues(ref result, StorageOrders.ColumnMajor);
            try {
                ArrayAssert.ValuesEqual(result, expCols);
            } catch (AssertFailedException exc) {
                Debugger.Break(); 
            }
            Stopwatch sw = new Stopwatch();
            try {
                Settings.MaxNumberThreads = 3;
                sw.Restart();
                A.ExportValues(ref result, StorageOrders.ColumnMajor);
                Trace.WriteLine($"Array.Export (row major -> column major): {sw.ElapsedMilliseconds}ms");
                ArrayAssert.ValuesEqual(result, expCols);

                sw.Restart();
                A.ExportValues(ref result, StorageOrders.RowMajor);
                Trace.WriteLine($"Array.Export (row major -> row major): {sw.ElapsedMilliseconds}ms");
                ArrayAssert.ValuesEqual(result, expRows);

            } finally {
                Settings.MaxNumberThreads = oldThreadCount;
            }
        }

        [TestMethod]
        public void ArrayExportTest_empty() {
            Array<double> A = new double[0];
            double[] result = null;
            A.ExportValues(ref result);
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.LongLength == 0);
            Assert.IsTrue(result.Rank == 1);  // strange enough: System.Array does not like true empties
        }

        [TestMethod]
        public void ArrayExportTest33_2() {

            var SysArr = new short[,] {
                {  1,  2,  3,  4,  5,  6,  7,  8,  9, 10, 11 },
                { 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22 },
                { 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33 },
            };
            Array<short> A = SysArr;
            short[] exp = null;
            A.ExportValues(ref exp);

            var res = new short[] { 1, 12, 23, 2, 13, 24, 3, 14, 25, 4, 15, 26, 5, 16, 27, 6, 17, 28, 7, 18, 29, 8, 19, 30, 9, 20, 31, 10, 21, 32, 11, 22, 33 };
            ArrayAssert.ValuesEqual(res, exp);

        }
        [TestMethod]
        public void ArrayExportTest33_4() {

            var SysArr = new uint[,] {
                {  1,  2,  3,  4,  5,  6,  7,  8,  9, 10, 11 },
                { 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22 },
                { 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33 },
            };
            Array<uint> A = SysArr;
            uint[] exp = null;
            A.ExportValues(ref exp);

            var res = new uint[] { 1, 12, 23, 2, 13, 24, 3, 14, 25, 4, 15, 26, 5, 16, 27, 6, 17, 28, 7, 18, 29, 8, 19, 30, 9, 20, 31, 10, 21, 32, 11, 22, 33 };
            ArrayAssert.ValuesEqual(res, exp);

        }
        [TestMethod]
        public void ArrayExportTest33_8() {

            var SysArr = new double[,] {
                {  1,  2,  3,  4,  5,  6,  7,  8,  9, 10, 11 },
                { 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22 },
                { 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33 },
            };
            Array<double> A = SysArr;
            double[] exp = null;
            A.ExportValues(ref exp);

            var res = new double[] { 1, 12, 23, 2, 13, 24, 3, 14, 25, 4, 15, 26, 5, 16, 27, 6, 17, 28, 7, 18, 29, 8, 19, 30, 9, 20, 31, 10, 21, 32, 11, 22, 33 };
            ArrayAssert.ValuesEqual(res, exp);

        }
        [TestMethod]
        public void ArrayExportTest33_16() {

            var SysArr = new complex[,] {
                {  1,  2,  3,  4,  5,  6,  7,  8,  9, 10, 11 },
                { 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22 },
                { 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33 },
            };
            Array<complex> A = SysArr;
            complex[] exp = null;
            A.ExportValues(ref exp);

            var res = new complex[] { 1, 12, 23, 2, 13, 24, 3, 14, 25, 4, 15, 26, 5, 16, 27, 6, 17, 28, 7, 18, 29, 8, 19, 30, 9, 20, 31, 10, 21, 32, 11, 22, 33 };
            ArrayAssert.ValuesEqual(res, exp);

        }

        // StorageOrders -> all permutations
        // with/out requesting size 
        // sequential, 1,2,3 dims
        // multithread, 1,2,3 dims / 1,2,3,4 threads
    }
}
