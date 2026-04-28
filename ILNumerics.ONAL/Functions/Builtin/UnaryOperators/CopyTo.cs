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
//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////

using ILNumerics.Core.Arrays;
using ILNumerics.Core.Global;
using ILNumerics.Core.MemoryLayer;
using ILNumerics.Core.Native;
using ILNumerics.Core.StorageLayer;
using System;
using System.Security;
using System.Threading;

namespace ILNumerics.Core.Functions.Builtin {

    internal static class CopyToOperators {

        /// <summary>
        /// Copy the data of this array to another host memory region, specify element storage order for writing. 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="inSize"></param>
        /// <param name="dest">Pointer to a memory region on the CLR host. The region must be large enough to store all elements of this array in the storage layout specified by <paramref name="layout"/>.</param>
        /// <param name="outSize">[Output] On return the (predefined, pre-allocated) size descriptor holds the dimension lengths and strides according to the size of this array and the specified <paramref name="layout"/>.</param>
        /// <param name="layout">The storage order used to write the elements to <paramref name="dest"/>. If this is null on entry <see cref="Settings.DefaultStorageOrder"/> is used.</param>
        /// <param name="sizeofT"></param>
        /// <remarks><para><paramref name="outSize"/> can be <c>null</c> on entry in which case it will be ignored.</para>
        /// <para>If <paramref name="layout"/> is <c>null</c> or <see cref="StorageOrders.Other"/> the storage layout of the array 
        /// returned will be automatically determined based on the current storage layout: copying from continous storages will keep 
        /// the source storage layout (column- or row major layout). Copying from non-continous storages will create storage as determined by <see cref="Settings.DefaultStorageOrder"/>. </para>
        /// <para>If <paramref name="layout"/> is one of <see cref="StorageOrders.ColumnMajor"/> or <see cref="StorageOrders.RowMajor"/> the 
        /// elements are stored into <paramref name="dest"/> with this layout.</para>
        /// <para>Make sure that the memory region addressed by <paramref name="dest"/> is large enough to hold at least all elements of 
        /// the region to be copied.</para>
        /// </remarks>

        internal static unsafe void CopyTo(IntPtr src, Size inSize, IntPtr dest, Size outSize, StorageOrders? layout, uint sizeofT) {

            if (!layout.HasValue || layout == StorageOrders.Other || layout == inSize.StorageOrder) {
                if (!Equals(inSize,null) && inSize.IsContinuous) {
                    // copy whole block. TODO: parallelize? 
                    //NativeMethods.CopyMemory(dest, new IntPtr(src.ToInt64() + (long)(inSize.BaseOffset * sizeofT)), new IntPtr(inSize.NumberOfElements * sizeofT));
                    System.Buffer.MemoryCopy(
                        source: (byte*)src + inSize.BaseOffset * sizeofT,
                        destination: (void*)dest,
                        sourceBytesToCopy: inSize.NumberOfElements * sizeofT,
                        destinationSizeInBytes: inSize.NumberOfElements * sizeofT);
                    if (!Equals(outSize, null))
                        outSize.SetAll(inSize.GetBSD(write: true), 0, flags: inSize.Flags);
                    return;
                } else {
                    // determine target storage layout
                    layout = Settings.DefaultStorageOrder;
                }
            }
            System.Diagnostics.Debug.Assert(layout != null && layout != StorageOrders.Other);
            switch (sizeofT) {
                case 1:
                    CopyTo_1(src, inSize, dest, outSize, layout);
                    break;
                case 2:
                    CopyTo_2(src, inSize, dest, outSize, layout);
                    break;
                case 4:
                    CopyTo_4(src, inSize, dest, outSize, layout);
                    break;
                case 8:
                    CopyTo_8(src, inSize, dest, outSize, layout);
                    break;
                case 16:
                    CopyTo_16(src, inSize, dest, outSize, layout);
                    break;
                default:
                    CopyTo_Arbitrary(src, inSize, dest, outSize, layout, sizeofT);
                    break; 
                    //throw new InvalidOperationException("This operation is not supported on the element datatype. Supported data types have lengths of 1, 2, 4, 8 or 16 bytes.");
            }
        }

