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
            </type>
        </hycalper>
        */
        internal static unsafe Array<double> cumsum<IndT>(BaseArray<double> A, IndT? axis = null)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            if (!axis.HasValue) {
                // cumsum over flattened array
                return MathInternal.cumsum((A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>).Reshape(-1, StorageOrders.RowMajor));
            } else {
                int dim = axis.GetValueOrDefault().ToInt32(null);
                using var _1 = ReaderLock.Create(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, out var storage);
                if (dim < -storage.Size.NumberOfDimensions || dim >= storage.Size.NumberOfDimensions) {
                    throw new ArgumentException($"Invalid dimension index provided: {dim}. The 'axes' argument must lay in the range -{storage.Size.NumberOfDimensions}...<{storage.Size.NumberOfDimensions}.");
                }
                if (dim < 0) {
                    dim += (int)storage.Size.NumberOfDimensions;
                }
                return MathInternal.cumsum(storage.AsRetArray(), dim);
            }
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       
        internal static unsafe Array<ulong> cumsum<IndT>(BaseArray<ulong> A, IndT? axis = null)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            if (!axis.HasValue) {
                // cumsum over flattened array
                return MathInternal.cumsum((A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>).Reshape(-1, StorageOrders.RowMajor));
            } else {
                int dim = axis.GetValueOrDefault().ToInt32(null);
                using var _1 = ReaderLock.Create(A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>, out var storage);
                if (dim < -storage.Size.NumberOfDimensions || dim >= storage.Size.NumberOfDimensions) {
                    throw new ArgumentException($"Invalid dimension index provided: {dim}. The 'axes' argument must lay in the range -{storage.Size.NumberOfDimensions}...<{storage.Size.NumberOfDimensions}.");
                }
                if (dim < 0) {
                    dim += (int)storage.Size.NumberOfDimensions;
                }
                return MathInternal.cumsum(storage.AsRetArray(), dim);
            }
        }
       
        internal static unsafe Array<long> cumsum<IndT>(BaseArray<long> A, IndT? axis = null)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            if (!axis.HasValue) {
                // cumsum over flattened array
                return MathInternal.cumsum((A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>).Reshape(-1, StorageOrders.RowMajor));
            } else {
                int dim = axis.GetValueOrDefault().ToInt32(null);
                using var _1 = ReaderLock.Create(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, out var storage);
                if (dim < -storage.Size.NumberOfDimensions || dim >= storage.Size.NumberOfDimensions) {
                    throw new ArgumentException($"Invalid dimension index provided: {dim}. The 'axes' argument must lay in the range -{storage.Size.NumberOfDimensions}...<{storage.Size.NumberOfDimensions}.");
                }
                if (dim < 0) {
                    dim += (int)storage.Size.NumberOfDimensions;
                }
                return MathInternal.cumsum(storage.AsRetArray(), dim);
            }
        }
       
        internal static unsafe Array<uint> cumsum<IndT>(BaseArray<uint> A, IndT? axis = null)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            if (!axis.HasValue) {
                // cumsum over flattened array
                return MathInternal.cumsum((A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>).Reshape(-1, StorageOrders.RowMajor));
            } else {
                int dim = axis.GetValueOrDefault().ToInt32(null);
                using var _1 = ReaderLock.Create(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>, out var storage);
                if (dim < -storage.Size.NumberOfDimensions || dim >= storage.Size.NumberOfDimensions) {
                    throw new ArgumentException($"Invalid dimension index provided: {dim}. The 'axes' argument must lay in the range -{storage.Size.NumberOfDimensions}...<{storage.Size.NumberOfDimensions}.");
                }
                if (dim < 0) {
                    dim += (int)storage.Size.NumberOfDimensions;
                }
                return MathInternal.cumsum(storage.AsRetArray(), dim);
            }
        }
       
        internal static unsafe Array<int> cumsum<IndT>(BaseArray<int> A, IndT? axis = null)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            if (!axis.HasValue) {
                // cumsum over flattened array
                return MathInternal.cumsum((A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>).Reshape(-1, StorageOrders.RowMajor));
            } else {
                int dim = axis.GetValueOrDefault().ToInt32(null);
                using var _1 = ReaderLock.Create(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>, out var storage);
                if (dim < -storage.Size.NumberOfDimensions || dim >= storage.Size.NumberOfDimensions) {
                    throw new ArgumentException($"Invalid dimension index provided: {dim}. The 'axes' argument must lay in the range -{storage.Size.NumberOfDimensions}...<{storage.Size.NumberOfDimensions}.");
                }
                if (dim < 0) {
                    dim += (int)storage.Size.NumberOfDimensions;
                }
                return MathInternal.cumsum(storage.AsRetArray(), dim);
            }
        }
       
        internal static unsafe Array<ushort> cumsum<IndT>(BaseArray<ushort> A, IndT? axis = null)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            if (!axis.HasValue) {
                // cumsum over flattened array
                return MathInternal.cumsum((A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>).Reshape(-1, StorageOrders.RowMajor));
            } else {
                int dim = axis.GetValueOrDefault().ToInt32(null);
                using var _1 = ReaderLock.Create(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>, out var storage);
                if (dim < -storage.Size.NumberOfDimensions || dim >= storage.Size.NumberOfDimensions) {
                    throw new ArgumentException($"Invalid dimension index provided: {dim}. The 'axes' argument must lay in the range -{storage.Size.NumberOfDimensions}...<{storage.Size.NumberOfDimensions}.");
                }
                if (dim < 0) {
                    dim += (int)storage.Size.NumberOfDimensions;
                }
                return MathInternal.cumsum(storage.AsRetArray(), dim);
            }
        }
       
        internal static unsafe Array<short> cumsum<IndT>(BaseArray<short> A, IndT? axis = null)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            if (!axis.HasValue) {
                // cumsum over flattened array
                return MathInternal.cumsum((A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>).Reshape(-1, StorageOrders.RowMajor));
            } else {
                int dim = axis.GetValueOrDefault().ToInt32(null);
                using var _1 = ReaderLock.Create(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>, out var storage);
                if (dim < -storage.Size.NumberOfDimensions || dim >= storage.Size.NumberOfDimensions) {
                    throw new ArgumentException($"Invalid dimension index provided: {dim}. The 'axes' argument must lay in the range -{storage.Size.NumberOfDimensions}...<{storage.Size.NumberOfDimensions}.");
                }
                if (dim < 0) {
                    dim += (int)storage.Size.NumberOfDimensions;
                }
                return MathInternal.cumsum(storage.AsRetArray(), dim);
            }
        }
       
        internal static unsafe Array<byte> cumsum<IndT>(BaseArray<byte> A, IndT? axis = null)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            if (!axis.HasValue) {
                // cumsum over flattened array
                return MathInternal.cumsum((A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>).Reshape(-1, StorageOrders.RowMajor));
            } else {
                int dim = axis.GetValueOrDefault().ToInt32(null);
                using var _1 = ReaderLock.Create(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>, out var storage);
                if (dim < -storage.Size.NumberOfDimensions || dim >= storage.Size.NumberOfDimensions) {
                    throw new ArgumentException($"Invalid dimension index provided: {dim}. The 'axes' argument must lay in the range -{storage.Size.NumberOfDimensions}...<{storage.Size.NumberOfDimensions}.");
                }
                if (dim < 0) {
                    dim += (int)storage.Size.NumberOfDimensions;
                }
                return MathInternal.cumsum(storage.AsRetArray(), dim);
            }
        }
       
        internal static unsafe Array<sbyte> cumsum<IndT>(BaseArray<sbyte> A, IndT? axis = null)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            if (!axis.HasValue) {
                // cumsum over flattened array
                return MathInternal.cumsum((A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>).Reshape(-1, StorageOrders.RowMajor));
            } else {
                int dim = axis.GetValueOrDefault().ToInt32(null);
                using var _1 = ReaderLock.Create(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>, out var storage);
                if (dim < -storage.Size.NumberOfDimensions || dim >= storage.Size.NumberOfDimensions) {
                    throw new ArgumentException($"Invalid dimension index provided: {dim}. The 'axes' argument must lay in the range -{storage.Size.NumberOfDimensions}...<{storage.Size.NumberOfDimensions}.");
                }
                if (dim < 0) {
                    dim += (int)storage.Size.NumberOfDimensions;
                }
                return MathInternal.cumsum(storage.AsRetArray(), dim);
            }
        }
       
        internal static unsafe Array<fcomplex> cumsum<IndT>(BaseArray<fcomplex> A, IndT? axis = null)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            if (!axis.HasValue) {
                // cumsum over flattened array
                return MathInternal.cumsum((A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>).Reshape(-1, StorageOrders.RowMajor));
            } else {
                int dim = axis.GetValueOrDefault().ToInt32(null);
                using var _1 = ReaderLock.Create(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, out var storage);
                if (dim < -storage.Size.NumberOfDimensions || dim >= storage.Size.NumberOfDimensions) {
                    throw new ArgumentException($"Invalid dimension index provided: {dim}. The 'axes' argument must lay in the range -{storage.Size.NumberOfDimensions}...<{storage.Size.NumberOfDimensions}.");
                }
                if (dim < 0) {
                    dim += (int)storage.Size.NumberOfDimensions;
                }
                return MathInternal.cumsum(storage.AsRetArray(), dim);
            }
        }
       
        internal static unsafe Array<complex> cumsum<IndT>(BaseArray<complex> A, IndT? axis = null)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            if (!axis.HasValue) {
                // cumsum over flattened array
                return MathInternal.cumsum((A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>).Reshape(-1, StorageOrders.RowMajor));
            } else {
                int dim = axis.GetValueOrDefault().ToInt32(null);
                using var _1 = ReaderLock.Create(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, out var storage);
                if (dim < -storage.Size.NumberOfDimensions || dim >= storage.Size.NumberOfDimensions) {
                    throw new ArgumentException($"Invalid dimension index provided: {dim}. The 'axes' argument must lay in the range -{storage.Size.NumberOfDimensions}...<{storage.Size.NumberOfDimensions}.");
                }
                if (dim < 0) {
                    dim += (int)storage.Size.NumberOfDimensions;
                }
                return MathInternal.cumsum(storage.AsRetArray(), dim);
            }
        }
       
        internal static unsafe Array<float> cumsum<IndT>(BaseArray<float> A, IndT? axis = null)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            if (!axis.HasValue) {
                // cumsum over flattened array
                return MathInternal.cumsum((A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>).Reshape(-1, StorageOrders.RowMajor));
            } else {
                int dim = axis.GetValueOrDefault().ToInt32(null);
                using var _1 = ReaderLock.Create(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, out var storage);
                if (dim < -storage.Size.NumberOfDimensions || dim >= storage.Size.NumberOfDimensions) {
                    throw new ArgumentException($"Invalid dimension index provided: {dim}. The 'axes' argument must lay in the range -{storage.Size.NumberOfDimensions}...<{storage.Size.NumberOfDimensions}.");
                }
                if (dim < 0) {
                    dim += (int)storage.Size.NumberOfDimensions;
                }
                return MathInternal.cumsum(storage.AsRetArray(), dim);
            }
        }

#endregion HYCALPER AUTO GENERATED CODE

    }
}
