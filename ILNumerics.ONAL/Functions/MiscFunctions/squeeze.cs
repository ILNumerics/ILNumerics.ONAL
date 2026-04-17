using ILNumerics.Core.Arrays;
using ILNumerics.Core.Global;
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.Functions.Builtin {
    internal static partial class MathInternal {

        /// <summary>
        /// Creates an array similar to this array, having singleton dimensions removed.
        /// </summary>
        /// <param name="A">Input array, will not be altered.</param>
        /// <returns>New array without singleton dimension.</returns>
        /// <remarks>This function (as all functions in ILNumerics) respects the setting of <see cref="Settings.MinNumberOfArrayDimensions"/>. 
        /// Thus, commonly, the array returned may still has up to two singleton dimensions, if <see cref="Settings.ArrayStyle"/> is <see cref="ArrayStyles.ILNumericsV4"/>.
        /// <para>Note that removing singleton dimensions does not change the number of elements of the array. Hence, no copy is made 
        /// for <see cref="squeeze{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT})"/>.</para></remarks>
        /// <seealso cref="reshape{T}(BaseArray{T}, long, long, long, long, StorageOrders?)"/>
        internal static RetT squeeze<T, LocalT, InT, OutT, RetT, StorageT>(ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A)
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            using var _1 = ReaderLock.Create(A, out var storage); 
 
            if (Equals(A, null)) {
                throw new ArgumentNullException(nameof(A));
            }
            StorageT ret = BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>.Create(storage.Handles, storage.Size);

            for (uint i = ret.Size.NumberOfDimensions; i-- > 0;) {
                if (ret.Size[i] == 1) {
                    ret.Size.RemoveDimension(i);
                }
            }
            return ret.RetArray;
        }
    }
}
