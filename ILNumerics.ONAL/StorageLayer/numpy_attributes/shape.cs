using ILNumerics.Core.Arrays;
using ILNumerics.Core.DeviceManagement;
using ILNumerics.Core.Functions.Builtin;
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.StorageLayer {

    public abstract partial class BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> : IDisposable, IStorage  
        where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

        
        internal void shape_set(Storage<long> storage) {

            if (storage.Size.NumberOfElements > Size.MaxNumberOfDimensions) {
                throw new ArgumentException($"shape_set received {storage.Size.NumberOfElements} elements for the new dimensions. The number of dimensions is currently limited to {Size.MaxNumberOfDimensions} ({nameof(ILNumerics.Size)}.{nameof(Size.MaxNumberOfDimensions)}).");
            }
            long elemCount = 1;
            if (storage.Size.NumberOfElements > 0) {
                long min, max;
                if (!storage.GetLimits(out min, out max) || min < 0) {
                    throw new ArgumentException($"Dimension lengths must be positive!");
                }
                for (int i = 0; i < storage.Size.NumberOfElements; i++) {
                    elemCount *= storage.GetValue(i);
                }
            }
            // don't leave A crippled in case of errors!
            if (elemCount != Size.NumberOfElements) {
                throw new ArgumentException($"The number of elements in A must not change when assigning to A.shape!");
            }
            unsafe {
                long stride = 1;
                long* bsd; 
                var otherNDims = Math.Max(storage.Size.NumberOfElements, Settings.MinNumberOfArrayDimensions);
                switch (S.StorageOrder) {
                    case StorageOrders.ColumnMajor:

                        bsd = Size.GetBSD(true);
                        for (int i = 0; i < otherNDims; i++) {
                            var s = i < storage.Size.NumberOfElements ? storage.GetValue(i) : 1;
                            bsd[3 + i] = s;
                            bsd[3 + otherNDims + i] = stride;
                            stride *= s;
                        }

                        break;
                    case StorageOrders.RowMajor:

                        bsd = Size.GetBSD(true);
                        for (int i = 0; i < otherNDims; i++) {
                            var s = i < storage.Size.NumberOfElements ? storage.GetValue(otherNDims - 1 - i) : 1;
                            bsd[2 + otherNDims - i] = s;
                            bsd[2 + 2 * otherNDims - i] = stride;
                            stride *= s;
                        }

                        break;
                    default:
                        throw new InvalidOperationException($"Assigning to the shape property of an array A is only supported for continous storages. Use Math.reshape(A,..) or A.Reshape() instead!");
                }
                bsd[0] = otherNDims;
            }
        }

        
        internal Storage<long> shape_get() {

            var ret = Storage<long>.Create();
            var ndims = Size.NumberOfDimensions;
            ret.Handles[0] = ret.New(Math.Max(ndims,1));
            ret.Size.SetAll(ndims);
            unsafe {
                var p = (long*)ret.Handles[0].Pointer;
                var bsd = Size.GetBSD(false) + 3;
                for (int i = 0; i < ndims; i++) {
                    p[i] = bsd[i];
                }
            }
            return ret;

        }

    }
}
