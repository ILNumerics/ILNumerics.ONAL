using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics {

    /// <summary>
    /// Interface for objects usable as index iterators in subarray expressions.
    /// </summary>
    /// <remarks>Index iterators provide a safe and (reasonable) fast way of iterating the indices addressed by 
    /// subarray expressions for each dimension. All checks are performed at the creation time of the iterator. 
    /// This includes the checks for all indices being inside the valid range of an array.</remarks>
    public interface IIndexIterator : IEnumerable<long>, IEnumerator<long> {
        /// <summary>
        ///  Gives the number of elements addressed by this object.
        /// </summary>
        /// <returns>Number of elements.</returns>
        long GetLength();
        /// <summary>
        /// Gives the maximum index addressed by this iterator, if such value exists.
        /// </summary>
        /// <returns></returns>
        long? GetMaximum();
        /// <summary>
        /// Gives the minimum index addressed by this iterator, if such value exists.
        /// </summary>
        /// <returns></returns>
        long? GetMinimum();
        /// <summary>
        /// Gets the index of the last element of the dimension this iterator is working along. 
        /// This must be provided by such iterators, capable of storing negative indices only.
        /// </summary>
        /// <returns></returns>
        /// <remarks>This value is specified at the time the iterator was initialized and used 
        /// for evaluating 'end' placeholders in some situations. For numpy array style this 
        /// equals the length of the corresponding dimension minus 1. For <see cref="ArrayStyles.ILNumericsV4"/>
        /// this corresponds to the last element index in this <b>and trailing, merged</b> dimensions.</remarks>
        long GetLastDimensionIndex();

        /// <summary>
        /// Gives the step size of the range, in case that this iterator object represents a simple (un-/stepped) range. Otherwise: null. 
        /// </summary>
        long? GetStepSize();
    }
}
