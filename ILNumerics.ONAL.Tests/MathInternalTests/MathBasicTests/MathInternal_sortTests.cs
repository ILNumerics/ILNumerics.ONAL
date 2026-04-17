using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;
using static ILNumerics.Globals; 

namespace ILNumerics.Core.UnitTests.MathInternalTests {

    /// <summary>
    /// Summary description for sortTests
    /// </summary>
    [TestClass]
    public class MathInternal_sortTests {

        [TestMethod]
        public void MathInternal_sort_simpleTest() {

            Array<double> A = -counter(1.0, 1.0, 4, 3);
            Array<double> B = sort(A);
            Array<double> Res = new double[,] {
                { -4, -8, -12 },
                { -3, -7, -11 },
                { -2, -6, -10 },
                { -1, -5, -9 }
            };
            Assert.IsTrue(B.Equals(Res));

            A = -counter(1.0, 1.0, 3, 4, StorageOrders.RowMajor);
            B = sort(A, 1);
            Assert.IsTrue(B.Equals(Res.T));

        }
        [TestMethod]
        public void MathInternal_sort_descendingTest() {

            Array<double> A = -counter(1.0, 1.0, 4, 3);
            Array<double> B = sort(A, descending: true);

            Assert.IsTrue(B.Equals(A));

            A = -counter(1.0, 1.0, 3, 2, 4, StorageOrders.RowMajor);
            B = sort(A, 1, true);
            Assert.IsTrue(B.Equals(A));

            A = counter(1.0, 1.0, 3, 2, 4, StorageOrders.RowMajor);
            B = sort(A, 2, true);
            Assert.IsTrue(B.Equals(A[full, full, "3,2,1,0"]));

            A = counter(1.0, 1.0, 3, 2, 4, StorageOrders.ColumnMajor);
            B = sort(A, 2, true);
            Assert.IsTrue(B.Equals(A[full, full, "3,2,1,0"]));

        }
        [TestMethod]
        public void MathInternal_sort_virtualDims() {

            Array<ushort> A = touint16(randn(5, 4, 5) * 100);

            Assert.IsTrue(A.Equals(sort(A, 3, true))); 
            Assert.IsTrue(A.Equals(sort(A, 3, false)));

        }
        [TestMethod]
        public void MathInternal_sort_baseOffset() {
            Array<long> A = counter<long>(1, -1, 5, 4, 3);
            A.a = A[r(1, end), r(1,end), 0];  // ML mode
            Assert.IsTrue(A.S.BaseOffset == 6); 
            Array<long> B = sort(A);
            Array<long> Res = counter<long>(-5, -1, 5, 3)["3,2,1,0", full];

            Assert.IsTrue(B.Equals(Res)); 
        }
        [TestMethod]
        public void MathInternal_sort_WorkingDimLastNP() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<long> A = counter<long>(1, -1, 5, 4, 3);
                A.a = A[r(1, end), r(1, end), 0];  // ML mode
                Assert.IsTrue(A.S.BaseOffset == 6);
                Array<long> B = sort(A); // S.WorkingDim resolves to 1, searching from the end
                Array<long> Res = counter<long>(-5, -1, 5, 3)[slice(0,4), vector<int>(2, 1, 0)];

