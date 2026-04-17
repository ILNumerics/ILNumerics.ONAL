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
