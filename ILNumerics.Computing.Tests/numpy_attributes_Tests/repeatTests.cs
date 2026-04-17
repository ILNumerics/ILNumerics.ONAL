using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILNumerics.Core.Functions.Builtin; 
using static ILNumerics.ILMath;
using static ILNumerics.Globals;
using System.Net.Security;

namespace ILNumerics.Core.UnitTests.numpy_attributes_Tests {

    [TestClass]
    public class repeatTests {

        [TestMethod]
        public unsafe void numpy_repeat_simple() {

            Array<double> A = counter<double>(1, 1, 5, 4, StorageOrders.RowMajor);
            Array<double> B = A.repeat(2);

            Array<double> Res = A.Reshape(1, A.S.NumberOfElements, StorageOrders.RowMajor);
            Res[1, ":"] = Res.C;
            Res.a = Res.Reshape(1, Res.S.NumberOfElements, StorageOrders.ColumnMajor);

            Assert.IsTrue(Res.Equals(B));

        }
        [TestMethod]
        public unsafe void numpy_repeat_A_empty() {
            Array<fcomplex> A = empty<fcomplex>(2, 3, dim2: 0);
            Array<fcomplex> B = A.repeat(2);
            Assert.IsTrue(all(A.repeat(2).shape == vector<long>(1,0))); 
            Assert.IsTrue(all(A.repeat(2,0).shape == vector<long>(4,3,0)));
            Assert.IsTrue(all(A.repeat(2,1).shape == vector<long>(2,6,0)));
            Assert.IsTrue(all(A.repeat(2,2).shape == vector<long>(2,3,0)));
        }
        [TestMethod]
        public unsafe void numpy_repeat_A_RetTRelease() {

            var A = counter<double>(1.0, 1.0, 4, 3, 2);

            var B = A.repeat(2);

            Assert.IsTrue(all(B.shape == vector<long>(1, 48)));
        }
        [TestMethod]
        public unsafe void numpy_repeat_Repeat_RetTRelease() {

            Array<double> A = counter<double>(1.0, 1.0, 4, 3, 2);
            var rep = vector<long>(2); 

            var B = A.repeat(rep);

            Assert.IsTrue(all(B.shape == vector<long>(1, 48)));

        }
        [TestMethod]
        public unsafe void numpy_repeat_A_scalar() {
            Array<double> A = 4;
            Array<double> B = A.repeat(3);

            Assert.IsTrue(all(B.shape == vector<long>(1, 3)));
            Assert.IsTrue(B.Equals(ones<double>(1, 3) * 4)); 

        }
        [TestMethod]
        public unsafe void numpy_repeat_A_NPscalar() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                Array<double> A = 4;
                Array<double> B = A.repeat(3);

