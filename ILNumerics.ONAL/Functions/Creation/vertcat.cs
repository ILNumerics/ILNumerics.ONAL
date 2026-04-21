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
using System.Security;
using ILNumerics;
using ILNumerics.Core.Arrays;
using ILNumerics.Core.Global;
using ILNumerics.Core.StorageLayer;
using static ILNumerics.Globals;

namespace ILNumerics.Core.Functions.Builtin {

    internal static partial class MathInternal {

        /// <summary>
        /// Create new array with the columns of <paramref name="A"/> extended by the columns of <paramref name="B"/>.
        /// </summary>
        /// <param name="A">The first array.</param>
        /// <param name="B">The second array.</param>
        /// <returns>New array with elements of <paramref name="A"/> and <paramref name="B"/>.</returns>
        /// <remarks><para>The size of both arrays must match. This means that all but the first dimension (#0) must have the same lengths.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"> if <paramref name="A"/> or <paramref name="B"/> are null.</exception>
        /// <exception cref="ArgumentException"> if the sizes of <paramref name="A"/> and <paramref name="B"/> do not match.</exception>
        /// <seealso cref="concat{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, uint)"/>
        /// <seealso cref="horzcat{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT})"/>
        
        internal static RetT vertcat<T, LocalT, InT, OutT, RetT, StorageT>(
            ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A, 
            ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> B)
                where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
                where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
                where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            using var _1 = ReaderLock.Create(A, out var aStorage, throwOnNullWithMsg: "Array A in 'vertcat(A,B)' cannot be null.");
            using var _2 = ReaderLock.Create(B, out var bStorage, throwOnNullWithMsg: "Array B in 'vertcat(A,B)' cannot be null.");

            return aStorage.Concat(bStorage, 0).RetArray;
        }

        /// <summary>
        /// Create new array with the columns of <paramref name="A"/> extended by the columns of <paramref name="B"/> and <paramref name="C"/>.
        /// </summary>
        /// <param name="A">The first array.</param>
        /// <param name="B">The second array.</param>
        /// <param name="C">The third array.</param>
        /// <returns>New array with elements of <paramref name="A"/> and <paramref name="B"/> and <paramref name="C"/>.</returns>
        /// <remarks><para>The size of all arrays must match. This means that all but the first dimension (#0) must have the same lengths.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"> if <paramref name="A"/>, <paramref name="B"/> or <paramref name="C"/> are null.</exception>
        /// <exception cref="ArgumentException"> if the sizes of <paramref name="A"/>, <paramref name="B"/> and/or <paramref name="C"/> do not match.</exception>
        /// <seealso cref="concat{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, uint)"/>
        /// <seealso cref="horzcat{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT})"/>
        
        internal static RetT vertcat<T, LocalT, InT, OutT, RetT, StorageT>(
            ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A,
            ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> B,
            ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> C)
                where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
                where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
                where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            return concat(A, concat(B, C, 0), 0);  // concat performs memory management
        }
        /// <summary>
        /// Create new array with the columns of <paramref name="A"/> extended by the columns of <paramref name="B"/> and <paramref name="C"/> and <paramref name="D"/>.
        /// </summary>
        /// <param name="A">The first array.</param>
        /// <param name="B">The second array.</param>
        /// <param name="C">The third array.</param>
        /// <param name="D">The fourth array.</param>
        /// <returns>New array with elements of <paramref name="A"/> and <paramref name="B"/> and <paramref name="C"/>.</returns>
        /// <remarks><para>The size of all arrays must match. This means that all but the first dimension (#0) must have the same lengths.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"> if <paramref name="A"/>, <paramref name="B"/> or <paramref name="C"/> are null.</exception>
        /// <exception cref="ArgumentException"> if the sizes of <paramref name="A"/>, <paramref name="B"/> and/or <paramref name="C"/> do not match.</exception>
        /// <seealso cref="concat{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, uint)"/>
        /// <seealso cref="horzcat{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT})"/>
        
        internal static RetT vertcat<T, LocalT, InT, OutT, RetT, StorageT>(
            ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A,
            ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> B,
            ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> C,
            ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> D)
                where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
                where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
                where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            return concat(A, concat(B, concat(C, D, 0), 0), 0);  // concat performs memory management
        }

    }
}
