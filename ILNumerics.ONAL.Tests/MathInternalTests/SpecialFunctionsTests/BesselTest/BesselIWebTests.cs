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
using ILNumerics; 
using static ILNumerics.Globals;
using static ILNumerics.ILMath;

namespace SpecialFunctionsTest
{
    [TestClass]
    public class BessellWebTests
    {

        [TestMethod]
        public void BesselWebConformV5Test() {

            using (Scope.Enter()) {
                // Input values - where the Bessel functions are evaluated
                double start = 0;
                double end = 10;
                Array<double> x = linspace<double>(start, end, 20);

                // Evaluation of different integer orders
                Array<float> data = tosingle(x);
                data[1, full] = tosingle(besselJ0(x));
                data[2, full] = tosingle(besselJ1(x));
                data[3, full] = tosingle(besselJn(x, 2));
                data[4, full] = tosingle(besselJn(x, 3));
                data[5, full] = tosingle(besselJn(x, 4));

                Array<double> res = array(
                    new double[] { 0,1,0,0,0,0,0.5263158,0.9319377,0.2541504,0.03383362,0.002985148,0.0001970753,1.052632,0.7415947,0.4567077,0.12615,0.02266212,0.003024137,1.578947,0.4673774,0.5677165,0.2517302,0.07000009,0.01427009,2.105263,0.1636174,0.5677393,0.375735,0.1461571,0.04081268,2.631579,-0.1115328,0.4618974,0.4625749,0.2412163,0.08739841,3.157895,-0.3088295,0.2781616,0.4849985,0.3361698,0.1537241,3.684211,-0.3983286,0.06037486,0.4311035,0.4076804,0.2328331,4.210526,-0.3750786,-0.1422514,0.3075092,0.4343852,0.3114896,4.736842,-0.258909,-0.2866036,0.1378986,0.4030513,0.372633,5.263158,-0.08853909,-0.3453448,-0.04269194,0.3128989,0.3993967,5.789474,0.08842059,-0.3125421,-0.1963897,0.1768547,0.3796755,6.31579,0.2270655,-0.2040115,-0.2916691,0.01928774,0.3099925,6.842105,0.2955731,-0.05246049,-0.3109078,-0.129301,0.1975207,7.368421,0.2819258,0.1012249,-0.2544505,-0.2393552,0.05954695,7.894737,0.1955131,0.2182992,-0.1402106,-0.2893392,-0.07968719,8.421053,0.06344873,0.2715034,0.001033322,-0.2710126,-0.1941298,8.947369,-0.07726521,0.25118,0.1334113,-0.1915372,-0.2618539,9.473684,-0.1896123,0.1667663,0.2248186,-0.07184294,-0.2703191,10,-0.2459358,0.04347274,0.2546303,0.05837938,-0.2196027 },
                    size(6, 20));

                Assert.IsTrue(maxall(abs(tosingle(res) - data)) < 1e-6f, maxall(abs(tosingle(res) - data)).ToString()); 

            }
        }

    }
}
