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
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using ILNumerics.Core.DeviceManagement;

namespace ILNumerics.Core.MemoryLayer {
    /// <summary>
    /// A memory pool allocating memory on the managed heap. 
    /// </summary>
    /// <typeparam name="T">Element type.</typeparam>
    
    public class ManagedHostPool<T> : MemoryPool<ManagedHostHandle<T>> {

        [ThreadStatic]
        private static new ManagedHostPool<T> m_pool;

        public ManagedHostPool(Device device, MemoryTypes memoryType) : base(device, memoryType) {
        }

        #region properties
        public static ManagedHostPool<T> Pool {
            get {
                if (m_pool == null) {
                    m_pool = new ManagedHostPool<T>(DeviceManagement.DeviceManager.GetDevice(0), MemoryTypes.Managed); 
                }
                return m_pool; 
            }
        }

        #endregion

        /// <summary>
        /// Create new ManagedHostHandle 
        /// </summary>
        /// <param name="length">Byte length!! (not element count!!) </param>
        /// <returns></returns>
        
        internal override ManagedHostHandle<T> AllocateInternal(UIntPtr length) {
            var ret = new ManagedHostHandle<T>(new UIntPtr(length.ToUInt32() / (uint)UIntPtr.Size));
            // set the base class Pool field. Class siblings use it for clean-up. We don't. 
            ret.Device = this.Device; 
            return ret; 
        }
    }
}
