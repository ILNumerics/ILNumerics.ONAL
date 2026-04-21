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
using ILNumerics.Core.Functions.Builtin;
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILNumerics {
    public abstract partial class BaseArray {

        /// <summary>
        /// Retrieve the value of the element addressed by the sequential index in <paramref name="i"/>.
        /// </summary>
        /// <param name="i">Sequential index (1 dim), taking the striding (<see cref="BaseArray.Size"/>) of 
        /// this array into account.</param>
        /// <returns>Boxed copy of the element at position <paramref name="i"/> if the elements stored in this 
        /// array are structs. Otherwise a reference copy of the element at <paramref name="i"/> is returned.</returns>
        /// <remarks>This function provides a way to access element values of this untyped base array for 
        /// convenience reasons. It is needed in very rare cases only. Don't expect great performance 
        /// from using untyped functions like this! The recommended performant way is to use the strongly typed classes 
        /// (<see cref="Array{T}"/>, <see cref="Logical"/>, <see cref="Cell"/>) and the corresponding typed 
        /// API functions only.</remarks>
        /// <seealso cref="Array{T}"/>
        /// <seealso cref="ExtensionMethods.GetValue(BaseArray{double}, uint)"/>
        public abstract object GetItem(long i);
        /// <summary>
        /// Gets the storage object for this array. This method supports cell objects internally. 
        /// </summary>
        /// <param name="forceRelease">[Optional] Releases this base array if it is a return type array. Default: false. (Required for some places (SetValueSeq), potentially not the 'optimal' solution, though)</param>
        /// <returns>(untyped) detached clone of this base storage object for storing into cell elements.</returns>
        internal abstract IStorage GetClonedStorage(bool forceRelease = false);

        /// <summary>
        /// Gets a clone of this array. Reference count: 1 or 2 (for pending arrays).
        /// </summary>
        /// <returns>Untyped, detached, retained array carrying the data of this array.</returns>
        /// <remarks>This function is capable of handling pending arrays.</remarks>
        internal abstract BaseArray GetClonedArray();

        ///// <summary>
        ///// Flag indicating if this array instance is a volatile array. It returns true for RetXXX array types, _without_ releasing the array. 
        ///// </summary>
        //internal abstract bool IsVolatile { get; }

    }
}

