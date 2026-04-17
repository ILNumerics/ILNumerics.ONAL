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
        /// Create new array with the elements of <paramref name="A"/> and <paramref name="B"/> concatenated along dimension <paramref name="dim"/>.
        /// </summary>
        /// <param name="A">The first array.</param>
        /// <param name="B">The other array.</param>
        /// <param name="dim">Dimension index to align both arrays elements along.</param>
        /// <returns>New array with elements of <paramref name="A"/> and <paramref name="B"/>.</returns>
        /// <remarks><para>The size of both arrays must match. This means that all but the dimension #<paramref name="dim"/>
        /// must have the same lengths.</para></remarks>
        /// <exception cref="ArgumentException">If <paramref name="dim"/> is not within the range of dimensions of the arrays or if the sizes of <paramref name="A"/> and <paramref name="B"/> do not match.</exception>
        /// <exception cref="ArgumentNullException"> if <paramref name="A"/> or <paramref name="B"/> are null.</exception>
        /// <seealso cref="concat{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, uint)"/>
        /// <seealso cref="BaseStorage{T, LocalT, InT, OutT, RetT, StorageT}.Concat(StorageT, uint)"/>
        /// <seealso cref="horzcat{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT})"/>
        /// <seealso cref="vertcat{T, LocalT, InT, OutT, RetT, StorageT}(ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT}, ConcreteArray{T, LocalT, InT, OutT, RetT, StorageT})"/>
        
        internal static RetT concat<T, LocalT, InT, OutT, RetT, StorageT>(
            ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A, 
            ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> B, uint dim)
                where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
                where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
                where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            using var _1 = ReaderLock.Create(A, out var aStorage, throwOnNullWithMsg: "Array A in 'A.concat(B)' cannot be null.");
            using var _2 = ReaderLock.Create(B, out var bStorage, throwOnNullWithMsg: "Array B in 'A.concat(B)' cannot be null.");

            return aStorage.Concat(bStorage, dim).RetArray;

        }
    }
}
