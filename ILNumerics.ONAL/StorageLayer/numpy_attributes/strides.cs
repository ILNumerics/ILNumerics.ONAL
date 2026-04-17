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

    public abstract partial class BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> : IDisposable, IStorage   // T1 must not read "T" to prevent from conflicts with the .T (transpose) property.

        where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {


        
        internal Storage<long> strides_get() {

            var ret = Storage<long>.Create();
            var ndims = Size.NumberOfDimensions;
            ret.Handles[0] = ret.New(Math.Max(ndims,1));
            ret.Size.SetAll(ndims);
            unsafe {
                var p = (long*)ret.Handles[0].Pointer;
                var bsd = Size.GetBSD(false) + 3 + Size.NumberOfDimensions;
                for (int i = 0; i < ndims; i++) {
                    p[i] = bsd[i];
                }
            }
            return ret;

        }

    }
}
