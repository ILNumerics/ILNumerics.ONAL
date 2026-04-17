using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.Globals; 
using static ILNumerics.ILMath;

namespace ILNumerics.Core.UnitTests.MathInternalTests {
    [TestClass]
    public class MathInternal_allTests {

        [TestMethod]
        public void MathInternal_all_Simple() {

            Array<double> A = counter(-2.0, 1.0, 5);

            Logical B = all(A, 0);
            Logical Res = new bool[] { false };

            Assert.IsTrue(B.Equals(Res.T));
            Assert.IsTrue(B.Storage.NumberTrues == 0); 

        }
        [TestMethod]
        public void MathInternal_all_SimpleRowMajor() {
            Array<double> A = zeros<double>(5, 4) + 1;
            A.Storage.EnsureStorageOrder(StorageOrders.RowMajor);
            A[6] = double.NaN;

            Logical B = all(A, 0);
            Logical Res = new bool[] { true, true, true, true };

            Assert.IsTrue(B.Equals(Res));
            Assert.IsTrue(B.Storage.NumberTrues == 4, $"{B.Storage.NumberTrues}");

            Res = new bool[] { true, true, true, true, true };
            Assert.IsTrue(Res.Equals(all(A, 1)));

            A[1,end] = 0;

            B = all(A, 0);
            Res = new bool[] { true, true, true, false };

            Assert.IsTrue(B.Equals(Res));
            Assert.IsTrue(B.Storage.NumberTrues == 3, $"{B.Storage.NumberTrues}");

            Res = new bool[] { true, false, true, true, true };
            Assert.IsTrue(Res.Equals(all(A, 1)));
            Assert.IsTrue(all(A, 1).Storage.NumberTrues == 4, $"{B.Storage.NumberTrues}");
        }

        [TestMethod]
        public void MathInternal_all_3D_ColumnRet() {
            //Assert.Fail("TODO");

            Array<double> A = counter(1.0, 1.0, 5, 4, 3, StorageOrders.RowMajor);
            A.Storage.EnsureStorageOrder(StorageOrders.ColumnMajor);

            Logical B = all(A[ellipsis], 0);
            Logical Res = new bool[,,] {
            {
                   { true, true, true },
                   { true, true, true },
                   { true, true, true },
                   { true, true, true },
            } };
            Assert.IsTrue(B.Equals(Res));

            B = all(A[ellipsis], 1);
            Res = new bool[,,] {
            {{ true, true, true } },
            {{ true, true, true } },
            {{ true, true, true } },
            {{ true, true, true } },
            {{ true, true, true } }};
            Assert.IsTrue(Res.Equals(B));

        }
        [TestMethod]
        public void MathInternal_all_EmptyRow() {
            Array<float> A = counter<float>(1f, 1f, 5, 0, 3, StorageOrders.RowMajor);

            // empty along non-empty dimension works as expected
            Logical B = all(A, 0);
            Assert.IsTrue(B.S[0] == 1);
            Assert.IsTrue(B.S[1] == 0);
            Assert.IsTrue(B.S[2] == 3);

            B = all(A, 1);
            Assert.IsTrue(B.S[0] == 5);
            Assert.IsTrue(B.S[1] == 1);
            Assert.IsTrue(B.S[2] == 3);
            Assert.IsTrue(B.Equals(ones<float>(5, 1, 3) == 0));

            B = all(A, 2);
            Assert.IsTrue(B.S[0] == 5);
            Assert.IsTrue(B.S[1] == 0);
            Assert.IsTrue(B.S[2] == 1);

        }
        [TestMethod]
        public void MathInternal_all_EmptyCol() {
            Array<float> A = counter<float>(1f, 1f, 5, 0, 3, StorageOrders.ColumnMajor);

            // empty along non-empty dimension works as expected
            Logical B = all(A, 0);
            Assert.IsTrue(B.S[0] == 1);
            Assert.IsTrue(B.S[1] == 0);
            Assert.IsTrue(B.S[2] == 3);

            B = all(A, 1);
            Assert.IsTrue(B.S[0] == 5);
            Assert.IsTrue(B.S[1] == 1);
            Assert.IsTrue(B.S[2] == 3);
            Assert.IsTrue(B.Equals(ones<float>(5, 1, 3) == 0));

            B = all(A, 2);
            Assert.IsTrue(B.S[0] == 5);
            Assert.IsTrue(B.S[1] == 0);
            Assert.IsTrue(B.S[2] == 1);

        }

