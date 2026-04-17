using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILNumerics.Core.Functions.Builtin;

namespace ILNumerics.Core.UnitTests {

    [TestClass]
    public class WriteToTests {
        [TestMethod]
        public unsafe void WriteTo_1Dim_1DimTest() {
            Array<uint> left = new uint[] { 1, 2, 3, 4, 5 };
            Array<uint> right = new uint[] { 22, 44 };


            long* outBSD = stackalloc long[6];
            outBSD[0] = (1); 
            outBSD[1] = (2); 
            outBSD[2] = (1);
            outBSD[3] = (2);
            outBSD[4] = (2);

            WriteToOperators.WriteTo_BSD<uint>(right.Storage.m_handles.GetHandlesArray()[0], right.S,
                                    left.Storage.m_handles.GetHandlesArray()[0], 
                                    outBSD, sizeof(uint));

            Assert.IsTrue(left.S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions);
            Assert.IsTrue(left.S.NumberOfElements == 5);
            Assert.IsTrue(left.S[0] == 5 && left.S[1] == 1);
            Assert.IsTrue(left.GetValue(0) == 1); 
            Assert.IsTrue(left.GetValue(1) == 22); 
            Assert.IsTrue(left.GetValue(2) == 3); 
            Assert.IsTrue(left.GetValue(3) == 44); 
            Assert.IsTrue(left.GetValue(4) == 5);

            Assert.IsTrue(right.S.NumberOfElements == 2); 
            Assert.IsTrue(right.S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions);
            Assert.IsTrue(right.GetValue(0) == 22);
            Assert.IsTrue(right.GetValue(1) == 44);

        }
        [TestMethod]
        public unsafe void WriteTo_1Dim_ScalarTest() {
            Array<double> left = MathInternal.counter(1.0, 1.0, 10);
            Array<double> right = -22;


            long* outBSD = stackalloc long[6];
            outBSD[0] = (1); 
            outBSD[1] = (8); 
            outBSD[2] = (1);
            outBSD[3] = (8);
            outBSD[4] = (1);

            WriteToOperators.WriteTo_BSD<double>(right.Storage.m_handles.GetHandlesArray()[0], right.S,
                                    left.Storage.m_handles.GetHandlesArray()[0], 
                                    outBSD, sizeof(double));

            Assert.IsTrue(left.S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions);
            Assert.IsTrue(left.S.NumberOfElements == 10);
            Assert.IsTrue(left.S[0] == 10 && left.S[1] == 1);
            Assert.IsTrue(left.GetValue(0) == 1); 
            Assert.IsTrue(left.GetValue(1) == -22); 
            Assert.IsTrue(left.GetValue(2) == -22); 
            Assert.IsTrue(left.GetValue(3) == -22); 
            Assert.IsTrue(left.GetValue(4) == -22);
            Assert.IsTrue(left.GetValue(5) == -22);
            Assert.IsTrue(left.GetValue(6) == -22);
            Assert.IsTrue(left.GetValue(7) == -22);
            Assert.IsTrue(left.GetValue(8) == -22);
            Assert.IsTrue(left.GetValue(9) == 10);

            Assert.IsTrue(right.S.NumberOfElements == 1); 
            Assert.IsTrue(right.S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions);
            Assert.IsTrue(right.GetValue(0) == -22);

        }
        [TestMethod]
        public unsafe void WriteTo_1Dim_NPScalarTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> left = MathInternal.counter(1.0, 1.0, 10);
                Array<double> right = -22;


                long* outBSD = stackalloc long[6];
                outBSD[0] = (1);
                outBSD[1] = (8);
                outBSD[2] = (1);
                outBSD[3] = (8);
                outBSD[4] = (1);

                WriteToOperators.WriteTo_BSD<double>(right.Storage.m_handles.GetHandlesArray()[0], right.S,
                                        left.Storage.m_handles.GetHandlesArray()[0],
                                        outBSD, sizeof(double));

                Assert.IsTrue(left.S.NumberOfDimensions == 1);
                Assert.IsTrue(left.S.NumberOfElements == 10);
                Assert.IsTrue(left.S[0] == 10);
                Assert.IsTrue(left.GetValue(0) == 1);
                Assert.IsTrue(left.GetValue(1) == -22);
                Assert.IsTrue(left.GetValue(2) == -22);
                Assert.IsTrue(left.GetValue(3) == -22);
                Assert.IsTrue(left.GetValue(4) == -22);
                Assert.IsTrue(left.GetValue(5) == -22);
                Assert.IsTrue(left.GetValue(6) == -22);
                Assert.IsTrue(left.GetValue(7) == -22);
                Assert.IsTrue(left.GetValue(8) == -22);
                Assert.IsTrue(left.GetValue(9) == 10);

