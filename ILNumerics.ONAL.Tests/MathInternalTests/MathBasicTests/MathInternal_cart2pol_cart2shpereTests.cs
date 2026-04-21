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
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;
using static ILNumerics.Globals;
using System.Diagnostics;

namespace ILNumerics.Core.UnitTests.MathInternalTests {
    /// <summary>
    /// Summary description for MathInternal_cumsumTests
    /// </summary>
    [TestClass]
    public class MathInternal_cart2polTests {

        [TestMethod]
        public void MathInternal_cart2pol_simpleTest() {

            Array<double> X = cos(linspace<double>(0, 2 * pi, 10));
            Array<double> Y = sin(linspace<double>(0, 2 * pi, 10));
            Array<double> rad = 1;
            Array<double> ang = cart2pol(X, Y, null, rad);

            Assert.IsTrue(allall(abs(rad - 1) < eps)); 
            Assert.IsTrue(allall(abs((linspace<double>(0, 2 * pi, 10) - ang) % (2 * pi)) < eps)); 

        }

        [TestMethod]
        public void MathInternal_cart2sphere_simpleTest() {

            Array<double> X = cos(linspace<double>(0, 2 * pi, 10));
            Array<double> Y = sin(linspace<double>(0, 2 * pi, 10));
            Array<double> phi = 1, theta = 1;
            Array<double> r = cart2sphere(X, Y, 0, theta, phi);

            Assert.IsTrue(allall(abs(r - 1) < eps));
            Assert.IsTrue(allall(abs(pi / 2 - theta) < eps));
            Assert.IsTrue(allall(abs((linspace<double>(0, 2 * pi, 10) - phi) % (pi)) < eps));

        }

        [TestMethod]
        public void MathInternal_cart2PolSizeOfZBroadcasted() {

            Array<double> X = cos(linspace<double>(0, 2 * pi, 10));
            Array<double> Y = sin(linspace<double>(0, 2 * pi, 10));
            Array<double> rad = 1, outZ = 0;
            Array<double> ang = cart2pol(X, Y, 1, rad, outZ);

            Assert.IsTrue(allall(abs(rad - 1) < eps));
            Assert.IsTrue(allall(abs((linspace<double>(0, 2 * pi, 10) - ang) % (2 * pi)) < eps));

            Assert.IsTrue(outZ.Equals(ones<double>(X.S))); // must have been broadcasted to out size!

        }
        [TestMethod]
        public void MathInternal_cart2PolSizeOfZBroadcastedFloat() {

            Array<float> X = cos(linspace<float>(0, 2 * pif, 10));
            Array<float> Y = sin(linspace<float>(0, 2 * pif, 10));
            Array<float> rad = 1, outZ = 0;
            Array<float> ang = cart2pol(X, Y, 1, rad, outZ);

            Assert.IsTrue(allall(abs(rad - 1) < epsf));
            Array<float> checkAng = (linspace<float>(0, 2 * pif, 10) - ang) / pif - round((linspace<float>(0, 2 * pif, 10) - ang) / pif);
            Assert.IsTrue(allall(abs(checkAng) < epsf * 2 * pif));

            Assert.IsTrue(outZ.Equals(ones<float>(X.S))); // must have been broadcasted to out size!

        }
    }
}
