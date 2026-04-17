using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILNumerics.Core.Functions.Builtin;
using System.Diagnostics;
using static ILNumerics.Globals; 
using static ILNumerics.ILMath; 

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public unsafe class AnyAllTests {

        [TestMethod]
        public void AnyAllSimpleTest() {
            Array<double> A = ones<double>(10, 1);

            Logical B = anyall(A);

            Assert.IsTrue(!Equals(B, null));
            Assert.IsTrue(B.IsScalar);
            Assert.IsTrue(B.S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions);
            Assert.IsTrue(B.GetValue(0));

            Assert.IsFalse(anyall(zeros<uint>(1, 2))); 
        }
        [TestMethod]
        public void AnyAll_Scalar_Test() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                Array<byte> A = 11;

                Logical B = MathInternal.anyall(A);

                Assert.IsTrue(!Equals(B, null));
                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B.S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions);
                Assert.IsTrue(B);

                A = 0;
                Assert.IsFalse(anyall(A)); 
            }
        }

        [TestMethod]
        public void AnyAllContMultithread2() {
            Array<double> A = counter<double>(0.0, 0.0, 3000, 3001);

           using (Settings.Ensure(()=> Settings.MaxNumberThreads, 2u)) { 

                Logical B = MathInternal.anyall(A);
                for (int i = 0; i < 5; i++) {

                    var sw = Stopwatch.StartNew();
                    B.a = MathInternal.anyall(A);
                    sw.Stop();

                    Console.WriteLine($"Anyall over {A.S[0]} x {A.S[1]} with {Settings.MaxNumberThreads} thread took: {sw.ElapsedMilliseconds}ms.");
                }
                Settings.MaxNumberThreads = 1;
                for (int i = 0; i < 5; i++) {

                    var sw = Stopwatch.StartNew();
                    B.a = MathInternal.anyall(A);
                    sw.Stop();

                    Console.WriteLine($"Anyall over {A.S[0]} x {A.S[1]} with {Settings.MaxNumberThreads} thread took: {sw.ElapsedMilliseconds}ms.");
                }
                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B.S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions);
                Assert.IsFalse(B);
            }
        }
        [TestMethod]
        public void AnyAllContMultithread3() {

            Array<double> A = zeros<double>(3000, 3001);
            A[end] = -1; // non-zero only at the very end 

            using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {

                Logical B = MathInternal.anyall(A);
                for (int i = 0; i < 5; i++) {

                    var sw = Stopwatch.StartNew();
                    B.a = MathInternal.anyall(A);
                    sw.Stop();
                    Assert.IsTrue(B); 
                    Console.WriteLine($"Anyall over {A.S[0]} x {A.S[1]} with {Settings.MaxNumberThreads} thread took: {sw.ElapsedMilliseconds}ms.");
                }
                Settings.MaxNumberThreads = 1;
                for (int i = 0; i < 5; i++) {

                    var sw = Stopwatch.StartNew();
                    B.a = MathInternal.anyall(A);
                    sw.Stop();
                    Assert.IsTrue(B);

                    Console.WriteLine($"Anyall over {A.S[0]} x {A.S[1]} with {Settings.MaxNumberThreads} thread took: {sw.ElapsedMilliseconds}ms.");
                }
                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B.S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions);
            }
        }

        [TestMethod]
        public void Anyall_Strided32SimpleTest() {
            double[] dummy = null;
            Array<double> A = Helper.generateSystemArray<double>(10, 20, ref dummy, ref dummy);
            var bsd = A.S.GetBSD(true);
            // take each second row only
            bsd[1] =bsd[1] / 2;
            bsd[3] =bsd[3] / 2;
            bsd[5] =bsd[5] * 2;

            Logical B = MathInternal.anyall(A);

            Assert.IsTrue(B.Storage.NumberTrues == 1); 
            Assert.IsTrue(B);
            Assert.IsTrue(B.IsScalar);

        }
        [TestMethod]
        public void Anyall_Strided32MultiThread2Test() {
            double[] dummy = null;
            Array<double> A = Helper.generateSystemArray<double>(200, 20, ref dummy, ref dummy);
            var bsd = A.S.GetBSD(true);
            // take each second row only
            bsd[1] = bsd[1] / 2;
            bsd[3] = bsd[3] / 2;
            bsd[5] = bsd[5] * 2;

            using (Settings.Ensure(() => Settings.MaxNumberThreads, 2u)) {

                Logical B = MathInternal.anyall(A);

                Assert.IsTrue(B.Storage.NumberTrues == 1);
                Assert.IsTrue(B);
                Assert.IsTrue(B.IsScalar);

                A[ellipsis] = 0;

                B = MathInternal.anyall(A);

                Assert.IsFalse(B);
                Assert.IsTrue(B.IsScalar);

            }


        }
        [TestMethod]
        public void Anyall_Strided64MultiThread3BaseOffsetTest() {

            Array<double> A = MathInternal.counter(0.0, 0, 101, 50);
            A.SetValue(1.0, 0); 

            A.a = A[r(4, 100 * 50 + 3)].Reshape(100, 50); // creates base offset 4
            A.a = A["0:2:", "0:2:"];  // creates non 1 striding in each dimension

            using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {

                Logical B = MathInternal.anyall(A);

                Assert.IsFalse(B);
                Assert.IsTrue(B.IsScalar);
            }
        }

        [TestMethod]
        public void Anyall_EmptyTest() {
            Array<uint> A = new uint[0];

            Logical B = MathInternal.anyall(A);
            Assert.IsTrue(B.IsScalar); 
            Assert.IsTrue(B.S[0] == 1);

            Array<short> NPA;
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                NPA = empty<short>(0);
                Assert.IsTrue(NPA.S[0] == 0);
                Assert.IsTrue(NPA.S.NumberOfDimensions == 1);
                Assert.IsTrue(NPA.S.NumberOfElements == 0);
                
                B = anyall(NPA);

                Assert.IsTrue(B.IsScalar);
                Assert.IsFalse(B);
                Assert.IsTrue(B.S.NumberOfDimensions == 0);
                Assert.IsTrue(B.S.NumberOfElements == 1);
            }
            // providing np zeros
            B = anyall(NPA);
            Assert.IsTrue(B.IsScalar);
            Assert.IsFalse(B);
            Assert.IsTrue(B.S.NumberOfDimensions == 2);
            Assert.IsTrue(B.S.NumberOfElements == 1);
        }

        [TestMethod]
        public void Anyall_NP_vectors1d() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<int> A = new int[] { 1, 2, 3, 4 };
                Assert.IsTrue(A.S.NumberOfDimensions == 1);
                Assert.IsTrue(any(A));
                Assert.IsTrue(all(A));
                Assert.IsTrue(any(A > 2));
                Assert.IsTrue(all(A > 0));
                Assert.IsFalse(all(A > 2));
            }
        }

        [TestMethod]
        public void Anyall_NP_Scalars0D() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<int> A = 2;
                Assert.IsTrue(A.S.NumberOfDimensions == 0);
                Assert.IsTrue(any(A));
                Assert.IsTrue(all(A));
                Assert.IsTrue(any(A >= 2));
                Assert.IsTrue(all(A > 0));
                Assert.IsFalse(all(A > 2));
            }
        }

        [TestMethod]
        public void Anyall_Strided32MultiThread2InternalChunkTest() {

            Array<double> A = MathInternal.counter<double>(1.0, 1.0, 10, 401);

            var bsd = A.S.GetBSD(true);
            // take each second row only
            bsd[1] = bsd[1] / 2;
            bsd[3] = bsd[3] / 2;
            bsd[5] = bsd[5] * 2;


            using (Settings.Ensure(() => Settings.MaxNumberThreads, 6u)) {
                // one complete chunk is within a single lead dim run
                Logical B = MathInternal.anyall(A == 177);

                Assert.IsTrue(B);
                Assert.IsTrue(B.IsScalar);
            }
        }

    }
}
