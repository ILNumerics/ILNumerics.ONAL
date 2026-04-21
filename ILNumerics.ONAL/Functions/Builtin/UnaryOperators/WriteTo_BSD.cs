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
using ILNumerics.Core.Segments;
using ILNumerics.Core.StorageLayer;
using System;
using System.Diagnostics;
using System.Security;
using System.Threading;

namespace ILNumerics.Core.Functions.Builtin {

    internal static partial class WriteToOperators {

        /// <summary>
        /// Write content of src as addressed by inSize to the subarray of dest as addressed by outSize. No reshape (Matlab). Supports broadcasting.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="inSize"></param>
        /// <param name="dest"></param>
        /// <param name="outBSD">must be broadcastable from inSize! this is checked in SetRange_ML</param>
        /// <param name="inExpand">If this is called from Expand disables cells copy &amp; clone mechanism.</param>
        /// <param name="sizeofT">Element size in bytes.</param>
        
        internal static unsafe void WriteTo_BSD<T>(MemoryHandle src, Size inSize, MemoryHandle dest, long* outBSD, uint sizeofT, bool inExpand = false) {
            // special cases: empty
            if (inSize.NumberOfElements == 0) {
                return;
            } else if (src is NativeHostHandle) {

                switch (sizeofT) {
                    case 1:
                        WriteTo_BSD_ML_1(src.Pointer, inSize, dest.Pointer, outBSD);
                        break;
                    case 2:
                        WriteTo_BSD_ML_2(src.Pointer, inSize, dest.Pointer, outBSD);
                        break;
                    case 4:
                        WriteTo_BSD_ML_4(src.Pointer, inSize, dest.Pointer, outBSD);
                        break;
                    case 8:
                        //try { 
                        WriteTo_BSD_ML_8(src.Pointer, inSize, dest.Pointer, outBSD);
                        //} catch (Exception exc) {
                        //    Debugger.Launch();
                        //    Debugger.Break(); 
                        //}
                        break; 
                    case 16:
                        WriteTo_BSD_ML_16(src.Pointer, inSize, dest.Pointer, outBSD);
                        break;
                    default:
                        WriteTo_BSD_Gen(src as NativeHostHandle, inSize, dest as NativeHostHandle, outBSD, sizeofT);
                        break;
                        //throw new InvalidOperationException("This operation is not supported on the element datatype. Supported data types have lengths of 1, 2, 4, 8 or 16 bytes.");
                }

            } else if (src is ManagedHostHandle<T>) {
                WriteTo_BSD_Ref<T>(src as ManagedHostHandle<T>, inSize, dest as ManagedHostHandle<T>, outBSD);
                return;
            } else if (src is ManagedHostHandle<IStorage>) {
                if (inExpand) {
                    // Shallow copy plain references to new HostArray to be used as new storage handle later on. 
                    // Instead of copying / creating clones we just copy the reference and clear the source afterwards. (!!) 
                    // Make sure not to leave the source references around! Dont put them into the pool either! 
                    WriteTo_BSD_Ref<IStorage>(src as ManagedHostHandle<IStorage>, inSize, dest as ManagedHostHandle<IStorage>, outBSD);
                    src.Clear(); 
                } else {
                    WriteTo_BSD_Cell(src as ManagedHostHandle<IStorage>, inSize, dest as ManagedHostHandle<IStorage>, outBSD);
                }
                return;
            }
        }

        /// <summary>
        /// Broadcasts elements from <paramref name="src"/> and <paramref name="inSize"/> and writes them to corresponding elements of <paramref name="dest"/> as addressed by <paramref name="outBSD"/>.
        /// </summary>
        /// <param name="src">Source memory handle.</param>
        /// <param name="inSize">Source array size.</param>
        /// <param name="dest">Destination memory handle.</param>
        /// <param name="outBSD">Destination BSD describing the range to be overwritten.</param>
        /// <param name="sizeOfT"></param>
        /// <remarks><paramref name="inSize"/> is broadcasted to <paramref name="outBSD"/> - not the other way around (unidirectional broadcasting).
        /// This function checks for argument sizes / shapes. It is provided for completeness and reference element types, since value (struct) element 
        /// types are handled by other overloads of this class more efficiently. However, this is not a technical restriction! Note, whatsoever, 
        /// that this function is not parallelized.</remarks>
        /// <exception cref="ArgumentException">if the input is not broadcastable to the destination shape.</exception>

