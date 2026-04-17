using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;
using static ILNumerics.Globals; 

namespace ILNumerics.Core.UnitTests.MathInternalTests {

    [TestClass]
    public class MathInternal_findTests {

        [TestMethod]
        public void mathInternal_find_simpleTest() {

            Array<double> A = zeros<double>(5, 4);
            Array<long> B = find(A);
            Assert.IsTrue(B.Equals(empty<long>(0)));

            A[0] = 2;
            A[end] = 4;
            A[15] = 3;
            B = find(A);
            Assert.IsTrue(B.Equals(vector<long>(0,15,19)));

            A = ones<double>(5, 4, 3);
            B = find(A);
            Assert.IsTrue(B.Equals(counter<long>(0,1, 5*4*3)));

            using (Settings.Ensure(()=> Settings.ArrayStyle, ArrayStyles.numpy)) {
                A = 1;
                Assert.IsTrue(find(A).Equals(vector<long>(0))); 
                Assert.IsTrue(find(A).S.NumberOfElements == 1);
                Assert.IsTrue(find(A).S.NumberOfDimensions == 1); // per def. 
            }
        }

        [TestMethod]
        public void MathInternal_find_ext1() {

            Array<float> A = zeros<float>(10, 20, 3, 2);
            Array<long> C = 1;
            Array<float> V = 1;
            Array<long> IR = counter<long>(0, 1, A.S.NumberOfElements / A.S[0]) % A.S[0];
            Array<long> IC = counter<long>(0, 1, A.S.NumberOfElements / A.S[0]);
            Array<long> I = IR + IC * A.S[0];


            A[I] = 1;
            Assert.IsTrue(find(A).Equals(I));

            Array<long> F = find(A, 0, false, C, V);

            Assert.IsTrue(F.Equals(IR));
            Assert.IsTrue(C.Equals(IC));
            Assert.IsTrue(V.Equals(ones<float>(IR.S.NumberOfElements, 1)));

        }

        [TestMethod]
        public void MathInternal_find_ext1_empty() {

            Array<float> A = zeros<float>(10, dim1: 0);
            Array<long> C = 1;
            Array<float> V = 1;
            Array<long> IR = counter<long>(0, 1, A.S.NumberOfElements / A.S[0]) % A.S[0];
            Array<long> IC = counter<long>(0, 1, A.S.NumberOfElements / A.S[0]);
            Array<long> I = IR + IC * A.S[0];

            A[I] = 1;
            Assert.IsTrue(find(A).Equals(I));

            Array<long> F = find(A, 0, false, C, V);

            Assert.IsTrue(F.Equals(IR));
            Assert.IsTrue(C.Equals(IC));
            Assert.IsTrue(V.Equals(ones<float>(IR.S.NumberOfElements, 1)));

        }
        [TestMethod]
        public void MathInternal_find_ext1_NPscalar() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<float> A = 0;
                Array<long> C = 1;
                Array<float> V = 1;
                Array<long> IR = counter<long>(0, 1, A.S.NumberOfElements / A.S[0]) % A.S[0];
                Array<long> IC = counter<long>(0, 1, A.S.NumberOfElements / A.S[0]);
                Array<long> I = IR + IC * A.S[0];

                A[I] = 1;
                Assert.IsTrue(find(A).Equals(I));

                Array<long> F = find(A, 0, false, C, V);

