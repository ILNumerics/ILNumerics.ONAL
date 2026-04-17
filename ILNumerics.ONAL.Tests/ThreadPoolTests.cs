using System;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.Globals;
using static ILNumerics.ILMath; 

namespace ILNumerics.Core.UnitTests {

    [TestClass]
    public class ThreadPoolTests {

        [TestMethod]
        public void ThreadPoolMaxThreadsOverallTest() {
            // make sure there are no more threads than specified in process

            var initThreads = Math.Max(Settings.MaxNumberThreads, (uint)getNrILNThreadPoolThreads() + 1u);
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 1u)) {
                var numThreads = getNrILNThreadPoolThreads(); 
                Assert.IsTrue(numThreads == Math.Max(initThreads - 1,1), $"Number of threads found were expected to count: {Math.Max(initThreads - 1, 1)}. But found were: {numThreads}.");
                Assert.IsTrue(Settings.MaxNumberThreads == 1);

                Settings.MaxNumberThreads = initThreads + 2;
                var nuThreads = getNrILNThreadPoolThreads(); 
                Assert.IsTrue(nuThreads == initThreads + 2 - 1, $"Expected {initThreads + 2 - 1} threads in thread pool. Found: {nuThreads}. InitThread was: {initThreads}.");
                Assert.IsTrue(Settings.MaxNumberThreads == initThreads + 2);

                Settings.MaxNumberThreads = 2;
                nuThreads = getNrILNThreadPoolThreads(); 
                Assert.IsTrue(nuThreads == initThreads + 2 - 1, $"Expected {initThreads + 2 - 1} threads in thread pool. Found: {nuThreads}. InitThread was: {initThreads}.");
                Assert.IsTrue(Settings.MaxNumberThreads == 2);

            }
        }

        private int getNrILNThreadPoolThreads() {
            var threads = ILNumerics.Core.Global.ThreadPool.Pool.m_threads;
            return threads.Count; 
        }
    }
}
