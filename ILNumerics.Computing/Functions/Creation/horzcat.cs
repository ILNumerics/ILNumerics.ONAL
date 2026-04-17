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
        /// Create new array with the rows of <paramref name="A"/> extended by the rows of <paramref name="B"/>.
        /// </summary>
        /// <param name="A">The first array.</param>
        /// <param name="B">The second array.</param>
        /// <returns>New array with elements of <paramref name="A"/> and <paramref name="B"/>.</returns>
        /// <remarks><para>The size of both arrays must match. This means that all but the dimension #1 must have the same lengths.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"> if <paramref name="A"/> or <paramref name="B"/> are null.</exception>
        /// <exception cref="ArgumentException"> if the sizes of <paramref name="A"/> and <paramref name="B"/> do not match.</exception>
        
        internal static RetT horzcat<T, LocalT, InT, OutT, RetT, StorageT>(
            ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A,
            ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> B)
                where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
                where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
                where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            using var _1 = ReaderLock.Create(A, out var aStorage, throwOnNullWithMsg: "Array A in 'horzcat(A,B)' cannot be null.");
            using var _2 = ReaderLock.Create(B, out var bStorage, throwOnNullWithMsg: "Array B in 'horzcat(A,B)' cannot be null.");

            return aStorage.Concat(bStorage, 1).RetArray;
        }
        /// <summary>
        /// Create new array with the rows of <paramref name="A"/> extended by the rows of <paramref name="B"/> and <paramref name="C"/>.
        /// </summary>
        /// <param name="A">The first array.</param>
        /// <param name="B">The second array.</param>
        /// <param name="C">The third array.</param>
        /// <returns>New array with elements of <paramref name="A"/> and <paramref name="B"/> and <paramref name="C"/>.</returns>
        /// <remarks><para>The size of all arrays must match. This means that all but the dimension #1 must have the same lengths.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"> if any of <paramref name="A"/> or <paramref name="B"/>  or <paramref name="C"/> is null.</exception>
        /// <exception cref="ArgumentException"> if the sizes of <paramref name="A"/> and/or <paramref name="B"/> and/or <paramref name="C"/> do not match.</exception>
        
        internal static RetT horzcat<T, LocalT, InT, OutT, RetT, StorageT>(
            ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A,
            ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> B,
            ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> C)
                where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
                where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
                where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            return concat(A, concat(B, C, 1), 1);  // concat performs memory management
        }
        /// <summary>
        /// Create new array with the rows of <paramref name="A"/> extended by the rows of <paramref name="B"/> and <paramref name="C"/> and <paramref name="D"/>.
        /// </summary>
        /// <param name="A">The first array.</param>
        /// <param name="B">The second array.</param>
        /// <param name="C">The third array.</param>
        /// <param name="D">The fourth array.</param>
        /// <returns>New array with elements of <paramref name="A"/> and <paramref name="B"/> and <paramref name="C"/> and <paramref name="D"/>.</returns>
        /// <remarks><para>The size of all arrays must match. This means that all but the dimension #1 must have the same lengths.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"> if any of <paramref name="A"/> or <paramref name="B"/>  or <paramref name="C"/> or <paramref name="D"/> is null.</exception>
        /// <exception cref="ArgumentException"> if the sizes of <paramref name="A"/> and/or <paramref name="B"/> and/or <paramref name="C"/> and/or <paramref name="D"/> do not match.</exception>
        
        internal static RetT horzcat<T, LocalT, InT, OutT, RetT, StorageT>(
            ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A,
            ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> B,
            ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> C,
            ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> D)
                where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
                where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
                where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            return concat(A, concat(B, concat(C, D, 1), 1), 1);  // concat performs memory management
        }
    }
}
