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
    public class putTests {
        [TestMethod]
        public unsafe void numpy_put_simple() {

            Array<double> A = counter<double>(1, 1, 5, 4, StorageOrders.RowMajor);
            Array<double> Res = A.C;
            Array<short> I = vector<short>(1, 3, 0, 19);
            Array<double> V = vector<double>(-1, -2, -3);  // only 3, sic.
            A.put(I, V);

            Assert.IsTrue(Res.Equals(counter<double>(1, 1, 5, 4, StorageOrders.RowMajor)));

            Res[0, 1] = V[0]; 
            Res[0, 3] = V[1]; 
            Res[0, 0] = V[2]; 
            Res[4, 3] = V[0];
            Assert.IsTrue(A.Equals(Res));
            
        }

        [TestMethod]
        public void numpy_put_emptyIndices() {
            Array<int> A = zeros<int>(4, 1, 2, order : StorageOrders.ColumnMajor);
            Array<short> I = empty<short>(0, 1, 0, 1); // no indices
            Array<int> V = vector<int>(-1, -2, -3);

            A.put(I, V); // does nothing
            Assert.IsTrue(all(A.shape == vector<long>(4,1,2)));
            Assert.IsTrue(A.Equals(zeros<int>(4, 1, 2)));
            // A storage layout changed only when required
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor); 

            A = zeros<int>(4, 0, 2); // empty A working? 
            A.put(I, V); // does nothing
            Assert.IsTrue(all(A.shape == vector<long>(4, 0, 2)));
            Assert.IsTrue(A.Equals(zeros<int>(4, 0, 2)));

        }
        [TestMethod]
        public void numpy_put_emptyValues() {

            Array<int> A = ones<int>(4, 1, 2, order: StorageOrders.ColumnMajor);
            Array<short> I = empty<short>(0, 1, 0, 1); // no indices.. 
            Array<int> V = empty<int>(1, 0, 4);  // than empty values is fine

            A.put(I, V); // throws 
            Assert.IsTrue(A.Equals(ones<int>(4, 1, 2, order: StorageOrders.ColumnMajor))); 
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),"empty values should not be allowed on non-empty array/indices")]
        public void numpy_put_OOR1Fail() {

            Array<byte> A = zeros<byte>(4, 1, 2, order: StorageOrders.ColumnMajor);
            Array<short> I = vector<short>(1, 0, 8); 
            Array<byte> V = empty<byte>(1, 0, 4); // ArgumentException: "Values argument must not be empty."
            //Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);

            A.put(I, V); // throws 
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void numpy_put_axisOOR2Fail() {
            Array<byte> A = zeros<byte>(4, 1, 2, order: StorageOrders.ColumnMajor);
            Array<short> I = vector<short>(1, 0, -9); // OOR: 8 
            Array<byte> V = empty<byte>(1);
            //Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);

            A.put(I, V); // throws 
        }
        [TestMethod]
        public void numpy_put_A_NPscalar() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<sbyte> A = 2;
                Array<short> I = vector<short>(0, -1);
                Array<sbyte> V = vector<sbyte>(1);
                //Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);

                A.put(I, V);
                Assert.IsTrue(A == 1); 
            }
        }
        [TestMethod]
        public void numpy_put_AShared() {
            Array<double> A = vector<double>(-9,-10,-11);
            var AC = A.C;
            Array<short> I = vector<short>(0, -1);
            Array<double> V = vector<double>(1);

            A.put(I, V); // A was shared, hence, was detached automatically
            Assert.IsTrue(A.Equals(vector<double>(1, -10, 1)), "Invalid result for 'put':" + A.ToString());
            Array<double> R = AC;
            Assert.IsTrue(R.Equals(vector<double>(-9, -10, -11)), "Invalid shared variable after 'put':" + R.ToString()); 
        }
        [TestMethod]
        public void numpy_put_Indices_NPScalar() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<sbyte> A = counter<sbyte>(1,1,5,4);
                Array<short> I = -2;
                Array<sbyte> V = vector<sbyte>(-1,-2,-3);
                //Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);

                A.put(I, V);
                Array<sbyte> R = counter<sbyte>(1, 1, 5, 4);
                R[end,end-1] = -1;  
                Assert.IsTrue(R.Equals(A));
            }
        }
        [TestMethod]
        public void numpy_put_Values_NPScalar() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<sbyte> A = counter<sbyte>(1, 1, 5, 4);
                Array<short> I = -2;
                Array<sbyte> V = -3;
                //Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);

                A.put(I, V);
                Array<sbyte> R = counter<sbyte>(1, 1, 5, 4);
                R[end, end-1] = -3;
                Assert.IsTrue(R.Equals(A));
            }
        }
        [TestMethod]
        public void numpy_put_Values_NonContiguous() {

            Array<float> A = new float[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            Array<float> R = A.C;
            Array<short> I = new short[] { 1, 0, -2, 3, -1 };
            Array<float> V = new float[,] { { -1, -2, -3 }, { -4, -5, -6 }, { -44, -45, -46 } };
            V.a = V[Globals.r(0, 1), Globals.full]; 


            A.put(I, V);
            Assert.IsFalse(R.Equals(A));
            R[0, 1] = -1;
            R[0, 0] = -2;
            R[1, 1] = -3;
            R[1, 0] = -4;
            R[-1] = -5;
            Assert.IsTrue(R.Equals(A));

        }
        [TestMethod]
        public void numpy_put_Indices_NonContiguous() {
            Array<float> A = new float[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            Array<float> R = A.C;
            Array<short> I = new short[,] { { 1, 22, 0, 22, -2, 22, 3, 22, -1, 22 } };
            I.a = I[Globals.r(0, 2, -1)];

            Assert.IsTrue(!I.S.IsContinuous); 

            Array<float> V = new float[,] { { -1, -2, -3 }, { -4, -5, -6 } };

            A.put(I, V);
            Assert.IsFalse(R.Equals(A));
            R[0, 1] = -1;
            R[0, 0] = -2;
            R[1, 1] = -3;
            R[1, 0] = -4;
            R[-1] = -5;
            Assert.IsTrue(R.Equals(A));

        }
        [TestMethod]
        public void numpy_put_Indices_Negative() {
            Array<float> A = new float[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            Array<float> R = A.C;
            Array<short> I = new short[] { 1, 0, -2, 3, -1 };
            Array<float> V = new float[,] { { -1, -2, -3 }, { -4, -5, -6 } };

            A.put(I, V);
            Assert.IsFalse(R.Equals(A));
            R[0, 1] = -1;
            R[0, 0] = -2;
            R[1, 1] = -3;
            R[1, 0] = -4;
            R[-1] = -5;
            Assert.IsTrue(R.Equals(A));

        }
        [TestMethod]
        public void numpy_put_WrapMode() {
            Array<float> A = new float[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            Array<float> R = A.C;

            Array<short> I = new short[] { 1, 0, -20, 31, -4 }; // wraps to: 1, 0, (-20 % 6) + 6 = 4, 31 % 6 = 1, -4 % 6 + 6 = 2
            Array<float> V = new float[,] { { -1, -2, -3 }, { -4, -5, -6 } };

            A.put(I, V, PutModes.Wrap);
            Assert.IsFalse(R.Equals(A));
            R[0, 1] = -1;
            R[0, 0] = -2;
            R[1, 1] = -3;
            R[0, 1] = -4;
            R[0, 2] = -5;
            Assert.IsTrue(R.Equals(A));

        }
        [TestMethod]
        public void numpy_put_ClipMode() {
            Array<float> A = new float[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            Array<float> R = A.C;

            Array<short> I = new short[] { 1, 0, -20, 31, -4 }; // wraps to: 1, 0, -20 => 0, 31 => 5, -4 => 0
            Array<float> V = new float[,] { { -1, -2, -3 }, { -4, -5, -6 } };

            A.put(I, V, PutModes.Clip);
            Assert.IsFalse(R.Equals(A));
            R[0, 1] = -1;
            R[0, 0] = -2;
            R[0, 0] = -3;
            R[1, 2] = -4;
            R[0, 0] = -5;
            Assert.IsTrue(R.Equals(A));

        }
    }
}