                Assert.IsTrue(F.Equals(IR));
                Assert.IsTrue(C.Equals(IC));
                Assert.IsTrue(V.Equals(ones<float>(IR.S.NumberOfElements, 1)));
            }
        }

        [TestMethod]
        public void MathInternal_find_bool_ext1() {

            Array<float> A = zeros<float>(10, 20, 3, 2);
            Array<long> C = 1;
            Array<long> IR = counter<long>(0, 1, A.S.NumberOfElements / A.S[0]) % A.S[0];
            Array<long> IC = counter<long>(0, 1, A.S.NumberOfElements / A.S[0]);
            Array<long> I = IR + IC * A.S[0];


            A[I] = 1;
            Assert.IsTrue(find(A == 1).Equals(I));

            Array<long> F = find(A == 1, 0, false, C);

            Assert.IsTrue(F.Equals(IR));
            Assert.IsTrue(C.Equals(IC));

            Assert.IsTrue(find32(A == 1).Equals(toint32(I)));
            Array<int> c = 1; 
            Array<int> f = find32(A == 1, 0, false, c);

            Assert.IsTrue(F.Equals(IR));
            Assert.IsTrue(C.Equals(IC));

        }
        [TestMethod]
        public void MathInternal_find_onlyC() {

            Array<float> A = zeros<float>(10, 20, 3, 2);
            Array<long> C = 1;
            Array<long> IR = counter<long>(0, 1, A.S.NumberOfElements / A.S[0]) % A.S[0];
            Array<long> IC = counter<long>(0, 1, A.S.NumberOfElements / A.S[0]);
            Array<long> I = IR + IC * A.S[0];


            A[I] = 1;
            Assert.IsTrue(find(A).Equals(I));

            Array<long> F = find(A, 0, false, C);

            Assert.IsTrue(F.Equals(IR));
            Assert.IsTrue(C.Equals(IC));
            
        }
        [TestMethod]
        public void MathInternal_find_onlyV() {

            Array<float> A = zeros<float>(10, 20, 3, 2);
            Array<float> V = 1;
            Array<long> IR = counter<long>(0, 1, A.S.NumberOfElements / A.S[0]) % A.S[0];
            Array<long> IC = counter<long>(0, 1, A.S.NumberOfElements / A.S[0]);
            Array<long> I = IR + IC * A.S[0];


            A[I] = 1;
            Assert.IsTrue(find(A).Equals(I));

            Array<long> F = find(A, 0, false, V: V);

            Assert.IsTrue(F.Equals(I));
            Assert.IsTrue(V.Equals(ones<float>(IR.S.NumberOfElements, 1)));

        }
        [TestMethod]
        public void MathInternal_find_onlyV_backwards() {

            Array<float> A = zeros<float>(10, 20, 3, 2);
            Array<float> V = 1;
            Array<long> IR = counter<long>(0, 1, A.S.NumberOfElements / A.S[0]) % A.S[0];
            Array<long> IC = counter<long>(0, 1, A.S.NumberOfElements / A.S[0]);
            Array<long> I = IR + IC * A.S[0];


            A[I] = tosingle(I+1);
            Assert.IsTrue(find(A).Equals(I));

            Array<long> F = find(A, 15, true, V: V);

            Assert.IsTrue(F.Equals(I[counter<int>(-1,-1,15)]));
            Assert.IsTrue(V.Equals(tosingle(I+1)[counter<int>(-1,-1,15)]));

        }
        [TestMethod]
        public void MathInternal_find_nEqualTrues_bool() {

            Array<float> A = zeros<float>(10, 20, 3, 2);
            Array<long> IR = counter<long>(0, 1, 11) % A.S[0];
            Array<long> IC = counter<long>(0, 1, 11);
            Array<long> I = IR + IC * A.S[0];


            A[I] = 1;
            Assert.IsTrue(find(A == 1).Equals(I));
            Array<long> C = -1;
            Array<long> F = find(A == 1, 11, false, C);
            Assert.IsTrue(F.Length == 11);
            Assert.IsTrue(C.Length == 11);

            Assert.IsTrue(F.Equals(IR));
            Assert.IsTrue(C.Equals(IC));

            Assert.IsTrue(find32(A == 1).Equals(toint32(I)));
            Array<int> f = find32(A == 1, 11, false);

            Assert.IsTrue(F.Equals(IR));
            Assert.IsTrue(C.Equals(IC));

        }
        [TestMethod]
        public void MathInternal_find_nSmallerTrues_bool() {

            Array<float> A = zeros<float>(10, 20, 3, 2);
            Array<long> IR = counter<long>(0, 1, A.S.NumberOfElements / A.S[0]) % A.S[0];
            Array<long> IC = counter<long>(0, 1, A.S.NumberOfElements / A.S[0]);
            Array<long> I = IR + IC * A.S[0];


            A[I] = 1;
            Assert.IsTrue(find(A == 1).Equals(I));
            Array<long> C = -1;
            Array<long> F = find(A == 1, 11, false, C);
            Assert.IsTrue(F.Length == 11);
            Assert.IsTrue(C.Length == 11);

            Assert.IsTrue(F.Equals(IR[r(0, 10)]));
            Assert.IsTrue(C.Equals(IC[r(0, 10)]));

            Assert.IsTrue(find32(A == 1).Equals(toint32(I)));
            Array<int> f = find32(A == 1, 11, false);

            Assert.IsTrue(F.Equals(IR[r(0, 10)]));
            Assert.IsTrue(C.Equals(IC[r(0, 10)]));

        }

        [TestMethod]
        public void MathInternal_find_nonCM_strides() {
            Array<float> A = counter<float>(0f, 1f, 4, 3, StorageOrders.RowMajor);
            A[A % 2 == 0] = 0;

            Assert.IsTrue(allall(find32(A) == vector<int>(1, 3, 4, 6, 9, 11)));
            Assert.IsTrue(allall(find(A) == vector<long>(1, 3, 4, 6, 9, 11)));
            Assert.IsTrue(allall(find32(A, 0, false) == vector<int>(1, 3, 4, 6, 9, 11)));
            Assert.IsTrue(allall(find(A, 0, false) == vector<long>(1, 3, 4, 6, 9, 11)));

            Assert.IsTrue(allall(find32(Helper.offset<float>(A)) == vector<int>(1, 3, 4, 6, 9, 11)));
            Assert.IsTrue(allall(find(Helper.offset<float>(A)) == vector<long>(1, 3, 4, 6, 9, 11)));
            Assert.IsTrue(allall(find32(Helper.offset<float>(A), 5, false) == vector<int>(1, 3, 4, 6, 9)));
            Assert.IsTrue(allall(find(Helper.offset<float>(A), 5, true) == vector<long>(11, 9, 6, 4, 3)));

            Assert.IsTrue(allall(find32(Helper.offsStride<float>(A)) == vector<int>(1, 3, 4, 6, 9, 11)));
            Assert.IsTrue(allall(find(Helper.offsStride<float>(A)) == vector<long>(1, 3, 4, 6, 9, 11)));
            Assert.IsTrue(allall(find32(Helper.offsStride<float>(A), 5, false) == vector<int>(1, 3, 4, 6, 9)));
            Assert.IsTrue(allall(find(Helper.offsStride<float>(A), 5, true) == vector<long>(11, 9, 6, 4, 3)));

        }
        [TestMethod]
        public void MathInternal_find_nonCM_stridesBool() {
            Array<float> A = counter<float>(0f, 1f, 4, 3, StorageOrders.RowMajor);
            Logical AB = A%2 != 0;

            Assert.IsTrue(allall(find32(AB) == vector<int>(1, 3, 4, 6, 9, 11)));
            Assert.IsTrue(allall(find(AB) == vector<long>(1, 3, 4, 6, 9, 11)));
            Assert.IsTrue(allall(find32(AB, 0, false) == vector<int>(1, 3, 4, 6, 9, 11)));
            Assert.IsTrue(allall(find(AB, 0, false) == vector<long>(1, 3, 4, 6, 9, 11)));

            Assert.IsTrue(allall(find32(Helper.offset(AB)) == vector<int>(1, 3, 4, 6, 9, 11)));
            Assert.IsTrue(allall(find(Helper.offset(AB)) == vector<long>(1, 3, 4, 6, 9, 11)));
            Assert.IsTrue(allall(find32(Helper.offset(AB), 5, false) == vector<int>(1, 3, 4, 6, 9)));
            Assert.IsTrue(allall(find(Helper.offset(AB), 5, true) == vector<long>(11, 9, 6, 4, 3)));

            Assert.IsTrue(allall(find32(Helper.offsStride(AB)) == vector<int>(1, 3, 4, 6, 9, 11)));
            Assert.IsTrue(allall(find(Helper.offsStride(AB)) == vector<long>(1, 3, 4, 6, 9, 11)));
            Assert.IsTrue(allall(find32(Helper.offsStride(AB), 5, false) == vector<int>(1, 3, 4, 6, 9)));
            Assert.IsTrue(allall(find(Helper.offsStride(AB), 5, true) == vector<long>(11, 9, 6, 4, 3)));

        }

    }
}
