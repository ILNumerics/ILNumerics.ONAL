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
#pragma warning disable 1570, 1591
using ILNumerics.Core.Functions.Builtin;
using ILNumerics.Core.MemoryLayer;
using System;
using System.Runtime.InteropServices;
using System.Security;
using ILNumerics.Core.Native;
using static ILNumerics.Core.Functions.Builtin.MathInternal;
using System.ComponentModel;
using System.Collections.Generic;
using ILNumerics.Core.Internal;
using ILNumerics.Core.StorageLayer;

namespace ILNumerics.F2NET {
    /// <summary>
    /// Lapack interface implementation, specialized for the official netlib.org Lapack package, as a direct 1:1 translation utilizing ILNumerics.F2NET. 
    /// </summary>
    /// 
    public unsafe class ManagedFFTPACK5 : IFFT {

        /* Currently (version 6.0) ManagedFFTPACK5 utilizes RFFTMI and CFFTMI from FFTPACK5 only! 
         * TODO: There is significant performance potential by connecting to specialized transforms: 1D, 2D, even / odd transforms! 
         */

        #region attributes
        F2NETDescriptorCache<float> fcomplex_descriptorCache = new F2NETDescriptorCache<float>(l => 2 * l + (long)(Math.Log((double)l) / Math.Log(2)) + 4);
        //F2NETDescriptorCache<float> Single_descriptorCache = new F2NETDescriptorCache<float>(l => l + (long)(Math.Log((double)l) / Math.Log(2)) + 4);
        F2NETDescriptorCache<double> complex_descriptorCache = new F2NETDescriptorCache<double>(l => 2 * l + (long)(Math.Log((double)l) / Math.Log(2)) + 4);
        //F2NETDescriptorCache<double> Double_descriptorCache = new F2NETDescriptorCache<double>(l => l + (long)(Math.Log((double)l) / Math.Log(2)) + 4); 
        #endregion

        public ManagedFFTPACK5() {
            Init();
        }

        public bool CachePlans => true;

        public bool SpeedyHermitian => false;  // TODO: use specialized transforms from FFTPACK5!
        public static void Init() {
        }

        public enum TransformTypes {
            Complex_Double,
            Real_Double,
            Complex_Single,
            Real_Single
        }
        #region HYCALPER LOOPSTART 
        /*
   <hycalper>
    <type>
        <source locate="here">
            float
        </source>
        <destination>double</destination>
   </type>
    <type>
        <source locate="here">
            Single
        </source>
        <destination>Double</destination>
   </type>
    <type>
        <source locate="here">
            FComplex
        </source>
        <destination>complex</destination>
   </type>
    <type>
        <source locate="here">
            fcomplex
        </source>
        <destination>complex</destination>
   </type>
    <type>
        <source locate="here">
            FFTPACK5
        </source>
        <destination>DFFTPACK5</destination>
   </type>
    </hycalper>
        */
        public Array<fcomplex> FFTForward1D(InArray<fcomplex> A, uint dim) {
            using (Scope.Enter(A)) {
                if (object.Equals(A, null)) {
                    throw new ArgumentNullException(nameof(A));
                }
                if (A.IsEmpty) {
                    return MathInternal.empty<fcomplex>(A.Size);
                }
                if (dim >= A.Size.NumberOfDimensions || A.Size[dim] == 1) {
                    return A.C;
                }
                // prepare output array 
                Array<fcomplex> ret = A.C;
                ret.Detach();

                long* outBSD = Core.StorageLayer.Storage<double>.Context.TmpBuffer1000;
                long lSizeDescr = 0;
                var descriptor = (float*)fcomplex_descriptorCache.getDescriptor(A.Size[dim], out lSizeDescr, TransformTypes.Complex_Single).Pointer;
                long insideIterLen = 0, insideIterInc = 0, nStride = 0;
                prepareIteration1D(ret.Size, dim, outBSD, out insideIterLen, out insideIterInc, out nStride);

                // create iterator for outside iterations
                // order is not that important here (inplace iteration), but the size iteration requires a continous layout order
                var itOrder = ret.Size.IsContinuous ? ret.S.StorageOrder : Settings.DefaultStorageOrder;
                using (var it = new Iterators.StridedSizeIterator(outBSD, itOrder)) {
                    // do the transform(s)

                    fcomplex* retArr = (fcomplex*)ret.GetHostPointerForWrite();
                    int iStride = (int)nStride, iN = (int)A.Size[dim], iDescLen = (int)lSizeDescr;
                    int iIntIterLen = (int)insideIterLen, iIntIterInc = (int)insideIterInc;
                    int iCLen = (int)ret.S.GetElementSpan() + 1, iWorkLen = 0, iErr = 0;
                    MemoryHandle workHandle = null;
                    iWorkLen = 2 * iN * iIntIterLen;
                    workHandle = New<float>(iWorkLen, 0, false);
                    var work = (float*)workHandle.Pointer;
                    while (it.MoveNext()) {
                        int iCLenCur = (int)(iCLen - it.Current);
                        ILNumerics.F2NET.FFTPACK5._l6sfv99g(

                                    ref iIntIterLen, ref iIntIterInc, ref iN, ref iStride,
                                    retArr + it.Current, ref iCLenCur,
                                    descriptor, ref iDescLen,
                                    work, ref iWorkLen, ref iErr);
                        if (iErr != 0)
                            throw new InvalidOperationException($"Error performing FFT (outer start index:{it.Current}). Error returned from cfftmf: '{iErr}'.");
                    }
                    free<float>(workHandle, 0);
                    return ret * new fcomplex((float)iN, 0);
                }
            }

        }

