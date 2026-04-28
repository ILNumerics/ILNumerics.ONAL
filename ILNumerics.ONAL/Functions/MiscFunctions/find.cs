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
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ILNumerics;
using static ILNumerics.Globals;
using System.Security;
using ILNumerics.Core.Global;
using ILNumerics.Core.Internal;

namespace ILNumerics.Core.Functions.Builtin {
    internal static partial class MathInternal {

        #region HYCALPER LOOPSTART
        /*!HC:TYPELIST:
        <hycalper>
        <type>
            <source locate="here">
                double
            </source>
            <destination>float</destination>
            <destination>complex</destination>
            <destination>fcomplex</destination>
            <destination>sbyte</destination>
            <destination>byte</destination>
            <destination>short</destination>
            <destination>ushort</destination>
            <destination>int</destination>
            <destination>uint</destination>
            <destination>long</destination>
            <destination>ulong</destination>
            <destination>double</destination>
            <destination>float</destination>
            <destination>complex</destination>
            <destination>fcomplex</destination>
            <destination>sbyte</destination>
            <destination>byte</destination>
            <destination>short</destination>
            <destination>ushort</destination>
            <destination>int</destination>
            <destination>uint</destination>
            <destination>long</destination>
            <destination>ulong</destination>
        </type>
        <type>
            <source locate="here">
                Int64
            </source>
            <destination>long</destination>
            <destination>long</destination>
            <destination>long</destination>
            <destination>long</destination>
            <destination>long</destination>
            <destination>long</destination>
            <destination>long</destination>
            <destination>long</destination>
            <destination>long</destination>
            <destination>long</destination>
            <destination>long</destination>
            <destination>int</destination>
            <destination>int</destination>
            <destination>int</destination>
            <destination>int</destination>
            <destination>int</destination>
            <destination>int</destination>
            <destination>int</destination>
            <destination>int</destination>
            <destination>int</destination>
            <destination>int</destination>
            <destination>int</destination>
            <destination>int</destination>
        </type>
        <type>
            <source locate="here">
                find
            </source>
            <destination>find</destination>
            <destination>find</destination>
            <destination>find</destination>
            <destination>find</destination>
            <destination>find</destination>
            <destination>find</destination>
            <destination>find</destination>
            <destination>find</destination>
            <destination>find</destination>
            <destination>find</destination>
            <destination>find</destination>
            <destination>find32</destination>
            <destination>find32</destination>
            <destination>find32</destination>
            <destination>find32</destination>
            <destination>find32</destination>
            <destination>find32</destination>
            <destination>find32</destination>
            <destination>find32</destination>
            <destination>find32</destination>
            <destination>find32</destination>
            <destination>find32</destination>
            <destination>find32</destination>
        </type>
        </hycalper>
        */

        /// <summary>
        /// Finds sequential indices of non-zero elements in n-d array, search from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <returns>Sequential indices of the non-zero-valued elements in <paramref name="A"/>.</returns>
        
