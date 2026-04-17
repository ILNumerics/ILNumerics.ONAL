using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public unsafe class GetLimitsTests {

        [TestMethod]
        public void GetLimitsContSimpleTest() {

            Array<int> A = new int[] { -2, -10, 6, -2, 0, 20, 1, 2 };
            int min, max;
            Assert.IsTrue(A.GetLimits(out min, out max));
            Assert.IsTrue(min == -10);
            Assert.IsTrue(max == 20);
        }
        [TestMethod]
        public void GetLimitsContSimpleTestUint() {

            Array<uint> A = new uint[] { 2, 10332, 6, 2, 0, 20, 1, 2 };
            uint min, max;
            Assert.IsTrue(A.GetLimits(out min, out max));
            Assert.IsTrue(min == 0);
            Assert.IsTrue(max == 10332);
        }
        [TestMethod]
        public void GetLimitsContSimpleTestSByte() {

            Array<sbyte> A = new sbyte[] { 2, -102, 6, 2, 0, 20, 1, -2 };
            sbyte min, max;
            Assert.IsTrue(A.GetLimits(out min, out max));
            Assert.IsTrue(min == -102);
            Assert.IsTrue(max == 20);
        }
        [TestMethod]
        public void GetLimitsContEmptyTest() {

            Array<sbyte> A = new sbyte[0] { };
            sbyte min, max;
            Assert.IsFalse(A.GetLimits(out min, out max));
            Assert.IsTrue(min == default(sbyte));
            Assert.IsTrue(max == default(sbyte));

        }
        [TestMethod]
        public void GetLimitsContScalarTest() {

            Array<float> A = new float[] { (float)Math.PI };
            float min, max;
            Assert.IsTrue(A.GetLimits(out min, out max));
            Assert.IsTrue(min == (float)Math.PI);
            Assert.IsTrue(max == (float)Math.PI);

        }
        [TestMethod]
        public void GetLimitsContScalarNumpyTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<float> A = (float)Math.PI; // 0 dim array
                Assert.IsTrue(A.S.NumberOfDimensions == 0); 
                float min, max;
                Assert.IsTrue(A.GetLimits(out min, out max));
                Assert.IsTrue(min == (float)Math.PI);
                Assert.IsTrue(max == (float)Math.PI);
            }
        }
        [TestMethod]
        public void GetLimitsContTestLargeMulti2() {

            float[] rows = new float[0], dummy = null; 
            Array<float> A = Helper.generateSystemArray<float>(1000, 1000, ref dummy, ref rows);

            var oldThreads = Settings.MaxNumberThreads; 
            try {
                Settings.MaxNumberThreads = (uint)Environment.ProcessorCount;

                float min, max;
                Assert.IsTrue(A.GetLimits(out min, out max));
                Assert.IsTrue(min == 0);
                Assert.IsTrue(max == A.S.NumberOfElements - 1);
                var sw = Stopwatch.StartNew();
                for (int l = 0; l < 200; l++) {

                    sw.Restart(); 
                    for (int i = 0; i < 10; i++) {
                        A.GetLimits(out min, out max);
                    }
                    sw.Stop();
                    Console.WriteLine($"GetLimits ([{A.S[0]}x{A.S[1]}]) Cont. took: {sw.ElapsedMilliseconds / 10.0}ms. Overhead: {InnerLoops.GetLimits.Single.s_threading_overhead_Cont64}");
                }

            } finally {
                Settings.MaxNumberThreads = oldThreads; 
            }

        }

        [TestMethod]
        public void GetLimitsStrided32simpleTest() {

            double[] dummy = null;
            Array<double> A = Helper.generateSystemArray<double>(10, 20, ref dummy, ref dummy);
            var bsd = A.S.GetBSD(true);
            // take each second row only
            bsd[1] = bsd[1] / 2;
            bsd[3] = bsd[3] / 2;
            bsd[5] = bsd[5] * 2;

            double min, max;
            Assert.IsTrue(A.GetLimits(out min, out max));
            Assert.IsTrue(min == 0);
            Assert.IsTrue(max == 179);

        }
        [TestMethod]
        public void GetLimitsStridedLargeMultiTest() {

            double[] dummy = null;
            Array<double> A = Helper.generateSystemArray<double>(1099, 401, ref dummy, ref dummy);
            var bsd = A.S.GetBSD(true);
            // take each second row only
            bsd[1] = (bsd[1] / 2);
            bsd[3] = (bsd[3] / 2);
            bsd[5] = (bsd[5] * 2);

            double min = -1, max = -1;
            Assert.IsTrue(Helper.Timeit(() => A.GetLimits(out min, out max), $"GetLimits Strided {A.S[0]}x{A.S[1]}", 
                            () => $" Overhead:{InnerLoops.GetLimits.Double.s_threading_overhead_Strided32}"));                         
            Assert.IsTrue(min == 0);
            Assert.IsTrue(max == A.GetValue(A.S[0] - 1, A.S[1] - 1));
        }

        [TestMethod]
        public void GetLimitsContNaNTests() {
            Array<float> A = new float[] { float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN };

            float min, max;
            Assert.IsFalse(A.GetLimits(out min, out max));
            Assert.IsTrue(min == default(float) && max == default(float));

        }
        [TestMethod]
        public void GetLimitsContNaNInfTests() {
            Array<float> A = new float[] { float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.PositiveInfinity };

            float min, max;
            Assert.IsFalse(A.GetLimits(out min, out max, true));
            Assert.IsTrue(min == default(float) && max == default(float));

            Assert.IsTrue(A.GetLimits(out min, out max, false));
            Assert.IsTrue(min == float.PositiveInfinity && max == float.PositiveInfinity);

        }
        [TestMethod]
        public void GetLimitsContNaNInfTests2() {

            Array<float> A = new float[] { float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.PositiveInfinity };

            using (Settings.Ensure(nameof(Settings.MaxNumberThreads), 3u)) {

                ILNumerics.Core.InnerLoops.GetLimits.Single.s_threading_overhead_Cont64 = 1;
                ILNumerics.Core.InnerLoops.GetLimitsNoInf.Single.s_threading_overhead_Cont64 = 1;

                float min, max;
                Assert.IsFalse(A.GetLimits(out min, out max, true));
                Assert.IsTrue(min == default(float) && max == default(float));

                Assert.IsTrue(A.GetLimits(out min, out max, false));
                Assert.IsTrue(min == float.PositiveInfinity && max == float.PositiveInfinity);

            }
        }
        [TestMethod]
        public void GetLimitsContNaNInfTests3() {

            Array<float> A = new float[] { float.NaN, float.NaN, float.NaN, float.NegativeInfinity, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN};

            using (Settings.Ensure(nameof(Settings.MaxNumberThreads), 3u)) {

                ILNumerics.Core.InnerLoops.GetLimits.Single.s_threading_overhead_Cont64 = 1;
                ILNumerics.Core.InnerLoops.GetLimitsNoInf.Single.s_threading_overhead_Cont64 = 1;

                float min, max;
                Assert.IsFalse(A.GetLimits(out min, out max, true));
                Assert.IsTrue(min == default(float) && max == default(float));

                Assert.IsTrue(A.GetLimits(out min, out max, false));
                Assert.IsTrue(min == float.NegativeInfinity && max == float.NegativeInfinity);

            }
        }
        [TestMethod]
        public void GetLimitsContNaNExcept1Tests() {
            Array<float> A = new float[] { float.NaN, float.NaN, float.NaN, -4.2f, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN };

            float min, max;
            Assert.IsTrue(A.GetLimits(out min, out max));
            Assert.IsTrue(min == -4.2f && max == -4.2f);

        }
        [TestMethod]
        public void GetLimitsScalarFloatMaxValueTests() {
            Array<float> A = new float[] { float.MaxValue };

            float min, max;
            Assert.IsTrue(A.GetLimits(out min, out max));
            Assert.IsTrue(min == float.MaxValue && max == float.MaxValue);

        }
        [TestMethod]
        public void GetLimitsScalarUIntMaxValueTests() {
            Array<uint> A = new uint[] { uint.MaxValue };

            uint min, max;
            Assert.IsTrue(A.GetLimits(out min, out max));
            Assert.IsTrue(min == uint.MaxValue && max == uint.MaxValue);

        }
        [TestMethod]
        public void GetLimitsScalarUIntMinValueTests() {
            Array<uint> A = new uint[] { uint.MinValue };

            uint min, max;
            Assert.IsTrue(A.GetLimits(out min, out max));
            Assert.IsTrue(min == uint.MinValue && max == uint.MinValue);

        }
        [TestMethod]
        public void GetLimitsScalarDoubleNANValueTests() {
            Array<double> A = new double[] { double.NaN };

            double min, max;
            Assert.IsFalse(A.GetLimits(out min, out max));
            Assert.IsTrue(min == 0 && max == 0);

        }
        [TestMethod]
        public void GetLimitsScalarDoubleMaxValueTests() {
            Array<double> A = new double[] { double.MaxValue };

            double min, max;
            Assert.IsTrue(A.GetLimits(out min, out max));
            Assert.IsTrue(min == double.MaxValue && max == double.MaxValue);

        }
        [TestMethod]
        public void GetLimitsScalarDoubleNegInfTests() {
            Array<double> A = new double[] { double.NegativeInfinity };

            double min, max;
            Assert.IsTrue(A.GetLimits(out min, out max));
            Assert.IsTrue(min == double.NegativeInfinity && max == double.NegativeInfinity);
            Assert.IsFalse(A.GetLimits(out min, out max, true));
            Assert.IsTrue(min == 0 && max == 0);

        }
        [TestMethod]
        public void GetLimitsContNaNExcept1FirstTests() {
            Array<float> A = new float[] { -4.2f, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN };

            float min, max;
            Assert.IsTrue(A.GetLimits(out min, out max));
            Assert.IsTrue(min == -4.2f && max == -4.2f);

        }
        [TestMethod]
        public void GetLimitsContNaNExcept1LastTests() {
            Array<float> A = new float[] { float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, -4.2f };

            float min, max;
            Assert.IsTrue(A.GetLimits(out min, out max));
            Assert.IsTrue(min == -4.2f && max == -4.2f);

        }
        [TestMethod]
        public void GetLimitsStridedLargeAllNANs() {
            float[] values = new float[1000 * 400];
            for (int i = 0; i < values.Length; i++) {
                values[i] = float.NaN;
            }
            Array<float> A = values;

            // make strided
            var bsd = A.S.GetBSD(true);
            // take each second 'row' only
            bsd[1] = (bsd[1] / 2);
            bsd[3] = (bsd[3] / 2);
            bsd[5] = (bsd[5] * 2);

            float min, max;
            Assert.IsFalse(A.GetLimits(out min, out max));
            Assert.IsTrue(min == default(float) && max == default(float));

        }
        [TestMethod]
        public void GetLimitsStridedLargeAllNANsMulti3() {
            float[] values = new float[1000 * 400];
            for (int i = 0; i < values.Length; i++) {
                values[i] = float.NaN;
            }
            Array<float> A = values;
            var oldThreads = Settings.MaxNumberThreads;
            // make strided
            var bsd = A.S.GetBSD(true);
            // take each second 'row' only
            bsd[1] = (bsd[1] / 2);
            bsd[3] = (bsd[3] / 2);
            bsd[5] = (bsd[5] * 2);

            try {
                Settings.MaxNumberThreads = 3;
                float min, max;
                Assert.IsFalse(A.GetLimits(out min, out max));
                Assert.IsTrue(min == default(float) && max == default(float));
            } finally {
                Settings.MaxNumberThreads = oldThreads;
            }

        }
        [TestMethod]
        public void GetLimitsStridedLargeAllBut1NANsMulti3() {
            float[] values = new float[999 * 400];
            for (int i = 0; i < values.Length; i++) {
                values[i] = float.NaN;
            }
            values[values.Length - 2] = 199;
            Array<float> A = values;
            var oldThreads = Settings.MaxNumberThreads;
            // make strided
            var bsd = A.S.GetBSD(true);
            // take each second 'row' only
            bsd[1] = (bsd[1] / 2);
            bsd[3] = (bsd[3] / 2);
            bsd[5] = (bsd[5] * 2);

            try {
                Settings.MaxNumberThreads = 3;
                float min, max;
                Assert.IsTrue(A.GetLimits(out min, out max));
                Assert.IsTrue(min == 199 && max == 199);
            } finally {
                Settings.MaxNumberThreads = oldThreads;
            }

        }
        [TestMethod]
        public void GetLimitsNegInfinityTestSimple1() {

            Array<float> A = new float[] { 1, -2, 3, -4, 5, 6, -7, 8, -9, float.NegativeInfinity, -11, 12 };

            float min, max;
            Assert.IsTrue(A.GetLimits(out min, out max));
            Assert.IsTrue(min == float.NegativeInfinity && max == 12);

            Assert.IsTrue(A.GetLimits(out min, out max, false));
            Assert.IsTrue(min == float.NegativeInfinity && max == 12);

            Assert.IsTrue(A.GetLimits(out min, out max, true));
            Assert.IsTrue(min == -11 && max == 12);

        }

        [TestMethod]
        public void GetLimitsComplexTestSimple() {

            Array<complex> A = new complex[,] {
                { new complex(), new complex(1, 12), new complex(3,4), new complex(5,6), new complex(7,8) },
                { new complex(), new complex(1, 2), new complex(13,4), new complex(5,6), new complex(7,8) }
            };

            complex min, max;
            Assert.IsTrue(A.GetLimits(out min, out max, true));
            Assert.IsTrue(min == new complex()); 
            Assert.IsTrue(max == new complex(13,12));

        }

        [TestMethod]
        public void GetLimitsComplexTestIgnoringInf() {

            Array<complex> A = new complex[,] {
                { new complex(), new complex(1, 12), new complex(3,-4), new complex(5,6), new complex(7,8),new complex(3,4), new complex(5,6), new complex(17,8) },
                { new complex(double.NegativeInfinity, -3), new complex(1, 2), new complex(5,6), new complex(3, double.PositiveInfinity), new complex(7,8),new complex(3,4), new complex(5,6), new complex(7,8) }
            };
            var bsd = A.S.GetBSD(true);
            bsd[0] =3;
            bsd[1] =12;
            bsd[2] =1;
            bsd[3] =3; 
            bsd[4] =2; 
            bsd[5] =2; 
            bsd[6] =1;
            bsd[7] =4;
            bsd[8] =6;

            complex min, max;
            Assert.IsTrue(A.GetLimits(out min, out max, false));
            Assert.IsTrue(min == new complex(double.NegativeInfinity, -4));
            Assert.IsTrue(max == new complex(17, double.PositiveInfinity));

            Assert.IsTrue(A.GetLimits(out min, out max, true));
            Assert.IsTrue(min == new complex(1, -4));
            Assert.IsTrue(max == new complex(17, 12));

        }

        [TestMethod]
        public void GetLimitsComplexIgnoreInfAndBaseOffsetLargeTest() {

            var vals = new fcomplex[10000];
            for (int i = 0; i < vals.Length; i++) {
                vals[i] = fcomplex.NaN;
            }
            vals[vals.Length / 2] = new fcomplex(float.PositiveInfinity, float.NaN);
            vals[10] = new fcomplex(float.NaN, float.PositiveInfinity);

            using (Settings.Ensure(nameof(Settings.MaxNumberThreads), 3u)) {

                Array<fcomplex> A = vals;

                fcomplex min, max;
                Assert.IsTrue(A.GetLimits(out min, out max, false));
                Assert.IsTrue(min == new complex(float.PositiveInfinity, float.PositiveInfinity));
                Assert.IsTrue(max == new complex(float.PositiveInfinity, float.PositiveInfinity));

                Assert.IsFalse(A.GetLimits(out min, out max, true));
                // for complex only: failure: return NaN in min & max
                Assert.IsTrue(complex.IsNaN(min));
                Assert.IsTrue(complex.IsNaN(max));

            }
        }
        [TestMethod]
        public void GetLimitsComplexIgnoreInfAndBaseOffsetLargeTest2() {

            var vals = new fcomplex[10000];
            for (int i = 0; i < vals.Length; i++) {
                vals[i] = fcomplex.NaN;
            }
            vals[vals.Length / 2] = new fcomplex(float.PositiveInfinity, float.NaN);
            vals[10] = new fcomplex(float.NaN, -10);

            using (Settings.Ensure(nameof(Settings.MaxNumberThreads), 3u)) {

                Array<fcomplex> A = vals;

                fcomplex min, max;
                Assert.IsTrue(A.GetLimits(out min, out max, false));
                Assert.IsTrue(min == new complex(float.PositiveInfinity, -10));
                Assert.IsTrue(max == new complex(float.PositiveInfinity, -10));

                Assert.IsFalse(A.GetLimits(out min, out max, true));
                // for complex only: failure: return NaN in min & max
                Assert.IsTrue(complex.IsNaN(min));
                Assert.IsTrue(double.IsNaN(max.real) && max.imag == -10);

            }
        }
    }
}
