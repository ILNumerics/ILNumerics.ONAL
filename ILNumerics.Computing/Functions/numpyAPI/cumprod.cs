//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////
using ILNumerics.Core.Arrays;
using ILNumerics.Core.Global;
using ILNumerics.Core.Internal;
using ILNumerics.Core.StorageLayer;
using System;

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
        internal static unsafe Array<double> cumprod<IndT>(BaseArray<double> A, IndT? axis = null)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            if (!axis.HasValue) {
                // cumprod over flattened array
                return MathInternal.cumprod((A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>).Reshape(-1, StorageOrders.RowMajor));
            } else {
                int dim = axis.GetValueOrDefault().ToInt32(null);
                using var _1 = ReaderLock.Create(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, out var storage); 
                if (dim < -storage.Size.NumberOfDimensions || dim >= storage.Size.NumberOfDimensions) {
                    throw new ArgumentException($"Invalid dimension index provided: {dim}. The 'axes' argument must lay in the range -{storage.Size.NumberOfDimensions}...<{storage.Size.NumberOfDimensions}.");
                }
                if (dim < 0) {
                    dim += (int)storage.Size.NumberOfDimensions;
                }
                return MathInternal.cumprod(storage.AsRetArray(), dim);
            }
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       
        internal static unsafe Array<ulong> cumprod<IndT>(BaseArray<ulong> A, IndT? axis = null)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            if (!axis.HasValue) {
                // cumprod over flattened array
                return MathInternal.cumprod((A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>).Reshape(-1, StorageOrders.RowMajor));
            } else {
                int dim = axis.GetValueOrDefault().ToInt32(null);
                using var _1 = ReaderLock.Create(A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>, out var storage); 
                if (dim < -storage.Size.NumberOfDimensions || dim >= storage.Size.NumberOfDimensions) {
                    throw new ArgumentException($"Invalid dimension index provided: {dim}. The 'axes' argument must lay in the range -{storage.Size.NumberOfDimensions}...<{storage.Size.NumberOfDimensions}.");
                }
                if (dim < 0) {
                    dim += (int)storage.Size.NumberOfDimensions;
                }
                return MathInternal.cumprod(storage.AsRetArray(), dim);
            }
        }
       
        internal static unsafe Array<long> cumprod<IndT>(BaseArray<long> A, IndT? axis = null)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            if (!axis.HasValue) {
                // cumprod over flattened array
                return MathInternal.cumprod((A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>).Reshape(-1, StorageOrders.RowMajor));
            } else {
                int dim = axis.GetValueOrDefault().ToInt32(null);
                using var _1 = ReaderLock.Create(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>, out var storage); 
                if (dim < -storage.Size.NumberOfDimensions || dim >= storage.Size.NumberOfDimensions) {
                    throw new ArgumentException($"Invalid dimension index provided: {dim}. The 'axes' argument must lay in the range -{storage.Size.NumberOfDimensions}...<{storage.Size.NumberOfDimensions}.");
                }
                if (dim < 0) {
                    dim += (int)storage.Size.NumberOfDimensions;
                }
                return MathInternal.cumprod(storage.AsRetArray(), dim);
            }
        }
       
        internal static unsafe Array<uint> cumprod<IndT>(BaseArray<uint> A, IndT? axis = null)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            if (!axis.HasValue) {
                // cumprod over flattened array
                return MathInternal.cumprod((A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>).Reshape(-1, StorageOrders.RowMajor));
            } else {
                int dim = axis.GetValueOrDefault().ToInt32(null);
                using var _1 = ReaderLock.Create(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>, out var storage); 
                if (dim < -storage.Size.NumberOfDimensions || dim >= storage.Size.NumberOfDimensions) {
                    throw new ArgumentException($"Invalid dimension index provided: {dim}. The 'axes' argument must lay in the range -{storage.Size.NumberOfDimensions}...<{storage.Size.NumberOfDimensions}.");
                }
                if (dim < 0) {
                    dim += (int)storage.Size.NumberOfDimensions;
                }
                return MathInternal.cumprod(storage.AsRetArray(), dim);
            }
        }
       
        internal static unsafe Array<int> cumprod<IndT>(BaseArray<int> A, IndT? axis = null)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            if (!axis.HasValue) {
                // cumprod over flattened array
                return MathInternal.cumprod((A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>).Reshape(-1, StorageOrders.RowMajor));
            } else {
                int dim = axis.GetValueOrDefault().ToInt32(null);
                using var _1 = ReaderLock.Create(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>, out var storage); 
                if (dim < -storage.Size.NumberOfDimensions || dim >= storage.Size.NumberOfDimensions) {
                    throw new ArgumentException($"Invalid dimension index provided: {dim}. The 'axes' argument must lay in the range -{storage.Size.NumberOfDimensions}...<{storage.Size.NumberOfDimensions}.");
                }
                if (dim < 0) {
                    dim += (int)storage.Size.NumberOfDimensions;
                }
                return MathInternal.cumprod(storage.AsRetArray(), dim);
            }
        }
       
        internal static unsafe Array<ushort> cumprod<IndT>(BaseArray<ushort> A, IndT? axis = null)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            if (!axis.HasValue) {
                // cumprod over flattened array
                return MathInternal.cumprod((A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>).Reshape(-1, StorageOrders.RowMajor));
            } else {
                int dim = axis.GetValueOrDefault().ToInt32(null);
                using var _1 = ReaderLock.Create(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>, out var storage); 
                if (dim < -storage.Size.NumberOfDimensions || dim >= storage.Size.NumberOfDimensions) {
                    throw new ArgumentException($"Invalid dimension index provided: {dim}. The 'axes' argument must lay in the range -{storage.Size.NumberOfDimensions}...<{storage.Size.NumberOfDimensions}.");
                }
                if (dim < 0) {
                    dim += (int)storage.Size.NumberOfDimensions;
                }
                return MathInternal.cumprod(storage.AsRetArray(), dim);
            }
        }
       
        internal static unsafe Array<short> cumprod<IndT>(BaseArray<short> A, IndT? axis = null)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            if (!axis.HasValue) {
                // cumprod over flattened array
                return MathInternal.cumprod((A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>).Reshape(-1, StorageOrders.RowMajor));
            } else {
                int dim = axis.GetValueOrDefault().ToInt32(null);
                using var _1 = ReaderLock.Create(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>, out var storage); 
                if (dim < -storage.Size.NumberOfDimensions || dim >= storage.Size.NumberOfDimensions) {
                    throw new ArgumentException($"Invalid dimension index provided: {dim}. The 'axes' argument must lay in the range -{storage.Size.NumberOfDimensions}...<{storage.Size.NumberOfDimensions}.");
                }
                if (dim < 0) {
                    dim += (int)storage.Size.NumberOfDimensions;
                }
                return MathInternal.cumprod(storage.AsRetArray(), dim);
            }
        }
       
        internal static unsafe Array<byte> cumprod<IndT>(BaseArray<byte> A, IndT? axis = null)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            if (!axis.HasValue) {
                // cumprod over flattened array
                return MathInternal.cumprod((A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>).Reshape(-1, StorageOrders.RowMajor));
            } else {
                int dim = axis.GetValueOrDefault().ToInt32(null);
                using var _1 = ReaderLock.Create(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>, out var storage); 
                if (dim < -storage.Size.NumberOfDimensions || dim >= storage.Size.NumberOfDimensions) {
                    throw new ArgumentException($"Invalid dimension index provided: {dim}. The 'axes' argument must lay in the range -{storage.Size.NumberOfDimensions}...<{storage.Size.NumberOfDimensions}.");
                }
                if (dim < 0) {
                    dim += (int)storage.Size.NumberOfDimensions;
                }
                return MathInternal.cumprod(storage.AsRetArray(), dim);
            }
        }
       
        internal static unsafe Array<sbyte> cumprod<IndT>(BaseArray<sbyte> A, IndT? axis = null)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            if (!axis.HasValue) {
                // cumprod over flattened array
                return MathInternal.cumprod((A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>).Reshape(-1, StorageOrders.RowMajor));
            } else {
                int dim = axis.GetValueOrDefault().ToInt32(null);
                using var _1 = ReaderLock.Create(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>, out var storage); 
                if (dim < -storage.Size.NumberOfDimensions || dim >= storage.Size.NumberOfDimensions) {
                    throw new ArgumentException($"Invalid dimension index provided: {dim}. The 'axes' argument must lay in the range -{storage.Size.NumberOfDimensions}...<{storage.Size.NumberOfDimensions}.");
                }
                if (dim < 0) {
                    dim += (int)storage.Size.NumberOfDimensions;
                }
                return MathInternal.cumprod(storage.AsRetArray(), dim);
            }
        }
       
        internal static unsafe Array<fcomplex> cumprod<IndT>(BaseArray<fcomplex> A, IndT? axis = null)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            if (!axis.HasValue) {
                // cumprod over flattened array
                return MathInternal.cumprod((A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>).Reshape(-1, StorageOrders.RowMajor));
            } else {
                int dim = axis.GetValueOrDefault().ToInt32(null);
                using var _1 = ReaderLock.Create(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>, out var storage); 
                if (dim < -storage.Size.NumberOfDimensions || dim >= storage.Size.NumberOfDimensions) {
                    throw new ArgumentException($"Invalid dimension index provided: {dim}. The 'axes' argument must lay in the range -{storage.Size.NumberOfDimensions}...<{storage.Size.NumberOfDimensions}.");
                }
                if (dim < 0) {
                    dim += (int)storage.Size.NumberOfDimensions;
                }
                return MathInternal.cumprod(storage.AsRetArray(), dim);
            }
        }
       
        internal static unsafe Array<complex> cumprod<IndT>(BaseArray<complex> A, IndT? axis = null)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            if (!axis.HasValue) {
                // cumprod over flattened array
                return MathInternal.cumprod((A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>).Reshape(-1, StorageOrders.RowMajor));
            } else {
                int dim = axis.GetValueOrDefault().ToInt32(null);
                using var _1 = ReaderLock.Create(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>, out var storage); 
                if (dim < -storage.Size.NumberOfDimensions || dim >= storage.Size.NumberOfDimensions) {
                    throw new ArgumentException($"Invalid dimension index provided: {dim}. The 'axes' argument must lay in the range -{storage.Size.NumberOfDimensions}...<{storage.Size.NumberOfDimensions}.");
                }
                if (dim < 0) {
                    dim += (int)storage.Size.NumberOfDimensions;
                }
                return MathInternal.cumprod(storage.AsRetArray(), dim);
            }
        }
       
        internal static unsafe Array<float> cumprod<IndT>(BaseArray<float> A, IndT? axis = null)
            where IndT : struct, IConvertible {
            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            if (!axis.HasValue) {
                // cumprod over flattened array
                return MathInternal.cumprod((A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>).Reshape(-1, StorageOrders.RowMajor));
            } else {
                int dim = axis.GetValueOrDefault().ToInt32(null);
                using var _1 = ReaderLock.Create(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, out var storage); 
                if (dim < -storage.Size.NumberOfDimensions || dim >= storage.Size.NumberOfDimensions) {
                    throw new ArgumentException($"Invalid dimension index provided: {dim}. The 'axes' argument must lay in the range -{storage.Size.NumberOfDimensions}...<{storage.Size.NumberOfDimensions}.");
                }
                if (dim < 0) {
                    dim += (int)storage.Size.NumberOfDimensions;
                }
                return MathInternal.cumprod(storage.AsRetArray(), dim);
            }
        }

#endregion HYCALPER AUTO GENERATED CODE

    }
}
