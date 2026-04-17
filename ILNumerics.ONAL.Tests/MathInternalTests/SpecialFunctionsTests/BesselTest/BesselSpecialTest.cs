using System;
using ILNumerics.Core.Functions.Builtin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILNumerics;
using static ILNumerics.Globals;
using static ILNumerics.ILMath; 

namespace ILNumerics.UnitTests.SpecialFunctionsTest.BesselTest
{
    [TestClass]
    public class BesselSpecialTest 
    {
        [TestMethod]
        public void FrexpTest()
        {
            TestOne(double.NaN);
            TestOne(double.NegativeInfinity);
            TestOne(double.PositiveInfinity);
            TestOne(8.4);
            TestOne(+0.0);
            TestOne(-0.0);
            TestOne(1230.0);
            TestOne(34324.6563);

            for (int i = 0; i < 10000; i++)
            {
                TestOne((double)randn(1, 1) * (double)rand(1, 1) * 10);
            }
        }

        private static void TestOne(double onNumber)
        {
            int k = 0;
            double mantissa = SpecialFunctionsHelper.frexp(onNumber, ref k);
            Assert.AreEqual(onNumber, mantissa * Math.Pow(2, k));
        }

        [TestMethod]
        public void BesselGeneratorNaNTest()
        {
            Assert.AreEqual(double.NaN, (double)besselGenerator_INTERNAL(-5, SpecialFunctionsHelper.besselJnElem, 0));
            Assert.AreEqual(double.NaN, (double)besselGenerator_INTERNAL(-5, SpecialFunctionsHelper.besselYnElem, 0));
            Assert.AreEqual(double.NaN, (double)besselGenerator_INTERNAL(-5, SpecialFunctionsHelper.besselModKnElem, 0));
            Assert.AreEqual(double.NaN, (double)besselGenerator_INTERNAL(-5, SpecialFunctionsHelper.besselModInElem, 0));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BesselGeneratorNullTest1()
        {
            besselGenerator_INTERNAL(null, SpecialFunctionsHelper.besselJnElem, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BesselGeneratorNullTest2()
        {
            besselGenerator_INTERNAL(null, SpecialFunctionsHelper.besselYnElem, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BesselGeneratorNullTest3()
        {   
            besselGenerator_INTERNAL(null, SpecialFunctionsHelper.besselModKnElem, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BesselGeneratorNullTest4()
        {
            besselGenerator_INTERNAL(null, SpecialFunctionsHelper.besselModInElem, 0);
        }

        [TestMethod]
        public void BesselGeneratorEmptyTest1()
        {
            Assert.IsTrue(besselGenerator_INTERNAL(zeros(0, 0), SpecialFunctionsHelper.besselJnElem, 0).IsEmpty);
            Assert.IsTrue(besselGenerator_INTERNAL(zeros(0, 0), SpecialFunctionsHelper.besselJnElem, 0).S[0] == 0);
            Assert.IsTrue(besselGenerator_INTERNAL(zeros(0, 0), SpecialFunctionsHelper.besselJnElem, 0).S[1] == 0);
            Assert.IsTrue(besselGenerator_INTERNAL(zeros(0, 0), SpecialFunctionsHelper.besselJnElem, 0).S.NumberOfDimensions == 2);
        }

        [TestMethod]
        public void BesselGeneratorEmptyTest2()
        {
            besselGenerator_INTERNAL(zeros(0, 0), SpecialFunctionsHelper.besselYnElem, 0);
        }

        [TestMethod]
        public void BesselGeneratorEmptyTest3()
        {
            besselGenerator_INTERNAL(zeros(0, 0), SpecialFunctionsHelper.besselModKnElem, 0);
        }

        [TestMethod]
        public void BesselGeneratorEmptyTest4()
        {
            besselGenerator_INTERNAL(zeros(0, 0), SpecialFunctionsHelper.besselModInElem, 0);
        }

        [TestMethod]
        public void BesselGeneratorOkTest1()
        {
            double res = (double)besselGenerator_INTERNAL(2, SpecialFunctionsHelper.besselJnElem, 0);
            Assert.AreNotEqual(double.NaN, res);
            Assert.IsFalse(double.IsInfinity(res));
            Assert.AreNotEqual(0, res);
        }

        [TestMethod]
        public void BesselGeneratorOkTest2()
        {
            double res = (double)besselGenerator_INTERNAL(2, SpecialFunctionsHelper.besselYnElem, 0);
            Assert.AreNotEqual(double.NaN, res);
            Assert.IsFalse(double.IsInfinity(res));
            Assert.AreNotEqual(0, res);
        }

        [TestMethod]
        public void BesselGeneratorOkTest3()
        {
            double res = (double)besselGenerator_INTERNAL(2, SpecialFunctionsHelper.besselModKnElem, 0);
            Assert.AreNotEqual(double.NaN, res);
            Assert.IsFalse(double.IsInfinity(res));
            Assert.AreNotEqual(0, res);
        }

        [TestMethod]
        public void BesselGeneratorOkTest4()
        {
            double res = (double)besselGenerator_INTERNAL(2, SpecialFunctionsHelper.besselModInElem, 0);
            Assert.AreNotEqual(double.NaN, res);
            Assert.IsFalse(double.IsInfinity(res));
            Assert.AreNotEqual(0, res);
        }

        #region double tests
        [TestMethod]
        public void BesselJ0ShapeTestDouble()
        {
            // starts from 1 goes around zero up and down
            Assert.IsTrue((1.0 - (double)besselJ0(0d)) < 1e-6);
            Assert.IsTrue(maxall(diff(besselJ0(linspace<double>(0, 3.75)))) <= 0);
            Assert.IsTrue(minall(diff(besselJ0(linspace<double>(4.25, 6.75)))) >= 0);
            Assert.IsTrue(maxall(diff(besselJ0(linspace<double>(7.25, 9)))) <= 0);
            Assert.IsTrue((0.0 - (double)besselJ0(double.PositiveInfinity)) < 1e-6);
        }

        [TestMethod]
        public void BesselJ1ShapeTestDouble()
        {
            // starts from 0 goes around zero up and down
            Assert.IsTrue((0.0 - (double)besselJ1(0d)) < 1e-6);
            Assert.IsTrue(minall(diff(besselJ1(linspace<double>(0, 1.75)))) >= 0);
            Assert.IsTrue(maxall(diff(besselJ1(linspace<double>(2.25, 3.75)))) <= 0);
            Assert.IsTrue(minall(diff(besselJ1(linspace<double>(7, 8.5)))) >= 0);
            Assert.IsTrue((0.0 - (double)besselJ1(double.PositiveInfinity)) < 1e-6);
        }


        [TestMethod]
        public void BesselJ2ShapeTestDouble()
        {
            // starts from 0 goes around zero up and down
            Assert.IsTrue((0.0 - (double)besselJn(0d, 2)) < 1e-6);
            Assert.IsTrue(minall(diff(besselJn(linspace<double>(0, 2.75), 2))) >= 0);
            Assert.IsTrue(maxall(diff(besselJn(linspace<double>(3.5, 6), 2))) <= 0);
            Assert.IsTrue(minall(diff(besselJn(linspace<double>(7, 9), 2))) >= 0);
            Assert.IsTrue((0.0 - (double)besselJn(double.PositiveInfinity, 2)) < 1e-6);
        }

        [TestMethod]
        public void BesselJnShapeTestDouble()
        {
            for (int n = 3; n < 40; n++)
            {
                Assert.IsTrue((0.0 - (double)besselJn(0d, n)) < 1e-6);
                Assert.IsTrue((0.0 - (double)besselJn(double.PositiveInfinity, n)) < 1e-6);
                Assert.AreEqual(double.NaN, (double)besselJn(-1d, n));
            }
        }

        [TestMethod]
        public void BesselY0ShapeTestDouble()
        {
            // starts from -inf goes around zero up and down
            Assert.AreEqual(double.NegativeInfinity, (double)besselY0(0d));
            Assert.AreNotEqual(double.NegativeInfinity, (double)besselY0(1e-6));
            Assert.AreNotEqual(double.NaN, (double)besselY0(1e-6));

            Assert.IsTrue(minall(diff(besselY0(linspace<double>(1e-6, 2)))) >= 0);
            Assert.IsTrue(maxall(diff(besselY0(linspace<double>(2.25, 5)))) <= 0);
            Assert.IsTrue(minall(diff(besselY0(linspace<double>(6, 7.5)))) >= 0); // ho: 8 is a threshold, triggering another algorithm. Both don't exactly match, hence produce a non-smoothness at the transition. Dunno if this is expected.

            Assert.IsTrue((0.0 - (double)besselY0(double.PositiveInfinity)) < 1e-6);
        }

        [TestMethod]
        public void BesselY1ShapeTestDouble()
        {
            // starts from -inf goes around zero up and down
            Assert.AreEqual(double.NegativeInfinity, (double)besselY1(0d));
            Assert.AreNotEqual(double.NegativeInfinity, (double)besselY1(1e-6));
            Assert.AreNotEqual(double.NaN, (double)besselY1(1e-6));

            Assert.IsTrue(minall(diff(besselY1(linspace<double>(1e-6, 3.5)))) >= 0);
            Assert.IsTrue(maxall(diff(besselY1(linspace<double>(4, 6)))) <= 0);
            Assert.IsTrue(minall(diff(besselY1(linspace<double>(7, 7.9)))) >= 0);

            Assert.IsTrue((0.0 - (double)besselY1(double.PositiveInfinity)) < 1e-6);
        }


        [TestMethod]
        public void BesselY2ShapeTestDouble()
        {
            // starts from -inf goes around zero up and down
            Assert.AreEqual(double.NegativeInfinity, (double)besselYn(0d, 2));
            Assert.AreNotEqual(double.NegativeInfinity, (double)besselYn(1e-6, 2));
            Assert.AreNotEqual(double.NaN, (double)besselYn(1e-6, 2));

            Assert.IsTrue(minall(diff(besselYn(linspace<double>(1e-6, 5), 2))) >= 0);
            Assert.IsTrue(maxall(diff(besselYn(linspace<double>(5.5, 7.9), 2))) <= 0);
            Assert.IsTrue(minall(diff(besselYn(linspace<double>(9, 10), 2))) >= 0);

            Assert.IsTrue((0.0 - (double)besselYn(double.PositiveInfinity, 2)) < 1e-6);
        }

        [TestMethod]
        public void BesselYnShapeTestDouble()
        {
            for (int n = 3; n < 40; n++)
            {
                Assert.AreEqual(double.NegativeInfinity, (double)besselYn(0d, n));
                Assert.AreNotEqual(double.NegativeInfinity, (double)besselYn(1e-6, n));
                Assert.AreNotEqual(double.NaN, (double)besselYn(1e-6, n));
                Assert.IsTrue((0.0 - (double)besselYn(double.PositiveInfinity, n)) < 1e-6);
            }
        }

        [TestMethod]
        public void BesselK0ShapeTestDouble()
        {
            Assert.AreEqual(double.PositiveInfinity, (double)besselModifiedK0(0d));
            Assert.IsTrue(maxall(diff(besselModifiedK0(linspace<double>(1e-6, 10)))) <= 0);
            Assert.AreEqual(0, (double)besselModifiedK0(double.PositiveInfinity));
        }

        [TestMethod]
        public void BesselK1ShapeTestDouble()
        {
            Assert.AreEqual(double.PositiveInfinity, (double)besselModifiedK1(0d));
            Assert.IsTrue(maxall(diff(besselModifiedK1(linspace<double>(1e-6, 10)))) <= 0);
            Assert.AreEqual(0, (double)besselModifiedK1(double.PositiveInfinity));
        }

        [TestMethod]
        public void BesselK2ShapeTestDouble()
        {
            Assert.AreEqual(double.PositiveInfinity, (double)besselModifiedKn(0d, 2));
            Assert.IsTrue(maxall(diff(besselModifiedKn(linspace<double>(1e-6, 10), 2))) <= 0);
            Assert.AreEqual(0, (double)besselModifiedKn(double.PositiveInfinity, 2));
        }

        [TestMethod]
        public void BesselKnShapeTestDouble()
        {
            for (int n = 3; n < 40; n++)
            {
                Assert.AreEqual(double.PositiveInfinity, (double)besselModifiedKn(0d, n));
                Assert.IsTrue(maxall(diff(besselModifiedKn(linspace<double>(1e-6, 10), n))) <= 0);
                Assert.AreEqual(0, (double)besselModifiedKn(double.PositiveInfinity, n));
            }
        }

        [TestMethod]
        public void BesselI0ShapeTestDouble()
        {
            Assert.IsTrue((1 - (double)besselModifiedI0(0d)) < 1e-6);
            Assert.IsTrue(minall(diff(besselModifiedI0(linspace<double>(1e-6, 10)))) >= 0);
            Assert.AreEqual(double.PositiveInfinity, (double)besselModifiedI0(double.PositiveInfinity));
        }

        [TestMethod]
        public void BesselI1ShapeTestDouble()
        {
            Assert.AreEqual(0, (double)besselModifiedI1(0d));
            Assert.IsTrue(minall(diff(besselModifiedI1(linspace<double>(1e-6, 10)))) >= 0);
            Assert.AreEqual(double.PositiveInfinity, (double)besselModifiedI1(double.PositiveInfinity));
        }

        [TestMethod]
        public void BesselI2ShapeTestDouble()
        {
            Assert.AreEqual(0, (double)besselModifiedIn(0d, 2));
            Assert.IsTrue(minall(diff(besselModifiedIn(linspace<double>(1e-6, 10), 2))) >= 0);
            Assert.AreEqual(double.PositiveInfinity, (double)besselModifiedIn(double.PositiveInfinity, 2));
        }

        [TestMethod]
        public void BesselInShapeTestDouble()
        {
            for (int n = 3; n < 17; n++)
            {
                Assert.AreEqual(0, (double)besselModifiedIn(0d, n));

                Assert.IsTrue(minall(diff(besselModifiedIn(linspace<double>(1e-6, 10), n))) >= 0, $"n:{n}");
                Assert.AreEqual(double.PositiveInfinity, (double)besselModifiedIn(double.PositiveInfinity, n));
            }
        }

#endregion

        #region float tests
        [TestMethod]
        public void BesselJ0ShapeTestFloat()
        {
            // starts from 1 goes around zero up and down
            Assert.IsTrue((1.0f - (float)besselJ0(vector<float>(0f))) < 1e-6f);
            Assert.IsTrue(maxall(diff(besselJ0(linspace<float>(0, 3.75f, 100)))) <= 0f);
            Assert.IsTrue(minall(diff(besselJ0(linspace<float>(4.25f, 6.75f, 100)))) >= 0f);
            Assert.IsTrue(maxall(diff(besselJ0(linspace<float>(7.25f, 9, 100)))) <= 0f);
            Assert.IsTrue((0.0f - (float)besselJ0(vector<float>(float.PositiveInfinity))) < 1e-6f);
        }

        [TestMethod]
        public void BesselJ1ShapeTestFloat()
        {
            // starts from 0 goes around zero up and down
            Assert.IsTrue((0.0f - (float)besselJ1(vector<float>(0f))) < 1e-6f);
            Assert.IsTrue(minall(diff(besselJ1(linspace<float>(0, 1.75f, 100)))) >= 0f);
            Assert.IsTrue(maxall(diff(besselJ1(linspace<float>(2.25f, 3.75f, 100)))) <= 0f);
            Assert.IsTrue(minall(diff(besselJ1(linspace<float>(7, 8.5f, 100)))) >= 0f);
            Assert.IsTrue((0.0 - (float)besselJ1(vector<float>(float.PositiveInfinity))) < 1e-6f);
        }


        [TestMethod]
        public void BesselJ2ShapeTestFloat()
        {
            // starts from 0 goes around zero up and down
            Assert.IsTrue((0.0f - (float)besselJn(vector<float>(0f), 2)) < 1e-6f);
            Assert.IsTrue(minall(diff(besselJn(linspace<float>(0, 2.75f, 100), 2))) >= 0f);
            Assert.IsTrue(maxall(diff(besselJn(linspace<float>(3.5f, 6, 100), 2))) <= 0f);
            Assert.IsTrue(minall(diff(besselJn(linspace<float>(7, 9, 100), 2))) >= 0f);
            Assert.IsTrue((0.0f - (float)besselJn(vector<float>(float.PositiveInfinity), 2)) < 1e-6f);
        }

        [TestMethod]
        public void BesselJnShapeTestFloat()
        {
            for (int n = 3; n < 17; n++)
            {
                Assert.IsTrue((0.0f - (float)besselJn(vector<float>(0f), n)) < 1e-6f);
                Assert.IsTrue((0.0f - (float)besselJn(vector<float>(float.PositiveInfinity), n)) < 1e-6f);
                Assert.AreEqual(float.NaN, (float)besselJn(vector<float>(-1f), n));
            }
        }

        [TestMethod]
        public void BesselY0ShapeTestFloat()
        {
            // starts from -inf goes around zero up and down
            Assert.AreEqual(float.NegativeInfinity, (float)besselY0(vector<float>(0f)));
            Assert.AreNotEqual(float.NegativeInfinity, (float)besselY0(vector<float>(1e-6f)));
            Assert.AreNotEqual(float.NaN, (float)besselY0(1e-6));

            Assert.IsTrue(minall(diff(besselY0(linspace<float>(1e-6f, 2, 100)))) >= 0f);
            Assert.IsTrue(maxall(diff(besselY0(linspace<float>(2.25f, 5, 100)))) <= 0f);
            Assert.IsTrue(minall(diff(besselY0(linspace<float>(6, 7.5f, 100)))) >= 0f);

            Assert.IsTrue((0.0f - (float)besselY0(vector<float>(float.PositiveInfinity))) < 1e-6f);
        }

        [TestMethod]
        public void BesselY1ShapeTestFloat()
        {
            // starts from -inf goes around zero up and down
            Assert.AreEqual(float.NegativeInfinity, (float)besselY1(vector<float>(0f)));
            Assert.AreNotEqual(float.NegativeInfinity, (float)besselY1(vector<float>(1e-6f)));
            Assert.AreNotEqual(float.NaN, (float)besselY1(vector<float>(1e-6f)));

            Assert.IsTrue(minall(diff(besselY1(linspace<float>(1e-6f, 3.5f, 100)))) >= 0f);
            Assert.IsTrue(maxall(diff(besselY1(linspace<float>(4, 6, 100)))) <= 0f);
            Assert.IsTrue(minall(diff(besselY1(linspace<float>(7, 7.9f, 100)))) >= 0f);

            Assert.IsTrue((0.0 - (float)besselY1(vector<float>(float.PositiveInfinity))) < 1e-6f);
        }


        [TestMethod]
        public void BesselY2ShapeTestFloat()
        {
            // starts from -inf goes around zero up and down
            Assert.AreEqual(float.NegativeInfinity, (float)besselYn(vector<float>(0f), 2));
            Assert.AreNotEqual(float.NegativeInfinity, (float)besselYn(vector<float>(1e-6f), 2));
            Assert.AreNotEqual(float.NaN, (float)besselYn(vector<float>(1e-6f), 2));

            Assert.IsTrue(minall(diff(besselYn(linspace<float>(1e-6f, 5, 100), 2))) >= 0f);
            Assert.IsTrue(maxall(diff(besselYn(linspace<float>(5.5f, 7.9f, 100), 2))) <= 0f);
            Assert.IsTrue(minall(diff(besselYn(linspace<float>(9, 10, 100), 2))) >= 0f);

            Assert.IsTrue((0.0f - (float)besselYn(vector<float>(float.PositiveInfinity), 2)) < 1e-6f);
        }

        [TestMethod]
        public void BesselYnShapeTestFloat()
        {
            // only works until 6th order
            for (int n = 3; n < 6; n++)
            {
                Assert.AreEqual(float.NegativeInfinity, (float)besselYn(vector<float>(0f), n));
                Assert.AreNotEqual(float.NegativeInfinity, (float)besselYn(vector<float>(1e-6f), n));
                Assert.AreNotEqual(float.NaN, (float)besselYn(vector<float>(1e-6f), n));
                Assert.IsTrue((0.0f - (float)besselYn(vector<float>(float.PositiveInfinity), n)) < 1e-6f);
            }
        }

        [TestMethod]
        public void BesselK0ShapeTestFloat()
        {
            Assert.AreEqual(float.PositiveInfinity, (float)besselModifiedK0(vector<float>(0f)));
            Assert.IsTrue(maxall(diff(besselModifiedK0(linspace<float>(1e-6f, 10, 100)))) <= 0f);
            Assert.AreEqual(0f, (float)besselModifiedK0(vector<float>(float.PositiveInfinity)));
        }

        [TestMethod]
        public void BesselK1ShapeTestFloat()
        {
            Assert.AreEqual(float.PositiveInfinity, (float)besselModifiedK1(vector<float>(0f)));
            Assert.IsTrue(maxall(diff(besselModifiedK1(linspace<float>(1e-6f, 10, 100)))) <= 0f);
            Assert.AreEqual(0f, (float)besselModifiedK1(vector<float>(float.PositiveInfinity)));
        }

        [TestMethod]
        public void BesselK2ShapeTestFloat()
        {
            Assert.AreEqual(float.PositiveInfinity, (float)besselModifiedKn(vector<float>(0f), 2));
            Assert.IsTrue(maxall(diff(besselModifiedKn(linspace<float>(1e-6f, 10, 100), 2))) <= 0f);
            Assert.AreEqual(0f, (float)besselModifiedKn(vector<float>(float.PositiveInfinity), 2));
        }

        [TestMethod]
        public void BesselKnShapeTestFloat()
        {
            for (int n = 3; n < 40; n++)
            {
                Assert.AreEqual(float.PositiveInfinity, (float)besselModifiedKn(vector<float>(0f), n));
                Assert.IsTrue(maxall(diff(besselModifiedKn(linspace<float>(1e-6f, 10, 100), n))) <= 0f);
                Assert.AreEqual(0f, (float)besselModifiedKn(vector<float>(float.PositiveInfinity), n));
            }
        }

        [TestMethod]
        public void BesselI0ShapeTestFloat()
        {
            Assert.IsTrue((1 - (float)besselModifiedI0(vector<float>(0f))) < 1e-6f);
            Assert.IsTrue(minall(diff(besselModifiedI0(linspace<float>(1e-6f, 10, 100)))) >= 0f);
            Assert.AreEqual(float.PositiveInfinity, (float)besselModifiedI0(vector<float>(float.PositiveInfinity)));
        }

        [TestMethod]
        public void BesselI1ShapeTestFloat()
        {
            Assert.AreEqual(0, (float)besselModifiedI1(vector<float>(0f)));
            Assert.IsTrue(minall(diff(besselModifiedI1(linspace<float>(1e-6f, 10, 100)))) >= 0f);
            Assert.AreEqual(float.PositiveInfinity, (float)besselModifiedI1(vector<float>(float.PositiveInfinity)));
        }

        [TestMethod]
        public void BesselI2ShapeTestFloat()
        {
            Assert.AreEqual(0, (float)besselModifiedIn(vector<float>(0f), 2));
            Assert.IsTrue(minall(diff(besselModifiedIn(linspace<float>(1e-6f, 10, 100), 2))) >= 0f);
            Assert.AreEqual(float.PositiveInfinity, (float)besselModifiedIn(vector<float>(float.PositiveInfinity), 2));
        }

        [TestMethod]
        public void BesselInShapeTestFloat()
        {
            for (int n = 3; n < 17; n++)
            {
                Assert.AreEqual(0, (float)besselModifiedIn(vector<float>(0f), n));
                Assert.IsTrue(minall(diff(besselModifiedIn(linspace<float>(1e-6f, 10, 100), n))) >= 0f);
                Assert.AreEqual(float.PositiveInfinity, (float)besselModifiedIn(vector<float>(float.PositiveInfinity), n));
            }
        }
        #endregion
    }
}
