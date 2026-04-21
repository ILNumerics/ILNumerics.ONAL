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
    public class SVDTests {

        [TestMethod]
        public void SVD_simple() {
            Array<double> A = ones(10, 10);
            Array<double> ResS = zeros(10, 1);
            ResS[0, 0] = 10.0;
            Array<double> U = empty<double>();
            Array<double> V = empty<double>();

            Array<double> B = svd(A);
            Assert.IsTrue(norm(B - ResS) < 1e-10, "Invalid values detected!");


            A = ones(3, 3);
            Array<double> ResU = vector<double>(
                new double[9] { -0.577350, -0.577350, -0.577350, -0.577350, -0.211325, 0.788675, -0.577350, 0.788675, -0.211325 }).Reshape(3, 3);
            Array<double> ResV = vector<double>(
                new double[9] { -0.5773503, -0.5773503, -0.5773503, 0, -0.7071068, 0.7071068, 0.8164966, -0.4082483, -0.4082483 }).Reshape(3, 3);
            ResS = zeros(3, 3);
            ResS[0, 0] = 3.0;
            B = svd(A, U, V);
            Assert.IsTrue(norm(B - ResS) < 1e-10, "Invalid values detected!");
            Assert.IsTrue(norm(multiply(U, B, V.T) - A) < eps * A.S.Longest * 2, "Invalid values detected!");

            ///////////////////////////////   float ///////////////////////////////////////

            Array<float> fA = array<float>(1f, vector<long>(10, 10));
            Array<float> fResS = zeros<float>(10, 1);
            fResS[0, 0] = 10.0f;
            Array<float> fU = empty<float>();
            Array<float> fV = empty<float>();

            Array<float> fB = svd(fA);
            Assert.IsTrue(norm(fB - fResS) < 1e-5f, "Invalid values detected!");

            fA = tosingle(reshape<float>(vector<float>(1, 2, 3, 4, 5, 6, 7, 8, 9), 3, 3));
            //Array<float> fResU = create<float>(
            //    new float[9] { -0.577350f, -0.577350f, -0.577350f,0.8164966f, -0.4082483f, -0.4082483f, 3.441276e-08f, -0.7071068f, 0.7071068f
            //                 }, 3, 3);
            //Array<float> fResV = create<float>(
            //    new float[9] { -0.5773503f, -0.5773503f, -0.5773503f, -0.8164966f,0.4082483f, 0.4082483f, 0.0f, 0.7071068f,-0.7071068f
            //                  }, 3, 3);
            //if (Lapack is LapackGeneric)
            //{
            //    fResV = fResV.T; 
            //}
            //fResS = single( zeros(3, 3));
            //fResS[0, 0] = 3.0f;
            fB = svd(fA, fU, fV);
            Assert.IsTrue(norm(multiply(multiply(fU, fB), fV.T) - fA) < 1e-5f, "invalid values detected:svd(fA, ref fU, ref fV);");

            ///////////////////////////////   complex ///////////////////////////////////////

            Array<complex> cA = ccomplex(ones(10, 10),0);
            ResS = zeros(10, 1);
            ResS[0, 0] = 10.0;
            Array<complex> cU = ccomplex(empty<double>(),0);
            Array<complex> cV = ccomplex(empty<double>(),0);

            B = svd(cA);
            Assert.IsTrue(norm(B - ResS) < 1e-6, "Invalid values detected!");

            cA = ccomplex(ones(3, 3),0);
            //Array<complex> cResU = create<complex>(
            //    new complex[9] { -0.577350, -0.577350, -0.577350, -0.577350, -0.211325, 0.788675, -0.577350, 0.788675, -0.211325
            //                 }, 3, 3);
            //Array<complex> cResV = create<complex>(
            //    new complex[9] { -0.5773503, -0.5773503, -0.5773503, 0, -0.7071068, 0.7071068, 0.8164966, -0.4082483, -0.4082483
            //                  }, 3, 3);
            //ResS = zeros(3, 3);
            //ResS[0, 0] = 3.0;
            B = svd(cA, cU, cV);
            Assert.IsTrue(norm(multiply(multiply(cU, ccomplex(B,0)), cV.T) - cA) < 1e-6, "Invalid values detected!");

            //if (norm(abs(cU - cResU)) > 1e-6)
            //    throw new Exception("Invalid values detected!");
            //if (norm(abs(cV - cResV)) > 1e-6)
            //    throw new Exception("Invalid values detected!");

        }


    }
}
