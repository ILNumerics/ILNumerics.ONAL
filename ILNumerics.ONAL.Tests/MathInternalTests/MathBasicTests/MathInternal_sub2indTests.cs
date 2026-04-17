using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;

namespace ILNumerics.Core.UnitTests.MathInternalTests {

    [TestClass]
    public class MathInternal_sub2indTests {

        [TestMethod]
        public void mathInternal_sub2ind_simpleTest() {

            Array<double> A1 = counter<double>(0.0, 1.0, 5, 4, 3, StorageOrders.ColumnMajor);
            Array<long> B = ind2sub(A1, counter<long>(0, 1, 5 * 4 * 3));
            Array<long> C = sub2ind(A1, B); 

            Assert.IsTrue(allall(C == counter<long>(0, 1, 5 * 4 * 3))); 
        }

        [TestMethod]
        public void sub2ind_docuExample() {

            Array<float> A = counter<float>(0f, 1f, 4, 3, StorageOrders.RowMajor);
            Array<float> B = A.C; 
            //A
            //<Single> [4,3] 0...11 order:-
            //    [0]:           0          1          2
            //    [1]:           3          4          5
            //    [2]:           6          7          8
            //    [3]:           9         10         11

            // get indices of the odd elements 
            Array<long> I = find(A % 2 != 0); // I are sequential indices in column major order! 
            //I
            //<UInt32> [6,1] 1...11 order:|
            //    [0]:           1
            //    [1]:           3
            //    [2]:           4
            //    [3]:           6
            //    [4]:           9
            //    [5]:          11

            // subscript tuples of odd elements 
            Array<long> S = ind2sub(A, I);
            //S
            //<UInt32> [6,2] 1...2 order:|
            //    [0]:           1          0
            //    [1]:           3          0
            //    [2]:           0          1
            //    [3]:           2          1
            //    [4]:           1          2
            //    [5]:           3          2

            // get the offsets into memory of odd elements
            Array<long> M = sum(A.strides.T * S, 1); 
            //M
            //<UInt32> [6,1] 3...11 order:|
            //    [0]:           3
            //    [1]:           9
            //    [2]:           1
            //    [3]:           7
            //    [4]:           5
            //    [5]:          11

            // set odd elements to: -1
            unsafe {
                float* p = (float*)A.GetHostPointerForWrite();
                foreach (var m in M) {
                    p[m] = -1; 
                }
            }
            //A
            //<Single> [4,3] 0...-1 order:|
            //    [0]:           0         -1          2
            //    [1]:          -1          4         -1
            //    [2]:           6         -1          8
            //    [3]:          -1         10         -1

            // now, the same is faster produced, by: 
            //A[A % 2 != 0] = -1;

            B[B % 2 != 0] = -1;

            Assert.IsTrue(A.Equals(B)); 

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void mathInternal_sub2ind_0D_fail() {
            using (Scope.Enter(arrayStyle: ArrayStyles.numpy)) {
                Array<double> A = 1;
                sub2ind(A, vector<long>(0, 0)); 
            }
        }

    }
}
