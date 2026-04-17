using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.Native {
    public class DescriptorCache<DescriptorT> : IDisposable where DescriptorT : IDisposable, IEqualityComparer<DescriptorT> {

        ConcurrentDictionary<DescriptorT, ConcurrentStack<DescriptorT>> m_cache = new ConcurrentDictionary<DescriptorT, ConcurrentStack<DescriptorT>>(); 
        
        public DescriptorT GetDescriptor(DescriptorT key) {
            if (m_cache.TryGetValue(key, out ConcurrentStack<DescriptorT> descriptors)) {
                while (descriptors.Count > 0 && descriptors.TryPop(out DescriptorT descriptor)) {
                    return descriptor;
                } 
            }
            return default(DescriptorT); 
        }
        public void AddDescriptor(DescriptorT descriptor) {

            if (!m_cache.TryGetValue(descriptor, out ConcurrentStack<DescriptorT> descriptors)) {
                descriptors = new ConcurrentStack<DescriptorT>();
                if (!m_cache.TryAdd(descriptor, descriptors)) {
                    descriptors = m_cache[descriptor];
                }
            }
            descriptors.Push(descriptor);

        }

        public void Dispose() {
            var cache = m_cache;
            m_cache = null; 
            if (cache != null) {
                foreach (var item in cache.Values) {
                    if (item != null) {
                        foreach (var descr in item) {
                            if (descr != null) {
                                descr.Dispose(); 
                            }
                        }
                    }
                }
            }
        }
            
    }
}
