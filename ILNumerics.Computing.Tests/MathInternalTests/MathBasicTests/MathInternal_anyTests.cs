using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.Globals; 
using static ILNumerics.ILMath;

namespace ILNumerics.Core.UnitTests.MathInternalTests {
    [TestClass]
    public class MathInternal_anyTests {

        [TestMethod]
        public void MathInternal_any_Simple() {

            Array<double> A = counter(-2.0, 1.0, 5);

            Logical B = any(A, 0);
            Logical Res = new bool[] { true };

            Assert.IsTrue(B.Equals(Res.T));

        }
        [TestMethod]
        public void MathInternal_any_SimpleRowMajor() {

            Array<double> A = zeros<double>(5, 4);
            A.Storage.EnsureStorageOrder(StorageOrders.RowMajor);
            A[6] = double.NaN;

            Logical B = any(A, 0);
            Logical Res = new bool[] { false, true, false, false };

            Assert.IsTrue(B.Equals(Res));

            Res = new bool[] { false, true, false, false, false };
            Assert.IsTrue(Res.Equals(any(A, 1)));

        }

        [TestMethod]
        public void MathInternal_any_3D_ColumnRet() {

            Array<double> A = counter(1.0, 1.0, 5, 4, 3, StorageOrders.RowMajor);
            A.Storage.EnsureStorageOrder(StorageOrders.ColumnMajor);

            Logical B = any(A[ellipsis], 0);
            Logical Res = new bool[,,] {
            {
                   { true, true, true },
                   { true, true, true },
                   { true, true, true },
                   { true, true, true },
            } };
            Assert.IsTrue(B.Equals(Res));

            B = any(A[ellipsis], 1);
            Res = new bool[,,] {
            {{ true, true, true } },
            {{ true, true, true } },
            {{ true, true, true } },
            {{ true, true, true } },
            {{ true, true, true } }};
            Assert.IsTrue(Res.Equals(B));

        }
        [TestMethod]
        public void MathInternal_any_EmptyRow() {
            Array<float> A = counter<float>(1f, 1f, 5, 0, 3, StorageOrders.RowMajor);

            // empty along non-empty dimension works as expected
            Logical B = any(A, 0);
            Assert.IsTrue(B.S[0] == 1);
            Assert.IsTrue(B.S[1] == 0);
            Assert.IsTrue(B.S[2] == 3);

            B = any(A, 1);
            Assert.IsTrue(B.S[0] == 5);
            Assert.IsTrue(B.S[1] == 1);
            Assert.IsTrue(B.S[2] == 3);
            Assert.IsTrue(B.Equals(ones<float>(5, 1, 3) == 0));

            B = any(A, 2);
            Assert.IsTrue(B.S[0] == 5);
            Assert.IsTrue(B.S[1] == 0);
            Assert.IsTrue(B.S[2] == 1);

        }
        [TestMethod]
        public void MathInternal_any_EmptyCol() {
            Array<float> A = counter<float>(1f, 1f, 5, 0, 3, StorageOrders.ColumnMajor);

            // empty along non-empty dimension works as expected
            Logical B = any(A, 0);
            Assert.IsTrue(B.S[0] == 1);
            Assert.IsTrue(B.S[1] == 0);
            Assert.IsTrue(B.S[2] == 3);

            B = any(A, 1);
            Assert.IsTrue(B.S[0] == 5);
            Assert.IsTrue(B.S[1] == 1);
            Assert.IsTrue(B.S[2] == 3);
            Assert.IsTrue(B.Equals(ones<float>(5, 1, 3) == 0));

            B = any(A, 2);
            Assert.IsTrue(B.S[0] == 5);
            Assert.IsTrue(B.S[1] == 0);
            Assert.IsTrue(B.S[2] == 1);

        }

        [TestMethod]
        public void MathInternal_any_ScalarML() {

            Array<uint> A = 1;
            Logical R = true;
            Assert.IsTrue(any(A, 0).Equals(R));
            Assert.IsTrue(any(A, 1).Equals(R));
            //Assert.IsTrue(any(A, 2).Equals(R));

        }
        [TestMethod]
        public void MathInternal_any_ScalarNP() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                Array<uint> A = 1;
                Logical R = true;
                Assert.IsTrue(any(A));
                Assert.IsTrue(any(A, keepdim: true));
                Assert.IsTrue(any(A, keepdim: false));
                Assert.IsTrue(any(A, 0));
                Assert.IsTrue(any(A, 1));
                Assert.IsTrue(any(R));
                Assert.IsTrue(any(R,0));
                Assert.IsTrue(any(R,1));
                Assert.IsTrue(any(R,1,keepdim:false));
                //Assert.IsTrue(any(A, 2).Equals(R));
            }
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MathInternal_any_DimOutsideMyDims() {
            Array<float> A = zeros<float>(5, 4, 3);
            any(A, 3);
        }

        [TestMethod]
        public void MathInternal_anyKeepDimsFalseTest() {
            Array<uint> A = counter<uint>(1, 1, 5, 4, 3);
            Logical B = any(A, 0, false);
            Assert.IsTrue(B.S.NumberOfDimensions == 2);
            Assert.IsTrue(B.S[0] == 4);
            Assert.IsTrue(B.S[1] == 3);
            Assert.IsTrue(B.S[2] == 1);

            B = any(A, 1, false);
            Assert.IsTrue(B.S.NumberOfDimensions == 2);
            Assert.IsTrue(B.S[0] == 5);
            Assert.IsTrue(B.S[1] == 3);
            Assert.IsTrue(B.S[2] == 1);

            B = any(A, 2, false);
            Assert.IsTrue(B.S.NumberOfDimensions == 2);
            Assert.IsTrue(B.S[0] == 5);
            Assert.IsTrue(B.S[1] == 4);
            Assert.IsTrue(B.S[2] == 1);

        }

        [TestMethod]
        public void MathInternal_anyLogicalTest() {

            Logical A = (counter(-2.0, 1.0, 5) * counter(2.0, -1.0, 1, 4)) > 0;
            Logical B = any(A); // wd : 0
            Logical Res = new[] { true, true, false, true };
            Assert.IsTrue(B.Equals(Res.T));

            B = any(A, 1);
            Res = new[] { true, true, false, true, true };
            Assert.IsTrue(B.Equals(Res));

            Assert.IsTrue(any(true));
        }
        [TestMethod]
        public void MathInternal_anyNumpyScalarLogicalFail() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                // working dimension / dim must be inside the existing array dimensions!
                Assert.IsTrue(any(true));
            }
        }

        [TestMethod]
        public void MathInternal_any1D2npScalar() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = vector<double>(0, 0, 0, 0, 0, 0, 0, .2, 0, 0, 0, 0.1);
                Assert.IsTrue(A.S.NumberOfDimensions == 1);
                Assert.IsTrue(A.S.NumberOfElements == 12);

                Logical R = any(A);
                Assert.IsTrue(R.S.NumberOfDimensions == 1);
                Assert.IsTrue(R.S.NumberOfElements == 1);
                Assert.IsTrue(R == true);

                R = any(A, keepdim: false);
                Assert.IsTrue(R.S.NumberOfDimensions == 0);
                Assert.IsTrue(R.S.NumberOfElements == 1);
                Assert.IsTrue(R == true);

            }
        }


    }
}
