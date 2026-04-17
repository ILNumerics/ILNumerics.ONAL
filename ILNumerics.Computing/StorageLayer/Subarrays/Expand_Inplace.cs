using ILNumerics.Core.Arrays;
using ILNumerics.Core.MemoryLayer;
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

        #region Expand interface 

        /// <summary>
        /// Creates a new storage similar to this storage with the given dimensions expanded to the specified lengths. 
        /// </summary>
        /// <param name="lengths">Vector with dimension lengths. Size: 0...[Size.MaxNumberOfDimensions].</param>
        
        internal unsafe void Expand(Span<long> lengths) {


            if (m_arrayCounter > 1 && IsReady) {
                throw new InvalidOperationException("This array can not be expanded currently because other arrays share the same storage. Check input-parameters of the current scope and make sure that the original array in the callers scope has not been used as input parameter AND output parameter at the same time. Consider providing a clone of the original array A when trying to mutate the array and when assigning to itself: A[1,full] = A.C");
            }
           
            if (lengths == null || lengths.Length == 0)
                return;
            if (lengths.Length > Size.MaxNumberOfDimensions) {
                throw new ArgumentException($"Number of parameters exceeds maximum number of dimensions ({Size.MaxNumberOfDimensions})."); 
            }
            if (lengths.Length < this.m_size.NumberOfDimensions && lengths[lengths.Length-1] > m_size[(uint)lengths.Length - 1]) {
                // Fewer dimension as existing in this array were specified and the last specified dimension is an expanding one. 
                // Ambiguities arise only when higher (unspecified) dimensions of this array do actually store more elements. 
                // Otherwise, the length of the last dimension cannot reach out into such an higher (merged-into-last-specified) 
                // dimension and it would be clear that the last dimension was meant to expand this array.
                for (uint t = (uint)lengths.Length; t < m_size.NumberOfDimensions; t++) {
                    if (m_size[t] > 1) {
                        throw new ArgumentException($"Unable to expand array {m_size.ToString()} along ambigous dimension #{lengths.Length - 1}! Consider specifying all dimensions when expansion is intended or to prevent from expansion by pre-creating a properly sized array.");
                    }
                }
            }
            int i = 0;
            long nelem = 1;
            int ndims = Math.Max(lengths.Length, (int)m_size.NumberOfDimensions); 
            for (; i < lengths.Length; i++) {
                lengths[i] = Math.Max(lengths[i], m_size[i]); 
                nelem *= lengths[i];
            }
            for (; i < m_size.NumberOfDimensions; i++) {
                nelem *= m_size[i];
            }

            var newCA = CountableArray.Create();
            newCA[0] = New((ulong)nelem, true);

            #region prepare new, expanded myBSD
            long* outBSD = stackalloc long[ndims * 2 + 3];
            outBSD[0] = ndims;
            outBSD[1] = nelem;
            outBSD[2] = 0;
            for (i = 0; i < lengths.Length; i++) {
                outBSD[3 + i] = lengths[i];
            }
            for (; i < ndims; i++) {
                outBSD[i + 3] = (m_size[i]);
            }
            long stride = 1;
            if (Settings.DefaultStorageOrder == StorageOrders.ColumnMajor) {
                for (i = 0; i < ndims; i++) {
                    outBSD[3 + ndims + i] = (outBSD[3 + i] == 1 ? 0 : stride);
                    stride *= outBSD[3 + i];
                }
            } else {
                System.Diagnostics.Debug.Assert(Settings.DefaultStorageOrder == StorageOrders.RowMajor);
                for (i = 0; i < ndims; i++) {
                    outBSD[2 + 2 * ndims - i] = (outBSD[2 + ndims - i] == 1 ? 0 : stride);
                    stride *= outBSD[2 + ndims - i];
                }
            }
            System.Diagnostics.Debug.Assert(stride == nelem);
            #endregion

            #region prepare BSD for WriteTo
            long* copyBSD = stackalloc long[(int)ndims * 2 + 3];
            var myBSD = m_size.GetBSD(false);

            copyBSD[0] = myBSD[0];
            copyBSD[1] = myBSD[1];
            copyBSD[2] = 0;
            uint myDims = (uint)myBSD[0];
            for (i = 0; i < myDims; i++) {
                copyBSD[3 + i] = myBSD[3 + i];
                copyBSD[3 + myDims + i] = outBSD[3 + ndims + i];
            }
            #endregion
            Core.Functions.Builtin.WriteToOperators.WriteTo_BSD<T>(m_handles[0], m_size, newCA[0], copyBSD, SizeOfT, inExpand: true);

            m_size.SetAll(outBSD);
            m_handles.Release();
            m_handles = newCA;
        }

        #endregion
    }
}