        public Array<fcomplex> FFTForward1D(InArray<float> A, uint dim) {
            return FFTForward1D(tofcomplex(A), dim);
        }
        public Array<fcomplex> FFTBackward1D(InArray<fcomplex> A, uint dim) {
            using (Scope.Enter(A)) {
                if (object.Equals(A, null)) {
                    throw new ArgumentNullException(nameof(A));
                }
                if (A.IsEmpty) {
                    return MathInternal.empty<fcomplex>(A.Size);
                }
                if (dim >= A.Size.NumberOfDimensions || A.Size[dim] == 1) {
                    return A.C;
                }
                // prepare output array 
                Array<fcomplex> ret = A.C;
                ret.Detach();

                long* outBSD = Core.StorageLayer.Storage<double>.Context.TmpBuffer1000;
                long lSizeDescr = 0;
                var descriptor = (float*)fcomplex_descriptorCache.getDescriptor(A.Size[dim], out lSizeDescr, TransformTypes.Complex_Single).Pointer;
                long insideIterLen = 0, insideIterInc = 0, nStride = 0;
                prepareIteration1D(ret.Size, dim, outBSD, out insideIterLen, out insideIterInc, out nStride);

                // create iterator for outside iterations
                // order is not that important here (inplace iteration), but the size iteration requires a continous layout order
                var itOrder = ret.Size.IsContinuous ? ret.S.StorageOrder : Settings.DefaultStorageOrder;
                using (var it = new Iterators.StridedSizeIterator(outBSD, itOrder)) {

                    // do the transform(s)
                    fcomplex* retArr = (fcomplex*)ret.GetHostPointerForWrite();
                    int iStride = (int)nStride, iN = (int)A.Size[dim], iDescLen = (int)lSizeDescr;
                    int iIntIterLen = (int)insideIterLen, iIntIterInc = (int)insideIterInc;
                    int iCLen = (int)ret.S.GetElementSpan() + 1, iWorkLen = 0, iErr = 0;
                    MemoryHandle workHandle = null;

                    iWorkLen = 2 * iN * iIntIterLen;
                    workHandle = New<float>(iWorkLen, 0, false);
                    var work = (float*)workHandle.Pointer;
                    while (it.MoveNext()) {
                        int iCLenCur = (int)(iCLen - it.Current);
                        ILNumerics.F2NET.FFTPACK5._gw6i40vj(

                                    ref iIntIterLen, ref iIntIterInc, ref iN, ref iStride,
                                    retArr + it.Current, ref iCLenCur,
                                    descriptor, ref iDescLen,
                                    work, ref iWorkLen, ref iErr);
                        if (iErr != 0)
                            throw new InvalidOperationException($"Error performing FFT (outer start index:{it.Current}). Error returned from cfftmb: '{iErr}'.");
                    }
                    free<float>(workHandle, 0);
                    return ret * (1f / new fcomplex(iN, 0));
                }
            }
        }