        [TestMethod]
        public void MathInternal_all_ScalarML() {

            Array<uint> A = 1;
            Logical R = true;
            Assert.IsTrue(all(A, 0).Equals(R));
            Assert.IsTrue(all(A, 1).Equals(R));
            //Assert.IsTrue(all(A, 2).Equals(R));

        }
        [TestMethod]
        public void MathInternal_all_ScalarNP() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                Array<uint> A = 1;
                Logical R = true;
                Assert.IsTrue(all(A));
                Assert.IsTrue(all(A, 0).Equals(R));
                Assert.IsTrue(all(A, 1).Equals(R));
                Assert.IsTrue(all(R));
                Assert.IsTrue(any(R, keepdim: false));
            }
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MathInternal_all_DimOutsideMyDims() {
            Array<float> A = zeros<float>(5, 4, 3);
            all(A, 3);
        }

        [TestMethod]
        public void MathInternal_allKeepDimsFalseTest() {
            Array<uint> A = counter<uint>(1, 1, 5, 4, 3);
            Logical B = all(A, 0, false);
            Assert.IsTrue(B.S.NumberOfDimensions == 2);
            Assert.IsTrue(B.S[0] == 4);
            Assert.IsTrue(B.S[1] == 3);
            Assert.IsTrue(B.S[2] == 1);

            B = all(A, 1, false);
            Assert.IsTrue(B.S.NumberOfDimensions == 2);
            Assert.IsTrue(B.S[0] == 5);
            Assert.IsTrue(B.S[1] == 3);
            Assert.IsTrue(B.S[2] == 1);

            B = all(A, 2, false);
            Assert.IsTrue(B.S.NumberOfDimensions == 2);
            Assert.IsTrue(B.S[0] == 5);
            Assert.IsTrue(B.S[1] == 4);
            Assert.IsTrue(B.S[2] == 1);

        }

        [TestMethod]
        public void MathInternal_allLogicalTest() {

            Logical A = (counter(-2.0, 1.0, 5) * counter(2.0, -1.0, 1, 4)) > 0;
            Logical B = all(A); // wd : 0
            Logical Res = new[] { false, false, false, false };
            Assert.IsTrue(B.Equals(Res.T));

            B = all(A, 1);
            Res = new[] { false, false, false, false, false };
            Assert.IsTrue(B.Equals(Res));

            Assert.IsTrue(all(true));
        }
        [TestMethod]
        public void MathInternal_allNumpyScalarLogical() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Assert.IsTrue(all(true));
            }
        }

        [TestMethod]
        public void MathInternal_allLongVector_autoWorkDim() {

            Array<short> A = counter<short>(-1, -1, 100, 100);

            Array<long> I = 1;
            min(A, I: I, dim: 0).Dispose();

            Logical L = all(I == 99); // auto work dim: 1

            Assert.IsTrue(L.IsScalar); 
            Assert.IsTrue(L);

            I[end] = 2;

            L = all(I == 99); // auto work dim: 1
            Assert.IsTrue(L.IsScalar);
            Assert.IsFalse(L);
            Assert.IsTrue(any(I == 2)); 

            I[end] = 99;
            L = all(I == 99); // auto work dim: 1
            Assert.IsTrue(L.IsScalar);
            Assert.IsTrue(L);
            Assert.IsFalse(any(I == 2));

        }
    }
}
