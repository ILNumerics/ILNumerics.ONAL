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


namespace ILNumerics.Core.UnitTests.MathInternalTests {
    [TestClass]
    public class detTests {

        [TestMethod]
        public void det_simple() {
            Array<double> A = counter<double>(1.0, 1.0, 4, 4);
            //A.a = A + diag(A) * 2;
            A[1] = 0; A[14] = 0; 

            Array<double> D = det(A);
            Assert.IsTrue(abs(D - -360) < eps); 

        }

        [TestMethod]
        public void det_simple2x2() {
            Array<double> A = counter<double>(1.0, 1.0, 2, 2, StorageOrders.RowMajor);

            Array<double> D = det(A);
            Assert.IsTrue(abs(D - -2) < eps);
        }

        [TestMethod]
        public void Test_detEmptyMatrix() {
            // special shapes
            Array<double> A = empty<double>();
            Assert.IsTrue(det(A).IsEmpty);
        }


        [TestMethod]
        public void Test_det() {

            Array<double> A = counter<double>(1.0, 1.0, 4, 4);
            A[1] = 0.0;  // makes A nonsingular ..
            A[14] = 0.0; // - '' - 
            Assert.IsTrue(det(A).Equals(-360.0), "invalid result");

            // float 

            Array<float> Af = tosingle(A);
            Assert.IsTrue(det(Af).Equals(-360.0f), "invalid result: float");

            // complex 

            Array<complex> Ac = ccomplex(A, A);
            Assert.IsTrue(abs(det(Ac) - new complex(1440, 0.0)) < epsf, "invalid result: complex");

            Ac.SetValue(new complex(3, -4.0), 0);
            Assert.IsFalse(det(Ac).GetValue(0) - new complex(7.2000e+002, -1.6800e+003) >= 10e-5, "invalid result: complex (2)");
            // fcomplex 

            Array<fcomplex> Afc = ccomplex(tosingle(A), tosingle(A));
            Assert.IsTrue(det(Afc).Equals(new fcomplex(1440.0f, 0.0f)), "invalid result: fcomplex");
            A = -4.70;
            Assert.IsTrue(det(A).Equals(A), "det: scalar double: invalid result");
            Af = -4.70f;
            Assert.IsTrue(det(A).Equals(A), "det: scalar float: invalid result");
            Ac = new complex(-4.70, 45.0);
            Assert.IsTrue(det(Ac).Equals(Ac), "det: scalar complex: invalid result");
            Afc = new fcomplex(-4.70f, -234.0f);
            Assert.IsTrue(det(Afc).Equals(Afc), "det: scalar fcomplex: invalid result");

        }

    }
}
