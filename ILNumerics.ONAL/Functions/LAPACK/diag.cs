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
using ILNumerics;
using ILNumerics.Core.Arrays;
using ILNumerics.Core.Global;
using ILNumerics.Core.Internal;
using ILNumerics.Core.StorageLayer;
using static ILNumerics.Globals; 

/*!HC:TYPELIST:
<hycalper>
    <type>
        <source locate="after">
            inArr1
        </source>
    <destination>float</destination>
    <destination>Int32</destination>
    <destination>Int64</destination>
    <destination>byte</destination>
    <destination>complex</destination>
    <destination>fcomplex</destination>
    </type>
 </hycalper>
 */

namespace ILNumerics {
    public static partial class ILMath {

        /// <summary>
        /// Diagonal matrix or diagonal from matrix.
        /// </summary>
        /// <param name="A">Input matrix or vector.</param>
        /// <param name="d">[Optional] Index of diagonal to extract/ to create. Default: 0.</param>
        /// <returns>Depending on the shape of <paramref name="A"/> a matrix or a vector with the elements on the 
        /// specified diagonal of <paramref name="A"/>.</returns>
        /// <remarks>The type of the Array returned will be the same as the type of <paramref name="A"/>.
        /// <para>If <paramref name="A"/> is a matrix, diag(<paramref name="A"/>) returns the 
        /// elements on the <paramref name="d"/>'s diagonal as column vector. </para>
        /// <para>If <paramref name="A"/> is vector, a square matrix of size 
        /// [<paramref name="A"/>.Length - abs(<paramref name="d"/>), <paramref name="A"/>.Length - abs(<paramref name="d"/>)] will be created, having 
        /// the elements of the <paramref name="d"/>'s diagonal of <paramref name="A"/>.</para>
        /// <para>0 for <paramref name="d"/> means: the main diagonal, <paramref name="d"/> > 0 is above the main diagonal, <paramref name="d"/> smaller 0 means 
        /// below the main diagonal. <paramref name="d"/> must lay in the range of existing rows / columns of <paramref name="A"/>.</para>
        /// <para>In general the equality diag(diag(A))==A holds true on a vector or a matrix A.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If abs(<paramref name="d"/>) addresses a non-existing diagonal.</exception>
        public static Array<T> diag<T>(BaseArray<T> A, int d = 0) {
            if (Equals(A, null)) {
                throw new ArgumentException("diag: input array must be a matrix, a vector or a scalar");
            }
            using var _1 = ReaderLock.Create(A as ConcreteArray<T, Array<T>, InArray<T>, OutArray<T>, Array<T>, Storage<T>>, out var storage);

            if (Equals(storage, null) || storage.Size.NumberOfDimensions > 2) {
                throw new ArgumentException($"diag(): A must be a matrix of numeric element type.");
            }
            if (storage.S.Longest < d) {
                throw new ArgumentException($"diag(): the 'd' parameter must address an existing diagonal. Found: {d}, A:[{storage.S.ToString()}]");
            }
            if (storage.S.NumberOfElements == 0)
                return empty<T>(storage.Size);
            if (storage.Size.NumberOfElements == 1) {
                return storage.RetArray;
            }
            using (Scope.Enter()) {
                Array<T> ret = null;
                if (storage.Size.NonSingletonDimensions == 1) {
                    // input is vector -> create a square matrix
                    long retLen = storage.S.Longest + Math.Abs(d);
                    ret = zeros<T>(retLen, retLen);
                    if (d > 0) {
                        for (int i = 0; i < storage.S.NumberOfElements; i++) {
                            ret.SetValue(storage.GetValue(i), i, i + d);
                        }
                    } else {
                        for (int i = 0; i < storage.S.NumberOfElements; i++) {
                            ret.SetValue(storage.GetValue(i), i - d, i);
                        }
                    }
                } else {
                    // A is matrix -> extract the d's diagonal.
                    if (d <= -storage.Size[0] || d >= storage.Size[1])
                        throw new ArgumentException($"diag(): the 'd' parameter must address an existing diagonal. Found: {d}, A:[{storage.S.ToString()}]");
                    if (d > 0) {
                        var retLen = Math.Min(storage.Size[0], storage.Size[1] - Math.Abs(d));
                        ret = zeros<T>(retLen, dim1: 1);
                        for (int i = 0; i < retLen; i++) {
                            ret[i] = storage.GetValueSeq(storage.Size.GetSeqIndex(i, d + i));
                        }
                    } else {
                        var retLen = Math.Min(storage.Size[1], storage.Size[0] - Math.Abs(d));
                        d = -d;
                        ret = zeros<T>(retLen, dim1: 1);
                        for (int i = 0; i < retLen; i++) {
                            ret[i] = storage.GetValueSeq(storage.Size.GetSeqIndex(d + i, i));
                        }
                    }
                }
                return ret;
            }
        }
    }
}
