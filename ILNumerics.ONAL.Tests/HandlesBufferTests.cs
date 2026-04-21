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
using static ILNumerics.ILMath;
using static ILNumerics.Globals;
using ILNumerics.Core.MemoryLayer;

#if ILNUMERICS_SEGMENTS
#if USING_OPENCL
using ILNumerics.Core.Segments.OpenCL;
#endif
#endif

using ILNumerics.Core.StorageLayer;

namespace ILNumerics.Core.Tests {

#if ILNUMERICS_SEGMENTS
#if USING_OPENCL


    [TestClass]
    public class HandlesBufferTests {

        [TestMethod]
        public void EnsureBuffer_0001() {
            // create on host
            Array<double> A = ones<double>(10, 20, StorageOrders.ColumnMajor);
            Assert.IsTrue(A.Storage.m_handles[0] != null);
            Assert.IsTrue(A.Storage.m_handles[0] is NativeHostHandle);
            var devs = DeviceManagement.DeviceManager.GetDevices(); 

            if (devs.Length > 0 && devs[1] as CLDevice != null && ((CLDevice)devs[1]).DeviceType == DeviceTypes.CPU) {
                // copy to device 1 (CPU CLDevice)

                var pooledObjCount0 = devs[0].MemoryPool.Count;
                var pooledObjCount1 = devs[1].MemoryPool.Count;
                
                devs[1].EnsureBuffer(A.Storage.m_handles);
                // leaves A's host handle unchanged
                Assert.IsTrue(A.Equals(ones<double>(10, 20, StorageOrders.ColumnMajor)));
                Assert.IsTrue(A.Storage.m_handles[0] != null);
                Assert.IsTrue(A.Storage.m_handles[0] is NativeHostHandle);

                Assert.IsTrue(A.Storage.m_handles[1] != null);
                Assert.IsTrue(A.Storage.m_handles[1] is Segments.OpenCL.Buffer);
                var buffer = A.Storage.m_handles[1] as Segments.OpenCL.Buffer; 
                Assert.IsTrue((long)buffer.Length >= (long)A.Storage.m_handles[0].Length);

                Array<double> B = A.C;
                Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); 
                B.GetHostPointerForWrite();  // detaches, copies elements 
                Assert.IsFalse(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles));
                Assert.IsTrue(B.Storage.m_handles.IsOnDevice(0)); 
                for (int i = 1; i < DeviceManagement.DeviceManager.GetDeviceCount(); i++) {
                    Assert.IsTrue(!B.Storage.m_handles.IsOnDevice(i)); 
                }
                //devs[1].CopyToHost(buffer, B.Storage.m_handles[0], IntPtr.Zero, (IntPtr)(long)buffer.Length);
                Assert.IsTrue(A.Equals(B));

                A.Release();
                Assert.IsTrue(devs[0].MemoryPool.Count == pooledObjCount0 + 1); 
                Assert.IsTrue(devs[1].MemoryPool.Count == pooledObjCount1 + 0);

            }
        }

        [TestMethod]
        public unsafe void AllocateBufferSVM_0001() {

            var storage = Storage<double>.Create();
            storage.S.SetAll(10); 
            storage.m_handles.New<double>(10, 1, false); // 1: (mostly) CPU device 

            Assert.IsTrue(storage.m_handles[0] != null); // implicitly created, base for buffer on (SVM) device 1
            Assert.IsTrue(storage.m_handles[1] != null);

            // make sure buffer is really shared
            double* a = (double*)storage.m_handles[0].Pointer; 
            for (int i = 0; i < 10; i++) {
                a[i] = i; 
            }

            // read back the buffer, it must contain the same values as a* 
            Segments.OpenCL.Buffer buf = storage.m_handles[1] as Segments.OpenCL.Buffer;

            Array<double> B = zeros<double>(10, 1); 
            var dev = DeviceManagement.DeviceManager.GetDevice(1) as CLDevice;

            IntPtr dummy = new IntPtr(); 
            var err = CL.clEnqueueReadBuffer(dev.CommandQueue.ID, buf.ID, 1u, IntPtr.Zero, new IntPtr(10 * sizeof(double)), B.GetHostPointerForWrite(), 0, null, ref dummy);

            Assert.IsTrue(err == CL.CL_SUCCESS); 

            Assert.IsTrue(B.Equals(storage.LocalArray)); 

        }

        [TestMethod]
        public void EnsureDevicesIndexSet() {
            var devs = DeviceManagement.DeviceManager.GetDevices();
            for (int i = 0; i < devs.Length; i++) {
                Assert.IsTrue(devs[i].Index == i);
            }

        }
    }
#endif
#endif

}
