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
using ILNumerics.Core.Functions.Builtin;
using System.Diagnostics;
using static ILNumerics.Globals; 
using static ILNumerics.ILMath; 

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public unsafe class AllAllTests {

        [TestMethod]
        public void AllAllSimpleTest() {

            Array<double> A = ones<double>(10, 1);

            Logical B = allall(A);

            Assert.IsTrue(!Equals(B, null));
            Assert.IsTrue(B.IsScalar);
            Assert.IsTrue(B.S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions);
            Assert.IsTrue(B.GetValue(0));

            Assert.IsFalse(allall(zeros<uint>(1, 2))); 
        }
        [TestMethod]
        public void AllAll_Scalar_Test() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                Array<byte> A = 11;

                Logical B = MathInternal.allall(A);

                Assert.IsTrue(!Equals(B, null));
                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B.S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions);
                Assert.IsTrue(B);

                A = 0;
                Assert.IsFalse(allall(A)); 
            }
        }

        [TestMethod]
        public void AllAllContMultithread2() {
            Array<double> A = counter<double>(0.1, 0.0, 307, 301);

            using (Settings.Ensure(()=> Settings.MaxNumberThreads, 2u)) { 

                Logical B = MathInternal.allall(A);
                Assert.IsTrue(B);
                B.a = MathInternal.allall(A);
                Assert.IsTrue(B);

                Settings.MaxNumberThreads = 1;
                B.a = MathInternal.allall(A);
                Assert.IsTrue(B);

                Assert.IsTrue(!Equals(B,null));
                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B.S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions);
                
                Settings.MaxNumberThreads = 3;
                B.a = MathInternal.allall(ones(117,23,19));
                Assert.IsTrue(B);
            }
        }

        [TestMethod]
        public void Allall_Strided32SimpleTest() {
            double[] dummy = null;
            Array<double> A = Helper.generateSystemArray<double>(10, 20, ref dummy, ref dummy);
            var bsd = A.S.GetBSD(true);
            // take each second row only
            bsd[1] =bsd[1] / 2;
            bsd[3] =bsd[3] / 2;
            bsd[5] =bsd[5] * 2;

            Logical B = MathInternal.allall(A);

            Assert.IsTrue(B.Storage.NumberTrues == 0); 
            Assert.IsFalse(B);
            Assert.IsTrue(B.IsScalar);

        }
        [TestMethod]
        public void Allall_Strided32MultiThread3Test() {
            double[] dummy = null;
            Array<double> A = Helper.generateSystemArray<double>(200, 20, ref dummy, ref dummy);
            A.SetValue(1, 0);  // makes them ALL non-zero

            var bsd = A.S.GetBSD(true);
            // take each second row only
            bsd[1] = bsd[1] / 2;
            bsd[3] = bsd[3] / 2;
            bsd[5] = bsd[5] * 2;
            
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {

                Logical B = MathInternal.allall(A);

                Assert.IsTrue(B.Storage.NumberTrues == 1);
                Assert.IsTrue(B);
                Assert.IsTrue(B.IsScalar);

                A[ellipsis] = 0;

                B = MathInternal.allall(A);

                Assert.IsFalse(B);
                Assert.IsTrue(B.IsScalar);
                Assert.IsTrue(B.Storage.NumberTrues == 0);

            }


        }
        [TestMethod]
        public void Allall_Strided64MultiThread3BaseOffsetTest() {

            Array<double> A = MathInternal.ones(101, 50);

            A.a = A[r(4, 100 * 50 + 3)].Reshape(100, 50); // creates base offset 4
            A.a = A["0:2:", "0:2:"];  // creates non 1 striding in each dimension
            A.SetValue(0.0, -1); 

            using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {

                Logical B = MathInternal.allall(A);
                  
                Assert.IsFalse(B);
                Assert.IsTrue(B.IsScalar, "B: " + B.ToString());
            }
        }

        [TestMethod]
        public void Allall_EmptyTest() { 
            Array<uint> A = new uint[0];

            Logical B = MathInternal.allall(A);
            
            Assert.IsFalse(B); 
            Assert.IsTrue(B.IsScalar); 
            Assert.IsTrue(B.S[0] == 1);

            Array<short> NPA;
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                NPA = empty<short>(0);
                Assert.IsTrue(NPA.S[0] == 0);
                Assert.IsTrue(NPA.S.NumberOfDimensions == 1);
                Assert.IsTrue(NPA.S.NumberOfElements == 0);
                
                B = allall(NPA);

                Assert.IsTrue(B.IsScalar);
                Assert.IsFalse(B);
                Assert.IsTrue(B.S.NumberOfDimensions == 0);
                Assert.IsTrue(B.S.NumberOfElements == 1);
            }
            // providing np zero (0-dim) in Matlab mode
            B = allall(NPA);
            Assert.IsTrue(B.IsScalar);
            Assert.IsFalse(B);
            Assert.IsTrue(B.S.NumberOfDimensions == 2);
            Assert.IsTrue(B.S.NumberOfElements == 1);
        }

        [TestMethod]
        public void Allall_NP_vectors1d() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<int> A = new int[] { 1, 2, 3, 4 };
                Assert.IsTrue(A.S.NumberOfDimensions == 1); 
                Assert.IsTrue(anyall(A)); 
                Assert.IsTrue(allall(A));
                Assert.IsTrue(anyall(A > 2));
                Assert.IsTrue(allall(A > 0));
                Assert.IsFalse(allall(A > 2));
            }
        }

        [TestMethod]
        public void Allall_NP_Scalars0D() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<int> A = 2;
                Assert.IsTrue(A.S.NumberOfDimensions == 0);
                Assert.IsTrue(anyall(A));
                Assert.IsTrue(allall(A));
                Assert.IsTrue(anyall(A >= 2));
                Assert.IsTrue(allall(A > 0));
                Assert.IsFalse(allall(A > 2));
            }
        }

        [TestMethod]
        public void Allall_Strided32MultiThread2InternalChunkTest() {

            Array<double> A = MathInternal.counter<double>(1.0, 1.0, 10, 401);

            var bsd = A.S.GetBSD(true);
            // take each second row only
            bsd[1] = bsd[1] / 2;
            bsd[3] = bsd[3] / 2;
            bsd[5] = bsd[5] * 2;


            using (Settings.Ensure(() => Settings.MaxNumberThreads, 6u)) {
                // one complete chunk is within a single lead dim run
                Logical B = MathInternal.allall(A == 177);

                Assert.IsFalse(B);
                Assert.IsTrue(B.IsScalar);
            }
        }

    }
}
