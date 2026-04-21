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
