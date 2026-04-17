using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILNumerics; 
using static ILNumerics.Globals;
using static ILNumerics.ILMath;

namespace ILNumerics.Core.Tests {

    [TestClass]
    public class Generic_Tests {

        [TestMethod]
        public void EnsureStorageOrder_AbitraryStructs() {

            var myStructs = new MyStruct[4] {
                new MyStruct() { A = 1, B = 2, C = 3 },
                new MyStruct() { A = 4, B = 5, C = 6 },
                new MyStruct() { A = 7, B = 8, C = 9 },
                new MyStruct() { A = 10, B = 11, C = 12 }
            };

            Array<MyStruct> A = myStructs;
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);
            A.a = A.Reshape(2, 2);
            Assert.IsTrue(A.shape.Equals(size(2, 2)));
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);

            var myOutStructs = new MyStruct[4];
            A.ExportValues(ref myOutStructs, layout: StorageOrders.RowMajor); 
            
            Assert.IsTrue(myStructs[0].Equals(myOutStructs[0]));
            Assert.IsTrue(myStructs[1].Equals(myOutStructs[2]));
            Assert.IsTrue(myStructs[2].Equals(myOutStructs[1]));
            Assert.IsTrue(myStructs[3].Equals(myOutStructs[3]));

            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);

            A.GetHostPointerForWrite(StorageOrders.RowMajor);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor);
            Assert.IsTrue(A.GetValue(0,0).Equals(myStructs[0]));
            Assert.IsTrue(A.GetValue(1,0).Equals(myStructs[1]));
            Assert.IsTrue(A.GetValue(0,1).Equals(myStructs[2]));
            Assert.IsTrue(A.GetValue(1,1).Equals(myStructs[3]));

        }



        [TestMethod]
        public void NumpyMaxTest() {
            InArray<int> inArrayA = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            InArray<int> maxArray = inArrayA.max(1);

            //ArrayAssert.AreEqual(new[] { 3, 6, 9 }, maxArray.ToArray());
        }
        [TestMethod]
        public void NegativeStepIndexing() {
            Array<double> array = arange(0.0, 10.0);
            var subArray1 = array[arange(9, -2, 0)];
            var expected = new double[] { 9, 7, 5, 3, 1 };
            //ArrayAssert.AreEqual(expected, subArray1.ToArray());
        }


        [TestMethod]
        public void LicenseOutput_simple() {

            Array<double> A = vector(0.0, 0, 1) * ones(3, 4, 5);
            Array<double> B = vector(0.0, 1, 0) * ones(3, 4, 5);

            Array<double> C = cross(A, B); 

            Array<double> Res = vector(-1.0, 0, 0) * ones(3, 4, 5);

            Array<double> Af = vector(0.0, 0, 1), Bf = vector(0.0, 1, 0), Cf = vector(-1.0, 0, 0);
            
            Assert.IsTrue(cross(Af * ones(3, 4, 5), Bf * ones(3, 4, 5)).Equals(Cf * ones(3, 4, 5))); 
            Assert.IsTrue(cross(Af.T * ones(4, 3, 5), Bf.T * ones(4, 3, 5)).Equals(Cf.T * ones(4, 3, 5)));
            using (Scope.Enter(ArrayStyles.numpy)) {
                Assert.IsTrue(cross(Af[newaxis] * ones(4, 3, 5), Bf[newaxis] * ones(4, 3, 5)).Equals(Cf[newaxis] * ones(4, 3, 5)));
                Assert.IsTrue(cross(Af.T[newaxis] * ones(4, 5, 3), Bf.T[newaxis] * ones(4, 5, 3)).Equals(Cf.T[newaxis] * ones(4, 5, 3)));
            }
        }

    }
}
