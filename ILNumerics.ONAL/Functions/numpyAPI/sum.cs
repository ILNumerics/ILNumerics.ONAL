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
using ILNumerics.Core.Functions.Builtin;
using ILNumerics.Core.Global;
using ILNumerics.Core.Internal;
using ILNumerics.Core.Native;
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using static ILNumerics.Core.Functions.Builtin.MathInternal;

namespace ILNumerics.Core.Functions.Builtin {

    internal static partial class numpyInternal {

        #region HYCALPER LOOPSTART SUM_TEMPLATE
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
            </type>
            <type>
                <source locate="here">
                    Double
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
            </type>
        </hycalper>
        */
        internal static unsafe Array<Double> sum<IndT>(BaseArray<double> A,
                                         BaseArray<IndT> axes = null, bool keepdims = false)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {

                throw new ArgumentNullException(nameof(A));
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, out Storage<double> storageA);
            using var _2 = ReaderLock.Create(axes as ConcreteArray<IndT, Array<IndT>, InArray<IndT>, OutArray<IndT>, Array<IndT>, Storage<IndT>>, out Storage<IndT> storageAxes);

            if (Equals(storageAxes, null) || storageAxes.Size.NumberOfElements == 0) {
                if (!keepdims) {
                    return sumall((storageA.Clone() as Storage<double>).m_retArray);
                } else {
                    return sumall((storageA.Clone() as Storage<double>).m_retArray).Reshape(array<long>(1, storageA.S.NumberOfDimensions));
                }
            } else if (storageAxes.Size.NumberOfElements == 1) {
                // single dim specified
                int dim = storageAxes.GetValue(0).ToInt32(null);
                int myDim = (dim < 0) ? dim += (int)storageA.Size.NumberOfDimensions : dim; 
                
                if (myDim < 0) {

                    throw new ArgumentException($"Invalid dimension index #{dim} for sum().");
                }
                /*!HC:emptyDims1*/
#if !EMPTY_DIMS_1
                if (storageA.S[dim] == 0) {
                    return Helper.ArrayAllDimsSameExcept<double>(0, storageA, (uint)dim, 1, keepdims); 
                }
#endif
                return MathInternal.sum((storageA.Clone() as Storage<double>).m_retArray, myDim, keepdims);
            } else if (storageAxes.Size.NumberOfElements > storageA.Size.NumberOfDimensions) {

                throw new ArgumentException($"Too many indices specified for sum().");

            } else {
                // multiple dimensions specified
                using (Scope.Enter()) {
                    Array<int> dims = convert<int>((storageAxes.Clone() as Storage<IndT>).m_retArray); // axes are already released by reader lock on exit
                    System.Diagnostics.Debug.Assert(dims.S.NumberOfElements > 1); 
                    int min, max;
                    if (!dims.GetLimits(out min, out max) || min < -storageA.Size.NumberOfDimensions) { // virtual dimensions are allowed! || max >= storageA.Size.NumberOfDimensions) {
                        throw new ArgumentException($"Dimension indices for sum() are out of the existing dimension range.");
                    }
                    // convert negative
                    dims[dims < 0] = dims[dims < 0] + (int)storageA.Size.NumberOfDimensions;
                    // trying to optimize for caches 
                    dims.a = sort(dims, descending: storageA.Size.StorageOrder == StorageOrders.RowMajor);
                    if (anyall(diff(dims) == 0)) {
                        throw new ArgumentException($"Multiple dimension indices specified for sum().");
                    }
                    // first iteration may changes the type for other implementation of this template (-> any, all)
                    Array<Double> ret = null;
                    var dim = (uint)dims.GetValue(0);
                    /*!HC:emptyDims1*/
#if !EMPTY_DIMS_1
                    if (storageA.S[dim] == 0) {
                        ret = Helper.ArrayAllDimsSameExcept<double>(0, storageA, dim, 1, true);
                    } else
#endif
                        ret = MathInternal.sum((storageA.Clone() as Storage<double>).m_retArray, (int)dim, true);

                    for (uint i = 0; i < dims.S.NumberOfElements; i++) {
                        dim = (uint)dims.GetValue(i);
                        /*!HC:emptyDims1*/
#if !EMPTY_DIMS_1
                        if (storageA.S[dim] == 0) {
                            ret.a = Helper.ArrayAllDimsSameExcept<double>(0, ret.Storage, dim, 1, true);
                        } else
#endif
                            ret.a = MathInternal.sum(ret, (int)dim, true);
                        
                    }
                    if (!keepdims) {
                        ret.a = squeeze(ret);
                    }
                    return ret;
                }
            }
        }
#endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       
        internal static unsafe Array<ulong> sum<IndT>(BaseArray<ulong> A,
                                         BaseArray<IndT> axes = null, bool keepdims = false)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {

                throw new ArgumentNullException(nameof(A));
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>, out Storage<ulong> storageA);
            using var _2 = ReaderLock.Create(axes as ConcreteArray<IndT, Array<IndT>, InArray<IndT>, OutArray<IndT>, Array<IndT>, Storage<IndT>>, out Storage<IndT> storageAxes);

