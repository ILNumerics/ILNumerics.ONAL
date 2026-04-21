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
