using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILNumerics.Core.Functions.Builtin;
using System.Diagnostics;
using static ILNumerics.Globals; 

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public unsafe class SumAllTests {

        [TestMethod]
        public void SumAllSimpleTest() {
            double[] dummy = null;
            Array<double> A = Helper.generateSystemArray<double>(10, 1, ref dummy, ref dummy);

            Array<double> B = MathInternal.sumall(A);

            Assert.IsTrue(!Equals(B, null));
            Assert.IsTrue(B.IsScalar);
            Assert.IsTrue(B.S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions);
            Assert.IsTrue(B.GetValue(0) == 45);

        }
        [TestMethod]
        public void SumAll_Scalar_Test() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                Array<byte> A = 11;

                Array<byte> B = MathInternal.sumall(A);

                Assert.IsTrue(!Equals(B, null));
                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B.S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions);
                Assert.IsTrue(B.GetValue(0) == 11);
            }
        }

        [TestMethod]
        public void SumAllContMultithread2() {
            double[] dummy = null;
            Array<double> A = Helper.generateSystemArray<double>(3000, 3001, ref dummy, ref dummy);

            var oldThreads = Settings.MaxNumberThreads;
            try {
                Settings.MaxNumberThreads = 2;

                Array<double> B = MathInternal.sumall(A);
                for (int i = 0; i < 5; i++) {

                    var sw = Stopwatch.StartNew();
                    B.a = MathInternal.sumall(A);
                    sw.Stop();

                    Console.WriteLine($"Sumall over {A.S[0]} x {A.S[1]} with {Settings.MaxNumberThreads} thread took: {sw.ElapsedMilliseconds}ms.");
                }
                Settings.MaxNumberThreads = 1;
                for (int i = 0; i < 5; i++) {

                    var sw = Stopwatch.StartNew();
                    B.a = MathInternal.sumall(A);
                    sw.Stop();

                    Console.WriteLine($"Sumall over {A.S[0]} x {A.S[1]} with {Settings.MaxNumberThreads} thread took: {sw.ElapsedMilliseconds}ms.");
                }
                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B.S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions);
                Assert.IsTrue(B.GetValue(0) == A.S.NumberOfElements * (A.S.NumberOfElements - 1) / 2);
            } finally {
                Settings.MaxNumberThreads = oldThreads;
            }
        }
        [TestMethod]
        public void SumAllContMultithread3() {
            double[] dummy = null;
            Array<double> A = Helper.generateSystemArray<double>(3000, 3001, ref dummy, ref dummy);

            var oldThreads = Settings.MaxNumberThreads;
            try {
                Settings.MaxNumberThreads = 3;

                Array<double> B = MathInternal.sumall(A);
                for (int i = 0; i < 5; i++) {

                    var sw = Stopwatch.StartNew();
                    B.a = MathInternal.sumall(A);
                    sw.Stop();

                    Console.WriteLine($"Sumall over {A.S[0]} x {A.S[1]} with {Settings.MaxNumberThreads} thread took: {sw.ElapsedMilliseconds}ms.");
                }
                Settings.MaxNumberThreads = 1;
                for (int i = 0; i < 5; i++) {

                    var sw = Stopwatch.StartNew();
                    B.a = MathInternal.sumall(A);
                    sw.Stop();

                    Console.WriteLine($"Sumall over {A.S[0]} x {A.S[1]} with {Settings.MaxNumberThreads} thread took: {sw.ElapsedMilliseconds}ms.");
                }
                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B.S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions);
                Assert.IsTrue(B.GetValue(0) == A.S.NumberOfElements * (A.S.NumberOfElements - 1) / 2);
            } finally {
                Settings.MaxNumberThreads = oldThreads;
            }
        }

        [TestMethod]
        public void Sumall_Strided32SimpleTest() {
            double[] dummy = null;
            Array<double> A = Helper.generateSystemArray<double>(10, 20, ref dummy, ref dummy);
            var bsd = A.S.GetBSD(true);
            // take each second row only
            bsd[1] =bsd[1] / 2;
            bsd[3] =bsd[3] / 2;
            bsd[5] =bsd[5] * 2;

            Array<double> B = MathInternal.sumall(A);

            Assert.IsTrue(B.GetValue(0) == 8950);
            Assert.IsTrue(B.IsScalar);

        }
        [TestMethod]
        public void Sumall_Strided32MultiThread2Test() {
            double[] dummy = null;
            Array<double> A = Helper.generateSystemArray<double>(200, 20, ref dummy, ref dummy);
            var bsd = A.S.GetBSD(true);
            // take each second row only
            bsd[1] = bsd[1] / 2;
            bsd[3] = bsd[3] / 2;
            bsd[5] = bsd[5] * 2;

            var oldThreads = Settings.MaxNumberThreads;

            try {
                Settings.MaxNumberThreads = 2;

                Array<double> B = MathInternal.sumall(A);

                Assert.IsTrue(B.GetValue(0) == 3979000);
                Assert.IsTrue(B.IsScalar);
            } finally {
                Settings.MaxNumberThreads = oldThreads;
            }
        }
        [TestMethod]
        public void Sumall_Strided64MultiThread3BaseOffsetTest() {

            Array<double> A = MathInternal.counter(-1.0, 0.5, 101, 50);
            A.a = A[r(4, 100 * 50 + 3)].Reshape(100, 50); // creates base offset 4
            A.a = A["0:2:", "0:2:"];  // creates non 1 striding in each dimension

            using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {

                Array<double> B = MathInternal.sumall(A);
                Array<double> Res = MathInternal.sum(MathInternal.sum(A)); 
                Assert.IsTrue(B == Res);
                Assert.IsTrue(B.IsScalar);
            }
        }

        [TestMethod]
        public void Sumall_EmptyTest() {
            Array<uint> A = new uint[0];

            Array<uint> B = MathInternal.sumall(A);
            Assert.IsTrue(B.IsScalar);
            Assert.IsTrue(B.S[0] == 1);

            Array<uint> NPA;
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                NPA = MathInternal.empty<uint>(0);
                Assert.IsTrue(NPA.S[0] == 0);
                Assert.IsTrue(NPA.S.NumberOfDimensions == 1);
                Assert.IsTrue(NPA.S.NumberOfElements == 0);

                B = MathInternal.sumall(NPA);

                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B == 0);
                Assert.IsTrue(B.S.NumberOfDimensions == 0);
                Assert.IsTrue(B.S.NumberOfElements == 1);
            }
            // providing np zeros
            B = MathInternal.sumall(NPA);
            Assert.IsTrue(B.IsScalar);
            Assert.IsTrue(B == 0);
            Assert.IsTrue(B.S.NumberOfDimensions == 2);
            Assert.IsTrue(B.S.NumberOfElements == 1);
        }

        [TestMethod]
        public void Sumall_Strided32MultiThread2InternalChunkTest() {
            double[] dummy = null;
            Array<double> A = Helper.generateSystemArray<double>(10, 401, ref dummy, ref dummy);

            var bsd = A.S.GetBSD(true);
            // take each second row only
            bsd[1] = bsd[1] / 2;
            bsd[3] = bsd[3] / 2;
            bsd[5] = bsd[5] * 2;

            var oldThreads = Settings.MaxNumberThreads;

            try {
                Settings.MaxNumberThreads = 6;
                // one complete chunk is within a single lead dim run
                Array<double> B = MathInternal.sumall(A);

                Assert.IsTrue(B.GetValue(0) == 3617020);
                Assert.IsTrue(B.IsScalar);
            } finally {
                Settings.MaxNumberThreads = oldThreads;
            }
        }

    }
}
