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
using ILNumerics.Core.Functions.Builtin;
using ILNumerics.Core.Global;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.UnitTests {

    [TestClass]
    public class UnaryOperatorSpeedMeasureTests {

        //[TestMethod]
        //public unsafe void MeasureAdjustment1kDouble() {

        //    Settings.MaxNumberThreads = 2; 
        //    double[] rows = new double[0], dummy = new double[0];
        //    Array<double> A = Helper.generateSystemArray(1000, 10, ref rows, ref dummy);
        //    Array<double> B = 1;
        //    Stopwatch sw = new Stopwatch();
        //    uint overhead = 10; 

        //    Action<object> work = data => {
        //        Global.Tuple<IntPtr, IntPtr, int, IntPtr> Data = (Global.Tuple<IntPtr, IntPtr, int, IntPtr>)data;
        //        double* myA = (double*)Data.Item1; 
        //        for (int k = 0; k < Data.Item3; k++) {
        //            myA[0] = (double)k; 
        //        }
        //        (*(InterlockedCounter*)Data.Item4).Decrement(); 
        //    };

        //    for (int i = 200; i >= 0; i -= 2) {
        //        long ticks = 0;
        //        int worker = 1; double a = -1; 
        //        for (int l = 0; l < 50; l++) {

        //            sw.Restart();
        //            ILNumerics.Core.Global.ThreadPool.QueueUserWorkItem(0, work, 
        //                Global.Tuple<IntPtr, IntPtr, int, IntPtr>.Create((IntPtr)(&a), IntPtr.Zero, i, (IntPtr)(&worker)));
        //            var oh = ThreadPool.Wait4Workers(ref worker); 
        //            sw.Stop();
        //            if (oh == 0) {
        //                overhead--; 
        //            } else {
        //                overhead += oh;
        //            }
        //            ticks += sw.ElapsedTicks;
        //            Console.WriteLine($"{overhead}\b");

        //        }
        //        Console.WriteLine($"{i}: {ticks / 100}[ticks]");
        //    }
        //}

        [TestMethod]
        public void SinAutoAdjustTest() {
            int rep = 1000;
            long[] times = new long[rep]; 

            for (int t = 0; t < 5000; t += 100) {


                double[] dummy = null;
                Array<double> A = Helper.generateSystemArray<double>((uint)t, 1, ref dummy, ref dummy);
                Array<double> B = 1;
                // warm up
                MathInternal.sin(A);

                Stopwatch sw = new Stopwatch();
                for (int i = 0; i < rep; i++) {
                    sw.Restart();
                    B.a = MathInternal.sin(A);
                    sw.Stop();
                    times[i] = sw.ElapsedTicks; 
                }
                //Console.WriteLine($"t: {t} time min: {times.Min()}[ticks] max: {times.Max()} mean: {times.Average()} Overhead: {ILNumerics.Core.Functions.Builtin.InnerLoops.Sin.Double.Instance.m_threadingOverhead[0]}");
            }
        }
    }
}