        private static unsafe void WriteTo_BSD_Gen(NativeHostHandle src, Size inSize, NativeHostHandle dest, long* outBSD, uint sizeOfT) {

            if (Settings.ArrayStyle == ArrayStyles.numpy) {
                inSize.CheckIsBroadcastableTo_np(outBSD + 3, (uint)outBSD[0]);
            } else {
                // Matlab mode: should have been checked in SetRange_ML already, no? 
                System.Diagnostics.Debug.Assert(inSize.IsBroadcastableTo(outBSD));
            }

            byte* srcArr = (byte*)src.Pointer;
            byte* destArr = (byte*)dest.Pointer;
            System.Diagnostics.Debug.Assert(srcArr != (byte*)0 && destArr != (byte*)0);

            uint ndims = (uint)outBSD[0];
            if ((long)outBSD[1] == 1) {
                // includes numpy scalar: ndims = 0
                destArr = (byte*)dest.Pointer + (long)outBSD[2] * sizeOfT;
                srcArr = (byte*)src.Pointer + inSize.BaseOffset * sizeOfT;
                for (int i = 0; i < sizeOfT; i++) {
                    destArr[i] = srcArr[i]; 
                }
                return;
            }
            long strideOut0 = (long)outBSD[3 + ndims];
            long strideIn0 = inSize.GetStride(0);
            long* cur = stackalloc long[(int)ndims];
            for (int i = 0; i < ndims; i++) {
                cur[0] = 0;
            }
            System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

            long higdimsOut = (long)outBSD[2];
            long higdimsIn = inSize.BaseOffset;
            long leadLen = (long)outBSD[3];

            while (true) {

                var cur0 = 0;
                while (cur0 < leadLen) {

                    //destArr[higdimsOut + cur0 * strideOut0] = srcArr[higdimsIn + cur0 * strideIn0];
                    destArr = (byte*)dest.Pointer + (higdimsOut + cur0 * strideOut0) * sizeOfT;
                    srcArr = (byte*)src.Pointer + (higdimsIn + cur0 * strideIn0) * sizeOfT;
                    for (int i = 0; i < sizeOfT; i++) {
                        destArr[i] = srcArr[i];
                    }
                    cur0++;
                }

                // increase higher dims
                uint d = 1;
                while (d < ndims) {
                    if (cur[d] < (long)outBSD[3 + d] - 1) {
                        higdimsIn += inSize.GetStride(d);
                        higdimsOut += (long)outBSD[3 + d + ndims];
                        cur[d]++;
                        break;
                    } else {
                        higdimsIn -= inSize.GetStride(d) * cur[d];
                        higdimsOut -= (long)outBSD[3 + d + ndims] * cur[d];
                        cur[d] = 0;
                        d++;
                    }
                }
                if (d == ndims) return;
            }
        }
        /// <summary>
        /// Broadcasts elements from <paramref name="src"/> and <paramref name="inSize"/> and writes them to corresponding elements of <paramref name="dest"/> as addressed by <paramref name="outBSD"/>.
        /// </summary>
        /// <typeparam name="T">Element type. This is expected to be a reference type.</typeparam>
        /// <param name="src">Source memory handle.</param>
        /// <param name="inSize">Source array size.</param>
        /// <param name="dest">Destination memory handle.</param>
        /// <param name="outBSD">Destination BSD describing the range to be overwritten.</param>
        /// <remarks><paramref name="inSize"/> is broadcasted to <paramref name="outBSD"/> - not the other way around (unidirectional broadcasting).
        /// This function checks for argument sizes / shapes. It is provided for completeness and reference element types, since value (struct) element 
        /// types are handled by other overloads of this class more efficiently. However, this is not a technical restriction! Note, whatsoever, 
        /// that this function is not parallelized.</remarks>
        /// <exception cref="ArgumentException">if the input is not broadcastable to the destination shape.</exception>
        
        private static unsafe void WriteTo_BSD_Ref<T>(ManagedHostHandle<T> src, Size inSize, ManagedHostHandle<T> dest, long* outBSD) {

            if (Settings.ArrayStyle == ArrayStyles.numpy) {
                inSize.CheckIsBroadcastableTo_np(outBSD + 3, (uint)outBSD[0]);
            } else {
                // Matlab mode: should have been checked in SetRange_ML already, no? 
                System.Diagnostics.Debug.Assert(inSize.IsBroadcastableTo(outBSD));
            }

            var srcArr = src.HostArray;
            var destArr = dest.HostArray;
            System.Diagnostics.Debug.Assert(srcArr != null && destArr != null);

            uint ndims = (uint)outBSD[0];
            if ((long)outBSD[1] == 1) {
                // includes numpy scalar: ndims = 0
                destArr[(long)outBSD[2]] = srcArr[inSize.BaseOffset];
                return;
            }
            long strideOut0 = (long)outBSD[3 + ndims];
            long strideIn0 = inSize.GetStride(0);
            long* cur = stackalloc long[(int)ndims];
            for (int i = 0; i < ndims; i++) {
                cur[0] = 0;
            }
            System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

            long higdimsOut = (long)outBSD[2];
            long higdimsIn = inSize.BaseOffset;
            long leadLen = (long)outBSD[3];

            while (true) {

                var cur0 = 0;
                while (cur0 < leadLen) {
                    destArr[higdimsOut + cur0 * strideOut0] = srcArr[higdimsIn + cur0 * strideIn0];
                    cur0++;
                }

                // increase higher dims
                uint d = 1;
                while (d < ndims) {
                    if (cur[d] < (long)outBSD[3 + d] - 1) {
                        higdimsIn += inSize.GetStride(d);
                        higdimsOut += (long)outBSD[3 + d + ndims];
                        cur[d]++;
                        break;
                    } else {
                        higdimsIn -= inSize.GetStride(d) * cur[d];
                        higdimsOut -= (long)outBSD[3 + d + ndims] * cur[d];
                        cur[d] = 0;
                        d++;
                    }
                }
                if (d == ndims) return;
            }
        }

