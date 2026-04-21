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
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.InteropServices;
using ILNumerics.Core.Arrays;
using ILNumerics.Core.StorageLayer;
using static ILNumerics.Globals;
using static ILNumerics.ILMath; 

namespace ILNumerics.Core.UnitTests {
    public static partial class Helper {

        public static unsafe void ArrayEqual(Array A, Array B) {
            if (A == null ||B == null) {
                throw new AssertFailedException("A or B was null."); 
            }
            if (A.LongLength != B.LongLength) {
                throw new AssertFailedException("Number of elements don't match.");
            }
            if (A.Rank != B.Rank) {
                throw new AssertFailedException("Ranks don't match.");
            }
            // check dimensions
            for (int i = 0; i < A.Rank; i++) {
                if (A.GetLongLength(i) != B.GetLongLength(i)) {
                    throw new AssertFailedException("Dimension lengths don't match.");
                }
            }

            // check type
            var aType = A.GetType().GetElementType(); 
            if (aType != B.GetType().GetElementType()) {
                throw new AssertFailedException("Element types don't match."); 
            }

            // check values
            var size = Marshal.SizeOf(aType);
            var allSize = size * A.LongLength; 
            GCHandle hA = GCHandle.Alloc(A, GCHandleType.Pinned); 
            GCHandle hB = GCHandle.Alloc(B, GCHandleType.Pinned);
            byte* pA = (byte*)hA.AddrOfPinnedObject(), pB = (byte*)hB.AddrOfPinnedObject(); 

            for (int i = 0; i < allSize; i++) {

                if (*(pA++) != *(pB++)) {
                    throw new AssertFailedException($"Elements at (sequential) position {i} don't match."); 
                }
            }
        }
        /// <summary>
        /// Returns an array similar to A with a non-zero base offset. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="A"></param>
        /// <returns></returns>
        public static Array<T> offset<T>(InArray<T> A) {

            using (Scope.Enter(A)) {
                Array<long> size = A.shape;
                size[r(0, 1)] = size[r(0, 1)] + 1;

                Array<T> ret = empty<T>(size);
                ret[r(1, end), r(1, end), ellipsis] = A;
                ret.a = ret[r(1, end), r(1, end), ellipsis];
                if (ret.S.BaseOffset <= 0) {
                    throw new AssertFailedException("unable to create an offset for the array A.");
                }
                return ret;
            }

        }
        /// <summary>
        /// Returns an array similar to A with a non-zero base offset. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="A"></param>
        /// <returns></returns>
        public static Array<T> offsStride<T>(InArray<T> A) {

            using (Scope.Enter(A, ArrayStyles.ILNumericsV4)) {
                Array<long> size = A.shape;
                size[r(0, 1)] = size[r(0, 1)] * 2 + 1;

                Array<T> ret = empty<T>(size);
                ret[r(1,2, end), r(1,2, end), ellipsis] = A;
                ret.a = ret[r(1, 2, end), r(1, 2, end), ellipsis];
                if (ret.S.BaseOffset <= 0 || ret.S.IsContinuous) {
                    throw new AssertFailedException("unable to create offset/striding for array A.");
                }
                return ret;
            }

        }
        /// <summary>
        /// Returns an array similar to A with a non-zero base offset. 
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        public static Logical offset(InLogical A) {

            using (Scope.Enter(A)) {
                Array<long> size = A.shape;
                size[r(0, 1)] = size[r(0, 1)] + 1;

                Logical ret = empty<int>(size) != 0;
                ret[r(1, end), r(1, end), ellipsis] = A;
                ret.a = ret[r(1, end), r(1, end), ellipsis];
                if (ret.S.BaseOffset <= 0) {
                    throw new AssertFailedException("unable to create an offset for the array A.");
                }
                return ret;
            }

        }
        /// <summary>
        /// Returns an array similar to A with a non-zero base offset. 
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        public static Logical offsStride(InLogical A) {

            using (Scope.Enter(A, ArrayStyles.ILNumericsV4)) {
                Array<long> size = A.shape;
                size[r(0, 1)] = size[r(0, 1)] * 2 + 1;

                Logical ret = empty<int>(size) != 0;
                ret[r(1,2, end), r(1,2, end), ellipsis] = A;
                ret.a = ret[r(1, 2, end), r(1, 2, end), ellipsis];
                if (ret.S.BaseOffset <= 0 || ret.S.IsContinuous) {
                    throw new AssertFailedException("unable to create offset/striding for array A.");
                }
                return ret;
            }

        }
    }
}
