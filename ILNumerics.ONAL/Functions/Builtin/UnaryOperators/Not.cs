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
using ILNumerics.Core.Global;
using ILNumerics.Core.StorageLayer;
using System;
using System.Threading;

namespace ILNumerics.Core.Functions.Builtin {

    internal static partial class MathInternal {

        /// <summary>Logical negation of array elements.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Logical negation of array elements.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The storage order of the array returned depends on the order of <paramref name="A"/>. If 
        /// <paramref name="A"/>.<see cref="Size.IsContinuous"/> is <c>true</c> the array returned will have the same <see cref="Size.StorageOrder"/> 
        /// as <paramref name="A"/>. Otherwise, the elements of the returned array will be reordered in the order defined by 
        /// <see cref="Settings.DefaultStorageOrder"/> (default: <see cref="StorageOrders.ColumnMajor"/>).</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered. However, if <paramref name="A"/>'s storage is suitable and not 
        /// shared by other arrays its memory will be used for the result and the operation is performed 'inplace'.</para></remarks>
        
        internal unsafe static Logical not(BaseArray<bool> A) {
            if (object.Equals(A, null)) {
                return null;
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>, out var storage, throwOnNullWithMsg: "In an operation not(A) the input parameter A must be a non-null logical array (ILNumerics.Logical).");
            if (storage.S.NumberOfElements == 0) {
                return LogicalStorage.Create(storage.m_handles, storage.S).RetArray;
            }
            // we need some computations
            byte* pIn = (byte*)storage.Handles[0].Pointer + storage.S.BaseOffset * (uint)sizeof(byte);

            LogicalStorage ret = null;

            var outLen = storage.S.NumberOfElements;
            long oldNumberTrues = storage.NumberTrues; 

            if (storage.S.IsContinuous) {

#region continous

                // continous, out of-place
                // 'new' storage derives the storage order of the source. This 
                // allows us to walk continously through A. However, any base offset
                // is removed from the target storage.
                ret = LogicalStorage.Create(storage.S, storage.S.StorageOrder, 0);

                IntPtr pOut = ret.Handles[0].Pointer;
                InnerLoops.Not.Bool.Continous64OOP(
                    pIn,
                    (byte*)pOut,
                    outLen);

                ret.NumberTrues = storage.S.NumberOfElements - oldNumberTrues;

                #endregion
            } else {
#region strided
                // strided. Output is always continous. No inplace option.
                var outStorageOrder = Settings.DefaultStorageOrder;
                ret = LogicalStorage.Create(storage.S, outStorageOrder); // 'new' storage
                IntPtr pOut = ret.Handles[0].Pointer;
                var bsd = storage.S.GetBSD(write: false);
                var ndims = (uint)bsd[0];
                    // strides are ulong* 
                    long* ordered_bsd = stackalloc long[(int)ndims * 2 + 3];
                    Helper.PrepareBSD(outStorageOrder, bsd, ordered_bsd, (uint)sizeof(byte));

                    InnerLoops.Not.Bool.Strided64(
                        pIn, (byte*)pOut,
                        0, outLen, 
                        ordered_bsd);

                #endregion
            }

            ret.NumberTrues = storage.S.NumberOfElements - oldNumberTrues;

            return ret.RetArray;
        }
    }
    namespace InnerLoops {

        namespace Not {

            internal static class Bool {
                internal static unsafe void Continous64OOP(byte* pIn, byte* pOut, long len) {

                    System.Diagnostics.Debug.Assert(pIn != (byte*)0);
                    System.Diagnostics.Debug.Assert(pOut != (byte*)0);
                    //byte* outRun = (byte*)pOut;
                    while (len >= 8) {
                        *pOut =       (byte) (1 ^ (*(byte*)pIn));
                        *(pOut + 1) = (byte) (1 ^ (*(byte*)(pIn + 1 * sizeof(byte))));
                        *(pOut + 2) = (byte) (1 ^ (*(byte*)(pIn + 2 * sizeof(byte))));
                        *(pOut + 3) = (byte) (1 ^ (*(byte*)(pIn + 3 * sizeof(byte))));
                        *(pOut + 4) = (byte) (1 ^ (*(byte*)(pIn + 4 * sizeof(byte))));
                        *(pOut + 5) = (byte) (1 ^ (*(byte*)(pIn + 5 * sizeof(byte))));
                        *(pOut + 6) = (byte) (1 ^ (*(byte*)(pIn + 6 * sizeof(byte))));
                        *(pOut + 7) = (byte) (1 ^ (*(byte*)(pIn + 7 * sizeof(byte))));
                        pIn += 8 * sizeof(byte); pOut += 8; len -= 8;
                    }
                    while (len-- > 0) {
                        *(pOut++) = (byte) (1 ^ (*(byte*)pIn));
                        pIn += sizeof(byte); 
                    }
                }