        /// <summary>
        /// Broadcasts elements from <paramref name="src"/> and <paramref name="inSize"/> and writes them to corresponding elements of <paramref name="dest"/> as addressed by <paramref name="outBSD"/>.
        /// </summary>
        /// <param name="src">Source memory handle.</param>
        /// <param name="inSize">Source array size.</param>
        /// <param name="dest">Destination memory handle.</param>
        /// <param name="outBSD">Destination BSD describing the range to be overwritten.</param>
        /// <remarks><paramref name="inSize"/> is broadcasted to <paramref name="outBSD"/> - not the other way around (unidirectional broadcasting).
        /// This function checks for argument sizes / shapes. It is provided for completeness and reference element types, since value (struct) element 
        /// types are handled by other overloads of this class more efficiently. However, this is not a technical restriction! Note, whatsoever, 
        /// that this function is not parallelized.</remarks>
        /// <exception cref="ArgumentException">if the input is not broadcastable to the destination shape.</exception>
        
        private static unsafe void WriteTo_BSD_Cell(ManagedHostHandle<IStorage> src, Size inSize, ManagedHostHandle<IStorage> dest, long* outBSD) {

            if (Settings.ArrayStyle == ArrayStyles.numpy) {
                inSize.CheckIsBroadcastableTo_np(outBSD + 3, (uint)outBSD[0]);
            } else {
                // Matlab mode: should have been checked in SetRange_ML already, no? 
                System.Diagnostics.Debug.Assert(inSize.IsBroadcastableTo(outBSD)); 
            }

            var srcArr = src.HostArray;
            var destArr = dest.HostArray;
            System.Diagnostics.Debug.Assert(srcArr != null && destArr != null);

            uint ndims = (uint)outBSD[0];
            if ((long)outBSD[1] == 1) {
                // includes numpy scalar: ndims = 0
                destArr[(long)outBSD[2]]?.Release();
                var clone = srcArr[inSize.BaseOffset]?.Clone();
                destArr[(long)outBSD[2]] = clone;
                return;
            }
            long strideOut0 = (long)outBSD[3 + ndims];
            long strideIn0 = inSize.GetStride(0);
            long* cur = stackalloc long[(int)ndims];
            for (int i = 0; i < ndims; i++) {
                cur[0] = 0;
            }
            System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

            long higdimsOut = (long)outBSD[2];
            long higdimsIn = inSize.BaseOffset;
            long leadLen = (long)outBSD[3];

            while (true) {

                var cur0 = 0;
                while (cur0 < leadLen) {
                    destArr[higdimsOut + cur0 * strideOut0]?.Release();
                    var clone = srcArr[higdimsIn + cur0 * strideIn0]?.Clone();
                    destArr[higdimsOut + cur0 * strideOut0] = clone;
                    cur0++;
                }

                // increase higher dims
                uint d = 1;
                while (d < ndims) {
                    if (cur[d] < (long)outBSD[3 + d] - 1) {
                        higdimsIn += inSize.GetStride(d);
                        higdimsOut += (long)outBSD[3 + d + ndims];
                        cur[d]++;
                        break;
                    } else {
                        higdimsIn -= inSize.GetStride(d) * cur[d];
                        higdimsOut -= (long)outBSD[3 + d + ndims] * cur[d];
                        cur[d] = 0;
                        d++;
                    }
                }
                if (d == ndims) return;
            }
        }

        #region HYCALPER LOOPSTART

