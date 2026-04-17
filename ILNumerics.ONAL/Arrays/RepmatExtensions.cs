using ILNumerics.Core.Arrays;
using ILNumerics.Core.Functions.Builtin;
using ILNumerics.Core.Global;
using ILNumerics.Core.StorageLayer;
using System;
using System.Security;

namespace ILNumerics {

    public static partial class ExtensionMethods {

        #region generic Repmat extensions

        /// <summary>
        /// Creates new array with repeated copies of the content of the source array.
        /// </summary>
        /// <param name="A">The source array.</param>
        /// <param name="rep">Repetition factors for each dimensions <paramref name="A"/> is to be replicated along.</param>
        /// <returns>New array with repeated copies of the source array along the dimensions as specified by <paramref name="rep"/>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="rep"/> is null.</exception>
        
        public unsafe static RetT Repmat<T, LocalT, InT, OutT, RetT, StorageT>(
                        this ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A,
                        InArray<long> rep)
                where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
                where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
                where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            using var _1 = ReaderLock.Create(A, out var aStorage, throwOnNullWithMsg: "In an expression A.Repmat(rep) A cannot be null.");
            using var _2 = ReaderLock.Create(rep, out var repStorage, throwOnNullWithMsg: "In an expression A.Repmat(rep) rep cannot be null.");
            return aStorage.Repmat(repStorage).RetArray;

        }
        /// <summary>
        /// Creates new array with repeated copies of the content of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">The source array.</param>
        /// <param name="d0">Repetition factor for the first dimension of <paramref name="A"/></param>
        /// <returns>New array with repeated copies of the source array along the dimensions as specified by <paramref name="d0"/>.</returns>
        
        public unsafe static RetT Repmat<T, LocalT, InT, OutT, RetT, StorageT>(
                        this ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A,
                        long d0)
                where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
                where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
                where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            return A.Repmat(MathInternal.size(d0)); 

        }
        /// <summary>
        /// Creates new array with repeated copies of the content of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">The source array.</param>
        /// <param name="d0">Repetition factor for the first dimension of <paramref name="A"/>.</param>
        /// <param name="d1">Repetition factor for dimension #1 of <paramref name="A"/>.</param>
        /// <returns>New array with repeated copies of the source array along the dimensions as specified by <paramref name="d0"/>.</returns>
        
        public unsafe static RetT Repmat<T, LocalT, InT, OutT, RetT, StorageT>(
                        this ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A,
                        long d0, long d1)
                where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
                where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
                where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            return A.Repmat(MathInternal.size(d0,d1));
        }
        /// <summary>
        /// Creates new array with repeated copies of the content of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">The source array.</param>
        /// <param name="d0">Repetition factor for the first dimension of <paramref name="A"/>.</param>
        /// <param name="d1">Repetition factor for dimension #1 of <paramref name="A"/>.</param>
        /// <param name="d2">Repetition factor for dimension #2 of <paramref name="A"/>.</param>
        /// <returns>New array with repeated copies of the source array along the dimensions as specified by <paramref name="d0"/>....</returns>
        
        public unsafe static RetT Repmat<T, LocalT, InT, OutT, RetT, StorageT>(
                        this ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A,
                        long d0, long d1, long d2)
                where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
                where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
                where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {
            
            return A.Repmat(MathInternal.size(d0, d1, d2));

        }
        /// <summary>
        /// Creates new array with repeated copies of the content of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">The source array.</param>
        /// <param name="d0">Repetition factor for the first dimension of <paramref name="A"/>.</param>
        /// <param name="d1">Repetition factor for dimension #1 of <paramref name="A"/>.</param>
        /// <param name="d2">Repetition factor for dimension #2 of <paramref name="A"/>.</param>
        /// <param name="d3">Repetition factor for dimension #3 of <paramref name="A"/>.</param>
        /// <returns>New array with repeated copies of the source array along the dimensions as specified by <paramref name="d0"/>....</returns>
        
        public unsafe static RetT Repmat<T, LocalT, InT, OutT, RetT, StorageT>(
                        this ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A,
                        long d0, long d1, long d2, long d3)
                where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
                where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
                where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            return A.Repmat(MathInternal.size(d0, d1, d2, d3));
        }
        /// <summary>
        /// Creates new array with repeated copies of the content of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">The source array.</param>
        /// <param name="d0">Repetition factor for the first dimension of <paramref name="A"/>.</param>
        /// <param name="d1">Repetition factor for dimension #1 of <paramref name="A"/>.</param>
        /// <param name="d2">Repetition factor for dimension #2 of <paramref name="A"/>.</param>
        /// <param name="d3">Repetition factor for dimension #3 of <paramref name="A"/>.</param>
        /// <param name="d4">Repetition factor for dimension #4 of <paramref name="A"/>.</param>
        /// <returns>New array with repeated copies of the source array along the dimensions as specified by <paramref name="d0"/>....</returns>
        
        public unsafe static RetT Repmat<T, LocalT, InT, OutT, RetT, StorageT>(
                        this ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A,
                        long d0, long d1, long d2, long d3, long d4)
                where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
                where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
                where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            return A.Repmat(MathInternal.size(d0, d1, d2, d3, d4));
        }
        /// <summary>
        /// Creates new array with repeated copies of the content of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">The source array.</param>
        /// <param name="d0">Repetition factor for the first dimension of <paramref name="A"/>.</param>
        /// <param name="d1">Repetition factor for dimension #1 of <paramref name="A"/>.</param>
        /// <param name="d2">Repetition factor for dimension #2 of <paramref name="A"/>.</param>
        /// <param name="d3">Repetition factor for dimension #3 of <paramref name="A"/>.</param>
        /// <param name="d4">Repetition factor for dimension #4 of <paramref name="A"/>.</param>
        /// <param name="d5">Repetition factor for dimension #5 of <paramref name="A"/>.</param>
        /// <returns>New array with repeated copies of the source array along the dimensions as specified by <paramref name="d0"/>....</returns>
        
        public unsafe static RetT Repmat<T, LocalT, InT, OutT, RetT, StorageT>(
                        this ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A,
                        long d0, long d1, long d2, long d3, long d4, long d5)
                where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
                where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
                where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            return A.Repmat(MathInternal.size(d0, d1, d2, d3, d4, d5));
        }
        /// <summary>
        /// Creates new array with repeated copies of the content of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">The source array.</param>
        /// <param name="d0">Repetition factor for the first dimension of <paramref name="A"/>.</param>
        /// <param name="d1">Repetition factor for dimension #1 of <paramref name="A"/>.</param>
        /// <param name="d2">Repetition factor for dimension #2 of <paramref name="A"/>.</param>
        /// <param name="d3">Repetition factor for dimension #3 of <paramref name="A"/>.</param>
        /// <param name="d4">Repetition factor for dimension #4 of <paramref name="A"/>.</param>
        /// <param name="d5">Repetition factor for dimension #5 of <paramref name="A"/>.</param>
        /// <param name="d6">Repetition factor for dimension #6 of <paramref name="A"/>.</param>
        /// <returns>New array with repeated copies of the source array along the dimensions as specified by <paramref name="d0"/>....</returns>
        
        public unsafe static RetT Repmat<T, LocalT, InT, OutT, RetT, StorageT>(
                        this ConcreteArray<T, LocalT, InT, OutT, RetT, StorageT> A,
                        long d0, long d1, long d2, long d3, long d4, long d5, long d6)
                where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
                where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
                where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
                where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

            return A.Repmat(MathInternal.size(d0, d1, d2, d3, d4, d5, d6));

        }

        #endregion
    }
}
