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
using ILNumerics.Core.Functions.Builtin;
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security;
using System.Text;

namespace ILNumerics.Core.Global {

    /// <summary>
    /// Helper functions for internal use.
    /// </summary>
    public static class Helper {

        internal const uint REORDER_THRESHOLD_WRITETO_OTHERSTORAGE = 40;

        internal static double GetScalingForPrint(double min, double max) {
            try {
                double scaling = Math.Max(Math.Abs(min), Math.Abs(max));

                if (scaling == 0 || (scaling > 1e-1 && scaling < 1e1))
                    scaling = 1;
                scaling = Math.Pow(10, Math.Floor(Math.Log10(scaling)));
                if (scaling < 1000 && scaling >= 1)
                    scaling = 1;
                return scaling;
            } catch (Exception) {
                return 1.0;
            }
        }

        internal static Array<T> ArrayAllDimsSameExcept<T>(T val, Storage<T> storageA, uint dim, int dimLen, bool keepdim) {
            using (Scope.Enter()) {
                Array<long> dims = storageA.shape_get().GetLocalArray();
                dims[dim] = dimLen;
                Array<T> ret = MathInternal.array<T>(val, dims); 
                if (!keepdim) {
                    ret.S.RemoveDimension(dim); 
                }
                return ret; 
            }
        }

        /// <summary>
        /// Computes the output shape from A and B. 
        /// </summary>
        /// <param name="A">Size of input array A.</param>
        /// <param name="B">Size of input array B.</param>
        /// <param name="buffer">buffer of long* with room for at least 3 + <see cref="Size.MaxNumberOfDimensions"/> elements.</param>
        /// <param name="ndims">[out] broadcasted number of dimension on return.</param>
        /// <remarks>This function sets buffer[0] (ndims), buffer[1] (nelem) and the dimension lengths (buffer + 2 + [1...ndims]) only. 
        /// It checks for matching broadcasting sizes and throws an exception on error.</remarks>
        internal static unsafe void BC_broadcastOutDims(Size A, Size B, long* buffer, out uint ndims) {
            // the output sizes are stored into buffer, starting from element 3 - as for a regular BSD. 
            // However, later in the process, they might be reordered again (reversed) for iteration.

            ndims = Math.Max(A.NumberOfDimensions, B.NumberOfDimensions);

            long* dims = buffer + 3;
            buffer[1] = 1;
            if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                // Column Major
                for (uint i = 0; i < ndims; i++) {
                    var a = A[i];
                    var b = B[i];
                    if (a != b && a != 1 && b != 1) {
                        throw new ArgumentException($"Arrays of size {A.ToString()} and {B.ToString()} cannot be broadcast together. Current array style: {Settings.ArrayStyle}.");
                    }
                    dims[i] = a != 1 ? a : b;  // was: Math.Max(a, b); <- this would ignore empties!
                    buffer[1] *= dims[i];
                }
            } else {
                // numpy / row major broadcasting
                for (uint i = 0; i < ndims; i++) {
                    var a = A[A.NumberOfDimensions - 1 - i]; // uint wrapping around intended!
                    var b = B[B.NumberOfDimensions - 1 - i]; // uint wrapping around intended!
                    if (a != b && a != 1 && b != 1) {
                        throw new ArgumentException($"Arrays of size {A.ToString()} and {B.ToString()} cannot be broadcast together. Current array style: {Settings.ArrayStyle}.");
                    }
                    a = a != 1 ? a : b;  // was: Math.Max(a, b); <- this would ignore empties!
                    dims[ndims - 1 - i] = a; 
                    buffer[1] *= a;
                }
            }
            buffer[0] = ndims; 
        }
        /// <summary>
        /// Computes the output shape from A. ILNumericsV4 array style. This is the scalar version of BC_bcI4.
        /// </summary>
        /// <param name="A">BSD of input array A.</param>
        /// <param name="ndims">(ignored)</param>
        /// <param name="nsdims">number of non-singleton dimensions</param>
        /// <param name="broadcasting">returns whether this shape performs broadcasting or not</param>
        /// <param name="buffer">buffer of long* with room for at least 3 + <see cref="Size.MaxNumberOfDimensions"/> elements.</param>
        /// <remarks>This function sets buffer[0] (ndims), buffer[1] (nelem) and the dimension lengths (buffer + 2 + [1...ndims]) only. 
        /// It copies the partial BSD info from the only non-scalar input of a binary array operation.
        /// Further, broadcasting and nsdims info is set. 
        /// </remarks>
        public static unsafe void BC_bcI4s(long* A, long* buffer, uint ndims, ref uint nsdims, ref bool broadcasting) {

            // the output sizes are stored into buffer, starting from element 3 - as for a regular BSD. 
            ndims = (uint)A[0];

            long* dims = buffer + 3;
            buffer[1] = 1;
            // Column Major
            nsdims = 0; 
            for (uint i = 0; i < ndims; i++) {
                dims[i] = A[3 + i]; 
                buffer[1] *= dims[i];
                if (dims[i] != 1) nsdims++; 
            }
            buffer[0] = ndims;
            broadcasting = false; 
        }