            if (Equals(storageAxes, null) || storageAxes.Size.NumberOfElements == 0) {
                if (!keepdims) {
                    return sumall((storageA.Clone() as Storage<ulong>).m_retArray);
                } else {
                    return sumall((storageA.Clone() as Storage<ulong>).m_retArray).Reshape(array<long>(1, storageA.S.NumberOfDimensions));
                }
            } else if (storageAxes.Size.NumberOfElements == 1) {
                // single dim specified
                int dim = storageAxes.GetValue(0).ToInt32(null);
                int myDim = (dim < 0) ? dim += (int)storageA.Size.NumberOfDimensions : dim; 
                
                if (myDim < 0) {

                    throw new ArgumentException($"Invalid dimension index #{dim} for sum().");
                }
               
#if !EMPTY_DIMS_1
                if (storageA.S[dim] == 0) {
                    return Helper.ArrayAllDimsSameExcept<ulong>(0, storageA, (uint)dim, 1, keepdims); 
                }
#endif
                return MathInternal.sum((storageA.Clone() as Storage<ulong>).m_retArray, myDim, keepdims);
            } else if (storageAxes.Size.NumberOfElements > storageA.Size.NumberOfDimensions) {

                throw new ArgumentException($"Too many indices specified for sum().");

            } else {
                // multiple dimensions specified
                using (Scope.Enter()) {
                    Array<int> dims = convert<int>((storageAxes.Clone() as Storage<IndT>).m_retArray); // axes are already released by reader lock on exit
                    System.Diagnostics.Debug.Assert(dims.S.NumberOfElements > 1); 
                    int min, max;
                    if (!dims.GetLimits(out min, out max) || min < -storageA.Size.NumberOfDimensions) { // virtual dimensions are allowed! || max >= storageA.Size.NumberOfDimensions) {
                        throw new ArgumentException($"Dimension indices for sum() are out of the existing dimension range.");
                    }
                    // convert negative
                    dims[dims < 0] = dims[dims < 0] + (int)storageA.Size.NumberOfDimensions;
                    // trying to optimize for caches 
                    dims.a = sort(dims, descending: storageA.Size.StorageOrder == StorageOrders.RowMajor);
                    if (anyall(diff(dims) == 0)) {
                        throw new ArgumentException($"Multiple dimension indices specified for sum().");
                    }
                    // first iteration may changes the type for other implementation of this template (-> any, all)
                    Array<ulong> ret = null;
                    var dim = (uint)dims.GetValue(0);
                   
#if !EMPTY_DIMS_1
                    if (storageA.S[dim] == 0) {
                        ret = Helper.ArrayAllDimsSameExcept<ulong>(0, storageA, dim, 1, true);
                    } else
#endif
                        ret = MathInternal.sum((storageA.Clone() as Storage<ulong>).m_retArray, (int)dim, true);

                    for (uint i = 0; i < dims.S.NumberOfElements; i++) {
                        dim = (uint)dims.GetValue(i);
                       
#if !EMPTY_DIMS_1
                        if (storageA.S[dim] == 0) {
                            ret.a = Helper.ArrayAllDimsSameExcept<ulong>(0, ret.Storage, dim, 1, true);
                        } else
#endif
                            ret.a = MathInternal.sum(ret, (int)dim, true);
                        
                    }
                    if (!keepdims) {
                        ret.a = squeeze(ret);
                    }
                    return ret;
                }
            }
        }
       
        internal static unsafe Array<long> sum<IndT>(BaseArray<long> A,
                                         BaseArray<IndT> axes = null, bool keepdims = false)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {

                throw new ArgumentNullException(nameof(A));
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, out Storage<long> storageA);
            using var _2 = ReaderLock.Create(axes as ConcreteArray<IndT, Array<IndT>, InArray<IndT>, OutArray<IndT>, Array<IndT>, Storage<IndT>>, out Storage<IndT> storageAxes);

            if (Equals(storageAxes, null) || storageAxes.Size.NumberOfElements == 0) {
                if (!keepdims) {
                    return sumall((storageA.Clone() as Storage<long>).m_retArray);
                } else {
                    return sumall((storageA.Clone() as Storage<long>).m_retArray).Reshape(array<long>(1, storageA.S.NumberOfDimensions));
                }
            } else if (storageAxes.Size.NumberOfElements == 1) {
                // single dim specified
                int dim = storageAxes.GetValue(0).ToInt32(null);
                int myDim = (dim < 0) ? dim += (int)storageA.Size.NumberOfDimensions : dim; 
                
                if (myDim < 0) {

                    throw new ArgumentException($"Invalid dimension index #{dim} for sum().");
                }
               
#if !EMPTY_DIMS_1
                if (storageA.S[dim] == 0) {
                    return Helper.ArrayAllDimsSameExcept<long>(0, storageA, (uint)dim, 1, keepdims); 
                }
#endif
                return MathInternal.sum((storageA.Clone() as Storage<long>).m_retArray, myDim, keepdims);
            } else if (storageAxes.Size.NumberOfElements > storageA.Size.NumberOfDimensions) {

                throw new ArgumentException($"Too many indices specified for sum().");

            } else {
                // multiple dimensions specified
                using (Scope.Enter()) {
                    Array<int> dims = convert<int>((storageAxes.Clone() as Storage<IndT>).m_retArray); // axes are already released by reader lock on exit
                    System.Diagnostics.Debug.Assert(dims.S.NumberOfElements > 1); 
                    int min, max;
                    if (!dims.GetLimits(out min, out max) || min < -storageA.Size.NumberOfDimensions) { // virtual dimensions are allowed! || max >= storageA.Size.NumberOfDimensions) {
                        throw new ArgumentException($"Dimension indices for sum() are out of the existing dimension range.");
                    }
                    // convert negative
                    dims[dims < 0] = dims[dims < 0] + (int)storageA.Size.NumberOfDimensions;
                    // trying to optimize for caches 
                    dims.a = sort(dims, descending: storageA.Size.StorageOrder == StorageOrders.RowMajor);
                    if (anyall(diff(dims) == 0)) {
                        throw new ArgumentException($"Multiple dimension indices specified for sum().");
                    }
                    // first iteration may changes the type for other implementation of this template (-> any, all)
                    Array<long> ret = null;
                    var dim = (uint)dims.GetValue(0);
                   
#if !EMPTY_DIMS_1
                    if (storageA.S[dim] == 0) {
                        ret = Helper.ArrayAllDimsSameExcept<long>(0, storageA, dim, 1, true);
                    } else
#endif
                        ret = MathInternal.sum((storageA.Clone() as Storage<long>).m_retArray, (int)dim, true);

                    for (uint i = 0; i < dims.S.NumberOfElements; i++) {
                        dim = (uint)dims.GetValue(i);
                       
#if !EMPTY_DIMS_1
                        if (storageA.S[dim] == 0) {
                            ret.a = Helper.ArrayAllDimsSameExcept<long>(0, ret.Storage, dim, 1, true);
                        } else
#endif
                            ret.a = MathInternal.sum(ret, (int)dim, true);
                        
                    }
                    if (!keepdims) {
                        ret.a = squeeze(ret);
                    }
                    return ret;
                }
            }
        }
       
        internal static unsafe Array<uint> sum<IndT>(BaseArray<uint> A,
                                         BaseArray<IndT> axes = null, bool keepdims = false)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {

                throw new ArgumentNullException(nameof(A));
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>, out Storage<uint> storageA);
            using var _2 = ReaderLock.Create(axes as ConcreteArray<IndT, Array<IndT>, InArray<IndT>, OutArray<IndT>, Array<IndT>, Storage<IndT>>, out Storage<IndT> storageAxes);

            if (Equals(storageAxes, null) || storageAxes.Size.NumberOfElements == 0) {
                if (!keepdims) {
                    return sumall((storageA.Clone() as Storage<uint>).m_retArray);
                } else {
                    return sumall((storageA.Clone() as Storage<uint>).m_retArray).Reshape(array<long>(1, storageA.S.NumberOfDimensions));
                }
            } else if (storageAxes.Size.NumberOfElements == 1) {
                // single dim specified
                int dim = storageAxes.GetValue(0).ToInt32(null);
                int myDim = (dim < 0) ? dim += (int)storageA.Size.NumberOfDimensions : dim; 
                
                if (myDim < 0) {

                    throw new ArgumentException($"Invalid dimension index #{dim} for sum().");
                }
               
#if !EMPTY_DIMS_1
                if (storageA.S[dim] == 0) {
                    return Helper.ArrayAllDimsSameExcept<uint>(0, storageA, (uint)dim, 1, keepdims); 
                }
#endif
                return MathInternal.sum((storageA.Clone() as Storage<uint>).m_retArray, myDim, keepdims);
            } else if (storageAxes.Size.NumberOfElements > storageA.Size.NumberOfDimensions) {

                throw new ArgumentException($"Too many indices specified for sum().");

            } else {
                // multiple dimensions specified
                using (Scope.Enter()) {
                    Array<int> dims = convert<int>((storageAxes.Clone() as Storage<IndT>).m_retArray); // axes are already released by reader lock on exit
                    System.Diagnostics.Debug.Assert(dims.S.NumberOfElements > 1); 
                    int min, max;
                    if (!dims.GetLimits(out min, out max) || min < -storageA.Size.NumberOfDimensions) { // virtual dimensions are allowed! || max >= storageA.Size.NumberOfDimensions) {
                        throw new ArgumentException($"Dimension indices for sum() are out of the existing dimension range.");
                    }
                    // convert negative
                    dims[dims < 0] = dims[dims < 0] + (int)storageA.Size.NumberOfDimensions;
                    // trying to optimize for caches 
                    dims.a = sort(dims, descending: storageA.Size.StorageOrder == StorageOrders.RowMajor);
                    if (anyall(diff(dims) == 0)) {
                        throw new ArgumentException($"Multiple dimension indices specified for sum().");
                    }
                    // first iteration may changes the type for other implementation of this template (-> any, all)
                    Array<uint> ret = null;
                    var dim = (uint)dims.GetValue(0);
                   
#if !EMPTY_DIMS_1
                    if (storageA.S[dim] == 0) {
                        ret = Helper.ArrayAllDimsSameExcept<uint>(0, storageA, dim, 1, true);
                    } else
#endif
                        ret = MathInternal.sum((storageA.Clone() as Storage<uint>).m_retArray, (int)dim, true);

                    for (uint i = 0; i < dims.S.NumberOfElements; i++) {
                        dim = (uint)dims.GetValue(i);
                       
#if !EMPTY_DIMS_1
                        if (storageA.S[dim] == 0) {
                            ret.a = Helper.ArrayAllDimsSameExcept<uint>(0, ret.Storage, dim, 1, true);
                        } else
#endif
                            ret.a = MathInternal.sum(ret, (int)dim, true);
                        
                    }
                    if (!keepdims) {
                        ret.a = squeeze(ret);
                    }
                    return ret;
                }
            }
        }
       
        internal static unsafe Array<int> sum<IndT>(BaseArray<int> A,
                                         BaseArray<IndT> axes = null, bool keepdims = false)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {

                throw new ArgumentNullException(nameof(A));
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>, out Storage<int> storageA);
            using var _2 = ReaderLock.Create(axes as ConcreteArray<IndT, Array<IndT>, InArray<IndT>, OutArray<IndT>, Array<IndT>, Storage<IndT>>, out Storage<IndT> storageAxes);

            if (Equals(storageAxes, null) || storageAxes.Size.NumberOfElements == 0) {
                if (!keepdims) {
                    return sumall((storageA.Clone() as Storage<int>).m_retArray);
                } else {
                    return sumall((storageA.Clone() as Storage<int>).m_retArray).Reshape(array<long>(1, storageA.S.NumberOfDimensions));
                }
            } else if (storageAxes.Size.NumberOfElements == 1) {
                // single dim specified
                int dim = storageAxes.GetValue(0).ToInt32(null);
                int myDim = (dim < 0) ? dim += (int)storageA.Size.NumberOfDimensions : dim; 
                
                if (myDim < 0) {

                    throw new ArgumentException($"Invalid dimension index #{dim} for sum().");
                }
               
#if !EMPTY_DIMS_1
                if (storageA.S[dim] == 0) {
                    return Helper.ArrayAllDimsSameExcept<int>(0, storageA, (uint)dim, 1, keepdims); 
                }
#endif
                return MathInternal.sum((storageA.Clone() as Storage<int>).m_retArray, myDim, keepdims);
            } else if (storageAxes.Size.NumberOfElements > storageA.Size.NumberOfDimensions) {

                throw new ArgumentException($"Too many indices specified for sum().");

            } else {
                // multiple dimensions specified
                using (Scope.Enter()) {
                    Array<int> dims = convert<int>((storageAxes.Clone() as Storage<IndT>).m_retArray); // axes are already released by reader lock on exit
                    System.Diagnostics.Debug.Assert(dims.S.NumberOfElements > 1); 
                    int min, max;
                    if (!dims.GetLimits(out min, out max) || min < -storageA.Size.NumberOfDimensions) { // virtual dimensions are allowed! || max >= storageA.Size.NumberOfDimensions) {
                        throw new ArgumentException($"Dimension indices for sum() are out of the existing dimension range.");
                    }
                    // convert negative
                    dims[dims < 0] = dims[dims < 0] + (int)storageA.Size.NumberOfDimensions;
                    // trying to optimize for caches 
                    dims.a = sort(dims, descending: storageA.Size.StorageOrder == StorageOrders.RowMajor);
                    if (anyall(diff(dims) == 0)) {
                        throw new ArgumentException($"Multiple dimension indices specified for sum().");
                    }
                    // first iteration may changes the type for other implementation of this template (-> any, all)
                    Array<int> ret = null;
                    var dim = (uint)dims.GetValue(0);
                   
#if !EMPTY_DIMS_1
                    if (storageA.S[dim] == 0) {
                        ret = Helper.ArrayAllDimsSameExcept<int>(0, storageA, dim, 1, true);
                    } else
#endif
                        ret = MathInternal.sum((storageA.Clone() as Storage<int>).m_retArray, (int)dim, true);

                    for (uint i = 0; i < dims.S.NumberOfElements; i++) {
                        dim = (uint)dims.GetValue(i);
                       
#if !EMPTY_DIMS_1
                        if (storageA.S[dim] == 0) {
                            ret.a = Helper.ArrayAllDimsSameExcept<int>(0, ret.Storage, dim, 1, true);
                        } else
#endif
                            ret.a = MathInternal.sum(ret, (int)dim, true);
                        
                    }
                    if (!keepdims) {
                        ret.a = squeeze(ret);
                    }
                    return ret;
                }
            }
        }
       
        internal static unsafe Array<ushort> sum<IndT>(BaseArray<ushort> A,
                                         BaseArray<IndT> axes = null, bool keepdims = false)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {

                throw new ArgumentNullException(nameof(A));
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>, out Storage<ushort> storageA);
            using var _2 = ReaderLock.Create(axes as ConcreteArray<IndT, Array<IndT>, InArray<IndT>, OutArray<IndT>, Array<IndT>, Storage<IndT>>, out Storage<IndT> storageAxes);

            if (Equals(storageAxes, null) || storageAxes.Size.NumberOfElements == 0) {
                if (!keepdims) {
                    return sumall((storageA.Clone() as Storage<ushort>).m_retArray);
                } else {
                    return sumall((storageA.Clone() as Storage<ushort>).m_retArray).Reshape(array<long>(1, storageA.S.NumberOfDimensions));
                }
            } else if (storageAxes.Size.NumberOfElements == 1) {
                // single dim specified
                int dim = storageAxes.GetValue(0).ToInt32(null);
                int myDim = (dim < 0) ? dim += (int)storageA.Size.NumberOfDimensions : dim; 
                
                if (myDim < 0) {

                    throw new ArgumentException($"Invalid dimension index #{dim} for sum().");
                }
               
#if !EMPTY_DIMS_1
                if (storageA.S[dim] == 0) {
                    return Helper.ArrayAllDimsSameExcept<ushort>(0, storageA, (uint)dim, 1, keepdims); 
                }
#endif
                return MathInternal.sum((storageA.Clone() as Storage<ushort>).m_retArray, myDim, keepdims);
            } else if (storageAxes.Size.NumberOfElements > storageA.Size.NumberOfDimensions) {

                throw new ArgumentException($"Too many indices specified for sum().");

            } else {
                // multiple dimensions specified
                using (Scope.Enter()) {
                    Array<int> dims = convert<int>((storageAxes.Clone() as Storage<IndT>).m_retArray); // axes are already released by reader lock on exit
                    System.Diagnostics.Debug.Assert(dims.S.NumberOfElements > 1); 
                    int min, max;
                    if (!dims.GetLimits(out min, out max) || min < -storageA.Size.NumberOfDimensions) { // virtual dimensions are allowed! || max >= storageA.Size.NumberOfDimensions) {
                        throw new ArgumentException($"Dimension indices for sum() are out of the existing dimension range.");
                    }
                    // convert negative
                    dims[dims < 0] = dims[dims < 0] + (int)storageA.Size.NumberOfDimensions;
                    // trying to optimize for caches 
                    dims.a = sort(dims, descending: storageA.Size.StorageOrder == StorageOrders.RowMajor);
                    if (anyall(diff(dims) == 0)) {
                        throw new ArgumentException($"Multiple dimension indices specified for sum().");
                    }
                    // first iteration may changes the type for other implementation of this template (-> any, all)
                    Array<ushort> ret = null;
                    var dim = (uint)dims.GetValue(0);
                   
#if !EMPTY_DIMS_1
                    if (storageA.S[dim] == 0) {
                        ret = Helper.ArrayAllDimsSameExcept<ushort>(0, storageA, dim, 1, true);
                    } else
#endif
                        ret = MathInternal.sum((storageA.Clone() as Storage<ushort>).m_retArray, (int)dim, true);

                    for (uint i = 0; i < dims.S.NumberOfElements; i++) {
                        dim = (uint)dims.GetValue(i);
                       
#if !EMPTY_DIMS_1
                        if (storageA.S[dim] == 0) {
                            ret.a = Helper.ArrayAllDimsSameExcept<ushort>(0, ret.Storage, dim, 1, true);
                        } else
#endif
                            ret.a = MathInternal.sum(ret, (int)dim, true);
                        
                    }
                    if (!keepdims) {
                        ret.a = squeeze(ret);
                    }
                    return ret;
                }
            }
        }
       
        internal static unsafe Array<short> sum<IndT>(BaseArray<short> A,
                                         BaseArray<IndT> axes = null, bool keepdims = false)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {

                throw new ArgumentNullException(nameof(A));
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>, out Storage<short> storageA);
            using var _2 = ReaderLock.Create(axes as ConcreteArray<IndT, Array<IndT>, InArray<IndT>, OutArray<IndT>, Array<IndT>, Storage<IndT>>, out Storage<IndT> storageAxes);

            if (Equals(storageAxes, null) || storageAxes.Size.NumberOfElements == 0) {
                if (!keepdims) {
                    return sumall((storageA.Clone() as Storage<short>).m_retArray);
                } else {
                    return sumall((storageA.Clone() as Storage<short>).m_retArray).Reshape(array<long>(1, storageA.S.NumberOfDimensions));
                }
            } else if (storageAxes.Size.NumberOfElements == 1) {
                // single dim specified
                int dim = storageAxes.GetValue(0).ToInt32(null);
                int myDim = (dim < 0) ? dim += (int)storageA.Size.NumberOfDimensions : dim; 
                
                if (myDim < 0) {

                    throw new ArgumentException($"Invalid dimension index #{dim} for sum().");
                }
               
#if !EMPTY_DIMS_1
                if (storageA.S[dim] == 0) {
                    return Helper.ArrayAllDimsSameExcept<short>(0, storageA, (uint)dim, 1, keepdims); 
                }
#endif
                return MathInternal.sum((storageA.Clone() as Storage<short>).m_retArray, myDim, keepdims);
            } else if (storageAxes.Size.NumberOfElements > storageA.Size.NumberOfDimensions) {

                throw new ArgumentException($"Too many indices specified for sum().");

            } else {
                // multiple dimensions specified
                using (Scope.Enter()) {
                    Array<int> dims = convert<int>((storageAxes.Clone() as Storage<IndT>).m_retArray); // axes are already released by reader lock on exit
                    System.Diagnostics.Debug.Assert(dims.S.NumberOfElements > 1); 
                    int min, max;
                    if (!dims.GetLimits(out min, out max) || min < -storageA.Size.NumberOfDimensions) { // virtual dimensions are allowed! || max >= storageA.Size.NumberOfDimensions) {
                        throw new ArgumentException($"Dimension indices for sum() are out of the existing dimension range.");
                    }
                    // convert negative
                    dims[dims < 0] = dims[dims < 0] + (int)storageA.Size.NumberOfDimensions;
                    // trying to optimize for caches 
                    dims.a = sort(dims, descending: storageA.Size.StorageOrder == StorageOrders.RowMajor);
                    if (anyall(diff(dims) == 0)) {
                        throw new ArgumentException($"Multiple dimension indices specified for sum().");
                    }
                    // first iteration may changes the type for other implementation of this template (-> any, all)
                    Array<short> ret = null;
                    var dim = (uint)dims.GetValue(0);
                   
#if !EMPTY_DIMS_1
                    if (storageA.S[dim] == 0) {
                        ret = Helper.ArrayAllDimsSameExcept<short>(0, storageA, dim, 1, true);
                    } else
#endif
                        ret = MathInternal.sum((storageA.Clone() as Storage<short>).m_retArray, (int)dim, true);

                    for (uint i = 0; i < dims.S.NumberOfElements; i++) {
                        dim = (uint)dims.GetValue(i);
                       
#if !EMPTY_DIMS_1
                        if (storageA.S[dim] == 0) {
                            ret.a = Helper.ArrayAllDimsSameExcept<short>(0, ret.Storage, dim, 1, true);
                        } else
#endif
                            ret.a = MathInternal.sum(ret, (int)dim, true);
                        
                    }
                    if (!keepdims) {
                        ret.a = squeeze(ret);
                    }
                    return ret;
                }
            }
        }
       
        internal static unsafe Array<byte> sum<IndT>(BaseArray<byte> A,
                                         BaseArray<IndT> axes = null, bool keepdims = false)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {

                throw new ArgumentNullException(nameof(A));
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>, out Storage<byte> storageA);
            using var _2 = ReaderLock.Create(axes as ConcreteArray<IndT, Array<IndT>, InArray<IndT>, OutArray<IndT>, Array<IndT>, Storage<IndT>>, out Storage<IndT> storageAxes);

            if (Equals(storageAxes, null) || storageAxes.Size.NumberOfElements == 0) {
                if (!keepdims) {
                    return sumall((storageA.Clone() as Storage<byte>).m_retArray);
                } else {
                    return sumall((storageA.Clone() as Storage<byte>).m_retArray).Reshape(array<long>(1, storageA.S.NumberOfDimensions));
                }
            } else if (storageAxes.Size.NumberOfElements == 1) {
                // single dim specified
                int dim = storageAxes.GetValue(0).ToInt32(null);
                int myDim = (dim < 0) ? dim += (int)storageA.Size.NumberOfDimensions : dim; 
                
                if (myDim < 0) {

                    throw new ArgumentException($"Invalid dimension index #{dim} for sum().");
                }
               
#if !EMPTY_DIMS_1
                if (storageA.S[dim] == 0) {
                    return Helper.ArrayAllDimsSameExcept<byte>(0, storageA, (uint)dim, 1, keepdims); 
                }
#endif
                return MathInternal.sum((storageA.Clone() as Storage<byte>).m_retArray, myDim, keepdims);
            } else if (storageAxes.Size.NumberOfElements > storageA.Size.NumberOfDimensions) {

                throw new ArgumentException($"Too many indices specified for sum().");

            } else {
                // multiple dimensions specified
                using (Scope.Enter()) {
                    Array<int> dims = convert<int>((storageAxes.Clone() as Storage<IndT>).m_retArray); // axes are already released by reader lock on exit
                    System.Diagnostics.Debug.Assert(dims.S.NumberOfElements > 1); 
                    int min, max;
                    if (!dims.GetLimits(out min, out max) || min < -storageA.Size.NumberOfDimensions) { // virtual dimensions are allowed! || max >= storageA.Size.NumberOfDimensions) {
                        throw new ArgumentException($"Dimension indices for sum() are out of the existing dimension range.");
                    }
                    // convert negative
                    dims[dims < 0] = dims[dims < 0] + (int)storageA.Size.NumberOfDimensions;
                    // trying to optimize for caches 
                    dims.a = sort(dims, descending: storageA.Size.StorageOrder == StorageOrders.RowMajor);
                    if (anyall(diff(dims) == 0)) {
                        throw new ArgumentException($"Multiple dimension indices specified for sum().");
                    }
                    // first iteration may changes the type for other implementation of this template (-> any, all)
                    Array<byte> ret = null;
                    var dim = (uint)dims.GetValue(0);
                   
#if !EMPTY_DIMS_1
                    if (storageA.S[dim] == 0) {
                        ret = Helper.ArrayAllDimsSameExcept<byte>(0, storageA, dim, 1, true);
                    } else
#endif
                        ret = MathInternal.sum((storageA.Clone() as Storage<byte>).m_retArray, (int)dim, true);

                    for (uint i = 0; i < dims.S.NumberOfElements; i++) {
                        dim = (uint)dims.GetValue(i);
                       
#if !EMPTY_DIMS_1
                        if (storageA.S[dim] == 0) {
                            ret.a = Helper.ArrayAllDimsSameExcept<byte>(0, ret.Storage, dim, 1, true);
                        } else
#endif
                            ret.a = MathInternal.sum(ret, (int)dim, true);
                        
                    }
                    if (!keepdims) {
                        ret.a = squeeze(ret);
                    }
                    return ret;
                }
            }
        }
       
        internal static unsafe Array<sbyte> sum<IndT>(BaseArray<sbyte> A,
                                         BaseArray<IndT> axes = null, bool keepdims = false)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {

                throw new ArgumentNullException(nameof(A));
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>, out Storage<sbyte> storageA);
            using var _2 = ReaderLock.Create(axes as ConcreteArray<IndT, Array<IndT>, InArray<IndT>, OutArray<IndT>, Array<IndT>, Storage<IndT>>, out Storage<IndT> storageAxes);

            if (Equals(storageAxes, null) || storageAxes.Size.NumberOfElements == 0) {
                if (!keepdims) {
                    return sumall((storageA.Clone() as Storage<sbyte>).m_retArray);
                } else {
                    return sumall((storageA.Clone() as Storage<sbyte>).m_retArray).Reshape(array<long>(1, storageA.S.NumberOfDimensions));
                }
            } else if (storageAxes.Size.NumberOfElements == 1) {
                // single dim specified
                int dim = storageAxes.GetValue(0).ToInt32(null);
                int myDim = (dim < 0) ? dim += (int)storageA.Size.NumberOfDimensions : dim; 
                
                if (myDim < 0) {

                    throw new ArgumentException($"Invalid dimension index #{dim} for sum().");
                }
               
#if !EMPTY_DIMS_1
                if (storageA.S[dim] == 0) {
                    return Helper.ArrayAllDimsSameExcept<sbyte>(0, storageA, (uint)dim, 1, keepdims); 
                }
#endif
                return MathInternal.sum((storageA.Clone() as Storage<sbyte>).m_retArray, myDim, keepdims);
            } else if (storageAxes.Size.NumberOfElements > storageA.Size.NumberOfDimensions) {

                throw new ArgumentException($"Too many indices specified for sum().");

            } else {
                // multiple dimensions specified
                using (Scope.Enter()) {
                    Array<int> dims = convert<int>((storageAxes.Clone() as Storage<IndT>).m_retArray); // axes are already released by reader lock on exit
                    System.Diagnostics.Debug.Assert(dims.S.NumberOfElements > 1); 
                    int min, max;
                    if (!dims.GetLimits(out min, out max) || min < -storageA.Size.NumberOfDimensions) { // virtual dimensions are allowed! || max >= storageA.Size.NumberOfDimensions) {
                        throw new ArgumentException($"Dimension indices for sum() are out of the existing dimension range.");
                    }
                    // convert negative
                    dims[dims < 0] = dims[dims < 0] + (int)storageA.Size.NumberOfDimensions;
                    // trying to optimize for caches 
                    dims.a = sort(dims, descending: storageA.Size.StorageOrder == StorageOrders.RowMajor);
                    if (anyall(diff(dims) == 0)) {
                        throw new ArgumentException($"Multiple dimension indices specified for sum().");
                    }
                    // first iteration may changes the type for other implementation of this template (-> any, all)
                    Array<sbyte> ret = null;
                    var dim = (uint)dims.GetValue(0);
                   
#if !EMPTY_DIMS_1
                    if (storageA.S[dim] == 0) {
                        ret = Helper.ArrayAllDimsSameExcept<sbyte>(0, storageA, dim, 1, true);
                    } else
#endif
                        ret = MathInternal.sum((storageA.Clone() as Storage<sbyte>).m_retArray, (int)dim, true);

                    for (uint i = 0; i < dims.S.NumberOfElements; i++) {
                        dim = (uint)dims.GetValue(i);
                       
#if !EMPTY_DIMS_1
                        if (storageA.S[dim] == 0) {
                            ret.a = Helper.ArrayAllDimsSameExcept<sbyte>(0, ret.Storage, dim, 1, true);
                        } else
#endif
                            ret.a = MathInternal.sum(ret, (int)dim, true);
                        
                    }
                    if (!keepdims) {
                        ret.a = squeeze(ret);
                    }
                    return ret;
                }
            }
        }
       
        internal static unsafe Array<fcomplex> sum<IndT>(BaseArray<fcomplex> A,
                                         BaseArray<IndT> axes = null, bool keepdims = false)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {

                throw new ArgumentNullException(nameof(A));
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, out Storage<fcomplex> storageA);
            using var _2 = ReaderLock.Create(axes as ConcreteArray<IndT, Array<IndT>, InArray<IndT>, OutArray<IndT>, Array<IndT>, Storage<IndT>>, out Storage<IndT> storageAxes);

            if (Equals(storageAxes, null) || storageAxes.Size.NumberOfElements == 0) {
                if (!keepdims) {
                    return sumall((storageA.Clone() as Storage<fcomplex>).m_retArray);
                } else {
                    return sumall((storageA.Clone() as Storage<fcomplex>).m_retArray).Reshape(array<long>(1, storageA.S.NumberOfDimensions));
                }
            } else if (storageAxes.Size.NumberOfElements == 1) {
                // single dim specified
                int dim = storageAxes.GetValue(0).ToInt32(null);
                int myDim = (dim < 0) ? dim += (int)storageA.Size.NumberOfDimensions : dim; 
                
                if (myDim < 0) {

                    throw new ArgumentException($"Invalid dimension index #{dim} for sum().");
                }
               
#if !EMPTY_DIMS_1
                if (storageA.S[dim] == 0) {
                    return Helper.ArrayAllDimsSameExcept<fcomplex>(0, storageA, (uint)dim, 1, keepdims); 
                }
#endif
                return MathInternal.sum((storageA.Clone() as Storage<fcomplex>).m_retArray, myDim, keepdims);
            } else if (storageAxes.Size.NumberOfElements > storageA.Size.NumberOfDimensions) {

                throw new ArgumentException($"Too many indices specified for sum().");

            } else {
                // multiple dimensions specified
                using (Scope.Enter()) {
                    Array<int> dims = convert<int>((storageAxes.Clone() as Storage<IndT>).m_retArray); // axes are already released by reader lock on exit
                    System.Diagnostics.Debug.Assert(dims.S.NumberOfElements > 1); 
                    int min, max;
                    if (!dims.GetLimits(out min, out max) || min < -storageA.Size.NumberOfDimensions) { // virtual dimensions are allowed! || max >= storageA.Size.NumberOfDimensions) {
                        throw new ArgumentException($"Dimension indices for sum() are out of the existing dimension range.");
                    }
                    // convert negative
                    dims[dims < 0] = dims[dims < 0] + (int)storageA.Size.NumberOfDimensions;
                    // trying to optimize for caches 
                    dims.a = sort(dims, descending: storageA.Size.StorageOrder == StorageOrders.RowMajor);
                    if (anyall(diff(dims) == 0)) {
                        throw new ArgumentException($"Multiple dimension indices specified for sum().");
                    }
                    // first iteration may changes the type for other implementation of this template (-> any, all)
                    Array<fcomplex> ret = null;
                    var dim = (uint)dims.GetValue(0);
                   
#if !EMPTY_DIMS_1
                    if (storageA.S[dim] == 0) {
                        ret = Helper.ArrayAllDimsSameExcept<fcomplex>(0, storageA, dim, 1, true);
                    } else
#endif
                        ret = MathInternal.sum((storageA.Clone() as Storage<fcomplex>).m_retArray, (int)dim, true);

                    for (uint i = 0; i < dims.S.NumberOfElements; i++) {
                        dim = (uint)dims.GetValue(i);
                       
#if !EMPTY_DIMS_1
                        if (storageA.S[dim] == 0) {
                            ret.a = Helper.ArrayAllDimsSameExcept<fcomplex>(0, ret.Storage, dim, 1, true);
                        } else
#endif
                            ret.a = MathInternal.sum(ret, (int)dim, true);
                        
                    }
                    if (!keepdims) {
                        ret.a = squeeze(ret);
                    }
                    return ret;
                }
            }
        }
       
        internal static unsafe Array<complex> sum<IndT>(BaseArray<complex> A,
                                         BaseArray<IndT> axes = null, bool keepdims = false)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {

                throw new ArgumentNullException(nameof(A));
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, out Storage<complex> storageA);
            using var _2 = ReaderLock.Create(axes as ConcreteArray<IndT, Array<IndT>, InArray<IndT>, OutArray<IndT>, Array<IndT>, Storage<IndT>>, out Storage<IndT> storageAxes);

            if (Equals(storageAxes, null) || storageAxes.Size.NumberOfElements == 0) {
                if (!keepdims) {
                    return sumall((storageA.Clone() as Storage<complex>).m_retArray);
                } else {
                    return sumall((storageA.Clone() as Storage<complex>).m_retArray).Reshape(array<long>(1, storageA.S.NumberOfDimensions));
                }
            } else if (storageAxes.Size.NumberOfElements == 1) {
                // single dim specified
                int dim = storageAxes.GetValue(0).ToInt32(null);
                int myDim = (dim < 0) ? dim += (int)storageA.Size.NumberOfDimensions : dim; 
                
                if (myDim < 0) {

                    throw new ArgumentException($"Invalid dimension index #{dim} for sum().");
                }
               
#if !EMPTY_DIMS_1
                if (storageA.S[dim] == 0) {
                    return Helper.ArrayAllDimsSameExcept<complex>(0, storageA, (uint)dim, 1, keepdims); 
                }
#endif
                return MathInternal.sum((storageA.Clone() as Storage<complex>).m_retArray, myDim, keepdims);
            } else if (storageAxes.Size.NumberOfElements > storageA.Size.NumberOfDimensions) {

                throw new ArgumentException($"Too many indices specified for sum().");

            } else {
                // multiple dimensions specified
                using (Scope.Enter()) {
                    Array<int> dims = convert<int>((storageAxes.Clone() as Storage<IndT>).m_retArray); // axes are already released by reader lock on exit
                    System.Diagnostics.Debug.Assert(dims.S.NumberOfElements > 1); 
                    int min, max;
                    if (!dims.GetLimits(out min, out max) || min < -storageA.Size.NumberOfDimensions) { // virtual dimensions are allowed! || max >= storageA.Size.NumberOfDimensions) {
                        throw new ArgumentException($"Dimension indices for sum() are out of the existing dimension range.");
                    }
                    // convert negative
                    dims[dims < 0] = dims[dims < 0] + (int)storageA.Size.NumberOfDimensions;
                    // trying to optimize for caches 
                    dims.a = sort(dims, descending: storageA.Size.StorageOrder == StorageOrders.RowMajor);
                    if (anyall(diff(dims) == 0)) {
                        throw new ArgumentException($"Multiple dimension indices specified for sum().");
                    }
                    // first iteration may changes the type for other implementation of this template (-> any, all)
                    Array<complex> ret = null;
                    var dim = (uint)dims.GetValue(0);
                   
#if !EMPTY_DIMS_1
                    if (storageA.S[dim] == 0) {
                        ret = Helper.ArrayAllDimsSameExcept<complex>(0, storageA, dim, 1, true);
                    } else
#endif
                        ret = MathInternal.sum((storageA.Clone() as Storage<complex>).m_retArray, (int)dim, true);

                    for (uint i = 0; i < dims.S.NumberOfElements; i++) {
                        dim = (uint)dims.GetValue(i);
                       
#if !EMPTY_DIMS_1
                        if (storageA.S[dim] == 0) {
                            ret.a = Helper.ArrayAllDimsSameExcept<complex>(0, ret.Storage, dim, 1, true);
                        } else
#endif
                            ret.a = MathInternal.sum(ret, (int)dim, true);
                        
                    }
                    if (!keepdims) {
                        ret.a = squeeze(ret);
                    }
                    return ret;
                }
            }
        }
       
        internal static unsafe Array<float> sum<IndT>(BaseArray<float> A,
                                         BaseArray<IndT> axes = null, bool keepdims = false)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {

                throw new ArgumentNullException(nameof(A));
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, out Storage<float> storageA);
            using var _2 = ReaderLock.Create(axes as ConcreteArray<IndT, Array<IndT>, InArray<IndT>, OutArray<IndT>, Array<IndT>, Storage<IndT>>, out Storage<IndT> storageAxes);

            if (Equals(storageAxes, null) || storageAxes.Size.NumberOfElements == 0) {
                if (!keepdims) {
                    return sumall((storageA.Clone() as Storage<float>).m_retArray);
                } else {
                    return sumall((storageA.Clone() as Storage<float>).m_retArray).Reshape(array<long>(1, storageA.S.NumberOfDimensions));
                }
            } else if (storageAxes.Size.NumberOfElements == 1) {
                // single dim specified
                int dim = storageAxes.GetValue(0).ToInt32(null);
                int myDim = (dim < 0) ? dim += (int)storageA.Size.NumberOfDimensions : dim; 
                
                if (myDim < 0) {

                    throw new ArgumentException($"Invalid dimension index #{dim} for sum().");
                }
               
#if !EMPTY_DIMS_1
                if (storageA.S[dim] == 0) {
                    return Helper.ArrayAllDimsSameExcept<float>(0, storageA, (uint)dim, 1, keepdims); 
                }
#endif
                return MathInternal.sum((storageA.Clone() as Storage<float>).m_retArray, myDim, keepdims);
            } else if (storageAxes.Size.NumberOfElements > storageA.Size.NumberOfDimensions) {

                throw new ArgumentException($"Too many indices specified for sum().");

            } else {
                // multiple dimensions specified
                using (Scope.Enter()) {
                    Array<int> dims = convert<int>((storageAxes.Clone() as Storage<IndT>).m_retArray); // axes are already released by reader lock on exit
                    System.Diagnostics.Debug.Assert(dims.S.NumberOfElements > 1); 
                    int min, max;
                    if (!dims.GetLimits(out min, out max) || min < -storageA.Size.NumberOfDimensions) { // virtual dimensions are allowed! || max >= storageA.Size.NumberOfDimensions) {
                        throw new ArgumentException($"Dimension indices for sum() are out of the existing dimension range.");
                    }
                    // convert negative
                    dims[dims < 0] = dims[dims < 0] + (int)storageA.Size.NumberOfDimensions;
                    // trying to optimize for caches 
                    dims.a = sort(dims, descending: storageA.Size.StorageOrder == StorageOrders.RowMajor);
                    if (anyall(diff(dims) == 0)) {
                        throw new ArgumentException($"Multiple dimension indices specified for sum().");
                    }
                    // first iteration may changes the type for other implementation of this template (-> any, all)
                    Array<float> ret = null;
                    var dim = (uint)dims.GetValue(0);
                   
#if !EMPTY_DIMS_1
                    if (storageA.S[dim] == 0) {
                        ret = Helper.ArrayAllDimsSameExcept<float>(0, storageA, dim, 1, true);
                    } else
#endif
                        ret = MathInternal.sum((storageA.Clone() as Storage<float>).m_retArray, (int)dim, true);

                    for (uint i = 0; i < dims.S.NumberOfElements; i++) {
                        dim = (uint)dims.GetValue(i);
                       
#if !EMPTY_DIMS_1
                        if (storageA.S[dim] == 0) {
                            ret.a = Helper.ArrayAllDimsSameExcept<float>(0, ret.Storage, dim, 1, true);
                        } else
#endif
                            ret.a = MathInternal.sum(ret, (int)dim, true);
                        
                    }
                    if (!keepdims) {
                        ret.a = squeeze(ret);
                    }
                    return ret;
                }
            }
        }

#endregion HYCALPER AUTO GENERATED CODE

    }
}