        private static unsafe void CopyTo_Arbitrary(IntPtr src, Size inSize, IntPtr dest, Size outSize, StorageOrders? layout, uint sizeofT) {

            // no checks for src- / dest integrity!!! Must all be done in caller

            System.Diagnostics.Debug.Assert(!(inSize.IsContinuous && (!layout.HasValue || layout == StorageOrders.Other)), "This internal method should only handle copy operations when the faster shortcut is not possible (continous storage and keep layout).");
            // this is non-continuous storage or we need to reorder dimensions

            uint ndims = (uint)inSize.NumberOfDimensions;
            long outLen = inSize.NumberOfElements;

            // empty arrays 
            if (outLen == 0) {
                return;
            }
            System.Diagnostics.Debug.Assert(!Equals(inSize, null) && inSize.NumberOfElements > 0, "Empty arrays must be handled separately.");

            //// straight forward loop over cur / iter_str 
            byte* pIn = (byte*)src;  // init without base offset. Any offset is considered in the iterators below.
            byte* pOut = (byte*)dest; // any potential BaseOffset in outSize is ignored here! ... safes a bunch of null-checks

            if (outLen == 1) {
                pOut[0] = pIn[0];
                return;
            }

            // strides are long* 
            long* dummyBSD = stackalloc long[(int)ndims * 2 + 3];
            Helper.PrepareBSD4CopyTo(layout.GetValueOrDefault(), inSize.GetBSD(false),
                              dummyBSD, outSize, (uint)sizeof(ushort));
            var inIter = inSize.Iterator(layout.GetValueOrDefault()).GetEnumerator();

            while (inIter.MoveNext()) {
                for (int i = 0; i < sizeofT; i++) {
                    pOut[i] = pIn[inIter.Current * sizeofT + i]; 
                }
                pOut += sizeofT;
            }

        }

        #region HYCALPER LOOPSTART

        /*!HC:TYPELIST:
        <hycalper>
        <type>
            <source locate="here">
                CopyTo_2
            </source>
            <destination>CopyTo_1</destination>
            <destination>CopyTo_4</destination>
            <destination>CopyTo_8</destination>
            <destination>CopyTo_16</destination>
        </type>
        <type>
            <source locate="here">
                ushort
            </source>
            <destination>byte</destination>
            <destination>uint</destination>
            <destination>ulong</destination>
            <destination>complex</destination>
        </type>
        <type>
            <source locate="here">
                UInt16
            </source>
            <destination>Byte</destination>
            <destination>UInt32</destination>
            <destination>UInt64</destination>
            <destination>Complex</destination>
        </type>
        </hycalper>
        */
        
