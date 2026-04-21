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
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.InteropServices;
using ILNumerics.Core.Segments;
using System.Security.Cryptography;
//ILN(enabled=false)

namespace ILNumerics.Core.UnitTests {

    [TestClass]
    public class AssignSynchTests {

        [TestMethod]
        public void AssignLocalFromLocalSynchTest() {

            Array<double> A = 1.0;
            Array<double> B = 2.0;

            A.a = B.T;

            Assert.IsTrue(A.Equals(2.0));

            Assert.IsTrue(B.Equals(2.0));

            Assert.IsTrue(!object.Equals(A.Storage, B.Storage));

        }

        [TestMethod]
        public void AssignLocalFromInArraySynchTest() {

            Array<double> A = 1.0;
            Array<double> B = 2.0;

            void innerTest(InArray<double> b) {
                using (Scope.Enter(b)) {
                    A.a = b;

                }
            }
            innerTest(B);
            Assert.IsTrue(A.Equals(2.0));

            Assert.IsTrue(B.Equals(2.0));

            Assert.IsTrue(!object.Equals(A.Storage, B.Storage));

        }

        [TestMethod]
        public void AssignLocalFromOutArraySynchTest() {

            Array<double> A = 1.0;
            Array<double> B = 2.0;

            void innerTest(OutArray<double> b) {
                using (Scope.Enter()) {
                    A.a = b;
                }
            }
            innerTest(B);
            Assert.IsTrue(A.Equals(2.0));

            Assert.IsTrue(B.Equals(2.0));

            Assert.IsTrue(!object.Equals(A.Storage, B.Storage));

        }

        [TestMethod]
        public void AssignLocalFromNullSynchTest() {
            
            using (Scope.Enter(arrayStyle: ArrayStyles.ILNumericsV4)) {
                Array<double> A = 1.0;

                A.a = null;

                Assert.IsTrue(A.IsEmpty);
                Assert.IsTrue(A.S.NumberOfDimensions == 2);
                Assert.IsTrue(A.S.NumberOfElements == 0);
            }

            using (Scope.Enter(arrayStyle: ArrayStyles.numpy)) {
                Array<double> A = 1.0;

                A.a = null;

                Assert.IsTrue(A.IsEmpty);
                Assert.IsTrue(A.S.NumberOfDimensions == 1);
                Assert.IsTrue(A.S.NumberOfElements == 0);
            }

        }



    }
}
