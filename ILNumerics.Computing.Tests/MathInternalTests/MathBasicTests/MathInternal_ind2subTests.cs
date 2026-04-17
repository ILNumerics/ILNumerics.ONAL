using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;
using static ILNumerics.Globals;

namespace ILNumerics.Core.UnitTests.MathInternalTests {

    [TestClass]
    public class MathInternal_ind2subTests {

        [TestMethod]
        public void mathInternal_ind2sub_simpleTest() {

            Array<double> A1 = counter<double>(0.0, 1.0, 5, 4, 3, StorageOrders.ColumnMajor);
            Array<long> B = ind2sub(A1, counter<long>(0, 1, 5 * 4 * 3));

            Assert.IsTrue(B.S[1] == A1.S.NumberOfDimensions); 
            Assert.IsTrue(B.S[0] == 5 * 4 * 3);

            Array<long> I1 = (counter<long>(0, 1, 5, 1) * ones<long>(1, 4 * 3)).Reshape(5 * 4 * 3, 1, StorageOrders.ColumnMajor);
            Assert.IsTrue(B[full, 0].Equals(I1));

            Array<long> I2 = (counter<long>(0, 1, 4, 1).repeat(5).T * ones<long>(1, 3)).Reshape(5 * 4 * 3, 1, StorageOrders.ColumnMajor);
            Assert.IsTrue(B[full, 1].Equals(I2));

            Array<long> I3 = counter<long>(0, 1, 1, 3).repeat(5 * 4).T;
            Assert.IsTrue(B[full, 2].Equals(I3));

        }
        [TestMethod]
        public void mathInternal_ind2sub_3D_2Out() {

            Array<double> A1 = counter<double>(0.0, 1.0, 5, 4, 3, StorageOrders.ColumnMajor);
            Array<long> B = ind2sub(A1, counter<long>(0, 1, 5 * 4 * 3), nrOutDims: 2);

            Assert.IsTrue(B.S[1] == 2);
            Assert.IsTrue(B.S[0] == 5 * 4 * 3);

            Array<long> I1 = (counter<long>(0, 1, 5, 1) * ones<long>(1, 4 * 3)).Reshape(5 * 4 * 3, 1, StorageOrders.ColumnMajor);
            Assert.IsTrue(B[full, 0].Equals(I1));

            Array<long> I2 = (counter<long>(0, 1, 4, 1).repeat(5).T * ones<long>(1, 3)).Reshape(5 * 4 * 3, 1, StorageOrders.ColumnMajor);
            Array<long> I3 = counter<long>(0, 1, 1, 3).repeat(5 * 4).T;
            Assert.IsTrue(B[full, 1].Equals(I2 + 4 * I3));

        }
        [TestMethod]
        public void mathInternal_ind2sub_3D_4Out() {

            Array<double> A1 = counter<double>(0.0, 1.0, 5, 4, 3, StorageOrders.ColumnMajor);
            Array<long> B = ind2sub(A1, counter<long>(0, 1, 5 * 4 * 3), nrOutDims: 4);

            Assert.IsTrue(B.S[1] == 4);
            Assert.IsTrue(B.S[0] == 5 * 4 * 3);

            Array<long> I1 = (counter<long>(0, 1, 5, 1) * ones<long>(1, 4 * 3)).Reshape(5 * 4 * 3, 1, StorageOrders.ColumnMajor);
            Assert.IsTrue(B[full, 0].Equals(I1));

            Array<long> I2 = (counter<long>(0, 1, 4, 1).repeat(5).T * ones<long>(1, 3)).Reshape(5 * 4 * 3, 1, StorageOrders.ColumnMajor);
            Assert.IsTrue(B[full, 1].Equals(I2));

            Array<long> I3 = counter<long>(0, 1, 1, 3).repeat(5 * 4).T;
            Assert.IsTrue(B[full, 2].Equals(I3));

            Assert.IsTrue(allall(B[full, 3] == 0)); 
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void mathInternal_ind2sub_0D_fail() {
            using (Scope.Enter(arrayStyle: ArrayStyles.numpy)) {
                Array<double> A = 1;
                ind2sub(A, vector<long>(0, 0)); 
            }
        }

    }
}
