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

    internal static partial class WriteToBSDIterOperators {

        /// <summary>
        /// Write content of src as addressed by inSize to the subarray of dest as addressed by outSize. No reshape (Matlab). Supports broadcasting.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="inSize"></param>
        /// <param name="dest"></param>
        /// <param name="destSize"></param>
        /// <param name="iterators"></param>
        /// <param name="nrIterDims"></param>
        /// <param name="sizeofT">Element size in bytes.</param>
        /// <param name="lastDimIterStride"></param>

        internal static unsafe void WriteTo_BSD_Iter<T>(MemoryHandle src, Size inSize, MemoryHandle dest, Size destSize, 
                                                    IIndexIterator[] iterators, uint nrIterDims, uint sizeofT, long lastDimIterStride = -1) {

            if (src is NativeHostHandle) {
                switch (sizeofT) {
                    case 1:
                        WriteTo_BSD_Iter_1(src.Pointer, inSize, dest.Pointer, destSize, iterators, nrIterDims, lastDimIterStride);
                        break;
                    case 2:
                        WriteTo_BSD_Iter_2(src.Pointer, inSize, dest.Pointer, destSize, iterators, nrIterDims, lastDimIterStride);
                        break;
                    case 4:
                        WriteTo_BSD_Iter_4(src.Pointer, inSize, dest.Pointer, destSize, iterators, nrIterDims, lastDimIterStride);
                        break;
                    case 8:
                        WriteTo_BSD_Iter_8(src.Pointer, inSize, dest.Pointer, destSize, iterators, nrIterDims, lastDimIterStride);
                        break;
                    case 16:
                        WriteTo_BSD_Iter_16(src.Pointer, inSize, dest.Pointer, destSize, iterators, nrIterDims, lastDimIterStride);
                        break;
                    default:
                        WriteTo_BSD_Iter_Gen(src.Pointer, inSize, dest.Pointer, destSize, iterators, nrIterDims, lastDimIterStride, Storage<T>.SizeOfT);
                        break;
                        //throw new InvalidOperationException("This operation is not supported on the element datatype. Supported data types have lengths of 1, 2, 4, 8 or 16 bytes.");
                }
            } else if (src is ManagedHostHandle<T>) { 
                WriteTo_BSD_Iter_T((src as ManagedHostHandle<T>).HostArray, inSize, (dest as ManagedHostHandle<T>).HostArray, destSize, iterators, nrIterDims, lastDimIterStride);
            } else if (src is ManagedHostHandle<IStorage>) {
                WriteTo_BSD_Iter_Cell((src as ManagedHostHandle<IStorage>).HostArray, inSize, (dest as ManagedHostHandle<IStorage>).HostArray, destSize, iterators, nrIterDims, lastDimIterStride);
            }
        }

        #region HYCALPER LOOPSTART

        /*!HC:TYPELIST:
        <hycalper>
        <type>
            <source locate="here">
                WriteTo_BSD_Iter_2
            </source>
            <destination>WriteTo_BSD_Iter_1</destination>
            <destination>WriteTo_BSD_Iter_4</destination>
            <destination>WriteTo_BSD_Iter_8</destination>
            <destination>WriteTo_BSD_Iter_16</destination>
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
        /// <param name="iterators">iterator objects for each dimension addressed.</param>
        /// <param name="nrIterDims">nr. of addressed dimensions in <paramref name="iterators"/></param>
        /// <param name="destSize">modified (ML: reshaped) BSD according to output storage.</param>
        /// <param name="lastDimIterStride">if positive, take this as the stride for <paramref name="dest"/> at 
        /// the last dimension provided by <paramref name="nrIterDims"/>. This is required for ML style only.</param>
        
        private static unsafe void WriteTo_BSD_Iter_2(IntPtr src, Size inSize, IntPtr dest, Size destSize, 
                                                        IIndexIterator[] iterators, uint nrIterDims, long lastDimIterStride) {

            long inHighDims = inSize.BaseOffset;
            long destHighDims = destSize.BaseOffset;
            long inStride0 = inSize.GetStride(0);
            long destStride0 = (nrIterDims == 1 && lastDimIterStride >= 0) ? lastDimIterStride : destSize.GetStride(0);
            var inBSD = inSize.GetBSD(false);
            uint inNDims = (uint)inBSD[0]; 
            long outLen = iterators[0].GetLength();

            for (uint i = 1; i < nrIterDims; i++) {

                if (!iterators[i].MoveNext()) {
                    return; // nothing to do. Empty right side.
                }

                var inStr = inSize.GetStride(i);
                // make sure singleton dimensions have 0 stride for broadcasting
                if (inStr != 0 && inSize[i] == 1) { // this means i is inside NumberDimensions
                    inBSD[3 + inNDims + i] = 0;
                    inStr = 0; 
                }
                //inHighDims += inStr;  // not needd? inHighDims is always == BaseArray, no? (We iterate the full array, even when broadcasting)
                var destStride = (nrIterDims == i + 1 && lastDimIterStride >= 0) ? lastDimIterStride : destSize.GetStride(i);
                destHighDims += destStride * iterators[i].Current;
                outLen *= iterators[i].GetLength(); 
            }

            ushort* pIn = (ushort*)src; 
            ushort* pOut = (ushort*)dest;

            if (outLen == 0) {
                return;
            }
            var it0 = iterators[0]; 

            while(true) {
                long inCur0 = 0; 
                while (it0.MoveNext()) {
                    pOut[destHighDims + destStride0 * it0.Current] = pIn[inHighDims + inStride0 * inCur0++]; 
                }
                it0.Reset();

                // increase higher dims
                uint d = 1;
                while (d < nrIterDims) {
                    var itD = iterators[d]; // boxed instance. Always the same! 
                    var oldIdx = itD.Current;
                    var destStride = (nrIterDims == d + 1 && lastDimIterStride >= 0) 
                                    ? lastDimIterStride : destSize.GetStride(d);

                    if (itD.MoveNext()) {
                        var val = itD.Current;
                        destHighDims += (val - oldIdx) * destStride;
                        inHighDims += inSize.GetStride(d); 
                        break;
                    } else {
                        itD.Reset();
                        itD.MoveNext();  // assuming this succeeds
                        destHighDims += (itD.Current - oldIdx) * destStride;
                        inHighDims -= (inSize[d] - 1) * inSize.GetStride(d);
                        d++; 
                    }
                }
                if (d == nrIterDims) return;
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
        /// <param name="iterators">iterator objects for each dimension addressed.</param>
        /// <param name="nrIterDims">nr. of addressed dimensions in <paramref name="iterators"/></param>
        /// <param name="destSize">modified (ML: reshaped) BSD according to output storage.</param>
        /// <param name="lastDimIterStride">if positive, take this as the stride for <paramref name="dest"/> at 
        /// the last dimension provided by <paramref name="nrIterDims"/>. This is required for ML style only.</param>
        
        private static unsafe void WriteTo_BSD_Iter_16(IntPtr src, Size inSize, IntPtr dest, Size destSize, 
                                                        IIndexIterator[] iterators, uint nrIterDims, long lastDimIterStride) {

            long inHighDims = inSize.BaseOffset;
            long destHighDims = destSize.BaseOffset;
            long inStride0 = inSize.GetStride(0);
            long destStride0 = (nrIterDims == 1 && lastDimIterStride >= 0) ? lastDimIterStride : destSize.GetStride(0);
            var inBSD = inSize.GetBSD(false);
            uint inNDims = (uint)inBSD[0]; 
            long outLen = iterators[0].GetLength();

            for (uint i = 1; i < nrIterDims; i++) {

                if (!iterators[i].MoveNext()) {
                    return; // nothing to do. Empty right side.
                }

                var inStr = inSize.GetStride(i);
                // make sure singleton dimensions have 0 stride for broadcasting
                if (inStr != 0 && inSize[i] == 1) { // this means i is inside NumberDimensions
                    inBSD[3 + inNDims + i] = 0;
                    inStr = 0; 
                }
                //inHighDims += inStr;  // not needd? inHighDims is always == BaseArray, no? (We iterate the full array, even when broadcasting)
                var destStride = (nrIterDims == i + 1 && lastDimIterStride >= 0) ? lastDimIterStride : destSize.GetStride(i);
                destHighDims += destStride * iterators[i].Current;
                outLen *= iterators[i].GetLength(); 
            }

            complex* pIn = (complex*)src; 
            complex* pOut = (complex*)dest;

            if (outLen == 0) {
                return;
            }
            var it0 = iterators[0]; 

            while(true) {
                long inCur0 = 0; 
                while (it0.MoveNext()) {
                    pOut[destHighDims + destStride0 * it0.Current] = pIn[inHighDims + inStride0 * inCur0++]; 
                }
                it0.Reset();

                // increase higher dims
                uint d = 1;
                while (d < nrIterDims) {
                    var itD = iterators[d]; // boxed instance. Always the same! 
                    var oldIdx = itD.Current;
                    var destStride = (nrIterDims == d + 1 && lastDimIterStride >= 0) 
                                    ? lastDimIterStride : destSize.GetStride(d);

                    if (itD.MoveNext()) {
                        var val = itD.Current;
                        destHighDims += (val - oldIdx) * destStride;
                        inHighDims += inSize.GetStride(d); 
                        break;
                    } else {
                        itD.Reset();
                        itD.MoveNext();  // assuming this succeeds
                        destHighDims += (itD.Current - oldIdx) * destStride;
                        inHighDims -= (inSize[d] - 1) * inSize.GetStride(d);
                        d++; 
                    }
                }
                if (d == nrIterDims) return;
            }
        }

       

        /// <summary>
        /// Write content of src as addressed by inSize to the subarray of dest as addressed by outSize. No reshape (Matlab). Supports broadcasting on the right side only.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="inSize"></param>
        /// <param name="dest"></param>
        /// <param name="iterators">iterator objects for each dimension addressed.</param>
        /// <param name="nrIterDims">nr. of addressed dimensions in <paramref name="iterators"/></param>
        /// <param name="destSize">modified (ML: reshaped) BSD according to output storage.</param>
        /// <param name="lastDimIterStride">if positive, take this as the stride for <paramref name="dest"/> at 
        /// the last dimension provided by <paramref name="nrIterDims"/>. This is required for ML style only.</param>
        
        private static unsafe void WriteTo_BSD_Iter_8(IntPtr src, Size inSize, IntPtr dest, Size destSize, 
                                                        IIndexIterator[] iterators, uint nrIterDims, long lastDimIterStride) {

            long inHighDims = inSize.BaseOffset;
            long destHighDims = destSize.BaseOffset;
            long inStride0 = inSize.GetStride(0);
            long destStride0 = (nrIterDims == 1 && lastDimIterStride >= 0) ? lastDimIterStride : destSize.GetStride(0);
            var inBSD = inSize.GetBSD(false);
            uint inNDims = (uint)inBSD[0]; 
            long outLen = iterators[0].GetLength();

            for (uint i = 1; i < nrIterDims; i++) {

                if (!iterators[i].MoveNext()) {
                    return; // nothing to do. Empty right side.
                }

                var inStr = inSize.GetStride(i);
                // make sure singleton dimensions have 0 stride for broadcasting
                if (inStr != 0 && inSize[i] == 1) { // this means i is inside NumberDimensions
                    inBSD[3 + inNDims + i] = 0;
                    inStr = 0; 
                }
                //inHighDims += inStr;  // not needd? inHighDims is always == BaseArray, no? (We iterate the full array, even when broadcasting)
                var destStride = (nrIterDims == i + 1 && lastDimIterStride >= 0) ? lastDimIterStride : destSize.GetStride(i);
                destHighDims += destStride * iterators[i].Current;
                outLen *= iterators[i].GetLength(); 
            }

            ulong* pIn = (ulong*)src; 
            ulong* pOut = (ulong*)dest;

            if (outLen == 0) {
                return;
            }
            var it0 = iterators[0]; 

            while(true) {
                long inCur0 = 0; 
                while (it0.MoveNext()) {
                    pOut[destHighDims + destStride0 * it0.Current] = pIn[inHighDims + inStride0 * inCur0++]; 
                }
                it0.Reset();

                // increase higher dims
                uint d = 1;
                while (d < nrIterDims) {
                    var itD = iterators[d]; // boxed instance. Always the same! 
                    var oldIdx = itD.Current;
                    var destStride = (nrIterDims == d + 1 && lastDimIterStride >= 0) 
                                    ? lastDimIterStride : destSize.GetStride(d);

                    if (itD.MoveNext()) {
                        var val = itD.Current;
                        destHighDims += (val - oldIdx) * destStride;
                        inHighDims += inSize.GetStride(d); 
                        break;
                    } else {
                        itD.Reset();
                        itD.MoveNext();  // assuming this succeeds
                        destHighDims += (itD.Current - oldIdx) * destStride;
                        inHighDims -= (inSize[d] - 1) * inSize.GetStride(d);
                        d++; 
                    }
                }
                if (d == nrIterDims) return;
            }
        }

       

        /// <summary>
        /// Write content of src as addressed by inSize to the subarray of dest as addressed by outSize. No reshape (Matlab). Supports broadcasting on the right side only.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="inSize"></param>
        /// <param name="dest"></param>
        /// <param name="iterators">iterator objects for each dimension addressed.</param>
        /// <param name="nrIterDims">nr. of addressed dimensions in <paramref name="iterators"/></param>
        /// <param name="destSize">modified (ML: reshaped) BSD according to output storage.</param>
        /// <param name="lastDimIterStride">if positive, take this as the stride for <paramref name="dest"/> at 
        /// the last dimension provided by <paramref name="nrIterDims"/>. This is required for ML style only.</param>
        
        private static unsafe void WriteTo_BSD_Iter_4(IntPtr src, Size inSize, IntPtr dest, Size destSize, 
                                                        IIndexIterator[] iterators, uint nrIterDims, long lastDimIterStride) {

            long inHighDims = inSize.BaseOffset;
            long destHighDims = destSize.BaseOffset;
            long inStride0 = inSize.GetStride(0);
            long destStride0 = (nrIterDims == 1 && lastDimIterStride >= 0) ? lastDimIterStride : destSize.GetStride(0);
            var inBSD = inSize.GetBSD(false);
            uint inNDims = (uint)inBSD[0]; 
            long outLen = iterators[0].GetLength();

            for (uint i = 1; i < nrIterDims; i++) {

                if (!iterators[i].MoveNext()) {
                    return; // nothing to do. Empty right side.
                }

                var inStr = inSize.GetStride(i);
                // make sure singleton dimensions have 0 stride for broadcasting
                if (inStr != 0 && inSize[i] == 1) { // this means i is inside NumberDimensions
                    inBSD[3 + inNDims + i] = 0;
                    inStr = 0; 
                }
                //inHighDims += inStr;  // not needd? inHighDims is always == BaseArray, no? (We iterate the full array, even when broadcasting)
                var destStride = (nrIterDims == i + 1 && lastDimIterStride >= 0) ? lastDimIterStride : destSize.GetStride(i);
                destHighDims += destStride * iterators[i].Current;
                outLen *= iterators[i].GetLength(); 
            }

            uint* pIn = (uint*)src; 
            uint* pOut = (uint*)dest;

            if (outLen == 0) {
                return;
            }
            var it0 = iterators[0]; 

            while(true) {
                long inCur0 = 0; 
                while (it0.MoveNext()) {
                    pOut[destHighDims + destStride0 * it0.Current] = pIn[inHighDims + inStride0 * inCur0++]; 
                }
                it0.Reset();

                // increase higher dims
                uint d = 1;
                while (d < nrIterDims) {
                    var itD = iterators[d]; // boxed instance. Always the same! 
                    var oldIdx = itD.Current;
                    var destStride = (nrIterDims == d + 1 && lastDimIterStride >= 0) 
                                    ? lastDimIterStride : destSize.GetStride(d);

                    if (itD.MoveNext()) {
                        var val = itD.Current;
                        destHighDims += (val - oldIdx) * destStride;
                        inHighDims += inSize.GetStride(d); 
                        break;
                    } else {
                        itD.Reset();
                        itD.MoveNext();  // assuming this succeeds
                        destHighDims += (itD.Current - oldIdx) * destStride;
                        inHighDims -= (inSize[d] - 1) * inSize.GetStride(d);
                        d++; 
                    }
                }
                if (d == nrIterDims) return;
            }
        }

       

        /// <summary>
        /// Write content of src as addressed by inSize to the subarray of dest as addressed by outSize. No reshape (Matlab). Supports broadcasting on the right side only.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="inSize"></param>
        /// <param name="dest"></param>
        /// <param name="iterators">iterator objects for each dimension addressed.</param>
        /// <param name="nrIterDims">nr. of addressed dimensions in <paramref name="iterators"/></param>
        /// <param name="destSize">modified (ML: reshaped) BSD according to output storage.</param>
        /// <param name="lastDimIterStride">if positive, take this as the stride for <paramref name="dest"/> at 
        /// the last dimension provided by <paramref name="nrIterDims"/>. This is required for ML style only.</param>
        
        private static unsafe void WriteTo_BSD_Iter_1(IntPtr src, Size inSize, IntPtr dest, Size destSize, 
                                                        IIndexIterator[] iterators, uint nrIterDims, long lastDimIterStride) {

            long inHighDims = inSize.BaseOffset;
            long destHighDims = destSize.BaseOffset;
            long inStride0 = inSize.GetStride(0);
            long destStride0 = (nrIterDims == 1 && lastDimIterStride >= 0) ? lastDimIterStride : destSize.GetStride(0);
            var inBSD = inSize.GetBSD(false);
            uint inNDims = (uint)inBSD[0]; 
            long outLen = iterators[0].GetLength();

            for (uint i = 1; i < nrIterDims; i++) {

                if (!iterators[i].MoveNext()) {
                    return; // nothing to do. Empty right side.
                }

                var inStr = inSize.GetStride(i);
                // make sure singleton dimensions have 0 stride for broadcasting
                if (inStr != 0 && inSize[i] == 1) { // this means i is inside NumberDimensions
                    inBSD[3 + inNDims + i] = 0;
                    inStr = 0; 
                }
                //inHighDims += inStr;  // not needd? inHighDims is always == BaseArray, no? (We iterate the full array, even when broadcasting)
                var destStride = (nrIterDims == i + 1 && lastDimIterStride >= 0) ? lastDimIterStride : destSize.GetStride(i);
                destHighDims += destStride * iterators[i].Current;
                outLen *= iterators[i].GetLength(); 
            }

            byte* pIn = (byte*)src; 
            byte* pOut = (byte*)dest;

            if (outLen == 0) {
                return;
            }
            var it0 = iterators[0]; 

            while(true) {
                long inCur0 = 0; 
                while (it0.MoveNext()) {
                    pOut[destHighDims + destStride0 * it0.Current] = pIn[inHighDims + inStride0 * inCur0++]; 
                }
                it0.Reset();

                // increase higher dims
                uint d = 1;
                while (d < nrIterDims) {
                    var itD = iterators[d]; // boxed instance. Always the same! 
                    var oldIdx = itD.Current;
                    var destStride = (nrIterDims == d + 1 && lastDimIterStride >= 0) 
                                    ? lastDimIterStride : destSize.GetStride(d);

                    if (itD.MoveNext()) {
                        var val = itD.Current;
                        destHighDims += (val - oldIdx) * destStride;
                        inHighDims += inSize.GetStride(d); 
                        break;
                    } else {
                        itD.Reset();
                        itD.MoveNext();  // assuming this succeeds
                        destHighDims += (itD.Current - oldIdx) * destStride;
                        inHighDims -= (inSize[d] - 1) * inSize.GetStride(d);
                        d++; 
                    }
                }
                if (d == nrIterDims) return;
            }
        }

#endregion HYCALPER AUTO GENERATED CODE

        /// <summary>
        /// Write content of src as addressed by inSize to the subarray of dest as addressed by outSize. No reshape (Matlab). Supports broadcasting on the right side only.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="inSize"></param>
        /// <param name="dest"></param>
        /// <param name="iterators">iterator objects for each dimension addressed.</param>
        /// <param name="nrIterDims">nr. of addressed dimensions in <paramref name="iterators"/></param>
        /// <param name="destSize">modified (ML: reshaped) BSD according to output storage.</param>
        /// <param name="lastDimIterStride">if positive, take this as the stride for <paramref name="dest"/> at 
        /// the last dimension provided by <paramref name="nrIterDims"/>. This is required for ML style only.</param>
        /// <param name="sizeOfT"></param>

        private static unsafe void WriteTo_BSD_Iter_Gen(IntPtr src, Size inSize, IntPtr dest, Size destSize,
                                                        IIndexIterator[] iterators, uint nrIterDims, long lastDimIterStride, uint sizeOfT) {

            long inHighDims = inSize.BaseOffset;
            long destHighDims = destSize.BaseOffset;
            long inStride0 = inSize.GetStride(0);
            long destStride0 = (nrIterDims == 1 && lastDimIterStride >= 0) ? lastDimIterStride : destSize.GetStride(0);
            var inBSD = inSize.GetBSD(false);
            uint inNDims = (uint)inBSD[0];
            long outLen = iterators[0].GetLength();

            for (uint i = 1; i < nrIterDims; i++) {

                if (!iterators[i].MoveNext()) {
                    return; // nothing to do. Empty right side.
                }

                var inStr = inSize.GetStride(i);
                // make sure singleton dimensions have 0 stride for broadcasting
                if (inStr != 0 && inSize[i] == 1) { // this means i is inside NumberDimensions
                    inBSD[3 + inNDims + i] = 0;
                    inStr = 0;
                }
                //inHighDims += inStr;  // not needd? inHighDims is always == BaseArray, no? (We iterate the full array, even when broadcasting)
                var destStride = (nrIterDims == i + 1 && lastDimIterStride >= 0) ? lastDimIterStride : destSize.GetStride(i);
                destHighDims += destStride * iterators[i].Current;
                outLen *= iterators[i].GetLength();
            }


            if (outLen == 0) {
                return;
            }
            var it0 = iterators[0];

            while (true) {
                long inCur0 = 0;
                while (it0.MoveNext()) {
                    byte* pIn = (byte*)src + (inHighDims + inStride0 * inCur0) * sizeOfT;
                    byte* pOut = (byte*)dest + (destHighDims + destStride0 * it0.Current) * sizeOfT;
                    uint l = sizeOfT;
                    while (l > 4) {
                        pOut[0] = pIn[0];
                        pOut[1] = pIn[1];
                        pOut[2] = pIn[2];
                        pOut[3] = pIn[3];
                        l -= 4; pOut += 4; pIn += 4; 
                    }
                    while (l -- > 0) {
                        *(pOut++) = *(pIn++); 
                    }
                    inCur0++; 
                }
                it0.Reset();

                // increase higher dims
                uint d = 1;
                while (d < nrIterDims) {
                    var itD = iterators[d]; // boxed instance. Always the same! 
                    var oldIdx = itD.Current;
                    var destStride = (nrIterDims == d + 1 && lastDimIterStride >= 0)
                                    ? lastDimIterStride : destSize.GetStride(d);

                    if (itD.MoveNext()) {
                        var val = itD.Current;
                        destHighDims += (val - oldIdx) * destStride;
                        inHighDims += inSize.GetStride(d);
                        break;
                    } else {
                        itD.Reset();
                        itD.MoveNext();  // assuming this succeeds
                        destHighDims += (itD.Current - oldIdx) * destStride;
                        inHighDims -= (inSize[d] - 1) * inSize.GetStride(d);
                        d++;
                    }
                }
                if (d == nrIterDims) return;
            }
        }

        /// <summary>
        /// Write content of src as addressed by inSize to the subarray of dest as addressed by outSize. No reshape (Matlab). Supports broadcasting on the right side only.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="inSize"></param>
        /// <param name="dest"></param>
        /// <param name="iterators">iterator objects for each dimension addressed.</param>
        /// <param name="nrIterDims">nr. of addressed dimensions in <paramref name="iterators"/></param>
        /// <param name="destSize">modified (ML: reshaped) BSD according to output storage.</param>
        /// <param name="lastDimIterStride">if positive, take this as the stride for <paramref name="dest"/> at 
        /// the last dimension provided by <paramref name="nrIterDims"/>. This is required for ML style only.</param>
        
        private static unsafe void WriteTo_BSD_Iter_T<T>(T[] src, Size inSize, T[] dest, Size destSize,
                                                        IIndexIterator[] iterators, uint nrIterDims, long lastDimIterStride) {

            long inHighDims = inSize.BaseOffset;
            long destHighDims = destSize.BaseOffset;
            long inStride0 = inSize.GetStride(0);
            long destStride0 = (nrIterDims == 1 && lastDimIterStride >= 0) ? lastDimIterStride : destSize.GetStride(0);
            var inBSD = inSize.GetBSD(false);
            uint inNDims = (uint)inBSD[0];
            long outLen = iterators[0].GetLength();

            for (uint i = 1; i < nrIterDims; i++) {

                if (!iterators[i].MoveNext()) {
                    return; // nothing to do. Empty right side.
                }

                var inStr = inSize.GetStride(i);
                // make sure singleton dimensions have 0 stride for broadcasting
                if (inStr != 0 && inSize[i] == 1) { // this means i is inside NumberDimensions
                    inBSD[3 + inNDims + i] = 0;
                    inStr = 0;
                }
                //inHighDims += inStr;  // not needd? inHighDims is always == BaseArray, no? (We iterate the full array, even when broadcasting)
                var destStride = (nrIterDims == i + 1 && lastDimIterStride >= 0) ? lastDimIterStride : destSize.GetStride(i);
                destHighDims += destStride * iterators[i].Current;
                outLen *= iterators[i].GetLength();
            }

            if (outLen == 0) {
                return;
            }
            var it0 = iterators[0];

            while (true) {
                long inCur0 = 0;
                while (it0.MoveNext()) {
                    dest[destHighDims + destStride0 * it0.Current] = src[inHighDims + inStride0 * inCur0++];
                }
                it0.Reset();

                // increase higher dims
                uint d = 1;
                while (d < nrIterDims) {
                    var itD = iterators[d]; // boxed instance. Always the same! 
                    var oldIdx = itD.Current;
                    var destStride = (nrIterDims == d + 1 && lastDimIterStride >= 0)
                                    ? lastDimIterStride : destSize.GetStride(d);

                    if (itD.MoveNext()) {
                        var val = itD.Current;
                        destHighDims += (val - oldIdx) * destStride;
                        inHighDims += inSize.GetStride(d);
                        break;
                    } else {
                        itD.Reset();
                        itD.MoveNext();  // assuming this succeeds
                        destHighDims += (itD.Current - oldIdx) * destStride;
                        inHighDims -= (inSize[d] - 1) * inSize.GetStride(d);
                        d++;
                    }
                }
                if (d == nrIterDims) return;
            }
        }
        /// <summary>
        /// Write content of src as addressed by inSize to the subarray of dest as addressed by outSize. No reshape (Matlab). Supports broadcasting on the right side only.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="inSize"></param>
        /// <param name="dest"></param>
        /// <param name="iterators">iterator objects for each dimension addressed.</param>
        /// <param name="nrIterDims">nr. of addressed dimensions in <paramref name="iterators"/></param>
        /// <param name="destSize">modified (ML: reshaped) BSD according to output storage.</param>
        /// <param name="lastDimIterStride">if positive, take this as the stride for <paramref name="dest"/> at 
        /// the last dimension provided by <paramref name="nrIterDims"/>. This is required for ML style only.</param>
        
        private static unsafe void WriteTo_BSD_Iter_Cell(IStorage[] src, Size inSize, IStorage[] dest, Size destSize,
                                                        IIndexIterator[] iterators, uint nrIterDims, long lastDimIterStride) {

            long inHighDims = inSize.BaseOffset;
            long destHighDims = destSize.BaseOffset;
            long inStride0 = inSize.GetStride(0);
            long destStride0 = (nrIterDims == 1 && lastDimIterStride >= 0) ? lastDimIterStride : destSize.GetStride(0);
            var inBSD = inSize.GetBSD(false);
            uint inNDims = (uint)inBSD[0];
            long outLen = iterators[0].GetLength();

            for (uint i = 1; i < nrIterDims; i++) {

                if (!iterators[i].MoveNext()) {
                    return; // nothing to do. Empty right side.
                }

                var inStr = inSize.GetStride(i);
                // make sure singleton dimensions have 0 stride for broadcasting
                if (inStr != 0 && inSize[i] == 1) { // this means i is inside NumberDimensions
                    inBSD[3 + inNDims + i] = 0;
                    inStr = 0;
                }
                //inHighDims += inStr;  // not needd? inHighDims is always == BaseArray, no? (We iterate the full array, even when broadcasting)
                var destStride = (nrIterDims == i + 1 && lastDimIterStride >= 0) ? lastDimIterStride : destSize.GetStride(i);
                destHighDims += destStride * iterators[i].Current;
                outLen *= iterators[i].GetLength();
            }

            if (outLen == 0) {
                return;
            }
            var it0 = iterators[0];

            while (true) {
                long inCur0 = 0;
                while (it0.MoveNext()) {
                    CellStorage.replace(ref dest[destHighDims + destStride0 * it0.Current], ref src[inHighDims + inStride0 * inCur0++]);
                }
                it0.Reset();

                // increase higher dims
                uint d = 1;
                while (d < nrIterDims) {
                    var itD = iterators[d]; // boxed instance. Always the same! 
                    var oldIdx = itD.Current;
                    var destStride = (nrIterDims == d + 1 && lastDimIterStride >= 0)
                                    ? lastDimIterStride : destSize.GetStride(d);

                    if (itD.MoveNext()) {
                        var val = itD.Current;
                        destHighDims += (val - oldIdx) * destStride;
                        inHighDims += inSize.GetStride(d);
                        break;
                    } else {
                        itD.Reset();
                        itD.MoveNext();  // assuming this succeeds
                        destHighDims += (itD.Current - oldIdx) * destStride;
                        inHighDims -= (inSize[d] - 1) * inSize.GetStride(d);
                        d++;
                    }
                }
                if (d == nrIterDims) return;
            }
        }


    }
}