                Assert.IsTrue(B.Equals(Res));
            }

        }
        [TestMethod]
        public void MathInternal_sort_NonOneStrideWorkingDim() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<float> A = counter<float>(1, -0.5f, 10, 4, 3);
                A.a = A[r(2, 2, end), r(1, end), 0];  // ML mode
                Assert.IsTrue(A.S.BaseOffset == 12);
                Assert.IsTrue(A.S.GetStride(A.S.WorkingDimension()) == 2);
                Array<float> B = sort(A); // S.WorkingDim resolves to 1, searching from the end
                //Array<float> Res = counter<float>(-5, -1, 5, 3)[slice(0, 4), vector<int>(2, 1, 0)];
                Array<float> Res = counter<float>(-5, -1, 5, 3)["3,2,1,0", full];

                Assert.IsTrue(B.Equals(Res));
            }

        }
        [TestMethod]
        public void MathInternal_sort_NonOneStrideWorkingDim1() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<float> A = counter<float>(1, -0.5f, 10, 8, 3);
                A.a = A[r(2, 2, end), r(2, 2, end), 0];  
                Assert.IsTrue(A.S.BaseOffset == 22);
                Assert.IsTrue(A.S.GetStride(A.S.WorkingDimension()) == 20);

                Array<float> B = sort(A); // S.WorkingDim resolves to 1, searching from the end
                //Array<float> Res = counter<float>(-5, -1, 5, 3)[slice(0, 4), vector<int>(2, 1, 0)];
                Array<float> Res = counter<float>(-10, -1, 10, 3)["0:3", vector<int>(2, 1, 0)];

                Assert.IsTrue(B.Equals(Res));
            }

        }
        [TestMethod]
        public void MathInternal_sort_MTSplit4() {

            using (Settings.Ensure(() => Settings.MaxNumberThreads, 4u)) {
                Array<int> A = counter<int>(1, 1, 500, 400, 30);
                Array<int> B = sort(A, 1, true);

                Array<int> Res = A.C;
                Res.a = Res[full, counter<int>(399, -1, 400), full];

                Assert.IsTrue(B.Equals(Res)); 
            }
        }
        [TestMethod]
        public void MathInternal_sort_MTSplit3() {

            using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {
                Array<int> A = counter<int>(1, 1, 500, 400, 30);
                Array<int> B = sort(A, 1, true);

                Array<int> Res = A.C;
                Res.a = Res[full, counter<int>(399, -1, 400), full];

                Assert.IsTrue(B.Equals(Res));
            }

        }

        [TestMethod]
        public void MathInternal_sort_emptyTest() {

            Array<ushort> A = array<ushort>(0, vector<long>(4, 0, 5));

            Assert.IsTrue(A.IsEmpty);
            Assert.IsTrue(sort(A).S[0] == 4);
            Assert.IsTrue(sort(A).S[1] == 0);
            Assert.IsTrue(sort(A).S[2] == 5);

            Assert.IsTrue(sort(A, dim: 0).S[0] == 4);
            Assert.IsTrue(sort(A, dim: 0).S[1] == 0);
            Assert.IsTrue(sort(A, dim: 0).S[2] == 5);

            Assert.IsTrue(sort(A, dim: 1).S[0] == 4);
            Assert.IsTrue(sort(A, dim: 1).S[1] == 0);
            Assert.IsTrue(sort(A, dim: 1).S[2] == 5);

            Assert.IsTrue(sort(A, dim: 2).S[0] == 4);
            Assert.IsTrue(sort(A, dim: 2).S[1] == 0);
            Assert.IsTrue(sort(A, dim: 2).S[2] == 5);

            Assert.IsTrue(sort(A, dim: 3).S[0] == 4);
            Assert.IsTrue(sort(A, dim: 3).S[1] == 0);
            Assert.IsTrue(sort(A, dim: 3).S[2] == 5);

        }

        [TestMethod]
        public void MathInternal_sort_scalarTest() {

            Array<uint> A = 91;

            Assert.IsTrue(sort(A).IsScalar);
            Assert.IsTrue((uint)sort(A) == 91);
            Assert.IsTrue(sort(A).S.NumberOfDimensions == 2);
            Assert.IsTrue(sort(A).S[0] == 1); 
            Assert.IsTrue(sort(A).S[1] == 1);

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                A = 92;
                Assert.IsTrue(sort(A).IsScalar);
                Assert.IsTrue((uint)sort(A) == 92);
                Assert.IsTrue(sort(A).S.NumberOfDimensions == 0);
                Assert.IsTrue(sort(A).S[0] == 1);
                Assert.IsTrue(sort(A).S[1] == 1);

            }
        }

        [TestMethod]
        public void MathInternal_sortIDX_simple() {

            Array<double> A = counter<double>(1, 1, 5, 4);
            Array<long> I = 1;

            Array<double> B = sort(A, I, descending: true);

            Assert.IsTrue(B.Equals(A[vector<int>(4, 3, 2, 1, 0), full])); 
            Assert.IsTrue(I.Equals(vector<long>(4, 3, 2, 1, 0) + zeros<long>(1,4)));

        }
        [TestMethod]
        public void MathInternal_sortIDX_empty() {
            Array<long> A = empty<long>(0,1,2,3);
            Array<long> I = -2;
            Array<long> B = sort(A, I); 
            Assert.IsTrue(B.IsEmpty); 
            Assert.IsTrue(B.S[0] == A.S[0]);
            Assert.IsTrue(B.S[1] == A.S[1]);
            Assert.IsTrue(B.S[2] == A.S[2]);
            Assert.IsTrue(B.S[3] == A.S[3]);

            Assert.IsTrue(I.IsEmpty);
            Assert.IsTrue(I.S[0] == A.S[0]);
            Assert.IsTrue(I.S[1] == A.S[1]);
            Assert.IsTrue(I.S[2] == A.S[2]);
            Assert.IsTrue(I.S[3] == A.S[3]);
        }

        [TestMethod]
        public void MathInternal_sortIDX_scalar() {
            Array<short> A = 2;
            Assert.IsTrue(A.IsScalar);
            Assert.IsTrue(A.ndim == 2);
            Assert.IsTrue(A.shape[0] == 1);
            Assert.IsTrue(A.shape[1] == 1);

            Assert.IsTrue(sort(A).IsScalar);
            Assert.IsTrue(sort(A).ndim == 2);
            Assert.IsTrue(sort(A).shape[0] == 1);
            Assert.IsTrue(sort(A).shape[1] == 1);

            Array<long> I = 3; 
            Assert.IsTrue(sort(A, I) == 2); 
            Assert.IsTrue(I == 0); // changed! 
            Assert.IsTrue(I.ndim == 2);
            Assert.IsTrue(I.shape[0] == 1);
            Assert.IsTrue(I.shape[1] == 1);

            I = empty<long>(0);
            Assert.IsTrue(sort(A, I) == 2);
            Assert.IsTrue(I == 0); // new 
            Assert.IsTrue(I.ndim == 2);
            Assert.IsTrue(I.shape[0] == 1);
            Assert.IsTrue(I.shape[1] == 1);

        }
        [TestMethod]
        public void MathInternal_sortIDX_NPscalar() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<uint> A = 92;
                Array<long> I = -1; 

                Assert.IsTrue(sort(A, I).IsScalar);
                Assert.IsTrue((uint)sort(A,I) == 92);
                Assert.IsTrue(sort(A, I).S.NumberOfDimensions == 0);
                Assert.IsTrue(sort(A, I).S[0] == 1);
                Assert.IsTrue(sort(A, I).S[1] == 1);

                Assert.IsTrue(I.IsScalar); 
                Assert.IsTrue(I.S.IsSameShape(A.S));
                Assert.IsTrue(I == 0);

                I = new long[] { };
                Assert.IsTrue((uint)sort(A, I) == 92, $"(uint)sort(A, I): {(uint)sort(A, I)}");
                Assert.IsTrue(I == 0, $"I: {I}");


            }
        }
    }
}