        private static unsafe void CopyTo_2(IntPtr src, Size inSize, IntPtr dest, Size outSize, StorageOrders? storageOrder) {

            // no checks for src- / dest integrity!!! Must all be done in caller

            System.Diagnostics.Debug.Assert(!(inSize.IsContinuous && (!storageOrder.HasValue || storageOrder == StorageOrders.Other)), "This internal method should only handle copy operations when the faster shortcut is not possible (continous storage and keep layout).");
            // this is non-continuous storage or we need to reorder dimensions

            uint ndims = (uint)inSize.NumberOfDimensions;
            long outLen = inSize.NumberOfElements;

            // empty arrays 
            if (outLen == 0) {
                return;
            }
            System.Diagnostics.Debug.Assert(!Equals(inSize, null) && inSize.NumberOfElements > 0, "Empty arrays must be handled separately.");

            //// straight forward loop over cur / iter_str 
            ushort* pIn = (ushort*)src + inSize.BaseOffset;  // init with base offset
            ushort* pOut = (ushort*)dest; // any potential BaseOffset in outSize is ignored here! ... safes a bunch of null-checks

            if (outLen == 1) {
                pOut[0] = pIn[0];
                return;
            }

#if TEST_1000ASUINTMAXVALUE
            if (inSize.GetElementSpan() > 1000) {
#else
            if (Environment.Is64BitProcess && inSize.GetElementSpan() * (uint)sizeof(uint) > uint.MaxValue) {
#endif
                // strides are long* 
                long* ordered_bsd = stackalloc long[(int)ndims * 2 + 3];
                Helper.PrepareBSD4CopyTo(storageOrder.GetValueOrDefault(), inSize.GetBSD(false),
                                  ordered_bsd, outSize, (uint)sizeof(ushort));
                // ordered_strides has SCALED strides! -> pIn is now byte*!
                // pIn, pOut -> include base offset! 
                UInt16.Strided64(
                    (byte*)pIn, pOut,
                    0, outLen,
                    ordered_bsd);

            } else {
                // strides are uint* 
                uint* ordered_bsd = stackalloc uint[(int)ndims * 2 + 3];
                Helper.PrepareBSD4CopyTo(storageOrder.GetValueOrDefault(), inSize.GetBSD(false),
                                  ordered_bsd, outSize, (uint)sizeof(ushort));
                // ordered_strides has SCALED strides! -> pIn is now byte*!

                // pIn, pOut -> include base offset! 
                UInt16.Strided32(
                    (byte*)pIn, pOut,
                    0, (uint)outLen,
                    ordered_bsd);

            }
        }

        internal static class UInt16 {

            // pSrc is b_y_t_e* - always!! increments are faster by scaled strides!  
            internal static unsafe void Strided32(byte* pSrc, ushort* pDest, uint start, uint len, uint* bsd) {
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


                ushort* pOut = pDest + start;

                while (true) {

                    byte* pIn = pSrc + higdims + cur[0] * stride0;

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    uint leadLen = Math.Min(start + len - (uint)(pOut - pDest), dims[0] - cur[0]);

                    while (leadLen > 8) {
                        pOut[0] = (*(ushort*)pIn);
                        pOut[1] = (*(ushort*)(pIn + stride0));
                        pOut[2] = (*(ushort*)(pIn + 2 * stride0));
                        pOut[3] = (*(ushort*)(pIn + 3 * stride0));
                        pOut[4] = (*(ushort*)(pIn + 4 * stride0));
                        pOut[5] = (*(ushort*)(pIn + 5 * stride0));
                        pOut[6] = (*(ushort*)(pIn + 6 * stride0));
                        pOut[7] = (*(ushort*)(pIn + 7 * stride0));
                        pOut += 8; pIn += 8 * stride0; leadLen -= 8;
                    }
                    while (leadLen-- > 0) {
                        *pOut++ = (*(ushort*)pIn);
                        pIn += stride0;
                    }
                    if (pOut == pDest + start + len) {
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
            // pSrc is b_y_t_e* - always!! increments are faster by scaled strides!  
            internal static unsafe void Strided64(byte* pSrc, ushort* pDest, long start, long len, long* bsd) {

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


                ushort* pOut = pDest + start;

                while (true) {

                    byte* pIn = pSrc + higdims + cur[0] * stride0;

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(start + len - (long)(pOut - pDest), dims[0] - cur[0]);

                    while (leadLen > 8) {
                        pOut[0] = (*(ushort*)pIn);
                        pOut[1] = (*(ushort*)(pIn + stride0));
                        pOut[2] = (*(ushort*)(pIn + 2 * stride0));
                        pOut[3] = (*(ushort*)(pIn + 3 * stride0));
                        pOut[4] = (*(ushort*)(pIn + 4 * stride0));
                        pOut[5] = (*(ushort*)(pIn + 5 * stride0));
                        pOut[6] = (*(ushort*)(pIn + 6 * stride0));
                        pOut[7] = (*(ushort*)(pIn + 7 * stride0));
                        pOut += 8; pIn += 8 * stride0; leadLen -= 8;
                    }
                    while (leadLen-- > 0) {
                        *pOut++ = (*(ushort*)pIn);
                        pIn += stride0;
                    }
                    if (pOut == pDest + start + len) {
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
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 

       
        
        private static unsafe void CopyTo_16(IntPtr src, Size inSize, IntPtr dest, Size outSize, StorageOrders? storageOrder) {

            // no checks for src- / dest integrity!!! Must all be done in caller

            System.Diagnostics.Debug.Assert(!(inSize.IsContinuous && (!storageOrder.HasValue || storageOrder == StorageOrders.Other)), "This internal method should only handle copy operations when the faster shortcut is not possible (continous storage and keep layout).");
            // this is non-continuous storage or we need to reorder dimensions

            uint ndims = (uint)inSize.NumberOfDimensions;
            long outLen = inSize.NumberOfElements;

            // empty arrays 
            if (outLen == 0) {
                return;
            }
            System.Diagnostics.Debug.Assert(!Equals(inSize, null) && inSize.NumberOfElements > 0, "Empty arrays must be handled separately.");

            //// straight forward loop over cur / iter_str 
            complex* pIn = (complex*)src + inSize.BaseOffset;  // init with base offset
            complex* pOut = (complex*)dest; // any potential BaseOffset in outSize is ignored here! ... safes a bunch of null-checks

            if (outLen == 1) {
                pOut[0] = pIn[0];
                return;
            }

#if TEST_1000ASUINTMAXVALUE
            if (inSize.GetElementSpan() > 1000) {
#else
            if (Environment.Is64BitProcess && inSize.GetElementSpan() * (uint)sizeof(uint) > uint.MaxValue) {
#endif
                // strides are long* 
                long* ordered_bsd = stackalloc long[(int)ndims * 2 + 3];
                Helper.PrepareBSD4CopyTo(storageOrder.GetValueOrDefault(), inSize.GetBSD(false),
                                  ordered_bsd, outSize, (uint)sizeof(complex));
                // ordered_strides has SCALED strides! -> pIn is now byte*!
                // pIn, pOut -> include base offset! 
                Complex.Strided64(
                    (byte*)pIn, pOut,
                    0, outLen,
                    ordered_bsd);

            } else {
                // strides are uint* 
                uint* ordered_bsd = stackalloc uint[(int)ndims * 2 + 3];
                Helper.PrepareBSD4CopyTo(storageOrder.GetValueOrDefault(), inSize.GetBSD(false),
                                  ordered_bsd, outSize, (uint)sizeof(complex));
                // ordered_strides has SCALED strides! -> pIn is now byte*!

                // pIn, pOut -> include base offset! 
                Complex.Strided32(
                    (byte*)pIn, pOut,
                    0, (uint)outLen,
                    ordered_bsd);

            }
        }

        internal static class Complex {

            // pSrc is b_y_t_e* - always!! increments are faster by scaled strides!  
            internal static unsafe void Strided32(byte* pSrc, complex* pDest, uint start, uint len, uint* bsd) {
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


                complex* pOut = pDest + start;

                while (true) {

                    byte* pIn = pSrc + higdims + cur[0] * stride0;

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    uint leadLen = Math.Min(start + len - (uint)(pOut - pDest), dims[0] - cur[0]);

                    while (leadLen > 8) {
                        pOut[0] = (*(complex*)pIn);
                        pOut[1] = (*(complex*)(pIn + stride0));
                        pOut[2] = (*(complex*)(pIn + 2 * stride0));
                        pOut[3] = (*(complex*)(pIn + 3 * stride0));
                        pOut[4] = (*(complex*)(pIn + 4 * stride0));
                        pOut[5] = (*(complex*)(pIn + 5 * stride0));
                        pOut[6] = (*(complex*)(pIn + 6 * stride0));
                        pOut[7] = (*(complex*)(pIn + 7 * stride0));
                        pOut += 8; pIn += 8 * stride0; leadLen -= 8;
                    }
                    while (leadLen-- > 0) {
                        *pOut++ = (*(complex*)pIn);
                        pIn += stride0;
                    }
                    if (pOut == pDest + start + len) {
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
            // pSrc is b_y_t_e* - always!! increments are faster by scaled strides!  
            internal static unsafe void Strided64(byte* pSrc, complex* pDest, long start, long len, long* bsd) {

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


                complex* pOut = pDest + start;

                while (true) {

                    byte* pIn = pSrc + higdims + cur[0] * stride0;

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(start + len - (long)(pOut - pDest), dims[0] - cur[0]);

                    while (leadLen > 8) {
                        pOut[0] = (*(complex*)pIn);
                        pOut[1] = (*(complex*)(pIn + stride0));
                        pOut[2] = (*(complex*)(pIn + 2 * stride0));
                        pOut[3] = (*(complex*)(pIn + 3 * stride0));
                        pOut[4] = (*(complex*)(pIn + 4 * stride0));
                        pOut[5] = (*(complex*)(pIn + 5 * stride0));
                        pOut[6] = (*(complex*)(pIn + 6 * stride0));
                        pOut[7] = (*(complex*)(pIn + 7 * stride0));
                        pOut += 8; pIn += 8 * stride0; leadLen -= 8;
                    }
                    while (leadLen-- > 0) {
                        *pOut++ = (*(complex*)pIn);
                        pIn += stride0;
                    }
                    if (pOut == pDest + start + len) {
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

       
        
        private static unsafe void CopyTo_8(IntPtr src, Size inSize, IntPtr dest, Size outSize, StorageOrders? storageOrder) {

            // no checks for src- / dest integrity!!! Must all be done in caller

            System.Diagnostics.Debug.Assert(!(inSize.IsContinuous && (!storageOrder.HasValue || storageOrder == StorageOrders.Other)), "This internal method should only handle copy operations when the faster shortcut is not possible (continous storage and keep layout).");
            // this is non-continuous storage or we need to reorder dimensions

            uint ndims = (uint)inSize.NumberOfDimensions;
            long outLen = inSize.NumberOfElements;

            // empty arrays 
            if (outLen == 0) {
                return;
            }
            System.Diagnostics.Debug.Assert(!Equals(inSize, null) && inSize.NumberOfElements > 0, "Empty arrays must be handled separately.");

            //// straight forward loop over cur / iter_str 
            ulong* pIn = (ulong*)src + inSize.BaseOffset;  // init with base offset
            ulong* pOut = (ulong*)dest; // any potential BaseOffset in outSize is ignored here! ... safes a bunch of null-checks

            if (outLen == 1) {
                pOut[0] = pIn[0];
                return;
            }

#if TEST_1000ASUINTMAXVALUE
            if (inSize.GetElementSpan() > 1000) {
#else
            if (Environment.Is64BitProcess && inSize.GetElementSpan() * (uint)sizeof(uint) > uint.MaxValue) {
#endif
                // strides are long* 
                long* ordered_bsd = stackalloc long[(int)ndims * 2 + 3];
                Helper.PrepareBSD4CopyTo(storageOrder.GetValueOrDefault(), inSize.GetBSD(false),
                                  ordered_bsd, outSize, (uint)sizeof(ulong));
                // ordered_strides has SCALED strides! -> pIn is now byte*!
                // pIn, pOut -> include base offset! 
                UInt64.Strided64(
                    (byte*)pIn, pOut,
                    0, outLen,
                    ordered_bsd);

            } else {
                // strides are uint* 
                uint* ordered_bsd = stackalloc uint[(int)ndims * 2 + 3];
                Helper.PrepareBSD4CopyTo(storageOrder.GetValueOrDefault(), inSize.GetBSD(false),
                                  ordered_bsd, outSize, (uint)sizeof(ulong));
                // ordered_strides has SCALED strides! -> pIn is now byte*!

                // pIn, pOut -> include base offset! 
                UInt64.Strided32(
                    (byte*)pIn, pOut,
                    0, (uint)outLen,
                    ordered_bsd);

            }
        }

        internal static class UInt64 {

            // pSrc is b_y_t_e* - always!! increments are faster by scaled strides!  
            internal static unsafe void Strided32(byte* pSrc, ulong* pDest, uint start, uint len, uint* bsd) {
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


                ulong* pOut = pDest + start;

                while (true) {

                    byte* pIn = pSrc + higdims + cur[0] * stride0;

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    uint leadLen = Math.Min(start + len - (uint)(pOut - pDest), dims[0] - cur[0]);

                    while (leadLen > 8) {
                        pOut[0] = (*(ulong*)pIn);
                        pOut[1] = (*(ulong*)(pIn + stride0));
                        pOut[2] = (*(ulong*)(pIn + 2 * stride0));
                        pOut[3] = (*(ulong*)(pIn + 3 * stride0));
                        pOut[4] = (*(ulong*)(pIn + 4 * stride0));
                        pOut[5] = (*(ulong*)(pIn + 5 * stride0));
                        pOut[6] = (*(ulong*)(pIn + 6 * stride0));
                        pOut[7] = (*(ulong*)(pIn + 7 * stride0));
                        pOut += 8; pIn += 8 * stride0; leadLen -= 8;
                    }
                    while (leadLen-- > 0) {
                        *pOut++ = (*(ulong*)pIn);
                        pIn += stride0;
                    }
                    if (pOut == pDest + start + len) {
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
            // pSrc is b_y_t_e* - always!! increments are faster by scaled strides!  
            internal static unsafe void Strided64(byte* pSrc, ulong* pDest, long start, long len, long* bsd) {

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


                ulong* pOut = pDest + start;

                while (true) {

                    byte* pIn = pSrc + higdims + cur[0] * stride0;

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(start + len - (long)(pOut - pDest), dims[0] - cur[0]);

                    while (leadLen > 8) {
                        pOut[0] = (*(ulong*)pIn);
                        pOut[1] = (*(ulong*)(pIn + stride0));
                        pOut[2] = (*(ulong*)(pIn + 2 * stride0));
                        pOut[3] = (*(ulong*)(pIn + 3 * stride0));
                        pOut[4] = (*(ulong*)(pIn + 4 * stride0));
                        pOut[5] = (*(ulong*)(pIn + 5 * stride0));
                        pOut[6] = (*(ulong*)(pIn + 6 * stride0));
                        pOut[7] = (*(ulong*)(pIn + 7 * stride0));
                        pOut += 8; pIn += 8 * stride0; leadLen -= 8;
                    }
                    while (leadLen-- > 0) {
                        *pOut++ = (*(ulong*)pIn);
                        pIn += stride0;
                    }
                    if (pOut == pDest + start + len) {
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

       
        
        private static unsafe void CopyTo_4(IntPtr src, Size inSize, IntPtr dest, Size outSize, StorageOrders? storageOrder) {

            // no checks for src- / dest integrity!!! Must all be done in caller

            System.Diagnostics.Debug.Assert(!(inSize.IsContinuous && (!storageOrder.HasValue || storageOrder == StorageOrders.Other)), "This internal method should only handle copy operations when the faster shortcut is not possible (continous storage and keep layout).");
            // this is non-continuous storage or we need to reorder dimensions

            uint ndims = (uint)inSize.NumberOfDimensions;
            long outLen = inSize.NumberOfElements;

            // empty arrays 
            if (outLen == 0) {
                return;
            }
            System.Diagnostics.Debug.Assert(!Equals(inSize, null) && inSize.NumberOfElements > 0, "Empty arrays must be handled separately.");

            //// straight forward loop over cur / iter_str 
            uint* pIn = (uint*)src + inSize.BaseOffset;  // init with base offset
            uint* pOut = (uint*)dest; // any potential BaseOffset in outSize is ignored here! ... safes a bunch of null-checks

            if (outLen == 1) {
                pOut[0] = pIn[0];
                return;
            }

#if TEST_1000ASUINTMAXVALUE
            if (inSize.GetElementSpan() > 1000) {
#else
            if (Environment.Is64BitProcess && inSize.GetElementSpan() * (uint)sizeof(uint) > uint.MaxValue) {
#endif
                // strides are long* 
                long* ordered_bsd = stackalloc long[(int)ndims * 2 + 3];
                Helper.PrepareBSD4CopyTo(storageOrder.GetValueOrDefault(), inSize.GetBSD(false),
                                  ordered_bsd, outSize, (uint)sizeof(uint));
                // ordered_strides has SCALED strides! -> pIn is now byte*!
                // pIn, pOut -> include base offset! 
                UInt32.Strided64(
                    (byte*)pIn, pOut,
                    0, outLen,
                    ordered_bsd);

            } else {
                // strides are uint* 
                uint* ordered_bsd = stackalloc uint[(int)ndims * 2 + 3];
                Helper.PrepareBSD4CopyTo(storageOrder.GetValueOrDefault(), inSize.GetBSD(false),
                                  ordered_bsd, outSize, (uint)sizeof(uint));
                // ordered_strides has SCALED strides! -> pIn is now byte*!

                // pIn, pOut -> include base offset! 
                UInt32.Strided32(
                    (byte*)pIn, pOut,
                    0, (uint)outLen,
                    ordered_bsd);

            }
        }

        internal static class UInt32 {

            // pSrc is b_y_t_e* - always!! increments are faster by scaled strides!  
            internal static unsafe void Strided32(byte* pSrc, uint* pDest, uint start, uint len, uint* bsd) {
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


                uint* pOut = pDest + start;

                while (true) {

                    byte* pIn = pSrc + higdims + cur[0] * stride0;

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    uint leadLen = Math.Min(start + len - (uint)(pOut - pDest), dims[0] - cur[0]);

                    while (leadLen > 8) {
                        pOut[0] = (*(uint*)pIn);
                        pOut[1] = (*(uint*)(pIn + stride0));
                        pOut[2] = (*(uint*)(pIn + 2 * stride0));
                        pOut[3] = (*(uint*)(pIn + 3 * stride0));
                        pOut[4] = (*(uint*)(pIn + 4 * stride0));
                        pOut[5] = (*(uint*)(pIn + 5 * stride0));
                        pOut[6] = (*(uint*)(pIn + 6 * stride0));
                        pOut[7] = (*(uint*)(pIn + 7 * stride0));
                        pOut += 8; pIn += 8 * stride0; leadLen -= 8;
                    }
                    while (leadLen-- > 0) {
                        *pOut++ = (*(uint*)pIn);
                        pIn += stride0;
                    }
                    if (pOut == pDest + start + len) {
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
            // pSrc is b_y_t_e* - always!! increments are faster by scaled strides!  
            internal static unsafe void Strided64(byte* pSrc, uint* pDest, long start, long len, long* bsd) {

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


                uint* pOut = pDest + start;

                while (true) {

                    byte* pIn = pSrc + higdims + cur[0] * stride0;

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(start + len - (long)(pOut - pDest), dims[0] - cur[0]);

                    while (leadLen > 8) {
                        pOut[0] = (*(uint*)pIn);
                        pOut[1] = (*(uint*)(pIn + stride0));
                        pOut[2] = (*(uint*)(pIn + 2 * stride0));
                        pOut[3] = (*(uint*)(pIn + 3 * stride0));
                        pOut[4] = (*(uint*)(pIn + 4 * stride0));
                        pOut[5] = (*(uint*)(pIn + 5 * stride0));
                        pOut[6] = (*(uint*)(pIn + 6 * stride0));
                        pOut[7] = (*(uint*)(pIn + 7 * stride0));
                        pOut += 8; pIn += 8 * stride0; leadLen -= 8;
                    }
                    while (leadLen-- > 0) {
                        *pOut++ = (*(uint*)pIn);
                        pIn += stride0;
                    }
                    if (pOut == pDest + start + len) {
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

       
        
        private static unsafe void CopyTo_1(IntPtr src, Size inSize, IntPtr dest, Size outSize, StorageOrders? storageOrder) {

            // no checks for src- / dest integrity!!! Must all be done in caller

            System.Diagnostics.Debug.Assert(!(inSize.IsContinuous && (!storageOrder.HasValue || storageOrder == StorageOrders.Other)), "This internal method should only handle copy operations when the faster shortcut is not possible (continous storage and keep layout).");
            // this is non-continuous storage or we need to reorder dimensions

            uint ndims = (uint)inSize.NumberOfDimensions;
            long outLen = inSize.NumberOfElements;

            // empty arrays 
            if (outLen == 0) {
                return;
            }
            System.Diagnostics.Debug.Assert(!Equals(inSize, null) && inSize.NumberOfElements > 0, "Empty arrays must be handled separately.");

            //// straight forward loop over cur / iter_str 
            byte* pIn = (byte*)src + inSize.BaseOffset;  // init with base offset
            byte* pOut = (byte*)dest; // any potential BaseOffset in outSize is ignored here! ... safes a bunch of null-checks

            if (outLen == 1) {
                pOut[0] = pIn[0];
                return;
            }

#if TEST_1000ASUINTMAXVALUE
            if (inSize.GetElementSpan() > 1000) {
#else
            if (Environment.Is64BitProcess && inSize.GetElementSpan() * (uint)sizeof(uint) > uint.MaxValue) {
#endif
                // strides are long* 
                long* ordered_bsd = stackalloc long[(int)ndims * 2 + 3];
                Helper.PrepareBSD4CopyTo(storageOrder.GetValueOrDefault(), inSize.GetBSD(false),
                                  ordered_bsd, outSize, (uint)sizeof(byte));
                // ordered_strides has SCALED strides! -> pIn is now byte*!
                // pIn, pOut -> include base offset! 
                Byte.Strided64(
                    (byte*)pIn, pOut,
                    0, outLen,
                    ordered_bsd);

            } else {
                // strides are uint* 
                uint* ordered_bsd = stackalloc uint[(int)ndims * 2 + 3];
                Helper.PrepareBSD4CopyTo(storageOrder.GetValueOrDefault(), inSize.GetBSD(false),
                                  ordered_bsd, outSize, (uint)sizeof(byte));
                // ordered_strides has SCALED strides! -> pIn is now byte*!

                // pIn, pOut -> include base offset! 
                Byte.Strided32(
                    (byte*)pIn, pOut,
                    0, (uint)outLen,
                    ordered_bsd);

            }
        }

        internal static class Byte {

            // pSrc is b_y_t_e* - always!! increments are faster by scaled strides!  
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
                    uint leadLen = Math.Min(start + len - (uint)(pOut - pDest), dims[0] - cur[0]);

                    while (leadLen > 8) {
                        pOut[0] = (*(byte*)pIn);
                        pOut[1] = (*(byte*)(pIn + stride0));
                        pOut[2] = (*(byte*)(pIn + 2 * stride0));
                        pOut[3] = (*(byte*)(pIn + 3 * stride0));
                        pOut[4] = (*(byte*)(pIn + 4 * stride0));
                        pOut[5] = (*(byte*)(pIn + 5 * stride0));
                        pOut[6] = (*(byte*)(pIn + 6 * stride0));
                        pOut[7] = (*(byte*)(pIn + 7 * stride0));
                        pOut += 8; pIn += 8 * stride0; leadLen -= 8;
                    }
                    while (leadLen-- > 0) {
                        *pOut++ = (*(byte*)pIn);
                        pIn += stride0;
                    }
                    if (pOut == pDest + start + len) {
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
            // pSrc is b_y_t_e* - always!! increments are faster by scaled strides!  
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
                    long leadLen = Math.Min(start + len - (long)(pOut - pDest), dims[0] - cur[0]);

                    while (leadLen > 8) {
                        pOut[0] = (*(byte*)pIn);
                        pOut[1] = (*(byte*)(pIn + stride0));
                        pOut[2] = (*(byte*)(pIn + 2 * stride0));
                        pOut[3] = (*(byte*)(pIn + 3 * stride0));
                        pOut[4] = (*(byte*)(pIn + 4 * stride0));
                        pOut[5] = (*(byte*)(pIn + 5 * stride0));
                        pOut[6] = (*(byte*)(pIn + 6 * stride0));
                        pOut[7] = (*(byte*)(pIn + 7 * stride0));
                        pOut += 8; pIn += 8 * stride0; leadLen -= 8;
                    }
                    while (leadLen-- > 0) {
                        *pOut++ = (*(byte*)pIn);
                        pIn += stride0;
                    }
                    if (pOut == pDest + start + len) {
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

#endregion HYCALPER AUTO GENERATED CODE

        internal static unsafe void CopyTo<T>(T[] src, Size inSize, T[] dest, Size outSize, StorageOrders? layout) {

            System.Diagnostics.Debug.Assert(src != null && dest != null);
            System.Diagnostics.Debug.Assert(inSize.NumberOfElements <= dest.Length);
            System.Diagnostics.Debug.Assert(inSize.NumberOfElements <= src.Length);
            System.Diagnostics.Debug.Assert(layout == StorageOrders.ColumnMajor || layout == StorageOrders.RowMajor);

            long pos = 0; 
            foreach(long ind in inSize.Iterator(order: layout ?? Settings.DefaultStorageOrder)) { 
                dest[pos++] = src[ind]; 
            }
            if (!Equals(outSize, null)) {
                var bsd = outSize.GetBSD(write: true);
                uint ndims = Math.Max(inSize.NumberOfDimensions, Settings.MinNumberOfArrayDimensions);
                long stride = 1;
                bsd[0] = (ndims);
                bsd[1] = (inSize.NumberOfElements);
                bsd[2] = 0;
                for (uint i = 0; i < ndims; i++) {
                    if (layout == StorageOrders.RowMajor) {
                        var s = inSize[ndims - 1 - i]; 
                        bsd[3 + i] = (s);
                        bsd[3 + i + ndims] = (s == 1 ? 0 : stride);
                        stride *= s; 
                    } else {
                        // assuming ColumnMajor (but this is not safe at this point...!)
                        var s = inSize[i];
                        bsd[3 + i] = (s);
                        bsd[3 + i + ndims] = (s == 1 ? 0 : stride);
                        stride *= s;
                    }
                }
            }

        }
        internal static unsafe void CopyTo_Cell(IStorage[] src, Size inSize, IStorage[] dest, Size outSize, StorageOrders? layout) {

            System.Diagnostics.Debug.Assert(src != null && dest != null);
            System.Diagnostics.Debug.Assert(inSize.NumberOfElements <= dest.Length);
            System.Diagnostics.Debug.Assert(inSize.NumberOfElements <= src.Length);
            System.Diagnostics.Debug.Assert(layout == StorageOrders.ColumnMajor || layout == StorageOrders.RowMajor);

            long pos = 0; 
            foreach(long ind in inSize.Iterator(order: layout ?? Settings.DefaultStorageOrder)) {

                dest[pos]?.Release();
                dest[pos] = src[ind];
                dest[pos]?.Retain();
                pos++;

            }
            if (!Equals(outSize, null)) {
                var bsd = outSize.GetBSD(write: true);
                uint ndims = Math.Max(inSize.NumberOfDimensions, Settings.MinNumberOfArrayDimensions);
                long stride = 1;
                bsd[0] = (ndims);
                bsd[1] = (inSize.NumberOfElements);
                bsd[2] = 0;
                for (uint i = 0; i < ndims; i++) {
                    if (layout == StorageOrders.RowMajor) {
                        var s = inSize[ndims - 1 - i]; 
                        bsd[3 + i] = (s);
                        bsd[3 + i + ndims] = (s == 1 ? 0 : stride);
                        stride *= s; 
                    } else {
                        // assuming ColumnMajor (but this is not safe at this point...!)
                        var s = inSize[i];
                        bsd[3 + i] = (s);
                        bsd[3 + i + ndims] = (s == 1 ? 0 : stride);
                        stride *= s;
                    }
                }
            }

        }
    }
}