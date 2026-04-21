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
using ILNumerics.Core.DeviceManagement;
using ILNumerics.Core.MemoryLayer;
using ILNumerics.F2NET;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.Internal {
    public class F2NETDescriptorCache<T> {
        public class CachedItem {
            public MemoryHandle Handle { get; set; }
            public int N { get; set; }
            public int HandleLength { get; set; }
        }

        /// <summary>
        /// One cache is currently enough, since we only use one transform type: CFFTM. Must split for multiple types. 
        /// </summary>
        ConcurrentDictionary<long, WeakReference<CachedItem>> cache { get; set; } = new ConcurrentDictionary<long, WeakReference<CachedItem>>();

        [ThreadStatic]
        static CachedItem _last;
        CachedItem Last {
            get {
                if (_last == null) {
                    _last = new CachedItem();
                }
                return _last;
            }
        }

        Func<long, long> lengthFunction { get; set; }

        public F2NETDescriptorCache(Func<long, long> lengthFunction) {
            this.lengthFunction = lengthFunction;
        }
        public unsafe MemoryHandle getDescriptor(long n, out long length, ManagedFFTPACK5.TransformTypes transformType) {
            if (Last.Handle != null && Last.N == n) {

                length = Last.HandleLength;
                return Last.Handle;
            }
            MemoryHandle ret = null;
            CachedItem item = null;
            int emergExit = 10;
            int descLen = 0;
            do {
                if (!cache.ContainsKey(n) || !cache[n].TryGetTarget(out item)) {
                    descLen = (int)lengthFunction(n);
                    ret = DeviceManager.GetDevice(0).New<T>((ulong)descLen);
                    item = new CachedItem() { Handle = ret, HandleLength = descLen, N = (int)n };
                    cache[n] = new WeakReference<CachedItem>(item);
                    // TODO: currently, we only utilize CFFTMI (single + double precision) 
                    // Must differentiate further, if more FFTPACK5 functions are used (1D, 2D, SIN, COS...)
                    int err = -1;
                    if (this is F2NETDescriptorCache<double>) {
                        //transformType == ManagedFFTPACK5.TransformTypes.Complex_Double) {
                        DFFTPACK5._d4ay1zl0(ref Unsafe.AsRef((int)n), (double*)ret.Pointer, ref descLen, ref err);

                    } else {
                        FFTPACK5._d4ay1zl0(ref Unsafe.AsRef((int)n), (float*)ret.Pointer, ref descLen, ref err);
                    
                    }
                }
            }
            while (item == null && emergExit-- > 0);
            if (item == null) {
                throw new OutOfMemoryException($"Unable to complete this operation. There is no sufficient memory available to the current process.");
            }
            _last = item;
            length = item.HandleLength;
            return item.Handle;
        }

        /// <summary>
        /// Releases memory resources hold on by the cache. Memory is not stored back to the ILNumerics memory pool but released to the OS' heap. 
        /// </summary>
        public void ClearCache() {
            foreach (var c in cache) {
                CachedItem item = null;
                if (c.Value.TryGetTarget(out item)) {
                    item.Handle?.Close();
                }
            }
        }
    }
}

