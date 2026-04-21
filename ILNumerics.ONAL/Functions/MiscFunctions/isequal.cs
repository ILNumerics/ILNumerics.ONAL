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
        /// Compares the number, shape, and type of elements of <paramref name="A"/> and <paramref name="B"/> and returns true if both are the same. 
        /// </summary>
        /// <param name="A">First input array.</param>
        /// <param name="B">Second input array.</param>
        /// <returns>True, if all elements and sizes of both arrays match; false otherwise.</returns>
        /// <remarks><para>Binary comparison is used on the elements. For floating point elements slight round-off 
        /// errors will cause the comparison to fail! Use <![CDATA['allall(abs(A - B) &lt; tol)']]> or similar for floating point 
        /// arrays instead. The same is recommended when performance is a critical factor: <see cref="isequal(BaseArray, BaseArray)"/> 
        /// (just like the underlying function <see cref="BaseArray.Equals(object)"/>) does not parallelize automatically and is recommended 
        /// for simple, non-performance critical comparisons only.</para>
        /// <para>If both arrays differ only in singleton dimensions but the types and (binary) values of all corresponding 
        /// elements are equal the comparison can still succeed. Consider using <see cref="Size.IsSameShape(Size)"/> additionally 
        /// for a stricter comparisons including singleton dimensions.</para>
        /// <para>This function is an alias for <see cref="BaseArray.Equals(object)"/> which overrides <see cref="object.Equals(object)"/>. 
        /// In difference to latter the function does not perform reference comparison but the type and value of the elements are 
        /// compared too.</para>
        /// </remarks>
        /// <seealso cref="BaseArray.Equals(object)"/>
        /// <seealso cref="eq(BaseArray{double}, BaseArray{double})"/>
        /// <seealso cref="allall(BaseArray{bool})"/>
        /// <seealso cref="Size.IsSameShape(Size)"/>
        /// <seealso cref="Size.IsSameSize(Size)"/>
        internal static bool isequal(BaseArray A, BaseArray B) {
            //using (Scope.Enter(A,B)) 
                return A.Equals(B); 
        }

    }
}
