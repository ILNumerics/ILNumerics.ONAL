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
using ILNumerics.Core.Arrays;
using ILNumerics.Core.StorageLayer;

namespace ILNumerics {

    /// <summary>
    /// This class implements extension methods on the main array classes.
    /// </summary>
    public static partial class ExtensionMethods {

        /// <summary>
        /// Return <paramref name="A"/> as array of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="A">Input array of storage type <see cref="Storage{T}"/>.</param>
        /// <returns>If <paramref name="A"/> stores elements of type <typeparamref name="T"/> the RetT of A's storage. Otherwise returns null.</returns>
        /// <remarks><para>This function is convenient when the element type of <paramref name="A"/> is known but the lifetime type 
        /// (local array, input array, return array, etc.) is not known. Instead, the common base type <see cref="BaseArray"/> is 
        /// used to reference the array and this function returns its corresponding return type array.</para>
        /// <para>Note, that commonly, arrays in ILNumerics are not handled via <see cref="BaseArray"/> but as concrete array types. 
        /// See the general function rules to learn recommended ways of working with arrays in ILNumerics.</para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/GeneralRules.html"/>
        /// <seealso cref="ILNumerics.Core.Functions.Builtin.MathInternal.convert{inT, outT}(BaseArray{inT})"/>
        /// <seealso cref="ToCell(BaseArray)"/>
        /// <seealso cref="ToLogical(BaseArray)"/>
        internal static Array<T> ToArray<T>(this BaseArray A) {
            if (object.Equals(A, null) || A is Array<T>) {
                return A as Array<T>;
            } else if (A is ConcreteArray<T, Array<T>, InArray<T>, OutArray<T>, Array<T>, Storage<T>> arrayT) {
                return arrayT.C;
            } else {
                throw new System.ArgumentException($"This base array API supports being called on members of the Array<T> type family only. Use ToCell() and ToLogical() for other array types! Incoming array type: {A.GetType().FullName}.");  
            }
        }

        /// <summary>
        /// Returns the ret cell for the storage of <paramref name="A"/> or null.
        /// </summary>
        /// <param name="A">Input cell array.</param>
        /// <returns>If <paramref name="A"/> stores elements of type <see cref="BaseArray"/> return a <see cref="Cell"/> of <paramref name="A"/>'s storage. Otherwise returns null.</returns>
        /// <remarks><para>This function is convenient when the element type of <paramref name="A"/> is known but the lifetime type 
        /// (local array, input array, return array, etc.) is not known. Instead, the common base type <see cref="BaseArray"/> is 
        /// used to reference the array and this function returns its corresponding return type array.</para>
        /// <para>Note, that commonly, arrays in ILNumerics are not handled via <see cref="BaseArray"/> but as concrete array types. 
        /// See the general function rules to learn recommended ways of working with arrays in ILNumerics.</para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/GeneralRules.html"/>
        /// <seealso cref="Core.Functions.Builtin.MathInternal.convert{inT, outT}(BaseArray{inT})"/>
        /// <seealso cref="ToArray{T}(BaseArray)"/>
        /// <seealso cref="ToLogical(BaseArray)"/>
        internal static Cell ToCell(this BaseArray A) {
            if (object.Equals(A, null) || A is Cell) {
                return A as Cell;
            } else {
                var storage = (A as ConcreteArray<BaseArray, Cell, InCell, OutCell, Cell, CellStorage>)?.Storage;
                return storage?.RetArray;
            }
        }

        /// <summary>
        /// Returns the return type logical for the storage of <paramref name="A"/> or null.
        /// </summary>
        /// <param name="A">Input logical array.</param>
        /// <returns>If <paramref name="A"/> stores elements of type <see cref="bool"/> the <see cref="Logical"/> of <paramref name="A"/>'s storage. Otherwise returns null.</returns>
        /// <remarks><para>This function is convenient when the element type of <paramref name="A"/> is known but the lifetime type 
        /// (local array, input array, return array, etc.) is not known. Instead, the common base type <see cref="BaseArray"/> is 
        /// used to reference the array and this function returns its corresponding return type array.</para>
        /// <para>Note, that commonly, arrays in ILNumerics are not handled via <see cref="BaseArray"/> but as concrete array types. 
        /// See the general function rules to learn recommended ways of working with arrays in ILNumerics.</para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/GeneralRules.html"/>
        /// <seealso cref="ILMath.convert{inT, outT}(BaseArray{inT})"/>
        /// <seealso cref="ToArray{T}(BaseArray)"/>
        /// <seealso cref="ToCell(BaseArray)"/>
        internal static Logical ToLogical(this BaseArray A) {
            if (object.Equals(A, null) || A is Logical) {
                return A as Logical;
            } else {
                var storage = (A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>)?.Storage;
                return storage?.RetArray;
            }
        }

    }
}
