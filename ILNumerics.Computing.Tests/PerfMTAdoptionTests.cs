using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;
using static ILNumerics.Globals; 

namespace ILNumerics.Core.Tests {
    [TestClass]
    public class PerfMTAdoptionTests {

        //[TestMethod]
        //public void BinaryMTAdoptionRange() {

        //    //using (Settings.Ensure<uint>(() => Settings.MaxNumberThreads, 3)) 

        //    //Settings.MaxNumberThreads = 3;

        //    {
        //        for (int i = 0; i < 50000; i += 50) {
        //            long wil;
        //            uint wic;
        //            var oh = ILNumerics.Core.Functions.Builtin.InnerLoops.Add.Double.Instance.Threading_overheadS64;
        //            ILNumerics.Core.Global.Helper.determineMultithreadingParameters(i,
        //                ref ILNumerics.Core.Functions.Builtin.InnerLoops.Add.Double.Instance.Threading_overheadS64,
        //                out wic, out wil);
        //            var ms = testMTAdoption(i);
        //            Console.Out.WriteLine($"{i}: MT: {ms}ms - pre oh: {oh} wic: {wic} wil:{wil}");
        //        }
        //    }
        //    Console.WriteLine("==========================================================");

        //    using (Settings.Ensure<uint>(() => Settings.MaxNumberThreads, 1)) {

        //        for (int i = 0; i < 50000; i += 50) {
        //            long wil;
        //            uint wic;
        //            var oh = ILNumerics.Core.Functions.Builtin.InnerLoops.Add.Double.Instance.Threading_overheadS64;
        //            ILNumerics.Core.Global.Helper.determineMultithreadingParameters(i,
        //                ref ILNumerics.Core.Functions.Builtin.InnerLoops.Add.Double.Instance.Threading_overheadS64,
        //                out wic, out wil);
        //            var ms = testMTAdoption(i);
        //            Console.Out.WriteLine($"{i}: ST: {ms}ms - pre oh: {oh} wic: {wic} wil:{wil}");
        //        }
        //    }
        //}
    

        private long testMTAdoption(int nrElem) {
            Stopwatch sw = new Stopwatch();

            Array<double> A = ones<double>(2, nrElem);

            Array<double> B = 0; 

            sw.Start();
            for (int i = 0; i < 100; i++) {
                using (Scope.Enter()) {
                    B = A + A + A + A; 
                }
            }
            sw.Stop();
            B = B[full]; 

            long mtTime = (long)(sw.ElapsedMilliseconds);

            ////using (Settings.Ensure<uint>(() => Settings.MaxNumberThreads, 1))
            //{
            //    Settings.MaxNumberThreads = 1;
            //    sw.Restart();
            //    for (int i = 0; i < 10; i++) {
            //        using (Scope.Enter()) {
            //            Array<double> B = A + A + A + A;
            //        }
            //    }
            //    sw.Stop();
            //}
            //stTime = (long)(sw.ElapsedMilliseconds / 10);
            return mtTime; 
        }
    }
}
