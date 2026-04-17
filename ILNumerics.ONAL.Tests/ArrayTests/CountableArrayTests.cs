using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILNumerics.Core;
using System.Diagnostics;

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public class CountableArrayTests {

        [TestMethod]
        public void CreateCountableArray() {
            var ca = CountableArray.Create();

            Assert.IsTrue(ca != null); 
            Assert.IsTrue(ca.GetHandlesArray() != null);
            Assert.IsTrue(ca.GetHandlesArray().Length > 0);

        }

        [TestMethod]
        public void CountableArrayCleanupTest() {

            var ca = CountableArray.Create();

            var dev = DeviceManagement.DeviceManager.GetDevice(0);

            //make sure pools maxsize was not set to 0 by other tests
            dev.MemoryPool.MaxSize = dev.GetMaxPoolSizeHint(); 

            // ca.Retain(); ca was created with RC = 1
            ca[0] = dev.New<byte>(10, false);  // New -> handle, refcount==0 -> store: 1

            Assert.IsTrue(ca.GetHandlesArray().Length == DeviceManagement.DeviceManager.GetDeviceCount());
            Assert.IsTrue(ca.GetHandlesArray()[0] != null);
            Assert.IsTrue(ca.GetHandlesArray()[0].Length == new UIntPtr(16));

            ca.Retain();

            ca.Release();

            var memHandle = ca[0];
            Assert.IsTrue(memHandle != null);
            var oldMemPoolCount = dev.MemoryPool.Count; 

            ca.Release();

            Assert.IsTrue(ca.GetHandlesArray().Length == DeviceManagement.DeviceManager.GetDeviceCount());
            // the handles are cleared
            Assert.IsTrue(ca.GetHandlesArray()[0] == null);

            // the storage is in the pool
            Assert.IsTrue(dev.MemoryPool.Count >= 1, $"dev.MemoryPool.Count: {dev.MemoryPool.Count} - oldPoolCount: {oldMemPoolCount}");  
            Assert.IsTrue(dev.MemoryPool.Size >= 16, $"dev.MemoryPool.Size: {dev.MemoryPool.Size}");

            // the countable array is cached 
            Assert.IsTrue(ca.Previous != null); 

        }

    }
}
