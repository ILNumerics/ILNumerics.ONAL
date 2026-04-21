// MIT License
// 
// Copyright (c) 2026 ILNumerics GmbH
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;
using static ILNumerics.Globals;

namespace ILNumerics.Core.UnitTests.numpy_attributes_Tests {

    [TestClass]
    public class sortTests {

        [TestMethod]
        public unsafe void numpy_sort_simple() {

            Array<double> A = counter<double>(1, 1, 5, 4, StorageOrders.RowMajor);
            A.sort(2);
            Assert.IsTrue(A.Equals(counter<double>(1, 1, 5, 4, StorageOrders.RowMajor)));

            A.sort(0, true);
            Assert.IsTrue(A.Equals(counter<double>(1, 1, 5, 4, StorageOrders.RowMajor)["4,3,2,1,0", full]));

            A = counter<double>(1, 1, 5, 4, StorageOrders.RowMajor);
            A.sort(-1, true); // -> 1
            Assert.IsTrue(A.Equals(counter<double>(1, 1, 5, 4, StorageOrders.RowMajor)[full, "3,2,1,0"]));

            A = counter<double>(-1, -1, 5, 4, StorageOrders.ColumnMajor);
            A.sort(); // -> 1
            Assert.IsTrue(A.Equals(counter<double>(-1, -1, 5, 4, StorageOrders.ColumnMajor)[full, "3,2,1,0"]));

        }
        [TestMethod]
        public unsafe void numpy_sort_A_empty() {
            Array<fcomplex> A = empty<fcomplex>(2, 3, dim2: 0);
            A.sort();

            Assert.IsTrue(A.Equals(empty<fcomplex>(2, 3, dim2: 0)));
            A.sort(-2);
            Assert.IsTrue(A.Equals(empty<fcomplex>(2, 3, dim2: 0)));

        }
        [TestMethod]
        public unsafe void numpy_sort_A_scalar() {
            Array<double> A = 4;
            A.sort(1);

            Assert.IsTrue(A == 4);

        }
        [TestMethod]
        public unsafe void numpy_sort_A_NPscalar() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                Array<double> A = 4;
                A.sort(0);

                Assert.IsTrue(A == 4);
            }
        }

        [TestMethod]
        public void numpy_sort_detaches() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<short> A = new short[] { 1, 4, 2, 5 };
                Array<short> B = A.C;
                A.sort();
                Assert.IsTrue(A.Equals(vector<short>(1, 2, 4, 5)));
                Assert.IsTrue(B.Equals(vector<short>(1, 4, 2, 5)));
            }
        }

        [TestMethod]
        public void numpy_sort_Shared_NoFail() {

            using (Scope.Enter(ArrayStyles.numpy)) { // to get non-singleton last dimension

                Array<double> A = new[] { 5, 6, 3 };
                var R = A.C;

                Assert.IsFalse(object.ReferenceEquals(R.Storage, A.Storage));
                Assert.IsTrue(object.ReferenceEquals(R.Storage.m_handles, A.Storage.m_handles)); 

                A.sort(); //must not throw (but detach)

                Assert.IsTrue(A.Equals(vector<double>(3, 5, 6)));

                Assert.IsTrue(R.Equals(vector<double>(5, 6, 3)));
            }
        }



    }
}