                Assert.IsTrue(all(B.shape == vector<long>(3)));
                Assert.IsTrue(B.Equals(vector<double>(1,1,1) * 4));
            }
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public unsafe void numpy_repeat_A_nullFail() {
            Array<short> A = null;
            numpyInternal.repeat(A, vector<int>(2)); 
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public unsafe void numpy_repeat_Repeats_nullFail() {
            Array<short> A = 4;
            Array<short> rep = null;
            numpyInternal.repeat(A, rep);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public unsafe void numpy_repeat_Repeats_EmptyFail() {
            Array<short> A = 4;
            Array<short> rep = zeros<short>(0);
            numpyInternal.repeat(A, rep);
        }
        [TestMethod]
        public unsafe void numpy_repeat_Repeats_scalar() {
            Array<int> A = vector<int>(0, 1, 2);
            Array<long> Rep = 3;
            Array<int> Res = vector<int>(0, 0, 0, 1, 1, 1, 2, 2, 2);
            Array<int> B = A.repeat(Rep);
            Assert.IsTrue(B.Equals(Res)); 
        }
        [TestMethod]
        public unsafe void numpy_repeat_Repeats_NPscalar() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                Array<int> A = vector<int>(0, 1, 2);
                Array<long> Rep = 3;
                Assert.IsTrue(Rep.S.NumberOfDimensions == 0); 
                Array<int> Res = vector<int>(0, 0, 0, 1, 1, 1, 2, 2, 2);
                Array<int> B = A.repeat(Rep);
                Assert.IsTrue(B.Equals(Res));
            }
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public unsafe void numpy_repeat_Repeats_nonIntegerFail() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                Array<int> A = vector<int>(0, 1, 2);
                Array<double> Rep = double.NaN;
                Assert.IsTrue(Rep.S.NumberOfDimensions == 0);
                Array<int> Res = vector<int>(0, 0, 0, 1, 1, 1, 2, 2, 2);
                Array<int> B = A.repeat(Rep);
                Assert.IsTrue(B.Equals(Res));
            }
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public unsafe void numpy_repeat_Repeats_negativeFail() {
            Array<int> A = vector<int>(0, 1, 2);
            Array<double> Rep = -2;
            Array<int> Res = vector<int>(0, 0, 0, 1, 1, 1, 2, 2, 2);
            Array<int> B = A.repeat(Rep);
            Assert.IsTrue(B.Equals(Res));
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public unsafe void numpy_repeat_Repeats_nonBroadcastableFail() {
            Array<int> A = counter<int>(0, 1, 2,3);
            Array<float> Rep = vector<float>(1,1);
            Array<int> Res = vector<int>(0, 0, 0, 1, 1, 1, 2, 2, 2);
            Array<int> B = A.repeat(Rep, 1);
  
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public unsafe void numpy_repeat_axis_OORFail() {
            counter<long>(1, 1, 5, 4).repeat(2, 3); 
        }
        [TestMethod]
        public unsafe void numpy_repeat_axis_3D_0() {
            Array<uint> A = counter<uint>(0, 1,  2, 3, 4);
            Array<float> Rep = vector<float>(2, 1);
            Array<uint> B = A.repeat(Rep, 0);


            Assert.IsTrue(B.shape.Equals(vector<long>(3,3,4)));
            Assert.IsTrue(B.GetValue(1, 0, 0) == A.GetValue(0, 0, 0));
            Assert.IsTrue(B[r(1, end), ellipsis].Equals(A)); 
            Assert.IsTrue(B["0,2", ellipsis].Equals(A));
        }
        [TestMethod]
        public unsafe void numpy_repeat_axis_3D_1_vector() {
            Array<uint> A = counter<uint>(0, 1, 2, 3, 4);
            Array<float> Rep = vector<float>(2, 1, 3);
            Array<uint> B = A.repeat(Rep, 1);

            Assert.IsTrue(B.shape.Equals(vector<long>(2, (long)(float)sum(Rep), 4)));
            Assert.IsTrue(B.GetValue(1, 5, 0) == B.GetValue(1, 4, 0));
            Assert.IsTrue(B.GetValue(1, 5, 0) == A.GetValue(1, 2, 0));
        }
        [TestMethod]
        public unsafe void numpy_repeat_axis_3D_1_scalar() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                Array<uint> A = counter<uint>(0, 1, 2, 3, 4);
                Array<float> Rep = 3;
                Array<uint> B = A.repeat(Rep, 1);

                Assert.IsTrue(B.shape.Equals(vector<long>(2, 3 * 3, 4)));
                Assert.IsTrue(B.GetValue(1, 5, 0) == B.GetValue(1, 4, 0));
                Assert.IsTrue(B.GetValue(1, 5, 0) == A.GetValue(1, 1, 0));
            }
        }
        [TestMethod]
        public unsafe void numpy_repeat_axis_3D_1_3Dshape() {

            Array<uint> A = counter<uint>(0, 1, 2, 4, 3);
            Array<float> Rep = new float[,] {
                { 1, 2 },
                { 3, 4 }
            };
            Array<uint> B = A.repeat(Rep, 1);

            Assert.IsTrue(B.shape.Equals(vector<long>(2, 10, 3)));
            Assert.IsTrue(B[0, 0, full].Equals(A[0, 0, full]));

            Assert.IsTrue(B[0, 1, full].Equals(A[0, 1, full]));
            Assert.IsTrue(B[0, 2, full].Equals(A[0, 1, full]));

            Assert.IsTrue(B[0, 3, full].Equals(A[0, 2, full]));
            Assert.IsTrue(B[0, 4, full].Equals(A[0, 2, full]));
            Assert.IsTrue(B[0, 5, full].Equals(A[0, 2, full]));

            Assert.IsTrue(B[0, 6, full].Equals(A[0, 3, full]));
            Assert.IsTrue(B[0, 7, full].Equals(A[0, 3, full]));
            Assert.IsTrue(B[0, 8, full].Equals(A[0, 3, full]));
            Assert.IsTrue(B[0, 9, full].Equals(A[0, 3, full]));

            Assert.IsTrue(B[1, 0, full].Equals(A[1, 0, full]));

            Assert.IsTrue(B[1, 1, full].Equals(A[1, 1, full]));
            Assert.IsTrue(B[1, 2, full].Equals(A[1, 1, full]));

            Assert.IsTrue(B[1, 3, full].Equals(A[1, 2, full]));
            Assert.IsTrue(B[1, 4, full].Equals(A[1, 2, full]));
            Assert.IsTrue(B[1, 5, full].Equals(A[1, 2, full]));

            Assert.IsTrue(B[1, 6, full].Equals(A[1, 3, full]));
            Assert.IsTrue(B[1, 7, full].Equals(A[1, 3, full]));
            Assert.IsTrue(B[1, 8, full].Equals(A[1, 3, full]));
            Assert.IsTrue(B[1, 9, full].Equals(A[1, 3, full]));

        }
        [TestMethod]
        public unsafe void numpy_repeat_axis_3D_2() {
            Array<byte> A = counter<byte>(0, 1, 2, 4, 3);
            Array<short> Rep = new short[,] {
                { 1 },
                { 3 },
                { 2 }
            };
            Array<byte> B = A.repeat(Rep, 2);

            Assert.IsTrue(B.shape.Equals(vector<long>(2, 4, 6)));
            Assert.IsTrue(B[0, 1, 0].Equals(A[0, 1, 0]));

            Assert.IsTrue(B[0, 1, 1].Equals(A[0, 1, 1]));
            Assert.IsTrue(B[0, 1, 2].Equals(A[0, 1, 1]));
            Assert.IsTrue(B[0, 1, 3].Equals(A[0, 1, 1]));

            Assert.IsTrue(B[0, 1, 4].Equals(A[0, 1, 2]));
            Assert.IsTrue(B[0, 1, 5].Equals(A[0, 1, 2]));

        }

    }
}