        internal static unsafe Array<Int64> find(BaseArray<double> A) {

            if (Equals(A, null)) {
                throw new ArgumentException("Input array for 'find' cannot be null.");
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, out var storageA);

            if (storageA.S.NumberOfElements == 0) {
                return empty<Int64>(dim0: 0);
            } else if (storageA.S.NumberOfElements > Int64.MaxValue) {
                // this is here for Hycalper reasons only. It is required for the 32 bit overloads only. 
                throw new InvalidCastException($"The input array has too many elements for this overload of find32(). Consider using find() instead!");
            }
            var ret = Storage<Int64>.Create();
            Int64 nrElements = (Int64)storageA.S.NumberOfElements; // assume maximum for now
            ret.S.SetAll(nrElements);
            DeviceManagement.DeviceManager.GetDevice(0).New<Int64>((ulong)nrElements, ret.Handles);

            Int64* outP = (Int64*)ret.Handles[0].Pointer;
            Int64 i = 0;
            foreach (var b in storageA.AsRetArray().Iterator(StorageOrders.ColumnMajor)) { // A is released after iteration
                if (b != 0) {
                    *outP++ = i;
                }
                i++;
            }
            return ret.RetArray /*!HC:hcslicedisable*/ /**/ [slice(0, outP - (Int64*)ret.Handles[0].Pointer)];
        }
        /// <summary>
        /// Find sequential indices of non-zero elements in n-d array, searches from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <param name="backwards">[Optional] Search direction backwards. Default: (false) start at the first element and search to the last element of <paramref name="A"/>.</param>
        /// <param name="C">[Optional] If non-null on entry: the function returns the row indices and gives column indices in <paramref name="C"/>. Default: (null) the function returns the sequential indices of elements found only.</param>
        /// <param name="n">[Optional] Maximum number of non-zero elements to find. Default: (0) find all non-zero elements.</param>
        /// <param name="V">[Optional] Return the found non-zero element values in <paramref name="V"/>. Default: (null) don't return the values.</param>
        /// <returns>Sequential, column major ordered indices of the non-zero-valued elements in <paramref name="A"/> or row indices if <paramref name="C"/> is not null.</returns>
        /// <remarks><para>If <paramref name="C"/> or <paramref name="V"/> are not null on entry they are filled with the requested respective 
        /// info as column vectors. If <paramref name="C"/> is not null it contains the column indices of non-zero values. If <paramref name="A"/> 
        /// has more than two dimensions subsequent dimensions are merged into the second dimension and the indices given in <paramref name="C"/> 
        /// are considered sequential indices of <paramref name="A"/> (column major order). </para>
        /// <para>Note, that the array returned by <see cref="find(BaseArray{double}, Int64, bool, OutArray{Int64}, OutArray{double})"/>
        /// corresponds to the indices of the rows of non-zero values if <paramref name="C"/> is not null and to sequential indices of those 
        /// non-zero values if <paramref name="C"/> is null. </para>
        /// <para>Negative values of <paramref name="n"/> are considered as abs(n).</para>
        /// <para>If <paramref name="backwards"/> is true <see cref="find(BaseArray{double}, Int64, bool, OutArray{Int64}, OutArray{double})"/>
        /// starts searching for non-zero values from the last element and proceeds in column major order to the first element or until <paramref name="n"/> 
        /// non-zero elements were found, whatever comes first.</para>
        /// <para>Find() returns and considers indices as 64 bit <see cref="Int64"/>. Overloads exist for backwards compatibility which expect indices as <see cref="Int32"/>
        /// values. Such functions are decorated with the suffix '32' in their names.</para>
        /// </remarks>
        internal static unsafe Array<Int64> find(BaseArray<double> A, Int64 n = 0, bool backwards = false, OutArray<Int64> C = null, OutArray<double> V = null) {

            using (Scope.Enter()) {
                if (Equals(A, null)) {
                    throw new ArgumentException("Input array for 'find' cannot be null.");
                }
                using var _1 = ReaderLock.Create(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, out var storageA);
                //if (storageA.S.NumberOfDimensions > 2 && !Equals(C,null)) {
                //    return find64(storageA.GetRange_ML(full, full, false), n, backwards, C, V); 
                //}
                #region  handle empty A
                if (storageA.S.NumberOfElements == 0) {
                    if (!Equals(C, null)) {
                        lock (C.SynchObj)
                            C.a = empty<Int64>(0);
                    }
                    if (!Equals(V, null)) {
                        lock (V.SynchObj)
                            V.a = empty<double>(dim0: 0);
                    }
                    return empty<Int64>(dim0: 0);
                } else if (storageA.S.NumberOfElements > Int64.MaxValue) {
                    throw new InvalidCastException($"The input array has too many elements for this overload of find(). Consider using find_64_ instead!");
                }
                #endregion

                if (n < 0) {
                    n = (Int64)Math.Abs(n);  // for hycalper...
                } else if (n == 0) {
                    n = (Int64)storageA.S.NumberOfElements; // assume maximum for now; 
                }
                if (n > storageA.S.NumberOfElements) {
                    n = (Int64)storageA.S.NumberOfElements;
                }
                Array<Int64> c = null;
                Array<double> v = null;

                // initialize working / out arrays with assumed maximum
                if (!Equals(C, null)) {
                    c = empty<Int64>(n);
                }
                if (!Equals(V, null)) {
                    v = empty<double>(n);
                }

                // initialize ret array with assumed maximum
                Array<Int64> ret = empty<Int64>(n);

                Int64* retP = (Int64*)ret.GetHostPointerForWrite();
                double* aP = (double*)storageA.Handles[0].Pointer; // size.GetSeqIndex contains storageA.S.BaseOffset;
                Int64* cP = (!Equals(c, null) ? (Int64*)c.GetHostPointerForWrite() : null);
                double* vP = (!Equals(v, null) ? (double*)v.GetHostPointerForWrite() : null);
                Int64 lastElemDim2 = (Int64)(storageA.S.NumberOfElements / storageA.S[0]);
                Int64 foundN = 0, strideA0 = (Int64)storageA.S[0];
                if (backwards == false) {
                    for (Int64 ic = 0; ic < lastElemDim2 && foundN < n; ic++) {
                        for (Int64 ir = 0; ir < strideA0 && foundN < n; ir++) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                } else {
                    for (Int64 ic = lastElemDim2; ic-- > 0 && foundN < n;) {
                        for (Int64 ir = (Int64)strideA0; ir-- > 0 && foundN < n;) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                }
                //Int64 foundN = outP - (Int64*)ret.m_handles[0].Pointer;
                if (!Equals(v, null)) {
                    lock (V.SynchObj) {
                        if (foundN == 0) {
                            V.a = empty<double>(dim0: 0);
                        } else {
                            V.a = v[slice(0, foundN)];
                        }
                    }
                }
                if (!Equals(c, null)) {
                    lock (C.SynchObj) {
                        if (foundN == 0) {
                            C.a = empty<Int64>(dim0: 0);
                        } else {
                            C.a = c[slice(0, foundN)];
                        }
                    }
                }
                return ret[slice(0, foundN)];
            }
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        /// <summary>
        /// Finds sequential indices of non-zero elements in n-d array, search from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <returns>Sequential indices of the non-zero-valued elements in <paramref name="A"/>.</returns>
        
        internal static unsafe Array<int> find32(BaseArray<ulong> A) {

            if (Equals(A, null)) {
                throw new ArgumentException("Input array for 'find32' cannot be null.");
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>, out var storageA);

            if (storageA.S.NumberOfElements == 0) {
                return empty<int>(dim0: 0);
            } else if (storageA.S.NumberOfElements > int.MaxValue) {
                // this is here for Hycalper reasons only. It is required for the 32 bit overloads only. 
                throw new InvalidCastException($"The input array has too many elements for this overload of find3232(). Consider using find32() instead!");
            }
            var ret = Storage<int>.Create();
            int nrElements = (int)storageA.S.NumberOfElements; // assume maximum for now
            ret.S.SetAll(nrElements);
            DeviceManagement.DeviceManager.GetDevice(0).New<int>((ulong)nrElements, ret.Handles);

            int* outP = (int*)ret.Handles[0].Pointer;
            int i = 0;
            foreach (var b in storageA.AsRetArray().Iterator(StorageOrders.ColumnMajor)) { // A is released after iteration
                if (b != 0) {
                    *outP++ = i;
                }
                i++;
            }
            return ret.RetArray  /**/ [slice(0, outP - (int*)ret.Handles[0].Pointer)];
        }
        /// <summary>
        /// Find sequential indices of non-zero elements in n-d array, searches from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <param name="backwards">[Optional] Search direction backwards. Default: (false) start at the first element and search to the last element of <paramref name="A"/>.</param>
        /// <param name="C">[Optional] If non-null on entry: the function returns the row indices and gives column indices in <paramref name="C"/>. Default: (null) the function returns the sequential indices of elements found only.</param>
        /// <param name="n">[Optional] Maximum number of non-zero elements to find32. Default: (0) find32 all non-zero elements.</param>
        /// <param name="V">[Optional] Return the found non-zero element values in <paramref name="V"/>. Default: (null) don't return the values.</param>
        /// <returns>Sequential, column major ordered indices of the non-zero-valued elements in <paramref name="A"/> or row indices if <paramref name="C"/> is not null.</returns>
        /// <remarks><para>If <paramref name="C"/> or <paramref name="V"/> are not null on entry they are filled with the requested respective 
        /// info as column vectors. If <paramref name="C"/> is not null it contains the column indices of non-zero values. If <paramref name="A"/> 
        /// has more than two dimensions subsequent dimensions are merged into the second dimension and the indices given in <paramref name="C"/> 
        /// are considered sequential indices of <paramref name="A"/> (column major order). </para>
        /// <para>Note, that the array returned by <see cref="find32(BaseArray{ulong}, int, bool, OutArray{int}, OutArray{ulong})"/>
        /// corresponds to the indices of the rows of non-zero values if <paramref name="C"/> is not null and to sequential indices of those 
        /// non-zero values if <paramref name="C"/> is null. </para>
        /// <para>Negative values of <paramref name="n"/> are considered as abs(n).</para>
        /// <para>If <paramref name="backwards"/> is true <see cref="find32(BaseArray{ulong}, int, bool, OutArray{int}, OutArray{ulong})"/>
        /// starts searching for non-zero values from the last element and proceeds in column major order to the first element or until <paramref name="n"/> 
        /// non-zero elements were found, whatever comes first.</para>
        /// <para>Find() returns and considers indices as 64 bit <see cref="int"/>. Overloads exist for backwards compatibility which expect indices as <see cref="Int32"/>
        /// values. Such functions are decorated with the suffix '32' in their names.</para>
        /// </remarks>
        internal static unsafe Array<int> find32(BaseArray<ulong> A, int n = 0, bool backwards = false, OutArray<int> C = null, OutArray<ulong> V = null) {

            using (Scope.Enter()) {
                if (Equals(A, null)) {
                    throw new ArgumentException("Input array for 'find32' cannot be null.");
                }
                using var _1 = ReaderLock.Create(A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>, out var storageA);
                //if (storageA.S.NumberOfDimensions > 2 && !Equals(C,null)) {
                //    return find3264(storageA.GetRange_ML(full, full, false), n, backwards, C, V); 
                //}
                #region  handle empty A
                if (storageA.S.NumberOfElements == 0) {
                    if (!Equals(C, null)) {
                        lock (C.SynchObj)
                            C.a = empty<int>(0);
                    }
                    if (!Equals(V, null)) {
                        lock (V.SynchObj)
                            V.a = empty<ulong>(dim0: 0);
                    }
                    return empty<int>(dim0: 0);
                } else if (storageA.S.NumberOfElements > int.MaxValue) {
                    throw new InvalidCastException($"The input array has too many elements for this overload of find32(). Consider using find32_64_ instead!");
                }
                #endregion

                if (n < 0) {
                    n = (int)Math.Abs(n);  // for hycalper...
                } else if (n == 0) {
                    n = (int)storageA.S.NumberOfElements; // assume maximum for now; 
                }
                if (n > storageA.S.NumberOfElements) {
                    n = (int)storageA.S.NumberOfElements;
                }
                Array<int> c = null;
                Array<ulong> v = null;

                // initialize working / out arrays with assumed maximum
                if (!Equals(C, null)) {
                    c = empty<int>(n);
                }
                if (!Equals(V, null)) {
                    v = empty<ulong>(n);
                }

                // initialize ret array with assumed maximum
                Array<int> ret = empty<int>(n);

                int* retP = (int*)ret.GetHostPointerForWrite();
                ulong* aP = (ulong*)storageA.Handles[0].Pointer; // size.GetSeqIndex contains storageA.S.BaseOffset;
                int* cP = (!Equals(c, null) ? (int*)c.GetHostPointerForWrite() : null);
                ulong* vP = (!Equals(v, null) ? (ulong*)v.GetHostPointerForWrite() : null);
                int lastElemDim2 = (int)(storageA.S.NumberOfElements / storageA.S[0]);
                int foundN = 0, strideA0 = (int)storageA.S[0];
                if (backwards == false) {
                    for (int ic = 0; ic < lastElemDim2 && foundN < n; ic++) {
                        for (int ir = 0; ir < strideA0 && foundN < n; ir++) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                } else {
                    for (int ic = lastElemDim2; ic-- > 0 && foundN < n;) {
                        for (int ir = (int)strideA0; ir-- > 0 && foundN < n;) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                }
                //int foundN = outP - (int*)ret.m_handles[0].Pointer;
                if (!Equals(v, null)) {
                    lock (V.SynchObj) {
                        if (foundN == 0) {
                            V.a = empty<ulong>(dim0: 0);
                        } else {
                            V.a = v[slice(0, foundN)];
                        }
                    }
                }
                if (!Equals(c, null)) {
                    lock (C.SynchObj) {
                        if (foundN == 0) {
                            C.a = empty<int>(dim0: 0);
                        } else {
                            C.a = c[slice(0, foundN)];
                        }
                    }
                }
                return ret[slice(0, foundN)];
            }
        }
       

        /// <summary>
        /// Finds sequential indices of non-zero elements in n-d array, search from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <returns>Sequential indices of the non-zero-valued elements in <paramref name="A"/>.</returns>
        
        internal static unsafe Array<int> find32(BaseArray<long> A) {

            if (Equals(A, null)) {
                throw new ArgumentException("Input array for 'find32' cannot be null.");
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, out var storageA);

            if (storageA.S.NumberOfElements == 0) {
                return empty<int>(dim0: 0);
            } else if (storageA.S.NumberOfElements > int.MaxValue) {
                // this is here for Hycalper reasons only. It is required for the 32 bit overloads only. 
                throw new InvalidCastException($"The input array has too many elements for this overload of find3232(). Consider using find32() instead!");
            }
            var ret = Storage<int>.Create();
            int nrElements = (int)storageA.S.NumberOfElements; // assume maximum for now
            ret.S.SetAll(nrElements);
            DeviceManagement.DeviceManager.GetDevice(0).New<int>((ulong)nrElements, ret.Handles);

            int* outP = (int*)ret.Handles[0].Pointer;
            int i = 0;
            foreach (var b in storageA.AsRetArray().Iterator(StorageOrders.ColumnMajor)) { // A is released after iteration
                if (b != 0) {
                    *outP++ = i;
                }
                i++;
            }
            return ret.RetArray  /**/ [slice(0, outP - (int*)ret.Handles[0].Pointer)];
        }
        /// <summary>
        /// Find sequential indices of non-zero elements in n-d array, searches from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <param name="backwards">[Optional] Search direction backwards. Default: (false) start at the first element and search to the last element of <paramref name="A"/>.</param>
        /// <param name="C">[Optional] If non-null on entry: the function returns the row indices and gives column indices in <paramref name="C"/>. Default: (null) the function returns the sequential indices of elements found only.</param>
        /// <param name="n">[Optional] Maximum number of non-zero elements to find32. Default: (0) find32 all non-zero elements.</param>
        /// <param name="V">[Optional] Return the found non-zero element values in <paramref name="V"/>. Default: (null) don't return the values.</param>
        /// <returns>Sequential, column major ordered indices of the non-zero-valued elements in <paramref name="A"/> or row indices if <paramref name="C"/> is not null.</returns>
        /// <remarks><para>If <paramref name="C"/> or <paramref name="V"/> are not null on entry they are filled with the requested respective 
        /// info as column vectors. If <paramref name="C"/> is not null it contains the column indices of non-zero values. If <paramref name="A"/> 
        /// has more than two dimensions subsequent dimensions are merged into the second dimension and the indices given in <paramref name="C"/> 
        /// are considered sequential indices of <paramref name="A"/> (column major order). </para>
        /// <para>Note, that the array returned by <see cref="find32(BaseArray{long}, int, bool, OutArray{int}, OutArray{long})"/>
        /// corresponds to the indices of the rows of non-zero values if <paramref name="C"/> is not null and to sequential indices of those 
        /// non-zero values if <paramref name="C"/> is null. </para>
        /// <para>Negative values of <paramref name="n"/> are considered as abs(n).</para>
        /// <para>If <paramref name="backwards"/> is true <see cref="find32(BaseArray{long}, int, bool, OutArray{int}, OutArray{long})"/>
        /// starts searching for non-zero values from the last element and proceeds in column major order to the first element or until <paramref name="n"/> 
        /// non-zero elements were found, whatever comes first.</para>
        /// <para>Find() returns and considers indices as 64 bit <see cref="int"/>. Overloads exist for backwards compatibility which expect indices as <see cref="Int32"/>
        /// values. Such functions are decorated with the suffix '32' in their names.</para>
        /// </remarks>
        internal static unsafe Array<int> find32(BaseArray<long> A, int n = 0, bool backwards = false, OutArray<int> C = null, OutArray<long> V = null) {

            using (Scope.Enter()) {
                if (Equals(A, null)) {
                    throw new ArgumentException("Input array for 'find32' cannot be null.");
                }
                using var _1 = ReaderLock.Create(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, out var storageA);
                //if (storageA.S.NumberOfDimensions > 2 && !Equals(C,null)) {
                //    return find3264(storageA.GetRange_ML(full, full, false), n, backwards, C, V); 
                //}
                #region  handle empty A
                if (storageA.S.NumberOfElements == 0) {
                    if (!Equals(C, null)) {
                        lock (C.SynchObj)
                            C.a = empty<int>(0);
                    }
                    if (!Equals(V, null)) {
                        lock (V.SynchObj)
                            V.a = empty<long>(dim0: 0);
                    }
                    return empty<int>(dim0: 0);
                } else if (storageA.S.NumberOfElements > int.MaxValue) {
                    throw new InvalidCastException($"The input array has too many elements for this overload of find32(). Consider using find32_64_ instead!");
                }
                #endregion

                if (n < 0) {
                    n = (int)Math.Abs(n);  // for hycalper...
                } else if (n == 0) {
                    n = (int)storageA.S.NumberOfElements; // assume maximum for now; 
                }
                if (n > storageA.S.NumberOfElements) {
                    n = (int)storageA.S.NumberOfElements;
                }
                Array<int> c = null;
                Array<long> v = null;

                // initialize working / out arrays with assumed maximum
                if (!Equals(C, null)) {
                    c = empty<int>(n);
                }
                if (!Equals(V, null)) {
                    v = empty<long>(n);
                }

                // initialize ret array with assumed maximum
                Array<int> ret = empty<int>(n);

                int* retP = (int*)ret.GetHostPointerForWrite();
                long* aP = (long*)storageA.Handles[0].Pointer; // size.GetSeqIndex contains storageA.S.BaseOffset;
                int* cP = (!Equals(c, null) ? (int*)c.GetHostPointerForWrite() : null);
                long* vP = (!Equals(v, null) ? (long*)v.GetHostPointerForWrite() : null);
                int lastElemDim2 = (int)(storageA.S.NumberOfElements / storageA.S[0]);
                int foundN = 0, strideA0 = (int)storageA.S[0];
                if (backwards == false) {
                    for (int ic = 0; ic < lastElemDim2 && foundN < n; ic++) {
                        for (int ir = 0; ir < strideA0 && foundN < n; ir++) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                } else {
                    for (int ic = lastElemDim2; ic-- > 0 && foundN < n;) {
                        for (int ir = (int)strideA0; ir-- > 0 && foundN < n;) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                }
                //int foundN = outP - (int*)ret.m_handles[0].Pointer;
                if (!Equals(v, null)) {
                    lock (V.SynchObj) {
                        if (foundN == 0) {
                            V.a = empty<long>(dim0: 0);
                        } else {
                            V.a = v[slice(0, foundN)];
                        }
                    }
                }
                if (!Equals(c, null)) {
                    lock (C.SynchObj) {
                        if (foundN == 0) {
                            C.a = empty<int>(dim0: 0);
                        } else {
                            C.a = c[slice(0, foundN)];
                        }
                    }
                }
                return ret[slice(0, foundN)];
            }
        }
       

        /// <summary>
        /// Finds sequential indices of non-zero elements in n-d array, search from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <returns>Sequential indices of the non-zero-valued elements in <paramref name="A"/>.</returns>
        
        internal static unsafe Array<int> find32(BaseArray<uint> A) {

            if (Equals(A, null)) {
                throw new ArgumentException("Input array for 'find32' cannot be null.");
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>, out var storageA);

            if (storageA.S.NumberOfElements == 0) {
                return empty<int>(dim0: 0);
            } else if (storageA.S.NumberOfElements > int.MaxValue) {
                // this is here for Hycalper reasons only. It is required for the 32 bit overloads only. 
                throw new InvalidCastException($"The input array has too many elements for this overload of find3232(). Consider using find32() instead!");
            }
            var ret = Storage<int>.Create();
            int nrElements = (int)storageA.S.NumberOfElements; // assume maximum for now
            ret.S.SetAll(nrElements);
            DeviceManagement.DeviceManager.GetDevice(0).New<int>((ulong)nrElements, ret.Handles);

            int* outP = (int*)ret.Handles[0].Pointer;
            int i = 0;
            foreach (var b in storageA.AsRetArray().Iterator(StorageOrders.ColumnMajor)) { // A is released after iteration
                if (b != 0) {
                    *outP++ = i;
                }
                i++;
            }
            return ret.RetArray  /**/ [slice(0, outP - (int*)ret.Handles[0].Pointer)];
        }
        /// <summary>
        /// Find sequential indices of non-zero elements in n-d array, searches from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <param name="backwards">[Optional] Search direction backwards. Default: (false) start at the first element and search to the last element of <paramref name="A"/>.</param>
        /// <param name="C">[Optional] If non-null on entry: the function returns the row indices and gives column indices in <paramref name="C"/>. Default: (null) the function returns the sequential indices of elements found only.</param>
        /// <param name="n">[Optional] Maximum number of non-zero elements to find32. Default: (0) find32 all non-zero elements.</param>
        /// <param name="V">[Optional] Return the found non-zero element values in <paramref name="V"/>. Default: (null) don't return the values.</param>
        /// <returns>Sequential, column major ordered indices of the non-zero-valued elements in <paramref name="A"/> or row indices if <paramref name="C"/> is not null.</returns>
        /// <remarks><para>If <paramref name="C"/> or <paramref name="V"/> are not null on entry they are filled with the requested respective 
        /// info as column vectors. If <paramref name="C"/> is not null it contains the column indices of non-zero values. If <paramref name="A"/> 
        /// has more than two dimensions subsequent dimensions are merged into the second dimension and the indices given in <paramref name="C"/> 
        /// are considered sequential indices of <paramref name="A"/> (column major order). </para>
        /// <para>Note, that the array returned by <see cref="find32(BaseArray{uint}, int, bool, OutArray{int}, OutArray{uint})"/>
        /// corresponds to the indices of the rows of non-zero values if <paramref name="C"/> is not null and to sequential indices of those 
        /// non-zero values if <paramref name="C"/> is null. </para>
        /// <para>Negative values of <paramref name="n"/> are considered as abs(n).</para>
        /// <para>If <paramref name="backwards"/> is true <see cref="find32(BaseArray{uint}, int, bool, OutArray{int}, OutArray{uint})"/>
        /// starts searching for non-zero values from the last element and proceeds in column major order to the first element or until <paramref name="n"/> 
        /// non-zero elements were found, whatever comes first.</para>
        /// <para>Find() returns and considers indices as 64 bit <see cref="int"/>. Overloads exist for backwards compatibility which expect indices as <see cref="Int32"/>
        /// values. Such functions are decorated with the suffix '32' in their names.</para>
        /// </remarks>
        internal static unsafe Array<int> find32(BaseArray<uint> A, int n = 0, bool backwards = false, OutArray<int> C = null, OutArray<uint> V = null) {

            using (Scope.Enter()) {
                if (Equals(A, null)) {
                    throw new ArgumentException("Input array for 'find32' cannot be null.");
                }
                using var _1 = ReaderLock.Create(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>, out var storageA);
                //if (storageA.S.NumberOfDimensions > 2 && !Equals(C,null)) {
                //    return find3264(storageA.GetRange_ML(full, full, false), n, backwards, C, V); 
                //}
                #region  handle empty A
                if (storageA.S.NumberOfElements == 0) {
                    if (!Equals(C, null)) {
                        lock (C.SynchObj)
                            C.a = empty<int>(0);
                    }
                    if (!Equals(V, null)) {
                        lock (V.SynchObj)
                            V.a = empty<uint>(dim0: 0);
                    }
                    return empty<int>(dim0: 0);
                } else if (storageA.S.NumberOfElements > int.MaxValue) {
                    throw new InvalidCastException($"The input array has too many elements for this overload of find32(). Consider using find32_64_ instead!");
                }
                #endregion

                if (n < 0) {
                    n = (int)Math.Abs(n);  // for hycalper...
                } else if (n == 0) {
                    n = (int)storageA.S.NumberOfElements; // assume maximum for now; 
                }
                if (n > storageA.S.NumberOfElements) {
                    n = (int)storageA.S.NumberOfElements;
                }
                Array<int> c = null;
                Array<uint> v = null;

                // initialize working / out arrays with assumed maximum
                if (!Equals(C, null)) {
                    c = empty<int>(n);
                }
                if (!Equals(V, null)) {
                    v = empty<uint>(n);
                }

                // initialize ret array with assumed maximum
                Array<int> ret = empty<int>(n);

                int* retP = (int*)ret.GetHostPointerForWrite();
                uint* aP = (uint*)storageA.Handles[0].Pointer; // size.GetSeqIndex contains storageA.S.BaseOffset;
                int* cP = (!Equals(c, null) ? (int*)c.GetHostPointerForWrite() : null);
                uint* vP = (!Equals(v, null) ? (uint*)v.GetHostPointerForWrite() : null);
                int lastElemDim2 = (int)(storageA.S.NumberOfElements / storageA.S[0]);
                int foundN = 0, strideA0 = (int)storageA.S[0];
                if (backwards == false) {
                    for (int ic = 0; ic < lastElemDim2 && foundN < n; ic++) {
                        for (int ir = 0; ir < strideA0 && foundN < n; ir++) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                } else {
                    for (int ic = lastElemDim2; ic-- > 0 && foundN < n;) {
                        for (int ir = (int)strideA0; ir-- > 0 && foundN < n;) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                }
                //int foundN = outP - (int*)ret.m_handles[0].Pointer;
                if (!Equals(v, null)) {
                    lock (V.SynchObj) {
                        if (foundN == 0) {
                            V.a = empty<uint>(dim0: 0);
                        } else {
                            V.a = v[slice(0, foundN)];
                        }
                    }
                }
                if (!Equals(c, null)) {
                    lock (C.SynchObj) {
                        if (foundN == 0) {
                            C.a = empty<int>(dim0: 0);
                        } else {
                            C.a = c[slice(0, foundN)];
                        }
                    }
                }
                return ret[slice(0, foundN)];
            }
        }
       

        /// <summary>
        /// Finds sequential indices of non-zero elements in n-d array, search from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <returns>Sequential indices of the non-zero-valued elements in <paramref name="A"/>.</returns>
        
        internal static unsafe Array<int> find32(BaseArray<int> A) {

            if (Equals(A, null)) {
                throw new ArgumentException("Input array for 'find32' cannot be null.");
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>, out var storageA);

            if (storageA.S.NumberOfElements == 0) {
                return empty<int>(dim0: 0);
            } else if (storageA.S.NumberOfElements > int.MaxValue) {
                // this is here for Hycalper reasons only. It is required for the 32 bit overloads only. 
                throw new InvalidCastException($"The input array has too many elements for this overload of find3232(). Consider using find32() instead!");
            }
            var ret = Storage<int>.Create();
            int nrElements = (int)storageA.S.NumberOfElements; // assume maximum for now
            ret.S.SetAll(nrElements);
            DeviceManagement.DeviceManager.GetDevice(0).New<int>((ulong)nrElements, ret.Handles);

            int* outP = (int*)ret.Handles[0].Pointer;
            int i = 0;
            foreach (var b in storageA.AsRetArray().Iterator(StorageOrders.ColumnMajor)) { // A is released after iteration
                if (b != 0) {
                    *outP++ = i;
                }
                i++;
            }
            return ret.RetArray  /**/ [slice(0, outP - (int*)ret.Handles[0].Pointer)];
        }
        /// <summary>
        /// Find sequential indices of non-zero elements in n-d array, searches from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <param name="backwards">[Optional] Search direction backwards. Default: (false) start at the first element and search to the last element of <paramref name="A"/>.</param>
        /// <param name="C">[Optional] If non-null on entry: the function returns the row indices and gives column indices in <paramref name="C"/>. Default: (null) the function returns the sequential indices of elements found only.</param>
        /// <param name="n">[Optional] Maximum number of non-zero elements to find32. Default: (0) find32 all non-zero elements.</param>
        /// <param name="V">[Optional] Return the found non-zero element values in <paramref name="V"/>. Default: (null) don't return the values.</param>
        /// <returns>Sequential, column major ordered indices of the non-zero-valued elements in <paramref name="A"/> or row indices if <paramref name="C"/> is not null.</returns>
        /// <remarks><para>If <paramref name="C"/> or <paramref name="V"/> are not null on entry they are filled with the requested respective 
        /// info as column vectors. If <paramref name="C"/> is not null it contains the column indices of non-zero values. If <paramref name="A"/> 
        /// has more than two dimensions subsequent dimensions are merged into the second dimension and the indices given in <paramref name="C"/> 
        /// are considered sequential indices of <paramref name="A"/> (column major order). </para>
        /// <para>Note, that the array returned by <see cref="find32(BaseArray{int}, int, bool, OutArray{int}, OutArray{int})"/>
        /// corresponds to the indices of the rows of non-zero values if <paramref name="C"/> is not null and to sequential indices of those 
        /// non-zero values if <paramref name="C"/> is null. </para>
        /// <para>Negative values of <paramref name="n"/> are considered as abs(n).</para>
        /// <para>If <paramref name="backwards"/> is true <see cref="find32(BaseArray{int}, int, bool, OutArray{int}, OutArray{int})"/>
        /// starts searching for non-zero values from the last element and proceeds in column major order to the first element or until <paramref name="n"/> 
        /// non-zero elements were found, whatever comes first.</para>
        /// <para>Find() returns and considers indices as 64 bit <see cref="int"/>. Overloads exist for backwards compatibility which expect indices as <see cref="Int32"/>
        /// values. Such functions are decorated with the suffix '32' in their names.</para>
        /// </remarks>
        internal static unsafe Array<int> find32(BaseArray<int> A, int n = 0, bool backwards = false, OutArray<int> C = null, OutArray<int> V = null) {

            using (Scope.Enter()) {
                if (Equals(A, null)) {
                    throw new ArgumentException("Input array for 'find32' cannot be null.");
                }
                using var _1 = ReaderLock.Create(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>, out var storageA);
                //if (storageA.S.NumberOfDimensions > 2 && !Equals(C,null)) {
                //    return find3264(storageA.GetRange_ML(full, full, false), n, backwards, C, V); 
                //}
                #region  handle empty A
                if (storageA.S.NumberOfElements == 0) {
                    if (!Equals(C, null)) {
                        lock (C.SynchObj)
                            C.a = empty<int>(0);
                    }
                    if (!Equals(V, null)) {
                        lock (V.SynchObj)
                            V.a = empty<int>(dim0: 0);
                    }
                    return empty<int>(dim0: 0);
                } else if (storageA.S.NumberOfElements > int.MaxValue) {
                    throw new InvalidCastException($"The input array has too many elements for this overload of find32(). Consider using find32_64_ instead!");
                }
                #endregion

                if (n < 0) {
                    n = (int)Math.Abs(n);  // for hycalper...
                } else if (n == 0) {
                    n = (int)storageA.S.NumberOfElements; // assume maximum for now; 
                }
                if (n > storageA.S.NumberOfElements) {
                    n = (int)storageA.S.NumberOfElements;
                }
                Array<int> c = null;
                Array<int> v = null;

                // initialize working / out arrays with assumed maximum
                if (!Equals(C, null)) {
                    c = empty<int>(n);
                }
                if (!Equals(V, null)) {
                    v = empty<int>(n);
                }

                // initialize ret array with assumed maximum
                Array<int> ret = empty<int>(n);

                int* retP = (int*)ret.GetHostPointerForWrite();
                int* aP = (int*)storageA.Handles[0].Pointer; // size.GetSeqIndex contains storageA.S.BaseOffset;
                int* cP = (!Equals(c, null) ? (int*)c.GetHostPointerForWrite() : null);
                int* vP = (!Equals(v, null) ? (int*)v.GetHostPointerForWrite() : null);
                int lastElemDim2 = (int)(storageA.S.NumberOfElements / storageA.S[0]);
                int foundN = 0, strideA0 = (int)storageA.S[0];
                if (backwards == false) {
                    for (int ic = 0; ic < lastElemDim2 && foundN < n; ic++) {
                        for (int ir = 0; ir < strideA0 && foundN < n; ir++) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                } else {
                    for (int ic = lastElemDim2; ic-- > 0 && foundN < n;) {
                        for (int ir = (int)strideA0; ir-- > 0 && foundN < n;) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                }
                //int foundN = outP - (int*)ret.m_handles[0].Pointer;
                if (!Equals(v, null)) {
                    lock (V.SynchObj) {
                        if (foundN == 0) {
                            V.a = empty<int>(dim0: 0);
                        } else {
                            V.a = v[slice(0, foundN)];
                        }
                    }
                }
                if (!Equals(c, null)) {
                    lock (C.SynchObj) {
                        if (foundN == 0) {
                            C.a = empty<int>(dim0: 0);
                        } else {
                            C.a = c[slice(0, foundN)];
                        }
                    }
                }
                return ret[slice(0, foundN)];
            }
        }
       

        /// <summary>
        /// Finds sequential indices of non-zero elements in n-d array, search from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <returns>Sequential indices of the non-zero-valued elements in <paramref name="A"/>.</returns>
        
        internal static unsafe Array<int> find32(BaseArray<ushort> A) {

            if (Equals(A, null)) {
                throw new ArgumentException("Input array for 'find32' cannot be null.");
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>, out var storageA);

            if (storageA.S.NumberOfElements == 0) {
                return empty<int>(dim0: 0);
            } else if (storageA.S.NumberOfElements > int.MaxValue) {
                // this is here for Hycalper reasons only. It is required for the 32 bit overloads only. 
                throw new InvalidCastException($"The input array has too many elements for this overload of find3232(). Consider using find32() instead!");
            }
            var ret = Storage<int>.Create();
            int nrElements = (int)storageA.S.NumberOfElements; // assume maximum for now
            ret.S.SetAll(nrElements);
            DeviceManagement.DeviceManager.GetDevice(0).New<int>((ulong)nrElements, ret.Handles);

            int* outP = (int*)ret.Handles[0].Pointer;
            int i = 0;
            foreach (var b in storageA.AsRetArray().Iterator(StorageOrders.ColumnMajor)) { // A is released after iteration
                if (b != 0) {
                    *outP++ = i;
                }
                i++;
            }
            return ret.RetArray  /**/ [slice(0, outP - (int*)ret.Handles[0].Pointer)];
        }
        /// <summary>
        /// Find sequential indices of non-zero elements in n-d array, searches from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <param name="backwards">[Optional] Search direction backwards. Default: (false) start at the first element and search to the last element of <paramref name="A"/>.</param>
        /// <param name="C">[Optional] If non-null on entry: the function returns the row indices and gives column indices in <paramref name="C"/>. Default: (null) the function returns the sequential indices of elements found only.</param>
        /// <param name="n">[Optional] Maximum number of non-zero elements to find32. Default: (0) find32 all non-zero elements.</param>
        /// <param name="V">[Optional] Return the found non-zero element values in <paramref name="V"/>. Default: (null) don't return the values.</param>
        /// <returns>Sequential, column major ordered indices of the non-zero-valued elements in <paramref name="A"/> or row indices if <paramref name="C"/> is not null.</returns>
        /// <remarks><para>If <paramref name="C"/> or <paramref name="V"/> are not null on entry they are filled with the requested respective 
        /// info as column vectors. If <paramref name="C"/> is not null it contains the column indices of non-zero values. If <paramref name="A"/> 
        /// has more than two dimensions subsequent dimensions are merged into the second dimension and the indices given in <paramref name="C"/> 
        /// are considered sequential indices of <paramref name="A"/> (column major order). </para>
        /// <para>Note, that the array returned by <see cref="find32(BaseArray{ushort}, int, bool, OutArray{int}, OutArray{ushort})"/>
        /// corresponds to the indices of the rows of non-zero values if <paramref name="C"/> is not null and to sequential indices of those 
        /// non-zero values if <paramref name="C"/> is null. </para>
        /// <para>Negative values of <paramref name="n"/> are considered as abs(n).</para>
        /// <para>If <paramref name="backwards"/> is true <see cref="find32(BaseArray{ushort}, int, bool, OutArray{int}, OutArray{ushort})"/>
        /// starts searching for non-zero values from the last element and proceeds in column major order to the first element or until <paramref name="n"/> 
        /// non-zero elements were found, whatever comes first.</para>
        /// <para>Find() returns and considers indices as 64 bit <see cref="int"/>. Overloads exist for backwards compatibility which expect indices as <see cref="Int32"/>
        /// values. Such functions are decorated with the suffix '32' in their names.</para>
        /// </remarks>
        internal static unsafe Array<int> find32(BaseArray<ushort> A, int n = 0, bool backwards = false, OutArray<int> C = null, OutArray<ushort> V = null) {

            using (Scope.Enter()) {
                if (Equals(A, null)) {
                    throw new ArgumentException("Input array for 'find32' cannot be null.");
                }
                using var _1 = ReaderLock.Create(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>, out var storageA);
                //if (storageA.S.NumberOfDimensions > 2 && !Equals(C,null)) {
                //    return find3264(storageA.GetRange_ML(full, full, false), n, backwards, C, V); 
                //}
                #region  handle empty A
                if (storageA.S.NumberOfElements == 0) {
                    if (!Equals(C, null)) {
                        lock (C.SynchObj)
                            C.a = empty<int>(0);
                    }
                    if (!Equals(V, null)) {
                        lock (V.SynchObj)
                            V.a = empty<ushort>(dim0: 0);
                    }
                    return empty<int>(dim0: 0);
                } else if (storageA.S.NumberOfElements > int.MaxValue) {
                    throw new InvalidCastException($"The input array has too many elements for this overload of find32(). Consider using find32_64_ instead!");
                }
                #endregion

                if (n < 0) {
                    n = (int)Math.Abs(n);  // for hycalper...
                } else if (n == 0) {
                    n = (int)storageA.S.NumberOfElements; // assume maximum for now; 
                }
                if (n > storageA.S.NumberOfElements) {
                    n = (int)storageA.S.NumberOfElements;
                }
                Array<int> c = null;
                Array<ushort> v = null;

                // initialize working / out arrays with assumed maximum
                if (!Equals(C, null)) {
                    c = empty<int>(n);
                }
                if (!Equals(V, null)) {
                    v = empty<ushort>(n);
                }

                // initialize ret array with assumed maximum
                Array<int> ret = empty<int>(n);

                int* retP = (int*)ret.GetHostPointerForWrite();
                ushort* aP = (ushort*)storageA.Handles[0].Pointer; // size.GetSeqIndex contains storageA.S.BaseOffset;
                int* cP = (!Equals(c, null) ? (int*)c.GetHostPointerForWrite() : null);
                ushort* vP = (!Equals(v, null) ? (ushort*)v.GetHostPointerForWrite() : null);
                int lastElemDim2 = (int)(storageA.S.NumberOfElements / storageA.S[0]);
                int foundN = 0, strideA0 = (int)storageA.S[0];
                if (backwards == false) {
                    for (int ic = 0; ic < lastElemDim2 && foundN < n; ic++) {
                        for (int ir = 0; ir < strideA0 && foundN < n; ir++) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                } else {
                    for (int ic = lastElemDim2; ic-- > 0 && foundN < n;) {
                        for (int ir = (int)strideA0; ir-- > 0 && foundN < n;) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                }
                //int foundN = outP - (int*)ret.m_handles[0].Pointer;
                if (!Equals(v, null)) {
                    lock (V.SynchObj) {
                        if (foundN == 0) {
                            V.a = empty<ushort>(dim0: 0);
                        } else {
                            V.a = v[slice(0, foundN)];
                        }
                    }
                }
                if (!Equals(c, null)) {
                    lock (C.SynchObj) {
                        if (foundN == 0) {
                            C.a = empty<int>(dim0: 0);
                        } else {
                            C.a = c[slice(0, foundN)];
                        }
                    }
                }
                return ret[slice(0, foundN)];
            }
        }
       

        /// <summary>
        /// Finds sequential indices of non-zero elements in n-d array, search from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <returns>Sequential indices of the non-zero-valued elements in <paramref name="A"/>.</returns>
        
        internal static unsafe Array<int> find32(BaseArray<short> A) {

            if (Equals(A, null)) {
                throw new ArgumentException("Input array for 'find32' cannot be null.");
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>, out var storageA);

            if (storageA.S.NumberOfElements == 0) {
                return empty<int>(dim0: 0);
            } else if (storageA.S.NumberOfElements > int.MaxValue) {
                // this is here for Hycalper reasons only. It is required for the 32 bit overloads only. 
                throw new InvalidCastException($"The input array has too many elements for this overload of find3232(). Consider using find32() instead!");
            }
            var ret = Storage<int>.Create();
            int nrElements = (int)storageA.S.NumberOfElements; // assume maximum for now
            ret.S.SetAll(nrElements);
            DeviceManagement.DeviceManager.GetDevice(0).New<int>((ulong)nrElements, ret.Handles);

            int* outP = (int*)ret.Handles[0].Pointer;
            int i = 0;
            foreach (var b in storageA.AsRetArray().Iterator(StorageOrders.ColumnMajor)) { // A is released after iteration
                if (b != 0) {
                    *outP++ = i;
                }
                i++;
            }
            return ret.RetArray  /**/ [slice(0, outP - (int*)ret.Handles[0].Pointer)];
        }
        /// <summary>
        /// Find sequential indices of non-zero elements in n-d array, searches from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <param name="backwards">[Optional] Search direction backwards. Default: (false) start at the first element and search to the last element of <paramref name="A"/>.</param>
        /// <param name="C">[Optional] If non-null on entry: the function returns the row indices and gives column indices in <paramref name="C"/>. Default: (null) the function returns the sequential indices of elements found only.</param>
        /// <param name="n">[Optional] Maximum number of non-zero elements to find32. Default: (0) find32 all non-zero elements.</param>
        /// <param name="V">[Optional] Return the found non-zero element values in <paramref name="V"/>. Default: (null) don't return the values.</param>
        /// <returns>Sequential, column major ordered indices of the non-zero-valued elements in <paramref name="A"/> or row indices if <paramref name="C"/> is not null.</returns>
        /// <remarks><para>If <paramref name="C"/> or <paramref name="V"/> are not null on entry they are filled with the requested respective 
        /// info as column vectors. If <paramref name="C"/> is not null it contains the column indices of non-zero values. If <paramref name="A"/> 
        /// has more than two dimensions subsequent dimensions are merged into the second dimension and the indices given in <paramref name="C"/> 
        /// are considered sequential indices of <paramref name="A"/> (column major order). </para>
        /// <para>Note, that the array returned by <see cref="find32(BaseArray{short}, int, bool, OutArray{int}, OutArray{short})"/>
        /// corresponds to the indices of the rows of non-zero values if <paramref name="C"/> is not null and to sequential indices of those 
        /// non-zero values if <paramref name="C"/> is null. </para>
        /// <para>Negative values of <paramref name="n"/> are considered as abs(n).</para>
        /// <para>If <paramref name="backwards"/> is true <see cref="find32(BaseArray{short}, int, bool, OutArray{int}, OutArray{short})"/>
        /// starts searching for non-zero values from the last element and proceeds in column major order to the first element or until <paramref name="n"/> 
        /// non-zero elements were found, whatever comes first.</para>
        /// <para>Find() returns and considers indices as 64 bit <see cref="int"/>. Overloads exist for backwards compatibility which expect indices as <see cref="Int32"/>
        /// values. Such functions are decorated with the suffix '32' in their names.</para>
        /// </remarks>
        internal static unsafe Array<int> find32(BaseArray<short> A, int n = 0, bool backwards = false, OutArray<int> C = null, OutArray<short> V = null) {

            using (Scope.Enter()) {
                if (Equals(A, null)) {
                    throw new ArgumentException("Input array for 'find32' cannot be null.");
                }
                using var _1 = ReaderLock.Create(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>, out var storageA);
                //if (storageA.S.NumberOfDimensions > 2 && !Equals(C,null)) {
                //    return find3264(storageA.GetRange_ML(full, full, false), n, backwards, C, V); 
                //}
                #region  handle empty A
                if (storageA.S.NumberOfElements == 0) {
                    if (!Equals(C, null)) {
                        lock (C.SynchObj)
                            C.a = empty<int>(0);
                    }
                    if (!Equals(V, null)) {
                        lock (V.SynchObj)
                            V.a = empty<short>(dim0: 0);
                    }
                    return empty<int>(dim0: 0);
                } else if (storageA.S.NumberOfElements > int.MaxValue) {
                    throw new InvalidCastException($"The input array has too many elements for this overload of find32(). Consider using find32_64_ instead!");
                }
                #endregion

                if (n < 0) {
                    n = (int)Math.Abs(n);  // for hycalper...
                } else if (n == 0) {
                    n = (int)storageA.S.NumberOfElements; // assume maximum for now; 
                }
                if (n > storageA.S.NumberOfElements) {
                    n = (int)storageA.S.NumberOfElements;
                }
                Array<int> c = null;
                Array<short> v = null;

                // initialize working / out arrays with assumed maximum
                if (!Equals(C, null)) {
                    c = empty<int>(n);
                }
                if (!Equals(V, null)) {
                    v = empty<short>(n);
                }

                // initialize ret array with assumed maximum
                Array<int> ret = empty<int>(n);

                int* retP = (int*)ret.GetHostPointerForWrite();
                short* aP = (short*)storageA.Handles[0].Pointer; // size.GetSeqIndex contains storageA.S.BaseOffset;
                int* cP = (!Equals(c, null) ? (int*)c.GetHostPointerForWrite() : null);
                short* vP = (!Equals(v, null) ? (short*)v.GetHostPointerForWrite() : null);
                int lastElemDim2 = (int)(storageA.S.NumberOfElements / storageA.S[0]);
                int foundN = 0, strideA0 = (int)storageA.S[0];
                if (backwards == false) {
                    for (int ic = 0; ic < lastElemDim2 && foundN < n; ic++) {
                        for (int ir = 0; ir < strideA0 && foundN < n; ir++) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                } else {
                    for (int ic = lastElemDim2; ic-- > 0 && foundN < n;) {
                        for (int ir = (int)strideA0; ir-- > 0 && foundN < n;) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                }
                //int foundN = outP - (int*)ret.m_handles[0].Pointer;
                if (!Equals(v, null)) {
                    lock (V.SynchObj) {
                        if (foundN == 0) {
                            V.a = empty<short>(dim0: 0);
                        } else {
                            V.a = v[slice(0, foundN)];
                        }
                    }
                }
                if (!Equals(c, null)) {
                    lock (C.SynchObj) {
                        if (foundN == 0) {
                            C.a = empty<int>(dim0: 0);
                        } else {
                            C.a = c[slice(0, foundN)];
                        }
                    }
                }
                return ret[slice(0, foundN)];
            }
        }
       

        /// <summary>
        /// Finds sequential indices of non-zero elements in n-d array, search from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <returns>Sequential indices of the non-zero-valued elements in <paramref name="A"/>.</returns>
        
        internal static unsafe Array<int> find32(BaseArray<byte> A) {

            if (Equals(A, null)) {
                throw new ArgumentException("Input array for 'find32' cannot be null.");
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>, out var storageA);

            if (storageA.S.NumberOfElements == 0) {
                return empty<int>(dim0: 0);
            } else if (storageA.S.NumberOfElements > int.MaxValue) {
                // this is here for Hycalper reasons only. It is required for the 32 bit overloads only. 
                throw new InvalidCastException($"The input array has too many elements for this overload of find3232(). Consider using find32() instead!");
            }
            var ret = Storage<int>.Create();
            int nrElements = (int)storageA.S.NumberOfElements; // assume maximum for now
            ret.S.SetAll(nrElements);
            DeviceManagement.DeviceManager.GetDevice(0).New<int>((ulong)nrElements, ret.Handles);

            int* outP = (int*)ret.Handles[0].Pointer;
            int i = 0;
            foreach (var b in storageA.AsRetArray().Iterator(StorageOrders.ColumnMajor)) { // A is released after iteration
                if (b != 0) {
                    *outP++ = i;
                }
                i++;
            }
            return ret.RetArray  /**/ [slice(0, outP - (int*)ret.Handles[0].Pointer)];
        }
        /// <summary>
        /// Find sequential indices of non-zero elements in n-d array, searches from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <param name="backwards">[Optional] Search direction backwards. Default: (false) start at the first element and search to the last element of <paramref name="A"/>.</param>
        /// <param name="C">[Optional] If non-null on entry: the function returns the row indices and gives column indices in <paramref name="C"/>. Default: (null) the function returns the sequential indices of elements found only.</param>
        /// <param name="n">[Optional] Maximum number of non-zero elements to find32. Default: (0) find32 all non-zero elements.</param>
        /// <param name="V">[Optional] Return the found non-zero element values in <paramref name="V"/>. Default: (null) don't return the values.</param>
        /// <returns>Sequential, column major ordered indices of the non-zero-valued elements in <paramref name="A"/> or row indices if <paramref name="C"/> is not null.</returns>
        /// <remarks><para>If <paramref name="C"/> or <paramref name="V"/> are not null on entry they are filled with the requested respective 
        /// info as column vectors. If <paramref name="C"/> is not null it contains the column indices of non-zero values. If <paramref name="A"/> 
        /// has more than two dimensions subsequent dimensions are merged into the second dimension and the indices given in <paramref name="C"/> 
        /// are considered sequential indices of <paramref name="A"/> (column major order). </para>
        /// <para>Note, that the array returned by <see cref="find32(BaseArray{byte}, int, bool, OutArray{int}, OutArray{byte})"/>
        /// corresponds to the indices of the rows of non-zero values if <paramref name="C"/> is not null and to sequential indices of those 
        /// non-zero values if <paramref name="C"/> is null. </para>
        /// <para>Negative values of <paramref name="n"/> are considered as abs(n).</para>
        /// <para>If <paramref name="backwards"/> is true <see cref="find32(BaseArray{byte}, int, bool, OutArray{int}, OutArray{byte})"/>
        /// starts searching for non-zero values from the last element and proceeds in column major order to the first element or until <paramref name="n"/> 
        /// non-zero elements were found, whatever comes first.</para>
        /// <para>Find() returns and considers indices as 64 bit <see cref="int"/>. Overloads exist for backwards compatibility which expect indices as <see cref="Int32"/>
        /// values. Such functions are decorated with the suffix '32' in their names.</para>
        /// </remarks>
        internal static unsafe Array<int> find32(BaseArray<byte> A, int n = 0, bool backwards = false, OutArray<int> C = null, OutArray<byte> V = null) {

            using (Scope.Enter()) {
                if (Equals(A, null)) {
                    throw new ArgumentException("Input array for 'find32' cannot be null.");
                }
                using var _1 = ReaderLock.Create(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>, out var storageA);
                //if (storageA.S.NumberOfDimensions > 2 && !Equals(C,null)) {
                //    return find3264(storageA.GetRange_ML(full, full, false), n, backwards, C, V); 
                //}
                #region  handle empty A
                if (storageA.S.NumberOfElements == 0) {
                    if (!Equals(C, null)) {
                        lock (C.SynchObj)
                            C.a = empty<int>(0);
                    }
                    if (!Equals(V, null)) {
                        lock (V.SynchObj)
                            V.a = empty<byte>(dim0: 0);
                    }
                    return empty<int>(dim0: 0);
                } else if (storageA.S.NumberOfElements > int.MaxValue) {
                    throw new InvalidCastException($"The input array has too many elements for this overload of find32(). Consider using find32_64_ instead!");
                }
                #endregion

                if (n < 0) {
                    n = (int)Math.Abs(n);  // for hycalper...
                } else if (n == 0) {
                    n = (int)storageA.S.NumberOfElements; // assume maximum for now; 
                }
                if (n > storageA.S.NumberOfElements) {
                    n = (int)storageA.S.NumberOfElements;
                }
                Array<int> c = null;
                Array<byte> v = null;

                // initialize working / out arrays with assumed maximum
                if (!Equals(C, null)) {
                    c = empty<int>(n);
                }
                if (!Equals(V, null)) {
                    v = empty<byte>(n);
                }

                // initialize ret array with assumed maximum
                Array<int> ret = empty<int>(n);

                int* retP = (int*)ret.GetHostPointerForWrite();
                byte* aP = (byte*)storageA.Handles[0].Pointer; // size.GetSeqIndex contains storageA.S.BaseOffset;
                int* cP = (!Equals(c, null) ? (int*)c.GetHostPointerForWrite() : null);
                byte* vP = (!Equals(v, null) ? (byte*)v.GetHostPointerForWrite() : null);
                int lastElemDim2 = (int)(storageA.S.NumberOfElements / storageA.S[0]);
                int foundN = 0, strideA0 = (int)storageA.S[0];
                if (backwards == false) {
                    for (int ic = 0; ic < lastElemDim2 && foundN < n; ic++) {
                        for (int ir = 0; ir < strideA0 && foundN < n; ir++) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                } else {
                    for (int ic = lastElemDim2; ic-- > 0 && foundN < n;) {
                        for (int ir = (int)strideA0; ir-- > 0 && foundN < n;) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                }
                //int foundN = outP - (int*)ret.m_handles[0].Pointer;
                if (!Equals(v, null)) {
                    lock (V.SynchObj) {
                        if (foundN == 0) {
                            V.a = empty<byte>(dim0: 0);
                        } else {
                            V.a = v[slice(0, foundN)];
                        }
                    }
                }
                if (!Equals(c, null)) {
                    lock (C.SynchObj) {
                        if (foundN == 0) {
                            C.a = empty<int>(dim0: 0);
                        } else {
                            C.a = c[slice(0, foundN)];
                        }
                    }
                }
                return ret[slice(0, foundN)];
            }
        }
       

        /// <summary>
        /// Finds sequential indices of non-zero elements in n-d array, search from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <returns>Sequential indices of the non-zero-valued elements in <paramref name="A"/>.</returns>
        
        internal static unsafe Array<int> find32(BaseArray<sbyte> A) {

            if (Equals(A, null)) {
                throw new ArgumentException("Input array for 'find32' cannot be null.");
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>, out var storageA);

            if (storageA.S.NumberOfElements == 0) {
                return empty<int>(dim0: 0);
            } else if (storageA.S.NumberOfElements > int.MaxValue) {
                // this is here for Hycalper reasons only. It is required for the 32 bit overloads only. 
                throw new InvalidCastException($"The input array has too many elements for this overload of find3232(). Consider using find32() instead!");
            }
            var ret = Storage<int>.Create();
            int nrElements = (int)storageA.S.NumberOfElements; // assume maximum for now
            ret.S.SetAll(nrElements);
            DeviceManagement.DeviceManager.GetDevice(0).New<int>((ulong)nrElements, ret.Handles);

            int* outP = (int*)ret.Handles[0].Pointer;
            int i = 0;
            foreach (var b in storageA.AsRetArray().Iterator(StorageOrders.ColumnMajor)) { // A is released after iteration
                if (b != 0) {
                    *outP++ = i;
                }
                i++;
            }
            return ret.RetArray  /**/ [slice(0, outP - (int*)ret.Handles[0].Pointer)];
        }
        /// <summary>
        /// Find sequential indices of non-zero elements in n-d array, searches from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <param name="backwards">[Optional] Search direction backwards. Default: (false) start at the first element and search to the last element of <paramref name="A"/>.</param>
        /// <param name="C">[Optional] If non-null on entry: the function returns the row indices and gives column indices in <paramref name="C"/>. Default: (null) the function returns the sequential indices of elements found only.</param>
        /// <param name="n">[Optional] Maximum number of non-zero elements to find32. Default: (0) find32 all non-zero elements.</param>
        /// <param name="V">[Optional] Return the found non-zero element values in <paramref name="V"/>. Default: (null) don't return the values.</param>
        /// <returns>Sequential, column major ordered indices of the non-zero-valued elements in <paramref name="A"/> or row indices if <paramref name="C"/> is not null.</returns>
        /// <remarks><para>If <paramref name="C"/> or <paramref name="V"/> are not null on entry they are filled with the requested respective 
        /// info as column vectors. If <paramref name="C"/> is not null it contains the column indices of non-zero values. If <paramref name="A"/> 
        /// has more than two dimensions subsequent dimensions are merged into the second dimension and the indices given in <paramref name="C"/> 
        /// are considered sequential indices of <paramref name="A"/> (column major order). </para>
        /// <para>Note, that the array returned by <see cref="find32(BaseArray{sbyte}, int, bool, OutArray{int}, OutArray{sbyte})"/>
        /// corresponds to the indices of the rows of non-zero values if <paramref name="C"/> is not null and to sequential indices of those 
        /// non-zero values if <paramref name="C"/> is null. </para>
        /// <para>Negative values of <paramref name="n"/> are considered as abs(n).</para>
        /// <para>If <paramref name="backwards"/> is true <see cref="find32(BaseArray{sbyte}, int, bool, OutArray{int}, OutArray{sbyte})"/>
        /// starts searching for non-zero values from the last element and proceeds in column major order to the first element or until <paramref name="n"/> 
        /// non-zero elements were found, whatever comes first.</para>
        /// <para>Find() returns and considers indices as 64 bit <see cref="int"/>. Overloads exist for backwards compatibility which expect indices as <see cref="Int32"/>
        /// values. Such functions are decorated with the suffix '32' in their names.</para>
        /// </remarks>
        internal static unsafe Array<int> find32(BaseArray<sbyte> A, int n = 0, bool backwards = false, OutArray<int> C = null, OutArray<sbyte> V = null) {

            using (Scope.Enter()) {
                if (Equals(A, null)) {
                    throw new ArgumentException("Input array for 'find32' cannot be null.");
                }
                using var _1 = ReaderLock.Create(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>, out var storageA);
                //if (storageA.S.NumberOfDimensions > 2 && !Equals(C,null)) {
                //    return find3264(storageA.GetRange_ML(full, full, false), n, backwards, C, V); 
                //}
                #region  handle empty A
                if (storageA.S.NumberOfElements == 0) {
                    if (!Equals(C, null)) {
                        lock (C.SynchObj)
                            C.a = empty<int>(0);
                    }
                    if (!Equals(V, null)) {
                        lock (V.SynchObj)
                            V.a = empty<sbyte>(dim0: 0);
                    }
                    return empty<int>(dim0: 0);
                } else if (storageA.S.NumberOfElements > int.MaxValue) {
                    throw new InvalidCastException($"The input array has too many elements for this overload of find32(). Consider using find32_64_ instead!");
                }
                #endregion

                if (n < 0) {
                    n = (int)Math.Abs(n);  // for hycalper...
                } else if (n == 0) {
                    n = (int)storageA.S.NumberOfElements; // assume maximum for now; 
                }
                if (n > storageA.S.NumberOfElements) {
                    n = (int)storageA.S.NumberOfElements;
                }
                Array<int> c = null;
                Array<sbyte> v = null;

                // initialize working / out arrays with assumed maximum
                if (!Equals(C, null)) {
                    c = empty<int>(n);
                }
                if (!Equals(V, null)) {
                    v = empty<sbyte>(n);
                }

                // initialize ret array with assumed maximum
                Array<int> ret = empty<int>(n);

                int* retP = (int*)ret.GetHostPointerForWrite();
                sbyte* aP = (sbyte*)storageA.Handles[0].Pointer; // size.GetSeqIndex contains storageA.S.BaseOffset;
                int* cP = (!Equals(c, null) ? (int*)c.GetHostPointerForWrite() : null);
                sbyte* vP = (!Equals(v, null) ? (sbyte*)v.GetHostPointerForWrite() : null);
                int lastElemDim2 = (int)(storageA.S.NumberOfElements / storageA.S[0]);
                int foundN = 0, strideA0 = (int)storageA.S[0];
                if (backwards == false) {
                    for (int ic = 0; ic < lastElemDim2 && foundN < n; ic++) {
                        for (int ir = 0; ir < strideA0 && foundN < n; ir++) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                } else {
                    for (int ic = lastElemDim2; ic-- > 0 && foundN < n;) {
                        for (int ir = (int)strideA0; ir-- > 0 && foundN < n;) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                }
                //int foundN = outP - (int*)ret.m_handles[0].Pointer;
                if (!Equals(v, null)) {
                    lock (V.SynchObj) {
                        if (foundN == 0) {
                            V.a = empty<sbyte>(dim0: 0);
                        } else {
                            V.a = v[slice(0, foundN)];
                        }
                    }
                }
                if (!Equals(c, null)) {
                    lock (C.SynchObj) {
                        if (foundN == 0) {
                            C.a = empty<int>(dim0: 0);
                        } else {
                            C.a = c[slice(0, foundN)];
                        }
                    }
                }
                return ret[slice(0, foundN)];
            }
        }
       

        /// <summary>
        /// Finds sequential indices of non-zero elements in n-d array, search from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <returns>Sequential indices of the non-zero-valued elements in <paramref name="A"/>.</returns>
        
        internal static unsafe Array<int> find32(BaseArray<fcomplex> A) {

            if (Equals(A, null)) {
                throw new ArgumentException("Input array for 'find32' cannot be null.");
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, out var storageA);

            if (storageA.S.NumberOfElements == 0) {
                return empty<int>(dim0: 0);
            } else if (storageA.S.NumberOfElements > int.MaxValue) {
                // this is here for Hycalper reasons only. It is required for the 32 bit overloads only. 
                throw new InvalidCastException($"The input array has too many elements for this overload of find3232(). Consider using find32() instead!");
            }
            var ret = Storage<int>.Create();
            int nrElements = (int)storageA.S.NumberOfElements; // assume maximum for now
            ret.S.SetAll(nrElements);
            DeviceManagement.DeviceManager.GetDevice(0).New<int>((ulong)nrElements, ret.Handles);

            int* outP = (int*)ret.Handles[0].Pointer;
            int i = 0;
            foreach (var b in storageA.AsRetArray().Iterator(StorageOrders.ColumnMajor)) { // A is released after iteration
                if (b != 0) {
                    *outP++ = i;
                }
                i++;
            }
            return ret.RetArray  /**/ [slice(0, outP - (int*)ret.Handles[0].Pointer)];
        }
        /// <summary>
        /// Find sequential indices of non-zero elements in n-d array, searches from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <param name="backwards">[Optional] Search direction backwards. Default: (false) start at the first element and search to the last element of <paramref name="A"/>.</param>
        /// <param name="C">[Optional] If non-null on entry: the function returns the row indices and gives column indices in <paramref name="C"/>. Default: (null) the function returns the sequential indices of elements found only.</param>
        /// <param name="n">[Optional] Maximum number of non-zero elements to find32. Default: (0) find32 all non-zero elements.</param>
        /// <param name="V">[Optional] Return the found non-zero element values in <paramref name="V"/>. Default: (null) don't return the values.</param>
        /// <returns>Sequential, column major ordered indices of the non-zero-valued elements in <paramref name="A"/> or row indices if <paramref name="C"/> is not null.</returns>
        /// <remarks><para>If <paramref name="C"/> or <paramref name="V"/> are not null on entry they are filled with the requested respective 
        /// info as column vectors. If <paramref name="C"/> is not null it contains the column indices of non-zero values. If <paramref name="A"/> 
        /// has more than two dimensions subsequent dimensions are merged into the second dimension and the indices given in <paramref name="C"/> 
        /// are considered sequential indices of <paramref name="A"/> (column major order). </para>
        /// <para>Note, that the array returned by <see cref="find32(BaseArray{fcomplex}, int, bool, OutArray{int}, OutArray{fcomplex})"/>
        /// corresponds to the indices of the rows of non-zero values if <paramref name="C"/> is not null and to sequential indices of those 
        /// non-zero values if <paramref name="C"/> is null. </para>
        /// <para>Negative values of <paramref name="n"/> are considered as abs(n).</para>
        /// <para>If <paramref name="backwards"/> is true <see cref="find32(BaseArray{fcomplex}, int, bool, OutArray{int}, OutArray{fcomplex})"/>
        /// starts searching for non-zero values from the last element and proceeds in column major order to the first element or until <paramref name="n"/> 
        /// non-zero elements were found, whatever comes first.</para>
        /// <para>Find() returns and considers indices as 64 bit <see cref="int"/>. Overloads exist for backwards compatibility which expect indices as <see cref="Int32"/>
        /// values. Such functions are decorated with the suffix '32' in their names.</para>
        /// </remarks>
        internal static unsafe Array<int> find32(BaseArray<fcomplex> A, int n = 0, bool backwards = false, OutArray<int> C = null, OutArray<fcomplex> V = null) {

            using (Scope.Enter()) {
                if (Equals(A, null)) {
                    throw new ArgumentException("Input array for 'find32' cannot be null.");
                }
                using var _1 = ReaderLock.Create(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, out var storageA);
                //if (storageA.S.NumberOfDimensions > 2 && !Equals(C,null)) {
                //    return find3264(storageA.GetRange_ML(full, full, false), n, backwards, C, V); 
                //}
                #region  handle empty A
                if (storageA.S.NumberOfElements == 0) {
                    if (!Equals(C, null)) {
                        lock (C.SynchObj)
                            C.a = empty<int>(0);
                    }
                    if (!Equals(V, null)) {
                        lock (V.SynchObj)
                            V.a = empty<fcomplex>(dim0: 0);
                    }
                    return empty<int>(dim0: 0);
                } else if (storageA.S.NumberOfElements > int.MaxValue) {
                    throw new InvalidCastException($"The input array has too many elements for this overload of find32(). Consider using find32_64_ instead!");
                }
                #endregion

                if (n < 0) {
                    n = (int)Math.Abs(n);  // for hycalper...
                } else if (n == 0) {
                    n = (int)storageA.S.NumberOfElements; // assume maximum for now; 
                }
                if (n > storageA.S.NumberOfElements) {
                    n = (int)storageA.S.NumberOfElements;
                }
                Array<int> c = null;
                Array<fcomplex> v = null;

                // initialize working / out arrays with assumed maximum
                if (!Equals(C, null)) {
                    c = empty<int>(n);
                }
                if (!Equals(V, null)) {
                    v = empty<fcomplex>(n);
                }

                // initialize ret array with assumed maximum
                Array<int> ret = empty<int>(n);

                int* retP = (int*)ret.GetHostPointerForWrite();
                fcomplex* aP = (fcomplex*)storageA.Handles[0].Pointer; // size.GetSeqIndex contains storageA.S.BaseOffset;
                int* cP = (!Equals(c, null) ? (int*)c.GetHostPointerForWrite() : null);
                fcomplex* vP = (!Equals(v, null) ? (fcomplex*)v.GetHostPointerForWrite() : null);
                int lastElemDim2 = (int)(storageA.S.NumberOfElements / storageA.S[0]);
                int foundN = 0, strideA0 = (int)storageA.S[0];
                if (backwards == false) {
                    for (int ic = 0; ic < lastElemDim2 && foundN < n; ic++) {
                        for (int ir = 0; ir < strideA0 && foundN < n; ir++) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                } else {
                    for (int ic = lastElemDim2; ic-- > 0 && foundN < n;) {
                        for (int ir = (int)strideA0; ir-- > 0 && foundN < n;) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                }
                //int foundN = outP - (int*)ret.m_handles[0].Pointer;
                if (!Equals(v, null)) {
                    lock (V.SynchObj) {
                        if (foundN == 0) {
                            V.a = empty<fcomplex>(dim0: 0);
                        } else {
                            V.a = v[slice(0, foundN)];
                        }
                    }
                }
                if (!Equals(c, null)) {
                    lock (C.SynchObj) {
                        if (foundN == 0) {
                            C.a = empty<int>(dim0: 0);
                        } else {
                            C.a = c[slice(0, foundN)];
                        }
                    }
                }
                return ret[slice(0, foundN)];
            }
        }
       

        /// <summary>
        /// Finds sequential indices of non-zero elements in n-d array, search from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <returns>Sequential indices of the non-zero-valued elements in <paramref name="A"/>.</returns>
        
        internal static unsafe Array<int> find32(BaseArray<complex> A) {

            if (Equals(A, null)) {
                throw new ArgumentException("Input array for 'find32' cannot be null.");
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, out var storageA);

            if (storageA.S.NumberOfElements == 0) {
                return empty<int>(dim0: 0);
            } else if (storageA.S.NumberOfElements > int.MaxValue) {
                // this is here for Hycalper reasons only. It is required for the 32 bit overloads only. 
                throw new InvalidCastException($"The input array has too many elements for this overload of find3232(). Consider using find32() instead!");
            }
            var ret = Storage<int>.Create();
            int nrElements = (int)storageA.S.NumberOfElements; // assume maximum for now
            ret.S.SetAll(nrElements);
            DeviceManagement.DeviceManager.GetDevice(0).New<int>((ulong)nrElements, ret.Handles);

            int* outP = (int*)ret.Handles[0].Pointer;
            int i = 0;
            foreach (var b in storageA.AsRetArray().Iterator(StorageOrders.ColumnMajor)) { // A is released after iteration
                if (b != 0) {
                    *outP++ = i;
                }
                i++;
            }
            return ret.RetArray  /**/ [slice(0, outP - (int*)ret.Handles[0].Pointer)];
        }
        /// <summary>
        /// Find sequential indices of non-zero elements in n-d array, searches from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <param name="backwards">[Optional] Search direction backwards. Default: (false) start at the first element and search to the last element of <paramref name="A"/>.</param>
        /// <param name="C">[Optional] If non-null on entry: the function returns the row indices and gives column indices in <paramref name="C"/>. Default: (null) the function returns the sequential indices of elements found only.</param>
        /// <param name="n">[Optional] Maximum number of non-zero elements to find32. Default: (0) find32 all non-zero elements.</param>
        /// <param name="V">[Optional] Return the found non-zero element values in <paramref name="V"/>. Default: (null) don't return the values.</param>
        /// <returns>Sequential, column major ordered indices of the non-zero-valued elements in <paramref name="A"/> or row indices if <paramref name="C"/> is not null.</returns>
        /// <remarks><para>If <paramref name="C"/> or <paramref name="V"/> are not null on entry they are filled with the requested respective 
        /// info as column vectors. If <paramref name="C"/> is not null it contains the column indices of non-zero values. If <paramref name="A"/> 
        /// has more than two dimensions subsequent dimensions are merged into the second dimension and the indices given in <paramref name="C"/> 
        /// are considered sequential indices of <paramref name="A"/> (column major order). </para>
        /// <para>Note, that the array returned by <see cref="find32(BaseArray{complex}, int, bool, OutArray{int}, OutArray{complex})"/>
        /// corresponds to the indices of the rows of non-zero values if <paramref name="C"/> is not null and to sequential indices of those 
        /// non-zero values if <paramref name="C"/> is null. </para>
        /// <para>Negative values of <paramref name="n"/> are considered as abs(n).</para>
        /// <para>If <paramref name="backwards"/> is true <see cref="find32(BaseArray{complex}, int, bool, OutArray{int}, OutArray{complex})"/>
        /// starts searching for non-zero values from the last element and proceeds in column major order to the first element or until <paramref name="n"/> 
        /// non-zero elements were found, whatever comes first.</para>
        /// <para>Find() returns and considers indices as 64 bit <see cref="int"/>. Overloads exist for backwards compatibility which expect indices as <see cref="Int32"/>
        /// values. Such functions are decorated with the suffix '32' in their names.</para>
        /// </remarks>
        internal static unsafe Array<int> find32(BaseArray<complex> A, int n = 0, bool backwards = false, OutArray<int> C = null, OutArray<complex> V = null) {

            using (Scope.Enter()) {
                if (Equals(A, null)) {
                    throw new ArgumentException("Input array for 'find32' cannot be null.");
                }
                using var _1 = ReaderLock.Create(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, out var storageA);
                //if (storageA.S.NumberOfDimensions > 2 && !Equals(C,null)) {
                //    return find3264(storageA.GetRange_ML(full, full, false), n, backwards, C, V); 
                //}
                #region  handle empty A
                if (storageA.S.NumberOfElements == 0) {
                    if (!Equals(C, null)) {
                        lock (C.SynchObj)
                            C.a = empty<int>(0);
                    }
                    if (!Equals(V, null)) {
                        lock (V.SynchObj)
                            V.a = empty<complex>(dim0: 0);
                    }
                    return empty<int>(dim0: 0);
                } else if (storageA.S.NumberOfElements > int.MaxValue) {
                    throw new InvalidCastException($"The input array has too many elements for this overload of find32(). Consider using find32_64_ instead!");
                }
                #endregion

                if (n < 0) {
                    n = (int)Math.Abs(n);  // for hycalper...
                } else if (n == 0) {
                    n = (int)storageA.S.NumberOfElements; // assume maximum for now; 
                }
                if (n > storageA.S.NumberOfElements) {
                    n = (int)storageA.S.NumberOfElements;
                }
                Array<int> c = null;
                Array<complex> v = null;

                // initialize working / out arrays with assumed maximum
                if (!Equals(C, null)) {
                    c = empty<int>(n);
                }
                if (!Equals(V, null)) {
                    v = empty<complex>(n);
                }

                // initialize ret array with assumed maximum
                Array<int> ret = empty<int>(n);

                int* retP = (int*)ret.GetHostPointerForWrite();
                complex* aP = (complex*)storageA.Handles[0].Pointer; // size.GetSeqIndex contains storageA.S.BaseOffset;
                int* cP = (!Equals(c, null) ? (int*)c.GetHostPointerForWrite() : null);
                complex* vP = (!Equals(v, null) ? (complex*)v.GetHostPointerForWrite() : null);
                int lastElemDim2 = (int)(storageA.S.NumberOfElements / storageA.S[0]);
                int foundN = 0, strideA0 = (int)storageA.S[0];
                if (backwards == false) {
                    for (int ic = 0; ic < lastElemDim2 && foundN < n; ic++) {
                        for (int ir = 0; ir < strideA0 && foundN < n; ir++) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                } else {
                    for (int ic = lastElemDim2; ic-- > 0 && foundN < n;) {
                        for (int ir = (int)strideA0; ir-- > 0 && foundN < n;) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                }
                //int foundN = outP - (int*)ret.m_handles[0].Pointer;
                if (!Equals(v, null)) {
                    lock (V.SynchObj) {
                        if (foundN == 0) {
                            V.a = empty<complex>(dim0: 0);
                        } else {
                            V.a = v[slice(0, foundN)];
                        }
                    }
                }
                if (!Equals(c, null)) {
                    lock (C.SynchObj) {
                        if (foundN == 0) {
                            C.a = empty<int>(dim0: 0);
                        } else {
                            C.a = c[slice(0, foundN)];
                        }
                    }
                }
                return ret[slice(0, foundN)];
            }
        }
       

        /// <summary>
        /// Finds sequential indices of non-zero elements in n-d array, search from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <returns>Sequential indices of the non-zero-valued elements in <paramref name="A"/>.</returns>
        
        internal static unsafe Array<int> find32(BaseArray<float> A) {

            if (Equals(A, null)) {
                throw new ArgumentException("Input array for 'find32' cannot be null.");
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, out var storageA);

            if (storageA.S.NumberOfElements == 0) {
                return empty<int>(dim0: 0);
            } else if (storageA.S.NumberOfElements > int.MaxValue) {
                // this is here for Hycalper reasons only. It is required for the 32 bit overloads only. 
                throw new InvalidCastException($"The input array has too many elements for this overload of find3232(). Consider using find32() instead!");
            }
            var ret = Storage<int>.Create();
            int nrElements = (int)storageA.S.NumberOfElements; // assume maximum for now
            ret.S.SetAll(nrElements);
            DeviceManagement.DeviceManager.GetDevice(0).New<int>((ulong)nrElements, ret.Handles);

            int* outP = (int*)ret.Handles[0].Pointer;
            int i = 0;
            foreach (var b in storageA.AsRetArray().Iterator(StorageOrders.ColumnMajor)) { // A is released after iteration
                if (b != 0) {
                    *outP++ = i;
                }
                i++;
            }
            return ret.RetArray  /**/ [slice(0, outP - (int*)ret.Handles[0].Pointer)];
        }
        /// <summary>
        /// Find sequential indices of non-zero elements in n-d array, searches from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <param name="backwards">[Optional] Search direction backwards. Default: (false) start at the first element and search to the last element of <paramref name="A"/>.</param>
        /// <param name="C">[Optional] If non-null on entry: the function returns the row indices and gives column indices in <paramref name="C"/>. Default: (null) the function returns the sequential indices of elements found only.</param>
        /// <param name="n">[Optional] Maximum number of non-zero elements to find32. Default: (0) find32 all non-zero elements.</param>
        /// <param name="V">[Optional] Return the found non-zero element values in <paramref name="V"/>. Default: (null) don't return the values.</param>
        /// <returns>Sequential, column major ordered indices of the non-zero-valued elements in <paramref name="A"/> or row indices if <paramref name="C"/> is not null.</returns>
        /// <remarks><para>If <paramref name="C"/> or <paramref name="V"/> are not null on entry they are filled with the requested respective 
        /// info as column vectors. If <paramref name="C"/> is not null it contains the column indices of non-zero values. If <paramref name="A"/> 
        /// has more than two dimensions subsequent dimensions are merged into the second dimension and the indices given in <paramref name="C"/> 
        /// are considered sequential indices of <paramref name="A"/> (column major order). </para>
        /// <para>Note, that the array returned by <see cref="find32(BaseArray{float}, int, bool, OutArray{int}, OutArray{float})"/>
        /// corresponds to the indices of the rows of non-zero values if <paramref name="C"/> is not null and to sequential indices of those 
        /// non-zero values if <paramref name="C"/> is null. </para>
        /// <para>Negative values of <paramref name="n"/> are considered as abs(n).</para>
        /// <para>If <paramref name="backwards"/> is true <see cref="find32(BaseArray{float}, int, bool, OutArray{int}, OutArray{float})"/>
        /// starts searching for non-zero values from the last element and proceeds in column major order to the first element or until <paramref name="n"/> 
        /// non-zero elements were found, whatever comes first.</para>
        /// <para>Find() returns and considers indices as 64 bit <see cref="int"/>. Overloads exist for backwards compatibility which expect indices as <see cref="Int32"/>
        /// values. Such functions are decorated with the suffix '32' in their names.</para>
        /// </remarks>
        internal static unsafe Array<int> find32(BaseArray<float> A, int n = 0, bool backwards = false, OutArray<int> C = null, OutArray<float> V = null) {

            using (Scope.Enter()) {
                if (Equals(A, null)) {
                    throw new ArgumentException("Input array for 'find32' cannot be null.");
                }
                using var _1 = ReaderLock.Create(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, out var storageA);
                //if (storageA.S.NumberOfDimensions > 2 && !Equals(C,null)) {
                //    return find3264(storageA.GetRange_ML(full, full, false), n, backwards, C, V); 
                //}
                #region  handle empty A
                if (storageA.S.NumberOfElements == 0) {
                    if (!Equals(C, null)) {
                        lock (C.SynchObj)
                            C.a = empty<int>(0);
                    }
                    if (!Equals(V, null)) {
                        lock (V.SynchObj)
                            V.a = empty<float>(dim0: 0);
                    }
                    return empty<int>(dim0: 0);
                } else if (storageA.S.NumberOfElements > int.MaxValue) {
                    throw new InvalidCastException($"The input array has too many elements for this overload of find32(). Consider using find32_64_ instead!");
                }
                #endregion

                if (n < 0) {
                    n = (int)Math.Abs(n);  // for hycalper...
                } else if (n == 0) {
                    n = (int)storageA.S.NumberOfElements; // assume maximum for now; 
                }
                if (n > storageA.S.NumberOfElements) {
                    n = (int)storageA.S.NumberOfElements;
                }
                Array<int> c = null;
                Array<float> v = null;

                // initialize working / out arrays with assumed maximum
                if (!Equals(C, null)) {
                    c = empty<int>(n);
                }
                if (!Equals(V, null)) {
                    v = empty<float>(n);
                }

                // initialize ret array with assumed maximum
                Array<int> ret = empty<int>(n);

                int* retP = (int*)ret.GetHostPointerForWrite();
                float* aP = (float*)storageA.Handles[0].Pointer; // size.GetSeqIndex contains storageA.S.BaseOffset;
                int* cP = (!Equals(c, null) ? (int*)c.GetHostPointerForWrite() : null);
                float* vP = (!Equals(v, null) ? (float*)v.GetHostPointerForWrite() : null);
                int lastElemDim2 = (int)(storageA.S.NumberOfElements / storageA.S[0]);
                int foundN = 0, strideA0 = (int)storageA.S[0];
                if (backwards == false) {
                    for (int ic = 0; ic < lastElemDim2 && foundN < n; ic++) {
                        for (int ir = 0; ir < strideA0 && foundN < n; ir++) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                } else {
                    for (int ic = lastElemDim2; ic-- > 0 && foundN < n;) {
                        for (int ir = (int)strideA0; ir-- > 0 && foundN < n;) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                }
                //int foundN = outP - (int*)ret.m_handles[0].Pointer;
                if (!Equals(v, null)) {
                    lock (V.SynchObj) {
                        if (foundN == 0) {
                            V.a = empty<float>(dim0: 0);
                        } else {
                            V.a = v[slice(0, foundN)];
                        }
                    }
                }
                if (!Equals(c, null)) {
                    lock (C.SynchObj) {
                        if (foundN == 0) {
                            C.a = empty<int>(dim0: 0);
                        } else {
                            C.a = c[slice(0, foundN)];
                        }
                    }
                }
                return ret[slice(0, foundN)];
            }
        }
       

        /// <summary>
        /// Finds sequential indices of non-zero elements in n-d array, search from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <returns>Sequential indices of the non-zero-valued elements in <paramref name="A"/>.</returns>
        
        internal static unsafe Array<int> find32(BaseArray<double> A) {

            if (Equals(A, null)) {
                throw new ArgumentException("Input array for 'find32' cannot be null.");
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, out var storageA);

            if (storageA.S.NumberOfElements == 0) {
                return empty<int>(dim0: 0);
            } else if (storageA.S.NumberOfElements > int.MaxValue) {
                // this is here for Hycalper reasons only. It is required for the 32 bit overloads only. 
                throw new InvalidCastException($"The input array has too many elements for this overload of find3232(). Consider using find32() instead!");
            }
            var ret = Storage<int>.Create();
            int nrElements = (int)storageA.S.NumberOfElements; // assume maximum for now
            ret.S.SetAll(nrElements);
            DeviceManagement.DeviceManager.GetDevice(0).New<int>((ulong)nrElements, ret.Handles);

            int* outP = (int*)ret.Handles[0].Pointer;
            int i = 0;
            foreach (var b in storageA.AsRetArray().Iterator(StorageOrders.ColumnMajor)) { // A is released after iteration
                if (b != 0) {
                    *outP++ = i;
                }
                i++;
            }
            return ret.RetArray  /**/ [slice(0, outP - (int*)ret.Handles[0].Pointer)];
        }
        /// <summary>
        /// Find sequential indices of non-zero elements in n-d array, searches from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <param name="backwards">[Optional] Search direction backwards. Default: (false) start at the first element and search to the last element of <paramref name="A"/>.</param>
        /// <param name="C">[Optional] If non-null on entry: the function returns the row indices and gives column indices in <paramref name="C"/>. Default: (null) the function returns the sequential indices of elements found only.</param>
        /// <param name="n">[Optional] Maximum number of non-zero elements to find32. Default: (0) find32 all non-zero elements.</param>
        /// <param name="V">[Optional] Return the found non-zero element values in <paramref name="V"/>. Default: (null) don't return the values.</param>
        /// <returns>Sequential, column major ordered indices of the non-zero-valued elements in <paramref name="A"/> or row indices if <paramref name="C"/> is not null.</returns>
        /// <remarks><para>If <paramref name="C"/> or <paramref name="V"/> are not null on entry they are filled with the requested respective 
        /// info as column vectors. If <paramref name="C"/> is not null it contains the column indices of non-zero values. If <paramref name="A"/> 
        /// has more than two dimensions subsequent dimensions are merged into the second dimension and the indices given in <paramref name="C"/> 
        /// are considered sequential indices of <paramref name="A"/> (column major order). </para>
        /// <para>Note, that the array returned by <see cref="find32(BaseArray{double}, int, bool, OutArray{int}, OutArray{double})"/>
        /// corresponds to the indices of the rows of non-zero values if <paramref name="C"/> is not null and to sequential indices of those 
        /// non-zero values if <paramref name="C"/> is null. </para>
        /// <para>Negative values of <paramref name="n"/> are considered as abs(n).</para>
        /// <para>If <paramref name="backwards"/> is true <see cref="find32(BaseArray{double}, int, bool, OutArray{int}, OutArray{double})"/>
        /// starts searching for non-zero values from the last element and proceeds in column major order to the first element or until <paramref name="n"/> 
        /// non-zero elements were found, whatever comes first.</para>
        /// <para>Find() returns and considers indices as 64 bit <see cref="int"/>. Overloads exist for backwards compatibility which expect indices as <see cref="Int32"/>
        /// values. Such functions are decorated with the suffix '32' in their names.</para>
        /// </remarks>
        internal static unsafe Array<int> find32(BaseArray<double> A, int n = 0, bool backwards = false, OutArray<int> C = null, OutArray<double> V = null) {

            using (Scope.Enter()) {
                if (Equals(A, null)) {
                    throw new ArgumentException("Input array for 'find32' cannot be null.");
                }
                using var _1 = ReaderLock.Create(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, out var storageA);
                //if (storageA.S.NumberOfDimensions > 2 && !Equals(C,null)) {
                //    return find3264(storageA.GetRange_ML(full, full, false), n, backwards, C, V); 
                //}
                #region  handle empty A
                if (storageA.S.NumberOfElements == 0) {
                    if (!Equals(C, null)) {
                        lock (C.SynchObj)
                            C.a = empty<int>(0);
                    }
                    if (!Equals(V, null)) {
                        lock (V.SynchObj)
                            V.a = empty<double>(dim0: 0);
                    }
                    return empty<int>(dim0: 0);
                } else if (storageA.S.NumberOfElements > int.MaxValue) {
                    throw new InvalidCastException($"The input array has too many elements for this overload of find32(). Consider using find32_64_ instead!");
                }
                #endregion

                if (n < 0) {
                    n = (int)Math.Abs(n);  // for hycalper...
                } else if (n == 0) {
                    n = (int)storageA.S.NumberOfElements; // assume maximum for now; 
                }
                if (n > storageA.S.NumberOfElements) {
                    n = (int)storageA.S.NumberOfElements;
                }
                Array<int> c = null;
                Array<double> v = null;

                // initialize working / out arrays with assumed maximum
                if (!Equals(C, null)) {
                    c = empty<int>(n);
                }
                if (!Equals(V, null)) {
                    v = empty<double>(n);
                }

                // initialize ret array with assumed maximum
                Array<int> ret = empty<int>(n);

                int* retP = (int*)ret.GetHostPointerForWrite();
                double* aP = (double*)storageA.Handles[0].Pointer; // size.GetSeqIndex contains storageA.S.BaseOffset;
                int* cP = (!Equals(c, null) ? (int*)c.GetHostPointerForWrite() : null);
                double* vP = (!Equals(v, null) ? (double*)v.GetHostPointerForWrite() : null);
                int lastElemDim2 = (int)(storageA.S.NumberOfElements / storageA.S[0]);
                int foundN = 0, strideA0 = (int)storageA.S[0];
                if (backwards == false) {
                    for (int ic = 0; ic < lastElemDim2 && foundN < n; ic++) {
                        for (int ir = 0; ir < strideA0 && foundN < n; ir++) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                } else {
                    for (int ic = lastElemDim2; ic-- > 0 && foundN < n;) {
                        for (int ir = (int)strideA0; ir-- > 0 && foundN < n;) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                }
                //int foundN = outP - (int*)ret.m_handles[0].Pointer;
                if (!Equals(v, null)) {
                    lock (V.SynchObj) {
                        if (foundN == 0) {
                            V.a = empty<double>(dim0: 0);
                        } else {
                            V.a = v[slice(0, foundN)];
                        }
                    }
                }
                if (!Equals(c, null)) {
                    lock (C.SynchObj) {
                        if (foundN == 0) {
                            C.a = empty<int>(dim0: 0);
                        } else {
                            C.a = c[slice(0, foundN)];
                        }
                    }
                }
                return ret[slice(0, foundN)];
            }
        }
       

        /// <summary>
        /// Finds sequential indices of non-zero elements in n-d array, search from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <returns>Sequential indices of the non-zero-valued elements in <paramref name="A"/>.</returns>
        
        internal static unsafe Array<long> find(BaseArray<ulong> A) {

            if (Equals(A, null)) {
                throw new ArgumentException("Input array for 'find' cannot be null.");
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>, out var storageA);

            if (storageA.S.NumberOfElements == 0) {
                return empty<long>(dim0: 0);
            } else if (storageA.S.NumberOfElements > long.MaxValue) {
                // this is here for Hycalper reasons only. It is required for the 32 bit overloads only. 
                throw new InvalidCastException($"The input array has too many elements for this overload of find32(). Consider using find() instead!");
            }
            var ret = Storage<long>.Create();
            long nrElements = (long)storageA.S.NumberOfElements; // assume maximum for now
            ret.S.SetAll(nrElements);
            DeviceManagement.DeviceManager.GetDevice(0).New<long>((ulong)nrElements, ret.Handles);

            long* outP = (long*)ret.Handles[0].Pointer;
            long i = 0;
            foreach (var b in storageA.AsRetArray().Iterator(StorageOrders.ColumnMajor)) { // A is released after iteration
                if (b != 0) {
                    *outP++ = i;
                }
                i++;
            }
            return ret.RetArray  /**/ [slice(0, outP - (long*)ret.Handles[0].Pointer)];
        }
        /// <summary>
        /// Find sequential indices of non-zero elements in n-d array, searches from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <param name="backwards">[Optional] Search direction backwards. Default: (false) start at the first element and search to the last element of <paramref name="A"/>.</param>
        /// <param name="C">[Optional] If non-null on entry: the function returns the row indices and gives column indices in <paramref name="C"/>. Default: (null) the function returns the sequential indices of elements found only.</param>
        /// <param name="n">[Optional] Maximum number of non-zero elements to find. Default: (0) find all non-zero elements.</param>
        /// <param name="V">[Optional] Return the found non-zero element values in <paramref name="V"/>. Default: (null) don't return the values.</param>
        /// <returns>Sequential, column major ordered indices of the non-zero-valued elements in <paramref name="A"/> or row indices if <paramref name="C"/> is not null.</returns>
        /// <remarks><para>If <paramref name="C"/> or <paramref name="V"/> are not null on entry they are filled with the requested respective 
        /// info as column vectors. If <paramref name="C"/> is not null it contains the column indices of non-zero values. If <paramref name="A"/> 
        /// has more than two dimensions subsequent dimensions are merged into the second dimension and the indices given in <paramref name="C"/> 
        /// are considered sequential indices of <paramref name="A"/> (column major order). </para>
        /// <para>Note, that the array returned by <see cref="find(BaseArray{ulong}, long, bool, OutArray{long}, OutArray{ulong})"/>
        /// corresponds to the indices of the rows of non-zero values if <paramref name="C"/> is not null and to sequential indices of those 
        /// non-zero values if <paramref name="C"/> is null. </para>
        /// <para>Negative values of <paramref name="n"/> are considered as abs(n).</para>
        /// <para>If <paramref name="backwards"/> is true <see cref="find(BaseArray{ulong}, long, bool, OutArray{long}, OutArray{ulong})"/>
        /// starts searching for non-zero values from the last element and proceeds in column major order to the first element or until <paramref name="n"/> 
        /// non-zero elements were found, whatever comes first.</para>
        /// <para>Find() returns and considers indices as 64 bit <see cref="long"/>. Overloads exist for backwards compatibility which expect indices as <see cref="Int32"/>
        /// values. Such functions are decorated with the suffix '32' in their names.</para>
        /// </remarks>
        internal static unsafe Array<long> find(BaseArray<ulong> A, long n = 0, bool backwards = false, OutArray<long> C = null, OutArray<ulong> V = null) {

            using (Scope.Enter()) {
                if (Equals(A, null)) {
                    throw new ArgumentException("Input array for 'find' cannot be null.");
                }
                using var _1 = ReaderLock.Create(A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>, out var storageA);
                //if (storageA.S.NumberOfDimensions > 2 && !Equals(C,null)) {
                //    return find64(storageA.GetRange_ML(full, full, false), n, backwards, C, V); 
                //}
                #region  handle empty A
                if (storageA.S.NumberOfElements == 0) {
                    if (!Equals(C, null)) {
                        lock (C.SynchObj)
                            C.a = empty<long>(0);
                    }
                    if (!Equals(V, null)) {
                        lock (V.SynchObj)
                            V.a = empty<ulong>(dim0: 0);
                    }
                    return empty<long>(dim0: 0);
                } else if (storageA.S.NumberOfElements > long.MaxValue) {
                    throw new InvalidCastException($"The input array has too many elements for this overload of find(). Consider using find_64_ instead!");
                }
                #endregion

                if (n < 0) {
                    n = (long)Math.Abs(n);  // for hycalper...
                } else if (n == 0) {
                    n = (long)storageA.S.NumberOfElements; // assume maximum for now; 
                }
                if (n > storageA.S.NumberOfElements) {
                    n = (long)storageA.S.NumberOfElements;
                }
                Array<long> c = null;
                Array<ulong> v = null;

                // initialize working / out arrays with assumed maximum
                if (!Equals(C, null)) {
                    c = empty<long>(n);
                }
                if (!Equals(V, null)) {
                    v = empty<ulong>(n);
                }

                // initialize ret array with assumed maximum
                Array<long> ret = empty<long>(n);

                long* retP = (long*)ret.GetHostPointerForWrite();
                ulong* aP = (ulong*)storageA.Handles[0].Pointer; // size.GetSeqIndex contains storageA.S.BaseOffset;
                long* cP = (!Equals(c, null) ? (long*)c.GetHostPointerForWrite() : null);
                ulong* vP = (!Equals(v, null) ? (ulong*)v.GetHostPointerForWrite() : null);
                long lastElemDim2 = (long)(storageA.S.NumberOfElements / storageA.S[0]);
                long foundN = 0, strideA0 = (long)storageA.S[0];
                if (backwards == false) {
                    for (long ic = 0; ic < lastElemDim2 && foundN < n; ic++) {
                        for (long ir = 0; ir < strideA0 && foundN < n; ir++) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                } else {
                    for (long ic = lastElemDim2; ic-- > 0 && foundN < n;) {
                        for (long ir = (long)strideA0; ir-- > 0 && foundN < n;) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                }
                //long foundN = outP - (long*)ret.m_handles[0].Pointer;
                if (!Equals(v, null)) {
                    lock (V.SynchObj) {
                        if (foundN == 0) {
                            V.a = empty<ulong>(dim0: 0);
                        } else {
                            V.a = v[slice(0, foundN)];
                        }
                    }
                }
                if (!Equals(c, null)) {
                    lock (C.SynchObj) {
                        if (foundN == 0) {
                            C.a = empty<long>(dim0: 0);
                        } else {
                            C.a = c[slice(0, foundN)];
                        }
                    }
                }
                return ret[slice(0, foundN)];
            }
        }
       

        /// <summary>
        /// Finds sequential indices of non-zero elements in n-d array, search from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <returns>Sequential indices of the non-zero-valued elements in <paramref name="A"/>.</returns>
        
        internal static unsafe Array<long> find(BaseArray<long> A) {

            if (Equals(A, null)) {
                throw new ArgumentException("Input array for 'find' cannot be null.");
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, out var storageA);

            if (storageA.S.NumberOfElements == 0) {
                return empty<long>(dim0: 0);
            } else if (storageA.S.NumberOfElements > long.MaxValue) {
                // this is here for Hycalper reasons only. It is required for the 32 bit overloads only. 
                throw new InvalidCastException($"The input array has too many elements for this overload of find32(). Consider using find() instead!");
            }
            var ret = Storage<long>.Create();
            long nrElements = (long)storageA.S.NumberOfElements; // assume maximum for now
            ret.S.SetAll(nrElements);
            DeviceManagement.DeviceManager.GetDevice(0).New<long>((ulong)nrElements, ret.Handles);

            long* outP = (long*)ret.Handles[0].Pointer;
            long i = 0;
            foreach (var b in storageA.AsRetArray().Iterator(StorageOrders.ColumnMajor)) { // A is released after iteration
                if (b != 0) {
                    *outP++ = i;
                }
                i++;
            }
            return ret.RetArray  /**/ [slice(0, outP - (long*)ret.Handles[0].Pointer)];
        }
        /// <summary>
        /// Find sequential indices of non-zero elements in n-d array, searches from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <param name="backwards">[Optional] Search direction backwards. Default: (false) start at the first element and search to the last element of <paramref name="A"/>.</param>
        /// <param name="C">[Optional] If non-null on entry: the function returns the row indices and gives column indices in <paramref name="C"/>. Default: (null) the function returns the sequential indices of elements found only.</param>
        /// <param name="n">[Optional] Maximum number of non-zero elements to find. Default: (0) find all non-zero elements.</param>
        /// <param name="V">[Optional] Return the found non-zero element values in <paramref name="V"/>. Default: (null) don't return the values.</param>
        /// <returns>Sequential, column major ordered indices of the non-zero-valued elements in <paramref name="A"/> or row indices if <paramref name="C"/> is not null.</returns>
        /// <remarks><para>If <paramref name="C"/> or <paramref name="V"/> are not null on entry they are filled with the requested respective 
        /// info as column vectors. If <paramref name="C"/> is not null it contains the column indices of non-zero values. If <paramref name="A"/> 
        /// has more than two dimensions subsequent dimensions are merged into the second dimension and the indices given in <paramref name="C"/> 
        /// are considered sequential indices of <paramref name="A"/> (column major order). </para>
        /// <para>Note, that the array returned by <see cref="find(BaseArray{long}, long, bool, OutArray{long}, OutArray{long})"/>
        /// corresponds to the indices of the rows of non-zero values if <paramref name="C"/> is not null and to sequential indices of those 
        /// non-zero values if <paramref name="C"/> is null. </para>
        /// <para>Negative values of <paramref name="n"/> are considered as abs(n).</para>
        /// <para>If <paramref name="backwards"/> is true <see cref="find(BaseArray{long}, long, bool, OutArray{long}, OutArray{long})"/>
        /// starts searching for non-zero values from the last element and proceeds in column major order to the first element or until <paramref name="n"/> 
        /// non-zero elements were found, whatever comes first.</para>
        /// <para>Find() returns and considers indices as 64 bit <see cref="long"/>. Overloads exist for backwards compatibility which expect indices as <see cref="Int32"/>
        /// values. Such functions are decorated with the suffix '32' in their names.</para>
        /// </remarks>
        internal static unsafe Array<long> find(BaseArray<long> A, long n = 0, bool backwards = false, OutArray<long> C = null, OutArray<long> V = null) {

            using (Scope.Enter()) {
                if (Equals(A, null)) {
                    throw new ArgumentException("Input array for 'find' cannot be null.");
                }
                using var _1 = ReaderLock.Create(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, out var storageA);
                //if (storageA.S.NumberOfDimensions > 2 && !Equals(C,null)) {
                //    return find64(storageA.GetRange_ML(full, full, false), n, backwards, C, V); 
                //}
                #region  handle empty A
                if (storageA.S.NumberOfElements == 0) {
                    if (!Equals(C, null)) {
                        lock (C.SynchObj)
                            C.a = empty<long>(0);
                    }
                    if (!Equals(V, null)) {
                        lock (V.SynchObj)
                            V.a = empty<long>(dim0: 0);
                    }
                    return empty<long>(dim0: 0);
                } else if (storageA.S.NumberOfElements > long.MaxValue) {
                    throw new InvalidCastException($"The input array has too many elements for this overload of find(). Consider using find_64_ instead!");
                }
                #endregion

                if (n < 0) {
                    n = (long)Math.Abs(n);  // for hycalper...
                } else if (n == 0) {
                    n = (long)storageA.S.NumberOfElements; // assume maximum for now; 
                }
                if (n > storageA.S.NumberOfElements) {
                    n = (long)storageA.S.NumberOfElements;
                }
                Array<long> c = null;
                Array<long> v = null;

                // initialize working / out arrays with assumed maximum
                if (!Equals(C, null)) {
                    c = empty<long>(n);
                }
                if (!Equals(V, null)) {
                    v = empty<long>(n);
                }

                // initialize ret array with assumed maximum
                Array<long> ret = empty<long>(n);

                long* retP = (long*)ret.GetHostPointerForWrite();
                long* aP = (long*)storageA.Handles[0].Pointer; // size.GetSeqIndex contains storageA.S.BaseOffset;
                long* cP = (!Equals(c, null) ? (long*)c.GetHostPointerForWrite() : null);
                long* vP = (!Equals(v, null) ? (long*)v.GetHostPointerForWrite() : null);
                long lastElemDim2 = (long)(storageA.S.NumberOfElements / storageA.S[0]);
                long foundN = 0, strideA0 = (long)storageA.S[0];
                if (backwards == false) {
                    for (long ic = 0; ic < lastElemDim2 && foundN < n; ic++) {
                        for (long ir = 0; ir < strideA0 && foundN < n; ir++) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                } else {
                    for (long ic = lastElemDim2; ic-- > 0 && foundN < n;) {
                        for (long ir = (long)strideA0; ir-- > 0 && foundN < n;) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                }
                //long foundN = outP - (long*)ret.m_handles[0].Pointer;
                if (!Equals(v, null)) {
                    lock (V.SynchObj) {
                        if (foundN == 0) {
                            V.a = empty<long>(dim0: 0);
                        } else {
                            V.a = v[slice(0, foundN)];
                        }
                    }
                }
                if (!Equals(c, null)) {
                    lock (C.SynchObj) {
                        if (foundN == 0) {
                            C.a = empty<long>(dim0: 0);
                        } else {
                            C.a = c[slice(0, foundN)];
                        }
                    }
                }
                return ret[slice(0, foundN)];
            }
        }
       

        /// <summary>
        /// Finds sequential indices of non-zero elements in n-d array, search from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <returns>Sequential indices of the non-zero-valued elements in <paramref name="A"/>.</returns>
        
        internal static unsafe Array<long> find(BaseArray<uint> A) {

            if (Equals(A, null)) {
                throw new ArgumentException("Input array for 'find' cannot be null.");
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>, out var storageA);

            if (storageA.S.NumberOfElements == 0) {
                return empty<long>(dim0: 0);
            } else if (storageA.S.NumberOfElements > long.MaxValue) {
                // this is here for Hycalper reasons only. It is required for the 32 bit overloads only. 
                throw new InvalidCastException($"The input array has too many elements for this overload of find32(). Consider using find() instead!");
            }
            var ret = Storage<long>.Create();
            long nrElements = (long)storageA.S.NumberOfElements; // assume maximum for now
            ret.S.SetAll(nrElements);
            DeviceManagement.DeviceManager.GetDevice(0).New<long>((ulong)nrElements, ret.Handles);

            long* outP = (long*)ret.Handles[0].Pointer;
            long i = 0;
            foreach (var b in storageA.AsRetArray().Iterator(StorageOrders.ColumnMajor)) { // A is released after iteration
                if (b != 0) {
                    *outP++ = i;
                }
                i++;
            }
            return ret.RetArray  /**/ [slice(0, outP - (long*)ret.Handles[0].Pointer)];
        }
        /// <summary>
        /// Find sequential indices of non-zero elements in n-d array, searches from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <param name="backwards">[Optional] Search direction backwards. Default: (false) start at the first element and search to the last element of <paramref name="A"/>.</param>
        /// <param name="C">[Optional] If non-null on entry: the function returns the row indices and gives column indices in <paramref name="C"/>. Default: (null) the function returns the sequential indices of elements found only.</param>
        /// <param name="n">[Optional] Maximum number of non-zero elements to find. Default: (0) find all non-zero elements.</param>
        /// <param name="V">[Optional] Return the found non-zero element values in <paramref name="V"/>. Default: (null) don't return the values.</param>
        /// <returns>Sequential, column major ordered indices of the non-zero-valued elements in <paramref name="A"/> or row indices if <paramref name="C"/> is not null.</returns>
        /// <remarks><para>If <paramref name="C"/> or <paramref name="V"/> are not null on entry they are filled with the requested respective 
        /// info as column vectors. If <paramref name="C"/> is not null it contains the column indices of non-zero values. If <paramref name="A"/> 
        /// has more than two dimensions subsequent dimensions are merged into the second dimension and the indices given in <paramref name="C"/> 
        /// are considered sequential indices of <paramref name="A"/> (column major order). </para>
        /// <para>Note, that the array returned by <see cref="find(BaseArray{uint}, long, bool, OutArray{long}, OutArray{uint})"/>
        /// corresponds to the indices of the rows of non-zero values if <paramref name="C"/> is not null and to sequential indices of those 
        /// non-zero values if <paramref name="C"/> is null. </para>
        /// <para>Negative values of <paramref name="n"/> are considered as abs(n).</para>
        /// <para>If <paramref name="backwards"/> is true <see cref="find(BaseArray{uint}, long, bool, OutArray{long}, OutArray{uint})"/>
        /// starts searching for non-zero values from the last element and proceeds in column major order to the first element or until <paramref name="n"/> 
        /// non-zero elements were found, whatever comes first.</para>
        /// <para>Find() returns and considers indices as 64 bit <see cref="long"/>. Overloads exist for backwards compatibility which expect indices as <see cref="Int32"/>
        /// values. Such functions are decorated with the suffix '32' in their names.</para>
        /// </remarks>
        internal static unsafe Array<long> find(BaseArray<uint> A, long n = 0, bool backwards = false, OutArray<long> C = null, OutArray<uint> V = null) {

            using (Scope.Enter()) {
                if (Equals(A, null)) {
                    throw new ArgumentException("Input array for 'find' cannot be null.");
                }
                using var _1 = ReaderLock.Create(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>, out var storageA);
                //if (storageA.S.NumberOfDimensions > 2 && !Equals(C,null)) {
                //    return find64(storageA.GetRange_ML(full, full, false), n, backwards, C, V); 
                //}
                #region  handle empty A
                if (storageA.S.NumberOfElements == 0) {
                    if (!Equals(C, null)) {
                        lock (C.SynchObj)
                            C.a = empty<long>(0);
                    }
                    if (!Equals(V, null)) {
                        lock (V.SynchObj)
                            V.a = empty<uint>(dim0: 0);
                    }
                    return empty<long>(dim0: 0);
                } else if (storageA.S.NumberOfElements > long.MaxValue) {
                    throw new InvalidCastException($"The input array has too many elements for this overload of find(). Consider using find_64_ instead!");
                }
                #endregion

                if (n < 0) {
                    n = (long)Math.Abs(n);  // for hycalper...
                } else if (n == 0) {
                    n = (long)storageA.S.NumberOfElements; // assume maximum for now; 
                }
                if (n > storageA.S.NumberOfElements) {
                    n = (long)storageA.S.NumberOfElements;
                }
                Array<long> c = null;
                Array<uint> v = null;

                // initialize working / out arrays with assumed maximum
                if (!Equals(C, null)) {
                    c = empty<long>(n);
                }
                if (!Equals(V, null)) {
                    v = empty<uint>(n);
                }

                // initialize ret array with assumed maximum
                Array<long> ret = empty<long>(n);

                long* retP = (long*)ret.GetHostPointerForWrite();
                uint* aP = (uint*)storageA.Handles[0].Pointer; // size.GetSeqIndex contains storageA.S.BaseOffset;
                long* cP = (!Equals(c, null) ? (long*)c.GetHostPointerForWrite() : null);
                uint* vP = (!Equals(v, null) ? (uint*)v.GetHostPointerForWrite() : null);
                long lastElemDim2 = (long)(storageA.S.NumberOfElements / storageA.S[0]);
                long foundN = 0, strideA0 = (long)storageA.S[0];
                if (backwards == false) {
                    for (long ic = 0; ic < lastElemDim2 && foundN < n; ic++) {
                        for (long ir = 0; ir < strideA0 && foundN < n; ir++) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                } else {
                    for (long ic = lastElemDim2; ic-- > 0 && foundN < n;) {
                        for (long ir = (long)strideA0; ir-- > 0 && foundN < n;) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                }
                //long foundN = outP - (long*)ret.m_handles[0].Pointer;
                if (!Equals(v, null)) {
                    lock (V.SynchObj) {
                        if (foundN == 0) {
                            V.a = empty<uint>(dim0: 0);
                        } else {
                            V.a = v[slice(0, foundN)];
                        }
                    }
                }
                if (!Equals(c, null)) {
                    lock (C.SynchObj) {
                        if (foundN == 0) {
                            C.a = empty<long>(dim0: 0);
                        } else {
                            C.a = c[slice(0, foundN)];
                        }
                    }
                }
                return ret[slice(0, foundN)];
            }
        }
       

        /// <summary>
        /// Finds sequential indices of non-zero elements in n-d array, search from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <returns>Sequential indices of the non-zero-valued elements in <paramref name="A"/>.</returns>
        
        internal static unsafe Array<long> find(BaseArray<int> A) {

            if (Equals(A, null)) {
                throw new ArgumentException("Input array for 'find' cannot be null.");
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>, out var storageA);

            if (storageA.S.NumberOfElements == 0) {
                return empty<long>(dim0: 0);
            } else if (storageA.S.NumberOfElements > long.MaxValue) {
                // this is here for Hycalper reasons only. It is required for the 32 bit overloads only. 
                throw new InvalidCastException($"The input array has too many elements for this overload of find32(). Consider using find() instead!");
            }
            var ret = Storage<long>.Create();
            long nrElements = (long)storageA.S.NumberOfElements; // assume maximum for now
            ret.S.SetAll(nrElements);
            DeviceManagement.DeviceManager.GetDevice(0).New<long>((ulong)nrElements, ret.Handles);

            long* outP = (long*)ret.Handles[0].Pointer;
            long i = 0;
            foreach (var b in storageA.AsRetArray().Iterator(StorageOrders.ColumnMajor)) { // A is released after iteration
                if (b != 0) {
                    *outP++ = i;
                }
                i++;
            }
            return ret.RetArray  /**/ [slice(0, outP - (long*)ret.Handles[0].Pointer)];
        }
        /// <summary>
        /// Find sequential indices of non-zero elements in n-d array, searches from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <param name="backwards">[Optional] Search direction backwards. Default: (false) start at the first element and search to the last element of <paramref name="A"/>.</param>
        /// <param name="C">[Optional] If non-null on entry: the function returns the row indices and gives column indices in <paramref name="C"/>. Default: (null) the function returns the sequential indices of elements found only.</param>
        /// <param name="n">[Optional] Maximum number of non-zero elements to find. Default: (0) find all non-zero elements.</param>
        /// <param name="V">[Optional] Return the found non-zero element values in <paramref name="V"/>. Default: (null) don't return the values.</param>
        /// <returns>Sequential, column major ordered indices of the non-zero-valued elements in <paramref name="A"/> or row indices if <paramref name="C"/> is not null.</returns>
        /// <remarks><para>If <paramref name="C"/> or <paramref name="V"/> are not null on entry they are filled with the requested respective 
        /// info as column vectors. If <paramref name="C"/> is not null it contains the column indices of non-zero values. If <paramref name="A"/> 
        /// has more than two dimensions subsequent dimensions are merged into the second dimension and the indices given in <paramref name="C"/> 
        /// are considered sequential indices of <paramref name="A"/> (column major order). </para>
        /// <para>Note, that the array returned by <see cref="find(BaseArray{int}, long, bool, OutArray{long}, OutArray{int})"/>
        /// corresponds to the indices of the rows of non-zero values if <paramref name="C"/> is not null and to sequential indices of those 
        /// non-zero values if <paramref name="C"/> is null. </para>
        /// <para>Negative values of <paramref name="n"/> are considered as abs(n).</para>
        /// <para>If <paramref name="backwards"/> is true <see cref="find(BaseArray{int}, long, bool, OutArray{long}, OutArray{int})"/>
        /// starts searching for non-zero values from the last element and proceeds in column major order to the first element or until <paramref name="n"/> 
        /// non-zero elements were found, whatever comes first.</para>
        /// <para>Find() returns and considers indices as 64 bit <see cref="long"/>. Overloads exist for backwards compatibility which expect indices as <see cref="Int32"/>
        /// values. Such functions are decorated with the suffix '32' in their names.</para>
        /// </remarks>
        internal static unsafe Array<long> find(BaseArray<int> A, long n = 0, bool backwards = false, OutArray<long> C = null, OutArray<int> V = null) {

            using (Scope.Enter()) {
                if (Equals(A, null)) {
                    throw new ArgumentException("Input array for 'find' cannot be null.");
                }
                using var _1 = ReaderLock.Create(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>, out var storageA);
                //if (storageA.S.NumberOfDimensions > 2 && !Equals(C,null)) {
                //    return find64(storageA.GetRange_ML(full, full, false), n, backwards, C, V); 
                //}
                #region  handle empty A
                if (storageA.S.NumberOfElements == 0) {
                    if (!Equals(C, null)) {
                        lock (C.SynchObj)
                            C.a = empty<long>(0);
                    }
                    if (!Equals(V, null)) {
                        lock (V.SynchObj)
                            V.a = empty<int>(dim0: 0);
                    }
                    return empty<long>(dim0: 0);
                } else if (storageA.S.NumberOfElements > long.MaxValue) {
                    throw new InvalidCastException($"The input array has too many elements for this overload of find(). Consider using find_64_ instead!");
                }
                #endregion

                if (n < 0) {
                    n = (long)Math.Abs(n);  // for hycalper...
                } else if (n == 0) {
                    n = (long)storageA.S.NumberOfElements; // assume maximum for now; 
                }
                if (n > storageA.S.NumberOfElements) {
                    n = (long)storageA.S.NumberOfElements;
                }
                Array<long> c = null;
                Array<int> v = null;

                // initialize working / out arrays with assumed maximum
                if (!Equals(C, null)) {
                    c = empty<long>(n);
                }
                if (!Equals(V, null)) {
                    v = empty<int>(n);
                }

                // initialize ret array with assumed maximum
                Array<long> ret = empty<long>(n);

                long* retP = (long*)ret.GetHostPointerForWrite();
                int* aP = (int*)storageA.Handles[0].Pointer; // size.GetSeqIndex contains storageA.S.BaseOffset;
                long* cP = (!Equals(c, null) ? (long*)c.GetHostPointerForWrite() : null);
                int* vP = (!Equals(v, null) ? (int*)v.GetHostPointerForWrite() : null);
                long lastElemDim2 = (long)(storageA.S.NumberOfElements / storageA.S[0]);
                long foundN = 0, strideA0 = (long)storageA.S[0];
                if (backwards == false) {
                    for (long ic = 0; ic < lastElemDim2 && foundN < n; ic++) {
                        for (long ir = 0; ir < strideA0 && foundN < n; ir++) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                } else {
                    for (long ic = lastElemDim2; ic-- > 0 && foundN < n;) {
                        for (long ir = (long)strideA0; ir-- > 0 && foundN < n;) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                }
                //long foundN = outP - (long*)ret.m_handles[0].Pointer;
                if (!Equals(v, null)) {
                    lock (V.SynchObj) {
                        if (foundN == 0) {
                            V.a = empty<int>(dim0: 0);
                        } else {
                            V.a = v[slice(0, foundN)];
                        }
                    }
                }
                if (!Equals(c, null)) {
                    lock (C.SynchObj) {
                        if (foundN == 0) {
                            C.a = empty<long>(dim0: 0);
                        } else {
                            C.a = c[slice(0, foundN)];
                        }
                    }
                }
                return ret[slice(0, foundN)];
            }
        }
       

        /// <summary>
        /// Finds sequential indices of non-zero elements in n-d array, search from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <returns>Sequential indices of the non-zero-valued elements in <paramref name="A"/>.</returns>
        
        internal static unsafe Array<long> find(BaseArray<ushort> A) {

            if (Equals(A, null)) {
                throw new ArgumentException("Input array for 'find' cannot be null.");
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>, out var storageA);

            if (storageA.S.NumberOfElements == 0) {
                return empty<long>(dim0: 0);
            } else if (storageA.S.NumberOfElements > long.MaxValue) {
                // this is here for Hycalper reasons only. It is required for the 32 bit overloads only. 
                throw new InvalidCastException($"The input array has too many elements for this overload of find32(). Consider using find() instead!");
            }
            var ret = Storage<long>.Create();
            long nrElements = (long)storageA.S.NumberOfElements; // assume maximum for now
            ret.S.SetAll(nrElements);
            DeviceManagement.DeviceManager.GetDevice(0).New<long>((ulong)nrElements, ret.Handles);

            long* outP = (long*)ret.Handles[0].Pointer;
            long i = 0;
            foreach (var b in storageA.AsRetArray().Iterator(StorageOrders.ColumnMajor)) { // A is released after iteration
                if (b != 0) {
                    *outP++ = i;
                }
                i++;
            }
            return ret.RetArray  /**/ [slice(0, outP - (long*)ret.Handles[0].Pointer)];
        }
        /// <summary>
        /// Find sequential indices of non-zero elements in n-d array, searches from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <param name="backwards">[Optional] Search direction backwards. Default: (false) start at the first element and search to the last element of <paramref name="A"/>.</param>
        /// <param name="C">[Optional] If non-null on entry: the function returns the row indices and gives column indices in <paramref name="C"/>. Default: (null) the function returns the sequential indices of elements found only.</param>
        /// <param name="n">[Optional] Maximum number of non-zero elements to find. Default: (0) find all non-zero elements.</param>
        /// <param name="V">[Optional] Return the found non-zero element values in <paramref name="V"/>. Default: (null) don't return the values.</param>
        /// <returns>Sequential, column major ordered indices of the non-zero-valued elements in <paramref name="A"/> or row indices if <paramref name="C"/> is not null.</returns>
        /// <remarks><para>If <paramref name="C"/> or <paramref name="V"/> are not null on entry they are filled with the requested respective 
        /// info as column vectors. If <paramref name="C"/> is not null it contains the column indices of non-zero values. If <paramref name="A"/> 
        /// has more than two dimensions subsequent dimensions are merged into the second dimension and the indices given in <paramref name="C"/> 
        /// are considered sequential indices of <paramref name="A"/> (column major order). </para>
        /// <para>Note, that the array returned by <see cref="find(BaseArray{ushort}, long, bool, OutArray{long}, OutArray{ushort})"/>
        /// corresponds to the indices of the rows of non-zero values if <paramref name="C"/> is not null and to sequential indices of those 
        /// non-zero values if <paramref name="C"/> is null. </para>
        /// <para>Negative values of <paramref name="n"/> are considered as abs(n).</para>
        /// <para>If <paramref name="backwards"/> is true <see cref="find(BaseArray{ushort}, long, bool, OutArray{long}, OutArray{ushort})"/>
        /// starts searching for non-zero values from the last element and proceeds in column major order to the first element or until <paramref name="n"/> 
        /// non-zero elements were found, whatever comes first.</para>
        /// <para>Find() returns and considers indices as 64 bit <see cref="long"/>. Overloads exist for backwards compatibility which expect indices as <see cref="Int32"/>
        /// values. Such functions are decorated with the suffix '32' in their names.</para>
        /// </remarks>
        internal static unsafe Array<long> find(BaseArray<ushort> A, long n = 0, bool backwards = false, OutArray<long> C = null, OutArray<ushort> V = null) {

            using (Scope.Enter()) {
                if (Equals(A, null)) {
                    throw new ArgumentException("Input array for 'find' cannot be null.");
                }
                using var _1 = ReaderLock.Create(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>, out var storageA);
                //if (storageA.S.NumberOfDimensions > 2 && !Equals(C,null)) {
                //    return find64(storageA.GetRange_ML(full, full, false), n, backwards, C, V); 
                //}
                #region  handle empty A
                if (storageA.S.NumberOfElements == 0) {
                    if (!Equals(C, null)) {
                        lock (C.SynchObj)
                            C.a = empty<long>(0);
                    }
                    if (!Equals(V, null)) {
                        lock (V.SynchObj)
                            V.a = empty<ushort>(dim0: 0);
                    }
                    return empty<long>(dim0: 0);
                } else if (storageA.S.NumberOfElements > long.MaxValue) {
                    throw new InvalidCastException($"The input array has too many elements for this overload of find(). Consider using find_64_ instead!");
                }
                #endregion

                if (n < 0) {
                    n = (long)Math.Abs(n);  // for hycalper...
                } else if (n == 0) {
                    n = (long)storageA.S.NumberOfElements; // assume maximum for now; 
                }
                if (n > storageA.S.NumberOfElements) {
                    n = (long)storageA.S.NumberOfElements;
                }
                Array<long> c = null;
                Array<ushort> v = null;

                // initialize working / out arrays with assumed maximum
                if (!Equals(C, null)) {
                    c = empty<long>(n);
                }
                if (!Equals(V, null)) {
                    v = empty<ushort>(n);
                }

                // initialize ret array with assumed maximum
                Array<long> ret = empty<long>(n);

                long* retP = (long*)ret.GetHostPointerForWrite();
                ushort* aP = (ushort*)storageA.Handles[0].Pointer; // size.GetSeqIndex contains storageA.S.BaseOffset;
                long* cP = (!Equals(c, null) ? (long*)c.GetHostPointerForWrite() : null);
                ushort* vP = (!Equals(v, null) ? (ushort*)v.GetHostPointerForWrite() : null);
                long lastElemDim2 = (long)(storageA.S.NumberOfElements / storageA.S[0]);
                long foundN = 0, strideA0 = (long)storageA.S[0];
                if (backwards == false) {
                    for (long ic = 0; ic < lastElemDim2 && foundN < n; ic++) {
                        for (long ir = 0; ir < strideA0 && foundN < n; ir++) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                } else {
                    for (long ic = lastElemDim2; ic-- > 0 && foundN < n;) {
                        for (long ir = (long)strideA0; ir-- > 0 && foundN < n;) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                }
                //long foundN = outP - (long*)ret.m_handles[0].Pointer;
                if (!Equals(v, null)) {
                    lock (V.SynchObj) {
                        if (foundN == 0) {
                            V.a = empty<ushort>(dim0: 0);
                        } else {
                            V.a = v[slice(0, foundN)];
                        }
                    }
                }
                if (!Equals(c, null)) {
                    lock (C.SynchObj) {
                        if (foundN == 0) {
                            C.a = empty<long>(dim0: 0);
                        } else {
                            C.a = c[slice(0, foundN)];
                        }
                    }
                }
                return ret[slice(0, foundN)];
            }
        }
       

        /// <summary>
        /// Finds sequential indices of non-zero elements in n-d array, search from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <returns>Sequential indices of the non-zero-valued elements in <paramref name="A"/>.</returns>
        
        internal static unsafe Array<long> find(BaseArray<short> A) {

            if (Equals(A, null)) {
                throw new ArgumentException("Input array for 'find' cannot be null.");
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>, out var storageA);

            if (storageA.S.NumberOfElements == 0) {
                return empty<long>(dim0: 0);
            } else if (storageA.S.NumberOfElements > long.MaxValue) {
                // this is here for Hycalper reasons only. It is required for the 32 bit overloads only. 
                throw new InvalidCastException($"The input array has too many elements for this overload of find32(). Consider using find() instead!");
            }
            var ret = Storage<long>.Create();
            long nrElements = (long)storageA.S.NumberOfElements; // assume maximum for now
            ret.S.SetAll(nrElements);
            DeviceManagement.DeviceManager.GetDevice(0).New<long>((ulong)nrElements, ret.Handles);

            long* outP = (long*)ret.Handles[0].Pointer;
            long i = 0;
            foreach (var b in storageA.AsRetArray().Iterator(StorageOrders.ColumnMajor)) { // A is released after iteration
                if (b != 0) {
                    *outP++ = i;
                }
                i++;
            }
            return ret.RetArray  /**/ [slice(0, outP - (long*)ret.Handles[0].Pointer)];
        }
        /// <summary>
        /// Find sequential indices of non-zero elements in n-d array, searches from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <param name="backwards">[Optional] Search direction backwards. Default: (false) start at the first element and search to the last element of <paramref name="A"/>.</param>
        /// <param name="C">[Optional] If non-null on entry: the function returns the row indices and gives column indices in <paramref name="C"/>. Default: (null) the function returns the sequential indices of elements found only.</param>
        /// <param name="n">[Optional] Maximum number of non-zero elements to find. Default: (0) find all non-zero elements.</param>
        /// <param name="V">[Optional] Return the found non-zero element values in <paramref name="V"/>. Default: (null) don't return the values.</param>
        /// <returns>Sequential, column major ordered indices of the non-zero-valued elements in <paramref name="A"/> or row indices if <paramref name="C"/> is not null.</returns>
        /// <remarks><para>If <paramref name="C"/> or <paramref name="V"/> are not null on entry they are filled with the requested respective 
        /// info as column vectors. If <paramref name="C"/> is not null it contains the column indices of non-zero values. If <paramref name="A"/> 
        /// has more than two dimensions subsequent dimensions are merged into the second dimension and the indices given in <paramref name="C"/> 
        /// are considered sequential indices of <paramref name="A"/> (column major order). </para>
        /// <para>Note, that the array returned by <see cref="find(BaseArray{short}, long, bool, OutArray{long}, OutArray{short})"/>
        /// corresponds to the indices of the rows of non-zero values if <paramref name="C"/> is not null and to sequential indices of those 
        /// non-zero values if <paramref name="C"/> is null. </para>
        /// <para>Negative values of <paramref name="n"/> are considered as abs(n).</para>
        /// <para>If <paramref name="backwards"/> is true <see cref="find(BaseArray{short}, long, bool, OutArray{long}, OutArray{short})"/>
        /// starts searching for non-zero values from the last element and proceeds in column major order to the first element or until <paramref name="n"/> 
        /// non-zero elements were found, whatever comes first.</para>
        /// <para>Find() returns and considers indices as 64 bit <see cref="long"/>. Overloads exist for backwards compatibility which expect indices as <see cref="Int32"/>
        /// values. Such functions are decorated with the suffix '32' in their names.</para>
        /// </remarks>
        internal static unsafe Array<long> find(BaseArray<short> A, long n = 0, bool backwards = false, OutArray<long> C = null, OutArray<short> V = null) {

            using (Scope.Enter()) {
                if (Equals(A, null)) {
                    throw new ArgumentException("Input array for 'find' cannot be null.");
                }
                using var _1 = ReaderLock.Create(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>, out var storageA);
                //if (storageA.S.NumberOfDimensions > 2 && !Equals(C,null)) {
                //    return find64(storageA.GetRange_ML(full, full, false), n, backwards, C, V); 
                //}
                #region  handle empty A
                if (storageA.S.NumberOfElements == 0) {
                    if (!Equals(C, null)) {
                        lock (C.SynchObj)
                            C.a = empty<long>(0);
                    }
                    if (!Equals(V, null)) {
                        lock (V.SynchObj)
                            V.a = empty<short>(dim0: 0);
                    }
                    return empty<long>(dim0: 0);
                } else if (storageA.S.NumberOfElements > long.MaxValue) {
                    throw new InvalidCastException($"The input array has too many elements for this overload of find(). Consider using find_64_ instead!");
                }
                #endregion

                if (n < 0) {
                    n = (long)Math.Abs(n);  // for hycalper...
                } else if (n == 0) {
                    n = (long)storageA.S.NumberOfElements; // assume maximum for now; 
                }
                if (n > storageA.S.NumberOfElements) {
                    n = (long)storageA.S.NumberOfElements;
                }
                Array<long> c = null;
                Array<short> v = null;

                // initialize working / out arrays with assumed maximum
                if (!Equals(C, null)) {
                    c = empty<long>(n);
                }
                if (!Equals(V, null)) {
                    v = empty<short>(n);
                }

                // initialize ret array with assumed maximum
                Array<long> ret = empty<long>(n);

                long* retP = (long*)ret.GetHostPointerForWrite();
                short* aP = (short*)storageA.Handles[0].Pointer; // size.GetSeqIndex contains storageA.S.BaseOffset;
                long* cP = (!Equals(c, null) ? (long*)c.GetHostPointerForWrite() : null);
                short* vP = (!Equals(v, null) ? (short*)v.GetHostPointerForWrite() : null);
                long lastElemDim2 = (long)(storageA.S.NumberOfElements / storageA.S[0]);
                long foundN = 0, strideA0 = (long)storageA.S[0];
                if (backwards == false) {
                    for (long ic = 0; ic < lastElemDim2 && foundN < n; ic++) {
                        for (long ir = 0; ir < strideA0 && foundN < n; ir++) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                } else {
                    for (long ic = lastElemDim2; ic-- > 0 && foundN < n;) {
                        for (long ir = (long)strideA0; ir-- > 0 && foundN < n;) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                }
                //long foundN = outP - (long*)ret.m_handles[0].Pointer;
                if (!Equals(v, null)) {
                    lock (V.SynchObj) {
                        if (foundN == 0) {
                            V.a = empty<short>(dim0: 0);
                        } else {
                            V.a = v[slice(0, foundN)];
                        }
                    }
                }
                if (!Equals(c, null)) {
                    lock (C.SynchObj) {
                        if (foundN == 0) {
                            C.a = empty<long>(dim0: 0);
                        } else {
                            C.a = c[slice(0, foundN)];
                        }
                    }
                }
                return ret[slice(0, foundN)];
            }
        }
       

        /// <summary>
        /// Finds sequential indices of non-zero elements in n-d array, search from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <returns>Sequential indices of the non-zero-valued elements in <paramref name="A"/>.</returns>
        
        internal static unsafe Array<long> find(BaseArray<byte> A) {

            if (Equals(A, null)) {
                throw new ArgumentException("Input array for 'find' cannot be null.");
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>, out var storageA);

            if (storageA.S.NumberOfElements == 0) {
                return empty<long>(dim0: 0);
            } else if (storageA.S.NumberOfElements > long.MaxValue) {
                // this is here for Hycalper reasons only. It is required for the 32 bit overloads only. 
                throw new InvalidCastException($"The input array has too many elements for this overload of find32(). Consider using find() instead!");
            }
            var ret = Storage<long>.Create();
            long nrElements = (long)storageA.S.NumberOfElements; // assume maximum for now
            ret.S.SetAll(nrElements);
            DeviceManagement.DeviceManager.GetDevice(0).New<long>((ulong)nrElements, ret.Handles);

            long* outP = (long*)ret.Handles[0].Pointer;
            long i = 0;
            foreach (var b in storageA.AsRetArray().Iterator(StorageOrders.ColumnMajor)) { // A is released after iteration
                if (b != 0) {
                    *outP++ = i;
                }
                i++;
            }
            return ret.RetArray  /**/ [slice(0, outP - (long*)ret.Handles[0].Pointer)];
        }
        /// <summary>
        /// Find sequential indices of non-zero elements in n-d array, searches from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <param name="backwards">[Optional] Search direction backwards. Default: (false) start at the first element and search to the last element of <paramref name="A"/>.</param>
        /// <param name="C">[Optional] If non-null on entry: the function returns the row indices and gives column indices in <paramref name="C"/>. Default: (null) the function returns the sequential indices of elements found only.</param>
        /// <param name="n">[Optional] Maximum number of non-zero elements to find. Default: (0) find all non-zero elements.</param>
        /// <param name="V">[Optional] Return the found non-zero element values in <paramref name="V"/>. Default: (null) don't return the values.</param>
        /// <returns>Sequential, column major ordered indices of the non-zero-valued elements in <paramref name="A"/> or row indices if <paramref name="C"/> is not null.</returns>
        /// <remarks><para>If <paramref name="C"/> or <paramref name="V"/> are not null on entry they are filled with the requested respective 
        /// info as column vectors. If <paramref name="C"/> is not null it contains the column indices of non-zero values. If <paramref name="A"/> 
        /// has more than two dimensions subsequent dimensions are merged into the second dimension and the indices given in <paramref name="C"/> 
        /// are considered sequential indices of <paramref name="A"/> (column major order). </para>
        /// <para>Note, that the array returned by <see cref="find(BaseArray{byte}, long, bool, OutArray{long}, OutArray{byte})"/>
        /// corresponds to the indices of the rows of non-zero values if <paramref name="C"/> is not null and to sequential indices of those 
        /// non-zero values if <paramref name="C"/> is null. </para>
        /// <para>Negative values of <paramref name="n"/> are considered as abs(n).</para>
        /// <para>If <paramref name="backwards"/> is true <see cref="find(BaseArray{byte}, long, bool, OutArray{long}, OutArray{byte})"/>
        /// starts searching for non-zero values from the last element and proceeds in column major order to the first element or until <paramref name="n"/> 
        /// non-zero elements were found, whatever comes first.</para>
        /// <para>Find() returns and considers indices as 64 bit <see cref="long"/>. Overloads exist for backwards compatibility which expect indices as <see cref="Int32"/>
        /// values. Such functions are decorated with the suffix '32' in their names.</para>
        /// </remarks>
        internal static unsafe Array<long> find(BaseArray<byte> A, long n = 0, bool backwards = false, OutArray<long> C = null, OutArray<byte> V = null) {

            using (Scope.Enter()) {
                if (Equals(A, null)) {
                    throw new ArgumentException("Input array for 'find' cannot be null.");
                }
                using var _1 = ReaderLock.Create(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>, out var storageA);
                //if (storageA.S.NumberOfDimensions > 2 && !Equals(C,null)) {
                //    return find64(storageA.GetRange_ML(full, full, false), n, backwards, C, V); 
                //}
                #region  handle empty A
                if (storageA.S.NumberOfElements == 0) {
                    if (!Equals(C, null)) {
                        lock (C.SynchObj)
                            C.a = empty<long>(0);
                    }
                    if (!Equals(V, null)) {
                        lock (V.SynchObj)
                            V.a = empty<byte>(dim0: 0);
                    }
                    return empty<long>(dim0: 0);
                } else if (storageA.S.NumberOfElements > long.MaxValue) {
                    throw new InvalidCastException($"The input array has too many elements for this overload of find(). Consider using find_64_ instead!");
                }
                #endregion

                if (n < 0) {
                    n = (long)Math.Abs(n);  // for hycalper...
                } else if (n == 0) {
                    n = (long)storageA.S.NumberOfElements; // assume maximum for now; 
                }
                if (n > storageA.S.NumberOfElements) {
                    n = (long)storageA.S.NumberOfElements;
                }
                Array<long> c = null;
                Array<byte> v = null;

                // initialize working / out arrays with assumed maximum
                if (!Equals(C, null)) {
                    c = empty<long>(n);
                }
                if (!Equals(V, null)) {
                    v = empty<byte>(n);
                }

                // initialize ret array with assumed maximum
                Array<long> ret = empty<long>(n);

                long* retP = (long*)ret.GetHostPointerForWrite();
                byte* aP = (byte*)storageA.Handles[0].Pointer; // size.GetSeqIndex contains storageA.S.BaseOffset;
                long* cP = (!Equals(c, null) ? (long*)c.GetHostPointerForWrite() : null);
                byte* vP = (!Equals(v, null) ? (byte*)v.GetHostPointerForWrite() : null);
                long lastElemDim2 = (long)(storageA.S.NumberOfElements / storageA.S[0]);
                long foundN = 0, strideA0 = (long)storageA.S[0];
                if (backwards == false) {
                    for (long ic = 0; ic < lastElemDim2 && foundN < n; ic++) {
                        for (long ir = 0; ir < strideA0 && foundN < n; ir++) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                } else {
                    for (long ic = lastElemDim2; ic-- > 0 && foundN < n;) {
                        for (long ir = (long)strideA0; ir-- > 0 && foundN < n;) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                }
                //long foundN = outP - (long*)ret.m_handles[0].Pointer;
                if (!Equals(v, null)) {
                    lock (V.SynchObj) {
                        if (foundN == 0) {
                            V.a = empty<byte>(dim0: 0);
                        } else {
                            V.a = v[slice(0, foundN)];
                        }
                    }
                }
                if (!Equals(c, null)) {
                    lock (C.SynchObj) {
                        if (foundN == 0) {
                            C.a = empty<long>(dim0: 0);
                        } else {
                            C.a = c[slice(0, foundN)];
                        }
                    }
                }
                return ret[slice(0, foundN)];
            }
        }
       

        /// <summary>
        /// Finds sequential indices of non-zero elements in n-d array, search from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <returns>Sequential indices of the non-zero-valued elements in <paramref name="A"/>.</returns>
        
        internal static unsafe Array<long> find(BaseArray<sbyte> A) {

            if (Equals(A, null)) {
                throw new ArgumentException("Input array for 'find' cannot be null.");
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>, out var storageA);

            if (storageA.S.NumberOfElements == 0) {
                return empty<long>(dim0: 0);
            } else if (storageA.S.NumberOfElements > long.MaxValue) {
                // this is here for Hycalper reasons only. It is required for the 32 bit overloads only. 
                throw new InvalidCastException($"The input array has too many elements for this overload of find32(). Consider using find() instead!");
            }
            var ret = Storage<long>.Create();
            long nrElements = (long)storageA.S.NumberOfElements; // assume maximum for now
            ret.S.SetAll(nrElements);
            DeviceManagement.DeviceManager.GetDevice(0).New<long>((ulong)nrElements, ret.Handles);

            long* outP = (long*)ret.Handles[0].Pointer;
            long i = 0;
            foreach (var b in storageA.AsRetArray().Iterator(StorageOrders.ColumnMajor)) { // A is released after iteration
                if (b != 0) {
                    *outP++ = i;
                }
                i++;
            }
            return ret.RetArray  /**/ [slice(0, outP - (long*)ret.Handles[0].Pointer)];
        }
        /// <summary>
        /// Find sequential indices of non-zero elements in n-d array, searches from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <param name="backwards">[Optional] Search direction backwards. Default: (false) start at the first element and search to the last element of <paramref name="A"/>.</param>
        /// <param name="C">[Optional] If non-null on entry: the function returns the row indices and gives column indices in <paramref name="C"/>. Default: (null) the function returns the sequential indices of elements found only.</param>
        /// <param name="n">[Optional] Maximum number of non-zero elements to find. Default: (0) find all non-zero elements.</param>
        /// <param name="V">[Optional] Return the found non-zero element values in <paramref name="V"/>. Default: (null) don't return the values.</param>
        /// <returns>Sequential, column major ordered indices of the non-zero-valued elements in <paramref name="A"/> or row indices if <paramref name="C"/> is not null.</returns>
        /// <remarks><para>If <paramref name="C"/> or <paramref name="V"/> are not null on entry they are filled with the requested respective 
        /// info as column vectors. If <paramref name="C"/> is not null it contains the column indices of non-zero values. If <paramref name="A"/> 
        /// has more than two dimensions subsequent dimensions are merged into the second dimension and the indices given in <paramref name="C"/> 
        /// are considered sequential indices of <paramref name="A"/> (column major order). </para>
        /// <para>Note, that the array returned by <see cref="find(BaseArray{sbyte}, long, bool, OutArray{long}, OutArray{sbyte})"/>
        /// corresponds to the indices of the rows of non-zero values if <paramref name="C"/> is not null and to sequential indices of those 
        /// non-zero values if <paramref name="C"/> is null. </para>
        /// <para>Negative values of <paramref name="n"/> are considered as abs(n).</para>
        /// <para>If <paramref name="backwards"/> is true <see cref="find(BaseArray{sbyte}, long, bool, OutArray{long}, OutArray{sbyte})"/>
        /// starts searching for non-zero values from the last element and proceeds in column major order to the first element or until <paramref name="n"/> 
        /// non-zero elements were found, whatever comes first.</para>
        /// <para>Find() returns and considers indices as 64 bit <see cref="long"/>. Overloads exist for backwards compatibility which expect indices as <see cref="Int32"/>
        /// values. Such functions are decorated with the suffix '32' in their names.</para>
        /// </remarks>
        internal static unsafe Array<long> find(BaseArray<sbyte> A, long n = 0, bool backwards = false, OutArray<long> C = null, OutArray<sbyte> V = null) {

            using (Scope.Enter()) {
                if (Equals(A, null)) {
                    throw new ArgumentException("Input array for 'find' cannot be null.");
                }
                using var _1 = ReaderLock.Create(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>, out var storageA);
                //if (storageA.S.NumberOfDimensions > 2 && !Equals(C,null)) {
                //    return find64(storageA.GetRange_ML(full, full, false), n, backwards, C, V); 
                //}
                #region  handle empty A
                if (storageA.S.NumberOfElements == 0) {
                    if (!Equals(C, null)) {
                        lock (C.SynchObj)
                            C.a = empty<long>(0);
                    }
                    if (!Equals(V, null)) {
                        lock (V.SynchObj)
                            V.a = empty<sbyte>(dim0: 0);
                    }
                    return empty<long>(dim0: 0);
                } else if (storageA.S.NumberOfElements > long.MaxValue) {
                    throw new InvalidCastException($"The input array has too many elements for this overload of find(). Consider using find_64_ instead!");
                }
                #endregion

                if (n < 0) {
                    n = (long)Math.Abs(n);  // for hycalper...
                } else if (n == 0) {
                    n = (long)storageA.S.NumberOfElements; // assume maximum for now; 
                }
                if (n > storageA.S.NumberOfElements) {
                    n = (long)storageA.S.NumberOfElements;
                }
                Array<long> c = null;
                Array<sbyte> v = null;

                // initialize working / out arrays with assumed maximum
                if (!Equals(C, null)) {
                    c = empty<long>(n);
                }
                if (!Equals(V, null)) {
                    v = empty<sbyte>(n);
                }

                // initialize ret array with assumed maximum
                Array<long> ret = empty<long>(n);

                long* retP = (long*)ret.GetHostPointerForWrite();
                sbyte* aP = (sbyte*)storageA.Handles[0].Pointer; // size.GetSeqIndex contains storageA.S.BaseOffset;
                long* cP = (!Equals(c, null) ? (long*)c.GetHostPointerForWrite() : null);
                sbyte* vP = (!Equals(v, null) ? (sbyte*)v.GetHostPointerForWrite() : null);
                long lastElemDim2 = (long)(storageA.S.NumberOfElements / storageA.S[0]);
                long foundN = 0, strideA0 = (long)storageA.S[0];
                if (backwards == false) {
                    for (long ic = 0; ic < lastElemDim2 && foundN < n; ic++) {
                        for (long ir = 0; ir < strideA0 && foundN < n; ir++) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                } else {
                    for (long ic = lastElemDim2; ic-- > 0 && foundN < n;) {
                        for (long ir = (long)strideA0; ir-- > 0 && foundN < n;) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                }
                //long foundN = outP - (long*)ret.m_handles[0].Pointer;
                if (!Equals(v, null)) {
                    lock (V.SynchObj) {
                        if (foundN == 0) {
                            V.a = empty<sbyte>(dim0: 0);
                        } else {
                            V.a = v[slice(0, foundN)];
                        }
                    }
                }
                if (!Equals(c, null)) {
                    lock (C.SynchObj) {
                        if (foundN == 0) {
                            C.a = empty<long>(dim0: 0);
                        } else {
                            C.a = c[slice(0, foundN)];
                        }
                    }
                }
                return ret[slice(0, foundN)];
            }
        }
       

        /// <summary>
        /// Finds sequential indices of non-zero elements in n-d array, search from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <returns>Sequential indices of the non-zero-valued elements in <paramref name="A"/>.</returns>
        
        internal static unsafe Array<long> find(BaseArray<fcomplex> A) {

            if (Equals(A, null)) {
                throw new ArgumentException("Input array for 'find' cannot be null.");
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, out var storageA);

            if (storageA.S.NumberOfElements == 0) {
                return empty<long>(dim0: 0);
            } else if (storageA.S.NumberOfElements > long.MaxValue) {
                // this is here for Hycalper reasons only. It is required for the 32 bit overloads only. 
                throw new InvalidCastException($"The input array has too many elements for this overload of find32(). Consider using find() instead!");
            }
            var ret = Storage<long>.Create();
            long nrElements = (long)storageA.S.NumberOfElements; // assume maximum for now
            ret.S.SetAll(nrElements);
            DeviceManagement.DeviceManager.GetDevice(0).New<long>((ulong)nrElements, ret.Handles);

            long* outP = (long*)ret.Handles[0].Pointer;
            long i = 0;
            foreach (var b in storageA.AsRetArray().Iterator(StorageOrders.ColumnMajor)) { // A is released after iteration
                if (b != 0) {
                    *outP++ = i;
                }
                i++;
            }
            return ret.RetArray  /**/ [slice(0, outP - (long*)ret.Handles[0].Pointer)];
        }
        /// <summary>
        /// Find sequential indices of non-zero elements in n-d array, searches from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <param name="backwards">[Optional] Search direction backwards. Default: (false) start at the first element and search to the last element of <paramref name="A"/>.</param>
        /// <param name="C">[Optional] If non-null on entry: the function returns the row indices and gives column indices in <paramref name="C"/>. Default: (null) the function returns the sequential indices of elements found only.</param>
        /// <param name="n">[Optional] Maximum number of non-zero elements to find. Default: (0) find all non-zero elements.</param>
        /// <param name="V">[Optional] Return the found non-zero element values in <paramref name="V"/>. Default: (null) don't return the values.</param>
        /// <returns>Sequential, column major ordered indices of the non-zero-valued elements in <paramref name="A"/> or row indices if <paramref name="C"/> is not null.</returns>
        /// <remarks><para>If <paramref name="C"/> or <paramref name="V"/> are not null on entry they are filled with the requested respective 
        /// info as column vectors. If <paramref name="C"/> is not null it contains the column indices of non-zero values. If <paramref name="A"/> 
        /// has more than two dimensions subsequent dimensions are merged into the second dimension and the indices given in <paramref name="C"/> 
        /// are considered sequential indices of <paramref name="A"/> (column major order). </para>
        /// <para>Note, that the array returned by <see cref="find(BaseArray{fcomplex}, long, bool, OutArray{long}, OutArray{fcomplex})"/>
        /// corresponds to the indices of the rows of non-zero values if <paramref name="C"/> is not null and to sequential indices of those 
        /// non-zero values if <paramref name="C"/> is null. </para>
        /// <para>Negative values of <paramref name="n"/> are considered as abs(n).</para>
        /// <para>If <paramref name="backwards"/> is true <see cref="find(BaseArray{fcomplex}, long, bool, OutArray{long}, OutArray{fcomplex})"/>
        /// starts searching for non-zero values from the last element and proceeds in column major order to the first element or until <paramref name="n"/> 
        /// non-zero elements were found, whatever comes first.</para>
        /// <para>Find() returns and considers indices as 64 bit <see cref="long"/>. Overloads exist for backwards compatibility which expect indices as <see cref="Int32"/>
        /// values. Such functions are decorated with the suffix '32' in their names.</para>
        /// </remarks>
        internal static unsafe Array<long> find(BaseArray<fcomplex> A, long n = 0, bool backwards = false, OutArray<long> C = null, OutArray<fcomplex> V = null) {

            using (Scope.Enter()) {
                if (Equals(A, null)) {
                    throw new ArgumentException("Input array for 'find' cannot be null.");
                }
                using var _1 = ReaderLock.Create(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, out var storageA);
                //if (storageA.S.NumberOfDimensions > 2 && !Equals(C,null)) {
                //    return find64(storageA.GetRange_ML(full, full, false), n, backwards, C, V); 
                //}
                #region  handle empty A
                if (storageA.S.NumberOfElements == 0) {
                    if (!Equals(C, null)) {
                        lock (C.SynchObj)
                            C.a = empty<long>(0);
                    }
                    if (!Equals(V, null)) {
                        lock (V.SynchObj)
                            V.a = empty<fcomplex>(dim0: 0);
                    }
                    return empty<long>(dim0: 0);
                } else if (storageA.S.NumberOfElements > long.MaxValue) {
                    throw new InvalidCastException($"The input array has too many elements for this overload of find(). Consider using find_64_ instead!");
                }
                #endregion

                if (n < 0) {
                    n = (long)Math.Abs(n);  // for hycalper...
                } else if (n == 0) {
                    n = (long)storageA.S.NumberOfElements; // assume maximum for now; 
                }
                if (n > storageA.S.NumberOfElements) {
                    n = (long)storageA.S.NumberOfElements;
                }
                Array<long> c = null;
                Array<fcomplex> v = null;

                // initialize working / out arrays with assumed maximum
                if (!Equals(C, null)) {
                    c = empty<long>(n);
                }
                if (!Equals(V, null)) {
                    v = empty<fcomplex>(n);
                }

                // initialize ret array with assumed maximum
                Array<long> ret = empty<long>(n);

                long* retP = (long*)ret.GetHostPointerForWrite();
                fcomplex* aP = (fcomplex*)storageA.Handles[0].Pointer; // size.GetSeqIndex contains storageA.S.BaseOffset;
                long* cP = (!Equals(c, null) ? (long*)c.GetHostPointerForWrite() : null);
                fcomplex* vP = (!Equals(v, null) ? (fcomplex*)v.GetHostPointerForWrite() : null);
                long lastElemDim2 = (long)(storageA.S.NumberOfElements / storageA.S[0]);
                long foundN = 0, strideA0 = (long)storageA.S[0];
                if (backwards == false) {
                    for (long ic = 0; ic < lastElemDim2 && foundN < n; ic++) {
                        for (long ir = 0; ir < strideA0 && foundN < n; ir++) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                } else {
                    for (long ic = lastElemDim2; ic-- > 0 && foundN < n;) {
                        for (long ir = (long)strideA0; ir-- > 0 && foundN < n;) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                }
                //long foundN = outP - (long*)ret.m_handles[0].Pointer;
                if (!Equals(v, null)) {
                    lock (V.SynchObj) {
                        if (foundN == 0) {
                            V.a = empty<fcomplex>(dim0: 0);
                        } else {
                            V.a = v[slice(0, foundN)];
                        }
                    }
                }
                if (!Equals(c, null)) {
                    lock (C.SynchObj) {
                        if (foundN == 0) {
                            C.a = empty<long>(dim0: 0);
                        } else {
                            C.a = c[slice(0, foundN)];
                        }
                    }
                }
                return ret[slice(0, foundN)];
            }
        }
       

        /// <summary>
        /// Finds sequential indices of non-zero elements in n-d array, search from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <returns>Sequential indices of the non-zero-valued elements in <paramref name="A"/>.</returns>
        
        internal static unsafe Array<long> find(BaseArray<complex> A) {

            if (Equals(A, null)) {
                throw new ArgumentException("Input array for 'find' cannot be null.");
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, out var storageA);

            if (storageA.S.NumberOfElements == 0) {
                return empty<long>(dim0: 0);
            } else if (storageA.S.NumberOfElements > long.MaxValue) {
                // this is here for Hycalper reasons only. It is required for the 32 bit overloads only. 
                throw new InvalidCastException($"The input array has too many elements for this overload of find32(). Consider using find() instead!");
            }
            var ret = Storage<long>.Create();
            long nrElements = (long)storageA.S.NumberOfElements; // assume maximum for now
            ret.S.SetAll(nrElements);
            DeviceManagement.DeviceManager.GetDevice(0).New<long>((ulong)nrElements, ret.Handles);

            long* outP = (long*)ret.Handles[0].Pointer;
            long i = 0;
            foreach (var b in storageA.AsRetArray().Iterator(StorageOrders.ColumnMajor)) { // A is released after iteration
                if (b != 0) {
                    *outP++ = i;
                }
                i++;
            }
            return ret.RetArray  /**/ [slice(0, outP - (long*)ret.Handles[0].Pointer)];
        }
        /// <summary>
        /// Find sequential indices of non-zero elements in n-d array, searches from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <param name="backwards">[Optional] Search direction backwards. Default: (false) start at the first element and search to the last element of <paramref name="A"/>.</param>
        /// <param name="C">[Optional] If non-null on entry: the function returns the row indices and gives column indices in <paramref name="C"/>. Default: (null) the function returns the sequential indices of elements found only.</param>
        /// <param name="n">[Optional] Maximum number of non-zero elements to find. Default: (0) find all non-zero elements.</param>
        /// <param name="V">[Optional] Return the found non-zero element values in <paramref name="V"/>. Default: (null) don't return the values.</param>
        /// <returns>Sequential, column major ordered indices of the non-zero-valued elements in <paramref name="A"/> or row indices if <paramref name="C"/> is not null.</returns>
        /// <remarks><para>If <paramref name="C"/> or <paramref name="V"/> are not null on entry they are filled with the requested respective 
        /// info as column vectors. If <paramref name="C"/> is not null it contains the column indices of non-zero values. If <paramref name="A"/> 
        /// has more than two dimensions subsequent dimensions are merged into the second dimension and the indices given in <paramref name="C"/> 
        /// are considered sequential indices of <paramref name="A"/> (column major order). </para>
        /// <para>Note, that the array returned by <see cref="find(BaseArray{complex}, long, bool, OutArray{long}, OutArray{complex})"/>
        /// corresponds to the indices of the rows of non-zero values if <paramref name="C"/> is not null and to sequential indices of those 
        /// non-zero values if <paramref name="C"/> is null. </para>
        /// <para>Negative values of <paramref name="n"/> are considered as abs(n).</para>
        /// <para>If <paramref name="backwards"/> is true <see cref="find(BaseArray{complex}, long, bool, OutArray{long}, OutArray{complex})"/>
        /// starts searching for non-zero values from the last element and proceeds in column major order to the first element or until <paramref name="n"/> 
        /// non-zero elements were found, whatever comes first.</para>
        /// <para>Find() returns and considers indices as 64 bit <see cref="long"/>. Overloads exist for backwards compatibility which expect indices as <see cref="Int32"/>
        /// values. Such functions are decorated with the suffix '32' in their names.</para>
        /// </remarks>
        internal static unsafe Array<long> find(BaseArray<complex> A, long n = 0, bool backwards = false, OutArray<long> C = null, OutArray<complex> V = null) {

            using (Scope.Enter()) {
                if (Equals(A, null)) {
                    throw new ArgumentException("Input array for 'find' cannot be null.");
                }
                using var _1 = ReaderLock.Create(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, out var storageA);
                //if (storageA.S.NumberOfDimensions > 2 && !Equals(C,null)) {
                //    return find64(storageA.GetRange_ML(full, full, false), n, backwards, C, V); 
                //}
                #region  handle empty A
                if (storageA.S.NumberOfElements == 0) {
                    if (!Equals(C, null)) {
                        lock (C.SynchObj)
                            C.a = empty<long>(0);
                    }
                    if (!Equals(V, null)) {
                        lock (V.SynchObj)
                            V.a = empty<complex>(dim0: 0);
                    }
                    return empty<long>(dim0: 0);
                } else if (storageA.S.NumberOfElements > long.MaxValue) {
                    throw new InvalidCastException($"The input array has too many elements for this overload of find(). Consider using find_64_ instead!");
                }
                #endregion

                if (n < 0) {
                    n = (long)Math.Abs(n);  // for hycalper...
                } else if (n == 0) {
                    n = (long)storageA.S.NumberOfElements; // assume maximum for now; 
                }
                if (n > storageA.S.NumberOfElements) {
                    n = (long)storageA.S.NumberOfElements;
                }
                Array<long> c = null;
                Array<complex> v = null;

                // initialize working / out arrays with assumed maximum
                if (!Equals(C, null)) {
                    c = empty<long>(n);
                }
                if (!Equals(V, null)) {
                    v = empty<complex>(n);
                }

                // initialize ret array with assumed maximum
                Array<long> ret = empty<long>(n);

                long* retP = (long*)ret.GetHostPointerForWrite();
                complex* aP = (complex*)storageA.Handles[0].Pointer; // size.GetSeqIndex contains storageA.S.BaseOffset;
                long* cP = (!Equals(c, null) ? (long*)c.GetHostPointerForWrite() : null);
                complex* vP = (!Equals(v, null) ? (complex*)v.GetHostPointerForWrite() : null);
                long lastElemDim2 = (long)(storageA.S.NumberOfElements / storageA.S[0]);
                long foundN = 0, strideA0 = (long)storageA.S[0];
                if (backwards == false) {
                    for (long ic = 0; ic < lastElemDim2 && foundN < n; ic++) {
                        for (long ir = 0; ir < strideA0 && foundN < n; ir++) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                } else {
                    for (long ic = lastElemDim2; ic-- > 0 && foundN < n;) {
                        for (long ir = (long)strideA0; ir-- > 0 && foundN < n;) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                }
                //long foundN = outP - (long*)ret.m_handles[0].Pointer;
                if (!Equals(v, null)) {
                    lock (V.SynchObj) {
                        if (foundN == 0) {
                            V.a = empty<complex>(dim0: 0);
                        } else {
                            V.a = v[slice(0, foundN)];
                        }
                    }
                }
                if (!Equals(c, null)) {
                    lock (C.SynchObj) {
                        if (foundN == 0) {
                            C.a = empty<long>(dim0: 0);
                        } else {
                            C.a = c[slice(0, foundN)];
                        }
                    }
                }
                return ret[slice(0, foundN)];
            }
        }
       

        /// <summary>
        /// Finds sequential indices of non-zero elements in n-d array, search from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <returns>Sequential indices of the non-zero-valued elements in <paramref name="A"/>.</returns>
        
        internal static unsafe Array<long> find(BaseArray<float> A) {

            if (Equals(A, null)) {
                throw new ArgumentException("Input array for 'find' cannot be null.");
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, out var storageA);

            if (storageA.S.NumberOfElements == 0) {
                return empty<long>(dim0: 0);
            } else if (storageA.S.NumberOfElements > long.MaxValue) {
                // this is here for Hycalper reasons only. It is required for the 32 bit overloads only. 
                throw new InvalidCastException($"The input array has too many elements for this overload of find32(). Consider using find() instead!");
            }
            var ret = Storage<long>.Create();
            long nrElements = (long)storageA.S.NumberOfElements; // assume maximum for now
            ret.S.SetAll(nrElements);
            DeviceManagement.DeviceManager.GetDevice(0).New<long>((ulong)nrElements, ret.Handles);

            long* outP = (long*)ret.Handles[0].Pointer;
            long i = 0;
            foreach (var b in storageA.AsRetArray().Iterator(StorageOrders.ColumnMajor)) { // A is released after iteration
                if (b != 0) {
                    *outP++ = i;
                }
                i++;
            }
            return ret.RetArray  /**/ [slice(0, outP - (long*)ret.Handles[0].Pointer)];
        }
        /// <summary>
        /// Find sequential indices of non-zero elements in n-d array, searches from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <param name="backwards">[Optional] Search direction backwards. Default: (false) start at the first element and search to the last element of <paramref name="A"/>.</param>
        /// <param name="C">[Optional] If non-null on entry: the function returns the row indices and gives column indices in <paramref name="C"/>. Default: (null) the function returns the sequential indices of elements found only.</param>
        /// <param name="n">[Optional] Maximum number of non-zero elements to find. Default: (0) find all non-zero elements.</param>
        /// <param name="V">[Optional] Return the found non-zero element values in <paramref name="V"/>. Default: (null) don't return the values.</param>
        /// <returns>Sequential, column major ordered indices of the non-zero-valued elements in <paramref name="A"/> or row indices if <paramref name="C"/> is not null.</returns>
        /// <remarks><para>If <paramref name="C"/> or <paramref name="V"/> are not null on entry they are filled with the requested respective 
        /// info as column vectors. If <paramref name="C"/> is not null it contains the column indices of non-zero values. If <paramref name="A"/> 
        /// has more than two dimensions subsequent dimensions are merged into the second dimension and the indices given in <paramref name="C"/> 
        /// are considered sequential indices of <paramref name="A"/> (column major order). </para>
        /// <para>Note, that the array returned by <see cref="find(BaseArray{float}, long, bool, OutArray{long}, OutArray{float})"/>
        /// corresponds to the indices of the rows of non-zero values if <paramref name="C"/> is not null and to sequential indices of those 
        /// non-zero values if <paramref name="C"/> is null. </para>
        /// <para>Negative values of <paramref name="n"/> are considered as abs(n).</para>
        /// <para>If <paramref name="backwards"/> is true <see cref="find(BaseArray{float}, long, bool, OutArray{long}, OutArray{float})"/>
        /// starts searching for non-zero values from the last element and proceeds in column major order to the first element or until <paramref name="n"/> 
        /// non-zero elements were found, whatever comes first.</para>
        /// <para>Find() returns and considers indices as 64 bit <see cref="long"/>. Overloads exist for backwards compatibility which expect indices as <see cref="Int32"/>
        /// values. Such functions are decorated with the suffix '32' in their names.</para>
        /// </remarks>
        internal static unsafe Array<long> find(BaseArray<float> A, long n = 0, bool backwards = false, OutArray<long> C = null, OutArray<float> V = null) {

            using (Scope.Enter()) {
                if (Equals(A, null)) {
                    throw new ArgumentException("Input array for 'find' cannot be null.");
                }
                using var _1 = ReaderLock.Create(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, out var storageA);
                //if (storageA.S.NumberOfDimensions > 2 && !Equals(C,null)) {
                //    return find64(storageA.GetRange_ML(full, full, false), n, backwards, C, V); 
                //}
                #region  handle empty A
                if (storageA.S.NumberOfElements == 0) {
                    if (!Equals(C, null)) {
                        lock (C.SynchObj)
                            C.a = empty<long>(0);
                    }
                    if (!Equals(V, null)) {
                        lock (V.SynchObj)
                            V.a = empty<float>(dim0: 0);
                    }
                    return empty<long>(dim0: 0);
                } else if (storageA.S.NumberOfElements > long.MaxValue) {
                    throw new InvalidCastException($"The input array has too many elements for this overload of find(). Consider using find_64_ instead!");
                }
                #endregion

                if (n < 0) {
                    n = (long)Math.Abs(n);  // for hycalper...
                } else if (n == 0) {
                    n = (long)storageA.S.NumberOfElements; // assume maximum for now; 
                }
                if (n > storageA.S.NumberOfElements) {
                    n = (long)storageA.S.NumberOfElements;
                }
                Array<long> c = null;
                Array<float> v = null;

                // initialize working / out arrays with assumed maximum
                if (!Equals(C, null)) {
                    c = empty<long>(n);
                }
                if (!Equals(V, null)) {
                    v = empty<float>(n);
                }

                // initialize ret array with assumed maximum
                Array<long> ret = empty<long>(n);

                long* retP = (long*)ret.GetHostPointerForWrite();
                float* aP = (float*)storageA.Handles[0].Pointer; // size.GetSeqIndex contains storageA.S.BaseOffset;
                long* cP = (!Equals(c, null) ? (long*)c.GetHostPointerForWrite() : null);
                float* vP = (!Equals(v, null) ? (float*)v.GetHostPointerForWrite() : null);
                long lastElemDim2 = (long)(storageA.S.NumberOfElements / storageA.S[0]);
                long foundN = 0, strideA0 = (long)storageA.S[0];
                if (backwards == false) {
                    for (long ic = 0; ic < lastElemDim2 && foundN < n; ic++) {
                        for (long ir = 0; ir < strideA0 && foundN < n; ir++) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                } else {
                    for (long ic = lastElemDim2; ic-- > 0 && foundN < n;) {
                        for (long ir = (long)strideA0; ir-- > 0 && foundN < n;) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                if (!Equals(V, null)) {
                                    *vP++ = b;
                                }
                                foundN++;
                            }
                        }
                    }
                }
                //long foundN = outP - (long*)ret.m_handles[0].Pointer;
                if (!Equals(v, null)) {
                    lock (V.SynchObj) {
                        if (foundN == 0) {
                            V.a = empty<float>(dim0: 0);
                        } else {
                            V.a = v[slice(0, foundN)];
                        }
                    }
                }
                if (!Equals(c, null)) {
                    lock (C.SynchObj) {
                        if (foundN == 0) {
                            C.a = empty<long>(dim0: 0);
                        } else {
                            C.a = c[slice(0, foundN)];
                        }
                    }
                }
                return ret[slice(0, foundN)];
            }
        }

#endregion HYCALPER AUTO GENERATED CODE

        /// <summary>
        /// Find true elements in n-d array and give their sequential index (column major order).
        /// </summary>
        /// <param name="A">Source logical array.</param>
        /// <returns>Sequential indices of the true-valued elements in <paramref name="A"/>.</returns>
        
        internal static unsafe Array<long> find(BaseArray<bool> A) {

            if (Equals(A, null)) {
                throw new ArgumentException("Input array for 'find' cannot be null.");
            }

            using var _1 = ReaderLock.Create(A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>, out var storageA);

            var ret = Storage<long>.Create();
            long nrTrues = storageA.NumberTrues;
            ret.S.SetAll(nrTrues);
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<long>((ulong)nrTrues);

            long* outP = (long*)ret.Handles[0].Pointer;
            byte* aP = (byte*)storageA.Handles[0].Pointer; // baseoffset from size.iterator!  + storageA.S.BaseOffset;
            long l = 0;
            foreach (var i in storageA.S.Iterator(StorageOrders.ColumnMajor)) { // A is released after iteration

                if (aP[i] != 0) {
                    *outP++ = l;
                }
                l++;
            }
            System.Diagnostics.Debug.Assert(outP - (long*)ret.Handles[0].Pointer == nrTrues);
            return ret.RetArray;

        }
        /// <summary>
        /// Find true elements in n-d array and give their sequential index (column major order). Assumes <![CDATA[<paramref name="A"/>.S.NumberOfElements < uint.MaxValue]]>.
        /// </summary>
        /// <param name="A">Source logical array.</param>
        /// <returns>Sequential indices of the true-valued elements in <paramref name="A"/>.</returns>
        
        internal static unsafe Array<int> find32(BaseArray<bool> A) {

            if (Equals(A, null)) {
                throw new ArgumentException("Input array for 'find' cannot be null.");
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>, out var storageA);
            var ret = Storage<int>.Create();
            int nrTrues = (int)storageA.NumberTrues;
            ret.S.SetAll(nrTrues);
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<int>((ulong)nrTrues);

            int* outP = (int*)ret.Handles[0].Pointer;
            byte* aP = (byte*)storageA.Handles[0].Pointer; // base offset from size iterator!   + storageA.S.BaseOffset;
            int l = 0;
            foreach (var i in storageA.S.Iterator(StorageOrders.ColumnMajor)) { // A is released after iteration

                if (aP[i] != 0) {
                    *outP++ = l;
                }
                l++;
            }
            System.Diagnostics.Debug.Assert(outP - (int*)ret.Handles[0].Pointer == nrTrues);
            return ret.RetArray;
        }

        /// <summary>
        /// Find sequential indices of <c>true</c> elements in n-d array, searches from start to end in column major order.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <param name="backwards">[Optional] Search direction backwards. Default: (false) start at the first element and search to the last element of <paramref name="A"/>.</param>
        /// <param name="C">[Optional] If non-null on entry: the function returns the row indices and gives column indices in <paramref name="C"/>. Default: (null) the function returns the sequential indices of elements found only.</param>
        /// <param name="n">[Optional] Maximum number of <c>true</c> elements to find. Default: (0) find all <c>true</c> elements.</param>
        /// <returns>Sequential, column major ordered indices of the <c>true</c>-valued elements in <paramref name="A"/> or row indices if <paramref name="C"/> is not null.</returns>
        /// <remarks><para>If <paramref name="C"/> is not null on entry it contains the column indices of <c>true</c> values when the function returns. If <paramref name="A"/> 
        /// has more than two dimensions subsequent dimensions are merged into the second dimension and the indices given in <paramref name="C"/> 
        /// are considered sequential indices. </para>
        /// <para>Note, that the array returned by <see cref="find(BaseArray{double}, long, bool, OutArray{long}, OutArray{double})"/>
        /// corresponds to the indices of the rows of <c>true</c> values if <paramref name="C"/> is not null and to sequential indices of those 
        /// <c>true</c> values if <paramref name="C"/> is null. </para>
        /// <para>Negative values of <paramref name="n"/> are considered as abs(n).</para>
        /// <para>If <paramref name="backwards"/> is true <see cref="find(BaseArray{double}, long, bool, OutArray{long}, OutArray{double})"/>
        /// starts searching for <c>true</c> values from the last element and proceeds in column major order to the first element or until <paramref name="n"/> 
        /// <c>true</c> elements were found, if more than <paramref name="n"/> <c>true</c> elements are contained in <paramref name="A"/>.</para></remarks>
        
        internal static unsafe Array<long> find(BaseArray<bool> A, long n = 0, bool backwards = false, OutArray<long> C = null) {

            using (Scope.Enter()) {
                if (Equals(A, null)) {
                    throw new ArgumentException("Input array for 'find' cannot be null.");
                }
                using var _1 = ReaderLock.Create(A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>, out var storageA);
                //if (storageA.S.NumberOfDimensions > 2 && !Equals(C,null)) {
                //    return find64(storageA.GetRange_ML(full, full, false), n, backwards, C, V); 
                //}
                #region  handle empty A
                if (storageA.NumberTrues == 0) {
                    if (!Equals(C, null)) {
                        lock (C.SynchObj) {
                            C.a = empty<long>(0);
                        }
                    }
                    return empty<long>(dim0: 0);
                }
                #endregion

                if (n < 0) {
                    n *= -1;
                } else if (n == 0) {
                    n = storageA.NumberTrues; // assume maximum for now; 
                }
                if (n > storageA.NumberTrues) {
                    n = storageA.NumberTrues;
                }
                Array<long> c = null;

                // outsize is known up-front
                if (!Equals(C, null)) {
                    c = empty<long>(n);
                }

                // initialize ret array with assumed maximum
                Array<long> ret = empty<long>(n);
                byte* aP = (byte*)storageA.Handles[0].Pointer; // base offset from S.GetSeqIndex!   + storageA.S.BaseOffset;
                long* retP = (long*)ret.GetHostPointerForWrite();
                long* cP = (!Equals(c, null) ? (long*)c.GetHostPointerForWrite() : null);
                long lastElemDim2 = storageA.S.NumberOfElements / storageA.S[0];
                long foundN = 0, strideA0 = storageA.S[0];
                if (backwards == false) {
                    for (long ic = 0; ic < lastElemDim2 && foundN < n; ic++) {
                        for (long ir = 0; ir < strideA0 && foundN < n; ir++) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                foundN++;
                            }
                        }
                    }
                } else {
                    for (long ic = lastElemDim2; ic-- > 0 && foundN < n;) {
                        for (long ir = strideA0; ir-- > 0 && foundN < n;) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                foundN++;
                            }
                        }
                    }
                }
                if (!Equals(c, null)) {
                    lock (C.SynchObj)
                        C.a = c;
                }
                return ret;
            }
        }
        /// <summary>
        /// Find sequential indices of <c>true</c> elements in n-d array, searches from start to end in column major order. Assumes <![CDATA[<paramref name="A"/>.S.NumberOfElements < uint.MaxValue]]>.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <param name="backwards">[Optional] Search direction backwards. Default: (false) start at the first element and search to the last element of <paramref name="A"/>.</param>
        /// <param name="C">[Optional] If non-null on entry: the function returns the row indices and gives column indices in <paramref name="C"/>. Default: (null) the function returns the sequential indices of elements found only.</param>
        /// <param name="n">[Optional] Maximum number of <c>true</c> elements to find. Default: (0) find all <c>true</c> elements.</param>
        /// <returns>Sequential, column major ordered indices of the <c>true</c>-valued elements in <paramref name="A"/> or row indices if <paramref name="C"/> is not null.</returns>
        /// <remarks><para>If <paramref name="C"/> is not null on entry it is filled with the column indices of <c>true</c> values as column vector. If <paramref name="A"/> 
        /// has more than two dimensions subsequent dimensions are merged into the second dimension and the indices given in <paramref name="C"/> 
        /// are considered sequential indices. </para>
        /// <para>Note, that the array returned by <see cref="find(BaseArray{double}, long, bool, OutArray{long}, OutArray{double})"/>
        /// corresponds to the indices of the rows of <c>true</c> values if <paramref name="C"/> is not null and to sequential indices of those 
        /// <c>true</c> values if <paramref name="C"/> is null. </para>
        /// <para>Negative values of <paramref name="n"/> are considered as abs(n).</para>
        /// <para>If <paramref name="backwards"/> is true <see cref="find(BaseArray{double}, long, bool, OutArray{long}, OutArray{double})"/>
        /// starts searching for <c>true</c> values from the last element and proceeds in column major order to the first element or until <paramref name="n"/> 
        /// <c>true</c> elements were found, if more than <paramref name="n"/> <c>true</c> elements are contained in <paramref name="A"/>.</para></remarks>
        
        internal static unsafe Array<int> find32(BaseArray<bool> A, long n = 0, bool backwards = false, OutArray<int> C = null) {

            using (Scope.Enter()) {
                if (Equals(A, null)) {
                    throw new ArgumentException("Input array for 'find' cannot be null.");
                }
                using var _1 = ReaderLock.Create(A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>, out var storageA);
                //if (storageA.S.NumberOfDimensions > 2 && !Equals(C,null)) {
                //    return find64(storageA.GetRange_ML(full, full, false), n, backwards, C, V); 
                //}
                #region  handle empty A
                if (storageA.NumberTrues == 0) {
                    if (!Equals(C, null)) {
                        lock (C.SynchObj)
                            C.a = empty<int>(0);
                    }
                    return empty<int>(dim0: 0);
                }
                #endregion

                if (n < 0) {
                    n *= -1;
                } else if (n == 0) {
                    n = storageA.NumberTrues; // assume maximum for now; 
                }
                if (n > storageA.NumberTrues) {
                    n = storageA.NumberTrues;
                }
                Array<int> c = null;

                // outsize is known up-front
                if (!Equals(C, null)) {
                    c = empty<int>(n);
                }

                // initialize ret array with assumed maximum
                Array<int> ret = empty<int>(n);
                byte* aP = (byte*)storageA.Handles[0].Pointer; // base offset from S.GetSeqIndex!    + storageA.S.BaseOffset;
                int* retP = (int*)ret.GetHostPointerForWrite();
                int* cP = (!Equals(c, null) ? (int*)c.GetHostPointerForWrite() : null);
                int lastElemDim2 = (int)(storageA.S.NumberOfElements / storageA.S[0]);
                int foundN = 0, strideA0 = (int)storageA.S[0];
                if (backwards == false) {
                    for (int ic = 0; ic < lastElemDim2 && foundN < n; ic++) {
                        for (int ir = 0; ir < strideA0 && foundN < n; ir++) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                foundN++;
                            }
                        }
                    }
                } else {
                    for (int ic = lastElemDim2; ic-- > 0 && foundN < n;) {
                        for (int ir = strideA0; ir-- > 0 && foundN < n;) {
                            var b = aP[storageA.S.GetSeqIndex(ir, ic)];
                            if (b != 0) {
                                if (!Equals(C, null)) {
                                    *retP++ = ir;
                                    *cP++ = ic;
                                } else {
                                    *retP++ = ir + ic * strideA0;
                                }
                                foundN++;
                            }
                        }
                    }
                }
                if (!Equals(c, null)) {
                    lock (C.SynchObj) C.a = c;
                }
                return ret;
            }
        }
    }
}