                Assert.IsTrue(right.S.NumberOfElements == 1);
                Assert.IsTrue(right.S.NumberOfDimensions == 0);
                Assert.IsTrue(right.GetValue(0) == -22);

            }
        }
        [TestMethod]
        public unsafe void WriteTo_2Dim_NPScalarTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> left = MathInternal.counter(1.0, 1.0, 10, 4, order: StorageOrders.RowMajor);
                Array<double> right = -22;


                long* outBSD = stackalloc long[7];
                outBSD[0] = (2);
                outBSD[1] = (12);
                outBSD[2] = (5);
                outBSD[3] = (4);
                outBSD[4] = (3);
                outBSD[5] = (8);
                outBSD[6] = (1);

                WriteToOperators.WriteTo_BSD<double>(right.Storage.m_handles.GetHandlesArray()[0], right.S,
                                        left.Storage.m_handles.GetHandlesArray()[0],
                                        outBSD, sizeof(double));

                Assert.IsTrue(left.S.NumberOfDimensions == 2);
                Assert.IsTrue(left.S.NumberOfElements == 40);
                Assert.IsTrue(left.S[0] == 10 && left.S[1] == 4);
                Array<double> Result = new double[,] {
                    { 1, 2, 3, 4 },{ 5,-22,-22,-22 },{ 9,10,11,12},{13,-22,-22,-22 },{ 17,18,19,20,},
                    { 21, -22,-22,-22 },{ 25,26,27,28 },{ 29,-22,-22,-22},{33,34,35,36 },{ 37,38,39,40}
                };

                Assert.IsTrue(Result.Equals(left)); 

                Assert.IsTrue(right.S.NumberOfElements == 1);
                Assert.IsTrue(right.S.NumberOfDimensions == 0);
                Assert.IsTrue(right.GetValue(0) == -22);

            }
        }

        [TestMethod]
        public unsafe void WriteTo_2Dim_NPVectorColasMatrixTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> left = MathInternal.counter(1.0, 1.0, 10, 4, order: StorageOrders.RowMajor);
                Array<double> right = MathInternal.counter(-22.0, -1.0, 4, 1, order: StorageOrders.RowMajor);


                long* outBSD = stackalloc long[7];
                outBSD[0] = (2);
                outBSD[1] = (12);
                outBSD[2] = (5);
                outBSD[3] = (4);
                outBSD[4] = (3);
                outBSD[5] = (8);
                outBSD[6] = (1);

                WriteToOperators.WriteTo_BSD<double>(right.Storage.m_handles.GetHandlesArray()[0], right.S,
                                        left.Storage.m_handles.GetHandlesArray()[0],
                                        outBSD, sizeof(double));

                Assert.IsTrue(left.S.NumberOfDimensions == 2);
                Assert.IsTrue(left.S.NumberOfElements == 40);
                Assert.IsTrue(left.S[0] == 10 && left.S[1] == 4);
                Array<double> Result = new double[,] {
                    { 1, 2, 3, 4 },{ 5,-22,-22,-22 },{ 9,10,11,12},{13,-23,-23,-23 },{ 17,18,19,20,},
                    { 21, -24,-24,-24 },{ 25,26,27,28 },{ 29,-25,-25,-25},{33,34,35,36 },{ 37,38,39,40}
                };

                Assert.IsTrue(Result.Equals(left)); 

                Assert.IsTrue(right.S.NumberOfElements == 4);
                Assert.IsTrue(right.S.NumberOfDimensions == 2);
                Assert.IsTrue(right.GetValue(0) == -22);
                Assert.IsTrue(right.GetValue(1) == -23);
                Assert.IsTrue(right.GetValue(2) == -24);
                Assert.IsTrue(right.GetValue(3) == -25);

            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public unsafe void WriteTo_2Dim_NP3DFailTest()
        {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy))
            {

                Array<double> left = MathInternal.counter(1.0, 1.0, 10, 4);
                Array<double> right = MathInternal.counter(-22.0, -1.0, 4,1,2);


                long* outBSD = stackalloc long[7];
                outBSD[0] = (2);
                outBSD[1] = (12);
                outBSD[2] = (5);
                outBSD[3] = (4);
                outBSD[4] = (3);
                outBSD[5] = (8);
                outBSD[6] = (1);
 
                WriteToOperators.WriteTo_BSD<double>(right.Storage.m_handles.GetHandlesArray()[0], right.S,
                                        left.Storage.m_handles.GetHandlesArray()[0],
                                        outBSD, sizeof(double));
            }
        }

        [TestMethod]
        public unsafe void WriteTo_2Dim_NPVectorColTest()
        {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy))
            {

                Array<double> left = MathInternal.counter(1.0, 1.0, 10, 4, order: StorageOrders.RowMajor);
                Array<double> right = MathInternal.counter(-22.0, -1.0, 4, 1, order: StorageOrders.RowMajor);


                long* outBSD = stackalloc long[7];
                outBSD[0] = (2); // ndims
                outBSD[1] = (12);// numel 
                outBSD[2] = (5); // offset
                outBSD[3] = (4); // d0
                outBSD[4] = (3); // d1
                outBSD[5] = (8); // s0
                outBSD[6] = (1); // s1

                WriteToOperators.WriteTo_BSD<double>(right.Storage.m_handles.GetHandlesArray()[0], right.S,
                                        left.Storage.m_handles.GetHandlesArray()[0],
                                        outBSD, sizeof(double));

                Assert.IsTrue(left.S.NumberOfDimensions == 2);
                Assert.IsTrue(left.S.NumberOfElements == 40);
                Assert.IsTrue(left.S[0] == 10 && left.S[1] == 4);
                Array<double> Result = new double[,] {
                    { 1, 2, 3, 4 },
                    { 5,-22,-22,-22 },
                    { 9,10,11,12},
                    {13,-23,-23,-23 },
                    { 17,18,19,20,},
                    { 21, -24,-24,-24 },
                    { 25,26,27,28 },
                    { 29,-25,-25,-25},
                    { 33,34,35,36 },
                    { 37,38,39,40}
                };

                Assert.IsTrue(Result.Equals(left));

                Assert.IsTrue(right.S.NumberOfElements == 4);
                Assert.IsTrue(right.S.NumberOfDimensions == 2);
                Assert.IsTrue(right.GetValue(0) == -22);
                Assert.IsTrue(right.GetValue(1) == -23);
                Assert.IsTrue(right.GetValue(2) == -24);
                Assert.IsTrue(right.GetValue(3) == -25);

            }
        }

        [TestMethod]
        public unsafe void WriteTo_2Dim_NPVectorRowTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> left = MathInternal.counter(1.0, 1.0, 10, 4, order: StorageOrders.RowMajor);
                Array<double> right = MathInternal.counter(-22.0, -1.0, 1, 3, order: StorageOrders.RowMajor);

                long* outBSD = stackalloc long[7];
                outBSD[0] = (2);
                outBSD[1] = (12);
                outBSD[2] = (5);
                outBSD[3] = (4);
                outBSD[4] = (3);
                outBSD[5] = (8);
                outBSD[6] = (1);

                WriteToOperators.WriteTo_BSD<double>(right.Storage.m_handles.GetHandlesArray()[0], right.S,
                                        left.Storage.m_handles.GetHandlesArray()[0],
                                        outBSD, sizeof(double));

                Assert.IsTrue(left.S.NumberOfDimensions == 2);
                Assert.IsTrue(left.S.NumberOfElements == 40);
                Assert.IsTrue(left.S[0] == 10 && left.S[1] == 4);
                Array<double> Result = new double[,] {
                    { 1, 2, 3, 4 },{ 5,-22,-23,-24 },{ 9,10,11,12},{13,-22,-23,-24 },{ 17,18,19,20,},
                    { 21, -22,-23,-24 },{ 25,26,27,28 },{ 29,-22,-23,-24},{33,34,35,36 },{ 37,38,39,40}
                };

                Assert.IsTrue(Result.Equals(left)); 

                Assert.IsTrue(right.S.NumberOfElements == 3);
                Assert.IsTrue(right.S.NumberOfDimensions == 2);
                Assert.IsTrue(right.GetValue(0) == -22);
                Assert.IsTrue(right.GetValue(1) == -23);
                Assert.IsTrue(right.GetValue(2) == -24);

            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public unsafe void WriteTo_NonBroadcastable_FailTest() {
            Array<double> left = MathInternal.counter(1.0, 1.0, 10);
            Array<double> right = MathInternal.counter(1.0, 1.0, 10);


            long* outBSD = stackalloc long[6];
            outBSD[0] = (1);
            outBSD[1] = (8);
            outBSD[2] = (1);
            outBSD[3] = (8);
            outBSD[4] = (1);
 

            WriteToOperators.WriteTo_BSD<double>(right.Storage.m_handles.GetHandlesArray()[0], right.S,
                                    left.Storage.m_handles.GetHandlesArray()[0],
                                    outBSD, sizeof(double));

        }
    }
}
