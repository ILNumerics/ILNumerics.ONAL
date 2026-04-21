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
using System.Text;
using ILNumerics;

namespace ILNumerics.Core.Functions.Builtin {
    internal static partial class MathInternal
    {
        /// <summary>
        /// Tests if <paramref name="A"/> has no elements.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <returns>True if <paramref name="A"/> has no elements, false otherwise.</returns>
        /// <remarks>
        /// <para>This function is an alias for <see cref="BaseArray.IsEmpty"/>.</para>
        /// </remarks>
        /// <seealso cref="ILNumerics.BaseArray.IsEmpty"/>
        /// <seealso cref="Size.NumberOfElements"/>
        /// <seealso cref="isnullorempty(BaseArray)"/>
        internal static bool isempty(BaseArray A) {
            return A.IsEmpty;
        }

    }
}
