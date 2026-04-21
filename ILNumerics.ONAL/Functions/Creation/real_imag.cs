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
using ILNumerics.Core.Arrays;
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.Functions.Builtin {
    internal static partial class MathInternal {

        /// <summary>
        /// Creates array with real parts of complex elements from <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Complex input array.</param>
        /// <returns>Array of the same shape and size as <paramref name="A"/> with only the real parts.</returns>
        internal unsafe static Array<double> real(BaseArray<complex> A) {
            var storage = (A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>).Storage;

            var ret = Storage<double>.Create(storage.Handles);
            storage.Handles.Retain();

            real_imag_internal(storage, ret, 0);

            return ret.RetArray;
        }

        /// <summary>
        /// Creates array with real parts of complex elements from <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Complex input array.</param>
        /// <returns>Array of the same shape and size as <paramref name="A"/> with only the real parts.</returns>
        internal unsafe static Array<float> real(BaseArray<fcomplex> A) {
            var storage = (A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>).Storage;

            var ret = Storage<float>.Create(storage.Handles);
            storage.Handles.Retain();

            real_imag_internal(storage, ret, 0);

            return ret.RetArray;
        }

        /// <summary>
        /// Creates array with imaginary parts of complex elements from <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Complex input array.</param>
        /// <returns>Array of the same shape and size as <paramref name="A"/> with only the imaginary parts.</returns>
        internal unsafe static Array<double> imag(BaseArray<complex> A) {
            var storage = (A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>).Storage;

            var ret = Storage<double>.Create(storage.Handles);
            storage.Handles.Retain();

            real_imag_internal(storage, ret, 1);

            return ret.RetArray;
        }
        /// <summary>
        /// Creates array with imaginary parts of complex elements from <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Complex input array.</param>
        /// <returns>Array of the same shape and size as <paramref name="A"/> with only the imaginary parts.</returns>
        internal unsafe static Array<float> imag(BaseArray<fcomplex> A) {
            var storage = (A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>).Storage;

            var ret = Storage<float>.Create(storage.Handles);
            storage.Handles.Retain();

            real_imag_internal(storage, ret, 1);

            return ret.RetArray;
        }

        private static unsafe void real_imag_internal<T1,T2>(Storage<T1> storage, Storage<T2> ret, uint offset_OutElements) {
            long* srcBSD = storage.S.GetBSD(false);
            long* dstBSD = ret.S.GetBSD(true);
            uint ndims = storage.S.NumberOfDimensions;

            dstBSD[0] = srcBSD[0];
            dstBSD[1] = srcBSD[1];
            dstBSD[2] = 2 * srcBSD[2] + offset_OutElements;
            for (int i = 0; i < ndims; i++) {
                dstBSD[3 + i] = srcBSD[3 + i];
                dstBSD[3 + ndims + i] = 2 * srcBSD[3 + ndims + i];
            }
        }
    }
}
