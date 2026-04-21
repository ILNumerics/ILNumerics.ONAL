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
