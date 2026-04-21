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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using ILNumerics.Core.StorageLayer;

namespace ILNumerics {
    public static partial class Globals {

        /// <summary>
        /// Creates a 1D vector by using the pre-allocated memory referenced by <paramref name="pointer"/>.
        /// </summary>
        /// <typeparam name="T">The element type.</typeparam>
        /// <param name="pointer">Pointer to the memory region to be used as storage for the new 1d ILNumerics <see cref="Array{T}"/>. The memory region 
        /// must be large enough to fit at least as many elements of type <typeparamref name="T"/> into, as is specified by <paramref name="nrElements"/>.</param>
        /// <param name="nrElements">The number of elements for the new vector.</param>
        /// <returns>Vector of given <paramref name="nrElements"/> length using the memory region according to <paramref name="pointer"/> as storage.</returns>
        /// <remarks>As the name suggests the pointer is 'borrowed'! The array returned may be used, without restriction, for creating 
        /// subarrays, as argument in function invocations, as right side in assignments and so on. However, once all arrays referencing the internal storage 
        /// based on <paramref name="pointer"/> are released and the memory is no longer needed, it will not be deallocated or otherwise freed! Nor will the memory 
        /// be hold on by means of any ILNumerics memory pool! Instead, the memory ownership remains with you - the caller of this method. 
        /// <para>In order to determine, whether or not all references to the memory have been released use <see cref="BaseArray.IsDisposed"/>.</para>
        /// </remarks>
        internal static Array<T> BorrowPointer<T>(IntPtr pointer, long nrElements) {
            if (nrElements < 0) {
                throw new ArgumentException($"The length for the new vector must be positive. Found: {nrElements}.");
            }
            var ret = Storage<T>.Create();
            ret.Handles[0] = new ILNumerics.Core.MemoryLayer.NativeHostHandle() { Pointer = pointer, Length = new UIntPtr((ulong)(nrElements * Storage<T>.SizeOfT)) }; 
            ret.S.SetAll(nrElements);
            return ret.RetArray;
        }


    }
}
