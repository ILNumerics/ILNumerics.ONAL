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
using ILNumerics.Core.Functions.Builtin.InnerLoops;
using ILNumerics.Core.Global;
using ILNumerics.Core.StorageLayer;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace ILNumerics.Core.Functions.Builtin {

    /// <summary>
    /// Unary operator template for unary operations, potentially inplace. Output type is the same as the input type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="LocalT"></typeparam>
    /// <typeparam name="InT"></typeparam>
    /// <typeparam name="OutT"></typeparam>
    /// <typeparam name="RetT"></typeparam>
    /// <typeparam name="StorageT"></typeparam>
    internal abstract unsafe class UnaryBaseInPlaceParameterized2<T, LocalT, InT, OutT, RetT, StorageT> 

            where T : struct
            where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

        #region attributes
        uint m_sizeOfT = Storage<T>.SizeOfT; 
        #endregion

        /// <summary>
        /// General operator over elements of <paramref name="storage"/>. Inplace operation.
        /// </summary>
        /// <param name="storage">Input storage.</param>
        /// <param name="v1">Additional parameter will end up in inner loops.</param>
        /// <param name="v2">Additional parameter will end up in inner loops.</param>
        /// <remarks><para>The operation is efficiently performed on all elements of the input storage <paramref name="storage"/>.</para>
        /// <para>The storage order of <paramref name="storage"/> is not changed.</para>
        /// <para>The storage is expected to be not shared with other arrays.</para>
        /// </remarks>
        /// <exception cref="InvalidOperationException">If <paramref name="storage"/> or its memory is shared by more than one array.</exception>
        
        internal unsafe void operate(StorageT storage, T v1, T v2) {
            if (object.Equals(storage, null) || storage.S.NumberOfElements == 0) {
                return;
            }
            // we need some computations
            byte* pIn = (byte*)storage.Handles[0].Pointer + storage.S.BaseOffset * m_sizeOfT;

            var outLen = storage.S.NumberOfElements;

            if (!storage.S.IsContinuous) {
                throw new InvalidOperationException("This operator requires the operands storage to be continously layed out and suitable for inplace operations.");
            }
            System.Diagnostics.Debug.Assert(storage.Handles.ReferenceCount == 1 && storage.ReferenceCount <= 1, "This inplace operation requires the input array to be the only array pointing to its storage, preventing from unintended side effects."); 

            Continous64Inplace(
                    pIn,
                    0, outLen, v1, v2);

        }

        #region abstract interface to be defined by (derived) unary inner functions
        public abstract void Continous64Inplace(byte* v, long start, long workItemLength, T v1, T v2);
        #endregion

    }
}