        /*!HC:TYPELIST:
        <hycalper>
        <type>
            <source locate="here">
                WriteTo_BSD_ML_2
            </source>
            <destination>WriteTo_BSD_ML_1</destination>
            <destination>WriteTo_BSD_ML_4</destination>
            <destination>WriteTo_BSD_ML_8</destination>
            <destination>WriteTo_BSD_ML_16</destination>
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


        /// <summary>
        /// Write content of src as addressed by inSize to the subarray of dest as addressed by outSize. No reshape (Matlab). Supports broadcasting on the right side only.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="inSize"></param>
        /// <param name="dest"></param>
        /// <param name="outBSD"></param>
        private static unsafe void WriteTo_BSD_ML_2(IntPtr src, Size inSize, IntPtr dest, long* outBSD) {

            // no broadcasting / shape checks have been done until here! 
            if (outBSD[1] == 1) {
                // disregard nr of dimensions, includes numpy scalars 
                if (inSize.NumberOfElements != 1) {
                    // let's utilize the same broadcasting helper so we get the same error message
                    inSize.CheckIsBroadcastableTo_np(outBSD + 3, (uint)outBSD[0]); 
                }
                ((ushort*)dest)[outBSD[2]] = ((ushort*)src)[inSize.BaseOffset];
                return; 
            }

            uint ndims = (uint)outBSD[0];
            long outLen = outBSD[1];
        
            ushort* pIn = (ushort*)src + inSize.BaseOffset;  // init with base offset
            ushort* pOut = (ushort*)dest + (ulong)outBSD[2];

            // strides are long* 
            long* ordered_bsd = stackalloc long[(int)ndims * 3 + 3];
            // ordered_bsd stores the dest BSD (original), dims + strides (corrected) for src
            Helper.PrepareBSD4WriteTo(inSize, outBSD, (uint)sizeof(ushort), ordered_bsd);
            // empty arrays - "early" exit after error checks 
            if (outLen == 0) {
                return;
            }
            System.Diagnostics.Debug.Assert(!Equals(inSize, null) && inSize.NumberOfElements > 0, "Empty arrays must be handled separately.");

            // ordered_strides has SCALED strides! -> pIn and pOut are now byte*!
            // pIn, pOut -> include base offset! 

            UInt16.Strided64(
                (byte*)pIn, (byte*)pOut,
                0, outLen,
                ordered_bsd);
        }

        internal static class UInt16 {

            // pSrc is b_y_t_e* - always!! increments are faster by scaled strides!  
            internal static unsafe void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsd) {
                // bsd: outBSD + inStrides. All scaled + in ready 4 broadcasting (singletons -> 0-strides)
                uint ndims = (uint)bsd[0];
                long* dims = bsd + 3;
                long* stridesOut = dims + ndims;
                long* stridesIn = stridesOut + ndims;
                long strideOut0 = stridesOut[0];
                long strideIn0 = stridesIn[0];
                long* cur = stackalloc long[(int)ndims];

                // figure out the dimension index position for start
                System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

                cur[0] = start % dims[0];
                long f = start / dims[0];
                long higdimsOut = 0;
                long higdimsIn = 0;
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % dims[i];
                    f /= dims[i];
                    higdimsOut += cur[i] * stridesOut[i];
                    higdimsIn += cur[i] * stridesIn[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);

                while (true) {

                    byte* pIn = pSrc + higdimsIn + cur[0] * strideIn0;
                    byte* pOut = pDest + higdimsOut + cur[0] * strideOut0;

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] - cur[0]);
                    len -= leadLen; 

                    while (leadLen > 8) {
                        *(ushort*)(pOut) = (*(ushort*)pIn);
                        *(ushort*)(pOut + strideOut0) = *(ushort*)(pIn + strideIn0);
                        *(ushort*)(pOut + 2 * strideOut0) = *(ushort*)(pIn + 2 * strideIn0);
                        *(ushort*)(pOut + 3 * strideOut0) = *(ushort*)(pIn + 3 * strideIn0);
                        *(ushort*)(pOut + 4 * strideOut0) = *(ushort*)(pIn + 4 * strideIn0);
                        *(ushort*)(pOut + 5 * strideOut0) = *(ushort*)(pIn + 5 * strideIn0);
                        *(ushort*)(pOut + 6 * strideOut0) = *(ushort*)(pIn + 6 * strideIn0);
                        *(ushort*)(pOut + 7 * strideOut0) = *(ushort*)(pIn + 7 * strideIn0);
                        pOut += 8 * strideOut0; pIn += 8 * strideIn0; leadLen -= 8;
                    }
                    while (leadLen-- > 0) {
                        *(ushort*)pOut = *(ushort*)pIn;
                        pIn += strideIn0;
                        pOut += strideOut0; 
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
                            higdimsIn += stridesIn[d];
                            higdimsOut += stridesOut[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            higdimsIn -= stridesIn[d] * (dims[d] - 1);
                            higdimsOut -= stridesOut[d] * (dims[d] - 1);
                            d++;
                        }
                    }
                }
            }
        }
#endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 

       


        /// <summary>
        /// Write content of src as addressed by inSize to the subarray of dest as addressed by outSize. No reshape (Matlab). Supports broadcasting on the right side only.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="inSize"></param>
        /// <param name="dest"></param>
        /// <param name="outBSD"></param>
        private static unsafe void WriteTo_BSD_ML_16(IntPtr src, Size inSize, IntPtr dest, long* outBSD) {

            // no broadcasting / shape checks have been done until here! 
            if (outBSD[1] == 1) {
                // disregard nr of dimensions, includes numpy scalars 
                if (inSize.NumberOfElements != 1) {
                    // let's utilize the same broadcasting helper so we get the same error message
                    inSize.CheckIsBroadcastableTo_np(outBSD + 3, (uint)outBSD[0]); 
                }
                ((complex*)dest)[outBSD[2]] = ((complex*)src)[inSize.BaseOffset];
                return; 
            }

            uint ndims = (uint)outBSD[0];
            long outLen = outBSD[1];
        
            complex* pIn = (complex*)src + inSize.BaseOffset;  // init with base offset
            complex* pOut = (complex*)dest + (ulong)outBSD[2];

            // strides are long* 
            long* ordered_bsd = stackalloc long[(int)ndims * 3 + 3];
            // ordered_bsd stores the dest BSD (original), dims + strides (corrected) for src
            Helper.PrepareBSD4WriteTo(inSize, outBSD, (uint)sizeof(complex), ordered_bsd);
            // empty arrays - "early" exit after error checks 
            if (outLen == 0) {
                return;
            }
            System.Diagnostics.Debug.Assert(!Equals(inSize, null) && inSize.NumberOfElements > 0, "Empty arrays must be handled separately.");

            // ordered_strides has SCALED strides! -> pIn and pOut are now byte*!
            // pIn, pOut -> include base offset! 

            Complex.Strided64(
                (byte*)pIn, (byte*)pOut,
                0, outLen,
                ordered_bsd);
        }

        internal static class Complex {

            // pSrc is b_y_t_e* - always!! increments are faster by scaled strides!  
            internal static unsafe void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsd) {
                // bsd: outBSD + inStrides. All scaled + in ready 4 broadcasting (singletons -> 0-strides)
                uint ndims = (uint)bsd[0];
                long* dims = bsd + 3;
                long* stridesOut = dims + ndims;
                long* stridesIn = stridesOut + ndims;
                long strideOut0 = stridesOut[0];
                long strideIn0 = stridesIn[0];
                long* cur = stackalloc long[(int)ndims];

                // figure out the dimension index position for start
                System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

                cur[0] = start % dims[0];
                long f = start / dims[0];
                long higdimsOut = 0;
                long higdimsIn = 0;
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % dims[i];
                    f /= dims[i];
                    higdimsOut += cur[i] * stridesOut[i];
                    higdimsIn += cur[i] * stridesIn[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);

                while (true) {

                    byte* pIn = pSrc + higdimsIn + cur[0] * strideIn0;
                    byte* pOut = pDest + higdimsOut + cur[0] * strideOut0;

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] - cur[0]);
                    len -= leadLen; 

                    while (leadLen > 8) {
                        *(complex*)(pOut) = (*(complex*)pIn);
                        *(complex*)(pOut + strideOut0) = *(complex*)(pIn + strideIn0);
                        *(complex*)(pOut + 2 * strideOut0) = *(complex*)(pIn + 2 * strideIn0);
                        *(complex*)(pOut + 3 * strideOut0) = *(complex*)(pIn + 3 * strideIn0);
                        *(complex*)(pOut + 4 * strideOut0) = *(complex*)(pIn + 4 * strideIn0);
                        *(complex*)(pOut + 5 * strideOut0) = *(complex*)(pIn + 5 * strideIn0);
                        *(complex*)(pOut + 6 * strideOut0) = *(complex*)(pIn + 6 * strideIn0);
                        *(complex*)(pOut + 7 * strideOut0) = *(complex*)(pIn + 7 * strideIn0);
                        pOut += 8 * strideOut0; pIn += 8 * strideIn0; leadLen -= 8;
                    }
                    while (leadLen-- > 0) {
                        *(complex*)pOut = *(complex*)pIn;
                        pIn += strideIn0;
                        pOut += strideOut0; 
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
                            higdimsIn += stridesIn[d];
                            higdimsOut += stridesOut[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            higdimsIn -= stridesIn[d] * (dims[d] - 1);
                            higdimsOut -= stridesOut[d] * (dims[d] - 1);
                            d++;
                        }
                    }
                }
            }
        }

       


        /// <summary>
        /// Write content of src as addressed by inSize to the subarray of dest as addressed by outSize. No reshape (Matlab). Supports broadcasting on the right side only.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="inSize"></param>
        /// <param name="dest"></param>
        /// <param name="outBSD"></param>
        private static unsafe void WriteTo_BSD_ML_8(IntPtr src, Size inSize, IntPtr dest, long* outBSD) {

            // no broadcasting / shape checks have been done until here! 
            if (outBSD[1] == 1) {
                // disregard nr of dimensions, includes numpy scalars 
                if (inSize.NumberOfElements != 1) {
                    // let's utilize the same broadcasting helper so we get the same error message
                    inSize.CheckIsBroadcastableTo_np(outBSD + 3, (uint)outBSD[0]); 
                }
                ((ulong*)dest)[outBSD[2]] = ((ulong*)src)[inSize.BaseOffset];
                return; 
            }

            uint ndims = (uint)outBSD[0];
            long outLen = outBSD[1];
        
            ulong* pIn = (ulong*)src + inSize.BaseOffset;  // init with base offset
            ulong* pOut = (ulong*)dest + (ulong)outBSD[2];

            // strides are long* 
            long* ordered_bsd = stackalloc long[(int)ndims * 3 + 3];
            // ordered_bsd stores the dest BSD (original), dims + strides (corrected) for src
            Helper.PrepareBSD4WriteTo(inSize, outBSD, (uint)sizeof(ulong), ordered_bsd);
            // empty arrays - "early" exit after error checks 
            if (outLen == 0) {
                return;
            }
            System.Diagnostics.Debug.Assert(!Equals(inSize, null) && inSize.NumberOfElements > 0, "Empty arrays must be handled separately.");

            // ordered_strides has SCALED strides! -> pIn and pOut are now byte*!
            // pIn, pOut -> include base offset! 

            UInt64.Strided64(
                (byte*)pIn, (byte*)pOut,
                0, outLen,
                ordered_bsd);
        }

        internal static class UInt64 {

            // pSrc is b_y_t_e* - always!! increments are faster by scaled strides!  
            internal static unsafe void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsd) {
                // bsd: outBSD + inStrides. All scaled + in ready 4 broadcasting (singletons -> 0-strides)
                uint ndims = (uint)bsd[0];
                long* dims = bsd + 3;
                long* stridesOut = dims + ndims;
                long* stridesIn = stridesOut + ndims;
                long strideOut0 = stridesOut[0];
                long strideIn0 = stridesIn[0];
                long* cur = stackalloc long[(int)ndims];

                // figure out the dimension index position for start
                System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

                cur[0] = start % dims[0];
                long f = start / dims[0];
                long higdimsOut = 0;
                long higdimsIn = 0;
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % dims[i];
                    f /= dims[i];
                    higdimsOut += cur[i] * stridesOut[i];
                    higdimsIn += cur[i] * stridesIn[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);

                while (true) {

                    byte* pIn = pSrc + higdimsIn + cur[0] * strideIn0;
                    byte* pOut = pDest + higdimsOut + cur[0] * strideOut0;

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] - cur[0]);
                    len -= leadLen; 

                    while (leadLen > 8) {
                        *(ulong*)(pOut) = (*(ulong*)pIn);
                        *(ulong*)(pOut + strideOut0) = *(ulong*)(pIn + strideIn0);
                        *(ulong*)(pOut + 2 * strideOut0) = *(ulong*)(pIn + 2 * strideIn0);
                        *(ulong*)(pOut + 3 * strideOut0) = *(ulong*)(pIn + 3 * strideIn0);
                        *(ulong*)(pOut + 4 * strideOut0) = *(ulong*)(pIn + 4 * strideIn0);
                        *(ulong*)(pOut + 5 * strideOut0) = *(ulong*)(pIn + 5 * strideIn0);
                        *(ulong*)(pOut + 6 * strideOut0) = *(ulong*)(pIn + 6 * strideIn0);
                        *(ulong*)(pOut + 7 * strideOut0) = *(ulong*)(pIn + 7 * strideIn0);
                        pOut += 8 * strideOut0; pIn += 8 * strideIn0; leadLen -= 8;
                    }
                    while (leadLen-- > 0) {
                        *(ulong*)pOut = *(ulong*)pIn;
                        pIn += strideIn0;
                        pOut += strideOut0; 
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
                            higdimsIn += stridesIn[d];
                            higdimsOut += stridesOut[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            higdimsIn -= stridesIn[d] * (dims[d] - 1);
                            higdimsOut -= stridesOut[d] * (dims[d] - 1);
                            d++;
                        }
                    }
                }
            }
        }

       


        /// <summary>
        /// Write content of src as addressed by inSize to the subarray of dest as addressed by outSize. No reshape (Matlab). Supports broadcasting on the right side only.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="inSize"></param>
        /// <param name="dest"></param>
        /// <param name="outBSD"></param>
        private static unsafe void WriteTo_BSD_ML_4(IntPtr src, Size inSize, IntPtr dest, long* outBSD) {

            // no broadcasting / shape checks have been done until here! 
            if (outBSD[1] == 1) {
                // disregard nr of dimensions, includes numpy scalars 
                if (inSize.NumberOfElements != 1) {
                    // let's utilize the same broadcasting helper so we get the same error message
                    inSize.CheckIsBroadcastableTo_np(outBSD + 3, (uint)outBSD[0]); 
                }
                ((uint*)dest)[outBSD[2]] = ((uint*)src)[inSize.BaseOffset];
                return; 
            }

            uint ndims = (uint)outBSD[0];
            long outLen = outBSD[1];
        
            uint* pIn = (uint*)src + inSize.BaseOffset;  // init with base offset
            uint* pOut = (uint*)dest + (ulong)outBSD[2];

            // strides are long* 
            long* ordered_bsd = stackalloc long[(int)ndims * 3 + 3];
            // ordered_bsd stores the dest BSD (original), dims + strides (corrected) for src
            Helper.PrepareBSD4WriteTo(inSize, outBSD, (uint)sizeof(uint), ordered_bsd);
            // empty arrays - "early" exit after error checks 
            if (outLen == 0) {
                return;
            }
            System.Diagnostics.Debug.Assert(!Equals(inSize, null) && inSize.NumberOfElements > 0, "Empty arrays must be handled separately.");

            // ordered_strides has SCALED strides! -> pIn and pOut are now byte*!
            // pIn, pOut -> include base offset! 

            UInt32.Strided64(
                (byte*)pIn, (byte*)pOut,
                0, outLen,
                ordered_bsd);
        }

        internal static class UInt32 {

            // pSrc is b_y_t_e* - always!! increments are faster by scaled strides!  
            internal static unsafe void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsd) {
                // bsd: outBSD + inStrides. All scaled + in ready 4 broadcasting (singletons -> 0-strides)
                uint ndims = (uint)bsd[0];
                long* dims = bsd + 3;
                long* stridesOut = dims + ndims;
                long* stridesIn = stridesOut + ndims;
                long strideOut0 = stridesOut[0];
                long strideIn0 = stridesIn[0];
                long* cur = stackalloc long[(int)ndims];

                // figure out the dimension index position for start
                System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

                cur[0] = start % dims[0];
                long f = start / dims[0];
                long higdimsOut = 0;
                long higdimsIn = 0;
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % dims[i];
                    f /= dims[i];
                    higdimsOut += cur[i] * stridesOut[i];
                    higdimsIn += cur[i] * stridesIn[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);

                while (true) {

                    byte* pIn = pSrc + higdimsIn + cur[0] * strideIn0;
                    byte* pOut = pDest + higdimsOut + cur[0] * strideOut0;

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] - cur[0]);
                    len -= leadLen; 

                    while (leadLen > 8) {
                        *(uint*)(pOut) = (*(uint*)pIn);
                        *(uint*)(pOut + strideOut0) = *(uint*)(pIn + strideIn0);
                        *(uint*)(pOut + 2 * strideOut0) = *(uint*)(pIn + 2 * strideIn0);
                        *(uint*)(pOut + 3 * strideOut0) = *(uint*)(pIn + 3 * strideIn0);
                        *(uint*)(pOut + 4 * strideOut0) = *(uint*)(pIn + 4 * strideIn0);
                        *(uint*)(pOut + 5 * strideOut0) = *(uint*)(pIn + 5 * strideIn0);
                        *(uint*)(pOut + 6 * strideOut0) = *(uint*)(pIn + 6 * strideIn0);
                        *(uint*)(pOut + 7 * strideOut0) = *(uint*)(pIn + 7 * strideIn0);
                        pOut += 8 * strideOut0; pIn += 8 * strideIn0; leadLen -= 8;
                    }
                    while (leadLen-- > 0) {
                        *(uint*)pOut = *(uint*)pIn;
                        pIn += strideIn0;
                        pOut += strideOut0; 
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
                            higdimsIn += stridesIn[d];
                            higdimsOut += stridesOut[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            higdimsIn -= stridesIn[d] * (dims[d] - 1);
                            higdimsOut -= stridesOut[d] * (dims[d] - 1);
                            d++;
                        }
                    }
                }
            }
        }

       


        /// <summary>
        /// Write content of src as addressed by inSize to the subarray of dest as addressed by outSize. No reshape (Matlab). Supports broadcasting on the right side only.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="inSize"></param>
        /// <param name="dest"></param>
        /// <param name="outBSD"></param>
        private static unsafe void WriteTo_BSD_ML_1(IntPtr src, Size inSize, IntPtr dest, long* outBSD) {

            // no broadcasting / shape checks have been done until here! 
            if (outBSD[1] == 1) {
                // disregard nr of dimensions, includes numpy scalars 
                if (inSize.NumberOfElements != 1) {
                    // let's utilize the same broadcasting helper so we get the same error message
                    inSize.CheckIsBroadcastableTo_np(outBSD + 3, (uint)outBSD[0]); 
                }
                ((byte*)dest)[outBSD[2]] = ((byte*)src)[inSize.BaseOffset];
                return; 
            }

            uint ndims = (uint)outBSD[0];
            long outLen = outBSD[1];
        
            byte* pIn = (byte*)src + inSize.BaseOffset;  // init with base offset
            byte* pOut = (byte*)dest + (ulong)outBSD[2];

            // strides are long* 
            long* ordered_bsd = stackalloc long[(int)ndims * 3 + 3];
            // ordered_bsd stores the dest BSD (original), dims + strides (corrected) for src
            Helper.PrepareBSD4WriteTo(inSize, outBSD, (uint)sizeof(byte), ordered_bsd);
            // empty arrays - "early" exit after error checks 
            if (outLen == 0) {
                return;
            }
            System.Diagnostics.Debug.Assert(!Equals(inSize, null) && inSize.NumberOfElements > 0, "Empty arrays must be handled separately.");

            // ordered_strides has SCALED strides! -> pIn and pOut are now byte*!
            // pIn, pOut -> include base offset! 

            Byte.Strided64(
                (byte*)pIn, (byte*)pOut,
                0, outLen,
                ordered_bsd);
        }

        internal static class Byte {

            // pSrc is b_y_t_e* - always!! increments are faster by scaled strides!  
            internal static unsafe void Strided64(byte* pSrc, byte* pDest, long start, long len, long* bsd) {
                // bsd: outBSD + inStrides. All scaled + in ready 4 broadcasting (singletons -> 0-strides)
                uint ndims = (uint)bsd[0];
                long* dims = bsd + 3;
                long* stridesOut = dims + ndims;
                long* stridesIn = stridesOut + ndims;
                long strideOut0 = stridesOut[0];
                long strideIn0 = stridesIn[0];
                long* cur = stackalloc long[(int)ndims];

                // figure out the dimension index position for start
                System.Diagnostics.Debug.Assert(ndims > 0, "Empty arrays should be handled outside this method.");

                cur[0] = start % dims[0];
                long f = start / dims[0];
                long higdimsOut = 0;
                long higdimsIn = 0;
                int i = 1;
                for (; f > 0 && i < ndims; i++) {
                    cur[i] = f % dims[i];
                    f /= dims[i];
                    higdimsOut += cur[i] * stridesOut[i];
                    higdimsIn += cur[i] * stridesIn[i];
                }
                while (i < ndims) {
                    cur[i++] = 0;
                }

                System.Diagnostics.Debug.Assert(f == 0);

                while (true) {

                    byte* pIn = pSrc + higdimsIn + cur[0] * strideIn0;
                    byte* pOut = pDest + higdimsOut + cur[0] * strideOut0;

                    // iteration length limited to either the dimension lengths or the end of the requested chunk 
                    long leadLen = Math.Min(len, dims[0] - cur[0]);
                    len -= leadLen; 

                    while (leadLen > 8) {
                        *(byte*)(pOut) = (*(byte*)pIn);
                        *(byte*)(pOut + strideOut0) = *(byte*)(pIn + strideIn0);
                        *(byte*)(pOut + 2 * strideOut0) = *(byte*)(pIn + 2 * strideIn0);
                        *(byte*)(pOut + 3 * strideOut0) = *(byte*)(pIn + 3 * strideIn0);
                        *(byte*)(pOut + 4 * strideOut0) = *(byte*)(pIn + 4 * strideIn0);
                        *(byte*)(pOut + 5 * strideOut0) = *(byte*)(pIn + 5 * strideIn0);
                        *(byte*)(pOut + 6 * strideOut0) = *(byte*)(pIn + 6 * strideIn0);
                        *(byte*)(pOut + 7 * strideOut0) = *(byte*)(pIn + 7 * strideIn0);
                        pOut += 8 * strideOut0; pIn += 8 * strideIn0; leadLen -= 8;
                    }
                    while (leadLen-- > 0) {
                        *(byte*)pOut = *(byte*)pIn;
                        pIn += strideIn0;
                        pOut += strideOut0; 
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
                            higdimsIn += stridesIn[d];
                            higdimsOut += stridesOut[d];
                            cur[d]++;
                            break;
                        } else {
                            cur[d] = 0;
                            higdimsIn -= stridesIn[d] * (dims[d] - 1);
                            higdimsOut -= stridesOut[d] * (dims[d] - 1);
                            d++;
                        }
                    }
                }
            }
        }

#endregion HYCALPER AUTO GENERATED CODE

    }
}
