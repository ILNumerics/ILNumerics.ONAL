using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;
using ILNumerics.Core.Functions.Builtin;

namespace ILNumerics.Core.UnitTests.numpy_attributes_Tests {

    [TestClass]
    public class argsortTests {

        [TestMethod]
        public unsafe void numpy_argsort_simple() {

            Array<int> A = counter<int>(-1, -1, 5, 4, StorageOrders.RowMajor);
            Array<long> I = A.argsort();
            
            Array<long> Res = counter<long>(3, -1, 1, 4) + zeros<long>(5,1); 
            Assert.IsTrue(I.Equals(Res));
            Assert.IsTrue(A.Equals(counter<int>(-1, -1, 5, 4, StorageOrders.RowMajor)));

            Array<int> S = 1;
            I = A.argsort(sorted: S);
            Assert.IsTrue(S.Equals(MathInternal.sort(A, dim: 1)));
            Assert.IsTrue(I.Equals(Res));
            A.Equals(counter<int>(-1, -1, 5, 4, StorageOrders.RowMajor));

        }
        [TestMethod]
        public unsafe void numpy_argsort_A_empty() {
            Array<fcomplex> A = empty<fcomplex>(2, 3, dim2: 0);
            Array<long> I = A.argsort();

            Assert.IsTrue(I.Equals(empty<long>(2, 3, dim2: 0)));
            I.a = A.argsort(-2);
            Assert.IsTrue(I.Equals(empty<long>(2, 3, dim2: 0)));
            I.a = A.argsort(-2,true,A);
            Assert.IsTrue(I.Equals(empty<long>(2, 3, dim2: 0)));
            Assert.IsTrue(A.Equals(empty<fcomplex>(2, 3, dim2: 0))); 
        }
        [TestMethod]
        public unsafe void numpy_argsort_A_scalar() {
            Array<double> A = 4;

            Assert.IsTrue(A.argsort(0) == 0);
            Assert.IsTrue(A.argsort(1) == 0);
            Assert.IsTrue(A.argsort(2) == 0);

        }
        [TestMethod]
        public unsafe void numpy_argsort_A_NPscalar() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                Array<double> A = 4;

                Assert.IsTrue(A.argsort(0) == 0);
                Assert.IsTrue(A.argsort(1) == 0);
                Assert.IsTrue(A.argsort(2) == 0);

            }
        }

        [TestMethod]
        public void numpy_argsort_detaches() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<short> A = new short[] { 1, 4, 2, 5 };
                Array<short> B = A.C;
                Array<short> C = 0; 
                Array<long> I = A.argsort(sorted: C);
                Assert.IsTrue(A.Equals(vector<short>(1, 4, 2, 5)));
                Assert.IsTrue(B.Equals(vector<short>(1, 4, 2, 5)));
                Assert.IsTrue(C.Equals(vector<short>(1, 2, 4, 5)));
                Assert.IsTrue(I.Equals(vector<long>(0, 2, 1, 3))); 
            }
        }

    }
}
