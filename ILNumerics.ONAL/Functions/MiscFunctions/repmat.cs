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
        /// Replicates <paramref name="A"/> along the given dimensions.
        /// </summary>
        /// <param name="rep">Array specifying the repetition factors along each dimension.</param>
        /// <param name="A">Array to be replicated.</param>
        /// <returns>New storage with elements of <paramref name="A"/> repeatedly copied as specified by <paramref name="rep"/>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="rep"/> is null.</exception>
        /// <exception cref="ArgumentNullException"> if <paramref name="A"/> or <paramref name="rep"/> are null.</exception>
        /// <seealso cref="concat{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, uint)"/>
        /// <seealso cref="BaseStorage{T, LocalT, InT, OutT, RetT, StorageT}.Concat(StorageT, uint)"/>
        /// <seealso cref="horzcat{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT})"/>
        /// <seealso cref="vertcat{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT})"/>
        internal static RetT repmat<T, LocalT, InT, OutT, RetT, StorageT>(
            ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A,
            InArray<long> rep)
                where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
                where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
                where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            return A.Repmat(rep);

        }
        /// <summary>
        /// Replicates <paramref name="A"/> along the given dimension.
        /// </summary>
        /// <param name="d0">Repetition factor along dimension #0.</param>
        /// <param name="A">Array to be replicated.</param>
        /// <returns>New array of the same type as <paramref name="A"/> with elements repeatedly copied as specified by <paramref name="d0"/>....</returns>
        /// <exception cref="ArgumentNullException"> if <paramref name="A"/> is null.</exception>
        /// <seealso cref="concat{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, uint)"/>
        /// <seealso cref="BaseStorage{T, LocalT, InT, OutT, RetT, StorageT}.Concat(StorageT, uint)"/>
        /// <seealso cref="horzcat{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT})"/>
        /// <seealso cref="vertcat{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT})"/>
        internal static RetT repmat<T, LocalT, InT, OutT, RetT, StorageT>(
            ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A,
            long d0)
                where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
                where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
                where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            return A.Repmat(size(d0));
        }
        /// <summary>
        /// Replicates <paramref name="A"/> along the given dimensions.
        /// </summary>
        /// <param name="d0">Repetition factor along dimension #0.</param>
        /// <param name="d1">Repetition factor along dimension #1.</param>
        /// <param name="A">Array to be replicated.</param>
        /// <returns>New array of the same type as <paramref name="A"/> with elements repeatedly copied as specified by <paramref name="d0"/>....</returns>
        /// <exception cref="ArgumentNullException"> if <paramref name="A"/> is null.</exception>
        /// <seealso cref="concat{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, uint)"/>
        /// <seealso cref="BaseStorage{T, LocalT, InT, OutT, RetT, StorageT}.Concat(StorageT, uint)"/>
        /// <seealso cref="horzcat{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT})"/>
        /// <seealso cref="vertcat{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT})"/>
        internal static RetT repmat<T, LocalT, InT, OutT, RetT, StorageT>(
            ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A,
            long d0, long d1)
                where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
                where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
                where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            return A.Repmat(size(d0, d1));
        }
        /// <summary>
        /// Replicates <paramref name="A"/> along the given dimensions.
        /// </summary>
        /// <param name="d0">Repetition factor along dimension #0.</param>
        /// <param name="d1">Repetition factor along dimension #1.</param>
        /// <param name="d2">Repetition factor along dimension #2.</param>
        /// <param name="A">Array to be replicated.</param>
        /// <returns>New array of the same type as <paramref name="A"/> with elements repeatedly copied as specified by <paramref name="d0"/>....</returns>
        /// <exception cref="ArgumentNullException"> if <paramref name="A"/> is null.</exception>
        /// <seealso cref="concat{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, uint)"/>
        /// <seealso cref="BaseStorage{T, LocalT, InT, OutT, RetT, StorageT}.Concat(StorageT, uint)"/>
        /// <seealso cref="horzcat{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT})"/>
        /// <seealso cref="vertcat{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT})"/>
        internal static RetT repmat<T, LocalT, InT, OutT, RetT, StorageT>(
            ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A,
            long d0, long d1, long d2)
                where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
                where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
                where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            return A.Repmat(size(d0, d1, d2));         
        }

        /// <summary>
        /// Replicates <paramref name="A"/> along the given dimensions.
        /// </summary>
        /// <param name="d0">Repetition factor along dimension #0.</param>
        /// <param name="d1">Repetition factor along dimension #1.</param>
        /// <param name="d2">Repetition factor along dimension #2.</param>
        /// <param name="d3">Repetition factor along dimension #3.</param>
        /// <param name="A">Array to be replicated.</param>
        /// <returns>New array of the same type as <paramref name="A"/> with elements repeatedly copied as specified by <paramref name="d0"/>....</returns>
        /// <exception cref="ArgumentNullException"> if <paramref name="A"/> is null.</exception>
        /// <seealso cref="concat{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, uint)"/>
        /// <seealso cref="BaseStorage{T, LocalT, InT, OutT, RetT, StorageT}.Concat(StorageT, uint)"/>
        /// <seealso cref="horzcat{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT})"/>
        /// <seealso cref="vertcat{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT})"/>
        internal static RetT repmat<T, LocalT, InT, OutT, RetT, StorageT>(
            ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A,
            long d0, long d1, long d2, long d3)
                where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
                where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
                where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            return A.Repmat(size(d0, d1, d2, d3));
        }
        /// <summary>
        /// Replicates <paramref name="A"/> along the given dimensions.
        /// </summary>
        /// <param name="d0">Repetition factor along dimension #0.</param>
        /// <param name="d1">Repetition factor along dimension #1.</param>
        /// <param name="d2">Repetition factor along dimension #2.</param>
        /// <param name="d3">Repetition factor along dimension #3.</param>
        /// <param name="d4">Repetition factor along dimension #4.</param>
        /// <param name="A">Array to be replicated.</param>
        /// <returns>New array of the same type as <paramref name="A"/> with elements repeatedly copied as specified by <paramref name="d0"/>....</returns>
        /// <exception cref="ArgumentNullException"> if <paramref name="A"/> is null.</exception>
        /// <seealso cref="concat{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, uint)"/>
        /// <seealso cref="BaseStorage{T, LocalT, InT, OutT, RetT, StorageT}.Concat(StorageT, uint)"/>
        /// <seealso cref="horzcat{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT})"/>
        /// <seealso cref="vertcat{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT})"/>
        internal static RetT repmat<T, LocalT, InT, OutT, RetT, StorageT>(
            ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A,
            long d0, long d1, long d2, long d3, long d4)
                where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
                where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
                where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            return A.Repmat(size(d0, d1, d2, d3, d4));
        }
        /// <summary>
        /// Replicates <paramref name="A"/> along the given dimensions.
        /// </summary>
        /// <param name="d0">Repetition factor along dimension #0.</param>
        /// <param name="d1">Repetition factor along dimension #1.</param>
        /// <param name="d2">Repetition factor along dimension #2.</param>
        /// <param name="d3">Repetition factor along dimension #3.</param>
        /// <param name="d4">Repetition factor along dimension #4.</param>
        /// <param name="d5">Repetition factor along dimension #5.</param>
        /// <param name="A">Array to be replicated.</param>
        /// <returns>New array of the same type as <paramref name="A"/> with elements repeatedly copied as specified by <paramref name="d0"/>....</returns>
        /// <exception cref="ArgumentNullException"> if <paramref name="A"/> is null.</exception>
        /// <seealso cref="concat{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, uint)"/>
        /// <seealso cref="BaseStorage{T, LocalT, InT, OutT, RetT, StorageT}.Concat(StorageT, uint)"/>
        /// <seealso cref="horzcat{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT})"/>
        /// <seealso cref="vertcat{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT})"/>
        internal static RetT repmat<T, LocalT, InT, OutT, RetT, StorageT>(
            ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A,
            long d0, long d1, long d2, long d3, long d4, long d5)
                where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
                where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
                where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            return A.Repmat(size(d0, d1, d2, d3, d4, d5));
        }
        /// <summary>
        /// Replicates <paramref name="A"/> along the given dimensions.
        /// </summary>
        /// <param name="d0">Repetition factor along dimension #0.</param>
        /// <param name="d1">Repetition factor along dimension #1.</param>
        /// <param name="d2">Repetition factor along dimension #2.</param>
        /// <param name="d3">Repetition factor along dimension #3.</param>
        /// <param name="d4">Repetition factor along dimension #4.</param>
        /// <param name="d5">Repetition factor along dimension #5.</param>
        /// <param name="d6">Repetition factor along dimension #6.</param>
        /// <param name="A">Array to be replicated.</param>
        /// <returns>New array of the same type as <paramref name="A"/> with elements repeatedly copied as specified by <paramref name="d0"/>....</returns>
        /// <exception cref="ArgumentNullException"> if <paramref name="A"/> is null.</exception>
        /// <seealso cref="concat{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, uint)"/>
        /// <seealso cref="BaseStorage{T, LocalT, InT, OutT, RetT, StorageT}.Concat(StorageT, uint)"/>
        /// <seealso cref="horzcat{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT})"/>
        /// <seealso cref="vertcat{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT})"/>
        internal static RetT repmat<T, LocalT, InT, OutT, RetT, StorageT>(
            ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A,
            long d0, long d1, long d2, long d3, long d4, long d5, long d6)
                where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
                where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
                where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            return A.Repmat(size(d0, d1, d2, d3, d4, d5, d6));
        }
    }
}