                internal static unsafe void Strided32(byte* pSrc, byte* pDest, uint start, uint len, uint* bsd) {
                    int ndims = (int)bsd[0];
                    uint* dims = bsd + 3;
                    uint* strides = dims + ndims;
                    uint stride0 = strides[0];
                    uint* cur = stackalloc uint[ndims];

                    // figure out the dimension index position for start
                    System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside of this method.");

                    cur[0] = start % dims[0];
                    uint f = start / dims[0];
                    uint higdims = 0;
                    for (int i = 1; f > 0 && i < ndims; i++) {
                        cur[i] = f % dims[i];
                        f /= dims[i];
                        higdims += cur[i] * strides[i];
                    }

                    System.Diagnostics.Debug.Assert(f == 0);

                   
                    byte* pOut = pDest + start;

                    while (true) {

                        byte* pIn = pSrc + higdims + cur[0] * stride0;

                        // iteration length limited to either the dimension lengths or the end of the requested chunk 
                        uint leadLen = Math.Min(len, dims[0] - cur[0]);
                        len -= leadLen; 

                        while (leadLen > 8) {
                            pOut[0] = (byte) (1 ^ (*(byte*)pIn));
                            pOut[1] = (byte) (1 ^ (*(byte*)(pIn + stride0)));
                            pOut[2] = (byte) (1 ^ (*(byte*)(pIn + 2 * stride0)));
                            pOut[3] = (byte) (1 ^ (*(byte*)(pIn + 3 * stride0)));
                            pOut[4] = (byte) (1 ^ (*(byte*)(pIn + 4 * stride0)));
                            pOut[5] = (byte) (1 ^ (*(byte*)(pIn + 5 * stride0)));
                            pOut[6] = (byte) (1 ^ (*(byte*)(pIn + 6 * stride0)));
                            pOut[7] = (byte) (1 ^ (*(byte*)(pIn + 7 * stride0)));
                            pOut += 8; pIn += 8 * stride0; leadLen -= 8;
                        }
                        while (leadLen-- > 0) {
                            *pOut++ = (byte) (1 ^ (*(byte*)pIn));
                            pIn += stride0;
                        }
                        if (len == 0) {
                            break;
                        }
                        // reset initial offset in lead dimension after first iteration
                        cur[0] = 0;

                        // increase higher dims
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d] - 1) {
                                higdims += strides[d];
                                cur[d]++;
                                break;
                            } else {
                                cur[d] = 0;
                                higdims -= strides[d] * (dims[d] - 1);
                                d++;
                            }
                        }
                    }
                }

                internal static unsafe void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsd) {

                    uint ndims = (uint)bsd[0];
                    long* dims = bsd + 3;
                    long* strides = dims + ndims;
                    long stride0 = strides[0];
                    long* cur = stackalloc long[(int)ndims];

                    // figure out the dimension index position for start
                    System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

                    cur[0] = start % dims[0];
                    long f = start / dims[0];
                    long higdims = 0;
                    for (int i = 1; f > 0 && i < ndims; i++) {
                        cur[i] = f % dims[i];
                        f /= dims[i];
                        higdims += cur[i] * strides[i];
                    }

                    System.Diagnostics.Debug.Assert(f == 0);

                   
                    byte* pOut = pDest + start;

                    while (true) {

                        byte* pIn = pSrc + higdims + cur[0] * stride0;

                        // iteration length limited to either the dimension lengths or the end of the requested chunk 
                        long leadLen = Math.Min(len, dims[0] - cur[0]);
                        len -= leadLen; 

                        while (leadLen > 8) {
                            pOut[0] = (byte) (1 ^ (*(byte*)pIn));
                            pOut[1] = (byte) (1 ^ (*(byte*)(pIn + 1 * stride0)));
                            pOut[2] = (byte) (1 ^ (*(byte*)(pIn + 2 * stride0)));
                            pOut[3] = (byte) (1 ^ (*(byte*)(pIn + 3 * stride0)));
                            pOut[4] = (byte) (1 ^ (*(byte*)(pIn + 4 * stride0)));
                            pOut[5] = (byte) (1 ^ (*(byte*)(pIn + 5 * stride0)));
                            pOut[6] = (byte) (1 ^ (*(byte*)(pIn + 6 * stride0)));
                            pOut[7] = (byte) (1 ^ (*(byte*)(pIn + 7 * stride0)));
                            pOut += 8; pIn += 8 * stride0; leadLen -= 8;
                        }
                        while (leadLen-- > 0) {
                            *pOut++ = (byte) (1 ^ (*(byte*)pIn));
                            pIn += stride0;
                        }
                        if (len == 0) {
                            break;
                        }
                        // reset initial offset in lead dimension after first iteration
                        cur[0] = 0;

                        // increase higher dims
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d] - 1) {
                                higdims += strides[d];
                                cur[d]++;
                                break;
                            } else {
                                cur[d] = 0;
                                higdims -= strides[d] * (dims[d] - 1);
                                d++;
                            }
                        }
                    }
                }
            }
        }
    }

}