        public Array<float> FFTBackwSym1D(InArray<fcomplex> A, uint dim) {
            return real(FFTBackward1D(A, dim));
        }

        public Array<fcomplex> FFTForward(InArray<fcomplex> A, uint nDims) {
            if (object.Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            if (nDims > A.S.NumberOfDimensions) {
                nDims = A.S.NumberOfDimensions;
            }
            if (nDims == 0) {
                return A;
            }
            Array<fcomplex> ret = A.C;
            for (uint i = 0; i < nDims; i++) {
                ret.a = FFTForward1D(ret, i);
            }
            return ret;
        }

        public Array<fcomplex> FFTForward(InArray<float> A, uint nDims) {
            return FFTForward(tofcomplex(A), nDims);
        }

        public Array<fcomplex> FFTBackward(InArray<fcomplex> A, uint nDims) {
            if (object.Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            if (nDims > A.S.NumberOfDimensions) {
                nDims = A.S.NumberOfDimensions;
            }
            if (nDims == 0) {
                return A;
            }
            Array<fcomplex> ret = A.C;
            for (uint i = 0; i < nDims; i++) {
                ret.a = FFTBackward1D(ret, i);
            }
            return ret;
        }

        public Array<float> FFTBackwSym(InArray<fcomplex> A, uint nDims) {
            return real(FFTBackward(A, nDims));
        }

        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
        /*

        */
        public Array<complex> FFTForward1D(InArray<complex> A, uint dim) {
            using (Scope.Enter(A)) {
                if (object.Equals(A, null)) {
                    throw new ArgumentNullException(nameof(A));
                }
                if (A.IsEmpty) {
                    return MathInternal.empty<complex>(A.Size);
                }
                if (dim >= A.Size.NumberOfDimensions || A.Size[dim] == 1) {
                    return A.C;
                }
                // prepare output array 
                Array<complex> ret = A.C;
                ret.Detach();

                long* outBSD = Core.StorageLayer.Storage<double>.Context.TmpBuffer1000;
                long lSizeDescr = 0;
                var descriptor = (double*)complex_descriptorCache.getDescriptor(A.Size[dim], out lSizeDescr, TransformTypes.Complex_Double).Pointer;
                long insideIterLen = 0, insideIterInc = 0, nStride = 0;
                prepareIteration1D(ret.Size, dim, outBSD, out insideIterLen, out insideIterInc, out nStride);

                // create iterator for outside iterations
                // order is not that important here (inplace iteration), but the size iteration requires a continous layout order
                var itOrder = ret.Size.IsContinuous ? ret.S.StorageOrder : Settings.DefaultStorageOrder;
                using (var it = new Iterators.StridedSizeIterator(outBSD, itOrder)) {
                    // do the transform(s)

                    complex* retArr = (complex*)ret.GetHostPointerForWrite();
                    int iStride = (int)nStride, iN = (int)A.Size[dim], iDescLen = (int)lSizeDescr;
                    int iIntIterLen = (int)insideIterLen, iIntIterInc = (int)insideIterInc;
                    int iCLen = (int)ret.S.GetElementSpan() + 1, iWorkLen = 0, iErr = 0;
                    MemoryHandle workHandle = null;
                    iWorkLen = 2 * iN * iIntIterLen;
                    workHandle = New<double>(iWorkLen, 0, false);
                    var work = (double*)workHandle.Pointer;
                    while (it.MoveNext()) {
                        int iCLenCur = (int)(iCLen - it.Current);
                        ILNumerics.F2NET.DFFTPACK5._l6sfv99g(

                                    ref iIntIterLen, ref iIntIterInc, ref iN, ref iStride,
                                    retArr + it.Current, ref iCLenCur,
                                    descriptor, ref iDescLen,
                                    work, ref iWorkLen, ref iErr);
                        if (iErr != 0)
                            throw new InvalidOperationException($"Error performing FFT (outer start index:{it.Current}). Error returned from cfftmf: '{iErr}'.");
                    }
                    free<double>(workHandle, 0);
                    return ret * new complex((double)iN, 0);
                }
            }

        }

        public Array<complex> FFTForward1D(InArray<double> A, uint dim) {
            return FFTForward1D(tocomplex(A), dim);
        }
        public Array<complex> FFTBackward1D(InArray<complex> A, uint dim) {
            using (Scope.Enter(A)) {
                if (object.Equals(A, null)) {
                    throw new ArgumentNullException(nameof(A));
                }
                if (A.IsEmpty) {
                    return MathInternal.empty<complex>(A.Size);
                }
                if (dim >= A.Size.NumberOfDimensions || A.Size[dim] == 1) {
                    return A.C;
                }
                // prepare output array 
                Array<complex> ret = A.C;
                ret.Detach();

                long* outBSD = Core.StorageLayer.Storage<double>.Context.TmpBuffer1000;
                long lSizeDescr = 0;
                var descriptor = (double*)complex_descriptorCache.getDescriptor(A.Size[dim], out lSizeDescr, TransformTypes.Complex_Double).Pointer;
                long insideIterLen = 0, insideIterInc = 0, nStride = 0;
                prepareIteration1D(ret.Size, dim, outBSD, out insideIterLen, out insideIterInc, out nStride);

                // create iterator for outside iterations
                // order is not that important here (inplace iteration), but the size iteration requires a continous layout order
                var itOrder = ret.Size.IsContinuous ? ret.S.StorageOrder : Settings.DefaultStorageOrder;
                using (var it = new Iterators.StridedSizeIterator(outBSD, itOrder)) {

                    // do the transform(s)
                    complex* retArr = (complex*)ret.GetHostPointerForWrite();
                    int iStride = (int)nStride, iN = (int)A.Size[dim], iDescLen = (int)lSizeDescr;
                    int iIntIterLen = (int)insideIterLen, iIntIterInc = (int)insideIterInc;
                    int iCLen = (int)ret.S.GetElementSpan() + 1, iWorkLen = 0, iErr = 0;
                    MemoryHandle workHandle = null;

                    iWorkLen = 2 * iN * iIntIterLen;
                    workHandle = New<double>(iWorkLen, 0, false);
                    var work = (double*)workHandle.Pointer;
                    while (it.MoveNext()) {
                        int iCLenCur = (int)(iCLen - it.Current);
                        ILNumerics.F2NET.DFFTPACK5._gw6i40vj(

                                    ref iIntIterLen, ref iIntIterInc, ref iN, ref iStride,
                                    retArr + it.Current, ref iCLenCur,
                                    descriptor, ref iDescLen,
                                    work, ref iWorkLen, ref iErr);
                        if (iErr != 0)
                            throw new InvalidOperationException($"Error performing FFT (outer start index:{it.Current}). Error returned from cfftmb: '{iErr}'.");
                    }
                    free<double>(workHandle, 0);
                    return ret * (1f / new complex(iN, 0));
                }
            }
        }

        public Array<double> FFTBackwSym1D(InArray<complex> A, uint dim) {
            return real(FFTBackward1D(A, dim));
        }

        public Array<complex> FFTForward(InArray<complex> A, uint nDims) {
            if (object.Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            if (nDims > A.S.NumberOfDimensions) {
                nDims = A.S.NumberOfDimensions;
            }
            if (nDims == 0) {
                return A;
            }
            Array<complex> ret = A.C;
            for (uint i = 0; i < nDims; i++) {
                ret.a = FFTForward1D(ret, i);
            }
            return ret;
        }

        public Array<complex> FFTForward(InArray<double> A, uint nDims) {
            return FFTForward(tocomplex(A), nDims);
        }

        public Array<complex> FFTBackward(InArray<complex> A, uint nDims) {
            if (object.Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            if (nDims > A.S.NumberOfDimensions) {
                nDims = A.S.NumberOfDimensions;
            }
            if (nDims == 0) {
                return A;
            }
            Array<complex> ret = A.C;
            for (uint i = 0; i < nDims; i++) {
                ret.a = FFTBackward1D(ret, i);
            }
            return ret;
        }

        public Array<double> FFTBackwSym(InArray<complex> A, uint nDims) {
            return real(FFTBackward(A, nDims));
        }


#endregion HYCALPER AUTO GENERATED CODE

        /// <summary>
        /// Releases all memory cached in plans. This method is NOT THREADSAFE!
        /// </summary>
        public void FreePlans() {
            fcomplex_descriptorCache.ClearCache();
            //Double_descriptorCache.ClearCache();
            //Single_descriptorCache.ClearCache();
            complex_descriptorCache.ClearCache();
        }

        private unsafe static void prepareIteration1D(Size size, uint d, long* outsideIterationBSD, out long insideIterLen, out long insideIterInc, out long nStride) {

            System.Diagnostics.Debug.Assert(!Equals(size, null) && size.NumberOfDimensions > 0 && size.NumberOfElements > 0, "handle special array shapes separately!");
            System.Diagnostics.Debug.Assert(size[d] != 1, "handle singleton working dimension separately!");
            // fakes a two element int array: [0, stride_d]

            nStride = size.GetStride(d);
            int dimLen = (int)size[d];

            if (size.NumberOfDimensions == 1) {
                // outside iteration not needed (means: is considered as "numpy scalar")
                outsideIterationBSD[0] = 0;
                outsideIterationBSD[1] = 1;
                outsideIterationBSD[2] = 0;
                // inside iteration is straight forward
                insideIterInc = 1;
                insideIterLen = 1;
                return;
            }

            if (size.IsContinuous) {
                if (d == 0 || d == size.NumberOfDimensions - 1) // size.GetStride(d) == 1) 
                {
                    // working along the leading or the last dimension: all done within the MKL
                    outsideIterationBSD[0] = 0; // outside iterator assumes a scalar array by ndims = 0
                    outsideIterationBSD[1] = 1;
                    outsideIterationBSD[2] = 0;

                    if (nStride == 1) {
                        insideIterLen = (int)(size.NumberOfElements / dimLen);
                        insideIterInc = dimLen;
                    } else {
                        insideIterLen = (int)size.GetStride(d);
                        insideIterInc = 1;
                    }
                    return;
                    //return DescriptorCache.GetDescriptor(precision, domain, 1, &dimLen, (int*)&stride, nrOfInnerTransforms, distance, inplace);
                } else {
                    // d is some arbitrary dimension within the dimension range (not the first, not the last)
                    insideIterInc = 1;
                    insideIterLen = (int)size.GetStride(d);

                    // column major: all 0...d dims -> MKL; d+1...n-1 dims -> outside
                    // row major; all 0..d-1 dims -> outside; d...n-1 dims -> MKL
                    #region outside iteration
                    System.Diagnostics.Debug.Assert(d > 0 && d < size.NumberOfDimensions - 1);  // otherwise the shortcut above would have been kicked in

                    outsideIterationBSD[0] = 1;
                    outsideIterationBSD[1] = size.MergeNextToEnd(d, ref outsideIterationBSD[4]);

                    outsideIterationBSD[2] = 0;
                    outsideIterationBSD[3] = outsideIterationBSD[1];
                    #endregion

                    //return DescriptorCache.GetDescriptor(precision, domain, 1, &dimLen, (int*)&stride, nrOfInnerTransforms, distance, inplace);
                    return;
                }
            } else {
                // arbitrary, non-contiguous storage order 
                System.Diagnostics.Debug.Assert(size.NumberOfDimensions >= 2);
                // find inner iteration dim: we let MKL handle the longest non-working dim via repeated transforms. 
                uint k = 0;
                #region determine 2nd, longest dim for inside iteration
                if (k == d) {
                    k++; // we have checked above that more dims exist
                }
                long curLongest = 0;
                for (uint i = 0; i < size.NumberOfDimensions; i++) {
                    if (i != d && i != k && size[i] > curLongest) {
                        curLongest = size[i];
                        k = i;
                    }
                }
                #endregion

                #region outside iteration is done over all but k and d
                outsideIterationBSD[1] = 1;
                for (uint i = 0, o = 0; i < size.NumberOfDimensions; i++) {
                    if (i == k || i == d) continue;

                    outsideIterationBSD[3 + o] = size[i];
                    outsideIterationBSD[1] *= size[i];
                    outsideIterationBSD[3 + size.NumberOfDimensions - 2 + o] = size.GetStride(i);
                    o++;
                }
                outsideIterationBSD[0] = size.NumberOfDimensions - 2;
                outsideIterationBSD[2] = 0;
                #endregion
                insideIterInc = Math.Max(1, (int)size.GetStride(k));
                insideIterLen = size[k];
                // return DescriptorCache.GetDescriptor(precision, domain, 1, &dimLen, (int*)&stride, (int)size[k], distance, inplace);
                return;
            }
        }
    }
}
