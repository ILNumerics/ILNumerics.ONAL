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
using ILNumerics.Core.Arrays;
using ILNumerics.Core.Global;
using ILNumerics.Core.StorageLayer;
using System;
using System.Security;
using System.Threading;

namespace ILNumerics.Core.Functions.Builtin {


    internal static partial class MathInternal {

        /// <summary>
        /// Creates a logical array with 'true' values at the positions with non-0 elements of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <returns>New array.</returns>
        internal static Logical tological(BaseArray A) {
            if (object.Equals(A, null) || A is Logical) {
                return A?.ToLogical(); // safe the null check 
            } else if (A is BaseArray<float>) {
                return neq(A as BaseArray<float>, 0);
            } else if (A is BaseArray<long>) {
                return neq(A as BaseArray<long>, 0);
            } else if (A is BaseArray<ulong>) {
                return neq(A as BaseArray<ulong>, 0);
            } else if (A is BaseArray<int>) {
                return neq(A as BaseArray<int>, 0);
            } else if (A is BaseArray<uint>) {
                return neq(A as BaseArray<uint>, 0);
            } else if (A is BaseArray<short>) {
                return neq(A as BaseArray<short>, 0);
            } else if (A is BaseArray<ushort>) {
                return neq(A as BaseArray<ushort>, 0);
            } else if (A is BaseArray<sbyte>) {
                return neq(A as BaseArray<sbyte>, 0);
            } else if (A is BaseArray<double>) {
                return neq(A as BaseArray<double>, 0);
            } else if (A is BaseArray<complex>) {
                return neq(A as BaseArray<complex>, complex.Zero); 
            } else if (A is BaseArray<fcomplex>) {
                return neq(A as BaseArray<fcomplex>, fcomplex.Zero);
            } else if (A is BaseArray<bool>) {
                return neq(A as BaseArray<bool>, true);
            } else {
                throw new InvalidCastException($"Unable to convert BaseArray of type {A.GetType().Name} to Array<byte>."); 
            }
        }

    }

}