        /// <summary>
        /// Computes the output shape from A and B. ILNumericsV4 array style 
        /// </summary>
        /// <param name="A">BSD of input array A.</param>
        /// <param name="B">BSD of input array B.</param>
        /// <param name="buffer">buffer of long* with room for at least 3 + <see cref="Size.MaxNumberOfDimensions"/> elements.</param>
        /// <param name="ndims">(broadcasted) number of dimensions of result.</param>
        /// <param name="nsdims">number of non-singleton dimensions</param>
        /// <param name="broadcasting">returns whether this shape performs broadcasting or not</param>
        /// <remarks>This function sets buffer[0] (ndims), buffer[1] (nelem) and the dimension lengths (buffer + 2 + [1...ndims]) only. 
        /// It checks for matching broadcasting sizes and throws an exception on error.</remarks>
        /// <exception cref="ArgumentException">if sizes of <paramref name="A"/> and <paramref name="B"/> are not broadcasting compatible.</exception>
        public static unsafe void BC_bcI4(long* A, long* B, long* buffer, uint ndims, ref uint nsdims, ref bool broadcasting) {

            // the output sizes are stored into buffer, starting from element 3 - as for a regular BSD. 
            // However, later in the process, they might be reordered again (reversed) for iteration.

            ndims = (uint)Math.Max(A[0], B[0]);

            long* dims = buffer + 3;
            buffer[1] = 1;
            // Column Major
            nsdims = 0; 
            for (uint i = 0; i < ndims; i++) {
                var a = A[0] > i ? A[3 + i] : 1;
                var b = B[0] > i ? B[3 + i] : 1;
                if (a != b) {
                    if (a != 1 && b != 1) {
                        throw new ArgumentException($"Arrays of size [{Helper.dims2string(A + 3, (uint)A[0])}] and [{Helper.dims2string(B + 3, (uint)B[0])}] cannot operate together. See: broadcasting rules. Current array style: {nameof(ArrayStyles.ILNumericsV4)}.");
                    }
                    broadcasting = true; 
                }
                dims[i] = a != 1 ? a : b;  // was: Math.Max(a, b); <- but this would ignore empties!
                buffer[1] *= dims[i];
                if (dims[i] != 1) nsdims++; 
            }
            buffer[0] = ndims;
        }

        /// <summary>
        /// Computes the output shape from A. Numpy array style. Scalar version of BC_bcNp.
        /// </summary>
        /// <param name="A">BSD of input array A.</param>
        /// <param name="buffer">buffer of long* with room for at least 3 + <see cref="Size.MaxNumberOfDimensions"/> elements.</param>
        /// <param name="ndims">(ignored)</param>
        /// <param name="nsdims">number of non-singleton dimensions</param>
        /// <param name="broadcasting">returns whether this shape performs broadcasting or not</param>
        /// <remarks>This function sets buffer[0] (ndims), buffer[1] (nelem) and the dimension lengths (buffer + 2 + [1...ndims]) only. 
        /// It checks for matching broadcasting sizes and throws an exception on error.</remarks>
        public static unsafe void BC_bcNps(long* A, long* buffer, uint ndims, ref uint nsdims, ref bool broadcasting) {

            // the output sizes are stored into buffer, starting from element 3 - as for a regular BSD. 
            // However, later in the process, they might be reordered again (reversed) for iteration.

            ndims = (uint)A[0];

            long* dims = buffer + 3;
            buffer[1] = 1;
            nsdims = 0; 
            // numpy / row major broadcasting
            for (uint i = 0; i < ndims; i++) {
                var a = A[3 - 1 + A[0] - i]; 
                dims[ndims - 1 - i] = a;
                buffer[1] *= a;
                if (a != 1) nsdims++; 
            }
            buffer[0] = ndims;
            broadcasting = false;
        }
        /// <summary>
        /// Computes the output shape from A and B. Numpy array style.
        /// </summary>
        /// <param name="A">BSD of input array A.</param>
        /// <param name="B">BSD of input array B.</param>
        /// <param name="buffer">buffer of long* with room for at least 3 + <see cref="Size.MaxNumberOfDimensions"/> elements.</param>
        /// <param name="ndims">(broadcasted) number of dimensions of result.</param>
        /// <param name="nsdims">number of non-singleton dimensions</param>
        /// <param name="broadcasting">returns whether this shape performs broadcasting or not</param>
        /// <remarks>This function sets buffer[0] (ndims), buffer[1] (nelem) and the dimension lengths (buffer + 2 + [1...ndims]) only. 
        /// It checks for matching broadcasting sizes and throws an exception on error.</remarks>
        /// <exception cref="ArgumentException">if sizes of <paramref name="A"/> and <paramref name="B"/> are not broadcasting compatible.</exception>
        public static unsafe void BC_bcNp(long* A, long* B, long* buffer, uint ndims, ref uint nsdims, ref bool broadcasting) {

            // the output sizes are stored into buffer, starting from element 3 - as for a regular BSD. 
            // However, later in the process, they might be reordered again (reversed) for iteration.

            ndims = (uint)Math.Max(A[0], B[0]);

            long* dims = buffer + 3;
            buffer[1] = 1;
            nsdims = 0; 
            // numpy / row major broadcasting
            for (uint i = 0; i < ndims; i++) {
                var a = A[0] > i ? A[3 - 1 + A[0] - i] : 1;
                var b = B[0] > i ? B[3 - 1 + B[0] - i] : 1;
                if (a != b) {
                    if (a != 1 && b != 1) {
                        throw new ArgumentException($"Arrays of size [{Helper.dims2string(A + 3, (uint)A[0])}] and [{Helper.dims2string(B + 3, (uint)B[0])}] cannot operate together. See: broadcasting rules. Current array style: {nameof(ArrayStyles.numpy)}.");
                    }
                    broadcasting = true; 
                }
                a = a != 1 ? a : b;  // was: Math.Max(a, b); <- this would ignore empties!
                dims[ndims - 1 - i] = a;
                buffer[1] *= a;
                if (a != 1) nsdims++; 
            }
            buffer[0] = ndims;
        }
        /// <summary>
        /// Completes metadata configuration for output array.
        /// </summary>
        /// <param name="bsd">Start of the BSD of the ouput array.</param>
        /// <param name="order">Storage order for the output. Not used.</param>
        /// <param name="leadingDim">Leading dimension: the dimension where elements will be sequentially stored along. Subsequent dimensions are ordered accordingly in increasing dimension index order.</param>
        public static unsafe void BC_configureFinalize(long* bsd, StorageOrders? order, int leadingDim = -1) {
            var n = bsd[0];
            var se = bsd + 3 + n;
            var st = 1L;
            var l = Math.Max(leadingDim, 0); 
            for (int i = 0; i < n; i++) {
                var imod = (i + l) % n; 
                var d = bsd[3 + imod]; 
                se[imod] = (d == 1) ? 0 : st;
                st *= d; 
            }
            bsd[2] = 0; 
        }

