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
using ILNumerics.Core.MemoryLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.DeviceManagement
{
    /// <summary>
    /// This class allows to access all <see cref="Device"/> objects available on the system.
    /// </summary>
    public class DeviceManager {

        private static Device[] s_devices;

        #region constructor
        static DeviceManager()
        {
            Device[] devices = new Device[1] { new HostDevice() };

            s_devices = devices; 

            // initialize devices. Important to have all devices set-up here! Initialize requires DeviceCount!
            foreach (var d in devices) {
                d.Initialize(); 
            }

        }
        #endregion

        #region public API 

        /// <summary>
        /// Gets the number of <see cref="Device"/>s currently available on the system. 
        /// </summary>
        /// <returns>Device count.</returns>
        /// <seealso cref="GetDevice(uint)"/>
        /// <seealso cref="GetDevices"/>
        public static int GetDeviceCount() {
            return s_devices.Length; 
        }

        /// <summary>
        /// Gives the (original!) array of available <see cref="Device"/> objects. Use with care and do not change the elements! No copy is made for performance reasons!
        /// </summary>
        /// <returns>Direct reference to the array hosting <see cref="Device"/> objects available on the system.</returns>
        public static Device[] GetDevices() 
        {
            return s_devices; 
        }

        /// <summary>
        /// Find the index of the first device available on the runtime system, matching the specified category.
        /// </summary>
        /// <param name="category">The device category to search for.</param>
        /// <returns>The 0-based index of the device found. If no device matches the category 0 is returned (targeting the CLR Host device) as fallback.</returns>
        public static uint GetIndexOrDefault(DeviceTypes category) {
            foreach (var dev in GetDevices()) {
                if (dev.DeviceType == category) {
                    return (uint)dev.Index;
                }
            }
            return 0; 
        }

        /// <summary>
        /// Get the device stored at index <paramref name="i"/>.
        /// </summary>
        /// <param name="i">The index of the device in the array of devices.</param>
        /// <returns>The device pointed to by <paramref name="i"/>.</returns>
        /// <remarks><para>There will always be at least one <see cref="Device"/> on a system: the host device at index 0.</para>
        /// <para>Make sure to request indices for available devices only! <paramref name="i"/> must be <c>%gt;= 0</c> and lower than <see cref="GetDeviceCount()"/>.</para></remarks>
        public static Device GetDevice(uint i)
        {
            return s_devices[i];
        }

        /// <summary>
        /// Copy memory from one device to another device, potentially using the host as temporary storage. 
        /// </summary>
        /// <param name="source">Handle to the source memory.</param>
        /// <param name="sourceDevice">Index of the source memory device.</param>
        /// <param name="target">Handle to the target memory.</param>
        /// <param name="targetDevice">Index of the target device. </param>
        /// <param name="start">Offset / index of the first byte to copy from <paramref name="source"/>.</param>
        /// <param name="length">Number of bytes to copy.</param>
        /// <remarks><para>If <paramref name="sourceDevice"/> and <paramref name="targetDevice"/> are not equal the memory will 
        /// be stored on the host device first and copied to the <paramref name="target"/>, unless <paramref name="targetDevice"/> != 0.</para></remarks>
        
        internal static unsafe void Copy(MemoryHandle source, uint sourceDevice, MemoryHandle target, uint targetDevice, ulong start, IntPtr length) {
            if (0 == sourceDevice && 0 == targetDevice) {
                //Native.NativeMethods.CopyMemory((IntPtr)((byte*)target.Pointer + start), source.Pointer, length);
                System.Buffer.MemoryCopy(
                    source: (byte*)source.Pointer + (long)start,
                    destination: (byte*)target.Pointer,
                    sourceBytesToCopy: (long)length,
                    destinationSizeInBytes: (long)length); 
            } else {
                throw new NotImplementedException();
            }
        }
        #endregion
    }
}