        /// <summary>
        /// Prepares all dimension and strides array for efficient iteration in binary, broadcasting operators.
        /// </summary>
        /// <typeparam name="T_In">Element type. Used to determine the element size / strides.</typeparam>
        /// <typeparam name="T_Out">Output element type. Used to determine strides.</typeparam>
        /// <param name="sizeA"></param>
        /// <param name="sizeB"></param>
        /// <param name="buffer">Input / output: predefined array with dimension lengths. Output: holds dimension lengths and strides, ready for iteration.</param>
        /// <param name="reverse">True: reverses the dimensions / strides of the returned BSD.</param>
        /// <remarks><para>This functions takes the dimension lengths in <paramref name="buffer"/> and completes the buffer 
        /// with * out strides, * A strides and * B strides for binary operator iteration.</para>
        /// <para>All strides are multiplied with the element size of <typeparamref name="T_In"/> in bytes.</para>
        /// <para>All dims are decreased by 1 for easier iteration later.</para>
        /// <para><paramref name="buffer"/> is of minimum length 3 + 7 * 4, for [ndims, nelem, offs] + outdims + outstrides ++ stridesA ++ stridesB. On entry, 
        /// only elements 0, 1 and 3...[ndims-1] are expected. other info (strides) are completed and returned within this function.</para>
        /// <para>If <paramref name="reverse"/> is true all dimensions and strides are reversed. This corresponds to an output array in row major order.</para></remarks>
        internal static unsafe void BC_prepareIterationDims<T_In, T_Out>(Size sizeA, Size sizeB, long* buffer, bool reverse) {


            uint ndims = (uint)buffer[0];
            uint elemSize = Storage<T_In>.SizeOfT;
            long* dims = buffer + 3;
            long* stridesOut = dims + ndims;
            long stride = Storage<T_Out>.SizeOfT;

            if (sizeA.IsContinuous && sizeB.StorageOrder == sizeA.StorageOrder && sizeA.IsSameShape(sizeB)) {

                // both inputs are continous and have the same storage order. moving all elements into the first dimension
                // can pot. save lots of overhead in the iteration! 

                // TODO: check if we can set ndims to 1 here! Is this used / working with the iteration in the caller? 
                dims[0] = buffer[1] - 1; 
                stridesOut[0] = stride;
                stridesOut[ndims] = elemSize; 
                stridesOut[ndims * 2] = elemSize;
                for (int i = 1; i < ndims; i++) {
                    dims[i] = 0; // -1 ! 
                    stridesOut[i] = 0;
                    stridesOut[i + ndims] = 0;
                    stridesOut[i + ndims * 2] = 0;
                }
                return; 
            }

            if (!reverse) {
                // out array is column major
                for (uint i = 0; i < ndims; i++) {
                    stridesOut[i] = stride;
                    stride *= dims[i];
                    dims[i]--;
                    // which broadcasting mode is active?
                    if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                        // dims are aligned at start, counting forwards
                        stridesOut[ndims + i] = sizeA.GetStride(i) * elemSize;
                        stridesOut[ndims * 2 + i] = sizeB.GetStride(i) * elemSize;
                    } else {
                        // dims are aligned at end, counting forwards
                        stridesOut[ndims + i] = sizeA.GetStride(sizeA.NumberOfDimensions - ndims + i) * elemSize;        // sic: uint wrapping intended!
                        stridesOut[ndims * 2 + i] = sizeB.GetStride(sizeB.NumberOfDimensions - ndims + i) * elemSize;
                    }
                }
            } else {
                // out array is row major
                for (uint i = 0; i < ndims; i++) {
                    stridesOut[i] = stride;
                    stride *= dims[ndims - i - 1];
                    // dims[i]--;  decreasing moved below
                    if (Settings.ArrayStyle == ArrayStyles.numpy) {
                        // dims are aligned at end, counting backwards
                        stridesOut[ndims + i] = sizeA.GetStride(sizeA.NumberOfDimensions - i - 1) * elemSize;  // sic: uint wrapping intended!
                        stridesOut[ndims * 2 + i] = sizeB.GetStride(sizeB.NumberOfDimensions - i - 1) * elemSize;
                    } else {
                        // dims are aligned at start, counting backwards
                        stridesOut[ndims + i] = sizeA.GetStride(ndims - 1 - i) * elemSize;
                        stridesOut[ndims * 2 + i] = sizeB.GetStride(ndims - 1 - i) * elemSize;
                    }
                }
                for (uint i = 0; i < ndims; i++) {
                    dims[i]--;
                }
                // must reverse dims for row major
                while (stridesOut-- > dims) {
                    var tmp = *dims;
                    *dims++ = *stridesOut;
                    *stridesOut = tmp;
                }
            }
            //buffer[0] = ndims;
            //buffer[1] = stride / elemSize; 
            buffer[2] = 0;
        }
        /// <summary>
        /// Prepares all dimension and strides array for efficient iteration in binary, broadcasting operators.
        /// </summary>
        /// <typeparam name="T_In1">Element type, input 1. Used to determine the element size / strides.</typeparam>
        /// <typeparam name="T_In2">Element type, input 2. Used to determine the element size / strides.</typeparam>
        /// <typeparam name="T_Out">Output element type. Used to determine strides.</typeparam>
        /// <param name="sizeA"></param>
        /// <param name="sizeB"></param>
        /// <param name="buffer">Input / output: predefined array with dimension lengths. Output: holds dimension lengths and strides, ready for iteration.</param>
        /// <param name="reverse">True: reverses the dimensions / strides of the returned BSD.</param>
        /// <remarks><para>This functions takes the dimension lengths in <paramref name="buffer"/> and completes the buffer 
        /// with * out strides, * A strides and * B strides for binary operator iteration.</para>
        /// <para>All strides are multiplied with the element size of <typeparamref name="T_In1"/> / <typeparamref name="T_In2"/> resp., in bytes.</para>
        /// <para>All dims are decreased by 1 for easier iteration later.</para>
        /// <para><paramref name="buffer"/> is of minimum length 3 + 7 * 4, for [ndims, nelem, offs] + outdims + outstrides ++ stridesA ++ stridesB. On entry, 
        /// only elements 0, 1 and 3...[ndims-1] are expected. other info (strides) are completed and returned within this function.</para>
        /// <para>If <paramref name="reverse"/> is true all dimensions and strides are reversed. This corresponds to an output array in row major order.</para></remarks>
        internal static unsafe void BC_prepareIterationDims<T_In1, T_In2, T_Out>(Size sizeA, Size sizeB, long* buffer, bool reverse) {

            uint ndims = (uint)buffer[0];
            uint elemSizeA = Storage<T_In1>.SizeOfT;
            uint elemSizeB = Storage<T_In2>.SizeOfT;
            long* dims = buffer + 3;
            long* stridesOut = dims + ndims;
            //long* stridesIn1 = stridesOut + ndims;
            //long* stridesIn2 = stridesIn1 + ndims; 
            long stride = Storage<T_Out>.SizeOfT;

            if (sizeA.IsContinuous && sizeB.StorageOrder == sizeA.StorageOrder && sizeA.IsSameShape(sizeB)) {

                // both inputs are continous and have the same storage order. moving all elements into the first dimension
                // can pot. save lots of overhead in the iteration! 

                // TODO: check if we can set ndims to 1 here! Is this used / working with the iteration in the caller? 
                dims[0] = buffer[1] - 1;
                stridesOut[0] = stride;
                stridesOut[ndims] = elemSizeA;
                stridesOut[ndims * 2] = elemSizeB;
                for (int i = 1; i < ndims; i++) {
                    dims[i] = 0; // -1 ! 
                    stridesOut[i] = 0;
                    stridesOut[i + ndims] = 0;
                    stridesOut[i + ndims * 2] = 0;
                }
                return;
            }

            if (!reverse) {
                // out array is column major
                for (uint i = 0; i < ndims; i++) {
                    stridesOut[i] = stride;
                    stride *= dims[i];
                    dims[i]--;
                    // which broadcasting mode is active?
                    if (Settings.ArrayStyle == ArrayStyles.ILNumericsV4) {
                        // dims are aligned at start, counting forwards
                        stridesOut[ndims + i] = sizeA.GetStride(i) * elemSizeA;
                        stridesOut[ndims * 2 + i] = sizeB.GetStride(i) * elemSizeB;
                    } else {
                        // dims are aligned at end, counting forwards
                        stridesOut[ndims + i] = sizeA.GetStride(sizeA.NumberOfDimensions - ndims + i) * elemSizeA;        // sic: uint wrapping intended!
                        stridesOut[ndims * 2 + i] = sizeB.GetStride(sizeB.NumberOfDimensions - ndims + i) * elemSizeB;
                    }
                }
            } else {
                // out array is row major
                for (uint i = 0; i < ndims; i++) {
                    stridesOut[i] = stride;
                    stride *= dims[ndims - i - 1];
                    //dims[i]--;
                    if (Settings.ArrayStyle == ArrayStyles.numpy) {
                        // dims are aligned at end, counting backwards
                        stridesOut[ndims + i] = sizeA.GetStride(sizeA.NumberOfDimensions - i - 1) * elemSizeA;  // sic: uint wrapping intended!
                        stridesOut[ndims * 2 + i] = sizeB.GetStride(sizeB.NumberOfDimensions - i - 1) * elemSizeB;
                    } else {
                        // dims are aligned at start, counting backwards
                        stridesOut[ndims + i] = sizeA.GetStride(ndims - 1 - i) * elemSizeA;
                        stridesOut[ndims * 2 + i] = sizeB.GetStride(ndims - 1 - i) * elemSizeB;
                    }
                }
                for (uint i = 0; i < ndims; i++) {
                    dims[i]--;
                }
                // must reverse dims for row major
                while (stridesOut-- > dims) {
                    var tmp = *dims;
                    *dims++ = *stridesOut;
                    *stridesOut = tmp;
                }
            }
            //buffer[0] = ndims;
            //buffer[1] = stride / elemSize; 
            buffer[2] = 0;
        }

        internal static unsafe string PrettyPrintNumber(double scaling, double element, int formatLength, int padding) {
            string sElement;
            if (double.IsNaN(element)) {
                sElement = "NaN";
            } else if (double.IsPositiveInfinity(element)) {
                sElement = "∞";
            } else if (double.IsNegativeInfinity(element)) {
                sElement = "-∞";
            } else if ((scaling == 1 && (long)element == element) || element == 0) {
                sElement = element.ToString();
            } else {
                element /= scaling;
                sElement = String.Format($"{{0:f{formatLength}}}", element);
                if (element < 0 && !sElement.StartsWith("-")) {
                    sElement = "-" + sElement;
                }
            }
            if (padding >= 0) {
                sElement = sElement.PadLeft(padding);
            } else {
                sElement = sElement.PadRight(-padding);
            }
            return sElement;
        }

        internal static unsafe string PrettyPrintNumber(double scaling, complex element, int formatLength, int padLeft) {
            bool hidei = double.IsNaN(element.imag) || double.IsInfinity(element.imag);
            string signi = (element.imag < 0) ? "-" : "+";
            var innerPadding = padLeft != 0 ? formatLength + 4 : 0; 
            string sElement = PrettyPrintNumber(scaling, element.real, formatLength, innerPadding)
                            + $"{signi}{(hidei ? " ":"i")}"
                            + ((element.imag * element.imag == 1) ? "".PadRight(innerPadding)  : PrettyPrintNumber(scaling, Math.Abs(element.imag), formatLength, -innerPadding)); 
            sElement = sElement.PadLeft(padLeft);
            return sElement;
        }

        /// <summary>
        /// Prepares an iteration BSD, considering output storage order and strides.
        /// </summary>
        /// <param name="outStorageOrder">The target storage order. For <see cref="StorageOrders.Other"/> the smallest strided dimension is used for iteration (cache awareness).</param>
        /// <param name="bsd">The source bsd.</param>
        /// <param name="ordered_bsd">[Out] re-ordered bsd, starting with the lead iteration dim at dim #0.</param>
        /// <param name="dimOffset">[Optional] offset for the dimension length. Some strided iterators use dim -1 instead of dim to save us some calculations inside inner loops.</param>
        /// <param name="strideFactor">Element sizes. Multiplied to the strides of the iteration bsd returned.</param>
        
        internal static unsafe void PrepareBSD(StorageOrders outStorageOrder, long* bsd, long* ordered_bsd, uint strideFactor = 1, int dimOffset = 0) {
            // bsd entries are ulong. Always.
            uint ndims = (uint)bsd[0];
            ordered_bsd[0] = bsd[0];
            ordered_bsd[1] = bsd[1];
            //ordered_strides[2] = *((ulong*)(bsd[2])); // base offset not used in workers
            if (outStorageOrder == StorageOrders.ColumnMajor) {
                for (uint i = 0; i < ndims; i++) {
                    ordered_bsd[i + 3] = bsd[i + 3] + dimOffset;
                    ordered_bsd[i + 3 + ndims] = bsd[i + 3 + ndims] * strideFactor;
                }
            } else if (outStorageOrder == StorageOrders.RowMajor) {
                // row major - copy reverted
                for (uint i = 0; i < ndims; i++) {
                    ordered_bsd[i + 3] = bsd[ndims - i + 2] + dimOffset;
                    ordered_bsd[i + 3 + ndims] = bsd[ndims - i + 2 + ndims] * strideFactor;
                }
            } else {
                System.Diagnostics.Debug.Assert(outStorageOrder == StorageOrders.Other);
                // find the smallest dimension, start iterating on smallest dim 
                var dStart = long.MaxValue;
                int dStartI = 0;  
                for (int i = 0; i < ndims; i++) {
                    if (bsd[i + 3 + ndims] < dStart) {
                        dStart = bsd[i + 3 + ndims];
                        dStartI = i;
                    }
                }

                for (uint i = 0; i < ndims; i++) {
                    ordered_bsd[3 + i] = bsd[3 + ((i + dStartI) % ndims)] + dimOffset;
                    ordered_bsd[i + 3 + ndims] = bsd[3 + ndims + ((i + dStartI) % ndims)] * strideFactor;
                }
            }
        }
        internal static unsafe void PrepareBSD(StorageOrders outStorageOrder, long* bsd, uint* ordered_bsd, uint strideFactor = 1) {
            // this gets called on 32 bit _and_ 64 bit. bsd entries are either ulong or uint but always in uint range.
            uint ndims = (uint)bsd[0];
            ordered_bsd[0] = ndims;
            ordered_bsd[1] = (uint)bsd[1];
            //ordered_strides[2] = *((ulong*)(bsd[2])); // base offset not used in workers
            if (outStorageOrder == StorageOrders.ColumnMajor) {
                for (uint i = 0; i < ndims; i++) {
                    ordered_bsd[i + 3] = (uint)bsd[i + 3];
                    ordered_bsd[i + 3 + ndims] = (uint)bsd[i + 3 + ndims] * strideFactor;
                }
            } else if (outStorageOrder == StorageOrders.RowMajor) {
                // row major - copy reverted
                for (uint i = 0; i < ndims; i++) {
                    ordered_bsd[i + 3] = (uint)bsd[ndims - i + 2];
                    ordered_bsd[i + 3 + ndims] = (uint)bsd[ndims - i + 2 + ndims] * strideFactor;
                }
            } else {
                System.Diagnostics.Debug.Assert(outStorageOrder == StorageOrders.Other);
                // find the smallest dimension, start iterating on smallest dim 
                var dStart = uint.MaxValue;
                uint dStartI = 0;
                for (uint i = 0; i < ndims; i++) {
                    if (bsd[i + 3 + ndims] < dStart) {
                        dStart = (uint)bsd[i + 3 + ndims];
                        dStartI = i;
                    }
                }

                for (uint i = 0; i < ndims; i++) {
                    ordered_bsd[3 + i] = (uint)bsd[3 + ((i + dStartI) % ndims)];
                    ordered_bsd[i + 3 + ndims] = (uint)bsd[3 + ndims + ((i + dStartI) % ndims)] * strideFactor;
                }
            }
        }

        internal static unsafe void determineMultithreadingParameters(long outLen, ref int overheadField, out uint workItemCount, out long workItemLength) {
            if (overheadField < 10) {
                overheadField = 10; // ugly hack to bring it back on track in case it slipped away
            }
            var overhead = (long)overheadField; // (long)Math.Max(overheadField, 1);
            workItemCount = Math.Max(1, Math.Min((uint)(outLen / overhead), Settings.MaxNumberThreads));
            workItemLength = Math.Max(outLen / workItemCount - overhead, Math.Sign(outLen));
            System.Diagnostics.Debug.Assert(workItemCount > 0 && workItemCount <= Settings.MaxNumberThreads);
        }

        /// <summary>
        /// reorder inBSD for iteration according to storageOrder and store into ordered_bsd. initialize outBSD accordingly (strides may differ!).
        /// </summary>
        /// <param name="storageOrder"></param>
        /// <param name="inBSD"></param>
        /// <param name="ordered_bsd"></param>
        /// <param name="outSize"></param>
        /// <param name="stride_factor"></param>
        /// <returns></returns>
        internal static unsafe void PrepareBSD4CopyTo(StorageOrders storageOrder, long* inBSD, long* ordered_bsd, Size outSize, uint stride_factor) {
            long stride = 1;
            uint ndims = (uint)inBSD[0];
            long* outBSD = (!Equals(outSize, null) ? outSize.GetBSD(true) : null); 
            for (int i = 0; i < ndims; i++) {
                // iter_ data for our local loop (always 'forward')
                switch (storageOrder) {
                    case StorageOrders.RowMajor:
                        ordered_bsd[3 + i] = inBSD[2 + ndims - i];
                        ordered_bsd[3 + ndims + i] = inBSD[2 + 2 * ndims - i] * stride_factor;

                        if (outBSD != null) {
                            outBSD[2 + ndims - i] = inBSD[2 + ndims - i];
                            outBSD[2 + 2 * ndims - i] = (stride);
                            stride *= inBSD[2 + ndims - i];
                        }
                        break;
                    case StorageOrders.ColumnMajor:
                        ordered_bsd[3 + i] = inBSD[3 + i];
                        ordered_bsd[3 + ndims + i] = inBSD[3 + ndims + i] * stride_factor;

                        if (outBSD != null) {
                            outBSD[3 + i] = inBSD[3 + i];
                            outBSD[3 + ndims + i] = (stride);
                            stride *= inBSD[3 + i];
                        }
                        break;
                    default:
                        throw new ArgumentException("CopyTo supports copying to continous storage only: ColumnMajor or RowMajor.");
                }

            }
            // finish BSD configuration
            ordered_bsd[0] = ndims;
            ordered_bsd[1] = inBSD[1];
            ordered_bsd[2] = inBSD[2];
            if (outBSD != null) {
                outBSD[0] = inBSD[0];
                outBSD[1] = inBSD[1];
                outBSD[2] = 0;
            }
        }
        /// 
        /// <summary>
        /// reorder inBSD for iteration according to storageOrder and store into ordered_bsd. initialize outBSD accordingly (strides may differ!).
        /// </summary>
        /// <param name="storageOrder"></param>
        /// <param name="inBSD"></param>
        /// <param name="ordered_bsd"></param>
        /// <param name="outSize"></param>
        /// <param name="stride_factor"></param>
        /// <returns></returns>
        /// <remarks><para>ordered_bsd stores the dest BSD (original, reordered), dims + strides (corrected) for src</para></remarks>
        internal static unsafe void PrepareBSD4CopyTo(StorageOrders storageOrder, long* inBSD, uint* ordered_bsd, Size outSize, uint stride_factor) {
            uint stride = 1;
            uint ndims = (uint)inBSD[0];
            long* outBSD = (!Equals(outSize, null) ? outSize.GetBSD(true) : null);
            for (int i = 0; i < ndims; i++) {
                // iter_ data for our local loop (always 'forward')
                switch (storageOrder) {
                    case StorageOrders.RowMajor:
                        ordered_bsd[3 + i] = (uint)inBSD[2 + ndims - i];
                        ordered_bsd[3 + ndims + i] = (uint)inBSD[2 + 2 * ndims - i] * stride_factor;

                        if (outBSD != null) {
                            outBSD[2 + ndims - i] = inBSD[2 + ndims - i];
                            outBSD[2 + 2 * ndims - i] = (stride);
                            stride *= (uint)inBSD[2 + ndims - i];
                        }
                        break;
                    case StorageOrders.ColumnMajor:
                        ordered_bsd[3 + i] = (uint)inBSD[3 + i];
                        ordered_bsd[3 + ndims + i] = (uint)inBSD[3 + ndims + i] * stride_factor;

                        if (outBSD != null) {
                            outBSD[3 + i] = inBSD[3 + i];
                            outBSD[3 + ndims + i] = (stride);
                            stride *= (uint)inBSD[3 + i];
                        }
                        break;
                    default:
                        throw new ArgumentException("CopyTo supports copying to continous storage only: ColumnMajor or RowMajor.");
                }

            }
            // finish BSD configuration
            ordered_bsd[0] = ndims;
            ordered_bsd[1] = (uint)inBSD[1];
            ordered_bsd[2] = (uint)inBSD[2];
            if (outBSD != null) {
                outBSD[0] = inBSD[0];
                outBSD[1] = inBSD[1];
                outBSD[2] = 0;
            }
        }

        /// <summary>
        /// creates the bsd and strides for fast iteration in WriteTo functions
        /// </summary>
        /// <param name="inSize">size of input (right side) array.</param>
        /// <param name="ordered_bsd">temp BSD / dim / strides for iteration. This is expected to be at least of len: ndims * 3 + 3 for: (out)ndims, nelem, baseoffset, dims, strides, (in) strides</param>
        /// <param name="outBSD">size of destination (left side) array</param>
        /// <param name="stride_factor">stride size (elements byte) factor</param>
        /// <remarks><para>This function prepares the strides for iterating over the 
        /// output elements of the destination array efficiently: </para>
        /// <para>1) Dimensions are reordered to iterate along the dimension of the 
        /// destination array with the smallest strides.</para>
        /// <para>2) virtual dimensions in inSize are replaced with singleton dimensions, 
        /// so that the number of dimensions in inSize equals ndims of outSize.</para>
        /// <para>3) all singleton dimensions in inSize get a stride of 0 assigned.</para>
        /// <para>4) broadcasting is allowed on the right side only!</para></remarks>
        internal static unsafe void PrepareBSD4WriteTo(Size inSize, long* outBSD, uint stride_factor, long* ordered_bsd) {
            uint ndims = (uint)outBSD[0];
            long outLen = outBSD[1];
            var inBSD = inSize.GetBSD(false);
            // find iteration dimension
            uint leadDim = 0;
            uint outFlags = 0;
            Size.CheckFlags(outBSD, ref outFlags); 

            // prepare ordered_bsd
            ordered_bsd[0] = ndims;
            ordered_bsd[1] = outLen;
            ordered_bsd[2] = outBSD[2];
            uint i = 0; 
            if (((uint)StorageOrders.ColumnMajor & outFlags) != 0) {
                for (; i < ndims; i++) {
                    var p = 3 + i + ordered_bsd;
                    var d = outBSD[3 + i];
                    *p = d; p += ndims; 
                    *p = outBSD[3 + i + ndims] * stride_factor; p += ndims;

                    var inDim = Settings.ArrayStyle == ArrayStyles.ILNumericsV4 ? i : inSize.NumberOfDimensions - ndims + i; 
                    var s = inSize[inDim];
                    if (s != d && s != 1) {
                        throw new ArgumentException($"The array {inSize.ToString()} on the right side is not broadcastable to the indexing shape [{Helper.dims2string(outBSD + 3, (uint)outBSD[0])}] on the left side.");
                    }
                    *p = (s < 2) ? 0 : inSize.GetStride(inDim) * stride_factor; 
                }
            } else if (((uint)StorageOrders.RowMajor & outFlags) != 0) {
                // for row major storage we reverse the dimensions, this 
                // is likely to create best cache performance.
                for (; i < ndims; i++) {
                    var p = 3 + i + ordered_bsd;
                    var d = outBSD[2 + ndims - i]; 
                    *p = d; p += ndims;
                    *p = outBSD[2 + 2 * ndims - i] * stride_factor; p += ndims;

                    var inDim = Settings.ArrayStyle == ArrayStyles.ILNumericsV4 ? ndims - 1 - i : inSize.NumberOfDimensions - 1 - i;
                    var s = inSize[inDim];
                    if (s != d && s != 1) {
                        throw new ArgumentException($"The array {inSize.ToString()} on the right side is not broadcastable to the indexing shape [{Helper.dims2string(outBSD + 3, (uint)outBSD[0])}] on the left side.");
                    }
                    *p = (s < 2) ? 0 : inSize.GetStride(inDim) * stride_factor;
                }
            } else {
#region reorder for non-trivial sizes
                // best cache performance would be archieved by sorting dimension
                // order according to stride size ascending. But since this deals 
                // with non-continous storage, cache performance will suffer anyway.
                // So, we keep it simple and rotate the smallest-strided dimension to first only.

                // find the smallest strides index: leadDim
                if (outLen > REORDER_THRESHOLD_WRITETO_OTHERSTORAGE) {
                    ordered_bsd[3] = long.MaxValue;
                    for (; i < ndims; i++) {
                        var str = outBSD[3 + ndims + i];
                        // recognize non-singleton dimensions only
                        if (str < ordered_bsd[3] && outBSD[3 + i] > 1) {
                            leadDim = i;
                            ordered_bsd[3] = str;
                        }
                    }
                }
                for (i = 0; i < ndims; i++) {
                    var p = 3 + i + ordered_bsd;
                    var d = outBSD[3 + ((i + leadDim) % ndims)];
                    * p = d; p += ndims;
                    *p = outBSD[3 + ndims + ((i + leadDim) % ndims)] * stride_factor; p += ndims;

                    // deal with the src array strides
                    // huh... ok ... weelllll......
                    // in numpy broadcasting starts with the last dimension. We rotate over 
                    // the range [inSize.NumberOfDimensions - ndims  ...  inSize.NumberOfDimensions - 1]
                    // TODO: this need some serious unit testing!!
                    var inDim = Settings.ArrayStyle == ArrayStyles.ILNumericsV4 ? (i + leadDim) % ndims 
                                                     : inSize.NumberOfDimensions - ndims + ((i + leadDim) % ndims);
                    var s = inSize[inDim];
                    if (s != d && s != 1) {
                        throw new ArgumentException($"The array {inSize.ToString()} on the right side is not broadcastable to the indexing shape [{Helper.dims2string(outBSD + 3, (uint)outBSD[0])}] on the left side.");
                    }
                    *p = (s < 2) ? 0 : inSize.GetStride(inDim) * stride_factor;
                }
#endregion
            }
            // error checking: if there are more dimensions in right side than in left side, all must be singletons or error.
            for (; i < inSize.NumberOfDimensions; i++) {
                var inDim = Settings.ArrayStyle == ArrayStyles.ILNumericsV4 ? i : ndims - i; 
                if (inSize[inDim] != 1) {
                    throw new ArgumentException($"The array {inSize.ToString()} on the right side is not broadcastable to the indexing shape [{Helper.dims2string(outBSD + 3, (uint)outBSD[0])}] on the left side."); 
                }
            }
        }
        /// <summary>
        /// Creates a comma separated list of indices in <paramref name="outDims"/> without limiting brackets or parantheses.
        /// </summary>
        /// <param name="outDims"></param>
        /// <param name="nrOutDims"></param>
        /// <param name="reverse">[Optional] reverse the listing of indices in <paramref name="nrOutDims"/>. Default: (false) list indices in the stored order.</param>
        /// <returns>String representation of the indices in <paramref name="nrOutDims"/>.</returns>
        internal static unsafe string dims2string(long* outDims, uint nrOutDims, bool reverse = false) {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < nrOutDims; i++) {
                if (i > 0) {
                    sb.Append(",");
                }
                sb.Append(outDims[reverse ? nrOutDims - i - 1 : i].ToString());
            }
            return sb.ToString();
        }

        internal static byte bool2byte(bool a) {
            return a ? (byte)1 : (byte)0;  
        }

        /// <summary>
        /// Find out, if value is finite-
        /// </summary>
        /// <param name="input">Input value</param>
        /// <returns>1 for finite values</returns>
        internal static byte isfinite(double input) {
            if (!double.IsInfinity(input) && !double.IsNaN(input))
                return (byte)1;
            else
                return (byte)0;
        }
        /// <summary>
        /// Find out, if value is finite
        /// </summary>
        /// <param name="input">Input value</param>
        /// <returns>1 for finite values</returns>
        internal static byte isfinite(complex input) {
            if (!complex.IsInfinity(input) && !complex.IsNaN(input))
                return (byte)1;
            else
                return (byte)0;
        }
        /// <summary>
        /// Find out, if value is finite
        /// </summary>
        /// <param name="input">Input value</param>
        /// <returns>1 for finite values</returns>
        internal static byte isfinite(float input) {
            if (!float.IsInfinity(input) && !float.IsNaN(input))
                return (byte)1;
            else
                return (byte)0;
        }
        /// <summary>
        /// Find out, if value is finite
        /// </summary>
        /// <param name="input">Input value</param>
        /// <returns>1 for finite values</returns>
        internal static byte isfinite(fcomplex input) {
            if (!fcomplex.IsInfinity(input) && !fcomplex.IsNaN(input))
                return (byte)1;
            else
                return (byte)0;
        }

        internal static bool isFullDimSpec(IIndexIterator it) {
            return !(it == null ||
                    it.GetMinimum() != Math.Min(0, it.GetLastDimensionIndex()) ||
                    it.GetMaximum() != it.GetLastDimensionIndex() ||
                    it.GetStepSize() != 1);
        }

        internal static string iterLengths2String(IIndexIterator[] iterators, uint nIterDims) {
            StringBuilder ret = new StringBuilder();
            ret.Append("[");
            for (int i = 0; i < nIterDims; i++) {
                if (i > 0) {
                    ret.Append(",");
                }
                ret.Append(iterators[i].GetLength().ToString());
            }
            ret.Append("]");
            return ret.ToString();
        }
        internal static unsafe string iterLengths2String(long* bsd) {
            StringBuilder ret = new StringBuilder();
            ret.Append("[");
            var ndims = bsd[0];
            for (uint i = 0; i < ndims; i++) {
                if (i > 0) {
                    ret.Append(",");
                }
                ret.Append(bsd[3 + i].ToString());
            }
            ret.Append("]");
            return ret.ToString();
        }

        #region binsearch
        internal unsafe static long binsearch(double* pA, long lo, long hi, double value) {
            while (lo <= hi) {
                long i = lo + ((hi - lo) >> 1);
                long c = pA[i].CompareTo(value);
                if (c == 0) return i;
                if (c < 0) {
                    lo = i + 1;
                } else {
                    hi = i - 1;
                }
            }
            return ~lo;
        }
        internal unsafe static int binsearch(float* pA, int lo, int hi, float value) {
            while (lo <= hi) {
                int i = lo + ((hi - lo) >> 1);
                int c = pA[i].CompareTo(value);
                if (c == 0) return i;
                if (c < 0) {
                    lo = i + 1;
                } else {
                    hi = i - 1;
                }
            }
            return ~lo;
        }
        internal unsafe static long binsearch(float* pA, long lo, long hi, float value) {
            while (lo <= hi) {
                long i = lo + ((hi - lo) >> 1);
                long c = pA[i].CompareTo(value);
                if (c == 0) return i;
                if (c < 0) {
                    lo = i + 1;
                } else {
                    hi = i - 1;
                }
            }
            return ~lo;
        }

        /// <summary>
        /// Creates an empty file at the current location with the pattern provided in pattern.
        /// </summary>
        /// <param name="pattern">Ex.: Filename_{0}.cs, where {0} will be replaced with a random string to produce an empty file.</param>
        /// <returns></returns>
        internal static string CreateFile(string pattern) {

            string newName() {
                var randName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
                return String.Format(pattern, randName);
            }

            var name = newName(); 
            do {
                while (System.IO.File.Exists(name)) {
                    name = newName();
                }
                FileStream file = null; 
                try {
                    file = System.IO.File.Open(name, System.IO.FileMode.CreateNew);
                    return name;
                } catch (IOException) {

                } finally {
                    file?.Close(); 
                }
            } while (true); 

        }

        #endregion

        internal static bool canBeUsedForInplaceOp<T, LocalT, InT, OutT, RetT, StorageT>(BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> s)
        where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {
            return false;
        }
        private static List<string> s_assemblyNames = new List<string>();
        public static ulong GetAssemblyIdent(Assembly assembly, int file) {
            // search the list of loaded assembly names for 'assembly'. If it is not found, add to the list. 
            // The ident number is simply the assembly's position in the list. It consumes the first (highest) 14 bits. 
            // The file number is generated by the ACC compiler and consumes up to 14 bits. It ends up in bits 15...29 of the result. 
            // The resulting ident is a long value, which can simply Or'ed with the invocation number in the segmented user code to get the final unique id. 

            var name = assembly.GetName().FullName; 
            if (!s_assemblyNames.Contains(name)) {
                lock (s_assemblyNames) {
                    if (!s_assemblyNames.Contains(name)) {
                        s_assemblyNames.Add(name);
                    }
                }
            }
            var pos = s_assemblyNames.IndexOf(name);
            System.Diagnostics.Debug.Assert(pos >= 0 && pos <= 1 << 14, $"Too many assemblies loaded! The maximum number of assemblies ILNumerics ACC can handle is: {1 << 14}. Found: {pos}.");
            System.Diagnostics.Debug.Assert(file >= 0 && file <= 1 << 14, $"Too large file identifier provided! The maximum number of files in a file ILNumerics ACC can handle is: {1 << 14}. Found: {file}."); 
            return (ulong)pos << (64 - 14) | (ulong)file << (64 - 2 * 14);
        }
    }
}
